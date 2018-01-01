using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Otter;
//http://metalslug.wikia.com/wiki/Metal_Slug_(Game)
//https://www.spriters-resource.com/search/?q=%22Metal+Slug%22&c=-1&o%5B%5D=g
namespace IdleGame
{
    class Program
    {
        static void Main(string[] args)
        {
            var game = new Game("Spritemap Animation", 1920, 1080);
            game.GameFolder = "PlayerSessionDemo";
            var session = game.AddSession("Player");
            session.Data.ExportMode = DataSaver.DataExportMode.Config;
            game.SetWindowAutoFullscreen(true);
            var scene = new MainScene(session);
            game.MouseVisible = true ;
            Music.GlobalVolume = 1f;
            Sound.GlobalVolume = 0.5f;
            game.Start(scene);
        }
    }
}
