/*----------------------------------------------------------------------------------------------------------------------------------------------------------
Purpose    : Insert/Update/Delete Customer Group 
Author     : Bibin tc
Copyright  : Perfect Software Solutions (Clt) Pvt Ltd
Created on :03/05/2018
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

public partial class CustomerGroup : System.Web.UI.Page
{
    #region Functions
    public void Delete()
    {
        try
        {
            divPopupReason.Visible = true;
            Populate Populate = new Populate();
            CommonPopup1.CallingFunction = "PopupOnReason";
            ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "PopupOnReason();", true);
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
            BlCustomerGroup BlCustomer = new BlCustomerGroup();
            BlCustomer.UserCode = hfuserName.Value;
            BlCustomer.MasterId = Convert.ToInt64(hfMasterID.Value);
            BlCustomer.FK_Reason = Convert.ToInt64(Reason);
            if (hfIsDelete.Value.ToLower() == "true")
            {
                status = BlCustomer.DeleteData();
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
                txtSearchName.Focus();
            }
            BlCustomer = null;
        }
    }
    protected void GridFill(int currentPage)
    {
        BlCustomerGroup blcustomergroup = new BlCustomerGroup();
        DataTable dtbl = new DataTable();
        blcustomergroup.ShortName = txtSearchShortName.Text.Trim();
        blcustomergroup.Name = txtSearchName.Text.Trim();
        blcustomergroup.FK_Client = Convert.ToInt64(hfClient.Value);
        blcustomergroup.UserCode = hfUser.Value;
        blcustomergroup.PageIndex = Convert.ToInt16(currentPage);
        dtbl = blcustomergroup.SelectAllData();
        gvCustomerGroup.DataSource = dtbl;
        gvCustomerGroup.DataBind();
        blcustomergroup = null;
    }

    protected void CheckUserAccess()
    {
        BlUserAccess bluseraccess = new BlUserAccess();
        Populate populate = new Populate();

        bluseraccess.FK_Users = Convert.ToInt64(hfUser.Value);
        bluseraccess.FK_Pages = 3;
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
        long status = 0;
        BlCustomerGroup blcustomergroup = new BlCustomerGroup();
        blcustomergroup.ShortName = txtShortName.Text;
        blcustomergroup.Name = txtName.Text;
        blcustomergroup.CreditLimit = Convert.ToDecimal(string.IsNullOrEmpty((txtCreditlimit.Text)) ? "0" : txtCreditlimit.Text);
        blcustomergroup.CreditDays = Convert.ToInt32(string.IsNullOrEmpty((txtCreditDays.Text)) ? "0" : txtCreditDays.Text);    
        blcustomergroup.Active = Convert.ToBoolean(chkActive.Checked);
        blcustomergroup.EnteredOn = DateTime.Now;
        blcustomergroup.FK_Client = Convert.ToInt64(hfClient.Value);
        blcustomergroup.UserCode = hfuserName.Value;
        if (hfCustomerGroupId.Value == "0")
        {
            if (hfIsAdd.Value.ToLower() == "true")
            {
                status = blcustomergroup.InsertData();
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
            if (hfIsUpdate.Value.ToLower() == "true")
            {

                blcustomergroup.MasterId = Convert.ToInt64(hfCustomerGroupId.Value);
                status = blcustomergroup.UpdateData();
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
        blcustomergroup = null;
    }
    #endregion

    #region Events
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                Page.Header.DataBind();
                //blMessage.Items.Clear();
                ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "ClearAdd();", true);
                divAdd.Style.Add("display", "block");
                hfUser.Value = Request.QueryString["uid"];
                hfClient.Value = Request.QueryString["clnt"];
                hfuserName.Value = Session["Username"].ToString();
                CheckUserAccess(); txtShortName.Focus();
                if (Convert.ToBoolean(hfIsAdd.Value) == false)
                {
                    ((Label)CommonPopupPrivilege.FindControl("popuplabel")).Text = "User does not have permission to Add";
                    mpPrivilege.Show();
                }
                if (Convert.ToBoolean(hfIsUpdate.Value) == false)
                {
                    gvCustomerGroup.Columns[4].Visible = false;
                }
                if (Convert.ToBoolean(hfIsDelete.Value) == false)
                {
                    gvCustomerGroup.Columns[5].Visible = false;
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
            //blMessage.Items.Clear();
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
    protected void gvCustomerGroup_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        try
        {
            //blMessage.Items.Clear();
            ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "ShowAddDiv();", true);
            BlCustomerGroup blcustomergroup = new BlCustomerGroup();
            blcustomergroup.MasterId = Convert.ToInt64(gvCustomerGroup.DataKeys[e.NewSelectedIndex]["ID_CustomerGroup"].ToString());
            hfCustomerGroupId.Value = blcustomergroup.MasterId.ToString();
            blcustomergroup.FK_Client = Convert.ToInt64(hfClient.Value);
            blcustomergroup.UserCode = hfUser.Value;
            blcustomergroup.SelectData();
            txtShortName.Text = blcustomergroup.ShortName.ToString();
            txtName.Text = blcustomergroup.Name.ToString();
            txtCreditlimit.Text = blcustomergroup.CreditLimit.ToString();
            txtCreditDays.Text = blcustomergroup.CreditDays.ToString();
            chkActive.Checked = blcustomergroup.Active;
            blcustomergroup = null;
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "toastr.warning('" + ex.Message + "')", true); 
        }
    }
    protected void gvCustomerGroup_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            //blMessage.Items.Clear();
            string confirmValue = Request.Form["confirm_value"];
            if (confirmValue.Length > 3)
                confirmValue = confirmValue.Substring(confirmValue.Length - 3, 3);
            if (confirmValue == "Yes")
            {
                long status = 0;
                BlCustomerGroup blcustomergroup = new BlCustomerGroup();
                blcustomergroup.MasterId = Convert.ToInt64(gvCustomerGroup.DataKeys[e.RowIndex]["ID_CustomerGroup"].ToString());
                hfMasterID.Value = (gvCustomerGroup.DataKeys[e.RowIndex]["ID_CustomerGroup"].ToString());
                blcustomergroup.FK_Client = Convert.ToInt64(hfClient.Value);
                blcustomergroup.UserCode = hfUser.Value;
                blcustomergroup = null;
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
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            //blMessage.Items.Clear();
            SaveData();
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "toastr.warning('" + ex.Message + "')", true); 
        }
    }
         
    protected void gvCustomerGroup_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvCustomerGroup.PageIndex = e.NewPageIndex;
        GridFill(e.NewPageIndex);
    }
  
    protected void ImageButton2_Click(object sender, ImageClickEventArgs e)
    {
        Delete();
    }
    #endregion
    protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
    {
        if (Convert.ToBoolean(hfIsAdd.Value) == false)
        {
            ((Label)CommonPopupPrivilege.FindControl("popuplabel")).Text = "User does not have permission to Add";
            mpPrivilege.Show();
        }
        ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "ShowAddDiv();", true);
    }
}