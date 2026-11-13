using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using WebPharma.Business;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Security.Cryptography;
using System.IO;
using System.Text;
using System.Web.UI;
/// <summary>
/// Summary description for Populate
/// </summary>
public class Populate
{
    public Populate()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    public void FillDropdownList(DropDownList ddList, string tableName, string textField,
                                string valueField, string sortField, string criteria,
                                string extraField)
    {
        ddList.Items.Clear();
        BlPopulate blpopulate = new BlPopulate();
        blpopulate.TableName = tableName;
        blpopulate.ListFields = textField;
        blpopulate.ValueFields = valueField;
        blpopulate.SortFields = sortField;
        blpopulate.Criteria = criteria;

        DataTable dt = new DataTable();
        dt = blpopulate.PopulateData();
        ddList.DataSource = dt;
        ddList.DataValueField = dt.Columns[1].Caption;
        ddList.DataTextField = dt.Columns[0].Caption;
        ddList.DataBind();
        if (extraField.Length <= 0)
            extraField = "select";
        ddList.Items.Insert(0, new ListItem(extraField, "0"));
        ddList.SelectedIndex = 0;
        blpopulate = null;

    }
    /*Mohd Dilshad for Fill DropDown in DashBoard*/
    public void FillDropdownListDashBoard(DropDownList ddList, string tableName, string textField,
                               string valueField, string sortField, string criteria,
                               string extraField)
    {
        ddList.Items.Clear();
        BlPopulate blpopulate = new BlPopulate();
        blpopulate.TableName = tableName;
        blpopulate.ListFields = textField;
        blpopulate.ValueFields = valueField;
        blpopulate.SortFields = sortField;
        blpopulate.Criteria = criteria;

        DataTable dt = new DataTable();
        dt = blpopulate.PopulateData();
        ddList.DataSource = dt;
        ddList.DataValueField = dt.Columns[1].Caption;
        ddList.DataTextField = dt.Columns[0].Caption;
        ddList.DataBind();
        if (extraField.Length <= 0)
            extraField = "All";
        ddList.Items.Insert(0, new ListItem(extraField, "0"));
        ddList.SelectedIndex = 0;
        blpopulate = null;

    }
    /*Mohd Dilshad for Common  Grid Filling Function NOTE:MasterId is Mandatory
     1.The Grid has Hide  First Three Field of Data Table Pls Foll
     */
    public void FillGrid(GridView gvGrid, string tableName, string Columns, string MasterId, string SortFields, string Criteria1, string Criteria2, int pageIndex, int PageSize, string GroupBy, Repeater rptPager, Page pge, int SortOrder = 0)
    {

        int i = 0;
        BlPopulate blpopulate = new BlPopulate();
        blpopulate.TableName = tableName;
        blpopulate.ListFields = Columns;
        blpopulate.ValueFields = MasterId;
        blpopulate.SortFields = SortFields;
        blpopulate.Criteria = Criteria1;
        blpopulate.Criteria2 = Criteria2;
        blpopulate.PageIndex = pageIndex;
        blpopulate.PageSize = PageSize;
        blpopulate.GroupBy = GroupBy;
        blpopulate.SortOrder = SortOrder;
        DataTable dtbl = new DataTable();
        try
        {
            dtbl = blpopulate.PopulateGrid();
        }
        catch (Exception ex)
        {
            i = 1;
            Label lbl = ((Label)rptPager.Parent.FindControl("lblErrorMsg"));
            lbl.Visible = true;
            return;
        }

        if (dtbl.Rows.Count > 0)
        {
            if (dtbl.Rows.Count == 1 && pageIndex == 1)
            {
                if (pge.Form.Name == "frmSalesReturn" && tableName == "Sales")
                {

                    pge.GetType().InvokeMember("SaleDetail", System.Reflection.BindingFlags.InvokeMethod, null, pge, new object[] { dtbl.Rows[0]["Name"].ToString(), dtbl.Rows[0]["ID"].ToString(), (bool)(dtbl.Rows[0]["Credit"]) });

                }
                else
                {
                    pge.GetType().InvokeMember("SetDetails", System.Reflection.BindingFlags.InvokeMethod, null, pge, new object[] { dtbl.Rows[0]["Name"].ToString(), dtbl.Rows[0]["ID"].ToString() });
                }

            }
            else
            {

                gvGrid.DataSource = dtbl;
                gvGrid.DataBind();

                //if (dtbl.Rows.Count > 0)
                //{
                //    if (pge.Form.Name == "frmSalesReturn")
                //    {
                //        //int recordCount = Convert.ToInt32(dtbl.Rows[0]["SLNO"].ToString());
                //        //rptPager.DataSource = this.PopulatePager(recordCount, pageIndex);
                //        //rptPager.DataBind();
                //    }
                //    else
                //    {
                //        int recordCount = Convert.ToInt32(dtbl.Rows[0]["RecordCount"].ToString());
                //        rptPager.DataSource = this.PopulatePager(recordCount, pageIndex);
                //        rptPager.DataBind();
                //    }
                //}

            }
        }
        else
        {
            gvGrid.DataSource = null;
            gvGrid.DataBind();
        }
        //else
        //{

        //}



        blpopulate = null;
        // return i;
    }
    public void FillPopUpGrid(GridView gvGrid, string tableName, string Columns, string MasterId, string SortFields, string Criteria1, string Criteria2, string GroupBy)
    {

        int i = 0;
        BlPopulate blpopulate = new BlPopulate();
        blpopulate.TableName = tableName;
        blpopulate.ListFields = Columns;
        blpopulate.ValueFields = MasterId;
        blpopulate.SortFields = SortFields;
        blpopulate.Criteria = Criteria1;
        blpopulate.Criteria2 = Criteria2;
        blpopulate.GroupBy = GroupBy;
        
        DataTable dtbl = new DataTable();
        try
        {
            dtbl = blpopulate.PopulatePopupGrid();
        }
        catch (Exception ex)
        {
            
        }

        if (dtbl.Rows.Count > 0)
        {
         
                gvGrid.DataSource = dtbl;
                gvGrid.DataBind();

               

            
        }
        else
        {
            gvGrid.DataSource = null;
            gvGrid.DataBind();
        }


        blpopulate = null;
        
    }
    public void FillGrid(GridView gvGrid, string tableName, string Columns, string MasterId, string Criteria1, int pageIndex, int PageSize, string GroupBy, Repeater rptPager, Page pge, int SortOrder=0)
    {

        int i = 0;
        BlPopulate blpopulate = new BlPopulate();
        blpopulate.TableName = tableName;
        blpopulate.ListFields = Columns;
        blpopulate.ValueFields = MasterId;
        blpopulate.SortFields = "";
        blpopulate.Criteria = Criteria1;
        blpopulate.Criteria2 = string.Empty;
        blpopulate.PageIndex = pageIndex;
        blpopulate.PageSize = PageSize;
        blpopulate.GroupBy = GroupBy;
        blpopulate.SortOrder = SortOrder;
        DataTable dtbl = new DataTable();



        try
        {
            dtbl = blpopulate.PopulateGrid();
        }
        catch (Exception ex)
        {
            i = 1;
            Label lbl = ((Label)rptPager.Parent.FindControl("lblErrorMsg"));
            lbl.Visible = true;
            return;
        }

        if (dtbl.Rows.Count > 0)
        {
            if (dtbl.Rows.Count == 1 && pageIndex == 1)
            {
                if (pge.Form.Name == "frmSalesReturn" && tableName == "Sales")
                {

                    pge.GetType().InvokeMember("SaleDetail", System.Reflection.BindingFlags.InvokeMethod, null, pge, new object[] { dtbl.Rows[0]["Name"].ToString(), dtbl.Rows[0]["ID"].ToString(), (bool)(dtbl.Rows[0]["Credit"]) });

                }
                else
                {
                    pge.GetType().InvokeMember("SetDetails", System.Reflection.BindingFlags.InvokeMethod, null, pge, new object[] { dtbl.Rows[0]["Name"].ToString(), dtbl.Rows[0]["ID"].ToString() });
                }

            }
            else
            {

                gvGrid.DataSource = dtbl;
                gvGrid.DataBind();

                if (dtbl.Rows.Count > 0)
                {

                    //int recordCount = Convert.ToInt32(dtbl.Rows[0]["RecordCount"].ToString());
                    //rptPager.DataSource = this.PopulatePager(recordCount, pageIndex);
                    //rptPager.DataBind();
                }

            }
        }
        else
        {
            gvGrid.DataSource = null;
            gvGrid.DataBind();
        }
        //else
        //{

        //}



        blpopulate = null;
        // return i;
    }

