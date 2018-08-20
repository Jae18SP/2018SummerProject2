using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApplication2.Models
{
    public class CustomerMaster
    {
        [Key]
        public int CustomerId { get; set; }

        [Required]
        public string CustomerName { get; set; }

        [Required]
        public string CustomerPhone { get; set; }

        [Required]
        public bool isActive { get; set; }

        [Required]
        public DateTime DOC { get; set; }

        [Required]
        public DateTime DOM { get; set; }
    }
}