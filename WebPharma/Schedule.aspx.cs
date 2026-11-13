/*----------------------------------------------------------------------------------------------------------------------------------------------------------
Purpose    : Insert/Update/Delete Employee  
Author     : bibin tc
Copyright  : Perfect Software Solutions (Clt) Pvt Ltd
Created on : 07/05/2018
----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
WebPharma
On			By			  OMID
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
using System.IO;
using System.Text;
using System.Security.Cryptography;
using System.Collections;
using System.Threading;
using System.Globalization;

using System.Data.OleDb;

using Excel;

public partial class Schedule : System.Web.UI.Page
{
    #region PablicVariables
    private const string Excel = "Excel";
    public static DataSet dsExcel;
    public string ExcelName = "";
    public decimal m = 0;
    #endregion

    #region Functions

    //private void FillStore()
    //{
    //    BlEmployee blEmployee = new BlEmployee();
    //    chklistStores.DataSource = null;
    //    chklistStores.DataValueField = "ID_Store";
    //    chklistStores.DataTextField = "Name";
    //    chklistStores.DataSource = blEmployee.SelectStores();
    //    chklistStores.DataBind();

    //    Populate populate = new Populate();
    //    populate.FillDropdownList(ddlDefaultStore, "Store", "ShortName+'-'+Name", "ID_Store", "ID_Store", " Active='True' AND  FK_Client= " + hfClient.Value, "");
    //    populate = null;

    //}

    protected void BindData()
    {
        Populate populate = new Populate();

        //populate.FillDropdownList(ddlSalesMan, "SalesMan", "ShortName+' - '+Name", "ID_SalesMan", "ID_SalesMan", "", "");
        populate = null;

    }
    protected void SetControls()
    {

    }
    protected void GridFill(int currentPage)
    {
        BlSchedule BlSchedule = new BlSchedule();
        DataTable dtbl = new DataTable();

        BlSchedule.SearchFromScheduleUpto = Convert.ToDateTime(txtSearchFromScheduleUpto.Text == "" ? "" + DateTime.Now.Date + "" : txtSearchFromScheduleUpto.Text); //DateTime.ParseExact(txtSearchFromScheduleUpto.Text == "" ? DateTime.Now.Date : txtSearchFromScheduleUpto.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
        BlSchedule.SearchToScheduleUpto = Convert.ToDateTime(txtSearchToScheduleUpto.Text == "" ? "" + DateTime.Now.Date + "" : txtSearchToScheduleUpto.Text);// DateTime.ParseExact(txtSearchToScheduleUpto.Text == "" ? DateTime.Now.Date : txtSearchToScheduleUpto.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
        BlSchedule.PageIndex = Convert.ToInt16(currentPage);
        BlSchedule.FK_Client = Convert.ToInt64(hfClient.Value);
        BlSchedule.UserCode = hfUser.Value;
        dtbl = BlSchedule.SelectAllData();
        gvSchedule.DataSource = dtbl;
        gvSchedule.DataBind();
        if (dtbl.Rows.Count > 0)
        {
            int recordCount = Convert.ToInt32(dtbl.Rows[0]["RecordCount"].ToString());
            this.PopulatePager(recordCount, currentPage);
        }
        BlSchedule = null;
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
    public static void MergeRows(GridView gridView)
    {
        for (int rowIndex = gridView.Rows.Count - 2; rowIndex >= 0; rowIndex--)
        {
            GridViewRow row = gridView.Rows[rowIndex];
            GridViewRow previousRow = gridView.Rows[rowIndex + 1];

            for (int i = 0; i < row.Cells.Count; i++)
            {
                if (row.Cells[i].Text == previousRow.Cells[i].Text)
                {
                    row.Cells[i].RowSpan = previousRow.Cells[i].RowSpan < 2 ? 2 :
                                           previousRow.Cells[i].RowSpan + 1;
                    previousRow.Cells[i].Visible = false;
                }
            }
        }
    }
    //private ArrayList UpdateStoreAccess()
    //{
    //    StoreAccessDetails StoreAccessDetails;
    //    ArrayList arlist = new ArrayList();
    //    for (int j = 0; j < chklistStores.Items.Count; j++)
    //    {
    //        if (chklistStores.Items[j].Selected == true && chklistStores.Items[j].Value != ddlDefaultStore.SelectedValue.ToString())
    //        {
    //            StoreAccessDetails = new StoreAccessDetails();
    //            StoreAccessDetails.FK_Store = Convert.ToInt64(chklistStores.Items[j].Value);
    //            StoreAccessDetails.IsDefault = false;
    //            string name = chklistStores.Items[j].Text;
    //            arlist.Add(StoreAccessDetails);
    //        }
    //    }
    //    StoreAccessDetails = new StoreAccessDetails();
    //    StoreAccessDetails.FK_Store = Convert.ToInt64(ddlDefaultStore.SelectedValue.ToString());
    //    StoreAccessDetails.IsDefault = true;
    //    arlist.Add(StoreAccessDetails);
    //    return arlist;
    //}
    protected void CheckUserAccess()
    {
        BlUserAccess bluseraccess = new BlUserAccess();
        Populate populate = new Populate();

        bluseraccess.FK_Users = Convert.ToInt64(hfUser.Value);
        bluseraccess.FK_Pages = 5;
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



    protected void PostingData()
    {
        long status = 0;
        BlSchedule blschedule = new BlSchedule();
        blschedule.FK_Client = Convert.ToInt64(hfClient.Value);
        blschedule.UserCode = hfUser.Value;
        blschedule.ScheduleFrom = DateTime.ParseExact(txtScheduleFrom.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
        blschedule.ScheduleUpto = DateTime.ParseExact(txtScheduleUpto.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
        if (rdPosting.Checked == true)
        {
            blschedule.ScheduleMode = Convert.ToInt16(2);
        }
        else if (rdProcessing.Checked == true)
        {
            blschedule.ScheduleMode = Convert.ToInt16(1);
        }

        if (hfIsAdd.Value.ToLower() == "true")
        {
            //status = blschedule.InsertData();
            if (status > 0)
            {
                ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "ShowAddDiv();", true);
                ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "toastr.success('Data inserted successfully');ClearAdd();", true);

                // GridFill(1);
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "ShowAddDiv();", true);
                ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "toastr.error('Cannot insert data, Employee code already exists')", true);

            }
        }


        blschedule = null;
    }

    protected void SaveData()
    {
        long status = 0;


        //BlEmployee blEmployee = new BlEmployee();


        //blEmployee.FK_Client = Convert.ToInt64(hfClient.Value);
        //blEmployee.UserCode = hfUser.Value;
        ////blEmployee.Dob = DateTime.ParseExact(txtDob.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
        ////blEmployee.Salesman = Convert.ToBoolean(ChkSalesMan.Checked);
        ////blEmployee.Active = Convert.ToBoolean(chkActive.Checked);
        //blEmployee.EnteredOn = Convert.ToDateTime(DateTime.Now);
        //// blEmployee.XmlStoreAccessDetails = Common.WriteToXml(UpdateStoreAccess());

        //if (hfScheduleId.Value == "0")
        //{
        //    if (hfIsAdd.Value.ToLower() == "true")
        //    {
        //        status = blEmployee.InsertData();
        //        if (status > 0)
        //        {
        //            ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "ShowAddDiv();", true);
        //            ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "toastr.success('Data inserted successfully');ClearAdd();", true);

        //            GridFill(1);
        //        }
        //        else
        //        {
        //            ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "ShowAddDiv();", true);
        //            ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "toastr.error('Cannot insert data, Employee code already exists')", true);

        //        }
        //    }
        //    else
        //    {
        //        ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "toastr.warning('User does not have permission to add')", true);

        //    }
        //}
        //else
        //{
        //    if (hfIsUpdate.Value.ToLower() == "true")
        //    {

        //        blEmployee.MasterId = Convert.ToInt64(hfScheduleId.Value);
        //        status = blEmployee.UpdateData();
        //        if (status > 0)
        //        {

        //            ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "ShowAddDiv();", true);
        //            ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "toastr.success('Data updated successfully');ClearAdd();", true);
        //            GridFill(1);
        //        }
        //        else
        //        {
        //            ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "ShowAddDiv();", true);
        //            ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "toastr.error('Cannot update data, Employee code already exists')", true);

        //        }
        //    }
        //    else
        //    {
        //        ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "toastr.warning('User does not have permission to Update')", true);
        //    }
        //}
        //blEmployee = null;
    }
    private void SaveScheduleImport()
    {
        long status = 0;
        BlSchedule blschedule = new BlSchedule();
        blschedule.FK_Client = Convert.ToInt64(hfClient.Value);
        blschedule.UserCode = hfUser.Value;
        blschedule.ScheduleFrom = DateTime.ParseExact(txtScheduleFrom.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
        blschedule.ScheduleUpto = DateTime.ParseExact(txtScheduleUpto.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
        blschedule.ScheduleMode = Convert.ToInt16(1);

        ArrayList arlist = new ArrayList();
        DataTable dtbl = (DataTable)ViewState["Posting"];

        BlSchedulePosting BlSchedulePosting;
        for (int i = 0; i < dtbl.Rows.Count; i++)
        {
            BlSchedulePosting = new BlSchedulePosting();

            BlSchedulePosting.CusNumber = Convert.ToString(dtbl.Rows[i]["CusNumber"].ToString());
            BlSchedulePosting.Amount = Convert.ToDecimal(dtbl.Rows[i]["RecoveredAmount"].ToString());


            arlist.Add(BlSchedulePosting);
            BlSchedulePosting = null;
        }

        blschedule.XmlSchedulePosting = Common.WriteToXml(arlist);




        if (hfIsAdd.Value.ToLower() == "true")
        {
            status = blschedule.SaveScheduleImport();
            if (status > 0)
            {
                ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "ShowAddDiv();", true);
                ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "toastr.success('Data Fetched and Inserted successfully');", true);

                // GridFill(1);
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "ShowAddDiv();", true);
                ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "toastr.error('Cannot insert data, Data Error..')", true);

            }
        }
        blschedule = null;
    }
    private void SaveProcessData()
    {
        long status = 0;
        BlSchedule blschedule = new BlSchedule();
        blschedule.FK_Client = Convert.ToInt64(hfClient.Value);
        blschedule.UserCode = hfUser.Value;
        blschedule.ScheduleFrom = DateTime.ParseExact(txtScheduleFrom.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
        blschedule.ScheduleUpto = DateTime.ParseExact(txtScheduleUpto.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
        blschedule.ScheduleMode = Convert.ToInt16(1);

        ArrayList arlist = new ArrayList();
        DataTable dtbl = (DataTable)ViewState["Processing"];

        BlScheduleProcessing BlScheduleProcessing;
        for (int i = 0; i < dtbl.Rows.Count; i++)
        {
            BlScheduleProcessing = new BlScheduleProcessing();

            BlScheduleProcessing.FK_Customer = Convert.ToInt64(dtbl.Rows[i]["FK_Customer"].ToString());
            BlScheduleProcessing.FK_TermLoanApplication = Convert.ToInt64(dtbl.Rows[i]["FK_TermLoanApplication"].ToString());
            BlScheduleProcessing.ProcessingNeethiAmount = Convert.ToDecimal(dtbl.Rows[i]["NeethiAmt"].ToString());
            BlScheduleProcessing.ProcessingConsumerAmount = Convert.ToDecimal(dtbl.Rows[i]["ConsumerAmt"].ToString());
            BlScheduleProcessing.ProcessingRecoveryAmount = Convert.ToDecimal(dtbl.Rows[i]["RecoveryAmt"].ToString());

            //BlScheduleProcessing.ProcessingLoanAmount = Convert.ToDecimal(dtbl.Rows[i]["LoanAmount"].ToString());
            //BlScheduleProcessing.ProcessingLoanNumber = Convert.ToString(dtbl.Rows[i]["LoanNumber"].ToString());

            //BlScheduleProcessing.ProcessingCusNumber = Convert.ToString(dtbl.Rows[i]["CusNumber"].ToString());

            arlist.Add(BlScheduleProcessing);
            BlScheduleProcessing = null;
        }

        blschedule.XmlScheduleProcessing = Common.WriteToXml(arlist);





        if (hfIsAdd.Value.ToLower() == "true")
        {
            status = blschedule.SaveProcessData();
            if (status > 0)
            {
                ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "ShowAddDiv();", true);
                ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "toastr.success('Schedule Processed successfully');ClearAdd();", true);
                ClearAll();
                string ScheduleId = status.ToString();

                string popupreport = "<script> window.open('ScheduleReport.aspx?ScheduleId=" + ScheduleId + "', 'self', 'left=200,top=50,width=900,height=680,status=0,resizable=0,fullscreen=0,scrollbars=1');</script>";
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "Success", popupreport, false);
                // GridFill(1);
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "ShowAddDiv();", true);
                ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "toastr.error('Cannot Process data,..')", true);

            }
        }
        blschedule = null;
    }

    private void SavePostingData()
    {
        long status = 0;
        BlSchedule blschedule = new BlSchedule();
        blschedule.FK_Client = Convert.ToInt64(hfClient.Value);
        blschedule.UserCode = hfUser.Value;
        blschedule.ScheduleFrom = DateTime.ParseExact(txtScheduleFrom.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
        blschedule.ScheduleUpto = DateTime.ParseExact(txtScheduleUpto.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
        blschedule.ScheduleMode = Convert.ToInt16(0);

        //ArrayList arlist = new ArrayList();
        //DataTable dtbl = (DataTable)ViewState["Posting"];

        //BlSchedulePosting BlSchedulePosting;
        //for (int i = 0; i < dtbl.Rows.Count; i++)
        //{
        //    BlSchedulePosting = new BlSchedulePosting();

        //    BlSchedulePosting.CusNumber = Convert.ToString(dtbl.Rows[i]["CusNumber"].ToString());
        //    BlSchedulePosting.Amount = Convert.ToDecimal(dtbl.Rows[i]["RecoveredAmount"].ToString());


        //    arlist.Add(BlSchedulePosting);
        //    BlSchedulePosting = null;
        //}

        //blschedule.XmlSchedulePosting = Common.WriteToXml(arlist);





        if (hfIsAdd.Value.ToLower() == "true")
        {
            status = blschedule.SaveProcessData();
            if (status > 0)
            {
                ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "ShowAddDiv();", true);
                ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "toastr.success('Schedule Posted  successfully');ClearAdd();", true);
                ClearAll();
                // GridFill(1);
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "ShowAddDiv();", true);
                ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "toastr.error('Cannot Post Schedule, Data Error..')", true);

            }
        }
        blschedule = null;
    }

    private void ClearAll()
    {
        GvProcessing.DataSource = null;
        GvProcessing.DataBind();
        grvExcelData.DataSource = null;
        grvExcelData.DataBind();
        grvPostingData.DataSource = null;
        grvPostingData.DataBind();
        txtScheduleFrom.Enabled = true;
        txtScheduleUpto.Enabled = true;
    }

    private void ProcessingData()
    {

        long status = 0;
        DataTable dtbl = new DataTable();
        BlSchedule blschedule = new BlSchedule();
        blschedule.FK_Client = Convert.ToInt64(hfClient.Value);
        blschedule.UserCode = hfUser.Value;
        blschedule.ScheduleFrom = DateTime.ParseExact(txtScheduleFrom.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
        blschedule.ScheduleUpto = DateTime.ParseExact(txtScheduleUpto.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);




        dtbl = blschedule.ProcessingData();
        if (dtbl.Rows.Count > 0)
        {


            GvProcessing.DataSource = dtbl;
            GvProcessing.DataBind();
            ViewState["Processing"] = dtbl;
            ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "ShowAddDiv();", true);
            // GridFill(1);
        }
        else
        {
            ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "ShowAddDiv();", true);
            ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "toastr.error('Cannot insert data, Employee code already exists')", true);

        }



        blschedule = null;
    }
    #endregion

    #region Events
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                Page.Header.DataBind();
                //blMessage.Items.Clear();
                hfUser.Value = Request.QueryString["uid"];
                hfClient.Value = Request.QueryString["clnt"];
                ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "ClearAdd();", true);
                divAdd.Style.Add("display", "block");
                CheckUserAccess();
                BindData();
                GridFill(1);
                //FillStore();
                SetControls();
                rdProcessing.Checked = true;
                ProcessingorPosting();

            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "toastr.warning('" + ex.Message + "')", true);
        }
    }
    protected void Page_Changed(object sender, EventArgs e)
    {
        //blMessage.Items.Clear();
        int pageIndex = int.Parse((sender as LinkButton).CommandArgument);

        GridFill(pageIndex);
    }
    protected void imgbtnView_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            //blMessage.Items.Clear();
            divAdd.Style.Add("display", "none");
            divView.Style.Add("display", "block");
            ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "ClearAdd();", true);
            GridFill(1);
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "toastr.warning('" + ex.Message + "')", true);
        }
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        try
        {
            //blMessage.Items.Clear();
            GridFill(1);
            //  txtSearchEmployeeCode.Focus();
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "toastr.warning('" + ex.Message + "')", true);
        }
    }
    //protected void gvEmployee_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    //{
    //    try
    //    {
    //        ////////blMessage.Items.Clear();
    //        //////ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "ShowAddDiv();", true);
    //        //////BlEmployee blEmployee = new BlEmployee();
    //        //////BlFunction blFunction = new BlFunction();
    //        //////blEmployee.MasterId = Convert.ToInt64(gvEmployee.DataKeys[e.NewSelectedIndex]["ID_Employee"].ToString());
    //        //////hfScheduleId.Value = blEmployee.MasterId.ToString();
    //        //////blEmployee.FK_Client = Convert.ToInt64(hfClient.Value);
    //        //////blEmployee.UserCode = hfUser.Value;
    //        //////blEmployee.SelectData();

    //        //////// txtName.Text = blEmployee.Name.ToString();
    //        //////// txtAddress.Text = blEmployee.Address;
    //        //////// txtPhone.Text = blEmployee.Phone;
    //        //////// txtEmail.Text = blEmployee.Email;

    //        //////// txtDob.Text = blEmployee.Dob.ToString("dd/MM/yyyy");
    //        ////////// ddlSalesMan.SelectedValue = blEmployee.FK_Salesman.ToString();
    //        //////// ChkSalesMan.Checked = blEmployee.Salesman;

    //        //////// chkActive.Checked = blEmployee.Active;


    //        ////////DataTable dt = blFunction.GetDataTable("StoreAccess", "FK_Store,IsDefault", "FK_Employee=" + hfEmployeeId.Value.ToString() + " AND FK_Client=1", " ", " ");
    //        ////////for (int i = 0; i < dt.Rows.Count; i++)
    //        ////////{
    //        ////////    for (int j = 0; j < chklistStores.Items.Count; j++)
    //        ////////    {

    //        ////////        if (chklistStores.Items[j].Value == dt.Rows[i]["FK_Store"].ToString())
    //        ////////        {
    //        ////////            chklistStores.Items[j].Selected = true;
    //        ////////        }

    //        ////////    }
    //        ////////    if (Convert.ToBoolean(dt.Rows[i]["IsDefault"]).ToString() == "True")
    //        ////////    {
    //        ////////        ddlDefaultStore.SelectedValue = dt.Rows[i]["FK_Store"].ToString();
    //        ////////    }
    //        ////////}
    //        //////blEmployee = null;
    //    }
    //    catch (Exception ex)
    //    {
    //        ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "toastr.warning('" + ex.Message + "')", true);
    //    }
    //}
    //protected void gvEmployee_RowDeleting(object sender, GridViewDeleteEventArgs e)
    //{
    //    try
    //    {
    //        // blMessage.Items.Clear();
    //        //string confirmValue = Request.Form["confirm_value"];
    //        //if (confirmValue.Length > 3)
    //        //    confirmValue = confirmValue.Substring(confirmValue.Length - 3, 3);
    //        //if (confirmValue == "Yes")
    //        //{
    //        //    long status = 0;
    //        //    BlEmployee blEmployee = new BlEmployee();
    //        //    blEmployee.MasterId = Convert.ToInt64(gvEmployee.DataKeys[e.RowIndex]["ID_Employee"].ToString());
    //        //    blEmployee.FK_Client = Convert.ToInt64(hfClient.Value);
    //        //    blEmployee.UserCode = hfUser.Value;
    //        //    if (hfIsDelete.Value.ToLower() == "true")
    //        //    {
    //        //        status = blEmployee.DeleteData();
    //        //        if (status > 0)
    //        //        {
    //        //            GridFill(1);
    //        //            ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "toastr.success('Deleted successfully')", true);
    //        //        }
    //        //        else
    //        //        {
    //        //            ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "toastr.error('Cannot delete, reference exists')", true);

    //        //        }
    //        //    }
    //        //    else
    //        //    {
    //        //        ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "toastr.warning('User does not have permission to Delete')", true);

    //        //    }
    //        //    blEmployee = null;
    //        //}
    //    }
    //    catch (Exception ex)
    //    {
    //        ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "toastr.warning('" + ex.Message + "')", true);
    //    }
    //}

    protected void btnImport_Click(object sender, EventArgs e)
    {

    }
    private void ProcessingorPosting()
    {
        if (rdProcessing.Checked == true)
        {
            DivImportData.Visible = false;
            DivProcessingDataGrid.Visible = true;
            GvProcessing.DataSource = null;
            GvProcessing.DataBind();
        }
        else if (rdPosting.Checked == true)
        {
            DivImportData.Visible = true;
            DivProcessingDataGrid.Visible = false;
            grvExcelData.DataSource = null;
            grvExcelData.DataBind();
        }
        ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "ShowAddDiv();", true);
        ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "ShowAddDiv();", true);

    }
    protected void rdProcessing_CheckedChanged(object sender, EventArgs e)
    {
        try
        {
            ProcessingorPosting();

        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "toastr.warning('" + ex.Message + "')", true);
        }
    }
    protected void rdPosting_CheckedChanged(object sender, EventArgs e)
    {
        try
        {

            ProcessingorPosting();

        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "toastr.warning('" + ex.Message + "')", true);
        }
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            // SaveData();
            if (rdProcessing.Checked == true)
            {
                if (GvProcessing.Rows.Count > 0)
                {
                    SaveProcessData();
                }
            }
            else if (rdPosting.Checked == true)
            {
                if (grvPostingData.Rows.Count > 0)
                {
                    SavePostingData();
                }
            }


        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "toastr.warning('" + ex.Message + "')", true);
        }
    }
    protected void btnProcess_Click(object sender, EventArgs e)
    {
        try
        {
            if (rdProcessing.Checked == true)
            {
                if (checkAlreadyprocessed() == false)
                {
                    txtScheduleFrom.Enabled = false;
                    txtScheduleUpto.Enabled = false;
                    ProcessingData();
                }
                else
                {

                    ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "toastr.warning('" + "Schedule Already processed " + "')", true);
                    // ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "", true);

                }
            }
            else if (rdPosting.Checked == true)
            {
                try
                {

                    string connString = "";
                    string fileName = Path.GetFileName(fileuploadExcel.PostedFile.FileName);
                    fileuploadExcel.PostedFile.SaveAs(Server.MapPath("~/Excel/") + fileName);
                    string strFileType = System.IO.Path.GetExtension(fileuploadExcel.FileName).ToLower();
                    string path = Server.MapPath("Excel");
                    path = path + "/" + fileuploadExcel.PostedFile.FileName;

                    //Connection String to Excel Workbook
                    if (strFileType.Trim() == ".xls")
                    {
                        connString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + path + ";Extended Properties=\"Excel 8.0;HDR=Yes;IMEX=2\"";
                    }
                    else if (strFileType.Trim() == ".xlsx")
                    {
                        connString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + path + ";Extended Properties=\"Excel 12.0;HDR=Yes;IMEX=2\"";
                    }
                    string query = "SELECT CusNumber,RecoveredAmount FROM [Sheet1$]";
                    OleDbConnection conn = new OleDbConnection(connString);
                    //  if (conn.State == ConnectionState.Closed)
                    //   conn.Open();
                    OleDbCommand cmd = new OleDbCommand(query, conn);
                    OleDbDataAdapter da = new OleDbDataAdapter(cmd);
                    DataSet ds = new DataSet();
                    da.Fill(ds);
                    grvExcelData.DataSource = ds.Tables[0];
                    grvExcelData.DataBind();
                    DataTable dtbl = new DataTable();
                    dtbl = ds.Tables[0];
                    ViewState["Posting"] = dtbl;
                    da.Dispose();
                    conn.Close();
                    conn.Dispose();
                    SaveScheduleImport();
                    SchedulePostingSelect();






                }
                catch (Exception ex)
                {
                    ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "toastr.warning('" + ex.Message + "Excel parameter Should be CusNumber and RecoveredAmount" + "')", true);
                    ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "toastr.warning('" + "Excel parameter Should be CusNumber and RecoveredAmount" + "')", true);

                }

            }




        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "toastr.warning('" + ex.Message + "')", true);
        }
    }
    protected void SchedulePostingSelect()
    {
        DataTable dt = new DataTable();
        BlSchedule blschedule = new BlSchedule();
        dt = blschedule.SelectSchedulePosting();
        grvPostingData.DataSource = dt;
        grvPostingData.DataBind();
        ViewState["Posting"] = dt;

    }
    protected Boolean checkAlreadyprocessed()
    {
        DataTable dt = new DataTable();
        BlSchedule blschedule = new BlSchedule();
        blschedule.FK_Client = Convert.ToInt64(hfClient.Value);
        blschedule.UserCode = hfUser.Value;

        blschedule.ScheduleUpto = DateTime.ParseExact(txtScheduleUpto.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
        blschedule.PageSize = 100;


        dt = blschedule.checkAlreadyprocessed();
        if (dt.Rows.Count > 0)
        {
            return true;
        }
        else
        {
            return false;
        }




    }
    protected void GvProcessing_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {

            int RowSpan = 2;
            for (int i = GvProcessing.Rows.Count - 2; i >= 0; i--)
            {
                GridViewRow currRow = GvProcessing.Rows[i];
                GridViewRow prevRow = GvProcessing.Rows[i + 1];
                if (currRow.Cells[0].Text == prevRow.Cells[0].Text)
                {
                    currRow.Cells[0].RowSpan = RowSpan;
                    prevRow.Cells[0].Visible = false;
                    currRow.Cells[1].RowSpan = RowSpan;
                    prevRow.Cells[1].Visible = false;
                    currRow.Cells[7].RowSpan = RowSpan;
                    prevRow.Cells[7].Visible = false;
                    //  currRow.Cells[8].RowSpan = RowSpan;
                    //  prevRow.Cells[8].Visible = false;

                    RowSpan += 1;
                }
                else
                {
                    RowSpan = 2;
                }
            }

            //if (e.Row.RowType == DataControlRowType.DataRow)
            //{

            //    Label Salary = (Label)e.Row.FindControl("lblSalary");

            //    //Label lblUnitsInStock = (Label)e.Row.FindControl("lblUnitsInStock");

            //    m = m + decimal.Parse(Salary.Text);

            //    //Table tb = new Table();

            //}

            //if (e.Row.RowType == DataControlRowType.Footer)
            //{

            //    Label lblTotalPrice = (Label)e.Row.FindControl("Salary");

            //    lblTotalPrice.Text = m.ToString();

            //}
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "toastr.warning('" + ex.Message + "')", true);
        }
    }

    protected void GvProcessing_PreRender(object sender, EventArgs e)
    {
        try
        {
            // MergeRows(GvProcessing);
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "toastr.warning('" + ex.Message + "')", true);
        }
    }
    #endregion


    protected void btnScheduleProcessTrigger_Click(object sender, EventArgs e)
    {
        try
        {
            //string confirmValue = Request.Form["confirm_value"];
            //if (confirmValue.Length > 3)
            //    confirmValue = confirmValue.Substring(confirmValue.Length - 3, 3);
            //if (confirmValue == "Yes")
            //{


            //}
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "toastr.warning('" + ex.Message + "')", true);
        }
    }
    protected void gvSchedule_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {

            string confirmValue = Request.Form["confirm_value"];
            if (confirmValue.Length > 3)
                confirmValue = confirmValue.Substring(confirmValue.Length - 3, 3);
            if (confirmValue == "Yes")
            {
                long status = 0;
                BlSchedule BlSchedule = new BlSchedule();
                BlSchedule.MasterId = Convert.ToInt64(gvSchedule.DataKeys[e.RowIndex]["ID_Schedule"].ToString());
                BlSchedule.FK_Client = Convert.ToInt64(hfClient.Value);
                BlSchedule.UserCode = hfUser.Value;
                if (hfIsDelete.Value.ToLower() == "true")
                {
                    status = BlSchedule.DeleteData();
                    if (status > 0)
                    {
                        GridFill(1);
                        ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "toastr.success('Deleted successfully')", true);
                    }
                    else
                    {
                        ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "toastr.error('Cannot delete, reference exists')", true);

                    }
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "toastr.warning('User does not have permission to Delete')", true);

                }
                BlSchedule = null;
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "toastr.warning('" + ex.Message + "')", true);
        }
    }
    protected void gvSchedule_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        try
        {
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "toastr.warning('" + ex.Message + "')", true);
        }
    }
    protected void btnClear_Click(object sender, EventArgs e)
    {
        try
        {

            ClearAll();
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "toastr.warning('" + ex.Message + "')", true);
        }
    }
    protected void gvSchedule_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            //


            if (e.CommandName == "Print")
            {
                //string confirmValue = Request.Form["confirm_value"];
                //  if (confirmValue.Length > 3)
                //      confirmValue = confirmValue.Substring(confirmValue.Length - 3, 3);
                //  if (confirmValue == "Yes")
                //  {

                GridViewRow row = (GridViewRow)((ImageButton)e.CommandSource).NamingContainer;
                int index = row.RowIndex;
                string ScheduleId = gvSchedule.DataKeys[index]["ID_Schedule"].ToString();
                string ScheduleType = gvSchedule.DataKeys[index]["ScheduleType"].ToString();
                if (ScheduleType == "True")
                {
                    string popupreport = "<script> window.open('ScheduleReport.aspx?ScheduleId=" + ScheduleId + "', 'self', 'left=200,top=50,width=900,height=680,status=0,resizable=0,fullscreen=0,scrollbars=1');</script>";
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "Success", popupreport, false);
                }
                if (ScheduleType == "False")
                {

                    string popupreport = "<script> window.open('SchedulePostingReport.aspx?ScheduleId=" + ScheduleId + "', 'self', 'left=200,top=50,width=900,height=680,status=0,resizable=0,fullscreen=0,scrollbars=1');</script>";
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "Success", popupreport, false);

                }
            }



        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "toastr.warning('" + ex.Message + "')", true);
        }
    }
}

































