using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Manager.Models
{
    public class CalcStats
    {
        [Key]
        public int ID { get; set; }

        [Display(AutoGenerateField = false)]
        public int Level { get; set; }

        [Display(AutoGenerateField = false)]
        public int EXP { get; set; }

        [Display(AutoGenerateField = false)]
        public int Age { get; set; }

        public int HP { get; set; }

        public int MP { get; set; }

        [Display(Name = "HP Regen")]
        public string HP_Regen { get; set; }

        [Display(Name = "MP Regen")]
        public string MP_Regen { get; set; }

        public int Damage { get; set; }

        [Display(Name = "Lift / Move")]
        public string Lift_or_Move { get; set; }

        [Display(Name = "Damage Reduction")]
        public decimal Damage_Reduction { get; set; }

        public string Dodge { get; set; }

        public string Crit { get; set; }

        [Display(Name = "Hit Chance")]
        public string Hit_Chance { get; set; }

        [Display(Name = "Magic Resist")]
        public decimal Magic_Resist { get; set; }

        [Display(Name = "Chance to Convince")]
        public decimal Chance_to_Convince { get; set; }

        [Display(Name = "Lie is believed")]
        public decimal Lie_is_believed { get; set; }

        [Display(Name = "Chance for loot")]
        public decimal Chance_for_loot { get; set; }

        [Display(Name = "Better loot quality")]
        public decimal Better_loot_quality { get; set; }

        [Display(Name = "Avoid overpowered enemies")]
        public decimal Avoid_overpowered_enemies { get; set; }

        [Display(Name = "Event goes well")]
        public decimal Event_goes_well { get; set; }
    }
}