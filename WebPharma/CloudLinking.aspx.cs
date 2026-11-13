using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing;
using System.Configuration;
using System.Web.Script.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using Newtonsoft.Json;
using System.IO;
using System.Net.Http.Headers;
using System.Runtime.Serialization.Json;
using System.Collections;
using System.Xml.Linq;
using System.Data;
using WebPharma.Business.Masters;
using WebPharma.Business;
using WebPharma.Business.WebPharma;
using System.Globalization;
using Excel;
using System.Collections;
using System.Security.Cryptography.X509Certificates;
using System.Net.Security;


public partial class CloudLinking : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

        try
        {
           // Common.SessionCheck();
            if (!IsPostBack)
            {
                Page.Header.DataBind();
                //ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "ClearAdd();", true);
                divAdd.Style.Add("display", "block");
                hfUser.Value = Request.QueryString["uid"];
                hfClient.Value = Request.QueryString["clnt"];
                CheckUserAccess();
                GetImportExportData();
                Session["SaveClick"] = 0;

            }
        }
        catch (Exception)
        {

            throw;
        }
    }
    protected void btnImport_Click(object sender, EventArgs e)
    {
        Session["SaveClick"] = 0;
       ImportFill(1);
        //string StrsucessScripts = "";
        //StrsucessScripts = "<script>toastr.error('No Data Found');</script>";
        //ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", StrsucessScripts, false);
    }
    protected void btnExport_Click(object sender, EventArgs e)
    {
       // ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "toastr.error('Data Insertion Failed !.', null, { positionClass: 'toast-top-right' })", true);
        Session["SaveClick"] = 0;
         ExportFill(1);
    }


    protected void Clear()
    {
        gvImport.Enabled = true;
        gvImport.DataSource = null;
        gvImport.DataBind();
        gvImport.Enabled = true;
        //gvExport.DataSource = null;
        //gvExport.DataSource = null;
        //gvExport.DataBind();
    }
    protected void InitialiseData()
    {
        DataTable dtbl = new DataTable();
        dtbl.Columns.Add("SLNO");
        dtbl.Columns.Add("Date");
        dtbl.Columns.Add("Time");
        dtbl.Columns.Add("By");
        dtbl.Columns.Add("Status");

    }

    protected void CheckUserAccess()
    {
        BlUserAccess bluseraccess = new BlUserAccess();
        Populate populate = new Populate();

        bluseraccess.FK_Users = Convert.ToInt64(hfUser.Value);
        bluseraccess.FK_Pages = 177;
        bluseraccess.FK_Client = Convert.ToInt64(hfClient.Value);
        bluseraccess.CheckUserAccess();
        if (Convert.ToBoolean(bluseraccess.IsView) == false)
        {


        }
        if (bluseraccess.FK_Pages != 0)
        {
            BlFunction blfunctions = new BlFunction();
          //  lblPageName.Text = Convert.ToString(blfunctions.GetScalarValue("Pages", "ISNULL( PageName,'') PageName", "ID_Pages=" + bluseraccess.FK_Pages.ToString() + " AND  Active=1 ", ""));

        }

        bluseraccess = null;
    }
    //protected void GetImportExportData()
    //{
    //    try
    //    {
    //        InitialiseData();
    //        DataSet ds = new DataSet();
    //        DataTable dtb1 = new DataTable();
    //        DataTable dtb2 = new DataTable();
    //        BlCloudLink blcloudlink = new BlCloudLink();
    //        blcloudlink.FK_Client = Convert.ToInt64(hfClient.Value);
    //        blcloudlink.UserCode = hfUser.Value;
    //        blcloudlink.Mode = 1;
    //        blcloudlink.MasterId = Convert.ToInt64(hdnIDProcessid.Value == "" ? "0" : hdnIDProcessid.Value);
    //        blcloudlink.IDProcessExport = Convert.ToInt64(hdnEXpIDProcessid.Value == "" ? "0" : hdnEXpIDProcessid.Value);
    //        ds = blcloudlink.SelectImportandExportData();
    //        if (ds.Tables[0].Rows.Count > 0)
    //        {
    //            dtb1 = ds.Tables[0];

    //            if (dtb1.Rows.Count > 0)
    //            {

    //                gvImport.DataSource = dtb1;
    //                gvImport.DataBind();
    //            }
    //            else
    //            {
    //                gvImport.DataSource = null;
    //                gvImport.DataBind();
    //            }
    //        }
    //        else
    //        {
    //            gvImport.DataSource = null;
    //            gvImport.DataBind();
    //        }

    //        if (ds.Tables[1].Rows.Count > 0)
    //        {
    //            dtb2 = ds.Tables[1];
    //            if (dtb2.Rows.Count > 0)
    //            {

    //                gvExport.DataSource = dtb2;
    //                gvExport.DataBind();
    //            }
    //            else
    //            {
    //                gvExport.DataSource = null;
    //                gvExport.DataBind();
    //            }
    //        }
    //        else
    //        {
    //            gvExport.DataSource = null;
    //            gvExport.DataBind();
    //        }
    //    }
    //    catch (Exception)
    //    {

    //        throw;
    //    }

    //}
    protected void GetImportExportData()
    {
        try
        {
            InitialiseData();
            DataSet ds = new DataSet();
            DataTable dtb1 = new DataTable();
            DataTable dtb2 = new DataTable();
            DataTable dtexErr = new DataTable();
            ArrayList arlist = new ArrayList();
            BlCloudLink blcloudlink = new BlCloudLink();
            if (ViewState["ExportErr"] != null)
            {


                dtexErr = (DataTable)ViewState["ExportErr"];
                // int RowIndex = 0;

                foreach (DataRow dr in dtexErr.Rows)
                {
                    BlErrExportMsg errexp = new BlErrExportMsg();

                    errexp.EResID = Convert.ToInt64(dr["ResID"].ToString());
                    errexp.EResMsg = dr["ResMsg"].ToString();

                    arlist.Add(errexp);
                    //errexp = null;
                    //  RowIndex = RowIndex + 1;

                }
                blcloudlink.XmlErrorCode = Common.WriteToXml(arlist);


            }
            else
            {
                blcloudlink.XmlErrorCode = "";
            }

            blcloudlink.FK_Client = Convert.ToInt64(hfClient.Value);
            blcloudlink.UserCode = hfUser.Value;
            blcloudlink.Mode = 1;
            blcloudlink.MasterId = Convert.ToInt64(hdnIDProcessid.Value == "" ? "0" : hdnIDProcessid.Value);
            blcloudlink.IDProcessExport = Convert.ToInt64(hdnEXpIDProcessid.Value == "" ? "0" : hdnEXpIDProcessid.Value);
            ds = blcloudlink.SelectImportandExportData();
            if (ds.Tables[0].Rows.Count > 0)
            {
                dtb1 = ds.Tables[0];

                if (dtb1.Rows.Count > 0)
                {

                    gvImport.DataSource = dtb1;
                    gvImport.DataBind();
                }
                else
                {
                    gvImport.DataSource = null;
                    gvImport.DataBind();
                }
            }
            else
            {
                gvImport.DataSource = null;
                gvImport.DataBind();
            }

            if (ds.Tables[1].Rows.Count > 0)
            {
                dtb2 = ds.Tables[1];
                if (dtb2.Rows.Count > 0)
                {

                    gvExport.DataSource = dtb2;
                    gvExport.DataBind();
                }
                else
                {
                    gvExport.DataSource = null;
                    gvExport.DataBind();
                }
            }
            else
            {
                gvExport.DataSource = null;
                gvExport.DataBind();
            }

           

            ViewState["ExportErr"] = null;
        }
        catch (Exception EX)
        {

            throw EX;
        }

    }

    protected void gvImport_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label lbl = (Label)e.Row.FindControl("lblgvSLNO");
            lbl.Text = Convert.ToString(e.Row.RowIndex + 1);
            HiddenField hdfStatus = (HiddenField)e.Row.FindControl("hdStatusbitImport");

            var Status = (DataBinder.Eval(e.Row.DataItem, "Status"));
            if (hdfStatus.Value == "1")
            {
                e.Row.Cells[5].ForeColor = Color.Green;
            }
            else if (hdfStatus.Value == "-1")
            {
                e.Row.Cells[5].ForeColor = Color.Red;
            }
        }


    }
    protected void gvExport_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label lbl = (Label)e.Row.FindControl("lblgvSLNO");
            lbl.Text = Convert.ToString(e.Row.RowIndex + 1);


            HiddenField hdfStatus = (HiddenField)e.Row.FindControl("hdStatusbitExport");
            var Status = (DataBinder.Eval(e.Row.DataItem, "Status"));
            if (hdfStatus.Value == "1")
            {
                e.Row.Cells[5].ForeColor = Color.Green;
            }
            else if (hdfStatus.Value == "-1")
            {
                e.Row.Cells[5].ForeColor = Color.Red;

            }
        }

    }
    public void OpenMessagePopUp()
    {
       // mpMsgImport.Show();
    }
    public void ImportFill(int pageIndex)
    {
        Session["SaveClick"] = 0;
        // mpCloudImport.Hide();
        string XmlData = "";
        BlCloudLink blcloudlinking = new BlCloudLink();
        DataTable dt;
        blcloudlinking.FK_Client = Convert.ToInt64(hfClient.Value == "" ? "0" : hfClient.Value);
        blcloudlinking.UserCode = hfUser.Value;
        Populate Populate = new Populate();
        CommonPopupImport.CallingFunction = "ImportFill";
        string Refno = ((TextBox)CommonPopupImport.FindControl("txtRefno")).Text;
        string Total = ((TextBox)CommonPopupImport.FindControl("txtTo")).Text;
        string searchCol = ((HiddenField)CommonPopupImport.FindControl("hfColumn")).Value;
        string hfTotalCharge = ((HiddenField)CommonPopupImport.FindControl("hfTotalCharge")).Value;
        Refno = Refno == string.Empty || Refno == null ? "0" : Refno;
        ListView gvNew = (ListView)CommonPopupImport.FindControl("gdvSearch");
        dt = new DataTable();
        blcloudlinking.Mode = 1;
        blcloudlinking.FK_Client = Convert.ToInt64(hfClient.Value == "" ? "0" : hfClient.Value);
        dt = blcloudlinking.SelectImportDetails();
        if (dt.Rows.Count > 0)
        {

            dt.Columns.Add("UserCode");
            dt.Columns.Add("FK_Client");
            foreach (DataRow row in dt.Rows)
            {
                row["FK_Client"] = hfClient.Value;
                row["UserCode"] = hfUser.Value;
            }
            XmlData = DataTableToXml(dt);
        }
        else
        {
            XmlData = "";
        }
        GetImportScoreDataCound(1, XmlData);
        dt = (DataTable)ViewState["ImportFill"];
        gvNew.DataSource = dt;
        gvNew.DataBind();
        mpCloudImport.Show();
        Button btnSumbit = ((Button)CommonPopupImport.FindControl("btnSumbit"));
        btnSumbit.Enabled = false;
        TextBox txtTo = ((TextBox)CommonPopupImport.FindControl("txtTo"));
        txtTo.Enabled = false;
        HiddenField hdnUsercode = ((HiddenField)CommonPopupImport.FindControl("hdfusercode"));
        hdnUsercode.Value = hfUser.Value;

    }
    public void ExportFill(int pageIndex)
    {
        Session["SaveClick"] = 0;
        BlCloudLink blcloudlinking = new BlCloudLink();
        DataTable dt;
        blcloudlinking.FK_Client = Convert.ToInt64(hfClient.Value == "" ? "0" : hfClient.Value);
        blcloudlinking.UserCode = hfUser.Value;
        Populate Populate = new Populate();
        CommonPopupExport.CallingFunction = "ExportFill";
        string Refno = ((TextBox)CommonPopupExport.FindControl("txtRefno")).Text;
        string searchCol = ((HiddenField)CommonPopupExport.FindControl("hfColumn")).Value;
        Refno = Refno == string.Empty || Refno == null ? "0" : Refno;
        ListView gvNew = (ListView)CommonPopupExport.FindControl("gdvSearch");
        dt = new DataTable();

        if (ViewState["ExportFill"] != null)
        {
            dt = (DataTable)ViewState["ExportDetails"];
        }
        else
        {
            dt = blcloudlinking.SelectExportDetails();
            if (dt.Rows.Count > 0)
            {
                dt.Columns.Add("UserCode");
                dt.Columns.Add("FK_Client");
                foreach (DataRow row in dt.Rows)
                {
                    row["FK_Client"] = hfClient.Value;
                    row["UserCode"] = hfUser.Value;
                }


            }

        }
        gvNew.DataSource = dt;
        gvNew.DataBind();

        mpCloudExport.Show();
        Button btnSumbit = ((Button)CommonPopupExport.FindControl("btnexSubmit"));
        btnSumbit.Enabled = false;

        TextBox txtTo = ((TextBox)CommonPopupExport.FindControl("txtTo"));
        txtTo.Enabled = false;
        TextBox txtInvoiceDate = ((TextBox)CommonPopupExport.FindControl("txtInvoiceDate"));
        DateTime datet = DateTime.Now;
        string datestr = datet.ToString("dd/MM/yyyy");
        txtInvoiceDate.Text = datestr;
        CheckBox chkSupplierWise = ((CheckBox)CommonPopupExport.FindControl("chkSupplierWise"));
        chkSupplierWise.Checked = false;
        CheckBox chkRecovery = ((CheckBox)CommonPopupExport.FindControl("chkRecovery"));
        chkRecovery.Checked = false;
        HiddenField hdnUsercode = ((HiddenField)CommonPopupExport.FindControl("hdfusercode"));
        hdnUsercode.Value = hfUser.Value;
    }
    private DataSet GetImportScoreDataCound(Int32 Mode, string XmlData)
    {
        DataSet importData = new DataSet();
        string url = "";

        string CloudLinkingApiUrl = ConfigurationManager.AppSettings["MedlifeCloudLinkManually"].ToString() + "/api/ImportServer/SelectScoreMasterCount";
        url = CloudLinkingApiUrl;
        string Key = ConfigurationManager.AppSettings["BankKey"].ToString();
        string Product = ConfigurationManager.AppSettings["Product"].ToString();
        try
        {
            importData = GetImportCountAPI(url, Mode, XmlData, Key,Product);
            ViewState["ImportFill"] = importData.Tables[0];
            //   ImportFill(1);
            BlCloudLink blcloudlinking = new BlCloudLink();
            string ar = blcloudlinking.XmlExportCloudDetails;
        }
        catch (Exception e)
        {
            throw e;
        }
        return importData;
    }



    protected DataSet GetImportCountAPI(string url, Int32 Mode, string XmlData, string Key, string Product)
    {
        DataSet DataSet = new DataSet();
        try
        {
            parameter objPara = new parameter();
            objPara.url = url;
            objPara.Mode = Mode;
            objPara.XMLdata = XmlData;
            objPara.Key = Key;
            objPara.Product = Product;
            string jsonModel = JsonConvert.SerializeObject(objPara);
            var client = new HttpClient();
            client.BaseAddress = new Uri(url);
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            HttpContent content = new StringContent(jsonModel, Encoding.UTF8, "application/json");
            System.Net.ServicePointManager.ServerCertificateValidationCallback = delegate(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors) { return true; };
            var datresponse = client.PostAsync(url, content).Result;
            if (datresponse.IsSuccessStatusCode)
            {
                string responseContent = datresponse.Content.ReadAsStringAsync().Result;
                RootScoreLinkingRQ root = JsonConvert.DeserializeObject<RootScoreLinkingRQ>(responseContent);
                DataSet = root.dsdata;

            }

        }
        catch (Exception ex)
        {
            throw ex;
        }
        return DataSet;
    }

    public static DataSet ConvertCsvToDataSet(string csvData)
    {
        // Create a new DataSet
        DataSet dataSet = new DataSet();

        // Split the CSV data into lines
        string[] lines = csvData.Split(new[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);

        // Assuming the first line contains column headers, split it to get column names
        string[] columnNames = lines[0].Split(',');

        // Create a new DataTable
        DataTable dataTable = new DataTable("CSVData");

        // Add columns to the DataTable
        foreach (string columnName in columnNames)
        {
            dataTable.Columns.Add(columnName.Trim()); // Trim to remove any leading/trailing spaces
        }

        // Add data rows to the DataTable
        for (int i = 1; i < lines.Length; i++)
        {
            string[] values = lines[i].Split(',');

            // Create a new row
            DataRow dataRow = dataTable.NewRow();

            // Populate the row with values
            for (int j = 0; j < values.Length; j++)
            {
                dataRow[j] = values[j].Trim(); // Trim to remove any leading/trailing spaces
            }

            // Add the row to the DataTable
            dataTable.Rows.Add(dataRow);
        }

        // Add the DataTable to the DataSet
        dataSet.Tables.Add(dataTable);

        return dataSet;
    }

    public void SetCount(DataTable dt, string idProcess)
    {
        try
        {
            hdnIDProcessid.Value = idProcess;
            BlMasterCount blmaster;
            ArrayList arlist = new ArrayList();
            foreach (DataRow dr in dt.Rows)
            {
                blmaster = new BlMasterCount();
                blmaster.TableName = (dr["TableName"].ToString());
                if (dr["LastEntedOn"] != DBNull.Value)
                {
                    blmaster.LastEntedOn = Convert.ToDateTime(dr["LastEntedOn"].ToString());
                }

                blmaster.LastIdUpdate = Convert.ToInt64(dr["LastIdUpdate"].ToString());
                blmaster.Checked = Convert.ToBoolean(dr["checkedTable"].ToString());
                arlist.Add(blmaster);
            }

            string XmlData = Common.WriteToXml(arlist);
            GetImportScoreData(2, XmlData);
            ViewState["ImportFill"] = null;
            GetImportExportData();
            mpCloudImport.Hide();

            ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "loadexpData();", true);
          
        }
        catch (Exception e)
        {

            throw e;
        }
    }
    private DataSet GetImportScoreData(Int32 Mode, string XmlData)
    {
        DataSet dsimportData = new DataSet();
        string url = "";

        string CloudLinkingApiUrl = ConfigurationManager.AppSettings["MedlifeCloudLinkManually"].ToString() + "/api/ImportServer/SelectScoreMasterCount";
        url = CloudLinkingApiUrl;
        string Key = ConfigurationManager.AppSettings["BankKey"].ToString();
        string Product = ConfigurationManager.AppSettings["Product"].ToString();
        try
        {
            long statuscode = 0; string REturname = "";
            dsimportData = GetImportCountAPI(url, Mode, XmlData, Key, Product);
            if (dsimportData.Tables.Count > 0)
            {
                DataTable dtAccountGroup = new DataTable();
                DataTable dtAccountsubGroup = new DataTable();
                DataTable dtBehaviour = new DataTable();
                DataTable dtAccountHead = new DataTable();
                DataTable dtAccountHeadSub = new DataTable();
                DataTable dtState = new DataTable();
                DataTable dtDistrict = new DataTable();
                DataTable dtPlace = new DataTable();
                DataTable dtPost = new DataTable();
                DataTable dtIdentityProof = new DataTable();
                DataTable dtCustomerType = new DataTable();
                DataTable dtCustomer = new DataTable();
                DataTable dtSupplier = new DataTable();
                DataTable dtAgent = new DataTable();
                DataTable dtRecovery = new DataTable();
                BlCloudLink blcloudlink = new BlCloudLink();
                DataSet ds;
                ds = dsimportData;
                ds = new DataSet();
                dtAccountGroup = dsimportData.Tables[0];
                dtAccountsubGroup = dsimportData.Tables[1];
                dtBehaviour = dsimportData.Tables[2];
                dtAccountHead = dsimportData.Tables[3];
                dtState = dsimportData.Tables[4];
                dtDistrict = dsimportData.Tables[5];
                dtPlace = dsimportData.Tables[6];
                dtPost = dsimportData.Tables[7];
                dtIdentityProof = dsimportData.Tables[8];
                dtCustomerType = dsimportData.Tables[9];
                dtCustomer = dsimportData.Tables[10];
                dtAccountHeadSub = dsimportData.Tables[11];
                dtSupplier = dsimportData.Tables[12];
                dtAgent = dsimportData.Tables[13];
                dtRecovery = dsimportData.Tables[14];
                string StrScripts = ""; string StrsucessScripts = "";

                if (dsimportData.Tables[0] != null)
                {

                    if (dtAccountGroup.Rows.Count > 0)
                    {
                        blcloudlink.XmldtAccountGroup = DataTableToXml(dtAccountGroup);
                        blcloudlink.InsertMasterAccountGroupDetail();
                        statuscode = blcloudlink.ReturnId;
                        REturname = blcloudlink.ReturnMessage;



                        if (statuscode > 0)
                        {

                            StrScripts = "<script>toastr.success('" + REturname + "');</script>";
                          
                        }
                        else
                        {
                            StrScripts = StrScripts + "<script>toastr.error('" + REturname + "');</script>";
                         
                        }
                    }

                }
                if (dsimportData.Tables[1] != null)
                {

                    if (dtAccountsubGroup.Rows.Count > 0)
                    {

                        blcloudlink.XmldtAccountsubGroup = DataTableToXml(dtAccountsubGroup);
                        blcloudlink.InsertMasterAccountsubGroupData();
                        statuscode = blcloudlink.ReturnId;
                        REturname = blcloudlink.ReturnMessage;
                        if (statuscode > 0)
                        {
                         
                            StrScripts = StrScripts + "<script>toastr.success('" + REturname + "');</script>";

                        }
                        else
                        {
                            StrScripts = StrScripts + "<script>toastr.error('" + REturname + "');</script>";
                          
                        }
                    }


                }
                if (dsimportData.Tables[2] != null)
                {

                    if (dtBehaviour.Rows.Count > 0)
                    {

                        blcloudlink.XmldtBehaviour = DataTableToXml(dtBehaviour);

                        blcloudlink.InsertMasterAccountBehaviourData();
                        statuscode = blcloudlink.ReturnId;
                        REturname = blcloudlink.ReturnMessage;
                        if (statuscode > 0)
                        {
                            StrScripts = StrScripts + "<script>toastr.success('" + REturname + "');</script>";
                          
                        }
                        else
                        {
                            StrScripts = StrScripts + "<script>toastr.error('" + REturname + "');</script>";
                           
                        }
                    }


                }
                if (dsimportData.Tables[3] != null)
                {

                    if (dtAccountHead.Rows.Count > 0)
                    {

                        blcloudlink.XmldtAccountHead = DataTableToXml(dtAccountHead);

                        blcloudlink.InsertMasterAccountHeadData();
                        statuscode = blcloudlink.ReturnId;
                        REturname = blcloudlink.ReturnMessage;
                        if (statuscode > 0)
                        {
                            StrScripts = StrScripts + "<script>toastr.success('" + REturname + "');</script>";

                        }
                        else
                        {
                            StrScripts = StrScripts + "<script>toastr.error('" + REturname + "');</script>";
                           
                        }
                    }


                }

                if (dsimportData.Tables[4] != null)
                {

                    if (dtState.Rows.Count > 0)
                    {

                        blcloudlink.XmldtState = DataTableToXml(dtState);
                        blcloudlink.InsertImportMasterStateData();
                        statuscode = blcloudlink.ReturnId;
                        REturname = blcloudlink.ReturnMessage;
                        if (statuscode > 0)
                        {
                            StrScripts = StrScripts + "<script>toastr.success('" + REturname + "');</script>";
                        }
                        else
                        {
                            StrScripts = StrScripts + "<script>toastr.error('" + REturname + "');</script>";
                           
                        }
                    }


                }
                if (dsimportData.Tables[5] != null)
                {

                    if (dtDistrict.Rows.Count > 0)
                    {

                        blcloudlink.XmldtDistrict = DataTableToXml(dtDistrict);
                        blcloudlink.InsertImportMasterDistrictData();
                        statuscode = blcloudlink.ReturnId;
                        REturname = blcloudlink.ReturnMessage;
                        if (statuscode > 0)
                        {
                            StrScripts = StrScripts + "<script>toastr.success('" + REturname + "');</script>";
                          
                        }
                        else
                        {
                            StrScripts = StrScripts + "<script>toastr.error('" + REturname + "');</script>";
                            
                        }
                    }


                }
                if (dsimportData.Tables[6] != null)
                {

                    if (dtPlace.Rows.Count > 0)
                    {

                        blcloudlink.XmldtPlace = DataTableToXml(dtPlace);
                        blcloudlink.InsertImportMasterPlaceData();
                        statuscode = blcloudlink.ReturnId;
                        REturname = blcloudlink.ReturnMessage;
                        if (statuscode > 0)
                        {
                            StrScripts = StrScripts + "<script>toastr.success('" + REturname + "');</script>";
                           
                        }
                        else
                        {
                            StrScripts = StrScripts + "<script>toastr.error('" + REturname + "');</script>";
                       
                        }
                    }


                }
                if (dsimportData.Tables[7] != null)
                {

                    if (dtPost.Rows.Count > 0)
                    {

                        blcloudlink.XmldtPost = DataTableToXml(dtPost);
                        blcloudlink.InsertImportMasterPostData();
                        statuscode = blcloudlink.ReturnId;
                        REturname = blcloudlink.ReturnMessage;
                        if (statuscode > 0)
                        {
                            StrScripts = StrScripts + "<script>toastr.success('" + REturname + "');</script>";
                        }
                        else
                        {
                            StrScripts = StrScripts + "<script>toastr.error('" + REturname + "');</script>";
                        }
                    }


                }
                if (dsimportData.Tables[8] != null)
                {

                    if (dtIdentityProof.Rows.Count > 0)
                    {

                        blcloudlink.XmldtIdentityProof = DataTableToXml(dtIdentityProof);
                        blcloudlink.InsertImportMasterIdentityProofData();
                        statuscode = blcloudlink.ReturnId;
                        REturname = blcloudlink.ReturnMessage;
                        if (statuscode > 0)
                        {
                            StrScripts = StrScripts + "<script>toastr.success('" + REturname + "');</script>";
                        }
                        else
                        {
                            StrScripts = StrScripts + "<script>toastr.error('" + REturname + "');</script>";
                       
                        }
                    }

                }
                if (dsimportData.Tables[9] != null)
                {

                    if (dtCustomerType.Rows.Count > 0)
                    {

                        blcloudlink.XmldtCustomerType = DataTableToXml(dtCustomerType);
                        blcloudlink.InsertImportMasterCustomerTypeData();
                        statuscode = blcloudlink.ReturnId;
                        REturname = blcloudlink.ReturnMessage;
                        if (statuscode > 0)
                        {
                            StrScripts = StrScripts + "<script>toastr.success('" + REturname + "');</script>";
                        }
                        else
                        {
                            StrScripts = StrScripts + "<script>toastr.error('" + REturname + "');</script>";
                        }
                    }


                }
                if (dsimportData.Tables[10] != null)
                {

                    if (dtCustomer.Rows.Count > 0)
                    {

                        blcloudlink.XmldtCustomer = DataTableToXml(dtCustomer);
                        blcloudlink.InsertImportMasterCustomerData();
                        statuscode = blcloudlink.ReturnId;
                        REturname = blcloudlink.ReturnMessage;
                        if (statuscode > 0)
                        {
                            StrScripts = StrScripts + "<script>toastr.success('" + REturname + "');</script>";
                        }
                        else
                        {
                            StrScripts = StrScripts + "<script>toastr.error('" + REturname + "');</script>";
                           
                        }
                    }

                }
                if (dsimportData.Tables[11] != null)
                {

                    if (dtAccountHeadSub.Rows.Count > 0)
                    {

                        blcloudlink.XmldtAccountHeadSub = DataTableToXml(dtAccountHeadSub);
                        blcloudlink.InsertMasterAccountHeadSubData();
                        statuscode = blcloudlink.ReturnId;
                        REturname = blcloudlink.ReturnMessage;
                        if (statuscode > 0)
                        {
                            StrScripts = StrScripts + "<script>toastr.success('" + REturname + "');</script>";
                        }
                        else
                        {
                            StrScripts = StrScripts + "<script>toastr.error('" + REturname + "');</script>";
                            
                        }
                    }

                }
                if (dsimportData.Tables[12] != null)
                {

                    if (dtSupplier.Rows.Count > 0)
                    {

                        blcloudlink.XmldtSupplier = DataTableToXml(dtSupplier);
                        blcloudlink.InsertMasterSupplierData();
                        statuscode = blcloudlink.ReturnId;
                        REturname = blcloudlink.ReturnMessage;
                        if (statuscode > 0)
                        {
                            StrScripts = StrScripts + "<script>toastr.success('" + REturname + "');</script>";
                            // ScriptManager.RegisterStartupScript(Page, Page.GetType(), "Script", "toastr.success('AccountHead Inserted Successfully');", true);
                        }
                        else
                        {
                            StrScripts = StrScripts + "<script>toastr.error('" + REturname + "');</script>";
                            //ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "toastr.error(' Process Failed..!! Cannot Proceed.')", true);
                            //ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "toastr.error('" + REturname + "')", true);
                        }
                    }


                }
                if (dsimportData.Tables[13] != null)
                {

                    if (dtAgent.Rows.Count > 0)
                    {

                        blcloudlink.XmldtAgent = DataTableToXml(dtAgent);
                        blcloudlink.InsertImportMasterAgentData();
                        statuscode = blcloudlink.ReturnId;
                        REturname = blcloudlink.ReturnMessage;
                        if (statuscode > 0)
                        {
                            StrScripts = StrScripts + "<script>toastr.success('" + REturname + "');</script>";
                            // ScriptManager.RegisterStartupScript(Page, Page.GetType(), "Script", "toastr.success('AccountHead Inserted Successfully');", true);
                        }
                        else
                        {
                            StrScripts = StrScripts + "<script>toastr.error('" + REturname + "');</script>";
                            //ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "toastr.error(' Process Failed..!! Cannot Proceed.')", true);
                            //ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "toastr.error('" + REturname + "')", true);
                        }
                    }


                }
                if (dsimportData.Tables[14] != null)
                {

                    if (dtRecovery.Rows.Count > 0)
                    {

                        blcloudlink.XmldtRecovery = DataTableToXml(dtRecovery);
                        blcloudlink.InsertImportRecoveryData();
                        statuscode = blcloudlink.ReturnId;
                        REturname = blcloudlink.ReturnMessage;
                        if (statuscode > 0)
                        {
                            StrScripts = StrScripts + "<script>toastr.success('" + REturname + "','', { timeOut: 8000 });</script>";
                            // ScriptManager.RegisterStartupScript(Page, Page.GetType(), "Script", "toastr.success('AccountHead Inserted Successfully');", true);
                        }
                        else
                        {
                            StrScripts = StrScripts + "<script>toastr.error('" + REturname + "','', { timeOut: 8000 });</script>";
                            //ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "toastr.error(' Process Failed..!! Cannot Proceed.')", true);
                            //ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "toastr.error('" + REturname + "')", true);
                        }
                    }


                }
                if ((dtAccountGroup.Rows.Count <= 0) && (dtAccountsubGroup.Rows.Count <= 0) && (dtBehaviour.Rows.Count <= 0) && (dtAccountHead.Rows.Count <= 0) && (dtState.Rows.Count <= 0)
                     && (dtDistrict.Rows.Count <= 0) && (dtPlace.Rows.Count <= 0) && (dtPost.Rows.Count <= 0) && (dtIdentityProof.Rows.Count <= 0) && (dtCustomerType.Rows.Count <= 0)
                     && (dtCustomer.Rows.Count <= 0) && (dtAccountHeadSub.Rows.Count <= 0) && (dtSupplier.Rows.Count <= 0) && (dtAgent.Rows.Count <= 0) && (dtRecovery.Rows.Count <= 0))
                {
                    StrsucessScripts = "<script>toastr.error('No Data Found');</script>";
                    ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", StrsucessScripts, false);
                }
                else
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "Success", StrScripts, false);
            }
        }
        catch (Exception e)
        {

        }
        return dsimportData;
    }
    public void SetExportData(DataTable dt, DataTable dtsup, Boolean ChkRecovery,DataTable dtsalm)
    {
        DataTable dtexpo = new DataTable();
        dtexpo = dt;
        GetExporttoScore(2, dt, dtsup, ChkRecovery, dtsalm);
        mpCloudExport.Hide();
        GetImportExportData();
        //ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "hideModalPopup();", true);
       ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "loadexpData();", true);
        //ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "HideModalScript", "hideModalPopup();", true);
    }

    public static string WriteToXml(DataTable dt, string tablename)
    {
        StringWriter strxml = new StringWriter();
        {
            try
            {
                dt.TableName = tablename;
                dt.WriteXml(strxml, XmlWriteMode.IgnoreSchema, true);
            }
            catch (Exception)
            {
                throw;
            }
            return strxml.ToString().Replace(tablename, "row").Replace("NewDataSet", tablename);
        }
    }

    public static string DsToXml(DataSet ds)
    {
        StringBuilder xml = new StringBuilder();
        xml.Append("<root>");

        foreach (DataTable dt in ds.Tables)
        {
            if (dt.Rows.Count > 0)
            {
                // Create a StringWriter to hold the XML for this DataTable
                using (StringWriter writer = new StringWriter())
                {
                    dt.WriteXml(writer, XmlWriteMode.WriteSchema, false);
                    xml.Append(writer.ToString());
                }
            }
            else
            {
                // Add an empty element for tables with no rows
                xml.Append("<{dt.TableName}></{dt.TableName}>");
            }
        }

        xml.Append("</root>");
        return xml.ToString();
    }
    public static string DataTableToXml(DataTable dataTable)
    {
        // Create a StringWriter to hold the XML
        using (StringWriter stringWriter = new StringWriter())
        {
            // Use the DataTable's WriteXml method to write the XML
            dataTable.WriteXml(stringWriter, XmlWriteMode.IgnoreSchema, true);

            // Return the XML as a string
            return stringWriter.ToString();
        }
    }

    private DataTable GetExporttoScore(Int32 Mode, DataTable dttExportData, DataTable dtSupplier, Boolean Recovery, DataTable dtSalesman)
    {
        DataTable dtExportData = new DataTable();
        string url = "";

        string CloudLinkingApiUrl = ConfigurationManager.AppSettings["MedlifeCloudLinkManually"].ToString() + "/api/ExportServer/InsertESModuleDatatoScore";
        url = CloudLinkingApiUrl;
        string Key = ConfigurationManager.AppSettings["BankKey"].ToString();
        string Product = ConfigurationManager.AppSettings["Product"].ToString();
        dtExportData = GetExportCountAPI(url, Mode, dttExportData, dtSupplier, Key, Product, Recovery, dtSalesman);
        string ResMessage = ""; string StrexScripts = "";
        long ResID = 0;
        if (dtExportData.Rows.Count > 0)
        {
            hdnEXpIDProcessid.Value = dtExportData.Rows[0]["Processid"].ToString() == "" ? "0" : dtExportData.Rows[0]["Processid"].ToString();
            ViewState["ExportErr"] = dtExportData;
            StringBuilder scriptBuilder = new StringBuilder();
            foreach (DataRow row in dtExportData.Rows)
            {
                // Access columns using column names or indexes
                ResID = (long)row["ResID"];
                ResMessage = (string)row["ResMsg"];
                if (ResID > 0)
                {
                    StrexScripts = "<script>toastr.success('" + ResMessage + "','', { timeOut: 8000 });</script>";
                }
                else
                {
                    hdnErrorCode.Value = "-1";
                    StrexScripts = "<script>toastr.error('" + ResMessage + "','', { timeOut: 8000 });</script>";
                }

                // Append the script to the StringBuilder
                scriptBuilder.AppendLine(StrexScripts);
            }

            // Register all scripts outside the loop
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "AllScripts", scriptBuilder.ToString(), false);
        }
        else
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "Script", "toastr.success('Data Insertion Failed !.');", false);
        }
        return dtExportData;

    }

    protected DataTable GetExportCountAPI(string url, Int32 Mode, DataTable dttExportData, DataTable dtSupplier, string Key, string Product, Boolean Recovery, DataTable dtSalesman)
    {
        DataTable dtExport = new DataTable();
        try
        {
            Exportparameter objPara = new Exportparameter();
            objPara.url = url;
            objPara.Mode = Mode;
            objPara.dtExpData = DataTableToXml(dttExportData);
            objPara.dtSupplierData = DataTToXml(dtSupplier);
            objPara.dtSalesmandata = DataTableToXml(dtSalesman);
            objPara.Key = Key;
            objPara.Product = Product;
            objPara.Recovery = Recovery;
            string jsonModel = JsonConvert.SerializeObject(objPara);
            var client = new HttpClient();
            client.BaseAddress = new Uri(url);
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            HttpContent content = new StringContent(jsonModel, Encoding.UTF8, "application/json");
            System.Net.ServicePointManager.ServerCertificateValidationCallback = delegate(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors) { return true; };
            var datresponse = client.PostAsync(url, content).Result;
            if (datresponse.IsSuccessStatusCode)
            {
                string responseContent = datresponse.Content.ReadAsStringAsync().Result;
                RootExportScoreLinkingRQ root = JsonConvert.DeserializeObject<RootExportScoreLinkingRQ>(responseContent);
                dtExport = root.dtExportData;
            }

        }
        catch (Exception ex)
        {

        }
        return dtExport;
    }
    protected void btnimpimpex_Click(object sender, EventArgs e)
    {
        GetImportExportData();
    }
    public string DataTToXml(DataTable dataTable)
    {
        using (var sw = new StringWriter())
        {
            dataTable.WriteXml(sw);
            return sw.ToString();
        }
    }
    public void CloseExportData(int i)
    {
        mpCloudExport.Hide();
        //ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "loadexpData();", true);
        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "HideModalScript", "hideModalPopup();", true);
        mpCloudExport.Hide();
    }
    public void CloseImportData(int i)
    {
        mpCloudImport.Hide();
        ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "loadexpData();", true);
        mpCloudImport.Hide();
    }
}