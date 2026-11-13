using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebPharma.Interface.General;

namespace WebPharma.Interface.WebPharma
{
    public interface IPriceCorrection : IGeneral
    {
        long FK_Store { get; set; }
       // long FK_Category { get; set; }
        DateTime PriceCorrectionDate { get; set; }
        string Cancelleduser { get; set; }
        DateTime CancelledOn { get; set; }
        bool Cancelled { get; set; }
        long FK_Reason { get; set; }
        bool Approved { get; set; }
        DateTime ApprovedOn { get; set; }
        string ApprovedBy { get; set; }
        string Remarks { get; set; }
        string xmlGriddata { get; set; }
        DateTime FromDate { get; set; }
        DateTime ToDate { get; set; }
        
    }
}
