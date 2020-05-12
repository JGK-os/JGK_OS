using System;
using System.IO;
using OS_Code.Core;

namespace External.Program.TextEditor
{
    public class TextEditor
    {
        #region Variables

        private static string FileName { get; set; } = string.Empty;
        private static string[] Text { get; set; } = new string[50];

        private static int  UsedLines     = 0;
        private static int  Col           = 0;
        private static int  Row           = 0;

        #endregion Variables

        public static void Start(string name)
        {
            FileManagement( name );

            bool running = true;

            while (running)
            {
                Console.SetCursorPosition( Col , Row );

                OnDataReceive();
            }

            Console.Clear();

            if (Text != null)
            {
                Save();
                Console.WriteLine( "Content has been saved to " + FileName );
            }

            Console.WriteLine( "Press any key to continue..." );
            Console.ReadKey( true );
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
                if (Row < 23)
                    Enter();
            }
            else if ((char.IsLetterOrDigit( keyPressed.KeyChar ) || char.IsPunctuation( keyPressed.KeyChar ) || char.IsWhiteSpace( keyPressed.KeyChar ) || char.IsSymbol( keyPressed.KeyChar )))
            {   //When Text is pressed
                if (Col < 79)
                {
                    if (Text[Row] == null)
                    {
                        Text[Row] = "";
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
            else if (keyPressed.Key == ConsoleKey.DownArrow && Row < Text.Length - 1 && Row < UsedLines && Text[Row + 1] != null && Row < 23)
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
            else if (Row != UsedLines)
            {
                Text[Row] = Text[Row] + Text[Row + 1];

                Console.SetCursorPosition( 0 , Row );
                Console.WriteLine( Text[Row] );
                Text[UsedLines + 1] = "";

                for (int i = Row + 1 ; i < UsedLines + 1 ; i++)
                {
                    Text[i] = Text[i + 1];
                    ClearRow( i );
                    Console.SetCursorPosition( 0 , i );
                    Console.WriteLine( Text[i] );
                }

                UsedLines--;
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
                Text[UsedLines + 1] = "";

                for (int i = Row + 1 ; i < UsedLines + 1 ; i++)
                {
                    Text[i] = Text[i + 1];
                    ClearRow( i );
                    Console.SetCursorPosition( 0 , i );
                    Console.WriteLine( Text[i] );
                }
                UsedLines--;
            }
        }

        private static void Enter()
        {
            if (Text[Row + 1] == null)
            {
                Text[Row + 1] = "";
            }
            else
            {
                if (Col < Text[Row].Length)
                {
                    string tmp = Text[Row].Substring(Col, Text[Row].Length - Col);
                    Text[Row] = (Col == 0 ? "" : Text[Row].Substring( 0 , Col ));
                    ClearRow( Row );
                    ClearRow( Row + 1 );
                    for (int i = UsedLines + 1 ; i > Row + 1 ; i--)
                    {
                        Text[i] = Text[i - 1];
                        ClearRow( i );
                    }
                    Text[Row + 1] = tmp;
                }
                else
                {
                    Text[Row] = "";
                }

                Console.SetCursorPosition( 0 , Row );
                for (int i = Row ; i < UsedLines + 2 ; i++)
                {
                    Console.WriteLine( Text[i] );
                }
            }
            UsedLines++;
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
                if (File.Exists( FileName ))
                {
                    Console.WriteLine( "Found file!" );
                }
                else if (!File.Exists( FileName ))
                {
                    Console.WriteLine( "Creating file!" );
                    File.Create( FileName );
                }

                Console.Clear();
            }
            catch (Exception ex)
            {
                Console.WriteLine( ex.Message );
            }
        }

        #endregion File
    }
}