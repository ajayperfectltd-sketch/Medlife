/*----------------------------------------------------------------------------------------------------------------------------------------------------------
Purpose    : Insert/Update/Delete Storage 
Author     : Bibin tc
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
using WebPharma.Business.Accounts;
using WebPharma.Business.WebPharma;

public partial class CustomerRePayment : System.Web.UI.Page
{
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
                BindData();
                //GridFill(1);
                chkPrint.Checked = true;
                lblTransType.Text = "";
                txtcustomer.Focus();
            }
        }
        catch (Exception ex)
        {
            //  blMessage.Items.Add(ex.Message);
        }
    }
    protected void ImageButton4_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            Customer();
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "toastr.warning('" + ex.Message + "')", true);
        }
    }

    public void CustomerFill(int pageIndex)
    {
        ClearSupplier();
        Populate Populate = new Populate();
        CommonPopup1.CallingFunction = "CustomerFill";
        string strName = ((TextBox)CommonPopup1.FindControl("txtName")).Text;
        string Refno = ((TextBox)CommonPopup1.FindControl("txtRefno")).Text;
        string searchCol = ((HiddenField)CommonPopup1.FindControl("hfColumn")).Value;
        Refno = Refno == string.Empty || Refno == null ? "0" : Refno;
        Populate.FillGrid((GridView)CommonPopup1.FindControl("gdvSearch"), "Customer", "ID_Customer as ID,Name as Name, ShortName  , Address , Mobile", "ID_Customer", "", "FK_Client=" + hfClient.Value.Split(',')[0] + " AND  Active=1 AND CreditSale=1 ", searchCol + "  LIKE '" + strName + "%'", pageIndex, 10, "", (Repeater)CommonPopup1.FindControl("rptPager"), this.Page);
        Populate = null;
        //    txtSubHeadRefno.Focus();
        if (txtcustomer.Text == string.Empty)
        {
            lblPopupHeader.Text = "Customer";
            mpCommon.Show();
        }
        txtcustomer.Focus();

    }
    protected void btnHiddenLoadStore_Click(object sender, EventArgs e)
    {
        try
        {
            if (ddlCounter.Items.Count > 0)
            {
                if (ddlCounter.SelectedIndex > 0)
                {

                    Populate populate = new Populate();

                    populate.FillDropdownList(ddlStore, "Store S inner join  Counter C on C.FK_Store=S.ID_Store  ", "S.Name", "S.ID_Store", "S.ID_Store", "    S.Active='True' AND C.ID_Counter=" + ddlCounter.SelectedValue.ToString() + " AND S.FK_Client= " + hfClient.Value.Split(',')[0], "");

                    populate = null;
                    if (ddlCounter.SelectedIndex > 0)
                        ddlStore.Focus();
                    else
                        ddlCounter.Focus();
                }
                else
                {


                }
            }
            divAdd.Style.Add("display", "block");
            divView.Style.Add("display", "none");

        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "toastr.warning('" + ex.Message + "')", true);
        }
    }
    protected void Customer()
    {
        try
        {
            ((TextBox)CommonPopup1.FindControl("txtName")).Text = txtcustomer.Text.Trim();
            ((TextBox)CommonPopup1.FindControl("txtRefno")).Text = txtcustomer.Text.Trim();
            ((HiddenField)CommonPopup1.FindControl("hfColumn")).Value = "Name";
            CustomerFill(1);
            divAdd.Style.Add("display", "block");
            divView.Style.Add("display", "none");
            ddlTransType.Focus();
        }
        catch (Exception ex)
        {
        }
    }
    public void SetDetails(string strName, string strRefno)
    {
        if (CommonPopup1.CallingFunction == "CustomerFill")
        {
            txtcustomer.Text = strName;
            hfFK_Customer.Value = strRefno.ToString();
            OustandingBalance(strRefno);
            ddlTransType.Focus();

        }

    }
    private void ClearSupplier()
    {
        txtcustomer.Text = "";
        txtAmount.Text = "0";
        txtBalance.Text = "0";
        ddlTransType.SelectedIndex = 0;
        ddlCounter.SelectedIndex = 0;
        ddlStore.SelectedIndex = 0;
        lblTransType.Text = "";
        hfFK_Customer.Value = "0";
        hfCustRepayId.Value = "0";
    }
    private void OustandingBalance(string FK_Customer)
    {
        BlAccountHeadSubTransactions blaccountheadsubtransactions = new BlAccountHeadSubTransactions();
        blaccountheadsubtransactions.CustomerNo = Convert.ToInt64(FK_Customer);
        decimal Amount = blaccountheadsubtransactions.OustandingBalance();
        if (Amount < 0)
        { lblTransType.Text = "Dr"; }
        else if (Amount == 0)
        { lblTransType.Text = ""; }
        else { lblTransType.Text = "Cr"; }
        txtBalance.Text = Math.Abs(Amount).ToString();
    }
    protected void CheckUserAccess()
    {
        BlUserAccess bluseraccess = new BlUserAccess();
        Populate populate = new Populate();

        bluseraccess.FK_Users = Convert.ToInt64(hfUser.Value);
        bluseraccess.FK_Pages = 78;
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
    protected void SaveData()
    {
        long status = 0;
        BlRePayment blRepayment = new BlRePayment();
        blRepayment.FK_Customer = Convert.ToInt64(hfFK_Customer.Value);
        blRepayment.TransType = ddlTransType.SelectedValue;
        blRepayment.Amount = Convert.ToDecimal(txtAmount.Text);
        blRepayment.FK_Client = Convert.ToInt64(hfClient.Value);
        blRepayment.FK_Counter = Convert.ToInt64(ddlCounter.SelectedValue.ToString());
        blRepayment.FK_Store = Convert.ToInt64(ddlStore.SelectedValue.ToString());
        blRepayment.UserCode = hfUser.Value;
        if (hfCustRepayId.Value == "0")
        {
            if (hfIsAdd.Value.ToLower() == "true")
            {
                status = blRepayment.InsertData();
                if (status > 0)
                {
                    ClearSupplier();
                    ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "ShowAddDiv();", true);
                    ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "toastr.success('Data inserted successfully');ClearAdd();", true);
                    if (chkPrint.Checked == true)
                    {
                        Print(status);
                    }
                }
                else if (status == -3)
                {
                    ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "ShowAddDiv();", true);
                    ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "toastr.warning('Amount Exceeded the Credit Limit')", true);
                    txtcustomer.Focus();
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "ShowAddDiv();", true);
                    ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "toastr.error('Cannot insert data')", true);
                    txtcustomer.Focus();
                }
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "toastr.warning('User does not have permission to add')", true);
                txtcustomer.Focus();

            }
        }

        blRepayment = null;
    }
    protected void BindData()
    {
        Populate populate = new Populate();
        populate.FillDropdownList(ddlCounter, "Counter", "Name", "ID_Counter", "ID_Counter", "Active='True' AND FK_Client= " + hfClient.Value.Split(',')[0], "");
        populate = null;


    }
    public void Print(Int64 Status)
    {
        BlIndentRequest blIndentRequest = new BlIndentRequest();
        string ID_CustomerRePayment = Status.ToString();
        string popupreport = "<script> window.open('PrintReport.aspx?Type=CRP&ID=" + ID_CustomerRePayment + "', 'self', 'left=200,top=50,width=900,height=680,status=0,resizable=0,fullscreen=0,scrollbars=1');</script>";
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "Success", popupreport, false);
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            if (Convert.ToInt64(hfFK_Customer.Value) == 0)
            {
                ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "toastr.warning('Please Select a Customer')", true);
                txtcustomer.Focus();
            }
            if(txtAmount.Text=="0")
            {
                ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "toastr.warning('Enter Amount')", true);
                txtAmount.Focus();
            }
            else
            {
                SaveData();
            }
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
    protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
    {
        ClearSupplier();
        divView.Style.Add("display", "none");
        divAdd.Style.Add("display", "block");
    }
    protected void GridFill(int currentPage)
    {
        BlRePayment blrepayment = new BlRePayment();
        DataTable dtbl = new DataTable();
        blrepayment.CustName = txtSearchCustName.Text.Trim();
        blrepayment.TransType = ddlSearchTransType.SelectedValue;
        blrepayment.FK_Client = Convert.ToInt64(hfClient.Value);
        blrepayment.PageIndex = Convert.ToInt16(currentPage);
        blrepayment.UserCode = hfUser.Value;
        dtbl = blrepayment.SelectAllData();
        gvRepayment.DataSource = dtbl;
        gvRepayment.DataBind();
        if (dtbl.Rows.Count > 0)
        {
            int recordCount = Convert.ToInt32(dtbl.Rows[0]["RecordCount"].ToString());
            this.PopulatePager(recordCount, currentPage);
        }
        blrepayment = null;
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
    protected void Page_Changed(object sender, EventArgs e)
    {
        int pageIndex = int.Parse((sender as LinkButton).CommandArgument);
        GridFill(pageIndex);
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        try
        {
            GridFill(1);
            txtcustomer.Focus();
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "toastr.warning('" + ex.Message + "')", true);
        }
    }
    protected void btnSearchClear_Click(object sender, EventArgs e)
    {
        txtSearchCustName.Text = "";
        ddlSearchTransType.SelectedIndex = 0;
        GridFill(1);
        txtSearchCustName.Focus();
    }
    protected void gvRepayment_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Print")
        {
            GridViewRow row = (GridViewRow)((ImageButton)e.CommandSource).NamingContainer;
            int index = row.RowIndex;
            string ID_CustomerRePayment = gvRepayment.DataKeys[index]["ID_CustomerRePayment"].ToString();

            string popupreport = "<script> window.open('PrintReport.aspx?Type=CRP&ID=" + ID_CustomerRePayment + "', 'self', 'left=200,top=50,width=900,height=680,status=0,resizable=0,fullscreen=0,scrollbars=1');</script>";
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "Success", popupreport, false);

        }
    }
}