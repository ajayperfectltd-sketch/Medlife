using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebPharma.Interface.General;

namespace WebPharma.Interface.Masters
{
    public interface IBin : IGeneral
    {
        string ShortName { get; set; }
        string BinName { get; set; }
        DateTime EnteredOn { get; set; }
        long FK_Client { get; set; }
       long  FK_Rack { get; set; }
       long FK_Reason { get; set; }
    }

}