    //public void FillGrid(GridView gvGrid, string tableName, string Columns, string MasterId, string Criteria1, int pageIndex, int PageSize, string GroupBy, Repeater rptPager, Page pge)
    //{
    //    BlPopulate blpopulate = new BlPopulate();
    //    blpopulate.TableName = tableName;
    //    blpopulate.ListFields = Columns;
    //    blpopulate.ValueFields = MasterId;
    //    blpopulate.SortFields = "";
    //    blpopulate.Criteria = Criteria1;
    //    blpopulate.Criteria2 = "";
    //    blpopulate.PageIndex = pageIndex;
    //    blpopulate.PageSize = PageSize;
    //    blpopulate.GroupBy = GroupBy;
    //    DataTable dtbl = blpopulate.PopulateGrid();

    //    if (dtbl.Rows.Count > 0)
    //    {
    //        if (dtbl.Rows.Count == 1 && pageIndex == 1)
    //        {
    //            if (pge.Form.Name == "frmSalesReturn" && tableName == "Sales")
    //            {

    //                pge.GetType().InvokeMember("SaleDetail", System.Reflection.BindingFlags.InvokeMethod, null, pge, new object[] { dtbl.Rows[0]["Name"].ToString(), dtbl.Rows[0]["ID"].ToString(), (bool)(dtbl.Rows[0]["Credit"]) });

