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

public partial class Indent : System.Web.UI.Page
{
    private const string GridData = "GridData";
    protected void BindData()
    {
        Populate populate = new Populate();
        populate.FillDropdownList(ddlStore, "Store", "StoName", "ID_Store", "StoName", " Cancelled=0 and Active='True' AND FK_Client= " + hfClient.Value.Split(',')[0], "");
        //populate.FillDropdownList(ddlSearchStore, "Store", "StoName", "ID_Store", "StoName", "Cancelled=0 and Active='True' AND FK_Client= " + hfClient.Value.Split(',')[0], "");
       // populate.FillDropdownList(ddlItem, "Items", "ItemName", "ID_Items", "ItemName", "Cancelled=0 AND FK_Client= " + hfClient.Value.Split(',')[0], "");
    }
    protected void CheckUserAccess()
    {
        BlUserAccess bluseraccess = new BlUserAccess();
        Populate populate = new Populate();

        bluseraccess.FK_Users = Convert.ToInt64(hfUser.Value);
        bluseraccess.FK_Pages = 54;
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
    public void SetReadonly()
    {
        txtDate.Attributes.Add("readonly", "readonly");
    }
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
            txtDate_CalendarExtender.StartDate = DateTime.Now;
           // txtItemShortName.Focus();
           // hfItemId.Value = "";
            hfuserName.Value = Session["Username"].ToString();
            txtReq.Enabled = false;
            imgReqNo.Enabled = true;
            txtReqno.Enabled = true;
            txtToStock.Visible = false;
            txtIndentStock.Visible = false;
            lblToStock.Visible = false;
            //lblCrntstk.Visible = false;
            lblIndent.Visible = false;
            txtSalesPrice.Enabled = false;
            txtCurrentstk.Enabled = false;
            btnApproval.Visible = false;
            txtIndentStock.Enabled = false;
            txtToStock.Enabled = false;
            if (Convert.ToBoolean(hfIsAdd.Value) == false)
            {
                ((Label)CommonPopupPrivilege.FindControl("popuplabel")).Text = "User does not have permission to Add";
                mpPrivilege.Show();
            }
            if (Convert.ToBoolean(hfIsUpdate.Value) == false)
            {
                 gvIndent.Columns[5].Visible = false;
            }
            if (Convert.ToBoolean(hfIsDelete.Value) == false)
            {
                 gvIndent.Columns[6].Visible = false;
            }
            ddlToStore.Focus();

        }
    }
    protected void imgbtnAdd_Click(object sender, ImageClickEventArgs e)
    {
        ClearAll();
        Clear();
        ddlStore.SelectedValue = Session["mainstore"].ToString();
        if (Convert.ToBoolean(hfIsAdd.Value) == false)
        {
            ((Label)CommonPopupPrivilege.FindControl("popuplabel")).Text = "User does not have permission to Add";
            mpPrivilege.Show();
        }
        divAdd.Style.Add("display", "block");
        divView.Style.Add("display", "none");
        ddlStore.Enabled = false;
        txtReqno.Enabled = true;
        imgReqNo.Enabled = true;

    }
    public void ClearAll()
    {
        ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "ClearAdd();", true);
        gvItem.DataSource = null;
        gvItem.DataBind();
        ViewState[GridData] = null;
    }
    public void Clear()
    {
        ViewState[GridData] = null;
        gvItem.DataSource = null;
        gvItem.DataBind();
        txtReqno.Text = "";
        ddlToStore.SelectedIndex = -1;
        btnApproval.Visible = false;
        btnSubmit.Visible = true;

    }
    protected void imgbtnView_Click(object sender, ImageClickEventArgs e)
    {
       // ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "SearchClear();", true);
        divAdd.Style.Add("display", "none");
        divView.Style.Add("display", "block");
        GridFill(1);
    }
    protected void GridFill(int currentPage)
    {
        BlIndent blIndent = new BlIndent();
        DataTable dtbl = new DataTable();
       // blpurchase.FK_Store = Convert.ToInt64(ddlStoreSearch.SelectedValue);
        //blIndent.ToStore = Convert.ToInt64(ddlToStore.SelectedValue);
        blIndent.FK_Client = Convert.ToInt64(hfClient.Value);
        blIndent.UserCode = hfUser.Value;
        blIndent.FromDate = Convert.ToDateTime(txtFromDateSearch.Text == "" ? "" + DateTime.Now.Date + "" : txtFromDateSearch.Text);// DateTime.ParseExact(txtFromDateSearch.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
        blIndent.Todate = Convert.ToDateTime(txtToDateSearch.Text == "" ? "" + DateTime.Now.Date + "" : txtToDateSearch.Text);// DateTime.ParseExact(txtToDateSearch.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
        dtbl = blIndent.SelectIndentData();
        gvIndent.DataSource = dtbl;
        gvIndent.DataBind();
        blIndent = null;
    }

    protected void imgReqNo_Click(object sender, ImageClickEventArgs e)
    {
        try
        {


            ClearReq();
            ((TextBox)CommonPopup1.FindControl("txtName")).Text = txtReqno.Text.Trim();
            ((TextBox)CommonPopup1.FindControl("txtRefno")).Text = txtReqno.Text.Trim();
            ((HiddenField)CommonPopup1.FindControl("hfColumn")).Value = "Name";
            Requestfill(1);
            divAdd.Style.Add("display", "block");
            divView.Style.Add("display", "none");
        }
        catch (Exception ex)
        {
        }
    }
    public void Requestfill(int pageIndex)
    {
        ClearReq();
        //Populate Populate = new Populate();
        //CommonPopup1.CallingFunction = "Requestfill";
        //string strName = ((TextBox)CommonPopup1.FindControl("txtName")).Text;
        //string Refno = ((TextBox)CommonPopup1.FindControl("txtRefno")).Text;
        //string searchCol = ((HiddenField)CommonPopup1.FindControl("hfColumn")).Value;
        //searchCol = searchCol == string.Empty ? "[Name]" : "[" + searchCol + "]";

        //Refno = Refno == string.Empty || Refno == null ? "0" : Refno;
        //Populate.FillPopUpGrid((GridView)CommonPopup1.FindControl("gdvSearch"), "IndentRequest", "ID_IndentRequest as ID,TransDate as  Name,FK_Store as FromStore,ToStore,Remarks", "ID_IndentRequest", "ID_IndentRequest ", "FK_Store=" + ddlToStore.SelectedValue+"AND ToStore=" + ddlStore.SelectedValue+"FK_Client=" + hfClient.Value.Split(',')[0] + "AND Cancelled=0 and TransDate", strName, null);
        //Populate = null;

        //if (txtReqno.Text == string.Empty)
        //{
        //    lblPopupHeader.Text = "Request";
        //    mpCommon.Show();
        //}
        BlIndent blIndent = new BlIndent();
        Populate Populate = new Populate();
        CommonPopup1.CallingFunction = "Requestfill";
        string Refno = ((TextBox)CommonPopup1.FindControl("txtRefno")).Text;
        string searchCol = ((HiddenField)CommonPopup1.FindControl("hfColumn")).Value;
        Refno = Refno == string.Empty || Refno == null ? "0" : Refno;
        GridView gvNew = (GridView)CommonPopup1.FindControl("gdvSearch");
        DataTable dt = new DataTable();
        blIndent.FK_Store = Convert.ToInt64(ddlStore.SelectedValue);
        blIndent.ToStore = Convert.ToInt64(ddlToStore.SelectedValue);
        blIndent.FK_Client = Convert.ToInt64(hfClient.Value);
        dt = blIndent.SelectRequestPopup();
     
        gvNew.DataSource = dt;
       // gvNew.Columns[2].Visible = false;
        gvNew.DataBind();

        if (txtReqno.Text == string.Empty)
        {
            lblPopupHeader.Text = "Request";
            mpCommon.Show();
            ((TextBox)CommonPopup1.FindControl("txtName")).Visible = false;
            ((Button)CommonPopup1.FindControl("btnSearch")).Visible = false;
            ((Button)CommonPopup1.FindControl("btnClearSearchPopup")).Visible = false;
            ((Label)CommonPopup1.FindControl("popuplabel")).Visible = false;
           
           // ((LinkButton)CommonPopup1.FindControl("lbtnName")).Visible = false;
          // ((LinkButton)CommonPopup1.FindControl("lbtnName")).Text = "Date";
            
            
        }

    }
    public void SetDetails(string strName, string strRefno)
    {
        if (CommonPopup1.CallingFunction == "Requestfill")
        {
            txtReqno.Text = strRefno;
            BindGridData(strRefno);
        }
    }

    protected int BindGridData(string strRefno)
    {
        BlIndent blIndent = new BlIndent();
        blIndent.ReqID = Convert.ToInt32(strRefno.ToString());
        hfID.Value = (strRefno.ToString());
        DataTable dtbl = blIndent.SelectIntentReq();
        gvItem.DataSource = dtbl;
        gvItem.DataBind();
        if (dtbl.Rows.Count == 0)
        {
            ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "alertify.alert('Item  does not have stock')", true);
            return 0;
        }

            
       // gvItem.Columns[4].Visible = true;
        ViewState[GridData] = dtbl;
        return 1;

    }
    private void ClearReq()
    {

        txtReqno.Text = "";

    }
   
   
    protected void btnItem_Click(object sender, EventArgs e)
    {
        BlStock blStock = new BlStock();
        blStock.MasterId = Convert.ToInt64(hdfFKStock.Value);
        blStock.UserCode = hfUser.Value;
        blStock.FK_Client = Convert.ToInt64(hfClient.Value);
        blStock.FK_Store = Convert.ToInt32(ddlStore.SelectedValue);
        blStock.Tostore = Convert.ToInt32(ddlToStore.SelectedValue);
      
        blStock.SelectData();
        hfItemId.Value = blStock.FK_Items.ToString();
      
      
      //  txtQuantity.Text = blStock.CurrentStock.ToString();
        txtToStock.Text = blStock.Tostore.ToString();
        txtSalesPrice.Text = blStock.SalePrice.ToString();
        txtCurrentstk.Text = blStock.CurrentStock.ToString();
        blStock = null;
       
    }
    protected void btnAddItems_Click(object sender, EventArgs e)
    {
        int a = 0;
        if (hfItem.Value.Trim() == txtitem.Text.ToString().Trim())
        {
            a = AddDataItem();

            ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "ItemClear();", true);
        }
        else
        {
            ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "toastr.warning('Item  does not exist')", true);
        }
    }
    private DataTable GetDataTable(DataTable dtbl)
    {
        dtbl.Columns.Add("slno");
        dtbl.Columns.Add("ItemName");
        dtbl.Columns.Add("FK_Items");
        dtbl.Columns.Add("BatchNo");
        dtbl.Columns.Add("ExpiryDate",typeof(string));
        dtbl.Columns.Add("RqueQuantity");
        dtbl.Columns.Add("TotalAmt", typeof(decimal));
        dtbl.Columns.Add("MRP");
        dtbl.Columns.Add("SalPrice");
        dtbl.Columns.Add("ID_Stock");
        dtbl.Columns.Add("CurrentStock");
        dtbl.Columns.Add("TotalAmount");
        dtbl.Columns.Add("Qty");
        
        
        return dtbl;
    }
    protected int AddDataItem()
    {
        DataTable dtbl = new DataTable();
        int inRowCount = 0;
        int inCheck = 0;
        if (ViewState[GridData] != null)
        {
            dtbl = (DataTable)ViewState[GridData];
            inRowCount = dtbl.Rows.Count;
        }
        else
        {

            dtbl = GetDataTable(dtbl);
        }
        if (hdfItem.Value != "")
        {

           // gvItem.Enabled = true;
            dtbl.Rows.RemoveAt(Convert.ToInt32(hdfItem.Value));
            gvItem.DataSource = dtbl;
            gvItem.DataBind();
           // hfSlNo.Value = hfItemId.Value;
            hdfItem.Value = "";
            // ViewState["TempTable"] = dtbl;


        }
        if (dtbl.Rows.Count > 0)
        {

            for (int i = 0; i < dtbl.Rows.Count; i++)
            {
                if (dtbl.Rows[i]["ID_Stock"].ToString() == hdfFKStock.Value)
                {
                    inCheck = inCheck + 1;
                    break;
                }
              
            }

        }
        if (inCheck == 0)
        {
          
            string BatchNo = "";
            decimal MRP = 0;
            decimal SalesPrice = 0;
            decimal currentStock = 0;
            decimal Quantity = 0;
            DateTime     Expiry;
            BlStock blStock = new BlStock();
            blStock.MasterId = Convert.ToInt64(hdfFKStock.Value==""?"0":hdfFKStock.Value);
            blStock.UserCode = hfUser.Value;
          blStock.FK_Client = Convert.ToInt64(hfClient.Value);
          blStock.FK_Store = Convert.ToInt32(ddlStore.SelectedValue);
          blStock.Tostore = Convert.ToInt32(ddlToStore.SelectedValue);
           blStock.SelectData();
            SalesPrice=blStock.SalePrice;
            BatchNo = blStock.BatchNo;
            MRP = blStock.MRP;
            currentStock = blStock.CurrentStock;
            Expiry = (blStock.ExpiryDate);
            string Ex = Expiry.ToShortDateString() ;
            decimal decqty = Math.Round(Convert.ToDecimal(txtQuantity.Text == "" ? "0" : txtQuantity.Text), 4);
            decimal Total = Math.Round((decqty * SalesPrice), 4);
            Quantity = Convert.ToDecimal(txtQuantity.Text);
            DataRow drow = dtbl.NewRow();
            drow["slno"] = inRowCount + 1;
            drow["ItemName"] = hfItem.Value;
            drow["FK_Items"] = hdfFkItem.Value;
            drow["RqueQuantity"] = txtReq.Text;
            drow["BatchNo"] = BatchNo;
            drow["ExpiryDate"] = Ex;
               // Convert.ToDateTime( DateTime.Parse(Expiry,"dd/MM/yyyy",CultureInfo.InvariantCulture));
              //  ( DateTime.ParseExact(Expiry, "dd/MM/yyyy", CultureInfo.InvariantCulture));
          //  drow["ExpiryDate"] = DateTime.Parse(Expiry.ToShortDateString(), "dd/MM/yyyy", CultureInfo.InvariantCulture);
               // Expiry.ToString("dd/MM/yyyy");
                //DateTime.Parse(blStock.ExpiryDate.ToString("dd/MM/yyyy"), "dd/MM/yyyy", CultureInfo.InvariantCulture);
           
           // drow["ExpiryDate"] = DateTime.Now.ToShortDateString();
           // drow["TotalAmt"] = Math.Round(Total, 2);
            drow["MRP"] = MRP;
            drow["SalPrice"] = SalesPrice;
            drow["ID_Stock"] = hdfFKStock.Value;
            drow["CurrentStock"] = currentStock; 
            drow["TotalAmount"] = Math.Round((Quantity * SalesPrice), 4);
            drow["Qty"] =  txtQuantity.Text;
            dtbl.Rows.InsertAt(drow, inRowCount);

            gvItem.DataSource = dtbl;
            gvItem.DataBind();
           // gvItem.Columns[4].Visible = false;
           // TotalAmount(dtbl);
            ViewState[GridData] = dtbl;
           

        }
        else
        {
            ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "alertify.alert('Item already exists')", true);
            return 1;
        }
        return 0;

    }
    protected void gvItem_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        try
        {
            if (gvItem.Rows.Count > 0)
            {
                DataTable dtbl = new DataTable();
                if (ViewState["GridData"] != null)
                {
                    dtbl = (DataTable)ViewState["GridData"];

                    //  ddlItem.SelectedValue = Convert.ToString(((Label)gvItem.Rows[e.NewSelectedIndex].FindControl("lblgvFK_Items")).Text);
                    txtitem.Text = Convert.ToString(((Label)gvItem.Rows[e.NewSelectedIndex].FindControl("lblItem")).Text);
                    txtQuantity.Text = Convert.ToString(((Label)gvItem.Rows[e.NewSelectedIndex].FindControl("lblQty")).Text);
                   // txtQuantity.Text = Convert.ToString(((Label)gvItem.Rows[e.NewSelectedIndex].FindControl("lblReqquantity")).Text);
                  //  txtQuantity.Text = Convert.ToString(((Label)gvItem.Rows[e.NewSelectedIndex].FindControl("lblQty")).Text);
                    txtSalesPrice.Text = Convert.ToString(((Label)gvItem.Rows[e.NewSelectedIndex].FindControl("lblSalesPrice")).Text);
                  // txtCurrentstk.Text = Convert.ToString(((Label)gvItem.Rows[e.NewSelectedIndex].FindControl("lblCurrentStock")).Text);
                   // txtCurrentstk.Text = Convert.ToString(((Label)gvItem.Rows[e.NewSelectedIndex].FindControl("lblQty")).Text);
                    txtCurrentstk.Text = Convert.ToString(((Label)gvItem.Rows[e.NewSelectedIndex].FindControl("lblCurrentStock")).Text);
                 
                    txtReq.Text = Convert.ToString(((Label)gvItem.Rows[e.NewSelectedIndex].FindControl("lblReqquantity")).Text);
                    hdfItem.Value = Convert.ToString(e.NewSelectedIndex);
                    hfItem.Value = txtitem.Text;
                    hdfFKStock.Value = Convert.ToString(gvItem.DataKeys[e.NewSelectedIndex]["ID_Stock"].ToString());
                    hdfFkItem.Value = Convert.ToString(gvItem.DataKeys[e.NewSelectedIndex]["FK_Items"].ToString());
                    BlIndent  blin=new BlIndent();
                    blin.FK_Stock = Convert.ToInt64(hdfFKStock.Value);
                    blin.MasterId = Convert.ToInt64(hfID_Indent.Value==""?"0":hfID_Indent.Value);
                    blin.FK_Items = Convert.ToInt64( hdfFkItem.Value);
                    blin.ToStoreE = Convert.ToInt64(dtbl.Rows[0]["FK_StoreTo"].ToString());
                    DataSet dts=blin.SelectIndentStockEdit();
                    if(dts.Tables[0].Rows.Count>0)
                    txtIndentStock.Text=dts.Tables[0].Rows[0]["Quantity"].ToString();
                    txtIndentStock.Visible = true;
                   
                    lblIndent.Visible = true;
                    if(dts.Tables[1].Rows.Count>0)
                    txtToStock.Text = dts.Tables[1].Rows[0]["Currentstock"].ToString()==""?"0":dts.Tables[1].Rows[0]["Currentstock"].ToString();
                    txtToStock.Visible = true;
                    lblToStock.Visible = true;
                   
                }
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "toastr.warning('" + ex.Message + "')", true);
        }
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
        BlIndent blIndent = new BlIndent();
        blIndent.FK_Store = Convert.ToInt64(ddlStore.SelectedValue);
        blIndent.ToStore = Convert.ToInt64(ddlToStore.SelectedValue);
        blIndent.IndentDate = Convert.ToDateTime(txtDate.Text);
        blIndent.ReqID =Convert.ToInt64(txtReqno.Text==""?"0":txtReqno.Text);
        blIndent.EnteredOn = Convert.ToDateTime(DateTime.Now);
        blIndent.FK_Client = Convert.ToInt64(hfClient.Value);
        
        blIndent.UserCode = hfuserName.Value;
        ArrayList arlist = new ArrayList();
        DataTable dtbl = (DataTable)ViewState["GridData"];
         IIndentItemDetails IIndentitem;
        for (int i = 0; i < dtbl.Rows.Count; i++)
        {
            IIndentitem = new IIndentItemDetails();
            IIndentitem.SlNo = Convert.ToInt32(i + 1);
            IIndentitem.FK_Stock = Convert.ToInt64(dtbl.Rows[i]["ID_Stock"].ToString());
            IIndentitem.FK_Items = Convert.ToInt64(dtbl.Rows[i]["FK_Items"].ToString());
            IIndentitem.Expdate = Convert.ToDateTime(dtbl.Rows[i]["ExpiryDate"].ToString());
            IIndentitem.Batch = Convert.ToString(dtbl.Rows[i]["BatchNo"].ToString());
            IIndentitem.SalesPrice = Convert.ToDecimal(dtbl.Rows[i]["SalPrice"].ToString());
            IIndentitem.MRP = Convert.ToDecimal(dtbl.Rows[i]["MRP"].ToString());
            IIndentitem.RequestedQuantity = Convert.ToDecimal(dtbl.Rows[i]["RqueQuantity"].ToString());
            IIndentitem.CurrentStock = Convert.ToDecimal(dtbl.Rows[i]["CurrentStock"].ToString());
            IIndentitem.Quantity = Convert.ToDecimal(dtbl.Rows[i]["Qty"].ToString());
           // IIndentitem. = Convert.ToDecimal(dtbl.Rows[i]["Qty"].ToString());

            arlist.Add(IIndentitem);
            IIndentitem = null;

        }
        blIndent.xmlGriddata = Common.WriteToXml(arlist);
        if (hfID_Indent.Value == "")
        {
            if (hfIsAdd.Value.ToLower() == "true")
            {
                status = blIndent.InsertData();
                if (status > 0)
                {
                    ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "toastr.success('Data Inserted Successfully');alertify.alert('Reference Number:  " + status + "');", true);


                    ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "ClearAdd();", true);
                   // ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "ClearAdd();", true);
                    ClearAll();
                    Populate populate = new Populate();
                    ddlStore.SelectedValue = Session["mainstore"].ToString();
                    ddlStore.Enabled = false;
                }
                else
                {
                    ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "ShowAddDiv();", true);

                    ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "toastr.error('Cannot insert data')", true);

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

                blIndent.MasterId = Convert.ToInt64(hfID_Indent.Value);
                status = blIndent.UpdateData();
                if (status > 0)
                {

                    ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "toastr.success('Data Updated Successfully')", true);

                    ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "ClearAdd();", true);
                    //ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "ClearAdd();", true);
                    ClearAll();
                    Populate populate = new Populate();
                    ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "alert('Reference Number" + status + "')", true);

                }
                else if (status == -1)
                {

                    ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "toastr.error('Cannot update data.. ')", true);

                }
                else if (status == -2)
                {

                    ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "toastr.error('Cannot update data.. ')", true);

                }

            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "toastr.warning('User does not have permission to Update')", true);

            }
        }
       
    }
    protected void gvItem_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {

            string confirmValue = Request.Form["confirm_value"];
            if (confirmValue.Length > 3)
                confirmValue = confirmValue.Substring(confirmValue.Length - 3, 3);
            if (confirmValue == "Yes")
            {
                if (ViewState["GridData"] != null)
                {
                    DataTable dtbl = (DataTable)ViewState["GridData"];


                    dtbl.Rows.RemoveAt(e.RowIndex);
                    for (int i = 0; i < dtbl.Rows.Count; i++)
                    {
                        dtbl.Rows[i]["slno"] = i + 1;
                    }
                    gvItem.DataSource = dtbl;
                    gvItem.DataBind();
                    ViewState["GridData"] = dtbl;

                }
            }
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
            if (gvItem.Rows.Count > 0)
            {
                SaveData();
            }
            else
            {


                ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "alertify.alert('Atleast one Item Required')", true);
            }
           
        }
        catch (Exception ex)
        {
        }
    }
    protected void btnClear_Click(object sender, EventArgs e)
    {
        ClearAll();
        Clear();
        ViewState[GridData] = null;
        gvItem.DataSource = null;
        gvItem.DataBind();

        lblToStock.Visible = false;
        lblIndent.Visible = false;
        txtIndentStock.Visible = false;
        txtToStock.Visible = false;
        ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "ClearAdd();", true);
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        GridFill(1);
    }
    protected void gvIndent_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvIndent.PageIndex = e.NewPageIndex;
        GridFill(e.NewPageIndex);
    }
    protected void gvIndent_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        BlIndent blIndent = new BlIndent();
        ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "ShowAddDiv();", true);
        long MasterId = Convert.ToInt64(gvIndent.DataKeys[e.NewSelectedIndex]["ID_Indent"].ToString());
        blIndent.MasterId = MasterId;
        hfID_Indent.Value = MasterId.ToString();
        blIndent.FK_Client = Convert.ToInt32(hfClient.Value.Split(',')[0]);
        blIndent.FromDate = DateTime.Parse("1/1/1753");
        blIndent.Todate = DateTime.MaxValue;
        blIndent.SelectData();
        ddlStore.SelectedValue = Convert.ToInt64(blIndent.FK_Store).ToString();
        hfIndentUsercode.Value = Convert.ToString(blIndent.UserCode).ToString();
        Populate populate = new Populate();
        populate.FillDropdownList(ddlToStore, "Store", "StoName", "ID_Store", "StoName", " Cancelled=0 and Active='True' AND FK_Client= " + hfClient.Value.Split(',')[0] + "AND ID_Store!=" + ddlStore.SelectedValue, "");
        ddlToStore.SelectedValue = Convert.ToInt64(blIndent.ToStore).ToString();
        txtDate.Text = Convert.ToDateTime(blIndent.IndentDate).ToShortDateString();
        txtReqno.Text =Convert.ToString (blIndent.ReqID);
        divAdd.Style.Add("display", "block");
        divView.Style.Add("display", "none;");
        DataSet dtbl = blIndent.SelectdetailsGridData();
        DataTable dt = dtbl.Tables[0];
        if (dt.Rows.Count > 0)
        {
            if (Convert.ToString(dt.Rows[0]["Approved"]) == "True")
            {
                btnApproval.Visible = false;
                btnSubmit.Visible = false;
               // gvItem.Enabled = false;
                ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "toastr.warning('Indent Already Approved')", true);
            }
            else
            {
                btnApproval.Visible = true;
               // gvItem.Enabled = true;
            }
        }
        
       // btnApproval.Visible = true;
        gvItem.DataSource = dt;
        gvItem.DataBind();
        ViewState["GridData"] = dt;
        imgReqNo.Enabled = false;
        txtReqno.Enabled = false;
    }
    protected void CalculateTotals()
    {
        Decimal NetAmount ,NetTotal= 0;
        if (ViewState["GridData"] != null)
        {
            if (ViewState["GridData"] != "")
            {
                DataTable dtbl = (DataTable)ViewState["GridData"];
                if (dtbl.Rows.Count > 0)
                {

                    for (int i = 0; i < dtbl.Rows.Count; i++)
                    {
                        NetTotal = NetTotal + (Convert.ToDecimal(dtbl.Rows[i]["TotalAmount"].ToString()));

                    }

                  //  txtTotNetPur.Text = Math.Round(NetTotal, 4).ToString();
                   
                }
            }
            else
            {
              //  txtTotalAmount.Text = Math.Round(Convert.ToDecimal(0), 2).ToString();
               
                
            }
        }
        else
        {
           // txtTotalAmount.Text = Math.Round(Convert.ToDecimal(0), 2).ToString();
           
            
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
            BlIndent blIndent = new BlIndent();
            blIndent.UserCode = hfuserName.Value;
            blIndent.MasterId = Convert.ToInt64(hfID_Indent.Value);
            blIndent.FK_Reason = Convert.ToInt64(Reason);
            blIndent.FK_Client = Convert.ToInt32(hfClient.Value);
            if (hfIsDelete.Value.ToLower() == "true")
            {
                status = blIndent.DeleteIndent();
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
                txtDate.Focus();
            }
            blIndent = null;
        }
    }
    protected void gvIndent_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {

            string confirmValue = Request.Form["confirm_value"];
            if (confirmValue.Length > 3)
                confirmValue = confirmValue.Substring(confirmValue.Length - 3, 3);
            if (confirmValue == "Yes")
            {
                long status = 0;
                BlIndent blIndent = new BlIndent();
                blIndent.MasterId = Convert.ToInt64(gvIndent.DataKeys[e.RowIndex]["ID_Indent"].ToString());
                hfID_Indent.Value = (gvIndent.DataKeys[e.RowIndex]["ID_Indent"].ToString());
                blIndent.CancelledBy = hfuserName.Value;
                blIndent.FK_Client = Convert.ToInt64(hfClient.Value);
                blIndent.CancelledOn = DateTime.Now;
                blIndent = null;
            }
            else
            {
                mpDelete.Hide();
            }

        }
        catch (Exception ex)
        {
            //blMessage.Items.Add(ex.Message);
            ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "toastr.warning('" + ex.Message + "')", true);
        }

    }
  
    protected void ImageButton2_Click(object sender, ImageClickEventArgs e)
    {
        Delete();
    }
    protected void btnApproval_Click(object sender, EventArgs e)
    {

        if (hfuserName.Value == hfIndentUsercode.Value)
        {
            ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "toastr.error('The same user does not have permission to approve.')", true);
        }
        else
        {
            BlIndent blIndent = new BlIndent();
            blIndent.UserCode = hfuserName.Value;
            // hfID_Indent.Value = (gvIndent.DataKeys[e.RowIndex]["ID_Indent"].ToString());\
            if (hfID_Indent.Value != null)
                blIndent.MasterId = Convert.ToInt64(hfID_Indent.Value);
            blIndent.FK_Client = Convert.ToInt64(hfClient.Value);
            long status = 0;
            status = blIndent.IndentApproval();
            if (status > 0)
            {
                ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "toastr.success('Indent Approved successfully')", true);
                btnClear_Click(sender, e);
              
            }
        }
        
    }
    protected void gvIndent_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
              
                Button btnapprove = (Button)e.Row.FindControl("btnApprovedStatus");
                if (btnapprove.Text == "Approved")
                {
                    btnapprove.Enabled = false;
                    btnapprove.BackColor = System.Drawing.Color.DarkGreen;//true means enable else you may set false to disable button 
                }
                else
                {
                    btnapprove.BackColor = System.Drawing.Color.DarkRed;
                    btnapprove.Enabled = false;
                   
                }
                // For `Reject` button
                // Same condition but in `FindControl` method use `btnReject` id.
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "toastr.warning('" + ex.Message + "')", true);
        }
    }
    protected void btnStore_Click(object sender, EventArgs e)
    {
        
    }
    protected void ddlToStore_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (txtitem.Text != "")
        {
            BlStock blStock = new BlStock();
            blStock.UserCode = hfUser.Value;
            blStock.FK_Client = Convert.ToInt64(hfClient.Value);
            blStock.FK_Store = Convert.ToInt32(ddlStore.SelectedValue);
            blStock.Tostore = Convert.ToInt64(ddlToStore.SelectedValue);
            hfItemId.Value = blStock.FK_Items.ToString();
            blStock.SelectData();
           

            txtToStock.Text = blStock.Tostore.ToString();
            //txtCurrentstk.Text = blStock.CurrentStock.ToString();
            blStock = null;
        }
        ImageButton3.Focus();
    }
    protected void gvIndent_RowCommand(object sender, GridViewCommandEventArgs e)
    {

        try
        {
             BlIndent blIndent = new BlIndent();
            if (e.CommandName == "Print")
            {
                 GridViewRow row = (GridViewRow)((ImageButton)e.CommandSource).NamingContainer;
                int index = row.RowIndex;
                string indentID = gvIndent.DataKeys[index]["ID_Indent"].ToString();
                string popupreport = "<script> window.open('IndentprintReport.aspx?indentID=" + indentID + "', 'self', 'left=200,top=50,width=900,height=680,status=0,resizable=0,fullscreen=0,scrollbars=1');</script>";
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "Success", popupreport, false);
         

            }
           
            blIndent = null;


        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "toastr.warning('" + ex.Message + "')", true);
        }

    }
}