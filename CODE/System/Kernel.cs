<<<<<<< HEAD
﻿using System;
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

            #region Logo

            //          ______ /\\\\\\\\\\\_        _____/\\\\\\\\\\\\_        __/\\\________ /\\\_        _______/\\\\\______        _____/\\\\\\\\\\\___
            // _____\/////\\\///__        ___/\\\//////////__        _\/\\\_____/\\\//__        _____/\\\///\\\____        ___/\\\/////////\\\_
            //_________\/\\\_____        __/\\\_____________        _\/\\\__ /\\\//_____        ___/\\\/__\///\\\__        __\//\\\______\///__
            // _________\/\\\_____        _\/\\\____ /\\\\\\\_        _\/\\\\\\//\\\_____        __/\\\______\//\\\_        ___\////\\\_________
            //  _________\/\\\_____        _\/\\\___\/////\\\_        _\/\\\//_\//\\\____        _\/\\\_______\/\\\_        ______\////\\\______
            //   _________\/\\\_____        _\/\\\_______\/\\\_        _\/\\\____\//\\\___        _\//\\\______/\\\__        _________\////\\\___
            //    __ /\\\___\/\\\_____        _\/\\\_______\/\\\_        _\/\\\_____\//\\\__        __\///\\\__/\\\____        __/\\\______\//\\\__
            //     _\//\\\\\\\\\______        _\//\\\\\\\\\\\\/__        _\/\\\______\//\\\_        ____\///\\\\\/_____        _\///\\\\\\\\\\\/___
            //      __\/////////_______        __\////////////____        _\///________\///__        ______\/////_______        ___\///////////_____

            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.Write( $@"

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
                fs.CreateFile( "0:\\System\\users" );
                fs.CreateFile( "0:\\System\\readme.txt" );

                string FirstUserFile = "root:0:password1\n" + username + ":1001:" + cPassword;

                File.WriteAllText( "0:\\System\\users" , FirstUserFile );

                #endregion Create Directory

                Console.WriteLine();
                Console.WriteLine();
                Console.WriteLine( "Installation done" );
            }

            #endregion Installation

            Config.Default();

            Crypto.Login();
        }

        protected override void Run()
        {
            Console.ForegroundColor = Config.ShellPrecommand.UsernameColor;
            Console.Write( $"{username}" );

            Console.ForegroundColor = Config.ShellPrecommand.Path.Color;
            Console.Write( $" : {Config.ShellPrecommand.Path.Type} " );

            Console.ForegroundColor = Config.ShellPrecommand.Separator.Color;
            Console.Write( $"{Config.ShellPrecommand.Separator.Simbol}" );
            Console.ForegroundColor = ConsoleColor.White;

            if (!nextCommand)
            {
                history.Add( Console.ReadLine().ToLower() );
            }

            nextCommand = false;

            Command.Identifier( history.list[history.Count - 1].Split() );
        }
    }
=======
﻿using System;
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

            #region Logo

            //          ______ /\\\\\\\\\\\_        _____/\\\\\\\\\\\\_        __/\\\________ /\\\_        _______/\\\\\______        _____/\\\\\\\\\\\___
            // _____\/////\\\///__        ___/\\\//////////__        _\/\\\_____/\\\//__        _____/\\\///\\\____        ___/\\\/////////\\\_
            //_________\/\\\_____        __/\\\_____________        _\/\\\__ /\\\//_____        ___/\\\/__\///\\\__        __\//\\\______\///__
            // _________\/\\\_____        _\/\\\____ /\\\\\\\_        _\/\\\\\\//\\\_____        __/\\\______\//\\\_        ___\////\\\_________
            //  _________\/\\\_____        _\/\\\___\/////\\\_        _\/\\\//_\//\\\____        _\/\\\_______\/\\\_        ______\////\\\______
            //   _________\/\\\_____        _\/\\\_______\/\\\_        _\/\\\____\//\\\___        _\//\\\______/\\\__        _________\////\\\___
            //    __ /\\\___\/\\\_____        _\/\\\_______\/\\\_        _\/\\\_____\//\\\__        __\///\\\__/\\\____        __/\\\______\//\\\__
            //     _\//\\\\\\\\\______        _\//\\\\\\\\\\\\/__        _\/\\\______\//\\\_        ____\///\\\\\/_____        _\///\\\\\\\\\\\/___
            //      __\/////////_______        __\////////////____        _\///________\///__        ______\/////_______        ___\///////////_____

            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.Write( $@"

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
                fs.CreateFile( "0:\\System\\users" );
                fs.CreateFile( "0:\\System\\readme.txt" );

                string FirstUserFile = "root:0:password1\n" + username + ":1001:" + cPassword;

                File.WriteAllText( "0:\\System\\users" , FirstUserFile );

                #endregion Create Directory

                Console.WriteLine();
                Console.WriteLine();
                Console.WriteLine( "Installation done" );
            }

            #endregion Installation

            Config.Default();

            Crypto.Login();
        }

        protected override void Run()
        {
            Console.ForegroundColor = Config.ShellPrecommand.UsernameColor;
            Console.Write( $"{username}" );

            Console.ForegroundColor = Config.ShellPrecommand.Path.Color;
            Console.Write( $" : {Config.ShellPrecommand.Path.Type} " );

            Console.ForegroundColor = Config.ShellPrecommand.Separator.Color;
            Console.Write( $"{Config.ShellPrecommand.Separator.Simbol}" );
            Console.ForegroundColor = ConsoleColor.White;

            if (!nextCommand)
            {
                history.Add( Console.ReadLine().ToLower() );
            }

            nextCommand = false;

            Command.Identifier( history.list[history.Count - 1].Split() );
        }
    }
>>>>>>> 2bda2d2289b11998c1550d61dd4e386d13f41e7d
}