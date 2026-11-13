/*----------------------------------------------------------------------------------------------------------------------------------------------------------
Purpose    : Insert/Update/Delete Account Head 
Author     : Mohammed Dilshad
Copyright  : Perfect Software Solutions (Clt) Pvt Ltd
Created on : 16/02/2014
----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
WebPharma
On			By			OMID
----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
14/02/2014 Mohd Dilshad 33431  
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


public partial class District : System.Web.UI.Page
{
    #region FunCtion
    public void BindData()
    {
        new Populate().FillDropdownList(ddlCountry, "Country", "CountryName", "ID_Country", "ID_Country", " Cancelled=0 and FK_Client = " + hfClient.Value, "");
        new Populate().FillDropdownList(ddlCountrySearch, "Country", "CountryName", "ID_Country", "ID_Country", "Cancelled=0  and FK_Client = " + hfClient.Value, "");
      
        BinSubAccount();
        BindSearch();
      


    }
    public void BinSubAccount()
    {

        new Populate().FillDropdownList(ddlState, "States", "StateName", "ID_States", "ID_States", "FK_Country = " + ddlCountry.SelectedValue + " and Cancelled=0 and FK_Client = " + hfClient.Value, "");
     
    }
    public void BindSearch()
    {

        new Populate().FillDropdownList(ddlStateSearch, "States", "StateName", "ID_States", "ID_States", "FK_Country = " + ddlCountrySearch.SelectedValue + " and Cancelled=0 and FK_Client = " + hfClient.Value, "");
      
    }
    public void GridFill(int currentPage)
    {
        BlDistrict BlAcc = new BlDistrict();
        DataTable dtbl = new DataTable();
        BlAcc.DistrictName = txtSearchName.Text.Trim();
       // BlAcc.ShortName = txtSearchShortName.Text.Trim();
        BlAcc.FK_Country = Convert.ToInt64(ddlCountrySearch.SelectedValue);
        BlAcc.FK_States = Convert.ToInt64(ddlStateSearch.SelectedValue);
       // BlAcc.AccountType = Convert.ToByte(ddlAccountTypeSearch.SelectedValue);
        currentPage = currentPage == 0 ? 1 : currentPage;
       
        BlAcc.PageIndex = Convert.ToInt16(currentPage);
        BlAcc.UserCode = hfUser.Value;
        BlAcc.FK_Client = Convert.ToInt64(hfClient.Value);

        dtbl = BlAcc.SelectAllData();
        gvAccountHead.DataSource = dtbl;
        gvAccountHead.DataBind();
        //if (dtbl.Rows.Count > 0)
        //{
        //    int recordCount = Convert.ToInt32(dtbl.Rows[0]["RecordCount"].ToString());
        //    this.PopulatePager(recordCount, currentPage);
        //}
        BlAcc = null;
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

    protected void SaveData()
    {
        long status = 0;
        BlDistrict blDistrict = new BlDistrict();
        blDistrict.FK_Country = Convert.ToInt64(ddlCountry.SelectedValue);
        blDistrict.FK_States = Convert.ToInt64(ddlState.SelectedValue);
       // blAcc.AhCode = txtAccountHeadCode.Text.Trim();
        blDistrict.DistrictName = txtName.Text.Trim();
       // blAcc.ShortName = txtShortName.Text.Trim();
       // blAcc.AccountType = Convert.ToByte(ddlAccountType.SelectedValue);
       // blAcc.SortOrder = int.Parse(txtSortOrder.Text == "" ? "0" : txtSortOrder.Text);
      //  blAcc.Description = txtDescrption.Text;

      
        blDistrict.UserCode = hfUsername.Value;

        blDistrict.FK_Client = Convert.ToInt64(hfClient.Value);

        if (hdfAccountHead.Value == string.Empty)
        {
            if (hfIsAdd.Value.ToLower() == "true")
            {
                status = blDistrict.InsertData();

                if (status > 0)
                {
                    GridFill(1);
                    //blMessage.Items.Add("Data inserted successfully");

                    ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "ShowAddDiv();", true);
                    //ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "toastr.success('Data inserted successfully');ClearAdd();", true);
                    Populate.InsertMsg(this);
                    nextahcode();
                    // GridFill(1);
                }

                else
                {
                    ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "ShowAddDiv();", true);
                    //  ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "toastr.error('Cannot insert data, ShortName/Name already exists')", true);
                    Populate.ErrorMessageBox(this, "Cannot insert data, District Name already exists");
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
            blDistrict.MasterId = Convert.ToInt64(hdfAccountHead.Value);
            if (hfIsUpdate.Value.ToLower() == "true")
            {
                status = blDistrict.UpdateData();
                if (status > 0)
                {
                    //blMessage.Items.Add("Data updated successfully");

                    //ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "ShowAddDiv();", true);
                    GridFill(1);
                    //ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "toastr.success('Data updated successfully');ClearAdd();", true);
                    Populate.UpdateMsg(this);
                }

                else
                {
                    ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "ShowAddDiv();", true);
                    //blMessage.Items.Add("Cannot update data, ShortName/Name/Account Head Code already exists");

                    //  ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "toastr.error('Cannot update data, ShortName/Name already exists')", true);
                    Populate.ErrorMessageBox(this, "Cannot update data, ShortName/Name already exists");
                    txtName.Focus();

                }
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "toastr.warning('User does not have permission to Update')", true);
                txtName.Focus();
            }


        }
        blDistrict = null;
     
    }
    protected void CheckUserAccess()
    {
        BlUserAccess bluseraccess = new BlUserAccess();
        Populate populate = new Populate();

        bluseraccess.FK_Users = Convert.ToInt64(hfUser.Value);
        bluseraccess.FK_Pages = 46;
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
    #endregion
    #region  Events
    protected void Page_Load(object sender, EventArgs e)
    {
       // blMessage.Items.Clear();
        try
        {
            nextahcode();
       
            if (!IsPostBack)
            {

                if (Session["Username"] != null && Convert.ToString(Session["Username"]) != "")
                {

                    hfUsername.Value = Session["Username"].ToString();

                    BlFunction blfunctions = new BlFunction();
                    Int16 type = Convert.ToInt16(blfunctions.GetScalarValue("Settings", "isnull(( select Value", "Name='SEC06'),0)", ""));
                    ViewState["type"] = type.ToString();
                    if (type > 0)
                    {


                        ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "toastr.warning('This Facilities Only Available In SCORE');", true);
                        btnSubmit.Enabled = false;
                        btnSearch.Enabled = false;
                        btnClear.Enabled = false; imgbtnView.Enabled = false;
                    }

                    else
                    {
                        btnSubmit.Enabled = true;
                        btnSearch.Enabled = true;
                        btnClear.Enabled = true; imgbtnView.Enabled = true;
                    }
                    Page.Header.DataBind();
                    ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "ClearAdd();", true);
                    divAdd.Style.Add("display", "block");

                    hfUser.Value = Request.QueryString["uid"];
                    hfClient.Value = Request.QueryString["clnt"];
                    BindData();
                    CheckUserAccess();
                    if (Convert.ToBoolean(hfIsAdd.Value) == false)
                    {
                        ((Label)CommonPopupPrivilege.FindControl("popuplabel")).Text = "User does not have permission to Add";
                        mpPrivilege.Show();
                    }
                    if (Convert.ToBoolean(hfIsUpdate.Value) == false)
                    {
                        gvAccountHead.Columns[4].Visible = false;
                    }
                    if (Convert.ToBoolean(hfIsDelete.Value) == false)
                    {
                        gvAccountHead.Columns[5].Visible = false;
                    }
                    txtName.Focus();
                }
                else
                {
                    Response.Redirect("~/Default.aspx", false);
                }
               
               
             
            }
        }
        catch (Exception ex)
        {
            Populate.WarningMsg(this, ex.Message);
        }
    }
    protected void Page_Changed(object sender, EventArgs e)
    {
        try
        {
           // blMessage.Items.Clear();
            int pageIndex = int.Parse((sender as LinkButton).CommandArgument);
            GridFill(pageIndex);
        }
        catch (Exception ex)
        {
            Populate.WarningMsg(this, ex.Message);
        }
    }
    protected void imgbtnView_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
           // blMessage.Items.Clear();
            txtSearchName.Focus();
            divAdd.Style.Add("display", "none");
            divView.Style.Add("display", "block");
            ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "ClearAdd();", true);
           // BindSearch();
            GridFill(1);

        }
        catch (Exception ex)
        {
            Populate.WarningMsg(this, ex.Message);
        }
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
           // blMessage.Items.Clear();
            SaveData();
        }
        catch (Exception ex)
        {
            Populate.WarningMsg(this, ex.Message);
        }
    }
   
    protected void btnAccountSubFill_Click(object sender, EventArgs e)
    {
        BinSubAccount();
        divAdd.Style.Add("display", "block");
        divView.Style.Add("display", "none");
        ddlState.Focus();

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
            Populate.WarningMsg(this, ex.Message);
        }
    }
   
    protected void btnDlFill_Click(object sender, EventArgs e)
    {
        BindSearch();
        //if (ddlStateSearch.SelectedIndex > 0)
        //    ddlAccountTypeSearch.Focus();
        //else
            ddlStateSearch.Focus();
        divAdd.Style.Add("display", "none");
        divView.Style.Add("display", "block");
    }
    protected void gvAccountHead_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        try
        {
           // blMessage.Items.Clear();
            ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "ShowAddDiv();", true);
            BlDistrict blAccHead = new BlDistrict();
            blAccHead.MasterId = Convert.ToInt64(gvAccountHead.DataKeys[e.NewSelectedIndex]["ID_District"].ToString());
            hdfAccountHead.Value = blAccHead.MasterId.ToString();

            blAccHead.UserCode = hfUser.Value;
            blAccHead.FK_Client = Convert.ToInt64(hfClient.Value);

            blAccHead.SelectData();
            try
            {
                ddlCountry.SelectedValue = blAccHead.FK_Country.ToString();
            }
            catch
            {
                ddlCountry.SelectedValue = "0";
            }
            BinSubAccount();
            try
            {
                ddlState.SelectedValue = blAccHead.FK_States.ToString();
            }
            catch
            {
                ddlState.SelectedValue = "0";
            }
           // txtAccountHeadCode.Text = blAccHead.AhCode;
            txtName.Text = blAccHead.DistrictName;
           // txtShortName.Text = blAccHead.ShortName;
          //  ddlAccountType.SelectedValue = blAccHead.AccountType.ToString();
           
          //  txtSortOrder.Text = blAccHead.SortOrder.ToString();
          //  txtDescrption.Text = blAccHead.Description;
            
            blAccHead = null;


        }
        catch (Exception ex)
        {
            Populate.WarningMsg(this, ex.Message);
        }
    }
    protected void gvAccountHead_RowDeleting(object sender, GridViewDeleteEventArgs e)
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

                hfMasterID.Value = gvAccountHead.DataKeys[e.RowIndex]["ID_District"].ToString();

            }
            else
            { mpDelete.Hide(); }
        }
        catch (Exception ex)
        {
            Populate.WarningMsg(this, ex.Message);
        }
    }
    protected void nextahcode()
    {

       // BlPopulate blcommon = new BlPopulate();
       // txtAccountHeadCode.Text = blcommon.GetNextNumber("AccountHead", "AhCode", "Cancelled=0","").ToString();
       //txtAccountHeadCode.Enabled = false;
       // blcommon = null;
    }
    #endregion
    protected void btnClear_Click(object sender, EventArgs e)
    {
        nextahcode();
        txtName.Focus();
        divAdd.Style.Add("display", "block");
        divView.Style.Add("display", "none");
    }
    protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
    {
        nextahcode();
        txtName.Focus();
        divAdd.Style.Add("display", "block");
        divView.Style.Add("display", "none");
    }
    protected void gvAccountHead_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvAccountHead.PageIndex = e.NewPageIndex;
        GridFill(e.NewPageIndex);
    }
    protected void ImageBtnDelete_Click(object sender, ImageClickEventArgs e)
    {
        Delete();
    }

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
            BlDistrict blDistrict = new BlDistrict();
            blDistrict.MasterId = Convert.ToInt64(hfMasterID.Value);
            blDistrict.UserCode = hfUsername.Value;
            blDistrict.FK_Reason = Convert.ToInt64(Reason);

            blDistrict.UserCode = hfUser.Value;
            blDistrict.FK_Client = Convert.ToInt64(hfClient.Value);
            if (hfIsDelete.Value.ToLower() == "true")
            {

                status = blDistrict.DeleteData();
                if (status > 0)
                {
                    GridFill(1);
                    //blMessage.Items.Add("Deleted successfully");
                    // ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "toastr.success('Data deleted successfully')", true);
                    Populate.DeleteMsg(this);
                }
                else
                {
                    //blMessage.Items.Add("Cannot delete, reference exists");
                    // ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "toastr.error('Cannot delete data, reference exists')", true);
                    Populate.ReferanceMsg(this);

                }
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "toastr.warning('User does not have permission to Delete')", true);
                txtName.Focus();
            }
            blDistrict = null;
        }



    }

}