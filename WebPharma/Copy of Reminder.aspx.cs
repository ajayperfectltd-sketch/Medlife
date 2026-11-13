/*----------------------------------------------------------------------------------------------------------------------------------------------------------
Purpose    : Insert/Update/Delete Bank 
Author     :Bibin TC
Copyright  : Perfect Software Solutions (Clt) Pvt Ltd
Created on : 11/05/2018
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
using System.Globalization;
using System.Collections;

public partial class Reminder : System.Web.UI.Page
{

    private void BindData()
    {
        Populate populate = new Populate();
        populate.FillDropdownList(ddlUserGroup, "UserGroup", "GroupName", "ID_UserGroup", "ID_UserGroup", " Active='True'   and FK_Client = " + hfClient.Value.Split(',')[0], "");
        populate = null;
    }
    private void DataListFill()
    {
        if (ddlType.SelectedValue == "3")
        {
            BlReminder blreminder = new BlReminder();
            blreminder.FK_UserGroup = Convert.ToInt64(ddlUserGroup.SelectedValue);
            blreminder.FK_Client = Convert.ToInt64(hfClient.Value.Split(',')[0]);
            blreminder.FK_User = hdfReminder.Value.Split(',')[0] == "" ? 0 : Convert.ToInt64(hdfReminder.Value.Split(',')[0]);
            DataTable dt = blreminder.SelectUserForReminder();
            dlUser.DataSource = dt;
          
        }
        else
        {
            dlUser.DataSource = null;
        }
        
        dlUser.DataBind();
        divAdd.Style.Add("display", "block");
        divView.Style.Add("display", "none;");
      
        liUserGroup.Visible = dlUser.Items.Count > 0;
        
    }
    private void ClearAll()
    {
        ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "ClearAdd();ClearSearch();", true);
        ddlType.SelectedIndex = 0;
       
        dlUser.DataSource = null;
        dlUser.DataBind();
        liUserGroup.Visible = false;
        ddlUserGroup.SelectedIndex = 0;
        divAdd.Style.Add("display", "block");
        divView.Style.Add("display", "none;");
    }
    #region Functions
    protected void GridFill(int currentPage)
    {
        BlReminder blreminder = new BlReminder();
        DataTable dtbl = new DataTable();
        blreminder.Topic = txtSearchTopic.Text.Trim();
        blreminder.FromDate = DateTime.ParseExact(txtFromDate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
        blreminder.ToDate = DateTime.ParseExact(txtToDate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
        blreminder.FK_Client = Convert.ToInt64(hfClient.Value);
        blreminder.Type = Convert.ToByte(ddlTypeSearch.SelectedValue);
        blreminder.PageIndex = Convert.ToInt16(currentPage);
        blreminder.UserCode = hfUser.Value;
        blreminder.FK_User = Convert.ToInt64(hfUser.Value);
        dtbl = blreminder.SelectAllReminder();
        gvReminder.DataSource = dtbl;
        gvReminder.DataBind();
        if (dtbl.Rows.Count > 0)
        {
            int recordCount = Convert.ToInt32(dtbl.Rows[0]["RecordCount"].ToString());
            this.PopulatePager(recordCount, currentPage);
        }
        blreminder = null;
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
       
    }
    protected void SaveData()
    {
        long Status = 0;
        bool isvalid = false;
        BlReminder Blr = new BlReminder();
        Blr.Topic = txtTopic.Text.Trim();
        Blr.Message = txtReminder.Text.Trim();
        Blr.FromDate=DateTime.ParseExact(txtStDate.Text.Trim(),"dd/MM/yyyy",CultureInfo.InvariantCulture);
        Blr.ToDate=DateTime.ParseExact(txtEndDate.Text.Trim(),"dd/MM/yyyy",CultureInfo.InvariantCulture);
        Blr.Type=Convert.ToByte(ddlType.SelectedValue);
        Blr.FK_User = 0;

        Blr.Active=chkActive.Checked;
        Blr.UserCode=hfUser.Value;
        Blr.EnteredOn=DateTime.Now;
        Blr.FK_Client = Convert.ToInt64(hfClient.Value);
        ArrayList arlist1 = new ArrayList();
        IReminderDetails IReminderDetails;
        if (Blr.Type == 1)
        {
            Blr.FK_User = Convert.ToInt64(hfUser.Value.Split(',')[0]);
           
        }
        else if (Blr.Type == 3)
        {
            foreach (DataListItem dlItem in dlUser.Items)
            {
                if (dlItem != null)
                {
                    bool chkReportSubCategory = Convert.ToBoolean(((CheckBox)dlItem.FindControl("chkUser")).Checked);
                         if (chkReportSubCategory)
                         {
                             isvalid = true;
                             IReminderDetails = new IReminderDetails();
                             IReminderDetails.FK_User = Convert.ToInt64(((HiddenField)dlItem.FindControl("hfID_User")).Value.ToString());
                             arlist1.Add(IReminderDetails);

                             IReminderDetails = null;
                         }

                }
            }

        }

         Blr.XmlUser= Common.WriteToXml(arlist1);

        if (hdfReminder.Value == "")
        {
            if (hfIsAdd.Value.ToLower() == "true")
            {
                if (Blr.Type == 3)
                {
                                    if (!isvalid)
                                    {
                                        ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "alertify.alert('At least one user required');", true);
                                        return;
                                    }
                                    else
                                    {
                                        Status = Blr.InsertReminder();
                                        if (Status > 0)
                                        {
                                            ClearAll();
                                            GridFill(1);
                                            Populate.InsertMsg(this);
                                        }
                                        else
                                        {
                                            Populate.ErrorMessageBox(this, "Cannot insert data");

                                        }
                                    }
                   
                }
                else
                {
                                        Status = Blr.InsertReminder();
                                        if (Status > 0)
                                        {
                                            ClearAll();
                                            GridFill(1);
                                            Populate.InsertMsg(this);
                                        }
                                        else
                                        {
                                            Populate.ErrorMessageBox(this, "Cannot insert data");

                                        }
                }

             
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "toastr.warning('User does not have permission to add')", true);
              

            }

        }
        else
        {
            Blr.MasterId = Convert.ToInt64(hdfReminder.Value);
            if (hfIsUpdate.Value.ToLower() == "true")
            {

                if (Blr.Type == 3)
                {
                    if (!isvalid)
                    {
                        ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "alertify.alert('At least one user required');", true);
                        return;
                    }
                    else
                    {
                        Status = Blr.UpdateReminder();
                        if (Status > 0)
                        {
                            ClearAll();
                            GridFill(1);
                            Populate.UpdateMsg(this);
                        }
                        else
                        {
                            Populate.ErrorMessageBox(this, "Cannot Update");
                        }
                    }
                }
                else
                {
                    Status = Blr.UpdateReminder();
                    if (Status > 0)
                    {
                        ClearAll();
                        GridFill(1);
                        Populate.UpdateMsg(this);
                    }
                    else
                    {
                        Populate.ErrorMessageBox(this, "Cannot Update");
                    }
                }
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "toastr.warning('User does not have permission to Update')", true);
              
            }
            

        }

        Blr = null;
        
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
               
                txtStDate.Attributes.Add("readonly", "readonly");
                txtEndDate.Attributes.Add("readonly", "readonly");
                txtFromDate.Attributes.Add("readonly", "readonly");
                txtToDate.Attributes.Add("readonly", "readonly");
                Page.Header.DataBind();
                //blMessage.Items.Clear();
                ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "ClearAdd();ClearSearch();", true);
                divAdd.Style.Add("display", "block");
                hfUser.Value = Request.QueryString["uid"];
                hfClient.Value = Request.QueryString["clnt"];
                CheckUserAccess();
             

                ClearAll();
                BindData();
                txtTopic.Focus();
              
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "toastr.warning('" + ex.Message + "')", true);    
        }
    }
    protected void Page_Changed(object sender, EventArgs e)
    {
        //blMessage.Items.Clear();
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
            GridFill(1);
            txtSearchTopic.Focus();
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "toastr.warning('" + ex.Message + "')", true);
        }
    }

    protected void gvReminder_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "ShowAddDiv();", true);
        BlReminder blR = new BlReminder();
        blR.MasterId = Convert.ToInt64(gvReminder.DataKeys[e.NewSelectedIndex]["ID_Reminder"].ToString());
        blR.FromDate = DateTime.Parse("1/1/1900");
        blR.ToDate = DateTime.MaxValue;
        hdfReminder.Value = blR.MasterId.ToString();
        blR.FK_Client = Convert.ToInt64(hfClient.Value);
        blR.UserCode = hfUser.Value;
        blR.FK_User = Convert.ToInt64(hfUser.Value);
        blR.SelectReminder();
        txtTopic.Text = blR.Topic;
        txtReminder.Text = blR.Message;
        txtStDate.Text = blR.FromDate.ToString("dd/MM/yyyy");
        txtEndDate.Text = blR.ToDate.ToString("dd/MM/yyyy");
        ddlType.SelectedValue = blR.Type.ToString();
        chkActive.Checked = blR.Active;
        ddlUserGroup.SelectedIndex = 0;
        DataListFill();
        

    }
    protected void gvReminder_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {  
        //blMessage.Items.Clear();
            string confirmValue = Request.Form["confirm_value"];
            if (confirmValue.Length > 3)
                confirmValue = confirmValue.Substring(confirmValue.Length - 3, 3);
            if (confirmValue == "Yes")
            {
                long status = 0;
                BlReminder blRemind = new BlReminder();
                blRemind.MasterId = Convert.ToInt64(gvReminder.DataKeys[e.RowIndex]["ID_Reminder"].ToString());
                blRemind.FK_Client = Convert.ToInt64(hfClient.Value);
                blRemind.UserCode = hfUser.Value;
                if (hfIsDelete.Value.ToLower() == "true")
                {
                    status = blRemind.DeleteData();
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
                    
                }
                blRemind = null;
            }

    }

    protected void btnTypeBlurTrigger_Click(object sender, EventArgs e)
    {
        try
        {
            ddlUserGroup.SelectedIndex = 0;
            DataListFill();
        }
        catch (Exception ex)
        {
        }

    }
    protected void btnClear_Click(object sender, EventArgs e)
    {
        ClearAll();
    }
    protected void ddlUserGroup_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            DataListFill();
        }
        catch (Exception ex)
        {
        }

    }
    protected void imgbtnAdd_Click(object sender, ImageClickEventArgs e)
    {
        ClearAll();
    }
    #endregion

   
}