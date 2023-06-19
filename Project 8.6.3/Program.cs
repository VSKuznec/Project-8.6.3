using System;
using System.IO;

namespace Task3
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                DirectoryInfo folder = new DirectoryInfo("C:\\Repository");
                
                long totalFolderSize = FolderSize(folder);
                Console.WriteLine("Размер папки в байтах: " + totalFolderSize);

                Delete(folder);

                long folderSizeAfter = FolderSize(folder);

                long freedFiles = folderSizeBefore - folderSizeAfter;

                Console.WriteLine("Освобождено: " + freedFiles);

                Console.WriteLine("Текущий размер папки: " + folderSizeAfter);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        static long FolderSize(DirectoryInfo folder)
        {
            if (folder.Exists)
            {
                long totalSizeOfDir = 0;

                FileInfo[] allFiles = folder.GetFiles();
                foreach (FileInfo file in allFiles)
                {
                    totalSizeOfDir += file.Length;
                }

                DirectoryInfo[] subFolders = folder.GetDirectories();
                foreach (DirectoryInfo dir in subFolders)
                {
                    totalSizeOfDir += FolderSize(dir);
                }

                return totalSizeOfDir;

            }
            else
            {
                Console.WriteLine("Такой папки не существует");
                return 0;
            }
        }
        static void Delete(DirectoryInfo folder)
        {

            try
            {
                DirectoryInfo fs = new DirectoryInfo(folder.ToString());

                if (fs.Exists)
                {
                    foreach (FileInfo file in fs.GetFiles())
                    {
                            file.Delete();
                    }

                    foreach (DirectoryInfo subFs in fs.GetDirectories())
                    {
                        if (subFs.Exists)
                        {
                            foreach (FileInfo childFile in subFs.GetFiles())
                            {
                                    childFile.Delete();
                            }

                            Directory.Delete(subFs.FullName, true);
                        }
                        else
                        {
                            Console.WriteLine("Error: Directory not found at path {0}", subFs.FullName);
                        }
                    }
                }
                else
                {
                    Console.WriteLine("Error: Folder not found at path {0}", folder);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}