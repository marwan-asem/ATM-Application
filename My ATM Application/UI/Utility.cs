using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace My_ATM_Application.UI
{
    public static class Utility
    {
        private static CultureInfo culture =new CultureInfo("en-EG");
        private static long tranID;


        public static long GetTransactionID()
        {
            return ++tranID;
        }
        public static void PressEnterToContinue()
        {
            Console.WriteLine("\n\nPress Enter to continue...\n");
            Console.ReadLine();
        }
        public static string GetUserInput (string prompt)
        {
            Console.WriteLine ($"Enter {prompt}");
            return Console.ReadLine(); // get the massage form user and return it  
        }
        public static void PrintMassage (string massage, bool flag=true)
        {
            if(flag)
            {
                Console.ForegroundColor = ConsoleColor.Green;
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
            }
            Console.WriteLine(massage);
            Console.ResetColor();
            PressEnterToContinue();
        }
        public static string GetSecretInput(string prompt)
        {
            bool isPrompt = true;
            string asterics = "";
            StringBuilder input = new StringBuilder();
            while(true)
            {
                if(isPrompt)
                {
                    Console.WriteLine(prompt);
                }
                isPrompt = false;
                ConsoleKeyInfo inputKey = Console.ReadKey(true);
                if(inputKey.Key ==ConsoleKey.Enter) 
                { 
                    if(input.Length == 6) 
                    {
                        break;
                    }
                    else
                    {
                        PrintMassage("\nPlease enter 6 digits.",false);
                        isPrompt = true;
                        input.Clear();
                        continue;
                    }
                }
                if(inputKey.Key == ConsoleKey.Backspace && input.Length > 0)
                {
                    input.Remove(input.Length-1, 1); //delete last index if you press BackSpace Key 
                } 
                else if(inputKey.Key != ConsoleKey.Backspace)
                {
                    input.Append(inputKey.KeyChar);
                    Console.Write(asterics + "*");
                }

            }
            return input.ToString(); 
        }
        public static void PrintDotAimation(int timer = 10)
        {

            for (int i = 0; i < timer; i++)
            {
                Console.Write(".");
                Thread.Sleep(200);
            }
            Console.Clear();
        }
        public static string FormatAmount(decimal amount)
        {
            return string.Format(culture, "{0:c2}", amount);
        }
    }
}
