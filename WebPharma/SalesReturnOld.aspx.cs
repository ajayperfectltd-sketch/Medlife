/*----------------------------------------------------------------------------------------------------------------------------------------------------------
Purpose    : Sale return
Author     : bibin TC
Copyright  : Perfect Software Solutions (Clt) Pvt Ltd
Created on : 03/05/20118
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
using System.Data;
using System.Collections;
using System.Globalization;
using System.Text;
using System.Web.UI.HtmlControls;


public partial class SalesReturnOld : System.Web.UI.Page
{

    private const string SaleReturnItems = "SaleReturnItems";
    private const string DirectSR = "DirectSale";
    #region Functions
    public void SRPrint(long SRID)
    {
        try
        {
            BlSaleReturn blsrtn = new BlSaleReturn();
            blsrtn.MasterId = SRID;// Convert.ToInt64(hfSaleReturn.Value);
            String strHtml = blsrtn.SRHtmlLayout();
            hdfPrintField.Value = strHtml;


            #region C#CodeOfDilshad

            //            DataTable dtsalesReturnPrintlayout = new DataTable();
            //            DataTable dtSrtrn = new DataTable();
            //            DataTable dtsalesreturn = new DataTable();
            //            DataTable dtsalesreturndetail = new DataTable();
            //            BlBarcodeGeneration blbarcodegeneration = new BlBarcodeGeneration();
            //            blbarcodegeneration.FK_Client = Convert.ToInt64(hfClient.Value);
            //            blbarcodegeneration.PriLayout = Convert.ToString("SalesReturn");
            //            dtsalesReturnPrintlayout = blbarcodegeneration.SelectPrintLayoutSalesReturn();
            //            BlSaleReturn blsrtn = new BlSaleReturn();
            //            blsrtn.MasterId = Convert.ToInt64(hfSaleReturn.Value);
            //            blsrtn.FK_Client = Convert.ToInt64(hfClient.Value);
            //            blsrtn.FromDate = DateTime.Parse("1/1/1753");
            //            blsrtn.ToDate = DateTime.MaxValue;
            //            dtSrtrn = blsrtn.SelectAllDataPrint();
            //            dtsalesreturndetail = blsrtn.SelectSaleReturnItemsPrint();
            //            int tmprow = 0;
            //            //StringBuilder result = new StringBuilder();

            //            HtmlGenericControl wrappar = new HtmlGenericControl("div");
            //            HtmlGenericControl bg = new HtmlGenericControl("div");
            //            HtmlGenericControl bill_bg;
            //            HtmlGenericControl det = new HtmlGenericControl("div");

            //            int inrow = 0;
            //            for (int i = 0; i < dtsalesReturnPrintlayout.Rows.Count; i++)                                               //Looping over print layout ->@1
            //            {
            //                if (inrow != Convert.ToInt32(dtsalesReturnPrintlayout.Rows[i]["priRow"].ToString()))
            //                {
            //                    inrow = Convert.ToInt32(dtsalesReturnPrintlayout.Rows[i]["priRow"].ToString());                            //Setting Up New Row Index
            //                    bg = new HtmlGenericControl("div");
            //                    bg.Attributes.Add("style", "width:100%; margin:0px 0px 0px 0px; padding:20px 0px 0px 0px;");
            //                }
            //                if (Convert.ToString(dtsalesReturnPrintlayout.Rows[i]["PriSubItem"].ToString()) == Convert.ToString("0"))             //Main Section printing?
            //                {
            //                    if (dtSrtrn.Columns.Count > 0)                                                                                  //Check salesr Return Columns are >0 ->2
            //                    {
            //                        int SalescolumnExist = 0;
            //                        string PriField = dtsalesReturnPrintlayout.Rows[i]["PriField"].ToString();                                //Set value of Print Layout row value
            //                        for (int j = 0; j < dtSrtrn.Columns.Count; j++)                                                         //iterate over Sales return Columns ->@3
            //                        {
            //                            if (Convert.ToString(PriField.ToString()) == Convert.ToString(dtSrtrn.Columns[j].ColumnName) && Convert.ToString(dtsalesReturnPrintlayout.Rows[i]["PriSubItem"].ToString()) == Convert.ToString("0")) //Condition for Value field in main section
            //                            {
            //                                bill_bg = new HtmlGenericControl("div");
            //                                bill_bg.InnerText = Convert.ToString(dtSrtrn.Rows[0][j].ToString().Substring(0, dtSrtrn.Rows[0][j].ToString().Length > Convert.ToInt32(dtsalesReturnPrintlayout.Rows[i]["PriWidth"].ToString()) ? Convert.ToInt32(dtsalesReturnPrintlayout.Rows[i]["PriWidth"].ToString()) : dtSrtrn.Rows[0][j].ToString().Length));//Set value 
            //                                bill_bg.Attributes.Add("style", "width:auto; margin:0px 0px 0px " + (Convert.ToInt32(dtsalesReturnPrintlayout.Rows[i]["PriCol"]) * 1).ToString() + "px ; padding:0px 0px 0px " + (Convert.ToInt32(dtsalesReturnPrintlayout.Rows[i]["PriConsider"]) * 1).ToString() + "px; float:left;");
            //                                bg.Controls.Add(bill_bg);
            //                                SalescolumnExist = 1;
            //                            }
            //                        }    //@3E
            //                        if (SalescolumnExist == 0)
            //                        {
            //                            bill_bg = new HtmlGenericControl("div");
            //                            bill_bg.InnerText = "" + Convert.ToString(dtsalesReturnPrintlayout.Rows[i]["PriField"].ToString().Substring(0, dtsalesReturnPrintlayout.Rows[i]["PriField"].ToString().Length > Convert.ToInt32(dtsalesReturnPrintlayout.Rows[i]["PriWidth"].ToString()) ? Convert.ToInt32(dtsalesReturnPrintlayout.Rows[i]["PriWidth"].ToString()) : dtsalesReturnPrintlayout.Rows[i]["PriField"].ToString().Length));
            //                            bill_bg.Attributes.Add("style", "width:auto; margin:0px 0px 0px " + (Convert.ToInt32(dtsalesReturnPrintlayout.Rows[i]["PriCol"]) * 1).ToString() + "px ; padding:0px 0px 0px " + (Convert.ToInt32(dtsalesReturnPrintlayout.Rows[i]["PriConsider"]) * 1).ToString() + "px; float:left;");
            //                            bg.Controls.Add(bill_bg);
            //                            // bill_bg = null;
            //                        }
            //                        wrappar.Controls.Add(bg);
            //                        // bg = null;

            //                        //dataDiv = null;
            //                    }
            //                    else
            //                    {
            //                        //   MessageBox.Show("No data in sales to print", "POS -Medlife", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //                    }
            //                }//<-2E

            //                else
            //                {
            //                    if (Convert.ToString(dtsalesReturnPrintlayout.Rows[i]["PriSubItem"].ToString()) == Convert.ToString("1"))
            //                    {
            //                        if (dtsalesreturndetail.Columns.Count > 0)
            //                        {

            //                            int SalesDetailscolumnExist = 0;
            //                            for (int k = 0; k < dtsalesreturndetail.Columns.Count; k++) //iteration over detail Columns
            //                            {
            //                                string PriFields = dtsalesReturnPrintlayout.Rows[i]["PriField"].ToString();
            //                                if (Convert.ToString(dtsalesReturnPrintlayout.Rows[i]["PriField"].ToString()) == Convert.ToString(dtsalesreturndetail.Columns[k].ColumnName))//Check for  value or data
            //                                {


            //                                    det = new HtmlGenericControl("div");

            //                                    det.Attributes.Add("style", "width:auto; margin:0px 0px 0px 0px; padding:0px 0px 0px 0px; float:left;");


            //                                    for (int l = 0; l < dtsalesreturndetail.Rows.Count; l++)
            //                                    {


            //                                        bill_bg = new HtmlGenericControl("div");
            //                                        bill_bg.InnerText = "" + Convert.ToString(dtsalesreturndetail.Rows[l][k].ToString().Substring(0, dtsalesreturndetail.Rows[l][k].ToString().Length > Convert.ToInt32(dtsalesReturnPrintlayout.Rows[i]["PriWidth"].ToString()) ? Convert.ToInt32(dtsalesReturnPrintlayout.Rows[i]["PriWidth"].ToString()) : dtsalesreturndetail.Rows[l][k].ToString().Length));
            //                                        bill_bg.Attributes.Add("style", "width:auto; margin:0px 0px 0px " + (Convert.ToInt32(dtsalesReturnPrintlayout.Rows[i]["PriCol"]) * 1).ToString() + "px ; padding:0px 0px 0px " + (Convert.ToInt32(dtsalesReturnPrintlayout.Rows[i]["PriConsider"]) * 1).ToString() + "px; float:left;");

            //                                        det.Controls.Add(bill_bg);
            //                                        HtmlGenericControl cleardiv = new HtmlGenericControl("div");
            //                                        cleardiv.Attributes.Add("style", "clear:both");
            //                                        det.Controls.Add(cleardiv);
            //                                        cleardiv.Dispose();
            //                                        SalesDetailscolumnExist = 1;
            //                                        //  bill_bg = null;

            //                                    }
            //                                    wrappar.Controls.Add(det);
            //                                    // det = null;
            //                                }
            //                            }
            //                            if (SalesDetailscolumnExist == 0) //Check  for Column Heading
            //                            {
            //                                //if (inrow != Convert.ToInt32(dtsalesReturnPrintlayout.Rows[i]["priRow"].ToString()))
            //                                //{
            //                                //    inrow = Convert.ToInt32(dtsalesReturnPrintlayout.Rows[i]["priRow"].ToString());                               
            //                                //    det = new HtmlGenericControl("div");

            //                                //}
            //                                det.Attributes.Add("style", "width:100%; margin:0px 0px 0px 0px; padding:0px 0px 0px 0px; float:left;");
            //                                bill_bg = new HtmlGenericControl("div");
            //                                bill_bg.InnerText = "" + Convert.ToString(dtsalesReturnPrintlayout.Rows[i]["PriField"].ToString().Substring(0, dtsalesReturnPrintlayout.Rows[i]["PriField"].ToString().Length > Convert.ToInt32(dtsalesReturnPrintlayout.Rows[i]["PriWidth"].ToString()) ? Convert.ToInt32(dtsalesReturnPrintlayout.Rows[i]["PriWidth"].ToString()) : dtsalesReturnPrintlayout.Rows[i]["PriField"].ToString().Length));
            //                                bill_bg.Attributes.Add("style", "width:auto; margin:0px 0px 0px " + (Convert.ToInt32(dtsalesReturnPrintlayout.Rows[i]["PriCol"]) * 1).ToString() + "px ; padding:0px 0px 0px " + (Convert.ToInt32(dtsalesReturnPrintlayout.Rows[i]["PriConsider"]) * 1).ToString() + "px; float:left;");
            //                                det.Controls.Add(bill_bg);
            //                                wrappar.Controls.Add(det);
            //                            }

            //                        }


            //                    }
            //                    if (Convert.ToString(dtsalesReturnPrintlayout.Rows[i]["PriSubItem"].ToString()) == Convert.ToString("2"))
            //                    {
            //                    }

            //                }

            //            }
            //            try
            //            {
            //                StringBuilder sb = new StringBuilder();
            //                System.IO.StringWriter sw = new System.IO.StringWriter(sb);
            //                HtmlTextWriter hw = new HtmlTextWriter(sw);
            //                wrappar.RenderControl(hw);
            //                String HTMLContent = sb.ToString();

            //                hdfPrintField.Value = "<html><body>" + HTMLContent + "</body></html>";
            //                det.Dispose();
            //                wrappar.Dispose();
            //                bill_bg = null;
            //                bg.Dispose(); 
            #endregion


            ScriptManager.RegisterStartupScript(Page, typeof(Page), "script", " PrintButtonClick();", true);

        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "toastr.warning('" + ex.Message + "')", true);
        }
    }
    private void ControlEnabled(bool status)
    {
        txtDate.Enabled = status;
        ddlStore.Enabled = status;
        ddlCounter.Enabled = status;
        ddlBillType.Enabled = status;
        ddlCategory.Enabled = status;
        txtSales.Enabled = status;
        imgbtnSales.Enabled = status;
        cbxIsCredit.Enabled = status;
        txtCustRefno.Enabled = status;
        imgbtnCustomer.Enabled = status;
        txtitem.Enabled = status;
        ImageButton3.Enabled = status;
        txtQuantity.Enabled = status;
        btnAddItems.Enabled = status;
        btnClearItems.Enabled = status;
        txtRemark.Enabled = status;
        btnSave.Visible = status;
        btnPrint.Visible = !status;
        btnAddItems.Visible = status;
        btnClearItems.Visible = status;
        gvItems.Enabled = status;
        gvSaleItems.Enabled = status;

        if (status)
        {
            if (rdItemSale.Checked)
            {
                // ddlStore.Enabled = true;
                ddlBillType.Enabled = true;
                ddlCategory.Enabled = true;
                //ddlCustomer.Enabled = true;
                CustomerEnabled(true);
            }
            else
            {
                // ddlStore.Enabled = true;
                ddlBillType.Enabled = false;
                //ddlCustomer.Enabled = true;
                CustomerEnabled(false);
            }
        }
        // btnClear.Visible = status;
    }
    private void CustomerEnabled(bool status)
    {
        txtCustRefno.Enabled = status;
        imgbtnCustomer.Enabled = status;
        txtCustomer.Enabled = status;
    }
    private void SaleEnable(bool status)
    {
        txtSaleRefno.Enabled = status;
        imgbtnSales.Enabled = status;
        txtSales.Enabled = status;
    }

    public void SetDetails(string strName, string strRefno)
    {
        if (CommonPopup1.CallingFunction == "CustomerFill")
        {
            txtCustRefno.Text = strRefno;
            txtCustomer.Text = strName;


        }

    }
    public void SaleDetail(string strName, string strRefno, bool status)
    {

        txtSaleRefno.Text = strRefno;
        txtSales.Text = strName;
        cbxIsCredit.Checked = status;
        HiddenbtnClick();



    }
    private void HiddenbtnClick()
    {

        if (txtSaleRefno.Text != "" && txtSales.Text != "")
        {
            BindSaleItems(Convert.ToInt64(txtSaleRefno.Text));

        }
        else
        {
            gvSaleItems.DataSource = null;
            gvSaleItems.DataBind();
            ViewState[SaleReturnItems] = null;
        }

    }
    private void ClearCustomer()
    {
        txtCustRefno.Text = "";
        txtCustomer.Text = "";

    }
    private void ClearSales()
    {
        txtSales.Text = "";
        txtSaleRefno.Text = "";
    }
    public void BillNoFill(int pageIndex)
    {
        //AND   BillNo LIKE '" + strName + "%'"
        Populate Populate = new Populate();
        CommonPopup1.CallingFunction = "BillNoFill";
        string strName = ((TextBox)CommonPopup1.FindControl("txtName")).Text;
        string Refno = ((TextBox)CommonPopup1.FindControl("txtRefno")).Text;
        Refno = Refno == string.Empty || Refno == null ? "0" : Refno;
        string searchCol = ((HiddenField)CommonPopup1.FindControl("hfColumn")).Value;
        searchCol = searchCol == string.Empty ? "[Name]" : "[" + searchCol + "]";
        Populate.FillGrid((GridView)CommonPopup1.FindControl("gdvSearch"), "Sales", "ID_Sales as ID,BillNo as Name,(Select Top 1 TypeName from BillType Where ID_BillType=FK_BillType) as [Bill Type],CustomerName As Customer ,dbo.DateFormats(BillDate) As [Bill Date],BillTotal as [Bill Total],credit as Credit", "ID_Sales", "", "FK_Client=" + hfClient.Value.Split(',')[0] + " AND FK_Store= (CASE WHEN " + ddlStore.SelectedValue + "=0 THEN FK_Store ELSE  " + ddlStore.SelectedValue + " END) AND FK_Counter= (CASE WHEN " + ddlCounter.SelectedValue + "=0 THEN FK_Counter ELSE  " + ddlCounter.SelectedValue + " END)    AND  (ID_Sales =" + Refno + " OR 0=" + Refno + ")  AND (ISNULL(Cancelled,0)= 0)  AND ((ISNULL((Select SUM(Quantity)  From Sales as S Join SalesDetails On FK_Sales=ID_Sales   where ID_Sales=Sales.ID_Sales And S.Cancelled=0),0)))-((ISNULL((SELECT      SUM(SRD.Quantity)  FROM            SRDetails AS SRD INNER JOIN   SalesReturn AS SR ON SRD.FK_SalesReturn = SR.ID_SalesReturn INNER JOIN  Sales AS S ON SR.Fk_Sale = S.ID_Sales WHere S.ID_Sales=Sales.ID_Sales AND SR.Cancelled=0),0)) )>0 ", searchCol + "  LIKE '" + strName + "%'", pageIndex, 10, "", (Repeater)CommonPopup1.FindControl("rptPager"), this.Page);
        Populate = null;
        txtSaleRefno.Focus();
        if (txtSaleRefno.Text == string.Empty)
        {
            lblPopupHeader.Text = "Sales";
            mpCommon.Show();
        }

    }
    public void CustomerFill(int pageIndex)
    {
        Populate Populate = new Populate();
        CommonPopup1.CallingFunction = "CustomerFill";
        string strName = ((TextBox)CommonPopup1.FindControl("txtName")).Text;
        string Refno = ((TextBox)CommonPopup1.FindControl("txtRefno")).Text;
        Refno = Refno == string.Empty || Refno == null ? "0" : Refno;
        string strAccountHead = txtSaleRefno.Text == "" ? "0" : txtSaleRefno.Text;
        string searchCol = ((HiddenField)CommonPopup1.FindControl("hfColumn")).Value;
        searchCol = searchCol == string.Empty ? "[Name]" : "[" + searchCol + "]";
        Populate.FillGrid((GridView)CommonPopup1.FindControl("gdvSearch"), "Customer", "ID_Customer as ID, ShortName+'/'+Name as Name,ShortName,ID_Customer As [Ref No]", "ID_Customer", "", "FK_Client=" + hfClient.Value.Split(',')[0] + " AND (ID_Customer =" + Refno + " OR 0=" + Refno + ")  AND Active='True' ", searchCol + "  LIKE '" + strName + "%'", pageIndex, 10, "", (Repeater)CommonPopup1.FindControl("rptPager"), this.Page);
        Populate = null;
        if (txtCustomer.Text == string.Empty)
        {
            lblPopupHeader.Text = "Customer";
            mpCommon.Show();
        }
    }
    protected void BindData()
    {
        Populate populate = new Populate();
        populate.FillDropdownList(ddlStore, "Store", "StoName", "ID_Store", "StoName", "     Active='True'   and FK_Client = " + hfClient.Value, "");
        populate.FillDropdownList(ddlBillType, "Billtype", "TypeName", "ID_Billtype", "TypeName", "    BillType=1 and FK_Client = " + hfClient.Value, "");
        populate.FillDropdownList(ddlSearchCustomer, "Customer", "Name", "ID_Customer", "Name", "  Active='True'  and FK_Client = " + hfClient.Value, "");
        populate.FillDropdownList(ddlSearchStore, "Store", "StoName", "ID_Store", "StoName", "   Active='True'   and FK_Client = " + hfClient.Value, "");
        populate.FillDropdownList(ddlCategory, "CategoryFirst", "Name", "ID_CategoryFirst", "Name", "  FK_Client= " + hfClient.Value.Split(',')[0], "");
        populate.FillDropdownList(ddlCounter, "Counter", "Name", "ID_Counter", "Name", "  Active='True'   AND  FK_Client= " + hfClient.Value.Split(',')[0], "");

        populate = null;
        txtDate.Text = DateTime.Now.ToShortDateString();
    }
    public void SetReadonly()
    {
        txtDate.Attributes.Add("readonly", "readonly");
        txtFromDate.Attributes.Add("readonly", "readonly");
        txtTodate.Attributes.Add("readonly", "readonly");
        txtDate.Text = txtFromDate.Text = txtTodate.Text = DateTime.Now.ToString("dd/MM/yyyy");
        txtTotalAmount.Attributes.Add("readonly", "readonly");
        txtRoundOff.Attributes.Add("readonly", "readonly");
        txtNetAmount.Attributes.Add("readonly", "readonly");
        txtVAT.Attributes.Add("readonly", "readonly");
        
        txtCustomer.Attributes.Add("readonly", "readonly");
        txtDiscountAmt.Attributes.Add("readonly", "readonly");
        txtQuantity.Attributes["onfocus"] = "javascript:this.select();";
        txtCESS.Attributes.Add("readonly", "readonly");
    }
    private void BindSaleItems(long SaleId)
    {
        try
        {
            BlSaleReturn blSaleReturn = new BlSaleReturn();
            blSaleReturn.Fk_Sale = SaleId;
            blSaleReturn.FK_Client = Convert.ToInt32(hfClient.Value);

            if (hfSaleReturn.Value == "")
            {
                blSaleReturn.MasterId = 0;
            }
            else
            {
                blSaleReturn.MasterId = Convert.ToInt64(hfSaleReturn.Value);
            }

            DataTable dtbl = blSaleReturn.SelectSaleReturnItems();
            hfBillNo.Value = dtbl.Rows[0]["BillNo"].ToString();
            hfBillType.Value = dtbl.Rows[0]["FK_BillType"].ToString();
            ddlBillType.SelectedValue = dtbl.Rows[0]["FK_BillType"].ToString();
            gvSaleItems.DataSource = dtbl;
            gvSaleItems.DataBind();

            if (dtbl.Rows.Count > 0 && blSaleReturn.MasterId == 0)
            {
                try
                {
                    ddlStore.SelectedValue = dtbl.Rows[0]["FK_Store"].ToString();
                    ddlStore.Enabled = false;
                    ddlCounter.SelectedValue = dtbl.Rows[0]["FK_Counter"].ToString();
                    ddlCounter.Enabled = false;



                }
                catch (Exception ex)
                {
                    ddlStore.SelectedValue = "0";
                    ddlStore.Enabled = false;
                    ddlCounter.SelectedValue = "0";
                    ddlCounter.Enabled = true;
                }
                try
                {
                    txtCustRefno.Text = dtbl.Rows[0]["FK_Customer"].ToString() == "0" ? "" : dtbl.Rows[0]["FK_Customer"].ToString();
                    txtCustomer.Text = dtbl.Rows[0]["CustomerName"].ToString();


                }
                catch (Exception ex)
                {
                    txtCustRefno.Text = "";
                    txtCustomer.Text = "";
                }
                hdfDate.Value = dtbl.Rows[0]["BillDate"].ToString();
            }

            ViewState[SaleReturnItems] = dtbl;
            TotalAmount(dtbl);
            blSaleReturn = null;
        }
        catch (Exception ex)
        {

        }
    }
    protected void CheckUserAccess()
    {
        BlUserAccess bluseraccess = new BlUserAccess();
        Populate populate = new Populate();

        bluseraccess.FK_Users = Convert.ToInt64(hfUser.Value);
        bluseraccess.FK_Pages = 66;
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
    protected void GridFill(int currentPage)
    {
        BlSaleReturn blSaleReturn = new BlSaleReturn();
        DataTable dtbl = new DataTable();
        blSaleReturn.MasterId = 0;
        blSaleReturn.FK_Customer = Convert.ToInt64(ddlSearchCustomer.SelectedValue);
        blSaleReturn.FK_Store = Convert.ToInt64(ddlSearchStore.SelectedValue);
        blSaleReturn.FK_Client = Convert.ToInt64(hfClient.Value);
        blSaleReturn.FromDate = DateTime.ParseExact(txtFromDate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
        blSaleReturn.ToDate = DateTime.ParseExact(txtTodate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
        currentPage = currentPage == 0 ? 1 : currentPage;
        blSaleReturn.PageIndex = Convert.ToInt16(currentPage);
        dtbl = blSaleReturn.SelectAllData();
        gvSaleReturn.DataSource = dtbl;
        gvSaleReturn.DataBind();
        if (dtbl.Rows.Count > 0)
        {
            int recordCount = Convert.ToInt32(dtbl.Rows[0]["RecordCount"].ToString());
            this.PopulatePager(recordCount, currentPage);

        }
        blSaleReturn = null;

    }
    protected void Save()
    {

        long Status = 0;
        try
        {
            if (rdSales.Checked)
            {

                DateTime SR = DateTime.ParseExact(txtDate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                DateTime dtP = DateTime.Parse(hdfDate.Value);
                if (SR < dtP)
                {
                    Status = 1;
                }
            }
        }
        catch
        {
            //Status = 1;
        }


        if (Status == 0)
        {
            BlSaleReturn blSaleReturn = new BlSaleReturn();
            blSaleReturn.FK_Store = Convert.ToInt64(ddlStore.SelectedValue);
            if (rdItemSale.Checked == true)
            {
                blSaleReturn.FK_BillType = Convert.ToInt64(ddlBillType.SelectedValue);
            }
            else if (rdSales.Checked == true)
            {
                blSaleReturn.FK_BillType = Convert.ToInt64(hfBillType.Value);
            }


            blSaleReturn.BillNo = Convert.ToInt64(hfBillNo.Value);
            blSaleReturn.BillDate = DateTime.ParseExact(txtDate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            blSaleReturn.CustomerType = 0;
            blSaleReturn.FK_Customer = Convert.ToInt64(txtCustRefno.Text == "" || txtCustomer.Text == "" ? "0" : txtCustRefno.Text);
            blSaleReturn.CustomerName = txtCustomer.Text;
            blSaleReturn.BillTotal = Convert.ToDecimal(txtTotalAmount.Text == "" ? "0" : txtTotalAmount.Text);
            blSaleReturn.Discount = Convert.ToDecimal(txtDiscountAmt.Text == "" ? "0" : txtDiscountAmt.Text);
            blSaleReturn.RoundOff = Convert.ToDecimal(txtRoundOff.Text == "" ? "0" : txtRoundOff.Text);
            blSaleReturn.NetAmount = Convert.ToDecimal(txtNetAmount.Text == "" ? "0" : txtNetAmount.Text);
            blSaleReturn.PaymentMode = 0;
            blSaleReturn.FK_PaymentDetails = 0;
            blSaleReturn.PayAmount = 0;
            blSaleReturn.Remarks = txtRemark.Text;
            blSaleReturn.UserCode = hfUser.Value;
            blSaleReturn.EnteredOn = DateTime.Now;
            blSaleReturn.Cancelled = false;
            blSaleReturn.Cancelleduser = hfUser.Value;
            blSaleReturn.FK_Counter = Convert.ToInt64(ddlCounter.SelectedValue);
            blSaleReturn.FK_Client = Convert.ToInt64(hfClient.Value);
            blSaleReturn.Fk_Sale = Convert.ToInt64(txtSaleRefno.Text == "" ? "0" : txtSaleRefno.Text);
            blSaleReturn.DirectReturn = rdItemSale.Checked;
            blSaleReturn.Credit = cbxIsCredit.Checked;
            ArrayList arlist = new ArrayList();
            BlSaleReturnDetails blSaleReturnDetails;
            if (blSaleReturn.DirectReturn)
            {
                int RowIndex = 0;
                DataTable dtbl = (DataTable)ViewState[DirectSR];
                foreach (DataRow dr in dtbl.Rows)
                {
                    blSaleReturnDetails = new BlSaleReturnDetails();
                    blSaleReturnDetails.FK_Items = Convert.ToInt64(dr["FK_Items"].ToString());
                    blSaleReturnDetails.FK_Stock = Convert.ToInt64(dr["FK_Stock"].ToString());
                    blSaleReturnDetails.MRP = Convert.ToDecimal(dr["MRP"].ToString());
                    blSaleReturnDetails.SalesPrice = Convert.ToDecimal(dr["SalesPrice"].ToString());
                    blSaleReturnDetails.Quantity = Convert.ToDecimal(dr["Quantity"].ToString());
                    blSaleReturnDetails.VAT = Convert.ToDecimal(dr["VAT"].ToString());
                    blSaleReturnDetails.CESS = Convert.ToDecimal(dr["CESS"].ToString());
                    blSaleReturnDetails.GroupId = Convert.ToInt32(dr["GroupId"].ToString());
                    blSaleReturnDetails.FK_Reason = Convert.ToInt64((((DropDownList)gvItems.Rows[RowIndex].FindControl("ddlReason")).SelectedValue));
                    blSaleReturnDetails.SlNo = RowIndex + 1;
                    arlist.Add(blSaleReturnDetails);
                    blSaleReturnDetails = null;
                    RowIndex = RowIndex + 1;



                }
            }
            else
            {
                int RowIndex = 0;
                foreach (GridViewRow gr in gvSaleItems.Rows)
                {
                    if (gr.RowType == DataControlRowType.DataRow)
                    {
                        if (Convert.ToDecimal(((TextBox)gr.FindControl("txtQuantity")).Text) > 0)
                        {
                            blSaleReturnDetails = new BlSaleReturnDetails();
                            blSaleReturnDetails.FK_Items = Convert.ToInt64(gvSaleItems.DataKeys[gr.RowIndex]["FK_Items"].ToString());
                            blSaleReturnDetails.FK_Stock = Convert.ToInt64(gvSaleItems.DataKeys[gr.RowIndex]["FK_Stock"].ToString());
                            blSaleReturnDetails.MRP = Convert.ToDecimal(gvSaleItems.DataKeys[gr.RowIndex]["MRP"].ToString());
                            string str3 = (((Label)gr.FindControl("lblSalesPrice")).Text);
                            str3 = str3 == "" ? "0" : str3;
                            blSaleReturnDetails.SalesPrice = Convert.ToDecimal(str3);
                            blSaleReturnDetails.Quantity = Convert.ToDecimal(((TextBox)gr.FindControl("txtQuantity")).Text);
                            blSaleReturnDetails.VAT = Convert.ToDecimal(gvSaleItems.DataKeys[gr.RowIndex]["VAT"].ToString());
                            blSaleReturnDetails.CESS = Convert.ToDecimal(gvSaleItems.DataKeys[gr.RowIndex]["CESS"].ToString());
                            blSaleReturnDetails.GroupId = Convert.ToInt32(gvSaleItems.DataKeys[gr.RowIndex]["GroupId"].ToString());
                            blSaleReturnDetails.FK_Reason = Convert.ToInt64((((DropDownList)gvSaleItems.Rows[RowIndex].FindControl("ddlReason")).SelectedValue));
                            blSaleReturnDetails.SlNo = Convert.ToInt32(gvSaleItems.DataKeys[gr.RowIndex]["SLNO"].ToString());
                            arlist.Add(blSaleReturnDetails);
                            blSaleReturnDetails = null;
                            RowIndex = RowIndex + 1;
                        }
                    }
                }
            }
            blSaleReturn.XmlSR = Common.WriteToXml(arlist);
            if (Status == 0)
            {
                if (hfSaleReturn.Value == string.Empty)
                {
                    if (hfIsAdd.Value.ToLower() == "true")
                    {
                        Status = blSaleReturn.InsertData();
                        if (Status > 0)
                        {
                            string confirmValue = Request.Form["confirm_Print"];
                            if (confirmValue.Length > 3)
                                confirmValue = confirmValue.Substring(confirmValue.Length - 3, 3);
                            if (confirmValue == "Yes")
                            {

                            }


                            ClearAll();
                            GridFill(1);
                            ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "ShowAddDiv();", true);
                            Populate.InsertMsg(this);
                            ddlStore.SelectedValue = Session["mainstore"].ToString();
                            ddlStore.Enabled = false;
                        }
                        else if (Status == -1 || Status == -2)
                        {
                            ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "ShowAddDiv();", true);
                            Populate.ErrorMessageBox(this, "Cannot insert data, No stock  available.");


                        }
                        else if (Status == -3)
                        {
                            Populate.ErrorMessageBox(this, " Return AccHead has not set in Bill Type");
                        }
                        else
                        {
                            Populate.ErrorMessageBox(this, "Cannot insert data");

                        }
                    }
                    else
                    {
                        ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "toastr.warning('User does not have permission to add')", true);

                    }

                }
                else
                {
                    long decid = Convert.ToInt64(hfSaleReturn.Value);
                    blSaleReturn.MasterId = decid;
                    if (hfIsUpdate.Value.ToLower() == "true")
                    {

                        Status = blSaleReturn.UpdateData();
                        if (Status > 0)
                        {
                            ClearAll();
                            GridFill(1);
                            Populate.UpdateMsg(this);
                        }

                        else if (Status == -1 || Status == -2)
                        {
                            ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "ShowAddDiv();", true);
                            Populate.ErrorMessageBox(this, "Cannot update data, No stock  available.");
                        }
                        else
                        {
                            Populate.ErrorMessageBox(this, "Cannot update data");

                        }
                    }
                    else
                    {
                        ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "toastr.warning('User does not have permission to Update')", true);
                    }


                }
            }
            else
            {
                Populate.ErrorMessageBox(this, "Quantity Cannot Exceed Available Stock");
            }
        }
        else
        {
            Populate.ErrorMessageBox(this, "Sales Return date cannot be less than Sales date");
        }

    }
    public void ClearAll()
    {
        ViewState[SaleReturnItems] = null;
        gvSaleItems.DataSource = null;
        gvSaleItems.DataBind();
        ViewState[DirectSR] = null;
        gvItems.DataSource = null;
        gvItems.DataBind();
        divAdd.Style.Add("display", "block;");
        divView.Style.Add("display", "none;");
        rdSales.Enabled = true;
        rdItemSale.Enabled = true;
        SaleEnable(true);
        ddlStore.Enabled = false;
        ddlBillType.Enabled = true;
        ddlCategory.Enabled = true;

        btnSave.Enabled = true;
        ClearItems();
        ControlEnabled(true);
        cbxIsCredit.Checked = false;
        cbxIsCredit.Visible = true;
        // cbxPrint.Checked = false;

        ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "ClearAdd();", true);
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
    private void ClearItems()
    {
        ViewState[DirectSR] = null;
        gvItems.DataSource = null;
        gvItems.DataBind();

        DisplayDiv();


    }
    private void DisplayDiv()
    {
        if (rdItemSale.Checked)
        {

            //Item wise
            gvSaleItems.Visible = false;
            divItemPurchaseReturn.Style.Add("display", "block");
            ddlStore.Enabled = false;
            ddlBillType.Enabled = true;
            ddlCategory.Enabled = true;
            //ddlCustomer.Enabled = true;
            CustomerEnabled(true);
            // ddlSales.SelectedValue = "0";
            ClearSales();
            liPurchase.Visible = false;
            liCategory.Visible = true;
            gvSaleItems.DataSource = null;
            gvSaleItems.DataBind();
            ViewState[SaleReturnItems] = null;
            gvsi.Visible = false;
        }
        else
        {


            divItemPurchaseReturn.Style.Add("display", "none");
            gvSaleItems.Visible = true;
            ddlStore.Enabled = false;
            ddlBillType.Enabled = false;
            CustomerEnabled(false);
            //ddlCustomer.Enabled = false;
            liPurchase.Visible = true;
            liCategory.Visible = false;
            gvItems.DataSource = null;
            gvItems.DataBind();
            ViewState[DirectSR] = null;
            gvsi.Visible = true;

        }
    }
    private DataTable GetDataTable(DataTable dtbl)
    {
        dtbl.Columns.Add("slno");
        dtbl.Columns.Add("Item");
        dtbl.Columns.Add("FK_Items");
        dtbl.Columns.Add("Quantity");
        dtbl.Columns.Add("TotalAmt", typeof(decimal));
        dtbl.Columns.Add("FK_Reason");
        dtbl.Columns.Add("FK_Stock");
        dtbl.Columns.Add("MRP");
        dtbl.Columns.Add("SalesPrice");
        dtbl.Columns.Add("VAT");
        dtbl.Columns.Add("CESS");
        dtbl.Columns.Add("VATTotal", typeof(decimal));
        dtbl.Columns.Add("Totalsp", typeof(decimal));
        dtbl.Columns.Add("GroupId");
        dtbl.Columns.Add("RoundOff");
         dtbl.Columns.Add("CESSTotal", typeof(decimal));
         

        return dtbl;

    }
    protected int AddDataItem()
    {


        DataTable dtbl = new DataTable();
        int inRowCount = 0;
        int inCheck = 0;
        if (ViewState[DirectSR] != null)
        {
            dtbl = (DataTable)ViewState[DirectSR];
            inRowCount = dtbl.Rows.Count;
        }
        else
        {

            dtbl = GetDataTable(dtbl);
        }
        if (dtbl.Rows.Count > 0)
        {

            for (int i = 0; i < dtbl.Rows.Count; i++)
            {
                if (dtbl.Rows[i]["FK_Stock"].ToString() == hdfFKStock.Value)
                {
                    inCheck = inCheck + 1;
                    break;
                }

                dtbl.Rows[i]["FK_Reason"] = ((DropDownList)gvItems.Rows[i].FindControl("ddlReason")).SelectedValue;
            }

        }
        if (inCheck == 0)
        {
            //get details using fk_stock

            //select purchase rate
            //bill No?
            string Unit = "";

            string BatchNo = "";
            decimal VAT = 0;
            decimal MRP = 0;
            decimal SalesPrice = 0;
            decimal CESS = 0;
            if (rdItemSale.Checked)
            {
                BlUnit blunit = new BlUnit();
                blunit.MasterId = Convert.ToInt64(hfFK_unit.Value);
                blunit.UserCode = hfUser.Value;
                blunit.FK_Client = Convert.ToInt64(hfClient.Value);
                blunit.SelectData();
                Unit = blunit.Name;

                BlStock blStock = new BlStock();
                blStock.MasterId = Convert.ToInt64(hdfFKStock.Value);
                blStock.UserCode = hfUser.Value;
                blStock.FK_Client = Convert.ToInt64(hfClient.Value);
                blStock.SelectData();
                // PurchaseRate = blStock.PurRate;
                BatchNo = blStock.BatchNo;
                MRP = blStock.MRP;
                //SalesPrice = blStock.RetailPrice;
                //VAT = blStock.VAT;
                //CESS = blStock.CESS;
            }

            //long decqty = Convert.ToInt64(txtQuantity.Text == "" ? "0" : txtQuantity.Text);
            decimal decqty = Convert.ToDecimal(txtQuantity.Text == "" ? "0" : txtQuantity.Text);
            decimal Total = (decqty * MRP);//qty*puramt+vat
            DataRow drow = dtbl.NewRow();
            drow["slno"] = inRowCount + 1;
            drow["Item"] = hfItem.Value;
            drow["FK_Items"] = hdfFkItem.Value;
            drow["Quantity"] = decqty;
            drow["TotalAmt"] = Total;
            drow["FK_Stock"] = hdfFKStock.Value;
            drow["MRP"] = MRP;
            drow["SalesPrice"] = SalesPrice;
            decimal vatamt = (decqty * (SalesPrice - (SalesPrice / ((VAT + 100) / 100))));
            drow["VATTotal"] = vatamt;
            drow["VAT"] = VAT;

            decimal CessAmt = (decqty * (SalesPrice - (SalesPrice / ((CESS + 100) / 100))));
            drow["CESSTotal"] = CessAmt;
            drow["CESS"] = CESS;



            drow["FK_Reason"] = "0";
            drow["GroupId"] = "0";
            drow["Totalsp"] = (decqty * SalesPrice);

            dtbl.Rows.InsertAt(drow, inRowCount);
            gvItems.DataSource = dtbl;
            gvItems.DataBind();
            TotalAmount(dtbl);

            ViewState[DirectSR] = dtbl;
            FillReasonddl(false);

        }
        else
        {
            ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "alertify.alert('Item already exists')", true);
            return 1;
        }
        return 0;

    }
    protected void TotalAmount(DataTable dtbl)
    {

        try
        {
            if (txtSaleRefno.Text != "" && txtSales.Text != "")
            {

                ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "CalculateTotalAmount()", true);

            }
            else
            {
                decimal totalSp = decimal.Parse((dtbl.Compute("Sum(Totalsp)", "")).ToString());
                decimal VATTotal = decimal.Parse((dtbl.Compute("Sum(VATTotal)", "")).ToString());
                decimal CESSTotal = decimal.Parse((dtbl.Compute("Sum(CESSTotal)", "")).ToString());

                decimal TotalMRP = decimal.Parse((dtbl.Compute("Sum(TotalAmt)", "")).ToString());

                decimal disAmt = TotalMRP - totalSp;
                txtTotalAmount.Text = Math.Round(TotalMRP, 2).ToString();
                txtDiscountAmt.Text = Math.Round(disAmt, 2).ToString();
                txtVAT.Text = Math.Round(VATTotal, 2).ToString();
                txtCESS.Text = Math.Round(CESSTotal, 2).ToString();
                ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "Total()", true);

            }
        }
        catch (Exception ex)
        {
            ClearTotal();
        }
        //    }
        //}
    }
    protected void ClearTotal()
    {
        txtTotalAmount.Text = "0";
        txtRoundOff.Text = "0";
        txtDiscountAmt.Text = "0";
        txtVAT.Text = "0";
        txtCESS.Text = "0";
        txtNetAmount.Text = "0";
    }
    protected void FillReasonddl(Boolean rdSales)
    {
        if (rdSales)
        {
            DataTable dtbl = (DataTable)ViewState[SaleReturnItems];
            for (int i = 0; i < gvSaleItems.Rows.Count; i++)
            {

                Populate populate = new Populate();
                populate.FillDropdownList(((DropDownList)gvSaleItems.Rows[i].FindControl("ddlReason")), "Reason", "Reason", "ID_Reason", "ID_Reason", " FK_Client = " + hfClient.Value, "");
                ((DropDownList)gvSaleItems.Rows[i].FindControl("ddlReason")).SelectedValue = dtbl.Rows[i]["FK_Reason"].ToString();
            }


        }
        else
        {

            DataTable dtbl = (DataTable)ViewState[DirectSR];
            for (int i = 0; i < gvItems.Rows.Count; i++)
            {

                Populate populate = new Populate();
                populate.FillDropdownList(((DropDownList)gvItems.Rows[i].FindControl("ddlReason")), "Reason", "Reason", "ID_Reason", "ID_Reason", " FK_Client = " + hfClient.Value, "");
                string mm = dtbl.Rows[i]["FK_Reason"].ToString();
                ((DropDownList)gvItems.Rows[i].FindControl("ddlReason")).SelectedValue = dtbl.Rows[i]["FK_Reason"].ToString();
            }


        }
    }
    #endregion
    #region Events
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                hfUser.Value = Request.QueryString["uid"];
                hfClient.Value = Request.QueryString["clnt"];

                ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "ClearAdd();", true);
                divAdd.Style.Add("display", "block;");
                divView.Style.Add("display", "none;");
                CheckUserAccess();
                SetReadonly();
                BindData();
             
                ddlBillType.Enabled = false;
                CustomerEnabled(false);
                rdSales.Checked = true;
                rdSales.Focus();
                DisplayDiv();
               // ddlStore.Enabled = true;
                GridFill(1);
                txtSales.Attributes.Add("readonly", "readonly");
                txtDate.Focus();
                btnPrint.Visible = false;
                ddlStore.SelectedValue = Session["mainstore"].ToString();
                ddlStore.Enabled = false;

            }
        }
        catch (Exception ex)
        {
            Populate.WarningMsg(this, ex.ToString());
        }

    }
    protected void imgbtnView_Click(object sender, ImageClickEventArgs e)
    {

        try
        {

            divAdd.Style.Add("display", "none");
            divView.Style.Add("display", "block");
            ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "ClearAdd();ClearSearch();", true);
            GridFill(1);
        }
        catch (Exception ex)
        {
            Populate.WarningMsg(this, ex.ToString());
        }
    }
    protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            ClearAll();
            ddlStore.SelectedValue = Session["mainstore"].ToString();
            ddlStore.Enabled = false;
        }
        catch (Exception ex)
        {
            Populate.WarningMsg(this, ex.ToString());
        }
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            System.Threading.Thread.Sleep(1000);
            string confirmValue = Request.Form["confirm_value"];
            if (confirmValue.Length > 3)
                confirmValue = confirmValue.Substring(confirmValue.Length - 3, 3);
            if (confirmValue == "Yes")
            {

                bool Status = false;
                if (rdSales.Checked && gvSaleItems.Rows.Count > 0)
                {
                    Status = true;


                }
                else if (rdItemSale.Checked && gvItems.Rows.Count > 0)
                {
                    Status = true;
                }
                if (Status)
                {
                    Save();
                    txtDate.Focus();
                }
                else
                {
                    ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "alertify.alert('Atleast one item required')", true);
                }
            }
        }
        catch (Exception ex)
        {
            Populate.WarningMsg(this, ex.ToString());
        }
    }
    protected void gvSaleItems_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {

        try
        {
            string confirmValue = Request.Form["confirm_value"];
            if (confirmValue.Length > 3)
                confirmValue = confirmValue.Substring(confirmValue.Length - 3, 3);
            if (confirmValue == "Yes")
            {
                if (ViewState[SaleReturnItems] != null)
                {
                    DataTable dtbl = (DataTable)ViewState[SaleReturnItems];
                    if (dtbl.Columns["FK_Reason"] == null)
                        dtbl.Columns.Add("FK_Reason");

                    for (int i = 0; i < dtbl.Rows.Count; i++)
                    {
                        dtbl.Rows[i]["FK_Reason"] = ((DropDownList)gvSaleItems.Rows[i].FindControl("ddlReason")).SelectedValue;
                        dtbl.Rows[i]["Quantity"] = ((TextBox)gvSaleItems.Rows[i].FindControl("txtQuantity")).Text;
                        dtbl.Rows[i]["TotalAmt"] = Convert.ToDecimal(((Label)gvSaleItems.Rows[i].FindControl("lblTotalAmt")).Text);

                    }
                    //keep all reason  in dttbl

                    dtbl.Rows.RemoveAt(e.RowIndex);
                    //slno
                    for (int i = 0; i < dtbl.Rows.Count; i++)
                    {
                        dtbl.Rows[i]["slno"] = i + 1;

                    }

                    TotalAmount(dtbl);
                    gvSaleItems.DataSource = dtbl;
                    gvSaleItems.DataBind();
                    ViewState[SaleReturnItems] = dtbl;
                    FillReasonddl(true);
                }
            }
        }
        catch (Exception ex)
        {
            Populate.WarningMsg(this, ex.ToString());
        }

    }
    protected void Page_Changed(object sender, EventArgs e)
    {
        // blMessage.Items.Clear();
        int pageIndex = int.Parse((sender as LinkButton).CommandArgument);
        DataTable dtbl = new DataTable();
        BlSaleReturn blSaleReturn = new BlSaleReturn();
        blSaleReturn.PageSize = 10;
        blSaleReturn.PageIndex = Convert.ToInt16(pageIndex);
        GridFill(blSaleReturn.PageIndex);
        blSaleReturn = null;
    }
    protected void gvSaleReturn_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            string confirmValue = Request.Form["confirm_value"];
            if (confirmValue.Length > 3)
                confirmValue = confirmValue.Substring(confirmValue.Length - 3, 3);
            if (confirmValue == "Yes")
            {
                long status = 0;
                BlSaleReturn blSaleReturn = new BlSaleReturn();
                blSaleReturn.MasterId = Convert.ToInt64(gvSaleReturn.DataKeys[e.RowIndex]["ID_SalesReturn"].ToString());
                blSaleReturn.FK_Store = Convert.ToInt64(gvSaleReturn.DataKeys[e.RowIndex]["FK_Store"].ToString());
                blSaleReturn.UserCode = hfUser.Value;
                blSaleReturn.FK_Client = Convert.ToInt64(hfClient.Value);
                if (hfIsDelete.Value.ToLower() == "true")
                {
                    status = blSaleReturn.DeleteSalesReturn();
                    if (status > 0)
                    {
                        GridFill(1);

                        Populate.DeleteMsg(this);
                    }
                    else
                    {
                        Populate.ReferanceMsg(this);

                    }
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "toastr.warning('User does not have permission to Delete')", true);
                }
                blSaleReturn = null;
            }
        }
        catch (Exception ex)
        {
            Populate.WarningMsg(this, ex.ToString());
        }


    }
    protected void gvSaleReturn_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        try
        {
            BlSaleReturn blSaleReturn = new BlSaleReturn();
            ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "ShowAddDiv();", true);
            long MasterId = Convert.ToInt64(gvSaleReturn.DataKeys[e.NewSelectedIndex]["ID_SalesReturn"].ToString());
            blSaleReturn.MasterId = MasterId;
            hfSaleReturn.Value = MasterId.ToString();
            blSaleReturn.FK_Customer = 0;
            blSaleReturn.FK_Store = 0;
            blSaleReturn.FromDate = DateTime.Parse("1/1/1900");
            blSaleReturn.FK_Client = Convert.ToInt32(hfClient.Value.Split(',')[0]);
            blSaleReturn.ToDate = DateTime.MaxValue;
            Populate populate = new Populate();
            populate.FillDropdownList(ddlSearchCustomer, "Customer", "Name", "ID_Customer", "ID_Customer", "  Active='True'  and FK_Client = " + hfClient.Value, "");
            populate = null;

            blSaleReturn.SelectData();
            txtDate.Text = blSaleReturn.BillDate.ToString("dd/MM/yyyy");

            try
            {
                txtCustRefno.Text = blSaleReturn.FK_Customer == 0 ? "" : blSaleReturn.FK_Customer.ToString();
                txtCustomer.Text = blSaleReturn.CustomerName;
            }
            catch (Exception ex)
            {
                txtCustRefno.Text = "";
                txtCustomer.Text = "";
            }
            try
            {
                ddlStore.SelectedValue = blSaleReturn.FK_Store.ToString();
            }
            catch (Exception ex)
            {
                ddlStore.SelectedValue = "0";
            }
            try
            {
                txtSaleRefno.Text = blSaleReturn.Fk_Sale.ToString();
                txtSales.Text = blSaleReturn.SaleNo;

            }
            catch (Exception ex)
            {
                ClearSales();
            }

            txtRemark.Text = blSaleReturn.Remarks.ToString();
            cbxIsCredit.Checked = blSaleReturn.Credit;
            divAdd.Style.Add("display", "block");
            divView.Style.Add("display", "none;");
            CustomerEnabled(false);
            rdItemSale.Enabled = false;
            rdSales.Enabled = false;
            btnSave.Enabled = false;

            if (blSaleReturn.Fk_Sale != 0)
            {
                rdSales.Checked = true;
                rdItemSale.Checked = false;
                BindSaleItems(blSaleReturn.Fk_Sale);
                FillReasonddl(true);
            }
            else
            {
                blSaleReturn.FK_Client = Convert.ToInt32(hfClient.Value.Split(',')[0]);
                DataTable dtbl = blSaleReturn.SelectSaleReturnItems();
                ViewState[DirectSR] = dtbl;
                gvItems.DataSource = dtbl;
                gvItems.DataBind();
                rdItemSale.Checked = true;
                rdSales.Checked = false;
                FillReasonddl(false);
                TotalAmount(dtbl);
            }
            DisplayDiv();
            ControlEnabled(false);
        }

        catch (Exception ex)
        {
            Populate.WarningMsg(this, ex.ToString());
        }

    }
    protected void btnClear_Click1(object sender, EventArgs e)
    {

        ClearAll();
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        try
        {
            GridFill(1);
            ddlStore.SelectedValue = Session["mainstore"].ToString();
            ddlStore.Enabled = false;
            ddlSearchCustomer.Focus();
        }
        catch (Exception ex)
        {
            Populate.WarningMsg(this, ex.ToString());
        }
    }
    protected void btnAddItems_Click(object sender, EventArgs e)
    {
        try
        {
            int a = 0;
            if (hfItem.Value.Trim() == txtitem.Text.ToString().Trim())
            {
                a = AddDataItem();

                ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "CalculateItemWiseTotal(); ItemClear();", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "toastr.warning('Item  does not exist')", true);
            }
            txtitem.Focus();


            if (a == 0)
            {
                //ddlStore.Enabled = true;
                ddlBillType.Enabled = false;
                ddlCategory.Enabled = false;
                txtitem.Focus();
                ddlStore.Enabled = false;
                ddlCounter.Enabled = false;
            }
            else
            {
                btnClearItems.Focus();

            }
            btnClear.Focus();
        }
        catch (Exception ex)
        {
        }
    }
    protected void btnClearItems_Click(object sender, EventArgs e)
    {

    }
    protected void rdItemSale_CheckedChanged(object sender, EventArgs e)
    {
        ClearAll();
        chkReturnAll.Visible = false;
        try
        {
            DisplayDiv();
            if (ViewState[DirectSR] != null)
            {
                DataTable dtb = (DataTable)ViewState[DirectSR];
                gvItems.DataSource = dtb;
                gvItems.DataBind();
            }
            else
            {
                gvItems.DataSource = null;
                gvItems.DataBind();
            }
            ddlStore.Focus();
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "toastr.warning('" + ex.Message + "')", true);
        }
    }
    protected void rdSales_CheckedChanged(object sender, EventArgs e)
    {
        try
        {
            DisplayDiv();
            cbxIsCredit.Focus();
            if (rdSales.Checked == true)
            {
                chkReturnAll.Visible = true;
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "toastr.warning('" + ex.Message + "')", true);
        }
    }
    protected void ddlStore_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            hfStore.Value = ddlStore.SelectedValue;
            ddlStore.Focus();
            if (ddlStore.SelectedIndex > 0)
            {
                cbxIsCredit.Focus();
                Populate populate = new Populate();
                populate.FillDropdownList(ddlCounter, "Counter", "Name", "ID_Counter", "ID_Counter", "  Active='True'   AND  FK_Store=" + ddlStore.SelectedValue.ToString() + "   AND  FK_Client= " + hfClient.Value.Split(',')[0], "");
                populate = null;

            }
            ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "ItemClear();", true);


        }
        catch (Exception ex)
        {

        }

    }
    protected void gvItems_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        // blMessage.Items.Clear();
        try
        {
            string confirmValue = Request.Form["confirm_value"];
            if (confirmValue.Length > 3)
                confirmValue = confirmValue.Substring(confirmValue.Length - 3, 3);
            if (confirmValue == "Yes")
            {
                if (ViewState[DirectSR] != null)
                {
                    DataTable dtbl = (DataTable)ViewState[DirectSR];

                    if (dtbl.Columns["FK_Reason"] == null)
                        dtbl.Columns.Add("FK_Reason");
                    for (int i = 0; i < dtbl.Rows.Count; i++)
                    {
                        dtbl.Rows[i]["FK_Reason"] = ((DropDownList)gvItems.Rows[i].FindControl("ddlReason")).SelectedValue;

                    }
                    dtbl.Rows.RemoveAt(e.RowIndex);
                    //slno
                    for (int i = 0; i < dtbl.Rows.Count; i++)
                    {
                        dtbl.Rows[i]["slno"] = i + 1;

                    }
                    gvItems.DataSource = dtbl;
                    gvItems.DataBind();
                    TotalAmount(dtbl);
                    ViewState[DirectSR] = dtbl;
                    FillReasonddl(false);
                }
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "toastr.warning('" + ex.Message + "')", true);
        }

    }
    protected void gvSaleItems_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        //blMessage.Items.Clear();ReasonCode+' - '
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                if (hfSaleReturn.Value == "" || hfSaleReturn.Value == "0")
                {
                    TextBox txtBox = (TextBox)e.Row.FindControl("txtQuantity");
                    txtBox.Text = "0";


                }





                Populate populate = new Populate();
                populate.FillDropdownList(((DropDownList)e.Row.FindControl("ddlReason")), "Reason", "Reason", "ID_Reason", "ID_Reason", " FK_Client = " + hfClient.Value, "");
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "toastr.warning('" + ex.Message + "')", true);
        }
    }
    protected void imgbtnSales_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            txtSales.Text = "";

            ((TextBox)CommonPopup1.FindControl("txtName")).Text = txtSales.Text.Trim();
            ((TextBox)CommonPopup1.FindControl("txtRefno")).Text = txtSaleRefno.Text;
            ((HiddenField)CommonPopup1.FindControl("hfColumn")).Value = "Name";
            BillNoFill(1);
            divAdd.Style.Add("display", "block");
            divView.Style.Add("display", "none");
            txtCustRefno.Focus();

        }
        catch (Exception ex)
        {
        }
    }
    protected void imgbtnCustomer_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            txtCustomer.Text = "";
            ((TextBox)CommonPopup1.FindControl("txtName")).Text = txtCustomer.Text.Trim();
            ((TextBox)CommonPopup1.FindControl("txtRefno")).Text = txtCustRefno.Text.Trim();
            ((HiddenField)CommonPopup1.FindControl("hfColumn")).Value = "Name";
            CustomerFill(1);
            divAdd.Style.Add("display", "block");
            divView.Style.Add("display", "none");
            txtitem.Focus();

        }
        catch (Exception ex)
        {
        }
    }
    #endregion
    protected void btnPrint_Click(object sender, EventArgs e)
    {
        try
        {
            //  SRPrint(Convert.ToInt64(hfSaleReturn.Value == "" ? "0" : hfSaleReturn.Value));

            string SaleReturnId = hfSaleReturn.Value;
            string popupreport = "<script> window.open('SalesReturnPrint.aspx?SaleReturnId=" + SaleReturnId + "', 'self', 'left=200,top=50,width=900,height=680,status=0,resizable=0,fullscreen=0,scrollbars=1');</script>";
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "Success", popupreport, false);


            #region C#CodeOfDilshad

            //            DataTable dtsalesReturnPrintlayout = new DataTable();
            //            DataTable dtSrtrn = new DataTable();
            //            DataTable dtsalesreturn = new DataTable();
            //            DataTable dtsalesreturndetail = new DataTable();
            //            BlBarcodeGeneration blbarcodegeneration = new BlBarcodeGeneration();
            //            blbarcodegeneration.FK_Client = Convert.ToInt64(hfClient.Value);
            //            blbarcodegeneration.PriLayout = Convert.ToString("SalesReturn");
            //            dtsalesReturnPrintlayout = blbarcodegeneration.SelectPrintLayoutSalesReturn();
            //            BlSaleReturn blsrtn = new BlSaleReturn();
            //            blsrtn.MasterId = Convert.ToInt64(hfSaleReturn.Value);
            //            blsrtn.FK_Client = Convert.ToInt64(hfClient.Value);
            //            blsrtn.FromDate = DateTime.Parse("1/1/1753");
            //            blsrtn.ToDate = DateTime.MaxValue;
            //            dtSrtrn = blsrtn.SelectAllDataPrint();
            //            dtsalesreturndetail = blsrtn.SelectSaleReturnItemsPrint();
            //            int tmprow = 0;
            //            //StringBuilder result = new StringBuilder();

            //            HtmlGenericControl wrappar = new HtmlGenericControl("div");
            //            HtmlGenericControl bg = new HtmlGenericControl("div");
            //            HtmlGenericControl bill_bg;
            //            HtmlGenericControl det = new HtmlGenericControl("div");

            //            int inrow = 0;
            //            for (int i = 0; i < dtsalesReturnPrintlayout.Rows.Count; i++)                                               //Looping over print layout ->@1
            //            {
            //                if (inrow != Convert.ToInt32(dtsalesReturnPrintlayout.Rows[i]["priRow"].ToString()))
            //                {
            //                    inrow = Convert.ToInt32(dtsalesReturnPrintlayout.Rows[i]["priRow"].ToString());                            //Setting Up New Row Index
            //                    bg = new HtmlGenericControl("div");
            //                    bg.Attributes.Add("style", "width:100%; margin:0px 0px 0px 0px; padding:20px 0px 0px 0px;");
            //                }
            //                if (Convert.ToString(dtsalesReturnPrintlayout.Rows[i]["PriSubItem"].ToString()) == Convert.ToString("0"))             //Main Section printing?
            //                {
            //                    if (dtSrtrn.Columns.Count > 0)                                                                                  //Check salesr Return Columns are >0 ->2
            //                    {
            //                        int SalescolumnExist = 0;
            //                        string PriField = dtsalesReturnPrintlayout.Rows[i]["PriField"].ToString();                                //Set value of Print Layout row value
            //                        for (int j = 0; j < dtSrtrn.Columns.Count; j++)                                                         //iterate over Sales return Columns ->@3
            //                        {
            //                            if (Convert.ToString(PriField.ToString()) == Convert.ToString(dtSrtrn.Columns[j].ColumnName) && Convert.ToString(dtsalesReturnPrintlayout.Rows[i]["PriSubItem"].ToString()) == Convert.ToString("0")) //Condition for Value field in main section
            //                            {
            //                                bill_bg = new HtmlGenericControl("div");
            //                                bill_bg.InnerText = Convert.ToString(dtSrtrn.Rows[0][j].ToString().Substring(0, dtSrtrn.Rows[0][j].ToString().Length > Convert.ToInt32(dtsalesReturnPrintlayout.Rows[i]["PriWidth"].ToString()) ? Convert.ToInt32(dtsalesReturnPrintlayout.Rows[i]["PriWidth"].ToString()) : dtSrtrn.Rows[0][j].ToString().Length));//Set value 
            //                                bill_bg.Attributes.Add("style", "width:auto; margin:0px 0px 0px " + (Convert.ToInt32(dtsalesReturnPrintlayout.Rows[i]["PriCol"]) * 1).ToString() + "px ; padding:0px 0px 0px " + (Convert.ToInt32(dtsalesReturnPrintlayout.Rows[i]["PriConsider"]) * 1).ToString() + "px; float:left;");
            //                                bg.Controls.Add(bill_bg);
            //                                SalescolumnExist = 1;
            //                            }
            //                        }    //@3E
            //                        if (SalescolumnExist == 0)
            //                        {
            //                            bill_bg = new HtmlGenericControl("div");
            //                            bill_bg.InnerText = "" + Convert.ToString(dtsalesReturnPrintlayout.Rows[i]["PriField"].ToString().Substring(0, dtsalesReturnPrintlayout.Rows[i]["PriField"].ToString().Length > Convert.ToInt32(dtsalesReturnPrintlayout.Rows[i]["PriWidth"].ToString()) ? Convert.ToInt32(dtsalesReturnPrintlayout.Rows[i]["PriWidth"].ToString()) : dtsalesReturnPrintlayout.Rows[i]["PriField"].ToString().Length));
            //                            bill_bg.Attributes.Add("style", "width:auto; margin:0px 0px 0px " + (Convert.ToInt32(dtsalesReturnPrintlayout.Rows[i]["PriCol"]) * 1).ToString() + "px ; padding:0px 0px 0px " + (Convert.ToInt32(dtsalesReturnPrintlayout.Rows[i]["PriConsider"]) * 1).ToString() + "px; float:left;");
            //                            bg.Controls.Add(bill_bg);
            //                            // bill_bg = null;
            //                        }
            //                        wrappar.Controls.Add(bg);
            //                        // bg = null;

            //                        //dataDiv = null;
            //                    }
            //                    else
            //                    {
            //                        //   MessageBox.Show("No data in sales to print", "POS -Medlife", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //                    }
            //                }//<-2E

            //                else
            //                {
            //                    if (Convert.ToString(dtsalesReturnPrintlayout.Rows[i]["PriSubItem"].ToString()) == Convert.ToString("1"))
            //                    {
            //                        if (dtsalesreturndetail.Columns.Count > 0)
            //                        {

            //                            int SalesDetailscolumnExist = 0;
            //                            for (int k = 0; k < dtsalesreturndetail.Columns.Count; k++) //iteration over detail Columns
            //                            {
            //                                string PriFields = dtsalesReturnPrintlayout.Rows[i]["PriField"].ToString();
            //                                if (Convert.ToString(dtsalesReturnPrintlayout.Rows[i]["PriField"].ToString()) == Convert.ToString(dtsalesreturndetail.Columns[k].ColumnName))//Check for  value or data
            //                                {


            //                                    det = new HtmlGenericControl("div");

            //                                    det.Attributes.Add("style", "width:auto; margin:0px 0px 0px 0px; padding:0px 0px 0px 0px; float:left;");


            //                                    for (int l = 0; l < dtsalesreturndetail.Rows.Count; l++)
            //                                    {


            //                                        bill_bg = new HtmlGenericControl("div");
            //                                        bill_bg.InnerText = "" + Convert.ToString(dtsalesreturndetail.Rows[l][k].ToString().Substring(0, dtsalesreturndetail.Rows[l][k].ToString().Length > Convert.ToInt32(dtsalesReturnPrintlayout.Rows[i]["PriWidth"].ToString()) ? Convert.ToInt32(dtsalesReturnPrintlayout.Rows[i]["PriWidth"].ToString()) : dtsalesreturndetail.Rows[l][k].ToString().Length));
            //                                        bill_bg.Attributes.Add("style", "width:auto; margin:0px 0px 0px " + (Convert.ToInt32(dtsalesReturnPrintlayout.Rows[i]["PriCol"]) * 1).ToString() + "px ; padding:0px 0px 0px " + (Convert.ToInt32(dtsalesReturnPrintlayout.Rows[i]["PriConsider"]) * 1).ToString() + "px; float:left;");

            //                                        det.Controls.Add(bill_bg);
            //                                        HtmlGenericControl cleardiv = new HtmlGenericControl("div");
            //                                        cleardiv.Attributes.Add("style", "clear:both");
            //                                        det.Controls.Add(cleardiv);
            //                                        cleardiv.Dispose();
            //                                        SalesDetailscolumnExist = 1;
            //                                        //  bill_bg = null;

            //                                    }
            //                                    wrappar.Controls.Add(det);
            //                                    // det = null;
            //                                }
            //                            }
            //                            if (SalesDetailscolumnExist == 0) //Check  for Column Heading
            //                            {
            //                                //if (inrow != Convert.ToInt32(dtsalesReturnPrintlayout.Rows[i]["priRow"].ToString()))
            //                                //{
            //                                //    inrow = Convert.ToInt32(dtsalesReturnPrintlayout.Rows[i]["priRow"].ToString());                               
            //                                //    det = new HtmlGenericControl("div");

            //                                //}
            //                                det.Attributes.Add("style", "width:100%; margin:0px 0px 0px 0px; padding:0px 0px 0px 0px; float:left;");
            //                                bill_bg = new HtmlGenericControl("div");
            //                                bill_bg.InnerText = "" + Convert.ToString(dtsalesReturnPrintlayout.Rows[i]["PriField"].ToString().Substring(0, dtsalesReturnPrintlayout.Rows[i]["PriField"].ToString().Length > Convert.ToInt32(dtsalesReturnPrintlayout.Rows[i]["PriWidth"].ToString()) ? Convert.ToInt32(dtsalesReturnPrintlayout.Rows[i]["PriWidth"].ToString()) : dtsalesReturnPrintlayout.Rows[i]["PriField"].ToString().Length));
            //                                bill_bg.Attributes.Add("style", "width:auto; margin:0px 0px 0px " + (Convert.ToInt32(dtsalesReturnPrintlayout.Rows[i]["PriCol"]) * 1).ToString() + "px ; padding:0px 0px 0px " + (Convert.ToInt32(dtsalesReturnPrintlayout.Rows[i]["PriConsider"]) * 1).ToString() + "px; float:left;");
            //                                det.Controls.Add(bill_bg);
            //                                wrappar.Controls.Add(det);
            //                            }

            //                        }


            //                    }
            //                    if (Convert.ToString(dtsalesReturnPrintlayout.Rows[i]["PriSubItem"].ToString()) == Convert.ToString("2"))
            //                    {
            //                    }

            //                }

            //            }
            //            try
            //            {
            //                StringBuilder sb = new StringBuilder();
            //                System.IO.StringWriter sw = new System.IO.StringWriter(sb);
            //                HtmlTextWriter hw = new HtmlTextWriter(sw);
            //                wrappar.RenderControl(hw);
            //                String HTMLContent = sb.ToString();

            //                hdfPrintField.Value = "<html><body>" + HTMLContent + "</body></html>";
            //                det.Dispose();
            //                wrappar.Dispose();
            //                bill_bg = null;
            //                bg.Dispose(); 
            #endregion


            //   ScriptManager.RegisterStartupScript(Page, typeof(Page), "script", " PrintButtonClick();", true);

        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "toastr.warning('" + ex.Message + "')", true);
        }
    }
    protected void chkReturnAll_CheckedChanged(object sender, EventArgs e)
    {
        try
        {
            if (chkReturnAll.Checked == true)
            {


                for (int i = 0; i < gvSaleItems.Rows.Count; i++)
                {

                    // ((TextBox)gvSaleItems.Rows[i].FindControl("txtQuantity")).Text = ((Label)gvSaleItems.Rows[i].FindControl("lblSaleQty")).Text;
                    string a = ((TextBox)gvSaleItems.Rows[i].FindControl("txtQuantity")).Text;
                    string b = ((HiddenField)gvSaleItems.Rows[i].FindControl("hdfQty")).Value;
                    ((TextBox)gvSaleItems.Rows[i].FindControl("txtQuantity")).Text = b;
                    ((DropDownList)gvSaleItems.Rows[i].FindControl("ddlReason")).SelectedIndex = 1;
                }
                ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "CalculateTotalAmount()", true);
                ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "CalculateTotalAmount()", true);
            }
            else if (chkReturnAll.Checked == false)
            {

                for (int i = 0; i < gvSaleItems.Rows.Count; i++)
                {

                    // ((TextBox)gvSaleItems.Rows[i].FindControl("txtQuantity")).Text = ((Label)gvSaleItems.Rows[i].FindControl("lblSaleQty")).Text;
                    string a = ((TextBox)gvSaleItems.Rows[i].FindControl("txtQuantity")).Text;
                    string b = ((HiddenField)gvSaleItems.Rows[i].FindControl("hdfQty")).Value;
                    ((TextBox)gvSaleItems.Rows[i].FindControl("txtQuantity")).Text = 0.ToString();
                    ((DropDownList)gvSaleItems.Rows[i].FindControl("ddlReason")).SelectedIndex = 0;
                }
                ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "CalculateTotalAmount()", true);
                ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "CalculateTotalAmount()", true);
            }



        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "toastr.warning('" + ex.Message + "')", true);
        }
    }
    protected void ddlCounter_SelectedIndexChanged(object sender, EventArgs e)
    {
        //try
        //{
        //    if (ddlCounter.SelectedIndex > 0)
        //    {
        //        ddlCounter.Enabled = false;

        //        Populate populate = new Populate();
        //        populate.FillDropdownList(ddlStore, "Store S   INNER JOIN Counter C ON C.FK_Store=S.ID_Store", "S.Name", "S.ID_Store", "S.ID_Store", "   S.Active='True'   AND C.ID_Counter="+ddlCounter.SelectedValue+" AND   S.FK_Client = " + hfClient.Value, "");
        //        populate=null;
        //    }
        //}
        //catch (Exception ex)
        //{
        //    ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "toastr.warning('" + ex.Message + "')", true);
        //}

    }
}