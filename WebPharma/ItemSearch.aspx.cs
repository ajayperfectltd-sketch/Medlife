using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebPharma.Business.Masters;
/*----------------------------------------------------------------------------------------------------------------------------------------------------------
Purpose    : Items View Pop up to search
Author     : Aiswarya
Copyright  : Perfect Software Solutions (Clt) Pvt Ltd
Created on : 03/05/2018
----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
WebPharma
On			By			OMID
----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------


--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------*/
public partial class ItemSearch : System.Web.UI.Page
{
    #region Functions
    protected void BindData()
    {
        Populate populate = new Populate();
        populate.FillDropdownList(ddlsearchUnit, "Units", "UnitName", "ID_Units", "UnitName", " FK_Client= " + hfClient.Value + "AND Cancelled=0", "");
        populate.FillDropdownList(ddlSearchManufacturer, "Manufacturer", "ManufactureName", "ID_Manufacturer", "ManufactureName", "FK_Client=" + hfClient.Value + "AND Cancelled=0", "");
        populate.FillDropdownList(ddlSerachCategory, "Category", "CategoryName", "ID_Category", "CategoryName", " FK_Client= " + hfClient.Value + "AND Cancelled=0", "");
        populate.FillDropdownList(ddlSerachSubUnit, "SubUnit", "SubUnitName", "ID_SubUnits", "SubUnitName"," FK_Client= "+ hfClient.Value + "AND Cancelled=0", "");
        populate.FillDropdownList(ddlSearchSupplier, "Supplier", "SupplierName", "ID_Supplier", "SupplierName", " FK_Client= " + hfClient.Value + "AND Cancelled=0", "");
        populate = null;
        BinSubcategory();
       
    }
    protected void BinSubcategory()
    {
        new Populate().FillDropdownList(ddlSeachSubCategory, "SubCategory", "SubCategoryName", "ID_SubCategory", "SubCategoryName", "FK_Category = " + ddlSerachCategory.SelectedValue + " and FK_Client = " + hfClient.Value + "AND Cancelled=0", "");

    }
    
    protected void GridFill(int currentPage)
    {
        BlItems blItems = new BlItems();
        DataTable dtbl = new DataTable();
        blItems.ShortName = txtSearchShortName.Text.Trim();
        blItems.Name = txtSearchName.Text.Trim();
       blItems.FK_CategoryFirst = Convert.ToInt64(ddlSerachCategory.SelectedValue);
       blItems.FK_CategorySecond = Convert.ToInt64(ddlSeachSubCategory.SelectedValue);
       blItems.FK_Manufacturer = Convert.ToInt64(ddlSearchManufacturer.SelectedValue);
       blItems.FK_Units = Convert.ToInt64(ddlsearchUnit.SelectedValue);
       blItems.FK_SubUnit = Convert.ToInt64(ddlSerachSubUnit.SelectedValue);

       Security security = new Security();
       if (Request.QueryString["Date"] != null)
           blItems.Date = Convert.ToDateTime(security.UrlDecrypt(HttpUtility.UrlDecode(Request.QueryString["Date"])));//Convert.ToDateTime(Request.QueryString["Date"]);
       // blItems.MRP = Convert.ToDecimal(txtMRP.Text == "" ? "0" : txtMRP.Text);
 

        blItems.UserCode = hfUser.Value;
        blItems.PageIndex = Convert.ToInt16(currentPage);
        if (Request.QueryString["Supplier"] != null)
            ddlSearchSupplier.SelectedValue = Convert.ToString(security.UrlDecrypt(HttpUtility.UrlDecode(Request.QueryString["Supplier"]))); //Convert.ToString(Request.QueryString["Supplier"]);
        blItems.FK_Supplier = Convert.ToInt64(ddlSearchSupplier.SelectedValue);
        if (Convert.ToInt64(ddlSearchSupplier.SelectedValue)>0)
            dtbl = blItems.StockItemsSelectPopupFromQuotation();
        else

        dtbl = blItems.StockItemsSelectPopup();
        gvItems.DataSource = dtbl;
        gvItems.DataBind();
        
        blItems = null;
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
            int j = i % 10;
            if (j == 0)
            { i = i - 9; }
            else
            { i = i - j + 1; }
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
            pages.Add(new ListItem("->", (i + 1).ToString(), (pageCount - currentPage) > 10));
            pages.Add(new ListItem("Last", pageCount.ToString(), currentPage < pageCount));
        }




