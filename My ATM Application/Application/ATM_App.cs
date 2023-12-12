using My_ATM_Application.CoreProject.Entities;
using My_ATM_Application.CoreProject.Eunm;
using My_ATM_Application.CoreProject.Interfaces;
using My_ATM_Application.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleTables; 
namespace My_ATM_Application.Application
{
    public class ATM_App : IUserLogin, IUserAccountAction , ITransaction
    {
        private List<UserAccount> userAccountList;
        private UserAccount selectedAccount;
        private List<Transaction>_listOfTransactions;
        private const decimal minimunAmount = 100;
        private readonly AppScreen screen;
        // to i can use stiatic method
        private void ProcessInternalTransfer(InternalTreansfer _internalTransfer)
        {
            if (_internalTransfer.TransferAmount <= 0)
            {
                Utility.PrintMassage("Amount needs to be more than zero.Try again.", false);
                return;
            }
            //check sender account's balance
            if(_internalTransfer.TransferAmount > selectedAccount.AccountBalance)
            {
                Utility.PrintMassage($"Transfer Faild. You don't have enough balance to transfer {_internalTransfer.TransferAmount}", false);
                return; 
            }
            //check the minimun kept amount
            if (selectedAccount.AccountBalance - _internalTransfer.TransferAmount < minimunAmount)
            {
                Utility.PrintMassage($"Transfer Faild.Your account needs to have minimum {Utility.FormatAmount(minimunAmount)}",false);
                return;
            }
            // this use Linq 
            //check reciever's account number is valid
            var selectedBankAccountReciever = (from userAcc in userAccountList
                                               where userAcc.AccountNumber == _internalTransfer.ReciepenitBankAccountNumber
                                               select userAcc).FirstOrDefault();

            if(selectedBankAccountReciever == null)
            {
                Utility.PrintMassage("Transfer Failed. Reciever bank account number is Invaild.", false);
                return;
            }
            //check reciever's account name is valid
            if(selectedBankAccountReciever.FullName!=_internalTransfer.ReciepenitBankAccountName) 
            {
                Utility.PrintMassage("Transfer Failed. Reciever bank account name is Invaild.", false);
                return;
            }

            // add tramsaction to transaction record sender
            InsertTransaction(selectedAccount.Id, TransactionType.Transfer, -1 * _internalTransfer.TransferAmount,
                $"Transfered to {selectedBankAccountReciever.AccountNumber} ({selectedBankAccountReciever.FullName})");

            selectedAccount.AccountBalance -= _internalTransfer.TransferAmount;

            InsertTransaction(selectedBankAccountReciever.Id, TransactionType.Transfer, _internalTransfer.TransferAmount,
                $"Transfered form {selectedAccount.AccountNumber} ({selectedAccount.FullName})");

            //Update Reciver
            selectedBankAccountReciever.AccountBalance += _internalTransfer.TransferAmount;
            Utility.PrintMassage($"You have Sucessfully Transfered {Utility.FormatAmount(_internalTransfer.TransferAmount)}",true);

        }
        public ATM_App()
        {
            screen = new AppScreen();
        }


        public void InitializeData()
        {
            userAccountList = new List<UserAccount>()
            {
                 new UserAccount{Id=1, FullName = "Marwan Asem", AccountNumber=123456,CardNumber =123456, CardPin=123456,AccountBalance=50000.00m,IsLocked=false},
                new UserAccount{Id=2, FullName = "Omar Ail", AccountNumber=234567,CardNumber =234567, CardPin=234567,AccountBalance=40000.00m,IsLocked=false},
                new UserAccount{Id=3, FullName = "Mohamed Nabway", AccountNumber=345678,CardNumber =345678, CardPin=345678,AccountBalance=20000.00m,IsLocked=true},
            };
            _listOfTransactions = new List<Transaction>();

        }


        public void Run()
        {
            AppScreen.Welcome();
            CheckUserCardNumAndPassword();
            AppScreen.WelcomeCustomer(selectedAccount.FullName);
            while (true)
            {

                AppScreen.DisplayAppMenu();
                ProcessMenuOption();
            }

        }


