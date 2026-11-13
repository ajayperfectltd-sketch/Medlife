/*----------------------------------------------------------------------------------------------------------------------------------------------------------
Purpose    : Insert/Update/Delete Purchase Order  
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
using WebPharma.Business.Accounts;
using System.Data;
using System.Collections;
using System.Globalization;


public partial class StockTransferRequest : System.Web.UI.Page
{

    #region Functions
    
    public void BindData()
    {
        Populate populate = new Populate();
        populate.FillDropdownList(ddlStore, "Store", "Name", "ID_Store", "Name", " Active=1 and FK_Client = " + hfClient.Value, "");
        populate.FillDropdownList(ddlStoreSearch, "Store", "Name", "ID_Store", "Name", " Active=1 and FK_Client = " + hfClient.Value, "");

        populate.FillDropdownList(ddlItem, "Items", "Name", "ID_Items", "Name", "  Active='True' AND ItemType=2 AND FK_Client= " + hfClient.Value.Split(',')[0], "");
      
      
        populate = null;

    }
    public void SetReadonly(params TextBox[] textbox)
    {
        foreach (TextBox txt in textbox)
            txt.Attributes.Add("readonly", "readonly");

    }
    protected void GridFill(int currentPage)
    {
        
        BlIndentRequest blIndentRequest = new BlIndentRequest();
        DataTable dtbl = new DataTable();
        blIndentRequest.FromDate =Convert.ToDateTime(txtFromDate.Text);//DateTime.ParseExact(txtFromDate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
        blIndentRequest.ToDate = Convert.ToDateTime(txtTodate.Text);//DateTime.ParseExact(txtTodate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
       // blIndentRequest.isClosed = ddlClosedSearch.SelectedValue;
        blIndentRequest.FK_Store = Convert.ToInt64(ddlStoreSearch.SelectedValue);
        blIndentRequest.PageSize = 10;
        currentPage = currentPage == 0 ? 1 : currentPage;
        blIndentRequest.PageIndex = Convert.ToInt16(currentPage);
        blIndentRequest.FK_Client = Convert.ToInt64(hfClient.Value);
        dtbl = blIndentRequest.SelectAllData();
        gvPurchaseOrder.DataSource = dtbl;
        gvPurchaseOrder.DataBind();
        if (dtbl.Rows.Count > 0)
        {
            int recordCount = Convert.ToInt32(dtbl.Rows[0]["RecordCount"].ToString());
            this.PopulatePager(recordCount, currentPage);

        }
        blIndentRequest = null;
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
    protected void CheckUserAccess()
    {
        BlUserAccess bluseraccess = new BlUserAccess();
        Populate populate = new Populate();

        bluseraccess.FK_Users = Convert.ToInt64(hfUser.Value);
        bluseraccess.FK_Pages = 75;
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
            dtbl.Columns.Add("Quantity");
            dtbl.Columns.Add("FK_Stock");      
           
 
          


        }
        if (dtbl.Rows.Count > 0)
        {

            for (int i = 0; i < dtbl.Rows.Count; i++)
            {
                if (dtbl.Rows[i]["FK_Items"].ToString() == ddlItem.SelectedValue.ToString() )
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
            drow["Quantity"] = txtQunatity.Text == "" ? "0" : txtQunatity.Text;
            drow["FK_Stock"] = hdfFKStock.Value == "" ? "0" : hdfFKStock.Value; 
         
   
            dtbl.Rows.InsertAt(drow, rowCount);
            gvItems.DataSource = dtbl;
            gvItems.DataBind();
            ViewState["TempTable"] = dtbl;
            txtItemShortName.Focus();
            ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "ItemClear();", true);
        }
        else
        {
            ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "alertify.alert('Item already exists')", true);
        }
    }

    protected void ddlItemsFill()
    {
        if (ddlItem.SelectedIndex > 0)
        {
            BlItems blitems = new BlItems();
            blitems.MasterId = Convert.ToInt64(ddlItem.SelectedValue.ToString());
            blitems.FK_Client = Convert.ToInt64(hfClient.Value);
            blitems.SelectData();
            //txtSalesPrice.Text = blitems.MinimumSalesPrice.ToString();
            //txtSalesPrice2.Text = blitems.SellingPrice2.ToString();         
         
            txtItemShortName.Text = blitems.Name.ToString();
            hfPacked.Value = blitems.Packed.ToString();
         

            DataTable dtbl= new DataTable();
          dtbl=  blitems.ItemsSelectCurrentStock();
          if (dtbl.Rows.Count > 0)
          {
              lblCurrentStock.Text = "Current Stock= "+ dtbl.Rows[0]["CurrentStock"].ToString();
              lblOrderedQuantity.Text = "Ordered Quantity= " + dtbl.Rows[0]["OrderedQuantity"].ToString();
          }
          else
          {
              lblCurrentStock.Text = "Current Stock= 0";
              lblOrderedQuantity.Text = "Ordered Quantity= 0 ";
          }
          blitems = null;
           
        }
  
    }

    public void Save()
    {
        long status = 0;
        BlIndentRequest blindentrequest = new BlIndentRequest();
      //  blindentrequest.Date = txtDate.Text == "" ? DateTime.Now : DateTime.ParseExact(txtDate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);      
      
        blindentrequest.Remarks = txtRemark.Text;     
        //blindentrequest.Approved = cbxIsClosed.Checked;

        //blindentrequest.ApprovedOn = DateTime.Now;
       // blindentrequest.ApprovedUser = hfUser.Value;
        //if (cbxIsClosed.Checked)
        //    blindentrequest.ClosedUser = hfUser.Value;
        //else
        //    blindentrequest.ClosedUser = "";
        blindentrequest.UserCode = hfUser.Value;
      //  blindentrequest.ClosedRemarks = txtClosedRemark.Text;
        blindentrequest.EnteredOn = DateTime.Now;
        blindentrequest.FK_Store = Convert.ToInt64(ddlStore.SelectedValue);
        blindentrequest.FK_Client = Convert.ToInt64(hfClient.Value);
        ArrayList arlist = new ArrayList();
        DataTable dtbl = (DataTable)ViewState["TempTable"];
        IIndentRequestDetails IIndentRequestDetails;
        for (int i = 0; i < dtbl.Rows.Count; i++)
        {
            IIndentRequestDetails = new IIndentRequestDetails();
            IIndentRequestDetails.FK_Items = Convert.ToInt64(dtbl.Rows[i]["FK_Items"].ToString());
            IIndentRequestDetails.Quantity = Convert.ToDecimal(dtbl.Rows[i]["Quantity"].ToString());
            IIndentRequestDetails.FK_Stock = Convert.ToInt64(dtbl.Rows[i]["FK_Stock"].ToString());

           
            arlist.Add(IIndentRequestDetails);
            IIndentRequestDetails = null;
        }

        blindentrequest.XmlPurchaseItems = Common.WriteToXml(arlist);


        if (hdfPurcahseOrder.Value == "")
        {
            if (hfIsAdd.Value.ToLower() == "true")
            {
                status = blindentrequest.InsertData();
                if (status > 0)
                {
                    //   //blMessage.Items.Add("Data inserted successfully");
                    Clear();
                    GridFill(1);
                    ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "ShowAddDiv();", true);
                    Populate.InsertMsg(this);

                }
                else 
                {
                    ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "ShowAddDiv();", true);
                    //  //blMessage.Items.Add("Cannot insert data,Item already exist in the Purchase Order.");
                    Populate.ErrorMessageBox(this, "Cannot insert data already done transfer.");
                    ddlItem.Focus();
                }
              
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "toastr.warning('User does not have permission to add')", true);

            }
        }
        else
        {
            blindentrequest.MasterId = Convert.ToInt64(hdfPurcahseOrder.Value);
            if (hfIsUpdate.Value.ToLower() == "true")
            {

                status = blindentrequest.UpdateData();
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
                    Populate.ErrorMessageBox(this, "Cannot update data,Indent already done");

                }
                else if (status == -3)
                {
                    ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "ShowAddDiv();", true);
                    //blMessage.Items.Add("Cannot update data,Item already exist in the Material Receipt.");
                    Populate.ErrorMessageBox(this, "Cannot update data,Alredy Approved ");

                }
                else if (status == -4)
                {
                    ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "ShowAddDiv();", true);
                    //blMessage.Items.Add("Cannot update data,Item already exist in the Material Receipt.");
                    Populate.ErrorMessageBox(this, "Cannot update data,Alredy Approved only closing available");

                }
                else
                {
                    ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "ShowAddDiv();", true);
                    //  //blMessage.Items.Add("Cannot insert data,Item already exist in the Purchase Order.");
                    Populate.ErrorMessageBox(this, "Cannot update data .referance exists ");
                   
                    ddlItem.Focus();
                }
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "toastr.warning('User does not have permission to Update')", true);
            }
        }



    }

    protected void Smartill()
    {


        BlPurchaseOrder blpurchase = new BlPurchaseOrder();
        DataTable dtbl = new DataTable();
        blpurchase.FK_Store = Convert.ToInt64(ddlStore.SelectedValue);
       
        blpurchase.FK_Client = Convert.ToInt64(hfClient.Value);

        dtbl = blpurchase.SelectPurchaseOrderSmartFillItems();
        gvItems.DataSource = dtbl;
        gvItems.DataBind();
        ViewState["TempTable"] = dtbl;
    }


    public void Clear()
    {
        ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "ClearAdd();", true);
        ViewState["TempTable"] = null;
      
        gvItems.DataSource = null;
        gvItems.DataBind();
        cbxIsClosed.Checked = false;
        divAdd.Style.Add("display", "block");
        divView.Style.Add("display", "none");
        lblSupplierBalance.Visible = false;
        lblCurrentStock.Text = "Current Stock= 0";
        lblOrderedQuantity.Text = "Ordered Quantity= 0 ";
        btnSave.Visible = true;
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
                SetReadonly(txtDate, txtDelivaryDate, txtTotalAmount, txtNetAmount, txtFromDate, txtTodate,txtSalesPrice,txtSalesPrice2);
               
                Clear();
                divAdd.Style.Add("display", "block");
                //txtFromDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
                //txtTodate.Text = DateTime.Now.ToString("dd/MM/yyyy");
               
                hfUser.Value = Request.QueryString["uid"];
                hfClient.Value = Request.QueryString["clnt"];
                CheckUserAccess();
                BindData();
                GridFill(1);
                txtDate.Focus();
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
            if (ddlItem.SelectedItem.ToString().Trim() == txtItemShortName.Text.ToString().Trim())
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
    protected void Page_Changed(object sender, EventArgs e)
    {
       // blMessage.Items.Clear();
        int pageIndex = int.Parse((sender as LinkButton).CommandArgument);

        GridFill(pageIndex);
      
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
                if (cbxPrint.Checked == true)
                {
                    Print();
                }
                else
                {

                }
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
    public void Print()
    {

        BlIndentRequest blIndentRequest = new BlIndentRequest();
        DataTable dt = blIndentRequest.SelectIntendRequestIDData();
        string PurchaseOrderId = dt.Rows[0]["ID_IndentRequest"].ToString();
        string popupreport = "<script> window.open('PrintReport.aspx?Type=STR&ID=" + PurchaseOrderId + "', 'self', 'left=200,top=50,width=900,height=680,status=0,resizable=0,fullscreen=0,scrollbars=1');</script>";
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "Success", popupreport, false);
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
            BlIndentRequest blIndentRequest = new BlIndentRequest();
            ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "ShowAddDiv();", true);
            blIndentRequest.FromDate = DateTime.ParseExact("01/01/1990", "dd/MM/yyyy", CultureInfo.InvariantCulture);
            blIndentRequest.ToDate = DateTime.ParseExact("01/01/9999", "dd/MM/yyyy", CultureInfo.InvariantCulture);
            blIndentRequest.MasterId = Convert.ToInt64(gvPurchaseOrder.DataKeys[e.NewSelectedIndex]["ID_IndentRequest"].ToString());
            blIndentRequest.FK_Client = Convert.ToInt64(hfClient.Value);
            hdfPurcahseOrder.Value = blIndentRequest.MasterId.ToString();
            blIndentRequest.SelectData();


           // txtDate.Text = blIndentRequest.Date.ToString("dd/MM/yyyy");
          
            try
            {
                ddlStore.SelectedValue = blIndentRequest.FK_Store.ToString();
            }
            catch
            {
                ddlStore.SelectedValue = "0";
            }
           
          //  txtDelivaryDate.Text = blIndentRequest.DeliveryDate.ToString("dd/MM/yyyy");
            txtRemark.Text = blIndentRequest.Remarks;
           // cbxIsClosed.Checked = blIndentRequest.Posted;
            txtTotalAmount.Text = blIndentRequest.totalAmount.ToString();
           // txtDiscountLast.Text = blIndentRequest.Discount.ToString();
           

           // txtCashAmount.Text = blIndentRequest.CashAmount.ToString();
            txtRoundAmount.Text = blIndentRequest.RoundAmount.ToString();
           // txtAdvance.Text = blIndentRequest.Advance.ToString();
            txtNetAmount.Text = blIndentRequest.NetAmount.ToString();
          //  txtClosedRemark.Text = blIndentRequest.ClosedRemarks;
            DataTable dtbl = blIndentRequest.SelectIndentRequestItems();
            gvItems.DataSource = dtbl;
            gvItems.DataBind();
            ViewState["TempTable"] = dtbl;
            blIndentRequest = null;
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
                BlIndentRequest blIndentRequest = new BlIndentRequest();
                blIndentRequest.MasterId = Convert.ToInt64(gvPurchaseOrder.DataKeys[e.RowIndex]["ID_IndentRequest"].ToString());
                blIndentRequest.CancelledUser = hfUser.Value;
                blIndentRequest.FK_Client = Convert.ToInt64(hfClient.Value);
                blIndentRequest.CancelledOn = DateTime.Now;
                if (hfIsDelete.Value.ToLower() == "true")
                {
                    status = blIndentRequest.DeleteData();
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
                blIndentRequest = null;
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
            divAdd.Style.Add("display", "block");
            divView.Style.Add("display", "none");
        }
        catch (Exception ex)
        {
            //blMessage.Items.Add(ex.Message);
            ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "toastr.warning('" + ex.Message + "')", true);
        }
    }
    protected void btnItem_Click(object sender, EventArgs e)
    {
        ddlItem_SelectedIndexChanged(sender, e);
    }
    protected void ddlItem_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {

            if (ddlItem.SelectedIndex > 0)
            {
                ddlItemsFill();
                txtQunatity.Focus();
            }
            divAdd.Style.Add("display", "block");
            divView.Style.Add("display", "none");
          
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "toastr.warning('" + ex.Message + "')", true);
        }
     
    }
    protected void gvPurchaseOrder_RowCommand(object sender, GridViewCommandEventArgs e)
    {


        try
        {
           
                BlIndentRequest blIndentRequest = new BlIndentRequest();
                long status = 0;
                string Sts = "Open";
                if (e.CommandName == "StatusUpdate")
                {
                    string confirmValue = Request.Form["confirm_value"];
                    if (confirmValue.Length > 3)
                        confirmValue = confirmValue.Substring(confirmValue.Length - 3, 3);
                    if (confirmValue == "Yes")
                    {

                        GridViewRow row = (GridViewRow)((Button)e.CommandSource).NamingContainer;
                        int index = row.RowIndex;
                        string PurchaseOrderId = gvPurchaseOrder.DataKeys[index]["ID_IndentRequest"].ToString();
                        if (PurchaseOrderId != null || PurchaseOrderId != "0")
                        {
                            blIndentRequest.MasterId = Convert.ToInt64(PurchaseOrderId);
                            blIndentRequest.FromDate = DateTime.ParseExact("01/01/1990", "dd/MM/yyyy", CultureInfo.InvariantCulture);
                            blIndentRequest.ToDate = DateTime.ParseExact("01/01/9999", "dd/MM/yyyy", CultureInfo.InvariantCulture);

                            blIndentRequest.FK_Client = Convert.ToInt64(hfClient.Value);

                            if (blIndentRequest.MasterId > 0)
                            {

                                blIndentRequest.SelectData();
                                //if (blIndentRequest.Approved == false)
                                //{
                                //    //blIndentRequest.Approved = true;
                                //   // blIndentRequest.ApprovedUser = hfUser.Value;
                                //    blIndentRequest.EnteredOn = DateTime.Now;
                                //    blIndentRequest.UserCode = hfUser.Value;
                                //    Sts = "Approved";
                                //}
                                //if (blIndentRequest.Approved == true)
                                //{
                                //    blIndentRequest.Approved = false;
                                //    blIndentRequest.ApprovedUser = "";
                                //    blIndentRequest.EnteredOn = DateTime.Now;
                                //    blIndentRequest.UserCode = hfUser.Value;
                                //    Sts = "Closed";
                                //}       


                                status = blIndentRequest.UpdateIndentRequestStatusOnly();
                                if (status > 0)
                                {
                                    GridFill(1);
                                    ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "toastr.success('Status " + Sts + " successfully');", true);

                                }
                                else if (status == -7)
                                {

                                    ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "toastr.error('Cannot update Status, Transfer already done..');", true);

                                }

                                else
                                {
                                    ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "toastr.error('Cannot update Status')", true);

                                }
                            }

                        }
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
                    string PurchaseOrderId = gvPurchaseOrder.DataKeys[index]["ID_IndentRequest"].ToString();

                    string popupreport = "<script> window.open('PrintReport.aspx?Type=STR&ID=" + PurchaseOrderId + "', 'self', 'left=200,top=50,width=900,height=680,status=0,resizable=0,fullscreen=0,scrollbars=1');</script>";
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "Success", popupreport, false);

                    // }
                }
                blIndentRequest = null;
            

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
            if (ddlStore.SelectedIndex > 0)
            {
                //if (ddlSupplier.SelectedIndex > 0)
                //{
                    Smartill();
               // }
            }

        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "toastr.warning('" + ex.Message + "')", true);
        }
    }



    protected void gvPurchaseOrder_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            //
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                // For `Approve` button
                Button btnapprove = (Button)e.Row.FindControl("btnStatus");
                ImageButton btnDelete = (ImageButton)e.Row.FindControl("ImageButton269");
                ImageButton btnEdit = (ImageButton)e.Row.FindControl("ImageButton145");
                // give property id of button form template field
                if (btnapprove.Text == "OPEN")
                {
                 
                    btnEdit.Enabled = true;
                    btnapprove.Enabled = true;
                    btnDelete.Enabled = true;
                    btnapprove.BackColor = System.Drawing.Color.DarkGreen;//true means enable else you may set false to disable button 
                }
                else if (btnapprove.Text == "APPROVED")
                {
                    btnapprove.Enabled = false;
                    btnDelete.Enabled = false;
                    btnEdit.Enabled = true;
                    btnapprove.BackColor = System.Drawing.Color.Orange;
                 
                }
                else 
                {
                    btnapprove.Enabled = false;
                    btnDelete.Enabled = false;
                    btnEdit.Enabled = true;
                    btnapprove.BackColor = System.Drawing.Color.Red;

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
   
    #endregion   
   
   
}