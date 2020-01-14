using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using OpenQA.Selenium.Chrome;
using Signum.React.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium;
using OpenQA.Selenium.Remote;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace AtTest.Test.React
{
    public class AtTestTestClass
    {
        public static string BaseUrl { get; private set; }

        static AtTestTestClass()
        {
            var config = new ConfigurationBuilder()
                 .SetBasePath(Directory.GetCurrentDirectory())
                 .AddJsonFile("appsettings.json")
                 .AddJsonFile($"appsettings.{System.Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")}.json", true)
                 .AddUserSecrets(typeof(AtTestTestClass).Assembly)
                 .Build();

            BaseUrl = config["Url"];
        }

        public static void Browse(string username, Action<AtTestBrowser> action)
        {
            var selenium = new ChromeDriver("../../../");

            var browser = new AtTestBrowser(selenium);
            try
            {
                browser.Login(username, username);
                action(browser);
            }
            catch (UnhandledAlertException)
            {
                selenium.SwitchTo().Alert();

            }
            finally
            {
                selenium.Close();
            }
        }
    }

    public class AtTestBrowser : BrowserProxy
    {
        public override string Url(string url)
        {
            return AtTestTestClass.BaseUrl + url;
        }

        public AtTestBrowser(RemoteWebDriver driver)
            : base(driver)
        {
        }

        public override void Login(string username, string password)
        {
            base.Login(username, password);

            string culture = Selenium.FindElement(By.Id("languageSelector")).SelectElement().SelectedOption.GetAttribute("value");

            Thread.CurrentThread.CurrentCulture = Thread.CurrentThread.CurrentUICulture = new CultureInfo(culture);
        }

    }
}
