/*----------------------------------------------------------------------------------------------------------------------------------------------------------
Purpose    : Insert/Update/Delete Items  
Author     : Aiswarya
Copyright  : Perfect Software Solutions (Clt) Pvt Ltd
Created on : 28/05/2018
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
using System.Data;
using WebPharma.Business.Masters;
using System.IO;
using System.Text;
using System.Security.Cryptography;
using Excel;
using System.Collections;

public partial class Items : System.Web.UI.Page
{
    private const string Image = "Image";
    private const string TempPath = "~\\UploadedItems\\Temp\\";
    private const string UploadedPath = "~\\UploadedItems\\";
    private const string DefaultImage = "~/images/DefaultImage.jpg";
    private const string Excel = "Excel";
    public static DataSet dsExcel;

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
            BlItems blItem = new BlItems();
            blItem.UserCode = hfuserName.Value;
            blItem.MasterId = Convert.ToInt64(hfMasterID.Value);
            blItem.FK_Reason = Convert.ToInt64(Reason);
            if (hfIsDelete.Value.ToLower() == "true")
            {
                status = blItem.DeleteData();
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
                txtSearchName.Focus();
            }
            blItem = null;
        }
    }
    private void UploadImages()
    {
        if (hdfImage.Value != "")
        {
            FileInfo infoFile = new FileInfo(Server.MapPath(TempPath + hdfImage.Value));
            if (infoFile.Exists)
            {
                infoFile.MoveTo(Server.MapPath(UploadedPath + hdfImage.Value));

                DirectoryInfo info = new DirectoryInfo(Server.MapPath("~\\UploadedItems\\Temp"));
                foreach (FileInfo files in info.GetFiles())
                {
                    try
                    {
                        files.Delete();
                    }
                    catch (Exception ex)
                    {

                    }
                }
            }
        }
    }




    private bool TryParseDecimalField(TextBox txt, string fieldLabel, out decimal value, out string error)
    {
        error = string.Empty;
        string text = (txt.Text ?? "").Trim();
        if (text == "")
        {
            value = 0;
            return true; // empty is allowed, defaults to 0
        }
        if (!decimal.TryParse(text, out value) || value < 0)
        {
            error = fieldLabel + " must be a valid non-negative number";
            txt.Focus();
            return false;
        }
        return true;
    }

    private bool ValidateItemLevels(out string errorMessage)
    {
        errorMessage = string.Empty;
        decimal minLevel, maxLevel, roLevel, roQty, dangerLevel;
        int returnDays, expiryValidation;

        if (!TryParseDecimalField(txtMinLevel, "Minimum Level", out minLevel, out errorMessage)) return false;
        if (!TryParseDecimalField(txtMaximumLevel, "Maximum Level", out maxLevel, out errorMessage)) return false;
        if (!TryParseDecimalField(txtReorderlevel, "Reorder Level", out roLevel, out errorMessage)) return false;
        if (!TryParseDecimalField(txtReoqty, "Reorder Quantity", out roQty, out errorMessage)) return false;
        if (!TryParseDecimalField(txtDangerlevel, "Danger Level", out dangerLevel, out errorMessage)) return false;

        if (!int.TryParse(string.IsNullOrWhiteSpace(txtReturnBeforDays.Text) ? "0" : txtReturnBeforDays.Text, out returnDays) || returnDays < 0)
        {
            errorMessage = "Return Before Days must be a valid non-negative number";
            txtReturnBeforDays.Focus();
            return false;
        }
        if (!int.TryParse(string.IsNullOrWhiteSpace(txtExpiry.Text) ? "0" : txtExpiry.Text, out expiryValidation))
        {
            errorMessage = "Expiry Validation must be a valid number";
            txtExpiry.Focus();
            return false;
        }
        if (maxLevel > 0 && maxLevel < minLevel)
        {
            errorMessage = "Maximum level must be greater than or equal to Minimum level";
            txtMaximumLevel.Focus();
            return false;
        }
        if (dangerLevel > 0 && maxLevel > 0 && dangerLevel > maxLevel)
        {
            errorMessage = "Danger level must be less than or equal to Maximum level";
            txtDangerlevel.Focus();
            return false;
        }

        return true;
    }


    protected void BindData()
    {
        Populate populate = new Populate();

        populate.FillDropdownList(ddlCategoryFirst, "Category", "CategoryName", "ID_Category", "CategoryName", " FK_Client= " + hfClient.Value+"AND Cancelled=0", "");
        populate.FillDropdownList(ddlRack, "Rack", "RackName", "ID_Rack", "RackName",   "FK_Client = " + hfClient.Value + "AND Cancelled=0", "");
        populate.FillDropdownList(ddlBin, "Bin", "BinName", "ID_Bin", "BinName", " FK_Client=" + hfClient.Value + "AND Cancelled=0", "");
        populate.FillDropdownList(ddlGeneric, "Generic", "GenericName", "ID_Generic", "GenericName", " FK_Client=" + hfClient.Value + "AND Cancelled=0", "");
        populate.FillDropdownList(ddlManufacturer, "Manufacturer", "ManufactureName", "ID_Manufacturer", "ManufactureName", "FK_Client=" + hfClient.Value + "AND Cancelled=0", "");
        populate.FillDropdownList(ddlSearchCategoryFirst, "Category", "CategoryName", "ID_Category", "CategoryName", " FK_Client= " + hfClient.Value + "AND Cancelled=0", "");
        populate.FillDropdownList(ddlSearchCategorySecond, "SubCategory", "SubCategoryName", "ID_SubCategory", "SubCategoryName", " FK_Client= " + hfClient.Value + "AND Cancelled=0", "");
        populate.FillDropdownList(ddlUnit, "Units", "UnitName", "ID_Units", "UnitName", " FK_Client= " + hfClient.Value+"AND Cancelled=0", "");
        populate.FillDropdownList(ddlSubUnit, "SubUnit", "SubUnitName", "ID_SubUnits", "SubUnitName", " FK_Client= " + hfClient.Value + "AND Cancelled=0", "");
        populate.FillDropdownList(ddlSearchManufacturer, "Manufacturer", "ManufactureName", "ID_Manufacturer", "ManufactureName", "FK_Client=" + hfClient.Value + "AND Cancelled=0", "");
        populate.FillDropdownList(ddlSchedule, "ScheduleMaster", "ScheduleName", "ID_Schedule", "ScheduleName", "FK_Client=" + hfClient.Value + "AND Cancelled=0", "");
        populate.FillDropdownList(ddlSearchSceduled, "ScheduleMaster", "ScheduleName", "ID_Schedule", "ScheduleName", "FK_Client=" + hfClient.Value + "AND Cancelled=0", "");
        populate = null;
        BinSubcategory();
       
        BinBin();
       
    }
    protected void BinSubcategory()
    {
        new Populate().FillDropdownList(ddlCategorySecond, "SubCategory", "SubCategoryName", "ID_SubCategory", "SubCategoryName", "FK_Category = " + ddlCategoryFirst.SelectedValue + " and FK_Client = " + hfClient.Value + "AND Cancelled=0", "");

    }
   
    protected void BinBin()
    {
        new Populate().FillDropdownList(ddlBin, "Bin", "BinName", "ID_Bin", "BinName", "FK_Rack = " + ddlRack.SelectedValue + " and FK_Client = " + hfClient.Value + "AND Cancelled=0", "");
    }
 
    protected void CheckUserAccess()
    {
        BlUserAccess bluseraccess = new BlUserAccess();
        Populate populate = new Populate();

        bluseraccess.FK_Users = Convert.ToInt64(hfUser.Value);
        bluseraccess.FK_Pages = 1;
        bluseraccess.FK_Client = Convert.ToInt64(hfClient.Value);
        bluseraccess.CheckUserAccess();
        if (Convert.ToBoolean(bluseraccess.IsView) == false)
        {
        }
        //string sPath = Request.Url.AbsolutePath;
        //System.IO.FileInfo oInfo = new System.IO.FileInfo(sPath);
        //string sRet = oInfo.Name;
       
        hfIsAdd.Value = (Convert.ToBoolean(bluseraccess.IsAdd)).ToString();
        hfIsUpdate.Value = (Convert.ToBoolean(bluseraccess.IsUpdate)).ToString();
        hfIsDelete.Value = (Convert.ToBoolean(bluseraccess.IsDelete)).ToString();
        bluseraccess = null;
    }
    protected void SetControls()
    {
        
    }
    protected void GridFill(int currentPage)
    {
        BlItems blItems = new BlItems();
        DataTable dtbl = new DataTable();
        blItems.ShortName = txtSearchShortName.Text.Trim();
        blItems.Name = txtSearchName.Text.Trim();
        blItems.FK_CategoryFirst = Convert.ToInt64(ddlSearchCategoryFirst.SelectedValue);
        blItems.FK_CategorySecond = Convert.ToInt64(ddlSearchCategorySecond.SelectedValue);
        blItems.FK_Manufacturer = Convert.ToInt64(ddlSearchManufacturer.SelectedValue);
        blItems.FK_Schedule = Convert.ToInt64(ddlSearchSceduled.SelectedValue);
        blItems.Combo = Convert.ToBoolean(chckCombo.Checked.ToString());
        blItems.FK_Client = Convert.ToInt64(hfClient.Value);
        blItems.PageIndex = Convert.ToInt16(currentPage);
        blItems.UserCode = hfUser.Value;
        dtbl = blItems.SelectAllData();
        gvItems.DataSource = dtbl;
        gvItems.DataBind();
        hdfSearchCriteria.Value = "Barcode=" + txtBarcodeSearch.Text.Trim() + "&ShortName=" + txtSearchShortName.Text.Trim() + "&Name=" + txtSearchName.Text.Trim() + "&FK_CategoryFirst=" + ddlSearchCategoryFirst.SelectedValue + "&FK_CategorySecond=" + ddlSearchCategorySecond.SelectedValue + "&FK_CategoryThird=" + ddlSearchCategoryThird.SelectedValue  + "&FK_Manufacturer=" + ddlSearchManufacturer.SelectedValue + "&FK_Client=" + hfClient.Value + "&User=" + hfUser.Value;
        if (dtbl.Rows.Count > 0)
        {
            
        }
        blItems = null;
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
    protected void SaveData()
    
    {
        long status = 0;
        BlItems blItems = new BlItems();
        blItems.Name = txtName.Text.ToString();
        blItems.ShortName = txtShortName.Text.ToString();
        blItems.FK_CategoryFirst = Convert.ToInt64(ddlCategoryFirst.SelectedValue==""?"0": ddlCategoryFirst.SelectedValue);
        blItems.FK_CategorySecond = Convert.ToInt64(ddlCategorySecond.SelectedValue == "" ? "0" : ddlCategorySecond.SelectedValue);
        blItems.FK_Units = Convert.ToInt64(ddlUnit.SelectedValue);
        blItems.FK_Rack = Convert.ToInt64(ddlRack.SelectedValue);
        blItems.FK_Manufacturer = Convert.ToInt64(ddlManufacturer.SelectedValue);
        blItems.FK_Bin = Convert.ToInt64(ddlBin.SelectedValue == "" ? "0" : ddlBin.SelectedValue);
        blItems.FK_Generic = Convert.ToInt64(ddlGeneric.SelectedValue == "" ? "0" : ddlGeneric.SelectedValue);
        blItems.FK_SubUnit = Convert.ToInt64(ddlSubUnit.SelectedValue == "" ? "0" : ddlSubUnit.SelectedValue);
        blItems.StoragePlace =txtStoragePlace.Text.ToString();
        blItems.ReturnBeforDays = Convert.ToInt32(txtReturnBeforDays.Text.ToString());
        blItems.ExpiryWarningBefore=Convert.ToInt32(txtExpiryDays.Text==""?"0":txtExpiryDays.Text);
        //try
        //{
        //    blItems.MinLevel = Convert.ToDecimal(txtMinLevel.Text);
        //}
        //catch (Exception ex)
        //{
        //    blItems.MinLevel = Convert.ToDecimal(0);
        //}
        try
        {
            blItems.MinLevel = string.IsNullOrWhiteSpace(txtMinLevel.Text) ? 0 : Convert.ToDecimal(txtMinLevel.Text);
        }
        catch (Exception ex)
        {
            // Should not happen post-validation; log for diagnostics
            System.Diagnostics.Trace.TraceError("MinLevel parse failed: " + ex.Message);
            blItems.MinLevel = 0;
        }
     
        try
        {
            blItems.MaxLevel = Convert.ToDecimal(txtMaximumLevel.Text);
        }
        catch (Exception ex)
        {
            blItems.MaxLevel = Convert.ToDecimal(0);
        }
        try
        {
            blItems.ROLevel = Convert.ToDecimal(txtReorderlevel.Text);
        }
        catch (Exception ex)
        {
            blItems.ROLevel = Convert.ToDecimal(0);
        }
      
        try
        {
            blItems.DangerLevel = Convert.ToDecimal(txtDangerlevel.Text);
        }
        catch (Exception ex)
        {
            blItems.DangerLevel = Convert.ToDecimal(0);
        }

      
        UploadImages();
        blItems.StopSale = Convert.ToBoolean(chkstopsale.Checked);
        blItems.NotReturnable = Convert.ToBoolean(chkNotReturnable.Checked);
        blItems.FK_Schedule = Convert.ToInt64(ddlSchedule.SelectedValue == "" ? "0" : ddlSchedule.SelectedValue);
        blItems.ROQuantity = Convert.ToDecimal(txtReoqty.Text.ToString());
        blItems.ExpairyValidation = Convert.ToInt32(txtExpiry.Text.ToString());
        blItems.Packed = Convert.ToBoolean(chkPacked.Checked);
        blItems.ExpairyValidation =Convert.ToInt32(txtExpiry.Text);
        blItems.StopSale = Convert.ToBoolean(chkstopsale.Checked);
        blItems.FK_Client = Convert.ToInt64(hfClient.Value);
        blItems.UserCode = hfuserName.Value;
        blItems.HSNCode = txtHSNSAC.Text.ToString();
        blItems.StopSaleRemarks = txtStopsale.Text.ToString();
        blItems.NotReturnable = Convert.ToBoolean(chkNotReturnable.Checked);
        
        ArrayList arlist = new ArrayList();
        DataTable dtbl = (DataTable)ViewState["TempTable"];
        IItemsDetails IItemsDetails;
        if (gvItemsdetails.Rows.Count > 0)
        {
            for (int i = 0; i < dtbl.Rows.Count; i++)
            {
                IItemsDetails = new IItemsDetails();
                IItemsDetails.FK_Items = Convert.ToInt64(dtbl.Rows[i]["FK_Items"].ToString());
                IItemsDetails.Quantity = Convert.ToDecimal(dtbl.Rows[i]["Quantity"].ToString());
                arlist.Add(IItemsDetails);
                IItemsDetails = null;
            }
        }
        blItems.XmlItemsDetails = Common.WriteToXml(arlist);
        ArrayList arlistTaxType = new ArrayList();
        DataTable dtTax = (DataTable)ViewState["TempTaxTable"];
        ITaxDetails ITaxDetails;
       if (grdTaxtype.Rows.Count > 0)
        {
            for (int i = 0; i < dtTax.Rows.Count; i++)
            {
                ITaxDetails = new ITaxDetails();
                ITaxDetails.ID_TaxType = Convert.ToInt64(dtTax.Rows[i]["ID_TaxType"].ToString());
                ITaxDetails.Percentage = Convert.ToDecimal(dtTax.Rows[i]["TaxPercentage"].ToString());
                arlistTaxType.Add(ITaxDetails);
                ITaxDetails = null;
            }
      }
        if (gvItemsdetails.Rows.Count > 0)
            blItems.Combo = true;
        else
            blItems.Combo = false;

        blItems.XmlTaxDetails = Common.WriteToXml(arlistTaxType);
        if (hfItemsId.Value == "0")
        {
            if (hfIsAdd.Value.ToLower() == "true")
            {
                status = blItems.InsertData();
                if (status > 0)
                {
                    ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "ShowAddDiv();", true);
                    ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "toastr.success('Data Inserted Successfully');ClearAdd();", true);
                    GridFill(1);
                    imgAttachment.ImageUrl = DefaultImage;
                    hdfImage.Value = "";
                    fupImage.Visible = true;
                    gvItemsdetails.DataSource = null;
                    gvItemsdetails.DataBind();
                    ViewState["TempTable"] = null;
                    grdTaxtype.DataSource = null;
                    grdTaxtype.DataBind();
                    ViewState["TempTaxTable"] = null;
                    
                    CheckingCombo();
                    lbtnRemoveAttachment.Attributes.Add("style", "display:none");
                    ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "ShowAddDiv();", true);
                }
                else if (status == -3)
                {
                    Populate.ErrorMessageBox(this, "Cannot insert data, Barcode already exists for another Item");
                    
                }
                else if (status == -4)
                {
                    Populate.ErrorMessageBox(this, "Cannot insert data, Barcode length should be less than or equal to 5");
                    
                }
                else if (status == -5)
                {
                    Populate.ErrorMessageBox(this, "Cannot insert data, Barcode already exists in Stock");
                    
                }
                else if (status == -10)
                {
                    Populate.ErrorMessageBox(this, "Cannot insert Item, Item already exists in Stock");
                   
                }
                else
                {
                    ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "ShowAddDiv();", true);
                    ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "toastr.error('Cannot insert data, ShortName/Name already exists')", true);
                    txtShortName.Focus();
                }
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "toastr.warning('User does not have permission to add')", true);
                txtShortName.Focus();

            }
        }
        else
        {
            if (hfIsUpdate.Value.ToLower() == "true")
            {
                blItems.MasterId = Convert.ToInt64(hfItemsId.Value);
                blItems.FK_Client = Convert.ToInt64(hfClient.Value);
                status = blItems.UpdateData();
                if (status > 0)
                {
                    ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "ShowAddDiv();", true);
                    ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "toastr.success('Data Updated Successfully');ClearAdd();", true);
                    GridFill(1);
                    imgAttachment.ImageUrl = DefaultImage;
                    hdfImage.Value = "";
                    fupImage.Visible = true;
                    gvItemsdetails.DataSource = null;
                    gvItemsdetails.DataBind();
                    ViewState["TempTable"] = null;
                    grdTaxtype.DataSource = null;
                    grdTaxtype.DataBind();
                    ViewState["TempTaxTable"] = null;
                    CheckingCombo();
                    lbtnRemoveAttachment.Attributes.Add("style", "display:none");
                    ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "ShowAddDiv();", true);
                }
                else if (status == -3)
                {
                    Populate.ErrorMessageBox(this, "Cannot update data, Barcode already exists for another Item");
                }
                else if (status == -6)
                {
                    Populate.ErrorMessageBox(this, "Cannot insert data, Barcode length should be less than or equal to 5");
                    
                }
                else if (status == -5)
                {
                    Populate.ErrorMessageBox(this, "Cannot insert data, Barcode already exists in Stock");
                    
                }
                else if (status == -10)
                {
                    Populate.ErrorMessageBox(this, "Cannot insert Item, Item already exists in Stock");
                   
                }
                else if (status == -4)
                {
                    ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "ShowAddDiv();", true);
                    ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "toastr.success('Data Updated successfully');ClearAdd();", true);
                    GridFill(1);
                    imgAttachment.ImageUrl = DefaultImage;
                    hdfImage.Value = "";
                    fupImage.Visible = true;
                    gvItemsdetails.DataSource = null;
                    gvItemsdetails.DataBind();
                    ViewState["TempTable"] = null;
                    CheckingCombo();
                    lbtnRemoveAttachment.Attributes.Add("style", "display:none");
                    ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "ShowAddDiv();", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "ShowAddDiv();", true);
                    ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "toastr.error('Cannot update data, ShortName/Name already exists')", true);
                    txtShortName.Focus();
                }
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "toastr.warning('User does not have permission to Update')", true);
                txtShortName.Focus();
            }
        }
        
        blItems = null;
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


    public void ItemFill(int pageIndex)
    {
        ClearSupplier();
        Populate Populate = new Populate();
        CommonPopup1.CallingFunction = "ItemFill";
        string strName = ((TextBox)CommonPopup1.FindControl("txtName")).Text;
        string Refno = ((TextBox)CommonPopup1.FindControl("txtRefno")).Text;
        string searchCol = ((HiddenField)CommonPopup1.FindControl("hfColumn")).Value;
        Refno = Refno == string.Empty || Refno == null ? "0" : Refno;

        Populate.FillPopUpGrid((GridView)CommonPopup1.FindControl("gdvSearch"), "Items", "ID_Items as ID,ItemName as Name", "ID_Items", "ID_Items", "FK_Client=" + hfClient.Value.Split(',')[0] + " AND Combo=0  AND Cancelled=0 AND ItemName ", strName, null);
        Populate = null;

        if (txtItem.Text == string.Empty)
        {
            lblPopupHeader.Text = "Item";
            mpCommon.Show();
        }

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

    private void ClearSupplier()
    {
        txtItem.Text = "";

    }
    private void ClearTax()
    {
        txtTaxType.Text = "";

    }

    public void SetDetails(string strName, string strRefno)
    {
        if (CommonPopup1.CallingFunction == "ItemFill")
        {
            txtItem.Text = strName;
            hfSelectedItem.Value = strRefno.ToString();
        }
        if (CommonPopup1.CallingFunction == "ItemTaxFill")
        {
            txtTaxType.Text = strName;
            hfSelectedTax.Value = strRefno.ToString();
        }
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
            dtbl.Columns.Add("Item");
            dtbl.Columns.Add("FK_Items");
            dtbl.Columns.Add("Quantity");
        }
        if (dtbl.Rows.Count > 0)
        {

            for (int i = 0; i < dtbl.Rows.Count; i++)
            {
                if (dtbl.Rows[i]["FK_Items"].ToString() == hfSelectedItem.Value)
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
            drow["Item"] = txtItem.Text;
            drow["FK_Items"] = hfSelectedItem.Value;
            drow["Quantity"] = txtItemQuantity.Text == "" ? "0" : txtItemQuantity.Text;
            if (txtItem.Text == string.Empty||txtItemQuantity.Text == string.Empty  )
            {
                ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "alertify.alert('Enter Quantity')", true);
            }
            else
            {
                dtbl.Rows.InsertAt(drow, rowCount);
                gvItemsdetails.DataSource = dtbl;
                gvItemsdetails.DataBind();
                ViewState["TempTable"] = dtbl;
                txtItem.Text = string.Empty;
                txtItemQuantity.Text = string.Empty;
            }
           

        }
        else
        {
            ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "alertify.alert('Item already exists')", true);
            txtItem.Text = string.Empty;
            txtItemQuantity.Text = string.Empty;
        }

    }
    protected void AddTaxData()
    {
        DataTable dtbl = new DataTable();
        int rowCount = 0;
        int inCheck = 0;
        if (ViewState["TempTaxTable"] != null)
        {
            dtbl = (DataTable)ViewState["TempTaxTable"];
            rowCount = dtbl.Rows.Count;
        }
        else
        {
           // dtbl.Columns.Add("SLNO");
            dtbl.Columns.Add("TaxType");
            dtbl.Columns.Add("ID_TaxType");
            dtbl.Columns.Add("TaxPercentage",typeof(decimal));
        }
        if (dtbl.Rows.Count > 0)
        {

            for (int i = 0; i < dtbl.Rows.Count; i++)
            {
                if (dtbl.Rows[i]["ID_TaxType"].ToString() == hfSelectedTax.Value)
                {
                    inCheck = inCheck + 1;
                    break;
                }
            }
        }
        if (inCheck == 0)
        {
            DataRow drow = dtbl.NewRow();
          //  drow["SLNO"] = rowCount + 1;
            drow["TaxType"] = txtTaxType.Text;
            drow["ID_TaxType"] = hfSelectedTax.Value;
            drow["TaxPercentage"] = (Convert.ToDouble(txtQ.Text == "" ? "0.00" : txtQ.Text)).ToString("0.00");
            if (txtTaxType.Text == string.Empty || txtQ.Text == "0")
            {
                Populate.ErrorMessageBox(this, "Enter Percentage");
                //ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "alertify.alert('Enter Percentage')", true);

            }
            else
            {
                dtbl.Rows.InsertAt(drow, rowCount);
                grdTaxtype.DataSource = dtbl;
                grdTaxtype.DataBind();
                ViewState["TempTaxTable"] = dtbl;
                txtTaxType.Text = string.Empty;
                txtQ.Text = string.Empty;
            }
           

        }
        else
        {
            Populate.ErrorMessageBox(this, "Tax Type already exists");
           // ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "alertify.alert('Tax Type already exists')", true);
            txtTaxType.Text = string.Empty;
            txtQ.Text = string.Empty;
        }

    }
    public void TaxType()
    {
        try
        {
            ((TextBox)CommonPopup1.FindControl("txtName")).Text = txtTaxType.Text.Trim();
            ((TextBox)CommonPopup1.FindControl("txtRefno")).Text = txtTaxType.Text.Trim();
            ((HiddenField)CommonPopup1.FindControl("hfColumn")).Value = "Name";
            ItemTaxFill(1);
            divAdd.Style.Add("display", "block");
            divView.Style.Add("display", "none");
        }
        catch (Exception ex)
        {
        }
    }
    public void ItemTaxFill(int pageIndex)
    {
        ClearTax();
        Populate Populate = new Populate();
        CommonPopup1.CallingFunction = "ItemTaxFill";
        string strName = ((TextBox)CommonPopup1.FindControl("txtName")).Text;
        string Refno = ((TextBox)CommonPopup1.FindControl("txtRefno")).Text;
        string searchCol = ((HiddenField)CommonPopup1.FindControl("hfColumn")).Value;
        Refno = Refno == string.Empty || Refno == null ? "0" : Refno;

        Populate.FillPopUpGrid((GridView)CommonPopup1.FindControl("gdvSearch"), "TaxType", "ID_TaxType as ID,TaxType as Name", "ID_TaxType", "ID_TaxType", "FK_Client=" + hfClient.Value.Split(',')[0] + " AND Cancelled=0 AND TaxType ", strName, null);
        Populate = null;

        if (txtTaxType.Text == string.Empty)
        {
            lblPopupHeader.Text = "Tax Settings";
            mpCommon.Show();
        }

       
    }
    #endregion

    #region Events
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                Session["Attachment"] = null;
                Page.Header.DataBind();
                hfUser.Value = Request.QueryString["uid"];
                hfClient.Value = Request.QueryString["clnt"];
                ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "ClearAdd();", true);
                divAdd.Style.Add("display", "block");
                CheckUserAccess();
                BindData();
                SetControls();
                GridFill(1);
                txtShortName.Focus();
                liStoreStock.Attributes.Add("style", "display:none");
                ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "DisableTextBox();", true);
                hfuserName.Value = Session["Username"].ToString();
                gvItemsdetails.DataSource = null;
                gvItemsdetails.DataBind();
                ViewState["TempTable"] = null;
                grdTaxtype.DataSource = null;
                grdTaxtype.DataBind();
                ViewState["TempTaxTable"] = null;
                CheckingCombo();
                SetReadonly(txtItem,txtTaxType);
                if (Convert.ToBoolean(hfIsAdd.Value) == false)
                {
                    ((Label)CommonPopupPrivilege.FindControl("popuplabel")).Text = "User does not have permission to Add";
                    mpPrivilege.Show();
                }
                if (Convert.ToBoolean(hfIsUpdate.Value) == false)
                {
                    gvItems.Columns[4].Visible = false;
                }
                if (Convert.ToBoolean(hfIsDelete.Value) == false)
                {
                    gvItems.Columns[5].Visible = false;
                }
               

            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "toastr.warning('" + ex.Message + "')", true);
        }
    }
    public void SetReadonly(params TextBox[] textbox)
    {
        foreach (TextBox txt in textbox)
            txt.Attributes.Add("readonly", "readonly");

    }
    protected void Page_Changed(object sender, EventArgs e)
    {
        
        int pageIndex = int.Parse((sender as LinkButton).CommandArgument);
        GridFill(pageIndex);
    }
    protected void imgbtnView_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            divAdd.Style.Add("display", "none");
            divView.Style.Add("display", "block");
            divImport.Style.Add("display", "none");
            ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "ClearAdd();", true);
            ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "EnableExport();", true);
            GridFill(1);
            txtSearchShortName.Focus();
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
            
            GridFill(1);
        
            ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "EnableExport();", true);
            txtSearchShortName.Focus();

        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "toastr.warning('" + ex.Message + "')", true);
        }
    }
    protected void gvItems_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        try
        {
            
            DataTable dtbl = new DataTable();
            BlItems blItems = new BlItems();
            blItems.MasterId = Convert.ToInt64(gvItems.DataKeys[e.NewSelectedIndex]["ID_Items"].ToString());
            hfItemsId.Value = blItems.MasterId.ToString();
            blItems.FK_Client = Convert.ToInt64(hfClient.Value);
            blItems.UserCode = hfUser.Value;
            blItems.SelectData();
            txtExpiryDays.Text = blItems.ExpiryWarningBefore.ToString();
            txtName.Text = blItems.Name.ToString();
            txtShortName.Text = blItems.ShortName.ToString();
            ddlCategoryFirst.SelectedValue = blItems.FK_CategoryFirst.ToString();
            ddlRack.SelectedValue = blItems.FK_Rack.ToString();
            ddlGeneric.SelectedValue = blItems.FK_Generic.ToString();
            try
            {
                ddlManufacturer.SelectedValue = blItems.FK_Manufacturer.ToString();
            }
            catch (Exception ex)
            {
                ddlManufacturer.SelectedValue = "0";
            }
           
            txtMinLevel.Text = blItems.MinLevel.ToString();
            txtMaximumLevel.Text = blItems.MaxLevel.ToString();
            txtReorderlevel.Text = blItems.ROLevel.ToString();
            txtReoqty.Text = blItems.ROQuantity.ToString();
            txtExpiry.Text = blItems.ExpairyValidation.ToString();
            ddlSchedule.SelectedValue = blItems.FK_Schedule.ToString();
            txtStoragePlace.Text = blItems.StoragePlace.ToString();
            chkNotReturnable.Checked = blItems.NotReturnable;
            txtReturnBeforDays.Text = blItems.ReturnBeforDays.ToString();
            txtDangerlevel.Text = blItems.DangerLevel.ToString();
            txtHSNSAC.Text = blItems.HSNCode.ToString();
            chkstopsale.Checked = blItems.StopSale;
            chkPacked.Checked = blItems.Packed;
            txtStopsale.Text = blItems.StopSaleRemarks.ToString();
            txtStoragePlace.Text = blItems.StoragePlace.ToString();
            chkPacked.Checked = blItems.Packed;
           
            if (blItems.StopSale == false)
            {

              
            }
      
            ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "EnablePrint();", true);
            Populate populate = new Populate();
            populate.FillDropdownList(ddlCategorySecond, "SubCategory", "SubCategoryName", "ID_SubCategory", "SubCategoryName", " FK_Client=" + hfClient.Value + " AND FK_Category= " + ddlCategoryFirst.SelectedValue.ToString()+"AND Cancelled=0", "");
            ddlCategorySecond.SelectedValue = blItems.FK_CategorySecond.ToString();
            populate.FillDropdownList(ddlBin, "Bin", "BinName", "ID_Bin", "BinName", " FK_Client=" + hfClient.Value + " AND FK_Rack= " + ddlRack.SelectedValue.ToString() + "AND Cancelled=0", "");
            ddlBin.SelectedValue = blItems.FK_Bin.ToString();
            ddlUnit.SelectedValue = blItems.FK_Units.ToString();
            ddlSubUnit.SelectedValue = blItems.FK_SubUnit.ToString();
           
            populate = null;
            dtbl = blItems.SelectItemsDetails();
            gvItemsdetails.DataSource = dtbl;
            gvItemsdetails.DataBind();
            ViewState["TempTable"] = dtbl;
            DataTable dttaxdetails = new DataTable();
            dttaxdetails = blItems.SelectItemWiseTaxDetails();
            grdTaxtype.DataSource = dttaxdetails;
            grdTaxtype.DataBind();
            ViewState["TempTaxTable"] = dttaxdetails;
            txtItem.Enabled = true;
            IbtnItemAdd.Enabled = true;
            txtItemQuantity.Enabled = true;
            btnAdd.Enabled = true;
            txtTaxType.Enabled = true;
            imgSearch.Enabled = true;
            txtQ.Enabled = true;
            btnSub.Enabled = true;
            blItems = null;
            ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "ShowAddDiv();EnablePrint();", true);
            ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "ShowAddDiv();EnablePrint();", true);

        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "toastr.warning('" + ex.Message + "')", true);
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
                BlItems blItems = new BlItems();
                blItems.MasterId = Convert.ToInt64(gvItems.DataKeys[e.RowIndex]["ID_Items"].ToString());
                hfMasterID.Value = (gvItems.DataKeys[e.RowIndex]["ID_Items"].ToString());
                blItems.FK_Client = Convert.ToInt64(hfClient.Value);
                blItems.UserCode = hfUser.Value;
                blItems = null;
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
    public bool VerifyTaxDetails()
    {
        bool _Continue = true;
        var dt =(DataTable) ViewState["TempTaxTable"];
        string CGST = "0", SGST = "0";
        if (dt != null)
        {
            foreach (DataRow row in dt.Rows)
            {
                if (row["ID_TaxType"].ToString() == "1")//CGST
                {
                    CGST = row["TaxPercentage"].ToString().Trim();
                }
                else if (row["ID_TaxType"].ToString() == "2")//SGST
                {
                    SGST = row["TaxPercentage"].ToString().Trim();
                }
            }
            if (CGST != "0" && SGST != "0")
            {
                if (CGST != SGST)
                {
                    Populate.ErrorMessageBox(this, "Please verify Tax Details,CGST & SGST Must be equal.");
                    _Continue = false;
                }
            }
            else if (CGST != "0" || SGST != "0")
            {
                if (CGST == "0")
                {
                    Populate.ErrorMessageBox(this, "Please Verify Tax Details,Enter CGST.");
                    _Continue = false;
                }
                else if (SGST == "0")
                {
                    Populate.ErrorMessageBox(this, "Please Verify Tax Details,Enter SGST.");
                    _Continue = false;
                }

            }
        }
        return _Continue;
    }
    //protected void btnSubmit_Click(object sender, EventArgs e)
    //{
    //    try
    //    {

    //        if (txtName.Text.ToString() == string.Empty || txtShortName.Text.ToString() == string.Empty || ddlGeneric.SelectedIndex == 0 || ddlManufacturer.SelectedIndex == 0 || ddlUnit.SelectedIndex == 0 || ddlCategoryFirst.SelectedIndex == 0)
    //        {
    //            Populate.ErrorMessageBox(this, "Enter Required Data");

    //        }
    //        else
    //        {
    //            if (VerifyTaxDetails())
    //            {
    //                SaveData();
    //            }
    //        }
           
    //    }
    //    catch (Exception ex)
    //    {
    //        ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "toastr.warning('" + ex.Message + "')", true);
    //    }
    //}

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            if (txtName.Text.ToString() == string.Empty || txtShortName.Text.ToString() == string.Empty || ddlGeneric.SelectedIndex == 0 || ddlManufacturer.SelectedIndex == 0 || ddlUnit.SelectedIndex == 0 || ddlCategoryFirst.SelectedIndex == 0)
            {
                Populate.ErrorMessageBox(this, "Enter Required Data");
            }
            else
            {
                string errorMessage;
                if (!ValidateItemLevels(out errorMessage))
                {
                    Populate.ErrorMessageBox(this, errorMessage);
                    return;
                }
                if (gvItemsdetails.Rows.Count == 0 && /* was checked as combo somewhere */ false)
                {
                    // see Combo note below
                }
                if (VerifyTaxDetails())
                {
                    SaveData();
                }
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "toastr.warning('" + ex.Message + "')", true);
        }
    }

    protected void btnGenerate_Click(object sender, EventArgs e)
    {
        BlItems blItems = new BlItems();
        blItems.FK_Client = Convert.ToInt64(hfClient.Value);
        DataTable dtBarcode = new DataTable();
        dtBarcode = blItems.SelectBarcode();
        ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "ShowAddDiv();", true);
    }

    protected void btnHidden_Click(object sender, EventArgs e)
    {
        try
        {
            if (Session["Attachment"] != null)
            {
                lbtnRemoveAttachment.Attributes.Add("style", "display:block;");
                imgAttachment.ImageUrl = "~\\Attachments\\" + Session["Attachment"].ToString();

            }
            else
            {
                imgAttachment.ImageUrl = DefaultImage;
                lbtnRemoveAttachment.Attributes.Add("style", "display:none");
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "toastr.warning('" + ex.Message + "')", true);
        }
    }

    protected void btnClear_Click(object sender, EventArgs e)
    {
        try
        {
            

            imgAttachment.ImageUrl = DefaultImage;
            hdfImage.Value = "";
            fupImage.Visible = true;
            lbtnRemoveAttachment.Attributes.Add("style", "display:none");
            liStoreStock.Attributes.Remove("style");
            liStoreStock.Attributes.Add("style", "display:none");
            gvItemsdetails.DataSource = null;
            gvItemsdetails.DataBind();
            ViewState["TempTable"] = null;
            grdTaxtype.DataSource = null;
            grdTaxtype.DataBind();
            ViewState["TempTaxTable"] = null;
            lblTInMsg.Text = "";
            ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "ShowAddDiv();", true);
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "toastr.warning('" + ex.Message + "')", true);
        }
    }
    protected void btnUnitBlurTrigger_Click(object sender, EventArgs e)
            {

        try
        {
            Populate populate = new Populate();
            populate.FillDropdownList(ddlCategorySecond, "SubCategory", "SubCategoryName", "ID_SubCategory", "SubCategoryName", " FK_Client=" + hfClient.Value + " AND FK_Category= " + ddlCategoryFirst.SelectedValue.ToString() + "AND Cancelled=0", "");
            populate = null;
            divAdd.Style.Add("display", "block");
            divView.Style.Add("display", "none");
            ddlCategorySecond.Focus();
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "toastr.warning('" + ex.Message + "')", true);
        }





     
    } 

    protected void lbtnRemoveAttachment_Click(object sender, EventArgs e)
    {
        try
        {

            imgAttachment.ImageUrl = DefaultImage;
            hdfImage.Value = "";
            fupImage.Visible = true;
            lbtnRemoveAttachment.Attributes.Add("style", "display:none");
            ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "ShowAddDiv();", true);

        }
        catch (Exception ex)
        {
            Populate.WarningMsg(this, ex.ToString());
        }
    }

    protected void btnHiddenImage_Click(object sender, EventArgs e)
    {
        try
        {
            if (Session[Image] != null)
            {
                
                lbtnRemoveAttachment.Attributes.Add("style", "display:block;");
                hdfImage.Value = Session[Image].ToString();
                fupImage.Visible = false;
                imgAttachment.ImageUrl = TempPath + hdfImage.Value.Split(',')[0];
                Session[Image] = null;

                ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "ShowAddDiv();", true);

            }
            else
            {
                Populate.ErrorMessageBox(this, "Image size cannot be greater than 300 KB");
                
            }
        }
        catch (Exception ex)
        {
            Populate.WarningMsg(this, ex.ToString());
        }
    }
    protected void fupImage_UploadedComplete(object sender, AjaxControlToolkit.AsyncFileUploadEventArgs e)
    {
        try
        {


            if (fupImage.HasFile)
            {
                if (fupImage.FileContent.Length <= 300000)
                {

                    //bool status;
                    string filext = System.IO.Path.GetExtension(fupImage.FileName).ToLower();
                    if (filext == ".jpg" || filext == ".jpeg" || filext == ".gif" || filext == ".bmp" || filext == ".png" || filext == ".jpe" || filext == ".jfif" || filext == ".tif" || filext == ".tiff")
                    {


                        Session[Image] = "item_" + DateTime.Now.Ticks.ToString() + filext;
                        fupImage.SaveAs(Server.MapPath(TempPath + Session[Image].ToString()));

                    }
                    else
                    {
                        Populate.ErrorMessageBox(this, "Invalid File Format");
                       
                    }
                }
                else
                {
                    Populate.ErrorMessageBox(this, "Image size cannot be greater than 300 KB");
                      
                }
            }
        }
        catch (Exception ex)
        {
            Populate.WarningMsg(this, ex.ToString());
        }
    }
    protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
    {

       
        gvItemsdetails.DataSource = null;
        gvItemsdetails.DataBind();
        ViewState["TempTable"] = null;
        imgAttachment.ImageUrl = DefaultImage;
        hdfImage.Value = "";
        fupImage.Visible = true;
        lbtnRemoveAttachment.Attributes.Add("style", "display:none");
        divImport.Attributes.Add("style", "display:none");
        ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "ShowAddDiv();ClearAdd();", true);
        liStoreStock.Attributes.Remove("style");
        liStoreStock.Attributes.Add("style", "display:none");
        grdTaxtype.DataSource = null;
        grdTaxtype.DataBind();
        ViewState["TempTaxTable"] = null;
        lblTInMsg.Text = "";
        if (Convert.ToBoolean(hfIsAdd.Value) == false)
        {
            ((Label)CommonPopupPrivilege.FindControl("popuplabel")).Text = "User does not have permission to Add";
            mpPrivilege.Show();
        }
    }
    /*Items Import & Save  From  Excel*/


    protected DataSet DatasetFromExcel(string filePath)
    {

        string ext = Path.GetExtension(filePath);
        dsExcel = new DataSet();

        FileStream stream = File.Open(filePath, FileMode.Open, FileAccess.ReadWrite);

        if (ext == ".xls")//Reading from Excel file ('97-2003 format; *.xls)
        {
            IExcelDataReader excelReader = ExcelReaderFactory.CreateBinaryReader(stream);
            excelReader.IsFirstRowAsColumnNames = true;// DataSet - Create column names from first row
            try
            {
                dsExcel = excelReader.AsDataSet();
            }
            catch
            {
                ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "toastr.warning('Please save the document and Re-Import')", true);

            }
            dsExcel.Tables[0].TableName = "Items";
            excelReader.Close();
        }
        else if (ext == ".xlsx")// Reading from Excel file (2007 format; *.xlsx)
        {
            IExcelDataReader excelReader = ExcelReaderFactory.CreateOpenXmlReader(stream);
            excelReader.IsFirstRowAsColumnNames = true;// DataSet - Create column names from first row
            dsExcel = excelReader.AsDataSet();
            dsExcel.Tables[0].TableName = "Items";
            excelReader.Close();
        }
        if (File.Exists(filePath))
        {
            DirectoryInfo info = new DirectoryInfo(Server.MapPath("Excel"));
            foreach (FileInfo files in info.GetFiles())
            {
                try
                {
                    files.Delete();
                }
                catch (Exception ex)
                {

                }
            }
        }
        return dsExcel;
    }
    protected void FileUploadExcel_UploadedComplete(object sender, AjaxControlToolkit.AsyncFileUploadEventArgs e)
    {
        try
        {
            if (FileUploadExcel.HasFile)
            {
                bool status;
                string filePath = FileUploadExcel.PostedFile.FileName.ToString();
                string path = Server.MapPath("Excel");
                FileUploadExcel.SaveAs(path + "/" + FileUploadExcel.FileName);
                Session[Excel] = path + "/" + FileUploadExcel.FileName;
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "toastr.warning('" + ex.Message + "')", true);
        }
    }

    protected void btnShowData_Click(object sender, EventArgs e)
    {
        try
        {
            ArrayList arlist = new ArrayList();
            ItemsDetails itemDetails;
            if (Session[Excel] != null)
            {
                string ExcelPath = Session[Excel].ToString();
                DataSet ds = DatasetFromExcel(ExcelPath);
                long status = 0;
                if (ds.Tables[0].Rows.Count > 0)
                {


                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        DataRow dr = ds.Tables[0].Rows[i];

                        itemDetails = new ItemsDetails();
                        if (dr["Name"].ToString() != "&nbsp;" && dr["Name"].ToString() != "" && dr["ShortName"].ToString() != "&nbsp" && dr["ShortName"].ToString() != "" && dr["CategoryFirst"].ToString() != "&nbsp;" && dr["CategoryFirst"].ToString() != "" && dr["ItemType"].ToString() != "&nbsp;" && dr["ItemType"].ToString() != "" && dr["Unit"].ToString() != "" && dr["Unit"].ToString() != "&nbsp")
                        {
                            
                            if (dr["Name"].ToString() != "&nbsp")
                                itemDetails.Name = Convert.ToString(dr["Name"]);
                            if (dr["ShortName"].ToString() != "&nbsp")
                                itemDetails.ShortName = Convert.ToString(dr["ShortName"]);

                            //itemDetails.Model = dr["Model"].ToString();

                            try
                            {
                                itemDetails.CategoryFirst = Convert.ToString(dr["CategoryFirst"]);
                            }
                            catch
                            {
                                itemDetails.CategoryFirst = "0";
                            }
                            try
                            {
                                itemDetails.CategorySecond = Convert.ToString(dr["CategorySecond"]);
                            }
                            catch
                            {
                                itemDetails.CategorySecond = "0";
                            }
                           
                            try
                            {
                                itemDetails.Units = Convert.ToString(dr["Unit"]);
                            }
                            catch
                            {
                                itemDetails.Units = "0";
                            }
                            try
                            {
                                itemDetails.Brand = Convert.ToString(dr["Brand"]);
                            }
                            catch
                            {
                                itemDetails.Brand = "0";
                            }
                            try
                            {
                                itemDetails.Manufacturer = Convert.ToString(dr["Manufacturer"]);
                            }
                            catch
                            {
                                itemDetails.Manufacturer = "0";
                            }


                            itemDetails.supplier = Convert.ToString(dr["Supplier"]);
                            itemDetails.Size = Convert.ToString(dr["Size"]);
                            itemDetails.Color = Convert.ToString(dr["Color"]);
                            itemDetails.Storage = Convert.ToString(dr["Storage"]);
                            itemDetails.Discount = Convert.ToString(dr["Discount"]);
                            try
                            {
                                decimal dec = Convert.ToDecimal(itemDetails.Discount);
                                if (dec <= 1)
                                {
                                    dec = dec * 100;
                                    itemDetails.Discount = dec.ToString("0") + '%';
                                }
                                else
                                {
                                    itemDetails.Discount = Convert.ToString(dec);
                                }
                            }
                            catch
                            {

                            }
                           
                            try
                            {
                                itemDetails.MinLevel = Convert.ToDecimal(dr["MinLevel"].ToString());
                            }
                            catch
                            {
                                itemDetails.MinLevel = 0;
                            }
                            try
                            {
                                itemDetails.MaxLevel = Convert.ToDecimal(dr["MaxLevel"].ToString());
                            }
                            catch
                            {
                                itemDetails.MaxLevel = 0;
                            }
                            try
                            {

                                itemDetails.ROLevel = Convert.ToDecimal(dr["ROLevel"].ToString());

                            }
                            catch
                            {
                                itemDetails.ROLevel = 0;
                            }
                           
                            try
                            {
                                itemDetails.DangerLevel = Convert.ToDecimal(dr["DamagedLevel"].ToString());
                            }
                            catch
                            {
                                itemDetails.DangerLevel = 0;
                            }

                            itemDetails.Description = dr["Description"].ToString();
                            try
                            {
                                itemDetails.StopSale = Convert.ToBoolean(dr["StopSale"].ToString());
                            }
                            catch
                            {
                                itemDetails.StopSale = false;
                            }
                            itemDetails.StopSaleRemarks = dr["StopSaleRemarks"].ToString();
                            itemDetails.ItemType = dr["ItemType"].ToString();
                            itemDetails.SpecialMessage = dr["SpecialMessage"].ToString();
                            arlist.Add(itemDetails);

                        }





                    }
                    BlItems blitems = new BlItems();
                    blitems.FK_Client = Convert.ToInt64(hfClient.Value);
                    blitems.UserCode = hfUser.Value;
                    if (arlist.Count > 0)
                    {

                        blitems.XmlImport = Common.WriteToXml(arlist);
                        string Status = blitems.ImportItems();
                        if (Status == "")
                        {
                            ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "toastr.success('Submitted successfully');", true);
                           
                        }
                        else
                        {
                            string[] st = Status.Split('|');
                            if (st.Length > 0)
                            {
                                if (st[0] == "-5")
                                {
                                    Status = "Cannot submit, Invalid entries " + st[1];

                                }
                                if (st[0] == "-4")
                                {
                                    Status = "Cannot submit, CategoryFirst : \"" + st[1] + "\" is invalid for Unit : \"" + st[2] + "\"";
                                }
                                if (st[0] == "-6")
                                {
                                    Status = "Cannot submit, Manufacture :\"" + st[1] + "\" Is invalid for Brand: \"" + st[2] + "\"";
                                }
                                if (st[0] == "-3")
                                {
                                    Status = "Cannot submit data, Barcode already exists for another Item";
                                }

                            }
                            ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "toastr.error('" + Status + "')", true);
                        }
                    }
                    else
                    {
                        ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "toastr.warning('There is no data to import or missing required fields')", true);
                    }

                }
                else
                {
                    
                    ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "toastr.warning('There is no data to import')", true);

                }

                Session[Excel] = null;
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "toastr.warning('Please save and close the importing document and Re-Import')", true);
        }

    }
    protected void imgbtnImport_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            divAdd.Style.Add("display", "none");
            divView.Style.Add("display", "none");
            divImport.Style.Add("display", "block");




        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "toastr.warning('" + ex.Message + "')", true);
        }
    }


   
    protected void ddlSearchCategoryFirst_SelectedIndexChanged(object sender, EventArgs e)
    {

        try
        {
            Populate populate = new Populate();
            if (ddlSearchCategoryFirst.SelectedIndex > 0)
                populate.FillDropdownList(ddlSearchCategorySecond, "SubCategory", "SubCategoryName", "ID_SubCategory", "SubCategoryName", " FK_Client=" + hfClient.Value + " AND FK_Category= " + ddlSearchCategoryFirst.SelectedValue.ToString()+"AND Cancelled=0", "");
          
                populate.FillDropdownList(ddlSearchCategorySecond, "SubCategory", "SubCategoryName", "ID_SubCategory", "SubCategoryName", " FK_Client=" + hfClient.Value.Split(',')[0]+"AND Cancelled=0", "");
             
            populate = null;
            ddlSearchCategorySecond.Focus();

        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "toastr.warning('" + ex.Message + "')", true);
        }
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
                ddlItem.Enabled = true;
                txtItem.Enabled = true;
                IbtnItemAdd.Enabled = true;
                txtItemQuantity.Enabled = true;
                btnAdd.Enabled = true;
                 
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

    protected void btnSub_Click(object sender, EventArgs e)
    {
        try
        {
            if (grdTaxtype.Rows.Count == 0)
            {
                ViewState["TempTaxTable"] = null;
            }
            
            AddTaxData();
            divAdd.Style.Add("display", "block");
            divView.Style.Add("display", "none");
            txtTaxType.Enabled = true;
            imgSearch.Enabled = true;
            txtQ.Enabled = true;
            btnSub.Enabled = true;
            //}
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "toastr.warning('" + ex.Message + "')", true);
        }


    }
    protected void grdTaxtype_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            string confirmValue = Request.Form["confirm_value"];
            if (confirmValue.Length > 3)
                confirmValue = confirmValue.Substring(confirmValue.Length - 3, 3);
            if (confirmValue == "Yes")
            {
                if (ViewState["TempTaxTable"] != null)
                {
                    DataTable dtbl = (DataTable)ViewState["TempTaxTable"];
                    dtbl.Rows.RemoveAt(e.RowIndex);
                    grdTaxtype.DataSource = dtbl;
                    grdTaxtype.DataBind();
                    ViewState["TempTaxTable"] = dtbl;
                    CheckingCombo();
                }

            }
        }
        catch (Exception ex)
        {
            //blMessage.Items.Add(ex.Message);
        }
        ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "ShowAddDiv();", true);
        divAdd.Style.Add("display", "block");
        divView.Style.Add("display", "none");
    }
    protected void imgSearch_Click(object sender, ImageClickEventArgs e)
    {

        try
        {
            TaxType();
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
 
  
    protected void btnRack_Click(object sender, EventArgs e)
                    {
        try
        {

          

            Populate populate = new Populate();
            new Populate().FillDropdownList(ddlBin, "Bin", "BinName", "ID_Bin", "BinName", "FK_Rack = " + ddlRack.SelectedValue + " and FK_Client = " + hfClient.Value + "AND Cancelled=0", "");
            populate = null;
            divAdd.Style.Add("display", "block");
            divView.Style.Add("display", "none");
            ddlBin.Focus();
          
        }

        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "toastr.warning('" + ex.Message + "')", true);
        }

    }
   
    protected void gvItems_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvItems.PageIndex = e.NewPageIndex;
       GridFill(e.NewPageIndex);
    }
  
    protected void gvItemsdetails_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvItemsdetails.PageIndex = e.NewPageIndex;
        gvItemsdetails.DataSource=(DataTable)ViewState["TempTable"];
        gvItemsdetails.DataBind();

    }
    protected void grdTaxtype_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdTaxtype.PageIndex = e.NewPageIndex;
        grdTaxtype.DataSource = (DataTable)ViewState["TempTaxTable"];
        grdTaxtype.DataBind();
    }
    #endregion
    protected void btnVerify_Click(object sender, EventArgs e)
    {
        BlItems blItem = new BlItems();
        if (txtName.Text != "" || txtShortName.Text != "")
        {
            blItem.Name = txtName.Text;

            blItem.ShortName = txtShortName.Text;
            int value = blItem.ItemExistenceChecking();
            if (value == 1)
            {
                lblTInMsg.ForeColor = System.Drawing.Color.Red;
                lblTInMsg.Text = "Shortname already exists";

                
            }
            else if (value == 2)
            {
                lblTInMsg.ForeColor = System.Drawing.Color.Red;
                lblTInMsg.Text = "Name already exists";
               
            }
            else
            {

                lblTInMsg.ForeColor = System.Drawing.Color.Green;
                lblTInMsg.Text = "No match found..";
            }
        }
    }


}