/*-------------------------------------------------------------------
Purpose    : Settings in Textiles
Author     :bibin tc
Copyright  : Perfect Software Solutions (Clt) Pvt Ltd
Created on :03/05/2018
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


public partial class Settings : System.Web.UI.Page
{
    #region Functions
    public static Boolean linkbit = false;
    protected void BindData()
    {
        Populate populate = new Populate();
        populate.FillRounding(ddlRoundingINV);
        populate.FillCostingMethod(ddlCostingMethod);
        if (linkbit == true)
        {
            populate.FillDropdownList(ddlCashAccountHead, "AccountHead", "Name ", "ID_AccountHead", "ID_AccountHead", " Active='True' AND FK_Client=" + hfClient.Value, "");
            populate.FillDropdownList(ddlPurchaseAccountHead, "AccountHead", "Name", "ID_AccountHead", "ID_AccountHead", " Active='True' AND FK_Client=" + hfClient.Value, "");
            populate.FillDropdownList(ddlSalesAccountHead, "AccountHead", "Name", "ID_AccountHead", "ID_AccountHead", " Active='True' AND FK_Client=" + hfClient.Value, "");
            populate.FillDropdownList(ddlCardAccountHead, "AccountHead", "Name", "ID_AccountHead", "ID_AccountHead", " Active='True' AND FK_Client=" + hfClient.Value, "");
            populate.FillDropdownList(ddlChequeAccountHead, "AccountHead", "Name", "ID_AccountHead", "ID_AccountHead", " Active='True' AND FK_Client=" + hfClient.Value, "");
        }

        populate = null;
    }
    protected void Getlinkornotlink()
    {
        DataTable dtbl = new DataTable();
        BlSettings blsettings = new BlSettings();
        Populate populate = new Populate();
        blsettings.FK_Client = Convert.ToInt64(hfClient.Value == "" ? "0" : hfClient.Value);
        blsettings.UserCode = hfUser.Value;
        blsettings.Module = "SEC";
        blsettings.SubModule = "SEC";
        dtbl = blsettings.SelectSettings();
        if (dtbl.Rows.Count > 0)
            linkbit = SettingValue(dtbl, "SEC04") == "1";
    }
    private string SettingValue(DataTable dtbl, string name)
    {
        foreach (DataRow row in dtbl.Rows)
        {
            if (string.Equals(Convert.ToString(row["Name"]), name, StringComparison.OrdinalIgnoreCase))
                return Convert.ToString(row["Value"]);
        }
        return "";
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
        if (status == -99)
        {

            ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "toastr.warning('Data Base Cannot Exist')", true);
        }

        if (status == -1)
        {

            ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "toastr.warning('Data Not Saved')", true);
        }

        blsettings = null;
    }
    protected void CheckAccessRights()
    {
        //BlUserAccess bluseraccess = new BlUserAccess();
        //bluseraccess.FK_Users = Convert.ToInt64(Session["UserId"].ToString());
        //bluseraccess.FK_Pages = 29;     
        //bluseraccess.SelectPageSettings();
        //if (Convert.ToBoolean(bluseraccess.IsView) == false)
        //{
        //    Response.Redirect("Home.aspx");
        //}
        //hfAddPermission.Value = (Convert.ToBoolean(bluseraccess.IsAdd)).ToString();
        //hfUpdatePermission.Value = (Convert.ToBoolean(bluseraccess.IsUpdate)).ToString();
        //bluseraccess = null;
        hfAddPermission.Value = "true";
        hfAddPermission.Value = "true";
    }
    protected void CheckUserAccess()
    {
        BlUserAccess bluseraccess = new BlUserAccess();
        Populate populate = new Populate();

        bluseraccess.FK_Users = Convert.ToInt64(hfUser.Value);
        bluseraccess.FK_Pages = 18;
        bluseraccess.FK_Client = Convert.ToInt64(hfClient.Value);
        bluseraccess.CheckUserAccess();
        if (Convert.ToBoolean(bluseraccess.IsView) == false)
        {
            // Response.Redirect("Home.aspx?");

        }

        hfIsAdd.Value = (Convert.ToBoolean(bluseraccess.IsAdd)).ToString();
        hfIsUpdate.Value = (Convert.ToBoolean(bluseraccess.IsUpdate)).ToString();
        hfIsDelete.Value = (Convert.ToBoolean(bluseraccess.IsDelete)).ToString();
        bluseraccess = null;
    }

    #endregion

    #region Events
    protected void btnCardBlurTrigger_Click(object sender, EventArgs e)
    {
        try
        {
            try
            {
                if (ddlCardAccountHead.SelectedIndex > 0)
                {
                    Populate populate = new Populate();
                    populate.FillDropdownList(ddlCardAccountSubHead, "AccountHeadSub", "Name", "ID_AccountHeadSub", "ID_AccountHeadSub", "FK_Client=1 AND Active='True' AND FK_AccountHead=" + ddlCardAccountHead.SelectedValue.ToString(), "");
                    populate = null;

                }
                else
                {
                    ddlCardAccountSubHead.Items.Clear();
                }
                ddlCardAccountSubHead.Focus();
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "toastr.warning('" + ex.Message + "')", true);
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "toastr.warning('" + ex.Message + "')", true);
        }
    }
    //protected void btnKNetBlurTrigger_Click(object sender, EventArgs e)
    //{
    //    try
    //    {
    //        try
    //        {
    //            if (ddlKNetAccountHead.SelectedIndex > 0)
    //            {
    //                Populate populate = new Populate();
    //                populate.FillDropdownList(ddlKNetAccountSubHead, "AccountHeadSub", "AhsCode+'-'+Name", "ID_AccountHeadSub", "ID_AccountHeadSub", "FK_Client=1 AND Active='True' AND FK_AccountHead=" + ddlKNetAccountHead.SelectedValue.ToString(), "");
    //                populate = null;
    //            }
    //            else
    //            {
    //                ddlKNetAccountSubHead.Items.Clear();
    //            }
    //            ddlKNetAccountSubHead.Focus();
    //        }
    //        catch (Exception ex)
    //        {
    //            blMessage.Items.Add(ex.Message);
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        blMessage.Items.Add(ex.Message);
    //    }

    //}



    protected void btnSubmitSLS_Click(object sender, EventArgs e)
    {
        try
        {
            if (hfIsUpdate.Value.ToLower() == "true")
            {
                blMessage.Items.Clear();
                SaveData("INV", "SLS", "SLS01", txtPortNo.Text.Trim(), 1);
                SaveData("INV", "SLS", "SLS02", TxtBillPrint.Text.Trim(), 2);
                DateTime time = DateTime.Parse(string.Format("{0}:{1}:{2} {3}", TmsSales.Hour, TmsSales.Minute, TmsSales.Second, TmsSales.AmPm));
                SaveData("INV", "SLS", "SLS03", time.ToString("hh:mm:ss tt"), 2);
                SaveData("INV", "SLS", "SLS04", cbxSalesRemark.Checked.ToString(), 2);
                SaveData("INV", "SLS", "SLS05", txtSeasonalGreetings.Text.Trim(), 2);
                SaveData("INV", "SLS", "SLS06", chksalediscountaccount.Checked == true ? "1" : "0", 2);
                SaveData("INV", "SLS", "SLS07", ddlSalesAccountPosting.SelectedValue.ToString(), 2);
                ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "toastr.success('Data Saved successfully')", true);
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
    protected void btnSubmitModule_Click(object sender, EventArgs e)
    {
        try
        {
            if (hfIsUpdate.Value.ToLower() == "true")
            {
                SaveData("GEN", "AMD", chkPO.Checked.ToString() + "," + chkPO.Checked.ToString(), "", 1); /* Active Modules  Purchgase order excluded/included*/
                ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "toastr.success('Submitted successfully')", true);
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
    protected void btnSubmitMAS_Click(object sender, EventArgs e)
    {
        try
        {
            if (hfIsUpdate.Value.ToLower() == "true")
            {
                SaveData("MAS", "CUS", "shortName", txtCustomerShortname.Text, 1);
                SaveData("MAS", "SUP", "shortName", txtSupplierShortName.Text, 1);
                SaveData("MAS", "COM", "CompanyName", txtCompanyName.Text, 1);
                Populate.MessageBox(this, "Submitted Successfully");
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
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                if (Session["name"].ToString() == "pss")
                {
                    LIAccountslink.Style.Add("display", "block");
                    licustomertype.Style.Add("display", "block");

                }
                else
                {
                    LIAccountslink.Style.Add("display", "none");
                    lidbname.Style.Add("display", "none");
                    licustomertype.Style.Add("display", "none");
                }

                hfUser.Value = Request.QueryString["uid"];
                hfClient.Value = Request.QueryString["clnt"];
                CheckAccessRights();
                linkbit = false;
                Getlinkornotlink();
                BindData();
                CheckUserAccess();
                blMessage.Items.Clear();
                rdHtmlPrint.Checked = true;
            }
            if (Session["name"].ToString() == "pss")
            {
                LIAccountslink.Style.Add("display", "block");

            }
            else
            {
                LIAccountslink.Style.Add("display", "none");
                lidbname.Style.Add("display", "none");
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "toastr.warning('" + ex.Message + "')", true);
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
            blsettings.FK_Client = Convert.ToInt64(hfClient.Value == "" ? "0" : hfClient.Value);
            blsettings.UserCode = hfUser.Value;

            if (tvSettings.SelectedValue == "SEC")
            {
                txtMinPwdLen.Focus();
                blsettings.Module = "SEC";
                blsettings.SubModule = "SEC";
                dtbl = blsettings.SelectSettings();
                mvSettings.SetActiveView(vwSEC);
                if (dtbl.Rows.Count > 0)
                {
                    txtMinPwdLen.Text = SettingValue(dtbl, "SEC01");
                    txtMaxPwdLen.Text = SettingValue(dtbl, "SEC02");
                    txtPwdChngPeriod.Text = SettingValue(dtbl, "SEC03");
                    if (Session["name"] != null && Session["name"].ToString() == "pss")
                    {
                        bool linked = SettingValue(dtbl, "SEC04") == "1";
                        chkAcountlnking.Checked = linked;
                        if (linked)
                        {
                            lidbname.Style.Add("display", "block");
                            licustomertype.Style.Add("display", "block");
                            txtdatabase.Text = SettingValue(dtbl, "SEC05");
                            string customerType = SettingValue(dtbl, "SEC06");
                            if (ddlcustomertype.Items.FindByValue(customerType) != null)
                                ddlcustomertype.SelectedValue = customerType;
                        }
                        else
                        {
                            lidbname.Style.Add("display", "none");
                            licustomertype.Style.Add("display", "none");
                        }
                    }
                }
            }
            else if (tvSettings.SelectedValue == "GRN")
            {
                ddlRoundingINV.Focus();
                hfINVSubModule.Value = "GRN";
                blsettings.Module = "INV";
                blsettings.SubModule = "GRN";
                dtbl = blsettings.SelectSettings();
                mvSettings.SetActiveView(vwINV);
                if (dtbl.Rows.Count > 0)
                {
                    ddlRoundingINV.SelectedValue = dtbl.Rows[0]["Value"].ToString();
                    ddlCostingMethod.SelectedValue = dtbl.Rows[1]["Value"].ToString();
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
                        if (dtbl.Rows[i]["Name"].ToString() == "RNDOFFDSBL")
                        {
                            if (dtbl.Rows[i]["Value"].ToString() == "1")
                            {
                                chkDisableRoundOff.Checked = true;
                            }
                            else
                            {
                                chkDisableRoundOff.Checked = false;
                            }

                        }
                        else if (dtbl.Rows[i]["Name"].ToString() == "NEGSTKDSBL")
                        {
                            if (dtbl.Rows[i]["Value"].ToString() == "1")
                            {
                                chkDsblNegstk.Checked = true;
                            }
                            else
                            {
                                chkDsblNegstk.Checked = false;
                            }

                        }
                        else if (dtbl.Rows[i]["Name"].ToString() == "DATEENBL")
                        {
                            if (dtbl.Rows[i]["Value"].ToString() == "1")
                            {
                                chkDateEnable.Checked = true;
                            }
                            else
                            {
                                chkDateEnable.Checked = false;
                            }

                        }
                        else if (dtbl.Rows[i]["Name"].ToString() == "RECENBL")
                        {
                            if (dtbl.Rows[i]["Value"].ToString() == "1")
                            {
                                chkRecoveryEnable.Checked = true;
                            }
                            else
                            {
                                chkRecoveryEnable.Checked = false;
                            }

                        }
                        else if (dtbl.Rows[i]["Name"].ToString() == "DSC")
                        {
                            if (dtbl.Rows[i]["Value"].ToString() == "1")
                            {
                                chkPosDiscEnable.Checked = true;
                            }
                            else
                            {
                                chkPosDiscEnable.Checked = false;
                            }

                        }
                        else if (dtbl.Rows[i]["Name"].ToString() == "CUSBAR")
                        {
                            if (dtbl.Rows[i]["Value"].ToString() == "1")
                            {
                                chkCustBarcode.Checked = true;
                            }
                            else
                            {
                                chkCustBarcode.Checked = false;
                            }

                        }
                        else if (dtbl.Rows[i]["Name"].ToString() == "CUSIMGPTH")
                        {
                            txtCustImagePath.Text = dtbl.Rows[i]["Value"].ToString();
                        }
                        else if (dtbl.Rows[i]["Name"].ToString() == "BARSPLT")
                        {
                            string SplitValue = dtbl.Rows[i]["Value"].ToString();
                            string[] a = SplitValue.Split(',');
                            txtBarcodeSplit1.Text = a[0];
                            txtBarcodeSplit2.Text = a[1];
                            txtBarcodeSplit3.Text = a[2];
                        }
                    }
                }
            }
            else if (tvSettings.SelectedValue == "WMB")
            {

                blsettings.Module = "INV";
                blsettings.SubModule = "WMB";
                mvSettings.SetActiveView(vwWMB);
                dtbl = blsettings.SelectSettings();
                if (dtbl.Rows.Count > 0)
                {
                    for (int i = 0; i < dtbl.Rows.Count; i++)
                    {
                        if (dtbl.Rows[i]["Name"].ToString() == "WMBLOCPATH")
                        {
                            txtBarcodeLocationPath.Text = dtbl.Rows[i]["Value"].ToString();
                        }
                    }
                }
            }
            else if (tvSettings.SelectedValue == "SLS")
            {
                ddlRoundingINV.Focus();
                hfINVSubModule.Value = "SLS";
                blsettings.Module = "INV";
                blsettings.SubModule = "SLS";
                dtbl = blsettings.SelectSettings();
                mvSettings.SetActiveView(vwSLS);

                //SaveData("INV", "SLS", "SLS01", txtPortNo.Text.Trim(), 1);
                //SaveData("INV", "SLS", "SLS02", TxtBillPrint.Text.Trim(), 2);
                //DateTime time = DateTime.Parse(string.Format("{0}:{1}:{2} {3}", TmsSales.Hour, TmsSales.Minute, TmsSales.Second, TmsSales.AmPm));
                //SaveData("INV", "SLS", "SLS03", time.ToString("hh:mm:ss tt"), 2);
                //SaveData("INV", "SLS", "SLS04", cbxSalesRemark.Checked.ToString(), 2);
                //SaveData("INV", "SLS", "SLS05", txtSeasonalGreetings.Text.Trim(), 2);
                //SaveData("INV", "SLS", "SLS06", chksalediscountaccount.Checked == true ? "1" : "0", 2);
                //SaveData("INV", "SLS", "SLS07", ddlSalesAccountPosting.SelectedValue.ToString(), 2);

                if (dtbl.Rows.Count > 0)
                {

                    string Name = dtbl.Rows[0]["Name"].ToString();

                    txtPortNo.Text = dtbl.Rows[0]["Value"].ToString();
                    TxtBillPrint.Text = dtbl.Rows[1]["Value"].ToString();

                    //var datetime = DateTime.Now.ToString().Split(' ');
                    //var ampm = (dtbl.Rows[0][3].ToString()).Split(' ');
                    //var newdateime = Convert.ToDateTime(datetime[0] + " " + ampm[0]);

                    //DateTime mydt = DateTime.Parse(dtbl.Rows[1]["Value"].ToString());
                    //MKB.TimePicker.TimeSelector.AmPmSpec am_pm;
                    //if (ampm[1] == "AM")
                    //{
                    //    am_pm = MKB.TimePicker.TimeSelector.AmPmSpec.AM;
                    //}
                    //else
                    //{
                    //    am_pm = MKB.TimePicker.TimeSelector.AmPmSpec.PM;
                    //}



                    DateTime mydt = DateTime.Parse(dtbl.Rows[2]["Value"].ToString());
                    MKB.TimePicker.TimeSelector.AmPmSpec am_pm;
                    if (mydt.ToString("tt") == "AM")
                    {
                        am_pm = MKB.TimePicker.TimeSelector.AmPmSpec.AM;
                    }
                    else
                    {
                        am_pm = MKB.TimePicker.TimeSelector.AmPmSpec.PM;
                    }
                    TmsSales.SetTime(mydt.Hour, mydt.Minute, am_pm);
                    if (dtbl.Rows.Count > 3)
                    {
                        cbxSalesRemark.Checked = Convert.ToBoolean(dtbl.Rows[3]["Value"].ToString());
                    }
                    else
                    {
                        cbxSalesRemark.Checked = true;
                    }
                    txtSeasonalGreetings.Text = dtbl.Rows[4]["Value"].ToString();
                    if (dtbl.Rows[5]["Value"].ToString() == "1")
                    {
                        chksalediscountaccount.Checked = true;
                    }
                    else
                    {
                        chksalediscountaccount.Checked = false;
                    }
                    ddlSalesAccountPosting.SelectedValue = dtbl.Rows[6]["Value"].ToString();
                }
            }
            else if (tvSettings.SelectedValue == "STK")
            {
                chklstSTKCodeGen.Focus();
                blsettings.Module = "INV";
                blsettings.SubModule = "STK";
                dtbl = blsettings.SelectSettings();
                if (dtbl.Rows.Count > 0)
                {
                    string str = dtbl.Rows[0]["Value"].ToString();
                    string[] words = str.Split(',');
                    foreach (string word in words)
                    {
                        if (word != "")
                        {
                            chklstSTKCodeGen.Items[Convert.ToInt16(word) - 1].Selected = true;
                        }
                    }
                }
                mvSettings.SetActiveView(vwSTK);
            }
            else if (tvSettings.SelectedValue == "ACC")
            {
                Getlinkornotlink();
                BindData();
                mvSettings.SetActiveView(vwACC);
                blsettings.Module = "ACC";
                blsettings.SubModule = "ACC";
                dtbl = blsettings.SelectSettings();
                if (dtbl.Rows.Count > 0)
                {
                    ddlSalesAccountHead.SelectedValue = dtbl.Rows[0]["Value"].ToString();
                    if (dtbl.Rows[0]["Value"].ToString() == "0")
                    {
                        ddlSalesAccountSubHead.Items.Clear();
                    }
                    else
                    {
                        populate.FillDropdownList(ddlSalesAccountSubHead, "AccountHeadSub", "Name", "ID_AccountHeadSub", "ID_AccountHeadSub", "Active='True'  AND FK_Client=" + hfClient.Value + " AND FK_AccountHead=" + ddlSalesAccountHead.SelectedValue.ToString(), "");
                        ddlSalesAccountSubHead.SelectedValue = dtbl.Rows[1]["Value"].ToString();
                    }
                    ddlPurchaseAccountHead.SelectedValue = dtbl.Rows[2]["Value"].ToString();
                    if (dtbl.Rows[2]["Value"].ToString() == "0")
                    {
                        ddlPurchaseAccountSubHead.Items.Clear();
                    }
                    else
                    {
                        populate.FillDropdownList(ddlPurchaseAccountSubHead, "AccountHeadSub", "Name", "ID_AccountHeadSub", "ID_AccountHeadSub", "FK_Client=" + hfClient.Value + " AND Active='True' AND FK_AccountHead=" + ddlPurchaseAccountHead.SelectedValue.ToString(), "");
                        ddlPurchaseAccountSubHead.SelectedValue = dtbl.Rows[3]["Value"].ToString();
                    }
                    ddlCashAccountHead.SelectedValue = dtbl.Rows[4]["Value"].ToString();
                    if (dtbl.Rows[4]["Value"].ToString() == "0")
                    {
                        ddlCashAccountSubHead.Items.Clear();
                    }
                    else
                    {
                        populate.FillDropdownList(ddlCashAccountSubHead, "AccountHeadSub", "Name", "ID_AccountHeadSub", "ID_AccountHeadSub", "FK_Client=" + hfClient.Value + " AND Active='True' AND FK_AccountHead=" + ddlCashAccountHead.SelectedValue.ToString(), "");
                        ddlCashAccountSubHead.SelectedValue = dtbl.Rows[5]["Value"].ToString();
                    }


                    //Card AccountSub Head
                    ddlCardAccountHead.SelectedValue = dtbl.Rows[6]["Value"].ToString();
                    if (dtbl.Rows[6]["Value"].ToString() == "0")
                    {
                        ddlCardAccountSubHead.Items.Clear();
                    }
                    else
                    {
                        populate.FillDropdownList(ddlCardAccountSubHead, "AccountHeadSub", "Name", "ID_AccountHeadSub", "ID_AccountHeadSub", "FK_Client=" + hfClient.Value + " AND Active='True' AND FK_AccountHead=" + ddlCardAccountHead.SelectedValue.ToString(), "");
                        ddlCardAccountSubHead.SelectedValue = dtbl.Rows[7]["Value"].ToString();
                    }

                    //cheque Account Head
                    ddlChequeAccountHead.SelectedValue = dtbl.Rows[8]["Value"].ToString();
                    if (dtbl.Rows[8]["Value"].ToString() == "0")
                    {
                        ddlChequeAccountSubHead.Items.Clear();
                    }
                    else
                    {
                        populate.FillDropdownList(ddlChequeAccountSubHead, "AccountHeadSub", "Name", "ID_AccountHeadSub", "ID_AccountHeadSub", "FK_Client=" + hfClient.Value + " AND Active='True' AND FK_AccountHead=" + ddlChequeAccountHead.SelectedValue.ToString(), "");
                        ddlChequeAccountSubHead.SelectedValue = dtbl.Rows[9]["Value"].ToString();
                    }

                    ////////KNet  AccountSub Head
                    //////ddlKNetAccountHead.SelectedValue = dtbl.Rows[8]["Value"].ToString();
                    //////if (dtbl.Rows[8]["Value"].ToString() == "0")
                    //////{
                    //////    ddlKNetAccountSubHead.Items.Clear();
                    //////}
                    //////else
                    //////{
                    //////    populate.FillDropdownList(ddlKNetAccountSubHead, "AccountHeadSub", "AhsCode+'-'+Name", "ID_AccountHeadSub", "ID_AccountHeadSub", "FK_Client=1 AND Active='True' AND FK_AccountHead=" + ddlKNetAccountHead.SelectedValue.ToString(), "");
                    //////    ddlKNetAccountSubHead.SelectedValue = dtbl.Rows[9]["Value"].ToString();
                    //////}
                }


                ddlSalesAccountHead.Focus();
            }
            else if (tvSettings.SelectedValue == "GEN")
            {
                mvSettings.SetActiveView(vwGEN);
                blsettings.Module = "GEN";
                blsettings.SubModule = "BAR";

                dtbl = blsettings.SelectSettings();

                if (dtbl.Rows.Count > 0)
                {
                    if (dtbl.Rows[0]["Name"].ToString() == "Both")
                    {
                        chkshortName.Checked = true;
                        chkCustom.Checked = true;
                        txtCustom.Text = dtbl.Rows[0]["Value"].ToString();
                        ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "ShowTextBox();", true);
                    }
                    else if (dtbl.Rows[0]["Name"].ToString() == "shortName")
                    {
                        chkshortName.Checked = true;
                        //ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "ClearGEN();", true);
                    }
                    else
                    {
                        chkCustom.Checked = true;
                        txtCustom.Text = dtbl.Rows[0]["Value"].ToString();
                        ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "ShowTextBox();", true);
                    }
                }
                blsettings.SubModule = "AMD";
                dtbl = blsettings.SelectSettings();
                if (dtbl.Rows.Count > 0)
                {
                    if (dtbl.Rows[0]["Name"].ToString() == "ActiveModule")
                    {
                        string[] str = dtbl.Rows[0]["Value"].ToString().Split(',');
                        chkPO.Checked = Convert.ToBoolean(str[0]);
                        chkMR.Checked = Convert.ToBoolean(str[1]);
                    }
                }
                else
                {
                    chkPO.Checked = true;
                    chkMR.Checked = true;
                }
                blsettings.SubModule = "BPR";
                dtbl = blsettings.SelectSettings();
                if (dtbl.Rows.Count > 0)
                {
                    string[] strArr = dtbl.Rows[0]["Value"].ToString().Split(',');
                    txtPageHeight.Text = strArr[0];
                    txtPageWidth.Text = strArr[1];
                    txtHorizontalNos.Text = strArr[2];
                    txtVerticalNos.Text = strArr[3];
                    txtHeight.Text = strArr[4];
                    txtWidth.Text = strArr[5];
                    txtMargin.Text = strArr[6];

                }
                else
                {
                    txtPageHeight.Text = "0";
                    txtPageWidth.Text = "0";
                    txtHorizontalNos.Text = "0";
                    txtVerticalNos.Text = "0";
                    txtHeight.Text = "0";
                    txtWidth.Text = "0";
                    txtMargin.Text = "0";
                }

                blsettings.SubModule = "BARPRNT";
                dtbl = blsettings.SelectSettings();
                if (dtbl.Rows.Count > 0)
                {
                    if (dtbl.Rows[0]["Value"].ToString() == "1")
                    {
                        rdHtmlPrint.Checked = true;
                    }
                    else
                    {
                        rdtxtPrint.Checked = true;
                    }

                }
                else
                {
                    rdHtmlPrint.Checked = true;
                }


                //Financial Year Created  Nithin Das H 
                blsettings.SubModule = "FINFR";
                dtbl = blsettings.SelectSettings();
                if (dtbl.Rows.Count > 0)
                {
                    if (dtbl.Rows[0]["Name"].ToString() == "FROM")
                    {
                        txtFromDateFinYear.Text = dtbl.Rows[0]["Value"].ToString();
                    }

                }
                blsettings.SubModule = "FINTO";
                dtbl = blsettings.SelectSettings();
                if (dtbl.Rows.Count > 0)
                {
                    if (dtbl.Rows[0]["Name"].ToString() == "TO")
                    {
                        txtToDateFinYear.Text = dtbl.Rows[0]["Value"].ToString();
                    }

                }
                blsettings.SubModule = "TIME";
                dtbl = blsettings.SelectSettings();
                if (dtbl.Rows.Count > 0)
                {
                    txtMinutes.Text = dtbl.Rows[0]["Value"].ToString();
                }

                blsettings.SubModule = "MGP";
                dtbl = blsettings.SelectSettings();
                if (dtbl.Rows.Count > 0)
                {
                    txtMonthlyProcessGracePeriod.Text = dtbl.Rows[0]["Value"].ToString();
                }
                else
                {
                    txtMonthlyProcessGracePeriod.Text = "0";
                }

            }
            else if (tvSettings.SelectedValue == "MAS")
            {
                mvSettings.SetActiveView(vwMAS);
                blsettings.Module = "MAS";
                blsettings.SubModule = "CUS";

                dtbl = blsettings.SelectSettings();

                if (dtbl.Rows.Count > 0)
                {

                    txtCustomerShortname.Text = dtbl.Rows[0]["Value"].ToString();
                }
                blsettings.SubModule = "SUP";
                dtbl = blsettings.SelectSettings();
                if (dtbl.Rows.Count > 0)
                {

                    txtSupplierShortName.Text = dtbl.Rows[0]["Value"].ToString();
                }
                blsettings.SubModule = "COM";
                dtbl = blsettings.SelectSettings();
                if (dtbl.Rows.Count > 0)
                {

                    txtCompanyName.Text = dtbl.Rows[0]["Value"].ToString();
                }

            }
            else if (tvSettings.SelectedValue == "PUR")
            {
                chklstSTKCodeGen.Focus();
                blsettings.Module = "INV";
                blsettings.SubModule = "PUR";
                dtbl = blsettings.SelectSettings();
                if (dtbl.Rows.Count > 0)
                {

                    if (dtbl.Rows[0]["Value"].ToString() == "1")
                    {
                        rbtnCategory.Checked = true;
                    }
                    else
                    {
                        rbtnPrePurch.Checked = true;
                    }
                    if (dtbl.Rows[1]["Value"].ToString() == "1")
                    {
                        chkpurhasediscountaccount.Checked = true;
                    }
                    else
                    {
                        chkpurhasediscountaccount.Checked = false;
                    }

                    if (dtbl.Rows[2]["Value"].ToString() == "1")
                    {
                        chkcnvertigst.Checked = true;
                    }
                    else
                    {
                        chkcnvertigst.Checked = false;
                    }

                }
                else
                {
                    rbtnPrePurch.Checked = true;
                }
                mvSettings.SetActiveView(vwPUR);
            }
            else
            {
                mvSettings.SetActiveView(vwTemp);
            }
            blsettings = null;
            populate = null;
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "toastr.warning('" + ex.Message + "')", true);
        }
    }

    /*start for vwWMB*/
    protected void btnBarcodeLoc_Click(object sender, EventArgs e)
    {
        divBarcodeLocation.Style.Add("display", "block");
    }
    protected void ibtnBarcodeLocClose_Click(object sender, ImageClickEventArgs e)
    {
        divBarcodeLocation.Style.Add("display", "none");
    }
    /*end vwWMB*/

    /*start for vwPOS*/
    protected void btnCustImagePath_Click(object sender, EventArgs e)
    {
        divImagePath.Style.Add("display", "block");
    }
    protected void ibtnImagePathClose_Click(object sender, ImageClickEventArgs e)
    {
        divImagePath.Style.Add("display", "none");
    }
    /*end vwPOS*/

    protected void btnSubmitSEC_Click(object sender, EventArgs e)
    {
        try
        {
            BlSettings settings = new BlSettings();
            if (hfIsUpdate.Value.ToLower() == "true")
            {

                blMessage.Items.Clear();
                //if (hfUpdatePermission.Value.ToLower() == "true" && hfAddPermission.Value.ToLower() == "true")
                //{

                SaveData("SEC", "SEC", "SEC01", txtMinPwdLen.Text.Trim(), 2);
                SaveData("SEC", "SEC", "SEC02", txtMaxPwdLen.Text.Trim(), 2);
                SaveData("SEC", "SEC", "SEC03", txtPwdChngPeriod.Text.Trim(), 2);

                if (Session["name"] != null && Session["name"].ToString() == "pss" && chkAcountlnking.Checked)
                {
                    SaveData("SEC", "SEC", "SEC04", "1", 2);

                    if (txtdatabase.Text.Trim() != "")
                        SaveData("SEC", "SEC", "SEC05", txtdatabase.Text.Trim(), 2);

                    SaveData("SEC", "SEC", "SEC06", ddlcustomertype.SelectedValue, 2);
                    lidbname.Style.Add("display", "block");
                    licustomertype.Style.Add("display", "block");
                }


                // settings.DbName=txtdatabase.Text==""?"":txtdatabase.Text;
                ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "toastr.success('Data Saved successfully')", true);
                //}
                //else
                //{
                //    blMessage.Items.Add("User does not have permission to update");
                //}

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


    protected void btnSubmitINV_Click(object sender, EventArgs e)
    {
        try
        {
            if (hfIsUpdate.Value.ToLower() == "true")
            {

                blMessage.Items.Clear();
                //if (hfUpdatePermission.Value.ToLower() == "true" && hfAddPermission.Value.ToLower() == "true")
                //{
                if (hfINVSubModule.Value == "GRN")
                {
                    SaveData("INV", "GRN", "GRN01", ddlRoundingINV.SelectedValue.ToString(), 1);
                    SaveData("INV", "GRN", "GRN02", ddlCostingMethod.SelectedValue.ToString(), 2);
                    ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "toastr.success('Data Saved successfully')", true);
                }
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "toastr.warning('User does not have permission to Update')", true);

            }
            //}
            //else
            //{
            //    blMessage.Items.Add("User does not have permission to update");
            //}
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "toastr.warning('" + ex.Message + "')", true);
        }
    }

    protected void btnSubmitSTK_Click(object sender, EventArgs e)
    {
        try
        {
            if (hfIsUpdate.Value.ToLower() == "true")
            {
                blMessage.Items.Clear();
                //if (hfUpdatePermission.Value.ToLower() == "true" && hfAddPermission.Value.ToLower() == "true")
                //{
                string str = "";
                foreach (ListItem item in chklstSTKCodeGen.Items)
                {
                    if (item.Selected == true)
                    {
                        if (str != "")
                        {
                            str = str + ",";
                        }
                        str = str + item.Value.ToString();
                    }
                }
                SaveData("INV", "STK", "STK01", str, 1);
                ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "toastr.success('Data Saved successfully')", true);

                //}
                //else
                //{
                //    blMessage.Items.Add("User does not have permission to update");
                //}
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
    protected void btnSubmitGEN_Click(object sender, EventArgs e)
    {
        try
        {
            if (hfIsUpdate.Value.ToLower() == "true")
            {
                Boolean b = false;
                if (chkshortName.Checked && chkCustom.Checked)
                {
                    SaveData("GEN", "BAR", "Both", txtCustom.Text, 1);
                    b = true;
                }
                else if (chkshortName.Checked)
                {
                    SaveData("GEN", "BAR", "shortName", "", 1);
                    b = true;
                }

                else if (chkCustom.Checked)
                {
                    SaveData("GEN", "BAR", "Custom", txtCustom.Text, 1);
                    b = true;
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "toastr.warning('Select Item Short Name/User Defined Code')", true);
                }
                if (b == true)
                {
                    SaveData("GEN", "BPR", "BARCODE", txtPageHeight.Text.Trim() + "," + txtPageWidth.Text.Trim() + "," + txtHorizontalNos.Text.Trim() + "," + txtVerticalNos.Text.Trim() + "," + txtHeight.Text.Trim() + "," + txtWidth.Text.Trim() + "," + txtMargin.Text.Trim(), 1);
                    if (chkPO.Checked)
                        chkMR.Checked = true;
                    if (!(chkMR.Checked))
                        chkPO.Checked = false;
                    SaveData("GEN", "AMD", "ActiveModule", chkPO.Checked.ToString() + "," + chkMR.Checked.ToString(), 1);
                    Populate.MessageBox(this, "Submitted Successfully");
                    ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "ShowTextBox();", true);
                }
                if (rdHtmlPrint.Checked == true)
                {
                    SaveData("GEN", "BARPRNT", "PRMethod", "1", 1);
                }
                else
                {
                    SaveData("GEN", "BARPRNT", "PRMethod", "2", 1);
                }

                //Financial year  Modifies by Nithin Das H
                SaveData("GEN", "FINFR", "FROM", txtFromDateFinYear.Text, 1);
                SaveData("GEN", "FINTO", "TO", txtToDateFinYear.Text, 1);
                SaveData("GEN", "TIME", "MINUTES", txtMinutes.Text, 1);
                SaveData("GEN", "MGP", "DAYS", txtMonthlyProcessGracePeriod.Text.Trim(), 1);

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

    protected void btnClearSEC_Click(object sender, EventArgs e)
    {
        try
        {
            Response.Redirect("Home.aspx");
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "toastr.warning('" + ex.Message + "')", true);
        }
    }
    protected void btnClearINV_Click(object sender, EventArgs e)
    {
        try
        {
            Response.Redirect("Home.aspx");
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "toastr.warning('" + ex.Message + "')", true);
        }
    }

    protected void btnClearSTK_Click(object sender, EventArgs e)
    {
        try
        {
            Response.Redirect("Home.aspx");
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "toastr.warning('" + ex.Message + "')", true);
        }
    }
    protected void btnSaleBlurTrigger_Click(object sender, EventArgs e)
    {
        try
        {
            if (ddlSalesAccountHead.SelectedIndex > 0)
            {
                Populate populate = new Populate();
                populate.FillDropdownList(ddlSalesAccountSubHead, "AccountHeadSub", "Name", "ID_AccountHeadSub", "ID_AccountHeadSub", "FK_Client=" + hfClient.Value + " AND Active='True' AND FK_AccountHead=" + ddlSalesAccountHead.SelectedValue.ToString(), "");
                populate = null;
            }
            else
            {
                ddlSalesAccountSubHead.Items.Clear();
            }
            ddlSalesAccountSubHead.Focus();
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "toastr.warning('" + ex.Message + "')", true);
        }
    }
    protected void btnPurchaseBlurTrigger_Click(object sender, EventArgs e)
    {
        try
        {
            if (ddlPurchaseAccountHead.SelectedIndex > 0)
            {
                Populate populate = new Populate();
                populate.FillDropdownList(ddlPurchaseAccountSubHead, "AccountHeadSub", "Name", "ID_AccountHeadSub", "ID_AccountHeadSub", "FK_Client=" + hfClient.Value + " AND Active='True' AND FK_AccountHead=" + ddlPurchaseAccountHead.SelectedValue.ToString(), "");
                populate = null;
            }
            else
            {
                ddlPurchaseAccountSubHead.Items.Clear();
            }
            ddlPurchaseAccountSubHead.Focus();
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "toastr.warning('" + ex.Message + "')", true);
        }
    }
    protected void btnCashBlurTrigger_Click(object sender, EventArgs e)
    {
        try
        {
            try
            {
                if (ddlCashAccountHead.SelectedIndex > 0)
                {
                    Populate populate = new Populate();
                    populate.FillDropdownList(ddlCashAccountSubHead, "AccountHeadSub", "Name", "ID_AccountHeadSub", "ID_AccountHeadSub", "FK_Client=" + hfClient.Value + " AND Active='True' AND FK_AccountHead=" + ddlCashAccountHead.SelectedValue.ToString(), "");
                    populate = null;
                }
                else
                {
                    ddlCashAccountSubHead.Items.Clear();
                }
                ddlCashAccountSubHead.Focus();
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "toastr.warning('" + ex.Message + "')", true);
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "toastr.warning('" + ex.Message + "')", true);
        }
    }
    protected void btnChequeBlurTrigger_Click(object sender, EventArgs e)
    {
        try
        {
            try
            {
                if (ddlChequeAccountHead.SelectedIndex > 0)
                {
                    Populate populate = new Populate();
                    populate.FillDropdownList(ddlChequeAccountSubHead, "AccountHeadSub", "Name", "ID_AccountHeadSub", "ID_AccountHeadSub", "FK_Client=" + hfClient.Value + " AND Active='True' AND FK_AccountHead=" + ddlChequeAccountHead.SelectedValue.ToString(), "");
                    populate = null;
                }
                else
                {
                    ddlChequeAccountSubHead.Items.Clear();
                }
                ddlChequeAccountSubHead.Focus();
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "toastr.warning('" + ex.Message + "')", true);
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "toastr.warning('" + ex.Message + "')", true);
        }
    }
    protected void btnAccClose_Click(object sender, EventArgs e)
    {
        try
        {
            Response.Redirect("Home.aspx");
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "toastr.warning('" + ex.Message + "')", true);
        }
    }
    protected void btnAccSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            if (hfIsUpdate.Value.ToLower() == "true")
            {
                blMessage.Items.Clear();
                //if (hfUpdatePermission.Value.ToLower() == "true" && hfAddPermission.Value.ToLower() == "true")
                //{
                SaveData("ACC", "ACC", "ACC01H", ddlSalesAccountHead.SelectedValue.ToString(), 1);
                if (ddlSalesAccountHead.SelectedIndex > 0)
                {
                    SaveData("ACC", "ACC", "ACC01SH", ddlSalesAccountSubHead.SelectedValue.ToString(), 2);
                }
                else
                {
                    SaveData("ACC", "ACC", "ACC01SH", "0", 2);
                }
                SaveData("ACC", "ACC", "ACC02H", ddlPurchaseAccountHead.SelectedValue.ToString(), 2);
                if (ddlPurchaseAccountHead.SelectedIndex > 0)
                {
                    SaveData("ACC", "ACC", "ACC02SH", ddlPurchaseAccountSubHead.SelectedValue.ToString(), 2);
                }
                else
                {
                    SaveData("ACC", "ACC", "ACC02SH", "0", 2);
                }
                SaveData("ACC", "ACC", "ACC03H", ddlCashAccountHead.SelectedValue.ToString(), 2);
                if (ddlCashAccountHead.SelectedIndex > 0)
                {
                    SaveData("ACC", "ACC", "ACC03SH", ddlCashAccountSubHead.SelectedValue.ToString(), 2);
                }
                else
                {
                    SaveData("ACC", "ACC", "ACC03SH", "0", 2);
                }
                //Card Head
                SaveData("ACC", "ACC", "ACC04H", ddlCardAccountHead.SelectedValue.ToString(), 2);
                if (ddlCardAccountHead.SelectedIndex > 0)
                {
                    SaveData("ACC", "ACC", "ACC04SH", ddlCardAccountSubHead.SelectedValue.ToString(), 2);
                }
                else
                {
                    SaveData("ACC", "ACC", "ACC04SH", "0", 2);
                }

                //Cheque Head
                SaveData("ACC", "ACC", "ACC05H", ddlChequeAccountHead.SelectedValue.ToString(), 2);
                if (ddlCardAccountHead.SelectedIndex > 0)
                {
                    SaveData("ACC", "ACC", "ACC05SH", ddlChequeAccountSubHead.SelectedValue.ToString(), 2);
                }
                else
                {
                    SaveData("ACC", "ACC", "ACC05SH", "0", 2);
                }

                //KNet Head
                //SaveData("ACC", "ACC", "ACC05H", ddlKNetAccountHead.SelectedValue.ToString(), 2);
                //if (ddlKNetAccountHead.SelectedIndex > 0)
                //{
                //    SaveData("ACC", "ACC", "ACC05SH", ddlKNetAccountSubHead.SelectedValue.ToString(), 2);
                //}
                //else
                //{
                //    SaveData("ACC", "ACC", "ACC05SH", "0", 2);
                //}
                ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "toastr.success('Data Saved successfully')", true);

                //}
                //else
                //{
                //    blMessage.Items.Add("User does not have permission to update");
                //}
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



    protected void btnSubmitPOS_Click(object sender, EventArgs e)
    {
        try
        {
            if (hfIsUpdate.Value.ToLower() == "true")
            {
                string barcodeSplit = (txtBarcodeSplit1.Text == "" ? "0" : txtBarcodeSplit1.Text).ToString() + "," + (txtBarcodeSplit2.Text == "" ? "0" : txtBarcodeSplit2.Text).ToString() + "," + (txtBarcodeSplit3.Text == "" ? "0" : txtBarcodeSplit3.Text).ToString();
                blMessage.Items.Clear();
                SaveData("INV", "POS", "DSC", chkPosDiscEnable.Checked == true ? "1" : "0", 1);
                SaveData("INV", "POS", "CUSBAR", chkCustBarcode.Checked == true ? "1" : "0", 2);
                SaveData("INV", "POS", "CUSIMGPTH", txtCustImagePath.Text == "" ? "D:/WorkArea/EasyShopPlus/WebPharma" : txtCustImagePath.Text, 2);
                SaveData("INV", "POS", "RNDOFFDSBL", chkDisableRoundOff.Checked == true ? "1" : "0", 2);
                SaveData("INV", "POS", "NEGSTKDSBL", chkDsblNegstk.Checked == true ? "1" : "0", 2);
                SaveData("INV", "POS", "DATEENBL", chkDateEnable.Checked == true ? "1" : "0", 2);
                SaveData("INV", "POS", "BARSPLT", barcodeSplit, 2);
                SaveData("INV", "POS", "RECENBL", chkRecoveryEnable.Checked == true ? "1" : "0", 2);
                ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "toastr.success('Data Saved successfully')", true);


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
    protected void btnSubmitWeighMachineBarcode_Click(object sender, EventArgs e)
    {
        try
        {
            if (hfIsUpdate.Value.ToLower() == "true")
            {
                SaveData("INV", "WMB", "WMBLOCPATH", txtBarcodeLocationPath.Text == "" ? "D:\\" : txtBarcodeLocationPath.Text, 1);
                ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "toastr.success('Data Saved successfully')", true);


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
    protected void btnSubmitPurchase_Click(object sender, EventArgs e)
    {
        if (hfIsUpdate.Value.ToLower() == "true")
        {
            if (rbtnCategory.Checked == true)
            {
                SaveData("INV", "PUR", "DISCOUNT", "1", 1);
            }
            else
            {
                SaveData("INV", "PUR", "DISCOUNT", "0", 1);
            }
            SaveData("INV", "PUR", "PURDISCACC", chkpurhasediscountaccount.Checked == true ? "1" : "0", 2);
            SaveData("INV", "PUR", "PURSPLT", chkcnvertigst.Checked == true ? "1" : "0", 3);
            SaveData("INV", "PUR", "PURSTCKUP", chkcnvertigst.Checked == true ? "1" : "0", 4);
            ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "toastr.success('Data Saved successfully')", true);
        }
        else
        {
            ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "toastr.warning('User does not have permission to Update')", true);

        }
    }
    private void cleared()
    {
        if (chkAcountlnking.Checked == false)
            txtdatabase.Text = "";
    }

    //protected bool DisplayData()
    //{
    //    try
    //    {
    //        BlGeneralSettings blgeneralsettings = new BlGeneralSettings();
    //        DataTable dt = new DataTable();
    //        DataTable dt1 = new DataTable();
    //        blgeneralsettings.UserAction = UserAction;
    //        blgeneralsettings.GsModule = NodeValue;
    //        blgeneralsettings.BranchCode = 0;
    //        if (blgeneralsettings.GsModule != "IMG")
    //            dt = (DataTable)blgeneralsettings.SelectData();
    //        else
    //            dt = (DataTable)blgeneralsettings.SelectDatas();

    //        string gsmodule;
    //        string gsfield;
    //        string gsvalue;
    //        CheckEnableIdcardPrinting.Checked = false;
    //        bltErrorList.Items.Clear();
    //        txtCashPaymentLimit.Text = "";
    //        chkLoanNoPaymentTime.Checked = false;
    //        ChkEnableVehicleNumberWiseSearching.Checked = false;
    //        chkRecoveryAsCash.Checked = false;
    //        chkEnableDetailedVoucherPrint.Checked = false;
    //        ChkPrintApplication.Checked = false;
    //        chkThirdPartyHoApproval.Checked = false;
    //        chkAccBulkHoApproval.Checked = false;


    //        if (NodeValue != "IMG")
    //        {


    //            if (dt.Rows.Count > 0)
    //                UserAction = (byte)BlPublic.UserAction.View;
    //            else
    //                UserAction = (byte)BlPublic.UserAction.None;
    //            foreach (DataRow row in dt.Rows)
    //            {
    //                gsmodule = row["GsModule"].ToString();
    //                gsfield = row["GsField"].ToString();
    //                gsvalue = row["GsValue"].ToString();

    //                switch (mvewSettings.ActiveViewIndex)
    //                {
    //                    case VEWSOFTWARSETTINGS:
    //                        switch (gsfield)
    //                        {
    //                            case BlPublic.SoftwareSettings.PROJECT_TYPE:
    //                                ddlProjectType.SelectedValue = gsvalue;
    //                                break;
    //                            case BlPublic.SoftwareSettings.CASHIER_MODULE:
    //                                chkSoftwareSettings.Items.FindByValue("CASHIER_MODULE").Selected = Convert.ToBoolean(gsvalue);
    //                                break;
    //                            case BlPublic.SoftwareSettings.CHATTING_FACILITY:
    //                                chkSoftwareSettings.Items.FindByValue("CHATTING_FACILITY").Selected = Convert.ToBoolean(gsvalue);
    //                                break;
    //                            case BlPublic.SoftwareSettings.DAYBEGIN_PROCESS:
    //                                chkSoftwareSettings.Items.FindByValue("DAYBEGIN_PROCESS").Selected = Convert.ToBoolean(gsvalue);
    //                                break;
    //                            case BlPublic.SoftwareSettings.PALMTECH_CONECTIVITY:
    //                                chkSoftwareSettings.Items.FindByValue("PALMTECH_CONECTIVITY").Selected = Convert.ToBoolean(gsvalue);
    //                                break;
    //                            case BlPublic.SoftwareSettings.PASSBOOK_PRINTING:
    //                                chkSoftwareSettings.Items.FindByValue("PASSBOOK_PRINTING").Selected = Convert.ToBoolean(gsvalue);
    //                                break;
    //                            case BlPublic.SoftwareSettings.SMS_BANKING:
    //                                chkSoftwareSettings.Items.FindByValue("SMS_BANKING").Selected = Convert.ToBoolean(gsvalue);
    //                                break;
    //                            case BlPublic.SoftwareSettings.TRANSACTION_PASSING:
    //                                chkSoftwareSettings.Items.FindByValue("TRANSACTION_PASSING").Selected = Convert.ToBoolean(gsvalue);
    //                                break;
    //                            case BlPublic.SoftwareSettings.MEMBER_SEARCH:
    //                                chkSoftwareSettings.Items.FindByValue("MEMBER_SEARCH").Selected = Convert.ToBoolean(gsvalue);
    //                                break;
    //                            case BlPublic.SoftwareSettings.DELETION_ASON_DATE:
    //                                chkSoftwareSettings.Items.FindByValue("DELETION_ON_ASON_DATE").Selected = Convert.ToBoolean(gsvalue);
    //                                break;
    //                            case BlPublic.SoftwareSettings.OTP_SECURITY:
    //                                chkSoftwareSettings.Items.FindByValue("OTP_SECURITY").Selected = Convert.ToBoolean(gsvalue);
    //                                if (Convert.ToBoolean(gsvalue) == true) showOTP(Convert.ToBoolean(gsvalue));
    //                                break;
    //                            case BlPublic.SoftwareSettings.FINGERPRINT_VERIFICATION:
    //                                chkSoftwareSettings.Items.FindByValue("FINGERPRINT_VERIFICATION").Selected = Convert.ToBoolean(gsvalue);
    //                                if (Convert.ToBoolean(gsvalue) == true) showOTP(Convert.ToBoolean(gsvalue));
    //                                break;
    //                            case BlPublic.SoftwareSettings.BANKEND_CHECKING:
    //                                chkSoftwareSettings.Items.FindByValue("BANKEND_CHECKING").Selected = Convert.ToBoolean(gsvalue);
    //                                break;
    //                            case BlPublic.SoftwareSettings.VOUCHER_MODULEWISE:
    //                                chkSoftwareSettings.Items.FindByValue("MODULE_WISE_VOUCHER").Selected = Convert.ToBoolean(gsvalue);
    //                                break;
    //                            case BlPublic.SoftwareSettings.WAIVE_WITH_NOT_ACCOUNTING:
    //                                chkSoftwareSettings.Items.FindByValue("WAIVE_WITH_NOTACCOUNTING").Selected = Convert.ToBoolean(gsvalue);
    //                                break;
    //                            case BlPublic.SoftwareSettings.UNIQUE_VOUCHER_STANDING:
    //                                chkSoftwareSettings.Items.FindByValue("UNIQUE_VOUCHER_STANDING").Selected = Convert.ToBoolean(gsvalue);
    //                                break;
    //                            case BlPublic.SoftwareSettings.POPUP_FOR_TYPECODE:
    //                                chkSoftwareSettings.Items.FindByValue("POPUP_FOR_TYPECODE").Selected = Convert.ToBoolean(gsvalue);
    //                                break;
    //                            case BlPublic.SoftwareSettings.DELETION_BACK_DATE:
    //                                chkSoftwareSettings.Items.FindByValue("DELETION_ON_BACK_DATE").Selected = Convert.ToBoolean(gsvalue);
    //                                break;
    //                            case BlPublic.SoftwareSettings.OTP_IN_LOAN_AUTHORIZATION:
    //                                chkSoftwareSettings.Items.FindByValue("OTP_IN_LOAN_AUTHORIZATION").Selected = Convert.ToBoolean(gsvalue);
    //                                break;
    //                            //sooraj
    //                            case BlPublic.SoftwareSettings.POS_IN_GL:
    //                                chkSoftwareSettings.Items.FindByValue("POS_IN_GL").Selected = Convert.ToBoolean(gsvalue);
    //                                break;
    //                            case BlPublic.SoftwareSettings.POS_IN_TL:
    //                                chkSoftwareSettings.Items.FindByValue("POS_IN_TL").Selected = Convert.ToBoolean(gsvalue);
    //                                break;
    //                            case BlPublic.SoftwareSettings.POS_POSTING_THROUGH_CORPORATE_OFFICE:
    //                                chkSoftwareSettings.Items.FindByValue("POS_POSTING_THROUGH_CORPORATE_OFFICE").Selected = Convert.ToBoolean(gsvalue);
    //                                break;
    //                            case BlPublic.SoftwareSettings.DISABLE_MANDATORY_IN_NOMINATION:
    //                                chkSoftwareSettings.Items.FindByValue("DISABLE_MANDATORY_IN_NOMINATION").Selected = Convert.ToBoolean(gsvalue);
    //                                break;
    //                            case BlPublic.SoftwareSettings.ENABLE_WAIVE_BUTTON_IN_GOLD_RECIEPT:
    //                                chkSoftwareSettings.Items.FindByValue("ENABLE_WAIVE_BUTTON_IN_GOLD_RECIEPT").Selected = Convert.ToBoolean(gsvalue);
    //                                break;

    //                            //bibin
    //                            case BlPublic.SoftwareSettings.GOLD_AUTO_INTEREST_BOOKING:
    //                                chkSoftwareSettings.Items.FindByValue("GOLD_AUTO_INTEREST_BOOKING").Selected = Convert.ToBoolean(gsvalue);
    //                                break;
    //                            case BlPublic.SoftwareSettings.REMOVE_NOMINEE_FROM_CUSTOMER:
    //                                chkSoftwareSettings.Items.FindByValue("REMOVE_NOMINEE_FROM_CUSTOMER").Selected = Convert.ToBoolean(gsvalue);
    //                                break;
    //                            //DIVYA
    //                            case BlPublic.SoftwareSettings.DISABLE_ALL_OPTION_IN_PASSING:
    //                                chkSoftwareSettings.Items.FindByValue("DISABLE_ALL_OPTION_IN_PASSING").Selected = Convert.ToBoolean(gsvalue);
    //                                break;
    //                            case BlPublic.SoftwareSettings.DISABLE_IB_HO_POSTING:
    //                                chkSoftwareSettings.Items.FindByValue("DISABLE_IB_HO_POSTING").Selected = Convert.ToBoolean(gsvalue);
    //                                break;
    //                            case BlPublic.SoftwareSettings.IMBP_FOR_OUTSTANDING_IN_INDIVIDUALLOAN:
    //                                chkSoftwareSettings.Items.FindByValue("IMBP_FOR_OUTSTANDING_IN_INDIVIDUALLOAN").Selected = Convert.ToBoolean(gsvalue);
    //                                break;
    //                            case BlPublic.SoftwareSettings.DASHBOARD_TYPE:
    //                                ddlDashboardType.SelectedValue = gsvalue;
    //                                break;

    //                        }
    //                        break;
    //                    case VEWAPPLICATIONSETTINGS:
    //                        switch (gsfield)
    //                        {
    //                            case BlPublic.ApplicationSettings.HEADER:
    //                                txtHeader.Text = gsvalue;
    //                                break;
    //                            case BlPublic.ApplicationSettings.FOOTER:
    //                                txtFooter.Text = gsvalue;
    //                                break;
    //                            case BlPublic.ApplicationSettings.INSTALLED_ON:
    //                                txtInstalledOn.Text = gsvalue;
    //                                break;
    //                            case BlPublic.ApplicationSettings.REPORT_FOOTER1:
    //                                txtReportFooter1.Text = gsvalue;
    //                                break;
    //                            case BlPublic.ApplicationSettings.REPORT_FOOTER2:
    //                                txtReportFooter2.Text = gsvalue;
    //                                break;
    //                            case BlPublic.ApplicationSettings.REPORT_FOOTER3:
    //                                txtReportFooter3.Text = gsvalue;
    //                                break;
    //                            case BlPublic.ApplicationSettings.SERIAL_NO:
    //                                txtSerialNo.Text = gsvalue;
    //                                break;
    //                            case BlPublic.ApplicationSettings.CONCURRENT_USERS:
    //                                txtConcurrentUsers.Text = gsvalue;
    //                                break;
    //                            case BlPublic.ApplicationSettings.FIN_YEAR:
    //                                txtFinYear.Text = gsvalue;
    //                                break;
    //                            case BlPublic.ApplicationSettings.FIN_START_ON:
    //                                txtFinStartOn.Text = gsvalue;
    //                                break;
    //                            case BlPublic.ApplicationSettings.FIN_END_ON:
    //                                txtFinEndOn.Text = gsvalue;
    //                                break;
    //                            case BlPublic.ApplicationSettings.REGIONAL_FONT:
    //                                ddlRegionalFont.SelectedValue = gsvalue;
    //                                break;
    //                            case BlPublic.ApplicationSettings.EMAIL:
    //                                txtMail.Text = gsvalue;
    //                                break;
    //                            case BlPublic.ApplicationSettings.WEB:
    //                                txtWeb.Text = gsvalue;
    //                                break;
    //                            case BlPublic.ApplicationSettings.AMC_DUE_DATE:
    //                                txtAMCDueDate.Text = Decrypt(gsvalue);
    //                                break;
    //                        }
    //                        break;
    //                    case VEWSECURITYSETTINGS:
    //                        switch (gsfield)
    //                        {
    //                            case BlPublic.SecuritySettings.PASSWORD_MIN_LENGTH:
    //                                txtPwdMinLength.Text = gsvalue;
    //                                break;
    //                            case BlPublic.SecuritySettings.PASSWORD_MAX_LENGTH:
    //                                txtPwdMaxLength.Text = gsvalue;
    //                                break;
    //                            case BlPublic.SecuritySettings.PASSWORD_CHANGE_PERIOD:
    //                                txtPwdChangePeriod.Text = gsvalue;
    //                                break;
    //                            case BlPublic.SecuritySettings.PASSWORD_HISTORYCHECK:
    //                                txtpwdhistoryCheck.Text = gsvalue;
    //                                break;
    //                            case BlPublic.SecuritySettings.PASSWORD_LOGIN_BLOCKDAYS:
    //                                txtPwdLoginBlockDays.Text = gsvalue;
    //                                break;
    //                            case BlPublic.SecuritySettings.PASSWORD_COMBINATION:
    //                                ddlPwdCombination.SelectedValue = gsvalue;
    //                                break;
    //                            case BlPublic.SecuritySettings.LOGIN_ATTEMPT:
    //                                txtloginAttempt.Text = gsvalue;
    //                                break;
    //                            case BlPublic.SecuritySettings.OTP_LENGTH:
    //                                txtOTPLength.Text = gsvalue;
    //                                break;
    //                            case BlPublic.SecuritySettings.OTP_ATTEMPT:
    //                                txtOTPAttempt.Text = gsvalue;
    //                                break;
    //                            case BlPublic.SecuritySettings.OTP_VALIDITY:
    //                                txtOTPValidity.Text = gsvalue;
    //                                break;
    //                            case BlPublic.SecuritySettings.DASHBOARD_DATABASE:
    //                                txtdashboarddatabase.Text = gsvalue;
    //                                break;
    //                            case BlPublic.SecuritySettings.DASHBOARD_LINK:
    //                                txtDashboardLink.Text = gsvalue;
    //                                break;
    //                            case BlPublic.SecuritySettings.ENABLE_THREE_DIGIT_IN_GOLDLOAN:
    //                                chkgoldloan.Checked = Convert.ToBoolean(gsvalue);
    //                                break;
    //                            case BlPublic.SecuritySettings.ENABLE_TD_WITHOUT_APPLICATION:
    //                                chkTDWithoutApplication.Checked = Convert.ToBoolean(gsvalue);
    //                                break;
    //                            case BlPublic.SecuritySettings.AUTO_GENERATE_USERCODE:
    //                                chkAutoGenerateUserCode.Checked = Convert.ToBoolean(gsvalue);
    //                                break;
    //                            case BlPublic.SecuritySettings.USER_CODE_PREFIX:
    //                                txtUserCodePrefix.Text = gsvalue;
    //                                break;

    //                        }
    //                        break;
    //                    case VEWAUTOMATIONSETTINGS:
    //                        switch (gsfield)
    //                        {
    //                            case BlPublic.AutomationSettings.PRD_SB_INT_APPLICATION:
    //                                txtSBIntAppl.Text = gsvalue;
    //                                break;
    //                            case BlPublic.AutomationSettings.PRD_CA_INT_APPLICATION:
    //                                txtCAIntAppl.Text = gsvalue;
    //                                break;
    //                            case BlPublic.AutomationSettings.PRD_OD_INT_APPLICATION:
    //                                txtODIntAppl.Text = gsvalue;
    //                                break;
    //                            case BlPublic.AutomationSettings.PRD_FOLIOCHARGE_POSTING:
    //                                txtFolioChargePosting.Text = gsvalue;
    //                                break;
    //                            case BlPublic.AutomationSettings.PRD_CHARGE_POSTING:
    //                                txtChargePosting.Text = gsvalue;
    //                                break;
    //                            case BlPublic.AutomationSettings.PRD_MINIMUMBALANCE:
    //                                txtMinimumBalance.Text = gsvalue;
    //                                break;
    //                            case BlPublic.AutomationSettings.PRD_UNHEALTHYACCPERIOD:
    //                                txtUnhealthyAccPeriod.Text = gsvalue;
    //                                break;
    //                            case BlPublic.AutomationSettings.PRD_MINORTOMAJOR:
    //                                txtMinorToMajor.Text = gsvalue;
    //                                break;
    //                            case BlPublic.AutomationSettings.COUNT_UNHEALTHYACC:
    //                                txtUnhealthyAccCount.Text = gsvalue;
    //                                break;
    //                            case BlPublic.AutomationSettings.SB_INT_APPLICATION:
    //                                chkAutoSettings.Items.FindByValue("AUTSB").Selected = Convert.ToBoolean(gsvalue);
    //                                break;
    //                            case BlPublic.AutomationSettings.CA_INT_APPLICATION:
    //                                chkAutoSettings.Items.FindByValue("AUTCA").Selected = Convert.ToBoolean(gsvalue);
    //                                break;
    //                            case BlPublic.AutomationSettings.OD_INT_APPLICATION:
    //                                chkAutoSettings.Items.FindByValue("AUTOD").Selected = Convert.ToBoolean(gsvalue);
    //                                break;
    //                            case BlPublic.AutomationSettings.FOLIOCHARGE_POSTING:
    //                                chkAutoSettings.Items.FindByValue("AUTFO").Selected = Convert.ToBoolean(gsvalue);
    //                                break;
    //                            case BlPublic.AutomationSettings.SB_Operative_InOperative:
    //                                chkAutoSettings.Items.FindByValue("SBOPIP").Selected = Convert.ToBoolean(gsvalue);
    //                                break;
    //                            case BlPublic.AutomationSettings.CA_Operative_InOperative:
    //                                chkAutoSettings.Items.FindByValue("CAOPIP").Selected = Convert.ToBoolean(gsvalue);
    //                                break;
    //                            case BlPublic.AutomationSettings.SB_InOperative_UnOperative:
    //                                chkAutoSettings.Items.FindByValue("SBIPUP").Selected = Convert.ToBoolean(gsvalue);
    //                                break;
    //                            case BlPublic.AutomationSettings.CA_InOperative_UnOperative:
    //                                chkAutoSettings.Items.FindByValue("CAIPUP").Selected = Convert.ToBoolean(gsvalue);
    //                                break;

    //                        }
    //                        break;
    //                    case VEWMODULESETTINGS:
    //                        switch (gsfield)
    //                        {
    //                            case BlPublic.ModuleSettings.LOCKING_DATE:
    //                                txtLockingDate.Text = gsvalue;
    //                                break;
    //                            case BlPublic.ModuleSettings.KEEP_DATA_YEAR_ENDING:
    //                                txtKeepData.Text = gsvalue;
    //                                break;
    //                            case BlPublic.ModuleSettings.ROUNDING:
    //                                ddlRounding.SelectedValue = gsvalue;
    //                                break;
    //                            case BlPublic.ModuleSettings.YEAREND_PROCESS:
    //                                chkModSettings.Items.FindByValue("YEAREND_PROCESS").Selected = Convert.ToBoolean(gsvalue);
    //                                break;
    //                            case BlPublic.ModuleSettings.FREEZEXISTING:
    //                                chkModSettings.Items.FindByValue("FREEZEXISTING").Selected = Convert.ToBoolean(gsvalue);
    //                                break;
    //                            case BlPublic.ModuleSettings.MIN_INTEREST:
    //                                txtMinInterest.Text = gsvalue;
    //                                break;
    //                            case BlPublic.ModuleSettings.MAX_INTEREST:
    //                                txtMaxInterest.Text = gsvalue;
    //                                break;
    //                            case BlPublic.ModuleSettings.ENABLETRANS_WISEROUNDING:
    //                                ChkTransaction.Checked = Convert.ToBoolean(gsvalue);
    //                                break;
    //                            case BlPublic.ModuleSettings.AGENTCHECKING_REMITTANCE:
    //                                chkAgentChecking.Checked = Convert.ToBoolean(gsvalue);
    //                                break;
    //                            case BlPublic.ModuleSettings.CASHPAYMENTLIMIT:
    //                                txtCashPaymentLimit.Text = gsvalue;
    //                                break;
    //                            case BlPublic.ModuleSettings.ENABLE_IDCARDPRINTING:
    //                                CheckEnableIdcardPrinting.Checked = Convert.ToBoolean(gsvalue);
    //                                break;
    //                            case BlPublic.ModuleSettings.ENABLE_LOAN_NO_AT_PAYMENT_TIME:
    //                                chkLoanNoPaymentTime.Checked = Convert.ToBoolean(gsvalue);
    //                                break;
    //                            case BlPublic.ModuleSettings.ENABLE_WAIVE_FOR_INTEREST:
    //                                chkEnableWaiveForInterest.Checked = Convert.ToBoolean(gsvalue);
    //                                break;
    //                            case BlPublic.ModuleSettings.ENABLE_RECOVERY_AS_CASH:
    //                                chkRecoveryAsCash.Checked = Convert.ToBoolean(gsvalue);
    //                                break;
    //                            case BlPublic.ModuleSettings.CONSIDER_MIN_INT_DAYS:
    //                                chkConsiderMinDaysInt.Checked = Convert.ToBoolean(gsvalue);
    //                                break;
    //                            case BlPublic.ModuleSettings.ALLOW_EXPENSE_WAIVE_WITH_OUT_ACCOUNTING:
    //                                chkExpenceWaiveWithoutAccounting.Checked = Convert.ToBoolean(gsvalue);
    //                                break;
    //                            case BlPublic.ModuleSettings.STANDING_ON_START_OF_MONTH://..............24.09.2014
    //                                chkStandingOnStartOfMonth.Checked = Convert.ToBoolean(gsvalue);
    //                                break;
    //                            case BlPublic.ModuleSettings.ENABLE_FINGERPRINT_AUTHENTICATION://..............22.10.2014
    //                                chkFingerprintAuthentication.Checked = Convert.ToBoolean(gsvalue);
    //                                break;
    //                            case BlPublic.ModuleSettings.ENABLE_GOLDRENEWALFROMEXISTINGLOAN://..............09.02.2015
    //                                chkEnableRenewFromExistingLoan.Checked = Convert.ToBoolean(gsvalue);
    //                                break;
    //                            case BlPublic.ModuleSettings.ENABLE_DETAILEDVOUCHERPRINT:
    //                                chkEnableDetailedVoucherPrint.Checked = Convert.ToBoolean(gsvalue);
    //                                break;
    //                            case BlPublic.ModuleSettings.ENABLE_PHOTOMANDATORY:
    //                                chkPhotoMandatory.Checked = Convert.ToBoolean(gsvalue);
    //                                break;
    //                            case BlPublic.ModuleSettings.ENABLE_VEHICLE_NO_SEARCHING:
    //                                ChkEnableVehicleNumberWiseSearching.Checked = Convert.ToBoolean(gsvalue);
    //                                break;
    //                            case BlPublic.ModuleSettings.BOND_PRINT_FORMAT: //..............16.10.2021
    //                                ddlBondPrintFormat.SelectedValue = gsvalue;
    //                                break;
    //                            case BlPublic.ModuleSettings.PRINT_CERTIFICATE:
    //                                ChkPrintCertificate.Checked = Convert.ToBoolean(gsvalue);
    //                                break;
    //                            case BlPublic.ModuleSettings.PRINT_APPLICATION:
    //                                ChkPrintApplication.Checked = Convert.ToBoolean(gsvalue);
    //                                break;
    //                            case BlPublic.ModuleSettings.DEFAULTMFCOLLECTIONDATE:
    //                                ddlMFcollectionDate.SelectedValue = gsvalue;
    //                                break;
    //                            case BlPublic.ModuleSettings.REALIZATIONPRINTFORMAT:
    //                                ddlRealizationPrint.SelectedValue = gsvalue;
    //                                break;
    //                            case BlPublic.ModuleSettings.ENABLE_GOLD_OTP:
    //                                chkEnableOTP.Checked = Convert.ToBoolean(gsvalue);
    //                                break;
    //                            case BlPublic.ModuleSettings.ENABLE_GoldTransaction_OTP:
    //                                chkEnableOTPTransaction.Checked = Convert.ToBoolean(gsvalue);
    //                                break;
    //                            case BlPublic.ModuleSettings.SL_TOTAL_LOAN_AMOUNT:
    //                                txtMaxLoanAmount.Text = gsvalue;
    //                                break;
    //                            case BlPublic.ModuleSettings.INCLUDE_IN_IMBPLIMIT:
    //                                chkIMBPjl.Checked = Convert.ToBoolean(gsvalue);
    //                                if (chkIMBPjl.Checked == true)
    //                                {
    //                                    txtMaxLoanAmount.Visible = false;
    //                                    lblSecLoanMaxLimitAmt.Visible = false;
    //                                    txtMaxLoanAmount.Text = "0";
    //                                }
    //                                else
    //                                {
    //                                    txtMaxLoanAmount.Visible = true;
    //                                    lblSecLoanMaxLimitAmt.Visible = true;
    //                                }
    //                                break;
    //                            case BlPublic.ModuleSettings.ENABLE_SHAREAPPLICATION_OTP:
    //                                chkEnableOTP.Checked = Convert.ToBoolean(gsvalue);
    //                                break;
    //                            case BlPublic.ModuleSettings.THIRDPARTY_HOAPPROVAL:
    //                                chkThirdPartyHoApproval.Checked = Convert.ToBoolean(gsvalue);
    //                                break;
    //                            case BlPublic.ModuleSettings.ACCBULKUPLOAD_HOAPPROVAL:
    //                                chkAccBulkHoApproval.Checked = Convert.ToBoolean(gsvalue);
    //                                break;
    //                            case BlPublic.ModuleSettings.TL_MAX_SUERTY_LIMIT:
    //                                txtTlSuertyLimit.Text = gsvalue;
    //                                break;
    //                            case BlPublic.ModuleSettings.GDS_MAX_SUERTY_LIMIT:
    //                                txtGDSSuertyLimit.Text = gsvalue;
    //                                break;
    //                            case BlPublic.ModuleSettings.CUS_MAX_SUERTY_LIMIT:
    //                                TxtMaxSurLmt.Text = gsvalue;
    //                                break;
    //                            case BlPublic.ModuleSettings.ENABLE_CUSTOMER_OTP:
    //                                chkEnableOTP.Checked = Convert.ToBoolean(gsvalue);
    //                                break;
    //                        }
    //                        break;


    //                }

    //            }
    //        }
    //        else
    //        {
    //            if (dt.Rows.Count > 0)
    //                UserAction = (byte)BlPublic.UserAction.View;
    //            else
    //                UserAction = (byte)BlPublic.UserAction.None;


    //            BlFunctions blfunction = new BlFunctions();
    //            Int64 ID_Image1 = 0;
    //            Int64 ID_Image2 = 0;
    //            Int64 ID_Image3 = 0;
    //            Int64 ID_Image4 = 0;
    //            ID_Image1 = Convert.ToInt64(blfunction.GetScalarValue("GeneralImage", "ISNULL((SELECT   ID_Image", "GsField='IMG001'),0)", ViewState["DBNAME"].ToString()).ToString());

    //            //  ID_Image1 = Convert.ToInt64(blfunction.GetScalarValue("GeneralImage ", " isnull ((select top 1 ID_Image ", "0=0),0)", ViewState["DBNAME"].ToString()));
    //            //ID_Image2 = Convert.ToInt64(blfunction.GetScalarValue("GeneralImage ", "top 1 isnull(ID_Image,0)", " 0=0 ORDER BY ID_Image DESC", ViewState["DBNAME"].ToString()));


    //            ID_Image2 = Convert.ToInt64(blfunction.GetScalarValue("GeneralImage ", "isnull (( SELECT  ID_Image ", "GsField='IMG002' ),0)", ViewState["DBNAME"].ToString()));
    //            ID_Image3 = Convert.ToInt64(blfunction.GetScalarValue("GeneralImage ", "isnull (( SELECT  ID_Image ", "GsField='IMG003' ),0)", ViewState["DBNAME"].ToString()));
    //            ID_Image4 = Convert.ToInt64(blfunction.GetScalarValue("GeneralImage ", "isnull (( SELECT  ID_Image ", "GsField='IMG004' ),0)", ViewState["DBNAME"].ToString()));

    //            if (ID_Image1 > 0)
    //                SelectImage(ID_Image1, "Reportheading");
    //            if (ID_Image2 > 0)
    //                SelectImage(ID_Image2, "Reportwatermark");
    //            if (ID_Image3 > 0)
    //                SelectImage(ID_Image3, "Reportfooter");
    //            if (ID_Image4 > 0)
    //                SelectImage(ID_Image4, "Reportlogo");
    //        }

    //        return true;


    //    }
    //    catch (Exception e1)
    //    {
    //        bltErrorList.Items.Add(e1.Message);
    //        return false;
    //    }
    //}
}