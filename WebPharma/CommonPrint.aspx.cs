/*-------------------------------------------------------------------
Purpose    : COMMON PRINT 
Author     :BIBIN TC
Copyright  : Perfect Software Solutions (Clt) Pvt Ltd
Created on :03/05/2018   	Accounts > Cash Transaction and Multiple Transaction > Add Print voucher Option
 * 
----------------------------------------------------------------------*/
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

public partial class CommonPrint : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        ShowReport();
    }
    private void ShowReport()
    {
        


        try
        {
            
            BlUsers blusers = new BlUsers();
            BlReportData BlReportdata = new BlReportData();
            ReportDocument reportDocument = new ReportDocument();

            string type=Request.QueryString["TYPE"];
            if(type == "AHSTM")
            {
                BlReportdata.ReportFile = "AccountHeadSubTransMultiplePrint.rpt";
                BlReportdata.ReportTable = "AccountHeadSubTransMultiplePrint";
                BlReportdata.ReportSchema = "AccountHeadSubTransMultiplePrint";
                BlReportdata.PageTitle = "Account HeadSubTransaction Multiple Receipt";
            }
            if (type == "AHST")
            {
                BlReportdata.ReportFile = "AccountHeadSubTransactionPrint.rpt";
                BlReportdata.ReportTable = "AccountHeadSubTransPrint";
                BlReportdata.ReportSchema = "AccountHeadSubTransPrint";
                BlReportdata.PageTitle = "Account HeadSubTransaction  Receipt";
            }
         
            long StatusId = Convert.ToInt64(Request.QueryString["Id"]);
            BlReportdata.ReportSchemaPath = Server.MapPath("Schema/");
            BlReportdata.ReportPath = Server.MapPath("Reports/");
            BlReportdata.DatasetName = "INVENTORYREPORTS";
            string s = BlReportdata.Title;
            BlReportdata.Title = s;
    
            if (BlReportdata.ReportFile.Length > 0)
            {
                reportDocument.Load(BlReportdata.ReportPath.Trim() + BlReportdata.ReportFile.Trim());
            }
            reportDocument.DataDefinition.FormulaFields["Title"].Text = "'" + BlReportdata.Title.ToString() + "'";
            reportDocument.DataDefinition.FormulaFields["PageTitle"].Text = "'" + BlReportdata.PageTitle.ToString() + "'";
           // reportDocument.DataDefinition.FormulaFields["User"].Text = "";
           // reportDocument.DataDefinition.FormulaFields["Date"].Text = "";
            reportDocument.SetDataSource(BlReportdata.Getprintdatasource(StatusId, type));
            CrystalReportViewer1.ReportSource = reportDocument;
            CrystalReportViewer1.DataBind();
            ExportCrystalReport(reportDocument);
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