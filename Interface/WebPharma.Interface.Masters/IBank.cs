using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebPharma.Interface.General;



namespace WebPharma.Interface.Masters
{
    public interface IBank : IGeneral
    {
        string Name { get; set; }
        string ShortName { get; set; }
        string Address1 { get; set; }
        string Address2 { get; set; }
        string Address3 { get; set; }
        Boolean Active { get; set; }

    }
}
