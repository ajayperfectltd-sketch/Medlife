
/*----------------------------------------------------------------------------------------------------------------------------------------------------------
Purpose    : Insert//Delete/SELECT   MULTIPLE  Cash Transaction
Author     : Bibin TC
Copyright  : Perfect Software Solutions (Clt) Pvt Ltd
Created on : 03/05/2018
WebPharma
On			 By		          	OMID
----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

 * 
 * --------------------------------------------------------------------------------------------------------------------------------------------------------------------------------*/
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
using System.Web.UI.WebControls;
using Excel;
using WebPharma.Business;
using System.IO;



public partial class AccountHeadSubTransMultiple : System.Web.UI.Page
{
    private const string Excel = "Excel";
    private const string ExcelData = "ExcelData";
    public static DataSet dsExcel;
    public static decimal RowCount = 0;
    public static string ExcelPath;
    //Grid Constants
    private const string COL_CODE = "Code";
    private const string COL_NAME = "Name";
    private const string COL_SUBNO = "SubNo";
    private const string COL_SUBID = "SubName";
    private const string COL_NARRATION = "Narration";
    private const string COL_PARTY = "Party";
    private const string COL_AMOUNT = "Amount";
    private const string COL_TYPE = "Type";
    private const string COL_BILLNO = "BillNo";
    private const string COL_BILLDATE = "BillDate";
    private const string COL_CHECKNO = "ChequeNo";
    private const string COL_CHECKDATE = "ChequeDate";
   // private const string COL_GROUPID = "GroupId";
    //private const string COL_TAXAMOUNT = "TaxAmount";
   // private const string COL_TAXDETAILS = "TaxDetails";
    //private const string COL_TRANSFER = "Transfer";
   // private const string COL_BATCH = "Batch";
   // private const string COL_TRANSDATE = "TransDate";
    //private const string COL_EFFECTDATE = "EffectDate";
    private const string CONSIDER_STAX = "ConsiderStax";
    private const string CONSIDER_BILLWISE = "ConsiderBillWise";
    //Grid Index
    private const byte ID_CODE = 1;
    private const byte ID_NAME = 2;
    private const byte ID_SUBCODE = 3;
    private const byte ID_SUBNAME = 4;
    private const byte ID_AMOUNT = 5;
    private const byte ID_TYPE = 6;
    private const byte ID_NARRATION = 7;
    private const byte ID_PARTY = 8;
    private const byte ID_CHECKDATE = 9;
    private const byte ID_CHECKNO = 10;
  //  private const byte TRANSDATE = 11;
   // private const byte EFFECTDATE = 12;
    private const byte ID_BillNo = 11;
    private const byte ID_BillDate = 12;
    //  private const byte groupid = 15;  
   // private const byte ID_SUBID = 8;
   // private const byte ID_TAX = 11;
   // private const byte ID_TAXDETAILS = 12;
   //private const byte ID_TRANSFER = 13;
   //private const byte ID_BATCH = 14;    

  


