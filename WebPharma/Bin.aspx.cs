using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebPharma.Business.Masters;

public partial class Bin : System.Web.UI.Page
{
    #region Functions
    public void Delete()
    {
        try
        {
            divPopupBin.Visible = true;
            Populate Populate = new Populate();
            CommonPopup1.CallingFunction = "PopupOnBin";
            ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "PopupOnBin();", true);
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
            BlBin blbin = new BlBin();
            blbin.UserCode = hfuserName.Value;
            blbin.MasterId = Convert.ToInt64(hfMasterID.Value);
            blbin.FK_Reason = Convert.ToInt64(Reason);
            if (hfIsDelete.Value.ToLower() == "true")
            {
                status = blbin.DeleteData();
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
            blbin = null;
        }
    }
    protected void GridFill(int currentPage)
    {
        BlBin blbin = new BlBin();
        DataTable dtbl = new DataTable();
        blbin.ShortName = txtSearchShortName.Text;
        blbin.BinName = txtSearchName.Text;

        blbin.FK_Client = Convert.ToInt64(hfClient.Value);
        blbin.UserCode = hfUser.Value;
        blbin.PageIndex = Convert.ToInt16(currentPage);
        dtbl = blbin.SelectAllData();
        grdBin.DataSource = dtbl;
        grdBin.DataBind();
      
        blbin = null;
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
        bluseraccess.FK_Pages = 42;
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
        BlBin blBin = new BlBin();
        blBin.ShortName = txtShortName.Text;
        blBin.BinName = txtBin.Text;
        blBin.FK_Client = Convert.ToInt64(hfClient.Value);
        blBin.UserCode =hfuserName.Value;
        blBin.FK_Rack = Convert.ToInt64(ddlRack.SelectedValue.ToString());
      
        if (hfBinID.Value == "0")
        {
            if (hfIsAdd.Value.ToLower() == "true")
            {
                status = blBin.InsertData();
                if (status > 0)
                {
                    ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "ShowAddDiv();", true);
                    ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "toastr.success('Data inserted successfully');ClearAdd();", true);
                    GridFill(1);
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
            blBin.MasterId = Convert.ToInt64(hfBinID.Value);
            if (hfIsUpdate.Value.ToLower() == "true")
            {
                status = blBin.UpdateData();
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
        blBin = null;
    }
    protected void BindData()
    {
        Populate populate = new Populate();
        populate.FillDropdownList(ddlRack, "Rack", "RackName", "ID_Rack", "RackName", " FK_Client=" + hfClient.Value+"AND Cancelled=0",  "");
    }
    #endregion Functions
    #region Events
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Page.Header.DataBind();
            //blMessage.Items.Clear();
            ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "ClearAdd();", true);
            divAdd.Style.Add("display", "block");
            hfUser.Value = Request.QueryString["uid"];
            hfClient.Value = Request.QueryString["clnt"];
            hfuserName.Value = Session["Username"].ToString();
            CheckUserAccess();
            GridFill(1);
            txtShortName.Focus();
            BindData();
            if (Convert.ToBoolean(hfIsAdd.Value) == false)
            {
                ((Label)CommonPopupPrivilege.FindControl("popuplabel")).Text = "User does not have permission to Add";
                mpPrivilege.Show();
            }
            if (Convert.ToBoolean(hfIsUpdate.Value) == false)
            {
                grdBin.Columns[4].Visible = false;
            }
            if (Convert.ToBoolean(hfIsDelete.Value) == false)
            {
                grdBin.Columns[5].Visible = false;
            }
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
    protected void lnkPage_Click(object sender, EventArgs e)
    {

    }
    protected void grdBin_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            // blMessage.Items.Clear();
            string confirmValue = Request.Form["confirm_value"];
            if (confirmValue.Length > 3)
                confirmValue = confirmValue.Substring(confirmValue.Length - 3, 3);
            if (confirmValue == "Yes")
            {
                long status = 0;
                BlBin blbin = new BlBin();
                blbin.MasterId = Convert.ToInt64(grdBin.DataKeys[e.RowIndex]["ID_Bin"].ToString());
                hfMasterID.Value = (grdBin.DataKeys[e.RowIndex]["ID_Bin"].ToString());
                blbin.FK_Client = Convert.ToInt64(hfClient.Value);
                blbin.UserCode = hfUser.Value;
                blbin = null;
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
    protected void grdBin_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        try
        {
            ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "ShowAddDiv();", true);
            BlBin blbin = new BlBin();
            blbin.MasterId = Convert.ToInt64(grdBin.DataKeys[e.NewSelectedIndex]["ID_Bin"].ToString());
            hfBinID.Value = blbin.MasterId.ToString();
            blbin.FK_Client = Convert.ToInt64(hfClient.Value);
            blbin.UserCode = hfUser.Value;
            blbin.SelectData();
            txtShortName.Text = blbin.ShortName.ToString();
            txtBin.Text = blbin.BinName.ToString();
            ddlRack.SelectedValue = blbin.FK_Rack.ToString();
            blbin = null;
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
            txtSearchName.Focus();
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
            //Licensing.GetLicense();
            //if (hfBinID.Value != "0")
            //{
            //    if (txtBin.Attributes["BinName"] !=  txtBin.Text)
            //    {
            //        Licensing.CheckBranchLicense("All");
            //    }
            //}
            //else
            //{
            //    Licensing.CheckBranchLicense("All");
            //}
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
   
    protected void grdBin_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdBin.PageIndex = e.NewPageIndex;
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
    #endregion Events
}