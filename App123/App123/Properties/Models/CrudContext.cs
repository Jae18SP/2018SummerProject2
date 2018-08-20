using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace WebApplication2.Models
{
    public class CrudContext : DbContext
    {
        public CrudContext() : base("CrudContextDemo")
        {

        }

        public DbSet<EmployeeMaster> Employees { get; set; }

        public DbSet<CustomerMaster> Customers { get; set; }

        public DbSet<EventMaster> EventMaster { get; set; }

        public DbSet<TableMaster> TableMaster { get; set; }

        public DbSet<GuestMaster> GuestMaster { get; set; } 

        public DbSet<GuestMapping> GuestMapping { get; set; }
    }
}