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
        bool dateOk(DateTime start, DateTime end, HostingUnit hu)
        {
            int i = start.Month - 1, k = end.Month - 1;
            int j = start.Day - 1, s = end.Day - 1;
            while (i != k && j != s)
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
        public bool allowedSendReq(Host host)//(2)//לממש
        {
            return true;
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
        public void cantCencelAllowens(GuestRequest gs)//(9)//חסר מימוש
        {

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
            List<Order> listOrd = dal.getOrderList().FindAll( o=> o.GuestRequestKey == gs.GuestRequestKey);
            return listOrd.Count();
        }
        /// <summary>
        /// a func that returns the number of orders who werte sent or closed successfuly
        /// </summary>
        /// <param name="hostU"></param>
        /// <returns></returns>
        
        public int numOfClosedOrSentOrders(HostingUnit hostU)//(16)//קיים שאלה מה זה מספר הזמנות שנשלחו     *********************************************************************************************************************
        {
            List<Order> listOrd = dal.getOrderList().FindAll(o=> o.HostingUnitKey== hostU.HostingUnitKey && o.Status == statusOrder.נסגר_בהיענות_של_לקוח);
            return listOrd.Count();
            //send_mail
        }
        #region func by grouping
        /// <summary>
        /// a func that returns a list of groups of guest request by sertain area
        /// </summary>
        /// <param name="a"></param>
        /// <returns></returns>
        public List<IGrouping<bool, GuestRequest>> groupGuestReqByArea(area a)//(17)
        {
            var listByArea = (from gs in dal.getGuestRequestList()
                              group gs by gs.Area == a).ToList();
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
        public List<IGrouping<int, Host>> groupByNumOfHostingUnits()//(19)😃😃😃😃😃😃😃😃😃😃😃😃😃😃😃😃😃😃😃😃😃😃😃😃😃😃😃😃😃😃😃😃😃😃😃😃😃😃😃😃😃😃😃😃😃😃😃😃😃😃😃😃😃😃😃
        {
            var listByNumOfUnit = (from gs in dal.getHostingUnitList()
                                   group gs by gs.Owner.NumOfHostingUnit).ToList();
            return listByNumOfUnit;
        }
        /// <summary>
        /// a func that returns a list of groups of hosting unit by area
        /// </summary>
        /// <param name="a"></param>
        /// <returns></returns>
        public List<IGrouping<bool, HostingUnit>> groupHostUnitByArea(area a)//(20)
        {
            var listByArea = (from gs in dal.getHostingUnitList()
                              group gs by gs.areaOfUnit == a).ToList();
            return listByArea;
        }
        #endregion


        void addCustomerReq(GuestRequest gs)//מה צריך לבדןק? איך בודקים מייל ואיך בודקים אם ההמפתח קיים אם הוא עוד לא מאותחל
        {
            if(isDateValid(gs.Clone().EntryDate,gs.Clone().ReleaseDate))
            {
                gs.RegistrationDate = new DateTime(2020, (DateTime.Today).Month, (DateTime.Today).Day);
                dal.addCustomerReq(gs);
            }
        }
        void updateCustomerReq(long gsKey, statusGusReq stat)
        {
            if (!(dal.getGuestRequestList().Any(gs1 => (gs1.Clone()).GuestRequestKey == gsKey)))
                throw new System.ArgumentException("request dont exist!");
            if (stat != statusGusReq.נסגר_כי_פג_תוקף && stat != statusGusReq.נסגר_על_ידי_האתר && stat != statusGusReq.פתוח)
                throw new System.ArgumentException("dont have a status");
            dal.updateCustomerReq(gsKey, stat);
        }
        List<GuestRequest> getGuestRequestList()
        {
            return dal.getGuestRequestList();

        }
        void addHostingUnit(HostingUnit hostunit)
        {

        }


    }
}
