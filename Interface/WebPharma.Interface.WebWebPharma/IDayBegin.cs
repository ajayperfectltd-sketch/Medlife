using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebPharma.Interface.General;

namespace WebPharma.Interface.WebPharma
{
    public interface IDayBegin 
    {
        DateTime DbDate { get; set; }
        DateTime DbPreviousDate { get; set; }
        Int16 BranchCode { get; set; }
        Int64 MasterID { get; set; }
        Int16 SortOrder { get; set; }
        bool Cancelled { get; set; }
        string SqlUpdateQuery { get; set; }
        string AuditData { get; set; }
        string XmlAttendance { get; set; }
        string DBCheckSum { get; set; }
        string UserCode { get; set; }
         decimal  openingbalance { get; set; }
         decimal openingliability { get; set; }
         decimal AmntDifference { get; set; }
         byte UserAction { get; set; }
         string TransMode { get; set; }
        
         Boolean OfflineEntry { get; set; }
    }
}
