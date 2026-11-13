
/*----------------------------------------------------------------------------------------------------------------------------------------------------------
Purpose    : Insert Opening Stock 
Author     : BIBIN TC
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
using WebPharma.Business.WebPharma;
using System.Data;
using System.Globalization;

public partial class OpeningStock : System.Web.UI.Page
{
    protected void BindData()
    {
        Populate populate = new Populate();
        populate.FillDropdownList(ddlStore, "Store", "StoName", "ID_Store", "StoName", " Active='True' AND FK_Client= " + hfClient.Value.Split(',')[0], "");
        populate.FillDropdownList(ddlItem, "Items", "Name", "ID_Items", "Name", "  Active='True' AND ItemType=2 AND FK_Client= " + hfClient.Value.Split(',')[0], "");
        populate.FillDropdownList(ddlUnit, "Units", "Name", "ID_Units", "Name", " FK_Client= " + hfClient.Value.Split(',')[0], "");
        populate = null;
    }
    protected void ddlItemsFill()
    {
        if (ddlItem.SelectedIndex > 0)
        {
            BlItems blitems = new BlItems();
            blitems.MasterId = Convert.ToInt64(ddlItem.SelectedValue.ToString());
            blitems.FK_Client = Convert.ToInt64(hfClient.Value);
            blitems.SelectData();

            ddlUnit.SelectedValue = blitems.FK_Units.ToString();
            txtItemShortName.Text = blitems.Name.ToString();
            //hfPacked.Value = blitems.Packed.ToString();
            //txtPurchaseVat.Text = blitems.Purchasevat.ToString();
         // txtVat.Text = blitems.Purchasevat.ToString();

            blitems = null;

            BlUnit blunit = new BlUnit();
            blunit.MasterId = Convert.ToInt64(ddlUnit.SelectedValue);
            blunit.FK_Client = Convert.ToInt64(hfClient.Value);
            blunit.SelectData();
            //hfUnitQuantity.Value = blunit.NoofUnits.ToString();
            blunit = null;


            LoadPreviousPurchase();
            ddlUnit.Focus();
            ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "CalculatePercentagefromWholeSalePrice();CalculatePercentagefromCardPrice();", true);
            ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "CalculatePercentagefromWholeSalePrice();CalculatePercentagefromCardPrice();", true);


        }
    }
    protected void LoadPreviousPurchase()
    {

        BlMaterialReciept blmaterialreciept = new BlMaterialReciept();
        blmaterialreciept.FK_Client = Convert.ToInt64(hfClient.Value);
        blmaterialreciept.FK_ItemsforPreviousPurchase = Convert.ToInt64(ddlItem.SelectedValue);
        DataTable dtbl = new DataTable();
        dtbl = blmaterialreciept.SelectPreviousPurchaseDetails();

        if (dtbl.Rows.Count > 0)
        {
            GvPreviousPurchase.DataSource = dtbl;
            GvPreviousPurchase.DataBind();
            DivPreviousPurchase.Visible = true;
        }
        else
        {
            GvPreviousPurchase.DataSource = null;
            GvPreviousPurchase.DataBind();
        }
    }
    protected void CheckUserAccess()
    {
        BlUserAccess bluseraccess = new BlUserAccess();
        Populate populate = new Populate();

        bluseraccess.FK_Users = Convert.ToInt64(hfUser.Value);
        bluseraccess.FK_Pages = 79;
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
        BlOpeningStock blopeningstock = new BlOpeningStock();
        //blopeningstock.FK_Items = Convert.ToInt64(ddlItem.SelectedValue.ToString());
        blopeningstock.FK_Store = Convert.ToInt64(ddlStore.SelectedValue.ToString());
        //blopeningstock.BatchNo = txtBatch.Text.ToString();
        //blopeningstock.ExpiryDate = Convert.ToDateTime(txtExpiryDate.Text);
        //blopeningstock.PurRate = Convert.ToDecimal(txtPurchaseRate.Text);
        //blopeningstock.MRP = Convert.ToDecimal(txtMRP.Text);
        //blopeningstock.RetailPrice = Convert.ToDecimal(txtRetailPrice.Text);
        //blopeningstock.WholeSalePrice = Convert.ToDecimal(txtWholeSalePrice.Text);
        //blopeningstock.PrivilagePrice = Convert.ToDecimal(txtPrivilagePrice.Text);
        //blopeningstock.VAT = Convert.ToDecimal(txtVat.Text);
        //blopeningstock.OpeningStock = Convert.ToDecimal(txtOpeningStock.Text);
        //blopeningstock.BarCode = Convert.ToString(txtBarCode.Text);
        blopeningstock.UserCode = hfUser.Value;
        blopeningstock.FK_Client = Convert.ToInt64(hfClient.Value);
        if (hdfOpenStockId.Value == string.Empty)
        {
            if (hfIsAdd.Value.ToLower() == "true")
            {
                status = blopeningstock.InsertData();
                if (status > 0)
                {
                    GridFill(1);
                    GvPreviousPurchase.DataSource = null;
                    GvPreviousPurchase.DataBind();
                    Populate.InsertMsg(this);
                }
                if (status == -2)
                {
                    Populate.ErrorMessageBox(this, "Cannot insert data, Stock Already Exists");
                    txtItemShortName.Focus();
                }
                if (status == -3)
                {
                    Populate.ErrorMessageBox(this, "Cannot insert data, Barcode already exists for another Item");
                    txtBarCode.Focus();
                }
                else
                {
                    Populate.ErrorMessageBox(this, "Cannot insert data");
                    txtItemShortName.Focus();
                }
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "toastr.warning('User does not have permission to add')", true);
                txtItemShortName.Focus();
            }
        }

        blopeningstock = null;
    }
    public void GridFill(int currentPage)
    {
        BlOpeningStock blopeningstock = new BlOpeningStock();
        DataTable dtbl = new DataTable();
       // blopeningstock.ItemName = txtSearchItemName.Text.Trim();
        blopeningstock.PageSize = 10;
        currentPage = currentPage == 0 ? 1 : currentPage;
        blopeningstock.PageIndex = Convert.ToInt16(currentPage);
        blopeningstock.FK_Client = Convert.ToInt64(hfClient.Value);
        blopeningstock.UserCode = hfUser.Value;

        dtbl = blopeningstock.SelectAllData();
        gvOpenStock.DataSource = dtbl;
        gvOpenStock.DataBind();

        if (dtbl.Rows.Count > 0)
        {
            int recordCount = Convert.ToInt32(dtbl.Rows[0]["RecordCount"].ToString());
            this.PopulatePager(recordCount, currentPage);
        }
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
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                btnSubmit.Visible = true;
                hfUser.Value = Request.QueryString["uid"];
                hfClient.Value = Request.QueryString["clnt"];
                Page.Header.DataBind();
                ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "ClearAdd();", true);
                divAdd.Style.Add("display", "block");
                CheckUserAccess();
                BindData();
                GridFill(1);
                txtItemShortName.Focus();
            }
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

    protected void Page_Changed(object sender, EventArgs e)
    {
        try
        {
            int pageIndex = int.Parse((sender as LinkButton).CommandArgument);

            DataTable dtbl = new DataTable();
            BlOpeningStock blopeningstock = new BlOpeningStock();
            //blopeningstock.PageSize = 10;
            //blopeningstock.PageIndex = Convert.ToInt16(pageIndex);
            //GridFill(blopeningstock.PageIndex);
            blopeningstock = null;
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
            if (Convert.ToInt64(ddlItem.SelectedValue) > 0)
            {
                SaveData();
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "toastr.warning('Please Select a Valid Item')", true);
                txtItemShortName.Focus();
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
            GridFill(1);
            txtSearchItemName.Focus();
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "toastr.warning('" + ex.Message + "')", true);
        }
    }
    protected void ddlItem_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            ddlItemsFill();
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "toastr.warning('" + ex.Message + "')", true);
        }
    }
    protected void gvOpenStock_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        try
        {
            ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "ShowAddDiv();", true);
            BlOpeningStock blopeningstock = new BlOpeningStock();
            blopeningstock.MasterId = Convert.ToInt64(gvOpenStock.DataKeys[e.NewSelectedIndex]["ID_OpeningStock"].ToString());
            hdfOpenStockId.Value = blopeningstock.MasterId.ToString();
            blopeningstock.UserCode = hfUser.Value;
            blopeningstock.FK_Client = Convert.ToInt64(hfClient.Value);
            blopeningstock.SelectData();
            //txtItemShortName.Text = blopeningstock.ItemName;
            //ddlItem.SelectedValue = blopeningstock.FK_Items.ToString();
            //ddlStore.SelectedValue = blopeningstock.FK_Store.ToString();
            //txtBatch.Text = blopeningstock.BatchNo;
            //txtExpiryDate.Text = blopeningstock.ExpiryDate.ToShortDateString();
            //txtPurchaseRate.Text = blopeningstock.PurRate.ToString();
            //txtMRP.Text = blopeningstock.MRP.ToString();
            ////txtRetailPrice.Text = blopeningstock.RetailPrice.ToString();
            //txtWholeSalePrice.Text = blopeningstock.WholeSalePrice.ToString();
            //txtPrivilagePrice.Text = blopeningstock.PrivilagePrice.ToString();
            //txtVat.Text = blopeningstock.VAT.ToString();
            //txtOpeningStock.Text = blopeningstock.OpeningStock.ToString();
            //txtBarCode.Text = blopeningstock.BarCode.ToString();
            btnSubmit.Visible = false;
            blopeningstock = null;
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "toastr.warning('" + ex.Message + "')", true);
        }
    }
    protected void imgbtnAdd_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            divAdd.Style.Add("display", "block");
            divView.Style.Add("display", "none");
            ddlItem.SelectedIndex = 0;
            btnSubmit.Visible = true;
            ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", " ShowAddDiv(); ClearAdd(); releasebtn();", true);

        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "toastr.warning('" + ex.Message + "')", true);
        }
    }
}