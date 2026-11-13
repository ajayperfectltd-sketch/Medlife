using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebPharma.Business.Masters;
using WebPharma.Business.Accounts;
using System.Data;
using System.Globalization;

public partial class WebPharmaTransaction : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {

                ddlTransType.Focus();
                Page.Header.DataBind();
                BindData();
                steparametetrs();

                CheckUserAccess();
                HdValuestore.Value = Session["mainstore"].ToString();
            }
        }
        catch (Exception ex)
        {
            Populate.WarningMsg(this, ex.Message);
        }

    }

    private void steparametetrs()
    {
        Session["SessionName"] = "";
        txtDate.Text = Convert.ToDateTime(DateTime.Now).ToShortDateString();
        txtToDate.Text = Convert.ToDateTime(DateTime.Now).ToShortDateString();
        txtFromDate.Text = Convert.ToDateTime(DateTime.Now).ToShortDateString();


        txtDate.Attributes.Add("readonly", "readonly");
        txtToDate.Attributes.Add("readonly", "readonly");
        txtFromDate.Attributes.Add("readonly", "readonly");

        hfClient.Value = Request.QueryString["clnt"];
        hfUser.Value = Request.QueryString["uid"];
        lblCustomerBalanceAmount.Text = "";
        ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "ClearAdd();", true);
        divAdd.Style.Add("display", "block");
    }
    public void BindData()
    {
        Populate populate = new Populate();
        populate.FillTransType(ddlTransType);


    }
    protected void CheckUserAccess()
    {
        BlUserAccess bluseraccess = new BlUserAccess();
        Populate populate = new Populate();
        bluseraccess.FK_Users = Convert.ToInt64(hfUser.Value);
        bluseraccess.FK_Pages = 86;
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
    protected void ddlTransType_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            Txtcusid.Text = "";
            txtcustomername.Text = "";
            lblCustomerBalanceAmount.Text = "";
            ViewState["ddltype"] = ddlTransType.SelectedValue.ToString();
            Populate populate = new Populate();
            if (Convert.ToInt16(ddlTransType.SelectedValue) == 1)
            {

                lblCreditType.Text = "Credit Account";
                lblCustomer.Text = "Customer";
                liCustomerSupplier.Style.Add("display", "block");

                populate.FillCreditAccountAll(ddlCreditAccount, true);

                //reqSup.Enabled = false;
                //reqCus.Enabled = true;
            }
            else
            {
                lblCreditType.Text = "Debit Account";
                lblCustomer.Text = "Supplier";
                liCustomerSupplier.Style.Add("display", "block");



                populate.FillCreditAccountAll(ddlCreditAccount, false);
                ddlCreditAccount.Enabled = true;
                //  reqSup.Enabled = true;
                // reqCus.Enabled = false;
                ddlCreditAccount.Focus();

            }
            //ddlCreditAccount.SelectedValue = "2";
            //ddlCreditAccount_SelectedIndexChanged(sender, e);
        }
        catch (Exception ex)
        {
        }
    }
    protected void ddlCreditAccount_SelectedIndexChanged(object sender, EventArgs e)
    {
        Populate populate = new Populate();
        try
        {




            if (ddlCreditAccount.SelectedValue == "1")
            {
                //  populate.FillDropdownList(ddlCustomer, "Customer", "Name", "ID_Customer", "Name", " FK_Client=" + hfClient.Value.Split(',')[0] + " AND Active='True'", "");


            }
            if (ddlCreditAccount.SelectedValue == "2")
            {
                //  populate.FillDropdownList(ddlCustomer, "Supplier", "Name", "ID_Customer", "Name", " FK_Client=" + hfClient.Value.Split(',')[0] + " AND Active='True'", "");
                //  populate.FillDropdownList(ddlCustomer, "Supplier", "Name", "ID_Supplier", "Name", "FK_Client=" + hfClient.Value.Split(',')[0] + " ", "");

            }
            // ddlCustomer.Focus();

        }

        catch (Exception ex1)
        {

        }
        //Populate populate = new Populate();
        //if (ddlCustomer.Text == "customer")
        //{
        //    populate.FillDropdownList(ddlCustomer, "Customer", "Name", "ID_Customer", "Name", " FK_Client=" + hfClient.Value.Split(',')[0] + " AND Active='True'", "");
        //}
        //if (ddlCustomer.SelectedItem.Text == "supplier")
        //{
        //    populate.FillDropdownList(ddlCustomer, "Supplier", "Name", "ID_Supplier", "Name", "FK_Client=" + hfClient.Value.Split(',')[0] + " ", "");
        //}
        //ddlCustomer.Focus();

    }
    //protected void ddlCustomer_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    BlWebPharmaTransaction WebPharmaTransactions = new BlWebPharmaTransaction();
    //    try
    //    {
    //        if (ddlCreditAccount.SelectedItem.Text == "Customer")
    //        {
    //            WebPharmaTransactions.Type = 1;
    //        }
    //        if (ddlCreditAccount.SelectedItem.Text == "Supplier")
    //        {
    //            WebPharmaTransactions.Type = 2;
    //        }
    //        {
    //            WebPharmaTransactions.FK_Name = Convert.ToInt64(ddlCustomer.SelectedValue);

    //        }
    //        WebPharmaTransactions.SelectCustBalanceAmount();
    //        lilblCustomerBalanceAmount.Style.Add("display", "block");

    //        if (WebPharmaTransactions.Type == 1)
    //        {
    //            lblCustomerBalanceAmount.Text = "Customer Credit Amount =" + WebPharmaTransactions.Amount.ToString();
    //        }
    //        if (WebPharmaTransactions.Type == 2)
    //        {
    //            lblCustomerBalanceAmount.Text = " Supplier Debit Amount =" + WebPharmaTransactions.Amount.ToString();
    //        }


    //    }
    //    catch (Exception ex)
    //    {
    //        lblCustomerBalanceAmount.Text = "";
    //    }


    //}

    protected void imgbtnAdd_Click(object sender, ImageClickEventArgs e)
    {
        lblCustomerBalanceAmount.Text = "";
        lockcntrl(true);
        btnClear.Visible = true;
        btnSubmit.Visible = true;
        ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "ClearAdd();", true);
        divAdd.Style.Add("display", "block");
        divView.Style.Add("display", "none");
        liCustomerSupplier.Style.Add("display", "none");
      
    }
    protected void imgbtnView_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            lblCustomerBalanceAmount.Text = "";
            divAdd.Style.Add("display", "none");
            divView.Style.Add("display", "block");
            // ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "ClearAdd();", true);

            GridFill(1);
        }
        catch (Exception ex)
        {
            Populate.WarningMsg(this, ex.Message);
        }
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {


            long StatusId;

            if (ddlTransType.SelectedValue != "0" && ddlCreditAccount.SelectedValue != "0" && (Convert.ToDecimal(txtAmount.Text == "" ? "0" : txtAmount.Text) > 0))
            //if (ddlTransType.SelectedValue != "0" && ddlCreditAccount.SelectedValue != "0" && ddlCustomer.SelectedValue != "0" && (Convert.ToDecimal(txtAmount.Text == "" ? "0" : txtAmount.Text) > 0))
            {
                StatusId = SaveData();

                if (StatusId > 0)
                {


                    ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "toastr.success('Data Inserted SuccessFully');ClearAdd(); ShowAddDiv();", true);
                    GridFill(1);
                    // cleared();
                    lblCustomerBalanceAmount.Text = "";


                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "ShowAddDiv();", true);
                    ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "toastr.error('Cannot insert data')", true);
                }
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "ShowAddDiv();", true);
                ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "toastr.error('Please Enter Mandatory Feields')", true);
            }
            divView.Attributes.Add("style", "display:none;");
            divAdd.Attributes.Add("style", "display:block;");


        }
        catch (Exception ex)
        {

        }
    }

    protected long SaveData()
    {
        long status = 0;
        DataTable dtbl = new DataTable();
        BlWebPharmaTransaction WebPharmaTransaction = new BlWebPharmaTransaction();
        //  WebPharmaTransaction.TransType = ddlTransType.SelectedValue == "1" ? "Reciept" : "Payment";
        WebPharmaTransaction.TransType = Convert.ToString(ddlTransType.SelectedItem.Value);
        WebPharmaTransaction.TransDate = DateTime.ParseExact(txtDate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
        WebPharmaTransaction.EnteredOn = Convert.ToDateTime(DateTime.Now);
        WebPharmaTransaction.UserCode = hfUser.Value;
        WebPharmaTransaction.FK_Client = Convert.ToInt64(hfClient.Value);

        WebPharmaTransaction.FK_Store = Convert.ToInt64(HdValuestore.Value);
        WebPharmaTransaction.Amount = Convert.ToDecimal(txtAmount.Text);
        WebPharmaTransaction.TransferTo = ddlCreditAccount.SelectedItem.Text;


        if (ddlCreditAccount.SelectedItem.Text == "Customer")
        {
            WebPharmaTransaction.Type = 1;
        }
        if (ddlCreditAccount.SelectedItem.Text == "Supplier")
        {
            WebPharmaTransaction.Type = 2;
        }

        WebPharmaTransaction.Remarks = txtRemarks.Text;
        WebPharmaTransaction.FK_Name = Convert.ToInt64(Txtcusid.Text);
        WebPharmaTransaction.XmlTransfertToAcc = Session["SessionName"].ToString();
        Session["SessionName"] = "";
        if (hdfWebPharmaTransaction.Value == string.Empty)

            if (hfIsAdd.Value.ToLower() == "true" && (WebPharmaTransaction.Type == 1 || WebPharmaTransaction.Type == 2))
            {
                status = WebPharmaTransaction.insertdata();
                if (status > 0)
                {
                    ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "ShowAddDiv();", true);
                    //Populate.InsertMsg(this);
                }

                else
                {
                    ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "ShowAddDiv();", true);
                    ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "toastr.error('Cannot insert data')", true);
                }

            }

        return status;

    }


    protected void GridFill(int currentPage)
    {
        BlWebPharmaTransaction WebPharmaTransaction = new BlWebPharmaTransaction();
        DataTable dtbl = new DataTable();

        WebPharmaTransaction.FK_Client = Convert.ToInt64(hfClient.Value);
        WebPharmaTransaction.UserCode = hfUser.Value;
        WebPharmaTransaction.PageSize = 10;
        WebPharmaTransaction.PageIndex = Convert.ToInt16(currentPage);

        // blAccountHeadSubTransactions.VoucherNo = txtSearchVoucherNo.Text == "" ? 0 : Convert.ToInt64(txtSearchVoucherNo.Text);
        //  WebPharmaTransaction.FromDate = DateTime.ParseExact(txtFromDate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
        //   WebPharmaTransaction.ToDate = DateTime.ParseExact(txtToDate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
        WebPharmaTransaction.FromDate = ((txtFromDate.Text == null) ? Convert.ToDateTime(DateTime.Now) : Convert.ToDateTime(txtFromDate.Text));
        WebPharmaTransaction.ToDate = ((txtToDate.Text == null) ? Convert.ToDateTime(DateTime.Now) : Convert.ToDateTime(txtToDate.Text));



        dtbl = WebPharmaTransaction.SelectTransactionData();



        gvWebPharmaTransaction.DataSource = dtbl;
        gvWebPharmaTransaction.DataBind();
        if (dtbl.Rows.Count > 0)
        {
            int recordCount = Convert.ToInt32(dtbl.Rows[0]["RecordCount"].ToString());
            this.PopulatePager(recordCount, currentPage);
        }
        else
        {
            rptPager.DataSource = null;
            rptPager.DataBind();
        }
        WebPharmaTransaction = null;
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


    protected void btnSearch_Click(object sender, EventArgs e)
    {
        GridFill(1);
    }

    protected void Page_Changed(object sender, EventArgs e)
    {
        try
        {
            int pageIndex = int.Parse((sender as LinkButton).CommandArgument);
            GridFill(pageIndex);
        }
        catch (Exception ex)
        {
            Populate.WarningMsg(this, ex.Message);
        }
    }
    protected void gvWebPharmaTransaction_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {


        try
        {
            //ID_AccountHeadSub
            ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "ShowAddDiv();", true);
            BlWebPharmaTransaction WebPharmaTransactions = new BlWebPharmaTransaction();
            WebPharmaTransactions.MasterId = Convert.ToInt64(gvWebPharmaTransaction.DataKeys[e.NewSelectedIndex]["ID_WebPharmaTransaction"].ToString());
            WebPharmaTransactions.FK_Client = Convert.ToInt64(hfClient.Value);
            WebPharmaTransactions.UserCode = hfUser.Value;
            WebPharmaTransactions.FromDate = Convert.ToDateTime(txtFromDate.Text);
            WebPharmaTransactions.ToDate = Convert.ToDateTime(txtToDate.Text);
            WebPharmaTransactions.SelectData();
            ddlTransType.SelectedValue = WebPharmaTransactions.TransType == "Receipt" ? "1" : "2";
            ddlTransType_SelectedIndexChanged(sender, e);
            if (WebPharmaTransactions.TransType == "Receipt")
            {
                lblCreditType.Text = "Credit Account";
                lblCustomer.Text = "Customer";
            }
            if (WebPharmaTransactions.TransType == "Payment")
            {
                lblCreditType.Text = "Debit Account";
                lblCustomer.Text = "Supplier";
            }
            ddlCreditAccount.SelectedValue = WebPharmaTransactions.Type.ToString();


            Txtcusid.Text = WebPharmaTransactions.FK_Name.ToString();
            txtcustomername.Text = WebPharmaTransactions.Name.ToString();
            
            // ddlCustomer.Items.Clear();
            // ddlCustomer.Items.Add(new ListItem(WebPharmaTransactions.Name.ToString(), "0"));
            // ddlCustomer.SelectedItem.Value = "0";
            //ddlCustomer.SelectedValue = (WebPharmaTransactions.FK_Name).ToString();

            txtDate.Text = WebPharmaTransactions.TransDate.ToShortDateString();


            txtAmount.Text = WebPharmaTransactions.Amount.ToString();
            txtRemarks.Text = WebPharmaTransactions.Remarks;
            divAdd.Style.Add("display", "block");
            divView.Style.Add("display", "none");

            liCustomerSupplier.Style.Add("display", "block");
            btnSubmit.Visible = false;
            btnClear.Visible = false;
            lockcntrl(false);
        }
        catch (Exception ex)
        {
            Populate.WarningMsg(this, ex.Message);
        }

    }
    private void cleared()
    {
        ddlTransType.SelectedItem.Value = "0";
        ddlCreditAccount.SelectedItem.Value = "0";
        //  ddlCustomer.SelectedItem.Value = "0";
        ddlCreditAccount.SelectedItem.Text = "Select";
        txtDate.Text = DateTime.Now.ToShortDateString();
        txtAmount.Text = "0";
        txtRemarks.Text = String.Empty;
        lblCustomerBalanceAmount.Text = "";
        liCustomerSupplier.Style.Add("display", "none");
    }
    private void lockcntrl(bool Show)
    {
        txtRemarks.Enabled = Show;
        txtDate.Enabled = Show;
        txtAmount.Enabled = Show;
        ddlCreditAccount.Enabled = Show;
        ddlTransType.Enabled = Show;
        // ddlCustomer.Enabled = Show;

    }

    protected void btnLoanTransfer_Click(object sender, EventArgs e)
    {

        try
        {

            //((TextBox)CommonPopup2.FindControl("txtRefno")).Text = txt.Text.Trim();
            //((TextBox)CommonPopup2.FindControl("txtName")).Text = txtAhName.Text.Trim();
            //((HiddenField)CommonPopup2.FindControl("hfColumn")).Value = "Name";

            divAdd.Style.Add("display", "block");
            divView.Style.Add("display", "none");
              Populate populate = new Populate();
          populate.FillDropdownList((DropDownList)CommonPopup1.FindControl("ddlBank"), "Bank", "Name", "ID_Bank", "ID_Bank", "Active='True' AND FK_Client=" + hfClient.Value, "");
          populate = null;

           mpCommon.Show();
        }
        catch (Exception ex)
        {
        }

    }


    protected void btnClear_Click(object sender, EventArgs e)
    {

    }
    protected void btnGos_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            ClearCustomer();
            ((TextBox)CommonPopup2.FindControl("txtName")).Text = txtcustomername.Text.Trim();
            ((TextBox)CommonPopup2.FindControl("txtRefno")).Text = Txtcusid.Text.Trim();
            ((HiddenField)CommonPopup2.FindControl("hfColumn")).Value = "Name";
            if (ViewState["ddltype"].ToString() == "1")
            {
                customerfill(1);
            }
            if (ViewState["ddltype"].ToString() == "2")
            {
           supplierfill(1);
            }
            divAdd.Style.Add("display", "block");
            divView.Style.Add("display", "none");
            //txtSubHeadRefno.Focus();
        }
        catch (Exception ex)
        {
        }

    }






    private void ClearCustomer()
    {
        txtcustomername.Text = "";
        Txtcusid.Text = "";
        //txtAccountSubhead.Text = "";
        // txtRefno.Text = "";
    }

    public void customerfill(int pageIndex)
    {


        // ClearAccountHead();
        Populate Populate = new Populate();
        CommonPopup2.CallingFunction = "customerfill";
        string strName = ((TextBox)CommonPopup2.FindControl("txtName")).Text;
        string Refno = ((TextBox)CommonPopup2.FindControl("txtRefno")).Text;
        string searchCol = ((HiddenField)CommonPopup2.FindControl("hfColumn")).Value;
        searchCol = searchCol == string.Empty ? "[Name]" : "[" + searchCol + "]";
        Refno = Refno == string.Empty || Refno == null ? "0" : Refno;

        // Populate.FillGrid((GridView)CommonPopup1.FindControl("gdvSearch"), "AccountHead", "Name , ID_AccountHead, ID_AccountHead As [Ref No],ShortName,(CASE WHEN AccountType=1 then 'NORMAL' WHEN AccountType=2 then 'SYSTEM'  WHEN AccountType=3 Then 'BANK' WHEN AccountType=4 THEN 'CASH' WHEN  AccountType=5 THEN 'CUSTOMER' ELSE 'SUPPLIER' END) as [Account Type]", "ID_AccountHead", "", "FK_Client=" + hfClient.Value.Split(',')[0] + " AND (ID_AccountHead =" + Refno + " OR 0=" + Refno + ") AND Active=1 ", searchCol + "  LIKE '" + strName + "%'", pageIndex, 10, "", (Repeater)CommonPopup1.FindControl("rptPager"), this.Page);
        // new Populate().FillDropdownList(ddlAccountHead, "AccountHead", "Name", "ID_AccountHead", "ID_AccountHead", " Active=1 and FK_Client = " + hfClient.Value, "");
        Populate.FillGrid((GridView)CommonPopup2.FindControl("gdvSearch"), "Customer", "Name ,ID_Customer as ID,  ID_Customer As [Ref No]", "FK_Client=" + hfClient.Value.Split(',')[0] + " AND (ID_Customer =" + Refno + " OR 0=" + Refno + ") AND Active=1 ", searchCol + "  LIKE '" + strName + "%' and  Active=1", pageIndex, 10, "", (Repeater)CommonPopup2.FindControl("rptPager"), this.Page);
        Populate = null;
        //  txtSubHeadRefno.Focus();
        if (txtcustomername.Text == string.Empty)
        {
            lblPopupHeader.Text = "Customer";
            mpCommon1.Show();
        }

    }

    public void supplierfill(int pageIndex)
    {


        // ClearAccountHead();
        Populate Populate = new Populate();
        CommonPopup2.CallingFunction = "supplierfill";
        string strName = ((TextBox)CommonPopup2.FindControl("txtName")).Text;
        string Refno = ((TextBox)CommonPopup2.FindControl("txtRefno")).Text;
        string searchCol = ((HiddenField)CommonPopup2.FindControl("hfColumn")).Value;
        searchCol = searchCol == string.Empty ? "[Name]" : "[" + searchCol + "]";
        Refno = Refno == string.Empty || Refno == null ? "0" : Refno;

        // Populate.FillGrid((GridView)CommonPopup1.FindControl("gdvSearch"), "AccountHead", "Name , ID_AccountHead, ID_AccountHead As [Ref No],ShortName,(CASE WHEN AccountType=1 then 'NORMAL' WHEN AccountType=2 then 'SYSTEM'  WHEN AccountType=3 Then 'BANK' WHEN AccountType=4 THEN 'CASH' WHEN  AccountType=5 THEN 'CUSTOMER' ELSE 'SUPPLIER' END) as [Account Type]", "ID_AccountHead", "", "FK_Client=" + hfClient.Value.Split(',')[0] + " AND (ID_AccountHead =" + Refno + " OR 0=" + Refno + ") AND Active=1 ", searchCol + "  LIKE '" + strName + "%'", pageIndex, 10, "", (Repeater)CommonPopup1.FindControl("rptPager"), this.Page);
        // new Populate().FillDropdownList(ddlAccountHead, "AccountHead", "Name", "ID_AccountHead", "ID_AccountHead", " Active=1 and FK_Client = " + hfClient.Value, "");
        Populate.FillGrid((GridView)CommonPopup2.FindControl("gdvSearch"), "Supplier", "Name ,ID_Supplier as ID,  ID_Supplier As [Ref No]", "FK_Client=" + hfClient.Value.Split(',')[0] + " AND (ID_Supplier =" + Refno + " OR 0=" + Refno + ") AND Active=1 ", searchCol + "  LIKE '" + strName + "%' and  Active=1", pageIndex, 10, "", (Repeater)CommonPopup2.FindControl("rptPager"), this.Page);
        Populate = null;
        //  txtSubHeadRefno.Focus();
        if (txtcustomername.Text == string.Empty)
        {
            lblPopupHeader.Text = "Supplier";
            mpCommon1.Show();
        }

    }


    public void SetDetails(string strName, string strRefno)
    {


        DataTable dt = new DataTable();
        DataTable dt1 = new DataTable();
        BlAccountHeadSub blAccountSub = new BlAccountHeadSub();

      
        if (CommonPopup2.CallingFunction == "customerfill")
        {
            Txtcusid.Text = strRefno;
            txtcustomername.Text = strName;
            // chkActive.Focus();
        }

        if (CommonPopup2.CallingFunction == "supplierfill")
        {
            Txtcusid.Text = strRefno;
            txtcustomername.Text = strName;
            // chkActive.Focus();
        }

    }




}