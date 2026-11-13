/*----------------------------------------------------------------------------------------------------------------------------------------------------------
Purpose    : Insert/Update/Delete Manufacturer 
Author     : bibin TC
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

public partial class Manufacturer : System.Web.UI.Page
{
    #region Functions
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
            BlManufacturer blManufature = new BlManufacturer();
            blManufature.UserCode = hfuserName.Value;
            blManufature.MasterId = Convert.ToInt64(hfMasterID.Value);
            blManufature.FK_Reason = Convert.ToInt64(Reason);
            if (hfIsDelete.Value.ToLower() == "true")
            {
                status = blManufature.DeleteData();
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
            blManufature = null;
        }
    }
    protected void BindData()
    {
        Populate populate = new Populate();
        populate = null;      
    }
    protected void GridFill(int currentPage)
    {
        BlManufacturer blmanufacturer = new BlManufacturer();
        DataTable dtbl = new DataTable();
        blmanufacturer.ShortName = txtSearchShortName.Text.Trim();
        blmanufacturer.Name = txtSearchName.Text.Trim();
        blmanufacturer.FK_Client = Convert.ToInt64(hfClient.Value);
        blmanufacturer.UserCode = hfUser.Value;
        blmanufacturer.PageIndex = Convert.ToInt16(currentPage);
        dtbl = blmanufacturer.SelectAllData();
        gvManufacturer.DataSource = dtbl;
        gvManufacturer.DataBind();
        blmanufacturer = null;
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
    protected void SaveData()
    {
        long status = 0;
        BlManufacturer blmanufacturer = new BlManufacturer();
        blmanufacturer.ShortName = txtShortName.Text;
        blmanufacturer.Name = txtName.Text;
        blmanufacturer.Phone = txtPhone.Text;
        blmanufacturer.Address1 = txtAddress1.Text;
        blmanufacturer.Address2 = txtAddress2.Text;
        blmanufacturer.Address3 = txtAddress3.Text;
        blmanufacturer.MobileNo = txtMobileno.Text;
        blmanufacturer.EnteredOn = DateTime.Now;
        blmanufacturer.FK_Client = Convert.ToInt64(hfClient.Value);
        blmanufacturer.UserCode = hfuserName.Value;
        if (hfManufacturerId.Value == "0")
        {
            if (hfIsAdd.Value.ToLower() == "true")
            {
                status = blmanufacturer.InsertData();
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

                blmanufacturer.MasterId = Convert.ToInt64(hfManufacturerId.Value);
                status = blmanufacturer.UpdateData();
                if (status > 0)
                {
                    GridFill(1);
                    ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "ShowAddDiv();", true);
                    ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "toastr.success('Data updated successfully');ClearAdd();", true);
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
        blmanufacturer = null;
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
    #endregion

    #region Events
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                Page.Header.DataBind();
                //blMessage.Items.Clear();
                hfUser.Value = Request.QueryString["uid"];
                hfClient.Value = Request.QueryString["clnt"];
                ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "ClearAdd();", true);
                divAdd.Style.Add("display", "block");
                hfuserName.Value = Session["Username"].ToString();
                CheckUserAccess();
                BindData();
                txtShortName.Focus();
                if (Convert.ToBoolean(hfIsAdd.Value) == false)
                {
                    ((Label)CommonPopupPrivilege.FindControl("popuplabel")).Text = "User does not have permission to Add";
                    mpPrivilege.Show();
                }
                if (Convert.ToBoolean(hfIsUpdate.Value) == false)
                {
                    gvManufacturer.Columns[3].Visible = false;
                }
                if (Convert.ToBoolean(hfIsDelete.Value) == false)
                {
                    gvManufacturer.Columns[4].Visible = false;
                }
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "toastr.warning('" + ex.Message + "')", true); 
        }
    }
    protected void Page_Changed(object sender, EventArgs e)
    {
        try
        {
            
            int pageIndex = int.Parse((sender as LinkButton).CommandArgument);
            GridFill(pageIndex);
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
    protected void gvManufacturer_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        try
        {
            
            ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "ShowAddDiv();", true);
            BlManufacturer blmanufacturer = new BlManufacturer();
            blmanufacturer.MasterId = Convert.ToInt64(gvManufacturer.DataKeys[e.NewSelectedIndex]["ID_Manufacturer"].ToString());
            hfManufacturerId.Value = blmanufacturer.MasterId.ToString();
            blmanufacturer.FK_Client = Convert.ToInt64(hfClient.Value);
            blmanufacturer.UserCode = hfUser.Value;
            blmanufacturer.SelectData();
            txtShortName.Text = blmanufacturer.ShortName.ToString();
            txtMobileno.Text = blmanufacturer.MobileNo.ToString();
            txtName.Text = blmanufacturer.Name.ToString();
            txtPhone.Text = blmanufacturer.Phone.ToString();
            txtAddress1.Text = blmanufacturer.Address1.ToString();
            txtAddress2.Text = blmanufacturer.Address2.ToString();
            txtAddress3.Text = blmanufacturer.Address3.ToString();
            blmanufacturer = null;
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "toastr.warning('" + ex.Message + "')", true); 
        }
    }
    protected void gvManufacturer_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            
            string confirmValue = Request.Form["confirm_value"];
            if (confirmValue.Length > 3)
                confirmValue = confirmValue.Substring(confirmValue.Length - 3, 3);
            if (confirmValue == "Yes")
            {
                long status = 0;
                BlManufacturer blmanufacturer = new BlManufacturer();
                blmanufacturer.MasterId = Convert.ToInt64(gvManufacturer.DataKeys[e.RowIndex]["ID_Manufacturer"].ToString());
                hfMasterID.Value = (gvManufacturer.DataKeys[e.RowIndex]["ID_Manufacturer"].ToString());
                blmanufacturer.FK_Client = Convert.ToInt64(hfClient.Value);
                blmanufacturer.UserCode = hfUser.Value;
                blmanufacturer = null;
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
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            
            SaveData();
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
   
    protected void gvManufacturer_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvManufacturer.PageIndex = e.NewPageIndex;
        GridFill(e.NewPageIndex);
    }
   

    protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
    {
        if (Convert.ToBoolean(hfIsAdd.Value) == false)
        {
            ((Label)CommonPopupPrivilege.FindControl("popuplabel")).Text = "User does not have permission to Add";
            mpPrivilege.Show();
        }
    }
    #endregion
}