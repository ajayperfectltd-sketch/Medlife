using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebPharma.Interface.General;

namespace WebPharma.Interface.Masters
{
    public interface IQuotation : IGeneral
    {
        Int16 Type { get; set; }
        long FK_Master { get; set; }
        DateTime EffectFrom { get; set; }
        DateTime EffectTo { get; set; }
        long FK_Reason { get; set; }
        string XmlDetails { get; set; }
        string Description { get; set; }
        string Name { get; set; }
    }
}
