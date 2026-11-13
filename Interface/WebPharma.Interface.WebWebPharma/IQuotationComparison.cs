using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebPharma.Interface.General;

namespace WebPharma.Interface.WebPharma
{
    public interface IQuotationComparison : IGeneral
    {
        long FK_Items { get; set; }
        string ItemName { get; set; }
        long Fk_Customer { get; set; }
        string CustomerName { get; set; }
        long Fk_Supplier { get; set; }
        string SupplierName { get; set; }
        Int64 FreeQty { get; set; }
        decimal PurchaseRate { get; set; }
        decimal DiscountAmount { get; set; }
        Int16 Category { get; set; }
        string CategoryName { get; set; }
        Int16 Type { get; set; }
        DateTime FromDate { get; set; }
        DateTime ToDate { get; set; }

       // long MasterID { get; set; }
    }
}
