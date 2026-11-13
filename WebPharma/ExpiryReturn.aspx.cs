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
    long TransactionID; decimal TaxAmount, TotalTaxPercentage, TotalTaxPerCS;
    Boolean Editable ;
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
                hdfMode.Value = "IER";
                ddlStore.Enabled = false;
                //ddlBillType.Focus();
                DisplayDiv();
                hfStore.Value = ddlStore.SelectedValue;

                GridFill(1);
                txttaxAmount.Enabled = false;
                txtCurrentStock.Enabled = false;
                //txtPurchaseRateItem.Enabled = false;
                //txtTotalAmountItem.Enabled = false;

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
    protected void btnAddItems_Click(object sender, EventArgs e)
    {
        try
        {
            //
            int a = 0;
            if (hfItem.Value.Trim() == txtitem.Text.ToString().Trim())
            {
            }
            if (hdfFkItem.Value == hfItemId.Value)
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
                    FillReasonddl();
                    ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "ItemClear();", true);
                    hfEditedRow.Value = "";
                }
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "toastr.warning('" + ex.Message + "')", true);
        }

    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            //
            System.Threading.Thread.Sleep(10);

            bool Status = false;
            //if (rdPurchase.Checked && gvPurchaseItems.Rows.Count > 0)
            //{
            //    Status = true;

            //}
            //else if (rdItemPurchase.Checked && gvItems.Rows.Count > 0)
            //{
            //    Status = true;

            //}
            if (gvItems.Rows.Count > 0)
            {
                Status = true;

            }
            if (Status)
            {
                Save();
            }
            else
            {
                ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "toastr.warning('Atleast one item required')", true);
            }
        }
        catch (Exception ex)
        {
            Populate.WarningMsg(this, ex.ToString());
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

                System.Threading.Thread.Sleep(10);

                bool Status = false;

                if (gvItems.Rows.Count > 0)
                {
                    Status = true;

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

            ScriptManager.RegisterStartupScript(pge, pge.GetType(), "Script", "toastr.success('Data Approved successfully');ClearAdd();", true);
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
        blpurchase.ReturnMethod = "IER";
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
        }
        else
        {
            ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "toastr.warning('User does not have permission to Approve')", true);


        }


    }
    protected void Save()
    {
        long Status = 0;
        BlPurchaseReturn blpurchase = new BlPurchaseReturn();


        if (Status == 0)
        {

            blpurchase.BillNo = Convert.ToInt64(hfBillNo.Value);
            blpurchase.BillDate = DateTime.ParseExact(txtDate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            blpurchase.Invoice = txtinvoice.Text;
            blpurchase.InvDate = DateTime.ParseExact(txtDate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            blpurchase.FK_Supplier = Convert.ToInt64(txtSupRefno.Text);
            blpurchase.FK_Store = Convert.ToInt64(ddlStore.SelectedValue);
            blpurchase.FK_Category = Convert.ToInt64(ddlStore.SelectedValue);
            blpurchase.TotalAmt = Convert.ToDecimal(txtTotalAmount.Text == "" ? "0" : txtTotalAmount.Text);
            blpurchase.Discount = Convert.ToDecimal(txtDiscount.Text == "" ? "0" : txtDiscount.Text);
            blpurchase.DiscountAmt = Convert.ToDecimal(txtDiscountAmt.Text == "" ? "0" : txtDiscountAmt.Text);
            blpurchase.NetAmount = Convert.ToDecimal(txtNetAmount.Text == "" ? "0" : txtNetAmount.Text);
            blpurchase.Remarks = "Expiry Return" + "-" + ddlExpiryMethod.SelectedItem.Text;
            blpurchase.OtherCharge = Convert.ToDecimal(txtOtherChrg.Text == "" ? "0" : txtOtherChrg.Text);
            blpurchase.TransactionID = Convert.ToInt64(ViewState["TransactionID"]);
            blpurchase.Cancelled = false;
            //blpurchase.CancelledBy = hfuserName.Value;
            blpurchase.UserCode = hfuserName.Value;
            string datetimes = DateTime.Now.ToShortDateString();
            blpurchase.EnteredOn = DateTime.ParseExact(datetimes, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            blpurchase.FK_Client = Convert.ToInt64(hfClient.Value);
            blpurchase.ExpiryDate = Convert.ToDateTime(txtExpiry.Text).ToString("yyyy/MM/dd"); //DateTime.ParseExact(txtExpiry.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture).Date.ToString(); 
            blpurchase.Expiry = true;
            blpurchase.ReturnMethod = ddlExpiryMethod.SelectedValue;
            blpurchase.FK_BillType = Convert.ToInt64(ddlBillType.SelectedValue);
            BlPurchase blpurchases = new BlPurchase();
            blpurchases.itemID = Convert.ToInt64(hfItemId.Value == "" ? "0" : hfItemId.Value);
            DataTable dtB = blpurchases.SelectGroupID();
            hfGroupID.Value = dtB.Rows[0]["GroupID"].ToString();
            ArrayList arlist = new ArrayList();
            BlPurchaseReturnDetails blpurchasedetails;

            int RowIndex = 0;
            //if (ddlExpiryMethod.SelectedValue == "IER")
            //{
            DataTable dtbl = (DataTable)ViewState[DirectPR];
            foreach (DataRow dr in dtbl.Rows)
            {
                blpurchasedetails = new BlPurchaseReturnDetails();
                blpurchasedetails.FK_Items = Convert.ToInt64(dr["FK_Items"].ToString());
                blpurchasedetails.FK_Stock = Convert.ToInt64(dr["FK_Stock"].ToString());
                blpurchasedetails.FK_Units = Convert.ToInt64(dr["FK_Unit"].ToString());
                blpurchasedetails.BatchNo = dr["BatchNo"].ToString();
                blpurchasedetails.ExpiryDate = DateTime.Parse(dr["ExpiryDate"].ToString());
                decimal Quantity = Convert.ToDecimal(dr["Quantity"].ToString());
                blpurchasedetails.ActQty = Convert.ToDecimal(dr["ActQty"].ToString());
                blpurchasedetails.Free = Convert.ToDecimal(dr["Free"].ToString());
                blpurchasedetails.PurRate = Convert.ToDecimal(dr["PurchaseRate"].ToString());
                blpurchasedetails.FK_Reason = Convert.ToInt64(dr["FK_Reason"].ToString());
                blpurchasedetails.MRP = Convert.ToDecimal(dr["MRP"].ToString());
                blpurchasedetails.Quantity = Convert.ToDecimal(dr["Quantity"].ToString());
                blpurchasedetails.MRP = Convert.ToDecimal(dr["MRP"].ToString());
                blpurchasedetails.FK_TaxGroupID = Convert.ToInt64(hfGroupID.Value);
                blpurchasedetails.PurchaseDiscPercentage = Convert.ToDecimal(dr["PurchaseDiscPercentage"].ToString());
                blpurchasedetails.PurchaseDiscAmount = Convert.ToDecimal(dr["PurchaseDiscAmt"].ToString());
                blpurchasedetails.SalesPrice = 0;
                blpurchasedetails.FK_Reason = Convert.ToInt64((((DropDownList)gvItems.Rows[RowIndex].FindControl("ddlReason")).SelectedValue));
                blpurchasedetails.SlNo = RowIndex + 1;
                blpurchasedetails.DiscountOnTax = Convert.ToBoolean(dr["DiscountOnTax"]);
                blpurchasedetails.IncludePtax = Convert.ToBoolean(dr["IncludePtax"]);
                blpurchasedetails.PurInvoiceNo = dr["PurInvoiceNo"].ToString();
                string PurInvDate = dr["PurInvoiceDate"].ToString();
                if (PurInvDate == "NULL" || PurInvDate == "")
                {
                    blpurchasedetails.PurInvoiceDate =  Convert.ToDateTime("01-01-1900");
                }
                else
                {
                    blpurchasedetails.PurInvoiceDate= DateTime.Parse(dr["PurInvoiceDate"].ToString());
                }
               // blpurchasedetails.PurInvoiceDate =   DateTime.Parse(dr["PurInvoiceDate"].ToString());
      //  blpurchasedetails.PurInvoiceDate = dr["PurInvoiceDate"] == null || dr["PurInvoiceDate"] == DBNull.Value  ? (DateTime?)null : Convert.ToDateTime(dr["PurInvoiceDate"].ToString());

                arlist.Add(blpurchasedetails);
                blpurchasedetails = null;
                RowIndex = RowIndex + 1;

            }
            //}
            //else
            //{
            //    RowIndex = 0;


            //    foreach (GridViewRow gr in gvItems.Rows)
            //    {
            //        if (gr.RowType == DataControlRowType.DataRow)
            //        {
            //            blpurchasedetails = new BlPurchaseReturnDetails();
            //            blpurchasedetails.FK_Items = Convert.ToInt64(gvItems.DataKeys[gr.RowIndex]["FK_Items"].ToString());
            //            blpurchasedetails.FK_Stock = Convert.ToInt64(((HiddenField)gr.FindControl("hfStockID")).Value);
            //            blpurchasedetails.FK_Units = Convert.ToInt64(gvItems.DataKeys[gr.RowIndex]["FK_Units"].ToString());
            //            blpurchasedetails.ExpiryDate = Convert.ToDateTime((((Label)gr.FindControl("ExpiryDate")).Text));
            //            blpurchasedetails.BatchNo = (((Label)gr.FindControl("BatchNo")).Text);
            //            blpurchasedetails.Quantity = Convert.ToDecimal(((TextBox)gr.FindControl("Quantity")).Text);


            //            blpurchasedetails.ActQty = Convert.ToDecimal((((HiddenField)gr.FindControl("hdnActQty")).Value));
            //            blpurchasedetails.Free = Convert.ToDecimal((((Label)gr.FindControl("Free")).Text));
            //            blpurchasedetails.PurRate = Convert.ToDecimal((((Label)gr.FindControl("PurRate")).Text));
            //            blpurchasedetails.FK_Reason = Convert.ToInt64(((DropDownList)gr.FindControl("ddlReason")).SelectedValue);
            //            blpurchasedetails.MRP = Convert.ToDecimal(((Label)gr.FindControl("MRP")).Text);
            //            blpurchasedetails.FK_TaxGroupID = Convert.ToInt64(hfGroupID.Value);
            //            blpurchasedetails.PurchaseDiscPercentage = Convert.ToDecimal((((HiddenField)gr.FindControl("hdnPurDisc")).Value));
            //            blpurchasedetails.SalesPrice = 0;
            //            blpurchasedetails.FK_Reason = Convert.ToInt64((((DropDownList)gvItems.Rows[RowIndex].FindControl("ddlReason")).SelectedValue));
            //            blpurchasedetails.SlNo = RowIndex + 1;
            //            arlist.Add(blpurchasedetails);
            //            blpurchasedetails = null;
            //            RowIndex = RowIndex + 1;

            //        }
            //    }
            //}
            blpurchase.XmlPR = Common.WriteToXml(arlist);
            if (Status == 0)
            {
                if (hfPurchaseReturn.Value == string.Empty)
                {
                    if (hfIsAdd.Value.ToLower() == "true")
                    {
                        Status = blpurchase.InsertData();
                        if (Status > 0)
                        {
                            ClearAll();
                            GridFill(1);
                            ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "ShowAddDiv();", true);
                            Populate.InsertMsg(this);
                            //*** Print Purchase
                            string PurachaseReturnId = Status.ToString();
                            string popupreport = "<script> window.open('ExpReturnPrint.aspx?PurchaseReturnId=" + PurachaseReturnId + "', 'self', 'left=200,top=50,width=900,height=680,status=0,resizable=0,fullscreen=0,scrollbars=1');</script>";

                            //string popupreport = "<script> window.open('PurchaseReturnPrint.aspx?PurchaseReturnId=" + PurachaseReturnId + "', 'self', 'left=200,top=50,width=900,height=680,status=0,resizable=0,fullscreen=0,scrollbars=1');</script>";
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "Success", popupreport, false);
                            ddlStore.SelectedValue = Session["mainstore"].ToString();
                            ddlStore.Enabled = false;
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
                        else if (Status == -8)
                        {
                            ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "toastr.error('Cannot update data.. TransactionID already exist')", true);
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
                            string PurachaseReturnId = Status.ToString();
                            string popupreport = "<script> window.open('ExpReturnPrint.aspx?PurchaseReturnId=" + PurachaseReturnId + "', 'self', 'left=200,top=50,width=900,height=680,status=0,resizable=0,fullscreen=0,scrollbars=1');</script>";

                            //string popupreport = "<script> window.open('PurchaseReturnPrint.aspx?PurchaseReturnId=" + PurachaseReturnId + "', 'self', 'left=200,top=50,width=900,height=680,status=0,resizable=0,fullscreen=0,scrollbars=1');</script>";
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "Success", popupreport, false);

                        }

                        else if (Status == -1 || Status == -2)
                        {
                            ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "ShowAddDiv();", true);
                            Populate.ErrorMessageBox(this, "Cannot update data, No stock  available.");

                        }
                        else if (Status == -3)
                        {
                            Populate.ErrorMessageBox(this, " Return AccHead has not set in Bill Type");
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
                Populate.ErrorMessageBox(this, "Quantity Cannot Zero/Exceed Available Stock");
            }
        }
        else
        {
            Populate.ErrorMessageBox(this, "Purchase Return date cannot be less than purchase date");
        }

    }
    protected void gvPurchaseReturn_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvPurchaseReturn.PageIndex = e.NewPageIndex;
        GridFill(e.NewPageIndex);
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
            blpurchaseReturn.Expiry = true;
            blpurchaseReturn.SelectData();

            ddlExpiryMethod.SelectedValue = blpurchaseReturn.ReturnMethod;
            btnS.Text = "PTax";
            txtGSTINT.Text = blpurchaseReturn.GSTIN;
            txtDate.Text = blpurchaseReturn.BillDate.ToString("dd/MM/yyyy");

            if (MasterId > 0)
            {
                btnApprove.Visible = true;

            }
            if (blpurchaseReturn.Approved == false)
            {
                btnApprove.Visible = true; btnSave.Visible = true;
            }
            else
            {
                btnApprove.Visible = false; btnSave.Visible = false;
                ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "toastr.warning('Already Approved.Cannot Edit !!!')", true);
            }
            try
            {
                txtSupRefno.Text = blpurchaseReturn.FK_Supplier.ToString();
                txtSupplier.Text = blpurchaseReturn.SupplierName.ToString();
               // Supplier();
                Supplieredit();
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
            //cbxIsCredit.Checked = blpurchaseReturn.Credit;
            //txtRemark.Text = blpurchaseReturn.Remarks.ToString();
            txtDiscount.Text = blpurchaseReturn.Discount.ToString();
            txtDiscountAmt.Text = blpurchaseReturn.DiscountAmt.ToString();
            hfBillNo.Value = blpurchaseReturn.BillNo.ToString();
            txtTotalAmount.Text = blpurchaseReturn.TotalAmt.ToString();
            txtOtherChrg.Text = blpurchaseReturn.OtherCharge.ToString();
            txtinvoice.Text = blpurchaseReturn.Invoice;
            txtInvoicedate.Text = blpurchaseReturn.InvDate.ToString("dd/MM/yyyy");
            divAdd.Style.Add("display", "block");
            divView.Style.Add("display", "none;");
            txtExpiry.Text = Convert.ToDateTime(blpurchaseReturn.ExpiryDate).ToString("dd/MM/yyyy");
            ddlExpiryMethod.Attributes.Add("disabled", "true");
            txtExpiry.Attributes.Add("disabled", "true");

            blpurchaseReturn.FK_Client = Convert.ToInt32(hfClient.Value.Split(',')[0]);
            blpurchaseReturn.ReturnMethod = "IER";
            DataTable dtbl = blpurchaseReturn.DirectPurchaseReturnItems();
            DataTable dtNetTotal = blpurchaseReturn.ExpiryReturnItemWiseRounfOff();
            ViewState[DirectPR] = dtbl; 
            gvItems.Enabled = true;
            gvItems.DataSource = dtbl;
            gvItems.DataBind();
            //if (blpurchaseReturn.Expiry)
            //{ rdItemPurchase.Checked = false; rdExpiry.Checked = true; }
            //else
            //{
            //    rdItemPurchase.Checked = true; rdExpiry.Checked = false;
            //}
            //rdPurchase.Checked = false;
            //TotalAmountEdit(dtbl);
            TotalAmountEdit(dtNetTotal);
          

            DisplayDiv();
            if (ddlExpiryMethod.SelectedValue == "IER")
            {
                divItems.Visible = true;
                liinsert.Visible = true;
            }
            else
            {
                divItems.Visible = false;
                liinsert.Visible = false;

            }
            FillReasonddl();

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
        catch (Exception ex)
        {
            Populate.WarningMsg(this, ex.ToString());
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
    protected void gvPurchaseReturn_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            //
            BlPurchase blPurchase = new BlPurchase();

            if (e.CommandName == "Print")
            {


                GridViewRow row = (GridViewRow)((ImageButton)e.CommandSource).NamingContainer;
                int index = row.RowIndex;
                string PurachaseReturnId = gvPurchaseReturn.DataKeys[index]["ID_PurchaseReturn"].ToString();
                string popupreport = "<script> window.open('ExpReturnPrint.aspx?PurchaseReturnId=" + PurachaseReturnId + "', 'self', 'left=200,top=50,width=900,height=680,status=0,resizable=0,fullscreen=0,scrollbars=1');</script>";
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
    protected void btnItem_Click(object sender, EventArgs e)
    {
        try
        {
            ViewState["GridDataPurchase"] = null;

            //if (rdItemPurchase.Checked)
            //{
            BlStock blStock = new BlStock();
            blStock.MasterId = Convert.ToInt64(hdfFKStock.Value);
            blStock.UserCode = hfUser.Value;
            blStock.FK_Client = Convert.ToInt64(hfClient.Value);
            blStock.Mode = "IER";
            //blStock.SelectData();
            DataTable dt = new DataTable();
          dt= blStock.SelectStockDatewithPurchase();
          if (dt.Rows.Count > 0)
          {
              if (ddlUnit.SelectedValue == "0" || (txtQuantity.Text =="0"))
              {
                  ddlUnit.SelectedValue = dt.Rows[0]["FK_Unit"].ToString();
              }

              if (Editable == true)
              {
                  txtCurrentStock.Text = dt.Rows[0]["CurrentStock"].ToString();
                  Editable = false;
              }
              else
              {


                  txtBatch.Text = dt.Rows[0]["BatchNo"].ToString();
                  txtItemExpDate.Text = dt.Rows[0]["ExpiryDate"].ToString();

                  hfMRP.Value = dt.Rows[0]["MRP"].ToString();
                  txtMRP.Text = dt.Rows[0]["MRP"].ToString();
                  txtSalesPrice.Text = dt.Rows[0]["SalPrice"].ToString();
                  txtPurchaseRateItem.Text = dt.Rows[0]["PurRate"].ToString();
                  txtPurDiscPer.Text = dt.Rows[0]["PurchaseDiscPercentage"].ToString();
                  txtPurDiscAmnt.Text = dt.Rows[0]["PurchaseTaxAmount"].ToString();
                  txttaxAmount.Text = dt.Rows[0]["PurchaseTaxAmount"].ToString();
                  hfTotTaxPercentage.Value = txttaxAmount.Text;
                  hfItemId.Value = dt.Rows[0]["FK_Items"].ToString();
                  //txtTotalAmountItem.Text = "0";
                  txtCurrentStock.Text = dt.Rows[0]["CurrentStock"].ToString();
                  txtpurinvoiceNo.Text = dt.Rows[0]["PurInvoiceNo"].ToString();
                  txtpurinvoiceDate.Text = dt.Rows[0]["PurInvoiceDate"].ToString();
              }
          }
          BlUnit blunit = new BlUnit();
          blunit.MasterId = Convert.ToInt64(blStock.FK_Units);
          blunit.FK_Client = Convert.ToInt64(hfClient.Value);
          blunit.SelectData();
          hfUnitQuantity.Value = blunit.NoofUnits.ToString();
        
            //ddlCategory.SelectedValue = blStock.FK_Category.ToString();
            //ddlUnit.SelectedValue = blStock.FK_Units.ToString();
            //txtBatch.Text = blStock.BatchNo;
            //txtItemExpDate.Text = blStock.ExpiryDate.ToString("dd/MM/yyyy");
            //hfMRP.Value = blStock.MRP.ToString();
            //txtMRP.Text = blStock.MRP.ToString();
            //txtSalesPrice.Text = blStock.SalePrice.ToString();
            //txtPurchaseRateItem.Text = blStock.PurRate.ToString();
            //txtPurDiscPer.Text = blStock.PurchaseDiscPercentage.ToString();
            //txtPurDiscAmnt.Text = blStock.PurchaseTaxAmount.ToString();
            //txttaxAmount.Text = blStock.PurchaseTaxAmount.ToString();
            //hfTotTaxPercentage.Value = txttaxAmount.Text;
            //hfItemId.Value = blStock.FK_Items.ToString();
            ////txtTotalAmountItem.Text = "0";
            //txtCurrentStock.Text = blStock.CurrentStock.ToString();
            //BlUnit blunit = new BlUnit();
            //blunit.MasterId = Convert.ToInt64(blStock.FK_Units);
            //blunit.FK_Client = Convert.ToInt64(hfClient.Value);
            //blunit.SelectData();
            //hfUnitQuantity.Value = blunit.NoofUnits.ToString();
            blStock = null;
            var blItm = new BlItems();
            blItm.SelectData();
            txtHSN.Text = blItm.HSNCode;
            ddlSubunit.SelectedValue = blItm.FK_SubUnit.ToString();


            BlPurchase blpurchase = new BlPurchase();
            blpurchase.itemID = Convert.ToInt64(hfItemId.Value);
            blpurchase.Purchase = true;
            DataTable dtPurchase = blpurchase.SelectGriddata();
            blpurchase.Purchase = false;

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
            //foreach (DataRow row in dt.Rows)
            //{

            //    if (Convert.ToBoolean(row["TaxIntraState"].ToString()) == true)
            //    {
            //        TotalTaxSales += Convert.ToDecimal(row["TaxPercentage"].ToString());
            //    }


            //    hfTotTaxsalesPercentage.Value = Convert.ToString(TotalTaxSales);
            //}
            cbxIncludeTax.Enabled = true;
            cbxIncludeTax.Checked = true;
            cbxIncludeTax.Enabled = false;

            CalculateActualQuantity();
            ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "getpurchaseamount()", true);

            txtQuantity.Focus();


        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "toastr.warning('" + ex.Message + "')", true);
        }
    }
    protected void chkInterState_CheckedChanged(object sender, EventArgs e)
    {

        //if (gvItems.Rows.Count > 0 && hfGvItemSelected.Value.ToString() == "")
        //{
        //    if (Validform())
        //        GridCalculation();
        //    else
        //        ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "toastr.warning('Incomplete item detail exist')", true);
        //}
        //if (gvItems.Rows.Count > 0 && hfGvItemSelected.Value.ToString() == "1")
        //{
        //    ddlItemsFill();
        //    if (Validform())
        //        GridCalculation();
        //    else
        //        ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "toastr.warning('Incomplete item detail exist')", true);
        //}
        //if (gvItems.Rows.Count == 0)
        //{
        //    ddlItemsFill();
        //}
        //CalculateTotals();



    }
    protected void ddlUnit_SelectedIndexChanged(object sender, EventArgs e)
    {

        try
        {
            CalculateActualQuantity(); ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "getpurchaseamount()", true);

        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "toastr.warning('" + ex.Message + "')", true);
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
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "toastr.warning('" + ex.Message + "')", true);
        }
    }

    //protected void btnS_Click(object sender, EventArgs e)
    //{
    //    BlPurchase blpurchase = new BlPurchase();
    //    DataTable dt;

    //    Populate Populate = new Populate();
    //    CommonPopupGrid1.CallingFunction = "Purchase";
    //    string Refno = ((TextBox)CommonPopupGrid1.FindControl("txtRefno")).Text;
    //    string searchCol = ((HiddenField)CommonPopupGrid1.FindControl("hfColumn")).Value;
    //    Refno = Refno == string.Empty || Refno == null ? "0" : Refno;
    //    GridView gvNew = (GridView)CommonPopupGrid1.FindControl("gdvSearch");
    //    BlStock blStock = new BlStock();
    //    if (hfItemId.Value != "")
    //        blpurchase.itemID = Convert.ToInt64(hfItemId.Value);
    //    dt = blpurchase.SelectGriddataPurchase();
    //    dt.Columns.Add("TaxAmount", typeof(decimal));
    //    gvNew.DataSource = dt;
    //    gvNew.DataBind();
    //    gvNew.Columns[3].Visible = false;
    //    lblPopupH.Text = "Purchase Tax";
    //    mpGrid.Show();

    //}
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
    private void getpurchaseamount()
    {
        var qty = Convert.ToDecimal(txtQuantity.Text == "" ? "0" : txtQuantity.Text);
        qty = qty * Convert.ToDecimal(hfUnitQuantity.Value == "" ? "0" : hfUnitQuantity.Value);
        if (cbxIncludeTax.Checked == true)
        {
            txttaxAmount.Text = Math.Round(((Convert.ToDecimal(txtPurchaseRateItem.Text == "" ? "0" : txtPurchaseRateItem.Text)) * (Convert.ToDecimal(qty  )) * (Convert.ToDecimal(hfTotTaxPercentage.Value.ToString()))) / ((Convert.ToDecimal(hfTotTaxPercentage.Value.ToString() == "" ? "0" : hfTotTaxPercentage.Value.ToString())) + (100)), 4).ToString();
            hfPurchaseValue.Value = ((Convert.ToDecimal(txtPurchaseRateItem.Text == null ? "0" : txtPurchaseRateItem.Text)) * (Convert.ToDecimal(qty  )) - (Convert.ToDecimal(txttaxAmount.Text == null ? "0" : txttaxAmount.Text))).ToString();
            if (cbxDiscountTax.Checked == true)
            {
                decimal PurRate = Convert.ToDecimal(txtPurchaseRateItem.Text == "" ? "0" : txtPurchaseRateItem.Text);
                decimal disPer = Convert.ToDecimal(txtPurDiscPer.Text == "" ? "0" : txtPurDiscPer.Text);
                decimal ActPrice = ((PurRate) * (disPer)) / (100);
                decimal ActPurRate = (PurRate - ActPrice);
                txttaxAmount.Text = Math.Round(((Convert.ToDecimal(ActPurRate)) * (Convert.ToDecimal(qty)) * (Convert.ToDecimal(hfTotTaxPercentage.Value.ToString()))) / ((Convert.ToDecimal(hfTotTaxPercentage.Value.ToString() == "" ? "0" : hfTotTaxPercentage.Value.ToString())) + (100)), 4).ToString();
            }
            //else
            //{
            //    txttaxAmount.Text = Math.Round(((Convert.ToDecimal(txtPurchaseRate.Text == "" ? "0" : txtPurchaseRate.Text)) * (Convert.ToDecimal(qty == "" ? "0" : qty)) * (Convert.ToDecimal(hfTotTaxPercentage.Value.ToString()))) / ((Convert.ToDecimal(hfTotTaxPercentage.Value.ToString() == "" ? "0" : hfTotTaxPercentage.Value.ToString())) + (100)), 4).ToString();
            //}


        }
        else
        {
            if (cbxDiscountTax.Checked == true)
            {
                decimal PurRate = Convert.ToDecimal(txtPurchaseRateItem.Text == "" ? "0" : txtPurchaseRateItem.Text);
                decimal disPer = Convert.ToDecimal(txtPurDiscPer.Text == "" ? "0" : txtPurDiscPer.Text);
                decimal ActPrice = ((PurRate) * (disPer)) / (100);
                decimal ActPurRate = (PurRate - ActPrice);
                txttaxAmount.Text = Math.Round(((Convert.ToDecimal(ActPurRate)) * (Convert.ToDecimal(qty  )) * (Convert.ToDecimal(hfTotTaxPercentage.Value.ToString() == "" ? "0" : hfTotTaxPercentage.Value.ToString()))) / (100), 4).ToString();
            }
            else
            {

                txttaxAmount.Text = Math.Round(((Convert.ToDecimal(txtPurchaseRateItem.Text == null ? "0" : txtPurchaseRateItem.Text)) * (Convert.ToDecimal(qty  )) * (Convert.ToDecimal(hfTotTaxPercentage.Value.ToString() == "" ? "0" : hfTotTaxPercentage.Value.ToString()))) / (100), 4).ToString();
            }
            hfPurchaseValue.Value = (Convert.ToDecimal(txtPurchaseRateItem.Text == null ? "0" : txtPurchaseRateItem.Text) * Convert.ToDecimal(qty )).ToString();
        }
        if (cbxDiscountTax.Checked == true)
        {

            txtPurDiscAmnt.Text = Math.Round((((Convert.ToDecimal(hfPurchaseValue.Value.ToString() == "" ? "0" : hfPurchaseValue.Value.ToString()))) * (Convert.ToDecimal(txtPurDiscPer.Text == null ? "0" : txtPurDiscPer.Text))) / (100), 4).ToString();
        }
        else
        {
            txtPurDiscAmnt.Text = Math.Round(((Convert.ToDecimal(hfPurchaseValue.Value.ToString() == "" ? "0" : hfPurchaseValue.Value.ToString())) * (Convert.ToDecimal(txtPurDiscPer.Text == null ? "0" : txtPurDiscPer.Text))) / (100), 4).ToString();
        }


        txtNetPur.Text = Math.Round((Convert.ToDecimal(hfPurchaseValue.Value.ToString() == "" ? "0" : hfPurchaseValue.Value.ToString())) + (Convert.ToDecimal(txttaxAmount.Text == null ? "0" : txttaxAmount.Text)) - (Convert.ToDecimal(txtPurDiscAmnt.Text == null ? "0" : txtPurDiscAmnt.Text)), 4).ToString();
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
        //dtbl.Columns.Add("PurchaseTaxAmount");
        dtbl.Columns.Add("PurchaseDiscPercentage");

        //dtbl.Columns.Add("FK_Category");
        //dtbl.Columns.Add("HSN");
        //dtbl.Columns.Add("NetPurchase");
        dtbl.Columns.Add("IncludePtax");
        dtbl.Columns.Add("DiscountOnTax");
        dtbl.Columns.Add("PurchaseDiscAmt");
        //dtbl.Columns.Add("ActualPurRate");
        //dtbl.Columns.Add("FK_SubUnit");
        dtbl.Columns.Add("Unit");
        dtbl.Columns.Add("PurInvoiceDate");
        dtbl.Columns.Add("PurInvoiceNo");
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

        if (hfEditedRow.Value != "")
        {

            gvItems.Enabled = true;
            dtbl.Rows.RemoveAt(Convert.ToInt32(hfEditedRow.Value));
            gvItems.DataSource = dtbl;
            gvItems.DataBind();
            hfSlNo.Value = hfEditedRow.Value;

            inRowCount = Convert.ToInt32(hfEditedRow.Value);
            hfEditedRow.Value = "";

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

                //var s = ((DropDownList)gvItems.Rows[i].FindControl("ddlReason"));
                //var k = s.SelectedValue;
                //dtbl.Rows[i]["FK_Reason"] = ((DropDownList)gvItems.Rows[i].FindControl("ddlReason")).SelectedValue;
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
            string Unit = "";
            decimal PurchaseRate = txtPurchaseRateItem.Text.Trim() == "" ? 0 : Convert.ToDecimal(txtPurchaseRateItem.Text);
            string BatchNo = txtBatch.Text.Trim();
            decimal PurVAT = 0;
            decimal MRP = txtMRP.Text.Trim() == "" ? 0 : Convert.ToDecimal(txtMRP.Text.Trim());
            decimal SalesPrice = txtSalesPrice.Text.Trim() == "" ? 0 : Convert.ToDecimal(txtSalesPrice.Text.Trim()); ;
            decimal CESS = 0;
            //if (rdItemPurchase.Checked)
            //{
            //    BlStock blStock = new BlStock();
            //    blStock.MasterId = Convert.ToInt64(hdfFKStock.Value);
            //    blStock.UserCode = hfUser.Value;
            //    blStock.FK_Client = Convert.ToInt64(hfClient.Value);
            //    blStock.SelectData();
            //    PurchaseRate = Convert.ToDecimal(txtPurchaseRateItem.Text);
            //    BatchNo = blStock.BatchNo;
            //    MRP = blStock.MRP;


            //}
            decimal actq = Math.Round(Convert.ToDecimal(txtActqty.Text.Trim() == "" ? "0" : txtActqty.Text), 4);
            decimal free = Math.Round(Convert.ToDecimal(txtFree.Text == "" ? "0" : txtFree.Text), 4);
            decimal noofunit = Convert.ToDecimal(hfUnitQuantity.Value);
            decimal decqty = (actq + free);
            decimal TotQty = (actq);
            // decimal decqty = (actq );
            //* (noofunit);
            decimal Total = (TotQty * PurchaseRate) + ((PurVAT / 100) * (TotQty * PurchaseRate)) + ((CESS / 100) * (TotQty * PurchaseRate));


            //drow["slno"] = inRowCount + 1;
            drow["Item"] = hfItem.Value;
            drow["FK_Items"] = hdfFkItem.Value;
            drow["Quantity"] = (txtQuantity.Text.Trim() == "" ? "0" : txtQuantity.Text);
            drow["PurchaseRate"] = PurchaseRate;
            drow["FK_Unit"] = ddlUnit.SelectedValue;
            drow["BatchNo"] = BatchNo;
            drow["ExpiryDate"] = txtItemExpDate.Text;
            drow["TotalAmt"] = Math.Round(Convert.ToDecimal(txtNetPur.Text.Trim() == "" ? "0" : txtNetPur.Text.Trim()));
            drow["FK_Reason"] = ddlReasons.SelectedValue;
            drow["ActQty"] = actq;
            drow["Free"] = (txtFree.Text == "" ? "0" : txtFree.Text);
            //drow["PurchaseTaxAmount"] = (txttaxAmount.Text == "" ? "0" : txttaxAmount.Text);  
            drow["MRP"] = MRP;
            drow["SalesPrice"] = SalesPrice;
            drow["FK_Stock"] = hdfFKStock.Value;
            drow["NoofUnits"] = noofunit;
            drow["PurchaseDiscPercentage"] = Convert.ToDecimal(txtPurDiscPer.Text == "" ? "0" : txtPurDiscPer.Text);
            //drow["FK_Category"] = ddlCategory.SelectedValue;
            //drow["HSN"] = txtHSN.Text;
            //drow["NetPurchase"] = txtNetPur.Text;
            drow["IncludePtax"] = cbxIncludeTax.Checked;

            drow["DiscountOnTax"] = cbxDiscountTax.Checked;
            //drow["PurchaseDiscAmount"] = txtPurDiscAmnt.Text;

            //drow["ActualPurRate"] = 0;
            //drow["FK_SubUnit"] = ddlSubunit.SelectedValue;
            drow["Unit"] = ddlUnit.SelectedItem.Text;
            DateTime dateValue;
            if (DateTime.TryParse(txtpurinvoiceDate.Text, out dateValue))
            {
                drow["PurInvoiceDate"] = dateValue; 
            }
            else {
                drow["PurInvoiceDate"] = Convert.ToDateTime("01-01-1900"); ;
            }        
            drow["PurInvoiceNo"] = txtpurinvoiceNo.Text == "" ? "" : txtpurinvoiceNo.Text;
            drow["PurchaseDiscAmt"] = Convert.ToDecimal(txtPurDiscAmnt.Text == "" ? "0" : txtPurDiscAmnt.Text);
            dtbl.Rows.InsertAt(drow, inRowCount);
            gvItems.DataSource = dtbl;
            gvItems.DataBind();
            var s = gvItems.Rows.Count;
            TotalAmount(dtbl);
            ViewState[DirectPR] = dtbl;
            FillReasonddl();
            btnS.Text = "Ptax";
            hfSlNo.Value = "";
            hfItemId.Value = "0";
            
        }
        else
        {
            ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "toastr.warning('Item already exist')", true);

            return 1;
        }
        return 0;

    }
    protected void FillReasonddl()
    {

        try
        {
            DataTable dtbl = (DataTable)ViewState[DirectPR];
            for (int i = 0; i < gvItems.Rows.Count; i++)
            {

                Populate populate = new Populate();
                populate.FillDropdownList(((DropDownList)gvItems.Rows[i].FindControl("ddlReason")), "Reason", "Reason", "ID_Reason", "ID_Reason", "ReasonMode=1  AND FK_Client = " + hfClient.Value, "");
                if (dtbl.Rows.Count > 0)
                {
                    string mm = dtbl.Rows[i]["FK_Reason"].ToString();
                    ((DropDownList)gvItems.Rows[i].FindControl("ddlReason")).SelectedValue = dtbl.Rows[i]["FK_Reason"].ToString();
                }
            }
        }
        catch (Exception ex) { }


    }
    protected void TotalAmount(DataTable dtbl)
    {
        try
        {
            if (dtbl.Rows.Count > 0)
            {
               
                txtTotalAmount.Text = Math.Round(decimal.Parse((dtbl.Compute("Sum(TotalAmt)", "")).ToString()), 2).ToString();
                //txtVAT.Text = Math.Round(decimal.Parse((dtbl.Compute("Sum(TotalVAT)", "")).ToString()), 2).ToString();
                // txtTotCESS.Text = Math.Round(decimal.Parse((dtbl.Compute("Sum(TotalCESS)", "")).ToString()), 2).ToString();
                decimal DiscountAmt = Math.Round(((Convert.ToDecimal(txtTotalAmount.Text)) * (Convert.ToDecimal(txtDiscount.Text)) / 100), 2);
                txtDiscountAmt.Text = DiscountAmt.ToString();
                ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "Total()", true);
                

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
        txtOtherChrg.Text = "0";
        txtTotalAmount.Text = "0";
        txtDiscount.Text = "0";
        txtDiscountAmt.Text = "0";
        txtNetAmount.Text = "0";
    }
    protected void Supplier()
    {
        try
        {
            txtSupRefno.Text = "";
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
    public void SetDetails(string strName, string strRefno)
    {


        txtSupRefno.Text = strRefno;
        txtSupplier.Text = strName;
        if (ddlExpiryMethod.SelectedValue == "IER")
        {
            divItems.Visible = true;
            liinsert.Visible = true;
        }
        else
        {
            divItems.Visible = false;
            liinsert.Visible = false;
            BindExpiryItems(Convert.ToInt64(strRefno));
        }
        ddlExpiryMethod.Attributes.Add("disabled", "true");
        txtExpiry.Attributes.Add("disabled", "true");
        BlSupplier BlSupp = new BlSupplier();
        BlSupp.MasterId = Convert.ToInt64(strRefno);
        BlSupp.FK_Client = Convert.ToInt64(hfClient.Value);
        BlSupp.SelectData();
        txtGSTINT.Text = BlSupp.TinNo;
        txtGSTINT.Focus();

    }
    private void BindExpiryItems(long SupplierId)
    {
        try
        {
            BlItems blItems = new BlItems();
            blItems.FK_Supplier = SupplierId;
            blItems.FK_Client = Convert.ToInt32(hfClient.Value);


            blItems.MasterId = 0;
            blItems.ExpiryDate = Convert.ToDateTime(txtExpiry.Text); 

            DataTable dtbl = blItems.SelectExpiryItems();
            gvItems.DataSource = dtbl;
            gvItems.DataBind();
            ViewState[DirectPR] = dtbl;


            TotalAmount(dtbl);
            FillReasonddl();

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


        Populate.FillPopUpGrid((GridView)CommonPopup1.FindControl("gdvSearch"), "Supplier", "ID_Supplier as ID,SupplierName as Name", "ID_Supplier", "ID_Supplier", "FK_Client=" + hfClient.Value.Split(',')[0] + " AND (ID_Supplier =" + Refno + " OR 0=" + Refno + ")" + " AND  Active=1  AND Cancelled=0 and SupplierName ", strName, null);
        Populate = null;

        if (txtSupplier.Text == string.Empty)
        {
            lblPopupHeader.Text = "Supplier";
            mpCommon.Show();
        }
        //if (Refno == "0" && strName == "")
        //{
        //    lblPopupHeader.Text = "Supplier";
        //    mpCommon.Show();
        //}
        //else
        //{
        //    //SetDetails(strName, Refno);
        //    txtSupRefno.Text = Refno;
        //    txtSupplier.Text = strName;
        //}
       
    }
    private void ClearSupplier()
    {
        txtSupplier.Text = "";
        txtSupRefno.Text = "";
    }
    public void ClearAll()
    {
        ViewState[PurchaseReturnItems] = null;
        btnApprove.Visible = false;
        //gvPurchaseItems.DataSource = null;
        //gvPurchaseItems.DataBind();
        ddlStore.Enabled = false;
        ddlBillType.Enabled = true; ddlBillType.SelectedIndex = 0;
        // PanelHead.Enabled = true;
        divAdd.Style.Add("display", "block;");
        divView.Style.Add("display", "none;");
        btnSave.Visible = true;
        hfQtyValidation.Value = "0"; ddlExpiryMethod.SelectedIndex = 0;
        ddlExpiryMethod.Attributes.Remove("disabled"); txtExpiry.Attributes.Remove("disabled");
        Editable = false;
        ClearItems();
        ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "ClearAdd();", true);
        Transactionchecking();
    }
    private void DisplayDiv()
    {


        //gvPurchaseItems.Visible = false;
        divItemPurchaseReturn.Style.Add("display", "block");
        // liBillType.Visible = false;
        liCategory.Visible = true;

        divItems.Visible = true;
        liinsert.Visible = true;

        //gvPurchaseItems.DataSource = null;
        //gvPurchaseItems.DataBind();
        ViewState[PurchaseReturnItems] = null;
        //gvpi.Visible = false;
        imgbtnSupplier.Enabled = true;

    }

    private void ClearItems()
    {
        ViewState[DirectPR] = null;
        gvItems.DataSource = null;
        gvItems.DataBind();
        ViewState[PurchaseReturnItems] = null;
        //gvPurchaseItems.DataSource = null;
        //gvPurchaseItems.DataBind();
        ddlStore.Enabled = false; txtOtherChrg.Text = "0";
        txtTotalAmount.Text = "0";
        txtNetAmount.Text = "0";
        txtDiscount.Text = "0";
        txtDiscountAmt.Text = "0";
        txtRoundOff.Text = "0";
        txtpurinvoiceNo.Text = "";
        txtpurinvoiceDate.Text = "";
        Editable = false;
        DisplayDiv();
        hfEditedRow.Value = "";
    }
    private void Transactionchecking()
    {
        BlFunction blfun = new BlFunction();
        blfun.Module = "PURRET";
        TransactionID = blfun.GetTransactionID("PURRET", hfuserName.Value);
        ViewState["TransactionID"] = TransactionID;
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
        blpurchase.Expiry = true;
        dtbl = blpurchase.SelectAllData();
        gvPurchaseReturn.DataSource = dtbl;
        gvPurchaseReturn.DataBind();
        gvPurchaseReturn.Columns[5].Visible = false;
        // gvPurchaseReturn.Columns[7].Visible = false;
        blpurchase = null;

    }
    public void SetReadonly()
    {
        txtDate.Attributes.Add("readonly", "readonly");
        txtFromDate.Attributes.Add("readonly", "readonly");
        txtTodate.Attributes.Add("readonly", "readonly"); txtExpiry.Attributes.Add("readonly", "readonly");
        txtDate.Text = txtFromDate.Text = txtTodate.Text = txtExpiry.Text = DateTime.Now.ToString("dd/MM/yyyy");
        txtTotalAmount.Attributes.Add("readonly", "readonly");
        //txtDiscountAmt.Attributes.Add("readonly", "readonly");
        txtNetAmount.Attributes.Add("readonly", "readonly");
        txtRoundOff.Attributes.Add("readonly", "readonly");
        //txtSupplier.Attributes.Add("readonly", "readonly");
        txtActqty.Attributes.Add("readonly", "readonly");
        //txtTotalAmountItem.Attributes.Add("readonly", "readonly");
        txtQuantity.Attributes["onfocus"] = "javascript:this.select();";
        txtFree.Attributes["onfocus"] = "javascript:this.select();";
        txtPurchaseRateItem.Attributes["onfocus"] = "javascript:this.select();";//nj
        txtPurDiscPer.Attributes["onfocus"] = "javascript:this.select();";//nj
        txtMRP.Attributes["onfocus"] = "javascript:this.select();";//nj
        txtItemExpDate.Attributes.Add("readonly", "readonly");//nj
        txtGSTINT.Attributes.Add("readonly", "readonly");//nj 
        txtNetPur.Attributes.Add("readonly", "readonly");//nj  
        txtMRP.Attributes.Add("readonly", "readonly");//nj  
        txtSalesPrice.Attributes.Add("readonly", "readonly");//nj  
        txtOtherChrg.Attributes["onfocus"] = "javascript:this.select();";
        txtDiscountAmt.Attributes["onfocus"] = "javascript:this.select();";

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
    protected void BindData()
    {
        Populate populate = new Populate();
        populate.FillDropdownList(ddlStore, "Store", "StoName", "ID_Store", "StoName", " Active='True' and Cancelled=0   and FK_Client = " + hfClient.Value, "");
        populate.FillDropdownList(ddlBillType, "BillType", "TypeName", "ID_BillType", "TypeName", " Cancelled=0 AND BillType=2  and FK_Client = " + hfClient.Value, "");
        populate.FillDropdownList(ddlSearchSupplier, "Supplier", "SupplierName", "ID_Supplier", "SupplierName", "  Active='True' and Cancelled=0 AND  FK_Client = " + hfClient.Value, "");
        populate.FillDropdownList(ddlSearchStore, "Store", "StoName", "ID_Store", "StoName", "   Active='True' and Cancelled=0  and FK_Client = " + hfClient.Value, "");
        populate.FillDropdownList(ddlCategory, "Category", "CategoryName", "ID_Category", "CategoryName", " Cancelled=0 AND FK_Client= " + hfClient.Value.Split(',')[0], "");

        populate.FillDropdownList(ddlReasons, "Reason", "Reason", "ID_Reason", "ID_Reason", " ReasonMode=1 AND FK_Client = " + hfClient.Value, "");
        populate.FillDropdownList(ddlUnit, "Units", "UnitName", "ID_Units", "UnitName", "Cancelled=0 and FK_Client= " + hfClient.Value.Split(',')[0], "");
        populate.FillDropdownList(ddlSubunit, "SubUnit", "SubUnitName", "ID_SubUnits", "SubUnitName", "Cancelled=0 and FK_Client= " + hfClient.Value.Split(',')[0], "");

        populate = null;
        txtDate.Text = DateTime.Now.ToShortDateString();
    }


    protected void ddlSubunit_SelectedIndexChanged(object sender, EventArgs e)
    {
        CalculateActualQuantity();
    }
    protected void gvItems_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        //if (e.Row.RowType == DataControlRowType.DataRow)
        //{
        //    FillReasonddl();
        //    //((DropDownList)e.Row.FindControl("ddlReason")).SelectedValue = ddlReasons.SelectedValue;

        //}
    }
    protected void gvItems_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        try
        {
            //

            if (gvItems.Rows.Count > 0)
            {
                //DataTable dtbl = new DataTable();
                if (ViewState[DirectPR] != null)
                {
                    hfItemId.Value = gvItems.DataKeys[e.NewSelectedIndex]["FK_Items"].ToString() == "" ? "0" : gvItems.DataKeys[e.NewSelectedIndex]["FK_Items"].ToString();
                    hdfFkItem.Value = hfItemId.Value;
                    txtitem.Text = gvItems.Rows[e.NewSelectedIndex].Cells[1].Text;
                    ddlUnit.SelectedValue = Convert.ToString(((Label)gvItems.Rows[e.NewSelectedIndex].FindControl("lblgvFK_Units")).Text) == "" ? "0" : Convert.ToString(((Label)gvItems.Rows[e.NewSelectedIndex].FindControl("lblgvFK_Units")).Text);
                    //ddlCategory.SelectedValue = Convert.ToString(((Label)gvItems.Rows[e.NewSelectedIndex].FindControl("lblgvFK_Category")).Text) == "" ? "0" : Convert.ToString(((Label)gvItems.Rows[e.NewSelectedIndex].FindControl("lblgvFK_Category")).Text);
                    //ddlSubunit.SelectedValue = Convert.ToString(((Label)gvItems.Rows[e.NewSelectedIndex].FindControl("lblgvFK_SubUnit")).Text) == "" ? "0" : Convert.ToString(((Label)gvItems.Rows[e.NewSelectedIndex].FindControl("lblgvFK_SubUnit")).Text);
                    txtBatch.Text = gvItems.Rows[e.NewSelectedIndex].Cells[3].Text;
                    txtItemExpDate.Text = gvItems.Rows[e.NewSelectedIndex].Cells[4].Text;
                    //txtHSN.Text = Convert.ToString(((Label)gvItems.Rows[e.NewSelectedIndex].FindControl("lblgvhsn")).Text);
                    txtQuantity.Text = gvItems.Rows[e.NewSelectedIndex].Cells[5].Text;
                    txtFree.Text = gvItems.Rows[e.NewSelectedIndex].Cells[7].Text;
                    txtActqty.Text = Convert.ToString(((Label)gvItems.Rows[e.NewSelectedIndex].FindControl("lblgvActQty")).Text);
                    txtPurchaseRateItem.Text = Convert.ToString(((Label)gvItems.Rows[e.NewSelectedIndex].FindControl("lblPurchaserate")).Text);
                    cbxIncludeTax.Checked = Convert.ToBoolean(((Label)gvItems.Rows[e.NewSelectedIndex].FindControl("lblgvIncludeptax")).Text);
                    //txttaxAmount.Text = Convert.ToString(((Label)gvItems.Rows[e.NewSelectedIndex].FindControl("lblgvPurchaseTax")).Text);
                    cbxDiscountTax.Checked = Convert.ToBoolean(((Label)gvItems.Rows[e.NewSelectedIndex].FindControl("lblgvDiscountOntax")).Text);
                    txtPurDiscPer.Text = Convert.ToString(((Label)gvItems.Rows[e.NewSelectedIndex].FindControl("lblgvPurDiscPer")).Text);
                    //txtPurDiscAmnt.Text = Convert.ToString(((Label)gvItems.Rows[e.NewSelectedIndex].FindControl("lblgvPurchaseDiscAmount")).Text);
                    //txtNetPur.Text = Convert.ToString(((Label)gvItems.Rows[e.NewSelectedIndex].FindControl("lblgvNetPurchase")).Text);
                    txtMRP.Text = Convert.ToString(((Label)gvItems.Rows[e.NewSelectedIndex].FindControl("lblgvMRP")).Text);
                    hfMRP.Value = Convert.ToString(((Label)gvItems.Rows[e.NewSelectedIndex].FindControl("lblgvMRP")).Text);
                    txtSalesPrice.Text = Convert.ToString(((Label)gvItems.Rows[e.NewSelectedIndex].FindControl("lblgvSalesPrice")).Text);
                    hdfFKStock.Value = gvItems.DataKeys[e.NewSelectedIndex]["FK_Stock"].ToString() == "" ? "0" : gvItems.DataKeys[e.NewSelectedIndex]["FK_Stock"].ToString();

                    //hfItemId.Value = Convert.ToString(e.NewSelectedIndex);
                    //HfItemsID.Value = Convert.ToString(((Label)gvItems.Rows[e.NewSelectedIndex].FindControl("lblgvFK_Items")).Text) == "" ? "0" : Convert.ToString(((Label)gvItems.Rows[e.NewSelectedIndex].FindControl("lblgvFK_Items")).Text);
                    hfItem.Value = Convert.ToString(((Label)gvItems.Rows[e.NewSelectedIndex].FindControl("lblgvItem")).Text);
                    txtpurinvoiceNo.Text = Convert.ToString(((Label)gvItems.Rows[e.NewSelectedIndex].FindControl("lblgvpurInvoiceno")).Text);
                    string Purdate = Convert.ToString(((Label)gvItems.Rows[e.NewSelectedIndex].FindControl("lblgvpurInvoiceDate")).Text);
                    if (Purdate == "01/01/1900" || Purdate == "01/01/1900 00:00:00")
                    {
                        txtpurinvoiceDate.Text = "";
                    }
                    else
                    {
                        txtpurinvoiceDate.Text = Convert.ToString(((Label)gvItems.Rows[e.NewSelectedIndex].FindControl("lblgvpurInvoiceDate")).Text);
                    }
                    txtPurDiscAmnt.Text = Convert.ToString(((Label)gvItems.Rows[e.NewSelectedIndex].FindControl("lblPurchaseDiscAmt")).Text);
                    
                    //decimal PurRate = Convert.ToDecimal(gvItems.Rows[e.NewSelectedIndex].Cells[8].Text);
                    //decimal AcPurRate = Convert.ToDecimal(((Label)gvItems.Rows[e.NewSelectedIndex].FindControl("lblgvActualPurRate")).Text);
                    //hfSingleQty.Value = Convert.ToString(PurRate - AcPurRate);
                    gvItems.Enabled = true;
                    // CalculateActualQuantity();
                    //ddlItemsFill();
                    hfEditedRow.Value = e.NewSelectedIndex.ToString();
                    ddlReasons.SelectedValue = ((DropDownList)gvItems.Rows[e.NewSelectedIndex].FindControl("ddlReason")).SelectedValue;
                    Editable = true;
                    btnItem_Click(sender, e);

                    liinsert.Visible = true;
                    divItems.Visible = true;



                }
            }


        }

        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "toastr.warning('" + ex.Message + "')", true);
        }
    }

    protected void ddlReasons_SelectedIndexChanged(object sender, EventArgs e)
    {
        DataTable dt = (DataTable)ViewState[DirectPR];
        if (dt != null)
        {
            for (int i = 0; i < dt.Rows.Count; i++)
            {

                //Populate populate = new Populate();
                //populate.FillDropdownList(((DropDownList)gvItems.Rows[i].FindControl("ddlReason")), "Reason", "Reason", "ID_Reason", "ID_Reason", "ReasonMode=1  AND FK_Client = " + hfClient.Value, "");


                //((DropDownList)gvItems.Rows[i].FindControl("ddlReason")).SelectedValue = ddlReasons.SelectedValue;

                //  foreach(var dt.r
                //= ddlReasons.SelectedValue.ToString();
                dt.Rows[i]["FK_Reason"] = ddlReasons.SelectedValue;

            }
        }
        gvItems.DataSource = dt;
        gvItems.DataBind(); FillReasonddl();
    }
    protected void ddlReason_SelectedIndexChanged(object sender, EventArgs e)
    {
        DropDownList ddl = (DropDownList)sender;
        GridViewRow row = (GridViewRow)ddl.Parent.Parent;
        DataTable dt = (DataTable)ViewState[DirectPR];
        for (int i = 0; i < dt.Rows.Count; i++)
        {

            if (dt.Rows[i]["FK_Stock"].ToString() == gvItems.DataKeys[row.RowIndex]["FK_Stock"].ToString())
            {
                dt.Rows[i]["FK_Reason"] = ddl.SelectedValue;
                break;
            }


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
                if (approved)
                {
                    imgDelete.Enabled = false; imgDelete.ToolTip = "Can't delete approved details";
                }
                else
                {
                    imgDelete.Enabled = true; imgDelete.ToolTip = "Delete";
                }
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "toastr.warning('" + ex.Message + "')", true);
        }
    }
    protected void TotalAmountEdit(DataTable dtbl)
    {
        try
        {
            if (dtbl.Rows.Count > 0)
            {
                txtTotalAmount.Text = dtbl.Rows[0]["TotalAmt"].ToString();
                txtNetAmount.Text = dtbl.Rows[0]["NetAmount"].ToString();
                txtDiscountAmt.Text = dtbl.Rows[0]["DiscAmt"].ToString();
                txtRoundOff.Text = dtbl.Rows[0]["Roundoff"].ToString();
                txtOtherChrg.Text = dtbl.Rows[0]["OtherCharge"].ToString();
                //txtTotalAmount.Text = Math.Round(decimal.Parse((dtbl.Compute("Sum(TotalAmt)", "")).ToString()), 2).ToString();
                //txtVAT.Text = Math.Round(decimal.Parse((dtbl.Compute("Sum(TotalVAT)", "")).ToString()), 2).ToString();
                // txtTotCESS.Text = Math.Round(decimal.Parse((dtbl.Compute("Sum(TotalCESS)", "")).ToString()), 2).ToString();
             
              //  ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "Total()", true);






                //txtTotalAmount.Text = dtbl.Rows[0]["TotalAmt"].ToString();
                //txtTotalAmount.Text = (Math.Round(Convert.ToDecimal(txtTotalAmount.Text), 2)).ToString();
                ////decimal DiscountAmt = Math.Round(((Convert.ToDecimal(txtTotalAmount.Text)) * (Convert.ToDecimal(txtDiscount.Text)) / 100), 2);
                //txtDiscountAmt.Text = dtbl.Rows[0]["DiscountAmt"].ToString();
                //txtDiscountAmt.Text = (Math.Round(Convert.ToDecimal(txtDiscountAmt.Text), 2)).ToString();

                //txtNetAmount.Text = dtbl.Rows[0]["NetAmount"].ToString();
                //txtNetAmount.Text = (Math.Round(Convert.ToDecimal(txtNetAmount.Text), 2)).ToString();
                //txtRoundOff.Text = dtbl.Rows[0]["Roundoff"].ToString();
                //txtRoundOff.Text = (Math.Round(Convert.ToDecimal(txtRoundOff.Text), 2)).ToString();

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
    protected void Supplieredit()
    {
        try
        {
          
            ((TextBox)CommonPopup1.FindControl("txtName")).Text = txtSupplier.Text.Trim();
            ((TextBox)CommonPopup1.FindControl("txtRefno")).Text = txtSupRefno.Text.Trim();
            ((HiddenField)CommonPopup1.FindControl("hfColumn")).Value = "Name";
            SupplierFill2(1);
            divAdd.Style.Add("display", "block");
            divView.Style.Add("display", "none");
        }
        catch (Exception ex)
        {
        }
    }

    public void SupplierFill2(int pageIndex)
    {
       // ClearSupplier();

        Populate Populate = new Populate();
        CommonPopup1.CallingFunction = "SupplierFill";
        string strName = ((TextBox)CommonPopup1.FindControl("txtName")).Text;
        string Refno = ((TextBox)CommonPopup1.FindControl("txtRefno")).Text;
        string searchCol = ((HiddenField)CommonPopup1.FindControl("hfColumn")).Value;
        Refno = Refno == string.Empty || Refno == null ? "0" : Refno;


        Populate.FillPopUpGrid((GridView)CommonPopup1.FindControl("gdvSearch"), "Supplier", "ID_Supplier as ID,SupplierName as Name", "ID_Supplier", "ID_Supplier", "FK_Client=" + hfClient.Value.Split(',')[0] + " AND (ID_Supplier =" + Refno + " OR 0=" + Refno + ")" + " AND  Active=1  AND Cancelled=0 and SupplierName ", strName, null);
        Populate = null;


        if (Refno == "0" && strName == "")
        {
            lblPopupHeader.Text = "Supplier";
            mpCommon.Show();
        }
        else
        {
            //SetDetails(strName, Refno);
            txtSupRefno.Text = Refno;
            txtSupplier.Text = strName;
        }

    }
}