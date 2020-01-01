using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class BankBranch
    {
        public int BankNumber { set; get; }
        public string BankName { set; get; }
        public int BranchNmber { set; get; }
        public string BranchAdress { set; get; }
        public string BranchCity { set; get; }
        public override string ToString ()
        {
            return string.Format("Bank Number: {0}\n", BankNumber) +
                string.Format("Bank Name: {0}\n", BankName) +
                string.Format("Branch Number: {0}\n", BranchNmber) +
                string.Format("Branch Adress: {0}\n", BranchAdress) +
                string.Format("Branch City: {0}\n", BranchCity);
        }
    }
}
