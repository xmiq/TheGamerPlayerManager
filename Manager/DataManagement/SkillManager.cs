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
    }
}