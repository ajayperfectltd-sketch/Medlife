using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebPharma.Interface.General;


namespace WebPharma.Interface.Masters
{
    public  interface  IGiftVoucher: IGeneral
    {
        string Shortname { get; set; }
        string Name { get; set; }
        Int32 VoucherType { get; set; }
     


    }
}
