using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace My_ATM_Application.CoreProject.Interfaces
{
    public interface IUserAccountAction
    {
        void PlaceDeposit();
        void CheckBalance();
        void MakeWithDrawal();
    }
}
