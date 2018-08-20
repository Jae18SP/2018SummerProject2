using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using WebApplication2.Models;

namespace WebApplication2.Controllers
{
    public class HomeController : ApiController
    {
        private CrudContext db = new CrudContext();
        [HttpGet]
        public int Index(string Email,string Password)
        {
            var display = db.Employees.Where(m => m.EmployeeEmail == Email && m.EmployeePassword == Password).FirstOrDefault();
            if (display != null)
            {
                return 1;
            }
            else
            {
                return 2;
            }
        }
    }
}
