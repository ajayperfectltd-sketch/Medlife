using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebPharma.Interface.General;
namespace WebPharma.Interface.Masters
{
    public interface IReportAcess:IGeneral
    {

        string XmlReportAccess { get; }
        long FK_User { get; set; }
        long FK_ReportCategory { get; set; }
        Int32 Type { get; set; }
        long FK_UserGroup { get; set; }
    }
}
