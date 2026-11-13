/*----------------------------------------------------------------------------------------------------------------------------------------------------------
Purpose    : Insert Cess Settings 
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
using System.Globalization;

public partial class CessSettings : System.Web.UI.Page
{
    #region Function
    protected void GridFill(int currentPage)
    {
        BlCessSettings blCessSettings = new BlCessSettings();
        DataTable dtbl = new DataTable();
        blCessSettings.FromDate = DateTime.ParseExact(txtFromDate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
        blCessSettings.ToDate = DateTime.ParseExact(txtToDate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
        blCessSettings.PageIndex = Convert.ToInt16(currentPage);
        blCessSettings.FK_Client = Convert.ToInt64(hfClient.Value);
        blCessSettings.UserCode = hfUser.Value;
        dtbl = blCessSettings.SelectAllData();
        gvCessSettings.DataSource = dtbl;
        gvCessSettings.DataBind();
        if (dtbl.Rows.Count > 0)
        {
            int recordCount = Convert.ToInt32(dtbl.Rows[0]["RecordCount"].ToString());
            this.PopulatePager(recordCount, currentPage);
        }
        blCessSettings = null;
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
        BlCessSettings blCessSettings = new BlCessSettings();
        blCessSettings.EffectFrom = DateTime.ParseExact(txtEffectFrom.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
        blCessSettings.CGST = Convert.ToDecimal(txtCGST.Text == "" ? "0" : txtCGST.Text);
        blCessSettings.SGST = Convert.ToDecimal(txtSGST.Text == "" ? "0" : txtSGST.Text);
        blCessSettings.IGST = Convert.ToDecimal(txtIGST.Text == "" ? "0" : txtIGST.Text);
        blCessSettings.EnteredOn = Convert.ToDateTime(DateTime.Now);
        blCessSettings.FK_Client = Convert.ToInt64(hfClient.Value);
        blCessSettings.UserCode = hfUser.Value;
        if (hfCessSettingsId.Value == "0")
        {
            if (hfIsAdd.Value.ToLower() == "true")
            {
                status = blCessSettings.InsertData();
                if (status > 0)
                {

                    GridFill(1);
                    ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "ShowAddDiv();", true);
                    ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "toastr.success('Data inserted successfully');ClearAdd();", true);
                }
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "toastr.warning('User does not have permission to add')", true);
                txtEffectFrom.Focus();

            }
        }
        blCessSettings = null;
    }
    protected void CheckUserAccess()
    {
        BlUserAccess bluseraccess = new BlUserAccess();
        Populate populate = new Populate();

        bluseraccess.FK_Users = Convert.ToInt64(hfUser.Value);
        bluseraccess.FK_Pages = 82;
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
                ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "ClearAdd();", true);
                divAdd.Style.Add("display", "block");
                hfUser.Value = Request.QueryString["uid"];
                hfClient.Value = Request.QueryString["clnt"];
                CheckUserAccess();
                hfCessSettingsId.Value = "0";
                txtEffectFrom.Focus();
                GridFill(1);
            }
        }
        catch (Exception ex)
        {
            //ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "toastr.warning('" + ex.Message + "')", true);
        }
    }
    protected void imgbtnView_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
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

        int pageIndex = int.Parse((sender as LinkButton).CommandArgument);
        GridFill(pageIndex);

    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            SaveData();
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "toastr.warning('" + ex.Message + "')", true);
        }
    }
    protected void gvCessSettings_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        try
        {
            ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "ShowAddDiv();", true);
            BlCessSettings blCessSettings = new BlCessSettings();
            blCessSettings.MasterId = Convert.ToInt64(gvCessSettings.DataKeys[e.NewSelectedIndex]["ID_CessSettings"].ToString());
            hfCessSettingsId.Value = blCessSettings.MasterId.ToString();
            blCessSettings.FK_Client = Convert.ToInt64(hfClient.Value);
            blCessSettings.UserCode = hfUser.Value;
            blCessSettings.SelectData();
            txtEffectFrom.Text = blCessSettings.EffectFrom.ToString("dd/MM/yyyy");
            txtCGST.Text = blCessSettings.CGST.ToString();
            txtSGST.Text = blCessSettings.SGST.ToString();
            txtIGST.Text = blCessSettings.IGST.ToString();
            blCessSettings = null;
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
            txtFromDate.Focus();
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "toastr.warning('" + ex.Message + "')", true);
        }
    }
    #endregion  
}