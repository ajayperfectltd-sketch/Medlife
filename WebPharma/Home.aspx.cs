/*----------------------------------------------------------------------------------------------------------------------------------------------------------
Purpose    : Home page with menu
Author     : Bibin TC
Copyright  : Perfect Software Solutions (Clt) Pvt Ltd
Created on : 03/05/2018
----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
WebPharma
On			By			OMID
----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Security.Cryptography;
using System.IO;
using System.Web.Security;
using System.Collections;
using System.Xml;
using System.Globalization;
using System.Data;
using System.Text;
using WebPharma.Business.Masters;
using WebPharma.Business;



public partial class Home : System.Web.UI.Page
{
    #region Functions
    public HtmlControl iframe
    {
        get
        {
            return this.myIframe;
        }
    }
    private string Decrypt(string cipherText)
    {
        string EncryptionKey = "P321PLATINUM123CLT";
        cipherText = cipherText.Replace(" ", "+");
        byte[] cipherBytes = Convert.FromBase64String(cipherText);
        using (Aes encryptor = Aes.Create())
        {
            Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
            encryptor.Key = pdb.GetBytes(32);
            encryptor.IV = pdb.GetBytes(16);
            using (MemoryStream ms = new MemoryStream())
            {
                using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateDecryptor(), CryptoStreamMode.Write))
                {
                    cs.Write(cipherBytes, 0, cipherBytes.Length);
                    cs.Close();
                }
                cipherText = Encoding.Unicode.GetString(ms.ToArray());
            }
        }
        return cipherText;
    }

    #endregion

    #region Events

    protected void Page_Load(object sender, EventArgs e)
    {

        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        Response.Cache.SetNoStore();
        Response.Cache.SetExpires(DateTime.UtcNow.AddDays(-1));
        Response.Cache.SetRevalidation(HttpCacheRevalidation.AllCaches);
        Response.AppendHeader("Pragma", "no-cache");
        try
        {
            if (!IsPostBack)
            {
                if (Session["uid"] != null)
                {

                    lblName.Text = Decrypt(HttpUtility.UrlDecode(Request.QueryString["nme"]));
                    lblUserName.Text = Decrypt(HttpUtility.UrlDecode(Request.QueryString["nme"]));
                    hfClient.Value = Decrypt(HttpUtility.UrlDecode(Request.QueryString["clnt"]));
                    lblLastLogin.Text = Decrypt(HttpUtility.UrlDecode(Request.QueryString["lstin"]));
                    lblLastPwdChange.Text = Decrypt(HttpUtility.UrlDecode(Request.QueryString["lstpwd"]));
                    ////  hfUser.Value = Decrypt(HttpUtility.UrlDecode(Request.QueryString["uid"]));
                    hfUser.Value = Decrypt(HttpUtility.UrlDecode(Session["uid"].ToString()));
                    // lblStore.Text = Session["mainstore"].ToString();
                    HdValuestore.Value = Session["mainstore"].ToString();
                    BlUsers bluser = new BlUsers();
                    bluser.Username = Session["UserName"].ToString();
                    DataTable dt = bluser.selectFaildLogindetails();
                    if (dt.Rows.Count > 0)
                        lblFaild.Text = dt.Rows[0]["EnteredOn"].ToString();
                    lblUsergrup.Text = Session["UserGroup"].ToString();
                    BlFunction blfunctions = new BlFunction();
                    lblStore.Text = blfunctions.GetScalarValue("Store", "ISNULL( StoName,'') StoName", "  Active=1 and Cancelled=0 and  ID_Store=" + HdValuestore.Value + "", "");
                    hfUserType.Value = Decrypt(HttpUtility.UrlDecode(Request.QueryString["utyp"]));
                    int chgPassword = Convert.ToInt32(Request.QueryString["chgpwd"].ToString());
                    if (chgPassword == 1)
                    {
                        // ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "ShowPwdMsg();", true);
                    }
                    //bindmenu();
                    // BindHomePageTitle();
                    CheckMonthlyProcessGracePeriodWarning();
                    SelectUserPhoto();
                }
                else
                {
                    Response.Redirect("Default.aspx");
                }


            }

        }
        catch (Exception)
        {

        }
    }


    #endregion
    protected void LinkButton5_Click(object sender, EventArgs e)
    {
        try
        {
            Session.Abandon();
            Session.Clear();

            System.Web.Security.FormsAuthentication.SignOut();

            Response.Redirect("~/Default.aspx");
        }
        catch (Exception ex)
        {
        }
    }

    protected void btnImage_Click(object sender, EventArgs e)
    {

    }

    protected void SelectUserPhoto()
    {
        if (hfUser.Value != "")
        {
            IUserPhoto iuserPhoto = new IUserPhoto();
            DataTable dtblEmployeePhoto = iuserPhoto.SelectUserPhoto(Convert.ToInt64(hfUser.Value.ToString()));
            if (dtblEmployeePhoto.Rows.Count > 0)
            {
                if (dtblEmployeePhoto.Rows[0]["Photo"].ToString() != "")
                    imgAttachment.ImageUrl = "~\\UploadedItems\\Users\\" + dtblEmployeePhoto.Rows[0]["Photo"].ToString();
                else
                    imgAttachment.ImageUrl = "~/UploadedItems/DefaultImage.jpg"; ;


            }
        }


    }

    /// <summary>
    /// Login-time check: warn when Monthly Process is approaching or past its grace-period due date.
    /// Does not block login. Logic is self-contained so it does not depend on a freshly rebuilt BL DLL.
    /// </summary>
    private void CheckMonthlyProcessGracePeriodWarning()
    {
        try
        {
            long fkClient = 0;
            long fkStore = 0;

            if (!string.IsNullOrEmpty(hfClient.Value))
                long.TryParse(hfClient.Value, out fkClient);
            if (!string.IsNullOrEmpty(HdValuestore.Value))
                long.TryParse(HdValuestore.Value, out fkStore);
            else if (Session["mainstore"] != null)
                long.TryParse(Session["mainstore"].ToString(), out fkStore);

            if (fkClient <= 0)
                return;
            if (fkStore <= 0)
                fkStore = 1;

            int gracePeriodDays = GetMonthlyProcessGracePeriodDays(fkClient);
            if (gracePeriodDays < 0)
                return;

            DateTime? lastProcessDate = GetLastMonthlyCloseDate(fkStore, fkClient);
            DateTime? periodFromDate = null;
            DateTime? periodToDate = null;

            try
            {
                BlMonthlyProcess blMonthlyProcess = new BlMonthlyProcess();
                blMonthlyProcess.FK_Store = fkStore;
                blMonthlyProcess.FK_Client = fkClient;
                DataTable dtActive = blMonthlyProcess.GetActiveBusinessDates();
                if (dtActive != null && dtActive.Rows.Count > 0)
                {
                    DataRow row = dtActive.Rows[0];
                    if (row.Table.Columns.Contains("FromDate") && row["FromDate"] != DBNull.Value)
                        periodFromDate = Convert.ToDateTime(row["FromDate"]).Date;
                    if (row.Table.Columns.Contains("ToDate") && row["ToDate"] != DBNull.Value)
                        periodToDate = Convert.ToDateTime(row["ToDate"]).Date;
                }
            }
            catch
            {
            }

            DateTime? referenceDate = lastProcessDate;
            if (!referenceDate.HasValue)
                referenceDate = periodToDate;
            if (!referenceDate.HasValue)
                return;

            DateTime dueDate = referenceDate.Value.Date.AddDays(gracePeriodDays);
            DateTime today = DateTime.Today;

            // Already completed for current open cycle and still before due → no warning.
            if (lastProcessDate.HasValue && periodFromDate.HasValue
                && lastProcessDate.Value.Date >= periodFromDate.Value.Date
                && today < dueDate)
            {
                return;
            }

            string dueDateText = dueDate.ToString("dd-MMM-yyyy", CultureInfo.InvariantCulture);
            bool isUrgent = today >= dueDate;
            string message = isUrgent
                ? "Urgent: Monthly Process is pending. It must be completed before " + dueDateText + ". Due date has been reached/exceeded."
                : "Warning: Monthly Process needs to be completed before " + dueDateText + " to avoid interruption of transactions.";

            // Visible banner (always shown even if JS/toastr fails)
            hfMonthlyProcessWarning.Value = message;
            hfMonthlyProcessUrgent.Value = isUrgent ? "1" : "0";
            lblMonthlyProcessWarning.Text = Server.HtmlEncode(message);
            pnlMonthlyProcessWarning.Visible = true;
            pnlMonthlyProcessWarning.Attributes["style"] =
                "position:fixed; top:0; left:0; right:0; z-index:99999; padding:12px 16px; font-family:Segoe UI, Arial, sans-serif; font-size:14px; font-weight:600; text-align:center; box-shadow:0 2px 8px rgba(0,0,0,.25); color:#fff; background-color:"
                + (isUrgent ? "#b42318;" : "#b47814;");

            // Popup is triggered once from Home.aspx document.ready via hidden fields.
        }
        catch (Exception)
        {
            // Login must not fail if grace-period validation encounters an error.
        }
    }

    private int GetMonthlyProcessGracePeriodDays(long fkClient)
    {
        int gracePeriodDays = -1;
        try
        {
            BlSettings blsettings = new BlSettings();
            blsettings.Module = "GEN";
            blsettings.SubModule = "MGP";
            blsettings.FK_Client = fkClient;
            DataTable dtGrace = blsettings.SelectSettings();
            if (dtGrace != null && dtGrace.Rows.Count > 0)
            {
                for (int i = 0; i < dtGrace.Rows.Count; i++)
                {
                    string settingName = Convert.ToString(dtGrace.Rows[i]["Name"]);
                    string settingValue = Convert.ToString(dtGrace.Rows[i]["Value"]);
                    if (string.Equals(settingName, "DAYS", StringComparison.OrdinalIgnoreCase)
                        || string.IsNullOrEmpty(settingName))
                    {
                        if (int.TryParse(settingValue, out gracePeriodDays))
                            return gracePeriodDays;
                    }
                }
                if (int.TryParse(Convert.ToString(dtGrace.Rows[0]["Value"]), out gracePeriodDays))
                    return gracePeriodDays;
            }
        }
        catch
        {
        }

        try
        {
            BlFunction blFn = new BlFunction();
            string graceValue = blFn.GetScalarValue(
                "Settings",
                "ISNULL(Value,'')",
                "Module='GEN' AND SubModule='MGP' AND Name='DAYS' AND FK_Client=" + fkClient,
                "");
            if (int.TryParse(graceValue, out gracePeriodDays))
                return gracePeriodDays;
        }
        catch
        {
        }

        return -1;
    }

    private DateTime? GetLastMonthlyCloseDate(long fkStore, long fkClient)
    {
        BlFunction blFn = new BlFunction();
        string[] criteriaList = new string[]
        {
            "FK_Store=" + fkStore + " AND FK_Client=" + fkClient,
            "FK_Store=" + fkStore,
            "FK_Client=" + fkClient,
            ""
        };

        foreach (string criteria in criteriaList)
        {
            try
            {
                string value = blFn.GetScalarValue("MonthlyClose", "MAX(Todate)", criteria, "");
                DateTime parsed;
                if (!string.IsNullOrWhiteSpace(value) && DateTime.TryParse(value, out parsed))
                    return parsed.Date;
            }
            catch
            {
            }
        }
        return null;
    }

    private string Encrypt(string clearText)
    {
        string EncryptionKey = "P321PLATINUM123CLT";
        byte[] clearBytes = Encoding.Unicode.GetBytes(clearText);
        using (Aes encryptor = Aes.Create())
        {
            Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
            encryptor.Key = pdb.GetBytes(32);
            encryptor.IV = pdb.GetBytes(16);
            using (MemoryStream ms = new MemoryStream())
            {
                using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateEncryptor(), CryptoStreamMode.Write))
                {
                    cs.Write(clearBytes, 0, clearBytes.Length);
                    cs.Close();
                }
                clearText = Convert.ToBase64String(ms.ToArray());
            }
        }
        return clearText;
    }
    protected void btnPasswordChange_Click(object sender, EventArgs e)
    {
        try
        {
            DataTable dtbl = new DataTable();

            Common common = new Common();
            dtbl = common.SelectPasswordLength();
            if (dtbl.Rows.Count > 0)
            {
                hfSEC01.Value = dtbl.Rows[0]["Value"].ToString();
                hfSEC02.Value = dtbl.Rows[1]["Value"].ToString();
            }
            blMessage.Items.Clear();
            if (((Convert.ToInt64(hfSEC01.Value)) <= txtNewPassword.Text.Length) && (txtNewPassword.Text.Length <= (Convert.ToInt64(hfSEC02.Value))))
            {
                long StatusId;
                BlUsers blusers = new BlUsers();
                blusers.MasterId = Convert.ToInt64(hfUser.Value.ToString());
                //blusers.Password = EncryptData(txtCurrentPassword.Text);
                // blusers.NewPassword = EncryptData(txtNewPassword.Text);



                blusers.Password = Encrypt(txtCurrentPswd.Text);
                blusers.NewPassword = Encrypt(txtNewPassword.Text);
                blusers.FK_Client = Convert.ToInt64(hfClient.Value);
                StatusId = blusers.ChangePassword();
                if (StatusId > 0)
                {


                    // ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "toastr.success('Password changed successfully');ClearAdd();", true);
                }
                else
                {

                    // ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "toastr.error('Incorrect Password');ClearAdd();", true);
                }
                blusers = null;
            }
            else
            {

                //ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "toastr.error('Minimum Password length is " + hfSEC01.Value + " and Maximum Password length is " + hfSEC02.Value + "');ShowPanel();", true);
            }

        }
        catch (Exception ex)
        {
            // ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "toastr.warning('" + ex.Message + "')", true);
        }
    }
    protected void btnChange_Click(object sender, EventArgs e)
    {

        // Response.Redirect("ChangePassword.aspx");
        try
        {
            DataTable dtbl = new DataTable();

            Common common = new Common();
            dtbl = common.SelectPasswordLength();
            if (dtbl.Rows.Count > 0)
            {
                hfSEC01.Value = dtbl.Rows[0]["Value"].ToString();
                hfSEC02.Value = dtbl.Rows[1]["Value"].ToString();
            }
            blMessage.Items.Clear();
            if (((Convert.ToInt64(hfSEC01.Value)) <= txtNewPassword.Text.Length) && (txtNewPassword.Text.Length <= (Convert.ToInt64(hfSEC02.Value))))
            {
                long StatusId;
                BlUsers blusers = new BlUsers();
                blusers.MasterId = Convert.ToInt64(hfUser.Value.ToString());
                //blusers.Password = EncryptData(txtCurrentPassword.Text);
                // blusers.NewPassword = EncryptData(txtNewPassword.Text);



                blusers.Password = Encrypt(txtCurrentPswd.Text);
                blusers.NewPassword = Encrypt(txtNewPassword.Text);
                blusers.FK_Client = Convert.ToInt64(hfClient.Value);
                StatusId = blusers.ChangePassword();
                if (StatusId > 0)
                {


                    ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "toastr.success('Password changed successfully');ClearAdd();", true);
                }
                else
                {

                    ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "toastr.error('Incorrect Password');ClearAdd();", true);
                }
                blusers = null;
            }
            else
            {

                ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "toastr.error('Minimum Password length is " + hfSEC01.Value + " and Maximum Password length is " + hfSEC02.Value + "');ShowPanel();", true);
            }

        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "toastr.warning('" + ex.Message + "')", true);
        }
    }
}