using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebPharma.Business;
using WebPharma.Business.Masters;
using WebPharma.Business.WebPharma;

public partial class DayClose1 : System.Web.UI.Page
{
    private const string DIVCOUNT = "DivCount";
    public Int16 DivCount
    {
        get { return Convert.ToInt16(this.ViewState[DIVCOUNT]); }
        set { this.ViewState[DIVCOUNT] = value.ToString(); }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                Page.Header.DataBind();
                steparametetrs();

                BindData();
                ddlBranch.SelectedValue = Session["mainstore"].ToString();
                lblcompanyname.Text = ddlBranch.SelectedItem.Text;
                if (Convert.ToBoolean(Session["MultiStore"].ToString()) == true)
                {
                    ddlBranch.Enabled = true;
                }
                else
                {
                    ddlBranch.Enabled = false;
                }

                txtCloseBalanceDate.Text = DateTime.Now.ToShortDateString();
                txtCloseBalanceDate.Attributes.Add("readonly", "readonly");
                txtOpenCashbalance.Attributes.Add("readonly", "readonly");
                txtTotalRecepit.Attributes.Add("readonly", "readonly");
                txtTotPayment.Attributes.Add("readonly", "readonly");
                txtClosingbalance.Attributes.Add("readonly", "readonly");
                txtLiabilty.Attributes.Add("readonly", "readonly");
                txtStock.Attributes.Add("readonly", "readonly");
                //txtCloseBalanceDate_CalendarExtender.StartDate = DateTime.Now;
                bindGridata();
                hfUser.Value = Request.QueryString["uid"];
                hfClient.Value = Request.QueryString["clnt"];
            }
            CheckUserAccess();

