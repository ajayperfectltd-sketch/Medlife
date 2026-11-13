/*----------------------------------------------------------------------------------------------------------------------------------------------------------
Purpose    : Insert/Update/Delete Bank 
Author     : Bibin TC
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
using System.Web.Security;
using WebPharma.Business.Masters;
using WebPharma.Business.WebPharma;
using System.Data;
using System.Text;
using System.IO;
using System.Drawing.Printing;

using System.Diagnostics;


public partial class WeighMachineBarcode : System.Web.UI.Page
{
    PrintDocument pdoc = null;
    StreamWriter yourStream = null;
    public static string SReportFileName = "";
    #region Functions
    protected void BindData()
    {
      
    }
    
    protected void SaveData()
    {
        long status = 0;
        BlBarcodeGeneration blBarcodeGeneration = new BlBarcodeGeneration();

        blBarcodeGeneration.FK_Client = Convert.ToInt64(hfClient.Value);
        blBarcodeGeneration.UserCode = hfUser.Value;
        blBarcodeGeneration.FK_StockCode = Convert.ToInt64(hfItemStockValue.Value);
   
        if (hfIsAdd.Value.ToLower() == "true")
        {
            status = blBarcodeGeneration.InsertData();
            if (status > 0)
            {
                ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "ShowAddDiv();", true);
                ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "toastr.success('BarCode Generated and Saved successfully');", true);
                               
            }
            if (status == -3)
            {

                ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "ShowAddDiv();", true);
                ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "toastr.error('BarCode already exist');", true);

            }
            else if (status == -4)
            {

                ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "ShowAddDiv();", true);
                ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "toastr.error('Entered BarCode should be greater than 10 character');", true);

            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "ShowAddDiv();", true);
                ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "toastr.error('Cannot insert data, Name required ')", true);

            }
        }
        else
        {
            ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "alertify.alert('User does not have permission to add')", true);


        }

        blBarcodeGeneration = null;
    }

    protected void CheckUserAccess()
    {
        BlUserAccess bluseraccess = new BlUserAccess();
        Populate populate = new Populate();

        bluseraccess.FK_Users = Convert.ToInt64(hfUser.Value);
        bluseraccess.FK_Pages = 6;
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
  


    #endregion

    #region Events
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {


            if (!IsPostBack)
            {
                divAdd.Style.Add("display", "block");
                hfUser.Value = Request.QueryString["uid"];
                hfClient.Value = Request.QueryString["clnt"];
                CheckUserAccess();

                BindData();
                SelectDBBackup(1);
            }

        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "alertify.alert('" + ex.Message + "')", true);
        }
    }

    protected void imgbtnView_Click(object sender, ImageClickEventArgs e)
    {
        try
        {

            divAdd.Style.Add("display", "block");

            ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "ClearAdd();", true);

        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "alertify.alert('" + ex.Message + "')", true);
        }
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {

            string a = Convert.ToString(hdfFkItem.Value.ToString());



            SaveData();



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


        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "toastr.warning('" + ex.Message + "')", true);
        }
    }

    #endregion
    protected void hfItemValue_ValueChanged(object sender, EventArgs e)
    {
        try
        {
         
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "toastr.warning('" + ex.Message + "')", true);
        }
    }

    protected void ClearAll()
    {
      
    }
    protected void btnClear_Click(object sender, EventArgs e)
    {
        try
        {
            ClearAll();
          
            ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "ClearAdd();", true);
            ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "ClearAdd();", true);

        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "toastr.warning('" + ex.Message + "')", true);
        }
    }

    protected void btnPrint_Click(object sender, EventArgs e)
    {
        try
        {
            StringBuilder result = new StringBuilder();


            DataTable dtweighingmachinePrint = new DataTable();
            BlBarcodeGeneration blweighingmachinePrint = new BlBarcodeGeneration();
            blweighingmachinePrint.FK_Client = Convert.ToInt64(hfClient.Value);
            blweighingmachinePrint.FK_Items = Convert.ToInt64(0);
            blweighingmachinePrint.UserCode = hfUser.Value;
            dtweighingmachinePrint = blweighingmachinePrint.SelectWeighingMachineItemsSelect();



            for (int i = 0; i < dtweighingmachinePrint.Rows.Count; i++)
            {
                result = result.Append("" + Convert.ToString(dtweighingmachinePrint.Rows[i]["First"].ToString())+"<br />");



            }
            BlSettings blsettings = new BlSettings();
            DataTable dt = new DataTable();
            blsettings.Module = "INV";
            blsettings.SubModule = "WMB";
            dt = blsettings.SelectBarcodeLocationPath();
     
            if (dt.Rows.Count > 0)
            {
                string a = dt.Rows[0]["Value"].ToString() + "WeighingMachineBarCodePrint.txt";
                yourStream = File.CreateText(a);
            }
            else
            {
                yourStream = File.CreateText("D:\\WeighingMachineBarCodePrint.txt");
            }


            ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "toastr.success('BarCode Generated and Saved successfully');", true);
            result = result.Replace("<br />", "" + System.Environment.NewLine);
            yourStream.Write(result);
            yourStream.Close();

            SelectDBBackup(1);

        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "toastr.warning('" + ex.Message + "')", true);
        }
    }
    protected void Page_Changed(object sender, EventArgs e)
    {
        int pageIndex = int.Parse((sender as LinkButton).CommandArgument);
        SelectDBBackup(pageIndex);
    }
    protected void SelectDBBackup(int currentPage)
    {
        DataTable dtbl;
        BlBarcodeGeneration blweighingmachinePrint = new BlBarcodeGeneration();
        dtbl = blweighingmachinePrint.SelectWeighingMachineHistory(currentPage, 10, Convert.ToInt64(hfClient.Value));
        gvLog.DataSource = dtbl;
        gvLog.DataBind();
        if (dtbl.Rows.Count > 0)
        {
            int recordCount = Convert.ToInt32(dtbl.Rows[0]["RecordCount"].ToString());
            this.PopulatePager(recordCount, currentPage);
        }
        blweighingmachinePrint = null;
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
}