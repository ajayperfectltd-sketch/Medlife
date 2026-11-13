/*----------------------------------------------------------------------------------------------------------------------------------------------------------
Purpose    : Insert/Update/Delete   Identity Proof
Author     :Bibin TC
Copyright  : Perfect Software Solutions (Clt) Pvt Ltd
Created on : 03/05/2018
----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------*/


using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebPharma.Business.Masters;
using System.Data;

public partial class IdentityProof : System.Web.UI.Page
{
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
            BlIdentityProof BlIdentity= new BlIdentityProof();
            BlIdentity.UserCode = hfuserName.Value;
            BlIdentity.MasterId = Convert.ToInt64(hfMasterID.Value);
            BlIdentity.FK_Reason = Convert.ToInt64(Reason);
            if (hfIsDelete.Value.ToLower() == "true")
            {
                status = BlIdentity.DeleteData();
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
            BlIdentity = null;
        }
    }
    public void GridFill(int currentPage)
    {
        BlIdentityProof blIdentity = new BlIdentityProof();
        DataTable dtbl = new DataTable();

        blIdentity.Name = txtSearchName.Text;
        blIdentity.PageSize = 10;
        currentPage = currentPage == 0 ? 1 : currentPage;
        blIdentity.PageIndex = Convert.ToInt16(currentPage);

        blIdentity.UserCode = hfUser.Value;
        blIdentity.FK_Client = Convert.ToInt64(hfClient.Value);

        dtbl = blIdentity.SelectAllData();
        grididentityproofs.DataSource = dtbl;
        grididentityproofs.DataBind();
       
        blIdentity = null;
    }
  
    protected void Page_Changed(object sender, EventArgs e)
    {
        try
        {
       
            int pageIndex = int.Parse((sender as LinkButton).CommandArgument);

            pageIndex = pageIndex == 0 ? 1 : pageIndex;
            GridFill(pageIndex);


        }
        catch (Exception ex)
        {
            
            Populate.WarningMsg(this, ex.Message);
        }
    }
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
                CheckUserAccess();
                txtIdpName.Focus();
                hfuserName.Value = Session["Username"].ToString();
                if (Convert.ToBoolean(hfIsAdd.Value) == false)
                {
                    ((Label)CommonPopupPrivilege.FindControl("popuplabel")).Text = "User does not have permission to Add";
                    mpPrivilege.Show();
                }
                if (Convert.ToBoolean(hfIsUpdate.Value) == false)
                {
                    grididentityproofs.Columns[7].Visible = false;
                }
                if (Convert.ToBoolean(hfIsDelete.Value) == false)
                {
                    grididentityproofs.Columns[8].Visible = false;
                }
            }
        }
        catch (Exception ex)
        {
          
            Populate.WarningMsg(this, ex.Message);
        }

    }
    protected void CheckUserAccess()
    {
        BlUserAccess bluseraccess = new BlUserAccess();
        Populate populate = new Populate();

        bluseraccess.FK_Users = Convert.ToInt64(hfUser.Value);
        bluseraccess.FK_Pages = 38;
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

   
    protected void imgbtnView_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            // blMessage.Items.Clear();
            divAdd.Style.Add("display", "none");
            divView.Style.Add("display", "block");
            ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "ClearAdd();", true);
            GridFill(1);
            txtSearchName.Focus();
        }
        catch (Exception ex)
        {
            // //blMessage.Items.Add(ex.Message);
            Populate.WarningMsg(this, ex.Message);
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
           
            Populate.WarningMsg(this, ex.Message);
        }

    }



    protected void SaveData()
    {
        long status = 0;

        BlIdentityProof blIdentity = new BlIdentityProof();
        blIdentity.Name = txtIdpName.Text.Trim();
        blIdentity.Type = Convert.ToInt64(ddlIdpType.SelectedValue);
        blIdentity.SortOrder = Int16.Parse(txtSortOrder.Text == "" ? "0" : txtSortOrder.Text.Trim());
        blIdentity.Descrption = txtIdpDescription.Text;
        blIdentity.AdressProof = chkAddressProof.Checked;
        blIdentity.IdProof = chkIdProof.Checked;
        blIdentity.UserCode = hfuserName.Value;
        blIdentity.FK_Client = Convert.ToInt64(hfClient.Value);
        if (hdfIdentityprrof.Value == string.Empty)
        {
            if (hfIsAdd.Value.ToLower() == "true")
            {
                status = blIdentity.InsertData();
                if (status > 0)
                {
                   
                 

                    // //blMessage.Items.Add("Data inserted successfully");
                    GridFill(1);
                    ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "ShowAddDiv();", true);

                    //  ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "toastr.success('Data inserted successfully');ClearAdd();", true);
                    Populate.InsertMsg(this);

                } 

                else
                {
                    ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "ShowAddDiv();", true);
                   
                    Populate.ErrorMessageBox(this, "Cannot insert data, Name already exists");
                    txtIdpName.Focus();
                }
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "toastr.warning('User does not have permission to add')", true);
                txtIdpName.Focus();

            }

        }
        else
        {
            blIdentity.MasterId = Convert.ToInt64(hdfIdentityprrof.Value);
            if (hfIsUpdate.Value.ToLower() == "true")
            {
               status = blIdentity.UpdateData();
                if (status > 0)
                {
                   GridFill(1);
                   
                    Populate.UpdateMsg(this);

                }

                else
                {
                    ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "ShowAddDiv();", true);
                   
                    Populate.ErrorMessageBox(this, "Cannot update data,Name already exists");
                    txtIdpName.Focus();
                }
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "toastr.warning('User does not have permission to Update')", true);
                txtIdpName.Focus();
            }
        }
        blIdentity = null;

    }
    protected void identityproofs_RowDeleting(object sender, GridViewDeleteEventArgs e)
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
                BlIdentityProof blIdentity = new BlIdentityProof();
                blIdentity.MasterId = Convert.ToInt64(grididentityproofs.DataKeys[e.RowIndex]["ID_IdentityProof"].ToString());
                hfMasterID.Value = (grididentityproofs.DataKeys[e.RowIndex]["ID_IdentityProof"].ToString());
                blIdentity.UserCode = hfuserName.Value;
                blIdentity.FK_Client = Convert.ToInt64(hfClient.Value);
                blIdentity = null;
            }
            else
            {
                mpCommon.Hide();
            }
        }
        catch (Exception ex)
        {
            // //blMessage.Items.Add(ex.Message);
            Populate.WarningMsg(this, ex.Message);
        }
    }
    protected void identityproofs_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        try
        {
            //blMessage.Items.Clear();
            ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "ShowAddDiv();", true);
            BlIdentityProof blIdentity = new BlIdentityProof();
            blIdentity.MasterId = Convert.ToInt64(grididentityproofs.DataKeys[e.NewSelectedIndex]["ID_IdentityProof"].ToString());

            hdfIdentityprrof.Value = blIdentity.MasterId.ToString();

            blIdentity.UserCode = hfUser.Value;
            blIdentity.FK_Client = Convert.ToInt64(hfClient.Value);
           

            blIdentity.SelectData();


            txtIdpName.Text = blIdentity.Name;
            ddlIdpType.SelectedValue = Convert.ToString(blIdentity.Type);

            txtSortOrder.Text = blIdentity.SortOrder.ToString();
            txtIdpDescription.Text = blIdentity.Descrption;
            chkAddressProof.Checked = blIdentity.AdressProof ;
            chkIdProof.Checked = blIdentity.IdProof;


            blIdentity = null;
        }
        catch (Exception ex)
        {
            // //blMessage.Items.Add(ex.Message);
            Populate.WarningMsg(this, ex.Message);
        }
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        try
        {
            //blMessage.Items.Clear();

            GridFill(1);
            txtSearchName.Focus();
        }
        catch (Exception ex)
        {
            // //blMessage.Items.Add(ex.Message);
            Populate.WarningMsg(this, ex.Message);
        }
    }


    protected void grididentityproofs_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grididentityproofs.PageIndex = e.NewPageIndex;
        GridFill(e.NewPageIndex);

    }
    protected void ImageButton2_Click(object sender, ImageClickEventArgs e)
    {
        Delete();
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