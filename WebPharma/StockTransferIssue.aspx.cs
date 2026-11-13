/*----------------------------------------------------------------------------------------------------------------------------------------------------------
Purpose    : Insert/Update/Delete Purchase Order  
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
using WebPharma.Business.WebPharma;
using WebPharma.Business.Masters;
using WebPharma.Business.Accounts;
using System.Data;
using System.Collections;
using System.Globalization;


public partial class StockTransferIssue : System.Web.UI.Page
{

    #region Functions
    
    public void BindData()
    {
        Populate populate = new Populate();
        populate.FillDropdownList(ddlStore, "Store", "Name", "ID_Store", "Name", " Active=1 and FK_Client = " + hfClient.Value, "");
        populate.FillDropdownList(ddlStoreSearch, "Store", "Name", "ID_Store", "Name", " Active=1 and FK_Client = " + hfClient.Value, "");
        populate.FillDropdownList(ddlToStore, "Store", "Name", "ID_Store", "Name", " Active=1 and FK_Client = " + hfClient.Value, "");      
      
        populate = null;

    }
    public void SetReadonly(params TextBox[] textbox)
    {
        foreach (TextBox txt in textbox)
            txt.Attributes.Add("readonly", "readonly");

    }
    protected void GridFill(int currentPage)
    {
        
        BlIndentIssue BlIndentIssue = new BlIndentIssue();
        DataTable dtbl = new DataTable();
        BlIndentIssue.FromDate = Convert.ToDateTime(txtFromDate.Text==""?"01/01/1991":txtFromDate.Text);//DateTime.ParseExact(txtFromDate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
        BlIndentIssue.ToDate = Convert.ToDateTime(txtTodate.Text == "" ? DateTime.Now.ToString("dd/MM/yyyy") :txtTodate.Text);//DateTime.ParseExact(txtTodate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
      
        BlIndentIssue.FK_FromStore = Convert.ToInt64(ddlStoreSearch.SelectedValue);
        BlIndentIssue.PageSize = 10;
        currentPage = currentPage == 0 ? 1 : currentPage;
        BlIndentIssue.PageIndex = Convert.ToInt16(currentPage);
        BlIndentIssue.FK_Client = Convert.ToInt64(hfClient.Value);
        dtbl = BlIndentIssue.SelectAllData();
        gvPurchaseOrder.DataSource = dtbl;
        gvPurchaseOrder.DataBind();
        if (dtbl.Rows.Count > 0)
        {
            int recordCount = Convert.ToInt32(dtbl.Rows[0]["RecordCount"].ToString());
            this.PopulatePager(recordCount, currentPage);

        }
        BlIndentIssue = null;
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
    protected void CheckUserAccess()
    {
        BlUserAccess bluseraccess = new BlUserAccess();
        Populate populate = new Populate();

        bluseraccess.FK_Users = Convert.ToInt64(hfUser.Value);
        bluseraccess.FK_Pages = 75;
        bluseraccess.FK_Client = Convert.ToInt64(hfClient.Value);
        bluseraccess.CheckUserAccess();
        if (Convert.ToBoolean(bluseraccess.IsView) == false)
        {        

        }

        hfIsAdd.Value = (Convert.ToBoolean(bluseraccess.IsAdd)).ToString();
        hfIsUpdate.Value = (Convert.ToBoolean(bluseraccess.IsUpdate)).ToString();
        hfIsDelete.Value = (Convert.ToBoolean(bluseraccess.IsDelete)).ToString();
        bluseraccess = null;
    }

   

  

    public void Save()
    {
        long status = 0;
        BlIndentIssue blIndentIssue = new BlIndentIssue();
        blIndentIssue.Date = txtDate.Text == "" ? DateTime.Now : DateTime.ParseExact(txtDate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);  
        blIndentIssue.Remarks = txtRemark.Text;
        blIndentIssue.FK_IndentRequest =Convert.ToInt64( txtRequestRefNo.Text==""?"0": txtRequestRefNo.Text) ;
        blIndentIssue.UserCode = hfUser.Value;
        blIndentIssue.EnteredOn = DateTime.Now;
        blIndentIssue.FK_FromStore = Convert.ToInt64(ddlStore.SelectedValue);
        blIndentIssue.FK_ToStore = Convert.ToInt64(ddlToStore.SelectedValue);
        blIndentIssue.FK_Client = Convert.ToInt64(hfClient.Value);
        blIndentIssue.PriceTag = 5;
        ArrayList arlist = new ArrayList();      
        IIndentIssueDetails IIndentIssueDetails;
        for (int i = 0; i < gvItems.Rows.Count; i++)
        {
            IIndentIssueDetails = new IIndentIssueDetails();
             IIndentIssueDetails.FK_Items = Convert.ToInt64(((HiddenField)gvItems.Rows[i].FindControl("hdfFK_Items")).Value == "" ? "0" : ((HiddenField)gvItems.Rows[i].FindControl("hdfFK_Items")).Value);
            IIndentIssueDetails.Quantity = Convert.ToDecimal(((TextBox)gvItems.Rows[i].FindControl("gvtxtQuantity")).Text == "" ? "0" : ((TextBox)gvItems.Rows[i].FindControl("gvtxtQuantity")).Text);
           // IIndentIssueDetails.FK_Stock = Convert.ToInt64(dtbl.Rows[i]["FK_Stock"].ToString());

           
            arlist.Add(IIndentIssueDetails);
            IIndentIssueDetails = null;
        }

        blIndentIssue.XmlPurchaseItems = Common.WriteToXml(arlist);


        if (hdfPurcahseOrder.Value == "")
        {
            if (hfIsAdd.Value.ToLower() == "true")
            {
                status = blIndentIssue.InsertData();
                if (status > 0)
                {
                    if (status == 1)
                    {
                        Clear();

                        ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "ShowAddDiv();", true);
                        Populate.InsertMsg(this);
                    }
                    if (status == 2)
                    {
                        Clear();

                        ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "ShowAddDiv();", true);
                        ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "toastr.success('Data inserted successfully AND Indent Request Closed');ClearAdd();", true);

                    }
                }
                else if(status == -10)
                {
                    ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "ShowAddDiv();", true);                
                    Populate.ErrorMessageBox(this, "Sorry,Indent Request is already Posted");
                }
                else if (status == -11)
                {
                    ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "ShowAddDiv();", true);
                    Populate.ErrorMessageBox(this, "Sorry,No Stock Available in the Store");
                }
                else if (status == -12)
                {
                    ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "ShowAddDiv();", true);
                    Populate.ErrorMessageBox(this, "Cannot insert data...");
                }
                else
                {
                    ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "ShowAddDiv();", true);                  
                    Populate.ErrorMessageBox(this, "Cannot insert data.");
                  
                }
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "toastr.warning('User does not have permission to add')", true);

            }
        }       


    }

    public void Clear()
    {
       
        ViewState["TempTable"] = null;
      
        gvItems.DataSource = null;
        gvItems.DataBind();
      
        divAdd.Style.Add("display", "block");
        divView.Style.Add("display", "none");
        btnSave.Visible = true;
        ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "ClearAdd();", true);
    }
    #endregion
   
    #region Events
    protected void Page_Load(object sender, EventArgs e)
    {
        
        try
        {
            if (!IsPostBack)
            {

                ViewState["TempTable"] = null;
                gvItems.DataSource = null;
                gvItems.DataBind();
                divAdd.Style.Add("display", "block");               
                hfUser.Value = Request.QueryString["uid"];
                hfClient.Value = Request.QueryString["clnt"];
                CheckUserAccess();
                BindData();
                txtDate.Focus();
                ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "ClearSearch();", true);

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
    protected void btnClear_Click(object sender, EventArgs e)
    {
       // blMessage.Items.Clear();
        try
        {
            Clear();
        }
        catch (Exception ex)
        {
            //blMessage.Items.Add(ex.Message);
            ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "toastr.warning('" + ex.Message + "')", true);
        }

    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        //blMessage.Items.Clear();
        try
        {
            if (gvItems.Rows.Count > 0)
            {
                Save();
                txtDate.Focus();
            }

            else
            {
                ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "alertify.alert('Atleast one item required')", true);
            }
        }
        catch (Exception ex)
        {
            //blMessage.Items.Add(ex.Message);
            ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "toastr.warning('" + ex.Message + "')", true);
        }
    }
    protected void gvItems_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {    
        try
        {
            string confirmValue = Request.Form["confirm_value"];
            if (confirmValue.Length > 3)
                confirmValue = confirmValue.Substring(confirmValue.Length - 3, 3);
            if (confirmValue == "Yes")
            {
                if (ViewState["TempTable"] != null)
                {
                    DataTable dtbl = (DataTable)ViewState["TempTable"];
                    dtbl.Rows.RemoveAt(e.RowIndex);
                    gvItems.DataSource = dtbl;
                    gvItems.DataBind();
                    ViewState["TempTable"] = dtbl;
                }
               
            }
        }
        catch (Exception ex)
        {
            //blMessage.Items.Add(ex.Message);
        }
        ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "ShowAddDiv();", true);
        divAdd.Style.Add("display", "block");
        divView.Style.Add("display", "none");
        
    }
    protected void gvPurchaseOrder_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {       
        try
        {
            BlIndentIssue BlIndentIssue = new BlIndentIssue();
            ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "ShowAddDiv();", true);
            BlIndentIssue.FromDate = DateTime.ParseExact("01/01/1990", "dd/MM/yyyy", CultureInfo.InvariantCulture);
            BlIndentIssue.ToDate = DateTime.ParseExact("01/01/9999", "dd/MM/yyyy", CultureInfo.InvariantCulture);
            BlIndentIssue.MasterId = Convert.ToInt64(gvPurchaseOrder.DataKeys[e.NewSelectedIndex]["ID_Indent"].ToString());
            BlIndentIssue.FK_Client = Convert.ToInt64(hfClient.Value);
            hdfPurcahseOrder.Value = BlIndentIssue.MasterId.ToString();
            BlIndentIssue.SelectData();


            txtDate.Text = BlIndentIssue.Date.ToString("dd/MM/yyyy");
            txtRequestNo.Text = BlIndentIssue.PostingId.ToString();
            try
            {
                ddlStore.SelectedValue = BlIndentIssue.FK_FromStore.ToString();
            }
            catch
            {
                ddlStore.SelectedValue = "0";
            }
            try
            {
                ddlToStore.SelectedValue = BlIndentIssue.FK_ToStore.ToString();
            }
            catch
            {
                ddlStore.SelectedValue = "0";
            }
           
            txtRemark.Text = BlIndentIssue.Remarks;
          

           
            DataTable dtbl= new DataTable();
            dtbl = BlIndentIssue.SelectIndentIssueItems();
            gvItems.DataSource = dtbl;
            gvItems.DataBind();
            ViewState["TempTable"] = dtbl;
            BlIndentIssue = null;
            btnSave.Visible = false;
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "toastr.warning('" + ex.Message + "')", true);
        }
    }
  
    protected void imgbtnView_Click(object sender, ImageClickEventArgs e)
    {       
        try
        {    
            ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "ClearAdd(); return false;", true);
            GridFill(1);
            divAdd.Style.Add("display", "none");
            divView.Style.Add("display", "block");
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
        }
        catch (Exception ex)
        {           
            ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "toastr.warning('" + ex.Message + "')", true);
        }
    }
    protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
    {    
        try
        {
            Clear();
            divAdd.Style.Add("display", "block");
            divView.Style.Add("display", "none");
        }
        catch (Exception ex)
        {        
            ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "toastr.warning('" + ex.Message + "')", true);
        }
    }
    protected void btnItem_Click(object sender, EventArgs e)
    {
        ddlItem_SelectedIndexChanged(sender, e);
    }
    protected void ddlItem_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {           
            divAdd.Style.Add("display", "block");
            divView.Style.Add("display", "none");
          
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "toastr.warning('" + ex.Message + "')", true);
        }
       
    }
  
     
    #endregion   
   
   
    protected void imgbtnRequestNo_Click(object sender, ImageClickEventArgs e)
    {
        try
        {

            txtRequestNo.Text = "";
            txtRequestRefNo.Text = "";
            ((TextBox)CommonPopup1.FindControl("txtName")).Text = txtRequestNo.Text.Trim();
            ((TextBox)CommonPopup1.FindControl("txtRefno")).Text = txtRequestRefNo.Text;
            ((HiddenField)CommonPopup1.FindControl("hfColumn")).Value = "Name";
            BillNoFill(1);
            divAdd.Style.Add("display", "block");
            divView.Style.Add("display", "none");
          
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "toastr.warning('" + ex.Message + "')", true);
        }
           
    }

    public void BillNoFill(int pageIndex)
    {
      
        Populate Populate = new Populate();
        CommonPopup1.CallingFunction = "BillNoFill";
        string strName = ((TextBox)CommonPopup1.FindControl("txtName")).Text;
        string Refno = ((TextBox)CommonPopup1.FindControl("txtRefno")).Text;
        Refno = Refno == string.Empty || Refno == null ? "0" : Refno;
        string searchCol = ((HiddenField)CommonPopup1.FindControl("hfColumn")).Value;
        searchCol = searchCol == string.Empty ? "[Name]" : "[" + searchCol + "]";
        Populate.FillGrid((GridView)CommonPopup1.FindControl("gdvSearch"), "IndentRequest", "ID_IndentRequest as ID,PostingID as Name ,dbo.DateFormats(TransDate) As [Bill Date]", "ID_IndentRequest", "", "FK_Client=" + hfClient.Value.Split(',')[0] + " AND FK_Store= (CASE WHEN " + ddlToStore.SelectedValue + "=0 THEN FK_Store ELSE  " + ddlToStore.SelectedValue + " END)    AND  (ID_IndentRequest =" + Refno + " OR 0=" + Refno + ")  AND Cancelled= 0 AND Approved=1 AND Posted=0  ", searchCol + "  LIKE '" + strName + "%'", pageIndex, 10, "", (Repeater)CommonPopup1.FindControl("rptPager"), this.Page);
        Populate = null;
        txtRequestRefNo.Focus();
        if (txtRequestRefNo.Text == string.Empty)
        {
            lblPopupHeader.Text = "Stock transfer Request";
            mpCommon.Show();
        }

    }

    public void SetDetails(string strName, string strRefno)
    {
        if (CommonPopup1.CallingFunction == "BillNoFill")
        {
            txtRequestRefNo.Text = strRefno;
            txtRequestNo.Text = strName;

            if (txtRequestRefNo.Text != "" && txtRequestRefNo.Text != "0")
            {
                LoadRequestItems(txtRequestRefNo.Text);
            }
        }
        ddlToStore.Focus();
    }
    public void LoadRequestItems( string strRefno)
    {
        DataTable dtblMain = new DataTable();
        BlIndentRequest blindentrequest = new BlIndentRequest();       
        blindentrequest.MasterId = Convert.ToInt64(strRefno);
        blindentrequest.FK_Client = Convert.ToInt64(hfClient.Value);
        dtblMain= blindentrequest.SelectAllData();
        ddlToStore.SelectedValue =dtblMain.Rows[0]["FK_ToStore"].ToString();

        BlIndentIssue blindentissue = new BlIndentIssue();
        blindentissue.FK_IndentRequest = Convert.ToInt64(strRefno);
        blindentissue.FK_FromStore = Convert.ToInt64(ddlStore.SelectedValue);
        blindentissue.FK_Client = Convert.ToInt64(hfClient.Value);
        DataTable dtbl = new DataTable();
        dtbl = blindentissue.SelectIndentRequestItemsForIssue();
     
        gvItems.DataSource = dtbl;
        gvItems.DataBind();
        ViewState["TempTable"] = dtbl;

        blindentrequest = null;
        blindentissue = null;
    }
    protected void ddlStore_SelectedIndexChanged(object sender, EventArgs e)
    {    
        try
        {
          
            txtRequestNo.Text = "";
            txtRequestRefNo.Text = "";
            ViewState["TempTable"] = null;
            gvItems.DataSource = null;
            gvItems.DataBind();
            divAdd.Style.Add("display", "block");
            divView.Style.Add("display", "none");
            txtRequestNo.Focus();
        }
        catch (Exception ex)
        {
         
            ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "toastr.warning('" + ex.Message + "')", true);
        }
    }
    protected void ddlToStore_SelectedIndexChanged(object sender, EventArgs e)
    {
      
        try
        {         
            txtRequestNo.Text = "";
            txtRequestRefNo.Text = "";
            ViewState["TempTable"] = null;
            gvItems.DataSource = null;
            gvItems.DataBind();
            divAdd.Style.Add("display", "block");
            divView.Style.Add("display", "none");
        }
        catch (Exception ex)
        {
         
            ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "toastr.warning('" + ex.Message + "')", true);
        }
    }
    protected void gvPurchaseOrder_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            if (e.CommandName == "Print")
            {

                GridViewRow row = (GridViewRow)((ImageButton)e.CommandSource).NamingContainer;
                int index = row.RowIndex;
                string ID_Indent = gvPurchaseOrder.DataKeys[index]["ID_Indent"].ToString();

                string popupreport = "<script> window.open('PrintReport.aspx?Type=STI&ID=" + ID_Indent + "', 'self', 'left=200,top=50,width=900,height=680,status=0,resizable=0,fullscreen=0,scrollbars=1');</script>";
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "Success", popupreport, false);

            }
        }
        catch (Exception)
        {

            throw;
        }
    }
}