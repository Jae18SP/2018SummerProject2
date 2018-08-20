using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApplication2.Models
{
    public class EventMaster
    {
        [Key]
        public int EventId { get; set; }

        [Required]
        public string EventName { get; set; }

        [Required]
        public DateTime EventDate { get; set; }


        [Required]
        public int TableId { get; set; }

        [Required]
        public int EmployeeId { get; set; }

        [Required]
        public int CustomerId { get; set; }

        [Required]
        public DateTime DOC { get; set; }

        [Required]
        public DateTime DOM { get; set; }

    }
}