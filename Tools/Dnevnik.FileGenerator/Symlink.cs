namespace Dnevnik.FileGenerator
{
    using System.Runtime.InteropServices;

    public static class Symlink
    {
        [DllImport("kernel32.dll")]
        static extern bool CreateSymbolicLink(string lpSymlinkFileName, string lpTargetFileName, SymbolicLink dwFlags);

        enum SymbolicLink
        {
            File = 0,
            Directory = 1
        }

        public static bool Create(string symLink, string path)
        {
            return CreateSymbolicLink(symLink, path, SymbolicLink.File);
        }
    }
}
