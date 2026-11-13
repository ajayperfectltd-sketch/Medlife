/*----------------------------------------------------------------------------------------------------------------------------------------------------------
Purpose    : Insert/Update/Delete Shift  
Author     :bibin
Copyright  : Perfect Software Solutions (Clt) Pvt Ltd
Created on : 03/05/2018
----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
WebPharma
On			By			OMID
----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
 11/5/2018   Aiswarya   
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

public partial class Shift : System.Web.UI.Page
{
    #region Functions
    public void Delete()
    {
        try
        {
            divPopupShift.Visible = true;
            Populate Populate = new Populate();
            CommonPopup1.CallingFunction = "PopupOnShift";
            ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "PopupOnShift();", true);
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
            BlShift blShift = new BlShift();
            //blReason.MasterId = Convert.ToInt64(gvReason.DataKeys[e.RowIndex]["ID_Reason"].ToString());
            blShift.UserCode = hfuserName.Value;
            blShift.MasterId = Convert.ToInt64(hfMasterID.Value);
            blShift.FK_Reason = Convert.ToInt64(Reason);
            if (hfIsDelete.Value.ToLower() == "true")
            {
                status = blShift.DeleteData();
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
            blShift = null;
        }
    }
    protected void BindData()
    {
        Populate populate = new Populate();
        populate = null;
        

    }
    protected void SetControls()
    {
      
    }
    protected void GridFill(int currentPage)
    {
        BlShift blShift = new BlShift();
        DataTable dtbl = new DataTable();
        blShift.ShortName = txtSearchShortName.Text.Trim();
        blShift.Name = txtSearchName.Text.Trim();
        blShift.FK_Client = Convert.ToInt64(hfClient.Value);
        blShift.PageIndex = Convert.ToInt16(currentPage);
        blShift.Active = Convert.ToBoolean(chkActive.Checked);
        blShift.UserCode = hfUser.Value;
        dtbl = blShift.SelectAllData();
        gvShift.DataSource = dtbl;
        gvShift.DataBind();
        blShift = null;
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
        BlShift blShift = new BlShift();
      
        blShift.Name = txtName.Text;
        blShift.ShortName = txtShortName.Text;
        
       
        DateTime timeFrom = DateTime.Parse(string.Format("{0}:{1}:{2} {3}", TmsShiftFrom.Hour, TmsShiftFrom.Minute, TmsShiftFrom.Second, TmsShiftFrom.AmPm));
        blShift.ShiftFrom = Convert.ToDateTime(timeFrom.ToString("hh:mm:ss tt"));
        DateTime timeTo = DateTime.Parse(string.Format("{0}:{1}:{2} {3}", TmsShiftTo.Hour, TmsShiftTo.Minute, TmsShiftTo.Second, TmsShiftTo.AmPm));
        blShift.ShiftTo = Convert.ToDateTime(timeTo.ToString("hh:mm:ss tt"));
        blShift.Active = Convert.ToBoolean(chkActive.Checked);
        blShift.FK_Client = Convert.ToInt64(hfClient.Value);
        blShift.UserCode = hfuserName.Value;

        if (hfShiftId.Value == "0")
        {
            if (hfIsAdd.Value.ToLower() == "true")
            {
                status = blShift.InsertData();
                if (status > 0)
                {
                    ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "ShowAddDiv();", true);
                    ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "toastr.success('Data inserted successfully');ClearAdd();", true);
                    GridFill(1);
                }
                else if (status == -3)
                {
                    ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "ShowAddDiv();", true);
                    ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "toastr.error('shift time already exists');ClearAdd();", true);
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
            if (hfIsUpdate.Value.ToLower() == "true")
            {
                blShift.MasterId = Convert.ToInt64(hfShiftId.Value);
                status = blShift.UpdateData();
                if (status > 0)
                {
                    ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "ShowAddDiv();", true);
                    ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "toastr.success('Data updated successfully');ClearAdd();", true);
                    GridFill(1);
                }
                else if (status == -3)
                {
                    ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "ShowAddDiv();", true);
                    ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "toastr.error('shift time already exists');ClearAdd();", true);
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
        blShift = null;
    }
    protected void CheckUserAccess()
    {
        BlUserAccess bluseraccess = new BlUserAccess();
        Populate populate = new Populate();

        bluseraccess.FK_Users = Convert.ToInt64(hfUser.Value);
        bluseraccess.FK_Pages = 15;
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
                hfuserName.Value = Session["Username"].ToString();
                CheckUserAccess();
                BindData();
                GridFill(1);
                SetControls();
                txtShortName.Focus();
                if (Convert.ToBoolean(hfIsAdd.Value) == false)
                {
                    ((Label)CommonPopupPrivilege.FindControl("popuplabel")).Text = "User does not have permission to Add";
                    mpPrivilege.Show();
                }
                if (Convert.ToBoolean(hfIsUpdate.Value) == false)
                {
                    gvShift.Columns[4].Visible = false;
                }
                if (Convert.ToBoolean(hfIsDelete.Value) == false)
                {
                    gvShift.Columns[5].Visible = false;
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
            ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "toastr.warning('" + ex.Message + "')", true);
        }
    }
    protected void gvShift_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        try
        {
            //blMessage.Items.Clear();
            ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "ShowAddDiv();", true);
            BlShift blShift = new BlShift();
            blShift.MasterId = Convert.ToInt64(gvShift.DataKeys[e.NewSelectedIndex]["ID_ShiftMaster"].ToString());
            hfShiftId.Value = blShift.MasterId.ToString();
            blShift.FK_Client = Convert.ToInt64(hfClient.Value);
            blShift.UserCode = hfUser.Value;
            blShift.SelectData();
            chkActive.Checked = blShift.Active;
            txtName.Text = blShift.Name.ToString();
            txtShortName.Text = blShift.ShortName.ToString();
           
            //ddlCounter.SelectedValue = blShift.FK_Couter.ToString();

            //**************************
            DateTime mydt = DateTime.Parse(blShift.ShiftFrom.ToString());
            MKB.TimePicker.TimeSelector.AmPmSpec am_pm;
            if (mydt.ToString("tt") == "AM")
            {
                am_pm = MKB.TimePicker.TimeSelector.AmPmSpec.AM;
            }
            else
            {
                am_pm = MKB.TimePicker.TimeSelector.AmPmSpec.PM;
            }
            TmsShiftFrom.SetTime(mydt.Hour, mydt.Minute, am_pm);
            //**************************
            //**************************
            DateTime mydt1 = DateTime.Parse(blShift.ShiftTo.ToString());
            MKB.TimePicker.TimeSelector.AmPmSpec am_pm1;
            if (mydt1.ToString("tt") == "AM")
            {
                am_pm1 = MKB.TimePicker.TimeSelector.AmPmSpec.AM;
            }
            else
            {
                am_pm1 = MKB.TimePicker.TimeSelector.AmPmSpec.PM;
            }
            TmsShiftTo.SetTime(mydt1.Hour, mydt1.Minute, am_pm1);
            //**************************

         

            //txtShiftFrom.Text = blShift.ShiftFrom.ToShortDateString();
            //txtShiftTo.Text = blShift.ShiftTo.ToShortDateString();
            
            Populate populate = new Populate();
            populate = null;

            blShift = null;
        }
        catch (Exception ex)
        {
            //blMessage.Items.Add(ex.Message);
        }
    }
    protected void gvShift_RowDeleting(object sender, GridViewDeleteEventArgs e)
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
                BlShift blShift = new BlShift();
                blShift.MasterId = Convert.ToInt64(gvShift.DataKeys[e.RowIndex]["ID_ShiftMaster"].ToString());
                hfMasterID.Value = (gvShift.DataKeys[e.RowIndex]["ID_ShiftMaster"].ToString());
                blShift.FK_Client = Convert.ToInt64(hfClient.Value);
                blShift.UserCode = hfuserName.Value;
                blShift = null;
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
            //blMessage.Items.Clear();
            SaveData();
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "toastr.warning('" + ex.Message + "')", true);
        }
    }
    
    protected void btnStoreBlurTrigger_Click(object sender, EventArgs e)
    {
        try
        {
            try
            {
                ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "ShowAddDiv();", true);
                
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "toastr.warning('" + ex.Message + "')", true);
            }
          
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
  
    protected void gvShift_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvShift.PageIndex = e.NewPageIndex;
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
    #endregion
}