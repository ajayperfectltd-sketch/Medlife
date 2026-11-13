using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebPharma.Business.Masters;

public partial class DashBoardAccess : System.Web.UI.Page
{
    #region Functions
    public Int64 ID_DashBoardAccess
    {
        get { return Convert.ToInt64(hfMasterId.Value); }
        set { hfMasterId.Value = value.ToString(); }
    }

    protected void FillData()
    {
        BlDashBoard bluseraccess = new BlDashBoard();
        DataTable dtbl = new DataTable();
        bluseraccess.FK_Client = Convert.ToInt64(hfClient.Value.Split(',')[0]);
        bluseraccess.FK_UserGroup = Convert.ToInt64(ddlUserGroup.SelectedValue);
        dtbl = bluseraccess.SelectDashBoardAccesss();
        dlDashBoardAccess.DataSource = dtbl;
        dlDashBoardAccess.DataBind();
        
    }
    protected void BindData()
    {
        Populate populate = new Populate();
        populate.FillDropdownList(ddlUserGroup, "UserGroup", "GroupName", "ID_UserGroup", "ID_UserGroup", "Active = 1 and Cancelled=0 and FK_Client = " + hfClient.Value, "");
        //populate.FillDropdownList(ddlUser, "Users", "Username", "ID_Users", "", "Active = 1 and Cancelled=0  and FK_Client = " + hfClient.Value, "");
        populate = null;
    }
    protected void CheckUserAccess()
    {
        BlUserAccess bluseraccess = new BlUserAccess();
        Populate populate = new Populate();

       bluseraccess.FK_Users = Convert.ToInt64(hfUser.Value);
        bluseraccess.FK_Pages = 32;
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
        BlDashBoard bldashboard = new BlDashBoard();
        BlDashBoardAccess dashBoardAccess;
        bldashboard.MasterId = ID_DashBoardAccess;
        bldashboard.UserCode = hfUser.Value;
        bldashboard.FK_UserGroup = Convert.ToInt32(ddlUserGroup.SelectedValue);
        bldashboard.FK_Client = Convert.ToInt64(hfClient.Value);
        ArrayList arlist = new ArrayList();
        foreach (DataListItem dlItem in dlDashBoardAccess.Items)
        {
            if (dlItem != null)
            {
                bool isadd = Convert.ToBoolean(((CheckBox)dlItem.FindControl("chkAccess")).Checked);
               
                if (isadd)
                {
                    dashBoardAccess = new BlDashBoardAccess();
                    dashBoardAccess.FK_Dashboard = Convert.ToInt64(((HiddenField)dlItem.FindControl("hdfDashBoard")).Value.ToString());
                    arlist.Add(dashBoardAccess);
                    dashBoardAccess = null;
                }
                
            }
        }
        if (arlist.Count <= 0)
        {

            dashBoardAccess = new BlDashBoardAccess(); 
            arlist.Add(dashBoardAccess);
            dashBoardAccess = null;

        }
        if (hfIsUpdate.Value.ToLower() == "true")
        {


           
                bldashboard.XmlUserAccess = Common.WriteToXml(arlist);
                long status = bldashboard.UpdateData();
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
            ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "script", "toastr.warning('User does not have permission to Update')", true);
        }
    }
    private void ClearSearch()
    {
        ddlUserGroup.SelectedIndex = 0;
       
        FillData();
    }
    #endregion
    #region Events


    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            hfUser.Value = Request.QueryString["uid"];
            hfClient.Value = Request.QueryString["clnt"];
            CheckUserAccess();
            BindData();
            FillData();
            ddlUserGroup.Focus();
        }
    }
    protected void ddlUserGroup_SelectedIndexChanged(object sender, EventArgs e)
    {
        Populate populate = new Populate();
        string str = "";
        string[] s = hfClient.Value.Split(',');
        string c = s[0];

       
        FillData(); 
        
        populate = null;
    }

    protected void ddlUser_SelectedIndexChanged1(object sender, EventArgs e)
    {
        try
        {
           

            FillData();
            
            btnSubmit.Focus();
            btnSubmit.Focus();
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "toastr.warning('" + ex.Message + "')", true);
        }

    }
   

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
           // blMessage.Items.Clear();
            //if (hfUpdatePermission.Value.ToLower() == "true" && hfAddPermission.Value.ToLower() == "true")
            //{
            SaveData();
            ddlUserGroup.SelectedIndex = 0;
           
            FillData();
            //blMessage.Items.Add("Data saved successfully");

            //}
            //else
            //{
            //    blMessage.Items.Add("User does not have permission to update");
            //}
            ddlUserGroup.Focus();
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "toastr.warning('" + ex.Message + "')", true);
        }
    }

    protected void btnClear_Click(object sender, EventArgs e)
    {
        ClearSearch();
        ddlUserGroup.Focus();
    }
    
    #endregion
    
}