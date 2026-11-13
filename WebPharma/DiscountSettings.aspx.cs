/*----------------------------------------------------------------------------------------------------------------------------------------------------------
Purpose    : Insert/Update/Delete Discount 
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
using System.Globalization;


public partial class DiscountSettings : System.Web.UI.Page
{
    #region Function
    protected void BindData()
    {
        Populate populate = new Populate();
        populate.FillDropdownList(ddlDiscount, "Discount", "DisName", "ID_Discount", "ID_Discount", "FK_Client=" + hfClient.Value, "");
        populate.FillDropdownList(ddlDiscountSearch, "Discount", "DisName", "ID_Discount", "ID_Discount", "FK_Client=" + hfClient.Value, "");
        populate.FillDropdownListAll(ddlEmployee, "Employee", "Name", "ID_Employee", "ID_Employee", "  Active='True'  and FK_Client = " + hfClient.Value, "");
        populate.FillDropdownList(ddlItems, "Items", "Name", "ID_Items", "ID_Items", "  Active='True' AND FK_Client= " + hfClient.Value, "");
        populate.FillDropdownList(ddlCustomer, "Customer", "Name", "ID_Customer", "ID_Customer", "  Active='True' AND FK_Client= " + hfClient.Value, "");
        populate.FillDropdownList(ddlSalesMan, "Employee", "Name", "ID_Employee", "ID_Employee", "Active='True'  and Salesman='True' and FK_Client = " + hfClient.Value, "");
        populate.FillDropdownList(ddlCustomerGroup, "CustomerGroup", "Name", "ID_CustomerGroup", "ID_CustomerGroup", "Active='True' and FK_Client = " + hfClient.Value, "");
        populate.FillDropdownList(ddlCategoryFirst, "CategoryFirst", "Name", "ID_CategoryFirst", "ID_CategoryFirst", " FK_Client= " + hfClient.Value, "");
        populate.FillDropdownList(ddlBrand, "Brand", "Name", "ID_Brand", "ID_Brand", " FK_Client=1", "");



        populate = null;
    }

    protected void SetControls()
    {
        txtFromDate.Attributes.Add("readOnly", "readOnly");
        txtToDate.Attributes.Add("readOnly", "readOnly");
        txtFromDateSearch.Attributes.Add("readOnly", "readOnly");
        txtTodateSearch.Attributes.Add("readOnly", "readOnly");
        txtFromDateSearch.Text = DateTime.Now.ToShortDateString();
        txtTodateSearch.Text = DateTime.Now.ToShortDateString();
    }

    protected void GridFill(int currentPage)
    {
        BlDiscountSettings BlDiscountSettings = new BlDiscountSettings();
        DataTable dtbl = new DataTable();

        BlDiscountSettings.FK_Client = Convert.ToInt64(hfClient.Value);
        BlDiscountSettings.UserCode = hfUser.Value;
        BlDiscountSettings.FK_Discount = Convert.ToInt64(ddlDiscountSearch.SelectedValue.ToString());
        BlDiscountSettings.FromDate = DateTime.ParseExact(txtFromDateSearch.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
        BlDiscountSettings.ToDate = DateTime.ParseExact(txtTodateSearch.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
        BlDiscountSettings.DiscountType = Convert.ToByte(ddlDiscountTypeSearch.SelectedValue);

        BlDiscountSettings.PageSize = 10;
        BlDiscountSettings.DisName = txtDisnameSearch.Text;
        currentPage = currentPage == 0 ? 1 : currentPage;
        BlDiscountSettings.PageIndex = Convert.ToInt16(currentPage);
        dtbl = BlDiscountSettings.SelectAllData();
        gvDiscount.DataSource = dtbl;
        gvDiscount.DataBind();
        if (dtbl.Rows.Count > 0)
        {
            int recordCount = Convert.ToInt32(dtbl.Rows[0]["RecordCount"].ToString());
            this.PopulatePager(recordCount, currentPage);
        }
        BlDiscountSettings = null;
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
    protected void CheckUserAccess()
    {
        BlUserAccess bluseraccess = new BlUserAccess();
        Populate populate = new Populate();

        bluseraccess.FK_Users = Convert.ToInt64(hfUser.Value);
        bluseraccess.FK_Pages = 22;
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
        BlDiscountSettings BlDiscountSettings = new BlDiscountSettings();
        BlDiscountSettings.EnteredOn = Convert.ToDateTime(DateTime.Now);
        BlDiscountSettings.FK_Client = Convert.ToInt64(hfClient.Value);
        BlDiscountSettings.UserCode = hfUser.Value;
        BlDiscountSettings.DiscountType = Convert.ToByte(RdDiscountType.SelectedItem.Value.ToString());

        BlDiscountSettings.FK_Discount = Convert.ToInt64(ddlDiscount.SelectedValue.ToString());
        BlDiscountSettings.FK_Employee = Convert.ToInt64(ddlEmployee.SelectedValue.ToString());
        BlDiscountSettings.FK_Customer = Convert.ToInt64(ddlCustomer.SelectedValue.ToString());
        if (RdDiscountType.SelectedItem.Value.ToString() == "5")
        {
            BlDiscountSettings.FK_Items = Convert.ToInt64(ddlItems.SelectedValue.ToString());
        }
        else if (RdDiscountType.SelectedItem.Value.ToString() == "6")
        {
            BlDiscountSettings.FK_CategoryFirst = Convert.ToInt64(ddlCategoryFirst.SelectedValue.ToString());
        }
        else if (RdDiscountType.SelectedItem.Value.ToString() == "7")
        {
            BlDiscountSettings.FK_Brand = Convert.ToInt64(ddlBrand.SelectedValue.ToString());
        }
        BlDiscountSettings.FK_SalesMan = Convert.ToInt64(ddlSalesMan.SelectedValue.ToString());
        BlDiscountSettings.FK_CustomerGroup = Convert.ToInt64(ddlCustomerGroup.SelectedValue);
        BlDiscountSettings.FromDate = Convert.ToDateTime(txtFromDate.Text);
        BlDiscountSettings.ToDate = Convert.ToDateTime(txtToDate.Text);
        BlDiscountSettings.Closed = cbxIsClosed.Checked;
        BlDiscountSettings.Percentage = Convert.ToDecimal(txtPercentage.Text == "" ? "0" : txtPercentage.Text);
        BlDiscountSettings.Amount = Convert.ToDecimal(txtAmount.Text == "" ? "0" : txtAmount.Text);
        BlDiscountSettings.DisName = txtDisName.Text;



        if (hfDiscountSettingsId.Value == "0")
        {
            if (hfIsAdd.Value.ToLower() == "true")
            {
                status = BlDiscountSettings.InsertData();
                if (status > 0)
                {
                    ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "ShowAddDiv();", true);
                    ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "toastr.success('Data inserted successfully');ClearAdd();", true);
                    clear();
                    GridFill(0);
                }
                else if (status == -2)
                {
                    ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "ShowAddDiv();", true);
                    ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "toastr.error('Cannot insert data, Discount Settings already exists')", true);
                    // ddlDiscount.Focus();
                }
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "toastr.warning('User does not have permission to add')", true);
                // ddlDiscount.Focus();

            }
        }
        else
        {
            if (hfIsUpdate.Value.ToLower() == "true")
            {

                BlDiscountSettings.MasterId = Convert.ToInt64(hfDiscountSettingsId.Value);
                status = BlDiscountSettings.UpdateData();
                if (status > 0)
                {
                    // ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "ShowAddDiv();", true);
                    ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "toastr.success('Data updated successfully');ClearAdd();", true);
                    clear();
                    GridFill(0);
                }
                else if (status == -2)
                {
                    // ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "ShowAddDiv();", true);
                    ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "toastr.error('Cannot update data, Discount Settings already exists')", true);
                    // ddlDiscount.Focus();
                }
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "toastr.warning('User does not have permission to Update')", true);
                // ddlDiscount .Focus();
            }
        }
        BlDiscountSettings = null;

        //  divAdd.Style.Add("display", "block");
        //  divView.Style.Add("display", "none");
    }

    protected void ShowTextForAmount()
    {
        decimal Percentage = 0;
        Percentage = Convert.ToDecimal(txtPercentage.Text == "" ? "0" : txtPercentage.Text);
        decimal Amount = 0;
        Amount = Convert.ToDecimal(txtAmount.Text == "" ? "0" : txtAmount.Text);

        if (Percentage > 0)
        {
            liAmount.Visible = false;
            liPercent.Visible = true;
            txtAmount.Text = Convert.ToString(0);
        }

        if (Amount > 0)
        {
            liPercent.Visible = false;
            liAmount.Visible = true;
            txtPercentage.Text = Convert.ToString(0);

        }
        //else if (Amount == 0 && Percentage == 0 && RdDiscountType.SelectedValue != "5")
        //{
        //    liAmount.Visible = true;
        //    liPercent.Visible = true;
        //}
        //else if (RdDiscountType.SelectedValue == "5")
        //{
        //    liAmount.Visible = true;
        //    liPercent.Visible = true;
        //}
    }
    private void ClearDropdown()
    {
        ddlEmployee.SelectedIndex = 0;
        ddlCustomer.SelectedIndex = 0;
        ddlItems.SelectedIndex = 0;
        ddlSalesMan.SelectedIndex = 0;
        ddlCustomerGroup.SelectedIndex = 0;

    }
    protected void clear()
    {
        txtAmount.Text = "0";
        txtPercentage.Text = "0";
        liAmount.Visible = true;
        liPercent.Visible = true;

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


                divView.Visible = false;
                hfUser.Value = Request.QueryString["uid"];
                hfClient.Value = Request.QueryString["clnt"];
                CheckUserAccess();
                BindData();
                RdDiscountType.SelectedIndex = 0;
                SetControls();
                CheckForDiscountType();
                txtDisName.Focus();

            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "toastr.warning('" + ex.Message + "')", true);
        }
    }
    protected void img_Click(object sender, EventArgs e)
    {

    }
    protected void imgAdd_Click(object sender, EventArgs e)
    {
        divView.Visible = false;
        divAdd.Visible = true;
        ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "ClearAdd();", true);
    }
    protected void imgbtnView_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            //blMessage.Items.Clear();

            divView.Visible = true;
            divAdd.Visible = false;
            //  divAdd.Style.Add("display", "none");
            // divView.Style.Add("display", "block");
            ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "ClearAdd();ShowVeiwDiv();", true);
            GridFill(1);
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "toastr.warning('" + ex.Message + "')", true);
        }
    }
    protected void Page_Changed(object sender, EventArgs e)
    {
        //blMessage.Items.Clear();
        int pageIndex = int.Parse((sender as LinkButton).CommandArgument);

        //DataTable dtbl = new DataTable();
        //BlDiscountSettings BlDiscountSettings = new BlDiscountSettings();
        //BlDiscountSettings.PageSize = 10;
        //BlDiscountSettings.PageIndex = Convert.ToInt16(pageIndex);
        //    dtbl = BlDiscountSettings.SelectAllData();
        //  gvDiscount.DataSource = dtbl;
        //  gvDiscount.DataBind();
        GridFill(pageIndex);
        //if (dtbl.Rows.Count > 0)
        //{
        //    int recordCount = Convert.ToInt32(dtbl.Rows[0]["RecordCount"].ToString());
        //    this.PopulatePager(recordCount, pageIndex);
        //}
        //BlDiscountSettings = null;
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            //blMessage.Items.Clear();
            SaveData();
            txtDisName.Focus();

        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "toastr.warning('" + ex.Message + "')", true);
        }
    }
    protected void gvDiscount_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        try
        {
            //blMessage.Items.Clear();
            ClearLi();
            //ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "ShowAddDiv();", true);
            BlDiscountSettings BlDiscountSettings = new BlDiscountSettings();
            BlDiscountSettings.MasterId = Convert.ToInt64(gvDiscount.DataKeys[e.NewSelectedIndex]["ID_DiscountSettings"].ToString());
            hfDiscountSettingsId.Value = BlDiscountSettings.MasterId.ToString();
            BlDiscountSettings.FromDate = DateTime.Parse("1/1/1900");
            BlDiscountSettings.ToDate = DateTime.MaxValue;
            BlDiscountSettings.DiscountType = 0;
            BlDiscountSettings.FK_Client = Convert.ToInt64(hfClient.Value);
            BlDiscountSettings.UserCode = hfUser.Value;
            BlDiscountSettings.SelectData();

            RdDiscountType.SelectedValue = BlDiscountSettings.DiscountType.ToString();
            CheckForDiscountType();

            ddlDiscount.SelectedValue = BlDiscountSettings.FK_Discount.ToString();
            try
            {
                ddlEmployee.SelectedValue = BlDiscountSettings.FK_Employee.ToString();
            }
            catch (Exception ex)
            {
                ddlEmployee.SelectedValue = "0";
            }
            try
            {
                ddlCustomer.SelectedValue = BlDiscountSettings.FK_Customer.ToString();
            }
            catch (Exception ex)
            {
                ddlCustomer.SelectedValue = "0";
            }
            //try
            //{
            //    ddlItems.SelectedValue = BlDiscountSettings.FK_Items.ToString();
            //}
            //catch(Exception ex)
            //{
            //    ddlItems.SelectedValue = "0"; 
            //}
            try
            {
                ddlSalesMan.SelectedValue = BlDiscountSettings.FK_SalesMan.ToString();
            }
            catch (Exception ex)
            {
                ddlSalesMan.SelectedValue = "0";
            }
            try
            {
                ddlCustomerGroup.SelectedValue = BlDiscountSettings.FK_CustomerGroup.ToString();
            }
            catch (Exception ex)
            {
                ddlCustomerGroup.SelectedValue = "0";
            }

            if (BlDiscountSettings.DiscountType == 5)
            {
                ddlItems.SelectedValue = BlDiscountSettings.FK_Items.ToString();
                liItems.Visible = true;
            }
            else if (BlDiscountSettings.DiscountType == 6)
            {
                ddlCategoryFirst.SelectedValue = BlDiscountSettings.FK_CategoryFirst.ToString();
                LiCategoryFirst.Visible = true;
            }
            else if (BlDiscountSettings.DiscountType == 7)
            {
                ddlBrand.SelectedValue = BlDiscountSettings.FK_Brand.ToString();
                LiBrand.Visible = true;
            }


            txtFromDate.Text = BlDiscountSettings.FromDate.ToString("dd/MM/yyyy");
            txtToDate.Text = BlDiscountSettings.ToDate.ToString("dd/MM/yyyy");
            cbxIsClosed.Checked = BlDiscountSettings.Closed;
            txtPercentage.Text = BlDiscountSettings.Percentage.ToString();
            txtAmount.Text = BlDiscountSettings.Amount.ToString();
            txtDisName.Text = BlDiscountSettings.DisName.ToString();
            ShowTextForAmount();


            //txtName.Text = BlDiscountSettings.DisName.ToString();
            //ddlUserGroup.SelectedValue = BlDiscountSettings.FK_UserGroup.ToString();
            BlDiscountSettings = null;

            divAdd.Visible = true;
            divView.Visible = false;
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "toastr.warning('" + ex.Message + "')", true);
        }
    }
    protected void gvDiscount_RowDeleting(object sender, GridViewDeleteEventArgs e)
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
                BlDiscountSettings BlDiscountSettings = new BlDiscountSettings();
                BlDiscountSettings.MasterId = Convert.ToInt64(gvDiscount.DataKeys[e.RowIndex]["ID_DiscountSettings"].ToString());
                BlDiscountSettings.FK_Client = Convert.ToInt64(hfClient.Value);
                BlDiscountSettings.UserCode = hfUser.Value;
                if (hfIsDelete.Value.ToLower() == "true")
                {
                    status = BlDiscountSettings.DeleteData();
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
                }
                BlDiscountSettings = null;
            }
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
            txtDisnameSearch.Focus();
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "toastr.warning('" + ex.Message + "')", true);
        }
    }

    protected void CheckForDiscountType()
    {
        ClearDropdown();
        if (RdDiscountType.SelectedItem.Value.ToString() == "1")
        {
            ShowTextForAmount();
            ClearLi();
            liEmployee.Visible = true;
            ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "ShowAddDiv();", true);
        }

        else if (RdDiscountType.SelectedItem.Value.ToString() == "2")
        {
            ShowTextForAmount();
            ClearLi();
            liCustomer.Visible = true;

            ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "ShowAddDiv();", true);
        }
        else if (RdDiscountType.SelectedItem.Value.ToString() == "3")
        {
            ShowTextForAmount();
            ClearLi();
            liSalesMan.Visible = true;

            ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "ShowAddDiv();", true);
        }
        else if (RdDiscountType.SelectedValue == "4")
        {
            txtAmount.Text = Convert.ToString(0);

            ClearLi();
            // liPercent.Visible = true;         
            liCustomerGroup.Visible = true;

            ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "ShowAddDiv();", true);
        }
        else if (RdDiscountType.SelectedItem.Value.ToString() == "5")
        {
            ShowTextForAmount();
            ClearLi();
            liItems.Visible = true;
            ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "ShowAddDiv();", true);
        }
        else if (RdDiscountType.SelectedItem.Value.ToString() == "6")
        {
            ShowTextForAmount();
            ClearLi();
            LiCategoryFirst.Visible = true;

            ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "ShowAddDiv();", true);
        }
        else if (RdDiscountType.SelectedItem.Value.ToString() == "7")
        {
            ShowTextForAmount();
            ClearLi();
            LiBrand.Visible = true;

            ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "ShowAddDiv();", true);
        }
    }
    protected void ClearLi()
    {
        liCustomer.Visible = false;
        liItems.Visible = false;
        LiCategoryFirst.Visible = false;
        LiBrand.Visible = false;
        liSalesMan.Visible = false;
        liCustomerGroup.Visible = false;
        liEmployee.Visible = false;
        //liAmount.Visible = false;

    }
    protected void RdDiscountType_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            CheckForDiscountType();
            switch (RdDiscountType.SelectedValue)
            {
                case "1": ddlEmployee.Focus();
                    break;
                case "2": ddlCustomer.Focus();
                    break;
                case "3": ddlSalesMan.Focus();
                    break;
                case "4": ddlCustomerGroup.Focus();
                    break;
                case "5": ddlItems.Focus();
                    break;
                case "6": ddlCategoryFirst.Focus();
                    break;
                default: ddlBrand.Focus();
                    break;
            }

        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "toastr.warning('" + ex.Message + "')", true);
        }
    }

    protected void txtPercentage_TextChanged(object sender, EventArgs e)
    {

        ShowTextForAmount();
        ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "ShowAddDiv();", true);
    }
    protected void txtAmount_TextChanged(object sender, EventArgs e)
    {
        ShowTextForAmount();
        ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "ShowAddDiv();", true);

    }
    protected void btnClear_Click(object sender, EventArgs e)
    {
        clear();
        txtDisName.Focus();
    }

    #endregion





}