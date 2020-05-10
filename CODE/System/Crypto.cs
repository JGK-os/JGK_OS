using System;
using System.IO;

namespace OS
{
    internal class Crypto
    {
        public static void Login()
        {
            while (true)
            {
                Console.Write( "Username : " );
                string user = Console.ReadLine();

                Console.Write( "Password " );
                Console.ForegroundColor = ConsoleColor.Black;
                string password = Console.ReadLine();
                Console.ForegroundColor = ConsoleColor.White;
                if (password == GetPassword( "0:\\System\\users" , user ))
                {
                    Kernel.username = user;
                    break;
                }
            }
        }

        private static string GetPassword(string path , string user)
        {
            string[] file = File.ReadAllLines(path);

            for (int i = 0 ; i < file.Length ; i++)
            {
                string line = file[i];
                if (line.Split( ":" )[0] == user)
                {
                    return line.Split( ":" )[2];
                }
            }

            return "GhezziShouldChange"; //Ghezzi change this
        }
    }
}