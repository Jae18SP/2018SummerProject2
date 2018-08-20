using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApplication2.Models
{
    public class GuestMaster
    {
        [Key]
        public int GuestId { get; set; }

        [Required]
        public string GuestName { get; set; }

        [Required]
        public int EventId{ get; set; }
    }
}