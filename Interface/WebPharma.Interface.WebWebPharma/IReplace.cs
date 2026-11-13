using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebPharma.Interface.General;

namespace WebPharma.Interface.WebPharma
{
    public interface IReplace : IGeneral
    {
        long FK_Supplier { get; set; }
        long FK_Store { get; set; }
        long ReplaceID { get; set; }
        DateTime RecivedDate { get; set; }
        Boolean Linkbtn { get; set; }
        long Fk_PurchaseReturn { get; set; }
        long Fk_BillType { get; set; }
        string BillNo { get; set; }
        string InvoiceNo { get; set; }
        DateTime InvoiceDate { get; set; }
        string xmlPurchseValues { get; set; }
        string xmlValues { get; set; }
        decimal NetAmount { get; set; }
        long Fk_Reason { get; set; }
        decimal FTotalAmount { get; set; }
        decimal FDiscPer { get; set; }
        decimal FDiscAmnt { get; set; }
        decimal FnetAmnt { get; set; }
        decimal TtotalAmount { get; set; }
        decimal TDiscPer { get; set; }
        decimal TDiscAmnt { get; set; }
        decimal TnetAmnt { get; set; }

        bool Cancelled { get; set; }
        bool Approved { get; set; }
        string CancelledUser { get; set; }
        DateTime CancelledOn { get; set; }
        long TransactionID { get; set; }
        DateTime FromDate { get; set; }
        DateTime ToDate { get; set; }
        Int16 ApproveorPending { get; set; }
         

        
    }
}
