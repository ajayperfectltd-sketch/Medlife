/*----------------------------------------------------------------------------------------------------------------------------------------------------------
Purpose    : Reminder View 
Author     : BIBIN TC
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

public partial class ReminderView : System.Web.UI.Page
{
    #region Functions
    protected void GridFill(int currentPage)
    {

        //ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "ClearSearch();", true);
        BlReminder blreminder = new BlReminder();
        DataTable dtbl = new DataTable();
        string a = txtFromDate.Text;
        blreminder.FromDate = Convert.ToDateTime(txtFromDate.Text);//DateTime.ParseExact(txtFromDate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
        blreminder.ToDate = Convert.ToDateTime(txtToDate.Text); //DateTime.ParseExact(txtToDate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
        blreminder.FK_Client = Convert.ToInt64(hfClient.Value);

        blreminder.PageIndex = Convert.ToInt16(currentPage);

        blreminder.FK_User = Convert.ToInt64(hfUser.Value);
        dtbl = blreminder.GetReminderDetailsView();

        gvReminder.DataSource = dtbl;
        gvReminder.DataBind();
        if (dtbl.Rows.Count > 0)
        {
            int recordCount = Convert.ToInt32(dtbl.Rows[0]["RecordCount"].ToString());
            this.PopulatePager(recordCount, currentPage);
        }
        blreminder = null;
    }
    protected void Page_Changed(object sender, EventArgs e)
    {
        blMessage.Items.Clear();
        int pageIndex = int.Parse((sender as LinkButton).CommandArgument);
        GridFill(pageIndex);
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
    #endregion
    #region evnts
    protected void Page_Load(object sender, EventArgs e)
    {          
            if (!IsPostBack)
            {                               
                ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "ClearSearch();", true);
                txtTopicReminder.Attributes.Add("readOnly", "readOnly");
                txtMessageReminder.Attributes.Add("readOnly", "readOnly");  
            }            
    }
     
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        try
        {
            blMessage.Items.Clear();

            GridFill(1);
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "toastr.warning('" + ex.Message + "')", true);
        }
    }
    protected void btnPageLoadFunctions_Click(object sender, EventArgs e)
    {
        try
        {
            hfUser.Value = Request.QueryString["uid"];
            hfClient.Value = Request.QueryString["clnt"];
            txtFromDate.Text = hfDate.Value;
            txtToDate.Text = hfDate.Value;            
            GridFill(1);
               
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "toastr.warning('" + ex.Message + "')", true);
        }
    }
    protected void lnkView_Click(object sender, EventArgs e)
    {
        MpeReminderShow.Show();
    }
    protected void gvReminder_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        MpeReminderShow.Show();

        BlReminder blreminder = new BlReminder();
        blreminder.MasterId = Convert.ToInt64(gvReminder.DataKeys[e.NewSelectedIndex]["ID_Reminder"].ToString());
        blreminder.FK_Client = Convert.ToInt64(hfClient.Value);
        blreminder.FromDate = Convert.ToDateTime(txtFromDate.Text);//DateTime.ParseExact(txtFromDate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
        blreminder.ToDate = Convert.ToDateTime(txtToDate.Text); //DateTime.ParseExact(txtToDate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);

        blreminder.UserCode = hfUser.Value;
        blreminder.SelectReminder();


        txtMessageReminder.Text = blreminder.Message.ToString();
        txtTopicReminder.Text = blreminder.Topic.ToString();

        blreminder = null;
        //blreminder.FromDate = Convert.ToDateTime(txtFromDate.Text);//DateTime.ParseExact(txtFromDate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
        //blreminder.ToDate = Convert.ToDateTime(txtToDate.Text); //DateTime.ParseExact(txtToDate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
        //blreminder.FK_Client = Convert.ToInt64(hfClient.Value);




        //blreminder.FK_User = Convert.ToInt64(hfUser.Value);

    }
    #endregion

   
   
}