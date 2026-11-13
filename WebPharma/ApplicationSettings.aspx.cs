/*-------------------------------------------------------------------
Purpose    : Settings in Textiles
Author     :Bibin TC
Copyright  : Perfect Software Solutions (Clt) Pvt Ltd
Created on : 03/05/2018
----------------------------------------------------------------------
WebPharma
OMID	On			By		
----------------------------------------------------------------------

----------------------------------------------------------------------*/
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
using System.Globalization;
using System.Threading;

public partial class ApplicationSettings : System.Web.UI.Page
{
    #region Functions
    protected void BindData()
    {
        Populate populate = new Populate();
        populate.FillDropdownList(ddlCurrency, "Currency", "Name", "ID_Currency", "ID_Currency", "FK_Client="+hfClient.Value, "");
        populate = null;

        blMessage.Items.Clear();
        DataTable dtbl = new DataTable();
        BlSettings blsettings = new BlSettings();

        blsettings.FK_Client = Convert.ToInt64(hfClient.Value);
        blsettings.UserCode = hfUser.Value;
        //blsettings.FK_Client = 1;
        //blsettings.UserCode = "1";

        blsettings.Module = "GCFA";
        blsettings.SubModule = "GCFA";
        dtbl = blsettings.SelectSettings();

        if (dtbl.Rows.Count > 0)
        {
            ddlCurrency.SelectedValue = dtbl.Rows[0]["Value"].ToString();
            txtDecimalPlace.Text = dtbl.Rows[1]["Value"].ToString();
            txtNoUsers.Text = dtbl.Rows[2]["Value"].ToString();
        }

        chkSYSVoucher.Focus();
        blsettings.Module = "SYS";
        blsettings.SubModule = "SYS";
        dtbl = blsettings.SelectSettings();

        if (dtbl.Rows.Count > 0)
        {
            string str = dtbl.Rows[0]["Value"].ToString();
            string[] words = str.Split(',');
            foreach (string word in words)
            {
                if (word != "")
                {
                    chkSYSVoucher.Items[Convert.ToInt16(word)].Selected = true;
                }
            }
            string str2 = dtbl.Rows[1]["Value"].ToString();
            string[] words2 = str2.Split(',');
            foreach (string word in words2)
            {
                if (word != "")
                {
                    chkSYSInvoice.Items[Convert.ToInt16(word)].Selected = true;
                }
            }


        }

    }
    protected void SaveData(string Module, string SubModule, string Name, string Value, byte DeleteFlag)
    {
        BlSettings blsettings = new BlSettings();
        blsettings.Module = Module;
        blsettings.SubModule = SubModule;
        blsettings.DeleteFlag = DeleteFlag;
        blsettings.Name = Name;
        blsettings.Value = Value;
        blsettings.FK_Client = Convert.ToInt64(hfClient.Value);
        blsettings.UserCode = hfUser.Value;
        blsettings.UpdateData();
        blsettings = null;
    }
    protected void CheckUserAccess()
    {
        BlUserAccess bluseraccess = new BlUserAccess();
        Populate populate = new Populate();

        bluseraccess.FK_Users = Convert.ToInt64(hfUser.Value);
        bluseraccess.FK_Pages = 47;
        bluseraccess.FK_Client = Convert.ToInt64(hfClient.Value);
        bluseraccess.CheckUserAccess();
        //if (Convert.ToBoolean(bluseraccess.IsView) == false)
        //{
        //    // Response.Redirect("Home.aspx?");

        //}
        hfIsAdd.Value = (Convert.ToBoolean(bluseraccess.IsAdd)).ToString();
        hfIsUpdate.Value = (Convert.ToBoolean(bluseraccess.IsUpdate)).ToString();
        hfIsDelete.Value = (Convert.ToBoolean(bluseraccess.IsDelete)).ToString();
        bluseraccess = null;
    }
    #endregion

    #region Events
    protected void Page_Load(object sender, EventArgs e)
    {
         try
         {
             ddlCurrency.Focus();
            if (!IsPostBack)
            {
                hfUser.Value = Request.QueryString["uid"];
                hfClient.Value = Request.QueryString["clnt"];
                BindData();
                CheckUserAccess();
                blMessage.Items.Clear();
                ddlCurrency.Focus();
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "toastr.warning('" + ex.Message + "')", true); 
        }
    }
    protected void btnSubmit_Click(Object sender, EventArgs e)
    {
        Boolean cheked = false;
        try
        {

            if (hfIsUpdate.Value.ToLower() == "true")
            {

                blMessage.Items.Clear();
            
                SaveData("GCFA", "GCFA", "GCFA01", ddlCurrency.SelectedValue.ToString(), 1);
                SaveData("GCFA", "GCFA", "GCFA02", txtDecimalPlace.Text.Trim(), 2);
                SaveData("GCFA", "GCFA", "GCFA03", txtNoUsers.Text.Trim(), 2);

                string str = "";
                string str2 = "";
                foreach (ListItem item in chkSYSVoucher.Items)
                {
                    if (item.Selected == true)
                    {
                        cheked = true;
                        if (str != "")
                        {
                            str = str + ",";
                        }
                        str = str + item.Value.ToString();
                    }
                }
              if( cheked == true)
              {
                  cheked = false;
                foreach (ListItem item2 in chkSYSInvoice.Items)
                {
                    if (item2.Selected == true)
                    {
                        cheked = true;
                        if (str2 != "")
                        {
                            str2 = str2 + ",";
                        }
                        str2 = str2 + item2.Value.ToString();
                    }
                }
              }
              if (cheked == true)
              {
                  SaveData("SYS", "SYS", "SYS01", str, 1);
                  SaveData("SYS", "SYS", "SYS02", str2, 2);
                  ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "toastr.success('Data Saved successfully')", true);
              }
              else
              {
                  ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "toastr.warning('Select at least one Invoice and Voucher No Generation option')", true);
              }

            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "toastr.warning('User does not have permission to Update')", true);

            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "toastr.warning('" + ex.Message + "')", true); 
        }
    }
    #endregion

}