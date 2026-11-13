/*----------------------------------- Business Layer Class -----------------------------------------------------------------------------------------------------------------------
Purpose    : Create a Crystal Report Template.It must contain<BR>1.Design of Type,Settings,Ledger and Transaction report(Font:-Size,Bold)<BR>2.Page Design<BR>3.Passing of Report Title,Sub Title and Page Footer Details.<BR>4.Option for Image Binding in CR
Author     : BIBIN tC
Copyright  : Perfect Software Solutions (Clt) Pvt Ltd
Created on : 15/05/2018
----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
WebPharma
On			By			OMID
----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------


--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------*/

using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using System.IO;
using System.Drawing.Printing;
using CrystalDecisions.Web;
using WebPharma.Business.Masters;
using WebPharma.Business;
public partial class Presentation_Common_ReportView : System.Web.UI.Page
{
    string reportname = ""; 
    protected void Page_Load(object sender, EventArgs e)
    {

        if (!IsPostBack)
        {
            ShowReport();
        }
    }

    private void ShowReport()
    {
        reportname = ""; 
        BlUsers blusers = new BlUsers();
        BlReportData BlReportData;
        ReportDocument reportDocument = new ReportDocument();
        BlReportData = (BlReportData)Session["ReportData"];
        reportname = BlReportData.ReportFile;
        Populate populate = new Populate();
        try
        {
            if (BlReportData.ReportFile.Length > 0)
            {

                reportDocument.Load(Server.MapPath("Reports/").Trim() + BlReportData.ReportFile.Trim());
            }
            blusers.MasterId = Convert.ToInt64(Request.QueryString["uid"]);
            blusers.FK_Client = Convert.ToInt64(Request.QueryString["clnt"]);
            blusers.SelectData();
            String str = Convert.ToString(DateTime.Now);
            String UserName = Session["Username"].ToString();
            reportDocument.SetDataSource(BlReportData.GetDataSource());
            reportDocument.DataDefinition.FormulaFields["Title"].Text = "'" + BlReportData.Title.ToString() + "'";
            reportDocument.DataDefinition.FormulaFields["PageTitle"].Text = "'" + BlReportData.PageTitle.ToString() + "'";
            reportDocument.DataDefinition.FormulaFields["User"].Text = "'" + UserName + "'";
            reportDocument.DataDefinition.FormulaFields["Date"].Text = "'" + str + "'";
            CrystalReportViewer1.ReportSource = reportDocument;
            CrystalReportViewer1.DataBind();
            ExportCrystalReport(reportDocument);

            populate = null;
        }
        catch (Exception ex)
        {
            WriteToLog(reportname + "::" + ex.Message);
            // ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "alert('Timed Out')", true);
        }
    }  
    private static object locker = new object();
    public static void WriteToLog(String data)
    {
        string path = System.Web.HttpContext.Current.Server.MapPath("~/ReportLog");
        string FilePath = path + "\\" + DateTime.Now.ToString("dd-MM-yyyy") + ".txt";
        if (!System.IO.Directory.Exists(path))
        {
            System.IO.Directory.CreateDirectory(path);
        }
        if (!System.IO.File.Exists(FilePath))
        {
            System.IO.FileStream fs = System.IO.File.Create(FilePath);
            fs.Close();
        }
        lock (locker)
        {
            using (System.IO.StreamWriter file = new System.IO.StreamWriter(FilePath, true))
            {
                file.WriteLine(DateTime.Now.ToString() + "::" + data);
                file.Flush();
                file.Close();
            }
        }
    }
    private void ExportCrystalReport(ReportDocument reportDocument)
    {

        try
        {
            if (Convert.ToInt64(Request.QueryString["Rptype"]) == 0)
            {
                reportDocument.ExportToHttpResponse(ExportFormatType.PortableDocFormat, Response, false, "");
            }
            else if (Convert.ToInt64(Request.QueryString["Rptype"]) == 1)
            {
                reportDocument.ExportToHttpResponse(ExportFormatType.CrystalReport, Response, false, "");
            }
            else if (Convert.ToInt64(Request.QueryString["Rptype"]) == 2)
            {
                reportDocument.ExportToHttpResponse(ExportFormatType.Excel, Response, false, "");
            }
            else if (Convert.ToInt64(Request.QueryString["Rptype"]) == 3)
            {
                reportDocument.ExportToHttpResponse(ExportFormatType.ExcelRecord, Response, false, "");
            }
            else if (Convert.ToInt64(Request.QueryString["Rptype"]) == 4)
            {
                reportDocument.ExportToHttpResponse(ExportFormatType.ExcelWorkbook, Response, false, "");
            } 
            Response.End();
            Response.Close();

        }
        catch (System.Threading.ThreadAbortException)
        {
            //ThreadException can happen for internale Response implementation
        }
        catch (Exception ex)
        {
            WriteToLog(reportname + "::" + ex.Message);
            throw ex;
        }
    }
}
