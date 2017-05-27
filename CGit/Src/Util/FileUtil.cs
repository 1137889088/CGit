using System;
using System.IO;
using System.IO.Compression;

namespace CGit.Src.Util
{
    public class FileUtil
    {

        static void zipFile(string sourcePath, string targetPath)
        {
            ZipFile.CreateFromDirectory(sourcePath, targetPath);
            //ZipFile.ExtractToDirectory(zipPath, extractPath);
        }
    }
}
