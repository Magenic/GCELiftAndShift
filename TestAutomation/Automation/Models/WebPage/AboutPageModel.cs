using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;

namespace Models.WebPage
{
    /// <summary>
    /// Page object for AboutPageModel
    /// </summary>
    public class AboutPageModel : AbstractPageModel
    {
        /// <summary>
        /// The page url
        /// </summary>
        private static string AboutPageUrl = PageUrl + "Home/About";

        /// <summary>
        /// About page header 'By' finder
        /// </summary>
        private static By AboutPageTitle = By.CssSelector("[class='page-title']");

        /// <summary>
        /// Initializes a new instance of the <see cref="AboutPageModel" /> class.
        /// </summary>
        /// <param name="webDriver">The selenium web driver</param>
        public AboutPageModel(IWebDriver webDriver) : base(webDriver)
        {

        }

        /// <summary>
        /// Open the page
        /// </summary>
        public void OpenPage()
        {
            // Open the gmail login page
            this.webDriver.Navigate().GoToUrl(AboutPageUrl);
            this.AssertPageLoaded();
        }

        /// <summary>
        /// Verify we are on the about page
        /// </summary>
        public void AssertPageLoaded()
        {
            //Assert depends on what testing framework is being used
            Assert.IsTrue(
                this.webDriver.Url.Equals(AboutPageUrl, System.StringComparison.CurrentCultureIgnoreCase),
                "Expected to be on '{0}', but was on '{1}' instead", this.webDriver.Url);
        }
    }
}
