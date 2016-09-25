using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Manager.Models
{
    public class SkillStats
    {
        [Key]
        public int ID { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public SkillType Type { get; set; }

        [Display(Name = "Active Description")]
        public string ActiveDescription { get; set; }

        [Display(Name = "Passive Description")]
        public string PassiveDescription { get; set; }

        public Skill SkillID { get; set; }

        public int Level { get; set; }

        public Chapter Chapter { get; set; }

        public int EXP { get; set; }
    }
}