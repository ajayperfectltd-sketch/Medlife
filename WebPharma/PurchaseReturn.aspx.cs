/*----------------------------------------------------------------------------------------------------------------------------------------------------------
Purpose    : Insert/Update/Delete Purchase 
Author     : Bibin TC
Copyright  : Perfect Software Solutions (Clt) Pvt Ltd
Created on : 15/05/2018
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
using WebPharma.Business;


public partial class PurchaseReturn : System.Web.UI.Page
{

    private const string PurchaseReturnItems = "PurchaseReturnItems";
    private const string DirectPR = "DirectPurchase";
    long TransactionID;
    decimal TaxAmount, TotalTaxPercentage, TotalTaxPerCS;
    private Boolean Approveedit=false;
    #region Functions

    protected void BindData()
    {
        Populate populate = new Populate();
        populate.FillDropdownList(ddlStore, "Store", "StoName", "ID_Store", "StoName", " Active='True' and Cancelled=0   and FK_Client = " + hfClient.Value, "");
        populate.FillDropdownList(ddlBillType, "BillType", "TypeName", "ID_BillType", "TypeName", " Cancelled=0 AND BillType=2  and FK_Client = " + hfClient.Value, "");
        populate.FillDropdownList(ddlSearchSupplier, "Supplier", "SupplierName", "ID_Supplier", "SupplierName", "  Active='True' and Cancelled=0 AND  FK_Client = " + hfClient.Value, "");
        populate.FillDropdownList(ddlSearchStore, "Store", "StoName", "ID_Store", "StoName", "   Active='True' and Cancelled=0  and FK_Client = " + hfClient.Value, "");
        populate.FillDropdownList(ddlCategory, "Category", "CategoryName", "ID_Category", "CategoryName", " Cancelled=0 AND FK_Client= " + hfClient.Value.Split(',')[0], "");
        populate = null;
        txtDate.Text = DateTime.Now.ToShortDateString();

    }
    private void Transactionchecking()
    {
        BlFunction blfun = new BlFunction();
        blfun.Module = "PURRET";
        TransactionID = blfun.GetTransactionID("PURRET", hfuserName.Value);
        ViewState["TransactionID"] = TransactionID;
    }
    public void SetReadonly()
    {
        txtDate.Attributes.Add("readonly", "readonly");
        txtFromDate.Attributes.Add("readonly", "readonly");
        txtTodate.Attributes.Add("readonly", "readonly");
        txtDate.Text = txtFromDate.Text = txtTodate.Text = DateTime.Now.ToString("dd/MM/yyyy");
        txtTotalAmount.Attributes.Add("readonly", "readonly");
        txtDiscountAmt.Attributes.Add("readonly", "readonly");
        txtNetAmount.Attributes.Add("readonly", "readonly");
        txtRoundOff.Attributes.Add("readonly", "readonly");
        //txtSupplier.Attributes.Add("readonly", "readonly");
        txtPurBillNo.Attributes.Add("readonly", "readonly");
        txtTotalAmountItem.Attributes.Add("readonly", "readonly");
        txtQuantity.Attributes["onfocus"] = "javascript:this.select();";
        txtFree.Attributes["onfocus"] = "javascript:this.select();";
        txtSupplier.Attributes.Add("readonly", "readonly");
    }
    private void BindPurchaseItems(long purchaseId)
    {
        try
        {
            BlPurchaseReturn blpurchasereturn = new BlPurchaseReturn();
            blpurchasereturn.FK_Purchase = purchaseId;
            blpurchasereturn.FK_Client = Convert.ToInt32(hfClient.Value);
            blpurchasereturn.ReturnMethod = "PWR";
            if (hfPurchaseReturn.Value == "")
            {
                blpurchasereturn.MasterId = 0;
            }
            else
            {
                blpurchasereturn.MasterId = Convert.ToInt64(hfPurchaseReturn.Value);
            }
            DataTable dtbl = new DataTable();
            if (rdPurchase.Checked == true)
            {
                dtbl = blpurchasereturn.SelectPurchaseItems();
                gvPurchaseItems.DataSource = dtbl;
                gvPurchaseItems.DataBind();
                BlPurchase blPurchase = new BlPurchase();
                blPurchase.MasterId = purchaseId;
                DataTable dt = blPurchase.SelectPurchaseTax();
                ViewState["GDPurchaseWiseTax"] = dt;
            }
            else
            {
                 dtbl = blpurchasereturn.SelectPurchaseItems();
                gvPurchaseItems.DataSource = dtbl;
                gvPurchaseItems.DataBind();
            }
            BlPurchase blp = new BlPurchase();
            blp.MasterId = purchaseId;
            blp.FK_Client = Convert.ToInt64(hfClient.Value);
            blp.FromDate = DateTime.Parse("1/1/1900");
            blp.SelectPurchase();
            ddlStore.SelectedValue = blp.FK_Store.ToString();
            ddlStore.Enabled = false;
            ddlBillType.Enabled = false;
            txtSupRefno.Text = blp.FK_Supplier.ToString();

            txtSupplier.Text = blp.SupplierName.ToString();
            hdfDate.Value = blp.BillDate.ToString();
            cbxIsCredit.Checked = blp.Credit;
            hfInvNo.Value = blp.InvoiceNo;
            hfQtyValidation.Value = "1";
             txtDiscount.Text = blp.DiscountPer.ToString();
             txtDiscountAmt.Text = blp.DiscountAmt.ToString();
            ViewState[PurchaseReturnItems] = dtbl;
            TotalAmount(dtbl);
            blpurchasereturn = null;
            blp = null;
            btnSave.Visible = true;
            imgbtnPurBill.Enabled = true;
            gvPurchaseItems.Enabled = true;
        }
        catch (Exception ex)
        {

        }
    }

    private void BindExpiryItems(long SupplierId)
    {
        try
        {
            BlItems blItems = new BlItems();
            blItems.FK_Supplier = SupplierId;
            blItems.FK_Client = Convert.ToInt32(hfClient.Value);

            if (hfPurchaseReturn.Value == "")
            {
                blItems.MasterId = 0;
            }
            else
            {
                blItems.MasterId = Convert.ToInt64(hfPurchaseReturn.Value);
            }

            DataTable dtbl = blItems.SelectExpiryItems();
            gvItems.DataSource = dtbl;
            gvItems.DataBind();
            ViewState[DirectPR] = dtbl;
            FillReasonddl(false);

            TotalAmount(dtbl);

        }
        catch (Exception ex)
        {

        }
    }

    protected void CheckUserAccess()
    {
        BlUserAccess bluseraccess = new BlUserAccess();
        Populate populate = new Populate();

        bluseraccess.FK_Users = Convert.ToInt64(hfUser.Value);
        bluseraccess.FK_Pages = 26;
        bluseraccess.FK_Client = Convert.ToInt64(hfClient.Value);
        bluseraccess.CheckUserAccess();
        if (Convert.ToBoolean(bluseraccess.IsView) == false)
        {
            // Response.Redirect("Home.aspx?");

        }

        hfIsAdd.Value = (Convert.ToBoolean(bluseraccess.IsAdd)).ToString();
        hfIsUpdate.Value = (Convert.ToBoolean(bluseraccess.IsUpdate)).ToString();
        hfIsDelete.Value = (Convert.ToBoolean(bluseraccess.IsDelete)).ToString();
        hfIsApprove.Value = (Convert.ToBoolean(bluseraccess.IsApprove)).ToString();
        bluseraccess = null;
    }
    protected void GridFill(int currentPage)
    {
        BlPurchaseReturn blpurchase = new BlPurchaseReturn();
        DataTable dtbl = new DataTable();
        blpurchase.MasterId = 0;
        blpurchase.FK_Supplier = Convert.ToInt64(ddlSearchSupplier.SelectedValue);
        blpurchase.FK_Store = Convert.ToInt64(Session["mainstore"].ToString());
        blpurchase.FK_Client = Convert.ToInt64(hfClient.Value);
        blpurchase.FromDate = DateTime.ParseExact(txtFromDate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
        blpurchase.ToDate = DateTime.ParseExact(txtTodate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
        currentPage = currentPage == 0 ? 1 : currentPage;
        blpurchase.PageIndex = Convert.ToInt16(currentPage);
        dtbl = blpurchase.SelectAllData();
        gvPurchaseReturn.DataSource = dtbl;
        gvPurchaseReturn.DataBind();
        gvPurchaseReturn.Columns[5].Visible = false;
        // gvPurchaseReturn.Columns[7].Visible = false;
        blpurchase = null;

    }
    protected void Save()
    {
        long Status = 0;
        BlPurchaseReturn blpurchase = new BlPurchaseReturn();

        try
        {
            // ── Remark length guard ──────────────────────────────────────────
            if (txtRemark.Text.Length > 500)
            {
                Populate.ErrorMessageBox(this, "Remark cannot exceed 500 characters.");
                return;
            }

             
            if (blpurchase.DiscountAmt > blpurchase.TotalAmt) { Populate.ErrorMessageBox(this, "Discount Amount cannot exceed Total Amount."); return; }
            if (blpurchase.Discount > 100) { Populate.ErrorMessageBox(this, "Discount % cannot exceed 100."); return; }

            // ── Parse the entered return date ────────────────────────────────
            DateTime returnDate;
            if (!DateTime.TryParseExact(
                    txtDate.Text, "dd/MM/yyyy",
                    CultureInfo.InvariantCulture,
                    DateTimeStyles.None,
                    out returnDate))
            {
                Populate.ErrorMessageBox(this, "Invalid Return Date. Please enter a valid date.");
                return;
            }

            DateTime today = DateTime.Today;

            // ── Rule 1: No future dates ──────────────────────────────────────
            if (returnDate > today)
            {
                Populate.ErrorMessageBox(this, "Purchase Return date cannot be a future date.");
                return;
            }

            // ── Rule 2: No dates before current financial year ───────────────
            // Financial year starts April 1.
            int currentYear = today.Month >= 4 ? today.Year : today.Year - 1;
            DateTime finYearStart = new DateTime(currentYear, 4, 1);

            if (returnDate < finYearStart)
            {
                Populate.ErrorMessageBox(this,
                    "Purchase Return date cannot be from a previous financial year. " +
                    "Earliest allowed date is " +
                    finYearStart.ToString("dd/MM/yyyy") + ".");
                return;
            }

            // ── Rule 3 (existing): Purchase Wise — return date vs purchase date ──
            if (rdPurchase.Checked)
            {
                DateTime purchaseDate;
                if (DateTime.TryParse(hdfDate.Value, out purchaseDate))
                {
                    if (returnDate < purchaseDate)
                    {
                        Populate.ErrorMessageBox(this,
                            "Purchase Return date cannot be earlier than the original Purchase date.");
                        return;
                    }
                }

                if (!string.IsNullOrEmpty(txtPurRefno.Text))
                {
                    blpurchase.FK_Purchase = Convert.ToInt64(txtPurRefno.Text);
                }
            }
        }
        catch (Exception ex)
        {
            Populate.ErrorMessageBox(this, "Date validation error: " + ex.Message);
            return;
        }
        if (ddlBillType.SelectedValue == "0")
        {
            Populate.ErrorMessageBox(this, "Bill Type required.");
            return;
        }
        GridView grid = rdPurchase.Checked ? gvPurchaseItems : gvItems;

        foreach (GridViewRow row in grid.Rows)
        {
            DropDownList ddlReason = row.FindControl("ddlReason") as DropDownList;

            if (ddlReason != null && ddlReason.SelectedValue == "0")
            {
                Populate.ErrorMessageBox(this, "Please select reason for all items.");
                return;
            }
        }

        // ── All date validations passed — continue with save ─────────────────────
        if (Status == 0)
        {
            //blpurchase.BillNo = Convert.ToInt64(hfBillNo.Value);
            long billNo;
            long.TryParse(hfBillNo.Value, out billNo);  
            blpurchase.BillNo = billNo;
            blpurchase.BillDate = DateTime.ParseExact(txtDate.Text, "dd/MM/yyyy",
                                        CultureInfo.InvariantCulture);
            blpurchase.Invoice = hfInvNo.Value;
            blpurchase.InvDate = DateTime.ParseExact(txtDate.Text, "dd/MM/yyyy",
                                        CultureInfo.InvariantCulture);
            blpurchase.FK_Supplier = Convert.ToInt64(txtSupRefno.Text);
            blpurchase.FK_Store = Convert.ToInt64(ddlStore.SelectedValue);
            blpurchase.FK_Category = Convert.ToInt64(ddlStore.SelectedValue);
            blpurchase.TotalAmt = Convert.ToDecimal(
                                        string.IsNullOrEmpty(txtTotalAmount.Text) ? "0" : txtTotalAmount.Text);
            blpurchase.Discount = Convert.ToDecimal(
                                        string.IsNullOrEmpty(txtDiscount.Text) ? "0" : txtDiscount.Text);
            blpurchase.DiscountAmt = Convert.ToDecimal(
                                        string.IsNullOrEmpty(txtDiscountAmt.Text) ? "0" : txtDiscountAmt.Text);
            blpurchase.NetAmount = Convert.ToDecimal(
                                        string.IsNullOrEmpty(txtNetAmount.Text) ? "0" : txtNetAmount.Text);
            blpurchase.Remarks = txtRemark.Text.Trim();
            blpurchase.Credit = cbxIsCredit.Checked;
            blpurchase.TransactionID = Convert.ToInt64(ViewState["TransactionID"]);
            blpurchase.Cancelled = false;
            blpurchase.CancelledBy = hfuserName.Value;
            blpurchase.UserCode = hfuserName.Value;
            blpurchase.EnteredOn = DateTime.Today;
            blpurchase.FK_Client = Convert.ToInt64(hfClient.Value);

            if (rdItemPurchase.Checked)
            {
                blpurchase.DirectReturn = true;
                blpurchase.ReturnMethod = "IWR";
            }
            if (rdPurchase.Checked)
            {
                blpurchase.ReturnMethod = "PWR";
            }
            if (rdExpiry.Checked)
            {
                blpurchase.Expiry = true;
            }

            blpurchase.FK_BillType = Convert.ToInt64(ddlBillType.SelectedValue);

            BlPurchase blpurchases = new BlPurchase();
            blpurchases.itemID = Convert.ToInt64(
                string.IsNullOrEmpty(hfItemId.Value) ? "0" : hfItemId.Value);
            DataTable dtB = blpurchases.SelectGroupID();
            hfGroupID.Value = dtB.Rows[0]["GroupID"].ToString();

            ArrayList arlist = new ArrayList();
            BlPurchaseReturnDetails blpurchasedetails;

            if (blpurchase.DirectReturn || blpurchase.Expiry)
            {
                // ── Item Wise / Expiry loop (unchanged from original) ────────
                int RowIndex = 0;
                DataTable dtbl = (DataTable)ViewState[DirectPR];
                foreach (DataRow dr in dtbl.Rows)
                {
                    blpurchasedetails = new BlPurchaseReturnDetails();
                    blpurchasedetails.FK_Items = Convert.ToInt64(dr["FK_Items"].ToString());
                    blpurchasedetails.FK_Stock = Convert.ToInt64(dr["FK_Stock"].ToString());
                    blpurchasedetails.FK_Units = Convert.ToInt64(dr["FK_Unit"].ToString());
                    blpurchasedetails.BatchNo = dr["BatchNo"].ToString();
                    blpurchasedetails.ExpiryDate = DateTime.Parse(dr["ExpiryDate"].ToString());
                    blpurchasedetails.ActQty = Convert.ToDecimal(dr["ActQty"].ToString());
                    blpurchasedetails.Free = Convert.ToDecimal(dr["Free"].ToString());
                    blpurchasedetails.PurRate = Convert.ToDecimal(dr["PurchaseRate"].ToString());
                    blpurchasedetails.MRP = Convert.ToDecimal(dr["MRP"].ToString());
                    blpurchasedetails.Quantity = Convert.ToDecimal(dr["Quantity"].ToString());
                    blpurchasedetails.FK_TaxGroupID = Convert.ToInt64(hfGroupID.Value);
                    blpurchasedetails.PurchaseDiscPercentage =
                        Convert.ToDecimal(dr["PurchaseDiscPercentage"].ToString());
                    blpurchasedetails.SalesPrice = 0;
                    blpurchasedetails.FK_Reason =
                        Convert.ToInt64(((DropDownList)gvItems.Rows[RowIndex]
                            .FindControl("ddlReason")).SelectedValue);
                    blpurchasedetails.PurchaseDiscAmount =
                        Convert.ToDecimal(dr["PurchaseDiscAmt"].ToString() == "" ? "0" : dr["PurchaseDiscAmt"]);
                    blpurchasedetails.SlNo = RowIndex + 1;

                    if (blpurchasedetails.Quantity < 0 || blpurchasedetails.Free < 0 || blpurchasedetails.ActQty < 0)
                        Status = 3;

                    arlist.Add(blpurchasedetails);
                    blpurchasedetails = null;
                    RowIndex++;
                }
            }
            else
            {
                // ── Purchase Wise loop ───────────────────────────────────────
                int RowIndex = 0;
                foreach (GridViewRow gr in gvPurchaseItems.Rows)
                {
                    if (gr.RowType == DataControlRowType.DataRow)
                    {
                        blpurchasedetails = new BlPurchaseReturnDetails();
                        blpurchasedetails.FK_Items =
                            Convert.ToInt64(gvPurchaseItems.DataKeys[gr.RowIndex]["FK_Items"].ToString());
                        blpurchasedetails.FK_Stock =
                            Convert.ToInt64(((HiddenField)gr.FindControl("hfStockID")).Value);
                        blpurchasedetails.FK_Units =
                            Convert.ToInt64(gvPurchaseItems.DataKeys[gr.RowIndex]["FK_Units"].ToString());
                        blpurchasedetails.ExpiryDate =
                            Convert.ToDateTime(gvPurchaseItems.DataKeys[gr.RowIndex]["ExpiryDate"].ToString());
                        blpurchasedetails.BatchNo =
                            ((HiddenField)gr.FindControl("hdfBatchNo")).Value;

                        string strRate = ((Label)gr.FindControl("lblPurchaseRate")).Text;
                        blpurchasedetails.PurRate =
                            Convert.ToDecimal(string.IsNullOrEmpty(strRate) ? "0" : strRate);

                        string strSales = ((HiddenField)gr.FindControl("hdfSalesPrice")).Value;
                        blpurchasedetails.SalesPrice =
                            Convert.ToDecimal(string.IsNullOrEmpty(strSales) ? "0" : strSales);

                        blpurchasedetails.FK_Reason =
                            Convert.ToInt64(((DropDownList)gvPurchaseItems.Rows[RowIndex]
                                .FindControl("ddlReason")).SelectedValue);
                        blpurchasedetails.ActQty =
                            Convert.ToDecimal(((TextBox)gr.FindControl("txtActQty")).Text);
                        blpurchasedetails.Free =
                            Convert.ToDecimal(((TextBox)gr.FindControl("txtFreeQty")).Text);
                        blpurchasedetails.Quantity =
                            Convert.ToDecimal(((TextBox)gr.FindControl("txtQuantity")).Text);
                        blpurchasedetails.MRP =
                            Convert.ToDecimal(gvPurchaseItems.DataKeys[gr.RowIndex]["MRP"].ToString());
                        blpurchasedetails.PurchaseDiscAmount =
                            Convert.ToDecimal(((TextBox)gr.FindControl("lblDiscAmt")).Text);

                        BlPurchase blpurchass = new BlPurchase();
                        blpurchass.itemID =
                            Convert.ToInt64(gvPurchaseItems.DataKeys[gr.RowIndex]["FK_Items"].ToString());
                        DataTable dtBr = blpurchass.SelectGroupID();
                        blpurchasedetails.FK_TaxGroupID =
                            Convert.ToInt32(dtBr.Rows[0]["GroupID"].ToString());
                        blpurchasedetails.PurchaseDiscPercentage =
                            Convert.ToDecimal(gvPurchaseItems.DataKeys[gr.RowIndex]
                                ["PurchaseDiscPercentage"].ToString());
                        blpurchasedetails.SlNo = RowIndex + 1;

                        if (blpurchasedetails.Quantity < 0 || blpurchasedetails.Free < 0 || blpurchasedetails.ActQty < 0)
                            Status = 3;

                        // Correct zero-check: all three must be zero to be invalid
                        if (blpurchasedetails.Quantity == 0
                          && blpurchasedetails.Free == 0
                          && blpurchasedetails.ActQty == 0)
                        {
                            Status = 2;
                        }


                        arlist.Add(blpurchasedetails);
                        blpurchasedetails = null;
                        RowIndex++;
                    }
                }
            }

            blpurchase.XmlPR = Common.WriteToXml(arlist);

            if (Status == 0)
            {
                if (string.IsNullOrEmpty(hfPurchaseReturn.Value))
                {
                    // ── INSERT ───────────────────────────────────────────────
                    if (hfIsAdd.Value.ToLower() == "true")
                    {
                        Status = blpurchase.InsertData();
                        if (Status > 0)
                        {
                            ClearAll();
                            GridFill(1);
                            ScriptManager.RegisterStartupScript(Page, typeof(Page),
                                "Script", "ShowAddDiv();", true);
                            Populate.InsertMsg(this);
                            string popupreport =
                                "<script> window.open('PurchaseReturnPrint.aspx?PurchaseReturnId="
                                + Status.ToString()
                                + "', 'self', 'left=200,top=50,width=900,height=680,"
                                + "status=0,resizable=0,fullscreen=0,scrollbars=1');</script>";
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(),
                                "Success", popupreport, false);
                            ddlStore.SelectedValue = Session["mainstore"].ToString();
                            ddlStore.Enabled = false;
                        }
                        else if (Status == -1 || Status == -2)
                        {
                            ScriptManager.RegisterStartupScript(Page, typeof(Page),
                                "Script", "ShowAddDiv();", true);
                            Populate.ErrorMessageBox(this,
                                "Cannot insert data. No stock available.");
                        }
                        else if (Status == -3)
                        {
                            Populate.ErrorMessageBox(this,
                                "Return AccHead has not been set in Bill Type.");
                        }
                        else if (Status == -8)
                        {
                            ScriptManager.RegisterClientScriptBlock(Page, typeof(Page),
                                "Script",
                                "toastr.error('Cannot insert data. TransactionID already exists.')",
                                true);
                        }
                        else if (Status == -66)
                        {
                            ScriptManager.RegisterClientScriptBlock(Page, typeof(Page),
                                "Script",
                                "toastr.error('Please update FinYear for this Transaction.')",
                                true);
                        }
                        else if (Status == -10)
                        {
                            Populate.ErrorMessageBox(this, "Duplicate item/stock entries found in the return.");
                        }
                        else if (Status == -11)
                        {
                            Populate.ErrorMessageBox(this, "A Purchase Return with this Invoice already exists for the selected Supplier/Store.");
                        }
                        else
                        {
                            Populate.ErrorMessageBox(this, "Cannot insert data.");
                        }
                    }
                    else
                    {
                        ScriptManager.RegisterClientScriptBlock(Page, typeof(Page),
                            "Script",
                            "toastr.warning('User does not have permission to Add.')",
                            true);
                    }
                }
                else
                {
                    // ── UPDATE ───────────────────────────────────────────────
                    long decid = Convert.ToInt64(hfPurchaseReturn.Value);
                    blpurchase.MasterId = decid;

                    if (hfIsUpdate.Value.ToLower() == "true")
                    {
                        Status = blpurchase.UpdateData();
                        if (Status > 0)
                        {
                            ClearAll();
                            GridFill(1);
                            Populate.UpdateMsg(this);
                            string popupreport =
                                "<script> window.open('PurchaseReturnPrint.aspx?PurchaseReturnId="
                                + Status.ToString()
                                + "', 'self', 'left=200,top=50,width=900,height=680,"
                                + "status=0,resizable=0,fullscreen=0,scrollbars=1');</script>";
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(),
                                "Success", popupreport, false);
                        }
                        else if (Status == -1 || Status == -2)
                        {
                            ScriptManager.RegisterStartupScript(Page, typeof(Page),
                                "Script", "ShowAddDiv();", true);
                            Populate.ErrorMessageBox(this,
                                "Cannot update data. No stock available.");
                        }
                        else if (Status == -3)
                        {
                            Populate.ErrorMessageBox(this,
                                "Return AccHead has not been set in Bill Type.");
                        }
                        else if (Status == -66)
                        {
                            ScriptManager.RegisterClientScriptBlock(Page, typeof(Page),
                                "Script",
                                "toastr.error('Please update FinYear for this Transaction.')",
                                true);
                        }
                        else if (Status == -10)
                        {
                            Populate.ErrorMessageBox(this, "Duplicate item/stock entries found in the return.");
                        }
                        else if (Status == -11)
                        {
                            Populate.ErrorMessageBox(this, "A Purchase Return with this Invoice already exists for the selected Supplier/Store.");
                        }
                        else
                        {
                            Populate.ErrorMessageBox(this, "Cannot update data.");
                        }
                    }
                    else
                    {
                        ScriptManager.RegisterClientScriptBlock(Page, typeof(Page),
                            "Script",
                            "toastr.warning('User does not have permission to Update.')",
                            true);
                    }
                }
            }
            else if (Status == 2)
            {
                Populate.ErrorMessageBox(this,
                    "All quantities are zero for one or more items. " +
                    "Please enter a valid Quantity, Free, or Actual Quantity before saving.");
            }
            else if (Status == 3)
            {
                Populate.ErrorMessageBox(this, "Quantity, Free and Actual Quantity cannot be negative.");
            }
        }
    }

    public void ClearAll()
    {
        hfeditpurchaase.Value = "0";
        ViewState[PurchaseReturnItems] = null;
        gvPurchaseItems.DataSource = null;
        gvPurchaseItems.DataBind();
        ddlStore.Enabled = false;
        ddlBillType.Enabled = true;
        // PanelHead.Enabled = true;
        divAdd.Style.Add("display", "block;");
        divView.Style.Add("display", "none;");
        rdPurchase.Enabled = true;
        rdItemPurchase.Enabled = true;
        rdExpiry.Enabled = true;
        hfQtyValidation.Value = "0";
        btnApprove.Visible = false;
        btnSave.Visible = true;
        imgbtnPurBill.Enabled = true;
        gvPurchaseItems.Enabled = true;
        ClearItems();
        ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "ClearAdd();", true);
        Transactionchecking();
        hdnapproveddelet.Value = "0";
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
    private void ClearItems()
    {
        ViewState[DirectPR] = null;
        gvItems.DataSource = null;
        gvItems.DataBind();
        ViewState[PurchaseReturnItems] = null;
        gvPurchaseItems.DataSource = null;
        gvPurchaseItems.DataBind();
        ddlStore.Enabled = false;
        txtTotalAmount.Text = "0";
        txtNetAmount.Text = "0";
        txtDiscount.Text = "0";
        txtDiscountAmt.Text = "0";
        txtRoundOff.Text = "0";
        DisplayDiv();
    }
    private void DisplayDiv()
    {

        if (rdPurchase.Checked)
        {
            divItemPurchaseReturn.Style.Add("display", "none");
            gvPurchaseItems.Visible = true;
            liPurchase.Visible = true;
            // liBillType.Visible = true;
            liCategory.Visible = false;
            gvItems.DataSource = null;
            gvItems.DataBind();
            ViewState[DirectPR] = null;
            gvpi.Visible = true;
            imgbtnSupplier.Enabled = false;
            rfvSuppName.Enabled = false;
            rfvPurRefno.Enabled = true;
            rfvPurBillNo.Enabled = true;
        }
        else
        {
            gvPurchaseItems.Visible = false;
            divItemPurchaseReturn.Style.Add("display", "block");
            liPurchase.Visible = false;
            // liBillType.Visible = false;
            liCategory.Visible = true;
            if (rdExpiry.Checked)
            {
                divItems.Visible = false; liinsert.Visible = false;
            }
            else
            {
                divItems.Visible = true;
                liinsert.Visible = true;
            }
            gvPurchaseItems.DataSource = null;
            gvPurchaseItems.DataBind();
            ViewState[PurchaseReturnItems] = null;
            gvpi.Visible = false;
            imgbtnSupplier.Enabled = true;

            rfvSuppName.Enabled = true;
            rfvPurRefno.Enabled = false;
            rfvPurBillNo.Enabled = false;
        }
    }
    private DataTable GetDataTable(DataTable dtbl)
    {
        dtbl.Columns.Add("slno");
        dtbl.Columns.Add("Item");
        dtbl.Columns.Add("FK_Items");
        dtbl.Columns.Add("BatchNo");
        dtbl.Columns.Add("PurchaseRate");
        dtbl.Columns.Add("FK_Unit");
        dtbl.Columns.Add("ExpiryDate");
        dtbl.Columns.Add("Quantity");
        dtbl.Columns.Add("TotalAmt", typeof(decimal));
        dtbl.Columns.Add("FK_Reason");
        dtbl.Columns.Add("ActQty");
        dtbl.Columns.Add("Free");
        dtbl.Columns.Add("MRP");
        dtbl.Columns.Add("SalesPrice");
        dtbl.Columns.Add("FK_Stock");
        dtbl.Columns.Add("NoofUnits");
        dtbl.Columns.Add("PurchaseTaxAmount");
        dtbl.Columns.Add("PurchaseDiscPercentage");
        dtbl.Columns.Add("PurchaseDiscAmt");
        return dtbl;
    }
    protected int AddDataItem()
    {
        DataTable dtbl = new DataTable();
        int inRowCount = 0;
        int inCheck = 0;
        if (ViewState[DirectPR] != null)
        {
            dtbl = (DataTable)ViewState[DirectPR];
            inRowCount = dtbl.Rows.Count;
        }
        else
        {

            dtbl = GetDataTable(dtbl);
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
                dtbl.Rows[i]["FK_Reason"] = ((DropDownList)gvItems.Rows[i].FindControl("ddlReason")).SelectedValue;
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
            decimal CESS = 0; string ExpDate = "";

            if (rdItemPurchase.Checked)
            {
                BlStock blStock = new BlStock();
                blStock.MasterId = Convert.ToInt64(hdfFKStock.Value);
                blStock.UserCode = hfUser.Value;
                blStock.FK_Client = Convert.ToInt64(hfClient.Value);
                blStock.SelectData();
                PurchaseRate = Convert.ToDecimal(txtPurchaseRateItem.Text);
                BatchNo = blStock.BatchNo;
                MRP = blStock.MRP;
                ExpDate = blStock.ExpiryDate == null ? "" : blStock.ExpiryDate.ToString("dd/MM/yyyy");  
               
            }
            else
            {
                ExpDate = DateTime.Now.ToString();
            }
            decimal actq = Math.Round(Convert.ToDecimal(txtQuantity.Text == "" ? "0" : txtQuantity.Text), 4);
            decimal free = Math.Round(Convert.ToDecimal(txtFree.Text == "" ? "0" : txtFree.Text), 4);
            decimal noofunit = Convert.ToDecimal(hfUnitQuantity.Value);
            decimal decqty = (actq + free);
            decimal TotQty = (actq);
            decimal PurItemDisAmt = Math.Round(Convert.ToDecimal(txtitemdisAmt.Text == "" ? "0" : txtitemdisAmt.Text), 4);
            SalesPrice = Convert.ToDecimal(hdnSalesPrice.Value == "" ? "0" : hdnSalesPrice.Value);
            decimal Actualquantity =decqty *noofunit;
            // decimal decqty = (actq );
            //* (noofunit);
            decimal Total = ((TotQty * PurchaseRate) + ((PurVAT / 100) * (TotQty * PurchaseRate)) + ((CESS / 100) * (TotQty * PurchaseRate))-PurItemDisAmt);

            DataRow drow = dtbl.NewRow();
            drow["slno"] = inRowCount + 1;
            drow["Item"] = hfItem.Value;
            drow["FK_Items"] = hdfFkItem.Value;
            drow["Quantity"] = actq;
            drow["PurchaseRate"] = PurchaseRate;
            drow["FK_Unit"] = "0";
            drow["BatchNo"] = BatchNo;
            drow["ExpiryDate"] = ExpDate;
           
            drow["TotalAmt"] = Math.Round(Total, 2);
            drow["FK_Reason"] = "0";
            if (rdItemPurchase.Checked)
            {
                drow["ActQty"] = decqty;
            }
            else
            {
                drow["ActQty"] = Actualquantity;
            }
            drow["Free"] = (txtFree.Text == "" ? "0" : txtFree.Text);
            drow["PurchaseTaxAmount"] = (txttaxAmount.Text == "" ? "0" : txttaxAmount.Text);
            // drow["TotalVAT"] = Math.Round(((PurVAT / 100) * (decqty * PurchaseRate)), 2);
            //drow["VAT"] = Math.Round(((PurVAT / 100) * (PurchaseRate)), 2);

            //drow["TotalCESS"] = Math.Round(((CESS / 100) * (decqty * PurchaseRate)), 2);
            // drow["CESS"] = CESS;

            drow["MRP"] = MRP;
            drow["SalesPrice"] = SalesPrice;
            drow["FK_Stock"] = hdfFKStock.Value;
            drow["NoofUnits"] = "0";
            drow["PurchaseDiscPercentage"] = "0";
            drow["PurchaseDiscAmt"] = PurItemDisAmt;
            dtbl.Rows.InsertAt(drow, inRowCount);
            gvItems.DataSource = dtbl;
            gvItems.DataBind();
            TotalAmount(dtbl);
            ViewState[DirectPR] = dtbl;
            FillReasonddl(false);

        }
        else
        {
            ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "alertify.alert('Item already exists')", true);
            return 1;
        }
        return 0;

    }
    protected void TotalAmountEditView(DataTable dtbl)
    {
        try
        {
            if (dtbl.Rows.Count > 0)
            {

                if (rdPurchase.Checked == true)
                {
                    txtTotalAmount.Text = dtbl.Rows[0]["TotalAmt"].ToString();
                    txtTotalAmount.Text = (Math.Round(Convert.ToDecimal(txtTotalAmount.Text), 2)).ToString();
                    //decimal DiscountAmt = Math.Round(((Convert.ToDecimal(txtTotalAmount.Text)) * (Convert.ToDecimal(txtDiscount.Text)) / 100), 2);
                    txtDiscountAmt.Text = dtbl.Rows[0]["DiscountAmt"].ToString();
                    txtDiscountAmt.Text = (Math.Round(Convert.ToDecimal(txtDiscountAmt.Text), 2)).ToString();
                    
                    txtNetAmount.Text = dtbl.Rows[0]["NetAmount"].ToString();
                    txtNetAmount.Text = (Math.Round(Convert.ToDecimal(txtNetAmount.Text), 2)).ToString();
                    txtRoundOff.Text = dtbl.Rows[0]["Roundoff"].ToString();
                    txtRoundOff.Text = (Math.Round(Convert.ToDecimal(txtRoundOff.Text), 2)).ToString();
                   
                }
                else
                {
                    txtTotalAmount.Text = Math.Round(decimal.Parse((dtbl.Compute("Sum(TotalAmt)", "")).ToString()), 2).ToString();
                    decimal DiscountAmt = Math.Round(((Convert.ToDecimal(txtTotalAmount.Text)) * (Convert.ToDecimal(txtDiscount.Text)) / 100), 2);
                    txtDiscountAmt.Text = DiscountAmt.ToString();
                    txtNetAmount.Text = dtbl.Rows[0]["NetAmount"].ToString();
                    txtNetAmount.Text = (Math.Round(Convert.ToDecimal(txtNetAmount.Text), 2)).ToString();
                    txtRoundOff.Text = dtbl.Rows[0]["Roundoff"].ToString();
                    txtRoundOff.Text = (Math.Round(Convert.ToDecimal(txtRoundOff.Text), 2)).ToString();
                }
                    //txtVAT.Text = Math.Round(decimal.Parse((dtbl.Compute("Sum(TotalVAT)", "")).ToString()), 2).ToString();
                    // txtTotCESS.Text = Math.Round(decimal.Parse((dtbl.Compute("Sum(TotalCESS)", "")).ToString()), 2).ToString();
               
                   // ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "Total()", true);
               
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
                    txtTotalAmount.Text = Math.Round(decimal.Parse((dtbl.Compute("Sum(TotalAmt)", "")).ToString()), 2).ToString();
                    //txtVAT.Text = Math.Round(decimal.Parse((dtbl.Compute("Sum(TotalVAT)", "")).ToString()), 2).ToString();
                    // txtTotCESS.Text = Math.Round(decimal.Parse((dtbl.Compute("Sum(TotalCESS)", "")).ToString()), 2).ToString();
                    decimal DiscountAmt = Math.Round(((Convert.ToDecimal(txtTotalAmount.Text)) * (Convert.ToDecimal(txtDiscount.Text)) / 100), 2);
                    txtDiscountAmt.Text = DiscountAmt.ToString();
                    ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "Total()", true);
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
    //protected void TotalAmount(DataTable dtbl)
    //{
    //    try
    //        {
    //        if (dtbl.Rows.Count > 0)
    //        {
    //            if (txtPurRefno.Text != "")
    //            {
    //                if (Approveedit == false)
    //                {
                        
    //                    ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "CalculateTotalAmount('txtActQty')", true);
    //                }
    //                else
    //                {
    //                    txtTotalAmount.Text = Math.Round(decimal.Parse((dtbl.Compute("Sum(TotalAmt)", "")).ToString()), 2).ToString();
    //                    //txtVAT.Text = Math.Round(decimal.Parse((dtbl.Compute("Sum(TotalVAT)", "")).ToString()), 2).ToString();
    //                    // txtTotCESS.Text = Math.Round(decimal.Parse((dtbl.Compute("Sum(TotalCESS)", "")).ToString()), 2).ToString();
    //                    decimal DiscountAmt = Math.Round(((Convert.ToDecimal(txtTotalAmount.Text)) * (Convert.ToDecimal(txtDiscount.Text)) / 100), 2);
    //                    txtDiscountAmt.Text = DiscountAmt.ToString();
    //                    txtNetAmount.Text =  dtbl.Rows[0]["NetAmount"].ToString();
    //                    txtNetAmount.Text = (Math.Round(Convert.ToDecimal(txtNetAmount.Text),2)).ToString();
    //                    txtRoundOff.Text = dtbl.Rows[0]["Roundoff"].ToString();
    //                    txtRoundOff.Text = (Math.Round(Convert.ToDecimal(txtRoundOff.Text), 2)).ToString();
    //                   // ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "Total()", true);
                       
    //                }
    //            }
    //            else
    //            {

    //                txtTotalAmount.Text = Math.Round(decimal.Parse((dtbl.Compute("Sum(TotalAmt)", "")).ToString()), 2).ToString();
    //                //txtVAT.Text = Math.Round(decimal.Parse((dtbl.Compute("Sum(TotalVAT)", "")).ToString()), 2).ToString();
    //                // txtTotCESS.Text = Math.Round(decimal.Parse((dtbl.Compute("Sum(TotalCESS)", "")).ToString()), 2).ToString();
    //                decimal DiscountAmt = Math.Round(((Convert.ToDecimal(txtTotalAmount.Text)) * (Convert.ToDecimal(txtDiscount.Text)) / 100), 2);
    //                txtDiscountAmt.Text = DiscountAmt.ToString();
    //                ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "Total()", true);
    //            }

    //        }
    //        else
    //        {
    //            ClearTotal();
    //        }

    //    }
    //    catch (Exception ex)
    //    {
    //        ClearTotal();
    //    }
    //}
    protected void ClearTotal()
    {
        txtTotalAmount.Text = "0";
        txtDiscount.Text = "0";
        txtDiscountAmt.Text = "0";
        txtNetAmount.Text = "0";
    }
    protected void FillReasonddl(Boolean rdPurchase)
    {
        if (rdPurchase)
        {
            DataTable dtbl = (DataTable)ViewState[PurchaseReturnItems];
            for (int i = 0; i < gvPurchaseItems.Rows.Count; i++)
            {

                Populate populate = new Populate();
                populate.FillDropdownList(((DropDownList)gvPurchaseItems.Rows[i].FindControl("ddlReason")), "Reason", "Reason", "ID_Reason", "ID_Reason", " ReasonMode=1 AND FK_Client = " + hfClient.Value, "");
                ((DropDownList)gvPurchaseItems.Rows[i].FindControl("ddlReason")).SelectedValue = dtbl.Rows[i]["FK_Reason"].ToString();
            }


        }
        else
        {

            DataTable dtbl = (DataTable)ViewState[DirectPR];
            for (int i = 0; i < gvItems.Rows.Count; i++)
            {

                Populate populate = new Populate();
                populate.FillDropdownList(((DropDownList)gvItems.Rows[i].FindControl("ddlReason")), "Reason", "Reason", "ID_Reason", "ID_Reason", "ReasonMode=1  AND FK_Client = " + hfClient.Value, "");
                string mm = dtbl.Rows[i]["FK_Reason"].ToString();
                ((DropDownList)gvItems.Rows[i].FindControl("ddlReason")).SelectedValue = dtbl.Rows[i]["FK_Reason"].ToString();
            }


        }
    }
    protected void Supplier()
    {
        try
        {
            if (txtSupplier.Text == "")
            {
                ClearSupplier();
            }
            ((TextBox)CommonPopup1.FindControl("txtName")).Text = txtSupplier.Text.Trim();
            ((TextBox)CommonPopup1.FindControl("txtRefno")).Text = txtSupRefno.Text.Trim();
            ((HiddenField)CommonPopup1.FindControl("hfColumn")).Value = "Name";
            SupplierFill(1);
            divAdd.Style.Add("display", "block");
            divView.Style.Add("display", "none");
        }
        catch (Exception ex)
        {
        }
    }
    public void PurchaseFill(int pageIndex)
    {
        ClearPurchase();
        Populate Populate = new Populate();
        PopUpPurRet.CallingFunction = "PurchaseFill";
        string strName = ((TextBox)PopUpPurRet.FindControl("txtName")).Text;
        string Refno = ((TextBox)PopUpPurRet.FindControl("txtRefno")).Text;
        string searchCol = ((HiddenField)PopUpPurRet.FindControl("hfColumn")).Value;
        searchCol = searchCol == string.Empty ? "[Name]" : "[" + searchCol + "]";
        string value = ((Label)PopUpPurRet.FindControl("popuplabel")).Text;


        Refno = Refno == string.Empty || Refno == null ? "0" : Refno;
        //Populate.FillGrid((GridView)CommonPopup1.FindControl("gdvSearch"), "View_Purchase inner join BillType on ID_BillType=FK_BillType", "ID_Purchase as ID,'[' + InvoiceNo  +'][' + CONVERT(VARCHAR(14),BillDate,103) +']' as Name,(select SupplierName from  Supplier where ID_Supplier=FK_Supplier) as Supplier,InvoiceNo,NetAmount, ID_Purchase As [Ref No]", "ID_Purchase", "BillDate", "(Quantity>0 or Free>0) and View_Purchase.FK_Client=" + hfClient.Value.Split(',')[0] + " and FK_Store=" + ddlStore.SelectedValue + " and FK_BillType=" + ddlBillType.SelectedValue + " AND (ID_Purchase =" + Refno + " OR 0=" + Refno + ") ", searchCol + "  LIKE '" + strName + "%'", pageIndex, 10, "", (Repeater)CommonPopup1.FindControl("rptPager"), this.Page);
        Populate.FillPopUpGrid((GridView)PopUpPurRet.FindControl("gdvSearch"), "View_Purchase inner join BillType on ID_BillType=FK_BillType", "ID_Purchase as ID, Billno,'[' + CONVERT(VARCHAR(14),BillDate,103) +']' as Billdate,'[' + CONVERT(VARCHAR(14),InvoiceDate,103) +']' as Invoicedate,Invoiceno,FK_Supplier", "ID_Purchase", "ID_Purchase", "(Quantity>0 or Free>0) and View_Purchase.FK_Client=" + hfClient.Value.Split(',')[0] + "  and FK_Store=" + ddlStore.SelectedValue + " and FK_BillType=" + ddlBillType.SelectedValue + " AND (ID_Purchase =" + Refno + " OR 0=" + Refno + ") AND Cancelled=0 and BillNo ", strName, null);
        Populate = null;
        // GridView gvNew = (GridView)CommonPopup1.FindControl("gdvSearch");
        //gvNew.Columns[1].Visible = false;
        //  gvNew.Columns[0].HeaderText = "S";
        //    txtSubHeadRefno.Focus();
        if (txtPurBillNo.Text == string.Empty)
        {
            lblpurr.Text = "Purchase";
            mpPurre.Show();
        }

    }
    public void SupplierFill(int pageIndex)
    {
        ClearSupplier();
        txtSupRefno.Text = "";
        Populate Populate = new Populate();
        CommonPopup1.CallingFunction = "SupplierFill";
        string strName = ((TextBox)CommonPopup1.FindControl("txtName")).Text;
        string Refno = ((TextBox)CommonPopup1.FindControl("txtRefno")).Text;
        string searchCol = ((HiddenField)CommonPopup1.FindControl("hfColumn")).Value;
        Refno = Refno == string.Empty || Refno == null ? "0" : Refno;
        if (rdPurchase.Checked)
        {
            txtSupplier.Text = strName;
            txtSupRefno.Text = Refno;
        }
        else
        {

            Populate.FillPopUpGrid((GridView)CommonPopup1.FindControl("gdvSearch"), "Supplier", "ID_Supplier as ID,SupplierName as Name", "ID_Supplier", "ID_Supplier", "FK_Client=" + hfClient.Value.Split(',')[0] + " AND (ID_Supplier =" + Refno + " OR 0=" + Refno + ")" + " AND  Active=1   AND Cancelled=0  and SupplierName ", strName, null);
            Populate = null;
            if (txtSupplier.Text == string.Empty)
            {
                lblPopupHeader.Text = "Supplier";
                mpCommon.Show();
            }
        }
        //if (rdPurchase.Checked)
        //{
        //    if (Refno == "0")
        //    {
        //        lblPopupHeader.Text = "Supplier";
        //        mpCommon.Show();
        //    }
        //    else
        //    {
        //        //SetDetails(strName, Refno);
        //        txtSupRefno.Text = Refno;
        //        txtSupplier.Text = strName;
        //    }
        //}
        //else
        //{
        //    if (txtSupplier.Text == string.Empty)
        //    {
        //        if (strName == "")
        //        {
        //            lblPopupHeader.Text = "Supplier";
        //            mpCommon.Show();
        //        }
        //        else
        //        {
        //            //SetDetails(strName, Refno);
        //            txtSupRefno.Text = Refno;
        //            txtSupplier.Text = strName;
        //        }
               
        //    }
        //}
      
        //  ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "funItemWiseSupplierClear();", true);
        

    }
    private void ClearPurchase()
    {
        txtPurBillNo.Text = "";
        txtPurRefno.Text = "";
        if (rdItemPurchase.Checked)
            ddlBillType.SelectedIndex = 0;
    }
    private void ClearSupplier()
    {
        txtSupplier.Text = "";
        txtSupRefno.Text = "";
    }
    public void SetDetails(string strName, string strRefno)
    {
        if (CommonPopup1.CallingFunction == "SupplierFill")
        {
            txtSupRefno.Text = strRefno;
            txtSupplier.Text = strName;
        }
        if (CommonPopup1.CallingFunction == "PurchaseFill")
        {
            txtPurRefno.Text = strRefno;
            txtPurBillNo.Text = strName;


            ClearItems();
            if (txtPurRefno.Text != "")
            {
                BindPurchaseItems(Convert.ToInt64(txtPurRefno.Text));
                Supplier();
                //txtRefNo.Focus();
            }
            else
            {
                gvPurchaseItems.DataSource = null;
                gvPurchaseItems.DataBind();
                ViewState[PurchaseReturnItems] = null;
            }


            //  chkActive.Focus();
            //get supplier and fill to the supplier field
        }

        else
        {
            //txtSupRefno.Text = strRefno;
            //txtSupplier.Text = strName;
            if (rdExpiry.Checked)
            {
                BindExpiryItems(Convert.ToInt64(strRefno));
            }
        }
       
    }

    public void SetPurchaseDetails(string strName, string strRefno)
    {

        if (PopUpPurRet.CallingFunction == "PurchaseFill")
        {
            txtPurRefno.Text = strRefno;
            txtPurBillNo.Text = strName;


            ClearItems();
            if (txtPurRefno.Text != "")
            {
                BindPurchaseItems(Convert.ToInt64(txtPurRefno.Text));
                Supplier();
                //txtRefNo.Focus();
            }
            else
            {
                gvPurchaseItems.DataSource = null;
                gvPurchaseItems.DataBind();
                ViewState[PurchaseReturnItems] = null;
            }


            //  chkActive.Focus();
            //get supplier and fill to the supplier field
        }
        
        //else
        //{
        //    txtSupRefno.Text = strRefno;
        //    txtSupplier.Text = strName;

        //}

    }
    public void SetSupplierDetails(string strName, string strRefno)
    {

    }
    #endregion
    #region Events

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {

            if (!IsPostBack)
            {
                //
                hfUser.Value = Request.QueryString["uid"];
                hfClient.Value = Request.QueryString["clnt"];
                ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "ClearAdd();", true);
                divAdd.Style.Add("display", "block;");
                divView.Style.Add("display", "none;");
                CheckUserAccess();
                SetReadonly();
                hfuserName.Value = Session["Username"].ToString();
                BindData();
                ddlStore.SelectedValue = Session["mainstore"].ToString();
                ddlStore.Enabled = false;
                rdPurchase.Checked = true;
                rfvPurBillNo.Enabled = false;
                rfvPurRefno.Enabled = false;
                rfvSuppName.Enabled = false;

                
                ddlBillType.Focus();
                DisplayDiv();

                rfvSuppName.Enabled = false;

                hfStore.Value = ddlStore.SelectedValue;

                GridFill(1);
                txttaxAmount.Enabled = false;
                txtCurrentStock.Enabled = false;
                txtPurchaseRateItem.Enabled = false;
                txtTotalAmountItem.Enabled = false;
                if (Convert.ToBoolean(hfIsAdd.Value) == false)
                {
                    ((Label)CommonPopupPrivilege.FindControl("popuplabel")).Text = "User does not have permission to Add";
                    mpPrivilege.Show();
                }
                if (Convert.ToBoolean(hfIsUpdate.Value) == false)
                {
                    gvPurchaseReturn.Columns[5].Visible = false;
                }
                if (Convert.ToBoolean(hfIsDelete.Value) == false)
                {
                    gvPurchaseReturn.Columns[6].Visible = false;
                }
                Transactionchecking();
                chkInterState.Checked = false;
                btnApprove.Visible = false;
            }
        }
        catch (Exception ex)
        {
            Populate.WarningMsg(this, ex.ToString());
        }

    }
    protected void imgbtnView_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            //
            divAdd.Style.Add("display", "none");
            divView.Style.Add("display", "block");

            ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "ClearAdd();ClearSearch();", true);
            GridFill(1);
            ddlSearchSupplier.Focus();
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
            //
            ClearAll();
            ddlStore.SelectedValue = Session["mainstore"].ToString();
            ddlStore.Enabled = false;
            if (Convert.ToBoolean(hfIsAdd.Value) == false)
            {
                ((Label)CommonPopupPrivilege.FindControl("popuplabel")).Text = "User does not have permission to Add";
                mpPrivilege.Show();
            }
        }
        catch (Exception ex)
        {
            Populate.WarningMsg(this, ex.ToString());
        }
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            //
            System.Threading.Thread.Sleep(10);

            bool Status = false;
            if (rdPurchase.Checked && gvPurchaseItems.Rows.Count > 0)
            {
                Status = true;

            }
            else if (rdItemPurchase.Checked && gvItems.Rows.Count > 0)
            {
                Status = true;

            }
            else if (rdExpiry.Checked && gvItems.Rows.Count > 0)
            {
                Status = true;

            }
            if (Status)
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
    protected void gvPurchaseItems_RowDeleting(object sender, GridViewDeleteEventArgs e)
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
                        dtbl.Rows[i]["FK_Reason"] = ((DropDownList)gvPurchaseItems.Rows[i].FindControl("ddlReason")).SelectedValue;
                        dtbl.Rows[i]["Quantity"] = ((TextBox)gvPurchaseItems.Rows[i].FindControl("txtQuantity")).Text;
                        dtbl.Rows[i]["TotalAmt"] = ((HiddenField)gvPurchaseItems.Rows[i].FindControl("hdfTotalAmt")).Value;
                        //Convert.ToDecimal(((Label)gvPurchaseItems.Rows[i].FindControl("lblTotalAmt")).Text);

                    }
                    dtbl.Rows.RemoveAt(e.RowIndex);
                    for (int i = 0; i < dtbl.Rows.Count; i++)
                    {
                        dtbl.Rows[i]["slno"] = i + 1;
                    }

                    TotalAmount(dtbl);
                    gvPurchaseItems.DataSource = dtbl;
                    gvPurchaseItems.DataBind();
                    ViewState[PurchaseReturnItems] = dtbl;
                    FillReasonddl(true);
                }
            }
        }
        catch (Exception ex)
        {
            Populate.WarningMsg(this, ex.ToString());
        }

    }

    protected void gvPurchaseReturn_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            //
            BlPurchase blPurchase = new BlPurchase();
            long status = 0;
            string Sts = "closed";
            if (e.CommandName == "Print")
            {
                //string confirmValue = Request.Form["confirm_value"];
                //  if (confirmValue.Length > 3)
                //      confirmValue = confirmValue.Substring(confirmValue.Length - 3, 3);
                //  if (confirmValue == "Yes")
                //  {

                GridViewRow row = (GridViewRow)((ImageButton)e.CommandSource).NamingContainer;
                int index = row.RowIndex;
                string PurachaseReturnId = gvPurchaseReturn.DataKeys[index]["ID_PurchaseReturn"].ToString();
                string popupreport = "<script> window.open('PurchaseReturnPrint.aspx?PurchaseReturnId=" + PurachaseReturnId + "', 'self', 'left=200,top=50,width=900,height=680,status=0,resizable=0,fullscreen=0,scrollbars=1');</script>";
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


    protected void Page_Changed(object sender, EventArgs e)
    {
        //   blMessage.Items.Clear();
        //
        int pageIndex = int.Parse((sender as LinkButton).CommandArgument);
        DataTable dtbl = new DataTable();
        BlPurchaseReturn blPurchaseReturn = new BlPurchaseReturn();
        blPurchaseReturn.PageSize = 10;
        blPurchaseReturn.PageIndex = Convert.ToInt16(pageIndex);
        GridFill(blPurchaseReturn.PageIndex);
        blPurchaseReturn = null;
    }
    protected void gvPurchaseReturn_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            BlPurchaseReturn blpurchaseReturn = new BlPurchaseReturn();
         
            long MasterId = Convert.ToInt64(gvPurchaseReturn.DataKeys[e.RowIndex]["ID_PurchaseReturn"].ToString());
            blpurchaseReturn.MasterId = MasterId;
            hfPurchaseReturn.Value = MasterId.ToString();
            blpurchaseReturn.FK_Supplier = 0;
            blpurchaseReturn.FK_Store = 0;
            blpurchaseReturn.FromDate = DateTime.Parse("1/1/1900");
            blpurchaseReturn.FK_Client = Convert.ToInt32(hfClient.Value.Split(',')[0]);
            blpurchaseReturn.ToDate = DateTime.MaxValue;
            blpurchaseReturn.SelectData();
               hdnapproveddelet.Value =  blpurchaseReturn.Approved.ToString();
            //
               if (hdnapproveddelet.Value == "False")
            {
                string confirmValue = Request.Form["confirm_value"];
                if (confirmValue.Length > 3)
                    confirmValue = confirmValue.Substring(confirmValue.Length - 3, 3);
                if (confirmValue == "Yes")
                {
                    long status = 0;
                    BlPurchaseReturn blpurchase = new BlPurchaseReturn();
                    blpurchase.MasterId = Convert.ToInt64(gvPurchaseReturn.DataKeys[e.RowIndex]["ID_PurchaseReturn"].ToString());
                    //blpurchase.UserCode = hfUser.Value;
                    blpurchase.UserCode = hfuserName.Value;
                    blpurchase.FK_Client = Convert.ToInt64(hfClient.Value);
                    if (hfIsDelete.Value.ToLower() == "true")
                    {
                        status = blpurchase.DeletePurchaseReturn();
                        if (status > 0)
                        {
                            GridFill(1);
                            Populate.DeleteMsg(this);
                        }
                        else
                        {
                            Populate.ReferanceMsg(this);
                        }
                    }
                    else
                    {
                        ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "toastr.warning('User does not have permission to Delete')", true);
                    }
                    blpurchase = null;
                }
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "toastr.warning('Can't delete approved details')", true);
            }
        }
        catch (Exception ex)
        {
            Populate.WarningMsg(this, ex.ToString());
        }


    }
    protected void gvPurchaseReturn_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        try
        {
            //
            BlPurchaseReturn blpurchaseReturn = new BlPurchaseReturn();
            ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "ShowAddDiv();", true);
            long MasterId = Convert.ToInt64(gvPurchaseReturn.DataKeys[e.NewSelectedIndex]["ID_PurchaseReturn"].ToString());

            blpurchaseReturn.MasterId = MasterId;
            hfPurchaseReturn.Value = MasterId.ToString();
            blpurchaseReturn.FK_Supplier = 0;
            blpurchaseReturn.FK_Store = 0;
            blpurchaseReturn.FromDate = DateTime.Parse("1/1/1900");
            blpurchaseReturn.FK_Client = Convert.ToInt32(hfClient.Value.Split(',')[0]);
            blpurchaseReturn.ToDate = DateTime.MaxValue;
            blpurchaseReturn.SelectData();
         
            txtDate.Text = blpurchaseReturn.BillDate.ToString("dd/MM/yyyy");
            try
            {
                txtSupRefno.Text = blpurchaseReturn.FK_Supplier.ToString();
                txtSupplier.Text = blpurchaseReturn.SupplierName.ToString();
                if(txtSupRefno.Text =="" &&  txtSupplier.Text=="")
                {
                Supplier();
                }
            }
            catch (Exception ex)
            {

            }
            try
            {
                ddlStore.SelectedValue = blpurchaseReturn.FK_Store.ToString();
            }
            catch (Exception ex)
            {
                ddlStore.SelectedValue = "0";
            }
            try
            {
                ddlBillType.SelectedValue = blpurchaseReturn.FK_BillType.ToString();
            }
            catch (Exception ex)
            {
                ddlBillType.SelectedValue = "0";
            }
            ddlBillType.Enabled = false;
            ddlStore.Enabled = false;
            cbxIsCredit.Checked = blpurchaseReturn.Credit;
            txtRemark.Text = blpurchaseReturn.Remarks.ToString();
            txtDiscount.Text = blpurchaseReturn.Discount.ToString();
            txtDiscountAmt.Text = blpurchaseReturn.DiscountAmt.ToString();
            hfBillNo.Value = blpurchaseReturn.BillNo.ToString();
            Approveedit = blpurchaseReturn.Approved;
            divAdd.Style.Add("display", "block");
            divView.Style.Add("display", "none;");

            // PanelHead.Enabled = false;
            if (MasterId > 0)
            {
                btnApprove.Visible = true;

            }
          
            if (blpurchaseReturn.Approved == false)
            {
                btnApprove.Visible = true; btnSave.Visible = true;
                gvPurchaseItems.Enabled = true;
            }
            else
            {
                btnApprove.Visible = false; btnSave.Visible = false; imgbtnPurBill.Enabled = false;
                gvPurchaseItems.Enabled = false;
                ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "toastr.warning('Already Approved.Cannot Edit !!!')", true);
            }
            rdItemPurchase.Enabled = false;
            rdPurchase.Enabled = false;
            rdExpiry.Enabled = false;
            hfeditpurchaase.Value = blpurchaseReturn.FK_Purchase.ToString();
            if (blpurchaseReturn.FK_Purchase != 0)
            {
                txtPurRefno.Text = blpurchaseReturn.FK_Purchase.ToString();
                txtPurBillNo.Text = "" + blpurchaseReturn.Invoice.ToString() + "" + blpurchaseReturn.BillDate.ToShortDateString() + "";

                rdPurchase.Checked = true;
                rdItemPurchase.Checked = false;
                //BindPurchaseItems(blpurchaseReturn.FK_Purchase);
                BindEditPurchaseItems(blpurchaseReturn.FK_Purchase);
                FillReasonddl(true);
            }
            else
            {
                blpurchaseReturn.FK_Client = Convert.ToInt32(hfClient.Value.Split(',')[0]);
                DataTable dtbl = blpurchaseReturn.DirectPurchaseReturnItems();
                blpurchaseReturn.ReturnMethod = "PWR";
                ViewState[DirectPR] = dtbl;
                gvItems.DataSource = dtbl;
                gvItems.DataBind();
                if (blpurchaseReturn.Expiry)
                { rdItemPurchase.Checked = false; rdExpiry.Checked = true; }
                else
                {
                    rdItemPurchase.Checked = true; rdExpiry.Checked = false;
                }
                rdPurchase.Checked = false;
                FillReasonddl(false);
                //TotalAmount(dtbl);
                TotalAmountEditView(dtbl);
            }
            DisplayDiv();
        }

        catch (Exception ex)
        {
            Populate.WarningMsg(this, ex.ToString());
        }

    }
    protected void btnClear_Click1(object sender, EventArgs e)
    {
        try
        {
            //
            ClearAll();
            ddlStore.SelectedValue = Session["mainstore"].ToString();
            ddlStore.Enabled = false;
        }
        catch (Exception ex)
        {
            Populate.WarningMsg(this, ex.ToString());
        }
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        try
        {
            //
            GridFill(1);
        }
        catch (Exception ex)
        {
            Populate.WarningMsg(this, ex.ToString());
        }
    }
    protected void btnAddItems_Click(object sender, EventArgs e)
    {
        try
        {
            //
            int a = 0;
            if (hfItem.Value.Trim() == txtitem.Text.ToString().Trim())
            {
                a = AddDataItem();

                ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "ItemClear();", true);
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
    protected void rdItemPurchase_CheckedChanged(object sender, EventArgs e)
    {
        try
        {
            //
            ClearPurchase();
            ClearAll();
            DisplayDiv();
            if (ViewState[DirectPR] != null)
            {
                DataTable dtb = (DataTable)ViewState[DirectPR];
                gvItems.DataSource = dtb;
                gvItems.DataBind();
            }
            else
            {
                gvItems.DataSource = null;
                gvItems.DataBind();
            }
            txtitem.Focus();
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "toastr.warning('" + ex.Message + "')", true);
        }
    }
    protected void rdMaterialPurchase_CheckedChanged(object sender, EventArgs e)
    {
        try
        {
            //
            ClearAll();
            DisplayDiv();
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "toastr.warning('" + ex.Message + "')", true);
        }
    }
    protected void ddlStore_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            hfStore.Value = ddlStore.SelectedValue;
            // ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "ItemClear();", true);
            // ddlStore.Focus();
        }
        catch (Exception ex)
        {

        }
    }
    protected void gvItems_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        //  blMessage.Items.Clear();
        try
        {
            //
            string confirmValue = Request.Form["confirm_value"];
            if (confirmValue.Length > 3)
                confirmValue = confirmValue.Substring(confirmValue.Length - 3, 3);
            if (confirmValue == "Yes")
            {
                if (ViewState[DirectPR] != null)
                {
                    DataTable dtbl = (DataTable)ViewState[DirectPR];

                    //if (dtbl.Columns["FK_Reason"] == null)
                    //    dtbl.Columns.Add("FK_Reason");
                    //for (int i = 0; i < dtbl.Rows.Count; i++)
                    //{
                    //    dtbl.Rows[i]["FK_Reason"] = ((DropDownList)gvItems.Rows[i].FindControl("ddlReason")).SelectedValue;
                    //}
                    //hfItem.Value = dtbl.Rows[e.RowIndex]["Item"].ToString();
                    //hdfFkItem.Value = dtbl.Rows[e.RowIndex]["FK_Items"].ToString();
                    //txtitem.Text = dtbl.Rows[e.RowIndex]["Item"].ToString();
                    //hdfFKStock.Value = dtbl.Rows[e.RowIndex]["FK_Stock"].ToString();

                    //txtFree.Text = dtbl.Rows[e.RowIndex]["Free"].ToString();
                    //txtQuantity.Text = dtbl.Rows[e.RowIndex]["Quantity"].ToString();
                    //txtPurchaseRateItem.Enabled = true;
                    //BlStock blStock = new BlStock();
                    //blStock.MasterId = Convert.ToInt64(hdfFKStock.Value);
                    //blStock.UserCode = hfUser.Value;
                    //blStock.FK_Client = Convert.ToInt64(hfClient.Value);
                    //blStock.SelectData();
                    //txtCurrentStock.Text = blStock.CurrentStock.ToString();
                    dtbl.Rows.RemoveAt(e.RowIndex);
                    for (int i = 0; i < dtbl.Rows.Count; i++)
                    {
                        dtbl.Rows[i]["slno"] = i + 1;
                    }
                    gvItems.DataSource = dtbl;
                    gvItems.DataBind();
                    TotalAmount(dtbl);
                    ViewState[DirectPR] = dtbl;
                    FillReasonddl(false);
                }
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "toastr.warning('" + ex.Message + "')", true);
        }

    }
    protected void gvPurchaseItems_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        //blMessage.Items.Clear();
        try
        {
            //
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Populate populate = new Populate();
                populate.FillDropdownList(((DropDownList)e.Row.FindControl("ddlReason")), "Reason", "Reason", "ID_Reason", "ID_Reason", "ReasonMode=1 and FK_Client = " + hfClient.Value, "");
                TextBox lblDISCOUNTAMT = (TextBox)e.Row.FindControl("lblDiscAmt");
                lblDISCOUNTAMT.Enabled = false;
            }
            
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "toastr.warning('" + ex.Message + "')", true);
        }
    }



    protected void imgbtnPurBill_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            //

            ClearPurchase();
            ((TextBox)PopUpPurRet.FindControl("txtName")).Text = txtPurBillNo.Text.Trim();
            ((TextBox)PopUpPurRet.FindControl("txtRefno")).Text = txtPurRefno.Text.Trim();

            ((HiddenField)PopUpPurRet.FindControl("hfColumn")).Value = "Bill No";
            // ((Label)CommonPopup1.FindControl("popuplabel")).Text = "Bill No";
            PurchaseFill(1);
            divAdd.Style.Add("display", "block");
            divView.Style.Add("display", "none");
        }
        catch (Exception ex)
        {
        }
    }
    protected void imgbtnSupplier_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            //
            Supplier();
        }
        catch (Exception ex)
        {
        }
    }
    protected void btnItem_Click(object sender, EventArgs e)
    {
        try
        {
            ViewState["GridDataPurchase"] = null;

            if (rdItemPurchase.Checked)
            {
                BlStock blStock = new BlStock();
                blStock.MasterId = Convert.ToInt64(hdfFKStock.Value);
                blStock.UserCode = hfUser.Value;
                blStock.FK_Client = Convert.ToInt64(hfClient.Value);
                blStock.SelectData();
                // txtItemVat.Text = blStock.VAT.ToString();
                txtPurchaseRateItem.Text = blStock.PurRate.ToString();
                hdnMRP.Value = blStock.MRP.ToString();
                hdnSalesPrice.Value = blStock.SalePrice.ToString();
                //txttaxAmount.Text = blStock.PurchaseTaxAmount.ToString();
                txttaxAmount.Text = "0";
                hfItemId.Value = blStock.FK_Items.ToString();
                txtTotalAmountItem.Text = "0";
                txtCurrentStock.Text = blStock.CurrentStock.ToString();
                BlUnit blunit = new BlUnit();
                blunit.MasterId = Convert.ToInt64(blStock.FK_Units);
                blunit.FK_Client = Convert.ToInt64(hfClient.Value);
                blunit.SelectData();
                hfUnitQuantity.Value = blunit.NoofUnits.ToString();
                blStock = null;
                txtQuantity.Focus();

                BlPurchase blpurchase = new BlPurchase();
                blpurchase.itemID = Convert.ToInt64(hfItemId.Value);
                blpurchase.Purchase = true;
                DataTable dtPurchase = blpurchase.SelectGriddata();
                blpurchase.Purchase = false;
                cbxIncludeTax.Checked = true;
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

                    if (chkInterState.Checked == true && Convert.ToBoolean(row["TaxInterState"].ToString()) == true
                        && Convert.ToInt32(hfItemId.Value) == Convert.ToInt32(row["ID_items"].ToString()))
                    {
                        if (Convert.ToInt32(row["GSTType"].ToString()) == 1 || Convert.ToInt32(row["GSTType"].ToString()) == 2)
                            TotalTaxPercentage += Convert.ToDecimal(row["TaxPercentage"].ToString());
                    }
                    if (chkInterState.Checked == false && Convert.ToBoolean(row["TaxIntraState"].ToString()) == true && Convert.ToInt32(hfItemId.Value) == Convert.ToInt32(row["ID_items"].ToString()))
                    {
                        if (Convert.ToInt32(row["GSTType"].ToString()) == 1 || Convert.ToInt32(row["GSTType"].ToString()) == 2)
                            TotalTaxPercentage += Convert.ToDecimal(row["TaxPercentage"].ToString());
                    }

                    hfTotTaxPercentage.Value = Convert.ToString(TotalTaxPercentage);
                    if (Convert.ToInt32(row["GSTType"].ToString()) == 1 || Convert.ToInt32(row["GSTType"].ToString()) == 2)
                    {
                        TotalTaxPerCS += Convert.ToDecimal(row["TaxPercentage"].ToString());
                    }
                    btnS.Text = String.Format("{0:0.##}", TotalTaxPerCS) + "%";

                }

                //foreach (DataRow row in dtPurchase.Rows)
                //{

                //    if (chkInterState.Checked == true && Convert.ToBoolean(row["TaxInterState"].ToString()) == true)
                //    {
                //        TotalTaxPercentage += Convert.ToDecimal(row["TaxPercentage"].ToString());
                //    }
                //    if (chkInterState.Checked == false && Convert.ToBoolean(row["TaxIntraState"].ToString()) == true)
                //    {
                //        TotalTaxPercentage += Convert.ToDecimal(row["TaxPercentage"].ToString());
                //    }
                //    if (  Convert.ToInt32(row["GSTType"].ToString()) == 3 || Convert.ToInt32(row["GSTType"].ToString()) == 4 || Convert.ToInt32(row["GSTType"].ToString()) == 5)
                //    {
                //        TotalTaxPerCS += Convert.ToDecimal(row["TaxPercentage"].ToString());
                   
                //    }
                //    else if (Convert.ToInt32(row["GSTType"].ToString()) == 1 || Convert.ToInt32(row["GSTType"].ToString()) == 2 || Convert.ToInt32(row["GSTType"].ToString()) == 4 || Convert.ToInt32(row["GSTType"].ToString()) == 5)
                //    {
                //        TotalTaxPerCS += Convert.ToDecimal(row["TaxPercentage"].ToString());
                //    }
                  
                    //if (chkInterState.Checked == true && Convert.ToBoolean(row["TaxInterState"].ToString()) == true
                    //    && Convert.ToInt32(hfItemId.Value) == Convert.ToInt32(row["ID_items"].ToString()))
                    //{
                    //    if (Convert.ToInt32(row["GSTType"].ToString()) == 1 || Convert.ToInt32(row["GSTType"].ToString()) == 2)
                    //        TotalTaxPercentage += Convert.ToDecimal(row["TaxPercentage"].ToString());
                    //}
                    //if (chkInterState.Checked == false && Convert.ToBoolean(row["TaxIntraState"].ToString()) == true && Convert.ToInt32(hfItemId.Value) == Convert.ToInt32(row["ID_items"].ToString()))
                    //{
                    //    if (Convert.ToInt32(row["GSTType"].ToString()) == 1 || Convert.ToInt32(row["GSTType"].ToString()) == 2)
                    //        TotalTaxPercentage += Convert.ToDecimal(row["TaxPercentage"].ToString());
                    //}

                   
                    //if (Convert.ToInt32(row["GSTType"].ToString()) == 1 || Convert.ToInt32(row["GSTType"].ToString()) == 2)
                    //{
                    //    TotalTaxPerCS += Convert.ToDecimal(row["TaxPercentage"].ToString());
                    //}
                   // btnS.Text = String.Format("{0:0.##}", TotalTaxPerCS) + "%";

               // }
                //foreach (DataRow row in dt.Rows)
                //{

                //    if (Convert.ToBoolean(row["TaxIntraState"].ToString()) == true)
                //    {
                //        TotalTaxSales += Convert.ToDecimal(row["TaxPercentage"].ToString());
                //    }


                //    hfTotTaxsalesPercentage.Value = Convert.ToString(TotalTaxSales);
                //}
                //cbxIncludeTax.Enabled = true;
                //cbxIncludeTax.Checked = true;
                //cbxIncludeTax.Enabled = false;

              
                //ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "getpurchaseamount()", true);

                txtQuantity.Focus();
            }






        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "toastr.warning('" + ex.Message + "')", true);
        }
    }





    protected void btnS_Click(object sender, EventArgs e)
    {
        //BlPurchase blpurchase = new BlPurchase();
        //DataTable dt;

        //Populate Populate = new Populate();
        //CommonPopupGrid1.CallingFunction = "Purchase";
        //string Refno = ((TextBox)CommonPopupGrid1.FindControl("txtRefno")).Text;
        //string searchCol = ((HiddenField)CommonPopupGrid1.FindControl("hfColumn")).Value;
        //Refno = Refno == string.Empty || Refno == null ? "0" : Refno;
        //GridView gvNew = (GridView)CommonPopupGrid1.FindControl("gdvSearch");
        //BlStock blStock = new BlStock();
        //if (hfItemId.Value != "")
        //    blpurchase.itemID = Convert.ToInt64(hfItemId.Value);
        //dt = blpurchase.SelectGriddataPurchase();
        //dt.Columns.Add("TaxAmount", typeof(decimal));
        //gvNew.DataSource = dt;
        //gvNew.DataBind();
        //gvNew.Columns[3].Visible = false;
        //lblPopupH.Text = "Purchase Tax";
        //mpGrid.Show();





        BlPurchase blpurchase = new BlPurchase();
        DataTable dt;
        Populate Populate = new Populate();
        CommonPopupGrid1.CallingFunction = "Purchase";
        string Refno = ((TextBox)CommonPopupGrid1.FindControl("txtRefno")).Text;
        string searchCol = ((HiddenField)CommonPopupGrid1.FindControl("hfColumn")).Value;
        Refno = Refno == string.Empty || Refno == null ? "0" : Refno;
        GridView gvNew = (GridView)CommonPopupGrid1.FindControl("gdvSearch");
        blpurchase.itemID = Convert.ToInt64(hfItemId.Value.ToString() == "" ? "0" : hfItemId.Value);
        blpurchase.Purchase = true;
        decimal s = Convert.ToDecimal(hfTotTaxPercentage.Value == "" ? "0" : hfTotTaxPercentage.Value);
        dt = blpurchase.SelectGriddata();
        dt.Columns.Add("TaxAmount", typeof(decimal));

        foreach (DataRow row in dt.Rows)
        {
            TaxAmount = 0;
            if (Convert.ToDecimal(hfTotTaxPercentage.Value == "" ? "0" : hfTotTaxPercentage.Value) != 0)
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
        gvNew.DataBind(); gvNew.Columns[3].Visible = false;
        lblPopupH.Text = "Purchase Tax";
        mpGrid.Show();

    }
    protected void hiddendiscountAmount_Click(object sender, EventArgs e)
    {

        //foreach (GridViewRow gr in gvPurchaseItems.Rows)
        //{

        //    if (gr.RowType == DataControlRowType.DataRow)
        //    {
        //        BlPurchaseReturnDetails objP = new BlPurchaseReturnDetails();
        //        objP.PurchaseDiscAmount = Convert.ToDecimal(((Label)gr.FindControl("lblDiscAmt")).Text);
        //    }
        //}
    }
    protected void gvPurchaseReturn_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvPurchaseReturn.PageIndex = e.NewPageIndex;
        GridFill(e.NewPageIndex);
    }
    #endregion
    protected void rdExpiry_CheckedChanged(object sender, EventArgs e)
    {
        try
        {
            //
            ClearPurchase();
            ClearAll();
            DisplayDiv();
            if (ViewState[DirectPR] != null)
            {
                DataTable dtb = (DataTable)ViewState[DirectPR];
                gvItems.DataSource = dtb;
                gvItems.DataBind();
            }
            else
            {
                gvItems.DataSource = null;
                gvItems.DataBind();
            }
            txtitem.Focus();
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "toastr.warning('" + ex.Message + "')", true);
        }
    }
    protected void chkInterState_CheckedChanged(object sender, EventArgs e)
    {

    }
    private void BindEditPurchaseItems(long purchaseId)
    {
        try
        {
            BlPurchaseReturn blpurchasereturn = new BlPurchaseReturn();
            blpurchasereturn.FK_Purchase = purchaseId;
            blpurchasereturn.FK_Client = Convert.ToInt32(hfClient.Value);
            blpurchasereturn.ReturnMethod = "PWR";
            if (hfPurchaseReturn.Value == "")
            {
                blpurchasereturn.MasterId = 0;
            }
            else
            {
                blpurchasereturn.MasterId = Convert.ToInt64(hfPurchaseReturn.Value);
            }
            DataTable dtbl = new DataTable();
            if (rdPurchase.Checked == true)
            {
                dtbl = blpurchasereturn.SelectPurchaseWiseItemsSelect();
                gvPurchaseItems.DataSource = dtbl;
                gvPurchaseItems.DataBind();
            }
            else
            {
                dtbl = blpurchasereturn.SelectPurchaseItems();
                gvPurchaseItems.DataSource = dtbl;
                gvPurchaseItems.DataBind();
            }
            BlPurchase blp = new BlPurchase();
            blp.MasterId = purchaseId;
            blp.FK_Client = Convert.ToInt64(hfClient.Value);
            blp.FromDate = DateTime.Parse("1/1/1900");
            blp.SelectPurchase();
            ddlStore.SelectedValue = blp.FK_Store.ToString();
            ddlStore.Enabled = false;
            ddlBillType.Enabled = false;
            txtSupRefno.Text = blp.FK_Supplier.ToString();

            txtSupplier.Text = blp.SupplierName.ToString();
            hdfDate.Value = blp.BillDate.ToString();
            cbxIsCredit.Checked = blp.Credit;
            hfInvNo.Value = blp.InvoiceNo;
            hfQtyValidation.Value = "1";
            txtDiscount.Text = blp.DiscountPer.ToString();
            txtDiscountAmt.Text = blp.DiscountAmt.ToString();
            ViewState[PurchaseReturnItems] = dtbl;

            //TotalAmount(dtbl);
            TotalAmountEditView(dtbl);
            blpurchasereturn = null;
            blp = null;
        }
        catch (Exception ex)
        {

        }
    }
    protected void btnApprove_Click(object sender, EventArgs e)
    {

        try
        {

            string confirmValue = Request.Form["confirm_value"];
            if (confirmValue.Length > 3)
                confirmValue = confirmValue.Substring(confirmValue.Length - 3, 3);
            if (confirmValue == "Yes")
            {
                //gvPurchaseItems
                System.Threading.Thread.Sleep(10);

                bool Status = false;
                if (rdPurchase.Checked == true)
                {
                    if (gvPurchaseItems.Rows.Count > 0)
                    {
                        Status = true;
                    }
                }
                else
                {
                    if (gvItems.Rows.Count > 0)
                    {
                        Status = true;

                    }
                }

                if (Status)
                {
                    Approve();
                }
                else
                {
                    ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "toastr.warning('Atleast one item required')", true);
                }
            }
            else
            {
                BlPurchaseReturn blpurchaseReturn = new BlPurchaseReturn();
                blpurchaseReturn.MasterId = Convert.ToInt64(hfPurchaseReturn.Value);
                blpurchaseReturn.FK_Supplier = 0;
                blpurchaseReturn.FK_Store = 0;
                blpurchaseReturn.FromDate = DateTime.Parse("1/1/1900");
                blpurchaseReturn.FK_Client = Convert.ToInt32(hfClient.Value.Split(',')[0]);
                blpurchaseReturn.ToDate = DateTime.MaxValue;
                blpurchaseReturn.SelectData();

                txtDate.Text = blpurchaseReturn.BillDate.ToString("dd/MM/yyyy");
                try
                {
                    txtSupRefno.Text = blpurchaseReturn.FK_Supplier.ToString();
                    txtSupplier.Text = blpurchaseReturn.SupplierName.ToString();
                    Supplier();
                }
                catch (Exception ex)
                {

                }
                try
                {
                    ddlStore.SelectedValue = blpurchaseReturn.FK_Store.ToString();
                }
                catch (Exception ex)
                {
                    ddlStore.SelectedValue = "0";
                }
                try
                {
                    ddlBillType.SelectedValue = blpurchaseReturn.FK_BillType.ToString();
                }
                catch (Exception ex)
                {
                    ddlBillType.SelectedValue = "0";
                }
                ddlBillType.Enabled = false;
                ddlStore.Enabled = false;
                cbxIsCredit.Checked = blpurchaseReturn.Credit;
                txtRemark.Text = blpurchaseReturn.Remarks.ToString();
                txtDiscount.Text = blpurchaseReturn.Discount.ToString();
                txtDiscountAmt.Text = blpurchaseReturn.DiscountAmt.ToString();
                hfBillNo.Value = blpurchaseReturn.BillNo.ToString();
                if (blpurchaseReturn.FK_Purchase != 0)
                {
                    txtPurRefno.Text = blpurchaseReturn.FK_Purchase.ToString();
                    txtPurBillNo.Text = "" + blpurchaseReturn.Invoice.ToString() + "" + blpurchaseReturn.BillDate.ToShortDateString() + "";

                    rdPurchase.Checked = true;
                    rdItemPurchase.Checked = false;
                    //BindPurchaseItems(blpurchaseReturn.FK_Purchase);
                    BindEditPurchaseItems(blpurchaseReturn.FK_Purchase);
                    FillReasonddl(true);
                }
                else
                {
                    blpurchaseReturn.FK_Client = Convert.ToInt32(hfClient.Value.Split(',')[0]);
                    DataTable dtbl = blpurchaseReturn.DirectPurchaseReturnItems();
                    blpurchaseReturn.ReturnMethod = "PWR";
                    ViewState[DirectPR] = dtbl;
                    gvItems.DataSource = dtbl;
                    gvItems.DataBind();
                    if (blpurchaseReturn.Expiry)
                    { rdItemPurchase.Checked = false; rdExpiry.Checked = true; }
                    else
                    {
                        rdItemPurchase.Checked = true; rdExpiry.Checked = false;
                    }
                    rdPurchase.Checked = false;
                    FillReasonddl(false);
                    TotalAmount(dtbl);
                }


               DisplayDiv();
                //long feditpurchaase = Convert.ToInt64(hfeditpurchaase.Value == "" ? "0" : hfeditpurchaase.Value);
                //BindEditPurchaseItems(feditpurchaase);
                //FillReasonddl(true);
              //  TotalAmount();
            }
        }
        catch (Exception ex)
        {
            Populate.WarningMsg(this, ex.ToString());
        }
    }
    public static void ApproveMsg(Page pge)
    {
        try
        {

            ScriptManager.RegisterClientScriptBlock(pge, pge.GetType(), "Script", "ShowAddDiv();", true);

            ScriptManager.RegisterStartupScript(pge, pge.GetType(), "Script", "toastr.success('Data Approved Successfully');ClearAdd();", true);
        }
        catch
        {
        }
    }
    protected void Approve()
    {
        long Status = 0;
        BlPurchaseReturn blpurchase = new BlPurchaseReturn();
        blpurchase.MasterId = Convert.ToInt64(hfPurchaseReturn.Value);
        // blpurchase.TransactionID = Convert.ToInt64(ViewState["TransactionID"]);
        blpurchase.FK_Client = Convert.ToInt64(hfClient.Value);
        blpurchase.FK_Store = Convert.ToInt64(Session["mainstore"].ToString());
        blpurchase.UserCode = hfuserName.Value;
        blpurchase.ReturnMethod = "PWR";
        if (hfIsApprove.Value.ToLower() == "true")
        {
            Status = blpurchase.ApproveData();
            if (Status == 0)
            {
                ClearAll();
                GridFill(1);
                ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "ShowAddDiv();", true);
                ApproveMsg(this);
                //*** Print Purchase
                //string PurachaseReturnId = Status.ToString();
                //string popupreport = "<script> window.open('PurchaseReturnPrint.aspx?PurchaseReturnId=" + PurachaseReturnId + "', 'self', 'left=200,top=50,width=900,height=680,status=0,resizable=0,fullscreen=0,scrollbars=1');</script>";
                //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "Success", popupreport, false);
                //ddlStore.SelectedValue = Session["mainstore"].ToString();
                //ddlStore.Enabled = false;
            }
            else if (Status == -2)
            {
                ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "toastr.warning('Insufficient Stock')", true);
            }
        }
        else
        {
            ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "toastr.warning('User does not have permission to Approve')", true);


        }


    }
    protected void gvPurchaseReturn_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            //



           
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                var row = (GridViewRow)e.Row;
                var approved = Convert.ToBoolean(gvPurchaseReturn.DataKeys[e.Row.RowIndex]["Approved"]);
                ImageButton imgDelete = row.FindControl("ImageButton2") as ImageButton;
                Button btnapprove = (Button)e.Row.FindControl("btnApprovedStatus");
                if (btnapprove.Text == "Approved")
                {
                    btnapprove.Enabled = false;
                    imgDelete.Enabled = false; imgDelete.ToolTip = "Can't delete approved details";
                    btnapprove.BackColor = System.Drawing.Color.DarkGreen;//true means enable else you may set false to disable button 
                }
                else
                {
                    btnapprove.Enabled = false;
                    btnapprove.BackColor = System.Drawing.Color.DarkRed;
                    imgDelete.Enabled = true; imgDelete.ToolTip = "Delete";
                }
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "toastr.warning('" + ex.Message + "')", true);
        }
    }
    protected void cvSupplierRef_ServerValidate(object source, ServerValidateEventArgs args)
    {
        if (rdPurchase.Checked) { args.IsValid = true; return; }
        long refNo;
        args.IsValid = long.TryParse(txtSupRefno.Text, out refNo) && refNo > 0;
    }

}