/*----------------------------------------------------------------------------------------------------------------------------------------------------------
Purpose    : Create WebPharma Reports
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
using WebPharma.Business.WebPharma;
using WebPharma.Business.Masters;
using WebPharma.Business;
using System.Data;
using System.Globalization;
using WebPharma.Business.WebWebPharma;

public partial class WebPharmaReport : System.Web.UI.Page
{
    #region Functions
    public void CustomerClear()
    {
        txtCustRefno.Text = "";
        txtCustomer.Text = "";
    }
    private void ClearSupplier()
    {
        txtSupplierName.Text = "";
        TxtSupplierRefNo.Text = "";
    }
    private void ClearItem()
    {
        textItemName.Text = "";
        txtItemRefno.Text = "";
    }
    protected void BindData()
    {
        Populate populate = new Populate();
        // populate.FillDropdownListAll(ddlUsers, " Users as U Join Sales as S  On S.UserCode=U.ID_Users AnD S.Cancelled=0 ", "Username", "Distinct U.ID_Users", "ID_Users", "U.Active='True' AND S.FK_Client=" + hfClient.Value, "");
        populate.FillDropdownListAll(ddlCategoryFirst, "CategoryFirst", "Name", "ID_CategoryFirst", "Name", "FK_Client=" + hfClient.Value, "");
        populate.FillDropdownListAll(ddlStore, "Store", "Name", "ID_Store", "Name", "Active='True' AND FK_Client=" + hfClient.Value, "");
        // BindddlItems();
        populate.FillDropdownListAll(ddlFromStore, "Store", "Name", "ID_Store", "Name", "Active='True' AND FK_Client=" + hfClient.Value, "");
        populate.FillDropdownListAll(ddlToStore, "Store", "Name", "ID_Store", "Name", "Active='True' AND FK_Client=" + hfClient.Value, "");
        populate.FillDropdownListAll(ddlUser, "Users", "Name", "ID_Users", "Name", "Active='True' AND FK_Client=" + hfClient.Value, "");
        populate.FillDropdownListAll(ddlEmployee, "Employee", "Name", "ID_Employee", "Name", "  Active='True' AND Salesman='True' AND FK_Client=" + hfClient.Value, "");
        //populate.FillDropdownListAll(ddlSupplier, "Supplier", "Name", "ID_Supplier", "Name", "Active='True' And FK_Client=" + hfClient.Value, "");
        ////populate.FillDropdownListAll(ddlCustomer, "Customer", "Name", "ID_Customer", "ID_Customer", "Active='True' And FK_Client=" + hfClient.Value, "");
        populate.FillDropdownList(ddlCustomer2, "Customer", "Name", "ID_Customer", "Name", "Active='True' And FK_Client=" + hfClient.Value, "");
        populate.FillDropdownListAll(ddlCustomerGrp, "CustomerGroup", "Name", "ID_CustomerGroup", "Name", "Active='True' And FK_Client=" + hfClient.Value, "");
        populate.FillDropdownListAll(ddlCounter, "Counter", "Name", "ID_Counter", "Name", "Active='True' AND FK_Client=" + hfClient.Value, "");
        populate.FillDropdownListAll(ddlShift, "Shift", "Name", "ID_Shift", "Name", " Active='True' AND FK_Client=" + hfClient.Value, "");
        populate.FillQuantitySearch(ddlQuantityCriteria);
        populate.FillQuantitySearch(ddlValueCriteria);
        populate.FillGoodsIssueMode(ddlIssueMode);
        populate = null;
    }
    private void BindddlItems()
    {
        Populate populate = new Populate();
        // if (hfINVSubModule.Value == "SL" || hfINVSubModule.Value == "SLR")
        // populate.FillDropdownListAll(ddlItems, "Items", "Name", "ID_Items", "Name", " Active='true' AND FK_Client=" + hfClient.Value, "");
        // else
        //  populate.FillDropdownListAll(ddlItems, "Items", "Name", "ID_Items", "Name", " Active='true' AND ITemtype=2 AND FK_Client=" + hfClient.Value, "");
    }
    public void SetReadonly(params TextBox[] textbox)
    {
        foreach (TextBox txt in textbox)
            txt.Attributes.Add("readonly", "readonly");
    }
    private bool ValidateData(string Value)
    {
        string reportfile = "";
        if ((Value == "STKIL") || (Value == "STKDL") || (Value == "SLSMI") || (Value == "SLSMIC") || (Value == "PRSL") || (Value == "PRCM") || (Value == "PRSMA") || (Value == "STKTR") || (Value == "GENSUPP") || (Value == "STKNEG") || (Value == "STKIM") || (Value == "STKVF") || (Value == "STKVL") || (Value == "GIIR") || (Value == "GIIW") || (Value == "PURREG") || (Value == "PUREG") || (Value == "PURREGDET") || (Value == "PUIW") || (Value == "STKREG") || (Value == "STKREGC") || (Value == "MRIW") || (Value == "SLREG") || (Value == "SLREGDT")||(Value == "BLREG") || (Value == "SLREGC") || (Value == "SAWR") || (Value == "SLABD") || (Value == "CCWR") || (Value == "CWSAL") || (Value == "SLMWREG") || (Value == "SLRREG") || (Value == "SLRREGC") || (Value == "SLVR") || (Value == "SLRIW") || (Value == "SLIW") || (Value == "STLMQ") || (Value == "SLSSTREG") || (Value == "SLSTIW") || (Value == "SLUSREG") || (Value == "SLUSDET") || (Value == "SLCS") || (Value == "VATOP") || (Value == "VATIP") || (Value == "VATPRR") || (Value == "VATSLR") || (Value == "STKMC") || (Value == "SLRCS") || (Value == "SALLIAP"))
        {
            return (ValidateFromToDate());
        }
        return true;
    }
    protected void BindReportMenu()
    {
        try
        {
            BlReportAccess blReportAccess = new BlReportAccess();
            blReportAccess.FK_Client = Convert.ToInt64(hfClient.Value);
            blReportAccess.FK_User = Convert.ToInt64(hfUser.Value);
            DataTable dtbl = new DataTable();
            dtbl = blReportAccess.SelectReportMenu();
            int nodes = 0;
            for (int i = 0; i < dtbl.Rows.Count; i++)
            {
                bool node = false;
                string CatName = dtbl.Rows[i]["CatName"].ToString();
                string CatCode = dtbl.Rows[i]["CatCode"].ToString();
                string SubCatName = dtbl.Rows[i]["SubCatName"].ToString();
                string SubCatCode = dtbl.Rows[i]["SubCatCode"].ToString();
                nodes = tvSettings.Nodes.Count;
                for (int j = 0; j < nodes; j++)
                {
                    if (tvSettings.Nodes[j].Text == CatName)
                    {
                        int cnCount = tvSettings.Nodes[j].ChildNodes.Count;
                        node = true;
                        tvSettings.Nodes[j].ChildNodes.Add(new TreeNode(SubCatName));
                        tvSettings.Nodes[j].ChildNodes[cnCount].Value = SubCatCode;
                    }
                }
                if (node == false)
                {
                    tvSettings.Nodes.Add(new TreeNode(CatName));
                    tvSettings.Nodes[nodes].Value = CatCode;
                    tvSettings.Nodes[nodes].ChildNodes.Add(new TreeNode(SubCatName));
                    tvSettings.Nodes[nodes].ChildNodes[0].Value = SubCatCode;
                }
            }
        }
        catch (Exception ex)
        {

        }
    }
    private Boolean ValidateFromToDate()
    {
        return true;
    }
    protected void CheckUserAccess()
    {
        BlUserAccess bluseraccess = new BlUserAccess();
        Populate populate = new Populate();

        bluseraccess.FK_Users = Convert.ToInt64(hfUser.Value);
        bluseraccess.FK_Pages = 23;

        bluseraccess.FK_Client = Convert.ToInt64(hfClient.Value);
        bluseraccess.CheckUserAccess();
        if (Convert.ToBoolean(bluseraccess.IsView) == false)
        {
            // Response.Redirect("Home.aspx?");
        }
        hfIsAdd.Value = (Convert.ToBoolean(bluseraccess.IsAdd)).ToString();
        hfIsUpdate.Value = (Convert.ToBoolean(bluseraccess.IsUpdate)).ToString();
        hfIsDelete.Value = (Convert.ToBoolean(bluseraccess.IsDelete)).ToString();
        hfIsAcessable.Value = (Convert.ToBoolean(bluseraccess.IsView)).ToString();
        bluseraccess = null;
    }
    private string InitializeReport(BlReportData blReportData, String Value)
    {
        string reportfile = "";
        string ID = string.Empty;
        ID = hfINVSubModule.Value.ToString();
        hfReportType.Value = "SSRS";
        switch (ID)
        {
            case "STK":
                switch (Value)
                {
                    case "STKREG":
                        hfReportType.Value = "CRPT";
                        blReportData.ReportFile = "StockRegister.rpt";
                        // blReportData.ReportFile = "StockRegister.rdlc";
                        blReportData.ReportTable = "StockRegister";
                        blReportData.ReportSchema = "StockRegister";
                        break;
                    case "STKREGC":
                        hfReportType.Value = "CRPT";
                        blReportData.ReportFile = "StockRegisterConsolidated.rpt";
                        // blReportData.ReportFile = "StockRegister.rdlc";
                        blReportData.ReportTable = "StockRegister";
                        blReportData.ReportSchema = "StockRegister";
                        break;
                    case "STKIL":
                        hfReportType.Value = "CRPT";
                        blReportData.ReportFile = "Item.rpt";
                        //blReportData.ReportFile = "Item.rdlc";
                        blReportData.ReportTable = "ItemList";
                        blReportData.ReportSchema = "ItemList";
                        break;
                    case "STKDL":
                        blReportData.ReportFile = "StockDamage.rdlc";
                        blReportData.ReportTable = "StockDamageList";
                        blReportData.ReportSchema = "StockDamageList";
                        break;

                    case "OPSTK":
                        blReportData.ReportFile = "OpeningStock.rdlc";
                        blReportData.ReportTable = "OpeningStockList";
                        blReportData.ReportSchema = "OpeningStockList";
                        break;
                    case "STKVF":
                        hfReportType.Value = "CRPT";
                        blReportData.ReportFile = "crptStockVarification.rpt";
                        blReportData.ReportTable = "StockVarification";
                        blReportData.ReportSchema = "StockVarification";
                        break;
                    case "STKVL":
                        hfReportType.Value = "CRPT";
                        blReportData.ReportFile = "crptStockValues.rpt";
                        blReportData.ReportTable = "crptStockValues";
                        blReportData.ReportSchema = "crptStockValues";
                        //blReportData.ReportFile = "StockVarification.rdlc";
                        //blReportData.ReportTable = "StockVarification";
                        //blReportData.ReportSchema = "StockVarification";
                        break;
                    case "STKMQ":
                        blReportData.ReportFile = "MinimumQuantity.rdlc";
                        blReportData.ReportTable = "MinimumQuantity";
                        blReportData.ReportSchema = "MinimumQuantity";
                        break;
                    case "STKNEG":
                        hfReportType.Value = "CRPT";
                        blReportData.ReportFile = "NegativeStock.rpt";
                        blReportData.ReportTable = "NegativeStock";
                        blReportData.ReportSchema = "NegativeStock";
                        break;
                    case "STKIM":
                        hfReportType.Value = "CRPT";
                        blReportData.ReportFile = "crptItemMovement.rpt";
                        blReportData.ReportTable = "crptItemMovement";
                        blReportData.ReportSchema = "crptItemMovement";
                        break;
                    case "STKMC":
                        hfReportType.Value = "CRPT";
                        blReportData.ReportFile = "MultipleStockCorrection.rpt";
                        blReportData.ReportTable = "MultipleStockCorrection";
                        blReportData.ReportSchema = "MultipleStockCorrection";
                        break;
                }
                break;
            case "GI":
                switch (Value)
                {
                    case "GIIR":
                        blReportData.ReportFile = "GoodsIssueRegister.rdlc";

                        blReportData.ReportTable = "GoodsIssueRegister";
                        blReportData.ReportSchema = "GoodsIssueRegister";
                        break;
                    case "GIIW":
                        blReportData.ReportFile = "GoodsIssueItemWise.rdlc";
                        blReportData.ReportTable = "GoodsIssueItemWise";
                        blReportData.ReportSchema = "GoodsIssueItemWise";
                        break;
                }
                break;
            case "PR":
                switch (Value)
                {
                    case "PRSL":
                        hfReportType.Value = "CRPT";
                        blReportData.ReportFile = "SalesProfitability.rpt";
                        blReportData.ReportTable = "SalesProfitability";
                        blReportData.ReportSchema = "SalesProfitability";
                        break;
                    case "PRCM":
                        hfReportType.Value = "CRPT";
                        blReportData.ReportFile = "CustomerProfitability.rpt";
                        blReportData.ReportTable = "CustomerProfitability";
                        blReportData.ReportSchema = "CustomerProfitability";
                        break;
                    case "PRSMA":
                        hfReportType.Value = "CRPT";
                        blReportData.ReportFile = "SalesManProfitability.rpt";
                        blReportData.ReportTable = "SalesManProfitability";
                        blReportData.ReportSchema = "SalesManProfitability";
                        break;
                }
                break;

            case "VAT":
                switch (Value)
                {
                    case "VATPUR":
                        hfReportType.Value = "CRPT";
                        blReportData.ReportFile = "VatPurchasewise.rpt";//"VatPurchasewise.rdlc";
                        // blReportData.ReportFile = "VatPurchasewise.rdlc";
                        blReportData.ReportTable = "VatPurchasewise";
                        blReportData.ReportSchema = "VatPurchasewise";
                        break;
                    case "VATSAL":
                        hfReportType.Value = "CRPT";
                        blReportData.ReportFile = "VatSaleswise.rpt";//"VatPurchasewise.rdlc";
                        // blReportData.ReportFile = "VatSaleswise.rdlc";
                        blReportData.ReportTable = "VatSaleswise";
                        blReportData.ReportSchema = "VatSaleswise";
                        break;
                    case "VATOP":
                        hfReportType.Value = "CRPT";
                        blReportData.ReportFile = "crptOutputTax.rpt";//"VatPurchasewise.rdlc";
                        // blReportData.ReportFile = "VatSaleswise.rdlc";
                        blReportData.ReportTable = "OutputTax";
                        blReportData.ReportSchema = "OutputTax";
                        break;
                    case "VATIP":
                        hfReportType.Value = "CRPT";
                        blReportData.ReportFile = "VatInputTax.rpt";//"VatPurchasewise.rdlc";
                        // blReportData.ReportFile = "VatSaleswise.rdlc";
                        blReportData.ReportTable = "VatInputTax";
                        blReportData.ReportSchema = "VatInputTax";
                        break;
                    case "VATPRR":
                        hfReportType.Value = "CRPT";
                        blReportData.ReportFile = "VatPurchaseReturnwise.rpt";//"VatPurchasewise.rdlc";
                        // blReportData.ReportFile = "VatSaleswise.rdlc";
                        blReportData.ReportTable = "VatPurchaseReturnwise";
                        blReportData.ReportSchema = "VatPurchaseReturnwise";
                        break;
                    case "VATSLR":
                        hfReportType.Value = "CRPT";
                        blReportData.ReportFile = "VatSalesReturnwise.rpt";//"VatPurchasewise.rdlc";
                        // blReportData.ReportFile = "VatSaleswise.rdlc";
                        blReportData.ReportTable = "VatSalesReturnwise";
                        blReportData.ReportSchema = "VatSalesReturnwise";
                        break;
                    case "GSTSTS":
                        hfReportType.Value = "CRPT";
                        blReportData.ReportFile = "GSTSalesTaxSummary.rpt";
                        blReportData.ReportTable = "GSTSalesTaxSummary";
                        blReportData.ReportSchema = "GSTSalesTaxSummary";
                        break;
                    case "GSTHSN":
                        hfReportType.Value = "CRPT";
                        blReportData.ReportFile = "GSTHSNCodeReport.rpt";
                        blReportData.ReportTable = "GSTHSNCodeReport";
                        blReportData.ReportSchema = "GSTHSNCodeReport";
                        break;
                    case "GSTHSN1":
                        hfReportType.Value = "CRPT";
                        blReportData.ReportFile = "GSTHSN1CodeReport.rpt";
                        blReportData.ReportTable = "GSTHSN1CodeReport";
                        blReportData.ReportSchema = "GSTHSN1CodeReport";
                        break;
                    case "GSTS":
                        hfReportType.Value = "CRPT";
                        blReportData.ReportFile = "GSTSales.rpt";
                        blReportData.ReportTable = "GSTSales";
                        blReportData.ReportSchema = "GSTSales";
                        break;
                    case "GSTSIB":
                        hfReportType.Value = "CRPT";
                        blReportData.ReportFile = "GSTSalesInvoiceAndBill.rpt";
                        blReportData.ReportTable = "GSTSalesInvoiceAndBill";
                        blReportData.ReportSchema = "GSTSalesInvoiceAndBill";
                        break;
                    case "GSTPIB":
                        hfReportType.Value = "CRPT";
                        blReportData.ReportFile = "GSTPurchaseInvoiceAndBill.rpt";
                        blReportData.ReportTable = "GSTPurchaseInvoiceAndBill";
                        blReportData.ReportSchema = "GSTPurchaseInvoiceAndBill";
                        break;
                        
                        
                }
                break;
            case "PU":
                switch (Value)
                {
                    case "PUREG":
                        hfReportType.Value = "CRPT";
                        blReportData.ReportFile = "crptPurchaseRegister.rpt";
                        //  blReportData.ReportFile = "PurchaseRegister.rdlc";
                        blReportData.ReportTable = "PurchaseRegister";
                        blReportData.ReportSchema = "PurchaseRegister";
                        break;
                    case "PURREGDET":
                        hfReportType.Value = "CRPT";
                        blReportData.ReportFile = "PurchaseRegisterDetailed.rpt";
                        //  blReportData.ReportFile = "PurchaseRegister.rdlc";
                        blReportData.ReportTable = "PurchaseRegisterDetailed";
                        blReportData.ReportSchema = "PurchaseRegisterDetailed";
                        break;
                    case "PUIW":
                        hfReportType.Value = "CRPT";
                        blReportData.ReportFile = "crptPurchaseItemWise.rpt";
                        //blReportData.ReportFile = "PurchaseItemWise.rdlc";                        
                        blReportData.ReportTable = "PurchaseItemWise";
                        blReportData.ReportSchema = "PurchaseItemWise";
                        break;
                    case "PUIWSP":
                        blReportData.ReportFile = "PurchaseItemWiseSupplier.rdlc";
                        blReportData.ReportTable = "PurchaseItemWiseSupplier";
                        blReportData.ReportSchema = "PurchaseItemWiseSupplier";
                        break;
                    case "PUSPIW":
                        hfReportType.Value = "CRPT";
                        blReportData.ReportFile = "crptPurchaseSupplierWise.rpt";
                        //  blReportData.ReportFile = "PurchaseSupplierWise.rdlc";
                        blReportData.ReportTable = "PurchaseSupplierWise";
                        blReportData.ReportSchema = "PurchaseSupplierWise";
                        break;
                    case "SUPPI":
                        hfReportType.Value = "CRPT";
                        blReportData.ReportFile = "SupplierwisePurchaseItems.rpt";
                        blReportData.ReportTable = "SupplierwisePurchaseItems";
                        blReportData.ReportSchema = "SupplierwisePurchaseItems";
                        break;
                        
                }
                break;
            case "PUR":
                switch (Value)
                {
                    case "PURREG":
                        blReportData.ReportFile = "PurchaseReturnRegister.rdlc";
                        blReportData.ReportTable = "PurchaseReturnRegister";
                        blReportData.ReportSchema = "PurchaseReturnRegister";
                        break;
                    case "PURIW":
                        hfReportType.Value = "CRPT";
                        blReportData.ReportFile = "PurchaseReturnItemwise.rpt";
                        // blReportData.ReportFile = "PurchaseReturnItemwise.rdlc";
                        blReportData.ReportTable = "PurchaseReturnItemwise";
                        blReportData.ReportSchema = "PurchaseReturnItemwise";
                        break;
                }
                break;

            case "MR":
                switch (Value)
                {
                    case "MRIW":
                        blReportData.ReportFile = "MaterialReceiptItemwise.rdlc";
                        blReportData.ReportTable = "MaterialReceiptItemwise";
                        blReportData.ReportSchema = "MaterialReceiptItemwise";
                        break;
                    case "MREXP":
                        blReportData.ReportFile = "ExpiredItem.rdlc";
                        blReportData.ReportTable = "ExpiredItem";
                        blReportData.ReportSchema = "ExpiredItem";
                        break;
                }

                break;
            case "GEN":
                switch (Value)
                {
                    case "GENSUPP":
                        hfReportType.Value = "CRPT";
                        blReportData.ReportFile = "SupplierList.rpt";
                        blReportData.ReportTable = "SupplierList";
                        blReportData.ReportSchema = "SupplierList";
                        break;

                }

                break;
            case "SL":
                switch (Value)
                {
                    case "SLREG":
                        hfReportType.Value = "CRPT";
                        blReportData.ReportFile = "SalesRegister.rpt";
                        // blReportData.ReportFile = "SalesRegister.rdlc";
                        blReportData.ReportTable = "SalesRegister";
                        blReportData.ReportSchema = "SalesRegister";
                        break;
                    case "SLREGDT":
                        hfReportType.Value = "CRPT";
                        blReportData.ReportFile = "SalesRegisterDetails.rpt";
                        // blReportData.ReportFile = "SalesRegister.rdlc";
                        blReportData.ReportTable = "SalesRegisterDetailed";
                        blReportData.ReportSchema = "SalesRegisterDetailed";
                        break;
                    case "SLMWREG":
                        blReportData.ReportFile = "SalesmanWiseRegister.rdlc";
                        blReportData.ReportTable = "SalesmanWiseRegister";
                        blReportData.ReportSchema = "SalesmanWiseRegister";
                        break;
                    case "BLREG":
                        hfReportType.Value = "CRPT";
                        blReportData.ReportFile = "BillRegister.rpt";
                        // blReportData.ReportFile = "BillRegister.rdlc";
                        blReportData.ReportTable = "BillRegister";
                        blReportData.ReportSchema = "BillRegister";
                        break;
                    case "SAWR":
                        hfReportType.Value = "CRPT";
                        blReportData.ReportFile = "SalesAbstract.rpt";//"SalesAbstractReport.rdlc";
                        blReportData.ReportTable = "SalesAbstractReport";
                        blReportData.ReportSchema = "SalesAbstractReport";
                        break;
                    case "SLABD":
                        hfReportType.Value = "CRPT";
                        blReportData.ReportFile = "SalesAbstractDateWise.rpt";//"SalesAbstractReport.rdlc";
                        blReportData.ReportTable = "SalesAbstractDateWise";
                        blReportData.ReportSchema = "SalesAbstractDateWise";
                        break;
                    case "SLMNTH":
                        hfReportType.Value = "CRPT";
                        blReportData.ReportFile = "MonthlySales.rpt";
                        blReportData.ReportTable = "MonthlySales";
                        blReportData.ReportSchema = "MonthlySales";
                        break;
                    case "CCWR":
                        blReportData.ReportFile = "CounterCloseReport.rdlc";
                        blReportData.ReportTable = "CounterCloseReport";
                        blReportData.ReportSchema = "CounterCloseReport";
                        break;
                    case "CWSAL":
                        blReportData.ReportFile = "CounterWiseSales.rdlc";
                        blReportData.ReportTable = "CounterWiseSales";
                        blReportData.ReportSchema = "CounterWiseSales";
                        break;
                    case "SLIW":
                        blReportData.ReportFile = "SaleItemWise.rdlc";
                        blReportData.ReportTable = "SalesItemWise";
                        blReportData.ReportSchema = "SalesItemWise";
                        break;
                    case "SLSTREG":
                        blReportData.ReportFile = "SalesStoreWiseRegister.rdlc";
                        blReportData.ReportTable = "SalesStoreWiseRegister";
                        blReportData.ReportSchema = "SalesStoreWiseRegister";
                        break;
                    case "SLSTDET":
                        blReportData.ReportFile = "SalesStoreWiseDetail.rdlc";
                        blReportData.ReportTable = "SalesStoreWiseDetail";
                        blReportData.ReportSchema = "SalesStoreWiseDetail";
                        break;

                    case "SLUSREG":
                        blReportData.ReportFile = "SaleUserWise.rdlc";
                        blReportData.ReportTable = "SaleUserWise";
                        blReportData.ReportSchema = "SaleUserWise";
                        break;

                    case "SLCS":
                        hfReportType.Value = "CRPT";
                        blReportData.ReportFile = "crptCustomerWiseSales.rpt";
                        // blReportData.ReportFile = "SaleCustomerWise.rdlc";
                        blReportData.ReportTable = "SaleCustomerWise";
                        blReportData.ReportSchema = "SaleCustomerWise";
                        break;
                    case "SLCSST":
                        blReportData.ReportFile = "CustomerStatement.rdlc";
                        blReportData.ReportTable = "CustomerStatement";
                        blReportData.ReportSchema = "CustomerStatement";
                        break;
                    case "SLNPCS":
                        blReportData.ReportFile = "NonPerformingCustomer.rdlc";
                        blReportData.ReportTable = "NonPerformingCustomer";
                        blReportData.ReportSchema = "NonPerformingCustomer";
                        break;
                    case "SLIM":
                        blReportData.ReportFile = "ItemMovement.rdlc";
                        blReportData.ReportTable = "ItemMovement";
                        blReportData.ReportSchema = "ItemMovement";
                        break;
                    case "SLVR":
                        blReportData.ReportFile = "Voucher.rdlc";
                        blReportData.ReportTable = "Voucher";
                        blReportData.ReportSchema = "Voucher";
                        break;
                    case "SUPS":
                        hfReportType.Value = "CRPT";
                        blReportData.ReportFile = "Supplierwisesales.rpt";
                        blReportData.ReportTable = "Supplierwisesales";
                        blReportData.ReportSchema = "Supplierwisesales";
                        break;
                    case "SLSMI":
                        hfReportType.Value = "CRPT";
                        blReportData.ReportFile = "SalesManIncentive.rpt";
                        blReportData.ReportTable = "SalesManIncentive";
                        blReportData.ReportSchema = "SalesManIncentive";
                        break;
                    case "SLSMIC":
                        hfReportType.Value = "CRPT";
                        blReportData.ReportFile = "SalesManIncentiveConsolidate.rpt";
                        blReportData.ReportTable = "SalesManIncentiveConsolidate";
                        blReportData.ReportSchema = "SalesManIncentiveConsolidate";
                        break;
                    case "SLREGC":
                        hfReportType.Value = "CRPT";
                        blReportData.ReportFile = "SalesRegisterCategoryWise.rpt";
                        blReportData.ReportTable = "SalesRegisterCategoryWise";
                        blReportData.ReportSchema = "SalesRegisterCategoryWise";
                        break;
                    case "SUPSI":
                        hfReportType.Value = "CRPT";
                        blReportData.ReportFile = "SupplierwiseSalesItems.rpt";
                        blReportData.ReportTable = "SupplierwiseSalesItems";
                        blReportData.ReportSchema = "SupplierwiseSalesItems";
                        break;
                    case "SLREGH":
                        hfReportType.Value = "CRPT";
                        blReportData.ReportFile = "SalesRegisterHSN.rpt";
                        blReportData.ReportTable = "SalesRegisterHSN";
                        blReportData.ReportSchema = "SalesRegisterHSN";
                        break;
                     
                }
                break;
            case "SLR":
                switch (Value)
                {
                    case "SLRREG":
                        hfReportType.Value = "CRPT";
                        //  blReportData.ReportFile = "SalesReturnRegister.rdlc";
                        blReportData.ReportFile = "SalesReturnRegister.rpt";
                        blReportData.ReportTable = "SalesReturnRegister";
                        blReportData.ReportSchema = "SalesReturnRegister";
                        break;

                    case "SLRIW":
                        blReportData.ReportFile = "SalesReturnItemWise.rdlc";
                        blReportData.ReportTable = "SalesReturnItemWise";
                        blReportData.ReportSchema = "SalesReturnItemWise";
                        break;
                    case "SLRREGC":
                        hfReportType.Value = "CRPT";
                        blReportData.ReportFile = "SalesReturnRegisterCategoryWise.rpt";
                        blReportData.ReportTable = "SalesReturnRegisterCategoryWise";
                        blReportData.ReportSchema = "SalesReturnRegisterCategoryWise";
                        break;
                    case "SLRCS":
                        hfReportType.Value = "CRPT";
                        blReportData.ReportFile = "crptCustomerWiseSalesReturn.rpt";
                        blReportData.ReportTable = "crptCustomerWiseSalesReturn";
                        blReportData.ReportSchema = "crptCustomerWiseSalesReturn";
                        break;
                }
                break;
            case "TFR":
                switch (Value)
                {
                    case "TFRREG":
                        blReportData.ReportFile = "StockTransfer.rdlc";
                        blReportData.ReportTable = "StockTransfer";
                        blReportData.ReportSchema = "StockTransfer";
                        break;
                    case "TFRPRC":
                        blReportData.ReportFile = "PriceConversion.rdlc";
                        blReportData.ReportTable = "PriceConversion";
                        blReportData.ReportSchema = "PriceConversion";
                        break;
                    case "TFRITC":
                        blReportData.ReportFile = "StockTransfer.rdlc";
                        blReportData.ReportTable = "StockTransfer";
                        blReportData.ReportSchema = "StockTransfer";
                        break;
                    case "TFRODR":
                        hfReportType.Value = "CRPT";
                        blReportData.ReportFile = "StockTransferOrder.rpt";
                        blReportData.ReportTable = "StockTransferOrder";
                        blReportData.ReportSchema = "StockTransferOrder";
                        break;
                }
                break;
            case "SHICL":
                switch (Value)
                {
                    case "SHCL":
                        blReportData.ReportFile = "ShiftClose.rdlc";
                        blReportData.ReportTable = "ShiftClose";
                        blReportData.ReportSchema = "ShiftClose";
                        break;
                }
                break;
            case "LIA":
                switch (Value)
                {
                    case "SALLIA":
                        hfReportType.Value = "CRPT";
                        blReportData.ReportFile = "SalesmanLiabilityRegister.rpt";
                        //  blReportData.ReportFile = "SalesmanLiabilityRegister.rdlc";
                        blReportData.ReportTable = "SalesmanLiabilityRegister";
                        blReportData.ReportSchema = "SalesmanLiabilityRegister";
                        break;
                    case "SALLIAP":
                        hfReportType.Value = "CRPT";
                        blReportData.ReportFile = "SalesmanLiabilityRegisterPeriodWise.rpt";
                        //  blReportData.ReportFile = "SalesmanLiabilityRegister.rdlc";
                        blReportData.ReportTable = "SalesmanLiabilityRegisterPeriodWise";
                        blReportData.ReportSchema = "SalesmanLiabilityRegisterPeriodWise";
                        break;
                }
                break;
            case "SD":
                switch (Value)
                {
                    case "SDPR":
                        hfReportType.Value = "CRPT";
                        blReportData.ReportFile = "ScheduleProcessedList.rpt";
                        blReportData.ReportTable = "ScheduleProcessedList";
                        blReportData.ReportSchema = "ScheduleProcessedList";
                        break;
                    case "SDPO":
                        hfReportType.Value = "CRPT";
                        blReportData.ReportFile = "SchedulePostedList.rpt";
                        blReportData.ReportTable = "SchedulePostedList";
                        blReportData.ReportSchema = "SchedulePostedList";
                        break;
                    case "SDDF":
                        hfReportType.Value = "CRPT";
                        blReportData.ReportFile = "ScheduleDifferenceList.rpt";
                        blReportData.ReportTable = "ScheduleDifferenceList";
                        blReportData.ReportSchema = "ScheduleDifferenceList";
                        break;
                }
                break;
            case "PO":
                switch (Value)
                {
                    case "POSTW":
                        hfReportType.Value = "CRPT";
                        blReportData.ReportFile = "PurchaseOrderStoreWise.rpt";
                        blReportData.ReportTable = "PurchaseOrderStoreWise";
                        blReportData.ReportSchema = "PurchaseOrderStoreWise";
                        break;
                }
                break;
        }
        return reportfile;
    }
    protected bool UpdateData(BlReportData BlReportData)
    {
        try
        {
            BlReportData.ID = hfINVSubModule.Value.ToString();
            BlReportData.SubID = tvSettings.SelectedValue.ToString();
            BlReportData.FK_CategoryFirst = Convert.ToInt64(ddlCategoryFirst.SelectedValue);
            BlReportData.FK_Store = Convert.ToInt64(ddlStore.SelectedValue);
            BlReportData.FromStore = Convert.ToInt64(ddlFromStore.SelectedValue);
            BlReportData.ToStore = Convert.ToInt64(ddlToStore.SelectedValue);
            BlReportData.FromDate = Convert.ToDateTime(txtFromDate.Text);
            BlReportData.AsonDate = Convert.ToDateTime(txtAsOnDate.Text);
            BlReportData.ToDate = Convert.ToDateTime(txtToDate.Text);
            BlReportData.FK_Items = Convert.ToInt64(txtItemRefno.Text == "" ? "0" : txtItemRefno.Text.Trim());
            BlReportData.FK_Client = Convert.ToInt64(hfClient.Value);
            BlReportData.FK_Users = Convert.ToInt64(ddlUser.SelectedValue);
            BlReportData.FK_Supplier = Convert.ToInt64(TxtSupplierRefNo.Text == "" ? "0" : TxtSupplierRefNo.Text.Trim());
            BlReportData.FK_Customer = Convert.ToInt64(txtCustRefno.Text == "" ? "0" : txtCustRefno.Text.Trim());      ////ddlCustomer.SelectedValue
            BlReportData.FK_Salesman = Convert.ToInt64(ddlEmployee.SelectedValue);
            BlReportData.FK_Counter = Convert.ToInt64(ddlCounter.SelectedValue);
            BlReportData.FK_Shift = Convert.ToInt64(ddlShift.SelectedValue);
            BlReportData.UserCode = hfUser.Value;
            BlReportData.ReportSchemaPath = Server.MapPath("Schema/");
            //BlReportData.ReportPath = Server.MapPath("Reports/");
            BlReportData.ReportPath = Server.MapPath("SSRS/");
            BlReportData.DatasetName = "INVENTORYREPORTS";
            //BlReportData.Title = "The Mannarkkad Rural Service Co-operative Bank Ltd (No.P-922)";
            //BlReportData.Title = "Cochin Shipyard Employees Consumer Co-operative Society Ltd (No. E-750)";
            //BlReportData.Title = "Kozhikode Thaluk Sahakarana Karshikolpadana Samskarana Vipanana Sangam Ltd- (D No.3043)";
            // BlReportData.Title = "Wayanad Dist Drivers Co-operative Society Ltd.,No. W.14";
            //BlReportData.Title = "Kozhikode District Vanitha Beauticians Welfare Co-Op. Society Ltd.";
            ///  BlReportData.Title = "NIT Co-opertative Employees Multipurpose Store";

            string s = BlReportData.Title;
            BlReportData.Title = s;


            if (BlReportData.SubID == "STKIL")
            {
                BlReportData.PageTitle = "STOCK ITEM LIST";
            }
            else if (BlReportData.SubID == "STKREG")
            {
                BlReportData.PageTitle = "STOCK REGISTER LIST From : " + BlReportData.FromDate.ToShortDateString() + "  To : " + BlReportData.ToDate.ToShortDateString();
            }
            else if (BlReportData.SubID == "STKREGC")
            {
                BlReportData.PageTitle = "STOCK REGISTER LIST From : " + BlReportData.FromDate.ToShortDateString() + "  To : " + BlReportData.ToDate.ToShortDateString();
            }
            else if (BlReportData.SubID == "STKDL")
            {
                BlReportData.PageTitle = "Damaged Item List From : " + BlReportData.FromDate.ToShortDateString() + "  To : " + BlReportData.ToDate.ToShortDateString();
            }
            else if (BlReportData.SubID == "STKVF")
            {
                BlReportData.FK_Store = Convert.ToInt64(ddlStore.SelectedValue);
                BlReportData.FK_CategoryFirst = Convert.ToInt64(ddlCategoryFirst.SelectedValue);
                BlReportData.PageTitle = "Current Stock Report  "; //As on Date  : " + BlReportData.AsonDate.ToShortDateString();
            }
            else if (BlReportData.SubID == "STKVL")
            {
                BlReportData.FK_Store = Convert.ToInt64(ddlStore.SelectedValue);
                BlReportData.PageTitle = "Stock Values Report  "; //As on Date  : " + BlReportData.AsonDate.ToShortDateString();
                BlReportData.Critrea1 = ddlValueCriteria.SelectedValue;
                BlReportData.Critrea2 = txtValues.Text;
            }
            else if (BlReportData.SubID == "STKNEG")
            {
                BlReportData.PageTitle = "NEGATIVE STOCK LIST ";
            }
            else if (BlReportData.SubID == "STKIM")
            {
                BlReportData.PageTitle = "Item Movement Register From : " + BlReportData.FromDate.ToShortDateString() + "  To : " + BlReportData.ToDate.ToShortDateString();

            }
            else if (BlReportData.SubID == "STKMQ")
            {
                BlReportData.FK_Store = Convert.ToInt64(ddlStore.SelectedValue);
                BlReportData.PageTitle = "Minimum Quantity Items  ";
            }
            else if (BlReportData.SubID == "STKMC")
            {
                BlReportData.PageTitle = "Multiple Stock Correction From : " + BlReportData.FromDate.ToShortDateString() + "  To : " + BlReportData.ToDate.ToShortDateString();

            }
            if (BlReportData.SubID == "GIIR")
            {
                BlReportData.IssueMode = Convert.ToByte(ddlIssueMode.SelectedValue.ToString());
                BlReportData.PageTitle = "Goods Issue Register From : " + BlReportData.FromDate.ToShortDateString() + "  To : " + BlReportData.ToDate.ToShortDateString();
            }
            if (BlReportData.SubID == "GIIW")
            {
                BlReportData.IssueMode = Convert.ToByte(ddlIssueMode.SelectedValue.ToString());
                // BlReportData.FK_Items = Convert.ToInt64(ddlItems.SelectedValue);
                BlReportData.PageTitle = "Goods Issue Item-Wise Report From : " + BlReportData.FromDate.ToShortDateString() + "  To : " + BlReportData.ToDate.ToShortDateString();
            }
            if (BlReportData.SubID == "PUREG" || BlReportData.SubID == "PURREGDET")
            {
                BlReportData.PageTitle = "Purchase Register Report From : " + BlReportData.FromDate.ToShortDateString() + "  To : " + BlReportData.ToDate.ToShortDateString();
            }
            if (BlReportData.SubID == "PUIWSP")
            {
                // BlReportData.FK_Supplier = Convert.ToInt64(ddlSupplier.SelectedValue);
                BlReportData.PageTitle = "Purchase Item-Wise Supplier Report From : " + BlReportData.FromDate.ToShortDateString() + "  To : " + BlReportData.ToDate.ToShortDateString();
            }
            if (BlReportData.SubID == "PUIW")
            {
                BlReportData.PageTitle = "Purchase Item-Wise Report From : " + BlReportData.FromDate.ToShortDateString() + "  To : " + BlReportData.ToDate.ToShortDateString();
            }
            if (BlReportData.SubID == "PUSPIW")
            {
                BlReportData.PageTitle = "Supplier Wise Purchase Report From : " + BlReportData.FromDate.ToShortDateString() + "  To : " + BlReportData.ToDate.ToShortDateString();
            }
            if (BlReportData.SubID == "SUPS")
            {
                BlReportData.PageTitle = "Supplier Wise Sales Report From : " + BlReportData.FromDate.ToShortDateString() + "  To : " + BlReportData.ToDate.ToShortDateString();
            }
            if (BlReportData.SubID == "SUPSI")
            {
                BlReportData.PageTitle = "Supplier Wise Sales Item Report From : " + BlReportData.FromDate.ToShortDateString() + "  To : " + BlReportData.ToDate.ToShortDateString();
            }
            if (BlReportData.SubID == "SLREGH")
            {
                BlReportData.Critrea1 = ddlSaleMode.SelectedValue;
                BlReportData.PageTitle = "Sales Register-HSN Code Report From : " + BlReportData.FromDate.ToShortDateString() + "  To : " + BlReportData.ToDate.ToShortDateString();
            }
            else if (BlReportData.SubID == "SUPPI")
            {
                BlReportData.PageTitle = "Supplier Wise Purchase Item Report From : " + BlReportData.FromDate.ToShortDateString() + "  To : " + BlReportData.ToDate.ToShortDateString();
            }
            if (BlReportData.SubID == "SLSMI")
            {
                BlReportData.PageTitle = "SalesMan Incentive Report From : " + BlReportData.FromDate.ToShortDateString() + "  To : " + BlReportData.ToDate.ToShortDateString();
            }
            if (BlReportData.SubID == "SLSMIC")
            {
                BlReportData.PageTitle = "SalesMan Incentive Consolidate Report From : " + BlReportData.FromDate.ToShortDateString() + "  To : " + BlReportData.ToDate.ToShortDateString();
            }
            if (BlReportData.SubID == "PRSL")
            {
                BlReportData.PageTitle = "Sales Profitability Report From : " + BlReportData.FromDate.ToShortDateString() + "  To : " + BlReportData.ToDate.ToShortDateString();
            }
            if (BlReportData.SubID == "PRCM")
            {
                BlReportData.PageTitle = "Customer Profitability Report From : " + BlReportData.FromDate.ToShortDateString() + "  To : " + BlReportData.ToDate.ToShortDateString();
            }
            if (BlReportData.SubID == "PRSMA")
            {
                BlReportData.PageTitle = "SalesMan Analysis Profitability Report From : " + BlReportData.FromDate.ToShortDateString() + "  To : " + BlReportData.ToDate.ToShortDateString();
            }
            if (BlReportData.SubID == "PURREG")
            {
                BlReportData.PageTitle = "Purchase Return Report From : " + BlReportData.FromDate.ToShortDateString() + "  To : " + BlReportData.ToDate.ToShortDateString();
            }
            if (BlReportData.SubID == "PURIW")
            {
                BlReportData.PageTitle = "Purchase Return Item-Wise Report From : " + BlReportData.FromDate.ToShortDateString() + "  To : " + BlReportData.ToDate.ToShortDateString();
            }
            if (BlReportData.SubID == "MRIW")
            {
                BlReportData.PageTitle = "Material Receipt Item-Wise Report From : " + BlReportData.FromDate.ToShortDateString() + "  To : " + BlReportData.ToDate.ToShortDateString();
            }
            if (BlReportData.SubID == "MREXP")
            {
                BlReportData.PageTitle = "Expired Item Report From : " + BlReportData.FromDate.ToShortDateString() + "  To : " + BlReportData.ToDate.ToShortDateString();
            }
            if (BlReportData.SubID == "SLREG")
            {
                //BlReportData.PageTitle = "SALES REGISTER LIST From : " + BlReportData.FromDate.ToShortDateString() + "  To : " + BlReportData.ToDate.ToShortDateString();
                BlReportData.Critrea1 = ddlSaleMode.SelectedValue;
                // BlReportData.Critrea2 = ddlPaymentType.SelectedValue;
                BlReportData.PageTitle = "Sales Register Report From : " + BlReportData.FromDate.ToShortDateString() + "  To : " + BlReportData.ToDate.ToShortDateString();

            }
            if (BlReportData.SubID == "SLREGDT")
            {
                //BlReportData.PageTitle = "SALES REGISTER LIST From : " + BlReportData.FromDate.ToShortDateString() + "  To : " + BlReportData.ToDate.ToShortDateString();
                BlReportData.Critrea1 = ddlSaleMode.SelectedValue;
                // BlReportData.Critrea2 = ddlPaymentType.SelectedValue;
                BlReportData.PageTitle = "Sales Register Detailed Report From : " + BlReportData.FromDate.ToShortDateString() + "  To : " + BlReportData.ToDate.ToShortDateString();

            }
            if (BlReportData.SubID == "BLREG")
            {
                BlReportData.Critrea1 = ddlSaleMode.SelectedValue;
                // BlReportData.Critrea2 = ddlPaymentType.SelectedValue;
                BlReportData.PageTitle = "Bill Register Report From : " + BlReportData.FromDate.ToShortDateString() + "  To : " + BlReportData.ToDate.ToShortDateString();
            }
            if (BlReportData.SubID == "SLREGC")
            {
                //BlReportData.PageTitle = "SALES REGISTER LIST From : " + BlReportData.FromDate.ToShortDateString() + "  To : " + BlReportData.ToDate.ToShortDateString();
                BlReportData.Critrea1 = ddlSaleMode.SelectedValue;
                // BlReportData.Critrea2 = ddlPaymentType.SelectedValue;
                BlReportData.PageTitle = "Sales Register-Category Wise Report From : " + BlReportData.FromDate.ToShortDateString() + "  To : " + BlReportData.ToDate.ToShortDateString();

            }
            if (BlReportData.SubID == "CCWR")
            {
                //BlReportData.Critrea1 = ddlSaleMode.SelectedValue;
                // BlReportData.Critrea2 = ddlPaymentType.SelectedValue;
                BlReportData.PageTitle = "Counter Close Report From : " + BlReportData.FromDate.ToShortDateString() + "  To : " + BlReportData.ToDate.ToShortDateString();
            }
            if (BlReportData.SubID == "CWSAL")
            {
                //BlReportData.Critrea1 = ddlSaleMode.SelectedValue;
                BlReportData.PageTitle = "Counter Wise Sale Report From : " + BlReportData.FromDate.ToShortDateString() + "  To : " + BlReportData.ToDate.ToShortDateString();
            }
            if (BlReportData.SubID == "SAWR")
            {
                //BlReportData.Critrea1 = ddlSaleMode.SelectedValue;
                BlReportData.PageTitle = "Sales Abstract Report From : " + BlReportData.FromDate.ToShortDateString() + "  To : " + BlReportData.ToDate.ToShortDateString();
            }
            if (BlReportData.SubID == "SLABD")
            {
                //BlReportData.Critrea1 = ddlSaleMode.SelectedValue;
                BlReportData.PageTitle = "Sales Abstract Date Wise Report From : " + BlReportData.FromDate.ToShortDateString() + "  To : " + BlReportData.ToDate.ToShortDateString();
            }
            if (BlReportData.SubID == "SLMWREG")
            {
                BlReportData.PageTitle = "Sales man Wise Register Report From : " + BlReportData.FromDate.ToShortDateString() + "  To : " + BlReportData.ToDate.ToShortDateString();
            }
            if (BlReportData.SubID == "SLRREG")
            {
                BlReportData.Critrea1 = ddlPaymentType.SelectedValue;
                BlReportData.PageTitle = "Sales Return Register Report From : " + BlReportData.FromDate.ToShortDateString() + "  To : " + BlReportData.ToDate.ToShortDateString();
            }
            if (BlReportData.SubID == "SLRREGC")
            {
                BlReportData.Critrea1 = ddlPaymentType.SelectedValue;
                BlReportData.PageTitle = "Sales Return Register-Category Wise Report From : " + BlReportData.FromDate.ToShortDateString() + "  To : " + BlReportData.ToDate.ToShortDateString();
            }
            if (BlReportData.SubID == "SLRIW")
            {

                BlReportData.Critrea1 = ddlPaymentType.SelectedValue;
                BlReportData.PageTitle = "Sales Return  Item Wise Report From : " + BlReportData.FromDate.ToShortDateString() + "  To : " + BlReportData.ToDate.ToShortDateString();
            }
            if (BlReportData.SubID == "SLIW")
            {
                BlReportData.Critrea1 = ddlSaleMode.SelectedValue;
                BlReportData.PageTitle = "Sales Item Wise Report From : " + BlReportData.FromDate.ToShortDateString() + "  To : " + BlReportData.ToDate.ToShortDateString();
            }
            if (BlReportData.SubID == "SLSTREG")
            {
                BlReportData.PageTitle = "Sales Store Wise Register Report From : " + BlReportData.FromDate.ToShortDateString() + "  To : " + BlReportData.ToDate.ToShortDateString();
            }
            if (BlReportData.SubID == "SLSTDET")
            {
                BlReportData.PageTitle = "Sales Store Wise Detail Report From : " + BlReportData.FromDate.ToShortDateString() + "  To : " + BlReportData.ToDate.ToShortDateString();
            }
            if (BlReportData.SubID == "SLSTIW")
            {
                BlReportData.PageTitle = "Sales Store Wise-Item Wise Report From : " + BlReportData.FromDate.ToShortDateString() + "  To : " + BlReportData.ToDate.ToShortDateString();
            }
            if (BlReportData.SubID == "SLCS")
            {
                BlReportData.Critrea1 = ddlSaleMode.SelectedValue;
                BlReportData.PageTitle = "Sales Customer Wise Report From : " + BlReportData.FromDate.ToShortDateString() + "  To : " + BlReportData.ToDate.ToShortDateString();
            }
            if (BlReportData.SubID == "SLCSST")
            {
                BlReportData.FK_Customer = Convert.ToInt64(ddlCustomer2.SelectedValue);
                BlReportData.PageTitle = "Sales Customer Statement Report From : " + BlReportData.FromDate.ToShortDateString() + "  To : " + BlReportData.ToDate.ToShortDateString();
            }
            if (BlReportData.SubID == "SLMNTH")
            {
                //BlReportData.PageTitle = "Sales Report From : " + BlReportData.FromDate.ToShortDateString() + "  To : " + BlReportData.ToDate.ToShortDateString();
                BlReportData.PageTitle = "MONTHLY SALES REPORT -" + CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(DateTime.Parse(txtAsOnDate.Text).Month) + " " + DateTime.Parse(txtAsOnDate.Text).Year;
            }
            if (BlReportData.SubID == "SLNPCS")
            {
                BlReportData.Critrea1 = txtNoSales.Text.ToString();
                BlReportData.FK_CustomerGroup = Convert.ToInt64(ddlCustomerGrp.SelectedValue);
                BlReportData.PageTitle = "NonPerforming Customer Register Report From : " + BlReportData.FromDate.ToShortDateString() + "  To : " + BlReportData.ToDate.ToShortDateString();
            }
            if (BlReportData.SubID == "SLUSREG")
            {
                BlReportData.PageTitle = "Sales User Wise Register Report From : " + BlReportData.FromDate.ToShortDateString() + "  To : " + BlReportData.ToDate.ToShortDateString();
            }
            if (BlReportData.SubID == "SLUSDET")
            {
                BlReportData.PageTitle = "Sales User Wise Detail Report From : " + BlReportData.FromDate.ToShortDateString() + "  To : " + BlReportData.ToDate.ToShortDateString();
            }
            if (BlReportData.SubID == "SLIM")
            {
                BlReportData.Critrea1 = ddlQuantityCriteria.SelectedValue;
                BlReportData.Critrea2 = txtQauntity.Text;
                BlReportData.PageTitle = "Item Movement Report From : " + BlReportData.FromDate.ToShortDateString() + "  To : " + BlReportData.ToDate.ToShortDateString();
            }
            if (BlReportData.SubID == "SLVR")
            {
                BlReportData.FK_Store = Convert.ToInt64(ddlStore.SelectedValue);
                BlReportData.Critrea1 = txtBillNo.Text;
                BlReportData.PageTitle = "Voucher Report From : " + BlReportData.FromDate.ToShortDateString() + "  To : " + BlReportData.ToDate.ToShortDateString();
            }
            if (BlReportData.SubID == "SHCL")
            {
                BlReportData.FK_Counter = Convert.ToInt64(ddlCounter.SelectedValue);
                BlReportData.FK_Shift = Convert.ToInt64(ddlShift.SelectedValue);
                BlReportData.PageTitle = "Shift Report From : " + BlReportData.FromDate.ToShortDateString() + "  To : " + BlReportData.ToDate.ToShortDateString();
            }
            if (BlReportData.SubID == "VATPUR")
            {
                BlReportData.PageTitle = "Purchasewise VAT Report From : " + BlReportData.FromDate.ToShortDateString() + "  To : " + BlReportData.ToDate.ToShortDateString();
            }
            if (BlReportData.SubID == "VATSAL")
            {
                BlReportData.PageTitle = "Saleswise GST Report From : " + BlReportData.FromDate.ToShortDateString() + "  To : " + BlReportData.ToDate.ToShortDateString();
            }
            if (BlReportData.SubID == "VATPRR")
            {
                BlReportData.PageTitle = "Purchase Returnwise VAT Report From : " + BlReportData.FromDate.ToShortDateString() + "  To : " + BlReportData.ToDate.ToShortDateString();
            }
            if (BlReportData.SubID == "VATSLR")
            {
                BlReportData.PageTitle = "Sales Returnwise VAT Report From : " + BlReportData.FromDate.ToShortDateString() + "  To : " + BlReportData.ToDate.ToShortDateString();
            }
            if (BlReportData.SubID == "VATOP")
            {
                BlReportData.PageTitle = "Output Tax Report From : " + BlReportData.FromDate.ToShortDateString() + "  To : " + BlReportData.ToDate.ToShortDateString();
            }
            if (BlReportData.SubID == "TFRREG")
            {
                BlReportData.PageTitle = "Stock Transfer From : " + BlReportData.FromDate.ToShortDateString() + "  To : " + BlReportData.ToDate.ToShortDateString();
            }
            if (BlReportData.SubID == "TFRPRC")
            {
                BlReportData.PageTitle = "Price Conversion Between : " + BlReportData.FromDate.ToShortDateString() + "  To : " + BlReportData.ToDate.ToShortDateString();
            }
            if (BlReportData.SubID == "TFRITC")
            {
                BlReportData.PageTitle = "Item Conversion Between : " + BlReportData.FromDate.ToShortDateString() + "  To : " + BlReportData.ToDate.ToShortDateString();
            }
            if (BlReportData.SubID == "TFRODR")
            {
                BlReportData.PageTitle = "Transfer Order From : " + BlReportData.FromDate.ToShortDateString() + "  To : " + BlReportData.ToDate.ToShortDateString();
            }
            if (BlReportData.SubID == "SALLIA")
            {
                //BlReportData.PageTitle = "Salesman Liability Register Between : " + BlReportData.FromDate.ToShortDateString() + "  To : " + BlReportData.ToDate.ToShortDateString();
                BlReportData.PageTitle = "Salesman Liability Register As on Date : " + BlReportData.ToDate.ToShortDateString();
            }



            if (BlReportData.SubID == "SALLIAP")
            {
                BlReportData.PageTitle = "Salesman Liability PeriodWise Between : " + BlReportData.FromDate.ToShortDateString() + "  To : " + BlReportData.ToDate.ToShortDateString();
                //BlReportData.PageTitle = "Salesman Liability PeriodWise As on Date : " + BlReportData.ToDate.ToShortDateString();
            }





            if (BlReportData.SubID == "SDPR")
            {
                BlReportData.ToDate = Convert.ToDateTime(txtsddate.Text);
                BlReportData.PageTitle = "Schedule Processed Upto : " + BlReportData.ToDate.ToShortDateString();
            }
            if (BlReportData.SubID == "SDPO")
            {
                BlReportData.ToDate = Convert.ToDateTime(txtsddate.Text);
                BlReportData.PageTitle = "Schedule Posted Upto : " + BlReportData.ToDate.ToShortDateString();
            }
            if (BlReportData.SubID == "SDDF")
            {
                BlReportData.ToDate = Convert.ToDateTime(txtsddate.Text);
                BlReportData.PageTitle = "Schedule Difference Upto : " + BlReportData.ToDate.ToShortDateString();
            }
            if (BlReportData.SubID == "POSTW")
            {
                BlReportData.Critrea1 = ddlQuantityCriteria.SelectedValue;
                BlReportData.Critrea2 = txtQauntity.Text;
                BlReportData.PageTitle = "Purchase Order Report  As on Date  : " + BlReportData.AsonDate.ToShortDateString();
            }
            if (BlReportData.SubID == "SLRCS")
            {
                BlReportData.Critrea1 = ddlSaleMode.SelectedValue;
                BlReportData.PageTitle = "SalesReturn Customer Wise Report From : " + BlReportData.FromDate.ToShortDateString() + "  To : " + BlReportData.ToDate.ToShortDateString();
            }
            if (BlReportData.SubID == "GSTSTS")
            {
                BlReportData.PageTitle = "Sales Tax Summary Report From : " + BlReportData.FromDate.ToShortDateString() + "  To : " + BlReportData.ToDate.ToShortDateString();
            }
            if (BlReportData.SubID == "GSTHSN")
            {
                BlReportData.PageTitle = "HSN Code Report From : " + BlReportData.FromDate.ToShortDateString() + "  To : " + BlReportData.ToDate.ToShortDateString();
            }
            if (BlReportData.SubID == "GSTHSN1")
            {
                BlReportData.PageTitle = "HSN Code Report(Format-1) From : " + BlReportData.FromDate.ToShortDateString() + "  To : " + BlReportData.ToDate.ToShortDateString();
            }
            if (BlReportData.SubID == "GSTS")
            {
                BlReportData.PageTitle = "Sales Report From : " + BlReportData.FromDate.ToShortDateString() + "  To : " + BlReportData.ToDate.ToShortDateString();
            }
            if (BlReportData.SubID == "GSTSIB")
            {
                BlReportData.PageTitle = "GST Sales Invoice and Bill Report From : " + BlReportData.FromDate.ToShortDateString() + "  To : " + BlReportData.ToDate.ToShortDateString();
            }

            
            return true;
        }
        catch (Exception ex)
        {
            return false;
        }
    }
    private bool SetReportData(BlReportData BlReportData)
    {
        InitializeReport(BlReportData, tvSettings.SelectedValue.ToString());
        if (UpdateData(BlReportData))
        {
            Session["ReportData"] = BlReportData;
            return true;
        }
        else
        {
            return false;
        }
    }
    public void HideDiv()
    {
        divcatgry.Visible = false;
        divDate.Visible = false;
        divStore.Visible = false;
        divAsOnDate.Visible = false;
        // divItems.Visible = false;
        divItemsPopUp.Visible = false;
        divButtons.Visible = false;
        divStoreFromTo.Visible = false;
        divIssueMode.Visible = false;
        divuser.Visible = false;
        divSaleMode.Visible = false;
        divPaymentType.Visible = false;
        //  divPaymentType.Visible = false;
        divEmployee.Visible = false;
        // divSupplier.Visible = false;
        divSupplierpopup.Visible = false;
        divCustomer.Visible = false;
        divCustomer2.Visible = false;
        divQuantitySearch.Visible = false;
        divValuesSearch.Visible = false;
        divNoSales.Visible = false;
        divCustomerGrp.Visible = false;
        divBillNo.Visible = false;
        divCounter.Visible = false;
        divShift.Visible = false;
        DivReportType.Visible = false;
        divSdDate.Visible = false;
    }
    public void SetDetails(string strName, string strRefno)
    {
        if (CommonPopup1.CallingFunction == "CustomerFill")
        {
            txtCustRefno.Text = strRefno;
            txtCustomer.Text = strName;

        }
        else if (CommonPopup1.CallingFunction == "ItemFill")
        {

            txtItemRefno.Text = strRefno;
            textItemName.Text = strName;
        }
        else if (CommonPopup1.CallingFunction == "SupplierFill")
        {
            TxtSupplierRefNo.Text = strRefno;
            txtSupplierName.Text = strName;
        }

    }
    public void CustomerFill(int pageIndex)
    {
        ClearCustomerFill();
        Populate Populate = new Populate();
        CommonPopup1.CallingFunction = "CustomerFill";
        string strName = ((TextBox)CommonPopup1.FindControl("txtName")).Text;
        string Refno = ((TextBox)CommonPopup1.FindControl("txtRefno")).Text;
        string searchCol = ((HiddenField)CommonPopup1.FindControl("hfColumn")).Value;
        Refno = Refno == string.Empty || Refno == null ? "0" : Refno;
        searchCol = searchCol == string.Empty ? "[Name]" : "[" + searchCol + "]";
        Populate.FillGrid((GridView)CommonPopup1.FindControl("gdvSearch"), "Customer", "ID_Customer as ID, Name as Name,CustomerCode as CustCode", "ID_Customer", "", "FK_Client=" + hfClient.Value.Split(',')[0] + "   AND Active=1 ", searchCol + "  LIKE '" + strName + "%'", pageIndex, 10, "", (Repeater)CommonPopup1.FindControl("rptPager"), this.Page);

        Populate = null;
        if (txtCustomer.Text == string.Empty)
        {
            lblPopupHeader.Text = "Customer";
            mpCommon.Show();
        }
    }
    private void ClearCustomerFill()
    {
        txtCustomer.Text = "";

    }
    protected void Supplier()
    {
        try
        {

            ((TextBox)CommonPopup1.FindControl("txtName")).Text = txtSupplierName.Text.Trim();
            ((TextBox)CommonPopup1.FindControl("txtRefno")).Text = txtSupplierName.Text.Trim();
            ((HiddenField)CommonPopup1.FindControl("hfColumn")).Value = "Name";
            SupplierFill(1);
            divAdd.Style.Add("display", "block");
            // divView.Style.Add("display", "none");
            //txtPurchaseOrder.Focus();


        }
        catch (Exception ex)
        {
        }
    }
    protected void Item()
    {
        try
        {

            ((TextBox)CommonPopup1.FindControl("txtName")).Text = textItemName.Text.Trim();
            ((TextBox)CommonPopup1.FindControl("txtRefno")).Text = textItemName.Text.Trim();
            ((HiddenField)CommonPopup1.FindControl("hfColumn")).Value = "Name";
            ItemFill(1);
            divAdd.Style.Add("display", "block");
            // divView.Style.Add("display", "none");
            //txtPurchaseOrder.Focus();
        }
        catch (Exception ex)
        {
        }
    }
    public void SupplierFill(int pageIndex)
    {
        ClearSupplier();
        Populate Populate = new Populate();
        CommonPopup1.CallingFunction = "SupplierFill";
        string strName = ((TextBox)CommonPopup1.FindControl("txtName")).Text;
        string Refno = ((TextBox)CommonPopup1.FindControl("txtRefno")).Text;
        string searchCol = ((HiddenField)CommonPopup1.FindControl("hfColumn")).Value;
        Refno = Refno == string.Empty || Refno == null ? "0" : Refno;
        searchCol = searchCol == string.Empty ? "[Name]" : "[" + searchCol + "]";
        Populate.FillGrid((GridView)CommonPopup1.FindControl("gdvSearch"), "Supplier", "ID_Supplier as ID,Name as Name", "ID_Supplier", "", "FK_Client=" + hfClient.Value.Split(',')[0] + " AND  Active=1  ", searchCol + "  LIKE '" + strName + "%'", pageIndex, 10, "", (Repeater)CommonPopup1.FindControl("rptPager"), this.Page);
        Populate = null;
        //    txtSubHeadRefno.Focus();
        if (txtSupplierName.Text == string.Empty)
        {
            lblPopupHeader.Text = "Supplier List";
            mpCommon.Show();
        }
    }
    public void ItemFill(int pageIndex)
    {
        ClearItem();
        Populate Populate = new Populate();
        CommonPopup1.CallingFunction = "ItemFill";
        string strName = ((TextBox)CommonPopup1.FindControl("txtName")).Text;
        string Refno = ((TextBox)CommonPopup1.FindControl("txtRefno")).Text;
        string searchCol = ((HiddenField)CommonPopup1.FindControl("hfColumn")).Value;
        Refno = Refno == string.Empty || Refno == null ? "0" : Refno;
        searchCol = searchCol == string.Empty ? "[Name]" : "[" + searchCol + "]";
        Populate.FillGrid((GridView)CommonPopup1.FindControl("gdvSearch"), "Items", "ID_Items as ID,Name as Name", "ID_Items", "Name", "FK_Client=" + hfClient.Value.Split(',')[0] + " AND  Active=1 and FK_Category= CASE WHEN  " + ddlCategoryFirst.SelectedValue + "=0 THEN FK_Category ELSE  " + ddlCategoryFirst.SelectedValue + "  END", searchCol + "  LIKE '" + strName + "%'", pageIndex, 10, "", (Repeater)CommonPopup1.FindControl("rptPager"), this.Page);

        // Populate.FillGrid((GridView)CommonPopup1.FindControl("gdvSearch"), "Items", "ID_Items as ID,Name as Name", "ID_Items", "", "FK_Client=" + hfClient.Value.Split(',')[0] + " AND Fk_Store=" + Convert.ToString(ddlStore.SelectedValue.ToString()) + "  AND (ID_Items =" + Refno + " OR 0=" + Refno + ")  AND Closed=0 AND Cancelled=0 ", searchCol + "  LIKE '" + strName + "%'", pageIndex, 10, "", (Repeater)CommonPopup1.FindControl("rptPager"), this.Page);
        Populate = null;
        //    txtSubHeadRefno.Focus();
        if (textItemName.Text == string.Empty)
        {
            lblPopupHeader.Text = "Item List";
            mpCommon.Show();
        }
    }
    #endregion

    #region Events
    protected void btnCategoryBlurTrigger_Click(object sender, EventArgs e)
    {
        try
        {
            Populate populate = new Populate();
            if (ddlCategoryFirst.SelectedIndex > 0)
            {
                //populate.FillDropdownList(ddlItems, "Items", "Name", "ID_Items", "Name", " Active='true' AND FK_Client=" + hfClient.Value + " AND FK_Category=" + ddlCategoryFirst.SelectedValue, "");
            }
            else
            {
                // populate.FillDropdownListAll(ddlItems, "Items", "Name", "ID_Items", "Name", " Active='true'AND FK_Client=" + hfClient.Value, "");
            }
        }
        catch (Exception ex)
        { }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                //CheckUserAccess();

                SetReadonly(txtFromDate, txtToDate, txtAsOnDate);
                txtFromDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
                txtToDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
                txtAsOnDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
                txtCurrentDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
                txtsddate.Text = DateTime.Now.ToString("dd/MM/yyyy");
                //if(Convert.ToBoolean(hfIsAcessable.Value.Split(',')[0]))
                //{
                HideDiv();
                txtNoSales.Text = "0";
                hfUser.Value = Request.QueryString["uid"];
                hfClient.Value = Request.QueryString["clnt"];
                CheckUserAccess();
                BindData();
                BindReportMenu();

                if (Convert.ToBoolean(hfIsAcessable.Value.Split(',')[0]))
                {
                    if ((Request.QueryString["pgtpe"] != "") && (Request.QueryString["pgtpe"] != null))
                    {
                        //sHistory.Select();
                        //sHistory.SelectedNode = sHistory.Nodes[0].Nodes[0];
                        //tvSettings_SelectedNodeChanged(sender, e);
                        //tvSettings.SelectedNode.Value = tvSettings.Nodes[0].ChildNodes[0].Text;
                        TreeNode Node = tvSettings.Nodes[0];
                        Node.Selected = true;
                        Node.ExpandAll();
                        Node.ChildNodes[5].Selected = true;
                        tvSettings_SelectedNodeChanged(sender, e);
                    }
                    if (Request.QueryString["Stock"] != null)
                    {
                        TreeNode Node = tvSettings.Nodes[0];
                        Node.Selected = true;
                        Node.ExpandAll();
                        Node.ChildNodes[5].Selected = true;
                        tvSettings_SelectedNodeChanged(sender, e);
                    }
                    else if (Request.QueryString["Store"] != null)
                    {
                        TreeNode Node = tvSettings.Nodes[5];
                        Node.Selected = true;
                        Node.ExpandAll();
                        Node.ChildNodes[2].Selected = true;
                        tvSettings_SelectedNodeChanged(sender, e);
                    }
                    else if (Request.QueryString["item"] != null)
                    {
                        TreeNode Node = tvSettings.Nodes[5];
                        Node.Selected = true;
                        Node.ExpandAll();
                        Node.ChildNodes[8].Selected = true;
                        tvSettings_SelectedNodeChanged(sender, e);
                    }
                    else if (Request.QueryString["Customer"] != null)
                    {
                        TreeNode Node = tvSettings.Nodes[5];
                        Node.Selected = true;
                        Node.ExpandAll();
                        Node.ChildNodes[6].Selected = true;
                        tvSettings_SelectedNodeChanged(sender, e);
                    }
                    else if (Request.QueryString["Supplier"] != null)
                    {
                        //TreeNode Node = tvSettings.Nodes[5];
                        //   Node.Selected = true;
                        //   Node.ExpandAll();
                        //   Node.ChildNodes[5].Selected = true;
                        //   tvSettings_SelectedNodeChanged(sender, e);
                    }

                    else if (Request.QueryString["SalesMan"] != null)
                    {
                        TreeNode Node = tvSettings.Nodes[5];
                        Node.Selected = true;
                        Node.ExpandAll();
                        Node.ChildNodes[5].Selected = true;
                        tvSettings_SelectedNodeChanged(sender, e);
                    }
                    else if (Request.QueryString["CRCUS"] != null)
                    {
                        TreeNode Node = tvSettings.Nodes[5];
                        Node.Selected = true;
                        Node.ExpandAll();
                        Node.ChildNodes[7].Selected = true;
                        tvSettings_SelectedNodeChanged(sender, e);
                    }
                    else if (Request.QueryString["SASTR"] != null)
                    {
                        TreeNode Node = tvSettings.Nodes[5];
                        Node.Selected = true;
                        Node.ExpandAll();
                        Node.ChildNodes[2].Selected = true;
                        tvSettings_SelectedNodeChanged(sender, e);
                    }

                    else if (Request.QueryString["SAITM"] != null)
                    {
                        TreeNode Node = tvSettings.Nodes[5];
                        Node.Selected = true;
                        Node.ExpandAll();
                        Node.ChildNodes[1].Selected = true;
                        tvSettings_SelectedNodeChanged(sender, e);
                    }

                    else if (Request.QueryString["SACUS"] != null)
                    {
                        TreeNode Node = tvSettings.Nodes[5];
                        Node.Selected = true;
                        Node.ExpandAll();
                        Node.ChildNodes[6].Selected = true;
                        tvSettings_SelectedNodeChanged(sender, e);
                    }

                    else if (Request.QueryString["PURSTORE"] != null)
                    {
                        TreeNode Node = tvSettings.Nodes[6];
                        Node.Selected = true;
                        Node.ExpandAll();
                        Node.ChildNodes[1].Selected = true;
                        tvSettings_SelectedNodeChanged(sender, e);
                    }
                    else if (Request.QueryString["PURSTORE"] != null)
                    {
                        TreeNode Node = tvSettings.Nodes[6];
                        Node.Selected = true;
                        Node.ExpandAll();
                        Node.ChildNodes[1].Selected = true;
                        tvSettings_SelectedNodeChanged(sender, e);
                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "alertify.alert('Access denied')", true);
                }
            }

        }

        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "alertify.alert('Access denied')", true);
            //blMessage.Items.Add(ex.Message);
        }
    }
    protected void tvSettings_SelectedNodeChanged(object sender, EventArgs e)
    {
        try
        {
            hfSubModuleName.Value = tvSettings.SelectedValue;
            HideDiv();
            lblHeadText.Text = "";
            ddlCategoryFirst.SelectedIndex = 0;
            ddlIssueMode.SelectedIndex = 0;

            Populate populate = new Populate();
            // populate.FillDropdownListAll(ddlItems, "Items", "Name", "ID_Items", "Name", " Active='true' AND FK_Client=" + hfClient.Value, "");
            if (tvSettings.SelectedValue.ToString() == "TFRODR")
            {
                populate.FillDropdownListSelect(ddlFromStore, "Store", "Name", "ID_Store", "Name", "Active='True' AND FK_Client=" + hfClient.Value, "");
                populate.FillDropdownListSelect(ddlToStore, "Store", "Name", "ID_Store", "Name", "Active='True' AND FK_Client=" + hfClient.Value, "");
            }
            else
            {
                populate.FillDropdownListAll(ddlFromStore, "Store", "Name", "ID_Store", "Name", "Active='True' AND FK_Client=" + hfClient.Value, "");
                populate.FillDropdownListAll(ddlToStore, "Store", "Name", "ID_Store", "Name", "Active='True' AND FK_Client=" + hfClient.Value, "");
            }
            BlWebPharmaReport blWebPharmaReport = new BlWebPharmaReport();
            if (tvSettings.SelectedValue == "GSTSIB")
            {
                ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "ShowDate('');", true);
            }
            else
            ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "ShowDate();", true);
            //  ddlSupplier.SelectedIndex = 0;
            CustomerClear();
            ClearItem();
            ClearSupplier();
            txtAsOnDate.Enabled = true;
            if (tvSettings.SelectedValue == "STKREG")
            {
                blWebPharmaReport.SubModule = "STKREG";
                divcatgry.Visible = true;

                divDate.Visible = true;
                divStore.Visible = true;
                //ivItems.Visible = true;
                divItemsPopUp.Visible = true;
                divButtons.Visible = true;
                divStoreFromTo.Visible = false;
                divAsOnDate.Visible = false;
                divIssueMode.Visible = false;
                divuser.Visible = false;
                divSaleMode.Visible = false;
                divPaymentType.Visible = false;
                divEmployee.Visible = false;
                //divSupplier.Visible = false;
                divSupplierpopup.Visible = false;
                divCustomer.Visible = false;
                divCustomer2.Visible = false;
                divQuantitySearch.Visible = false;
                divNoSales.Visible = false;
                divBillNo.Visible = false;
                divCounter.Visible = false;
                divShift.Visible = false;
                DivReportType.Visible = true;
                divSdDate.Visible = false;
                ddlReportType.SelectedIndex = 0;
                hfINVSubModule.Value = "STK";
                lblHeadText.Text = "Stock Register";
            }
            if (tvSettings.SelectedValue == "STKREGC")
            {
                blWebPharmaReport.SubModule = "STKREGC";
                divcatgry.Visible = true;
                divDate.Visible = true;
                divStore.Visible = true;
                //ivItems.Visible = true;
                divItemsPopUp.Visible = true;
                divButtons.Visible = true;
                divStoreFromTo.Visible = false;
                divAsOnDate.Visible = false;
                divIssueMode.Visible = false;
                divuser.Visible = false;
                divSaleMode.Visible = false;
                divPaymentType.Visible = false;
                divEmployee.Visible = false;
                //divSupplier.Visible = false;
                divSupplierpopup.Visible = false;
                divCustomer.Visible = false;
                divCustomer2.Visible = false;
                divQuantitySearch.Visible = false;
                divNoSales.Visible = false;
                divBillNo.Visible = false;
                divCounter.Visible = false;
                divShift.Visible = false;
                DivReportType.Visible = true;
                divSdDate.Visible = false;
                ddlReportType.SelectedIndex = 0;
                hfINVSubModule.Value = "STK";
                lblHeadText.Text = "Stock Register Consolidated";
            }
            if (tvSettings.SelectedValue == "STKIL")
            {
                blWebPharmaReport.SubModule = "STKIL";
                divcatgry.Visible = true;
                divDate.Visible = false;
                divStore.Visible = true;
                //ivItems.Visible = false;
                divItemsPopUp.Visible = false;
                divButtons.Visible = true;
                divStoreFromTo.Visible = false;
                divAsOnDate.Visible = false;
                divIssueMode.Visible = false;
                divuser.Visible = false;
                divSaleMode.Visible = false;
                divPaymentType.Visible = false;
                divEmployee.Visible = false;
                //divSupplier.Visible = false;
                divSupplierpopup.Visible = false;
                divCustomer.Visible = false;
                divCustomer2.Visible = false;
                divQuantitySearch.Visible = false;
                divNoSales.Visible = false;
                divBillNo.Visible = false;
                divCounter.Visible = false;
                divShift.Visible = false;
                divSdDate.Visible = false;
                DivReportType.Visible = true;
                ddlReportType.SelectedIndex = 0;
                hfINVSubModule.Value = "STK";
                lblHeadText.Text = "Stock ItemList";
            }

            if (tvSettings.SelectedValue == "STKNEG")
            {
                blWebPharmaReport.SubModule = "STKNEG";
                divcatgry.Visible = true;
                divDate.Visible = false;
                divStore.Visible = true;
                // divItems.Visible = true;
                divItemsPopUp.Visible = true;
                divButtons.Visible = true;
                divStoreFromTo.Visible = false;
                divAsOnDate.Visible = false;
                divIssueMode.Visible = false;
                divuser.Visible = false;
                divSaleMode.Visible = false;
                divPaymentType.Visible = false;
                divEmployee.Visible = false;
                //    divSupplier.Visible = false;
                divSupplierpopup.Visible = false;
                divCustomer.Visible = false;
                divCustomer2.Visible = false;
                divQuantitySearch.Visible = false;
                divNoSales.Visible = false;
                divBillNo.Visible = false;
                divCounter.Visible = false;
                divShift.Visible = false;
                DivReportType.Visible = true;
                divSdDate.Visible = false;
                ddlReportType.SelectedIndex = 0;
                hfINVSubModule.Value = "STK";
                lblHeadText.Text = "Negative Stock";
            }
            if (tvSettings.SelectedValue == "STKIM")
            {
                blWebPharmaReport.SubModule = "STKIM";
                divcatgry.Visible = false;
                divDate.Visible = true;
                divStore.Visible = true;
                // divItems.Visible = true;
                divItemsPopUp.Visible = true;
                divButtons.Visible = true;
                divStoreFromTo.Visible = false;
                divAsOnDate.Visible = false;
                divIssueMode.Visible = false;
                divuser.Visible = false;
                divSaleMode.Visible = false;
                divPaymentType.Visible = false;
                divEmployee.Visible = false;
                //    divSupplier.Visible = false;
                divSupplierpopup.Visible = false;
                divCustomer.Visible = false;
                divCustomer2.Visible = false;
                divQuantitySearch.Visible = false;
                divNoSales.Visible = false;
                divBillNo.Visible = false;
                divCounter.Visible = false;
                divShift.Visible = false;
                DivReportType.Visible = true;
                divSdDate.Visible = false;
                ddlReportType.SelectedIndex = 0;
                hfINVSubModule.Value = "STK";
                lblHeadText.Text = "Item Movement";
            }

            if (tvSettings.SelectedValue == "STKDL")
            {
                blWebPharmaReport.SubModule = "STKDL";
                divcatgry.Visible = true;
                divDate.Visible = true;
                divStore.Visible = true;
                //  divItems.Visible = true;
                divItemsPopUp.Visible = true;
                divButtons.Visible = true;
                divStoreFromTo.Visible = false;
                divAsOnDate.Visible = false;
                divIssueMode.Visible = false;
                divuser.Visible = false;
                divSaleMode.Visible = false;
                divPaymentType.Visible = false;
                divEmployee.Visible = false;
                // divSupplier.Visible = false;
                divSupplierpopup.Visible = false;
                divCustomer.Visible = false;
                divCustomer2.Visible = false;
                divNoSales.Visible = false;
                divQuantitySearch.Visible = false;
                divBillNo.Visible = false;
                divCounter.Visible = false;
                divShift.Visible = false;
                divSdDate.Visible = false;
                DivReportType.Visible = true;
                hfINVSubModule.Value = "STK";
                lblHeadText.Text = "Stock Damage List";
            }
            if (tvSettings.SelectedValue == "OPSTK")
            {
                blWebPharmaReport.SubModule = "OPSTK";
                divcatgry.Visible = false;
                divDate.Visible = false;
                divStore.Visible = true;
                //  divItems.Visible = true;
                divItemsPopUp.Visible = true;
                divButtons.Visible = true;
                divStoreFromTo.Visible = false;
                divAsOnDate.Visible = false;
                divIssueMode.Visible = false;
                divuser.Visible = false;
                divSaleMode.Visible = false;
                divPaymentType.Visible = false;
                divEmployee.Visible = false;
                // divSupplier.Visible = false;
                divSupplierpopup.Visible = false;
                divCustomer.Visible = false;
                divCustomer2.Visible = false;
                divNoSales.Visible = false;
                divQuantitySearch.Visible = false;
                divBillNo.Visible = false;
                divCounter.Visible = false;
                divShift.Visible = false;
                divSdDate.Visible = false;
                DivReportType.Visible = true;
                hfINVSubModule.Value = "STK";
                lblHeadText.Text = "Opening Stock List";
            }
            if (tvSettings.SelectedValue == "STKTR")
            {
                blWebPharmaReport.SubModule = "STKTR";
                divcatgry.Visible = true;
                divDate.Visible = true;
                divStore.Visible = false;
                //divItems.Visible = true;
                divItemsPopUp.Visible = true;
                divStoreFromTo.Visible = true;
                divButtons.Visible = true;
                divAsOnDate.Visible = false;
                divIssueMode.Visible = false;
                divuser.Visible = false;
                divSaleMode.Visible = false;
                divPaymentType.Visible = false;
                divEmployee.Visible = false;
                //divSupplier.Visible = false;
                divSupplierpopup.Visible = false;
                divCustomer.Visible = false;
                divCustomer2.Visible = false;
                divQuantitySearch.Visible = false;
                divNoSales.Visible = false;
                divBillNo.Visible = false;
                divCounter.Visible = false;
                divShift.Visible = false;
                divSdDate.Visible = false;
                DivReportType.Visible = true;
                hfINVSubModule.Value = "STK";
                lblHeadText.Text = "Stock Transfer";
            }
            if (tvSettings.SelectedValue == "STKVF")
            {
                blWebPharmaReport.SubModule = "STKVF";
                divcatgry.Visible = true;
                divDate.Visible = false;
                divStore.Visible = true;
                // divItems.Visible = false;
                divItemsPopUp.Visible = false;
                divStoreFromTo.Visible = false;
                divButtons.Visible = true;
                divAsOnDate.Visible = false;
                divIssueMode.Visible = false;
                divuser.Visible = false;
                divSaleMode.Visible = false;
                divPaymentType.Visible = false;
                divEmployee.Visible = false;
                // divSupplier.Visible = false;
                divSupplierpopup.Visible = false;
                divCustomer.Visible = false;
                divCustomer2.Visible = false;
                divNoSales.Visible = false;
                divQuantitySearch.Visible = false;
                divBillNo.Visible = false;
                divCounter.Visible = false;
                divShift.Visible = false;
                divSdDate.Visible = false;
                DivReportType.Visible = true;                
                hfINVSubModule.Value = "STK";
                lblHeadText.Text = "Stock Verification";
            }
            if (tvSettings.SelectedValue == "STKVL")
            {
                blWebPharmaReport.SubModule = "STKVL";
                divcatgry.Visible = false;
                divDate.Visible = false;
                divStore.Visible = true;
                // divItems.Visible = false;
                divItemsPopUp.Visible = true;
                divStoreFromTo.Visible = false;
                divButtons.Visible = true;
                divAsOnDate.Visible = false;
                divIssueMode.Visible = false;
                divuser.Visible = false;
                divSaleMode.Visible = false;
                divPaymentType.Visible = false;
                divEmployee.Visible = false;
                // divSupplier.Visible = false;
                divSupplierpopup.Visible = false;
                divCustomer.Visible = false;
                divCustomer2.Visible = false;
                divNoSales.Visible = false;
                divQuantitySearch.Visible = false;
                divValuesSearch.Visible = true;
                divBillNo.Visible = false;
                divCounter.Visible = false;
                divShift.Visible = false;
                divSdDate.Visible = false;
                DivReportType.Visible = true;
                hfINVSubModule.Value = "STK";
                lblHeadText.Text = "Stock Values";
            }
            if (tvSettings.SelectedValue == "STKMQ")
            {
                blWebPharmaReport.SubModule = "STKMQ";
                divcatgry.Visible = true;
                divDate.Visible = false;
                divStore.Visible = true;
                // divItems.Visible = true;
                divItemsPopUp.Visible = true;
                divStoreFromTo.Visible = false;
                divButtons.Visible = true;
                divAsOnDate.Visible = false;
                divIssueMode.Visible = false;
                divuser.Visible = false;
                divSaleMode.Visible = false;
                divPaymentType.Visible = false;
                divEmployee.Visible = false;
                //divSupplier.Visible = false;
                divSupplierpopup.Visible = false;
                divCustomer.Visible = false;
                divCustomer2.Visible = false;
                divNoSales.Visible = false;
                divQuantitySearch.Visible = false;
                divBillNo.Visible = false;
                divCounter.Visible = false;
                divShift.Visible = false;
                divSdDate.Visible = false;
                DivReportType.Visible = true;
                hfINVSubModule.Value = "STK";
                lblHeadText.Text = "Minimum Quantity Items";
            }
            if (tvSettings.SelectedValue == "STKMC")
            {
                blWebPharmaReport.SubModule = "STKMC";
                divcatgry.Visible = false;
                divDate.Visible = true;
                divStore.Visible = false;
                divItemsPopUp.Visible = false;
                divStoreFromTo.Visible = false;
                divButtons.Visible = true;
                divAsOnDate.Visible = false;
                divIssueMode.Visible = false;
                divuser.Visible = false;
                divSaleMode.Visible = false;
                divPaymentType.Visible = false;
                divEmployee.Visible = false;
                divSupplierpopup.Visible = false;
                divCustomer.Visible = false;
                divCustomer2.Visible = false;
                divNoSales.Visible = false;
                divQuantitySearch.Visible = false;
                divBillNo.Visible = false;
                divCounter.Visible = false;
                divShift.Visible = false;
                divSdDate.Visible = false;
                DivReportType.Visible = true;
                hfINVSubModule.Value = "STK";
                lblHeadText.Text = "Multiple Stock Correction";
            }
            if (tvSettings.SelectedValue == "GIIR")
            {
                blWebPharmaReport.SubModule = "GIIR";
                divcatgry.Visible = false;
                divDate.Visible = true;
                divAsOnDate.Visible = false;
                divStore.Visible = false;
                //  divItems.Visible = false;
                divItemsPopUp.Visible = false;
                divStoreFromTo.Visible = false;
                divIssueMode.Visible = true;
                divButtons.Visible = true;
                divuser.Visible = false;
                divSaleMode.Visible = false;
                divPaymentType.Visible = false;
                divEmployee.Visible = false;
                //  divSupplier.Visible = false;
                divSupplierpopup.Visible = false;
                divCustomer.Visible = false;
                divCustomer2.Visible = false;
                divNoSales.Visible = false;
                divQuantitySearch.Visible = false;
                divBillNo.Visible = false;
                divCounter.Visible = false;
                divShift.Visible = false;
                divSdDate.Visible = false;
                DivReportType.Visible = true;
                lblHeadText.Text = "GoodsIssue Register";
                hfINVSubModule.Value = "GI";
            }
            if (tvSettings.SelectedValue == "GIIW")
            {
                blWebPharmaReport.SubModule = "GIIW";
                divcatgry.Visible = false;
                divDate.Visible = true;
                divAsOnDate.Visible = false;
                divStore.Visible = false;
                // divItems.Visible = true;
                divItemsPopUp.Visible = true;
                divStoreFromTo.Visible = false;
                divIssueMode.Visible = true;
                divuser.Visible = false;
                divSaleMode.Visible = false;
                divPaymentType.Visible = false;
                divEmployee.Visible = false;
                divButtons.Visible = true;
                //  divSupplier.Visible = false;
                divSupplierpopup.Visible = false;
                divCustomer.Visible = false;
                divQuantitySearch.Visible = false;
                divCustomer2.Visible = false;
                divNoSales.Visible = false;
                divBillNo.Visible = false;
                divCounter.Visible = false;
                divShift.Visible = false;
                divSdDate.Visible = false;
                DivReportType.Visible = true;
                hfINVSubModule.Value = "GI";
                lblHeadText.Text = "GoodsIssue Item-Wise";
            }
            if (tvSettings.SelectedValue == "PUREG" || tvSettings.SelectedValue == "PURREGDET")
            {
                if (tvSettings.SelectedValue == "PUREG")
                {
                    blWebPharmaReport.SubModule = "PUREG";
                    lblHeadText.Text = "Purchase Register";
                }
                else
                {
                    blWebPharmaReport.SubModule = "PURREGDET";
                    lblHeadText.Text = "Purchase Register Detalied";
                }             
                divcatgry.Visible = false;
                divDate.Visible = true;
                divAsOnDate.Visible = false;
                divStore.Visible = true;               
                divItemsPopUp.Visible = false;
                divStoreFromTo.Visible = false;
                divIssueMode.Visible = false;
                divuser.Visible = false;
                divSaleMode.Visible = false;
                divPaymentType.Visible = false;
                divEmployee.Visible = false;
                divButtons.Visible = true;               
                divSupplierpopup.Visible = false;
                divCustomer.Visible = false;
                divCustomer2.Visible = false;
                divQuantitySearch.Visible = false;
                divNoSales.Visible = false;
                divBillNo.Visible = false;
                divCounter.Visible = false;
                divShift.Visible = false;
                divSdDate.Visible = false;
                DivReportType.Visible = true;
                hfINVSubModule.Value = "PU";

            }
            if (tvSettings.SelectedValue == "PUIW")
            {
                blWebPharmaReport.SubModule = "PUIW";
                DivReportType.Visible = true;
                divcatgry.Visible = true;
                divDate.Visible = true;
                divAsOnDate.Visible = false;
                divStore.Visible = true;
                // divItems.Visible = true;
                divItemsPopUp.Visible = true;
                divStoreFromTo.Visible = false;
                divIssueMode.Visible = false;
                divButtons.Visible = true;
                divuser.Visible = false;
                divSaleMode.Visible = false;
                divPaymentType.Visible = false;
                divEmployee.Visible = false;
                //  divSupplier.Visible = false;
                divSupplierpopup.Visible = false;
                divCustomer.Visible = false;
                divCustomer2.Visible = false;
                divQuantitySearch.Visible = false;
                divNoSales.Visible = false;
                divBillNo.Visible = false;
                divCounter.Visible = false;
                divShift.Visible = false;
                divSdDate.Visible = false;                
                hfINVSubModule.Value = "PU";
                lblHeadText.Text = "Purchase Item-Wise";
            }
            if (tvSettings.SelectedValue == "PUIWSP")
            {
                blWebPharmaReport.SubModule = "PUIWSP";
                divcatgry.Visible = false;
                divDate.Visible = true;
                divAsOnDate.Visible = false;
                divStore.Visible = false;
                // divItems.Visible = true;
                divItemsPopUp.Visible = true;
                divStoreFromTo.Visible = false;
                divIssueMode.Visible = false;
                divButtons.Visible = true;
                divuser.Visible = false;
                divSaleMode.Visible = false;
                divPaymentType.Visible = false;
                divEmployee.Visible = false;
                //divSupplier.Visible = true;
                divSupplierpopup.Visible = true;
                divCustomer.Visible = false;
                divCustomer2.Visible = false;
                divQuantitySearch.Visible = false;
                divNoSales.Visible = false;
                divBillNo.Visible = false;
                divCounter.Visible = false;
                divShift.Visible = false;
                divSdDate.Visible = false;
                DivReportType.Visible = true;
                hfINVSubModule.Value = "PU";
                lblHeadText.Text = "Purchase Item-Wise Supplier";
            }
            if (tvSettings.SelectedValue == "PUSPIW")
            {
                blWebPharmaReport.SubModule = "PUSPIW";
                DivReportType.Visible = true;
                divcatgry.Visible = false;
                divDate.Visible = true;
                divAsOnDate.Visible = false;
                divStore.Visible = false;
                // divItems.Visible = false;
                divItemsPopUp.Visible = false;
                divStoreFromTo.Visible = false;
                divIssueMode.Visible = false;
                divButtons.Visible = true;
                divuser.Visible = false;
                divSaleMode.Visible = false;
                divPaymentType.Visible = false;
                divEmployee.Visible = false;
                //  divSupplier.Visible = true;
                divSupplierpopup.Visible = true;
                divCustomer.Visible = false;
                divCustomer2.Visible = false;
                divQuantitySearch.Visible = false;
                divNoSales.Visible = false;
                divBillNo.Visible = false;
                divCounter.Visible = false;
                divShift.Visible = false;
                divSdDate.Visible = false;
                hfINVSubModule.Value = "PU";
                lblHeadText.Text = "Supplier Wise Purchase";
            }
            if (tvSettings.SelectedValue == "SUPS")
            {
                blWebPharmaReport.SubModule = "SUPS";
                DivReportType.Visible = true;
                divcatgry.Visible = false;
                divDate.Visible = true;
                divAsOnDate.Visible = false;
                divStore.Visible = false;
                // divItems.Visible = false;
                divItemsPopUp.Visible = false;
                divStoreFromTo.Visible = false;
                divIssueMode.Visible = false;
                divButtons.Visible = true;
                divuser.Visible = false;
                divSaleMode.Visible = false;
                divPaymentType.Visible = false;
                divEmployee.Visible = false;
                //  divSupplier.Visible = true;
                divSupplierpopup.Visible = true;
                divCustomer.Visible = false;
                divCustomer2.Visible = false;
                divQuantitySearch.Visible = false;
                divNoSales.Visible = false;
                divBillNo.Visible = false;
                divCounter.Visible = false;
                divShift.Visible = false;
                divSdDate.Visible = false;
                hfINVSubModule.Value = "SL";
                lblHeadText.Text = "Supplier Wise Sale";
            }
            if (tvSettings.SelectedValue == "SLSMI")
            {
                blWebPharmaReport.SubModule = "SLSMI";
                DivReportType.Visible = true;
                divcatgry.Visible = false;
                divDate.Visible = true;
                divAsOnDate.Visible = false;
                divStore.Visible = false;
                divItemsPopUp.Visible = false;
                divStoreFromTo.Visible = false;
                divIssueMode.Visible = false;
                divButtons.Visible = true;
                divuser.Visible = true;
                divSaleMode.Visible = false;
                divPaymentType.Visible = false;
                divEmployee.Visible = false;
                divSupplierpopup.Visible = false;
                divCustomer.Visible = false;
                divCustomer2.Visible = false;
                divQuantitySearch.Visible = false;
                divNoSales.Visible = false;
                divBillNo.Visible = false;
                divCounter.Visible = true;
                divShift.Visible = false;
                divSdDate.Visible = false;
                hfINVSubModule.Value = "SL";
                lblHeadText.Text = "SalesMan Incentive";
            }
            if (tvSettings.SelectedValue == "SLSMIC")
            {
                blWebPharmaReport.SubModule = "SLSMIC";
                DivReportType.Visible = true;
                divcatgry.Visible = false;
                divDate.Visible = true;
                divAsOnDate.Visible = false;
                divStore.Visible = false;
                divItemsPopUp.Visible = false;
                divStoreFromTo.Visible = false;
                divIssueMode.Visible = false;
                divButtons.Visible = true;
                divuser.Visible = true;
                divSaleMode.Visible = false;
                divPaymentType.Visible = false;
                divEmployee.Visible = false;
                divSupplierpopup.Visible = false;
                divCustomer.Visible = false;
                divCustomer2.Visible = false;
                divQuantitySearch.Visible = false;
                divNoSales.Visible = false;
                divBillNo.Visible = false;
                divCounter.Visible = true;
                divShift.Visible = false;
                divSdDate.Visible = false;
                hfINVSubModule.Value = "SL";
                lblHeadText.Text = "SalesMan Incentive Consolidate";
            }
            if (tvSettings.SelectedValue == "PRSL")
            {
                blWebPharmaReport.SubModule = "PRSL";
                DivReportType.Visible = true;
                divcatgry.Visible = false;
                divDate.Visible = true;
                divAsOnDate.Visible = false;
                divStore.Visible = false;
                divItemsPopUp.Visible = false;
                divStoreFromTo.Visible = false;
                divIssueMode.Visible = false;
                divButtons.Visible = true;
                divuser.Visible = false;
                divSaleMode.Visible = false;
                divPaymentType.Visible = false;
                divEmployee.Visible = false;
                divSupplierpopup.Visible = false;
                divCustomer.Visible = false;
                divCustomer2.Visible = false;
                divQuantitySearch.Visible = false;
                divNoSales.Visible = false;
                divBillNo.Visible = false;
                divCounter.Visible = true;
                divShift.Visible = false;
                divSdDate.Visible = false;
                hfINVSubModule.Value = "PR";
                lblHeadText.Text = "Sales Profitability";
            }
            if (tvSettings.SelectedValue == "PRCM")
            {
                blWebPharmaReport.SubModule = "PRCM";
                DivReportType.Visible = true;
                divcatgry.Visible = false;
                divDate.Visible = true;
                divAsOnDate.Visible = false;
                divStore.Visible = false;
                divItemsPopUp.Visible = false;
                divStoreFromTo.Visible = false;
                divIssueMode.Visible = false;
                divButtons.Visible = true;
                divuser.Visible = false;
                divSaleMode.Visible = false;
                divPaymentType.Visible = false;
                divEmployee.Visible = false;
                divSupplierpopup.Visible = false;
                divCustomer.Visible = true;
                divCustomer2.Visible = false;
                divQuantitySearch.Visible = false;
                divNoSales.Visible = false;
                divBillNo.Visible = false;
                divCounter.Visible = true;
                divShift.Visible = false;
                divSdDate.Visible = false;
                hfINVSubModule.Value = "PR";
                lblHeadText.Text = "Customer Profitability";
            }
            if (tvSettings.SelectedValue == "PRSMA")
            {
                blWebPharmaReport.SubModule = "PRSMA";
                DivReportType.Visible = true;
                divcatgry.Visible = false;
                divDate.Visible = true;
                divAsOnDate.Visible = false;
                divStore.Visible = false;
                divItemsPopUp.Visible = false;
                divStoreFromTo.Visible = false;
                divIssueMode.Visible = false;
                divButtons.Visible = true;
                divuser.Visible = true;
                divSaleMode.Visible = false;
                divPaymentType.Visible = false;
                divEmployee.Visible = false;
                divSupplierpopup.Visible = false;
                divCustomer.Visible = false;
                divCustomer2.Visible = false;
                divQuantitySearch.Visible = false;
                divNoSales.Visible = false;
                divBillNo.Visible = false;
                divCounter.Visible = true;
                divShift.Visible = false;
                divSdDate.Visible = false;
                hfINVSubModule.Value = "PR";
                lblHeadText.Text = "SalesMan Profitability Analysis";
            }
            if (tvSettings.SelectedValue == "PURREG")
            {
                blWebPharmaReport.SubModule = "PURREG";
                divcatgry.Visible = false;
                divDate.Visible = true;
                divAsOnDate.Visible = false;
                divStore.Visible = true;
                // divItems.Visible = false;
                divItemsPopUp.Visible = false;
                divStoreFromTo.Visible = false;
                divIssueMode.Visible = false;
                divuser.Visible = false;
                divSaleMode.Visible = false;
                divPaymentType.Visible = false;
                divEmployee.Visible = false;
                divButtons.Visible = true;
                //divSupplier.Visible = false;
                divSupplierpopup.Visible = false;
                divCustomer.Visible = false;
                divCustomer2.Visible = false;
                divQuantitySearch.Visible = false;
                divNoSales.Visible = false;
                divBillNo.Visible = false;
                divCounter.Visible = false;
                divShift.Visible = false;
                divSdDate.Visible = false;
                DivReportType.Visible = true;
                hfINVSubModule.Value = "PUR";
                lblHeadText.Text = "Purchase return Register";
            }
            if (tvSettings.SelectedValue == "PURIW")
            {
                blWebPharmaReport.SubModule = "PURIW";
                divcatgry.Visible = false;
                divDate.Visible = true;
                divAsOnDate.Visible = false;
                divStore.Visible = false;
                // divItems.Visible = true;
                divItemsPopUp.Visible = true;
                divStoreFromTo.Visible = false;
                divIssueMode.Visible = false;
                divuser.Visible = false;
                divSaleMode.Visible = false;
                divPaymentType.Visible = false;
                divEmployee.Visible = false;
                divButtons.Visible = true;
                //  divSupplier.Visible = false;
                divSupplierpopup.Visible = false;
                divCustomer.Visible = false;
                divCustomer2.Visible = false;
                divQuantitySearch.Visible = false;
                divNoSales.Visible = false;
                divBillNo.Visible = false;
                divCounter.Visible = false;
                divShift.Visible = false;
                divSdDate.Visible = false;
                DivReportType.Visible = true;
                ddlReportType.SelectedIndex = 0;
                hfINVSubModule.Value = "PUR";
                lblHeadText.Text = "Purchase return Item wise";
            }
            if (tvSettings.SelectedValue == "MRIW")
            {
                blWebPharmaReport.SubModule = "MRIW";
                divcatgry.Visible = true;
                divDate.Visible = true;
                divStore.Visible = true;
                // divItems.Visible = true;
                divItemsPopUp.Visible = true;
                divButtons.Visible = true;
                divStoreFromTo.Visible = false;
                divAsOnDate.Visible = false;
                divIssueMode.Visible = false;
                divuser.Visible = false;
                divSaleMode.Visible = false;
                divPaymentType.Visible = false;
                divEmployee.Visible = false;
                // divSupplier.Visible = false;
                divSupplierpopup.Visible = false;
                divCustomer.Visible = false;
                divCustomer2.Visible = false;
                divQuantitySearch.Visible = false;
                divNoSales.Visible = false;
                divBillNo.Visible = false;
                divCounter.Visible = false;
                divShift.Visible = false;
                divSdDate.Visible = false;
                DivReportType.Visible = true;
                hfINVSubModule.Value = "MR";
                lblHeadText.Text = "Material Receipt Item-Wise";

            }
            if (tvSettings.SelectedValue == "MREXP")
            {
                blWebPharmaReport.SubModule = "MREXP";
                divcatgry.Visible = false;
                divDate.Visible = true;
                divStore.Visible = false;
                // divItems.Visible = false;
                divItemsPopUp.Visible = false;
                divButtons.Visible = true;
                divStoreFromTo.Visible = false;
                divAsOnDate.Visible = false;
                divIssueMode.Visible = false;
                divuser.Visible = false;
                divSaleMode.Visible = false;
                divPaymentType.Visible = false;
                divEmployee.Visible = false;
                // divSupplier.Visible = false;
                divSupplierpopup.Visible = false;
                divCustomer.Visible = false;
                divCustomer2.Visible = false;
                divQuantitySearch.Visible = false;
                divNoSales.Visible = false;
                divBillNo.Visible = false;
                divCounter.Visible = false;
                divShift.Visible = false;
                divSdDate.Visible = false;
                DivReportType.Visible = true;
                hfINVSubModule.Value = "MR";
                lblHeadText.Text = "Expired Item";

            }
            if (tvSettings.SelectedValue == "SLREG")
            {
                blWebPharmaReport.SubModule = "SLREG";
                divcatgry.Visible = false;
                divDate.Visible = true;
                divStore.Visible = true;
                //divItems.Visible = false;
                divItemsPopUp.Visible = false;
                divButtons.Visible = true;
                divStoreFromTo.Visible = false;
                divAsOnDate.Visible = false;
                divIssueMode.Visible = false;
                //divuser.Visible = false;
                ddlUser.SelectedIndex = 0;
                ddlSaleMode.SelectedIndex = 0;
                //ddlPaymentType.SelectedIndex = 0;
                divEmployee.Visible = false;
                // divSupplier.Visible = false;
                divSupplierpopup.Visible = false;
                divCustomer.Visible = false;
                divCustomer2.Visible = false;
                divQuantitySearch.Visible = false;
                divNoSales.Visible = false;
                divBillNo.Visible = false;
                divCounter.Visible = true;
                divuser.Visible = true;
                divSaleMode.Visible = true;
                divPaymentType.Visible = false;
                divSdDate.Visible = false;
                divShift.Visible = Visible;
                ddlUser.SelectedIndex = 0;
                ddlPaymentType.SelectedIndex = 0;
                ddlStore.SelectedIndex = 0;
                ddlCounter.SelectedIndex = 0;
                ddlSaleMode.SelectedIndex = 0;
                ddlShift.SelectedIndex = 0;
                DivReportType.Visible = true;
                ddlReportType.SelectedIndex = 0;
                hfINVSubModule.Value = "SL";
                lblHeadText.Text = "Sales Register";

            }




            if (tvSettings.SelectedValue == "SLREGDT")
            {
                blWebPharmaReport.SubModule = "SLREGDT";
                divcatgry.Visible = false;
                divDate.Visible = true;
                divStore.Visible = true;
                //divItems.Visible = false;
                divItemsPopUp.Visible = false;
                divButtons.Visible = true;
                divStoreFromTo.Visible = false;
                divAsOnDate.Visible = false;
                divIssueMode.Visible = false;
                //divuser.Visible = false;
                ddlUser.SelectedIndex = 0;
                ddlSaleMode.SelectedIndex = 0;
                //ddlPaymentType.SelectedIndex = 0;
                divEmployee.Visible = false;
                // divSupplier.Visible = false;
                divSupplierpopup.Visible = false;
                divCustomer.Visible = false;
                divCustomer2.Visible = false;
                divQuantitySearch.Visible = false;
                divNoSales.Visible = false;
                divBillNo.Visible = false;
                divCounter.Visible = true;
                divuser.Visible = true;
                divSaleMode.Visible = true;
                divPaymentType.Visible = false;
                divSdDate.Visible = false;
                divShift.Visible = Visible;
                ddlUser.SelectedIndex = 0;
                ddlPaymentType.SelectedIndex = 0;
                ddlStore.SelectedIndex = 0;
                ddlCounter.SelectedIndex = 0;
                ddlSaleMode.SelectedIndex = 0;
                ddlShift.SelectedIndex = 0;
                DivReportType.Visible = true;
                ddlReportType.SelectedIndex = 0;
                hfINVSubModule.Value = "SL";
                lblHeadText.Text = "Sales Register Detailed";

            }





















            if (tvSettings.SelectedValue == "SLREGC")
            {
                blWebPharmaReport.SubModule = "SLREGC";
                divcatgry.Visible = true;
                divDate.Visible = true;
                divStore.Visible = true;
                divItemsPopUp.Visible = false;
                divButtons.Visible = true;
                divStoreFromTo.Visible = false;
                divAsOnDate.Visible = false;
                divIssueMode.Visible = false;
                ddlUser.SelectedIndex = 0;
                ddlSaleMode.SelectedIndex = 0;
                divEmployee.Visible = false;
                divSupplierpopup.Visible = false;
                divCustomer.Visible = false;
                divCustomer2.Visible = false;
                divQuantitySearch.Visible = false;
                divNoSales.Visible = false;
                divBillNo.Visible = false;
                divCounter.Visible = true;
                divuser.Visible = true;
                divSaleMode.Visible = true;
                divPaymentType.Visible = false;
                divSdDate.Visible = false;
                divShift.Visible = Visible;
                ddlUser.SelectedIndex = 0;
                ddlStore.SelectedIndex = 0;
                ddlCounter.SelectedIndex = 0;
                ddlSaleMode.SelectedIndex = 0;
                ddlShift.SelectedIndex = 0;
                DivReportType.Visible = true;
                ddlReportType.SelectedIndex = 0;
                hfINVSubModule.Value = "SL";
                lblHeadText.Text = "Sales Register (Category Wise)";

            }
           
            if (tvSettings.SelectedValue == "SLMNTH")
            {
                blWebPharmaReport.SubModule = "SLMNTH";
                divcatgry.Visible = false;
                divDate.Visible = false;
                divAsOnDate.Visible = true;
                divStore.Visible = true;
                divItemsPopUp.Visible = false;
                divStoreFromTo.Visible = false;
                divIssueMode.Visible = false;
                divuser.Visible = false;
                divEmployee.Visible = false;
                divButtons.Visible = true;
                divSupplierpopup.Visible = false;
                divCustomer.Visible = false;
                divCustomer2.Visible = false;
                divQuantitySearch.Visible = false;
                divNoSales.Visible = false;
                divBillNo.Visible = false;
                divCounter.Visible = false;
                divShift.Visible = false;
                divPaymentType.Visible = false;
                divSaleMode.Visible = false;
                DivReportType.Visible = true;
                divSdDate.Visible = false;
                ddlReportType.SelectedIndex = 0;
                hfINVSubModule.Value = "SL";
                lblHeadText.Text = "Monthly Sales";
            }
            if (tvSettings.SelectedValue == "BLREG")
            {
                blWebPharmaReport.SubModule = "BLREG";
                divcatgry.Visible = false;
                divDate.Visible = true;
                divStore.Visible = true;
                // divItems.Visible = false;
                divItemsPopUp.Visible = false;
                divButtons.Visible = true;
                divStoreFromTo.Visible = false;
                divAsOnDate.Visible = false;
                divIssueMode.Visible = false;
                //divuser.Visible = false;
                ddlUser.SelectedIndex = 0;
                ddlSaleMode.SelectedIndex = 0;
                //ddlPaymentType.SelectedIndex = 0;
                divEmployee.Visible = false;
                //  divSupplier.Visible = false;
                divSupplierpopup.Visible = false;
                divCustomer.Visible = false;
                divCustomer2.Visible = false;
                divQuantitySearch.Visible = false;
                divNoSales.Visible = false;
                divBillNo.Visible = false;
                divCounter.Visible = true;
                divuser.Visible = true;
                divSaleMode.Visible = true;
                divPaymentType.Visible = false;
                divSdDate.Visible = false;
                divShift.Visible = Visible;
                ddlUser.SelectedIndex = 0;
                ddlPaymentType.SelectedIndex = 0;
                ddlStore.SelectedIndex = 0;
                ddlCounter.SelectedIndex = 0;
                ddlSaleMode.SelectedIndex = 0;
                ddlShift.SelectedIndex = 0;
                DivReportType.Visible = true;
                ddlReportType.SelectedIndex = 0;
                hfINVSubModule.Value = "SL";
                lblHeadText.Text = "Bill Register";
            }
            if (tvSettings.SelectedValue == "CCWR")
            {
                blWebPharmaReport.SubModule = "CCWR";
                divcatgry.Visible = false;
                divDate.Visible = true;
                divStore.Visible = true;
                // divItems.Visible = false;
                divItemsPopUp.Visible = false;
                divButtons.Visible = true;
                divStoreFromTo.Visible = false;
                divAsOnDate.Visible = false;
                divIssueMode.Visible = false;
                //divuser.Visible = false;
                ddlUser.SelectedIndex = 0;
                ddlSaleMode.SelectedIndex = 0;
                //ddlPaymentType.SelectedIndex = 0;
                divEmployee.Visible = false;
                // divSupplier.Visible = false;
                divSupplierpopup.Visible = false;
                divCustomer.Visible = false;
                divCustomer2.Visible = false;
                divQuantitySearch.Visible = false;
                divNoSales.Visible = false;
                divBillNo.Visible = false;
                divCounter.Visible = true;
                divuser.Visible = true;
                divSaleMode.Visible = false;
                divPaymentType.Visible = false;
                divSdDate.Visible = false;
                DivReportType.Visible = true;
                divShift.Visible = Visible;
                ddlUser.SelectedIndex = 0;
                ddlPaymentType.SelectedIndex = 0;
                ddlStore.SelectedIndex = 0;
                ddlCounter.SelectedIndex = 0;
                ddlSaleMode.SelectedIndex = 0;
                ddlShift.SelectedIndex = 0;
                hfINVSubModule.Value = "SL";
                lblHeadText.Text = "Counter Close  Report";
            }
            if (tvSettings.SelectedValue == "CWSAL")
            {
                blWebPharmaReport.SubModule = "CWSAL";
                divcatgry.Visible = false;
                divDate.Visible = true;
                divStore.Visible = true;
                // divItems.Visible = false;
                divItemsPopUp.Visible = false;
                divButtons.Visible = true;
                divStoreFromTo.Visible = false;
                divAsOnDate.Visible = false;
                divIssueMode.Visible = false;
                //divuser.Visible = false;
                ddlUser.SelectedIndex = 0;
                ddlSaleMode.SelectedIndex = 0;
                //ddlPaymentType.SelectedIndex = 0;
                divEmployee.Visible = false;
                //  divSupplier.Visible = false;
                divSupplierpopup.Visible = false;
                divCustomer.Visible = false;
                divCustomer2.Visible = false;
                divQuantitySearch.Visible = false;
                divNoSales.Visible = false;
                divBillNo.Visible = false;
                divCounter.Visible = true;
                divuser.Visible = true;
                divSaleMode.Visible = false;
                divPaymentType.Visible = false;
                divSdDate.Visible = false;
                divShift.Visible = false;
                DivReportType.Visible = true;
                ddlUser.SelectedIndex = 0;
                ddlPaymentType.SelectedIndex = 0;
                ddlStore.SelectedIndex = 0;
                ddlCounter.SelectedIndex = 0;
                ddlSaleMode.SelectedIndex = 0;
                ddlShift.SelectedIndex = 0;
                hfINVSubModule.Value = "SL";
                lblHeadText.Text = "Counter Wise Sales";
            }
            if (tvSettings.SelectedValue == "SAWR")
            {
                blWebPharmaReport.SubModule = "SAWR";
                divcatgry.Visible = false;
                divDate.Visible = true;
                divStore.Visible = true;
                //divItems.Visible = false;
                divItemsPopUp.Visible = false;
                divButtons.Visible = true;
                divStoreFromTo.Visible = false;
                divAsOnDate.Visible = false;
                divIssueMode.Visible = false;
                //divuser.Visible = false;
                ddlUser.SelectedIndex = 0;
                ddlSaleMode.SelectedIndex = 0;
                //ddlPaymentType.SelectedIndex = 0;
                divEmployee.Visible = false;
                // divSupplier.Visible = false;
                divSupplierpopup.Visible = false;
                divCustomer.Visible = false;
                divCustomer2.Visible = false;
                divQuantitySearch.Visible = false;
                divNoSales.Visible = false;
                divBillNo.Visible = false;
                divCounter.Visible = true;
                divuser.Visible = true;
                divSaleMode.Visible = false;
                divPaymentType.Visible = false;
                DivReportType.Visible = true;
                divSdDate.Visible = false;
                ddlReportType.SelectedIndex = 0;
                divShift.Visible = Visible;
                ddlUser.SelectedIndex = 0;
                ddlPaymentType.SelectedIndex = 0;
                ddlStore.SelectedIndex = 0;
                ddlCounter.SelectedIndex = 0;
                ddlSaleMode.SelectedIndex = 0;
                ddlShift.SelectedIndex = 0;
                hfINVSubModule.Value = "SL";
                lblHeadText.Text = "Sales Abstract";
            }
            if (tvSettings.SelectedValue == "SLABD")
            {
                blWebPharmaReport.SubModule = "SLABD";
                divcatgry.Visible = false;
                divDate.Visible = true;
                divStore.Visible = true;
                //divItems.Visible = false;
                divItemsPopUp.Visible = false;
                divButtons.Visible = true;
                divStoreFromTo.Visible = false;
                divAsOnDate.Visible = false;
                divIssueMode.Visible = false;
                //divuser.Visible = false;
                ddlUser.SelectedIndex = 0;
                ddlSaleMode.SelectedIndex = 0;
                //ddlPaymentType.SelectedIndex = 0;
                divEmployee.Visible = false;
                // divSupplier.Visible = false;
                divSupplierpopup.Visible = false;
                divCustomer.Visible = false;
                divCustomer2.Visible = false;
                divQuantitySearch.Visible = false;
                divNoSales.Visible = false;
                divBillNo.Visible = false;
                divCounter.Visible = true;
                divuser.Visible = true;
                divSaleMode.Visible = false;
                divPaymentType.Visible = false;
                DivReportType.Visible = true;
                divSdDate.Visible = false;
                ddlReportType.SelectedIndex = 0;
                divShift.Visible = Visible;
                ddlUser.SelectedIndex = 0;
                ddlPaymentType.SelectedIndex = 0;
                ddlStore.SelectedIndex = 0;
                ddlCounter.SelectedIndex = 0;
                ddlSaleMode.SelectedIndex = 0;
                ddlShift.SelectedIndex = 0;
                hfINVSubModule.Value = "SL";
                lblHeadText.Text = "Sales Abstract Date Wise";
            }
            if (tvSettings.SelectedValue == "SLMWREG")
            {
                blWebPharmaReport.SubModule = "SLMWREG";
                divcatgry.Visible = false;
                divDate.Visible = true;
                divStore.Visible = false;
                // divItems.Visible = false;
                divItemsPopUp.Visible = false;
                divButtons.Visible = true;
                divStoreFromTo.Visible = false;
                divAsOnDate.Visible = false;
                divIssueMode.Visible = false;
                divuser.Visible = false;
                divSaleMode.Visible = false;
                divPaymentType.Visible = false;
                divEmployee.Visible = true;
                //  divSupplier.Visible = false;
                divSupplierpopup.Visible = false;
                divCustomer.Visible = false;
                divCustomer2.Visible = false;
                divQuantitySearch.Visible = false;
                divNoSales.Visible = false;
                divBillNo.Visible = false;
                divCounter.Visible = false;
                divShift.Visible = false;
                divSdDate.Visible = false;
                DivReportType.Visible = true;
                hfINVSubModule.Value = "SL";
                lblHeadText.Text = "Sales man Wise Register";
            }
            if (tvSettings.SelectedValue == "SLIW")
            {
                blWebPharmaReport.SubModule = "SLIW";
                divcatgry.Visible = true;
                divDate.Visible = true;
                divStore.Visible = true;
                //  divItems.Visible = true;
                divItemsPopUp.Visible = true;
                divButtons.Visible = true;
                divStoreFromTo.Visible = false;
                divAsOnDate.Visible = false;
                divIssueMode.Visible = false;
                divuser.Visible = false;
                divEmployee.Visible = false;
                // divSupplier.Visible = false;
                divSupplierpopup.Visible = false;
                divCustomer.Visible = false;
                divCustomer2.Visible = false;
                divQuantitySearch.Visible = false;
                divNoSales.Visible = false;
                divBillNo.Visible = false;
                divCounter.Visible = true;
                divShift.Visible = false;
                divuser.Visible = true;
                divSaleMode.Visible = true;
                divPaymentType.Visible = false;
                divSdDate.Visible = false;
                DivReportType.Visible = true;
                ddlUser.SelectedIndex = 0;
                ddlPaymentType.SelectedIndex = 0;
                ddlStore.SelectedIndex = 0;
                ddlCounter.SelectedIndex = 0;
                ddlSaleMode.SelectedIndex = 0;
                hfINVSubModule.Value = "SL";
                lblHeadText.Text = "Sales Item Wise";
            }
            if (tvSettings.SelectedValue == "SLSTREG")
            {
                blWebPharmaReport.SubModule = "SLSTREG";
                divcatgry.Visible = false;
                divDate.Visible = true;
                divStore.Visible = true;
                divItemsPopUp.Visible = false;
                divButtons.Visible = true;
                divStoreFromTo.Visible = false;
                divAsOnDate.Visible = false;
                divIssueMode.Visible = false;
                divuser.Visible = false;
                divEmployee.Visible = false;
                divSupplierpopup.Visible = false;
                divCustomer.Visible = false;
                divCustomer2.Visible = false;
                divQuantitySearch.Visible = false;
                divNoSales.Visible = false;
                divBillNo.Visible = false;
                divCounter.Visible = false;
                divShift.Visible = false;
                divPaymentType.Visible = false;
                divSaleMode.Visible = false;
                divSdDate.Visible = false;
                DivReportType.Visible = true;
                hfINVSubModule.Value = "SL";
                lblHeadText.Text = "Sales Store Wise Register";
            }
            if (tvSettings.SelectedValue == "SLSTDET")
            {
                blWebPharmaReport.SubModule = "SLSTDET";
                divcatgry.Visible = false;
                divDate.Visible = true;
                divStore.Visible = true;
                divItemsPopUp.Visible = false;
                divButtons.Visible = true;
                divStoreFromTo.Visible = false;
                divAsOnDate.Visible = false;
                divIssueMode.Visible = false;
                divuser.Visible = false;
                divEmployee.Visible = false;
                divSupplierpopup.Visible = false;
                divCustomer.Visible = false;
                divCustomer2.Visible = false;
                divQuantitySearch.Visible = false;
                divNoSales.Visible = false;
                divCustomerGrp.Visible = false;
                divBillNo.Visible = false;
                divCounter.Visible = false;
                divShift.Visible = false;
                divPaymentType.Visible = false;
                divSaleMode.Visible = false;
                divSdDate.Visible = false;
                DivReportType.Visible = true;
                hfINVSubModule.Value = "SL";
                lblHeadText.Text = "Sales Store Wise Detail";
            }

            if (tvSettings.SelectedValue == "SLUSREG")
            {
                blWebPharmaReport.SubModule = "SLUSREG";
                divcatgry.Visible = false;
                divDate.Visible = true;
                divStore.Visible = false;
                divItemsPopUp.Visible = false;
                divuser.Visible = true;
                divEmployee.Visible = false;
                divButtons.Visible = true;
                divStoreFromTo.Visible = false;
                divAsOnDate.Visible = false;
                divIssueMode.Visible = false;
                divSupplierpopup.Visible = false;
                divCustomer.Visible = false;
                divCustomer2.Visible = false;
                divQuantitySearch.Visible = false;
                divNoSales.Visible = false;
                divCustomerGrp.Visible = false;
                divBillNo.Visible = false;
                divCounter.Visible = false;
                divShift.Visible = false;
                divPaymentType.Visible = false;
                divSaleMode.Visible = false;
                divSdDate.Visible = false;
                DivReportType.Visible = true;
                hfINVSubModule.Value = "SL";
                lblHeadText.Text = "Sales User Wise Register";
            }

            if (tvSettings.SelectedValue == "SLRREG")
            {
                blWebPharmaReport.SubModule = "SLRREG";
                divcatgry.Visible = false;
                divDate.Visible = true;
                divStore.Visible = true;
                divItemsPopUp.Visible = false;
                divuser.Visible = false;
                divEmployee.Visible = false;
                divButtons.Visible = true;
                divStoreFromTo.Visible = false;
                divAsOnDate.Visible = false;
                divIssueMode.Visible = false;
                divSupplierpopup.Visible = false;
                divCustomer.Visible = false;
                divCustomer2.Visible = false;
                divQuantitySearch.Visible = false;
                divNoSales.Visible = false;
                divCustomerGrp.Visible = false;
                divBillNo.Visible = false;
                divCounter.Visible = true;
                divShift.Visible = false;
                divuser.Visible = true;
                divSaleMode.Visible = false;
                divPaymentType.Visible = true;
                divSdDate.Visible = false;
                ddlUser.SelectedIndex = 0;
                ddlPaymentType.SelectedIndex = 0;
                ddlStore.SelectedIndex = 0;
                ddlCounter.SelectedIndex = 0;
                ddlSaleMode.SelectedIndex = 0;
                ddlUser.SelectedIndex = 0;
                ddlPaymentType.SelectedIndex = 0;

                DivReportType.Visible = true;
                ddlReportType.SelectedIndex = 0;

                hfINVSubModule.Value = "SLR";
                lblHeadText.Text = "Sales Return Register";
            }
            if (tvSettings.SelectedValue == "SLRREGC")
            {
                blWebPharmaReport.SubModule = "SLRREGC";
                divcatgry.Visible = true;
                divDate.Visible = true;
                divStore.Visible = true;
                divItemsPopUp.Visible = false;
                divuser.Visible = false;
                divEmployee.Visible = false;
                divButtons.Visible = true;
                divStoreFromTo.Visible = false;
                divAsOnDate.Visible = false;
                divIssueMode.Visible = false;
                divSupplierpopup.Visible = false;
                divCustomer.Visible = false;
                divCustomer2.Visible = false;
                divQuantitySearch.Visible = false;
                divNoSales.Visible = false;
                divCustomerGrp.Visible = false;
                divBillNo.Visible = false;
                divCounter.Visible = true;
                divShift.Visible = false;
                divuser.Visible = true;
                divSaleMode.Visible = false;
                divPaymentType.Visible = false;
                divSdDate.Visible = false;
                ddlUser.SelectedIndex = 0;
                ddlPaymentType.SelectedIndex = 0;
                ddlStore.SelectedIndex = 0;
                ddlCounter.SelectedIndex = 0;
                ddlSaleMode.SelectedIndex = 0;
                ddlUser.SelectedIndex = 0;

                DivReportType.Visible = true;
                ddlReportType.SelectedIndex = 0;

                hfINVSubModule.Value = "SLR";
                lblHeadText.Text = "Sales Return Register - Category Wise";
            }
            if (tvSettings.SelectedValue == "SLCS")
            {
                blWebPharmaReport.SubModule = "SLCS";
                divcatgry.Visible = false;
                divDate.Visible = true;
                divStore.Visible = true;
                divItemsPopUp.Visible = false;
                divuser.Visible = false;
                divEmployee.Visible = false;
                divButtons.Visible = true;
                divStoreFromTo.Visible = false;
                divAsOnDate.Visible = false;
                divIssueMode.Visible = false;
                divSupplierpopup.Visible = false;
                divCustomer.Visible = true;
                divCustomer2.Visible = false;
                divQuantitySearch.Visible = false;
                divNoSales.Visible = false;
                divCustomerGrp.Visible = false;
                divBillNo.Visible = false;
                divCounter.Visible = false;
                divShift.Visible = false;
                divuser.Visible = true;
                divSaleMode.Visible = true;
                divPaymentType.Visible = false;
                divSdDate.Visible = false;
                ddlUser.SelectedIndex = 0;
                ddlPaymentType.SelectedIndex = 0;
                ddlStore.SelectedIndex = 0;
                ddlCounter.SelectedIndex = 0;
                ddlSaleMode.SelectedIndex = 0;
                DivReportType.Visible = true;
                ddlReportType.SelectedIndex = 0;
                CustomerClear();
                ClearItem();
                ClearSupplier();
                hfINVSubModule.Value = "SL";
                lblHeadText.Text = "Sales Customer Wise";
            }
            if (tvSettings.SelectedValue == "SLCSST")
            {
                blWebPharmaReport.SubModule = "SLCSST";
                divcatgry.Visible = false;
                divDate.Visible = true;
                divStore.Visible = false;
                divItemsPopUp.Visible = false;
                divuser.Visible = false;
                divEmployee.Visible = false;
                divButtons.Visible = true;
                divStoreFromTo.Visible = false;
                divAsOnDate.Visible = false;
                divIssueMode.Visible = false;
                divSupplierpopup.Visible = false;
                divCustomer.Visible = false;
                divCustomer2.Visible = true;
                divQuantitySearch.Visible = false;
                divNoSales.Visible = false;
                divCustomerGrp.Visible = false;
                divBillNo.Visible = false;
                divCounter.Visible = false;
                divShift.Visible = false;
                divPaymentType.Visible = false;
                divSaleMode.Visible = false;
                divSdDate.Visible = false;
                DivReportType.Visible = true;
                hfINVSubModule.Value = "SL";
                lblHeadText.Text = "Sales Customer Statement Report";
            }
            if (tvSettings.SelectedValue == "SLNPCS")
            {
                blWebPharmaReport.SubModule = "SLNPCS";
                divcatgry.Visible = false;
                divDate.Visible = true;
                divStore.Visible = false;
                divItemsPopUp.Visible = false;
                divuser.Visible = false;
                divEmployee.Visible = false;
                divButtons.Visible = true;
                divStoreFromTo.Visible = false;
                divAsOnDate.Visible = false;
                divIssueMode.Visible = false;
                divSupplierpopup.Visible = false;
                divCustomer.Visible = false;
                divCustomer2.Visible = false;
                divQuantitySearch.Visible = false;
                divNoSales.Visible = true;
                divCustomerGrp.Visible = true;
                divBillNo.Visible = false;
                divCounter.Visible = false;
                divShift.Visible = false;
                divPaymentType.Visible = false;
                divSaleMode.Visible = false;
                divSdDate.Visible = false;
                DivReportType.Visible = true;
                hfINVSubModule.Value = "SL";
                lblHeadText.Text = "Non Performing Customer Statement Report";
            }
            if (tvSettings.SelectedValue == "SLRIW")
            {
                blWebPharmaReport.SubModule = "SLRIW";
                divcatgry.Visible = true;
                divDate.Visible = true;
                divStore.Visible = true;
                divItemsPopUp.Visible = true;
                divuser.Visible = false;
                divEmployee.Visible = false;
                divButtons.Visible = true;
                divStoreFromTo.Visible = false;
                divAsOnDate.Visible = false;
                divIssueMode.Visible = false;
                divSupplierpopup.Visible = false;
                divCustomer.Visible = false;
                divCustomer2.Visible = false;
                divQuantitySearch.Visible = false;
                divNoSales.Visible = false;
                divCustomerGrp.Visible = false;
                divBillNo.Visible = false;
                divCounter.Visible = false;
                divShift.Visible = false;
                divPaymentType.Visible = true;
                divSaleMode.Visible = false;
                divuser.Visible = true;
                divCounter.Visible = true;
                divSdDate.Visible = false;
                DivReportType.Visible = true;
                ddlPaymentType.SelectedIndex = 0;
                ddlUser.SelectedIndex = 0;
                ddlStore.SelectedIndex = 0;
                ddlCounter.SelectedIndex = 0;
                ddlUser.SelectedIndex = 0;
                ddlPaymentType.SelectedIndex = 0;
                ddlStore.SelectedIndex = 0;
                ddlCounter.SelectedIndex = 0;
                ddlSaleMode.SelectedIndex = 0;
                hfINVSubModule.Value = "SLR";
                lblHeadText.Text = "Sales Return Item Wise Register";
            }
            if (tvSettings.SelectedValue == "SLIM")
            {
                blWebPharmaReport.SubModule = "SLIM";
                divcatgry.Visible = true;
                divDate.Visible = true;
                divStore.Visible = true;
                divItemsPopUp.Visible = true;
                divButtons.Visible = true;
                divStoreFromTo.Visible = false;
                divAsOnDate.Visible = false;
                divIssueMode.Visible = false;
                divuser.Visible = false;
                divEmployee.Visible = false;
                divSupplierpopup.Visible = false;
                divCustomer.Visible = false;
                divCustomer2.Visible = false;
                divQuantitySearch.Visible = true;
                divNoSales.Visible = false;
                divCustomerGrp.Visible = false;
                divBillNo.Visible = false;
                divCounter.Visible = false;
                divPaymentType.Visible = false;
                divShift.Visible = false;
                divSaleMode.Visible = false;
                divSdDate.Visible = false;
                DivReportType.Visible = true;
                hfINVSubModule.Value = "SL";
                lblHeadText.Text = "Item Movement";
            }
            if (tvSettings.SelectedValue == "SLVR")
            {
                blWebPharmaReport.SubModule = "SLVR";
                divcatgry.Visible = false;
                divDate.Visible = true;
                divStore.Visible = true;
                divItemsPopUp.Visible = false;
                divButtons.Visible = true;
                divStoreFromTo.Visible = false;
                divAsOnDate.Visible = false;
                divIssueMode.Visible = false;
                divuser.Visible = false;
                divEmployee.Visible = false;
                divSupplierpopup.Visible = false;
                divCustomer.Visible = false;
                divCustomer2.Visible = false;
                divQuantitySearch.Visible = false;
                divNoSales.Visible = false;
                divCustomerGrp.Visible = false;
                divBillNo.Visible = true;
                divCounter.Visible = false;
                divShift.Visible = false;
                divPaymentType.Visible = false;
                divSaleMode.Visible = false;
                divSdDate.Visible = false;
                DivReportType.Visible = true;
                hfINVSubModule.Value = "SL";
                lblHeadText.Text = "Voucher report";
            }
            if (tvSettings.SelectedValue == "SHCL")
            {
                blWebPharmaReport.SubModule = "SHCL";
                divDate.Visible = true;
                divCounter.Visible = true;
                divShift.Visible = true;
                divButtons.Visible = true;
                divcatgry.Visible = false;
                divStore.Visible = false;
                divItemsPopUp.Visible = false;
                divStoreFromTo.Visible = false;
                divAsOnDate.Visible = false;
                divIssueMode.Visible = false;
                divuser.Visible = false;
                divEmployee.Visible = false;
                divSupplierpopup.Visible = false;
                divCustomer.Visible = false;
                divCustomer2.Visible = false;
                divQuantitySearch.Visible = false;
                divNoSales.Visible = false;
                divCustomerGrp.Visible = false;
                divBillNo.Visible = false;
                divPaymentType.Visible = false;
                divSaleMode.Visible = false;
                divSdDate.Visible = false;
                DivReportType.Visible = true;
                hfINVSubModule.Value = "SHICL";
                lblHeadText.Text = "Shift Close";
            }
            if (tvSettings.SelectedValue == "VATPUR")
            {
                blWebPharmaReport.SubModule = "VATPUR";
                divcatgry.Visible = false;
                divDate.Visible = true;
                divAsOnDate.Visible = false;
                divStore.Visible = true;
                divItemsPopUp.Visible = false;
                divStoreFromTo.Visible = false;
                divIssueMode.Visible = false;
                divuser.Visible = false;
                divEmployee.Visible = false;
                divButtons.Visible = true;
                divSupplierpopup.Visible = false;
                divCustomer.Visible = false;
                divCustomer2.Visible = false;
                divQuantitySearch.Visible = false;
                divNoSales.Visible = false;
                divBillNo.Visible = false;
                divCounter.Visible = false;
                divShift.Visible = false;
                divPaymentType.Visible = false;
                divSaleMode.Visible = false;
                DivReportType.Visible = true;
                divSdDate.Visible = false;
                ddlReportType.SelectedIndex = 0;
                hfINVSubModule.Value = "VAT";
                lblHeadText.Text = "Purchase wise GST";
            }
            if (tvSettings.SelectedValue == "VATSAL")
            {
                blWebPharmaReport.SubModule = "VATSAL";
                divcatgry.Visible = false;
                divDate.Visible = true;
                divAsOnDate.Visible = false;
                divStore.Visible = true;
                divItemsPopUp.Visible = false;
                divStoreFromTo.Visible = false;
                divIssueMode.Visible = false;
                divuser.Visible = false;
                divEmployee.Visible = false;
                divButtons.Visible = true;
                divSupplierpopup.Visible = false;
                divCustomer.Visible = false;
                divCustomer2.Visible = false;
                divQuantitySearch.Visible = false;
                divNoSales.Visible = false;
                divBillNo.Visible = false;
                divCounter.Visible = false;
                divShift.Visible = false;
                divPaymentType.Visible = false;
                divSaleMode.Visible = false;
                DivReportType.Visible = true;
                divSdDate.Visible = false;
                ddlReportType.SelectedIndex = 0;
                hfINVSubModule.Value = "VAT";
                lblHeadText.Text = "Sales wise GST";
            }
            if (tvSettings.SelectedValue == "GSTSTS")
            {
                blWebPharmaReport.SubModule = "GSTSTS";
                divcatgry.Visible = false;
                divDate.Visible = true;
                divAsOnDate.Visible = false;
                divStore.Visible = false;
                divItemsPopUp.Visible = false;
                divStoreFromTo.Visible = false;
                divIssueMode.Visible = false;
                divuser.Visible = false;
                divEmployee.Visible = false;
                divButtons.Visible = true;
                divSupplierpopup.Visible = false;
                divCustomer.Visible = false;
                divCustomer2.Visible = false;
                divQuantitySearch.Visible = false;
                divNoSales.Visible = false;
                divBillNo.Visible = false;
                divCounter.Visible = false;
                divShift.Visible = false;
                divPaymentType.Visible = false;
                divSaleMode.Visible = false;
                DivReportType.Visible = true;
                divSdDate.Visible = false;
                ddlReportType.SelectedIndex = 0;
                hfINVSubModule.Value = "VAT";
                lblHeadText.Text = "Sales Tax Summary";
            }
            if (tvSettings.SelectedValue == "GSTS")
            {
                blWebPharmaReport.SubModule = "GSTS";
                divcatgry.Visible = false;
                divDate.Visible = true;
                divAsOnDate.Visible = false;
                divStore.Visible = false;
                divItemsPopUp.Visible = false;
                divStoreFromTo.Visible = false;
                divIssueMode.Visible = false;
                divuser.Visible = false;
                divEmployee.Visible = false;
                divButtons.Visible = true;
                divSupplierpopup.Visible = false;
                divCustomer.Visible = false;
                divCustomer2.Visible = false;
                divQuantitySearch.Visible = false;
                divNoSales.Visible = false;
                divBillNo.Visible = false;
                divCounter.Visible = false;
                divShift.Visible = false;
                divPaymentType.Visible = false;
                divSaleMode.Visible = false;
                DivReportType.Visible = true;
                divSdDate.Visible = false;
                ddlReportType.SelectedIndex = 0;
                hfINVSubModule.Value = "VAT";
                lblHeadText.Text = "Sales Tax";
         
            }
            if (tvSettings.SelectedValue == "GSTHSN")
            {
                blWebPharmaReport.SubModule = "GSTHSN";
                divcatgry.Visible = false;
                divDate.Visible = true;
                divAsOnDate.Visible = false;
                divStore.Visible = false;
                divItemsPopUp.Visible = false;
                divStoreFromTo.Visible = false;
                divIssueMode.Visible = false;
                divuser.Visible = false;
                divEmployee.Visible = false;
                divButtons.Visible = true;
                divSupplierpopup.Visible = false;
                divCustomer.Visible = false;
                divCustomer2.Visible = false;
                divQuantitySearch.Visible = false;
                divNoSales.Visible = false;
                divBillNo.Visible = false;
                divCounter.Visible = false;
                divShift.Visible = false;
                divPaymentType.Visible = false;
                divSaleMode.Visible = false;
                DivReportType.Visible = true;
                divSdDate.Visible = false;
                ddlReportType.SelectedIndex = 0;
                hfINVSubModule.Value = "VAT";
                lblHeadText.Text = "HSN Code Report";
            }
            if (tvSettings.SelectedValue == "GSTHSN1")
            {
                blWebPharmaReport.SubModule = "GSTHSN1";
                divcatgry.Visible = false;
                divDate.Visible = true;
                divAsOnDate.Visible = false;
                divStore.Visible = false;
                divItemsPopUp.Visible = false;
                divStoreFromTo.Visible = false;
                divIssueMode.Visible = false;
                divuser.Visible = false;
                divEmployee.Visible = false;
                divButtons.Visible = true;
                divSupplierpopup.Visible = false;
                divCustomer.Visible = false;
                divCustomer2.Visible = false;
                divQuantitySearch.Visible = false;
                divNoSales.Visible = false;
                divBillNo.Visible = false;
                divCounter.Visible = false;
                divShift.Visible = false;
                divPaymentType.Visible = false;
                divSaleMode.Visible = false;
                DivReportType.Visible = true;
                divSdDate.Visible = false;
                ddlReportType.SelectedIndex = 0;
                hfINVSubModule.Value = "VAT";
                lblHeadText.Text = "HSN Code Report(Format-1)";
                lblHeadText.ToolTip = "This report is used for uploading purpose.";
            }
            if (tvSettings.SelectedValue == "VATOP")
            {
                blWebPharmaReport.SubModule = "VATOP";
                divcatgry.Visible = false;
                divDate.Visible = true;
                divAsOnDate.Visible = false;
                divStore.Visible = true;
                divItemsPopUp.Visible = false;
                divStoreFromTo.Visible = false;
                divIssueMode.Visible = false;
                divuser.Visible = false;
                divEmployee.Visible = false;
                divButtons.Visible = true;
                divSupplierpopup.Visible = false;
                divCustomer.Visible = false;
                divCustomer2.Visible = false;
                divQuantitySearch.Visible = false;
                divNoSales.Visible = false;
                divBillNo.Visible = false;
                divCounter.Visible = false;
                divShift.Visible = false;
                divPaymentType.Visible = false;
                divSaleMode.Visible = false;
                DivReportType.Visible = true;
                divSdDate.Visible = false;
                ddlReportType.SelectedIndex = 0;
                hfINVSubModule.Value = "VAT";
                lblHeadText.Text = "Output Tax";
            }
            if (tvSettings.SelectedValue == "VATIP")
            {
                blWebPharmaReport.SubModule = "VATIP";
                divcatgry.Visible = false;
                divDate.Visible = true;
                divAsOnDate.Visible = false;
                divStore.Visible = true;
                divItemsPopUp.Visible = false;
                divStoreFromTo.Visible = false;
                divIssueMode.Visible = false;
                divuser.Visible = false;
                divEmployee.Visible = false;
                divButtons.Visible = true;
                divSupplierpopup.Visible = false;
                divCustomer.Visible = false;
                divCustomer2.Visible = false;
                divQuantitySearch.Visible = false;
                divNoSales.Visible = false;
                divBillNo.Visible = false;
                divCounter.Visible = false;
                divShift.Visible = false;
                divPaymentType.Visible = false;
                divSaleMode.Visible = false;
                DivReportType.Visible = true;
                divSdDate.Visible = false;
                ddlReportType.SelectedIndex = 0;
                hfINVSubModule.Value = "VAT";
                lblHeadText.Text = "Input Tax";
            }
            if (tvSettings.SelectedValue == "VATPRR")
            {
                blWebPharmaReport.SubModule = "VATPRR";
                divcatgry.Visible = false;
                divDate.Visible = true;
                divAsOnDate.Visible = false;
                divStore.Visible = true;
                divItemsPopUp.Visible = false;
                divStoreFromTo.Visible = false;
                divIssueMode.Visible = false;
                divuser.Visible = false;
                divEmployee.Visible = false;
                divButtons.Visible = true;
                divSupplierpopup.Visible = false;
                divCustomer.Visible = false;
                divCustomer2.Visible = false;
                divQuantitySearch.Visible = false;
                divNoSales.Visible = false;
                divBillNo.Visible = false;
                divCounter.Visible = false;
                divShift.Visible = false;
                divPaymentType.Visible = false;
                divSaleMode.Visible = false;
                DivReportType.Visible = true;
                divSdDate.Visible = false;
                ddlReportType.SelectedIndex = 0;
                hfINVSubModule.Value = "VAT";
                lblHeadText.Text = "Purchase Returnwise VAT";
            }
            if (tvSettings.SelectedValue == "VATSLR")
            {
                blWebPharmaReport.SubModule = "VATSLR";
                divcatgry.Visible = false;
                divDate.Visible = true;
                divAsOnDate.Visible = false;
                divStore.Visible = true;
                divItemsPopUp.Visible = false;
                divStoreFromTo.Visible = false;
                divIssueMode.Visible = false;
                divuser.Visible = false;
                divEmployee.Visible = false;
                divButtons.Visible = true;
                divSupplierpopup.Visible = false;
                divCustomer.Visible = false;
                divCustomer2.Visible = false;
                divQuantitySearch.Visible = false;
                divNoSales.Visible = false;
                divBillNo.Visible = false;
                divCounter.Visible = false;
                divShift.Visible = false;
                divPaymentType.Visible = false;
                divSaleMode.Visible = false;
                DivReportType.Visible = true;
                divSdDate.Visible = false;
                ddlReportType.SelectedIndex = 0;
                hfINVSubModule.Value = "VAT";
                lblHeadText.Text = "Sales Returnwise VAT";
            }
            if (tvSettings.SelectedValue == "TFRREG")
            {

                blWebPharmaReport.SubModule = "TFRREG";
                divcatgry.Visible = true;
                divDate.Visible = true;
                divStore.Visible = false;
                divItemsPopUp.Visible = true;
                divStoreFromTo.Visible = true;
                divButtons.Visible = true;
                divAsOnDate.Visible = false;
                divIssueMode.Visible = false;
                divuser.Visible = false;
                divEmployee.Visible = false;
                divSupplierpopup.Visible = false;
                divCustomer.Visible = false;
                divCustomer2.Visible = false;
                divQuantitySearch.Visible = false;
                divNoSales.Visible = false;
                divBillNo.Visible = false;
                divCounter.Visible = false;
                divShift.Visible = false;
                divPaymentType.Visible = false;
                divSaleMode.Visible = false;
                divSdDate.Visible = false;
                DivReportType.Visible = true;
                hfINVSubModule.Value = "TFR";
                lblHeadText.Text = "Stock Transfer";
            }
            if (tvSettings.SelectedValue == "TFRODR")
            {
                DivReportType.Visible = true;
                ddlReportType.SelectedIndex = 0;
                blWebPharmaReport.SubModule = "TFRODR";
                divcatgry.Visible = false;
                divDate.Visible = true;
                divStore.Visible = false;
                divItemsPopUp.Visible = true;
                divStoreFromTo.Visible = true;
                divButtons.Visible = true;
                divAsOnDate.Visible = false;
                divIssueMode.Visible = false;
                divuser.Visible = false;
                divEmployee.Visible = false;
                divSupplierpopup.Visible = false;
                divCustomer.Visible = false;
                divCustomer2.Visible = false;
                divQuantitySearch.Visible = false;
                divNoSales.Visible = false;
                divBillNo.Visible = false;
                divCounter.Visible = false;
                divShift.Visible = false;
                divPaymentType.Visible = false;
                divSaleMode.Visible = false;
                divSdDate.Visible = false;
                hfINVSubModule.Value = "TFR";
                lblHeadText.Text = "Transfer Order";
            }
            if (tvSettings.SelectedValue == "TFRPRC")
            {
                blWebPharmaReport.SubModule = "TFRPRC";
                divcatgry.Visible = false;
                divDate.Visible = true;
                divStore.Visible = false;
                divItemsPopUp.Visible = true;
                divStoreFromTo.Visible = false;
                divButtons.Visible = true;
                divAsOnDate.Visible = false;
                divIssueMode.Visible = false;
                divuser.Visible = false;
                divEmployee.Visible = false;
                divSupplierpopup.Visible = false;
                divCustomer.Visible = false;
                divCustomer2.Visible = false;
                divQuantitySearch.Visible = false;
                divNoSales.Visible = false;
                divBillNo.Visible = false;
                divCounter.Visible = false;
                divShift.Visible = false;
                divPaymentType.Visible = false;
                divSaleMode.Visible = false;
                divSdDate.Visible = false;
                DivReportType.Visible = true;
                hfINVSubModule.Value = "TFR";
                lblHeadText.Text = "Price Conversion";
            }
            if (tvSettings.SelectedValue == "TFRITC")
            {
                blWebPharmaReport.SubModule = "TFRITC";
                divcatgry.Visible = false;
                divDate.Visible = true;
                divStore.Visible = false;
                //divItems.Visible = true;
                divItemsPopUp.Visible = true;
                divStoreFromTo.Visible = false;
                divButtons.Visible = true;
                divAsOnDate.Visible = false;
                divIssueMode.Visible = false;
                divuser.Visible = false;
                divEmployee.Visible = false;
                // divSupplier.Visible = false;
                divSupplierpopup.Visible = false;
                divCustomer.Visible = false;
                divCustomer2.Visible = false;
                divQuantitySearch.Visible = false;
                divNoSales.Visible = false;
                divBillNo.Visible = false;
                divCounter.Visible = false;
                divShift.Visible = false;
                divPaymentType.Visible = false;
                divSaleMode.Visible = false;
                divSdDate.Visible = false;
                DivReportType.Visible = true;
                hfINVSubModule.Value = "TFR";
                lblHeadText.Text = "Item Conversion";
            }
            if (tvSettings.SelectedValue == "SALLIA")
            {
                blWebPharmaReport.SubModule = "SALLIA";
                DivReportType.Visible = true;
                divcatgry.Visible = false;
                divDate.Visible = false;
                divStore.Visible = true;
                //divItems.Visible = false;
                divItemsPopUp.Visible = false;
                divStoreFromTo.Visible = false;
                divButtons.Visible = true;
                divAsOnDate.Visible = true;
                txtAsOnDate.Enabled = false;
                divIssueMode.Visible = false;
                divuser.Visible = false;
                divEmployee.Visible = false;
                // divSupplier.Visible = false;
                divSupplierpopup.Visible = false;
                divCustomer.Visible = false;
                divCustomer2.Visible = false;
                divQuantitySearch.Visible = false;
                divNoSales.Visible = false;
                divBillNo.Visible = false;
                divCounter.Visible = false;
                divShift.Visible = false;
                divPaymentType.Visible = false;
                divSaleMode.Visible = false;
                divSdDate.Visible = false;
                hfINVSubModule.Value = "LIA";
                lblHeadText.Text = "Liability Register";
            }
            if (tvSettings.SelectedValue == "SALLIAP")
            {


                blWebPharmaReport.SubModule = "SALLIAP";
                DivReportType.Visible = true;
                divcatgry.Visible = false;
                divDate.Visible = true;
                divStore.Visible = true;
                //divItems.Visible = false;
                divItemsPopUp.Visible = false;
                divStoreFromTo.Visible = false;
                divButtons.Visible = true;
                divAsOnDate.Visible = false;
                txtAsOnDate.Enabled = false;
                divIssueMode.Visible = false;
                divuser.Visible = false;
                divEmployee.Visible = false;
                // divSupplier.Visible = false;
                divSupplierpopup.Visible = false;
                divCustomer.Visible = false;
                divCustomer2.Visible = false;
                divQuantitySearch.Visible = false;
                divNoSales.Visible = false;
                divBillNo.Visible = false;
                divCounter.Visible = false;
                divShift.Visible = false;
                divPaymentType.Visible = false;
                divSaleMode.Visible = false;
                divSdDate.Visible = false;
                hfINVSubModule.Value = "LIA";
                lblHeadText.Text = "Salesman Liability Register PeriodWise";
            }
            if (tvSettings.SelectedValue == "SDPR")
            {
                blWebPharmaReport.SubModule = "SDPR";
                divcatgry.Visible = false;
                divDate.Visible = false;
                divStore.Visible = false;
                // divItems.Visible = false;
                divItemsPopUp.Visible = false;
                divButtons.Visible = true;
                divStoreFromTo.Visible = false;
                divAsOnDate.Visible = false;
                divIssueMode.Visible = false;
                divuser.Visible = false;
                divSaleMode.Visible = false;
                divPaymentType.Visible = false;
                divEmployee.Visible = false;
                // divSupplier.Visible = false;
                divSupplierpopup.Visible = false;
                divCustomer.Visible = false;
                divCustomer2.Visible = false;
                divNoSales.Visible = false;
                divQuantitySearch.Visible = false;
                divBillNo.Visible = false;
                divCounter.Visible = false;
                divShift.Visible = false;
                divSdDate.Visible = true;
                DivReportType.Visible = true;
                ddlReportType.SelectedIndex = 0;
                hfINVSubModule.Value = "SD";
                lblHeadText.Text = "Schedule Processed List";
            }
            if (tvSettings.SelectedValue == "SDPO")
            {
                blWebPharmaReport.SubModule = "SDPO";
                divcatgry.Visible = false;
                divDate.Visible = false;
                divStore.Visible = false;
                // divItems.Visible = false;
                divItemsPopUp.Visible = false;
                divButtons.Visible = true;
                divStoreFromTo.Visible = false;
                divAsOnDate.Visible = false;
                divIssueMode.Visible = false;
                divuser.Visible = false;
                divSaleMode.Visible = false;
                divPaymentType.Visible = false;
                divEmployee.Visible = false;
                // divSupplier.Visible = false;
                divSupplierpopup.Visible = false;
                divCustomer.Visible = false;
                divCustomer2.Visible = false;
                divNoSales.Visible = false;
                divQuantitySearch.Visible = false;
                divBillNo.Visible = false;
                divCounter.Visible = false;
                divShift.Visible = false;
                divSdDate.Visible = true;
                DivReportType.Visible = true;
                ddlReportType.SelectedIndex = 0;
                hfINVSubModule.Value = "SD";
                lblHeadText.Text = "Schedule Posted List";
            }
            if (tvSettings.SelectedValue == "SDDF")
            {
                blWebPharmaReport.SubModule = "SDDF";
                divcatgry.Visible = false;
                divDate.Visible = false;
                divStore.Visible = false;
                divButtons.Visible = true;
                divStoreFromTo.Visible = false;
                divAsOnDate.Visible = false;
                divIssueMode.Visible = false;
                divuser.Visible = false;
                divSaleMode.Visible = false;
                divPaymentType.Visible = false;
                divEmployee.Visible = false;
                // divSupplier.Visible = false;
                divSupplierpopup.Visible = false;
                divCustomer.Visible = false;
                divCustomer2.Visible = false;
                divNoSales.Visible = false;
                divQuantitySearch.Visible = false;
                divBillNo.Visible = false;
                divCounter.Visible = false;
                divShift.Visible = false;
                divSdDate.Visible = true;
                DivReportType.Visible = true;
                ddlReportType.SelectedIndex = 0;
                hfINVSubModule.Value = "SD";
                lblHeadText.Text = "Schedule Difference List";
            }
            if (tvSettings.SelectedValue == "POSTW")
            {
                blWebPharmaReport.SubModule = "POSTW";
                divcatgry.Visible = false;
                divDate.Visible = false;
                divStore.Visible = false;
                divButtons.Visible = true;
                divStoreFromTo.Visible = false;
                divAsOnDate.Visible = false;
                divIssueMode.Visible = false;
                divuser.Visible = false;
                divSaleMode.Visible = false;
                divPaymentType.Visible = false;
                divEmployee.Visible = false;
                divSupplierpopup.Visible = true;
                divCustomer.Visible = false;
                divCustomer2.Visible = false;
                divNoSales.Visible = false;
                divQuantitySearch.Visible = true;
                divBillNo.Visible = false;
                divCounter.Visible = false;
                divShift.Visible = false;
                divSdDate.Visible = false;
                DivReportType.Visible = true;
                ddlReportType.SelectedIndex = 0;
                ddlQuantityCriteria.Enabled = false;
                ddlQuantityCriteria.SelectedIndex = 2;
                hfINVSubModule.Value = "PO";
                lblHeadText.Text = "Purchase Order Report";
            }
            if (tvSettings.SelectedValue == "GENSUPP")
            {
                blWebPharmaReport.SubModule = "GENSUPP";
                divcatgry.Visible = false;
                divDate.Visible = false;
                divStore.Visible = false;
                divButtons.Visible = true;
                divStoreFromTo.Visible = false;
                divAsOnDate.Visible = false;
                divIssueMode.Visible = false;
                divuser.Visible = false;
                divSaleMode.Visible = false;
                divPaymentType.Visible = false;
                divEmployee.Visible = false;
                divSupplierpopup.Visible = false;
                divCustomer.Visible = false;
                divCustomer2.Visible = false;
                divNoSales.Visible = false;
                divQuantitySearch.Visible = false;
                divBillNo.Visible = false;
                divCounter.Visible = false;
                divShift.Visible = false;
                divSdDate.Visible = false;
                DivReportType.Visible = true;
                ddlReportType.SelectedIndex = 0;
                ddlQuantityCriteria.Enabled = false;
                hfINVSubModule.Value = "GEN";
                lblHeadText.Text = "Supplier List Report";
            }
            if (tvSettings.SelectedValue == "SLRCS")
            {
                blWebPharmaReport.SubModule = "SLRCS";
                divcatgry.Visible = false;
                divDate.Visible = true;
                divStore.Visible = true;
                divItemsPopUp.Visible = false;
                divuser.Visible = false;
                divEmployee.Visible = false;
                divButtons.Visible = true;
                divStoreFromTo.Visible = false;
                divAsOnDate.Visible = false;
                divIssueMode.Visible = false;
                divSupplierpopup.Visible = false;
                divCustomer.Visible = true;
                divCustomer2.Visible = false;
                divQuantitySearch.Visible = false;
                divNoSales.Visible = false;
                divCustomerGrp.Visible = false;
                divBillNo.Visible = false;
                divCounter.Visible = false;
                divShift.Visible = false;
                divuser.Visible = true;
                divSaleMode.Visible = true;
                divPaymentType.Visible = false;
                divSdDate.Visible = false;
                ddlUser.SelectedIndex = 0;
                ddlPaymentType.SelectedIndex = 0;
                ddlStore.SelectedIndex = 0;
                ddlCounter.SelectedIndex = 0;
                ddlSaleMode.SelectedIndex = 0;
                DivReportType.Visible = true;
                ddlReportType.SelectedIndex = 0;
                CustomerClear();
                ClearItem();
                ClearSupplier();
                hfINVSubModule.Value = "SLR";
                lblHeadText.Text = "SalesReturn Customer Wise";
            }
            if (tvSettings.SelectedValue == "GSTSIB")
            {
                blWebPharmaReport.SubModule = "GSTSIB";
                divButtons.Visible = true;
                divDate.Visible = true;
               
                ddlUser.SelectedIndex = 0;
                ddlPaymentType.SelectedIndex = 0;
                ddlStore.SelectedIndex = 0;
                ddlCounter.SelectedIndex = 0;
                ddlSaleMode.SelectedIndex = 0;
                ddlReportType.SelectedIndex = 4;
                DivReportType.Visible = true;
                CustomerClear();
                ClearItem();
                ClearSupplier();
                hfINVSubModule.Value = "VAT";
                lblHeadText.Text = "GST Sales Invoice and Bill";
            }
            else if (tvSettings.SelectedValue == "GSTPIB")
            {
                blWebPharmaReport.SubModule = "GSTPIB";
                divButtons.Visible = true;
                divDate.Visible = true;

                ddlUser.SelectedIndex = 0;
                ddlPaymentType.SelectedIndex = 0;
                ddlStore.SelectedIndex = 0;
                ddlCounter.SelectedIndex = 0;
                ddlSaleMode.SelectedIndex = 0;
                ddlReportType.SelectedIndex = 4;
                DivReportType.Visible = true;
                CustomerClear();
                ClearItem();
                ClearSupplier();
                hfINVSubModule.Value = "VAT";
                lblHeadText.Text = "GST Purchase Invoice and Bill";
            }
            else if (tvSettings.SelectedValue == "SUPSI")
            {
                blWebPharmaReport.SubModule = "SUPSI";
                divButtons.Visible = true;
                divDate.Visible = true;
                divSupplierpopup.Visible = true;
                DivReportType.Visible = true;
                ddlReportType.SelectedIndex = 0;

                CustomerClear();
                ClearItem();
                ClearSupplier();

                hfINVSubModule.Value = "SL";
                lblHeadText.Text = "Supplier Wise Sale Items";
            }
            else if (tvSettings.SelectedValue == "SUPPI")
            {
                blWebPharmaReport.SubModule = "SUPPI";
                divButtons.Visible = true;
                divDate.Visible = true;
                divSupplierpopup.Visible = true;
                DivReportType.Visible = true;
                ddlReportType.SelectedIndex = 0;

                CustomerClear();
                ClearItem();
                ClearSupplier();

                hfINVSubModule.Value = "PU";
                lblHeadText.Text = "Supplier Wise Purchase Items";
            }
            else if (tvSettings.SelectedValue == "SLREGH")
            {
                blWebPharmaReport.SubModule = "SLREGH";
                divcatgry.Visible = true;
                divStore.Visible = true;
                divCounter.Visible = true;
                divuser.Visible = true;
                divSaleMode.Visible = true;
                ddlUser.SelectedIndex = 0;
                ddlSaleMode.SelectedIndex = 0;
                divShift.Visible = Visible;

                divButtons.Visible = true;
                divDate.Visible = true;
                DivReportType.Visible = true;
                ddlReportType.SelectedIndex = 0;

                ddlUser.SelectedIndex = 0;
                ddlStore.SelectedIndex = 0;
                ddlCounter.SelectedIndex = 0;
                ddlSaleMode.SelectedIndex = 0;
                ddlShift.SelectedIndex = 0;

                CustomerClear();
                ClearItem();
                ClearSupplier();

                hfINVSubModule.Value = "SL";
                lblHeadText.Text = "Sales Register(HSN Code)";
            }
             
            populate = null;
        }
        catch (Exception ex)
        {

        }
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
     {
        try
        {
            BlReportData BlReportData = new BlReportData();
            if (ValidateData(tvSettings.SelectedValue.ToString()))
            {
                if (SetReportData(BlReportData))
                {
                    hfUrl.Value = Request.QueryString["uid"];
                    hfClient.Value = Request.QueryString["clnt"];
                    Session["date"] = txtPassDate.Text;
                    hfdate.Value = txtPassDate.Text;
                    ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "popUp();", true);
                }
            }
        }
        catch (Exception ex)
        {
        }
    }
    protected void imgbtnCustomer_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            ((TextBox)CommonPopup1.FindControl("txtName")).Text = txtCustomer.Text.Trim();
            // txtCustomer.Text = "";
            ((TextBox)CommonPopup1.FindControl("txtRefno")).Text = txtCustomer.Text.Trim();
            ((HiddenField)CommonPopup1.FindControl("hfColumn")).Value = "Name";
            CustomerFill(1);
            divAdd.Style.Add("display", "block");
            // divView.Style.Add("display", "none");


        }
        catch (Exception ex)
        {
        }
    }
    protected void ImageButton3_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            Supplier();
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "toastr.warning('" + ex.Message + "')", true);
        }
    }
    protected void ImageItemPopup_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            Item();
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "toastr.warning('" + ex.Message + "')", true);
        }
    }
    protected void btnClear_Click(object sender, EventArgs e)
    {
        ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "ShowDate();", true);
        //ddlIssueMode.SelectedIndex = 0;
        ddlCategoryFirst.SelectedIndex = 0;
        ddlStore.SelectedIndex = 0;
        ddlCounter.SelectedIndex = 0;
        //  ddlSupplier.SelectedIndex = 0;
        Populate populate = new Populate();
        //  if (hfINVSubModule.Value == "SL" || hfINVSubModule.Value == "SLR")
        //   populate.FillDropdownListAll(ddlItems, "Items", "Name", "ID_Items", "Name", " Active='true' AND FK_Client=" + hfClient.Value, "");
        //else
        //   populate.FillDropdownListAll(ddlItems, "Items", "Name", "ID_Items", "Name", " Active='true' AND ITemtype=2 AND FK_Client=" + hfClient.Value, "");
        // populate.FillDropdownListAll(ddlItems, "Items", "ShortName+'-'+Name", "ID_Items", "ID_Items", " Active='true' AND FK_Client=" + hfClient.Value, "");
        populate.FillDropdownListAll(ddlCounter, "Counter", "Name", "ID_Counter", "Name", "Active='True' AND FK_Client=" + hfClient.Value, "");
        populate.FillDropdownListAll(ddlShift, "Shift", "Name", "ID_Shift", "Name", " Active='True' AND FK_Client=" + hfClient.Value, "");
        populate.FillGoodsIssueMode(ddlIssueMode);
        populate = null;
        // ddlItems.SelectedIndex = 0;    
        txtItemRefno.Text = "";
        textItemName.Text = "";
        txtSupplierName.Text = "";
        TxtSupplierRefNo.Text = "";
        if (tvSettings.SelectedValue == "STKIL")
        {
            ddlCategoryFirst.SelectedIndex = 0;
            ddlStore.SelectedIndex = 0;
        }
        else if (tvSettings.SelectedValue == "STKDL")
        {
            ddlCategoryFirst.SelectedIndex = 0;
            ddlStore.SelectedIndex = 0;
            ClearItem();
            //ddlItems.SelectedIndex = 0;
        }
        else if (tvSettings.SelectedValue == "OPSTK")
        {
            //ddlCategoryFirst.SelectedIndex = 0;
            ddlStore.SelectedIndex = 0;
            ClearItem();
            //ddlItems.SelectedIndex = 0;
        }
        else if (tvSettings.SelectedValue == "STKNEG")
        {
            ddlCategoryFirst.SelectedIndex = 0;
            ddlStore.SelectedIndex = 0;
            ClearItem();
            // ddlItems.SelectedIndex = 0;
        }
        else if (tvSettings.SelectedValue == "STKIM")
        {

            ClearItem();
            // ddlItems.SelectedIndex = 0;
        }
        else if (tvSettings.SelectedValue == "STKMQ")
        {
            ddlCategoryFirst.SelectedIndex = 0;
            ddlStore.SelectedIndex = 0;
            ClearItem();
            // ddlItems.SelectedIndex = 0;
        }
        else if (tvSettings.SelectedValue == "STKTR")
        {
            ddlCategoryFirst.SelectedIndex = 0;
            ddlFromStore.SelectedIndex = 0;
            ddlToStore.SelectedIndex = 0;
            //ddlItems.SelectedIndex = 0;
            ClearItem();
        }
        else if (tvSettings.SelectedValue == "STKVF")
        {
            ddlStore.SelectedIndex = 0;
            ddlCategoryFirst.SelectedIndex = 0;
            ddlReportType.SelectedIndex = 0;
        }
        else if (tvSettings.SelectedValue == "STKVL")
        {
            ddlStore.SelectedIndex = 0;
            ddlReportType.SelectedIndex = 0;
            ddlValueCriteria.SelectedIndex = 0;
            txtValues.Text = "";

        }
        else if (tvSettings.SelectedValue == "GIIR")
        {
            ddlIssueMode.SelectedIndex = 0;
        }
        else if (tvSettings.SelectedValue == "GIIW")
        {
            ddlIssueMode.SelectedIndex = 0;
            // ddlItems.SelectedIndex = 0;
            ClearItem();
        }
        else if (tvSettings.SelectedValue == "PUREG" || tvSettings.SelectedValue == "PURREGDET")
        {
            ddlStore.SelectedIndex = 0;
        }
        else if (tvSettings.SelectedValue == "PUIW")
        {
            ddlStore.SelectedIndex = 0;
            // ddlItems.SelectedIndex = 0;
            ddlCategoryFirst.SelectedIndex = 0;
            ClearItem();
        }
        else if (tvSettings.SelectedValue == "PUIWSP")
        {
            // ddlItems.SelectedIndex = 0;
            ClearItem();
            ClearSupplier();
            // ddlSupplier.SelectedIndex = 0;
        }
        else if (tvSettings.SelectedValue == "PUSPIW")
        {
            // ddlSupplier.SelectedIndex = 0;
            ClearSupplier();
        }
        else if (tvSettings.SelectedValue == "GENSUPP")
        {
            // ddlSupplier.SelectedIndex = 0;
            ClearSupplier();
        }
        else if (tvSettings.SelectedValue == "MRIW")
        {
            ddlCategoryFirst.SelectedIndex = 0;
            ddlStore.SelectedIndex = 0;
            // ddlItems.SelectedIndex = 0;
            ClearItem();
        }
        else if (tvSettings.SelectedValue == "STKREG")
        {
            ddlCategoryFirst.SelectedIndex = 0;
            ddlStore.SelectedIndex = 0;
            //  ddlItems.SelectedIndex = 0;
            ClearItem();
        }
        else if (tvSettings.SelectedValue == "STKREGC")
        {
            ddlCategoryFirst.SelectedIndex = 0;
            ddlStore.SelectedIndex = 0;
            // ddlItems.SelectedIndex = 0;
            ClearItem();
        }
        else if (tvSettings.SelectedValue == "PURREG")
        {
            ddlStore.SelectedIndex = 0;
        }
        else if (tvSettings.SelectedValue == "PURIW")
        {
            ddlCategoryFirst.SelectedIndex = 0;
            ddlStore.SelectedIndex = 0;
            //ddlItems.SelectedIndex = 0;

            ClearItem();
        }
        else if (tvSettings.SelectedValue == "SLIW")
        {
            ddlCategoryFirst.SelectedIndex = 0;
            //  ddlItems.SelectedIndex = 0;
            ddlUser.SelectedIndex = 0;
            ddlSaleMode.SelectedIndex = 0;
            ClearItem();
        }
        else if (tvSettings.SelectedValue == "SLSTREG")
        {
            ddlStore.SelectedIndex = 0;
        }
        else if (tvSettings.SelectedValue == "SLSTDET")
        {
            ddlStore.SelectedIndex = 0;
        }
        else if (tvSettings.SelectedValue == "SLSTIW")
        {
            ddlStore.SelectedIndex = 0;
            //  ddlItems.SelectedIndex = 0;
            ClearItem();
        }
        else if (tvSettings.SelectedValue == "SLUSREG")
        {
            ddlUser.SelectedIndex = 0;
        }
        else if (tvSettings.SelectedValue == "SLCS")
        {
            //// ddlCustomer.SelectedIndex = 0;
            CustomerClear();
            ddlSaleMode.SelectedIndex = 0;
            ddlUser.SelectedIndex = 0;

        }
        else if (tvSettings.SelectedValue == "SLCSST")
        {
            //// ddlCustomer2.SelectedIndex = 0;
            CustomerClear();
        }
        else if (tvSettings.SelectedValue == "SLNPCS")
        {
            txtNoSales.Text = "0";
            ddlCustomerGrp.SelectedIndex = 0;
        }
        else if (tvSettings.SelectedValue == "SLMWREG")
        {
            ddlEmployee.SelectedIndex = 0;
        }
        else if (tvSettings.SelectedValue == "SLIM")
        {
            ddlQuantityCriteria.SelectedIndex = 0;
            txtQauntity.Text = "";
            ddlSaleMode.SelectedIndex = 0;
            ddlUser.SelectedIndex = 0;
        }
        else if (tvSettings.SelectedValue == "SLVR")
        {
            txtBillNo.Text = "";
            ddlStore.SelectedIndex = 0;
        }
        else if (tvSettings.SelectedValue == "SLREG")
        {
            //ddlCategoryFirst.SelectedIndex = 0;
            //ddlStore.SelectedIndex = 0;
            //ddlItems.SelectedIndex = 0;
            ddlStore.SelectedIndex = 0;
            ddlUser.SelectedIndex = 0;
            ddlPaymentType.SelectedIndex = 0;
            ddlSaleMode.SelectedIndex = 0;
            ddlUser.SelectedIndex = 0;
            ddlCounter.SelectedIndex = 0;
            //  ddlPaymentType.SelectedIndex = 0;
            //ddlCounter.SelectedIndex = 0;
            //ddlShift.SelectedIndex = 0;ddlFromStore
            ClearItem();
        }
        else if (tvSettings.SelectedValue == "SLREGDT")
        {
            //ddlCategoryFirst.SelectedIndex = 0;
            //ddlStore.SelectedIndex = 0;
            //ddlItems.SelectedIndex = 0;
            ddlStore.SelectedIndex = 0;
            ddlUser.SelectedIndex = 0;
            ddlPaymentType.SelectedIndex = 0;
            ddlSaleMode.SelectedIndex = 0;
            ddlUser.SelectedIndex = 0;
            ddlCounter.SelectedIndex = 0;
            //  ddlPaymentType.SelectedIndex = 0;
            //ddlCounter.SelectedIndex = 0;
            //ddlShift.SelectedIndex = 0;ddlFromStore
            ClearItem();
        }
        else if (tvSettings.SelectedValue == "SLREGC")
        {
            ddlStore.SelectedIndex = 0;
            ddlUser.SelectedIndex = 0;
            ddlPaymentType.SelectedIndex = 0;
            ddlSaleMode.SelectedIndex = 0;
            ddlUser.SelectedIndex = 0;
            ddlCounter.SelectedIndex = 0;
            ClearItem();
        }
        else if (tvSettings.SelectedValue == "BLREG")
        {

            ddlStore.SelectedIndex = 0;

            ddlUser.SelectedIndex = 0;
            ddlPaymentType.SelectedIndex = 0;
            ddlSaleMode.SelectedIndex = 0;
            ddlUser.SelectedIndex = 0;
            ddlCounter.SelectedIndex = 0;
        }
        else if (tvSettings.SelectedValue == "CWSAL")
        {
            ddlStore.SelectedIndex = 0;
            ddlUser.SelectedIndex = 0;
            ddlPaymentType.SelectedIndex = 0;
            ddlSaleMode.SelectedIndex = 0;
            ddlUser.SelectedIndex = 0;
            ddlCounter.SelectedIndex = 0;
        }
        else if (tvSettings.SelectedValue == "CCWR")
        {
            ddlStore.SelectedIndex = 0;
            ddlUser.SelectedIndex = 0;
            ddlPaymentType.SelectedIndex = 0;
            ddlSaleMode.SelectedIndex = 0;
            ddlUser.SelectedIndex = 0;
            ddlCounter.SelectedIndex = 0;
        }
        else if (tvSettings.SelectedValue == "SAWR")
        {
            ddlStore.SelectedIndex = 0;
            ddlUser.SelectedIndex = 0;
            ddlPaymentType.SelectedIndex = 0;
            ddlSaleMode.SelectedIndex = 0;
            ddlUser.SelectedIndex = 0;
            ddlCounter.SelectedIndex = 0;
        }
        else if (tvSettings.SelectedValue == "SLABD")
        {
            ddlStore.SelectedIndex = 0;
            ddlUser.SelectedIndex = 0;
            ddlPaymentType.SelectedIndex = 0;
            ddlSaleMode.SelectedIndex = 0;
            ddlUser.SelectedIndex = 0;
            ddlCounter.SelectedIndex = 0;
        }
        else if (tvSettings.SelectedValue == "SLRIW")
        {
            ddlStore.SelectedIndex = 0;
            ddlUser.SelectedIndex = 0;
            ddlPaymentType.SelectedIndex = 0;
            ddlSaleMode.SelectedIndex = 0;
            ddlCounter.SelectedIndex = 0;
            ddlCategoryFirst.SelectedIndex = 0;
        }
        else if (tvSettings.SelectedValue == "SLRREG")
        {
            ddlStore.SelectedIndex = 0;
            ddlCounter.SelectedIndex = 0;
            ddlUser.SelectedIndex = 0;
            ddlPaymentType.SelectedIndex = 0;
        }
        else if (tvSettings.SelectedValue == "SLRREGC")
        {
            ddlStore.SelectedIndex = 0;
            ddlCounter.SelectedIndex = 0;
            ddlUser.SelectedIndex = 0;
            ddlPaymentType.SelectedIndex = 0;
        }
        else if (tvSettings.SelectedValue == "TFRREG")
        {
            try
            {
                // ddlItems.SelectedIndex = 0;
                ddlFromStore.SelectedIndex = 0;
                ddlCategoryFirst.SelectedIndex = 0;
                ddlFromStore.SelectedIndex = 0;
                ddlToStore.SelectedIndex = 0;
                ClearItem();
            }
            catch (Exception ex)
            {
            }

        }
        else if (tvSettings.SelectedValue == "TFRODR")
        {
            try
            {
                // ddlItems.SelectedIndex = 0;
                ddlFromStore.SelectedIndex = 0;
                //ddlCategoryFirst.SelectedIndex = 0;
                //ddlFromStore.SelectedIndex = 0;
                ddlToStore.SelectedIndex = 0;
                //ClearItem();
            }
            catch (Exception ex)
            {
            }

        }
        else if (tvSettings.SelectedValue == "SLRCS")
        {
            //// ddlCustomer.SelectedIndex = 0;
            CustomerClear();
            ddlSaleMode.SelectedIndex = 0;
            ddlUser.SelectedIndex = 0;

        }

    }
    protected void ddlStore_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            Populate populate = new Populate();
            if (ddlStore.SelectedIndex > 0)
            {
                populate.FillDropdownList(ddlCounter, "Counter", "Name", "ID_Counter", "Name", "Active='True' AND FK_Client=" + hfClient.Value + "  AND FK_Store=" + ddlStore.SelectedValue, "");
            }
            else
            {
                populate.FillDropdownListAll(ddlCounter, "Counter", "Name", "ID_Counter", "Name", "Active='True' AND FK_Client=" + hfClient.Value, "");
            }
        }
        catch (Exception ex)
        { }
    }
    protected void ddlCounter_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            Populate populate = new Populate();
            if (ddlCounter.SelectedIndex > 0)
            {
                populate.FillDropdownList(ddlShift, "Shift", "Name", "ID_Shift", "Name", " Active='True' AND FK_Client=" + hfClient.Value + " and FK_Couter=" + ddlCounter.SelectedValue, "");
            }
            else
            {
                populate.FillDropdownListAll(ddlShift, "Shift", "Name", "ID_Shift", "Name", " Active='True' AND FK_Client=" + hfClient.Value, "");
            }
        }
        catch (Exception ex)
        { }
    }
    #endregion
}