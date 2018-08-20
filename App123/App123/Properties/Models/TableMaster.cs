using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApplication2.Models
{
    public class TableMaster
    {
        [Key]
        public int TableId { get; set; } 

        [Required]
        public string TableType { get; set; }

        [Required]
        public int TabeSize { get; set; }
    }
}