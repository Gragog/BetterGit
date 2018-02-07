using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BetterGit
{
    class Program
    {
        private static MyGit program;

        public static MyGit MyGitInstance { get { return program; } }

        static void Main(string[] args)
        {
            Settings.exePath = Directory.GetCurrentDirectory() + @"\";
            //Console.WriteLine(Settings.exePath);
            Settings.FillSettings();

            program = new MyGit();
            bool keepOn = program.Prepare();
            while (keepOn) keepOn = program.Start();

            //Console.ReadLine();
        }
    }
}
