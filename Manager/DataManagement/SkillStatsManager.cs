using Manager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Manager.DataManagement
{
    public class SkillStatsManager
    {
        private DataManager mgr = new DataManager();

        public List<SkillStats> GetSkillStats(int chapterID, int playerID, int opponentLevel = 1)
        {
            var chapter = mgr.GetParameter();
            chapter.ParameterName = "@Chapter";
            chapter.Value = chapterID;

            var player = mgr.GetParameter();
            player.ParameterName = "@Player";
            player.Value = playerID;

            var otherCharLevel = mgr.GetParameter();
            otherCharLevel.ParameterName = "@OtherCharLevel";
            otherCharLevel.Value = opponentLevel;

            return mgr.GetData("Player.usp_GetSkills", chapter, player, otherCharLevel)
                .Select(x => new SkillStats
                {
                    ID = Convert.ToInt32(x["ID"]),
                    Name = x["Name"].ToString(),
                    Description = x["Description"].ToString(),
                    Type = (SkillType)Enum.Parse(typeof(SkillType), x["Type"].ToString()),
                    ActiveDescription = x["Active Description"].ToString(),
                    PassiveDescription = x["Passive Description"].ToString(),
                    Level = Convert.ToInt32(x["Level"]),
                    EXP = Convert.ToInt32(x["EXP"])
                })
                .ToList();
        }

        public SkillStats GetSkillStat(int ID)
        {
            var skillStatID = mgr.GetParameter();
            skillStatID.ParameterName = "@SkillStatID";
            skillStatID.Value = ID;

            return mgr.GetData("Player.usp_GetSkillStat", skillStatID)
                .Select(x => new SkillStats
                {
                    ID = ID,
                    SkillID = new Skill { ID = Convert.ToInt32(x["SkillID"]) },
                    Level = Convert.ToInt32(x["Level"]),
                    Chapter = new Chapter { ID = Convert.ToInt32(x["Chapter"]) },
                    EXP = Convert.ToInt32(x["EXP"])
                })
                .FirstOrDefault();
        }

        public void CreateSkillStat(SkillStats ss)
        {
            var skillID = mgr.GetParameter();
            skillID.ParameterName = "@SkillID";
            skillID.Value = ss.SkillID.ID;

            var level = mgr.GetParameter();
            level.ParameterName = "@Level";
            level.Value = ss.Level;

            var chapter = mgr.GetParameter();
            chapter.ParameterName = "@Chapter";
            chapter.Value = ss.Chapter.ID;

            var player = mgr.GetParameter();
            player.ParameterName = "@Player";
            player.Value = ss.Player.ID;

            var exp = mgr.GetParameter();
            exp.ParameterName = "@EXP";
            exp.Value = ss.EXP;

            mgr.Execute("Player.usp_CreateSkillStat", skillID, level, chapter, player, exp);
        }

        public void UpdateSkillStat(SkillStats ss)
        {
            var id = mgr.GetParameter();
            id.ParameterName = "@ID";
            id.Value = ss.ID;

            var level = mgr.GetParameter();
            level.ParameterName = "@Level";
            level.Value = ss.Level;

            var exp = mgr.GetParameter();
            exp.ParameterName = "@EXP";
            exp.Value = ss.EXP;

            mgr.Execute("Player.usp_UpdateSkillStat", id, level, exp);
        }

        public void DeleteSkillStat(SkillStats ss)
        {
            var id = mgr.GetParameter();
            id.ParameterName = "@ID";
            id.Value = ss.ID;

            mgr.Execute("Player.usp_DeleteSkillStat", id);
        }

        public void AddXP(int ID, int XP)
        {
            var id = mgr.GetParameter();
            id.ParameterName = "@ID";
            id.Value = ID;

            var xp = mgr.GetParameter();
            xp.ParameterName = "@XPtoAdd";
            xp.Value = XP;

            mgr.Execute("Player.usp_AddSkillXP", id, xp);
        }
    }
}