using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class Order
    {
        private int HostingUnitKey { set; get; }
        private int GuestRequestKey { set; get; }
        public readonly int OrderKey;

        private statusOrder Status { set; get; }
        private DateTime CreateDate { set; get; }
        private DateTime OrderDate { set; get; }
        Order()
        {
            Configurations.orderKey++;
            OrderKey= Configurations.orderKey;
        }
        public override string ToString()
        {
            return string.Format("Hosting Unit Key: {0}\n", HostingUnitKey) +
                string.Format("Guest Request Key: {0}\n", GuestRequestKey) +
                string.Format("Order Key: {0}\n", OrderKey) +
                string.Format("Status: {0}\n", Status) +
                string.Format("Create Date: {0}\n", CreateDate) +
                string.Format("OrderDate: {0}\n", OrderDate);
        }
    }
}
