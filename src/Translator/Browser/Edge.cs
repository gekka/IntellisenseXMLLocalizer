namespace Gekka.Language.Translator.Browser
{
    using System;
    using System.Collections.Generic;
    using OpenQA.Selenium.Edge;

    internal class Edge : WebDriverFactoryBase<EdgeDriver, EdgeOptions>
    {
        public Edge() : base("Edge", "msedge.exe", "edgedriver.exe")
        {
        }

        protected override EdgeDriver NewDriver(EdgeOptions opt) => new EdgeDriver(opt);
    }
}

