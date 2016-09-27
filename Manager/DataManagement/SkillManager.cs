using Manager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Manager.DataManagement
{
    public class SkillManager
    {
        private DataManager mgr = new DataManager();

        public List<Skill> GetAllSkills()
        {
            return mgr.GetData("Player.usp_GetAllSkills")
                .Select(x => new Skill
                {
                    ID = Convert.ToInt32(x["ID"]),
                    Name = x["Name"].ToString(),
                    Description = x["Description"].ToString()
                })
                .ToList();
        }

        public Skill GetSkill(int ID)
        {
            var id = mgr.GetParameter();
            id.ParameterName = "@ID";
            id.Value = ID;

            return mgr.GetData("Player.usp_GetSkill", id)
                .Select(x => new Skill
                {
                    ID = Convert.ToInt32(x["ID"]),
                    Name = x["Name"].ToString(),
                    Description = x["Description"].ToString(),
                    Type = (SkillType)Convert.ToInt32(x["Type"]),
                    ActiveDescriptionFormula = x["Active Description Formula"].ToString(),
                    PassiveDescriptionFormula = x["Passive Description Formula"].ToString(),
                    ActiveFormula = x["Active Formula"].ToString(),
                    ActiveCostFormula = x["Active Cost Formula"].ToString(),
                    PassiveFormula = x["Passive Formula"].ToString()
                })
                .FirstOrDefault();
        }
    }
}