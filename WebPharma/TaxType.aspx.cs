/*----------------------------------------------------------------------------------------------------------------------------------------------------------
Purpose    : Insert/Update/Delete TaxType 
Author     : Praveen MP
Copyright  : Perfect Software Solutions (Clt) Pvt Ltd
Created on : 18/05/2018
----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
WebPharma
On			By			OMID
----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
22/01/2019 VINISHA   Added GSTType Combo box
                     Changed Account head and Account Sub head Fill
--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using WebPharma.Business.Masters;
using WebPharma.Business;



public partial class TaxType : System.Web.UI.Page
{
    #region Function
    protected void BindData()
    {
       //Populate populate = new Populate();
       //populate.FillBillType(ddlBillType);
       //populate = null;
    }
    protected void GridFill(int currentPage)
    {
        BlTaxType blTaxType = new BlTaxType();

        DataTable dtbl = new DataTable();
        blTaxType.TypeName = txtSearchName.Text.Trim();
        blTaxType.FK_Client = Convert.ToInt64(hfClient.Value);
        blTaxType.PageIndex = Convert.ToInt16(currentPage);
        dtbl = blTaxType.SelectAllData();
        gvBillType.DataSource = dtbl;
        gvBillType.DataBind();
        if (dtbl.Rows.Count > 0)
        {
            //int recordCount = Convert.ToInt32(dtbl.Rows[0]["RecordCount"].ToString());
            //this.PopulatePager(recordCount, currentPage);
        }
        blTaxType = null;
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
        bluseraccess.FK_Pages = 43;
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
        BlTaxType blTaxType = new BlTaxType();
        blTaxType.TypeName = txttypeName.Text;
     
        blTaxType.GSTType = Convert.ToByte(ddlGSTType.SelectedValue);
       
        blTaxType.FK_AccountHead = Convert.ToInt64(txtRefno.Text == "" ? "0" : txtRefno.Text);

        blTaxType.FK_AccountHeadSub = Convert.ToInt64(txtRefno1.Text == "" ? "0" : txtRefno1.Text);

        blTaxType.FK_Client = Convert.ToInt64(hfClient.Value);
        blTaxType.UserCode = hfUserName.Value;// hfUser.Value;
        blTaxType.TaxInterState = chkTaxInterState.Checked;
        blTaxType.TaxIntraState = chkTaxIntraState.Checked;


        if (hfBillTypeId.Value == "0")
        {
            if (hfIsAdd.Value.ToLower() == "true")
            {
                status = blTaxType.InsertData();
                if (status > 0)
                {
                    ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "ShowAddDiv();", true);
                    ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "toastr.success('Data inserted successfully');ClearAdd();", true);
                    GridFill(0);
                    cleared();
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "ShowAddDiv();", true);
                    ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "toastr.error('Cannot insert data, Name already exists')", true);
                    txttypeName.Focus();
                }
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "toastr.warning('User does not have permission to add')", true);
                txttypeName.Focus();

            }
        }
        else
        {
            if (hfIsUpdate.Value.ToLower() == "true")
            {

                blTaxType.MasterId = Convert.ToInt64(hfBillTypeId.Value);
                status = blTaxType.UpdateData();
                if (status > 0)
                {
                    ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "ShowAddDiv();", true);
                    ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "toastr.success('Data updated successfully');ClearAdd();", true);
                    GridFill(1);
                    cleared();
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "ShowAddDiv();", true);
                    ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "toastr.error('Cannot update data, Name already exists')", true);
                    txttypeName.Focus();
                }
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "toastr.warning('User does not have permission to Update')", true);
                txttypeName.Focus();
            }
        }
        blTaxType = null;
    }
    #endregion

    #region Events
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {

            if (!IsPostBack)
            {
                if (Session["Username"] != null && Convert.ToString(Session["Username"]) != "")
                {
                    var username = Session["Username"];
                    hfUser.Value = Request.QueryString["uid"];
                    hfClient.Value =  Request.QueryString["clnt"];
                    hfUserName.Value = username.ToString();
                    Page.Header.DataBind();
                    //  blMessage.Items.Clear();
                    ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "ClearAdd();", true);
                    divAdd.Style.Add("display", "block");
                    BindData();

                    CheckUserAccess();
                    GridFill(1);
                    txttypeName.Focus();
                }
                else
                {
                    Response.Redirect("~/Default.aspx",false);
                  
                }
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
            ///    blMessage.Items.Clear();
            divAdd.Style.Add("display", "none");
            divView.Style.Add("display", "block");
            txtSearchName.Text = "";
            ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "ClearAdd();ClearSearch();", true);
            GridFill(1);
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "toastr.warning('" + ex.Message + "')", true);
        }
    }
    protected void Page_Changed(object sender, EventArgs e)
    {
        //  blMessage.Items.Clear();
        int pageIndex = int.Parse((sender as LinkButton).CommandArgument);

        GridFill(pageIndex);
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            if (chkTaxInterState.Checked == true || chkTaxIntraState.Checked == true)
            {
                // blMessage.Items.Clear();
                SaveData();
                txttypeName.Focus();
            }
            else
            {
                ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "toastr.error('Select Atleast One Tax Type Needed')", true);
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "toastr.warning('" + ex.Message + "')", true);
        }
    }
    protected void gvBillType_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        try
        {
            BlFunction blfunctions = new BlFunction();
            // blMessage.Items.Clear();
            ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "ShowAddDiv();", true);
            BlTaxType blTaxType = new BlTaxType();
            blTaxType.MasterId = Convert.ToInt64(gvBillType.DataKeys[e.NewSelectedIndex]["ID_TaxType"].ToString());
            hfBillTypeId.Value = blTaxType.MasterId.ToString();
            blTaxType.FK_Client = Convert.ToInt64(hfClient.Value);
            blTaxType.SelectData();
            txttypeName.Text = blTaxType.TypeName.ToString();

            txtRefno.Text = blfunctions.GetScalarValue("AccountHead", "ISNULL( ahcode,'') ahcode", " Cancelled=0 and  ID_AccountHead=" + blTaxType.FK_AccountHead.ToString() + "", ""); //blTaxType.FK_AccountHead.ToString();         
            txtAccountHead.Text = blfunctions.GetScalarValue("AccountHead", "ISNULL( HeadName,'') Name", " Cancelled=0 and  AhCode=" + Convert.ToInt64(txtRefno.Text) + "", "");

            txtRefno1.Text = blTaxType.FK_AccountHeadSub.ToString();
            txtAccountHeadSub.Text = blfunctions.GetScalarValue("AccountHeadSub", "ISNULL( HeadSubName,'') Name", " Cancelled=0 and AhsCode=" + Convert.ToInt64(txtRefno1.Text) + "AND AhCode="+ Convert.ToInt64(txtRefno.Text) +"", "");
            chkTaxInterState.Checked = blTaxType.TaxInterState;
            chkTaxIntraState.Checked = blTaxType.TaxIntraState;
            ddlGSTType.SelectedValue = blTaxType.GSTType.ToString();

            blTaxType = null;
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "toastr.warning('" + ex.Message + "')", true);
        }
    }
    protected void gvBillType_RowDeleting(object sender, GridViewDeleteEventArgs e)
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

                hfMasterID.Value = gvBillType.DataKeys[e.RowIndex]["ID_TaxType"].ToString();

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


    
    #endregion

    protected void btnClear_Click(object sender, EventArgs e)
    {
        cleared();
        txtRefno.Text = "";
        txtAccountHead.Text = "";
        txttypeName.Focus();


        ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "ClearAdd();", true);
        divAdd.Style.Add("display", "block");
        divView.Style.Add("display", "none");

    }

private void cleared()
{
      txtRefno.Text = "";
     txtRefno1.Text = "";
      
     txtAccountHead.Text = "";
     txtAccountHeadSub.Text = "";
   
      txttypeName.Text = "";
      txttypeName.Focus();
      divAdd.Style.Add("display", "block");
      divView.Style.Add("display", "none");
	
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

            // chkActive.Focus();
        }

        if (CommonPopup1.CallingFunction == "AccountHeadsubFill")
        {
            txtRefno1.Text = strRefno;
            txtAccountHeadSub.Text = strName;
            // chkActive.Focus();
        }

      

    }
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
        Populate.FillPopUpGrid((GridView)CommonPopup1.FindControl("gdvSearch"), "AccountHeadSub AHS LEFT JOIN Customer C ON C.ID_Customer= AHS.Fk_Cuscode AND C.Active=1", "AHS.AhsCode as ID,AHS.HeadSubName as Name", "ID_AccountHeadSub", "ID_AccountHeadSub", " AHS.Ahcode = " + txtRefno.Text + " AND AHS.FK_Client=" + hfClient.Value.Split(',')[0] + "  AND AHS.Cancelled=0 AND HeadSubName ", strName, null);
        Populate = null;
        //  txtSubHeadRefno.Focus();
        if (txtAccountHeadSub.Text == string.Empty)
        {
            lblPopupHeader.Text = "Account HeadSub";
            mpCommon.Show();
        }

    }
   

   
    protected void ImageButton1_Click1(object sender, ImageClickEventArgs e)
    {
        cleared();
    }
    protected void chkCredit_CheckedChanged(object sender, EventArgs e)
    {
        
    }
    protected void gvBillType_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvBillType.PageIndex = e.NewPageIndex;
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
            BlTaxType blTaxType = new BlTaxType();
            blTaxType.MasterId = Convert.ToInt64(hfMasterID.Value);
            blTaxType.FK_Client = Convert.ToInt64(hfClient.Value);
            blTaxType.UserCode = hfUserName.Value;
            blTaxType.FK_Reason = Convert.ToInt64(Reason);
 
            if (hfIsDelete.Value.ToLower() == "true")
            {
                status = blTaxType.DeleteData();
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
            blTaxType = null;
        }



    }

}