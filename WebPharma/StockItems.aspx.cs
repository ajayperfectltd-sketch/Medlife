/*----------------------------------------------------------------------------------------------------------------------------------------------------------
Purpose    : Select items with current Stock
Author     :Bibin TC
Copyright  : Perfect Software Solutions (Clt) Pvt Ltd
Created on : 03/05/2018
--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using WebPharma.Business.WebPharma;
using System.IO;
using System.Text;
using System.Security.Cryptography;
using WebPharma.Business.Masters;
using System.Globalization;

public partial class StockItems : System.Web.UI.Page
{


    #region functions
     

    protected void BindData()
    {
        Populate populate = new Populate();
       // populate.FillDropdownList(ddlSearchColor, "Color", "Name", "ID_Color", "ID_Color", " FK_Client= " + hfClient.Value, "");
        populate.FillDropdownList(ddlSearchSupplier, "Supplier", "SupplierName", "ID_Supplier", "ID_Supplier", "Active='True' AND FK_Client= " + hfClient.Value, "");
      //  populate.FillDropdownList(ddlSearchSize, "Size", "Name", "ID_Size", "ID_Size", " FK_Client= " + hfClient.Value, "");
      //  populate.FillDropdownList(ddlSearchStorage, "Storage", "Name", "ID_Storage", "ID_Storage", " FK_Client= " + hfClient.Value, "");
        populate.FillDropdownList(ddlSearchCategoryFirst, "Category", "CategoryName", "ID_Category", "ID_Category", " FK_Client= " + hfClient.Value, "");
        //populate.FillDropdownList(ddlSearchCategorySecond, "CategorySecond", "Name", "ID_CategorySecond", "ID_CategorySecond", " FK_Client= " + hfClient.Value, "");
        //populate.FillDropdownList(ddlSearchCategoryThird, "CategoryThird", "Name", "ID_CategoryThird", "ID_CategoryThird", " FK_Client=" + hfClient.Value, "");
      //  populate.FillDropdownList(ddlSearchBrand, "Brand", "Name", "ID_Brand", "ID_Brand", " FK_Client=" + hfClient.Value, "");
        populate.FillDropdownList(ddlSearchManufacturer, "Manufacturer", "ManufactureName", "ID_Manufacturer", "ID_Manufacturer", "FK_Client=" + hfClient.Value, "");
    }
    protected void SetControls()
    {
        //  txtDob.Attributes.Add("readOnly", "readOnly");
    }

    protected void GridFill(int currentPage)
    {
        BlItems blItems = new BlItems();
        DataTable dtbl = new DataTable();
        blItems.ShortName = txtSearchShortName.Text.Trim();

        blItems.Name =  txtSearchName.Text.Trim();
        blItems.BatchNo =  txtBatch.Text.Trim();
        blItems.FK_CategoryFirst = Convert.ToInt64(ddlSearchCategoryFirst.SelectedValue);
        blItems.FK_Manufacturer = Convert.ToInt64(ddlSearchManufacturer.SelectedValue);
        blItems.FK_Supplier = Convert.ToInt64((hfddlSearchSupplier.Value == "0" || hfddlSearchSupplier.Value == "" || hfddlSearchSupplier.Value == "undefined") ? ddlSearchSupplier.SelectedValue : hfddlSearchSupplier.Value);
        blItems.FK_Client =Convert.ToInt64(hfClient.Value);
        blItems.UserCode = hfUser.Value;
        blItems.FK_Store = Convert.ToInt64(hfStore.Value == "" ? "0" : hfStore.Value);
     
        blItems.MRP = Convert.ToDecimal(txtMRP.Text == "" ? "0" : txtMRP.Text);
       // blItems.ZeroStock = chkZeroStock.Checked;
        blItems.NotReturnable = Convert.ToBoolean(hfNotReturnable.Value == "" ? "False": hfNotReturnable.Value); 
        blItems.ExpiryDate = Convert.ToDateTime(txtSearchExpDate.Text == "" ? "" + DateTime.Now.Date + "" : txtSearchExpDate.Text);  
        blItems.Mode = hfMode.Value;
        dtbl = blItems.SelectCurrentStock();    //blItems.StockItemsSelect();
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
    protected void Page_Changed(object sender, EventArgs e)
    {

        int pageIndex = int.Parse((sender as LinkButton).CommandArgument);

        GridFill(pageIndex);
    }
    #endregion

    #region events
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                
                Session["Attachment"] = null;
                Page.Header.DataBind();
                blMessage.Items.Clear();
                Security security = new Security();
                if (Request.QueryString["uid"] != null)
                    hfUser.Value = security.UrlDecrypt(HttpUtility.UrlDecode(Request.QueryString["uid"])); //Request.QueryString["uid"];
                if (Request.QueryString["clnt"] != null)
                    hfClient.Value = security.UrlDecrypt(HttpUtility.UrlDecode(Request.QueryString["clnt"])); //Request.QueryString["clnt"];
                if (Request.QueryString["str"] != null)
                    hfStore.Value = security.UrlDecrypt(HttpUtility.UrlDecode(Request.QueryString["str"])); //Request.QueryString["str"];
                hfddlSearchSupplier.Value = "0";
                hfNotReturnable.Value = "False";
                BindData();
                //txtBarCode.Text = Convert.ToString(Request.QueryString["itn"]);
                if (Request.QueryString["itn"] != null)
                    txtSearchName.Text = security.UrlDecrypt(HttpUtility.UrlDecode(Request.QueryString["itn"])); //Request.QueryString["itn"].ToString();

                  if (Request.QueryString["ctgry"] != null)
                  {
                      ddlSearchCategoryFirst.SelectedValue = security.UrlDecrypt(HttpUtility.UrlDecode(Request.QueryString["ctgry"])).ToString(); //Request.QueryString["ctgry"].ToString();
                  }
                  if ((Request.QueryString["suplr"]) != null)
                  {
                      hfddlSearchSupplier.Value=security.UrlDecrypt(HttpUtility.UrlDecode(Request.QueryString["suplr"])); //Request.QueryString["suplr"].ToString();                
                      if (hfddlSearchSupplier.Value == "0" || hfddlSearchSupplier.Value == "" || hfddlSearchSupplier.Value == "undefined")
                      {
                          ddlSearchSupplier.Enabled = true;
                      }
                      else 
                      ddlSearchSupplier.Enabled = false; 
                  }
                  if (Request.QueryString["NtRtrn"] != null)
                  {
                      hfNotReturnable.Value = security.UrlDecrypt(HttpUtility.UrlDecode(Request.QueryString["NtRtrn"]));// Request.QueryString["NtRtrn"].ToString();
                  }
                  ScriptManager.RegisterStartupScript(Page, typeof(Page), "script", "ClearSearch();", true);

                  if (Request.QueryString["ctgry"] != null)
                  {
                      ddlSearchCategoryFirst.SelectedValue = security.UrlDecrypt(HttpUtility.UrlDecode(Request.QueryString["ctgry"])).ToString(); //Request.QueryString["ctgry"].ToString();
                  }
                  if (Request.QueryString["expdt"] != null)
                  {
                      txtSearchExpDate.Text = security.UrlDecrypt(HttpUtility.UrlDecode(Request.QueryString["expdt"])).ToString(); //Request.QueryString["ctgry"].ToString();
                  }
                  else
                  {
                      txtSearchExpDate.Text = "";
                  }
                  if (Request.QueryString["Expmode"] != null)
                  {
                      hfMode.Value = security.UrlDecrypt(HttpUtility.UrlDecode(Request.QueryString["Expmode"])).ToString(); 
                  }
                 
                        GridFill(1);
                        SetControls();
                        gvItems.Focus();
             
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "toastr.warning('" + ex.Message + "')", true);
        }
    }






    protected void gvItems_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        //hfId_Items.Value = gvItems.DataKeys[e.NewSelectedIndex]["ID_Items"].ToString();
        //hfStockCode.Value = gvItems.DataKeys[e.NewSelectedIndex]["StockCode"].ToString();

        //ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "closeModel();", true);

    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        try
        {
             
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

    #endregion

    protected void gvItems_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvItems.PageIndex = e.NewPageIndex;
        GridFill(e.NewPageIndex); 
    }
}