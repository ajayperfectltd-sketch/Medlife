/*----------------------------------------------------------------------------------------------------------------------------------------------------------
Purpose    : Insert/Update/Delete Counter  
Author     : Aiswarya
Copyright  : Perfect Software Solutions (Clt) Pvt Ltd
Created on : 31/05/2018
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
using WebPharma.Business.Masters;
using System.Data;
using WebPharma.Business;
using System.Collections;
using WebPharma.Interface.General;

public partial class Counter : System.Web.UI.Page
{
    #region Functions
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
            BlCounter blCounter = new BlCounter();
            //blReason.MasterId = Convert.ToInt64(gvReason.DataKeys[e.RowIndex]["ID_Reason"].ToString());
            blCounter.UserCode = hfuserName.Value;
            blCounter.MasterId = Convert.ToInt64(hfMasterID.Value);
            blCounter.FK_Reason = Convert.ToInt64(Reason);
            if (hfIsDelete.Value.ToLower() == "true")
            {
                status = blCounter.DeleteData();
                if (status > 0)
                {
                    GridFill(1);
                    ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "toastr.success('Data deleted successfully')", true);
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "toastr.error('Cannot delete data, reference exists')", true);
                }
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "toastr.warning('User does not have permission to Delete')", true);
                txtSearchName.Focus();
            }
            blCounter = null;
        }
    }
    protected void BindData()
    {
        Populate populate = new Populate();
       // populate.FillDropdownList(ddlSearchStore, "Store", "StoName", "ID_Store", "ID_Store", "  Active='True' AND FK_Client= " + hfClient.Value, "");
        populate.FillDropdownList(ddlStore, "Store", "StoName", "ID_Store", "ID_Store", "  Active='True' AND FK_Client= " + hfClient.Value+"AND Cancelled=0", "");
       // populate.FillDropdownList(ddlBillType, "BillType", "TypeName", "ID_BillType", "ID_BillType", " FK_Client= " + hfClient.Value, "");
        populate = null;
    }
    protected void SetControls()
    {
       // txtDob.Attributes.Add("readOnly", "readOnly");
    }
    protected void GridFill(int currentPage)
    {
        BlCounter blCounter = new BlCounter();
        DataTable dtbl = new DataTable();
        blCounter.Name = txtSearchName.Text.Trim();
        blCounter.FK_Client = Convert.ToInt64(hfClient.Value);
        blCounter.PageIndex = Convert.ToInt16(currentPage);
        blCounter.UserCode = hfUser.Value;
        dtbl = blCounter.SelectAllData();
        gvCounter.DataSource = dtbl;
        gvCounter.DataBind();
        blCounter = null;
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
    protected void CheckUserAccess()
    {
        BlUserAccess bluseraccess = new BlUserAccess();
        Populate populate = new Populate();

        bluseraccess.FK_Users = Convert.ToInt64(hfUser.Value);
        bluseraccess.FK_Pages = 16;
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
    protected void SaveData()
    {
        long status = 0;
        BlCounter blCounter = new BlCounter();
      
        blCounter.Name = txtName.Text;
        blCounter.FK_Client = Convert.ToInt64(hfClient.Value);
        blCounter.UserCode = hfuserName.Value;
        blCounter.FK_Store = Convert.ToInt64(ddlStore.SelectedValue.ToString());
        blCounter.Active = Convert.ToBoolean(chkActive.Checked);
        blCounter.FK_AccountHead = Convert.ToInt64(txtRefno.Text == "" ? "0" : txtRefno.Text);
        blCounter.FK_AccountHeadSub = Convert.ToInt64(txtRefno1.Text == "" ? "0" : txtRefno1.Text);
        ArrayList arlist = new ArrayList();
        DataTable dtbl = (DataTable)ViewState["TempTable"];
          IShiftDetails iCounter;
        if (grdShiftdetails.Rows.Count > 0)
        {
            for (int i = 0; i < dtbl.Rows.Count; i++)
            {
                iCounter = new IShiftDetails();
                iCounter.FK_Shift = Convert.ToInt64(dtbl.Rows[i]["FK_Shift"].ToString());
                iCounter.ShiftName = (dtbl.Rows[i]["ShiftName"].ToString());
                arlist.Add(iCounter);
                iCounter = null;
            }
        }
           
        blCounter.XmlShiftDetails = Common.WriteToXml(arlist);
        //blCounter.Active = cbxAcctive.Checked;
        if (hfCounterId.Value == "0")
        {
            if (hfIsAdd.Value.ToLower() == "true")
            {
                status = blCounter.InsertData();
                if (status > 0)
                {
                    ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "ShowAddDiv();", true);
                    ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "toastr.success('Data inserted successfully');ClearAdd();", true);

                    GridFill(1);

                    grdShiftdetails.DataSource = null;
                    grdShiftdetails.DataBind();
                    ViewState["TempTable"] = null;
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "ShowAddDiv();", true);
                    ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "toastr.error('Cannot insert data, Name already exists')", true);
                    txtName.Focus();
                }
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "toastr.warning('User does not have permission to add')", true);
                txtName.Focus();

            }
        }
        else
        {
            if (hfIsUpdate.Value.ToLower() == "true")
            {
                blCounter.MasterId = Convert.ToInt64(hfCounterId.Value);
                status = blCounter.UpdateData();
                if (status > 0)
                {
                    ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "ShowAddDiv();", true);
                    ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "toastr.success('Data updated successfully');ClearAdd();", true);
                    GridFill(1);
                    grdShiftdetails.DataSource = null;
                    grdShiftdetails.DataBind();
                    ViewState["TempTable"] = null;
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "ShowAddDiv();", true);
                    ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "toastr.error('Cannot update data, Name already exists')", true);
                    txtName.Focus();
                }
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "toastr.warning('User does not have permission to Update')", true);
               
            }
        }
        blCounter = null;
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
                ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "ClearAdd();", true);
                hfUser.Value = Request.QueryString["uid"];
                hfClient.Value = Request.QueryString["clnt"];
                divAdd.Style.Add("display", "block");
                CheckUserAccess();
                BindData();
                GridFill(1);
                hfuserName.Value = Session["Username"].ToString();
                SetControls();
                txtName.Focus();
                grdShiftdetails.DataSource = null;
                grdShiftdetails.DataBind();
                SetReadonly(txtShift);
                if (Convert.ToBoolean(hfIsAdd.Value) == false)
                {
                    ((Label)CommonPopupPrivilege.FindControl("popuplabel")).Text = "User does not have permission to Add";
                    mpPrivilege.Show();
                }
                if (Convert.ToBoolean(hfIsUpdate.Value) == false)
                {
                    gvCounter.Columns[3].Visible = false;
                }
                if (Convert.ToBoolean(hfIsDelete.Value) == false)
                {
                    gvCounter.Columns[4].Visible = false;
                }
               
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "toastr.warning('" + ex.Message + "')", true); 
        }
    }
    public void SetReadonly(params TextBox[] textbox)
    {
        foreach (TextBox txt in textbox)
            txt.Attributes.Add("readonly", "readonly");

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
           // blMessage.Items.Clear();
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
           // blMessage.Items.Clear();
            GridFill(1);
            txtSearchName.Focus();
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "toastr.warning('" + ex.Message + "')", true); 
        }
    }
    protected void gvCounter_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        try
        {
            BlFunction blfunctions = new BlFunction();
            //blMessage.Items.Clear();
            ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "ShowAddDiv();", true);
            BlCounter blCounter = new BlCounter();
            blCounter.MasterId = Convert.ToInt64(gvCounter.DataKeys[e.NewSelectedIndex]["ID_Counters"].ToString());
            blCounter.FK_Client = Convert.ToInt64(hfClient.Value);
            blCounter.UserCode = hfUser.Value;
            hfCounterId.Value = blCounter.MasterId.ToString();
            blCounter.SelectData();
            txtName.Text = blCounter.Name.ToString();
            chkActive.Checked = blCounter.Active;
            //cbxAcctive.Checked = blCounter.Active;
            try
            {
                ddlStore.SelectedValue = blCounter.FK_Store.ToString();
            }
            catch (Exception ex)
            {
                ddlStore.SelectedValue = "0";
            }
           
           DataTable  dtbl = blCounter.selectCounterShiftDetails();
           grdShiftdetails .DataSource = dtbl;
           grdShiftdetails.DataBind();
            ViewState["TempTable"] = dtbl;
            txtRefno.Text = blCounter.FK_AccountHead.ToString();
            if (Convert.ToInt64(txtRefno.Text) > 0)
            {
                txtAccountHead.Text = blfunctions.GetScalarValue(
                    "AccountHead",
                    "ISNULL(HeadName,'') Name",
                    " FK_Client=" + hfClient.Value.Split(',')[0] +
                    " AND Cancelled=0 AND AhCode=" + Convert.ToInt64(txtRefno.Text),
                    "");
            }
            BlFunction blfunctions1 = new BlFunction();
            txtRefno1.Text = blCounter.FK_AccountHeadSub.ToString();
            if (Convert.ToInt64(txtRefno1.Text) > 0)
            {
                txtAccountHeadSub.Text = blfunctions1.GetScalarValue(
                    "AccountHeadSub",
                    "ISNULL(HeadSubName,'') Name",
                    " FK_Client=" + hfClient.Value.Split(',')[0] +
                    " AND Cancelled=0 AND AhsCode=" + Convert.ToInt64(txtRefno1.Text),
                    "");
            }
            blCounter = null;
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "toastr.warning('" + ex.Message + "')", true); 
        }
    }
    protected void gvCounter_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            //blMessage.Items.Clear();
            string confirmValue = Request.Form["confirm_value"];
            if (confirmValue.Length > 3)
                confirmValue = confirmValue.Substring(confirmValue.Length - 3, 3);
            if (confirmValue == "Yes")
            {
                long status = 0;
                BlCounter blCounter = new BlCounter();
                blCounter.MasterId = Convert.ToInt64(gvCounter.DataKeys[e.RowIndex]["ID_Counters"].ToString());
                hfMasterID.Value = (gvCounter.DataKeys[e.RowIndex]["ID_Counters"].ToString());
                blCounter.FK_Client = Convert.ToInt64(hfClient.Value);
                blCounter.UserCode = hfUser.Value;
                blCounter = null;
            }
            else
            {
                mpDelete.Hide();
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
            //blMessage.Items.Clear();
            SaveData();
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "toastr.warning('" + ex.Message + "')", true); 
        }
    }
    #endregion          
    protected void btnGo_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            txtAccountHead.Text = "";
            txtRefno.Text = "";
            txtAccountHeadSub.Text = "";
            txtRefno1.Text = "";
            ((TextBox)CommonPopup1.FindControl("txtName")).Text = txtAccountHead.Text.Trim();
            ((TextBox)CommonPopup1.FindControl("txtRefno")).Text = txtRefno.Text.Trim();
            ((HiddenField)CommonPopup1.FindControl("hfColumn")).Value = "Name";
            AccountHeadFill(1);
            divAdd.Style.Add("display", "block");
            divView.Style.Add("display", "none");



        }
        catch (Exception ex)
        {
        }

    }
    public void AccountHeadFill(int pageIndex)
    {
        txtAccountHead.Text = "";
        txtRefno.Text = "";
        Populate Populate = new Populate();
        CommonPopup1.CallingFunction = "AccountHeadFill";
        string strName = ((TextBox)CommonPopup1.FindControl("txtName")).Text;
        string Refno = ((TextBox)CommonPopup1.FindControl("txtRefno")).Text;
        string searchCol = ((HiddenField)CommonPopup1.FindControl("hfColumn")).Value;
        searchCol = searchCol == string.Empty ? "[Name]" : "[" + searchCol + "]";
        Refno = Refno == string.Empty || Refno == null ? "0" : Refno;

        Populate.FillPopUpGrid((GridView)CommonPopup1.FindControl("gdvSearch"), "AccountHead", "AhCode as ID,HeadName as Name", "ID_AccountHead", "ID_AccountHead", "FK_Client=" + hfClient.Value.Split(',')[0] + "  AND Cancelled=0 AND HeadName ", strName, null);
        Populate = null;
        //  txtSubHeadRefno.Focus();
        if (txtAccountHead.Text == string.Empty)
        {
            lblPopupHeader.Text = "Account Head";
            mpCommon.Show();
        }

    }
    public void SetDetails(string strName, string strRefno)
    {


        DataTable dt = new DataTable();
        DataTable dt1 = new DataTable();
        BlAccountHeadSub blAccountSub = new BlAccountHeadSub();

        if (CommonPopup1.CallingFunction == "AccountHeadFill")
        {
            txtRefno.Text = strRefno;
            txtAccountHead.Text = strName;
            blAccountSub.Ahcode = (strRefno);
            blAccountSub.FK_Client = Convert.ToInt64(hfClient.Value);
            blAccountSub.UserCode = hfUser.Value;
            blAccountSub = null;
        }
        if (CommonPopup1.CallingFunction == "AccountHeadsubFill")
        {
            txtRefno1.Text = strRefno;
            txtAccountHeadSub.Text = strName;
            // chkActive.Focus();
        }
        if (CommonPopup1.CallingFunction == "ShiftFill")
        {
            //txtRefno1.Text = strRefno;
            hfSelectedItem.Value = strRefno.ToString();  
            txtShift.Text = strName;
            // chkActive.Focus();
        }

    }
    protected void ImageButtonheadsub_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            txtAccountHeadSub.Text = "";
            txtRefno1.Text = "";
            //  ClearAccountHead();
            ((TextBox)CommonPopup1.FindControl("txtName")).Text = txtAccountHeadSub.Text.Trim();
            ((TextBox)CommonPopup1.FindControl("txtRefno")).Text = txtRefno1.Text.Trim();
            ((HiddenField)CommonPopup1.FindControl("hfColumn")).Value = "Name";
            AccountHeadsubFill(1);
            divAdd.Style.Add("display", "block");
            divView.Style.Add("display", "none");



        }
        catch (Exception ex)
        {
        }
    }
    public void AccountHeadsubFill(int pageIndex)
    {
        txtAccountHeadSub.Text = "";
        txtRefno1.Text = "";
        Populate Populate = new Populate();
        CommonPopup1.CallingFunction = "AccountHeadsubFill";
        string strName = ((TextBox)CommonPopup1.FindControl("txtName")).Text;
        string Refno = ((TextBox)CommonPopup1.FindControl("txtRefno")).Text;
        string searchCol = ((HiddenField)CommonPopup1.FindControl("hfColumn")).Value;
        searchCol = searchCol == string.Empty ? "[Name]" : "[" + searchCol + "]";
        Refno = Refno == string.Empty || Refno == null ? "0" : Refno;

        Populate.FillPopUpGrid((GridView)CommonPopup1.FindControl("gdvSearch"), "AccountHeadSub AHS LEFT JOIN Customer C ON C.ID_Customer= AHS.Fk_Cuscode AND C.Active=1", "AHS.AhsCode as ID,AHS.HeadSubName as Name", "ID_AccountHeadSub", "ID_AccountHeadSub", "AHS.FK_Client=" + hfClient.Value.Split(',')[0] + "  AND AHS.Cancelled=0 AND HeadSubName ", strName, null);
        Populate = null;
        //  txtSubHeadRefno.Focus();
        if (txtAccountHeadSub.Text == string.Empty)
        {
            lblPopupHeader.Text = "Account HeadSub";
            mpCommon.Show();
        }

    }
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        try
        {
            if (grdShiftdetails.Rows.Count == 0)
            {
                ViewState["TempTable"] = null;
            }
            
            AddData();
            //CheckingCombo();
            divAdd.Style.Add("display", "block");
            divView.Style.Add("display", "none");
            txtShift.Enabled = true;
            IbtnItemAdd.Enabled = true;
            //txtItemQuantity.Enabled = true;
            btnAdd.Enabled = true;
            //}
        }
        catch (Exception ex)
        {
           
            ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "toastr.warning('" + ex.Message + "')", true);
           
        }
    }
    protected void grdShiftdetails_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            string confirmValue = Request.Form["confirm_value"];
            if (confirmValue.Length > 3)
                confirmValue = confirmValue.Substring(confirmValue.Length - 3, 3);
            if (confirmValue == "Yes")
            {
                if (ViewState["TempTable"] != null)
                {
                    DataTable dtbl = (DataTable)ViewState["TempTable"];
                    dtbl.Rows.RemoveAt(e.RowIndex);
                    grdShiftdetails.DataSource = dtbl;
                    grdShiftdetails.DataBind();
                    ViewState["TempTable"] = dtbl;
                    
                }

            }
        }
        catch (Exception ex)
        {
            //blMessage.Items.Add(ex.Message);
        }
        ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "ShowAddDiv();", true);
        divAdd.Style.Add("display", "block");
        divView.Style.Add("display", "none");

    }
    protected void IbtnItemAdd_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            ShiftDetails();
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "toastr.warning('" + ex.Message + "')", true);
        }
    }
    public void ShiftDetails()
    {
        try
        {
            ((TextBox)CommonPopup1.FindControl("txtName")).Text = txtShift.Text.Trim();
            ((TextBox)CommonPopup1.FindControl("txtRefno")).Text = txtShift.Text.Trim();
            ((HiddenField)CommonPopup1.FindControl("hfColumn")).Value = "Name";
            ShiftFill(1);
            divAdd.Style.Add("display", "block");
            divView.Style.Add("display", "none");
        }
        catch (Exception ex)
        {
        }
    }
    public void ShiftFill(int pageIndex)
    {
        //ClearSupplier();
        Populate Populate = new Populate();
        CommonPopup1.CallingFunction = "ShiftFill";
        string strName = ((TextBox)CommonPopup1.FindControl("txtName")).Text;
        string Refno = ((TextBox)CommonPopup1.FindControl("txtRefno")).Text;
        string searchCol = ((HiddenField)CommonPopup1.FindControl("hfColumn")).Value;
        Refno = Refno == string.Empty || Refno == null ? "0" : Refno;

        Populate.FillPopUpGrid((GridView)CommonPopup1.FindControl("gdvSearch"), "ShiftMaster", "ID_ShiftMaster as ID,ShiftName as Name,ShiftFrom,ShiftTo,ShortName", "ID_ShiftMaster", "ID_ShiftMaster", "FK_Client=" + hfClient.Value.Split(',')[0] +"AND Cancelled=0"+ "  AND ShiftName ", strName, null);
        Populate = null;

        if (txtShift.Text == string.Empty)
        {
            lblPopupHeader.Text = "Shift";
            mpCommon.Show();
        }

    }
    protected void AddData()
    {
        DataTable dtbl = new DataTable();
        int rowCount = 0;
        int inCheck = 0;
        if (ViewState["TempTable"] != null)
        {
            dtbl = (DataTable)ViewState["TempTable"];
            rowCount = dtbl.Rows.Count;
        }
        else
        {
            dtbl.Columns.Add("SLNO");
            dtbl.Columns.Add("ShiftName");
            dtbl.Columns.Add("FK_Shift");
            dtbl.Columns.Add("ShiftFrom");
            dtbl.Columns.Add("ShiftTo");
        }
        if (dtbl.Rows.Count > 0)
        {

            for (int i = 0; i < dtbl.Rows.Count; i++)
            {
                if (dtbl.Rows[i]["FK_Shift"].ToString() == hfSelectedItem.Value)
                {
                    inCheck = inCheck + 1;
                    break;
                }
            }
        }
        if (inCheck == 0)
        {
            DataRow drow = dtbl.NewRow();
            drow["SLNO"] = rowCount + 1;
            drow["ShiftName"] = txtShift.Text;
            drow["FK_Shift"] = hfSelectedItem.Value;
           /// drow["ShiftFrom"] = hfSelectedItem.Value;
            ////drow["ShiftTo"] = hfSelectedItem.Value;
            dtbl.Rows.InsertAt(drow, rowCount);
            grdShiftdetails.DataSource = dtbl;
            grdShiftdetails.DataBind();
            ViewState["TempTable"] = dtbl;
            txtShift.Text = string.Empty;

        }
        else
        {
            ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "alertify.alert('Item already exists')", true);
            txtShift.Text = string.Empty;
        }

    }
    protected void ImageButton2_Click(object sender, ImageClickEventArgs e)
    {
        Delete();
    }
    protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
    {
        grdShiftdetails.DataSource = null;
        grdShiftdetails.DataBind();
        ViewState["TempTable"] = null;
        ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "ShowAddDiv();ClearAdd();", true);
        if (Convert.ToBoolean(hfIsAdd.Value) == false)
        {
            ((Label)CommonPopupPrivilege.FindControl("popuplabel")).Text = "User does not have permission to Add";
            mpPrivilege.Show();
        }
        ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "ShowAddDiv();", true);
    
    }
    protected void gvCounter_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvCounter.PageIndex = e.NewPageIndex;
        GridFill(e.NewPageIndex);
    }
    protected void btnClear_Click(object sender, EventArgs e)
    {
        grdShiftdetails.DataSource = null;
        grdShiftdetails.DataBind();
        ViewState["TempTable"] = null;
        txtRefno.Text = string.Empty;
        txtRefno1.Text = string.Empty;
        txtAccountHead.Text = string.Empty;
        txtAccountHeadSub.Text = string.Empty;
        hfCounterId.Value = "0";
        ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "ShowAddDiv();", true);
    }
    protected void grdShiftdetails_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdShiftdetails.PageIndex = e.NewPageIndex;
        grdShiftdetails.DataSource = ViewState["TempTable"];
        grdShiftdetails.DataBind();
    }
}