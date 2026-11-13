using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebPharma.Interface.General;

namespace WebPharma.Interface.Masters
{
    public interface IItemRearrange : IGeneral
    {
        long FK_Newcategory { get; set; }
        long FK_NewSubCategory { get; set; }

        //DateTime EnteredOn { get; set; }
        DateTime groupingdate { get; set; }
        string XmlItems { get; set; }
        bool Checked { get; set; }
        long FK_Category { get; set; }
        long FK_SubCategory { get; set; }
          long FK_Manufacturer { get; set; }
        long FK_Bin { get; set; }
          long FK_Unit { get; set; }
        long FK_Generic { get; set; }
         long FK_Rack { get; set; }
        long FK_PriceGroup { get; set; }
        long FK_QuantityGroup { get; set; }

        string Name { get; set; }
        string Barcode { get; set; }
        bool StopSale { get; set; }
        bool Packed { get; set; }
         
        long ChangeTypeID { get; set; }
        long DataID { get; set; }
        long SubDataID { get; set; }
        string Reason { get; set; }
    }
}
