using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApplication2.Models
{
    public class GuestMapping
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int GuestId1 { get; set; }

        [Required]
        public int GuestId2 { get; set; }

        [Required]
        public int EventId { get; set; }
    }
}