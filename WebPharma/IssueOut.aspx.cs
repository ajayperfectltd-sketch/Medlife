/*----------------------------------------------------------------------------------------------------------------------------------------------------------
Purpose    : Insert/Update/Delete Issue Out
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

public partial class IssueOut : System.Web.UI.Page
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
            BlIssueOut BlIssue = new BlIssueOut();
            BlIssue.UserCode = hfuserName.Value;
            BlIssue.MasterId = Convert.ToInt64(hfIssueOut.Value);
            BlIssue.Fk_Reasons = Convert.ToInt64(Reason);
            if (hfIsDelete.Value.ToLower() == "true")
            {
                status = BlIssue.DeleteData();
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
               // txtSearchName.Focus();
            }
            BlIssue = null;
        }
    }

    private void AddView()
    {
        divAdd.Style.Add("display", "block");
        divView.Style.Add("display", "none");
      
    }
    private void CategoryDisable()
    {
        if (gvIssuedItems.Rows.Count > 0)
        {
            ddlCategory.Enabled = false;
            ddlStore.Enabled = false;

        }
        else
        {
            ddlCategory.Enabled = true;
            ddlStore.Enabled = true;
        }
    }
    private const string TempTable = "TempTable";
    protected void CheckUserAccess()
    {
        BlUserAccess bluseraccess = new BlUserAccess();
        Populate populate = new Populate();

        bluseraccess.FK_Users = Convert.ToInt64(hfUser.Value);
        bluseraccess.FK_Pages = 37;
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
    protected void BindData()
    {
        Populate populate = new Populate();
        populate.FillDropdownList(ddlStore, "Store", "StoName", "ID_Store", "StoName", "Cancelled=0 and Active='True' AND FK_Client= " + hfClient.Value.Split(',')[0], "");
        populate.FillDropdownList(ddlSearchStore, "Store", "StoName", "ID_Store", "StoName", " Cancelled=0 and Active='True' AND FK_Client= " + hfClient.Value.Split(',')[0], "");
        populate.FillDropdownList(ddlReason, "Reason", "Reason", "ID_Reason", "Reason", "Cancelled=0 and ReasonMode=3 and  FK_Client= " + hfClient.Value.Split(',')[0], "");
        populate.FillDropdownList(ddlCategory, "Category", "CategoryName", "ID_Category", "CategoryName", "Cancelled=0 and  FK_Client= " + hfClient.Value.Split(',')[0], "");
    //    populate.FillDropdownList(ddlCategory, "CategoryFirst", "Name", "ID_CategoryFirst", "Name", "  FK_Client= " + hfClient.Value.Split(',')[0], "All");
        //ddlUnit.SelectedValue = blitems.FK_Units.ToString();
        //  populate.FillDropdownList(ddlItem, "Items", "Name", "ID_Items", "ID_Items", "  Active='True' AND ItemType=2 AND FK_Client= " + hfClient.Value.Split(',')[0], "");
        populate = null;
        // txtBillDate.Text = DateTime.Now.ToShortDateString();
    }
    private void SetControls()
    {
        txtDate.Attributes.Add("readonly", "readonly");
        txtFromDate.Attributes.Add("readonly", "readonly");
        txtTodate.Attributes.Add("readonly", "readonly");
        txtAmount.Attributes.Add("readonly", "reasonly");

    }
    protected void BindStockDetail(long ID_Stock, long Fk_item)
    {
        BlStock Blstock = new BlStock();
        Blstock.MasterId = 0;
        Blstock.FK_Client = Convert.ToInt64(hfClient.Value.Split(',')[0]);
        Blstock.FK_Items = Fk_item;
        Blstock.FK_Store = Convert.ToInt64(ddlStore.SelectedValue);
        Blstock.MasterId = ID_Stock;
        Blstock.PageIndex = 1;

        DataTable dtbl = new DataTable();
        dtbl = Blstock.SelectAllData();
        gvIssuedItemDetails.DataSource = dtbl;
        gvIssuedItemDetails.DataBind();

    }
    protected void GridFill(int currentPage)
    {
        BlIssueOut issueout = new BlIssueOut();
        DataTable dtbl = new DataTable();
        issueout.FK_Store = Convert.ToInt64(ddlSearchStore.SelectedValue.ToString());
        issueout.FK_Client = Convert.ToInt64(hfClient.Value);
        issueout.UserCode = hfUser.Value;
        issueout.FromDate = DateTime.ParseExact(txtFromDate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
        issueout.ToDate = DateTime.ParseExact(txtTodate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
        issueout.IssueType = ddlIssueTypeSearch.SelectedValue == "0" ? "" : ddlIssueTypeSearch.SelectedValue;
        issueout.PageSize = 10;
        issueout.PageIndex = Convert.ToInt16(currentPage);
        dtbl = issueout.SelectAllData();
        gvViewIssued.DataSource = dtbl;
        gvViewIssued.DataBind();
       
        issueout = null;
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
    protected void SaveData()
    {
        long status = 0;

        DateTime transDate;
        if (!DateTime.TryParseExact(txtDate.Text, "dd/MM/yyyy",
                CultureInfo.InvariantCulture, DateTimeStyles.None, out transDate))
        {
            ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script",
                "alertify.alert('Invalid date format. Please use dd/MM/yyyy.');", true);
            ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "ShowAddDiv();", true);
            return;
        }

        DateTime today = DateTime.Today;

        // 1. Block future dates
        if (transDate > today)
        {
            ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script",
                "alertify.alert('Future date not allowed.');", true);
            ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "ShowAddDiv();", true);
            return;
        }

        // 2. Determine current financial year start (April 1)
        int fyStartYear = (today.Month >= 4) ? today.Year : today.Year - 1;
        DateTime fyStart = new DateTime(fyStartYear, 4, 1);

        // 3. Block dates before the current financial year
        if (transDate < fyStart)
        {
            ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script",
                "alertify.alert('Backdated entry not allowed before the current financial year (from 01/04/"
                + fyStartYear + ").');", true);
            ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "ShowAddDiv();", true);
            return;
        }

        BlIssueOut issueout = new BlIssueOut();
        issueout.FK_Store = Convert.ToInt64(ddlStore.SelectedValue);
        issueout.FK_Category = Convert.ToInt64(ddlCategory.SelectedValue);
        issueout.IssueType = ddlIssueType.SelectedValue.ToString();
        issueout.TransDate = DateTime.ParseExact(txtDate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
        issueout.TotalAmt = Convert.ToDecimal(txtAmount.Text);
        issueout.Fk_Reasons = Convert.ToInt64(ddlReason.SelectedValue);
        issueout.UserCode = hfUser.Value;
        issueout.EnteredOn = DateTime.Now;
        issueout.Cancelled = false;
        issueout.FK_Client = Convert.ToInt16(hfClient.Value.Split(',')[0]);
        ArrayList arlist1 = new ArrayList();
        DataTable dtbl1 = (DataTable)ViewState[TempTable];
        BlIssueoutItems BlIssueoutItems;
        for (int i = 0; i < dtbl1.Rows.Count; i++)
        {
            BlIssueoutItems = new BlIssueoutItems();
            BlIssueoutItems.FK_Items = Convert.ToInt64(dtbl1.Rows[i]["FK_Items"].ToString());
            BlIssueoutItems.FK_Stock = Convert.ToInt64(dtbl1.Rows[i]["FK_Stock"].ToString());
            BlIssueoutItems.CurrentStock = decimal.Parse(dtbl1.Rows[i]["Quantity"].ToString());
            BlIssueoutItems.FK_IssueType = (dtbl1.Rows[i]["IssueType"].ToString());
            BlIssueoutItems.FK_Reason = Convert.ToInt64(dtbl1.Rows[i]["FK_Reason"].ToString());
            arlist1.Add(BlIssueoutItems);
        }
        issueout.XmlIssuedItems = Common.WriteToXml(arlist1);




        if (hfIssueOut.Value.Split(',')[0] == "0")
        {




            if (hfIsAdd.Value.ToLower() == "true")
            {
                status = issueout.insertData();
                if (status > 0)
                {
                    Populate.InsertMsg(this);
                    GridFill(1);
                    ClearAll();
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "ShowAddDiv();", true);
                    ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "toastr.error('Cannot insert data')", true);
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
                issueout.MasterId = Convert.ToInt64(hfIssueOut.Value.Split(',')[0]);
                status = issueout.UpdateData();
                if (status > 0)
                {
                    ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "ShowAddDiv();", true);
                    ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "toastr.success('Data Updated Successfully');ClearAdd();", true);
                    GridFill(1);
                    ClearAll();
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "ShowAddDiv();", true);
                    ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "toastr.error('Cannot update data')", true);

                }
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "toastr.warning('User does not have permission to Update')", true);

            }
            issueout = null;
        }

    }
    private DataTable GetDefaultTable()
    {
        DataTable dtbl = new DataTable();
        dtbl.Columns.Add("Item");
        dtbl.Columns.Add("Quantity");
        dtbl.Columns.Add("PRate");
        dtbl.Columns.Add("FK_Items");
        dtbl.Columns.Add("FK_Stock");
        dtbl.Columns.Add("SalPrice");
        dtbl.Columns.Add("FK_Category");
        dtbl.Columns.Add("TotalAmt");
        dtbl.Columns.Add("CurrentStock");
        dtbl.Columns.Add("FK_Reason");
        dtbl.Columns.Add("IssueType");
        return dtbl;
    }
    private void ClearIssueDetails()
    {
        gvIssuedItems.DataSource = new DataTable();
        gvIssuedItems.DataBind();
        ViewState[TempTable] = null;
    }
    protected void AddData()
    {
        DataTable dtbl = new DataTable();
        int rowCount = 0;
        int rowindex = 0;
        int inCheck = 0;
        //  bool IsSameCatgory = false;

        decimal decqty = decimal.Parse(txtQuantity.Text == "" ? "0" : txtQuantity.Text);
        decimal decCS = decimal.Parse(hdfCurrentStock.Value.Split(',')[0] == "" ? "0" : hdfCurrentStock.Value.Split(',')[0]);
        if (decqty > decCS)
        {
            string str = "Quantity must be less than or equal to " + decCS.ToString();
            ScriptManager.RegisterStartupScript(Page, typeof(Page), "script", "alertify.alert('"+str+"')", true);
            return;
        }
        if (txtitem.Text.Trim() != hfItem.Value.Trim())
        {
            ScriptManager.RegisterStartupScript(Page, typeof(Page), "script", "alertify.alert('Item  does not exist')", true);
            // ScriptManager.RegisterStartupScript(Page, typeof(Page), "script", "toastr.warning('Item  does not exist')", true);
            return;
        }

        if (ViewState[TempTable] != null)
        {
            dtbl = (DataTable)ViewState[TempTable];
            rowCount = dtbl.Rows.Count;
        }
        else
        {
            dtbl = GetDefaultTable();


        }
        DataRow drow;

        if (hdfRowindex.Value.Split(',')[0] == "")
        {

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
                drow = dtbl.NewRow();
                drow["Item"] = txtitem.Text.Trim();
                drow["Quantity"] = decimal.Parse(txtQuantity.Text == "" ? "0" : txtQuantity.Text).ToString("0.0000");
                drow["FK_Items"] = txtfkIssuedItems.Text.Split(',')[0];
                drow["FK_Stock"] = hdfFKStock.Value.Split(',')[0];
                drow["PRate"] = txtPurChaseRate.Text.Split(',')[0];
                drow["SalPrice"] = decimal.Parse(txtSalesPrice.Text.Split(',')[0] == "" ? "0" : txtSalesPrice.Text.Split(',')[0]).ToString("0.0000");
                drow["FK_Category"] = hdfcategory.Value.Split(',')[0];
                drow["TotalAmt"] = (decimal.Parse(drow["Quantity"].ToString()) * decimal.Parse(drow["SalPrice"].ToString())).ToString("0.0000");
                drow["CurrentStock"] = hdfCurrentStock.Value.Split(',')[0];
                drow["FK_Reason"] = ddlReason.SelectedValue == "" ? "0" : ddlReason.SelectedValue;
                drow["IssueType"] = ddlIssueType.SelectedValue == "" ? "0" : ddlIssueType.SelectedValue;
                dtbl.Rows.Add(drow);
            }
            else
            {
                if (inCheck > 0)
                    ScriptManager.RegisterStartupScript(Page, typeof(Page), "script", "alertify.alert('Stock Item already exists')", true);

            }

        }
        else
        {
            int i = int.Parse(hdfRowindex.Value.Split(',')[0]);
            if (dtbl.Rows.Count > 0)
            {

                for (int j = 0; j < dtbl.Rows.Count; j++)
                {
                    if (dtbl.Rows[j]["FK_Stock"].ToString() == hdfFKStock.Value.Split(',')[0] && j != i)
                    {

                        inCheck = inCheck + 1;

                        break;
                    }


                }


            }


            if (inCheck == 0)
            {

                drow = dtbl.Rows[i];
                drow["Item"] = txtitem.Text.Trim();
                drow["Quantity"] = decimal.Parse(txtQuantity.Text == "" ? "0" : txtQuantity.Text).ToString("0.0000");
                drow["FK_Items"] = txtfkIssuedItems.Text.Split(',')[0];
                drow["FK_Stock"] = hdfFKStock.Value.Split(',')[0];
                drow["PRate"] = txtPurChaseRate.Text.Split(',')[0];
                drow["SalPrice"] = txtSalesPrice.Text.Split(',')[0] == "" ? "0" : txtSalesPrice.Text.Split(',')[0];
                drow["FK_Category"] = hdfcategory.Value.Split(',')[0];
                drow["TotalAmt"] = (decimal.Parse(drow["Quantity"].ToString()) * decimal.Parse(drow["SalPrice"].ToString())).ToString("0.0000");
                drow["CurrentStock"] = hdfCurrentStock.Value.Split(',')[0];
                drow["FK_Reason"] = ddlReason.SelectedValue;
                drow["IssueType"] = ddlIssueType.SelectedValue == "" ? "0" : ddlIssueType.SelectedValue;
            }
            else
            {
                ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "alertify.alert('Stock Item already exists')", true);

            }
        }

        ClearIssuedDetails();
        ViewState[TempTable] = dtbl;

        gvIssuedItems.DataSource = dtbl;
        gvIssuedItems.DataBind();
        gvIssuedItems.Columns[6].Visible = false;
        gvIssuedItems.Columns[7].Visible = false;
        CategoryDisable();
        txtitem.Focus();
    }
    private void ClearIsssueOut()
    {
        ViewState[TempTable] = null;
        gvIssuedItems.DataSource = new DataTable();
        gvIssuedItems.DataBind();
        txtitem.Text = "";
        txtItemShortNameCopy.Text = "";
        txtQuantity.Text = "";
        ClearIssuedDetails();

    }
    private void ClearIssuedDetails()
    {
        gvIssuedItemDetails.DataSource = new DataTable();
        gvIssuedItemDetails.DataBind();
        txtfkIssuedItems.Text = "";
        txtitem.Text = "";
        txtItemShortNameCopy.Text = "";
        txtQuantity.Text = "";
        hdfFkItem.Value = "";
        hdfFKStock.Value = "";
        txtPurChaseRate.Text = "";
        txtSalesPrice.Text = "";
        hdfRowindex.Value = "";
        hdfcategory.Value = "";
        hdfCurrentStock.Value = "0";
        hdfPacked.Value = "False";
        ddlIssueType.SelectedIndex = -1;
        ddlReason.SelectedIndex = -1;
     
        
        
    }
    private void ClearAll()
    {
        ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "ClearAdd();", true);
        ClearIsssueOut();
        ddlCategory.SelectedIndex = 0;
        hfIssueOut.Value = "0";

        // Restore default store from ViewState — no hidden field needed
        string defaultStore = ViewState["DefaultStore"] != null
                              ? ViewState["DefaultStore"].ToString()
                              : "0";

        if (!string.IsNullOrEmpty(defaultStore)
            && ddlStore.Items.FindByValue(defaultStore) != null)
        {
            ddlStore.SelectedValue = defaultStore;
        }
        else
        {
            ddlStore.SelectedIndex = 0;
        }

        ddlIssueType.SelectedIndex = 0;
        CategoryDisable();
        ddlCategory.Enabled = true;
        ddlStore.Enabled = true;
        ddlIssueType.Enabled = true;
        ddlReason.SelectedIndex = 0;
    }

   
    #endregion
    #region Events
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
       txtTodate.Text= txtFromDate.Text= txtDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
       txtDate.Focus();
            hfUser.Value = Request.QueryString["uid"];
            hfClient.Value = Request.QueryString["clnt"];
            Page.Header.DataBind();
           // blMessage.Items.Clear();


            divAdd.Style.Add("display", "block");
            SetControls();
            BindData();
            SetDefaultStore(); 
            CheckUserAccess();
            hfuserName.Value = Session["Username"].ToString();
            AddView();
            ClearAll();
            if (Convert.ToBoolean(hfIsAdd.Value) == false)
            {
                ((Label)CommonPopupPrivilege.FindControl("popuplabel")).Text = "User does not have permission to Add";
                mpPrivilege.Show();
            }
            if (Convert.ToBoolean(hfIsUpdate.Value) == false)
            {
                gvViewIssued.Columns[5].Visible = false;
            }
            if (Convert.ToBoolean(hfIsDelete.Value) == false)
            {
                gvViewIssued.Columns[6].Visible = false;
            }
            ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "ClearAdd();", true);
        }
    }
    protected void btnAddItems_Click(object sender, EventArgs e)
    {
        try
        {
            if (txtfkIssuedItems.Text == "")
            {
                ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "alertify.alert('Item Required')", true);
            }
            else
            {
                AddData();
            }
            AddView();
        }
        catch (Exception ex)
        {
        }

    }
    protected void btnClearItems_Click(object sender, EventArgs e)
    {
        try
        {
            //ClearIsssueOut();
            ClearIssuedDetails();
            AddView();
            ddlCategory.SelectedIndex = -1;
        }
        catch (Exception ex)
        {
        }
    }


    protected void imgbtnView_Click(object sender, ImageClickEventArgs e)
    {
       // blMessage.Items.Clear();
        try
        {
            //blMessage.Items.Clear();
            divAdd.Style.Add("display", "none");
            divView.Style.Add("display", "block");
            ClearAll();
            //ClearSearch();
            ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "ClearAdd();", true);
ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "ClearSearch();", true);
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
        try
        {
            ClearAll();
            divAdd.Style.Add("display", "block");
            divView.Style.Add("display", "none");
        }
        catch (Exception ex)
        {
        }

    }


    protected void gvItems_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {

    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            if (gvIssuedItems.Rows.Count > 0)
            {
                SaveData();
            }
            else
            {


                ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "alertify.alert('Atleast one Item Required')", true);
            }
            AddView();
        }
        catch (Exception ex)
        {
        }
    }
    protected void btnClear_Click1(object sender, EventArgs e)
    {
        ClearAll();
        AddView();
        ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "ClearAdd();", true);
    
    }
    protected void Page_Changed(object sender, EventArgs e)
    {
       // blMessage.Items.Clear();
        int pageIndex = int.Parse((sender as LinkButton).CommandArgument);

        GridFill(pageIndex);
    }

    protected void btnItem_Click(object sender, EventArgs e)
    {
        try
        {
            // string dk = hdfFKStock.Value;
            // string a2 = hdfFkItem.Value;

            BindStockDetail(Convert.ToInt64(hdfFKStock.Value.Split(',')[0]), Convert.ToInt64(hdfFkItem.Value.Split(',')[0]));
            AddView();
            txtQuantity.Focus();
        }
        catch (Exception ex)
        {
        }
    }
    protected void gvIssuedItems_RowDeleting(object sender, GridViewDeleteEventArgs e)
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
                    if (ViewState[TempTable] != null)
                    {
                        ClearIssuedDetails();
                        DataTable dtbl = (DataTable)ViewState[TempTable];
                        dtbl.Rows.RemoveAt(e.RowIndex);
                        gvIssuedItems.DataSource = dtbl;
                        gvIssuedItems.DataBind();
                        CategoryDisable();
                        ViewState[TempTable] = dtbl;

                    }
                }
                AddView();

            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "toastr.warning('" + ex.Message + "')", true);
            }

        }
        catch (Exception ex)
        {
        }

    }
    protected void gvIssuedItems_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        try
        {
            ClearIssuedDetails();
            hdfRowindex.Value = (gvIssuedItems.PageIndex + e.NewSelectedIndex).ToString();
           
            string FK_Item = gvIssuedItems.DataKeys[e.NewSelectedIndex]["FK_Items"].ToString();
            string FK_Stock = gvIssuedItems.DataKeys[e.NewSelectedIndex]["FK_Stock"].ToString();
            DataTable dt = (DataTable)ViewState[TempTable];
            string FK_Category = dt.Rows[0]["FK_Category"].ToString();
            string CurrentStock = gvIssuedItems.DataKeys[e.NewSelectedIndex]["CurrentStock"].ToString();
            BindStockDetail(Convert.ToInt64(FK_Item), Convert.ToInt64(FK_Stock));
            txtfkIssuedItems.Text = FK_Item;
            txtitem.Text = gvIssuedItems.Rows[e.NewSelectedIndex].Cells[1].Text;
            txtItemShortNameCopy.Text = txtitem.Text;
            txtQuantity.Text = gvIssuedItems.Rows[e.NewSelectedIndex].Cells[2].Text;
            decimal currentStock = decimal.Parse(CurrentStock == "" ? "0" : CurrentStock);
            decimal AvailQty=decimal.Parse(txtQuantity.Text==""?"0":txtQuantity.Text);
            hdfCurrentStock.Value = currentStock.ToString();
            
            hdfFkItem.Value = FK_Item;
            hdfFKStock.Value = FK_Stock;
            hdfcategory.Value = FK_Category;
            txtPurChaseRate.Text = gvIssuedItems.Rows[e.NewSelectedIndex].Cells[3].Text;
            txtSalesPrice.Text = gvIssuedItems.Rows[e.NewSelectedIndex].Cells[4].Text;
            AddView();
            hfItem.Value = gvIssuedItems.Rows[e.NewSelectedIndex].Cells[1].Text;
            ddlCategory.SelectedValue = hdfcategory.Value;
           ddlReason.SelectedValue = gvIssuedItems.Rows[e.NewSelectedIndex].Cells[6].Text;
           ddlIssueType.SelectedValue = gvIssuedItems.Rows[e.NewSelectedIndex].Cells[7].Text;
           bindReason();
        }
        catch (Exception ex)
        {
        }

    }
    protected void ddlCategory_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {

            CategoryDisable();
            if (ddlCategory.SelectedIndex > 0)
            {
                //ClearIssueDetails();
                ClearIsssueOut();

            }
            AddView();
            imgPopUp.Focus();
            //if (ddlCategory.SelectedIndex > 0)
            //{
            //    if (ddlCategory.SelectedIndex > 0 && gvIssuedItems.Rows.Count > 0 && ViewState[TempTable] != null)
            //    {
            //        if (gvIssuedItems.DataKeys[0]["FK_Category"].ToString() != ddlCategory.SelectedValue)
            //        {
            //            ClearIsssueOut();
            //        }
            //else

            //    }
            //}

        }
        catch (Exception ex)
        {
        }
    }

    protected void gvViewIssued_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {

        try
        {
           // blMessage.Items.Clear();
            ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "ShowAddDiv();", true);
            BlIssueOut blissueout = new BlIssueOut();
            blissueout.MasterId = Convert.ToInt64(gvViewIssued.DataKeys[e.NewSelectedIndex]["ID_IssueOut"].ToString());
            hfIssueOut.Value = blissueout.MasterId.ToString();
            blissueout.FromDate = DateTime.Parse("1/1/1753");
            blissueout.ToDate = DateTime.MaxValue;
            blissueout.FK_Store = 0;
            blissueout.IssueType = "";
            blissueout.FK_Client = Convert.ToInt64(hfClient.Value);

            blissueout.SelectData();

            ddlStore.SelectedValue = blissueout.FK_Store.ToString();
            ddlCategory.SelectedValue = blissueout.FK_Category.ToString();
          //  ddlIssueType.SelectedValue = blissueout.IssueType.ToString();
          //  ddlReason.SelectedValue = blissueout.Fk_Reasons.ToString();
            txtDate.Text = blissueout.TransDate.ToString("dd/MM/yyyy");
            txtAmount.Text = blissueout.TotalAmt.ToString();

            DataTable dt = new DataTable();
            dt = blissueout.SelectIssueOutItems();
            ddlCategory.SelectedValue = dt.Rows[0]["FK_Category"].ToString();
            ViewState[TempTable] = dt;
            gvIssuedItems.DataSource = dt;
            gvIssuedItems.DataBind();
            gvIssuedItems.Columns[6].Visible = false;
            gvIssuedItems.Columns[7].Visible = false;
            AddView();
            ddlCategory.Enabled = false;
            ddlStore.Enabled = false;
            //ddlIssueType.Enabled = false;
            bindReason();
            blissueout = null;

        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "toastr.warning('" + ex.Message + "')", true);
        }
    }
    protected void gvViewIssued_RowDeleting(object sender, GridViewDeleteEventArgs e)
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
                    long status = 0;
                    BlIssueOut BlIssue = new BlIssueOut();
                    BlIssue.MasterId = Convert.ToInt64(gvViewIssued.DataKeys[e.RowIndex]["ID_IssueOut"].ToString());
                    hfIssueOut.Value = (gvViewIssued.DataKeys[e.RowIndex]["ID_IssueOut"].ToString());
                    BlIssue.FK_Client = Convert.ToInt64(hfClient.Value);
                    BlIssue.UserCode = hfUser.Value.Split(',')[0];
                    //if (hfIsDelete.Value.ToLower() == "true")
                    //{
                    //    status = BlIssue.DeleteData();
                    //    if (status > 0)
                    //    {
                    //        GridFill(1);
                    //        ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "toastr.success('Data deleted successfully')", true);
                    //    }
                    //    else
                    //    {
                    //        ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "toastr.error('Cannot delete data, reference exists')", true);
                    //    }
                    //}
                    //else
                    //{
                    //    ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "toastr.warning('User does not have permission to Delete')", true);

                    //}
                    BlIssue = null;
                }
                else
                {
                    mpDelete.Hide();
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "toastr.warning('" + ex.Message + "')", true);
            }


        }
        catch (Exception ex)
        {
        }

    }
    protected void imgbtnAdd_Click(object sender, ImageClickEventArgs e)
    {
        ClearAll();
        if (Convert.ToBoolean(hfIsAdd.Value) == false)
        {
            ((Label)CommonPopupPrivilege.FindControl("popuplabel")).Text = "User does not have permission to Add";
            mpPrivilege.Show();
        }
        divAdd.Style.Add("display", "block");
        divView.Style.Add("display", "none");

    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        try
        {
          //  blMessage.Items.Clear();
            GridFill(1);
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
            ClearIsssueOut();
            ddlCategory.Focus();
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "toastr.warning('" + ex.Message + "')", true);
        }

    }
    #endregion


    protected void ImageButton2_Click(object sender, ImageClickEventArgs e)
    {
        Delete();
    }
    protected void btnReason_Click(object sender, EventArgs e)
    {
        bindReason();

    }
    private void bindReason()
    {
        if (ddlIssueType.SelectedValue == "D")
        {
            ddlReason.Visible = true;
            lblReason.Visible = true;
        }
        else
        {
            ddlReason.Visible = false;
            lblReason.Visible = false;
        }
    }


    private void SetDefaultStore()
    {
        try
        {
            WebPharma.Business.BlPopulate blPopulate = new WebPharma.Business.BlPopulate();
            blPopulate.TableName = "Users";          // adjust to your actual Users table name
            blPopulate.ListFields = "FK_Store";
            blPopulate.ValueFields = "ID_Users";
            blPopulate.SortFields = "ID_Users";
            blPopulate.Criteria = "ID_Users = " + hfUser.Value.Split(',')[0]
                                     + " AND FK_Client = " + hfClient.Value.Split(',')[0];

            DataTable dt = blPopulate.PopulateData();

            if (dt != null && dt.Rows.Count > 0)
            {
                string defaultStore = dt.Rows[0][0].ToString(); // first column = FK_Store

                // Store server-side in ViewState — no hidden field needed
                ViewState["DefaultStore"] = defaultStore;

                // Pre-select Add form dropdown
                if (ddlStore.Items.FindByValue(defaultStore) != null)
                    ddlStore.SelectedValue = defaultStore;

                // Pre-select Search/View dropdown
                if (ddlSearchStore.Items.FindByValue(defaultStore) != null)
                    ddlSearchStore.SelectedValue = defaultStore;

                // Pass value to JavaScript via a startup script variable
                ScriptManager.RegisterStartupScript(Page, typeof(Page),
                    "DefaultStore",
                    "var _defaultStore = '" + defaultStore + "';", true);
            }
            else
            {
                ViewState["DefaultStore"] = "0";
                ScriptManager.RegisterStartupScript(Page, typeof(Page),
                    "DefaultStore", "var _defaultStore = '0';", true);
            }
        }
        catch
        {
            ViewState["DefaultStore"] = "0";
            ScriptManager.RegisterStartupScript(Page, typeof(Page),
                "DefaultStore", "var _defaultStore = '0';", true);
        }
    }
}