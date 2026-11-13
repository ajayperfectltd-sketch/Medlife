using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebPharma.Business.Masters;

public partial class SubUnit : System.Web.UI.Page
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
            BlSubUnit blsubUnit = new BlSubUnit();
            //blReason.MasterId = Convert.ToInt64(gvReason.DataKeys[e.RowIndex]["ID_Reason"].ToString());
            blsubUnit.UserCode = hfuserName.Value;
            blsubUnit.MasterId = Convert.ToInt64(hfMasterID.Value);
            blsubUnit.FK_Reason = Convert.ToInt64(Reason);
            if (hfIsDelete.Value.ToLower() == "true")
            {
                status = blsubUnit.DeleteData();
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
                txtSearchName.Focus();
            }
            blsubUnit = null;
        }
    }
  

    public void GridFill(int currentPage)
    {
        WebPharma.Business.Masters.BlSubUnit Blsubunit = new WebPharma.Business.Masters.BlSubUnit();
        DataTable dtbl = new DataTable();
        Blsubunit.Name = txtSearchName.Text.Trim();
        Blsubunit.PageSize = 10;
        currentPage = currentPage == 0 ? 1 : currentPage;
        Blsubunit.PageIndex = Convert.ToInt16(currentPage);
        Blsubunit.FK_Client = Convert.ToInt64(hfClient.Value);
        Blsubunit.UserCode = hfUser.Value;

        dtbl = Blsubunit.SelectAllData();
        gvSubUnits.DataSource = dtbl;
        gvSubUnits.DataBind();

    }

    protected void CheckUserAccess()
    {
        BlUserAccess bluseraccess = new BlUserAccess();
        Populate populate = new Populate();

        bluseraccess.FK_Users = Convert.ToInt64(hfUser.Value);
        bluseraccess.FK_Pages = 48;
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
        WebPharma.Business.Masters.BlSubUnit Blsubunit = new WebPharma.Business.Masters.BlSubUnit();
        Blsubunit.Name = txtName.Text;
        Blsubunit.NoofUnits = int.Parse(txtNofUnits.Text == "" ? "0" : txtNofUnits.Text);
        Blsubunit.UserCode = hfuserName.Value;
        Blsubunit.FK_Client = Convert.ToInt64(hfClient.Value);
        if (hdfUnitId.Value == string.Empty)
        {
            if (hfIsAdd.Value.ToLower() == "true")
            {
                status = Blsubunit.InsertData();
                if (status > 0)
                {
                    GridFill(1);

                    Populate.InsertMsg(this);
                }
                if (status == -3)
                {
                    Populate.ErrorMessageBox(this, "Cannot insert data by base unit,  referance exists");
                    txtName.Focus();
                }

                else
                {


                    Populate.ErrorMessageBox(this, "Cannot insert data, ShortName/Name already exists");
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
            Blsubunit.MasterId = Convert.ToInt64(hdfUnitId.Value);
            if (hfIsUpdate.Value.ToLower() == "true")
            {

                status = Blsubunit.UpdateDate();
                if (status > 0)
                {
                    GridFill(1);
                    Populate.UpdateMsg(this);



                }
                if (status == -3)
                {
                    Populate.ErrorMessageBox(this, "Cannot update data,  referance exists");

                    txtName.Focus();
                }
                else
                {

                    Populate.ErrorMessageBox(this, " Cannot update data, ShortName/Name already exists");

                    txtName.Focus();
                }
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "toastr.warning('User does not have permission to Update')", true);
                txtName.Focus();
            }
        }
        Blsubunit = null;

    }
    #endregion
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
           
            if (!IsPostBack)
            {

                hfUser.Value = Request.QueryString["uid"];
                hfClient.Value = Request.QueryString["clnt"];


                Page.Header.DataBind();
                //blMessage.Items.Add.Clear();
                ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "ClearAdd();", true);
                divAdd.Style.Add("display", "block");
                CheckUserAccess();
                hfuserName.Value = Session["Username"].ToString();
                GridFill(1);
                txtName.Focus();
                if (Convert.ToBoolean(hfIsAdd.Value) == false)
                {
                    ((Label)CommonPopupPrivilege.FindControl("popuplabel")).Text = "User does not have permission to Add";
                    mpPrivilege.Show();
                }
                if (Convert.ToBoolean(hfIsUpdate.Value) == false)
                {
                    gvSubUnits.Columns[2].Visible = false;
                }
                if (Convert.ToBoolean(hfIsDelete.Value) == false)
                {
                    gvSubUnits.Columns[3].Visible = false;
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
            //blMessage.Items.Add.Clear();
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
    protected void ImageButton2_Click(object sender, ImageClickEventArgs e)
    {
        Delete();
    }
    protected void gvSubUnits_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            //blMessage.Items.Add.Clear();
            string confirmValue = Request.Form["confirm_value"];
            if (confirmValue.Length > 3)
                confirmValue = confirmValue.Substring(confirmValue.Length - 3, 3);
            if (confirmValue == "Yes")
            {
                long status = 0;
                BlUnit blunit = new BlUnit();
                blunit.MasterId = Convert.ToInt64(gvSubUnits.DataKeys[e.RowIndex]["ID_SubUnits"].ToString());
                hfMasterID.Value = (gvSubUnits.DataKeys[e.RowIndex]["ID_SubUnits"].ToString());
                blunit.FK_Client = Convert.ToInt64(hfClient.Value);
                blunit.UserCode = hfUser.Value;
                blunit = null;
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
    protected void gvSubUnits_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        try
        {
            //blMessage.Items.Add.Clear();
            ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "ShowAddDiv();", true);
            BlSubUnit blsubunit = new BlSubUnit();
            blsubunit.MasterId = Convert.ToInt64(gvSubUnits.DataKeys[e.NewSelectedIndex]["ID_SubUnits"].ToString());
            hdfUnitId.Value = blsubunit.MasterId.ToString();
            blsubunit.UserCode = hfUser.Value;
            blsubunit.FK_Client = Convert.ToInt64(hfClient.Value);
            blsubunit.SelectData();
            txtName.Text = blsubunit.Name;
            txtNofUnits.Text = blsubunit.NoofUnits.ToString();
            blsubunit = null;
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
            //blMessage.Items.Add.Clear();
            GridFill(1);
            txtName.Focus();
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
    protected void gvSubUnits_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvSubUnits.PageIndex = e.NewPageIndex;
        GridFill(e.NewPageIndex);
    }
    protected void imgbtnAdd_Click(object sender, ImageClickEventArgs e)
    {
        if (Convert.ToBoolean(hfIsAdd.Value) == false)
        {
            ((Label)CommonPopupPrivilege.FindControl("popuplabel")).Text = "User does not have permission to Add";
            mpPrivilege.Show();
        }
        ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "ShowAddDiv();", true);
    }
}