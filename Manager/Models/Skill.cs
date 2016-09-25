using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Manager.Models
{
    public class Skill
    {
        [Key]
        public int ID { get; set; }

        [Required]
        public string Name { get; set; }
    }
}