/*----------------------------------------------------------------------------------------------------------------------------------------------------------
Purpose    : Insert/Update/Delete Account Sub 
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
using WebPharma.Business.Masters;
using System.Data;
using WebPharma.Business;


public partial class AccountHeadSub : System.Web.UI.Page
{
    #region Function
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
            BlAccountHeadSub BlAccountHeadSub = new BlAccountHeadSub();
            BlAccountHeadSub.UserCode = hfuserName.Value;
            BlAccountHeadSub.MasterId = Convert.ToInt64(hfMasterID.Value);
            BlAccountHeadSub.FK_Reason = Convert.ToInt64(Reason);
            //  BlAccountGroup.FK_Client = Convert.ToInt64(Reason);
            if (hfIsDelete.Value.ToLower() == "true")
            {
                status = BlAccountHeadSub.DeleteData();
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
                //txtName.Focus();
            }
            BlAccountHeadSub = null;
        }
    }
    public void BindData()
    {
      //  new Populate().FillDropdownList(ddlAccountHead, "AccountHead", "Name", "ID_AccountHead", "ID_AccountHead", " Active=1 and FK_Client = "+ hfClient.Value, "");
        new Populate().FillDropdownList(ddlStore, "Store", "StoName", "ID_Store", "StoName", " Cancelled=0 and Active='True' AND FK_Client= " + hfClient.Value.Split(',')[0], "");
        new Populate().FillDropdownList(ddlAccountHeadSearch, "AccountHead", "HeadName", "ID_AccountHead", "ID_AccountHead", " Cancelled=0 and FK_Client = " + hfClient.Value, "");
  
        txtopnBalanceDate.Text = DateTime.Now.ToShortDateString();
        txtopnBalanceDate.Attributes.Add("readonly", "readonly");
    }
    public void GridFill(int currentPage)
    {

        BlAccountHeadSub BlAC = new BlAccountHeadSub();
        DataTable dtbl = new DataTable();
        BlAC.Name = txtSearchName.Text.Trim();
        BlAC.Fk_AccountHead = Convert.ToInt64(ddlAccountHeadSearch.SelectedValue);
        currentPage = currentPage == 0 ? 1 : currentPage;
        BlAC.PageIndex = Convert.ToInt16(currentPage);
        BlAC.UserCode = hfUser.Value;
        BlAC.FK_Client= Convert.ToInt64(hfClient.Value);

        dtbl = BlAC.SelectAllData();
        gvAccountHeadSub.DataSource = dtbl;
        gvAccountHeadSub.DataBind();
        BlAC = null;
    }

    public void GetCode(long AccountHeadId)
    {
        BlAccountHead blac = new BlAccountHead();
        blac.MasterId = AccountHeadId;
        blac.FK_Client = Convert.ToInt64(hfClient.Value);
        blac.SelectData();
      txtRefno.Text = blac.AhCode;
        divAdd.Style.Add("display", "block");
        divView.Style.Add("display", "none");
        blac = null;
    }
      protected void SaveData()
    {
        long status = 0;
        BlAccountHeadSub blAccountSub = new BlAccountHeadSub();

      //  blAccountSub.Fk_AccountHead = Convert.ToInt64( ViewState["ID_AccountHead"]);
      //  blAccountSub.Fk_AccountHead = Int64.Parse(txtRefno.Text == "" ? "0" : txtRefno.Text);

        blAccountSub.Ahcode = (txtRefno.Text == "" ? "0" : txtRefno.Text);
        DataTable dt1;
        dt1 = blAccountSub.selectfkAccounthead();
        //ViewState["ID_AccountHead"] = dt1.Rows[0]["Column1"].ToString();
        blAccountSub.Fk_AccountHead =Convert.ToInt64( dt1.Rows[0]["Column1"].ToString());
     //   blAccountSub.Ahcode =txtAccountHead.Text.Trim();
        blAccountSub.AhsCode = txtAccountSubHeadCode.Text.Trim();
        blAccountSub.Name = txtcustomername.Text.Trim();
        blAccountSub.Fk_Cuscode = Int64.Parse(Txtcusid.Text == "" ? "0" : Txtcusid.Text);
        blAccountSub.SortOrder = int.Parse(txtSortOrder.Text == "" ? "0" : txtSortOrder.Text);
        blAccountSub.Fk_Store = Convert.ToInt64(ddlStore.SelectedValue);
        blAccountSub.PettyCash = cbxPettyCash.Checked;
        blAccountSub.OpeningDate= Convert.ToDateTime(txtopnBalanceDate.Text);
       // blAccountSub.OpeningBalance = Convert.ToInt64(txtopnBalanceAmnt.Text);
        blAccountSub.BalanceType = Convert.ToInt16(ddlCreditDebit.SelectedValue);
        if (blAccountSub.BalanceType == 0)
        {
            blAccountSub.OpeningBalance = Convert.ToDecimal(txtopnBalanceAmnt.Text == "" ? "0" : txtopnBalanceAmnt.Text);
        }
          if(blAccountSub.BalanceType==1)
              {

                  blAccountSub.OpeningBalance = Convert.ToDecimal(txtopnBalanceAmnt.Text == "" ? 0 : Convert.ToDecimal(txtopnBalanceAmnt.Text) * -1);
     
              }
                 

       
        blAccountSub.UserCode = hfUser.Value;
        blAccountSub.FK_Client = Convert.ToInt64(hfClient.Value);


        if (hdfAccountSubHead.Value == string.Empty)
        {
            if (hfIsAdd.Value.ToLower() == "true")
            {

                status = blAccountSub.InsertData();
                if (status > 0)
                {
                    GridFill(1);
                    // //blMessage.Items.Add("Data inserted successfully");

                    ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "ShowAddDiv();", true);
                    Populate.InsertMsg(this);
                   // ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "toastr.success('Data inserted successfully');ClearAdd();", true);
                }

                else
                {

                    ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "ShowAddDiv();", true);
                    // // //blMessage.Items.Add("Cannot insert data, ShortName/Name/Sort order already exists");

                    ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "toastr.error('Cannot insert data, ShortName/Name/Account Sub Head Code already exists')", true);
                  

                    // ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "ShowAddDiv();", true);
                    //  //blMessage.Items.Add("");
                }
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "toastr.warning('User does not have permission to add')", true);
              
                ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "ShowAddDiv();", true);
            }
        }
        else
        {
            blAccountSub.MasterId = Convert.ToInt64(hdfAccountSubHead.Value);
            if (hfIsUpdate.Value.ToLower() == "true")
            {
                status = blAccountSub.UpdateData();

                if (status > 0)
                {
                    GridFill(1);
                    ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "toastr.success('Data updated successfully');ClearAdd();", true);
                    ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "ShowAddDiv();", true);
                }

                else
                {
                    ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "ShowAddDiv();", true);
                    //blMessage.Items.Add("Cannot update data, ShortName/Name/Account Sub Head Code already exists");
                    
                    ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "ShowAddDiv();", true);
                }
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "toastr.warning('User does not have permission to Update')", true);
                
                ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "ShowAddDiv();", true);
            }

        }
        blAccountSub = null;
    }
      protected void CheckUserAccess()
      {
          BlUserAccess bluseraccess = new BlUserAccess();
          Populate populate = new Populate();

          bluseraccess.FK_Users = Convert.ToInt64(hfUser.Value);
          bluseraccess.FK_Pages = 21;
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
       // blMessage.Items.Clear();
        try
        {
            if (!IsPostBack)

            {
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
                
                txtRefno.Attributes.Add("readonly", "readonly");
               // txtcustomername.Attributes.Add("readonly", "readonly");
                txtAccountHead.Attributes.Add("readonly", "readonly");
                Txtcusid.Attributes.Add("readonly", "readonly");
                hfuserName.Value = Session["Username"].ToString();
                hfUser.Value = Request.QueryString["uid"];
                hfClient.Value = Request.QueryString["clnt"];
                BindData();
                CheckUserAccess();
                
                //GridFill(0);
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
            //blMessage.Items.Clear();
            int pageIndex = int.Parse((sender as LinkButton).CommandArgument);
            GridFill(pageIndex);
        }
        catch (Exception ex)
        {
             //blMessage.Items.Add(ex.Message);
            Populate.WarningMsg(this, ex.Message);
        }
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
             //blMessage.Items.Add(ex.Message);
            Populate.WarningMsg(this, ex.Message);
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
             //blMessage.Items.Add(ex.Message);
            Populate.WarningMsg(this, ex.Message);
        }
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        try
        {
            //blMessage.Items.Clear();
            GridFill(1);
           
        }
        catch (Exception ex)
        {
             //blMessage.Items.Add(ex.Message);
            Populate.WarningMsg(this, ex.Message);
        }
    }
    protected void gvAccountHeadSub_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        //blMessage.Items.Clear();
        try
        {
            //ID_AccountHeadSub
            ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "ShowAddDiv();", true);
            BlAccountHeadSub AccountHeadSub = new BlAccountHeadSub();
            AccountHeadSub.MasterId = Convert.ToInt64(gvAccountHeadSub.DataKeys[e.NewSelectedIndex]["ID_AccountHeadSub"].ToString());

            AccountHeadSub.FK_Client = Convert.ToInt64(hfClient.Value);
            AccountHeadSub.UserCode = hfUser.Value;

       
            AccountHeadSub.SelectData();
            hdfAccountSubHead.Value = AccountHeadSub.MasterId.ToString();
            
            txtRefno.Text = AccountHeadSub.Fk_AccountHead.ToString();
       

           txtAccountHead.Text = AccountHeadSub.Ahcode; 
            txtAccountSubHeadCode.Text = AccountHeadSub.AhsCode;
              txtcustomername.Text = AccountHeadSub.Name;
              Txtcusid.Text = AccountHeadSub.Fk_Cuscode.ToString();
         
            txtSortOrder.Text = AccountHeadSub.SortOrder.ToString();
            cbxPettyCash.Checked = AccountHeadSub.PettyCash;
            txtopnBalanceDate.Text = AccountHeadSub.OpeningDate.ToShortDateString();
           
            String  balance = AccountHeadSub.OpeningBalance.ToString(); ;
            if (Convert.ToDecimal(balance) < 0)
            {
                ddlCreditDebit.SelectedValue = "1";
                txtopnBalanceAmnt.Text = (Convert.ToDecimal(balance) * -1).ToString();
              
            }
            else
            {
                ddlCreditDebit.SelectedValue = "0";
                txtopnBalanceAmnt.Text = balance;
              
                
            }
            
        }
        catch (Exception ex)
        {
            Populate.WarningMsg(this, ex.Message);
        }
    }
    protected void gvAccountHeadSub_RowDeleting(object sender, GridViewDeleteEventArgs e)
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
                BlAccountHeadSub AccountHeadSub = new BlAccountHeadSub();

                 AccountHeadSub.MasterId = Convert.ToInt64(gvAccountHeadSub.DataKeys[e.RowIndex]["ID_AccountHeadSub"].ToString());
                 hfMasterID.Value = (gvAccountHeadSub.DataKeys[e.RowIndex]["ID_AccountHeadSub"].ToString());
                 AccountHeadSub.UserCode = hfuserName.Value;
                AccountHeadSub.FK_Client = Convert.ToInt64(hfClient.Value);
                AccountHeadSub = null;
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
       

    protected void btnHide_Click(object sender, EventArgs e)
    {
        //try
        //{
        //    GetCode(Convert.ToInt64(ddlAccountHead.SelectedValue));
        //    txtAccountSubHeadCode.Focus();
        //}
        //catch (Exception ex)
        //{
        //    Populate.WarningMsg(this, ex.Message);
        //}

    }
      #endregion
    protected void btnGo_Click(object sender, ImageClickEventArgs e)
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

            //txtSubHeadRefno.Focus();


        }
        catch (Exception ex)
        {
        }
    }
    private void ClearAccountHead()
    {
        txtAccountHead.Text = "";
        txtRefno.Text  = "";
        //txtAccountSubhead.Text = "";
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

       // Populate.FillGrid((GridView)CommonPopup1.FindControl("gdvSearch"), "AccountHead", "Name , ID_AccountHead, ID_AccountHead As [Ref No],ShortName,(CASE WHEN AccountType=1 then 'NORMAL' WHEN AccountType=2 then 'SYSTEM'  WHEN AccountType=3 Then 'BANK' WHEN AccountType=4 THEN 'CASH' WHEN  AccountType=5 THEN 'CUSTOMER' ELSE 'SUPPLIER' END) as [Account Type]", "ID_AccountHead", "", "FK_Client=" + hfClient.Value.Split(',')[0] + " AND (ID_AccountHead =" + Refno + " OR 0=" + Refno + ") AND Active=1 ", searchCol + "  LIKE '" + strName + "%'", pageIndex, 10, "", (Repeater)CommonPopup1.FindControl("rptPager"), this.Page);
      // new Populate().FillDropdownList(ddlAccountHead, "AccountHead", "Name", "ID_AccountHead", "ID_AccountHead", " Active=1 and FK_Client = " + hfClient.Value, "");
        Populate.FillPopUpGrid((GridView)CommonPopup1.FindControl("gdvSearch"), "AccountHead", "AhCode as ID,HeadName as Name", "ID_AccountHead", "ID_AccountHead", "FK_Client=" + hfClient.Value.Split(',')[0] + "  AND Cancelled=0 AND HeadName ", strName, null);
        Populate = null;
        //Populate.FillGrid((GridView)CommonPopup1.FindControl("gdvSearch"), "AccountHead", "HeadName ,AhCode as ID,  AhCode As [Ref No],ShortName", "FK_Client=" + hfClient.Value.Split(',')[0] + " AND (ID_AccountHead =" + Refno + " OR 0=" + Refno + ")  ", searchCol + "  LIKE '" + strName + "%' and Cancelled=0   ", pageIndex, 10, "", (Repeater)CommonPopup1.FindControl("rptPager"), this.Page,1);
        //Populate = null;
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
            blAccountSub.Ahcode =(strRefno);

            blAccountSub.FK_Client = Convert.ToInt64(hfClient.Value);

            blAccountSub.UserCode = hfUser.Value;
            
            dt = blAccountSub.selectid();
            txtAccountSubHeadCode.Text = dt.Rows[0]["Column1"].ToString();
            txtAccountSubHeadCode.Attributes.Add("readonly", "readonly");

            dt1 = blAccountSub.selectfkAccounthead();
            ViewState["ID_AccountHead"] = dt1.Rows[0]["Column1"].ToString();
            blAccountSub = null;

            // chkActive.Focus();
        }
        if (CommonPopup1.CallingFunction == "customerfill")
        {
            Txtcusid.Text = strRefno;
            txtcustomername.Text = strName;
            // chkActive.Focus();
        }
       
       
       
    }

    protected void btnGos_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
              ClearCustomer();
            ((TextBox)CommonPopup1.FindControl("txtName")).Text = txtcustomername.Text.Trim();
            ((TextBox)CommonPopup1.FindControl("txtRefno")).Text = Txtcusid.Text.Trim();
            ((HiddenField)CommonPopup1.FindControl("hfColumn")).Value = "Name";
            customerfill(1);
            divAdd.Style.Add("display", "block");
            divView.Style.Add("display", "none");
            //txtSubHeadRefno.Focus();
        }
        catch (Exception ex)
        {
        }
        	
    }
    private void ClearCustomer()
    {
        txtcustomername.Text = "";
        Txtcusid.Text = "";
     
        //txtAccountSubhead.Text = "";
        // txtRefno.Text = "";
    }

    public void customerfill(int pageIndex)
    {
  

        // ClearAccountHead();
        Populate Populate = new Populate();
        CommonPopup1.CallingFunction = "customerfill";
        string strName = ((TextBox)CommonPopup1.FindControl("txtName")).Text;
        string Refno = ((TextBox)CommonPopup1.FindControl("txtRefno")).Text;
        string searchCol = ((HiddenField)CommonPopup1.FindControl("hfColumn")).Value;
        searchCol = searchCol == string.Empty ? "[Name]" : "[" + searchCol + "]";
        Refno = Refno == string.Empty || Refno == null ? "0" : Refno;
        Populate.FillPopUpGrid((GridView)CommonPopup1.FindControl("gdvSearch"), "Customer", "ID_Customer as ID,CustomerName as Name", "ID_Customer", "ID_Customer", "FK_Client=" + hfClient.Value.Split(',')[0] + " AND Active=1 AND Cancelled=0 AND CustomerName ", strName, null);
        Populate = null;
        // Populate.FillGrid((GridView)CommonPopup1.FindControl("gdvSearch"), "AccountHead", "Name , ID_AccountHead, ID_AccountHead As [Ref No],ShortName,(CASE WHEN AccountType=1 then 'NORMAL' WHEN AccountType=2 then 'SYSTEM'  WHEN AccountType=3 Then 'BANK' WHEN AccountType=4 THEN 'CASH' WHEN  AccountType=5 THEN 'CUSTOMER' ELSE 'SUPPLIER' END) as [Account Type]", "ID_AccountHead", "", "FK_Client=" + hfClient.Value.Split(',')[0] + " AND (ID_AccountHead =" + Refno + " OR 0=" + Refno + ") AND Active=1 ", searchCol + "  LIKE '" + strName + "%'", pageIndex, 10, "", (Repeater)CommonPopup1.FindControl("rptPager"), this.Page);
        // new Populate().FillDropdownList(ddlAccountHead, "AccountHead", "Name", "ID_AccountHead", "ID_AccountHead", " Active=1 and FK_Client = " + hfClient.Value, "");
        //Populate.FillGrid((GridView)CommonPopup1.FindControl("gdvSearch"), "Customer", "Name ,ID_Customer as ID,  ID_Customer As [Ref No]", "FK_Client=" + hfClient.Value.Split(',')[0] + " AND (ID_Customer =" + Refno + " OR 0=" + Refno + ") AND Active=1 ", searchCol + "  LIKE '" + strName + "%'", pageIndex, 10, "", (Repeater)CommonPopup1.FindControl("rptPager"), this.Page);
        //Populate = null;
        //  txtSubHeadRefno.Focus();
       if (txtcustomername.Text == string.Empty)
        {
            lblPopupHeader.Text = "Customer";
            mpCommon.Show();
        }

    }



    //protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
    //{

    //}
    protected void ImageButton2_Click(object sender, ImageClickEventArgs e)
    {
        Delete();
    }
    protected void gvAccountHeadSub_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvAccountHeadSub.PageIndex = e.NewPageIndex;
        GridFill(e.NewPageIndex);
    }
}