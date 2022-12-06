using System.IO;

namespace ViagemYamaha.Core.Extensions
{
    public static class FileExtensions
    {
        public static bool FileExists(this string fileName)
        {
            return File.Exists(fileName);
        }
    }
}
