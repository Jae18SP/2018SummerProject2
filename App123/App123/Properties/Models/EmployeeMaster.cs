using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApplication2.Models
{
    public class EmployeeMaster
    {
        [Key]
        public int EmployeeId { get; set; }

        [Required]
        public string EmployeeName { get; set; }

        [Required]
        public string EmployeeEmail { get; set; }

        [Required]
        public string EmployeePassword { get; set; }

        [Required]
        public bool  isActive { get; set; }

        [Required]
        public DateTime DOC { get; set; }

        [Required]
        public DateTime DOM { get; set; }
    }
}