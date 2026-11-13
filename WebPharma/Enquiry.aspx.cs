using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebPharma.Business.WebPharma;


public partial class Enquiry : System.Web.UI.Page
{
    protected void BindData()
    {
        Populate populate = new Populate();

      
        populate.FillDropdownList(ddlRack, "Rack", "RackName", "ID_Rack", "RackName", "FK_Client = " + hfClient.Value + "AND Cancelled=0", "");
        populate.FillDropdownList(ddlbin, "Bin", "BinName", "ID_Bin", "BinName", " FK_Client=" + hfClient.Value + "AND Cancelled=0", "");
        populate.FillDropdownList(ddlGeneric, "Generic", "GenericName", "ID_Generic", "GenericName", " FK_Client=" + hfClient.Value + "AND Cancelled=0", "");
        populate.FillDropdownList(ddlManufacturer, "Manufacturer", "ManufactureName", "ID_Manufacturer", "ManufactureName", "FK_Client=" + hfClient.Value + "AND Cancelled=0", "");
        populate.FillDropdownList(ddlCategory, "Category", "CategoryName", "ID_Category", "CategoryName", " FK_Client= " + hfClient.Value + "AND Cancelled=0", "");
        populate.FillDropdownList(ddlSubcategory, "SubCategory", "SubCategoryName", "ID_SubCategory", "SubCategoryName", " FK_Client= " + hfClient.Value + "AND Cancelled=0", "");
        populate.FillDropdownList(ddlUnit, "Units", "UnitName", "ID_Units", "UnitName", " FK_Client= " + hfClient.Value + "AND Cancelled=0", "");
        populate.FillDropdownList(ddlSubUnit, "SubUnit", "SubUnitName", "ID_SubUnits", "SubUnitName", " FK_Client= " + hfClient.Value + "AND Cancelled=0", "");
        populate.FillDropdownList(ddlManufacturer, "Manufacturer", "ManufactureName", "ID_Manufacturer", "ManufactureName", "FK_Client=" + hfClient.Value + "AND Cancelled=0", "");
        populate.FillDropdownList(ddlScedule, "ScheduleMaster", "ScheduleName", "ID_Schedule", "ScheduleName", "FK_Client=" + hfClient.Value + "AND Cancelled=0", "");
        populate.FillDropdownList(ddlSupplier, "Supplier", "SupplierName", "ID_Supplier", "SupplierName", "Active='True' AND Cancelled=0 AND FK_Client= " + hfClient.Value.Split(',')[0], "");
        populate.FillDropdownList(ddlStore, "Store", "StoName", "ID_Store", "StoName", " Active='True'  AND Cancelled=0 AND FK_Client= " + hfClient.Value.Split(',')[0], "");
     
        BinSubcategory();
        BinBin();
      

    }
    protected void BinSubcategory()
    {
        new Populate().FillDropdownList(ddlSubcategory, "SubCategory", "SubCategoryName", "ID_SubCategory", "SubCategoryName", "FK_Category = " + ddlCategory.SelectedValue + " and FK_Client = " + hfClient.Value + "AND Cancelled=0", "");

    }

