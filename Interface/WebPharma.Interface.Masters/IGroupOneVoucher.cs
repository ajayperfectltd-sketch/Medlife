using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebPharma.Interface.General;
namespace WebPharma.Interface.Masters
{
    public interface IGroupOneVoucher:IGeneral 
    {
        string GroupOneCode { get; set; }
        string GroupOneName { get; set; }
       
        int SortOrder { get; set; }
        string Description { get; set; }
        long   FK_GroupOneVoucher  { get; set; }
        string GroupOneVoucherNo  { get; set; }
        decimal GroupOneVoucherAmount  { get; set; }
        long FK_AccountHeadSub { get; set; }
        Boolean Active { get; set; }
    }
}
