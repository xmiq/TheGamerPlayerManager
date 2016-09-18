using Manager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Manager.DataManagement
{
    public class ChapterManager
    {
        private DataManager mgr = new DataManager();

        public List<Chapter> GetAllChapters(int PlayerID)
        {
            var player = mgr.GetParameter();
            player.ParameterName = "@Player";
            player.Value = PlayerID;

            return mgr.GetData("Player.usp_GetAllChapters", player)
                .Select(x => new Chapter
                {
                    ID = Convert.ToInt32(x["ID"]),
                    Number = Convert.ToInt32(x["Number"])
                })
                .ToList();
        }

        public Chapter GetNextChapter(int PlayerID)
        {
            var player = mgr.GetParameter();
            player.ParameterName = "@Player";
            player.Value = PlayerID;

            return mgr.GetData("Player.usp_GetNextChapter", player)
                .Select(x => new Chapter
                {
                    Number = Convert.ToInt32(x["Number"])
                })
                .FirstOrDefault();
        }

        public void CreateChapter(Chapter c)
        {
            var player = mgr.GetParameter();
            player.ParameterName = "@Player";
            player.Value = c.Player.ID;

            var number = mgr.GetParameter();
            number.ParameterName = "@Number";
            number.Value = c.Number;

            mgr.Execute("Player.usp_CreatePlayer", player, number);
        }
    }
}