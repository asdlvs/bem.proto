namespace Dnevnik.FileGenerator
{
    using System;
    using System.Configuration;
    using System.Diagnostics;
    using System.IO;
    using System.Reflection;
    using System.Runtime.InteropServices;
    using System.Text;

    using Microsoft.VisualStudio.TextTemplating.VSHost;

    [ComVisible(true)]
    [Guid("447D1A79-F4C4-43B7-BFB0-6BB5863D592B")]
    public class TscGenerator : BaseCodeGeneratorWithSite
    {
        protected override byte[] GenerateCode(string inputFileName, string inputFileContent)
        {
            try
            {
                var filename = Assembly.GetExecutingAssembly().Location;
                var configuration = ConfigurationManager.OpenExeConfiguration(filename);
                var target = configuration.AppSettings.Settings["tscTarget"].Value;
                var cmd = configuration.AppSettings.Settings["tscCmd"].Value;

                string resultPath = string.Format(@"{0}{1}", target, new FileInfo(inputFileName).Name.Replace(".ts", ".js"));

            var proc = new Process
            {
                StartInfo =
                {
                    UseShellExecute = false,
                    RedirectStandardOutput = true,
                    FileName = cmd,
                    Arguments = string.Format("{0} --out {1}", inputFileName, resultPath)
                }
            };

            var buffer = new byte[0];
            if (proc.Start())
            {
                proc.WaitForExit();
                var symLink = inputFileName.Replace(".ts", ".js");
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
            return ".js";
        }
    }
}
