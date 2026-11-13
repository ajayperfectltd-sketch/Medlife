/*----------------------------------------------------------------------------------------------------------------------------------------------------------
Purpose    : Insert/Update/Delete BillType 
Author     : Bibin TC
Copyright  : Perfect Software Solutions (Clt) Pvt Ltd
Created on : 03/05/2018
----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
WebPharma
On			By			OMID
----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
22/01/2019 VINISHA    Changed Account head and Account Sub head Fill
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



public partial class BillType : System.Web.UI.Page
{
    #region Function
    protected void BindData()
    {
       Populate populate = new Populate();
       populate.FillBillType(ddlBillType);
       populate = null;
    }
    protected void GridFill(int currentPage)
    {
        BlBillType blBillType = new BlBillType();

        DataTable dtbl = new DataTable();
        blBillType.TypeName = txtSearchName.Text.Trim();
        blBillType.FK_Client = Convert.ToInt64(hfClient.Value);
        blBillType.PageIndex = Convert.ToInt16(currentPage);
        dtbl = blBillType.SelectAllData();
        gvBillType.DataSource = dtbl;
        gvBillType.DataBind();
        //if (dtbl.Rows.Count > 0)
        //{
        //    int recordCount = Convert.ToInt32(dtbl.Rows[0]["RecordCount"].ToString());
        //    this.PopulatePager(recordCount, currentPage);
        //}
        blBillType = null;
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
        bluseraccess.FK_Pages = 35;
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
        BlBillType blBillType = new BlBillType();
        blBillType.TypeName = txttypeName.Text;

        blBillType.BillType = Convert.ToInt32(ddlBillType.SelectedValue);
        blBillType.FK_AccountHead = Convert.ToInt64(txtRefno.Text == "" ? "0": txtRefno.Text);
        
        blBillType.FK_AccountHeadSub = Convert.ToInt64(txtRefno1.Text==""?"0":txtRefno1.Text);
      

       

      

        blBillType.FK_AccountReturn = Convert.ToInt64(txtRefno24.Text == "" ? "0" : txtRefno24.Text);
        blBillType.FK_AccountSubReturn = Convert.ToInt64(txtRefno25.Text == "" ? "0" : txtRefno25.Text);

        blBillType.FK_AccountHeadDiscount = Convert.ToInt64(txtRefno28.Text == "" ? "0" : txtRefno28.Text);
        blBillType.FK_AccountHeadSubDiscount = Convert.ToInt64(txtRefno29.Text == "" ? "0" : txtRefno29.Text);

        blBillType.FK_AccountHeadRounding = Convert.ToInt64(txtRefno30.Text == "" ? "0" : txtRefno30.Text);
        blBillType.FK_AccountHeadSubRounding = Convert.ToInt64(txtRefno31.Text == "" ? "0" : txtRefno31.Text);

        blBillType.FK_AccountHeadCard = Convert.ToInt64(txtRefno2.Text == "" ? "0" : txtRefno2.Text);
        blBillType.FK_AccountHeadSubCard = Convert.ToInt64(txtRefno3.Text == "" ? "0" : txtRefno3.Text);
        blBillType.FK_AccountHeadCheque = Convert.ToInt64(txtRefno4.Text == "" ? "0" : txtRefno4.Text);
        blBillType.FK_AccountHeadSubCheque = Convert.ToInt64(txtRefno5.Text == "" ? "0" : txtRefno5.Text);


        blBillType.FK_AccountCredit = Convert.ToInt64(txtRefno32.Text == "" ? "0" : txtRefno32.Text);
            blBillType.FK_Client = Convert.ToInt64(hfClient.Value);
           blBillType.UserCode = hfUsername.Value;
          blBillType.GovtSuppliers = Convert.ToBoolean(chkGovtSuppliers.Checked);
          blBillType.Credit = Convert.ToBoolean(chkCredit.Checked);









        if (hfBillTypeId.Value == "0")
        {
            if (hfIsAdd.Value.ToLower() == "true")
            {
                status = blBillType.InsertData();
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

                blBillType.MasterId = Convert.ToInt64(hfBillTypeId.Value);
                status = blBillType.UpdateData();
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
        blBillType = null;
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
    
                    hfUsername.Value = Session["Username"].ToString();
                    hfUser.Value = Request.QueryString["uid"];
                    hfClient.Value = Request.QueryString["clnt"];
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
                    Response.Redirect("~/Default.aspx", false);
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
            //   blMessage.Items.Clear();
            SaveData();
            txttypeName.Focus();
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
            BlBillType blBillType = new BlBillType();
            blBillType.MasterId = Convert.ToInt64(gvBillType.DataKeys[e.NewSelectedIndex]["ID_BillType"].ToString());
            hfBillTypeId.Value = blBillType.MasterId.ToString();
            blBillType.FK_Client = Convert.ToInt64(hfClient.Value);
            blBillType.SelectData();
            txttypeName.Text = blBillType.TypeName.ToString();

            ddlBillType.SelectedValue = blBillType.BillType.ToString();

            txtRefno.Text = blBillType.FK_AccountHead.ToString();

            var dbname = blfunctions.GetScalarValue("Settings", "ISNULL( Value,0)", " Module = 'SEC' AND SubModule = 'SEC' AND NAME = 'SEC05'" , "");


            //txtAccountHead.Text = blfunctions.GetScalarValue("AccountHead", "ISNULL( Name,'') Name", " Cancelled=0 and Active=1 and  AhCode=" + Convert.ToInt64(txtRefno.Text) + "", "");
            txtAccountHead.Text = blfunctions.GetScalarValue("AccountHead", "ISNULL( HeadName,'') Name", " Cancelled=0 and  ID_AccountHead=" + Convert.ToInt64(txtRefno.Text) + "", "");

            txtRefno1.Text = blBillType.FK_AccountHeadSub.ToString();
            //txtAccountHeadSub.Text = blfunctions.GetScalarValue("AccountHeadSub", "ISNULL( Name,'') Name", " Cancelled=0 and Active=1 and  ID_AccountHeadSub=" + Convert.ToInt64(txtRefno1.Text) + "", "");
            txtAccountHeadSub.Text = blfunctions.GetScalarValue("AccountHeadSub", "ISNULL( HeadSubName,'') Name", " Cancelled=0 and ID_AccountHeadSub=" + Convert.ToInt64(txtRefno1.Text) + "AND FK_AccountHead=" + Convert.ToInt64(txtRefno.Text) + "", "");

            txtRefno2.Text = blBillType.FK_AccountHeadCard.ToString();
            txtAccountHeadCard.Text = blfunctions.GetScalarValue("AccountHead", "ISNULL( HeadName,'') Name", " Cancelled=0 and  ID_AccountHead=" + Convert.ToInt64(txtRefno2.Text) + "", "");

            txtRefno3.Text = blBillType.FK_AccountHeadSubCard.ToString();
            txtAccountHeadSubCard.Text = blfunctions.GetScalarValue("AccountHeadSub", "ISNULL( HeadSubName,'') Name", " Cancelled=0 and ID_AccountHeadSub=" + Convert.ToInt64(txtRefno3.Text) + "AND FK_AccountHead=" + Convert.ToInt64(txtRefno2.Text) + "", "");

            txtRefno4.Text = blBillType.FK_AccountHeadCheque.ToString();
            txtAccountHeadCheque.Text = blfunctions.GetScalarValue("AccountHead", "ISNULL( HeadName,'') Name", " Cancelled=0 and  ID_AccountHead=" + Convert.ToInt64(txtRefno4.Text) + "", "");

            txtRefno5.Text = blBillType.FK_AccountHeadSubCheque.ToString();
            txtAccountHeadSubCheque.Text = blfunctions.GetScalarValue("AccountHeadSub", "ISNULL( HeadSubName,'') Name", " Cancelled=0 and ID_AccountHeadSub=" + Convert.ToInt64(txtRefno5.Text) + "AND FK_AccountHead=" + Convert.ToInt64(txtRefno4.Text) + "", "");


            txtRefno24.Text = blBillType.FK_AccountReturn.ToString();
            //txtreturnacchead.Text = blfunctions.GetScalarValue("AccountHead", "ISNULL( Name,'') Name", " Cancelled=0 and Active=1 and  AhCode=" + Convert.ToInt64(txtRefno24.Text) + "", "");
            txtreturnacchead.Text = blfunctions.GetScalarValue("AccountHead", "ISNULL( HeadName,'') Name", " Cancelled=0 and  ID_AccountHead=" + Convert.ToInt64(txtRefno24.Text) + "", "");

            txtRefno25.Text = blBillType.FK_AccountSubReturn.ToString();
            //txtreturnaccheadsub.Text = blfunctions.GetScalarValue("AccountHeadSub", "ISNULL( Name,'') Name", " Cancelled=0 and Active=1 and  ID_AccountHeadSub=" + Convert.ToInt64(txtRefno25.Text) + "", "");
            txtreturnaccheadsub.Text = blfunctions.GetScalarValue("AccountHeadSub", "ISNULL( HeadSubName,'') Name", " Cancelled=0 and ID_AccountHeadSub=" + Convert.ToInt64(txtRefno25.Text) + "AND FK_AccountHead=" + Convert.ToInt64(txtRefno24.Text) + "", "");
            
            txtRefno28.Text = blBillType.FK_AccountHeadDiscount.ToString();
            //txtDiscountAccHead.Text = blfunctions.GetScalarValue("AccountHead", "ISNULL( Name,'') Name", " Cancelled=0 and Active=1 and  AhCode=" + Convert.ToInt64(txtRefno28.Text) + "", "");
            txtDiscountAccHead.Text = blfunctions.GetScalarValue("AccountHead", "ISNULL( HeadName,'') Name", " Cancelled=0 and  ID_AccountHead=" + Convert.ToInt64(txtRefno28.Text) + "", "");

            txtRefno29.Text = blBillType.FK_AccountHeadSubDiscount.ToString();
            //txtlDiscountAccSubHead.Text = blfunctions.GetScalarValue("AccountHeadSub", "ISNULL( Name,'') Name", " Cancelled=0 and Active=1 and  ID_AccountHeadSub=" + Convert.ToInt64(txtRefno29.Text) + "", "");
            txtlDiscountAccSubHead.Text = blfunctions.GetScalarValue("AccountHeadSub", "ISNULL( HeadSubName,'') Name", " Cancelled=0 and ID_AccountHeadSub=" + Convert.ToInt64(txtRefno29.Text) + "AND FK_AccountHead=" + Convert.ToInt64(txtRefno28.Text) + "", "");
            

            txtRefno30.Text = blBillType.FK_AccountHeadRounding.ToString();
            //txtRoundingAccountHead.Text = blfunctions.GetScalarValue("AccountHead", "ISNULL( Name,'') Name", " Cancelled=0 and Active=1 and  AhCode=" + Convert.ToInt64(txtRefno30.Text) + "", "");
            txtRoundingAccountHead.Text = blfunctions.GetScalarValue("AccountHead", "ISNULL( HeadName,'') Name", " Cancelled=0 and  ID_AccountHead=" + Convert.ToInt64(txtRefno30.Text) + "", "");

            txtRefno31.Text = blBillType.FK_AccountHeadSubRounding.ToString();
            //txtRoundingAccountHeadsub.Text = blfunctions.GetScalarValue("AccountHeadSub", "ISNULL( Name,'') Name", " Cancelled=0 and Active=1 and  ID_AccountHeadSub=" + Convert.ToInt64(txtRefno31.Text) + "", "");
            txtRoundingAccountHeadsub.Text = blfunctions.GetScalarValue("AccountHeadSub", "ISNULL( HeadSubName,'') Name", " Cancelled=0 and ID_AccountHeadSub=" + Convert.ToInt64(txtRefno31.Text) + "AND FK_AccountHead=" + Convert.ToInt64(txtRefno30.Text) + "", "");
            

            txtRefno32.Text = blBillType.FK_AccountCredit.ToString();
            //txtcreditAccHead.Text = blfunctions.GetScalarValue("AccountHead", "ISNULL( Name,'') Name", " Cancelled=0 and Active=1 and  AhCode=" + Convert.ToInt64(txtRefno32.Text) + "", "");
            txtcreditAccHead.Text = blfunctions.GetScalarValue("AccountHead", "ISNULL( HeadName,'') Name", " Cancelled=0 and  ID_AccountHead=" + Convert.ToInt64(txtRefno32.Text) + "", "");

            chkGovtSuppliers.Checked = blBillType.GovtSuppliers;
            chkCredit.Checked = blBillType.Credit;



            blBillType = null;
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
             
                hfMasterID.Value = gvBillType.DataKeys[e.RowIndex]["ID_BillType"].ToString();
              
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
        txtRefno2.Text = "";
        txtRefno3.Text = "";
        txtRefno4.Text = "";
        txtRefno5.Text = "";

        txtRefno24.Text = "";
        txtRefno25.Text = "";

        txtRefno28.Text = "";
        txtRefno29.Text = "";
        txtRefno30.Text = "";
        txtRefno31.Text = "";
        txtRefno32.Text = "";
        txtAccountHead.Text = "";
        txtAccountHeadSub.Text = "";

        txtreturnacchead.Text = "";
        txtreturnaccheadsub.Text = "";

        txtcreditAccHead.Text = "";

        txtDiscountAccHead.Text = "";
        txtlDiscountAccSubHead.Text = "";
        txtRoundingAccountHead.Text = "";
        txtRoundingAccountHeadsub.Text = "";
        txtAccountHeadCard.Text = "";
        txtAccountHeadSubCard.Text = "";
        txtAccountHeadCheque.Text = "";
        txtAccountHeadSubCheque.Text = "";

        txttypeName.Text = "";
        ddlBillType.SelectedValue = "0";
        chkCredit.Checked = false;
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

        if (CommonPopup1.CallingFunction == "AccountHeadCardfill")
        {
            txtRefno2.Text = strRefno;
            txtAccountHeadCard.Text = strName;
            // chkActive.Focus();
        }
        if (CommonPopup1.CallingFunction == "AccountHeadCardsubfill")
        {
            txtRefno3.Text = strRefno;
            txtAccountHeadSubCard.Text = strName;
            // chkActive.Focus();
        }
        if (CommonPopup1.CallingFunction == "AccountHeadchequeFill")
        {
            txtRefno4.Text = strRefno;
            txtAccountHeadCheque.Text = strName;
            // chkActive.Focus();
        }
        if (CommonPopup1.CallingFunction == "AccountHeadchequesubFill")
        {
            txtRefno5.Text = strRefno;
            txtAccountHeadSubCheque.Text = strName;
            // chkActive.Focus();
        }
        if (CommonPopup1.CallingFunction == "AccountHeadvatFill")
        {
            //txtRefno6.Text = strRefno;
            //txtVatAccountHead.Text = strName;
            //// chkActive.Focus();
        }
        if (CommonPopup1.CallingFunction == "AccountHeadvatsubFill")
        {
            //txtRefno7.Text = strRefno;
            //txtVatAccountHeadSub.Text = strName;
            //// chkActive.Focus();
        }
        if (CommonPopup1.CallingFunction == "AccountHeadCGSTFill")
        {
            //txtRefno8.Text = strRefno;
            //txtCGSTAccountHead.Text = strName;
            //// chkActive.Focus();
        }
        if (CommonPopup1.CallingFunction == "AccountHeadCGSTsubFill")
        {
            //txtRefno9.Text = strRefno;
            //txtCGSTAccountHeadSub.Text = strName;
            //// chkActive.Focus();
        }
        if (CommonPopup1.CallingFunction == "AccountHeadSGSTFill")
        {
            //txtRefno10.Text = strRefno;
            //TXTSGSTAccountHead.Text = strName;
            //// chkActive.Focus();
        }
        if (CommonPopup1.CallingFunction == "AccountHeadsubSGSTFill")
        {
            //txtRefno11.Text = strRefno;
            //TXTSGSTAccountHeadSub.Text = strName;
            //// chkActive.Focus();
        }
        if (CommonPopup1.CallingFunction == "AccountHeadIGSTFill")
        {
            //txtRefno12.Text = strRefno;
            //TXTIGSTAccountHead.Text = strName;
            //// chkActive.Focus();
        }
        if (CommonPopup1.CallingFunction == "AccountHeadSUBIGSTFill")
        {
            //txtRefno13.Text = strRefno;
            //TXTIGSTAccountHeadSub.Text = strName;
            //// chkActive.Focus();
        }
        if (CommonPopup1.CallingFunction == "AccountHeadCESSFill")
        {
            //txtRefno14.Text = strRefno;
            //TXTCESSAccountHead.Text = strName;
            //// chkActive.Focus();
        }
        if (CommonPopup1.CallingFunction == "AccountHeadSUBCESSFill")
        {
            //txtRefno15.Text = strRefno;
            //TXTCESSAccountHeadSub.Text = strName;
            //// chkActive.Focus();
        }
        if (CommonPopup1.CallingFunction == "AccountHeadLoadingFill")
        {
            //txtRefno16.Text = strRefno;
            //txtAccountHeadLoading.Text = strName;
            //// chkActive.Focus();
        }
        if (CommonPopup1.CallingFunction == "AccountHeadsubLoadingFill")
        {
            //txtRefno17.Text = strRefno;
            //txtAccountHeadSubLoading.Text = strName;
            //// chkActive.Focus();
        }
        if (CommonPopup1.CallingFunction == "AccountHeadunLoadingFill")
        {
            //txtRefno18.Text = strRefno;
            //txtAccountHeadunLoading.Text = strName;
            //// chkActive.Focus();
        }
        if (CommonPopup1.CallingFunction == "AccountHeadsubunLoadingFill")
        {
            //txtRefno19.Text = strRefno;
            //txtAccountHeadSubunLoading.Text = strName;
            //// chkActive.Focus();
        }
        if (CommonPopup1.CallingFunction == "AccountHeadDisplayFill")
        {
            //txtRefno20.Text = strRefno;
            //txtdisplayAccHead.Text = strName;
            //// chkActive.Focus();
        }
        if (CommonPopup1.CallingFunction == "AccountHeadsubDisplayFill")
        {
            //txtRefno21.Text = strRefno;
            //txtdisplayAccHeadsub.Text = strName;
            //// chkActive.Focus();
        }
        if (CommonPopup1.CallingFunction == "AccountHeadOthersFill")
        {
            //txtRefno22.Text = strRefno;
            //txtotherAccHead.Text = strName;
            //// chkActive.Focus();
        }
        if (CommonPopup1.CallingFunction == "AccountHeadsubOthersFill")
        {
            //txtRefno23.Text = strRefno;
            //txtotherAccHeadsub.Text = strName;
            //// chkActive.Focus();
        }
        if (CommonPopup1.CallingFunction == "AccountHeadReturnFill")
        {
            txtRefno24.Text = strRefno;
            txtreturnacchead.Text = strName;
            // chkActive.Focus();
        }
        if (CommonPopup1.CallingFunction == "AccountHeadsubReturnFill")
        {
            txtRefno25.Text = strRefno;
            txtreturnaccheadsub.Text = strName;
            // chkActive.Focus();
        }
        if (CommonPopup1.CallingFunction == "AccountHeadfrightFill")
        {
            //txtRefno26.Text = strRefno;
            //txtFrightAccHead.Text = strName;
            //// chkActive.Focus();
        }
        if (CommonPopup1.CallingFunction == "AccountHeadsubfrightFill")
        {
            //txtRefno27.Text = strRefno;
            //txtFrightAccSubHead.Text = strName;
            //// chkActive.Focus();
        }
        if (CommonPopup1.CallingFunction == "AccountHeadDiscountFill")
        {
            txtRefno28.Text = strRefno;
            txtDiscountAccHead.Text = strName;
            // chkActive.Focus();
        }
        if (CommonPopup1.CallingFunction == "AccountHeadsubDiscountFill")
        {
            txtRefno29.Text = strRefno;
            txtlDiscountAccSubHead.Text = strName;
            // chkActive.Focus();
        }
        if (CommonPopup1.CallingFunction == "AccountHeadRoundingFill")
        {
            txtRefno30.Text = strRefno;
            txtRoundingAccountHead.Text = strName;
            // chkActive.Focus();
        }
        if (CommonPopup1.CallingFunction == "AccountHeadsubRoundingFill")
        {
            txtRefno31.Text = strRefno;
            txtRoundingAccountHeadsub.Text = strName;
            // chkActive.Focus();
        }
        if (CommonPopup1.CallingFunction == "AccountHeadCreditFill")
        {
            txtRefno32.Text = strRefno;
            txtcreditAccHead.Text = strName;
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

        Populate.FillPopUpGrid((GridView)CommonPopup1.FindControl("gdvSearch"), "AccountHead", "ID_AccountHead as ID,HeadName as Name", "ID_AccountHead", "ID_AccountHead", "FK_Client=" + hfClient.Value.Split(',')[0] + "  AND Cancelled=0 AND HeadName ", strName, null);
        //Populate.FillPopUpGrid((GridView)CommonPopup1.FindControl("gdvSearch"), "AccountHead", "AhCode as ID,HeadName as Name", "ID_AccountHead", "ID_AccountHead", "FK_Client=" + hfClient.Value.Split(',')[0] + "  AND Cancelled=0 AND HeadName ", strName, null);
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

        Populate.FillPopUpGrid((GridView)CommonPopup1.FindControl("gdvSearch"), "AccountHeadSub AHS LEFT JOIN Customer C ON C.ID_Customer= AHS.Fk_Cuscode AND C.Active=1", "AHS.ID_AccountHeadSub as ID,AHS.HeadSubName as Name", "ID_AccountHeadSub", "ID_AccountHeadSub", " AHS.FK_AccountHead = " + txtRefno.Text + " AND AHS.FK_Client=" + hfClient.Value.Split(',')[0] + "  AND AHS.Cancelled=0 AND HeadSubName ", strName, null);
       
        //Populate.FillGrid((GridView)CommonPopup1.FindControl("gdvSearch"), "AccountHeadSub", "Name ,ID_AccountHeadSub as ID,  ID_AccountHeadSub As [Ref No],ShortName", "FK_Client=" + hfClient.Value.Split(',')[0] + " AND (ID_AccountHeadSub =" + Refno + " OR 0=" + Refno + ") AND Active=1 ", searchCol + "  LIKE '" + strName + "%' and Cancelled=0 and Active=1 and  AhCode= '"+txtRefno.Text+"'  ", pageIndex, 10, "", (Repeater)CommonPopup1.FindControl("rptPager"), this.Page);
        Populate = null;
        //  txtSubHeadRefno.Focus();
        if (txtAccountHeadSub.Text == string.Empty)
        {
            lblPopupHeader.Text = "Account HeadSub";
            mpCommon.Show();
        }

    }
    protected void ImageButtonCardAccHead_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            txtAccountHeadCard.Text = "";
            txtRefno2.Text = "";
            txtAccountHeadSubCard.Text = "";
            txtRefno3.Text = "";
            //  ClearAccountHead();
            ((TextBox)CommonPopup1.FindControl("txtName")).Text = txtAccountHeadCard.Text.Trim();
            ((TextBox)CommonPopup1.FindControl("txtRefno")).Text = txtRefno2.Text.Trim();
            ((HiddenField)CommonPopup1.FindControl("hfColumn")).Value = "Name";
            AccountHeadCardfill(1);
            divAdd.Style.Add("display", "block");
            divView.Style.Add("display", "none");



        }
        catch (Exception ex)
        {
        }

    }
    public void AccountHeadCardfill(int pageIndex)
    {
        txtAccountHeadCard.Text = "";
        txtRefno2.Text = "";
        Populate Populate = new Populate();
        CommonPopup1.CallingFunction = "AccountHeadCardfill";
        string strName = ((TextBox)CommonPopup1.FindControl("txtName")).Text;
        string Refno = ((TextBox)CommonPopup1.FindControl("txtRefno")).Text;
        string searchCol = ((HiddenField)CommonPopup1.FindControl("hfColumn")).Value;
        searchCol = searchCol == string.Empty ? "[Name]" : "[" + searchCol + "]";
        Refno = Refno == string.Empty || Refno == null ? "0" : Refno;


        Populate.FillPopUpGrid((GridView)CommonPopup1.FindControl("gdvSearch"), "AccountHead", "ID_AccountHead as ID,HeadName as Name", "ID_AccountHead", "ID_AccountHead", "FK_Client=" + hfClient.Value.Split(',')[0] + "  AND Cancelled=0 AND HeadName ", strName, null);
        //Populate.FillGrid((GridView)CommonPopup1.FindControl("gdvSearch"), "AccountHead", "Name ,AhCode as ID,  AhCode As [Ref No],ShortName", "FK_Client=" + hfClient.Value.Split(',')[0] + " AND (ID_AccountHead =" + Refno + " OR 0=" + Refno + ") AND Active=1 ", searchCol + "  LIKE '" + strName + "%' and Cancelled=0 and Active=1  ", pageIndex, 10, "", (Repeater)CommonPopup1.FindControl("rptPager"), this.Page, 1);
        Populate = null;
        //  txtSubHeadRefno.Focus();
        if (txtAccountHeadCard.Text == string.Empty)
        {
            lblPopupHeader.Text = "Account Head Card";
            mpCommon.Show();
        }

    }
    protected void ImageButtonCardAccHeadsub_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            txtAccountHeadSubCard.Text = "";
            txtRefno3.Text = "";
            //  ClearAccountHead();
            ((TextBox)CommonPopup1.FindControl("txtName")).Text = txtAccountHeadSubCard.Text.Trim();
            ((TextBox)CommonPopup1.FindControl("txtRefno")).Text = txtRefno3.Text.Trim();
            ((HiddenField)CommonPopup1.FindControl("hfColumn")).Value = "Name";
            AccountHeadCardsubfill(1);
            divAdd.Style.Add("display", "block");
            divView.Style.Add("display", "none");



        }
        catch (Exception ex)
        {
        }


    }
    public void AccountHeadCardsubfill(int pageIndex)
    {

        txtAccountHeadSubCard.Text = "";
        txtRefno3.Text = "";
        Populate Populate = new Populate();
        CommonPopup1.CallingFunction = "AccountHeadCardsubfill";
        string strName = ((TextBox)CommonPopup1.FindControl("txtName")).Text;
        string Refno = ((TextBox)CommonPopup1.FindControl("txtRefno")).Text;
        string searchCol = ((HiddenField)CommonPopup1.FindControl("hfColumn")).Value;
        searchCol = searchCol == string.Empty ? "[Name]" : "[" + searchCol + "]";
        Refno = Refno == string.Empty || Refno == null ? "0" : Refno;

        Populate.FillPopUpGrid((GridView)CommonPopup1.FindControl("gdvSearch"), "AccountHeadSub AHS LEFT JOIN Customer C ON C.ID_Customer= AHS.Fk_Cuscode AND C.Active=1", "AHS.ID_AccountHeadSub as ID,AHS.HeadSubName as Name", "ID_AccountHeadSub", "ID_AccountHeadSub", " AHS.FK_AccountHead = " + txtRefno2.Text + " AND AHS.FK_Client=" + hfClient.Value.Split(',')[0] + "  AND AHS.Cancelled=0 AND HeadSubName ", strName, null);
       
        //Populate.FillGrid((GridView)CommonPopup1.FindControl("gdvSearch"), "AccountHeadSub", "Name ,ID_AccountHeadSub as ID,  ID_AccountHeadSub As [Ref No],ShortName", "FK_Client=" + hfClient.Value.Split(',')[0] + " AND (ID_AccountHeadSub =" + Refno + " OR 0=" + Refno + ") AND Active=1 ", searchCol + "  LIKE '" + strName + "%' and Cancelled=0 and Active=1 and AhCode= '" + txtRefno2.Text + "' ", pageIndex, 10, "", (Repeater)CommonPopup1.FindControl("rptPager"), this.Page);
        Populate = null;
        //  txtSubHeadRefno.Focus();
        if (txtAccountHeadSubCard.Text == string.Empty)
        {
            lblPopupHeader.Text = "AccountHeadCardsubfill";
            mpCommon.Show();
        }

       


    }

    protected void ImageButtonchequeAccHead_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            txtAccountHeadCheque.Text = "";
            txtRefno4.Text = "";
            txtAccountHeadSubCheque.Text = "";
            txtRefno5.Text = "";
            ((TextBox)CommonPopup1.FindControl("txtName")).Text = txtAccountHeadCheque.Text.Trim();
            ((TextBox)CommonPopup1.FindControl("txtRefno")).Text = txtRefno4.Text.Trim();
            ((HiddenField)CommonPopup1.FindControl("hfColumn")).Value = "Name";
            AccountHeadchequeFill(1);
            divAdd.Style.Add("display", "block");
            divView.Style.Add("display", "none");



        }
        catch (Exception ex)
        {
        }
    }
    public void AccountHeadchequeFill(int pageIndex)
    {
        txtAccountHeadCheque.Text = "";
        txtRefno4.Text = "";
        Populate Populate = new Populate();
        CommonPopup1.CallingFunction = "AccountHeadchequeFill";
        string strName = ((TextBox)CommonPopup1.FindControl("txtName")).Text;
        string Refno = ((TextBox)CommonPopup1.FindControl("txtRefno")).Text;
        string searchCol = ((HiddenField)CommonPopup1.FindControl("hfColumn")).Value;
        searchCol = searchCol == string.Empty ? "[Name]" : "[" + searchCol + "]";
        Refno = Refno == string.Empty || Refno == null ? "0" : Refno;

        Populate.FillPopUpGrid((GridView)CommonPopup1.FindControl("gdvSearch"), "AccountHead", "ID_AccountHead as ID,HeadName as Name", "ID_AccountHead", "ID_AccountHead", "FK_Client=" + hfClient.Value.Split(',')[0] + "  AND Cancelled=0 AND HeadName ", strName, null);
        //Populate.FillGrid((GridView)CommonPopup1.FindControl("gdvSearch"), "AccountHead", "Name ,AhCode as ID,  AhCode As [Ref No],ShortName", "FK_Client=" + hfClient.Value.Split(',')[0] + " AND (ID_AccountHead =" + Refno + " OR 0=" + Refno + ") AND Active=1 ", searchCol + "  LIKE '" + strName + "%' and Cancelled=0 and Active=1  ", pageIndex, 10, "", (Repeater)CommonPopup1.FindControl("rptPager"), this.Page, 1);
        Populate = null;
        //  txtSubHeadRefno.Focus();
        if (txtAccountHeadCheque.Text == string.Empty)
        {
            lblPopupHeader.Text = "Account Head Cheque";
            mpCommon.Show();
        }

    }
    protected void ImageButtonchequeAccHeadsub_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            txtAccountHeadSubCheque.Text = "";
            txtRefno5.Text = "";
            //  ClearAccountHead();
            ((TextBox)CommonPopup1.FindControl("txtName")).Text = txtAccountHeadSubCheque.Text.Trim();
            ((TextBox)CommonPopup1.FindControl("txtRefno")).Text = txtRefno5.Text.Trim();
            ((HiddenField)CommonPopup1.FindControl("hfColumn")).Value = "Name";
            AccountHeadchequesubFill(1);
            divAdd.Style.Add("display", "block");
            divView.Style.Add("display", "none");



        }
        catch (Exception ex)
        {
        }
    }
    public void AccountHeadchequesubFill(int pageIndex)
    {

        txtAccountHeadSubCheque.Text = "";
        txtRefno5.Text = "";
        Populate Populate = new Populate();
        CommonPopup1.CallingFunction = "AccountHeadchequesubFill";
        string strName = ((TextBox)CommonPopup1.FindControl("txtName")).Text;
        string Refno = ((TextBox)CommonPopup1.FindControl("txtRefno")).Text;
        string searchCol = ((HiddenField)CommonPopup1.FindControl("hfColumn")).Value;
        searchCol = searchCol == string.Empty ? "[Name]" : "[" + searchCol + "]";
        Refno = Refno == string.Empty || Refno == null ? "0" : Refno;

        Populate.FillPopUpGrid((GridView)CommonPopup1.FindControl("gdvSearch"), "AccountHeadSub AHS LEFT JOIN Customer C ON C.ID_Customer= AHS.Fk_Cuscode AND C.Active=1", "AHS.ID_AccountHeadSub as ID,AHS.HeadSubName as Name", "ID_AccountHeadSub", "ID_AccountHeadSub", " AHS.FK_AccountHead = " + txtRefno4.Text + " AND AHS.FK_Client=" + hfClient.Value.Split(',')[0] + "  AND AHS.Cancelled=0 AND HeadSubName ", strName, null);
       
        //Populate.FillGrid((GridView)CommonPopup1.FindControl("gdvSearch"), "AccountHeadSub", "Name ,ID_AccountHeadSub as ID,  ID_AccountHeadSub As [Ref No],ShortName", "FK_Client=" + hfClient.Value.Split(',')[0] + " AND (ID_AccountHeadSub =" + Refno + " OR 0=" + Refno + ") AND Active=1  ", searchCol + "  LIKE '" + strName + "%' and Cancelled=0 and Active=1 and AhCode= '" + txtRefno4.Text + "'  ", pageIndex, 10, "", (Repeater)CommonPopup1.FindControl("rptPager"), this.Page);
        Populate = null;
        //  txtSubHeadRefno.Focus();
        if (txtAccountHeadSubCheque.Text == string.Empty)
        {
            lblPopupHeader.Text = "Account HeadSub cheque";
            mpCommon.Show();
        }

    }


    public void AccountHeadvatFill(int pageIndex)
    {
        //txtVatAccountHead.Text = "";
        //txtRefno6.Text = "";
        //Populate Populate = new Populate();
        //CommonPopup1.CallingFunction = "AccountHeadvatFill";
        //string strName = ((TextBox)CommonPopup1.FindControl("txtName")).Text;
        //string Refno = ((TextBox)CommonPopup1.FindControl("txtRefno")).Text;
        //string searchCol = ((HiddenField)CommonPopup1.FindControl("hfColumn")).Value;
        //searchCol = searchCol == string.Empty ? "[Name]" : "[" + searchCol + "]";
        //Refno = Refno == string.Empty || Refno == null ? "0" : Refno;

        //Populate.FillGrid((GridView)CommonPopup1.FindControl("gdvSearch"), "AccountHead", "Name ,AhCode as ID,  AhCode As [Ref No],ShortName", "FK_Client=" + hfClient.Value.Split(',')[0] + " AND (ID_AccountHead =" + Refno + " OR 0=" + Refno + ") AND Active=1 ", searchCol + "  LIKE '" + strName + "%' and Cancelled=0 and Active=1  ", pageIndex, 10, "", (Repeater)CommonPopup1.FindControl("rptPager"), this.Page,1);
        //Populate = null;
        ////  txtSubHeadRefno.Focus();
        //if (txtVatAccountHead.Text == string.Empty)
        //{
        //    lblPopupHeader.Text = "Account Headvat";
        //    mpCommon.Show();
        //}

    }

    protected void ImageButtonVatAccHead_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            //txtVatAccountHead.Text = "";
            //txtRefno6.Text = "";
            //((TextBox)CommonPopup1.FindControl("txtName")).Text = txtVatAccountHead.Text.Trim();
            //((TextBox)CommonPopup1.FindControl("txtRefno")).Text = txtRefno6.Text.Trim();
            //((HiddenField)CommonPopup1.FindControl("hfColumn")).Value = "Name";
            //AccountHeadvatFill(1);
            //divAdd.Style.Add("display", "block");
            //divView.Style.Add("display", "none");



        }
        catch (Exception ex)
        {
        }

    }
    protected void ImageButtonVatAccHeadsub_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            //txtVatAccountHeadSub.Text = "";
            //txtRefno7.Text = "";
            ////  ClearAccountHead();
            //((TextBox)CommonPopup1.FindControl("txtName")).Text = txtVatAccountHeadSub.Text.Trim();
            //((TextBox)CommonPopup1.FindControl("txtRefno")).Text = txtRefno7.Text.Trim();
            //((HiddenField)CommonPopup1.FindControl("hfColumn")).Value = "Name";
            //AccountHeadvatsubFill(1);
            //divAdd.Style.Add("display", "block");
            //divView.Style.Add("display", "none");



        }
        catch (Exception ex)
        {
        }
    }
    public void AccountHeadvatsubFill(int pageIndex)
    {
        //txtVatAccountHeadSub.Text = "";
        //txtRefno7.Text = "";
        //Populate Populate = new Populate();
        //CommonPopup1.CallingFunction = "AccountHeadvatsubFill";
        //string strName = ((TextBox)CommonPopup1.FindControl("txtName")).Text;
        //string Refno = ((TextBox)CommonPopup1.FindControl("txtRefno")).Text;
        //string searchCol = ((HiddenField)CommonPopup1.FindControl("hfColumn")).Value;
        //searchCol = searchCol == string.Empty ? "[Name]" : "[" + searchCol + "]";
        //Refno = Refno == string.Empty || Refno == null ? "0" : Refno;

        //Populate.FillGrid((GridView)CommonPopup1.FindControl("gdvSearch"), "AccountHeadSub", "Name ,ID_AccountHeadSub as ID,  ID_AccountHeadSub As [Ref No],ShortName", "FK_Client=" + hfClient.Value.Split(',')[0] + " AND (ID_AccountHeadSub =" + Refno + " OR 0=" + Refno + ") AND Active=1 ", searchCol + "  LIKE '" + strName + "%' and Cancelled=0 and Active=1 and AhCode= '" + txtRefno6.Text + "'  ", pageIndex, 10, "", (Repeater)CommonPopup1.FindControl("rptPager"), this.Page);
        //Populate = null;
        ////  txtSubHeadRefno.Focus();
        //if (txtVatAccountHeadSub.Text == string.Empty)
        //{
        //    lblPopupHeader.Text = "Account HeadSubvat";
        //    mpCommon.Show();
        //}

    }
    protected void ImageButtonCGSTAccHead_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            //txtCGSTAccountHead.Text = "";
            //txtRefno8.Text = "";
            //((TextBox)CommonPopup1.FindControl("txtName")).Text = txtCGSTAccountHead.Text.Trim();
            //((TextBox)CommonPopup1.FindControl("txtRefno")).Text = txtRefno8.Text.Trim();
            //((HiddenField)CommonPopup1.FindControl("hfColumn")).Value = "Name";
            //AccountHeadCGSTFill(1);
            //divAdd.Style.Add("display", "block");
            //divView.Style.Add("display", "none");



        }
        catch (Exception ex)
        {
        }
    }
    public void AccountHeadCGSTFill(int pageIndex)
    {
        //txtCGSTAccountHead.Text = "";
        //txtRefno8.Text = "";
        //Populate Populate = new Populate();
        //CommonPopup1.CallingFunction = "AccountHeadCGSTFill";
        //string strName = ((TextBox)CommonPopup1.FindControl("txtName")).Text;
        //string Refno = ((TextBox)CommonPopup1.FindControl("txtRefno")).Text;
        //string searchCol = ((HiddenField)CommonPopup1.FindControl("hfColumn")).Value;
        //searchCol = searchCol == string.Empty ? "[Name]" : "[" + searchCol + "]";
        //Refno = Refno == string.Empty || Refno == null ? "0" : Refno;

        //Populate.FillGrid((GridView)CommonPopup1.FindControl("gdvSearch"), "AccountHead", "Name ,AhCode as ID,  AhCode As [Ref No],ShortName", "FK_Client=" + hfClient.Value.Split(',')[0] + " AND (ID_AccountHead =" + Refno + " OR 0=" + Refno + ") AND Active=1 ", searchCol + "  LIKE '" + strName + "%' and Cancelled=0 and Active=1  ", pageIndex, 10, "", (Repeater)CommonPopup1.FindControl("rptPager"), this.Page,1);
        //Populate = null;
        ////  txtSubHeadRefno.Focus();
        //if (txtCGSTAccountHead.Text == string.Empty)
        //{
        //    lblPopupHeader.Text = "Account HeadCGST";
        //    mpCommon.Show();
        //}

    }
    protected void ImageButtonCGSTAccHeadsub_Click(object sender, ImageClickEventArgs e)
    {

        try
        {
            //txtCGSTAccountHeadSub.Text = "";
            //txtRefno9.Text = "";
            ////  ClearAccountHead();
            //((TextBox)CommonPopup1.FindControl("txtName")).Text = txtCGSTAccountHeadSub.Text.Trim();
            //((TextBox)CommonPopup1.FindControl("txtRefno")).Text = txtRefno9.Text.Trim();
            //((HiddenField)CommonPopup1.FindControl("hfColumn")).Value = "Name";
            //AccountHeadCGSTsubFill(1);
            //divAdd.Style.Add("display", "block");
            //divView.Style.Add("display", "none");



        }
        catch (Exception ex)
        {
        }

    }

    public void AccountHeadCGSTsubFill(int pageIndex)
    {

        //txtCGSTAccountHeadSub.Text = "";
        //txtRefno9.Text = "";
        //Populate Populate = new Populate();
        //CommonPopup1.CallingFunction = "AccountHeadCGSTsubFill";
        //string strName = ((TextBox)CommonPopup1.FindControl("txtName")).Text;
        //string Refno = ((TextBox)CommonPopup1.FindControl("txtRefno")).Text;
        //string searchCol = ((HiddenField)CommonPopup1.FindControl("hfColumn")).Value;
        //searchCol = searchCol == string.Empty ? "[Name]" : "[" + searchCol + "]";
        //Refno = Refno == string.Empty || Refno == null ? "0" : Refno;

        //Populate.FillGrid((GridView)CommonPopup1.FindControl("gdvSearch"), "AccountHeadSub", "Name ,ID_AccountHeadSub as ID,  ID_AccountHeadSub As [Ref No],ShortName", "FK_Client=" + hfClient.Value.Split(',')[0] + " AND (ID_AccountHeadSub =" + Refno + " OR 0=" + Refno + ") AND Active=1 ", searchCol + "  LIKE '" + strName + "%' and Cancelled=0 and Active=1 and AhCode= '" + txtRefno8.Text + "' ", pageIndex, 10, "", (Repeater)CommonPopup1.FindControl("rptPager"), this.Page);
        //Populate = null;
        ////  txtSubHeadRefno.Focus();
        //if (txtCGSTAccountHeadSub.Text == string.Empty)
        //{
        //    lblPopupHeader.Text = "Account HeadSubCGST";
        //    mpCommon.Show();
        //}

    }

    protected void ImageButtonSGSTAccHead_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            //TXTSGSTAccountHead.Text = "";
            //txtRefno10.Text = "";
            ////  ClearAccountHead();
            //((TextBox)CommonPopup1.FindControl("txtName")).Text = TXTSGSTAccountHead.Text.Trim();
            //((TextBox)CommonPopup1.FindControl("txtRefno")).Text = txtRefno10.Text.Trim();
            //((HiddenField)CommonPopup1.FindControl("hfColumn")).Value = "Name";
            //AccountHeadSGSTFill(1);
            //divAdd.Style.Add("display", "block");
            //divView.Style.Add("display", "none");



        }
        catch (Exception ex)
        {
        }
    }
    public void AccountHeadSGSTFill(int pageIndex)
    {
        //TXTSGSTAccountHead.Text = "";
        //txtRefno10.Text = "";
        //Populate Populate = new Populate();
        //CommonPopup1.CallingFunction = "AccountHeadSGSTFill";
        //string strName = ((TextBox)CommonPopup1.FindControl("txtName")).Text;
        //string Refno = ((TextBox)CommonPopup1.FindControl("txtRefno")).Text;
        //string searchCol = ((HiddenField)CommonPopup1.FindControl("hfColumn")).Value;
        //searchCol = searchCol == string.Empty ? "[Name]" : "[" + searchCol + "]";
        //Refno = Refno == string.Empty || Refno == null ? "0" : Refno;

        //Populate.FillGrid((GridView)CommonPopup1.FindControl("gdvSearch"), "AccountHead", "Name ,AhCode as ID,  AhCode As [Ref No],ShortName", "FK_Client=" + hfClient.Value.Split(',')[0] + " AND (ID_AccountHead =" + Refno + " OR 0=" + Refno + ") AND Active=1 ", searchCol + "  LIKE '" + strName + "%' and Cancelled=0 and Active=1  ", pageIndex, 10, "", (Repeater)CommonPopup1.FindControl("rptPager"), this.Page,1);
        //Populate = null;
        ////  txtSubHeadRefno.Focus();
        //if (TXTSGSTAccountHead.Text == string.Empty)
        //{
        //    lblPopupHeader.Text = "Account HeadSGST";
        //    mpCommon.Show();
        //}

    }
    protected void ImageButtonSGSTAccHeadSUB_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            //TXTSGSTAccountHeadSub.Text = "";
            //txtRefno11.Text = "";
            ////  ClearAccountHead();
            //((TextBox)CommonPopup1.FindControl("txtName")).Text = TXTSGSTAccountHeadSub.Text.Trim();
            //((TextBox)CommonPopup1.FindControl("txtRefno")).Text = txtRefno11.Text.Trim();
            //((HiddenField)CommonPopup1.FindControl("hfColumn")).Value = "Name";
            //AccountHeadsubSGSTFill(1);
            //divAdd.Style.Add("display", "block");
            //divView.Style.Add("display", "none");



        }
        catch (Exception ex)
        {
        }
    }
    public void AccountHeadsubSGSTFill(int pageIndex)
    {
        //TXTSGSTAccountHeadSub.Text = "";
        //txtRefno11.Text = "";
        //Populate Populate = new Populate();
        //CommonPopup1.CallingFunction = "AccountHeadsubSGSTFill";
        //string strName = ((TextBox)CommonPopup1.FindControl("txtName")).Text;
        //string Refno = ((TextBox)CommonPopup1.FindControl("txtRefno")).Text;
        //string searchCol = ((HiddenField)CommonPopup1.FindControl("hfColumn")).Value;
        //searchCol = searchCol == string.Empty ? "[Name]" : "[" + searchCol + "]";
        //Refno = Refno == string.Empty || Refno == null ? "0" : Refno;

        //Populate.FillGrid((GridView)CommonPopup1.FindControl("gdvSearch"), "AccountHeadSub", "Name ,ID_AccountHeadSub as ID,  ID_AccountHeadSub As [Ref No],ShortName", "FK_Client=" + hfClient.Value.Split(',')[0] + " AND (ID_AccountHeadSub =" + Refno + " OR 0=" + Refno + ") AND Active=1 ", searchCol + "  LIKE '" + strName + "%' and Cancelled=0 and Active=1 and AhCode= '" + txtRefno10.Text + "'   ", pageIndex, 10, "", (Repeater)CommonPopup1.FindControl("rptPager"), this.Page);
        //Populate = null;
        ////  txtSubHeadRefno.Focus();
        //if (TXTSGSTAccountHeadSub.Text == string.Empty)
        //{
        //    lblPopupHeader.Text = "Account HeadSub";
        //    mpCommon.Show();
        //}

    }
    protected void ImageButtonIGSTAccHead_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            //TXTIGSTAccountHead.Text = "";
            //txtRefno12.Text = "";
            //((TextBox)CommonPopup1.FindControl("txtName")).Text = TXTIGSTAccountHead.Text.Trim();
            //((TextBox)CommonPopup1.FindControl("txtRefno")).Text = txtRefno12.Text.Trim();
            //((HiddenField)CommonPopup1.FindControl("hfColumn")).Value = "Name";
            //AccountHeadIGSTFill(1);
            //divAdd.Style.Add("display", "block");
            //divView.Style.Add("display", "none");



        }
        catch (Exception ex)
        {
        }
    }
    public void AccountHeadIGSTFill(int pageIndex)
    {
        //TXTIGSTAccountHead.Text = "";
        //txtRefno12.Text = "";
        //Populate Populate = new Populate();
        //CommonPopup1.CallingFunction = "AccountHeadIGSTFill";
        //string strName = ((TextBox)CommonPopup1.FindControl("txtName")).Text;
        //string Refno = ((TextBox)CommonPopup1.FindControl("txtRefno")).Text;
        //string searchCol = ((HiddenField)CommonPopup1.FindControl("hfColumn")).Value;
        //searchCol = searchCol == string.Empty ? "[Name]" : "[" + searchCol + "]";
        //Refno = Refno == string.Empty || Refno == null ? "0" : Refno;

        //Populate.FillGrid((GridView)CommonPopup1.FindControl("gdvSearch"), "AccountHead", "Name ,AhCode as ID,  AhCode As [Ref No],ShortName", "FK_Client=" + hfClient.Value.Split(',')[0] + " AND (ID_AccountHead =" + Refno + " OR 0=" + Refno + ") AND Active=1 ", searchCol + "  LIKE '" + strName + "%' and Cancelled=0 and Active=1  ", pageIndex, 10, "", (Repeater)CommonPopup1.FindControl("rptPager"), this.Page,1);
        //Populate = null;
        ////  txtSubHeadRefno.Focus();
        //if (TXTIGSTAccountHead.Text == string.Empty)
        //{
        //    lblPopupHeader.Text = "Account HeadIGST";
        //    mpCommon.Show();
        //}


    }
    protected void ImageButtonIGSTAccHeadSUB_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            //TXTIGSTAccountHeadSub.Text = "";
            //txtRefno13.Text = "";
            ////  ClearAccountHead();
            //((TextBox)CommonPopup1.FindControl("txtName")).Text = TXTIGSTAccountHeadSub.Text.Trim();
            //((TextBox)CommonPopup1.FindControl("txtRefno")).Text = txtRefno13.Text.Trim();
            //((HiddenField)CommonPopup1.FindControl("hfColumn")).Value = "Name";
            //AccountHeadSUBIGSTFill(1);
            //divAdd.Style.Add("display", "block");
            //divView.Style.Add("display", "none");



        }
        catch (Exception ex)
        {
        }

    }
    public void AccountHeadSUBIGSTFill(int pageIndex)
    {
        //TXTIGSTAccountHeadSub.Text = "";
        //txtRefno13.Text = "";
        //Populate Populate = new Populate();
        //CommonPopup1.CallingFunction = "AccountHeadSUBIGSTFill";
        //string strName = ((TextBox)CommonPopup1.FindControl("txtName")).Text;
        //string Refno = ((TextBox)CommonPopup1.FindControl("txtRefno")).Text;
        //string searchCol = ((HiddenField)CommonPopup1.FindControl("hfColumn")).Value;
        //searchCol = searchCol == string.Empty ? "[Name]" : "[" + searchCol + "]";
        //Refno = Refno == string.Empty || Refno == null ? "0" : Refno;

        //Populate.FillGrid((GridView)CommonPopup1.FindControl("gdvSearch"), "AccountHeadSub", "Name ,ID_AccountHeadSub as ID,  ID_AccountHeadSub As [Ref No],ShortName", "FK_Client=" + hfClient.Value.Split(',')[0] + " AND (ID_AccountHeadSub =" + Refno + " OR 0=" + Refno + ") AND Active=1 ", searchCol + "  LIKE '" + strName + "%' and Cancelled=0 and Active=1 and AhCode= '" + txtRefno12.Text + "' ", pageIndex, 10, "", (Repeater)CommonPopup1.FindControl("rptPager"), this.Page);
        //Populate = null;
        ////  txtSubHeadRefno.Focus();
        //if (TXTIGSTAccountHeadSub.Text == string.Empty)
        //{
        //    lblPopupHeader.Text = "Account HeadSubIGST";
        //    mpCommon.Show();
        //}

    }
    protected void ImageButtonCESSAccHead_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            //TXTCESSAccountHead.Text = "";
            //txtRefno14.Text = "";
            //((TextBox)CommonPopup1.FindControl("txtName")).Text = TXTCESSAccountHead.Text.Trim();
            //((TextBox)CommonPopup1.FindControl("txtRefno")).Text = txtRefno14.Text.Trim();
            //((HiddenField)CommonPopup1.FindControl("hfColumn")).Value = "Name";
            //AccountHeadCESSFill(1);
            //divAdd.Style.Add("display", "block");
            //divView.Style.Add("display", "none");



        }
        catch (Exception ex)
        {
        }
    }
    public void AccountHeadCESSFill(int pageIndex)
    {
        //TXTCESSAccountHead.Text = "";
        //txtRefno14.Text = "";
        //Populate Populate = new Populate();
        //CommonPopup1.CallingFunction = "AccountHeadCESSFill";
        //string strName = ((TextBox)CommonPopup1.FindControl("txtName")).Text;
        //string Refno = ((TextBox)CommonPopup1.FindControl("txtRefno")).Text;
        //string searchCol = ((HiddenField)CommonPopup1.FindControl("hfColumn")).Value;
        //searchCol = searchCol == string.Empty ? "[Name]" : "[" + searchCol + "]";
        //Refno = Refno == string.Empty || Refno == null ? "0" : Refno;

        //Populate.FillGrid((GridView)CommonPopup1.FindControl("gdvSearch"), "AccountHead", "Name ,AhCode as ID,  AhCode As [Ref No],ShortName", "FK_Client=" + hfClient.Value.Split(',')[0] + " AND (ID_AccountHead =" + Refno + " OR 0=" + Refno + ") AND Active=1 ", searchCol + "  LIKE '" + strName + "%' and Cancelled=0 and Active=1  ", pageIndex, 10, "", (Repeater)CommonPopup1.FindControl("rptPager"), this.Page,1);
        //Populate = null;
        ////  txtSubHeadRefno.Focus();
        //if (TXTCESSAccountHead.Text == string.Empty)
        //{
        //    lblPopupHeader.Text = "Account HeadCESS";
        //    mpCommon.Show();
        //}

    }
    protected void ImageButtonCESSAccHeadSUB_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            //TXTCESSAccountHeadSub.Text = "";
            //txtRefno15.Text = "";
            ////  ClearAccountHead();
            //((TextBox)CommonPopup1.FindControl("txtName")).Text = TXTCESSAccountHeadSub.Text.Trim();
            //((TextBox)CommonPopup1.FindControl("txtRefno")).Text = txtRefno15.Text.Trim();
            //((HiddenField)CommonPopup1.FindControl("hfColumn")).Value = "Name";
            //AccountHeadSUBCESSFill(1);
            //divAdd.Style.Add("display", "block");
            //divView.Style.Add("display", "none");



        }
        catch (Exception ex)
        {
        }
    }
    public void AccountHeadSUBCESSFill(int pageIndex)
    {
        //TXTCESSAccountHeadSub.Text = "";
        //txtRefno15.Text = "";
        //Populate Populate = new Populate();
        //CommonPopup1.CallingFunction = "AccountHeadSUBCESSFill";
        //string strName = ((TextBox)CommonPopup1.FindControl("txtName")).Text;
        //string Refno = ((TextBox)CommonPopup1.FindControl("txtRefno")).Text;
        //string searchCol = ((HiddenField)CommonPopup1.FindControl("hfColumn")).Value;
        //searchCol = searchCol == string.Empty ? "[Name]" : "[" + searchCol + "]";
        //Refno = Refno == string.Empty || Refno == null ? "0" : Refno;

        //Populate.FillGrid((GridView)CommonPopup1.FindControl("gdvSearch"), "AccountHeadSub", "Name ,ID_AccountHeadSub as ID,  ID_AccountHeadSub As [Ref No],ShortName", "FK_Client=" + hfClient.Value.Split(',')[0] + " AND (ID_AccountHeadSub =" + Refno + " OR 0=" + Refno + ") AND Active=1  ", searchCol + "  LIKE '" + strName + "%' and Cancelled=0 and Active=1 and AhCode= '" + txtRefno14.Text + "'  ", pageIndex, 10, "", (Repeater)CommonPopup1.FindControl("rptPager"), this.Page);
        //Populate = null;
        ////  txtSubHeadRefno.Focus();
        //if (TXTCESSAccountHeadSub.Text == string.Empty)
        //{
        //    lblPopupHeader.Text = "AccountHeadSUB CESS";
        //    mpCommon.Show();
        //}

    }

    protected void ImageButtonLoadingAccHead_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            //txtAccountHeadLoading.Text = "";
            //txtRefno16.Text = "";
            //((TextBox)CommonPopup1.FindControl("txtName")).Text = txtAccountHeadLoading.Text.Trim();
            //((TextBox)CommonPopup1.FindControl("txtRefno")).Text = txtRefno16.Text.Trim();
            //((HiddenField)CommonPopup1.FindControl("hfColumn")).Value = "Name";
            //AccountHeadLoadingFill(1);
            //divAdd.Style.Add("display", "block");
            //divView.Style.Add("display", "none");



        }
        catch (Exception ex)
        {
        }
    }
    public void AccountHeadLoadingFill(int pageIndex)
    {
        //txtAccountHeadLoading.Text = "";
        //txtRefno16.Text = "";
        //Populate Populate = new Populate();
        //CommonPopup1.CallingFunction = "AccountHeadLoadingFill";
        //string strName = ((TextBox)CommonPopup1.FindControl("txtName")).Text;
        //string Refno = ((TextBox)CommonPopup1.FindControl("txtRefno")).Text;
        //string searchCol = ((HiddenField)CommonPopup1.FindControl("hfColumn")).Value;
        //searchCol = searchCol == string.Empty ? "[Name]" : "[" + searchCol + "]";
        //Refno = Refno == string.Empty || Refno == null ? "0" : Refno;

        //Populate.FillGrid((GridView)CommonPopup1.FindControl("gdvSearch"), "AccountHead", "Name ,AhCode as ID,  AhCode As [Ref No],ShortName", "FK_Client=" + hfClient.Value.Split(',')[0] + " AND (ID_AccountHead =" + Refno + " OR 0=" + Refno + ") AND Active=1 ", searchCol + "  LIKE '" + strName + "%' and Cancelled=0 and Active=1  ", pageIndex, 10, "", (Repeater)CommonPopup1.FindControl("rptPager"), this.Page,1);
        //Populate = null;
        ////  txtSubHeadRefno.Focus();
        //if (txtAccountHeadLoading.Text == string.Empty)
        //{
        //    lblPopupHeader.Text = "Account Head loading";
        //    mpCommon.Show();
        //}

    }
    protected void ImageButtonLoadingAccHeadsub_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            //txtAccountHeadSubLoading.Text = "";
            //txtRefno17.Text = "";
            ////  ClearAccountHead();
            //((TextBox)CommonPopup1.FindControl("txtName")).Text = txtAccountHeadSubLoading.Text.Trim();
            //((TextBox)CommonPopup1.FindControl("txtRefno")).Text = txtRefno17.Text.Trim();
            //((HiddenField)CommonPopup1.FindControl("hfColumn")).Value = "Name";
            //AccountHeadsubLoadingFill(1);
            //divAdd.Style.Add("display", "block");
            //divView.Style.Add("display", "none");



        }
        catch (Exception ex)
        {
        }
    }
    public void AccountHeadsubLoadingFill(int pageIndex)
    {
        //txtAccountHeadSubLoading.Text = "";
        //txtRefno17.Text = "";
        //Populate Populate = new Populate();
        //CommonPopup1.CallingFunction = "AccountHeadsubLoadingFill";
        //string strName = ((TextBox)CommonPopup1.FindControl("txtName")).Text;
        //string Refno = ((TextBox)CommonPopup1.FindControl("txtRefno")).Text;
        //string searchCol = ((HiddenField)CommonPopup1.FindControl("hfColumn")).Value;
        //searchCol = searchCol == string.Empty ? "[Name]" : "[" + searchCol + "]";
        //Refno = Refno == string.Empty || Refno == null ? "0" : Refno;

        //Populate.FillGrid((GridView)CommonPopup1.FindControl("gdvSearch"), "AccountHeadSub", "Name ,ID_AccountHeadSub as ID,  ID_AccountHeadSub As [Ref No],ShortName", "FK_Client=" + hfClient.Value.Split(',')[0] + " AND (ID_AccountHeadSub =" + Refno + " OR 0=" + Refno + ") AND Active=1 ", searchCol + "  LIKE '" + strName + "%' and Cancelled=0 and Active=1 and AhCode= '" + txtRefno16.Text + "'  ", pageIndex, 10, "", (Repeater)CommonPopup1.FindControl("rptPager"), this.Page);
        //Populate = null;
        ////  txtSubHeadRefno.Focus();
        //if (txtAccountHeadSubLoading.Text == string.Empty)
        //{
        //    lblPopupHeader.Text = "Account HeadSubLoading";
        //    mpCommon.Show();
        //}

    }
    protected void ImageButtonunLoadingAccHead_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            //txtAccountHeadunLoading.Text = "";
            //txtRefno18.Text = "";
            //((TextBox)CommonPopup1.FindControl("txtName")).Text = txtAccountHeadunLoading.Text.Trim();
            //((TextBox)CommonPopup1.FindControl("txtRefno")).Text = txtRefno18.Text.Trim();
            //((HiddenField)CommonPopup1.FindControl("hfColumn")).Value = "Name";
            //AccountHeadunLoadingFill(1);
            //divAdd.Style.Add("display", "block");
            //divView.Style.Add("display", "none");



        }
        catch (Exception ex)
        {
        }
    }
    public void AccountHeadunLoadingFill(int pageIndex)
    {
        //txtAccountHeadunLoading.Text = "";
        //txtRefno18.Text = "";
        //Populate Populate = new Populate();
        //CommonPopup1.CallingFunction = "AccountHeadunLoadingFill";
        //string strName = ((TextBox)CommonPopup1.FindControl("txtName")).Text;
        //string Refno = ((TextBox)CommonPopup1.FindControl("txtRefno")).Text;
        //string searchCol = ((HiddenField)CommonPopup1.FindControl("hfColumn")).Value;
        //searchCol = searchCol == string.Empty ? "[Name]" : "[" + searchCol + "]";
        //Refno = Refno == string.Empty || Refno == null ? "0" : Refno;

        //Populate.FillGrid((GridView)CommonPopup1.FindControl("gdvSearch"), "AccountHead", "Name ,AhCode as ID,  AhCode As [Ref No],ShortName", "FK_Client=" + hfClient.Value.Split(',')[0] + " AND (ID_AccountHead =" + Refno + " OR 0=" + Refno + ") AND Active=1 ", searchCol + "  LIKE '" + strName + "%' and Cancelled=0 and Active=1  ", pageIndex, 10, "", (Repeater)CommonPopup1.FindControl("rptPager"), this.Page,1);
        //Populate = null;
        ////  txtSubHeadRefno.Focus();
        //if (txtAccountHeadunLoading.Text == string.Empty)
        //{
        //    lblPopupHeader.Text = "Account Head unloading";
        //    mpCommon.Show();
        //}

    }
    protected void ImageButtonunLoadingAccHeadsub_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            //txtAccountHeadSubunLoading.Text = "";
            //txtRefno19.Text = "";
            ////  ClearAccountHead();
            //((TextBox)CommonPopup1.FindControl("txtName")).Text = txtAccountHeadSubunLoading.Text.Trim();
            //((TextBox)CommonPopup1.FindControl("txtRefno")).Text = txtRefno19.Text.Trim();
            //((HiddenField)CommonPopup1.FindControl("hfColumn")).Value = "Name";
            //AccountHeadsubunLoadingFill(1);
            //divAdd.Style.Add("display", "block");
            //divView.Style.Add("display", "none");



        }
        catch (Exception ex)
        {
        }
    }
    public void AccountHeadsubunLoadingFill(int pageIndex)
    {
        //txtAccountHeadSubunLoading.Text = "";
        //txtRefno19.Text = "";
        //Populate Populate = new Populate();
        //CommonPopup1.CallingFunction = "AccountHeadsubunLoadingFill";
        //string strName = ((TextBox)CommonPopup1.FindControl("txtName")).Text;
        //string Refno = ((TextBox)CommonPopup1.FindControl("txtRefno")).Text;
        //string searchCol = ((HiddenField)CommonPopup1.FindControl("hfColumn")).Value;
        //searchCol = searchCol == string.Empty ? "[Name]" : "[" + searchCol + "]";
        //Refno = Refno == string.Empty || Refno == null ? "0" : Refno;

        //Populate.FillGrid((GridView)CommonPopup1.FindControl("gdvSearch"), "AccountHeadSub", "Name ,ID_AccountHeadSub as ID,  ID_AccountHeadSub As [Ref No],ShortName", "FK_Client=" + hfClient.Value.Split(',')[0] + " AND (ID_AccountHeadSub =" + Refno + " OR 0=" + Refno + ") AND Active=1  ", searchCol + "  LIKE '" + strName + "%' and Cancelled=0 and Active=1 and AhCode= '" + txtRefno18.Text + "' ", pageIndex, 10, "", (Repeater)CommonPopup1.FindControl("rptPager"), this.Page);
        //Populate = null;
        ////  txtSubHeadRefno.Focus();
        //if (txtAccountHeadSubunLoading.Text == string.Empty)
        //{
        //    lblPopupHeader.Text = "Account HeadSubunLoading";
        //    mpCommon.Show();
        //}

    }
    protected void ImageButtondisplayAccHead_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            //txtdisplayAccHead.Text = "";
            //txtRefno20.Text = "";
            //((TextBox)CommonPopup1.FindControl("txtName")).Text = txtdisplayAccHead.Text.Trim();
            //((TextBox)CommonPopup1.FindControl("txtRefno")).Text = txtRefno20.Text.Trim();
            //((HiddenField)CommonPopup1.FindControl("hfColumn")).Value = "Name";
            //AccountHeadDisplayFill(1);
            //divAdd.Style.Add("display", "block");
            //divView.Style.Add("display", "none");



        }
        catch (Exception ex)
        {
        }
    }
    public void AccountHeadDisplayFill(int pageIndex)
    {
        //txtdisplayAccHead.Text = "";
        //txtRefno20.Text = "";
        //Populate Populate = new Populate();
        //CommonPopup1.CallingFunction = "AccountHeadDisplayFill";
        //string strName = ((TextBox)CommonPopup1.FindControl("txtName")).Text;
        //string Refno = ((TextBox)CommonPopup1.FindControl("txtRefno")).Text;
        //string searchCol = ((HiddenField)CommonPopup1.FindControl("hfColumn")).Value;
        //searchCol = searchCol == string.Empty ? "[Name]" : "[" + searchCol + "]";
        //Refno = Refno == string.Empty || Refno == null ? "0" : Refno;

        //Populate.FillGrid((GridView)CommonPopup1.FindControl("gdvSearch"), "AccountHead", "Name ,AhCode as ID,  AhCode As [Ref No],ShortName", "FK_Client=" + hfClient.Value.Split(',')[0] + " AND (ID_AccountHead =" + Refno + " OR 0=" + Refno + ") AND Active=1 ", searchCol + "  LIKE '" + strName + "%' and Cancelled=0 and Active=1  ", pageIndex, 10, "", (Repeater)CommonPopup1.FindControl("rptPager"), this.Page,1);
        //Populate = null;
        ////  txtSubHeadRefno.Focus();
        //if (txtdisplayAccHead.Text == string.Empty)
        //{
        //    lblPopupHeader.Text = "Account Head Dispaly";
        //    mpCommon.Show();
        //}

    }
    protected void ImageButtondisplayAccHeadsub_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            //txtdisplayAccHeadsub.Text = "";
            //txtRefno21.Text = "";
            ////  ClearAccountHead();
            //((TextBox)CommonPopup1.FindControl("txtName")).Text = txtdisplayAccHeadsub.Text.Trim();
            //((TextBox)CommonPopup1.FindControl("txtRefno")).Text = txtRefno21.Text.Trim();
            //((HiddenField)CommonPopup1.FindControl("hfColumn")).Value = "Name";
            //AccountHeadsubDisplayFill(1);
            //divAdd.Style.Add("display", "block");
            //divView.Style.Add("display", "none");



        }
        catch (Exception ex)
        {
        }
    }
    public void AccountHeadsubDisplayFill(int pageIndex)
    {
        //txtdisplayAccHeadsub.Text = "";
        //txtRefno21.Text = "";
        //Populate Populate = new Populate();
        //CommonPopup1.CallingFunction = "AccountHeadsubDisplayFill";
        //string strName = ((TextBox)CommonPopup1.FindControl("txtName")).Text;
        //string Refno = ((TextBox)CommonPopup1.FindControl("txtRefno")).Text;
        //string searchCol = ((HiddenField)CommonPopup1.FindControl("hfColumn")).Value;
        //searchCol = searchCol == string.Empty ? "[Name]" : "[" + searchCol + "]";
        //Refno = Refno == string.Empty || Refno == null ? "0" : Refno;

        //Populate.FillGrid((GridView)CommonPopup1.FindControl("gdvSearch"), "AccountHeadSub", "Name ,ID_AccountHeadSub as ID,  ID_AccountHeadSub As [Ref No],ShortName", "FK_Client=" + hfClient.Value.Split(',')[0] + " AND (ID_AccountHeadSub =" + Refno + " OR 0=" + Refno + ") AND Active=1 ", searchCol + "  LIKE '" + strName + "%' and Cancelled=0 and Active=1 and AhCode= '" + txtRefno20.Text + "'  ", pageIndex, 10, "", (Repeater)CommonPopup1.FindControl("rptPager"), this.Page);
        //Populate = null;
        ////  txtSubHeadRefno.Focus();
        //if (txtdisplayAccHeadsub.Text == string.Empty)
        //{
        //    lblPopupHeader.Text = "Account Headsub Dispaly";
        //    mpCommon.Show();
        //}

    }
    protected void ImageButtonotherAccHead_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            //txtotherAccHead.Text = "";
            //txtRefno22.Text = "";
            //((TextBox)CommonPopup1.FindControl("txtName")).Text = txtotherAccHead.Text.Trim();
            //((TextBox)CommonPopup1.FindControl("txtRefno")).Text = txtRefno22.Text.Trim();
            //((HiddenField)CommonPopup1.FindControl("hfColumn")).Value = "Name";
            //AccountHeadOthersFill(1);
            //divAdd.Style.Add("display", "block");
            //divView.Style.Add("display", "none");



        }
        catch (Exception ex)
        {
        }
    }
    public void AccountHeadOthersFill(int pageIndex)
    {
        //txtotherAccHead.Text = "";
        //txtRefno22.Text = "";
        //Populate Populate = new Populate();
        //CommonPopup1.CallingFunction = "AccountHeadOthersFill";
        //string strName = ((TextBox)CommonPopup1.FindControl("txtName")).Text;
        //string Refno = ((TextBox)CommonPopup1.FindControl("txtRefno")).Text;
        //string searchCol = ((HiddenField)CommonPopup1.FindControl("hfColumn")).Value;
        //searchCol = searchCol == string.Empty ? "[Name]" : "[" + searchCol + "]";
        //Refno = Refno == string.Empty || Refno == null ? "0" : Refno;

        //Populate.FillGrid((GridView)CommonPopup1.FindControl("gdvSearch"), "AccountHead", "Name ,AhCode as ID,  AhCode As [Ref No],ShortName", "FK_Client=" + hfClient.Value.Split(',')[0] + " AND (ID_AccountHead =" + Refno + " OR 0=" + Refno + ") AND Active=1 ", searchCol + "  LIKE '" + strName + "%' and Cancelled=0 and Active=1  ", pageIndex, 10, "", (Repeater)CommonPopup1.FindControl("rptPager"), this.Page,1);
        //Populate = null;
        ////  txtSubHeadRefno.Focus();
        //if (txtotherAccHead.Text == string.Empty)
        //{
        //    lblPopupHeader.Text = "Account Head Others";
        //    mpCommon.Show();
        //}

    }
    protected void ImageButtonotherAccHeadsubd_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            //txtotherAccHeadsub.Text = "";
            //txtRefno23.Text = "";
            ////  ClearAccountHead();
            //((TextBox)CommonPopup1.FindControl("txtName")).Text = txtotherAccHeadsub.Text.Trim();
            //((TextBox)CommonPopup1.FindControl("txtRefno")).Text = txtRefno23.Text.Trim();
            //((HiddenField)CommonPopup1.FindControl("hfColumn")).Value = "Name";
            //AccountHeadsubOthersFill(1);
            //divAdd.Style.Add("display", "block");
            //divView.Style.Add("display", "none");



        }
        catch (Exception ex)
        {
        }
    }
    public void AccountHeadsubOthersFill(int pageIndex)
    {
        //txtotherAccHeadsub.Text = "";
        //txtRefno23.Text = "";
        //Populate Populate = new Populate();
        //CommonPopup1.CallingFunction = "AccountHeadsubOthersFill";
        //string strName = ((TextBox)CommonPopup1.FindControl("txtName")).Text;
        //string Refno = ((TextBox)CommonPopup1.FindControl("txtRefno")).Text;
        //string searchCol = ((HiddenField)CommonPopup1.FindControl("hfColumn")).Value;
        //searchCol = searchCol == string.Empty ? "[Name]" : "[" + searchCol + "]";
        //Refno = Refno == string.Empty || Refno == null ? "0" : Refno;

        //Populate.FillGrid((GridView)CommonPopup1.FindControl("gdvSearch"), "AccountHeadSub", "Name ,ID_AccountHeadSub as ID,  ID_AccountHeadSub As [Ref No],ShortName", "FK_Client=" + hfClient.Value.Split(',')[0] + " AND (ID_AccountHeadSub =" + Refno + " OR 0=" + Refno + ") AND Active=1 ", searchCol + "  LIKE '" + strName + "%' and Cancelled=0 and Active=1 and AhCode= '" + txtRefno22.Text + "' ", pageIndex, 10, "", (Repeater)CommonPopup1.FindControl("rptPager"), this.Page);
        //Populate = null;
        ////  txtSubHeadRefno.Focus();
        //if (txtotherAccHeadsub.Text == string.Empty)
        //{
        //    lblPopupHeader.Text = "Account Headsub Others";
        //    mpCommon.Show();
        //}

    }
    protected void ImageButtonreturnAccHead_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            txtRefno24.Text = "";
            txtreturnacchead.Text = "";
            txtRefno25.Text = "";
            txtreturnaccheadsub.Text = "";

            ((TextBox)CommonPopup1.FindControl("txtName")).Text = txtreturnacchead.Text.Trim();
            ((TextBox)CommonPopup1.FindControl("txtRefno")).Text = txtRefno24.Text.Trim();
            ((HiddenField)CommonPopup1.FindControl("hfColumn")).Value = "Name";
            AccountHeadReturnFill(1);
            divAdd.Style.Add("display", "block");
            divView.Style.Add("display", "none");



        }
        catch (Exception ex)
        {
        }
    }
    public void AccountHeadReturnFill(int pageIndex)
    {
        txtreturnacchead.Text = "";
        txtRefno24.Text = "";
        Populate Populate = new Populate();
        CommonPopup1.CallingFunction = "AccountHeadReturnFill";
        string strName = ((TextBox)CommonPopup1.FindControl("txtName")).Text;
        string Refno = ((TextBox)CommonPopup1.FindControl("txtRefno")).Text;
        string searchCol = ((HiddenField)CommonPopup1.FindControl("hfColumn")).Value;
        searchCol = searchCol == string.Empty ? "[Name]" : "[" + searchCol + "]";
        Refno = Refno == string.Empty || Refno == null ? "0" : Refno;
        Populate.FillPopUpGrid((GridView)CommonPopup1.FindControl("gdvSearch"), "AccountHead", "ID_AccountHead as ID,HeadName as Name", "ID_AccountHead", "ID_AccountHead", "FK_Client=" + hfClient.Value.Split(',')[0] + "  AND Cancelled=0 AND HeadName ", strName, null);
        //Populate.FillGrid((GridView)CommonPopup1.FindControl("gdvSearch"), "AccountHead", "Name ,AhCode as ID,  AhCode As [Ref No],ShortName", "FK_Client=" + hfClient.Value.Split(',')[0] + " AND (ID_AccountHead =" + Refno + " OR 0=" + Refno + ") AND Active=1 ", searchCol + "  LIKE '" + strName + "%' and Cancelled=0 and Active=1  ", pageIndex, 10, "", (Repeater)CommonPopup1.FindControl("rptPager"), this.Page, 1);
        Populate = null;
        //  txtSubHeadRefno.Focus();
        if (txtreturnacchead.Text == string.Empty)
        {
            lblPopupHeader.Text = "Account Head Return";
            mpCommon.Show();
        }

    }
    protected void ImageButtonreturnAccHeadsub_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            txtreturnaccheadsub.Text = "";
            txtRefno25.Text = "";
            //  ClearAccountHead();
            ((TextBox)CommonPopup1.FindControl("txtName")).Text = txtreturnaccheadsub.Text.Trim();
            ((TextBox)CommonPopup1.FindControl("txtRefno")).Text = txtRefno25.Text.Trim();
            ((HiddenField)CommonPopup1.FindControl("hfColumn")).Value = "Name";
            AccountHeadsubReturnFill(1);
            divAdd.Style.Add("display", "block");
            divView.Style.Add("display", "none");



        }
        catch (Exception ex)
        {
        }
    }
    public void AccountHeadsubReturnFill(int pageIndex)
    {
        txtreturnaccheadsub.Text = "";
        txtRefno25.Text = "";
        Populate Populate = new Populate();
        CommonPopup1.CallingFunction = "AccountHeadsubReturnFill";
        string strName = ((TextBox)CommonPopup1.FindControl("txtName")).Text;
        string Refno = ((TextBox)CommonPopup1.FindControl("txtRefno")).Text;
        string searchCol = ((HiddenField)CommonPopup1.FindControl("hfColumn")).Value;
        searchCol = searchCol == string.Empty ? "[Name]" : "[" + searchCol + "]";
        Refno = Refno == string.Empty || Refno == null ? "0" : Refno;
        Populate.FillPopUpGrid((GridView)CommonPopup1.FindControl("gdvSearch"), "AccountHeadSub AHS LEFT JOIN Customer C ON C.ID_Customer= AHS.Fk_Cuscode AND C.Active=1", "AHS.ID_AccountHeadSub as ID,AHS.HeadSubName as Name", "ID_AccountHeadSub", "ID_AccountHeadSub", " AHS.FK_AccountHead = " + txtRefno24.Text + " AND AHS.FK_Client=" + hfClient.Value.Split(',')[0] + "  AND AHS.Cancelled=0 AND HeadSubName ", strName, null);
        //Populate.FillGrid((GridView)CommonPopup1.FindControl("gdvSearch"), "AccountHeadSub", "Name ,ID_AccountHeadSub as ID,  ID_AccountHeadSub As [Ref No],ShortName", "FK_Client=" + hfClient.Value.Split(',')[0] + " AND (ID_AccountHeadSub =" + Refno + " OR 0=" + Refno + ") AND Active=1 ", searchCol + "  LIKE '" + strName + "%' and Cancelled=0 and Active=1 and AhCode= '" + txtRefno24.Text + "' ", pageIndex, 10, "", (Repeater)CommonPopup1.FindControl("rptPager"), this.Page);
        Populate = null;
        //  txtSubHeadRefno.Focus();
        if (txtreturnaccheadsub.Text == string.Empty)
        {
            lblPopupHeader.Text = "Account Headsub Return";
            mpCommon.Show();
        }

    }
    protected void ImageButtonfrightAccHead_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            //txtRefno26.Text = "";
            //txtFrightAccHead.Text = "";
            //((TextBox)CommonPopup1.FindControl("txtName")).Text = txtFrightAccHead.Text.Trim();
            //((TextBox)CommonPopup1.FindControl("txtRefno")).Text = txtRefno26.Text.Trim();
            //((HiddenField)CommonPopup1.FindControl("hfColumn")).Value = "Name";
            //AccountHeadfrightFill(1);
            //divAdd.Style.Add("display", "block");
            //divView.Style.Add("display", "none");



        }
        catch (Exception ex)
        {
        }
    }
    public void AccountHeadfrightFill(int pageIndex)
    {
        //txtFrightAccHead.Text = "";
        //txtRefno26.Text = "";
        //Populate Populate = new Populate();
        //CommonPopup1.CallingFunction = "AccountHeadfrightFill";
        //string strName = ((TextBox)CommonPopup1.FindControl("txtName")).Text;
        //string Refno = ((TextBox)CommonPopup1.FindControl("txtRefno")).Text;
        //string searchCol = ((HiddenField)CommonPopup1.FindControl("hfColumn")).Value;
        //searchCol = searchCol == string.Empty ? "[Name]" : "[" + searchCol + "]";
        //Refno = Refno == string.Empty || Refno == null ? "0" : Refno;

        //Populate.FillGrid((GridView)CommonPopup1.FindControl("gdvSearch"), "AccountHead", "Name ,AhCode as ID,  AhCode As [Ref No],ShortName", "FK_Client=" + hfClient.Value.Split(',')[0] + " AND (ID_AccountHead =" + Refno + " OR 0=" + Refno + ") AND Active=1 ", searchCol + "  LIKE '" + strName + "%' and Cancelled=0 and Active=1  ", pageIndex, 10, "", (Repeater)CommonPopup1.FindControl("rptPager"), this.Page,1);
        //Populate = null;
        ////  txtSubHeadRefno.Focus();
        //if (txtFrightAccHead.Text == string.Empty)
        //{
        //    lblPopupHeader.Text = "Account Head Fright";
        //    mpCommon.Show();
        //}

    }
    protected void ImageButtonfrightAccHeadsub_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            //txtFrightAccSubHead.Text = "";
            //txtRefno27.Text = "";
            ////  ClearAccountHead();
            //((TextBox)CommonPopup1.FindControl("txtName")).Text = txtFrightAccSubHead.Text.Trim();
            //((TextBox)CommonPopup1.FindControl("txtRefno")).Text = txtRefno27.Text.Trim();
            //((HiddenField)CommonPopup1.FindControl("hfColumn")).Value = "Name";
            //AccountHeadsubfrightFill(1);
            //divAdd.Style.Add("display", "block");
            //divView.Style.Add("display", "none");



        }
        catch (Exception ex)
        {
        }
    }
    public void AccountHeadsubfrightFill(int pageIndex)
    {
        //txtFrightAccSubHead.Text = "";
        //txtRefno27.Text = "";
        //Populate Populate = new Populate();
        //CommonPopup1.CallingFunction = "AccountHeadsubfrightFill";
        //string strName = ((TextBox)CommonPopup1.FindControl("txtName")).Text;
        //string Refno = ((TextBox)CommonPopup1.FindControl("txtRefno")).Text;
        //string searchCol = ((HiddenField)CommonPopup1.FindControl("hfColumn")).Value;
        //searchCol = searchCol == string.Empty ? "[Name]" : "[" + searchCol + "]";
        //Refno = Refno == string.Empty || Refno == null ? "0" : Refno;

        //Populate.FillGrid((GridView)CommonPopup1.FindControl("gdvSearch"), "AccountHeadSub", "Name ,ID_AccountHeadSub as ID,  ID_AccountHeadSub As [Ref No],ShortName", "FK_Client=" + hfClient.Value.Split(',')[0] + " AND (ID_AccountHeadSub =" + Refno + " OR 0=" + Refno + ") AND Active=1 ", searchCol + "  LIKE '" + strName + "%' and Cancelled=0 and Active=1 and AhCode= '" + txtRefno26.Text + "' ", pageIndex, 10, "", (Repeater)CommonPopup1.FindControl("rptPager"), this.Page);
        //Populate = null;
        ////  txtSubHeadRefno.Focus();
        //if (txtFrightAccSubHead.Text == string.Empty)
        //{
        //    lblPopupHeader.Text = "Account Headsub fright";
        //    mpCommon.Show();
        //}

    }
    protected void ImageButtonDiscountAccHead_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            txtRefno28.Text = "";
            txtDiscountAccHead.Text = "";
            txtRefno29.Text = "";
            txtlDiscountAccSubHead.Text = "";

            ((TextBox)CommonPopup1.FindControl("txtName")).Text = txtDiscountAccHead.Text.Trim();
            ((TextBox)CommonPopup1.FindControl("txtRefno")).Text = txtRefno28.Text.Trim();
            ((HiddenField)CommonPopup1.FindControl("hfColumn")).Value = "Name";
            AccountHeadDiscountFill(1);
            divAdd.Style.Add("display", "block");
            divView.Style.Add("display", "none");



        }
        catch (Exception ex)
        {
        }
    }
    public void AccountHeadDiscountFill(int pageIndex)
    {
        txtDiscountAccHead.Text = "";
        txtRefno28.Text = "";
        Populate Populate = new Populate();
        CommonPopup1.CallingFunction = "AccountHeadDiscountFill";
        string strName = ((TextBox)CommonPopup1.FindControl("txtName")).Text;
        string Refno = ((TextBox)CommonPopup1.FindControl("txtRefno")).Text;
        string searchCol = ((HiddenField)CommonPopup1.FindControl("hfColumn")).Value;
        searchCol = searchCol == string.Empty ? "[Name]" : "[" + searchCol + "]";
        Refno = Refno == string.Empty || Refno == null ? "0" : Refno;
        Populate.FillPopUpGrid((GridView)CommonPopup1.FindControl("gdvSearch"), "AccountHead", "ID_AccountHead as ID,HeadName as Name", "ID_AccountHead", "ID_AccountHead", "FK_Client=" + hfClient.Value.Split(',')[0] + "  AND Cancelled=0 AND HeadName ", strName, null);
        //Populate.FillGrid((GridView)CommonPopup1.FindControl("gdvSearch"), "AccountHead", "Name ,AhCode as ID,  AhCode As [Ref No],ShortName", "FK_Client=" + hfClient.Value.Split(',')[0] + " AND (ID_AccountHead =" + Refno + " OR 0=" + Refno + ") AND Active=1 ", searchCol + "  LIKE '" + strName + "%' and Cancelled=0 and Active=1  ", pageIndex, 10, "", (Repeater)CommonPopup1.FindControl("rptPager"), this.Page,1);
        Populate = null;
        //  txtSubHeadRefno.Focus();
        if (txtDiscountAccHead.Text == string.Empty)
        {
            lblPopupHeader.Text = "Account Head Discount";
            mpCommon.Show();
        }

    }
    protected void ImageButtonDiscountAccHeadsub_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            txtlDiscountAccSubHead.Text = "";
            txtRefno29.Text = "";
            //  ClearAccountHead();
            ((TextBox)CommonPopup1.FindControl("txtName")).Text = txtlDiscountAccSubHead.Text.Trim();
            ((TextBox)CommonPopup1.FindControl("txtRefno")).Text = txtRefno29.Text.Trim();
            ((HiddenField)CommonPopup1.FindControl("hfColumn")).Value = "Name";
            AccountHeadsubDiscountFill(1);
            divAdd.Style.Add("display", "block");
            divView.Style.Add("display", "none");



        }
        catch (Exception ex)
        {
        }
    }
    public void AccountHeadsubDiscountFill(int pageIndex)
    {
        txtlDiscountAccSubHead.Text = "";
        txtRefno29.Text = "";
        Populate Populate = new Populate();
        CommonPopup1.CallingFunction = "AccountHeadsubDiscountFill";
        string strName = ((TextBox)CommonPopup1.FindControl("txtName")).Text;
        string Refno = ((TextBox)CommonPopup1.FindControl("txtRefno")).Text;
        string searchCol = ((HiddenField)CommonPopup1.FindControl("hfColumn")).Value;
        searchCol = searchCol == string.Empty ? "[Name]" : "[" + searchCol + "]";
        Refno = Refno == string.Empty || Refno == null ? "0" : Refno;
        Populate.FillPopUpGrid((GridView)CommonPopup1.FindControl("gdvSearch"), "AccountHeadSub AHS LEFT JOIN Customer C ON C.ID_Customer= AHS.Fk_Cuscode AND C.Active=1", "AHS.ID_AccountHeadSub as ID,AHS.HeadSubName as Name", "ID_AccountHeadSub", "ID_AccountHeadSub", " AHS.FK_AccountHead = " + txtRefno28.Text + " AND AHS.FK_Client=" + hfClient.Value.Split(',')[0] + "  AND AHS.Cancelled=0 AND HeadSubName ", strName, null);
        //Populate.FillGrid((GridView)CommonPopup1.FindControl("gdvSearch"), "AccountHeadSub", "Name ,ID_AccountHeadSub as ID,  ID_AccountHeadSub As [Ref No],ShortName", "FK_Client=" + hfClient.Value.Split(',')[0] + " AND (ID_AccountHeadSub =" + Refno + " OR 0=" + Refno + ") AND Active=1 ", searchCol + "  LIKE '" + strName + "%' and Cancelled=0 and Active=1 and AhCode= '" + txtRefno28.Text + "' ", pageIndex, 10, "", (Repeater)CommonPopup1.FindControl("rptPager"), this.Page);
        Populate = null;
        //  txtSubHeadRefno.Focus();
        if (txtlDiscountAccSubHead.Text == string.Empty)
        {
            lblPopupHeader.Text = "Account Headsub Discount";
            mpCommon.Show();
        }

    }
    protected void ImageButtonroundingAccHead_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            txtRefno30.Text = "";
            txtRoundingAccountHead.Text = "";

            txtRefno31.Text = "";
            txtRoundingAccountHeadsub.Text = "";

            ((TextBox)CommonPopup1.FindControl("txtName")).Text = txtRoundingAccountHead.Text.Trim();
            ((TextBox)CommonPopup1.FindControl("txtRefno")).Text = txtRefno30.Text.Trim();
            ((HiddenField)CommonPopup1.FindControl("hfColumn")).Value = "Name";
            AccountHeadRoundingFill(1);
            divAdd.Style.Add("display", "block");
            divView.Style.Add("display", "none");



        }
        catch (Exception ex)
        {
        }
    }
    public void AccountHeadRoundingFill(int pageIndex)
    {
        txtRoundingAccountHead.Text = "";
        txtRefno30.Text = "";
        Populate Populate = new Populate();
        CommonPopup1.CallingFunction = "AccountHeadRoundingFill";
        string strName = ((TextBox)CommonPopup1.FindControl("txtName")).Text;
        string Refno = ((TextBox)CommonPopup1.FindControl("txtRefno")).Text;
        string searchCol = ((HiddenField)CommonPopup1.FindControl("hfColumn")).Value;
        searchCol = searchCol == string.Empty ? "[Name]" : "[" + searchCol + "]";
        Refno = Refno == string.Empty || Refno == null ? "0" : Refno;


        Populate.FillPopUpGrid((GridView)CommonPopup1.FindControl("gdvSearch"), "AccountHead", "ID_AccountHead as ID,HeadName as Name", "ID_AccountHead", "ID_AccountHead", "FK_Client=" + hfClient.Value.Split(',')[0] + "  AND Cancelled=0 AND HeadName ", strName, null);
        //Populate.FillGrid((GridView)CommonPopup1.FindControl("gdvSearch"), "AccountHead", "Name ,AhCode as ID,  AhCode As [Ref No],ShortName", "FK_Client=" + hfClient.Value.Split(',')[0] + " AND (ID_AccountHead =" + Refno + " OR 0=" + Refno + ") AND Active=1 ", searchCol + "  LIKE '" + strName + "%' and Cancelled=0 and Active=1  ", pageIndex, 10, "", (Repeater)CommonPopup1.FindControl("rptPager"), this.Page,1);
        Populate = null;
        //  txtSubHeadRefno.Focus();
        if (txtRoundingAccountHead.Text == string.Empty)
        {
            lblPopupHeader.Text = "Account Head Rounding";
            mpCommon.Show();
        }

    }
    protected void ImageButtonroundingAccHeadsub_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            txtRoundingAccountHeadsub.Text = "";
            txtRefno31.Text = "";
            //  ClearAccountHead();
            ((TextBox)CommonPopup1.FindControl("txtName")).Text = txtRoundingAccountHeadsub.Text.Trim();
            ((TextBox)CommonPopup1.FindControl("txtRefno")).Text = txtRefno31.Text.Trim();
            ((HiddenField)CommonPopup1.FindControl("hfColumn")).Value = "Name";
            AccountHeadsubRoundingFill(1);
            divAdd.Style.Add("display", "block");
            divView.Style.Add("display", "none");



        }
        catch (Exception ex)
        {
        }
    }
    public void AccountHeadsubRoundingFill(int pageIndex)
    {
        txtRoundingAccountHeadsub.Text = "";
        txtRefno31.Text = "";
        Populate Populate = new Populate();
        CommonPopup1.CallingFunction = "AccountHeadsubRoundingFill";
        string strName = ((TextBox)CommonPopup1.FindControl("txtName")).Text;
        string Refno = ((TextBox)CommonPopup1.FindControl("txtRefno")).Text;
        string searchCol = ((HiddenField)CommonPopup1.FindControl("hfColumn")).Value;
        searchCol = searchCol == string.Empty ? "[Name]" : "[" + searchCol + "]";
        Refno = Refno == string.Empty || Refno == null ? "0" : Refno;
        Populate.FillPopUpGrid((GridView)CommonPopup1.FindControl("gdvSearch"), "AccountHeadSub AHS LEFT JOIN Customer C ON C.ID_Customer= AHS.Fk_Cuscode AND C.Active=1", "AHS.ID_AccountHeadSub as ID,AHS.HeadSubName as Name", "ID_AccountHeadSub", "ID_AccountHeadSub", " AHS.FK_AccountHead = " + txtRefno30.Text + " AND AHS.FK_Client=" + hfClient.Value.Split(',')[0] + "  AND AHS.Cancelled=0 AND HeadSubName ", strName, null);
        //Populate.FillGrid((GridView)CommonPopup1.FindControl("gdvSearch"), "AccountHeadSub", "Name ,ID_AccountHeadSub as ID,  ID_AccountHeadSub As [Ref No],ShortName", "FK_Client=" + hfClient.Value.Split(',')[0] + " AND (ID_AccountHeadSub =" + Refno + " OR 0=" + Refno + ") AND Active=1  ", searchCol + "  LIKE '" + strName + "%' and Cancelled=0 and Active=1 and AhCode= '" + txtRefno30.Text + "' ", pageIndex, 10, "", (Repeater)CommonPopup1.FindControl("rptPager"), this.Page);
        Populate = null;
        //  txtSubHeadRefno.Focus();
        if (txtRoundingAccountHeadsub.Text == string.Empty)
        {
            lblPopupHeader.Text = "Account Headsub Rounding";
            mpCommon.Show();
        }

    }
    protected void ImageButtonCreditAccHead_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            txtRefno32.Text = "";
            txtcreditAccHead.Text = "";
           
            ((TextBox)CommonPopup1.FindControl("txtName")).Text = txtcreditAccHead.Text.Trim();
            ((TextBox)CommonPopup1.FindControl("txtRefno")).Text = txtRefno32.Text.Trim();
            ((HiddenField)CommonPopup1.FindControl("hfColumn")).Value = "Name";
            AccountHeadCreditFill(1);
            divAdd.Style.Add("display", "block");
            divView.Style.Add("display", "none");



        }
        catch (Exception ex)
        {
        }
    }
    public void AccountHeadCreditFill(int pageIndex)
    {
        txtcreditAccHead.Text = "";
        txtRefno32.Text = "";
        Populate Populate = new Populate();
        CommonPopup1.CallingFunction = "AccountHeadCreditFill";
        string strName = ((TextBox)CommonPopup1.FindControl("txtName")).Text;
        string Refno = ((TextBox)CommonPopup1.FindControl("txtRefno")).Text;
        string searchCol = ((HiddenField)CommonPopup1.FindControl("hfColumn")).Value;
        searchCol = searchCol == string.Empty ? "[Name]" : "[" + searchCol + "]";
        Refno = Refno == string.Empty || Refno == null ? "0" : Refno;
        Populate.FillPopUpGrid((GridView)CommonPopup1.FindControl("gdvSearch"), "AccountHead", "ID_AccountHead as ID,HeadName as Name", "ID_AccountHead", "ID_AccountHead", "FK_Client=" + hfClient.Value.Split(',')[0] + "  AND Cancelled=0 AND HeadName ", strName, null);
        //Populate.FillGrid((GridView)CommonPopup1.FindControl("gdvSearch"), "AccountHead", "Name ,AhCode as ID,  AhCode As [Ref No],ShortName", "FK_Client=" + hfClient.Value.Split(',')[0] + " AND (ID_AccountHead =" + Refno + " OR 0=" + Refno + ") AND Active=1 ", searchCol + "  LIKE '" + strName + "%' and Cancelled=0 and Active=1  ", pageIndex, 10, "", (Repeater)CommonPopup1.FindControl("rptPager"), this.Page,1);
        Populate = null;
        //  txtSubHeadRefno.Focus();
        if (txtcreditAccHead.Text == string.Empty)
        {
            lblPopupHeader.Text = "Account Head Credit";
            mpCommon.Show();
        }

    }

   
    protected void ImageButton1_Click1(object sender, ImageClickEventArgs e)
    {
        cleared();
    }
    protected void chkCredit_CheckedChanged(object sender, EventArgs e)
    {
        if (chkCredit.Checked)
        {
            rfvcreditAccHead.Enabled = true;
        }
        else 
        {
            rfvcreditAccHead.Enabled = false;
        }
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
            BlBillType blBillType = new BlBillType();
            blBillType.MasterId = Convert.ToInt64(hfMasterID.Value);
            blBillType.FK_Client = Convert.ToInt64(hfClient.Value);
            blBillType.UserCode = hfUsername.Value;
            blBillType.FK_Reason = Convert.ToInt64(Reason);
            if (hfIsDelete.Value.ToLower() == "true")
            {
                status = blBillType.DeleteData();
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
            blBillType = null;
        }

       
      
    }
    
}