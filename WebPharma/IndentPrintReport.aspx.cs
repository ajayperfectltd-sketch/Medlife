using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using System.Drawing.Printing;
using CrystalDecisions.Web;
using WebPharma.Business;
using WebPharma.Business.WebPharma;
using System.Data;
using System.IO;
using System.Net;
using WebPharma.Business.Masters;

public partial class IndentPrintReport : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

        ShowReport();
    }
    private void ShowReport()
    {
       
        try
        {
            // String str = Request.QueryString["datepass"].ToString();
            BlUsers blusers = new BlUsers();
            String str = DateTime.Now.ToString();
            BlReportData BlReportdata = new BlReportData();
            ReportDocument reportDocument = new ReportDocument();
            long StatusId = Convert.ToInt64(Request.QueryString["indentID"]);
            BlReportdata.ReportFile = "IndentPrint.rpt";
            BlReportdata.ReportTable = "IndentPrint";
            BlReportdata.ReportSchema = "IndentPrint";
            BlReportdata.ReportSchemaPath = Server.MapPath("Schema/");
            BlReportdata.ReportPath = Server.MapPath("Reports/");
            BlReportdata.DatasetName = "INVENTORYREPORTS";
            string s = BlReportdata.Title;
            BlReportdata.Title = s;
            String UserName = Session["Username"].ToString();
            BlReportdata.PageTitle = "Indent Report";
            if (BlReportdata.ReportFile.Length > 0)
            {
                reportDocument.Load(BlReportdata.ReportPath.Trim() + BlReportdata.ReportFile.Trim());
            }
            reportDocument.DataDefinition.FormulaFields["Title"].Text = "'" + BlReportdata.Title.ToString() + "'";
            reportDocument.DataDefinition.FormulaFields["PageTitle"].Text = "'" + BlReportdata.PageTitle.ToString() + "'";
            reportDocument.DataDefinition.FormulaFields["User"].Text = "'" + UserName + "'";
            reportDocument.DataDefinition.FormulaFields["Date"].Text = "'" + str + "'";
            reportDocument.SetDataSource(BlReportdata.GetIndentDataSource(StatusId));
            CrystalReportViewer1.ReportSource = reportDocument;
            CrystalReportViewer1.DataBind();
            ExportCrystalReport(reportDocument);
            reportDocument.Close();
            reportDocument.Dispose();
        }
        catch (Exception ex)
        {

            ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "Alert('" + ex.ToString() + "')", true);
        }




    }
    private void ExportCrystalReport(ReportDocument reportDocument)
    {

        reportDocument.ExportToHttpResponse(ExportFormatType.PortableDocFormat, Response, false, "");

    }
}