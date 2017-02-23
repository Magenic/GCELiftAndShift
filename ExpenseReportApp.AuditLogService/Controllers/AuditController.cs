using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ExpenseReportApp.AuditLogService.Controllers
{
    public class AuditController : ApiController
    {
        // GET: api/Audit
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Audit/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Audit
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/Audit/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Audit/5
        public void Delete(int id)
        {
        }
    }
}
