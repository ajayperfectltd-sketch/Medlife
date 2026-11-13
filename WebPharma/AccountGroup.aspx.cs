/*----------------------------------------------------------------------------------------------------------------------------------------------------------
Purpose    : Insert/Update/Delete Account Group 
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
using WebPharma.Business;

public partial class AccountGroup : System.Web.UI.Page
{
    #region Function
    public void Delete()
    {
        try
        {
            divPopupAccountGroup.Visible = true;
            Populate Populate = new Populate();
            CommonPopup1.CallingFunction = "PopupOnAccountGroup";
            ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "PopupOnAccountGroup();", true);
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
            BlAccountGroup BlAccountGroup = new BlAccountGroup();
            BlAccountGroup.UserCode = hfuserName.Value;
            BlAccountGroup.MasterId = Convert.ToInt64(hfMasterID.Value);
            BlAccountGroup.FK_Reason = Convert.ToInt64(Reason);
          //  BlAccountGroup.FK_Client = Convert.ToInt64(Reason);
            if (hfIsDelete.Value.ToLower() == "true")
            {
                status = BlAccountGroup.DeleteData();
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
            BlAccountGroup = null;
        }
    }
    public void GridFill(int currentPage) 
    {
        BlAccountGroup blAcc = new BlAccountGroup();
        DataTable dtbl = new DataTable();
        blAcc.ShortName = txtSearchShortName.Text;
        blAcc.Name = txtSearchName.Text;
        blAcc.PageSize = 10;
        currentPage = currentPage == 0 ? 1 : currentPage;
        blAcc.PageIndex = Convert.ToInt16(currentPage);
       
        blAcc.UserCode = hfUser.Value;
        blAcc.FK_Client = Convert.ToInt64(hfClient.Value);

        dtbl = blAcc.SelectAllData();
        gvAccountGroup.DataSource = dtbl;
        gvAccountGroup.DataBind();
        blAcc = null;
    }


    
    protected void SaveData()
    {
        long status = 0;
        BlAccountGroup blAcc = new BlAccountGroup();
        blAcc.Name = txtName.Text.Trim();
        blAcc.AccountType = ddlAgAccountType.SelectedValue;

        
        blAcc.GroupType = ddlGroupType.SelectedValue;


        blAcc.SortOrder = int.Parse(txtSortOrder.Text == "" ? "0" : txtSortOrder.Text.Trim());
        blAcc.UserCode = hfuserName.Value;
        blAcc.FK_Client = Convert.ToInt64(hfClient.Value);
        if (hdfAccountGroup.Value == string.Empty)
        {
            if (hfIsAdd.Value.ToLower() == "true")
            {
                status = blAcc.InsertData();
                if (status > 0)
                {
                    // //blMessage.Items.Add("Data inserted successfully");
                    GridFill(1);
                    ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "ShowAddDiv();", true);
                    ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "toastr.success('Data inserted successfully');ClearAdd();", true);
                    


                }

                else
                {
                    ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "ShowAddDiv();", true);
                    // //blMessage.Items.Add("Cannot insert data, ShortName/Name/Sort order already exists");
                    //ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "toastr.error('Cannot insert data, ShortName/Name already exists')", true);
                    Populate.ErrorMessageBox(this, "Cannot insert data, ShortName/Name already exists");
                    
                }
            }
            else 
            {
                ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "toastr.warning('User does not have permission to add')", true);
                
            
            }

        }
        else
        {
            blAcc.MasterId = Convert.ToInt64(hdfAccountGroup.Value);
            if (hfIsUpdate.Value.ToLower() == "true")
            {
                status = blAcc.UpdateData();
                if (status > 0)
                {
                    GridFill(1);
                    // //blMessage.Items.Add("Data updated successfully");
                    // ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "ClearAdd();", true);
                    ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "ShowAddDiv();", true);
                    ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "toastr.success('Data updated successfully');ClearAdd();", true);
                  

                }

                else
                {
                    ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "ShowAddDiv();", true);
                    // //blMessage.Items.Add("Cannot update data, ShortName/Name/Sort order already exists");
                    // ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "toastr.error('Cannot update data, ShortName/Name already exists')", true);    
                    Populate.ErrorMessageBox(this, "Cannot update data, ShortName/Name already exists");
                    
                }
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "toastr.warning('User does not have permission to Update')", true);
               
            }
        }
        blAcc = null;

    }

    protected void CheckUserAccess()
    {
        BlUserAccess bluseraccess = new BlUserAccess();
        Populate populate = new Populate();

        bluseraccess.FK_Users = Convert.ToInt64(hfUser.Value);
        bluseraccess.FK_Pages = 4;
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
       // blMessage.Items.Clear();
        try
        {
           
            if (!IsPostBack)
            {
                BlFunction blfunctions = new BlFunction();
                Int16 type = Convert.ToInt16(blfunctions.GetScalarValue("Settings", "isnull(( select Value", "Name='SEC06'),0)", ""));
                ViewState["type"] = type.ToString();
                if (type > 0)
                {


                    ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "toastr.warning('This Facilities Only Available In SCORE');", true);
                    btnSubmit.Enabled = false;
                    btnSearch.Enabled = false;
                    btnClear.Enabled = false;
                    imgbtnView.Enabled = false;
                
                }

                else
                {
                    btnSubmit.Enabled = true;
                    btnSearch.Enabled = true;
                    btnClear.Enabled = true;
                    imgbtnView.Enabled = true;
                }

                    Page.Header.DataBind();
                    ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "ClearAdd();", true);
                    divAdd.Style.Add("display", "block");
                    hfuserName.Value = Session["Username"].ToString();
                    hfUser.Value = Request.QueryString["uid"];
                    hfClient.Value = Request.QueryString["clnt"];
                    CheckUserAccess();
                    
                
            }
        }
        catch (Exception ex)
        {
            // //blMessage.Items.Add(ex.Message);
            Populate.WarningMsg(this,ex.Message);
        }



    }
    protected void Page_Changed(object sender, EventArgs e)
    {
        try
        {
           // blMessage.Items.Clear();
            int pageIndex = int.Parse((sender as LinkButton).CommandArgument);

            pageIndex = pageIndex == 0 ? 1 : pageIndex;
            GridFill(pageIndex);


      
           
         
         //   BlAccountGroup blAcc = new BlAccountGroup();
        
          

          
        
            //blAcc = null;

        }
        catch (Exception ex)
        {
            // //blMessage.Items.Add(ex.Message);
            Populate.WarningMsg(this, ex.Message);
        }
    }

    protected void imgbtnView_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
           // blMessage.Items.Clear();
            divAdd.Style.Add("display", "none");
            divView.Style.Add("display", "block");
            ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "ClearAdd();", true);
            ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "ClearSearch();", true);
            GridFill(1);
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
           // blMessage.Items.Clear();
            SaveData();
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
            txtSearchShortName.Focus();
        }
        catch (Exception ex)
        {
            // //blMessage.Items.Add(ex.Message);
            Populate.WarningMsg(this, ex.Message);
        }
    }


    protected void gvAccountGroup_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        //ID_AccountGroup
        try
        {
            //blMessage.Items.Clear();
            ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "ShowAddDiv();", true);
            BlAccountGroup blAcc = new BlAccountGroup();
            blAcc.MasterId = Convert.ToInt64(gvAccountGroup.DataKeys[e.NewSelectedIndex]["ID_AccountGroup"].ToString());

            hdfAccountGroup.Value = blAcc.MasterId.ToString();

            blAcc.UserCode = hfUser.Value;
            blAcc.FK_Client = Convert.ToInt64(hfClient.Value);

            blAcc.SelectData();
            
            txtName.Text = blAcc.Name;
            ddlAgAccountType.SelectedValue = blAcc.AccountType;
            if (ddlAgAccountType.SelectedValue == "I" || ddlAgAccountType.SelectedValue == "E")
            {
               
                ddlGroupType.Enabled = false;
            }
            else
            {
                
                ddlGroupType.Enabled = true;
            }
            ddlGroupType.SelectedValue = blAcc.GroupType;
            txtSortOrder.Text = blAcc.SortOrder.ToString();

            blAcc = null;
        }
        catch (Exception ex)
        {
            // //blMessage.Items.Add(ex.Message);
            Populate.WarningMsg(this, ex.Message);
        }
    }
    protected void gvAccountGroup_RowDeleting(object sender, GridViewDeleteEventArgs e)
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
                BlAccountGroup blAcc = new BlAccountGroup();
                blAcc.MasterId = Convert.ToInt64(gvAccountGroup.DataKeys[e.RowIndex]["ID_AccountGroup"].ToString());
                hfMasterID.Value = (gvAccountGroup.DataKeys[e.RowIndex]["ID_AccountGroup"].ToString());
                blAcc.UserCode = hfUser.Value;
                blAcc.FK_Client = Convert.ToInt64(hfClient.Value);
                blAcc = null;
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
        
    #endregion
   
   

    protected void ddlAgAccountType_SelectedIndexChanged(object sender, EventArgs e)
    {
      //  ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "ShowAddDiv();", true);
        //if (ddlAgAccountType.SelectedValue == "I" || ddlAgAccountType.SelectedValue == "E")
        //{
        //    ddlGroupType.SelectedValue = "P";
        //    ddlGroupType.Enabled=false;
        //}
        //else
        //{
        //    ddlGroupType.SelectedValue = "0";
        //     ddlGroupType.Enabled=true;
        //}

        divAdd.Style.Add("display", "block");
      
    }
    protected void gvAccountGroup_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvAccountGroup.PageIndex = e.NewPageIndex;
        GridFill(e.NewPageIndex); 
    }
    protected void ImageButton2_Click(object sender, ImageClickEventArgs e)
    {
        Delete();
    }
}