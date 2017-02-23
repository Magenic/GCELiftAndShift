using Magenic.MaqsFramework.BaseSeleniumTest;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;

namespace Models.WebPage
{
    /// <summary>
    /// Page object for the Automation page
    /// </summary>
    public class HomePageModel : AbstractPageModel
    {
       
        /// <summary>
        /// Expense Report button 'By' finder
        /// </summary>
        private static By ExpenseReport = By.CssSelector("[class='home-page-btn glyphicon glyphicon-pencil']");

        /// <summary>
        /// My Reports button 'By' finder
        /// </summary>
        private static By MyReports = By.CssSelector("[class='home-page-btn glyphicon glyphicon-list']");
        
        /// <summary>
        /// Initializes a new instance of the <see cref="LoginPageModel" /> class.
        /// </summary>
        /// <param name="webDriver">The selenium web driver</param>
        public HomePageModel(IWebDriver webDriver) : base(webDriver)
        {

        }

        /// <summary>
        /// Open the login page
        /// </summary>
        public void OpenPage()
        {
            // Make sure we can login
            this.webDriver.Navigate().GoToUrl(LoginUrl);

            this.webDriver.WaitForPageLoad();

            // Open the gmail login page
            this.webDriver.Navigate().GoToUrl(PageUrl);
           
        }

        /// <summary>
        /// Navigate to Create expense report page
        /// </summary>
        /// <returns>CreateReportPageModel</returns>
        public CreateReportPageModel ToCreateReportPage()
        {
            this.webDriver.FindElement(ExpenseReport).Click();
            return new CreateReportPageModel(this.webDriver);
        }

        /// <summary>
        /// Navigate to Veiew expense report page
        /// </summary>
        /// <returns>CreateReportPageModel</returns>
        public ViewReportsPageModel ToViewReportPage()
        {
            this.webDriver.FindElement(MyReports).Click();
            return new ViewReportsPageModel(this.webDriver);
        }


        /// <summary>
        /// Verify we are on the login page
        /// </summary>
        public void AssertPageLoaded()
        {
            //Assert depends on what testing framework is being used
            Assert.IsTrue(this.IsOnPage(), "Home page was not loaded");
        }

        /// <summary>
        /// Check if the page is loaded
        /// </summary>
        /// <returns>True if the page is loaded</returns>
        public bool IsOnPage()
        {
            return this.webDriver.WaitUntilClickableElement(ExpenseReport);
        }
    }
}

