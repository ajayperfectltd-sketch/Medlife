using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebPharma.Business.Masters;

public partial class Generic : System.Web.UI.Page
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
            BlGeneric blGeneric = new BlGeneric();
            //blReason.MasterId = Convert.ToInt64(gvReason.DataKeys[e.RowIndex]["ID_Reason"].ToString());
            blGeneric.UserCode = hfuserName.Value;
            blGeneric.MasterId = Convert.ToInt64(hfMasterID.Value);
            blGeneric.FK_Reason = Convert.ToInt64(Reason);
            if (hfIsDelete.Value.ToLower() == "true")
            {
                status = blGeneric.DeleteData();
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
            blGeneric = null;
        }
    }
    protected void CheckUserAccess()
    {
        BlUserAccess bluseraccess = new BlUserAccess();
        Populate populate = new Populate();

        bluseraccess.FK_Users = Convert.ToInt64(hfUser.Value);
        bluseraccess.FK_Pages = 47;
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
    protected void GridFill(int currentPage)
    {
        BlGeneric blGeneric = new BlGeneric();
        DataTable dtbl = new DataTable();
        blGeneric.ShortName = txtSearchShortName.Text;
        blGeneric.GenericName = txtSearchName.Text;

        blGeneric.FK_Client = Convert.ToInt64(hfClient.Value);
        blGeneric.UserCode = hfUser.Value;
        blGeneric.PageIndex = Convert.ToInt16(currentPage);
        dtbl = blGeneric.SelectAllData();
        grdGeneric.DataSource = dtbl;
        grdGeneric.DataBind();
        blGeneric = null;
    }
    protected void SaveData()
    {
        long status = 0;
        BlGeneric blGeneric = new BlGeneric();
        blGeneric.ShortName = txtShortName.Text;
        blGeneric.GenericName = txtGeneric.Text;
        blGeneric.FK_Client = Convert.ToInt64(hfClient.Value);
        blGeneric.UserCode = hfuserName.Value;
        if (hfGenericID.Value == "0")
        {
            if (hfIsAdd.Value.ToLower() == "true")
            {
                status = blGeneric.InsertData();
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
            blGeneric.MasterId = Convert.ToInt64(hfGenericID.Value);
            if (hfIsUpdate.Value.ToLower() == "true")
            {
                status = blGeneric.UpdateData();
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
        blGeneric = null;
    }
    #endregion Functions
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
            if (Convert.ToBoolean(hfIsAdd.Value) == false)
            {
                ((Label)CommonPopupPrivilege.FindControl("popuplabel")).Text = "User does not have permission to Add";
                mpPrivilege.Show();
            }
            if (Convert.ToBoolean(hfIsUpdate.Value) == false)
            {
                grdGeneric.Columns[3].Visible = false;
            }
            if (Convert.ToBoolean(hfIsDelete.Value) == false)
            {
                grdGeneric.Columns[4].Visible = false;
            }
            
        }
    }
    protected void grdGeneric_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdGeneric.PageIndex = e.NewPageIndex;
        GridFill(e.NewPageIndex);
    }
    protected void grdGeneric_RowDeleting(object sender, GridViewDeleteEventArgs e)
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
                BlGeneric blGeneric = new BlGeneric();
                blGeneric.MasterId = Convert.ToInt64(grdGeneric.DataKeys[e.RowIndex]["ID_Generic"].ToString());
                hfMasterID.Value = (grdGeneric.DataKeys[e.RowIndex]["ID_Generic"].ToString());
                blGeneric.FK_Client = Convert.ToInt64(hfClient.Value);
                blGeneric.UserCode = hfUser.Value;
                blGeneric = null;
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
    protected void grdGeneric_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        try
        {
            ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "ShowAddDiv();", true);
            BlGeneric blGeneric = new BlGeneric();
            blGeneric.MasterId = Convert.ToInt64(grdGeneric.DataKeys[e.NewSelectedIndex]["ID_Generic"].ToString());
            hfGenericID.Value = blGeneric.MasterId.ToString();
            blGeneric.FK_Client = Convert.ToInt64(hfClient.Value);
            blGeneric.UserCode = hfUser.Value;
            blGeneric.SelectData();
            txtShortName.Text = blGeneric.ShortName.ToString();
            txtGeneric.Text = blGeneric.GenericName.ToString();
            blGeneric = null;
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
    protected void ImageButton2_Click(object sender, ImageClickEventArgs e)
    {
        Delete();

    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {

        try
        {
            //Licensing.GetLicense();
            //if (hfGenericID.Value != "0")
            //{
            //    if (txtGeneric.Attributes["GenericName"] != txtGeneric.Text)
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
    protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
    {
        if (Convert.ToBoolean(hfIsAdd.Value) == false)
        {
            ((Label)CommonPopupPrivilege.FindControl("popuplabel")).Text = "User does not have permission to Add";
            mpPrivilege.Show();
        }
        ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "ShowAddDiv();", true);
    }
}