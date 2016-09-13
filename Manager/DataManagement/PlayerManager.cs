using Manager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Manager.DataManagement
{
    public class PlayerManager
    {
        private DataManager mgr = new DataManager();

        public List<Player> GetAllPlayers(string playerUsername)
        {
            var username = mgr.GetParameter();
            username.ParameterName = "@Username";
            username.Value = playerUsername;

            return mgr.GetData("Player.usp_GetAllPlayers", username)
                .Select(x => new Player
                {
                    ID = Convert.ToInt32(x["ID"]),
                    Name = x["Name"].ToString(),
                    Surname = x["Surname"].ToString(),
                    Story = x["Story"].ToString()
                })
                .ToList();
        }

        public Player GetPlayer(int ID)
        {
            var playerID = mgr.GetParameter();
            playerID.ParameterName = "@ID";
            playerID.Value = ID;

            return mgr.GetData("Player.usp_GetPlayer", playerID)
                .Select(x => new Player
                {
                    ID = Convert.ToInt32(x["ID"]),
                    Name = x["Name"].ToString(),
                    Surname = x["Surname"].ToString(),
                    Story = x["Story"].ToString()
                })
                .FirstOrDefault();
        }

        public void CreatePlayer(string username, Player p)
        {
            var name = mgr.GetParameter();
            name.ParameterName = "@Name";
            name.Value = p.Name;

            var surname = mgr.GetParameter();
            surname.ParameterName = "@Surname";
            surname.Value = p.Surname;

            var story = mgr.GetParameter();
            story.ParameterName = "@Story";
            story.Value = p.Story;

            var user = mgr.GetParameter();
            user.ParameterName = "@User";
            user.Value = username;

            mgr.Execute("Player.usp_CreatePlayer", name, surname, story, user);
        }

        public void UpdatePlayer(string username, Player p)
        {
            var id = mgr.GetParameter();
            id.ParameterName = "@ID";
            id.Value = p.ID;

            var name = mgr.GetParameter();
            name.ParameterName = "@Name";
            name.Value = p.Name;

            var surname = mgr.GetParameter();
            surname.ParameterName = "@Surname";
            surname.Value = p.Surname;

            var story = mgr.GetParameter();
            story.ParameterName = "@Story";
            story.Value = p.Story;

            var user = mgr.GetParameter();
            user.ParameterName = "@User";
            user.Value = username;

            mgr.Execute("Player.usp_UpdatePlayer", id, name, surname, story, user);
        }

        public void DeletePlayer(Player p)
        {
            var id = mgr.GetParameter();
            id.ParameterName = "@ID";
            id.Value = p.ID;

            mgr.Execute("Player.usp_DeletePlayer", id);
        }
    }
}