        public void CheckUserCardNumAndPassword()
        {
            bool isCorrectLogin = false;
            while (isCorrectLogin == false)
            {


                UserAccount intputAccount = AppScreen.UserLoginForm();

                AppScreen.LoginProgress();

                foreach (UserAccount userAccount in userAccountList)
                {

                    selectedAccount = userAccount;

                    if (intputAccount.CardNumber == selectedAccount.CardNumber)
                    {

                        selectedAccount.TotalLogin++;

                        if (intputAccount.CardPin == selectedAccount.CardPin)
                        {
                            selectedAccount = userAccount;
                          
                            if (selectedAccount.IsLocked || selectedAccount.TotalLogin > 3)
                            {
                                AppScreen.PrintLockScreen();
                                AppScreen.LogOutProgress();
                                Run();
                            }
                            

                            else
                            {
                                selectedAccount.TotalLogin = 0;
                                isCorrectLogin = true;
                                break;
                            }
                        }
                    }

                }
                if (isCorrectLogin == false)
                {
                    Utility.PrintMassage("\nInvaild Card Number Or PIN", false);
                    //if(selectedAccount.TotalLogin == 3) selectedAccount.IsLocked = true;
                    selectedAccount.IsLocked = (selectedAccount.TotalLogin == 3);
                    if (selectedAccount.IsLocked)
                    {
                        AppScreen.PrintLockScreen();
                    }
                }
                Console.Clear();

            }

        }


        private void ProcessMenuOption()
        {
            switch (Vaildator.Convert<int>("An Option:"))
            {
                case (int)AppMenu.CheckBalance:
                    CheckBalance();
                    break;
                case (int)AppMenu.PlaceDeposit:
                    PlaceDeposit();
                    break;

                case (int)AppMenu.MakeWithDrawal:
                    MakeWithDrawal();
                    break;
                case (int)AppMenu.InternalTransfer:
                    var internalTransfer = screen.InternalTreansferForm();
                    ProcessInternalTransfer(internalTransfer);
                    break;

                case (int)AppMenu.ViewTransaction:
                    ViewTransaction();
                    break;
                case (int)AppMenu.Logout:
                    AppScreen.LogOutProgress();
                    Utility.PrintMassage("\nYou Have Sucessfully logged out . Please Collect Your Card .");
                    Run();
                    break;
                default:
                    Utility.PrintMassage("Invaild Option", false);
                    break;

            }
        }

        public void CheckBalance()
        {
            Utility.PrintMassage($"Your Accont Balance is : {Utility.FormatAmount(selectedAccount.AccountBalance)}");
        }
        public void MakeWithDrawal()
        {
            var transaction_amount = 0;
            int _selecetAmount = AppScreen.SelectAmount();
            if(_selecetAmount == -1) 
            {
                // _selecetAmount=AppScreen.SelectAmount();
                MakeWithDrawal();
                return;
            }
            else if(_selecetAmount != 0) 
            {
                transaction_amount=_selecetAmount;
            }
            else
            {
                transaction_amount = Vaildator.Convert<int>($"amount {AppScreen.cur}");
            }

            //input vaildation 
            if(transaction_amount <= 0)
            {
                Utility.PrintMassage("Amount needs to be greater than 0 . Try again",false); 

            }
            if (transaction_amount %50!=0) 
            {
                Utility.PrintMassage("You can only withdraw amount in mulitples of 50 ,100 or 200 EGP . Try again", false);
                return;
            }
            //Business logic vaildation
            if(transaction_amount > selectedAccount.AccountBalance)
            {
                Utility.PrintMassage($"Withdrawal Failed .Your balance it too low to withdrawal {Utility.FormatAmount(transaction_amount)}", false);
                return;
            }
            if((selectedAccount.AccountBalance-transaction_amount) < minimunAmount)
            {
                Utility.PrintMassage($"Withdrawal Failed .Your account needs to have minimun  {Utility.FormatAmount(minimunAmount)}", false);
                return;
            }
            InsertTransaction(selectedAccount.Id, TransactionType.WithDrawal,-1*transaction_amount,"");
            //Update account balance 
            selectedAccount.AccountBalance -= transaction_amount;
            //sucess massage
            Utility.PrintMassage($"You have successfully withdrawn {Utility.FormatAmount(transaction_amount)}", true);


        }

