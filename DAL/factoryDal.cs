using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class FactoryDal
    {
        //add singleton like bl
            public static Idal getDal()
            {
                return new Dal_imp();
            }

       
    }
}
