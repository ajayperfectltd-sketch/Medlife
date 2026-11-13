/*----------------------------------------------------------------------------------------------------------------------------------------------------------
Purpose    : Insert/Update/Delete Supplier  
Author     :Aiswarya
Copyright  : Perfect Software Solutions (Clt) Pvt Ltd
Created on : 10/05/2018
----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
WebPharma
On			By			OMID
----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

 * --------------------------------------------------------------------------------------------------------------------------------------------------------------------------------*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebPharma.Business.Masters;
using WebPharma.Business.Accounts;

using System.Data;

public partial class Supplier : System.Web.UI.Page
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
            BlSupplier blSupplier = new BlSupplier();
            //blReason.MasterId = Convert.ToInt64(gvReason.DataKeys[e.RowIndex]["ID_Reason"].ToString());
            blSupplier.UserCode = hfuserName.Value;
            blSupplier.MasterId = Convert.ToInt64(hfMasterID.Value);
            blSupplier.FK_Reason = Convert.ToInt64(Reason);
            if (hfIsDelete.Value.ToLower() == "true")
            {
                status = blSupplier.DeleteData();
                if (status > 0)
                {
                    GridFill(1);
                    ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "toastr.success('Data Deleted Successfully')", true);
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
            blSupplier = null;
        }
    }
    protected void BindData()
    {
        Populate populate = new Populate();
        populate.FillDropdownList(ddlCountry, "Country", "CountryName", "ID_Country", "ID_Country", " FK_Client= " + hfClient.Value+"and  Cancelled=0" , "");
        populate = null;
        BindDropdown();
        BindDropdownDistrict();
       
    }
    protected void BindDropdown()

    {
        new Populate().FillDropdownList(ddlState, "States", "StateName", "ID_States", "ID_States", "FK_Country = " + ddlCountry.SelectedValue + " and Cancelled=0 and FK_Client = " + hfClient.Value, "");
        
    }
    protected void BindDropdownDistrict()
    {
        new Populate().FillDropdownList(ddlDistrict, "District", "DistrictName", "ID_District", "ID_District", "FK_Country = " + ddlCountry.SelectedValue + " and FK_States = " + ddlState.SelectedValue + " and Cancelled=0 and FK_Client = " + hfClient.Value, "");
    }
 
    protected void SetControls()
    {
        
    }
    protected void CheckUserAccess()
    {
        BlUserAccess bluseraccess = new BlUserAccess();
        Populate populate = new Populate();

        bluseraccess.FK_Users = Convert.ToInt64(hfUser.Value);
        bluseraccess.FK_Pages = 5;
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
    protected void GridFill(int currentPage)
    {
        BlSupplier blSupplier = new BlSupplier();
        DataTable dtbl = new DataTable();
        blSupplier.SupplierName = txtSearchName.Text.Trim();
        blSupplier.FK_Client = Convert.ToInt64(hfClient.Value);
        blSupplier.UserCode = hfUser.Value;
        blSupplier.PageIndex = Convert.ToInt16(currentPage);
        dtbl = blSupplier.SelectAllData();
        gvSupplier.DataSource = dtbl;
        gvSupplier.DataBind();
        blSupplier = null;
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

        string errorMessage;
        if (!ValidateSupplierInput(out errorMessage))
        {
            ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script",
                "toastr.warning('" + errorMessage.Replace("'", "\\'") + "')", true);
            return;
        }
        long status = 0;
        BlSupplier blSupplier = new BlSupplier();
        blSupplier.SupplierName = txtSuppliername.Text;
        blSupplier.ContactPerson = txtContractPerson.Text;
        blSupplier.Area = txtArea.Text;
        blSupplier.Place = txtPlace.Text;
        blSupplier.Post = txtPost.Text;
        blSupplier.Pin = txtPin.Text;
        blSupplier.LandMark = txtLandmark.Text;
        blSupplier.Fax = txtFax.Text;
        blSupplier.FK_Country = Convert.ToInt64(ddlCountry.SelectedValue.ToString());
        blSupplier.FK_State = Convert.ToInt64(ddlState.SelectedValue.ToString());
        blSupplier.FK_District = Convert.ToInt64(ddlDistrict.SelectedValue.ToString());
        blSupplier.Phone = txtPhone.Text;
        blSupplier.Mobile = txtMobile.Text;
        blSupplier.Website = txtWebsite.Text;
        blSupplier.Email = txtEmail.Text;
        blSupplier.FK_Client = Convert.ToInt64(hfClient.Value);
        blSupplier.UserCode = hfuserName.Value;
        blSupplier.FK_Country = Convert.ToInt64(ddlCountry.SelectedValue.ToString());
        blSupplier.TinNo = txtTinNo.Text;
        blSupplier.CstNo = txtCstNo.Text;
        blSupplier.KgstNo = txtKgstNo.Text;
        blSupplier.Active = Convert.ToBoolean(chkActive.Checked);
        blSupplier.EnteredOn = Convert.ToDateTime(DateTime.Now);
        blSupplier.LandMark = txtLandmark.Text;
        blSupplier.GovernmentSupplier = Convert.ToBoolean(chkGovSupplier.Checked);
        if (hfSupplierId.Value == "0")
        {
            if (hfIsAdd.Value.ToLower() == "true")
            {
                status = blSupplier.InsertData();
                if (status > 0)
                {
                    ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "ShowAddDiv();", true);
                    ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "toastr.success('Data Inserted Successfully');ClearAdd();", true);
                    GridFill(1);
                    // liSupplierName.Attributes.Add("style", "display:none;");
                    lblTInMsg.Text = "";
                }
                else if (status == -2)
                {
                    ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "ShowAddDiv();", true);
                    ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "toastr.error('Cannot insert data, Name already exists')", true);
                    txtSuppliername.Focus();
                }
                else if (status == -3)
                {
                    ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "ShowAddDiv();", true);
                    ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "toastr.error('Cannot insert data, TIN Number already exists')", true);
                    txtSuppliername.Focus();
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "ShowAddDiv();", true);
                    ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "toastr.error('Cannot insert data)", true);
                    txtSuppliername.Focus();
                }
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "toastr.warning('User does not have permission to add')", true);
                //txtShortName.Focus();

            }
        }
        else
        {
            blSupplier.MasterId = Convert.ToInt64(hfSupplierId.Value);
            status = blSupplier.UpdateData();
            if (status > 0)
            {
                ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "ShowAddDiv();", true);
                ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "toastr.success('Data Updated Successfully');ClearAdd();", true);   
                 GridFill(1);
               // liSupplierName.Attributes.Add("style", "display:none;");
                lblTInMsg.Text = "";
            }
            else if (status == -2)
            {
                ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "ShowAddDiv();", true);
                ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "toastr.error('Cannot update data, Name already exists')", true);
                txtSuppliername.Focus();
            }
            else if (status == -3)
            {
                ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "ShowAddDiv();", true);
                ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "toastr.error('Cannot update data, TIN Number already exists')", true);
                txtSuppliername.Focus();
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "ShowAddDiv();", true);
                ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "toastr.error('Cannot update data')", true);
                txtSuppliername.Focus();
            }
        }
        blSupplier = null;
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
                //liSupplierName.Attributes.Add("style", "display:none;");
                hfUser.Value = Request.QueryString["uid"];
                hfClient.Value = Request.QueryString["clnt"];
                hfuserName.Value = Session["Username"].ToString();
                BindData();
                CheckUserAccess();
                GridFill(1);
                SetControls();
                if (Convert.ToBoolean(hfIsAdd.Value) == false)
                {
                    ((Label)CommonPopupPrivilege.FindControl("popuplabel")).Text = "User does not have permission to Add";
                    mpPrivilege.Show();
                }
                if (Convert.ToBoolean(hfIsUpdate.Value) == false)
                {
                    gvSupplier.Columns[4].Visible = false;
                }
                if (Convert.ToBoolean(hfIsDelete.Value) == false)
                {
                    gvSupplier.Columns[5].Visible = false;
                }
               
                //txtSuppliername.Focus();
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "toastr.warning('" + ex.Message + "')", true); 
        }
    }
    protected void Page_Changed(object sender, EventArgs e)
    {
       
        int pageIndex = int.Parse((sender as LinkButton).CommandArgument);

        GridFill(pageIndex);
    }
    protected void imgbtnView_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            
            divAdd.Style.Add("display", "none");
            divView.Style.Add("display", "block");
            lblTInMsg.Text = "";
            ScriptManager.RegisterStartupScript(Page, typeof(Page), "script", "ClearAdd();", true);
            txtSearchName.Focus();
        
             GridFill(1);
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "script", "toastr.warning('" + ex.Message + "')", true); 
        }
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        try
        {
           
             GridFill(1);
             txtSearchName.Focus();
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "script", "toastr.warning('" + ex.Message + "')", true); 
        }
    }
    protected void gvSupplier_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        try
        {
            
            ScriptManager.RegisterStartupScript(Page, typeof(Page), "script", "ShowAddDiv();", true);
            BlSupplier blSupplier = new BlSupplier();
            BlAccountHeadSubTransactions blaccountheadsubtransactions = new BlAccountHeadSubTransactions();
            blSupplier.MasterId = Convert.ToInt64(gvSupplier.DataKeys[e.NewSelectedIndex]["ID_Supplier"].ToString());
            hfSupplierId.Value = blSupplier.MasterId.ToString();
            blSupplier.FK_Client = Convert.ToInt64(hfClient.Value);
            blSupplier.UserCode = hfUser.Value;
            blSupplier.SelectData();
            txtSuppliername.Text = blSupplier.SupplierName.ToString();
            txtContractPerson.Text = blSupplier.ContactPerson.ToString();
            txtArea.Text = blSupplier.Area.ToString();
            txtPlace.Text = blSupplier.Place.ToString();
            txtPost.Text = blSupplier.Post.ToString();
            txtPin.Text = blSupplier.Pin.ToString();
            txtLandmark.Text = blSupplier.LandMark.ToString();
            ddlCountry.SelectedValue = blSupplier.FK_Country.ToString();
            new Populate().FillDropdownList(ddlState, "States", "StateName", "ID_States", "ID_States", "FK_Country = " + ddlCountry.SelectedValue + " and Cancelled=0 and FK_Client = " + hfClient.Value, "");
            ddlState.SelectedValue = blSupplier.FK_State.ToString();
            new Populate().FillDropdownList(ddlDistrict, "District", "DistrictName", "ID_District", "ID_District", "FK_Country = " + ddlCountry.SelectedValue + " and FK_States = " + ddlState.SelectedValue + " and Cancelled=0 and FK_Client = " + hfClient.Value, "");
            ddlDistrict.SelectedValue = blSupplier.FK_District.ToString();
            txtFax.Text = blSupplier.Fax.ToString();
            txtPhone.Text = blSupplier.Phone.ToString();
            txtMobile.Text = blSupplier.Mobile.ToString();
            txtTinNo.Text = blSupplier.TinNo.ToString();
            txtCstNo.Text = blSupplier.CstNo.ToString();
            txtKgstNo.Text = blSupplier.KgstNo.ToString();
            txtEmail.Text = blSupplier.Email.ToString();
            txtWebsite.Text = blSupplier.Website.ToString();
            chkActive.Checked = blSupplier.Active;
            chkGovSupplier.Checked = blSupplier.GovernmentSupplier;
           // liSupplierName.Attributes.Remove("style");
            blaccountheadsubtransactions.FK_Party = blSupplier.MasterId;
            blaccountheadsubtransactions.SubId = "SUPP";
            blaccountheadsubtransactions.FK_Client = Convert.ToInt64(hfClient.Value);
            ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "$('#<%#liSupplierBalance.ClientID%>').show();", true); 
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "toastr.warning('" + ex.Message + "')", true); 
        }
    }
    protected void gvSupplier_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            
            string confirmValue = Request.Form["confirm_value"];
            if (confirmValue.Length > 3)
                confirmValue = confirmValue.Substring(confirmValue.Length - 3, 3);
            if (confirmValue == "Yes")
            {
                long status = 0;
                BlSupplier blSupplier = new BlSupplier();
                blSupplier.MasterId = Convert.ToInt64(gvSupplier.DataKeys[e.RowIndex]["ID_Supplier"].ToString());
                hfMasterID.Value = (gvSupplier.DataKeys[e.RowIndex]["ID_Supplier"].ToString());
                blSupplier.FK_Client = Convert.ToInt64(hfClient.Value);
                blSupplier.UserCode = hfUser.Value;
                blSupplier = null;
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

          
    protected void btnSubHeadBlurTrigger_Click(object sender, EventArgs e)
    {
        try
        {

            ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "ShowAddDiv();", true);

        }
        catch (Exception ex)
        {

            ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "toastr.warning('" + ex.Message + "')", true);
        }

    }
    protected void btnCheckTinNumber_Click(object sender, EventArgs e)
    {
        try
        {
            BlSupplier blSupplier = new BlSupplier();
            blSupplier.TinNo = txtTinNo.Text.ToString();
            blSupplier.FK_Client = Convert.ToInt64(hfClient.Value);

            DataTable dtbl = new DataTable();
            dtbl = blSupplier.SelectTinNumber();
            if (dtbl.Rows.Count > 0)
            {

                lblTInMsg.ForeColor = System.Drawing.Color.Red;
                lblTInMsg.Text = "Tin already exist";
            }
            else
            {

                lblTInMsg.ForeColor = System.Drawing.Color.Green ;
                lblTInMsg.Text = "Tin available ";
            }
            divAdd.Style.Add("display", "block");
            divView.Style.Add("display", "none");

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
            
            SaveData();
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "toastr.warning('" + ex.Message + "')", true);
        }
    }
 

    protected void ImageButton2_Click(object sender, ImageClickEventArgs e)
    {
        Delete();
    }
   
    protected void gvSupplier_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvSupplier.PageIndex = e.NewPageIndex;
        GridFill(e.NewPageIndex);
    }
       protected void btnSubFill_Click(object sender, EventArgs e)
    {
        BindDropdown();
        divAdd.Style.Add("display", "block");
        divView.Style.Add("display", "none");
        ddlState.Focus();

    }

    protected void btnDistictFill_Click(object sender, EventArgs e)
    {
        BindDropdownDistrict();
        divAdd.Style.Add("display", "block");
        divView.Style.Add("display", "none");
        ddlDistrict.Focus();

    }
    
    protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
    {
        if (Convert.ToBoolean(hfIsAdd.Value) == false)
        {
            ((Label)CommonPopupPrivilege.FindControl("popuplabel")).Text = "User does not have permission to Add";
            mpPrivilege.Show();
        }
    }



    private bool ValidateSupplierInput(out string errorMessage)
    {
        errorMessage = string.Empty;

        if (string.IsNullOrWhiteSpace(txtSuppliername.Text))
        {
            errorMessage = "Supplier name is required";
            txtSuppliername.Focus();
            return false;
        }
        if (ddlCountry.SelectedValue == "0")
        {
            errorMessage = "Please select a country";
            ddlCountry.Focus();
            return false;
        }
        if (ddlState.SelectedValue == "0")
        {
            errorMessage = "Please select a state";
            ddlState.Focus();
            return false;
        }
        if (ddlDistrict.SelectedValue == "0")
        {
            errorMessage = "Please select a district";
            ddlDistrict.Focus();
            return false;
        }
        if (!string.IsNullOrWhiteSpace(txtPin.Text) &&
            !System.Text.RegularExpressions.Regex.IsMatch(txtPin.Text.Trim(), @"^\d{6}$"))
        {
            errorMessage = "Pin must be a valid 6-digit number";
            txtPin.Focus();
            return false;
        }
        if (!string.IsNullOrWhiteSpace(txtMobile.Text) &&
            !System.Text.RegularExpressions.Regex.IsMatch(txtMobile.Text.Trim(), @"^[0-9+\-]{7,15}$"))
        {
            errorMessage = "Mobile number format is invalid";
            txtMobile.Focus();
            return false;
        }
        if (!string.IsNullOrWhiteSpace(txtEmail.Text) &&
            !System.Text.RegularExpressions.Regex.IsMatch(txtEmail.Text.Trim(),
                @"^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$"))
        {
            errorMessage = "Email format is invalid";
            txtEmail.Focus();
            return false;
        }

        return true;
    }
    #endregion
}