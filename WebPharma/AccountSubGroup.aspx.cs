/*----------------------------------------------------------------------------------------------------------------------------------------------------------
Purpose    : Insert/Update/Delete Account Sub Group 
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
using WebPharma.Business;

public partial class AccountSubGroup : System.Web.UI.Page
{
    #region Function
    public void Delete()
    {
        try
        {
            divPopupAccountGroupsub.Visible = true;
            Populate Populate = new Populate();
            CommonPopup1.CallingFunction = "PopupOnAccountGroupsub";
            ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "PopupOnAccountGroupsub();", true);
            mpCommon.Show();
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
            BlAccountSubGroup BlAccountGroupsub = new BlAccountSubGroup();
            BlAccountGroupsub.UserCode = hfUser.Value;
            BlAccountGroupsub.MasterId = Convert.ToInt64(hfMasterID.Value);
            BlAccountGroupsub.FK_Reason = Convert.ToInt64(Reason);
            //  BlAccountGroup.FK_Client = Convert.ToInt64(Reason);
            if (hfIsDelete.Value.ToLower() == "true")
            {
                status = BlAccountGroupsub.DeleteData();
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
                txtName.Focus();
            }
            BlAccountGroupsub = null;
        }
    }
    protected void BindData()
    {
        new Populate().FillDropdownList(ddlAccountGroup, "AccountGroup", "GroupName", "ID_AccountGroup", "ID_AccountGroup", " Cancelled=0 And   FK_Client = " + hfClient.Value, "");
        new Populate().FillDropdownList(ddlAccountGroupSearch, "AccountGroup", "GroupName", "ID_AccountGroup", "ID_AccountGroup", " Cancelled=0 And  FK_Client = " + hfClient.Value, "");
        
    }
    public void GridFill(int currentPage)
    {
        BlAccountSubGroup blAccsub = new BlAccountSubGroup();
        DataTable dtbl = new DataTable();
       
        blAccsub.Name = txtSearchName.Text;
        blAccsub.ShortName = txtSearchShortName.Text;
        blAccsub.FK_Accountgroup = Convert.ToInt64(ddlAccountGroupSearch.SelectedValue);
        currentPage = currentPage == 0 ? 1 : currentPage;
        blAccsub.PageIndex = Convert.ToInt16(currentPage);
        blAccsub.UserCode = hfUser.Value;
        blAccsub.FK_Client = Convert.ToInt64(hfClient.Value);

        dtbl = blAccsub.SelectAllData();
        gvAccountSubGroup.DataSource = dtbl;
        gvAccountSubGroup.DataBind();
       
        blAccsub = null;

    }
    
    protected void SaveData()
    {
        long status = 0;
        BlAccountSubGroup blAcc = new BlAccountSubGroup();
        blAcc.FK_Accountgroup = Convert.ToInt64(ddlAccountGroup.SelectedValue);
        blAcc.Name = txtName.Text.Trim();
        blAcc.ShortName = txtShortName.Text.Trim();
        blAcc.SortOrder = Convert.ToInt32(txtSortOrder.Text == "" ? "0" : txtSortOrder.Text);


        blAcc.UserCode = hfuserName.Value;
        blAcc.FK_Client = Convert.ToInt64(hfClient.Value);
      
        if (hdfAccSubGroup.Value == string.Empty)
        {
            if (hfIsAdd.Value.ToLower() == "true")
            {
                status = blAcc.InsertData();
                if (status > 0)
                {
                    //blMessage.Items.Add("Data inserted successfully");
                    GridFill(1);
                    //blMessage.Items.Add("Data inserted successfully");

                    ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "ShowAddDiv();", true);
                    ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "toastr.success('Data inserted successfully');ClearAdd();", true);


                }

                else
                {
                    ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "ShowAddDiv();", true);
                    ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "toastr.error('Cannot insert data, ShortName/Name already exists')", true);
                    //blMessage.Items.Add("Cannot insert data, ShortName/Name/Sort order already exists");

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
            if (hfIsUpdate.Value.ToLower() == "true")
            {
                blAcc.MasterId = Convert.ToInt64(hdfAccSubGroup.Value);
                status = blAcc.UpdateData();
                if (status > 0)
                {
                    //blMessage.Items.Add("Data updated successfully");
                    GridFill(1);
                    ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "ShowAddDiv();", true);
                    ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "toastr.success('Data updated successfully');ClearAdd();", true);


                }

                else
                {
                    ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "ShowAddDiv();", true);
                    //blMessage.Items.Add("Cannot update data, ShortName/Name/Account Head Code already exists");

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
        blAcc = null;

      

    }

    protected void CheckUserAccess()
    {
        BlUserAccess bluseraccess = new BlUserAccess();
        Populate populate = new Populate();

        bluseraccess.FK_Users = Convert.ToInt64(hfUser.Value);
        bluseraccess.FK_Pages = 19;
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
    protected void Page_Load(object sender, EventArgs e)
    {

        try
        {
            if (!IsPostBack)
            {
                BlFunction blfunctions = new BlFunction();
                Int16 type = Convert.ToInt16(blfunctions.GetScalarValue("Settings", "isnull(( select Value", "Name='SEC06'),0)", ""));
                ViewState["type"] = type.ToString();
                if (type > 0)
                {


                    ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "toastr.warning('This Facilities Only Available In SCORE');", true);
                    btnSubmit.Enabled = false;
                    btnSearch.Enabled = false;
                    btnClear.Enabled = false; imgbtnView.Enabled = false;
                }

                else
                {
                    btnSubmit.Enabled = true;
                    btnSearch.Enabled = true;
                    btnClear.Enabled = true; imgbtnView.Enabled = true;
                }
                Page.Header.DataBind();

                hfUser.Value = Request.QueryString["uid"];
                hfClient.Value = Request.QueryString["clnt"];
                hfuserName.Value = Session["Username"].ToString();
                ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "ClearAdd();", true);
                divAdd.Style.Add("display", "block");
               BindData();
               CheckUserAccess();
               txtShortName.Focus();
              // GridFill(1);
            }
        }
        catch (Exception ex)
        {
             //blMessage.Items.Add(ex.Message);
        }
    }

    protected void Page_Changed(object sender, EventArgs e)
    {
        try
        {
           // blMessage.Items.Clear();
            int pageIndex = int.Parse((sender as LinkButton).CommandArgument);
            GridFill(pageIndex);
        }
        catch (Exception ex)
        {
             //blMessage.Items.Add(ex.Message);
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
             //blMessage.Items.Add(ex.Message);
        }

    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
           // blMessage.Items.Clear();
            SaveData();
        }
        catch (Exception ex)
        {
             //blMessage.Items.Add(ex.Message);
        }
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        try
        {
            //blMessage.Items.Clear();
            GridFill(1);
        }
        catch (Exception ex)
        {
             //blMessage.Items.Add(ex.Message);
        }
    }
    protected void gvAccountSubGroup_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        try
        {
           // blMessage.Items.Clear();
            ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "ShowAddDiv();", true);
            BlAccountSubGroup blAcc = new BlAccountSubGroup();
            blAcc.MasterId = Convert.ToInt64(gvAccountSubGroup.DataKeys[e.NewSelectedIndex]["ID_AccountSubGroup"].ToString());
            hdfAccSubGroup.Value = blAcc.MasterId.ToString();

            blAcc.UserCode = hfUser.Value;
            blAcc.FK_Client = Convert.ToInt64(hfClient.Value);

            blAcc.SelectData();
            txtShortName.Text = blAcc.ShortName;
            txtName.Text = blAcc.Name;
            try
            {
                ddlAccountGroup.SelectedValue = blAcc.FK_Accountgroup.ToString();
            }
            catch
            {
                ddlAccountGroup.SelectedValue = "0";
            }
            txtSortOrder.Text = blAcc.SortOrder.ToString();
           
          


        }
        catch (Exception ex)
        {
             //blMessage.Items.Add(ex.Message);
        }
    }
    protected void gvAccountSubGroup_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
           // blMessage.Items.Clear();
            string confirmValue = Request.Form["confirm_value"];
            if (confirmValue.Length > 3)
                confirmValue = confirmValue.Substring(confirmValue.Length - 3, 3);
            if (confirmValue == "Yes")
            {
                long status = 0;
                BlAccountSubGroup blAcc = new BlAccountSubGroup();

                blAcc.UserCode = hfUser.Value;

                blAcc.FK_Client = Convert.ToInt64(hfClient.Value);

                blAcc.MasterId = Convert.ToInt64(gvAccountSubGroup.DataKeys[e.RowIndex]["ID_AccountSubGroup"].ToString());
                hfMasterID.Value = (gvAccountSubGroup.DataKeys[e.RowIndex]["ID_AccountSubGroup"].ToString());
                blAcc.FK_Client = Convert.ToInt64(hfClient.Value);
                blAcc = null;
            }
            else
            {
                mpCommon.Hide();
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "toastr.warning('" + ex.Message + "')", true);
        }
    }
    #endregion
    protected void gvAccountSubGroup_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvAccountSubGroup.PageIndex = e.NewPageIndex;
        GridFill(e.NewPageIndex); 
    }
    protected void ImageButton2_Click(object sender, ImageClickEventArgs e)
    {
        Delete();
    }
}