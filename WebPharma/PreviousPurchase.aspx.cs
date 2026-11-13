using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.DataVisualization.Charting;
using System.Web.UI.WebControls;
using WebPharma.Business;
using WebPharma.Business.Masters;
using WebPharma.Business.WebPharma;

public partial class PreviousPurchase : System.Web.UI.Page
{
    protected void BindData()
    {
        Populate populate = new Populate();
        populate.FillDropdownList(ddlsearchUnit, "Units", "UnitName", "ID_Units", "UnitName", " FK_Client= " + hfClient.Value + "AND Cancelled=0", "");
        populate.FillDropdownList(ddlSearchManufacturer, "Manufacturer", "ManufactureName", "ID_Manufacturer", "ManufactureName", "FK_Client=" + hfClient.Value + "AND Cancelled=0", "");
        populate.FillDropdownList(ddlSerachCategory, "Category", "CategoryName", "ID_Category", "CategoryName", " FK_Client= " + hfClient.Value + "AND Cancelled=0", "");
        populate.FillDropdownList(ddlSerachSubUnit, "SubUnit", "SubUnitName", "ID_SubUnits", "SubUnitName", " FK_Client= " + hfClient.Value + "AND Cancelled=0", "");
        populate.FillDropdownList(ddlGeneric, "Generic", "GenericName", "ID_Generic", "GenericName", "FK_Client=" + hfClient.Value + "AND Cancelled=0", "");
        populate.FillDropdownList(ddlSearchSupplier, "Supplier", "SupplierName", "ID_Supplier", "SupplierName", " FK_Client= " + hfClient.Value + "AND Cancelled=0", "");
      //  populate.FillDropdownListDashBoard(ddlItemStore, "Store", "StoName", "ID_Store", "ID_Store", "Active='True' AND FK_Client= " + hfClient.Value.Split(',')[0], "");
        populate = null;
        //BinSubcategory();

    }
    //protected void BinSubcategory()
    //{
    //    new Populate().FillDropdownList(ddlSeachSubCategory, "SubCategory", "SubCategoryName", "ID_SubCategory", "SubCategoryName", "FK_Category = " + ddlSerachCategory.SelectedValue + " and FK_Client = " + hfClient.Value + "AND Cancelled=0", "");

    //}
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

            if (Request.QueryString["itn"] != null)
                txtSearchName.Text = Convert.ToString(security.UrlDecrypt(HttpUtility.UrlDecode(Request.QueryString["itn"]))); //Convert.ToString(Request.QueryString["itn"]);


          //  ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "ClearSearch();", true);
            BindData();
            SetReadonly();
            ddlSerachCategory.SelectedValue = Convert.ToString(Request.QueryString["ctgry"]);
            //int value = Convert.ToInt32(hdfValue.Value);
           // txtDate.Text = Convert.ToString(DateTime.Now);
            //txtDate.Text = Convert.ToDateTime(DateTime.Now).ToString();
            //txtTodate.Text = Convert.ToDateTime(DateTime.Now).ToString();
            txtDate.Text = Convert.ToString(txtDate.Text);
            txtTodate.Text = Convert.ToString(txtTodate.Text);
            // txtSearchShortName.Focus();
            
