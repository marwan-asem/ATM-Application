using My_ATM_Application.CoreProject.Eunm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace My_ATM_Application.CoreProject.Entities
{
    public class Transaction
    {
        public long TransactionID { get; set; }
        public long UserBankAccountID { get; set; }
        public DateTime TranssctionDate{ get; set; }
        public TransactionType TransactionType { get; set; }
        public string Descriprion { get; set; }
        public decimal TransactionAmount { get; set; }
    }
}
