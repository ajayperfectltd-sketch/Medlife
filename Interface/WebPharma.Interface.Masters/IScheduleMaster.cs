using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebPharma.Interface.General;

namespace WebPharma.Interface.Masters
{
    public interface IScheduleMaster : IGeneral
    {
        string ShortName { get; set; }
        string Name { get; set; }
        long FK_Reason { get; set; }
    }
}
