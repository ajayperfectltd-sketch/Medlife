/*----------------------------------------------------------------------------------------------------------------------------------------------------------
Purpose    : Insert/Update/Delete Purchase Order  
Author     : Aiswarya
Copyright  : Perfect Software Solutions (Clt) Pvt Ltd
Created on : 05/06/2018
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
using WebPharma.Business.Accounts;
using System.Data;
using System.Collections;
using System.Globalization;


public partial class PurchaseOrder : System.Web.UI.Page
{

    #region Functions
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
            BlPurchaseOrder blPurchaseOrder = new BlPurchaseOrder();
            //blReason.MasterId = Convert.ToInt64(gvReason.DataKeys[e.RowIndex]["ID_Reason"].ToString());
            blPurchaseOrder.UserCode = hfuserName.Value;
            blPurchaseOrder.MasterId = Convert.ToInt64(hfMasterID.Value);
            blPurchaseOrder.FK_Reason = Convert.ToInt64(Reason);
            if (hfIsDelete.Value.ToLower() == "true")
            {
                status = blPurchaseOrder.DeleteData();
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
            blPurchaseOrder = null;
        }
    }
    public void BindData()
    {
        Populate populate = new Populate();
        populate.FillDropdownList(ddlStore, "Store", "StoName", "ID_Store", "StoName", " Cancelled=0 and Active=1 and FK_Client = " + hfClient.Value,"");
        populate.FillDropdownList(ddlStoreSearch, "Store", "StoName", "ID_Store", "StoName", "Cancelled=0 and Active=1 and FK_Client = " + hfClient.Value,"");

        populate.FillDropdownList(ddlSupplier, "Supplier", "SupplierName", "ID_Supplier", "SupplierName", " Active=1 And Cancelled=0 and FK_Client = " + hfClient.Value, "");
        populate.FillDropdownList(ddlSupplierSearch, "Supplier", "SupplierName", "ID_Supplier", "SupplierName", " Active=1 And Cancelled=0 and FK_Client = " + hfClient.Value, "");

       // populate.FillDropdownList(ddlItem, "Items", "ItemName", "ID_Items", "ItemName", "Cancelled=0 AND FK_Client= " + hfClient.Value.Split(',')[0], "");
        populate.FillDropdownList(ddlUnit, "Units", "UnitName", "ID_Units", "UnitName", " Cancelled=0 AND FK_Client= " + hfClient.Value.Split(',')[0], "");
        populate.FillDropdownList(ddlSubUnit, "SubUnit", "SubUnitName", "ID_SubUnits", "SubUnitName", " Cancelled=0 AND FK_Client= " + hfClient.Value.Split(',')[0], "");
        populate = null;

    }
    public void SetReadonly(params TextBox[] textbox)
    {
        foreach (TextBox txt in textbox)
            txt.Attributes.Add("readonly", "readonly");

    }
    protected void GridFill(int currentPage)
    {
        
        BlPurchaseOrder blpurchasse = new BlPurchaseOrder();
        DataTable dtbl = new DataTable();
        blpurchasse.FromDate = Convert.ToDateTime(txtFromDate.Text == "" ? "" + DateTime.Now.Date + "" : txtFromDate.Text);//DateTime.ParseExact(txtFromDate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
        blpurchasse.ToDate = Convert.ToDateTime(txtTodate.Text == "" ? "" + DateTime.Now.Date + "" : txtTodate.Text);//DateTime.ParseExact(txtTodate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
        blpurchasse.FK_Supplier = Convert.ToInt64(ddlSupplierSearch.SelectedValue);
        blpurchasse.DeliveryType = ddlDeliveryTypeSearch.SelectedIndex == 0 ? "" : ddlDeliveryTypeSearch.SelectedValue;
        blpurchasse.isClosed = ddlClosedSearch.SelectedValue;
        blpurchasse.FK_Store = Convert.ToInt64(ddlStoreSearch.SelectedValue);
        blpurchasse.PageSize = 10;
        currentPage = currentPage == 0 ? 1 : currentPage;
        blpurchasse.PageIndex = Convert.ToInt16(currentPage);
        blpurchasse.FK_Client = Convert.ToInt64(hfClient.Value);
        dtbl = blpurchasse.SelectAllData();
        gvPurchaseOrder.DataSource = dtbl;
        gvPurchaseOrder.DataBind();
        blpurchasse = null;
    }
   
    protected void CheckUserAccess()
    {
        BlUserAccess bluseraccess = new BlUserAccess();
        Populate populate = new Populate();

        bluseraccess.FK_Users = Convert.ToInt64(hfUser.Value);
        bluseraccess.FK_Pages = 22;
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

    protected void AddData()
    {
        // Add your max limit here
        decimal maxAllowedAmount = 9999999;

        decimal qty = Convert.ToDecimal(txtQunatity.Text == "" ? "0" : txtQunatity.Text);
        decimal rate = Math.Round(Convert.ToDecimal(txtPurchaseRate.Text == "" ? "0" : txtPurchaseRate.Text), 4);
        decimal amount = Math.Round(qty * rate, 4);

        if (amount > maxAllowedAmount)
        {
            ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "alertify.alert('Total amount exceeds allowed limit.');", true);
            return;
        }

        DataTable dtbl = new DataTable();
        int rowCount = 0;
        int inCheck = 0;
        if (ViewState["TempTable"] != null)
        {
            dtbl = (DataTable)ViewState["TempTable"];
            rowCount = dtbl.Rows.Count;

            // Check total existing amount + new amount
            decimal existingTotal = 0;
            foreach (DataRow r in dtbl.Rows)
                existingTotal += Convert.ToDecimal(r["Amount"].ToString() == "" ? "0" : r["Amount"].ToString());

            if ((existingTotal + amount) > maxAllowedAmount)
            {
                ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "alertify.alert('Total amount exceeds allowed limit.');", true);
                return;
            }
        }
        else
        {
            dtbl.Columns.Add("Item"); dtbl.Columns.Add("FK_Items"); dtbl.Columns.Add("Quantity");
            dtbl.Columns.Add("PurchaseRate"); dtbl.Columns.Add("SalesPrice"); dtbl.Columns.Add("SalesPrice2");
            dtbl.Columns.Add("Discount"); dtbl.Columns.Add("BatchNo"); dtbl.Columns.Add("Amount");
            dtbl.Columns.Add("Notes"); dtbl.Columns.Add("FK_Units"); dtbl.Columns.Add("Units");
            dtbl.Columns.Add("CurrentStock"); dtbl.Columns.Add("FK_SubUnit"); dtbl.Columns.Add("SubUnit");
        }

        if (dtbl.Rows.Count > 0)
        {
            for (int i = 0; i < dtbl.Rows.Count; i++)
            {
                if (dtbl.Rows[i]["FK_Items"].ToString() == HfItemsID.Value.ToString())
                { inCheck++; break; }
            }
        }

        if (inCheck == 0)
        {
            DataRow drow = dtbl.NewRow();
            drow["Item"] = hfItem.Value.ToString();
            drow["FK_Items"] = HfItemsID.Value.ToString();
            drow["Quantity"] = qty;
            drow["PurchaseRate"] = rate;
            drow["Amount"] = amount.ToString();
            drow["FK_Units"] = ddlUnit.SelectedValue.ToString();
            drow["Units"] = ddlUnit.SelectedItem.ToString();
            drow["CurrentStock"] = Convert.ToDecimal(lblCurrentStock.Text == "" ? "0" : lblCurrentStock.Text.Replace("Current Stock=", ""));
            drow["FK_SubUnit"] = Convert.ToInt64(ddlSubUnit.SelectedValue == "" ? "0" : ddlSubUnit.SelectedValue);
            drow["SubUnit"] = Convert.ToInt64(ddlSubUnit.SelectedValue.ToString()) > 0 ? ddlSubUnit.SelectedItem.ToString() : (object)DBNull.Value;

            dtbl.Rows.InsertAt(drow, rowCount);
            gvItems.DataSource = dtbl;
            gvItems.DataBind();
            ViewState["TempTable"] = dtbl;
            ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "ItemClear(); CalculateTotal();", true);
            txtItemShortName.Focus();
            lblCurrentStock.Text = "0";
            lblOrderedQuantity.Text = "0";
            ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "ItemClear();", true);
        }
        else
        {
            ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "alertify.alert('Item already exists')", true);
            txtItemShortName.Text = string.Empty; txtQunatity.Text = string.Empty;
            lblCurrentStock.Text = string.Empty; ddlUnit.SelectedValue = "0";
            lblOrderedQuantity.Text = string.Empty; txtPurchaseRate.Text = string.Empty;
        }
    }

    protected void ddlItemsFill()
    {
        //if (ddlItem.SelectedIndex > 0)
        //{
            BlItems blitems = new BlItems();
            blitems.MasterId = Convert.ToInt64(HfItemsID.Value);
            hfItemvalue.Value = Convert.ToString(HfItemsID.Value);
            blitems.FK_Client = Convert.ToInt64(hfClient.Value);
            blitems.SelectData();
           // txtSalesPrice.Text = blitems.MinimumSalesPrice.ToString();
            //txtSalesPrice2.Text = blitems.SellingPrice2.ToString();
            Populate populate = new Populate();
           // populate.FillDropdownList(ddlUnit, "Units", "UnitName", "ID_Units", "UnitName", " Cancelled=0 AND FK_Client= " + hfClient.Value.Split(',')[0], "");
          // populate.FillDropdownList(ddlUnit, "Units", "Name", "ID_Units", "ID_Units", "FK_Client = " + Convert.ToInt64(hfClient.Value)And Cancelled=0", "");
            //populate = null;
            ddlUnit.SelectedValue = blitems.FK_Units.ToString();
            txtItemShortName.Text = blitems.Name.ToString();
            hfPacked.Value = blitems.Packed.ToString();
            ddlUnit.Focus();

            DataTable dtbl= new DataTable();
          dtbl=  blitems.ItemsSelectCurrentStock();
          if (dtbl.Rows.Count > 0)
          {
              lblCurrentStock.Text =  dtbl.Rows[0]["CurrentStock"].ToString();
              lblOrderedQuantity.Text =  dtbl.Rows[0]["OrderedQuantity"].ToString();
          }
          else
          {
              lblCurrentStock.Text = "0";
              lblOrderedQuantity.Text = "0";
          }
          blitems = null;
           
      //  }
        ddlUnit.Focus();
    }

    public void Save()
    {
        DateTime orderDate;
        if (!DateTime.TryParseExact(txtDate.Text, "dd/MM/yyyy",
                CultureInfo.InvariantCulture, DateTimeStyles.None, out orderDate))
        {
            ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script",
                "toastr.warning('Order date is not in a valid format.');", true);
            return;
        }

        DateTime today = DateTime.Today;
        if (orderDate > today)
        {
            ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script",
                "toastr.warning('Order date cannot be a future date.');", true);
            return;
        }

        // Financial year start: April 1 of current FY
        int fyYear = (today.Month >= 4) ? today.Year : today.Year - 1;
        DateTime fyStart = new DateTime(fyYear, 4, 1);

        if (orderDate < fyStart)
        {
            ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script",
                "toastr.warning('Order date cannot be from a previous financial year. "
                + "Allowed from: " + fyStart.ToString("dd/MM/yyyy") + ".');", true);
            return;
        }

        DateTime deliveryDate;
        if (!DateTime.TryParseExact(txtDelivaryDate.Text, "dd/MM/yyyy",
                CultureInfo.InvariantCulture, DateTimeStyles.None, out deliveryDate))
        {
            ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script",
                "toastr.warning('Delivery date is not in a valid format.');", true);
            return;
        }

        if (deliveryDate < orderDate)
        {
            ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script",
                "toastr.warning('Delivery date cannot be earlier than the order date.');", true);
            return;
        }
        // ── End date validation ────────────────────────────────────────────


        long status = 0;
        BlPurchaseOrder blpurchase = new BlPurchaseOrder();


        if (txtRemark.Text.Length > 500)
        {
            ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script",
                "toastr.warning('Remark must not exceed 500 characters.');", true);
            txtRemark.Focus();
            return;
        }
        if (txtClosedRemark.Text.Length > 500)
        {
            ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script",
                "toastr.warning('Close Remark must not exceed 500 characters.');", true);
            txtClosedRemark.Focus();
            return;
        }
        blpurchase.Date = txtDate.Text == "" ? DateTime.Now : DateTime.ParseExact(txtDate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
        blpurchase.FK_Supplier = Convert.ToInt64(ddlSupplier.SelectedValue);
       // blpurchase.FK_OrderType = Convert.ToInt64(ddlOrderType.SelectedValue);
        blpurchase.totalAmount = Convert.ToDecimal(txtTotalAmount.Text == "" ? "0" : txtTotalAmount.Text);
        blpurchase.Discount = Convert.ToDecimal(txtDiscountLast.Text == "" ? "0" : txtDiscountLast.Text);
        blpurchase.RoundAmount = Convert.ToDecimal(txtRoundAmount.Text == "" ? "0" : txtRoundAmount.Text);
        blpurchase.NetAmount = Convert.ToDecimal(txtNetAmount.Text == "" ? "0" : txtNetAmount.Text);
        blpurchase.Advance = Convert.ToDecimal(txtAdvance.Text == "" ? "0" : txtAdvance.Text);
        blpurchase.Remarks = txtRemark.Text;
        blpurchase.CashAmount = Convert.ToDecimal(txtCashAmount.Text == "" ? "0" : txtCashAmount.Text);
        blpurchase.FK_ItemType = Convert.ToInt64(ddlItemType.SelectedValue);
        blpurchase.DeliveryDate = txtDelivaryDate.Text == "" ? DateTime.Now : DateTime.ParseExact(txtDelivaryDate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
        blpurchase.DeliveryType = ddlDeliveryType.SelectedValue;
        blpurchase.Closed = cbxIsClosed.Checked;
        if (cbxIsClosed.Checked)
            blpurchase.ClosedUser = hfUser.Value;
        else
            blpurchase.ClosedUser = "";
        blpurchase.UserCode = hfuserName.Value;
        blpurchase.ClosedRemarks = txtClosedRemark.Text;
        blpurchase.EnteredOn = DateTime.Now;
        blpurchase.FK_Store = Convert.ToInt64(ddlStore.SelectedValue);
        blpurchase.FK_Client = Convert.ToInt64(hfClient.Value);
        ArrayList arlist = new ArrayList();
        DataTable dtbl = (DataTable)ViewState["TempTable"];
        IPurchaseOrderDetails purchaseOrderDetails;
       
        for (int i = 0; i < dtbl.Rows.Count; i++)
        {
            if (Convert.ToDecimal(((TextBox)gvItems.Rows[i].FindControl("txtQuantity")).Text) > 0 && Convert.ToDecimal(((TextBox)gvItems.Rows[i].FindControl("txtPurchaseRategv")).Text) > 0)
            {
                purchaseOrderDetails = new IPurchaseOrderDetails();
                // purchaseOrderDetails.FK_PurchaseOrder = 0;
                purchaseOrderDetails.FK_Items = Convert.ToInt64(dtbl.Rows[i]["FK_Items"].ToString());
                purchaseOrderDetails.Quantity = Convert.ToDecimal(((TextBox)gvItems.Rows[i].FindControl("txtQuantity")).Text);// Convert.ToDecimal(dtbl.Rows[i]["Quantity"].ToString());
                purchaseOrderDetails.PurchaseRate = Convert.ToDecimal(((TextBox)gvItems.Rows[i].FindControl("txtPurchaseRategv")).Text);// Convert.ToDecimal(dtbl.Rows[i]["PurchaseRate"].ToString());

               // purchaseOrderDetails.SalesPrice = Convert.ToDecimal(dtbl.Rows[i]["SalesPrice"].ToString() == "" ? 0 : Convert.ToDecimal(dtbl.Rows[i]["SalesPrice"].ToString()));
               // purchaseOrderDetails.DiscountItem = Convert.ToDecimal(dtbl.Rows[i]["Discount"].ToString() == "" ? 0 : Convert.ToDecimal(dtbl.Rows[i]["SalesPrice"].ToString()));
               // purchaseOrderDetails.BatchNo = dtbl.Rows[i]["BatchNo"].ToString();
                purchaseOrderDetails.SalesPrice = Convert.ToDecimal(((Label)gvItems.Rows[i].FindControl("lblAmount")).Text);
                purchaseOrderDetails.FK_Units = Convert.ToInt64(dtbl.Rows[i]["FK_Units"].ToString());
                purchaseOrderDetails.FK_SubUnit = Convert.ToInt64(dtbl.Rows[i]["FK_SubUnit"].ToString()==""?0:Convert.ToInt64(dtbl.Rows[i]["FK_SubUnit"].ToString()));
                arlist.Add(purchaseOrderDetails);
                purchaseOrderDetails = null;
            }
        }

        blpurchase.XmlPurchaseItems = Common.WriteToXml(arlist);

        if (arlist.Count > 0)
        {
            if (hdfPurcahseOrder.Value == "")
            {
                if (hfIsAdd.Value.ToLower() == "true")
                {
                    status = blpurchase.InsertData();
                    if (status > 0)
                    {
                        Clear();
                        GridFill(1);
                        ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "ShowAddDiv();", true);
                        Populate.InsertMsg(this);
                    }
                    else if (status == -3)
                    {
                        ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "ShowAddDiv();", true);
                        Populate.ErrorMessageBox(this, "Order date cannot be a future date.");
                    }
                    else if (status == -4)
                    {
                        ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "ShowAddDiv();", true);
                        Populate.ErrorMessageBox(this, "Order date cannot be from a previous financial year.");
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "ShowAddDiv();", true);
                        Populate.ErrorMessageBox(this, "Cannot insert data, already exists in the Purchase Order.");
                    }
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "toastr.warning('User does not have permission to add')", true);

                }
            }
            else
            {
                blpurchase.MasterId = Convert.ToInt64(hdfPurcahseOrder.Value);
                if (hfIsUpdate.Value.ToLower() == "true")
                {

                    status = blpurchase.UpdateData();
                    if (status > 0)
                    {

                        Clear();
                        GridFill(1);

                        Populate.UpdateMsg(this);

                    }
                    else if (status == -2)
                    {
                        ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "ShowAddDiv();", true);
                        //blMessage.Items.Add("Cannot update data,Item already exist in the Material Receipt.");
                        Populate.ErrorMessageBox(this, "Cannot update data,Material Receipt already done");

                    }

                    else
                    {
                        ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "ShowAddDiv();", true);
                        //  //blMessage.Items.Add("Cannot insert data,Item already exist in the Purchase Order.");
                        Populate.ErrorMessageBox(this, "Cannot update data .referance exists ");

                        //ddlItem.Focus();
                    }
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "toastr.warning('User does not have permission to Update')", true);
                }
            }

        }
        else
        { ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "toastr.warning('Please Fill Atleast one data with Quantity and Purchase Rate')", true); }
    }

    protected void Smartill()
    {


        BlPurchaseOrder blpurchase = new BlPurchaseOrder();
        DataTable dtbl = new DataTable();
        blpurchase.FK_Store = Convert.ToInt64(ddlStore.SelectedValue);
        blpurchase.FK_Supplier = Convert.ToInt64(ddlSupplier.SelectedValue);
        blpurchase.FK_Client = Convert.ToInt64(hfClient.Value);

        dtbl = blpurchase.SelectPurchaseOrderSmartFillItems();
        gvItems.DataSource = dtbl;
        gvItems.DataBind();
        ViewState["TempTable"] = dtbl;
        if (gvItems.Rows.Count > 0)
        {
           
            //btnSmartFill.Enabled = false;
            //txtSupplierName.Enabled = false;
            //if (Convert.ToInt64(ddlItemType.SelectedValue) == 2)
            //{
            //    txtPurchaseNo.Visible = true;
            //    ImageButton5.Visible = true;
            //    lblPurchaseNo.Visible = true;
            //}
            //ddlItemType.Enabled = false;
            //ImageButton4.Enabled = false;
            //txtPurchaseNo.Enabled = false;
            //ImageButton5.Enabled = false;
            ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "CalculateTotal();", true);
        }
    }


    public void Clear()
    {
        ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "ClearAdd();", true);
        ViewState["TempTable"] = null;
      
        gvItems.DataSource = null;
        gvItems.DataBind();
        txtNetAmount.Text = "0";
        cbxIsClosed.Checked = false;
        divAdd.Style.Add("display", "block");
        divView.Style.Add("display", "none");
        lblSupplierBalance.Visible = false;
        lblCurrentStock.Text = "0";
        lblOrderedQuantity.Text = "0";
        btnSmartFill.Enabled = true;
        txtSupplierName.Enabled = true;
        ddlItemType.Enabled = true;
        ImageButton4.Enabled = true;
        //ddlStore.SelectedValue = Session["mainstore"].ToString();
        //ddlStore.Enabled = false;
        if (Convert.ToInt64(ddlItemType.SelectedValue) == 2)
        {
            txtPurchaseNo.Visible = true;
            ImageButton5.Visible = true;
            lblPurchaseNo.Visible = true;
            txtPurchaseNo.Enabled = true;
            ImageButton5.Enabled = true;
        }
        else
        {
            txtPurchaseNo.Visible = false;
            ImageButton5.Visible = false;
            lblPurchaseNo.Visible = false;
            txtPurchaseNo.Enabled = false;
            ImageButton5.Enabled = false;

        }
      
       
       

    }
    #endregion
   
    #region Events
    protected void Page_Load(object sender, EventArgs e)
    {
        //blMessage.Items.Clear();
        try
        {
            if (!IsPostBack)
            {//txtTotalAmount
                ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "ClearSearch();", true);
                SetReadonly(txtDate, txtDelivaryDate, txtTotalAmount, txtNetAmount, txtFromDate, txtTodate,txtSalesPrice,txtSalesPrice2,txtSupplierName,txtItemShortName);
               
                Clear();
                divAdd.Style.Add("display", "block");
                //txtFromDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
                //txtTodate.Text = DateTime.Now.ToString("dd/MM/yyyy");
                
                hfuserName.Value = Session["Username"].ToString();
                hfUser.Value = Request.QueryString["uid"];
                hfClient.Value = Request.QueryString["clnt"];
                CheckUserAccess();
                BindData();
                if (Session["mainstore"] != null)
                {
                    ddlStore.SelectedValue = Session["mainstore"].ToString();
                }
                ddlStore.Enabled = false;

                txtPurchaseNo.Visible = false;
                lblPurchaseNo.Visible = false;
                ImageButton5.Visible = false;
                btnSmartFill.Enabled = true;
                ddlItemType.Enabled = true;
                ddlSupplier.Enabled = true;
                txtPurchaseNo.Enabled = true;
                ImageButton5.Enabled = true;
                txtSupplierName.Enabled = true;
                ImageButton4.Enabled = true;

                if (Convert.ToInt64(ddlItemType.SelectedValue) == 2)
                {
                    txtPurchaseNo.Visible = true;
                    lblPurchaseNo.Visible = true;
                    ImageButton5.Visible = true;
                }
                GridFill(1);
                txtDate.Focus();
                lblCurrentStock.Enabled = false;
                lblOrderedQuantity.Enabled = false;
                if (Convert.ToBoolean(hfIsAdd.Value) == false)
                {
                    ((Label)CommonPopupPrivilege.FindControl("popuplabel")).Text = "User does not have permission to Add";
                    mpPrivilege.Show();
                }
                if (Convert.ToBoolean(hfIsUpdate.Value) == false)
                {
                    gvPurchaseOrder.Columns[10].Visible = false;
                }
                if (Convert.ToBoolean(hfIsDelete.Value) == false)
                {
                    gvPurchaseOrder.Columns[11].Visible = false;
                }
            }
        
        }
        catch (Exception ex)
        {
            //blMessage.Items.Add(ex.Message);
            ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "toastr.warning('" + ex.Message + "')", true);
        }
    }

    
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        try
        {
            divAdd.Style.Add("display", "block");
            divView.Style.Add("display", "none");
            if (hfItem.Value.ToString().Trim() == txtItemShortName.Text.ToString().Trim())
            {
                AddData();
               
            }
            else
            {
                ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "toastr.warning('Item  does not exist')", true);

            }
            txtRemark.Focus();
            divAdd.Style.Add("display", "block");
            divView.Style.Add("display", "none");
        }
        catch (Exception ex)
        {
            //blMessage.Items.Add(ex.Message);
            ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "toastr.warning('" + ex.Message + "')", true);
        }
    }
   
    protected void btnClear_Click(object sender, EventArgs e)
    {
       // blMessage.Items.Clear();
        try
        {
            Clear();
        }
        catch (Exception ex)
        {
            //blMessage.Items.Add(ex.Message);
            ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "toastr.warning('" + ex.Message + "')", true);
        }

    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        //blMessage.Items.Clear();
        try
        {
            if (gvItems.Rows.Count > 0)
            {
                Save();
                txtDate.Focus();
            }

            else
            {
                ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "alertify.alert('Atleast one item required')", true);
            }
        }
        catch (Exception ex)
        {
            //blMessage.Items.Add(ex.Message);
            ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "toastr.warning('" + ex.Message + "')", true);
        }
    }
    protected void gvItems_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
       // blMessage.Items.Clear();
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
                ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "CalculateTotal();", true);
                if (gvItems.Rows.Count > 0)
                {
                    btnSmartFill.Enabled = false;
                    txtSupplierName.Enabled = false;

                    if (Convert.ToInt64(ddlItemType.SelectedValue) == 2)
                    {
                        txtPurchaseNo.Visible = true;
                        ImageButton5.Visible = true;
                        lblPurchaseNo.Visible = true;
                    }
                    ddlItemType.Enabled = false;
                    ImageButton4.Enabled = false;
                    txtPurchaseNo.Enabled = false;
                    ImageButton5.Enabled = false;

                }
                else
                {
                    btnSmartFill.Enabled = true;
                    txtSupplierName.Enabled = true;
                    ddlItemType.Enabled = true;
                    ImageButton4.Enabled = true;

                    if (Convert.ToInt64(ddlItemType.SelectedValue) == 2)
                    {
                        txtPurchaseNo.Visible = true;
                        ImageButton5.Visible = true;
                        lblPurchaseNo.Visible = true;
                        txtPurchaseNo.Enabled = true;
                        ImageButton5.Enabled = true;
                    }
                    else
                    {
                        txtPurchaseNo.Visible = false;
                        ImageButton5.Visible = false;
                        lblPurchaseNo.Visible = false;
                    }
                }
              

            }
        }
        catch (Exception ex)
        {
            //blMessage.Items.Add(ex.Message);
        }
        ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "ShowAddDiv();", true);
        divAdd.Style.Add("display", "block");
        divView.Style.Add("display", "none");
        
    }
    protected void gvPurchaseOrder_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        //blMessage.Items.Clear();
        try
        {
            BlPurchaseOrder blpurchase = new BlPurchaseOrder();
            ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "ShowAddDiv();", true);
            blpurchase.FromDate = DateTime.ParseExact("01/01/1990", "dd/MM/yyyy", CultureInfo.InvariantCulture);
            blpurchase.ToDate = DateTime.ParseExact("01/01/9999", "dd/MM/yyyy", CultureInfo.InvariantCulture);
            blpurchase.FK_Supplier = Convert.ToInt64(ddlSupplierSearch.SelectedValue);

            blpurchase.DeliveryType = ddlDeliveryTypeSearch.SelectedIndex == 0 ? "" : ddlDeliveryTypeSearch.SelectedValue;
            blpurchase.isClosed = "";
            blpurchase.FK_Store = Convert.ToInt64(ddlStoreSearch.SelectedValue);
            blpurchase.MasterId = Convert.ToInt64(gvPurchaseOrder.DataKeys[e.NewSelectedIndex]["ID_PurchaseOrder"].ToString());
            blpurchase.FK_Client = Convert.ToInt64(hfClient.Value);
            hdfPurcahseOrder.Value = blpurchase.MasterId.ToString();
            blpurchase.SelectData();

            txtDate.Text = blpurchase.Date.ToString("dd/MM/yyyy");
            try
            {
                ddlSupplier.SelectedValue = blpurchase.FK_Supplier.ToString();
            }
            catch
            {
                ddlSupplier.SelectedValue = "0";
            }
            try
            {
                ddlStore.SelectedValue = blpurchase.FK_Store.ToString();
            }
            catch
            {
                ddlStore.SelectedValue = "0";
            }
            try
            {
                ddlDeliveryType.SelectedValue = blpurchase.DeliveryType;
            }
            catch
            {
                ddlDeliveryType.SelectedValue = "0";
            }
            try
            {
                ddlItemType.SelectedValue = blpurchase.FK_ItemType.ToString(); ;
            }
            catch
            {
                ddlItemType.SelectedValue = "0";
            }
            txtDelivaryDate.Text = blpurchase.DeliveryDate.ToString("dd/MM/yyyy");
            txtRemark.Text = blpurchase.Remarks;
            cbxIsClosed.Checked = blpurchase.Closed;
            txtTotalAmount.Text = blpurchase.totalAmount.ToString();
            txtDiscountLast.Text = blpurchase.Discount.ToString();
            //ddlOrderType.SelectedValue = blpurchase.FK_OrderType.ToString();
            txtCashAmount.Text = blpurchase.CashAmount.ToString();
            txtRoundAmount.Text = blpurchase.RoundAmount.ToString();
            txtAdvance.Text = blpurchase.Advance.ToString();
            txtNetAmount.Text = blpurchase.NetAmount.ToString();
            txtClosedRemark.Text = blpurchase.ClosedRemarks;
            DataTable dtbl = blpurchase.SelectPurchaseOrderItems();
            gvItems.DataSource = dtbl;
            gvItems.DataBind();
            ViewState["TempTable"] = dtbl;
            ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "CalculateTotal();", true); 

            //Supplier Name Selection (Nithin Das H)
            BlSupplier blsupplier = new BlSupplier();
            blsupplier.MasterId = blpurchase.FK_Supplier;
            blsupplier.FK_Client = Convert.ToInt64(hfClient.Value);
            blsupplier.SelectData();
            txtSupplierName.Text = blsupplier.SupplierName.ToString();
            if (Convert.ToInt64(ddlItemType.SelectedValue) == 2)
            {
                txtPurchaseNo.Visible = true;
                lblPurchaseNo.Visible = true;
                ImageButton5.Visible = true;
            }
            else
            {
                txtPurchaseNo.Visible = false;
                lblPurchaseNo.Visible = false;
                ImageButton5.Visible = false;
            }
            if (gvItems.Rows.Count > 0)
            {
                btnSmartFill.Enabled = false;
                ddlItemType.Enabled = false;
                txtSupplierName.Enabled = false;
                ImageButton4.Enabled = false;
                txtPurchaseNo.Enabled = false;
                ImageButton5.Enabled = false;
            }
            else
            {
                btnSmartFill.Enabled = true;
                ddlItemType.Enabled = true;
                txtSupplierName.Enabled = true;
                ImageButton4.Enabled = true;
                txtPurchaseNo.Enabled = true;
                ImageButton5.Enabled = true;
            }
            divAdd.Style.Add("display", "block");
            divView.Style.Add("display", "None");
            blsupplier = null;          
            blpurchase = null;
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "toastr.warning('" + ex.Message + "')", true);
        }
    }
    protected void gvPurchaseOrder_RowDeleting(object sender, GridViewDeleteEventArgs e)
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
                BlPurchaseOrder blpurchase = new BlPurchaseOrder();
                blpurchase.MasterId = Convert.ToInt64(gvPurchaseOrder.DataKeys[e.RowIndex]["ID_PurchaseOrder"].ToString());
                hfMasterID.Value = (gvPurchaseOrder.DataKeys[e.RowIndex]["ID_PurchaseOrder"].ToString());
                blpurchase.CancelledUser = hfuserName.Value;
                blpurchase.FK_Client = Convert.ToInt64(hfClient.Value);
                blpurchase.CancelledOn = DateTime.Now;
                blpurchase = null;
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
    protected void imgbtnView_Click(object sender, ImageClickEventArgs e)
    {
        //blMessage.Items.Clear();
        try
        {
           // blMessage.Items.Clear();

            ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "ClearAdd();  return false;", true);
            GridFill(1);
            divAdd.Style.Add("display", "none");
            divView.Style.Add("display", "block");
            ddlStoreSearch.Focus();
        }
        catch (Exception ex)
        {
            //blMessage.Items.Add(ex.Message);
            ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "toastr.warning('" + ex.Message + "')", true);
        }
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
       // blMessage.Items.Clear();
       // ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "ClearSearch();", true);
        try
        {
           // blMessage.Items.Clear();
            GridFill(1);
        }
        catch (Exception ex)
        {
            //blMessage.Items.Add(ex.Message);
            ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "toastr.warning('" + ex.Message + "')", true);
        }
    }
    protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
    {
       // blMessage.Items.Clear();
        try
        {
            
            Clear();
            if (Convert.ToBoolean(hfIsAdd.Value) == false)
            {
                ((Label)CommonPopupPrivilege.FindControl("popuplabel")).Text = "User does not have permission to Add";
                mpPrivilege.Show();
            }
            //ddlStore.SelectedValue = Session["mainstore"].ToString();
            //ddlStore.Enabled = false;
            divAdd.Style.Add("display", "block");
            divView.Style.Add("display", "none");
        }
        catch (Exception ex)
        {
            //blMessage.Items.Add(ex.Message);
            ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "toastr.warning('" + ex.Message + "')", true);
        }
    }
    //protected void btnItem_Click(object sender, EventArgs e)
    //{
    //    ddlItem_SelectedIndexChanged(sender, e);
    //}
    protected void ddlItem_SelectedIndexChanged(object sender, EventArgs e)
    {
       // try
        //{

        //    if (ddlItem.SelectedIndex > 0)
        //    {
        //        ddlItemsFill();
              
        //    }
        //    divAdd.Style.Add("display", "block");
        //    divView.Style.Add("display", "none");
        //    ddlUnit.Focus();
            
        //}
        //catch (Exception ex)
        //{
        //    ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "toastr.warning('" + ex.Message + "')", true);
        //}
        //ddlItem.Focus();
    }
    protected void gvPurchaseOrder_RowCommand(object sender, GridViewCommandEventArgs e)
    {


        try
        {
           
                BlPurchaseOrder blPurchaseOrder = new BlPurchaseOrder();
                long status = 0;
                string Sts = "closed";
                if (e.CommandName == "StatusUpdate")
                {
                    string confirmValue = Request.Form["confirm_value"];
                    if (confirmValue.Length > 3)
                        confirmValue = confirmValue.Substring(confirmValue.Length - 3, 3);
                    if (confirmValue == "Yes")
                    {

                        GridViewRow row = (GridViewRow)((Button)e.CommandSource).NamingContainer;
                        int index = row.RowIndex;
                        string PurchaseOrderId = gvPurchaseOrder.DataKeys[index]["ID_PurchaseOrder"].ToString();
                        if (PurchaseOrderId != null || PurchaseOrderId != "0")
                        {
                            blPurchaseOrder.MasterId = Convert.ToInt64(PurchaseOrderId);
                            blPurchaseOrder.isClosed = "";
                            blPurchaseOrder.FromDate = DateTime.ParseExact("01/01/1990", "dd/MM/yyyy", CultureInfo.InvariantCulture);
                            blPurchaseOrder.ToDate = DateTime.ParseExact("01/01/9999", "dd/MM/yyyy", CultureInfo.InvariantCulture);
                            blPurchaseOrder.DeliveryType = "";
                            blPurchaseOrder.FK_Client = Convert.ToInt64(hfClient.Value);
           
                            if (blPurchaseOrder.MasterId > 0)
                            {

                                blPurchaseOrder.SelectData();
                                if (blPurchaseOrder.Closed == true)
                                {
                                    blPurchaseOrder.Closed = false;
                                    blPurchaseOrder.ClosedUser = "";
                                    blPurchaseOrder.EnteredOn = DateTime.Now;
                                    blPurchaseOrder.UserCode = hfUser.Value;
                                    Sts = "Opened";

                                }
                                else if (blPurchaseOrder.Closed == false)
                                {
                                    blPurchaseOrder.Closed = true;
                                    blPurchaseOrder.ClosedUser = hfUser.Value;
                                    blPurchaseOrder.EnteredOn = DateTime.Now;
                                    blPurchaseOrder.UserCode = hfUser.Value;
                                    Sts = "Closed";
                                }


                                status = blPurchaseOrder.UpdatePurchaseOrderStatusOnly();
                                if (status > 0)
                                {
                                    GridFill(1);
                                    ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "toastr.success('Status " + Sts + " successfully');", true);

                                }
                                else if (status == -7)
                                {

                                    ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "toastr.error('Cannot update Status, Purchase already done..');", true);

                                }

                                else
                                {
                                    ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "toastr.error('Cannot update Status')", true);

                                }
                            }

                        }
                    }
                }
                if (e.CommandName == "Print")
                {
                    //string confirmValue = Request.Form["confirm_value"];
                    //  if (confirmValue.Length > 3)
                    //      confirmValue = confirmValue.Substring(confirmValue.Length - 3, 3);
                    //  if (confirmValue == "Yes")
                    //  {

                    GridViewRow row = (GridViewRow)((ImageButton)e.CommandSource).NamingContainer;
                    int index = row.RowIndex;
                    string PurchaseOrderId = gvPurchaseOrder.DataKeys[index]["ID_PurchaseOrder"].ToString();
                    string popupreport = "<script> window.open('PurchaseOrderPrint.aspx?PurchaseOrderPrintID=" + PurchaseOrderId + "', 'self', 'left=200,top=50,width=900,height=680,status=0,resizable=0,fullscreen=0,scrollbars=1');</script>";
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "Success", popupreport, false);
                    // }

                }
                blPurchaseOrder = null;
            

        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "toastr.warning('" + ex.Message + "')", true);
        }

    }
    protected void ddlSupplier_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlSupplier.SelectedIndex > 0)
            {
                    lblSupplierBalance.Visible = true;
                BlAccountHeadSubTransactions blaccountheadsubtransactions = new BlAccountHeadSubTransactions();
                blaccountheadsubtransactions.FK_Party = Convert.ToInt64(ddlSupplier.SelectedValue.ToString());
                blaccountheadsubtransactions.SubId = "SUPP";
                blaccountheadsubtransactions.FK_Client = Convert.ToInt64(hfClient.Value);
                blaccountheadsubtransactions.SelectCustBalanceAmount();
                lblSupplierBalance.Text = "";
                if (Convert.ToDecimal(blaccountheadsubtransactions.BalanceAmount.ToString()) >= 0)
                {
                    lblSupplierBalance.Text = "Outstanding Credit Amount = " + blaccountheadsubtransactions.BalanceAmount.ToString();
                }
                else
                {
                    lblSupplierBalance.Text = "Outstanding Debit Amount = " + Math.Abs(Convert.ToDecimal(blaccountheadsubtransactions.BalanceAmount.ToString()));
                }
                blaccountheadsubtransactions = null;
                
            }
            else
            {
                lblSupplierBalance.Visible = false;
            }
          
            ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "ShowAddDiv();$('#<%#liSupplierBalance.ClientID%>').show();", true);
           
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "toastr.warning('" + ex.Message + "')", true);
        }
    }
    protected void btnSmartFill_Click(object sender, EventArgs e)
    {
        try
        {
            ////if (ddlStore.SelectedIndex > 0)
            ////{
                if (ddlSupplier.SelectedIndex > 0)
                {
                    Smartill();
                }
            ////}

        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "toastr.warning('" + ex.Message + "')", true);
        }
    }



    protected void btnHiddenItemsQuantityChange_Click(object sender, EventArgs e)
    {
        try
        {
            DataTable dt = new DataTable();
            DataTable dtbl = (DataTable)ViewState["TempTable"];
            if (dtbl == null || gvItems.Rows.Count == 0) return;

            dt.Columns.Add("Item"); dt.Columns.Add("FK_Items"); dt.Columns.Add("Quantity"); dt.Columns.Add("PurchaseRate");
            dt.Columns.Add("Amount"); dt.Columns.Add("FK_Units"); dt.Columns.Add("Units"); dt.Columns.Add("CurrentStock");
            dt.Columns.Add("FK_SubUnit"); dt.Columns.Add("SubUnit");

            for (int i = 0; i < gvItems.Rows.Count; i++)
            {
                string qty = ((TextBox)gvItems.Rows[i].FindControl("txtQuantity")).Text;
                string rate = ((TextBox)gvItems.Rows[i].FindControl("txtPurchaseRategv")).Text;
                qty = string.IsNullOrEmpty(qty) ? "0" : qty;
                rate = string.IsNullOrEmpty(rate) ? "0" : rate;

                DataRow drow = dt.NewRow();
                drow["Item"] = ((Label)gvItems.Rows[i].FindControl("lblItemNameGv")).Text;
                drow["FK_Items"] = dtbl.Rows[i]["FK_Items"];
                drow["FK_Units"] = dtbl.Rows[i]["FK_Units"];
                drow["Units"] = dtbl.Rows[i]["Units"];
                drow["CurrentStock"] = dtbl.Rows[i]["CurrentStock"];
                drow["FK_SubUnit"] = dtbl.Columns.Contains("FK_SubUnit") ? dtbl.Rows[i]["FK_SubUnit"] : (object)0;
                drow["SubUnit"] = dtbl.Columns.Contains("SubUnit") ? dtbl.Rows[i]["SubUnit"] : (object)DBNull.Value;
                drow["Quantity"] = qty;
                drow["PurchaseRate"] = Math.Round(Convert.ToDecimal(rate), 4);
                drow["Amount"] = Math.Round(Convert.ToDecimal(qty) * Convert.ToDecimal(rate), 4);
                dt.Rows.Add(drow);
            }

            gvItems.DataSource = dt;
            gvItems.DataBind();
            ViewState["TempTable"] = dt;

            divAdd.Style.Add("display", "block");
            divView.Style.Add("display", "none");
            ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "focusItem()", true);
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "toastr.warning('" + ex.Message.Replace("'", "\\'") + "')", true);
        }
    }

    protected void btnHiddenItemsPurchaseRateChange_Click(object sender, EventArgs e)
    {
        try
        {
            DataTable dt = new DataTable();
            DataTable dtbl = (DataTable)ViewState["TempTable"];
            if (dtbl == null || dtbl.Rows.Count == 0) return;

            dt.Columns.Add("Item"); dt.Columns.Add("FK_Items"); dt.Columns.Add("Quantity"); dt.Columns.Add("PurchaseRate");
            dt.Columns.Add("Amount"); dt.Columns.Add("FK_Units"); dt.Columns.Add("Units"); dt.Columns.Add("CurrentStock");
            dt.Columns.Add("FK_SubUnit"); dt.Columns.Add("SubUnit");

            for (int i = 0; i < dtbl.Rows.Count; i++)
            {
                string qty = ((TextBox)gvItems.Rows[i].FindControl("txtQuantity")).Text;
                string rate = ((TextBox)gvItems.Rows[i].FindControl("txtPurchaseRategv")).Text;
                qty = string.IsNullOrEmpty(qty) ? "0" : qty;
                rate = string.IsNullOrEmpty(rate) ? "0" : rate;

                DataRow drow = dt.NewRow();
                drow["Item"] = ((Label)gvItems.Rows[i].FindControl("lblItemNameGv")).Text;
                drow["FK_Items"] = dtbl.Rows[i]["FK_Items"];
                drow["FK_Units"] = dtbl.Rows[i]["FK_Units"];
                drow["Units"] = dtbl.Rows[i]["Units"];
                drow["CurrentStock"] = dtbl.Rows[i]["CurrentStock"];
                drow["FK_SubUnit"] = dtbl.Columns.Contains("FK_SubUnit") ? dtbl.Rows[i]["FK_SubUnit"] : (object)0;
                drow["SubUnit"] = dtbl.Columns.Contains("SubUnit") ? dtbl.Rows[i]["SubUnit"] : (object)DBNull.Value;
                drow["Quantity"] = qty;
                drow["PurchaseRate"] = Math.Round(Convert.ToDecimal(rate), 4);
                drow["Amount"] = Math.Round(Convert.ToDecimal(qty) * Convert.ToDecimal(rate), 4);
                dt.Rows.Add(drow);
            }

            gvItems.DataSource = dt;
            gvItems.DataBind();
            ViewState["TempTable"] = dt;
            ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "CalculateTotal();", true); 
            divAdd.Style.Add("display", "block");
            divView.Style.Add("display", "none");
            if (hfQtyId.Value == "LastRow") txtRemark.Focus();
            else ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "focusItem()", true);
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "toastr.warning('" + ex.Message.Replace("'", "\\'") + "')", true);
        }
    }
    #endregion   
   
   
    protected void ImageButton4_Click(object sender, ImageClickEventArgs e)
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
        ddlItemType.Focus();
      
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
            divView.Style.Add("display", "none");
            ddlItemType.Focus();
        }
        catch (Exception ex)
        {
        }
    }

    private void ClearSupplier()
    {     
        txtSupplierName.Text = "";
        ddlSupplier.SelectedIndex = 0;
    }

    public void SetDetails(string strName, string strRefno)
    {
        if (CommonPopup1.CallingFunction == "SupplierFill")
        {
            txtSupplierName.Text = strName;        
            ddlSupplier.SelectedValue = strRefno.ToString();
           // ddlOrderType.Focus();
         
        }
      
    }
    protected void gvPurchaseOrder_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                 Button btnStatus = (Button)e.Row.FindControl("btnStatus");               
                if (btnStatus.Text == "CLOSED")
                {                  
                    btnStatus.BackColor = System.Drawing.Color.Red;
                }
                else
                {                 
                    btnStatus.BackColor = System.Drawing.Color.Green;

                }
              
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "toastr.warning('" + ex.Message + "')", true);
        }
        
    }
    protected void gvPurchaseOrder_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvPurchaseOrder.PageIndex = e.NewPageIndex;
        GridFill(e.NewPageIndex);
    }
    protected void btnPurchaseNo_Click(object sender, EventArgs e)
    {
        if (Convert.ToInt64(ddlItemType.SelectedValue) == 2)
        {
            txtPurchaseNo.Visible = true;
           
        }

    }

    protected void ddlItemType_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (Convert.ToInt64(ddlItemType.SelectedValue) == 2)
        {
            txtPurchaseNo.Visible = true;
            lblPurchaseNo.Visible = true;
            ImageButton5.Visible = true;
            txtPurchaseNo.Enabled = true;
            ImageButton5.Enabled = true;
        }
        else
        {
            txtPurchaseNo.Visible = false;
            lblPurchaseNo.Visible = false;
            ImageButton5.Visible = false;
        }

    }
    protected void btnItem_Click(object sender, EventArgs e)
    {
        ddlItemsFill();
    }
    protected void ImageButton2_Click(object sender, ImageClickEventArgs e)
    {
        Delete();
    }
    //protected void gvItems_PageIndexChanging(object sender, GridViewPageEventArgs e)
    //{
    //    gvItems.PageIndex = e.NewPageIndex;
    //    gvItems.DataSource = ViewState["TempTable"];
    //    gvItems.DataBind();
    //}
    //protected void gvItems_PageIndexChanging(object sender, GridViewPageEventArgs e)
    //{
    //    gvItems.PageIndex = e.NewPageIndex;
    //    gvItems.DataSource = ViewState["TempTable"];
    //    gvItems.DataBind();
    //}
}