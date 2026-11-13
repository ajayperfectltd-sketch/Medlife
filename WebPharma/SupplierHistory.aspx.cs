using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebPharma.Business.WebPharma;

public partial class SupplierHistory : System.Web.UI.Page
{
    protected void BindData()
    {
        Populate populate = new Populate();
        populate.FillDropdownList(ddlSupplier, "Supplier", "SupplierName", "ID_Supplier", "SupplierName", "Active='True' AND Cancelled=0 AND FK_Client= " + hfClient.Value.Split(',')[0], "");
    }
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
                hfClient.Value = Request.QueryString["clnt"];
            else if (Request.QueryString["clntPop"] != null)
                hfClient.Value = security.UrlDecrypt(HttpUtility.UrlDecode(Request.QueryString["clntPop"]));

            BindData();
            SetReadonly();

            if (Request.QueryString["Supplier"] != null)
                ddlSupplier.SelectedValue = Convert.ToString(security.UrlDecrypt(HttpUtility.UrlDecode(Request.QueryString["Supplier"])));// Convert.ToString(Request.QueryString["Supplier"]);

            ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "Datetime();", true);
            txtDate.Text = Convert.ToString(txtDate.Text);
           txtTodate.Text = Convert.ToString(txtTodate.Text);
           // txtDate_CalendarExtender.StartDate = DateTime.Now;
            //txtToDate_CalendarExtender.StartDate = DateTime.Now;
            GridFill(1);
        }
    }
    public void SetReadonly()
    {
        txtDate.Attributes.Add("readonly", "readonly");
        txtTodate.Attributes.Add("readonly", "readonly");
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
        BlQuotationComparison Blquotation = new BlQuotationComparison();
        DataTable dtbl = new DataTable();
        Blquotation.Fk_Supplier = Convert.ToInt32(ddlSupplier.SelectedValue);
        Blquotation.FromDate = Convert.ToDateTime(txtDate.Text == "" ? "" + DateTime.Now.Date + "" : txtDate.Text);
        Blquotation.ToDate = Convert.ToDateTime(txtTodate.Text==""?""+DateTime.Now.Date+"":txtTodate.Text);
        dtbl = Blquotation.SelectSupplierHistoryData();
        gvSupplier.DataSource = dtbl;
        gvSupplier.DataBind();                                                                                                           
        ViewState["DataValue"] = dtbl;

    }
   
    
    protected void IbtnItemAdd_Click(object sender, ImageClickEventArgs e)
    {

    }
    protected void gvSupplier_Sorting(object sender, GridViewSortEventArgs e)
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
            gvSupplier.DataSource = dv;
        }
        else
        {
            gvSupplier.DataSource = dt;
        }
        gvSupplier.DataBind();
    }
    private string SortDirection
    {
        get { return ViewState["SortDirection"] != null ? ViewState["SortDirection"].ToString() : "ASC"; }
        set { ViewState["SortDirection"] = value; }
    }
    protected void gvSupplier_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvSupplier.PageIndex = e.NewPageIndex;
        GridFill(e.NewPageIndex);
    }
    protected void btnSearchClear_Click(object sender, EventArgs e)
    {
        gvSupplier.DataSource = null;
        gvSupplier.DataBind();
    }
}