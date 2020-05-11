using System;
using System.IO;
using OS_Code.Core;

namespace MIV
{
    internal class MIV
    {
        private static string FileName;

        public static void printMIVStartScreen()
        {
            Console.Clear();
            Console.WriteLine( "~" );
            Console.WriteLine( "~" );
            Console.WriteLine( "~" );
            Console.WriteLine( "~" );
            Console.WriteLine( "~" );
            Console.WriteLine( "~" );
            Console.WriteLine( "~" );
            Console.WriteLine( "~                               MIV - MInimalistic Vi" );
            Console.WriteLine( "~" );
            Console.WriteLine( "~                                  version 1.2" );
            Console.WriteLine( "~                             by Denis Bartashevich" );
            Console.WriteLine( "~                            Minor additions by CaveSponge" );
            Console.WriteLine( "~                    MIV is open source and freely distributable" );
            Console.WriteLine( "~" );
            Console.WriteLine( "~                     type :help<Enter>          for information" );
            Console.WriteLine( "~                     type :q<Enter>             to exit" );
            Console.WriteLine( "~                     type :wq<Enter>            save to file and exit" );
            Console.WriteLine( "~                     press i                    to write" );
            Console.WriteLine( "~" );
            Console.WriteLine( "~" );
            Console.WriteLine( "~" );
            Console.WriteLine( "~" );
            Console.WriteLine( "~" );
            Console.WriteLine( "~" );
            Console.Write( "~" );
        }

        public static string stringCopy(string value)
        {
            string newString = string.Empty;

            for (int i = 0 ; i < value.Length - 1 ; i++)
            {
                newString += value[i];
            }

            return newString;
        }

        public static void printMIVScreen(char[] chars , int pos , string infoBar , bool editMode)
        {
            int countNewLine = 0;
            int countChars = 0;
            Console.Clear();

            for (int i = 0 ; i < pos ; i++)
            {
                if (chars[i] == '\n')
                {
                    Console.WriteLine( "" );
                    countNewLine++;
                    countChars = 0;
                }
                else
                {
                    Console.Write( chars[i] );
                    countChars++;
                    if (countChars % 80 == 79)
                    {
                        countNewLine++;
                    }
                }
            }

            Console.Write( "/" );

            for (int i = 0 ; i < 23 - countNewLine ; i++)
            {
                Console.WriteLine( "" );
                Console.Write( "~" );
            }

            //PRINT INSTRUCTION
            Console.WriteLine();
            for (int i = 0 ; i < 72 ; i++)
            {
                if (i < infoBar.Length)
                {
                    Console.Write( infoBar[i] );
                }
                else
                {
                    Console.Write( " " );
                }
            }

            if (editMode)
            {
                Console.Write( countNewLine + 1 + "," + countChars );
            }
        }

