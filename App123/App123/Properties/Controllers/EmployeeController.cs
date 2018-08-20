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
    public class EmployeeController : ApiController
    {
        private CrudContext db = new CrudContext();

        // GET: api/Employee 
        public DataTableResponse GetEmployees()
        {
            var Employees = db.Employees;
            return new DataTableResponse
            {
                recordsTotal = Employees.Count(),
                recordsFiltered = 10,
                data = Employees.ToArray()
            };
        } 

        //public ComboResponse GetEmployeesList(int id=0)
        //{
        //    try
        //    { 
        //        return new ComboResponse
        //        {
        //            data = (from empployee in db.Employees 
        //                   select new EmployeeMaster()
        //                   {
        //                       EmployeeId = empployee.EmployeeId,
        //                       EmployeeName = empployee.EmployeeName,
        //                   }).ToArray(),
        //            error = ""
        //        };
        //    }
        //    catch (Exception ex)
        //    {
        //        return new ComboResponse
        //        {
        //            error = "" + ex.Message
        //        };
        //    }
        //}
    

        // GET: api/Employee/5
        [ResponseType(typeof(EmployeeMaster))]
        public IHttpActionResult GetEmployeeMaster(int id)
        {
            EmployeeMaster employeeMaster = db.Employees.Find(id);
            if (employeeMaster == null)
            {
                return NotFound();
            }

            return Ok(employeeMaster);
        }

        // PUT: api/Employee/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutEmployeeMaster(int id, EmployeeMaster employeeMaster)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != employeeMaster.EmployeeId)
            {
                return BadRequest();
            }

            db.Entry(employeeMaster).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EmployeeMasterExists(id))
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

        // POST: api/Employee
        [ResponseType(typeof(EmployeeMaster))]
        public IHttpActionResult PostEmployeeMaster(EmployeeMaster employeeMaster)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Employees.Add(employeeMaster);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = employeeMaster.EmployeeId }, employeeMaster);
        }

        // DELETE: api/Employee/5
        [ResponseType(typeof(EmployeeMaster))]
        public IHttpActionResult DeleteEmployeeMaster(int id)
        {
            EmployeeMaster employeeMaster = db.Employees.Find(id);
            if (employeeMaster == null)
            {
                return NotFound();
            }

            db.Employees.Remove(employeeMaster);
            db.SaveChanges();

            return Ok(employeeMaster);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool EmployeeMasterExists(int id)
        {
            return db.Employees.Count(e => e.EmployeeId == id) > 0;
        }
    }
}