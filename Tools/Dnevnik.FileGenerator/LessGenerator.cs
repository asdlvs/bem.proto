using System;
using System.Text;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;

using Microsoft.VisualStudio.TextTemplating.VSHost;

namespace Dnevnik.FileGenerator
{
    using System.Reflection;

    [ComVisible(true)]
    [Guid("295A802C-A100-4858-BD7D-AB6C2904DE6B")]
    public class LessGenerator : BaseCodeGeneratorWithSite
    {
        protected override byte[] GenerateCode(string inputFileName, string inputFileContent)
        {
            try
            {
                var filename = Assembly.GetExecutingAssembly().Location;
                var configuration = ConfigurationManager.OpenExeConfiguration(filename);
                var target = configuration.AppSettings.Settings["lessTarget"].Value;
                var cmd = configuration.AppSettings.Settings["lessCmd"].Value;

                string resultPath = string.Format(@"{0}{1}", target, new FileInfo(inputFileName).Name.Replace(".less", ".css"));

                var proc = new Process
                {
                    StartInfo =
                    {
                        UseShellExecute = false,
                        RedirectStandardOutput = true,
                        FileName = cmd,
                        Arguments = string.Format("\"{0}\" \"{1}\"", inputFileName, resultPath)
                    }
                };

                var buffer = new byte[0];
                if (proc.Start())
                {
                    proc.WaitForExit();
                    var symLink = inputFileName.Replace(".less", ".css");
                    if (!File.Exists(symLink))
                    {
                        Symlink.Create(symLink, resultPath);
                    }

                    buffer = File.ReadAllBytes(symLink);
                }
                return buffer;
            }
            catch (Exception ex)
            {
                return Encoding.UTF8.GetBytes(ex.Message);
                throw;
            }
        }


        public override string GetDefaultExtension()
        {
            return ".css";
        }
    }
}
