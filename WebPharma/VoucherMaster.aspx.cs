/*----------------------------------------------------------------------------------------------------------------------------------------------------------
Purpose    : Insert/Update/Delete Voucher 
Author     :Bibin TC
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


public partial class VoucherMaster : System.Web.UI.Page
{
    #region Function
    protected void BindData()
    {
        Populate populate = new Populate();
        populate.FillDropdownList(ddlAcSubHead, "AccountHeadSub", "Name", "ID_AccountHeadSub", "Name", "FK_Client=" + hfClient.Value + "  And Active = 1 ", "");
        populate.FillDropdownList(ddlSearchAccountHead, "AccountHeadSub", "Name", "ID_AccountHeadSub", "Name", "FK_Client=" + hfClient.Value + "  And Active = 1 ", "");
        populate = null;
    }
    protected void GridFill(int currentPage)
    {
        BlVoucher blvouuchr = new BlVoucher();
        blvouuchr.MasterId = 0;
        blvouuchr.ShortName = txtSearchShortName.Text.Trim();
        blvouuchr.Name = txtSearchName.Text.Trim();
        blvouuchr.Type = Convert.ToByte(ddlSearchType.SelectedValue);
        blvouuchr.FK_AccountHeadSub = Convert.ToInt64(ddlSearchAccountHead.SelectedValue);

        blvouuchr.FK_Client = Convert.ToInt32(hfClient.Value);
        blvouuchr.UserCode = hfUser.Value;
        blvouuchr.PageSize = 10;
        currentPage = currentPage == 0 ? 1 : currentPage;
        blvouuchr.PageIndex = Convert.ToInt16(currentPage);
        DataTable dtbl = blvouuchr.SelectAllData();
        gvVoucher.DataSource = dtbl;
        gvVoucher.DataBind();
        if (dtbl.Rows.Count > 0)
        {
           
            int recordCount = Convert.ToInt32(dtbl.Rows[0]["RecordCount"].ToString());
            this.PopulatePager(recordCount, currentPage);
        }
        blvouuchr = null;

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
        BlVoucher blVoucher = new BlVoucher();
        blVoucher.ShortName = txtShortName.Text.Trim();
        blVoucher.Name = txtName.Text.Trim();
        blVoucher.Type = Convert.ToByte(rbtnDiscount.Checked ? 1 : (rbtnReturn.Checked ? 3 : 2));
        if (rbtnDiscount.Checked)
        {
            blVoucher.Amount = Convert.ToDecimal(txtDiscAmount.Text == "" ? "0" : txtDiscAmount.Text.Trim());
            blVoucher.Percentage = Convert.ToDecimal(txtDiscPercentage.Text == "" ? "0" : txtDiscPercentage.Text.Trim());
        }
        else if (rbtnReturn.Checked)
        {
            blVoucher.Amount = 0;
            blVoucher.Percentage = 0;
        }
        else
        {
            blVoucher.Percentage = 0;
            blVoucher.Amount = Convert.ToDecimal(txtAmount.Text == "" ? "0" : txtAmount.Text);
        }
        blVoucher.validity = Convert.ToInt32(txtValidity.Text.Trim());
        blVoucher.FK_AccountHeadSub = Convert.ToInt64(ddlAcSubHead.SelectedValue);
        blVoucher.Remarks = txtRemarks.Text.Trim();
        blVoucher.Active = cbxActive.Checked;
        blVoucher.UserCode = hfUser.Value;
        blVoucher.FK_Client = Convert.ToInt32(hfClient.Value);
        if (hdfVoucherMaster.Value == "")
        {
            if (hfIsAdd.Value.ToLower() == "true")
            {
                status = blVoucher.InsertData();
                if (status > 0)
                {
                    ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "ShowAddDiv();", true);
                    ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "toastr.success('Data inserted successfully');ClearAdd();", true);
                     GridFill(1);

                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "ShowAddDiv();", true);
                    ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "toastr.error('Cannot insert data, Short Name/Name already exists')", true);
                }

            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "toastr.warning('User does not have permission to add')", true);
            }

        }
        else
        {
            if (hfIsUpdate.Value.ToLower() == "true")
            {
                blVoucher.MasterId = Convert.ToInt64(hdfVoucherMaster.Value);
                status = blVoucher.UpdateData();
                if (status > 0)
                {
                    ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "ShowAddDiv();", true);
                    ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "toastr.success('Data updated successfully');ClearAdd();", true);
                     GridFill(1);
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "ShowAddDiv();", true);
                    ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "toastr.error('Cannot update data, Short Name/Name already exists ')", true);

                }
            }

            else
            {
                ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "toastr.warning('User does not have permission to Update')", true);
               
            }
        }

        blVoucher = null;

    }
    #endregion

    #region Events
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Page.Header.DataBind();
            //blMessage.Items.Clear();
            ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "ClearAdd();", true);
            divAdd.Style.Add("display", "block");
            hfUser.Value = Request.QueryString["uid"];
            hfClient.Value = Request.QueryString["clnt"];
            CheckUserAccess();
            BindData();
            hdfVoucherMaster.Value = "";
            hdfIsReferance.Value = "0";
            txtShortName.Focus();
            
        }
    }
    protected void imgbtnView_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            //blMessage.Items.Clear();

            ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "ClearAdd();ShowVeiwDiv();", true);
            divAdd.Style.Add("display", "none");
            divView.Style.Add("display", "block");
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
    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            //blMessage.Items.Clear();
            SaveData();
            txtShortName.Focus();
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "toastr.warning('" + ex.Message + "')", true);
        }
    }
    protected void gvVoucher_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        //blMessage.Items.Clear();
        ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "ShowAddDiv();", true);
        BlVoucher blvoucher = new BlVoucher();
        blvoucher.ShortName = "";
        blvoucher.Name = "";
        blvoucher.Type = 0;
        blvoucher.FK_AccountHeadSub = 0;
        blvoucher.MasterId=Convert.ToInt64(Convert.ToInt64(gvVoucher.DataKeys[e.NewSelectedIndex]["ID_Voucher"].ToString()));
        hdfVoucherMaster.Value = blvoucher.MasterId.ToString();
        blvoucher.FK_Client = Convert.ToInt32(hfClient.Value);
        blvoucher.UserCode = hfUser.Value;
        blvoucher.SelectData();
        txtShortName.Text = blvoucher.ShortName;
        txtName.Text = blvoucher.Name;
        if (blvoucher.Type == 1)
        {
            rbtnDiscount.Checked = true;
            txtDiscAmount.Text = blvoucher.Amount.ToString();
            txtDiscPercentage.Text = blvoucher.Percentage.ToString();
            ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "script", "GetDiscountDetails();", true);
            if (blvoucher.Amount == 0)
            {
                ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "script", "Percentage();", true);
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "script", "Amount();", true);
            }
        }
        else if (blvoucher.Type == 3)
        {
            rbtnReturn.Checked = true;
            ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "script", "GetReturnDetails();", true);
        }
        else
        {
            rbtnGift.Checked = true;
            txtAmount.Text = blvoucher.Amount.ToString();
            ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "script", "GetGiftDetals();", true);
        }
        txtValidity.Text = blvoucher.validity.ToString();
        hdfIsReferance.Value = blvoucher.IsRefrence ? "1" : "0";
       // ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "script", blvoucher.IsRefrence?"SetReadonly();":"RemoveAttribute();", true);
        try
        {
            ddlAcSubHead.SelectedValue = blvoucher.FK_AccountHeadSub.ToString();
        }
        catch(Exception ex)
        {
            ddlAcSubHead.SelectedValue = "0";
        }
        cbxActive.Checked = blvoucher.Active;
        txtRemarks.Text = blvoucher.Remarks;
        blvoucher = null;

    }


    protected void gvVoucher_RowDeleting(object sender, GridViewDeleteEventArgs e)
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
                BlVoucher blvoucher = new BlVoucher();
                blvoucher.MasterId = Convert.ToInt64(gvVoucher.DataKeys[e.RowIndex]["ID_Voucher"].ToString());
                blvoucher.FK_Client = Convert.ToInt64(hfClient.Value);
                blvoucher.UserCode = hfUser.Value;
                if (hfIsDelete.Value.ToLower() == "true")
                {
                    status = blvoucher.DeleteData();
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
                blvoucher = null;
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
             txtSearchShortName.Focus();
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "toastr.warning('" + ex.Message + "')", true);
        }
    }
    #endregion
}