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
using WebPharma.Business.Masters;



public partial class ItemConversionReport : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        ShowReport();
    }
    private void ShowReport()
    {
        //BlltemConversion blItemConversion = new BlltemConversion();
        //ReportDocument reportDocument = new ReportDocument();
        //long StatusId = Convert.ToInt64(Request.QueryString["PurchaseId"]);
        //blItemConversion.ReportFile = "ItemConversionReport.rpt";
        //blItemConversion.ReportTable = "ItemConversionReport";
        //blItemConversion.ReportSchema = "ItemConversionReport";
        //blItemConversion.ReportSchemaPath = Server.MapPath("Schema/");
        //blItemConversion.ReportPath = Server.MapPath("Reports/");
        //blItemConversion.DatasetName = "INVENTORYREPORTS";
        //try
        //{
        //    if (blItemConversion.ReportFile.Length > 0)
        //    {
        //        reportDocument.Load(blItemConversion.ReportPath.Trim() + blItemConversion.ReportFile.Trim());
        //    }
        //    reportDocument.SetDataSource(blItemConversion.GetPurchaseDataSource(StatusId));
        //    //ExportCrystalReport(reportDocument);
        //    CrystalReportViewer1.ReportSource = reportDocument;
        //    CrystalReportViewer1.DataBind();
        //}
        //catch (Exception ex)
        //{

        //}


        try
        {
            //String str = Request.QueryString["datepass"].ToString();
            BlUsers blusers = new BlUsers();
            BlReportData BlReportdata = new BlReportData();
            ReportDocument reportDocument = new ReportDocument();
            long StatusId = Convert.ToInt64(Request.QueryString["PurchaseId"]);
            BlReportdata.ReportFile = "ItemConversionReport.rpt";
            BlReportdata.ReportTable = "ItemConversionReport";
            BlReportdata.ReportSchema = "ItemConversionReport";
            BlReportdata.ReportSchemaPath = Server.MapPath("Schema/");
            BlReportdata.ReportPath = Server.MapPath("Reports/");
            BlReportdata.DatasetName = "INVENTORYREPORTS";
            string s = BlReportdata.Title;
            BlReportdata.Title = s;
            BlReportdata.PageTitle = "Item Conversion Reciept";
            if (BlReportdata.ReportFile.Length > 0)
            {
                reportDocument.Load(BlReportdata.ReportPath.Trim() + BlReportdata.ReportFile.Trim());
            }
            reportDocument.DataDefinition.FormulaFields["Title"].Text = "'" + BlReportdata.Title.ToString() + "'";
            reportDocument.DataDefinition.FormulaFields["PageTitle"].Text = "'" + BlReportdata.PageTitle.ToString() + "'";
            //reportDocument.DataDefinition.FormulaFields["User"].Text = "'" + blusers.Name.ToString() + "'";
            //reportDocument.DataDefinition.FormulaFields["Date"].Text = "'" + str + "'";
            reportDocument.SetDataSource(BlReportdata.GetPurchaseDataSource(StatusId));
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