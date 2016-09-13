using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Manager.Models
{
    public class Chapter
    {
        [Key]
        public int ID { get; set; }

        [Required]
        public Player Player { get; set; }

        [Required]
        public int Number { get; set; }
    }
}