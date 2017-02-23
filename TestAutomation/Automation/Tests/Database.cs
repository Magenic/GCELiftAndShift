using Magenic.MaqsFramework.BaseDatabaseTest;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Data;

namespace Tests
{
    /// <summary>
    /// Sample test class
    /// </summary>
    [TestClass]
    public class Database : BaseDatabaseTest
    {
        /// <summary>
        /// Sample test
        /// </summary>
        [TestMethod]
        public void IsDatabaseUp()
        {
            DataTable table = this.DatabaseWrapper.QueryAndGetDataTable("SELECT * FROM information_schema.tables");
            Assert.IsTrue(table.Rows.Count > 0 , "Expected tables");
        }
    }
}
