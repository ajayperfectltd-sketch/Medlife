/*----------------------------------------------------------------------------------------------------------------------------------------------------------
Purpose    : Insert/Update/Delete Stock adjustment 
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

public partial class StockAdjustment : System.Web.UI.Page
{
    protected void BindData()
    {
        Populate populate = new Populate();
        populate.FillDropdownList(ddlStore, "Store", "StoName", "ID_Store", "StoName", "     Active='True'   and FK_Client = " + hfClient.Value, "");
        populate.FillDropdownList(ddlCategory, "CategoryFirst", "Name", "ID_CategoryFirst", "Name", "  FK_Client= " + hfClient.Value.Split(',')[0], "");
        populate.FillDropdownList(ddlCategorySearch, "CategoryFirst", "Name", "ID_CategoryFirst", "Name", "  FK_Client= " + hfClient.Value.Split(',')[0], "");
        populate.FillDropdownList(ddlStoreSearch, "Store", "StoName", "ID_Store", "StoName", "     Active='True'   and FK_Client = " + hfClient.Value, "");
        populate = null;

        


    }
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {


                hfUser.Value = Request.QueryString["uid"];
                hfClient.Value = Request.QueryString["clnt"];
                hfUserType.Value = Request.QueryString["utyp"].ToString();
                ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "ClearAdd();", true);
                divAdd.Style.Add("display", "block;");
                divView.Style.Add("display", "none;");
                CheckUserAccess();


                BindData();

                SetReadonly();
                GridFill(1);
            }
        }
        catch (Exception ex)
        {
            Populate.WarningMsg(this, ex.ToString());
        }
    }
    protected void CheckUserAccess()
    {
        BlUserAccess bluseraccess = new BlUserAccess();
        Populate populate = new Populate();

        bluseraccess.FK_Users = Convert.ToInt64(hfUser.Value);
        bluseraccess.FK_Pages = 69;
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
        BlStockAdjustment blStockAdjustment = new BlStockAdjustment();
        DataTable dtbl = new DataTable();
        // long MasterId = Convert.ToInt64(gvPurchase.DataKeys[e.NewSelectedIndex]["ID_Purchase"].ToString());
        blStockAdjustment.MasterId = 0;

        blStockAdjustment.FromDate = Convert.ToDateTime(txtFromDate.Text);
        blStockAdjustment.ToDate = Convert.ToDateTime(txtToDate.Text); 

        blStockAdjustment.FK_Store = Convert.ToInt64(ddlStoreSearch.SelectedValue);
        blStockAdjustment.FK_Category = Convert.ToInt64(ddlCategorySearch.SelectedValue);
        blStockAdjustment.FK_Client = Convert.ToInt64(hfClient.Value);

        currentPage = currentPage == 0 ? 1 : currentPage;
        blStockAdjustment.PageIndex = Convert.ToInt16(currentPage);
        dtbl = blStockAdjustment.SelectAllData();
        gvStockAdjustment.DataSource = dtbl;
        gvStockAdjustment.DataBind();
        if (dtbl.Rows.Count > 0)
        {
            int recordCount = Convert.ToInt32(dtbl.Rows[0]["RecordCount"].ToString());
            this.PopulatePager(recordCount, currentPage);

        }
        blStockAdjustment = null;

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
 


    protected void gvStockAdjustment_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        //if (e.CommandName == "Approve")
        //{
        //    try
        //    {
        //        string confirmValue = Request.Form["confirm_value"];
        //        if (confirmValue.Length > 3)
        //            confirmValue = confirmValue.Substring(confirmValue.Length - 3, 3);
        //        if (confirmValue == "Yes")
        //        {
        //            GridViewRow gr = (GridViewRow)((ImageButton)e.CommandSource).NamingContainer;
        //            int index = gr.RowIndex;


        //            long status = 0;
        //            BlltemConversion blltemConversion = new BlltemConversion();
        //            blltemConversion.MasterId = Convert.ToInt64(gvItemConversion.DataKeys[index]["ID_Indent"].ToString());
        //            blltemConversion.FK_Client = Convert.ToInt64(hfClient.Value);
        //            blltemConversion.UserCode = hfUser.Value;
        //            if (hfIsUpdate.Value.ToLower() == "true")
        //            {
        //                Boolean approved = Convert.ToBoolean(gvItemConversion.DataKeys[index]["Approved"].ToString());
        //                if (approved == false)
        //                {
        //                    status = blltemConversion.IndentApprove();
        //                    if (status > 0)
        //                    {
        //                        GridFill(1);
        //                        ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "toastr.success('Data approved successfully')", true);
        //                    }
        //                    else
        //                    {
        //                        ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "toastr.success('Can not approve')", true);
        //                    }
        //                }
        //                else
        //                {
        //                    ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "toastr.success('Already approved ')", true);
        //                }
        //            }
        //            else
        //            {
        //                ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "toastr.warning('User does not have permission to Approve')", true);
        //            }
        //            blltemConversion = null;

        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Populate.WarningMsg(this, ex.ToString());
        //    }


        //}
    }
    
    protected void imgbtnView_Click(object sender, ImageClickEventArgs e)
    {

        try
        {

            divAdd.Style.Add("display", "none");
            divView.Style.Add("display", "block");
            ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "ClearAdd();ClearSearch();", true);
            GridFill(1);
        }
        catch (Exception ex)
        {
            Populate.WarningMsg(this, ex.ToString());
        }
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
    public void ClearAll()
    {

        gvItems.DataSource = null;
        gvItems.DataBind();
        gvItems.Enabled = true;
        txtDate.Enabled = true;
        ddlCategory.Enabled = true;
        ddlStore.Enabled = true;
        btnProcess.Enabled = true;
        btnSave.Visible = true;
        divAdd.Style.Add("display", "block;");
        divView.Style.Add("display", "none;");
        ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "ClearAdd();", true);
    }
    protected void Disableitems()
    {
        gvItems.Enabled = false;
        txtDate.Enabled = false;
        ddlCategory.Enabled = false;
        ddlStore.Enabled = false;
        btnProcess.Enabled = false;
        btnSave.Visible = false;
    }
    protected void btnProcess_Click(object sender, EventArgs e)
    {
        if (hfUserType.Value == "True")
        {
            ddlCategory.Enabled = false;
            ddlStore.Enabled = false;
            //txtDate.Enabled = false;
            ProcessBind();
        }
        else
        {
            ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "alertify.alert('User does not have permission for Stock adjustment ')", true);
        }
       
    }
    protected void ProcessBind()
    {
        BlStockAdjustment blStockAdjustment = new BlStockAdjustment();
        DataTable dtbl = new DataTable();
       
        blStockAdjustment.FK_Store = Convert.ToInt64(ddlStore.SelectedValue);
        blStockAdjustment.FK_Category = Convert.ToInt64(ddlCategory.SelectedValue);
        blStockAdjustment.FK_Client = Convert.ToInt64(hfClient.Value);
        blStockAdjustment.FK_Item = Convert.ToInt64(hfItemValue.Value);
        blStockAdjustment.Name = txtItemShortName.Text.ToString();
        dtbl = blStockAdjustment.SelectStockAdjustmentItem();
        gvItems.DataSource = dtbl;
        gvItems.DataBind();
        blStockAdjustment = null;


    }
   
    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            System.Threading.Thread.Sleep(10);
            if (gvItems.Rows.Count > 0)
            {

                System.Threading.Thread.Sleep(10);
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
    protected void Save()
    {
      long Status = 0;
      Boolean Itemchecked = false;

        if (hfIsUpdate.Value.ToLower() == "true")
        {


            BlStockAdjustment blStockAdjustment = new BlStockAdjustment();

            blStockAdjustment.ApprovedBy = hfUser.Value;
            ArrayList arlist = new ArrayList();
            StockAdjustmentItems stockAdjustmentItems;
            for (int i = 0; i < gvItems.Rows.Count; i++)
            {
                CheckBox chkSelect = (CheckBox)gvItems.Rows[i].FindControl("chkSelectitem");
                if (chkSelect.Checked)
                {
                    stockAdjustmentItems = new StockAdjustmentItems();
                    stockAdjustmentItems.ID_StockAdjustment = Convert.ToInt64(gvItems.DataKeys[i]["ID_StockAdjustment"].ToString());
                    stockAdjustmentItems.FK_Module = Convert.ToInt64(gvItems.DataKeys[i]["FK_Module"].ToString());
                    HiddenField hdfStockFrom = (HiddenField)gvItems.Rows[i].FindControl("hdfStockFrom");
                    HiddenField hdfStockTo = (HiddenField)gvItems.Rows[i].FindControl("hdfStockTo");


                    HiddenField hdfStoreFROM = (HiddenField)gvItems.Rows[i].FindControl("hdfStoreFROM");
                    HiddenField hdfStoreTo = (HiddenField)gvItems.Rows[i].FindControl("hdfStoreTo");
                    stockAdjustmentItems.FK_StockFrom = Convert.ToInt64(hdfStockFrom.Value);
                    stockAdjustmentItems.FK_StockTo = Convert.ToInt64(hdfStockTo.Value);

                    stockAdjustmentItems.FK_StoreTo = Convert.ToInt64(hdfStoreTo.Value);
                    stockAdjustmentItems.FK_StoreFrom = Convert.ToInt64(hdfStoreFROM.Value);
                    Label lblSalePriceFrom = (Label)gvItems.Rows[i].FindControl("lblSalePriceFrom");
                    stockAdjustmentItems.SalesPriceFrom = Convert.ToDecimal((lblSalePriceFrom.Text == "" ? "0" : lblSalePriceFrom.Text));

                    //Label lblSalePriceTo = (Label)gvItems.Rows[i].FindControl("lblSalePriceTo");
                    //stockAdjustmentItems.SalesPriceTo = Convert.ToDecimal((lblSalePriceTo.Text == "" ? "0" : lblSalePriceTo.Text));
                  
                    Label lblQunatity = (Label)gvItems.Rows[i].FindControl("lblQunatity");
                    Label lblAdjustmentQuantity = (Label)gvItems.Rows[i].FindControl("lblAdjustmentQuantity");
                    stockAdjustmentItems.Quantity = Convert.ToDecimal(lblQunatity.Text);
                    stockAdjustmentItems.AdjustmentQuantity = Convert.ToDecimal(lblAdjustmentQuantity.Text);
                    stockAdjustmentItems.Module = ((Label)gvItems.Rows[i].FindControl("lblModule")).Text;
                    string FK_Details = ((HiddenField)gvItems.Rows[i].FindControl("hdfFK_Details")).Value;
                    stockAdjustmentItems.FK_Details = Convert.ToInt64((FK_Details == "" ? "0" : FK_Details));
                    
                    arlist.Add(stockAdjustmentItems);
                    stockAdjustmentItems = null;
                    Itemchecked = true;
                }
            }
            if (Itemchecked)
            {
                blStockAdjustment.XmlStockAdjustmentItems = Common.WriteToXml(arlist);
                blStockAdjustment.ApprovedOn = DateTime.ParseExact(txtDate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                blStockAdjustment.ApprovedBy = hfUser.Value;
                blStockAdjustment.TransDate = DateTime.ParseExact(txtDate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                blStockAdjustment.EnteredOn = DateTime.ParseExact(txtDate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                blStockAdjustment.FK_Client = Convert.ToInt64(hfClient.Value);
                string AdjustmentID = gvItems.DataKeys[0]["AdjustmentID"].ToString();
                blStockAdjustment.AdjustmentID = Convert.ToInt64((AdjustmentID == "") ? "0" : AdjustmentID);
                blStockAdjustment.FK_Category = Convert.ToInt64(ddlCategory.SelectedValue);
                blStockAdjustment.FK_Store = Convert.ToInt64(ddlStore.SelectedValue);
                Status = blStockAdjustment.UpdateData();
                if (Status > 0)
                {
                    ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "ShowAddDiv();", true);
                    ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "toastr.success('Data updated successfully');HideProgress();", true);
                    ProcessBind();
                    // GridFill(1);

                   
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "ShowAddDiv();", true);
                    ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "toastr.error('Cannot update data')", true);

                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "alertify.alert('Check Atleast one item from the grid ')", true);
            }
        }
        else
        {
            ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "toastr.warning('User does not have permission to Update')", true);
           
        }

    }
    protected void btnClear_Click1(object sender, EventArgs e)
    {

        ClearAll();
    }
    protected void gvStockAdjustment_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        BlStockAdjustment blStockAdjustment = new BlStockAdjustment();
        DataTable dtbl = new DataTable();
        string Transdate= gvStockAdjustment.DataKeys[e.NewSelectedIndex]["Transdate"].ToString();
        string FK_Store = gvStockAdjustment.DataKeys[e.NewSelectedIndex]["FK_Store"].ToString();        
        string FK_Category = gvStockAdjustment.DataKeys[e.NewSelectedIndex]["FK_Category"].ToString();
      
        blStockAdjustment.FK_Store = Convert.ToInt64(FK_Store);
        blStockAdjustment.FK_Category = Convert.ToInt64(FK_Category);
        blStockAdjustment.AdjustmentID = Convert.ToInt64(gvStockAdjustment.DataKeys[e.NewSelectedIndex]["AdjustmentID"].ToString());
        blStockAdjustment.FK_Client = Convert.ToInt64(hfClient.Value);
        dtbl = blStockAdjustment.SelectStockAdjustmentItem();
        gvItems.DataSource = dtbl;
        gvItems.DataBind();
        txtDate.Text =  Transdate;
        ddlStore.SelectedValue = FK_Store;
        ddlCategory.SelectedValue = FK_Category;

        blStockAdjustment = null;
        divAdd.Style.Add("display", "block");
        divView.Style.Add("display", "none;");
      
        Disableitems();
    }
   
    public void SetReadonly()
    {

        txtDate.Attributes.Add("readonly", "readonly");
        txtFromDate.Attributes.Add("readonly", "readonly");
        txtToDate.Attributes.Add("readonly", "readonly");
    }
  protected void btnSearch_Click(object sender, EventArgs e)
    {
        try
        {
            GridFill(1);
        }
        catch (Exception ex)
        {
            Populate.WarningMsg(this, ex.ToString());
        }

    }
    protected void Page_Changed(object sender, EventArgs e)
    {
        
        int pageIndex = int.Parse((sender as LinkButton).CommandArgument);
        DataTable dtbl = new DataTable();
        BlStockAdjustment blStockAdjustment = new BlStockAdjustment();
        blStockAdjustment.PageSize = 10;
        blStockAdjustment.PageIndex = Convert.ToInt16(pageIndex);
        GridFill(blStockAdjustment.PageIndex);
        blStockAdjustment = null;
    }
    public void SetDetails(string strName, string strRefno)
    {
        if (CommonPopup1.CallingFunction == "StockAdjustmentFill")
        {
            //txtPurRefno.Text = strRefno;
            //txtPurBillNo.Text = strName;

            HiddenField hdfItem = (HiddenField)gvItems.Rows[int.Parse(hfgvItemsRowIndex.Value)].FindControl("hdfItem");

            string[] words = strRefno.Split('_');
            string ID_Stock = words[0].ToString();
            string FK_Store = words[1].ToString();

            BlStock blStock = new BlStock();
            blStock.FK_Client = Convert.ToInt64(hfClient.Value.Split(',')[0]);
            blStock.FK_Items = Convert.ToInt64(hdfItem.Value);
            blStock.MasterId = Convert.ToInt64(ID_Stock);
            blStock.FK_Store = Convert.ToInt64(FK_Store);
            blStock.SelectData();

            //string RetailPrice = blStock.RetailPrice.ToString();
            string CurrentStock = blStock.CurrentStock.ToString();
            string BatchNo = blStock.BatchNo.ToString();


            HiddenField hdfStockFrom = (HiddenField)gvItems.Rows[int.Parse(hfgvItemsRowIndex.Value)].FindControl("hdfStockFrom");
            hdfStockFrom.Value = ID_Stock;






            HiddenField hdfStoreFROM = (HiddenField)gvItems.Rows[int.Parse(hfgvItemsRowIndex.Value)].FindControl("hdfStoreFROM");
            hdfStoreFROM.Value = FK_Store;

            Label lblSalePriceFrom = (Label)gvItems.Rows[int.Parse(hfgvItemsRowIndex.Value)].FindControl("lblSalePriceFrom");
           // lblSalePriceFrom.Text = RetailPrice;

            Label lblAdjustmentQuantity = (Label)gvItems.Rows[int.Parse(hfgvItemsRowIndex.Value)].FindControl("lblAdjustmentQuantity");
            lblAdjustmentQuantity.Text = CurrentStock;

            TextBox txtbatch = (TextBox)gvItems.Rows[int.Parse(hfgvItemsRowIndex.Value)].FindControl("txtbatch");
            txtbatch.Text = BatchNo;
           hfgvItemsRowIndex.Value = "";
            //hfitem, hfstock and client get data and fill to grid row

            //ClearItems();
            //if (txtPurRefno.Text != "")
            //{
            //    BindPurchaseItems(Convert.ToInt64(txtPurRefno.Text));
            //    Supplier();
            //    //txtRefNo.Focus();
            //}
            //else
            //{
            //    gvPurchaseItems.DataSource = null;
            //    gvPurchaseItems.DataBind();
            //    ViewState[PurchaseReturnItems] = null;
            //}

        }
        else
        {
            //txtSupRefno.Text = strRefno;
            //txtSupplier.Text = strName;

        }
    }
    public void StockAdjustmentFill(int pageIndex)
    {
       // ClearPurchase();
        Populate Populate = new Populate();
        CommonPopup1.CallingFunction = "StockAdjustmentFill";
        string strName = ((TextBox)CommonPopup1.FindControl("txtName")).Text;
        string Refno = ((TextBox)CommonPopup1.FindControl("txtRefno")).Text;
        string searchCol = ((HiddenField)CommonPopup1.FindControl("hfColumn")).Value;
        searchCol = searchCol == string.Empty ? "[Name]" : "[" + searchCol + "]";
        Refno = Refno == string.Empty || Refno == null ? "0" : Refno;
        GridViewRow gr = (GridViewRow)gvItems.Rows[int.Parse(hfgvItemsRowIndex.Value)];
        HiddenField hdfItem = (HiddenField)gr.FindControl("hdfItem");
   //     Populate.FillGrid((GridView)CommonPopup1.FindControl("gdvSearch"), "Purchase ", "ID_Purchase as ID,'[' + CONVERT(VARCHAR(14),BillNo)  +'][' + CONVERT(VARCHAR(14),BillDate,103) +']' as Name,(select Name from  Supplier where ID_Supplier=FK_Supplier) as Supplier,InvoiceNo,NetAmount, ID_Purchase As [Ref No]", "ID_Purchase", "Cancelled='False' and  Approved=1 and Purchase.FK_Client=" + hfClient.Value.Split(',')[0] + " and FK_Store=" + ddlStore.SelectedValue + "AND (ID_Purchase =" + Refno + " OR 0=" + Refno + ")  AND BillNo LIKE '" + strName + "%'", pageIndex, 10, "", (Repeater)CommonPopup1.FindControl("rptPager"), this.Page);
        Populate.FillGrid((GridView)CommonPopup1.FindControl("gdvSearch"), "Stock s inner join Items i on s.FK_Items=i.ID_Items inner join Store st on s.FK_Store=st.ID_Store",
            "(CONVERT(VARCHAR(max),s.ID_Stock) +'_' + CONVERT(VARCHAR(max), s.FK_Store))  as ID,i.Name as Name ,s.CurrentStock,s.RetailPrice,st.Shortname as Store,i.ShortName,s.ID_Stock As [Ref No],s.BatchNo",
            "s.ID_Stock", "CurrentStock desc", " i.StopSale='False' and i.Active='True'  and s.CurrentStock>0 and  s.FK_Store=" + ddlStore.SelectedValue + "  and s.FK_Client=" + hfClient.Value.Split(',')[0] +
            " AND (s.FK_Items =" + hdfItem.Value + " OR 0=" + hdfItem.Value + ")", searchCol + "  LIKE '" + strName + "%' ", pageIndex, 10, "", (Repeater)CommonPopup1.FindControl("rptPager"), this.Page);
        
      

        Populate = null;
        //    txtSubHeadRefno.Focus();
        GridView gv= (GridView)CommonPopup1.FindControl("gdvSearch");
        
        if (hfgvItemsRowIndex.Value != string.Empty && gv.Rows.Count!=0)
        {
            lblPopupHeader.Text = "Stock";
            mpCommon.Show();
        }
       

    }
    protected void gvItems_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            if (e.CommandName == "PopUp")
            {
                GridViewRow gr = (GridViewRow)((ImageButton)e.CommandSource).NamingContainer;
                int index = gr.RowIndex;
                hfgvItemsRowIndex.Value = index.ToString();
             //   GridViewRow gr = (GridViewRow)gvItems.Rows[index];
                Label lblname = (Label)gr.FindControl("lblItem");
                HiddenField hdfItem = (HiddenField)gr.FindControl("hdfItem");
                ((TextBox)CommonPopup1.FindControl("txtName")).Text = lblname.Text.Trim();
                ((TextBox)CommonPopup1.FindControl("txtRefno")).Text = hdfItem.Value;
                ((HiddenField)CommonPopup1.FindControl("hfColumn")).Value = "Name";
                StockAdjustmentFill(1);
                divAdd.Style.Add("display", "block");
                divView.Style.Add("display", "none");

            }
        }
        catch (Exception ex)
        {
        }
    }


    protected void hfItemValue_ValueChanged(object sender, EventArgs e)
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
    protected void ddlItemsFill()
    {
        if (hfItemValue.Value != "0")
        {


            BlItems blitems = new BlItems();
            blitems.MasterId = Convert.ToInt64(hfItemValue.Value.ToString());
            blitems.FK_Client = Convert.ToInt64(hfClient.Value);
            blitems.SelectData();

            txtItemShortName.Text = blitems.Name.ToString();
            blitems = null;




        }
    }
}