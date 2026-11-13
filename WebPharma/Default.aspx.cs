/*----------------------------------------------------------------------------------------------------------------------------------------------------------
Purpose    : login screen
Author     : bibin tc
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
using System.Data;
using WebPharma.Business.Masters;
using System.IO;
using System.Text;
using System.Security.Cryptography;
using System.Configuration;
using System.Web.Security;
using System.Collections;
using System.Xml;
using System.Globalization;
using WebPharma.Business;

public partial class _Default : System.Web.UI.Page
{
    #region Functions
    private static int checklogin = 0;
    private static int otpResendAttempt = 0;
    private static int otpVerifyAttempt = 3;
    private static int otpLength = 6;
    private static int otpValidity = 60;
    protected String EncryptData(string strPassword)
    {
        string tempPass;
        tempPass = "";
        tempPass = FormsAuthentication.HashPasswordForStoringInConfigFile(strPassword, "SHA1");
        return tempPass;
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
    protected void LoginCheck()
    {
         //DateTime CurrentDate = DateTime.Now.Date;
         //DateTime ExpiredDate = Convert.ToDateTime("11/08/2023");
         //if (CurrentDate > ExpiredDate)
         //{
         //    blMessage.Items.Add("Liscence Expired!");

         //}
         //else
         //{
             BlUsers blusers = new BlUsers();
             BlUserGroup blusergroup = new BlUserGroup();
             blusers.Username = txtUsername.Text.Trim().ToLower();
             // ViewState["password"] = txtPassword.Text.Trim();
             //   blusers.Password = Encrypt(ViewState["password"].ToString());
             blusers.Password = Encrypt(txtPassword.Text.Trim());

             //if ((blusers.Username == "PSS" || blusers.Username == "pss") && blusers.Password == blusers.DailyPassword)
             //{
             DailyPassword DailyPassword = new DailyPassword();
             if (txtUsername.Text.Trim().ToLower() == "supp")
             {
                 blusers.DailyPassword = Encrypt(DailyPassword.SupportPassword());
             }
             else
             {
                 blusers.DailyPassword = Encrypt(DailyPassword.SoftwareVendorPassword());
             }
             DailyPassword = null;
             //}


             blusers.LoginCheck();
             Session["storetype"] = blusers.MasterId.ToString();
             Session["UsrName"] = blusers.Name;

             blusergroup.MasterId = blusers.FK_UserGroup;
             blusergroup.SelectData();
             Session["UserGroup"] = blusergroup.GroupName;
             Session["MultiStore"] = blusergroup.UgMultiStoreLogin.ToString();

             if (blusers.MasterId > 0)
             {
                 if (blusergroup.UgMultiStoreLogin == true)
                 {
                     // divMenuButtons.Enabled = false;
                     mvLogin.ActiveViewIndex = 1;
                     divPopupOnstores.Visible = true;
                     Populate Populate = new Populate();
                     CommonPopupOnStore.CallingFunction = "PopupOnStore";
                     ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "PopupOnStore();", true);
                     mpCommon.Show();

                 }
                 else
                 {


                     string userid = HttpUtility.UrlEncode(Encrypt(blusers.MasterId.ToString()));
                     string name = HttpUtility.UrlEncode(Encrypt(blusers.Name));
                     string login = HttpUtility.UrlEncode(Encrypt(blusers.LoginTime.ToString()));
                     string lastpassword = HttpUtility.UrlEncode(Encrypt(blusers.LastPasswordChange.ToString()));
                     string client = HttpUtility.UrlEncode(Encrypt(blusers.FK_Client.ToString()));
                     string usertype = HttpUtility.UrlEncode(Encrypt(blusergroup.Administrator.ToString()));
                     blusers.UserAction = 1;
                     blusers.MasterId = blusers.MasterId;
                     blusers.LoginTime = DateTime.Now;
                     blusers.FK_Client = blusers.FK_Client;
                     string changepassword = blusers.UpdateLoginDetails().ToString();
                     Session["uid"] = userid;
                     Session["name"] = blusers.Username;
                     Session["UserName"] = blusers.Username;
                     BlFunction blfunctions1 = new BlFunction();
                     Int16 store = Convert.ToInt16(blfunctions1.GetScalarValue("Users", " isnull((select FK_Store", "Active=1 and ID_Users=" + blusers.MasterId + "),0)", ""));
                     Session["mainstore"] = store.ToString();


                     Response.Redirect(string.Format("Home.aspx?nme={0}&lstin={1}&lstpwd={2}&uid={3}&clnt={4}&chgpwd={5}&utyp={6}", name, login, lastpassword, userid, client, changepassword, usertype));
                 }
             }
             else
             {
                 blMessage.Items.Add("Incorrect username/password");
                 txtUsername.Focus();
                 // long AuditTrailUpdation = blusers.AuditTrailUpdation();
             }

             blusers = null;
             blusergroup = null;
        // }

        // txtPassword.Text = ViewState["password"].ToString();
    }
    #endregion

    #region Events
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            LoginChecks();

            if (Session["UserId"] != null)
            {
                Response.Redirect("Home.aspx");
            }

            
            if (!IsPostBack)
            {

                PSSLicensing();
                blMessage.Items.Clear();
                txtUsername.Focus();
                mvLogin.ActiveViewIndex = 0;

                //if (Licensing.Reseller == (Int16)Licensing.eReseller.NC)
                //{
                //    ScriptManager.RegisterStartupScript(Page, GetType(), "success", "StockAwayLogo();", true);//NCode And CMore
                //}
            }
        }
        catch (Exception ex)
        {
            blMessage.Items.Add(ex.Message);
        }
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {

            blMessage.Items.Clear();
            PSSUserLicenseCheck();                      //User License Check
            PSSMachineLicenseCheck();                   //Machine License Check
            PSSBranchLicenseCheck();
            if (!PSSSerialKeyLicenseCheck()) return;    //SerialKey Check
            LoginCheck();


        }
        catch (Exception ex)
        {
            blMessage.Items.Add(ex.Message);
        }
    }
    #endregion

    #region License
    protected void btnFinish_Click(object sender, EventArgs e)
    {
        try
        {
            string SerialKeyPss;
            string SerialKeyClient;
            string ProdID;
            SerialKeyPss = txtBox1.Text + txtBox2.Text + txtBox3.Text + txtBox4.Text + txtBox5.Text;

            ProdID = hidProductID.Value.Replace("-", "");
            SerialKeyClient = Licensing.GetMD5ProductID(ProdID).Replace("-", "").Substring(0, 20);
            if (SerialKeyClient == SerialKeyPss)
            {
                if (Licensing.InsertSerialKey(SerialKeyClient))
                {
                    string script = "<script>Successpopup();</script>";
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "success", script, false);
                }
                else
                {
                    string script = "<script>AlertMsg();</script>";
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "success", script, false);
                }
            }
            else
            {
                string script = "<script>AlertMsg();</script>";
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "success", script, false);
            }
        }
        catch (Exception)
        {
            throw;
        }
    }
    protected void btnSkip_Click(object sender, EventArgs e)
    {
        try
        {

            int daysWithSkip = (Convert.ToDateTime(DateTime.Now.ToShortDateString()) - Convert.ToDateTime(Licensing.InstallationDate.ToString())).Days;

            if (daysWithSkip > 0)
            {
                hidSkip.Value = "1";
                string script = "<script>WaitSeconds(" + daysWithSkip * 10 + ");</script>";
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "success", script, false);
            }
        }
        catch (Exception)
        {
            throw;
        }
    }
/*=================Checking SCORE License============================
     26035    Ajith K       16-05-2013         
====================================================================*/
    private void PSSLicensing()
    {
        try
        {
            Licensing.Initialize();
            Licensing.CheckLicensedOrNot();
            if (Licensing.Licensed == true)
            {
                Licensing.GetLicense();//License Details To  PUBLIC static Datatable dtLicense

                PSSUserLicenseCheck();
                PSSMachineLicenseCheck();
                PSSBranchLicenseCheck();
                PSSDaysLicenseCheck();
            }
            else
            {
                Licensing.ErrorMessage = Licensing.ErrorMessage = "Pharma not licensed ! If you bought the License Please select and update your License now !";
                Response.Redirect("Licensing.aspx");
            }
        }
        catch (Exception ex)
        {
            throw;
        }
    }
    private void PSSUserLicenseCheck()
    {
        if (Licensing.Licensed == true)
        {
            if (Licensing.LicenseMode == Licensing.eLicenseMode.User.ToString())
            {
                Licensing.CheckUserLicense();
            }
            if (Licensing.LicenseMode == Licensing.eLicenseMode.Days.ToString())
            {
                Licensing.CheckDayLicense();
            }
        }
    }
    private void PSSDaysLicenseCheck()
    {
        if (Licensing.Licensed == true)
        {
            if (Licensing.LicenseMode == Licensing.eLicenseMode.Days.ToString())
            {
                Licensing.CheckDayLicense();
            }
        }
    }
    private void PSSMachineLicenseCheck()
    {
        if (Licensing.Licensed == true)
        {
            if (Licensing.LicenseMode == Licensing.eLicenseMode.Machine.ToString())
            {
                Licensing.CheckMachineLicense();
            }
        }
    }
    private void PSSBranchLicenseCheck()
    {
        if (Licensing.Licensed == true)
        {
            if (Licensing.LicenseMode == Licensing.eLicenseMode.Branch.ToString())
            {
                string CompanyName = "";
                DataTable dtsettings = new DataTable();
                BlSettings blsettings = new BlSettings();
                blsettings.FK_Client = 1;
                blsettings.Module = "MAS";
                blsettings.SubModule = "COM";
                dtsettings = blsettings.SelectSettings();
                if (dtsettings.Rows.Count > 0)
                {
                    CompanyName = dtsettings.Rows[0]["Value"].ToString() == "" ? "0" : dtsettings.Rows[0]["Value"].ToString();
                }
                Licensing.CheckBranchLicense(CompanyName);
            }
        }
    }   

    private bool PSSSerialKeyLicenseCheck()
    {
        if (Licensing.Licensed == true)
        {
            btnEvaluation.Visible = (Licensing.Edition == Licensing.eEdition.Standard.ToString() && Licensing.LicenseMode == Licensing.eLicenseMode.Branch.ToString() && Licensing.SerialKey == "") ? true : false;

            if (Licensing.LicenseMode == "Branch" && Licensing.EvaluationPeriod > 3)
            {
                txtPassword.Attributes.Add("UserPwd", txtPassword.Text);
                hidProductID.Value = Licensing.GenerateProductID();
                string script = "<script>SerialKeyPopup();</script>";
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "success", script, false);
                return false;
            }

            else if ((Licensing.LicenseMode == "Branch" && Licensing.EvaluationPeriod > 0) && (Convert.ToDateTime(DateTime.Now.ToShortDateString()) < Convert.ToDateTime(Licensing.InstallationDate.ToString()) ||
                Convert.ToDateTime(DateTime.Now.ToShortDateString()) > Convert.ToDateTime(Licensing.ExpiryDate.ToString())))
            {
                txtPassword.Attributes.Add("UserPwd", txtPassword.Text);
                hidProductID.Value = Licensing.GenerateProductID();
                string script = "<script>SerialKeyPopup();</script>";
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "success", script, false);
                //bltErrorList.Items.Clear();
                return false;
            }

            else if (Licensing.SerialKey == "" && hidSkip.Value != "1" && Licensing.EvaluationPeriod == 0)
            {
                txtPassword.Attributes.Add("UserPwd", txtPassword.Text);
                hidProductID.Value = Licensing.GenerateProductID();
                string script = "<script>SerialKeyPopup();</script>";
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "success", script, false);
                //bltErrorList.Items.Clear();
                return false;
            }
            else if (Licensing.SerialKey != "" && Licensing.EvaluationPeriod == 0)
            {
                string SerialKey;
                string ProductID;
                hidProductID.Value = Licensing.GenerateProductID();
                ProductID = hidProductID.Value.Replace("-", "");
                SerialKey = Licensing.GetMD5ProductID(ProductID).Replace("-", "").Substring(0, 20);
                if (Licensing.SerialKey != SerialKey)
                {
                    string script = "<script>SerialKeyPopup();</script>";
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "success", script, false);
                    //bltErrorList.Items.Clear();
                    return false;
                }

            }
        }
        hidSkip.Value = "";
        return true;
    }
    protected void btnEvaluation_Click(object sender, EventArgs e)
    {
        try
        {
            if (Licensing.SerialKey == "")
            {
                if (Licensing.EvaluationPeriod <= 3)
                {
                    if (Licensing.InsertEvaluationPeriod(Convert.ToInt16(Licensing.EvaluationPeriod)))
                    {
                        string script = "<script>SuccessEvaluationpopup();</script>";
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "success", script, false);
                    }
                    else
                    {
                        string script = "<script>AlertEvaluationMsg();</script>";
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "success", script, false);
                    }
                }
                else
                {
                    string script = "<script>AlertEvaluationMsg();</script>";
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "success", script, false);
                }
            }
        }
        catch (Exception)
        {
            throw;
        }
    }

    public void Storeselect(string reason = "0")
    {
        HdValuestore.Value = reason;
        Session["mainstore"] = reason;

        if (Convert.ToInt16(HdValuestore.Value.Trim()) > 0)
        {
            BlUsers blusers = new BlUsers();
            BlUserGroup blusergroup = new BlUserGroup();
            blusers.Username = txtUsername.Text.Trim().ToLower();
            // blusers.Password = Encrypt(txtPassword.Text.Trim());

            if (Session["password"] == null)
            {
                LoginPageActivate();
            }
            else
            {
                blusers.Password = Encrypt(Session["password"].ToString());

                DailyPassword DailyPassword = new DailyPassword();
                if (txtUsername.Text.Trim().ToLower() == "supp")
                {
                    blusers.DailyPassword = Encrypt(DailyPassword.SupportPassword());
                }
                else
                {
                    blusers.DailyPassword = Encrypt(DailyPassword.SoftwareVendorPassword());
                }

                DailyPassword = null;

                blusers.LoginCheck();
                blusergroup.MasterId = blusers.FK_UserGroup;

                blusergroup.SelectData();



                if (blusers.MasterId > 0)
                {
                    string userid = HttpUtility.UrlEncode(Encrypt(blusers.MasterId.ToString()));
                    string name = HttpUtility.UrlEncode(Encrypt(blusers.Name));
                    string login = HttpUtility.UrlEncode(Encrypt(blusers.LoginTime.ToString()));
                    string lastpassword = HttpUtility.UrlEncode(Encrypt(blusers.LastPasswordChange.ToString()));
                    string client = HttpUtility.UrlEncode(Encrypt(blusers.FK_Client.ToString()));
                    string usertype = HttpUtility.UrlEncode(Encrypt(blusergroup.Administrator.ToString()));
                    blusers.UserAction = 1;
                    blusers.MasterId = blusers.MasterId;
                    blusers.LoginTime = DateTime.Now;
                    blusers.FK_Client = blusers.FK_Client;
                    string changepassword = blusers.UpdateLoginDetails().ToString();
                    Session["uid"] = userid;
                    Session["name"] = blusers.Username;
                    Session["UserName"] = blusers.Username;

                    Session["UserGroup"] = blusergroup.GroupName.ToString();
                    Response.Redirect(string.Format("Home.aspx?nme={0}&lstin={1}&lstpwd={2}&uid={3}&clnt={4}&chgpwd={5}&utyp={6}", name, login, lastpassword, userid, client, changepassword, usertype));
                }
                else
                {
                    blMessage.Items.Add("Incorrect username/password");
                    txtUsername.Focus();
                }

                blusers = null;
                blusergroup = null;
            }
        }
        else
        {
            LoginPageActivate();
        }


    }

    public void LoginPageActivate()
    {
        txtPassword.Text = "";
        txtUsername.Text = "";
        blMessage.ClearSelection();
        mvLogin.ActiveViewIndex = 0;
        txtUsername.Focus();
    }




    protected void LoginChecks()
    {


        BlUsers blusers = new BlUsers();
        DailyPassword DailyPassword = new DailyPassword();
        if (txtUsername.Text.Trim().ToLower() == "supp")
        {
            blusers.DailyPassword = Encrypt(DailyPassword.SupportPassword());
        }
        else
        {
            blusers.DailyPassword = Encrypt(DailyPassword.SoftwareVendorPassword());
        }
        DailyPassword = null;
        blusers.Username = txtUsername.Text.Trim().ToLower();

        if (txtPassword.Text != "")
        {

            Session["password"] = txtPassword.Text.Trim();
            blusers.Password = Encrypt(Session["password"].ToString());
        }
        else
        {
            blusers.Password = Encrypt("");
        }




        blusers.LoginCheck();
        Session["storetype"] = blusers.MasterId.ToString();
    }


    #endregion
}