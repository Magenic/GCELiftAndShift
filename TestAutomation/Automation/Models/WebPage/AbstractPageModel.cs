using Magenic.MaqsFramework.Utilities.Helper;
using OpenQA.Selenium;

namespace Models.WebPage
{
    /// <summary>
    /// Page object for AbstractPageModel
    /// </summary>
    public class AbstractPageModel
    {
        /// <summary>
        /// The page url
        /// </summary>
        protected static string LoginUrl = Config.GetValue("WebSiteBase");

        /// <summary>
        /// The page url
        /// </summary>
        protected static string PageUrl = Config.GetValue("BaseUrl");

        /// <summary>
        /// Header 'By' finder
        /// </summary>
        protected static By Header = By.CssSelector("[class='navbar-collapse collapse']");

        /// <summary>
        /// The username element 'By' finder
        /// </summary>
        protected static By logo = By.CssSelector("#logo");

        /// <summary>
        /// Home page link 'By' finder
        /// </summary>
        protected static By HomeLink = By.CssSelector("[href='/']");

        /// <summary>
        /// About navigation Link 'By' finder
        /// </summary>
        protected static By AboutLink = By.CssSelector("[href='/Home/About']");

        /// <summary>
        /// Contant Navigation Link 'By' finder
        /// </summary>
        protected static By ContactLink = By.CssSelector("[href='/Home/Contact']");

        /// <summary>
        /// Footer 'By' Finder
        /// </summary>
        protected static By Footer = By.CssSelector("footer");

        /// <summary>
        /// Selenium Web Driver
        /// </summary>
        protected IWebDriver webDriver;

        /// <summary>
        /// Initializes a new instance of the <see cref="AbstractPageModel" /> class.
        /// </summary>
        /// <param name="webDriver">The selenium web driver</param>
        public AbstractPageModel(IWebDriver webDriver)
        {
            this.webDriver = webDriver;
        }

        /// <summary>
        /// Click the 'Home' navigation link
        /// </summary>
        public HomePageModel ToHomePage()
        {
            this.webDriver.FindElement(HomeLink).Click();
            return new HomePageModel(this.webDriver);
        }

        /// <summary>
        /// Click the 'About' navigation link
        /// </summary>
        public AboutPageModel ToAboutPage()
        {
            this.webDriver.FindElement(AboutLink).Click();
            return new AboutPageModel(this.webDriver);
        }

        /// <summary>
        /// Click the 'Contact' Navigation link
        /// </summary>
        public ContactPageModel ToContactPage()
        {
            this.webDriver.FindElement(ContactLink).Click();
            return new ContactPageModel(this.webDriver);
        }
    }
}
