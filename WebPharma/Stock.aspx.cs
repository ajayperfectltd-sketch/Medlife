/*----------------------------------------------------------------------------------------------------------------------------------------------------------
Purpose    : Insert/Update/ Stock
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
using WebPharma.Business.WebPharma;
using WebPharma.Business.Masters;
using System.Data;
using System.Collections;
using System.Globalization;


public partial class Stock : System.Web.UI.Page
{
    #region Functions
    protected void BindData()
    {
        Populate populate = new Populate();
        populate.FillDropdownList(ddlStore, "Store", "StoName", "ID_Store", "StoName", "     Active='True'   and FK_Client = " + hfClient.Value, "");
        populate.FillDropdownList(ddlCategory, "CategoryFirst", "Name", "ID_CategoryFirst", "Name", "  FK_Client= " + hfClient.Value.Split(',')[0], "");
        populate = null;
        ddlStore.Focus();
    }
 
    
    protected void CheckUserAccess()
    {
        BlUserAccess bluseraccess = new BlUserAccess();
        Populate populate = new Populate();

        bluseraccess.FK_Users = Convert.ToInt64(hfUser.Value);
        bluseraccess.FK_Pages = 34;
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
    public void ClearAll()
    {
        gvItems.DataSource = null;
        gvItems.DataBind();
        divAdd.Style.Add("display", "block;");

        ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "ClearAdd();", true);
    }
    protected void Save()
    {
        long Status = 0;
        Boolean Itemchanged = false;
        if (hfIsUpdate.Value.ToLower() == "true")
        {
            BlStockCorrection blStockCorrection = new BlStockCorrection();

            ArrayList arlist = new ArrayList();
            StockCorrectionItems stockCorrectionItems;
            for (int i = 0; i < gvItems.Rows.Count; i++)
            {
                stockCorrectionItems = new StockCorrectionItems();
                stockCorrectionItems.FK_Stock = Convert.ToInt64(gvItems.DataKeys[i]["ID_Stock"].ToString());
                stockCorrectionItems.ItmOldStock = Convert.ToDecimal(gvItems.DataKeys[i]["CurrentStock"].ToString());
                TextBox txtCurrentStock = (TextBox)gvItems.Rows[i].FindControl("txtCurrentStock");
                stockCorrectionItems.ItmNewStock = Convert.ToDecimal(txtCurrentStock.Text);

                if ((stockCorrectionItems.ItmOldStock - stockCorrectionItems.ItmNewStock) != 0)
                {
                    Itemchanged = true;
                }
                arlist.Add(stockCorrectionItems);
                stockCorrectionItems = null;
            }
            if (Itemchanged)
            {
                blStockCorrection.UserCode = hfUser.Value;
                blStockCorrection.xmlStockCorrectionItems = Common.WriteToXml(arlist);
                blStockCorrection.FK_Client = Convert.ToInt64(hfClient.Value);
                blStockCorrection.FK_Store = Convert.ToInt64(ddlStore.SelectedValue);
                Status = blStockCorrection.InsertData();
                if (Status > 0)
                {
                    ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "ShowAddDiv();", true);
                    ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "toastr.success('Data saved successfully');", true);

                    ProcessBind();
                    // GridFill(1);
                    ddlStore.Focus();
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "ShowAddDiv();", true);
                    ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "toastr.error('Cannot save data')", true);

                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "alertify.alert('No item changed from the grid')", true);
            }
        }
        else
        {
            ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "toastr.warning('User does not have permission to Update')", true);

        }

    }
    protected void ProcessBind(int currentPage=1)
    {
        BlStock blStock = new BlStock();
        DataTable dtbl = new DataTable();
        blStock.FK_Items = Convert.ToInt64(hfFK_Items.Value);
        blStock.FK_Store = Convert.ToInt64(ddlStore.SelectedValue);
        blStock.FK_Client = Convert.ToInt64(hfClient.Value);
        blStock.FK_Category = Convert.ToInt64(ddlCategory.SelectedValue);
        blStock.PageSize = 100;
        blStock.PageIndex = Convert.ToInt16(currentPage);
        dtbl = blStock.SelectAllStockStockCorrection();
        gvItems.DataSource = dtbl;
        gvItems.DataBind();
        if (dtbl.Rows.Count > 0)
        {
            int recordCount = Convert.ToInt32(dtbl.Rows[0]["RecordCount"].ToString());
            this.PopulatePager(recordCount, currentPage);
        }
        else
        {
            this.PopulatePager(0, 0);
        
        }


        blStock = null;
        gvItems.Focus();
    }
    private void PopulatePager(int recordCount, int currentPage)
    {
        double dblPageCount = (double)((decimal)recordCount / 100);
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
    #endregion
    #region Events
    protected void Page_Changed(object sender, EventArgs e)
    {
     

        int pageIndex = int.Parse((sender as LinkButton).CommandArgument);
        ProcessBind(pageIndex);
     
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {

            if (gvItems.Rows.Count > 0)
            {


                Save();
            }
            else
            {

                ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "alertify.alert('Atleast one item required')", true);
            }
        }
        catch (Exception ex)
        {
            Populate.WarningMsg(this, ex.ToString());
        }

    }
    protected void btnClear_Click1(object sender, EventArgs e)
    {

        ClearAll();
    }
    protected void imgbtnAdd_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            ClearAll();
        }
        catch (Exception ex)
        {
            Populate.WarningMsg(this, ex.ToString());
        }
    }
    protected void btnProcess_Click(object sender, EventArgs e)
    {

        try
        {
            
            ProcessBind();
                divAdd.Style.Add("display", "block");
            
        }
        catch (Exception ex)
        {

        }


    }
  
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {


                hfUser.Value = Request.QueryString["uid"];
                hfClient.Value = Request.QueryString["clnt"];

                ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "ClearAdd();", true);
                divAdd.Style.Add("display", "block;");
                CheckUserAccess();

                BindData();

            }
        }
        catch (Exception ex)
        {
            Populate.WarningMsg(this, ex.ToString());
        }
    }
    #endregion
}