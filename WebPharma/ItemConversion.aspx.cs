/*----------------------------------------------------------------------------------------------------------------------------------------------------------
Purpose    : Insert/Update/Delete Item Conversion 
Author     : BIBIN tc
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

public partial class ItemConversion : System.Web.UI.Page
{
    #region Functions
    protected void BindData()
    {
        Populate populate = new Populate();
        populate.FillDropdownList(ddlToStore, "Store", "StoName", "ID_Store", "ID_Store", "     Active='True'   and FK_Client = " + hfClient.Value, "");
        populate.FillDropdownList(ddlFromStore, "Store", "StoName", "ID_Store", "ID_Store", "     Active='True'   and FK_Client = " + hfClient.Value, "");
        populate.FillDropdownList(ddlToStoreSearch, "Store", "StoName", "ID_Store", "ID_Store", "     Active='True'   and FK_Client = " + hfClient.Value, "");
        populate.FillDropdownList(ddlFromStoreSearch, "Store", "StoName", "ID_Store", "ID_Store", "     Active='True'   and FK_Client = " + hfClient.Value, "");
        populate = null;

    }
    protected void CheckUserAccess()
    {
        BlUserAccess bluseraccess = new BlUserAccess();
        Populate populate = new Populate();
        bluseraccess.FK_Users = Convert.ToInt64(hfUser.Value);
        bluseraccess.FK_Pages = 70;
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
        BlltemConversion blltemConversion = new BlltemConversion();
        DataTable dtbl = new DataTable();
        blltemConversion.MasterId = 0;
        blltemConversion.FK_StoreFrom = Convert.ToInt64(ddlFromStoreSearch.SelectedValue);
        blltemConversion.FK_StoreTo = Convert.ToInt64(ddlToStoreSearch.SelectedValue);
        blltemConversion.FromDate = DateTime.ParseExact(txtFromDate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
        blltemConversion.ToDate = DateTime.ParseExact(txtToDate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
        blltemConversion.FK_Client = Convert.ToInt64(hfClient.Value);
        currentPage = currentPage == 0 ? 1 : currentPage;
        blltemConversion.PageIndex = Convert.ToInt16(currentPage);
        dtbl = blltemConversion.SelectAllData();
        gvItemConversion.DataSource = dtbl;
        gvItemConversion.DataBind();
        if (dtbl.Rows.Count > 0)
        {
            int recordCount = Convert.ToInt32(dtbl.Rows[0]["RecordCount"].ToString());
            this.PopulatePager(recordCount, currentPage);
        }
        blltemConversion = null;

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
    public void ClearAll()
    {
        divproductioncost.Visible = false;
        chkproduction.Checked = false;
        ViewState["ItemsFrom"] = null;
        ViewState["ItemsTo"] = null;
        gvItems.DataSource = null;
        gvItemsConv.DataSource = null;
        gvItems.DataBind();
        gvItemsConv.DataBind();
        cbxTransfer.Checked = false;
        ddlFromStore.Enabled = true;
        PanelAdd.Enabled = true;
        btnSave.Visible = true;
        txtFromAmount.Text = "";
        txtToAmount.Text = "";
        txtQuantityConv.Enabled = true;
        ddlSlno.Items.Clear();
        DivPreviousPurchase.Visible = false;
        chkproduction.Checked = false;
        chkproduction.Enabled = true;
        divAdd.Style.Add("display", "block;");
        divView.Style.Add("display", "none;");
        lblCurrentStock.Text = "Current Stock= 0";
        ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "ClearAdd();", true);
    }
    protected void SetSlno()
    {
        ddlSlno.Items.Clear();
        DataTable dtbl = new DataTable();
        DataTable dtblConv = new DataTable();
        if (ViewState["ItemsTo"] != null)
        {
            dtblConv = (DataTable)ViewState["ItemsTo"];
        }
        else
        {
            dtblConv = GetDataTable(dtbl);
        }
        if (ViewState["ItemsFrom"] != null)
        {
            dtbl = (DataTable)ViewState["ItemsFrom"];
        }
        else
        {
            dtbl = GetDataTable(dtbl);
        }
        if (dtbl.Rows.Count > 0)
        {
            //if (chkproduction.Checked == false)
            //{
                for (int i = 1; i <= dtbl.Rows.Count; i++)
                {
                    // string slno = i.ToString();
                    string slno = dtbl.Rows[i - 1]["slno"].ToString();

                    try
                    {
                        ddlSlno.Items.Insert(i - 1, new ListItem(slno, slno));
                    }
                    catch (Exception ex)
                    {

                    }

                }
            //}
        }
        string extraField = "Select";
        ddlSlno.Items.Insert(0, new ListItem(extraField, "0"));

    }
    protected void gvItemsConv_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (chkproduction.Checked == true)
            {
                e.Row.Cells[12].Visible = true;
              
            }
            else
            {
                e.Row.Cells[12].Visible = false;
            }

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lbl = (Label)e.Row.FindControl("lblgvSLNO");
                lbl.Text = Convert.ToString(e.Row.RowIndex + 1);

            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "toastr.warning('" + ex.Message + "')", true);
        }
    }
    private DataTable GetDataTable(DataTable dtbl)
    {
        dtbl.Columns.Add("slno");
        dtbl.Columns.Add("Item");
        dtbl.Columns.Add("Quantity");
        dtbl.Columns.Add("FK_Items");
        dtbl.Columns.Add("CardPrice");
        dtbl.Columns.Add("WholeSalePrice");
        dtbl.Columns.Add("RetailPrice");
        dtbl.Columns.Add("MRP");
        dtbl.Columns.Add("BatchNo");
        dtbl.Columns.Add("SlNoFrom");
        dtbl.Columns.Add("FK_Stock");
        dtbl.Columns.Add("Expdate");
        dtbl.Columns.Add("PurRate");
        dtbl.Columns.Add("VAT");
        dtbl.Columns.Add("BarCode");
        dtbl.Columns.Add("Total", typeof(decimal));
        dtbl.Columns.Add("CGST");
        dtbl.Columns.Add("SGST");
        dtbl.Columns.Add("IGST");
       
            dtbl.Columns.Add("Productcost");
        
        return dtbl;

    }
    protected int AddDataItem(string ItemType, Int64 FK_Stock)
    {

        //if fk stock 0  select with fk items else  blstock.SelectData();


        //drow["FK_Items"] = blstock.FK_Items.ToString();
        //drow["BatchNo"] = blstock.BatchNo;
        //drow["FK_Stock"] = FK_Stock;

        //drow["Expdate"] = blstock.ExpiryDate;
        //drow["PurRate"] = blstock.PurRate;
        //drow["VAT"] = blstock.VAT;
        //drow["BarCode"] = blstock.BarCode;




        DataTable dtbl = new DataTable();
        int inRowCount = 0;
        int inCheck = 0;
        string VAT = "";
        if (ViewState[ItemType] != null)
        {
            dtbl = (DataTable)ViewState[ItemType];
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
                if (dtbl.Rows[i]["FK_Stock"].ToString() == FK_Stock.ToString() && dtbl.Rows[i]["FK_Stock"].ToString() != "0")
                {
                    inCheck = inCheck + 1;
                    break;
                }
                if (ItemType == "ItemsTo")
                {
                    if (dtbl.Rows[i]["SlNoFrom"].ToString() == ddlSlno.SelectedValue)
                    {
                        inCheck = inCheck + 1;
                        break;
                    }

                }
            }


        }
        if(chkproduction.Checked==true || inCheck==0)
        {

        //if (inCheck == 0)
        //{
            BlStock blstock = new BlStock();
            DataRow drow = dtbl.NewRow();
            if (inRowCount > 0)
            {
                drow["slno"] = (Convert.ToInt64(dtbl.Rows[inRowCount - 1]["slno"].ToString()) + 1);
            }
            else
                drow["slno"] = 1;
            if (FK_Stock != 0)
            {
                blstock.MasterId = Convert.ToInt64(FK_Stock);
                blstock.FK_Store = Convert.ToInt64(ddlFromStore.SelectedValue);
                blstock.SelectData();
                drow["FK_Items"] = blstock.FK_Items.ToString();
                drow["BatchNo"] = blstock.BatchNo;
                drow["FK_Stock"] = FK_Stock;

                drow["Expdate"] = blstock.ExpiryDate;
                drow["PurRate"] = blstock.PurRate;
                //drow["VAT"] = blstock.VAT;
                //drow["BarCode"] = blstock.BarCode;
                //drow["CGST"] = blstock.CGST;
              
                //drow["SGST"] = blstock.SGST;

                //drow["IGST"] = blstock.IGST;


            }
            else
            {
                int Slno = Convert.ToInt32(ddlSlno.SelectedValue);
                int rn = 0;

                DataTable dtblFrom = (DataTable)ViewState["ItemsFrom"];
                //find slno row
                for (int i = 0; i < dtblFrom.Rows.Count; i++)
                {
                    if (dtblFrom.Rows[i]["slno"].ToString() == Slno.ToString())
                    {
                        rn = i;
                        break;
                    }
                }
                VAT = dtblFrom.Rows[rn]["VAT"].ToString();

                drow["FK_Items"] = hfFK_Items.Value;
                drow["BatchNo"] = txtBatchNo.Text;
                drow["FK_Stock"] = 0;

                drow["Expdate"] = DateTime.Now;
                drow["PurRate"] = txtPurChaseRate.Text;
                drow["VAT"] = VAT;
                drow["BarCode"] = txtBarCode.Text;
            }

            if (ItemType == "ItemsFrom")
            {
                drow["Item"] = hf_Item.Value;
                drow["Quantity"] = txtQuantity.Text;
                //drow["CardPrice"] = blstock.PrivilagePrice;
                //drow["WholeSalePrice"] = blstock.WholeSalePrice;
                //drow["RetailPrice"] = blstock.RetailPrice;
                drow["PurRate"] = blstock.PurRate;
                drow["MRP"] = blstock.MRP;
                drow["SlNoFrom"] = "0";
               // drow["Total"] = Math.Round(blstock.RetailPrice * Convert.ToDecimal(txtQuantity.Text), 2);
                //drow["CGST"] = blstock.CGST;

                //drow["SGST"] = blstock.SGST;

                //drow["IGST"] = blstock.IGST;

                dtbl.Rows.InsertAt(drow, inRowCount);
                ViewState[ItemType] = dtbl;
                gvItems.DataSource = dtbl;
                gvItems.DataBind();
            }
            else
            {
                drow["Item"] = hf_ItemConv.Value;
                drow["Quantity"] = (txtQuantityConv.Text == "" ? "0" : txtQuantityConv.Text);
                drow["CardPrice"] = (txtCardPrice.Text == "" ? "0" : txtCardPrice.Text);
                drow["WholeSalePrice"] = (txtWholeSale.Text == "" ? "0" : txtWholeSale.Text);
                drow["RetailPrice"] = (txtRetailPriceConv.Text == "" ? "0" : txtRetailPriceConv.Text);
                drow["MRP"] = (txtMRP.Text == "" ? "0" : txtMRP.Text);
                drow["SlNoFrom"] = ddlSlno.SelectedValue;
                drow["Total"] = Math.Round(Convert.ToDecimal(txtRetailPriceConv.Text) * Convert.ToDecimal(txtQuantityConv.Text), 2);
                if (chkproduction.Checked == true)
                {
                    drow["Productcost"] = (txtproductioncost.Text == "" ? "0" : txtproductioncost.Text);
                }
                dtbl.Rows.InsertAt(drow, inRowCount);
                ViewState[ItemType] = dtbl;
                gvItemsConv.DataSource = dtbl;
                gvItemsConv.DataBind();
                chkproduction.Enabled = false;
            }

            Total(ItemType);

        }
        else
        {
            ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "alertify.alert('Item already exists')", true);
            return 1;
        }
        return 0;

    }
    protected void Total(string ItemType)
    {
        try
        {
            DataTable dtbl = new DataTable();
            dtbl = (DataTable)ViewState[ItemType];
            object Total = dtbl.Compute("Sum(Total)", "");
            if (ItemType == "ItemsFrom")
            {
                txtFromAmount.Text = Math.Round(decimal.Parse(Total.ToString()), 2).ToString();
            }
            else
            {

                txtToAmount.Text = Math.Round(decimal.Parse(Total.ToString()), 2).ToString();
            }
        }
        catch (Exception ex)
        {
            txtToAmount.Text = "0";
        }
    }
    protected void Save()
    {
        long Status = 0;
        BlltemConversion blltemConversion = new BlltemConversion();
        blltemConversion.FK_StoreFrom = Convert.ToInt64(ddlFromStore.SelectedValue);
        blltemConversion.FK_StoreTo = Convert.ToInt64(ddlToStore.SelectedValue);
        blltemConversion.TransDate = DateTime.ParseExact(txtDate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
        blltemConversion.Remarks = txtRemark.Text;
        blltemConversion.FromAmount = Convert.ToDecimal(txtFromAmount.Text);
        blltemConversion.ToAmount = Convert.ToDecimal(txtToAmount.Text);
        blltemConversion.Cancelled = false;
        blltemConversion.CancelledBy = hfUser.Value;
        blltemConversion.UserCode = hfUser.Value;
        blltemConversion.EnteredOn = DateTime.Now;
        blltemConversion.Production = chkproduction.Checked;
        blltemConversion.FK_Client = Convert.ToInt64(hfClient.Value);


        ArrayList arlist = new ArrayList();
        BlItemConversionDetails blItemConversionDetails;

        DataTable dtblFrom = (DataTable)ViewState["ItemsFrom"];
        foreach (DataRow dr in dtblFrom.Rows)
        {
            blItemConversionDetails = new BlItemConversionDetails();
            blItemConversionDetails.SlNo = Convert.ToInt32(dr["slno"].ToString());
            blItemConversionDetails.FK_Stock = Convert.ToInt64(dr["FK_Stock"].ToString());
            blItemConversionDetails.FK_Items = Convert.ToInt64(dr["FK_Items"].ToString());
            blItemConversionDetails.FK_Store = Convert.ToInt64(ddlFromStore.SelectedValue);
            blItemConversionDetails.FK_RackArrangement = Convert.ToInt64(ddlFromStore.SelectedValue);
            blItemConversionDetails.BatchNo = dr["BatchNo"].ToString();
            blItemConversionDetails.ExpiryDate = DateTime.Parse(dr["Expdate"].ToString());
            blItemConversionDetails.PurchaseRate = Convert.ToDecimal(dr["PurRate"].ToString());
            blItemConversionDetails.MRP = Convert.ToDecimal(dr["MRP"].ToString());
            blItemConversionDetails.RetailPrice = Convert.ToDecimal(dr["RetailPrice"].ToString());
            blItemConversionDetails.WholesalePrice = Convert.ToDecimal(dr["WholesalePrice"].ToString());
            blItemConversionDetails.PrivilagePrice = Convert.ToDecimal(dr["CardPrice"].ToString());
            blItemConversionDetails.VAT = Convert.ToDecimal(dr["VAT"].ToString());
            blItemConversionDetails.Quantity = Convert.ToDecimal(dr["Quantity"].ToString());
            blItemConversionDetails.BarCode = dr["BarCode"].ToString();
            arlist.Add(blItemConversionDetails);
            blItemConversionDetails = null;

        }

        blltemConversion.XmlIndendFrom = Common.WriteToXml(arlist);
        arlist = new ArrayList();
        int SlNoTo = 0;
        DataTable dtblTo = (DataTable)ViewState["ItemsTo"];
        foreach (DataRow dr in dtblTo.Rows)
        {
            blItemConversionDetails = new BlItemConversionDetails();
            SlNoTo = SlNoTo + 1;
            //blItemConversionDetails.SlNo = Convert.ToInt32(dr["slno"].ToString());
            blItemConversionDetails.SlNo = SlNoTo;
            blItemConversionDetails.FK_Stock = Convert.ToInt64(dr["FK_Stock"].ToString());
            blItemConversionDetails.FK_Items = Convert.ToInt64(dr["FK_Items"].ToString()); 
            blItemConversionDetails.FK_Store = Convert.ToInt64(ddlFromStore.SelectedValue);
            blItemConversionDetails.FK_RackArrangement = Convert.ToInt64(ddlFromStore.SelectedValue);
            blItemConversionDetails.BatchNo = dr["BatchNo"].ToString();
          
            blItemConversionDetails.ExpiryDate = DateTime.Parse(dr["Expdate"].ToString());
            blItemConversionDetails.PurchaseRate = Convert.ToDecimal(dr["PurRate"].ToString());
            blItemConversionDetails.MRP = Convert.ToDecimal(dr["MRP"].ToString());
            blItemConversionDetails.RetailPrice = Convert.ToDecimal(dr["RetailPrice"].ToString());
            blItemConversionDetails.WholesalePrice = Convert.ToDecimal(dr["WholesalePrice"].ToString());
            blItemConversionDetails.PrivilagePrice = Convert.ToDecimal(dr["CardPrice"].ToString());
            blItemConversionDetails.VAT = Convert.ToDecimal(dr["VAT"].ToString());
            blItemConversionDetails.Quantity = Convert.ToDecimal(dr["Quantity"].ToString());
            blItemConversionDetails.BarCode = dr["BarCode"].ToString();
           
            
                blItemConversionDetails.SlNoFrom = Convert.ToInt32(dr["SlNoFrom"].ToString());
                if (chkproduction.Checked == true)
                {
                    blItemConversionDetails.Productcost = Convert.ToDecimal(dr["Productcost"].ToString());
                }
            arlist.Add(blItemConversionDetails);
            blItemConversionDetails = null;
        }
        blltemConversion.XmlIndendTo = Common.WriteToXml(arlist);
        if (dtblFrom.Rows.Count == dtblTo.Rows.Count || chkproduction.Checked==true)
        {
            if (hfitemConversion.Value == string.Empty)
            {
                if (hfIsAdd.Value.ToLower() == "true")
                {
                    Status = blltemConversion.InsertData();
                    if (Status > 0)
                    {
                        ClearAll();
                        GridFill(1);
                        ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "ShowAddDiv();", true);
                        Populate.InsertMsg(this);
                        chkproduction.Checked = false;
                       // divproductioncost.Visible = false;
                    }
                    else if (Status == -1 || Status == -2)
                    {
                        ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "ShowAddDiv();", true);
                        Populate.ErrorMessageBox(this, "Cannot insert data, No stock  available.");
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
                long decid = Convert.ToInt64(hfitemConversion.Value);
                blltemConversion.MasterId = decid;
                if (hfIsUpdate.Value.ToLower() == "true")
                {

                    Status = blltemConversion.UpdateData();
                    if (Status > 0)
                    {
                        ClearAll();
                        GridFill(1);
                        Populate.UpdateMsg(this);
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
            ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "alertify.alert('Number of items in both list should be equal')", true);
        }

    }
    public void SetReadonly()
    {

        txtDate.Attributes.Add("readonly", "readonly");
        txtFromDate.Attributes.Add("readonly", "readonly");
        txtToDate.Attributes.Add("readonly", "readonly");
    }
    #endregion
    #region Events
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                divproductioncost.Visible = false;

                hfUser.Value = Request.QueryString["uid"];
                hfClient.Value = Request.QueryString["clnt"];
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

    protected void gvItemConversion_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            
            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                Button btnapprove = (Button)e.Row.FindControl("btnApprove");
                ImageButton btnDelete = (ImageButton)e.Row.FindControl("btnDelete");
                ImageButton btnEdit = (ImageButton)e.Row.FindControl("btnEdit");
                // give property id of button form template field
                if (btnapprove.Text == "True")
                {
                    btnapprove.Enabled = false;
                    btnDelete.Enabled = false;
                    btnapprove.Text = "Approved";
                    // btnEdit.Enabled = true;
                    btnapprove.BackColor = System.Drawing.Color.DarkGreen;//true means enable else you may set false to disable button 
                }
                else
                {
                    btnapprove.BackColor = System.Drawing.Color.DarkRed;
                    btnapprove.Text = "Pending";
                    //  btnEdit.Enabled = true;
                    btnapprove.Enabled = true;
                    btnDelete.Enabled = true;
                }
                // For `Reject` button
                // Same condition but in `FindControl` method use `btnReject` id.
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "toastr.warning('" + ex.Message + "')", true);
        }
    }

    protected void gvItemConversion_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        try
        {
            BlltemConversion blltemConversion = new BlltemConversion();
            ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "ShowAddDiv();", true);
            long MasterId = Convert.ToInt64(gvItemConversion.DataKeys[e.NewSelectedIndex]["ID_Indent"].ToString());
            blltemConversion.MasterId = MasterId;
            hfitemConversion.Value = MasterId.ToString();
            blltemConversion.FK_Client = Convert.ToInt32(hfClient.Value.Split(',')[0]);
            blltemConversion.FromDate = DateTime.Parse("1/1/1900");
            blltemConversion.SelectData();
            txtDate.Text = blltemConversion.TransDate.ToString("dd/MM/yyyy");
            txtRemark.Text = blltemConversion.Remarks;
            txtFromAmount.Text = blltemConversion.FromAmount.ToString();
            txtToAmount.Text = blltemConversion.ToAmount.ToString();

            try
            {
                ddlFromStore.SelectedValue = blltemConversion.FK_StoreFrom.ToString();
            }
            catch (Exception ex)
            {
                ddlFromStore.SelectedValue = "0";
            }
            try
            {
                ddlToStore.SelectedValue = blltemConversion.FK_StoreTo.ToString();
            }
            catch (Exception ex)
            {
                ddlToStore.SelectedValue = "0";
            }

            divAdd.Style.Add("display", "block");
            divView.Style.Add("display", "none;");
            if (blltemConversion.Approved == true)
            {
                PanelAdd.Enabled = false;
                btnSave.Visible = false;
            }
            blltemConversion = new BlltemConversion();
            blltemConversion.MasterId = MasterId;
            blltemConversion.FK_Client = Convert.ToInt32(hfClient.Value.Split(',')[0]);
            blltemConversion.IndentMode = "F";
            DataTable dtblFrom = blltemConversion.SelectItemConversionItems();
            ViewState["ItemsFrom"] = dtblFrom;
            gvItems.DataSource = dtblFrom;
            gvItems.DataBind();
            blltemConversion = new BlltemConversion();
            blltemConversion.MasterId = MasterId;
            blltemConversion.FK_Client = Convert.ToInt32(hfClient.Value.Split(',')[0]);
            blltemConversion.IndentMode = "T";
            DataTable dtblTo = blltemConversion.SelectItemConversionItems();
            ViewState["ItemsTo"] = dtblTo;
            ddlFromStore.Enabled = false;
            if (dtblTo.Rows[0]["Production"].ToString() == "True")
            {
                chkproduction.Checked = true;
                divproductioncost.Visible = true;
                chkproduction.Enabled = false;
            }
            else
            {
                chkproduction.Checked = false;
                chkproduction.Enabled = false;
                divproductioncost.Visible = false;
            }
            gvItemsConv.DataSource = dtblTo;
            gvItemsConv.DataBind();
            SetSlno();
            DivPreviousPurchase.Visible = false;
        }
        catch (Exception ex)
        {

        }


    }
    protected void btnClearItemsConv_Click(object sender, EventArgs e)
    {
        txtQuantityConv.Enabled = true;
        txtPurChaseRate.Enabled = true;
        cmpRetailPrice1.Enabled = true;
        txtBatchNo.Enabled = true;
        txtBarCode.Enabled = true;
        DivPreviousPurchase.Visible = false;
    }
    protected void gvItemConversion_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            if (e.CommandName == "Approve")
            {

                string confirmValue = Request.Form["confirm_value"];
                if (confirmValue.Length > 3)
                    confirmValue = confirmValue.Substring(confirmValue.Length - 3, 3);
                if (confirmValue == "Yes")
                {
                    GridViewRow gr = (GridViewRow)((Button)e.CommandSource).NamingContainer;
                    int index = gr.RowIndex;


                    long status = 0;
                    BlltemConversion blltemConversion = new BlltemConversion();
                    blltemConversion.MasterId = Convert.ToInt64(gvItemConversion.DataKeys[index]["ID_Indent"].ToString());
                    blltemConversion.FK_Client = Convert.ToInt64(hfClient.Value);
                    blltemConversion.UserCode = hfUser.Value;
                    if (hfIsUpdate.Value.ToLower() == "true")
                    {
                        Boolean approved = Convert.ToBoolean(gvItemConversion.DataKeys[index]["Approved"].ToString());
                        if (approved == false)
                        {
                            status = blltemConversion.IndentApprove();
                            if (status >= 0)
                            {
                                GridFill(1);
                                ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "toastr.success('Data approved successfully')", true);
                            }
                            else
                            {
                                ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "toastr.warning('Can not approve')", true);
                            }
                        }
                        else
                        {
                            ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "toastr.warning('Already approved ')", true);
                        }
                    }
                    else
                    {
                        ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "toastr.warning('User does not have permission to Approve')", true);
                    }
                    blltemConversion = null;

                }



            }
            else if (e.CommandName == "Print")
            {
                //string confirmValue = Request.Form["confirm_value"];
                //  if (confirmValue.Length > 3)
                //      confirmValue = confirmValue.Substring(confirmValue.Length - 3, 3);
                //  if (confirmValue == "Yes")
                //  {

                GridViewRow row = (GridViewRow)((ImageButton)e.CommandSource).NamingContainer;
                int index = row.RowIndex;
                string PurchaseId = gvItemConversion.DataKeys[index]["ID_Indent"].ToString();

                string popupreport = "<script> window.open('ItemConversionReport.aspx?PurchaseId=" + PurchaseId + "', 'self', 'left=200,top=50,width=900,height=680,status=0,resizable=0,fullscreen=0,scrollbars=1');</script>";
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "Success", popupreport, false);

                // }

            }
        }
        catch (Exception ex)
        {
            Populate.WarningMsg(this, ex.ToString());
        }
       

    }
    protected void gvItemConversion_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            string confirmValue = Request.Form["confirm_value"];
            if (confirmValue.Length > 3)
                confirmValue = confirmValue.Substring(confirmValue.Length - 3, 3);
            if (confirmValue == "Yes")
            {
                long status = 0;
                BlltemConversion blltemConversion = new BlltemConversion();
                blltemConversion.MasterId = Convert.ToInt64(gvItemConversion.DataKeys[e.RowIndex]["ID_Indent"].ToString());
                blltemConversion.FK_Client = Convert.ToInt64(hfClient.Value);
                blltemConversion.FK_StoreFrom = Convert.ToInt64(gvItemConversion.DataKeys[e.RowIndex]["FK_StoreFrom"].ToString());
                blltemConversion.FK_StoreTo = Convert.ToInt64(gvItemConversion.DataKeys[e.RowIndex]["FK_StoreTo"].ToString());
                blltemConversion.UserCode = hfUser.Value;
                if (hfIsDelete.Value.ToLower() == "true")
                {
                    status = blltemConversion.DeleteltemConversion();
                    if (status > 0)
                    {

                        GridFill(1);
                        Populate.DeleteMsg(this);
                    }
                    else
                    {
                        ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "toastr.warning('Cannot delete data, Already approved')", true);
                    }
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "toastr.warning('User does not have permission to Delete')", true);
                }
                blltemConversion = null;

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
    protected void btnClear_Click1(object sender, EventArgs e)
    {
        ClearAll();
    }
    protected void gvItemsConv_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            string confirmValue = Request.Form["confirm_value"];
            if (confirmValue.Length > 3)
                confirmValue = confirmValue.Substring(confirmValue.Length - 3, 3);
            if (confirmValue == "Yes")
            {
                if (ViewState["ItemsTo"] != null)
                {
                    DataTable dtbl = (DataTable)ViewState["ItemsTo"];
                    dtbl.Rows.RemoveAt(e.RowIndex);
                    ViewState["ItemsTo"] = dtbl;
                    gvItemsConv.DataSource = dtbl;
                    gvItemsConv.DataBind();
                    Total("ItemsTo");
                    SetSlno();
                }
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "toastr.warning('" + ex.Message + "')", true);
        }

    }
    protected void btnItemBlurTrigger_Click(object sender, EventArgs e)
    {
        if (hfType.Value == "1")
        {
            hdfFKStockItem.Value = hdfFKStock.Value;
            hf_Item.Value = hfItem.Value;
            txtQuantity.Focus();
        }
        else if (hfType.Value == "2")
        {

            //select from items

            hdfFKStockConv.Value = "0";
            hf_ItemConv.Value = hfItem.Value;
            txtitem.Text = "";

            //BlStock blstock = new BlStock();
            //blstock.MasterId = Convert.ToInt64(hdfFKStockConv.Value);
            //blstock.SelectData();

            int slno = 0;

            DataTable dtbl = new DataTable();
            Boolean itemexist = false;
            if (ViewState["ItemsFrom"] != null)
            {
                dtbl = (DataTable)ViewState["ItemsFrom"];
            }
            else
            {
                dtbl = GetDataTable(dtbl);
            }
            for (int i = 0; i < dtbl.Rows.Count; i++)
            {
                if (dtbl.Rows[i]["FK_Items"].ToString() == hfFK_Items.Value.ToString())
                {
                    itemexist = true;
                    slno = i + 1;
                    break;
                }
            }

            if (itemexist)
            {
                txtQuantityConv.Enabled = false;
                txtPurChaseRate.Enabled = true;
                cmpRetailPrice1.Enabled = false;
                txtBatchNo.Enabled = false;
                txtBarCode.Enabled = false;
                btnAddItemsConv.Focus();
                try
                {

                    ddlSlno.SelectedValue = slno.ToString();
                    FillConversionItems();
                }
                catch (Exception ex)
                {
                }
            }
            else
            {
                txtitemConv.Text = hfItem.Value;
                txtQuantityConv.Enabled = true;
                txtPurChaseRate.Enabled = true;
                cmpRetailPrice1.Enabled = true;
                txtBatchNo.Enabled = true;
                txtBarCode.Enabled = true;
                txtQuantityConv.Focus();
            }

        }
        if (hfFK_Items.Value != "" && hfFK_Items.Value != "0")
        {
            LoadPreviousPurchase();
        }
        else
        {
            DivPreviousPurchase.Visible = false;
        }
        if (hdfCurrentStock.Value != "")
        {

            lblCurrentStock.Text = "Current Stock= " + hdfCurrentStock.Value.ToString();
        }
    }
    protected void btnAddItemsTo_Click(object sender, EventArgs e)
    {

        try
        {
          if ((Convert.ToDecimal(txtRetailPriceConv.Text)) >= (Convert.ToDecimal(txtproductioncost.Text == "" ? "0" : txtproductioncost.Text)))
            {

                int a = 0;
                if (hf_ItemConv.Value.Trim() == txtitemConv.Text.ToString().Trim() && ddlSlno.SelectedIndex > 0)
                {
                    AddDataItem("ItemsTo", Convert.ToInt64(hdfFKStockConv.Value));

                    ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "ItemClear();", true);
                    DivPreviousPurchase.Visible = false;
                    lblCurrentStock.Text = "Current Stock= 0";
                }
                else
                {
                    ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "toastr.warning('Item  does not exist')", true);
                }
                txtitem.Focus();


                if (a == 0)
                {

                    txtitemConv.Focus();
                    SetSlno();
                }
                else
                {
                    btnClearItems.Focus();

                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "toastr.warning('Production Cost Must be Less Than Or Eaqual To Retail Price')", true);
            }
        }
        catch (Exception ex)
        {
        }

    }
    protected void btnTransfer_Click(object sender, EventArgs e)
    {
        if (cbxTransfer.Checked)
        {
            DataTable dtbl = new DataTable();
            DataTable dtblConv = new DataTable();
            int inRowCount = 0;
            int inCheck = 0;
            if (ViewState["ItemsTo"] != null)
            {
                dtblConv = (DataTable)ViewState["ItemsTo"];
                inRowCount = dtblConv.Rows.Count;
            }
            else
            {
                dtblConv = GetDataTable(dtbl);
            }
            if (ViewState["ItemsFrom"] != null)
            {
                dtbl = (DataTable)ViewState["ItemsFrom"];
            }
            else
            {
                dtbl = GetDataTable(dtbl);
            }
            if (dtbl.Rows.Count > 0)
            {
                for (int j = 0; j < dtblConv.Rows.Count; j++)
                {
                    for (int i = 0; i < dtbl.Rows.Count; i++)
                    {

                        if (dtbl.Rows[i]["FK_Stock"].ToString() == dtblConv.Rows[j]["FK_Stock"].ToString() || dtbl.Rows[i]["slno"].ToString() == dtblConv.Rows[i]["SlNoFrom"].ToString())
                        {
                            inCheck = inCheck + 1;
                            break;
                        }
                    }
                }

            }
            if (inCheck == 0)
            {
                for (int i = 0; i < dtbl.Rows.Count; i++)
                {
                    DataRow drow = dtblConv.NewRow();
                    drow["slno"] = inRowCount + i + 1;
                    drow["FK_Items"] = dtbl.Rows[i]["FK_Items"].ToString();
                    drow["BatchNo"] = dtbl.Rows[i]["BatchNo"].ToString();
                    drow["Item"] = dtbl.Rows[i]["Item"].ToString();
                    drow["Quantity"] = dtbl.Rows[i]["Quantity"].ToString();
                    drow["CardPrice"] = dtbl.Rows[i]["CardPrice"].ToString();
                    drow["WholeSalePrice"] = dtbl.Rows[i]["WholeSalePrice"].ToString();
                    drow["RetailPrice"] = dtbl.Rows[i]["RetailPrice"].ToString();
                    drow["MRP"] = dtbl.Rows[i]["MRP"].ToString();
                    drow["SlNoFrom"] = dtbl.Rows[i]["slno"].ToString();
                    drow["FK_Stock"] = dtbl.Rows[i]["FK_Stock"].ToString();
                    drow["Expdate"] = dtbl.Rows[i]["Expdate"].ToString();
                    drow["PurRate"] = dtbl.Rows[i]["PurRate"].ToString();
                    drow["VAT"] = dtbl.Rows[i]["VAT"].ToString();
                    drow["BarCode"] = dtbl.Rows[i]["BarCode"].ToString();
                    drow["Total"] = dtbl.Rows[i]["Total"].ToString();
                    dtblConv.Rows.InsertAt(drow, inRowCount);
                }
                dtblConv.DefaultView.Sort = "slno";
                dtblConv = dtblConv.DefaultView.ToTable();
                ViewState["ItemsTo"] = dtblConv;
                gvItemsConv.DataSource = dtblConv;
                gvItemsConv.DataBind();
                Total("ItemsTo");
                SetSlno();
                btnSave.Focus();
            }
            else
            {
                ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "alertify.alert('Item already exists')", true);
            }
        }
    }
    protected void FillConversionItems()
    {
        try
        {
            if (ddlSlno.SelectedIndex > 0 && txtitemConv.Text == "")
            {
                DataTable dtbl = new DataTable();
                if (ViewState["ItemsFrom"] != null)
                {
                    dtbl = (DataTable)ViewState["ItemsFrom"];
                }
                string Slno = ddlSlno.SelectedValue;
                int i = 0;
                for (i = 0; i < dtbl.Rows.Count; i++)
                {
                    if (dtbl.Rows[i]["Slno"].ToString() == Slno)
                    {
                        break;
                    }
                }
                //    Slno = Slno - 1;
                hdfFKStockConv.Value = dtbl.Rows[i]["FK_Stock"].ToString();
                hf_ItemConv.Value = dtbl.Rows[i]["Item"].ToString();
                txtQuantityConv.Text = dtbl.Rows[i]["Quantity"].ToString();
                txtCardPrice.Text = dtbl.Rows[i]["CardPrice"].ToString();
                txtWholeSale.Text = dtbl.Rows[i]["WholeSalePrice"].ToString();
                txtRetailPriceConv.Text = dtbl.Rows[i]["RetailPrice"].ToString();
                txtMRP.Text = dtbl.Rows[i]["MRP"].ToString();
                txtitemConv.Text = dtbl.Rows[i]["Item"].ToString();
                txtCurrentStockTo.Text = dtbl.Rows[i]["Quantity"].ToString();
                txtPurChaseRate.Text = dtbl.Rows[i]["PurRate"].ToString();
                txtBatchNo.Text = dtbl.Rows[i]["BatchNo"].ToString();
                txtBarCode.Text = dtbl.Rows[i]["BarCode"].ToString();
                txtQuantityConv.Enabled = false;
                txtPurChaseRate.Enabled = false;
                cmpRetailPrice1.Enabled = false;
                txtBatchNo.Enabled = false;
                txtBarCode.Enabled = false;
            }
            btnAddItemsConv.Focus();
        }
        catch (Exception ex)
        {

        }
    }
    protected void ddlSlno_SelectedIndexChanged(object sender, EventArgs e)
    {

        FillConversionItems();
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
        BlltemConversion blltemConversion = new BlltemConversion();
        blltemConversion.PageSize = 10;
        blltemConversion.PageIndex = Convert.ToInt16(pageIndex);
        GridFill(blltemConversion.PageIndex);
        blltemConversion = null;
    }
    //protected void ddlToStore_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    try
    //    {
    //        hfToStore.Value = ddlToStore.SelectedValue;
    //        ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "ItemClear();", true);
    //        ddlToStore.Focus();
    //    }
    //    catch (Exception ex)
    //    {

    //    }
    //}
    //protected void ddlFromStore_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    try
    //    {
    //        hfFromStore.Value = ddlfromStore.SelectedValue;
    //        ddlfromStore.Focus();
    //    }
    //    catch (Exception ex)
    //    {

    //    }
    //}
    protected void btnAddItemsFrom_Click(object sender, EventArgs e)
    {
        try
        {
            int a = 0;
            if (hf_Item.Value.Trim() == txtitem.Text.ToString().Trim())
            {
                a = AddDataItem("ItemsFrom", Convert.ToInt64(hdfFKStockItem.Value));

                ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", " ItemClear();", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "toastr.warning('Item  does not exist')", true);
            }
            txtitem.Focus();


            if (a == 0)
            {
                ddlFromStore.Enabled = false;
                txtitem.Focus();

                SetSlno();

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
    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            System.Threading.Thread.Sleep(10);
            if (gvItems.Rows.Count > 0 && gvItemsConv.Rows.Count > 0)
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
    protected void gvItems_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            string confirmValue = Request.Form["confirm_value"];
            if (confirmValue.Length > 3)
                confirmValue = confirmValue.Substring(confirmValue.Length - 3, 3);
            if (confirmValue == "Yes")
            {
                DataTable dtbl = new DataTable();
                DataTable dtblConv = new DataTable();
                int inRowCount = 0;
                int inCheck = 0;
                if (ViewState["ItemsTo"] != null)
                {
                    dtblConv = (DataTable)ViewState["ItemsTo"];
                    inRowCount = dtblConv.Rows.Count;
                }
                else
                {
                    dtblConv = GetDataTable(dtbl);
                }
                if (ViewState["ItemsFrom"] != null)
                {
                    dtbl = (DataTable)ViewState["ItemsFrom"];
                }
                else
                {
                    dtbl = GetDataTable(dtbl);
                }
                int i = e.RowIndex;

                if (dtbl.Rows.Count > 0)
                {
                    for (int j = 0; j < dtblConv.Rows.Count; j++)
                    {
                        //for (int i = 0; i < dtbl.Rows.Count; i++)
                        //{

                        if (gvItems.DataKeys[e.RowIndex]["FK_Stock"].ToString() == dtblConv.Rows[j]["FK_Stock"].ToString() || gvItems.DataKeys[e.RowIndex]["slno"].ToString() == dtblConv.Rows[j]["SlNoFrom"].ToString())
                        {
                            inCheck = inCheck + 1;
                            break;
                        }
                        //}
                    }

                }
                if (inCheck == 0)
                {

                    dtbl.Rows.RemoveAt(e.RowIndex);
                    ViewState["ItemsFrom"] = dtbl;
                    gvItems.DataSource = dtbl;
                    gvItems.DataBind();
                    SetSlno();
                    Total("ItemsFrom");
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "toastr.error('Cannot delete, reference exists')", true);
                }
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "toastr.warning('" + ex.Message + "')", true);
        }

    }
    protected void LoadPreviousPurchase()
    {

        BlMaterialReciept blmaterialreciept = new BlMaterialReciept();
        blmaterialreciept.FK_Client = Convert.ToInt64(hfClient.Value);
        blmaterialreciept.FK_ItemsforPreviousPurchase = Convert.ToInt64(hfFK_Items.Value);
        DataTable dtbl = new DataTable();
        dtbl = blmaterialreciept.SelectPreviousPurchaseDetails();

        if (dtbl.Rows.Count > 0)
        {
            GvPreviousPurchase.DataSource = dtbl;
            GvPreviousPurchase.DataBind();
            DivPreviousPurchase.Visible = true;



           

        }
        else
        {
            GvPreviousPurchase.DataSource = dtbl;
            GvPreviousPurchase.DataBind();
            DivPreviousPurchase.Visible = false;

        }
        blmaterialreciept = null;

    }
    #endregion

    protected void cbxTransfer_CheckedChanged(object sender, EventArgs e)
    {
        try
        {
            if (cbxTransfer.Checked == true)
            {
                btnTransfer_Click(sender, e);
                DivPreviousPurchase.Visible = false;

            }
         }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "toastr.warning('" + ex.Message + "')", true);
        }
    }
    protected void btnClearItems_Click(object sender, EventArgs e)
    {
         try
        {

            lblCurrentStock.Text = "Current Stock= 0";
        }
         catch (Exception ex)
         {
             ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "toastr.warning('" + ex.Message + "')", true);
         }
    }
    protected void ddlItemsFill()
    {
        ////if (ddlItem.SelectedIndex > 0)
        ////{
        ////    BlItems blitems = new BlItems();
        ////    blitems.MasterId = Convert.ToInt64(ddlItem.SelectedValue.ToString());
        ////    blitems.FK_Client = Convert.ToInt64(hfClient.Value);
        ////    blitems.SelectData();
           

        ////    DataTable dtbl = new DataTable();
        ////    dtbl = blitems.ItemsSelectCurrentStock();
        ////    if (dtbl.Rows.Count > 0)
        ////    {
        ////        lblCurrentStock.Text = "Current Stock= " + dtbl.Rows[0]["CurrentStock"].ToString();
        ////       // lblOrderedQuantity.Text = "Ordered Quantity= " + dtbl.Rows[0]["OrderedQuantity"].ToString();
        ////    }
        ////    else
        ////    {
        ////        lblCurrentStock.Text = "Current Stock= 0";
        ////      //  lblOrderedQuantity.Text = "Ordered Quantity= 0 ";
        ////    }
        ////    blitems = null;

        ////}
      
    }
    protected void chkproduction_CheckedChanged(object sender, EventArgs e)
    {
        if (chkproduction.Checked == true)
        {
            divproductioncost.Visible = true;
        }
        else
        {
            divproductioncost.Visible = false;
        }
    }
}