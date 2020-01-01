using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE;

namespace BL
{
    public delegate bool GuestReqDelegate(GuestRequest gs);
    public interface IBL//16
    {
        bool isDateValid(DateTime start, DateTime end);//(1)
        bool allowedSendReq(Order order);//(2)
        bool checkIfOk(GuestRequest gs, HostingUnit hu);//(3)
        bool CanChangeStatus(Order order);//(4)
        int calculateAmla(Order order);//(5)
        void updateDiary(Order order);//(6)
        void changeStatusOfOtherThings(Order order);//(7)
        bool cantDel(HostingUnit hu);//(8)
        bool canCencelAllowens(Host host);//(9)
        void sendEmailAfterStatusChange(Order order);//(10)
        List<HostingUnit> availableUnits(DateTime d, int numOfDays);//(11)
        int numOfDaysPassed(DateTime d1, DateTime? d2 = null);//(12)
        List<Order> ordersOverTime(int numOfDays);//(13)
        List<GuestRequest> guestWithCondition(GuestReqDelegate d);//(14)
        int numOfOrders(GuestRequest gs);//(15)
        int numOfClosedOrSentOrders(HostingUnit hostU);//(16
        List<IGrouping<area, GuestRequest>> groupGuestReqByArea();//(17)
        List<IGrouping<int, GuestRequest>> groupByNumOfPeople();//(18)
        List<IGrouping<int, Host>> groupByNumOfHostingUnits();//(19)
        List<IGrouping<area, HostingUnit>> groupHostUnitByArea();//(20)




        //int calculatProfit(Host host);//(21)
        //int calculateTotalPriceForVication(Order order);//(22)
        //List<IGrouping<int, HostingUnit>> groupByNumOfRooms();//(23)
        //List<HostingUnit> availableForTomorrow();//(24)
        //List<HostingUnit> byHost(Host host);//(25)
        //



        void addCustomerReq(GuestRequest gs);//()
        void updateCustomerReq(long gsKey, statusGusReq stat);
        List<GuestRequest> getGuestRequestList();
        void addHostingUnit(HostingUnit hostunit);
        void deleteHostingUnit(HostingUnit HstUnt);
        void UpdateHostingUnit(HostingUnit HstUnt);
        List<HostingUnit> getHostingUnitList();
        void addOrder(Order ord);
        void updateOrder(long orKey, statusOrder statO);
        List<Order> getOrderList();
        List<BankBranch> getBankBranches();
    }
}
