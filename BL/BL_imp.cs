
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE;
using DAL;

namespace BL
{

    class BL_imp : IBL
    {
        public static Idal dal = FactoryDal.getDal();
        /// <summary>
        /// פונקצית עזר
        /// </summary>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <param name="hu"></param>
        /// <returns></returns>
        public bool dateOk(DateTime start, DateTime end, HostingUnit hu)
        {
            int i = start.Month - 1, k = end.Month - 1;
            int j = start.Day - 1, s = end.Day - 1;
            while (i != k || j != s)
            {
                if (hu.diary[i, j] == true)
                    return false;
                j++;
                if (j == 31)
                {
                    j = 0;
                    i++;
                    if (i == 11)
                        i = 0;
                }
            }
            return true;
        }
        /// <summary>
        /// func to check if date is valid
        /// </summary>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        public bool isDateValid(DateTime start, DateTime end)//(1)
        {
            return start < end;
        }
        /// <summary>
        /// a func to check if host can send an email to a client
        /// </summary>
        /// <param name="host"></param>
        /// <returns></returns>
        public bool allowedSendReq(Order order)//(2)//לממש
        {
            var hostingU = (from hosUn in dal.getHostingUnitList()
                            where (order.HostingUnitKey == hosUn.HostingUnitKey)
                            select hosUn).FirstOrDefault();
            if (hostingU.Owner.CollectionClearance)
                return true;
            return false;
        }
        /// <summary>
        /// a func to check that all the dates are available from guest request
        /// </summary>
        /// <param name="gs"></param>
        /// <param name="hu"></param>
        /// <returns></returns>
        public bool checkIfOk(GuestRequest gs, HostingUnit hu)//(3)
        {
            return dateOk(gs.EntryDate, gs.ReleaseDate, hu);

        }
        /// <summary>
        /// a func to check the status of an order and if it close returns false-you cant change the status anymore
        /// </summary>
        /// <param name="order"></param>
        /// <returns></returns>
        public bool CanChangeStatus(Order order)//(4)
        {
            return !(order.Status == statusOrder.נסגר_מחוסר_הענות_של_הלקוח || order.Status == statusOrder.נסגר_בהיענות_של_לקוח);
        }
        /// <summary>
        /// a func to calculate the amla that needs to be paid to the host
        /// </summary>
        /// <param name="order"></param>
        /// <returns></returns>
        public int calculateAmla(Order order)//(5)
        {
            GuestRequest gs = (from g in dal.getGuestRequestList()
                               where (g.GuestRequestKey == order.GuestRequestKey)
                               select g).FirstOrDefault();
            return (gs.ReleaseDate - gs.EntryDate).Days * Configurations.amla;
        }
        /// <summary>
        /// func to update diary with a given order
        /// </summary>
        /// <param name="order"></param>
        public void updateDiary(Order order)//(6)
        {
            GuestRequest gs = dal.getGuestRequestList().Find(g => g.GuestRequestKey == order.GuestRequestKey);
            HostingUnit hu = dal.getHostingUnitList().Find(hosu => hosu.HostingUnitKey == order.HostingUnitKey);
            int i = gs.EntryDate.Month - 1, k = gs.ReleaseDate.Month - 1;
            int j = gs.EntryDate.Day - 1, s = gs.ReleaseDate.Day - 1;
            while (i != k && j != s)
            {
                hu.diary[i, j] = true;
                j++;
                if (j == 31)
                {
                    j = 0;
                    i++;
                    if (i == 11)
                        i = 0;
                }
            }

        }
        /// <summary>
        /// a func that change status of other orders from the same client and his guest request
        /// </summary>
        /// <param name="order"></param>
        public void changeStatusOfOtherThings(Order order)//(7)
        {
            var guestReq = (from gs in dal.getGuestRequestList()
                            where (order.GuestRequestKey == gs.GuestRequestKey)
                            select gs).FirstOrDefault();
            guestReq.Status = statusGusReq.נסגר_על_ידי_האתר;
            var listOrd = (from ord in dal.getOrderList()
                           where (ord.GuestRequestKey == order.GuestRequestKey)
                           select ord).ToList();
            foreach (Order ord in listOrd)//האם מותר לעבור כאן עם FOREACH 
                ord.Status = order.Status;
        }
        /// <summary>
        /// a func to check if there is an open order for a hosting unit if there is- the host cant delete it
        /// </summary>
        /// <param name="hu"></param>
        /// <returns></returns>
        public bool cantDel(HostingUnit hu)//(8)
        {
            List<Order> lorder = dal.getOrderList();
            var isThere = (from order in lorder
                           where (hu.HostingUnitKey == order.HostingUnitKey)
                           select new { order }).FirstOrDefault();
            return isThere == null;
        }
        /// <summary>
        /// if there is an open 
        /// </summary>
        /// <param name="gs"></param>
        public bool canCencelAllowens(Host host)//(9)
        {
            var ordList = (from order in dal.getOrderList()
                           from hu in dal.getHostingUnitList()
                           where order.HostingUnitKey == hu.HostingUnitKey && hu.Owner.HostKey == host.HostKey && order.Status == statusOrder.טרם_טופל
                           select order).ToList();
            return !ordList.Any();
        }