        public void PlaceDeposit()
        {
            Console.WriteLine("\nOnly multiple of 50, 100 and 200 EGP allowed.");
            var _transactionAmount = Vaildator.Convert<int>($"amount {AppScreen.cur}");
            Console.Write("\nChecking And Counting bank notes.");
            Utility.PrintDotAimation();
            Console.WriteLine("");

            if (_transactionAmount <= 0)
            {
                Utility.PrintMassage("Amount need to be Greater than zero .Try Again", false);
                return;
            }
            if(_transactionAmount %50!=0)
            {
                Utility.PrintMassage("Enter Deposit amount in mulitple of 50 ,100 or 200 . Try Again", false);
                return;
            }
            if(PreviewBankNotesCount(_transactionAmount)==false)
            {
                Utility.PrintMassage("You have cancelled your action", false);
                return;

            }
            InsertTransaction(selectedAccount.Id, TransactionType.Deposit, _transactionAmount, "");
            //update account balance
            selectedAccount.AccountBalance += _transactionAmount;
            //print sucess massage 
            Utility.PrintMassage($"Your deposit of {Utility.FormatAmount(_transactionAmount)} Was Sucessful",true);





        }
        private bool PreviewBankNotesCount(int amount)
        {
            int _amount=amount;
            int TwoHundredPound = _amount / 200;
            _amount %= 200;
            int OneHundedPound = (_amount) / 100;
            _amount %= 100;
            int FitiyPound = _amount / 50;
            Console.WriteLine("\nSummary");
            Console.WriteLine("-------------");
            Console.WriteLine($"{AppScreen.cur}200 X {TwoHundredPound} = {200 * TwoHundredPound}");
            Console.WriteLine($"{AppScreen.cur}100 X {OneHundedPound} = {100 * OneHundedPound}");
            Console.WriteLine($"{AppScreen.cur}50 X {FitiyPound} = {50 * FitiyPound}");
            Console.WriteLine($"Total amount: {Utility.FormatAmount(amount)}\n\n");
            int option = Vaildator.Convert<int>("1 to confirm");
            return option.Equals(1);

        }



        public void InsertTransaction(long userBankAccountID, TransactionType transType, decimal transAmount, string desc)
        {
            //create a new transaction obj
            var transaction = new Transaction()
            {
                TransactionID = Utility.GetTransactionID(),
                UserBankAccountID = userBankAccountID,
                TransactionAmount = transAmount,
                TransactionType = transType,
                TranssctionDate = DateTime.Now,
                Descriprion = desc
            };
            // add transaction obj to the list
            _listOfTransactions.Add(transaction);

        }

        public void ViewTransaction()
        {
            var filteredTransactionList = _listOfTransactions.Where(t => t.UserBankAccountID == selectedAccount.Id).ToList();
            //check if theres's a transactions
            if( filteredTransactionList.Count <=0 )
            {
                Utility.PrintMassage("You Have No Transaction Yet.", true);

            }
            else
            {
                //here i installed NuGet Packetge called "ConsoleTables "
                //this is the link : https://github.com/khalidabuhakmeh/ConsoleTables
                var table = new ConsoleTable("ID", "Transaction Date", "Type", "Description", "Amount" + AppScreen.cur);
                 foreach( var transaction in filteredTransactionList )
                {
                    table.AddRow(transaction.TransactionID,transaction.TranssctionDate ,transaction.TransactionType,transaction.Descriprion,transaction.TransactionAmount);

                }
                table.Options.EnableCount = false;
                table.Write();
                Utility.PrintMassage($"You have {filteredTransactionList.Count} transaction(s)",true);
            }
        }


    }
}
