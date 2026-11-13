/*----------------------------------------------------------------------------------------------------------------------------------------------------------
Purpose    : Insert/Update/Delete Account Head 
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


public partial class AccountHead : System.Web.UI.Page
{
    #region FunCtion
    public void Delete()
    {
        try
        {
            divPopupOnAccountHead.Visible = true;
            Populate Populate = new Populate();
            CommonPopup1.CallingFunction = "PopupOnAccountHead";
            ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "PopupOnAccountHead();", true);
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
            BlAccountHead BlAccountHead = new BlAccountHead();
            BlAccountHead.UserCode = Session["Username"].ToString();// hfUser.Value;
            BlAccountHead.MasterId = Convert.ToInt64(hfMasterID.Value);
            BlAccountHead.FK_Reason = Convert.ToInt64(Reason);
            //  BlAccountGroup.FK_Client = Convert.ToInt64(Reason);
            if (hfIsDelete.Value.ToLower() == "true")
            {
                status = BlAccountHead.DeleteData();
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
            BlAccountHead = null;
        }
    }
    public void BindData()
    {
        new Populate().FillDropdownList(ddlAccountGroup, "AccountGroup", "GroupName", "ID_AccountGroup", "ID_AccountGroup", "  Cancelled=0 And  FK_Client = "+hfClient.Value, "");
        new Populate().FillDropdownList(ddlAccountGroupSearch, "AccountGroup", "GroupName", "ID_AccountGroup", "ID_AccountGroup", "  Cancelled=0 And   FK_Client = " + hfClient.Value, "");
      
        BinSubAccount();
        BindSearch();
      


    }
    public void BinSubAccount()
    {

        new Populate().FillDropdownList(ddlAccountSubGroup, "AccountSubGroup", "SubGroupName", "ID_AccountSubGroup", "ID_AccountSubGroup", "Cancelled=0 And FK_AccountGroup = " + ddlAccountGroup.SelectedValue + "  and FK_Client = " + hfClient.Value, "");
     
    }
    public void BindSearch()
    {

        new Populate().FillDropdownList(ddlAccSubGroupSearch, "AccountSubGroup", "SubGroupName", "ID_AccountSubGroup", "ID_AccountSubGroup", " Cancelled=0 AND FK_AccountGroup = " + ddlAccountGroupSearch.SelectedValue + " and FK_Client = " + hfClient.Value, "");
      
    }
    public void GridFill(int currentPage)
    {
        BlAccountHead BlAcc = new BlAccountHead();
        DataTable dtbl = new DataTable();
        BlAcc.Name = txtSearchName.Text.Trim();
        BlAcc.ShortName = txtSearchShortName.Text.Trim();
        BlAcc.FK_AccountGroup = Convert.ToInt64(ddlAccountGroupSearch.SelectedValue);
        BlAcc.FK_AccountSubGroup = Convert.ToInt64(ddlAccSubGroupSearch.SelectedValue);
        BlAcc.AccountType = Convert.ToByte(ddlAccountTypeSearch.SelectedValue);
        currentPage = currentPage == 0 ? 1 : currentPage;
       
        BlAcc.PageIndex = Convert.ToInt16(currentPage);
        BlAcc.UserCode = hfUser.Value;
        BlAcc.FK_Client = Convert.ToInt64(hfClient.Value);

        dtbl = BlAcc.SelectAllData();
        gvAccountHead.DataSource = dtbl;
        gvAccountHead.DataBind();
       
        BlAcc = null;
    }



    protected void SaveData()
    {
        long status = 0;
        BlAccountHead blAcc = new BlAccountHead();
        blAcc.FK_AccountGroup = Convert.ToInt64(ddlAccountGroup.SelectedValue);
        blAcc.FK_AccountSubGroup = Convert.ToInt64(ddlAccountSubGroup.SelectedValue);
        blAcc.AhCode = txtAccountHeadCode.Text.Trim();
        blAcc.Name = txtName.Text.Trim();
        blAcc.ShortName = txtShortName.Text.Trim();
        blAcc.AccountType = Convert.ToByte(ddlAccountType.SelectedValue);
        blAcc.SortOrder = int.Parse(txtSortOrder.Text == "" ? "0" : txtSortOrder.Text);
        blAcc.UserCode = hfUser.Value; 
        blAcc.CashinHand = cbxCashInHand.Checked;

        blAcc.FK_Client = Convert.ToInt64(hfClient.Value);

        if (hdfAccountHead.Value == string.Empty)
        {
            if (hfIsAdd.Value.ToLower() == "true")
            {
                status = blAcc.InsertData();

                if (status > 0)
                {
                    GridFill(1);
                    //blMessage.Items.Add("Data inserted successfully");

                    ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "ShowAddDiv();", true);
                    //ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "toastr.success('Data inserted successfully');ClearAdd();", true);
                    Populate.InsertMsg(this);
                    nextahcode();
                    // GridFill(1);
                }

                else
                {
                    ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "ShowAddDiv();", true);
                    //  ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "toastr.error('Cannot insert data, ShortName/Name already exists')", true);
                    Populate.ErrorMessageBox(this, "Cannot insert data, ShortName/Name/Cash in Hand already exists");
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
            blAcc.MasterId = Convert.ToInt64(hdfAccountHead.Value);
            if (hfIsUpdate.Value.ToLower() == "true")
            {
                status = blAcc.UpdateData();
                if (status > 0)
                {
                    //blMessage.Items.Add("Data updated successfully");

                    ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "ShowAddDiv();", true);
                    GridFill(1);
                    //ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "toastr.success('Data updated successfully');ClearAdd();", true);
                    Populate.UpdateMsg(this);
                }

                else
                {
                    ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "ShowAddDiv();", true);
                    //blMessage.Items.Add("Cannot update data, ShortName/Name/Account Head Code already exists");

                    //  ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "toastr.error('Cannot update data, ShortName/Name already exists')", true);
                    Populate.ErrorMessageBox(this, "Cannot update data, ShortName/Name already exists");
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
        bluseraccess.FK_Pages = 20;
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
    #region  Events
    protected void Page_Load(object sender, EventArgs e)
    {
       // blMessage.Items.Clear();
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
                ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "ClearAdd();", true);
                divAdd.Style.Add("display", "block");
         
                hfUser.Value = Request.QueryString["uid"];
                hfClient.Value = Request.QueryString["clnt"];
                BindData();
                CheckUserAccess();
                nextahcode();
                txtShortName.Focus();
                if (Convert.ToBoolean(hfIsAdd.Value) == false)
                {
                    ((Label)CommonPopupPrivilege.FindControl("popuplabel")).Text = "User does not have permission to Add";
                    mpPrivilege.Show();
                }
                if (Convert.ToBoolean(hfIsUpdate.Value) == false)
                {
                    gvAccountHead.Columns[8].Visible = false;
                }
                if (Convert.ToBoolean(hfIsDelete.Value) == false)
                {
                    gvAccountHead.Columns[9].Visible = false;
                }
             
            }
        }
        catch (Exception ex)
        {
            Populate.WarningMsg(this, ex.Message);
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
            Populate.WarningMsg(this, ex.Message);
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
           // BindSearch();
            GridFill(1);

        }
        catch (Exception ex)
        {
            Populate.WarningMsg(this, ex.Message);
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
            Populate.WarningMsg(this, ex.Message);
        }
    }
   
    protected void btnAccountSubFill_Click(object sender, EventArgs e)
    {
        BinSubAccount();
        divAdd.Style.Add("display", "block");
        divView.Style.Add("display", "none");
        ddlAccountSubGroup.Focus();

    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        try
        {
           // blMessage.Items.Clear();
            GridFill(1);
            txtSearchShortName.Focus();
        }
        catch (Exception ex)
        {
            Populate.WarningMsg(this, ex.Message);
        }
    }
   
    protected void btnDlFill_Click(object sender, EventArgs e)
    {
        BindSearch();
        if (ddlAccSubGroupSearch.SelectedIndex > 0)
            ddlAccountTypeSearch.Focus();
        else
            ddlAccSubGroupSearch.Focus();
        divAdd.Style.Add("display", "none");
        divView.Style.Add("display", "block");
    }
    protected void gvAccountHead_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        try
        {
           // blMessage.Items.Clear();
            ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "ShowAddDiv();", true);
            BlAccountHead blAccHead = new BlAccountHead();
            blAccHead.MasterId = Convert.ToInt64(gvAccountHead.DataKeys[e.NewSelectedIndex]["ID_AccountHead"].ToString());
            hdfAccountHead.Value = blAccHead.MasterId.ToString();

            blAccHead.UserCode = hfUser.Value;
            blAccHead.FK_Client = Convert.ToInt64(hfClient.Value);

            blAccHead.SelectData();
            try
            {
                ddlAccountGroup.SelectedValue = blAccHead.FK_AccountGroup.ToString();
            }
            catch
            {
                ddlAccountGroup.SelectedValue = "0";
            }
            BinSubAccount();
            try
            {
                ddlAccountSubGroup.SelectedValue = blAccHead.FK_AccountSubGroup.ToString();
            }
            catch
            {
                ddlAccountSubGroup.SelectedValue = "0";
            }
            txtAccountHeadCode.Text = blAccHead.AhCode;
            txtName.Text = blAccHead.Name;
            txtShortName.Text = blAccHead.ShortName;
            ddlAccountType.SelectedValue = blAccHead.AccountType.ToString();
           
            txtSortOrder.Text = blAccHead.SortOrder.ToString();
            cbxCashInHand.Checked = blAccHead.CashinHand;
            blAccHead = null;


        }
        catch (Exception ex)
        {
            Populate.WarningMsg(this, ex.Message);
        }
    }
    protected void gvAccountHead_RowDeleting(object sender, GridViewDeleteEventArgs e)
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
                BlAccountHead blAcc = new BlAccountHead();
                blAcc.MasterId = Convert.ToInt64(gvAccountHead.DataKeys[e.RowIndex]["ID_AccountHead"].ToString());
                hfMasterID.Value = (gvAccountHead.DataKeys[e.RowIndex]["ID_AccountHead"].ToString());
                blAcc.UserCode = hfUser.Value;
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
    protected void nextahcode()
    {

        BlPopulate blcommon = new BlPopulate();
        txtAccountHeadCode.Text = blcommon.GetNextNumber("AccountHead", "AhCode", "Cancelled=0", "").ToString();
        txtAccountHeadCode.Enabled = false;
        blcommon = null;
    }
    #endregion
    protected void btnClear_Click(object sender, EventArgs e)
    {
        nextahcode();
        divAdd.Style.Add("display", "block");
        divView.Style.Add("display", "none");
    }
    protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
    {
        nextahcode();
        divAdd.Style.Add("display", "block");
        divView.Style.Add("display", "none");
        if (Convert.ToBoolean(hfIsAdd.Value) == false)
        {
            ((Label)CommonPopupPrivilege.FindControl("popuplabel")).Text = "User does not have permission to Add";
            mpPrivilege.Show();
        }
    }
    protected void ImageButton2_Click(object sender, ImageClickEventArgs e)
    {
        Delete();
    }
    protected void gvAccountHead_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvAccountHead.PageIndex = e.NewPageIndex;
        GridFill(e.NewPageIndex);
    }
}