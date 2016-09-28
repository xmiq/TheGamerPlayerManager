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

        public void CreateSkill(Skill s)
        {
            var name = mgr.GetParameter();
            name.ParameterName = "@Name";
            name.Value = s.Name;

            var description = mgr.GetParameter();
            description.ParameterName = "@Description";
            description.Value = s.Description;

            var type = mgr.GetParameter();
            type.ParameterName = "@Type";
            type.Value = (s.ActiveDescriptionFormula != null && s.PassiveDescriptionFormula == null) ? SkillType.Active : (s.ActiveDescriptionFormula == null && s.PassiveDescriptionFormula != null) ? SkillType.Passive : SkillType.Active | SkillType.Passive;

            var activeDescriptionFormula = mgr.GetParameter();
            activeDescriptionFormula.ParameterName = "@ActiveDescriptionFormula";
            activeDescriptionFormula.Value = s.ActiveDescriptionFormula;

            var passiveDescriptionFormula = mgr.GetParameter();
            passiveDescriptionFormula.ParameterName = "@PassiveDescriptionFormula";
            passiveDescriptionFormula.Value = s.PassiveDescriptionFormula;

            var activeFormula = mgr.GetParameter();
            activeFormula.ParameterName = "@ActiveFormula";
            activeFormula.Value = s.ActiveFormula;

            var activeCostFormula = mgr.GetParameter();
            activeCostFormula.ParameterName = "@ActiveCostFormula";
            activeCostFormula.Value = s.ActiveCostFormula;

            var passiveFormula = mgr.GetParameter();
            passiveFormula.ParameterName = "@PassiveFormula";
            passiveFormula.Value = s.PassiveFormula;

            mgr.Execute("Player.usp_CreateSkill", name, description, type, activeDescriptionFormula, passiveDescriptionFormula, activeFormula, activeCostFormula, passiveFormula);
        }

        public void UpdateSkill(Skill s)
        {
            var id = mgr.GetParameter();
            id.ParameterName = "@ID";
            id.Value = s.ID;

            var name = mgr.GetParameter();
            name.ParameterName = "@Name";
            name.Value = s.Name;

            var description = mgr.GetParameter();
            description.ParameterName = "@Description";
            description.Value = s.Description;

            var type = mgr.GetParameter();
            type.ParameterName = "@Type";
            type.Value = (s.ActiveDescriptionFormula != null && s.PassiveDescriptionFormula == null) ? SkillType.Active : (s.ActiveDescriptionFormula == null && s.PassiveDescriptionFormula != null) ? SkillType.Passive : SkillType.Active | SkillType.Passive;

            var activeDescriptionFormula = mgr.GetParameter();
            activeDescriptionFormula.ParameterName = "@ActiveDescriptionFormula";
            activeDescriptionFormula.Value = s.ActiveDescriptionFormula;

            var passiveDescriptionFormula = mgr.GetParameter();
            passiveDescriptionFormula.ParameterName = "@PassiveDescriptionFormula";
            passiveDescriptionFormula.Value = s.PassiveDescriptionFormula;

            var activeFormula = mgr.GetParameter();
            activeFormula.ParameterName = "@ActiveFormula";
            activeFormula.Value = s.ActiveFormula;

            var activeCostFormula = mgr.GetParameter();
            activeCostFormula.ParameterName = "@ActiveCostFormula";
            activeCostFormula.Value = s.ActiveCostFormula;

            var passiveFormula = mgr.GetParameter();
            passiveFormula.ParameterName = "@PassiveFormula";
            passiveFormula.Value = s.PassiveFormula;

            mgr.Execute("Player.usp_UpdateSkill", id, name, description, type, activeDescriptionFormula, passiveDescriptionFormula, activeFormula, activeCostFormula, passiveFormula);
        }
    }
}