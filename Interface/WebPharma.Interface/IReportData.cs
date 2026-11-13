using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebPharma.Interface
{
    public interface IReportData
    {
        string ReportFile { get; set; }
        DateTime FromDate { get; set; }
        DateTime ToDate { get; set; }
        string SubModule { get; set; }
        string ID { get; set; }
        string SubID { get; set; }
        string ReportSchema { get; set; }
        string ReportTable { get; set; }
        string ReportSchemaPath { get; set; }
        Int64 FK_Customer { get; set; }
        Int64 FK_Users { get; set; }
        Int64 FK_Store { get; set; }
        Int64 FK_Items { get; set; }
        Int64 FK_Supplier { get; set; }
        Int64 FK_Client { get; set; }
        string ReportPath { get; set; }
        string DatasetName { get; set; }
        string Critrea1 { get; set; }
        string Critrea2 { get; set; }
        string Critrea3 { get; set; }
        string Critrea4 { get; set; }
        Int32 TableCount { get; set; }
        string PrinterName { get; set; }
        string UserCode { get; set; }
        string XmlData { get; set; }
        string ReportCation { get; set; }
        Int16 FinYear { get; set; }
        string Title { get; set; }
        string PageTitle { get; set; }
        Int64 FromStore { get; set; }
        Int64 ToStore { get; set; }
        Int64 FK_Salesman { get; set; }
        byte PaymentMode { get; set; }
        Int64 FK_AccountHead { get; set; }
        Int64 FK_AccountSubHead { get; set; }
        Int64 FK_AccountGroup { get; set; }
        Int64 FK_AccountSubGroup { get; set; }
        DateTime AsonDate { get; set; }
        byte IssueMode { get; set; }
        Int64 FK_CategoryFirst { get; set; }
        Int64 FK_Counter { get; set; }
        Int64 FK_Shift { get; set; }
        Int64 FK_CustomerGroup { get; set; }
        byte Period { get; set; }
        DateTime CompareDateFrom { get; set; }//CompareDate
        DateTime CompareDateTo { get; set; }
        byte UserAction { get; set; }
        string AuditData { get; set; }
        string reportname { get; set; }
        string UserName { get; set; }
        Int64 FK_Manufacturer { get; set; }
        Int64 FK_Schedule { get; set; }
        Int64 FK_Generic { get; set; }
    }
}
