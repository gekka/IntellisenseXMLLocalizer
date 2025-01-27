namespace Gekka.Language.Translator.Browser
{
    using OpenQA.Selenium.Chrome;

    internal class Chrome : WebDriverFactoryBase<ChromeDriver, ChromeOptions>
    {
        public Chrome() : base("Chrome", "chrome.exe", "chromedriver.exe")
        {
        }

        protected override ChromeDriver NewDriver(ChromeOptions opt) => new ChromeDriver(opt);
    }
}

