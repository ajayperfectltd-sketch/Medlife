/*----------------------------------------------------------------------------------------------------------------------------------------------------------
Purpose    : Insert/Update/Delete CategoryFirst 
Author     : Bibin tc
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
using System.Data;
using WebPharma.Business.Masters;


public partial class CategoryFirst: System.Web.UI.Page
{
    #region Function
    public void Delete()
    {
        try
        {
            divPopupReason.Visible = true;
            Populate Populate = new Populate();
            CommonPopup1.CallingFunction = "PopupOnReason";
            ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "PopupOnReason();", true);
            mpCommon.Show();
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
            BlCategoryFirst blcategory = new BlCategoryFirst();
            //blReason.MasterId = Convert.ToInt64(gvReason.DataKeys[e.RowIndex]["ID_Reason"].ToString());
            blcategory.UserCode = hfuserName.Value;
            blcategory.MasterId = Convert.ToInt64(hfMasterID.Value);
            blcategory.FK_Reason = Convert.ToInt64(Reason);
            if (hfIsDelete.Value.ToLower() == "true")
            {
                status = blcategory.DeleteData();
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
            else
            {
                ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "toastr.warning('User does not have permission to Delete')", true);
                txtShortName.Focus();
            }
            blcategory = null;
        }
    }
    protected void BindData()
    {
        Populate populate = new Populate();
        populate = null;
    }
    protected void GridFill(int currentPage)
    {
        BlCategoryFirst blCategoryFirst = new BlCategoryFirst();
        DataTable dtbl = new DataTable();
        blCategoryFirst.Shortname = txtSearchShortName.Text.Trim();
        blCategoryFirst.Name = txtSearchName.Text.Trim();
        blCategoryFirst.FK_Client = Convert.ToInt64(hfClient.Value);
        blCategoryFirst.UserCode = hfUser.Value;
        blCategoryFirst.PageIndex = Convert.ToInt16(currentPage);
        dtbl = blCategoryFirst.SelectAllData();
        gvCategoryFirst.DataSource = dtbl;
        gvCategoryFirst.DataBind();
        blCategoryFirst = null;
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
        bluseraccess.FK_Pages = 10;
        bluseraccess.FK_Client = Convert.ToInt64(hfClient.Value);
        bluseraccess.CheckUserAccess();

        hfIsAdd.Value = (Convert.ToBoolean(bluseraccess.IsAdd)).ToString();
        hfIsUpdate.Value = (Convert.ToBoolean(bluseraccess.IsUpdate)).ToString();
        hfIsDelete.Value = (Convert.ToBoolean(bluseraccess.IsDelete)).ToString();
        bluseraccess = null;
    }
    protected void SaveData()
    {
        long status = 0;
        BlCategoryFirst blCategoryFirst = new BlCategoryFirst();
        blCategoryFirst.Shortname = txtShortName.Text;
        blCategoryFirst.Name = txtName.Text;
        blCategoryFirst.Discount = Convert.ToDecimal(txtDiscountPercentage.Text);
        blCategoryFirst.Commission = Convert.ToDecimal(txtCommission.Text);
        blCategoryFirst.Profit = Convert.ToDecimal(txtProfit.Text);
        blCategoryFirst.ExpiryDays = Convert.ToInt32(txtExpiryDays.Text);
        blCategoryFirst.FK_Client = Convert.ToInt64(hfClient.Value);
        blCategoryFirst.NonMedicine = Convert.ToBoolean(chkNonMedicine.Checked);
        blCategoryFirst.UserCode = hfuserName.Value;
        if (hfCategoryFirstId.Value == "0")
        {
            if (hfIsAdd.Value.ToLower() == "true")
            {
                status = blCategoryFirst.InsertData();
                if (status > 0)
                {
                    GridFill(1);
                    ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "ShowAddDiv();", true);
                    ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "toastr.success('Data inserted successfully');ClearAdd();", true);

                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "ShowAddDiv();", true);
                    ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "toastr.error('Cannot insert data, ShortName/Name already exists')", true);
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
                blCategoryFirst.MasterId = Convert.ToInt64(hfCategoryFirstId.Value);
                status = blCategoryFirst.UpdateData();
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
        blCategoryFirst = null;
    }
    #endregion

    #region Events
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                Page.Header.DataBind();
                ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "ClearAdd();", true);
                divAdd.Style.Add("display", "block");
                hfUser.Value = Request.QueryString["uid"];
                hfClient.Value = Request.QueryString["clnt"];
                hfuserName.Value = Session["Username"].ToString();
                BindData();
                CheckUserAccess();
                GridFill(1);
                txtShortName.Focus();
                txtDiscountPercentage.Attributes.Add("onblur", "disc()");
                if (Convert.ToBoolean(hfIsAdd.Value) == false)
                {
                    ((Label)CommonPopupPrivilege.FindControl("popuplabel")).Text = "User does not have permission to Add";
                    mpPrivilege.Show();
                }
                if (Convert.ToBoolean(hfIsUpdate.Value) == false)
                {
                    gvCategoryFirst.Columns[3].Visible = false;
                }
                if (Convert.ToBoolean(hfIsDelete.Value) == false)
                {
                    gvCategoryFirst.Columns[4].Visible = false;
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
           
            divAdd.Style.Add("display", "none");
            divView.Style.Add("display", "block");
            ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "ClearAdd();", true);
            GridFill(1);
            txtSearchShortName.Focus();
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "toastr.warning('" + ex.Message + "')", true); 
        }
    }
    protected void Page_Changed(object sender, EventArgs e)
    {
        
        int pageIndex = int.Parse((sender as LinkButton).CommandArgument);

        GridFill(pageIndex);
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            
            SaveData();
        }
        catch (Exception ex)
        {
           
        }
    }
    protected void gvCategoryFirst_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        try
        {
            
            ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "ShowAddDiv();", true);
            BlCategoryFirst blCategoryFirst = new BlCategoryFirst();
            blCategoryFirst.MasterId = Convert.ToInt64(gvCategoryFirst.DataKeys[e.NewSelectedIndex]["ID_Category"].ToString());
            hfCategoryFirstId.Value = blCategoryFirst.MasterId.ToString();
            blCategoryFirst.FK_Client = Convert.ToInt64(hfClient.Value);
            blCategoryFirst.UserCode = hfUser.Value;
            blCategoryFirst.SelectData();
            txtShortName.Text = blCategoryFirst.Shortname.ToString();
            txtName.Text = blCategoryFirst.Name.ToString();
            txtDiscountPercentage.Text =(blCategoryFirst.Discount.ToString());
            txtCommission.Text = blCategoryFirst.Commission.ToString();
            txtProfit.Text = blCategoryFirst.Profit.ToString();
            txtExpiryDays.Text = blCategoryFirst.ExpiryDays.ToString();
            chkNonMedicine.Checked =Convert.ToBoolean(blCategoryFirst.NonMedicine.ToString());
                
            blCategoryFirst = null;
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "toastr.warning('" + ex.Message + "')", true); 
        }
    }
    protected void gvCategoryFirst_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
           
            string confirmValue = Request.Form["confirm_value"];
            if (confirmValue.Length > 3)
                confirmValue = confirmValue.Substring(confirmValue.Length - 3, 3);
            if (confirmValue == "Yes")
            {
                long status = 0;
                BlCategoryFirst blCategoryFirst = new BlCategoryFirst();
                blCategoryFirst.MasterId = Convert.ToInt64(gvCategoryFirst.DataKeys[e.RowIndex]["ID_Category"].ToString());
                hfMasterID.Value = (gvCategoryFirst.DataKeys[e.RowIndex]["ID_Category"].ToString());
                blCategoryFirst.UserCode = hfUser.Value;
                blCategoryFirst = null;
            }
            else
            {
                mpCommon.Hide();
                
            }

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
          
            GridFill(1);
            txtSearchShortName.Focus();
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "toastr.warning('" + ex.Message + "')", true); 
        }
    }
                
    protected void ImageButton2_Click(object sender, ImageClickEventArgs e)
    {
        Delete();
    }

    protected void gvCategoryFirst_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvCategoryFirst.PageIndex = e.NewPageIndex;
        GridFill(e.NewPageIndex); 
    }
    
    protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
    {
        if (Convert.ToBoolean(hfIsAdd.Value) == false)
        {
            ((Label)CommonPopupPrivilege.FindControl("popuplabel")).Text = "User does not have permission to Add";
            mpPrivilege.Show();
           
        }
        ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "ShowAddDiv();", true);
    }
    #endregion
}