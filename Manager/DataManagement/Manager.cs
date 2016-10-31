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
            if (toInitialize.HasFlag(ManagerClasses.Player)) Player = new PlayerManager();
            if (toInitialize.HasFlag(ManagerClasses.Story)) Story = new StoryManager();
            if (toInitialize.HasFlag(ManagerClasses.User)) User = new UserManager();
        }

        public PlayerManager Player { get; set; }

        public StoryManager Story { get; set; }

        public UserManager User { get; set; }
    }

    public enum ManagerClasses { Player = 2, Story = 32, User = 64 }
}