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



public partial class PrintReport : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        ShowReport();
    }
    private void ShowReport()
    {
        if (Request.QueryString["Type"] == "STR")
        {
            StockTransferRequest();
        }
        else if (Request.QueryString["Type"] == "CRP")
        {
            CustomerRePayment();
        }
        else if (Request.QueryString["Type"] == "STI")
        {
            StockTransferIssue();
        }
    }
    private void StockTransferRequest()
    {
        //try
        //{
       // BlltemConversion blItemConversion = new BlltemConversion();
       // ReportDocument reportDocument = new ReportDocument();
        //   long StatusId = Convert.ToInt64(Request.QueryString["ID"]);
        //    blItemConversion.ReportFile = "StockTransferRequest.rpt";
        //    blItemConversion.ReportTable = "StockTransferRequest";
        //    blItemConversion.ReportSchema = "StockTransferRequest";
        //    blItemConversion.ReportSchemaPath = Server.MapPath("Schema/");
        //    blItemConversion.ReportPath = Server.MapPath("Reports/");
        //    blItemConversion.DatasetName = "INVENTORYREPORTS";

        //    if (blItemConversion.ReportFile.Length > 0)
        //    {
        //      reportDocument.Load(blItemConversion.ReportPath.Trim() + blItemConversion.ReportFile.Trim());
        //    }
         // reportDocument.SetDataSource(blItemConversion.GetStockTransferRequestDataSource(StatusId));
        //    CrystalReportViewer1.ReportSource = reportDocument;
        //    CrystalReportViewer1.DataBind();
        //}
        //catch (Exception ex)
        //{

        //    ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "Alert('" + ex.ToString() + "')", true);
        //}



        try
        {
            //String str = Request.QueryString["datepass"].ToString();
            BlUsers blusers = new BlUsers();
            BlReportData BlReportdata = new BlReportData();
            ReportDocument reportDocument = new ReportDocument();
            long StatusId = Convert.ToInt64(Request.QueryString["ID"]);
            BlReportdata.ReportFile = "StockTransferRequest.rpt";
            BlReportdata.ReportTable = "StockTransferRequest";
            BlReportdata.ReportSchema = "StockTransferRequest";
            BlReportdata.ReportSchemaPath = Server.MapPath("Schema/");
            BlReportdata.ReportPath = Server.MapPath("Reports/");
            BlReportdata.DatasetName = "INVENTORYREPORTS";
            string s = BlReportdata.Title;
            BlReportdata.Title = s;
            BlReportdata.PageTitle = "Stock Transfer Request";
            if (BlReportdata.ReportFile.Length > 0)
            {
                reportDocument.Load(BlReportdata.ReportPath.Trim() + BlReportdata.ReportFile.Trim());
            }
            reportDocument.DataDefinition.FormulaFields["Title"].Text = "'" + BlReportdata.Title.ToString() + "'";
            reportDocument.DataDefinition.FormulaFields["PageTitle"].Text = "'" + BlReportdata.PageTitle.ToString() + "'";
            //reportDocument.DataDefinition.FormulaFields["User"].Text = "'" + blusers.Name.ToString() + "'";
            //reportDocument.DataDefinition.FormulaFields["Date"].Text = "'" + str + "'";
            reportDocument.SetDataSource(BlReportdata.GetStockTransferRequestDataSource(StatusId));
            CrystalReportViewer1.ReportSource = reportDocument;
            CrystalReportViewer1.DataBind();
        }
        catch (Exception ex)
        {

            ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "Alert('" + ex.ToString() + "')", true);
        }
    }
    private void CustomerRePayment()
    {
        //BlltemConversion blItemConversion = new BlltemConversion();
        //try
        //{
       // BlRePayment blrepayment = new BlRePayment();
       // ReportDocument reportDocument = new ReportDocument();
       // long StatusId = Convert.ToInt64(Request.QueryString["ID"]);
        //blrepayment.ReportFile = "CustomerRepayment.rpt";
        //blrepayment.ReportTable = "CustomerRepayment";
        //blrepayment.ReportSchema = "CustomerRepayment";
        //blrepayment.ReportSchemaPath = Server.MapPath("Schema/");
        //blrepayment.ReportPath = Server.MapPath("Reports/");
      
        //blrepayment.DatasetName = "INVENTORYREPORTS";
        
        //    if (blrepayment.ReportFile.Length > 0)
        //    {
        //        reportDocument.Load(blrepayment.ReportPath.Trim() + blrepayment.ReportFile.Trim());
        //    }
         //  reportDocument.SetDataSource(blrepayment.RepaymentReportData(StatusId));
        //    CrystalReportViewer1.ReportSource = reportDocument;
        //    CrystalReportViewer1.DataBind();
        //}
        //catch (Exception ex)
        //{
          
      
        //    ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "Alert('" + ex.ToString() + "')", true);

        //}
        try
        {
            //String str = Request.QueryString["datepass"].ToString();
            BlUsers blusers = new BlUsers();
            BlReportData BlReportdata = new BlReportData();
            ReportDocument reportDocument = new ReportDocument();
            long StatusId = Convert.ToInt64(Request.QueryString["ID"]);
            BlReportdata.ReportFile = "CustomerRepayment.rpt";
            BlReportdata.ReportTable = "CustomerRepayment";
            BlReportdata.ReportSchema = "CustomerRepayment";
            BlReportdata.ReportSchemaPath = Server.MapPath("Schema/");
            BlReportdata.ReportPath = Server.MapPath("Reports/");
            BlReportdata.DatasetName = "INVENTORYREPORTS";
            string s = BlReportdata.Title;
            BlReportdata.Title = s;
            BlReportdata.PageTitle = "Customer Re-payment";
            if (BlReportdata.ReportFile.Length > 0)
            {
                reportDocument.Load(BlReportdata.ReportPath.Trim() + BlReportdata.ReportFile.Trim());
            }
            reportDocument.DataDefinition.FormulaFields["Title"].Text = "'" + BlReportdata.Title.ToString() + "'";
            reportDocument.DataDefinition.FormulaFields["PageTitle"].Text = "'" + BlReportdata.PageTitle.ToString() + "'";
            //reportDocument.DataDefinition.FormulaFields["User"].Text = "'" + blusers.Name.ToString() + "'";
            //reportDocument.DataDefinition.FormulaFields["Date"].Text = "'" + str + "'";
            reportDocument.SetDataSource(BlReportdata.RepaymentReportData(StatusId));
            CrystalReportViewer1.ReportSource = reportDocument;
            CrystalReportViewer1.DataBind();
        }
        catch (Exception ex)
        {

            ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "Alert('" + ex.ToString() + "')", true);
        }
    }
    private void StockTransferIssue()
    {
        //try
        //{
         //   BlIndentIssue blindentissue = new BlIndentIssue();
          //  ReportDocument reportDocument = new ReportDocument();
         //   long StatusId = Convert.ToInt64(Request.QueryString["ID"]);
        //    blindentissue.ReportFile = "StockTransferIssue.rpt";
        //    blindentissue.ReportTable = "StockTransferIssue";
        //    blindentissue.ReportSchema = "StockTransferIssue";
        //    blindentissue.ReportSchemaPath = Server.MapPath("Schema/");
        //    blindentissue.ReportPath = Server.MapPath("Reports/");

        //    blindentissue.DatasetName = "INVENTORYREPORTS";

        //    if (blindentissue.ReportFile.Length > 0)
        //    {
       //         reportDocument.Load(blindentissue.ReportPath.Trim() + blindentissue.ReportFile.Trim());
        //    }
         //     reportDocument.SetDataSource(blindentissue.StockTransferIssueReceipt(StatusId));
        //    CrystalReportViewer1.ReportSource = reportDocument;
        //    CrystalReportViewer1.DataBind();
        //}
        //catch (Exception ex)
        //{
        //    ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "Alert('" + ex.ToString() + "')", true);
        //}
        try
        {
            //String str = Request.QueryString["datepass"].ToString();
            BlUsers blusers = new BlUsers();
            BlReportData BlReportdata = new BlReportData();
            ReportDocument reportDocument = new ReportDocument();
            long StatusId = Convert.ToInt64(Request.QueryString["ID"]);
            BlReportdata.ReportFile = "StockTransferIssue.rpt";
            BlReportdata.ReportTable = "StockTransferIssue";
            BlReportdata.ReportSchema = "StockTransferIssue";
            BlReportdata.ReportSchemaPath = Server.MapPath("Schema/");
            BlReportdata.ReportPath = Server.MapPath("Reports/");
            BlReportdata.DatasetName = "INVENTORYREPORTS";
            string s = BlReportdata.Title;
            BlReportdata.Title = s;
            BlReportdata.PageTitle = "Stock Transfer Issue";
            if (BlReportdata.ReportFile.Length > 0)
            {
                reportDocument.Load(BlReportdata.ReportPath.Trim() + BlReportdata.ReportFile.Trim());
            }
            reportDocument.DataDefinition.FormulaFields["Title"].Text = "'" + BlReportdata.Title.ToString() + "'";
            reportDocument.DataDefinition.FormulaFields["PageTitle"].Text = "'" + BlReportdata.PageTitle.ToString() + "'";
            //reportDocument.DataDefinition.FormulaFields["User"].Text = "'" + blusers.Name.ToString() + "'";
            //reportDocument.DataDefinition.FormulaFields["Date"].Text = "'" + str + "'";
            reportDocument.SetDataSource(BlReportdata.StockTransferIssueReceipt(StatusId));
            CrystalReportViewer1.ReportSource = reportDocument;
            CrystalReportViewer1.DataBind();
        }
        catch (Exception ex)
        {

            ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "Alert('" + ex.ToString() + "')", true);
        }
    }
}