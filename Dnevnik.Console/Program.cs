using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dnevnik.Console
{
    using System.Diagnostics;
    using System.Text.RegularExpressions;

    using Console = System.Console;

    class Program
    {
        static void Main(string[] args)
        {
            var regex = new Regex(@"^(?<path>[^\-]*)(\-?(?<mod>\S*)?)$", RegexOptions.IgnoreCase);
            int pathPart = regex.GroupNumberFromName("path");
            int modPart = regex.GroupNumberFromName("mod");

            string value = "container/widget/widget-mod";
            var b = regex.IsMatch(value);

            var match = regex.Match(value);
            string path = match.Groups[pathPart].Value;
            string mod = match.Groups[modPart].Value;
        }
    }
}