    protected void Page_Load(object sender, EventArgs e)
    {
        try
        
        {
            if (!IsPostBack)
            {
                HdValuestore.Value = Session["mainstore"].ToString();
                Pageloadfunction();
            }
        }
        catch (Exception ex)
        {
            Populate.WarningMsg(this, ex.Message);
        }
        
    }
    public void Delete()
    {
        try
        {
            divPopupReason.Visible = true;
            Populate Populate = new Populate();
            CommonPopupDelete1.CallingFunction = "PopupOnReason";
            ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "PopupOnReason();", true);
            mpDelete.Show();
        }
        catch (Exception ex)
        {
        }
    }
    private void Pageloadfunction()
    {
        btnSearchAccountHead.Focus();
        txtAhCode.Attributes.Add("readonly", "readonly");
        txtAhName.Attributes.Add("readonly", "readonly");
        txtAhsNumber.Attributes.Add("readonly", "readonly");
        txtAhSubName.Attributes.Add("readonly", "readonly");
        txtClrBalance.Attributes.Add("readonly", "readonly");
        txtClsBalance.Attributes.Add("readonly", "readonly");
        txtAhstBillDate.Attributes.Add("readonly", "readonly");
        txtTransDate.Attributes.Add("readonly", "readonly");
        txtEffectDate.Attributes.Add("readonly", "readonly");
        txtAhstChequeDate.Attributes.Add("readonly", "readonly");
        Page.Header.DataBind();
        ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "ClearAdd();", true);
        divAdd.Style.Add("display", "block");
        hfUser.Value = Request.QueryString["uid"];
        hfClient.Value = Request.QueryString["clnt"];
        CheckUserAccess();
    }
    protected void CheckUserAccess()
    {
        BlUserAccess bluseraccess = new BlUserAccess();
        Populate populate = new Populate();
        bluseraccess.FK_Users = Convert.ToInt64(hfUser.Value);
        bluseraccess.FK_Pages = 39;
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
   // protected void btnSubmit_Click(object sender, EventArgs e)
   //{
   //     try
   //     {
   //         if(btnSubmit.Text=="Submit")
   //         {
   //             if (gdvTrans.Rows.Count > 0)
   //             {
   //                 SaveData();                
   //             }
   //             else
   //             {
   //                 ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "toastr.warning('Please Enter Datas')", true);
   //                 ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "ShowAddDiv();", true);
   //                 cleared();
   //             }
  
   //         }
   //         if (btnSubmit.Text == "Delete")
   //         {
   //             ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "ShowAddDiv();", true);  
      
   //             Delete();
   //             //deletedata();               
   //         }
   //     }
   //     catch (Exception ex)
   //     {
   //         Populate.WarningMsg(this, ex.Message);
   //     }
   // }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            if (btnSubmit.Text == "Submit")
            {
                if (!Page.IsValid)
                {
                    return;
                }

                DateTime dtTrans, dtEffect;
                if (!DateTime.TryParse(txtTransDate.Text, out dtTrans) ||
                    !DateTime.TryParse(txtEffectDate.Text, out dtEffect))
                {
                    ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "toastr.warning('Please enter valid Trans.Date and Effect Date')", true);
                    ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "ShowAddDiv();", true);
                    return;
                }

                if (gdvTrans.Rows.Count > 0)
                {
                    SaveData();
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "toastr.warning('Please Enter Datas')", true);
                    ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "ShowAddDiv();", true);
                    cleared();
                }
            }
            if (btnSubmit.Text == "Delete")
            {
                ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "ShowAddDiv();", true);
                Delete();
            }
        }
        catch (Exception ex)
        {
            Populate.WarningMsg(this, ex.Message);
        }
    }

    public void DeleteMethod(string Reason = "0")
    {
        //string confirmValue = Request.Form["confirm_value"];
        //if (confirmValue.Length > 3)
        //    confirmValue = confirmValue.Substring(confirmValue.Length - 3, 3);
        //if (confirmValue == "Yes")
        //{
            long status = 0;
            BlAccountHeadSubTransactions blAccountSubTrn = new BlAccountHeadSubTransactions();
            blAccountSubTrn.UserCode = hfUser.Value;
            blAccountSubTrn.groupid = Convert.ToInt64(ViewState["GROUPIDS"]);
            blAccountSubTrn.Fk_Reason = Convert.ToInt64(Reason);

            //  BlAccountGroup.FK_Client = Convert.ToInt64(Reason);
            if (hfIsDelete.Value.ToLower() == "true")
            {
                status = blAccountSubTrn.DeleteData();
                if (status > 0)
                {
                    GridFill(1);
                    ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "toastr.success('Data Deleted Successfully')", true);
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "toastr.error('Cannot delete data, reference exists')", true);
                }
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "toastr.warning('User does not have permission to Delete')", true);
                //txtName.Focus();
            }
            blAccountSubTrn = null;
        //}
    }
    //private void deletedata()
    //{
    //    BlAccountHeadSubTransactions blAccountSubTrn = new BlAccountHeadSubTransactions();  
    //    long status = 0;
    //    BlAccountHeadSubTransactions blaccountheadsubtransaction = new BlAccountHeadSubTransactions();
    //    blAccountSubTrn.groupid = Convert.ToInt64(ViewState["GROUPIDS"]);
    //    blAccountSubTrn.UserCode = hfUser.Value;
       
    //    blAccountSubTrn.FK_Client = Convert.ToInt64(hfClient.Value);
    //    status = blAccountSubTrn.DeleteMultipleData();
    //    if (status > 0)
    //    {
    //        GridFill(1);
    //        ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "toastr.success('Data deleted successfully')", true);
    //       // Populate.DeleteMsg(this);
    //        cleared();
    //       divAdd.Style.Add("display", "block");
    //       divView.Style.Add("display", "none");            
    //    }
    //    else
    //    {
    //       ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "toastr.error('Cannot delete data, reference exists')", true);
    //       Populate.ReferanceMsg(this);
    //    }
    //}
    protected bool SaveData()
    {
        long statusId;
        bool status;
        status = false;
        try
        {           
                BlAccountHeadSubTransactions blaccountheadsubtransaction = new BlAccountHeadSubTransactions();
                if (!UpdateData(blaccountheadsubtransaction)) return false;
                statusId = blaccountheadsubtransaction.InsertDataMultiple();
                if (statusId > 0)
                {
                    ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "ShowAddDiv();", true);
                    Populate.InsertMsg(this);
                    gdvTrans.DataSource = null;
                    gdvTrans.DataBind();
                    cleared();
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "ShowAddDiv();", true);
                    ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "toastr.error('Cannot insert data')", true);
                    cleared();
                }
                blaccountheadsubtransaction = null;
           // }
            return status;
        }
        catch (Exception e1)
        {         
            return false;
        }     
    }
    protected bool UpdateData(BlAccountHeadSubTransactions blaccountheadsubtransaction)
    {
        BlAccountHeadSubTransactions BlAccountHeadSubTransaction = new BlAccountHeadSubTransactions();
        int count = gdvTrans.Rows[0].Cells.Count;
        //  blaccountheadsubtransaction.TransactionID = 0;
        // blaccountheadsubtransaction.TransType = ddlTransType.SelectedValue.ToString();
        blaccountheadsubtransaction.Cancelled = false;
        blaccountheadsubtransaction.UserCode = hfUser.Value;
        blaccountheadsubtransaction.FK_Client = Convert.ToInt64(hfClient.Value);
        blaccountheadsubtransaction.TransDate =Convert.ToDateTime( txtTransDate.Text);
        blaccountheadsubtransaction.EffectDate = Convert.ToDateTime(txtEffectDate.Text);
        blaccountheadsubtransaction.FK_Store = Convert.ToInt64(HdValuestore.Value);
        ArrayList arlist = new ArrayList();
        ArrayList list = new ArrayList();
        AccountHeadSubTransMultiples accountheadsubtransmultiple;
        decimal receiptamnt=0;
        decimal paymentamnt=0;
        foreach (GridViewRow gdvRow in gdvTrans.Rows)
        {
            accountheadsubtransmultiple = new AccountHeadSubTransMultiples();
            accountheadsubtransmultiple.Transfer = chkTransfer.Checked;
          //  accountheadsubtransmultiple.AhName = Convert.ToString((gdvRow.Cells[ID_NAME].Text.ToString().Replace("&nbsp;", "")) == "" ? "" : (gdvRow.Cells[ID_NAME].Text.ToString()));
          //  accountheadsubtransmultiple.FK_AccountHeadSub = Convert.ToInt32((gdvRow.Cells[ID_SUBCODE].Text.ToString().Replace("&nbsp;", "")) == "" ? "0" : (gdvRow.Cells[ID_SUBCODE].Text.ToString()));
           // accountheadsubtransmultiple.AhName = (ViewState["ID_AccountHead"].ToString());//fk_accounthead
            accountheadsubtransmultiple.AhCode = Convert.ToInt32((gdvRow.Cells[ID_CODE].Text.ToString().Replace("&nbsp;", "")) == "" ? "0" : (gdvRow.Cells[ID_CODE].Text.ToString()));//ahcode
           // accountheadsubtransmultiple.FK_AccountHeadSub =Convert.ToInt64( ViewState["ID_AccountHeadSub"]);
            accountheadsubtransmultiple.FK_AccountHeadSubName = ((gdvRow.Cells[ID_SUBCODE].Text.ToString().Replace("&nbsp;", "")) == "" ? "0" : (gdvRow.Cells[ID_SUBCODE].Text.ToString()));
            accountheadsubtransmultiple.TransType = Convert.ToString(gdvRow.Cells[ID_TYPE].Text.ToString());
            if(accountheadsubtransmultiple.TransType=="R")
            {
                accountheadsubtransmultiple.AhstAmount = Convert.ToDecimal((gdvRow.Cells[ID_AMOUNT].Text.ToString().Replace("&nbsp;", "")) == "" ? "0" : (gdvRow.Cells[ID_AMOUNT].Text.ToString()));    
                receiptamnt=receiptamnt+ accountheadsubtransmultiple.AhstAmount;
            }
              accountheadsubtransmultiple.TransType = Convert.ToString(gdvRow.Cells[ID_TYPE].Text.ToString());
            if(  accountheadsubtransmultiple.TransType=="P")

            {
                accountheadsubtransmultiple.AhstAmount = Convert.ToDecimal((gdvRow.Cells[ID_AMOUNT].Text.ToString().Replace("&nbsp;", "")) == "" ? "0" : (gdvRow.Cells[ID_AMOUNT].Text.ToString()));    
                paymentamnt=paymentamnt+ accountheadsubtransmultiple.AhstAmount;
            }

            if (count > 8)
            {  
                accountheadsubtransmultiple.AhstNarration = Convert.ToString((gdvRow.Cells[ID_NARRATION].Text.ToString().Replace("&nbsp;", "")) == "" ? "" : (gdvRow.Cells[ID_NARRATION].Text.ToString()));
                accountheadsubtransmultiple.AhstParty = Convert.ToString((gdvRow.Cells[ID_PARTY].Text.ToString().Replace("&nbsp;", "")) == "" ? "" : (gdvRow.Cells[ID_PARTY].Text.ToString()));
                if (gdvRow.Cells[ID_CHECKDATE].Text != "&nbsp;")
                accountheadsubtransmultiple.CheckDate = Convert.ToDateTime(gdvRow.Cells[ID_CHECKDATE].Text).ToString("M/d/yyyy");             
                accountheadsubtransmultiple.CheckNo = Convert.ToString((gdvRow.Cells[ID_CHECKNO].Text.ToString().Replace("&nbsp;", "")) == "" ? "" : (gdvRow.Cells[ID_CHECKNO].Text.ToString()));
                if (gdvRow.Cells[ID_BillDate].Text != "&nbsp;")
                // accountheadsubtransmultiple.Billdate = Common.DateFormat(Convert.ToDateTime(gdvRow.Cells[ID_BillDate].Text.ToString()));
                accountheadsubtransmultiple.Billdate = Convert.ToDateTime(gdvRow.Cells[ID_BillDate].Text).ToString("M/d/yyyy");
                accountheadsubtransmultiple.BillNo = Convert.ToString((gdvRow.Cells[ID_BillNo].Text.ToString().Replace("&nbsp;", "")) == "" ? "" : (gdvRow.Cells[ID_BillNo].Text.ToString()));
               // if (gdvRow.Cells[TRANSDATE].Text != "&nbsp;")
               //  accountheadsubtransmultiple.TransDate = Common.DateFormat(Convert.ToDateTime(gdvRow.Cells[TRANSDATE].Text.ToString()));
               // accountheadsubtransmultiple.TransDate = Convert.ToDateTime(gdvRow.Cells[TRANSDATE].Text).ToString("M/d/yyyy");
               // accountheadsubtransmultiple.EffectDate = Convert.ToString((gdvRow.Cells[EFFECTDATE].Text.ToString().Replace("&nbsp;", "")) == "" ? "" : (gdvRow.Cells[EFFECTDATE].Text.ToString()));
               // accountheadsubtransmultiple.EffectDate = Convert.ToDateTime(gdvRow.Cells[EFFECTDATE].Text).ToString("M/d/yyyy");            
            }
            else
            {
                accountheadsubtransmultiple.AhstNarration = "";
                accountheadsubtransmultiple.AhstParty = "";
                accountheadsubtransmultiple.CheckDate = DateTime.Now.ToString("M/d/yyyy");
                accountheadsubtransmultiple.CheckNo = "";
                accountheadsubtransmultiple.Billdate = DateTime.Now.ToString("M/d/yyyy");
                accountheadsubtransmultiple.BillNo = "";
               // accountheadsubtransmultiple.TransDate = DateTime.Now.ToString("M/d/yyyy");
               // accountheadsubtransmultiple.EffectDate =DateTime.Now.ToString("M/d/yyyy");
            }
           // accountheadsubtransmultiple.TaxDetails = Convert.ToString(gdvRow.Cells[ID_TAXDETAILS].Text.ToString().Replace("&nbsp;", ""));
            arlist.Add(accountheadsubtransmultiple);
            accountheadsubtransmultiple = null;          
        }
        if (arlist.Count > 0 && chkTransfer.Checked == true)
        {
            if (receiptamnt != paymentamnt)
            {
                ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "toastr.warning('Credit Amount and Debit Amount not Tallied')", true);
                ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "ShowAddDiv();", true);
              //  cleared();
                return false;
            }
        }
            blaccountheadsubtransaction.XmlAccountTransMultiple = Common.WriteToXml(arlist);    
            return true;
    }
    protected void ddlTransType_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void btnClear_Click(object sender, EventArgs e)
    {
        cleared();
    }
    private void cleared()
    {
        liacountsubhead.Style.Add("display", "none");
        litxtCusAddress.Style.Add("display", "none");
        lblclrbal0.Style.Add("display", "none");
        lblclbal.Style.Add("display", "none");
        divAdd.Style.Add("display", "block");
        btnClear.Visible = true;
        txtDebitAmount.Text="0.00";
        txtCreditAmount.Text = "0.00";
        txtTotalAmount.Text = "0.00";       
        ViewState["GridValue"] = null;
        btnShowData.Enabled = true;
        btnSubmit.Visible = true;
        Btninsert.Visible = true;
        btnShowData.Visible = true;
        btnExcelFormat.Visible = true;
        FileUploadExcel.Visible = true;
        gdvTrans.DataSource = null;
        gdvTrans.DataBind();
        (Session[Excel]) = null;
        btnprint.Visible = false;
    }
    protected void imgbtnAdd_Click(object sender, ImageClickEventArgs e)
    {
        btnSubmit.Text = "Submit";
        Btninsert.Visible = true;
        Liclosingbalance.Style.Add("display", "block");
        lilclarbalnce.Style.Add("display", "block");
        divAdd.Style.Add("display", "block");
        divView.Style.Add("display", "none");      
        cleared();
       

    }
    protected void imgbtnView_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            divAdd.Style.Add("display", "none");
            divView.Style.Add("display", "block");
            ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "ClearAdd();", true);
            GridFill(1);
            btnShowData.Visible = false;
            btnExcelFormat.Visible = false;
            FileUploadExcel.Visible = false;
            Btninsert.Visible = false;
        }
        catch (Exception ex)
        {
            Populate.WarningMsg(this, ex.Message);
        }
    }
    public void GridFill(int currentPage)
    {
        BlAccountHeadSubTransactions blAccountSubTrn = new BlAccountHeadSubTransactions();
        DataTable dtbl = new DataTable();
        blAccountSubTrn.AhCode = txtSearchAhName.Text.Trim();
        blAccountSubTrn.AhsCode = txtSearchAhsName.Text.Trim();
        currentPage = currentPage == 0 ? 1 : currentPage;
        blAccountSubTrn.PageIndex = Convert.ToInt16(currentPage);
        blAccountSubTrn.UserCode = hfUser.Value;
        blAccountSubTrn.FK_Client = Convert.ToInt64(hfClient.Value);
        dtbl = blAccountSubTrn.SelectallmultipleData();
        gvAccountHeadSubTrn.DataSource = dtbl;
        gvAccountHeadSubTrn.DataBind();

       // clearsearchfileds();
        blAccountSubTrn = null;
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
    private void ClearAccountHead()
    {
        txtAhCode.Text = "";
        txtAhName.Text = "";
    }
    protected void btnSearchAccountHead_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            ClearAccountHead();
            ((TextBox)CommonPopup1.FindControl("txtRefno")).Text = txtAhCode.Text.Trim();
            ((TextBox)CommonPopup1.FindControl("txtName")).Text = txtAhName.Text.Trim();
            ((HiddenField)CommonPopup1.FindControl("hfColumn")).Value = "Name";
            AccountHeadFill(1);
            divAdd.Style.Add("display", "block");
            divView.Style.Add("display", "none");
            txtAhCode.Focus();
        }
        catch (Exception ex)
        {
        }
    }
    public void AccountHeadFill(int pageIndex)
    {
        ClearAccountHead();
        Populate Populate = new Populate();
        CommonPopup1.CallingFunction = "AccountHeadFill";
        string strName = ((TextBox)CommonPopup1.FindControl("txtName")).Text;
        string Refno = ((TextBox)CommonPopup1.FindControl("txtRefno")).Text;
        string searchCol = ((HiddenField)CommonPopup1.FindControl("hfColumn")).Value;
        searchCol = searchCol == string.Empty ? "[Name]" : "[" + searchCol + "]";
        Refno = Refno == string.Empty || Refno == null ? "0" : Refno;
        Populate.FillPopUpGrid((GridView)CommonPopup1.FindControl("gdvSearch"), "AccountHead", "AhCode as ID,HeadName as Name,AhCode As [Ref No]", "ID_AccountHead", "ID_AccountHead", "FK_Client=" + hfClient.Value.Split(',')[0] + "  AND Cancelled=0 AND HeadName ", strName, null);
        Populate = null;
        
        if (txtAhName.Text == string.Empty)
        {
            lblPopupHeader.Text = "Account Name";
            mpCommon.Show();
        }
    }
    public void SetDetails(string strName, string strRefno)
    {
        DataTable dt1 = new DataTable();
        DataTable dt2 = new DataTable();
        BlAccountHeadSub blAccountSub = new BlAccountHeadSub();
        DataTable dt = new DataTable();
        if (CommonPopup1.CallingFunction == "AccountHeadFill")
        {
            txtCusAddress.Text = "";
            txtAhsNumber.Text = "";
            txtAhSubName.Text = "";
            txtAhCode.Text = strRefno;
            txtAhName.Text = strName;
            blAccountSub.Ahcode = strRefno;
            ViewState["Ahcode"] = blAccountSub.Ahcode;
            blAccountSub.FK_Client = Convert.ToInt64(hfClient.Value);
            blAccountSub.UserCode = hfUser.Value;
            dt = blAccountSub.selectAccountsubhead();
            liacountsubhead.Style.Add("display", (dt.Rows.Count > 0) ? "block" : "none");
            litxtCusAddress.Style.Add("display", (dt.Rows.Count > 0) ? "block" : "none");
            //dt1 = blAccountSub.selectfkAccounthead();
            //ViewState["ID_AccountHead"] = dt1.Rows[0]["Column1"].ToString();
            blAccountSub = null;
          GetBalance();
        }
        if (CommonPopup1.CallingFunction == "AccountHeadSubFill")
        {
            Populate Populate = new Populate();
            txtAhsNumber.Text = strRefno;
            txtAhSubName.Text = strName;
            txtAhstParty.Text = strName;
            blAccountSub.FK_Client = Convert.ToInt64(hfClient.Value);
            blAccountSub.UserCode = hfUser.Value;
            blAccountSub.AhsCode = strRefno;
            blAccountSub.Ahcode = ViewState["Ahcode"].ToString();
            dt = blAccountSub.selectSubHeadCustomerDetails();
            if (dt.Rows.Count > 0)
            {
                txtCusAddress.Text = "CustomerNo :"+dt.Rows[0]["CustomerCode"].ToString() +  "\n" +"MobileNo:"+ dt.Rows[0]["Mobile"].ToString() + "\n" + "Email:"+ dt.Rows[0]["Email"].ToString();
            }

            //dt2 = blAccountSub.selectfkAccountheadsub();
            //ViewState["ID_AccountHeadSub"] = dt2.Rows[0]["Column1"].ToString();
           GetBalance();
        }
    }
    protected void GetBalance()
    {
        BlAccountHeadSubTransactions BlAccountHeadSubTransaction = new BlAccountHeadSubTransactions();

        if (txtAhCode.Text != "")
        {
            BlAccountHeadSubTransaction.AhCode = txtAhCode.Text;
        }
        //if (txtAhsNumber.Text != "")
        //{
        BlAccountHeadSubTransaction.AhsCode = txtAhsNumber.Text;
        //}
        //else if (txtAhsNumber.Text == "")
        //{
        //    txtAhsNumber.Text = "0";
        //}

        ArrayList array = BlAccountHeadSubTransaction.GetBalance();


        if (array.Count > 0)
        {
            lblclrbal0.Style.Add("display", "blok");
            //lblunclrbal.Style.Add("display", "none");
            lblclbal.Style.Add("display", "block");
            //lblclrbal0.Text = "DR";
            //  //lblunclrbal.Text = "DR";
            //  lblclbal.Text = "DR";
            //  lblclbal.Visible = true;
            //lblunclrbal.Visible = true;
            lblclrbal0.Visible = true;
            txtClrBalance.Text = (0).ToString();
            //if (Convert.ToDouble(-500) < 0)
            txtClrBalance.Text = Math.Abs(Convert.ToDecimal(array[0])).ToString(); // +((Convert.ToDecimal(array[0]) > 0) ? "DR" : "CR");
            if (Convert.ToDouble(array[0]) < 0)
            {
                lblclrbal0.Text = "DR";
                lblclrbal0.Visible = true;
            }
            //if (Convert.ToDouble(-500) > 0)
            if ((Convert.ToDecimal(array[0]) >= 0))
            {
                lblclrbal0.Text = "CR";
                lblclrbal0.Visible = true;
            }
            //txtUnClrBalance.Text = (2800).ToString();
            //if (Convert.ToDouble(2800) < 0)
            // txtUnClrBalance.Text = Math.Abs(Convert.ToDecimal(array[1])).ToString();
            //if (Convert.ToDouble(array[1]) < 0)
            //{
            //    lblunclrbal.Text = "DR";
            //    lblunclrbal.Visible = true;
            //}
            //if (Convert.ToDouble(2800) > 0)
            // if ((Convert.ToDecimal(array[1]) > 0))
            //{
            //    lblunclrbal.Text = "CR";
            //    lblunclrbal.Visible = true;

            //}

            txtClsBalance.Text = (0).ToString();
            txtClsBalance.Text = Math.Abs(Convert.ToDecimal(array[1])).ToString();
            //txtClsBalance.Attributes["clsBal"] = Math.Abs(Convert.ToDecimal(array[1])).ToString();//This Attribute is used in js fn calcAmount();

            if (Convert.ToDouble(array[1]) < 0)

            //txtClsBalance.Attributes["clsBal"] = (500).ToString();
            //if (Convert.ToDouble(-500) < 0)
            {
                lblclbal.Text = "DR";
                lblclbal.Visible = true;
            }

            if ((Convert.ToDecimal(array[1]) >= 0))
            //if (Convert.ToDouble(-500) > 0)
            {
                lblclbal.Text = "CR";
                lblclbal.Visible = true;
            }
            ViewState["clsbalance"] = lblclbal.Text;
            // lblclbal.Attributes["clsBal"] = lblclbal.Text;//This Attribute is used in js fn calcAmount();

            //txtBrBalance.Text = Math.Abs(Convert.ToDecimal(array[3])).ToString();
            //txtClsBalance.Attributes["BrBal"] = Math.Abs(Convert.ToDecimal(array[3])).ToString();//This Attribute is used in js fn calcAmount();
            //if (Convert.ToDouble(array[3]) < 0)
            //{
            //    lblbrbal.Text = "DR";
            //    lblbrbal.Visible = true;
            //}
            //if ((Convert.ToDecimal(array[3]) > 0))
            //{
            //    lblbrbal.Text = "CR";
            //    lblbrbal.Visible = true;
            //}
            //lblclbal.Attributes["BrBal"] = lblbrbal.Text;//This Attribute is used in js fn calcAmount();
            //}

            BlAccountHeadSubTransaction = null;

        }
    }
    protected void btnSearchAccountHeadSub_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            ((TextBox)CommonPopup1.FindControl("txtRefno")).Text = txtAhsNumber.Text.Trim();
            ((TextBox)CommonPopup1.FindControl("txtName")).Text = txtAhSubName.Text.Trim();
            ((HiddenField)CommonPopup1.FindControl("hfColumn")).Value = "Name";
            AccountSUBHeadFill(1);
            divAdd.Style.Add("display", "block");
            divView.Style.Add("display", "none");
            txtAhCode.Focus();
        }
        catch (Exception ex)
        {
        }

    }
    private void AccountSUBHeadFill(int pageIndex)
    {
        Populate Populate = new Populate();
        CommonPopup1.CallingFunction = "AccountHeadSubFill";
        string strName = ((TextBox)CommonPopup1.FindControl("txtName")).Text;
        string Refno = ((TextBox)CommonPopup1.FindControl("txtRefno")).Text;
        string searchCol = ((HiddenField)CommonPopup1.FindControl("hfColumn")).Value;
        searchCol = searchCol == string.Empty ? "[Name]" : "[" + searchCol + "]";
        Refno = Refno == string.Empty || Refno == null ? "0" : Refno;
        Populate.FillPopUpGrid((GridView)CommonPopup1.FindControl("gdvSearch"), "AccountHeadSub AHS LEFT JOIN Customer C ON C.ID_Customer= AHS.Fk_Cuscode AND C.Active=1", "AHS.AhsCode as ID,AHS.HeadSubName as Name", "ID_AccountHeadSub", "ID_AccountHeadSub", "AHS.FK_Client=" + hfClient.Value.Split(',')[0] + "  AND AHS.Cancelled=0 AND  AHS.AhCode= " + txtAhCode.Text + " AND HeadSubName ", strName, null);
        Populate = null;
        //Populate.FillGrid((GridView)CommonPopup1.FindControl("gdvSearch"), "AccountHeadSub AHS LEFT JOIN Customer C ON C.ID_Customer= AHS.Fk_Cuscode AND C.Active=1", "AHS.Name AS Name ,AHS.AhsCode as ID,  AHS.AhsCode As [Ref No],C.CustomerCode AS CustomerCode, C.Address AS Address", "AHS.FK_Client=" + hfClient.Value.Split(',')[0] + " AND (AHS.AhsCode =" + Refno + " OR 0=" + Refno + ") AND AHS.Active=1 ", " AHS.AhCode= " + txtAhCode.Text + " AND AHS." + searchCol + "  LIKE '" + strName + "%'", pageIndex, 10, "", (Repeater)CommonPopup1.FindControl("rptPager"), this.Page);       
       // Populate.FillGrid((GridView)CommonPopup1.FindControl("gdvSearch"), "AccountHeadSub AHS LEFT JOIN Customer C ON C.ID_Customer= AHS.Fk_Cuscode AND C.Active=1", "AHS.Name AS Name ,AHS.ID_AccountHeadSub as ID,  AHS.ID_AccountHeadSub As [Ref No],C.CustomerCode AS CustomerCode, C.Address AS Address", "AHS.FK_Client=" + hfClient.Value.Split(',')[0] + " AND (AHS.ID_AccountHeadSub =" + Refno + " OR 0=" + Refno + ") AND AHS.Active=1 ", " AHS.FK_AccountHead= " + txtAhCode.Text + " AND AHS." + searchCol + "  LIKE '" + strName + "%'", pageIndex, 10, "", (Repeater)CommonPopup1.FindControl("rptPager"), this.Page);
        Populate = null;
        if (txtAhSubName.Text == string.Empty)
        {
            lblPopupHeader.Text = "Account Sub Head Fill";
            mpCommon.Show();
        }
    }
    protected void btnInsert_Click(object sender, EventArgs e)
    {
        

        if (Convert.ToDecimal(txtAhstAmount.Text) > 0)
        {
            btnShowData.Visible = false;
            btnExcelFormat.Visible = false;
            FileUploadExcel.Visible = false;
            UpdateToGrid();




            lblclrbal0.Style.Add("display", "none");
            lblclbal.Style.Add("display", "none");
            LIGRID.Style.Add("display", "blok");
     
            CalculateTotal();
        }
        else
        {
            ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "toastr.warning('Amount Must Be Greater Than Zero ')", true);
            ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "ShowAddDiv();", true);
        }
    }
    private bool UpdateToGrid()
    {

        DataTable dt;
        DataRow dr = null;
        if (ViewState["GridValue"] == null)
        {
            dt = new DataTable();
            InitializeTable(dt);
        }
        else
        {
            dt = (DataTable)ViewState["GridValue"];
        }
        dr = dt.NewRow();
        dr[COL_CODE] = txtAhCode.Text.ToString();
        dr[COL_NAME] = txtAhName.Text.ToString();
        if (txtAhsNumber.Text == "")
        {
            dr[COL_PARTY] = txtAhstParty.Text.ToString();
            dr[COL_SUBNO] = "";
            dr[COL_SUBID] = "";                     
        }
        else
        {
            dr[COL_PARTY] = txtAhstParty.Text.ToString();
            dr[COL_SUBNO] = txtAhsNumber.Text.ToString();
            dr[COL_SUBID] = txtAhSubName.Text.ToString();          
           // dr[COL_PARTY] = txtAhstParty.Text.ToString() + '[' + (txtAhsNumber.Text).ToString() + ']';
        }
        dr[COL_AMOUNT] = txtAhstAmount.Text.ToString();
        dr[COL_TYPE] = ddlTransType.SelectedValue.ToString();
        dr[COL_NARRATION] = txtAhstNarration.Text.ToString();       
       // dr[COL_SUBID] = ID_AccountHeadSub.ToString();
        dr[COL_CHECKDATE] = txtAhstChequeDate.Text.ToString();
        dr[COL_CHECKNO] = txtAhstChequeNo.Text.ToString() == "" ? "0" : txtAhstChequeNo.Text.ToString();
        dr[COL_BILLNO] = txtAhstBillNo.Text.ToString() == "" ? " " : txtAhstBillNo.Text.ToString(); ;
        dr[COL_BILLDATE] = txtAhstBillDate.Text.ToString() == "" ? "" : txtAhstBillDate.Text.ToString(); ;
       // dr[COL_TRANSDATE] = txtTransDate.Text.ToString() == "" ? " " : txtTransDate.Text.ToString(); ;
       // dr[COL_EFFECTDATE] = txtEffectDate.Text.ToString() == "" ? "" : txtEffectDate.Text.ToString(); ;
        //if ((ArrayList)Session[TaxSessionName] != null && ((txtAhstServicetax.Text == "" ? 0 : Convert.ToDecimal(txtAhstAmount1.Text)) > 0) && (ddlTransType.SelectedValue != "P"))
        //{
        //    dr[COL_TAXDETAILS] = Common.WriteToXml((ArrayList)Session[TaxSessionName]);
        //    //Session["savetax"] = null; 
        //}
       // dr[COL_TAXAMOUNT] = txtAhstServicetax.Text.ToString() == "" ? "0" : txtAhstServicetax.Text.ToString();
       // dr[COL_TRANSFER] = chkTransfer.Checked;
        for (int j = 0; j < dt.Rows.Count; j++)
        {

//            code
//name
//subno
//subname
//amount
//type
//narration 
//party
//cheque date
//cheque number
//billno
//billdate


           if (txtAhCode.Text == Convert.ToString(dt.Rows[j][0]) && txtAhName.Text == Convert.ToString(dt.Rows[j][1]) && txtAhsNumber.Text == Convert.ToString(dt.Rows[j][2]) && txtAhSubName.Text == Convert.ToString(dt.Rows[j][3]) && txtAhstAmount.Text == Convert.ToString(dt.Rows[j][4]) && ddlTransType.SelectedValue.ToString() == Convert.ToString(dt.Rows[j][5]) && txtAhstNarration.Text == Convert.ToString(dt.Rows[j][6]) && txtAhstParty.Text == Convert.ToString(dt.Rows[j][7])
           && txtAhstChequeDate.Text == Convert.ToString(dt.Rows[j][8]) && txtAhstChequeNo.Text == Convert.ToString(dt.Rows[j][9]) && txtAhstBillNo.Text == Convert.ToString(dt.Rows[j][10]) && txtAhstBillDate.Text == Convert.ToString(dt.Rows[j][11]))
            {
                ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "toastr.warning('Same Data Not Allowed Multiple Time ')", true);
                return false;
            }
        }



        dt.Rows.Add(dr);
        ViewState["GridValue"] = dt;

      



        gdvTrans.DataSource = dt;
        gdvTrans.DataBind();      
        clearfield();
        return true;
    }
    private void clearfield()
    {
         txtAhCode.Text="";
         txtAhName.Text="";
         txtCusAddress.Text="";
         txtAhsNumber.Text="";
         txtAhSubName.Text="";
         txtAhstParty.Text="";
         ddlTransType.SelectedValue="0";
         txtAhstAmount.Text="0";
         txtAmountInWords.Text="";
         txtAhstBillNo.Text="";
         txtAhstChequeNo.Text="";
         txtAhstNarration.Text="";
         txtClrBalance.Text="";
         txtClsBalance.Text="";
         //gdvTrans.DataSource = null;
         //gdvTrans.DataBind();
    }
    private void InitializeTable(DataTable dt)
    {
        dt.Columns.Add(new DataColumn(COL_CODE, typeof(string)));
        dt.Columns.Add(new DataColumn(COL_NAME, typeof(string)));
        dt.Columns.Add(new DataColumn(COL_SUBNO, typeof(string)));
        dt.Columns.Add(new DataColumn(COL_SUBID, typeof(string)));
        dt.Columns.Add(new DataColumn(COL_AMOUNT, typeof(string)));
        dt.Columns.Add(new DataColumn(COL_TYPE, typeof(string)));
        dt.Columns.Add(new DataColumn(COL_NARRATION, typeof(string)));
        dt.Columns.Add(new DataColumn(COL_PARTY, typeof(string)));     
        dt.Columns.Add(new DataColumn(COL_CHECKDATE, typeof(string)));
        dt.Columns.Add(new DataColumn(COL_CHECKNO, typeof(string)));
      //  dt.Columns.Add(new DataColumn(COL_TAXAMOUNT, typeof(string)));
       // dt.Columns.Add(new DataColumn(COL_TAXDETAILS, typeof(string)));
      //  dt.Columns.Add(new DataColumn(COL_TRANSFER, typeof(string)));
      //  dt.Columns.Add(new DataColumn(COL_BATCH, typeof(string)));
      //  dt.Columns.Add(new DataColumn(COL_TRANSDATE, typeof(string)));
       // dt.Columns.Add(new DataColumn(COL_EFFECTDATE, typeof(string)));
        dt.Columns.Add(new DataColumn(COL_BILLNO, typeof(string)));
        dt.Columns.Add(new DataColumn(COL_BILLDATE, typeof(string)));
       // dt.Columns.Add(new DataColumn(COL_GROUPID, typeof(string)));
    }
    protected void gdvTrans_RowCommand1(object sender, GridViewCommandEventArgs e)
    {
       // BlFunctions blfunction = new BlFunctions();
        if (e.CommandName == "rowdelete")
        {
            DataTable dt = (DataTable)ViewState["GridValue"];
            if (dt == null)
            {
                divAdd.Style.Add("display", "block");
                divView.Style.Add("display", "none");
                ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "toastr.warning('No Permission To Delete Grid Data ');", true);
            }
            else
            {
                if (dt.Columns.Count > 12)
                {
                    ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "toastr.warning('No Permission To Delete Excel Datas');", true);
                }
                else
                {
                    dt.Rows.RemoveAt(Convert.ToInt16(e.CommandArgument.ToString()));
                    ViewState["GridValue"] = dt;
                    gdvTrans.DataSource = null;
                    gdvTrans.DataSource = dt;
                    gdvTrans.DataBind();
                    CalculateTotal();
                }
            }
        }
    }
    protected void gdvTrans_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {

    }
    protected void gdvTrans_RowCreated(object sender, GridViewRowEventArgs e)
    {         
         //e.Row.Cells[ID_CODE].Visible = false;
         //e.Row.Cells[ID_NAME].Visible = false;
         //e.Row.Cells[ID_SUBCODE].Visible = false;
         //e.Row.Cells[ID_SUBNAME].Visible = false;
         //e.Row.Cells[ID_AMOUNT].Visible = false;
         // e.Row.Cells[ID_TYPE].Visible = false;
         //e.Row.Cells[ID_NARRATION].Visible = false;
         //e.Row.Cells[ID_PARTY].Visible = false;
         //e.Row.Cells[ID_CHECKDATE].Visible = false;
         //e.Row.Cells[ID_CHECKNO].Visible = false;
         // e.Row.Cells[TRANSDATE].Visible = false;
         //e.Row.Cells[EFFECTDATE].Visible = false;
         //e.Row.Cells[ID_BillNo].Visible = false;
         //e.Row.Cells[ID_BillDate].Visible = false;
    }
    protected void gdvTrans_RowDataBound(object sender, GridViewRowEventArgs e)
    {
       
    }
    protected void CalculateTotal()
    {
        decimal total = 0;
        decimal receiptTot = 0, paymentTot = 0;
        foreach (GridViewRow gdvRow in gdvTrans.Rows)
        {
        total = total + Convert.ToDecimal(gdvRow.Cells[ID_AMOUNT].Text.ToString() == "" ? "0" : gdvRow.Cells[ID_AMOUNT].Text.ToString());
        if (Convert.ToString(gdvRow.Cells[ID_TYPE].Text.ToString()) == "R")
         receiptTot = receiptTot + Convert.ToDecimal(gdvRow.Cells[ID_AMOUNT].Text.ToString());
          else if (Convert.ToString(gdvRow.Cells[ID_TYPE].Text.ToString()) == "P")
           paymentTot = paymentTot + Convert.ToDecimal(gdvRow.Cells[ID_AMOUNT].Text.ToString());
        }
        txtTotalAmount.Text = (Convert.ToDecimal(receiptTot) - Convert.ToDecimal(paymentTot)).ToString("0.00");
        txtCreditAmount.Text = Convert.ToDecimal(receiptTot).ToString("0.00");
        txtDebitAmount.Text = Convert.ToDecimal(paymentTot).ToString("0.00");
    }
    //protected void txtAhstAmount_TextChanged(object sender, EventArgs e)
    //{
    //    if (ddlTransType.SelectedItem.Value == "R")
    //    {
    //        txtClrBalance.Text = (Convert.ToDecimal(txtClrBalance.Text) - Convert.ToDecimal(txtAhstAmount.Text)).ToString();
    //    }
    //}
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        try
        {
            GridFill(1);
        }
        catch (Exception ex)
        {
            Populate.WarningMsg(this, ex.Message);
        }
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
    protected void gvAccountHeadSubTrn_SelectedIndexChanging(object sender, System.Web.UI.WebControls.GridViewSelectEventArgs e)
    {
        try
        {
            btnprint.Visible = true;
            btnSubmit.Text = "Delete";
            Btninsert.Visible = false;
            btnClear.Visible = false;       
            ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "ShowAddDiv();", true);
            ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "ClearAdd();", true);
            BlAccountHeadSubTransactions blAccountSubTrn = new BlAccountHeadSubTransactions();
            blAccountSubTrn.MasterId = Convert.ToInt64(gvAccountHeadSubTrn.DataKeys[e.NewSelectedIndex]["GroupId"].ToString());
            blAccountSubTrn.FK_Client = Convert.ToInt64(hfClient.Value);
            blAccountSubTrn.UserCode = hfUser.Value;
            ViewState["USERCODE"] = hfUser.Value; ;
            ViewState["masterid"] = blAccountSubTrn.MasterId;

            blAccountSubTrn.SelectMultipleData();
            Int64 remitcount = blAccountSubTrn.groupid;
            ViewState["GROUPIDS"] = blAccountSubTrn.groupid;
            DataTable dtgdvTrans = blAccountSubTrn.SelectMutipleTransaction();
            DataRow dr = null;
           dr = dtgdvTrans.NewRow();                
           gdvTrans.DataSource = dtgdvTrans;

           gdvTrans.DataBind();
    
           LIGRID.Style.Add("display", "blok");                
        }
        catch (Exception ex)
        {
            Populate.WarningMsg(this, ex.Message);
        }
    }
    protected void btnShowData_Click(object sender, EventArgs e)
    {
        
            FillGrid();
           // Btninsert.Visible = false;
            btnShowData.Enabled = false;           
    }

    private void FillGrid()
    {
      
       BlAccountHeadSubTransactions blAccountSubTrn = new BlAccountHeadSubTransactions();      
      // ViewState[ExcelData] = null;
       gdvTrans.DataSource = null;
       gdvTrans.DataBind();                      
       DataTable dt = new DataTable();
       dt = GetExcelData();
       ViewState["GridValue"] = dt;  
       if (dt.Rows.Count > 0)
          {
             string Xmldata = ValidateExcel(dt);
             blAccountSubTrn.XmlExcelImport = Xmldata;
             DataTable dtDetails = blAccountSubTrn.ValidateExcelData();
             ViewState[ExcelData] = dtDetails;             
                 if (dtDetails.Rows.Count > 0)
                   {
                      LIGRID.Style.Add("display", "blok");
                      
                      gdvTrans.DataSource = dtDetails;
                      gdvTrans.DataBind();

                      btnShow.Enabled = false;                        
                      ViewState[ExcelData] = null;
                      CalculateTotal();
                      dt = null;
                      dtDetails = null;
                      Session[Excel] = null;
                   }
                  else
                   {
                     BindNullRecDetails();
                     Btninsert.Visible = false;
                   }

                }
               else
               {

             
                   ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "toastr.warning('Please Upload Excel Document in Correct Format');", true);
               }
    

    //        else
    //        {

    //            dtAmendmentDetails = blRecoveryAmendment.SelectAmendment();
    //            if (dtAmendmentDetails.Rows.Count > 0)
    //            {
    //                gvDetails.DataSource = dtAmendmentDetails;
    //                gvDetails.DataBind();
    //                TotalSection.Visible = true;
    //                for (int i = 0; i < dtAmendmentDetails.Rows.Count; i++)
    //                {
    //                    Total = Total + Convert.ToDecimal(dtAmendmentDetails.Rows[i]["Amount"].ToString());

    //                }
    //                lblTotal.Text = Convert.ToString(Total);
    //                gvDetails.Rows[0].FindControl("txtgvAmount").Focus();
    //                Total = 0;
    //            }
    //            else
    //            {
    //                BindNullRecDetails();
    //            }
    //            dtAmendmentDetails = null;
    //            blRecoveryAmendment = null;
    //        }
    //    }
    //    else
    //    {
    //        ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "toastr.warning('Please Entered Correct Data');", true);
    //        return;
    //    }
    //    btnSearch.Enabled = false;
   }



    protected DataTable GetExcelData()
    {
        DataTable dtbl = new DataTable();
        if (Session[Excel] != null)
        {
            ExcelPath = Session[Excel].ToString();
            DataSet ds = DatasetFromExcel(ExcelPath);
            DataTable dt = new DataTable();
            dt = ds.Tables[0];
            
           // if (dt.Columns.Contains("Code") || dt.Columns.Contains("Name") || dt.Columns.Contains("SubNo")|| dt.Columns.Contains("SubName")|| dt.Columns.Contains("Amount")|| dt.Columns.Contains("Type")|| dt.Columns.Contains("Narration")|| dt.Columns.Contains("Party")|| dt.Columns.Contains("ChequeDate")|| dt.Columns.Contains("ChequeNo")|| dt.Columns.Contains("TransDate")|| dt.Columns.Contains("EffectDate")|| dt.Columns.Contains("BillNo")|| dt.Columns.Contains("BillDate"))
                if (dt.Columns.Contains("Code") || dt.Columns.Contains("Name") || dt.Columns.Contains("SubNo") || dt.Columns.Contains("SubName") || dt.Columns.Contains("Amount") || dt.Columns.Contains("Type"))
            {

                int columnCount = ds.Tables[0].Columns.Count;
                for (int i = 0; i < columnCount; i++)
                {
                    dtbl.Columns.Add(ds.Tables[0].Columns[i].ColumnName, typeof(System.String));
                }
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    DataRow drow = dtbl.NewRow();
                    for (int j = 0; j < columnCount; j++)
                    {
                        drow[j] = ds.Tables[0].Rows[i][j].ToString();
                    }
                   // if (Convert.ToString(drow.ItemArray[0]) != "" && Convert.ToString(drow.ItemArray[1]) != "" && Convert.ToString(drow.ItemArray[2]) != "" && Convert.ToString(drow.ItemArray[3]) != "" && Convert.ToString(drow.ItemArray[4]) != "" && Convert.ToString(drow.ItemArray[5]) != "" && Convert.ToString(drow.ItemArray[6]) != "" && Convert.ToString(drow.ItemArray[7]) != "" && Convert.ToString(drow.ItemArray[8]) != "" && Convert.ToString(drow.ItemArray[9]) != "" && Convert.ToString(drow.ItemArray[10]) != "" && Convert.ToString(drow.ItemArray[11]) != "" && Convert.ToString(drow.ItemArray[12]) != "" && Convert.ToString(drow.ItemArray[13]) != "")
                        if (Convert.ToString(drow.ItemArray[0]) != "" && Convert.ToString(drow.ItemArray[1]) != "" && Convert.ToString(drow.ItemArray[2]) != "" && Convert.ToString(drow.ItemArray[3]) != "" && Convert.ToString(drow.ItemArray[4]) != "" && Convert.ToString(drow.ItemArray[5]) != "")
                 
                    {
                        dtbl.Rows.InsertAt(drow, i);
                    }
                }
            }
        }
        return dtbl;

    }

     public void BindNullRecDetails()
    {
       
        DataTable dt = new DataTable();
        dt.Columns.Add("slno");
        dt.Columns.Add("Code");
        dt.Columns.Add("Name");
        dt.Columns.Add("SubNo");
        dt.Columns.Add("SubName");
        dt.Columns.Add("Amount");
        dt.Columns.Add("Type");
        dt.Columns.Add("Narration");
        dt.Columns.Add("Party");
        dt.Columns.Add("ChequeDate");
        dt.Columns.Add("ChequeNo");
        dt.Columns.Add("TransDate");
        dt.Columns.Add("EffectDate");
        dt.Columns.Add("BillNo");
        dt.Columns.Add("BillDate");
       

        DataRow drow = dt.NewRow();
        drow["slno"] = "";
        drow["Code"] = "";
        drow["Name"] = "";
        drow["SubNo"] = "";
        drow["SubName"] = "";
        drow["Amount"] = "";
        drow["Type"] = "";

          drow["Narration"] = "";
        drow["Party"] = "";
        drow["ChequeDate"] = "";
        drow["ChequeNo"] = "";
        drow["TransDate"] = "";
        drow["EffectDate"] = "";
        drow["BillNo"] = "";
           drow["BillDate"] = "";

        dt.Rows.InsertAt(drow, 1);
        gdvTrans.DataSource = dt;
        gdvTrans.DataBind();
        int columncount = gdvTrans.Rows[0].Cells.Count;
        gdvTrans.Rows[0].Cells.Clear();
        gdvTrans.Rows[0].Height = 0;
        
    }



     











    protected DataSet DatasetFromExcel(string filePath)
    {
       
        string ext = Path.GetExtension(filePath);
        dsExcel = new DataSet();

        FileStream stream = File.Open(filePath, FileMode.Open, FileAccess.ReadWrite);

        if (ext == ".xls")//Reading from Excel file ('97-2003 format; *.xls)
        {
            IExcelDataReader excelReader = ExcelReaderFactory.CreateBinaryReader(stream);
            excelReader.IsFirstRowAsColumnNames = true;// DataSet - Create column names from first row
            try
            {
                dsExcel = excelReader.AsDataSet();
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "toastr.error('abcd')", true);
               // ErrorMessage.WriteErrorLog(Page.ToString() + " : " + ex);
            }
            //  dsExcel.Tables[0].TableName = "Items";
            excelReader.Close();
        }
        else if (ext == ".xlsx")// Reading from Excel file (2007 format; *.xlsx)
        {
            IExcelDataReader excelReader = ExcelReaderFactory.CreateOpenXmlReader(stream);
            excelReader.IsFirstRowAsColumnNames = true;// DataSet - Create column names from first row
            dsExcel = excelReader.AsDataSet();
            //    dsExcel.Tables[0].TableName = "Items";
            excelReader.Close();


        }
        if (File.Exists(filePath))
        {
            string path = AppDomain.CurrentDomain.BaseDirectory + @"Excel";
            DirectoryInfo info = new DirectoryInfo(path);
            //DirectoryInfo info = new DirectoryInfo(Server.MapPath("Excel"));
            foreach (FileInfo files in info.GetFiles())
            {
                try
                {
                    files.Delete();
                }
                catch (Exception ex)
                {
                    ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "toastr.error('efgh')", true);
                   // ErrorMessage.WriteErrorLog(Page.ToString() + " : " + ex);
                }
            }
        }
        return dsExcel;
    }


   protected string ValidateExcel(DataTable dt)
   {
       ArrayList arrColumns = new ArrayList();
       dt.TableName = "AccountHeadSubTransaction";
       MemoryStream str = new MemoryStream();
       dt.WriteXml(str, true);
       str.Seek(0, SeekOrigin.Begin);
       StreamReader sr = new StreamReader(str);
       string xmlstr;
       xmlstr = sr.ReadToEnd();
       return (xmlstr);
   }


    protected void FileUploadExcel_UploadedComplete(object sender, AjaxControlToolkit.AsyncFileUploadEventArgs e)
    {
        try
        {
          
            if (FileUploadExcel.HasFile)
            {
              
               
                string filext = System.IO.Path.GetExtension(FileUploadExcel.FileName).ToLower();
                if (filext == ".xls" || filext == ".xlt" || filext == ".xlsm")
                {
                    string filePath = FileUploadExcel.PostedFile.FileName.ToString();
                   // string path = AppDomain.CurrentDomain.BaseDirectory + @"Excel";

                    string path = Server.MapPath("~") + "\\Excel/";
                  
                    if (!Directory.Exists(path))
                    {
                        Directory.CreateDirectory(path);
                    }



                    FileUploadExcel.SaveAs(path + "/" + FileUploadExcel.FileName);
                    Session[Excel] = path + "/" + FileUploadExcel.FileName;
                  //  WriteToLog(Session[Excel].ToString());
                }
                else
                {
                   // ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "alert('Invalid File Format')", true);
                   
                    return;
                }
            }
        }
        catch (Exception ex)
        {
          //  WriteToLog(ex.Message);
            //ErrorMessage.WriteErrorLog(Page.ToString() + " : " + ex);
        }
    }

    protected void btnPrint_Click(object sender, EventArgs e)
    {
        try
        {
            string ReturnId = ViewState["GROUPIDS"].ToString();
            string popupreport = "<script> window.open('CommonPrint.aspx?Id=" + ReturnId + "&TYPE=AHSTM" + "', 'self', 'left=200,top=50,width=900,height=680,status=0,resizable=0,fullscreen=0,scrollbars=1');</script>";
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "Success", popupreport, false);
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "toastr.warning('" + ex.Message + "')", true);
        }    
    }
