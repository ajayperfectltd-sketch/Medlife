using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebPharma.Interface.General;


namespace WebPharma.Interface.Masters
{
   public interface IBankBranch:IGeneral
    {
        string Name { get; set; }
        string ShortName { get; set; }
        string Address1 { get; set; }
        string Address2 { get; set; }
        string Address3 { get; set; }
        Boolean Active { get; set; }
        Int64 FK_Bank { get; set; }
        Int64 FK_Country { get; set; }
        Int64 FK_AccountHead { get; set; }
        Int64 FK_AccountHeadSub { get; set; }
        string AccountSubHead { get; set; }
        string AccountHead { get; set; }
    }
}
