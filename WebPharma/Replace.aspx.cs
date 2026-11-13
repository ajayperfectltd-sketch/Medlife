/*----------------------------------------------------------------------------------------------------------------------------------------------------------
Purpose    : Insert/Update/Delete Replace
Author     : Aiswarya--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
Copyright  : Perfect Software Solutions (Clt) Pvt Ltd
Created on : 01/09/2018
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
using System.Data.SqlClient;
using System.Globalization;
using System.Collections;
using WebPharma.Interface.WebPharma;
using WebPharma.Business;


public partial class Replace : System.Web.UI.Page
{
    #region Functions
    private const string PurchaseReturnItems = "PurchaseReturnItems";
    decimal TotValue, TotalTaxPercentage, TotalTaxSales, TaxAmount, TotalFromTaxSales, TaxAmountFrom, TotalFromTaxPercentage;
    private bool button1WasClicked = false;
    long TransactionID;
    public void SetReadonly()
    {
        txtDate.Attributes.Add("readonly", "readonly");
        txtInvoiceDate.Attributes.Add("readonly", "readonly");
        txtPurBillNo.Attributes.Add("readonly", "readonly");
        txtToTotal.Attributes.Add("readonly", "readonly");
        txtToRoundOff.Attributes.Add("readonly", "readonly");
        txtToNetAmount.Attributes.Add("readonly", "readonly");
    }
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
    public void DeleteMethod(string Reason = "0")
    {
        string confirmValue = Request.Form["confirm_value"];
        if (confirmValue.Length > 3)
            confirmValue = confirmValue.Substring(confirmValue.Length - 3, 3);
        if (confirmValue == "Yes")
        {
            long status = 0;
            BlReplace blreplace = new BlReplace();
            blreplace.UserCode = hfuserName.Value;
            blreplace.MasterId = Convert.ToInt64(hfMasterID.Value);
            blreplace.Fk_Reason = Convert.ToInt64(Reason);
            if (hfIsDelete.Value.ToLower() == "true")
            {
                status = blreplace.DeleteReplace();
                if (status > 0)
                {
                    GridFill(1);
                    ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "toastr.success('Data deleted successfully')", true);
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "toastr.error('Cannot delete data, reference exists')", true);
                }
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "toastr.warning('User does not have permission to Delete')", true);
                txtDate.Focus();
            }
            blreplace = null;
        }
    }
    protected void CheckUserAccess()
    {
        BlUserAccess bluseraccess = new BlUserAccess();
        Populate populate = new Populate();

        bluseraccess.FK_Users = Convert.ToInt64(hfUser.Value);
        bluseraccess.FK_Pages = 51;
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
    protected void BindData()
    {
        Populate populate = new Populate();

        // populate.FillDropdownList(ddlItem, "Items", "ItemName", "ID_Items", "ItemName", "Cancelled=0 AND FK_Client= " + hfClient.Value.Split(',')[0], "");
        populate.FillDropdownList(ddlSupplier, "Supplier", "SupplierName", "ID_Supplier", "SupplierName", "Cancelled=0 AND FK_Client = " + hfClient.Value, "");
        populate.FillDropdownList(ddlSearchSupplier, "Supplier", "SupplierName", "ID_Supplier", "SupplierName", "Cancelled=0 AND FK_Client = " + hfClient.Value, "");
        populate.FillDropdownList(ddlCategory, "Category", "CategoryName", "ID_Category", "CategoryName", "Cancelled=0 and  FK_Client= " + hfClient.Value.Split(',')[0], "");
        populate.FillDropdownList(ddlUnit, "Units", "UnitName", "ID_Units", "UnitName", "Cancelled=0 and FK_Client= " + hfClient.Value.Split(',')[0], "");
        populate.FillDropdownList(ddlSubunit, "SubUnit", "SubUnitName", "ID_SubUnits", "SubUnitName", "Cancelled=0 and FK_Client= " + hfClient.Value.Split(',')[0], "");
        populate.FillDropdownList(ddlBillType, "BillType", "TypeName", "ID_BillType", "ID_BillType", "Cancelled=0 AND BillType=2 AND  FK_Client= " + hfClient.Value.Split(',')[0], "");
        populate.FillDropdownList(ddlStore, "Store", "StoName", "ID_Store", "StoName", " Active='True'  AND Cancelled=0 AND FK_Client= " + hfClient.Value.Split(',')[0], "");
        populate.FillDropdownList(ddlStoreSearch, "Store", "StoName", "ID_Store", "StoName", " Active='True' AND Cancelled=0 AND FK_Client= " + hfClient.Value.Split(',')[0], "");
        populate = null;
        // txtDate.Text = DateTime.Now.ToShortDateString();
        //  txtInvoiceDate.Text = DateTime.Now.ToShortDateString();


    }
    private void Transactionchecking()
    {
        BlFunction blfun = new BlFunction();
        blfun.Module = "REP";
        TransactionID = blfun.GetTransactionID("REP", hfuserName.Value);
        ViewState["TransactionID"] = TransactionID;
    }
    private void BindPurchaseItems(long purchaseId)
    {
        try
        {
            BlPurchaseReturn blpurchasereturn = new BlPurchaseReturn();
            blpurchasereturn.FK_Purchase = purchaseId;
            blpurchasereturn.FK_Client = Convert.ToInt32(hfClient.Value);
            blpurchasereturn.MasterId = Convert.ToInt64(purchaseId);
            hfFK_PurchaseReturn.Value = purchaseId.ToString();
            blpurchasereturn.ReturnMethod = "";
            DataTable dtbl = blpurchasereturn.SelectPurchaseItems();
            gvPurchaseReturn.DataSource = dtbl;
            gvPurchaseReturn.DataBind();
            ddlSupplier.SelectedValue = dtbl.Rows[0]["FK_Supplier"].ToString();
            hdfFKStock.Value = dtbl.Rows[0]["Fk_Stock"].ToString();
            BlPurchase blp = new BlPurchase();
            blp.MasterId = purchaseId;
            blp.FK_Client = Convert.ToInt64(hfClient.Value);
            blp.FromDate = DateTime.Parse("1/1/1900");
            blp.SelectPurchase();
            ddlStore.SelectedValue = blp.FK_Store.ToString();
            ddlStore.Enabled = false;
            // txtDiscount.Text = blp.DiscountPer.ToString();
            // txtDiscountAmt.Text = blp.DiscountAmt.ToString();
            ViewState[PurchaseReturnItems] = dtbl;
            ddlStore.SelectedValue = Session["mainstore"].ToString();
            ddlStore.Enabled = false;
            FillReasonddl(true);
            foreach (GridViewRow row in gvPurchaseReturn.Rows)
            {
               // ((TextBox)row.FindControl("txtQuantity")).ReadOnly = true;
               // ((TextBox)row.FindControl("txtQuantity")).ReadOnly = true;
               // ((TextBox)row.FindControl("txtFreeQty")).ReadOnly = true;
               // ((TextBox)row.FindControl("txtActQty")).ReadOnly = true;
                ((TextBox)row.FindControl("lblDiscAmt")).ReadOnly = true;
                ((DropDownList)row.FindControl("ddlReason")).Enabled = false;
                ((ImageButton)row.FindControl("ImageButton4")).Visible = false;
                ((TextBox)row.FindControl("lblDiscAmt")).ReadOnly = true;
            }
            // EnableTotalTextBox(true);
            TotalAmount(dtbl);
            blpurchasereturn = null;
            blp = null;
        }
        catch (Exception ex)
        {

        }
    }
    protected void FillReasonddl(Boolean rdPurchase)
    {

        DataTable dtbl = (DataTable)ViewState[PurchaseReturnItems];
        for (int i = 0; i < gvPurchaseReturn.Rows.Count; i++)
        {

            Populate populate = new Populate();
            populate.FillDropdownList(((DropDownList)gvPurchaseReturn.Rows[i].FindControl("ddlReason")), "Reason", "Reason", "ID_Reason", "ID_Reason", " FK_Client = " + hfClient.Value, "");
            ((DropDownList)gvPurchaseReturn.Rows[i].FindControl("ddlReason")).SelectedValue = dtbl.Rows[i]["FK_Reason"].ToString();
        }



    }
    private void getpurchaseamount()
    {
        if (chkInctax.Checked == true)
        {

            txttaxAmount.Text = Math.Round(((Convert.ToDecimal(txtPurchaseRate.Text == "" ? "0" : txtPurchaseRate.Text)) * (Convert.ToDecimal(txtQuantity.Text == "" ? "0" : txtQuantity.Text)) * (Convert.ToDecimal(hfTotTaxPercentage.Value.ToString()))) / ((Convert.ToDecimal(hfTotTaxPercentage.Value.ToString() == "" ? "0" : hfTotTaxPercentage.Value.ToString())) + (100)), 4).ToString();

            hfPurchaseValue.Value = ((Convert.ToDecimal(txtPurchaseRate.Text == null ? "0" : txtPurchaseRate.Text)) * (Convert.ToDecimal(txtQuantity.Text == null ? "0" : txtQuantity.Text)) - (Convert.ToDecimal(txttaxAmount.Text == null ? "0" : txttaxAmount.Text))).ToString();
        }
        else
        {
            txttaxAmount.Text = Math.Round(((Convert.ToDecimal(txtPurchaseRate.Text == "" ? "0" : txtPurchaseRate.Text)) * (Convert.ToDecimal(txtQuantity.Text == "" ? "0" : txtQuantity.Text)) * (Convert.ToDecimal(hfTotTaxPercentage.Value.ToString() == "" ? "0" : hfTotTaxPercentage.Value.ToString()))) / (100), 4).ToString();
            hfPurchaseValue.Value = (Convert.ToDecimal(txtPurchaseRate.Text == "" ? "0" : txtPurchaseRate.Text) * Convert.ToDecimal(txtQuantity.Text == "" ? "0" : txtQuantity.Text)).ToString();
        }
        if (chkDiscounttax.Checked == true)
        {

            txtPurDiscAmnt.Text = Math.Round((((Convert.ToDecimal(hfPurchaseValue.Value.ToString() == "" ? "0" : hfPurchaseValue.Value.ToString())) + (Convert.ToDecimal(txttaxAmount.Text == null ? "0" : txttaxAmount.Text))) * (Convert.ToDecimal(txtPurDiscPer.Text == null ? "0" : txtPurDiscPer.Text))) / (100), 4).ToString();
        }
        else
        {
            txtPurDiscAmnt.Text = Math.Round(((Convert.ToDecimal(hfPurchaseValue.Value.ToString() == "" ? "0" : hfPurchaseValue.Value.ToString())) * (Convert.ToDecimal(txtPurDiscPer.Text == null ? "0" : txtPurDiscPer.Text))) / (100), 4).ToString();
        }

        // txtNetPur.Text= Math.Round((Convert.ToDecimal(hfPurchaseValue.Value.ToString()) + Convert.ToDecimal(txttaxAmount.Text == null ? "0" : txttaxAmount.Text) - Convert.ToDecimal(txtPurDiscAmnt.Text == null ? "0" : txtPurDiscAmnt.Text)), 4);
        txtNetPur.Text = Math.Round((Convert.ToDecimal(hfPurchaseValue.Value.ToString() == "" ? "0" : hfPurchaseValue.Value.ToString())) + (Convert.ToDecimal(txttaxAmount.Text == null ? "0" : txttaxAmount.Text)) - (Convert.ToDecimal(txtPurDiscAmnt.Text == null ? "0" : txtPurDiscAmnt.Text)), 4).ToString();
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
            //hdfItem.Value=
        }
        else
        {
            dtbl.Columns.Add("SLNO", typeof(int));
            dtbl.Columns.Add("Item");
            dtbl.Columns.Add("FK_Items");
            dtbl.Columns.Add("FK_Unit");
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
            dtbl.Columns.Add("Flag");
            dtbl.Columns.Add("TaxPer");

        }
        if ((hdfItem.Value != "" ))
        {

            gvItems.Enabled = true;
            dtbl.Rows.RemoveAt(Convert.ToInt32(hdfItem.Value));
            gvItems.DataSource = dtbl;
            gvItems.DataBind();
            hfSlNo.Value = hdfItem.Value;
            hdfItem.Value = "";

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
            drow["FK_Unit"] = ddlUnit.SelectedValue.ToString();
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
            drow["ActQty"] = Convert.ToDecimal(txtActq.Text == "" ? "0" : txtActq.Text);
            drow["IncludePtax"] = Convert.ToBoolean(chkInctax.Checked);
            drow["PurchaseRate"] = Convert.ToDecimal(txtPurchaseRate.Text == "" ? "0" : txtPurchaseRate.Text);
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
            drow["Flag"] = Convert.ToInt64(txtKey.Text);
            drow["TaxPer"] = Convert.ToDecimal(hfTotTaxPercentage.Value == "" ? "0" : hfTotTaxPercentage.Value);
            dtbl.Rows.InsertAt(drow, rowCount);
            dtbl.DefaultView.Sort = "SLNO";
            dtbl = dtbl.DefaultView.ToTable();
            gvItems.DataSource = dtbl;
            gvItems.DataBind();
            ViewState["TempTable"] = dtbl;
            hfSlNo.Value = "";
            CalculateTotals();
            txtItmShortName.Focus();
            ClearTo();
            ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "ItemClear();", true);
            ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "Total();", true);
        }
        else
        {
            ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "alertify.alert('Item already exists with same batch no')", true);
            ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "Total();", true);
        }

    }
    protected void CalculateTotals()
    {
        Decimal NetPur = 0; Decimal NetAmount = 0;
        decimal value = Convert.ToDecimal(hfPurchaseValue.Value);
        decimal DiscountAmnt = 0; decimal TaxAmount = 0;
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
                        DiscountAmnt = DiscountAmnt + (Convert.ToDecimal(dtbl.Rows[i]["PurchaseDiscAmount"].ToString()));
                        TaxAmount = TaxAmount + (Convert.ToDecimal(dtbl.Rows[i]["PurchasetaxAmount"].ToString()));
                    }

                    txtToTotal.Text = Math.Round(NetPur, 4).ToString();
                    hfTotalDiscAmnt.Value = Math.Round(((value + TaxAmount) - (DiscountAmnt)), 4).ToString();

                }
                else
                {
                    txtToTotal.Text = "0";
                    txtToRoundOff.Text = "0";
                    txtToNetAmount.Text = "0";
                }
            }
            else
            {
                txtToTotal.Text = Math.Round(Convert.ToDecimal(0), 2).ToString();
                txtToNetAmount.Text = Math.Round(Convert.ToDecimal(0), 2).ToString();
            }
        }
        else
        {
            txtToTotal.Text = Math.Round(Convert.ToDecimal(0), 2).ToString();
            txtToNetAmount.Text = Math.Round(Convert.ToDecimal(0), 2).ToString();
        }



        txtToNetAmount.Text = Math.Round((NetAmount + Convert.ToDecimal(txtToTotal.Text)), 4).ToString();
        ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "CalculateNetT()", true);
        ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "Total()", true);



    }
    protected void ddlItemsFill()
    {


        BlItems blitems = new BlItems();
        blitems.MasterId = Convert.ToInt64(HfItemsID.Value);
        blitems.FK_Client = Convert.ToInt64(hfClient.Value);
        blitems.SelectData();
        ddlCategory.SelectedValue = blitems.FK_CategoryFirst.ToString();
        ddlUnit.SelectedValue = blitems.FK_Units.ToString();
        txtItmShortName.Text = blitems.Name.ToString();
        hfExpirydays.Value = blitems.ExpairyValidation.ToString();
        txtHSNSAC.Text = blitems.HSNCode;
        blitems = null;
        BlUnit blunit = new BlUnit();
        blunit.MasterId = Convert.ToInt64(ddlUnit.SelectedValue);
        blunit.FK_Client = Convert.ToInt64(hfClient.Value);
        blunit.SelectData();
        hfUnitQuantity.Value = blunit.NoofUnits.ToString();
        blunit = null;
        // QuotationData();
        BlPurchase blpurchase = new BlPurchase();
        blpurchase.itemID = Convert.ToInt64(HfItemsID.Value);
        
        blpurchase.Purchase = true;
        DataTable dtPurchase = blpurchase.SelectGriddata();
        blpurchase.Purchase = false;

        DataTable dt = blpurchase.SelectGriddata();
        foreach (DataRow row in dt.Rows)
        {

            TotalTaxPercentage += Convert.ToDecimal(row["TaxPercentage"].ToString());
            hfTotTaxPercentage.Value = Convert.ToString(TotalTaxPercentage);
        }
        foreach (DataRow row in dtPurchase.Rows)
        {

            if (Convert.ToBoolean(row["TaxIntraState"].ToString()) == true)
            {
                TotalTaxSales += Convert.ToDecimal(row["TaxPercentage"].ToString());
            }


            hfTotTaxsalesPercentage.Value = Convert.ToString(TotalTaxSales);
        }

        //   getpurchaseamount();

        ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "calculateActualQuantity();getSalesPrice(0);", true);
        ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "Total();", true);
        ddlUnit.Focus();



    }
    protected void SaveData()
    {
        long status = 0;
        BlReplace blReplace = new BlReplace();
        blReplace.RecivedDate = Convert.ToDateTime(txtDate.Text);
        blReplace.Linkbtn = Convert.ToBoolean(chkLinkbutton.Checked);
        blReplace.FK_Supplier = Convert.ToInt64(ddlSupplier.SelectedValue);
        blReplace.Fk_BillType = Convert.ToInt64(ddlBillType.SelectedValue);
        blReplace.BillNo = Convert.ToString(txtPurBillNo.Text);
        blReplace.FK_Store = Convert.ToInt64(ddlStore.SelectedValue);
        blReplace.InvoiceNo = txtInvoiceNo.Text.ToString().Trim();
        blReplace.InvoiceDate = Convert.ToDateTime(txtInvoiceDate.Text);
        blReplace.FK_Store = Convert.ToInt64(ddlStore.SelectedValue);
        blReplace.EnteredOn = Convert.ToDateTime(DateTime.Now);
        blReplace.FK_Client = Convert.ToInt64(hfClient.Value);
        blReplace.UserCode = hfuserName.Value;
        blReplace.FTotalAmount = Convert.ToDecimal(txtTotalAmount.Text);
        blReplace.FDiscPer = Convert.ToDecimal(txtDiscountP.Text);
        blReplace.FDiscAmnt = Convert.ToDecimal(txtDiscAmnt.Text);
        blReplace.FnetAmnt = Convert.ToDecimal(txtNetAmnt.Text);
        blReplace.TtotalAmount = Convert.ToDecimal(txtToTotal.Text == "" ? "0" : txtToTotal.Text);
        blReplace.TnetAmnt = Convert.ToDecimal(txtToNetAmount.Text == "" ? "0" : txtToNetAmount.Text);
        blReplace.Linkbtn = Convert.ToBoolean(chkLinkbutton.Checked);
        blReplace.TransactionID = Convert.ToInt64(ViewState["TransactionID"]);
        if (hfFK_PurchaseReturn.Value != "")
            blReplace.Fk_PurchaseReturn = Convert.ToInt64(hfFK_PurchaseReturn.Value);


        if (hfID_Replace.Value != "")
        {
            blReplace.MasterId = Convert.ToInt64(hfID_Replace.Value);
        }

        ArrayList arlist = new ArrayList();
        ArrayList arlt = new ArrayList();
        DataTable dtbl = (DataTable)ViewState[PurchaseReturnItems];
        int RowIndex = 0;
        foreach (GridViewRow gr in gvPurchaseReturn.Rows)
        {
           
            if (gr.RowType == DataControlRowType.DataRow)
            {
                IReplaceDetails IReplceValues = new IReplaceDetails();

                IReplceValues.FK_Item = Convert.ToInt64(gvPurchaseReturn.DataKeys[gr.RowIndex]["FK_Items"].ToString());
                IReplceValues.ExpiryDate = Convert.ToDateTime(gvPurchaseReturn.DataKeys[gr.RowIndex]["ExpiryDate"].ToString());
              //  IReplceValues.FK_Unit = Convert.ToInt64(gvPurchaseReturn.DataKeys[gr.RowIndex]["FK_Unit"].ToString());
                IReplceValues.FK_Unit = ddlUnit.SelectedIndex;
                IReplceValues.FK_Reason = Convert.ToInt64((((DropDownList)gvPurchaseReturn.Rows[RowIndex].FindControl("ddlReason")).SelectedValue));
                decimal decid = Convert.ToDecimal(((HiddenField)gr.FindControl("hdfQty")).Value);
                IReplceValues.BatchNo = ((HiddenField)gr.FindControl("hdfBatchNo")).Value;
                string str = (((Label)gr.FindControl("lblPurchaseRate")).Text);
                str = str == "" ? "0" : str;
                IReplceValues.PurchaseRate = Convert.ToDecimal(str);
                string str3 = (((HiddenField)gr.FindControl("hdfSalesPrice")).Value);
                str3 = str3 == "" ? "0" : str3;
                IReplceValues.SalesPrice = Convert.ToDecimal(str3);
                IReplceValues.FK_Reason = Convert.ToInt64((((DropDownList)gvPurchaseReturn.Rows[RowIndex].FindControl("ddlReason")).SelectedValue));
                string ActQ = (((Label)gr.FindControl("txtActQty")).Text);
                IReplceValues.ActQty = Convert.ToDecimal(ActQ == "" ? "0" : ActQ);
                IReplceValues.Free = Convert.ToDecimal(((Label)gr.FindControl("txtFreeQty")).Text);
                IReplceValues.MRP = Convert.ToDecimal(gvPurchaseReturn.DataKeys[gr.RowIndex]["MRP"].ToString());
                IReplceValues.Quantity = Convert.ToDecimal(((Label)gr.FindControl("txtQuantity")).Text);
                IReplceValues.PurchaseDiscAmnt = Convert.ToDecimal(((Label)gr.FindControl("lblDiscAmt")).Text);
                IReplceValues.Type = Convert.ToString("F");
                string strFK_Stock = (((HiddenField)gr.FindControl("hfStockID")).Value);
                strFK_Stock = strFK_Stock == "" ? "0" : strFK_Stock;
                IReplceValues.FK_Stock = Convert.ToInt64(strFK_Stock);
                string strsales = (((Label)gr.FindControl("lblSalesPrice")).Text);
                IReplceValues.SalesPrice = Convert.ToDecimal(strsales == "" ? "0" : strsales);
                //string hdfNetPurchase = (((HiddenField)gr.FindControl("hdfNetPurchase")).Value);
                HiddenField hdfNetPurchase = (HiddenField)gr.FindControl("hdfNetPurchase");
                string controlId = hdfNetPurchase.ClientID; // Use for debugging
                string hiddenFieldValue = hdfNetPurchase.Value;
                IReplceValues.NetPurchase = Convert.ToDecimal(hiddenFieldValue == "" ? "0" : hiddenFieldValue);
                IReplceValues.SlNo = RowIndex + 1;
                arlist.Add(IReplceValues);

                RowIndex = RowIndex + 1;


                IReplceValues = null;
            }
        }
        DataTable dtData = (DataTable)ViewState["TempTable"];
        for (int i = 0; i < dtData.Rows.Count; i++)
        {

            IReplaceDetails IReplceValues = new IReplaceDetails();
            IReplceValues.SlNo = Convert.ToInt32(i + 1);
            // IReplceValues.MRP = Convert.ToDecimal(dtData.Rows[i]["MRP"].ToString());
            IReplceValues.Type = Convert.ToString("T");
            IReplceValues.MRP = Convert.ToDecimal(dtData.Rows[i]["MRP"].ToString());
            IReplceValues.FK_Item = Convert.ToInt32(dtData.Rows[i]["FK_Items"].ToString());
            IReplceValues.Quantity = Convert.ToDecimal(dtData.Rows[i]["Quantity"].ToString());
            IReplceValues.Free = Convert.ToDecimal(dtData.Rows[i]["Free"].ToString());
            IReplceValues.ActQuantity = Convert.ToDecimal(dtData.Rows[i]["ActQty"].ToString());
            IReplceValues.FK_Unit = Convert.ToInt32(dtData.Rows[i]["FK_Unit"].ToString());
            IReplceValues.FK_SubUnit = Convert.ToInt32(dtData.Rows[i]["FK_SubUnit"].ToString());
            IReplceValues.BatchNo = Convert.ToString(dtData.Rows[i]["BatchNo"].ToString());
            IReplceValues.PurchaseRate = Convert.ToDecimal(dtData.Rows[i]["PurchaseRate"].ToString());
            IReplceValues.SalesPrice = Convert.ToDecimal(dtData.Rows[i]["SalePrice"].ToString());
            IReplceValues.PurchaseDiscper = Convert.ToDecimal(dtData.Rows[i]["PurchaseDiscpercentage"].ToString());
            IReplceValues.PurchaseDiscAmnt = Convert.ToDecimal(dtData.Rows[i]["PurchaseDiscAmount"].ToString());
            IReplceValues.Discper = Convert.ToDecimal(dtData.Rows[i]["DiscountPer"].ToString());
            IReplceValues.DiscAmnt = Convert.ToDecimal(dtData.Rows[i]["DiscountAmt"].ToString());
            IReplceValues.ExpiryDate = Convert.ToDateTime(dtData.Rows[i]["ExpiryDate"].ToString());
            IReplceValues.HSN = Convert.ToString(dtData.Rows[i]["HSN"].ToString());
            IReplceValues.PurchaseTaxAmount = Convert.ToDecimal(dtData.Rows[i]["PurchasetaxAmount"].ToString());
            IReplceValues.NetPurchase = Convert.ToDecimal(dtData.Rows[i]["NetPurchase"].ToString());
            IReplceValues.ProfitPer = Convert.ToDecimal(dtData.Rows[i]["Profitper"].ToString());
            IReplceValues.ProfitAmnt = Convert.ToDecimal(dtData.Rows[i]["ProfitAmount"].ToString());
            IReplceValues.IncludePtax = Convert.ToBoolean(dtData.Rows[i]["IncludePtax"].ToString());
            IReplceValues.IncludeStax = Convert.ToBoolean(dtData.Rows[i]["IncludeStax"].ToString());
            IReplceValues.DiscountOntax = Convert.ToBoolean(dtData.Rows[i]["DiscountOntax"].ToString());
            IReplceValues.SalesTaxAmnt = Convert.ToDecimal(dtData.Rows[i]["SaletaxAmount"].ToString());
            IReplceValues.TaxPer = Convert.ToDecimal(dtData.Rows[i]["TaxPer"].ToString());
            // IReplceValues.FK_Stock = Convert.ToInt64(dtData.Rows[i]["FK_Stock"].ToString());
            decimal ActPur = 0;

            if (IReplceValues.IncludePtax)
            {
                ActPur = Math.Round(((IReplceValues.PurchaseRate * 100) / (100 + IReplceValues.TaxPer)), 4);
            }
            else
            {
                ActPur = IReplceValues.PurchaseRate;
                //IMaterialRecieptDetails.ActualPurchaseRate = ActPur - singleAmnt;
            }


            IReplceValues.ActualPurchaseRate = ActPur;
            //IReplceValues.Type = Convert.ToString("T");
            arlt.Add(IReplceValues);
            IReplceValues = null;
        }

        blReplace.TDiscAmnt = Convert.ToDecimal(hfTotalDiscAmnt.Value == "" ? "0" : hfTotalDiscAmnt.Value);
        blReplace.TDiscPer = (Convert.ToDecimal(hfTotalDiscAmnt.Value == "" ? "0" : hfTotalDiscAmnt.Value)) * (100) / (Convert.ToDecimal(txtToTotal.Text == "" ? "0" : txtToTotal.Text));
        blReplace.xmlPurchseValues = Common.WriteToXml(arlist);
        blReplace.xmlValues = Common.WriteToXml(arlt);

        if (hfID_Replace.Value == "")
        {
            if (hfIsAdd.Value.ToLower() == "true")
            {
                status = blReplace.InsertData();
                if (status > 0)
                {
                    //ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "alertify.alert('Reference Number" + status + "')", true);

                    ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "toastr.success('Data inserted successfully');alertify.alert('Reference Number:  " + status + "');", true);


                    ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "ClearAdd();", true);
                    ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "ClearAdd();", true);
                    Clear();
                    Populate populate = new Populate();
                    ddlSupplier.Focus();
                    ddlStore.SelectedValue = Session["mainstore"].ToString();
                    ddlStore.Enabled = false;
                }

                else
                {
                   // ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "ShowAddDiv();", true);

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

                blReplace.MasterId = Convert.ToInt64(hfID_Replace.Value);
                status = blReplace.UpdateData();
                if (status > 0)
                {

                    ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "toastr.success('Data updated successfully')", true);

                    ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "ClearAdd();", true);
                    ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "ClearAdd();", true);
                    Clear();
                    Populate populate = new Populate();
                    ddlSupplier.Focus();
                    ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "alert('Reference Number" + status + "')", true);

                }
                else if (status == -1)
                {

                    ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "toastr.error('Cannot update data.. ')", true);

                }
                else if (status == -2)
                {

                    ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "toastr.error('Cannot update data.. ')", true);

                }

            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "toastr.warning('User does not have permission to Update')", true);

            }
        }
        blReplace = null;
    }
    public void Clear()
    {
        Transactionchecking();
        ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "ClearAdd();", true);
        ViewState["TempTable"] = null;
        ViewState[PurchaseReturnItems] = null;
        gvItems.DataSource = null;
        gvItems.DataBind();
        gvPurchaseReturn.DataSource = null;
        gvPurchaseReturn.DataBind();
        txtTotalAmount.Text = "0";
        txtDiscountP.Text = "0";
        txtDiscAmnt.Text = "0";
        txtRoundOff.Text = "0";
        txtNetAmnt.Text = "0";
        txtToTotal.Text = "0";
        txtToRoundOff.Text = "0";
        txtToNetAmount.Text = "0";
        txtInvoiceNo.Text = "";
        ddlSupplier.SelectedValue = "0";
        txtInvoiceNo.Enabled = true;
        txtDate.Enabled = true;
        txtPurBillNo.Enabled = true;
        txtInvoiceDate.Enabled = true;
        ddlStore.Enabled = true;
        ddlSupplier.Enabled = true;
        ddlBillType.Enabled = true;
        chkLinkbutton.Enabled = true;
        chkLinkbutton.Checked = false;
        btnAddItems.Enabled = true;
        txtPurBillNo.Text = "";
        FillBillNo();
        btnApprove.Visible = false;
        gvItems.Enabled = true;
        gvPurchaseReturn.Enabled = true;
        btnSubmit.Visible = true;
    }
    protected void GridFill(int currentPage)
    {
        BlReplace blreplace = new BlReplace();
        DataTable dtbl = new DataTable();
        blreplace.MasterId = 0;
      
        blreplace.FK_Supplier = Convert.ToInt64(ddlSearchSupplier.SelectedValue==""?"0":ddlSearchSupplier.SelectedValue);
        //blreplace.FK_Store = Convert.ToInt64(Session["mainstore"].ToString());
        blreplace.FK_Store = Convert.ToInt64(ddlStoreSearch.SelectedValue == "" ? "0" : ddlStoreSearch.SelectedValue);
        blreplace.FK_Client = Convert.ToInt64(hfClient.Value);
        blreplace.FromDate = Convert.ToDateTime(txtFromDateSearch.Text == "" ? "" + DateTime.Now.Date + "" : txtFromDateSearch.Text);// DateTime.ParseExact(txtFromDateSearch.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
        blreplace.ToDate = Convert.ToDateTime(txtToDateSearch.Text == "" ? "" + DateTime.Now.Date + "" : txtToDateSearch.Text);// DateTime.ParseExact(txtToDateSearch.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
        blreplace.ApproveorPending = Convert.ToInt16(ddlStatusApproveSearch.SelectedValue);
        dtbl = blreplace.SelectAllData();
        grdReplace.DataSource = dtbl;
        grdReplace.DataBind();

        blreplace = null;

    }
    protected void FillBillNo()
    {
        BlReplace blreplace = new BlReplace();
        DataTable dt = new DataTable();
        dt = blreplace.SelectBillNo();
        txtPurBillNo.Text = dt.Rows[0]["BillNo"].ToString();
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

            if (Convert.ToInt64(blSub.NoofUnits) > 0)
                txtActq.Text = Convert.ToString(((Convert.ToDecimal(txtQuantity.Text == "" ? "0" : txtQuantity.Text)) + (Convert.ToDecimal(txtFree.Text == "" ? "0" : txtFree.Text))) * (Convert.ToDecimal(blunit.NoofUnits)) * (Convert.ToDecimal(blSub.NoofUnits)));
            else
                txtActq.Text = Convert.ToString(((Convert.ToDecimal(txtQuantity.Text == "" ? "0" : txtQuantity.Text)) + (Convert.ToDecimal(txtFree.Text == "" ? "0" : txtFree.Text))) * (Convert.ToDecimal(blunit.NoofUnits)));

            hfSubUnitQuantity.Value = Convert.ToString(blSub.NoofUnits);

            hfUnitQuantity.Value = Convert.ToString(blunit.NoofUnits);
            blunit = null;


        }

        ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "Total();", true);

    }
    private void DisplayDiv()
    {
        if (chkLinkbutton.Checked == true)
        {
            imgbtnPurReturn.Enabled = true;
            txtPurRefno.Enabled = true;
            EnableItem(false);
            EnableTotalTextBox(true);
            imgbtnPurReturn.Focus();
        }
        else
        {
            imgbtnPurReturn.Enabled = false;
            txtPurRefno.Enabled = false;
            gvPurchaseReturn.DataSource = null;
            gvPurchaseReturn.DataBind();
            ViewState[PurchaseReturnItems] = null;
            EnableItem(true);
            EnableTotalTextBoxNormal();
            ddlSupplier.Focus();
            ddlSupplier.SelectedValue = "0";
            txtTotalAmount.Text = "0";
            txtDiscountP.Text = "0";
            txtDiscAmnt.Text = "0";
            txtNetAmnt.Text = "0";
            txtRoundOff.Text = "0";


        }

    }
    protected void PreviousData()
    {
        BlMaterialReciept blMaterialReciept = new BlMaterialReciept();
        BlPurchase blpurchase = new BlPurchase();
        blMaterialReciept.FK_Items = Convert.ToInt64(HfItemsID.Value.ToString());
        DataTable dtPrevious = new DataTable();
        dtPrevious = blMaterialReciept.PreviousData();
        if (dtPrevious.Rows.Count > 0)
        {
            ddlUnit.SelectedValue = dtPrevious.Rows[0]["FK_Units"].ToString();
            ddlSubunit.SelectedValue = dtPrevious.Rows[0]["FK_SubUnit"].ToString();
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
    protected void btnItem_Click(object sender, EventArgs e)
    {
        if (hfType.Value == "1")
        {
            ddlItemsPurReturnFill();
        }
        else if (hfType.Value == "2")
        {
            ddlItemsFill();
            PreviousData();

        }
    }
    private void EnableTotalTextBoxNormal()
    {
        txtTotalAmount.ReadOnly = txtNetAmnt.ReadOnly = txtRoundOff.ReadOnly = txtDiscAmnt.ReadOnly = true;
        txtDiscountP.ReadOnly = false;
    }
    private void EnableTotalTextBox(bool TextValue)
    {
        txtTotalAmount.ReadOnly = txtDiscAmnt.ReadOnly = txtDiscountP.ReadOnly = txtNetAmnt.ReadOnly = txtRoundOff.ReadOnly = TextValue;
    }
    public void PurchaseReturnFill(int pageIndex)
    {
        ClearPurchase();
        Populate Populate = new Populate();
        CommonPopup1.CallingFunction = "PurchaseReturnFill";
        string strName = ((TextBox)CommonPopup1.FindControl("txtName")).Text;
        string Refno = ((TextBox)CommonPopup1.FindControl("txtRefno")).Text;
        string searchCol = ((HiddenField)CommonPopup1.FindControl("hfColumn")).Value;
        searchCol = searchCol == string.Empty ? "[Name]" : "[" + searchCol + "]";

        Refno = Refno == string.Empty || Refno == null ? "0" : Refno;

        //Populate.FillGrid((GridView)CommonPopup1.FindControl("gdvSearch"), "View_Purchase inner join BillType on ID_BillType=FK_BillType", "ID_Purchase as ID,'[' + InvoiceNo  +'][' + CONVERT(VARCHAR(14),BillDate,103) +']' as Name,(select SupplierName from  Supplier where ID_Supplier=FK_Supplier) as Supplier,InvoiceNo,NetAmount, ID_Purchase As [Ref No]", "ID_Purchase", "BillDate", "(Quantity>0 or Free>0) and View_Purchase.FK_Client=" + hfClient.Value.Split(',')[0] + " and FK_Store=" + ddlStore.SelectedValue + " and FK_BillType=" + ddlBillType.SelectedValue + " AND (ID_Purchase =" + Refno + " OR 0=" + Refno + ") ", searchCol + "  LIKE '" + strName + "%'", pageIndex, 10, "", (Repeater)CommonPopup1.FindControl("rptPager"), this.Page);
        Populate.FillPopUpGrid((GridView)CommonPopup1.FindControl("gdvSearch"), "PurchaseReturn left Join  Supplier On Supplier.ID_Supplier=PurchaseReturn.FK_Supplier ", "ID_PurchaseReturn as ID,BillNo as Name,convert(varchar(14),BillDate,103)as BillDate ,Supplier.SupplierName as Supplier,Invoice", "ID_PurchaseReturn", "ID_PurchaseReturn ", "PurchaseReturn.FK_Client=" + hfClient.Value.Split(',')[0] + " AND PurchaseReturn.Cancelled=0 and BillNo", strName, null);
        GridView gvNew = (GridView)CommonPopup1.FindControl("gdvSearch");
        gvNew.Columns[1].HeaderText = "Bill No";
        Populate = null;

        if (txtPurRefno.Text == string.Empty)
        {
            lblPopupHeader.Text = "Purchase Return";
            mpCommon.Show();
        }

    }
    public void SetDetails(string strName, string strRefno)
    {
        BindPurchaseItems((Convert.ToInt64(strRefno)));
    }
    private void ClearPurchase()
    {

        txtPurRefno.Text = "";

    }
    protected void ddlItemsPurReturnFill()
    {

        BlStock blStock = new BlStock();
            blStock.MasterId = Convert.ToInt64(hdfFKStock.Value);
        blStock.UserCode = hfUser.Value;
        blStock.FK_Client = Convert.ToInt64(hfClient.Value);
        blStock.SelectData();

        txtPurchaseRateItem.Text = blStock.PurRate.ToString();
        txttaxAmnt.Text = blStock.PurchaseTaxAmount.ToString();
       // hfItemId.Value = blStock.FK_Items.ToString();
        txtTotalAmountItem.Text = "0";
        txtCurrentStock.Text = blStock.CurrentStock.ToString();
        blStock = null;
        txttaxAmnt.Focus();
        txtSalesPrice.Text = "0";

        BlPurchase blpurchase = new BlPurchase();
        blpurchase.itemID = Convert.ToInt64(hdfFkItem.Value);
        blpurchase.Purchase = true;
        DataTable dtPurchase = blpurchase.SelectGriddata();
        blpurchase.Purchase = false;

        DataTable dt = blpurchase.SelectGriddata();
        foreach (DataRow row in dt.Rows)
        {

            TotalFromTaxPercentage += Convert.ToDecimal(row["TaxPercentage"].ToString());
            hfFromTotTaxPercentage.Value = Convert.ToString(TotalFromTaxPercentage);
        }
        foreach (DataRow row in dtPurchase.Rows)
        {

            if (Convert.ToBoolean(row["TaxIntraState"].ToString()) == true)
            {
                TotalFromTaxSales += Convert.ToDecimal(row["TaxPercentage"].ToString());
            }


            hfFromTotTaxsalesPercentage.Value = Convert.ToString(TotalFromTaxSales);
        }

    }
    private DataTable GetDataTable(DataTable dtbl)
    {
        dtbl.Columns.Add("SLNO", typeof(int));
        dtbl.Columns.Add("Item");
        dtbl.Columns.Add("FK_Items");
        dtbl.Columns.Add("BatchNo");
        dtbl.Columns.Add("PurchaseRate");
        dtbl.Columns.Add("FK_Unit");
        dtbl.Columns.Add("Unit");
        dtbl.Columns.Add("ExpiryDate");
        dtbl.Columns.Add("Quantity");
        dtbl.Columns.Add("TotalAmt", typeof(decimal));
        dtbl.Columns.Add("FK_Reason");
        dtbl.Columns.Add("ActQty");
        dtbl.Columns.Add("FreeQty");
        dtbl.Columns.Add("Free");
        dtbl.Columns.Add("MRP");
        dtbl.Columns.Add("SalesPrice");
        dtbl.Columns.Add("FK_Stock");
        dtbl.Columns.Add("NoofUnits");
        dtbl.Columns.Add("PurchaseTaxAmount");
        dtbl.Columns.Add("PurchaseDiscPercentage");
        dtbl.Columns.Add("PurchaseDiscAmnt");
        dtbl.Columns.Add("SingleDisAmnt");
        dtbl.Columns.Add("Discount", typeof(decimal));
        dtbl.Columns.Add("DiscountAmt", typeof(decimal));
        dtbl.Columns.Add("ActualPurRate");
        dtbl.Columns.Add("ActualSalesPrice");
        dtbl.Columns.Add("NetAmount", typeof(decimal));
        dtbl.Columns.Add("NetPurchase", typeof(decimal));
        dtbl.Columns.Add("CurrentStock", typeof(decimal));
        return dtbl;
    }
    protected int AddDataItem()
    {
        DataTable dtbl = new DataTable();
        int inRowCount = 0;
        int inCheck = 0;
        if (ViewState[PurchaseReturnItems] != null)
        {
            dtbl = (DataTable)ViewState[PurchaseReturnItems];
            inRowCount = dtbl.Rows.Count;
        }
        else
        {

            dtbl = GetDataTable(dtbl);
        }
        if (hfItemId.Value != "")
        {

            gvPurchaseReturn.Enabled = true;
            dtbl.Rows.RemoveAt(Convert.ToInt32(hfItemId.Value));
            gvPurchaseReturn.DataSource = dtbl;
            gvPurchaseReturn.DataBind();
            hfSlNo.Value = hfItemId.Value;
            hfItemId.Value = "";



        }
        if (dtbl.Rows.Count > 0)
        {

            for (int i = 0; i < dtbl.Rows.Count; i++)
            {
                if (dtbl.Rows[i]["FK_Stock"].ToString() == hdfFKStock.Value)
                {
                    inCheck = inCheck + 1;
                    break;
                }
               // dtbl.Rows[i]["FK_Reason"] = ((DropDownList)gvPurchaseReturn.Rows[i].FindControl("ddlReason")).SelectedValue;
            }

        }
        if (inCheck == 0)
        {
            string Unit = "";
            decimal PurchaseRate = 0;
            string BatchNo = "";
            decimal PurVAT = 0;
            decimal MRP = 0;
            decimal SalesPrice = 0;
            decimal CESS = 0;
            DateTime ExpiryDate;
            long FK_Unit = 0;
            int NoOfUnit = 0;
            decimal sal = 0;
            BlStock blStock = new BlStock();
            blStock.MasterId = Convert.ToInt64(hdfFKStock.Value);
            blStock.UserCode = hfUser.Value;
            blStock.FK_Client = Convert.ToInt64(hfClient.Value);
            blStock.SelectData();
            BatchNo = blStock.BatchNo;
            ExpiryDate = blStock.ExpiryDate;
            FK_Unit = blStock.FK_Units;
            Unit = blStock.Unit;
            NoOfUnit = blStock.NoOfUnits;
            MRP = blStock.MRP;
            sal = blStock.SalePrice;
            PurchaseRate = Convert.ToDecimal(txtPurchaseRateItem.Text);
            decimal decqty = Math.Round(Convert.ToDecimal(txtPurQty.Text == "" ? "0" : txtPurQty.Text), 4);
            decimal Total = (decqty * PurchaseRate);
            decimal Free = Math.Round(Convert.ToDecimal(txtPurFree.Text == "" ? "0" : txtPurFree.Text), 4);
            DataRow drow = dtbl.NewRow();
           // drow["slno"] = inRowCount + 1;
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
            drow["Item"] = hfItemName.Value;
            drow["FK_Items"] = hdfFkItem.Value;
            drow["Quantity"] = decqty;
            drow["PurchaseRate"] = PurchaseRate;
            drow["Unit"] = Unit;
            drow["FK_Unit"] = FK_Unit;
            drow["BatchNo"] = BatchNo;
            drow["ExpiryDate"] = blStock.ExpiryDate;
            drow["TotalAmt"] = Math.Round(Total, 4);
            drow["FK_Reason"] = "0";
            drow["FreeQty"] = (txtFree.Text == "" ? "0" : txtFree.Text);
            drow["Free"] = (txtPurFree.Text == "" ? "0" : txtPurFree.Text);
            drow["ActQty"] = (decqty + Free) * NoOfUnit;
            drow["PurchaseTaxAmount"] = (txttaxAmount.Text == "" ? "0" : txttaxAmount.Text);
            drow["FK_Stock"] = hdfFKStock.Value;
            drow["PurchaseDiscAmnt"] = (txtDiscAmntFrom.Text == "" ? "0" : txtDiscAmntFrom.Text);
            drow["SingleDisAmnt"] = "0";
            drow["Discount"] = "0";
            drow["DiscountAmt"] = "0";
            drow["ActualPurRate"] = "0";
            drow["ActualSalesPrice"] = "0";
            decimal NetAmnt = Math.Round(Total, 4);
            drow["NetAmount"] = NetAmnt;


            drow["MRP"] = MRP;
            drow["SalesPrice"] = sal;
            drow["FK_Stock"] = hdfFKStock.Value;
            drow["NoofUnits"] = "0";
            drow["PurchaseDiscPercentage"] = "0";
            drow["CurrentStock"] = hdfCurrentStock.Value;
            dtbl.Rows.InsertAt(drow, inRowCount);
            dtbl.DefaultView.Sort = "SLNO";
            dtbl = dtbl.DefaultView.ToTable();
            gvPurchaseReturn.DataSource = dtbl;
            gvPurchaseReturn.DataBind();
            foreach (GridViewRow row in gvPurchaseReturn.Rows)
            {
                ((Label)row.FindControl("txtActQty")).Visible = false;
              //  ((Label)row.FindControl("txtQuantity")).ReadOnly = true;
               // ((Label)row.FindControl("lblDiscAmt")).ReadOnly = true;

            }
            gvPurchaseReturn.Columns[4].Visible = false;
            gvPurchaseReturn.Columns[9].Visible = false;
            TotalAmount(dtbl);
            ViewState[PurchaseReturnItems] = dtbl;
            FillReasonddl(false);
            hfSlNo.Value = "";
            hdfFKStock.Value = "0";
            hfItemName.Value = "";

        }
        else
        {
            ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "alertify.alert('Item already exists')", true);
            return 1;
        }
        return 0;

    }
    protected void TotalAmount(DataTable dtbl)
    {
        try
        {
            if (dtbl.Rows.Count > 0)
            {
                if (txtPurRefno.Text != "")
                {
                    ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "CalculateTotalAmount('txtActQty')", true);
                }
                else
                {
                    txtTotalAmount.Text = Math.Round(decimal.Parse((dtbl.Compute("Sum(TotalAmt)", "")).ToString()), 4).ToString();

                    // decimal DiscountAmt = Math.Round(((Convert.ToDecimal(txtTotalAmount.Text)) * (Convert.ToDecimal(txtDiscountP.Text == "" ? "0" : txtDiscountP.Text)) / 100), 4);
                    //txtDiscAmnt.Text = Math.Round(Convert.ToDecimal(dtbl.Rows[0]["DiscountAmt"].ToString()), 4).ToString();
                    //txtDiscountP.Text = Math.Round(Convert.ToDecimal(dtbl.Rows[0]["Discount"].ToString()), 4).ToString();
                    //txtNetAmnt.Text = Math.Round(Convert.ToDecimal(dtbl.Rows[0]["NetAmount"].ToString()), 4).ToString();

                    txtDiscAmnt.Text = (Math.Round(decimal.Parse((dtbl.Compute("Sum(DiscountAmt)", "")).ToString()), 4)).ToString();
                    txtDiscountP.Text = Math.Round(decimal.Parse((dtbl.Compute("Sum(Discount)", "")).ToString()), 4).ToString();
                    txtNetAmnt.Text = Math.Round(decimal.Parse((dtbl.Compute("Sum(NetAmount)", "")).ToString()), 2).ToString();

                    ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "Total()", true);
                    if (chkLinkbutton.Checked == true)
                        EnableTotalTextBox(true);
                    else
                        EnableTotalTextBoxNormal();
                }

            }
            else
            {
                ClearTotal();
            }

        }
        catch (Exception ex)
        {
            ClearTotal();
        }
    }


    protected void ClearTotal()
    {
        txtTotalAmount.Text = "0";
        txtDiscountP.Text = "0";
        txtDiscountAmt.Text = "0";
        txtNetAmnt.Text = "0";
    }

    private void EnableItem(Boolean Value)
    {
        txtitem.Visible = ImageButton3.Visible = txtPurQty.Visible = txtPurFree.Visible = txtPurchaseRateItem.Visible =
            txttaxAmnt.Visible = btnPtax.Visible = txtCurrentStock.Visible = txtTotalAmountItem.Visible = btnAddItems.Visible =
            btnClearItems.Visible = lblItem.Visible = lblTaxAmnt.Visible = lblqty.Visible = lblFree.Visible =
            lblPurRate.Visible = lblTotAmnt.Visible = lblCurrentStock.Visible = Value;
    }
    #endregion Functions
    #region Events
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
                divView.Style.Add("display", "none;");
                CheckUserAccess();
                SetReadonly();
                hfuserName.Value = Session["Username"].ToString();
                BindData();
                hfItemId.Value = "";
                DisplayDiv();
                txtPurBillNo.Enabled = false;
                FillBillNo();
                ddlStore.SelectedValue = Session["mainstore"].ToString();
                ddlStore.Enabled = false;
                ddlBillType.Focus();
                CalendarExtender3.EndDate = DateTime.Now;
                txtDate_CalendarExtender.EndDate = DateTime.Now;
                // GridFill(1);
                hfStore.Value = ddlStore.SelectedValue;
                if (Convert.ToBoolean(hfIsAdd.Value) == false)
                {
                    ((Label)CommonPopupPrivilege.FindControl("popuplabel")).Text = "User does not have permission to Add";
                    mpPrivilege.Show();
                }
                if (Convert.ToBoolean(hfIsUpdate.Value) == false)
                {
                    grdReplace.Columns[6].Visible = false;
                }
                if (Convert.ToBoolean(hfIsDelete.Value) == false)
                {
                    grdReplace.Columns[7].Visible = false;
                }
                Transactionchecking();
            }
        }
        catch (Exception ex)
        {
            Populate.WarningMsg(this, ex.ToString());
        }

    }
    protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            Clear();
            if (Convert.ToBoolean(hfIsAdd.Value) == false)
            {
                ((Label)CommonPopupPrivilege.FindControl("popuplabel")).Text = "User does not have permission to Add";
                mpPrivilege.Show();
            }
            divAdd.Style.Add("display", "block");
            divView.Style.Add("display", "none");
            FillBillNo();
            DisplayDiv();
        }
        catch (Exception ex)
        {
            //blMessage.Items.Add(ex.Message);
            ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "toastr.warning('" + ex.Message + "')", true);
        }
    }

    protected void imgbtnView_Click(object sender, ImageClickEventArgs e)
    {
        divAdd.Style.Add("display", "none");
        divView.Style.Add("display", "block");
        ViewState["TempTable"] = null;
        ViewState[PurchaseReturnItems] = null;
        gvPurchaseReturn.DataSource = null;
        gvItems.DataSource = null;
        ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "ClearAdd();ClearSearch();", true);
        GridFill(1);
        gvItems.Enabled = true;

    }

    protected void ddlCategory_SelectedIndexChanged(object sender, EventArgs e)
    {

    }

    protected void ddlItem_SelectedIndexChanged(object sender, EventArgs e)
    {


        if (hfType.Value == "1")
        {
            ddlItemsPurReturnFill();
        }
        else if (hfType.Value == "2")
        {
            ddlItemsFill();
        }

    }

    protected void ddlUnit_SelectedIndexChanged(object sender, EventArgs e)
    {
        //btnHiddenActualQuantityfromUnit_Click(sender, e);
        CalculateActualQuantity();
        txtBatch.Focus();
        ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "Total()", true);

    }
    protected void btnHiddenActualQuantityfromUnit_Click(object sender, EventArgs e)
    {
        try
        {

            CalculateActualQuantity();
            txtBatch.Focus();
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "toastr.warning('" + ex.Message + "')", true);
        }
    }


    protected void ddlSubunit_SelectedIndexChanged(object sender, EventArgs e)
    {
        CalculateActualQuantity();


    }
    protected void btnQuation_Click(object sender, EventArgs e)
    {

    }
    protected void btnHiddenActullessQuantity_Click(object sender, EventArgs e)
    {

    }
    protected void btnHiddenlessQuantity_Click(object sender, EventArgs e)
    {

    }
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        try
        {

            string aa = hfpurrate.Value;

            DateTime dtExpiry = DateTime.ParseExact(txtExpiryDate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);


            DateTime dtNow = Convert.ToDateTime(hfDate.Value);
            if (!string.IsNullOrWhiteSpace(hfItem.Value) && hfItem.Value.Trim() == txtItmShortName.Text.ToString().Trim())
            {
                int days = Convert.ToInt32(hfExpirydays.Value.ToString() == "" ? "0" : hfExpirydays.Value.ToString()) + (30);
                DateTime date = dtNow.AddDays(days);
                if (dtExpiry < date)
                    ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "toastr.error('Expirydate should be greater than Item Expiry Days')", true);
                //ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "alertify.alert('Expirydate should be greater than Item Expiry Days')", true);
                else
                    if (dtExpiry < dtNow)
                    {
                        if (Validate())
                        {
                            AddData();
                        }


                    }
                    else
                    {
                        if (Validate())
                        {
                            AddData();
                        }

                    }
                ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "Total()", true);
            }

            else
            {
                ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "toastr.warning('Item  does not exist')", true);
                ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "Total();", true);
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "toastr.warning('" + ex.Message + "')", true);
        }


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
        return St;

    }
    protected void btnClearAdd_Click(object sender, EventArgs e)
    {
        hfItemId.Value = "";
        if (hfItemId.Value == "")
        {
            gvItems.Enabled = true;

        }
        ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "Total();", true);
    }
    protected void btnHiddenActulaQuantity_Click(object sender, EventArgs e)
    {

    }
    protected void btnS_Click(object sender, EventArgs e)
    {

        BlPurchase blpurchase = new BlPurchase();
        DataTable dt;

        Populate Populate = new Populate();
        CommonPopupGrid1.CallingFunction = "Purchase";
        string Refno = ((TextBox)CommonPopupGrid1.FindControl("txtRefno")).Text;
        string searchCol = ((HiddenField)CommonPopupGrid1.FindControl("hfColumn")).Value;

        Refno = Refno == string.Empty || Refno == null ? "0" : Refno;
        GridView gvNew = (GridView)CommonPopupGrid1.FindControl("gdvSearch");
        if (HfItemsID.Value != "")
            blpurchase.itemID = Convert.ToInt64(HfItemsID.Value.ToString());
        blpurchase.Purchase = true;
        dt = blpurchase.SelectGriddata();
        dt.Columns.Add("TaxAmount", typeof(decimal));
        foreach (DataRow row in dt.Rows)
        {
            TaxAmount = 0;
            if (Convert.ToBoolean(row["TaxInterState"]) == true)
            {

                TaxAmount = Math.Round(Convert.ToDecimal(row["TaxPercentage"]) / Convert.ToDecimal(hfTotTaxPercentage.Value) * (Convert.ToDecimal(txttaxAmount.Text == "" ? "0" : txttaxAmount.Text)), 4);
                // TaxAmount = (Convert.ToDecimal(row["TaxPercentage"]) / (TotalTaxPercentage) * (Convert.ToDecimal(txttaxAmount.Text == "" ? "0" : txttaxAmount.Text)));
            }
            else if (Convert.ToBoolean(row["TaxIntraState"]) == true)
            {
                TaxAmount = Math.Round(Convert.ToDecimal(row["TaxPercentage"]) / Convert.ToDecimal(hfTotTaxPercentage.Value) * (Convert.ToDecimal(txttaxAmount.Text == "" ? "0" : txttaxAmount.Text)), 4);

            }

            row["TaxAmount"] = TaxAmount;
        }
        gvNew.DataSource = dt;
        gvNew.DataBind();
        gvNew.Columns[3].Visible = false;

        lblPopupH.Text = "Purchase Tax";
        mpGrid.Show();


    }


    protected void btSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            // ClearSupplier();
            Populate Populate = new Populate();
            BlPurchase blpurchase = new BlPurchase();
            DataTable dt;
            //if(ViewState["MasterIDTaxDetails"]!=null)
            // MasterID= Convert.ToInt64(ViewState["MasterIDTaxDetails"].ToString());
            CommonPopupGrid1.CallingFunction = "Sales";
            //  string strName = ((TextBox)CommonPopup2.FindControl("txtName")).Text;
            string Refno = ((TextBox)CommonPopupGrid1.FindControl("txtRefno")).Text;
            string searchCol = ((HiddenField)CommonPopupGrid1.FindControl("hfColumn")).Value;
            Refno = Refno == string.Empty || Refno == null ? "0" : Refno;
            GridView gvNew = (GridView)CommonPopupGrid1.FindControl("gdvSearch");
            blpurchase.itemID = Convert.ToInt64(HfItemsID.Value.ToString());

            dt = blpurchase.SelectGriddata();
            dt.Columns.Add("TaxAmount", typeof(decimal));

            gvNew.DataSource = dt;
            gvNew.DataBind();
            gvNew.Columns[3].Visible = false;

            lblPopupH.Text = "Sales Tax";
            mpGrid.Show();
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "toastr.warning('" + ex.Message + "')", true);
        }
        // ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "CalculateAmount('id','txtQuantity')", true);
    }
    protected void gvItems_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        try
        {

            hfGvItemSelected.Value = "1";
            if (gvItems.Rows.Count > 0)
            {
                DataTable dtbl = new DataTable();
                if (ViewState["TempTable"] != null)
                {
                    dtbl = (DataTable)ViewState["TempTable"];

                    ddlItem.SelectedValue = Convert.ToString(((Label)gvItems.Rows[e.NewSelectedIndex].FindControl("lblgvFK_Items")).Text);
                    txtItmShortName.Text = Convert.ToString(((Label)gvItems.Rows[e.NewSelectedIndex].FindControl("lblgvItem")).Text);
                    ddlUnit.SelectedValue = Convert.ToString(((Label)gvItems.Rows[e.NewSelectedIndex].FindControl("lblgvFK_Units")).Text);
                    ddlCategory.SelectedValue = Convert.ToString(((Label)gvItems.Rows[e.NewSelectedIndex].FindControl("lblgvFK_Category")).Text);
                    ddlSubunit.SelectedValue = Convert.ToString(((Label)gvItems.Rows[e.NewSelectedIndex].FindControl("lblgvFK_SubUnit")).Text);
                    txtBatch.Text = Convert.ToString(((Label)gvItems.Rows[e.NewSelectedIndex].FindControl("lblgvBatchNo")).Text);
                    txtExpiryDate.Text = Convert.ToString(((Label)gvItems.Rows[e.NewSelectedIndex].FindControl("lblgvExpiryDate")).Text).Substring(0, 10);
                    txtHSNSAC.Text = Convert.ToString(((Label)gvItems.Rows[e.NewSelectedIndex].FindControl("lblgvhsn")).Text);
                    txtQuantity.Text = Convert.ToString(((Label)gvItems.Rows[e.NewSelectedIndex].FindControl("lblgvquty")).Text);
                    txtFree.Text = Convert.ToString(((Label)gvItems.Rows[e.NewSelectedIndex].FindControl("lblgvFree")).Text);
                    txtActq.Text = Convert.ToString(((Label)gvItems.Rows[e.NewSelectedIndex].FindControl("lblgvActQty")).Text);
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
                    txtDiscountPer.Text = Convert.ToString(((Label)gvItems.Rows[e.NewSelectedIndex].FindControl("lblgvDiscountPer")).Text);
                    txtDiscountAmt.Text = Convert.ToString(((Label)gvItems.Rows[e.NewSelectedIndex].FindControl("lblgvDiscountAmt")).Text);
                    ChkIncStax.Checked = Convert.ToBoolean(((Label)gvItems.Rows[e.NewSelectedIndex].FindControl("lblgvIncludeStax")).Text);
                    txtSTaxAmount.Text = Convert.ToString(((Label)gvItems.Rows[e.NewSelectedIndex].FindControl("lblgvSalesTaxAmt")).Text);
                    txtSalesPrice.Text = Convert.ToString(((Label)gvItems.Rows[e.NewSelectedIndex].FindControl("lblgvSalesPrice")).Text);
                    hdfItem.Value = Convert.ToString(e.NewSelectedIndex);
                    HfItemsID.Value = Convert.ToString(((Label)gvItems.Rows[e.NewSelectedIndex].FindControl("lblgvFK_Items")).Text);
                    hfItem.Value = Convert.ToString(((Label)gvItems.Rows[e.NewSelectedIndex].FindControl("lblgvItem")).Text);
                    gvItems.Enabled = false;
                    // hdfItem.Value = "";
                    CalculateActualQuantity();
                    ddlItemsFill();




                }
                ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "Total()", true);
            }


        }

        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "toastr.warning('" + ex.Message + "')", true);
        }
    }

    protected void gvItems_RowDataBound(object sender, GridViewRowEventArgs e)
    {

    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        bool Status = false;
        if (gvItems.Rows.Count > 0 && gvPurchaseReturn.Rows.Count > 0)
            Status = true;

        if (Status)
        {

            SaveData();
        }
        else
        {


            ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "toastr.error('Atleast one item required')", true);
            // ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "alertify.alert('Atleast one item required')", true);
        }

    }
    protected void btnClear_Click(object sender, EventArgs e)
    {
        try
        {


            Clear();
            ddlStore.SelectedValue = Session["mainstore"].ToString();
            ddlStore.Enabled = false;
            Populate populate = new Populate();
            DisplayDiv();


        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "toastr.warning('" + ex.Message + "')", true);
        }
    }
    protected void imgbtnPurReturn_Click(object sender, ImageClickEventArgs e)
    {
        try
        {


            ClearPurchase();
            ((TextBox)CommonPopup1.FindControl("txtName")).Text = txtPurRefno.Text.Trim();
            ((TextBox)CommonPopup1.FindControl("txtRefno")).Text = txtPurRefno.Text.Trim();
            ((HiddenField)CommonPopup1.FindControl("hfColumn")).Value = "Bill No";
            GridView gvNew = (GridView)CommonPopup1.FindControl("gdvSearch");
            gvNew.Columns[1].HeaderText = "Bill No";
            PurchaseReturnFill(1);
            divAdd.Style.Add("display", "block");
            divView.Style.Add("display", "none");
        }
        catch (Exception ex)
        {
        }
    }


    protected void btnLinkbutton_Click(object sender, EventArgs e)
    {
        DisplayDiv();

    }





    protected void btnAddItems_Click(object sender, EventArgs e)
    {
        try
        {

            int a = 0;
            if (hfItemName.Value.Trim() == txtitem.Text.ToString().Trim())
            {
                a = AddDataItem();

                ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "ItemClearPurRet();", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "toastr.warning('Item  does not exist')", true);
            }
            txtitem.Focus();


            if (a == 0)
            {
                ddlStore.Enabled = false;
                txtitem.Focus();
            }
            else
            {
                btnClearItems.Focus();

            }
        }
        catch (Exception ex)
        {
        }
    }

    protected void txtTotNetPur_TextChanged(object sender, EventArgs e)
    {

    }
    protected void txtTotalAmount_TextChanged(object sender, EventArgs e)
    {

    }
    protected void txtDiscAmnt_TextChanged(object sender, EventArgs e)
    {

    }

    protected void txtDiscountP_TextChanged(object sender, EventArgs e)
    {

    }

    protected void txtToTotal_TextChanged(object sender, EventArgs e)
    {

    }
    protected void btnSearchView_Click(object sender, EventArgs e)
    {

    }
    protected void grdReplace_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdReplace.PageIndex = e.NewPageIndex;
        GridFill(e.NewPageIndex);

    }
    protected void grdReplace_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        BlReplace blreplace = new BlReplace();
      //  ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "ShowAddDiv();", true);
        long MasterId = Convert.ToInt64(grdReplace.DataKeys[e.NewSelectedIndex]["ID_Replace"].ToString());
        blreplace.MasterId = MasterId;
        hfID_Replace.Value = MasterId.ToString();
        hfID_Replace.Value = Convert.ToString(blreplace.MasterId);
        blreplace.FK_Client = Convert.ToInt32(hfClient.Value.Split(',')[0]);
        blreplace.FromDate = Convert.ToDateTime(txtFromDateSearch.Text == "" ? "" + DateTime.Now.Date + "" : txtFromDateSearch.Text);// DateTime.ParseExact(txtFromDateSearch.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
        blreplace.ToDate = Convert.ToDateTime(txtToDateSearch.Text == "" ? "" + DateTime.Now.Date + "" : txtToDateSearch.Text);// DateTime.ParseExact(txtToDateSearch.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
        blreplace.SelectData();

        ddlSupplier.SelectedValue = Convert.ToInt64(blreplace.FK_Supplier).ToString();
        ddlStore.SelectedValue = Convert.ToInt64(blreplace.FK_Store).ToString();

        ddlBillType.SelectedValue = blreplace.Fk_BillType.ToString();
        txtInvoiceNo.Text = blreplace.InvoiceNo.ToString();
        txtInvoiceDate.Text = blreplace.InvoiceDate.ToString("dd/MM/yyyy");
        txtDate.Text = blreplace.RecivedDate.ToString("dd/MM/yyyy");
        txtPurBillNo.Text = blreplace.BillNo.ToString();
        chkLinkbutton.Checked = Convert.ToBoolean(blreplace.Linkbtn.ToString());
        DataSet dtbl = blreplace.SelectAllGridData();
        txtTotalAmount.Text = Convert.ToDecimal(dtbl.Tables[0].Rows[0]["FromTotal"]).ToString();
        hdfFKStock.Value = Convert.ToInt64(dtbl.Tables[0].Rows[0]["Fk_Stock"]).ToString();

        txtDiscountP.Text = Convert.ToDecimal(dtbl.Tables[0].Rows[0]["FromDiscountPer"]).ToString();
        txtDiscAmnt.Text = Convert.ToDecimal(dtbl.Tables[0].Rows[0]["FromDiscountAmount"]).ToString();
        txtNetAmnt.Text = Convert.ToDecimal(dtbl.Tables[0].Rows[0]["FromNetAmount"]).ToString();
        txtToTotal.Text = Convert.ToDecimal(dtbl.Tables[0].Rows[0]["ToTotal"]).ToString();
        // txtDiscountP.Text = Convert.ToDecimal(dtbl.Tables[0].Rows[0]["FromDiscountPer"]).ToString();
        //  txtDiscAmnt.Text = Convert.ToDecimal(dtbl.Tables[0].Rows[0]["FromDiscountAmount"]).ToString();
        txtToNetAmount.Text = Convert.ToDecimal(dtbl.Tables[0].Rows[0]["ToTotalNetAmount"]).ToString();
        if (chkLinkbutton.Checked == true)
        {
            gvPurchaseReturn.DataSource = dtbl.Tables[0];
            gvPurchaseReturn.DataBind();
            btnAddItems.Enabled = false;
            ViewState[PurchaseReturnItems] = dtbl.Tables[0];
            FillReasonddl(true);
            chkLinkbutton.Enabled = false;
            imgbtnPurReturn.Enabled = false;
            foreach (GridViewRow row in gvPurchaseReturn.Rows)
            {
                //((TextBox)row.FindControl("txtQuantity")).ReadOnly = true;
                //((TextBox)row.FindControl("txtFreeQty")).ReadOnly = true;
                //((TextBox)row.FindControl("txtActQty")).ReadOnly = true;
              //  ((TextBox)row.FindControl("lblDiscAmt")).ReadOnly = true;
                ((DropDownList)row.FindControl("ddlReason")).Enabled = false;
                ((ImageButton)row.FindControl("ImageButton4")).Visible = false;
            }
            EnableItem(false);
        }
        else
        {

            gvPurchaseReturn.DataSource = dtbl.Tables[0];
            gvPurchaseReturn.DataBind();
            ViewState[PurchaseReturnItems] = dtbl.Tables[0];
            FillReasonddl(true);
            chkLinkbutton.Enabled = false;
            imgbtnPurReturn.Enabled = false;
            btnAddItems.Enabled = true;
            EnableItem(true);
        }
        if (blreplace.Approved == true)
        {
            btnApprove.Visible = false;
            btnSubmit.Visible = false;
            gvPurchaseReturn.Enabled = false;
            gvItems.Enabled = false;
        }
        else
        {
            btnApprove.Visible = true;
            btnSubmit.Visible = true;
           
        }
        gvItems.DataSource = dtbl.Tables[1];
        gvItems.DataBind();

        ViewState["TempTable"] = dtbl.Tables[1];

        // txtInvoiceNo.Enabled = false;
        // txtDate.Enabled = false;
        txtPurBillNo.Enabled = false;
        // txtInvoiceDate.Enabled = false;
        ddlStore.Enabled = false;
        //  ddlSupplier.Enabled = false;
        ddlBillType.Enabled = false;
        //txtSupplierName.Text = ddlSupplier.SelectedItem.Text.ToString();
        //try
        //{
        //    ddlStore.SelectedValue = blreplace.FK_Store.ToString();
        //}
        //catch (Exception ex)
        //{
        //    ddlStore.SelectedValue = "0";
        //}
        //try
        //{
        //    ddlBillType.SelectedValue = blreplace.FK_BillType.ToString();
        //}
        //catch (Exception ex)
        //{
        //    ddlBillType.SelectedValue = "0";
        //}
        //ddlBillType.Enabled = false;
        //ddlStore.Enabled = false;
        //cbxIsCredit.Checked = blpurchaseReturn.Credit;
        //txtRemark.Text = blpurchaseReturn.Remarks.ToString();
        //txtDiscount.Text = blpurchaseReturn.Discount.ToString();
        //txtDiscountAmt.Text = blpurchaseReturn.DiscountAmt.ToString();
        //hfBillNo.Value = blpurchaseReturn.BillNo.ToString();

        divAdd.Style.Add("display", "block");
        divView.Style.Add("display", "none;");
        //PanelHead.Enabled = false;

        //DisplayDiv();
        // ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "CalculateAmount('id','txtQuantity')", true);
    }




    protected void grdReplace_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {


        try
        {

            string confirmValue = Request.Form["confirm_value"];
            if (confirmValue.Length > 3)
                confirmValue = confirmValue.Substring(confirmValue.Length - 3, 3);
            if (confirmValue == "Yes")
            {
                long status = 0;
                BlReplace blreplace = new BlReplace();
                blreplace.MasterId = Convert.ToInt64(grdReplace.DataKeys[e.RowIndex]["ID_Replace"].ToString());
                hfMasterID.Value = (grdReplace.DataKeys[e.RowIndex]["ID_Replace"].ToString());
                blreplace.CancelledUser = hfuserName.Value;
                blreplace.FK_Client = Convert.ToInt64(hfClient.Value);
                blreplace.CancelledOn = DateTime.Now;
                blreplace = null;
            }
            else
            {
                mpDelete.Hide();
            }

        }
        catch (Exception ex)
        {
            //blMessage.Items.Add(ex.Message);
            ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "toastr.warning('" + ex.Message + "')", true);
        }

    }
    protected void grdReplace_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {

            BlPurchase blPurchase = new BlPurchase();
            long status = 0;
            string Sts = "closed";
            if (e.CommandName == "Print")
            {

                GridViewRow row = (GridViewRow)((ImageButton)e.CommandSource).NamingContainer;
                int index = row.RowIndex;
                string IDReplace = grdReplace.DataKeys[index]["ID_Replace"].ToString();
                string popupreport = "<script> window.open('ReplaceReport.aspx?ReplaceID=" + IDReplace + "', 'self', 'left=200,top=50,width=900,height=680,status=0,resizable=0,fullscreen=0,scrollbars=1');</script>";
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "Success", popupreport, false);

            }
            blPurchase = null;


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
        }
        catch (Exception ex)
        {
            Populate.WarningMsg(this, ex.ToString());
        }
    }
    protected void gvPurchaseReturn_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            //
            string confirmValue = Request.Form["confirm_value"];
            if (confirmValue.Length > 3)
                confirmValue = confirmValue.Substring(confirmValue.Length - 3, 3);
            if (confirmValue == "Yes")
            {
                if (ViewState[PurchaseReturnItems] != null)
                {
                    DataTable dtbl = (DataTable)ViewState[PurchaseReturnItems];

                    if (dtbl.Columns["FK_Reason"] == null)
                        dtbl.Columns.Add("FK_Reason");
                    for (int i = 0; i < dtbl.Rows.Count; i++)
                    {
                        dtbl.Rows[i]["FK_Reason"] = ((DropDownList)gvPurchaseReturn.Rows[i].FindControl("ddlReason")).SelectedValue;
                    }

                    dtbl.Rows.RemoveAt(e.RowIndex);
                    for (int i = 0; i < dtbl.Rows.Count; i++)
                    {
                        dtbl.Rows[i]["slno"] = i + 1;
                    }
                    gvPurchaseReturn.DataSource = dtbl;
                    gvPurchaseReturn.DataBind();
                    TotalAmount(dtbl);
                    ViewState[PurchaseReturnItems] = dtbl;
                    FillReasonddl(false);
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
                    for (int i = 0; i < dtbl.Rows.Count; i++)
                    {
                        dtbl.Rows[i]["slno"] = i + 1;
                    }
                    gvItems.DataSource = dtbl;
                    gvItems.DataBind();
                    CalculateTotals();
                    ViewState["TempTable"] = dtbl;
                    // FillReasonddl(false);
                }
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "toastr.warning('" + ex.Message + "')", true);
        }
    }
    protected void ImageButton5_Click(object sender, ImageClickEventArgs e)
    {
        Delete();
    }

    protected void btnApprove_Click(object sender, EventArgs e)
    {
        BlReplace blReplace = new BlReplace();
        blReplace.UserCode = hfuserName.Value;
        //  blReplace.MasterId = Convert.ToInt64(hfID_Replace.Value);
        if (hfID_Replace.Value != null)
            blReplace.MasterId = Convert.ToInt64(hfID_Replace.Value);
        blReplace.FK_Client = Convert.ToInt64(hfClient.Value);
        if (hfIsApprove.Value.ToLower() == "true")
        {
            long status = 0;
            status = blReplace.ReplaceApproval();
            if (status > 0)
            {

                ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "toastr.success('Replace Approved successfully')", true);
                ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "ClearAdd();", true);
                Clear();
            }
            else
            {
              //  ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "ShowAddDiv();", true);
                Populate.ErrorMessageBox(this, "Cannot approve data");
                // ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "ClearAdd();", true);
                // Clear();
            }
        }

        else
        {
            ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "toastr.warning('User does not have permission to Approve')", true);


        }
        btnApprove.Visible = false;


    }

    protected void grdReplace_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {

            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                Button btnapprove = (Button)e.Row.FindControl("btnApprovedStatus");
                ImageButton btnDelete = (ImageButton)e.Row.FindControl("ImageButton5");
                if (btnapprove.Text == "Approved")
                {
                    btnapprove.Enabled = false;
                    btnDelete.Enabled = false;
                    btnapprove.BackColor = System.Drawing.Color.DarkGreen;//true means enable else you may set false to disable button 
                }
                else
                {
                    btnapprove.BackColor = System.Drawing.Color.DarkRed;
                    btnapprove.Enabled = false;
                    btnDelete.Enabled = true;
                }

            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "toastr.warning('" + ex.Message + "')", true);
        }
    }
    #endregion Events
    protected void gvItems_PreRender(object sender, EventArgs e)
    {

    }
    protected void btnClearItems_Click(object sender, EventArgs e)
    {

    }
    protected void ClearTo()
    {
        hdfItem.Value = "";
        //hfID_Replace.Value = "";
        hfSlNo.Value = "";
        //ddlItem.SelectedIndex=0;
        ddlCategory.SelectedIndex = 0;
        txtItmShortName.Text = "";
        ddlUnit.SelectedIndex = 0;
        ddlSubunit.SelectedIndex = 0;
        txtBatch.Text = "";
        txtExpiryDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
        txtHSNSAC.Text = "";
        txtQuantity.Text = "0";
        txtFree.Text = "0";
        txtActq.Text = "0";
        txtPurchaseRate.Text = "0";
        chkInctax.Checked = false;
        txttaxAmount.Text = "0";
        chkDiscounttax.Checked = false;
        txtPurDiscPer.Text = "0";
        txtPurDiscAmnt.Text = "0";
        txtNetPur.Text = "0";
        txtMRP.Text = "0";
        txtProfitPer.Text = "0";
        txtProfitAmount.Text = "0";
        txtDiscountPer.Text = "0";
        txtDiscountAmt.Text = "0";
        ChkIncStax.Checked = false;
        txtSTaxAmount.Text = "0";
        txtSalesPrice.Text = "0";
        hfUnitQuantity.Value = "0";
        hfTotTaxPercentage.Value = "0";
        txtKey.Text = "0";
        hfItemId.Value = "";

    }
    protected void gvPurchaseReturn_RowDataBound(object sender, GridViewRowEventArgs e)
    {

    }
    protected void btnPtax_Click(object sender, EventArgs e)
    {
        BlPurchase blpurchase = new BlPurchase();
        DataTable dt;
        string s = txttaxAmnt.Text;
        Populate Populate = new Populate();
        CommonPopupGrid1.CallingFunction = "Purchase";
        string Refno = ((TextBox)CommonPopupGrid1.FindControl("txtRefno")).Text;
        string searchCol = ((HiddenField)CommonPopupGrid1.FindControl("hfColumn")).Value;

        Refno = Refno == string.Empty || Refno == null ? "0" : Refno;
        GridView gvNew = (GridView)CommonPopupGrid1.FindControl("gdvSearch");
        if (hfItemId.Value != "")
            blpurchase.itemID = Convert.ToInt64(hfItemId.Value.ToString());
        blpurchase.Purchase = true;
        dt = blpurchase.SelectGriddata();
        dt.Columns.Add("TaxAmount", typeof(decimal));
        foreach (DataRow row in dt.Rows)
        {
            TaxAmountFrom = 0;
            if (Convert.ToBoolean(row["TaxInterState"]) == true)
            {

                TaxAmountFrom = Math.Round(Convert.ToDecimal(row["TaxPercentage"]) / Convert.ToDecimal(hfFromTotTaxPercentage.Value) * (Convert.ToDecimal(txttaxAmnt.Text == "" ? "0" : txttaxAmnt.Text)), 4);
                // TaxAmount = (Convert.ToDecimal(row["TaxPercentage"]) / (TotalTaxPercentage) * (Convert.ToDecimal(txttaxAmount.Text == "" ? "0" : txttaxAmount.Text)));
            }
            else if (Convert.ToBoolean(row["TaxIntraState"]) == true)
            {
                TaxAmountFrom = Math.Round(Convert.ToDecimal(row["TaxPercentage"]) / Convert.ToDecimal(hfFromTotTaxPercentage.Value) * (Convert.ToDecimal(txttaxAmnt.Text == "" ? "0" : txttaxAmnt.Text)), 4);

            }

            row["TaxAmount"] = TaxAmountFrom;
        }
        gvNew.DataSource = dt;
        gvNew.DataBind();
        gvNew.Columns[3].Visible = false;

        lblPopupH.Text = "Purchase Tax";
        mpGrid.Show();
    }
    protected void gvPurchaseReturn_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        if (gvPurchaseReturn.Rows.Count > 0)
        {
            DataTable dtbl = new DataTable();
            if (ViewState["PurchaseReturnItems"] != null)
            {
                dtbl = (DataTable)ViewState["PurchaseReturnItems"];

                txtitem.Text = Convert.ToString(((Label)gvPurchaseReturn.Rows[e.NewSelectedIndex].FindControl("lblItem")).Text);
                txtPurQty.Text = Convert.ToString(((Label)gvPurchaseReturn.Rows[e.NewSelectedIndex].FindControl("txtQuantity")).Text);
                txtPurFree.Text = Convert.ToString(((Label)gvPurchaseReturn.Rows[e.NewSelectedIndex].FindControl("txtFreeQty")).Text);
                txtPurchaseRateItem.Text = Convert.ToString(((Label)gvPurchaseReturn.Rows[e.NewSelectedIndex].FindControl("lblPurchaseRate")).Text);
                txtCurrentStock.Text = dtbl.Rows[0]["CurrentStock"].ToString();
                txttaxAmnt.Text = dtbl.Rows[0]["PurchaseTaxAmount"].ToString();
                hfItem.Value = Convert.ToString(((Label)gvPurchaseReturn.Rows[e.NewSelectedIndex].FindControl("lblItem")).Text);
                hfItemName.Value = Convert.ToString(((Label)gvPurchaseReturn.Rows[e.NewSelectedIndex].FindControl("lblItem")).Text);
                hfItemId.Value = Convert.ToString(e.NewSelectedIndex);
                hdfFkItem.Value = Convert.ToString(((HiddenField)gvPurchaseReturn.Rows[e.NewSelectedIndex].FindControl("hiddenFk_Item")).Value);
                hdfCurrentStock.Value = Convert.ToString(((HiddenField)gvPurchaseReturn.Rows[e.NewSelectedIndex].FindControl("hfCurrentStk")).Value);
                hdfFKStock.Value = Convert.ToString(((HiddenField)gvPurchaseReturn.Rows[e.NewSelectedIndex].FindControl("hfStockID")).Value);
                txtDiscAmntFrom.Text = Convert.ToString(((Label)gvPurchaseReturn.Rows[e.NewSelectedIndex].FindControl("lblDiscAmt")).Text);
                  //  dtbl.Rows[0]["FK_Stock"].ToString();
               // txtQuantity.Text = Convert.ToString(((TextBox)gvPurchaseReturn.Rows[e.NewSelectedIndex].FindControl("txtQuantity")).Text);
               // txtQuantity.Text = Convert.ToString(((TextBox)gvPurchaseReturn.Rows[e.NewSelectedIndex].FindControl("txtQuantity")).Text);
                
            }
        }
    }
}