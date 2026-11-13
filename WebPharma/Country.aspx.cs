/*----------------------------------------------------------------------------------------------------------------------------------------------------------
Purpose    : Insert/Update/Delete Country 
Author     : Bibin TC
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
using WebPharma.Business.Masters;
using System.Data;

public partial class Country : System.Web.UI.Page
{
    #region Function
    protected void BindData()
    {
        //Populate populate = new Populate();
        //populate.FillDropdownList(ddlCurrency, "Currency", "Name", "ID_Currency", "ID_Currency","FK_Client="+hfClient.Value, "");
        //populate = null;
    }
    protected void GridFill(int currentPage)
    {
        BlCountry blcountry = new BlCountry();
        DataTable dtbl = new DataTable();
        blcountry.CountryCode = txtSearchShortName.Text.Trim();
        blcountry.CountryName = txtSearchName.Text.Trim();
        blcountry.FK_Client = Convert.ToInt64(hfClient.Value);
        blcountry.PageIndex = Convert.ToInt16(currentPage);
        blcountry.UserCode = hfUser.Value;
        dtbl = blcountry.SelectAllData();
        gvCountry.DataSource = dtbl;
        gvCountry.DataBind();
        //if (dtbl.Rows.Count > 0)
        //{
        //    int recordCount = Convert.ToInt32(dtbl.Rows[0]["RecordCount"].ToString());
        //    this.PopulatePager(recordCount, currentPage);
        //}
        blcountry = null;
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
        bluseraccess.FK_Pages = 8;
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
        BlCountry blcountry = new BlCountry();
        blcountry.CountryCode = txtShortName.Text;
        blcountry.CountryName = txtName.Text;
        blcountry.FK_Client = Convert.ToInt64(hfClient.Value);
        blcountry.UserCode = hfUsername.Value;
       // blcountry.FK_Currency = Convert.ToInt64(ddlCurrency.SelectedValue.ToString());     
        if (hfCountryId.Value == "0")
        {
            if (hfIsAdd.Value.ToLower() == "true")
            {
                status = blcountry.InsertData();
                if (status > 0)
                {
                    ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "ShowAddDiv();", true);
                    ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "toastr.success('Data inserted successfully');ClearAdd();", true);
                    GridFill(1);
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "ShowAddDiv();", true);
                    ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "toastr.error('Cannot insert data, Country Name already exists')", true);
                    txtShortName.Focus();
                }
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "toastr.warning('User does not have permission to add')", true);
                txtShortName.Focus();

            }
        }
        else
        {
            if (hfIsUpdate.Value.ToLower() == "true")
            {

                blcountry.MasterId = Convert.ToInt64(hfCountryId.Value);
                status = blcountry.UpdateData();
                if (status > 0)
                {
                    ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "ShowAddDiv();", true);
                    ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "toastr.success('Data updated successfully');ClearAdd();", true);
                    GridFill(1);
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "ShowAddDiv();", true);
                    ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "toastr.error('Cannot update data, ShortName/Name already exists')", true);
                    txtShortName.Focus();
                }
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "toastr.warning('User does not have permission to Update')", true);
                txtShortName.Focus();
            }
        }
        blcountry = null;
    }
    #endregion

    #region Events
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {

                if (Session["Username"] != null && Convert.ToString(Session["Username"]) != "")
                {

                    hfUsername.Value = Session["Username"].ToString();

                    Page.Header.DataBind();
                    //blMessage.Items.Clear();
                    ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "ClearAdd();", true);
                    divAdd.Style.Add("display", "block");
                    hfUser.Value = Request.QueryString["uid"];
                    hfClient.Value = Request.QueryString["clnt"];
                    CheckUserAccess();

                    BindData();
                    GridFill(1);
                    if (Convert.ToBoolean(hfIsAdd.Value) == false)
                    {
                        ((Label)CommonPopupPrivilege.FindControl("popuplabel")).Text = "User does not have permission to Add";
                        mpPrivilege.Show();
                    }
                    if (Convert.ToBoolean(hfIsUpdate.Value) == false)
                    {
                        gvCountry.Columns[3].Visible = false;
                    }
                    if (Convert.ToBoolean(hfIsDelete.Value) == false)
                    {
                        gvCountry.Columns[4].Visible = false;
                    }
               
                }
                else
                {
                    Response.Redirect("~/Default.aspx", false);
                }

               
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "toastr.warning('" + ex.Message + "')", true);  
        }
    }
    protected void imgbtnView_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            //blMessage.Items.Clear();
            txtSearchName.Focus();
            divAdd.Style.Add("display", "none");
            divView.Style.Add("display", "block");
            ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "ClearAdd();", true);
            GridFill(1);
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "toastr.warning('" + ex.Message + "')", true);  
        }
    }
    protected void Page_Changed(object sender, EventArgs e)
    {
      //  blMessage.Items.Clear();
        int pageIndex = int.Parse((sender as LinkButton).CommandArgument);
        GridFill(pageIndex);
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            //blMessage.Items.Clear();
            SaveData();
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "toastr.warning('" + ex.Message + "')", true);  
        }
    }
    protected void gvCountry_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        try
        {
           // blMessage.Items.Clear();
            ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "ShowAddDiv();", true);
            BlCountry blcountry = new BlCountry();
            blcountry.MasterId = Convert.ToInt64(gvCountry.DataKeys[e.NewSelectedIndex]["ID_Country"].ToString());
            hfCountryId.Value = blcountry.MasterId.ToString();
            blcountry.FK_Client = Convert.ToInt64(hfClient.Value);
            blcountry.SelectData();
            txtShortName.Text = blcountry.CountryCode.ToString();
            txtName.Text = blcountry.CountryName.ToString();
           // ddlCurrency.SelectedValue = blcountry.FK_Currency.ToString();
            blcountry = null;
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "toastr.warning('" + ex.Message + "')", true);  
        }
    }
    protected void gvCountry_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            //blMessage.Items.Clear();
            string confirmValue = Request.Form["confirm_value"];
            if (confirmValue.Length > 3)
                confirmValue = confirmValue.Substring(confirmValue.Length - 3, 3);
            if (confirmValue == "Yes")
            {
                
                hfMasterID.Value = gvCountry.DataKeys[e.RowIndex]["ID_Country"].ToString();

            }
            else
            { mpDelete.Hide(); }
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
            //blMessage.Items.Clear();
            GridFill(1); 
            txtSearchShortName.Focus();
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "toastr.warning('" + ex.Message + "')", true);  
        }
    }
                   
    protected void gvCountry_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvCountry.PageIndex = e.NewPageIndex;
        GridFill(e.NewPageIndex);
    }
    
    #endregion 
    protected void ImageBtnDelete_Click(object sender, ImageClickEventArgs e)
    {
        Delete();
    }

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
            BlCountry blcountry = new BlCountry();

            blcountry.MasterId = Convert.ToInt64(hfMasterID.Value);
            blcountry.FK_Client = Convert.ToInt64(hfClient.Value);
            blcountry.UserCode = hfUsername.Value;
            blcountry.FK_Reason = Convert.ToInt64(Reason);
            if (hfIsDelete.Value.ToLower() == "true")
            {
                status = blcountry.DeleteData();
                if (status > 0)
                {
                    GridFill(1);
                    ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "toastr.success('Data deleted successfully')", true);

                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "toastr.error('Cannot delete data, reference exists')", true);

                }
            }
            {
                ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "toastr.warning('User does not have permission to Delete')", true);
            }
            blcountry = null;
        }



    }

}