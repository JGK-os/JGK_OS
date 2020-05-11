using System;
using System.IO;
using Sys = Cosmos.System;

namespace OS
{
    public class Kernel : Sys.Kernel
    {
        #region Global Variables

        public static               bool            nextCommand     = false;
        public static   readonly    string          version         = "v 0.0.1 pre-alpha";
        public static               string          pwd             = "0:\\";
        public static               string          username;
        public static               List<string>    history         = new List<string>(3);

        #endregion Global Variables

        protected override void BeforeRun()
        {
            // Inizializza File system
            var fs = new Sys.FileSystem.CosmosVFS();
            Sys.FileSystem.VFS.VFSManager.RegisterVFS( fs );

            #region Installation

           if (!File.Exists( "0:\\System\\users" ))
            {
                Console.Clear();

                #region Create User

                Console.Clear();
                Console.WriteLine( "GUIDED JGKOS INSTALLATION" );
                Console.WriteLine();
                Console.WriteLine( "Create a default username and password:" );
                Console.WriteLine();
                Console.Write( "Username: " );
                string username = Console.ReadLine();
                Console.Write( "Password: " );
                string cPassword = Console.ReadLine();

                #endregion Create User

                #region Create Directory

                //Console.WriteLine("Deleating default dirs...");
                //Directory.Delete("0:\\Dir Testing\\");
                //Directory.Delete("0:\\TEST\\");
                //File.Delete("0:\\Kudzu.txt");
                //File.Delete("0:\\Root.txt");

                Console.WriteLine();
                Console.WriteLine( "Creating system files..." );
                Console.WriteLine();

                fs.CreateDirectory( "0:\\System\\" );
                fs.CreateFile( "0:\\System\\USERS" );
                fs.CreateFile( "0:\\System\\readme.txt" );
                fs.CreateFile( "0:\\System\\CONFIG.cnf" );

                string FirstUserFile = "root:0:password1\n" + username + ":1001:" + cPassword;

                File.WriteAllText( "0:\\System\\users" , FirstUserFile );

                #region Config

                Config.Default();
                Config.SaveCustom();

                #endregion Config

                #endregion Create Directory

                Console.WriteLine();
                Console.WriteLine();
                Console.WriteLine( "Installation done" );
            }

            #endregion Installation

            Config.LoadCustom();

            #region Logo

            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.Write( $@"
_________ _______  _             _______  _______
\__    _/(  ____ \| \    /\     (  ___  )(  ____ \
   )  (  | (    \/|  \  / /     | (   ) || (    \/
   |  |  | |      |  (_/ /      | |   | || (_____
   |  |  | | ____ |   _ (       | |   | |(_____  )
   |  |  | | \_  )|  ( \ \      | |   | |      ) |
|\_)  )  | (___) ||  /  \ \     | (___) |/\____) |
(____/   (_______)|_/    \/_____(_______)\_______)
                          (_____)

Username : developer
Password : dev

----------------------------------------------
 JKG OS has been booted ({version})
 Type help for command list
----------------------------------------------

            " );

            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.White;

            #endregion Logo

            Crypto.Login();
        }

        protected override void Run()
        {
            Console.ForegroundColor = Config.Prompt.UsernameColor;
            Console.Write( $"{username}" );

            Console.ForegroundColor = Config.Prompt.Path.Color;
            Console.Write( $" : {Config.Prompt.Path.Type} " );

            Console.ForegroundColor = Config.Prompt.Separator.Color;
            Console.Write( $"{Config.Prompt.Separator.Simbol}" );
            Console.ForegroundColor = ConsoleColor.White;

            if (!nextCommand)
            {
                history.Add( Console.ReadLine().ToLower() );
            }

            nextCommand = false;

            Command.Identifier( history.list[history.Count - 1].Split() );
        }
    }
}