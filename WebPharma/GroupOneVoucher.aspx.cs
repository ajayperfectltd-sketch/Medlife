/*----------------------------------------------------------------------------------------------------------------------------------------------------------
Purpose    : Insert/Update/Delete GroupOneVoucher
Author     :Bibn tc
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
using System.Data;
using WebPharma.Business.Masters;

public partial class GroupOneVoucher : System.Web.UI.Page
{
 
    #region Function
    protected void BindData()
    {
        Populate populate = new Populate();
        populate.FillDropdownList(ddlAcSubHead, "AccountHeadSub", "Name", "ID_AccountHeadSub", "ShortName+'-'+Name", "FK_Client=" + hfClient.Value + "  And Active = 1 ", "");
        populate.FillDropdownList(ddlSearchAccountSubHead, "AccountHeadSub", "Name", "ID_AccountHeadSub", "ShortName+'-'+Name", "FK_Client=" + hfClient.Value + "  And Active = 1 ", "");
        populate = null;
    }
    public void GridFill(int currentPage)
    {
        BlGroupOneVoucher blGroupOneVoucher=new BlGroupOneVoucher();
        DataTable dtbl = new DataTable();
        blGroupOneVoucher.GroupOneCode= txtSearchGroupOneCode.Text;
        blGroupOneVoucher.GroupOneName = txtSearchGroupOneName.Text;
        blGroupOneVoucher.FK_AccountHeadSub = Convert.ToInt64(ddlSearchAccountSubHead.SelectedValue);
        blGroupOneVoucher.PageSize = 10;

       
        blGroupOneVoucher.PageIndex = Convert.ToInt16(currentPage);
       
       blGroupOneVoucher.UserCode = hfUser.Value;
       blGroupOneVoucher.FK_Client = Convert.ToInt64(hfClient.Value);

        dtbl =blGroupOneVoucher.SelectAllData();
        gvGroupOneVoucher.DataSource = dtbl;
        gvGroupOneVoucher.DataBind();
        if (dtbl.Rows.Count > 0)
        {
            int recordCount = Convert.ToInt32(dtbl.Rows[0]["RecordCount"].ToString());
            this.PopulatePager(recordCount, currentPage);
        }
       blGroupOneVoucher = null;
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
        BlGroupOneVoucher blGroupOneVoucher=new BlGroupOneVoucher();
        blGroupOneVoucher.GroupOneCode= txtGroupOneCode.Text.Trim();
        blGroupOneVoucher.GroupOneName = txtGroupOneName.Text.Trim();
        blGroupOneVoucher.FK_AccountHeadSub = Convert.ToInt64(ddlAcSubHead.SelectedValue);   
        blGroupOneVoucher.Description = txtDescrption.Text;
        blGroupOneVoucher.Active = chkActive.Checked;
        blGroupOneVoucher.UserCode = hfUser.Value;
        blGroupOneVoucher.FK_Client = Convert.ToInt64(hfClient.Value);
        if (hdfGroupOneVoucher.Value == string.Empty)
        {
            if (hfIsAdd.Value.ToLower() == "true")
            {
                status = blGroupOneVoucher.InsertData();
                if (status > 0)
                {
                   
                    GridFill(1);
                    ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "ShowAddDiv();", true);                  
                    Populate.InsertMsg(this);
                }

                else
                {
                    ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "ShowAddDiv();", true);                  
                    Populate.ErrorMessageBox(this, "Cannot insert data, GroupOneCode/GroupOneName already exists");
                    txtGroupOneCode.Focus();
                }
            }
            else 
            {
                ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "toastr.error('User does not have permission to add')", true);
                txtGroupOneCode.Focus();
            
            }

        }
        else
        {
            blGroupOneVoucher.MasterId = Convert.ToInt64(hdfGroupOneVoucher.Value);
            blGroupOneVoucher.FK_Client =  Convert.ToInt64(hfClient.Value);
            if (hfIsUpdate.Value.ToLower() == "true")
            {
                status = blGroupOneVoucher.UpdateData();
                if (status > 0)
             {
                    GridFill(1);                   
                    Populate.UpdateMsg(this);
                }

                else
                {
                    ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "ShowAddDiv();", true);
                    Populate.ErrorMessageBox(this, "Cannot update data, GroupOneCode/GroupOneName already exists");
                    txtGroupOneCode.Focus();
                }
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "toastr.error('User does not have permission to Update')", true);
                txtGroupOneCode.Focus();
            }
        }
        blGroupOneVoucher = null;

    }

    protected void CheckUserAccess()
    {
        BlUserAccess bluseraccess = new BlUserAccess();
        Populate populate = new Populate();

        bluseraccess.FK_Users = Convert.ToInt64(hfUser.Value);
        bluseraccess.FK_Pages = 54;
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
        //blMessage.Items.Clear();
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
                BindData();
                txtGroupOneCode.Focus();
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
            //blMessage.Items.Clear();
            int pageIndex = int.Parse((sender as LinkButton).CommandArgument);

            pageIndex = pageIndex == 0 ? 1 : pageIndex;
            GridFill(pageIndex);       
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
            //blMessage.Items.Clear();
            divAdd.Style.Add("display", "none");
            divView.Style.Add("display", "block");
            ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "ClearAdd();", true);
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
            //blMessage.Items.Clear();
            SaveData();
            txtGroupOneCode.Focus();
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
            txtSearchGroupOneCode.Focus();
        }
        catch (Exception ex)
        {
            // //blMessage.Items.Add(ex.Message);
            Populate.WarningMsg(this, ex.Message);
        }
    }


    protected void gvGroupOneVoucher_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        //ID_AccountGroup
        try
        {
            //blMessage.Items.Clear();
            ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "ShowAddDiv();", true);
            BlGroupOneVoucher blGroup=new BlGroupOneVoucher();
          
            blGroup.MasterId = Convert.ToInt64(gvGroupOneVoucher.DataKeys[e.NewSelectedIndex]["ID_GroupOneVoucher"].ToString());

            hdfGroupOneVoucher.Value = blGroup.MasterId.ToString();

            blGroup.UserCode = hfUser.Value;
            blGroup.FK_Client = Convert.ToInt64(hfClient.Value);
            blGroup.SelectData();
            txtGroupOneCode.Text = blGroup.GroupOneCode;
            txtGroupOneName.Text =blGroup.GroupOneName;
            try
            {
                ddlAcSubHead.SelectedValue = blGroup.FK_AccountHeadSub.ToString();
            }
            catch (Exception ex)
            {
                ddlAcSubHead.SelectedValue = "0"; 
            }
            txtDescrption.Text =blGroup.Description;
            chkActive.Checked = blGroup.Active;
          

           blGroup = null;
        }
        catch (Exception ex)
        {
            // //blMessage.Items.Add(ex.Message);
            Populate.WarningMsg(this, ex.Message);
        }
    }
    protected void gvGroupOneVoucher_RowDeleting(object sender, GridViewDeleteEventArgs e)
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
                BlGroupOneVoucher blGroup=new BlGroupOneVoucher();
                blGroup.MasterId = Convert.ToInt64(gvGroupOneVoucher.DataKeys[e.RowIndex]["ID_GroupOneVoucher"].ToString());
                blGroup.UserCode = hfUser.Value;
                blGroup.FK_Client = Convert.ToInt64(hfClient.Value);
                if (hfIsDelete.Value.ToLower() == "true")
                {
                    status = blGroup.DeleteData();
                    if (status > 0)
                    {
                        GridFill(1);                       
                        Populate.DeleteMsg(this);
                    }
                    else
                    {                      
                        Populate.ReferanceMsg(this);
                    }
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "toastr.error('User does not have permission to Delete')", true);
                    txtGroupOneCode.Focus();
                }
                blGroup= null;
            }
        }
        catch (Exception ex)
        {         
            Populate.WarningMsg(this, ex.Message);
        }
    }
    #endregion
}
