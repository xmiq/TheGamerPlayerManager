using Manager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Manager.DataManagement
{
    public class StoryManager
    {
        private DataManager mgr = new DataManager();

        public List<Story> GetAllStories()
        {
            return mgr.GetData("Player.usp_GetAllStories")
                .Select(x => new Story
                {
                    ID = Convert.ToInt32(x["ID"]),
                    Name = x["Name"].ToString(),
                    User = new User { Username = x["User"].ToString() }
                })
                .ToList();
        }

        public List<Story> GetAllStories(string Username)
        {
            var username = mgr.GetParameter();
            username.ParameterName = "@Username";
            username.Value = Username;

            return mgr.GetData("Player.usp_GetAllMyStories", username)
                .Select(x => new Story
                {
                    ID = Convert.ToInt32(x["ID"]),
                    Name = x["Name"].ToString(),
                    User = new User { Username = Username }
                })
                .ToList();
        }

        public Story GetStory(int ID)
        {
            var id = mgr.GetParameter();
            id.ParameterName = "@ID";
            id.Value = ID;

            return mgr.GetData("Player.usp_GetStory", id)
                .Select(x => new Story
                {
                    ID = Convert.ToInt32(x["ID"]),
                    Name = x["Name"].ToString(),
                    User = new User { Username = x["User"].ToString() }
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

        public void DeleteSkill(int ID)
        {
            var id = mgr.GetParameter();
            id.ParameterName = "@ID";
            id.Value = ID;

            mgr.Execute("Player.usp_DeleteSkill", id);
        }
    }
}