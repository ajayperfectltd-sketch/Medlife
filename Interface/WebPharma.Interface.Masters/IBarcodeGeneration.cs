using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebPharma.Interface.General;



namespace WebPharma.Interface.Masters
{
    public interface IBarcodeGeneration : IGeneral
    {
        string Name { get; set; }
        string ShortName { get; set; }
        string Address1 { get; set; }
        string Address2 { get; set; }
        string Address3 { get; set; }
        string BarCode { get; set; }
        Boolean Active { get; set; }
        Int64 FK_StockCode { get; set; }
        Int64 FK_Items { get; set; }
        string PriLayout { get; set; }
    }
}
