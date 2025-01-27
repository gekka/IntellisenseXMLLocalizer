using Gekka.Language.Translator.Browser;
using OpenQA.Selenium;
using System.Diagnostics;
using System.Collections.Generic;

namespace Gekka.Language.Translator.Interfaces
{
    public interface IWebDriverFactory
    {
        //IWebDriver? GetDriver(CustomDriverOption dvropt);
        //IWebDriver? GetDriver(out Process? process, CustomDriverOption dvropt);

        string BrowserName { get; }

        Process? TryGetExisting(out uint port);

        WebDriverAndProcess GetDriverAndProcess(CustomDriverOption dvropt);
    }

    public class WebDriverAndProcess
    {
        public WebDriverAndProcess(string browserName, IWebDriver driver, Process process)
        {
            BrowserName = browserName;
            Driver = driver;
            Process = process;
        }

        public string BrowserName { get; }
        public IWebDriver Driver { get; }
        public Process Process { get; }
    }
}