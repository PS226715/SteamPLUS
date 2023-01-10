using SteamKit2;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace SteamAPIExample
{
    class Program
    {
        static void Main(string[] args)
        {
            // Create a new SteamClient object
            var client = new SteamClient();

            // Connect to Steam
            client.Connect();

            // Create a new SteamUser object
            var user = client.GetHandler<SteamUser>();

            // Log in to Steam using your username and password
            user.LogOn(new SteamUser.LogOnDetails
            {
                Username = "your_username",
                Password = "your_password"
            });

            // Wait for the logon process to complete
            while (user.LoggedOn == false)
            {
                Task.Delay(1000).Wait();
            }

            // Create a new SteamApps object
            var apps = client.GetHandler<SteamApps>();

            // Request a list of all the games in the user's library
            var games = apps.GetAppList().Result;

            // Save the list of games to a local file
            using (var writer = new StreamWriter("games.txt"))
            {
                foreach (var game in games)
                {
                    writer.WriteLine(game.Name);
                }
            }
        }
    }
}