    //            }
    //            else
    //            {
    //                pge.GetType().InvokeMember("SetDetails", System.Reflection.BindingFlags.InvokeMethod, null, pge, new object[] { dtbl.Rows[0]["Name"].ToString(), dtbl.Rows[0]["ID"].ToString() });
    //            }

    //        }
    //        else
    //        {

    //            gvGrid.DataSource = dtbl;
    //            gvGrid.DataBind();
    //            if (dtbl.Rows.Count > 0)
    //            {

    //                int recordCount = Convert.ToInt32(dtbl.Rows[0]["RecordCount"].ToString());
    //                rptPager.DataSource = this.PopulatePager(recordCount, pageIndex);
    //                rptPager.DataBind();
    //            }

    //        }
    //    }
    //    else
    //    {
    //        gvGrid.DataSource = null;
    //        gvGrid.DataBind();
    //    }
    //    //else
    //    //{

    //    //}



    //    blpopulate = null;

    //}

    private List<ListItem> PopulatePager(int recordCount, int currentPage)
    {
        //double dblPageCount = (double)((decimal)recordCount / 10);
        //int pageCount = (int)Math.Ceiling(dblPageCount);
        //List<ListItem> pages = new List<ListItem>();
        //int i;
        //if (currentPage <= 10)
        //{
        //    i = 1;
        //}
        //else
        //{
        //    i = currentPage;
        //}
        //if (pageCount > 0)
        //{
        //    pages.Add(new ListItem("First", "1", currentPage > 1));
        //    pages.Add(new ListItem("<-", (i - 10).ToString(), i > 1));
        //    while (i <= pageCount)
        //    {
        //        pages.Add(new ListItem(i.ToString(), i.ToString(), i != currentPage));
        //        if (i % 10 == 0)
        //        {
        //            break;
        //        }
        //        i++;
        //    }
        //    pages.Add(new ListItem("->", (i + 1).ToString(), pageCount > 10));
        //    pages.Add(new ListItem("Last", pageCount.ToString(), currentPage < pageCount));
        //}
        //return pages;


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
            int j = i % 10;
            if (j == 0)
            { i = i - 9; }
            else
            { i = i - j + 1; }
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
            
            pages.Add(new ListItem("->", (i + 1).ToString(), (pageCount - currentPage) > 10));
            pages.Add(new ListItem("Last", pageCount.ToString(), currentPage < pageCount));
        }
        return pages;


    }

    public void FillDropdownListAdditionFeild(DropDownList ddList, string tableName, string textField,
                               string valueField, string sortField, string criteria,
                               string extraField, string extraValue, string extraText)
    {
        ddList.Items.Clear();
        BlPopulate blpopulate = new BlPopulate();
        blpopulate.TableName = tableName;
        blpopulate.ListFields = textField;
        blpopulate.ValueFields = valueField;
        blpopulate.SortFields = sortField;
        blpopulate.Criteria = criteria;

        DataTable dt = new DataTable();
        dt = blpopulate.PopulateData();
        ddList.DataSource = dt;
        ddList.DataValueField = dt.Columns[1].Caption;
        ddList.DataTextField = dt.Columns[0].Caption;
        ddList.DataBind();
        if (extraField.Length <= 0)
            extraField = "select";
        ddList.Items.Insert(0, new ListItem(extraField, "select"));
        ddList.Items.Insert(1, new ListItem(extraText, extraValue));

        ddList.SelectedIndex = 0;
        blpopulate = null;

    }

    public void FillDropdownListAll(DropDownList ddList, string tableName, string textField,
                               string valueField, string sortField, string criteria,
                               string extraField)
    {
        ddList.Items.Clear();
        BlPopulate blpopulate = new BlPopulate();
        blpopulate.TableName = tableName;
        blpopulate.ListFields = textField;
        blpopulate.ValueFields = valueField;
        blpopulate.SortFields = sortField;
        blpopulate.Criteria = criteria;

        DataTable dt = new DataTable();
        dt = blpopulate.PopulateData();
        ddList.DataSource = dt;
        ddList.DataValueField = dt.Columns[1].Caption;
        ddList.DataTextField = dt.Columns[0].Caption;
        ddList.DataBind();
        if (extraField.Length <= 0)
            extraField = "All";
        ddList.Items.Insert(0, new ListItem(extraField, "0"));
        ddList.SelectedIndex = 0;
        blpopulate = null;

    }


    public void FillDropdownListSelect(DropDownList ddList, string tableName, string textField,
                              string valueField, string sortField, string criteria,
                              string extraField)
    {
        ddList.Items.Clear();
        BlPopulate blpopulate = new BlPopulate();
        blpopulate.TableName = tableName;
        blpopulate.ListFields = textField;
        blpopulate.ValueFields = valueField;
        blpopulate.SortFields = sortField;
        blpopulate.Criteria = criteria;

        DataTable dt = new DataTable();
        dt = blpopulate.PopulateData();
        ddList.DataSource = dt;
        ddList.DataValueField = dt.Columns[1].Caption;
        ddList.DataTextField = dt.Columns[0].Caption;
        ddList.DataBind();
        if (extraField.Length <= 0)
            extraField = "Select";
        ddList.Items.Insert(0, new ListItem(extraField, "0"));
        ddList.SelectedIndex = 0;
        blpopulate = null;

    }


    public void FillGoodsIssueMode(DropDownList ddList)
    {
        ddList.Items.Clear();
        ddList.Items.Add(new ListItem("select", "0"));
        ddList.Items.Add(new ListItem("To Store", "1"));
        //ddList.Items.Add(new ListItem("To VendorReturn", "2"));
        ddList.Items.Add(new ListItem("IssueOut", "3"));
        ddList.Items.Add(new ListItem("Wastage", "4"));
        ddList.Items.Add(new ListItem("Damage", "5"));
        ddList.Items.Add(new ListItem("Missing", "6"));
        ddList.SelectedIndex = 0;
    }
    public void FillEmirate(DropDownList ddlEmirates)
    {
        ddlEmirates.Items.Clear();
        ddlEmirates.Items.Add(new ListItem("select", "0"));
        ddlEmirates.Items.Add(new ListItem("Abu Dhabi", "1"));
        ddlEmirates.Items.Add(new ListItem("Ajman", "2"));
        ddlEmirates.Items.Add(new ListItem("Dubai", "3"));
        ddlEmirates.Items.Add(new ListItem("Fujaira", "4"));
        ddlEmirates.Items.Add(new ListItem("Ras Al-Khaimah", "5"));
        ddlEmirates.Items.Add(new ListItem("Sharjah ", "6"));
        ddlEmirates.Items.Add(new ListItem("Umm Al-Quwain ", "7"));
        ddlEmirates.SelectedIndex = 0;
    }
    public void FillGiftvoucher(DropDownList ddList)
    {
        ddList.Items.Clear();
        ddList.Items.Add(new ListItem("select", "0"));
        ddList.Items.Add(new ListItem("Gift", "1"));
        ddList.Items.Add(new ListItem("Discount", "2"));
        ddList.Items.Add(new ListItem("Return", "3"));
        ddList.SelectedIndex = 0;
    }
    public void FillItemType(DropDownList ddList)
    {
        ddList.Items.Clear();
        ddList.Items.Add(new ListItem("select", "0"));
        ddList.Items.Add(new ListItem("Service", "1"));
        ddList.Items.Add(new ListItem("WebPharma", "2"));
        ddList.SelectedIndex = 0;
    }
    public void FillBillType(DropDownList ddList)
    {
        ddList.Items.Clear();
        ddList.Items.Add(new ListItem("select", "0"));
        ddList.Items.Add(new ListItem("Sale", "1"));
        ddList.Items.Add(new ListItem("Purchase", "2"));
        ddList.SelectedIndex = 0;
    }
    public void FillPurchasePaymentMode(DropDownList ddList)
    {
        ddList.Items.Clear();

        ddList.Items.Add(new ListItem("Cash", "1"));
        ddList.Items.Add(new ListItem("Bank", "2"));
        ddList.Items.Add(new ListItem("Others", "3"));
        ddList.SelectedIndex = 0;
    }
    public void FillRounding(DropDownList ddList)
    {
        ddList.Items.Clear();
        ddList.Items.Add(new ListItem("RoundAbsolute", "0"));
        ddList.Items.Add(new ListItem("Round.500", "1"));
        ddList.Items.Add(new ListItem("Round.100", "2"));
        ddList.SelectedIndex = 0;
    }
    public void FillCostingMethod(DropDownList ddList)
    {
        ddList.Items.Clear();
        ddList.Items.Add(new ListItem("select", "0"));

        ddList.Items.Add(new ListItem("Batch wise", "1"));
        ddList.Items.Add(new ListItem("FIFO", "2"));
        ddList.Items.Add(new ListItem("LIFO", "3"));
        ddList.Items.Add(new ListItem("Weighted Average", "4"));
        ddList.Items.Add(new ListItem("Last Purchase Price", "5"));
        ddList.SelectedIndex = 0;
    }
    //Trans Type
    public void FillTransType(DropDownList ddList)
    {
        ddList.Items.Clear();
        ddList.Items.Add(new ListItem("select", "0"));
        ddList.Items.Add(new ListItem("Reciept", "1"));
        ddList.Items.Add(new ListItem("Payment", "2"));
        ddList.SelectedIndex = 0;
    }
    public void FillEmpty(DropDownList ddList)
    {
        ddList.Items.Clear();
        ddList.Items.Add(new ListItem("select", "0"));
        ddList.SelectedIndex = 0;
    }

    //TransType for Report
    public void FillTransTypeAll(DropDownList ddList)
    {
        ddList.Items.Clear();
        ddList.Items.Add(new ListItem("ALL", " "));
        ddList.Items.Add(new ListItem("Credit", "R"));
        ddList.Items.Add(new ListItem("Debit", "P"));
        ddList.SelectedIndex = 0;
    }
    //Credit Account Type
    public void FillCreditAccountAll(DropDownList ddList, Boolean Customer)
    {
        ddList.Items.Clear();
       ddList.Items.Add(new ListItem("select", "0"));
        // ddList.Items.Add(new ListItem("Invoice", "1"));
        if (Customer == true)
        {
            ddList.Items.Add(new ListItem("Customer", "1"));
        }
        else
        {
            ddList.Items.Add(new ListItem("Supplier", "2"));
        }

       // ddList.SelectedIndex = 0;
    }
    public static void InsertMsg(Page pge)
    {
        try
        {

            ScriptManager.RegisterClientScriptBlock(pge, pge.GetType(), "Script", "ShowAddDiv();", true);

            ScriptManager.RegisterStartupScript(pge, pge.GetType(), "Script", "toastr.success('Data Inserted Successfully');ClearAdd();", true);
        }
        catch
        {
        }
    }

    public static void UpdateMsg(Page pge)
    {
        try
        {



            ScriptManager.RegisterStartupScript(pge, pge.GetType(), "Script", "toastr.success('Data Updated Successfully');ClearAdd();", true);
        }
        catch
        {
        }
    }
    public static void DeleteMsg(Page pge)
    {
        try
        {



            ScriptManager.RegisterClientScriptBlock(pge, pge.GetType(), "Script", "toastr.success('Data Deleted Successfully')", true);
        }
        catch
        {
        }
    }
    public static void ReferanceMsg(Page pge)
    {
        try
        {




            ScriptManager.RegisterClientScriptBlock(pge, pge.GetType(), "Script", "toastr.error('Cannot delete data, reference exists')", true);
        }
        catch
        {
        }
    }

    public static void MessageBox(Page pge, string strMsg)
    {
        try
        {



            ScriptManager.RegisterStartupScript(pge, pge.GetType(), "Script", "toastr.success('" + strMsg + "');ClearAdd();", true);
        }
        catch
        {
        }
    }

    public static void ErrorMessageBox(Page pge, string strMsg)
    {
        try
        {
            ScriptManager.RegisterClientScriptBlock(pge, pge.GetType(), "Script", "ShowAddDiv();", true);


            ScriptManager.RegisterStartupScript(pge, pge.GetType(), "Script", "toastr.error('" + strMsg + "')", true);
        }
        catch
        {
        }
    }
    public static void WarningMsg(Page pge, string Message)
    {
        ScriptManager.RegisterClientScriptBlock(pge, pge.GetType(), "Script", "toastr.warning('" + Message + "'); ", true);
    }

    public string Decrypt(string cipherText)
    {
        string EncryptionKey = "P321PLATINUM123CLT";
        cipherText = cipherText.Replace(" ", "+");
        byte[] cipherBytes = Convert.FromBase64String(cipherText);
        using (Aes encryptor = Aes.Create())
        {
            Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
            encryptor.Key = pdb.GetBytes(32);
            encryptor.IV = pdb.GetBytes(16);
            using (MemoryStream ms = new MemoryStream())
            {
                using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateDecryptor(), CryptoStreamMode.Write))
                {
                    cs.Write(cipherBytes, 0, cipherBytes.Length);
                    cs.Close();
                }
                cipherText = Encoding.Unicode.GetString(ms.ToArray());
            }
        }
        return cipherText;
    }
    public void FillQuantitySearch(DropDownList ddList)
    {
        ddList.Items.Clear();
        ddList.Items.Add(new ListItem("select", "0"));
        ddList.Items.Add(new ListItem(">=", "1"));
        ddList.Items.Add(new ListItem("<=", "2"));

        ddList.SelectedIndex = 0;
    }
    public void FillPeriodType(DropDownList ddList)
    {
        ddList.Items.Clear();
        ddList.Items.Add(new ListItem("Select", "0"));
        ddList.Items.Add(new ListItem("Days", "1"));
        ddList.Items.Add(new ListItem("Months", "2"));
        ddList.SelectedIndex = 0;
    }
    public void FillPaymentMode(DropDownList ddList)
    {
        ddList.Items.Clear();
        ddList.Items.Add(new ListItem("Select", "0"));
        ddList.Items.Add(new ListItem("Cash", "1"));
        ddList.Items.Add(new ListItem("Adjustment", "2"));
        ddList.SelectedIndex = 0;
    }

    public void FillSalesReturnGrid(GridView gvGrid, string tableName, string Columns, string MasterId, string SortFields, string Criteria1, string Criteria2, int pageIndex, int PageSize, string GroupBy, Repeater rptPager, Page pge, int SortOrder = 0)
    {

        int i = 0;
        BlPopulate blpopulate = new BlPopulate();
        blpopulate.TableName = tableName;
        blpopulate.ListFields = Columns;
        blpopulate.ValueFields = MasterId;
        blpopulate.SortFields = SortFields;
        blpopulate.Criteria = Criteria1;
        blpopulate.Criteria2 = Criteria2;
        blpopulate.PageIndex = pageIndex;
        blpopulate.PageSize = PageSize;
        blpopulate.GroupBy = GroupBy;
        blpopulate.SortOrder = SortOrder;
        DataTable dtbl = new DataTable();
        try
        {
            dtbl = blpopulate.PopulateSalesReturnGrid();
        }
        catch (Exception ex)
        {
            i = 1;
            Label lbl = ((Label)rptPager.Parent.FindControl("lblErrorMsg"));
            lbl.Visible = true;
            return;
        }

        if (dtbl.Rows.Count > 0)
        {
            if (dtbl.Rows.Count == 1 && pageIndex == 1)
            {
                if (pge.Form.Name == "frmSalesReturn" && tableName == "Sales")
                {

                    pge.GetType().InvokeMember("SaleDetail", System.Reflection.BindingFlags.InvokeMethod, null, pge, new object[] { dtbl.Rows[0]["Name"].ToString(), dtbl.Rows[0]["ID"].ToString(), (bool)(dtbl.Rows[0]["Credit"]) });

                }
                else
                {
                    pge.GetType().InvokeMember("SetDetails", System.Reflection.BindingFlags.InvokeMethod, null, pge, new object[] { dtbl.Rows[0]["Name"].ToString(), dtbl.Rows[0]["ID"].ToString() });
                }

            }
            else
            {

                gvGrid.DataSource = dtbl;
                gvGrid.DataBind();

                //if (dtbl.Rows.Count > 0)
                //{
                //    if (pge.Form.Name == "frmSalesReturn")
                //    {
                //        //int recordCount = Convert.ToInt32(dtbl.Rows[0]["SLNO"].ToString());
                //        //rptPager.DataSource = this.PopulatePager(recordCount, pageIndex);
                //        //rptPager.DataBind();
                //    }
                //    else
                //    {
                //        int recordCount = Convert.ToInt32(dtbl.Rows[0]["RecordCount"].ToString());
                //        rptPager.DataSource = this.PopulatePager(recordCount, pageIndex);
                //        rptPager.DataBind();
                //    }
                //}

            }
        }
        else
        {
            gvGrid.DataSource = null;
            gvGrid.DataBind();
        }
        //else
        //{

        //}



        blpopulate = null;
        // return i;
    }

}