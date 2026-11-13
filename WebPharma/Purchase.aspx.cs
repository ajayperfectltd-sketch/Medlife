/*----------------------------------------------------------------------------------------------------------------------------------------------------------
Purpose    : Insert/Update/Delete Purchase 
Author     : BIBIN TC
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


public partial class Purchase : System.Web.UI.Page
{
    /*Veiw State*/
    private const string Materials = "Materials";
    private const string Excel = "Excel";

    #region  Function
    /*Import Purchase Starts*/
    protected DataSet DatasetFromExcel(string filePath)
    {

        string ext = Path.GetExtension(filePath);
        DataSet dsExcel = new DataSet();

        FileStream stream = File.Open(filePath, FileMode.Open, FileAccess.ReadWrite);

        if (ext == ".xls")//Reading from Excel file ('97-2003 format; *.xls)
        {
            IExcelDataReader excelReader = ExcelReaderFactory.CreateBinaryReader(stream);
            excelReader.IsFirstRowAsColumnNames = true;// DataSet - Create column names from first row
            try
            {
                dsExcel = excelReader.AsDataSet();
            }
            catch
            {
                ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "toastr.warning('Please save the document and Re-Import')", true);

            }
            dsExcel.Tables[0].TableName = "Purchase";
            excelReader.Close();
        }
        else if (ext == ".xlsx")// Reading from Excel file (2007 format; *.xlsx)
        {
            IExcelDataReader excelReader = ExcelReaderFactory.CreateOpenXmlReader(stream);
            excelReader.IsFirstRowAsColumnNames = true;// DataSet - Create column names from first row
            dsExcel = excelReader.AsDataSet();
            dsExcel.Tables[0].TableName = "Purchase";
            excelReader.Close();
        }
        if (File.Exists(filePath))
        {
            DirectoryInfo info = new DirectoryInfo(Server.MapPath("Excel"));
            foreach (FileInfo files in info.GetFiles())
            {
                try
                {
                    files.Delete();
                }
                catch (Exception ex)
                {

                }
            }
        }
        return dsExcel;
    }
    private void FillMaterialDropDown()
    {
        Populate populate = new Populate();
        if (ddlPurchaseOrder.SelectedIndex > 0)
        {
            string[] str = ddlPurchaseOrder.SelectedValue.Split(',');
            string[] Client = hfClient.Value.Split(',');
            ddlMaterialReceipt.Items.Clear();

            // populate.FillDropdownList(ddlMaterialReceipt, "MaterialReciept", "MRNo", "ID_MaterialReciept", "ID_MaterialReciept", " FK_Client = " + Client[0] + " and FK_PurchaseOrder = " + str[0], "");
            if (hfPurchse.Value == "")
            {
                //populate.FillDropdownList(ddlMaterialReceipt, "MaterialReciept", "MRNo", "ID_MaterialReciept", "ID_MaterialReciept", " FK_Client = " + Client[0] + " and FK_PurchaseOrder = " + str[0] + " and ID_MaterialReciept Not in( Select FK_MaterialReciept From PurchaseDetails )", "");
                populate.FillDropdownList(ddlMaterialReceipt, "MaterialReciept", "MRNo", "ID_MaterialReciept", "ID_MaterialReciept", " FK_Client = " + Client[0] + " And Cancelled='False' and FK_PurchaseOrder = " + str[0] + "  and   ID_MaterialReciept Not IN  ( SELECT        PD.FK_MaterialReciept FROM            PurchaseDetails AS PD INNER JOIN  Purchase AS P ON PD.FK_Purchase = P.ID_Purchase  where P.Cancelled='False'  )", "");
            }
            else
            {
                populate.FillDropdownList(ddlMaterialReceipt, "MaterialReciept", "MRNo", "ID_MaterialReciept", "ID_MaterialReciept", " FK_Client = " + Client[0] + " and FK_PurchaseOrder = " + str[0] + " and ID_MaterialReciept Not in( Select FK_MaterialReciept From PurchaseDetails where FK_Purchase <> " + hfPurchse.Value + "  )", "");
            }
            populate = null;

            ddlSupplier.Enabled = false;
            ddlStore.Enabled = false;

            ddlMaterialReceipt.Focus();
        }
        else
        {
            ddlSupplier.Enabled = true;
            ddlStore.Enabled = true;
        }
    }
    protected void BindData()
    {
        Populate populate = new Populate();

        populate.FillDropdownList(ddlSupplier, "Supplier", "Name", "ID_Supplier", "ID_Supplier", "  Active='True'  and FK_Client = " + hfClient.Value.Split(',')[0], "");
        populate.FillDropdownList(ddlStore, "Store", "StoName", "ID_Store", "ID_Store", "     Active='True'   and FK_Client = " + hfClient.Value.Split(',')[0], "");
        // populate.FillDropdownListAll(ddlPurchaseOrder, "PurchaseOrder", "PONo", "ID_PurchaseOrder", "ID_PurchaseOrder", "    Cancelled='False' AND Closed='False'    and FK_Client = " + hfClient.Value, "", "0", "NA");
        populate.FillDropdownListAdditionFeild(ddlPurchaseOrder, "PurchaseOrder", "PONo", "ID_PurchaseOrder", "ID_PurchaseOrder", "Cancelled='False' AND Closed='False' and ID_PurchaseOrder Not In (select FK_PurchaseOrder from Purchase  where Cancelled='false' )    and FK_Client = " + hfClient.Value.Split(',')[0], "", "0", "NA");
        // populate.FillDropdownList(ddlItems, "Items", "ShortName+' - '+Name", "ID_Items", "ID_Items", "   Active='True'   and FK_Client = " + hfClient.Value, "");
        populate.FillDropdownList(ddlSearchSupplier, "Supplier", "Name", "ID_Supplier", "ID_Supplier", "  Active='True'   and FK_Client = " + hfClient.Value.Split(',')[0], "");
        populate.FillDropdownList(ddlSearchStore, "Store", "StoName", "ID_Store", "ID_Store", "   Active='True'   and FK_Client = " + hfClient.Value.Split(',')[0], "");
        populate.FillDropdownList(ddlItem, "Items", "Name", "ID_Items", "ID_Items", "  Active='True' AND ItemType=2 AND FK_Client= " + hfClient.Value.Split(',')[0], "");
        //  populate.FillDropdownList(ddlMaterialReceipt, "MaterialReciept", "MrNo", "ID_MaterialReciept", "ID_MaterialReciept", "FK_Client = -1", "");
        ddlMaterialReceipt.Items.Insert(0, new ListItem("select", "0"));
        populate = null;
        txtDate.Text = DateTime.Now.ToShortDateString();
        // txtExpiryDate.Text = DateTime.Now.ToShortDateString();
    }

    private void FillterPurchaseorder()
    {
        Populate populate = new Populate();
        if (ddlSupplier.SelectedIndex > 0 && ddlPurchaseOrder.Enabled)
        {
            populate.FillDropdownList(ddlPurchaseOrder, "PurchaseOrder", "PONo", "ID_PurchaseOrder", "ID_PurchaseOrder", "  Cancelled='False' AND Closed='False' and ID_PurchaseOrder Not In (select FK_PurchaseOrder from Purchase  where Cancelled='false' )  AND FK_Supplier = " + ddlSupplier.SelectedValue.Split(',')[0] + "  AND FK_Client=" + hfClient.Value.Split(',')[0], "");
            ddlPurchaseOrder.Focus();
        }
        else
        {
            populate.FillDropdownListAdditionFeild(ddlPurchaseOrder, "PurchaseOrder", "PONo", "ID_PurchaseOrder", "ID_PurchaseOrder", "Cancelled='False' AND Closed='False' and ID_PurchaseOrder Not In (select FK_PurchaseOrder from Purchase  where Cancelled='false' )    and FK_Client = " + hfClient.Value.Split(',')[0], "", "0", "NA");
            ddlPurchaseOrder.Focus();
        }
        if (rdItemPurchase.Checked)
        {
            ddlPurchaseOrder.SelectedValue = Convert.ToString(0);
            ddlStore.Focus();
        }



        populate = null;
        divAdd.Style.Add("display", "block");
        divView.Style.Add("display", "none");
    }

    public void SetReadonly()
    {

        txtDate.Attributes.Add("readonly", "readonly");
        txtFromDate.Attributes.Add("readonly", "readonly");
        txtTodate.Attributes.Add("readonly", "readonly");
        txtSalesPrice.Attributes.Add("readonly", "readonly");
        txtWholeSale.Attributes.Add("readonly", "readonly");
        txtDate.Text = txtFromDate.Text = txtTodate.Text = DateTime.Now.ToString("dd/MM/yyyy");
        // txtExpiryDate.Attributes.Add("readonly", "readonly");
    }
    protected void CheckUserAccess()
    {
        BlUserAccess bluseraccess = new BlUserAccess();
        Populate populate = new Populate();

        bluseraccess.FK_Users = Convert.ToInt64(hfUser.Value);
        bluseraccess.FK_Pages = 41;
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
    protected void BindMaterialReceipt(long id)
    {
        if (id != 0)
        {
            BlPurchase blpurchase = new BlPurchase();
            blpurchase.FK_MaterialReciept = id;
            DataTable dtbl = blpurchase.proMaterialItemsUnderMaterials();
            gvMaterialItems.DataSource = dtbl;
            gvMaterialItems.DataBind();
            //  ViewState[TempTable] = dtbl;
            blpurchase = null;
            LoadStoreFromPurchaseOrder();
        }
        else
        {
            //ViewState[TempTable] = null;
            if (hfPurchse.Value == "")
            {
                ddlStore.SelectedIndex = 0;
                ddlSupplier.SelectedIndex = 0;
            }
            gvMaterialItems.DataSource = null;
            gvMaterialItems.DataBind();
        }

        txtRefNo.Focus();



    }

    protected void GridFill(int currentPage)
    {
        BlPurchase blpurchase = new BlPurchase();
        DataTable dtbl = new DataTable();


        // long MasterId = Convert.ToInt64(gvPurchase.DataKeys[e.NewSelectedIndex]["ID_Purchase"].ToString());
        blpurchase.MasterId = 0;
        blpurchase.FK_Supplier = Convert.ToInt64(ddlSearchSupplier.SelectedValue);
        blpurchase.FK_Store = Convert.ToInt64(ddlSearchStore.SelectedValue);
        blpurchase.FK_Client = Convert.ToInt64(hfClient.Value.Split(',')[0]);
        blpurchase.FromDate = DateTime.ParseExact(txtFromDate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
        blpurchase.ToDate = DateTime.ParseExact(txtTodate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
        currentPage = currentPage == 0 ? 1 : currentPage;
        blpurchase.PageIndex = Convert.ToInt16(currentPage);
        dtbl = blpurchase.SelectAllData();
        gvPurchase.DataSource = dtbl;
        gvPurchase.DataBind();
        hdfSearch.Value = "&FK_Supplier=" + ddlSearchSupplier.SelectedValue + "&FK_Store=" + ddlSearchStore.SelectedValue + "&FK_Client=" + hfClient.Value.Split(',')[0] + "&FromDate=" + txtFromDate.Text + "&ToDate=" + txtTodate.Text;
        if (dtbl.Rows.Count > 0)
        {
            int recordCount = Convert.ToInt32(dtbl.Rows[0]["RecordCount"].ToString());
            this.PopulatePager(recordCount, currentPage);

        }
        blpurchase = null;

    }



    protected void AddMaterials()
    {
        DataTable dtbl = new DataTable();
        int rowCount = 0;
        int inCheck = 0;
        bool st = false;
        if (ViewState[Materials] != null)
        {
            dtbl = (DataTable)ViewState[Materials];
            rowCount = dtbl.Rows.Count;

        }
        else
        {

            dtbl.Columns.Add("SLNO");
            dtbl.Columns.Add("MrNo");
            dtbl.Columns.Add("ID_MaterialReciept");
            dtbl.Columns.Add("PurchaseRate", typeof(decimal));
            dtbl.Columns.Add("FK_PurchaseOrder", typeof(Int64));


        }

        if (dtbl.Rows.Count > 0)
        {
            for (int i = 0; i < dtbl.Rows.Count; i++)
            {
                if (dtbl.Rows[i]["ID_MaterialReciept"].ToString() == ddlMaterialReceipt.SelectedValue)
                {
                    inCheck = inCheck + 1;
                    break;
                }
                else if (dtbl.Rows[i]["FK_PurchaseOrder"].ToString() != ddlPurchaseOrder.SelectedValue.ToString())
                {
                    st = true;
                    break;
                }
            }

        }
        if (st)
        {
            ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "alertify.alert('Cannot add  material receipt of different purchase order')", true);
            return;
        }
        else if (inCheck == 0)
        {
            DataRow drow = dtbl.NewRow();
            drow["SLNO"] = rowCount + 1;
            drow["MrNo"] = ddlMaterialReceipt.SelectedItem.Text;
            drow["ID_MaterialReciept"] = ddlMaterialReceipt.SelectedValue;
            drow["PurchaseRate"] = 0;
            drow["FK_PurchaseOrder"] = Convert.ToDecimal(ddlPurchaseOrder.SelectedValue);
            if (gvMaterialItems.Rows.Count > 0)
            {
                //  DataTable dtblDt = (DataTable)ViewState[TempTable];
                decimal PR = 0;
                foreach (GridViewRow gr in gvMaterialItems.Rows)
                {
                    if (gr.RowType == DataControlRowType.DataRow)
                    {
                        string str = gr.Cells[5].Text;
                        PR = PR + Convert.ToDecimal(str);
                    }
                }
                drow["PurchaseRate"] = PR;
            }
            dtbl.Rows.Add(drow);

            ViewState[Materials] = dtbl;

            gvMatrerialReceipt.DataSource = dtbl;
            gvMatrerialReceipt.DataBind();
            CalculateInvoiceAmount(dtbl, "PurchaseRate", txtInvoiceAmount);
            ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "MaterialClear();", true);
        }
        else
        {
            ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "alertify.alert('Material already exists')", true);
        }

    }


    protected void Save()
    {

        DateTime purchaseDate;

        if (!DateTime.TryParseExact(
                txtDate.Text.Trim(), "dd/MM/yyyy",
                CultureInfo.InvariantCulture,
                DateTimeStyles.None,
                out purchaseDate))
        {
            Populate.ErrorMessageBox(this, "Invalid Purchase Date. Use dd/MM/yyyy.");
            return;
        }

        DateTime today = DateTime.Today;

        // Rule 1: No future date
        if (purchaseDate > today)
        {
            Populate.ErrorMessageBox(this, "Purchase date cannot be a future date.");
            return;
        }

        // Rule 2: Financial year validation
        int finYear = today.Month >= 4 ? today.Year : today.Year - 1;
        DateTime finYearStart = new DateTime(finYear, 4, 1);

        if (purchaseDate.Date < finYearStart.Date)
        {
            Populate.ErrorMessageBox(this,
                "Purchase date cannot be from a previous financial year. " +
                "Earliest allowed date is " + finYearStart.ToString("dd/MM/yyyy") + ".");
            return;
        }

        bool status = true;
        long result = 0;
        BlPurchase blpurchase = new BlPurchase();
        // blpurchase.PurBillNo = txtBillNo.Text;
        blpurchase.RefNo = txtRefNo.Text;
        blpurchase.Date = DateTime.ParseExact(txtDate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
        blpurchase.FK_Supplier = Convert.ToInt64(ddlSupplier.SelectedValue);
        blpurchase.FK_Store = Convert.ToInt64(ddlStore.SelectedValue);
        blpurchase.Description = txtRemark.Text;
        blpurchase.Credit = cbxIsCredit.Checked;
        blpurchase.UserCode = hfUser.Value;
        blpurchase.EnteredOn = DateTime.Now;
        blpurchase.Cancelled = false;
        blpurchase.Cancelleduser = "";
        blpurchase.FK_Client = Convert.ToInt64(hfClient.Value.Split(',')[0]);
        //if(rdItemPurchase.Checked==true)
        //{
        //    blpurchase.ItemPurchase = true;
        //}
        //else if (rdMaterialPurchase.Checked == true)
        //{
        //    blpurchase.ItemPurchase = false;
        //}
        blpurchase.ItemPurchase = Convert.ToBoolean(rdItemPurchase.Checked);

        blpurchase.Cost = 0; //Convert.ToDecimal(txtCost.Text == "" ? "0" : txtCost.Text);

        blpurchase.Expense = 0; //Convert.ToDecimal(txtExpense.Text == "" ? "0" : txtExpense.Text);
        blpurchase.InvoiceAmount = Convert.ToDecimal(txtInvoiceAmount.Text == "" ? "0" : txtInvoiceAmount.Text);


        blpurchase.FK_PurchaseOrder = Convert.ToInt64(ddlPurchaseOrder.SelectedValue);

        if (blpurchase.ItemPurchase == true)
        {
            //Purchase Item to xml
            ArrayList arlist1 = new ArrayList();
            DataTable dtbl1 = (DataTable)ViewState["ItemWise"];

            IPurchaseItemDetails IPurchaseItemDetails;
            for (int i = 0; i < dtbl1.Rows.Count; i++)
            {
                IPurchaseItemDetails = new IPurchaseItemDetails();
                // IPurchaseItemDetails.FK_MaterialReciept = 0;
                IPurchaseItemDetails.FK_Items = Convert.ToInt64(dtbl1.Rows[i]["FK_Items"].ToString());
                IPurchaseItemDetails.BatchNo = Convert.ToString(dtbl1.Rows[i]["BatchNo"].ToString());
                IPurchaseItemDetails.ExpiryDate = Convert.ToDateTime(dtbl1.Rows[i]["ExpiryDate"].ToString());
                IPurchaseItemDetails.Quantity = Convert.ToDecimal(dtbl1.Rows[i]["Quantity"].ToString());
                IPurchaseItemDetails.PurchaseRate = Convert.ToDecimal(dtbl1.Rows[i]["PurchaseRate"].ToString());
                
                IPurchaseItemDetails.ActualCost = Convert.ToDecimal(dtbl1.Rows[i]["ActualCost"].ToString());
                IPurchaseItemDetails.SalesPrice = Convert.ToDecimal(dtbl1.Rows[i]["SalesPrice"].ToString());
                IPurchaseItemDetails.OfferPrice = Convert.ToDecimal(dtbl1.Rows[i]["OfferPrice"].ToString());
                IPurchaseItemDetails.WholeSalePrice = Convert.ToDecimal(dtbl1.Rows[i]["WholeSalePrice"].ToString());
                IPurchaseItemDetails.FK_Unit = Convert.ToInt64(dtbl1.Rows[i]["FK_Unit"].ToString());
                arlist1.Add(IPurchaseItemDetails);
                IPurchaseItemDetails = null;
            }

            blpurchase.XmlPurchaseItems = Common.WriteToXml(arlist1);
            status = true;
            //End Purchase item to xml
        }
        else if (blpurchase.ItemPurchase == false)
        {
            ArrayList arlist = new ArrayList();

            DataTable dtbl = (DataTable)ViewState[Materials];
            BlPurchaseDetails blpurchaseDetails;


            foreach (DataRow dr in dtbl.Rows)
            {
                blpurchaseDetails = new BlPurchaseDetails();
                blpurchaseDetails.FK_MaterialReciept = Convert.ToInt64(dr["ID_MaterialReciept"]);
                //  blpurchaseDetails.
                if (Convert.ToInt64(dr["FK_PurchaseOrder"]) != blpurchase.FK_PurchaseOrder)
                {
                    status = false;
                }
                arlist.Add(blpurchaseDetails);
                blpurchaseDetails = null;
            }
            if (status)
            {
                blpurchase.BlPurchaseDetails = Common.WriteToXml(arlist);
            }

        }
        if (status)
        {


            if (hfPurchse.Value == "")
            {
                if (hfIsAdd.Value.ToLower() == "true")
                {
                    result = blpurchase.InsertData();
                    if (result > 0)
                    {
                        ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "ShowAddDiv();", true);

                        ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "ClearAdd();", true);
                        Populate.InsertMsg(this);
                        ClearAll();
                    }
                    else if (result == -6)
                    {
                        Populate.ErrorMessageBox(this, "Cannot insert, Pending material receipt exists under the purchase order");
                    }
                    else if (result == -9)
                    {
                        ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "ShowAddDiv();", true);

                        ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "toastr.error('Date of Purchase is lesser than date of Material reciept')", true);
                        txtDate.Focus();
                    }
                    else
                    {
                        Populate.ErrorMessageBox(this, "Cannot insert,..");
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

                    blpurchase.MasterId = Convert.ToInt64(hfPurchse.Value);
                    blpurchase.FK_Client = Convert.ToInt64(hfClient.Value);
                    result = blpurchase.UpdateData();
                    if (result > 0)
                    {
                        Populate.UpdateMsg(this);
                        ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "ShowAddDiv();", true);

                        ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "ClearAdd();", true);

                        ClearAll();
                    }
                    else if (result == -3)
                    {
                        Populate.ErrorMessageBox(this, "Cannot Update.. Low Stock");
                    }
                    else if (result == -6)
                    {
                        Populate.ErrorMessageBox(this, "Cannot insert, Pending material receipt exist under the purchase order");
                    }
                    else if (result == -9)
                    {
                        ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "ShowAddDiv();", true);
                        ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "toastr.error('Date of Purchase is lesser than date of Material reciept')", true);
                        txtDate.Focus();
                    }
                    else
                    {
                        Populate.ErrorMessageBox(this, "Cannot Update.. ");
                    }
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "toastr.warning('User does not have permission to Update')", true);

                }
                ClearAll();
            }



        }
        else
        {
            Populate.ErrorMessageBox(this, "Cannot " + (hfPurchse.Value == "" ? " insert " : " update ") + ", material receipts and purchase order mismatched");
        }
    }

    protected void ClearDirectPurchaseitems()
    {
        ViewState["ItemWise"] = null;
        gvItems.DataSource = null;
        gvItems.DataBind();
        rdMaterialPurchase.Visible = true;
        rdItemPurchase.Visible = true;
        rdMaterialPurchase.Checked = true;
        rdItemPurchase.Checked = false;

        DisplayDiv();



    }



    protected void ClearItems()
    {
        //ViewState[TempTable] = null;
        //  ddlPurchaseOrder.SelectedIndex = 0;


        ddlMaterialReceipt.SelectedIndex = 0;
        gvMaterialItems.DataSource = null;
        gvMaterialItems.DataBind();
    }
    private void ClearAll()
    {
        Populate populate = new Populate();
        populate.FillDropdownListAdditionFeild(ddlPurchaseOrder, "PurchaseOrder", "PONo", "ID_PurchaseOrder", "ID_PurchaseOrder", "Cancelled='False' AND Closed='False' and ID_PurchaseOrder Not In (select FK_PurchaseOrder from Purchase  where Cancelled='false')    and FK_Client = " + hfClient.Value.Split(',')[0], "", "0", "NA");
        ddlSupplier.Enabled = true;
        ddlStore.Enabled = true;
        populate = null;
        // ddlPurchaseOrder.Enabled = true;
        //ddlMaterialReceipt.Enabled = true;
        ViewState[Materials] = null;



        ddlMaterialReceipt.Items.Clear();
        ddlMaterialReceipt.Items.Insert(0, new ListItem("select", "0"));
        ClearItems();

        gvMatrerialReceipt.DataSource = null;
        gvMatrerialReceipt.DataBind();
        ////divAdd.Style.Add("display", "block");
        ////divView.Style.Add("display", "none;");
        ClearDirectPurchaseitems();
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
        rptPager.DataSource = pages;
        rptPager.DataBind();
    }

    protected void LoadStoreFromPurchaseOrder()
    {
      //  BlMaterialReciept blMaterialReciept = new BlMaterialReciept();
      //  blMaterialReciept.MasterId = Convert.ToInt64(ddlMaterialReceipt.SelectedValue);
      //  blMaterialReciept.FK_Client = Convert.ToInt64(hfClient.Value.Split(',')[0]);
      //  blMaterialReciept.SelectMaterialStoreSupplier();
        //try
        //{
        //    ddlStore.SelectedValue = blMaterialReciept.FK_Store.ToString();
        //}
        //catch (Exception ex)
        //{
        //    ddlStore.SelectedValue = "0";
        //}
        //try
        //{
        //    ddlSupplier.SelectedValue = blMaterialReciept.FK_Supplier.ToString();
        //}
        //catch (Exception ex)
        //{
        //    ddlSupplier.SelectedValue = "0";
        //}
        //blMaterialReciept = null;

    }
    //START ItemPurchase Function
    protected void DisplayDiv()
    {
        if (rdItemPurchase.Checked == true)
        {

            divItemPurchase.Style.Add("display", "block;");
            divMaterialPurchase.Style.Add("display", "none;");
            ddlStore.Enabled = true;
            ddlSupplier.Enabled = true;
            ddlMaterialReceipt.Enabled = false;
            ddlPurchaseOrder.Enabled = false;
            ddlPurchaseOrder.SelectedValue = Convert.ToString(0);
            txtRefNo.Focus();

        }
        else if (rdItemPurchase.Checked == false)
        {
            divMaterialPurchase.Style.Add("display", "block;");
            divItemPurchase.Style.Add("display", "none;");
            ddlStore.Enabled = true;//false    
            ddlSupplier.Enabled = true;//false
            ddlMaterialReceipt.Enabled = true;
            ddlPurchaseOrder.Enabled = true;
            txtRefNo.Focus();
        }

    }
    protected void AddDataItems()
    {
        DataTable dtbl = new DataTable();
        int rowCount = 0;
        int inCheck = 0;
        if (ViewState["ItemWise"] != null)
        {
            dtbl = (DataTable)ViewState["ItemWise"];
            rowCount = dtbl.Rows.Count;
        }
        else
        {
            dtbl.Columns.Add("SLNO");
            dtbl.Columns.Add("Item");
            dtbl.Columns.Add("FK_Items");
            dtbl.Columns.Add("BatchNo");
            dtbl.Columns.Add("ExpiryDate");
            dtbl.Columns.Add("Quantity");
            dtbl.Columns.Add("PurchaseRate");
            dtbl.Columns.Add("ActualCost");
            dtbl.Columns.Add("SalesPrice");
            dtbl.Columns.Add("OfferPrice");
            dtbl.Columns.Add("WholeSalePrice");
            dtbl.Columns.Add("TotalAmount");
            dtbl.Columns.Add("FK_Unit");
            dtbl.Columns.Add("Units");

        }
        if (dtbl.Rows.Count > 0)
        {

            for (int i = 0; i < dtbl.Rows.Count; i++)
            {
                if (dtbl.Rows[i]["FK_Items"].ToString() == ddlItem.SelectedValue.ToString())
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

            drow["BatchNo"] = Convert.ToString(txtBatch.Text);
            drow["ExpiryDate"] = DateTime.ParseExact(txtExpiryDate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            drow["Quantity"] = txtQuantity.Text == "" ? "0" : txtQuantity.Text;
            drow["PurchaseRate"] = txtPurchaseRate.Text == "" ? "0" : txtPurchaseRate.Text;
            drow["ActualCost"] = txtActualCost.Text == "" ? "0" : txtActualCost.Text;

            drow["SalesPrice"] = txtSalesPrice.Text == "" ? "0" : txtSalesPrice.Text;
            drow["OfferPrice"] = txtOfferPrice.Text == "" ? "0" : txtOfferPrice.Text;
            drow["WholeSalePrice"] = txtWholeSale.Text == "" ? "0" : txtWholeSale.Text;
            drow["TotalAmount"] = Math.Round(Convert.ToDecimal(txtPurchaseRate.Text == "" ? "0" : txtPurchaseRate.Text) * Convert.ToDecimal(txtQuantity.Text == "" ? "0" : txtQuantity.Text), 3);
            drow["FK_Unit"] = ddlUnit.SelectedValue.ToString();
            drow["Units"] = ddlUnit.SelectedItem.ToString();
            dtbl.Rows.InsertAt(drow, rowCount);
            gvItems.DataSource = dtbl;
            gvItems.DataBind();
            ViewState["ItemWise"] = dtbl;
            CalculateInvoiceAmount(dtbl, "TotalAmount", txtInvoiceAmount);


            //ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "ItemClear();", true);
        }
        else
        {
            ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "alertify.alert('Item already exists')", true);
        }
    }
    protected void CalculateInvoiceAmount(DataTable dt, String ColumnField, TextBox txt)
    {
        Decimal invoAmnt = 0;
        if (dt.Rows.Count > 0)
        {

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                invoAmnt = invoAmnt + Convert.ToDecimal(dt.Rows[i]["" + ColumnField + ""].ToString());

            }
        }
        txt.Text = Convert.ToString(invoAmnt.ToString("0.000"));
    }
    protected void ddlItemsFill()
    {
        if (ddlItem.SelectedIndex > 0)
        {
            BlItems blitems = new BlItems();
            blitems.MasterId = Convert.ToInt64(ddlItem.SelectedValue.ToString());
            blitems.SelectData();
            //txtSalesPrice.Text = blitems.MinimumSalesPrice.ToString();
            //txtWholeSale.Text = blitems.SellingPrice2.ToString();
            txtItemShortName.Text = blitems.Name.ToString();
            Populate populate = new Populate();
            populate.FillDropdownList(ddlUnit, "Units", "Name", "ID_Units", "ID_Units", "FK_Client = " + Convert.ToInt64(hfClient.Value.Split(',')[0]) + "  AND FK_CategoryFirst=" + blitems.FK_CategoryFirst.ToString(), "");
            populate = null;
            ddlUnit.SelectedValue = blitems.FK_Units.ToString();
        }
    }
    protected void GetModSettings()
    {
        string ModSettings;
        string[] settings;
        BlSettings blsettings = new BlSettings();
        blsettings.FK_Client = Convert.ToInt64(hfClient.Value);
        ModSettings = blsettings.GetModuleSettings();
        settings = ModSettings.Split(',');
        if (settings[0].ToString().ToLower() == "false" || settings[1].ToString().ToLower() == "false")
        {
            if (settings[0].ToString().ToLower() == "false")
            {
                liPO.Attributes.Add("style", "display:none");
                ddlPurchaseOrder.SelectedIndex = 1;
                ddlPurchaseOrder_SelectedIndexChanged1(this, EventArgs.Empty);
            }
            if (settings[1].ToString().ToLower() == "false")
            {
                liMR.Attributes.Add("style", "display:none");
                rdMaterialPurchase.Visible = false;
            }                               
            rdItemPurchase.Checked = true;
            rdItemPurchase_CheckedChanged(this, EventArgs.Empty);
        }
        else
        {
            liPO.Attributes.Remove("style");
            liMR.Attributes.Remove("style");
            rdMaterialPurchase.Visible = true;
        }
    }
    //END ItemPurchase Function
    #endregion
    #region Events
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            try
            {

                hfUser.Value = Request.QueryString["uid"];
                hfClient.Value = Request.QueryString["clnt"];
                //  hfUser.Value = "1";
                // hfClient.Value = "1";
                ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "ClearAdd();", true);
                divAdd.Style.Add("display", "block;");
                divView.Style.Add("display", "none;");
                CheckUserAccess();
                SetReadonly();
                BindData();
                txtInvoiceAmount.Attributes.Add("readonly", "readonly");
                //txtExpiryDate.Attributes.Add("readonly", "readonly");

                ddlPurchaseOrder.Enabled = true;
                ddlMaterialReceipt.Enabled = true;
                ddlStore.Enabled = true;
                ddlSupplier.Enabled = true;
                //rdMaterialPurchase.Checked = true;
                rdItemPurchase.Checked = true;
                DisplayDiv();
                //ScriptManager.RegisterStartupScript(this, this.GetType(), "Script", "DisableTextBox();", true);
                imgbtnExport.Enabled = false;
                GridFill(1);
                GetModSettings();
                rdItemPurchase.Focus();
            }
            catch (Exception ex)
            {
                // blMessage.Items.Add(ex.ToString());
                Populate.WarningMsg(this, ex.ToString());
            }
        }
    }

    protected void btnHidden_Click(object sender, EventArgs e)
    {
        try
        {
            if (ddlMaterialReceipt.SelectedValue != "" && ddlMaterialReceipt.SelectedValue != null)
            {
                BindMaterialReceipt(Convert.ToInt64(ddlMaterialReceipt.SelectedValue));
            }
        }
        catch (Exception ex)
        {
            Populate.WarningMsg(this, ex.ToString());
        }



    }

    protected void imgbtnView_Click(object sender, ImageClickEventArgs e)
    {

       // blMessage.Items.Clear();
        try
        {
           // blMessage.Items.Clear();
         
            ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "ClearAdd();", true);
           
            GridFill(1);
            imgbtnExport.Enabled = true;

            divAdd.Style.Add("display", "none");
            divView.Style.Add("display", "block");
            divImport.Style.Add("display", "none");
        }
        catch (Exception ex)
        {
            Populate.WarningMsg(this, ex.ToString());
        }
    }
    protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
    {
        // ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "ShowAddDiv();", true);
        ClearAll();
        divView.Style.Add("display", "none");
        divAdd.Style.Add("display", "block");
        divImport.Style.Add("display", "none");
        imgbtnExport.Enabled = false;
       // ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Script", "DisableTextBox();", true);
    }
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        try
        {
            if (gvMaterialItems.Rows.Count > 0)
            {
                AddMaterials();
                ClearItems();
                txtRemark.Focus();
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
    protected void btnClearAdd_Click(object sender, EventArgs e)
    {

    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            if (gvMatrerialReceipt.Rows.Count > 0 || gvItems.Rows.Count > 0)
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
    protected void btnClear_Click(object sender, EventArgs e)
    {


    }
    protected void Page_Changed(object sender, EventArgs e)
    {
        //blMessage.Items.Clear();
        int pageIndex = int.Parse((sender as LinkButton).CommandArgument);

        DataTable dtbl = new DataTable();
        BlPurchase blPromotionType = new BlPurchase();

        blPromotionType.PageSize = 10;
        blPromotionType.PageIndex = Convert.ToInt16(pageIndex);
        GridFill(blPromotionType.PageIndex);
        //if (dtbl.Rows.Count > 0)
        //{
        //    int recordCount = Convert.ToInt32(dtbl.Rows[0]["RecordCount"].ToString());
        //    this.PopulatePager(recordCount, pageIndex);
        //}
        blPromotionType = null;
    }
    protected void btnAddMaterials_Click(object sender, EventArgs e)
    {
        try
        {

            //AddData();
        }
        catch (Exception ex)
        {
            //blMessage.Items.Add(ex.ToString());
        }
    }
    protected void btnMaterialReceipt_Click(object sender, EventArgs e)
    {

    }
    protected void btnHidden2_Click(object sender, EventArgs e)
    {
        FillMaterialDropDown();

    }
    protected void btnItem_Click(object sender, EventArgs e)
    {
        ddlItem_SelectedIndexChanged(sender, e);
    }

    protected void gvPurchase_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        try
        {
            BlPurchase blpurchase = new BlPurchase();
            ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "ShowAddDiv();", true);


            long MasterId = Convert.ToInt64(gvPurchase.DataKeys[e.NewSelectedIndex]["ID_Purchase"].ToString());
            blpurchase.MasterId = MasterId;
            hfPurchse.Value = MasterId.ToString();



            // populate.FillDropdownListAdditionFeild(ddlPurchaseOrder, "PurchaseOrder", "PONo", "ID_PurchaseOrder", "ID_PurchaseOrder", "Cancelled='False'    and FK_Client = " + hfClient.Value, "", "0", "NA");


            // populate.FillDropdownList(ddlItems, "Items", "ShortName+' - '+Name", "ID_Items", "ID_Items", "   Active='True'   and FK_Client = " + hfClient.Value, "");



            blpurchase.FK_Supplier = 0; //Convert.ToInt64(ddlSearchSupplier.SelectedValue);
            blpurchase.FK_Store = 0; //Convert.ToInt64(ddlSearchStore.SelectedValue);
            blpurchase.FK_Client = Convert.ToInt64(hfClient.Value.Split(',')[0]);
            blpurchase.FromDate = DateTime.Parse("1/1/1900");  //DateTime.ParseExact(txtFromDate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            blpurchase.ToDate = DateTime.MaxValue;//DateTime.ParseExact(txtTodate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            blpurchase.SelectPurchase();
            rdItemPurchase.Checked = blpurchase.ItemPurchase;
            if (blpurchase.ItemPurchase == true)
            {
                rdMaterialPurchase.Visible = false;
                rdItemPurchase.Visible = true;
            }
            else if (blpurchase.ItemPurchase == false)
            {
                rdMaterialPurchase.Visible = true;
                rdItemPurchase.Visible = false;
            }

            txtDate.Text = blpurchase.Date.ToString("dd/MM/yyyy");
            txtRefNo.Text = blpurchase.RefNo;


            txtRemark.Text = blpurchase.Description;

            //ddlPurchaseOrder.Enabled = false;
            //ddlMaterialReceipt.Enabled = false;
            // ddlPurchaseOrder.SelectedValue = blpurchase.FK_PurchaseOrder.ToString();
            try
            {
                ddlSupplier.SelectedValue = blpurchase.FK_Supplier.ToString();
            }
            catch (Exception ex)
            {
                ddlSupplier.SelectedValue = "0";
            }
            Populate populate = new Populate();
            populate.FillDropdownListAdditionFeild(ddlPurchaseOrder, "PurchaseOrder", "PONo", "ID_PurchaseOrder", "ID_PurchaseOrder", "Cancelled='False' AND FK_Supplier = " + blpurchase.FK_Supplier.ToString() + "   and FK_Client = " + hfClient.Value, "", "0", "NA");
            populate = null;
            ddlPurchaseOrder.SelectedValue = blpurchase.FK_PurchaseOrder.ToString();
            FillMaterialDropDown();
            //ddlMaterialReceipt.SelectedValue = blpurchase.FK_MaterialReciept.ToString();
            try
            {
                ddlStore.SelectedValue = blpurchase.FK_Store.ToString();
            }
            catch(Exception ex)
            {
                ddlStore.SelectedValue = "0";
            }
            cbxIsCredit.Checked = blpurchase.Credit;
            txtInvoiceAmount.Text = blpurchase.InvoiceAmount.ToString();
            txtExpense.Text = blpurchase.Expense.ToString();
            txtCost.Text = blpurchase.Cost.ToString();

            DataTable dtblItems = blpurchase.proPurchaseMaterialsPurchaseItems();
            ViewState["ItemWise"] = dtblItems;
            gvItems.DataSource = dtblItems;
            gvItems.DataBind();




            DataTable dtbl = blpurchase.proPurchaseMaterials();
            gvMatrerialReceipt.DataSource = dtbl;
            gvMatrerialReceipt.DataBind();
            ViewState[Materials] = dtbl;
            divAdd.Style.Add("display", "block");
            divView.Style.Add("display", "none;");
            DisplayDiv();
            ddlSupplier.Enabled = false;
            ddlStore.Enabled = false;

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
            
            //ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "EnableExport();", true);
            imgbtnExport.Enabled = true;
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "toastr.warning('" + ex.Message + "')", true);

        }
    }
    protected void ddlMaterialReceipt_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void gvMatrerialReceipt_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {

           // blMessage.Items.Clear();
            try
            {
                string confirmValue = Request.Form["confirm_value"];
                if (confirmValue.Length > 3)
                    confirmValue = confirmValue.Substring(confirmValue.Length - 3, 3);
                if (confirmValue == "Yes")
                {
                    if (ViewState[Materials] != null)
                    {
                        DataTable dtbl = (DataTable)ViewState[Materials];
                        dtbl.Rows.RemoveAt(e.RowIndex);
                        gvMatrerialReceipt.DataSource = dtbl;
                        gvMatrerialReceipt.DataBind();
                        ViewState[Materials] = dtbl;
                        CalculateInvoiceAmount(dtbl, "PurchaseRate", txtInvoiceAmount);
                    }
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "toastr.warning('" + ex.Message + "')", true);
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "toastr.warning('" + ex.Message + "')", true);
        }
    }
    protected void ddlPurchaseOrder_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {

            Populate populate = new Populate();
            string[] str = ddlPurchaseOrder.SelectedValue.Split(',');
            string[] Client = hfClient.Value.Split(',');
            if (hfPurchse.Value == "")
            {
                // populate.FillDropdownList(ddlMaterialReceipt, "MaterialReciept", "MRNo", "ID_MaterialReciept", "ID_MaterialReciept", " FK_Client = " + Client[0] + " and FK_PurchaseOrder = " + str[0] + " and ID_MaterialReciept Not in(Select FK_MaterialReciept PurchaseDetails )", "");
                populate.FillDropdownList(ddlMaterialReceipt, "MaterialReciept", "MRNo", "ID_MaterialReciept", "ID_MaterialReciept", " FK_Client = " + Client[0] + " and FK_PurchaseOrder = " + str[0] + " and ID_MaterialReciept Not in(SELECT        PD.FK_MaterialReciept FROM            PurchaseDetails AS PD INNER JOIN  Purchase AS P ON PD.FK_Purchase = P.ID_Purchase  where P.Cancelled='False' )", "");



            }
            else
            {
                populate.FillDropdownList(ddlMaterialReceipt, "MaterialReciept", "MRNo", "ID_MaterialReciept", "ID_MaterialReciept", " FK_Client = " + Client[0] + " and FK_PurchaseOrder = " + str[0] + " and ID_MaterialReciept Not in(Select FK_MaterialReciept PurchaseDetails )  and FK_Purchase <> " + hfPurchse.Value, "");
            }
            populate = null;
            ddlMaterialReceipt.Focus();

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
           // blMessage.Items.Clear();
            string confirmValue = Request.Form["confirm_value"];
            if (confirmValue.Length > 3)
                confirmValue = confirmValue.Substring(confirmValue.Length - 3, 3);
            if (confirmValue == "Yes")
            {
                long status = 0;
                BlPurchase blpurchase = new BlPurchase();
                blpurchase.MasterId = Convert.ToInt64(gvPurchase.DataKeys[e.RowIndex]["ID_Purchase"].ToString());
                blpurchase.Cancelleduser = hfUser.Value;
                blpurchase.CancelledOn = DateTime.Now;
                blpurchase.FK_Client = Convert.ToInt64(hfClient.Value);
                if (hfIsDelete.Value.ToLower() == "true")
                {
                    status = blpurchase.DeletePurchase();
                    if (status > 0)
                    {
                        GridFill(1);

                        //blMessage.Items.Add("Deleted successfully");

                        Populate.DeleteMsg(this);
                    }
                    else
                    {
                        Populate.ReferanceMsg(this);

                        //blMessage.Items.Add("Cannot delete, reference exists");
                    }
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "toastr.warning('User does not have permission to Delete')", true);
                }
                blpurchase = null;
                //ID_Purchase
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "toastr.warning('" + ex.Message + "')", true);
        }

    }
    protected void btnClear_Click1(object sender, EventArgs e)
    {
        try
        {
            ClearAll();
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "toastr.warning('" + ex.Message + "')", true);
        }
    }


    protected void ddlPurchaseOrder_SelectedIndexChanged1(object sender, EventArgs e)
    {
        try
        {
            FillMaterialDropDown();

        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "toastr.warning('" + ex.Message + "')", true);
        }
    }
    protected void ddlMaterialReceipt_SelectedIndexChanged1(object sender, EventArgs e)
    {
        try
        {
            if (ddlMaterialReceipt.SelectedValue != "" && ddlMaterialReceipt.SelectedValue != null)
            {
                BindMaterialReceipt(Convert.ToInt64(ddlMaterialReceipt.SelectedValue));
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "toastr.warning('" + ex.Message + "')", true);
        }

    }


    //START Item Purchase Events
    protected void btnAddItems_Click(object sender, EventArgs e)
    {
        try
        {

            DateTime dtExpiry = DateTime.ParseExact(txtExpiryDate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            //DateTime dtNow = DateTime.ParseExact(DateTime.Now.ToString("dd/MM/yyyy"), "dd/MM/yyyy", CultureInfo.InvariantCulture);
            DateTime dtNow = Convert.ToDateTime(hfDate.Value);
            if (ddlItem.SelectedItem.ToString().Trim() == txtItemShortName.Text.ToString().Trim())
            {
                if (dtExpiry < dtNow)
                {
                    //ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "alert('Expirydate cannot be lesser than actual date')", true);
                    //ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "ConfirmDate();", true);

                    //string confirmValue = Request.Form["confirm_value"];
                    //if (confirmValue.Length > 3)
                    //    confirmValue = confirmValue.Substring(confirmValue.Length - 3, 3);
                    //if (confirmValue == "Yes")
                    //{
                    ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "alertify.alert('Expiry date is lesser than actual date')", true);
                    AddDataItems();
                    ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "ItemClear();", true);
                    //}
                    //else
                    //{
                    //    txtDate.Text = Convert.ToString(DateTime.Now);

                    //}



                }
                else
                {
                    AddDataItems();
                    ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "ItemClear();", true);
                }
            }
            else 
            {
                ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "toastr.warning('Item  does not exist')", true);
                        
            }
            txtItemShortName.Focus();


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

            if (ddlItem.SelectedIndex > 0)
            {
                ddlItemsFill();
            }
            ddlUnit.Focus();

        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "toastr.warning('" + ex.Message + "')", true);
        }
    }
    protected void gvItems_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        //blMessage.Items.Clear();
        try
        {
            string confirmValue = Request.Form["confirm_value"];
            if (confirmValue.Length > 3)
                confirmValue = confirmValue.Substring(confirmValue.Length - 3, 3);
            if (confirmValue == "Yes")
            {
                if (ViewState["ItemWise"] != null)
                {
                    DataTable dtbl = (DataTable)ViewState["ItemWise"];
                    dtbl.Rows.RemoveAt(e.RowIndex);
                    gvItems.DataSource = dtbl;
                    gvItems.DataBind();

                    ViewState["ItemWise"] = dtbl;
                    CalculateInvoiceAmount(dtbl, "TotalAmount", txtInvoiceAmount);
                }
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "toastr.warning('" + ex.Message + "')", true);
        }

    }
    protected void rdItemPurchase_CheckedChanged(object sender, EventArgs e)
    {
        try
        {
            DisplayDiv();
            //  ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "ShowPurchaseItems();", true);
            if (ViewState["ItemWise"] == null)
            {
                gvItems.DataSource = null;
                gvItems.DataBind();

            }
            //rdMaterialPurchase.Focus();
            //UpdatePanel1.Update();
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
            // ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "ShowPurchaseOrderItems();", true);
            DisplayDiv();
            if (ViewState["Materials"] == null)
            {
                gvMaterialItems.DataSource = null;
                gvMaterialItems.DataBind();
            }
            // UpdatePanel1.Update();
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "toastr.warning('" + ex.Message + "')", true);
        }
    }
    //END Item Purchase Events
    protected void btnHiddenSupplierFilter_Click(object sender, EventArgs e)
    {
        FillterPurchaseorder();
    }

    //Purchase Imports Starts

    protected void FileUploadExcel_UploadedComplete(object sender, AjaxControlToolkit.AsyncFileUploadEventArgs e)
    {
        try
        {
            if (FileUploadExcel.HasFile)
            {
                bool status;
                //DataTable dt = new DataTable();
                string filePath = FileUploadExcel.PostedFile.FileName.ToString();
                string path = Server.MapPath("Excel");
                FileUploadExcel.SaveAs(path + "/" + FileUploadExcel.FileName);
                Session[Excel] = path + "/" + FileUploadExcel.FileName;
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "toastr.warning('" + ex.Message + "')", true);
        }
    }
    protected void btnShowData_Click(object sender, EventArgs e)
    {
        try
        {

            ArrayList arlist = new ArrayList();
            IpurchaseExport ipurchase;
            string str="",strNegative = "";

            bool blex = false;
            bool isNegative = false;
        


            if (Session[Excel] != null)
            {
                string ExcelPath = Session[Excel].ToString();
                DataSet ds = DatasetFromExcel(ExcelPath);
                long status = 0;
              //  decimal decqty=0;
              //  decimal decAmt=0;
                decimal decSum=0;
                if (ds.Tables[0].Rows.Count > 0)
                {

                    DataRow dm = ds.Tables[0].Rows[0];

                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        DataRow dr = ds.Tables[0].Rows[i];

                        ipurchase = new IpurchaseExport();

                        if (dm["RefNo"].ToString() != "&nbsp;" && dm["RefNo"].ToString() != "" && dm["Supplier"].ToString() != "&nbsp;" && dm["Supplier"].ToString() != "" && dm["Store"].ToString() != "&nbsp;" && dm["Store"].ToString() != "" && dr["Item"].ToString() != "&nbsp;" && dr["Item"].ToString() != "" && dr["Unit"].ToString() != "&nbsp;" && dr["Unit"].ToString() != "" && dr["Quantity"].ToString() != "&nbsp;" && dr["Quantity"].ToString() != "" && dr["PurchaseRate"].ToString() != "" && dr["PurchaseRate"].ToString() != "&nbsp;" )
                        {



                            ipurchase.PurBillNo = " ";
                            ipurchase.RefNo = dm["RefNo"].ToString();

                            try
                            {


                                ipurchase.Date = Convert.ToDateTime(dm["InvoiceDate"].ToString()); //DateTime.ParseExact(dm["InvoiceDate"].ToString(), "dd/MM/yyyy", CultureInfo.InvariantCulture);

                            }
                            catch
                            {
                                ipurchase.Date = DateTime.Now;
                                str += " " + (1).ToString();
                                blex = true;
                                break;
                            }
                            ipurchase.ItemPurchase = true;///dr["PurchaseMethod"].ToString() == "MaterialWise" ? false : true;
                            ipurchase.Supplier = dm["Supplier"].ToString();
                            ipurchase.Store = dm["Store"].ToString();
                            ipurchase.PONo = "0";
                            ipurchase.MrNo = 0;
                            try
                            {

                                ipurchase.Credit = bool.Parse(dm["Credit"].ToString());

                            }
                            catch
                            {

                                ipurchase.Credit = false;
                                str += " " + (1).ToString();
                                blex = true;
                                break;
                            }

                            ipurchase.Item = dr["Item"].ToString();
                            ipurchase.Unit = dr["Unit"].ToString();
                            ipurchase.Batch = dr["Batch"].ToString() == "&nbsp;" || dr["Batch"].ToString() == "" ? " " : dr["Batch"].ToString();
                            try
                            {
                                ipurchase.ExpiryDate = Convert.ToDateTime(dr["ExpiryDate"].ToString()); //DateTime.ParseExact(dr["ExpiryDate"].ToString(), "dd/mm/yyyy", CultureInfo.InvariantCulture);

                            }
                            catch
                            {
                                ipurchase.ExpiryDate = DateTime.Now;
                            }
                            try
                            {
                                ipurchase.PurchaseRate = Convert.ToDecimal(dr["PurchaseRate"]);




                                if (ipurchase.PurchaseRate < 0)//1
                                {
                                    ipurchase.PurchaseRate = 0;
                                    strNegative += " " + (i + 1).ToString();
                                    isNegative = true;
                                    break;

                                }
                              
                            }
                            catch
                            {
                                ipurchase.PurchaseRate = 0;
                                str += " " + (i + 1).ToString();
                                blex = true;
                                break;
                            }
                            try
                            {
                                ipurchase.Quantity = Convert.ToDecimal(dr["Quantity"]);



                                if (ipurchase.Quantity < 0)  //1
                                {
                                    ipurchase.Quantity = 0;
                                    strNegative += " " + (i + 1).ToString();
                                    isNegative = true;
                                    break;
                                }
                              
                            }
                            catch
                            {
                                ipurchase.Quantity = 0;
                                str += " " + (i + 1).ToString();
                                blex = true;
                                break;
                            }
                            //try
                            //{
                            //    ipurchase.SalesPrice = Convert.ToDecimal(dr["SalesPrice1"].ToString());



                            //    if (ipurchase.SalesPrice < 0)
                            //    {

                            //            ipurchase.SalesPrice = 0;//1
                            //            strNegative += " " + (i + 1).ToString();
                            //            isNegative = true;
                            //            break;
                                    
                            //    }
                            //}
                            //catch
                            //{
                            //    ipurchase.SalesPrice = 0;
                            //    str += " " + (i + 1).ToString();
                            //    blex = true;
                            //    break;
                            //}
                            //try
                            //{
                            //    ipurchase.WholeSalePrice = Convert.ToDecimal(dr["SalesPrice2"].ToString());



                            //            if (ipurchase.WholeSalePrice < 0)
                            //            {
                            //                ipurchase.WholeSalePrice = 0;//1
                            //                strNegative += " " + (i + 1).ToString();
                            //                isNegative = true;
                            //                break;
                            //            }
                            //}
                            //catch
                            //{
                            //    ipurchase.WholeSalePrice = 0;
                            //    str += " " + (i + 1).ToString();
                            //    blex = true;
                            //    break;
                            //}
                            //try
                            //{


                            //    ipurchase.InvoiceAmount = Convert.ToDecimal(dm["InvoiceAmount"].ToString());

                            //}
                            //catch
                            //{
                            //    ipurchase.InvoiceAmount = 0;
                            //    str += " " + (1).ToString();
                            //    blex = true;
                            //    break;
                            //}
                            //try
                            //{

                            //    ipurchase.Cost = Convert.ToDecimal(dm["Cost"].ToString());



                            //    if (ipurchase.Cost < 0)
                            //    {
                            //        ipurchase.Cost = 0;//1
                            //        strNegative += " " + (i + 1).ToString();
                            //        isNegative = true;
                            //        break;
                            //    }
                                
                            //}
                            //catch
                            //{
                            //    ipurchase.Cost = 0;
                            //    str += " " + (1).ToString();
                            //    blex = true;
                            //    break;
                            //}
                            //try
                            //{

                            //    ipurchase.Expense = Convert.ToDecimal(dm["Expense"]);


                            //    if (ipurchase.Expense < 0)
                            //    {
                            //        ipurchase.Expense = 0;//1
                            //        strNegative += " " + (i + 1).ToString();
                            //        isNegative = true;
                            //        break;
                            //    }

                            //}
                            //catch
                            //{
                            //    ipurchase.Expense = 0;
                            //    str += " " + (1).ToString();
                            //    blex = true;
                            //    break;
                            //}
                            ipurchase.Expense = 0;
                            ipurchase.Cost = 0;
                            if (ipurchase.PurchaseRate != 0 && ipurchase.Quantity != 0 )
                            {
                                decSum += ipurchase.Quantity*ipurchase.PurchaseRate;
                                arlist.Add(ipurchase);
                            }
                            else
                            {
                                blex = true;
                                str += " " + (i + 1).ToString();
                                break;
                            }
                        }
                        else
                        {
                            str += " " + (i + 1).ToString();
                            blex = true;
                            break;
                        }

                    }

                    /****Purchase Importing Starts Here****/
                    BlPurchase blpurchase = new BlPurchase();
                    blpurchase.FK_Client = Convert.ToInt64(hfClient.Value.Split(',')[0]);
                    blpurchase.UserCode = hfUser.Value;
                    blpurchase.InvoiceAmount = decSum;
                    
                    if (!blex)
                    {
                        if (isNegative)
                        {
                            string[] stRows = strNegative.Trim().Split(' ');

                            string rowindex = string.Join(",", stRows.Distinct().ToArray());
                            ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "toastr.warning('Values cannot be negative in Rows " + rowindex + " ')", true);
                        }
                        else if (arlist.Count > 0)
                        {

                            blpurchase.XmlPurchaseItems = Common.WriteToXml(arlist);
                            // string Status = blitems.ImportItems();

                            string strStatus = blpurchase.PurchaseImport();
                            if (strStatus == "")
                            {
                                ScriptManager.RegisterStartupScript(Page, typeof(Page), "script", "toastr.success('Submitted successfully');", true);

                            }
                            else
                            {

                                string[] strary = strStatus.Split('|');
                                string st = "";
                                if (strary[0] == "-15")
                                {
                                    st = "Invalid Supplier " + strary[1];
                                }
                                else if (strary[0] == "-16")
                                {
                                    st = "Invalid Store " + strary[1];

                                }
                                else if (strary[0] == "-17")
                                {
                                    st = "Invalid item  " + strary[1];

                                }
                                else if (strary[0] == "-18")
                                {
                                    st = "Invalid Unit  " + strary[1];
                                }
                                else if (strary[0] == "-19")
                                {
                                    st = "Invalid Item and unit " + strary[1];
                                }

                                else
                                {
                                    st = "Cannot Insert";
                                }
                                ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "toastr.error('" + st + "')", true);
                            }

                        }
                        else
                        {
                            ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "toastr.warning('No data to import')", true);
                        }
                    }
                    else
                    {

                        string[] stRows = str.Trim().Split(' ');

                        string rowindex = string.Join(",", stRows.Distinct().ToArray());
                        ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "toastr.warning('Invalid entries exists or missing required fields in  Rows " + rowindex + " ')", true);
                    }


                }
                else
                {
                    // Populate.ErrorMessageBox(this, "There is no data to import");
                    ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "toastr.warning('There is no data to import')", true);

                }

                Session[Excel] = null;
            }


        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "toastr.warning('Please save and close the importing document and Re-Import')", true);
        }

    }
    protected void imgbtnImport_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            divAdd.Style.Add("display", "none");
            divView.Style.Add("display", "none");
            divImport.Style.Add("display", "block");
           // ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Script", "DisableTextBox();", true);
           // imgbtnExport.Attributes.Add("disabled", "true");
            imgbtnExport.Enabled = false;


        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "toastr.warning('" + ex.Message + "')", true);
        }
    }

    #endregion
}