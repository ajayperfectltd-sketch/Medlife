using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebPharma.Business.Masters;
using System.Data;
using System.Data.SqlClient;
using System.Collections;

public partial class Quatation : System.Web.UI.Page
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
            BlQuotation blquotation = new BlQuotation();
            blquotation.UserCode = hfuserName.Value;
            blquotation.MasterId = Convert.ToInt64(hfMasterID.Value);
            blquotation.FK_Reason = Convert.ToInt64(Reason);
            if (hfIsDelete.Value.ToLower() == "true")
            {
                status = blquotation.DeleteData();
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
            blquotation = null;
        }
    }
    protected void CheckUserAccess()
    {
        BlUserAccess bluseraccess = new BlUserAccess();
        Populate populate = new Populate();

        bluseraccess.FK_Users = Convert.ToInt64(hfUser.Value);
        bluseraccess.FK_Pages = 49;
        bluseraccess.FK_Client = Convert.ToInt64(hfClient.Value);
        bluseraccess.CheckUserAccess();
        //if (Convert.ToBoolean(bluseraccess.IsView) == false)
        //{
        //    // Response.Redirect("Home.aspx?");

        //}

        hfIsAdd.Value = (Convert.ToBoolean(bluseraccess.IsAdd)).ToString();
        hfIsUpdate.Value = (Convert.ToBoolean(bluseraccess.IsUpdate)).ToString();
        hfIsDelete.Value = (Convert.ToBoolean(bluseraccess.IsDelete)).ToString();
        bluseraccess = null;
    }
    public void ItemName()
    {
        try
        {
            ((TextBox)CommonPopup1.FindControl("txtName")).Text = txtItem.Text.Trim();
            ((TextBox)CommonPopup1.FindControl("txtRefno")).Text = txtItem.Text.Trim();
            ((HiddenField)CommonPopup1.FindControl("hfColumn")).Value = "Name";
            ItemFill(1);
            divAdd.Style.Add("display", "block");
            divView.Style.Add("display", "none");
        }
        catch (Exception ex)
        {
        }
    }
    public void ItemFill(int pageIndex)
    {
        Clearitem();
        Populate Populate = new Populate();
        CommonPopup1.CallingFunction = "ItemFill";
        string strName = ((TextBox)CommonPopup1.FindControl("txtName")).Text;
        string Refno = ((TextBox)CommonPopup1.FindControl("txtRefno")).Text;
        string searchCol = ((HiddenField)CommonPopup1.FindControl("hfColumn")).Value;
        Refno = Refno == string.Empty || Refno == null ? "0" : Refno;

        Populate.FillPopUpGrid((GridView)CommonPopup1.FindControl("gdvSearch"), "Items", "ID_Items as ID,ItemName as Name", "ID_Items", "ID_Items", "FK_Client=" + hfClient.Value.Split(',')[0] + "AND Cancelled=0 AND Combo=0 AND ItemName ", strName, null);
        Populate = null;

        if (txtItem.Text == string.Empty)
        {
            lblPopupHeader.Text = "Item";
            mpCommon.Show();
        }

    }
    private void Clearitem()
    {
        txtItem.Text = "";
        hfSelectedItem.Value = "0";
    }
    public void SetDetails(string strName, string strRefno)
    {
        if (CommonPopup1.CallingFunction == "ItemFill")
        {
            txtItem.Text = strName;
            hfSelectedItem.Value = strRefno.ToString();
        }
        
    }
    protected void BindData()
    {
        Populate populate = new Populate();
        populate.FillDropdownList(ddlUnit, "Units", "UnitName", "ID_Units", "UnitName", " FK_Client=" + hfClient.Value + "AND Cancelled=0", "");
        populate.FillDropdownList(ddlSubUnit, "SubUnit", "SubUnitName", "ID_SubUnits", "SubUnitName", " FK_Client=" + hfClient.Value + "AND Cancelled=0", "");
       
        if (Convert.ToInt64(ddlGroup.SelectedValue) == 1)
            populate.FillDropdownList(ddlType, "Supplier", "SupplierName", "ID_Supplier", "SupplierName", " FK_Client=" + hfClient.Value + "AND Cancelled=0 AND Active=1", "");
        
        else if (Convert.ToInt64(ddlGroup.SelectedValue) == 2)
            populate.FillDropdownList(ddlType, "Customer", "CustomerName", "ID_Customer", "CustomerName", " FK_Client=" + hfClient.Value + "AND Cancelled=0 AND Active=1" , "");
        
        

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
            dtbl.Columns.Add("ItemName");
            dtbl.Columns.Add("FK_item");
            dtbl.Columns.Add("Rate");
            dtbl.Columns.Add("UnitName");
            dtbl.Columns.Add("FK_Units");
            dtbl.Columns.Add("FK_SubUnit");
            dtbl.Columns.Add("SubUnitName");
            dtbl.Columns.Add("FreeQuantity");
            dtbl.Columns.Add("FreeQuantityPer");
            dtbl.Columns.Add("Frequent");
            dtbl.Columns.Add("PercentageWise");
            dtbl.Columns.Add("Discount");
            dtbl.Columns.Add("DiscountPer");
            dtbl.Columns.Add("FrequentPer");
            dtbl.Columns.Add("SupplierOnly");
        }
        if (dtbl.Rows.Count > 0)
        {

            for (int i = 0; i < dtbl.Rows.Count; i++)
            {
                if (dtbl.Rows[i]["FK_item"].ToString() == hfSelectedItem.Value)
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
            drow["ItemName"] = txtItem.Text;
            drow["FK_item"] = hfSelectedItem.Value;
            drow["FK_Units"] = ddlUnit.SelectedValue;
            drow["UnitName"] = ddlUnit.SelectedItem;
            drow["FK_SubUnit"] = ddlSubUnit.SelectedValue;
            drow["SubUnitName"] = ddlSubUnit.SelectedItem;
            drow["FreeQuantity"] = txtFreequantity.Text;
            drow["FreeQuantityPer"] = TxtPer.Text;
            drow["Frequent"] = chkFrequent.Checked;
            drow["PercentageWise"] = chkPercentage.Checked;
            drow["Discount"] = txtDiscount.Text;
            drow["DiscountPer"] = txtPereD.Text;
            drow["Rate"] = txtRate.Text == "" ? "0" : txtRate.Text;
            drow["FrequentPer"] = chkFr.Checked;
            if (Convert.ToInt64(ddlSubUnit.SelectedValue.ToString()) > 0)
                drow["SubUnitName"] = ddlSubUnit.SelectedItem.ToString();
            else
                drow["SubUnitName"] = null;
            drow["SupplierOnly"] = chkIsSupplier.Checked;
            dtbl.Rows.InsertAt(drow, rowCount);
            gvItemsdetails.DataSource = dtbl;
            gvItemsdetails.DataBind();
            ViewState["TempTable"] = dtbl;
            ClerValue();

        }
        else
        {
            txtItem.Text = string.Empty;
            ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "alertify.alert('Item already exists')", true);
        }

    }
    protected void CheckingCombo()
    {
        if (gvItemsdetails.Rows.Count > 0)
        {

        }
        else
        {
        }
    }
    protected void GridFill(int currentPage)
    {
        BlQuotation blQuatation = new BlQuotation();
        DataTable dtbl = new DataTable();
        blQuatation.Type = Convert.ToInt16(ddlGroup.SelectedValue);
        blQuatation.MasterId = Convert.ToInt64(ddlSearchGroup.SelectedValue);
        blQuatation.FK_Client = Convert.ToInt64(hfClient.Value);
        blQuatation.PageIndex = Convert.ToInt16(currentPage);
        blQuatation.Name = txtSearchName.Text.Trim();
        blQuatation.UserCode = hfUser.Value;
        dtbl = blQuatation.SelectAllData();
        gvItems.DataSource = dtbl;
        gvItems.DataBind();
        blQuatation = null;
    }

    #endregion Functions
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Page.Header.DataBind();
            ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "ClearAdd();", true);
            divAdd.Style.Add("display", "block");
            hfUser.Value = Request.QueryString["uid"];
            hfClient.Value = Request.QueryString["clnt"];
           hfuserName.Value = Session["Username"].ToString();
            CheckUserAccess();
            BindData();
            GridFill(1);
            SetReadonly(txtDate,txtEffectTo);
           
            gvItemsdetails.DataSource = null;
            gvItemsdetails.DataBind();
            if (Convert.ToBoolean(hfIsAdd.Value) == false)
            {
                ((Label)CommonPopupPrivilege.FindControl("popuplabel")).Text = "User does not have permission to Add";
                mpPrivilege.Show();
            }
            if (Convert.ToBoolean(hfIsUpdate.Value) == false)
            {
                gvItems.Columns[3].Visible = false;
            }
            if (Convert.ToBoolean(hfIsDelete.Value) == false)
            {
                gvItems.Columns[4].Visible = false;
            }
            
            
        }
    }
    public void SetReadonly(params TextBox[] textbox)
    {
        foreach (TextBox txt in textbox)
            txt.Attributes.Add("readonly", "readonly");

    }

    protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
    {
        btnSubmit.Visible = true;
        gvItemsdetails.DataSource = null;
        gvItemsdetails.DataBind();
        ViewState["TempTable"] = null;
        divAdd.Style.Add("display", "block");
        divView.Style.Add("display", "none");
        ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "ShowAddDiv();ClearAdd();", true);
        if (Convert.ToBoolean(hfIsAdd.Value) == false)
        {
            ((Label)CommonPopupPrivilege.FindControl("popuplabel")).Text = "User does not have permission to Add";
            mpPrivilege.Show();
        }
        ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "ShowAddDiv();", true);
    }
    protected void imgbtnView_Click(object sender, ImageClickEventArgs e)
    {
        try
        {

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
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
           
            SaveData();

        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "toastr.warning('" + ex.Message + "')", true);
        }
    }
    protected void SaveData()
    {
        
        long status = 0;
        BlQuotation blquotation = new BlQuotation();
        blquotation.Type = Convert.ToInt16(ddlGroup.SelectedValue == "" ? "0" : ddlGroup.SelectedValue);
        blquotation.FK_Master = Convert.ToInt64(ddlType.SelectedValue == "" ? "0" : ddlType.SelectedValue);
        blquotation.EffectFrom =Convert.ToDateTime(txtDate.Text);
        blquotation.UserCode = hfuserName.Value;
        blquotation.EffectTo = Convert.ToDateTime(txtEffectTo.Text);
        blquotation.Description = txtDescrption.Text;
        ArrayList arlist = new ArrayList();
        DataTable dtbl = (DataTable)ViewState["TempTable"];
        IItemsDetail IItemsDetails;
        if (gvItemsdetails.Rows.Count > 0)
        {
            for (int i = 0; i < dtbl.Rows.Count; i++)
            {
                IItemsDetails = new IItemsDetail();
                IItemsDetails.FK_Items = Convert.ToInt64(dtbl.Rows[i]["FK_Item"].ToString() == "" ? "0" : dtbl.Rows[i]["FK_Item"].ToString());
                IItemsDetails.Rate = Convert.ToDecimal(dtbl.Rows[i]["Rate"].ToString() == "" ? "0" : dtbl.Rows[i]["Rate"].ToString());
                IItemsDetails.FK_Units = Convert.ToInt64(dtbl.Rows[i]["FK_Units"].ToString() == "" ? "0" : dtbl.Rows[i]["FK_Units"].ToString());
                IItemsDetails.FK_SubUnits = Convert.ToInt64(dtbl.Rows[i]["FK_SubUnit"].ToString() == "" ? "0" : dtbl.Rows[i]["FK_SubUnit"].ToString());
                IItemsDetails.FreeQuantity = Convert.ToDecimal(dtbl.Rows[i]["FreeQuantity"].ToString() == "" ? "0" : dtbl.Rows[i]["FreeQuantity"].ToString());
                IItemsDetails.FreeQuantityPer = Convert.ToDecimal(dtbl.Rows[i]["FreeQuantityPer"].ToString() == "" ? "0" : dtbl.Rows[i]["FreeQuantityPer"].ToString());
                IItemsDetails.Frequent = Convert.ToBoolean(dtbl.Rows[i]["Frequent"].ToString() == "" ? "0" : dtbl.Rows[i]["Frequent"].ToString());
                IItemsDetails.PercentageWise = Convert.ToBoolean(dtbl.Rows[i]["PercentageWise"].ToString() == "" ? "0" : dtbl.Rows[i]["PercentageWise"].ToString());
                IItemsDetails.Discount = Convert.ToDecimal(dtbl.Rows[i]["Discount"].ToString() == "" ? "0" : dtbl.Rows[i]["Discount"].ToString());
                IItemsDetails.DiscountPer = Convert.ToDecimal(dtbl.Rows[i]["DiscountPer"].ToString() == "" ? "0" : dtbl.Rows[i]["DiscountPer"].ToString());
                IItemsDetails.FrequentPer = Convert.ToBoolean(dtbl.Rows[i]["FrequentPer"].ToString() == "" ? "0" : dtbl.Rows[i]["FrequentPer"].ToString());
                IItemsDetails.SupplierOnly = Convert.ToBoolean(dtbl.Rows[i]["SupplierOnly"].ToString() == "" ? "0" : dtbl.Rows[i]["SupplierOnly"].ToString());
                arlist.Add(IItemsDetails);
                IItemsDetails = null;
            }
        }
        else
        {
            ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "toastr.warning('Please add Item')", true);
        }
        blquotation.XmlDetails = Common.WriteToXml(arlist);
        if (hfItemsId.Value == "0")
        {
            if (hfIsAdd.Value.ToLower() == "true")
            {
               status = blquotation.InsertData();
                if (status > 0)
                {
                    ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "ShowAddDiv();", true);
                    ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "toastr.success('Data inserted successfully');ClearAdd();", true);
                    GridFill(1);
                  
                    gvItemsdetails.DataSource = null;
                    gvItemsdetails.DataBind();
                    ViewState["TempTable"] = null;
                    CheckingCombo();
                    ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "ShowAddDiv();", true);
                }
           
            
             
                else
                {
                    ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "ShowAddDiv();", true);
                    ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "toastr.error('Cannot insert data, ShortName/Name already exists')", true);
                    
                }
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "toastr.warning('User does not have permission to add')", true);
                

            }
        }
        else
        {
            if (hfIsUpdate.Value.ToLower() == "true")
            {
                blquotation.MasterId = Convert.ToInt64(hfItemsId.Value);
                blquotation.FK_Client = Convert.ToInt64(hfClient.Value);
                status = blquotation.UpdateData();
                if (status > 0)
                {
                    ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "ShowAddDiv();", true);
                    ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "toastr.success('Data Updated successfully');ClearAdd();", true);
                   // GridFill(1);
                    gvItemsdetails.DataSource = null;
                    gvItemsdetails.DataBind();
                    ViewState["TempTable"] = null;
                    CheckingCombo();
                    
                    ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "ShowAddDiv();", true);
                }
                
                
                else
                {
                    ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "ShowAddDiv();", true);
                    ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "toastr.error('Cannot update data, ShortName/Name already exists')", true);
                    
                }
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "toastr.warning('User does not have permission to Update')", true);
               
            }
        }
        //ddlItemType.SelectedIndex = 0;
        blquotation = null;
    }
    protected void btnClear_Click(object sender, EventArgs e)
    {
        gvItemsdetails.DataSource = null;
        gvItemsdetails.DataBind();
        ViewState["TempTable"] = null;
        ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "ShowAddDiv();", true);
    }
    protected void ClerValue()
    {
        txtItem.Text = String.Empty;
        txtRate.Text = String.Empty;
        ddlUnit.SelectedValue = "0";
        ddlSubUnit.SelectedValue="0";
        TxtPer.Text=String.Empty;
        txtFreequantity.Text = String.Empty;
        chkFrequent.Checked = false;
        chkPercentage.Checked = false;
        txtDiscount.Text = String.Empty;
        chkFr.Checked = false;
        txtPereD.Text = string.Empty;
        chkIsSupplier.Checked = false;

    }
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        try
        {
            if (gvItemsdetails.Rows.Count == 0)
            {
                ViewState["TempTable"] = null;
               
            }

            AddData();
            CheckingCombo();
            divAdd.Style.Add("display", "block");
            divView.Style.Add("display", "none");
            txtItem.Enabled = true;
             txtRate.Enabled = true;
            btnAdd.Enabled = true;
            chkFr.Enabled = true;
            chkPercentage.Enabled = true;

        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "toastr.warning('" + ex.Message + "')", true);
        }
    }
    protected void btnSearch_Click(object sender, EventArgs e)

    {
        divAdd.Style.Add("display", "none");
        divView.Style.Add("display", "block");
        GridFill(1);
        ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "EnableExport();", true);
        

    }
    protected void btnSub_Click(object sender, EventArgs e)
    {

    }
    protected void ImageButton2_Click(object sender, ImageClickEventArgs e)
    {
        Delete();
    }
    protected void imgSearch_Click(object sender, ImageClickEventArgs e)
    {

    }
    protected void IbtnItemAdd_Click(object sender, ImageClickEventArgs e)
    {

    }

    protected void ImageButton4_Click(object sender, ImageClickEventArgs e)
    {
        ItemName();
    }
    protected void ddlGroup_SelectedIndexChanged(object sender, EventArgs e)
    {
        divAdd.Style.Add("display", "block");
        divView.Style.Add("display", "none");
        if (Convert.ToInt64(ddlGroup.SelectedValue) == 1)
        {
            Populate populate = new Populate();
            populate.FillDropdownList(ddlType, "Supplier", "SupplierName", "ID_Supplier", "SupplierName", " FK_Client=" + hfClient.Value + "AND Cancelled=0 AND Active=1", "");
        }
        else if (Convert.ToInt64(ddlGroup.SelectedValue) == 2)
        {
            Populate populate = new Populate();
            populate.FillDropdownList(ddlType, "Customer", "CustomerName", "ID_Customer", "CustomerName", " FK_Client=" + hfClient.Value + "AND Cancelled=0 AND Active=1", "");
        }
    }

    protected void gvItems_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        try
        {

            DataTable dtbl = new DataTable();
            BlQuotation blQuatation = new BlQuotation();
            blQuatation.MasterId = Convert.ToInt64(gvItems.DataKeys[e.NewSelectedIndex]["GroupID"].ToString());
            hfItemsId.Value = blQuatation.MasterId.ToString();
            blQuatation.FK_Client = Convert.ToInt64(hfClient.Value);
            
            blQuatation.UserCode = hfuserName.Value;
            dtbl = blQuatation.SelectQuatationDetails();
            txtDate.Text = Convert.ToDateTime(dtbl.Rows[0]["EffectFrom"].ToString()).ToShortDateString();
            txtEffectTo.Text = Convert.ToDateTime(dtbl.Rows[0]["EffectTo"].ToString()).ToShortDateString();
            txtDescrption.Text = dtbl.Rows[0]["Description"].ToString();
            ddlGroup.SelectedValue = dtbl.Rows[0]["Type"].ToString();
            if (Convert.ToInt64(ddlGroup.SelectedValue) == 1)
            {
                Populate populate = new Populate();
                populate.FillDropdownList(ddlType, "Supplier", "SupplierName", "ID_Supplier", "SupplierName", " FK_Client=" + hfClient.Value + "AND Cancelled=0 AND Active=1", "");
            }
            else if (Convert.ToInt64(ddlGroup.SelectedValue) == 2)
            {
                Populate populate = new Populate();
                populate.FillDropdownList(ddlType, "Customer", "CustomerName", "ID_Customer", "CustomerName", " FK_Client=" + hfClient.Value + "AND Cancelled=0 AND Active=1", "");
            }
        
            ddlType.SelectedValue = dtbl.Rows[0]["FK_Master"].ToString();
           
            //blQuatation.SelectData();
            //ddlGroup.SelectedValue = blQuatation.Type.ToString();
            
            gvItemsdetails.DataSource = dtbl;
            gvItemsdetails.DataBind();
            ViewState["TempTable"] = dtbl;

            btnSubmit.Visible = false;
            btnAdd.Enabled = true;
            blQuatation = null;
            ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "ShowAddDiv();EnablePrint();", true);
            ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "ShowAddDiv();EnablePrint();", true);
            
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "toastr.warning('" + ex.Message + "')", true);
        }
    }
    protected void gvItemsdetails_RowDeleting(object sender, GridViewDeleteEventArgs e)
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
                    gvItemsdetails.DataSource = dtbl;
                    gvItemsdetails.DataBind();
                    ViewState["TempTable"] = dtbl;
                    CheckingCombo();
                }

            }
           
        }
        catch (Exception ex)
        {

        }
        ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "ShowAddDiv();", true);
        divAdd.Style.Add("display", "block");
        divView.Style.Add("display", "none");
    }
    private void validatefreeqty()
    {
        if (chkPercentage.Checked == false)
        {
            chkFr.Enabled = true;
            txtFreequantity.Text = Math.Round(Convert.ToDecimal(txtFreequantity.Text == "" ? "0" : txtFreequantity.Text), 0).ToString();
        }
        else
        {
            chkFr.Enabled = false;
           
        }

    }
    protected void gvItems_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {

            string confirmValue = Request.Form["confirm_value"];
            if (confirmValue.Length > 3)
                confirmValue = confirmValue.Substring(confirmValue.Length - 3, 3);
            if (confirmValue == "Yes")
            {
                long status = 0;
                BlQuotation blquotation = new BlQuotation();
                blquotation.MasterId = Convert.ToInt64(gvItems.DataKeys[e.RowIndex]["GroupID"].ToString());
                hfMasterID.Value = (gvItems.DataKeys[e.RowIndex]["GroupID"].ToString());
                blquotation.FK_Client = Convert.ToInt64(hfClient.Value);
                blquotation.UserCode = hfUser.Value;
                blquotation = null;
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

    protected void chkPercentage_CheckedChanged(object sender, EventArgs e)
    {
        validatefreeqty();
        //ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "oncheckchange(checked);", true);
    }
    protected void txtFreequantity_TextChanged(object sender, EventArgs e)
    {
        validatefreeqty();
    }

    protected void chkFr_CheckedChanged(object sender, EventArgs e)
    {
        if (chkFr.Checked == false)
        {
            chkPercentage.Enabled = true;
           
        }
        else
        {
            chkPercentage.Enabled = false;

        }
    }
    protected void btnComparison_Click(object sender, EventArgs e)
    {

    }

    protected void gvItems_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {

        gvItems.PageIndex = e.NewPageIndex;
        GridFill(e.NewPageIndex);
    }
}