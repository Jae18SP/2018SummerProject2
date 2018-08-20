using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Http.Description;
using WebApplication2.Models;

namespace WebApplication2.Controllers
{
    public class CustomerController : ApiController
    {
        private CrudContext db = new CrudContext();

        // GET: api/Customer
        public DataTableResponse GetCustomers()
        {
            //return (a+b).ToString();
            var customer = db.Customers.Where(cs => cs.isActive == true);
            return new DataTableResponse
            {
                recordsTotal = customer.Count(),

                recordsFiltered = 10,

                data = customer.ToArray()
            };
        }

        // GET: api/Customer/5
        [ResponseType(typeof(CustomerMaster))]
        public IHttpActionResult GetCustomerMaster(int id)
        {
            CustomerMaster customerMaster = db.Customers.Find(id);
            if (customerMaster == null)
            {
                return NotFound();
            }

            return Ok(customerMaster);
        }

        // PUT: api/Customer/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutCustomerMaster(int id, CustomerMaster customerMaster)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != customerMaster.CustomerId)
            {
                return BadRequest();
            }

            db.Entry(customerMaster).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CustomerMasterExists(id))
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

        // POST: api/Customer
        [ResponseType(typeof(CustomerMaster))]
        public IHttpActionResult PostCustomerMaster(CustomerMaster customerMaster)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Customers.Add(customerMaster);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = customerMaster.CustomerId }, customerMaster);
        }

        // DELETE: api/Customer/5 
        [ResponseType(typeof(CustomerMaster))]
        public IHttpActionResult DeleteCustomerMaster(int id)
        {
            CustomerMaster customerMaster = db.Customers.Find(id);
            if (customerMaster == null)
            {
                return NotFound();
            }

            db.Customers.Remove(customerMaster);
            db.SaveChanges();

            return Ok(customerMaster);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool CustomerMasterExists(int id)
        {
            return db.Customers.Count(e => e.CustomerId == id) > 0;
        }

        //[AcceptVerbs("OPTIONS")]
        //public HttpResponseMessage Options()
        //{
        //    var resp = new HttpResponseMessage(HttpStatusCode.OK);
        //    resp.Headers.Add("Access-Control-Allow-Origin", "*");
        //    resp.Headers.Add("Access-Control-Allow-Methods", "GET,DELETE");

        //    return resp;
        //}
    }
}