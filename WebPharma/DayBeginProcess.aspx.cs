using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebPharma.Business;
using WebPharma.Business.Masters;
using System.Data;
using WebPharma.Business.WebPharma;
using System.Collections;

public partial class DayBeginProcess : System.Web.UI.Page
{    private const string DIVCOUNT = "DivCount";
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
                steparametetrs();
               CheckUserAccess();
                BindData();
                txtopnBalanceDate.Text = DateTime.Now.ToShortDateString();
                txtopnBalanceDate.Attributes.Add("readonly", "readonly");
            }
          
            //ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "ClearAdd();", true);
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
        bluseraccess.FK_Pages = 87;
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
    protected void ddlBranch_SelectedIndexChanged(object sender, EventArgs e)
    {
        SetCashBalance();
    }
    protected void gdvAttendance_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow || e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.Cells[3].Visible = false;
        }
    }
    protected void gdvMessage_RowDataBound(object sender, GridViewRowEventArgs e)
    {

    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        BlFunction blfunctions = new BlFunction();
        if (btnSave.Text == "Add"  && DivCount==11)
        {
            btnSave.Text = "Next";
                DivCount = 1;
            divdaybegin.Style.Add("display", "none");
            divbranch.Style.Add("display", "block");
        }
        else if (btnSave.Text == "Add" && DivCount == 10)
        {
            divaccountlinking.Style.Add("display", "none");

            divdaybegin.Style.Add("display", "none");
            divbranch.Style.Add("display", "none");
            lblFinishMessage.Text = "NO PERMISSION  FOR DAY BEGIN PROCESS";
            divdayfinished.Style.Add("display", "block");
            btnSave.Text = "Close";
        }
        else if (String.Compare(btnSave.Text, "Close") == 0)  
        {
           PageRedirection();
        }
        else if (String.Compare(btnSave.Text, "Finish") == 0)
        {
         //   SaveData();
           // PageRedirection();
            PageRedirection();
        }
     else  if (Convert.ToByte(ViewState["DivCount"]) == 1)
            {

              string id_DayBegin = blfunctions.GetScalarValue("DayBegin db", "isnull(id_DayBegin,0)", "db.DbDate='" + Common.DateFormat(Convert.ToDateTime(txtopnBalanceDate.Text)) + "' AND db.TransMode='CPDPDB' AND db.BranchCode=" + Convert.ToInt16(ddlBranch.SelectedValue) + "", "");
              if ((id_DayBegin == "" ? 0 : Convert.ToInt64(id_DayBegin)) == 0)
              {


                  if (validatefield())
                  {
                      gdvAttendance.DataSource = blfunctions.GetDataTable("Users", "ID_Users as ID_User,Name as UserName,Username as UserCode", "Active=1 ", "", "");
                      gdvAttendance.DataBind();
                      btnSave.Text = "Process";
                      divbranch.Style.Add("display", "none");
                      div1.Style.Add("display", "block");
                      DivCount = 2;

                  }
              }
              else
              {

                       divbranch.Style.Add("display", "none");
                        btnSave.Text = "Close";
                        divdayfinished.Style.Add("display", "block");
                        lblFinishMessage.Text = "Day Begin Already Done";
                  
              }


        }
        else if (Convert.ToByte(ViewState["DivCount"]) == 2)
        {
            btnSave.Text = "Finish";

            div1.Style.Add("display", "none");
            //divgrids.Style.Add("display", "none");
            divdayfinished.Style.Add("display", "block");
            SaveData();

            //btnSave.Text = "Process";
            //div1.Style.Add("display", "none");
            //divgrids.Style.Add("display", "block");
            //DivCount = 3;
            
           // trAttandanceMarking.Visible = true;
        
        }
        //else if (Convert.ToByte(ViewState["DivCount"]) == 3)
        //{

        //    btnSave.Text = "Finish";
        //    divgrids.Style.Add("display", "none");
        //    divdayfinished.Style.Add("display", "block");
    
        //}

    }
    protected void BindData()
    {
        Populate populate = new Populate();
        populate.FillDropdownList(ddlBranch, "Store", "StoName", "ID_Store", "StoName", "     Active='True'   and FK_Client = " + hfClient.Value, "");
        //populate.FillDropdownList(ddlCategory, "CategoryFirst", "Name", "ID_CategoryFirst", "Name", "  FK_Client= " + hfClient.Value.Split(',')[0], "");
        populate = null;
        ddlBranch.Focus();
    }
    //private DataTable initialiseDataTable()
    //{
    //    DataTable dt = new DataTable();
    //    dt.Columns.Add(new DataColumn("SlNo", typeof(int)));
    //    dt.Columns.Add(new DataColumn("Progress", typeof(String)));
    //    dt.Columns.Add(new DataColumn("Description", typeof(string)));
    //    dt.Columns.Add(new DataColumn("Status", typeof(string)));
    //    dt.Columns.Add(new DataColumn("Message", typeof(string)));
    //    dt.Columns.Add(new DataColumn("RowAffected", typeof(string)));
    //    dt.Columns.Add(new DataColumn("ErrCode", typeof(string)));
    //    dt.Columns.Add(new DataColumn("ID", typeof(string)));
    //    dt.Columns.Add(new DataColumn("DesError", typeof(string)));

    //    return dt;
    //}

    //private DataTable GetDayProcessDetail()
    //{
    //    BlDayBegin BlDayBegin = new BlDayBegin();
    //    //DataTable dt = BlDayBegin.DayProcessValidate(ViewState["DBNAME"].ToString(), "CPDPDB", Convert.ToInt16(ddlBranch.SelectedValue), Convert.ToDateTime(txtopnBalanceDate.Text), Convert.ToBoolean(ViewState["HEADOFFICE"]), 0);
    //    DataTable dt = BlDayBegin.DayProcessValidate("CPDPDB", Convert.ToInt16(ddlBranch.SelectedValue), Convert.ToDateTime(txtopnBalanceDate.Text), 0);
    //    BlDayBegin = null;
    //    DataTable currenttable = new DataTable();
    //    currenttable = initialiseDataTable();

    //    currenttable.Columns[0].AutoIncrement = true;
    //    currenttable.Columns[0].AutoIncrementSeed = 1;
    //    currenttable.Columns[0].ReadOnly = true;
    //    foreach (DataRow dr in dt.Rows)
    //    {
    //        DataRow drc = currenttable.NewRow();

    //        drc["Description"] = dr["Description"].ToString();
    //        drc["Progress"] = ResolveUrl("~/Images/ProgressBar/Progess.gif");
    //        drc["Status"] = dr["Status"].ToString();
    //        drc["Message"] = "";
    //        drc["RowAffected"] = 0;
    //        drc["ErrCode"] = dr["ErrCode"].ToString();
    //        drc["ID"] = dr["ID"].ToString();
    //        drc["DesError"] = dr["DesError"].ToString();


    //        currenttable.Rows.Add(drc);

    //    }
    //    ViewState["currenttable"] = currenttable;
    //    return currenttable;


    //}
    protected void chkHeader_CheckedChanged(object sender, EventArgs e)
    {
        CheckBox chk = new CheckBox();
        chk = (CheckBox)gdvAttendance.HeaderRow.FindControl("chkHeader");

        if (chk.Checked == false)
        {
            for (int i = 0; i < gdvAttendance.Rows.Count; i++)
            {
                chk = (CheckBox)gdvAttendance.Rows[i].Cells[1].FindControl("chkMark");
                chk.Checked = false;
            }
        }
        else
        {
            for (int i = 0; i < gdvAttendance.Rows.Count; i++)
            {
                chk = (CheckBox)gdvAttendance.Rows[i].Cells[1].FindControl("chkMark");
                chk.Checked = true;
            }
        }
    }
    private void SaveData()
    {
        BlDayBegin blDayBegin = new BlDayBegin();
      
        ArrayList arlist = new ArrayList();
        CheckBox chkbox = new CheckBox();
        long status = 0;

        blDayBegin.UserCode = hfUser.Value;
        blDayBegin.FK_Client = Convert.ToInt64(hfClient.Value);
        blDayBegin.BranchCode = Convert.ToInt16(ddlBranch.SelectedValue);
        blDayBegin.TransMode = "CPDPDB";
        blDayBegin.OfflineEntry = false;
      
       blDayBegin.DbDate = Convert.ToDateTime(txtopnBalanceDate.Text);
       blDayBegin.openingbalance = Convert.ToDecimal(txtCashBalance.Text);
        blDayBegin.openingliability = Convert.ToDecimal(Txtliability.Text);
        blDayBegin.AmntDifference = Convert.ToDecimal(txtdifference.Text);

        foreach (GridViewRow gdvRow in gdvAttendance.Rows)
        {
            chkbox = (CheckBox)gdvRow.Cells[0].FindControl("ChkMark");
            if (chkbox.Checked == true)
            {
                BlAttendancedetails blAttendancedetails = new BlAttendancedetails();
                blAttendancedetails.AttUser = gdvRow.Cells[3].Text.ToString();
                blAttendancedetails.AttDate = Convert.ToDateTime(DateTime.Now.ToShortDateString());
                blAttendancedetails.AttTime = Convert.ToDateTime(DateTime.Now.ToShortTimeString());
                blAttendancedetails.ID_Attendance = 0;
                blAttendancedetails.UserCode = hfUser.Value;
                blAttendancedetails.BranchCode = Convert.ToInt64(ddlBranch.SelectedValue);
                arlist.Add(blAttendancedetails);
                blAttendancedetails = null;
            }

        }

        blDayBegin.XmlAttendance = Common.WriteToXml(arlist);    

        //blDayBegin.XmlAttendance = BlFormats.WriteToXml(UpdateAttendance());
      //  blDayBegin.MasterID = ID_CommonProcess;
       // blDayBegin.UserAction = Convert.ToByte(UserAction);
     //   blDayBegin.DbName = ViewState["DBNAME"].ToString();
     //   blDayBegin.BranchCodeUser = Convert.ToInt16(ViewState["BRANCHCODEUSER"].ToString().ToString());
       // blDayBegin.MachineCode = Convert.ToInt32(ViewState["MACHINECODE"].ToString());
      //  blDayBegin.XmlAttendance = BlFormats.WriteToXml(UpdateAttendance());

        if (hdfDaybegin.Value == string.Empty)
        {
            if (hfIsAdd.Value.ToLower() == "true")
            {
                status = blDayBegin.InsertData();
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



    }


    protected void btnProcess_Click(object sender, EventArgs e)
    {
    }
  
    protected void btnScriptFill_Click(object sender, EventArgs e)
    {
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
            divdaybegin.Style.Add("display", "none");
            DivCount = 10;
        }
        else
        {
            divaccountlinking.Style.Add("display", "none");
            divdaybegin.Style.Add("display", "block");
            DivCount = 11;
        }



       // divdaybegin.Style.Add("display", "block");
       divbranch.Style.Add("display", "none");
       divdayfinished.Style.Add("display", "none");
       div1.Style.Add("display", "none");
       divgrids.Style.Add("display", "none");
     //  divattendance.Style.Add("display", "none");
   
     

     
        
           
    }

    protected void SetCashBalance()
    {
        BlFunction blFunctions = new BlFunction();
        //txtCashBalance.Text = blFunctions.GetScalarValue("Store", "ISNULL( OpeningBalance,0) OpeningBalance", " ID_Store=" + Convert.ToInt64(ddlBranch.SelectedValue) + "", "");

        txtCashBalance.Text = blFunctions.GetScalarValue("Store", "ISNULL( OpeningBalance,0) OpeningBalance", " ID_Store=" + Convert.ToInt64(ddlBranch.SelectedValue) + "", "");
        Txtliability.Text = blFunctions.GetScalarValue("Store", "ISNULL( OpeningLiability,0) OpeningLiability", " ID_Store=" + Convert.ToInt64(ddlBranch.SelectedValue) + "", "");
       // txtCashBalance.Text = blFunctions.GetDataTable("Store", "ISNULL( OpeningBalance,0) OpeningBalance,ISNULL( OpeningLiability,0) OpeningLiability", " ID_Store=" + Convert.ToInt64(ddlBranch.SelectedValue) + "", "","");
        txtdifference.Text = ((Convert.ToDecimal(txtCashBalance.Text)) - (Convert.ToDecimal(Txtliability.Text))).ToString();
        txtCashBalance.Enabled = false;
        Txtliability.Enabled = false;
        txtdifference.Enabled = false;
        blFunctions = null;
       
    }
    
    public void PageRedirection()
    {
        Response.Redirect("Default.aspx");
    }
    protected bool validatefield()
    {
        bltErrorList.Items.Clear();
        if (ddlBranch.SelectedValue == "0")
        {
            bltErrorList.Items.Add("*Select Branch Type");
            return false;
        }


        return true;
    }
    //protected DataTable PreviouusDayEndCheck()
    //{
    //    BlDayBegin BlDayBegins = new BlDayBegin();
    //    DataTable dt = new DataTable();
    //    BlDayBegins.DbDate = Convert.ToDateTime(txtopnBalanceDate.Text);
    //   //  BlDayBegins.MasterID = ID_CommonProcess;

    //   // BlDayBegins.DbName = ViewState["DBNAME"].ToString();
    //   // BlDayBegins.BranchCodeUser = Convert.ToInt16(ViewState["BRANCHCODEUSER"].ToString().ToString());
    //    BlDayBegins.BranchCode = Convert.ToInt16(ddlBranch.SelectedValue);
    //    BlDayBegins.UserCode = hfUser.Value;

    //    BlDayBegins.TransMode = "CPDPDB";
    //  //  BlDayBegins.MachineCode = Convert.ToInt32(ViewState["MACHINECODE"].ToString());
    //    dt = BlDayBegins.PreviouusDayEndCheck();
    //    return dt;
    //}
}

