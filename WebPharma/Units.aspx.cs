

/*----------------------------------------------------------------------------------------------------------------------------------------------------------
Purpose    : Insert/Update/Delete Unit 
Author     :Bibin TC
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
public partial class Units : System.Web.UI.Page
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
            BlUnit blUnit = new BlUnit();
            //blReason.MasterId = Convert.ToInt64(gvReason.DataKeys[e.RowIndex]["ID_Reason"].ToString());
            blUnit.UserCode = hfuserName.Value;
            blUnit.MasterId = Convert.ToInt64(hfMasterID.Value);
            blUnit.FK_Reason = Convert.ToInt64(Reason);
            if (hfIsDelete.Value.ToLower() == "true")
            {
                status = blUnit.DeleteData();
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
            blUnit = null;
        }
    }
    protected void BindData()
    {
        Populate populate = new Populate();
        populate.FillDropdownList(ddlCategoryFirst, "Category", "CategoryName", "ID_Category", "ID_Category", "FK_Client = " + hfClient.Value, "");
        populate.FillDropdownList(ddlCtaegorySearch, "Category", "CategoryName", "ID_Category", "ID_Category", "FK_Client = " + hfClient.Value, "");
        populate = null;
    }

    public void GridFill(int currentPage)
    {
        WebPharma.Business.Masters.BlUnit Blunit = new WebPharma.Business.Masters.BlUnit();
        DataTable dtbl = new DataTable();
        Blunit.Name = txtSearchName.Text.Trim();
        Blunit.PageSize = 10;
        currentPage = currentPage == 0 ? 1 : currentPage;
        Blunit.PageIndex =Convert.ToInt16(currentPage);
        Blunit.FK_Client = Convert.ToInt64(hfClient.Value);
        Blunit.UserCode = hfUser.Value;

        dtbl = Blunit.SelectAllData();
        gvUnits.DataSource = dtbl;
        gvUnits.DataBind();

    }

    protected void CheckUserAccess()
    {
        BlUserAccess bluseraccess = new BlUserAccess();
        Populate populate = new Populate();

        bluseraccess.FK_Users = Convert.ToInt64(hfUser.Value);
        bluseraccess.FK_Pages = 9;
        bluseraccess.FK_Client = Convert.ToInt64(hfClient.Value);
        bluseraccess.CheckUserAccess();
        //if (Convert.ToBoolean(bluseraccess.IsView) == false)
        //{
           // Response.Redirect("Home.aspx?");

        //}

        hfIsAdd.Value = (Convert.ToBoolean(bluseraccess.IsAdd)).ToString();
        hfIsUpdate.Value = (Convert.ToBoolean(bluseraccess.IsUpdate)).ToString();
        hfIsDelete.Value = (Convert.ToBoolean(bluseraccess.IsDelete)).ToString();
        bluseraccess = null;
    }

    protected void SaveData()
    {
        long status = 0;
        WebPharma.Business.Masters.BlUnit Blunit = new WebPharma.Business.Masters.BlUnit();
        Blunit.Name = txtName.Text;
        Blunit.NoofUnits = int.Parse(txtNofUnits.Text == "" ? "0" : txtNofUnits.Text);
        Blunit.UserCode = hfuserName.Value;
        Blunit.FK_Client = Convert.ToInt64(hfClient.Value);

        Blunit.BaseUnit = cbxIsBaseUnit.Checked;
        if (hdfUnitId.Value == string.Empty)
        {
            if (hfIsAdd.Value.ToLower() == "true")
            {
                status = Blunit.InsertData();
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
            Blunit.MasterId = Convert.ToInt64(hdfUnitId.Value);
            if (hfIsUpdate.Value.ToLower() == "true")
            {

                status = Blunit.UpdateDate();
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
        Blunit = null;

    }
    #endregion
    #region Events
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
             //blMessage.Items.Add.Clear();
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
                BindData();
                GridFill(1);
                txtName.Focus();
                if (Convert.ToBoolean(hfIsAdd.Value) == false)
                {
                    ((Label)CommonPopupPrivilege.FindControl("popuplabel")).Text = "User does not have permission to Add";
                    mpPrivilege.Show();
                }
                if (Convert.ToBoolean(hfIsUpdate.Value) == false)
                {
                    gvUnits.Columns[2].Visible = false;
                }
                if (Convert.ToBoolean(hfIsDelete.Value) == false)
                {
                    gvUnits.Columns[3].Visible = false;
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
    protected void Page_Changed(object sender, EventArgs e)
    {
        try
        {
             //blMessage.Items.Add.Clear();
            int pageIndex = int.Parse((sender as LinkButton).CommandArgument);

            DataTable dtbl = new DataTable();
            BlUnit blunit = new BlUnit();

            blunit.PageSize = 10;
            blunit.PageIndex = Convert.ToInt16(pageIndex);
            GridFill(blunit.PageIndex);


            blunit = null;
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
             //blMessage.Items.Add.Clear();
            SaveData();
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "toastr.warning('" + ex.Message + "')", true); 
        }
    }
    protected void gvUnits_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        try
        {
             //blMessage.Items.Add.Clear();
            ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "ShowAddDiv();", true);
            BlUnit blunit = new BlUnit();
            blunit.MasterId = Convert.ToInt64(gvUnits.DataKeys[e.NewSelectedIndex]["ID_Units"].ToString());
            hdfUnitId.Value = blunit.MasterId.ToString();
            blunit.UserCode = hfUser.Value;
            blunit.FK_Client = Convert.ToInt64(hfClient.Value);         
            blunit.SelectData();
            txtName.Text = blunit.Name;
            txtNofUnits.Text = blunit.NoofUnits.ToString();
            blunit = null;
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "toastr.warning('" + ex.Message + "')", true); 
        }

    }
    protected void gvUnits_RowDeleting(object sender, GridViewDeleteEventArgs e)
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
                blunit.MasterId = Convert.ToInt64(gvUnits.DataKeys[e.RowIndex]["ID_Units"].ToString());
                hfMasterID.Value = (gvUnits.DataKeys[e.RowIndex]["ID_Units"].ToString());
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
   
    protected void ImageButton2_Click(object sender, ImageClickEventArgs e)
    {
        Delete();
    }
   
    protected void gvUnits_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {

        gvUnits.PageIndex = e.NewPageIndex;
        GridFill(e.NewPageIndex);
    }
    #endregion
    protected void imgbtnAdd_Click(object sender, ImageClickEventArgs e)
    {
        if (Convert.ToBoolean(hfIsAdd.Value) == false)
        {
            ((Label)CommonPopupPrivilege.FindControl("popuplabel")).Text = "User does not have permission to Add";
            mpPrivilege.Show();
        }
        divAdd.Style.Add("display", "block");
        divView.Style.Add("display", "none");
    }
}