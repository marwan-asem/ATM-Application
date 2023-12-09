using My_ATM_Application.CoreProject.Eunm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace My_ATM_Application.CoreProject.Interfaces
{
    public interface ITransaction
    {
        void InsertTransaction(long userBankAccountID, TransactionType transType, decimal transAmount, string desc);
        void ViewTransaction();

    }
}
