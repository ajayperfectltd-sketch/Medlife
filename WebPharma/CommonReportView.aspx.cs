//using CrystalDecisions.CrystalReports.Engine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebPharma.Business;
using System.Data;
using Microsoft.Reporting.WebForms;
using WebPharma.Business.Masters;
using System.IO;

public partial class CommonReportView : System.Web.UI.Page
{

    #region Functions
    private void ShowReport()
    {
      
        BlUsers blusers = new BlUsers();
        BlReportData BlReportData;
        //ReportDocument reportDocument = new ReportDocument();
      
       BlReportData = (BlReportData)Session["ReportData"];

        try
        {  /* ----------------------------------For Crystal Report-------------------------------------------------------*/
            //if (BlReportData.ReportFile.Length > 0)
            //{
            //    reportDocument.Load(BlReportData.ReportPath.Trim() + BlReportData.ReportFile.Trim());

            //}
            //reportDocument.SetDataSource(BlReportData.GetDataSource());
            //reportDocument.DataDefinition.FormulaFields["Title"].Text = "'" + BlReportData.Title.ToString() + "'";
            //reportDocument.DataDefinition.FormulaFields["PageTitle"].Text = "'" + BlReportData.PageTitle.ToString() + "'";
            ////if (Session["Name"].ToString() != null)
            ////{
            ////    reportDocument.DataDefinition.FormulaFields["User"].Text = "'" + Session["Name"].ToString() + "'";
            ////}

            //CrystalReportViewer1.ReportSource = reportDocument;
            //CrystalReportViewer1.DataBind(); 

            /*----------------------------------For SSRS------------------------------------------------------------------*/

           

            ReportViewer1.Visible = true;
            if (BlReportData.ReportFile.Length > 0)
            {
                ReportViewer1.LocalReport.ReportPath = BlReportData.ReportPath.Trim() + BlReportData.ReportFile.Trim();
            }
            blusers.MasterId = Convert.ToInt64(Request.QueryString["uid"]);
            blusers.FK_Client = Convert.ToInt64(Request.QueryString["clnt"]);
            blusers.SelectData();
            String str = Request.QueryString["datepass"].ToString();
            ReportViewer1.ProcessingMode = ProcessingMode.Local;
            ReportDataSource ds = new ReportDataSource(BlReportData.ReportTable, BlReportData.GetDataSource().Tables[0]);
            ReportViewer1.LocalReport.DataSources.Clear();
            ReportParameter p1 = new ReportParameter("Title", BlReportData.Title.ToString());
            ReportParameter p2 = new ReportParameter("PageTitle", BlReportData.PageTitle.ToString());
            ReportParameter p3 = new ReportParameter("User", blusers.Name.ToString());
            ReportParameter p4 = new ReportParameter("Date", str);
            this.ReportViewer1.LocalReport.SetParameters(new ReportParameter[] { p1, p2, p3, p4 });
            ReportViewer1.ShowPrintButton = true;
            ReportViewer1.ShowRefreshButton = true;
            ReportViewer1.ShowZoomControl = true;
            ReportViewer1.ShowToolBar = true;
            ReportViewer1.LocalReport.DataSources.Add(ds);
            ReportViewer1.DataBind();
            ReportViewer1.LocalReport.Refresh();


            string mimeType, encoding, extension, deviceInfo;
            string[] streamids; Microsoft.Reporting.WebForms.Warning[] warnings;
            string format = "PDF"; //Desired format goes here (PDF, Excel, or Image)
            deviceInfo = "<DeviceInfo>" + "<SimplePageHeaders>True</SimplePageHeaders>" +
                         "</DeviceInfo>";
            byte[] bytes = ReportViewer1.LocalReport.Render(format, deviceInfo, out mimeType,
                           out encoding, out extension, out streamids, out warnings);
            Response.Clear();

            if (format == "PDF")
            {
                Response.ContentType = "application/pdf";
                Response.AddHeader("Content-disposition", "filename=output.pdf");
            }
            Response.OutputStream.Write(bytes, 0, bytes.Length);
            Response.OutputStream.Flush();
            Response.OutputStream.Close();
            Response.Flush();
            Response.Close();




           // Warning[] warnings;
           // string[] streamids;
           // string mimeType;
           // string encoding;
           // string filenameExtension;


           //// ReportParameterInfoCollection pInfo = ReportViewer1.ServerReport.GetParameters();
           // string filenameParams = "";


           // byte[] bytes;
           // if (ReportViewer1.ProcessingMode == ProcessingMode.Local)
           // {
           //     bytes = ReportViewer1.LocalReport.Render("PDF", null, out mimeType,
           //      out encoding, out filenameExtension, out streamids, out warnings);
           // }
           // else
           // {
           //     bytes = ReportViewer1.ServerReport.Render("PDF", null, out mimeType,
           //      out encoding, out filenameExtension, out streamids, out warnings);
           // }


           // string filename = Path.Combine(Server.MapPath("~\\Temp\\"), filenameParams  + "report.pdf");
           // using (FileStream fs = new FileStream(filename, FileMode.Create))
           // { fs.Write(bytes, 0, bytes.Length); }

           // System.IO.FileInfo file = new System.IO.FileInfo(filename); //-- if the file exists on the server  




            //ParameterValue[] reportHistoryParameters = null;
            //string[] streamIDs = null;
            //SessionHeader sh = new SessionHeader();
            //rs.SessionHeaderValue = sh;


            //string strExp = BlReportData.Title;
            //string[] streamids;
            //string mimeType;
            //string encoding;
            //string extension;
            //string filename;
            //Warning[] warnings;
            //Response.Buffer = false;
            //Response.BufferOutput = false;
            //Response.Charset = "UTF-8";
            //Response.ContentEncoding = System.Text.Encoding.UTF8;
            //byte[] bytes = ReportViewer1.LocalReport.Render("Pdf", null, out mimeType, out encoding, out extension, out streamids, out warnings);
                    
                     
                   

            //filename = string.Format("{0}.{1}", strExp, "pdf");
            //Response.ClearHeaders();
            //Response.Clear();

            //Response.AddHeader("Content-Disposition", "attachment;filename=" + filename);
            //Response.ContentType = mimeType;
            //// Response.BinaryWrite(bytes);
            //// Response.Flush();


            //Response.BinaryWrite(bytes);
            //Response.Flush();
            //Response.End();
           
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "alertify.alert('Session Timed Out')", true);
        }
    }
    #endregion
    #region Events
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ShowReport();
        }

    }
    #endregion
}