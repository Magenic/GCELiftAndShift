using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;

namespace Models.WebPage
{
    /// <summary>
    /// Page object for ContactPageModel
    /// </summary>
    public class ContactPageModel: AbstractPageModel
    {
        /// <summary>
        /// The page url
        /// </summary>
        private static string ContactPageUrl = PageUrl + "Home/Contact";

       
        /// <summary>
        /// Contact page header 'By' finder
        /// </summary>
        protected static By ContactPageTitle = By.CssSelector("[class='page-title']");

        /// <summary>
        /// Initializes a new instance of the <see cref="ContactPageModel" /> class.
        /// </summary>
        /// <param name="webDriver">The selenium web driver</param>
        public ContactPageModel(IWebDriver webDriver) : base(webDriver)
        {

        }

        /// <summary>
        /// Open the page
        /// </summary>
        public void OpenPage()
        {
            // Open the gmail login page
            this.webDriver.Navigate().GoToUrl(ContactPageUrl);
            this.AssertPageLoaded();
        }

        /// <summary>
        /// Verify we are on the Contact Page
        /// </summary>
        public void AssertPageLoaded()
        {
            //Assert depends on what testing framework is being used
            Assert.IsTrue(
                this.webDriver.Url.Equals(ContactPageUrl, System.StringComparison.CurrentCultureIgnoreCase),
                "Expected to be on '{0}', but was on '{1}' instead", this.webDriver.Url);
        }
    }
}
