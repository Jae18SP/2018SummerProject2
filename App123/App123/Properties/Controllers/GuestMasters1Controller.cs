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
    public class GuestMasters1Controller : ApiController
    {
        private CrudContext db = new CrudContext();

        // GET: api/GuestMasters1
        public IQueryable<GuestMaster> GetGuestMaster()
        {
            return db.GuestMaster;
        }

        // GET: api/GuestMasters1/5
        [ResponseType(typeof(GuestMaster))]
        public IHttpActionResult GetGuestMaster(int id)
        {
            GuestMaster guestMaster = db.GuestMaster.Find(id);
            if (guestMaster == null)
            {
                return NotFound();
            }

            return Ok(guestMaster);
        }

        // PUT: api/GuestMasters1/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutGuestMaster(int id, GuestMaster guestMaster)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != guestMaster.GuestId)
            {
                return BadRequest();
            }

            db.Entry(guestMaster).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!GuestMasterExists(id))
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

        // POST: api/GuestMasters1
        [ResponseType(typeof(GuestMaster))]
        public IHttpActionResult PostGuestMaster(GuestMaster guestMaster)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.GuestMaster.Add(guestMaster);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = guestMaster.GuestId }, guestMaster);
        }

        // DELETE: api/GuestMasters1/5
        [ResponseType(typeof(GuestMaster))]
        public IHttpActionResult DeleteGuestMaster(int id)
        {
            GuestMaster guestMaster = db.GuestMaster.Find(id);
            if (guestMaster == null)
            {
                return NotFound();
            }

            db.GuestMaster.Remove(guestMaster);
            db.SaveChanges();

            return Ok(guestMaster);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool GuestMasterExists(int id)
        {
            return db.GuestMaster.Count(e => e.GuestId == id) > 0;
        }
    }
}