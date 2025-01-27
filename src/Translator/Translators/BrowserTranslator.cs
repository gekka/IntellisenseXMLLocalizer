using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using OpenQA.Selenium;
using Gekka.Language.Translator.Browser;
using Gekka.Language.Translator.Interfaces;

namespace Gekka.Language.Translator.Translators
{
    /// <summary>ブラウザの翻訳機能でまとめて翻訳</summary>
    internal class BrowserTranslator : WebSiteTranslatorBase, IMultipleTranslator, IWebSite
    {
        private const string comment = "このファイルの翻訳テキストは、%%の翻訳機能を使用して翻訳されています。翻訳された内容はOSSに使用しないでください。\r\nThis translated text in this file was translated using %%'s translation function. This content must not be used for OSS.";
        private const int BLOCK_SIZE = 1000; //一度に1000行までに制限
        private const int WAIT_MS = 5000; //翻訳待ちする時間

        public BrowserTranslator(WebDriverAndProcess dp)
            : base(dp, comment.Replace("%%", dp.BrowserName))
        {
        }

        /// <inheritdoc/>
        public string TranslatorName => BrowserTranslatorFactory.Default.TranslatorName;

        /// <inheritdoc/>
        public bool IsMultipleTranslator => BrowserTranslatorFactory.Default.IsMultipleTranslator;

        /// <summary>渡した文字列を翻訳</summary>
        /// <param name="originalText">翻訳元の文字列</param>
        /// <param name="token"></param>
        /// <returns>翻訳結果</returns>
        public async Task<string?> GetLocalizeTextAsync(string originalText, CancellationToken token)
        {
            Dictionary<string, string?> dic = new Dictionary<string, string?>() { { originalText, null } };
            await GetLocalizeTextAsync(dic, token);
            return dic.First().Value;
        }

        /// <summary>辞書内の翻訳結果がない文字列を翻訳して、翻訳結果を辞書の値に入れる</summary>
        /// <param name="dic">翻訳元の文字列をキーに、翻訳結果を値に入れた辞書。</param>
        /// <param name="token"></param>
        /// <returns></returns>
        public async Task GetLocalizeTextAsync(IDictionary<string, string?> dic, CancellationToken token)
        {
            if (dic.Count == 0)
            {
                return;
            }

            var keys = dic.Keys.ToArray();
            var groups = keys.Select((k, idx)
                => new { gkey = idx / BLOCK_SIZE, Value = k })
                    .GroupBy(_ => _.gkey)
                    .Select(_ => _.Select(_ => _.Value).ToList())
                    .ToList();

            int sourceCount = dic.Count;
            int count = 0;
            OnProgress(0, sourceCount);

            foreach (var grp in groups)
            {
                await GetLocalizeTextAsync(dic, grp, token);

                count += grp.Count;
                OnProgress(count, sourceCount);
            }
        }

