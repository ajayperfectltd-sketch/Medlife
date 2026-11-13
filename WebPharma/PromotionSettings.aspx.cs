/*----------------------------------------------------------------------------------------------------------------------------------------------------------
Purpose    : Insert/Update/Delete Promotion Settings
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


public partial class PromotionSettings : System.Web.UI.Page
{
    #region  Functions
    private void BindData()
    {
         Populate populate=new Populate();
         populate.FillDropdownList(ddlPromotionType, "PromotionType", "Name", "ID_PromotionType", "ID_PromotionType", "FK_Client = "+hfClient.Value, "");
         populate.FillDropdownList(ddlPromotionTypeSearch, "PromotionType", "Name", "ID_PromotionType", "ID_PromotionType", "FK_Client = " + hfClient.Value, "");
         populate.FillDropdownList(ddlItem, "Items", "Name", "ID_Items", "ID_Items", "Active=1 AND ItemType=2 and FK_Client = " + hfClient.Value, "");

         populate.FillDropdownList(ddlItemearch, "Items", "Name", "ID_Items", "ID_Items", "Active=1  AND ItemType=2 and FK_Client = " + hfClient.Value, "");
         populate.FillDropdownList(ddlCategoryFirst, "CategoryFirst", "Name", "ID_CategoryFirst", "ID_CategoryFirst", " FK_Client= " + hfClient.Value, "");
         populate.FillDropdownList(ddlBrand, "Brand", "Name", "ID_Brand", "ID_Brand", " FK_Client=1", "");
         populate = null;
    }
     public void GridFill(int currentPage)
    {
        BlPromotionSettings Blpromotion = new BlPromotionSettings();
        DataTable dtbl = new DataTable();
        Blpromotion.FK_PromotionType = Convert.ToInt64(ddlPromotionTypeSearch.SelectedValue);
        Blpromotion.FK_Items = Convert.ToInt64(ddlItemearch.SelectedValue);
        Blpromotion.PageSize = 10;
        currentPage = currentPage == 0 ? 1 : currentPage;
        Blpromotion.PageIndex = Convert.ToInt16(currentPage);
        Blpromotion.FK_Client = Convert.ToInt64(hfClient.Value);
        Blpromotion.UserCode = hfUser.Value;
        dtbl = Blpromotion.SelectAllData();
        gvPromotionSettings.DataSource = dtbl;
        gvPromotionSettings.DataBind();
        if (dtbl.Rows.Count > 0)
        {
            int recordCount = Convert.ToInt32(dtbl.Rows[0]["RecordCount"].ToString());
            this.PopulatePager(recordCount, currentPage);
        }
        Blpromotion = null;
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
     protected void SaveData()
     {
         long status = 0;
         BlPromotionSettings Blpromotion = new BlPromotionSettings();
         Blpromotion.FK_PromotionType = Convert.ToInt64(ddlPromotionType.SelectedValue);
         if(rbtnlstPromotionType.SelectedItem.Text=="Items")
         Blpromotion.FK_Items = Convert.ToInt64(ddlItem.SelectedValue.ToString());
         else if (rbtnlstPromotionType.SelectedItem.Text == "Category")
          Blpromotion.FK_CategoryFirst = Convert.ToInt64(ddlCategoryFirst.SelectedValue);
         else
          Blpromotion.FK_Brand = Convert.ToInt64(ddlBrand.SelectedValue);
        
         Blpromotion.EffectDate = DateTime.ParseExact(txtEffectedDate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
         Blpromotion.DiscountPercent = decimal.Parse(txtDiscountPercent.Text == "" ? "0" : txtDiscountPercent.Text);
         Blpromotion.Buy = Convert.ToInt64(txtBuy.Text == "" ? "0" : txtBuy.Text);
         Blpromotion.Get = Convert.ToInt64(txtGet.Text == "" ? "0" : txtGet.Text);
         Blpromotion.FK_Client = Convert.ToInt64(hfClient.Value);
         Blpromotion.UserCode = hfUser.Value;
         Blpromotion.Closed = cbxClosed.Checked;
         Blpromotion.CloseDate = DateTime.ParseExact(txtClosedDate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
         Blpromotion.Type = Convert.ToInt16(rbtnlstPromotionType.SelectedValue);
         if (hdfPromotionSettings.Value == string.Empty)
         {
             if (hfIsAdd.Value.ToLower() == "true")
             {
                 status = Blpromotion.InsertData();
                 if (status > 0)
                 {
                     GridFill(1);
                     //blMessage.Items.Add("Data inserted successfully");

                     ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "ShowAddDiv();", true);
                     // ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "ClearAdd();", true);
                     Populate.InsertMsg(this);

                 }
                 else if (status == -2)
                 {
                     ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "ShowAddDiv();", true);
                     //blMessage.Items.Add("Cannot insert data");
                     Populate.ErrorMessageBox(this, "Cannot insert data,Settings already exists");

                 }
                 else
                 {
                     ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "ShowAddDiv();", true);
                     //blMessage.Items.Add("Cannot insert data");
                     Populate.ErrorMessageBox(this, "Cannot insert data");

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
                 Blpromotion.MasterId = Convert.ToInt64(hdfPromotionSettings.Value);
                 status = Blpromotion.UpdateData();
                 if (status > 0)
                 {
                     //blMessage.Items.Add("Data updated successfully");

                     // ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "ShowAddDiv();", true);
                     //ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "ClearAdd();", true);
                     Populate.UpdateMsg(this);

                     GridFill(1);
                 }
                 else if (status==-2)
                 {
                     ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "ShowAddDiv();", true);
                     //blMessage.Items.Add("Cannot update data");
                     Populate.ErrorMessageBox(this, "Cannot update data,Settings already exists");

                 }
                 else
                 {
                     ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "ShowAddDiv();", true);
                     //blMessage.Items.Add("Cannot update data");
                     Populate.ErrorMessageBox(this, "Cannot update data..");

                 }
             }
             else
             {
                 ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "toastr.warning('User does not have permission to Update')", true);
                 
             }


         }
         Blpromotion = null;

         ClearLi();
         if (rbtnlstPromotionType.SelectedItem.Text == "Items")
            LiItems.Attributes.Add("Style", "display:block");       
         else if (rbtnlstPromotionType.SelectedItem.Text == "Category")
             LiCategoryFirst.Attributes.Add("Style", "display:block");
         else
             LiBrand.Attributes.Add("Style", "display:block");
     }
    #endregion
    #region Events
     protected void Page_Load(object sender, EventArgs e)
    {
        //blMessage.Items.Clear();
        try
        {
            if (!IsPostBack)
            {
                hfUser.Value = Request.QueryString["uid"];
                hfClient.Value = Request.QueryString["clnt"]; 

                Page.Header.DataBind();
                ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "ClearAdd();", true);
                divAdd.Style.Add("display", "block");
                CheckUserAccess();
                BindData();
                txtEffectedDate.Attributes.Add("readonly", "readonly");
                txtClosedDate.Attributes.Add("readonly", "readonly");                
                txtClosedDate.Text = txtEffectedDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
                ddlPromotionType.Focus();
            }
        }
        catch (Exception ex)
        {
            //blMessage.Items.Add(ex.Message);
        }
    }

    protected void Page_Changed(object sender, EventArgs e)
    {
        //blMessage.Items.Clear();
        try
        {
            //blMessage.Items.Clear();
            int pageIndex = int.Parse((sender as LinkButton).CommandArgument);
            GridFill(pageIndex);
        }
        catch (Exception ex)
        {
            //blMessage.Items.Add(ex.Message);
        }
    }
    protected void imgbtnView_Click(object sender, ImageClickEventArgs e)
    {
        //blMessage.Items.Clear();
        try
        {
            //blMessage.Items.Clear();
            divAdd.Style.Add("display", "none");
            divView.Style.Add("display", "block");
            ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "ClearAdd();", true);
            // BindSearch();
            GridFill(1);
        }
        catch (Exception ex)
        {
            //blMessage.Items.Add(ex.Message);
        }
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        //blMessage.Items.Clear();
        try
        {
           
            SaveData();
            ddlPromotionType.Focus();
        }
        catch (Exception ex)
        {
            //blMessage.Items.Add(ex.Message);
        }
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        //blMessage.Items.Clear();
        try
        {
            //blMessage.Items.Clear();
            GridFill(1);
            ddlPromotionTypeSearch.Focus();
        }
        catch (Exception ex)
        {
            //blMessage.Items.Add(ex.Message);
        }
    }



    protected void gvPromotionSettings_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        //blMessage.Items.Clear();
        ClearLi();
        ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "ShowAddDiv();", true);
        BlPromotionSettings Blpromotion = new BlPromotionSettings();
        Blpromotion.MasterId = Convert.ToInt64(gvPromotionSettings.DataKeys[e.NewSelectedIndex]["ID_PromotionSettings"].ToString());
        hdfPromotionSettings.Value = Blpromotion.MasterId.ToString();


        Blpromotion.FK_Client = Convert.ToInt64(hfClient.Value);
        Blpromotion.UserCode = hfUser.Value;

        Blpromotion.SelectData();
        ddlPromotionType.SelectedValue = Blpromotion.FK_PromotionType.ToString();
        try
        {
            if (Blpromotion.Type == 0)
            {
                ddlItem.SelectedValue = Blpromotion.FK_Items.ToString();
                LiItems.Attributes.Add("Style", "display:block");
            }
            else if (Blpromotion.Type == 1)
            {
                ddlCategoryFirst.SelectedValue = Blpromotion.FK_CategoryFirst.ToString();
                LiCategoryFirst.Attributes.Add("Style", "display:block");
            }
            else if (Blpromotion.Type == 2)
            {
                ddlBrand.SelectedValue = Blpromotion.FK_Brand.ToString();
                LiBrand.Attributes.Add("Style", "display:block");
            }
        }
        catch (Exception ex)
        {
            ddlItem.SelectedValue = "0";
            ddlCategoryFirst.SelectedValue = "0";
            ddlBrand.SelectedValue = "0";
        }
        rbtnlstPromotionType.SelectedIndex = Blpromotion.Type;
        txtEffectedDate.Text = Blpromotion.EffectDate.ToString("dd/MM/yyyy");
        txtDiscountPercent.Text = Blpromotion.DiscountPercent.ToString();
        txtBuy.Text = Blpromotion.Buy.ToString();
        txtGet.Text = Blpromotion.Get.ToString();
        //if (Blpromotion.Closed)
        //{
            cbxClosed.Checked = Blpromotion.Closed;
            txtClosedDate.Text = Blpromotion.CloseDate.ToString("dd/MM/yyyy");
        //}
        //else
        //{
        //    cbxClosed.Checked = false;
        //    txtClosedDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
        //}

    }
    protected void ClearLi()
    {
        LiItems.Attributes.Add("Style", "display:none");
        LiCategoryFirst.Attributes.Add("Style", "display:none");
        LiBrand.Attributes.Add("Style", "display:none");
    }
    protected void gvPromotionSettings_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            //blMessage.Items.Clear();
            string confirmValue = Request.Form["confirm_value"];
            if (confirmValue.Length > 3)
                confirmValue = confirmValue.Substring(confirmValue.Length - 3, 3);
            if (confirmValue == "Yes")
            {
                long status = 0;
                BlPromotionSettings Blpromotion = new BlPromotionSettings();


                Blpromotion.MasterId = Convert.ToInt64(gvPromotionSettings.DataKeys[e.RowIndex]["ID_PromotionSettings"].ToString());
                Blpromotion.FK_Client = Convert.ToInt64(hfClient.Value);
                Blpromotion.UserCode = hfUser.Value;
                if (hfIsDelete.Value.ToLower() == "true")
                {

                    status = Blpromotion.DeleteData();
                    if (status > 0)
                    {
                        GridFill(1);
                        Populate.DeleteMsg(this);
                        //blMessage.Items.Add("Deleted successfully");
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
                Blpromotion = null;

            }
        }
        catch(Exception ex)
        {
        }
    }
     #endregion
}