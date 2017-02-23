using Magenic.MaqsFramework.BaseWebServiceTest;
using Magenic.MaqsFramework.Utilities.Helper;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Net;
using System.Net.Http;

namespace Tests
{
    /// <summary>
    /// Sample test class
    /// </summary>
    [TestClass]
    public class WebServiceValid : BaseWebServiceTest
    {
        /// <summary>
        /// Can we connect with valid credentials
        /// </summary>
        [TestMethod]
        public void ConnectWithCridentials()
        {
            var result = ConnectWithNTLMCreds("gcptester", "M@genicons", "magenicons");

            Assert.IsTrue(result.IsSuccessStatusCode, "Failed to connect to site");
        }

        /// <summary>
        /// Can we connect with valid credentials
        /// </summary>
        [TestMethod]
        [DataSource("Microsoft.VisualStudio.TestTools.DataSource.CSV",
    @"|DataDirectory|\DataDir\InvalidLogins.csv",
    "InvalidLogins#csv",
    DataAccessMethod.Sequential)]
        public void ConnectWithInvalidCridentials()
        {
            var result = ConnectWithNTLMCreds(TestContext.DataRow["UserName"].ToString(), TestContext.DataRow["Password"].ToString(), TestContext.DataRow["Domain"].ToString());

            Assert.IsFalse(result.IsSuccessStatusCode, "Connect to site with bad creds");
        }

        /// <summary>
        /// Try to connect
        /// </summary>
        /// <param name="user">The user</param>
        /// <param name="pass">The user's password</param>
        /// <param name="domain">The domain</param>
        /// <returns></returns>
        private HttpResponseMessage ConnectWithNTLMCreds(string user, string pass, string domain)
        {
            var credential = new NetworkCredential(user, pass, domain);
            var cache = new CredentialCache();

            // Setup auth
            cache.Add(new Uri(Config.GetValue("WebServiceUri")), "NTLM", credential);

            // Create auth handle
            var handler = new HttpClientHandler();
            handler.AllowAutoRedirect = true;
            handler.Credentials = cache;

            // Use auth with http client
            this.WebServiceWrapper.BaseHttpClient = new HttpClient(handler);

            return this.WebServiceWrapper.GetWithResponse("", "text/html", false);
        }

        /// <summary>
        /// Can we connect with invalid credentials
        /// </summary>
        [TestMethod]
        public void ConnectWithNoCridentials()
        {
            var result = this.WebServiceWrapper.GetWithResponse("", "text/html", false);

            Assert.IsFalse(result.IsSuccessStatusCode, "Should not have been able to connect to site");
        }

    }
}
