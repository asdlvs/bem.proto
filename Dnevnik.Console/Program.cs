using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dnevnik.Console
{
    using System.Diagnostics;

    using Console = System.Console;

    class Program
    {
        static void Main(string[] args)
        {
            var proc = new Process
                       {
                           StartInfo =
                           {
                               UseShellExecute = false,
                               RedirectStandardOutput = true,
                               FileName = @"c:\users\vlebedev\AppData\Roaming\npm\lessc.cmd",
                               Arguments = @"D:\Projs\proto\Dnevnik.Proto\Dnevnik.Blocks\Views\menu\menu.less"
                           }
                       };

            if(proc.Start())
            {
                string output = proc.StandardOutput.ReadToEnd();
                proc.WaitForExit();
                Console.WriteLine(output);
            }
            else
            {
                Console.WriteLine("Process couldn't be started.");
            }
        }
    }
}
