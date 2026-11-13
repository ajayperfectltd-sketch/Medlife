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
using System.IO;

public partial class Presentation_Licensing_Licensing : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (Licensing.ErrorMessage == null)
                Response.Redirect("~/Default.aspx", false);
            lblErrorMessage.Text = Licensing.ErrorMessage;
            divErrorBox.Visible = true;
            divSuccessBox.Visible = false;
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    protected void btnActivate_Click(object sender, EventArgs e)
    {
        if (FileUpload.HasFile)
        {
            bool lic = false;
            string filePath = Path.GetFullPath(FileUpload.FileName.ToString());
            string path = Server.MapPath("Licensing\\PSS");
            FileUpload.SaveAs(path + "\\" + "PSS.License");
            StreamReader streamLicense = new StreamReader(path + "\\" + "PSS.License");
            string LicenseToExecute = streamLicense.ReadToEnd();
            LicenseToExecute = Security.DecryptLicense(LicenseToExecute);
            lic = Licensing.InsertLicense(LicenseToExecute);
            if (lic == true)
            {
                string script = "<script>Close();</script>";
                ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "success", script, false);
                divErrorBox.Visible = false;
                divSuccessBox.Visible = true;
            }
            else
            {

            }
            streamLicense.Close();
        }
    }
    protected void lnkGoHome_Click(object sender, EventArgs e)
    {
        try
        {
            if (Session["UserCode"] == null)
                Response.Redirect("~/Default.aspx");
            else
                Response.Redirect("~/Default.aspx" + (Request.QueryString == null ? "" : "?" + Request.QueryString));
        }
        catch (Exception ex)
        {
            throw;
        }
    }
}
