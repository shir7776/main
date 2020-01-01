using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE;
using BL;
namespace PL1
{
    class Program
    {
        IBL bl = FactoryBL.GetBL();
        static void Main(string[] args)
        {
            IBL bl = FactoryBL.GetBL();

            #region Hosting Unit
            HostingUnit h1 = new HostingUnit()
            {
                HostingUnitName = "apple",
                areaOfUnit = BE.area.דרום,
                diary = new bool[12,31],
                Owner = new Host()
                {
                    HostKey = 315206482,
                    PrivateName = "dan",
                    FamilyName = "cohen",
                    PhoneNumber = 0543198488,
                    MailAdress = "dan@gmail.com",
                    BankAccountNumber = 118833,
                    CollectionClearance = true,
                    BankBranchDetails = new BankBranch()
                    {
                        BankNumber = ++Configurations.bankNumber,
                        BankName = "hapoalim",
                        BranchNmber = 18,
                        BranchAdress = "beit hadfus 78",
                        BranchCity = "tlv"
                    }
                }
            };
            h1.diary[4, 4] = true;
            h1.diary[4, 5] = true;
            h1.diary[4, 6] = true;

            HostingUnit h2 = new HostingUnit()
            {
                HostingUnitName = "pear",
                areaOfUnit = BE.area.ירושלים,
                diary = new bool[12, 31],
                Owner = new Host()
                {
                    HostKey = 209387125,
                    PrivateName = "yosi",
                    FamilyName = "cohen",
                    PhoneNumber = 0543198488,
                    MailAdress = "yosi@gmail.com",
                    BankAccountNumber = 912233,
                    CollectionClearance = true,
                    BankBranchDetails = new BankBranch()
                    {
                        BankNumber = ++Configurations.bankNumber,
                        BankName = "leumi",
                        BranchNmber = 19,
                        BranchAdress = "beit hadfus 78",
                        BranchCity = "tlv"
                    }
                }
            };
            h2.diary[8, 4] = true;
            h2.diary[8, 5] = true;
            h2.diary[8, 6] = true;
            HostingUnit h3 = new HostingUnit()
            {
                HostingUnitName = "orange",
                areaOfUnit = BE.area.מרכז,
                diary = new bool[12, 31],
                Owner = new Host()
                {
                    HostKey = 318336062,
                    PrivateName = "ron",
                    FamilyName = "cohen",
                    PhoneNumber = 0543198488,
                    MailAdress = "ron@gmail.com",
                    BankAccountNumber = 141414,
                    CollectionClearance = true,
                    BankBranchDetails = new BankBranch()
                    {
                        BankNumber = ++Configurations.bankNumber,
                        BankName = "leumi",
                        BranchNmber = 14,
                        BranchAdress = "beit hadfus 78",
                        BranchCity = "tlv"
                    }
                }
            };
            h3.diary[4, 4] = true;
            h3.diary[4, 5] = true;
            h3.diary[4, 6] = true;
            HostingUnit h4 = new HostingUnit()
            {
                HostingUnitName = "grape",
                areaOfUnit = BE.area.צפון,
                diary = new bool[12, 31],
                Owner = new Host()
                {
                    HostKey = 029567567,
                    PrivateName = "yael shoshana",
                    FamilyName = "chaya",
                    PhoneNumber = 0543198488,
                    MailAdress = "yalla@gmail.com",
                    BankAccountNumber = 112236,
                    CollectionClearance = true,
                    BankBranchDetails = new BankBranch()
                    {
                        BankNumber = ++Configurations.bankNumber,
                        BankName = "leumi",
                        BranchNmber = 16,
                        BranchAdress = "beit hadfus 78",
                        BranchCity = "tlv"
                    }
                }
            };
            h4.diary[2, 4] = true;
            h4.diary[2, 5] = true;
            h4.diary[2, 6] = true;

            HostingUnit h5 = new HostingUnit()
            {
                HostingUnitName = "olive",
                areaOfUnit = BE.area.צפון,
                diary = new bool[12, 31],
                Owner = new Host()
                {
                    HostKey = 029567567,
                    PrivateName = "yael shoshana",
                    FamilyName = "chaya",
                    PhoneNumber = 0543198488,
                    MailAdress = "yalla@gmail.com",
                    BankAccountNumber = 112236,
                    CollectionClearance = true,
                    BankBranchDetails = new BankBranch()
                    {
                        BankNumber = ++Configurations.bankNumber,
                        BankName = "leumi",
                        BranchNmber = 16,
                        BranchAdress = "beit hadfus 78",
                        BranchCity = "tlv"
                    }
                }
            };
            h5.diary[2, 4] = true;
            h5.diary[2, 5] = true;
            h5.diary[2, 6] = true;

            bl.addHostingUnit(h1);
            bl.addHostingUnit(h2);
            bl.addHostingUnit(h3);
            bl.addHostingUnit(h4);
            bl.addHostingUnit(h5);

            foreach (var item in bl.getHostingUnitList())
            {
                Console.WriteLine("hosting unit: \n" + item + "\n");
            }
            h4.HostingUnitName = "banana";
            bl.UpdateHostingUnit(h4);
            foreach (var item in bl.getHostingUnitList())
            {
                Console.WriteLine("Hosting Unit: \n" + item + "\n");
            }

            Console.WriteLine("hosting unit group by area: \n");
            var v = bl.groupHostUnitByArea();
            foreach (var item in v)
            {
                Console.WriteLine(item.Key);
                foreach (var w in item)
                    Console.WriteLine(w);
            }

            DateTime temp = new DateTime(2020, 5, 5);
            //Console.WriteLine("get available days: \n");
            //var vd = bl.GetAvailableDays(temp, 2);
            //foreach(var item6 in vd)
            //{
            //    Console.WriteLine();
            //}
            var vd = bl.availableUnits(temp, 2);
            int i = 1;
            foreach (var item6 in vd)
            {
                Console.WriteLine("hosting unit #" + i + ": \n" + item6);
                i++;
            }

            //int i = 1;
            //foreach (var item in bl.GetHostingUnitCopy())
            //{

            //    Console.WriteLine("hosting unit #" +i+ ": \n" + bl.GetAvailableDays(temp, 2) + "\n");
            //    i++;
            //}
            #endregion

            #region Guest Request
            GuestRequest g1 = new GuestRequest()
            {
                PrivateName = "yosi",
                FamilyName = "cohen",
                MailAddress = "yosi@gmail.com",
                Status = BE.statusGusReq.פתוח,
                RegistrationDate = new DateTime(2020, 1, 1),
                EntryDate = new DateTime(2020, 4, 3),
                ReleaseDate = new DateTime(2020, 4, 6),
                Area = BE.area.דרום,
                Type = BE.type.דירת_אירוח,
                Adults = 4,
                Children = 5,
                Pool = BE.isNecessary.אפשרי,
                Jacuzzi = BE.isNecessary.אפשרי ,
                Garden = BE.isNecessary.אפשרי,
                ChildrensAttractions = BE.isNecessary.אפשרי,
            };

            GuestRequest g2 = new GuestRequest()
            {
                PrivateName = "ron",
                FamilyName = "cohen",
                MailAddress = "ron@gmail.com",
                Status = BE.statusGusReq.פתוח ,
                RegistrationDate = new DateTime(2020, 1, 1),
                EntryDate = new DateTime(2020, 3, 4),
                ReleaseDate = new DateTime(2020, 3, 6),
                Area = BE.area.ירושלים,
                Type = BE.type.חדר_במלון,
                Adults = 3,
                Children = 2,
                Pool = BE.isNecessary.הכרחי,
                Jacuzzi = BE.isNecessary.הכרחי,
                Garden = BE.isNecessary.הכרחי,
                ChildrensAttractions = BE.isNecessary.הכרחי,
            };

            GuestRequest g3 = new GuestRequest()
            {
                PrivateName = "dan",
                FamilyName = "cohen",
                MailAddress = "yosi@gmail.com",
                Status = BE.statusGusReq.פתוח,
                RegistrationDate = new DateTime(2020, 1, 1),
                EntryDate = new DateTime(2020, 4, 3),
                ReleaseDate = new DateTime(2020, 4, 6),
                Area = BE.area.מרכז,
                Type = BE.type.מאהל,
                Adults = 3,
                Children = 6,
                Pool = BE.isNecessary.לא_מעונין,
                Jacuzzi = BE.isNecessary.לא_מעונין,
                Garden = BE.isNecessary.לא_מעונין,
                ChildrensAttractions = BE.isNecessary.לא_מעונין,
            };


            GuestRequest g4 = new GuestRequest()
            {
                PrivateName = "yahoo",
                FamilyName = "cohen",
                MailAddress = "yahoo@gmail.com",
                Status = BE.statusGusReq.פתוח,
                RegistrationDate = new DateTime(2020, 1, 1),
                EntryDate = new DateTime(2020, 2, 4),
                ReleaseDate = new DateTime(2020, 2, 6),
                Area = BE.area.דרום,
                Type = BE.type.צימר,
                Adults = 1,
                Children = 4,
                Pool = BE.isNecessary.אפשרי,
                Jacuzzi = BE.isNecessary.אפשרי,
                Garden = BE.isNecessary.אפשרי,
                ChildrensAttractions = BE.isNecessary.אפשרי,
            };
            bl.addCustomerReq(g1);
            bl.addCustomerReq(g2);
            bl.addCustomerReq(g3);
            bl.addCustomerReq(g4);
            foreach (var item in bl.getGuestRequestList())
            {
                Console.WriteLine("hosting unit: \n" + item + "\n");
            }

            statusGusReq sgs = statusGusReq.נסגר_כי_פג_תוקף;
            bl.updateCustomerReq(g2.GuestRequestKey,sgs);

            Console.WriteLine("Hosting Unit after changes: ");
            foreach (var item in bl.getGuestRequestList())
            {
                Console.WriteLine("hosting unit: \n" + item + "\n");
            }


            Console.WriteLine("groups by amount of people: \n");
            var r = bl.groupByNumOfPeople();
            foreach (var item in r)
            {
                Console.WriteLine(item.Key);
                foreach (var w in item)
                    Console.WriteLine(w);
            }

            Console.WriteLine("groups by area: \n");
            var b = bl.groupGuestReqByArea();
            foreach (var item in b)
            {
                Console.WriteLine(item.Key);
                foreach (var w in item)
                    Console.WriteLine(w);
            }

            #endregion

            #region Order

            Order o1 = new Order()
            {
                Status = BE.statusOrder.טרם_טופל,
                CreateDate = new DateTime(2019, 2, 1),
                OrderDate = new DateTime(2020, 4, 4),
                HostingUnitKey = h1.HostingUnitKey,
                GuestRequestKey = g2.GuestRequestKey
            };

            Order o2 = new Order()
            {
                Status = BE.statusOrder.נסגר_בהיענות_של_לקוח,
                CreateDate = new DateTime(2019, 4, 1),
                OrderDate = new DateTime(2020, 3, 4),
                HostingUnitKey = h2.HostingUnitKey,
                GuestRequestKey = g1.GuestRequestKey
            };

            Order o3 = new Order()
            {
                Status = BE.statusOrder.נסגר_מחוסר_הענות_של_הלקוח,
                CreateDate = new DateTime(2019, 4, 1),
                OrderDate = new DateTime(2020, 4, 4),
                HostingUnitKey = h3.HostingUnitKey,
                GuestRequestKey = g4.GuestRequestKey
            };

            Order o4 = new Order()
            {
                Status = BE.statusOrder.נשלח_מייל,
                CreateDate = new DateTime(2019, 2, 2),
                OrderDate = new DateTime(2020, 2, 4),
                HostingUnitKey = h4.HostingUnitKey,
                GuestRequestKey = g3.GuestRequestKey
            };

            Order o5 = new Order()
            {
                Status = BE.statusOrder.נסגר_בהיענות_של_לקוח,
                CreateDate = new DateTime(2019, 6, 1),
                OrderDate = new DateTime(2020, 6, 3),
                HostingUnitKey = h1.HostingUnitKey,
                GuestRequestKey = g2.GuestRequestKey
            };

            bl.addOrder(o1);
            bl.addOrder(o2);
            bl.addOrder(o3);
            bl.addOrder(o4);
            bl.addOrder(o5);


            foreach (var item0 in bl.getOrderList())
            {
                Console.WriteLine("Order: \n" + item0 + "\n");
            }

            bl.deleteHostingUnit(h5);

            Console.WriteLine("after removing: ");
            foreach (var item1 in bl.getHostingUnitList())
            {
                Console.WriteLine("Hosting Unit: \n" + item1 + "\n");
            }
            Console.WriteLine("amount of finalize: " + bl.numOfClosedOrSentOrders(h2));


            Console.WriteLine("amount of invetation: \n" + bl.numOfOrders(g1));
            statusOrder st = statusOrder.נסגר_בהיענות_של_לקוח;
            bl.updateOrder(o1.OrderKey,st);
            bl.updateOrder(o2.OrderKey, st);

            Console.WriteLine("after updating: \n");
            foreach (var item2 in bl.getOrderList())
            {
                Console.WriteLine("Order: \n" + item2 + "\n");
            }

            Console.WriteLine("amount of orders: ");
            foreach (var item7 in bl.ordersOverTime(0))
            {
                Console.WriteLine("order: " + item7 + "\n");
            }

            #endregion

            //foreach (var item3 in bl.GetBankBranches())
            //{
            //    Console.WriteLine("Branch: " + item3);
            //}

            //DateTime d1 = new DateTime(2019, 3, 3);
            //DateTime d2 = new DateTime(2019, 3, 10);
            //Console.WriteLine("amount of days between 2 given days: " + bl.AmountOfDaysBetween(d1, d2));
            //Console.WriteLine("amount of days between given day and today: " + bl.AmountOfDaysBetween(d2));
            //Console.WriteLine("hosting units grouped by hosts");
            //var l = bl.GetAllHostsGruopByAmountOfHostingUnits();
            //foreach (var item4 in l)
            //{
            //    Console.WriteLine(item4.Key);
            //    foreach (var w in item4)
            //    {
            //        // Console.WriteLine(w.Key);
            //        foreach (var v2 in w)
            //            Console.WriteLine(v2);

            //    }

            //}


            //foreach (var item5 in bl.CustomerDemands(x => x.Area == g1.Area))
            //{
            //    Console.WriteLine("demands: \n" + item5);
            //}

            //Console.WriteLine("bye bye :P");
            ////}
            ////catch(System.Exception exp)
            ////{
            ////    Console.WriteLine(exp.Message);
            ////}
            //Console.ReadKey();
        }
    
    }
}
