using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using Gekka.Language.Translator.Browser;
using System.Threading;
using Gekka.Language.Translator.Interfaces;

namespace Gekka.Language.Translator.Translators
{
    class DeepLWebTranslator : WebSiteTranslatorBase, ITranslator, IWebSite
    {
        const string comment = "このファイルの翻訳テキストは、Deeplのウェブサイトを使用して翻訳されています。翻訳された内容はOSSに使用しないでください。\r\nThis translated text in this file was translated from DeepL web sites.  If you use the translated content for OSS, please refer to the original agreement.";


        public DeepLWebTranslator(WebDriverAndProcess dp)
            : base(dp, comment)
        {
        }

        private System.Uri site = new Uri("https://www.deepl.com/ja/translator");

        private const int TimeoutMs_Clear = 10000;
        private const int TimeoutMs_Translate = 10000;

        public int LimitLength { get; } = 2000;

        public string TranslatorName => BrowserTranslatorFactory.Default.TranslatorName;
        //public string Comment => BrowserTranslatorFactory.Default.Comment;
        public bool IsMultipleTranslator => BrowserTranslatorFactory.Default.IsMultipleTranslator;

        private System.Diagnostics.Stopwatch watch = new System.Diagnostics.Stopwatch();
        private System.TimeSpan lastTime = TimeSpan.Zero;

        public TimeSpan IntervalMs { get; set; } = TimeSpan.FromSeconds(3);

        public async Task GotoSiteAsync(System.Threading.CancellationToken token)
        {
            if (drv == null) throw new InvalidOperationException();

            drv.GotoEx(this.site);
            for (int i = 0; i < 100; i++)
            {
                await Task.Delay(100, token);
                token.ThrowIfCancellationRequested();

                var container = await drv.WaitUntil(By.CssSelector("#textareasContainer"), token);
                if (container != null)
                {
                    return;
                }
            }
            throw new ApplicationException("サイトの移動が認識でませんでした");
        }

        private async Task ClearAsync(System.Threading.CancellationToken token)
        {
            DateTime t = DateTime.Now.AddMilliseconds(TimeoutMs_Clear);
            while (t > DateTime.Now && !token.IsCancellationRequested)
            {
                try
                {
                    var undo = drv.FindElements(By.CssSelector("#textareasContainer button[aria-label='元に戻す']")).FirstOrDefault();
                    if (undo == null)
                    {
                        return;
                    }
                    else
                    {
                        if (!undo.Enabled)
                        {
                            return;
                        }
                        else
                        {
                            try
                            {
                                undo.ClickJS();
                            }
                            catch (Exception)
                            {
                                await Task.Delay(100);
                            }
                        }

                    }
                }
                catch
                {
                    throw;
                }
                await Task.Delay(10, token);
            }
            throw new ApplicationException("入力エリアをクリアできませんでした");
        }

        private async Task SetEnglishAsync(string english, System.Threading.CancellationToken token)
        {
            english = english.Trim();
            if (english.Length > LimitLength)
            {
                throw new ArgumentOutOfRangeException("文字が長すぎます");
            }

            await Task.Run(async () =>
            {
                await ClearAsync(token);

                //var source = drv.FindElement(By.CssSelector("d-textarea[name='source']"));
                //source.SendKeys("");
                for (; ; )
                {
                    token.ThrowIfCancellationRequested();

                    var target = drv.FindElement(By.CssSelector("d-textarea[name='target']"));
                    var text = target.Text;
                    if (string.IsNullOrWhiteSpace(text))
                    {
                        break;
                    }
                    await Task.Delay(10, token);
                }
                var textarea = drv.FindElement(By.CssSelector("d-textarea[name='source']"));

                //for (int i = 0; i < 2; i++)
                {
                    try
                    {
                        textarea.SendKeys(english);
                        return;
                    }
                    catch (Exception ex)
                    {
                        throw new ApplicationException("文字列入力が行えませんでした", ex);
                    }
                }

            });
        }

        private async Task<string> TryGetText(CancellationToken token)
        {
            string result = "";
            for (; ; )
            {
                token.ThrowIfCancellationRequested();

                var target = drv.FindElement(By.CssSelector("d-textarea[name='target']"));
                result = target.Text.Trim();
                if (!string.IsNullOrWhiteSpace(result))
                {
                    //await ClearAsync();
                    return result;
                }
                await Task.Delay(10, token);
            }
        }

        public async Task<string?> GetLocalizeTextAsync(string english, System.Threading.CancellationToken token)
        {
            if (string.IsNullOrWhiteSpace(english))
            {
                return english;
            }

            if (!watch.IsRunning)
            {
                watch.Start();
            }
            else
            {
                var now = watch.Elapsed;
                var wait = IntervalMs - (now - lastTime);
                if (wait > TimeSpan.Zero)
                {
                    await Task.Delay(wait, token);

                    token.ThrowIfCancellationRequested();
                    lastTime = watch.Elapsed;
                }
            }

            await SetEnglishAsync(english, token);
            token.ThrowIfCancellationRequested();

            await Task.Delay(1000, token);
            token.ThrowIfCancellationRequested();

            var result = await Task.Run<string>(async () =>
            {

                string result;
                result = await TryGetText(token);

                if (result.Length > 1)
                {
                    return result;
                }

                DateTime timeout = DateTime.Now.AddMilliseconds(TimeoutMs_Translate);
                while (timeout > DateTime.Now)
                {
                    await Task.Delay(100, token);
                    token.ThrowIfCancellationRequested();

                    var result2 = await TryGetText(token);
                    if (result != result2)
                    {
                        await ClearAsync(token);
                        return result2;
                    }
                }
                throw new TimeoutException("翻訳結果が得られませんでした");
            }, token);

            lastTime = watch.Elapsed;
            return result;
        }

    }


    internal class DeepLWebTranslatorFactory : TranslatorFactoryBase<DeepLWebTranslator, DeepLWebTranslatorFactory>, IBrowserTranslatorFactory
    {

        public DeepLWebTranslatorFactory() : base("DeepL Web", false)
        {
            this.DriverFactory = Factory.GetDriverFactories().First();
        }

        public override ITranslator Create()
        {
            if (DriverFactory == null)
            {
                throw new System.InvalidOperationException();
            }
            return new DeepLWebTranslator(this.DriverFactory.GetDriverAndProcess(this.Option));
        }

        public IWebDriverFactory DriverFactory { get; set; }
        public CustomDriverOption Option { get; } = new CustomDriverOption();
    }
}
