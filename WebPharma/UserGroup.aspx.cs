/*----------------------------------------------------------------------------------------------------------------------------------------------------------
Purpose    : Insert/Update/Delete User Group 
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
using WebPharma.Business.Masters;
using System.Data;

public partial class UserGroup : System.Web.UI.Page
{
    #region Functions
    protected void GridFill(int currentPage)
    {
        BlUserGroup blusergroup = new BlUserGroup();
        DataTable dtbl = new DataTable();
        blusergroup.Shortname = txtSearchShortName.Text.Trim();
        blusergroup.GroupName = txtSearchName.Text.Trim();
        blusergroup.FK_Client = Convert.ToInt64(hfClient.Value);
        blusergroup.UserCode = hfUser.Value;
        blusergroup.PageIndex = Convert.ToInt16(currentPage);
        dtbl = blusergroup.SelectAllData();
        gvUserGroup.DataSource = dtbl;
        gvUserGroup.DataBind();
        //if (dtbl.Rows.Count > 0)
        //{
        //    int recordCount = Convert.ToInt32(dtbl.Rows[0]["RecordCount"].ToString());
        //    this.PopulatePager(recordCount, currentPage);
        //}
        blusergroup = null;
    }
    protected void CheckUserAccess()
    {
        BlUserAccess bluseraccess = new BlUserAccess();
        Populate populate = new Populate();

        bluseraccess.FK_Users = Convert.ToInt64(hfUser.Value);
        bluseraccess.FK_Pages = 12;
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
    private void PopulatePager(int recordCount, int currentPage)
    {
        double dblPageCount = (double)((decimal)recordCount / 10);
        int pageCount = (int)Math.Ceiling(dblPageCount);
        List<ListItem> pages = new List<ListItem>();
        int i;
        if (currentPage <= 10)
        {
            i = 1;
        }
        else
        {
            i = currentPage;
        }
        if (pageCount > 0)
        {
            pages.Add(new ListItem("First", "1", currentPage > 1));
            pages.Add(new ListItem("<-", (i - 10).ToString(), i > 1));
            while (i <= pageCount)
            {
                pages.Add(new ListItem(i.ToString(), i.ToString(), i != currentPage));
                if (i % 10 == 0)
                {
                    break;
                }
                i++;
            }
            pages.Add(new ListItem("->", (i + 1).ToString(), pageCount > 10));
            pages.Add(new ListItem("Last", pageCount.ToString(), currentPage < pageCount));
        }
        rptPager.DataSource = pages;
        rptPager.DataBind();
    }
    protected void SaveData()
    {
        long status = 0;
        BlUserGroup blusergroup = new BlUserGroup();
        blusergroup.Shortname = txtShortName.Text;
        blusergroup.GroupName = txtName.Text;
        blusergroup.Administrator = Convert.ToBoolean(chkAdministrator.Checked);
        blusergroup.UgAdd = Convert.ToBoolean(chkAdd.Checked);
        blusergroup.UgModify = Convert.ToBoolean(chkModify.Checked);
        blusergroup.UgDelete = Convert.ToBoolean(chkDelete.Checked);
        blusergroup.UgView = Convert.ToBoolean(chkView.Checked);
        blusergroup.Active = Convert.ToBoolean(chkActive.Checked);
        blusergroup.UgPurchase = Convert.ToBoolean(chkPurchase.Checked);
        blusergroup.UgPurchaseReturn = Convert.ToBoolean(chkPurchaseReturn.Checked);
        blusergroup.UgSales = Convert.ToBoolean(chkSales.Checked);
        blusergroup.UgSalesReturn = Convert.ToBoolean(chkSalesReturn.Checked);
        blusergroup.UgApprove = Convert.ToBoolean(chkApprove.Checked);
        blusergroup.EnteredOn = DateTime.Now;
        blusergroup.FK_Client = Convert.ToInt64(hfClient.Value);
        blusergroup.UserCode = hfUsername.Value;
        if (hfUserGroupId.Value == "0")
        {
            if (hfIsAdd.Value.ToLower() == "true")
            {

                status = blusergroup.InsertData();
                if (status > 0)
                {
                    GridFill(1);
                    ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "ShowAddDiv();", true);
                    ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "toastr.success('Data inserted successfully');ClearAdd();", true);
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "ShowAddDiv();", true);
                    ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "toastr.error('Cannot insert data, ShortName/Name already exists')", true);
                    txtShortName.Focus();
                }
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "toastr.warning('User does not have permission to add')", true);
                txtShortName.Focus();

            }
        }
        else
        {
            blusergroup.MasterId = Convert.ToInt64(hfUserGroupId.Value);
            if (hfIsUpdate.Value.ToLower() == "true")
            {
                status = blusergroup.UpdateData();
                if (status > 0)
                {
                    GridFill(1);
                    ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "ShowAddDiv();", true);
                    ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "toastr.success('Data updated successfully');ClearAdd();", true);
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "ShowAddDiv();", true);
                    ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "toastr.error('Cannot update data, ShortName/Name already exists')", true);
                    txtShortName.Focus();
                }
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "toastr.warning('User does not have permission to Update')", true);
                txtShortName.Focus();
            }
        }
        blusergroup = null;
    }
    #endregion

    #region Events
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
          
            if (!IsPostBack)
            {
                if (Session["Username"] != null && Convert.ToString(Session["Username"]) != "")
                {

                    hfUsername.Value = Session["Username"].ToString();

                    Page.Header.DataBind();
                    //blMessage.Items.Clear();
                    ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "ClearAdd();", true);
                    divAdd.Style.Add("display", "block");
                    hfUser.Value = Request.QueryString["uid"];
                    hfClient.Value = Request.QueryString["clnt"];
                    CheckUserAccess();
                    txtShortName.Focus();
                }
                else
                {
                    Response.Redirect("~/Default.aspx", false);
                }

              
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "toastr.warning('" + ex.Message + "')", true); 
        }
    }
    protected void Page_Changed(object sender, EventArgs e)
    {
        try
        {
            //blMessage.Items.Clear();
            int pageIndex = int.Parse((sender as LinkButton).CommandArgument);
            GridFill(pageIndex);
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "toastr.warning('" + ex.Message + "')", true); 
        }
    }
    protected void imgbtnView_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
           // blMessage.Items.Clear();
            divAdd.Style.Add("display", "none");
            divView.Style.Add("display", "block");
            ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "ClearAdd();", true);
            GridFill(1);
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "toastr.warning('" + ex.Message + "')", true); 
        }
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        try
        {
            //blMessage.Items.Clear();
            GridFill(1);
            txtSearchShortName.Focus();
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "toastr.warning('" + ex.Message + "')", true); 
        }
    }
    protected void gvUserGroup_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        try
        {
            //blMessage.Items.Clear();
            ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "ShowAddDiv();", true);
            BlUserGroup blusergroup = new BlUserGroup();
            blusergroup.MasterId = Convert.ToInt64(gvUserGroup.DataKeys[e.NewSelectedIndex]["ID_UserGroup"].ToString());
            hfUserGroupId.Value = blusergroup.MasterId.ToString();
            blusergroup.FK_Client = Convert.ToInt64(hfClient.Value);
            blusergroup.UserCode = hfUser.Value;
            blusergroup.SelectData();
            txtShortName.Text = blusergroup.Shortname.ToString();
            txtName.Text = blusergroup.GroupName.ToString();
            chkAdministrator.Checked = blusergroup.Administrator;
            chkAdd.Checked = blusergroup.UgAdd;
            chkModify.Checked = blusergroup.UgModify;
            chkDelete.Checked = blusergroup.UgDelete;
            chkView.Checked = blusergroup.UgView;
            chkActive.Checked = blusergroup.Active;
            chkApprove.Checked = blusergroup.UgApprove;
            chkPurchase.Checked = blusergroup.UgPurchase;
            chkPurchaseReturn.Checked = blusergroup.UgPurchaseReturn;
            chkSales.Checked = blusergroup.UgSales;
            chkSalesReturn.Checked = blusergroup.UgSalesReturn;
           

            blusergroup = null;
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "toastr.warning('" + ex.Message + "')", true); 
        }
    }
    protected void gvUserGroup_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
           // blMessage.Items.Clear();
            string confirmValue = Request.Form["confirm_value"];
            if (confirmValue.Length > 3)
                confirmValue = confirmValue.Substring(confirmValue.Length - 3, 3);
            if (confirmValue == "Yes")
            {

                hfMasterID.Value = gvUserGroup.DataKeys[e.RowIndex]["ID_UserGroup"].ToString();

            }
            else
            { mpDelete.Hide(); }
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
            ////blMessage.Items.Clear();
            SaveData();
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "toastr.warning('" + ex.Message + "')", true); 
        }
    }
    #endregion           
    protected void ImageBtnDelete_Click(object sender, ImageClickEventArgs e)
    {
        Delete();
    }

    public void Delete()
    {
        try
        {
            divPopupReason.Visible = true;
            Populate Populate = new Populate();
            CommonPopupDelete1.CallingFunction = "PopupOnReason";
            ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "PopupOnReason();", true);
            mpDelete.Show();
        }
        catch (Exception ex)
        {
        }
    }

    public void DeleteMethod(string Reason = "0")
    {
        string confirmValue = Request.Form["confirm_value"];
        if (confirmValue.Length > 3)
            confirmValue = confirmValue.Substring(confirmValue.Length - 3, 3);
        if (confirmValue == "Yes")
        {
            long status = 0;
            BlUserGroup blusergroup = new BlUserGroup();
            blusergroup.MasterId = Convert.ToInt64(hfMasterID.Value);
            blusergroup.FK_Client = Convert.ToInt64(hfClient.Value);
            blusergroup.UserCode = hfUsername.Value;
            blusergroup.FK_Reason = Convert.ToInt64(Reason);
            if (hfIsDelete.Value.ToLower() == "true")
            {
                status = blusergroup.DeleteData();
                if (status > 0)
                {
                    GridFill(1);
                    ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "toastr.success('Data deleted successfully')", true);
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "toastr.error('Cannot delete data, reference exists')", true);
                }
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "toastr.warning('User does not have permission to Delete')", true);
                txtShortName.Focus();
            }
            blusergroup = null;
        }



    }

    protected void gvUserGroup_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvUserGroup.PageIndex = e.NewPageIndex;
        GridFill(e.NewPageIndex);
    }
}