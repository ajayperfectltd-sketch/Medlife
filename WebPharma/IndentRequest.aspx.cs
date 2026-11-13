using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebPharma.Business.Masters;
using WebPharma.Business.WebPharma;

public partial class IndentRequest : System.Web.UI.Page
{

    protected void BindData()
    {
        Populate populate = new Populate();
        populate.FillDropdownList(ddlStore, "Store", "StoName", "ID_Store", "StoName", " Cancelled=0 and Active='True' AND FK_Client= " + hfClient.Value.Split(',')[0], "");
        populate.FillDropdownList(ddlToStore, "Store", "StoName", "ID_Store", "StoName", " Cancelled=0 and Active='True' AND FK_Client= " + hfClient.Value.Split(',')[0], "");
        //populate.FillDropdownList(ddlItem, "Items", "Name", "ID_Items", "ID_Items", "  Active='True' AND ItemType=2 AND FK_Client= " + hfClient.Value.Split(',')[0], "");
        populate.FillDropdownList(ddlSearchStore, "Store", "StoName", "ID_Store", "StoName", "Cancelled=0 and Active='True' AND FK_Client= " + hfClient.Value.Split(',')[0], "");
        populate.FillDropdownList(ddlItem, "Items", "ItemName", "ID_Items", "ItemName", "Cancelled=0 AND FK_Client= " + hfClient.Value.Split(',')[0], "");
    }
    private const string TempTable = "TempTable";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            hfUser.Value = Request.QueryString["uid"];
            hfClient.Value = Request.QueryString["clnt"];
            ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "ClearAdd();", true);
            BindData();
            CheckUserAccess();
            ddlStore.SelectedValue = Session["mainstore"].ToString();
            hfStore.Value = Session["mainstore"].ToString();
            ddlStore.Enabled = false;
            Populate populate = new Populate();
            populate.FillDropdownList(ddlToStore, "Store", "StoName", "ID_Store", "StoName", " Cancelled=0 and Active='True' AND FK_Client= " + hfClient.Value.Split(',')[0] + "AND ID_Store!=" + ddlStore.SelectedValue, "");
            SetReadonly();
            txtDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
            txtDate_CalendarExtender.StartDate = DateTime.Now;
            // txtDate.Attributes["min"] = DateTime.Now.ToString("dd/MM/yyyy");
            txtItemShortName.Focus();
            hfItemId.Value = "";
            hfuserName.Value = Session["Username"].ToString();
            if (Convert.ToBoolean(hfIsAdd.Value) == false)
            {
                ((Label)CommonPopupPrivilege.FindControl("popuplabel")).Text = "User does not have permission to Add";
                mpPrivilege.Show();
            }
            if (Convert.ToBoolean(hfIsUpdate.Value) == false)
            {
               gvViewIndent.Columns[3].Visible = false;
            }
            if (Convert.ToBoolean(hfIsDelete.Value) == false)
            {
                gvViewIndent.Columns[4].Visible = false;
            }
               

        }
    }
    private void ClearAll()
    {
        ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "ClearAdd();", true);
        ClearIndentgrid();
        ddlToStore.SelectedIndex = 0;
        hfIndentreq.Value = "";
        ddlStore.SelectedIndex = 0;
    }
    protected void imgbtnAdd_Click(object sender, ImageClickEventArgs e)
    {
        ClearAll();
        if (Convert.ToBoolean(hfIsAdd.Value) == false)
        {
            ((Label)CommonPopupPrivilege.FindControl("popuplabel")).Text = "User does not have permission to Add";
            mpPrivilege.Show();
        }
        divAdd.Style.Add("display", "block");
        divView.Style.Add("display", "none");
        ddlStore.SelectedValue = Session["mainstore"].ToString();
        ddlStore.Enabled = false;
    }
    protected void imgbtnView_Click(object sender, ImageClickEventArgs e)
    {

        try
        {

            divAdd.Style.Add("display", "none");
            divView.Style.Add("display", "block");
            ClearAll();

            ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "ClearAdd();", true);
            ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "ClearSearch();", true);
            GridFill(1);
        }
        catch (Exception ex)
        {

            ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "toastr.warning('" + ex.Message + "')", true);
        }
    }
    protected void btnItem_Click(object sender, EventArgs e)
    {
        try
        {

        }
        catch (Exception ex)
        {
        }
    }
    protected void btnAddItems_Click(object sender, EventArgs e)
    {

        // int a = 0;
        if (ddlItem.SelectedItem.ToString().Trim() == txtItemShortName.Text.ToString().Trim())
        
        {
            AddDataItem();

            ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "ItemDataClear();", true);
        }
        else
        {
            ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "toastr.warning('Item  does not exist')", true);
        }

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
            BlIndentRequest blIndent = new BlIndentRequest();
            blIndent.UserCode = hfuserName.Value;
            blIndent.CancelledUser = hfuserName.Value;
            blIndent.MasterId = Convert.ToInt64(hfIndentreq.Value);
            blIndent.FK_Reason = Convert.ToInt64(Reason);
            if (hfIsDelete.Value.ToLower() == "true")
            {
                status = blIndent.DeleteData();
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
                // txtSearchName.Focus();
            }
            blIndent = null;
        }
    }

    //private void ClearSearch()
    //{
    //    ddlSearchStore.SelectedIndex = 0;
    //    txtFromDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
    //    txtTodate.Text = DateTime.Now.ToString("dd/MM/yyyy");
    //    ddlSearchStore.Focus();

    //}
    private void ClearIndentgrid()
    {
        ViewState[TempTable] = null;
        txtItemShortName.Text = "";
       // txtItemShortNameCopy.Text = "";
        txtQuantity.Text = "";
        gvItems.DataSource = null;
        gvItems.DataBind();
    }
    public void SetReadonly()
    {
        txtDate.Attributes.Add("readonly", "readonly");
    }
    protected void GridFill(int currentPage)
    {
        BlIndentRequest IndentRequest = new BlIndentRequest();
        DataTable dtbl = new DataTable();
        IndentRequest.FK_Store = Convert.ToInt64(ddlSearchStore.SelectedValue.ToString());

        IndentRequest.FK_Client = Convert.ToInt64(hfClient.Value);
        IndentRequest.UserCode = hfUser.Value;
        IndentRequest.FromDate = Convert.ToDateTime(txtFromDate.Text == "" ? "" + DateTime.Now.Date + "" : txtFromDate.Text);// DateTime.ParseExact(txtFromDateSearch.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
        IndentRequest.ToDate = Convert.ToDateTime(txtTodate.Text == "" ? "" + DateTime.Now.Date + "" : txtTodate.Text);
        //  IndentRequest.FromDate = DateTime.ParseExact(txtFromDate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
        //  IndentRequest.ToDate = DateTime.ParseExact(txtTodate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);

        IndentRequest.PageSize = 10;
        IndentRequest.PageIndex = Convert.ToInt16(currentPage);
        dtbl = IndentRequest.SelectAllData();
        gvViewIndent.DataSource = dtbl;
        gvViewIndent.DataBind();
        ViewState[TempTable] = dtbl;

        IndentRequest = null;
    }
    private DataTable GetDefaultTable()
    {
        DataTable dtbl = new DataTable();
        dtbl.Columns.Add("SLNO");
        dtbl.Columns.Add("Item");
        dtbl.Columns.Add("Quantity");
        dtbl.Columns.Add("FK_Items");
        dtbl.Columns.Add("FK_Stock");
        dtbl.Columns.Add("CurrentStock");
        return dtbl;
    }
    protected void CheckUserAccess()
    {
        BlUserAccess bluseraccess = new BlUserAccess();
        Populate populate = new Populate();

        bluseraccess.FK_Users = Convert.ToInt64(hfUser.Value);
        bluseraccess.FK_Pages = 53;
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

        if (ddlStore.SelectedValue == ddlToStore.SelectedValue)
        {
            ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script",
                "alertify.alert('From Store and To Store cannot be the same');", true);
            ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "ShowAddDiv();", true);
            return;
        }
        long status = 0;
        BlIndentRequest blIndent = new BlIndentRequest();
        blIndent.FK_Store = Convert.ToInt64(ddlStore.SelectedValue);
        blIndent.ToStore = Convert.ToInt64(ddlToStore.SelectedValue);
        blIndent.InReqDate = DateTime.ParseExact(txtDate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
        // blIndent.TotalAmt = Convert.ToDecimal(txtAmount.Text);
        blIndent.UserCode = hfuserName.Value;
        blIndent.EnteredOn = DateTime.Now;
        blIndent.Cancelled = false;
        blIndent.FK_Client = Convert.ToInt16(hfClient.Value.Split(',')[0]);
        ArrayList arlist1 = new ArrayList();
        DataTable dtbl1 = (DataTable)ViewState[TempTable];
        IIndentRequestDetails Iindentdtails;
        for (int i = 0; i < dtbl1.Rows.Count; i++)
        {
            Iindentdtails = new IIndentRequestDetails();
            Iindentdtails.SlNo = Convert.ToInt32(i + 1);
            Iindentdtails.FK_Items = Convert.ToInt64(dtbl1.Rows[i]["FK_Items"].ToString());
           // Iindentdtails.FK_Stock = Convert.ToInt64(dtbl1.Rows[i]["FK_Stock"].ToString());
            Iindentdtails.Quantity = decimal.Parse(dtbl1.Rows[i]["Quantity"].ToString());
            arlist1.Add(Iindentdtails);
        }
        blIndent.XmlIndentRequestItems = Common.WriteToXml(arlist1);
        if (hfIndentreq.Value == "")
        {
            if (hfIsAdd.Value.ToLower() == "true")
            {
                status = blIndent.InsertData();
                if (status > 0)
                {
                    Populate.InsertMsg(this);
                    GridFill(1);
                    ClearAll();
                    ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "toastr.success('Data Inserted Successfully');alertify.alert('Reference Number:  " + status + "');", true);

                    ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "ClearAdd();", true);

                    ddlStore.SelectedValue = Session["mainstore"].ToString();
                    ddlStore.Enabled = false;
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "ShowAddDiv();", true);
                    ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "toastr.error('Cannot insert data')", true);
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
                blIndent.MasterId = Convert.ToInt64(hfIndentreq.Value.Split(',')[0]);
                status = blIndent.UpdateData();
                if (status > 0)
                {
                    ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "ShowAddDiv();", true);
                    ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "toastr.success('Data Updated Successfully');ClearAdd();", true);
                    GridFill(1);
                    ClearAll();
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "ShowAddDiv();", true);
                    ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "toastr.error('Cannot update data')", true);

                }
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "toastr.warning('User does not have permission to Update')", true);

            }
            blIndent = null;
        }
    }
    protected void AddDataItem()
    {
        DataTable dtbl = new DataTable();
        int inRowCount = 0;
        int inCheck = 0;
        decimal decqty = decimal.Parse(txtQuantity.Text == "" ? "0" : txtQuantity.Text);
        //decimal decCS = decimal.Parse(hdfCurrentStock.Value.Split(',')[0] == "" ? "0" : hdfCurrentStock.Value.Split(',')[0]);
        //if (decqty > decCS)
        //{
        //    string str = "Quantity must be less than or equal to " + decCS.ToString();
        //    ScriptManager.RegisterStartupScript(Page, typeof(Page), "script", "alertify.alert('" + str + "')", true);
        //    return;

        //}
        if (ViewState["TempTable"] != null)
        {
            dtbl = (DataTable)ViewState["TempTable"];
            inRowCount = dtbl.Rows.Count;
        }
        else
        {

            dtbl = GetDefaultTable();
        }
        if (hfItemId.Value != "")
        {

            gvItems.Enabled = true;
            dtbl.Rows.RemoveAt(Convert.ToInt32(hfItemId.Value));
            gvItems.DataSource = dtbl;
            gvItems.DataBind();
            hfSlNo.Value = hfItemId.Value;
            hfItemId.Value = "";
            // ViewState["TempTable"] = dtbl;


        }
        if (dtbl.Rows.Count > 0)
        {

            for (int i = 0; i < dtbl.Rows.Count; i++)
            {
                if (dtbl.Rows[i]["FK_Items"].ToString() == ddlItem.SelectedValue.ToString())
                {
                    inCheck = inCheck + 1;
                    break;
                }
            }
        }
        //if (dtbl.Rows.Count > 0)
        //{

        //    for (int i = 0; i < dtbl.Rows.Count; i++)
        //    {
        //        if (dtbl.Rows[i]["FK_Stock"].ToString() == hdfFKStock.Value)
        //        {
        //            inCheck = inCheck + 1;
        //            break;
        //        }

        //    }

        //}

        if (inCheck == 0)
        {
            DataRow drow = dtbl.NewRow();
            if (hfSlNo.Value == "")
            {
                if (dtbl.Rows.Count > 0)
                {

                    Int64 max = Convert.ToInt64(dtbl.Rows[dtbl.Rows.Count - 1]["SLNO"].ToString());
                    drow["SLNO"] = Convert.ToInt64(max) + 1;
                }
                else
                {
                    drow["SLNO"] = 1;
                }
            }
            else
            {
                drow["SLNO"] = Convert.ToInt64(hfSlNo.Value) + 1;

            }



         //   drow["Item"] = hfItem.Value;
            drow["Item"] = ddlItem.SelectedItem.ToString();
            drow["FK_Items"] = ddlItem.SelectedValue.ToString();
            drow["Quantity"] = decqty;
          //  drow["FK_Stock"] = hdfFKStock.Value;
            dtbl.Rows.InsertAt(drow, inRowCount);
            gvItems.DataSource = dtbl;
            gvItems.DataBind();

            ViewState["TempTable"] = dtbl;


        }
        else
        {
            ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "alertify.alert('Item already exists')", true);
            // return 1;
        }
        // return 0;

    }
    protected void AddData()
    {
        DataTable dtbl = new DataTable();
        int rowCount = 0;
        int rowindex = 0;
        int inCheck = 0;


        decimal decqty = decimal.Parse(txtQuantity.Text == "" ? "0" : txtQuantity.Text);
        decimal decCS = decimal.Parse(hdfCurrentStock.Value.Split(',')[0] == "" ? "0" : hdfCurrentStock.Value.Split(',')[0]);
        if (decqty > decCS)
        {
            string str = "Quantity must be less than or equal to " + decCS.ToString();
            ScriptManager.RegisterStartupScript(Page, typeof(Page), "script", "alertify.alert('" + str + "')", true);
            return;
        }
       
        if (ViewState[TempTable] != null)
        {
            dtbl = (DataTable)ViewState[TempTable];
            rowCount = dtbl.Rows.Count;
        }
        else
        {
            dtbl = GetDefaultTable();


        }
        DataRow drow;

        if (hdfRowindex.Value.Split(',')[0] == "")
        {

            if (dtbl.Rows.Count > 0)
            {



                for (int i = 0; i < dtbl.Rows.Count; i++)
                {
                    if (dtbl.Rows[i]["FK_Stock"].ToString() == hdfFKStock.Value.Split(',')[0])
                    {
                        inCheck = inCheck + 1;

                        break;
                    }

                }
            }

            if (inCheck == 0)
            {
                drow = dtbl.NewRow();
                drow["Item"] = hfItem.Value;
                drow["Item"] = txtItemShortName.Text.Trim();
                drow["Quantity"] = decimal.Parse(txtQuantity.Text == "" ? "0" : txtQuantity.Text).ToString("0.0000");
               // drow["FK_Items"] = txtfkIssuedItems.Text.Split(',')[0];
                drow["FK_Stock"] = hdfFKStock.Value.Split(',')[0];
                drow["CurrentStock"] = hdfCurrentStock.Value.Split(',')[0];

                dtbl.Rows.Add(drow);
            }
            else
            {
                if (inCheck > 0)
                    ScriptManager.RegisterStartupScript(Page, typeof(Page), "script", "alertify.alert('Stock Item already exists')", true);

            }

        }
        else
        {
            int i = int.Parse(hdfRowindex.Value.Split(',')[0]);
            if (dtbl.Rows.Count > 0)
            {

                for (int j = 0; j < dtbl.Rows.Count; j++)
                {
                    if (dtbl.Rows[j]["FK_Stock"].ToString() == hdfFKStock.Value.Split(',')[0] && j != i)
                    {

                        inCheck = inCheck + 1;

                        break;
                    }


                }


            }


            if (inCheck == 0)
            {

                drow = dtbl.Rows[i];
                drow["Item"] = txtItemShortName.Text.Trim();
                drow["Quantity"] = decimal.Parse(txtQuantity.Text == "" ? "0" : txtQuantity.Text).ToString("0.0000");
                //drow["FK_Items"] = txtfkIssuedItems.Text.Split(',')[0];
                drow["FK_Stock"] = hdfFKStock.Value.Split(',')[0];
                // drow["TotalAmt"] = (decimal.Parse(drow["Quantity"].ToString()) * decimal.Parse(drow["SalPrice"].ToString())).ToString("0.0000");
                drow["CurrentStock"] = hdfCurrentStock.Value.Split(',')[0];

            }
            else
            {
                ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "alertify.alert('Stock Item already exists')", true);

            }
        }


        ViewState[TempTable] = dtbl;

        gvItems.DataSource = dtbl;
        gvItems.DataBind();
        txtItemShortName.Focus();
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            if (gvItems.Rows.Count > 0)
            {
                SaveData();
            }
            else
            {


                ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "alertify.alert('Atleast one Item Required')", true);
            }
            AddView();
        }
        catch (Exception ex)
        {
        }

    }
    private void AddView()
    {
        divAdd.Style.Add("display", "block");
        divView.Style.Add("display", "none");
        ddlToStore.Focus();
    }
    protected void btnClear_Click(object sender, EventArgs e)
    {
        ClearAll();
        AddView();
        ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "ClearAdd();", true);
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        try
        {
            //  blMessage.Items.Clear();
            GridFill(1);
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "toastr.warning('" + ex.Message + "')", true);
        }
    }
    protected void ImageButton2_Click(object sender, ImageClickEventArgs e)
    {
        Delete();
    }
    //protected void gvViewIndent_RowDeleting(object sender, GridViewDeleteEventArgs e)
    //{
    //    try
    //    {
    //        // blMessage.Items.Clear();
    //        try
    //        {
    //            string confirmValue = Request.Form["confirm_value"];
    //            if (confirmValue.Length > 3)
    //                confirmValue = confirmValue.Substring(confirmValue.Length - 3, 3);
    //            if (confirmValue == "Yes")
    //            {
    //                long status = 0;
    //                BlIssueOut BlIssue = new BlIssueOut();
    //                BlIssue.MasterId = Convert.ToInt64(gvViewIndent.DataKeys[e.RowIndex]["ID_IndentRequest"].ToString());
    //                hfIndentreq.Value = (gvViewIndent.DataKeys[e.RowIndex]["ID_IndentRequest"].ToString());
    //                BlIssue.FK_Client = Convert.ToInt64(hfClient.Value);
    //                BlIssue.UserCode = hfUser.Value.Split(',')[0];

    //                BlIssue = null;
    //            }
    //            else
    //            {
    //                mpDelete.Hide();
    //            }
    //        }
    //        catch (Exception ex)
    //        {
    //            ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "toastr.warning('" + ex.Message + "')", true);
    //        }


    //    }
    //    catch (Exception ex)
    //    {
    //    }
    //}
    protected void gvViewIndent_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            string confirmValue = Request.Form["confirm_value"];
            if (string.IsNullOrEmpty(confirmValue)) return;
            if (confirmValue.Length > 3)
                confirmValue = confirmValue.Substring(confirmValue.Length - 3, 3);
            if (confirmValue == "Yes")
            {
                long status = 0;
                BlIndentRequest blIndent = new BlIndentRequest();
                blIndent.MasterId = Convert.ToInt64(gvViewIndent.DataKeys[e.RowIndex]["ID_IndentRequest"].ToString());
                hfIndentreq.Value = (gvViewIndent.DataKeys[e.RowIndex]["ID_IndentRequest"].ToString());
                blIndent.FK_Client = Convert.ToInt64(hfClient.Value);
                blIndent.UserCode = hfUser.Value.Split(',')[0];
                blIndent.CancelledUser = hfuserName.Value;
                if (hfIsDelete.Value.ToLower() == "true")
                {
                    status = blIndent.DeleteData();
                    if (status > 0)
                    {
                        GridFill(1);
                        ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "toastr.success('Data Deleted Successfully')", true);
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
                blIndent = null;
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
    protected void gvViewIndent_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        try
        {
            // blMessage.Items.Clear();
            ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "ShowAddDiv();", true);
            BlIndentRequest blIndentReq = new BlIndentRequest();
            blIndentReq.MasterId = Convert.ToInt64(gvViewIndent.DataKeys[e.NewSelectedIndex]["ID_IndentRequest"].ToString());
            hfIndentreq.Value = blIndentReq.MasterId.ToString();
            blIndentReq.FromDate = DateTime.Parse("1/1/1753");
            blIndentReq.ToDate = DateTime.MaxValue;
            blIndentReq.FK_Store = blIndentReq.FK_Store;
            blIndentReq.FK_Client = Convert.ToInt64(hfClient.Value);
            blIndentReq.SelectData();
            ddlStore.SelectedValue = blIndentReq.FK_Store.ToString();
            Populate populate = new Populate();
            populate.FillDropdownList(ddlToStore, "Store", "StoName", "ID_Store", "StoName", " Cancelled=0 and Active='True' AND FK_Client= " + hfClient.Value.Split(',')[0] + "AND ID_Store!=" + ddlStore.SelectedValue, "");
            ddlToStore.SelectedValue = blIndentReq.ToStore.ToString();
            txtDate.Text = blIndentReq.InReqDate.ToString("dd/MM/yyyy");
            //txtAmount.Text = blIndentReq.TotalAmt.ToString();
            DataTable dt = new DataTable();
            dt = blIndentReq.SelectIndentRequestItems();
            ViewState[TempTable] = dt;
            gvItems.DataSource = dt;
            gvItems.DataBind();

            AddView();
            ddlStore.Enabled = false;
            blIndentReq = null;

        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "toastr.warning('" + ex.Message + "')", true);
        }
    }
    protected void gvItems_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        
         DataTable dtbl = new DataTable();
         if (ViewState["TempTable"] != null)
         {
             dtbl = (DataTable)ViewState["TempTable"];
             ddlItem.SelectedValue = Convert.ToString(((Label)gvItems.Rows[e.NewSelectedIndex].FindControl("lblgvFK_Items")).Text);
             txtItemShortName.Text = Convert.ToString(((Label)gvItems.Rows[e.NewSelectedIndex].FindControl("lblgvItem")).Text);
             txtQuantity.Text = Convert.ToString(((Label)gvItems.Rows[e.NewSelectedIndex].FindControl("lblgvquty")).Text);
             hfItemId.Value = Convert.ToString(e.NewSelectedIndex);
         }
        //AddView();
    }
    protected void gvItems_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {

            // blMessage.Items.Clear();
            try
            {
                string confirmValue = Request.Form["confirm_value"];
                if (confirmValue.Length > 3)
                    confirmValue = confirmValue.Substring(confirmValue.Length - 3, 3);
                if (confirmValue == "Yes")
                {
                    if (ViewState[TempTable] != null)
                    {

                        DataTable dtbl = (DataTable)ViewState[TempTable];
                        dtbl.Rows.RemoveAt(e.RowIndex);
                        gvItems.DataSource = dtbl;
                        gvItems.DataBind();

                        ViewState[TempTable] = dtbl;

                    }
                }
                AddView();

            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "toastr.warning('" + ex.Message + "')", true);
            }

        }
        catch (Exception ex)
        {
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
    protected void ddlItemsFill()
    {
        //if (ddlItem.SelectedIndex > 0)
        //{


            BlItems blitems = new BlItems();
            blitems.MasterId = Convert.ToInt64(Convert.ToInt64(HfItemsID.Value.ToString()));
            blitems.FK_Client = Convert.ToInt64(hfClient.Value);
            blitems.SelectData();
            txtItemShortName.Text = blitems.Name.ToString();

       // }
    }
    protected void gvViewIndent_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvViewIndent.PageIndex = e.NewPageIndex;
        GridFill(e.NewPageIndex);
    }
}