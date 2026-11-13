using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebPharma.Business.Masters;
using System.Data;
using System.Globalization;

public partial class RentCollection : System.Web.UI.Page
{
    #region Functions
   
    protected void CheckUserAccess()
    {
        BlUserAccess bluseraccess = new BlUserAccess();
        Populate populate = new Populate();

        bluseraccess.FK_Users = Convert.ToInt64(hfUser.Value);
        bluseraccess.FK_Pages = 67;
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
    protected void BindData()
    {
        Populate populate = new Populate();
        populate.FillDropdownList(ddlSupplier, "Supplier", "Name", "ID_Supplier", "ID_Supplier", "  Active='True'  and FK_Client = " + hfClient.Value.Split(',')[0], "");
       
        populate.FillDropdownList(ddlSearchRack, "Rack", "RackName", "ID_Rack", "ID_Rack", "FK_Client = " + hfClient.Value, "");
        populate.FillPaymentMode(ddlPaymentMode);
        populate = null;
       txtTransDate.Text = DateTime.Now.ToShortDateString();
     
    }
    protected void GridFill(int currentPage)
    {
        BlRentCollection blRentCollection = new BlRentCollection();
        DataTable dtbl = new DataTable();
        blRentCollection.FK_Rack = Convert.ToInt64(ddlSearchRack.SelectedValue);
        blRentCollection.FK_Client = Convert.ToInt64(hfClient.Value);
        blRentCollection.UserCode = hfUser.Value;
        blRentCollection.PageIndex = Convert.ToInt16(currentPage);
        dtbl = blRentCollection.SelectAllData();
        gvRentCollection.DataSource = dtbl;
        gvRentCollection.DataBind();
        if (dtbl.Rows.Count > 0)
        {
            int recordCount = Convert.ToInt32(dtbl.Rows[0]["RecordCount"].ToString());
            this.PopulatePager(recordCount, currentPage);
        }
        blRentCollection = null;
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
    public void SetReadonly()
    {
        txtTransDate.Attributes.Add("readonly", "readonly");
    }
    protected void SaveData()
    {
        if (txtRackName.Text.Trim() == hfRack.Value)
        {
            long status = 0;
            BlRentCollection blRentCollection = new BlRentCollection();
            blRentCollection.FK_Rack = Convert.ToInt64(hfFK_Rack.Value);
            blRentCollection.TransDate = DateTime.ParseExact(txtTransDate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            blRentCollection.PaymentMode = Convert.ToInt32(ddlPaymentMode.SelectedValue);
            blRentCollection.Amount = Convert.ToDecimal(txtAmount.Text);
            blRentCollection.Vacate = chkVacate.Checked;
            blRentCollection.FK_Supplier = Convert.ToInt64(ddlSupplier.SelectedValue);
            blRentCollection.FK_RackAllotment = Convert.ToInt64(hfRackAllotment.Value);
            if (hfRentCollectionId.Value == "0")
            {
                if (hfIsAdd.Value.ToLower() == "true")
                {
                    status = blRentCollection.InsertData();
                    if (status > 0)
                    {
                        GridFill(1);
                        ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "ShowAddDiv();", true);
                        ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "toastr.success('Data inserted successfully');ClearAdd();", true);

                    }
                    else
                    {
                        ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "ShowAddDiv();", true);
                        ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "toastr.error('Cannot insert data, Rack code / Rack Name already exists')", true);
                        //   ddlRack.Focus();
                    }
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "toastr.warning('User does not have permission to add')", true);
                    //   ddlRack.Focus();

                }
            }
            else
            {
                if (hfIsUpdate.Value.ToLower() == "true")
                {
                    blRentCollection.MasterId = Convert.ToInt64(hfRentCollectionId.Value);
                    status = blRentCollection.UpdateData();
                    if (status > 0)
                    {
                        ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "ShowAddDiv();", true);
                        ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "toastr.success('Data updated successfully');ClearAdd();", true);

                        GridFill(1);

                    }
                    else
                    {
                        ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "ShowAddDiv();", true);
                        ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "toastr.error('Cannot update data, Reason code already exists')", true);
                        //  ddlRack.Focus();
                    }
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "toastr.warning('User does not have permission to Update')", true);
                    // ddlRack.Focus();
                }
            }
            blRentCollection = null;
        }
        else
        {
            ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "toastr.error('Invalid Rack');", true);
        }
    }
    
    #endregion
    #region Events
    protected void gvRentCollection_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        try
        {
            //blMessage.Items.Clear();
            ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "ShowAddDiv();", true);
            BlRentCollection blRentCollection = new BlRentCollection();
            blRentCollection.MasterId = Convert.ToInt64(gvRentCollection.DataKeys[e.NewSelectedIndex]["ID_RentCollection"].ToString());
            hfRentCollectionId.Value = blRentCollection.MasterId.ToString();
            blRentCollection.FK_Client = Convert.ToInt64(hfClient.Value);
            blRentCollection.UserCode = hfUser.Value;

            blRentCollection.SelectData();
          //  ddlRack.SelectedValue = blRentCollection.FK_Rack.ToString();
            txtRackName.Text = blRentCollection.RackName;
            txtTransDate.Text = blRentCollection.TransDate.ToShortDateString();
            ddlPaymentMode.SelectedValue = blRentCollection.PaymentMode.ToString();
            txtAmount.Text = blRentCollection.Amount.ToString();
            chkVacate.Checked = blRentCollection.Vacate;
            ddlSupplier.SelectedValue = blRentCollection.FK_Supplier.ToString();
            blRentCollection = null;
            panelAdd.Enabled = false;
            btnSubmit.Visible = false;
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "toastr.warning('" + ex.Message + "')", true);
        }
    }
    protected void gvRentCollection_RowDeleting(object sender, GridViewDeleteEventArgs e)
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
                BlRentCollection blRentCollection = new BlRentCollection();
                blRentCollection.MasterId = Convert.ToInt64(gvRentCollection.DataKeys[e.RowIndex]["ID_RentCollection"].ToString());
                blRentCollection.UserCode = hfUser.Value;
                if (hfIsDelete.Value.ToLower() == "true")
                {
                    status = blRentCollection.DeleteData();
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
                blRentCollection = null;
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
            //blMessage.Items.Clear();
            GridFill(1);
            ddlSearchRack.Focus();
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
           // blMessage.Items.Clear();
            SaveData();
          //  ddlRack.Focus();
        }
        catch (Exception ex)
        {
           // blMessage.Items.Add(ex.Message);
        }
    }
    protected void Page_Changed(object sender, EventArgs e)
    {
        //blMessage.Items.Clear();
        int pageIndex = int.Parse((sender as LinkButton).CommandArgument);
        GridFill(pageIndex);
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
                SetReadonly();
                GridFill(1);
                CheckUserAccess();
                ddlSupplier.Focus();
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "toastr.warning('" + ex.Message + "')", true);
        }
    }

    //protected void ddlSupplier_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    Populate populate = new Populate();
    //   // populate.FillDropdownList(ddlRack, "Rack r inner join RackAllotment ra on ra.FK_Rack=r.ID_Rack", "Code+' - '+RackName", "ID_Rack", "ID_Rack", "ra.FK_Client = " + hfClient.Value + "  And ra.vacate=0 and ra.FK_Supplier=" + ddlSupplier.SelectedValue, "");

    //}
    protected void imgbtnAdd_Click(object sender, EventArgs e)
    {
        try
        {
               divAdd.Style.Add("display", "block");
               divView.Style.Add("display", "none");
               panelAdd.Enabled = true;
               btnSubmit.Visible = true;
               chkVacate.Checked = false;
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "toastr.warning('" + ex.Message + "')", true);
        }
    }
   
    #endregion
}