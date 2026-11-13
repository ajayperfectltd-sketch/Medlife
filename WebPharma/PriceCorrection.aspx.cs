using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebPharma.Business.Masters;
using WebPharma.Business.WebPharma;

public partial class PriceCorrection : System.Web.UI.Page
{
    #region Functions
    protected void CheckUserAccess()
    {
        BlUserAccess bluseraccess = new BlUserAccess();
        Populate populate = new Populate();

        bluseraccess.FK_Users = Convert.ToInt64(hfUser.Value);
        bluseraccess.FK_Pages = 52;
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
            BlPriceCorrection blPrice = new BlPriceCorrection();
            blPrice.UserCode = hfuserName.Value;
            blPrice.MasterId = Convert.ToInt64(hfMasterID.Value);
            blPrice.FK_Reason = Convert.ToInt64(Reason);
            if (hfIsDelete.Value.ToLower() == "true")
            {
                status = blPrice.DeletePriceCorrection();
                if (status > 0)
                {
                    GridFill(1);
                    ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "toastr.success('Data Deleted Successfully')", true);
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
            blPrice = null;
        }
    }
    protected void BindData()
    {
        Populate populate = new Populate();
        populate.FillDropdownList(ddlItem, "Items", "ItemName", "ID_Items", "ItemName", "Cancelled=0 AND FK_Client= " + hfClient.Value.Split(',')[0], "");
        populate.FillDropdownList(ddlStore, "Store", "StoName", "ID_Store", "StoName", " Active='True'  AND Cancelled=0 AND FK_Client= " + hfClient.Value.Split(',')[0], "");
        populate.FillDropdownList(ddlCat, "Category", "CategoryName", "ID_Category", "CategoryName", "Cancelled=0 and  FK_Client= " + hfClient.Value.Split(',')[0], "");
        populate.FillDropdownList(ddlSearchStore, "Store", "StoName", "ID_Store", "StoName", " Active='True'  AND Cancelled=0 AND FK_Client= " + hfClient.Value.Split(',')[0], "");
    }
    private DataTable GetDataTable(DataTable dtbl)
    {
        dtbl.Columns.Add("slno");
        dtbl.Columns.Add("Item");
        dtbl.Columns.Add("FK_Items");
        dtbl.Columns.Add("Quantity");
        dtbl.Columns.Add("NewMRP");
        dtbl.Columns.Add("SalesPrice");
        dtbl.Columns.Add("NewSalesPrice");
        dtbl.Columns.Add("FK_Stock");
        dtbl.Columns.Add("FK_Category");
        dtbl.Columns.Add("Category");
        dtbl.Columns.Add("OldMRP");
        
        return dtbl;
    }
    protected int AddDataItem()
    {
        DataTable dtbl = new DataTable();
        int inRowCount = 0;
        int inCheck = 0;
        if (ViewState["GridData"] != null)
        {
            dtbl = (DataTable)ViewState["GridData"];
            inRowCount = dtbl.Rows.Count;
        }
        else
        {

            dtbl = GetDataTable(dtbl);
        }
        if (hdfItem.Value != "")
        {

            gvItems.Enabled = true;
            dtbl.Rows.RemoveAt(Convert.ToInt32(hdfItem.Value));
            gvItems.DataSource = dtbl;
            gvItems.DataBind();
            hfSlNo.Value = hfItemId.Value;
            hdfItem.Value = "";
            // ViewState["TempTable"] = dtbl;


        }
        if (dtbl.Rows.Count > 0)
        {

            for (int i = 0; i < dtbl.Rows.Count; i++)
            {
                if (dtbl.Rows[i]["FK_Stock"].ToString() == hdfFKStock.Value.Split(',')[0])
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

            decimal MRP = Math.Round(Convert.ToDecimal(HdnOldMRP.Value == "" ? "0" : HdnOldMRP.Value), 4);
            decimal NewMRP = Math.Round(Convert.ToDecimal(txtMRP.Text == "" ? "0" : txtMRP.Text), 4);
            decimal SalesPrice = Math.Round(Convert.ToDecimal(txtSalesPrice.Text == "" ? "0" : txtSalesPrice.Text), 4);
            decimal NewSalesPrice = Math.Round(Convert.ToDecimal(txtNewSalePrice.Text == "" ? "0" : txtNewSalePrice.Text), 4);

            decimal decqty = Math.Round(Convert.ToDecimal(txtQuantity.Text == "" ? "0" : txtQuantity.Text), 4);
           // decimal Total = (decqty * PurchaseRate) + ((PurVAT / 100) * (decqty * PurchaseRate)) + ((CESS / 100) * (decqty * PurchaseRate));

           
            drow["Item"] = hfItem.Value;
            drow["FK_Items"] = hdfFkItem.Value;
            drow["Quantity"] = decqty;
            drow["NewSalesPrice"] = NewSalesPrice;
            drow["NewMRP"] = NewMRP;
            drow["SalesPrice"] = SalesPrice;
            drow["FK_Stock"] = hdfFKStock.Value;
            drow["FK_Category"] = ddlCat.SelectedValue.ToString();
            drow["Category"] = ddlCat.SelectedItem.ToString();
            drow["OldMRP"] = MRP;
            dtbl.Rows.InsertAt(drow, inRowCount);
            gvItems.DataSource = dtbl;
            gvItems.DataBind();
           // TotalAmount(dtbl);
            ViewState["GridData"] = dtbl;
           

        }
        else
        {
            ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "alertify.alert('Item already exists')", true);
            return 1;
        }
        return 0;

    }
    public void Save()
    {
        DataTable dtbl = (DataTable)ViewState["GridData"];
        if (dtbl != null && dtbl.Rows.Count > 0)
        {
            long status = 0;
            BlPriceCorrection blprice = new BlPriceCorrection();
            blprice.PriceCorrectionDate = txtDate.Text == "" ? DateTime.Now : DateTime.ParseExact(txtDate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);

            blprice.UserCode = hfuserName.Value;
            blprice.EnteredOn = DateTime.Now;
            blprice.FK_Store = Convert.ToInt64(ddlStore.SelectedValue);
            blprice.FK_Client = Convert.ToInt64(hfClient.Value);
            blprice.Cancelled = false;
            blprice.Cancelleduser = hfuserName.Value;
            string datetimes = DateTime.Now.ToShortDateString();
            blprice.EnteredOn = DateTime.ParseExact(datetimes, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            blprice.UserCode = hfuserName.Value;
            if (hfID_Price.Value != "")
            {
                blprice.MasterId = Convert.ToInt64(hfID_Price.Value);
            }

            ArrayList arlist = new ArrayList();

            IPricCrtionDetails PriceCrrtionDetails;


            for (int i = 0; i < dtbl.Rows.Count; i++)
            {
                PriceCrrtionDetails = new IPricCrtionDetails();

                PriceCrrtionDetails.SlNo = Convert.ToInt32(i + 1);
                PriceCrrtionDetails.FK_Item = Convert.ToInt64(dtbl.Rows[i]["FK_Items"].ToString());
                PriceCrrtionDetails.Quantity = Convert.ToDecimal(dtbl.Rows[i]["Quantity"].ToString());
                PriceCrrtionDetails.SalesPrice = Convert.ToDecimal(dtbl.Rows[i]["SalesPrice"].ToString());
                PriceCrrtionDetails.NewSalesPrice = Convert.ToDecimal(dtbl.Rows[i]["NewSalesPrice"].ToString());
                PriceCrrtionDetails.FK_Stock = Convert.ToInt64(dtbl.Rows[i]["FK_Stock"].ToString());
                PriceCrrtionDetails.MRP = Convert.ToDecimal(dtbl.Rows[i]["OldMRP"].ToString());
                PriceCrrtionDetails.NewMRP = Convert.ToDecimal(dtbl.Rows[i]["NewMRP"].ToString());
                arlist.Add(PriceCrrtionDetails);
                PriceCrrtionDetails = null;
            }
            blprice.xmlGriddata = Common.WriteToXml(arlist);
            if (hfID_Price.Value == "")
            {
                if (hfIsAdd.Value.ToLower() == "true")
                {
                    status = blprice.InsertData();
                    if (status > 0)
                    {
                        ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "toastr.success('Data Inserted Successfully');alertify.alert('Reference Number:  " + status + "');", true);


                        ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "ClearAdd();", true);
                        ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "ClearAdd();", true);
                        Clear();
                        Populate populate = new Populate();
                        try
                        {
                            ddlStore.SelectedValue = Session["mainstore"].ToString();
                            ddlStore.Enabled = false;
                        }
                        catch (Exception ex)
                        {
                        }

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

                    blprice.MasterId = Convert.ToInt64(hfID_Price.Value);
                    status = blprice.UpdateData();
                    if (status > 0)
                    {

                        ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "toastr.success('Data Updated Successfully')", true);

                        ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "ClearAdd();", true);
                        ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "ClearAdd();", true);

                        Populate populate = new Populate();
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
        }
        else
        {
            ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "toastr.warning('Add atleast one record to save!!')", true);

        }
    }

    public void Clear()
    {
        ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "ClearAdd();", true);
        ViewState["GridData"] = null;
        gvItems.DataSource = null;
        gvItems.DataBind();
    }
       
    #endregion Functions
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
                BindData();
                hfuserName.Value = Session["Username"].ToString();
                ddlStore.SelectedValue = Session["mainstore"].ToString();
                ddlStore.Enabled = false;
                hfStore.Value = Session["mainstore"].ToString();
                EnableItem(true);
                SetReadonly();
                txtDate.Focus();
                txtQuantity.Enabled = false;
                txtSalesPrice.Enabled = false;
                //txtMRP.Enabled = false;
                GridFill(1);
                if (Convert.ToBoolean(hfIsAdd.Value) == false)
                {
                    ((Label)CommonPopupPrivilege.FindControl("popuplabel")).Text = "User does not have permission to Add";
                    mpPrivilege.Show();
                }
                if (Convert.ToBoolean(hfIsUpdate.Value) == false)
                {
                    grdPrice.Columns[4].Visible = false;
                }
                if (Convert.ToBoolean(hfIsDelete.Value) == false)
                {
                    grdPrice.Columns[5].Visible = false;
                }
            }
            txtQuantity.Enabled = false;
            txtSalesPrice.Enabled = false;
            //txtMRP.Enabled = false;
        }
        catch (Exception ex)
        {
            Populate.WarningMsg(this, ex.ToString());
        }
    }
   
    protected void btnAddItems_Click(object sender, EventArgs e)
    {

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
                if (ViewState["GridData"] != null)
                {
                    DataTable dtbl = (DataTable)ViewState["GridData"];


                    dtbl.Rows.RemoveAt(e.RowIndex);
                    for (int i = 0; i < dtbl.Rows.Count; i++)
                    {
                        dtbl.Rows[i]["slno"] = i + 1;
                    }
                    gvItems.DataSource = dtbl;
                    gvItems.DataBind();
                    ViewState["GridData"] = dtbl;
                   
                }
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "toastr.warning('" + ex.Message + "')", true);
        }
    }
  

    protected void gvItems_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        if (gvItems.Rows.Count > 0)
        {
            DataTable dtbl = new DataTable();
            if (ViewState["GridData"] != null)
            {
                dtbl = (DataTable)ViewState["GridData"];
                ddlItem.SelectedValue = Convert.ToString(((Label)gvItems.Rows[e.NewSelectedIndex].FindControl("lblgvFK_Items")).Text);
                txtitem.Text = Convert.ToString(((Label)gvItems.Rows[e.NewSelectedIndex].FindControl("lblgvItem")).Text);
                txtMRP.Text = Convert.ToString(((Label)gvItems.Rows[e.NewSelectedIndex].FindControl("lblgvMrp")).Text);
                txtQuantity.Text = Convert.ToString(((Label)gvItems.Rows[e.NewSelectedIndex].FindControl("lblgvquty")).Text);
                txtSalesPrice.Text = Convert.ToString(((Label)gvItems.Rows[e.NewSelectedIndex].FindControl("lblgvSalesPrice")).Text);
                txtNewSalePrice.Text = Convert.ToString(((Label)gvItems.Rows[e.NewSelectedIndex].FindControl("lblgvNewSalesPrice")).Text);
                txtQ.Text = hfC.Value;
                hfItem.Value = txtitem.Text;
                hdfItem.Value = Convert.ToString(e.NewSelectedIndex);
                HdnOldMRP.Value = Convert.ToString(((HiddenField)gvItems.Rows[e.NewSelectedIndex].FindControl("gvHdnOldMRP")).Value);
            
              //  hdfItem.Value = txtitem.Text;
            }
        }
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        Save();
    }
    protected void ddlItem_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (hfType.Value == "1")
        {
            ddlItemsFill();
            //ddlItemsPurReturnFill();
        }
        else if (hfType.Value == "2")
        {

        }
    }
    protected void ddlItemsFill()
    {
        if (ddlItem.SelectedIndex > 0)
        {
             BlStock blStock = new BlStock();
                blStock.MasterId = Convert.ToInt64(hdfFKStock.Value);
                blStock.UserCode = hfUser.Value;
                blStock.FK_Client = Convert.ToInt64(hfClient.Value);
                blStock.SelectData();
                txtQuantity.Text = blStock.CurrentStock.ToString();
                txtQ.Text = blStock.CurrentStock.ToString();
                hfC.Value = blStock.CurrentStock.ToString();
                txtMRP.Text = Convert.ToString(blStock.MRP);
                HdnOldMRP.Value = Convert.ToString(blStock.MRP);
                txtSalesPrice.Text = Convert.ToString(blStock.SalePrice);
                blStock = null; 
            
        }
    }
    public void btnItem_Click(object sender, EventArgs e)
    {
        try
        {
            ddlItemsFill();
        }
        catch (Exception ex)
        {

        }
    }
    protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            btnSubmit.Visible = true;
            Clear();
            if (Convert.ToBoolean(hfIsAdd.Value) == false)
            {
                ((Label)CommonPopupPrivilege.FindControl("popuplabel")).Text = "User does not have permission to Add";
                mpPrivilege.Show();
            }
            divAdd.Style.Add("display", "block");
            divView.Style.Add("display", "none");
            EnableItem(true);
            
        }
        catch (Exception ex)
        {

            ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "toastr.warning('" + ex.Message + "')", true);
        }
    }
    protected void imgbtnView_Click(object sender, ImageClickEventArgs e)
    {
        divAdd.Style.Add("display", "none");
        divView.Style.Add("display", "block");
        ViewState["GridData"] = null;
        gvItems.DataSource = null;
        ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "ClearAdd();ClearSearch();", true);
        GridFill(1);
    }
    protected void GridFill(int currentPage)
    {
        BlPriceCorrection blPrice = new BlPriceCorrection();
        DataTable dtbl = new DataTable();
        blPrice.MasterId = 0;
        blPrice.FK_Store = Convert.ToInt64(ddlSearchStore.SelectedValue);
        blPrice.FK_Client = Convert.ToInt64(hfClient.Value);
        blPrice.FromDate = Convert.ToDateTime(txtFromDateSearch.Text == "" ? "" + DateTime.Now.Date + "" : txtFromDateSearch.Text);// DateTime.ParseExact(txtFromDateSearch.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
        blPrice.ToDate = Convert.ToDateTime(txtToDateSearch.Text == "" ? "" + DateTime.Now.Date + "" : txtToDateSearch.Text);
        dtbl = blPrice.SelectAllData();
        grdPrice.DataSource = dtbl;
        grdPrice.DataBind();

        blPrice = null;

    }
    protected void grdPrice_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdPrice.PageIndex = e.NewPageIndex;
        GridFill(e.NewPageIndex); 
    }
    protected void grdPrice_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        BlPriceCorrection blPrice = new BlPriceCorrection();
        ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "ShowAddDiv();", true);
        long MasterId = Convert.ToInt64(grdPrice.DataKeys[e.NewSelectedIndex]["ID_priceCorrection"].ToString());
        blPrice.MasterId = MasterId;
        hfID_Price.Value = MasterId.ToString();
        blPrice.FK_Client = Convert.ToInt32(hfClient.Value.Split(',')[0]);
        blPrice.SelectData();
        ddlStore.SelectedValue = Convert.ToInt64(blPrice.FK_Store).ToString();
        txtDate.Text = Convert.ToDateTime(blPrice.PriceCorrectionDate).ToString();
        divAdd.Style.Add("display", "block");
        divView.Style.Add("display", "none;");
        DataSet dtbl = blPrice.SelectdetailsGridData();
        gvItems.DataSource = dtbl;
        gvItems.DataBind();
   
        EnableItem(false);


        btnSubmit.Visible = false;

      
    }
    protected void grdPrice_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {

        try
        {

            string confirmValue = Request.Form["confirm_value"];
            if (confirmValue.Length > 3)
                confirmValue = confirmValue.Substring(confirmValue.Length - 3, 3);
            if (confirmValue == "Yes")
            {
                long status = 0;
                BlPriceCorrection blPrice = new BlPriceCorrection();
                blPrice.MasterId = Convert.ToInt64(grdPrice.DataKeys[e.RowIndex]["ID_priceCorrection"].ToString());
                hfMasterID.Value = (grdPrice.DataKeys[e.RowIndex]["ID_priceCorrection"].ToString());
                blPrice.Cancelleduser = hfuserName.Value;
                blPrice.FK_Client = Convert.ToInt64(hfClient.Value);
                blPrice.CancelledOn = DateTime.Now;
                blPrice = null;
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
   private void EnableItem(Boolean Value)
    {
        ddlCat.Enabled = txtDate.Enabled  = txtitem.Enabled   = txtNewSalePrice.Enabled =
            gvItems.Enabled =ImageButton3.Enabled=btnSubmit.Enabled= Value;
        gvItems.Columns[8].Visible = Value;
        gvItems.Columns[9].Visible = Value;
       
    }
   protected void btnSearch_Click(object sender, EventArgs e)
   {
       GridFill(1);
   }
   protected void ImageButton5_Click(object sender, ImageClickEventArgs e)
   {
       Delete();
   }
   public void SetReadonly()
   {
       txtDate.Attributes.Add("readonly", "readonly");
   }
   protected void grdPrice_RowDataBound(object sender, GridViewRowEventArgs e)
   {
       try
       {
           //
           if (e.Row.RowType == DataControlRowType.DataRow)
           {
               // For `Approve` button
               Button btnapprove = (Button)e.Row.FindControl("btnApprovedStatus");
               ImageButton btnDelete = (ImageButton)e.Row.FindControl("ImageButton5");
              // ImageButton btnEdit = (ImageButton)e.Row.FindControl("ImageButton1784");
               // give property id of button form template field
               if (btnapprove.Text == "Approved")
               {
                   btnapprove.Enabled = false;
                   btnDelete.Enabled = false;
                 //  btnEdit.Enabled = true;
                   btnapprove.BackColor = System.Drawing.Color.DarkGreen;//true means enable else you may set false to disable button 
               }
               else
               {
                   btnapprove.BackColor = System.Drawing.Color.DarkRed;
                //   btnEdit.Enabled = true;
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
   protected void btnClear_Click(object sender, EventArgs e)
   {
       gvItems.DataSource = null;
       gvItems.DataBind();
       btnSubmit.Visible = true;
       ViewState["GridData"] = null;
   }
   protected void grdPrice_RowCommand(object sender, GridViewCommandEventArgs e)
   {
       if (e.CommandName == "ApprovedStatus")
       {

           string confirmValue = Request.Form["confirm_value"];
           if (confirmValue.Length > 3)
               confirmValue = confirmValue.Substring(confirmValue.Length - 3, 3);
           if (confirmValue == "Yes")
           {
               GridViewRow gr = (GridViewRow)((Button)e.CommandSource).NamingContainer;
               int index = gr.RowIndex;


               long status = 0;
               BlPriceCorrection blPrice = new BlPriceCorrection();
               blPrice.MasterId = Convert.ToInt64(grdPrice.DataKeys[index]["ID_priceCorrection"].ToString());
               blPrice.FK_Client = Convert.ToInt64(hfClient.Value);
               blPrice.UserCode = hfuserName.Value;
               blPrice.FK_Store = Convert.ToInt64(ddlStore.SelectedValue);
               if (hfIsUpdate.Value.ToLower() == "true")
               {
                   Boolean approved = Convert.ToBoolean(grdPrice.DataKeys[index]["Approved"].ToString());
                   if (approved == false)
                   {
                       status = blPrice.UpdateData();
                       if (status >= 0)
                       {
                           GridFill(1);
                           ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "toastr.success('Data approved successfully')", true);
                       }
                       else if (status == -6)
                       {
                           ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "ShowAddDiv();", true);
                           Populate.ErrorMessageBox(this, "Cannot approve, Day end Process already done");
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
               blPrice = null;

           }



       }
   }
}