        public static string miv(string start)
        {
            bool editMode = false;
            int pos = 0;
            char[] chars = new char[2000];
            string infoBar = string.Empty;

            if (start == null)
            {
                printMIVStartScreen();
            }
            else
            {
                pos = start.Length;

                for (int i = 0 ; i < start.Length ; i++)
                {
                    chars[i] = start[i];
                }
                printMIVScreen( chars , pos , infoBar , editMode );
            }

            ConsoleKeyInfo keyInfo;

            do
            {
                keyInfo = Console.ReadKey( true );

                if (isForbiddenKey( keyInfo.Key )) continue;
                else if (!editMode && keyInfo.KeyChar == ':')
                {
                    infoBar = ":";
                    printMIVScreen( chars , pos , infoBar , editMode );
                    do
                    {
                        keyInfo = Console.ReadKey( true );
                        if (keyInfo.Key == ConsoleKey.Enter)
                        {
                            if (infoBar == ":wq")
                            {
                                string returnString = string.Empty;
                                for (int i = 0 ; i < pos ; i++)
                                {
                                    returnString += chars[i];
                                }
                                return returnString;
                            }
                            else if (infoBar == ":q")
                            {
                                return null;
                            }
                            else if (infoBar == ":help")
                            {
                                printMIVStartScreen();
                                break;
                            }
                            else
                            {
                                infoBar = "ERROR: No such command";
                                printMIVScreen( chars , pos , infoBar , editMode );
                                break;
                            }
                        }
                        else if (keyInfo.Key == ConsoleKey.Backspace)
                        {
                            infoBar = stringCopy( infoBar );
                            printMIVScreen( chars , pos , infoBar , editMode );
                        }
                        else if (keyInfo.KeyChar == 'q')
                        {
                            infoBar += "q";
                        }
                        else if (keyInfo.KeyChar == ':')
                        {
                            infoBar += ":";
                        }
                        else if (keyInfo.KeyChar == 'w')
                        {
                            infoBar += "w";
                        }
                        else if (keyInfo.KeyChar == 'h')
                        {
                            infoBar += "h";
                        }
                        else if (keyInfo.KeyChar == 'e')
                        {
                            infoBar += "e";
                        }
                        else if (keyInfo.KeyChar == 'l')
                        {
                            infoBar += "l";
                        }
                        else if (keyInfo.KeyChar == 'p')
                        {
                            infoBar += "p";
                        }
                        else
                        {
                            continue;
                        }
                        printMIVScreen( chars , pos , infoBar , editMode );
                    } while (keyInfo.Key != ConsoleKey.Escape);
                }
                else if (keyInfo.Key == ConsoleKey.Escape)
                {
                    editMode = false;
                    infoBar = string.Empty;
                    printMIVScreen( chars , pos , infoBar , editMode );
                    continue;
                }
                else if (keyInfo.Key == ConsoleKey.I && !editMode)
                {
                    editMode = true;
                    infoBar = "-- INSERT --";
                    printMIVScreen( chars , pos , infoBar , editMode );
                    continue;
                }
                else if (keyInfo.Key == ConsoleKey.Enter && editMode && pos >= 0)
                {
                    chars[pos++] = '\n';
                    printMIVScreen( chars , pos , infoBar , editMode );
                    continue;
                }
                else if (keyInfo.Key == ConsoleKey.Backspace && editMode && pos >= 0)
                {
                    if (pos > 0) pos--;

                    chars[pos] = '\0';

                    printMIVScreen( chars , pos , infoBar , editMode );
                    continue;
                }

                if (editMode && pos >= 0)
                {
                    chars[pos++] = keyInfo.KeyChar;
                    printMIVScreen( chars , pos , infoBar , editMode );
                }
            } while (true);
        }

        public static bool isForbiddenKey(ConsoleKey key)
        {
            ConsoleKey[] forbiddenKeys = { ConsoleKey.Print, ConsoleKey.PrintScreen, ConsoleKey.Pause, ConsoleKey.Home, ConsoleKey.PageUp, ConsoleKey.PageDown, ConsoleKey.End, ConsoleKey.NumPad0, ConsoleKey.NumPad1, ConsoleKey.NumPad2, ConsoleKey.NumPad3, ConsoleKey.NumPad4, ConsoleKey.NumPad5, ConsoleKey.NumPad6, ConsoleKey.NumPad7, ConsoleKey.NumPad8, ConsoleKey.NumPad9, ConsoleKey.Insert, ConsoleKey.F1, ConsoleKey.F2, ConsoleKey.F3, ConsoleKey.F4, ConsoleKey.F5, ConsoleKey.F6, ConsoleKey.F7, ConsoleKey.F8, ConsoleKey.F9, ConsoleKey.F10, ConsoleKey.F11, ConsoleKey.F12, ConsoleKey.Add, ConsoleKey.Divide, ConsoleKey.Multiply, ConsoleKey.Subtract, ConsoleKey.LeftWindows, ConsoleKey.RightWindows };
            for (int i = 0 ; i < forbiddenKeys.Length ; i++)
            {
                if (key == forbiddenKeys[i]) return true;
            }
            return false;
        }

        public static void StartMIV()
        {
            Console.WriteLine( "Enter file's filename to open:" );
            Console.WriteLine( "If the specified file does not exist, it will be created." );
            FileName = Console.ReadLine();
            try
            {
                if (File.Exists( Kernel.pwd + FileName ))
                {
                    Console.WriteLine( "Found file!" );
                }
                else if (!File.Exists( Kernel.pwd + FileName ))
                {
                    Console.WriteLine( "Creating file!" );
                    File.Create( Kernel.pwd + FileName );
                }
                Console.Clear();
            }
            catch (Exception ex)
            {
                Console.WriteLine( ex.Message );
            }

            string text = string.Empty;
            Console.WriteLine( "Do you want to open " + FileName + " content? (Yes/No)" );
            if (Console.ReadLine().ToLower() == "yes" || Console.ReadLine().ToLower() == "y")
            {
                text = miv( File.ReadAllText( Kernel.pwd + FileName ) );
            }
            else
            {
                text = miv( null );
            }

            Console.Clear();

            if (text != null)
            {
                File.WriteAllText( Kernel.pwd + FileName , text );
                Console.WriteLine( "Content has been saved to " + FileName );
            }
            Console.WriteLine( "Press any key to continue..." );
            Console.ReadKey( true );
        }
    }
}