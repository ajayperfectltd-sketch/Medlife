/*----------------------------------------------------------------------------------------------------------------------------------------------------------
Purpose    : Insert/Update/Delete Leave  
Author     : bibin TC
Copyright  : Perfect Software Solutions (Clt) Pvt Ltd
Created on :03/05/18
----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
WebPharma
On			By			OMID
----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------*/


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

public partial class HolidaySettings : System.Web.UI.Page
{
    #region Functions
   
    protected void GridFill(int currentPage)
    {
        BlHolidaySettings blholiday = new BlHolidaySettings();
        DataTable dtbl = new DataTable();
        blholiday.FK_Client = Convert.ToInt64(hfClient.Value);
        blholiday.PageIndex = Convert.ToInt16(currentPage == 0 ? 1 : currentPage);
        blholiday.UserCode = hfUser.Value;
        blholiday.Reason = txtSearchReason.Text.Trim();
        blholiday.Type = Convert.ToByte(ddlSearchLeaveType.SelectedValue);
        blholiday.FromDate = DateTime.ParseExact(txtDateFrom.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
        blholiday.ToDate = DateTime.ParseExact(txtToDate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
        dtbl = blholiday.SelectAllData();
        gvHolidaySettings.DataSource = dtbl;
        gvHolidaySettings.DataBind();
        if (dtbl.Rows.Count > 0)
        {
            int recordCount = Convert.ToInt32(dtbl.Rows[0]["RecordCount"].ToString());
            this.PopulatePager(recordCount, currentPage);
        }
        blholiday = null;

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
        BlHolidaySettings blholiday = new BlHolidaySettings();
        blholiday.Reason = txtReason.Text.Trim();
        blholiday.Date = DateTime.ParseExact(txtDate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
        blholiday.Type = Convert.ToByte(ddleavType.SelectedValue);
        DateTime timeFrom = DateTime.Parse(string.Format("{0}:{1}:{2} {3}", TmsShiftFrom.Hour, TmsShiftFrom.Minute, TmsShiftFrom.Second, TmsShiftFrom.AmPm));
        blholiday.FromTime = Convert.ToDateTime(timeFrom.ToString("hh:mm:ss tt"));
        DateTime timeTo = DateTime.Parse(string.Format("{0}:{1}:{2} {3}", TmsShiftTo.Hour, TmsShiftTo.Minute, TmsShiftTo.Second, TmsShiftTo.AmPm));
        blholiday.ToTime = Convert.ToDateTime(timeTo.ToString("hh:mm:ss tt"));

        blholiday.EnteredOn = DateTime.Now;
        blholiday.FK_Client = Convert.ToInt64(hfClient.Value);
        blholiday.UserCode = hfUser.Value;
        if (hdfHolyDayId.Value.Split(',')[0] == "0")
        {
            if (hfIsAdd.Value.ToLower() == "true")
            {
                status = blholiday.InsertData();
                if (status > 0)
                {
                    ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "ShowAddDiv();", true);
                    ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "toastr.success('Data inserted successfully');ClearAdd();", true);
                    GridFill(1);
                }

                else
                {
                    ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "ShowAddDiv();", true);
                    ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "toastr.error('Cannot insert data,already Assigned leave for the day')", true);
                    txtReason.Focus();
                }
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "toastr.warning('User does not have permission to add')", true);
                txtReason.Focus();

            }
        }
        else
        {
            if (hfIsUpdate.Value.ToLower() == "true")
            {
                blholiday.MasterId = Convert.ToInt64(hdfHolyDayId.Value.Split(',')[0]);
                status = blholiday.UpdateData();
                if (status > 0)
                {
                    ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "script", "ShowAddDiv();", true);
                    ScriptManager.RegisterStartupScript(Page, typeof(Page), "script", "toastr.success('Data updated successfully');ClearAdd();", true);
                    GridFill(1);
                }

                else
                {
                    ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "script", "ShowAddDiv();", true);
                    ScriptManager.RegisterStartupScript(Page, typeof(Page), "script", "toastr.error('Cannot update data,already Assigned leave for the day')", true);
                    txtReason.Focus();
                }
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "toastr.warning('User does not have permission to Update')", true);
                txtReason.Focus();
            }
        }
        blholiday = null;


    }
    protected void CheckUserAccess()
    {
        BlUserAccess bluseraccess = new BlUserAccess();
        Populate populate = new Populate();

        bluseraccess.FK_Users = Convert.ToInt64(hfUser.Value);
        bluseraccess.FK_Pages = 21;
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
            lblValidate.Attributes.Add("style","display:none;color:red;");
                
            if (!IsPostBack)
            {
                Page.Header.DataBind();
               // blMessage.Items.Clear();
                ScriptManager.RegisterStartupScript(Page, typeof(Page), "script", "ClearAdd();", true);
                divAdd.Style.Add("display", "block");
                hfUser.Value = Request.QueryString["uid"];
                hfClient.Value = Request.QueryString["clnt"];
                CheckUserAccess();
                txtDate.Attributes.Add("readonly", "readonly");
                txtDateFrom.Attributes.Add("readonly", "readonly");
                txtToDate.Attributes.Add("readonly", "readonly");
                GridFill(1);
                txtReason.Focus();
              
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "toastr.warning('" + ex.Message + "')", true);
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
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        try
        {
            //blMessage.Items.Clear();
            GridFill(1);
            txtSearchReason.Focus();
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "toastr.warning('" + ex.Message + "')", true);
        }
    }


    protected void gvHolidaySettings_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        try
        {
           // blMessage.Items.Clear();
            ScriptManager.RegisterStartupScript(Page, typeof(Page), "script", "ShowAddDiv();", true);
            BlHolidaySettings blHoliday = new BlHolidaySettings();

            blHoliday.FromDate = DateTime.Parse("1/1/1900");
            blHoliday.ToDate = DateTime.MaxValue;

            blHoliday.FK_Client = Convert.ToInt64(hfClient.Value.Split(',')[0]);
            blHoliday.MasterId = Convert.ToInt64(gvHolidaySettings.DataKeys[e.NewSelectedIndex]["ID_HolidaySettings"].ToString());
            blHoliday.Reason = "";
            blHoliday.Type = 2;


            hdfHolyDayId.Value = blHoliday.MasterId.ToString();
            blHoliday.SelectData();
            txtReason.Text = blHoliday.Reason;
            txtDate.Text = blHoliday.Date.ToString("dd/MM/yyyy");
            ddleavType.SelectedValue = blHoliday.Type.ToString();

          

            DateTime mydt = DateTime.Parse(blHoliday.FromTime.ToString());
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
            DateTime mydt1 = DateTime.Parse(blHoliday.ToTime.ToString());
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
            blHoliday = null;

        }
        catch (Exception ex)
        {
        }

      
    
        
        
    }
    protected void gvHolidaySettings_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {

        try
        {
            string confirmValue = Request.Form["confirm_value"];
            if (confirmValue.Length > 3)
                confirmValue = confirmValue.Substring(confirmValue.Length - 3, 3);
            if (confirmValue == "Yes")
            {
                long status = 0;
                BlHolidaySettings blHolidaySettings = new BlHolidaySettings();
                blHolidaySettings.FK_Client = Convert.ToInt64(hfClient.Value);
                blHolidaySettings.MasterId = Convert.ToInt64(gvHolidaySettings.DataKeys[e.RowIndex]["ID_HolidaySettings"].ToString());
                blHolidaySettings.Cancelled = true;

                blHolidaySettings.Cancelleduser = hfUser.Value;
                blHolidaySettings.CancelledOn = DateTime.Now;
                if (hfIsDelete.Value.ToLower() == "true")
                {
                    status = blHolidaySettings.DeleteData();
                    if (status > 0)
                    {
                        GridFill(1);
                        ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "script", "toastr.success('Data deleted successfully')", true);
                    }
                    else
                    {
                        ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "script", "toastr.error('Cannot delete data, reference exists')", true);
                    }
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "toastr.warning('User does not have permission to Delete')", true);
                   
                }
                blHolidaySettings = null;


            }

        }
        catch (Exception ex)
        {
        }
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
           // blMessage.Items.Clear();

            string confirm_validate = Request.Form["confirm_validate"];
            
            if (confirm_validate.Length > 3)
                confirm_validate = confirm_validate.Substring(confirm_validate.Length - 3, 3);
            if (confirm_validate == "Yes")
            {
                lblValidate.Attributes.Add("style", "display:none;");
                SaveData();
                txtReason.Focus();
            }
            else
            {
                lblValidate.Attributes.Add("style", "display:block;color:red;");
                ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "script", "ShowAddDiv();", true);
            }
        }
        catch (Exception ex)
        {
        }

    }
    protected void imgbtnAdd_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
        }
        catch (Exception ex)
        {
        }
    }

    #endregion



  
}