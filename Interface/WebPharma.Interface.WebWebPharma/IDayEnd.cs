using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebPharma.Interface.WebPharma
{
       public interface IDayEnd
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
       // decimal openingbalance { get; set; }
     //   decimal openingliability { get; set; }
        //decimal AmntDifference { get; set; }
        byte UserAction { get; set; }
        string TransMode { get; set; }
        Int16 MachineCode { get; set; }
        Boolean OfflineEntry { get; set; }
        Int64 FK_Store { get; set; }
        DateTime FromDate { get; set; }
        Int64 FK_Client { get; set; }
        Decimal PurchaseAmount { get; set; }
        Decimal PurchaseLiability { get; set; }
        Decimal PReturnAmount { get; set; }
        Decimal PReturnLiability { get; set; }
        Decimal SalesAmount { get; set; }
        Decimal SalesLiability { get; set; }
        Decimal SReturnAmount { get; set; }
        Decimal SReturnLiability { get; set; }
        Decimal DamageStock { get; set; }
        Decimal IndentFrom { get; set; }
        Decimal IndentTo { get; set; }
        Decimal Stockvalue { get; set; }
        Decimal Receipt { get; set; }
        Decimal Payment { get; set; }
        Decimal Closingbalance { get; set; }
        decimal openingbalance { get; set; }


    }
}
