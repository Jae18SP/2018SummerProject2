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
using WebApplication2.Models;

namespace WebApplication2.Controllers
{
    public class EventMastersController : ApiController
    {
        private CrudContext db = new CrudContext();

        // GET: api/EventMasters
        public DataTableResponse GetEventMaster()
        {
            //return db.EventMaster;
            //var Employees = db.EventMaster;
            var query = db.EventMaster.Join 
            (
            db.Employees, r => r.EmployeeId, p => p.EmployeeId, (r, p) => new
            { 
                r.EventId,
                r.CustomerId,
                r.EventName,
                r.EventDate,
                r.TableId,
                p.EmployeeName  
            } 
            ) 
            .Join(db.Customers, a => a.CustomerId, b => b.CustomerId, (a, b) => new
            {
                b.CustomerName,
                a.EventId,
                a.EventName,
                a.EventDate,
                a.TableId,
                a.EmployeeName 
            });
            return new DataTableResponse
            {
                recordsTotal = query.Count(),
                recordsFiltered = 10,
                data = query.ToArray()
            };
        }

        // GET: api/EventMasters/5
        [ResponseType(typeof(EventMaster))]
        public IHttpActionResult GetEventMaster(int id)
        {
            var eventMaster  = db.EventMaster.Join
            (
            db.Employees, r => r.EmployeeId, p => p.EmployeeId, (r, p) => new
            {
                r.EventId,
                r.CustomerId,
                r.EventName,
                r.EventDate,
                r.TableId,
                p.EmployeeName
            }
            )
            .Join(db.Customers, a => a.CustomerId, b => b.CustomerId, (a, b) => new
            {
                b.CustomerName,
                a.EventId,
                a.EventName,
                a.EventDate,
                a.TableId,
                a.EmployeeName
            }).Where(a=>a.EventId==id);
            if (eventMaster == null)
            {
                return NotFound();
            }

            return Ok(eventMaster);
        }

        // PUT: api/EventMasters/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutEventMaster(int id, EventMaster eventMaster)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != eventMaster.EventId)
            {
                return BadRequest();
            }

            db.Entry(eventMaster).State = EntityState.Modified;

            try 
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EventMasterExists(id))
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

        // POST: api/EventMasters
        [ResponseType(typeof(EventMaster))]
        public IHttpActionResult PostEventMaster(EventMaster eventMaster)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.EventMaster.Add(eventMaster);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = eventMaster.EventId }, eventMaster);
        }

        // DELETE: api/EventMasters/5
        [ResponseType(typeof(EventMaster))]
        public IHttpActionResult DeleteEventMaster(int id)
        {
            EventMaster eventMaster = db.EventMaster.Find(id);
            if (eventMaster == null)
            {
                return NotFound();
            }

            db.EventMaster.Remove(eventMaster);
            db.SaveChanges();

            return Ok(eventMaster);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool EventMasterExists(int id)
        {
            return db.EventMaster.Count(e => e.EventId == id) > 0;
        }
    }
}