using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    enum statusGusReq { פתוח, נסגר_על_ידי_האתר, נסגר_כי_פג_תוקף }
    enum area { צפון, דרום, מרכז, ירושלים }
    enum subArea { שרון, ערבה, עוטף_עזה, גליל_עליון, גולן, גליל_תחתון, שומרון, הרי_יהודה, אילת, גוש_דן, בנימין }
    enum type { צימר, דירת_אירוח, חדר_במלון, מאהל }
    enum isNecessary { הכרחי, אפשרי, לא_מעונין }
    enum statusOrder { טרם_טופל, נשלח_מייל, נסגר_מחוסר_הענות_של_הלקוח, נסגר_בהיענות_של_לקוח }

}
