using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebPharma.Business.Masters;

public partial class SecSettings : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {

                if (Session["name"].ToString() == "pss")
                {
                    liStock.Style.Add("display", "block");
                    btnSubmitPurchase.Visible = true;

                }
                else
                {
                    liStock.Style.Add("display", "none");
                    btnSubmitPurchase.Visible = false;//only one list item is in purchase div so hide submit button
                } 
                hfUser.Value = Request.QueryString["uid"];
                hfClient.Value = Request.QueryString["clnt"];
            }
        }
        catch (Exception ex)
        {
        }
    }

    protected void tvSettings_SelectedNodeChanged(object sender, EventArgs e)
    {
        try
        {
            blMessage.Items.Clear();
            DataTable dtbl = new DataTable();
            BlSettings blsettings = new BlSettings();
            Populate populate = new Populate();
            blsettings.FK_Client = Convert.ToInt64(hfClient.Value);
            blsettings.UserCode = hfUser.Value;

            if (tvSettings.SelectedValue == "SLS")
            {
                blsettings.Module = "INV";
                blsettings.SubModule = "SLS";
                dtbl = blsettings.SelectSettings();
                mvSettings.SetActiveView(vwSLS);
                if (dtbl.Rows.Count > 0)
                {
                    if (dtbl.Rows.Count > 7)
                    {
                        if (dtbl.Rows[7]["Value"].ToString() == "1")
                        {
                            chkAcountPost.Checked = true;
                        }
                        else if (dtbl.Rows[7]["Value"].ToString() == "0")
                        {
                            chkAcountPost.Checked = false;
                        }
                    }
                    
                }

            }
            if (tvSettings.SelectedValue == "PUR")
            {
                blsettings.Module = "INV";
                blsettings.SubModule = "PUR";
                dtbl = blsettings.SelectSettings();
                mvSettings.SetActiveView(vwPurchase);
                if (dtbl.Rows.Count > 0)
                {
                    //if (dtbl.Rows.Count > 1)
                    //{
                        if (dtbl.Rows[0]["Value"].ToString() == "1")
                        {
                            chkStockUpdation.Checked = true;
                        }
                        else if (dtbl.Rows[0]["Value"].ToString() == "0")
                        {
                            chkStockUpdation.Checked = false;
                        }
                  //  }
                }
            }
            else if (tvSettings.SelectedValue == "POS")
            {

                blsettings.Module = "INV";
                blsettings.SubModule = "POS";
                mvSettings.SetActiveView(vwPOS);
                dtbl = blsettings.SelectSettings();
                if (dtbl.Rows.Count > 0)
                {
                    for (int i = 0; i < dtbl.Rows.Count; i++)
                    {
                        if (dtbl.Rows[i]["Name"].ToString() == "MNFCTRTOBIN")
                        {
                            if (dtbl.Rows[i]["Value"].ToString() == "1")
                            {
                                chkItemListField.Checked = true;
                            }
                            else if (dtbl.Rows[i]["Value"].ToString() == "0")
                            {
                                chkItemListField.Checked = false;
                            }

                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "toastr.warning('" + ex.Message + "')", true);
        }
    }
    protected void btnSubmitSL_Click(object sender, EventArgs e)
    {
        blMessage.Items.Clear();
        SaveData("INV", "SLS", "SLS08", chkAcountPost.Checked ? "1" : "0", 1);
      
       

    }
    protected void SaveData(string Module, string SubModule, string Name, string Value, byte DeleteFlag)
    {
        long status = 0;
        BlSettings blsettings = new BlSettings();
        blsettings.Module = Module;
        blsettings.SubModule = SubModule;
        blsettings.DeleteFlag = DeleteFlag;
        blsettings.Name = Name;
        blsettings.Value = Value;
        blsettings.FK_Client = Convert.ToInt64(hfClient.Value);
        blsettings.UserCode = hfUser.Value;
        status = blsettings.UpdateData();
        if (status > 0)
        {
            ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "toastr.success('Data Saved successfully')", true);

        }
        else if (status == -99)
        {

            ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "toastr.warning('Data Base Cannot Exist')", true);
        }

        else if (status == -1)
        {

            ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "toastr.warning('Data Not Saved')", true);
        }

        blsettings = null;
    }

    protected void btnSubmitPOS_Click(object sender, EventArgs e)
    {
        blMessage.Items.Clear();

        SaveData("INV", "POS", "MNFCTRTOBIN", chkItemListField.Checked == true ? "1" : "0", 1);
    }
    protected void btnSubmitPurchase_Click(object sender, EventArgs e)
    {
        blMessage.Items.Clear();
        if (Session["name"].ToString() == "pss")
        {
            SaveData("INV", "PUR", "PURSTCKUP", chkStockUpdation.Checked == true ? "1" : "0", 1);
        }
    }
}