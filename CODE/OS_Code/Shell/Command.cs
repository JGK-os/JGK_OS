using System;
using Sys = Cosmos.System;
using OS_Code.Core;

namespace OS_Code.Shell
{
    internal class Command
    {
        public static void Identifier(string[] input)
        {
            if (input[0].Equals( "help" ))
            {
                Help( input );
            }
            else if (input[0].Equals( "cls" ) || input[0].Equals( "cls" ))
            {
                Console.Clear();
            }
            else if (input[0].Equals( "ver" ) || input[0].Equals( "version" ))
            {
                Console.WriteLine( "OS version: {0}" , Kernel.version );
            }
            else if (input[0].Equals( "echo" ))
            {
                Echo( input );
            }
            else if (input[0].Equals( "rm" ) || input[0].Equals( "remove" ) || input[0].Equals( "delete" ) || input[0].Equals( "del" ))
            {
                Remove( input );
            }
            else if (input[0].Equals( "h" ) || input[0].Equals( "hist" ) || input[0].Equals( "history" ))
            {
                History( input );
            }
            else if (input[0].Equals( "shutdown" ))
            {
                TryShutdown();
            }
            else if (input[0].Equals( "ls" ))
            {
                Ls( input );
            }
            else if (input[0].Equals( "touch" ) || input[0].Equals( "cnf" ) || input[0].Equals( "create" ) || input[0].Equals( "new" ))
            {
                CreateNewFile( input );
            }
            else if (input[0].Equals( "mkdir" ) || input[0].Equals( "cnd" ))
            {
                CreateNewDir( input );
            }
            else if (input[0].Equals( "cat" ))
            {
                if (input.Length.Equals( 2 ))
                {
                    string path = input[1];
                    Console.WriteLine( FileSystem.Cat( Kernel.pwd + path ) );
                }
                else
                {
                    Console.WriteLine( "No file specified" );
                }
            }
            else if (input[0].Equals( "pwd" ))
            {
                Console.WriteLine( Kernel.pwd );
            }
            else if (input[0].Equals( "whoami" ))
            {
                Console.WriteLine( Kernel.username );
            }
            else if (input[0].Equals( "cd" ))
            {
                for (int i = 1 ; i < input.Length ; i++)
                    FileSystem.Cd( input[i] );
            }
            else if (input[0].Equals( "exit" ))
            {
                Sys.Power.Shutdown();
            }
            else if (input[0].Equals( "reboot" ))
            {
                TryReboot();
            }
            else if (input[0].Equals( "config" ))
            {
                if (input.Length == 2 && input[1].Equals( "-s" ))//save custom config
                {
                    Config.SaveCustom();
                }
                else
                {
                    Config.Set();
                }
            }
            else if (input[0].Equals( "txt" ))
            {
                External.Program.TextEditor.TextEditor.Start( input.Length == 2 ? input[1] : "" );
            }
            else if (input[0].Equals( "rmd" ) || input[0].Equals( "rmdir" ))
            {
                RemoveDir( input );
            }
            else if (input[0].Equals("mv") || input[0].Equals("move"))
            {
                Move(input);
            }
            else if (input[0].Equals("cp") || input[0].Equals("copy"))
            {
                CopyFile(input);
            }
            else
            {
                Console.WriteLine( "Invalid command" );
                Console.WriteLine( "Type \"help\" for a comand list" );
                Console.WriteLine();
            }
        }

        #region Commands

        private static void InvalidParameter()
        {
            Console.WriteLine();
            Console.WriteLine( "The parameter is not valid" );
            Console.WriteLine( "Try \"help <command>\"" );
            Console.WriteLine();
        }

        #region File Management
        private static void CopyFile(string[] input)
        {
            if (input.Length.Equals(3))
                FileSystem.Copy(Kernel.pwd + input[1], Kernel.pwd + input[2]);
            else
                InvalidParameter();
        }
        private static void Move(string[] input)
        {
            if (input.Length.Equals(3))
            {
                FileSystem.Copy(Kernel.pwd + input[1], Kernel.pwd + input[2]);
                FileSystem.Remove(Kernel.pwd + input[1]);
            }
            else
                InvalidParameter();
        }
        private static void RemoveDir(string[] input)
        {
            if (input.Length.Equals( 2 ))
                try
                {
                    FileSystem.RemoveDir( Kernel.pwd + input[1] );
                }
                catch (Exception ex)
                {
                    Console.WriteLine( ex.Message );
                }
            else
                Console.WriteLine( "No file name specified" );
        }

        private static void CreateNewDir(string[] input)
        {
            if (input.Length == 2)
            {
                FileSystem.CreateDir( Kernel.pwd + input[1] );
            }
            else
                Console.WriteLine( "No directory name specified" );
        }

        private static void CreateNewFile(string[] input)
        {
            if (input.Length == 2)
            {
                try
                {
                    FileSystem.CreateFile( Kernel.pwd + input[1] );
                }
                catch (Exception ex)
                {
                    Console.WriteLine( ex.Message );
                }
            }
            else
                Console.WriteLine( "No file name specified" );
        }

