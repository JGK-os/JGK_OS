using System;
using System.IO;
using OS_Code.Core;

namespace External.Program.TextEditor
{
    public class TextEditor
    {
        #region Variables

        private static string FileName { get; set; } = string.Empty;
        private static string[] Text { get; set; }

        private static int  MaxLines      = 23;
        private static int  Col           = 0;
        private static int  Row           = 0;

        #endregion Variables

        public static void Start(string name)
        {
            Text = new string[24];
            Col = 0;
            Row = 0;

            FileManagement( name );

            Start:

            Console.SetCursorPosition( 0 , 0 );

            for (int i = 0 ; i < MaxLines ; i++)
            {
                Console.WriteLine( Text[i] );
            }

            bool running = true;

            while (running)
            {
                Console.SetCursorPosition( Col , Row );

                running = OnDataReceive();
            }

            Console.Clear();

            if (Text != null)
            {
                Console.WriteLine( "Choose an option" );
                Console.WriteLine();
                Console.WriteLine( "\t1 - Save" );
                Console.WriteLine();
                Console.WriteLine( "\t2 - Save and Exit" );
                Console.WriteLine();
                Console.WriteLine( "\t3 - Exit without Save" );
                Console.WriteLine();
                Console.WriteLine( "\t0 - Cancel" );

                int choise = int.Parse(Console.ReadLine());

                if (choise == 1)
                {
                    Save();
                    goto Start;
                }
                else if (choise == 2)
                {
                    Save();
                    Console.WriteLine( "Content has been saved to " + FileName );
                }
                else if (choise == 3)
                {
                    Console.Clear();
                    Console.WriteLine( "Are you sure? you are gonna lose all changes" );
                    Console.WriteLine();
                    Console.WriteLine( "\t1 - Yes, exit without saving" );
                    Console.WriteLine();
                    Console.WriteLine( "\t2 - Save and Exit" );

                    if (int.Parse( Console.ReadLine() ) == 2)
                    {
                        Save();
                    }
                }
                else
                {
                    goto Start;
                }
            }

            Console.WriteLine();
            Console.WriteLine( "Press any key to continue..." );

            Console.ReadKey( true );

            Console.Clear();
        }

        #region Writing

        private static bool OnDataReceive()
        {
            ConsoleKeyInfo keyPressed = Console.ReadKey(true);
            if (keyPressed.Key == ConsoleKey.Escape)
            {
                return false;
            }
            else if (keyPressed.Key == ConsoleKey.Enter) //When Enter is pressed
            {
                if (Row < MaxLines)
                    Enter();
            }
            else if ((char.IsLetterOrDigit( keyPressed.KeyChar ) || char.IsPunctuation( keyPressed.KeyChar ) || char.IsWhiteSpace( keyPressed.KeyChar ) || char.IsSymbol( keyPressed.KeyChar )))
            {   //When Text is pressed
                if (Col < 79)
                {
                    if (Text[Row] == null)
                    {
                        Text[Row] = string.Empty;
                    }

                    Text[Row] = Text[Row].Insert( Col , keyPressed.KeyChar.ToString() );
                    Console.SetCursorPosition( 0 , Row );
                    Console.WriteLine( Text[Row] );
                    Col++;
                }
            }
            else if (keyPressed.Key == ConsoleKey.Backspace) //When Backspace is pressed
            {
                BackSpace();
            }
            else if (keyPressed.Key == ConsoleKey.Delete) //When Delete is pressed
            {
                Delete();
            }
            else if (keyPressed.Key == ConsoleKey.UpArrow && Row > 0) // When Up/Down Arrow is pressed
            {
                Row--;
                if (Col > Text[Row].Length)
                {
                    Col = Text[Row].Length;
                }
            }
            else if (keyPressed.Key == ConsoleKey.DownArrow && Row < Text.Length - 1 && Row < MaxLines && Text[Row + 1] != null)
            {
                Row++;
                if (Col > Text[Row].Length)
                {
                    Col = Text[Row].Length;
                }
            }
            else if (keyPressed.Key == ConsoleKey.LeftArrow && Col > 0) // When Left/Right Arrow is pressed
            {
                Col--;
            }
            else if (keyPressed.Key == ConsoleKey.RightArrow && Col < Text[Row].Length && Col < 79)
            {
                Col++;
            }
            return true;
        }

