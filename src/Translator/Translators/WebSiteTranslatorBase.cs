//using Gekka.Language.Translator.Browser.Factories;
using Gekka.Language.Translator.Interfaces;
using OpenQA.Selenium;

namespace Gekka.Language.Translator.Translators
{
    class WebSiteTranslatorBase : System.IDisposable
    {
        public WebSiteTranslatorBase(WebDriverAndProcess dp, string comment) 
        {
            this.p =dp.Process;
            this.drv = dp.Driver;
            this.exec = (IJavaScriptExecutor)this.drv;
            this.Comment = comment;
        }

        protected System.Diagnostics.Process p { get; private set; }
        protected IWebDriver drv { get; private set; }
        protected IJavaScriptExecutor exec { get; private set; }

        public string Comment { get; }//=> BrowserTranslatorFactory.Default.Comment;

        public void Dispose()
        {
            drv?.Close();
            drv?.Dispose();
        }
    }
}
