using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebPharma.Interface.General;

namespace WebPharma.Interface.Masters
{
    public interface ISubUnit : IGeneral 
    {
        string Name { get; set; }
        long Fk_Units { get; set; }
        int NoofUnits { get; set; }
        long FK_Reason { get; set; }
    }
}
