
/*----------------------------------------------------------------------------------------------------------------------------------------------------------
Purpose    : Change Password with min and max Password length
Author     : Bibin TC
Copyright  : Perfect Software Solutions (Clt) Pvt Ltd
Created on : 03/05/2018
----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
WebPharma
On			 By			      OMID
----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

------------------------------------------------------------------------------------------------------------------------------------------------------------ */
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebPharma.Business.Masters;

public partial class ChangePassword : System.Web.UI.Page
{
    #region Functions
    protected String EncryptData(string strPassword)
    {
        string tempPass;
        tempPass = "";
        tempPass = FormsAuthentication.HashPasswordForStoringInConfigFile(strPassword, "SHA1");
        return tempPass;
    }
    #endregion
    #region Events
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            hfUser.Value = Request.QueryString["uid"];
            hfClient.Value = Request.QueryString["clnt"];
            txtCurrentPassword.Text = "";
        }
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


    protected void btnSubmit_Click(object sender, EventArgs e)
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



                blusers.Password = Encrypt(txtCurrentPassword.Text);
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

                ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "toastr.error('Minimum Password length is " + hfSEC01.Value + " and Maximum Password length is " + hfSEC02.Value + "');ClearAdd();", true);
            }

        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "toastr.warning('" + ex.Message + "')", true);
        }
    }
    #endregion
}