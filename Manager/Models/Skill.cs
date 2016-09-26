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

        [Required]
        public string Description { get; set; }

        [Required]
        public SkillType Type { get; set; }

        [Required, Display(Name = "Active Description Formula")]
        public string ActiveDescriptionFormula { get; set; }

        [Required, Display(Name = "Passive Description Formula")]
        public string PassiveDescriptionFormula { get; set; }

        [Required, Display(Name = "Active Formula")]
        public string ActiveFormula { get; set; }

        [Required, Display(Name = "Active Cost Formula")]
        public string ActiveCostFormula { get; set; }

        [Required, Display(Name = "Passive Formula")]
        public string PassiveFormula { get; set; }
    }
}