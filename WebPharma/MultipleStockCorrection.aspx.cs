
/*----------------------------------------------------------------------------------------------------------------------------------------------------------
Purpose    : Insert Multiple Stock 
Author     : BIBIN TC
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
using WebPharma.Business.WebPharma;
using System.Data;
using System.Collections;
using System.Globalization;

public partial class MultipleStockCorrection : System.Web.UI.Page
{
    protected void BindData()
    {
        Populate populate = new Populate();
        populate.FillDropdownList(ddlItem, "Items", "Name", "ID_Items", "Name", "  Active='True' AND ItemType=2 AND FK_Client= " + hfClient.Value.Split(',')[0], "");
        populate.FillDropdownList(ddlUnit, "Units", "Name", "ID_Units", "Name", " FK_Client= " + hfClient.Value.Split(',')[0], "");
        populate = null;
    }
    protected void ddlItemsFill()
    {
        if (ddlItem.SelectedIndex > 0)
        {
            BlItems blitems = new BlItems();
            blitems.MasterId = Convert.ToInt64(ddlItem.SelectedValue.ToString());
            blitems.FK_Client = Convert.ToInt64(hfClient.Value);
            blitems.SelectData();

            ddlUnit.SelectedValue = blitems.FK_Units.ToString();
            txtItemShortName.Text = blitems.Name.ToString();
            //hfPacked.Value = blitems.Packed.ToString();
            //txtPurchaseVat.Text = blitems.Purchasevat.ToString();
            //txtVat.Text = blitems.Purchasevat.ToString();

            blitems = null;

            BlUnit blunit = new BlUnit();
            blunit.MasterId = Convert.ToInt64(ddlUnit.SelectedValue);
            blunit.FK_Client = Convert.ToInt64(hfClient.Value);
            blunit.SelectData();
            //hfUnitQuantity.Value = blunit.NoofUnits.ToString();
            blunit = null;


            LoadAllStocks();
            imgPopUp.Focus();
            


        }
    }
    protected void LoadAllStocks()
    {

        BlMultipleStockCorrection blmultiplestockentry = new BlMultipleStockCorrection();
        blmultiplestockentry.FK_Client = Convert.ToInt64(hfClient.Value);
        blmultiplestockentry.FK_Items = Convert.ToInt64(ddlItem.SelectedValue);
        DataTable dtbl = new DataTable();
        dtbl = blmultiplestockentry.SelectStockDetails();

        if (dtbl.Rows.Count > 0)
        {
            GvStockDetails.DataSource = dtbl;
            GvStockDetails.DataBind();
            DivStockDetails.Visible = true;
        }
        else
        {
            GvStockDetails.DataSource = null;
            GvStockDetails.DataBind();
        }
    }
    protected void CheckUserAccess()
    {
        BlUserAccess bluseraccess = new BlUserAccess();
        Populate populate = new Populate();

        bluseraccess.FK_Users = Convert.ToInt64(hfUser.Value);
        bluseraccess.FK_Pages = 80;
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
    public void Save()
    {
        long status = 0;
        BlMultipleStockCorrection blmultiplestockentry = new BlMultipleStockCorrection();
        blmultiplestockentry.FK_Items = Convert.ToInt64(ddlItem.SelectedValue);
        blmultiplestockentry.UserCode = hfUser.Value;
        blmultiplestockentry.FK_Client = Convert.ToInt64(hfClient.Value);
        ArrayList arlist = new ArrayList();
        IStockDetails istockdetails;
        for (int i = 0; i < GvStockDetails.Rows.Count; i++)
        {
            istockdetails = new IStockDetails();
            istockdetails.FK_Stock = Convert.ToInt64(((Label)GvStockDetails.Rows[i].FindControl("lblStock")).Text);
            istockdetails.FK_Store = Convert.ToInt64(((Label)GvStockDetails.Rows[i].FindControl("lblStore")).Text);
            istockdetails.OpeningStock = Convert.ToDecimal(((Label)GvStockDetails.Rows[i].FindControl("lblOpeningStock")).Text);
            istockdetails.CurrentStock = Convert.ToDecimal(((Label)GvStockDetails.Rows[i].FindControl("lblCurrentStock")).Text);
            istockdetails.ActualStock = Convert.ToDecimal(((TextBox)GvStockDetails.Rows[i].FindControl("txtActualStock")).Text);
            arlist.Add(istockdetails);
        }
        blmultiplestockentry.XmlMultipleStocks = Common.WriteToXml(arlist);
        if (arlist.Count > 0)
        {
            if (hfIsAdd.Value.ToLower() == "true")
            {
                status = blmultiplestockentry.InsertData();
                if (status > 0)
                {
                    ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "toastr.success('Data inserted successfully');ClearAdd();", true);
                }
                else if (status == -2)
                {
                    ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "toastr.warning('Cannot insert negative stock,Please check it')", true);
                }
                else
                {
                    Populate.ErrorMessageBox(this, "Cannot insert data.");
                }
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "toastr.warning('User does not have permission to add')", true);

            }
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (DateTime.Now > Convert.ToDateTime("03/04/2017"))
            {
                ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "toastr.warning('Please contact software vendor')", true);
            }
            else
            {
                if (!IsPostBack)
                {
                    //btnSubmit.Visible = true;
                    hfUser.Value = Request.QueryString["uid"];
                    hfClient.Value = Request.QueryString["clnt"];
                    Page.Header.DataBind();
                    ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "ClearAdd();", true);
                    divAdd.Style.Add("display", "block");
                    CheckUserAccess();
                    BindData();
                    //GridFill(1);
                    txtItemShortName.Focus();
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
            divAdd.Style.Add("display", "none");
            divView.Style.Add("display", "block");
            ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "ClearAdd();", true);
            //GridFill(1);
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "toastr.warning('" + ex.Message + "')", true);
        }
    }
    protected void ddlItem_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            ddlItemsFill();
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "toastr.warning('" + ex.Message + "')", true);
        }
    }
    protected void imgbtnAdd_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            divAdd.Style.Add("display", "block");
            divView.Style.Add("display", "none");
            //btnSubmit.Visible = true;
            ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", " ShowAddDiv(); ClearAdd(); releasebtn();", true);

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
            if (GvStockDetails.Rows.Count > 0)
            {
                Save();
                txtItemShortName.Focus();
            }

            else
            {
                ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "alertify.alert('Atleast one item required')", true);
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "toastr.warning('" + ex.Message + "')", true);
        }
    }
}