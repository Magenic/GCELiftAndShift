using Magenic.MaqsFramework.Utilities.Helper;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Models.WebPage;

namespace Tests
{
    /// <summary>
    /// Composite Selenium test class
    /// </summary>
    [TestClass]
    public class SeleniumTest : BaseTest
    {
        /// <summary>
        /// Open page test
        /// </summary>
        [TestMethod]
        public void PageNavigation()
        {
            HomePageModel model = new HomePageModel(this.WebDriver);
            
            model.OpenPage();
            model.ToContactPage().AssertPageLoaded();
            model.ToAboutPage().AssertPageLoaded();
            model.ToHomePage().AssertPageLoaded();
            model.ToCreateReportPage().AssertPageLoaded();
            model.ToHomePage().AssertPageLoaded();
            model.ToViewReportPage().AssertPageLoaded();
            model.ToHomePage().AssertPageLoaded();
        }

        [TestMethod]
        public void CanLogin()
        {
            HomePageModel model = new HomePageModel(this.WebDriver);
            model.OpenPage();
            model.AssertPageLoaded();
        }

        [TestMethod]
        public void TryAccessWithoutCreds()
        {
            this.WebDriver.Navigate().GoToUrl(Config.GetValue("BaseUrl"));
            HomePageModel model = new HomePageModel(this.WebDriver);


            Assert.IsFalse(model.IsOnPage(), "Page should not have loaded");
        }

        
        [TestMethod]
        public void CreateReport()
        {
            HomePageModel model = new HomePageModel(GetBrowser());

            model.OpenPage();
            model.ToCreateReportPage().FillOutForm();

        }

        [TestMethod]
        public void UpdateReport()
        {
            HomePageModel model = new HomePageModel(GetBrowser());

            model.OpenPage();
            model.ToViewReportPage().updateReport();
        }
    }
}
