using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class BankAccount
    {
        private int BankNumber { set; get; }
        private string BankName { set; get; }
        private int BranchNmber { set; get; }
        private string BranchAdress { set; get; }
        private string BranchCity { set; get; }
        private int BankAccountNumber { set; get; }
        public override string ToString ()
        {
            return string.Format("Bank Number: {0}\n", BankNumber) +
                string.Format("Bank Name: {0}\n", BankName) +
                string.Format("Branch Number: {0}\n", BranchNmber) +
                string.Format("Branch Adress: {0}\n", BranchAdress) +
                string.Format("Branch City: {0}\n", BranchCity) +
                string.Format("Bank Account Number: {0}\n", BankAccountNumber);
        }
    }
}
