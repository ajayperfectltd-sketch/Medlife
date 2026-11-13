/*----------------------------------------------------------------------------------------------------------------------------------------------------------
Purpose    : Insert//Delete/SELECT    Cash Transaction
Author     : Bibin TC
Copyright  : Perfect Software Solutions (Clt) Pvt Ltd
Created on : 03/05/2018 
 * --------------------------------------------------------------------------------------------------------------------------------------------------------------------------------*/


using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebPharma.Business.Masters;
using WebPharma.Business.Accounts;

using System.Data;
using System.Collections;

public partial class AccountHeadSubTransaction : System.Web.UI.Page
{
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
    public void DeleteMethod(string Reason = "0")
    {
        string confirmValue = Request.Form["confirm_value"];
        if (confirmValue.Length > 3)
            confirmValue = confirmValue.Substring(confirmValue.Length - 3, 3);
        if (confirmValue == "Yes")
        {
            long status = 0;
            //BlAccountHeadSub BlAccountHeadSub = new BlAccountHeadSub();
            //BlAccountHeadSub.UserCode = hfUser.Value;
            //BlAccountHeadSub.MasterId = Convert.ToInt64(hfMasterID.Value);
            //BlAccountHeadSub.FK_Reason = Convert.ToInt64(Reason);
            BlAccountHeadSubTransactions blAccountSubTrn = new BlAccountHeadSubTransactions();
            blAccountSubTrn.UserCode = hfUser.Value;
            blAccountSubTrn.MasterId = Convert.ToInt64(hfMasterID.Value);
            blAccountSubTrn.Fk_Reason = Convert.ToInt64(Reason);

               
           
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
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                txtAhCode.Attributes.Add("readonly", "readonly");
                txtAhName.Attributes.Add("readonly", "readonly");
                txtAhsNumber.Attributes.Add("readonly", "readonly");
                txtAhSubName.Attributes.Add("readonly", "readonly");
                
                txtClrBalance.Attributes.Add("readonly", "readonly");
                txtClsBalance.Attributes.Add("readonly", "readonly");
                Page.Header.DataBind();
                ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "ClearAdd();", true);
                divAdd.Style.Add("display", "block");
                hfUser.Value = Request.QueryString["uid"];
                hfClient.Value = Request.QueryString["clnt"];
              //  HdValuestore.Value = Session["mainstore"].ToString() == null ? "0" : Session["mainstore"].ToString();
                HdValuestore.Value = Session["mainstore"].ToString();
             
                  CheckUserAccess();           
            }
        }
        catch (Exception ex)
        {
            Populate.WarningMsg(this, ex.Message);
        }

    }
    protected void CheckUserAccess()
    {
        BlUserAccess bluseraccess = new BlUserAccess();
        Populate populate = new Populate();
        bluseraccess.FK_Users = Convert.ToInt64(hfUser.Value);
        bluseraccess.FK_Pages = 40;
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
    private void ClearAccountHead()
    {
        txtAhCode.Text = "";
        txtAhName.Text = "";    
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
        Populate.FillPopUpGrid((GridView)CommonPopup1.FindControl("gdvSearch"), "AccountHead", "AhCode as ID,HeadName as Name", "ID_AccountHead", "ID_AccountHead", "FK_Client=" + hfClient.Value.Split(',')[0] + "  AND Cancelled=0 AND HeadName ", strName, null);
        Populate = null;
        //Populate.FillGrid((GridView)CommonPopup1.FindControl("gdvSearch"), "AccountHead", "Name ,AhCode as ID,  AhCode As [Ref No],ShortName,ID_AccountHead as Code ", "FK_Client=" + hfClient.Value.Split(',')[0] + " AND (ID_AccountHead =" + Refno + " OR 0=" + Refno + ") AND Active=1 ", searchCol + "  LIKE '" + strName + "%'  and Cancelled=0 and Active=1  ", pageIndex, 10, "", (Repeater)CommonPopup1.FindControl("rptPager"), this.Page,1);          
    
        //Populate = null;      
        if (txtAhName.Text == string.Empty)
        {
            lblPopupHeader.Text = "Account Name";
            mpCommon.Show();
        }
    }
    public void SetDetails(string strName, string strRefno)
    {
        DataTable dt1 = new DataTable();
        BlAccountHeadSub blAccountSub = new BlAccountHeadSub();     
        DataTable dt = new DataTable();
        DataTable dt2 = new DataTable();
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
            dt1 = blAccountSub.selectfkAccounthead();
            ViewState["ID_AccountHead"] = dt1.Rows[0]["Column1"].ToString();
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
           // blAccountSub.Ahcode= ViewState["AhcodeS"].ToString();
            ViewState["AhcodeS"] = blAccountSub.AhsCode;
            dt = blAccountSub.selectSubHeadCustomerDetails();
            if (dt.Rows.Count > 0)
            {
                txtCusAddress.Text = "CustomerNo :" + dt.Rows[0]["CustomerCode"].ToString() + "\n" + "MobileNo:" + dt.Rows[0]["Mobile"].ToString() + "\n" + "Email:" + dt.Rows[0]["Email"].ToString();         
            }
         
            dt2 = blAccountSub.selectfkAccountheadsub();
            ViewState["ID_AccountHeadSub"] = dt2.Rows[0]["Column1"].ToString();
            blAccountSub = null;
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
            BlAccountHeadSubTransaction.FK_Client = Convert.ToInt64(hfClient.Value);
            BlAccountHeadSubTransaction.UserCode = hfUser.Value;
            BlAccountHeadSubTransaction.FK_Store = Convert.ToInt64(HdValuestore.Value);
            decimal bal = BlAccountHeadSubTransaction.GetAccountBalance();
            txtClrBalance.Text = Math.Abs(bal).ToString();
            lblclrbal0.Style.Add("display", "blok");
            lblclbal.Style.Add("display", "block");
            lblclrbal0.Text = "DR";
            lblclbal.Text = "DR";
            lblclbal.Visible = true;
            lblclrbal0.Visible = true;
         
            if (bal < 0)
          
            {
                lblclrbal0.Text = "DR";
                lblclrbal0.Visible = true;
            }
            else if (bal >= 0)
       
            {
                lblclrbal0.Text = "CR";
                lblclrbal0.Visible = true;
            }
          
            txtClsBalance.Text = Math.Abs(bal).ToString();
            txtClsBalance.Attributes["clsBal"] = Math.Abs(bal).ToString();
            if (bal < 0)
            {
                lblclbal.Text = "DR";
                lblclbal.Visible = true;
            }

    
            if (bal >= 0)
            {
                lblclbal.Text = "CR";
                lblclbal.Visible = true;
            }
            ViewState["clsbalance"] = lblclbal.Text;

            //ArrayList array = BlAccountHeadSubTransaction.GetBalance();


        //if (array.Count > 0)
        //{
        //lblclrbal0.Style.Add("display", "blok");
        ////lblunclrbal.Style.Add("display", "none");
        //lblclbal.Style.Add("display", "block");
        //  //lblclrbal0.Text = "DR";
        //  //  //lblunclrbal.Text = "DR";
        //  //  lblclbal.Text = "DR";
        //  //  lblclbal.Visible = true;
        //    //lblunclrbal.Visible = true;
        //    lblclrbal0.Visible = true;
        //    txtClrBalance.Text = (0).ToString();
            //if (Convert.ToDouble(-500) < 0)
                //txtClrBalance.Text = Math.Abs(Convert.ToDecimal(array[0])).ToString(); // +((Convert.ToDecimal(array[0]) > 0) ? "DR" : "CR");
            //if (Convert.ToDouble(array[0]) < 0)
                
            //{
            //    lblclrbal0.Text = "DR";
            //    lblclrbal0.Visible = true;
            //}
            //    //if (Convert.ToDouble(-500) > 0)
            //  if ((Convert.ToDecimal(array[0]) >= 0))
            //{
            //    lblclrbal0.Text = "CR";
            //    lblclrbal0.Visible = true;
            //}
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

            //  txtClsBalance.Text = (0).ToString();
            //txtClsBalance.Text = Math.Abs(Convert.ToDecimal(array[1])).ToString();
                 //txtClsBalance.Attributes["clsBal"] = Math.Abs(Convert.ToDecimal(array[1])).ToString();//This Attribute is used in js fn calcAmount();
               
            //    if (Convert.ToDouble(array[1]) < 0)
               
            //    //txtClsBalance.Attributes["clsBal"] = (500).ToString();
            //    //if (Convert.ToDouble(-500) < 0)
            //{
            //    lblclbal.Text = "DR";
            //    lblclbal.Visible = true;
            //}

            //if ((Convert.ToDecimal(array[1]) >= 0))
            //    //if (Convert.ToDouble(-500) > 0)
            //{
            //    lblclbal.Text = "CR";
            //    lblclbal.Visible = true;
            //}
            //    ViewState["clsbalance"] = lblclbal.Text;
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
          
        //}
    }
    protected void ddlTransType_SelectedIndexChanged(object sender, EventArgs e)
    {

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
        Populate.FillPopUpGrid((GridView)CommonPopup1.FindControl("gdvSearch"), "AccountHeadSub AHS LEFT JOIN Customer C ON C.ID_Customer= AHS.Fk_Cuscode AND C.Active=1", "AHS.AhsCode as ID,AHS.HeadSubName as Name", "ID_AccountHeadSub", "ID_AccountHeadSub", "AHS.FK_Client=" + hfClient.Value.Split(',')[0] + "  AND AHS.Cancelled=0 AND AHS.AhCode= " + txtAhCode.Text + " AND HeadSubName ", strName, null);
        Populate = null;
        //Populate.FillGrid((GridView)CommonPopup1.FindControl("gdvSearch"), "AccountHeadSub AHS LEFT JOIN Customer C ON C.ID_Customer= AHS.Fk_Cuscode AND C.Active=1", "AHS.HeadSubName AS Name ,AHS.AhsCode as ID,  AHS.AhsCode As [Ref No],C.CustomerCode AS CustomerCode", "AHS.FK_Client=" + hfClient.Value.Split(',')[0] + " AND (AHS.AhsCode =" + Refno + " OR 0=" + Refno + ") AND AHS.Active=1 ", " AHS.AhCode= " + txtAhCode.Text + " AND AHS." + searchCol + "  LIKE '" + strName + "%'", pageIndex, 10, "", (Repeater)CommonPopup1.FindControl("rptPager"), this.Page);       
        //Populate = null;
        if (txtAhSubName.Text == string.Empty)
        {
            lblPopupHeader.Text = "Account Sub Head Fill";
            mpCommon.Show();
        }      
    }
    protected void btnBillWseProcess_Click(object sender, ImageClickEventArgs e)
    {

    }
    protected void imgbtnView_Click(object sender, ImageClickEventArgs e)
    {
        try
        {          
            divAdd.Style.Add("display", "none");
            divView.Style.Add("display", "block");
           ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "ClearAdd();", true);
           ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "ClearSearch();", true);
              //  txtSearchAhName.Text = Convert.ToString(ViewState["Ahcode"]);
            
            //if(txtAhsNumber.Text!="")
            //{
               // txtSearchAhsName.Text = Convert.ToString(ViewState["AhcodeS"]);
           // }

           //  $("#<%#txtAhsNumber.ClientID%>").val('');

            GridFill(1);
        }
        catch (Exception ex)
        {          
            Populate.WarningMsg(this, ex.Message);
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
          Populate.WarningMsg(this, ex.Message);
        }
    }
    protected void btnTax_Click(object sender, ImageClickEventArgs e)
    {

    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            if (Btnprocess.Text == "OK")
            {
                SaveData();
                
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "toastr.warning('Click Process Button....')", true);
               // ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "toastr.success('Data inserted successfully');", true);
                ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "ShowAddDiv();", true);
            }
        }
        catch (Exception ex)
        {
            Populate.WarningMsg(this, ex.Message);
        }
    }
    private void SaveData()
    {

        DateTime dtTrans, dtEffect;
        decimal amount;
        if (!DateTime.TryParse(txtTransDate.Text, out dtTrans) ||
            !DateTime.TryParse(txtEffectDate.Text, out dtEffect) ||
            !decimal.TryParse(txtAhstAmount.Text, out amount) || amount <= 0)
        {
            ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "toastr.warning('Please check Date and Amount fields')", true);
            ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "ShowAddDiv();", true);
            return;
        }
        long status = 0;
        BlAccountHeadSubTransactions blAccountSubTrn = new BlAccountHeadSubTransactions();
        //blAccountSubTrn.FK_AccountHead = Int64.Parse(txtAhCode.Text == "" ? "0" : txtAhCode.Text);
        blAccountSubTrn.FK_AccountHead = Convert.ToInt64(ViewState["ID_AccountHead"]);
        blAccountSubTrn.AhCode = (txtAhCode.Text == "" ? "0" : txtAhCode.Text);
        //blAccountSubTrn.FK_AccountHeadSub = Int64.Parse(txtAhsNumber.Text == "" ? "0" : txtAhsNumber.Text);
        blAccountSubTrn.FK_AccountHeadSub =Convert.ToInt64( ViewState["ID_AccountHeadSub"]);
        blAccountSubTrn.AhsCode =(txtAhsNumber.Text == "" ? "0" : txtAhsNumber.Text);
        blAccountSubTrn.Cancelled = false;       
        blAccountSubTrn.Party = Convert.ToString(txtAhstParty.Text);
        blAccountSubTrn.TransDate = Convert.ToDateTime(txtTransDate.Text);
        blAccountSubTrn.EffectDate = Convert.ToDateTime(txtEffectDate.Text);
        blAccountSubTrn.TransType = ddlTransType.SelectedValue.ToString();
        blAccountSubTrn.Amount = Convert.ToDecimal(txtAhstAmount.Text);
        if (txtAhstChequeNo.Text.Length > 0)
        {
            blAccountSubTrn.ChequeNo = Convert.ToString(txtAhstChequeNo.Text);
            blAccountSubTrn.ChequeDate = Convert.ToDateTime(txtAhstChequeDate.Text);
        }
        blAccountSubTrn.Narration = Convert.ToString(txtAhstNarration.Text);
        blAccountSubTrn.UserCode = hfUser.Value;
        blAccountSubTrn.FK_Client = Convert.ToInt64(hfClient.Value);
        blAccountSubTrn.FK_Store = Convert.ToInt64(HdValuestore.Value);
        if (hdfAcnthedsubtrn.Value == string.Empty)
        {
            if (hfIsAdd.Value.ToLower() == "true")
            {
                status = blAccountSubTrn.InsertData();
                if (status > 0)
                {                 
                    ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "ShowAddDiv();", true);
                    Populate.InsertMsg(this);                  
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "ShowAddDiv();", true);                 
                    ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "toastr.error('Cannot insert data')", true);                 
                }
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "toastr.warning('User does not have permission to add')", true);               
                ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "ShowAddDiv();", true);
            }
        }
        else
        {
            blAccountSubTrn.MasterId = Convert.ToInt64(hdfAcnthedsubtrn.Value);
            if (hfIsUpdate.Value.ToLower() == "true")
            {
                status = blAccountSubTrn.UpdateData();
                if (status > 0)
                {                 
                    ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "toastr.success('Data Updated Successfully');ClearAdd();", true);
                    ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "ShowAddDiv();", true);
                }

                else
                {
                    ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "ShowAddDiv();", true);                  
                    ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "ShowAddDiv();", true);
                }
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "toastr.warning('User does not have permission to Update')", true);            
                ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "ShowAddDiv();", true);
            }

        }
        blAccountSubTrn = null;
        Btnprocess.Text = "Process";
        lockcntrl(true);
    }
    protected void btnProcess_Click(object sender, EventArgs e)
    {
        if (Convert.ToDecimal(txtAhstAmount.Text) > 0 && ddlTransType.SelectedValue!="0")
        {
            decimal Amount = 0;
            decimal OriginalAmount = 0;
            if (Btnprocess.Text == "Process")
            {
                if (lblclbal.Text == "DR")
                {
                    OriginalAmount = -1 * Convert.ToDecimal(txtClsBalance.Text);
                }
                if (lblclbal.Text == "CR")
                {
                    OriginalAmount = 1 * Convert.ToDecimal(txtClsBalance.Text);
                }
                if (ddlTransType.SelectedValue == "R")
                {
                    Amount = OriginalAmount + Convert.ToDecimal(txtAhstAmount.Text == "" ? "0" : txtAhstAmount.Text);                   
                    if (Amount > 0)
                    {
                        lblclbal.Text = "CR";
                    }
                    else
                    {
                        lblclbal.Text = "DR";
                    }
                    txtClsBalance.Text = Math.Abs(Convert.ToDecimal(Amount)).ToString();
                }
                if (ddlTransType.SelectedValue == "P")
                {
                    Amount = OriginalAmount - Convert.ToDecimal(txtAhstAmount.Text == "" ? "0" : txtAhstAmount.Text);
                    if (Amount > 0)
                    {
                        lblclbal.Text = "CR";
                    }
                    else
                    {
                        lblclbal.Text = "DR";
                    }                               
                    txtClsBalance.Text = Math.Abs(Convert.ToDecimal(Amount)).ToString();
                }           
                lockcntrl(false);
                Btnprocess.Text = "OK";
            }
            else
            {
                lockcntrl(true);
                Btnprocess.Text = "Process";
                txtClsBalance.Text = txtClrBalance.Text;
                lblclbal.Text = ViewState["clsbalance"].ToString();
            }
        }
        else
        {
            if (ddlTransType.SelectedValue == "0")
            {
                ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "toastr.warning('Select TransType')", true); 
            }
            else if (Convert.ToDecimal(txtAhstAmount.Text) <= 0)
            {
                ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "toastr.warning('Amount Must Be Greater Than Zero')", true);     
            }
   
            ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "ShowAddDiv();", true);
        }
    }
    private void lockcntrl(bool Show)
    {
        txtAhCode.Enabled = Show;
        txtAhName.Enabled = Show;
        txtAhstParty.Enabled = Show;
        txtEffectDate.Enabled = Show;
        txtTransDate.Enabled = Show;
        txtAhstAmount.Enabled = Show;
        ddlTransType.Enabled = Show;
        txtAmountInWords.Enabled = Show;
        txtAhstChequeNo.Enabled = Show;
        txtAhstChequeDate.Enabled = Show;
        txtAhstNarration.Enabled = Show;
        txtClrBalance.Enabled = Show;
        txtClsBalance.Enabled = Show;
        txtAhsNumber.Enabled = Show;
        txtAhSubName.Enabled = Show;     
        txtCusAddress.Enabled = Show;
        btnSearchAccountHead.Enabled = Show;
        btnSearchSubHead.Enabled = Show;                 
    }
    protected void gvAccountHeadSubTrn_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            
            string confirmValue = Request.Form["confirm_value"];
            if (confirmValue.Length > 3)
                confirmValue = confirmValue.Substring(confirmValue.Length - 3, 3);
            if (confirmValue == "Yes")
            {
                long status = 0;
                BlAccountHeadSubTransactions blAccountSubTrn = new BlAccountHeadSubTransactions();
                blAccountSubTrn.MasterId = Convert.ToInt64(gvAccountHeadSubTrn.DataKeys[e.RowIndex]["ID_AccountHeadSubTransaction"].ToString());
                hfMasterID.Value = (gvAccountHeadSubTrn.DataKeys[e.RowIndex]["ID_AccountHeadSubTransaction"].ToString());
                blAccountSubTrn.UserCode = hfUser.Value;
                blAccountSubTrn.FK_Client = Convert.ToInt64(hfClient.Value);

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
                
            }
            else
            {
               // mpDelete.Hide();
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "toastr.warning('" + ex.Message + "')", true);
        }
    }
    protected void gvAccountHeadSubTrn_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        try
        {
            btnprint.Visible = true;
            btnSubmit.Visible = false;
            Btnprocess.Visible = false;
            btnClear.Visible = false;       
            ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "ShowAddDiv();", true);
            BlAccountHeadSubTransactions blAccountSubTrn = new BlAccountHeadSubTransactions();
            blAccountSubTrn.MasterId = Convert.ToInt64(gvAccountHeadSubTrn.DataKeys[e.NewSelectedIndex]["ID_AccountHeadSubTransaction"].ToString());
            ViewState["MasterId"] = blAccountSubTrn.MasterId;
            blAccountSubTrn.FK_Client = Convert.ToInt64(hfClient.Value);
            blAccountSubTrn.UserCode = hfUser.Value;
            blAccountSubTrn.SelectData();
            if (blAccountSubTrn.AhsCode != "")
            {
                liacountsubhead.Style.Add("display", "block");
                litxtCusAddress.Style.Add("display", "block");
            }
            else
            {
                liacountsubhead.Style.Add("display", "none");
                litxtCusAddress.Style.Add("display", "none");
            }
           // txtAhCode.Text = blAccountSubTrn.FK_AccountHead.ToString() ;
           // txtAhName.Text = blAccountSubTrn.AhCode.ToString();

            txtAhCode.Text = blAccountSubTrn.AhCode.ToString();
            txtAhName.Text = blAccountSubTrn.headName.ToString();
            txtAhsNumber.Text = blAccountSubTrn.FK_AccountHeadSub.ToString();
            txtAhSubName.Text = blAccountSubTrn.Subheadname.ToString();
            object TransDates = blAccountSubTrn.TransDate;
            txtTransDate.Text = (TransDates == null) ? "" : Convert.ToDateTime(TransDates).ToShortDateString();
            object EffectDateS = blAccountSubTrn.EffectDate;
            txtEffectDate.Text = (EffectDateS == null) ? "" : Convert.ToDateTime(EffectDateS).ToShortDateString();
            object ChequeDates = blAccountSubTrn.ChequeDate;
            txtAhstChequeDate.Text = (ChequeDates == null) ? "" : Convert.ToDateTime(ChequeDates).ToShortDateString();
            txtAhstParty.Text = blAccountSubTrn.Party;
            ddlTransType.SelectedValue = blAccountSubTrn.TransType;
            txtAhstChequeNo.Text = blAccountSubTrn.ChequeNo;
            txtAhstAmount.Text = blAccountSubTrn.Amount.ToString();
            if (txtAhstAmount.Text.Length > 0)
            {
                txtAmountInWords.Text = Common.ToWords(Convert.ToDecimal(txtAhstAmount.Text));
            }
            txtAhstNarration.Text = blAccountSubTrn.Narration;
            lockcntrl(false);
            Liclosingbalance.Style.Add("display", "none");
            lilclarbalnce.Style.Add("display", "none");
            lblclrbal0.Style.Add("display", "none");
   
            lblclbal.Style.Add("display", "none");
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
        dtbl = blAccountSubTrn.SelectAllData();
        gvAccountHeadSubTrn.DataSource = dtbl;
        //gvTransaction.DataSource = dtbl;
        //if (dtbl.Rows.Count > 0)
        //{
        //    int recordCount = Convert.ToInt32(dtbl.Rows[0]["RecordCount"].ToString());
        //    this.PopulatePager(recordCount, currentPage);
        //}        gvAccountHeadSubTrn.DataBind();
        gvAccountHeadSubTrn.DataBind();
        //gvTransaction.DataBind();
       // clearsearchfileds();
        blAccountSubTrn = null;
    }
    private void clearsearchfileds()
    {
        txtSearchAhName.Text = "";
        txtSearchAhsName.Text = "";     
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
    private void cleared()
    {
        liacountsubhead.Style.Add("display", "none");
        litxtCusAddress.Style.Add("display", "none");
        lblclrbal0.Style.Add("display", "none");    
        lblclbal.Style.Add("display", "none");
        divAdd.Style.Add("display", "block");
        btnClear.Visible = true;
        Btnprocess.Visible = true;
        btnSubmit.Visible = true;
        btnprint.Visible = false;
        Btnprocess.Text = "Process";
    }
    protected void btnClear_Click(object sender, EventArgs e)
    {  
        lockcntrl(true);
        cleared();
        //gvTransaction.DataSource = null;
        //gvTransaction.DataBind();
    }
    protected void imgbtnAdd_Click(object sender, ImageClickEventArgs e)
    {
        //if (!Page.IsPostBack)
       // {
            Liclosingbalance.Style.Add("display", "block");
            lilclarbalnce.Style.Add("display", "block");
            divAdd.Style.Add("display", "block");
            divView.Style.Add("display", "none");
            lockcntrl(true);
            cleared();
        //}
    }
    protected void btnPrint_Click(object sender, EventArgs e)
    {
        try
        {
            string ReturnId = ViewState["MasterId"].ToString();
            string popupreport = "<script> window.open('CommonPrint.aspx?Id=" + ReturnId + "&TYPE=AHST" + "', 'self', 'left=200,top=50,width=900,height=680,status=0,resizable=0,fullscreen=0,scrollbars=1');</script>";
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "Success", popupreport, false);
            divAdd.Style.Add("display", "block");
            divView.Style.Add("display", "none");
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "toastr.success('Data Inserted Successfully');ClearAdd();", true);
           // ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "toastr.warning('" + ex.Message + "')", true);
        }    
    }
    protected void ImageButton2_Click(object sender, ImageClickEventArgs e)
    {
       // Delete();
    }
    protected void gvAccountHeadSubTrn_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvAccountHeadSubTrn.PageIndex = e.NewPageIndex;
        GridFill(e.NewPageIndex);
    }

    protected void Btntrns_Click(object sender, EventArgs e)
    {
        GridFill(1);

    }
}

