/*----------------------------------------------------------------------------------------------------------------------------------------------------------
Purpose    : CounterClose
Author     : Aiswarya
Copyright  : Perfect Software Solutions (Clt) Pvt Ltd
Created on : 10/11/2021
----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
MODIFICATIONS 
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
using Inventory.Business;
using WebPharma.Interface.General;
using System.Collections;
using System.Globalization;
using System.Text;
using System.Web.UI.HtmlControls;
using System.Web.Security;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using Inventory.Business;
using System.IO;
using System.Text;
using WebPharma.Business.WebPharma;
using WebPharma.Business;
public partial class CounterClose : System.Web.UI.Page
{
    #region Functions



    protected void CheckUserAccess()
    {
        BlUserAccess bluseraccess = new BlUserAccess();
        Populate populate = new Populate();

        bluseraccess.FK_Users = Convert.ToInt64(hfUser.Value);
        bluseraccess.FK_Pages = 98;
        bluseraccess.FK_Client = Convert.ToInt64(hfClient.Value);
        bluseraccess.CheckUserAccess();
        if (Convert.ToBoolean(bluseraccess.IsView) == false)
        {
            // Response.Redirect("Home.aspx?");

        }
        if (bluseraccess.FK_Pages != 0)
        {
            BlFunction blfunctions = new BlFunction();
            //lblPageName.Text = Convert.ToString(blfunctions.GetScalarValue("Pages", "ISNULL( PageName,'') PageName", "ID_Pages=" + bluseraccess.FK_Pages.ToString() + " AND  Active=1 ", ""));
        }
        hfIsAdd.Value = (Convert.ToBoolean(bluseraccess.IsAdd)).ToString();
        hfIsUpdate.Value = (Convert.ToBoolean(bluseraccess.IsUpdate)).ToString();
        hfIsDelete.Value = (Convert.ToBoolean(bluseraccess.IsDelete)).ToString();
        bluseraccess = null;
    }
    protected void SaveCounterClose()
    {

        long status = 0;
        BlSales blsales = new BlSales();

        blsales.FK_Client = hfClient.Value == "" ? 0 : Convert.ToInt64(hfClient.Value);
        blsales.FK_Counter = hfCounter.Value == "" ? 0 : Convert.ToInt64(hfCounter.Value);
        blsales.FK_Store = hfStore.Value == "" ? 0 : Convert.ToInt64(hfStore.Value);
        blsales.Fk_Shift = Convert.ToInt64(0);
        blsales.BillDate = Convert.ToDateTime(DateTime.Now);
        blsales.OpeningCounterClose = Convert.ToDecimal(LblCloseCounterOpening.Text);
        blsales.SalesAmountCounterClose = Convert.ToDecimal(LblCloseCounterSales.Text);
        blsales.PayInAmountCounterClose = 0;
        blsales.PayOutAmountCounterClose = 0;
        blsales.ClosingAmountCounterClose = Convert.ToDecimal(LblCloseCounterClosingBalance.Text);
        blsales.CloseCounterRemittance = Convert.ToDecimal(txtCounterCloseRemittance.Text);
        blsales.EnteredOn = Convert.ToDateTime(DateTime.Now);
        blsales.UserCode = hfUser.Value;
        blsales.SalesRetAmountCounterClose = Convert.ToDecimal(lblCloseCounterSalesReturn.Text);

        ArrayList arlist = new ArrayList();
        DataTable dtbl = new DataTable();
        ICloseCounterDetails icounterclosedetails;
        foreach (GridViewRow gr in gvDenomination.Rows)
        {
            icounterclosedetails = new ICloseCounterDetails();
            icounterclosedetails.FK_Denominationmaster = Convert.ToInt64(gvDenomination.DataKeys[gr.RowIndex]["ID_DenominationMaster"].ToString());//Convert.ToInt64(gvDenomination.Rows[i].Cells[0].Text);
            icounterclosedetails.DenominationCount = Convert.ToInt64(((TextBox)gr.FindControl("txtPieces")).Text); //Convert.ToInt64(gvDenomination.Rows[i].Cells[4].Text);//Pieces
            arlist.Add(icounterclosedetails);
            icounterclosedetails = null;
        }

        blsales.XmlCloseCounterDetails = Common.WriteToXml(arlist);
        status = blsales.CounterCloseUpdatePOS();
        if (status > 0)
        {
            long CounterId;
          //  blsales.FK_Counter = hfCounter.Value == "" ? 0 : Convert.ToInt64(hfCounter.Value);
            blsales.OpeningCounterClose = 0;
            blsales.CashAmount = 0;
            blsales.UserCode = hfUser.Value;
            blsales.FK_Client = Convert.ToInt64(hfClient.Value);
            blsales.Status = "Close";
           // CounterId = blsales.CounterOpenClose();
            Clear();
            ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "toastr.success('" + "Counter Closed Successfully" + "')", true);

        }
        else
        {
            ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "toastr.warning('" + "Insertion failed" + "')", true);
        }

        blsales = null;
        dtbl = null;

    }

    public void Counter()
    {
        try
        {
            long CounterId;
            BlSales blsales = new BlSales();
            blsales.FK_Counter = 0;
            blsales.OpeningCounterClose = 0;
            blsales.CashAmount = 0;
            blsales.UserCode = hfUser.Value;
            blsales.FK_Client = Convert.ToInt64(hfClient.Value);
            blsales.Status = "Open";
            BlFunction blfun = new BlFunction();
            blsales.FK_Store = hfStore.Value == "" ? 0 : Convert.ToInt64(hfStore.Value);
            //CounterId = Convert.ToInt64(blfun.GetScalarValue("Counters", "ISNULL( ID_Counters,0) ID_Counters", "FK_Store=" + hfStore.Value == "" ? "0" : hfStore.Value + " AND  Active=1 ", ""));
          //  CounterId = Convert.ToInt64(blfun.GetScalarValue("Counters", "ISNULL(ID_Counters,0)ID_Counters", "Active=1 and  FK_Store=" + Convert.ToInt64(hfStore.Value) + "", ""));
           // CounterId = blsales.CounterOpenClose();
            CounterId = Convert.ToInt64(ddlCounter.SelectedValue);
            if (CounterId > 0)
            {
                hfCounter.Value = CounterId.ToString();
                
                LoadParticularsforCounterClose();
                LoadOpeningPayInOut();
                CalculateSumDenominationPicesAmount();
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "toastr.warning('" + "Counter Not Opened" + "')", true);
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "toastr.warning('" + ex.Message + "')", true);
        }
    }
    protected void LoadParticularsforCounterClose()
    {
        BlSales blsale = new BlSales();
        blsale.FK_Client = Convert.ToInt64(hfClient.Value);
        DataTable dtbl = new DataTable();
        dtbl = blsale.SelectAllDenominations();
        gvDenomination.DataSource = dtbl;
        gvDenomination.DataBind();
        dtbl = null;
        blsale = null;
    }
    protected void LoadOpeningPayInOut()
    {
        BlSales blsale = new BlSales();
        blsale.FK_Client = hfClient.Value == "" ? 0 : Convert.ToInt64(hfClient.Value);
        blsale.FK_Counter = hfCounter.Value == "" ? 0 : Convert.ToInt64(hfCounter.Value);
        blsale.FK_Store = hfStore.Value == "" ? 0 : Convert.ToInt64(hfStore.Value);
        blsale.Fk_Shift = Convert.ToInt64(0);

        DataTable dtbl = new DataTable();
        dtbl = blsale.SelectOpeningPayInOutPOS();

       LblCloseCounterOpening.Text = Math.Round(Convert.ToDecimal(dtbl.Rows[0]["OpeningAmount"]), 2).ToString();
       LblCloseCounterSales.Text = Math.Round(Convert.ToDecimal(dtbl.Rows[0]["SalesAmount"]), 2).ToString();
        lblCloseCounterCash.Text = Math.Round(Convert.ToDecimal(dtbl.Rows[0]["CashAmount"]), 2).ToString();
        lblCloseCounterCheque.Text = Math.Round(Convert.ToDecimal(dtbl.Rows[0]["ChequeAmount"]), 2).ToString();
        lblCloseCounterCard.Text = Math.Round(Convert.ToDecimal(dtbl.Rows[0]["CardAmount"]), 2).ToString();
        lblCloseCounterVoucher.Text = Math.Round(Convert.ToDecimal(dtbl.Rows[0]["VoucherAmount"]), 2).ToString();
      
        lblCloseCounterSalesReturn.Text = Math.Round(Convert.ToDecimal(dtbl.Rows[0]["SalesReturn"]), 2).ToString();
        dtbl = null;
        blsale = null;
    }
    protected void CalculateSumDenominationPicesAmount()
    {
        Decimal SumDenomination = 0;
        foreach (GridViewRow gr in gvDenomination.Rows)
        {
            SumDenomination = SumDenomination + Convert.ToDecimal(((Label)gr.FindControl("lblTotal")).Text);
        }


        Decimal Opening = Convert.ToDecimal(LblCloseCounterOpening.Text == "" ? "0" : LblCloseCounterOpening.Text);
        Decimal Sales = Convert.ToDecimal(LblCloseCounterSales.Text == "" ? "0" : LblCloseCounterSales.Text);
        Decimal PayIn = 0; // Convert.ToDecimal(LblCloseCounterPayIn.Text == "" ? "0" : LblCloseCounterPayIn.Text);
        Decimal PayOut = 0;// Convert.ToDecimal(LblCloseCounterPayOut.Text == "" ? "0" : LblCloseCounterPayOut.Text);
        Decimal Salesreturn = Convert.ToDecimal(lblCloseCounterSalesReturn.Text == "" ? "0" : lblCloseCounterSalesReturn.Text);
        Decimal CardSales = Convert.ToDecimal(LblCloseCounterSales.Text == "" ? "0" : lblCloseCounterCard.Text);
        Decimal ChqSales = Convert.ToDecimal(LblCloseCounterSales.Text == "" ? "0" : lblCloseCounterCheque.Text);
        Decimal VouSales = Convert.ToDecimal(LblCloseCounterSales.Text == "" ? "0" : lblCloseCounterVoucher.Text);
        Decimal CashSales = Convert.ToDecimal(lblCloseCounterCash.Text == "" ? "0" : lblCloseCounterCash.Text);

        LblCloseCounterClosingBalance.Text = Math.Round(Opening + CashSales + PayIn - PayOut - Salesreturn, 2).ToString();
        txtCounterCloseRemittance.Text = "0";
        lblCloseCounterDenomination.Text = Math.Round(SumDenomination, 2).ToString();
        lblCloseCounterDifference.Text = Math.Round(Convert.ToDecimal(LblCloseCounterClosingBalance.Text == "" ? "0" : LblCloseCounterClosingBalance.Text) - Convert.ToDecimal(lblCloseCounterDenomination.Text == "" ? "0" : lblCloseCounterDenomination.Text), 2).ToString();
    }
    public void Clear()
    {
       // gvDenomination.DataSource = null;
       // gvDenomination.DataBind();
        BlSales blsale = new BlSales();
        DataTable dtbl = new DataTable();
        dtbl = blsale.SelectAllDenominations();
        gvDenomination.DataSource = dtbl;
        gvDenomination.DataBind();
        dtbl = null;
        LblCloseCounterOpening.Text = "";
        LblCloseCounterSales.Text = "";
        lblCloseCounterCash.Text = "";
        lblCloseCounterCheque.Text = "";
        lblCloseCounterCard.Text = "";
        lblCloseCounterVoucher.Text = "";
        lblCloseCounterSalesReturn.Text = "";
        LblCloseCounterClosingBalance.Text = "";
        txtCounterCloseRemittance.Text = "0";
        lblCloseCounterDenomination.Text = "";
        lblCloseCounterDifference.Text = "";
        ddlCounter.SelectedValue = "0";
        hfCounter.Value = "0";


    }
    #endregion
    #region Events

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
             
            if(!IsPostBack)
            {
               
              //  hfStore.Value = Session["mainstore"] == null ? "0" : Session["mainstore"].ToString();
                BlFunction blfunctions1 = new BlFunction();
               // Int16 store = Convert.ToInt16(Session["mainstore"].ToString());
                Common cmn = new Common();
                hfUser.Value = Request.QueryString["uid"];
                hfClient.Value = Request.QueryString["clnt"];
                Int16 store = Convert.ToInt16(blfunctions1.GetScalarValue("Users", " isnull((select FK_Store", "Active=1 and ID_Users=" + hfUser.Value + "),0)", ""));
               hfStore.Value = store.ToString();
                CheckUserAccess();
               
                Populate populate = new Populate();
                populate.FillDropdownList(ddlCounter, "Counters", "CounterName", "ID_Counters", "ID_Counters", "  Active='True' AND FK_Client= " + hfClient.Value + "AND Cancelled=0", "");
               // Counter();
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "toastr.warning('" + ex.Message + "')", true);
        }
    }
    protected void gvDenomination_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            gvDenomination.PageIndex = e.NewPageIndex;
            LoadParticularsforCounterClose();
        }
        catch (Exception ex)
        {

        }

    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            //if (Convert.ToInt64(ddlCounter.SelectedValue) > 0)
            //{
                //if (hfCounter.Value != "" || hfCounter.Value != "0")
                //{
                    if (Validform())
                        SaveCounterClose();
                //}
                //else
                //{
                //    ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "toastr.warning('" + "Counter Not Opened" + "')", true);
                //}
           // }
            //else
            //{
            //    ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "toastr.error('" + "Please Select Counter" + "')", true);
            //}
        }
        catch (Exception ex)
        {
            //  blMessage.Items.Add(ex.Message);
        }
    }
    protected bool Validform()
    {
        decimal Balance = Convert.ToDecimal(LblCloseCounterClosingBalance.Text == "" ? "0" : LblCloseCounterClosingBalance.Text);
        decimal Denomination = Convert.ToDecimal(lblCloseCounterDenomination.Text == "" ? "0" : lblCloseCounterDenomination.Text);
        decimal Remittance = Convert.ToDecimal(txtCounterCloseRemittance.Text == "" ? "0" : txtCounterCloseRemittance.Text);
        Boolean Sts = true;
        if (Convert.ToInt64(ddlCounter.SelectedValue) == 0)
        {
            ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "toastr.error('Please Select Counter');", true);
            Sts = false;
        }
        else if (Remittance > Denomination)
        {
            ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "toastr.error('Remittance should be less than or equal to Denomination amount');", true);
            Sts = false;
        }
        else if (txtCounterCloseRemittance.Text == "")
        {
            ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "toastr.error('Remittance required');", true);

            Sts = false;
        }
        else if (Balance < Denomination)
        {
            ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "toastr.error('Denomination should be less than or equal to Closing amount');", true);

            Sts = false;
        }
        return Sts;
    }
    #endregion
    protected void btnClear_Click(object sender, EventArgs e)
    {
        BlSales blsale = new BlSales();
        DataTable dtbl = new DataTable();
        dtbl = blsale.SelectAllDenominations();
        gvDenomination.DataSource = dtbl;
        gvDenomination.DataBind();
        dtbl = null;
        LblCloseCounterOpening.Text = "0";
        ddlCounter.SelectedValue = "0";
        lblCloseCounterCard.Text = "0";
        lblCloseCounterCash.Text = "0";
        lblCloseCounterCheque.Text = "0";
        LblCloseCounterClosingBalance.Text = "0";
        lblCloseCounterDenomination.Text = "0";
        lblCloseCounterDifference.Text = "0";
        LblCloseCounterSales.Text = "0";
        lblCloseCounterSalesReturn.Text = "0";
        hfCounter.Value = "0";
        

    }
    protected void ddlCounter_SelectedIndexChanged(object sender, EventArgs e)
    {
        Counter();
    }
}