        private static void Delete()
        {
            if (Col < Text[Row].Length)
            {
                Text[Row] = Text[Row].Remove( Col , 1 );
                ClearRow( Row );
                Console.SetCursorPosition( 0 , Row );
                Console.WriteLine( Text[Row] );
            }
            else if (Row != MaxLines)
            {
                Text[Row] = Text[Row] + Text[Row + 1];

                Console.SetCursorPosition( 0 , Row );
                Console.WriteLine( Text[Row] );
                Text[MaxLines + 1] = string.Empty;

                for (int i = Row + 1 ; i < MaxLines ; i++)
                {
                    Text[i] = Text[i + 1];
                    ClearRow( i );
                    Console.SetCursorPosition( 0 , i );
                    Console.WriteLine( Text[i] );
                }
            }
        }

        private static void BackSpace()
        {
            if (Col > 0)
            {
                Col--;
                Text[Row] = Text[Row].Remove( Col , 1 );
                ClearRow( Row );
                Console.SetCursorPosition( 0 , Row );
                Console.WriteLine( Text[Row] );
            }
            else if (Row > 0)
            {
                Row--;
                Col = Text[Row].Length;
                Text[Row] = Text[Row] + Text[Row + 1];

                Console.SetCursorPosition( 0 , Row );
                Console.WriteLine( Text[Row] );
                Text[MaxLines + 1] = string.Empty;

                for (int i = Row + 1 ; i < MaxLines ; i++)
                {
                    Text[i] = Text[i + 1];
                    ClearRow( i );
                    Console.SetCursorPosition( 0 , i );
                    Console.WriteLine( Text[i] );
                }
            }
        }

        private static void Enter()
        {
            if (Text[Row + 1] == null)
            {
                Text[Row + 1] = string.Empty;
            }
            else
            {
                if (Col < Text[Row].Length)
                {
                    string tmp = Text[Row].Substring(Col, Text[Row].Length - Col);
                    Text[Row] = (Col == 0 ? string.Empty : Text[Row].Substring( 0 , Col ));
                    ClearRow( Row );
                    ClearRow( Row + 1 );
                    for (int i = MaxLines + 1 ; i > Row + 1 ; i--)
                    {
                        Text[i] = Text[i - 1];
                        ClearRow( i );
                    }
                    Text[Row + 1] = tmp;
                }
                else
                {
                    Text[Row] = string.Empty;
                }

                Console.SetCursorPosition( 0 , Row );
                for (int i = Row ; i < MaxLines + 2 ; i++)
                {
                    Console.WriteLine( Text[i] );
                }
            }
            Row++;
            Col = 0;
        }

        private static void ClearRow(int row)
        {
            Console.SetCursorPosition( 0 , row );
            for (int i = 0 ; i < 79 ; i++)
            {
                Console.Write( " " );
            }
        }

        #endregion Writing

        #region File

        private static void Save()
        {
            Console.Clear();

            File.WriteAllLines( FileName , Text );
        }

        private static void FileManagement(string name)
        {
            if (name.Equals( string.Empty ))
            {
                Console.WriteLine( "Enter file's filename to open:" );
                Console.WriteLine( "If the specified file does not exist, it will be created." );
                name = Console.ReadLine().ToLower();
            }

            FileName = Kernel.pwd + name;

            try
            {
                if (!File.Exists( FileName ))
                {
                    File.Create( FileName );
                }
                else
                {
                    Text = File.ReadAllLines( FileName );
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine( ex.Message );
            }

            Console.Clear();
        }

        #endregion File
    }
}