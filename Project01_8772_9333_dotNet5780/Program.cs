using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE;
using DAL;

namespace Project01_8772_9333_dotNet5780
{
    /// <summary>
    /// בדיקה של המחלקה DAL בדיקה זמנית
    /// </summary>
    class Program
    {
        public static Idal dal = FactoryDal.getDal();
        static void Main(string[] args)
        {
            bool[,] diar = new bool[12, 31];
            for (int i = 0; i < 12; i++)
                for (int j = 0; j < 31; j++)
                    if (i + j < 10)
                        diar[i, j] = true;
            HostingUnit hu = new HostingUnit { HostingUnitName = "shoclat", diary = diar };
            dal.addHostingUnit(hu);
            List<HostingUnit> lhu = dal.getHostingUnitList();
            HostingUnit hun = new HostingUnit { HostingUnitKey = hu.HostingUnitKey, HostingUnitName = "bamba" };
            dal.UpdateHostingUnit(hun);
            lhu = dal.getHostingUnitList();
            //dal.deleteHostingUnit(hu);
            lhu = dal.getHostingUnitList();
            BE.GuestRequest gs = new GuestRequest { PrivateName = "hodaya rachel", FamilyName = "bismut", Status = statusGusReq.פתוח };
            dal.addCustomerReq(gs);
            List<GuestRequest> lgs = dal.getGuestRequestList();
            statusGusReq st = statusGusReq.נסגר_על_ידי_האתר;
            dal.updateCustomerReq(gs.GuestRequestKey,st);
            lgs = dal.getGuestRequestList();
            Order order = new Order { HostingUnitKey = hu.HostingUnitKey,Status = statusOrder.טרם_טופל ,CreateDate = new DateTime(2020, 12, 23),OrderDate=new DateTime(2020,12,25) };
            dal.addOrder(order);
            List<Order> ord = dal.getOrderList();
            statusOrder SO = statusOrder.נשלח_מייל;
            dal.updateOrder(order.OrderKey, SO);
            ord = dal.getOrderList();


        }
    }
}
