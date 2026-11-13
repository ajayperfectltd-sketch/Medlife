using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using WebPharma.Business.Masters;
/*----------------------------------------------------------------------------------------------------------------------------------------------------------
Purpose    : Rack View Pop up to search
Author     : BIBIN TC
Copyright  : Perfect Software Solutions (Clt) Pvt Ltd
Created on : 10/05/2018
----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
WebPharma
On			By			OMID
----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------


--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------*/

public partial class RackSearch : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            txtRackName.Text = Convert.ToString(Request.QueryString["itn"]);
            hfUser.Value = Request.QueryString["uid"];
            hfClient.Value = Request.QueryString["clnt"];



         
          //  BindData();
            //   ddlSearchCategoryFirst.SelectedValue = Convert.ToString(Request.QueryString["ctgry"]);

            GridFill(1);
           
            gvRack.Focus();
        }
    }
    protected void gvRack_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {

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
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "toastr.warning('" + ex.Message + "')", true);
        }
    }
    protected void GridFill(int currentPage)
    {
        BlRack blRack = new BlRack();
        DataTable dtbl = new DataTable();
        blRack.RackName = txtRackName.Text.Trim();
        blRack.FK_Supplier = Convert.ToInt64(Request.QueryString["FK_Supplier"].ToString());
        blRack.FK_Client = Convert.ToInt64(hfClient.Value);
        blRack.UserCode = hfUser.Value;
        blRack.PageIndex = Convert.ToInt16(currentPage);

        dtbl = blRack.SelectRackPopup();
        gvRack.DataSource = dtbl;
        gvRack.DataBind();
        if (dtbl.Rows.Count > 0)
        {
            int recordCount = Convert.ToInt32(dtbl.Rows[0]["RecordCount"].ToString());
            this.PopulatePager(recordCount, currentPage);
        }
        blRack = null;
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
}