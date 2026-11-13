using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebPharma.Business.WebPharma;

public partial class QuotationComparison : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {

            Security security = new Security();
            if (Request.QueryString["uid"] != null)
                hfUser.Value = Request.QueryString["uid"];
            else if (Request.QueryString["uidPop"] != null)
                hfUser.Value = security.UrlDecrypt(HttpUtility.UrlDecode(Request.QueryString["uidPop"]));
            
            if (Request.QueryString["clnt"] != null)
               hfClient.Value =  Request.QueryString["clnt"];
            else if (Request.QueryString["clntPop"] != null)
                hfClient.Value = security.UrlDecrypt(HttpUtility.UrlDecode(Request.QueryString["clntPop"]));

            ddlType.Visible = true;
            lblType.Visible = true;
            lblName.Text = "Item";
            this.BindGrid();

            if (Request.QueryString["itn"] != null)
                txtSearchName.Text = Convert.ToString(security.UrlDecrypt(HttpUtility.UrlDecode(Request.QueryString["itn"])));


            if (Convert.ToString(Request.QueryString["itn"]) != null)
            {
                ddlCategory.Enabled = false;
                ddlType.Enabled = false;
            }
            else
            {
                ddlCategory.Enabled = true;
                ddlType.Enabled = true;
            }
            GridFill(1);
        }
    }
    
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        try
        {
            gvQuotationComparison.Columns[1].Visible = true;
            gvQuotationComparison.Columns[2].Visible = true;
            GridFill(1);
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "toastr.warning('" + ex.Message + "')", true);
        }

    }
    protected void GridFill(int currentPage)
    
    {
        Security security = new Security();
        BlQuotationComparison blQtion = new BlQuotationComparison();
        DataTable dtbl = new DataTable();
       // blQtion.Category = Convert.ToInt16(ddlCategory.SelectedValue);
        blQtion.Category = Convert.ToInt16(ddlCategory.SelectedValue);
        if (Convert.ToString(Request.QueryString["itn"]) != null)
        {
            if (Request.QueryString["itn"] != null)
                blQtion.CategoryName = Convert.ToString(security.UrlDecrypt(HttpUtility.UrlDecode(Request.QueryString["itn"]))); //Convert.ToString(Request.QueryString["itn"]);
            if (Request.QueryString["ID"] != null)
                hfSelectedItem.Value = Convert.ToString(security.UrlDecrypt(HttpUtility.UrlDecode(Request.QueryString["ID"]))); //Convert.ToString(Request.QueryString["ID"]);
            if (Request.QueryString["ctgry"] != null)
                ddlType.SelectedValue = Convert.ToString(security.UrlDecrypt(HttpUtility.UrlDecode(Request.QueryString["ctgry"]))); //Convert.ToString(Request.QueryString["ctgry"]);

        }

        blQtion.CategoryName = txtSearchName.Text.Trim();
        blQtion.Type = Convert.ToInt16(ddlType.SelectedValue);
        blQtion.FreeQty = Convert.ToInt64(txtFreeqty.Text==""?"0":txtFreeqty.Text);
        blQtion.PurchaseRate = Convert.ToDecimal(txtPurchaseRate.Text==""?"0":txtPurchaseRate.Text);
        blQtion.DiscountAmount = Convert.ToDecimal(txtDiscountAmount.Text==""?"0":txtDiscountAmount.Text);
        blQtion.UserCode = hfUser.Value;
        blQtion.FK_Items = Convert.ToInt64(hfSelectedItem.Value == "" ? "0" : hfSelectedItem.Value);
        blQtion.Fk_Customer = Convert.ToInt64(hfSelectedCustomer.Value==""?"0":hfSelectedCustomer.Value);
        blQtion.Fk_Supplier = Convert.ToInt64(hfSelectedSupplier.Value == "" ? "0" : hfSelectedSupplier.Value);
        dtbl = blQtion.SelectItemsData();
        gvQuotationComparison.DataSource = dtbl;
        gvQuotationComparison.DataBind();
        if (Convert.ToInt16(ddlType.SelectedValue) == 1)
        {
            gvQuotationComparison.Columns[2].Visible = false;
        }
        if (Convert.ToInt16(ddlType.SelectedValue) == 2)
        {
            gvQuotationComparison.Columns[1].Visible = false;
        }
        if (Convert.ToInt16(ddlType.SelectedValue) == 3)
        {
            gvQuotationComparison.Columns[1].Visible = true;
            gvQuotationComparison.Columns[2].Visible = true;
        }

        ViewState["DataValue"] = dtbl;
        blQtion = null;
    }

    
   
  
    protected void IbtnItemAdd_Click(object sender, ImageClickEventArgs e)
    {
        if (Convert.ToInt32(ddlCategory.SelectedValue) == 1)
            SupplierName();

        else if (Convert.ToInt32(ddlCategory.SelectedValue) == 2)
            CustomerName();
        else if (Convert.ToInt32(ddlCategory.SelectedValue) == 3)
            ItemName();
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
    public void SetDetails(string strName, string strRefno)
    {
        if (CommonPopup1.CallingFunction == "ItemFill")
        {
            txtSearchName.Text = strName;
            hfSelectedItem.Value = strRefno.ToString();
        }
        if (CommonPopup1.CallingFunction == "SupplierFill")
        {
            txtSearchName.Text = strName;
            hfSelectedSupplier.Value = strRefno.ToString();
        }
        if (CommonPopup1.CallingFunction == "CustomerFill")
        {
            txtSearchName.Text = strName;
            hfSelectedCustomer.Value = strRefno.ToString();
        }
    }
    private void Item()
    {
        txtSearchName.Text = "";

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
    public void SupplierName()
    {
        try
        {
            ((TextBox)CommonPopup1.FindControl("txtName")).Text = txtSearchName.Text.Trim();
            ((TextBox)CommonPopup1.FindControl("txtRefno")).Text = txtSearchName.Text.Trim();
            ((HiddenField)CommonPopup1.FindControl("hfColumn")).Value = "Name";
            SupplierFill(1);

        }
        catch (Exception ex)
        {
        }
    }
    public void SupplierFill(int pageIndex)
    {
        Item();
        Populate Populate = new Populate();
        CommonPopup1.CallingFunction = "SupplierFill";
        string strName = ((TextBox)CommonPopup1.FindControl("txtName")).Text;
        string Refno = ((TextBox)CommonPopup1.FindControl("txtRefno")).Text;
        string searchCol = ((HiddenField)CommonPopup1.FindControl("hfColumn")).Value;
        Refno = Refno == string.Empty || Refno == null ? "0" : Refno;

        Populate.FillPopUpGrid((GridView)CommonPopup1.FindControl("gdvSearch"), "Supplier", "ID_Supplier as ID,SupplierName as Name", "ID_Supplier", "ID_Supplier", "FK_Client=" + hfClient.Value.Split(',')[0] + "AND Cancelled=0 AND SupplierName ", strName, null);
        Populate = null;

        if (txtSearchName.Text == string.Empty)
        {
            lblPopupHeader.Text = "Supplier";
            mpCommon.Show();
        }

    }
    public void CustomerName()
    {
        try
        {
            ((TextBox)CommonPopup1.FindControl("txtName")).Text = txtSearchName.Text.Trim();
            ((TextBox)CommonPopup1.FindControl("txtRefno")).Text = txtSearchName.Text.Trim();
            ((HiddenField)CommonPopup1.FindControl("hfColumn")).Value = "Name";
            CustomerFill(1);

        }
        catch (Exception ex)
        {
        }
    }
    public void CustomerFill(int pageIndex)
    {
        Item();
        Populate Populate = new Populate();
        CommonPopup1.CallingFunction = "CustomerFill";
        string strName = ((TextBox)CommonPopup1.FindControl("txtName")).Text;
        string Refno = ((TextBox)CommonPopup1.FindControl("txtRefno")).Text;
        string searchCol = ((HiddenField)CommonPopup1.FindControl("hfColumn")).Value;
        Refno = Refno == string.Empty || Refno == null ? "0" : Refno;

        Populate.FillPopUpGrid((GridView)CommonPopup1.FindControl("gdvSearch"), "Customer", "ID_Customer as ID,CustomerName as Name", "ID_Customer", "ID_Customer", "FK_Client=" + hfClient.Value.Split(',')[0] + "AND Cancelled=0 AND CustomerName ", strName, null);
        Populate = null;

        if (txtSearchName.Text == string.Empty)
        {
            lblPopupHeader.Text = "Customer";
            mpCommon.Show();
        }

    }
    protected void btnQuotation_Click(object sender, EventArgs e)
    {
        hfSelectedSupplier.Value = "";
        hfSelectedCustomer.Value = "";
        hfSelectedItem.Value = "";

        if (Convert.ToInt32(ddlCategory.SelectedValue) == 1)
        {
            lblName.Text = "Supplier";
            ddlType.Visible = false;
            lblType.Visible = false;
            //gvQuotationComparison.Columns[2].Visible = false;
            //gvQuotationComparison.Columns[1].Visible = true;
            txtSearchName.Text = "";
          
            gvQuotationComparison.DataSource = null;
            gvQuotationComparison.DataBind();
            txtSearchName.Focus();
        }
        else if (Convert.ToInt32(ddlCategory.SelectedValue) == 2)
        {
            lblName.Text = "Customer";
            ddlType.Visible = false;
            lblType.Visible = false;
            //gvQuotationComparison.Columns[1].Visible = false;
            //gvQuotationComparison.Columns[2].Visible = true;
            txtSearchName.Text = "";
          
            gvQuotationComparison.DataSource = null;
            gvQuotationComparison.DataBind();
            txtSearchName.Focus();
        }
        else if (Convert.ToInt32(ddlCategory.SelectedValue) == 3)
        {
            ddlType.Visible = true;
            lblType.Visible = true;
            lblName.Text = "Item";
            //gvQuotationComparison.Columns[1].Visible = true;
            //gvQuotationComparison.Columns[2].Visible = true;
           
            txtSearchName.Text = "";
            gvQuotationComparison.DataSource = null;
            gvQuotationComparison.DataBind();
            txtSearchName.Focus();
        }
    }
    protected void gvQuotationComparison_Sorting(object sender, GridViewSortEventArgs e)
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
            gvQuotationComparison.DataSource = dv;
        }
        else
        {
            gvQuotationComparison.DataSource = dt;
        }
        gvQuotationComparison.DataBind();
    }
    private string SortDirection
    {
        get { return ViewState["SortDirection"] != null ? ViewState["SortDirection"].ToString() : "ASC"; }
        set { ViewState["SortDirection"] = value; }
    }
    protected void gvQuotationComparison_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvQuotationComparison.PageIndex = e.NewPageIndex;
        GridFill(e.NewPageIndex);
    }
    protected void OnRowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {


            //if ((DataBinder.Eval(e.Row.DataItem, "DiscountPer").ToString()) != "")//check zero
            //{
            //    e.Row.ToolTip = (e.Row.DataItem as DataRowView)["DiscountPer"].ToString() + "Free For"
            //        + (e.Row.DataItem as DataRowView)["FreeQuantity"].ToString();
                
            //}
            //for (int i = 0; i < e.Row.Cells.Count; i++)
            //{
            //    //set the tooltip text to be the header text
            //    e.Row.Cells[7].ToolTip =(e.Row.DataItem as DataRowView)["DiscountPer"].ToString() + "Free For"
            //        + (e.Row.DataItem as DataRowView)["FreeQuantity"].ToString();
                
            //}
        }
    }
    protected void btnSearchClear_Click(object sender, EventArgs e)
    {
        gvQuotationComparison.DataSource = null;
        gvQuotationComparison.DataBind();
    }
}