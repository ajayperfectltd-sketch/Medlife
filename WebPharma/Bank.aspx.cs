/*----------------------------------------------------------------------------------------------------------------------------------------------------------
Purpose    : Insert/Update/Delete Bank 
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
using System.Web.Security;
using WebPharma.Business.Masters;
using System.Data;

public partial class Bank : System.Web.UI.Page
{
    #region Functions
    protected void GridFill(int currentPage)
    {
        BlBank blbank = new BlBank();
        DataTable dtbl = new DataTable();
        blbank.ShortName = txtSearchShortName.Text.Trim();
        blbank.Name = txtSearchName.Text.Trim();
        blbank.FK_Client = Convert.ToInt64(hfClient.Value);
        blbank.UserCode = hfUser.Value;
        blbank.PageIndex = Convert.ToInt16(currentPage);
        dtbl = blbank.SelectAllData();
        gvBank.DataSource = dtbl;
        gvBank.DataBind();
        if (dtbl.Rows.Count > 0)
        {
            int recordCount = Convert.ToInt32(dtbl.Rows[0]["RecordCount"].ToString());
            this.PopulatePager(recordCount, currentPage);
        }
        blbank = null;
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
        BlBank blbank = new BlBank();
        blbank.ShortName = txtShortName.Text;
        blbank.Name = txtName.Text;
        blbank.Address1 = txtAddress1.Text;
        blbank.Address2 = txtAddress2.Text;
        blbank.Address3 = txtAddress3.Text;
        blbank.FK_Client = Convert.ToInt64(hfClient.Value);
        blbank.UserCode = hfUser.Value;
        blbank.Active = Convert.ToBoolean(chkActive.Checked);
        if (hfBankId.Value == "0")
        {
            if (hfIsAdd.Value.ToLower() == "true")
            {
                status = blbank.InsertData();
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
            blbank.MasterId = Convert.ToInt64(hfBankId.Value);
            if (hfIsUpdate.Value.ToLower() == "true")
            {

                status = blbank.UpdateData();
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
        blbank = null;
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
                ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "ClearAdd();", true);
                divAdd.Style.Add("display", "block");
                hfUser.Value = Request.QueryString["uid"];
                hfClient.Value = Request.QueryString["clnt"];
                CheckUserAccess();
                txtShortName.Focus();
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "toastr.warning('"+ex.Message+"')", true);    
        }
    }
    protected void Page_Changed(object sender, EventArgs e)
    {
       // blMessage.Items.Clear();
        int pageIndex = int.Parse((sender as LinkButton).CommandArgument);
        GridFill(pageIndex);
    }
    protected void imgbtnView_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            //blMessage.Items.Clear();
            divAdd.Style.Add("display", "none");
            divView.Style.Add("display", "block");
            ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "ClearAdd();", true);
            GridFill(1);
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
            //blMessage.Items.Clear();
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
            //blMessage.Items.Clear();
            GridFill(1); txtSearchShortName.Focus();
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "toastr.warning('" + ex.Message + "')", true);    
        }
    }
    protected void gvBank_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        try
        {
            //blMessage.Items.Clear();
            ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "ShowAddDiv();", true);
            BlBank blbank = new BlBank();
            blbank.MasterId = Convert.ToInt64(gvBank.DataKeys[e.NewSelectedIndex]["ID_Bank"].ToString());
            hfBankId.Value = blbank.MasterId.ToString();
            blbank.FK_Client = Convert.ToInt64(hfClient.Value);
            blbank.UserCode = hfUser.Value;
            blbank.SelectData();
            txtShortName.Text = blbank.ShortName.ToString();
            txtName.Text = blbank.Name.ToString();
            txtAddress1.Text = blbank.Address1.ToString();
            txtAddress2.Text = blbank.Address2.ToString();
            txtAddress3.Text = blbank.Address3.ToString();
            chkActive.Checked = blbank.Active;
            blbank = null;
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "toastr.warning('" + ex.Message + "')", true);    
        }
    }
    protected void gvBank_RowDeleting(object sender, GridViewDeleteEventArgs e)
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
                BlBank blbank = new BlBank();
                blbank.MasterId = Convert.ToInt64(gvBank.DataKeys[e.RowIndex]["ID_Bank"].ToString());
                blbank.FK_Client = Convert.ToInt64(hfClient.Value);
                blbank.UserCode = hfUser.Value;
                if (hfIsDelete.Value.ToLower() == "true")
                {
                    status = blbank.DeleteData();
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
                blbank = null;
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "toastr.warning('" + ex.Message + "')", true);    
        }
    }
    #endregion                   
}