

/*----------------------------------------------------------------------------------------------------------------------------------------------------------
Purpose    : Insert/Update/Delete Unit 
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
using System.Collections;
using WebPharma.Business.WebPharma;

public partial class ItemRearrange : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //Common.SessionCheck();
        if (!IsPostBack)
        {
          
            //Common cmn = new Common();
            hfUser.Value = Request.QueryString["uid"];
            hfClient.Value = Request.QueryString["clnt"];
            Page.Header.DataBind();
            divAdd.Style.Add("display", "block;");
            CheckUserAccess();
            //AddView();
            BindData();
            Page.Header.DataBind();
            Clear();
        }
       
    }
    protected void imgbtnAdd_Click(object sender, ImageClickEventArgs e)
    {
          Clear();
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        GridFill(1);
         
    }
    protected void gvItemdetails_RowCommand(object sender, GridViewCommandEventArgs e)
    {

    }
    protected void CheckUserAccess()
    {
        BlUserAccess bluseraccess = new BlUserAccess();
        Populate populate = new Populate();

        bluseraccess.FK_Users = Convert.ToInt64(hfUser.Value);
        bluseraccess.FK_Pages = 136;
        bluseraccess.FK_Client = Convert.ToInt64(hfClient.Value);
        bluseraccess.CheckUserAccess();
        if (Convert.ToBoolean(bluseraccess.IsView) == false)
        {
            // Response.Redirect("Home.aspx?");

        }
        if (bluseraccess.FK_Pages != 0)
        {
            BlFunction blfunctions = new BlFunction();
            //lblPageName.Text = Convert.ToString(blfunctions.GetScalarValue("Pages", "ISNULL( PageName,'') PageName", "ID_Pages=" + bluseraccess.FK_Pages.ToString() + " AND  Active=1 ", ""));
        }
        hfIsAdd.Value = (Convert.ToBoolean(bluseraccess.IsAdd)).ToString();
        hfIsUpdate.Value = (Convert.ToBoolean(bluseraccess.IsUpdate)).ToString();
        hfIsDelete.Value = (Convert.ToBoolean(bluseraccess.IsDelete)).ToString();
        bluseraccess = null;
    }
    protected void BindData()
    {
        Populate populate = new Populate();
        populate.FillDropdownList(ddlGeneric, "Generic", "GenericName", "ID_Generic", "GenericName", " FK_Client= " + hfClient.Value+" And Cancelled=0", "");
        populate.FillDropdownList(ddlCategory, "Category", "CategoryName", "ID_Category", "CategoryName", "  FK_Client= " + hfClient.Value + " and Cancelled=0", "");
        populate.FillDropdownList(ddlRack, "Rack", "RackName", "ID_Rack", "RackName", "FK_Client=" + hfClient.Value + " And Cancelled=0", "");
        populate.FillDropdownList(ddlBin, "Bin", "BinName", "ID_Bin", "BinName", " FK_Client=" + hfClient.Value + " And Cancelled=0 AND FK_Rack=" + ddlRack.SelectedValue.ToString(), "");
        populate.FillDropdownList(ddlManufacturer, "Manufacturer", "ManufactureName", "ID_Manufacturer", "ManufactureName", "     FK_Client=" + hfClient.Value + " And Cancelled=0", "");
        populate.FillDropdownList(ddlCategorySecond, "SubCategory", "SubCategoryName", "ID_SubCategory", "SubCategoryName", " FK_Client=" + hfClient.Value + " AND FK_Category= " + ddlCategory.SelectedValue.ToString() + " And Cancelled=0", "");
           
        populate.FillDropdownList(ddlUnit, "Units", "UnitName", "ID_Units", "UnitName", " FK_Client=" + hfClient.Value  + " And Cancelled=0", "");
    }

    protected void GridFill(int currentPage)
    {
        try
        {
            DataTable dtbl = new DataTable();
            BlItemRearrange blItemRearrange = new BlItemRearrange();
            blItemRearrange.FK_Category = ddlCategory.SelectedValue == "" ? 0 : Convert.ToInt64(ddlCategory.SelectedValue);
            blItemRearrange.FK_SubCategory = ddlCategorySecond.SelectedValue == "" ? 0 : Convert.ToInt64(ddlCategorySecond.SelectedValue);
            //blItemRearrange.FK_SubGroup = ddlSubGroup.SelectedValue == "" ? 0 : Convert.ToInt64(ddlSubGroup.SelectedValue);
            blItemRearrange.FK_Rack = ddlRack.SelectedValue == "" ? 0 : Convert.ToInt64(ddlRack.SelectedValue);
            //blItemRearrange.FK_Storage = ddlStorage.SelectedValue == "" ? 0 : Convert.ToInt64(ddlStorage.SelectedValue);
            //blItemRearrange.FK_Size = ddlSize.SelectedValue == "" ? 0 : Convert.ToInt64(ddlSize.SelectedValue);
            blItemRearrange.FK_Generic = ddlGeneric.SelectedValue == "" ? 0 : Convert.ToInt64(ddlGeneric.SelectedValue);
            blItemRearrange.FK_Unit = ddlUnit.SelectedValue == "" ? 0 : Convert.ToInt64(ddlUnit.SelectedValue);

            blItemRearrange.Name = txtitem.Text.Trim();
            blItemRearrange.MasterId = hfFK_Items.Value == "" ? 0 : Convert.ToInt64(hfFK_Items.Value);
            blItemRearrange.Barcode = txtBarcode.Text.Trim();

            blItemRearrange.FK_Manufacturer = ddlManufacturer.SelectedValue == "" ? 0 : Convert.ToInt64(ddlManufacturer.SelectedValue);
            blItemRearrange.FK_Bin = ddlBin.SelectedValue == "" ? 0 : Convert.ToInt64(ddlBin.SelectedValue);

            blItemRearrange.Packed = Convert.ToBoolean(chkPacked.Checked);
            blItemRearrange.StopSale = Convert.ToBoolean(chkStopSale.Checked);
            dtbl = blItemRearrange.SelectData();

            gvItemdetails.DataSource = dtbl;
            gvItemdetails.DataBind();

            blItemRearrange = null;
            ViewState["TempTable"] = dtbl;
            dtbl = null;
        }
        catch (Exception ex) { }
    }
    protected void chkAll_CheckedChanged(object sender, EventArgs e)
    {
        try
        {
            if (gvItemdetails.Rows.Count > 0)
            {
                CheckBox ChkBoxHeader = (CheckBox)gvItemdetails.HeaderRow.FindControl("chkAll");
                foreach (GridViewRow row in gvItemdetails.Rows)
                {
                    CheckBox ChkBoxRows = (CheckBox)row.FindControl("chkBox");
                    if (ChkBoxHeader.Checked == true)
                    {
                        ChkBoxRows.Checked = true;
                    }
                    else
                    {
                        ChkBoxRows.Checked = false;
                    }
                }
            }
        }
        catch (Exception ex)
        {

        }
    }


    protected void ddlCategory_SelectedIndexChanged(object sender, EventArgs e)
    {
        //if (ddlCategory.SelectedIndex > 0)
        //{
            Populate populate = new Populate();
            populate.FillDropdownList(ddlCategorySecond, "SubCategory", "SubCategoryName", "ID_SubCategory", "SubCategoryName", " FK_Client=" + hfClient.Value + " AND FK_Category= " + ddlCategory.SelectedValue.ToString() + " And Cancelled=0", "");
            populate = null;

        //}
        //else
        //{
        //    ddlCategorySecond.Items.Clear();
        //}
    }
    
    protected void ddlChangeType_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlChangeType.SelectedIndex > 0)
            {
                DivData.Visible = true;
                DivSubData.Visible = false;
                DivReason.Visible = false;
                lblData.Text = ddlChangeType.SelectedItem.Text;
                lblChecked.Text = "";
                DivChecked.Visible = false;

                Populate populate = new Populate();
                if (ddlChangeType.SelectedValue == "1")
                {
                    populate.FillDropdownList(ddlData, "Category", "CategoryName", "ID_Category", "CategoryName", "  FK_Client= " + hfClient.Value+" And Cancelled=0", "");
                }                
                else if (ddlChangeType.SelectedValue == "2")
                {
                    populate.FillDropdownList(ddlData, "Manufacturer", "ManufactureName", "ID_Manufacturer", "ManufactureName", "   FK_Client=" + hfClient.Value + " And Cancelled=0", "");
                }
                
                else if (ddlChangeType.SelectedValue == "3")
                {
                    populate.FillDropdownList(ddlData, "Rack", "RackName", "ID_Rack", "RackName", "FK_Client=" + hfClient.Value + " And Cancelled=0", "");
                }
                
                else if (ddlChangeType.SelectedValue == "4")
                {
                    populate.FillDropdownList(ddlData, "Units", "UnitName", "ID_Units", "UnitName", " FK_Client=" + hfClient.Value+" And Cancelled=0", "");
                }
                else if (ddlChangeType.SelectedValue == "7")
                {
                    populate.FillDropdownList(ddlData, "Generic", "GenericName", "ID_Generic", "GenericName", " FK_Client= " + hfClient.Value + " And Cancelled=0", "");
                }
                else
                {
                    DivData.Visible = false;
                    DivSubData.Visible = false;
                    DivReason.Visible = false;
                    ddlData.Items.Clear();
                    ddlSubData.Items.Clear();
                    if (ddlChangeType.SelectedValue == "5")
                    {
                        txtReason.Text = "";
                        DivReason.Visible = true;
                    }
                    lblChecked.Text = ddlChangeType.SelectedItem.Text;
                    DivChecked.Visible = true;
                }
            }
            else
            {
                ddlData.Items.Clear();
                DivData.Visible = false;
                DivSubData.Visible = false;
                DivReason.Visible = false;
            }
        }
        catch (Exception ex)
        {

        }
    }
    protected void ddlData_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {

            DivSubData.Visible = false;
            DivReason.Visible = false;
            if (ddlData.SelectedIndex > 0)
            {
                ddlSubData.Items.Clear();
                DivSubData.Visible = true;
                if (ddlChangeType.SelectedValue == "1")
                {
                    lblSubData.Text = "Sub " + ddlChangeType.SelectedItem.Text;
                    Populate populate = new Populate();
                    populate.FillDropdownList(ddlSubData, "SubCategory", "SubCategoryName", "ID_SubCategory", "SubCategoryName", " FK_Client=" + hfClient.Value + " AND FK_Category= " + ddlData.SelectedValue.ToString() + " And Cancelled=0", "");
                    populate = null;
                }
                //else if (ddlChangeType.SelectedValue == "2")
                //{
                //    lblSubData.Text = "Sub " + ddlChangeType.SelectedItem.Text;
                //    Populate populate = new Populate();
                //    populate.FillDropdownList(ddlSubData, "Subgroup", "SubgroupName", "ID_Subgroup", "SubgroupName", "Cancelled = 0 AND FK_Client=" + hfClient.Value + " AND FK_group= " + ddlData.SelectedValue.ToString(), "");
                //    populate = null;
                //}
                else if (ddlChangeType.SelectedValue == "3")
                {
                    lblSubData.Text = "Bin";
                    Populate populate = new Populate();
                    populate.FillDropdownList(ddlSubData, "Bin", "BinName", "ID_Bin", "BinName", " FK_Client=" + hfClient.Value + " AND FK_Rack= " + ddlData.SelectedValue.ToString() + " And Cancelled=0", "");
                    populate = null;
                }
                else
                {
                    ddlSubData.Items.Clear();
                    DivSubData.Visible = false;
                }
            }
            else
            {
                ddlSubData.Items.Clear();
            }
        }
        catch (Exception ex)
        {

        }
    }
    protected void btnProcess_Click(object sender, EventArgs e)
    {
        int checkfalse = 0;
        string str = "";
        if (ddlChangeType.SelectedIndex > 0)
        {
            if (ddlChangeType.SelectedValue == "1")
            {
                if (ddlData.SelectedIndex <= 0)
                {
                    str = "Select Category";
                }
            }
            else if (ddlChangeType.SelectedValue == "4")
            {
                if (ddlData.SelectedIndex <= 0)
                {
                    str = "Select Unit";
                }
            }

            if (str == "")
            {
                if (gvItemdetails.Rows.Count > 0)
                {
                    BlItemRearrange blchecking = new BlItemRearrange();
                    ArrayList arlist = new ArrayList();
                    DataTable dtbl = (DataTable)ViewState["TempTable"];
                    Items Items;
                    for (int i = 0; i < gvItemdetails.Rows.Count; i++)
                    {
                        if (((System.Web.UI.WebControls.CheckBox)gvItemdetails.Rows[Convert.ToInt16(i)].FindControl("chkBox")).Checked == true)
                        {
                            Items items = new Items();
                            checkfalse = 1;
                            Items = new Items();
                            items.FK_Items = Convert.ToInt64(dtbl.Rows[i]["ID_Items"].ToString());
                            arlist.Add(Items);
                            Items = null;

                        }
                    }
                    blchecking.ChangeTypeID = Convert.ToInt64(ddlChangeType.SelectedValue == "" ? "0" : ddlChangeType.SelectedValue);
                    blchecking.DataID = Convert.ToInt64(ddlData.SelectedValue == "" ? "0" : ddlData.SelectedValue);
                    blchecking.SubDataID = Convert.ToInt64(ddlSubData.SelectedValue == "" ? "0" : ddlSubData.SelectedValue);
                    blchecking.Checked = chkChecked.Checked;
                    blchecking.Reason = txtReason.Text;
                    blchecking.UserCode = hfUser.Value;
                    blchecking.XmlItems = Common.WriteToXml(arlist);
                    if (checkfalse == 1)
                    {
                        long status = 0;
                        if (hfIsAdd.Value.ToLower() == "true")
                        {

                            status = blchecking.InsertData();
                            if (status > 0)
                            {
                                Clear();
                                Populate.InsertMsg(this);
                                 
                            }
                        }

                    }
                    else
                    {
                        Populate.WarningMsg(this,"Please select atleast one item " );

                    }
                }
                else
                {
                    Populate.WarningMsg(this, "Please select atleast one item ");

                }
            }
            else
            {
                Populate.WarningMsg(this, str);

                
            }
        }
        else
        {
            Populate.WarningMsg(this, "Select Change Type");

            
        }
    }
    protected void btnClear_Click(object sender, EventArgs e)
    {
        Clear();
    }
    protected void Clear()
    {
        ViewState["TempTable"] = null;
        gvItemdetails.DataSource = null;
        gvItemdetails.DataBind();

        ddlRack.SelectedIndex = 0;
        //ddlCostcenter.SelectedIndex = 0;
        //ddlStockPoint.SelectedIndex = 0;

        ddlCategory.SelectedIndex = 0;
        ddlCategorySecond.SelectedIndex = 0;
        //ddlGroup.SelectedIndex = 0;
        //ddlSubGroup.Items.Clear();
        txtitem.Text = "";
        hfItem.Value = "";
        hfFK_Items.Value = "0";
        txtBarcode.Text = "";

        ddlManufacturer.SelectedIndex = 0;
        ddlBin.SelectedIndex = 0;
        ddlGeneric.SelectedIndex = 0;
        //ddlBrand.SelectedIndex = 0;
        //ddlColor.SelectedIndex = 0;
        //ddlStorage.SelectedIndex = 0;
        //ddlSize.SelectedIndex = 0;
        ddlUnit.SelectedIndex = 0;

        //chkOnlineitem.Checked = false;
        chkPacked.Checked = false;
        chkStopSale.Checked = false;


        ddlChangeType.SelectedIndex = 0;
        chkChecked.Checked = true;
        txtReason.Text = "";
        DivData.Visible = false;
        DivSubData.Visible = false;
        DivReason.Visible = false;
        DivChecked.Visible = false;
        lblChecked.Text = "";
        lblData.Text = "";
        lblSubData.Text = "";
    }


    //protected void ImageButton3_Click(object sender, ImageClickEventArgs e)
    //{
    //    try
    //    {
    //        ItemName();
    //    }
    //    catch (Exception ex)
    //    {
    //        ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "toastr.warning('" + ex.Message + "')", true);
    //    }
    //}
    public void ItemName()
    {
        try
        {
            ((TextBox)CommonPopup1.FindControl("txtName")).Text = txtitem.Text.Trim();
            ((TextBox)CommonPopup1.FindControl("txtRefno")).Text = txtitem.Text.Trim();
            ((HiddenField)CommonPopup1.FindControl("hfColumn")).Value = "Name";
            ItemFill(1);

        }
        catch (Exception ex)
        {
        }
    }
    public void ItemFill(int pageIndex)
    {
        Item();
        Populate Populate = new Populate();
        CommonPopup1.CallingFunction = "ItemFill";
        string strName = ((TextBox)CommonPopup1.FindControl("txtName")).Text;
        string Refno = ((TextBox)CommonPopup1.FindControl("txtRefno")).Text;
        string searchCol = ((HiddenField)CommonPopup1.FindControl("hfColumn")).Value;
        Refno = Refno == string.Empty || Refno == null ? "0" : Refno;

        Populate.FillPopUpGrid((GridView)CommonPopup1.FindControl("gdvSearch"), "Items", "ID_Items as ID,ItemName as Name", "ID_Items", "ID_Items", "FK_Client=" + hfClient.Value.Split(',')[0] + " AND Combo=0  AND Cancelled=0 AND  (" + ddlCategory.SelectedValue.ToString() + "='0' OR FK_Category = " + ddlCategory.SelectedValue.ToString() + " ) AND     (" + ddlCategorySecond.SelectedValue.ToString() + "='0' OR FK_SubCategory = " + ddlCategorySecond.SelectedValue.ToString() + " ) AND       (" + ddlUnit.SelectedValue.ToString() + "='0' OR FK_Units = " + ddlUnit.SelectedValue.ToString() + " ) AND        (" + ddlManufacturer.SelectedValue.ToString() + "='0' OR FK_Manufacturer = " + ddlManufacturer.SelectedValue.ToString() + " ) AND    (" + ddlRack.SelectedValue.ToString() + "='0' OR FK_Rack = " + ddlRack.SelectedValue.ToString() + " ) AND    (" + ddlBin.SelectedValue.ToString() + "='0' OR FK_Bin = " + ddlBin.SelectedValue.ToString() + " ) AND   (" + ddlGeneric.SelectedValue.ToString() + "='0' OR FK_Generic = " + ddlGeneric.SelectedValue.ToString() + " ) AND    ItemName ", strName, null);
        Populate = null;

        if (txtitem.Text == string.Empty)
        {
            lblPopupHeader.Text = "Item";
            mpCommon.Show();
        }

    }
    public void SetDetails(string strName, string strRefno)
    {
        if (CommonPopup1.CallingFunction == "ItemFill")
        {
            txtitem.Text = strName;
            hfItem.Value = strRefno.ToString();
        }
        
    }

    private void Item()
    {
        txtitem.Text = "";

    }

     
    protected void IbtnItemAdd_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            ItemName();
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "toastr.warning('" + ex.Message + "')", true);
        }
    }
    protected void ddlRack_SelectedIndexChanged(object sender, EventArgs e)
    {
        Populate populate = new Populate();
           
        populate.FillDropdownList(ddlBin, "Bin", "BinName", "ID_Bin", "BinName", " FK_Client=" + hfClient.Value + " And Cancelled=0 AND FK_Rack=" + ddlRack.SelectedValue.ToString(), "");
     
    }
}