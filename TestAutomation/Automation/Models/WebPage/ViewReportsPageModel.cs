using Magenic.MaqsFramework.BaseSeleniumTest;
using Magenic.MaqsFramework.Utilities.Helper;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;

namespace Models.WebPage
{
    /// <summary>
    /// Page object for ViewReportPageModel
    /// </summary>
    public class ViewReportsPageModel: AbstractPageModel
    {
        /// <summary>
        /// The page url
        /// </summary>
        protected static string ViewPageUrl = PageUrl + "ExpenseReport/ViewExpenseReportList?user=MAGENICONS%5Cgcptester";

        /// <summary>
        /// Url of the chosen report; default set to view page meaning no selected report
        /// </summary>
        protected static string newReport = ViewPageUrl;
        
        /// <summary>
        /// Report with ID 3'By' finder
        /// </summary>
        protected static By ReportId3 = By.CssSelector("[href='/ExpenseReport/ViewExpenseReport?expReportId=3']");

        /// <summary>
        /// Update ID 3 'By' finder
        /// </summary>
        protected static By UpdateId3 = By.CssSelector("[href='/ExpenseReport/Update?expReportId=3']");

        /// <summary>
        /// Table containing reports 'By' finder
        /// </summary>
        protected static By ReportsTable = By.CssSelector("[class='table table-bordered']");

        /// <summary>
        /// Initializes a new instance of the <see cref="ViewReportsPageModel" /> class.
        /// </summary>
        /// <param name="webDriver">The selenium web driver</param>
        public ViewReportsPageModel(IWebDriver webDriver) : base(webDriver)
        {

        }

        /// <summary>
        /// Open the page
        /// </summary>
        public virtual void OpenPage()
        {
            // Open the gmail login page
            this.webDriver.Navigate().GoToUrl(ViewPageUrl);
            this.AssertPageLoaded();

        }

        /// <summary>
        /// Update selected report
        /// </summary>
        public void updateReport()
        {
            this.webDriver.FindElement(UpdateId3).Click();

        }


        /// <summary>
        /// Choose a report to view; currently defaulted to only 1 report
        /// </summary>
        /// <returns></returns>
        public ExpenseReportPageModel ViewReport()
        {
            this.webDriver.FindElement(ReportId3).Click();
            newReport = this.webDriver.Url;
            return new ExpenseReportPageModel(this.webDriver);
        }

        

       
        /// <summary>
        /// Verify we are on the login page
        /// </summary>
        public virtual void AssertPageLoaded()
        {
            //Assert depends on what testing framework is being used
            Assert.IsTrue(
                this.webDriver.Url.Equals(ViewPageUrl, System.StringComparison.CurrentCultureIgnoreCase),
                "Expected to be on '{0}', but was on '{1}' instead", this.webDriver.Url);
        }
    }
}
