/*----------------------------------------------------------------------------------------------------------------------------------------------------------
Purpose    : Create Inventory Reports
Author     : Aiswarya
Copyright  : Perfect Software Solutions (Clt) Pvt Ltd
Created on : 31/10/2018
----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
MODIFICATIONS 
On			By			    OMID
26/01/2019 VINISHA     Stock > Stock Register,Stock ItemList,Damage List,Stock Verification,Current Stock Items
31/01/2019 VINISHA     Sales > Sales Register,Sales Item Wise,Sales Customer Wise,Bill Register,Sales Abstract,Sales Doctor Wise,Sales Manufacture Wise,Sales Generic Wise,Sales Schedule Wise
                       Sales > Item Movement
01/02/2019 VINISHA     Sales > Sales Return Register,Item Wise,Customer Wise Sales Return,Bill Register
 11-10-2021 NIJI TJ     GST>Consolidated Sales Register
 05/11/2021   NIJI TJ   GST>SALES GST REPORT
20/04/2022   NIJI TJ   Sales>Daily Sales Summary REPORT
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


public partial class InventoryReport : System.Web.UI.Page
{
    #region Functions
    public void DoctorClear()
    {
        txtDoctor.Text = "";
    }
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

        populate.FillDropdownListAll(ddlCategoryFirst, "Category", "CategoryName", "ID_Category", "CategoryName", "Cancelled=0 AND FK_Client=" + hfClient.Value, "");
        populate.FillDropdownListAll(ddlStore, "Store", "StoName", "ID_Store", "StoName", "Active='True' AND Cancelled=0 and FK_Client=" + hfClient.Value, "");
        populate.FillDropdownListAll(ddlFromStore, "Store", "StoName", "ID_Store", "StoName", "Active='True' AND  Cancelled=0 AND FK_Client=" + hfClient.Value, "");
        populate.FillDropdownListAll(ddlToStore, "Store", "StoName", "ID_Store", "StoName", "Active='True' AND  Cancelled=0 AND FK_Client=" + hfClient.Value, "");
        populate.FillDropdownListAll(ddlUser, "Users", "Username", "ID_Users", "Username", "Active='True' AND FK_Client=" + hfClient.Value, "");
        populate.FillDropdownList(ddlCustomer2, "Customer", "CustomerName", "ID_Customer", "CustomerName", "Active='True' AND Cancelled=0 And FK_Client=" + hfClient.Value, "");
        populate.FillDropdownListAll(ddlCustomerGrp, "CustomerGroup", "CustGroupName", "ID_CustomerGroup", "CustGroupName", "Active='True' and Cancelled=0 And FK_Client=" + hfClient.Value, "");
        populate.FillDropdownListAll(ddlCounter, "Counters", "CounterName", "ID_Counters", "CounterName", "Active='True' AND Cancelled=0 AND FK_Client=" + hfClient.Value, "");
        populate.FillDropdownListAll(ddlShift, "ShiftMaster", "ShiftName", "ID_ShiftMaster", "ShiftName", " Active='True' AND Cancelled=0 AND FK_Client=" + hfClient.Value, "");

        populate.FillDropdownListAll(ddlManufacturer, "Manufacturer", "ManufactureName", "ID_Manufacturer", "ManufactureName", " Cancelled=0 AND FK_Client=" + hfClient.Value, "");
        populate.FillDropdownListAll(ddlSchedule, "ScheduleMaster", "ScheduleName", "ID_Schedule", "ScheduleName", "Cancelled=0 AND  FK_Client=" + hfClient.Value, "");
        populate.FillDropdownListAll(ddlGeneric, "Generic", "GenericName", "ID_Generic", "GenericName", "Cancelled=0 AND  FK_Client=" + hfClient.Value, "");
        populate.FillDropdownListAll(ddlBin, "Bin", "BinName", "ID_Bin", "BinName", " FK_Client=" + hfClient.Value + " AND Cancelled=0", "");
     
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
        if ((Value == "STKIL") || (Value == "STKILE") || (Value == "STKDL") || (Value == "SLSMI") || (Value == "SLSMIC") || (Value == "PRSL") || (Value == "SLPR") || (Value == "PRCM") || (Value == "PRSMA") || (Value == "STKTR") || (Value == "GENSUPP") || (Value == "GENCCR") || (Value == "STKNEG") || (Value == "STKIM") || (Value == "STKVF") || (Value == "STKITM") || (Value == "STKVL") || (Value == "GIIR") || (Value == "GIIW") || (Value == "PURREG") || (Value == "PUREG") || (Value == "PURREGDET") || (Value == "PUIW") || (Value == "STKREG") || (Value == "STKREGC") || (Value == "MRIW") || (Value == "SLREG") || (Value == "SLREGDT") || (Value == "BLREG") || (Value == "SLRBLREG") || (Value == "SLREGC") || (Value == "SAWR") || (Value == "SLABD") || (Value == "CCWR") || (Value == "CWSAL") || (Value == "SLMWREG") || (Value == "SLRREG") || (Value == "SLRREGC") || (Value == "SLVR") || (Value == "SLRIW") || (Value == "SLIW") || (Value == "SLMW") || (Value == "SLGW") || (Value == "SLSW") || (Value == "STLMQ") || (Value == "SLSSTREG") || (Value == "SLSTIW") || (Value == "SLUSREG") || (Value == "SLUSDET") || (Value == "SLCS") || (Value == "SLDS") || (Value == "VATOP") || (Value == "VATIP") || (Value == "VATPRR") || (Value == "PRG") || (Value == "VATSLR") || (Value == "STKMC") || (Value == "SLRCS") || (Value == "SALLIAP") || (Value == "PUDET"))
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
                    tvSettings.Nodes[nodes].SelectAction = TreeNodeSelectAction.Expand;
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
        bluseraccess.FK_Pages = 24;

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
                    case "STKILE":
                        hfReportType.Value = "CRPT";
                        blReportData.ReportFile = "ExpiryItem.rpt";
                        //blReportData.ReportFile = "Item.rdlc";
                        blReportData.ReportTable = "ExpiryItemList";
                        blReportData.ReportSchema = "ExpiryItemList";
                        break;
                    case "STKDL":
                        hfReportType.Value = "CRPT";
                        blReportData.ReportFile = "StockDamage.rpt";
                        blReportData.ReportTable = "StockDamageList";
                        blReportData.ReportSchema = "StockDamageList";
                        break;
                    case "OPSTK":
                        hfReportType.Value = "CRPT";
                        blReportData.ReportFile = "OpeningStock.rpt";
                        blReportData.ReportTable = "OpeningStockList";
                        blReportData.ReportSchema = "OpeningStockList";
                        break;
                    case "STKVF":
                        hfReportType.Value = "CRPT";
                        blReportData.ReportFile = "crptStockVarification.rpt";
                        blReportData.ReportTable = "StockVarification";
                        blReportData.ReportSchema = "StockVarification";
                        break;
                    case "STKITM":
                        hfReportType.Value = "CRPT";
                        blReportData.ReportFile = "crptItemWiseStock.rpt";
                        blReportData.ReportTable = "ItemWiseStock";
                        blReportData.ReportSchema = "ItemWiseStock";
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
                        hfReportType.Value = "CRPT";
                        blReportData.ReportFile = "MinimumQuantity.rpt";
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
                    case "STKREGCN":
                        hfReportType.Value = "CRPT";
                        blReportData.ReportFile = "StockRegisterConsolidateRP.rpt";
                        // blReportData.ReportFile = "StockRegister.rdlc";
                        blReportData.ReportTable = "StockRegisterConsolidateRP";
                        blReportData.ReportSchema = "StockRegisterConsolidateRP";
                        break;
                    case "STKCSDR":
                        hfReportType.Value = "CRPT";
                        blReportData.ReportFile = "DailyConsolidatedTransaction.rpt";
                        blReportData.ReportTable = "DailyConsolidatedTransaction";
                        blReportData.ReportSchema = "DailyConsolidatedTransaction";
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
                        blReportData.ReportFile = "PurchaseTaxWise.rpt";//"VatPurchasewise.rdlc";
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
                    case "PRG":
                        hfReportType.Value = "CRPT";
                        blReportData.ReportFile = "PurchaseReturnwiseGST.rpt";//"VatPurchasewise.rdlc";
                        // blReportData.ReportFile = "VatSaleswise.rdlc";
                        blReportData.ReportTable = "PurchaseReturnwiseGST";
                        blReportData.ReportSchema = "PurchaseReturnwiseGST";
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
                    case "SALESCON":
                        hfReportType.Value = "CRPT";
                        blReportData.ReportFile = "SalesRegisterConsolidated.rpt";
                        blReportData.ReportTable = "SaleRegisterConsolidated";
                        blReportData.ReportSchema = "SaleRegisterConsolidated";
                        break;
                    case "GSTSALES":
                        hfReportType.Value = "CRPT";
                        blReportData.ReportFile = "SalesGSTReport.rpt";
                        blReportData.ReportTable = "SalesGSTReport";
                        blReportData.ReportSchema = "SalesGSTReport";
                        break;
                    case "GSTSRHSN1":
                        hfReportType.Value = "CRPT";
                        blReportData.ReportFile = "GSTSRHSN1CodeReport.rpt";
                        blReportData.ReportTable = "GSTSRHSN1CodeReport";
                        blReportData.ReportSchema = "GSTSRHSN1CodeReport";
                        break;
                    case "VATDTSL":
                        hfReportType.Value = "CRPT";
                        blReportData.ReportFile = "VatSalesDatewise.rpt";
                        blReportData.ReportTable = "VatSalesDatewise";
                        blReportData.ReportSchema = "VatSalesDatewise";
                        break;
                    case "GSTHSNC":
                        hfReportType.Value = "CRPT";
                        blReportData.ReportFile = "GSTHSNCCodeReport.rpt";
                        blReportData.ReportTable = "GSTHSNCCodeReport";
                        blReportData.ReportSchema = "GSTHSNCCodeReport";
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
                        hfReportType.Value = "CRPT";
                        blReportData.ReportFile = "PurchaseItemWiseSupplier.rpt";
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
                    case "PUDET":
                        hfReportType.Value = "CRPT";
                        blReportData.ReportFile = "PurchaseDetailed.rpt";
                        blReportData.ReportTable = "PurchaseDetailed";
                        blReportData.ReportSchema = "PurchaseDetailed";
                        break;


                }
                break;
            case "PUR":
                switch (Value)
                {
                    case "PURREG":
                        hfReportType.Value = "CRPT";
                        blReportData.ReportFile = "PurchaseReturnRegister.rpt";
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
                    case "GENCCR":
                    case "GENCCRD":
                        hfReportType.Value = "CRPT";
                        blReportData.ReportFile = "CustomerCardList.rpt";
                        blReportData.ReportTable = "CustomerCardList";
                        blReportData.ReportSchema = "CustomerCardList";
                        break;
                    //case "GENCCRD":
                    //    hfReportType.Value = "CRPT";
                    //    blReportData.ReportFile = "CustomerCardList.rpt";
                    //    blReportData.ReportTable = "CustomerCardList";
                    //    blReportData.ReportSchema = "CustomerCardList";
                    //    break;

                }

                break;
            case "SL":
                switch (Value)
                {
                    case "SLREG":
                        if (ddlReportMethod.SelectedValue == "0")
                        {
                            hfReportType.Value = "CRPT";
                            blReportData.ReportFile = "SalesRegister.rpt";
                            blReportData.ReportTable = "SalesRegister";
                            blReportData.ReportSchema = "SalesRegister";
                        }
                        else
                        {
                            hfReportType.Value = "CRPT";
                            blReportData.ReportFile = "SalesRegisterDateWise.rpt";
                            blReportData.ReportTable = "SalesRegisterDateWise";
                            blReportData.ReportSchema = "SalesRegisterDateWise";
                        }
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
                        hfReportType.Value = "CRPT";
                        blReportData.ReportFile = "SaleItemWise.rpt";
                        blReportData.ReportTable = "SalesItemWise";
                        blReportData.ReportSchema = "SalesItemWise";
                        break;
                    case "SLMW":
                        hfReportType.Value = "CRPT";
                        blReportData.ReportFile = "SaleMGSWise.rpt";
                        blReportData.ReportTable = "SaleMGSWise";
                        blReportData.ReportSchema = "SaleMGSWise";
                        break;
                    case "SLGW":
                        hfReportType.Value = "CRPT";
                        blReportData.ReportFile = "SaleMGSWise.rpt";
                        blReportData.ReportTable = "SaleMGSWise";
                        blReportData.ReportSchema = "SaleMGSWise";
                        break;
                    case "SLSW":
                        hfReportType.Value = "CRPT";
                        blReportData.ReportFile = "SaleMGSWise.rpt";
                        blReportData.ReportTable = "SaleMGSWise";
                        blReportData.ReportSchema = "SaleMGSWise";
                        break;
                    case "SLSM":
                        hfReportType.Value = "CRPT";
                        blReportData.ReportFile = "SchedulewiseSales.rpt";
                        blReportData.ReportTable = "SchedulewiseSales";
                        blReportData.ReportSchema = "SchedulewiseSales";
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
                    case "SLDS":
                        hfReportType.Value = "CRPT";
                        blReportData.ReportFile = "crptCustomerWiseSales.rpt";
                        // blReportData.ReportFile = "SaleCustomerWise.rdlc";
                        blReportData.ReportTable = "SaleCustomerWise";
                        blReportData.ReportSchema = "SaleCustomerWise";
                        break;
                    case "SLNPCS":
                        blReportData.ReportFile = "NonPerformingCustomer.rdlc";
                        blReportData.ReportTable = "NonPerformingCustomer";
                        blReportData.ReportSchema = "NonPerformingCustomer";
                        break;
                    case "SLIM":
                        hfReportType.Value = "CRPT";
                        blReportData.ReportFile = "SaleItemMovement.rpt";
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
                    case "DLYSLSMRY":
                        hfReportType.Value = "CRPT";
                        blReportData.ReportFile = "DailySalesSummary.rpt";
                        blReportData.ReportTable = "DailySalesSummary";
                        blReportData.ReportSchema = "DailySalesSummary";

                        break;
                    case "SLPR":
                        hfReportType.Value = "CRPT";
                        blReportData.ReportFile = "SalesProfitability.rpt";
                        blReportData.ReportTable = "SalesProfitability";
                        blReportData.ReportSchema = "SalesProfitability";

                        break;

                }
                break;
            case "SLR":
                switch (Value)
                {
                    case "SLRREG":
                        hfReportType.Value = "CRPT";
                        blReportData.ReportFile = "SalesReturnRegister.rpt";
                        blReportData.ReportTable = "SalesReturnRegister";
                        blReportData.ReportSchema = "SalesReturnRegister";
                        break;
                    case "SLRBLREG":
                        hfReportType.Value = "CRPT";
                        blReportData.ReportFile = "SalesReturnBillRegister.rpt";
                        blReportData.ReportTable = "SalesReturnBillRegister";
                        blReportData.ReportSchema = "SalesReturnBillRegister";
                        break;
                    case "SLRIW":
                        hfReportType.Value = "CRPT";
                        blReportData.ReportFile = "SalesReturnItemWise.rpt";
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
                        blReportData.ReportFile = "PurchaseOrderReport.rpt";
                        blReportData.ReportTable = "PurchaseOrderStoreWise";
                        blReportData.ReportSchema = "PurchaseOrderStoreWise";
                        break;
                    case "POSTORE":
                        hfReportType.Value = "CRPT";
                        blReportData.ReportFile = "PurchaseOrderReport.rpt";
                        blReportData.ReportTable = "PurchaseOrderStoreWise";
                        blReportData.ReportSchema = "PurchaseOrderStoreWise";
                        break;
                }
                break;
            case "IN":
                switch (Value)
                {
                    case "INR":
                        hfReportType.Value = "CRPT";
                        blReportData.ReportFile = "IndentRequest.rpt";
                        blReportData.ReportTable = "IndentRequest";
                        blReportData.ReportSchema = "IndentRequest";
                        break;
                    case "IND":
                        hfReportType.Value = "CRPT";
                        blReportData.ReportFile = "Indent.rpt";
                        blReportData.ReportTable = "Indent";
                        blReportData.ReportSchema = "Indent";
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
            BlReportData.FK_CategoryFirst = ddlCategoryFirst.SelectedValue == "" ? 0 : Convert.ToInt64(ddlCategoryFirst.SelectedValue);
            BlReportData.FK_Store = ddlStore.SelectedValue == "" ? 0 : Convert.ToInt64(ddlStore.SelectedValue);
            BlReportData.FromStore = ddlFromStore.SelectedValue == "" ? 0 : Convert.ToInt64(ddlFromStore.SelectedValue);
            BlReportData.ToStore = ddlToStore.SelectedValue == "" ? 0 : Convert.ToInt64(ddlToStore.SelectedValue);
            BlReportData.FromDate = Convert.ToDateTime(txtFromDate.Text);
            BlReportData.AsonDate = Convert.ToDateTime(txtAsOnDate.Text);
            BlReportData.ToDate = Convert.ToDateTime(txtToDate.Text);
            BlReportData.FK_Items = Convert.ToInt64(txtItemRefno.Text == "" ? "0" : txtItemRefno.Text.Trim());
            BlReportData.FK_Client = Convert.ToInt64(hfClient.Value);
            BlReportData.FK_Users = ddlUser.SelectedValue == "" ? 0 : Convert.ToInt64(ddlUser.SelectedValue);
            BlReportData.FK_Supplier = Convert.ToInt64(TxtSupplierRefNo.Text == "" ? "0" : TxtSupplierRefNo.Text.Trim());
            BlReportData.FK_Customer = Convert.ToInt64(txtCustRefno.Text == "" ? "0" : txtCustRefno.Text.Trim());      ////ddlCustomer.SelectedValue
            // BlReportData.FK_Salesman = Convert.ToInt64(ddlEmployee.SelectedValue);
            BlReportData.FK_Counter = ddlCounter.SelectedValue == "" ? 0 : Convert.ToInt64(ddlCounter.SelectedValue);
            BlReportData.FK_Shift = ddlShift.SelectedValue == "" ? 0 : Convert.ToInt64(ddlShift.SelectedValue);
            BlReportData.FK_Manufacturer = ddlManufacturer.SelectedValue == "" ? 0 : Convert.ToInt64(ddlManufacturer.SelectedValue);
            BlReportData.FK_Schedule = ddlSchedule.SelectedValue == "" ? 0 : Convert.ToInt64(ddlSchedule.SelectedValue);
            BlReportData.FK_Generic = ddlGeneric.SelectedValue == "" ? 0 : Convert.ToInt64(ddlGeneric.SelectedValue);
            BlReportData.UserCode = hfUser.Value;
            BlReportData.ReportSchemaPath = Server.MapPath("Schema/");
            BlReportData.ReportPath = Server.MapPath("Reports/");
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
                BlReportData.PageTitle = "Stock Item List";
            }
            else if (BlReportData.SubID == "STKILE")
            {
                BlReportData.Critrea1 = ddlBin.SelectedValue;
                // BlReportData.PageTitle = "Expiry List As On " +  (DateTime.Parse(txtAsOnDate.Text).Day) +"  "+ CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(DateTime.Parse(txtAsOnDate.Text).Month) + " " + DateTime.Parse(txtAsOnDate.Text).Year;
                BlReportData.PageTitle = "Expiry List From: " + BlReportData.FromDate.ToShortDateString() + "  To : " + BlReportData.ToDate.ToShortDateString() + "      Supplier : " + (txtSupplierName.Text == "" ? "All" : txtSupplierName.Text);
            }
            else if (BlReportData.SubID == "STKREG")
            {
                BlReportData.Critrea1 = chkWithoutExpiry.Checked == true ? "1" : "0";
                BlReportData.PageTitle = "Stock Register List From : " + BlReportData.FromDate.ToShortDateString() + "  To : " + BlReportData.ToDate.ToShortDateString();
            }
            else if (BlReportData.SubID == "STKREGC")
            {
                BlReportData.PageTitle = "Stock Register List From : " + BlReportData.FromDate.ToShortDateString() + "  To : " + BlReportData.ToDate.ToShortDateString();
            }
            else if (BlReportData.SubID == "STKREGCN")
            {
                BlReportData.PageTitle = "Stock Register Consolidate As on Date  : " + BlReportData.AsonDate.ToShortDateString();
                //BlReportData.PageTitle = "Stock Register Consolidate   From : " + BlReportData.FromDate.ToShortDateString() + "  To : " + BlReportData.ToDate.ToShortDateString();
            }
          
                
            else if (BlReportData.SubID == "STKDL")
            {
                BlReportData.PageTitle = "Damaged Item List From : " + BlReportData.FromDate.ToShortDateString() + "  To : " + BlReportData.ToDate.ToShortDateString();
            }
            else if (BlReportData.SubID == "OPSTK")
            {
                BlReportData.PageTitle = "Opening Stock List";
            }
            else if (BlReportData.SubID == "STKVF")
            {
                BlReportData.FK_Store = Convert.ToInt64(ddlStore.SelectedValue);
                BlReportData.FK_CategoryFirst = Convert.ToInt64(ddlCategoryFirst.SelectedValue);
                BlReportData.Critrea1 = chkWithoutExpiry.Checked==true?"1":"0";
                BlReportData.PageTitle = "Current Stock Report  "; //As on Date  : " + BlReportData.AsonDate.ToShortDateString();
            }
            else if (BlReportData.SubID == "STKITM")
            {
                BlReportData.FK_Store = Convert.ToInt64(ddlStore.SelectedValue);
                BlReportData.FK_CategoryFirst = Convert.ToInt64(ddlCategoryFirst.SelectedValue);
                BlReportData.PageTitle = "Item Wise Stock Report  "; //As on Date  : " + BlReportData.AsonDate.ToShortDateString();
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
                BlReportData.PageTitle = "Negative Stock List ";
            }
            else if (BlReportData.SubID == "STKIM")
            {
                BlReportData.PageTitle = "Item Movement Register From : " + BlReportData.FromDate.ToShortDateString() + "  To : " + BlReportData.ToDate.ToShortDateString();

            }
            else if (BlReportData.SubID == "STKMQ")
            {
                BlReportData.FK_Store = Convert.ToInt64(ddlStore.SelectedValue);
                BlReportData.PageTitle = "Current Stock Items  " + (ddlLevel.SelectedValue == "0" ? "" : ddlLevel.SelectedItem.Text);
                BlReportData.Critrea1 = ddlLevel.SelectedValue;
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
                BlReportData.Critrea1 = ddlApproved.SelectedValue == "" ? "0" : ddlApproved.SelectedValue;
                if (ddlApproved.SelectedValue == "1")
                {
                    BlReportData.PageTitle = "Purchase Register Report From : " + BlReportData.FromDate.ToShortDateString() + "  To : " + BlReportData.ToDate.ToShortDateString();
                }
                else
                {
                    BlReportData.PageTitle = "Unauthorised Purchase Register Report From : " + BlReportData.FromDate.ToShortDateString() + "  To : " + BlReportData.ToDate.ToShortDateString();
                }
            } if (BlReportData.SubID == "PUDET")
            {
                BlReportData.PageTitle = "Purchase Detailed Report From : " + BlReportData.FromDate.ToShortDateString() + "  To : " + BlReportData.ToDate.ToShortDateString();
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

            else if (BlReportData.SubID == "DLYSLSMRY")
            {
                 
                BlReportData.PageTitle = "Daily Sales Summary From : " + BlReportData.FromDate.ToShortDateString() + "  To : " + BlReportData.ToDate.ToShortDateString();

            }
            else if (BlReportData.SubID == "SUPPI")
            {
                BlReportData.PageTitle = "Supplier Wise Purchase Item Report From : " + BlReportData.FromDate.ToShortDateString() + "  To : " + BlReportData.ToDate.ToShortDateString();
            }
            else if (BlReportData.SubID == "SALESCON")
            {
                BlReportData.PageTitle = "Sales Register Consolidated Report From : " + BlReportData.FromDate.ToShortDateString() + "  To : " + BlReportData.ToDate.ToShortDateString();
            }
            else if (BlReportData.SubID == "GSTSALES")
            {
                BlReportData.PageTitle = "Sales GST Report From : " + BlReportData.FromDate.ToShortDateString() + "  To : " + BlReportData.ToDate.ToShortDateString();
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
            if (BlReportData.SubID == "SLPR")
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
                 BlReportData.Critrea2 = ddlReportMethod.SelectedValue;
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
            if (BlReportData.SubID == "SLRBLREG")
            {
                BlReportData.Critrea1 = ddlPaymentType.SelectedValue;
                BlReportData.PageTitle = "Sales Return Bill Register Report From : " + BlReportData.FromDate.ToShortDateString() + "  To : " + BlReportData.ToDate.ToShortDateString();
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
            if (BlReportData.SubID == "SLMW")
            {
                BlReportData.Critrea1 = ddlSaleMode.SelectedValue;
                BlReportData.FK_Generic = 0;
                BlReportData.FK_Schedule = 0;
                BlReportData.PageTitle = "Manufacture Wise Sales Report From : " + BlReportData.FromDate.ToShortDateString() + "  To : " + BlReportData.ToDate.ToShortDateString();
            }
            if (BlReportData.SubID == "SLGW")
            {
                BlReportData.Critrea1 = ddlSaleMode.SelectedValue;
                BlReportData.FK_Manufacturer = 0;
                BlReportData.FK_Schedule = 0;
                BlReportData.PageTitle = "Generic Wise Sales Report From : " + BlReportData.FromDate.ToShortDateString() + "  To : " + BlReportData.ToDate.ToShortDateString();
            }
            if (BlReportData.SubID == "SLSW")
            {
                BlReportData.Critrea1 = ddlSaleMode.SelectedValue;
                BlReportData.FK_Manufacturer = 0;
                BlReportData.FK_Generic = 0;
                BlReportData.PageTitle = "Schedule Wise Sales Report From : " + BlReportData.FromDate.ToShortDateString() + "  To : " + BlReportData.ToDate.ToShortDateString();
            }
            if (BlReportData.SubID == "SLSM")
            {
                BlReportData.Critrea1 = ddlSaleMode.SelectedValue;
                // BlReportData.Critrea2 = .SelectedValue;
                BlReportData.Critrea2 = txtDoctor.Text;
                BlReportData.FK_Items = Convert.ToInt64(txtItemRefno.Text == "" ? "0" : txtItemRefno.Text.Trim());
                BlReportData.FK_Customer = Convert.ToInt64(txtCustRefno.Text == "" ? "0" : txtCustRefno.Text.Trim());
                BlReportData.FK_Manufacturer = 0;
                BlReportData.FK_Generic = 0;
                BlReportData.PageTitle = "Schedule Item  Wise Sales Report From : " + BlReportData.FromDate.ToShortDateString() + "  To : " + BlReportData.ToDate.ToShortDateString();
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
            if (BlReportData.SubID == "SLDS")
            {
                BlReportData.FK_CustomerGroup = Convert.ToInt64(txtDoctRefno.Text == "" ? "0" : txtDoctRefno.Text.Trim());
                BlReportData.Critrea1 = ddlSaleMode.SelectedValue;
                BlReportData.Critrea2 = txtDoctor.Text;
                BlReportData.PageTitle = "Sales Doctor Wise Report From : " + BlReportData.FromDate.ToShortDateString() + "  To : " + BlReportData.ToDate.ToShortDateString();
            }
            if (BlReportData.SubID == "SLCS")
            {
                BlReportData.Critrea1 = ddlSaleMode.SelectedValue;
                BlReportData.Critrea2 = txtCustomer.Text;
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
                BlReportData.PageTitle = "Monthly Sales Report -" + CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(DateTime.Parse(txtAsOnDate.Text).Month) + " " + DateTime.Parse(txtAsOnDate.Text).Year;
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
            if (BlReportData.SubID == "GSTPIB")
            {
                BlReportData.PageTitle = "GST Purchase Invoice and Bill : " + BlReportData.FromDate.ToShortDateString() + "  To : " + BlReportData.ToDate.ToShortDateString();
            }
            if (BlReportData.SubID == "VATPUR")
            {
                BlReportData.PageTitle = "Purchase wise GST Report From : " + BlReportData.FromDate.ToShortDateString() + "  To : " + BlReportData.ToDate.ToShortDateString();
            }
            if (BlReportData.SubID == "VATSAL")
            {
                BlReportData.PageTitle = "Saleswise GST Report From : " + BlReportData.FromDate.ToShortDateString() + "  To : " + BlReportData.ToDate.ToShortDateString();
            }
            if (BlReportData.SubID == "VATPRR")
            {
                BlReportData.PageTitle = "Purchase Returnwise GST Report From : " + BlReportData.FromDate.ToShortDateString() + "  To : " + BlReportData.ToDate.ToShortDateString();
            }
            if (BlReportData.SubID == "VATSLR")
            {
                BlReportData.PageTitle = "Sales Returnwise GST Report From : " + BlReportData.FromDate.ToShortDateString() + "  To : " + BlReportData.ToDate.ToShortDateString();
            }
            if (BlReportData.SubID == "VATOP")
            {
                BlReportData.PageTitle = "Output GST Report From : " + BlReportData.FromDate.ToShortDateString() + "  To : " + BlReportData.ToDate.ToShortDateString();
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
                BlReportData.PageTitle = "Salesman Liability Period Wise Between : " + BlReportData.FromDate.ToShortDateString() + "  To : " + BlReportData.ToDate.ToShortDateString();
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
                BlReportData.Critrea1 = ddlPaymentType.SelectedValue;
                BlReportData.PageTitle = "Sales Return Customer Wise Report From : " + BlReportData.FromDate.ToShortDateString() + "  To : " + BlReportData.ToDate.ToShortDateString();
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
            if (BlReportData.SubID == "POSTORE")
            {
                BlReportData.PageTitle = "Store Wise Purchase Order Report  As on Date  : " + BlReportData.AsonDate.ToShortDateString();
            }
            if (BlReportData.SubID == "PRG")
            {
                BlReportData.PageTitle = "Purchase Returnwise GST Report From : " + BlReportData.FromDate.ToShortDateString() + "  To : " + BlReportData.ToDate.ToShortDateString();
            }
            if (BlReportData.SubID == "INR")
            {
                BlReportData.PageTitle = "Indent Request Report From : " + BlReportData.FromDate.ToShortDateString() + "  To : " + BlReportData.ToDate.ToShortDateString();
            }
            if (BlReportData.SubID == "IND")
            {
                BlReportData.PageTitle = "Indent  Report From : " + BlReportData.FromDate.ToShortDateString() + "  To : " + BlReportData.ToDate.ToShortDateString();
            }
            if (BlReportData.SubID == "GSTSRHSN1")
            {
                BlReportData.PageTitle = "Sales Return HSN Code Report(Format-1) From : " + BlReportData.FromDate.ToShortDateString() + "  To : " + BlReportData.ToDate.ToShortDateString();
            }
            if (BlReportData.SubID == "VATDTSL")
            {
                BlReportData.PageTitle = "Sales Date Wise GST Report From : " + BlReportData.FromDate.ToShortDateString() + "  To : " + BlReportData.ToDate.ToShortDateString();
            }
            if (BlReportData.SubID == "GSTHSNC")
            {
                BlReportData.PageTitle = "HSN Code Report Consolidate From : " + BlReportData.FromDate.ToShortDateString() + "  To : " + BlReportData.ToDate.ToShortDateString();
            }
            if (BlReportData.SubID == "STKCSDR")
            {
                BlReportData.PageTitle = "Daily Consolidated Transaction From : " + BlReportData.FromDate.ToShortDateString() + "  To : " + BlReportData.ToDate.ToShortDateString();
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
            DataTable dtTitle = new DataTable();
           
            
            dtTitle = BlReportData.SelectStoreName();
            if (dtTitle.Rows.Count > 0)
            {
                BlReportData.Title = dtTitle.Rows[0]["Name"].ToString() == "" ? "0" : dtTitle.Rows[0]["Name"].ToString().Trim();
            }
            else { BlReportData.Title = Session["mainstore"].ToString(); }


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
        DivApproved.Visible = false;
        divSdDate.Visible = false;
        divManufacturer.Visible = false;
        divSchedule.Visible = false;
        divGeneric.Visible = false;
        divLevel.Visible = false;
        divDoctor.Visible = false;
        divBin.Visible = false;
        divExpiry.Visible = false;
        divReportMethod.Visible = false;
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
        else if (CommonPopup1.CallingFunction == "DoctorFill")
        {
            txtDoctRefno.Text = strRefno;
            txtDoctor.Text = strName;

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
        Populate.FillGrid((GridView)CommonPopup1.FindControl("gdvSearch"), "Customer", "ID_Customer as ID, CustomerName as Name,CustomerCode as CustCode", "ID_Customer", "", "FK_Client=" + hfClient.Value.Split(',')[0] + "   AND Active=1 ", searchCol + "  LIKE '" + strName + "%'", pageIndex, 10, "", (Repeater)CommonPopup1.FindControl("rptPager"), this.Page);

        Populate = null;
        if (txtCustomer.Text == string.Empty)
        {
            lblPopupHeader.Text = "Customer";
            mpCommon.Show();
        }
    }
    public void DoctorFill(int pageIndex)
    {
        txtDoctor.Text = "";
        Populate Populate = new Populate();
        CommonPopup1.CallingFunction = "DoctorFill";
        string strName = ((TextBox)CommonPopup1.FindControl("txtName")).Text;
        string Refno = ((TextBox)CommonPopup1.FindControl("txtRefno")).Text;
        string searchCol = ((HiddenField)CommonPopup1.FindControl("hfColumn")).Value;
        Refno = Refno == string.Empty || Refno == null ? "0" : Refno;
        searchCol = searchCol == string.Empty ? "[Name]" : "[" + searchCol + "]";
        Populate.FillGrid((GridView)CommonPopup1.FindControl("gdvSearch"), "Doctor", "ID_Doctor as ID, DoctorName as Name", "ID_Doctor", "", "FK_Client=" + hfClient.Value.Split(',')[0], searchCol + "  LIKE '" + strName + "%'", pageIndex, 10, "", (Repeater)CommonPopup1.FindControl("rptPager"), this.Page);

        Populate = null;
        if (txtDoctor.Text == string.Empty)
        {
            lblPopupHeader.Text = "Doctor";
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
      
        Populate.FillPopUpGrid((GridView)CommonPopup1.FindControl("gdvSearch"), "Supplier", "ID_Supplier as ID,SupplierName as Name", "ID_Supplier", "SupplierName", "FK_Client=" + hfClient.Value.Split(',')[0] + " AND  Cancelled=0  and SupplierName", strName, null);
       
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
        Populate.FillPopUpGrid((GridView)CommonPopup1.FindControl("gdvSearch"), "Items", "ID_Items as ID,ItemName as Name", "ID_Items", "ItemName", "FK_Client=" + hfClient.Value.Split(',')[0] + " AND  Cancelled=0 AND FK_Category= CASE WHEN " + Convert.ToString(ddlCategoryFirst.SelectedValue.ToString()) + "=0 THEN FK_Category ELSE  " + ddlCategoryFirst.SelectedValue + "  END  and ItemName", strName, null);
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
            ddlStore.SelectedValue = "0"; 

            Populate populate = new Populate();
            // populate.FillDropdownListAll(ddlItems, "Items", "Name", "ID_Items", "Name", " Active='true' AND FK_Client=" + hfClient.Value, "");
            if (tvSettings.SelectedValue.ToString() == "TFRODR")
            {
                populate.FillDropdownListSelect(ddlFromStore, "Store", "StoName", "ID_Store", "StoName", "Active='True'AND Cancelld=0 AND FK_Client=" + hfClient.Value, "");
                populate.FillDropdownListSelect(ddlToStore, "Store", "StoName", "ID_Store", "StoName", "Active='True' AND Cancelled=0 AND FK_Client=" + hfClient.Value, "");
            }
            else
            {
                populate.FillDropdownListAll(ddlFromStore, "Store", "StoName", "ID_Store", "StoName", "Active='True' and Cancelled=0 AND FK_Client=" + hfClient.Value, "");
                populate.FillDropdownListAll(ddlToStore, "Store", "StoName", "ID_Store", "StoName", "Active='True' and Cancelled=0 AND FK_Client=" + hfClient.Value, "");
            }
            BlInventoryReport blInventoryReport = new BlInventoryReport();
            if (tvSettings.SelectedValue == "GSTSIB")
            {
                ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "ShowDate('');", true);
            }
            else
                txtFromDate.Text = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1).ToString("dd/MM/yyyy");

            //ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "ShowDate();", true);
            // ddlSupplier.SelectedIndex = 0;
            CustomerClear();
            ClearItem();
            ClearSupplier();
            txtAsOnDate.Enabled = true;
            chkWithoutExpiry.Checked = false;
            if (tvSettings.SelectedValue == "STKREG")
            {
                blInventoryReport.SubModule = "STKREG";
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
                divManufacturer.Visible = true;
                divGeneric.Visible = true;
                divSchedule.Visible = true;
               
                ddlManufacturer.SelectedIndex = 0;
                ddlGeneric.SelectedIndex = 0;
                ddlSchedule.SelectedIndex = 0;
                ddlReportType.SelectedIndex = 0;
                divReportMethod.Visible = false;
                hfINVSubModule.Value = "STK";
                lblHeadText.Text = "Stock Register";
                divExpiry.Visible = true;
               
            }
            if (tvSettings.SelectedValue == "STKREGC")
            {
                blInventoryReport.SubModule = "STKREGC";
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
                divReportMethod.Visible = false;
                hfINVSubModule.Value = "STK";
                lblHeadText.Text = "Stock Register Consolidated";

            }
            if (tvSettings.SelectedValue == "STKIL")
            {
                blInventoryReport.SubModule = "STKIL";
                divcatgry.Visible = true;
                divDate.Visible = false;
                divStore.Visible = false;
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
                divManufacturer.Visible = true;
                divSchedule.Visible = true;
                divGeneric.Visible = true;
                ddlManufacturer.SelectedIndex = 0;
                ddlGeneric.SelectedIndex = 0;
                ddlSchedule.SelectedIndex = 0;
                divReportMethod.Visible = false;

                hfINVSubModule.Value = "STK";
                lblHeadText.Text = "Stock ItemList";
            }
            if (tvSettings.SelectedValue == "STKILE")
            {
                blInventoryReport.SubModule = "STKILE";
                divcatgry.Visible = true;
                divDate.Visible = true;
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
                //divSupplier.Visible = true;
                divSupplierpopup.Visible = true;

                divBin.Visible = true;
                 
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
                divManufacturer.Visible = true;
                divSchedule.Visible = true;
                divGeneric.Visible = true;
                ddlManufacturer.SelectedIndex = 0;
                ddlGeneric.SelectedIndex = 0;
                ddlSchedule.SelectedIndex = 0;
                divReportMethod.Visible = false;
                hfINVSubModule.Value = "STK";
                lblHeadText.Text = "Expiry List";
            }
            if (tvSettings.SelectedValue == "STKNEG")
            {
                blInventoryReport.SubModule = "STKNEG";
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
                divReportMethod.Visible = false;
                hfINVSubModule.Value = "STK";
                lblHeadText.Text = "Negative Stock";
            }
            if (tvSettings.SelectedValue == "STKIM")
            {
                blInventoryReport.SubModule = "STKIM";
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
                divReportMethod.Visible = false;
                hfINVSubModule.Value = "STK";
                lblHeadText.Text = "Item Movement";
            }

            if (tvSettings.SelectedValue == "STKDL")
            {
                blInventoryReport.SubModule = "STKDL";
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
                divReportMethod.Visible = false;
                hfINVSubModule.Value = "STK";
                lblHeadText.Text = "Stock Damage List";
            }


            if (tvSettings.SelectedValue == "OPSTK")
            {
                blInventoryReport.SubModule = "OPSTK";
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
                divReportMethod.Visible = false;
                hfINVSubModule.Value = "STK";
                lblHeadText.Text = "Opening Stock List";
            }
            if (tvSettings.SelectedValue == "STKTR")
            {
                blInventoryReport.SubModule = "STKTR";
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
                divReportMethod.Visible = false;
                hfINVSubModule.Value = "STK";
                lblHeadText.Text = "Stock Transfer";
            }
            if (tvSettings.SelectedValue == "STKVF")
            {
                blInventoryReport.SubModule = "STKVF";
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
                divReportMethod.Visible = false;
                hfINVSubModule.Value = "STK";
                lblHeadText.Text = "Stock Verification";
                divExpiry.Visible = true;
            }
            if (tvSettings.SelectedValue == "STKITM")
            {
                blInventoryReport.SubModule = "STKITM";
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
                divReportMethod.Visible = false;
                hfINVSubModule.Value = "STK";
                lblHeadText.Text = "Item Wise Stock";
            }


            if (tvSettings.SelectedValue == "STKVL")
            {
                blInventoryReport.SubModule = "STKVL";
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
                divReportMethod.Visible = false;
                hfINVSubModule.Value = "STK";
                lblHeadText.Text = "Stock Values";
            }
            if (tvSettings.SelectedValue == "STKMQ")
            {
                blInventoryReport.SubModule = "STKMQ";
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
                divManufacturer.Visible = true;
                divGeneric.Visible = true;
                divLevel.Visible = true;
                ddlManufacturer.SelectedIndex = 0;
                ddlGeneric.SelectedIndex = 0;
                ddlLevel.SelectedIndex = 0;
                divReportMethod.Visible = false;
                hfINVSubModule.Value = "STK";
                lblHeadText.Text = "Current Stock Items";
            }
            if (tvSettings.SelectedValue == "STKMC")
            {
                blInventoryReport.SubModule = "STKMC";
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
                divReportMethod.Visible = false;
                hfINVSubModule.Value = "STK";
                lblHeadText.Text = "Multiple Stock Correction";
            }
            if (tvSettings.SelectedValue == "GIIR")
            {
                blInventoryReport.SubModule = "GIIR";
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
                divReportMethod.Visible = false;
                lblHeadText.Text = "GoodsIssue Register";
                hfINVSubModule.Value = "GI";
            }
            if (tvSettings.SelectedValue == "GIIW")
            {
                blInventoryReport.SubModule = "GIIW";
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
                divReportMethod.Visible = false;
                hfINVSubModule.Value = "GI";
                lblHeadText.Text = "GoodsIssue Item-Wise";
            }
            if (tvSettings.SelectedValue == "PUREG" || tvSettings.SelectedValue == "PURREGDET")
            {
                if (tvSettings.SelectedValue == "PUREG")
                {
                    blInventoryReport.SubModule = "PUREG";
                    lblHeadText.Text = "Purchase Register";
                }
                else
                {
                    blInventoryReport.SubModule = "PURREGDET";
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
                DivApproved.Visible = true;
                divReportMethod.Visible = false;
                hfINVSubModule.Value = "PU";

            }
            if (tvSettings.SelectedValue == "PUDET")
            {
                divDate.Visible = true;
                divSupplierpopup.Visible = true;
                divStore.Visible = true;
                DivReportType.Visible = true;
                divReportMethod.Visible = false;
                hfINVSubModule.Value = "PU";
                lblHeadText.Text = "Purchase Detailed";
                divButtons.Visible = true;
            }
            if (tvSettings.SelectedValue == "PUIW")
            {
                blInventoryReport.SubModule = "PUIW";
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
                divReportMethod.Visible = false;

                hfINVSubModule.Value = "PU";
                lblHeadText.Text = "Purchase Item-Wise";
            }
            if (tvSettings.SelectedValue == "PUIWSP")
            {
                blInventoryReport.SubModule = "PUIWSP";
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
                divReportMethod.Visible = false;
                hfINVSubModule.Value = "PU";
                lblHeadText.Text = "Purchase Item-Wise Supplier";
            }
            if (tvSettings.SelectedValue == "PUSPIW")
            {
                blInventoryReport.SubModule = "PUSPIW";
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
                divReportMethod.Visible = false;
                hfINVSubModule.Value = "PU";
                lblHeadText.Text = "Supplier Wise Purchase";
            }
            if (tvSettings.SelectedValue == "SUPS")
            {
                blInventoryReport.SubModule = "SUPS";
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
                divReportMethod.Visible = false;
                hfINVSubModule.Value = "SL";
                lblHeadText.Text = "Supplier Wise Sale";
            }
            if (tvSettings.SelectedValue == "SLSMI")
            {
                blInventoryReport.SubModule = "SLSMI";
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
                divReportMethod.Visible = false;
                hfINVSubModule.Value = "SL";
                lblHeadText.Text = "SalesMan Incentive";
            }
            if (tvSettings.SelectedValue == "SLSMIC")
            {
                blInventoryReport.SubModule = "SLSMIC";
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
                divReportMethod.Visible = false;
                hfINVSubModule.Value = "SL";
                lblHeadText.Text = "SalesMan Incentive Consolidate";
            }
            if (tvSettings.SelectedValue == "PRSL")
            {
                blInventoryReport.SubModule = "PRSL";
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
                divReportMethod.Visible = false;
                hfINVSubModule.Value = "PR";
                lblHeadText.Text = "Sales Profitability";
            }
            //if (tvSettings.SelectedValue == "SLPR")
            //{
            //    blInventoryReport.SubModule = "SLPR";
            //    DivReportType.Visible = true;
            //    divcatgry.Visible = false;
            //    divDate.Visible = true;
            //    divAsOnDate.Visible = false;
            //    divStore.Visible = false;
            //    divItemsPopUp.Visible = false;
            //    divStoreFromTo.Visible = false;
            //    divIssueMode.Visible = false;
            //    divButtons.Visible = true;
            //    divuser.Visible = false;
            //    divSaleMode.Visible = false;
            //    divPaymentType.Visible = false;
            //    divEmployee.Visible = false;
            //    divSupplierpopup.Visible = false;
            //    divCustomer.Visible = false;
            //    divCustomer2.Visible = false;
            //    divQuantitySearch.Visible = false;
            //    divNoSales.Visible = false;
            //    divBillNo.Visible = false;
            //    divCounter.Visible = true;
            //    divShift.Visible = false;
            //    divSdDate.Visible = false;
            //    divReportMethod.Visible = false;
            //    hfINVSubModule.Value = "SL";
            //    lblHeadText.Text = "Sales Profitability";
            //}
            if (tvSettings.SelectedValue == "PRCM")
            {
                blInventoryReport.SubModule = "PRCM";
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
                divReportMethod.Visible = false;
                hfINVSubModule.Value = "PR";
                lblHeadText.Text = "Customer Profitability";
            }
            if (tvSettings.SelectedValue == "PRSMA")
            {
                blInventoryReport.SubModule = "PRSMA";
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
                divReportMethod.Visible = false;
                hfINVSubModule.Value = "PR";
                lblHeadText.Text = "SalesMan Profitability Analysis";
            }
            if (tvSettings.SelectedValue == "PURREG")
            {
                blInventoryReport.SubModule = "PURREG";
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
                divReportMethod.Visible = false;
                hfINVSubModule.Value = "PUR";
                lblHeadText.Text = "Purchase return Register";
            }
            if (tvSettings.SelectedValue == "PURIW")
            {
                blInventoryReport.SubModule = "PURIW";
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
                divReportMethod.Visible = false;
                ddlReportType.SelectedIndex = 0;
                hfINVSubModule.Value = "PUR";
                lblHeadText.Text = "Purchase return Item wise";
            }
            if (tvSettings.SelectedValue == "MRIW")
            {
                blInventoryReport.SubModule = "MRIW";
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
                divReportMethod.Visible = false;
                hfINVSubModule.Value = "MR";
                lblHeadText.Text = "Material Receipt Item-Wise";

            }
            if (tvSettings.SelectedValue == "MREXP")
            {
                blInventoryReport.SubModule = "MREXP";
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
                divReportMethod.Visible = false;
                hfINVSubModule.Value = "MR";
                lblHeadText.Text = "Expired Item";

            }
            if (tvSettings.SelectedValue == "SLREG")
            {
                blInventoryReport.SubModule = "SLREG";
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
                divReportMethod.Visible = true;
                hfINVSubModule.Value = "SL";
                lblHeadText.Text = "Sales Register";

            }
            if (tvSettings.SelectedValue == "SLREGDT")
            {
                blInventoryReport.SubModule = "SLREGDT";
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
                divReportMethod.Visible = false;
                hfINVSubModule.Value = "SL";
                lblHeadText.Text = "Sales Register Detailed";

            }
            if (tvSettings.SelectedValue == "SLREGC")
            {
                blInventoryReport.SubModule = "SLREGC";
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
                divReportMethod.Visible = false;
                hfINVSubModule.Value = "SL";
                lblHeadText.Text = "Sales Register (Category Wise)";

            }

            if (tvSettings.SelectedValue == "SLMNTH")
            {
                blInventoryReport.SubModule = "SLMNTH";
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
                divReportMethod.Visible = false;
                hfINVSubModule.Value = "SL";
                lblHeadText.Text = "Monthly Sales";
            }
            if (tvSettings.SelectedValue == "BLREG")
            {
                blInventoryReport.SubModule = "BLREG";
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
                divReportMethod.Visible = false;
                hfINVSubModule.Value = "SL";
                lblHeadText.Text = "Bill Register";
            }
            if (tvSettings.SelectedValue == "CCWR")
            {
                blInventoryReport.SubModule = "CCWR";
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
                divReportMethod.Visible = false;
                hfINVSubModule.Value = "SL";
                lblHeadText.Text = "Counter Close  Report";
            }
            if (tvSettings.SelectedValue == "CWSAL")
            {
                blInventoryReport.SubModule = "CWSAL";
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
                divReportMethod.Visible = false;
                hfINVSubModule.Value = "SL";
                lblHeadText.Text = "Counter Wise Sales";
            }
            if (tvSettings.SelectedValue == "SAWR")
            {
                blInventoryReport.SubModule = "SAWR";
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
                divReportMethod.Visible = false;
                hfINVSubModule.Value = "SL";
                lblHeadText.Text = "Sales Abstract";
            }
            if (tvSettings.SelectedValue == "SLABD")
            {
                blInventoryReport.SubModule = "SLABD";
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
                divReportMethod.Visible = false;
                hfINVSubModule.Value = "SL";
                lblHeadText.Text = "Sales Abstract Date Wise";
            }
            if (tvSettings.SelectedValue == "SLMWREG")
            {
                blInventoryReport.SubModule = "SLMWREG";
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
                divReportMethod.Visible = false;
                hfINVSubModule.Value = "SL";
                lblHeadText.Text = "Sales man Wise Register";
            }
            if (tvSettings.SelectedValue == "SLIW")
            {
                blInventoryReport.SubModule = "SLIW";
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
                divReportMethod.Visible = false;
                ddlUser.SelectedIndex = 0;
                ddlPaymentType.SelectedIndex = 0;
                ddlStore.SelectedIndex = 0;
                ddlCounter.SelectedIndex = 0;
                ddlSaleMode.SelectedIndex = 0;
                hfINVSubModule.Value = "SL";
                lblHeadText.Text = "Sales Item Wise";
            }
            if (tvSettings.SelectedValue == "SLSW")
            {
                blInventoryReport.SubModule = "SLSW";
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
                divSchedule.Visible = true;
                ddlSchedule.SelectedIndex = 0;
                ddlManufacturer.SelectedIndex = 0;
                ddlGeneric.SelectedIndex = 0;
                ddlUser.SelectedIndex = 0;
                ddlPaymentType.SelectedIndex = 0;
                ddlStore.SelectedIndex = 0;
                ddlCounter.SelectedIndex = 0;
                ddlSaleMode.SelectedIndex = 0;
                divReportMethod.Visible = false;
                hfINVSubModule.Value = "SL";
                lblHeadText.Text = "Sales Schedule Wise";
            }
            if (tvSettings.SelectedValue == "SLSM")
            {
                blInventoryReport.SubModule = "SLSM";
                divcatgry.Visible = false;
                divDate.Visible = true;
                divStore.Visible = false;
                //  divItems.Visible = true;
                divItemsPopUp.Visible = true;
                divDoctor.Visible = true;
                divButtons.Visible = true;
                divStoreFromTo.Visible = false;
                divAsOnDate.Visible = false;
                divIssueMode.Visible = false;
                divuser.Visible = false;
                divEmployee.Visible = false;
                // divSupplier.Visible = false;
                divSupplierpopup.Visible = false;
                divCustomer.Visible = true;
                divCustomer2.Visible = false;
                divQuantitySearch.Visible = false;
                divNoSales.Visible = false;
                divBillNo.Visible = false;
                divCounter.Visible = false;
                divShift.Visible = false;
                divuser.Visible = false;
                divSaleMode.Visible = false;
                divPaymentType.Visible = false;
                divSdDate.Visible = false;
                DivReportType.Visible = true;
                divSchedule.Visible = true;
                divReportMethod.Visible = false;
                ddlSchedule.SelectedIndex = 0;
                ddlManufacturer.SelectedIndex = 0;
                ddlGeneric.SelectedIndex = 0;
                ddlUser.SelectedIndex = 0;
                ddlPaymentType.SelectedIndex = 0;
                ddlStore.SelectedIndex = 0;
                ddlCounter.SelectedIndex = 0;
                ddlSaleMode.SelectedIndex = 0;
                hfINVSubModule.Value = "SL";
                lblHeadText.Text = "Schedule Item  Wise";
            }
            if (tvSettings.SelectedValue == "SLMW")
            {
                blInventoryReport.SubModule = "SLMW";
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
                divManufacturer.Visible = true;
                divReportMethod.Visible = false;
                ddlSchedule.SelectedIndex = 0;
                ddlManufacturer.SelectedIndex = 0;
                ddlGeneric.SelectedIndex = 0;
                ddlUser.SelectedIndex = 0;
                ddlPaymentType.SelectedIndex = 0;
                ddlStore.SelectedIndex = 0;
                ddlCounter.SelectedIndex = 0;
                ddlSaleMode.SelectedIndex = 0;
                ddlReportMethod.SelectedIndex = 0;
                hfINVSubModule.Value = "SL";
                lblHeadText.Text = "Sales Manufacture Wise";
            }
            if (tvSettings.SelectedValue == "SLGW")
            {
                blInventoryReport.SubModule = "SLGW";
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
                divGeneric.Visible = true;
                ddlSchedule.SelectedIndex = 0;
                divReportMethod.Visible = false;
                ddlManufacturer.SelectedIndex = 0;
                ddlGeneric.SelectedIndex = 0;
                ddlUser.SelectedIndex = 0;
                ddlPaymentType.SelectedIndex = 0;
                ddlStore.SelectedIndex = 0;
                ddlCounter.SelectedIndex = 0;
                ddlSaleMode.SelectedIndex = 0;
                hfINVSubModule.Value = "SL";
                lblHeadText.Text = "Sales Generic Wise";
            }
            if (tvSettings.SelectedValue == "SLSTREG")
            {
                blInventoryReport.SubModule = "SLSTREG";
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
                divReportMethod.Visible = false;
                hfINVSubModule.Value = "SL";
                lblHeadText.Text = "Sales Store Wise Register";
            }
            if (tvSettings.SelectedValue == "SLSTDET")
            {
                blInventoryReport.SubModule = "SLSTDET";
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
                divReportMethod.Visible = false;
                hfINVSubModule.Value = "SL";
                lblHeadText.Text = "Sales Store Wise Detail";
            }

            if (tvSettings.SelectedValue == "SLUSREG")
            {
                blInventoryReport.SubModule = "SLUSREG";
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
                divReportMethod.Visible = false;
                hfINVSubModule.Value = "SL";
                lblHeadText.Text = "Sales User Wise Register";
            }

            if (tvSettings.SelectedValue == "SLRREG")
            {
                blInventoryReport.SubModule = "SLRREG";
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
                divReportMethod.Visible = false;
                hfINVSubModule.Value = "SLR";
                lblHeadText.Text = "Sales Return Register";
            }
            if (tvSettings.SelectedValue == "SLRBLREG")
            {
                blInventoryReport.SubModule = "SLRBLREG";
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
                divPaymentType.Visible = true;
                divSdDate.Visible = false;
                divShift.Visible = false;
                divReportMethod.Visible = false;
                ddlUser.SelectedIndex = 0;
                ddlPaymentType.SelectedIndex = 0;
                ddlStore.SelectedIndex = 0;
                ddlCounter.SelectedIndex = 0;
                ddlSaleMode.SelectedIndex = 0;
                ddlShift.SelectedIndex = 0;
                DivReportType.Visible = true;
                ddlReportType.SelectedIndex = 0;
                hfINVSubModule.Value = "SLR";
                lblHeadText.Text = "Bill Register";
            }
            if (tvSettings.SelectedValue == "SLRREGC")
            {
                blInventoryReport.SubModule = "SLRREGC";
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
                divReportMethod.Visible = false;
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
                blInventoryReport.SubModule = "SLCS";
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
                divReportMethod.Visible = false;
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
            if (tvSettings.SelectedValue == "SLDS")
            {
                blInventoryReport.SubModule = "SLDS";
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
                divDoctor.Visible = true;
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
                divReportMethod.Visible = false;
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
                lblHeadText.Text = "Sales Doctor Wise";
            }
            if (tvSettings.SelectedValue == "SLCSST")
            {
                blInventoryReport.SubModule = "SLCSST";
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
                divReportMethod.Visible = false;
                hfINVSubModule.Value = "SL";
                lblHeadText.Text = "Sales Customer Statement Report";
            }
            if (tvSettings.SelectedValue == "SLNPCS")
            {
                blInventoryReport.SubModule = "SLNPCS";
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
                divReportMethod.Visible = false;
                hfINVSubModule.Value = "SL";
                lblHeadText.Text = "Non Performing Customer Statement Report";
            }
            if (tvSettings.SelectedValue == "SLRIW")
            {
                blInventoryReport.SubModule = "SLRIW";
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
                divReportMethod.Visible = false;
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
                blInventoryReport.SubModule = "SLIM";
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
                divReportMethod.Visible = false;
                hfINVSubModule.Value = "SL";
                lblHeadText.Text = "Item Movement";
            }
            if (tvSettings.SelectedValue == "SLVR")
            {
                blInventoryReport.SubModule = "SLVR";
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
                divReportMethod.Visible = false;
                hfINVSubModule.Value = "SL";
                lblHeadText.Text = "Voucher report";
            }
            if (tvSettings.SelectedValue == "SHCL")
            {
                blInventoryReport.SubModule = "SHCL";
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
                divReportMethod.Visible = false;
                hfINVSubModule.Value = "SHICL";
                lblHeadText.Text = "Shift Close";
            }
            if (tvSettings.SelectedValue == "VATPUR")
            {
                blInventoryReport.SubModule = "VATPUR";
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
                divReportMethod.Visible = false;
                ddlReportType.SelectedIndex = 0;
                hfINVSubModule.Value = "VAT";
                lblHeadText.Text = "Purchase wise GST";
            }
            if (tvSettings.SelectedValue == "VATSAL")
            {
                blInventoryReport.SubModule = "VATSAL";
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
                divReportMethod.Visible = false;
                ddlReportType.SelectedIndex = 0;
                hfINVSubModule.Value = "VAT";
                lblHeadText.Text = "Sales wise GST";
            }
            if (tvSettings.SelectedValue == "GSTSTS")
            {
                blInventoryReport.SubModule = "GSTSTS";
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
                divReportMethod.Visible = false;
                ddlReportType.SelectedIndex = 0;
                hfINVSubModule.Value = "VAT";
                lblHeadText.Text = "Sales GST Summary";
            }
            if (tvSettings.SelectedValue == "GSTS")
            {
                blInventoryReport.SubModule = "GSTS";
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
                divReportMethod.Visible = false;
                ddlReportType.SelectedIndex = 0;
                hfINVSubModule.Value = "VAT";
                lblHeadText.Text = "Sales GST";

            }
            if (tvSettings.SelectedValue == "GSTHSN")
            {
                blInventoryReport.SubModule = "GSTHSN";
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
                divReportMethod.Visible = false;
                ddlReportType.SelectedIndex = 0;
                hfINVSubModule.Value = "VAT";
                lblHeadText.Text = "HSN Code Report";
            }

            if (tvSettings.SelectedValue == "GSTHSN1")
            {
                blInventoryReport.SubModule = "GSTHSN1";
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
                blInventoryReport.SubModule = "VATOP";
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
                divReportMethod.Visible = false;
                ddlReportType.SelectedIndex = 0;
                hfINVSubModule.Value = "VAT";
                lblHeadText.Text = "Output GST";
            }
            if (tvSettings.SelectedValue == "VATIP")
            {
                blInventoryReport.SubModule = "VATIP";
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
                divReportMethod.Visible = false;
                ddlReportType.SelectedIndex = 0;
                hfINVSubModule.Value = "VAT";
                lblHeadText.Text = "Input GST";
            }
            if (tvSettings.SelectedValue == "VATPRR")
            {
                blInventoryReport.SubModule = "VATPRR";
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
                divReportMethod.Visible = false;
                ddlReportType.SelectedIndex = 0;
                hfINVSubModule.Value = "VAT";
                lblHeadText.Text = "Purchase Returnwise GST";
            }
            if (tvSettings.SelectedValue == "PRG")
            {
                blInventoryReport.SubModule = "PRG";
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
                divReportMethod.Visible = false;
                ddlReportType.SelectedIndex = 0;
                hfINVSubModule.Value = "VAT";
                lblHeadText.Text = "Purchase Returnwise GST";
            }
            if (tvSettings.SelectedValue == "VATSLR")
            {
                blInventoryReport.SubModule = "VATSLR";
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
                divReportMethod.Visible = false;
                ddlReportType.SelectedIndex = 0;
                hfINVSubModule.Value = "VAT";
                lblHeadText.Text = "Sales Returnwise GST";
            }
            if (tvSettings.SelectedValue == "TFRREG")
            {

                blInventoryReport.SubModule = "TFRREG";
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
                divReportMethod.Visible = false;
                hfINVSubModule.Value = "TFR";
                lblHeadText.Text = "Stock Transfer";
            }
            if (tvSettings.SelectedValue == "TFRODR")
            {
                DivReportType.Visible = true;
                ddlReportType.SelectedIndex = 0;
                blInventoryReport.SubModule = "TFRODR";
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
                divReportMethod.Visible = false;
                hfINVSubModule.Value = "TFR";
                lblHeadText.Text = "Transfer Order";
            }
            if (tvSettings.SelectedValue == "TFRPRC")
            {
                blInventoryReport.SubModule = "TFRPRC";
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
                divReportMethod.Visible = false;
                hfINVSubModule.Value = "TFR";
                lblHeadText.Text = "Price Conversion";
            }
            if (tvSettings.SelectedValue == "TFRITC")
            {
                blInventoryReport.SubModule = "TFRITC";
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
                divReportMethod.Visible = false;
                hfINVSubModule.Value = "TFR";
                lblHeadText.Text = "Item Conversion";
            }
            if (tvSettings.SelectedValue == "SALLIA")
            {
                blInventoryReport.SubModule = "SALLIA";
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
                divReportMethod.Visible = false;
                hfINVSubModule.Value = "LIA";
                lblHeadText.Text = "Liability Register";
            }
            if (tvSettings.SelectedValue == "SALLIAP")
            {
                blInventoryReport.SubModule = "SALLIAP";
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
                divReportMethod.Visible = false;
                hfINVSubModule.Value = "LIA";
                lblHeadText.Text = "Salesman Liability Register PeriodWise";
            }
            if (tvSettings.SelectedValue == "SDPR")
            {
                blInventoryReport.SubModule = "SDPR";
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
                divReportMethod.Visible = false;
                ddlReportType.SelectedIndex = 0;
                hfINVSubModule.Value = "SD";
                lblHeadText.Text = "Schedule Processed List";
            }
            if (tvSettings.SelectedValue == "SDPO")
            {
                blInventoryReport.SubModule = "SDPO";
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
                divReportMethod.Visible = false;
                ddlReportType.SelectedIndex = 0;
                hfINVSubModule.Value = "SD";
                lblHeadText.Text = "Schedule Posted List";
            }
            if (tvSettings.SelectedValue == "SDDF")
            {
                blInventoryReport.SubModule = "SDDF";
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
                divReportMethod.Visible = false;
                ddlReportType.SelectedIndex = 0;
                hfINVSubModule.Value = "SD";
                lblHeadText.Text = "Schedule Difference List";
            }
            if (tvSettings.SelectedValue == "POSTW")
            {
                blInventoryReport.SubModule = "POSTW";
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
                divReportMethod.Visible = false;
                ddlReportType.SelectedIndex = 0;
                ddlQuantityCriteria.Enabled = false;
                ddlQuantityCriteria.SelectedIndex = 2;
                hfINVSubModule.Value = "PO";
                lblHeadText.Text = "Purchase Order Report";
            }
            if (tvSettings.SelectedValue == "POSTORE")
            {
                blInventoryReport.SubModule = "POSTW";
                divcatgry.Visible = false;
                divDate.Visible = true;
                divStore.Visible = true;
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
                divQuantitySearch.Visible = false;
                divBillNo.Visible = false;
                divCounter.Visible = false;
                divShift.Visible = false;
                divSdDate.Visible = false;
                DivReportType.Visible = true;
                divReportMethod.Visible = false;
                ddlReportType.SelectedIndex = 0;
                ddlQuantityCriteria.Enabled = false;
                ddlQuantityCriteria.SelectedIndex = 2;
                hfINVSubModule.Value = "PO";
                lblHeadText.Text = "Store Wise Purchase Order Report";
            }
            if (tvSettings.SelectedValue == "GENSUPP")
            {
                blInventoryReport.SubModule = "GENSUPP";
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
                divReportMethod.Visible = false;
                ddlReportType.SelectedIndex = 0;
                ddlQuantityCriteria.Enabled = false;
                hfINVSubModule.Value = "GEN";
                lblHeadText.Text = "Supplier List Report";
            }
            if (tvSettings.SelectedValue == "SLRCS")
            {
                blInventoryReport.SubModule = "SLRCS";
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
                divSaleMode.Visible = false;
                divPaymentType.Visible = true;
                divSdDate.Visible = false;
                divReportMethod.Visible = false;
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
                blInventoryReport.SubModule = "GSTSIB";
                divButtons.Visible = true;
                divDate.Visible = true;
                divReportMethod.Visible = false;
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
                blInventoryReport.SubModule = "GSTPIB";
                divButtons.Visible = true;
                divDate.Visible = true;
                divReportMethod.Visible = false;
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
            else if (tvSettings.SelectedValue == "SALESCON")
            {
                blInventoryReport.SubModule = "SALESCON";
                divDate.Visible = true;
                divStore.Visible = true;
                divButtons.Visible = true;
                DivReportType.Visible = true;
                ddlReportType.SelectedIndex = 0;
                hfINVSubModule.Value = "VAT";
                lblHeadText.Text = "Sales Register Consolidated";
            }
            else if (tvSettings.SelectedValue == "GSTSALES")
            {
                blInventoryReport.SubModule = "GSTSALES";
                divButtons.Visible = true;
                divDate.Visible = true;
                divStore.Visible = true;
                divReportMethod.Visible = false;
                ddlUser.SelectedIndex = 0;
                ddlPaymentType.SelectedIndex = 0;
                ddlStore.SelectedIndex = 0;
                ddlCounter.SelectedIndex = 0;
                ddlSaleMode.SelectedIndex = 0;
                ddlReportType.SelectedIndex = 0;
                DivReportType.Visible = true;
                CustomerClear();
                ClearItem();
                ClearSupplier();
                hfINVSubModule.Value = "VAT";
                lblHeadText.Text = "Sales GST Report";
            }
            else if (tvSettings.SelectedValue == "SUPSI")
            {
                blInventoryReport.SubModule = "SUPSI";
                divButtons.Visible = true;
                divDate.Visible = true;
                divSupplierpopup.Visible = true;
                divReportMethod.Visible = false;
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
                blInventoryReport.SubModule = "SUPPI";
                divButtons.Visible = true;
                divDate.Visible = true;
                divSupplierpopup.Visible = true;
                divReportMethod.Visible = false;
                DivReportType.Visible = true;
                ddlReportType.SelectedIndex = 0;

                CustomerClear();
                ClearItem();
                ClearSupplier();

                hfINVSubModule.Value = "PU";
                lblHeadText.Text = "Supplier Wise Purchase Items";
            }
            else if (tvSettings.SelectedValue == "DLYSLSMRY")
            {
                blInventoryReport.SubModule = "DLYSLSMRY";
                divButtons.Visible = true;
                divDate.Visible = true;
                divStore.Visible = true;
                divReportMethod.Visible = false;
                ddlStore.SelectedIndex = 0;
                ddlReportType.SelectedIndex = 0;
                DivReportType.Visible = true;
                hfINVSubModule.Value = "SL";
                lblHeadText.Text = "Daily Sales Summary";
            }
            else if (tvSettings.SelectedValue == "SLPR")
            {
                blInventoryReport.SubModule = "SLPR";
                divButtons.Visible = true;
                divDate.Visible = true;
                divStore.Visible = true;
                divReportMethod.Visible = false;
                ddlStore.SelectedIndex = 0;
                ddlReportType.SelectedIndex = 0;
                DivReportType.Visible = true;
                hfINVSubModule.Value = "SL";
                lblHeadText.Text = "Sales Profitability";
            }
            else if (tvSettings.SelectedValue == "SLREGH")
            {
                blInventoryReport.SubModule = "SLREGH";
                divcatgry.Visible = true;
                divStore.Visible = true;
                divCounter.Visible = true;
                divuser.Visible = true;
                divSaleMode.Visible = true;
                divReportMethod.Visible = false;
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


            if (tvSettings.SelectedValue == "GENCCR" || tvSettings.SelectedValue == "GENCCRD")
            {
                blInventoryReport.SubModule = "GENCCR";
                DivReportType.Visible = true;
                divcatgry.Visible = false;
                divDate.Visible = true;
                divStore.Visible = false;
                divItemsPopUp.Visible = false;
                divStoreFromTo.Visible = false;
                divButtons.Visible = true;
                divAsOnDate.Visible = false;
                txtAsOnDate.Enabled = false;
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
                divReportMethod.Visible = false;
                hfINVSubModule.Value = "GEN";
                if (tvSettings.SelectedValue == "GENCCR")
                {
                    lblHeadText.Text = "Customer Card Issue List";
                }
                else
                {
                    lblHeadText.Text = "Customer Card Deactive List";
                }
            }
            if (tvSettings.SelectedValue == "INR")
            {
                blInventoryReport.SubModule = "INR";
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
                divReportMethod.Visible = false;
                ddlReportType.SelectedIndex = 0;
                hfINVSubModule.Value = "IN";
                lblHeadText.Text = "Indent Request";
            }
            if (tvSettings.SelectedValue == "IND")
            {
                blInventoryReport.SubModule = "IND";
                divcatgry.Visible = false;
                divDate.Visible = true;
                divAsOnDate.Visible = false;
                // divStore.Visible = true;
                divItemsPopUp.Visible = false;
                divStoreFromTo.Visible = true;
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
                divReportMethod.Visible = false;
                ddlReportType.SelectedIndex = 0;
                hfINVSubModule.Value = "IN";
                lblHeadText.Text = "Indent";
            }
            if (tvSettings.SelectedValue == "GSTSRHSN1")
            {
                blInventoryReport.SubModule = "GSTSRHSN1";
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
                divReportMethod.Visible = false;
                ddlReportType.SelectedIndex = 0;
                hfINVSubModule.Value = "VAT";
                lblHeadText.Text = "Sales Return HSN Code Report(Format-1)";
                lblHeadText.ToolTip = "This report is used for uploading purpose.";
            }
            if (tvSettings.SelectedValue == "VATDTSL")
            {
                blInventoryReport.SubModule = "VATDTSL";
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
                divReportMethod.Visible = false;
                ddlReportType.SelectedIndex = 0;
                hfINVSubModule.Value = "VAT";
                lblHeadText.Text = "Sales Date Wise GST";
            }
            if (tvSettings.SelectedValue == "GSTHSNC")
            {
                blInventoryReport.SubModule = "GSTHSNC";
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
                divReportMethod.Visible = false;
                ddlReportType.SelectedIndex = 0;
                hfINVSubModule.Value = "VAT";
                lblHeadText.Text = "HSN Code Report Consolidated";
                lblHeadText.ToolTip = "This report is used for uploading purpose.";
            }
            if (tvSettings.SelectedValue == "STKREGCN")
            {
                blInventoryReport.SubModule = "STKREGCN";
                divcatgry.Visible = true;
                divDate.Visible = false;
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
                divReportMethod.Visible = false;
                ddlReportType.SelectedIndex = 0;
                hfINVSubModule.Value = "STK";
                lblHeadText.Text = "Stock Register Consolidated";

            }
            if (tvSettings.SelectedValue == "STKCSDR")
            {
                blInventoryReport.SubModule = "STKCSDR";
                divcatgry.Visible = false;
                divDate.Visible = true;
                divStore.Visible = true;
                divItemsPopUp.Visible = false;
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
                divQuantitySearch.Visible = false;
                divNoSales.Visible = false;
                divBillNo.Visible = false;
                divCounter.Visible = false;
                divShift.Visible = false;
                DivReportType.Visible = true;
                divSdDate.Visible = false;
                divReportMethod.Visible = false;
                ddlReportType.SelectedIndex = 0;
                hfINVSubModule.Value = "STK";
                lblHeadText.Text = "Daily Consolidate Transaction";

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

                    BlReportData.UserName = Convert.ToString(Session["Username"]);
                    BlReportData.reportname = BlReportData.ReportFile;

                    ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "popUp();", true);
                    long AuditTrailUpdation = BlReportData.AuditTrailUpdation();
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
            ((HiddenField)CommonPopup1.FindControl("hfColumn")).Value = "CustomerName";
            CustomerFill(1);
            divAdd.Style.Add("display", "block");
            // divView.Style.Add("display", "none");


        }
        catch (Exception ex)
        {
        }
    }
    protected void imgbtnDoctor_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            ((TextBox)CommonPopup1.FindControl("txtName")).Text = txtCustomer.Text.Trim();
            // txtCustomer.Text = "";
            ((TextBox)CommonPopup1.FindControl("txtRefno")).Text = txtCustomer.Text.Trim();
            ((HiddenField)CommonPopup1.FindControl("hfColumn")).Value = "DoctorName";
            DoctorFill(1);
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
        if (tvSettings.SelectedValue == "GSTSIB")
        {
            ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "ShowDate('');", true);
        }
        else
            txtFromDate.Text = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1).ToString("dd/MM/yyyy");
        txtToDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
        ddlReportType.SelectedIndex = 0;
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
        populate.FillDropdownListAll(ddlCounter, "Counters", "CounterName", "ID_Counters", "CounterName", "Active='True' and Cancelled=0 AND FK_Client=" + hfClient.Value, "");
        populate.FillDropdownListAll(ddlShift, "ShiftMaster", "ShiftName", "ID_ShiftMaster", "ShiftName", " Active='True' AND cancelled=0 and FK_Client=" + hfClient.Value, "");
        populate.FillGoodsIssueMode(ddlIssueMode);
        populate = null;
        // ddlItems.SelectedIndex = 0;    
        txtItemRefno.Text = "";
        textItemName.Text = "";
        txtSupplierName.Text = "";
        TxtSupplierRefNo.Text = "";
        ddlBin.SelectedValue = "0"; chkWithoutExpiry.Checked = false;
        if (tvSettings.SelectedValue == "STKIL")
        {
            ddlCategoryFirst.SelectedIndex = 0;
            //ddlStore.SelectedIndex = 0;
            ddlManufacturer.SelectedIndex = 0;
            ddlGeneric.SelectedIndex = 0;
            ddlSchedule.SelectedIndex = 0;
        }
        if (tvSettings.SelectedValue == "STKILE")
        {
            ddlCategoryFirst.SelectedIndex = 0;
            //ddlStore.SelectedIndex = 0;
            ddlManufacturer.SelectedIndex = 0;
            ddlGeneric.SelectedIndex = 0;
            ddlSchedule.SelectedIndex = 0;
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
            ddlLevel.SelectedIndex = 0;
            ddlStore.SelectedIndex = 0;
            ddlManufacturer.SelectedIndex = 0;
            ddlGeneric.SelectedIndex = 0;
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
        else if (tvSettings.SelectedValue == "STKITM")
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
            ddlManufacturer.SelectedIndex = 0;
            ddlGeneric.SelectedIndex = 0;
            ddlSchedule.SelectedIndex = 0;
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
        else if (tvSettings.SelectedValue == "SLMW")
        {
            ddlCategoryFirst.SelectedIndex = 0;
            ddlManufacturer.SelectedIndex = 0;
            //  ddlItems.SelectedIndex = 0;
            ddlUser.SelectedIndex = 0;
            ddlSaleMode.SelectedIndex = 0;
            ClearItem();
        }
        else if (tvSettings.SelectedValue == "SLGW")
        {
            ddlCategoryFirst.SelectedIndex = 0;
            ddlGeneric.SelectedIndex = 0;
            //  ddlItems.SelectedIndex = 0;
            ddlUser.SelectedIndex = 0;
            ddlSaleMode.SelectedIndex = 0;
            ClearItem();
        }
        else if (tvSettings.SelectedValue == "SLSW")
        {
            ddlCategoryFirst.SelectedIndex = 0;
            ddlSchedule.SelectedIndex = 0;
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
        else if (tvSettings.SelectedValue == "SLDS")
        {
            //// ddlCustomer.SelectedIndex = 0;
            DoctorClear();
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
        else if (tvSettings.SelectedValue == "SLRBLREG")
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
        else if (tvSettings.SelectedValue == "SALESCON")
        {
            //// ddlCustomer.SelectedIndex = 0;
            ddlStore.SelectedIndex = 0;
            ddlReportType.SelectedIndex = 0;

            txtToDate.Text = DateTime.Now.ToString("dd/MM/yyyy");


        }
        else if (tvSettings.SelectedValue == "GSTSALES")
        {
            //// ddlCustomer.SelectedIndex = 0;
            ddlStore.SelectedIndex = 0;
            ddlReportType.SelectedIndex = 0;

            txtToDate.Text = DateTime.Now.ToString("dd/MM/yyyy");


        }

    }
    protected void ddlStore_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            Populate populate = new Populate();
            if (ddlStore.SelectedIndex > 0)
            {
                populate.FillDropdownList(ddlCounter, "Counters", "CounterName", "ID_Counters", "CounterName", "Active='True' and Cancelled=0 AND FK_Client=" + hfClient.Value + "  AND FK_Store=" + ddlStore.SelectedValue, "");
            }
            else
            {
                populate.FillDropdownListAll(ddlCounter, "Counters", "CounterName", "ID_Counters", "CounterName", "Active='True' and Cancelled=0 AND FK_Client=" + hfClient.Value, "");
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
                populate.FillDropdownListAll(ddlShift, "ShiftMaster", "ShiftName", "ID_ShiftMaster", "ShiftName", " Active='True' AND Cancelled=0 AND FK_Client=" + hfClient.Value + " and FK_Couter=" + ddlCounter.SelectedValue, "");
            }
            else
            {
                populate.FillDropdownListAll(ddlShift, "ShiftMaster", "ShiftName", "ID_ShiftMaster", "ShiftName", " Active='True' AND Cancelled=0 AND FK_Client=" + hfClient.Value, "");
            }
        }
        catch (Exception ex)
        { }
    }
    #endregion
    protected void tvSettings_TreeNodeDataBound(object sender, TreeNodeEventArgs e)
    {
        e.Node.SelectAction = TreeNodeSelectAction.SelectExpand;
    }
}