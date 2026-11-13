/*----------------------------------------------------------------------------------------------------------------------------------------------------------
Purpose    : Insert/Update/Delete Customer  
Author     : Bibin TC
Copyright  : Perfect Software Solutions (Clt) Pvt Ltd
Created on : 03/05/2018
----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
WebPharma
On			By			OMID
----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
Purpose    : Insert/Update/Delete Customer  
Author     :Aiswarya
Copyright  : Perfect Software Solutions (Clt) Pvt Ltd
Created on : 23/05/2018
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
using System.Collections;
using System.IO;
using System.Globalization;
using WebPharma.Business.Masters;
using WebPharma.Business;

using System.Text;
using System.Security.Cryptography;
using System.Configuration;
using System.Web.Security;

using System.Xml;
using System.Data.SqlClient;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data.Common;


public partial class Customer : System.Web.UI.Page
{
    private const string Image = "Image";
    private const string TempPath = "~\\UploadedItems\\Temp\\";
    private const string UploadedPath = "~\\UploadedItems\\";
    private const string DefaultImage = "~/images/DefaultImage.jpg";
    private static Byte[] AttachmentData;
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
            BlCustomer blcustomer = new BlCustomer();
            blcustomer.UserCode = hfuserName.Value;
            blcustomer.MasterId = Convert.ToInt64(hfMasterID.Value);
            blcustomer.FK_Reason = Convert.ToInt64(Reason);
            if (hfIsDelete.Value.ToLower() == "true")
            {
                status = blcustomer.DeleteData();
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
            blcustomer = null;
        }
    }
    protected void BindData()
    {
        Populate populate = new Populate();
        populate.FillDropdownList(ddlIdpType, "IdentityProof", "IdpName", "ID_IdentityProof", "SortOrder", " FK_Client=" + hfClient.Value + "AND Cancelled=0", "");
        populate.FillDropdownList(ddlCountry, "Country", "CountryName", "ID_Country", "ID_Country", "FK_Client=" + hfClient.Value + "AND Cancelled=0", "");
        populate.FillDropdownList(ddlCustomerGroup, "CustomerGroup", "CustGroupName", "ID_CustomerGroup", "ID_CustomerGroup", "Cancelled=0 AND Active='True' AND FK_Client= " + hfClient.Value, "");
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
        // txtDob.Attributes.Add("readOnly", "readOnly");

    }
    protected void GridFill(int currentPage)
    {
        BlCustomer blcustomer = new BlCustomer();
        DataTable dtbl = new DataTable();
        blcustomer.Mobile = txtSearchMobile.Text;
        blcustomer.CustomerCode = txtSearchCustCode.Text;
        blcustomer.Name = txtSearchName.Text.Trim();
        blcustomer.FK_Client = Convert.ToInt64(hfClient.Value);
        blcustomer.UserCode = hfUser.Value;
        blcustomer.PageIndex = Convert.ToInt16(currentPage);
        blcustomer.Status = Convert.ToInt16(ddlactive.SelectedValue);
        dtbl = blcustomer.SelectAllData();
        gvCustomer.DataSource = dtbl;
        gvCustomer.DataBind();
        // clearsearchfileds();
        blcustomer = null;
    }
    private void clearsearchfileds()
    {
        txtSearchCustCode.Text = "";
        txtSearchMobile.Text = "";
        txtSearchName.Text = "";
        ddlactive.SelectedValue = "True";

    }
    protected void CheckUserAccess()
    {
        BlUserAccess bluseraccess = new BlUserAccess();
        Populate populate = new Populate();

        bluseraccess.FK_Users = Convert.ToInt64(hfUser.Value);
        bluseraccess.FK_Pages = 2;
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
        BlCustomer blcustomer = new BlCustomer();
        blcustomer.CustomerCode = txtCustCode.Text;
        blcustomer.Name = txtName.Text;
        blcustomer.Area = txtArea.Text;
        blcustomer.Post = txtPost.Text;
        blcustomer.Place = txtPlace.Text;
        blcustomer.Pin = txtPin.Text;
        blcustomer.LandMark = txtLandmark.Text;
        blcustomer.Phone = txtPhone.Text;
        blcustomer.Mobile = txtMobile.Text;
        blcustomer.Email = txtEmail.Text;
        blcustomer.DateOfBirth = Convert.ToDateTime(txtDob.Text == "" ? "01/01/1900 00:00:00" : txtDob.Text);
        blcustomer.InactiveRemarks = txtInactiveRemarks.Text;
        blcustomer.FK_Country = Convert.ToInt64(ddlCountry.SelectedValue.ToString());
        blcustomer.FK_District = Convert.ToInt64(ddlDistrict.SelectedValue.ToString());
        blcustomer.FK_State = Convert.ToInt64(ddlState.SelectedValue.ToString());
        blcustomer.Idtype = Convert.ToInt64(ddlIdpType.SelectedValue);
        blcustomer.CardNumber = Convert.ToString(Textcardnumber.Text);
        blcustomer.FK_CustomerGroup = Convert.ToInt64(ddlCustomerGroup.SelectedValue.ToString());
        blcustomer.Active = Convert.ToBoolean(chkActive.Checked);
        blcustomer.CreditSale = Convert.ToBoolean(ChkCreditSale.Checked);
        blcustomer.CardPrice = Convert.ToBoolean(ChkCardPrice.Checked);
        blcustomer.IsEmployee = Convert.ToBoolean(chkISEmployee.Checked);
        BlFunction blFunctions = new BlFunction();
        Boolean m = blFunctions.GetIdentityProof(ddlIdpType.SelectedValue, Textcardnumber.Text);
        if (m == false)
        {

            ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "ShowAddDiv();", true);
            ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "toastr.warning('AadharNo needs 12 numeric numbers');", true);

        }


        blcustomer.FK_Client = Convert.ToInt64(hfClient.Value);
        blcustomer.FK_Store = Convert.ToInt64(HdValuestore.Value);
        blcustomer.UserCode = hfuserName.Value;
        blcustomer.CreditDays = Convert.ToInt64(txtCreditDays.Text == "" ? "0" : txtCreditDays.Text);
        blcustomer.DiscountPercentage = Convert.ToDecimal(txtDiscountPercentage.Text == "" ? "0" : txtDiscountPercentage.Text);
        blcustomer.CreditLimit = Convert.ToDecimal(txtCreditLimit.Text == "" ? "0" : txtCreditLimit.Text);
        blcustomer.GSTTIN = txtGSTTIN.Text;
        if (Convert.ToInt64(ddlIdpType.SelectedValue) == 0 && Textcardnumber.Text != "")
        {
            ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "ShowAddDiv();", true);
            ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "toastr.warning('Select Identity Proof');ClearAdd();", true);
        }



        if (hfCustomerId.Value == "0")
        {
            if (hfIsAdd.Value.ToLower() == "true" && m == true)
            {
                BlFunction blfunctions = new BlFunction();
                Int16 type = Convert.ToInt16(blfunctions.GetScalarValue("Settings", "isnull(( select Value", "Name='SEC06'),0)", ""));
                if (type > 0)
                {
                    ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "toastr.warning('No Permission To Save New Customer');", true);

                }
                else
                {

                    status = blcustomer.InsertData();
                    if (status > 0)
                    {
                        ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "ShowAddDiv();", true);
                        ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "toastr.success('Data Inserted Successfully');ClearAdd();", true);
                        ClearAll();
                        ddlCustomerGroup.SelectedIndex = -1;
                        GridFill(1);

                    }
                    else
                    {
                        ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "ShowAddDiv();", true);
                        ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "toastr.error('Cannot insert data, Name already exists')", true);
                        // txtShortName.Focus();
                    }

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
            if (hfIsUpdate.Value.ToLower() == "true" && m == true)
            {
                blcustomer.MasterId = Convert.ToInt64(hfCustomerId.Value);
                status = blcustomer.UpdateData();
                if (status > 0)
                {
                    ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "ShowAddDiv();", true);
                    ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "toastr.success('Data Updated Successfully');ClearAdd();", true);
                    ClearAll();
                    GridFill(1);
                    // imgAttachment.ImageUrl = DefaultImage;
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "ShowAddDiv();", true);
                    ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "toastr.error('Cannot update data, Name already exists')", true);

                }
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "toastr.warning('User does not have permission to Update')", true);

            }
        }
        blcustomer = null;
    }
    protected void ClearAll()
    {
        Populate Populate = new Populate();
        Populate = null;

        ScriptManager.RegisterStartupScript(Page, typeof(Page), "script", "ShowAddDiv();", true);
        ScriptManager.RegisterStartupScript(Page, typeof(Page), "script", "ClearAdd();", true);
        lblCustomerBalance.Visible = false;
        txtCustCode.Text = string.Empty;
        unlock();
    }
    private void unlock()
    {
        ddlCustomerGroup.Enabled = true;
        txtCustCode.Enabled = true;
        txtName.Enabled = true;
        txtPin.Enabled = true;
        txtPhone.Enabled = true;
        txtMobile.Enabled = true;
        txtEmail.Enabled = true;
        txtDob.Enabled = true;
        ddlIdpType.Enabled = true;
        Textcardnumber.Enabled = true;
        ddlCountry.Enabled = true;
        chkActive.Enabled = true;

    }
    private void lockfield()
    {

        if (Convert.ToInt16(ViewState["type"]) > 0)
        {
            ddlCustomerGroup.Enabled = false;
            txtCustCode.Enabled = false;
            txtName.Enabled = false;
            txtPin.Enabled = false;
            txtPhone.Enabled = false;
            txtMobile.Enabled = false;
            txtEmail.Enabled = false;
            txtDob.Enabled = false;
            ddlIdpType.Enabled = false;
            Textcardnumber.Enabled = false;
            ddlCountry.Enabled = false;
            chkActive.Enabled = false;

        }
        else
        {

        }


    }
    protected bool validationfield()
    {
        BlFunction blfunctions1 = new BlFunction();
        Int16 type1 = Convert.ToInt16(blfunctions1.GetScalarValue("Settings", "isnull(( select Value", "Name='SEC06'),0)", ""));

        if (type1 > 0)
        {
            ddlIdpType.CssClass = "dropdown";
            Textcardnumber.CssClass = "textbox";

        }
        else
        {

            ddlIdpType.CssClass = "dropdownMandatory";
            Textcardnumber.CssClass = "textboxMandatory";

            if (ddlIdpType.SelectedValue == "0")
            {
                ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "toastr.warning('Identity Proof required');", true);
                return false;
            }
            else if (Textcardnumber.Text == "")
            {
                ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "toastr.warning('Card number required');", true);
                return false;
            }

        }
        BlCustomer blcustomer = new BlCustomer();
        DataTable dtbl = new DataTable();
        Int64 cusomerGroup = Convert.ToInt64(ddlCustomerGroup.SelectedValue);
        blcustomer.FK_CustomerGroup = cusomerGroup;
        dtbl = blcustomer.SelectCustCreditLimit();
        if (dtbl.Rows.Count > 0 && txtCreditLimit.Text.ToString() != "0.000")
        {
            if (Convert.ToDecimal(txtCreditLimit.Text.ToString()) > Convert.ToDecimal(dtbl.Rows[0]["CreditLimit"].ToString()))
            {
                ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "toastr.warning('Credit Limit Exceeded');", true);
                ScriptManager.RegisterStartupScript(Page, typeof(Page), "script", "ShowAddDiv();", true);
                return false;
            }

        }
        if (dtbl.Rows.Count > 0 && txtCreditDays.Text.ToString() != "0")
        {
            if (Convert.ToDecimal(txtCreditDays.Text.ToString()) > Convert.ToDecimal(dtbl.Rows[0]["CreditDays"].ToString()))
            {
                ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "toastr.warning('Credit Days Exceeded');", true);
                ScriptManager.RegisterStartupScript(Page, typeof(Page), "script", "ShowAddDiv();", true);
                return false;
            }
        }
        return true;
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
                Int16 type = Convert.ToInt16(blfunctions.GetScalarValue("Settings", "isnull(( select Value", "Name='SEC04'),0)", ""));
                ViewState["type"] = type.ToString();
                if (type > 0)
                {
                    btnCustomerLinking.Visible = true;
                    btnSubmit.Visible = false;
                    ddlIdpType.CssClass = "dropdown";
                    Textcardnumber.CssClass = "textbox";
                    ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "toastr.warning('No Permission To Save New Customer');", true);
                }
                else
                {
                    btnCustomerLinking.Visible = false;
                    btnSubmit.Visible = true;
                    ddlIdpType.CssClass = "dropdownMandatory";
                    Textcardnumber.CssClass = "textboxMandatory";
                }

                Page.Header.DataBind();

                ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "ClearAdd();", true);
                divAdd.Style.Add("display", "block");
                hfUser.Value = Request.QueryString["uid"];
                hfClient.Value = Request.QueryString["clnt"];
                hfuserName.Value = Session["Username"].ToString();
                HdValuestore.Value = Session["mainstore"].ToString();
                txtCreditLimit.Attributes.Add("onkeypress", "CheckNumWithDecimal(event)");
                txtCreditDays.Attributes.Add("onkeypress", "CheckInteger(event)");
                CheckUserAccess();
                GridFill(1);
                BindData();
                SetControls();
                if (Convert.ToBoolean(hfIsAdd.Value) == false)
                {
                    ((Label)CommonPopupPrivilege.FindControl("popuplabel")).Text = "User does not have permission to Add";
                    mpPrivilege.Show();
                }
                if (Convert.ToBoolean(hfIsUpdate.Value) == false)
                {
                    gvCustomer.Columns[5].Visible = false;
                }
                if (Convert.ToBoolean(hfIsDelete.Value) == false)
                {
                    gvCustomer.Columns[6].Visible = false;
                }
                txtCurrentDate.Text = DateTime.Now.ToShortDateString();
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

            btnSubmit.Visible = true;
            btnCustomerLinking.Visible = false;
            Populate Populate = new Populate();
            Populate = null;
            ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "ClearAdd();EnableExport();", true);
            GridFill(1);
            divAdd.Style.Add("display", "none");
            divView.Style.Add("display", "block");
            txtSearchName.Focus();
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

            GridFill(1); txtSearchCustCode.Focus();
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "toastr.warning('" + ex.Message + "')", true);
        }
    }
    protected void gvCustomer_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        try
        {
            BlFunction blfunctions1 = new BlFunction();
            Int16 type1 = Convert.ToInt16(blfunctions1.GetScalarValue("Settings", "isnull(( select Value", "Name='SEC06'),0)", ""));

            if (type1 > 0)
            {
                ddlIdpType.CssClass = "dropdown";
                Textcardnumber.CssClass = "textbox";

            }
            else
            {

                ddlIdpType.CssClass = "dropdownMandatory";
                Textcardnumber.CssClass = "textboxMandatory";

            }

            ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "ShowAddDiv();", true);
            BlCustomer blcustomer = new BlCustomer();
            Populate populate = new Populate();
            BlAccountHeadSubTransactions blaccountheadsubtransactions = new BlAccountHeadSubTransactions();

            blcustomer.MasterId = Convert.ToInt64(gvCustomer.DataKeys[e.NewSelectedIndex]["ID_Customer"].ToString());
            populate = null;
            hfCustomerId.Value = blcustomer.MasterId.ToString();
            blcustomer.FK_Client = Convert.ToInt64(hfClient.Value);
            blcustomer.Status = Convert.ToInt16(ddlactive.SelectedValue);
            blcustomer.UserCode = hfUser.Value;
            blcustomer.SelectData();
            ddlCustomerGroup.SelectedValue = blcustomer.FK_CustomerGroup.ToString();
            txtName.Text = blcustomer.Name.ToString();
            txtCustCode.Text = blcustomer.CustomerCode.ToString();
            txtArea.Text = blcustomer.Area.ToString();
            txtPlace.Text = blcustomer.Place.ToString();
            txtPost.Text = blcustomer.Post.ToString();
            txtPin.Text = blcustomer.Pin;
            txtPhone.Text = blcustomer.Phone;
            txtMobile.Text = blcustomer.Mobile;
            txtEmail.Text = blcustomer.Email;
            txtLandmark.Text = blcustomer.LandMark;
            txtCreditDays.Text = blcustomer.CreditDays.ToString();
            txtCreditLimit.Text = blcustomer.CreditLimit.ToString();
            txtDob.Text = blcustomer.DateOfBirth.ToShortDateString() == "01/01/1900" ? "" : blcustomer.DateOfBirth.ToShortDateString();
            txtInactiveRemarks.Text = blcustomer.InactiveRemarks;
            try
            {
                ddlCountry.SelectedValue = blcustomer.FK_Country.ToString();
            }
            catch
            {
                ddlCountry.SelectedValue = "0";
            }
            BindDropdown();
            try
            {
                ddlState.SelectedValue = blcustomer.FK_State.ToString();
            }
            catch
            {
                ddlState.SelectedValue = "0";
            }
            BindDropdownDistrict();
            try
            {
                ddlDistrict.SelectedValue = blcustomer.FK_District.ToString();
            }
            catch
            {
                ddlDistrict.SelectedValue = "0";
            }

            chkISEmployee.Checked = blcustomer.IsEmployee;

            txtCreditDays.Text = blcustomer.CreditDays.ToString();
            ChkCardPrice.Checked = blcustomer.CardPrice;
            txtDiscountPercentage.Text = blcustomer.DiscountPercentage.ToString();
            txtCreditLimit.Text = blcustomer.CreditLimit.ToString();
            ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "Remarks();", true);

            //if (chkActive.Checked == false)
            //{
            //  //  ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "$('#<%#liInactiveRemarks.ClientID%>').show();", true);
            //      liInactiveRemarks.Visible = true;
            //}
            //else
            //{
            //   // ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "$('#<%#liInactiveRemarks.ClientID%>').hide();", true);
            //    liInactiveRemarks.Visible = false;
            //}
            try
            {
                ddlCustomerGroup.SelectedValue = blcustomer.FK_CustomerGroup.ToString();
            }
            catch (Exception)
            {
                ddlCustomerGroup.SelectedValue = "0";
            }

            chkActive.Checked = blcustomer.Active;
            ChkCreditSale.Checked = blcustomer.CreditSale;
            ChkCardPrice.Checked = blcustomer.CardPrice;
            ddlIdpType.SelectedValue = Convert.ToString(blcustomer.Idtype);
            Textcardnumber.Text = blcustomer.CardNumber;

            lblCustomerBalance.Visible = true;
            blaccountheadsubtransactions.FK_Party = blcustomer.MasterId;
            blaccountheadsubtransactions.SubId = "CUST";
            blaccountheadsubtransactions.FK_Client = Convert.ToInt64(hfClient.Value);
            lblCustomerBalance.Text = "";
            txtGSTTIN.Text = blcustomer.GSTTIN;
            if (Convert.ToDecimal(blaccountheadsubtransactions.BalanceAmount.ToString()) >= 0)
            {
                // lblCustomerBalance.Text = "Outstanding Credit Amount = " + blaccountheadsubtransactions.BalanceAmount.ToString();
            }
            else
            {
                // lblCustomerBalance.Text = "Outstanding Debit Amount = " + Math.Abs(Convert.ToDecimal(blaccountheadsubtransactions.BalanceAmount.ToString()));
            }
            if (blcustomer.Attachment != string.Empty)
            {

            }
            else
            {

            }

            blcustomer = null;
            blaccountheadsubtransactions = null;
            ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "$('#<%#liCustomerBalance.ClientID%>').show();", true);
            lockfield();
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "toastr.warning('" + ex.Message + "')", true);
        }

    }

    protected void gvCustomer_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {

            string confirmValue = Request.Form["confirm_value"];
            if (confirmValue.Length > 3)
                confirmValue = confirmValue.Substring(confirmValue.Length - 3, 3);
            if (confirmValue == "Yes")
            {
                long status = 0;
                BlCustomer blcustomer = new BlCustomer();
                blcustomer.MasterId = Convert.ToInt64(gvCustomer.DataKeys[e.RowIndex]["ID_Customer"].ToString());
                blcustomer.FK_Client = Convert.ToInt64(hfClient.Value);
                blcustomer.UserCode = hfUser.Value;
                hfMasterID.Value = (gvCustomer.DataKeys[e.RowIndex]["ID_Customer"].ToString());


                blcustomer = null;
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

            if (validationfield())
            {
                SaveData();

            }


        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "toastr.warning('" + ex.Message + "')", true);
        }
    }

    protected void btnClear_Click(object sender, EventArgs e)
    {
        ClearAll();
        BlFunction blfunctions = new BlFunction();
        Int16 type = Convert.ToInt16(blfunctions.GetScalarValue("Settings", "isnull(( select Value", "Name='SEC04'),0)", ""));
        ViewState["type"] = type.ToString();
        if (type > 0)
        {
            btnCustomerLinking.Visible = true;
            btnSubmit.Visible = false;
            ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "toastr.warning('No Permission To Save New Customer');", true);
        }
        else
        {
            btnCustomerLinking.Visible = false;
            btnSubmit.Visible = true;
        }
    }
    protected void gvCustomer_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "SelectName")
        {
            GridViewRow gr = (GridViewRow)((LinkButton)e.CommandSource).NamingContainer;
            int index = gr.RowIndex;
            long id = Convert.ToInt64(gvCustomer.DataKeys[index]["ID_Customer"].ToString());
            hfUrl.Value = "?ID=" + id.ToString() + "&uid=" + Request.QueryString["uid"] + "&nme=" + Request.QueryString["nme"] + "&lgn=" + Request.QueryString["lgn"] + "&lpwd=" + Request.QueryString["lpwd"] + "&clnt=" + Request.QueryString["clnt"];
            ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "script", "popUp();", true);
        }


    }

    protected void btnHidden_Click(object sender, EventArgs e)
    {
        try
        {
            if (Session["Attachment"] != null)
            {


            }
            else
            {

            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "toastr.warning('" + ex.Message + "')", true);
        }
    }



    protected void ddlCustomerGroup_SelectedIndexChanged(object sender, EventArgs e)
    {


        BlCustomer blcustomer = new BlCustomer();
        DataTable dtbl = new DataTable();

        Int64 cusomerGroup = Convert.ToInt64(ddlCustomerGroup.SelectedValue);
        blcustomer.FK_CustomerGroup = cusomerGroup;
        blcustomer.FK_Client = Convert.ToInt64(hfClient.Value);
        blcustomer.MasterId = Convert.ToInt32(hfCustomerId.Value);
        dtbl = blcustomer.SelectCustId();

        txtCustCode.Text = dtbl.Rows[0]["Column1"].ToString();
        txtCustCode.Attributes.Add("readonly", "readonly");


        divAdd.Style.Add("display", "block");
        divView.Style.Add("display", "none");
        blcustomer = null;
        txtName.Focus();
    }
    protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
    {
        ddlCustomerGroup.SelectedValue = "0";
        ddlCustomerGroup.Enabled = true;

        txtCustCode.Enabled = true;
        txtName.Enabled = true;

        txtPin.Enabled = true;
        txtPhone.Enabled = true;
        txtMobile.Enabled = true;
        txtEmail.Enabled = true;
        txtDob.Enabled = true;
        ddlIdpType.Enabled = true;
        Textcardnumber.Enabled = true;
        ddlCountry.Enabled = true;
        chkActive.Enabled = true;
        divAdd.Style.Add("display", "block");
        divView.Style.Add("display", "none");
        txtCustCode.Text = string.Empty;

        BlFunction blfunctions = new BlFunction();
        Int16 type = Convert.ToInt16(blfunctions.GetScalarValue("Settings", "isnull(( select Value", "Name='SEC04'),0)", ""));
        ViewState["type"] = type.ToString();
        if (type > 0)
        {
            btnCustomerLinking.Visible = true;
            btnSubmit.Visible = false;
            ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "toastr.warning('No Permission To Save New Customer');", true);
        }
        else
        {
            btnCustomerLinking.Visible = false;
            btnSubmit.Visible = true;
        }
        if (Convert.ToBoolean(hfIsAdd.Value) == false)
        {
            ((Label)CommonPopupPrivilege.FindControl("popuplabel")).Text = "User does not have permission to Add";
            mpPrivilege.Show();
        }

    }
    protected void btnCustomerLinkingt_Click(object sender, EventArgs e)
    {
        if (Convert.ToInt16(ViewState["type"]) > 0)
        {
            Database db = DatabaseFactory.CreateDatabase("WebPharma");
            string sqlCommand = "ProSecurityCustomerLinking ";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

            try
            {
                db.ExecuteScalar(dbCommand);

            }
            catch (SqlException Ex)
            {

            }
        }

    }

    protected void btnDistictFill_Click(object sender, EventArgs e)
    
    {
        BindDropdownDistrict();
        divAdd.Style.Add("display", "block");
        divView.Style.Add("display", "none");
        ddlDistrict.Focus();

    }

    protected void btnGridFill_Click(object sender, EventArgs e)
    {
        BindDropdown();
        divAdd.Style.Add("display", "block");
        divView.Style.Add("display", "none");
        ddlState.Focus();
    }
    protected void ImageButton2_Click(object sender, ImageClickEventArgs e)
    {
        Delete();
    }

    protected void gvCustomer_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvCustomer.PageIndex = e.NewPageIndex;
        GridFill(e.NewPageIndex);
    }
    #endregion
    protected void btBlurCreditlimit_Click(object sender, EventArgs e)
        {

        BlCustomer blcustomer = new BlCustomer();
        DataTable dtbl = new DataTable();
        Int64 cusomerGroup = Convert.ToInt64(ddlCustomerGroup.SelectedValue == "" ? "0" : ddlCustomerGroup.SelectedValue);
        blcustomer.FK_CustomerGroup = cusomerGroup;
        dtbl = blcustomer.SelectCustCreditLimit();
        if (dtbl.Rows.Count > 0 && txtCreditLimit.Text.ToString() != "0")
        {
            if (Convert.ToDecimal(txtCreditLimit.Text.ToString()) > Convert.ToDecimal(dtbl.Rows[0]["CreditLimit"].ToString()))
            {
                ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "toastr.warning('Credit Limit Exceeded');", true);

            }

        }

        divAdd.Style.Add("display", "block");
        divView.Style.Add("display", "none");
       txtCreditDays.Focus();


    }
    protected void btBlurCreditDays_Click(object sender, EventArgs e)
    {
        BlCustomer blcustomer = new BlCustomer();
        DataTable dtbl = new DataTable();
        Int64 cusomerGroup = Convert.ToInt64(ddlCustomerGroup.SelectedValue == "" ? "0" : ddlCustomerGroup.SelectedValue);
        blcustomer.FK_CustomerGroup = cusomerGroup;
        dtbl = blcustomer.SelectCustCreditLimit();
        if (dtbl.Rows.Count > 0 && txtCreditDays.Text.ToString() != "0")
        {
            if (Convert.ToDecimal(txtCreditDays.Text.ToString()) > Convert.ToDecimal(dtbl.Rows[0]["CreditDays"].ToString()))
            {
                ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "toastr.warning('Credit Days Exceeded');", true);

            }
        }
         divAdd.Style.Add("display", "block");
         divView.Style.Add("display", "none");
        txtDiscountPercentage.Focus();
    }


}