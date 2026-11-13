using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebPharma.Interface.General;

namespace WebPharma.Interface.Masters
{
    public interface ISettings : IGeneral
    {
        string Module { get; set; }
        string SubModule { get; set; }
        string Name { get; set; }
        string Value { get; set; }
        byte DeleteFlag { get; set; }
        
    }
}
