using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Manager.Models
{
    public class Player
    {
        [Key]
        public int ID { get; set; }

        [Required, MaxLength(25)]
        public string Name { get; set; }

        [Required, MaxLength(25)]
        public string Surname { get; set; }

        [Required, MaxLength(100)]
        public string Story { get; set; }
    }
}