//    public static void WriteToLog(String data)
//    {
//        string path = @"C:\BizcoreLog";
//        string FilePath = path + "\\" + DateTime.Now.ToString("dd-MM-yyyy") + ".txt";
//        //DateTime date = DateTime.ParseExact(TransDate, "c", System.Globalization.CultureInfo.InvariantCulture);
//        if (!Directory.Exists(path))
//        {
//            Directory.CreateDirectory(path);
//        }
//        if (!File.Exists(FilePath))
//        {
//            FileStream fs = File.Create(FilePath);
//            fs.Close();
//        }
//        using (StreamWriter file = new StreamWriter(FilePath, true))
//        {
//            file.WriteLine(DateTime.Now.ToString() + "::" + data);
//            file.Flush();
//            file.Close();
//        }
//    }
    
    protected void imgNew_Click(object sender, ImageClickEventArgs e)
    {
        btnSubmit.Text = "Submit";
        Btninsert.Visible = true;
        Liclosingbalance.Style.Add("display", "block");
        lilclarbalnce.Style.Add("display", "block");
        divAdd.Style.Add("display", "block");
        divView.Style.Add("display", "none");
        cleared();
       



        gdvTrans.DataSource = null;
        gdvTrans.DataBind();
    }
  
    protected void gvAccountHeadSubTrn_PageIndexChanging(object sender, System.Web.UI.WebControls.GridViewPageEventArgs e)
    {
        gdvTrans.PageIndex = e.NewPageIndex;
        GridFill(e.NewPageIndex);
    }
}