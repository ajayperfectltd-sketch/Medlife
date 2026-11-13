/*----------------------------------------------------------------------------------------------------------------------------------------------------------
Purpose    : Insert/Update/Delete Material Reciept 
Author     : bibin TC
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

public partial class MaterialReciept : System.Web.UI.Page
{
    #region Functions
    protected void ddlItemsFill()
    {
        if (ddlItem.SelectedIndex > 0)
        {
            BlItems blitems = new BlItems();
            blitems.MasterId = Convert.ToInt64(ddlItem.SelectedValue.ToString());
            blitems.FK_Client = Convert.ToInt64(hfClient.Value);
            blitems.SelectData();
           
            ddlUnit.SelectedValue = blitems.FK_Units.ToString();
            txtItemShortName.Text = "abc"; //blitems.Name.ToString();
        
           
        }
    }
    protected void BindData()
    {
        Populate populate = new Populate();

        populate.FillDropdownList(ddlSupplier, "Supplier", "Name", "ID_Supplier", "ID_Supplier", "Active='True' AND FK_Client= " + hfClient.Value.Split(',')[0], "");
        populate.FillDropdownList(ddlStore, "Store", "StoName", "ID_Store", "ID_Store", " Active='True' AND FK_Client= " + hfClient.Value.Split(',')[0], "");
        populate.FillDropdownList(ddlPurchaseOrder, "PurchaseOrder", "PONo", "ID_PurchaseOrder", "ID_PurchaseOrder", "  Cancelled='False' AND Closed='False' AND FK_Client=" + hfClient.Value.Split(',')[0], "");
        populate.FillDropdownList(ddlItem, "Items", "Name", "ID_Items", "ID_Items", "  Active='True' AND ItemType=2 AND FK_Client= " + hfClient.Value.Split(',')[0], "");
        populate.FillDropdownList(ddlSearchSupplier, "Supplier", "Name", "ID_Supplier", "ID_Supplier", " Active='True' AND FK_Client= " + hfClient.Value.Split(',')[0], "");
        populate.FillDropdownList(ddlSearchStore, "Store", "StoName", "ID_Store", "ID_Store", " Active='True' AND FK_Client= " + hfClient.Value.Split(',')[0], "");
        populate.FillDropdownList(ddlBillType, "BillType", "TypeName", "ID_BillType", "ID_BillType", " BillType=2 AND FK_Client= " + hfClient.Value.Split(',')[0], "");
        populate.FillDropdownList(ddlCategory, "CategoryFirst", "Name", "ID_CategoryFirst", "ID_CategoryFirst", "  FK_Client= " + hfClient.Value.Split(',')[0], "");
        populate.FillDropdownList(ddlRecieptNo, "MaterialReciept", "ID_MaterialReciept", "ID_MaterialReciept", " ", "  Cancelled='False' AND   FK_Client= " + hfClient.Value.Split(',')[0], "");
        populate.FillDropdownList(ddlUnit, "Units", "Name", "ID_Units", "ID_Units", " FK_Client= " + hfClient.Value.Split(',')[0], "");
       
        populate = null;
     
        txtExpiryDate.Text = DateTime.Now.ToShortDateString();
    }
  
    private void FillterPurchaseorder()
    {
        Populate populate = new Populate();
        if (ddlSupplier.SelectedIndex > 0 && ddlPurchaseOrder.Enabled)
        {
            populate.FillDropdownList(ddlPurchaseOrder, "PurchaseOrder", "PONo", "ID_PurchaseOrder", "ID_PurchaseOrder", "  Cancelled='False' AND Closed='False' AND FK_Supplier = " + ddlSupplier.SelectedValue.Split(',')[0] + "  AND FK_Client=" + hfClient.Value.Split(',')[0], "");
        }
        else
        {
            populate.FillDropdownList(ddlPurchaseOrder, "PurchaseOrder", "PONo", "ID_PurchaseOrder", "ID_PurchaseOrder", "  Cancelled='False' AND Closed='False' AND FK_Client=" + hfClient.Value.Split(',')[0], "");
        }
        populate = null;
        divAdd.Style.Add("display", "block");
        divView.Style.Add("display", "none");
    }
    protected void SetControls()
    {
          txtExpiryDate.Attributes.Add("readOnly", "readOnly");
          txtInvoiceDate.Attributes.Add("readOnly", "readOnly");
          //txtSalesPrice.Attributes.Add("readOnly", "readOnly");
          //txtWholeSale.Attributes.Add("readOnly", "readOnly");

    }
    protected void GridFill(int currentPage)
    {
        //BlMaterialReciept blMaterialReciept = new BlMaterialReciept();
        //DataTable dtbl = new DataTable();
        //blMaterialReciept.FK_Store = Convert.ToInt64(ddlSearchStore.SelectedValue.ToString());
        //blMaterialReciept.FK_Client = Convert.ToInt64(hfClient.Value);
        //blMaterialReciept.UserCode = hfUser.Value;
        //blMaterialReciept.PageSize = 10;
        //blMaterialReciept.PageIndex = Convert.ToInt16(currentPage);
        //blMaterialReciept.FK_Supplier = Convert.ToInt64(ddlSearchSupplier.SelectedValue.ToString());
        //dtbl = blMaterialReciept.SelectAllData();
        //gvMaterialReciept.DataSource = dtbl;
        //gvMaterialReciept.DataBind();
        //if (dtbl.Rows.Count > 0)
        //{
        //    int recordCount = Convert.ToInt32(dtbl.Rows[0]["RecordCount"].ToString());
        //    this.PopulatePager(recordCount, currentPage);

        //    //Block new entries            
        //    Common common = new Common();
        //    int bcount = common.BlockNewEntry(recordCount);
        //    if (bcount > 0)
        //    {
        //        btnSubmit.Visible = false;
        //        ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "alertify.alert('Product expired. Please contact software vendor')", true);
        //    }
        //    else
        //    {
        //        btnSubmit.Visible = true;
        //    }
        //    common = null;
        //}
        //blMaterialReciept = null;
    }
    protected void CheckUserAccess()
    {
        BlUserAccess bluseraccess = new BlUserAccess();
        Populate populate = new Populate();

        bluseraccess.FK_Users = Convert.ToInt64(hfUser.Value);
        bluseraccess.FK_Pages = 35;
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
    protected void LoadStoreFromPurchaseOrder()
    { 
         BlMaterialReciept blMaterialReciept = new BlMaterialReciept();
         blMaterialReciept.FK_PurchaseOrder = Convert.ToInt64( ddlPurchaseOrder.SelectedValue.ToString());
         blMaterialReciept.FK_Client = Convert.ToInt64(hfClient.Value);
         blMaterialReciept.SelectpurchaseOrderStore();
         try
         {
             ddlStore.SelectedValue = blMaterialReciept.FK_Store.ToString();
         }
         catch (Exception ex)
         {
             ddlStore.SelectedValue = "0";
         }
         try
         {
             ddlSupplier.SelectedValue = blMaterialReciept.FK_Supplier.ToString();
         }
         catch (Exception ex)
         {
             ddlSupplier.SelectedValue = "0";
         }
       //  ddlStore.SelectedValue = blMaterialReciept.FK_Store.ToString();
        // ddlSupplier.SelectedValue = blMaterialReciept.FK_Supplier.ToString();
         blMaterialReciept = null;
    
    }
    protected void LoadItemFromPurchaseOrder()
    {
        DataTable dtbl = new DataTable();
        BlMaterialReciept blMaterialReciept = new BlMaterialReciept();
        blMaterialReciept.FK_PurchaseOrder = Convert.ToInt64( ddlPurchaseOrder.SelectedValue.ToString());
        blMaterialReciept.FK_Client = Convert.ToInt64(hfClient.Value);
        dtbl = blMaterialReciept.GetPartialMaterialRecieptItems();
        if (dtbl.Rows.Count > 0)
        {
            //dtbl.Columns.Add("SLNO", typeof(Int64));
            //dtbl.Columns["SLNO"].AutoIncrement = true;
            //dtbl.Columns["SLNO"].AutoIncrementSeed = 1;
            //dtbl.Columns["SLNO"].AutoIncrementStep = 1;
              for (int i = 0; i < dtbl.Rows.Count; i++)
             
              {
                   decimal a = Convert.ToDecimal( dtbl.Rows[i]["Quantity"]);
                   if (a <= 0)
                   {
                       DataRow dr = dtbl.Rows[i];
                       dr.Delete();
                     
                   
                   }
              }
              dtbl.AcceptChanges();

           //if(dtbl.Columns["Quantity"].
            
           // ViewState["TempTable"] = dtbl;
          //  gvItems.DataSource = dtbl;
           // gvItems.DataBind();
           

            ddlItem.Enabled = false;
            txtBatch.Enabled = false;
            //txtWholeSale.Enabled = false;
            txtExpiryDate.Enabled = false;
            txtQuantity.Enabled = false;
            txtPurchaseRate.Enabled = false;
            //txtActualCost.Enabled = false;
           // txtSalesPrice.Enabled = false;
           // txtOfferPrice.Enabled = false;
            btnAdd.Enabled = false;
            btnClearAdd.Enabled = false;
            ddlSupplier.Enabled = false;
            ddlUnit.Enabled = false;
            imgPopUp.Enabled = false;


        }
        else
        {
            
            ddlItem.Enabled = true;
            txtBatch.Enabled = true;
           // txtWholeSale.Enabled = true;
            txtExpiryDate.Enabled = true;
            txtQuantity.Enabled = true;
            txtPurchaseRate.Enabled = true;
            //txtActualCost.Enabled = true;
            //txtSalesPrice.Enabled = true;
            //txtOfferPrice.Enabled = true;
            btnAdd.Enabled = true;
            btnClearAdd.Enabled = true;
            ddlSupplier.Enabled = true;
            ddlUnit.Enabled = true;
            imgPopUp.Enabled = true;
            ddlPurchaseOrder.SelectedIndex = 0;
            ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "alertify.alert('Material Receipt already done')", true);
        }

    
    }

    protected void AddData()
    {
        DataTable dtbl = new DataTable();
        int rowCount = 0;
        int inCheck = 0;
        if (ViewState["TempTable"] != null)
        {
            dtbl = (DataTable)ViewState["TempTable"];
            rowCount = dtbl.Rows.Count;
        }
        else
        {
            dtbl.Columns.Add("SLNO");
            dtbl.Columns.Add("Item");
            dtbl.Columns.Add("FK_Items");
            dtbl.Columns.Add("FK_Units");
            dtbl.Columns.Add("Units");
            dtbl.Columns.Add("BatchNo");
            dtbl.Columns.Add("ExpiryDate");
            dtbl.Columns.Add("Quantity");
            dtbl.Columns.Add("ActQty");
            dtbl.Columns.Add("Free");
            dtbl.Columns.Add("PurchaseVAT");


            dtbl.Columns.Add("PurchaseRate");
            dtbl.Columns.Add("MRP");
            dtbl.Columns.Add("VAT");
            dtbl.Columns.Add("DicountPer");
            dtbl.Columns.Add("DiscountAmt");
            dtbl.Columns.Add("ProfitPer");
            dtbl.Columns.Add("RetailPrice");
            dtbl.Columns.Add("WholeSalePrice");
            dtbl.Columns.Add("PrivilagePrice");



            dtbl.Columns.Add("NetAmount");
            dtbl.Columns.Add("FK_MaterialReciept");
            
        }
        if (dtbl.Rows.Count > 0)
        {

            for (int i = 0; i < dtbl.Rows.Count; i++)
            {
                if (dtbl.Rows[i]["FK_Items"].ToString() == ddlItem.SelectedValue.ToString())
                {
                    inCheck = inCheck + 1;
                    break;
                }
            }
        }

      
        if (inCheck == 0)
        {
            DataRow drow = dtbl.NewRow();
            drow["SLNO"] = rowCount + 1;
            drow["Item"] = ddlItem.SelectedItem.ToString();
            drow["FK_Items"] = ddlItem.SelectedValue.ToString();
            drow["FK_Units"] = ddlUnit.SelectedValue.ToString();
            drow["Units"] = ddlUnit.SelectedItem.ToString();



            drow["BatchNo"] = Convert.ToString( txtBatch.Text);

            drow["ExpiryDate"] = DateTime.ParseExact(txtExpiryDate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);

            drow["Quantity"] = txtQuantity.Text == "" ? "0" : txtQuantity.Text;
            drow["ActQty"] = txtActqty.Text == "" ? "0" : txtActqty.Text;
            drow["Free"] = txtFree.Text == "" ? "0" : txtFree.Text;

            drow["PurchaseRate"] = txtPurchaseRate.Text == "" ? "0" : txtPurchaseRate.Text;
            drow["MRP"] = txtMRP.Text == "" ? "0" : txtMRP.Text;

            drow["VAT"] = txtVat.Text == "" ? "0" : txtVat.Text;
            drow["PurchaseVAT"] = txtPurchaseVat.Text == "" ? "0" : txtPurchaseVat.Text;

            drow["DicountPer"] = txtDiscountPer.Text == "" ? "0" : txtDiscountPer.Text;
            drow["DiscountAmt"] = txtDiscountAmt.Text == "" ? "0" : txtDiscountAmt.Text;
            drow["ProfitPer"] = txtProfitPer.Text == "" ? "0" : txtProfitPer.Text;


            drow["RetailPrice"] = txtRetailPrice.Text == "" ? "0" : txtRetailPrice.Text;
            drow["WholeSalePrice"] = txtWholeSalePrice.Text == "" ? "0" : txtWholeSalePrice.Text;

            drow["PrivilagePrice"] = txtPrivilagePrice.Text == "" ? "0" : txtPrivilagePrice.Text;


            drow["NetAmount"] = (decimal.Parse(txtPurchaseRate.Text == "" ? "0" : txtPurchaseRate.Text) * decimal.Parse(txtQuantity.Text == "" ? "0" : txtQuantity.Text));

            drow["FK_MaterialReciept"] = ddlRecieptNo.SelectedValue.ToString();
          


            dtbl.Rows.InsertAt(drow, rowCount);
            gvItems.DataSource = dtbl;
            gvItems.DataBind();
            ViewState["TempTable"] = dtbl;
            gvItems.Visible = true;
            //ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "ItemClear();", true);
        }
        else
        {
            ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "alertify.alert('Item already exists')", true);
        }
    }

    protected void SaveData()
    {
        long status = 0;
        BlMaterialReciept blMaterialReciept = new BlMaterialReciept();

        DateTime invoiceDate;
        if (!DateTime.TryParse(txtInvoiceDate.Text.Trim(), out invoiceDate))
        {
            ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script",
                "toastr.error('Invalid Invoice Date.')", true);
            return;
        }

        DateTime today = DateTime.Today;

        // Rule 1 — No future dates
        if (invoiceDate.Date > today)
        {
            ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script",
                "toastr.error('Invoice date cannot be a future date.')", true);
            return;
        }

        // Rule 2 — Must be within current financial year
        int finYear = (today.Month >= 4) ? today.Year : today.Year - 1;
        DateTime finYearStart = new DateTime(finYear, 4, 1);

        if (invoiceDate.Date < finYearStart.Date)
        {
            ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script",
                "toastr.error('Invoice date cannot be from a previous financial year. " +
                "Earliest allowed date is " + finYearStart.ToString("dd/MM/yyyy") + ".')", true);
            return;
        }

        blMaterialReciept.FK_PurchaseOrder = Convert.ToInt64(ddlPurchaseOrder.SelectedValue);
        blMaterialReciept.FK_Supplier = Convert.ToInt64(ddlSupplier.SelectedValue);
        blMaterialReciept.FK_Store = Convert.ToInt64(ddlStore.SelectedValue);
        blMaterialReciept.EnteredOn = Convert.ToDateTime(DateTime.Now);
        blMaterialReciept.FK_Client = Convert.ToInt64(hfClient.Value);
        blMaterialReciept.UserCode = hfUser.Value;
        ArrayList arlist = new ArrayList();
        DataTable dtbl = (DataTable)ViewState["TempTable"];
        
        IMaterialRecieptDetails IMaterialRecieptDetails;
        for (int i = 0; i < dtbl.Rows.Count; i++)
        {
            IMaterialRecieptDetails = new IMaterialRecieptDetails();

                IMaterialRecieptDetails.SlNo = Convert.ToInt32(dtbl.Rows[i]["SLNo"].ToString());
                IMaterialRecieptDetails.FK_Items = Convert.ToInt32(dtbl.Rows[i]["FK_Items"].ToString());
                IMaterialRecieptDetails.FK_Category = Convert.ToInt32(dtbl.Rows[i]["FK_Category"].ToString());
                IMaterialRecieptDetails.Fk_Unit = Convert.ToInt32(dtbl.Rows[i]["FK_Units"].ToString());
                IMaterialRecieptDetails.FK_SubUnit = Convert.ToInt32(dtbl.Rows[i]["FK_SubUnit"].ToString());
                IMaterialRecieptDetails.BatchNo = Convert.ToString(dtbl.Rows[i]["BatchNo"].ToString());
                IMaterialRecieptDetails.ExpiryDate = Convert.ToDateTime(dtbl.Rows[i]["ExpiryDate"].ToString());
                IMaterialRecieptDetails.HSN = Convert.ToString(dtbl.Rows[i]["HSN"].ToString());
                IMaterialRecieptDetails.Quantity = Convert.ToDecimal(dtbl.Rows[i]["Quantity"].ToString());
                IMaterialRecieptDetails.Free = Convert.ToDecimal(dtbl.Rows[i]["Free"].ToString());
                IMaterialRecieptDetails.ActQty = Convert.ToDecimal(dtbl.Rows[i]["ActQty"].ToString());
                IMaterialRecieptDetails.PurRate = Convert.ToDecimal(dtbl.Rows[i]["PurRate"].ToString());
                IMaterialRecieptDetails.IncludePTax = Convert.ToBoolean(dtbl.Rows[i]["IncludePtax"].ToString());
                IMaterialRecieptDetails.PurchaseTaxAmount = Convert.ToDecimal(dtbl.Rows[i]["PurchaseTaxAmount "].ToString());
                IMaterialRecieptDetails.DiscountOnTax = Convert.ToBoolean(dtbl.Rows[i]["DiscountOntax"].ToString());
                IMaterialRecieptDetails.PurchaseDiscPercentage = Convert.ToDecimal(dtbl.Rows[i]["PurchaseDiscPercentage"].ToString());
                IMaterialRecieptDetails.PurchaseDiscAmount = Convert.ToDecimal(dtbl.Rows[i]["PurchaseDiscAmount"].ToString());
                IMaterialRecieptDetails.NetPurchase = Convert.ToDecimal(dtbl.Rows[i]["NetPurchase"].ToString());
                IMaterialRecieptDetails.MRP = Convert.ToDecimal(dtbl.Rows[i]["MRP"].ToString());
                IMaterialRecieptDetails.ProfitPer = Convert.ToDecimal(dtbl.Rows[i]["ProfitPer"].ToString());
                IMaterialRecieptDetails.ProfitAmount = Convert.ToDecimal(dtbl.Rows[i]["ProfitAmount"].ToString());
                IMaterialRecieptDetails.DiscountPer = Convert.ToDecimal(dtbl.Rows[i]["DiscountPer"].ToString());
                IMaterialRecieptDetails.DiscountAmt = Convert.ToDecimal(dtbl.Rows[i]["DiscountAmt"].ToString());
                IMaterialRecieptDetails.IncludeSTax = Convert.ToBoolean(dtbl.Rows[i]["IncludeSTax "].ToString());
                IMaterialRecieptDetails.SalesTaxAmount = Convert.ToDecimal(dtbl.Rows[i]["SalesTaxAmount "].ToString());
                IMaterialRecieptDetails.SalesPrice = Convert.ToDecimal(dtbl.Rows[i]["SalesPrice"].ToString());
                IMaterialRecieptDetails.FK_MaterialReciept = Convert.ToInt64(dtbl.Rows[i]["FK_MaterialReciept"].ToString());
                IMaterialRecieptDetails.PurchaseTotal = Convert.ToDecimal(dtbl.Rows[i]["PurchaseTotal"].ToString());
           

            arlist.Add(IMaterialRecieptDetails);
            IMaterialRecieptDetails = null;
        }

        blMaterialReciept.XmlMaterialRecieptItems = Common.WriteToXml(arlist);



        if (hfMaterialRecieptId.Value == "0")
        {
            if (hfIsAdd.Value.ToLower() == "true")
            {
                status = blMaterialReciept.InsertData();
                if (status > 0)
                {
                    ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "toastr.success('Data inserted successfully')", true);
                    ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "ShowAddDiv();", true);
                     
                    Clear();
                             
                }
                else if(status == -9)
                {
                    ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "ShowAddDiv();", true);

                    ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "toastr.error('Date of  Material Reciept is lesser than date of PurchaseOrder')", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "ShowAddDiv();", true);

                    ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "toastr.error('Cannot insert data')", true);
                   
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

                blMaterialReciept.MasterId = Convert.ToInt64(hfMaterialRecieptId.Value);
                status = blMaterialReciept.UpdateData();
                if (status > 0)
                {

                    ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "toastr.success('Data updated successfully')", true);
                    ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "ShowAddDiv();", true);
                   
                   // GridFill(0);
                    Clear();
                  
                }
                else if (status == -1)
                {
                    ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "ShowAddDiv();", true);
                    ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "toastr.error('Cannot update data.. ')", true);

                }
                else if (status == -9)
                {
                    ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "ShowAddDiv();", true);

                    ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "toastr.error('Date of  Material Reciept is lesser than date of PurchaseOrder')", true);
                }
                else if (status == -7)
                {
                    ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "ShowAddDiv();", true);
                    ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "toastr.error('Cannot update data.. Purchase already done ')", true);

                
                }
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "toastr.warning('User does not have permission to Update')", true);
                
            }
        }
        blMaterialReciept = null;
    }
    public void Clear()
    {
        ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "ClearAdd();", true);
        ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "ClearAdd();", true);
        ddlItem.Enabled = true;
        txtBatch.Enabled = true;
        //txtWholeSale.Enabled = true;
        txtExpiryDate.Enabled = true;
        txtQuantity.Enabled = true;
        txtPurchaseRate.Enabled = true;
        //txtActualCost.Enabled = true;
       // txtSalesPrice.Enabled = true;
       // txtOfferPrice.Enabled = true;
        btnAdd.Enabled = true;
        ddlSupplier.Enabled = true;
        btnClearAdd.Enabled = true;
        ddlSupplier.Enabled = true;
        ddlUnit.Enabled = true;
       ddlPurchaseOrder.Enabled = true;
       imgPopUp.Enabled = true;

        Populate populate = new Populate();
        populate.FillDropdownList(ddlPurchaseOrder, "PurchaseOrder", "PONo", "ID_PurchaseOrder", "ID_PurchaseOrder", "  Cancelled='False' AND Closed='False' AND FK_Client=" + hfClient.Value, "");

        //ViewState["TempTable"] = null;
        //gvItems.DataSource = null;
       // gvItems.DataBind();
        divAdd.Style.Add("display", "block");
        divView.Style.Add("display", "none");       
    }
    protected void GetModSettings()
    
    {
        //string ModSettings;
        //string[] settings;
        //BlSettings blsettings = new BlSettings();
        //blsettings.FK_Client= Convert.ToInt64(hfClient.Value);
        //ModSettings = blsettings.GetModuleSettings();
        //settings=ModSettings.Split(',');
        //if (settings[0].ToString().ToLower() == "false")
        //{
        //    liPO.Attributes.Add("style", "display:none");
        //}
        //else
        //{
        //    liPO.Attributes.Remove("style");
        //}
    }
    #endregion
    #region Events
    

    protected void ddlItem_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {

            if (ddlItem.SelectedIndex > 0)
            {
                ddlItemsFill();
            }

        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "toastr.warning('" + ex.Message + "')", true);
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "ClearAdd();", true);
                Page.Header.DataBind();
               // blMessage.Items.Clear();
               
                divAdd.Style.Add("display", "block");
                hfUser.Value = Request.QueryString["uid"];
                hfClient.Value = Request.QueryString["clnt"];
                CheckUserAccess();
                BindData();
                SetControls();
               // GridFill(1);
                gvItems.DataSource = null;
                gvItems.DataBind();
                GetModSettings();
              
            }
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

        GridFill(pageIndex);
    }
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        try
        {

            DateTime dtExpiry = DateTime.ParseExact(txtExpiryDate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
           // DateTime dtNow = Convert.ToDateTime(hfDate.Value);//DateTime.ParseExact(DateTime.Now.ToString("dd/MM/yyyy"), "dd/MM/yyyy", CultureInfo.InvariantCulture);

            DateTime dtNow = Convert.ToDateTime(hfDate.Value);
            if (ddlItem.SelectedItem.ToString().Trim() == txtItemShortName.Text.ToString().Trim())
            {
                if (dtExpiry < dtNow)
                {
                    //ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "alert('Expirydate cannot be lesser than actual date')", true);
                    //ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "ConfirmDate();", true);

                    //string confirmValue = Request.Form["confirm_value"];
                    //if (confirmValue.Length > 3)
                    //    confirmValue = confirmValue.Substring(confirmValue.Length - 3, 3);
                    //if (confirmValue == "Yes")
                    //{
                    ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "alertify.alert('Expirydate is lesser than actual date')", true);
                    AddData();
                    ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "ItemClear();", true);
                    ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "ShowAddDiv();", true);
                    ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "ShowAddDiv();", true);
                    //}
                    //else
                    //{
                    //    txtDate.Text = Convert.ToString(DateTime.Now);

                    //}



                }
                else
                {
                    AddData();
                    //ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "ItemClear();", true);
                    //ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "ShowAddDiv();", true);
                   // ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "ShowAddDiv();", true);
                }
            } 
            else 
            {
                ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "toastr.warning('Item  does not exist')", true);
                        
            }

           
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "toastr.warning('" + ex.Message + "')", true); 
        }
    }
    protected void btnClear_Click(object sender, EventArgs e)
    {
        try
        {
            Clear();
           
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
             //blMessage.Items.Clear();
            divAdd.Style.Add("display", "none");
            divView.Style.Add("display", "block");
           
          
           
            GridFill(1);
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "toastr.warning('" + ex.Message + "')", true); 
        }

       

    }
    protected void gvItems_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        //blMessage.Items.Clear();
        try
        {
            string confirmValue = Request.Form["confirm_value"];
            if (confirmValue.Length > 3)
                confirmValue = confirmValue.Substring(confirmValue.Length - 3, 3);
            if (confirmValue == "Yes")
            {
                if (ViewState["TempTable"] != null)
                {
                    DataTable dtbl = (DataTable)ViewState["TempTable"];
                    dtbl.Rows.RemoveAt(e.RowIndex);
                    gvItems.DataSource = dtbl;
                    gvItems.DataBind();
                    ViewState["TempTable"] = dtbl;
                }
            }
            ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "ShowAddDiv();", true);

        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "toastr.warning('" + ex.Message + "')", true); 
        }

    }
    protected void gvMaterialReciept_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        //blMessage.Items.Clear();
        try
        {

            string confirmValue = Request.Form["confirm_value"];
            if (confirmValue.Length > 3)
                confirmValue = confirmValue.Substring(confirmValue.Length - 3, 3);
            if (confirmValue == "Yes")
            {
                long status = 0;
                BlMaterialReciept blMaterialReciept = new BlMaterialReciept();
                blMaterialReciept.MasterId = Convert.ToInt64(gvMaterialReciept.DataKeys[e.RowIndex]["ID_MaterialReciept"].ToString());
                blMaterialReciept.FK_Client = Convert.ToInt64(hfClient.Value);
                blMaterialReciept.UserCode = hfUser.Value;
                if (hfIsDelete.Value.ToLower() == "true")
                {
                    status = blMaterialReciept.DeleteData();
                    if (status > 0)
                    {
                        GridFill(1);
                        // blMessage.Items.Add("Deleted successfully");
                        ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "toastr.success('Deleted successfully')", true);
                    }
                    else if(status==-3)
                    {
                        ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "toastr.error('Cannot delete, Purchase Already done')", true);
                        // blMessage.Items.Add("Cannot delete, reference exists");
                    }

                    else
                    {
                        ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "toastr.error('Cannot delete, reference exists')", true);
                        // blMessage.Items.Add("Cannot delete, reference exists");
                    }
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "toastr.warning('User does not have permission to Delete')", true);
                    //txtShortName.Focus();
                }
                blMaterialReciept = null;
            }

        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "toastr.warning('" + ex.Message + "')", true); 
        }
    }
    protected void gvMaterialReciept_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        try
        {
            //BlMaterialReciept blMaterialReciept = new BlMaterialReciept();
            //ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "ShowAddDiv();", true);

            //blMaterialReciept.MasterId = Convert.ToInt64(gvMaterialReciept.DataKeys[e.NewSelectedIndex]["ID_MaterialReciept"].ToString());
            //hfMaterialRecieptId.Value = blMaterialReciept.MasterId.ToString();
            //blMaterialReciept.FK_Client = Convert.ToInt64(hfClient.Value);
            //Populate populate = new Populate();
            //populate.FillDropdownList(ddlPurchaseOrder, "PurchaseOrder", "PONo", "ID_PurchaseOrder", "ID_PurchaseOrder", "  Cancelled='False'  AND FK_Client=" + hfClient.Value, "");
            //blMaterialReciept.UserCode = hfUser.Value;
            //blMaterialReciept.SelectData();

            //txtBillDate.Text = blMaterialReciept.Date.ToString("dd/MM/yyyy");
            //try
            //{
            //    ddlSupplier.SelectedValue = blMaterialReciept.FK_Supplier.ToString();
            //}
            //catch (Exception ex)
            //{
            //    ddlSupplier.SelectedValue = "0";
            //}
            //try
            //{
            //    ddlStore.SelectedValue = blMaterialReciept.FK_Store.ToString();
            //}
            //catch (Exception ex)
            //{
            //    ddlStore.SelectedValue = "0"; 
            //}
            //ddlPurchaseOrder.SelectedValue = blMaterialReciept.FK_PurchaseOrder.ToString();
            //txtRemarks.Text = blMaterialReciept.Remarks.ToString();
            //txtBillNo.Text = blMaterialReciept.RefNo.ToString();
           

            //if (blMaterialReciept.FK_PurchaseOrder > 0)
            //{
            //    ddlItem.Enabled = false;
            //    txtBatch.Enabled = false;
            //   // txtWholeSale.Enabled = false;
            //    txtExpiryDate.Enabled = false;
            //    txtQuantity.Enabled = false;
            //    txtPurchaseRate.Enabled = false;
            //   // txtActualCost.Enabled = false;
            //   // txtSalesPrice.Enabled = false;
            //   // txtOfferPrice.Enabled = false;
            //    btnAdd.Enabled = false;
            //    btnClearAdd.Enabled = false;
            //    ddlSupplier.Enabled = false;
            //    ddlPurchaseOrder.Enabled = false;
            //    imgPopUp.Enabled = false;
            //}
            //else
            //{
            //    imgPopUp.Enabled = true;
            //    ddlItem.Enabled = true;
            //    txtBatch.Enabled = true;
            //    //txtWholeSale.Enabled = true;
            //    txtExpiryDate.Enabled = true;
            //    txtQuantity.Enabled = true;
            //    txtPurchaseRate.Enabled = true;
            //    //txtActualCost.Enabled = true;
            //   // txtSalesPrice.Enabled = true;
            //    //txtOfferPrice.Enabled = true;
            //    btnAdd.Enabled = true;
            //    btnClearAdd.Enabled = true;
            //    ddlSupplier.Enabled = true;
            //    ddlPurchaseOrder.Enabled = false;
            //}
            //txtRemarks.Text = blMaterialReciept.Remarks.ToString();


            //DataTable dtbl = blMaterialReciept.SelectMaterialRecieptItems();
            //gvItems.DataSource = dtbl;
            //gvItems.DataBind();
            //ViewState["TempTable"] = dtbl;
            //blMaterialReciept = null;
        }
        catch (Exception ex)
        {
        }
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        try
        {
           // blMessage.Items.Clear();
            GridFill(1);
        }
        catch (Exception ex)
        {
           // blMessage.Items.Add(ex.Message);
        }
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            if (gvItems.Rows.Count > 0)
                SaveData();

            else
            {

                ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "alertify.alert('Atleast one item required')", true);
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "toastr.warning('" + ex.Message + "')", true); 
        }

    }
    protected void btnPurchaseOrderBlurTrigger_Click(object sender, EventArgs e)
    {
        try
        {
            try
            {
                if (ddlPurchaseOrder.SelectedIndex > 0)
                {
                    // Populate populate = new Populate();
                    ///// populate.FillDropdownList(ddlCardAccountSubHead, "AccountHeadSub", "AhsCode+'-'+Name", "ID_AccountHeadSub", "ID_AccountHeadSub", "FK_Client=1 AND Active='True' AND FK_AccountHead=" + ddlCardAccountHead.SelectedValue.ToString(), "");
                    // populate = null;


                    LoadItemFromPurchaseOrder();
                    LoadStoreFromPurchaseOrder();

                    ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "ShowAddDiv();", true);

                }
                else
                {

                }
                ddlSupplier.Focus();
                // ddlPurchaseOrder.Focus();
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "toastr.warning('" + ex.Message + "')", true); 
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "toastr.warning('" + ex.Message + "')", true); 
        }
    }
   
  
   
   

   
  
    protected void ddlCategory_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlCategory.SelectedIndex > 0)
        {
      //  gvItems.DataSource = null;
     //   gvItems.DataBind();
        }
    }

    #endregion

    protected void btnMRDelete_Click(object sender, EventArgs e)
    {

        try

        {
            
           

                if (ddlRecieptNo.SelectedIndex > 0)
                {
                    //Delete Satement for materialReciept

                }
                else
                {
                    ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "alertify.alert('Please select a MaterialReciept ')", true);

                }
           
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "toastr.warning('" + ex.Message + "')", true);
        }

    }
    protected void gvItems_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        try
        {

            if (gvItems.Rows.Count > 0)
            {
                DataTable dtbl = new DataTable();
                if (ViewState["TempTable"] != null)
                {
                    dtbl = (DataTable)ViewState["TempTable"];

                    ddlItem.SelectedValue= dtbl.Rows[e.NewSelectedIndex]["FK_Items"].ToString();
                   
                    txtBatch.Text = Convert.ToString(((TextBox)gvItems.Rows[e.NewSelectedIndex].FindControl("lblgvBatchNo")).Text);
                  

                }
            }
            

        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "toastr.warning('" + ex.Message + "')", true);
        }
    }
}