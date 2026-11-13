/*----------------------------------------------------------------------------------------------------------------------------------------------------------
Purpose    : Insert/Update/Delete Store  
Author     : Bibin TC
Copyright  : Perfect Software Solutions (Clt) Pvt Ltd
Created on : 03/05/2018
----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
WebPharma
On			By			OMID
----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
 
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebPharma.Business.Masters;
using System.Data;

public partial class Store : System.Web.UI.Page
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
            BlStore blStore = new BlStore();
            blStore.UserCode = hfuserName.Value;
            blStore.MasterId = Convert.ToInt64(hfMasterID.Value);
            blStore.FK_Reason = Convert.ToInt64(Reason);
            if (hfIsDelete.Value.ToLower() == "true")
            {
                status = blStore.DeleteData();
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
            blStore = null;
        }
    }
    public void SetReadonly(params TextBox[] textbox)
    {
        foreach (TextBox txt in textbox)
            txt.Attributes.Add("readonly", "readonly");

    }
    protected void BindData()
    {
        Populate populate = new Populate();

        populate.FillDropdownListSelect(ddlAgent, "Customer C join Agent A on A.FK_Customer=C.ID_Customer", "C.CustomerName", "A.ID_Agent", "ID_Agent", " A.Cancelled=0  and A.Passed=1 AND FK_Client= " + hfClient.Value.Split(',')[0], "");
        populate = null;
    }
    protected void SetControls()
    {
       // txtDob.Attributes.Add("readOnly", "readOnly");
    }
    protected void branchbind()
    {
        BlStore blStore = new BlStore();
        DataTable dt = new DataTable();

        dt=blStore.bindBranch();
        ddlbranchcode.DataSource = dt;
        ddlbranchcode.DataTextField = "BranchName";
        ddlbranchcode.DataValueField = "ID";
        ddlbranchcode.DataBind();
    }
    protected void selectaccountlink()
    {
        BlStore blStore = new BlStore();
        DataTable dt = new DataTable();

        dt = blStore.selectaccountlink();
        if (dt.Rows[0]["valuees"].ToString() == "0")
        {
            txtbranchcode.Visible = true;
            ddlbranchcode.Visible = false;
        }
        else
        {
            txtbranchcode.Visible = false;
            ddlbranchcode.Visible = true;
        }
        //  populate.FillDropdownList(ddlbranchcode, "Branch", "ShortName+' - '+Name", "ID_SalesMan", "ID_SalesMan", "", "");
        //opulate = null;
    }
    
    protected void GridFill(int currentPage)
    {
        BlStore blStore = new BlStore();
        DataTable dtbl = new DataTable();
        blStore.Shortname = txtSearchShortName.Text.Trim();
        blStore.Name = txtSearchName.Text.Trim();
        blStore.FK_Client = Convert.ToInt64(hfClient.Value);
        blStore.PageIndex = Convert.ToInt16(currentPage);
        blStore.UserCode = hfUser.Value;
        dtbl = blStore.SelectAllData();
        gvStore.DataSource = dtbl;
        gvStore.DataBind();
        if (dtbl.Rows.Count > 0)
        {
            //int recordCount = Convert.ToInt32(dtbl.Rows[0]["RecordCount"].ToString());
           // this.PopulatePager(recordCount, currentPage);
        }
        blStore = null;
    }
    protected void CheckUserAccess()
    {
        BlUserAccess bluseraccess = new BlUserAccess();
        Populate populate = new Populate();

        bluseraccess.FK_Users = Convert.ToInt64(hfUser.Value);
        bluseraccess.FK_Pages = 7;
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
        BlStore blStore = new BlStore();
        blStore.Shortname = txtShortName.Text;
        blStore.Name = txtName.Text;
        blStore.StartDate = Convert.ToDateTime(txtDate.Text);
        blStore.OpeningBalance = Convert.ToDecimal(txtopeningbalance.Text == "" ? "0" : txtopeningbalance.Text);
        blStore.OpeningLiability = Convert.ToDecimal(txtOpeningliabitlty.Text == "" ? "0" : txtOpeningliabitlty.Text);
        blStore.BranchType = Convert.ToInt16(ddlType.SelectedValue.ToString());
        blStore.BranchCode=Convert.ToInt16(txtbranchcode.Text== "" ? "0" : txtbranchcode.Text);
        blStore.FK_Client = Convert.ToInt64(hfClient.Value);
        blStore.UserCode = hfuserName.Value;
        blStore.Address1 = txtAddress1.Text;
        blStore.Address2 = txtAddress2.Text;
        blStore.Address3 = txtAddress3.Text;
        blStore.Phone = txtPhone.Text;
        blStore.Email = txtEmail.Text;
        blStore.Website = txtWebsite.Text;
        blStore.Active = Convert.ToBoolean(chkActive.Checked);
        blStore.FK_Agent = Convert.ToInt64(ddlAgent.SelectedValue == "" ? "0" : ddlAgent.SelectedValue);

        if (Session["name"].ToString() == "pss")
        {
            blStore.BranchCode = Convert.ToInt16(ddlbranchcode.SelectedValue.ToString());
        }
        else
        {
        
            blStore.BranchCode = Convert.ToInt16(txtbranchcode.Text == "" ? "0" : txtbranchcode.Text);
        }

        if (hfStoreId.Value == "0")
        {
            if (hfIsAdd.Value.ToLower() == "true")
            {
                status = blStore.InsertData();
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
            blStore.MasterId = Convert.ToInt64(hfStoreId.Value);
            if (hfIsUpdate.Value.ToLower() == "true")
            {
                status = blStore.UpdateData();
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
        blStore = null;
    }
    #endregion

    #region Events
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
              
                if (Session["name"].ToString() == "pss")
                {
                    
                   
                    branchbind();
                    selectaccountlink();
                    txtbranchcode.Visible = false;
                    ddlbranchcode.Visible = true;
                   

                }
                else
                {
                    txtbranchcode.Visible = true;
                    ddlbranchcode.Visible = false;
                }
                Page.Header.DataBind();
              
                ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "ClearAdd();", true);
                divAdd.Style.Add("display", "block");
                hfUser.Value = Request.QueryString["uid"];
                hfClient.Value = Request.QueryString["clnt"];
                hfuserName.Value = Session["Username"].ToString();
                CheckUserAccess();
                BindData();
                GridFill(1);
                SetControls();
                txtShortName.Focus();
                SetReadonly(txtDate);
                if (Convert.ToBoolean(hfIsAdd.Value) == false)
                {
                    ((Label)CommonPopupPrivilege.FindControl("popuplabel")).Text = "User does not have permission to Add";
                    mpPrivilege.Show();
                }
                if (Convert.ToBoolean(hfIsUpdate.Value) == false)
                {
                    gvStore.Columns[5].Visible = false;
                }
                if (Convert.ToBoolean(hfIsDelete.Value) == false)
                {
                    gvStore.Columns[6].Visible = false;
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
        
        int pageIndex = int.Parse((sender as LinkButton).CommandArgument);

        GridFill(pageIndex);
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
    protected void gvStore_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        try
        {
           
            ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "ShowAddDiv();", true);
            BlStore blStore = new BlStore();
            blStore.MasterId = Convert.ToInt64(gvStore.DataKeys[e.NewSelectedIndex]["ID_Store"].ToString());
            hfStoreId.Value = blStore.MasterId.ToString();
            blStore.FK_Client = Convert.ToInt64(hfClient.Value);
            blStore.UserCode = hfUser.Value;
            blStore.SelectData();
            txtShortName.Text = blStore.Shortname.ToString();
            txtopeningbalance.Text = blStore.OpeningBalance.ToString();
            txtName.Text = blStore.Name.ToString();
            txtName.Attributes["Name"] = blStore.Name.ToString();
            ddlType.SelectedValue = blStore.BranchType.ToString();
            txtDate.Text = Convert.ToDateTime(blStore.StartDate.ToString()).ToShortDateString();
            txtopeningbalance.Text = Convert.ToDecimal(blStore.OpeningBalance).ToString();
            txtOpeningliabitlty.Text = Convert.ToDecimal(blStore.OpeningLiability).ToString();
            txtAddress1.Text = blStore.Address1.ToString();
            txtAddress2.Text = blStore.Address2.ToString();
            txtAddress3.Text = blStore.Address3.ToString();
            txtPhone.Text=blStore.Phone.ToString();
            txtEmail.Text = blStore.Email.ToString();
            txtWebsite.Text = blStore.Website.ToString();
            chkActive.Checked =blStore.Active;
            if (Session["name"].ToString() == "pss")
            {
              
                ddlbranchcode.SelectedValue = blStore.BranchCode.ToString();
               

            }


            else

            {
                txtbranchcode.Text = blStore.BranchCode.ToString();

            }
            BindData();
            ddlAgent.SelectedValue = blStore.FK_Agent.ToString();
          
            blStore = null;
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "toastr.warning('" + ex.Message + "')", true); 
        }
    }
    protected void gvStore_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
          
            string confirmValue = Request.Form["confirm_value"];
            if (confirmValue.Length > 3)
                confirmValue = confirmValue.Substring(confirmValue.Length - 3, 3);
            if (confirmValue == "Yes")
            {
                long status = 0;
                BlStore blStore = new BlStore();
                blStore.MasterId = Convert.ToInt64(gvStore.DataKeys[e.RowIndex]["ID_Store"].ToString());
                hfMasterID.Value = (gvStore.DataKeys[e.RowIndex]["ID_Store"].ToString());
                blStore.FK_Client = Convert.ToInt64(hfClient.Value);
                blStore.UserCode = hfUser.Value;
                blStore = null;
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
           // Licensing.GetLicense();
            //if (hfStoreId.Value != "0")
            //{
            //    if (txtName.Attributes["Name"] != txtName.Text)
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
    #endregion          
  
  
    
    protected void ImageButton2_Click(object sender, ImageClickEventArgs e)
    {
        Delete();
    }
    protected void gvStore_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvStore.PageIndex = e.NewPageIndex;
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
}