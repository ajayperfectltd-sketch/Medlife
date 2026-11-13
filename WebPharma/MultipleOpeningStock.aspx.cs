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

public partial class MultipleOpeningStock : System.Web.UI.Page
{



    protected void BindData()
    {
        Populate populate = new Populate();


        populate.FillDropdownList(ddlCategory, "Category", "CategoryName", "ID_Category", "CategoryName", "Cancelled=0 and  FK_Client= " + hfClient.Value.Split(',')[0], "");
        populate.FillDropdownList(ddlRack, "Rack", "RackName", "ID_Rack", "RackName", "FK_Client = " + hfClient.Value + "AND Cancelled=0", "");
        populate.FillDropdownList(ddlBin, "Bin", "BinName", "ID_Bin", "BinName", " FK_Client=" + hfClient.Value + "AND Cancelled=0", "");
        populate.FillDropdownList(ddlGeneric, "Generic", "GenericName", "ID_Generic", "GenericName", " FK_Client=" + hfClient.Value + "AND Cancelled=0", "");
        populate.FillDropdownList(ddlManufacturer, "Manufacturer", "ManufactureName", "ID_Manufacturer", "ManufactureName", "FK_Client=" + hfClient.Value + "AND Cancelled=0", "");
        populate.FillDropdownList(ddlSubcategory, "SubCategory", "SubCategoryName", "ID_SubCategory", "SubCategoryName", " FK_Client= " + hfClient.Value + "AND Cancelled=0", "");
        populate = null;

    }
    protected void CheckUserAccess()
    {
        BlUserAccess bluseraccess = new BlUserAccess();
        Populate populate = new Populate();

        bluseraccess.FK_Users = Convert.ToInt64(hfUser.Value);
        bluseraccess.FK_Pages = 68;
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
                

                hfUser.Value = Request.QueryString["uid"];
                hfClient.Value = Request.QueryString["clnt"];


                hfStore.Value = Session["mainstore"].ToString();

                CheckUserAccess();
                txtItemShortName.Enabled = true;
               
                BindData();
               




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
            Response.Redirect("~/Default.aspx");
            //ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "toastr.warning('" + ex.Message + "')", true);
        }
    }


    protected void imgbtnAdd_Click(object sender, ImageClickEventArgs e)
    {
           divAdd.Style.Add("display", "block");

        
        gvOpening.DataSource = null;
        gvOpening.DataBind();
        txtItemShortName.Enabled = true;



    }

    protected void GridFillOpening()
    {
        BlOpeningStock blopening = new BlOpeningStock();
        blopening.FK_Store = Convert.ToInt64(1);
        blopening.FK_Client = Convert.ToInt64(hfClient.Value);
        blopening.FK_Category = Convert.ToInt64(ddlCategory.SelectedValue);
        blopening.FK_SubCategory = Convert.ToInt64(ddlSubcategory.SelectedValue);
        blopening.FK_Manufacturer = Convert.ToInt64(ddlManufacturer.SelectedValue);
        blopening.FK_Generic = Convert.ToInt64(ddlGeneric.SelectedValue);
        blopening.FK_Rack = Convert.ToInt64(ddlRack.SelectedValue);
        blopening.FK_Bin = Convert.ToInt64(ddlBin.SelectedValue);
        blopening.ItemName = txtItemShortName.Text.ToString().Trim();
        blopening.BatchNo = txtBatch.Text.ToString().Trim();
        DataTable dtbl = new DataTable();
        dtbl = blopening.SelectMultipleOpeningStock();
        gvOpening.DataSource = dtbl;
        gvOpening.DataBind();
        blopening = null;
    }
    protected void ddlCategory_SelectedIndexChanged(object sender, EventArgs e)
    {
        Populate populate = new Populate();
        populate.FillDropdownList(ddlSubcategory, "SubCategory", "SubCategoryName", "ID_SubCategory", "SubCategoryName", " FK_Client=" + hfClient.Value + " AND FK_Category= " + ddlCategory.SelectedValue.ToString() + "AND Cancelled=0", "");
        populate = null;

    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        GridFillOpening();
    }


    
     
    protected void btnClearSearch_Click(object sender, EventArgs e)
    {
        Clear();
    }

    //protected void btnClear_Click(object sender, EventArgs e)
    //{
    //    Clear();
    //}
    protected void SaveData()
    {
        bool _continue = false;
        if (gvOpening.Rows.Count > 0)
        {

            long status = 0;
            BlOpeningStock blOpening = new BlOpeningStock();
            blOpening.FK_Store = Convert.ToInt64(hfStore.Value);
            blOpening.EnteredOn = Convert.ToDateTime(DateTime.Now);
            blOpening.FK_Client = Convert.ToInt64(hfClient.Value);
            blOpening.UserCode = hfUser.Value;


            ArrayList arlist = new ArrayList();
             IOpngStock IOpening;

            foreach (GridViewRow gr in gvOpening.Rows)
            {
                IOpening = new IOpngStock();
                IOpening.OpeningStock = Convert.ToDecimal(((TextBox)gr.Cells[8].FindControl("txtOpeningStock")).Text);//openingstock

                IOpening.PrevOpeningStock = Convert.ToDecimal(gr.Cells[9].Text.ToString());
                if (IOpening.OpeningStock != IOpening.PrevOpeningStock)
                {
                   
                    IOpening.SlNo = Convert.ToInt32(gr.RowIndex + 1);
                    IOpening.FK_Items = Convert.ToInt64(gvOpening.DataKeys[gr.DataItemIndex]["FK_Items"].ToString());
                    IOpening.BatchNo = Convert.ToString(gr.Cells[1].Text.ToString());//BatchNo
                    IOpening.ExpiryDate = Convert.ToDateTime(gr.Cells[2].Text.ToString());//BatchNo
                    IOpening.PurRate = Convert.ToDecimal(gr.Cells[3].Text.ToString());
                    IOpening.ActualPurRate = Convert.ToDecimal(gr.Cells[4].Text.ToString());
                    IOpening.SalePrice = Convert.ToDecimal(gr.Cells[5].Text.ToString());
                    IOpening.ActualSalePrice = Convert.ToDecimal(gr.Cells[6].Text.ToString());
                    IOpening.CurrentStock = Convert.ToDecimal(gr.Cells[7].Text.ToString());

                    IOpening.PurTaxAmount = Convert.ToDecimal(gr.Cells[10].Text.ToString());
                    IOpening.MRP = Convert.ToDecimal(gr.Cells[11].Text.ToString());
                    IOpening.IncludeSTax = Convert.ToBoolean(gr.Cells[12].Text.ToString());
                    IOpening.SalesTaxAmount = Convert.ToDecimal(gr.Cells[13].Text.ToString());
                    IOpening.PurTaxAmount = Convert.ToDecimal(gr.Cells[14].Text.ToString());
                    IOpening.ProfitPer = Convert.ToDecimal(gr.Cells[15].Text.ToString());
                    IOpening.ProfitAmt = Convert.ToDecimal(gr.Cells[16].Text.ToString());
                    IOpening.DiscountAmt = Convert.ToDecimal(gr.Cells[17].Text.ToString());
                    IOpening.DiscountPer = Convert.ToDecimal(gr.Cells[18].Text.ToString());
                    IOpening.FK_TaxSettings = Convert.ToInt64(gr.Cells[19].Text.ToString());
                    IOpening.ID_Stock = Convert.ToInt64(gvOpening.DataKeys[gr.DataItemIndex]["ID_Stock"].ToString());
                  
                    arlist.Add(IOpening);
                    IOpening = null;
                }
            }

            if (arlist.Count > 0)
            {
                _continue = true;
            } 
            if (_continue)
            {
                blOpening.xmlOpening = Common.WriteToXml(arlist);

                if (hfIsAdd.Value.ToLower() == "true")
                {
                    status = blOpening.InsertMultipleOpeningStockData();
                    if (status > 0)
                    {
                        ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "toastr.success('Data inserted successfully');alertify.alert('Reference Number:  " + status + "');", true);



                        Clear();
                        //ddlStore.Enabled = false;
                    }
                    else if (status == -2)
                    {
                        Populate.ErrorMessageBox(this, "Cannot insert data, Stock Already Exists");

                    }
                    else
                    {


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
                ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "toastr.warning('Please change any opening stock to continue..')", true);
            }


            blOpening = null;
        }
    }
    public void Clear()
    {
        ddlCategory.SelectedValue = "0";
        ddlBin.SelectedValue = "0";
        ddlGeneric.SelectedValue = "0";
        ddlSubcategory.SelectedValue = "0";
        ddlRack.SelectedValue = "0";
        ddlManufacturer.SelectedValue = "0";
        txtItemShortName.Text = "";
        txtBatch.Text = "";
        gvOpening.DataSource = null;
        gvOpening.DataBind();

    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        SaveData();
    }
}