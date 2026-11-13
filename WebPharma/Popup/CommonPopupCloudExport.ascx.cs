using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebPharma.Business.WebPharma;

public partial class Popup_CommonPopupCloudExport : System.Web.UI.UserControl
{
    private const string CalFun = "CallingFunction";
    decimal TotalAmnt;
    public static int processenableid = 0;
    public static int bSaveclick = 0;
    public string CallingFunction
    {
        get
        {
            if (ViewState[CalFun] == null)
            {

            }
            return ViewState[CalFun].ToString();
        }

        set
        {
            ViewState[CalFun] = value;
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        lblEXErrMsg.Text = string.Empty;
        lblagreexemsg.Text = string.Empty;
        lblsucessmessage.Text = string.Empty;
        lblrmsg.Text = string.Empty;
        chkAgree.Enabled = false;
        chkAgree.Checked = false;
        btnExProcess.BackColor = Color.CadetBlue;
        btnExProcess.Enabled = true;
        gdvSearch.Enabled = true;
        chkSupplierWise.Enabled = true;
        chkSalesMan.Enabled = true;
        //chkSalesMan.Checked = false;
        txtInvoiceDate.Enabled = true;

    }
    protected void Page_Changed(object sender, EventArgs e)
    {

        int pageIndex = int.Parse((sender as LinkButton).CommandArgument);
        this.Page.GetType().InvokeMember(CallingFunction, System.Reflection.BindingFlags.InvokeMethod, null, this.Page, new object[] { pageIndex });


    }
    protected void gdvSearch_PagePropertiesChanging(object sender, PagePropertiesChangingEventArgs e)
    {

    }
    protected void gdvSearch_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void gdvSearch_ItemDataBound(object sender, ListViewItemEventArgs e)
    {

    }
    protected void gdvSearch_DataBound(object sender, EventArgs e)
    {

    }
    protected void btnExProcess_Click(object sender, EventArgs e)
    {
     
        try
        {
            int rowCount = gdvSearch.Items.Count;
            if ((rowCount > 0) || (chkSupplierWise.Checked == true) || (chkRecovery.Checked == true) || (chkSalesMan.Checked == true))
            {
                processenableid = 0;
                Errormessage();
                System.Threading.Thread.Sleep(9000);
                processsection();
            }
            else
            {

            }


        }
        catch (Exception ex)
        {
            hdfagreeex.Value = "false";
            throw ex;
            //ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "toastr.warning('" + ex.Message + "')", true);
        }
    }
   
   
    protected void processsection()
    {
        try
        {
             DateTime DT = DateTime.Now;
            string DTDateTime = DT.ToString("dd/MM/yyyy");
            DateTime dd = Convert.ToDateTime(DTDateTime);
            string ASTR = (txtInvoiceDate.Text);

            DateTime DEXPORT = Convert.ToDateTime(ASTR);
            if (DEXPORT > dd)
            {
                lblEXErrMsg.Text = "Transaction Date Cannot be greater than Current Date";

            }
            else
            {
                BlCloudLink blcloudlinking = new BlCloudLink();

                ArrayList arlist = new ArrayList();

                IcloudExportDetails ICloudLink;
                foreach (ListViewDataItem lvRow in gdvSearch.Items)
                {
                    ICloudLink = new IcloudExportDetails();
                    CheckBox chBox = (CheckBox)lvRow.FindControl("chklistExview");
                    HiddenField ClID = (HiddenField)lvRow.FindControl("hdClID");
                    HiddenField hfClient = (HiddenField)lvRow.FindControl("hdFK_Client");
                    HiddenField Transmode = (HiddenField)lvRow.FindControl("hdnTransmode");
                    HiddenField hdntdate = (HiddenField)lvRow.FindControl("hdntdate");
                    HiddenField hdnparty = (HiddenField)lvRow.FindControl("hdnparty");
                    HiddenField hdnbillno = (HiddenField)lvRow.FindControl("hdnbillno");
                    HiddenField hdnAmount = (HiddenField)lvRow.FindControl("hdnAmount");
                    HiddenField hdnEXprocess = (HiddenField)lvRow.FindControl("hdfExProcessid");
                    HiddenField hdFK_Transaction = (HiddenField)lvRow.FindControl("hdFK_Transaction");
                    HiddenField hfUserCode = (HiddenField)lvRow.FindControl("hdUserCode");
                    if (chBox.Checked == true)
                    {
                        ICloudLink.Module = (ClID.Value);
                        ICloudLink.Transmode = Transmode.Value;
                        ICloudLink.Party = hdnparty.Value;
                        ICloudLink.BillNo = hdnbillno.Value;
                        ICloudLink.Amount = Convert.ToDecimal(hdnAmount.Value == "" ? "0" : hdnAmount.Value);
                        ICloudLink.FK_Transaction = Convert.ToInt64(hdFK_Transaction.Value == "" ? "0" : hdFK_Transaction.Value);
                        ICloudLink.Checked = chBox.Checked;
                        ICloudLink.Transdate = Convert.ToDateTime(hdntdate.Value);
                        hdfimpclient.Value = (hfClient.Value == "0" ? "0" : hfClient.Value);
                        //hdfusercode.Value = hfUserCode.Value == "" ? "" : hfUserCode.Value;
                        arlist.Add(ICloudLink);
                    }

                    ICloudLink = null;
                    //hdnexpprocess.Value= (hdnEXprocess.Value == "" ? "0" : hdnEXprocess.Value);
                }

                blcloudlinking.XmlExportCloudDetails = Common.WriteToXml(arlist);
                blcloudlinking.Mode = 2;
                blcloudlinking.UserCode = hdfusercode.Value;
                blcloudlinking.Tdate = Convert.ToDateTime(txtInvoiceDate.Text);
                blcloudlinking.SupplierWise = chkSupplierWise.Checked;
                blcloudlinking.SalesMan = chkSalesMan.Checked;
                blcloudlinking.Recovery = chkRecovery.Checked;
                DataSet ds = new DataSet();
                ds = blcloudlinking.SelectExportDataDetails();
                DataTable dt = new DataTable();
                DataTable dtsupplierwise = new DataTable();
                DataTable dtSalesMan = new DataTable();
                dt = ds.Tables[0];
                dtsupplierwise = ds.Tables[1];
                dtSalesMan = ds.Tables[2];
                if ((dt.Rows.Count > 0) || (dtsupplierwise.Rows.Count > 0) || (chkRecovery.Checked == true) || (dtSalesMan.Rows.Count > 0))
                {
                    lblsucessmessage.Text = "Process Completed.";

                    ViewState["ExportTempTable"] = dt;
                    btnExProcess.Enabled = false;
                    gdvSearch.Enabled = false;
                    chkSupplierWise.Enabled = false;
                    chkSalesMan.Enabled = false;
                    txtInvoiceDate.Enabled = false;
                    processenableid = 1;

                    ViewState["ExportSupplierTable"] = dtsupplierwise;
                    ViewState["ExportSalesManTable"] = dtSalesMan;

                }
                else if (((chkSupplierWise.Checked == true) && (dt.Rows.Count == 0)) || ((chkSalesMan.Checked == true) && (dt.Rows.Count == 0)))
                {
                    lblrmsg.Text = "No Data Found !.";
                    chkAgree.Enabled = false;
                    processenableid = 0;
                }
                else
                {
                    lblrmsg.Text = "Process Failed.";
                    processenableid = 0;
                    chkAgree.Enabled = false;
                }
            }
        }
        catch (Exception e)
        {
            hdfagreeex.Value = "false";
            throw e;
        }
    }
    protected void saveData()
    {


        BlCloudLink blcloudlinking = new BlCloudLink();
        DataTable dts = (DataTable)ViewState["ExportTempTable"];
        DataTable ds = (DataTable)ViewState["ExportSupplierTable"];
        DataTable dsm = (DataTable)ViewState["ExportSalesManTable"];

        if (CallingFunction == "ExportFill")
        {
            processenableid = 0;
            hdfagreeex.Value = "false";
            System.Threading.Thread.Sleep(9000);
            this.Page.GetType().InvokeMember("SetExportData", System.Reflection.BindingFlags.InvokeMethod, null, this.Page, new object[] { dts, ds, chkRecovery.Checked, dsm });
            hdfagreeex.Value = "false";
        }
    }
    protected void Errormessage()
    {
        int rowCount = gdvSearch.Items.Count;
        bool atLeastOneChecked = false;
        if (rowCount > 0)
        {
            foreach (ListViewDataItem item in gdvSearch.Items)
            {
                CheckBox chkItem = (CheckBox)item.FindControl("chklistExview");
                if (chkItem != null && chkItem.Checked)
                {
                    atLeastOneChecked = true;
                    lblEXErrMsg.Text = "";
                    lblagreexemsg.Text = "";
                    //lblsucessmessage.Text = "";
                    break;
                }
            }

            if (atLeastOneChecked)
            {
                btnExProcess.BackColor = Color.Blue;
                chkAgree.Enabled = true;
                hdfagreeex.Value = "False";

                // At least one checkbox is checked; you can proceed with the submission.
            }
            else if (chkAgree.Checked == true)
            {

                chkAgree.Enabled = true;
                //lblsucessmessage.Text = "";

            }
            else
            {
                //lblEXErrMsg.Text = "At least one checkbox must be checked.";
                chkAgree.Enabled = true;
            }
        }
        else if (chkSupplierWise.Checked == true || rowCount > 0 || chkRecovery.Checked == true || chkSalesMan.Checked == true)
        {
            chkAgree.Enabled = true;
        }
        else if (chkSupplierWise.Checked == false && rowCount == 0 && chkRecovery.Checked == false && chkSalesMan.Checked == false)
        {
            lblEXErrMsg.Text = "No Selected Item Found.";
            chkAgree.Enabled = false;
        }



    }
    protected void btnexSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            if (Session["SaveClick"].ToString() == "0")
            {
                Session["SaveClick"] = 1;
                Boolean chkag = false;
                chkag = hdfagreeex.Value == "" ? false : true;
                var k = chkAgree.Checked;
                if (hdfagreeex.Value == "true")
                {
                    lblErrorMsg.Text = "";
                    lblagreexemsg.Text = "";
                    lblsucessmessage.Text = "";
                    lblrmsg.Text = "";
                    saveData();
                    chkSalesMan.Checked = false;
                }
                else
                {
                    lblagreexemsg.Text = "Please click Process Button and  Tick I Agree Checkbox to Continue.";
                    if (processenableid == 1)
                    {
                        if (hdfagreeex.Value == "true")
                        {
                            chkAgree.Checked = true;
                            btnexSubmit.Enabled = true;
                            btnExProcess.Enabled = false;
                        }
                        else
                        {
                            chkAgree.Enabled = true;
                        }
                    }
                    else
                    {
                        chkAgree.Enabled = false;
                    }
                }
            }
            else
            {
                int i = 1;
                this.Page.GetType().InvokeMember("CloseExportData", System.Reflection.BindingFlags.InvokeMethod, null, this.Page, new object[] { i });
            }
        }
        catch (Exception ex)
        {
            
            throw ex;
        }
    }
}