using My_ATM_Application.Application;
using My_ATM_Application.CoreProject.Entities;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace My_ATM_Application.UI
{
    public class AppScreen
    {

        internal static void Welcome()
        {
          
            Console.Clear();
            Console.Title = "My ATM App";
            Console.ForegroundColor = ConsoleColor.White;
            #region Hallo
            Console.BackgroundColor=ConsoleColor.Magenta;
            Console.WriteLine("\n-----------------Welcome to My ATM App-----------------\n\n");
            Console.BackgroundColor = ConsoleColor.Black;
            #endregion Hallo
            Console.WriteLine("Please insert your ATM card");
            #region Note


            Console.ForegroundColor= ConsoleColor.Blue;
            Console.WriteLine("Note: Actual ATM machine will accept and validate" +
                " a physical ATM card, read the card number and validate it.");
            Console.ResetColor();
            #endregion Note
            Utility.PressEnterToContinue();
        }
        internal static UserAccount UserLoginForm()
        {
            UserAccount TempUserAccount = new UserAccount();
            TempUserAccount.CardNumber = Vaildator.Convert<long>("Your Card Number");
            TempUserAccount.CardPin = Convert.ToInt32(Utility.GetSecretInput("Enter Your Card PIN "));
            return TempUserAccount; 
        }
        internal static  void LoginProgress()
        {
            Console.Write("\nChecking Card Number And PIN");
            Utility.PrintDotAimation();

        }
        internal static void PrintLockScreen()
        {
            Console.Clear();
            Utility.PrintMassage("Your Account is locked. Please go to " +
                "the nearset branch to unlock Your Account,\n\nPlease Collect Your Card .", false);
            // Utility.PressEnterToContinue() ;
            //Environment.Exit(1);// this an error process not complete "this to OS "
        }
        internal static void WelcomeCustomer(string fullname)
        {
            Utility.PrintMassage($"Welcome back, {fullname}", true);
           // Utility.PressEnterToContinue();

        }
        internal static void DisplayAppMenu()
        {
            Console.Clear();
            Console.WriteLine("-------My ATM App Menu-------");
            Console.WriteLine(":                           :");
            Console.WriteLine("1. Account Balance          :");
            Console.WriteLine("2. Cash Deposit             :");
            Console.WriteLine("3. Withdrawal               :");
            Console.WriteLine("4. Transfer                 :");
            Console.WriteLine("5. Transactions             :");
            Console.WriteLine("6. Logout                   :");
            Console.WriteLine("\n");
        }
        internal static void LogOutProgress()
        {
            Console.Write("Thank You for using My ATM App.");
            Utility.PrintDotAimation();
            Console.Clear();
        }
        internal const string cur = "EGP ";
        internal static int SelectAmount()
        {
            Console.WriteLine("");
            Console.WriteLine(":1.{0}50       5.{0}1,000", cur);
            Console.WriteLine(":2.{0}100      6.{0}2,000", cur);
            Console.WriteLine(":3.{0}200      7.{0}5,000", cur);
            Console.WriteLine(":4.{0}500      8.{0}10,000", cur);
            Console.WriteLine(":0.Other");
            Console.WriteLine("");
            int selectAmount = Vaildator.Convert<int>("Option:");
            switch(selectAmount)
            {
                case 1:
                    return 50;
                    break;

                case 2:
                    return 100;
                    break;
                case 3:
                    return 200;
                    break;
                case 4:
                    return 500;
                    break;
                case 5:
                    return 1000;
                    break;
                case 6:
                    return 2000;
                    break;
                case 7:
                    return 5000;
                    break;
                case 8:
                    return 10000;
                    break;
                case 0:
                    return 0;
                    break;
                default:
                    Utility.PrintMassage("Invaild Input. Try again.", false);
                   // SelectAmount();
                    return -1;
                    break;


            }

        }
        internal InternalTreansfer InternalTreansferForm()
        {
            var _InternalTransfer= new InternalTreansfer();
            _InternalTransfer.ReciepenitBankAccountNumber = Vaildator.Convert<long>("Reciepenit's Account Number:");
            _InternalTransfer.TransferAmount = Vaildator.Convert<decimal>($"amount {cur} ");
            _InternalTransfer.ReciepenitBankAccountName = Utility.GetUserInput("Reciepenit's Account Name:");
            return _InternalTransfer;
        }

    }
}
