using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BetterGit
{
    class MyGit
    {
        string branch = "";

        public bool Prepare()
        {
            // git stuff test
            //AskForGit();
            if (Settings.gitPath == "" || Settings.repoPath == "") return false;

            // cd stuff
            //Directory.SetCurrentDirectory(@"C:\Users\GRAMINI\Documents\MyUnityProjects\Tower-Defense");
            Directory.SetCurrentDirectory(Settings.repoPath);

            RefreshBranch();
            return true;
        }

        public bool Start()
        {
            Console.BackgroundColor = ConsoleColor.Green;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.Write(branch);

            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("\u25BA ");

            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.Write("git ");
            Console.ForegroundColor = ConsoleColor.Magenta;
            string param = Console.ReadLine().Replace("\t", "").ToLower();

            if (param.Length >= 4)
            {
                if (param[0] == 'g' && param[1] == 'i' && param[2] == 't' && param[3] == ' ')
                {
                    param = param.Substring(4);
                }
            }

            Console.Write("\n");

            int nonGitAction = ParseSpecialCommands(param);
            if (nonGitAction == -1) return false;

            Run(param);

            if (nonGitAction == 1) RefreshBranch();

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write("\n");
            Console.WriteLine("=========================================================================================");
            Console.WriteLine("=========================================================================================");
            Console.Write("\n");

            Console.ForegroundColor = ConsoleColor.Cyan;

            return true;
        }

        void Run(string param)
        {
            Console.ForegroundColor = ConsoleColor.Gray;
            Process proc = new Process();
            proc.StartInfo.FileName = Settings.gitPath;
            proc.StartInfo.Arguments = param;
            proc.StartInfo.RedirectStandardInput = true;
            proc.StartInfo.UseShellExecute = false;
            proc.Start();

            StreamWriter mySW = proc.StandardInput;

            proc.WaitForExit();
        }

        void RefreshBranch()
        {
            Console.ForegroundColor = ConsoleColor.Gray;
            Process proc = new Process();
            proc.StartInfo.FileName = Settings.gitPath;
            proc.StartInfo.Arguments = "rev-parse --abbrev-ref HEAD";
            proc.StartInfo.RedirectStandardInput = true;
            proc.StartInfo.RedirectStandardOutput = true;
            proc.StartInfo.UseShellExecute = false;
            proc.Start();

            string newBranch = proc.StandardOutput.ReadToEnd();

            StreamWriter mySW = proc.StandardInput;

            proc.WaitForExit();

            branch = newBranch.Replace("\n", "");
        }

        public static string AskForGit()
        {
            return Gramini.Manager.Input.GetInputFile("Where is your git executable? ");
        }

        public static string AskForRepoPath()
        {
            bool validInput = false;
            string input = "";

            while (!validInput)
            {
                input = Gramini.Manager.Input.GetInputPath();

                if (Directory.Exists(input + @"\.git"))
                {
                    validInput = true;
                    break;
                }

                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(input + " is not a git repository");
            }

            return input;
        }

        int ParseSpecialCommands(string input)
        {

            if (input == "exit")
            {
                return -1;
            }

            if (input.Contains("checkout"))
            {
                return 1;
            }

            return 0;
        }
    }
}
