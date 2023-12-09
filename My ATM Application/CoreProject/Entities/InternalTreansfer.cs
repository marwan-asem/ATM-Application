using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace My_ATM_Application.CoreProject.Entities
{
    public class InternalTreansfer
    {
        public decimal TransferAmount { get; set; }
        public long ReciepenitBankAccountNumber { get; set; }
        public string ReciepenitBankAccountName { get; set; }
    }
}