        /// <summary>
        /// a func to send an enail after status change to נשלח_מייל
        /// </summary>
        /// <param name="order"></param>
        public void sendEmailAfterStatusChange(Order order)//(10)
        {
            if (order.Status == statusOrder.נשלח_מייל)
                Console.WriteLine(order);
        }
        /// <summary>
        /// a func that retuns all the available units in a particular dates
        /// </summary>
        /// <param name="d"></param>
        /// <param name="numOfDays"></param>
        /// <returns></returns>
        public List<HostingUnit> availableUnits(DateTime d, int numOfDays)//(11)
        {
            DateTime end = d.AddDays(numOfDays);
            List<HostingUnit> hostUnit = dal.getHostingUnitList().FindAll(hu => dateOk(d, end, hu));
            return hostUnit;
        }
        /// <summary>
        /// a func that returns the number of days between two date or between a date until now
        /// </summary>
        /// <param name="d1"></param>
        /// <param name="d2"></param>
        /// <returns></returns>
        public int numOfDaysPassed(DateTime d1, DateTime? d2 = null)//(12)
        {
            if (d2 == null)
            {
                DateTime today = new DateTime(2020, (DateTime.Today).Month, (DateTime.Today).Day);
                return (d1 - today).Days;
            }
            DateTime d3 = (DateTime)d2;
            return (d1 - d3).Days;
        }
        /// <summary>
        /// a func that returns a list of orders who created before a given number of days
        /// </summary>
        /// <param name="numOfDays"></param>
        /// <returns></returns>
        public List<Order> ordersOverTime(int numOfDays)//(13)
        {
            var order = (from ord in dal.getOrderList()
                         where (DateTime.Today - ord.CreateDate).Days >= numOfDays || (DateTime.Today - ord.OrderDate).Days >= numOfDays
                         select ord).ToList();
            return order;
        }
        /// <summary>
        /// abstract func that returns all the guest request how answer to a sertain condition
        /// </summary>
        /// <param name="d"></param>
        /// <returns></returns>
        public List<GuestRequest> guestWithCondition(GuestReqDelegate d)//(14)
        {
            var guesReq = (from gs in dal.getGuestRequestList()
                           where d(gs)
                           select gs).ToList();
            return guesReq;
        }
        /// <summary>
        /// a func that returns the number of all the orders that abstract guest request has
        /// </summary>
        /// <param name="gs"></param>
        /// <returns></returns>
        public int numOfOrders(GuestRequest gs)//(15)
        {
            List<Order> listOrd = dal.getOrderList().FindAll(o => o.GuestRequestKey == gs.GuestRequestKey);
            return listOrd.Count();
        }
        /// <summary>
        /// a func that returns the number of orders who werte sent or closed successfuly
        /// </summary>
        /// <param name="hostU"></param>
        /// <returns></returns>
        public int numOfClosedOrSentOrders(HostingUnit hostU)//(16)
        {
            List<Order> listOrd = dal.getOrderList().FindAll(o => o.HostingUnitKey == hostU.HostingUnitKey && (o.Status == statusOrder.נסגר_בהיענות_של_לקוח || o.Status == statusOrder.נשלח_מייל));
            return listOrd.Count();
        }
        #region func by grouping
        /// <summary>
        /// a func that returns a list of groups of guest request by sertain area
        /// </summary>
        /// <param name="a"></param>
        /// <returns></returns>
        public List<IGrouping<area, GuestRequest>> groupGuestReqByArea()//(17)
        {
            var listByArea = (from gs in dal.getGuestRequestList()
                              group gs by gs.Area).ToList();
            return listByArea;
        }
        /// <summary>
        /// a func that returns a list of groups of guest request by number of peaople in vication
        /// </summary>
        /// <param ></param>
        /// <returns></returns>
        public List<IGrouping<int, GuestRequest>> groupByNumOfPeople()//(18)
        {
            var listByNum = (from gs in dal.getGuestRequestList()
                             group gs by gs.Adults + gs.Children).ToList();
            return listByNum;
        }
        /// <summary>
        /// a func that return a list of groups host unit by number of hosting units 
        /// </summary>
        /// <param name="numOfHostingUnits"></param>
        /// <returns></returns>
        public List<IGrouping<int, Host>> groupByNumOfHostingUnits()//(19)
        {
            var listByNumOfUnit = (from hu in dal.getHostingUnitList()
                                   group hu.Owner by hu.Owner.NumOfHostingUnit).ToList();
            return listByNumOfUnit;
        }
        /// <summary>
        /// a func that returns a list of groups of hosting unit by area
        /// </summary>
        /// <param name="a"></param>
        /// <returns></returns>
        public List<IGrouping<area, HostingUnit>> groupHostUnitByArea()//(20)
        {
            var listByArea = (from gs in dal.getHostingUnitList()
                              group gs by gs.areaOfUnit).ToList();
            return listByArea;
        }
        #endregion

