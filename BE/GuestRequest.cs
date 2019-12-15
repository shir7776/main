using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class GuestRequest
    {
        private static long GuestRequestKey ;
        private string PrivateName{ set; get; }
        private string FamilyName{ set; get; }
        private string MailAddress{ set; get; }
        private statusGusReq Status { set; get; }
        private DateTime RegistrationDate{ get; set; }
        private DateTime EntryDate{ set; get; }
        private DateTime ReleaseDate{ get; set; }
        private area Area{ set; get; }
        private subArea SubArea{ set; get; }
        private type Type{ set; get; }
        private int Adults{ set ; get; }
        private int Children{ set; get; }
        private isNecessary Pool{ set; get; }
        private isNecessary Jacuzzi { set; get; }
        private isNecessary Garden { set; get; }
        private isNecessary ChildrensAttractions { set; get; }

        public override string ToString()
        {
            return string.Format("Guest Request Key: {0}\n", GuestRequestKey)
                + string.Format("name: {0}\n", PrivateName)
                + string.Format("Family Name: {0}\n", FamilyName)
                + string.Format("Mail Address: {0}\n", MailAddress)
                + string.Format("Status: {0}\n", Status)
                + string.Format("Registration Date: {0}\n", RegistrationDate)
                + string.Format("Entry Date: {0}\n", EntryDate)
                + string.Format("Release Date: {0}\n", ReleaseDate)
                + string.Format("Area: {0}\n", Area)
                + string.Format("SubArea: {0}\n", SubArea)
                + string.Format("Type: {0}\n", Type)
                + string.Format("Number of Adults: {0}\n", Adults)
                + string.Format("Number of Children: {0}\n", Children)
                + string.Format("pool: {0}\n", Pool)
                + string.Format("Jacuzzi: {0}\n", Jacuzzi)
                + string.Format("Garden: {0}\n", Garden)
                + string.Format("ChildrensAttractions: {0}\n", ChildrensAttractions);
        }

    }
}
