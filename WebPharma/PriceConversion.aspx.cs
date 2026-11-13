/*----------------------------------------------------------------------------------------------------------------------------------------------------------
Purpose    : Insert/Update/Delete Bank 
Author     : BIBIN TC
Copyright  : Perfect Software Solutions (Clt) Pvt Ltd
Created on : 03/05/2018
----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
WebPharma
On			 By			OMID        REMARK

----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;
using System.Web.Security;
using WebPharma.Business.Masters;
using WebPharma.Business.WebPharma;
using System.Data;
using System.Text;
using System.IO;
using System.Drawing.Printing;
//using BitMiracle.Docotic.Pdf;
///using Neodynamic.SDK.Web;
using System.Diagnostics;
 

public partial class PriceConversion : System.Web.UI.Page
{
    
    #region Functions
    protected void BindData()
    {
        Populate populate = new Populate();

        populate.FillDropdownList(ddlStore, "Store", "StoName", "ID_Store", "ID_Store", " Active='True' AND FK_Client= " + hfClient.Value.Split(',')[0], "");
        populate.FillDropdownList(ddlCategory, "CategoryFirst", "Name", "ID_CategoryFirst", "ID_CategoryFirst", "  FK_Client= " + hfClient.Value.Split(',')[0], "");


        populate = null;

    }
   
  
   
    protected void CheckUserAccess()
    {
        BlUserAccess bluseraccess = new BlUserAccess();
        Populate populate = new Populate();

        bluseraccess.FK_Users = Convert.ToInt64(hfUser.Value);
        bluseraccess.FK_Pages = 6;
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
        if (hfItemValue .Value != "0")
        {
          

            BlItems blitems = new BlItems();
            blitems.MasterId = Convert.ToInt64(hfItemValue .Value.ToString());
            blitems.FK_Client = Convert.ToInt64(hfClient.Value);
            blitems.SelectData();
             
            txtItemShortName.Text = blitems.Name.ToString();
            blitems = null;
            


           
        }
    }


    #endregion

    #region Events
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
           
               // Page.Header.DataBind();
             
             //   ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "ClearAdd();", true);
            if (!IsPostBack)
            {
                divAdd.Style.Add("display", "block");
                hfUser.Value = Request.QueryString["uid"];
                hfClient.Value = Request.QueryString["clnt"];
                CheckUserAccess();

                BindData();
                GvPreviousPurchase.DataSource = null;
                GvPreviousPurchase.DataBind();

                GvCurrentStock.DataSource = null;
                GvCurrentStock.DataBind();
                ddlStore.Focus();
            }
            
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "alertify.alert('"+ex.Message+"')", true);    
        }
    }
   
    protected void imgbtnView_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            //blMessage.Items.Clear();
            divAdd.Style.Add("display", "block");
          //  divView.Style.Add("display", "block");
            ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "ClearAdd();", true);
           
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "alertify.alert('" + ex.Message + "')", true);    
        }
    }
   
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        try
        {
            //blMessage.Items.Clear();
           
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "toastr.warning('" + ex.Message + "')", true);    
        }
    }
   
    #endregion                   
   

    protected void ClearAll()
    {
        GvPreviousPurchase.DataSource = null;
        GvPreviousPurchase.DataBind();

        GvCurrentStock.DataSource = null;
        GvCurrentStock.DataBind();
        lblRowCount.Text = "";
        lblCriteria.Text = "Below Quantity";
          
    }
    protected void btnClear_Click(object sender, EventArgs e)
    {
        try
        {
            ClearAll();
          //  txtBarCode.Text = "";
            ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "ClearAdd();", true);
            ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "ClearAdd();", true);

        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "toastr.warning('" + ex.Message + "')", true);
        }
    }


    protected void btnShow_Click(object sender, EventArgs e)
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

         

          
                 DataTable dtbl = new DataTable();
                 BlItems blitems = new BlItems();
                 blitems.MasterId = Convert.ToInt64(hfItemValue.Value.ToString() == "" ? "0" : hfItemValue.Value.ToString());
                 blitems.FK_Client = Convert.ToInt64(hfClient.Value);
                 blitems.PageIndex = Convert.ToInt16(currentPage);
                 blitems.FK_CategoryFirst = Convert.ToInt64(ddlCategory.SelectedValue);
                // blitems.PageSize = Convert.ToInt16(202);
                 blitems.Name = Convert.ToString(txtItemShortName.Text);
                 dtbl = blitems.SelectAllData();
                // GvPreviousPurchase.AllowPaging = false;

                  GvPreviousPurchase.DataSource = dtbl;
                 GvPreviousPurchase.DataBind();
                // GvPreviousPurchase.AllowPaging = true;

                if (dtbl.Rows.Count > 0)
                {
                    int recordCount = Convert.ToInt32(dtbl.Rows[0]["RecordCount"].ToString());
                    this.PopulatePager(recordCount, currentPage);
                }

                
              
                blitems = null;
                ViewState["TempTable"] = dtbl;


             
                //BlStock blstock = new BlStock();
                //blstock.FK_Items = Convert.ToInt64(hfItemValue.Value.ToString());
                //blstock.FK_Client = Convert.ToInt64(hfClient.Value);
                //blstock.PageSize = Convert.ToInt16(500);
                //dtbl = blstock.SelectAllDataForPriceConversion();   //blItems.StockItemsSelect();
                //GvPreviousPurchase.DataSource = dtbl;
                //GvPreviousPurchase.DataBind();


                dtbl = null;
               
          

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
    protected void Page_Changed(object sender, EventArgs e)
    {
        int pageIndex = int.Parse((sender as LinkButton).CommandArgument);
        GridFill(pageIndex);
    }


    protected void hfItemValue_ValueChanged(object sender, EventArgs e)
    {
        try
        {

            ddlItemsFill();

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
             // ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "ShowProgress();", true);
          //   OnClientClick=" ShowProgress();" 
            System.Threading.Thread.Sleep(1000);
         
            //blMessage.Items.Clear();
            ///   string a = Convert.ToString(hfItemValue.Value.ToString());
            ///   
            int checkfalse = 0;
            if (GvPreviousPurchase.Rows.Count > 0)
            {
                BlPriceConversion blpriceconversion = new BlPriceConversion();

                ArrayList arlist = new ArrayList();
                DataTable dtbl = (DataTable)ViewState["TempTable"];

                Items Items;

                for (int i = 0; i < GvPreviousPurchase.Rows.Count; i++)
                {
                    if (((System.Web.UI.WebControls.CheckBox)GvPreviousPurchase.Rows[Convert.ToInt16(i)].FindControl("chkBox")).Checked == true)
                    {
                        //

                        checkfalse = 1;
                        Items = new Items();

                        Items.FK_Items = Convert.ToInt64(dtbl.Rows[i]["ID_Items"].ToString());
                        arlist.Add(Items);
                        Items = null;

                    }
                }



                blpriceconversion.XmlItems = Common.WriteToXml(arlist);



                blpriceconversion.TransDate = Convert.ToDateTime(DateTime.Now);
                blpriceconversion.FK_Store = Convert.ToInt64(ddlStore.SelectedValue);
                blpriceconversion.FK_Category = Convert.ToInt64(ddlCategory.SelectedValue);
                blpriceconversion.MinimumQty = Convert.ToDecimal(txtBelowQuantity.Text == "" ? "0" : txtBelowQuantity.Text);
                blpriceconversion.PriceTag = Convert.ToInt64(ddlCriteria.SelectedValue);
                blpriceconversion.FK_Client = Convert.ToInt64(hfClient.Value);
                blpriceconversion.UserCode = Convert.ToString(hfUser.Value);
                blpriceconversion.FK_Items = Convert.ToInt64(hfItemValue.Value == "" ? "0" : hfItemValue.Value);

                if (checkfalse == 1)
                {
                    long status = 0;
                    if (hfIsAdd.Value.ToLower() == "true")
                    {

                        status = blpriceconversion.InsertData();
                        if (status > 0)
                        {




                            ClearAll();
                            ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "ClearAdd(); toastr.success('Data inserted successfully')", true);

                        }

                        else
                        {

                            ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "toastr.error('Cannot insert data')", true);

                        }
                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", " toastr.warning('Please select atleast one item ')", true);
                
                }

            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "Select atleast one Items...", true);

            }


            //  SaveData();



        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "toastr.warning('" + ex.Message + "')", true);
        }
    }
  
    protected void GvPreviousPurchase_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try{
        if (e.CommandName == "ItemStockSelect")
        {
            GridViewRow row = (GridViewRow)((LinkButton)e.CommandSource).NamingContainer;
            int index = row.RowIndex;
            string ID_Items = GvPreviousPurchase.DataKeys[index]["ID_Items"].ToString();


            BlItems blItems = new BlItems();
            DataTable dtbl = new DataTable();
            blItems.MasterId = Convert.ToInt64(ID_Items);
            //blItems.FK_Store = Convert.ToInt64(ddlStore.SelectedValue);
            blItems.FK_Client = Convert.ToInt64(hfClient.Value);

            dtbl = blItems.SelectCurrentStockForStockInPriceConversion();    //blItems.StockItemsSelect();
            GvCurrentStock.DataSource = dtbl;
            GvCurrentStock.DataBind();
          
            blItems = null;
        }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "toastr.warning('" + ex.Message + "')", true);
        }
    }
    protected void ddlCriteria_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlCriteria.Items.Count>0)
            {
                if (ddlCriteria.SelectedValue == "2")
                {
                    txtBelowQuantity.Enabled = false;
                    txtBelowQuantity.Text = "0";
                    lblCriteria.Text = "Below Quantity";
                }
                if (ddlCriteria.SelectedValue == "4")
                {
                    txtBelowQuantity.Enabled = true;
                    txtBelowQuantity.Text = "0";
                    lblCriteria.Text = "Sales Price";
                    reqvalidatorStore.Enabled = false;
                    ddlStore.BackColor = System.Drawing.ColorTranslator.FromHtml("#FFF");

                }
                else
                {
                    txtBelowQuantity.Enabled = true;
                    lblCriteria.Text = "Below Quantity";
                    reqvalidatorStore.Enabled = true;
                    ddlStore.BackColor = System.Drawing.ColorTranslator.FromHtml("#FCFDD6");
                }
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "toastr.warning('" + ex.Message + "')", true);
        }
    }
  
}