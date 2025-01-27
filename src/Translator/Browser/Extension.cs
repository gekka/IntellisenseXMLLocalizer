namespace Gekka.Language.Translator.Browser
{
    using System;
    using System.Threading.Tasks;
    using OpenQA.Selenium;
    using OpenQA.Selenium.Support.UI;
    using System.Collections.Generic;
    using System.Linq;

    static class Extension
    {

        public static void GotoEx(this IWebDriver drv, Uri url)
        {
            string u;
            try
            {
                u = drv.Url;
                if (drv.Url == url.ToString())
                {
                    drv.Navigate().Refresh();
                    try
                    {
                        drv.SwitchTo().Alert().Accept();
                    }
                    catch
                    {
                    }
                }
                else
                {
                    drv.Navigate().GoToUrl(url);
                }
            }
            catch
            {
                throw;
            }



        }

        public static IWebDriver GetDriver(this IWebElement elem)
        {
            if (elem is IWrapsDriver iwd)
            {
                return iwd.WrappedDriver;
            }

            if (elem is WebElement we)
            {
                return we.WrappedDriver;
            }
            throw new ApplicationException("IWebDriverを取得できませんでした");
        }

        //public static IJavaScriptExecutor GetJavaScriptExecutor(this IWebElement elem)
        //{
        //    return (IJavaScriptExecutor)GetDriver(elem);
        //}

        public static bool IsHiddenJS(this IWebElement elem)
        {
            return IsHiddenJS((IJavaScriptExecutor)GetDriver(elem), elem);
        }

        public static bool IsHiddenJS(this IJavaScriptExecutor exec, IWebElement elem)
        {
            var b = (bool)exec.ExecuteScript("return arguments[0].offsetParent==null", elem);
            return (b == true);
        }

        public static IWebElement[] GetOnlyVisible(this IEnumerable<IWebElement> elements)
        {
            return elements.Where(_ => !_.IsHiddenJS()).ToArray();
        }


        public static string[] classList(this IWebElement elem)
        {
            string @class = elem.GetAttribute("class");
            return @class.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
        }

        public static void ClickJS(this IWebElement elem)
        {
            ClickJS(GetDriver(elem), elem);
        }

        public static void ClickJS(this IWebDriver drv, IWebElement elem)
        {

            ((IJavaScriptExecutor)drv).ExecuteScript($"arguments[0].scrollIntoView();arguments[0].click()", elem);
        }


        public static void FocusJS(this IWebElement elem)
        {
            FocusJS(GetDriver(elem), elem);
        }
        public static void FocusJS(this IWebDriver drv, IWebElement elem)
        {
             ((IJavaScriptExecutor)drv).ExecuteScript("arguments[0].scrollIntoView()", elem);
            if (elem.TagName == "input")
            {
                try
                {
                    elem.SendKeys("");
                }
                catch
                {
                    //elem.ClickJS();
               
                }
            }
            else
            {
                ((IJavaScriptExecutor)drv).ExecuteScript("arguments[0].focus()", elem);
            }
        }

        public static IWebElement? GetFocusElement(this IWebElement elem)
        {
            return GetFocusElement(GetDriver(elem));
        }
        public static IWebElement? GetFocusElement(this IWebDriver drv)
        {

            return  ((IJavaScriptExecutor)drv).ExecuteScript("return document.activeElement") as IWebElement;
        }

        public static void SetValueJS(this IWebElement elem, string value)
        {
            SetValueJS(GetDriver(elem), elem, value);
        }
        public static void SetValueJS(IWebDriver drv, IWebElement elem, string value)
        {
            ((IJavaScriptExecutor)drv).ExecuteScript($"arguments[0].value=arguments[1]", elem, value);
        }

        public static Task<IWebElement> WaitUntil(this IWebDriver drv, By by, double msec = 10000)
        {
            return WaitUntil(drv, by, msec, System.Threading.CancellationToken.None);
        }
        public static Task<IWebElement> WaitUntil(this IWebDriver drv, By by, System.Threading.CancellationToken token, double msec = 10000)
        {
            return WaitUntil(drv, by, msec, token);
        }

        public static Task<IWebElement> WaitUntil(this IWebDriver drv, By by, double msec, System.Threading.CancellationToken token)
        {
            return WaitUntil(drv, drv, by, msec, token);
        }

        public static Task<IWebElement> WaitUntil(this IWebDriver drv, ISearchContext root, By by, double msec = 10000)
        {
            return WaitUntil(drv, root, by, msec, System.Threading.CancellationToken.None);
        }
        public static Task<IWebElement> WaitUntil(this IWebDriver drv, ISearchContext root, By by, System.Threading.CancellationToken token, double msec = 10000)
        {
            return WaitUntil(drv, root, by, msec, token);
        }

        public static async Task<IWebElement> WaitUntil(this IWebDriver drv, ISearchContext root, By by, double msec, System.Threading.CancellationToken token)
        {
            if (double.IsNaN(msec) || double.IsInfinity(msec))
            {
                throw new ArgumentOutOfRangeException();
            }

            for (; ; )
            {
                token.ThrowIfCancellationRequested();

                //var w = new WebDriverWait(drv, TimeSpan.FromMilliseconds(100));
                try
                {
                    var found = root.FindElements(by).FirstOrDefault();
                    if (found != null)
                    {
                        return found;
                    }
                }
                catch
                {
                }

                msec -= 100;
                if (msec < 0)
                {
                    break;
                }
                await Task.Delay(100, token);
            }


            throw new TimeoutException("要素の生成待ちで、要素が見つかりませんでした\r\n" + by.ToString());
        }
    }
}
