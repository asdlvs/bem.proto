using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dnevnik.FileGenerator
{
    using System.Diagnostics;
    using System.Runtime.InteropServices;

    using Microsoft.VisualStudio.TextTemplating.VSHost;

    [ComVisible(true)]
    [Guid("ec52dd18-6bcf-46e7-9f6b-13661e37d3f3")]
    public class LessGenerator : BaseCodeGeneratorWithSite
    {
        protected override byte[] GenerateCode(string inputFileName, string inputFileContent)
        {
            var proc = new Process
            {
                StartInfo =
                {
                    UseShellExecute = false,
                    RedirectStandardOutput = true,
                    FileName = @"c:\users\vlebedev\AppData\Roaming\npm\lessc.cmd",
                    Arguments = inputFileName
                }
            };

            if (proc.Start())
            {
                string output = proc.StandardOutput.ReadToEnd();
                proc.WaitForExit();
                return Encoding.UTF8.GetBytes(output);
            }
            return Encoding.UTF8.GetBytes("Process couldn't be started.");
        }

        public override string GetDefaultExtension()
        {
            return ".css";
        }
    }
}
