//--------------------------------------------------
// <copyright file="SeleniumTest.cs" company="Magenic">
//  Copyright 2016 Magenic, All rights Reserved
// </copyright>
// <summary>Sample Selenium test class</summary>
//--------------------------------------------------
using Magenic.MaqsFramework.BaseSeleniumTest;
using Magenic.MaqsFramework.Utilities.Helper;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Remote;
using System;

namespace Tests
{
    /// <summary>
    /// Simple login test class
    /// </summary>
    [TestClass]
    public class BaseTest : BaseSeleniumTest
    {
        protected override IWebDriver GetBrowser()
        {
           if(SeleniumConfig.GetBrowserName().Equals("Remote", StringComparison.CurrentCultureIgnoreCase))
            {
                return GetRandom();
            }

            return SeleniumConfig.Browser();
        }

        private IWebDriver GetRandom()
        {
            DesiredCapabilities caps = new DesiredCapabilities();

            var rnd = new Random((unchecked((int)DateTime.Now.Ticks)));

            switch (rnd.Next(0, 4))
            {
                case 0:
                    caps.SetCapability("browserName", "Chrome");
                    caps.SetCapability("platform", "OS X 10.11");
                    caps.SetCapability("version", "46.0");
                    break;
                case 1:
                    caps.SetCapability("browserName", "Chrome");
                    caps.SetCapability("platform", "Windows 10");
                    caps.SetCapability("version", "55.0");
                    break;
                case 2:
                    caps.SetCapability("browserName", "Chrome");
                    caps.SetCapability("platform", "OS X 10.11");
                    caps.SetCapability("version", "54.0");
                    break;
                case 3:
                    caps.SetCapability("browserName", "Chrome");
                    caps.SetCapability("platform", "Linux");
                    caps.SetCapability("version", "48.0");
                    break;
                default:
                    caps.SetCapability("browserName", "Chrome");
                    caps.SetCapability("platform", "Windows XP");
                    caps.SetCapability("version", "54.0");
                    break;
            }



            this.Log.LogMessage("Caps: " + caps.ToString());

            caps.SetCapability("username", Config.GetValue("sauceName"));
            caps.SetCapability("accessKey", Config.GetValue("accessKey"));

            return new RemoteWebDriver(new Uri(Config.GetValue("HubUrl")), caps);
        }
    }
}
