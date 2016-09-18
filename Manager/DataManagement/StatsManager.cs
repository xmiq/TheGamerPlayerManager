using Manager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Manager.DataManagement
{
    public class StatsManager
    {
        private DataManager mgr = new DataManager();

        public Stats GetStats(int Chapter)
        {
            var chapterID = mgr.GetParameter();
            chapterID.ParameterName = "@Chapter";
            chapterID.Value = Chapter;

            var toReturn = mgr.GetData("Player.usp_GetStats", chapterID)
                .Select(x => new Stats
                {
                    ID = Convert.ToInt32(x["ID"]),
                    Chapter = new Chapter { ID = Chapter },
                    Level = Convert.ToInt32(x["Level"]),
                    EXP = Convert.ToInt32(x["EXP"]),
                    Age = Convert.ToInt32(x["Age"]),
                    Strength = Convert.ToInt32(x["Strength"]),
                    Vitality = Convert.ToInt32(x["Vitality"]),
                    Constitution = Convert.ToInt32(x["Constitution"]),
                    Dexterity = Convert.ToInt32(x["Dexterity"]),
                    Accuracy = Convert.ToInt32(x["Accuracy"]),
                    Inteligence = Convert.ToInt32(x["Inteligence"]),
                    Wisdom = Convert.ToInt32(x["Wisdom"]),
                    Charisma = Convert.ToInt32(x["Charisma"]),
                    Luck = Convert.ToInt32(x["Luck"])
                })
                .FirstOrDefault();

            var ID = mgr.GetParameter();
            ID.ParameterName = "@ID";
            ID.Value = toReturn.ID;

            toReturn.CalculatedPartialModel = mgr.GetData("Player.usp_GetCalculatedStats", ID)
                .Select(x => new CalcStats
                {
                    ID = toReturn.ID,
                    Level = Convert.ToInt32(x["Level"]),
                    EXP = Convert.ToInt32(x["EXP"]),
                    Age = Convert.ToInt32(x["Age"]),
                    HP = Convert.ToInt32(x["HP"]),
                    MP = Convert.ToInt32(x["MP"]),
                    HP_Regen = x["HP Regen"].ToString(),
                    MP_Regen = x["MP Regen"].ToString(),
                    Damage = Convert.ToInt32(x["Damage"]),
                    Lift_or_Move = x["Lift / Move"].ToString(),
                    Damage_Reduction = Convert.ToDecimal(x["Damage Reduction"]),
                    Dodge = x["Dodge"].ToString(),
                    Crit = x["Crit"].ToString(),
                    Hit_Chance = x["Hit Chance"].ToString(),
                    Magic_Resist = Convert.ToDecimal(x["Magic Resist"]),
                    Chance_to_Convince = Convert.ToDecimal(x["Chance to Convince"]),
                    Lie_is_believed = Convert.ToDecimal(x["Lie is believed"]),
                    Chance_for_loot = Convert.ToDecimal(x["Chance for loot"]),
                    Better_loot_quality = Convert.ToDecimal(x["Better loot quality"]),
                    Avoid_overpowered_enemies = Convert.ToDecimal(x["Avoid overpowered enemies"]),
                    Event_goes_well = Convert.ToDecimal(x["Event goes well"])
                })
                .FirstOrDefault();

            return toReturn;
        }

        public void UpdateStats(Stats s)
        {
            var id = mgr.GetParameter();
            id.ParameterName = "@ID";
            id.Value = s.ID;

            var chapter = mgr.GetParameter();
            chapter.ParameterName = "@Chapter";
            chapter.Value = s.Chapter.ID;

            var level = mgr.GetParameter();
            level.ParameterName = "@Level";
            level.Value = s.Level;

            var exp = mgr.GetParameter();
            exp.ParameterName = "@EXP";
            exp.Value = s.EXP;

            var age = mgr.GetParameter();
            age.ParameterName = "@Age";
            age.Value = s.Age;

            var strength = mgr.GetParameter();
            strength.ParameterName = "@Strength";
            strength.Value = s.Strength;

            var vitality = mgr.GetParameter();
            vitality.ParameterName = "@Vitality";
            vitality.Value = s.Vitality;

            var constitution = mgr.GetParameter();
            constitution.ParameterName = "@Constitution";
            constitution.Value = s.Constitution;

            var dexterity = mgr.GetParameter();
            dexterity.ParameterName = "@Dexterity";
            dexterity.Value = s.Dexterity;

            var accuracy = mgr.GetParameter();
            accuracy.ParameterName = "@Accuracy";
            accuracy.Value = s.Accuracy;

            var inteligence = mgr.GetParameter();
            inteligence.ParameterName = "@Inteligence";
            inteligence.Value = s.Inteligence;

            var wisdom = mgr.GetParameter();
            wisdom.ParameterName = "@Wisdom";
            wisdom.Value = s.Wisdom;

            var charisma = mgr.GetParameter();
            charisma.ParameterName = "@Charisma";
            charisma.Value = s.Charisma;

            var luck = mgr.GetParameter();
            luck.ParameterName = "@Luck";
            luck.Value = s.Luck;

            mgr.Execute("Player.usp_UpdateStats", id, chapter, level, exp, age, strength, vitality, constitution, dexterity, accuracy, inteligence, wisdom, charisma, luck);
        }

        public void AddXP(int ID, int XP)
        {
            var id = mgr.GetParameter();
            id.ParameterName = "@ID";
            id.Value = ID;

            var xp = mgr.GetParameter();
            xp.ParameterName = "@XPtoAdd";
            xp.Value = XP;

            mgr.Execute("Player.usp_AddPlayerXP", id, xp);
        }
    }
}