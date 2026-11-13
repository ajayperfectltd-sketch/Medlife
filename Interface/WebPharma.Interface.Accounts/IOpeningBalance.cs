using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebPharma.Interface.Accounts
{
    public interface IOpeningBalance
    {
        DateTime TransDate { get; set; }
        long FK_Store { get; set; }
        string UserCode { get; set; }
        byte UserAction { get; set; }
        DateTime EnteredOn { get; set; }
        Int16 PageIndex { get; set; }
        Int16 PageSize { get; set; }
        long FK_Client { get; set; }
        string FinalGroup { get; set; }
        Int32 FK_AccountGroup { get; set; }
        Int32 FK_AccountSubGroup { get; set; }
        Int32 FinYear { get; set; }
        string XmlAccountBalance { get; set; }
        Int32 BranchCode { get; set; }
    }
}
