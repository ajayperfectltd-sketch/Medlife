using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebPharma.Interface.General;

namespace WebPharma.Interface.Masters
{
   public interface ICustomerGroup:IGeneral
    {
        string Name { get; set; }
        string ShortName { get; set; }
        decimal CreditLimit { get; set; }
        Int32 CreditDays { get; set; }
        Boolean Active { get; set; }
        long FK_Reason { get; set; }
    }
}
