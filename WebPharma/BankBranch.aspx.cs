/*----------------------------------------------------------------------------------------------------------------------------------------------------------
Purpose    : Insert/Update/Delete Bank Branch
Author     : Bibin TC
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
using WebPharma.Business.Masters;
using System.Data;

public partial class BankBranch : System.Web.UI.Page
{
    #region Functions

    public void SetDetails(string strName, string strRefno)
    {
        if (CommonPopup1.CallingFunction == "AccountSubHeadFill")
        {
            txtSubHeadRefno.Text = strRefno;
            txtAccountSubhead.Text = strName;
            chkActive.Focus();
        }
        else
        {
            txtRefno.Text = strRefno;
            txtAccountHead.Text = strName;
            txtSubHeadRefno.Focus();
        }
    }
    private void ClearAccountHead()
    {
        txtAccountHead.Text = "";
        txtSubHeadRefno.Text = "";
        txtAccountSubhead.Text = "";
        // txtRefno.Text = "";
    }

    public void AccountHeadFill(int pageIndex)
    {
        ClearAccountHead();
        Populate Populate = new Populate();
        CommonPopup1.CallingFunction = "AccountHeadFill";
        string strName = ((TextBox)CommonPopup1.FindControl("txtName")).Text;
        string Refno = ((TextBox)CommonPopup1.FindControl("txtRefno")).Text;
        string searchCol = ((HiddenField)CommonPopup1.FindControl("hfColumn")).Value;
        searchCol = searchCol == string.Empty ? "[Name]" : "[" + searchCol + "]";
        Refno = Refno == string.Empty || Refno == null ? "0" : Refno;
         Populate.FillGrid((GridView)CommonPopup1.FindControl("gdvSearch"), "AccountHead", "ID_AccountHead as ID, Name, ID_AccountHead As [Ref No],ShortName,(CASE WHEN AccountType=1 then 'NORMAL' WHEN AccountType=2 then 'SYSTEM'  WHEN AccountType=3 Then 'BANK' WHEN AccountType=4 THEN 'CASH' WHEN  AccountType=5 THEN 'CUSTOMER' ELSE 'SUPPLIER' END) as [Account Type]","ID_AccountHead","", "FK_Client=" + hfClient.Value.Split(',')[0] + " AND (ID_AccountHead =" + Refno + " OR 0=" + Refno + ") AND Active=1 ", searchCol+"  LIKE '" + strName + "%'", pageIndex, 10, "", (Repeater)CommonPopup1.FindControl("rptPager"), this.Page,1);
         
        Populate = null;
        txtSubHeadRefno.Focus();
        if (txtAccountHead.Text == string.Empty)
        {
            lblPopupHeader.Text = "Account Head";
            mpCommon.Show();
        }

    }
    public void AccountSubHeadFill(int pageIndex)
    {
        Populate Populate = new Populate();
        CommonPopup1.CallingFunction = "AccountSubHeadFill";
        string strName = ((TextBox)CommonPopup1.FindControl("txtName")).Text;
        string Refno = ((TextBox)CommonPopup1.FindControl("txtRefno")).Text;
        Refno = Refno == string.Empty || Refno == null ? "0" : Refno;
        string strAccountHead = txtRefno.Text == "" ? "0" : txtRefno.Text;
        string searchCol = ((HiddenField)CommonPopup1.FindControl("hfColumn")).Value;
        searchCol = searchCol == string.Empty ? "[Name]" : "[" + searchCol + "]";
        Populate.FillGrid((GridView)CommonPopup1.FindControl("gdvSearch"), "AccountHeadSub", "ID_AccountHeadSub as ID,Name,ID_AccountHeadSub As [Ref No],ShortName,AhCode, AhsCode", "ID_AccountHeadSub", "","FK_Client=" + hfClient.Value.Split(',')[0] + " AND (ID_AccountHeadSub =" + Refno + " OR 0=" + Refno + ")  AND Active=1 And (FK_AccountHead=" + strAccountHead + " OR 0=" + strAccountHead + ") AND AccountHeadSub.Active=1 " ,searchCol+"  LIKE '" + strName + "%'", pageIndex, 10, "", (Repeater)CommonPopup1.FindControl("rptPager"), this.Page,1);
        Populate = null;
      //  txtDescription.Focus();
        if (txtAccountSubhead.Text == string.Empty)
        {
            lblPopupHeader.Text = "Account Sub Head";
            mpCommon.Show();
        }
    }
 
    protected void BindData()
    {
        Populate populate = new Populate();
        populate.FillDropdownList(ddlBank, "Bank", "Name", "ID_Bank", "ID_Bank", "Active='True' AND FK_Client=" + hfClient.Value, "");
        populate.FillDropdownList(ddlCountry, "Country", "Name", "ID_Country", "ID_Country", "FK_Client=" + hfClient.Value, "");
       // populate.FillDropdownList(ddlAccountHead, "AccountHead", "ShortName+' - '+Name", "ID_AccountHead", "ID_AccountHead", "Active='True' AND FK_Client=" + hfClient.Value, "");
        populate.FillDropdownList(ddlSearchBank, "Bank", "Name", "ID_Bank", "ID_Bank", "Active='True' AND FK_Client=" + hfClient.Value, "");
        populate = null;
    }
    protected void GridFill(int currentPage)
    {
        BlBankBranch blbankbranch = new BlBankBranch();
        DataTable dtbl = new DataTable();
        blbankbranch.ShortName = txtSearchShortName.Text.Trim();
        blbankbranch.Name = txtSearchName.Text.Trim();
        blbankbranch.FK_Bank = Convert.ToInt64(ddlSearchBank.SelectedValue.ToString());
        blbankbranch.FK_Client = Convert.ToInt64(hfClient.Value);
        blbankbranch.UserCode = hfUser.Value;
        blbankbranch.PageIndex = Convert.ToInt16(currentPage);
        dtbl = blbankbranch.SelectAllData();
        gvBankBranch.DataSource = dtbl;
        gvBankBranch.DataBind();
        if (dtbl.Rows.Count > 0)
        {
            int recordCount = Convert.ToInt32(dtbl.Rows[0]["RecordCount"].ToString());
            this.PopulatePager(recordCount, currentPage);
        }
        blbankbranch = null;
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
        BlBankBranch blbankbranch = new BlBankBranch();
        blbankbranch.FK_Bank = Convert.ToInt64(ddlBank.SelectedValue.ToString());
        blbankbranch.ShortName = txtShortName.Text;
        blbankbranch.Name = txtName.Text;
        blbankbranch.FK_Country = Convert.ToInt64(ddlCountry.SelectedValue.ToString());
        blbankbranch.Address1 = txtAddress1.Text;
        blbankbranch.Address2 = txtAddress2.Text;
        blbankbranch.Address3 = txtAddress3.Text;
        blbankbranch.FK_AccountHeadSub = Convert.ToInt64(txtSubHeadRefno.Text == "" || txtAccountSubhead.Text == "" ? "0" : txtSubHeadRefno.Text);//ddlAccountHead.SelectedValue.ToString()
        blbankbranch.FK_AccountHead = Convert.ToInt64((txtRefno.Text));
       // blbankbranch.FK_AccountHead = Convert.ToInt64(ddlAccountHead.SelectedValue.ToString());
        blbankbranch.Active = Convert.ToBoolean(chkActive.Checked);
        blbankbranch.FK_Client = Convert.ToInt64(hfClient.Value);
        blbankbranch.UserCode = hfUser.Value;
    
        if (hfBankBranchId.Value == "0")
        {
            if (hfIsAdd.Value.ToLower() == "true")
            {

                status = blbankbranch.InsertData();
                if (status > 0)
                {
                    GridFill(1);
                    ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "ShowAddDiv();", true);
                    ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "toastr.success('Data inserted successfully');ClearAdd();", true);
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "ShowAddDiv();", true);
                    ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "toastr.error('Cannot insert data, ShortName/Name already exists')", true);
                    txtShortName.Focus();
                }
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "toastr.warning('User does not have permission to add')", true);
                txtShortName.Focus();

            }
        }
        else
        {
            blbankbranch.MasterId = Convert.ToInt64(hfBankBranchId.Value);
            if (hfIsUpdate.Value.ToLower() == "true")
            {

                status = blbankbranch.UpdateData();
                if (status > 0)
                {
                    GridFill(1);
                    ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "ShowAddDiv();", true);
                    ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "toastr.success('Data updated successfully');ClearAdd();", true);
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "ShowAddDiv();", true);
                    ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "toastr.error('Cannot update data, ShortName/Name already exists')", true);
                    txtShortName.Focus();
                }
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "toastr.warning('User does not have permission to Update')", true);
                txtShortName.Focus();
            }
        }
        blbankbranch = null;
    }
    protected void CheckUserAccess()
    {
        BlUserAccess bluseraccess = new BlUserAccess();
        Populate populate = new Populate();

        bluseraccess.FK_Users = Convert.ToInt64(hfUser.Value);
        bluseraccess.FK_Pages = 7;
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

    #region Events
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                
                txtAccountHead.Attributes.Add("readonly", "readonly");
                txtAccountSubhead.Attributes.Add("readonly", "readonly");      
                Page.Header.DataBind();
                //blMessage.Items.Clear();
                ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "ClearAdd();", true);
                divAdd.Style.Add("display", "block");
                hfUser.Value = Request.QueryString["uid"];
                hfClient.Value = Request.QueryString["clnt"];
                BindData();
                CheckUserAccess();
                ddlBank.Focus();
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "toastr.warning('" + ex.Message + "')", true);    
        }
    }
    protected void Page_Changed(object sender, EventArgs e)
    {
        try
        {
            //blMessage.Items.Clear();
            int pageIndex = int.Parse((sender as LinkButton).CommandArgument);
            GridFill(pageIndex);
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
            ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "ClearAdd(); ClearSearch();", true);
            GridFill(1);
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
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        try
        {
            //blMessage.Items.Clear();
            GridFill(1); ddlSearchBank.Focus();
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "toastr.warning('" + ex.Message + "')", true);    
        }
    }
    protected void gvBankBranch_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        try
        {
            //blMessage.Items.Clear();
            ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "ShowAddDiv();", true);
            BlBankBranch blbankbranch = new BlBankBranch();
            blbankbranch.MasterId = Convert.ToInt64(gvBankBranch.DataKeys[e.NewSelectedIndex]["ID_BankBranch"].ToString());
            hfBankBranchId.Value = blbankbranch.MasterId.ToString();
            blbankbranch.FK_Client = Convert.ToInt64(hfClient.Value);
            blbankbranch.UserCode = hfUser.Value;
            blbankbranch.SelectData();
            try
            {
                ddlBank.SelectedValue = blbankbranch.FK_Bank.ToString();
            }
            catch(Exception)
            {
                ddlBank.SelectedValue="0";
            }
            txtShortName.Text = blbankbranch.ShortName.ToString();
            txtName.Text = blbankbranch.Name.ToString();
            ddlCountry.SelectedValue = blbankbranch.FK_Country.ToString();
            txtAddress1.Text = blbankbranch.Address1.ToString();
            txtAddress2.Text = blbankbranch.Address2.ToString();
            txtAddress3.Text = blbankbranch.Address3.ToString();
            try
            {

                txtSubHeadRefno.Text = blbankbranch.FK_AccountHeadSub == 0 ? "" : blbankbranch.FK_AccountHeadSub.ToString();
                txtAccountSubhead.Text = blbankbranch.AccountSubHead;

                txtRefno.Text = blbankbranch.FK_AccountHead.ToString();
                txtAccountHead.Text = blbankbranch.AccountHead;


                //blcustomer.FK_AccountHead = Convert.ToInt64((txtRefno.Text));
                //txtAccountSubhead.Text = blcustomer.FK_AccountHeadSub.ToString();
                //ddlAccountHead.SelectedValue = blcustomer.FK_AccountHeadSub.ToString();
            }
            catch (Exception)
            {
                ClearAccountHead(); txtRefno.Text = "";

                // ddlAccountHead.SelectedValue = "0";
            }
            chkActive.Checked = blbankbranch.Active;
            blbankbranch = null;
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "toastr.warning('" + ex.Message + "')", true);    
        }
    }
    protected void gvBankBranch_RowDeleting(object sender, GridViewDeleteEventArgs e)
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
                BlBankBranch blbankbranch = new BlBankBranch();
                blbankbranch.MasterId = Convert.ToInt64(gvBankBranch.DataKeys[e.RowIndex]["ID_BankBranch"].ToString());
                blbankbranch.FK_Client = Convert.ToInt64(hfClient.Value);
                blbankbranch.UserCode = hfUser.Value;
                if (hfIsDelete.Value.ToLower() == "true")
                {
                    status = blbankbranch.DeleteData();
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
                    txtShortName.Focus();
                }

                blbankbranch = null;
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "toastr.warning('" + ex.Message + "')", true);    
        }
    }
    #endregion              
    protected void btnGo_Click(object sender, EventArgs e)
    {
        try
        {
            ClearAccountHead();
            ((TextBox)CommonPopup1.FindControl("txtName")).Text = txtAccountHead.Text.Trim();
            ((TextBox)CommonPopup1.FindControl("txtRefno")).Text = txtRefno.Text.Trim();
            ((HiddenField)CommonPopup1.FindControl("hfColumn")).Value = "Name";
            AccountHeadFill(1);
            divAdd.Style.Add("display", "block");
            divView.Style.Add("display", "none");
            txtSubHeadRefno.Focus();
        }
        catch (Exception ex)
        {
        }
    }
    protected void btnSubheadGo_Click(object sender, EventArgs e)
    {
        try
        {
            txtAccountSubhead.Text = string.Empty;
            ((TextBox)CommonPopup1.FindControl("txtName")).Text = txtAccountSubhead.Text;
            ((TextBox)CommonPopup1.FindControl("txtRefno")).Text = txtSubHeadRefno.Text.Trim();
            ((HiddenField)CommonPopup1.FindControl("hfColumn")).Value = "Name";
            AccountSubHeadFill(1);
            divAdd.Style.Add("display", "block");
            divView.Style.Add("display", "none");
            chkActive.Focus();
        }
        catch (Exception ex)
        {
        }
    }
}