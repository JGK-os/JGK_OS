using System;

namespace OS
{
    public class Config
    {
        #region Config Variables

        public struct Ls
        {
            public      static      ConsoleColor    DirectoryColor;
            public      static      ConsoleColor    FileColor;

            public static void DefaultValues()
            {
                DirectoryColor = ConsoleColor.Green;
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
                            DirectoryColor = ChangeColor();

                        break;

                    case 2:

                        defValue = RestoreDefault();

                        if (defValue == 1)
                            FileColor = ConsoleColor.White;
                        else if (defValue == 2)
                            FileColor = ChangeColor();

                        break;

                    default:
                        break;
                }
            }
        }

        public struct ShellPrecommand
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
                                Path.Color = ChangeColor();

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
                    ShellPrecommand.Separator.Simbol = ">:";
                    ShellPrecommand.Separator.Color = ConsoleColor.White;
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
                                ShellPrecommand.Separator.Color = ConsoleColor.White;
                            else if (defValue == 2)
                                ShellPrecommand.Separator.Color = ChangeColor();

                            break;

                        case 2:
                            defValue = RestoreDefault();

                            if (defValue == 1)
                                ShellPrecommand.Separator.Simbol = ">:";
                            else if (defValue == 2)
                            {
                                Console.WriteLine();
                                Console.WriteLine( "Write your own separator" );
                                ShellPrecommand.Separator.Simbol = Console.ReadLine();
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
                            UsernameColor = ChangeColor();

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
                ShellPrecommand.Change();
            }
        }

        #region Support

        public static void Default()
        {
            Ls.DefaultValues();
            ShellPrecommand.DefaultValues();
        }

        private static ConsoleColor ChangeColor()
        {
            string[] tmp = {"White","Black","Gray","Red","Green","Yellow","Blue","Cyan","Magenta", "DarkGray" , "DarkRed","DarkGreen","DarkYellow","DarkBlue","DarkCyan", "DarkMagenta" };
            int choise = Choose(tmp);

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