    protected void BinBin()
    {
        new Populate().FillDropdownList(ddlbin, "Bin", "BinName", "ID_Bin", "BinName", "FK_Rack = " + ddlRack.SelectedValue + " and FK_Client = " + hfClient.Value + "AND Cancelled=0", "");
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            hfUser.Value = Request.QueryString["uid"];
            hfClient.Value = Request.QueryString["clnt"];
            BindData();
            this.BindGrid();
        }

    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        try
        {
          
            GridFill(1);
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "toastr.warning('" + ex.Message + "')", true);
        }

    }
    protected void GridFill(int currentPage)
    {
        BlItemEnquiry blItems = new BlItemEnquiry();
        DataTable dtbl = new DataTable();
        blItems.Name = txtSearchName.Text.Trim();
        blItems.FK_Category = Convert.ToInt64(ddlCategory.SelectedValue);
        blItems.FK_SubCategory = Convert.ToInt64(ddlSubcategory.SelectedValue);
        blItems.DoctorName = Convert.ToString(txtDoctor.Text);
        blItems.CustomerName = Convert.ToString(txtCustomer.Text);
        blItems.FK_Manufacturer = Convert.ToInt64(ddlManufacturer.SelectedValue);
        blItems.FK_Units = Convert.ToInt64(ddlUnit.SelectedValue);
        blItems.FK_SubUnit = Convert.ToInt64(ddlSubUnit.SelectedValue);
        blItems.FK_Supplier = Convert.ToInt64(ddlSupplier.SelectedValue);
        blItems.FK_Store = Convert.ToInt64(ddlStore.SelectedValue);
        blItems.FK_Generic = Convert.ToInt64(ddlGeneric.SelectedValue);
        blItems.FK_Schedule = Convert.ToInt64(ddlScedule.SelectedValue);
        blItems.BatchNo = txtBatchNo.Text.Trim();
        blItems.FK_Rack = Convert.ToInt64(ddlRack.SelectedValue);
        blItems.FK_Bin = Convert.ToInt64(ddlbin.SelectedValue);
        blItems.MRP = Convert.ToDecimal(txtMrp.Text.Trim() == "" ? "0 ": txtMrp.Text.Trim());
        blItems.Salesprice = Convert.ToDecimal(txtSalesPrice.Text.Trim()==""?"0":txtSalesPrice.Text.Trim());
        blItems.PurchaseRate =  Convert.ToDecimal(txtPurchaseRate.Text.Trim()==""?"0":txtPurchaseRate.Text.Trim());
        blItems.CriteriaMRP = ddlCriteria.SelectedItem.Text;
        blItems.CriteriaPurchase = ddlPurchse.SelectedItem.Text;
        blItems.CreteriaSales = ddlSalesPrice.SelectedItem.Text;
        blItems.Zerostock = Convert.ToBoolean(chkzeorstock.Checked);
        blItems.UserCode = hfUser.Value;
        blItems.Level = Convert.ToInt32(ddlReorder.SelectedValue);
        dtbl = blItems.SelectItemsData();
        gvEnquiry.DataSource = dtbl;
        gvEnquiry.DataBind();
        ViewState["DataValue"] = dtbl;

        blItems = null;
    }
    protected void gvPriviousPurchase_Sorting(object sender, GridViewSortEventArgs e)
    {
        this.BindGrid(e.SortExpression);
    }
    private void BindGrid(string sortExpression = null)
    {
        DataTable dt = (DataTable)(ViewState["DataValue"]);
        if (sortExpression != null)
        {
            DataView dv = dt.AsDataView();
            this.SortDirection = this.SortDirection == "ASC" ? "DESC" : "ASC";

            dv.Sort = sortExpression + " " + this.SortDirection;
            gvEnquiry.DataSource = dv;
        }
        else
        {
            gvEnquiry.DataSource = dt;
        }
        gvEnquiry.DataBind();
    }
    private string SortDirection
    {
        get { return ViewState["SortDirection"] != null ? ViewState["SortDirection"].ToString() : "ASC"; }
        set { ViewState["SortDirection"] = value; }
    }
   
    protected void btnRack_Click(object sender, EventArgs e)
    {
        try
        {



            Populate populate = new Populate();
            new Populate().FillDropdownList(ddlbin, "Bin", "BinName", "ID_Bin", "BinName", "FK_Rack = " + ddlRack.SelectedValue + " and FK_Client = " + hfClient.Value + "AND Cancelled=0", "");
            populate = null;
            //divAdd.Style.Add("display", "block");
           // divView.Style.Add("display", "none");
            ddlbin.Focus();

        }

        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "toastr.warning('" + ex.Message + "')", true);
        }
    }

    protected void btnSubCategor_Click(object sender, EventArgs e)
    {
        Populate populate = new Populate();
        new Populate().FillDropdownList(ddlSubcategory, "SubCategory", "SubCategoryName", "ID_SubCategory", "SubCategoryName", "FK_Category = " + ddlCategory.SelectedValue + " and FK_Client = " + hfClient.Value + "AND Cancelled=0", "");
        populate = null;
        //divAdd.Style.Add("display", "block");
        // divView.Style.Add("display", "none");
        ddlSubcategory.Focus();

    }
    protected void IbtnItemAdd_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            ItemName();
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "toastr.warning('" + ex.Message + "')", true);
        }
    }
    public void ItemName()
    {
        try
        {
            ((TextBox)CommonPopup1.FindControl("txtName")).Text = txtSearchName.Text.Trim();
            ((TextBox)CommonPopup1.FindControl("txtRefno")).Text = txtSearchName.Text.Trim();
            ((HiddenField)CommonPopup1.FindControl("hfColumn")).Value = "Name";
            ItemFill(1);
            
        }
        catch (Exception ex)
        {
        }
    }
    public void ItemFill(int pageIndex)
    {
        Item();
        Populate Populate = new Populate();
        CommonPopup1.CallingFunction = "ItemFill";
        string strName = ((TextBox)CommonPopup1.FindControl("txtName")).Text;
        string Refno = ((TextBox)CommonPopup1.FindControl("txtRefno")).Text;
        string searchCol = ((HiddenField)CommonPopup1.FindControl("hfColumn")).Value;
        Refno = Refno == string.Empty || Refno == null ? "0" : Refno;

        Populate.FillPopUpGrid((GridView)CommonPopup1.FindControl("gdvSearch"), "Items", "ID_Items as ID,ItemName as Name", "ID_Items", "ID_Items", "FK_Client=" + hfClient.Value.Split(',')[0] + " AND Combo=0  AND Cancelled=0 AND ItemName ", strName, null);
        Populate = null;

        if (txtSearchName.Text == string.Empty)
        {
            lblPopupHeader.Text = "Item";
            mpCommon.Show();
        }

    }
    public void SetDetails(string strName, string strRefno)
    {
        if (CommonPopup1.CallingFunction == "ItemFill")
        {
            txtSearchName.Text = strName;
            hfSelectedItem.Value = strRefno.ToString();
        }
        if (CommonPopup1.CallingFunction == "DoctorFill")
        {
            txtDoctor.Text = strName;
            hfDoctoritem.Value = strRefno.ToString();
        }
        if (CommonPopup1.CallingFunction == "CustomerFill")
        {
            txtCustomer.Text = strName;
            hfCustomeritem.Value = strRefno.ToString();
        }
    }
    
    private void Item()
    {
        txtSearchName.Text = "";

    }

    protected void imgDoctor_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            DoctorName();
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "toastr.warning('" + ex.Message + "')", true);
        }
    }
    protected void imgCustomer_Click(object sender, ImageClickEventArgs e)
    {
        CustomerName();
    }
    public void DoctorName()
    {
        try
        {
            ((TextBox)CommonPopup1.FindControl("txtName")).Text = txtDoctor.Text.Trim();
            ((TextBox)CommonPopup1.FindControl("txtRefno")).Text = txtDoctor.Text.Trim();
            ((HiddenField)CommonPopup1.FindControl("hfColumn")).Value = "Name";
            DoctorFill(1);

        }
        catch (Exception ex)
        {
        }
    }
    public void DoctorFill(int pageIndex)
    {
        DoctorClear();
        Populate Populate = new Populate();
        CommonPopup1.CallingFunction = "DoctorFill";
        string strName = ((TextBox)CommonPopup1.FindControl("txtName")).Text;
        string Refno = ((TextBox)CommonPopup1.FindControl("txtRefno")).Text;
        string searchCol = ((HiddenField)CommonPopup1.FindControl("hfColumn")).Value;
        Refno = Refno == string.Empty || Refno == null ? "0" : Refno;
        Populate.FillPopUpGrid((GridView)CommonPopup1.FindControl("gdvSearch"), "Doctor", "ID_Doctor as ID,DoctorName as Name", "ID_Doctor", "ID_Doctor", "FK_Client=" + hfClient.Value.Split(',')[0] + "   AND Cancelled=0 AND DoctorName ", strName, null);
        Populate = null;

        if (txtDoctor.Text == string.Empty)
        {
            lblPopupHeader.Text = "Doctor";
            mpCommon.Show();
        }

    }
    private void DoctorClear()
    {
        txtDoctor.Text = "";

    }
    public void CustomerName()
    {
        try
        {
            ((TextBox)CommonPopup1.FindControl("txtName")).Text = txtCustomer.Text.Trim();
            ((TextBox)CommonPopup1.FindControl("txtRefno")).Text = txtCustomer.Text.Trim();
            ((HiddenField)CommonPopup1.FindControl("hfColumn")).Value = "Name";
            CustomerFill(1);

        }
        catch (Exception ex)
        {
        }
    }
    public void CustomerFill(int pageIndex)
    {
        CustomerClear();
        Populate Populate = new Populate();
        CommonPopup1.CallingFunction = "CustomerFill";
        string strName = ((TextBox)CommonPopup1.FindControl("txtName")).Text;
        string Refno = ((TextBox)CommonPopup1.FindControl("txtRefno")).Text;
        string searchCol = ((HiddenField)CommonPopup1.FindControl("hfColumn")).Value;
        Refno = Refno == string.Empty || Refno == null ? "0" : Refno;
        Populate.FillPopUpGrid((GridView)CommonPopup1.FindControl("gdvSearch"), "Customer", "ID_Customer as ID,CustomerName as Name", "ID_Customer", "ID_Customer", "FK_Client=" + hfClient.Value.Split(',')[0] + " AND Cancelled=0 AND CustomerName ", strName, null);
        Populate = null;

        if (txtCustomer.Text == string.Empty)
        {
            lblPopupHeader.Text = "Customer";
            mpCommon.Show();
        }

    }
    private void CustomerClear()
    {
        txtCustomer.Text = "";

    }
    protected void btnSearchClear_Click(object sender, EventArgs e)
    {
       chkzeorstock.Checked = false;
       gvEnquiry.DataSource = null;
       gvEnquiry.DataBind();
    }
    protected void gvEnquiry_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvEnquiry.PageIndex = e.NewPageIndex;
        GridFill(e.NewPageIndex);
    }
}