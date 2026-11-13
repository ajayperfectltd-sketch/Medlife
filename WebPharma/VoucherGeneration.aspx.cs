/*----------------------------------------------------------------------------------------------------------------------------------------------------------
Purpose    : Insert/Update/Delete Voucher Generation
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
public partial class VoucherGeneration : System.Web.UI.Page
{
    #region Functions
    protected void BindData()
    {
        Populate populate = new Populate();
        populate.FillDropdownList(ddlVoucher, "Voucher Inner  join AccountHeadSub On Voucher.FK_AccountHeadSub=ID_AccountHeadSub And AccountHeadSub.Active=1", "Voucher.Name", "ID_Voucher", "ID_Voucher", "Voucher.Active='True' And Type<>3  AND Voucher.FK_Client= " + hfClient.Value, "");
        populate.FillDropdownList(ddlVoucherMasterSearch, "Voucher", "Name", "ID_Voucher", "ID_Voucher", "Active='True' And Type<>3  AND FK_Client= " + hfClient.Value.Split(',')[0], "");
        populate = null;

    }
    protected void GridFill(int currentPage)
    {
        BlVoucherGeneration blvouchergen = new BlVoucherGeneration();
        DataTable dtbl = new DataTable();
        blvouchergen.FK_Client = Convert.ToInt64(hfClient.Value);
        blvouchergen.UserCode = hfUser.Value;
        blvouchergen.ValidFrom = DateTime.ParseExact(txtDateFrom.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
        blvouchergen.ValidUpto = DateTime.ParseExact(txtDateTo.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
        blvouchergen.IsUsed = Convert.ToByte(ddlUsed.SelectedValue);
        blvouchergen.IsUsed =Convert.ToByte( blvouchergen.IsUsed == 0 ? 2 : (blvouchergen.IsUsed == 1 ? 0 : 1));
        blvouchergen.VoucherNo = txtVoucherNo.Text.Trim();
        blvouchergen.FK_Voucher = Convert.ToInt64(ddlVoucherMasterSearch.SelectedValue);
        blvouchergen.VoucherType = Convert.ToByte(ddlDiscountType.SelectedValue);
        blvouchergen.PageSize = 10;
        currentPage = currentPage == 0 ? 1 : currentPage;
        blvouchergen.PageIndex = Convert.ToInt16(currentPage);

        dtbl = blvouchergen.SelectAllData();
        gvVoucherGenration.DataSource = dtbl;
        gvVoucherGenration.DataBind();
        if (dtbl.Rows.Count > 0)
        {
            int recordCount = Convert.ToInt32(dtbl.Rows[0]["RecordCount"].ToString());
            this.PopulatePager(recordCount, currentPage);
        }
        blvouchergen = null;

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
        string status = "";
        BlVoucherGeneration blvoucherge = new BlVoucherGeneration();
        blvoucherge.FK_Voucher = Convert.ToInt64(ddlVoucher.SelectedValue);
        blvoucherge.ValidFrom = DateTime.ParseExact(txtValidFrom.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
      
        blvoucherge.UserCode = hfUser.Value;
        blvoucherge.EnteredOn = DateTime.Now;
        blvoucherge.FK_Client = Convert.ToInt64(hfClient.Value);
        blvoucherge.Nos = Convert.ToInt64(txtNoofCopies.Text == "" ? "0" : txtNoofCopies.Text);
        if (hdfVG.Value == "")
        {
            if (hfIsAdd.Value.ToLower() == "true")
            {
                status = blvoucherge.InsertData();
                if (status != "")
                {
                    ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "ShowAddDiv();", true);
                   // ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "toastr.success('Data inserted successfully');ClearAdd();", true);
                    Populate.MessageBox(this, "Voucher Generated From " + status);
                    GridFill(1);

                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "ShowAddDiv();", true);
                    ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "toastr.error('Cannot insert data')", true);
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
                blvoucherge.MasterId = Convert.ToInt64(hdfVG.Value);
                status = blvoucherge.UpdateData();
                if (status !="")
                {
                    ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "ShowAddDiv();", true);
                    ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "toastr.success('Data updated successfully');ClearAdd();", true);
                     GridFill(1);
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "ShowAddDiv();", true);
                    ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "toastr.error('Cannot update data, Short Name already exists ')", true);

                }
            }
        }
        blvoucherge = null;
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
            hdfVG.Value = "";
            txtValidFrom.Attributes.Add("readonly", "readonly");
            txtValidUpto.Attributes.Add("readonly", "readonly");
            txtDateFrom.Attributes.Add("readonly", "readonly");
            txtDateTo.Attributes.Add("readonly", "readonly");

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
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "toastr.warning('" + ex.Message + "')", true);
        }
    }
    protected void gvVoucherGenration_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        //blMessage.Items.Clear();
        ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "ShowAddDiv();", true);
        BlVoucherGeneration blvouchergen = new BlVoucherGeneration();
        blvouchergen.MasterId = Convert.ToInt64(Convert.ToInt64(gvVoucherGenration.DataKeys[e.NewSelectedIndex]["ID_VoucherGeneration"].ToString()));
        blvouchergen.UserCode = hfUser.Value;
        blvouchergen.FK_Client = Convert.ToInt64(hfClient.Value);
        hdfVG.Value = blvouchergen.MasterId.ToString();
        blvouchergen.ValidFrom = DateTime.Parse("1/1/1900");
        blvouchergen.ValidUpto = DateTime.MaxValue;
        blvouchergen.IsUsed = 2;
        blvouchergen.VoucherNo = "";
        blvouchergen.SelectData();
        try
        {
            ddlVoucher.SelectedValue = blvouchergen.FK_Voucher.ToString();
        }
        catch(Exception ex)
        {
            ddlVoucher.SelectedValue = "0";
        }

        txtValidFrom.Text = blvouchergen.ValidFrom.ToString("dd/MM/yyyy");
        txtNoofCopies.Text = blvouchergen.Nos.ToString();


       


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
            ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "toastr.warning('" + ex.Message + "')", true);
        }
    }
    protected void gvVoucherGenration_RowDeleting(object sender, GridViewDeleteEventArgs e)
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
                BlVoucherGeneration blvouchergen=new BlVoucherGeneration();
                 blvouchergen.MasterId = Convert.ToInt64(Convert.ToInt64(gvVoucherGenration.DataKeys[e.RowIndex]["ID_VoucherGeneration"].ToString()));
                 blvouchergen.FK_Client = Convert.ToInt64(hfClient.Value);
                 blvouchergen.UserCode = hfUser.Value;
                if (hfIsDelete.Value.ToLower() == "true")
                {
                    status = blvouchergen.DeleteData();
                    if (status > 0)
                    {
                         GridFill(1);
                        ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "toastr.success('Data deleted successfully')", true);
                    }
                    else
                    {
                        ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "toastr.error('Cannot delete data, Voucher has used')", true);
                    }
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "toastr.warning('User does not have permission to Delete')", true);
                }
                blvouchergen = null;
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "toastr.warning('" + ex.Message + "')", true);
        }
    }
    protected void btnHiddenFromDateTrigger_Click(object sender, EventArgs e)
    {
        try
        {
            if (ddlVoucher.SelectedIndex > 0)
            {
                BlVoucherGeneration blvoucher = new BlVoucherGeneration();
                blvoucher.FK_Voucher = Convert.ToInt64(ddlVoucher.SelectedValue);
                hdfDate.Value = "";

                long status = blvoucher.VoucherValidUptoDate();
                hdfDate.Value = status.ToString();
                txtValidUpto.Text = DateTime.ParseExact(txtValidFrom.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture).AddDays(status-1).ToString("dd/MM/yyyy");
                blvoucher = null;
            }
            else
            {
                hdfDate.Value = "";
                hdfDate.Value = "0";
            }
            txtValidFrom.Focus();
            ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "ShowAddDiv();", true);
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "toastr.warning('" + ex.Message + "')", true);
        }

    }
    protected void btnTextTrigger_Click(object sender, EventArgs e)
    {
        if (hdfDate.Value != null && hdfDate.Value != "")
        {
            long status = Convert.ToInt64(hdfDate.Value.Split('/')[0]);
            if (status != 0)
            {
                txtValidUpto.Text = DateTime.ParseExact(txtValidFrom.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture).AddDays(status-1).ToString("dd/MM/yyyy");
            }
        }
        txtNoofCopies.Focus();
        ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "ShowAddDiv();", true);
    }
    #endregion
}