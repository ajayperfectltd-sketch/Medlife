/*----------------------------------------------------------------------------------------------------------------------------------------------------------
Purpose    : Create Account Reports
Author     :Bibin TC
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
using WebPharma.Business;
using System.Data;
using WebPharma.Business.WebWebPharma;
public partial class AccountsReport : System.Web.UI.Page
{
    #region Functions
    protected void BindData()
    {
        Populate populate = new Populate();
        populate.FillTransTypeAll(ddltransType);

        populate.FillDropdownList(ddlAccountHead, "AccountHead AH ", "AH.HeadName", "AH.ID_AccountHead", "HeadName", "Cancelled=0  AND AH.FK_Client=" + hfClient.Value, "");
        //populate.FillDropdownList(ddlAccountHead, "AccountHead", "Name+'-'+Shortname", "ID_AccountHead", "Name", " Active=1 AND FK_Client="+hfClient.Value, "");
        populate.FillDropdownList(ddlAccountSubHead, "AccountHeadSub", "HeadSubName", "ID_AccountHeadSub", "HeadSubName", " Cancelled=0 AND FK_Client=" + hfClient.Value, "");
        populate.FillDropdownListAll(ddlCounter, "Counters", "CounterName", "ID_Counters", "CounterName", "Active='True' and Cancelled= 0 AND FK_Client=" + hfClient.Value, "");
        populate.FillDropdownListAll(ddlShift, "ShiftMaster", "ShortName", "ID_ShiftMaster", "ShiftName", "Cancelled=0 AND Active='True' AND FK_Client=" + hfClient.Value, "");
        populate.FillDropdownListAll(ddlAccountGroup, "AccountGroup", "GroupName", "ID_AccountGroup", "GroupName", " Cancelled=0 And FK_Client=" + hfClient.Value, "");
        populate.FillDropdownListAll(ddlAccountSubGroup, "AccountSubGroup", "SubGroupName", "ID_AccountSubGroup", "SubGroupName", " Cancelled=0 And FK_Client=" + hfClient.Value, "");
        populate.FillDropdownListAll(ddlStore, "Store", "StoName", "ID_Store", "Shortname", "Active='True' AND FK_Client=" + hfClient.Value, "");
        populate.FillDropdownListSelect(ddlstoreCashbook, "Store", "StoName", "ID_Store", "StoName", "Active='True' AND FK_Client=" + hfClient.Value, "");
        populate = null;


    }
    public void SetReadonly(params TextBox[] textbox)
    {
        foreach (TextBox txt in textbox)
            txt.Attributes.Add("readonly", "readonly");

    }
    public void HideDiv()
    {
        divTransType.Visible = false;
        divDate.Visible = false;
        divStore.Visible = false;
        divAsOnDate.Visible = false;
        divItems.Visible = false;
        divButtons.Visible = false;
        divStoreFromTo.Visible = false;
        divIssueMode.Visible = false;
        //divcatgry.Visible = false;
        divAccountHead.Visible = false;
        divAccountSubHead.Visible = false;
        divCounter.Visible = false;
        divShift.Visible = false;
        divAccountGroup.Visible = false;
        divAccountsubGroup.Visible = false;
        DivReportType.Visible = false;
        divStoreCash.Visible = false;
    }
    private bool ValidateData(string Value)
    {
        //string reportfile = "";

        if ((Value == "ACCLER") || (Value == "ACCTBG") || (Value == "ACCOS") || (Value == "ACCSUB"))
        {
            return (ValidateFromToDate());
        }

        return true;
    }
    private Boolean ValidateFromToDate()
    {
       
        return true;
    }
    protected void BindReportMenu()
    {
        try
        {
            BlReportAccess blReportAccess = new BlReportAccess();
            blReportAccess.FK_Client = Convert.ToInt64(hfClient.Value);
            blReportAccess.FK_User = Convert.ToInt64(hfUser.Value);
            blReportAccess.Type = 2;
            DataTable dtbl = new DataTable();
            dtbl = blReportAccess.SelectReportMenu();
            int nodes = 0;
            for (int i = 0; i < dtbl.Rows.Count; i++)
            {
                bool node = false;
                string CatName = dtbl.Rows[i]["CatName"].ToString();
                string CatCode = dtbl.Rows[i]["CatCode"].ToString();
                string SubCatName = dtbl.Rows[i]["SubCatName"].ToString();
                string SubCatCode = dtbl.Rows[i]["SubCatCode"].ToString();
                nodes = tvSettings.Nodes.Count;
                for (int j = 0; j < nodes; j++)
                {
                    if (tvSettings.Nodes[j].Text == CatName)
                    {
                        int cnCount = tvSettings.Nodes[j].ChildNodes.Count;
                        node = true;
                        tvSettings.Nodes[j].ChildNodes.Add(new TreeNode(SubCatName));
                        tvSettings.Nodes[j].ChildNodes[cnCount].Value = SubCatCode;
                    }
                }
                if (node == false)
                {
                    tvSettings.Nodes.Add(new TreeNode(CatName));
                    tvSettings.Nodes[nodes].Value = CatCode;
                    tvSettings.Nodes[nodes].SelectAction = TreeNodeSelectAction.Expand;
                    tvSettings.Nodes[nodes].ChildNodes.Add(new TreeNode(SubCatName));
                    tvSettings.Nodes[nodes].ChildNodes[0].Value = SubCatCode;
                }
            }

        }
        catch (Exception ex)
        {

        }

    }
    private string InitializeReport(BlReportData blReportData, String Value)
    {
        string reportfile = "";
        string ID = string.Empty;
        ID = hfINVSubModule.Value.ToString();

        switch (ID)
        {
            case "ACC":
                switch (Value)
                {
                 
                    case "ACCLER":
                        hfReportType.Value = "CRPT";
                        blReportData.ReportFile = "AccountLedger.rpt";
                        blReportData.ReportTable = "AccountLedger";
                        blReportData.ReportSchema = "AccountLedger";

                        break;
                    case "ACCOS":
                        hfReportType.Value = "CRPT";
                        //blReportData.ReportFile = "Outstanding.rdlc";
                        blReportData.ReportFile = "AccountOutstanding.rpt";
                        blReportData.ReportTable = "AccountOutstanding";
                        blReportData.ReportSchema = "AccountOutstanding";

                        break;
                    case "ACCSUB":
                        hfReportType.Value = "CRPT";
                        blReportData.ReportFile = "AccountSubsidary.rpt";
                        blReportData.ReportTable = "AccountSubsidary";
                        blReportData.ReportSchema = "AccountSubsidary";
                        break;

                    case "ACCREP":
                        hfReportType.Value = "CRPT";
                        blReportData.ReportFile = "Repayment.rpt";
                        blReportData.ReportTable = "Repayment";
                        blReportData.ReportSchema = "Repayment";
                        break;

                    case "ACCDAY":
                        hfReportType.Value = "CRPT";
                        blReportData.ReportFile = "DayBook.rpt";
                        blReportData.ReportTable = "DayBook";
                        blReportData.ReportSchema = "DayBook";
                        break;
                    case "ACCGL":
                        hfReportType.Value = "CRPT";
                        blReportData.ReportFile = "AccountGeneralLedger.rpt";
                        blReportData.ReportTable = "AccountGeneralLedger";
                        blReportData.ReportSchema = "AccountGeneralLedger";
                        break;
                    case "ACCCB":
                        hfReportType.Value = "CRPT";
                        blReportData.ReportFile = "AccountCashBook.rpt";
                        blReportData.ReportTable = "AccountCashBook";
                        blReportData.ReportSchema = "AccountCashBook";
                        break;
                    case "ACCRND":
                        hfReportType.Value = "CRPT";
                        blReportData.ReportFile = "AccountRandD.rpt";
                        blReportData.ReportTable = "AccountRandD";
                        blReportData.ReportSchema = "AccountRandD";
                        break;
                        
                 }
                break;
            case "ACTB":
                 switch (Value)
                {
                    case "ACCTBG":
                        hfReportType.Value = "CRPT";
                        blReportData.ReportFile = "AccountTrialBalance.rpt";
                        blReportData.ReportTable = "AccountTrialBalance";
                        blReportData.ReportSchema = "AccountTrialBalance";

                        break;
                }
                break;

            
        }
        return reportfile;
    }
    protected bool UpdateData(BlReportData BlReportData)
    {
        try
        {
            BlReportData.ID = hfINVSubModule.Value.ToString();
            BlReportData.SubID = tvSettings.SelectedValue.ToString();
            BlReportData.FromDate = Convert.ToDateTime(txtFromDate.Text);
            BlReportData.ToDate = Convert.ToDateTime(txtToDate.Text);
            BlReportData.AsonDate = Convert.ToDateTime(txtAsOnDate.Text);
            //BlReportData.FK_CategoryFirst = Convert.ToInt64(ddlCategoryFirst.SelectedValue);
            //BlReportData.FK_Items = Convert.ToInt64(ddlItems.SelectedValue);
            BlReportData.FK_Store = Convert.ToInt64(ddlStore.SelectedValue);
            BlReportData.FK_Client = Convert.ToInt64(hfClient.Value);
            BlReportData.UserCode = hfUser.Value;
            BlReportData.ReportSchemaPath = Server.MapPath("Schema/");
            BlReportData.ReportPath = Server.MapPath("Reports/");
            //BlReportData.ReportPath = Server.MapPath("SSRS/");
            BlReportData.DatasetName = "INVENTORYREPORTS";
            //BlReportData.Title = "NIT Co-opertative Employees Multipurpose Store";
         //BlReportData.Title = "Cochin Shipyard Employees Consumer Co-operative Society Ltd (No. E-750)";
            string s = BlReportData.Title;
            BlReportData.Title = s;

            if (BlReportData.SubID == "ACCLER")
            {
                BlReportData.FK_AccountHead = Convert.ToInt64(ddlAccountHead.SelectedValue);
                if (ddlAccountSubHead.Items.Count > 0)
                {
                    BlReportData.Critrea2 = ddltransType.SelectedValue.ToString();
                    BlReportData.FK_AccountSubHead = Convert.ToInt64(ddlAccountSubHead.SelectedValue);
                }
                else
                {
                    BlReportData.FK_AccountSubHead = 0;
                }

                BlReportData.PageTitle = "Account Ledger Form : " + BlReportData.FromDate.ToShortDateString() + " To : " + BlReportData.ToDate.ToShortDateString();
            }
           
            if (BlReportData.SubID == "ACCTBG")
            {
                BlReportData.FK_AccountGroup = Convert.ToInt64(ddlAccountGroup.SelectedValue);
                BlReportData.FK_AccountSubGroup = Convert.ToInt64(ddlAccountSubGroup.SelectedValue);
                BlReportData.AsonDate = Convert.ToDateTime(txtAsOnDate.Text);
                BlReportData.PageTitle = "Account Trial Balance Report AsOnDate " + BlReportData.AsonDate.ToShortDateString();

            }
            if (BlReportData.SubID == "ACCOS")
            {
                BlReportData.FK_AccountHead = Convert.ToInt64(ddlAccountHead.SelectedValue);
                if (ddlAccountSubHead.Items.Count > 0)
                {
                    BlReportData.Critrea2 = ddltransType.SelectedValue.ToString();
                    BlReportData.FK_AccountSubHead = Convert.ToInt64(ddlAccountSubHead.SelectedValue);
                }
                else
                {
                    BlReportData.FK_AccountSubHead = 0;
                }

                BlReportData.PageTitle = "Account Outstanding Report As On : " + BlReportData.AsonDate.ToShortDateString();
            }
                if (BlReportData.SubID == "ACCSUB")
                {
                    BlReportData.FK_AccountHead = Convert.ToInt64(ddlAccountHead.SelectedValue);
                    if (ddlAccountSubHead.Items.Count > 0)
                    {
                        BlReportData.Critrea2 = ddltransType.SelectedValue.ToString();
                        BlReportData.FK_AccountSubHead = Convert.ToInt64(ddlAccountSubHead.SelectedValue);
                    }
                    else
                    {
                        BlReportData.FK_AccountSubHead = 0;
                    }

                    BlReportData.PageTitle = "Account Subsidiary Report Form : " + BlReportData.FromDate.ToShortDateString() + " To : " + BlReportData.ToDate.ToShortDateString();
                }


                if (BlReportData.SubID == "ACCREP")
                {
                 
                    BlReportData.PageTitle = "Repayment Report Form : " + BlReportData.FromDate.ToShortDateString() + " To : " + BlReportData.ToDate.ToShortDateString();
                }
                if (BlReportData.SubID == "ACCDAY")
                {
                    BlReportData.PageTitle = "Day Book As On : " + BlReportData.AsonDate.ToShortDateString();
                    BlReportData.FK_Store = Convert.ToInt64(ddlstoreCashbook.SelectedValue);
                }
                if (BlReportData.SubID == "ACCGL")
                {
                    BlReportData.FK_AccountHead = Convert.ToInt64(ddlAccountHead.SelectedValue);
                    if (ddlAccountSubHead.Items.Count > 0)
                    {
                        BlReportData.Critrea2 = ddltransType.SelectedValue.ToString();
                        BlReportData.FK_AccountSubHead = Convert.ToInt64(ddlAccountSubHead.SelectedValue);
                    }
                    else
                    {
                        BlReportData.FK_AccountSubHead = 0;
                    }
                    BlReportData.PageTitle = "General Ledger : " + BlReportData.FromDate.ToShortDateString() + " To : " + BlReportData.ToDate.ToShortDateString();
                }
                if (BlReportData.SubID == "ACCCB")
                {

                    BlReportData.PageTitle = "Cash Book As On : " + BlReportData.AsonDate.ToShortDateString();
                    BlReportData.FK_Store = Convert.ToInt64(ddlstoreCashbook.SelectedValue);
                }
                if (BlReportData.SubID == "ACCRND")
                {

                    BlReportData.PageTitle = "Receipt And Disbersement : " + BlReportData.FromDate.ToShortDateString() + " To : " + BlReportData.ToDate.ToShortDateString();
                }


            return true;
        }
        catch (Exception ex)
        {
            return false;
        }
    }
    private bool SetReportData(BlReportData BlReportData)
    {
        InitializeReport(BlReportData, tvSettings.SelectedValue.ToString());
        if (UpdateData(BlReportData))
        {
            Session["ReportData"] = BlReportData;
            return true;
        }
        else
        {
            return false;
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
                SetReadonly(txtFromDate, txtToDate);
                txtFromDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
                txtToDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
                txtAsOnDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
                HideDiv();
                
               hfUser.Value = Request.QueryString["uid"];
               hfClient.Value = Request.QueryString["clnt"];
               BindData();
               BindReportMenu();

               lblHeadText.Text = "";
            }
        }

        catch (Exception ex)
        {
           blMessage.Items.Add(ex.Message);
        }
    }
    protected void btnSubHeadBlurTrigger_Click(object sender, EventArgs e)
    {

        try
        {
           
                    ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "ShowAddDiv();", true);
                    if (ddlAccountHead.SelectedIndex > 0)
                    {
                        Populate populate = new Populate();
                        populate.FillDropdownList(ddlAccountSubHead, "AccountHeadSub", "HeadSubName", "ID_AccountHeadSub", "HeadSubName", "FK_Client=" + hfClient.Value + " AND Cancelled=0 AND FK_AccountHead=" + ddlAccountHead.SelectedValue, "");
                        populate = null;
                       
                        //if (ddlAccountSubHead.Items.Count > 0)
                        //{
                        //    rfvAccountSubHead.Enabled = true;
                        //    rfvAccountSubHead.ErrorMessage = "Account Sub Name required";
                        //}

                    }
                   
                }
                catch (Exception ex)
                {
                  
                    ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "toastr.warning('" + ex.Message + "')", true);
                }

       

    }
    protected void btnGroupBlurTrigger_Click(object sender, EventArgs e)
    {

        try
        {
            if (ddlAccountGroup.SelectedIndex > 0)
            {
                Populate populate = new Populate();               
                populate.FillDropdownList(ddlAccountSubGroup, "AccountSubGroup", "Name", "ID_AccountSubGroup", "Name", "Active=1 And FK_Client=" + hfClient.Value +" AND FK_AccountGroup="+ddlAccountGroup.SelectedValue, "");
                populate = null;
            }
                       
        }
        catch (Exception ex)
        {

            ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "toastr.warning('" + ex.Message + "')", true);
        }
    }
    
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            BlReportData BlReportData = new BlReportData();

            if (ValidateData(tvSettings.SelectedValue.ToString()))
            {
                if (SetReportData(BlReportData))
                {
                    hfUrl.Value = Request.QueryString["uid"];
                    Session["date"] = txtPassDate.Text;
                    hfdate.Value = txtPassDate.Text;
                    ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "popUp();", true);

                }
            }
        }
        catch (Exception ex)
        {
            blMessage.Items.Add(ex.Message);
        }
    }

    protected void btnClear_Click(object sender, EventArgs e)
    {
        ddlCounter.SelectedIndex = 0;
        ddlAccountGroup.SelectedIndex = 0;
        ddlAccountHead.SelectedIndex = 0;
        ddlReportType.SelectedIndex = 0;
        Populate populate = new Populate();
        //populate.FillDropdownListAll(ddlShift, "Shift", "ShortName", "ID_Shift", "Name", " Active='True' AND FK_Client=" + hfClient.Value, "");       
        populate.FillDropdownListAll(ddlAccountSubGroup, "AccountSubGroup", "SubGroupName", "ID_AccountSubGroup", "SubGroupName", "cancelled=0 And FK_Client=" + hfClient.Value, "");
        populate.FillDropdownList(ddlAccountSubHead, "AccountHeadSub", "HeadSubName", "ID_AccountHeadSub", "HeadSubName", " cancelled=0 AND FK_Client=" + hfClient.Value, "");
        populate = null;

        ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "ShowDate();", true);

        if (tvSettings.SelectedValue == "ACCIE")
        {
            ddltransType.SelectedIndex = 0;
        }
        if (tvSettings.SelectedValue == "ACCLER")
        {
            ddlAccountHead.SelectedIndex = 0;
            ddlAccountSubHead.SelectedIndex = 0;
        }
        if (tvSettings.SelectedValue == "SHCL")
        {
            ddlCounter.SelectedIndex = 0;
            ddlShift.SelectedIndex = 0;
        }
        if (tvSettings.SelectedValue == "ACCTBG")
        {
            ddlAccountGroup.SelectedIndex = 0;
            ddlAccountSubGroup.SelectedIndex = 0;
            ddlStore.SelectedIndex = 0;
        }
        if (tvSettings.SelectedValue == "ACCDAY")
        {
           ddlStore.SelectedIndex=0;
        }
        if (tvSettings.SelectedValue == "ACCGL")
        {
            ddlStore.SelectedIndex = 0;
        }
        if (tvSettings.SelectedValue == "ACCCB")
        {
            ddlstoreCashbook.SelectedIndex = 0;
        }
        if (tvSettings.SelectedValue == "ACCRND")
        {
            ddlStore.SelectedIndex = 0;
           
        }

        if (tvSettings.SelectedValue == "ACCSUB")
        {
            ddlAccountGroup.SelectedIndex = 0;
            ddlAccountSubGroup.SelectedIndex = 0;
            ddltransType.SelectedIndex = 0;
        }
    }
    protected void btnShiftBlurTrigger_Click(object sender, EventArgs e)
    {
        try
        {
            //clear();

            if (ddlCounter.SelectedIndex > 0)
            {
                Populate populate = new Populate();
                populate.FillDropdownList(ddlShift, "Shift", "ShortName", "ID_Shift", "Name", "FK_Client="+hfClient.Value+" AND Active='True' AND FK_Couter=" + ddlCounter.SelectedValue, "");
                populate = null;

            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "toastr.warning('" + ex.Message + "')", true);
        }
    }
    protected void tvSettings_SelectedNodeChanged(object sender, EventArgs e)
    {
        try
        {
            HideDiv();
            lblHeadText.Text = "";
            BlWebPharmaReport blWebPharmaReport = new BlWebPharmaReport();
            ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "ShowDate();", true);
            //if (tvSettings.SelectedValue == "ACCIE")
            //{
            //    blWebPharmaReport.SubModule = "ACCIE";
            //    divTransType.Visible = true;
            //    divDate.Visible = true;
            //    divButtons.Visible = true;
            //    divAccountHead.Visible = false;
            //    divAccountSubHead.Visible =false;
            //    divCounter.Visible = false;
            //    divShift.Visible = false;
            //    divAccountGroup.Visible = false;
            //    divAccountsubGroup.Visible = false;
            //    hfINVSubModule.Value = "ACC";
            //    divAsOnDate.Visible = false;
            //    divAccountGroup.Visible = false;
            //    divAccountsubGroup.Visible = false;
            //    lblHeadText.Text = "Income/Expense";

            //}
            rfvAccountHead.Enabled = true;
            ddlAccountHead.Attributes.Remove("class");
            ddlAccountHead.Attributes.Add("class", "dropdownMandatory");
            if (tvSettings.SelectedValue == "ACCOS")
            {
                blWebPharmaReport.SubModule = "ACCOS";
                divDate.Visible = true;
                divAccountHead.Visible = true;
                divAccountSubHead.Visible = false;
                divTransType.Visible = false;
                divButtons.Visible = true;
                divCounter.Visible = false;
                divShift.Visible = false;
                divAccountGroup.Visible = false;
                divAccountsubGroup.Visible = false;
                divAsOnDate.Visible = false;
                divAccountGroup.Visible = false;
                divAccountsubGroup.Visible = false;
                DivReportType.Visible = true;
                hfINVSubModule.Value = "ACC";
                lblHeadText.Text = "Account Outstanding";

                rfvAccountHead.Enabled = false;
                ddlAccountHead.Attributes.Remove("class");
                ddlAccountHead.Attributes.Add("class", "dropdown");
              
            }
            if (tvSettings.SelectedValue == "ACCSUB")
            {
                blWebPharmaReport.SubModule = "ACCSUB";
                divDate.Visible = true;
                divAccountHead.Visible = true;
                divAccountSubHead.Visible = true;
                divTransType.Visible = true;
                divButtons.Visible = true;
                divCounter.Visible = false;
                divShift.Visible = false;
                divAccountGroup.Visible = false;
                divAccountsubGroup.Visible = false;
                divAsOnDate.Visible = false;
                divAccountGroup.Visible = false;
                divAccountsubGroup.Visible = false;
                DivReportType.Visible = true;
                hfINVSubModule.Value = "ACC";
                lblHeadText.Text = "Account Subsidiary";

                rfvAccountHead.Enabled = false;
                ddlAccountHead.Attributes.Remove("class");
                ddlAccountHead.Attributes.Add("class", "dropdown");
              
            }
            if (tvSettings.SelectedValue == "ACCLER")
            {
                blWebPharmaReport.SubModule = "ACCLER";
                divDate.Visible = true;
                divAccountHead.Visible = true;
                divAccountSubHead.Visible = true;
                divTransType.Visible = false;
                divButtons.Visible = true;
                divCounter.Visible = false;
                divShift.Visible = false;
                divAccountGroup.Visible = false;
                divAccountsubGroup.Visible = false;
                divAsOnDate.Visible = false;
                divAccountGroup.Visible = false;
                divAccountsubGroup.Visible = false;
                DivReportType.Visible = true;
                hfINVSubModule.Value = "ACC";
                lblHeadText.Text = "Account Ledger";

               

            }
            if (tvSettings.SelectedValue == "ACCTBG")
            {
                blWebPharmaReport.SubModule = "ACCTBG";
                divDate.Visible = false;
                divAccountHead.Visible =false;
                divAccountSubHead.Visible =false;
                divTransType.Visible = false;
                divAsOnDate.Visible = true;
                divAccountGroup.Visible = true;
                divAccountsubGroup.Visible = true;
                divButtons.Visible = true;
                divCounter.Visible = false;
                divShift.Visible = false;
                DivReportType.Visible = true;
                divStore.Visible = true;
                divStoreCash.Visible = false;
                hfINVSubModule.Value = "ACTB";
                lblHeadText.Text = "Account Trial Balance";



            }

            if (tvSettings.SelectedValue == "ACCREP")
            {
                blWebPharmaReport.SubModule = "ACCREP";
                divDate.Visible = true;
                divAccountHead.Visible =false;
                divAccountSubHead.Visible =false;
                divTransType.Visible = false;
                divAsOnDate.Visible = false;
                divAccountGroup.Visible = false;
                divAccountsubGroup.Visible = false;
                divButtons.Visible = true;
                divCounter.Visible = false;
                divShift.Visible = false;
                DivReportType.Visible = true;
                hfINVSubModule.Value = "ACC";
                lblHeadText.Text = "Repayment";
            }
            if (tvSettings.SelectedValue == "ACCDAY")
            {
                blWebPharmaReport.SubModule = "ACCDAY";
                divDate.Visible = false;
                divAccountHead.Visible = false;
                divAccountSubHead.Visible = false;
                divTransType.Visible = false;
                divButtons.Visible = true;
                divCounter.Visible = false;
                divShift.Visible = false;
                divAccountGroup.Visible = false;
                divAccountsubGroup.Visible = false;
                divAsOnDate.Visible = true;
                divAccountGroup.Visible = false;
                divAccountsubGroup.Visible = false;
                DivReportType.Visible = true;
                divStore.Visible = false;
                divStoreCash.Visible = true;
                hfINVSubModule.Value = "ACC";
                lblHeadText.Text = "Account Day Book";
            }
            if (tvSettings.SelectedValue == "ACCCB")
            {
                blWebPharmaReport.SubModule = "ACCCB";
                divDate.Visible = false;
                divAccountHead.Visible = false;
                divAccountSubHead.Visible = false;
                divStoreCash.Visible = false;
                divTransType.Visible = false;
                divButtons.Visible = true;
                divCounter.Visible = false;
                divShift.Visible = false;
                divAccountGroup.Visible = false;
                divAccountsubGroup.Visible = false;
                divAsOnDate.Visible = true;
                divAccountGroup.Visible = false;
                divAccountsubGroup.Visible = false;
                DivReportType.Visible = true;
                divStore.Visible = false;
                divStoreCash.Visible = true;

                // ddlStore.SelectedValue
                hfINVSubModule.Value = "ACC";
                lblHeadText.Text = "Account Cash Book";
            }
            if (tvSettings.SelectedValue == "ACCRND")
            {
                blWebPharmaReport.SubModule = "ACCRND";
                divDate.Visible = true;
                divAccountHead.Visible = false;
                divAccountSubHead.Visible = false;
                divStoreCash.Visible = false;
                divTransType.Visible = false;
                divButtons.Visible = true;
                divCounter.Visible = false;
                divShift.Visible = false;
                divAccountGroup.Visible = false;
                divAccountsubGroup.Visible = false;
                divAsOnDate.Visible = false;
                divAccountGroup.Visible = false;
                divAccountsubGroup.Visible = false;
                DivReportType.Visible = true;
                divStore.Visible = true;
                divStoreCash.Visible = false;

                // ddlStore.SelectedValue
                hfINVSubModule.Value = "ACC";
                lblHeadText.Text = "Reciept And Disbersement";
            }

            if (tvSettings.SelectedValue == "ACCGL")
            {
                blWebPharmaReport.SubModule = "ACCGL";
                divDate.Visible = true;
                divAccountHead.Visible = true;
                divAccountSubHead.Visible = true;
                divTransType.Visible = false;
                divButtons.Visible = true;
                divCounter.Visible = false;
                divShift.Visible = false;
                divStoreCash.Visible = true;
                divStore.Visible = false;
                divAccountGroup.Visible = false;
                divAccountsubGroup.Visible = false;
                divAsOnDate.Visible = false;
                divAccountGroup.Visible = false;
                divAccountsubGroup.Visible = false;
                DivReportType.Visible = true;
                hfINVSubModule.Value = "ACC";
                lblHeadText.Text = "General Ledger";



            }



        }
        catch (Exception ex)
        { 
        }
    }
    #endregion

}