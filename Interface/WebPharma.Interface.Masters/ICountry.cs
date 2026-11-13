using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebPharma.Interface.General;

namespace WebPharma.Interface.Masters
{
    public interface ICountry:IGeneral
    {
        string CountryName { get; set; }
        string CountryCode { get; set; }
        Int64 FK_Currency { get; set; }
        long FK_Reason { get; set; }
    }
}
