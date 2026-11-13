using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebPharma.Business.Masters;

public partial class Doctor : System.Web.UI.Page
{

    #region Functions
    public void Delete()
    {
        try
        {
            divPopupDoctor.Visible = true;
            Populate Populate = new Populate();
            CommonPopup1.CallingFunction = "PopupOnDoctor";
            ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "PopupOnDoctor();", true);
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
            BlDoctor bldoctor = new BlDoctor();
            //blReason.MasterId = Convert.ToInt64(gvReason.DataKeys[e.RowIndex]["ID_Reason"].ToString());
            bldoctor.UserCode = hfuserName.Value;
            bldoctor.MasterId = Convert.ToInt64(hfMasterID.Value);
            bldoctor.Reason =Convert.ToInt64(Reason);
            if (hfIsDelete.Value.ToLower() == "true")
            {
                status = bldoctor.DeleteData();
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
                txtName.Focus();
            }
            bldoctor = null;
        }
    }
    protected void CheckUserAccess()
    {
        BlUserAccess bluseraccess = new BlUserAccess();
        Populate populate = new Populate();

        bluseraccess.FK_Users = Convert.ToInt64(hfUser.Value);
        bluseraccess.FK_Pages = 41;
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
        BlDoctor blDoctor = new BlDoctor();
        DataTable dtbl = new DataTable();
        blDoctor.DoctorName = txtSearchName.Text.Trim();
        blDoctor.ClinicName = txtClinicName.Text.Trim();
        blDoctor.FK_Client = Convert.ToInt64(hfClient.Value);
        blDoctor.PageIndex = Convert.ToInt16(currentPage);
        blDoctor.UserCode = hfUser.Value;
        dtbl = blDoctor.SelectAllData();
        grdDoctor.DataSource = dtbl;
        grdDoctor.DataBind();
        blDoctor = null;
    }
    protected void SaveData()
    {
        long status = 0;
        BlDoctor blDoctor = new BlDoctor();
        blDoctor.DoctorName = txtName.Text;
        blDoctor.ClinicName =  txtClinicName.Text;
        blDoctor.Qualification = (txtQualification.Text);
        blDoctor.FK_Client = Convert.ToInt64(hfClient.Value);
        blDoctor.UserCode = hfuserName.Value;
        if (hfDoctorId.Value == "0")
        {
            if (hfIsAdd.Value.ToLower() == "true")
            {
                status = blDoctor.InsertData();
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
                    txtName.Focus();
                }
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "toastr.warning('User does not have permission to add')", true);
                txtName.Focus();

            }
        }
        else
        {
            blDoctor.MasterId = Convert.ToInt64(hfDoctorId.Value);
            if (hfIsUpdate.Value.ToLower() == "true")
            {
                status = blDoctor.UpdateData();
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
                    txtName.Focus();
                }
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "toastr.warning('User does not have permission to Update')", true);
                txtName.Focus();
            }
        }
        blDoctor = null;
    }
    #endregion Functions
    #region Events
    protected void Page_Load(object sender, EventArgs e)
    {
        try
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
                txtName.Focus();
                if (Convert.ToBoolean(hfIsAdd.Value) == false)
                {
                    ((Label)CommonPopupPrivilege.FindControl("popuplabel")).Text = "User does not have permission to Add";
                    mpPrivilege.Show();
                }
                if (Convert.ToBoolean(hfIsUpdate.Value) == false)
                {
                    grdDoctor.Columns[3].Visible = false;
                }
                if (Convert.ToBoolean(hfIsDelete.Value) == false)
                {
                    grdDoctor.Columns[4].Visible = false;
                }
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
            txtSearchName.Focus();
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "toastr.warning('" + ex.Message + "')", true);
        }
    }
    protected void grdDoctor_RowDeleting(object sender, GridViewDeleteEventArgs e)
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
                blbin.MasterId = Convert.ToInt64(grdDoctor.DataKeys[e.RowIndex]["ID_Doctor"].ToString());
                hfMasterID.Value = (grdDoctor.DataKeys[e.RowIndex]["ID_Doctor"].ToString());
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
    protected void grdDoctor_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        try
        {
            ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "ShowAddDiv();", true);
            BlDoctor blDoctor = new BlDoctor();
            blDoctor.MasterId = Convert.ToInt64(grdDoctor.DataKeys[e.NewSelectedIndex]["ID_Doctor"].ToString());
            hfDoctorId.Value = blDoctor.MasterId.ToString();
            blDoctor.FK_Client = Convert.ToInt64(hfClient.Value);
            blDoctor.UserCode = hfUser.Value;
            blDoctor.SelectData();
            txtName.Text = blDoctor.DoctorName.ToString();
            txtQualification.Text = blDoctor.Qualification.ToString();
            txtClinicName.Text = blDoctor.ClinicName.ToString();
            blDoctor = null;
        }
        catch(Exception ex)
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
            txtSearchName.Focus();

        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "toastr.warning('" + ex.Message + "')", true);
        }
    }
    protected void lnkPage_Click(object sender, EventArgs e)
    {

    }
    
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            //Licensing.GetLicense();
            ////blMessage.Items.Clear();
            //if (hfDoctorId.Value != "0")
            //{
            //    if (txtName.Attributes["DoctorName"] != txtName.Text)
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
    
    protected void grdDoctor_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdDoctor.PageIndex = e.NewPageIndex;
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