        private static void Remove(string[] input)
        {
            if (input.Length >= 2)
            {
                if (!(input[1].Equals( "-v" ))) //Ask for a confirm
                {
                    for (int i = 1 ; i < input.Length ; i++)
                    {
                        try
                        {
                            FileSystem.Remove( Kernel.pwd + input[i] );
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine( ex.Message );
                        }
                    }
                }
                else
                {
                    bool boolean = true;
                    ConsoleKeyInfo c;

                    for (int i = 2 ; i < input.Length ; i++)
                    {
                        while (boolean)
                        {
                            boolean = true;
                            Console.WriteLine();
                            Console.Write( $"Are you sure you want to delete {input[i]}? (y or n) : " );
                            c = Console.ReadKey();

                            Console.WriteLine();

                            if (c.Key == ConsoleKey.Y)
                            {
                                FileSystem.Remove( Kernel.pwd + input[i] );
                                boolean = false;
                            }
                            else if (c.Key == ConsoleKey.N)
                            {
                                boolean = false;
                            }
                        }
                    }
                }
            }
            else
            {
                Console.WriteLine( "No file name specified" );
            }
        }

        private static void Ls(string[] input) //Change colors
        {
            string[] files;

            if (input.Length == 2)
            {
                files = FileSystem.Ls( Kernel.pwd + input[1] );
            }
            else
            {
                files = FileSystem.Ls( Kernel.pwd );
            }

            for (int i = 0 ; i < files.Length ; i++)
            {
                if (files[i].EndsWith( ' ' ))
                {
                    Console.ForegroundColor = Config.Ls.DirectoryColor;
                    Console.WriteLine( files[i] );
                    Console.ForegroundColor = ConsoleColor.White;
                }
                else
                {
                    Console.ForegroundColor = Config.Ls.FileColor;
                    Console.WriteLine( files[i] );
                    Console.ForegroundColor = ConsoleColor.White;
                }
            }
        }

        #endregion File Management

        #region System

        private static void TryShutdown()
        {
            ConsoleKeyInfo c;

            while (c.Key != ConsoleKey.Y && c.Key != ConsoleKey.N)
            {
                Console.WriteLine();
                Console.Write( "Are you sure you want to shutdown the system? (y or n) : " );
                c = Console.ReadKey();

                if (c.Key == ConsoleKey.Y)
                {
                    Sys.Power.Shutdown();
                }
                else if (c.Key == ConsoleKey.N)
                {
                    Console.WriteLine( "Abort" );
                }
            }
        }

        private static void TryReboot()
        {
            {
                ConsoleKeyInfo c;

                while (c.Key != ConsoleKey.Y && c.Key != ConsoleKey.N)
                {
                    Console.WriteLine();
                    Console.Write( "Are you sure you want to reboot the system? (y or n) : " );
                    c = Console.ReadKey();

                    if (c.Key == ConsoleKey.Y)
                    {
                        Sys.Power.Reboot();
                    }
                    else if (c.Key == ConsoleKey.N)
                    {
                        Console.WriteLine( "Abort" );
                    }
                }
            }
        }

        #endregion System

        #region Shell

        private static void Help(string[] command)
        {
            if (command.Length == 1 || command[1].Equals( "help" ))
            {
                Console.WriteLine();
                Console.WriteLine( "Here the commands:" );
                Console.WriteLine( "[NI] = not implemented yet" );
                Console.WriteLine();
                Console.WriteLine();
                Console.WriteLine( "cat     \t\t -Print a file content" );
                Console.WriteLine( "cd      \t\t -Change drectory" );
                Console.WriteLine( "cls     \t\t -Clear the sceen" );
                Console.WriteLine( "echo    \t\t -Print text back" );
                Console.WriteLine( "help    \t\t -Display this message" );
                Console.WriteLine( "hist    \t\t -Use last command" );
                Console.WriteLine( "ls      \t\t -List files and directorys" );
                Console.WriteLine( "pwd     \t\t -Show current directory" );
                Console.WriteLine( "rm      \t\t -[NI]Remove a file or directory" );
                Console.WriteLine( "ver     \t\t -Display the version" );
                Console.WriteLine( "whoami  \t\t -Who are you" );
                Console.WriteLine();
            }
            else
            {
                //Go to manuals and find the right one
            }
        }

        private static void Echo(string[] input)
        {
            Console.WriteLine();

            for (int i = 1 ; i < input.Length ; i++)
            {
                Console.Write( input[i] + " " );
            }

            Console.WriteLine();
            Console.WriteLine();
        }

        private static void History(string[] input)
        {
            if (input.Length == 1)
            {
                Kernel.nextCommand = true;
                Kernel.history.Add( Kernel.history.list[Kernel.history.Count - 2] );
            }
            else if (int.TryParse( input[1] , out int n ) && Kernel.history.Count - n - 1 > 0 && n != 0)
            {
                Kernel.nextCommand = true;
                Kernel.history.Add( Kernel.history.list[Kernel.history.Count - n - 1] );
            }
            else if (input[1].Equals( "-c" ))
            {
                Kernel.history.Clear();
            }
            else if (input.Length.Equals( 3 ) && input[1].Equals( "-s" ) && int.TryParse( input[2] , out n ))
            {
                if (n > Kernel.history.Count)
                {
                    n = Kernel.history.Count;
                }
                else if (n < -Kernel.history.Count)
                {
                    n = -Kernel.history.Count;
                }

                if (n > 0)
                {
                    for (int i = Kernel.history.Count - n ; i < Kernel.history.Count ; i++)
                    {
                        Console.WriteLine( Kernel.history.list[i] );
                    }
                }
                else
                {
                    for (int i = 0 ; i < -n ; i++)
                    {
                        Console.WriteLine( Kernel.history.list[i] );
                    }
                }
            }
            else
            {
                InvalidParameter();
            }
        }

        #endregion Shell

        #endregion Commands
    }
}