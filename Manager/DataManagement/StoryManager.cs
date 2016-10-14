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

        public void CreateStory(Story s)
        {
            var name = mgr.GetParameter();
            name.ParameterName = "@Name";
            name.Value = s.Name;

            var user = mgr.GetParameter();
            user.ParameterName = "@User";
            user.Value = s.User;

            mgr.Execute("Player.usp_CreateSkill", name, user);
        }

        public void UpdateStory(Story s)
        {
            var id = mgr.GetParameter();
            id.ParameterName = "@ID";
            id.Value = s.ID;

            var name = mgr.GetParameter();
            name.ParameterName = "@Name";
            name.Value = s.Name;

            var user = mgr.GetParameter();
            user.ParameterName = "@User";
            user.Value = s.User;

            mgr.Execute("Player.usp_UpdateStory", id, name, user);
        }

        public void DeleteStory(int ID)
        {
            var id = mgr.GetParameter();
            id.ParameterName = "@ID";
            id.Value = ID;

            mgr.Execute("Player.usp_DeleteStory", id);
        }
    }
}