using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Manager.Models
{
    public class Stats
    {
        [Key]
        public int ID { get; set; }

        [Required]
        public Chapter Chapter { get; set; }

        [Required]
        public int Level { get; set; }

        [Required]
        public int EXP { get; set; }

        [Required]
        public int Age { get; set; }

        [Required]
        public int Strength { get; set; }

        [Required]
        public int Vitality { get; set; }

        [Required]
        public int Constitution { get; set; }

        [Required]
        public int Dexterity { get; set; }

        [Required]
        public int Accuracy { get; set; }

        [Required]
        public int Inteligence { get; set; }

        [Required]
        public int Wisdom { get; set; }

        [Required]
        public int Charisma { get; set; }

        [Required]
        public int Luck { get; set; }

        [Display(AutoGenerateField = false)]
        public CalcStats CalculatedPartialModel { get; set; }
    }
}