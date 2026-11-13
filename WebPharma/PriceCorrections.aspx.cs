using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebPharma.Business.Masters;
using WebPharma.Business.WebPharma;

public partial class PriceCorrections : System.Web.UI.Page
{
    protected void CheckUserAccess()
    {
        BlUserAccess bluseraccess = new BlUserAccess();
        Populate populate = new Populate();

        bluseraccess.FK_Users = Convert.ToInt64(hfUser.Value);
        bluseraccess.FK_Pages = 43;
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
         populate.FillDropdownList(ddlItem, "Items", "ItemName", "ID_Items", "ItemName", "Cancelled=0 AND FK_Client= " + hfClient.Value.Split(',')[0], "");
        populate.FillDropdownList(ddlStore, "Store", "StoName", "ID_Store", "StoName", " Active='True'  AND Cancelled=0 AND FK_Client= " + hfClient.Value.Split(',')[0], "");
        populate.FillDropdownList(ddlCategory, "Category", "CategoryName", "ID_Category", "CategoryName", "Cancelled=0 and  FK_Client= " + hfClient.Value.Split(',')[0], "");
    }
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
                //divView.Style.Add("display", "none;");
                CheckUserAccess();
                BindData();
                hfuserName.Value = Session["Username"].ToString();
                ddlStore.SelectedValue = Session["mainstore"].ToString();
                ddlStore.Enabled = false;
                txtDate.Focus();
            }
        }
        catch (Exception ex)
        {
            Populate.WarningMsg(this, ex.ToString());
        }
    }
    protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
    {

    }
    protected void imgbtnView_Click(object sender, ImageClickEventArgs e)
    {

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
        //if (ddlItem.SelectedIndex > 0)

        //{
        BlStock blStock = new BlStock();
        blStock.MasterId = Convert.ToInt64(hdfFKStock.Value);
        blStock.UserCode = hfUser.Value;
        blStock.FK_Client = Convert.ToInt64(hfClient.Value);
        blStock.SelectData();
        txtQuantity.Text = blStock.CurrentStock.ToString();
        txtMRP.Text = Convert.ToString(blStock.MRP);
        txtSalesPrice.Text = Convert.ToString(blStock.SalePrice);
        blStock = null;

        //}
    }
   
    protected void btnAddItems_Click(object sender, EventArgs e)
    {
        int a = 0;
        if (hfItem.Value.Trim() == txtitem.Text.ToString().Trim())
        {
            AddDataItem();

            ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "ItemClear();", true);
        }
        else
        {
            ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "toastr.warning('Item  does not exist')", true);
        }

    }
    private DataTable GetDataTable(DataTable dtbl)
    {
        dtbl.Columns.Add("slno");
        dtbl.Columns.Add("Item");
        dtbl.Columns.Add("FK_Items");
        dtbl.Columns.Add("Quantity");
        dtbl.Columns.Add("MRP");
        dtbl.Columns.Add("SalesPrice");
        dtbl.Columns.Add("NewSalesPrice");
        dtbl.Columns.Add("FK_Stock");
        return dtbl;
    }
    //protected int AddDataItem()
    
    //{
    //    DataTable dtbl = new DataTable();
    //    int inRowCount = 0;
    //    int inCheck = 0;
    //    if (ViewState["GridData"] != null)
    //    {
    //        dtbl = (DataTable)ViewState["GridData"];
    //        inRowCount = dtbl.Rows.Count;
    //    }
    //    else
    //    {

    //        dtbl = GetDataTable(dtbl);
    //    }
    //    if (dtbl.Rows.Count > 0)
    //    {

    //        for (int i = 0; i < dtbl.Rows.Count; i++)
    //        {
    //            if (dtbl.Rows[i]["FK_Stock"].ToString() == hdfFKStock.Value)
    //            {
    //                inCheck = inCheck + 1;
    //                break;
    //            }

    //        }

    //    }
    //    if (inCheck == 0)
    //    {

    //        decimal MRP = Math.Round(Convert.ToDecimal(txtMRP.Text == "" ? "0" : txtMRP.Text), 4);
    //        decimal SalesPrice = Math.Round(Convert.ToDecimal(txtSalesPrice.Text == "" ? "0" : txtSalesPrice.Text), 4);
    //        decimal NewSalesPrice = Math.Round(Convert.ToDecimal(txtNewSalePrice.Text == "" ? "0" : txtNewSalePrice.Text), 4);

    //        decimal decqty = Math.Round(Convert.ToDecimal(txtQuantity.Text == "" ? "0" : txtQuantity.Text), 4);
    //        // decimal Total = (decqty * PurchaseRate) + ((PurVAT / 100) * (decqty * PurchaseRate)) + ((CESS / 100) * (decqty * PurchaseRate));

    //        DataRow drow = dtbl.NewRow();
    //        drow["slno"] = inRowCount + 1;
    //        drow["Item"] = hfItem.Value;
    //        drow["FK_Items"] = hdfFkItem.Value;
    //        drow["Quantity"] = decqty;
    //        drow["NewSalesPrice"] = NewSalesPrice;
    //        drow["MRP"] = MRP;
    //        drow["SalesPrice"] = SalesPrice;
    //        drow["FK_Stock"] = hdfFKStock.Value;
    //        dtbl.Rows.InsertAt(drow, inRowCount);
    //        gvItems.DataSource = dtbl;
    //        gvItems.DataBind();
    //        // TotalAmount(dtbl);
    //        ViewState["GridData"] = dtbl;


    //    }
    //    else
    //    {
    //        ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "alertify.alert('Item already exists')", true);
    //        return 1;
    //    }
    //    return 0;

    //}
    //protected void AddData()
    //{


    //    DataTable dtbl = new DataTable();
    //    int rowCount = 0;
    //    int inCheck = 0;
    //    if (ViewState["GridData"] != null)
    //    {
    //        dtbl = (DataTable)ViewState["GridData"];
    //        rowCount = dtbl.Rows.Count;
    //    }
    //    else
    //    {
    //        dtbl.Columns.Add("slno");
    //        dtbl.Columns.Add("Item");
    //        dtbl.Columns.Add("FK_Items");
    //        dtbl.Columns.Add("FK_Category");
    //        dtbl.Columns.Add("Category");
    //        dtbl.Columns.Add("Quantity");
    //        dtbl.Columns.Add("MRP");
    //        dtbl.Columns.Add("SalesPrice");
    //        dtbl.Columns.Add("NewSalesPrice");
    //        dtbl.Columns.Add("FK_Stock");


    //    }
    //    if (hdfItem.Value != "")
    //    {

    //        gvItems.Enabled = true;
    //        dtbl.Rows.RemoveAt(Convert.ToInt32(hdfItem.Value));
    //        gvItems.DataSource = dtbl;
    //        gvItems.DataBind();
    //        hfSlNo.Value = hfItemId.Value;
    //        hdfItem.Value = "";
    //        // ViewState["TempTable"] = dtbl;


    //    }



    //    if (dtbl.Rows.Count > 0)
    //    {

    //        for (int i = 0; i < dtbl.Rows.Count; i++)
    //        {
    //            if (dtbl.Rows[i]["FK_Stock"].ToString() == hdfFKStock.Value)
    //            {
    //                inCheck = inCheck + 1;
    //                break;
    //            }
    //        }
    //    }




    //    if (inCheck == 0)
    //    {
    //        DataRow drow = dtbl.NewRow();
    //        if (hfSlNo.Value == "")
    //        {
    //            if (dtbl.Rows.Count > 0)
    //            {

    //                Int64 max = Convert.ToInt64(dtbl.Rows[dtbl.Rows.Count - 1]["SLNO"].ToString());
    //                drow["SLNO"] = Convert.ToInt64(max) + 1;
    //            }
    //            else
    //            {
    //                drow["SLNO"] = 1;
    //            }
    //        }
    //        else
    //        {
    //            drow["SLNO"] = Convert.ToInt64(hfSlNo.Value) + 1;

    //        }

    //        drow["Item"] = ddlItem.SelectedItem.ToString();
    //        drow["FK_Items"] = ddlItem.SelectedValue.ToString();

    //        drow["FK_Category"] = ddlCategory.SelectedValue.ToString();
    //        drow["Category"] = ddlCategory.SelectedItem.ToString();
    //        decimal MRP = Math.Round(Convert.ToDecimal(txtMRP.Text == "" ? "0" : txtMRP.Text), 4);
    //        decimal SalesPrice = Math.Round(Convert.ToDecimal(txtSalesPrice.Text == "" ? "0" : txtSalesPrice.Text), 4);
    //        decimal NewSalesPrice = Math.Round(Convert.ToDecimal(txtNewSalePrice.Text == "" ? "0" : txtNewSalePrice.Text), 4);

    //        decimal decqty = Math.Round(Convert.ToDecimal(txtQuantity.Text == "" ? "0" : txtQuantity.Text), 4);
    //        drow["Quantity"] = decqty;
    //        drow["NewSalesPrice"] = NewSalesPrice;
    //        drow["MRP"] = MRP;
    //        drow["SalesPrice"] = SalesPrice;
    //        drow["FK_Stock"] = hdfFKStock.Value;


    //        dtbl.Rows.InsertAt(drow, rowCount);
    //        dtbl.DefaultView.Sort = "SLNO";
    //        dtbl = dtbl.DefaultView.ToTable();
    //        gvItems.DataSource = dtbl;
    //        gvItems.DataBind();
    //        ViewState["GridData"] = dtbl;
    //        hfSlNo.Value = "";
           
    //        txtitem.Focus();
    //        ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "ItemClear();", true);
    //    }
    //    else
    //    {
    //        ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "alertify.alert('Item already exists with same batch no')", true);
    //    }

    //}
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
        if (dtbl.Rows.Count > 0)
        {

            for (int i = 0; i < dtbl.Rows.Count; i++)
            {
                if (dtbl.Rows[i]["FK_Stock"].ToString() == hdfFKStock.Value)
                {
                    inCheck = inCheck + 1;
                    break;
                }
                
            }

        }
        if (inCheck == 0)
        {
           
            decimal MRP = 0;
            decimal SalesPrice = 0;
           
            long FK_Unit = 0;
            int NoOfUnit = 0;

            BlStock blStock = new BlStock();
            blStock.MasterId = Convert.ToInt64(hdfFKStock.Value);
            blStock.UserCode = hfUser.Value;
            blStock.FK_Client = Convert.ToInt64(hfClient.Value);
            blStock.SelectData();
            
            FK_Unit = blStock.FK_Units;
           
            NoOfUnit = blStock.NoOfUnits;
            DataRow drow = dtbl.NewRow();
            drow["Item"] = ddlItem.SelectedItem.ToString();
            drow["FK_Items"] = ddlItem.SelectedValue.ToString();

            //drow["FK_Category"] = ddlCategory.SelectedValue.ToString();
            //drow["Category"] = ddlCategory.SelectedItem.ToString();
            MRP = Math.Round(Convert.ToDecimal(txtMRP.Text == "" ? "0" : txtMRP.Text), 4);
             SalesPrice = Math.Round(Convert.ToDecimal(txtSalesPrice.Text == "" ? "0" : txtSalesPrice.Text), 4);
            decimal NewSalesPrice = Math.Round(Convert.ToDecimal(txtNewSalePrice.Text == "" ? "0" : txtNewSalePrice.Text), 4);

            decimal decqty = Math.Round(Convert.ToDecimal(txtQuantity.Text == "" ? "0" : txtQuantity.Text), 4);
            drow["Quantity"] = decqty;
            drow["NewSalesPrice"] = NewSalesPrice;
            drow["MRP"] = MRP;
            drow["SalesPrice"] = SalesPrice;
            drow["FK_Stock"] = hdfFKStock.Value;
            dtbl.Rows.InsertAt(drow, inRowCount);
            GvPriceData.DataSource = dtbl;
            GvPriceData.DataBind();
            ViewState["GridData"] = dtbl;
            //FillReasonddl(false);


        }
        else
        {
            ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "alertify.alert('Item already exists')", true);
            return 1;
        }
        return 0;

    }
    protected void GvPriceData_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {

    }
    protected void GvPriceData_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {

    }
}