            //ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "ClearAdd();", true);
        }
        catch (Exception ex)
        {
            Populate.WarningMsg(this, ex.Message);
        }
    }

    private void steparametetrs()
    {
        hfClient.Value = Request.QueryString["clnt"];
        hfUser.Value = Request.QueryString["uid"];

        ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "ClearAdd();", true);
        BlFunction blfunctions = new BlFunction();
        Int16 type = Convert.ToInt16(blfunctions.GetScalarValue("Settings", "isnull(( select Value", "Name='SEC06'),0)", ""));
        ViewState["type"] = type.ToString();
        if (type > 0)
        {
            divaccountlinking.Style.Add("display", "block");
            Divdayend.Style.Add("display", "none");
            DivCount = 10;
        }
        else
        {
            divaccountlinking.Style.Add("display", "none");
            Divdayend.Style.Add("display", "block");
            DivCount = 11;
        }

        divbranch.Style.Add("display", "none");
        divdayfinished.Style.Add("display", "none");
        divCashdetails.Style.Add("display", "none");
        divDateDetails.Style.Add("display", "none");
    }

    protected void CheckUserAccess()
    {
        BlUserAccess bluseraccess = new BlUserAccess();
        Populate populate = new Populate();
        bluseraccess.FK_Users = Convert.ToInt64(hfUser.Value);
        bluseraccess.FK_Pages = 63;
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
        populate.FillDropdownList(ddlBranch, "Store", "stoName", "ID_Store", "stoName", "     Active='True'   and FK_Client = " + hfClient.Value, "");
        //populate.FillDropdownList(ddlCategory, "CategoryFirst", "Name", "ID_CategoryFirst", "Name", "  FK_Client= " + hfClient.Value.Split(',')[0], "");
        populate = null;
        ddlBranch.Focus();
    }
    private void bindGridata()
    {

        DataSet dt = new DataSet();
        BlDayEnd bdend = new BlDayEnd();
        //  bdend.FK_Client = Convert.ToInt64(hfClient.Value);
        bdend.FK_Client = Convert.ToInt64(Request.QueryString["clnt"]);
        bdend.FK_Store = Convert.ToInt16(ddlBranch.SelectedValue);
        bdend.FromDate = Convert.ToDateTime(txtCloseBalanceDate.Text);
        dt = bdend.SelectGridData();
        DataSet dtLiabilityDetails = new DataSet();
        dtLiabilityDetails = bdend.LiabilityDetails();
        ViewState["TempTable"] = dt;
        txtLiabilty.Text = dtLiabilityDetails.Tables[0].Rows[0]["Closing"].ToString();
        txtStock.Text = dtLiabilityDetails.Tables[1].Rows[0]["StockValue"].ToString();
        gvTransactionDetails.DataSource = dt.Tables[0];
        gvTransactionDetails.DataBind();
        grdIndent.DataSource = dt.Tables[1];
        grdIndent.DataBind();
        DataSet ds = bdend.Payementreceiptdetails();
        txtOpenCashbalance.Text = ds.Tables[0].Rows[0]["OpeningBalance"].ToString();
        txtTotPayment.Text = ds.Tables[1].Rows[0]["Payment"].ToString();
        txtTotalRecepit.Text = ds.Tables[2].Rows[0]["Receipt"].ToString();

        Decimal Liability = Convert.ToDecimal(dtLiabilityDetails.Tables[0].Rows[0]["Closing"].ToString());
        // int Liability=Convert.ToInt32(dtLiabilityDetails.Tables[0].Rows[0]["Closing"].ToString());
        Decimal StockValue = Convert.ToDecimal(dtLiabilityDetails.Tables[1].Rows[0]["StockValue"].ToString());
        //lblDifference.Text = Convert.ToString(Liability - StockValue);
        lblDifference.ForeColor = System.Drawing.Color.Red;
        lblDifference.Text = "Difference=" + Convert.ToString(Liability - StockValue);
        txtClosingbalance.Text = Convert.ToString(Convert.ToDecimal(txtOpenCashbalance.Text) + Convert.ToDecimal(txtTotalRecepit.Text) - Convert.ToDecimal(txtTotPayment.Text));
        //Convert.ToString(txtLiabilty.Text.ToString()) - Convert.ToString(txtStock.Text.ToString())
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        long status = 0;
        BlFunction blfunctions = new BlFunction();
        if (btnSave.Text == "Add")
        {
            bindGridata();
            btnSave.Text = "Next";
            DivCount = 3;
            Divdayend.Style.Add("display", "none");
            divbranch.Style.Add("display", "none");
            divCashdetails.Style.Add("display", "none");
            divDateDetails.Style.Add("display", "block");
            //  divDateDetails.Style.Add("Width", "100%");
            btnClose.Visible = true;
        }
        else if (btnSave.Text == "Next" && DivCount == 3)
        {
            bindGridata();
            btnSave.Text = "Next";
            DivCount = 11;
            Divdayend.Style.Add("display", "none");
            divbranch.Style.Add("display", "none");
            divDateDetails.Style.Add("display", "none");

            divCashdetails.Style.Add("display", "block");
            btnClose.Visible = true;
            validatefield();
        }
        else if (btnSave.Text == "Next" && DivCount == 11)
        {
            bindGridata();
            btnSave.Text = "Process";
            DivCount = 1;
            Divdayend.Style.Add("display", "none");
            divDateDetails.Style.Add("display", "none");
            divbranch.Style.Add("display", "block");
            divCashdetails.Style.Add("display", "none");
            btnClose.Visible = false;
            //btnSave.Text = "Next";
            //DivCount = 1;
            //Divdayend.Style.Add("display", "none");
            //divbranch.Style.Add("display", "none");
            //divCashdetails.Style.Add("display", "block");
        }
        else if (btnSave.Text == "Add" && DivCount == 10)
        {
            divaccountlinking.Style.Add("display", "none");

            Divdayend.Style.Add("display", "none");
            divbranch.Style.Add("display", "none");
            lblFinishMessage.Text = " NO PERMISSION  FOR DAY END PROCESS ";
            divdayfinished.Style.Add("display", "block");
            btnSave.Text = "Close";
        }
        else if (String.Compare(btnSave.Text, "Close") == 0)
        {
            PageRedirection();
        }
        else if (btnSave.Text == "Process")
        {
            BlDayEnd blDayEnd = new BlDayEnd();

            blDayEnd.UserCode = hfUser.Value;
            blDayEnd.FK_Store = Convert.ToInt64(ddlBranch.SelectedValue);
            blDayEnd.FK_Client = Convert.ToInt64(Request.QueryString["clnt"]);
            //blDayEnd.FK_Client = Convert.ToInt64(hfClient.Value);
            blDayEnd.BranchCode = Convert.ToInt16(ddlBranch.SelectedValue);
            blDayEnd.FromDate = Convert.ToDateTime(txtCloseBalanceDate.Text);
            blDayEnd.DbDate = Convert.ToDateTime(txtCloseBalanceDate.Text);
            DataSet dtbl = (DataSet)ViewState["TempTable"];
            DataTable dtw = dtbl.Tables[0];
            ArrayList arlist = new ArrayList();
            int RowIndex = 0;
            blDayEnd.PurchaseAmount = Convert.ToDecimal(dtbl.Tables[0].Rows[0]["NetAmount"].ToString());
            blDayEnd.PurchaseLiability = Convert.ToDecimal(dtbl.Tables[0].Rows[0]["Liability"].ToString());
            blDayEnd.PReturnAmount = Convert.ToDecimal(dtw.Rows[1]["NetAmount"].ToString());
            blDayEnd.PReturnLiability = Convert.ToDecimal(dtw.Rows[1]["Liability"].ToString());
            blDayEnd.SalesAmount = Convert.ToDecimal(dtw.Rows[2]["NetAmount"].ToString());
            blDayEnd.SalesLiability = Convert.ToDecimal(dtw.Rows[2]["Liability"].ToString());
            blDayEnd.SReturnAmount = Convert.ToDecimal(dtw.Rows[3]["NetAmount"].ToString());
            blDayEnd.SReturnLiability = Convert.ToDecimal(dtw.Rows[3]["Liability"].ToString());
            blDayEnd.DamageStock = Convert.ToDecimal(dtbl.Tables[2].Rows[0]["DamageStock"].ToString());
            blDayEnd.IndentFrom = Convert.ToDecimal(dtbl.Tables[1].Rows[0]["PreviousLiability"].ToString());
            blDayEnd.IndentTo = Convert.ToDecimal(dtbl.Tables[1].Rows[0]["CurrentLiability"].ToString());
            blDayEnd.openingbalance = Convert.ToDecimal(txtOpenCashbalance.Text);
            blDayEnd.Stockvalue = Convert.ToDecimal(txtStock.Text);
            blDayEnd.Receipt = Convert.ToDecimal(txtTotalRecepit.Text);
            blDayEnd.Payment = Convert.ToDecimal(txtTotPayment.Text);
            blDayEnd.ClosingLiability = Convert.ToDecimal(txtLiabilty.Text);
            blDayEnd.Closingbalance = Convert.ToDecimal(txtClosingbalance.Text);
            blDayEnd.MasterID = 1;
            if (validatefield())
            {

                if (hdfDayend.Value == string.Empty)
                {
                    if (hfIsAdd.Value.ToLower() == "true")
                    {

                        status = blDayEnd.InsertDayendData();
                        if (status > 0)
                        {
                            divbranch.Style.Add("display", "none");
                            divdayfinished.Style.Add("display", "block");
                            btnSave.Text = "Close";
                            //ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "ShowAddDiv();", true);
                            //Populate.InsertMsg(this);
                        }
                        else if (status ==-2)
                        {
                            validatefields();
                            divbranch.Style.Add("display", "none");
                            lblFinishMessage.Text = "DAYEND PROCESS ALREADY DONE";
                            divdayfinished.Style.Add("display", "block");
                            btnSave.Text = "Close";
                            // ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "ShowAddDiv();", true);
                            // ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "toastr.error('Cannot insert data')", true);
                        }
                        else
                        {
                            validatefields();
                            divbranch.Style.Add("display", "none");
                            lblFinishMessage.Text = "DAY END PROCESS FAILED";
                            divdayfinished.Style.Add("display", "block");
                            btnSave.Text = "Close";
                            // ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "ShowAddDiv();", true);
                            // ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "toastr.error('Cannot insert data')", true);
                        }
                    }
                    else
                    {
                        ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "toastr.warning('User does not have permission to add')", true);
                        ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "ShowAddDiv();", true);
                    }
                }

            }



        }
    }

    public void PageRedirection()
    {
        Response.Redirect("Default.aspx");
    }
    protected void btnProcess_Click(object sender, EventArgs e)
    {
    }

    protected void btnScriptFill_Click(object sender, EventArgs e)
    {
    }
    protected void btnClose_Click(object sender, EventArgs e)
    {
        PageRedirection();
    }
    protected bool validatefield()
    {
        long status = 0;
        bltErrorList.Items.Clear();
        if (ddlBranch.SelectedValue == "0")
        {
            bltErrorList.Items.Add("*Select Branch Type");
            return false;
        }
        BlDayEnd blDayEnd = new BlDayEnd();
        blDayEnd.FK_Store = Convert.ToInt64(ddlBranch.SelectedValue);
        blDayEnd.FromDate = Convert.ToDateTime(txtCloseBalanceDate.Text);
        blDayEnd.MasterID = 0;
        status = blDayEnd.InsertDayendData();
        if (status == -2)
        {
         
            //bltErrorList.Items.Add("*DayCloseProcess Already Done");
            divbranch.Style.Add("display", "none");
            divDateDetails.Style.Add("display", "none");
            divCashdetails.Style.Add("display", "none");
          //  div.Style.Add("display", "none");
            divdayfinished.Style.Add("display", "block");
            lblFinishMessage.Text = "DAYEND PROCESS ALREADY DONE";
            btnSave.Visible = false;
            btnClose.Visible = true;
            return false;
        }

        else
        {
           
            return true;
        }
    }
    protected bool validatefields()
    {

        bltErrorList.Items.Add("Canot process");
        return true;
    }

    private void clear()
    {
        gvTransactionDetails.DataSource = null;
        gvTransactionDetails.DataBind();
        grdIndent.DataSource = null;
        grdIndent.DataBind();
        txtLiabilty.Text = "";
        txtStock.Text = "";
        ViewState["TempTable"] = "";
        txtOpenCashbalance.Text = "";
        txtTotalRecepit.Text = "";
        txtTotPayment.Text = "";
        txtClosingbalance.Text = "";
    }
    protected void btngrid_Click(object sender, EventArgs e)
    {
        clear();
        bindGridata();
    }


}