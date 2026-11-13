/*----------------------------------------------------------------------------------------------------------------------------------------------------------
Purpose    : Insert/Update/Delete SalesReturn 
Author     : VINISHA
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


public partial class SalesReturn : System.Web.UI.Page
{

    private const string SalesReturnItems = "SalesReturnItems";
    private const string DirectSR = "DirectSales";
    long TransactionID;

    #region Functions

    protected void BindData()
    {
        Populate populate = new Populate();
        populate.FillDropdownList(ddlStore, "Store", "StoName", "ID_Store", "StoName", " Active='True' and Cancelled=0   and FK_Client = " + hfClient.Value, "");
        populate.FillDropdownList(ddlBillType, "BillType", "TypeName", "ID_BillType", "ID_BillType", " Cancelled=0 AND BillType=1  and FK_Client = " + hfClient.Value, "");
        populate.FillDropdownList(ddlSearchCustomer, "Customer", "CustomerName", "ID_Customer", "CustomerName", "  Active='True' and Cancelled=0 AND  FK_Client = " + hfClient.Value, "");
        populate.FillDropdownList(ddlSearchStore, "Store", "StoName", "ID_Store", "StoName", "   Active='True' and Cancelled=0  and FK_Client = " + hfClient.Value, "");
        populate.FillDropdownList(ddlCounter, "Counters", "CounterName", "ID_Counters", "CounterName", " Cancelled=0 AND FK_Client= " + hfClient.Value.Split(',')[0], "");
        populate = null;
        txtDate.Text = DateTime.Now.ToShortDateString();


    }
    public void SetReadonly()
    {
        txtDate.Attributes.Add("readonly", "readonly");
        txtFromDate.Attributes.Add("readonly", "readonly");
        txtTodate.Attributes.Add("readonly", "readonly");
        txtDate.Text = txtFromDate.Text = txtTodate.Text = DateTime.Now.ToString("dd/MM/yyyy");
        txtTotalAmount.Attributes.Add("readonly", "readonly");
        txtNetAmount.Attributes.Add("readonly", "readonly");
        txtRoundOff.Attributes.Add("readonly", "readonly");
        txtCustomer.Attributes.Add("readonly", "readonly");
        //txtSalesBillNo.Attributes.Add("readonly", "readonly");
        txtTotalAmountItem.Attributes.Add("readonly", "readonly");
        txtQuantity.Attributes["onfocus"] = "javascript:this.select();";
        //txtFree.Attributes["onfocus"] = "javascript:this.select();";
    }
    private void BindSalesItems(long SalesId)
    {
        try
        {
            BlSaleReturn BlSaleReturn = new BlSaleReturn();
            BlSaleReturn.Fk_Sale = SalesId;
            BlSaleReturn.FK_Client = Convert.ToInt32(hfClient.Value);

            if (hfSalesReturn.Value == "")
                BlSaleReturn.MasterId = 0;
            else
                BlSaleReturn.MasterId = Convert.ToInt64(hfSalesReturn.Value);

            DataTable dtbl = BlSaleReturn.SelectSaleReturnItems();
            gvSalesItems.DataSource = dtbl;
            gvSalesItems.DataBind();

            if (dtbl.Rows.Count > 0 && BlSaleReturn.MasterId == 0)
            {

                string sessionStore = Session["mainstore"].ToString();
                try
                {
                    ddlStore.SelectedValue = sessionStore;
                }
                catch
                {
                    ddlStore.SelectedValue = "0";
                }
                ddlStore.Enabled = false;
                hfStore.Value = sessionStore;


                try
                {
                    ddlCounter.SelectedValue = dtbl.Rows[0]["FK_Counter"].ToString();
                }
                catch
                {

                    ddlCounter.Enabled = true;
                }

                try
                {
                    txtCustRefno.Text = dtbl.Rows[0]["FK_Customer"].ToString() == "0"
                        ? "" : dtbl.Rows[0]["FK_Customer"].ToString();
                    txtCustomer.Text = dtbl.Rows[0]["CustomerName"].ToString();
                }
                catch
                {
                    txtCustRefno.Text = "";
                    txtCustomer.Text = "";
                }

                hdfDate.Value = dtbl.Rows[0]["BillDate"].ToString();
            }

            ViewState[SalesReturnItems] = dtbl;
            TotalAmount(dtbl);
            BlSaleReturn = null;
        }
        catch (Exception ex)
        {
            // Optionally log: Populate.WarningMsg(this, ex.ToString());
        }
    }
    protected void CheckUserAccess()
    {
        BlUserAccess bluseraccess = new BlUserAccess();
        Populate populate = new Populate();

        bluseraccess.FK_Users = Convert.ToInt64(hfUser.Value);
        bluseraccess.FK_Pages = 36;
        bluseraccess.FK_Client = Convert.ToInt64(hfClient.Value);
        bluseraccess.CheckUserAccess();
        if (Convert.ToBoolean(bluseraccess.IsView) == false)
        {
            // Response.Redirect("Home.aspx?");
        }
        Boolean PageAccess = (Convert.ToBoolean(bluseraccess.IsSalesReturn));
        if (PageAccess == true)
        {
            hfIsAdd.Value = (Convert.ToBoolean(bluseraccess.IsAdd)).ToString();
            hfIsUpdate.Value = (Convert.ToBoolean(bluseraccess.IsUpdate)).ToString();
            hfIsDelete.Value = (Convert.ToBoolean(bluseraccess.IsDelete)).ToString();
        }
        else
        {
            hfIsAdd.Value = "false";
            hfIsUpdate.Value = "false";
            hfIsDelete.Value = "false";
        }

        bluseraccess = null;
    }
    protected void GridFill(int currentPage)
    {
        BlSaleReturn blSaleReturn = new BlSaleReturn();
        DataTable dtbl = new DataTable();
        blSaleReturn.MasterId = 0;
        blSaleReturn.FK_Customer = ddlSearchCustomer.SelectedValue == "0" ? 0 : Convert.ToInt64(ddlSearchCustomer.SelectedValue);
        //blSaleReturn.FK_Store = Convert.ToInt64(ddlStore.SelectedValue);
        blSaleReturn.FK_Store = Convert.ToInt64(Session["mainstore"].ToString());
        blSaleReturn.FK_Client = Convert.ToInt64(hfClient.Value);
        blSaleReturn.FromDate = DateTime.ParseExact(txtFromDate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
        blSaleReturn.ToDate = DateTime.ParseExact(txtTodate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
        currentPage = currentPage == 0 ? 1 : currentPage;
        blSaleReturn.PageIndex = Convert.ToInt16(currentPage);
        dtbl = blSaleReturn.SelectAllData();
        gvSalesReturn.DataSource = dtbl;
        gvSalesReturn.DataBind();
        blSaleReturn = null;
    }
    private void Transactionchecking()
    {
        BlFunction blfun = new BlFunction();
        blfun.Module = "SALRET";
        TransactionID = blfun.GetTransactionID("SALRET", hfuserName.Value);
        ViewState["TransactionID"] = TransactionID;
    }
    protected void Save()
    {
        long Status = 0;
        BlSaleReturn blSaleReturn = new BlSaleReturn();
        try
        {
            if (rdSales.Checked)
            {

                DateTime PR = DateTime.ParseExact(txtDate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                DateTime dtP = DateTime.Parse(hdfDate.Value);
                if (PR < dtP)
                {
                    Status = 1;
                }
            }
        }
        catch
        {
        }
        if (Status == 0)
        {
            //blSaleReturn.FK_Store = Convert.ToInt64(ddlStore.SelectedValue);
            blSaleReturn.FK_Store = Convert.ToInt64(Session["mainstore"].ToString());

            blSaleReturn.FK_BillType = Convert.ToInt64(ddlBillType.SelectedValue);
            blSaleReturn.BillNo = Convert.ToInt64(hfBillNo.Value);
            blSaleReturn.BillDate = DateTime.ParseExact(txtDate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            blSaleReturn.FK_Customer = Convert.ToInt64(txtCustRefno.Text == "" || txtCustomer.Text == "" ? "0" : txtCustRefno.Text);
            blSaleReturn.CustomerName = txtCustomer.Text;
            blSaleReturn.BillTotal = Convert.ToDecimal(txtTotalAmount.Text == "" ? "0" : txtTotalAmount.Text);
            blSaleReturn.RoundOff = Convert.ToDecimal(txtRoundOff.Text == "" ? "0" : txtRoundOff.Text);
            blSaleReturn.NetAmount = Convert.ToDecimal(txtNetAmount.Text == "" ? "0" : txtNetAmount.Text);
            string remarks = txtRemark.Text.Trim();
            blSaleReturn.Remarks = remarks.Length > 500 ? remarks.Substring(0, 500) : remarks;
            blSaleReturn.UserCode = hfuserName.Value;
            blSaleReturn.Cancelled = false;
            blSaleReturn.Cancelleduser = hfUser.Value;
            blSaleReturn.FK_Counter = Convert.ToInt64(ddlCounter.SelectedValue);
            blSaleReturn.FK_Client = Convert.ToInt64(hfClient.Value);
            blSaleReturn.Fk_Sale = Convert.ToInt64(txtSalesRefno.Text == "" ? "0" : txtSalesRefno.Text);
            blSaleReturn.TransactionID = Convert.ToInt64(ViewState["TransactionID"]);
            blSaleReturn.DirectReturn = rdItemSales.Checked;
            blSaleReturn.Credit = cbxIsCredit.Checked;
            ArrayList arlist = new ArrayList();
            BlSaleReturnDetails blSaleReturnDetails;
            if (blSaleReturn.DirectReturn)
            {
                int RowIndex = 0;
                DataTable dtbl = (DataTable)ViewState[DirectSR];
                foreach (DataRow dr in dtbl.Rows)
                {
                    blSaleReturnDetails = new BlSaleReturnDetails();
                    blSaleReturnDetails.FK_Items = Convert.ToInt64(dr["FK_Items"].ToString());
                    blSaleReturnDetails.FK_Stock = Convert.ToInt64(dr["FK_Stock"].ToString());
                    blSaleReturnDetails.MRP = Convert.ToDecimal(dr["MRP"].ToString());
                    blSaleReturnDetails.SalesPrice = Convert.ToDecimal(dr["SalesRate"].ToString());
                    blSaleReturnDetails.ActualSalesPrice = Convert.ToDecimal(dr["ActualSalPrice"].ToString());
                    blSaleReturnDetails.Quantity = Convert.ToDecimal(dr["Quantity"].ToString());
                    blSaleReturnDetails.BatchNo = dr["BatchNo"].ToString();
                    blSaleReturnDetails.ExpiryDate = Convert.ToDateTime(dr["ExpiryDate"].ToString());
                    blSaleReturnDetails.VAT = Convert.ToDecimal(dr["TaxAmount"].ToString());
                    blSaleReturnDetails.FK_Reason = Convert.ToInt64((((DropDownList)gvItems.Rows[RowIndex].FindControl("ddlReason")).SelectedValue));
                    arlist.Add(blSaleReturnDetails);
                    blSaleReturnDetails = null;
                    RowIndex = RowIndex + 1;
                }
            }

            else
            {
                int RowIndex = 0;

                foreach (GridViewRow gr in gvSalesItems.Rows)
                {
                    if (gr.RowType == DataControlRowType.DataRow)
                    {
                        decimal qty = 0;
                        string rawQty = ((TextBox)gr.FindControl("txtQuantity")).Text;
                        decimal.TryParse(rawQty, out qty);

                        if (qty <= 0)
                        {
                            RowIndex++;
                            continue;
                        }

                        // Validate against available quantity
                        decimal balQty = 0;
                        decimal.TryParse(
                            ((HiddenField)gr.FindControl("hdfBalanceQuantity")).Value,
                            out balQty);

                        if (qty > balQty)
                        {
                            ScriptManager.RegisterStartupScript(
                                Page,
                                typeof(Page),
                                "QtyValidation",
                                "alertify.alert('Return quantity (" + qty +
                                ") exceeds available quantity (" + balQty +
                                ") for item " +
                                ((Label)gr.FindControl("lblItem")).Text.Replace("'", "\\'") +
                                "');",
                                true);

                            return;
                        }

                        blSaleReturnDetails = new BlSaleReturnDetails();
                        blSaleReturnDetails.FK_Items = Convert.ToInt64(gvSalesItems.DataKeys[gr.RowIndex]["FK_Items"].ToString());
                        blSaleReturnDetails.FK_Stock = Convert.ToInt64(gvSalesItems.DataKeys[gr.RowIndex]["FK_Stock"].ToString());
                        blSaleReturnDetails.MRP = Convert.ToDecimal(gvSalesItems.DataKeys[gr.RowIndex]["MRP"].ToString());
                        blSaleReturnDetails.ActualSalesPrice = Convert.ToDecimal(gvSalesItems.DataKeys[gr.RowIndex]["ActSalPrice"].ToString());
                        blSaleReturnDetails.BatchNo = gvSalesItems.DataKeys[gr.RowIndex]["BatchNo"].ToString();
                        blSaleReturnDetails.ExpiryDate = Convert.ToDateTime(gvSalesItems.DataKeys[gr.RowIndex]["ExpiryDate"].ToString());

                        string str3 = ((Label)gr.FindControl("lblSalesRate")).Text;
                        str3 = string.IsNullOrEmpty(str3) ? "0" : str3;

                        blSaleReturnDetails.SalesPrice = Convert.ToDecimal(str3);
                        blSaleReturnDetails.Quantity = qty;
                        blSaleReturnDetails.FK_Reason = Convert.ToInt64(
                            ((DropDownList)gvSalesItems.Rows[RowIndex].FindControl("ddlReason")).SelectedValue);

                        blSaleReturnDetails.VAT =
                            blSaleReturnDetails.SalesPrice -
                            blSaleReturnDetails.ActualSalesPrice;

                        arlist.Add(blSaleReturnDetails);
                        blSaleReturnDetails = null;

                        RowIndex++;
                    }
                }
            }
            blSaleReturn.XmlSR = Common.WriteToXml(arlist);
            if (Status == 0)
            {
                if (hfSalesReturn.Value == string.Empty)
                {
                    if (hfIsAdd.Value.ToLower() == "true")
                    {
                        Status = blSaleReturn.InsertData();
                        if (Status > 0)
                        {
                            ClearAll();
                            GridFill(1);
                            ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "ShowAddDiv();", true);
                            Populate.InsertMsg(this);

                            string SalesReturnId = Status.ToString();
                            string popupreport = "<script> window.open('SalesReturnPrint.aspx?SalesReturnId=" + SalesReturnId + "', 'self', 'left=200,top=50,width=900,height=680,status=0,resizable=0,fullscreen=0,scrollbars=1');</script>";
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "Success", popupreport, false);

                            ddlStore.SelectedValue = Session["mainstore"].ToString();
                            ddlStore.Enabled = false;

                        }
                        else if (Status == -8)
                        {
                            ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "toastr.error('Cannot update data.. TransactionID already exist')", true);
                        }
                        else if (Status == -1 || Status == -2)
                        {
                            ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "ShowAddDiv();", true);
                            Populate.ErrorMessageBox(this, "Cannot insert data, No stock  available.");


                        }
                        else if (Status == -3)
                        {
                            Populate.ErrorMessageBox(this, " Return AccHead has not set in Bill Type");
                        }
                        else if (Status == -66)
                        {
                            ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "toastr.error('Please Update FinYear For Transaction')", true);
                        }
                        else
                        {
                            Populate.ErrorMessageBox(this, "Cannot insert data");

                        }
                    }
                    else
                    {
                        ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "toastr.warning('User does not have permission to add')", true);

                    }

                }
                else
                {
                    long decid = Convert.ToInt64(hfSalesReturn.Value);
                    blSaleReturn.MasterId = decid;
                    if (hfIsUpdate.Value.ToLower() == "true")
                    {

                        Status = blSaleReturn.UpdateData();
                        if (Status > 0)
                        {
                            ClearAll();
                            GridFill(1);
                            Populate.UpdateMsg(this);
                            string SalesReturnId = Status.ToString();
                            string popupreport = "<script> window.open('SalesReturnPrint.aspx?SalesReturnId=" + SalesReturnId + "', 'self', 'left=200,top=50,width=900,height=680,status=0,resizable=0,fullscreen=0,scrollbars=1');</script>";
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "Success", popupreport, false);

                        }

                        else if (Status == -1 || Status == -2)
                        {
                            ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "ShowAddDiv();", true);
                            Populate.ErrorMessageBox(this, "Cannot update data, No stock  available.");
                        }
                        else if (Status == -66)
                        {
                            ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "toastr.error('Please Update FinYear For Transaction')", true);
                        }
                        else
                        {
                            Populate.ErrorMessageBox(this, "Cannot update data");

                        }
                    }
                    else
                    {
                        ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "toastr.warning('User does not have permission to Update')", true);
                    }


                }
            }
            else
            {
                Populate.ErrorMessageBox(this, "Quantity Cannot Exceed Available Stock");
            }
        }
        else
        {
            Populate.ErrorMessageBox(this, "Sales Return date cannot be less than Sales date");
        }
    }
    public void ClearAll()
    {

        Transactionchecking();
        ViewState[SalesReturnItems] = null;
        gvSalesItems.DataSource = null;
        gvSalesItems.DataBind();
        ddlStore.Enabled = false;
        ddlBillType.Enabled = true;
        ddlCounter.SelectedValue = "0";
        ddlCounter.Enabled = true;
        btnSave.Enabled = true;
        // PanelHead.Enabled = true;
        divAdd.Style.Add("display", "block;");
        divView.Style.Add("display", "none;");
        rdSales.Enabled = true;
        rdItemSales.Enabled = true;
        ClearItems();
        ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "ClearAdd();", true);
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
        //  rptPager.DataSource = pages;
        // rptPager.DataBind();
    }
    private void ClearItems()
    {
        ViewState[DirectSR] = null;
        gvItems.DataSource = null;
        gvItems.DataBind();
        ViewState[SalesReturnItems] = null;
        gvSalesItems.DataSource = null;
        gvSalesItems.DataBind();

        try
        {
            ddlStore.SelectedValue = Session["mainstore"].ToString();
        }
        catch { }
        ddlStore.Enabled = false;

        ddlBillType.SelectedIndex = 0;
        ddlCounter.SelectedIndex = 0;
        txtTotalAmount.Text = "0";
        txtNetAmount.Text = "0";
        txtRoundOff.Text = "0";
        DisplayDiv();
    }
    private void DisplayDiv()
    {
        if (rdItemSales.Checked)
        {
            gvSalesItems.Visible = false;
            divItemSalesReturn.Style.Add("display", "block");
            liSales.Visible = false;
            gvSalesItems.DataSource = null;
            gvSalesItems.DataBind();
            ViewState[SalesReturnItems] = null;
            gvpi.Visible = false;
            imgbtnCustomer.Enabled = true;
        }
        else
        {
            divItemSalesReturn.Style.Add("display", "none");
            gvSalesItems.Visible = true;
            liSales.Visible = true;
            gvItems.DataSource = null;
            gvItems.DataBind();
            ViewState[DirectSR] = null;
            gvpi.Visible = true;
            imgbtnCustomer.Enabled = false;
        }
    }
    private DataTable GetDataTable(DataTable dtbl)
    {
        dtbl.Columns.Add("slno");
        dtbl.Columns.Add("Item");
        dtbl.Columns.Add("FK_Items");
        dtbl.Columns.Add("BatchNo");
        dtbl.Columns.Add("SalesRate");
        dtbl.Columns.Add("ExpiryDate");
        dtbl.Columns.Add("Quantity");
        dtbl.Columns.Add("TotalAmt", typeof(decimal));
        dtbl.Columns.Add("FK_Reason");
        dtbl.Columns.Add("MRP");
        dtbl.Columns.Add("SalesPrice");
        dtbl.Columns.Add("FK_Stock");
        dtbl.Columns.Add("SalesTaxAmount");
        dtbl.Columns.Add("ActualSalPrice");
        dtbl.Columns.Add("TaxAmount");
        return dtbl;
    }
    protected int AddDataItem()
    {
        DataTable dtbl = new DataTable();
        int inRowCount = 0;
        int inCheck = 0;
        if (ViewState[DirectSR] != null)
        {
            dtbl = (DataTable)ViewState[DirectSR];
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
            decimal SalesRate = 0;
            string BatchNo = "";
            decimal PurVAT = 0;
            decimal MRP = 0;
            decimal SalesPrice = 0;
            decimal ActualSalesPrice = 0;
            decimal CESS = 0;
            DateTime Expirydate = DateTime.Now;
            if (rdItemSales.Checked)
            {
                BlStock blStock = new BlStock();
                blStock.MasterId = Convert.ToInt64(hdfFKStock.Value);
                blStock.UserCode = hfUser.Value;
                blStock.FK_Client = Convert.ToInt64(hfClient.Value);
                blStock.SelectData();
                SalesRate = Convert.ToDecimal(txtSalesRateItem.Text);
                BatchNo = blStock.BatchNo;
                MRP = blStock.MRP;
                ActualSalesPrice = blStock.ActualSalPrice;
                Expirydate = blStock.ExpiryDate;
            }

            decimal decqty = Math.Round(Convert.ToDecimal(txtQuantity.Text == "" ? "0" : txtQuantity.Text), 3);
            decimal Total = (decqty * SalesRate) + ((PurVAT / 100) * (decqty * SalesRate)) + ((CESS / 100) * (decqty * SalesRate));

            DataRow drow = dtbl.NewRow();
            drow["slno"] = inRowCount + 1;
            drow["Item"] = hfItem.Value;
            drow["FK_Items"] = hdfFkItem.Value;
            drow["Quantity"] = decqty;
            drow["SalesRate"] = SalesRate;
            drow["BatchNo"] = BatchNo;
            drow["ExpiryDate"] = Expirydate.ToString("dd/MM/yyyy");
            drow["TotalAmt"] = Math.Round(Total, 2);
            drow["FK_Reason"] = "0";
            drow["SalesTaxAmount"] = (txttaxAmount.Text == "" ? "0" : txttaxAmount.Text);

            drow["MRP"] = MRP;
            drow["SalesPrice"] = SalesPrice;
            drow["FK_Stock"] = hdfFKStock.Value;
            drow["ActualSalPrice"] = ActualSalesPrice;
            drow["TaxAmount"] = SalesRate - ActualSalesPrice;

            dtbl.Rows.InsertAt(drow, inRowCount);
            gvItems.DataSource = dtbl;
            gvItems.DataBind();
            TotalAmount(dtbl);
            ViewState[DirectSR] = dtbl;
            FillReasonddl(false);

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
                if (txtSalesRefno.Text != "")
                {
                    ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "CalculateTotalAmount()", true);
                }
                else
                {
                    txtTotalAmount.Text = Math.Round(decimal.Parse((dtbl.Compute("Sum(TotalAmt)", "")).ToString()), 2).ToString();
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
    protected void ClearTotal()
    {
        txtTotalAmount.Text = "0";
        txtNetAmount.Text = "0";
    }
    protected void FillReasonddl(Boolean rdSales)
    {
        if (rdSales)
        {
            DataTable dtbl = (DataTable)ViewState[SalesReturnItems];
            for (int i = 0; i < gvSalesItems.Rows.Count; i++)
            {

                Populate populate = new Populate();
                populate.FillDropdownList(((DropDownList)gvSalesItems.Rows[i].FindControl("ddlReason")), "Reason", "Reason", "ID_Reason", "ID_Reason", "ReasonMode=1 AND FK_Client = " + hfClient.Value, "");
                ((DropDownList)gvSalesItems.Rows[i].FindControl("ddlReason")).SelectedValue = dtbl.Rows[i]["FK_Reason"].ToString();
            }


        }
        else
        {

            DataTable dtbl = (DataTable)ViewState[DirectSR];
            for (int i = 0; i < gvItems.Rows.Count; i++)
            {

                Populate populate = new Populate();
                populate.FillDropdownList(((DropDownList)gvItems.Rows[i].FindControl("ddlReason")), "Reason", "Reason", "ID_Reason", "ID_Reason", "ReasonMode=1 AND  FK_Client = " + hfClient.Value, "");
                string mm = dtbl.Rows[i]["FK_Reason"].ToString();
                ((DropDownList)gvItems.Rows[i].FindControl("ddlReason")).SelectedValue = dtbl.Rows[i]["FK_Reason"].ToString();
            }


        }
    }
    protected void Customer()
    {
        try
        {

            ((TextBox)CommonPopup1.FindControl("txtName")).Text = txtCustomer.Text.Trim();
            ((TextBox)CommonPopup1.FindControl("txtRefno")).Text = txtCustRefno.Text.Trim();
            ((HiddenField)CommonPopup1.FindControl("hfColumn")).Value = "CustomerName";
            CustomerFill(1);
            divAdd.Style.Add("display", "block");
            divView.Style.Add("display", "none");
        }
        catch (Exception ex)
        {
        }
    }
    public void BillNoFill(int pageIndex)
    {
        //ClearSales();

        Populate Populate = new Populate();
        CommonPopup1.CallingFunction = "BillNoFill";
        string strName = ((TextBox)CommonPopup1.FindControl("txtName")).Text;
        string Refno = ((TextBox)CommonPopup1.FindControl("txtRefno")).Text;
        Refno = Refno == string.Empty || Refno == null ? "0" : Refno;
        string searchCol = ((HiddenField)CommonPopup1.FindControl("hfColumn")).Value;
        searchCol = searchCol == string.Empty ? "[Name]" : "[" + searchCol + "]";


        // Populate.FillPopUpGrid((GridView)CommonPopup1.FindControl("gdvSearch"), "View_Sales inner join BillType on ID_BillType=FK_BillType", "ID_Sales as ID,'[' + BillNo  +'][' + CONVERT(VARCHAR(14),BillDate,103) +']' as Name", "ID_Sales", "ID_Sales", "(Quantity>0 or Free>0) and View_Sales.FK_Client=" + hfClient.Value.Split(',')[0] + "  and FK_Store=" + ddlStore.SelectedValue + " and FK_BillType=" + ddlBillType.SelectedValue + " AND (ID_Sales =" + Refno + " OR 0=" + Refno + ") AND Cancelled=0 and BillDate ", strName, null);
        // Populate.FillPopUpGrid((GridView)CommonPopup1.FindControl("gdvSearch"), "Sales",  "ID_Sales as ID,BillNo AS Name,(Select Top 1 TypeName from BillType Where ID_BillType=FK_BillType) as [Bill Type],CustomerName As Customer ,dbo.DateFormats(BillDate) As [Bill Date],BillTotal as [Bill Total],credit as Credit", "ID_Sales", "ID_Sales","FK_Client=" + hfClient.Value.Split(',')[0] +" AND FK_Store= (CASE WHEN " + ddlStore.SelectedValue + "=0 THEN FK_Store ELSE  " + ddlStore.SelectedValue + " END) AND FK_BillType= (CASE WHEN " + ddlBillType.SelectedValue + "=0 THEN FK_BillType ELSE  " + ddlBillType.SelectedValue + " END) AND  (ID_Sales =" + Refno + " OR 0=" + Refno + ")    AND ((ISNULL((Select SUM(Quantity)  From Sales as S Join SalesDetails On FK_Sales=ID_Sales   where ID_Sales=Sales.ID_Sales ),0)))-((ISNULL((SELECT      SUM(SRD.Quantity)  FROM            SRDetails AS SRD INNER JOIN   SalesReturn AS SR ON SRD.FK_SalesReturn = SR.ID_SalesReturn INNER JOIN  Sales AS S ON SR.Fk_Sale = S.ID_Sales WHere S.ID_Sales=Sales.ID_Sales AND SR.Cancelled=0),0)) )>0 ",strName, null);
        //Populate.FillGrid((GridView)CommonPopup1.FindControl("gdvSearch"), "Sales", , "ID_Sales", "", "FK_Client=" + hfClient.Value.Split(',')[0] +" AND FK_Store= (CASE WHEN " + ddlStore.SelectedValue + "=0 THEN FK_Store ELSE  " + ddlStore.SelectedValue + " END) AND FK_BillType= (CASE WHEN " + ddlBillType.SelectedValue + "=0 THEN FK_BillType ELSE  " + ddlBillType.SelectedValue + " END) AND  (ID_Sales =" + Refno + " OR 0=" + Refno + ")    AND ((ISNULL((Select SUM(Quantity)  From Sales as S Join SalesDetails On FK_Sales=ID_Sales   where ID_Sales=Sales.ID_Sales ),0)))-((ISNULL((SELECT      SUM(SRD.Quantity)  FROM            SRDetails AS SRD INNER JOIN   SalesReturn AS SR ON SRD.FK_SalesReturn = SR.ID_SalesReturn INNER JOIN  Sales AS S ON SR.Fk_Sale = S.ID_Sales WHere S.ID_Sales=Sales.ID_Sales AND SR.Cancelled=0),0)) )>0 ", searchCol + "  LIKE '" + strName + "%'", pageIndex, 10, "", (Repeater)CommonPopup1.FindControl("rptPager"), this.Page);


        Populate.FillSalesReturnGrid((GridView)CommonPopup1.FindControl("gdvSearch"), "Sales join SalesDetails SD  on sd.FK_Sales=ID_Sales", "ID_Sales as ID,BillNo AS Name,(Select Top 1 TypeName from BillType Where ID_BillType=FK_BillType) as [Bill Type],CustomerName As Customer ,dbo.DateFormats(BillDate) As [Bill Date],CAST(Sales.NetAmount AS DECIMAL(18,3)) as [Net Amount],credit as Credit", "ID_Sales", "", "FK_Client=" + hfClient.Value.Split(',')[0] + " AND FK_Store= " + ddlStore.SelectedValue + " AND FK_BillType= " + ddlBillType.SelectedValue + " AND  (ID_Sales =" + Refno + " OR 0=" + Refno + ") AND ISNULL((SELECT SUM(SD.Quantity) - ISNULL(SUM(SRD.Quantity),0) FROM SalesDetails SD LEFT JOIN SalesReturn SR ON SR.FK_Sale = SD.FK_Sales AND SR.Cancelled = 0 LEFT JOIN SRDetails SRD ON SRD.FK_SalesReturn = SR.ID_SalesReturn AND SRD.FK_Stock = SD.FK_Stock AND SRD.FK_Items = SD.FK_Items  /*WHERE SD.FK_Sales = ID_Sales*/),0) >0 ", searchCol + "  LIKE '" + strName + "%'", pageIndex, 10, "ID_Sales,BillNo,FK_BillType,CustomerName,BillDate,BillTotal,NetAmount,Credit ", (Repeater)CommonPopup1.FindControl("rptPager"), this.Page, 0);

        Populate = null;

        txtSalesRefno.Focus();

        string a = txtSalesRefno.Text;
        if (txtSalesRefno.Text == string.Empty)
        {
            lblPopupHeader.Text = "Sales";
            mpCommon.Show();
        }


    }
    public void CustomerFill(int pageIndex)
    {
        ClearCustomer();

        Populate Populate = new Populate();
        CommonPopup1.CallingFunction = "CustomerFill";
        string strName = ((TextBox)CommonPopup1.FindControl("txtName")).Text;
        string Refno = ((TextBox)CommonPopup1.FindControl("txtRefno")).Text;
        string searchCol = ((HiddenField)CommonPopup1.FindControl("hfColumn")).Value;
        Refno = Refno == string.Empty || Refno == null ? "0" : Refno;

        Populate.FillGrid((GridView)CommonPopup1.FindControl("gdvSearch"), "Customer", "ID_Customer as ID, CustomerName as Name", "ID_Customer", "", "FK_Client=" + hfClient.Value.Split(',')[0] + "   AND Active=1 ", searchCol + "  LIKE '" + strName + "%'", pageIndex, 10, "", (Repeater)CommonPopup1.FindControl("rptPager"), this.Page);

        // Populate.FillPopUpGrid((GridView)CommonPopup1.FindControl("gdvSearch"), "Customer", "ID_Customer as ID,CustomerName as Name", "ID_Customer", "ID_Customer", "FK_Client=" + hfClient.Value.Split(',')[0] + " AND (ID_Customer =" + Refno + " OR 0=" + Refno + ")" + " AND  Active=1  AND Cancelled=0 and CustomerName ", strName, null);

        Populate = null;
        txtCustRefno.Focus();
        if (txtCustomer.Text == string.Empty)
        {
            lblPopupHeader.Text = "Customer";
            mpCommon.Show();
        }

    }
    private void ClearSales()
    {
        txtSalesBillNo.Text = "";
        txtSalesRefno.Text = "";
        if (rdItemSales.Checked)
            ddlBillType.SelectedIndex = 0;
    }
    private void ClearCustomer()
    {
        txtCustomer.Text = "";
        txtCustRefno.Text = "";
    }

    public void SaleDetail(string strName, string strRefno, bool status)
    {

        txtSalesRefno.Text = strRefno;
        txtSalesBillNo.Text = strName;
        cbxIsCredit.Checked = status;
        HiddenbtnClick();
    }
    private void HiddenbtnClick()
    {

        if (txtSalesRefno.Text != "" && txtSalesBillNo.Text != "")
        {
            BindSalesItems(Convert.ToInt64(txtSalesRefno.Text));

        }
        else
        {
            gvSalesItems.DataSource = null;
            gvSalesItems.DataBind();
            ViewState[SalesReturnItems] = null;
        }

    }

    public void SetDetails(string strName, string strRefno)
    {
        if (CommonPopup1.CallingFunction == "CustomerFill")
        {
            txtCustRefno.Text = strRefno;
            txtCustomer.Text = strName;
        }
        if (CommonPopup1.CallingFunction == "BillNoFill")
        {
            txtSalesRefno.Text = strRefno;
            txtSalesBillNo.Text = strName;
            BindSalesItems(Convert.ToInt64(txtSalesRefno.Text));
        }

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
            BlSaleReturn blSaleReturn = new BlSaleReturn();
            blSaleReturn.MasterId = Convert.ToInt64(hfMasterID.Value);
            blSaleReturn.UserCode = hfuserName.Value;
            blSaleReturn.FK_Client = Convert.ToInt64(hfClient.Value);
            blSaleReturn.FK_Reason = Convert.ToInt64(Reason);
            if (hfIsDelete.Value.ToLower() == "true")
            {
                status = blSaleReturn.DeleteSalesReturn();
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
            blSaleReturn = null;
        }
    }
    #endregion
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
                ddlStore.SelectedValue = Session["mainstore"].ToString();
                ddlStore.Enabled = false;
                rdSales.Checked = true;
                ddlBillType.Focus();
                DisplayDiv();
                GridFill(1);
                txttaxAmount.Enabled = false;
                txtSalesRateItem.Enabled = false;
                txtTotalAmountItem.Enabled = false;
                hdfMode.Value = "SAR";
                if (Convert.ToBoolean(hfIsAdd.Value) == false)
                {
                    ((Label)CommonPopupPrivilege.FindControl("popuplabel")).Text = "User does not have permission to Add";
                    mpPrivilege.Show();
                }
                if (Convert.ToBoolean(hfIsUpdate.Value) == false)
                {
                    gvSalesReturn.Columns[5].Visible = false;
                }
                if (Convert.ToBoolean(hfIsDelete.Value) == false)
                {
                    gvSalesReturn.Columns[6].Visible = false;
                }
                Transactionchecking();
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

            divAdd.Style.Add("display", "none");
            divView.Style.Add("display", "block");

            ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "ClearAdd();ClearSearch();", true);
            GridFill(1);
            ddlSearchCustomer.Focus();
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
            if (!string.IsNullOrEmpty(hfSalesReturn.Value))
            {
                ScriptManager.RegisterStartupScript(Page, typeof(Page), "EditSubmitBlocked", "toastr.error('Submit is blocked. This sales return is opened for edit.');", true);
                return;
            }

            if (!Page.IsValid)
            {
                return;
            }

            System.Threading.Thread.Sleep(10);

            bool Status = false;





            if (rdSales.Checked && gvSalesItems.Rows.Count > 0)
            {
                // Ensure at least one row carries a non-zero return quantity
                foreach (GridViewRow gr in gvSalesItems.Rows)
                {
                    if (gr.RowType == DataControlRowType.DataRow)
                    {
                        decimal qty;
                        string rawQty = ((TextBox)gr.FindControl("txtQuantity")).Text;
                        if (decimal.TryParse(rawQty, out qty) && qty > 0)
                        {
                            Status = true;
                            break;
                        }
                    }
                }
            }
            else if (rdItemSales.Checked && gvItems.Rows.Count > 0)
            {
                Status = true;
            }
            if (Status)
            {
                if (cbxIsCredit.Checked == true)
                {
                    if (txtCustomer.Text != "")
                    {
                        if (Convert.ToDateTime(txtDate.Text) <= DateTime.Now)
                            Save();
                        else
                            ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "alertify.alert('Date must be less than or equal to current date')", true);
                    }
                    else
                        ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "alertify.alert('Customer required')", true);
                }
                else
                {
                    if (Convert.ToDateTime(txtDate.Text) <= DateTime.Now)
                        Save();
                    else
                        ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "alertify.alert('Date must be less than or equal to current date')", true);

                }
            }

            else
            {
                string msg = (rdSales.Checked && gvSalesItems.Rows.Count > 0)
                    ? "Quantity must be greater than zero for at least one item."
                    : "At least one item is required.";

                ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script",
                    "alertify.alert('" + msg + "')", true);
            }

        }
        catch (Exception ex)
        {
            Populate.WarningMsg(this, ex.ToString());
        }
    }
    protected void gvSalesItems_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {

        try
        {
            //
            string confirmValue = Request.Form["confirm_value"];
            if (confirmValue.Length > 3)
                confirmValue = confirmValue.Substring(confirmValue.Length - 3, 3);
            if (confirmValue == "Yes")
            {
                if (ViewState[SalesReturnItems] != null)
                {
                    DataTable dtbl = (DataTable)ViewState[SalesReturnItems];
                    if (dtbl.Columns["FK_Reason"] == null)
                        dtbl.Columns.Add("FK_Reason");

                    for (int i = 0; i < dtbl.Rows.Count; i++)
                    {
                        dtbl.Rows[i]["FK_Reason"] = ((DropDownList)gvSalesItems.Rows[i].FindControl("ddlReason")).SelectedValue;
                        dtbl.Rows[i]["Quantity"] = ((TextBox)gvSalesItems.Rows[i].FindControl("txtQuantity")).Text;
                        // dtbl.Rows[i]["TotalAmt"] = Convert.ToDecimal(((Label)gvSalesItems.Rows[i].FindControl("lblTotalAmt")).Text);

                    }
                    dtbl.Rows.RemoveAt(e.RowIndex);
                    for (int i = 0; i < dtbl.Rows.Count; i++)
                    {
                        dtbl.Rows[i]["slno"] = i + 1;
                    }

                    TotalAmount(dtbl);
                    gvSalesItems.DataSource = dtbl;
                    gvSalesItems.DataBind();
                    ViewState[SalesReturnItems] = dtbl;
                    FillReasonddl(true);
                }
            }
        }
        catch (Exception ex)
        {
            Populate.WarningMsg(this, ex.ToString());
        }

    }

    protected void gvSalesReturn_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {

            BlSaleReturn blSales = new BlSaleReturn();
            long status = 0;
            string Sts = "closed";
            if (e.CommandName == "Print")
            {

                GridViewRow row = (GridViewRow)((ImageButton)e.CommandSource).NamingContainer;
                int index = row.RowIndex;
                string SalesReturnId = gvSalesReturn.DataKeys[index]["ID_SalesReturn"].ToString();
                string popupreport = "<script> window.open('SalesReturnPrint.aspx?SalesReturnId=" + SalesReturnId + "', 'self', 'left=200,top=50,width=900,height=680,status=0,resizable=0,fullscreen=0,scrollbars=1');</script>";
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "Success", popupreport, false);
            }

            else if (e.CommandName == "Select")
            {
                if (Convert.ToBoolean(hfIsUpdate.Value) == false)
                {
                    ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "Script", "toastr.warning('User does not have permission to Update')", true);
                }
            }
            else if (e.CommandName == "Delete")
            {
                if (Convert.ToBoolean(hfIsDelete.Value) == false)
                {
                    ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "Script", "toastr.warning('User does not have permission to Delete')", true);
                }
            }

            blSales = null;

        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "toastr.warning('" + ex.Message + "')", true);
        }
    }


    protected void Page_Changed(object sender, EventArgs e)
    {
        int pageIndex = int.Parse((sender as LinkButton).CommandArgument);
        DataTable dtbl = new DataTable();
        BlSaleReturn BlSaleReturn = new BlSaleReturn();
        BlSaleReturn.PageSize = 10;
        BlSaleReturn.PageIndex = Convert.ToInt16(pageIndex);
        GridFill(BlSaleReturn.PageIndex);
        BlSaleReturn = null;
    }
    protected void gvSalesReturn_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            string confirmValue = Request.Form["confirm_value"];
            if (confirmValue.Length > 3)
                confirmValue = confirmValue.Substring(confirmValue.Length - 3, 3);
            if (confirmValue == "Yes")
            {
                hfMasterID.Value = gvSalesReturn.DataKeys[e.RowIndex]["ID_SalesReturn"].ToString();
            }
            else
            {
                mpDelete.Hide();
            }

        }
        catch (Exception ex)
        {
            Populate.WarningMsg(this, ex.ToString());
        }


    }
    protected void gvSalesReturn_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        try
        {
            BlSaleReturn BlSaleReturn = new BlSaleReturn();
            ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "ShowAddDiv();", true);
            long MasterId = Convert.ToInt64(gvSalesReturn.DataKeys[e.NewSelectedIndex]["ID_SalesReturn"].ToString());
            BlSaleReturn.MasterId = MasterId;
            hfSalesReturn.Value = MasterId.ToString();
            BlSaleReturn.FK_Customer = 0;
            BlSaleReturn.FK_Store = 0;
            BlSaleReturn.FromDate = DateTime.Parse("1/1/1900");
            BlSaleReturn.FK_Client = Convert.ToInt32(hfClient.Value.Split(',')[0]);
            BlSaleReturn.ToDate = DateTime.MaxValue;
            BlSaleReturn.SelectData();
            txtDate.Text = BlSaleReturn.BillDate.ToString("dd/MM/yyyy");
            try
            {
                txtCustRefno.Text = BlSaleReturn.FK_Customer.ToString();
                txtCustomer.Text = BlSaleReturn.CustomerName.ToString();
                //Customer();
            }
            catch (Exception ex)
            {

            }
            try
            {
                ddlStore.SelectedValue = BlSaleReturn.FK_Store.ToString();
            }
            catch (Exception ex)
            {
                ddlStore.SelectedValue = "0";
            }
            try
            {
                ddlBillType.SelectedValue = BlSaleReturn.FK_BillType.ToString();
            }
            catch (Exception ex)
            {
                ddlBillType.SelectedValue = "0";
            }
            try
            {
                ddlCounter.SelectedValue = BlSaleReturn.FK_Counter.ToString();
            }
            catch (Exception ex)
            {
                ddlCounter.SelectedValue = "0";
            }

            ddlStore.Enabled = false;
            btnSave.Enabled = true;
            ScriptManager.RegisterStartupScript(Page, typeof(Page), "EditSubmitBlocked", "toastr.error('Submit is blocked. This sales return is opened for edit.');", true);
            cbxIsCredit.Checked = BlSaleReturn.Credit;
            txtRemark.Text = BlSaleReturn.Remarks.ToString();
            hfBillNo.Value = BlSaleReturn.BillNo.ToString();

            divAdd.Style.Add("display", "block");
            divView.Style.Add("display", "none;");

            // PanelHead.Enabled = false;

            rdItemSales.Enabled = false;
            rdSales.Enabled = false;

            if (BlSaleReturn.Fk_Sale != 0)
            {
                txtSalesRefno.Text = BlSaleReturn.Fk_Sale.ToString();
                txtSalesBillNo.Text = BlSaleReturn.Fk_Sale.ToString();
                ddlBillType.Enabled = false;
                rdSales.Checked = true;
                rdItemSales.Checked = false;
                BindSalesItems(BlSaleReturn.Fk_Sale);
                FillReasonddl(true);
            }
            else
            {
                BlSaleReturn.FK_Client = Convert.ToInt32(hfClient.Value.Split(',')[0]);
                DataTable dtbl = BlSaleReturn.DirectSaleReturnItems();
                ViewState[DirectSR] = dtbl;
                gvItems.DataSource = dtbl;
                gvItems.DataBind();
                rdItemSales.Checked = true;
                rdSales.Checked = false;
                FillReasonddl(false);
                TotalAmount(dtbl);
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
    protected void btnInsertItems_Click(object sender, EventArgs e)
    {
        try
        {


            if (!Page.IsValid)
            {
                return;
            }
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
    protected void rdItemSales_CheckedChanged(object sender, EventArgs e)
    {
        try
        {
            //
            ClearSales();
            ClearAll();
            DisplayDiv();
            if (ViewState[DirectSR] != null)
            {
                DataTable dtb = (DataTable)ViewState[DirectSR];
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
    protected void rdSales_CheckedChanged(object sender, EventArgs e)
    {
        try
        {
            ClearAll();
            DisplayDiv();
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
            string confirmValue = Request.Form["confirm_value"];
            if (confirmValue.Length > 3)
                confirmValue = confirmValue.Substring(confirmValue.Length - 3, 3);
            if (confirmValue == "Yes")
            {
                if (ViewState[DirectSR] != null)
                {
                    DataTable dtbl = (DataTable)ViewState[DirectSR];

                    if (dtbl.Columns["FK_Reason"] == null)
                        dtbl.Columns.Add("FK_Reason");
                    for (int i = 0; i < dtbl.Rows.Count; i++)
                    {
                        dtbl.Rows[i]["FK_Reason"] = ((DropDownList)gvItems.Rows[i].FindControl("ddlReason")).SelectedValue;
                    }
                    hfItem.Value = dtbl.Rows[e.RowIndex]["Item"].ToString();
                    txtitem.Text = dtbl.Rows[e.RowIndex]["Item"].ToString();
                    hdfFKStock.Value = dtbl.Rows[e.RowIndex]["FK_Stock"].ToString();

                    //txtFree.Text = dtbl.Rows[e.RowIndex]["Free"].ToString();
                    txtQuantity.Text = dtbl.Rows[e.RowIndex]["Quantity"].ToString();
                    BlStock blStock = new BlStock();
                    blStock.MasterId = Convert.ToInt64(hdfFKStock.Value);
                    blStock.UserCode = hfUser.Value;
                    blStock.FK_Client = Convert.ToInt64(hfClient.Value);
                    blStock.SelectData();
                    //txtCurrentStock.Text = blStock.CurrentStock.ToString();
                    dtbl.Rows.RemoveAt(e.RowIndex);
                    for (int i = 0; i < dtbl.Rows.Count; i++)
                    {
                        dtbl.Rows[i]["slno"] = i + 1;
                    }
                    gvItems.DataSource = dtbl;
                    gvItems.DataBind();
                    TotalAmount(dtbl);
                    ViewState[DirectSR] = dtbl;
                    FillReasonddl(false);
                }
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "toastr.warning('" + ex.Message + "')", true);
        }

    }
    protected void gvSalesItems_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            //
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Populate populate = new Populate();
                populate.FillDropdownList(((DropDownList)e.Row.FindControl("ddlReason")), "Reason", "Reason", "ID_Reason", "ID_Reason", "ReasonMode=1 AND FK_Client = " + hfClient.Value, "");
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "toastr.warning('" + ex.Message + "')", true);
        }
    }
    protected void imgbtnSalesBill_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            // ClearSales();
            txtSalesRefno.Text = "";
            ((TextBox)CommonPopup1.FindControl("txtName")).Text = txtSalesBillNo.Text.Trim();
            ((TextBox)CommonPopup1.FindControl("txtRefno")).Text = txtSalesRefno.Text.Trim();
            ((HiddenField)CommonPopup1.FindControl("hfColumn")).Value = "BillNo";
            BillNoFill(1);
            divAdd.Style.Add("display", "block");
            divView.Style.Add("display", "none");

        }
        catch (Exception ex)
        {
        }
    }
    protected void imgbtnCustomer_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            Customer();
        }
        catch (Exception ex)
        {
        }
    }
    protected void btnItem_Click(object sender, EventArgs e)
    {
        try
        {

            if (rdItemSales.Checked)
            {

                BlStock blStock = new BlStock();
                blStock.MasterId = Convert.ToInt64(hdfFKStock.Value);
                blStock.UserCode = hfUser.Value;
                blStock.FK_Client = Convert.ToInt64(hfClient.Value);
                blStock.SelectData();

                txtSalesRateItem.Text = blStock.SalePrice.ToString();
                txttaxAmount.Text = blStock.SalesTaxAmount.ToString();
                hfItemId.Value = blStock.FK_Items.ToString();
                hfActualSalPrice.Value = blStock.ActualSalPrice.ToString();
                txtTotalAmountItem.Text = "0";
                //txtCurrentStock.Text = blStock.CurrentStock.ToString();
                blStock = null;
                txtQuantity.Text = "0";
                txtQuantity.Focus();

            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "toastr.warning('" + ex.Message + "')", true);
        }
    }

    protected void btnSTax_Click(object sender, EventArgs e)
    {
        BlSaleReturn blSales = new BlSaleReturn();
        DataTable dt;

        Populate Populate = new Populate();
        CommonPopupGrid1.CallingFunction = "Sales";
        string Refno = ((TextBox)CommonPopupGrid1.FindControl("txtRefno")).Text;
        string searchCol = ((HiddenField)CommonPopupGrid1.FindControl("hfColumn")).Value;
        Refno = Refno == string.Empty || Refno == null ? "0" : Refno;
        GridView gvNew = (GridView)CommonPopupGrid1.FindControl("gdvSearch");
        BlStock blStock = new BlStock();
        blSales.FK_ItemId = Convert.ToInt64(hfItemId.Value == "" ? "0" : hfItemId.Value);
        blSales.FK_Stock = Convert.ToInt64(hdfFKStock.Value == "" ? "0" : hdfFKStock.Value);
        dt = blSales.SelectTaxGriddata();
        //dt.Columns.Add("TaxAmount", typeof(decimal));
        gvNew.DataSource = dt;
        gvNew.DataBind();
        gvNew.Columns[3].Visible = true;
        lblPopupH.Text = "Sales Tax";
        mpGrid.Show();

    }


    protected void gvSalesReturn_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvSalesReturn.PageIndex = e.NewPageIndex;
        GridFill(e.NewPageIndex);
    }
    protected void ImageBtnDelete_Click(object sender, ImageClickEventArgs e)
    {
        Delete();
    }
    protected void cvCustomerCredit_ServerValidate(object source, ServerValidateEventArgs args)
    {
        args.IsValid = !(cbxIsCredit.Checked && string.IsNullOrWhiteSpace(txtCustomer.Text));
    }
    #endregion
}