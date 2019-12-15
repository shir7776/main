using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE;

namespace DAL
{
    interface Idal
    {
        #region customer reques func
        void addCustomerReq(GuestRequest gs);
         void updateCustomerReq(long GstReqKey);
        #endregion
        #region hosting unit func
        void addHostingUnit(HostingUnit hostunit);
         void deleteHostingUnit(int HstUntKey);
         void UpdateHostingUnit(int HstUntKey);
        #endregion
        #region order func
        void addOrder(Order ord);
         void updateOrder(int OrdKey);
        #endregion
        List<HostingUnit> getHostingUnitList();
        List<GuestRequest> getGuestRequestList();
        List<Order> getOrderList();
        List<BankAccount> getBankBranches();
    }
}
