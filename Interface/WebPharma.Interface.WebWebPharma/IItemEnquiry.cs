using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebPharma.Interface.General;

namespace WebPharma.Interface.WebPharma
{
    public interface IItemEnquiry : IGeneral
     
    {
        string Name { get; set; }
        long FK_Category { get; set; }
        long FK_SubCategory { get; set; }
        long FK_Units { get; set; }
        long FK_SubUnit { get; set; }
        long FK_Manufacturer { get; set; }
        long FK_Generic { get; set; }
        long FK_Rack { get; set; }
        long FK_Bin { get; set; }
        long FK_Store { get; set; }
        long FK_Supplier { get; set; }
        string BatchNo { get; set; }
        long FK_Schedule { get; set; }
        decimal MRP { get; set; }
        decimal Salesprice { get; set; }
        decimal PurchaseRate { get; set; }
        string Criteria { get; set; }
        long Fk_Doctor { get; set; }
        long Fk_Customer { get; set; }
        string DoctorName { get; set; }
        string CustomerName { get; set; }
        string CriteriaMRP { get; set; }
        string CriteriaPurchase { get; set; }
        string CreteriaSales { get; set; }
        bool Zerostock { get; set; }
        int Level { get; set; }
    }
}
