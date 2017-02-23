using Magenic.MaqsFramework.BaseSeleniumTest;
using Magenic.MaqsFramework.Utilities.Helper;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;

namespace Models.WebPage
{
    /// <summary>
    /// Page object for ExpenseReportPageModel
    /// </summary>
    public class ExpenseReportPageModel : ViewReportsPageModel
    {
        /// <summary>
        /// Report 'By' finder
        /// </summary>
        private static By Report = By.CssSelector("[class='panel panel-primary']");

        /// <summary>
        /// Initializes a new instance of the <see cref="ExpenseReportPageModel" /> class.
        /// </summary>
        /// <param name="webDriver">The selenium web driver</param>
        public ExpenseReportPageModel(IWebDriver webDriver) : base(webDriver)
        {

        }

        /// <summary>
        /// Open the page
        /// </summary>
        override public void OpenPage()
        {
            //Calling ViewReport method to choose which report we will be viewing  
            //Method also sets the URL for newReport
            ViewReportsPageModel chooseReport = new ViewReportsPageModel(this.webDriver);
            ExpenseReportPageModel currentReport = chooseReport.ViewReport();

            // Open the URL of the chosen report 
            this.webDriver.Navigate().GoToUrl(newReport);
            this.AssertPageLoaded();
        }

        /// <summary>
        /// Verify we are on the login page
        /// </summary>
        public override void AssertPageLoaded()
        {
            //Assert depends on what testing framework is being used
            Assert.IsTrue(
                this.webDriver.Url.Equals(newReport, System.StringComparison.CurrentCultureIgnoreCase),
                "Expected to be on '{0}', but was on '{1}' instead", this.webDriver.Url);
        }
    }
}
