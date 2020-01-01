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
        bool allowedSendReq(Host host);//(2)
        bool checkIfOk(GuestRequest gs, HostingUnit hu);//(3)
        bool CanChangeStatus(Order order);//(4)
        int calculateAmla(Order order);//(5)
        void updateDiary(Order order);//(6)
        void changeStatusOfOtherThings(Order order);//(7)
        bool cantDel(HostingUnit hu);//(8)
        void cantCencelAllowens(GuestRequest gs);//(9)
        void sendEmailAfterStatusChange(Order order);//(10)
        List<HostingUnit> availableUnits(DateTime d, int numOfDays);//(11)
        int numOfDaysPassed(DateTime d1, DateTime? d2 = null);//(12)
        List<Order> ordersOverTime(int numOfDays);//(13)
        List<GuestRequest> guestWithCondition(GuestReqDelegate d);//(14)
        int numOfOrders(GuestRequest gs);//(15)
        int numOfClosedOrSentOrders(HostingUnit hostU);//(16
        List<IGrouping<bool, GuestRequest>> groupGuestReqByArea(area a);//(17)
        List<IGrouping<int, GuestRequest>> groupByNumOfPeople();//(18)
        List<IGrouping<int, HostingUnit>> groupByNumOfHostingUnits();//(19)
        List<IGrouping<bool, HostingUnit>> groupHostUnitByArea(area a);//(20)




        void addGuestRequest(GuestRequest gs);//(21)
        void addHostUnit(HostingUnit hostingUnit);//(22)
        //void addOrder(Order order);//(23)
        int calculatProfit(Host host);//(24)
        int calculateTotalPriceForVication(Order order);//(25)
        List<IGrouping<int, HostingUnit>> groupByNumOfRooms();//(26)




        void addCustomerReq(GuestRequest gs);
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