        /// <summary>辞書内の翻訳結果がない文字列を翻訳して、翻訳結果を辞書の値に入れる</summary>
        /// <param name="origianlTexts">翻訳元の文字列をキーに、翻訳結果を値に入れた辞書。</param>
        /// <param name="keys">翻訳したい文字列の一覧</param>
        /// <param name="token"></param>
        /// <returns></returns>
        private async Task GetLocalizeTextAsync(IDictionary<string, string?> dic, IList<string> keys, CancellationToken token)
        {
            string DETECT_TEXT = System.Web.HttpUtility.HtmlEncode("The unnamed text at the end of lines."); //翻訳が終わったことを検出するのに使用する名状しがたき文字列。

            var tempHTML = GetTempHTML();
            try
            {
                using (var file = System.IO.File.OpenWrite(tempHTML))
                {
                    var enc = new System.Text.UTF8Encoding(true);
                    System.IO.StreamWriter w = new System.IO.StreamWriter(file, enc);

                    w.WriteLine("<!DOCTYPE html ><html lang=\"en\"> <head></head> <body>");
                    for (int i = 0; i < keys.Count; i++)
                    {
                        w.WriteLine($"<p id=\"n{i}\">{System.Web.HttpUtility.HtmlEncode(keys[i])}</p>");
                    }

                    w.WriteLine($"<p id=\"detect\" style=\"visibility: collapse;\">" + DETECT_TEXT + "</p>");

                    w.WriteLine("</body></html>");

                    await w.FlushAsync();
                }


                await drv.Navigate().GoToUrlAsync("file://" + tempHTML.Replace("\\", "/"));

                await drv.WaitUntil(By.Id("n0"), token);
                await drv.WaitUntil(By.Id("n" + (keys.Count - 1).ToString()), token);
                await drv.WaitUntil(By.Id("detect"), token);

                drv.SwitchTo();

                var title = System.IO.Path.GetFileName(tempHTML);


                bool isDetectTranslated = false;
                for (int i = 0; i < 10; i++)
                {
                    var n0 = drv.FindElement(By.CssSelector("p[id='n0']"));
                    new OpenQA.Selenium.Interactions.Actions(drv).ContextClick(n0).Perform();
                    await Task.Delay(100);

                    bool successClick = Win.UIA.SelectContextMenu(p.MainWindowHandle, (name) =>
                    {
                        return name.Contains("(T)");
                    });

                    //var n0_before = drv.FindElement(By.Id("n0")).Text;

                    if (!successClick)
                    {
                        continue;
                        //throw new ApplicationException("翻訳クリック失敗");
                    }

                    drv.FindElement(By.TagName("body")).ClickJS();

                    System.Diagnostics.Stopwatch watch = new System.Diagnostics.Stopwatch();
                    watch.Start();


                    foreach (var n in drv.FindElements(By.CssSelector("p[id*='n']")))
                    {
                        drv.FocusJS(n);
                    }

                    var delay = WAIT_MS - watch.Elapsed.TotalMilliseconds;
                    if (delay > 0)
                    {
                        await Task.Delay(WAIT_MS);
                    }

                    var detect = drv.FindElement(By.Id("detect"));
                    drv.FocusJS(detect);

                    if (detect.Text != DETECT_TEXT)
                    {
                        //行数が少ないと失敗する？
                        //行数が少ないとスクロールにかかる時間が短いので、翻訳処理が終わる前に進んでしまうのかもしれない。

                        isDetectTranslated = true;
                        break;
                    }
                }

                if (!isDetectTranslated)
                {
                    throw new ApplicationException("翻訳文を検出できませんでした");
                }

                foreach (var n in drv.FindElements(By.CssSelector("p[id*='n']")))
                {
                    drv.FocusJS(n);

                    int id = int.Parse(n.GetAttribute("id").Substring(1));
                    var original = keys[id];
                    var translated = n.Text;
                    dic[keys[id]] = translated;

                    if (!isDetectTranslated)
                    {
                        isDetectTranslated |= !string.Equals(original, translated);
                    }
                }
            }
            catch //(Exception ex)
            {
                throw;
            }
            finally
            {
                System.IO.File.Delete(tempHTML);
            }
        }

        private static string GetTempHTML()
        {
            var temp = System.IO.Path.GetTempFileName();
            try
            {
                var tempthml = System.IO.Path.ChangeExtension(temp, ".html");
                System.IO.File.Move(temp, tempthml);
                return tempthml;
            }
            catch
            {
                System.IO.File.Delete(temp);
                throw new ApplicationException("一時ファイルを作成できませんでした");
            }         
        }


        //private void s(nint h)
        //{
        //    UIAutomationClient.CUIAutomation8 uia = new UIAutomationClient.CUIAutomation8();
        //    var e = uia.ElementFromHandle(h);


        //}

        public Task GotoSiteAsync(System.Threading.CancellationToken token)
        {
            drv.Url = "about:blank";
            drv.Navigate();
            return Task.CompletedTask;
        }

        public event EventHandler<MultipleTranslatorEventArgs>? Progress;

        protected void OnProgress(int translatedCount, int sourceCount)
        {
            Progress?.Invoke(this, new MultipleTranslatorEventArgs(translatedCount, sourceCount));
        }
        protected void OnProgress(int translatedCount, int sourceCount, double percent)
        {
            Progress?.Invoke(this, new MultipleTranslatorEventArgs(translatedCount, sourceCount, percent));
        }
    }

    internal class BrowserTranslatorFactory : TranslatorFactoryBase<BrowserTranslator, BrowserTranslatorFactory>, IBrowserTranslatorFactory
    {

        public BrowserTranslatorFactory() : base("BrowserTranslator", true)
        {
            this.DriverFactory = Factory.GetDriverFactories().First();
            this.Option.TranslateLanguages.Add(new TranslateLang("en", "ja"));
        }

        public override ITranslator Create()
        {
            if (DriverFactory == null)
            {
                throw new System.InvalidOperationException();
            }

            return new BrowserTranslator(this.DriverFactory.GetDriverAndProcess(this.Option));
        }

        public IWebDriverFactory DriverFactory { get; set; }
        public CustomDriverOption Option { get; } = new CustomDriverOption();
    }
}
