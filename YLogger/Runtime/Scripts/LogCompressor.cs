using System.IO;
using System.IO.Compression;

namespace YLogger
{
    public class LogCompressor
    {
        public static string CompressLogFile(string filePath)
        {
#if UNITY_WEBGL
            return filePath; // ²»Ñ¹Ëõ
#else
            if (!File.Exists(filePath)) return null;

            string zipPath = filePath + ".zip";
            using (FileStream zipToOpen = new FileStream(zipPath, FileMode.Create))
            {
                using (ZipArchive archive = new ZipArchive(zipToOpen, ZipArchiveMode.Create))
                {
                    var entry = archive.CreateEntry(Path.GetFileName(filePath));
                    using (var entryStream = entry.Open())
                    using (var fileStream = File.OpenRead(filePath))
                    {
                        fileStream.CopyTo(entryStream);
                    }
                }
            }

            return zipPath;
#endif

        }
    }
}
