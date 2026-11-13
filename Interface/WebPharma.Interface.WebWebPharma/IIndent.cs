using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebPharma.Interface.General;

namespace WebPharma.Interface.WebPharma
{
    public interface IIndent:IGeneral
    {
        bool Cancelled { get; set; }
        string CancelledBy { get; set; }
        DateTime CancelledOn { get; set; }
        long ReqID{get;set;}
        long FK_Store { get; set; }
        long ToStore { get; set; }
        DateTime IndentDate { get; set; }
        string xmlGriddata { get; set; }
        string Remarks { get; set; }
        DateTime FromDate { get; set; }
        DateTime Todate { get; set; }
        long FK_Reason { get; set; }
        decimal FK_Stock { get; set; }
        long ToStoreE { get; set; }
        long FK_Items { get; set; }
    }

    
}
