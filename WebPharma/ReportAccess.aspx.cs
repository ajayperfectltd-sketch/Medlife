/*----------------------------------------------------------------------------------------------------------------------------------------------------------
Purpose    : Report Access 
Author     : AISWARYA 
Copyright  : Perfect Software Solutions (Clt) Pvt Ltd
Created on : 23/01/2019
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
using WebPharma.Business.Masters; 
using System.Data;
using System.Collections;

public partial class ReportAccess : System.Web.UI.Page
{
    #region Functions
    public Int64 ID_ReportAccess
    {
        get { return Convert.ToInt64(hfMasterId.Value); }
        set { hfMasterId.Value = value.ToString(); }
    }
    public void BindData()
    {

        Populate populate = new Populate();
        populate.FillDropdownList(ddlUserGroup, "UserGroup", "GroupName", "ID_UserGroup", "ID_UserGroup", "Active = 1 and Cancelled=0 and FK_Client = " + hfClient.Value, "");
    populate = null;

    }
    protected void FillReportMenu()
    {

        BlReportAccess blReportAccess = new BlReportAccess();
        blReportAccess.Type = 1;
        DataTable dtbl = new DataTable();
        dtbl = blReportAccess.SelectReportCategory();
        RepeaterWebPharma.DataSource = dtbl;
        RepeaterWebPharma.DataBind();

        blReportAccess.Type = 2;
        DataTable dtb2 = new DataTable();
        dtbl = blReportAccess.SelectReportCategory();
        RepeaterAccounts.DataSource = dtbl;
        RepeaterAccounts.DataBind();
        ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "ReportType();", true);
    }
    protected void SaveData()
    {
        try
        {
            if (hfIsUpdate.Value.ToLower() == "true")
            {
                long status = 0;
                BlReportAccess blReportAccess = new BlReportAccess();
                blReportAccess.FK_UserGroup = Convert.ToInt64(ddlUserGroup.SelectedValue);
                blReportAccess.FK_Client = Convert.ToInt64(hfClient.Value);
                blReportAccess.MasterId = ID_ReportAccess;
                blReportAccess.UserCode = hfuserName.Value;
                ReportSubCategory ReportSubCategory;
                ArrayList arlist = new ArrayList();
                foreach (RepeaterItem rptrCategory in RepeaterWebPharma.Controls)
                {
                    DataList dlReportAccess = (DataList)rptrCategory.FindControl("dlReportAccess");
                    foreach (DataListItem dlItem in dlReportAccess.Items)
                    {
                        if (dlItem != null)
                        {
                            bool chkReportSubCategory = Convert.ToBoolean(((CheckBox)dlItem.FindControl("chkReportSubCategory")).Checked);
                            if (chkReportSubCategory)
                            {
                                ReportSubCategory = new ReportSubCategory();
                                ReportSubCategory._FK_ReportSubCategory = Convert.ToInt64(((HiddenField)dlItem.FindControl("hfID_ReportSubCategory")).Value.ToString());
                                ReportSubCategory._ReportSubType = 1;
                              
                                arlist.Add(ReportSubCategory);
                                
                                ReportSubCategory = null;
                            }
                        }
                    }
                }
                foreach (RepeaterItem rptrAccounts in RepeaterAccounts.Controls)
                {
                    DataList dlReportAccess = (DataList)rptrAccounts.FindControl("dlReportAccess");
                    foreach (DataListItem dlItem in dlReportAccess.Items)
                    {
                        if (dlItem != null)
                        {
                            bool chkReportSubCategory = Convert.ToBoolean(((CheckBox)dlItem.FindControl("chkReportSubCategory")).Checked);
                            if (chkReportSubCategory)
                            {
                                ReportSubCategory = new ReportSubCategory();
                                ReportSubCategory._FK_ReportSubCategory = Convert.ToInt64(((HiddenField)dlItem.FindControl("hfID_ReportSubCategory")).Value.ToString());
                                ReportSubCategory._ReportSubType = 2;
                                arlist.Add(ReportSubCategory);                                
                                ReportSubCategory = null;
                            }
                        }
                    }
                }
                blReportAccess.XmlReportAccess = Common.WriteToXml(arlist);
                status = blReportAccess.InsertData();
                //if (status > 0)
                //{
                Populate.MessageBox(this, "Data Submitted Successfully");
                ddlUserGroup.SelectedIndex = 0;
                rdobtnType.SelectedIndex = 0;
                FillReportMenu();
                // }
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
    protected void CheckUserAccess()
    {
        BlUserAccess bluseraccess = new BlUserAccess();
        Populate populate = new Populate();

        bluseraccess.FK_Users = Convert.ToInt64(hfUser.Value);
        bluseraccess.FK_Pages = 27;
        bluseraccess.FK_Client = Convert.ToInt64(hfClient.Value);
        bluseraccess.CheckUserAccess();
        //if (Convert.ToBoolean(bluseraccess.IsView) == false)
        //{
        //    // Response.Redirect("Home.aspx?");

        //}
        hfIsUpdate.Value = (Convert.ToBoolean(bluseraccess.IsUpdate)).ToString();        
        bluseraccess = null;
    }
    #endregion

    #region Events
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {

            hfUser.Value = Request.QueryString["uid"];
            hfClient.Value = Request.QueryString["clnt"];
            BindData();
            FillReportMenu();
            CheckUserAccess();
            ddlUserGroup.Focus();
            hfuserName.Value = Session["Username"].ToString();
        }
    }

    protected void ddlUserGroup_SelectedIndexChanged(object sender, EventArgs e)
    {
        Populate populate = new Populate();
        string str = "";
        string[] s = hfClient.Value.Split(',');
        string c = s[0];
       // ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "ClearItems()", true);
        FillReportMenu();
        populate = null;
    }
    protected void ddlUser_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillReportMenu();
    }
    protected void RepeaterReportCategory_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            try
            {
                HiddenField hf = (HiddenField)e.Item.FindControl("hfID_ReportCategory");
                BlReportAccess blReportAccess = new BlReportAccess();
                blReportAccess.FK_ReportCategory = Convert.ToInt64(hf.Value);
                blReportAccess.FK_Client = Convert.ToInt64(hfClient.Value);
                blReportAccess.FK_UserGroup = Convert.ToInt64(ddlUserGroup.SelectedValue);
                blReportAccess.Type = 1;
                DataList DataList = (DataList)e.Item.FindControl("dlReportAccess");
                DataTable dtbl = new DataTable();
                dtbl = blReportAccess.SelectReportAccess();
                DataList.DataSource = dtbl;
                DataList.DataBind();
            }
            catch (Exception ex)
            {

            }
        }
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            blMessage.Items.Clear();
            SaveData();
        }
        catch (Exception ex)
        {
            Populate.WarningMsg(this, ex.Message);
        }

    }
    public bool ReportAcc(object obj)
    {
        string access = obj.ToString();
        bool x = Convert.ToBoolean(access);
        return x;
    }
   
    protected void RepeaterAccounts_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            try
            {
                HiddenField hf = (HiddenField)e.Item.FindControl("hfID_ReportCategory");
                BlReportAccess blReportAccess = new BlReportAccess();
                blReportAccess.FK_ReportCategory = Convert.ToInt64(hf.Value);
                blReportAccess.FK_UserGroup = Convert.ToInt64(ddlUserGroup.SelectedValue.ToString());
                blReportAccess.FK_Client = Convert.ToInt64(hfClient.Value);
                blReportAccess.Type = 2;
                DataList DataList = (DataList)e.Item.FindControl("dlReportAccess");
                DataTable dtbl = new DataTable();
                dtbl = blReportAccess.SelectReportAccess();
                DataList.DataSource = dtbl;
                DataList.DataBind();
            }
            catch (Exception ex)
            {

            }
        }
    }
    #endregion
}