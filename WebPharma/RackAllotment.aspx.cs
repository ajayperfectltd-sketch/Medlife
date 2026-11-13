/*----------------------------------------------------------------------------------------------------------------------------------------------------------
Purpose    : Insert/Update/Delete Rack Allotment
* Author     : Bibin TC
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
using WebPharma.Business.Masters;
using System.Data;
using System.Globalization;

public partial class RackAllotment : System.Web.UI.Page
{
    #region Functions
    protected void CheckUserAccess()
    {
        BlUserAccess bluseraccess = new BlUserAccess();
        Populate populate = new Populate();

        bluseraccess.FK_Users = Convert.ToInt64(hfUser.Value);
        bluseraccess.FK_Pages = 65;
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
    protected void BindData()
    {
        Populate populate = new Populate();
        populate.FillDropdownList(ddlRack, "Rack", "RackName", "ID_Rack", "ID_Rack", "FK_Client = " + hfClient.Value, "");
        populate.FillDropdownList(ddlSupplier, "Supplier", "Name", "ID_Supplier", "ID_Supplier", "  Active='True'  and FK_Client = " + hfClient.Value.Split(',')[0], "");
        populate.FillDropdownList(ddlSearchSupplier, "Supplier", "Name", "ID_Supplier", "ID_Supplier", "  Active='True'  and FK_Client = " + hfClient.Value.Split(',')[0], "");
        populate.FillPeriodType(ddlPeriodType);
        populate = null;
        txtAllotmentDate.Text = DateTime.Now.ToShortDateString();
        txtDueDate.Text = DateTime.Now.ToShortDateString();
    }
    protected void GridFill(int currentPage)
    {
        BlRackAllotment blRackAllotment = new BlRackAllotment();
        DataTable dtbl = new DataTable();
        blRackAllotment.FK_Supplier = Convert.ToInt64(ddlSearchSupplier.SelectedValue);
        blRackAllotment.FK_Client = Convert.ToInt64(hfClient.Value);
        blRackAllotment.UserCode = hfUser.Value;
        blRackAllotment.PageIndex = Convert.ToInt16(currentPage);
        dtbl = blRackAllotment.SelectAllData();
        gvRackAllotment.DataSource = dtbl;
        gvRackAllotment.DataBind();
        if (dtbl.Rows.Count > 0)
        {
            int recordCount = Convert.ToInt32(dtbl.Rows[0]["RecordCount"].ToString());
            this.PopulatePager(recordCount, currentPage);
        }
        blRackAllotment = null;
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
    public void SetReadonly()
    {

        txtAllotmentDate.Attributes.Add("readonly", "readonly");
        txtDueDate.Attributes.Add("readonly", "readonly");


    }
    protected void SaveData()
    {
        long status = 0;
        BlRackAllotment blRackAllotment = new BlRackAllotment();
        blRackAllotment.FK_Rack = Convert.ToInt64(ddlRack.SelectedValue);
        blRackAllotment.AllotmentDate = DateTime.ParseExact(txtAllotmentDate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
        blRackAllotment.FK_Supplier = Convert.ToInt64(ddlSupplier.SelectedValue);
        blRackAllotment.PeriodType = Convert.ToInt32(ddlPeriodType.SelectedValue);
        blRackAllotment.Period = Convert.ToInt32(txtPeriod.Text);
        blRackAllotment.DueDate = DateTime.ParseExact(txtDueDate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
        blRackAllotment.Rent = Convert.ToDecimal(txtRent.Text);
        blRackAllotment.Remarks = txtRemarks.Text;
        blRackAllotment.FK_Client = Convert.ToInt64(hfClient.Value);
        blRackAllotment.UserCode = hfUser.Value;
        if (hfRackAllotmentId.Value == "0")
        {
            if (hfIsAdd.Value.ToLower() == "true")
            {
                status = blRackAllotment.InsertData();
                if (status > 0)
                {
                    GridFill(1);
                    ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "ShowAddDiv();", true);
                    ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "toastr.success('Data inserted successfully');ClearAdd();", true);

                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "ShowAddDiv();", true);
                    ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "toastr.error('Cannot insert data,  Rack allotment already exists')", true);
                    ddlRack.Focus();
                }
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "toastr.warning('User does not have permission to add')", true);
                ddlRack.Focus();

            }
        }
        else
        {
            if (hfIsUpdate.Value.ToLower() == "true")
            {
                blRackAllotment.MasterId = Convert.ToInt64(hfRackAllotmentId.Value);
                status = blRackAllotment.UpdateData();
                if (status > 0)
                {
                    ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "ShowAddDiv();", true);
                    ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "toastr.success('Data updated successfully');ClearAdd();", true);
                    GridFill(1);

                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "ShowAddDiv();", true);
                    ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "toastr.error('Cannot update data, Rack allotment already exists or Vacated')", true);
                    ddlRack.Focus();
                }
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "toastr.warning('User does not have permission to Update')", true);
                ddlRack.Focus();
            }
        }
        blRackAllotment = null;
    }
    #endregion
    #region Events
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
    protected void Page_Changed(object sender, EventArgs e)
    {
        //blMessage.Items.Clear();
        int pageIndex = int.Parse((sender as LinkButton).CommandArgument);
        GridFill(pageIndex);
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        try
        {
            //blMessage.Items.Clear();
            GridFill(1);
            ddlSearchSupplier.Focus();
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
         ddlRack.Focus();
        }
        catch (Exception ex)
        {
            //blMessage.Items.Add(ex.Message);
        }
    }
    protected void gvRackAllotment_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        try
        {
            //blMessage.Items.Clear();
            ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "ShowAddDiv();", true);
            BlRackAllotment blRackAllotment = new BlRackAllotment();
            blRackAllotment.MasterId = Convert.ToInt64(gvRackAllotment.DataKeys[e.NewSelectedIndex]["ID_RackAllotment"].ToString());
            hfRackAllotmentId.Value = blRackAllotment.MasterId.ToString();
            blRackAllotment.FK_Client = Convert.ToInt64(hfClient.Value);
            blRackAllotment.UserCode = hfUser.Value;
            blRackAllotment.SelectData();

            ddlRack.SelectedValue = blRackAllotment.FK_Rack.ToString();
            txtAllotmentDate.Text = blRackAllotment.AllotmentDate.ToShortDateString();
            ddlSupplier.SelectedValue = blRackAllotment.FK_Supplier.ToString();
            ddlPeriodType.SelectedValue = blRackAllotment.PeriodType.ToString();
            txtPeriod.Text = blRackAllotment.Period.ToString();
            txtDueDate.Text = blRackAllotment.DueDate.ToShortDateString();
            txtRent.Text = blRackAllotment.Rent.ToString();
            txtRemarks.Text = blRackAllotment.Remarks.ToString();
            blRackAllotment = null;
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "toastr.warning('" + ex.Message + "')", true);
        }
    }
    protected void gvRackAllotment_RowDeleting(object sender, GridViewDeleteEventArgs e)
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
                BlRackAllotment blRackAllotment = new BlRackAllotment();
                blRackAllotment.MasterId = Convert.ToInt64(gvRackAllotment.DataKeys[e.RowIndex]["ID_RackAllotment"].ToString());
                blRackAllotment.UserCode = hfUser.Value;
                if (hfIsDelete.Value.ToLower() == "true")
                {
                    status = blRackAllotment.DeleteData();
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
                blRackAllotment = null;
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "toastr.warning('" + ex.Message + "')", true);
        }
    }
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
                BindData();
                SetReadonly();
                GridFill(1);
                CheckUserAccess();
                ddlRack.Focus();
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "toastr.warning('" + ex.Message + "')", true);
        }
    }
    #endregion
}