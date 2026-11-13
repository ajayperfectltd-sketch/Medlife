/*----------------------------------------------------------------------------------------------------------------------------------------------------------
Purpose    : Insert/Update/Delete Machine 
Author     : BIBIN tc
Copyright  : Perfect Software Solutions (Clt) Pvt Ltd
Created on : 03/05/2018
----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
WebPharma
On			      By			OMID
----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
23-01-2019  VINISHA Removed PosModule,DisableNegativeStock,PolePortName And Added Shift,BillType
--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using WebPharma.Business.Masters;


public partial class Machine: System.Web.UI.Page
{

    #region Function
    public void Delete()
    {
        try
        {
            divPopupReason.Visible = true;
            Populate Populate = new Populate();
            CommonPopup1.CallingFunction = "PopupOnReason";
            ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "PopupOnReason();", true);
            mpCommon.Show();
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
            BlMachine blMachine = new BlMachine();
            //blReason.MasterId = Convert.ToInt64(gvReason.DataKeys[e.RowIndex]["ID_Reason"].ToString());
            blMachine.UserCode = hfuserName.Value;
            blMachine.MasterId = Convert.ToInt64(hfMachineId.Value);
            blMachine.FK_Reason = Convert.ToInt64(Reason);
            if (hfIsDelete.Value.ToLower() == "true")
            {
                status = blMachine.DeleteData();
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
                
            }
            blMachine = null;
        }
    }
    protected void BindData()
    {
        Populate populate = new Populate();

        populate.FillDropdownList(ddlCounter, "Counters", "CounterName", "ID_Counters", "CounterName", "Active='True' AND Cancelled=0 AND FK_Client= " + hfClient.Value.Split(',')[0], "");
        populate.FillDropdownList(ddlPrint, "PrintLayout", "PriModule", "PriModule", "", " PriModule<>''  Group by PriModule", "");
        //populate.FillDropdownList(ddlStore, "Store", "StoName", "ID_Store", "ID_Store", "Active='True' AND Cancelled=0 AND FK_Client= " + hfClient.Value.Split(',')[0], "");
        populate.FillDropdownList(ddlShift, "ShiftMaster", "ShiftName", "ID_ShiftMaster", "ShiftName", "Active='True' AND Cancelled=0 AND FK_Client= " + hfClient.Value.Split(',')[0], "");
        populate.FillDropdownList(ddlBillType, "BillType", "TypeName", "ID_BillType", "TypeName", " Cancelled=0 AND FK_Client= " + hfClient.Value.Split(',')[0], "");
        
        populate = null;
    }
    protected void GridFill(int currentPage)
    {
        BlMachine blMachine = new BlMachine();
        DataTable dtbl = new DataTable();
        blMachine.MecName = txtSearchMecName.Text.Trim();
        blMachine.MecIP = txtSearchMecIp.Text.Trim();
        blMachine.FK_Client = Convert.ToInt64(hfClient.Value);
        blMachine.UserCode = hfUser.Value;
        blMachine.PageIndex = Convert.ToInt16(currentPage);
        dtbl = blMachine.SelectAllData();
        gvMachine.DataSource = dtbl;
        gvMachine.DataBind();
        blMachine = null;
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
        bluseraccess.FK_Pages = 17;
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
        BlMachine blMachine = new BlMachine();
        blMachine.MecName = txtMecName.Text;
        blMachine.MecIP = txtMecIp.Text;
        blMachine.MecAddress = txtMecAddress.Text;
        blMachine.FK_Client = Convert.ToInt64(hfClient.Value);
        blMachine.UserCode = hfUser.Value;
        blMachine.AutoPrint = Convert.ToBoolean(chkAutoPrint.Checked);
        blMachine.FK_Counter = Convert.ToInt64(ddlCounter.SelectedValue.ToString());
        blMachine.FK_Store = Convert.ToInt64(ddlStore.SelectedValue.ToString());
        blMachine.PrintModule = Convert.ToString(ddlPrint.SelectedValue.ToString());
        blMachine.FK_Shift = Convert.ToInt64(ddlShift.SelectedValue.ToString());
        blMachine.FK_BillType = Convert.ToInt64(ddlBillType.SelectedValue.ToString()); 

        if (hfMachineId.Value == "0")
        {
            if (hfIsAdd.Value.ToLower() == "true")
            {
                status = blMachine.InsertData();
                if (status > 0)
                {
                    ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "ShowAddDiv();", true);
                    ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "toastr.success('Data inserted successfully');ClearAdd();", true);
                    GridFill(1);
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "ShowAddDiv();", true);
                    ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "toastr.error('Cannot insert data, Machine already exists')", true);
                    txtMecName.Focus();
                }
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "toastr.warning('User does not have permission to add')", true);
                txtMecName.Focus();
            }
        }
        else
        {
            if (hfIsUpdate.Value.ToLower() == "true")
            {
                blMachine.MasterId = Convert.ToInt64(hfMachineId.Value);
                status = blMachine.UpdateData();
                if (status > 0)
                {
                    ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "ShowAddDiv();", true);
                    ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "toastr.success('Data updated successfully');ClearAdd();", true);
                    GridFill(1);
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "ShowAddDiv();", true);
                    ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "toastr.error('Cannot update data, Machine already exists')", true);
                    txtMecName.Focus();
                }
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "toastr.warning('User does not have permission to Update')", true);
                txtMecName.Focus();
            }
        }
        blMachine = null;
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
                divAdd.Style.Add("display", "block");
                hfUser.Value = Request.QueryString["uid"];
                hfClient.Value = Request.QueryString["clnt"];
                hfuserName.Value = Session["Username"].ToString();
                CheckUserAccess();
                BindData();
                GridFill(1);
                txtMecName.Focus();
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "toastr.warning('" + ex.Message + "')", true); 
        }
    }
    protected void imgbtnView_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            //blMessage.Items.Clear();
            divAdd.Style.Add("display", "none");
            divView.Style.Add("display", "block");
            ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "ClearAdd(); ", true);
            GridFill(1);
            txtSearchMecName.Focus();
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
    protected void btnHiddenLoadStore_Click(object sender, EventArgs e)
    {
        try
        {
            if (ddlCounter.Items.Count > 0)
            {
                if (ddlCounter.SelectedIndex > 0)
                {

                    Populate populate = new Populate();

                    populate.FillDropdownList(ddlStore, "Store S inner join  Counters C on C.FK_Store=S.ID_Store  ", "S.StoName", "S.ID_Store", "S.ID_Store", "    S.Active='True' AND C.ID_Counters=" + ddlCounter.SelectedValue.ToString() + " AND S.FK_Client= " + hfClient.Value.Split(',')[0], "");
                   
                    populate = null;
                    if (ddlCounter.SelectedIndex > 0)
                        ddlStore.Focus();
                    else
                        ddlCounter.Focus();
                }
                else
                { 
                
                
                }
            }
            divAdd.Style.Add("display", "block");
            divView.Style.Add("display", "none");

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
            //if (hfMachineId.Value == "0")//Check Licensing
            //{
            //    Licensing.CheckMachineLicense();
            //}
            SaveData();
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "toastr.warning('" + ex.Message + "')", true); 
        }
    }
    protected void gvMachine_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        try
        {
           // blMessage.Items.Clear();
            ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "ShowAddDiv();", true);
            BlMachine blMachine = new BlMachine();
            blMachine.MasterId = Convert.ToInt64(gvMachine.DataKeys[e.NewSelectedIndex]["ID_Machine"].ToString());
            hfMachineId.Value = blMachine.MasterId.ToString();
            blMachine.FK_Client = Convert.ToInt64(hfClient.Value);
            blMachine.UserCode = hfUser.Value;
            blMachine.SelectData();
            txtMecName.Text = blMachine.MecName.ToString().Trim();
            txtMecIp.Text = blMachine.MecIP.ToString();
            txtMecAddress.Text = blMachine.MecAddress.ToString().Trim();
            chkAutoPrint.Checked = blMachine.AutoPrint;
            ddlCounter.SelectedValue = blMachine.FK_Counter.ToString();
            ddlPrint.SelectedValue = blMachine.PrintModule.ToString();
            ddlShift.SelectedValue= blMachine.FK_Shift.ToString();
            ddlBillType.SelectedValue = blMachine.FK_BillType.ToString();
            if (ddlCounter.Items.Count > 0)
            {
                if (ddlCounter.SelectedIndex > 0)
                {

                    Populate populate = new Populate();

                    populate.FillDropdownList(ddlStore, "Store S inner join  Counters C on C.FK_Store=S.ID_Store  ", "S.StoName", "S.ID_Store", "S.ID_Store", "    S.Active='True' AND C.ID_Counters=" + blMachine.FK_Counter.ToString() + " AND S.FK_Client= " + hfClient.Value.Split(',')[0], "");

                    populate = null;
                }
                else
                {


                }
            }

            ddlStore.SelectedValue = blMachine.FK_Store.ToString();

            blMachine = null;

            divAdd.Style.Add("display", "block");
            divView.Style.Add("display", "none");
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "toastr.warning('" + ex.Message + "')", true); 
        }
    }
    protected void gvMachine_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
           // blMessage.Items.Clear();
            string confirmValue = Request.Form["confirm_value"];
            if (confirmValue.Length > 3)
                confirmValue = confirmValue.Substring(confirmValue.Length - 3, 3);
            if (confirmValue == "Yes")
            {
                long status = 0;
                BlMachine blMachine = new BlMachine();
                blMachine.MasterId = Convert.ToInt64(gvMachine.DataKeys[e.RowIndex]["ID_Machine"].ToString());
                hfMachineId.Value = (gvMachine.DataKeys[e.RowIndex]["ID_Machine"].ToString());
                blMachine.FK_Client = Convert.ToInt64(hfClient.Value);
                blMachine.UserCode = hfUser.Value;
                blMachine = null;
            }
            else
            {
                mpCommon.Hide();
            }
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
            txtSearchMecName.Focus();

        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "toastr.warning('" + ex.Message + "')", true); 
        }
    }
    #endregion                
   
    protected void ImageButton2_Click(object sender, ImageClickEventArgs e)
    {
        Delete();
    }
}