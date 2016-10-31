using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Manager.DataManagement
{
    public class Manager
    {
        public Manager(ManagerClasses toInitialize)
        {
            if (toInitialize.HasFlag(ManagerClasses.Chapter)) Chapter = new ChapterManager();
            if (toInitialize.HasFlag(ManagerClasses.Player)) Player = new PlayerManager();
            if (toInitialize.HasFlag(ManagerClasses.Skill)) Skill = new SkillManager();
            if (toInitialize.HasFlag(ManagerClasses.SkillStats)) SkillStats = new SkillStatsManager();
            if (toInitialize.HasFlag(ManagerClasses.Stats)) Stats = new StatsManager();
            if (toInitialize.HasFlag(ManagerClasses.Story)) Story = new StoryManager();
            if (toInitialize.HasFlag(ManagerClasses.User)) User = new UserManager();
        }

        public ChapterManager Chapter { get; set; }

        public PlayerManager Player { get; set; }

        public SkillManager Skill { get; set; }

        public SkillStatsManager SkillStats { get; set; }

        public StatsManager Stats { get; set; }

        public StoryManager Story { get; set; }

        public UserManager User { get; set; }
    }

    public enum ManagerClasses { Chapter = 1, Player = 2, Skill = 4, SkillStats = 8, Stats = 16, Story = 32, User = 64 }
}