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
        public static string repoPath = "";

        static string[] configFile;
        #endregion

        public static void FillSettings()
        {
            ReadConfigFile();

            if (gitPath == "")
            {
                gitPath = MyGit.AskForGit();
            }

            if (repoPath == "")
            {
                repoPath = MyGit.AskForRepoPath();
            }

            // everything is set:
            WriteToSettingsToConfig();
        }

        private static void WriteToSettingsToConfig()
        {
            string[] newConfig = new string[] {
                "gitPath = " + gitPath,
                "repoPath = " + repoPath
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
                repoPath = line.Substring(11);

                Console.WriteLine(repoPath);

                if (!Directory.Exists(repoPath))
                {
                    Console.WriteLine("exists");

                    repoPath = MyGit.AskForRepoPath();
                }

                return;
            }
        }

        public static void ChangeSettingsMenu()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine(
                "Change what settings?" +
                "\n" +
                "\n1 - Path of git.exe" +
                "\n2 - Repository"
                );

            int choice = Gramini.Manager.Input.GetInputInt(1, 2, true, "What setting to change? ", "Not a number!");


        }
    }
}
