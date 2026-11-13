using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using CrystalDecisions.CrystalReports.Engine;

using CrystalDecisions.Shared;
using System.Drawing.Printing;
using CrystalDecisions.Web;
using WebPharma.Business;
using WebPharma.Business.WebPharma;
using System.Data;
using System.IO;
using System.Net;



public partial class ScheduleReport : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        ShowReport();
    }
    private void ShowReport()
    {
        BlSchedule blSchedule = new BlSchedule();
        ReportDocument reportDocument = new ReportDocument();
        long StatusId = Convert.ToInt64(Request.QueryString["ScheduleId"]);
        blSchedule.ReportFile = "SchedulePostedList.rpt";
        blSchedule.ReportTable = "SchedulePostedList";
        blSchedule.ReportSchema = "SchedulePostedList";
        blSchedule.ReportSchemaPath = Server.MapPath("Schema/");
        blSchedule.ReportPath = Server.MapPath("Reports/");
        blSchedule.DatasetName = "INVENTORYREPORTS";
        try
        {
            if (blSchedule.ReportFile.Length > 0)
            {
                reportDocument.Load(blSchedule.ReportPath.Trim() + blSchedule.ReportFile.Trim());
            }
            reportDocument.SetDataSource(blSchedule.GetSchedulePostingDataSource(StatusId));
            //ExportCrystalReport(reportDocument);
            CrystalReportViewer1.ReportSource = reportDocument;
            CrystalReportViewer1.DataBind();
            ExportCrystalReport(reportDocument);
        }
        catch (Exception ex)
        {

        }
    }
    private void ExportCrystalReport(ReportDocument reportDocument)
    {


        
         
            reportDocument.ExportToHttpResponse(ExportFormatType.ExcelRecord, Response, false, "");
       
    }
}