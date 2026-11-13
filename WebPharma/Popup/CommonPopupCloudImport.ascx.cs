using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;
using WebPharma.Interface.WebPharma;
using WebPharma.Business.WebPharma;
using System.Collections;
using System.Drawing;
public partial class Popup_CommonPopupCloudImport : System.Web.UI.UserControl
{
    private const string CalFun = "CallingFunction";
    decimal TotalAmnt;
     public static int processenableimportid = 0;
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
    protected void Page_Changed(object sender, EventArgs e)
    {
        int pageIndex = int.Parse((sender as LinkButton).CommandArgument);
        this.Page.GetType().InvokeMember(CallingFunction, System.Reflection.BindingFlags.InvokeMethod, null, this.Page, new object[] { pageIndex });
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        lblImportErrMsg.Text = "";
        lblagreemsg.Text = "";
        lblImsucessmessage.Text = "";
        lblImrmsg.Text = "";
        chkAgree.Enabled = false;
        chkAgree.Checked = false;
        gdvSearch.Enabled = true;
        btnProcess.BackColor = Color.CadetBlue;
        btnProcess.Enabled = true;
       // disableselectall();
    }
    protected void btnSumbit_Click(object sender, EventArgs e)
    {
        if (Session["SaveClick"].ToString() == "0")
        {
            Session["SaveClick"] = 1;
            Boolean chkag = false;
            chkag = HiddenField1.Value == "" ? false : true;
            var k = chkAgree.Checked;
            if (HiddenField1.Value == "true")
            {
                lblImportErrMsg.Text = "";
                lblagreemsg.Text = "";
                lblImsucessmessage.Text = "";
                lblImrmsg.Text = "";
                saveData();
            }
            else
            {
                lblagreemsg.Text = "Please click Process Button and  Tick I Agree Checkbox to Continue.";
                if (processenableimportid == 1)
                {
                    if (HiddenField1.Value == "true")
                    {
                        chkAgree.Checked = true;
                        btnSumbit.Enabled = true;
                        btnProcess.Enabled = false;
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
            int k = 1;
            this.Page.GetType().InvokeMember("CloseImportData", System.Reflection.BindingFlags.InvokeMethod, null, this.Page, new object[] { k });

        }
    }
    protected void btnProcess_Click(object sender, EventArgs e)
    {
        try
        {
            System.Threading.Thread.Sleep(7000);
            processenableimportid = 0;
            Errormessage();
           // ProcessSection();
           
        }
        catch (Exception)
        {
            throw;
        }
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
    protected void ProcessSection()
    {
        try
        {
            ViewState["TempTable"] = null;
            DataTable dts = new DataTable();
            BlCloudLink blcloudlinking = new BlCloudLink();
            ArrayList arlist = new ArrayList(); ;
            ICloudImportDetails ICloudLink;
            // ICloudLink = null;
            dts = null;
            //arlist = null;
            foreach (ListViewDataItem lvRow in gdvSearch.Items)
            {
                ICloudLink = new ICloudImportDetails();
                // arlist= new ArrayList();
                CheckBox chBox = (CheckBox)lvRow.FindControl("chklistview");
                HiddenField ClID = (HiddenField)lvRow.FindControl("hdClID");
                HiddenField hfClient = (HiddenField)lvRow.FindControl("hdFK_Client");
                HiddenField hfTableName = (HiddenField)lvRow.FindControl("hdnTableName");
                HiddenField hfprocessid = (HiddenField)lvRow.FindControl("hdnProcessid");
                HiddenField hfUserCode = (HiddenField)lvRow.FindControl("hdnUserCode");
                HiddenField hdfNewMasters = (HiddenField)lvRow.FindControl("hdnNewMasters");

                if (chBox.Checked == true)
                {
                    ICloudLink.ID = (ClID.Value);
                    ICloudLink.TableName = hfTableName.Value;
                    ICloudLink.Processid = Convert.ToInt64(hfprocessid.Value == "" ? "0" : hfprocessid.Value);
                    if (hdfNewMasters.Value == "0")
                    {
                        ICloudLink.checkedTable = false;
                    }
                    else
                    {
                        ICloudLink.checkedTable = chBox.Checked;
                    }
                }
                arlist.Add(ICloudLink);

                hdfimpclient.Value = (hfClient.Value == "0" ? "0" : hfClient.Value);
                //hdfusercode.Value = (hfUserCode.Value);
                ICloudLink = null;
                hdnidprocess.Value = (hfprocessid.Value == "" ? "0" : hfprocessid.Value);
            }

            //   Errormessage();
            blcloudlinking.Mode = 2;
            blcloudlinking.FK_Client = Convert.ToInt64(hdfimpclient.Value == "0" ? "0" : hdfimpclient.Value);
            blcloudlinking.UserCode = hdfusercode.Value;
            blcloudlinking.XmlExportCloudDetails = Common.WriteToXml(arlist);
            dts = blcloudlinking.SelectMasterCountDetails();
            if (dts.Rows.Count > 0)
            {
                lblImsucessmessage.Text = "Process Completed.";

                ViewState["TempTable"] = dts;
                btnProcess.Enabled = false;
                gdvSearch.Enabled = false;
                processenableimportid = 1;
            }
            else
            {
                lblImrmsg.Text = "Process Failed !.";
                processenableimportid = 0;
            }
        }
        catch (Exception ex)
        {

            HiddenField1.Value = "false";
            throw ex;
        }




    }
    protected void Errormessage()
    {
        bool atLeastOneChecked = false;

        foreach (ListViewDataItem item in gdvSearch.Items)
        {
            CheckBox chkItem = (CheckBox)item.FindControl("chklistview");
            if (chkItem != null && chkItem.Checked)
            {
                atLeastOneChecked = true;
                lblImportErrMsg.Text = "";
                lblagreemsg.Text = "";
                lblImsucessmessage.Text = "";
                break;
            }
        }

        if (atLeastOneChecked)
        {
            btnProcess.BackColor = Color.Blue;
            chkAgree.Enabled = true;
            ProcessSection();         
        }
        else if (chkAgree.Checked == true)
        {

            chkAgree.Enabled = true;
          
        }
        else
        {
            lblImportErrMsg.Text = "At least one checkbox must be checked.";
            chkAgree.Enabled = false;

        }
    }
    protected void saveData()
    {
       
        BlCloudLink blcloudlinking = new BlCloudLink();
        DataTable dts = new DataTable();

        dts = (DataTable)ViewState["TempTable"];
        if (dts.Rows.Count > 0)
        {

            lblImportErrMsg.Text = "";
            if (CallingFunction == "ImportFill")
            {
                   processenableimportid = 0;
                   System.Threading.Thread.Sleep(7000);
                this.Page.GetType().InvokeMember("SetCount", System.Reflection.BindingFlags.InvokeMethod, null, this.Page, new object[] { dts, hdnidprocess.Value });
                HiddenField1.Value = "false";
            }
        }
        else
        {
            lblImportErrMsg.Text = "Please click Process Button or check I Agree Checkbox.";
        }

    }
    protected void disableselectall()
    {
        foreach (ListViewItem item in gdvSearch.Items)
        {

            CheckBox chkSelectInListView = (CheckBox)item.FindControl("chkImpSelect");
            if (chkSelectInListView != null && chkSelectInListView.Checked)
            {
                chkSelectInListView.Checked = false;
            }
        }
    }
}