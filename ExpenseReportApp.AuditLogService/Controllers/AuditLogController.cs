using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using ExpenseReportApp.AuditLogService.DAL;
using ExpenseReportApp.AuditLogService.Models;

namespace ExpenseReportApp.AuditLogService.Controllers
{
    public class AuditLogController : ApiController
    {
        private AuditLogEntryContext db = new AuditLogEntryContext();

        // GET: api/AuditLog
        public IQueryable<AuditLogEntry> GetAuditLogEntries()
        {
            return db.AuditLogs;
        }

        // GET: api/AuditLog/5
        [ResponseType(typeof(AuditLogEntry))]
        public IHttpActionResult GetAuditLogEntry(int id)
        {
            AuditLogEntry auditLogEntry = db.AuditLogs.Find(id);
            if (auditLogEntry == null)
            {
                return NotFound();
            }

            return Ok(auditLogEntry);
        }

        // PUT: api/AuditLog/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutAuditLogEntry(int id, AuditLogEntry auditLogEntry)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != auditLogEntry.Id)
            {
                return BadRequest();
            }

            db.Entry(auditLogEntry).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AuditLogEntryExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/AuditLog
        [ResponseType(typeof(AuditLogEntry))]
        public IHttpActionResult PostAuditLogEntry(AuditLogEntry auditLogEntry)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.AuditLogs.Add(auditLogEntry);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = auditLogEntry.Id }, auditLogEntry);
        }

        // DELETE: api/AuditLog/5
        [ResponseType(typeof(AuditLogEntry))]
        public IHttpActionResult DeleteAuditLogEntry(int id)
        {
            AuditLogEntry auditLogEntry = db.AuditLogs.Find(id);
            if (auditLogEntry == null)
            {
                return NotFound();
            }

            db.AuditLogs.Remove(auditLogEntry);
            db.SaveChanges();

            return Ok(auditLogEntry);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool AuditLogEntryExists(int id)
        {
            return db.AuditLogs.Count(e => e.Id == id) > 0;
        }
    }
}