            //LoadPreviousPurchase();
            this.BindGrid();
            gvPriviousPurchase.Focus();
            GridFill(1);
            Search();
             ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "SearhGraph();", true);
          //  Data();

        }
    }
    public void SetReadonly()
    {
        txtTodate.Attributes.Add("readonly", "readonly");
        txtDate.Attributes.Add("readonly", "readonly");
    }
    private void ClearSearch()
    {
       
        Search();
    }
    protected void GridFill(int currentPage)
    {
        BlMaterialReciept blMater = new BlMaterialReciept();
        DataTable dtbl = new DataTable();
      //  blMater.ShortName = txtSearchShortName.Text.Trim();
        blMater.ItemName = txtSearchName.Text.Trim();
        blMater.FK_Category = Convert.ToInt64(ddlSerachCategory.SelectedValue);
        blMater.BatchNo = txtBatchNo.Text.Trim();
        blMater.FK_Manufacture = Convert.ToInt64(ddlSearchManufacturer.SelectedValue);
        blMater.FK_Units = Convert.ToInt64(ddlsearchUnit.SelectedValue);
        blMater.FK_Generic = Convert.ToInt64(ddlGeneric.SelectedValue);
        blMater.FK_SubUnits = Convert.ToInt64(ddlSerachSubUnit.SelectedValue);
        blMater.FK_Supplier = Convert.ToInt64(ddlSearchSupplier.SelectedValue);
        blMater.UserCode = hfUser.Value;
        blMater.PageIndex = Convert.ToInt16(currentPage);
        blMater.Period = Convert.ToInt16(ddlType.SelectedValue);
        blMater.FromDate = Convert.ToDateTime(txtDate.Text == "" ? "" + DateTime.Now.Date + "" : txtDate.Text);
        blMater.ToDate = Convert.ToDateTime(txtTodate.Text==""?""+DateTime.Now.Date+"":txtTodate.Text);
      //  ddlSearchSupplier.SelectedValue = Convert.ToString(Request.QueryString["Supplier"]);
      //  blMater.FK_Supplier = Convert.ToInt64(ddlSearchSupplier.SelectedValue);
        dtbl = blMater.SelectPreviousPurchaseDetails();
        gvPriviousPurchase.DataSource = dtbl;
        gvPriviousPurchase.DataBind();
       // gvPriviousPurchase.HeaderStyle.Font.Underline = false;
        ViewState["DataValue"] = dtbl;
        DataTable dtGraph = blMater.SelectPreviousPurchaseDetailsGraph();
        ViewState["Graph"] = dtGraph;
        blMater = null;
    }
    protected void LoadPreviousPurchase()
    {

        BlMaterialReciept blmaterialreciept = new BlMaterialReciept();
        blmaterialreciept.FK_Client = Convert.ToInt64(hfClient.Value);
        if (Convert.ToString(Request.QueryString["ID"]) !="")
        {
            Security security = new Security();
            blmaterialreciept.FK_ItemsforPreviousPurchase = Convert.ToInt64(security.UrlDecrypt(HttpUtility.UrlDecode(Request.QueryString["ID"])).ToString());
            DataTable dtbl = new DataTable();
            dtbl = blmaterialreciept.SelectPreviousPurchaseDetails();

            if (dtbl.Rows.Count > 0)
            {
                gvPriviousPurchase.DataSource = dtbl;
                gvPriviousPurchase.DataBind();
                ViewState["DataValue"] = dtbl;
                //ViewState["sortdr"] = "Asc";   
                //  DivPreviousPurchase.Visible = true;

            }
        }
        else
        {
           // ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "toastr.error('Please seelct an item.. ')", true);
        }


    }


 
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        GridFill(1);
       // Data();
        Search();
        ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "SearhGraph();", true);
        if (Convert.ToInt32(ddlType.SelectedValue)==6)
        {
            txtDate.Text = Convert.ToString(txtDate.Text);
            txtTodate.Text = Convert.ToString(txtTodate.Text);
        }
        
       // ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "CustomDateSerarch();", true);
    }
    protected void gvPriviousPurchase_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvPriviousPurchase.PageIndex = e.NewPageIndex;
         this.BindGrid();
        // GridFill(1);
      
         
    }
    private void BindGrid(string sortExpression = null)
    {
        DataTable dt = (DataTable)(ViewState["DataValue"]);
        if (sortExpression != null)
        {
            DataView dv = dt.AsDataView();
            this.SortDirection = this.SortDirection == "ASC" ? "DESC" : "ASC";

            dv.Sort = sortExpression + " " + this.SortDirection;
            gvPriviousPurchase.DataSource = dv;
        }
        else
        {
            gvPriviousPurchase.DataSource = dt;
        }
        gvPriviousPurchase.DataBind();
    }

    private string SortDirection
    {
        get { return ViewState["SortDirection"] != null ? ViewState["SortDirection"].ToString() : "ASC"; }
        set { ViewState["SortDirection"] = value; }
    }

    protected void gvPriviousPurchase_Sorting(object sender, GridViewSortEventArgs e)
    {
        this.BindGrid(e.SortExpression);
        //DataTable dtrslt = (DataTable)ViewState["dirState"];
        //if (dtrslt.Rows.Count > 0)
        //{
        //    if (Convert.ToString(ViewState["sortdr"]) == "Asc")
        //    {
        //        dtrslt.DefaultView.Sort = e.SortExpression + " Desc";
        //        ViewState["sortdr"] = "Desc";
        //    }
        //    else
        //    {
        //        dtrslt.DefaultView.Sort = e.SortExpression + " Asc";
        //        ViewState["sortdr"] = "Asc";
        //    }
        //    gvPriviousPurchase.DataSource = dtrslt;
        //    gvPriviousPurchase.DataBind();


        //}  


        ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "SearhGraphSot();", true);
 
    }

    
    //protected DataTable  Data()
    //{
    //   // Chart1.Visible = ddlCountries.SelectedValue != "";
    //   // string query = string.Format("select shipcity, count(orderid) from orders where shipcountry = '{0}' group by shipcity", ddlCountries.SelectedValue);
    //    DataTable dt = (DataTable)ViewState["DataValue"];
       
    //      //  Chart2.Visible = dt.Rows.Count > 0;
    //  //  string[] x = new string[dt.Rows.Count];
    //  //  int[] y = new int[dt.Rows.Count];
    //  //  for (int i = 0; i < dt.Rows.Count; i++)
    //  //  {
    //  //      x[i] = dt.Rows[i]["BatchNo"].ToString();
    //  //      y[i] = Convert.ToInt32(dt.Rows[i]["Stock"]);
           
    //  //  }
    //  //  Chart1.Series[0].Points.DataBindXY(x, y);
    //  //  Chart1.Series[0].ChartType = SeriesChartType.Pie;
    //  ////  Chart1.ChartAreas["ChartArea1"].Area3DStyle.Enable3D = true;
    //  //  Chart1.Legends[0].Enabled = true;
    //    //List<string> DataValue = (from p in dt.AsEnumerable()
    //    //                          select p.Field<string>("BatchNo")).Distinct().ToList();
    //    //foreach (string BatchNo in DataValue)
    //    //{

    //    //    //Get the Year for each Country.
    //    //    int[] x = (from p in dt.AsEnumerable()
    //    //               where p.Field<string>("SupplierName") == BatchNo
    //    //               orderby p.Field<int>("MRP") ascending
    //    //               select p.Field<int>("Stock")).ToArray();

    //    //    //Get the Total of Orders for each Country.
    //    //    int[] y = (from p in dt.AsEnumerable()
    //    //               where p.Field<string>("ItemName") == BatchNo
    //    //               orderby p.Field<int>("MRP") ascending
    //    //               select p.Field<int>("Stock")).ToArray();

    //    //    //Add Series to the Chart.
    //    //    Chart1.Series.Add(new Series(BatchNo));
    //    //    Chart1.Series[BatchNo].IsValueShownAsLabel = true;
    //    //    Chart1.Series[BatchNo].ChartType = SeriesChartType.Bar;
    //    //    Chart1.Series[BatchNo].Points.DataBindXY(x, y);
    //    //}
    //    //Chart1.Legends[0].Enabled = true;


    //        //foreach (DataRow r in dt.Rows)
    //        //{
    //        //    Series s = new Series((string)r["BatchNo"]);

    //        //    s.ChartType = SeriesChartType.Column;
    //        //    s.Points.AddXY("Stock", new object[] { r["Stock"] });
    //        //    s.Points.AddXY("Purchaserate", new object[] { r["PurchaseRate"] });
    //        //    s.Points.AddXY("MRP", new object[] { r["MRP"] });
    //        //    s.Points.AddXY("SalesPrice", new object[] { r["SalesPrice"] });
    //        //    s.Points.AddXY("DiscountPer", new object[] { r["DiscountPer"] });
    //        //    s.Points.AddXY("DiscountAmount", new object[] { r["DiscountAmount"] });

    //        //    Chart1.Series.Add(s);

    //        //}


    //       // Chart1.Series[0].ChartType = SeriesChartType.Pie;
    //        //Chart1.ChartAreas["ChartArea1"].Area3DStyle.Enable3D = true;
    //       // Chart1.Legends[0].Enabled = true;

    //        //Chart2.DataSource = dt;
    //        //Chart2.Series[0].ChartType = SeriesChartType.Column;
    //        //Chart2.Legends[0].Enabled = true;
    //        //Chart2.Series[0].XValueMember = dt.Columns["SupplierName"].ToString();
    //        //Chart2.Series[0].YValueMembers = "MRP";
            
    //        //Chart2.DataBind();
    //        //Chart2.Legends[0].Enabled = true;

    //        string[] x = new string[dt.Rows.Count];
    //        decimal[] y = new decimal[dt.Rows.Count];
    //        for (int i = 0; i < dt.Rows.Count; i++)
    //        {
    //            //x[i] = dt.Columns["SupplierName"].ToString();
    //            //y[i] = Convert.ToInt32(dt.Rows[i][1]);
    //            x[i] = dt.Rows[i]["SupplierName"].ToString();
    //            y[i] = Convert.ToInt32(dt.Rows[i][1]);
    //        }
    //        BarChart1.Series.Add(new AjaxControlToolkit.BarChartSeries { Data = y });
    //        BarChart1.CategoriesAxis = string.Join(",", x);
    //        BarChart1.ChartTitle = "";
            
    //        if (x.Length > 3)
    //        {
    //            BarChart1.ChartWidth = (x.Length * 100).ToString();
    //        }
    //        BarChart1.Visible = dt.Rows.Count > 0;



    //        //List<string> DataValue = (from p in dt.AsEnumerable()
    //        //                          select p.Field<string>("BatchNo")).Distinct().ToList();
    //        //foreach (string BatchNo in DataValue)
    //        //{

    //        //    //Get the Year for each Country.
    //        //    int[] x = (from p in dt.AsEnumerable()
    //        //               where p.Field<string>("BatchNo") == BatchNo
    //        //               orderby p.Field<decimal>("MRP") ascending
    //        //               select p.Field<int>("BillNo")).ToArray();

    //        //    //Get the Total of Orders for each Country.
    //        //    int[] y = (from p in dt.AsEnumerable()
    //        //               where p.Field<string>("BatchNo") == BatchNo
    //        //               orderby p.Field<decimal>("MRP") ascending
    //        //               select p.Field<int>("BillNo")).ToArray();

    //        //    //Add Series to the Chart.
    //        //    Chart2.Series.Add(new Series(BatchNo));
    //        //    Chart2.Series[BatchNo].IsValueShownAsLabel = true;
    //        //    Chart2.Series[BatchNo].ChartType = SeriesChartType.Bar;
    //        //    Chart2.Series[BatchNo].Points.DataBindXY(x, y);
    //        //}
    //        //Chart2.Legends[0].Enabled = true;




    //        return dt;
       

    //}



    protected void btnHidden_Click(object sender, EventArgs e)
    {
        column4.Visible = true;
        Search();
       //GetMinimuStock();
    }
    public string GetPurchaseDetails()
    {
        DataTable dst = (DataTable)ViewState["DataValue"];
        
        DataTable dtbl = (DataTable)ViewState["Graph"];
        gvPriviousPurchase.DataSource = dst;
        gvPriviousPurchase.DataBind();
        int i = 1;
        string mst = "";
        string str = "[";
        foreach (DataRow dtRow in dtbl.Rows)
        {
            if (i < dtbl.Rows.Count)
            {
                str += "\n {Batch:'" + dtRow[0].ToString() + "',Profit:'" + dtRow[6].ToString() + "',y:" + dtRow[1].ToString() + ",name:'" + dtRow[12].ToString() + "',MRP:'" + dtRow["MRP"].ToString() + "',SalesPrice:'" + dtRow["SalesPrice"].ToString() + "',ProfitAmnt:'" + dtRow[15].ToString() + "'},\n";
            }
            else
            {
                str += "\n {Batch:'" + dtRow[0].ToString() + "',Profit:'" + dtRow[6].ToString() + "',y:" + dtRow[1].ToString() + ",name:'" + dtRow[12].ToString() + "',MRP:'" + dtRow["MRP"].ToString() + "',SalesPrice:'" + dtRow["SalesPrice"].ToString() + "',ProfitAmnt:'" + dtRow[15].ToString() + "'}";
            }
            i++;
        }
        str += "]";


        return str;
    }
  
    private void Search()
    {

        column4.Visible = true;
    hdfMinStock.Value = GetPurchaseDetails();      

        }



    protected void ddlType_SelectedIndexChanged(object sender, EventArgs e)
    {
        GridFill(1);
        // Data();
        Search();
    }
    protected void btnSearchClear_Click(object sender, EventArgs e)
    {
        gvPriviousPurchase.DataSource = null;
        gvPriviousPurchase.DataBind();
        hdfMinStock.Value = "0";
    }
}