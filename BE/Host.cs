using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class Host
    {
        public readonly int HostKey;
        Host()
        {
            Configurations.hostKey++; 
            HostKey = Configurations.hostKey++;
        }
        private string PrivateName { set; get; }
        private string FamilyName { set; get; }
        private int PhoneNumber { set; get; }
        private string MailAdress { set; get; }
        private BankAccount account { set; get; }
        private bool CollectionClearance { set; get; }
        public override string ToString()
        {
            return string.Format("Host Key: {0}\n", HostKey) +
                string.Format("Private Name: {0}\n", PrivateName) +
                string.Format("Family Name: {0}\n", FamilyName) +
                string.Format("Phone Number: {0}\n", PhoneNumber) +
                string.Format("Mail Adress: {0}\n", MailAdress) +
                string.Format("Bank Account: {0}\n", account) +
                string.Format("Collection Clearance: {0}\n", PrivateName);
        }
    }
}
