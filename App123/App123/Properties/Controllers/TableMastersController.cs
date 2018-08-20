using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using WebApplication2.Models;
using Newtonsoft.Json;

namespace WebApplication2.Controllers
{
    public class TableMastersController : ApiController
    {
        private CrudContext db = new CrudContext();

        // GET: api/TableMasters
        public IQueryable<TableMaster> GetTableMaster()
        {
            return db.TableMaster;
        }

        // GET: api/TableMasters/5
        [ResponseType(typeof(TableMaster))]
        public IHttpActionResult GetTableMaster(int id)
        {
            TableMaster tableMaster = db.TableMaster.Find(id);
            if (tableMaster == null)
            {
                return NotFound();
            }

            return Ok(tableMaster);
        }

        // PUT: api/TableMasters/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutTableMaster(int id, TableMaster tableMaster)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != tableMaster.TableId)
            {
                return BadRequest();
            }

            db.Entry(tableMaster).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TableMasterExists(id))
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

        // POST: api/TableMasters
        [ResponseType(typeof(TableMaster))]
        public IHttpActionResult PostTableMaster(TableMaster tableMaster)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.TableMaster.Add(tableMaster);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = tableMaster.TableId }, tableMaster);
        }

        // DELETE: api/TableMasters/5
        [ResponseType(typeof(TableMaster))]
        public IHttpActionResult DeleteTableMaster(int id)
        {
            TableMaster tableMaster = db.TableMaster.Find(id);
            if (tableMaster == null)
            {
                return NotFound();
            }

            db.TableMaster.Remove(tableMaster);
            db.SaveChanges();

            return Ok(tableMaster);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool TableMasterExists(int id)
        {
            return db.TableMaster.Count(e => e.TableId == id) > 0;
        }

        
    }
}