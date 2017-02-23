using Magenic.MaqsFramework.BaseSeleniumTest;
using Magenic.MaqsFramework.Utilities.Helper;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;

namespace Models.WebPage
{
    /// <summary>
    /// Page object for CreateReportPageModel
    /// </summary>
    public class CreateReportPageModel: AbstractPageModel
    {
        /// <summary>
        /// The page url
        /// </summary>
        private static string CreatePageUrl = PageUrl + "ExpenseReport/CreateExpenseReport";

        /// <summary>
        /// First name textbox 'By' finder
        /// </summary>
        private static By FirstName = By.CssSelector("#FirstName");

        /// <summary>
        /// Last name textbox 'By' finder
        /// </summary>
        private static By LastName = By.CssSelector("#LastName");

        /// <summary>
        /// Company name textbox 'By' finder
        /// </summary>
        private static By CompanyName = By.CssSelector("#CompanyName");

        /// <summary>
        /// Date picker 'By' finder
        /// </summary>
        private static By Date = By.CssSelector("#Date");

        /// <summary>
        /// Description textbox 'By' finder
        /// </summary>
        private static By Description = By.CssSelector("#Description");

        /// <summary>
        /// Date picker object 'By' finder
        /// </summary>
        private static By DatePicker = By.CssSelector("[class='datepicker-days']");

        /// <summary>
        /// Create button 'By' finder
        /// </summary>
        private static By CreateButton = By.CssSelector("[class='btn btn-primary expRepBtn']");

        /// <summary>
        /// Cancel Button 'By' finder
        /// </summary>
        private static By CancelButton = By.CssSelector("[class='btn btn-danger expRepBtn']");

        /// <summary>
        /// Current date and time for date picker
        /// </summary>
        public static DateTime Today = DateTime.Today;

        /// <summary>
        /// Initializes a new instance of the <see cref="CreateReportPageModel" /> class.
        /// </summary>
        /// <param name="webDriver">The selenium web driver</param>
        public CreateReportPageModel(IWebDriver webDriver) : base(webDriver)
        {

        }

        /// <summary>
        /// Open the page
        /// </summary>
        public void OpenPage()
        {
            // Open the gmail login page
            this.webDriver.Navigate().GoToUrl(CreatePageUrl);
            this.AssertPageLoaded();
        }
        /// <summary>
        /// Filling out an expense report 
        /// </summary>
        public void FillOutForm()
        {
            //Creates a list of all the dates to select from this month

            this.webDriver.FindElement(Date).Click();
            IWebElement Dates = webDriver.FindElement(DatePicker);

            IReadOnlyList<IWebElement> SelectDate = Dates.FindElements(By.TagName("td"));

            for (int i = 0; i <= SelectDate.Count; i++)
            {
                if (Convert.ToInt32(SelectDate[i].Text) == Today.Day)
                {
                    SelectDate[i].Click();
                    break;
                }
            }

            this.webDriver.FindElement(FirstName).SendKeys("John");
            this.webDriver.FindElement(LastName).SendKeys("Doe");
            this.webDriver.FindElement(CompanyName).SendKeys("Magenic");
            this.webDriver.FindElement(Description).SendKeys("Testing");

            this.webDriver.FindElement(CreateButton).Click();
        }

        /// <summary>
        /// Verify we are on the Create a Report Page
        /// </summary>
        public void AssertPageLoaded()
        {
            //Assert depends on what testing framework is being used
            Assert.IsTrue(
                this.webDriver.Url.Equals(CreatePageUrl, System.StringComparison.CurrentCultureIgnoreCase),
                "Expected to be on '{0}', but was on '{1}' instead", this.webDriver.Url);
        }
    }
}
