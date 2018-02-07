using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BetterGit
{
    class Settings
    {
        public static string exePath = "";

        #region Stuff for settings.cfg
        public static string gitPath = "";
        public static string currentRepoPath = "";

        static string[] configFile;
        #endregion

        public static void FillSettings()
        {
            ReadConfigFile();

            if (gitPath == "")
            {
                gitPath = MyGit.AskForGit();
            }

            if (currentRepoPath == "")
            {
                currentRepoPath = MyGit.AskForRepoPath();
            }

            // everything is set:
            WriteToSettingsToConfig();
        }

        private static void WriteToSettingsToConfig()
        {
            string[] newConfig = new string[] {
                "gitPath = " + gitPath,
                "repoPath = " + currentRepoPath
            };

            File.WriteAllLines(exePath + @"settings.cfg", newConfig);
        }

        static void ReadConfigFile()
        {
            if (!File.Exists(exePath + @"settings.cfg"))
            {
                File.WriteAllText(exePath + @"settings.cfg", "");
                return;
            }

            configFile = File.ReadAllLines(exePath + @"settings.cfg");

            foreach (string line in configFile)
            {
                WriteToSettings(line);
            }
        }

        private static void WriteToSettings(string line)
        {
            if (line.StartsWith("//")) return;

            // gitPath
            if (line.StartsWith("gitPath = "))
            {
                gitPath = line.Substring(10);
                if (!File.Exists(gitPath))
                {
                    gitPath = MyGit.AskForGit();
                }
                return;
            }

            if (line.StartsWith("repoPath = "))
            {
                currentRepoPath = line.Substring(11);

                if (!Directory.Exists(currentRepoPath))
                {
                    currentRepoPath = MyGit.AskForRepoPath();
                }

                return;
            }
        }

        public static void ChangeSettingsMenu()
        {
            while (true)
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.WriteLine(
                    "Change what settings?" +
                    "\n" +
                    "\n1 - Path of git.exe" +
                    "\n2 - Repository" +
                    "\n" +
                    "\ne - exit" +
                    "\n"
                    );

                string choice = Gramini.Manager.Input.GetInput(@"^[12e]$", "What setting to change? ", "Not a valid choice!", ConsoleColor.DarkGreen, ConsoleColor.DarkMagenta);

                Console.WriteLine();

                switch (choice)
                {
                    case "1": Settings.gitPath = MyGit.AskForGit(); break;
                    case "2": Settings.currentRepoPath = MyGit.AskForRepoPath(); break;

                    case "e": Settings.WriteToSettingsToConfig(); Console.Clear(); Console.WriteLine("Settings saved to config\n"); Program.MyGitInstance.Prepare(); return;
                }
            }
        }
    }
}
