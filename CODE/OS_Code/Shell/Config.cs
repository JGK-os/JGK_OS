using System;
using System.IO;
using External.Tools;
using OS_Code.Core;

namespace OS_Code.Shell
{
    public class Config
    {
        public static List<string> configFile;

        #region Config Variables

        public struct Ls
        {
            public      static      ConsoleColor    DirectoryColor;
            public      static      ConsoleColor    FileColor;

            public static void DefaultValues()
            {
                DirectoryColor = ConsoleColor.Yellow;
                FileColor = ConsoleColor.White;
            }

            public static void Change()
            {
                string[] tmp = { "Directory color" , "File color" };
                int input = Choose(tmp);

                Console.Clear();

                int defValue;

                switch (input)
                {
                    case 0:
                        DefaultValues();
                        break;

                    case 1:
                        defValue = RestoreDefault();

                        if (defValue == 1)
                            DirectoryColor = ConsoleColor.Green;
                        else if (defValue == 2)
                            DirectoryColor = ChangeColor( true , 0 );

                        break;

                    case 2:

                        defValue = RestoreDefault();

                        if (defValue == 1)
                            FileColor = ConsoleColor.White;
                        else if (defValue == 2)
                            FileColor = ChangeColor( true , 0 );

                        break;

                    default:
                        break;
                }
            }
        }

        public struct Prompt
        {
            public static ConsoleColor UsernameColor;

            public struct Path
            {
                public      static      string          Type;
                public      static      ConsoleColor    Color;

                public static void DefaultValues()
                {
                    Type = $"{Kernel.pwd}";
                    Color = ConsoleColor.White;
                }

                public static void Change()
                {
                    string[] tmp = {"Path color","Path Type"};

                    int input = Choose(tmp);

                    int defValue;

                    switch (input)
                    {
                        case 0:
                            DefaultValues();
                            break;

                        case 1:

                            defValue = RestoreDefault();

                            if (defValue == 1)
                                Path.Color = ConsoleColor.White;
                            else if (defValue == 2)
                                Path.Color = ChangeColor( true , 0 );

                            break;

                        case 2: //Todo
                            defValue = RestoreDefault();

                            if (defValue == 1)
                                Path.Type = "";
                            else if (defValue == 2)
                            {
                                string[] tempor = {"Full path","Last directory","No path"};

                                int temp = Choose(tempor);

                                switch (temp)
                                {
                                    default:
                                        break;
                                }
                                Path.Type = Console.ReadLine();
                            }
                            break;

                        default:
                            break;
                    }
                }
            }

            public struct Separator
            {
                public      static      string          Simbol;
                public      static      ConsoleColor    Color;

                public static void DefaultValues()
                {
                    Prompt.Separator.Simbol = ">:";
                    Prompt.Separator.Color = ConsoleColor.White;
                }

                public static void Change()
                {
                    string[] tmp = {"Separator color","Separator Simbol"};

                    int input = Choose(tmp);

                    int defValue;

                    switch (input)
                    {
                        case 0:
                            DefaultValues();
                            break;

                        case 1:
                            defValue = RestoreDefault();

                            if (defValue == 1)
                                Prompt.Separator.Color = ConsoleColor.White;
                            else if (defValue == 2)
                                Prompt.Separator.Color = ChangeColor( true , 0 );

                            break;

                        case 2:
                            defValue = RestoreDefault();

                            if (defValue == 1)
                                Prompt.Separator.Simbol = ">:";
                            else if (defValue == 2)
                            {
                                Console.WriteLine();
                                Console.WriteLine( "Write your own separator" );
                                Prompt.Separator.Simbol = Console.ReadLine();
                            }
                            break;

                        default:
                            break;
                    }
                }
            }

            public static void DefaultValues()
            {
                Path.DefaultValues();
                Separator.DefaultValues();
                UsernameColor = ConsoleColor.White;
            }

            public static void Change()
            {
                string[] tmp = {"Username color","Path","Separator"};

                int input = Choose(tmp);

                switch (input)
                {
                    case 0:
                        DefaultValues();
                        break;

                    case 1:
                        int defValue = RestoreDefault();

                        if (defValue == 1)
                            UsernameColor = ConsoleColor.White;
                        else if (defValue == 2)
                            UsernameColor = ChangeColor( true , 0 );

                        break;

                    case 2:
                        Path.Change();
                        break;

                    case 3:
                        Separator.Change();
                        break;

                    default:
                        break;
                }
            }
        }

        #endregion Config Variables

        public static void Set()
        {
            string[] tmp = { "\"ls\" command colors" , "Pre shell colors" };

            int input = Choose(tmp);

            if (input == 0)
            {
                Default();
            }
            else if (input == 1)
            {
                Ls.Change();
            }
            else if (input == 2)
            {
                Prompt.Change();
            }
        }

        #region Custom

        public static void LoadCustom()
        {
            // File di configurazione: "0:\\System\\config.cnf"
            configFile = new List<string>( 10 );

            string[] fileConfig = File.ReadAllLines( "0:\\System\\config.cnf" );

            for (int i = 0 ; i < fileConfig.Length - 1 ; i++)
            {
                configFile.Add( fileConfig[i] );
                configFile.list[i] = configFile.list[i].Split( " : " )[1];
            }

            int index = 0;

            Ls.DirectoryColor = ChangeColor( false , index++ );
            Ls.FileColor = ChangeColor( false , index++ );
            Prompt.UsernameColor = ChangeColor( false , index++ );
            Prompt.Separator.Color = ChangeColor( false , index++ );
            Prompt.Separator.Simbol = configFile.list[index++];
            Prompt.Path.Color = ChangeColor( false , index++ );
            Prompt.Path.Type = Kernel.pwd;
        }

