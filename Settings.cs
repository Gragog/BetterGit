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

        public static string gitPath = "";
        public static string repoPath = "";

        static string[] configFile;

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
            Console.WriteLine(exePath + @"settings.cfg");

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
            // gitPath
            if (line.StartsWith("gitPath = "))
            {
                gitPath = line.Substring(13);
                return;
            }

            if (line.StartsWith("repoPath = "))
            {
                repoPath = line.Substring(11);
                return;
            }
        }
    }
}
