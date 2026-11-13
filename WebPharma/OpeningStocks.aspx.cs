using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebPharma.Business.Masters;
using WebPharma.Business.WebPharma;
using Excel;

public partial class OpeningStocks : System.Web.UI.Page
{
    decimal TotalTaxSales,TaxAmount;
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

    protected void BindData()
    {
        Populate populate = new Populate();

       
        populate.FillDropdownList(ddlStore, "Store", "StoName", "ID_Store", "StoName", " Active='True'  AND Cancelled=0 AND FK_Client= " + hfClient.Value.Split(',')[0], "");
        populate.FillDropdownList(ddlStoreSearch, "Store", "StoName", "ID_Store", "StoName", " Active='True' AND Cancelled=0 AND FK_Client= " + hfClient.Value.Split(',')[0], "");
        populate.FillDropdownList(ddlItem, "Items", "ItemName", "ID_Items", "ItemName", "Cancelled=0 AND FK_Client= " + hfClient.Value.Split(',')[0], "");
        populate.FillDropdownList(ddlCategory, "Category", "CategoryName", "ID_Category", "CategoryName", "Cancelled=0 and  FK_Client= " + hfClient.Value.Split(',')[0], "");
        populate = null;

    }
    protected void CheckUserAccess()
    {
        BlUserAccess bluseraccess = new BlUserAccess();
        Populate populate = new Populate();

        bluseraccess.FK_Users = Convert.ToInt64(hfUser.Value);
        bluseraccess.FK_Pages = 50;
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
    protected void ddlItemsFill()
    {
        if (ddlItem.SelectedIndex > 0)
        {
            BlItems blitems = new BlItems();
            blitems.MasterId = Convert.ToInt64(ddlItem.SelectedValue.ToString());
            blitems.FK_Client = Convert.ToInt64(hfClient.Value);
            blitems.SelectData();
            ddlCategory.SelectedValue = blitems.FK_CategoryFirst.ToString();
            txtItemShortName.Text = blitems.Name.ToString();
            hfExpirydays.Value = blitems.ExpairyValidation.ToString();
            blitems = null;
            BlPurchase blpurchase = new BlPurchase();
            blpurchase.itemID = Convert.ToInt64(ddlItem.SelectedValue.ToString());
            DataTable dtB = blpurchase.SelectGroupID();
            hfGroupID.Value = dtB.Rows[0]["GroupID"].ToString();
            DataTable dt = blpurchase.SelectGriddata();
            foreach (DataRow row in dt.Rows)
            {
                TotalTaxSales += Convert.ToDecimal(row["TaxPercentage"].ToString());
                hfTotTaxsalesPercentage.Value = Convert.ToString(TotalTaxSales);
            }
         
        

      
        }
    }
    public void SetReadonly(params TextBox[] textbox)
    {
        foreach (TextBox txt in textbox)
            txt.Attributes.Add("readonly", "readonly");

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
                BindData();
                hfOpningID.Value = "";
                hfuserName.Value = Session["Username"].ToString();
                ddlStore.SelectedValue = Session["mainstore"].ToString();
                ddlStoreSearch.SelectedValue = Session["mainstore"].ToString();
               // ddlStore.Enabled = false;
                CheckUserAccess();
                txtItemShortName.Enabled = true;
                txtBatch.Enabled = true;
                txtExpiryDate.Enabled = true;
                txtPurchaseRate.Enabled = true;
                txtMRP.Enabled = true;
                txtSalesPrice.Enabled = true;
                txtCurrentStock.Enabled = true;
                ChkIncStax.Enabled = true;
                txttaxAmount.Enabled = true;
                txtSTaxAmount.Enabled = true;
                txtCurrentLiability.Enabled = true;
                txtOpeningLiability.Enabled = true;
                btnAutofill.Enabled = true;
                SetReadonly(txtExpiryDate, txtOpeninLiablity, txtCurLiability);
                
                if (Convert.ToBoolean(hfIsAdd.Value) == false)
                {
                    ((Label)CommonPopupPrivilege.FindControl("popuplabel")).Text = "User does not have permission to Add";
                    mpPrivilege.Show();
                }
                if (Convert.ToBoolean(hfIsUpdate.Value) == false)
                {
                    gvOpening.Columns[9].Visible = false;
                }
                if (Convert.ToBoolean(hfIsDelete.Value) == false)
                {
                    gvOpening.Columns[6].Visible = false;
                }

            }


        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "toastr.warning('" + ex.Message + "')", true);
        }
    }

    protected void SaveData()
    {
        if (ViewState["TempTable"] == null ||
        ((DataTable)ViewState["TempTable"]).Rows.Count == 0)
        {
            ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script",
                "toastr.warning('No items to save.')", true);
            return;
        }

        long status = 0;
        BlOpeningStock blOpening = new BlOpeningStock();
        blOpening.FK_Store = Convert.ToInt64(ddlStore.SelectedValue);
        blOpening.EnteredOn = Convert.ToDateTime(DateTime.Now);
        blOpening.FK_Client = Convert.ToInt64(hfClient.Value);
        blOpening.UserCode = hfuserName.Value;
        ArrayList arlist = new ArrayList();
        DataTable dtbl = (DataTable)ViewState["TempTable"];
        IOpngStock IOpening;
        for (int i = 0; i < dtbl.Rows.Count; i++)
        {
            IOpening = new IOpngStock();
            IOpening.SlNo = Convert.ToInt32(i + 1);
            IOpening.FK_Items = Convert.ToInt64(dtbl.Rows[i]["FK_Items"].ToString());
            IOpening.BatchNo = Convert.ToString(dtbl.Rows[i]["BatchNo"].ToString());
            IOpening.ExpiryDate = Convert.ToDateTime(dtbl.Rows[i]["ExpiryDate"].ToString());
            IOpening.Quantity = Convert.ToDecimal(dtbl.Rows[i]["OpeningStock"].ToString());
            IOpening.PurRate = Convert.ToDecimal(dtbl.Rows[i]["PurchaseRate"].ToString());
            IOpening.PurTaxAmount = Convert.ToDecimal(dtbl.Rows[i]["PurchasetaxAmount"].ToString());
            IOpening.MRP = Convert.ToDecimal(dtbl.Rows[i]["MRP"].ToString());
            IOpening.SalePrice = Convert.ToDecimal(dtbl.Rows[i]["SalPrice"].ToString());
            IOpening.IncludeSTax = Convert.ToBoolean(dtbl.Rows[i]["IncludeSTax"].ToString());
            IOpening.SalesTaxAmount = Convert.ToDecimal(dtbl.Rows[i]["SaletaxAmount"].ToString());
            IOpening.ActualPurRate = Convert.ToDecimal(dtbl.Rows[i]["ActualPurRate"].ToString());
            IOpening.ActualSalePrice = Convert.ToDecimal(dtbl.Rows[i]["ActualSalPrice"].ToString());
            IOpening.ProfitPer = Convert.ToDecimal(dtbl.Rows[i]["ProfitPer"].ToString());
            IOpening.ProfitAmt = Convert.ToDecimal(dtbl.Rows[i]["ProfitAmt"].ToString());
            IOpening.DiscountAmt = Convert.ToDecimal(dtbl.Rows[i]["DiscountAmt"].ToString());
            IOpening.DiscountPer = Convert.ToDecimal(dtbl.Rows[i]["DiscountPer"].ToString());
            IOpening.FK_TaxSettings = Convert.ToInt64(dtbl.Rows[i]["FK_TaxSettings"].ToString());
            IOpening.CurrentStock = Convert.ToDecimal(dtbl.Rows[i]["CurrentStock"].ToString()==""?"0":(dtbl.Rows[i]["CurrentStock"].ToString()));
           
            arlist.Add(IOpening);
            IOpening = null;

        }
        blOpening.xmlOpening = Common.WriteToXml(arlist);
        if (hfID.Value == "")
        {
            if (hfIsAdd.Value.ToLower() == "true")
            {
                status = blOpening.InsertData();
                if (status > 0)
                {
                    ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "toastr.success('Data inserted successfully');alertify.alert('Reference Number:  " + status + "');", true);


                    ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "ClearAdd();", true);
                    ddlStore.SelectedValue = Session["mainstore"].ToString();
                    Clear();
                    //ddlStore.Enabled = false;
                }
                else if (status == -2)
                {
                    Populate.ErrorMessageBox(this, "Cannot insert data, Stock Already Exists");
                    
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
                blOpening.MasterId = Convert.ToInt64(hfID.Value);
                blOpening.FK_Client = Convert.ToInt64(hfClient.Value);
                blOpening.GroupID = Convert.ToInt64(hfGID.Value == "" ? "0" : hfGID.Value);
                status = blOpening.UpdateData();
                if (status > 0)
                {
                    ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "ShowAddDiv();", true);
                    ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "toastr.success('Data Updated successfully');ClearAdd();", true);
                   // GridFill(1);
                    Clear();
                }
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "toastr.warning('User does not have permission to Update')", true);
               // txtShortName.Focus();
            }
        
        }
        blOpening = null;

    }

    protected void imgbtnAdd_Click(object sender, ImageClickEventArgs e)
    {
        ddlStore.SelectedValue = Session["mainstore"].ToString();
        ddlStore.Enabled = true;
        ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "ShowAddDiv();", true);
        ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "ShowAddDiv();", true);
        divAdd.Style.Add("display", "block");
        divView.Style.Add("display", "none");
        ViewState["TempTable"] = null;
        gvItems.DataSource = null;
        gvItems.DataBind();
        txtItemShortName.Enabled = true;
        txtBatch.Enabled = true;
        txtExpiryDate.Enabled = true;
        txtPurchaseRate.Enabled = true;
        txtMRP.Enabled = true;
        txtSalesPrice.Enabled = true;
        txtCurrentStock.Enabled = true;
        ChkIncStax.Enabled = true;
        txttaxAmount.Enabled = true;
        txtSTaxAmount.Enabled = true;
        txtCurrentLiability.Enabled = true;
        txtOpeningLiability.Enabled = true;
        txtOpeninLiablity.Text = "0";
        txtCurLiability.Text = "0";
        if (Convert.ToBoolean(hfIsAdd.Value) == false)
        {
            ((Label)CommonPopupPrivilege.FindControl("popuplabel")).Text = "User does not have permission to Add";
            mpPrivilege.Show();
        }


    }
    protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
    {
        ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "SearchClear();", true);
        ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "SearchClear();", true);
        ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "ClearAdd();  return false;", true);
        divAdd.Style.Add("display", "none");
        divView.Style.Add("display", "block");
        GridFillOpening(1);
        ddlStoreSearch.Focus();

    }
    protected void GridFillOpening(int currentPage)
    {
        BlOpeningStock blopening = new BlOpeningStock();
        blopening.FK_Store = Convert.ToInt64(ddlStoreSearch.SelectedValue);
        blopening.BatchNo = txtSearchBatch.Text;
        blopening.ItemName = txtItemSearch.Text;
        blopening.FK_Client = Convert.ToInt64(hfClient.Value);
        blopening.UserCode = hfuserName.Value;
        DataTable dtbl = new DataTable();
        dtbl = blopening.SelectAllData();
        gvOpening.DataSource = dtbl;
        gvOpening.DataBind();
        blopening = null;
    }
    protected void ddlCategory_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void ddlItem_SelectedIndexChanged(object sender, EventArgs e)
    {

        try
        {
            //
            ddlItemsFill();
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "toastr.warning('" + ex.Message + "')", true);
        }
    }
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        //if(hfAutofill.Value.ToString()!="1")
       // if(gvItems.Rows.Count>0)
        int days = Convert.ToInt32(hfExpirydays.Value.ToString()) + (30);
        DateTime dtNow = Convert.ToDateTime(hfDate.Value);
        DateTime dtExpiry = DateTime.ParseExact(txtExpiryDate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
        DateTime date = dtNow.AddDays(days);
        if (dtExpiry < date)
            ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "alertify.alert('Expirydate should be greater than Item Expiry Days')", true);
        else

        AddData();
        hfExceldata.Value = "0";
        ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "CalculateTotal();", true);
        //if(hfAutofill.Value.ToString()=="1")
        //    ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "alertify.alert('Item Cannot Insert')", true);

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
            dtbl.Columns.Add("SLNO", typeof(int));
            dtbl.Columns.Add("Item");
            dtbl.Columns.Add("FK_Items");
            //dtbl.Columns.Add("FK_Category");
          //  dtbl.Columns.Add("Category");
            dtbl.Columns.Add("BatchNo");
            dtbl.Columns.Add("ExpiryDate");
            dtbl.Columns.Add("PurchaseRate");
            dtbl.Columns.Add("PurchasetaxAmount");
            dtbl.Columns.Add("MRP");
            dtbl.Columns.Add("SalPrice");
            dtbl.Columns.Add("ActualSalPrice");
            dtbl.Columns.Add("ActualPurRate");
            dtbl.Columns.Add("IncludeStax");
            dtbl.Columns.Add("SaletaxAmount");
            dtbl.Columns.Add("OpeningStock");
            dtbl.Columns.Add("CurrentStock");
            dtbl.Columns.Add("OpeningLiability");
            dtbl.Columns.Add("CurrentLiability");
            dtbl.Columns.Add("ProfitPer");
            dtbl.Columns.Add("ProfitAmt");
            dtbl.Columns.Add("DiscountPer");
            dtbl.Columns.Add("DiscountAmt");
            dtbl.Columns.Add("FK_TaxSettings");



        }
        if (hfOpningID.Value != "")
        {

            gvItems.Enabled = true;
            dtbl.Rows.RemoveAt(Convert.ToInt32(hfOpningID.Value));
            gvItems.DataSource = dtbl;
            gvItems.DataBind();
            hfSlNo.Value = hfOpningID.Value;
            hfOpningID.Value = "";
            // ViewState["TempTable"] = dtbl;


        }



        //if (dtbl.Rows.Count > 0)
        //{

        //    for (int i = 0; i < dtbl.Rows.Count; i++)
        //    {
        //        if (dtbl.Rows[i]["FK_Items"].ToString() == ddlItem.SelectedValue.ToString())
        //        {
        //            inCheck = inCheck + 1;
        //            break;
        //        }
        //    }
        //}




        if (inCheck == 0)
        {
            DataRow drow = dtbl.NewRow();
            if (hfSlNo.Value == "")
            {
                if (dtbl.Rows.Count > 0)
                {
                    //  dtbl.DefaultView.Sort = "SLNO DESC";
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
           // DataRow drow = dtbl.NewRow();
           // drow["SLNO"] = rowCount + 1;
            drow["Item"] = ddlItem.SelectedItem.ToString();
            drow["FK_Items"] = ddlItem.SelectedValue.ToString();
           // hfID.Value = ddlItem.SelectedValue.ToString();
          //  drow["FK_Category"] = ddlCategory.SelectedValue.ToString();
           // drow["Category"] = ddlCategory.SelectedItem.ToString();
            drow["BatchNo"] = Convert.ToString(txtBatch.Text);
            drow["FK_TaxSettings"] =(hfGroupID.Value == "" ? "0" : hfGroupID.Value);
            drow["ExpiryDate"] = DateTime.ParseExact(txtExpiryDate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
         
            drow["PurchasetaxAmount"] = Convert.ToDecimal(txttaxAmount.Text == "" ? "0" : txttaxAmount.Text);
            drow["MRP"] = Convert.ToDecimal(txtMRP.Text == "" ? "0" : txtMRP.Text);
          
            drow["IncludeStax"] = Convert.ToBoolean(ChkIncStax.Checked);
            drow["SaletaxAmount"] = Convert.ToDecimal(txtSTaxAmount.Text == "" ? "0" : txtSTaxAmount.Text);
            drow["OpeningStock"] = Convert.ToDecimal(txtQuantity.Text == "" ? "0" : txtQuantity.Text);
            drow["CurrentStock"] = Convert.ToDecimal(txtCurrentStock.Text == "" ? "0.00" : txtCurrentStock.Text);
            //drow["OpeningLiability"] = Convert.ToDecimal(txtQuantity.Text == "" ? "0" : txtQuantity.Text) * Convert.ToDecimal(txtSalesPrice.Text == "" ? "0" : txtSalesPrice.Text);
            drow["OpeningLiability"] = Convert.ToDecimal(txtOpeningLiability.Text == "" ? "0" : txtOpeningLiability.Text);
          //  drow["CurrentLiability"] = Convert.ToDecimal(txtCurrentStock.Text == "" ? "0" : txtCurrentStock.Text) * Convert.ToDecimal(txtSalesPrice.Text == "" ? "0" : txtSalesPrice.Text);
            if (ChkIncStax.Checked == true)
            {
                drow["ActualSalPrice"] = Convert.ToDecimal(txtSalesPrice.Text == "" ? "0" : txtSalesPrice.Text) -
                    Convert.ToDecimal(txtSTaxAmount.Text == "" ? "0" : txtSTaxAmount.Text);
                drow["SalPrice"] = Convert.ToDecimal(txtSalesPrice.Text == "" ? "0" : txtSalesPrice.Text);
            }
            else
            {
                drow["ActualSalPrice"] = Convert.ToDecimal(txtSalesPrice.Text == "" ? "0" : txtSalesPrice.Text);
                drow["SalPrice"] = Convert.ToDecimal(txtSalesPrice.Text == "" ? "0" : txtSalesPrice.Text) +
                     Convert.ToDecimal(txtSTaxAmount.Text == "" ? "0" : txtSTaxAmount.Text);
            }
            drow["ActualPurRate"] = Convert.ToDecimal(txtPurchaseRate.Text == "" ? "0" : txtPurchaseRate.Text);
            drow["PurchaseRate"] = Convert.ToDecimal(txtPurchaseRate.Text == "" ? "0" : txtPurchaseRate.Text) +
                 Convert.ToDecimal(txttaxAmount.Text == "" ? "0" : txttaxAmount.Text);

            drow["CurrentLiability"] = Convert.ToDecimal(txtCurrentLiability.Text == "" ? "0" : txtCurrentLiability.Text);
            // ── Discount ────────────────────────────────────────────────────────────────
            decimal decMRP = Convert.ToDecimal(txtMRP.Text == "" ? "0" : txtMRP.Text);
            decimal decSalesPrice = Convert.ToDecimal(txtSalesPrice.Text == "" ? "0" : txtSalesPrice.Text);
            decimal decPurRate = Convert.ToDecimal(txtPurchaseRate.Text == "" ? "0" : txtPurchaseRate.Text);
            decimal decTaxAmount = Convert.ToDecimal(txttaxAmount.Text == "" ? "0" : txttaxAmount.Text);

            drow["DiscountAmt"] = decMRP - decSalesPrice;

            decimal discountPer = 0;
            if (decMRP > 0)
            {
                discountPer = (decMRP - decSalesPrice * 100) / decMRP;
                if (discountPer < 0) discountPer = 0;
            }
            drow["DiscountPer"] = discountPer;

            // ── Profit ──────────────────────────────────────────────────────────────────
            decimal profitAmt = decSalesPrice - (decPurRate + decTaxAmount);
            drow["ProfitAmt"] = profitAmt;

            decimal profitPer = 0;
            if (decPurRate > 0)
            {
                profitPer = (profitAmt * 100) / (decPurRate + decTaxAmount);
                if (profitPer < 0) profitPer = 0;
            }
            drow["ProfitPer"] = profitPer;
            dtbl.Rows.InsertAt(drow, rowCount);
           // dtbl.DefaultView.Sort = "SLNO";
            dtbl = dtbl.DefaultView.ToTable();
            gvItems.DataSource = dtbl;
            gvItems.DataBind();
            ViewState["TempTable"] = dtbl;
            hfSlNo.Value = "";
            txtItemShortName.Enabled = true;
            txtBatch.Enabled = true;
            txtPurchaseRate.Enabled = true;
            txtMRP.Enabled = true;
            txtSalesPrice.Enabled = true;
            txtCurrentStock.Enabled = true;
            ChkIncStax.Enabled = true;
            txttaxAmount.Enabled = true;
            txtSTaxAmount.Enabled = true;
            txtCurrentLiability.Enabled = true;
            txtOpeningLiability.Enabled = true;
            txtItemShortName.Focus();
            ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "ItemClear();", true);
        }
        else
        {
            ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "alertify.alert('Item already exists with same batch no')", true);
        }

    }
    protected void btnClearAdd_Click(object sender, EventArgs e)
    {
        txtItemShortName.Enabled = true;
        txtBatch.Enabled = true;
        txtPurchaseRate.Enabled = true;
        txtMRP.Enabled = true;
        txtSalesPrice.Enabled = true;
        txtCurrentStock.Enabled = true;
        ChkIncStax.Enabled = true;
        txttaxAmount.Enabled = true;
        txtSTaxAmount.Enabled = true;
        txtCurrentLiability.Enabled = true;
        txtOpeningLiability.Enabled = true;
        txtItemShortName.Focus();
    }
    protected void btnClear_Click(object sender, EventArgs e)
    {
        ddlStore.Enabled = false;
        Clear();
    }
    public void Clear()
    {
        ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "ClearAdd();", true);
        ViewState["TempTable"] = null;
        txtOpeninLiablity.Text = "0";
        txtCurLiability.Text = "0";
        gvItems.DataSource = null;
        gvItems.DataBind();
        btnAutofill.Enabled = true;
        btnShowData.Enabled = true;
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        GridFillOpening(1);
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
            blpurchase.itemID = Convert.ToInt64(ddlItem.SelectedValue.ToString() == "" ? "0" : ddlItem.SelectedValue.ToString());
            dt = blpurchase.SelectGriddata();
            dt.Columns.Add("TaxAmount");
            foreach (DataRow row in dt.Rows)
            {
                TaxAmount = 0;
                if (Convert.ToBoolean(row["TaxIntraState"]) == true)
                {
                    TaxAmount = Math.Round(Convert.ToDecimal(row["TaxPercentage"] == "" ? "0" : row["TaxPercentage"]) / Convert.ToDecimal(hfTotTaxsalesPercentage.Value == "" ? "0" : hfTotTaxsalesPercentage.Value) * (Convert.ToDecimal(txtSTaxAmount.Text == "" ? "0" : txtSTaxAmount.Text)), 4);

                }

                row["TaxAmount"] = TaxAmount;
            }
            gvNew.DataSource = dt;
            gvNew.DataBind();
            gvNew.Columns[1].Visible = false;
            lblPopupH.Text = "Sales Tax";
            mpGrid.Show();
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
            // Guard: store must be selected
            if (ddlStore.SelectedValue == "0")
            {
                ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script",
                    "alertify.alert('Please select a Store before submitting.')", true);
                return;
            }

            // Guard: grid must have at least one item
            if (ViewState["TempTable"] == null ||
                ((DataTable)ViewState["TempTable"]).Rows.Count == 0)
            {
                ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script",
                    "alertify.alert('Please add at least one item before submitting.')", true);
                return;
            }

            SaveData();
        }
        catch (Exception ex)
        {
            Populate.WarningMsg(this, ex.ToString());
        }
    }
    protected void gvOpening_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvOpening.PageIndex = e.NewPageIndex;
        GridFillOpening(e.NewPageIndex);
    }
    protected void gvOpening_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        ddlStore.Enabled = false;
        if (gvItems.Rows.Count > 0)
            btnAutofill.Enabled = false;
        BlOpeningStock blOpening = new BlOpeningStock();
        DataTable dtbl = new DataTable();
        blOpening.FK_Store = Convert.ToInt64(ddlStore.SelectedValue);
        blOpening.MasterId = Convert.ToInt64(gvOpening.DataKeys[e.NewSelectedIndex]["ID_Stock"].ToString());
        hfGID.Value = gvOpening.DataKeys[e.NewSelectedIndex]["GroupID"].ToString();
       // ViewState["MasterIDTaxDetails"] = Convert.ToInt64(gvOpening.DataKeys[e.NewSelectedIndex]["ID_Stock"].ToString());
        blOpening.FK_Client = Convert.ToInt64(hfClient.Value);
        divAdd.Style.Add("display", "block");
        divView.Style.Add("display", "None");

        DataTable dt = blOpening.SelectAllGridData();
        gvItems.DataSource = dt;
        gvItems.DataBind();
        ViewState["TempTable"] = dt;
        
        ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "CalculateTotal();", true);
    }
    protected void gvItems_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        if (gvItems.Rows.Count > 0)
        {
            btnAutofill.Enabled = false;
            BlItems blItm=new BlItems();
            
            DataTable dtbl = new DataTable();
            if (ViewState["TempTable"] != null)
            {
                dtbl = (DataTable)ViewState["TempTable"];
                ddlItem.SelectedValue = Convert.ToString(((Label)gvItems.Rows[e.NewSelectedIndex].FindControl("lblgvFK_Items")).Text);
                txtItemShortName.Text = Convert.ToString(((Label)gvItems.Rows[e.NewSelectedIndex].FindControl("lblgvItem")).Text);
                txtBatch.Text = Convert.ToString(((Label)gvItems.Rows[e.NewSelectedIndex].FindControl("lblgvBatchNo")).Text);
                txtExpiryDate.Text = Convert.ToString(((Label)gvItems.Rows[e.NewSelectedIndex].FindControl("lblgvExpiryDate")).Text).Substring(0, 10);
                blItm.MasterId=Convert.ToInt64(ddlItem.SelectedValue);
                blItm.SelectData();
                hfExpirydays.Value = Convert.ToString(blItm.ExpairyValidation);
                txtPurchaseRate.Text = Convert.ToString(((Label)gvItems.Rows[e.NewSelectedIndex].FindControl("lblgvPurchaseRate")).Text);
                txtMRP.Text = Convert.ToString(((Label)gvItems.Rows[e.NewSelectedIndex].FindControl("lblgvMRP")).Text);
                txtSalesPrice.Text = Convert.ToString(((Label)gvItems.Rows[e.NewSelectedIndex].FindControl("lblgvSalesPrice")).Text);
                txtQuantity.Text = Convert.ToString(((Label)gvItems.Rows[e.NewSelectedIndex].FindControl("lblgvquty")).Text);
                txtCurrentStock.Text = Convert.ToString(((Label)gvItems.Rows[e.NewSelectedIndex].FindControl("lblgvCurrentStock")).Text);
                decimal SalesPrice = Convert.ToDecimal(((Label)gvItems.Rows[e.NewSelectedIndex].FindControl("lblgvSalesPrice")).Text);
                decimal ActualSalesprice = Convert.ToDecimal(((Label)gvItems.Rows[e.NewSelectedIndex].FindControl("lblgvActualSalesPrice")).Text);
                decimal PurchaseRate = Convert.ToDecimal(((Label)gvItems.Rows[e.NewSelectedIndex].FindControl("lblgvPurchaseRate")).Text);
                decimal ActualPurchaseRate = Convert.ToDecimal(((Label)gvItems.Rows[e.NewSelectedIndex].FindControl("lblgvActualPurRate")).Text);
                decimal quantity = Convert.ToDecimal(((Label)gvItems.Rows[e.NewSelectedIndex].FindControl("lblgvquty")).Text);
                decimal CurrentStock = Convert.ToDecimal(((Label)gvItems.Rows[e.NewSelectedIndex].FindControl("lblgvCurrentStock")).Text);
                txtSTaxAmount.Text = (SalesPrice - ActualSalesprice).ToString();
                txttaxAmount.Text = (PurchaseRate - ActualPurchaseRate).ToString();
                decimal ActualOpning = Convert.ToDecimal(((Label)gvItems.Rows[e.NewSelectedIndex].FindControl("lblgvquty")).Text);
                decimal CurrentStk = Convert.ToDecimal(((Label)gvItems.Rows[e.NewSelectedIndex].FindControl("lblgvCurrentStock")).Text);
                txtOpeningLiability.Text = Math.Round((quantity * SalesPrice),4).ToString();
                txtCurrentLiability.Text = Math.Round((CurrentStock * SalesPrice),4).ToString();
                decimal Qty=Convert.ToDecimal(txtQuantity.Text.ToString());
               // gvItems.Enabled = false;
                if (hfAutofill.Value == "1")
                {

                    txtCurrentStock.Text = Math.Round((CurrentStk + (ActualOpning - Qty)), 4).ToString();
                       // Convert.ToString(CurrentStk) + (ActualOpning - Qty);
                }
                //if (hfExceldata.Value == "1")
                //{
                //    txtExpiryDate.Enabled = true;
                //    txtBatch.Enabled = true;
                //    txtPurchaseRate.Enabled = true;
                //    txtMRP.Enabled = true;
                //    txtSalesPrice.Enabled = true;
                //    txtCurrentStock.Enabled = true;
                //    ChkIncStax.Enabled = true;
                //    txttaxAmount.Enabled = true;
                //    txtSTaxAmount.Enabled = true;
                //    txtCurrentLiability.Enabled = true;
                //    txtOpeningLiability.Enabled = true;
                //    txtItemShortName.Enabled = false;
                //}
                //else
                //{
                //    txtExpiryDate.Enabled = false;
                //}
                //else
                //{
                //    txtItemShortName.Enabled = false;
                //    txtBatch.Enabled = false;
                //    txtPurchaseRate.Enabled = false;
                //    txtMRP.Enabled = false;
                //    txtSalesPrice.Enabled = false;
                //    txtCurrentStock.Enabled = false;
                //    ChkIncStax.Enabled = false;
                //    txttaxAmount.Enabled = false;
                //    txtSTaxAmount.Enabled = false;
                //    txtCurrentLiability.Enabled = false;
                //    txtOpeningLiability.Enabled = false;
                //}
                ddlItemsFill();
              
                hfOpningID.Value = Convert.ToString(e.NewSelectedIndex);
                hfID.Value = dtbl.Rows[0]["ID_Stock"].ToString();
            }
        }

    }
    protected void btnAutofill_Click(object sender, EventArgs e)
    {

        ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "CalculateTotal();", true);
        DataTable dtAutogrid = new DataTable();
        BlOpeningStock blopening = new BlOpeningStock();
        blopening.FK_Store = Convert.ToInt64(ddlStore.SelectedValue);
        dtAutogrid  = blopening.SelectAllData();
        gvItems.DataSource = dtAutogrid;
        gvItems.DataBind();
        ViewState["TempTable"] = dtAutogrid;
        ddlStore.Enabled = false;
        hfAutofill.Value = "1";
        if (gvItems.Rows.Count > 0)
        {
            btnAutofill.Enabled = false;
        }
            
    }
    private void GridDetails()
    {
        DataTable dtbl = (DataTable)ViewState["TempTable"];
        for (int i = 0; i < dtbl.Rows.Count; i++)
        {

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
                   
                    ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "CalculateTotal();", true);
                    if (gvItems.Rows.Count > 0)
                    {
                        btnAutofill.Enabled = false;
                    }
                    else
                    {
                        btnAutofill.Enabled = true;
                    }

                   // CalculateTotals();
                }

            }
            ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "ShowAddDiv();", true);

        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "toastr.warning('" + ex.Message + "')", true);
        }
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
                            //dsExcel.Tables.Add(dt);
                        }
                    }
                }

               

            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "toastr.error('Excel format does not support: Error 1')", true);
                // ErrorMessage.WriteErrorLog(Page.ToString() + " : " + ex);
            }
            //  dsExcel.Tables[0].TableName = "Items";
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
                    // ErrorMessage.WriteErrorLog(Page.ToString() + " : " + ex);
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

            // if (dt.Columns.Contains("Code") || dt.Columns.Contains("Name") || dt.Columns.Contains("SubNo")|| dt.Columns.Contains("SubName")|| dt.Columns.Contains("Amount")|| dt.Columns.Contains("Type")|| dt.Columns.Contains("Narration")|| dt.Columns.Contains("Party")|| dt.Columns.Contains("ChequeDate")|| dt.Columns.Contains("ChequeNo")|| dt.Columns.Contains("TransDate")|| dt.Columns.Contains("EffectDate")|| dt.Columns.Contains("BillNo")|| dt.Columns.Contains("BillDate"))
            if (dt.Columns.Contains("ItemCode") ||dt.Columns.Contains("Quantity")  || dt.Columns.Contains("MRP")|| dt.Columns.Contains("BatchNo")||dt.Columns.Contains("ExpiryDate")||dt.Columns.Contains("SalePrice") )
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
    protected string ValidateExcel(DataTable dt)
    {
        ArrayList arrColumns = new ArrayList();
        dt.TableName = "BlOpeningStock";
        MemoryStream str = new MemoryStream();
        dt.WriteXml(str, true);
        str.Seek(0, SeekOrigin.Begin);
        StreamReader sr = new StreamReader(str);
        string xmlstr;
        xmlstr = sr.ReadToEnd();
        return (xmlstr);
    }
    private void FillGrid()
    {
        try
        {
            ViewState["XmlExcel"] = null;
            BlOpeningStock blOpening = new BlOpeningStock();
          
            //// ViewState[ExcelData] = null;
            //gdvTrans.DataSource = null;
            //gdvTrans.DataBind();
            DataTable dt = new DataTable();
            dt = GetExcelData();
            //ViewState["GridValue"] = dt;
            if (dt.Rows.Count > 0)
            {
                ViewState["RedLine"] = null;
                ViewState["XmlExcel"] = dt;
                string Xmldata = ValidateExcel(dt);
               // blOpening.XmlExcelImport = Xmldata;
                blOpening.xmlOpening=Xmldata;
               
                DataTable dtDetails = blOpening.ValidateExcelData();
                ViewState["XmlExcel"] = dt;
                if (dtDetails.Rows.Count > 0)
                {


                    gvItems.DataSource = dtDetails;
                    gvItems.DataBind();
                    ViewState["TempTable"] = dtDetails;

                    hfSlNo.Value = "";

                    txtItemShortName.Focus();
                  //  CalculateTotals();


                    dt = null;
                    dtDetails = null;

                    btnShowData.Enabled = false;
                    hfExceldata.Value = "1";
                    ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "CalculateTotal();", true);
                }
                else
                {
                    //BindNullRecDetails();
                    //Btninsert.Visible = false;
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
    protected void btnShowData_Click(object sender, EventArgs e)
    {
        
        
        FillGrid();
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
                    // string path = AppDomain.CurrentDomain.BaseDirectory + @"Excel";

                    string path = Server.MapPath("~") + "\\Excel/";

                    if (!Directory.Exists(path))
                    {
                        Directory.CreateDirectory(path);
                    }

                    FileUploadExcel.SaveAs(path + "/" + FileUploadExcel.FileName);
                    Session[Excel] = path + "/" + FileUploadExcel.FileName;
                    //  WriteToLog(Session[Excel].ToString());
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
    protected void gvItems_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if ((DataBinder.Eval(e.Row.DataItem, "Item").ToString()) == "")//check zero
            {
                e.Row.BackColor = System.Drawing.Color.IndianRed;
                ViewState["RedLine"] = "true";
                Label lb = new Label();

            }

            if (Convert.ToString(DataBinder.Eval(e.Row.DataItem, "BatchNo").ToString()) == "")//check zero
            {
                e.Row.BackColor = System.Drawing.Color.IndianRed;
                ViewState["RedLine"] = "true";
            }
           
            if (Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "MRP").ToString()) == 0)//check zero
            {
                e.Row.BackColor = System.Drawing.Color.IndianRed;
                ViewState["RedLine"] = "true";
            }
            if (Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "Salprice").ToString()) == 0)//check zero
            {
                e.Row.BackColor = System.Drawing.Color.IndianRed;
                ViewState["RedLine"] = "true";
            }
            //if (Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "OpeningStock").ToString()) == 0)//check zero
            //{
            //    e.Row.BackColor = System.Drawing.Color.IndianRed;
            //    ViewState["RedLine"] = "true";
            //}
          
                
        }
    }
}