        public static void SaveCustom()
        {
            File.WriteAllText( "0:\\System\\config.cnf" ,
$@"Directory Color : {ColorToString( Ls.DirectoryColor )}
File Color : {ColorToString( Ls.FileColor )}
Username Color : {ColorToString( Prompt.UsernameColor )}
Separator Color : {ColorToString( Prompt.Separator.Color )}
Separator Simbol : >:
Path Color : {ColorToString( Prompt.Path.Color )}
                " );
        }

        public static string ColorToString(ConsoleColor color)
        {
            if (ConsoleColor.Black == color)
            {
                return "Black";
            }
            else if (ConsoleColor.Blue == color)
            {
                return "Blue";
            }
            else if (ConsoleColor.Cyan == color)
            {
                return "Cyan";
            }
            else if (ConsoleColor.DarkBlue == color)
            {
                return "DarkBlue";
            }
            else if (ConsoleColor.DarkCyan == color)
            {
                return "DarkCyan";
            }
            else if (ConsoleColor.DarkGray == color)
            {
                return "DarkGray";
            }
            else if (ConsoleColor.DarkGreen == color)
            {
                return "DarkGreen";
            }
            else if (ConsoleColor.DarkMagenta == color)
            {
                return "DarkMagenta";
            }
            else if (ConsoleColor.DarkRed == color)
            {
                return "DarkRed";
            }
            else if (ConsoleColor.DarkYellow == color)
            {
                return "DarkYellow";
            }
            else if (ConsoleColor.Gray == color)
            {
                return "Gray";
            }
            else if (ConsoleColor.Green == color)
            {
                return "Green";
            }
            else if (ConsoleColor.Magenta == color)
            {
                return "Magenta";
            }
            else if (ConsoleColor.Red == color)
            {
                return "Red";
            }
            else if (ConsoleColor.Yellow == color)
            {
                return "Yellow";
            }
            else
            {
                return "White";
            }
        }

        #endregion Custom

        #region Support

        public static void Default()
        {
            Ls.DefaultValues();
            Prompt.DefaultValues();
        }

        private static ConsoleColor ChangeColor(bool print , int listIndex)
        {
            string[] tmp = {"White","Black","Gray","Red","Green","Yellow","Blue","Cyan","Magenta", "DarkGray" , "DarkRed","DarkGreen","DarkYellow","DarkBlue","DarkCyan", "DarkMagenta" };
            int choise = 0;

            if (print)
                choise = Choose( tmp );
            else
            {
                for (int i = 0 ; i < tmp.Length ; i++)
                {
                    if (tmp[i] == configFile.list[listIndex])
                    {
                        choise = i + 1;
                        i = tmp.Length;
                    }
                }
            }

            switch (choise)
            {
                case 1:
                    return ConsoleColor.White;

                case 2:
                    return ConsoleColor.Black;

                case 3:
                    return ConsoleColor.Gray;

                case 4:
                    return ConsoleColor.Red;

                case 5:
                    return ConsoleColor.Green;

                case 6:
                    return ConsoleColor.Yellow;

                case 7:
                    return ConsoleColor.Blue;

                case 8:
                    return ConsoleColor.Cyan;

                case 9:
                    return ConsoleColor.Magenta;

                case 10:
                    return ConsoleColor.DarkGray;

                case 11:
                    return ConsoleColor.DarkRed;

                case 12:
                    return ConsoleColor.DarkGreen;

                case 13:
                    return ConsoleColor.DarkYellow;

                case 14:
                    return ConsoleColor.DarkBlue;

                case 15:
                    return ConsoleColor.DarkCyan;

                case 16:
                    return ConsoleColor.DarkMagenta;

                default:
                    return ConsoleColor.White;
            }
        }

        private static int Choose(string[] s)
        {
            Console.Clear();
            Console.WriteLine();
            Console.WriteLine( "Select what you want to change :" );
            Console.WriteLine( "0 \t-Default values" );
            for (int i = 0 ; i < s.Length ; i++)
            {
                if (i < 9)
                {
                    Console.WriteLine( "{1} \t-{0}" , s[i] , i + 1 );
                }
                else
                {
                    Console.WriteLine( "{1}\t-{0}" , s[i] , i + 1 );
                }
            }

            Console.WriteLine( "{0} \t-Exit" , s.Length + 1 );
            Console.Write( "Your choise: " );

            int a = int.Parse(Console.ReadLine());

            Console.Clear();

            return a;
        }

        private static int RestoreDefault()
        {
            Console.WriteLine();
            Console.WriteLine( "Wanna restore default value? (yes/no)" );

            string defaultValues = Console.ReadLine();

            if (defaultValues.Equals( "y" ) || defaultValues.Equals( "yes" ))
            {
                return 1;
            }
            else if (defaultValues.Equals( "n" ) || defaultValues.Equals( "no" ))
            {
                return 2;
            }
            else
            {
                return 0;
            }
        }

        #endregion Support
    }
}