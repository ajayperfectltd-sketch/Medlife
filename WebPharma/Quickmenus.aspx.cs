using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebPharma.Business.Masters;

public partial class Quickmenus : System.Web.UI.Page
{
    #region Functions
    public Int64 ID_UserAccess
    {
        get { return Convert.ToInt64(hfMasterId.Value); }
        set { hfMasterId.Value = value.ToString(); }
    }
    protected void FillPages()
    {
        BlUserAccess bluseraccess = new BlUserAccess();
        DataTable dtbl = new DataTable();
        dtbl = bluseraccess.SelectPages();
        //dlUserAccess.DataSource = dtbl;
        // dlUserAccess.DataBind();
    }
    protected void BindData()
    {
        Populate populate = new Populate();

        populate.FillDropdownList(ddlUser, "Users", "Username", "ID_Users", "", "Active = 1 and   Cancelled=0 and FK_Client = " + hfClient.Value, "");
        populate.FillDropdownList(ddlPage1, "Pages", "PageName", "ID_Pages", "", "Active = 1", "");
        populate.FillDropdownList(ddlPage2, "Pages", "PageName", "ID_Pages", "", "Active = 1", "");
        populate.FillDropdownList(ddlPage3, "Pages", "PageName", "ID_Pages", "", "Active = 1", "");
        populate.FillDropdownList(ddlPage4, "Pages", "PageName", "ID_Pages", "", "Active = 1", "");
        populate.FillDropdownList(ddlPage5, "Pages", "PageName", "ID_Pages", "", "Active = 1", "");
        populate.FillDropdownList(ddlPage6, "Pages", "PageName", "ID_Pages", "", "Active = 1", "");
        populate = null;
    }
    protected void CheckUserAccess()
    {
        BlUserAccess bluseraccess = new BlUserAccess();
        Populate populate = new Populate();

        bluseraccess.FK_Users = Convert.ToInt64(hfUser.Value);
        bluseraccess.FK_Pages = 55;
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
    protected void SaveData()
    {
        BlUserAccess bluseraccess = new BlUserAccess();
        UserAccessPages useraccesspages;
        bluseraccess.FK_Users = Convert.ToInt64(ddlUser.SelectedValue.ToString());
        bluseraccess.UserCode = hfUser.Value;
        bluseraccess.FK_Client = Convert.ToInt64(hfClient.Value);
        bluseraccess.MasterId = ID_UserAccess;
        ArrayList arlist = new ArrayList();
        //foreach (DataListItem dlItem in dlUserAccess.Items)
        //{
        //    if (dlItem != null)
        //    {
        //        bool isadd = Convert.ToBoolean(((CheckBox)dlItem.FindControl("chkIsCheck")).Checked);
        //    //bool IsUpdate = Convert.ToBoolean(((CheckBox)dlItem.FindControl("chkUpdate")).Checked);
        //    //bool IsView = Convert.ToBoolean(((CheckBox)dlItem.FindControl("chkView")).Checked);
        //    //bool IsDelete = Convert.ToBoolean(((CheckBox)dlItem.FindControl("chkDelete")).Checked);
        //        if (isadd)
        //     {
        //        useraccesspages = new UserAccessPages();
        //        useraccesspages.FK_Pages = Convert.ToInt64(((HiddenField)dlItem.FindControl("hfPagesId")).Value.ToString());
        //        useraccesspages.IsAdd = isadd;
        //        //useraccesspages.isadd = IsDelete;
        //        //useraccesspages.IsUpdate = IsUpdate;
        //        //useraccesspages.IsView = IsView;
        //        arlist.Add(useraccesspages);
        //        useraccesspages = null;
        //      }
        //    }
        //}
        bluseraccess.XmlUserAccess = Common.WriteToXml(arlist);
        bluseraccess.FK_Pages1 = Convert.ToInt64(ddlPage1.SelectedValue.ToString());
        bluseraccess.FK_Pages2 = Convert.ToInt64(ddlPage2.SelectedValue.ToString());
        bluseraccess.FK_Pages3 = Convert.ToInt64(ddlPage3.SelectedValue.ToString());
        bluseraccess.FK_Pages4 = Convert.ToInt64(ddlPage4.SelectedValue.ToString());
        bluseraccess.FK_Pages5 = Convert.ToInt64(ddlPage5.SelectedValue.ToString());
        bluseraccess.FK_Pages6 = Convert.ToInt64(ddlPage6.SelectedValue.ToString());

        if (ddlPage1.SelectedValue.ToString() == ddlPage2.SelectedValue.ToString() || ddlPage1.SelectedValue.ToString() == ddlPage3.SelectedValue.ToString() ||
            ddlPage1.SelectedValue.ToString() == ddlPage4.SelectedValue.ToString() || ddlPage1.SelectedValue.ToString() == ddlPage5.SelectedValue.ToString() ||
            ddlPage1.SelectedValue.ToString() == ddlPage6.SelectedValue.ToString() || ddlPage2.SelectedValue.ToString() == ddlPage1.SelectedValue.ToString() ||
            ddlPage2.SelectedValue.ToString() == ddlPage3.SelectedValue.ToString() || ddlPage2.SelectedValue.ToString() == ddlPage4.SelectedValue.ToString() ||
            ddlPage2.SelectedValue.ToString() == ddlPage5.SelectedValue.ToString() || ddlPage2.SelectedValue.ToString() == ddlPage6.SelectedValue.ToString() ||
            ddlPage3.SelectedValue.ToString() == ddlPage1.SelectedValue.ToString() || ddlPage3.SelectedValue.ToString() == ddlPage2.SelectedValue.ToString() ||
            ddlPage3.SelectedValue.ToString() == ddlPage4.SelectedValue.ToString() || ddlPage3.SelectedValue.ToString() == ddlPage5.SelectedValue.ToString() ||
            ddlPage3.SelectedValue.ToString() == ddlPage6.SelectedValue.ToString() || ddlPage4.SelectedValue.ToString() == ddlPage1.SelectedValue.ToString() ||
            ddlPage4.SelectedValue.ToString() == ddlPage2.SelectedValue.ToString() || ddlPage4.SelectedValue.ToString() == ddlPage3.SelectedValue.ToString() ||
            ddlPage4.SelectedValue.ToString() == ddlPage5.SelectedValue.ToString() || ddlPage4.SelectedValue.ToString() == ddlPage6.SelectedValue.ToString() ||
            ddlPage5.SelectedValue.ToString() == ddlPage1.SelectedValue.ToString() || ddlPage5.SelectedValue.ToString() == ddlPage2.SelectedValue.ToString() ||
            ddlPage5.SelectedValue.ToString() == ddlPage3.SelectedValue.ToString() || ddlPage5.SelectedValue.ToString() == ddlPage4.SelectedValue.ToString() ||
            ddlPage5.SelectedValue.ToString() == ddlPage6.SelectedValue.ToString() || ddlPage6.SelectedValue.ToString() == ddlPage1.SelectedValue.ToString() ||
            ddlPage6.SelectedValue.ToString() == ddlPage2.SelectedValue.ToString() || ddlPage6.SelectedValue.ToString() == ddlPage3.SelectedValue.ToString() ||
            ddlPage6.SelectedValue.ToString() == ddlPage4.SelectedValue.ToString() || ddlPage6.SelectedValue.ToString() == ddlPage5.SelectedValue.ToString())
        {
            ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "toastr.warning('Cannot Insert Duplicate Values')", true);
        }

        else
        {
            if (hfIsUpdate.Value.ToLower() == "true")
            {

                long status = bluseraccess.UpdateQuickmenuData();
                if (status > 0)
                {
                    Populate.MessageBox(this, "Data Submitted Successfully");
                }
                else
                {
                    Populate.ErrorMessageBox(this, "Cannot submit data");
                }
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "toastr.warning('User does not have permission to Update')", true);
            }
        }
    }


    protected void ResetQuickMenu()
    {
        ddlPage1.SelectedIndex = 0;
        ddlPage2.SelectedIndex = 0;
        ddlPage3.SelectedIndex = 0;
        ddlPage4.SelectedIndex = 0;
        ddlPage5.SelectedIndex = 0;
        ddlPage6.SelectedIndex = 0;
    }
    //protected void CheckAccessRights()
    //{
    //    BlUserAccess bluseraccess = new BlUserAccess();
    //    bluseraccess.FK_Users = 1;//Convert.ToInt64(hfClient.Value);
    //    bluseraccess.FK_Pages = 30;
    //    bluseraccess.SelectPageSettings();
    //    if (Convert.ToBoolean(bluseraccess.IsView) == false)
    //    {
    //        Response.Redirect("Home.aspx");
    //    }
    //    hfAddPermission.Value = (Convert.ToBoolean(bluseraccess.IsAdd)).ToString();
    //    hfUpdatePermission.Value = (Convert.ToBoolean(bluseraccess.IsUpdate)).ToString();
    //    bluseraccess = null;
    //}

    #endregion
    protected void Page_Load(object sender, EventArgs e)
    {

        try
        {
            if (!IsPostBack)
            {

                hfUser.Value = Request.QueryString["uid"];
                hfClient.Value = Request.QueryString["clnt"];


                BindData();
                CheckUserAccess();
                // blMessage.Items.Clear();
                FillPages(); ddlUser.Focus();
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "toastr.warning('" + ex.Message + "')", true);
        }

    }
     protected void ddlUser_SelectedIndexChanged(object sender, EventArgs e)
    {
     
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
           // blMessage.Items.Clear();
            //if (hfUpdatePermission.Value.ToLower() == "true" && hfAddPermission.Value.ToLower() == "true")
            //{
                SaveData();
                ddlUser.SelectedIndex = 0;
              //  ddlUser.SelectedIndex = 0;
                FillPages();
                ResetQuickMenu();
                ddlUser.Focus();
                //blMessage.Items.Add("Data saved successfully");
               
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
    //protected void btnHidden_Click(object sender, EventArgs e)
    //{

    //    Populate populate = new Populate();
    //    string str = "";
    //    string[] s =hfClient.Value.Split(',');
    //    string c = s[0];
           
    //    if (ddlUserGroup.SelectedIndex != 0)
    //    {
         
    //        str = ddlUserGroup.SelectedValue.ToString();
    //        populate.FillDropdownList(ddlUser, "Users", "Name", "ID_Users", "", "Active = 1 and  FK_UserGroup = " + str + "  and FK_Client = " + c, "");
            
    //    }

    //    else
    //    {
    //        populate.FillDropdownList(ddlUser, "Users", "Name", "ID_Users", "", "Active = 1  and FK_Client = " + c, "");
           
    //    }
    //    FillPages();
    //    ResetQuickMenu();
    //    //ddlUser.Focus();
    //    populate = null;

       
        
       
    //}
    //protected void btnFill_Click(object sender, EventArgs e)
    //{
    //    try
    //    {
    //      //  blMessage.Items.Clear();
    //        ResetQuickMenu();
    //        if (ddlUser.SelectedIndex > 0)
    //        {
    //            DataSet dset = new DataSet();
    //            DataTable dtbl = new DataTable();
    //            DataTable dtblMenu = new DataTable();
    //            BlUserAccess bluseraccess = new BlUserAccess();
    //            bluseraccess.FK_Users = Convert.ToInt64(ddlUser.SelectedValue.ToString());
    //            bluseraccess.FK_Client = Convert.ToInt64(hfClient.Value);
    //            dset = bluseraccess.SelectUserAccess();
    //            dtbl = dset.Tables[0];
    //            dtblMenu = dset.Tables[1];
    //            if (dtbl.Rows.Count > 0)
    //            {
    //                dlUserAccess.DataSource = dtbl;
    //                dlUserAccess.DataBind();
    //            }
    //            else
    //            {
    //                FillPages();
    //            }
    //            if (dtblMenu.Rows.Count > 0)
    //            {
    //                ddlPage1.SelectedValue = dtblMenu.Rows[0]["FK_pages1"].ToString();
    //                ddlPage2.SelectedValue = dtblMenu.Rows[0]["FK_pages2"].ToString();
    //                ddlPage3.SelectedValue = dtblMenu.Rows[0]["FK_pages3"].ToString();
    //                ddlPage4.SelectedValue = dtblMenu.Rows[0]["FK_pages4"].ToString();
    //                ddlPage5.SelectedValue = dtblMenu.Rows[0]["FK_pages5"].ToString();
    //                ddlPage6.SelectedValue = dtblMenu.Rows[0]["FK_pages6"].ToString();
    //            }
    //            else
    //            {
    //                ResetQuickMenu();
    //            }
    //            bluseraccess = null;
    //            dset = null;
    //            ddlPage1.Focus();
    //        }
    //        else
    //        {
    //            FillPages();
    //            ResetQuickMenu();
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "toastr.warning('" + ex.Message + "')", true); 
    //    }


    //}

    protected void ddlUser_SelectedIndexChanged1(object sender, EventArgs e)
    {
        try
        {
           
            ResetQuickMenu();
            if (ddlUser.SelectedIndex > 0)
            {
                DataSet dset = new DataSet();
                DataTable dtbl = new DataTable();
                DataTable dtblMenu = new DataTable();
                BlUserAccess bluseraccess = new BlUserAccess();
                bluseraccess.FK_Client = Convert.ToInt64(hfClient.Value);
                bluseraccess.FK_Users = Convert.ToInt64(ddlUser.SelectedValue.ToString());
                dset = bluseraccess.SelectUserAccess();
                dtbl = dset.Tables[0];
                dtblMenu = dset.Tables[0];
                if (dtbl.Rows.Count > 0)
                {
                   // dlUserAccess.DataSource = dtbl;
                   // dlUserAccess.DataBind();
                }
                else
                {
                    FillPages();
                }
                if (dtblMenu.Rows.Count > 0)
                {
                    ddlPage1.SelectedValue = dtblMenu.Rows[0]["FK_pages1"].ToString();
                    ddlPage2.SelectedValue = dtblMenu.Rows[0]["FK_pages2"].ToString();
                    ddlPage3.SelectedValue = dtblMenu.Rows[0]["FK_pages3"].ToString();
                    ddlPage4.SelectedValue = dtblMenu.Rows[0]["FK_pages4"].ToString();
                    ddlPage5.SelectedValue = dtblMenu.Rows[0]["FK_pages5"].ToString();
                    ddlPage6.SelectedValue = dtblMenu.Rows[0]["FK_pages6"].ToString();
                }
                else
                {
                    ResetQuickMenu();
                }
                bluseraccess = null;
                dset = null;
                ddlPage1.Focus();
            }
            else
            {
                FillPages();
                ResetQuickMenu();
            }
            ddlPage1.Focus();
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "toastr.warning('" + ex.Message + "')", true);
        }
    }
}