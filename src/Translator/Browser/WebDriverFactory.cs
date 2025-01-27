namespace Gekka.Language.Translator.Browser
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Gekka.Language.Translator.Interfaces;
    using OpenQA.Selenium;
    using OpenQA.Selenium.Chromium;

    internal abstract class WebDriverFactoryBase<TDriver, TOptions> : IWebDriverFactory where TDriver : ChromiumDriver
        where TOptions : ChromiumOptions, new()
    {
        protected WebDriverFactoryBase(string name, string processNameBrowser, string processNameDriver)
        {
            BrowserName = name;
            this.processNameBrowser = processNameBrowser;
            this.processNameDriver = processNameDriver;
        }

        public virtual string BrowserName { get; }
        private string processNameBrowser;
        private string processNameDriver;

        private IEnumerable<(System.Diagnostics.Process process, IList<string> args)> GetEdges()
            => Win.Util.GetProcessAndCommandLineArgs(processNameBrowser);

        protected abstract TDriver NewDriver(TOptions opt);

        /// <summary>既存のブラウザを探す</summary>
        /// <param name="port">見つかったブラウザのデバッグ接続用ポート番号</param>
        /// <returns></returns>
        public System.Diagnostics.Process? TryGetExisting(out uint port)
        {
            const string ARG = "--remote-debugging-port=";
            port = 0;

            foreach (var (process, args) in GetEdges())
            {
                var part = args.FirstOrDefault(_ => _.StartsWith(ARG, StringComparison.OrdinalIgnoreCase));

                if (part != null && uint.TryParse(part.Substring(ARG.Length), out port) && port != 0)
                {
                    return process;
                }
            }
            return null;
        }


        //public IWebDriver? GetDriver(CustomDriverOption dvropt) => GetDriver(out _, dvropt);

        /// <summary>既存のブラウザか、なければ新しいブラウザを取得</summary>
        /// <param name="process"></param>
        /// <returns></returns>
        private IWebDriver? GetDriver(out System.Diagnostics.Process? process, CustomDriverOption dvropt)
        {
            process = TryGetExisting(out var port);
            if (process != null)
            {
                TOptions opt = new TOptions();
                opt.DebuggerAddress = "127.0.0.1:" + port.ToString();

                return NewDriver(opt);
            }
            else
            {
                return NewDriver(out process, false, dvropt);
            }
        }

        /// <summary>新しいブラウザを取得</summary>
        /// <param name="process"></param>
        /// <param name="headless">Headlessモードで起動させるか</param>
        /// <returns></returns>
        public IWebDriver? NewDriver(out System.Diagnostics.Process? process, bool headless, CustomDriverOption dvropt)
        {
            process = null;

            string UNIQUE_KEY = "script_id-";
            TDriver? drv = null;
            string unique = Guid.NewGuid().ToString("N");
            string uniqueArg = UNIQUE_KEY + unique;
            try
            {
                TOptions opt = new TOptions();
                opt.AddArgument("--incognito");
                opt.AddArgument(uniqueArg);
                if (headless)
                {
                    opt.AddArgument("--headless");
                }
                if (dvropt.AllowLocalFile)
                {
                    opt.AddArgument("--allow-file-access-from-files");
                    opt.AddArgument("--enable-local-file-accesses ");
                }

                if (dvropt.TranslateLanguages.Count > 0)
                {
                    opt.AddUserProfilePreference("translate", new Dictionary<string, bool>() { { "enabled", true } });

                    opt.AddUserProfilePreference("translate_whitelists", dvropt.TranslateLanguages.ToDictionary(_ => _.From, _ => _.To));
                }


                drv = NewDriver(opt);

                foreach (var pa in GetEdges())
                {
                    if (pa.args.Any(_ => _.Contains(uniqueArg)))
                    {
                        process = pa.process;
                        return drv;
                    }
                }
                return null;
            }
            catch
            {
                drv?.Dispose();
                throw;
            }
        }

        public WebDriverAndProcess GetDriverAndProcess(CustomDriverOption dvropt)
        {
            var d = GetDriver(out var p, dvropt);
            if (d == null)
            {
                throw new ApplicationException("WebDriverを取得できませんでした");
            }
            if (p == null)
            {
                throw new ApplicationException("WebDriverプロセスを取得できませんでした");
            }
            return new WebDriverAndProcess(this.BrowserName, d, p);
        }


    }
}