        rptPager.DataSource = pages;
        rptPager.DataBind();
    }
    #endregion
    #region Events
    
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
        
            //hfUser.Value = Request.QueryString["uid"];
            //hfClient.Value = Request.QueryString["clnt"];

            Security security = new Security();

            if (Request.QueryString["uid"] != null)
                hfUser.Value = Request.QueryString["uid"];
            else if (Request.QueryString["uidPop"] != null)
                hfUser.Value = security.UrlDecrypt(HttpUtility.UrlDecode(Request.QueryString["uidPop"]));

            if (Request.QueryString["clnt"] != null)
                hfClient.Value = Request.QueryString["clnt"];
            else if (Request.QueryString["clntPop"] != null)
                hfClient.Value = security.UrlDecrypt(HttpUtility.UrlDecode(Request.QueryString["clntPop"]));

            if (Request.QueryString["itn"] != null)
                txtSearchName.Text = Convert.ToString(security.UrlDecrypt(HttpUtility.UrlDecode(Request.QueryString["itn"]))); //Convert.ToString(Request.QueryString["itn"]);

          
            ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "ClearSearch();", true);
            BindData();

            if (Request.QueryString["ctgry"] != null)
                ddlSerachCategory.SelectedValue = Convert.ToString(security.UrlDecrypt(HttpUtility.UrlDecode(Request.QueryString["ctgry"])));//Convert.ToString(Request.QueryString["ctgry"]);
          //  ddlSearchStore.SelectedValue = Convert.ToString(Request.QueryString["Str"]);
           // ddlSearchSupplier.SelectedValue = Convert.ToString(Request.QueryString["Supplier"]);
            GridFill(1);
           // txtSearchShortName.Focus();
            gvItems.Focus();

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
            ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "toastr.warning('" + ex.Message + "')", true);
        }
    }
    //protected void btnSearchClear_Click(object sender, EventArgs e)
    //{
    //    try
    //    {
    //        ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "ClearSearch();", true);
    //        BindData();
    //        GridFill(1);
    //    }
    //    catch (Exception ex)
    //    {
    //        ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "toastr.warning('" + ex.Message + "')", true);
    //    }
    //}
    protected void Page_Changed(object sender, EventArgs e)
    {
        //blMessage.Items.Clear();
  
        int pageIndex = int.Parse((sender as LinkButton).CommandArgument);
        GridFill(pageIndex);
        //DataTable dtbl = new DataTable();
        //BlItems blItems = new BlItems();
        //blItems.PageSize = 10;
        //blItems.PageIndex = Convert.ToInt16(pageIndex);
        //dtbl = blItems.StockItemsSelectPopup();
        //gvItems.DataSource = dtbl;
        //gvItems.DataBind();
        //if (dtbl.Rows.Count > 0)
        //{
        //    int recordCount = Convert.ToInt32(dtbl.Rows[0]["RecordCount"].ToString());
        //    this.PopulatePager(recordCount, pageIndex);
        //}
        //blItems = null;
    }
    

    protected void btnSubCat_Click(object sender, EventArgs e)
    {
        try
        {



            Populate populate = new Populate();
            populate.FillDropdownList(ddlSeachSubCategory, "SubCategory", "SubCategoryName", "ID_SubCategory", "SubCategoryName", " FK_Client=" + hfClient.Value + " AND FK_Category= " + ddlSerachCategory.SelectedValue.ToString() + "AND Cancelled=0", "");
            populate = null;
            //divAdd.Style.Add("display", "block");
            divView.Style.Add("display", "none");
            ddlSeachSubCategory.Focus();
        }

        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "toastr.warning('" + ex.Message + "')", true);
        }

    }
    #endregion

    protected void ddlSerachCategory_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {



            Populate populate = new Populate();
            populate.FillDropdownList(ddlSeachSubCategory, "SubCategory", "SubCategoryName", "ID_SubCategory", "SubCategoryName", " FK_Client=" + hfClient.Value + " AND FK_Category= " + ddlSerachCategory.SelectedValue.ToString() + "AND Cancelled=0", "");
            populate = null;
            ddlSeachSubCategory.Focus();
        }

        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "toastr.warning('" + ex.Message + "')", true);
        }

    }
    protected void gvItems_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvItems.PageIndex = e.NewPageIndex;
        GridFill(e.NewPageIndex);
    }
}