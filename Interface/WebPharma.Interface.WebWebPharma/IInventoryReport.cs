using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebPharma.Interface.General;

namespace WebPharma.Interface.WebPharma
{
   public class IInventoryReport
    {
        Int64 FK_Category { get; set; }
        Int64 FK_Store { get; set; }
        Int64 FK_Items { get; set; }
        Int64 FK_Supplier { get; set; }
    }
}
