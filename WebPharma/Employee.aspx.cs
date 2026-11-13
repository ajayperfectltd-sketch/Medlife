/*----------------------------------------------------------------------------------------------------------------------------------------------------------
Purpose    : Insert/Update/Delete Employee  
Author     : bibin TC
Copyright  : Perfect Software Solutions (Clt) Pvt Ltd
Created on : 03/05/2018
----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
WebPharma
On			By			  OMID
----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebPharma.Business.Masters;
using WebPharma.Business;
using System.Data;
using System.IO;
using System.Text;
using System.Security.Cryptography;
using System.Collections;
using System.Threading;
using System.Globalization;


public partial class Employee : System.Web.UI.Page
{
    #region Functions
    //private void FillStore()
    //{
    //    BlEmployee blEmployee = new BlEmployee();
    //    chklistStores.DataSource = null;
    //    chklistStores.DataValueField = "ID_Store";
    //    chklistStores.DataTextField = "Name";
    //    chklistStores.DataSource = blEmployee.SelectStores();
    //    chklistStores.DataBind();

    //    Populate populate = new Populate();
    //    populate.FillDropdownList(ddlDefaultStore, "Store", "ShortName+'-'+Name", "ID_Store", "ID_Store", " Active='True' AND  FK_Client= " + hfClient.Value, "");
    //    populate = null;

    //}

    protected void BindData()
    {
        Populate populate = new Populate();
      
        //populate.FillDropdownList(ddlSalesMan, "SalesMan", "ShortName+' - '+Name", "ID_SalesMan", "ID_SalesMan", "", "");
        populate = null;
        txtDob.Text = DateTime.Now.ToShortDateString();
    }
    protected void SetControls()
    {
        txtDob.Attributes.Add("readOnly", "readOnly");
    }
    protected void GridFill(int currentPage)
    {
        BlEmployee blEmployee = new BlEmployee();
        DataTable dtbl = new DataTable();
        blEmployee.EmployeeCode = txtSearchEmployeeCode.Text.Trim();
        blEmployee.Name = txtSearchName.Text.Trim();
        blEmployee.PageIndex = Convert.ToInt16(currentPage);
        blEmployee.FK_Client = Convert.ToInt64(hfClient.Value);
        blEmployee.UserCode = hfUser.Value;
        dtbl = blEmployee.SelectAllData();
        gvEmployee.DataSource = dtbl;
        gvEmployee.DataBind();
        if (dtbl.Rows.Count > 0)
        {
            int recordCount = Convert.ToInt32(dtbl.Rows[0]["RecordCount"].ToString());
            this.PopulatePager(recordCount, currentPage);
        }
        blEmployee = null;
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

    //private ArrayList UpdateStoreAccess()
    //{
    //    StoreAccessDetails StoreAccessDetails;
    //    ArrayList arlist = new ArrayList();
    //    for (int j = 0; j < chklistStores.Items.Count; j++)
    //    {
    //        if (chklistStores.Items[j].Selected == true && chklistStores.Items[j].Value != ddlDefaultStore.SelectedValue.ToString())
    //        {
    //            StoreAccessDetails = new StoreAccessDetails();
    //            StoreAccessDetails.FK_Store = Convert.ToInt64(chklistStores.Items[j].Value);
    //            StoreAccessDetails.IsDefault = false;
    //            string name = chklistStores.Items[j].Text;
    //            arlist.Add(StoreAccessDetails);
    //        }
    //    }
    //    StoreAccessDetails = new StoreAccessDetails();
    //    StoreAccessDetails.FK_Store = Convert.ToInt64(ddlDefaultStore.SelectedValue.ToString());
    //    StoreAccessDetails.IsDefault = true;
    //    arlist.Add(StoreAccessDetails);
    //    return arlist;
    //}
    protected void CheckUserAccess()
    {
        BlUserAccess bluseraccess = new BlUserAccess();
        Populate populate = new Populate();

        bluseraccess.FK_Users = Convert.ToInt64(hfUser.Value);
        bluseraccess.FK_Pages = 5;
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
        BlEmployee blEmployee = new BlEmployee();
        blEmployee.EmployeeCode = txtEmployeeCode.Text;
        blEmployee.Name = txtName.Text;
        blEmployee.Address = txtAddress.Text;
        blEmployee.Phone = txtPhone.Text;
        blEmployee.Email = txtEmail.Text;
        blEmployee.FK_Client = Convert.ToInt64(hfClient.Value);
        blEmployee.UserCode = hfUser.Value;
        blEmployee.Dob = DateTime.ParseExact(txtDob.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
        blEmployee.Salesman = Convert.ToBoolean(ChkSalesMan.Checked);
        blEmployee.Active = Convert.ToBoolean(chkActive.Checked);
        blEmployee.EnteredOn = Convert.ToDateTime(DateTime.Now);
       // blEmployee.XmlStoreAccessDetails = Common.WriteToXml(UpdateStoreAccess());

        if (hfEmployeeId.Value == "0")
        {
            if (hfIsAdd.Value.ToLower() == "true")
            {
                status = blEmployee.InsertData();
                if (status > 0)
                {
                    ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "ShowAddDiv();", true);
                    ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "toastr.success('Data inserted successfully');ClearAdd();", true);

                    GridFill(1);
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "ShowAddDiv();", true);
                    ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "toastr.error('Cannot insert data, Employee code already exists')", true);
                    txtEmployeeCode.Focus();
                }
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "toastr.warning('User does not have permission to add')", true);
                txtEmployeeCode.Focus();

            }
        }
        else
        {
            if (hfIsUpdate.Value.ToLower() == "true")
            {

                blEmployee.MasterId = Convert.ToInt64(hfEmployeeId.Value);
                status = blEmployee.UpdateData();
                if (status > 0)
                {

                    ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "ShowAddDiv();", true);
                    ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "toastr.success('Data updated successfully');ClearAdd();", true);
                    GridFill(1);
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "ShowAddDiv();", true);
                    ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "toastr.error('Cannot update data, Employee code already exists')", true);
                    txtEmployeeCode.Focus();
                }
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "toastr.warning('User does not have permission to Update')", true);
            }
        }
        blEmployee = null;
    }
    #endregion

    #region Events
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                Page.Header.DataBind();
                //blMessage.Items.Clear();
                hfUser.Value = Request.QueryString["uid"];
                hfClient.Value = Request.QueryString["clnt"];
                ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "ClearAdd();", true);
                divAdd.Style.Add("display", "block");
                CheckUserAccess();
                BindData();
                GridFill(1);
                //FillStore();
                SetControls(); 
                txtEmployeeCode.Focus();           
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
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        try
        {
            //blMessage.Items.Clear();
            GridFill(1); 
            txtSearchEmployeeCode.Focus();
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "toastr.warning('" + ex.Message + "')", true); 
        }
    }
    protected void gvEmployee_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        try
        {
            //blMessage.Items.Clear();
            ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "ShowAddDiv();", true);
            BlEmployee blEmployee = new BlEmployee();
            BlFunction blFunction = new BlFunction();
            blEmployee.MasterId = Convert.ToInt64(gvEmployee.DataKeys[e.NewSelectedIndex]["ID_Employee"].ToString());
            hfEmployeeId.Value = blEmployee.MasterId.ToString();
            blEmployee.FK_Client = Convert.ToInt64(hfClient.Value);
            blEmployee.UserCode = hfUser.Value;
            blEmployee.SelectData();
            txtEmployeeCode.Text = blEmployee.EmployeeCode.ToString();
            txtName.Text = blEmployee.Name.ToString();
            txtAddress.Text = blEmployee.Address;
            txtPhone.Text = blEmployee.Phone;
            txtEmail.Text = blEmployee.Email;
           
            txtDob.Text = blEmployee.Dob.ToString("dd/MM/yyyy");
           // ddlSalesMan.SelectedValue = blEmployee.FK_Salesman.ToString();
            ChkSalesMan.Checked = blEmployee.Salesman;
          
            chkActive.Checked = blEmployee.Active;


           // DataTable dt = blFunction.GetDataTable("StoreAccess", "FK_Store,IsDefault", "FK_Employee=" + hfEmployeeId.Value.ToString() + " AND FK_Client=1", " ", " ");
            //for (int i = 0; i < dt.Rows.Count; i++)
            //{
            //    for (int j = 0; j < chklistStores.Items.Count; j++)
            //    {

            //        if (chklistStores.Items[j].Value == dt.Rows[i]["FK_Store"].ToString())
            //        {
            //            chklistStores.Items[j].Selected = true;
            //        }

            //    }
            //    if (Convert.ToBoolean(dt.Rows[i]["IsDefault"]).ToString() == "True")
            //    {
            //        ddlDefaultStore.SelectedValue = dt.Rows[i]["FK_Store"].ToString();
            //    }
            //}
            blEmployee = null;
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "toastr.warning('" + ex.Message + "')", true); 
        }
    }
    protected void gvEmployee_RowDeleting(object sender, GridViewDeleteEventArgs e)
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
                BlEmployee blEmployee = new BlEmployee();
                blEmployee.MasterId = Convert.ToInt64(gvEmployee.DataKeys[e.RowIndex]["ID_Employee"].ToString());
                blEmployee.FK_Client = Convert.ToInt64(hfClient.Value);
                blEmployee.UserCode = hfUser.Value;
                if (hfIsDelete.Value.ToLower() == "true")
                {
                    status = blEmployee.DeleteData();
                    if (status > 0)
                    {
                        GridFill(1);
                        ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "toastr.success('Deleted successfully')", true);
                    }
                    else
                    {
                        ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "toastr.error('Cannot delete, reference exists')", true);

                    }
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "toastr.warning('User does not have permission to Delete')", true);
                  
                }
                blEmployee = null;
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
           // blMessage.Items.Clear();
            SaveData();
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "toastr.warning('" + ex.Message + "')", true); 
        }
    }
    #endregion          
}