        /// <summary>
        /// a func that add guest request after checking the edges
        /// </summary>
        /// <param name="gs"></param>
        public void addCustomerReq(GuestRequest gs)//מה צריך לבדןק? איך בודקים מייל ואיך בודקים אם ההמפתח קיים אם הוא עוד לא מאותחל
        {
            if (isDateValid(gs.Clone().EntryDate, gs.Clone().ReleaseDate))
            {
                gs.RegistrationDate = new DateTime(2020, (DateTime.Today).Month, (DateTime.Today).Day);
                dal.addCustomerReq(gs);
            }
        }
        /// <summary>
        /// a func to update guest request
        /// </summary>
        /// <param name="gsKey"></param>
        /// <param name="stat"></param>
        public void updateCustomerReq(long gsKey, statusGusReq stat)
        {
            if (!(dal.getGuestRequestList().Any(gs1 => (gs1.GuestRequestKey == gsKey))))
                throw new System.ArgumentException("request dont exist!");
            if (stat != statusGusReq.נסגר_כי_פג_תוקף && stat != statusGusReq.נסגר_על_ידי_האתר && stat != statusGusReq.פתוח)
                throw new System.ArgumentException("dont have a status");
            dal.updateCustomerReq(gsKey, stat);
        }
        /// <summary>
        /// a func that returns the guest request list
        /// </summary>
        /// <returns></returns>
        public List<GuestRequest> getGuestRequestList()
        {
            return dal.getGuestRequestList();
        }
        /// <summary>
        /// a func that adds a new host unit to the list
        /// </summary>
        /// <param name="hostunit"></param>
        public void addHostingUnit(HostingUnit hostunit)
        {
            dal.addHostingUnit(hostunit);
        }
        /// <summary>
        /// a func that deletes a host unit from the list
        /// </summary>
        /// <param name="HstUnt"></param>
        public void deleteHostingUnit(HostingUnit HstUnt)
        {
            if (!(dal.getHostingUnitList().Any(hu => (hu.HostingUnitKey == HstUnt.HostingUnitKey))))
                throw new System.ArgumentException("hosting unit dont exist!");
            if (cantDel(HstUnt))
                dal.deleteHostingUnit(HstUnt);
            else
                throw new System.ArgumentException("can not delete hosting unit becase there is an open order!");
        }
        /// <summary>
        /// a func that updates hosting unit
        /// </summary>
        /// <param name="HstUnt"></param>
        public void UpdateHostingUnit(HostingUnit HstUnt)
        {
            if (!(dal.getHostingUnitList().Any(hu => (hu.HostingUnitKey == HstUnt.HostingUnitKey))))
                throw new System.ArgumentException("hosting unit dont exist!");
            dal.UpdateHostingUnit(HstUnt);
        }
        /// <summary>
        /// a func that returns the hosting unit list
        /// </summary>
        /// <returns></returns>
        public List<HostingUnit> getHostingUnitList()
        {
            return dal.getHostingUnitList();
        }
        /// <summary>
        /// a func to add an order to the list
        /// </summary>
        /// <param name="ord"></param>
        public void addOrder(Order ord)
        {
            if (dal.getOrderList().Any(ord1 => (ord1.OrderKey == ord.OrderKey)))
                throw new System.ArgumentException("order is exist");
            ord.CreateDate = new DateTime(2020, (DateTime.Today).Month, (DateTime.Today).Day);
            dal.addOrder(ord);
        }
        /// <summary>
        /// a func to update order status
        /// </summary>
        /// <param name="orKey"></param>
        /// <param name="statO"></param>
        public void updateOrder(long orKey, statusOrder statO)
        {
            if (!dal.getOrderList().Any(ord1 => (ord1.OrderKey ==orKey)))
                throw new System.ArgumentException("order is not exist");
            if (statO != statusOrder.טרם_טופל && statO != statusOrder.נסגר_בהיענות_של_לקוח && statO != statusOrder.נסגר_מחוסר_הענות_של_הלקוח && statO != statusOrder.נשלח_מייל)
                throw new System.ArgumentException("dont have a status");
            dal.updateOrder(orKey, statO);
        }
        /// <summary>
        /// a func that returns the order list
        /// </summary>
        /// <returns></returns>
        public List<Order> getOrderList()
        {
            return dal.getOrderList();
        }
        /// <summary>
        /// a func that returns the bank branch list
        /// </summary>
        /// <returns></returns>
        public List<BankBranch> getBankBranches()
        {
            return dal.getBankBranches();
        }
    }
}
