/*----------------------------------------------------------------------------------------------------------------------------------------------------------
Purpose    : Insert/Update/Delete Storage 
Author     : bibin tc
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
using System.IO;
using Excel;
using WebPharma.Business;


public partial class MaterialReciepts : System.Web.UI.Page
{
    decimal TotValue, TotalTaxPercentage, TotalTaxSales, TotalTaxPerCS;
    decimal TotalAdd, TotalLess, TaxAmount;
    long MasterID, TransactionID;
    int groupName;
    decimal TotalPercentage;
    bool Sts;
    decimal v, d, d1;
    decimal NetEx;
    private const string Excel = "Excel";
    private const string ExcelData = "ExcelData";
    public static DataSet dsExcel;
    public static string ExcelPath;
    public void Delete()
    {
        try
        {
            divPopupReason.Visible = true;
            Populate Populate = new Populate();
            CommonPopupDelete1.CallingFunction = "PopupOnReason";
            ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "PopupOnReason();", true);
            mpDelete.Show();
        }
        catch (Exception ex)
        {
        }
    }

    //public void DeleteMethod(string Reason = "0")
    //{
    //    try
    //    {
    //        //
    //        string confirmValue = Request.Form["confirm_value"];
    //        if (confirmValue.Length > 3)
    //            confirmValue = confirmValue.Substring(confirmValue.Length - 3, 3);
    //        if (confirmValue == "Yes")
    //        {
    //            long status = 0;
    //            BlPurchase blpurchase = new BlPurchase();
    //            DataTable dtbl = new DataTable();
    //            blpurchase.MasterId = Convert.ToInt64(hfMasterID.Value);
    //            blpurchase.FK_Client = Convert.ToInt64(hfClient.Value);
    //            blpurchase.Approved = 2;
    //            dtbl = blpurchase.SelectAllDataForPurchaseApproveEditView();
    //            blpurchase.FK_Client = Convert.ToInt64(hfClient.Value);
    //            blpurchase.FK_Reason = Convert.ToInt64(Reason);
    //            blpurchase.UserCode = hfuserName.Value;
    //            blpurchase.CancelledOn = DateTime.Now;
    //            if (hfIsDelete.Value.ToLower() == "true")
    //            {


    //                BlMaterialReciept blMaterialReciept = new BlMaterialReciept();


    //                blMaterialReciept.FK_Supplier = Convert.ToInt64(dtbl.Rows[0]["FK_Supplier"]);
    //                blMaterialReciept.FK_PurchaseOrder = Convert.ToInt64(dtbl.Rows[0]["FK_Supplier"]);
    //                blMaterialReciept.InvoiceNo = Convert.ToString(dtbl.Rows[0]["InvoiceNo"]);
    //                blMaterialReciept.InvoiceDate = Convert.ToDateTime(dtbl.Rows[0]["InvoiceDate"]);
    //                blMaterialReciept.FK_Reason = Convert.ToInt64(Reason);
    //                blMaterialReciept.FK_Store = Convert.ToInt64(dtbl.Rows[0]["FK_Store"]);
    //                blMaterialReciept.FK_Category = Convert.ToInt64(0);
    //                blMaterialReciept.Fright = Convert.ToDecimal(0);
    //                blMaterialReciept.RoundOff = Convert.ToDecimal(0);

    //                blMaterialReciept.EnteredOn = Convert.ToDateTime(DateTime.Now);
    //                blMaterialReciept.FK_Client = Convert.ToInt64(hfClient.Value);
    //                blMaterialReciept.UserCode = hfuserName.Value;
    //                if (blpurchase.MasterId.ToString() != "")
    //                {
    //                    blMaterialReciept.MasterId = Convert.ToInt64(blpurchase.MasterId);
    //                }



    //                status = blMaterialReciept.UpdateData();
    //                if (status > 0)
    //                {

    //                    ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "toastr.success('Data Deleted successfully')", true);
    //                    GridFillPurchase(1);


    //                }
    //                else if (status == -1)
    //                {

    //                    ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "toastr.error('Cannot Delete data.. ')", true);

    //                }
    //                else
    //                {

    //                    ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "toastr.error('Cannot Delete data.. ')", true);

    //                }


    //                blMaterialReciept = null;
    //            }
    //            else
    //            {
    //                ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "toastr.warning('User does not have permission to Delete')", true);
    //            }
    //            blpurchase = null;
    //        }
    //        MpeApprove.Hide();

    //    }
    //    catch (Exception ex)
    //    {

    //        ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "toastr.warning('" + ex.Message + "')", true);
    //    }
    //}

    public void DeleteMethod(string Reason = "0")
    {
        try
        {

            string confirmValue = Request.Form["confirm_value"];
            if (confirmValue.Length > 3)
                confirmValue = confirmValue.Substring(confirmValue.Length - 3, 3);
            if (confirmValue == "Yes")
            {
                long status = 0;
                BlPurchase blpurchase = new BlPurchase();
                DataTable dtbl = new DataTable();
                blpurchase.MasterId = Convert.ToInt64(hfMasterID.Value);
                blpurchase.FK_Client = Convert.ToInt64(hfClient.Value);
                blpurchase.UserCode = hfuserName.Value;
                blpurchase.FK_Reason = Convert.ToInt64(Reason == "" ? "0" : Reason);
                if (hfIsDelete.Value.ToLower() == "true")
                {
                    status = blpurchase.DeletePurchaseDetails();

                    if (status > 0)
                    {

                        ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "toastr.success('Data Deleted Successfully')", true);
                        GridFillPurchase(1);


                    }
                    else if (status == -1)
                    {

                        ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "toastr.error('Cannot Delete Data.. ')", true);

                    }
                    else
                    {

                        ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "toastr.error('Cannot Delete Data.. ')", true);

                    }
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "toastr.warning('User does not have permission to Delete')", true);
                }
                blpurchase = null;
            }
            MpeApprove.Hide();

        }
        catch (Exception ex)
        {
            //blMessage.Items.Add(ex.Message);
            ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "toastr.warning('" + ex.Message + "')", true);
        }
    }
    protected void BindData()
    {
        Populate populate = new Populate();

        populate.FillDropdownList(ddlSupplier, "Supplier", "SupplierName", "ID_Supplier", "SupplierName", "Active='True' AND Cancelled=0 AND FK_Client= " + hfClient.Value.Split(',')[0], "");
        populate.FillDropdownList(ddlStore, "Store", "StoName", "ID_Store", "StoName", " Active='True'  AND Cancelled=0 AND FK_Client= " + hfClient.Value.Split(',')[0], "");
        populate.FillDropdownList(ddlSupplierSearch, "Supplier", "SupplierName", "ID_Supplier", "SupplierName", "Active='True' AND Cancelled=0 AND FK_Client= " + hfClient.Value.Split(',')[0], "");
        populate.FillDropdownList(ddlStoreSearch, "Store", "StoName", "ID_Store", "StoName", " Active='True' AND Cancelled=0 AND FK_Client= " + hfClient.Value.Split(',')[0], "");
        populate.FillDropdownList(ddlPurchaseOrder, "PurchaseOrder", "PONo", "ID_PurchaseOrder", "PONo", "  Cancelled='False' AND Closed='False' AND FK_Client=" + hfClient.Value.Split(',')[0], "");
        populate.FillDropdownList(ddlCategory, "Category", "CategoryName", "ID_Category", "CategoryName", "Cancelled=0 and  FK_Client= " + hfClient.Value.Split(',')[0], "");
        populate.FillDropdownList(ddlUnit, "Units", "UnitName", "ID_Units", "UnitName", "Cancelled=0 and FK_Client= " + hfClient.Value.Split(',')[0], "");
        populate.FillDropdownList(ddlSubunit, "SubUnit", "SubUnitName", "ID_SubUnits", "SubUnitName", "Cancelled=0 and FK_Client= " + hfClient.Value.Split(',')[0], "");
        populate.FillDropdownList(ddlpopupbillType, "BillType", "TypeName", "ID_BillType", "ID_BillType", "Cancelled=0 AND BillType=2 AND  FK_Client= " + hfClient.Value.Split(',')[0], "");
        populate.FillDropdownList(ddlAcchead, "AccountHead", "HeadName", "ID_AccountHead", "HeadName", " Cancelled=0  AND FK_Client=" + hfClient.Value.Split(',')[0], "");
        populate.FillPurchasePaymentMode(ddlPaymentMode);
        populate = null;


    }

    protected void CheckUserAccess()
    {
        BlUserAccess bluseraccess = new BlUserAccess();
        Populate populate = new Populate();

        bluseraccess.FK_Users = Convert.ToInt64(hfUser.Value);
        bluseraccess.FK_Pages = 25;
        bluseraccess.FK_Client = Convert.ToInt64(hfClient.Value);
        bluseraccess.CheckUserAccess();
        if (Convert.ToBoolean(bluseraccess.IsView) == false)
        {


        }

        hfIsAdd.Value = (Convert.ToBoolean(bluseraccess.IsAdd)).ToString();
        hfIsUpdate.Value = (Convert.ToBoolean(bluseraccess.IsUpdate)).ToString();
        hfIsDelete.Value = (Convert.ToBoolean(bluseraccess.IsDelete)).ToString();
        hfIsApprove.Value = (Convert.ToBoolean(bluseraccess.IsApprove)).ToString();
        bluseraccess = null;
    }
    protected void SetControls()
    {
        ((TextBox)CommonPopupExpense.FindControl("txtTo")).Attributes.Add("readOnly", "readOnly");
        txtBillNetAmount.Attributes.Add("readOnly", "readOnly");
        txtTotalAmount.Attributes.Add("readOnly", "readOnly");
        txtOther.Attributes.Add("readOnly", "readOnly");
        txtRoundOff.Attributes.Add("readOnly", "readOnly");
        txtpopupRoundOff.Attributes.Add("readOnly", "readOnly");
        txtpopupBalanceAmount.Attributes.Add("readOnly", "readOnly");
        txtpopupNetPayAmount.Attributes.Add("readOnly", "readOnly");
        txtpopupPurchaseAmoount.Attributes.Add("readOnly", "readOnly");
        txtpopupApproveBillNo.Attributes.Add("readOnly", "readOnly");
        txtActqty.Attributes.Add("readOnly", "readOnly");
        txtTotNetPur.Attributes.Add("readOnly", "readOnly");
        txtBatch.Attributes["onfocus"] = "javascript:this.select();";
        txtQuantity.Attributes["onfocus"] = "javascript:this.select();";//nj
        txtProfitPer.Attributes.Add("onfocus", "javascript:this.select();");//nj
        txtProfitAmount.Attributes.Add("onfocus", "javascript:this.select();");//nj
        txtDiscountAmt.Attributes.Add("onfocus", "javascript:this.select();");//nj
        txtSalesPrice.Attributes.Add("onfocus", "javascript:this.select();");//nj
        txtHSNSAC.Attributes.Add("onfocus", "javascript:this.select();");//nj
        txtPurchaseRate.Attributes["onfocus"] = "javascript:this.select();"; //nj
        txtPurDiscAmnt.Attributes["onfocus"] = "javascript:this.select();"; //nj
        txtFree.Attributes["onfocus"] = "javascript:this.select();";//nj
        txtActqty.Attributes["onfocus"] = "javascript:this.select();";//nj
        txtPurDiscPer.Attributes["onfocus"] = "javascript:this.select();";//nj
        txtMRP.Attributes["onfocus"] = "javascript:this.select();";//nj
        txtDiscountPer.Attributes["onfocus"] = "javascript:this.select();";//nj
        txtGSTIN.Attributes.Add("readOnly", "readOnly");//nj
        txtNetPur.Attributes.Add("readOnly", "readOnly"); //nj
        txttaxAmount.Attributes.Add("readOnly", "readOnly"); //nj
        txtProfitAmount.Attributes.Add("readOnly", "readOnly"); //nj
        txtSTaxAmount.Attributes.Add("readOnly", "readOnly"); //nj
    }
    protected void ddlItemsFill()
    {


        BlItems blitems = new BlItems();
        blitems.MasterId = Convert.ToInt64(HfItemsID.Value);
        //  blitems.MasterId = Convert.ToInt64(hfFK_Items.Value);
        //  blitems.MasterId = Convert.ToInt64(ddlItem.SelectedValue.ToString());
        hfItemvalue.Value = Convert.ToString(HfItemsID.Value);
        blitems.FK_Client = Convert.ToInt64(hfClient.Value);
        blitems.SelectData();
        ddlCategory.SelectedValue = blitems.FK_CategoryFirst.ToString();
        ddlUnit.SelectedValue = blitems.FK_Units.ToString();
        txtItemShortName.Text = blitems.Name.ToString();
        hfPacked.Value = blitems.Packed.ToString();
        hfDiscPer.Value = blitems.DiscPer.ToString();
        hfExpirydays.Value = blitems.ExpairyValidation.ToString();
        txtHSNSAC.Text = blitems.HSNCode;
        blitems = null;
        BlUnit blunit = new BlUnit();
        blunit.MasterId = Convert.ToInt64(ddlUnit.SelectedValue);
        blunit.FK_Client = Convert.ToInt64(hfClient.Value);
        blunit.SelectData();
        hfUnitQuantity.Value = blunit.NoofUnits.ToString();
        blunit = null;
        BlPurchase blpurchase = new BlPurchase();
        blpurchase.itemID = Convert.ToInt64(HfItemsID.Value);
        blpurchase.Purchase = true;



        DataTable dtPurchase = blpurchase.SelectGriddata();
        blpurchase.Purchase = false;
        DataTable dt = blpurchase.SelectGriddata();


        DataTable dsw = (DataTable)ViewState["GridDataPurchase"];
        if (dsw != null)
        {
            DataTable ds34 = dsw.Copy();
            dtPurchase.Merge(ds34, true, MissingSchemaAction.Ignore);

        }


        DataTable _datatable = new DataTable();

        _datatable.Columns.Add("ID_items", typeof(int));
        _datatable.Columns.Add("ID_TaxType");
        _datatable.Columns.Add("TaxType");
        _datatable.Columns.Add("TaxPercentage");
        _datatable.Columns.Add("TaxInterState");
        _datatable.Columns.Add("TaxIntraState");

        foreach (DataRow row in dtPurchase.Rows)
        {
            DataRow dr = _datatable.NewRow();

            int ID_items = Convert.ToInt32(row["ID_items"]);
            string FK_TaxType = row["ID_TaxType"].ToString();
            string Percentage = row["TaxPercentage"].ToString();
            string TaxType = row["TaxType"].ToString();
            string TaxInterState = row["TaxInterState"].ToString();
            string TaxIntraState = row["TaxIntraState"].ToString();
            dr["ID_items"] = ID_items;
            dr["ID_TaxType"] = FK_TaxType;
            dr["TaxPercentage"] = Percentage;
            dr["TaxType"] = TaxType;
            dr["TaxInterState"] = TaxInterState;
            dr["TaxIntraState"] = TaxIntraState;
            _datatable.Rows.Add(dr);
            ViewState["GridDataPurchase"] = _datatable;
        }




        foreach (DataRow row in dtPurchase.Rows)
        {
            if (!row.IsNull("GSTType"))
            {
                if (chkInterState.Checked == true && Convert.ToBoolean(row["TaxInterState"].ToString()) == true && Convert.ToInt32(hfItemvalue.Value) == Convert.ToInt32(row["ID_items"].ToString()))
                {
                    TotalTaxPercentage += Convert.ToDecimal(row["TaxPercentage"].ToString());
                }
                if (chkInterState.Checked == false && Convert.ToBoolean(row["TaxIntraState"].ToString()) == true && Convert.ToInt32(hfItemvalue.Value) == Convert.ToInt32(row["ID_items"].ToString()))
                {
                    TotalTaxPercentage += Convert.ToDecimal(row["TaxPercentage"].ToString());
                }

                hfTotTaxPercentage.Value = Convert.ToString(TotalTaxPercentage);
                if (Convert.ToInt32(row["GSTType"].ToString()) == 1 || Convert.ToInt32(row["GSTType"].ToString()) == 2 || Convert.ToInt32(row["GSTType"].ToString()) == 4 || Convert.ToInt32(row["GSTType"].ToString()) == 5)
                {
                    TotalTaxPerCS += Convert.ToDecimal(row["TaxPercentage"].ToString());
                }
                btnS.Text = String.Format("{0:0.##}", TotalTaxPerCS) + "%";
            }
        }
        foreach (DataRow row in dt.Rows)
        {

            if (Convert.ToBoolean(row["TaxIntraState"].ToString()) == true)
            {
                TotalTaxSales += Convert.ToDecimal(row["TaxPercentage"].ToString());
            }


            hfTotTaxsalesPercentage.Value = Convert.ToString(TotalTaxSales);
        }

        getpurchaseamount();

        ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "calculateActualQuantity();", true);
        //  getSalesPrice(0);
        ddlUnit.Focus();
        // LoadPreviousPurchase();

        // }
    }

    protected void LoadPreviousPurchase()
    {

        BlMaterialReciept blmaterialreciept = new BlMaterialReciept();
        blmaterialreciept.FK_Client = Convert.ToInt64(hfClient.Value);

        blmaterialreciept.FK_ItemsforPreviousPurchase = Convert.ToInt64(hfItemvalue.Value.ToString());
        DataTable dtbl = new DataTable();
        // dtbl = blmaterialreciept.SelectPrevPurchaseDetails();

        //if (dtbl.Rows.Count > 0)
        //{
        //    GvPreviousPurchase.DataSource = dtbl;
        //    GvPreviousPurchase.DataBind();
        //    DivPreviousPurchase.Visible = true;
        //    txtBatch.Text = Convert.ToString(dtbl.Rows[0]["BatchNo"].ToString());
        //    if (!chkFree.Checked)
        //    { txtPurchaseRate.Text = Convert.ToString(dtbl.Rows[0]["PurchaseRate"].ToString()); txtProfitPer.Text = Convert.ToString(dtbl.Rows[0]["ProfitPer"].ToString()); ViewState["ActualPurchaseRate"] = Convert.ToString(dtbl.Rows[0]["PurchaseRate"].ToString()); }
        //    else
        //    {
        //        txtPurchaseRate.Text = "0";
        //        txtProfitPer.Text = "0"; ViewState["ActualPurchaseRate"] = "0";
        //    }

        //    txtMRP.Text = Convert.ToString(dtbl.Rows[0]["MRP"].ToString());
        //    //txtPurchaseVat.Text = Convert.ToString(dtbl.Rows[0]["PurchaseVat"].ToString());
        //    // txtVat.Text = Convert.ToString(dtbl.Rows[0]["PurchaseVat"].ToString());

        //    txtRetailPrice.Text = Convert.ToString(dtbl.Rows[0]["RetailPrice"].ToString());
        //    txtWholeSalePrice.Text = Convert.ToString(dtbl.Rows[0]["WholeSalePrice"].ToString());
        //    txtPrivilagePrice.Text = Convert.ToString(dtbl.Rows[0]["CardPrice"].ToString());
        //    txtExpiryDate.Text = Convert.ToString(dtbl.Rows[0]["ExpiryDate"].ToString()).Substring(0, 10);
        //    if (hfDiscSettings.Value == "0")
        //    {
        //        txtDiscountPer.Text = Convert.ToString(dtbl.Rows[0]["DiscountPer"].ToString());
        //        txtDiscountAmt.Text = Convert.ToString(dtbl.Rows[0]["DiscountAmount"].ToString());
        //    }
        //    else
        //    {
        //        txtDiscountPer.Text = hfDiscPer.Value;
        //        txtDiscountAmt.Text = "";
        //    }
        //}
        //else
        //{
        //    if (hfDiscSettings.Value == "1")
        //    {
        //        txtDiscountPer.Text = hfDiscPer.Value;
        //        txtDiscountAmt.Text = "";
        //    }
        //    GvPreviousPurchase.DataSource = dtbl;
        //    GvPreviousPurchase.DataBind();
        //    DivPreviousPurchase.Visible = false;

        //}




        //blmaterialreciept = null;


        //BlItems blitems = new BlItems();
        //DataTable dtblstock = new DataTable();
        //blitems.MasterId = Convert.ToInt64(hfItemValue.Value.ToString());
        //blitems.FK_Client = Convert.ToInt64(hfClient.Value);
        //dtblstock = blitems.ItemsSelectCurrentStock();

        //if (dtblstock.Rows.Count > 0)
        //{
        //    lblCurrentStock.Text = "Current Stock= " + dtblstock.Rows[0]["CurrentStock"].ToString();

        //}
        //else
        //{
        //    lblCurrentStock.Text = "Current Stock= 0";

        //}
        //blitems = null;
    }
    protected void ddlItemsFillEdit()
    {


        BlItems blitems = new BlItems();
        blitems.MasterId = Convert.ToInt64(HfItemsID.Value);
        //  blitems.MasterId = Convert.ToInt64(hfFK_Items.Value);
        //  blitems.MasterId = Convert.ToInt64(ddlItem.SelectedValue.ToString());
        hfItemvalue.Value = Convert.ToString(HfItemsID.Value);
        blitems.FK_Client = Convert.ToInt64(hfClient.Value);
        blitems.SelectData();
        // ddlCategory.SelectedValue = blitems.FK_CategoryFirst.ToString();
        // ddlUnit.SelectedValue = blitems.FK_Units.ToString();
        txtItemShortName.Text = blitems.Name.ToString();
        hfPacked.Value = blitems.Packed.ToString();
        hfDiscPer.Value = blitems.DiscPer.ToString();
        hfExpirydays.Value = blitems.ExpairyValidation.ToString();
        // txtHSNSAC.Text = blitems.HSNCode;
        blitems = null;
        BlUnit blunit = new BlUnit();
        blunit.MasterId = Convert.ToInt64(ddlUnit.SelectedValue);
        blunit.FK_Client = Convert.ToInt64(hfClient.Value);
        blunit.SelectData();
        hfUnitQuantity.Value = blunit.NoofUnits.ToString();
        blunit = null;
        BlPurchase blpurchase = new BlPurchase();
        blpurchase.itemID = Convert.ToInt64(HfItemsID.Value);
        blpurchase.Purchase = true;
        DataTable dtPurchase = blpurchase.SelectGriddata();
        blpurchase.Purchase = false;
        DataTable dt = blpurchase.SelectGriddata();


        //DataTable dsw = (DataTable)ViewState["GridDataPurchase"];
        //if (dsw != null)
        //{
        //    DataTable ds34 = dsw.Copy();
        //    dtPurchase.Merge(ds34, true, MissingSchemaAction.Ignore);

        //}


        DataTable _datatable = new DataTable();

        _datatable.Columns.Add("ID_items", typeof(int));
        _datatable.Columns.Add("ID_TaxType");
        _datatable.Columns.Add("TaxType");
        _datatable.Columns.Add("TaxPercentage");
        _datatable.Columns.Add("TaxInterState");
        _datatable.Columns.Add("TaxIntraState");

        foreach (DataRow row in dtPurchase.Rows)
        {
            DataRow dr = _datatable.NewRow();

            int ID_items = Convert.ToInt32(row["ID_items"]);
            string FK_TaxType = row["ID_TaxType"].ToString();
            string Percentage = row["TaxPercentage"].ToString();
            string TaxType = row["TaxType"].ToString();
            string TaxInterState = row["TaxInterState"].ToString();
            string TaxIntraState = row["TaxIntraState"].ToString();
            dr["ID_items"] = ID_items;
            dr["ID_TaxType"] = FK_TaxType;
            dr["TaxPercentage"] = Percentage;
            dr["TaxType"] = TaxType;
            dr["TaxInterState"] = TaxInterState;
            dr["TaxIntraState"] = TaxIntraState;
            _datatable.Rows.Add(dr);
            ViewState["GridDataPurchase"] = _datatable;
        }




        foreach (DataRow row in dtPurchase.Rows)
        {

            if (chkInterState.Checked == true && Convert.ToBoolean(row["TaxInterState"].ToString()) == true)
            {
                TotalTaxPercentage += Convert.ToDecimal(row["TaxPercentage"].ToString());
            }
            if (chkInterState.Checked == false && Convert.ToBoolean(row["TaxIntraState"].ToString()) == true)
            {
                TotalTaxPercentage += Convert.ToDecimal(row["TaxPercentage"].ToString());
            }

            hfTotTaxPercentage.Value = Convert.ToString(TotalTaxPercentage);
            if (Convert.ToInt32(row["GSTType"].ToString()) == 1 || Convert.ToInt32(row["GSTType"].ToString()) == 2 || Convert.ToInt32(row["GSTType"].ToString()) == 4 || Convert.ToInt32(row["GSTType"].ToString()) == 5)
            {
                TotalTaxPerCS += Convert.ToDecimal(row["TaxPercentage"].ToString());
            }
            btnS.Text = String.Format("{0:0.##}", TotalTaxPerCS) + "%";
        }
        foreach (DataRow row in dt.Rows)
        {

            if (Convert.ToBoolean(row["TaxIntraState"].ToString()) == true)
            {
                TotalTaxSales += Convert.ToDecimal(row["TaxPercentage"].ToString());
            }


            hfTotTaxsalesPercentage.Value = Convert.ToString(TotalTaxSales);
        }

        getpurchaseamount();

        ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "calculateActualQuantity();", true);
        //  getSalesPrice(0);
        ddlUnit.Focus();

        // }
    }
    protected void PreviousData()
    {
        BlMaterialReciept blMaterialReciept = new BlMaterialReciept();
        BlPurchase blpurchase = new BlPurchase();
        blMaterialReciept.FK_Items = Convert.ToInt64(HfItemsID.Value.ToString());
        blMaterialReciept.InterStatePurchase = chkInterState.Checked;
        DataTable dtPrevious = new DataTable();
        dtPrevious = blMaterialReciept.PreviousData();
        if (dtPrevious.Rows.Count > 0)
        {
            //ddlUnit.SelectedValue = dtPrevious.Rows[0]["FK_Units"].ToString();

            // hfUnitQuantity.Value = dtPrevious.Rows[0]["FK_Units"].ToString();
            // ddlSubunit.SelectedValue = dtPrevious.Rows[0]["FK_SubUnit"].ToString();
            txtBatch.Text = dtPrevious.Rows[0]["BatchNo"].ToString();
            txtExpiryDate.Text = dtPrevious.Rows[0]["ExpiryDate"].ToString();
            txtHSNSAC.Text = dtPrevious.Rows[0]["HSN"].ToString();
            txtPurchaseRate.Text = dtPrevious.Rows[0]["PurRate"].ToString();
            chkInctax.Checked = Convert.ToBoolean(dtPrevious.Rows[0]["IncludePTax"].ToString());
            txttaxAmount.Text = dtPrevious.Rows[0]["PurchaseTaxAmount"].ToString();
            chkDiscounttax.Checked = Convert.ToBoolean(dtPrevious.Rows[0]["DiscountOnTax"].ToString());
            txtPurDiscPer.Text = dtPrevious.Rows[0]["PurchaseDiscPercentage"].ToString();
            txtPurDiscAmnt.Text = dtPrevious.Rows[0]["PurchaseDiscAmount"].ToString();
            txtMRP.Text = dtPrevious.Rows[0]["MRP"].ToString();
            txtProfitPer.Text = dtPrevious.Rows[0]["ProfitPer"].ToString();
            txtProfitAmount.Text = dtPrevious.Rows[0]["ProfitAmount"].ToString();
            txtDiscountPer.Text = dtPrevious.Rows[0]["DiscountPer"].ToString();
            txtDiscountAmt.Text = dtPrevious.Rows[0]["DiscountAmt"].ToString();
            txtSTaxAmount.Text = dtPrevious.Rows[0]["SalesTaxAmount"].ToString();
            txtSalesPrice.Text = dtPrevious.Rows[0]["SalesPrice"].ToString();
        }
    }

    protected void QuotationData()
    {
        BlMaterialReciept blMaterialReciept = new BlMaterialReciept();
        BlPurchase blpurchase = new BlPurchase();
        blMaterialReciept.InvoiceDate = Convert.ToDateTime(txtInvoiceDate.Text);
        blMaterialReciept.FK_Items = Convert.ToInt64(HfItemsID.Value.ToString() == "" ? "0" : HfItemsID.Value.ToString());
        blMaterialReciept.FK_Supplier = Convert.ToInt64(ddlSupplier.SelectedValue.ToString());
        DataTable dtP = blMaterialReciept.PuchaseQuatationSelect();
        decimal Qty = Convert.ToDecimal(txtQuantity.Text == "" ? "0" : txtQuantity.Text);

        if (dtP.Rows.Count > 0)
        {
            lblQuotation.Text = "Item from Quotation";
            blpurchase.itemID = Convert.ToInt64(HfItemsID.Value.ToString());
            ddlUnit.SelectedValue = dtP.Rows[0]["FK_Units"].ToString();
            ddlSubunit.SelectedValue = dtP.Rows[0]["FK_SubUnit"].ToString();
            txtPurchaseRate.Text = dtP.Rows[0]["Rate"].ToString();
            if (Qty > 0)
            {
                if (Convert.ToDecimal(dtP.Rows[0]["FreeQuantityPer"].ToString()) > 0)
                    d = Qty / Convert.ToDecimal(dtP.Rows[0]["FreeQuantityPer"].ToString() == "" ? "0" : dtP.Rows[0]["FreeQuantityPer"].ToString());
                int val = (int)Math.Floor(d);
                if (Convert.ToBoolean(dtP.Rows[0]["Frequent"]) == true)
                {
                    txtFree.Text = Convert.ToString((val) * (Convert.ToDecimal(dtP.Rows[0]["FreeQuantity"].ToString())));
                    hfQFree.Value = txtFree.Text;
                }
                else
                {
                    if (Qty > Convert.ToDecimal(dtP.Rows[0]["FreeQuantityPer"].ToString()))
                    {
                        txtFree.Text = (Convert.ToString(dtP.Rows[0]["FreeQuantity"].ToString()));
                        hfQFree.Value = txtFree.Text;
                    }
                    else
                    {
                        txtFree.Text = "0";
                        hfQFree.Value = txtFree.Text;
                    }

                }
                if (Convert.ToBoolean(dtP.Rows[0]["FrequentPer"]) == true)
                {
                    if (Convert.ToDecimal(dtP.Rows[0]["DiscountPer"].ToString()) > 0)
                        d1 = Qty / Convert.ToDecimal(dtP.Rows[0]["DiscountPer"].ToString());
                    int value = Convert.ToInt32(d1);
                    txtPurDiscAmnt.Text = Convert.ToString((value) * (Convert.ToDecimal(dtP.Rows[0]["Discount"].ToString())));
                    txtPurDiscPer.Text = Math.Round((Convert.ToDecimal(txtPurDiscAmnt.Text.ToString() == "" ? "0" : txtPurDiscAmnt.Text.ToString())) * (100) / (Convert.ToDecimal(hfPurchaseValue.Value.ToString() == "" ? "0" : hfPurchaseValue.Value.ToString())), 4).ToString();
                    txtNetPur.Text = Math.Round((Convert.ToDecimal(hfPurchaseValue.Value.ToString() == "" ? "0" : hfPurchaseValue.Value.ToString())) + (Convert.ToDecimal(txttaxAmount.Text == null ? "0" : txttaxAmount.Text)) - (Convert.ToDecimal(txtPurDiscAmnt.Text == null ? "0" : txtPurDiscAmnt.Text)), 4).ToString();

                    hfQDisAmt.Value = txtPurDiscAmnt.Text;
                    hfDiscPer.Value = txtPurDiscPer.Text;
                }
                else if (Convert.ToBoolean(dtP.Rows[0]["PercentageWise"]) == true)
                {
                    if (Convert.ToDecimal(dtP.Rows[0]["DiscountPer"].ToString()) > 0)
                        d1 = Qty / Convert.ToDecimal(dtP.Rows[0]["DiscountPer"].ToString());
                    int value = Convert.ToInt32(d1);
                    txtPurDiscPer.Text = Convert.ToString((value) * (Convert.ToDecimal(dtP.Rows[0]["Discount"].ToString())));
                    hfDiscPer.Value = txtPurDiscPer.Text;
                    getpurchaseamount();
                }
                ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "calculateActualQuantity();", true);

                hfQPurchaseRate.Value = dtP.Rows[0]["Rate"].ToString();
                Enbletextbox(false);
                txtFree.Focus();
            }


        }
        else
        {
            lblQuotation.Text = "";
        }
    }


    protected void AddData()
    {

        ViewState["RedLine"] = null;
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
            dtbl.Columns.Add("SLNO", typeof(int));
            dtbl.Columns.Add("Item");
            dtbl.Columns.Add("FK_Items");
            dtbl.Columns.Add("FK_Units");
            dtbl.Columns.Add("Units");
            dtbl.Columns.Add("FK_SubUnit");
            dtbl.Columns.Add("SubUnit");
            dtbl.Columns.Add("FK_Category");
            dtbl.Columns.Add("Category");
            dtbl.Columns.Add("BatchNo");
            dtbl.Columns.Add("ExpiryDate");
            dtbl.Columns.Add("HSN");
            dtbl.Columns.Add("Quantity");
            dtbl.Columns.Add("Free");
            dtbl.Columns.Add("ActQty");
            dtbl.Columns.Add("PurchaseRate");
            dtbl.Columns.Add("IncludePtax");

            dtbl.Columns.Add("PurchasetaxAmount");
            dtbl.Columns.Add("DiscountOntax");
            dtbl.Columns.Add("PurchaseDiscPercentage");
            dtbl.Columns.Add("PurchaseDiscAmount");
            dtbl.Columns.Add("NetPurchase");
            dtbl.Columns.Add("MRP");
            dtbl.Columns.Add("ProfitPer");
            dtbl.Columns.Add("ProfitAmount");
            dtbl.Columns.Add("DiscountPer");
            dtbl.Columns.Add("DiscountAmt");
            dtbl.Columns.Add("IncludeStax");
            dtbl.Columns.Add("SaletaxAmount");
            dtbl.Columns.Add("SalePrice");
            dtbl.Columns.Add("ActualPurRate");
            dtbl.Columns.Add("FK_MaterialReciept");
            dtbl.Columns.Add("SingleTaxAmount");
            dtbl.Columns.Add("Flag");
            dtbl.Columns.Add("TaxPer");
            dtbl.Columns.Add("IsFreeItem");



        }
        if (hfItemId.Value != "")
        {

            gvItems.Enabled = true;
            dtbl.Rows.RemoveAt(Convert.ToInt32(hfItemId.Value));
            gvItems.DataSource = dtbl;
            gvItems.DataBind();
            hfSlNo.Value = hfItemId.Value;
            hfItemId.Value = "";



        }



        if (dtbl.Rows.Count > 0)
        {

            for (int i = 0; i < dtbl.Rows.Count; i++)
            {
                if (dtbl.Rows[i]["FK_Items"].ToString() == HfItemsID.Value.ToString() && dtbl.Rows[i]["BatchNo"].ToString() == txtBatch.Text.ToString())
                {
                    inCheck = inCheck + 1;
                    break;
                }
            }
        }




        if (inCheck == 0)
        {
            DataRow drow = dtbl.NewRow();
            if (hfSlNo.Value == "")
            {
                if (dtbl.Rows.Count > 0)
                {

                    Int64 max = Convert.ToInt64(dtbl.Rows[dtbl.Rows.Count - 1]["SLNO"].ToString());
                    drow["SLNO"] = Convert.ToInt64(max) + 1;
                }
                else
                {
                    drow["SLNO"] = 1;
                }
            }
            else
            {
                drow["SLNO"] = Convert.ToInt64(hfSlNo.Value) + 1;

            }

            drow["Item"] = hfItem.Value.ToString();
            drow["FK_Items"] = HfItemsID.Value.ToString();
            drow["FK_Units"] = ddlUnit.SelectedValue.ToString();
            drow["Units"] = ddlUnit.SelectedItem.ToString();
            drow["FK_SubUnit"] = ddlSubunit.SelectedValue.ToString();
            drow["SubUnit"] = ddlSubunit.SelectedItem.ToString();
            if (Convert.ToInt64(ddlSubunit.SelectedValue.ToString()) > 0)
                drow["SubUnit"] = ddlSubunit.SelectedItem.ToString();
            else
                drow["SubUnit"] = null;
            drow["FK_Category"] = ddlCategory.SelectedValue.ToString();
            drow["Category"] = ddlCategory.SelectedItem.ToString();
            drow["BatchNo"] = Convert.ToString(txtBatch.Text);
            drow["ExpiryDate"] = DateTime.ParseExact(txtExpiryDate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            drow["HSN"] = Convert.ToString(txtHSNSAC.Text);
            drow["Quantity"] = Convert.ToDecimal(txtQuantity.Text == "" ? "0" : txtQuantity.Text);
            drow["Free"] = Convert.ToDecimal(txtFree.Text == "" ? "0" : txtFree.Text);
            drow["ActQty"] = Convert.ToDecimal(txtActqty.Text == "" ? "0" : txtActqty.Text);
            drow["IncludePtax"] = Convert.ToBoolean(chkInctax.Checked);
            drow["PurchaseRate"] = Convert.ToDecimal(txtPurchaseRate.Text == "" ? "0" : txtPurchaseRate.Text);
            if (chkFree.Checked == true)
            {
                drow["TaxPer"] = "0";
            }
            else
            {
                drow["TaxPer"] = Convert.ToDecimal(hfTotTaxPercentage.Value == "" ? "0" : hfTotTaxPercentage.Value);
            }

            drow["PurchasetaxAmount"] = Convert.ToDecimal(txttaxAmount.Text == "" ? "0" : txttaxAmount.Text);
            drow["DiscountOntax"] = Convert.ToBoolean(chkDiscounttax.Checked);
            drow["PurchaseDiscPercentage"] = Convert.ToDecimal(txtPurDiscPer.Text == "" ? "0" : txtPurDiscPer.Text);
            drow["PurchaseDiscAmount"] = Convert.ToDecimal(txtPurDiscAmnt.Text == "" ? "0" : txtPurDiscAmnt.Text);
            drow["NetPurchase"] = Convert.ToDecimal(txtNetPur.Text == "" ? "0" : txtNetPur.Text);
            drow["MRP"] = Convert.ToDecimal(txtMRP.Text == "" ? "0" : txtMRP.Text);
            drow["ProfitPer"] = Convert.ToDecimal(txtProfitPer.Text == "" ? "0" : txtProfitPer.Text);
            drow["ProfitAmount"] = Convert.ToDecimal(txtProfitAmount.Text == "" ? "0" : txtProfitAmount.Text);
            drow["DiscountPer"] = Convert.ToDecimal(txtDiscountPer.Text == "" ? "0" : txtDiscountPer.Text);
            drow["DiscountAmt"] = Convert.ToDecimal(txtDiscountAmt.Text == "" ? "0" : txtDiscountAmt.Text);
            drow["IncludeStax"] = Convert.ToBoolean(ChkIncStax.Checked);
            drow["SaletaxAmount"] = Convert.ToDecimal(txtSTaxAmount.Text == "" ? "0" : txtSTaxAmount.Text);
            drow["SalePrice"] = Convert.ToDecimal(txtSalesPrice.Text == "" ? "0" : txtSalesPrice.Text);
            drow["SingleTaxAmount"] = Convert.ToDecimal(hfSingleQty.Value == "" ? "0" : hfSingleQty.Value);
            drow["ActualPurRate"] = Convert.ToDecimal(txtPurchaseRate.Text == "" ? "0" : txtPurchaseRate.Text) - Convert.ToDecimal(hfSingleQty.Value == "" ? "0" : hfSingleQty.Value);
            drow["Flag"] = Convert.ToInt64(txtKey.Text);
            drow["IsFreeItem"] = Convert.ToBoolean(chkFree.Checked);
            dtbl.Rows.InsertAt(drow, rowCount);
            dtbl.DefaultView.Sort = "SLNO";
            dtbl = dtbl.DefaultView.ToTable();
            gvItems.DataSource = dtbl;
            gvItems.DataBind();
            ViewState["TempTable"] = dtbl;
            hfSlNo.Value = "";
            txtItemShortName.Focus();
            btnShowData.Enabled = false;
            CalculateTotals();

            btnS.Text = "Ptax";
            HfItemsID.Value = "0";

            chkFree.Checked = false;
            EnableControlForFreeQty(true);
        }
        else
        {
            ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "alertify.alert('Item already exists with same batch no')", true);
        }

    }
    public void GridCalculation()
    {
        ArrayList arl = new ArrayList();
        long status = 0;
        BlMaterialReciept blMaterialReciept = new BlMaterialReciept();
        blMaterialReciept.InterStatePurchase = chkInterState.Checked;
        if (gvItems.Rows.Count > 0)
        {
            DataTable dt = (DataTable)ViewState["TempTable"];
            IPurchaseGridDetails IPurchaseGridDetails;
            for (int i = 0; i < dt.Rows.Count; i++)
            {

                IPurchaseGridDetails = new IPurchaseGridDetails();
                IPurchaseGridDetails.SlNo = Convert.ToInt32(i + 1);
                IPurchaseGridDetails.FK_Items = Convert.ToInt64(dt.Rows[i]["FK_Items"].ToString());
                IPurchaseGridDetails.Item = (dt.Rows[i]["Item"].ToString());
                IPurchaseGridDetails.Units = (dt.Rows[i]["Units"].ToString());
                IPurchaseGridDetails.SubUnit = (dt.Rows[i]["SubUnit"].ToString());
                IPurchaseGridDetails.Category = (dt.Rows[i]["Category"].ToString());
                IPurchaseGridDetails.Fk_Unit = Convert.ToInt32(dt.Rows[i]["FK_Units"].ToString() == "" ? "0" : dt.Rows[i]["FK_Units"].ToString());
                IPurchaseGridDetails.FK_SubUnit = Convert.ToInt32(dt.Rows[i]["FK_SubUnit"].ToString() == "" ? "0" : dt.Rows[i]["FK_SubUnit"].ToString());
                IPurchaseGridDetails.BatchNo = Convert.ToString(dt.Rows[i]["BatchNo"].ToString());
                IPurchaseGridDetails.ExpiryDate = Convert.ToDateTime(dt.Rows[i]["ExpiryDate"].ToString());
                IPurchaseGridDetails.HSN = Convert.ToString(dt.Rows[i]["HSN"].ToString());
                IPurchaseGridDetails.Quantity = Convert.ToDecimal(dt.Rows[i]["Quantity"].ToString());
                IPurchaseGridDetails.Free = Convert.ToDecimal(dt.Rows[i]["Free"].ToString());
                IPurchaseGridDetails.PurRate = Convert.ToDecimal(dt.Rows[i]["PurchaseRate"].ToString());
                IPurchaseGridDetails.IncludePTax = Convert.ToBoolean(dt.Rows[i]["IncludePtax"].ToString() == "1" ? "True" : "False");
                IPurchaseGridDetails.PurchaseTaxAmount = Convert.ToDecimal(dt.Rows[i]["PurchasetaxAmount"].ToString());
                IPurchaseGridDetails.TaxPer = Convert.ToDecimal(dt.Rows[i]["TaxPer"].ToString());
                IPurchaseGridDetails.DiscountOnTax = Convert.ToBoolean(dt.Rows[i]["DiscountOntax"].ToString() == "1" ? "True" : "False");
                IPurchaseGridDetails.PurchaseDiscAmount = Convert.ToDecimal(dt.Rows[i]["PurchaseDiscAmount"].ToString());
                IPurchaseGridDetails.NetPurchase = Convert.ToDecimal(dt.Rows[i]["NetPurchase"].ToString());
                IPurchaseGridDetails.MRP = Convert.ToDecimal(dt.Rows[i]["MRP"].ToString());
                IPurchaseGridDetails.ProfitAmount = Convert.ToDecimal(dt.Rows[i]["ProfitAmount"].ToString());
                IPurchaseGridDetails.DiscountPer = Convert.ToDecimal(dt.Rows[i]["DiscountPer"].ToString());
                IPurchaseGridDetails.DiscountAmt = Convert.ToDecimal(dt.Rows[i]["DiscountAmt"].ToString());
                IPurchaseGridDetails.IncludeSTax = Convert.ToBoolean(dt.Rows[i]["IncludeSTax"].ToString() == "1" ? "True" : "False");
                IPurchaseGridDetails.SalesTaxAmount = Convert.ToDecimal(dt.Rows[i]["SaletaxAmount"].ToString());
                IPurchaseGridDetails.SalesPrice = Convert.ToDecimal(dt.Rows[i]["SalePrice"].ToString());
                IPurchaseGridDetails.ProfitPer = Convert.ToDecimal(dt.Rows[i]["ProfitPer"].ToString());
                IPurchaseGridDetails.FlagBit = Convert.ToInt64(dt.Rows[i]["Flag"].ToString());

                arl.Add(IPurchaseGridDetails);
                IPurchaseGridDetails = null;

            }

            blMaterialReciept.xmlGriddata = Common.WriteToXml(arl);
            DataTable dst = blMaterialReciept.selectGridData();
            ViewState["TempTable"] = dst;
            gvItems.DataSource = dst;
            gvItems.DataBind();
        }
    }

    protected void BindExpenseData()
    {
        BlPurchase blPurchase = new BlPurchase();
        blPurchase.MasterId = Convert.ToInt64(ViewState["MasterIDTaxDetails"]);
        DataTable dt = blPurchase.SelectExpenseInPurchase();
        ViewState["ExpenseDetails"] = dt;
    }
    protected void BindPurchaseTax()
    {
        BlPurchase blPurchase = new BlPurchase();
        blPurchase.MasterId = Convert.ToInt64(ViewState["MasterIDTaxDetails"]);
        DataTable dt = blPurchase.SelectPurchaseTax();
        ViewState["GridDataPurchase"] = dt;
    }

    private void Transactionchecking()
    {
        BlFunction blfun = new BlFunction();
        blfun.Module = "PUR";
        TransactionID = blfun.GetTransactionID("PUR", hfuserName.Value);
        ViewState["TransactionID"] = TransactionID;
    }
    protected void CalculateTotals()
    {
        Decimal NetPur = 0; Decimal NetAmount = 0;
        if (ViewState["TempTable"] != null)
        {
            if (ViewState["TempTable"] != "")
            {
                DataTable dtbl = (DataTable)ViewState["TempTable"];
                if (dtbl.Rows.Count > 0)
                {



                    for (int i = 0; i < dtbl.Rows.Count; i++)
                    {
                        NetPur = NetPur + (Convert.ToDecimal(dtbl.Rows[i]["NetPurchase"].ToString()));

                    }

                    txtTotNetPur.Text = Math.Round(NetPur, 4).ToString();

                }
            }
            else
            {
                txtTotalAmount.Text = Math.Round(Convert.ToDecimal(0), 2).ToString();
                txtTotNetPur.Text = Math.Round(Convert.ToDecimal(0), 2).ToString();
                txtBillNetAmount.Text = Math.Round(Convert.ToDecimal(0), 2).ToString();
                txtOther.Text = Math.Round(Convert.ToDecimal(0), 2).ToString();
            }
        }
        else
        {
            txtTotalAmount.Text = Math.Round(Convert.ToDecimal(0), 2).ToString();
            txtTotNetPur.Text = Math.Round(Convert.ToDecimal(0), 2).ToString();
            txtBillNetAmount.Text = Math.Round(Convert.ToDecimal(0), 2).ToString();
            txtOther.Text = Math.Round(Convert.ToDecimal(0), 2).ToString();
        }

        if (ViewState["ExpenseDetails"] != null)
        {
            if (ViewState["ExpenseDetails"] != "")
            {
                DataTable dtExpense = (DataTable)ViewState["ExpenseDetails"];
                if (dtExpense.Rows.Count > 0)
                {

                    NetAmount = (Convert.ToDecimal(dtExpense.Rows[0]["grand"].ToString()));
                    // NetEx = (Convert.ToDecimal(dtExpense.Rows[0]["grand"].ToString()));
                    // hfOtherCharges.Value = NetAmount.ToString();
                }
            }
        }
        // hfOtherCharges.Value = NetAmount.ToString();
        //  txtOther.Text = Math.Round((NetAmount), 4).ToString();
        //txtOther.Text = "350";
        decimal billnetAmount = Convert.ToDecimal(hdnbillnetAmt.Value == "" ? "0" : hdnbillnetAmt.Value);
        txtOther.Text = NetAmount.ToString();
        if (billnetAmount > 0 && hfPurchseApprv.Value == "Approved")
        {
            txtBillNetAmount.Text = billnetAmount.ToString();
        }
        else
        {
            txtBillNetAmount.Text = Math.Round((NetAmount + Convert.ToDecimal(txtTotNetPur.Text)), 4).ToString();

        }

        ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "CalculateNetT();", true);

    }

    protected void Clear()
    {
        ViewState["TempTable"] = null;
        ViewState["RedLine"] = null;
        ddlStore.Enabled = true;
        chkInterState.Enabled = true;
        btnApprove.Visible = true;
        btnSubmit.Visible = true;
        gvItems.Enabled = true;
        gvItems.DataSource = null;
        gvItems.DataBind();
        ddlStore.SelectedValue = "0";
        txtGSTIN.Text = "";
        ViewState["ExpenseDetails"] = null;
        ViewState["GridDataPurchase"] = null;
        btnS.Text = "Ptax";
        //  DivPreviousPurchase.Visible = false;
        btnShowData.Enabled = true;
        CalculateTotals();
        btnOtherCharges.Visible = true;

        ViewState["MasterIDTaxDetails"] = 0;

        Transactionchecking();
        hdnbillnetAmt.Value = "0";
    }
    protected bool Validform()
    {
        Sts = true;
        foreach (GridViewRow row in gvItems.Rows)
        {

            //for templated control
            Label tb = row.FindControl("lblgvNetPurchase") as Label;
            decimal NetPurchase = Convert.ToDecimal(tb.Text.ToString());
            Label MRP = row.FindControl("lblgvMRP") as Label;
            decimal MRPValue = Convert.ToDecimal(MRP.Text.ToString());
            Label lbfreeitem = row.FindControl("lblgvISFreeItem") as Label;
            Boolean hfreeitems = Convert.ToBoolean(lbfreeitem.Text.ToString());
            if (hfreeitems == false)
            {
                if (NetPurchase == 0 || MRPValue == 0)
                {
                    row.ForeColor = System.Drawing.Color.Red;
                    Sts = false;
                }
            }
            else
            {
                if (MRPValue == 0)
                {
                    row.ForeColor = System.Drawing.Color.Red;
                    Sts = false;
                }
            }

        }
        return Sts;
    }


    protected void SaveData()
    {
        long status = 0;
        BlMaterialReciept blMaterialReciept = new BlMaterialReciept();


        // ── DATE VALIDATION ─────────────────────────────────────────
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

        //blMaterialReciept.RefNo = Convert.ToInt64(lblRefno.Text);
        blMaterialReciept.FK_Supplier = Convert.ToInt64(ddlSupplier.SelectedValue);
        blMaterialReciept.FK_PurchaseOrder = Convert.ToInt64(ddlPurchaseOrder.SelectedValue);
        blMaterialReciept.InvoiceNo = txtInvoiceNo.Text.ToString().Trim();
        blMaterialReciept.InvoiceDate = Convert.ToDateTime(txtInvoiceDate.Text);
        blMaterialReciept.GSTIN = txtGSTIN.Text;
        blMaterialReciept.InterStatePurchase = Convert.ToBoolean(chkInterState.Checked);

        blMaterialReciept.FK_Store = Convert.ToInt64(ddlStore.SelectedValue);
        blMaterialReciept.FK_Category = Convert.ToInt64(ddlCategory.SelectedValue);
        blMaterialReciept.RoundOff = Convert.ToDecimal(txtRoundOff.Text);
        blMaterialReciept.PurAmt = Convert.ToDecimal(txtTotNetPur.Text);

        blMaterialReciept.EnteredOn = Convert.ToDateTime(DateTime.Now);
        blMaterialReciept.FK_Client = Convert.ToInt64(hfClient.Value);
        blMaterialReciept.NetAmount = Convert.ToDecimal(txtBillNetAmount.Text);
        blMaterialReciept.UserCode = hfuserName.Value;
        blMaterialReciept.TransactionID = Convert.ToInt64(ViewState["TransactionID"]);

        if (hfID_Purchase.Value != "")
        {
            blMaterialReciept.MasterId = Convert.ToInt64(hfID_Purchase.Value);
        }

        ArrayList arlist = new ArrayList();
        DataTable dtbl = (DataTable)ViewState["TempTable"];


        IMaterialRecieptDetails IMaterialRecieptDetails;
        for (int i = 0; i < dtbl.Rows.Count; i++)
        {
            IMaterialRecieptDetails = new IMaterialRecieptDetails();

            IMaterialRecieptDetails.SlNo = Convert.ToInt32(i + 1);
            IMaterialRecieptDetails.FK_Items = Convert.ToInt64(dtbl.Rows[i]["FK_Items"].ToString() == "" ? "0" : dtbl.Rows[i]["FK_Items"].ToString());
            IMaterialRecieptDetails.FK_Category = Convert.ToInt32(dtbl.Rows[i]["FK_Category"].ToString() == "" ? "0" : dtbl.Rows[i]["FK_Category"].ToString());
            IMaterialRecieptDetails.Fk_Unit = Convert.ToInt32(dtbl.Rows[i]["FK_Units"].ToString() == "" ? "0" : dtbl.Rows[i]["FK_Units"].ToString());
            IMaterialRecieptDetails.FK_SubUnit = Convert.ToInt32(dtbl.Rows[i]["FK_SubUnit"].ToString() == "" ? "0" : dtbl.Rows[i]["FK_SubUnit"].ToString());
            IMaterialRecieptDetails.BatchNo = Convert.ToString(dtbl.Rows[i]["BatchNo"].ToString());
            IMaterialRecieptDetails.ExpiryDate = Convert.ToDateTime(dtbl.Rows[i]["ExpiryDate"].ToString());
            IMaterialRecieptDetails.HSN = Convert.ToString(dtbl.Rows[i]["HSN"].ToString());
            IMaterialRecieptDetails.Quantity = Convert.ToDecimal(dtbl.Rows[i]["Quantity"].ToString());
            IMaterialRecieptDetails.Free = Convert.ToDecimal(dtbl.Rows[i]["Free"].ToString());
            IMaterialRecieptDetails.ActQty = Convert.ToDecimal(dtbl.Rows[i]["ActQty"].ToString());
            IMaterialRecieptDetails.PurRate = Convert.ToDecimal(dtbl.Rows[i]["PurchaseRate"].ToString());
            IMaterialRecieptDetails.IncludePTax = Convert.ToBoolean(dtbl.Rows[i]["IncludePtax"].ToString());
            IMaterialRecieptDetails.TaxPer = Convert.ToDecimal(dtbl.Rows[i]["TaxPer"].ToString());
            IMaterialRecieptDetails.PurchaseTaxAmount = Convert.ToDecimal(dtbl.Rows[i]["PurchasetaxAmount"].ToString());
            IMaterialRecieptDetails.DiscountOnTax = Convert.ToBoolean(dtbl.Rows[i]["DiscountOntax"].ToString());
            IMaterialRecieptDetails.PurchaseDiscPercentage = Convert.ToDecimal(dtbl.Rows[i]["PurchaseDiscPercentage"].ToString());
            IMaterialRecieptDetails.PurchaseDiscAmount = Convert.ToDecimal(dtbl.Rows[i]["PurchaseDiscAmount"].ToString());
            IMaterialRecieptDetails.NetPurchase = Convert.ToDecimal(dtbl.Rows[i]["NetPurchase"].ToString());
            IMaterialRecieptDetails.MRP = Convert.ToDecimal(dtbl.Rows[i]["MRP"].ToString());
            IMaterialRecieptDetails.ProfitPer = Convert.ToDecimal(dtbl.Rows[i]["ProfitPer"].ToString());
            IMaterialRecieptDetails.ProfitAmount = Convert.ToDecimal(dtbl.Rows[i]["ProfitAmount"].ToString());
            IMaterialRecieptDetails.DiscountPer = Convert.ToDecimal(dtbl.Rows[i]["DiscountPer"].ToString());
            IMaterialRecieptDetails.DiscountAmt = Convert.ToDecimal(dtbl.Rows[i]["DiscountAmt"].ToString());
            IMaterialRecieptDetails.IncludeSTax = Convert.ToBoolean(dtbl.Rows[i]["IncludeSTax"].ToString());
            IMaterialRecieptDetails.SalesTaxAmount = Convert.ToDecimal(dtbl.Rows[i]["SaletaxAmount"].ToString());
            IMaterialRecieptDetails.SalesPrice = Convert.ToDecimal(dtbl.Rows[i]["SalePrice"].ToString());
            IMaterialRecieptDetails.FlagBit = Convert.ToInt16(dtbl.Rows[i]["Flag"].ToString());
            IMaterialRecieptDetails.IsFreeItem = Convert.ToBoolean(dtbl.Rows[i]["IsFreeItem"]);
            //IMaterialRecieptDetails.ActualPurchaseRate = Convert.ToDecimal(dtbl.Rows[i]["PurchaseRate"].ToString()) - Convert.ToDecimal(dtbl.Rows[i]["SingleTaxAmount"].ToString());

            decimal ActPur = 0;

            if (IMaterialRecieptDetails.IncludePTax)
            {
                ActPur = Math.Round(((IMaterialRecieptDetails.PurRate * 100) / (100 + IMaterialRecieptDetails.TaxPer)), 4);
            }
            else
            {
                ActPur = IMaterialRecieptDetails.PurRate;
                //IMaterialRecieptDetails.ActualPurchaseRate = ActPur - singleAmnt;
            }


            IMaterialRecieptDetails.ActualPurchaseRate = ActPur;

            //ToString()=="1"?"True":"False");

            arlist.Add(IMaterialRecieptDetails);
            IMaterialRecieptDetails = null;
        }

        blMaterialReciept.XmlMaterialRecieptItems = Common.WriteToXml(arlist);
        DataTable dt = (DataTable)ViewState["GridDataPurchase"];

        if (dt != null && dt.Rows.Count > 0)
        {
            IPurchaseTaxDetails IPurchaseTaxDetail;
            ArrayList arPurchase = new ArrayList();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                IPurchaseTaxDetail = new IPurchaseTaxDetails();
                // IPurchaseTaxDetail.SlNo = Convert.ToInt32(i + 1);
                IPurchaseTaxDetail.TaxTypeID = Convert.ToInt64(dt.Rows[i]["ID_TaxType"].ToString());
                IPurchaseTaxDetail.TaxType = (dt.Rows[i]["TaxType"].ToString());
                IPurchaseTaxDetail.Percentage = (dt.Rows[i]["TaxPercentage"].ToString());
                IPurchaseTaxDetail.FK_Items = Convert.ToInt64(dt.Rows[i]["ID_items"].ToString());
                IPurchaseTaxDetail.TaxInterstate = Convert.ToBoolean(dt.Rows[i]["TaxInterState"].ToString());
                IPurchaseTaxDetail.TaxIntrastate = Convert.ToBoolean(dt.Rows[i]["TaxIntraState"].ToString());
                arPurchase.Add(IPurchaseTaxDetail);
            }
            blMaterialReciept.XmlPurchaseTax = Common.WriteToXml(arPurchase);
        }



        DataTable dtExpense = (DataTable)ViewState["ExpenseDetails"];
        if (dtExpense != null && dtExpense.Rows.Count > 0)
        {
            IExpenseDetails IExpenseDetails;
            ArrayList arExpense = new ArrayList();
            for (int i = 0; i < dtExpense.Rows.Count; i++)
            {
                IExpenseDetails = new IExpenseDetails();
                IExpenseDetails.SlNo = Convert.ToInt32(i + 1);
                IExpenseDetails.FK_ExpenseType = Convert.ToInt64(dtExpense.Rows[i]["ID_ExpenseType"].ToString());
                IExpenseDetails.Amount = Convert.ToDecimal(dtExpense.Rows[i]["Amount"].ToString());
                arExpense.Add(IExpenseDetails);
            }
            blMaterialReciept.xmlExpenseData = Common.WriteToXml(arExpense);
        }
        if (hfID_Purchase.Value == "")
        {
            if (hfIsAdd.Value.ToLower() == "true")
            {
                status = blMaterialReciept.InsertData();
                if (status > 0)
                {

                    BlMaterialReciept blmaterialReciepts = new BlMaterialReciept();

                    blmaterialReciepts.MasterId = status;
                    var refno = blmaterialReciepts.SelectPurchaseRefNo();

                    ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "toastr.success('Data Inserted Successfully');alertify.alert('Reference Number:  " + refno + "');", true);
                    ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "ClearAdd();", true);
                    ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "ClearAdd();", true);
                    Clear();
                    CurrentRefNo();
                    ViewState["GridDataPurchase"] = null;
                    ViewState["GridDataSales"] = null;
                    ViewState["TempTable"] = null;
                    Populate populate = new Populate();
                    ddlSupplier.Focus();
                    ddlStore.SelectedValue = Session["mainstore"].ToString();
                    ddlStore.Enabled = false;
                }
                else if (status == -8)
                {
                    ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "toastr.error('Cannot update data.. TransactionID  already exist')", true);
                }
                else if (status == -66)
                {
                    ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "toastr.error('Please Update FinYear For Transaction')", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "ShowAddDiv();", true);

                    ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "toastr.error('Duplicate Invoice For Same Supplier')", true);

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

                blMaterialReciept.MasterId = Convert.ToInt64(hfID_Purchase.Value);
                status = blMaterialReciept.UpdateData();
                if (status > 0)
                {
                    BlMaterialReciept blmaterialReciepts = new BlMaterialReciept();

                    blmaterialReciepts.MasterId = status;
                    var refno = blmaterialReciepts.SelectPurchaseRefNo();
                    ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "toastr.success('Data Updated Successfully');alertify.alert('Reference Number:  " + refno + "');", true);

                    ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "ClearAdd();", true);
                    ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "ClearAdd();", true);
                    Clear(); CurrentRefNo();
                    Populate populate = new Populate();
                    ddlSupplier.Focus();


                    //ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "alertify.alert('Reference Number:  " + refno + "');", true);

                }
                else if (status == -1)
                {

                    ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "toastr.error('Cannot update data.. ')", true);

                }
                else if (status == -2)
                {

                    ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "toastr.error('Cannot update data.. ')", true);

                }
                else if (status == -66)
                {
                    ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "toastr.error('Please Update FinYear For Transaction')", true);
                }

            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "toastr.warning('User does not have permission to Update')", true);

            }
        }
        blMaterialReciept = null;
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {



            if (!IsPostBack)
            {
                ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "ClearAdd();", true);
                divAdd.Style.Add("display", "block");
                divView.Style.Add("display", "None");
                hfUser.Value = Request.QueryString["uid"];
                hfClient.Value = Request.QueryString["clnt"];
                txtSupplierName.Enabled = true;
                ImageButton3.Enabled = true;
                hfItemId.Value = "";
                CheckUserAccess();
                BindData();
                hfuserName.Value = Session["Username"].ToString();
                ddlStore.SelectedValue = Session["mainstore"].ToString();
                ddlStore.Enabled = false;
                SetControls();
                // GridFill(1);
                gvItems.DataSource = null;
                gvItems.DataBind();
                ddlSupplier.Focus();

                txtInvoiceDate_CalendarExtender.EndDate = DateTime.Now;

                if (hfIsApprove.Value.ToLower() == "true")
                {
                    btnmppPurchaseApproveDone.Enabled = true;
                    liwarning.Visible = false;

                }
                else
                {
                    btnmppPurchaseApproveDone.Enabled = false;
                    liwarning.Visible = true;
                }
                if (Convert.ToBoolean(hfIsAdd.Value) == false)
                {
                    ((Label)CommonPopupPrivilege.FindControl("popuplabel")).Text = "User does not have permission to Add";
                    mpPrivilege.Show();
                }
                if (Convert.ToBoolean(hfIsUpdate.Value) == false)
                {
                    gvPurchase.Columns[6].Visible = false; //gvPurchase.Columns[5].Visible = false;
                }
                if (Convert.ToBoolean(hfIsDelete.Value) == false)
                {
                    gvPurchase.Columns[7].Visible = false; //gvPurchase.Columns[6].Visible = false;
                }
                Transactionchecking();
                //CurrentRefNo();
                liRefNo.Style.Add("display", "none");

            }
            chkInterState.Checked = false;
            ApplyMonthlyProcessGraceBlock();

        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "toastr.warning('" + ex.Message + "')", true);
        }

    }

    /// <summary>
    /// Blocks Material Receipt submit when Monthly Process grace due date is overdue.
    /// Warning remains until due date becomes a future date (after Monthly Process is completed).
    /// </summary>
    private void ApplyMonthlyProcessGraceBlock()
    {
        try
        {
            long fkClient = 0;
            long fkStore = 0;
            if (!string.IsNullOrEmpty(hfClient.Value))
                long.TryParse(hfClient.Value.Split(',')[0], out fkClient);
            if (Session["mainstore"] != null)
                long.TryParse(Session["mainstore"].ToString(), out fkStore);
            else if (ddlStore.SelectedValue != null)
                long.TryParse(ddlStore.SelectedValue, out fkStore);

            MonthlyProcessGraceInfo graceStatus = MonthlyProcessGraceHelper.Evaluate(fkClient, fkStore);
            if (graceStatus != null && graceStatus.IsOverdue)
            {
                hfMonthlyProcessBlocked.Value = "1";
                btnSubmit.Enabled = false;
                lblMonthlyProcessWarning.Visible = true;
                lblMonthlyProcessWarning.Text = graceStatus.Message;

                // Show popup only once on initial page load (not on postbacks).
                if (!IsPostBack)
                {
                    string safeMessage = graceStatus.Message.Replace("\\", "\\\\").Replace("'", "\\'").Replace("\r", " ").Replace("\n", " ");
                    ScriptManager.RegisterStartupScript(Page, typeof(Page), "MonthlyProcessOverdueMR",
                        "setTimeout(function(){ try { if(window._mpWarnShown) return; window._mpWarnShown=true; toastr.clear(); toastr.options={closeButton:true,timeOut:0,extendedTimeOut:0,positionClass:'toast-top-full-width'}; toastr.error('"
                        + safeMessage + "', 'Monthly Process'); } catch(e) { alert('" + safeMessage + "'); } }, 500);", true);
                }
            }
            else
            {
                bool wasBlocked = hfMonthlyProcessBlocked.Value == "1";
                hfMonthlyProcessBlocked.Value = "0";
                lblMonthlyProcessWarning.Visible = false;
                lblMonthlyProcessWarning.Text = string.Empty;
                if (wasBlocked)
                    btnSubmit.Enabled = true;
            }
        }
        catch
        {
        }
    }

    private bool IsMonthlyProcessOverdue()
    {
        try
        {
            long fkClient = 0;
            long fkStore = 0;
            if (!string.IsNullOrEmpty(hfClient.Value))
                long.TryParse(hfClient.Value.Split(',')[0], out fkClient);
            if (Session["mainstore"] != null)
                long.TryParse(Session["mainstore"].ToString(), out fkStore);
            else if (ddlStore.SelectedValue != null)
                long.TryParse(ddlStore.SelectedValue, out fkStore);

            MonthlyProcessGraceInfo graceStatus = MonthlyProcessGraceHelper.Evaluate(fkClient, fkStore);
            return graceStatus != null && graceStatus.IsOverdue;
        }
        catch
        {
            return false;
        }
    }
    private void CurrentRefNo()
    {
        BlMaterialReciept blmaterialReciepts = new BlMaterialReciept();


        lblRefno.Text = blmaterialReciepts.SelectPurchaseRefNo();

    }
    public void SetReadonly(params TextBox[] textbox)
    {
        foreach (TextBox txt in textbox)
            txt.Attributes.Add("readonly", "readonly");

    }


    protected void GridFill()
    {
        if (ddlSupplier.SelectedIndex > 0 && ddlStore.SelectedIndex > 0 && txtInvoiceNo.Text != "")
        {
            BlMaterialReciept blmaterialReciepts = new BlMaterialReciept();
            blmaterialReciepts.FK_Supplier = Convert.ToInt64(ddlSupplier.SelectedValue);
            blmaterialReciepts.FK_Store = Convert.ToInt64(ddlStore.SelectedValue);
            blmaterialReciepts.InvoiceNo = Convert.ToString(txtInvoiceNo.Text.ToString().Trim());
            blmaterialReciepts.FK_Client = Convert.ToInt64(hfClient.Value);
            DataTable dtbl = new DataTable();
            dtbl = blmaterialReciepts.SelectMaterialRecieptDetails();


            if (dtbl.Rows.Count > 0)
            {
                hfID_Purchase.Value = Convert.ToString(dtbl.Rows[0]["ID_Purchase"]);

                if (dtbl.Rows.Count > 0)
                {
                    if (Convert.ToString(dtbl.Rows[0]["Approved"]) == "True")
                    {
                        btnApprove.Visible = false;
                        btnSubmit.Visible = false;
                        gvItems.Enabled = false;
                        btnOtherCharges.Visible = false;


                        ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "toastr.warning('Purchase Already Approved')", true);
                    }

                }
            }
            else
            {
                hfID_Purchase.Value = "";

            }


            int rowCount = 0;
            DataTable dtbl1 = new DataTable();

            if (ViewState["TempTable"] != null)
            {
                dtbl1 = (DataTable)ViewState["TempTable"];
                rowCount = dtbl1.Rows.Count;
            }

            if (dtbl1.Rows.Count > 0)
            {
                if (ddlPurchaseOrder.SelectedIndex > 0 && (ddlPurchaseOrder.SelectedValue == Convert.ToString(txtPurchaseOrderNo.Text.ToString().Trim())))
                {
                    if (dtbl.Rows.Count > 0)
                    {
                        for (int i = 0; i < dtbl.Rows.Count; i++)
                        {
                            DataRow drow = dtbl1.NewRow();
                            drow["SLNO"] = rowCount + 1;
                            drow["Item"] = Convert.ToString(dtbl.Rows[i]["Item"]);
                            drow["FK_Items"] = Convert.ToString(dtbl.Rows[i]["FK_Items"]);
                            drow["FK_Units"] = Convert.ToString(dtbl.Rows[i]["FK_Units"]);
                            drow["Units"] = Convert.ToString(dtbl.Rows[i]["Units"]);



                            drow["BatchNo"] = Convert.ToString(dtbl.Rows[i]["BatchNo"]);
                            drow["BarCode"] = Convert.ToString(dtbl.Rows[i]["BarCode"]);
                            drow["ExpiryDate"] = Convert.ToString(dtbl.Rows[i]["ExpiryDate"]);

                            drow["Quantity"] = Convert.ToDecimal(dtbl.Rows[i]["Quantity"]);
                            drow["ActQty"] = Convert.ToDecimal(dtbl.Rows[i]["ActQty"]);
                            drow["Free"] = Convert.ToDecimal(dtbl.Rows[i]["Free"]);

                            drow["PurchaseRate"] = Convert.ToDecimal(dtbl.Rows[i]["PurchaseRate"]);
                            drow["MRP"] = Convert.ToDecimal(dtbl.Rows[i]["MRP"]);

                            drow["VAT"] = Convert.ToDecimal(dtbl.Rows[i]["VAT"]);
                            drow["PurchaseVAT"] = Convert.ToDecimal(dtbl.Rows[i]["PurchaseVAT"]);

                            drow["DiscountPer"] = Convert.ToDecimal(dtbl.Rows[i]["DiscountPer"]);
                            drow["DiscountAmt"] = Convert.ToDecimal(dtbl.Rows[i]["DiscountAmt"]);
                            drow["ProfitPer"] = Convert.ToDecimal(dtbl.Rows[i]["ProfitPer"]);


                            drow["RetailPrice"] = Convert.ToDecimal(dtbl.Rows[i]["RetailPrice"]);
                            drow["WholeSalePrice"] = Convert.ToDecimal(dtbl.Rows[i]["WholeSalePrice"]);

                            drow["PrivilagePrice"] = Convert.ToDecimal(dtbl.Rows[i]["PrivilagePrice"]);
                            drow["PurchaseTotal"] = Convert.ToDecimal(dtbl.Rows[i]["PurchaseTotal"]);
                            drow["PurchaseDiscPercentage"] = Convert.ToDecimal(dtbl.Rows[i]["PurchaseDiscPercentage"]);

                            drow["PurchaseDiscAmount"] = Convert.ToDecimal(dtbl.Rows[i]["PurchaseDiscAmount"]);
                            drow["NetPurchase"] = Convert.ToDecimal(dtbl.Rows[i]["NetPurchase"]);

                            drow["VATAmnt"] = Convert.ToDecimal(dtbl.Rows[i]["VATAmnt"]);


                            drow["NetAmount"] = Convert.ToDecimal(dtbl.Rows[i]["NetAmount"]);


                            drow["FK_MaterialReciept"] = Convert.ToInt64(dtbl.Rows[i]["FK_MaterialReciept"]);
                            drow["CGST"] = Convert.ToDecimal(dtbl.Rows[i]["CGST"]);
                            drow["SGST"] = Convert.ToDecimal(dtbl.Rows[i]["SGST"]);
                            drow["IGST"] = Convert.ToDecimal(dtbl.Rows[i]["IGST"]);

                            drow["TaxPer"] = Convert.ToDecimal(dtbl.Rows[i]["TaxPer"]);

                            dtbl1.Rows.InsertAt(drow, rowCount);
                        }
                    }

                    gvItems.DataSource = dtbl1;
                    gvItems.DataBind();
                    ViewState["TempTable"] = dtbl1;


                }

                //   CalculateTotals();
            }
            else
            {

                //appending with purchaseOrder

                gvItems.DataSource = dtbl;



                gvItems.DataBind();
                ViewState["TempTable"] = dtbl;
            }


            if (dtbl1.Rows.Count > 0)
            {

                txtRoundOff.Text = Convert.ToDecimal(dtbl.Rows[0]["RoundOff"]).ToString();
            }
            blmaterialReciepts = null;
            Populate populate = new Populate();


        }
        CalculateTotals();
    }

    protected void btnMRDelete_Click(object sender, EventArgs e)
    {

        try
        {

            //
            string confirmValue = Request.Form["confirm_value"];
            if (confirmValue.Length > 3)
                confirmValue = confirmValue.Substring(confirmValue.Length - 3, 3);
            if (confirmValue == "Yes")
            {
                DataTable dtblR = (DataTable)ViewState["TempTable"];
            }

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
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        try
        {
            //
            string aa = hfpurrate.Value;



            DateTime dtExpiry = DateTime.ParseExact(txtExpiryDate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);


            DateTime dtNow = Convert.ToDateTime(hfDate.Value);
            if (hfItem.Value.Trim() == txtItemShortName.Text.ToString().Trim())
            {
                int days = Convert.ToInt32(hfExpirydays.Value.ToString()) + (30);
                DateTime date = dtNow.AddDays(days);
                if (dtExpiry < date)
                    ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "alertify.alert('Expirydate should be greater than Item Expiry Days')", true);
                else
                    if (dtExpiry < dtNow)
                    {

                        if (Validate())
                        {
                            AddData();
                            hfApproveOk.Value = "1";
                        }

                    }
                    else
                    {
                        if (Validate())
                        {
                            AddData();
                            hfApproveOk.Value = "1";
                        }

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


    protected void gvItems_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        try
        {
            //
            hfGvItemSelected.Value = "1";
            if (gvItems.Rows.Count > 0)
            {
                DataTable dtbl = new DataTable();
                if (ViewState["TempTable"] != null)
                {
                    dtbl = (DataTable)ViewState["TempTable"];
                    // Populate populate = new Populate();
                    //   populate.FillDropdownList(ddlItem, "Items", "ItemName", "ID_Items", "ItemName", "Cancelled=0 AND FK_Client= " + hfClient.Value.Split(',')[0], "");
                    ddlItem.SelectedValue = Convert.ToString(((Label)gvItems.Rows[e.NewSelectedIndex].FindControl("lblgvFK_Items")).Text) == "" ? "0" : Convert.ToString(((Label)gvItems.Rows[e.NewSelectedIndex].FindControl("lblgvFK_Items")).Text);
                    txtItemShortName.Text = Convert.ToString(((Label)gvItems.Rows[e.NewSelectedIndex].FindControl("lblgvItem")).Text);
                    ddlUnit.SelectedValue = Convert.ToString(((Label)gvItems.Rows[e.NewSelectedIndex].FindControl("lblgvFK_Units")).Text) == "" ? "0" : Convert.ToString(((Label)gvItems.Rows[e.NewSelectedIndex].FindControl("lblgvFK_Units")).Text);
                    ddlCategory.SelectedValue = Convert.ToString(((Label)gvItems.Rows[e.NewSelectedIndex].FindControl("lblgvFK_Category")).Text) == "" ? "0" : Convert.ToString(((Label)gvItems.Rows[e.NewSelectedIndex].FindControl("lblgvFK_Category")).Text);
                    ddlSubunit.SelectedValue = Convert.ToString(((Label)gvItems.Rows[e.NewSelectedIndex].FindControl("lblgvFK_SubUnit")).Text) == "" ? "0" : Convert.ToString(((Label)gvItems.Rows[e.NewSelectedIndex].FindControl("lblgvFK_SubUnit")).Text);
                    txtBatch.Text = Convert.ToString(((Label)gvItems.Rows[e.NewSelectedIndex].FindControl("lblgvBatchNo")).Text);
                    txtExpiryDate.Text = Convert.ToString(((Label)gvItems.Rows[e.NewSelectedIndex].FindControl("lblgvExpiryDate")).Text).Substring(0, 10);
                    txtHSNSAC.Text = Convert.ToString(((Label)gvItems.Rows[e.NewSelectedIndex].FindControl("lblgvhsn")).Text);
                    txtQuantity.Text = Convert.ToString(((Label)gvItems.Rows[e.NewSelectedIndex].FindControl("lblgvquty")).Text);
                    txtActqty.Text = Convert.ToString(((Label)gvItems.Rows[e.NewSelectedIndex].FindControl("lblgvActQty")).Text);
                    txtMRP.Text = Convert.ToString(((Label)gvItems.Rows[e.NewSelectedIndex].FindControl("lblgvMRP")).Text);
                    txtDiscountPer.Text = Convert.ToString(((Label)gvItems.Rows[e.NewSelectedIndex].FindControl("lblgvDiscountPer")).Text);
                    txtDiscountAmt.Text = Convert.ToString(((Label)gvItems.Rows[e.NewSelectedIndex].FindControl("lblgvDiscountAmt")).Text);


                    txtFree.Text = Convert.ToString(((Label)gvItems.Rows[e.NewSelectedIndex].FindControl("lblgvFree")).Text);

                    txtPurchaseRate.Text = Convert.ToString(((Label)gvItems.Rows[e.NewSelectedIndex].FindControl("lblgvPurchaseRate")).Text);
                    chkInctax.Checked = Convert.ToBoolean(((Label)gvItems.Rows[e.NewSelectedIndex].FindControl("lblgvIncludeptax")).Text);
                    txttaxAmount.Text = Convert.ToString(((Label)gvItems.Rows[e.NewSelectedIndex].FindControl("lblgvPurchaseTax")).Text);
                    chkDiscounttax.Checked = Convert.ToBoolean(((Label)gvItems.Rows[e.NewSelectedIndex].FindControl("lblgvDiscountOntax")).Text);
                    txtPurDiscPer.Text = Convert.ToString(((Label)gvItems.Rows[e.NewSelectedIndex].FindControl("lblgvPurDiscPer")).Text);
                    txtPurDiscAmnt.Text = Convert.ToString(((Label)gvItems.Rows[e.NewSelectedIndex].FindControl("lblgvPurchaseDiscAmount")).Text);
                    txtNetPur.Text = Convert.ToString(((Label)gvItems.Rows[e.NewSelectedIndex].FindControl("lblgvNetPurchase")).Text);
                    txtMRP.Text = Convert.ToString(((Label)gvItems.Rows[e.NewSelectedIndex].FindControl("lblgvMRP")).Text);
                    txtProfitPer.Text = Convert.ToString(((Label)gvItems.Rows[e.NewSelectedIndex].FindControl("lblgvProfitPer")).Text);
                    txtProfitAmount.Text = Convert.ToString(((Label)gvItems.Rows[e.NewSelectedIndex].FindControl("lblgvProfitAmount")).Text);
                    txtSalesPrice.Text = Convert.ToString(((Label)gvItems.Rows[e.NewSelectedIndex].FindControl("lblgvSalesPrice")).Text);
                    txtSTaxAmount.Text = Convert.ToString(((Label)gvItems.Rows[e.NewSelectedIndex].FindControl("lblgvSalesTaxAmt")).Text);

                    // ChkIncStax.Checked = Convert.ToBoolean(((Label)gvItems.Rows[e.NewSelectedIndex].FindControl("lblgvIncludeStax")).Text=="1"?"True":"False");
                    ChkIncStax.Checked = Convert.ToBoolean(((Label)gvItems.Rows[e.NewSelectedIndex].FindControl("lblgvIncludeStax")).Text);
                    chkFree.Checked = Convert.ToBoolean(((Label)gvItems.Rows[e.NewSelectedIndex].FindControl("lblgvISFreeItem")).Text);
                    hfItemId.Value = Convert.ToString(e.NewSelectedIndex);
                    HfItemsID.Value = Convert.ToString(((Label)gvItems.Rows[e.NewSelectedIndex].FindControl("lblgvFK_Items")).Text) == "" ? "0" : Convert.ToString(((Label)gvItems.Rows[e.NewSelectedIndex].FindControl("lblgvFK_Items")).Text);
                    hfItem.Value = Convert.ToString(((Label)gvItems.Rows[e.NewSelectedIndex].FindControl("lblgvItem")).Text);
                    if (chkFree.Checked == false)
                    {


                        decimal PurRate = Convert.ToDecimal(((Label)gvItems.Rows[e.NewSelectedIndex].FindControl("lblgvPurchaseRate")).Text);
                        decimal AcPurRate = Convert.ToDecimal(((Label)gvItems.Rows[e.NewSelectedIndex].FindControl("lblgvActualPurRate")).Text);
                        hfSingleQty.Value = Convert.ToString(PurRate - AcPurRate);
                        EnableControlForFreeQty(true);
                    }
                    else
                    {
                        EnableControlForFreeQty(false);
                    }
                    gvItems.Enabled = true;
                    // CalculateActualQuantity();
                    //ddlItemsFill();
                    ddlItemsFillEdit();

                    hfApproveOk.Value = "1";




                }
            }


        }

        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "toastr.warning('" + ex.Message + "')", true);
        }
    }

    protected void gvItems_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {

        try
        {
            //
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
                    DataTable dtbl12 = (DataTable)ViewState["GridDataPurchase"];


                    for (int i = 0; i < dtbl12.Rows.Count; i++)
                    {

                        // string a = e.Keys["FK_Items"].ToString();
                        string b = dtbl12.Rows[i]["ID_Items"].ToString();
                        if (dtbl12.Rows[i]["ID_Items"].ToString() == e.Keys["FK_Items"].ToString())
                        {
                            dtbl12.Rows[i].Delete();

                        }
                    }
                    dtbl12.AcceptChanges();
                    ViewState["GridDataPurchase"] = dtbl12;

                    hfApproveOk.Value = "1";


                    CalculateTotals();
                }

            }
            ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "ShowAddDiv();", true);

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
            if (IsMonthlyProcessOverdue())
            {
                ApplyMonthlyProcessGraceBlock();
                ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script",
                    "toastr.error('Monthly Process is overdue. Complete Monthly Process before submitting Material Receipt.');", true);
                return;
            }

            //System.Threading.Thread.Sleep(1000);

            if (ddlSupplier.SelectedItem.Text.ToString().Trim() == txtSupplierName.Text.ToString().Trim())
            {
                // if (ddlPurchaseOrder.SelectedValue.ToString().Trim() == txtPurchaseOrderNo.Text.ToString().Trim())
                // {
                int inCheckMRP = 0;
                if (gvItems.Rows.Count > 0)
                {
                    DataTable dtblCheck = (DataTable)ViewState["TempTable"];
                    if (dtblCheck.Rows.Count > 0)
                    {

                        for (int i = 0; i < dtblCheck.Rows.Count; i++)
                        {
                            if (dtblCheck.Rows[i]["MRP"].ToString() == "0")
                            {
                                inCheckMRP = inCheckMRP + 1;
                                break;
                            }
                        }
                    }
                    if (inCheckMRP == 0)
                    {

                        if (Validform())
                            SaveData();
                        else
                            ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "toastr.warning('Incomplete item detail exist')", true);



                        ddlSupplier.Focus();
                    }
                    else
                    {


                        ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "toastr.warning('Incomplete item detail exist')", true);


                    }
                }
                else
                {

                    ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "toastr.warning('Atleast one item required')", true);
                }

            }
            else
            {
                ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "toastr.warning('Supplier Mismatch')", true);

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
            //

            Clear();
            //CurrentRefNo();
            ddlStore.SelectedValue = Session["mainstore"].ToString();
            ddlStore.Enabled = false;
            Populate populate = new Populate();
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "toastr.warning('" + ex.Message + "')", true);
        }
    }
    protected void btnHiddenActulaQuantity_Click(object sender, EventArgs e)
    {
        try
        {
            //
            CalculateActualQuantity();

        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "toastr.warning('" + ex.Message + "')", true);
        }
    }
    protected void btnHiddenActualQuantityfromUnit_Click(object sender, EventArgs e)
    {
        try
        {
            //
            CalculateActualQuantity();
            txtBatch.Focus();
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "toastr.warning('" + ex.Message + "')", true);
        }
    }

    protected void CalculateActualQuantity()
    {
        if (ddlUnit.SelectedIndex > 0)
        {
            BlUnit blunit = new BlUnit();
            blunit.MasterId = Convert.ToInt64(ddlUnit.SelectedValue);
            blunit.FK_Client = Convert.ToInt64(hfClient.Value);
            blunit.SelectData();
            BlSubUnit blSub = new BlSubUnit();
            if (Convert.ToInt64(ddlSubunit.SelectedValue) > 0)
            {
                blSub.MasterId = Convert.ToInt64(ddlSubunit.SelectedValue);
                blSub.SelectData();
            }


            // txtActqty.Text = Convert.ToString(Math.Round((Convert.ToDecimal(blunit.NoofUnits) * Convert.ToDecimal(txtQuantity.Text == "" ? "0" : txtQuantity.Text) +(Convert.ToDecimal(txtFree.Text == "" ? "0" : txtFree.Text))), 3));
            if (Convert.ToInt64(blSub.NoofUnits) > 0)
                txtActqty.Text = Convert.ToString(((Convert.ToDecimal(txtQuantity.Text == "" ? "0" : txtQuantity.Text)) + (Convert.ToDecimal(txtFree.Text == "" ? "0" : txtFree.Text))) * (Convert.ToDecimal(blunit.NoofUnits)) * (Convert.ToDecimal(blSub.NoofUnits)));
            else
                txtActqty.Text = Convert.ToString(((Convert.ToDecimal(txtQuantity.Text == "" ? "0" : txtQuantity.Text)) + (Convert.ToDecimal(txtFree.Text == "" ? "0" : txtFree.Text))) * (Convert.ToDecimal(blunit.NoofUnits)));

            hfSubUnitQuantity.Value = Convert.ToString(blSub.NoofUnits);
            hfUnitQuantity.Value = Convert.ToString(blunit.NoofUnits);
            blunit = null;

        }

    }

    protected void CalculateLessQuantity()
    {
        if (ddlUnit.SelectedIndex > 0)
        {
            BlUnit blunit = new BlUnit();
            blunit.MasterId = Convert.ToInt64(ddlUnit.SelectedValue);
            blunit.FK_Client = Convert.ToInt64(hfClient.Value);
            blunit.SelectData();

            //txtLessQuantity.Text = Convert.ToString(Math.Round((Convert.ToDecimal(blunit.NoofUnits) * Convert.ToDecimal(txtQuantity.Text == "" ? "0" : txtQuantity.Text) - (Convert.ToDecimal(txtActqty.Text == "" ? "0" : txtActqty.Text)) + (Convert.ToDecimal(txtFree.Text == "" ? "0" : txtFree.Text))), 2));
            // txtCheckActQuantity.Text = Convert.ToString(Math.Round((Convert.ToDecimal(blunit.NoofUnits) * Convert.ToDecimal(txtQuantity.Text == "" ? "0" : txtQuantity.Text)), 2));
            // txtLessQuantity.Text = Convert.ToString(Math.Round(Convert.ToDecimal(txtCheckActQuantity.Text) - Convert.ToDecimal(txtActqty.Text), 2));
            blunit = null;
        }

    }

    protected void btnHiddenMaterialRecieptItems_Click(object sender, EventArgs e)
    {
        try
        {
            //
            GridFill();

            ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "ItemClear();", true);

            hfItemId.Value = "";
            txtInvoiceNo.Focus();

            divAdd.Style.Add("display", "block");
            divView.Style.Add("display", "None");
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "toastr.warning('" + ex.Message + "')", true);
        }
    }
    protected void btnInvoiceNumber_Click(object sender, EventArgs e)
    {
        try
        {
            //
            GridFill();

            ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "ItemClear();", true);
            hfItemId.Value = "";
            if (hfItemId.Value == "")
            {
                // gvItems.Enabled = true;

            }


            txtInvoiceDate.Focus();
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "toastr.warning('" + ex.Message + "')", true);
        }

    }
    protected void btnStoreMaterialRecieptSelect_Click(object sender, EventArgs e)
    {
        try
        {
            //
            GridFill();
            //  txtBatch.Focus();

            ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "ItemClear();", true);
            hfItemId.Value = "";
            if (hfItemId.Value == "")
            {
                //  gvItems.Enabled = true;

            }
            //DivPreviousPurchase.Visible = false;
            ddlCategory.Focus();
            // ddlRecieptNo.Focus();

        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "toastr.warning('" + ex.Message + "')", true);
        }
    }

    protected void gvItems_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        try
        {


            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                Label lbl = (Label)e.Row.FindControl("lblgvSLNO");
                lbl.Text = Convert.ToString(e.Row.RowIndex + 1);

            }

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                var IsFreeItem = Convert.ToBoolean(DataBinder.Eval(e.Row.DataItem, "IsFreeItem"));
                if ((DataBinder.Eval(e.Row.DataItem, "Item").ToString()) == "")//check zero
                {
                    e.Row.BackColor = System.Drawing.Color.IndianRed;
                    ViewState["RedLine"] = "true";
                    Label lb = new Label();

                }


                if (Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "Quantity").ToString()) == 0)//check zero
                {
                    e.Row.BackColor = System.Drawing.Color.IndianRed;
                    ViewState["RedLine"] = "true";
                    Label lb = new Label();

                }
                if (Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "MRP").ToString()) == 0)//check zero
                {
                    e.Row.BackColor = System.Drawing.Color.IndianRed;
                    ViewState["RedLine"] = "true";
                }
                if (Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "PurchaseRate").ToString()) == 0 && !IsFreeItem)//check zero
                {
                    e.Row.BackColor = System.Drawing.Color.IndianRed;
                    ViewState["RedLine"] = "true";
                }





            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "toastr.warning('" + ex.Message + "')", true);
        }
    }
    protected void btnClearAdd_Click(object sender, EventArgs e)
    {
        try
        {
            //
            hfItemId.Value = "";
            if (hfItemId.Value == "")
            {
                gvItems.Enabled = true;

            }
            lblQuotation.Text = "";
            Enbletextbox(true);
            btnS.Text = "PTax";
            //CurrentRefNo();
            HfItemsID.Value = "0";
            ViewState["GridDataPurchase"] = null;
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "toastr.warning('" + ex.Message + "')", true);
        }

    }
    protected void LoadPurchaseBillNumber()
    {


        txtInvoiceDate.Text = hfInvicedate.Value;

        invDate.EndDate = DateTime.Now;
        txtpopupRoundOff.Text = txtRoundOff.Text;
        txtpopupPurchaseAmoount.Text = txtBillNetAmount.Text;
        txtpopupNetPayAmount.Text = txtBillNetAmount.Text;
        txtpopupBalanceAmount.Text = txtBillNetAmount.Text;
        txtpopupDiscountAmount.Text = "0";
        txtpopupDiscountPer.Text = "0";
        txtpopupAdvanceAmount.Text = "0";
        txtpopupApproveBillNo.Text = "0";

        DivpopupAdvance.Attributes.Add("Style", "Display:None;");

    }

    protected void btnApprove_Click(object sender, EventArgs e)
    {
        try
        {
            //
            if (gvItems.Rows.Count > 0)
            {
                DataTable dtbl = (DataTable)ViewState["TempTable"];
                if (dtbl.Rows.Count > 0)
                {
                    int NewMr = 1;

                    for (int i = 0; i < dtbl.Rows.Count; i++)
                    {
                        if (dtbl.Rows[i]["ID_Purchase"].ToString() == "0")
                        {
                            NewMr = 0;
                            continue;
                        }
                        else
                        {
                            NewMr = 1;

                        }
                    }

                    if (NewMr == 0 || hfApproveOk.Value == "1")
                    {
                        ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "toastr.warning('First need to Submit the purchase')", true);

                    }
                    else
                    {
                        try
                        {
                            ddlpopupbillType.SelectedIndex = 0;
                            ddlPaymentMode.SelectedIndex = 0;
                            //ddlBank.SelectedIndex = 0;
                            //  ddlBankBranch.SelectedIndex = 0;
                        }
                        catch
                        {
                        }
                        txtpopupTax.Text = "0";
                        txtpopupChequeNo.Text = "";
                        txtpopupDiscountPer.Text = "0";
                        txtpopupNetPayAmount.Text = "0";
                        txtpopupRemarks.Text = "";
                        txtpopupDiscountAmount.Text = "0";
                        txtpopupBalanceAmount.Text = "0";
                        chkCredit.Checked = false;
                        LoadPurchaseBillNumber();
                        CheckForPayment();
                        MpeApprove.Show();

                    }

                }




            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "toastr.warning('No Items Present to Purchase')", true);
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "toastr.warning('" + ex.Message + "')", true);
        }
    }

    protected void btnmppPurchaseApproveDone_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            DateTime billDate;
            if (!DateTime.TryParse(txtpopupBillDate.Text.Trim(), out billDate))
            {
                ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script",
                    "toastr.error('Invalid Bill Date.')", true);
                MpeApprove.Show();
                return;
            }

            DateTime today = DateTime.Today;

            // Rule 1 — No future dates
            if (billDate.Date > today)
            {
                ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script",
                    "toastr.error('Bill date cannot be a future date.')", true);
                MpeApprove.Show();
                return;
            }

            // Rule 2 — Must be within current financial year
            int finYear = (today.Month >= 4) ? today.Year : today.Year - 1;
            DateTime finYearStart = new DateTime(finYear, 4, 1);

            if (billDate.Date < finYearStart.Date)
            {
                ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script",
                    "toastr.error('Bill date cannot be from a previous financial year. " +
                    "Earliest allowed date is " + finYearStart.ToString("dd/MM/yyyy") + ".')", true);
                MpeApprove.Show();
                return;
            }

            // Rule 3 — Bill date must not be earlier than Invoice date
            DateTime invoiceDate;
            if (DateTime.TryParse(txtInvoiceDate.Text.Trim(), out invoiceDate) &&
                billDate.Date < invoiceDate.Date)
            {
                ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script",
                    "toastr.error('Bill date cannot be earlier than the Invoice date (" +
                    invoiceDate.ToString("dd/MM/yyyy") + ").')", true);
                MpeApprove.Show();
                return;
            }

            BlPurchaseApprove blpurchaseapprove = new BlPurchaseApprove();
            blpurchaseapprove.BillDate = Convert.ToDateTime(txtpopupBillDate.Text);
            blpurchaseapprove.MasterId = Convert.ToInt64(hfID_Purchase.Value);
            blpurchaseapprove.FK_BillType = Convert.ToInt64(ddlpopupbillType.SelectedValue.ToString());
            blpurchaseapprove.FK_Supplier = Convert.ToInt64(ddlSupplier.SelectedValue.ToString());
            blpurchaseapprove.BillNo = Convert.ToInt64(txtpopupApproveBillNo.Text.ToString());
            blpurchaseapprove.PurchaseAmt = Convert.ToDecimal(txtpopupPurchaseAmoount.Text.ToString());
            blpurchaseapprove.RoundOff = Convert.ToDecimal(txtpopupRoundOff.Text.ToString());
            blpurchaseapprove.Advance = Convert.ToDecimal(txtpopupAdvanceAmount.Text.ToString() == "" ? "0" : txtpopupAdvanceAmount.Text.ToString());
            blpurchaseapprove.Credit = chkCredit.Checked;
            blpurchaseapprove.NetAmount = Convert.ToDecimal(txtpopupNetPayAmount.Text.ToString() == "" ? "0" : txtpopupNetPayAmount.Text.ToString());
            blpurchaseapprove.DiscAmt = Convert.ToDecimal(txtpopupDiscountAmount.Text.ToString() == "" ? "0" : txtpopupDiscountAmount.Text.ToString());
            blpurchaseapprove.DiscPer = Convert.ToDecimal(txtpopupDiscountPer.Text.ToString() == "" ? "0" : txtpopupDiscountPer.Text.ToString());

            blpurchaseapprove.PaymentMode = Convert.ToInt16(ddlPaymentMode.SelectedValue.ToString());
            blpurchaseapprove.State = 1;
            if (ddlPaymentMode.SelectedValue.ToString() == "1")
            {

                blpurchaseapprove.FK_Accounthead = Convert.ToInt64(0);
                blpurchaseapprove.FK_AccountheadSub = Convert.ToInt64(0);
                blpurchaseapprove.FK_Bank = Convert.ToInt64(0);
                blpurchaseapprove.FK_BankBranch = Convert.ToInt64(0);
                blpurchaseapprove.Chequeno = Convert.ToString("");
                blpurchaseapprove.ChequeDate = Convert.ToDateTime(txtpopupChequeDate.Text);
            }
            else if (ddlPaymentMode.SelectedValue.ToString() == "2")
            {
                blpurchaseapprove.FK_Bank = Convert.ToInt64(ddlBank.SelectedValue.ToString());
                blpurchaseapprove.FK_BankBranch = Convert.ToInt64(ddlBankBranch.SelectedValue.ToString());
                blpurchaseapprove.Chequeno = Convert.ToString(txtpopupChequeNo.Text.ToString());
                blpurchaseapprove.ChequeDate = Convert.ToDateTime(txtpopupChequeDate.Text);
                blpurchaseapprove.FK_Accounthead = Convert.ToInt64(ddlBank.SelectedValue.ToString());
                blpurchaseapprove.FK_AccountheadSub = Convert.ToInt64(ddlBankBranch.SelectedValue.ToString());

            }
            else if (ddlPaymentMode.SelectedValue.ToString() == "3")
            {

                blpurchaseapprove.Chequeno = Convert.ToString("");
                blpurchaseapprove.ChequeDate = Convert.ToDateTime(txtpopupChequeDate.Text);
                blpurchaseapprove.FK_Bank = Convert.ToInt64(0);
                blpurchaseapprove.FK_BankBranch = Convert.ToInt64(0);
                blpurchaseapprove.FK_Accounthead = Convert.ToInt64(ddlAcchead.SelectedValue.ToString());
                blpurchaseapprove.FK_AccountheadSub = Convert.ToInt64(ddlAccSubHead.SelectedValue.ToString());

            }



            blpurchaseapprove.Remarks = Convert.ToString(txtpopupRemarks.Text.ToString());
            blpurchaseapprove.EnteredOn = Convert.ToDateTime(DateTime.Now);
            blpurchaseapprove.FK_Client = Convert.ToInt64(hfClient.Value);
            blpurchaseapprove.UserCode = hfUser.Value;

            long statuscode = 0; string statusMsg = "";
            var dtstatus = blpurchaseapprove.InsertData();
            if (dtstatus.Rows.Count > 0)
            {
                statuscode = Convert.ToInt64(dtstatus.Rows[0][0]);
                statusMsg = dtstatus.Rows[0][1].ToString();
                if (statuscode > 0)
                {
                    ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "toastr.success('" + statusMsg + "')", true);


                    ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "ClearAdd();", true);
                    ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "ClearAdd();", true);
                    Clear();

                    MpeApprove.Hide(); MpeApprove.Hide();
                    GridFillPurchase(1);

                    liRefNo.Style.Add("display", "none");
                }
                else
                {
                    MpeApprove.Hide();
                    MpeApprove.Hide();
                    ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "toastr.error('Can not Approve, " + statusMsg + "')", true);

                }


            }

            //long status = 0;
            //status = blpurchaseapprove.InsertData();
            //if (status > 0)
            //{
            //    ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "toastr.success('Purchase Approved successfully')", true);


            //    ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "ClearAdd();", true);
            //    ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "ClearAdd();", true);
            //    Clear();

            //    MpeApprove.Hide();
            //    GridFillPurchase(1);

            //    liRefNo.Style.Add("display", "none");
            //}

            //else if (status == -2)
            //{
            //    MpeApprove.Hide();
            //    ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "toastr.error('Cannot Approve, Purchase Discount Account Head is not Given Please Check')", true);
            //}
            //else if (status == -3)
            //{
            //    MpeApprove.Hide();
            //    ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "toastr.error('Cannot Approve, Purchase VAT Account Head is not Given, Please Check')", true);
            //}
            //else if (status == -4)
            //{
            //    MpeApprove.Hide();
            //    ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "toastr.error('Cannot Approve, Purchase CGST Account Head is not Given, Please Check')", true);
            //}
            //else if (status == -5)
            //{
            //    MpeApprove.Hide();
            //    ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "toastr.error('Cannot Approve, Purchase SGST Account Head is not Given, Please Check')", true);
            //}
            //else if (status == -6)
            //{
            //    MpeApprove.Hide();
            //    ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "toastr.error('Cannot Approve, Purchase IGST Account Head is not Given, Please Check')", true);
            //}
            //else if (status == -7)
            //{
            //    MpeApprove.Hide();
            //    ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "toastr.error('Cannot Approve, Purchase CESS Account Head is not Given, Please Check')", true);
            //}
            //else if (status == -8)
            //{
            //    MpeApprove.Hide();
            //    ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "toastr.error('Cannot Approve, Purchase Fright Account Head is not Given, Please Check')", true);
            //}
            //else if (status == -9)
            //{
            //    MpeApprove.Hide();
            //    ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "toastr.error('Cannot Approve, Purchase HeadLoading Account Head is not Given, Please Check')", true);
            //}
            //else if (status == -10)
            //{
            //    MpeApprove.Hide();
            //    ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "toastr.error('Cannot Approve, Purchase HeadUnLoading Account Head is not Given, Please Check')", true);
            //}
            //else if (status == -11)
            //{
            //    MpeApprove.Hide();
            //    ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "toastr.error('Cannot Approve, Purchase Display Account Head is not Given, Please Check')", true);
            //}
            //else if (status == -12)
            //{
            //    MpeApprove.Hide();
            //    ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "toastr.error('Cannot Approve, Purchase Others Account Head is not Given, Please Check')", true);
            //}
            //else if (status == -13)
            //{
            //    MpeApprove.Hide();
            //    ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "toastr.error('Cannot Approve, Purchase Round Account Head is not Given, Please Check')", true);
            //}
            //else if (status == -14)
            //{
            //    MpeApprove.Hide();
            //    ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "toastr.error('Cannot Approve, Purchase Supplier Account Head is not Given, Please Check')", true);
            //}
            //else
            //{
            //    MpeApprove.Hide();
            //    ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "toastr.error('Cannot Approve')", true);

            //}
            blpurchaseapprove = null;

        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "toastr.warning('" + ex.Message + "')", true);
        }

    }
    protected void PaymentModeVisiblity()
    {

        if (chkCredit.Checked == true)
        {
            DivpopuppaymentMode.Attributes.Add("Style", "Display:None;");
            DivpopupBank.Visible = false;
            DivpopupBankBranch.Visible = false;
            DivpopupchequeNo.Visible = false;
            DivpopupchequeDate.Visible = false;
            DivpopupAccHead.Visible = false;
            DivpopupAccSubHead.Visible = false;
            //  DivpopupAdvance.Visible = true;
            DivpopupAdvance.Attributes.Add("Style", "Display:Block;");
            MpeApprove.Show();
            // txtCardAmount.Text = "0";
        }
        else
        {
            DivpopuppaymentMode.Attributes.Add("Style", "Display:Block;");
            ddlPaymentMode.SelectedIndex = 0;
            DivpopupBank.Visible = false;
            DivpopupBankBranch.Visible = false;
            DivpopupchequeNo.Visible = false;
            DivpopupchequeDate.Visible = false;
            DivpopupAccHead.Visible = false;
            DivpopupAccSubHead.Visible = false;
            // DivpopupAdvance.Visible = false;
            DivpopupAdvance.Attributes.Add("Style", "Display:None;");
            txtpopupAdvanceAmount.Text = "0";

            MpeApprove.Show();

        }

    }
    protected void CheckForPayment()
    {
        if (ddlPaymentMode.SelectedValue.ToString() == "1")
        {
            DivpopupBank.Visible = false;
            DivpopupBankBranch.Visible = false;
            DivpopupchequeNo.Visible = false;
            DivpopupchequeDate.Visible = false;
            DivpopupAccHead.Visible = false;
            DivpopupAccSubHead.Visible = false;


            MpeApprove.Show();
            // txtCardAmount.Text = "0";
        }
        else if (ddlPaymentMode.SelectedValue.ToString() == "2")
        {
            DivpopupBank.Visible = true;
            DivpopupBankBranch.Visible = true;
            DivpopupchequeNo.Visible = true;
            DivpopupchequeDate.Visible = true;
            DivpopupAccHead.Visible = false;
            DivpopupAccSubHead.Visible = false;

            MpeApprove.Show();
        }
        else if (ddlPaymentMode.SelectedValue.ToString() == "3")
        {
            DivpopupBank.Visible = false;
            DivpopupBankBranch.Visible = false;
            DivpopupchequeNo.Visible = false;
            DivpopupchequeDate.Visible = false;
            DivpopupAccHead.Visible = true;
            DivpopupAccSubHead.Visible = true;

            MpeApprove.Show();
        }

        else
        {
            DivpopupBank.Visible = false;
            DivpopupBankBranch.Visible = false;
            DivpopupchequeNo.Visible = false;
            DivpopupchequeDate.Visible = false;
            DivpopupAccHead.Visible = false;
            DivpopupAccSubHead.Visible = false;

            MpeApprove.Show();
        }
    }


    protected void ddlPaymentMode_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            //
            CheckForPayment();
            MpeApprove.Show();
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "toastr.warning('" + ex.Message + "')", true);
        }
    }
    protected void chkCredit_CheckedChanged(object sender, EventArgs e)
    {
        try
        {
            //

            PaymentModeVisiblity();

            ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "CalculatePopUpBalanceAndBillAmount();", true);
            ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "CalculatePopUpBalanceAndBillAmount();", true);

            MpeApprove.Show();
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "toastr.warning('" + ex.Message + "')", true);
        }
    }
    protected void ddlAcchead_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            //
            if (ddlAcchead.SelectedIndex > 0)
            {
                Populate populate = new Populate();
                populate.FillDropdownList(ddlAccSubHead, "AccountHeadSub", "HeadName", "ID_AccountHeadSub", "ID_AccountHeadSub", "FK_Client=1  AND FK_AccountHead=" + ddlAcchead.SelectedValue.ToString(), "");
                populate = null;

            }
            MpeApprove.Show();
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "toastr.warning('" + ex.Message + "')", true);
        }
    }
    protected void ddlBank_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            //
            if (ddlBank.SelectedIndex > 0)
            {
                Populate populate = new Populate();
                populate.FillDropdownList(ddlBankBranch, "BankBranch", "ShortName", "ID_BankBranch", "ID_BankBranch", "FK_Client= " + hfClient.Value.Split(',')[0] + " AND Active='True' AND FK_Bank=" + ddlBank.SelectedValue.ToString(), "");
                populate = null;

            }
            MpeApprove.Show();
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "toastr.warning('" + ex.Message + "')", true);
        }
    }
    protected void ddlpopupbillType_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            //
            BlPurchaseApprove blpurchaseapprove = new BlPurchaseApprove();

            blpurchaseapprove.FK_BillType = Convert.ToInt64(ddlpopupbillType.SelectedValue.ToString());

            blpurchaseapprove.FK_Client = Convert.ToInt64(hfClient.Value);
            if (ddlStore.SelectedIndex > 0)
            {
                blpurchaseapprove.FK_Store = Convert.ToInt64(ddlStore.SelectedValue.ToString());

            }
            else
            {
                blpurchaseapprove.FK_Store = Convert.ToInt64(hfFk_Store.Value);
            }

            long status = 0;
            status = blpurchaseapprove.proGetPurchaseApproveBillNo();
            if (status > 0)
            {
                txtpopupApproveBillNo.Text = Convert.ToString(status);

            }

            else
            {
                txtpopupApproveBillNo.Text = "0";
            }
            blpurchaseapprove = null;


            MpeApprove.Show();
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "toastr.warning('" + ex.Message + "')", true);
        }
    }
    protected void txtpopupAdvanceAmount_TextChanged(object sender, EventArgs e)
    {
        try
        {

            //
            Decimal AdvnceAmnt = 0;
            AdvnceAmnt = Convert.ToDecimal(txtpopupAdvanceAmount.Text.ToString().Trim() == "" ? "0" : txtpopupAdvanceAmount.Text.ToString().Trim());
            if (chkCredit.Checked == true)
            {
                if (AdvnceAmnt > 0)
                {

                    DivpopuppaymentMode.Attributes.Add("Style", "Display:Block;");
                }
                else if (AdvnceAmnt == 0)
                {
                    DivpopuppaymentMode.Attributes.Add("Style", "Display:None;");
                }


            }
            else
            {


            }
            MpeApprove.Show();

        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "toastr.warning('" + ex.Message + "')", true);
        }
    }

    protected void LoadItemFromPurchaseOrder()
    {
        DataTable dtbl = new DataTable();
        BlMaterialReciept blMaterialReciept = new BlMaterialReciept();
        blMaterialReciept.FK_PurchaseOrder = Convert.ToInt64(ddlPurchaseOrder.SelectedValue.ToString());
        blMaterialReciept.FK_Client = Convert.ToInt64(hfClient.Value);
        dtbl = blMaterialReciept.GetPartialMaterialRecieptItems();
        if (dtbl.Rows.Count > 0)
        {

            for (int i = 0; i < dtbl.Rows.Count; i++)
            {
                decimal a = Convert.ToDecimal(dtbl.Rows[i]["Quantity"]);
                if (a <= 0)
                {
                    DataRow dr = dtbl.Rows[i];
                    dr.Delete();


                }
            }
            dtbl.AcceptChanges();



            ViewState["TempTable"] = dtbl;
            gvItems.DataSource = dtbl;
            gvItems.DataBind();



        }
        else
        {


            ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "alertify.alert('Material Receipt already done')", true);
        }


    }
    protected void ddlPurchaseOrder_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            //
            if (ddlPurchaseOrder.SelectedIndex > 0)
            {
                LoadItemFromPurchaseOrder();
                txtInvoiceNo.Focus();
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "toastr.warning('" + ex.Message + "')", true);
        }
    }

    public void PurchaseOrderFill(int pageIndex)
    {
        ClearPurchaseOrder();
        Populate Populate = new Populate();
        CommonPopup1.CallingFunction = "PurchaseOrderFill";
        string strName = ((TextBox)CommonPopup1.FindControl("txtName")).Text;
        string Refno = ((TextBox)CommonPopup1.FindControl("txtRefno")).Text;
        string searchCol = ((HiddenField)CommonPopup1.FindControl("hfColumn")).Value;
        Refno = Refno == string.Empty || Refno == null ? "0" : Refno;
        Populate.FillPopUpGrid((GridView)CommonPopup1.FindControl("gdvSearch"), "PurchaseOrder", "ID_PurchaseOrder as ID,PONo as Name", "ID_PurchaseOrder", "ID_PurchaseOrder", "FK_Client=" + hfClient.Value.Split(',')[0] + " AND FK_Supplier=" + Convert.ToString(ddlSupplier.SelectedValue.ToString()) + " AND (ID_PurchaseOrder =" + Refno + " OR 0=" + Refno + ")   AND Cancelled=0 and PONo ", strName, null);
        GridView gvNew = (GridView)CommonPopup1.FindControl("gdvSearch");
        gvNew.Columns[1].HeaderText = "Order No";
        Populate = null;
        if (txtPurchaseOrderNo.Text == string.Empty)
        {
            lblPopupHeader.Text = "Purchase Order";
            mpCommon.Show();
        }
    }
    protected void PurchaseOrder()
    {
        try
        {

            ((TextBox)CommonPopup1.FindControl("txtName")).Text = txtPurchaseOrderNo.Text.Trim();
            ((TextBox)CommonPopup1.FindControl("txtRefno")).Text = txtPurchaseOrderNo.Text.Trim();
            ((HiddenField)CommonPopup1.FindControl("hfColumn")).Value = "Order No";
            PurchaseOrderFill(1);
            divAdd.Style.Add("display", "block");
            divView.Style.Add("display", "none");
        }
        catch (Exception ex)
        {
        }
    }

    private void ClearPurchaseOrder()
    {
        // txtPurchaseOrder.Text = "";
        txtPurchaseOrderNo.Text = "";

    }
    public void SetDetails(string strName, string strRefno)
    {
        if (CommonPopup1.CallingFunction == "PurchaseOrderFill")
        {
            txtPurchaseOrderNo.Text = strRefno;
            ddlPurchaseOrder.SelectedValue = txtPurchaseOrderNo.Text.ToString();
            if (ddlPurchaseOrder.SelectedIndex > 0)
            {
                LoadItemFromPurchaseOrder();
            }

            txtInvoiceNo.Focus();
        }
        else if (CommonPopup1.CallingFunction == "SupplierFill")
        {
            txtSupplierName.Text = strName;
            ddlSupplier.SelectedValue = strRefno.ToString();

            txtSupplierSearch.Text = strName;
            BlSupplier blSupplier = new BlSupplier();
            blSupplier.MasterId = Convert.ToInt64(strRefno);
            blSupplier.SelectData();
            txtGSTIN.Text = blSupplier.TinNo;
            txtPurchaseOrderNo.Focus();

        }
        else if (CommonPopup1.CallingFunction == "SupplierSearchFill")
        {
            txtSupplierSearch.Text = strName;
        }
        else
        {
            txtPurchaseOrder.Text = strRefno;


        }
    }
    //protected 
    protected void imgbtnPurchaseOrder_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            // Validate supplier first
            if (ddlSupplier.SelectedIndex <= 0)
            {
                ScriptManager.RegisterStartupScript(
                    Page,
                    typeof(Page),
                    "Script",
                    "toastr.warning('Please select supplier first');",
                    true);

                ddlSupplier.Focus();
                return;
            }

            // Validate Purchase Order textbox
            //if (string.IsNullOrWhiteSpace(txtPurchaseOrderNo.Text) ||
            //    txtPurchaseOrderNo.Text.Trim() == "0")
            //{
            //    ScriptManager.RegisterStartupScript(
            //        Page,
            //        typeof(Page),
            //        "Script",
            //        "toastr.warning('Please enter Purchase Order No');",
            //        true);

            //    txtPurchaseOrderNo.Focus();
            //    return;
            //}

            // Continue only if validation passed
            PurchaseOrder();
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(
                Page,
                typeof(Page),
                "Script",
                "toastr.warning('" + ex.Message.Replace("'", "") + "')",
                true);
        }
    }
    protected void btnLoadPurchaseOrder_Click(object sender, EventArgs e)
    {
        try
        {


            txtInvoiceNo.Focus();
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "toastr.warning('" + ex.Message + "')", true);
        }
    }

    protected void ddlUnit_SelectedIndexChanged(object sender, EventArgs e)
    {

        try
        {

            btnHiddenActualQuantityfromUnit_Click(sender, e);
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "toastr.warning('" + ex.Message + "')", true);
        }
    }
    protected void btnHiddenActullessQuantity_Click(object sender, EventArgs e)
    {
        try
        {

            CalculateActualQuantity();
            txtFree.Focus();




        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "toastr.warning('" + ex.Message + "')", true);
        }
    }
    protected void btnHiddenlessQuantity_Click(object sender, EventArgs e)
    {
        try
        {


        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "toastr.warning('" + ex.Message + "')", true);
        }
    }
    protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
    {
        try
        {

            //

            ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "SearchClear();", true);
            ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "SearchClear();", true);
            divAdd.Style.Add("display", "none");
            divView.Style.Add("display", "block");
            GridFillPurchase(1);
            MpeApprove.Hide();
            txtFromDateSearch.Focus();



        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "toastr.warning('" + ex.Message + "')", true);
        }
    }
    protected void GridFillPurchase(int currentPage)
    {
        BlPurchase blpurchase = new BlPurchase();
        DataTable dtbl = new DataTable();
        blpurchase.FK_Store = Convert.ToInt64(ddlStoreSearch.SelectedValue);
        blpurchase.FK_Supplier = Convert.ToInt64(ddlSupplierSearch.SelectedValue);
        blpurchase.FK_Client = Convert.ToInt64(hfClient.Value);
        blpurchase.UserCode = hfUser.Value;
        blpurchase.Approved = Convert.ToInt16(ddlStatusApproveSearch.SelectedValue);
        blpurchase.CashCredit = Convert.ToInt16(ddlPaymentModeCashCredit.SelectedValue);

        blpurchase.PageIndex = Convert.ToInt16(currentPage);
        blpurchase.FromDate = Convert.ToDateTime(txtFromDateSearch.Text == "" ? "" + DateTime.Now.Date + "" : txtFromDateSearch.Text);// DateTime.ParseExact(txtFromDateSearch.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
        blpurchase.ToDate = Convert.ToDateTime(txtToDateSearch.Text == "" ? "" + DateTime.Now.Date + "" : txtToDateSearch.Text);// DateTime.ParseExact(txtToDateSearch.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
        blpurchase.SupplierName = txtSupplierSearch.Text.Trim();
        blpurchase.RefNo = txtRefnoSrch.Text.Trim();
        dtbl = blpurchase.SelectAllDataForPurchase();
        gvPurchase.DataSource = dtbl;
        gvPurchase.DataBind();
        blpurchase = null;
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
        GridFillPurchase(pageIndex);
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        try
        {

            GridFillPurchase(1);
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "toastr.warning('" + ex.Message + "')", true);
        }
    }
    protected void gvPurchase_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            //
            BlPurchase blPurchase = new BlPurchase();
            long status = 0;
            string Sts = "closed";
            //if (e.CommandName == "ApprovedStatus")
            //{
            //    string confirmValue = Request.Form["confirm_value"];
            //    if (confirmValue.Length > 3)
            //        confirmValue = confirmValue.Substring(confirmValue.Length - 3, 3);
            //    if (confirmValue == "Yes")
            //    {

            //        GridViewRow row = (GridViewRow)((Button)e.CommandSource).NamingContainer;
            //        int index = row.RowIndex;
            //        string PurchaseId = gvPurchase.DataKeys[index]["ID_Purchase"].ToString();
            //        hfFk_Store.Value =gvPurchase.DataKeys[index]["FK_Store"].ToString();
            //        if (PurchaseId != null || PurchaseId != "0")
            //        {
            //            try
            //            {
            //                ddlpopupbillType.SelectedIndex = 0;
            //                ddlPaymentMode.SelectedIndex = 0;
            //               // ddlBank.SelectedIndex = 0;
            //              //  ddlBankBranch.SelectedIndex = 0;
            //            }
            //            catch
            //            { 
            //            }
            //            txtpopupTax.Text = "0";
            //            txtpopupChequeNo.Text = "";
            //            txtpopupDiscountPer.Text = "0";
            //            txtpopupNetPayAmount.Text = "0";
            //            txtpopupRemarks.Text = "";
            //            txtpopupDiscountAmount.Text = "0";
            //            txtpopupBalanceAmount.Text = "0";
            //            chkCredit.Checked = false;
            //            blPurchase.MasterId = Convert.ToInt64(PurchaseId);
            //            hfID_Purchase.Value = PurchaseId;
            //            DataTable dtbl = new DataTable();
            //            DataTable dtblItmsValue = new DataTable();
            //            blPurchase.FK_Client = Convert.ToInt64(hfClient.Value);
            //            blPurchase.UserCode = hfUser.Value;
            //           // blPurchase.Approved = Convert.ToInt16(ddlStatusApproveSearch.SelectedValue);

            //            blPurchase.FromDate = Convert.ToDateTime(txtFromDateSearch.Text == "" ? "" + DateTime.Now.Date + "" : txtFromDateSearch.Text);// DateTime.ParseExact(txtFromDateSearch.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            //            blPurchase.ToDate = Convert.ToDateTime(txtToDateSearch.Text == "" ? "" + DateTime.Now.Date + "" : txtToDateSearch.Text);// DateTime.ParseExact(txtToDateSearch.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            //            dtbl = blPurchase.proPurchaseMaterialsPurchaseItemsForApprove();
            //            txtRoundOff.Text= dtbl.Rows[0]["RoundOff"].ToString();
            //            txtTotalAmount.Text = dtbl.Rows[0]["PurchaseAmount"].ToString();
            //          //  txtBillNetAmount.Text = dtbl.Rows[0]["NetAmount"].ToString();
            //          //  txtBillNetAmount.Text = dtbl.Rows[0]["NetAmount"].ToString();



            //            LoadPurchaseBillNumber();
            //            CheckForPayment();
            //          //  MpeApprove.Show();

            //            //blPurchase.isClosed = "";
            //            //blPurchaseOrder.FromDate = DateTime.ParseExact("01/01/1990", "dd/MM/yyyy", CultureInfo.InvariantCulture);
            //            //blPurchaseOrder.ToDate = DateTime.ParseExact("01/01/9999", "dd/MM/yyyy", CultureInfo.InvariantCulture);
            //            //blPurchaseOrder.DeliveryType = "";
            //            //blPurchaseOrder.FK_Client = Convert.ToInt64(hfClient.Value);

            //            //if (blPurchaseOrder.MasterId > 0)
            //            //{

            //            //    blPurchaseOrder.SelectData();
            //            //    if (blPurchaseOrder.Closed == true)
            //            //    {
            //            //        blPurchaseOrder.Closed = false;
            //            //        blPurchaseOrder.ClosedUser = "";
            //            //        blPurchaseOrder.EnteredOn = DateTime.Now;
            //            //        blPurchaseOrder.UserCode = hfUser.Value;
            //            //        Sts = "Opened";

            //            //    }
            //            //    else if (blPurchaseOrder.Closed == false)
            //            //    {
            //            //        blPurchaseOrder.Closed = true;
            //            //        blPurchaseOrder.ClosedUser = hfUser.Value;
            //            //        blPurchaseOrder.EnteredOn = DateTime.Now;
            //            //        blPurchaseOrder.UserCode = hfUser.Value;
            //            //        Sts = "Closed";
            //            //    }


            //            //    status = blPurchase.UpdatePurchaseOrderStatusOnly();
            //            //    if (status > 0)
            //            //    {
            //            //        GridFillPurchase(1);
            //            //        ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "toastr.success('Status " + Sts + " successfully');", true);

            //            //    }
            //            //    else if (status == -7)
            //            //    {

            //            //        ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "toastr.error('Cannot update Status, Purchase already done..');", true);

            //            //    }

            //            //    else
            //            //    {
            //            //        ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "toastr.error('Cannot update Status')", true);

            //            //    }
            //            //}

            //        }
            //    }
            //} 
            if (e.CommandName == "Print")
            {
                //string confirmValue = Request.Form["confirm_value"];
                //  if (confirmValue.Length > 3)
                //      confirmValue = confirmValue.Substring(confirmValue.Length - 3, 3);
                //  if (confirmValue == "Yes")
                //  {

                GridViewRow row = (GridViewRow)((ImageButton)e.CommandSource).NamingContainer;
                int index = row.RowIndex;
                string PurchaseId = gvPurchase.DataKeys[index]["ID_Purchase"].ToString();

                string popupreport = "<script> window.open('PurchaseReport.aspx?PurchaseId=" + PurchaseId + "&DatePass=" + hfDate.Value.ToString() + "', 'self', 'left=200,top=50,width=900,height=680,status=0,resizable=0,fullscreen=0,scrollbars=1');</script>";
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "Success", popupreport, false);

                // }

            }
            blPurchase = null;


        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "toastr.warning('" + ex.Message + "')", true);
        }
    }
    protected void gvPurchase_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            //
            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                Button btnapprove = (Button)e.Row.FindControl("btnApprovedStatus");
                ImageButton btnDelete = (ImageButton)e.Row.FindControl("ImageButton2");
                ImageButton btnEdit = (ImageButton)e.Row.FindControl("ImageButton1784");
                // give property id of button form template field
                if (btnapprove.Text == "Approved")
                {
                    btnapprove.Enabled = false;
                    btnDelete.Enabled = false;
                    // btnDelete.Visible = false;
                    btnEdit.Enabled = true;
                    btnapprove.BackColor = System.Drawing.Color.DarkGreen;//true means enable else you may set false to disable button 
                }
                else
                {
                    btnapprove.BackColor = System.Drawing.Color.DarkRed;
                    btnEdit.Enabled = true;
                    btnapprove.Enabled = false;
                    // btnDelete.Visible = true;
                    btnDelete.Enabled = true;
                }

            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "toastr.warning('" + ex.Message + "')", true);
        }
    }
    protected void gvPurchase_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {

        try
        {

            string confirmValue = Request.Form["confirm_value"];
            if (confirmValue.Length > 3)
                confirmValue = confirmValue.Substring(confirmValue.Length - 3, 3);
            if (confirmValue == "Yes")
            {
                long status = 0;
                BlPurchase blpurchase = new BlPurchase();
                blpurchase.MasterId = Convert.ToInt64(gvPurchase.DataKeys[e.RowIndex]["ID_Purchase"].ToString());
                hfMasterID.Value = (gvPurchase.DataKeys[e.RowIndex]["ID_Purchase"].ToString());
                blpurchase.FK_Client = Convert.ToInt64(hfClient.Value);
                blpurchase.UserCode = hfUser.Value;
                blpurchase = null;
            }
            else
            {
                mpDelete.Hide();
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "toastr.warning('" + ex.Message + "')", true);
        }


    }
    protected void gvPurchase_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        try
        {
            hfPurchseApprv.Value = "";
            BlPurchase blpurchase = new BlPurchase();
            DataTable dtbl = new DataTable();
            blpurchase.MasterId = Convert.ToInt64(gvPurchase.DataKeys[e.NewSelectedIndex]["ID_Purchase"].ToString());
            ViewState["MasterIDTaxDetails"] = Convert.ToInt64(gvPurchase.DataKeys[e.NewSelectedIndex]["ID_Purchase"].ToString());
            blpurchase.FK_Client = Convert.ToInt64(hfClient.Value);
            blpurchase.Approved = 2;

            dtbl = blpurchase.SelectAllDataForPurchaseApproveEditView();

            hfPurchseApprv.Value = dtbl.Rows[0]["ApprovedStatus"].ToString();
            MpeApprove.Hide();
            btnClear_Click(sender, e);
            ddlStore.Enabled = false;

            ViewState["MasterIDTaxDetails"] = Convert.ToInt64(gvPurchase.DataKeys[e.NewSelectedIndex]["ID_Purchase"].ToString());


            liRefNo.Style.Add("display", "flex");
            if (hfIsUpdate.Value.ToLower() == "true")
            {

                ddlSupplier.SelectedValue = Convert.ToInt64(dtbl.Rows[0]["FK_Supplier"]).ToString();
                txtSupplierName.Text = ddlSupplier.SelectedItem.Text.ToString();

                txtPurchaseOrderNo.Text = Convert.ToInt64(dtbl.Rows[0]["PONo"]).ToString();
                txtInvoiceNo.Text = Convert.ToString(dtbl.Rows[0]["InvoiceNo"]).ToString();
                txtInvoiceDate.Text = Convert.ToDateTime(dtbl.Rows[0]["InvoiceDate"]).ToString();
                hfInvicedate.Value = Convert.ToDateTime(txtInvoiceDate.Text.ToString()).ToShortDateString();
                txtGSTIN.Text = Convert.ToString(dtbl.Rows[0]["GSTIN"]).ToString();
                chkInterState.Checked = Convert.ToBoolean(dtbl.Rows[0]["InterStatePurchase"]);
                ddlStore.SelectedValue = Convert.ToInt64(dtbl.Rows[0]["FK_Store"]).ToString();
                hdnbillnetAmt.Value = Convert.ToDecimal(dtbl.Rows[0]["NetAmount"]).ToString();
                hfPurchseApprv.Value = dtbl.Rows[0]["ApprovedStatus"].ToString();
                BindExpenseData();
                BindPurchaseTax();
                GridFill();
                lblRefno.Text = Convert.ToInt64(dtbl.Rows[0]["RefNo"]).ToString();
                txtRoundOff.Text = Convert.ToDecimal(dtbl.Rows[0]["RoundOff"]).ToString();
                //txtRoundOff.Text= Math.Round(Convert.ToDecimal(txtRoundOff.Text), 3).ToString();
                ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "ItemClear();", true);
                if (Convert.ToInt64(dtbl.Rows[0]["PONo"]) > 0)
                {
                    txtSupplierName.Enabled = false;
                    ImageButton3.Enabled = false;

                }
                else
                {
                    txtSupplierName.Enabled = true;
                    ImageButton3.Enabled = true;
                }
                hfItemId.Value = "";
                if (hfItemId.Value == "")
                {

                }

                txtInvoiceNo.Focus();
                string a = txtPurchaseOrder.Text;
                // ddlPurchaseOrder.Focus();
                btnShowData.Enabled = false;
                divAdd.Style.Add("display", "block");
                divView.Style.Add("display", "None");

            }

            blpurchase = null;
        }
        catch (Exception ex)
        {

            ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "toastr.warning('" + ex.Message + "')", true);
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



    public void SupplierFill(int pageIndex)
    {
        ClearSupplier();
        Populate Populate = new Populate();
        CommonPopup1.CallingFunction = "SupplierFill";
        string strName = ((TextBox)CommonPopup1.FindControl("txtName")).Text;
        string Refno = ((TextBox)CommonPopup1.FindControl("txtRefno")).Text;
        string searchCol = ((HiddenField)CommonPopup1.FindControl("hfColumn")).Value;
        Refno = Refno == string.Empty || Refno == null ? "0" : Refno;


        Populate.FillPopUpGrid((GridView)CommonPopup1.FindControl("gdvSearch"), "Supplier", "ID_Supplier as ID,SupplierName as Name", "ID_Supplier", "ID_Supplier", "FK_Client=" + hfClient.Value.Split(',')[0] + " AND  Active=1  AND Cancelled=0 and SupplierName ", strName, null);
        Populate = null;
        //    txtSubHeadRefno.Focus();
        if (txtSupplierName.Text == string.Empty)
        {
            lblPopupHeader.Text = "Supplier";
            mpCommon.Show();
        }
        txtPurchaseOrder.Focus();
    }
    protected void Supplier()
    {
        try
        {
            // ((TextBox)CommonPopup1.FindControl("txtName")).Text = ((TextBox)CommonPopup1.FindControl("txtName")).Text == "" ? txtSupplierName.Text.Trim() : ((TextBox)CommonPopup1.FindControl("txtName")).Text;
            ((TextBox)CommonPopup1.FindControl("txtName")).Text = txtSupplierName.Text.Trim();
            ((TextBox)CommonPopup1.FindControl("txtRefno")).Text = txtSupplierName.Text.Trim();
            ((HiddenField)CommonPopup1.FindControl("hfColumn")).Value = "Name";
            SupplierFill(1);
            divAdd.Style.Add("display", "block");
            divView.Style.Add("display", "none");
            txtPurchaseOrder.Focus();
        }
        catch (Exception ex)
        {
        }
    }

    private void ClearSupplier()
    {

        txtSupplierName.Text = "";

    }

    protected void imgbtnAdd_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            Populate populate = new Populate();
            populate.FillDropdownList(ddlStore, "Store", "StoName", "ID_Store", "StoName", " Active='True'  AND Cancelled=0 AND FK_Client= " + hfClient.Value.Split(',')[0], "");
            populate = null;
            ddlStore.SelectedValue = Session["mainstore"].ToString();
            ddlStore.Enabled = false;
            btnClear_Click(sender, e);
            ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "ShowAddDiv();", true);
            ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "ShowAddDiv();", true);
            divAdd.Style.Add("display", "block");
            divView.Style.Add("display", "none");
            ViewState["TempTable"] = null;
            ViewState["ExpenseDetails"] = null;
            txtSupplierName.Enabled = true;
            ImageButton3.Enabled = true;
            Enbletextbox(true);
            lblQuotation.Text = "";
            //CurrentRefNo();
            if (Convert.ToBoolean(hfIsAdd.Value) == false)
            {
                ((Label)CommonPopupPrivilege.FindControl("popuplabel")).Text = "User does not have permission to Add";
                mpPrivilege.Show();
            }
            liRefNo.Style.Add("display", "none");
        }
        catch (Exception ex)
        {
        }
    }
    protected void ImageButton4_Click(object sender, ImageClickEventArgs e)
    {
        try
        {

            SupplierSearch();
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "toastr.warning('" + ex.Message + "')", true);
        }
    }



    public void SuppliersearchFill(int pageIndex)
    {
        ClearSupplierSearch();
        Populate Populate = new Populate();
        CommonPopup1.CallingFunction = "SupplierSearchFill";
        string strName = ((TextBox)CommonPopup1.FindControl("txtName")).Text;
        string Refno = ((TextBox)CommonPopup1.FindControl("txtRefno")).Text;
        string searchCol = ((HiddenField)CommonPopup1.FindControl("hfColumn")).Value;
        Refno = Refno == string.Empty || Refno == null ? "0" : Refno;

        Populate.FillGrid((GridView)CommonPopup1.FindControl("gdvSearch"), "Supplier", "ID_Supplier as ID,Name as Name, ShortName  , Address , Mobile", "ID_Supplier", "", "FK_Client=" + hfClient.Value.Split(',')[0] + " AND  Active=1  ", searchCol + "  LIKE '" + strName + "%'", pageIndex, 10, "", (Repeater)CommonPopup1.FindControl("rptPager"), this.Page);

        Populate = null;

        if (txtSupplierSearch.Text == string.Empty)
        {
            lblPopupHeader.Text = "Supplier";
            mpCommon.Show();
        }
        txtSupplierSearch.Focus();
    }
    protected void SupplierSearch()
    {
        try
        {

            ((TextBox)CommonPopup1.FindControl("txtName")).Text = txtSupplierSearch.Text.Trim();
            ((TextBox)CommonPopup1.FindControl("txtRefno")).Text = txtSupplierSearch.Text.Trim();
            ((HiddenField)CommonPopup1.FindControl("hfColumn")).Value = "Name";
            SupplierFill(1);
            divAdd.Style.Add("display", "none");
            divView.Style.Add("display", "block");
            txtSupplierSearch.Focus();
        }
        catch (Exception ex)
        {
        }
    }

    private void ClearSupplierSearch()
    {

        txtSupplierSearch.Text = "";

    }
    protected void imgSearch_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            ClearSupplier();
            Populate Populate = new Populate();
            CommonPopup1.CallingFunction = "CallingFunction";
            string Refno = ((TextBox)CommonPopup1.FindControl("txtRefno")).Text;
            string searchCol = ((HiddenField)CommonPopup1.FindControl("hfColumn")).Value;
            Refno = Refno == string.Empty || Refno == null ? "0" : Refno;
            GridView gvNew = (GridView)CommonPopupGrid1.FindControl("gdvSearch");
            BlPurchase blpurchase = new BlPurchase();
            blpurchase.itemID = Convert.ToInt64(HfItemsID.Value.ToString());
            DataTable dt = blpurchase.SelectGriddata();
            gvNew.DataSource = dt;
            gvNew.DataBind();
            lblPopupH.Text = "Sales Tax";
            mpGrid.Show();
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "toastr.warning('" + ex.Message + "')", true);
        }

    }


    protected void Purchasetax()
    {
        try
        {

            ((TextBox)CommonPopup1.FindControl("txtName")).Text = txtSupplierName.Text.Trim();
            ((TextBox)CommonPopup1.FindControl("txtRefno")).Text = txtSupplierName.Text.Trim();
            ((HiddenField)CommonPopup1.FindControl("hfColumn")).Value = "Name";

            GridView gdvSearch1 = ((GridView)CommonPopup1.FindControl("gdvSearch"));

            DataTable _datatable = new DataTable();
            _datatable.Columns.Add("slno");
            _datatable.Columns.Add("ID_TaxType");
            _datatable.Columns.Add("TaxType");
            _datatable.Columns.Add("Percentage");

            foreach (GridViewRow row in gdvSearch1.Rows)
            {
                DataRow dr = _datatable.NewRow();

                string txtboxType1 = ((Label)row.Cells[0].FindControl("lblSlno")).Text;
                string TaxType = ((LinkButton)row.Cells[0].FindControl("lbtnName")).Text;
                string Percentage = ((TextBox)row.Cells[0].FindControl("txtPurchaseRategv")).Text;
                string ID_TaxType = ((HiddenField)row.Cells[0].FindControl("ID_TaxType")).Value;
                dr["slno"] = txtboxType1;
                dr["TaxType"] = TaxType;
                dr["Percentage"] = Percentage;
                dr["ID_TaxType"] = ID_TaxType;
                _datatable.Rows.Add(dr);
                ViewState["GridData"] = _datatable;
            }

            divAdd.Style.Add("display", "block");
            divView.Style.Add("display", "none");
            txtPurchaseOrder.Focus();
        }
        catch (Exception ex)
        {
        }
    }

    protected void btnS_Click(object sender, EventArgs e)
    {

        // ClearSupplier();

        BlPurchase blpurchase = new BlPurchase();
        DataTable dt;
        Populate Populate = new Populate();
        CommonPopupGrid1.CallingFunction = "Purchase";
        string Refno = ((TextBox)CommonPopupGrid1.FindControl("txtRefno")).Text;
        string searchCol = ((HiddenField)CommonPopupGrid1.FindControl("hfColumn")).Value;
        Refno = Refno == string.Empty || Refno == null ? "0" : Refno;
        GridView gvNew = (GridView)CommonPopupGrid1.FindControl("gdvSearch");
        blpurchase.itemID = Convert.ToInt64(HfItemsID.Value.ToString() == "" ? "0" : HfItemsID.Value);
        blpurchase.Purchase = true;
        decimal s = Convert.ToDecimal(hfTotTaxPercentage.Value == "" ? "0" : hfTotTaxPercentage.Value);
        dt = blpurchase.SelectGriddata();
        dt.Columns.Add("TaxAmount", typeof(decimal));

        foreach (DataRow row in dt.Rows)
        {
            TaxAmount = 0;
            if (Convert.ToDecimal(hfTotTaxPercentage.Value) != 0)
            {
                if (chkInterState.Checked == true && Convert.ToBoolean(row["TaxInterState"]) == true)
                {

                    TaxAmount = Math.Round(Convert.ToDecimal(row["TaxPercentage"]) / Convert.ToDecimal(hfTotTaxPercentage.Value) * (Convert.ToDecimal(txttaxAmount.Text == "" ? "0" : txttaxAmount.Text)), 4);

                }
                else if (chkInterState.Checked == false && Convert.ToBoolean(row["TaxIntraState"]) == true)
                {
                    TaxAmount = Math.Round(Convert.ToDecimal(row["TaxPercentage"]) / Convert.ToDecimal(hfTotTaxPercentage.Value) * (Convert.ToDecimal(txttaxAmount.Text == "" ? "0" : txttaxAmount.Text)), 4);

                }
            }
            row["TaxAmount"] = TaxAmount;
        }


        gvNew.DataSource = dt;
        gvNew.DataBind();
        lblPopupH.Text = "Purchase Tax";
        mpGrid.Show();

    }
    //protected void setPurchaseTaxDetails(int pageIndex)
    //{
    //    GridView gdvSearch1 = ((GridView)CommonPopup1.FindControl("gdvSearch"));

    //    DataTable _datatable = new DataTable();
    //    _datatable.Columns.Add("slno");
    //    _datatable.Columns.Add("ID_TaxType");
    //    _datatable.Columns.Add("TaxType");
    //    _datatable.Columns.Add("Percentage");

    //    foreach (GridViewRow row in gdvSearch1.Rows)
    //    {
    //        DataRow dr = _datatable.NewRow();

    //        string txtboxType1 = ((Label)row.Cells[0].FindControl("lblSlno")).Text;
    //        string TaxType = ((Label)row.Cells[0].FindControl("lbtnName")).Text;
    //        string Percentage = ((TextBox)row.Cells[0].FindControl("txtPurchaseRategv")).Text;
    //        string ID_TaxType = ((HiddenField)row.Cells[0].FindControl("hdfIDValue")).Value;
    //        dr["slno"] = txtboxType1;
    //        dr["TaxType"] = TaxType;
    //        dr["Percentage"] = Percentage;
    //        dr["ID_TaxType"] = ID_TaxType;
    //        _datatable.Rows.Add(dr);

    //        ViewState["GridDataPurchase"] = _datatable;
    //    }

    //}

    public void setSalesTaxDetails(int pageIndex)
    {
        GridView gdvSearch1 = ((GridView)CommonPopupGrid1.FindControl("gdvSearch"));

        DataTable _datatable = new DataTable();
        _datatable.Columns.Add("slno");
        _datatable.Columns.Add("ID_TaxType");
        _datatable.Columns.Add("TaxType");
        _datatable.Columns.Add("FK_Items");
        _datatable.Columns.Add("TaxPercentage");
        _datatable.Columns.Add("TaxInterState");
        _datatable.Columns.Add("TaxIntraState");

        foreach (GridViewRow row in gdvSearch1.Rows)
        {
            DataRow dr = _datatable.NewRow();

            string txtboxType1 = ((Label)row.Cells[0].FindControl("lblSlno")).Text;
            string TaxType = ((Label)row.Cells[0].FindControl("lbtnName")).Text;
            string Percentage = ((TextBox)row.Cells[0].FindControl("txtPurchaseRategv")).Text;
            string ID_TaxType = ((HiddenField)row.Cells[0].FindControl("hdfIDValue")).Value;
            string TaxInterState = ((HiddenField)row.Cells[0].FindControl("hfTaxInterState")).Value;
            string TaxIntraState = ((HiddenField)row.Cells[0].FindControl("hfTaxIntraState")).Value;
            dr["slno"] = txtboxType1;
            dr["TaxType"] = TaxType;
            dr["TaxPercentage"] = Percentage;
            dr["ID_TaxType"] = ID_TaxType;
            dr["TaxInterState"] = TaxInterState;
            dr["TaxIntraState"] = TaxIntraState;
            dr["FK_Items"] = Convert.ToInt64(HfItemsID.Value.ToString());
            decimal value = ((Convert.ToDecimal(txtSalesPrice.Text.ToString()) * Convert.ToDecimal(Percentage.ToString())) / 100) * Convert.ToDecimal(txtQuantity.Text.ToString());
            _datatable.Rows.Add(dr);
            TotValue += value;
            hfSalesValue.Value = Convert.ToString(TotValue);
            TotalTaxPercentage += Convert.ToDecimal(Percentage);
            hfTotTaxsalesPercentage.Value = Convert.ToString(TotalTaxPercentage);

            ViewState["GridDataSales"] = _datatable;
        }
        mpGrid.Hide();
        txtSTaxAmount.Focus();

    }
    public void setPurchasetaxDetails()
    {

        GridView gdvSearch1 = ((GridView)CommonPopupGrid1.FindControl("gdvSearch"));

        DataTable _datatable = new DataTable();
        _datatable.Columns.Add("slno");
        _datatable.Columns.Add("ID_TaxType");
        _datatable.Columns.Add("FK_Items");
        _datatable.Columns.Add("TaxType");
        _datatable.Columns.Add("TaxPercentage");
        _datatable.Columns.Add("Value");
        _datatable.Columns.Add("TaxInterState");
        _datatable.Columns.Add("TaxIntraState");


        foreach (GridViewRow row in gdvSearch1.Rows)
        {
            DataRow dr = _datatable.NewRow();

            string txtboxType1 = ((Label)row.Cells[0].FindControl("lblSlno")).Text;
            string TaxType = ((Label)row.Cells[0].FindControl("lbtnName")).Text;
            string Percentage = ((TextBox)row.Cells[0].FindControl("txtPurchaseRategv")).Text;
            string ID_TaxType = ((HiddenField)row.Cells[0].FindControl("hdfIDValue")).Value;
            string TaxInterState = ((HiddenField)row.Cells[0].FindControl("hfTaxInterState")).Value;
            string TaxIntraState = ((HiddenField)row.Cells[0].FindControl("hfTaxIntraState")).Value;
            decimal value = ((Convert.ToDecimal(txtPurchaseRate.Text.ToString()) * Convert.ToDecimal(Percentage.ToString())) / 100) * Convert.ToDecimal(txtQuantity.Text.ToString());

            dr["slno"] = txtboxType1;
            dr["TaxType"] = TaxType;
            dr["TaxPercentage"] = Percentage;
            dr["ID_TaxType"] = ID_TaxType;
            dr["Value"] = value;
            dr["FK_Items"] = Convert.ToInt64(HfItemsID.Value.ToString());
            dr["TaxInterState"] = TaxInterState;
            dr["TaxIntraState"] = TaxIntraState;
            _datatable.Rows.Add(dr);
            TotValue += value;
            hfTaxValue.Value = Convert.ToString(TotValue);
            if (chkInterState.Checked == true && Convert.ToBoolean(TaxInterState) == true)
            {
                TotalTaxPercentage += Convert.ToDecimal(Percentage);
                hfTotTaxPercentage.Value = Convert.ToString(TotalTaxPercentage);
            }
            else if (chkInterState.Checked == false && Convert.ToBoolean(TaxIntraState) == true)
            {
                TotalTaxPercentage += Convert.ToDecimal(Percentage);
                hfTotTaxPercentage.Value = Convert.ToString(TotalTaxPercentage);
            }

            ViewState["GridDataPurchase"] = _datatable;
        }

        mpGrid.Hide();
        getpurchaseamount();
        txttaxAmount.Focus();

    }


    protected void btSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            // ClearSupplier();
            Populate Populate = new Populate();
            BlPurchase blpurchase = new BlPurchase();
            DataTable dt;
            CommonPopupGrid1.CallingFunction = "Sales";
            string Refno = ((TextBox)CommonPopupGrid1.FindControl("txtRefno")).Text;
            string searchCol = ((HiddenField)CommonPopupGrid1.FindControl("hfColumn")).Value;
            Refno = Refno == string.Empty || Refno == null ? "0" : Refno;
            GridView gvNew = (GridView)CommonPopupGrid1.FindControl("gdvSearch");
            blpurchase.itemID = Convert.ToInt64(HfItemsID.Value.ToString() == "" ? "0" : HfItemsID.Value);

            dt = blpurchase.SelectGriddata();
            dt.Columns.Add("TaxAmount");
            foreach (DataRow row in dt.Rows)
            {
                TaxAmount = 0;
                if (chkInterState.Checked == false && Convert.ToBoolean(row["TaxIntraState"]) == true)
                {
                    if (Convert.ToDecimal(hfTotTaxsalesPercentage.Value) != 0)
                        TaxAmount = Math.Round(Convert.ToDecimal(row["TaxPercentage"]) / Convert.ToDecimal(hfTotTaxsalesPercentage.Value) * (Convert.ToDecimal(txtSTaxAmount.Text == "" ? "0" : txtSTaxAmount.Text)), 4);

                }

                row["TaxAmount"] = TaxAmount;
            }
            gvNew.DataSource = dt;
            gvNew.DataBind();

            lblPopupH.Text = "Sales Tax";
            mpGrid.Show();
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "toastr.warning('" + ex.Message + "')", true);
        }
    }
    protected void btnInsert_Click(object sender, EventArgs e)
    {
        try
        {
            ClearSupplier();
            Populate Populate = new Populate();
            CommonPopup1.CallingFunction = "CallingFunction";
            string Refno = ((TextBox)CommonPopup1.FindControl("txtRefno")).Text;
            string searchCol = ((HiddenField)CommonPopup1.FindControl("hfColumn")).Value;
            Refno = Refno == string.Empty || Refno == null ? "0" : Refno;
            GridView gvNew = (GridView)CommonPopup1.FindControl("gdvSearch");
            BlPurchase blpurchase = new BlPurchase();
            blpurchase.itemID = Convert.ToInt64(HfItemsID.Value.ToString());
            DataTable dt = blpurchase.SelectGriddata();
            gvNew.DataSource = dt;
            gvNew.DataBind();
            lblPopupH.Text = "Sales Tax";
            mpGrid.Show();
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "toastr.warning('" + ex.Message + "')", true);
        }
    }

    protected void chkDiscounttax_CheckedChanged(object sender, EventArgs e)
    {
        ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "DiscountOnTax();", true);

    }
    private void getpurchaseamount()
    {
        if (chkInctax.Checked == true)
        {
            txttaxAmount.Text = Math.Round(((Convert.ToDecimal(txtPurchaseRate.Text == "" ? "0" : txtPurchaseRate.Text)) * (Convert.ToDecimal(txtQuantity.Text == "" ? "0" : txtQuantity.Text)) * (Convert.ToDecimal(hfTotTaxPercentage.Value.ToString()))) / ((Convert.ToDecimal(hfTotTaxPercentage.Value.ToString() == "" ? "0" : hfTotTaxPercentage.Value.ToString())) + (100)), 4).ToString();
            hfPurchaseValue.Value = ((Convert.ToDecimal(txtPurchaseRate.Text == null ? "0" : txtPurchaseRate.Text)) * (Convert.ToDecimal(txtQuantity.Text == null ? "0" : txtQuantity.Text)) - (Convert.ToDecimal(txttaxAmount.Text == null ? "0" : txttaxAmount.Text))).ToString();
            if (chkDiscounttax.Checked == true)
            {
                decimal PurRate = Convert.ToDecimal(txtPurchaseRate.Text == "" ? "0" : txtPurchaseRate.Text);
                decimal disPer = Convert.ToDecimal(txtPurDiscPer.Text == "" ? "0" : txtPurDiscPer.Text);
                decimal ActPrice = ((PurRate) * (disPer)) / (100);
                decimal ActPurRate = (PurRate - ActPrice);
                txttaxAmount.Text = Math.Round(((Convert.ToDecimal(ActPurRate)) * (Convert.ToDecimal(txtQuantity.Text == "" ? "0" : txtQuantity.Text)) * (Convert.ToDecimal(hfTotTaxPercentage.Value.ToString()))) / ((Convert.ToDecimal(hfTotTaxPercentage.Value.ToString() == "" ? "0" : hfTotTaxPercentage.Value.ToString())) + (100)), 4).ToString();
            }
            //else
            //{
            //    txttaxAmount.Text = Math.Round(((Convert.ToDecimal(txtPurchaseRate.Text == "" ? "0" : txtPurchaseRate.Text)) * (Convert.ToDecimal(txtQuantity.Text == "" ? "0" : txtQuantity.Text)) * (Convert.ToDecimal(hfTotTaxPercentage.Value.ToString()))) / ((Convert.ToDecimal(hfTotTaxPercentage.Value.ToString() == "" ? "0" : hfTotTaxPercentage.Value.ToString())) + (100)), 4).ToString();
            //}


        }
        else
        {
            if (chkDiscounttax.Checked == true)
            {
                decimal PurRate = Convert.ToDecimal(txtPurchaseRate.Text == "" ? "0" : txtPurchaseRate.Text);
                decimal disPer = Convert.ToDecimal(txtPurDiscPer.Text == "" ? "0" : txtPurDiscPer.Text);
                decimal ActPrice = ((PurRate) * (disPer)) / (100);
                decimal ActPurRate = (PurRate - ActPrice);
                txttaxAmount.Text = Math.Round(((Convert.ToDecimal(ActPurRate)) * (Convert.ToDecimal(txtQuantity.Text == null ? "0" : txtQuantity.Text)) * (Convert.ToDecimal(hfTotTaxPercentage.Value.ToString() == "" ? "0" : hfTotTaxPercentage.Value.ToString()))) / (100), 4).ToString();
            }
            else
            {

                txttaxAmount.Text = Math.Round(((Convert.ToDecimal(txtPurchaseRate.Text == null ? "0" : txtPurchaseRate.Text)) * (Convert.ToDecimal(txtQuantity.Text == null ? "0" : txtQuantity.Text)) * (Convert.ToDecimal(hfTotTaxPercentage.Value.ToString() == "" ? "0" : hfTotTaxPercentage.Value.ToString()))) / (100), 4).ToString();
            }
            hfPurchaseValue.Value = (Convert.ToDecimal(txtPurchaseRate.Text == null ? "0" : txtPurchaseRate.Text) * Convert.ToDecimal(txtQuantity.Text == null ? "0" : txtQuantity.Text)).ToString();
        }
        if (chkDiscounttax.Checked == true)
        {

            txtPurDiscAmnt.Text = Math.Round((((Convert.ToDecimal(hfPurchaseValue.Value.ToString() == "" ? "0" : hfPurchaseValue.Value.ToString()))) * (Convert.ToDecimal(txtPurDiscPer.Text == null ? "0" : txtPurDiscPer.Text))) / (100), 4).ToString();
        }
        else
        {
            txtPurDiscAmnt.Text = Math.Round(((Convert.ToDecimal(hfPurchaseValue.Value.ToString() == "" ? "0" : hfPurchaseValue.Value.ToString())) * (Convert.ToDecimal(txtPurDiscPer.Text == null ? "0" : txtPurDiscPer.Text))) / (100), 4).ToString();
        }


        txtNetPur.Text = Math.Round((Convert.ToDecimal(hfPurchaseValue.Value.ToString() == "" ? "0" : hfPurchaseValue.Value.ToString())) + (Convert.ToDecimal(txttaxAmount.Text == null ? "0" : txttaxAmount.Text)) - (Convert.ToDecimal(txtPurDiscAmnt.Text == null ? "0" : txtPurDiscAmnt.Text)), 4).ToString();
    }
    private void getpurchaseamountQation()
    {
        if (chkInctax.Checked == true)
        {

            txttaxAmount.Text = Math.Round(((Convert.ToDecimal(txtPurchaseRate.Text == "" ? "0" : txtPurchaseRate.Text)) * (Convert.ToDecimal(txtQuantity.Text == "" ? "0" : txtQuantity.Text)) * (Convert.ToDecimal(hfTotTaxPercentage.Value.ToString()))) / ((Convert.ToDecimal(hfTotTaxPercentage.Value.ToString() == "" ? "0" : hfTotTaxPercentage.Value.ToString())) + (100)), 4).ToString();

            hfPurchaseValue.Value = ((Convert.ToDecimal(txtPurchaseRate.Text == null ? "0" : txtPurchaseRate.Text)) * (Convert.ToDecimal(txtQuantity.Text == null ? "0" : txtQuantity.Text)) - (Convert.ToDecimal(txttaxAmount.Text == null ? "0" : txttaxAmount.Text))).ToString();
        }
        else
        {
            txttaxAmount.Text = Math.Round(((Convert.ToDecimal(txtPurchaseRate.Text == null ? "0" : txtPurchaseRate.Text)) * (Convert.ToDecimal(txtQuantity.Text == null ? "0" : txtQuantity.Text)) * (Convert.ToDecimal(hfTotTaxPercentage.Value.ToString() == "" ? "0" : hfTotTaxPercentage.Value.ToString()))) / (100), 4).ToString();
            hfPurchaseValue.Value = (Convert.ToDecimal(txtPurchaseRate.Text == null ? "0" : txtPurchaseRate.Text) * Convert.ToDecimal(txtQuantity.Text == null ? "0" : txtQuantity.Text)).ToString();
        }
        if (chkDiscounttax.Checked == true)
        {


            txtPurDiscPer.Text = Math.Round((Convert.ToDecimal(txtPurDiscAmnt.Text.ToString() == "" ? "0" : txtPurDiscAmnt.Text.ToString())) * (100) / (Convert.ToDecimal(hfPurchaseValue.Value.ToString() == "" ? "0" : hfPurchaseValue.Value.ToString())), 4).ToString();
            txtPurDiscAmnt.Text = Math.Round((((Convert.ToDecimal(hfPurchaseValue.Value.ToString() == "" ? "0" : hfPurchaseValue.Value.ToString())) + (Convert.ToDecimal(txttaxAmount.Text == null ? "0" : txttaxAmount.Text))) * (Convert.ToDecimal(txtPurDiscPer.Text == null ? "0" : txtPurDiscPer.Text))) / (100), 4).ToString();
        }
        else
        {

            txtPurDiscPer.Text = Math.Round((Convert.ToDecimal(txtPurDiscAmnt.Text.ToString() == "" ? "0" : txtPurDiscAmnt.Text.ToString())) * (100) / (Convert.ToDecimal(hfPurchaseValue.Value.ToString() == "" ? "0" : hfPurchaseValue.Value.ToString())), 4).ToString();
            txtPurDiscAmnt.Text = Math.Round(((Convert.ToDecimal(hfPurchaseValue.Value.ToString() == "" ? "0" : hfPurchaseValue.Value.ToString())) * (Convert.ToDecimal(txtPurDiscPer.Text == null ? "0" : txtPurDiscPer.Text))) / (100), 4).ToString();
        }


        txtNetPur.Text = Math.Round((Convert.ToDecimal(hfPurchaseValue.Value.ToString() == "" ? "0" : hfPurchaseValue.Value.ToString())) + (Convert.ToDecimal(txttaxAmount.Text == null ? "0" : txttaxAmount.Text)) - (Convert.ToDecimal(txtPurDiscAmnt.Text == null ? "0" : txtPurDiscAmnt.Text)), 4).ToString();
    }

    protected void gvPurchase_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvPurchase.PageIndex = e.NewPageIndex;
        GridFillPurchase(e.NewPageIndex);
    }
    protected void ImageButton2_Click(object sender, ImageClickEventArgs e)
    {
        Delete();
    }
    protected void chkInterState_CheckedChanged(object sender, EventArgs e)
    {

        if (gvItems.Rows.Count > 0 && hfGvItemSelected.Value.ToString() == "")
        {
            if (Validform())
                GridCalculation();
            else
                ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "toastr.warning('Incomplete item detail exist')", true);
        }
        if (gvItems.Rows.Count > 0 && hfGvItemSelected.Value.ToString() == "1")
        {
            ddlItemsFill();
            if (Validform())
                GridCalculation();
            else
                ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "toastr.warning('Incomplete item detail exist')", true);
        }
        if (gvItems.Rows.Count == 0)
        {
            ddlItemsFill();
        }
        CalculateTotals();



    }
    protected void ddlSubunit_SelectedIndexChanged(object sender, EventArgs e)
    {
        CalculateActualQuantity();

    }

    protected void btnQuation_Click(object sender, EventArgs e)
    {
        QuotationData();
        txtFree.Focus();

    }
    private void Enbletextbox(bool value)
    {
        txtPurchaseRate.Enabled = ddlSubunit.Enabled = ddlUnit.Enabled = chkDiscounttax.Enabled = chkInctax.Enabled = value;

    }


    protected bool Validate()
    {
        bool St = true;
        if (Convert.ToDecimal(txtSalesPrice.Text.ToString() == "" ? "0" : txtSalesPrice.Text.ToString()) < Convert.ToDecimal(txtPurchaseRate.Text.ToString().Trim() == "" ? "0" : txtPurchaseRate.Text.ToString()))
        {
            ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "alertify.alert('Sales Price rate Should not be less than Purchase Rate')", true);
            St = false;
        }

        if (Convert.ToDecimal(txtSalesPrice.Text.ToString() == "" ? "0" : txtSalesPrice.Text.ToString()) > Convert.ToDecimal(txtMRP.Text.ToString().Trim() == "" ? "0" : txtMRP.Text.ToString()))
        {
            ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "alertify.alert('Sales Price rate Should not be greater than MRP')", true);
            St = false;
        }
        if (Convert.ToDecimal(txtFree.Text.ToString() == "" ? "0" : txtFree.Text.ToString()) < Convert.ToDecimal(hfQFree.Value.ToString().Trim() == "" ? "0" : hfQFree.Value.ToString()))
        {
            ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "alertify.alert('Invalid free Quantity')", true);
            St = false;
        }
        else if (Convert.ToDecimal(txtPurDiscPer.Text.ToString()) < Convert.ToDecimal(hfQDiscper.Value.ToString().Trim() == "" ? "0" : hfQDiscper.Value.ToString()))
        {
            ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "alertify.alert('Invalid PurDisc% Quantity')", true);
            St = false;
        }
        else if (Convert.ToDecimal(txtPurDiscAmnt.Text.ToString()) < Convert.ToDecimal(hfQDisAmt.Value.ToString().Trim() == "" ? "0" : hfQDisAmt.Value.ToString()))
        {
            ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "alertify.alert('Invalid PurDisAmt Quantity')", true);
            St = false;
        }
        return St;
    }

    protected void btnOtherCharges_Click(object sender, EventArgs e)
    {
        ExpenseFill(1);
        CalculateTotals();
    }
    public void ExpenseFill(int pageIndex)
    {


        BlPurchase blpurchase = new BlPurchase();
        DataTable dt;
        Populate Populate = new Populate();
        CommonPopupExpense.CallingFunction = "PurchaseExpense";
        string Refno = ((TextBox)CommonPopupExpense.FindControl("txtRefno")).Text;
        string Total = ((TextBox)CommonPopupExpense.FindControl("txtTo")).Text;
        string searchCol = ((HiddenField)CommonPopupExpense.FindControl("hfColumn")).Value;
        string hfTotalExpense = ((HiddenField)CommonPopupExpense.FindControl("hfTotalExpense")).Value;
        Refno = Refno == string.Empty || Refno == null ? "0" : Refno;
        GridView gvNew = (GridView)CommonPopupExpense.FindControl("gdvSearch");
        blpurchase.MasterId = Convert.ToInt64(ViewState["MasterIDTaxDetails"]);
        if (Convert.ToInt32(hfBoolValue.Value == "" ? "1" : hfBoolValue.Value) == 1 && ViewState["ExpenseDetails"] != null)
        {
            dt = (DataTable)ViewState["ExpenseDetails"];
        }
        else
        {
            dt = blpurchase.SelectExpenseInPurchase();
        }


        gvNew.DataSource = dt;
        gvNew.DataBind();

        foreach (GridViewRow row in gvNew.Rows)
        {
            string AddLess = ((HiddenField)row.Cells[0].FindControl("hfAddLess")).Value;
            if (Convert.ToString(AddLess) == "Add")
            {
                string Amount = ((TextBox)row.Cells[0].FindControl("txtAmount")).Text;
                TotalAdd += Convert.ToDecimal(Amount == "" ? "0" : Amount);
            }
            else if (Convert.ToString(AddLess) == "Less")
            {
                string Amount = ((TextBox)row.Cells[0].FindControl("txtAmount")).Text;
                TotalLess += Convert.ToDecimal(Amount == "" ? "0" : Amount);
            }
            hfTotalExpense = Convert.ToString(TotalAdd - TotalLess);
            hfExpenseAmount.Value = hfTotalExpense;
            ((TextBox)CommonPopupExpense.FindControl("txtTo")).Text = hfTotalExpense;

        }


        lblPopupHeader2.Text = "Expense";
        mpExpense.Show();

    }


    public void setExpenseDetails(int pageIndex)
    {
        GridView gdvSearch1 = ((GridView)CommonPopupExpense.FindControl("gdvSearch"));

        DataTable _datatable = new DataTable();
        _datatable.Columns.Add("slno");
        _datatable.Columns.Add("ID_ExpenseType");
        _datatable.Columns.Add("ExpenseType");
        _datatable.Columns.Add("Amount");
        _datatable.Columns.Add("EtAddorLess");
        _datatable.Columns.Add("grand");


        foreach (GridViewRow row in gdvSearch1.Rows)
        {
            DataRow dr = _datatable.NewRow();

            string txtboxType1 = ((Label)row.Cells[0].FindControl("lblSlno")).Text;
            string ExpenseType = ((Label)row.Cells[0].FindControl("lbtnName")).Text;
            string Amount = ((TextBox)row.Cells[0].FindControl("txtAmount")).Text;
            string ID_ExpenseType = ((HiddenField)row.Cells[0].FindControl("hdfIDValue")).Value;
            string EtAddorLess = ((HiddenField)row.Cells[0].FindControl("hfAddLess")).Value;
            string txtTo = ((TextBox)CommonPopupExpense.FindControl("txtTo")).Text;
            dr["slno"] = txtboxType1;
            dr["ExpenseType"] = ExpenseType;
            dr["Amount"] = Amount;
            dr["ID_ExpenseType"] = ID_ExpenseType;
            dr["EtAddorLess"] = EtAddorLess;
            dr["grand"] = txtTo;
            _datatable.Rows.Add(dr);
            ViewState["ExpenseDetails"] = _datatable;
            hfBoolValue.Value = "1";

        }
        CalculateTotals();
        mpExpense.Hide();


    }


    protected void FileUploadExcel_UploadedComplete(object sender, AjaxControlToolkit.AsyncFileUploadEventArgs e)
    {
        try
        {

            if (FileUploadExcel.HasFile)
            {

                string filext = System.IO.Path.GetExtension(FileUploadExcel.FileName).ToLower();
                if (filext == ".xls" || filext == ".xlt" || filext == ".xlsm")
                {
                    string filePath = FileUploadExcel.PostedFile.FileName.ToString();
                    string path = Server.MapPath("~") + "\\Excel/";

                    if (!Directory.Exists(path))
                    {
                        Directory.CreateDirectory(path);
                    }

                    FileUploadExcel.SaveAs(path + "/" + FileUploadExcel.FileName);
                    Session[Excel] = path + "/" + FileUploadExcel.FileName;

                }
                else
                {

                    return;
                }
            }
        }
        catch (Exception ex)
        {

        }
    }
    protected void btnShowData_Click(object sender, EventArgs e)
    {
        FillGrid();
    }
    public void ExcelDataValidationInGrid(DataTable dtData)
    {
        if (dtData.Rows.Count > 0)
        {

        }

    }
    protected string ValidateExcel(DataTable dt)
    {
        ArrayList arrColumns = new ArrayList();
        dt.TableName = "BlMaterialReciept";
        MemoryStream str = new MemoryStream();
        dt.WriteXml(str, true);
        str.Seek(0, SeekOrigin.Begin);
        StreamReader sr = new StreamReader(str);
        string xmlstr;
        xmlstr = sr.ReadToEnd();
        return (xmlstr);
    }

    protected DataSet DatasetFromExcel(string filePath)
    {

        string ext = Path.GetExtension(filePath);
        dsExcel = new DataSet();

        FileStream stream = File.Open(filePath, FileMode.Open, FileAccess.ReadWrite);

        if (ext == ".xls")//Reading from Excel file ('97-2003 format; *.xls)
        {
            IExcelDataReader excelReader = ExcelReaderFactory.CreateBinaryReader(stream);
            excelReader.IsFirstRowAsColumnNames = true;// DataSet - Create column names from first row
            try
            {

                dsExcel = excelReader.AsDataSet(true);
                DataTable dt = dsExcel.Tables[0];
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    for (int j = 0; j < dt.Columns.Count; j++)
                    {
                        if (string.IsNullOrEmpty(dt.Rows[i][j].ToString()))
                        {

                            dt.Rows[i][j] = 0;

                        }
                    }
                }


            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "toastr.error('Excel format does not support: Error 1')", true);

            }

            excelReader.Close();
        }
        else if (ext == ".xlsx")// Reading from Excel file (2007 format; *.xlsx)
        {
            IExcelDataReader excelReader = ExcelReaderFactory.CreateOpenXmlReader(stream);
            excelReader.IsFirstRowAsColumnNames = true;// DataSet - Create column names from first row
            dsExcel = excelReader.AsDataSet(true);
            //    dsExcel.Tables[0].TableName = "Items";
            excelReader.Close();


        }
        if (File.Exists(filePath))
        {
            string path = AppDomain.CurrentDomain.BaseDirectory + @"Excel";
            DirectoryInfo info = new DirectoryInfo(path);
            //DirectoryInfo info = new DirectoryInfo(Server.MapPath("Excel"));
            foreach (FileInfo files in info.GetFiles())
            {
                try
                {
                    files.Delete();
                }
                catch (Exception ex)
                {
                    ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "toastr.error('Excel format does not support: Error 2')", true);

                }
            }
        }
        return dsExcel;
    }

    protected DataTable GetExcelData()
    {
        DataTable dtbl = new DataTable();
        if (Session[Excel] != null)
        {
            ExcelPath = Session[Excel].ToString();
            DataSet ds = DatasetFromExcel(ExcelPath);
            DataTable dt = new DataTable();
            dt = ds.Tables[0];
            if (dt.Columns.Contains("ItemCode") || dt.Columns.Contains("PurRate") || dt.Columns.Contains("Qty") || dt.Columns.Contains("Free") || dt.Columns.Contains("ActQty") || dt.Columns.Contains("MRP") || dt.Columns.Contains("Unit") || dt.Columns.Contains("BatchNo") || dt.Columns.Contains("ExpiryDate") || dt.Columns.Contains("DiscountPer") || dt.Columns.Contains("DiscountAmt") || dt.Columns.Contains("ProfitPer") || dt.Columns.Contains("PurDiscPer") || dt.Columns.Contains("PurchaseDiscAmount") || dt.Columns.Contains("NetPurchase"))
            {

                int columnCount = ds.Tables[0].Columns.Count;
                for (int i = 0; i < columnCount; i++)
                {
                    dtbl.Columns.Add(ds.Tables[0].Columns[i].ColumnName, typeof(System.String));
                }
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    DataRow drow = dtbl.NewRow();
                    for (int j = 0; j < columnCount; j++)
                    {
                        drow[j] = ds.Tables[0].Rows[i][j].ToString();
                    }
                    // if (Convert.ToString(drow.ItemArray[0]) != "" && Convert.ToString(drow.ItemArray[1]) != "" && Convert.ToString(drow.ItemArray[2]) != "" && Convert.ToString(drow.ItemArray[3]) != "" && Convert.ToString(drow.ItemArray[4]) != "" && Convert.ToString(drow.ItemArray[5]) != "" && Convert.ToString(drow.ItemArray[6]) != "" && Convert.ToString(drow.ItemArray[7]) != "" && Convert.ToString(drow.ItemArray[8]) != "" && Convert.ToString(drow.ItemArray[9]) != "" && Convert.ToString(drow.ItemArray[10]) != "" && Convert.ToString(drow.ItemArray[11]) != "" && Convert.ToString(drow.ItemArray[12]) != "" && Convert.ToString(drow.ItemArray[13]) != "")
                    if (Convert.ToString(drow.ItemArray[0]) != "" && Convert.ToString(drow.ItemArray[1]) != "" && Convert.ToString(drow.ItemArray[2]) != "" && Convert.ToString(drow.ItemArray[3]) != "" && Convert.ToString(drow.ItemArray[4]) != "" && Convert.ToString(drow.ItemArray[5]) != "")
                    {
                        dtbl.Rows.InsertAt(drow, i);
                    }
                }
            }
        }
        return dtbl;

    }
    private void FillGrid()
    {
        try
        {
            ViewState["XmlExcel"] = null;
            BlMaterialReciept blAccountSubTrn = new BlMaterialReciept();
            DataTable dt = new DataTable();
            dt = GetExcelData();
            if (dt.Rows.Count > 0)
            {
                ViewState["RedLine"] = null;
                ViewState["XmlExcel"] = dt;
                string Xmldata = ValidateExcel(dt);
                blAccountSubTrn.XmlExcelImport = Xmldata;
                DataTable dtDetails = blAccountSubTrn.ValidateExcelData();
                ViewState["XmlExcel"] = dt;
                if (dtDetails.Rows.Count > 0)
                {


                    gvItems.DataSource = dtDetails;
                    gvItems.DataBind();
                    ViewState["TempTable"] = dtDetails;
                    hfSlNo.Value = "";
                    txtItemShortName.Focus();
                    ExcelDataValidationInGrid(dtDetails);
                    CalculateTotals();
                    dt = null;
                    dtDetails = null;

                    btnShowData.Enabled = false;

                }
                else
                {

                }

            }
            else
            {

                ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "toastr.warning('Please Upload Correct Formatted Excel Document')", true);

            }

        }
        catch
        {
            gvItems.DataSource = null;
            gvItems.DataBind();
            ViewState["TempTable"] = null;
            ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "toastr.warning('Please Upload Correct Formatted Excel Document')", true);

        }


    }

    protected void btnItem_Click(object sender, EventArgs e)
    {
        hfQFree.Value = "0";
        hfQDiscper.Value = "0";
        hfQDisAmt.Value = "0";
        //ViewState["GridDataPurchase"] = null;
        Enbletextbox(true);
        ddlItemsFill();
        PreviousData();
        QuotationData();
    }
    protected void imgPopUp_Click(object sender, ImageClickEventArgs e)
    {
        MpeApprove.Hide();
        ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "popUpStockSelectClick();", true);
        ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "popUpStockSelectClick();", true);

    }
    protected void chkFree_CheckedChanged(object sender, EventArgs e)
    {
        try
        {

            ddlItemsFill();
            if (chkFree.Checked)
            {
                EnableControlForFreeQty(false);
            }
            else
            {
                EnableControlForFreeQty(true);
            }
        }
        catch (Exception)
        {

            throw;
        }
    }
    private void EnableControlForFreeQty(bool IsChecked)
    {

        if (chkFree.Checked == true)
        {

            txtFree.Text = "0";
            txtPurDiscPer.Text = "0";
            txtPurDiscAmnt.Text = "0";
            txtNetPur.Text = "0";
            txtPurchaseRate.Text = "0";
            txtActqty.Text = txtQuantity.Text;
            txtTotalAmount.Text = "0"; txttaxAmount.Text = "0"; txtNetPur.Text = "0";
            txtProfitAmount.Text = "0";
            txtProfitPer.Text = "0";
        }

        if (IsChecked == false)
        {
            txtFree.Attributes.Add("readOnly", "readOnly");
            txtPurchaseRate.Attributes.Add("readOnly", "readOnly");
            txtNetPur.Attributes.Add("readOnly", "readOnly");
            txtPurDiscPer.Attributes.Add("readOnly", "readOnly");
            txtPurDiscAmnt.Attributes.Add("readOnly", "readOnly");
            txtNetPur.Attributes.Add("readOnly", "readOnly");
            chkDiscounttax.Attributes.Add("readOnly", "readOnly");
            txtProfitAmount.Attributes.Add("readOnly", "readOnly");
            txtProfitPer.Attributes.Add("readOnly", "readOnly");
            chkDiscounttax.Attributes.Add("readOnly", "readOnly");
            //chkInctax.Attributes.Add("readOnly", "readOnly");

        }
        else
        {
            txtFree.Attributes.Remove("readOnly");// This is working...Agin
            txtPurchaseRate.Attributes.Remove("readOnly");
            // txtNetPur.Attributes.Remove("readOnly");
            txtPurDiscPer.Attributes.Remove("readOnly");
            txtPurDiscAmnt.Attributes.Remove("readOnly");
            // txtNetPur.Attributes.Remove("readOnly");
            chkDiscounttax.Attributes.Remove("readOnly");
            txtProfitAmount.Attributes.Remove("readOnly");
            txtProfitPer.Attributes.Remove("readOnly");
            chkDiscounttax.Attributes.Remove("readOnly");
            chkInctax.Attributes.Remove("readOnly");
        }
        chkInctax.Enabled = IsChecked;
        chkDiscounttax.Enabled = IsChecked;
        //txtFree.Enabled = IsChecked;
        // txtPurchaseRate.Enabled = IsChecked;
        //txtNetPur.Enabled = IsChecked;
        //txtPurDiscPer.Enabled = IsChecked;
        //txtPurDiscAmnt.Enabled = IsChecked;
        // txtNetPur.Enabled = IsChecked;
        //chkDiscounttax.Checked = IsChecked;
        //txtProfitAmount.Enabled = IsChecked;
        // txtProfitPer.Enabled = IsChecked;
        //chkDiscounttax.Enabled = IsChecked;
        //

    }
}