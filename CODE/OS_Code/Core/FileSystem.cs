using System;
using System.IO;

namespace OS_Code.Core
{
    internal class FileSystem
    {
        #region Commands
        public static void Copy(string originPath, string destPath)
        {
            if (File.Exists(originPath))
            {
                if (originPath == destPath)
                {
                    destPath += "-COPY";
                }
                File.Copy(originPath, destPath);
            }
            else
                Console.WriteLine("Invalid source file name");
        }
        public static void RemoveDir(string path)
        {
            if (Directory.Exists( path ))
                Directory.Delete( path );
            else
                Console.WriteLine( "Invalid directory name" );
        }

        public static void CreateDir(string path)
        {
            if (!Directory.Exists( path ))
                Directory.CreateDirectory( path );
            else
                Console.WriteLine( "Directory already exists" );
        }

        public static void CreateFile(string path)
        {
            if (!File.Exists( path ))
                File.Create( path );
            else
                Console.WriteLine( "File already exists" );
        }

        public static void Remove(string path)
        {
            if (File.Exists( path ))
            {
                File.Delete( path );
            }
            else
                Console.WriteLine( "File not found" );
        }

        public static string[] Ls(string path)
        {
            string[]    dirs    = GetDirectories(path);
            string[]    files   = GetFiles(path);
            string[]    ret     = new string[dirs.Length + files.Length];

            for (int i = 0 ; i < dirs.Length ; i++)
                ret[i] = dirs[i] + " ";

            for (int i = dirs.Length ; i < ret.Length ; i++)
                ret[i] = files[i - dirs.Length];

            return ret;
        }

        public static string Cat(string path)
        {
            string text;
            try
            {
                Console.WriteLine( path );
                text = File.ReadAllText( path );
            }
            catch (Exception a)
            {
                return a.Message;
            }

            return text;
        }

        public static void Cd(string path)
        {
            string check = path[0].ToString() + path[1].ToString();

            // If the path is absolute
            if (check == "0:")
            {
                if (Directory.Exists( path ))
                {
                    Kernel.pwd = path + "\\";
                }

                Console.WriteLine( "Invalid path" );
            }
            else
            {
                if (path == "..")
                {
                    string[] percorso = Kernel.pwd.Split("\\");
                    string finale = "";

                    for (int i = 0 ; i < percorso.Length - 2 ; i++)
                    {
                        finale += percorso[i] + "\\";
                    }

                    Kernel.pwd = finale;
                }
                else if (Directory.Exists( Kernel.pwd + path ))
                {
                    Kernel.pwd = Kernel.pwd + path + "\\";
                }
                else
                {
                    Console.WriteLine( "Directory " + path + " does not exist" );
                }
            }
        }

        #endregion Commands

        #region Tools

        private static string[] GetFiles(string Adr)
        {
            string[] Files = new string[Directory.GetFiles(Adr).Length];

            if (Files.Length > 0)
                Files = Directory.GetFiles( Adr );
            else
                Files[0] = "No files found.";

            return Files;
        }

        private static string[] GetDirectories(string Adr)
        {
            string[] Directories = new string[Directory.GetDirectories(Adr).Length];

            if (Directories.Length > 0)
                Directories = Directory.GetDirectories( Adr );
            else
                Directories[0] = "No directories found.";

            return Directories;
        }

        #endregion Tools
    }
}