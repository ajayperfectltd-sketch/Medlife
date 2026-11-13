/*----------------------------------------------------------------------------------------------------------------------------------------------------------
Purpose    : Insert/Update/Delete Rack Arrangements
* Author     : BIBIN tC
Copyright  : Perfect Software Solutions (Clt) Pvt Ltd
Created on : 16/05/2018
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
using System.Collections;

public partial class RackArrangement : System.Web.UI.Page
{
    #region Functions
  
    protected void BindData()
    {
        Populate populate = new Populate();
        populate.FillDropdownList(ddlRack, "Rack", "RackName", "ID_Rack", "ID_Rack", "FK_Client = " + hfClient.Value, "");
    //    populate.FillDropdownList(ddlItem, "Items", "Name", "ID_Items", "ID_Items", "  Active='True' AND ItemType=2 AND FK_Client= " + hfClient.Value.Split(',')[0], "");
        populate.FillDropdownList(ddlSearchRack, "Rack", "RackName", "ID_Rack", "ID_Rack", "FK_Client = " + hfClient.Value, "");
       // populate.FillDropdownList(ddlSearchItem, "Items", "Name", "ID_Items", "ID_Items", "  Active='True' AND ItemType=2 AND FK_Client= " + hfClient.Value.Split(',')[0], "");
        gvRackItem.DataBind();
    }
    protected void GridFill(int currentPage)
    {
        BlRackArrangement blRackArrangement = new BlRackArrangement();
        DataTable dtbl = new DataTable();
        blRackArrangement.FK_Rack = Convert.ToInt64(ddlSearchRack.SelectedValue);
        blRackArrangement.FK_Item = Convert.ToInt64(hfFK_Items.Value);
        blRackArrangement.FK_Client = Convert.ToInt64(hfClient.Value);
        blRackArrangement.UserCode = hfUser.Value;
        blRackArrangement.PageIndex = Convert.ToInt16(currentPage);
        dtbl = blRackArrangement.SelectAllGroupData();
        gvRackArrangement.DataSource = dtbl;
        gvRackArrangement.DataBind();
        if (dtbl.Rows.Count > 0)
        {
            int recordCount = Convert.ToInt32(dtbl.Rows[0]["RecordCount"].ToString());
            this.PopulatePager(recordCount, currentPage);
        }
        blRackArrangement = null;
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
        bluseraccess.FK_Pages = 63;
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
        BlRackArrangement blRackArrangement = new BlRackArrangement();
        blRackArrangement.FK_Rack = Convert.ToInt64(ddlRack.SelectedValue);
       
        ArrayList arlist1 = new ArrayList();
        DataTable dtbl1 = (DataTable)ViewState["RackItem"];
        if (dtbl1!=null && dtbl1.Rows.Count > 0)
        {
            RackItems RackItems;
            for (int i = 0; i < dtbl1.Rows.Count; i++)
            {
                RackItems = new RackItems();
                // IPurchaseItemDetails.FK_MaterialReciept = 0;
                RackItems.FK_Items = Convert.ToInt64(dtbl1.Rows[i]["FK_Items"].ToString());
                RackItems.Quantity = Convert.ToDecimal(dtbl1.Rows[i]["Quantity"].ToString());
                //     RackItems.GroupID = Convert.ToInt64(dtbl1.Rows[i]["GroupID"].ToString());
                RackItems.ID_RackArrangement = Convert.ToInt64(dtbl1.Rows[i]["ID_RackArrangement"].ToString());
                arlist1.Add(RackItems);
                RackItems = null;
            }
            blRackArrangement.xmlRackItems = Common.WriteToXml(arlist1);
            blRackArrangement.FK_Client = Convert.ToInt64(hfClient.Value);
            blRackArrangement.UserCode = hfUser.Value;

            if (hfGroupID.Value == "0")
            {
                if (hfIsAdd.Value.ToLower() == "true")
                {
                    status = blRackArrangement.InsertData();
                    if (status > 0)
                    {
                        GridFill(1);
                        ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "ShowAddDiv();", true);
                        ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "toastr.success('Data inserted successfully');Clear();", true);

                    }
                    else
                    {
                        ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "ShowAddDiv();", true);
                        //    ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "toastr.error('Cannot insert data, Rack arrangement already set for this item')", true);
                        ddlRack.Focus();
                    }
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "toastr.warning('User does not have permission to add')", true);
                    ddlRack.Focus();

                }
            }
            else
            {
                if (hfIsUpdate.Value.ToLower() == "true")
                {

                    blRackArrangement.GroupID = Convert.ToInt64(hfGroupID.Value);
                    status = blRackArrangement.UpdateData();
                    if (status > 0)
                    {
                        ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "ShowAddDiv();", true);
                        ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "toastr.success('Data updated successfully');Clear();", true);
                        GridFill(1);

                    }
                    else
                    {
                        ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "ShowAddDiv();", true);
                        // ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "toastr.error('Cannot update data, Reason code already exists')", true);
                        ddlRack.Focus();
                    }
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "toastr.warning('User does not have permission to Update')", true);
                    ddlRack.Focus();
                }

            }
        }
        else
        {

            ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "toastr.warning('Atleast one item required')", true);
        }
        ViewState["RackItem"] = null;
        gvRackItem.DataSource = null;
        gvRackItem.DataBind();
        blRackArrangement = null;
    }
    protected void AddDataItems()
    {
        DataTable dtbl = new DataTable();
        int rowCount = 0;
        int inCheck = 0;
        if (ViewState["RackItem"] != null)
        {
            dtbl = (DataTable)ViewState["RackItem"];
            rowCount = dtbl.Rows.Count;
        }
        else
        {
            dtbl.Columns.Add("SLNO");
            dtbl.Columns.Add("Item");
            dtbl.Columns.Add("FK_Items");
            dtbl.Columns.Add("Quantity");
            dtbl.Columns.Add("GroupID");
            dtbl.Columns.Add("ID_RackArrangement");

        }
        if (dtbl.Rows.Count > 0)
        {

            for (int i = 0; i < dtbl.Rows.Count; i++)
            {

                if (dtbl.Rows[i]["FK_Items"].ToString() == hfFK_Items.Value)
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
            drow["Item"] = hfItem.Value;
            drow["FK_Items"] = hfFK_Items.Value;
            drow["Quantity"] = txtQuantity.Text == "" ? "0" : txtQuantity.Text;
            drow["GroupID"] = 0;
            drow["ID_RackArrangement"] = 0;
            dtbl.Rows.InsertAt(drow, rowCount);
            gvRackItem.DataSource = dtbl;
            gvRackItem.DataBind();
            ViewState["RackItem"] = dtbl;

        }
        else
        {
            ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "toastr.warning('Item already exists')", true);

        }
    }

   
    #endregion
    #region Events

    protected void gvRackArrangement_RowDeleting(object sender, GridViewDeleteEventArgs e)
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
                BlRackArrangement blRackArrangement = new BlRackArrangement();
                blRackArrangement.GroupID = Convert.ToInt64(gvRackArrangement.DataKeys[e.RowIndex]["GroupID"].ToString());
                blRackArrangement.UserCode = hfUser.Value;
                if (hfIsDelete.Value.ToLower() == "true")
                {
                    status = blRackArrangement.DeleteGroupData();
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
                blRackArrangement = null;
            }
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
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        try
        {
            //blMessage.Items.Clear();
            if (hfItem.Value.Trim() == txtSearchItem.Text.ToString().Trim() || txtSearchItem.Text=="")
            {
                GridFill(1);
                ddlSearchRack.Focus();
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "toastr.warning('" + ex.Message + "')", true);
        }
    }
    //protected void btnItem_Click(object sender, EventArgs e)
    //{
    //   // ddlItem_SelectedIndexChanged(sender, e);
    //  //  txtitem.Text = ddlItem.SelectedItem.ToString();

    //}
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        if (hfItem.Value.Trim() == txtitem.Text.ToString().Trim())
        {
        AddDataItems();
        txtitem.Text = "";
        txtQuantity.Text = "";
        txtitem.Focus();
        ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "ShowAddDiv();", true);
        }
        else
        {
            ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "toastr.warning('Item  does not exist')", true);

        }
    
    }
    protected void gvRackItem_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        //blMessage.Items.Clear();
        try
        {
            string confirmValue = Request.Form["confirm_value"];
            if (confirmValue.Length > 3)
                confirmValue = confirmValue.Substring(confirmValue.Length - 3, 3);
            if (confirmValue == "Yes")
            {
                if (ViewState["RackItem"] != null)
                {
                    DataTable dtbl = (DataTable)ViewState["RackItem"];
                    dtbl.Rows.RemoveAt(e.RowIndex);

                    for (int i = 0; i < dtbl.Rows.Count; i++)
                    {
                        dtbl.Rows[i]["SLNO"] = i+1;
                    }

                    gvRackItem.DataSource = dtbl;
                    gvRackItem.DataBind();

                    ViewState["RackItem"] = dtbl;
                    
                }
            }
            ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "ShowAddDiv();", true);
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "toastr.warning('" + ex.Message + "')", true);
        }
    }
    protected void gvRackArrangement_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        try
        {
            //blMessage.Items.Clear();
            //ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "ShowAddDiv();", true);
            BlRackArrangement blRackArrangement = new BlRackArrangement();
           // blRackArrangement.MasterId = Convert.ToInt64(gvRackArrangement.DataKeys[e.NewSelectedIndex]["ID_RackArrangement"].ToString());
          //  hfRackArrangementId.Value = blRackArrangement.MasterId.ToString();
            hfGroupID.Value = gvRackArrangement.DataKeys[e.NewSelectedIndex]["GroupID"].ToString();
            ddlRack.SelectedValue = (gvRackArrangement.DataKeys[e.NewSelectedIndex]["FK_Rack"].ToString());
            blRackArrangement.FK_Client = Convert.ToInt64(hfClient.Value);
            blRackArrangement.UserCode = hfUser.Value;
            blRackArrangement.GroupID = Convert.ToInt64(hfGroupID.Value);

            DataTable dtblItems = blRackArrangement.SelectAllData();
            ViewState["RackItem"] = dtblItems;
            gvRackItem.DataSource = dtblItems;
            gvRackItem.DataBind();

            //txtCode.Text = blRack.Code.ToString();
            //txtRackName.Text = blRack.RackName.ToString();
            //txtDescription.Text = blRack.Description.ToString();
            blRackArrangement = null;
            divAdd.Style.Add("display", "block");
            divView.Style.Add("display", "none");
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "toastr.warning('" + ex.Message + "')", true);
        }
    }
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
                BindData();

                GridFill(1);
                CheckUserAccess();
                ddlRack.Focus();
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "toastr.warning('" + ex.Message + "')", true);
        }
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            //blMessage.Items.Clear();

            SaveData();

            ddlRack.Focus();
        }
        catch (Exception ex)
        {
            //blMessage.Items.Add(ex.Message);
        }
    }
    protected void imgbtnView_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            //blMessage.Items.Clear();
            ViewState["RackItem"] = null;
            gvRackItem.DataSource = null;
            gvRackItem.DataBind();
            divAdd.Style.Add("display", "none");
            divView.Style.Add("display", "block");
            ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "ClearAdd();", true);
            GridFill(1);
            ddlSearchRack.Focus();
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "toastr.warning('" + ex.Message + "')", true);
        }
    }
    protected void btnClear_Click(object sender, EventArgs e)
    {
        ViewState["RackItem"] = null;
        gvRackItem.DataSource = null;
        gvRackItem.DataBind();
      
    }

    #endregion
}