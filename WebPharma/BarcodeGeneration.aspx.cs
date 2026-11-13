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
//using BitMiracle.Docotic.Pdf;
///using Neodynamic.SDK.Web;
using System.Diagnostics;
 

public partial class BarcodeGeneration : System.Web.UI.Page
{
    PrintDocument pdoc = null;
    StreamWriter yourStream = null;
      public static string SReportFileName = ""; 
    #region Functions
    protected void BindData()
    {
        Populate populate = new Populate();
        populate.FillDropdownList(ddlPrint, "PrintLayout", "PriModule", "PriModule", "", " PriModule<>'' AND PriModule Like 'BarCode%'  Group by PriModule ", "");
        populate = null;


    }
   
  
    protected void SaveData()
    {
        long status = 0;
        BlBarcodeGeneration blBarcodeGeneration = new BlBarcodeGeneration();
    
        blBarcodeGeneration.FK_Client = Convert.ToInt64(hfClient.Value);
        blBarcodeGeneration.UserCode = hfUser.Value;
        blBarcodeGeneration.FK_StockCode = Convert.ToInt64(hfItemStockValue.Value);
        blBarcodeGeneration.BarCode = Convert.ToString(txtBarCode.Text);
        //if (hfBarcodeGenerationId.Value == "0")
        //{
            if (hfIsAdd.Value.ToLower() == "true")
            {
                status = blBarcodeGeneration.InsertData();
                if (status > 0)
                {
                  
                    ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "ShowAddDiv();", true);
                    ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "toastr.success('BarCode Generated and Saved successfully');", true);
                   // ClearAll();
                    ddlItemsFill();
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
      //  }
        //else
        //{
        //    blBarcodeGeneration.MasterId = Convert.ToInt64(hfBarcodeGenerationId.Value);
        //    if (hfIsUpdate.Value.ToLower() == "true")
        //    {

        //        status = blBarcodeGeneration.UpdateData();
        //        if (status > 0)
        //        {
        //            GridFill(1);
        //            ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "ShowAddDiv();", true);
        //            ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "toastr.success('Data updated successfully');ClearAdd();", true);
        //        }
        //        else
        //        {
        //            ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "ShowAddDiv();", true);
        //            ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "toastr.error('Cannot update data, ShortName/Name already exists')", true);
                 
        //        }
        //    }
        //    else
        //    {
        //        ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "alertify.alert('User does not have permission to Update')", true);
              
        //    }
        //}
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
    protected void ddlItemsFill()
    {
        if (hdfFkItem.Value != "0")
        {
            DataTable dtbl = new DataTable();

            BlItems blitems = new BlItems();
            blitems.MasterId = Convert.ToInt64(hdfFkItem.Value.ToString());
            blitems.FK_Client = Convert.ToInt64(hfClient.Value);
            blitems.SelectData();
             
            txtItemShortName.Text = blitems.Name.ToString();
            blitems = null;
            
            
            BlStock blstock= new BlStock();
            blstock.FK_Items = Convert.ToInt64(hdfFkItem.Value.ToString());
            blstock.MasterId= Convert.ToInt64(hfItemStockValue.Value.ToString());
            blstock.FK_Client = Convert.ToInt64(hfClient.Value);
            
            dtbl = blstock.SelectAllData();   //blItems.StockItemsSelect();
            GvPreviousPurchase.DataSource = dtbl;
            GvPreviousPurchase.DataBind();
            if (dtbl.Rows.Count > 0)
            {
                txtBarCode.Text = dtbl.Rows[0]["BarCode"].ToString();
            }
            else
            {
                ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "toastr.error('Item not in stock')", true);
                //ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "alertify.alert('Item not in stock')", true);
            }
            dtbl = null;
            blstock=null;

           
        }
    }


    #endregion

    #region Events
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
           
               // Page.Header.DataBind();
             
             //   ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "ClearAdd();", true);
            if (!IsPostBack)
            {
                divAdd.Style.Add("display", "block");
                hfUser.Value = Request.QueryString["uid"];
                hfClient.Value = Request.QueryString["clnt"];
                CheckUserAccess();

                BindData();
                GvPreviousPurchase.DataSource = null;
                GvPreviousPurchase.DataBind();

                txtItemShortName.Focus();
            }
            
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "alertify.alert('"+ex.Message+"')", true);    
        }
    }
   
    protected void imgbtnView_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            //blMessage.Items.Clear();
            divAdd.Style.Add("display", "block");
          //  divView.Style.Add("display", "block");
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
            //blMessage.Items.Clear();
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
            //blMessage.Items.Clear();
           
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
            ddlItemsFill();
            txtBarCode.Focus();
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "toastr.warning('" + ex.Message + "')", true);
        }
    }

    protected void ClearAll()
    {
        GvPreviousPurchase.DataSource = null;
        GvPreviousPurchase.DataBind();
    }
    protected void btnClear_Click(object sender, EventArgs e)
    {
        try
        {
            ClearAll();
          //  txtBarCode.Text = "";
            //ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "ClearAdd();", true);
            ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "ClearAdd();", true);

        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "toastr.warning('" + ex.Message + "')", true);
        }
    }
    //public void print()
    //{
    //    PrintDialog pd = new PrintDialog();
    //    pdoc = new PrintDocument();
    //    PrinterSettings ps = new PrinterSettings();
    //    Font font = new Font("Courier New", 15);


    //    PaperSize psize = new PaperSize("Custom", 100, 200);
    //    //ps.DefaultPageSettings.PaperSize = psize;



    //    pd.Document = pdoc;
    //    pd.Document.DefaultPageSettings.PaperSize = psize;
    //    //pdoc.DefaultPageSettings.PaperSize.Height =320;
    //    pdoc.DefaultPageSettings.PaperSize.Height = 820;

    //    pdoc.DefaultPageSettings.PaperSize.Width = 520;

    //    pdoc.PrintPage += new PrintPageEventHandler(pdoc_PrintPage);

    //    DialogResult result = pd.ShowDialog();
    //    if (result == DialogResult.OK)
    //    {
    //        PrintPreviewDialog pp = new PrintPreviewDialog();
    //        pp.Document = pdoc;
    //        result = pp.ShowDialog();
    //        if (result == DialogResult.OK)
    //        {
    //            pdoc.Print();
    //        }
    //    }

    //}
    protected void btnPrint_Click(object sender, EventArgs e)
    {
        try
        {

            //StringBuilder sb = new StringBuilder();

            //sb.AppendLine("var txtBox = document.getElementById('txtBox');");
            //sb.AppendLine(" var popup = window.open( '','popup','toolbar=no,menubar=no,scrollbars=1,width=750,height=800');");

            //sb.AppendLine("popup.document.write('<html><head>');");
            //sb.AppendLine("popup.document.write('<title>Configurations for ACA</title>');");

            //sb.AppendLine("popup.document.write('</head><body>');");
            //sb.AppendLine("popup.document.write('<div id=\"lnk\" class=\"noprint\" style=\"text-align:right\">');");

            //sb.AppendLine("popup.document.write('<a href=\"#\" onclick=\"window.print();return false\">Print</a>');");
            //sb.AppendLine("popup.document.write(' ');");

            //sb.AppendLine("popup.document.write('<a href=\"#\" onclick=\"window.close();return false;\">Close</a>');");
            //sb.AppendLine("popup.document.write('</div>');");

            //sb.AppendLine("popup.document.write('<table border=1px style=width:500px; margin-left:100px; margin-top:50px; border-color:Maroon; >');");
            //sb.AppendLine("popup.document.write('<tr><td>');");

            //sb.AppendLine("popup.document.write('This is my string');");
            //sb.AppendLine("popup.document.write('</td><td>')");

            //sb.AppendLine("popup.document.write('</td></tr></table>');");
            //sb.AppendLine("popup.document.write('</table>');");

            //sb.AppendLine("popup.document.write('</body></html>');");
            //ScriptManager.RegisterClientScriptBlock(this, GetType(), "OpenWindow", sb.ToString(), true);

            DataTable dtsalesPrintlayout = new DataTable();
            DataTable dtStock = new DataTable();
            BlBarcodeGeneration blbarcodegeneration = new BlBarcodeGeneration();


            blbarcodegeneration.FK_Client = Convert.ToInt64(hfClient.Value);
            blbarcodegeneration.PriLayout = Convert.ToString(ddlPrint.SelectedValue);
            dtsalesPrintlayout = blbarcodegeneration.SelectPrintLayout();


            BlStock blstock = new BlStock();
            blstock.MasterId = Convert.ToInt64(hfItemStockValue.Value);
            blstock.FK_Client = Convert.ToInt64(hfClient.Value);
            blstock.FK_Items = Convert.ToInt64(hdfFkItem.Value.ToString());

            dtStock = blstock.SelectAllData();
            StringBuilder result = new StringBuilder();

            for (int i = 0; i < dtsalesPrintlayout.Rows.Count; i++)
            {

                if (Convert.ToString(dtsalesPrintlayout.Rows[i]["PriSubItem"].ToString()) == Convert.ToString("0"))
                {
                    string PriField = dtsalesPrintlayout.Rows[i]["PriField"].ToString();

                    PriField = PriField == "PRINT 1,1" ? ("PRINT 1,"+txtCopy.Text) : PriField;

                    if (dtStock.Columns.Count > 0)
                    {
                        int SalescolumnExist = 0;

                        for (int j = 0; j < dtStock.Columns.Count; j++)
                        {

                            if (PriField == Convert.ToString(dtStock.Columns[j].ColumnName) && Convert.ToString(dtsalesPrintlayout.Rows[i]["PriSubItem"].ToString()) == Convert.ToString("0"))
                            {
                              
                                // graphics.DrawString(Convert.ToString(dtStock.Rows[0][j].ToString().Substring(0, dtStock.Rows[0][j].ToString().Length > Convert.ToInt32(dtsalesPrintlayout.Rows[i]["PriWidth"].ToString()) ? Convert.ToInt32(dtsalesPrintlayout.Rows[i]["PriWidth"].ToString()) : dtStock.Rows[0][j].ToString().Length)), new Font(Convert.ToString(dtsalesPrintlayout.Rows[i]["PriFont"]), Convert.ToInt32(dtsalesPrintlayout.Rows[i]["PriFontSize"]), Convert.ToBoolean(dtsalesPrintlayout.Rows[i]["PriBold"]) == Convert.ToBoolean(true) ? FontStyle.Bold : FontStyle.Regular), new SolidBrush(Color.Black), Convert.ToInt32(dtsalesPrintlayout.Rows[i]["PriCol"]), Convert.ToInt32(startY) * Convert.ToInt64(dtsalesPrintlayout.Rows[i]["PriRow"]) + Convert.ToInt32(Offset));
                                result = result.Append("" + Convert.ToString(dtStock.Rows[0][j].ToString().Substring(0, dtStock.Rows[0][j].ToString().Length > Convert.ToInt32(dtsalesPrintlayout.Rows[i]["PriWidth"].ToString()) ? Convert.ToInt32(dtsalesPrintlayout.Rows[i]["PriWidth"].ToString()) : dtStock.Rows[0][j].ToString().Length)) );
                                result = result.Append("" + Convert.ToBoolean(dtsalesPrintlayout.Rows[i]["priKeepLine"].ToString()) == Convert.ToBoolean(0).ToString() ? "<br />" : "");
                                 SalescolumnExist = 1;

                               // string a = Convert.ToInt32(dtsalesPrintlayout.Rows[i]["priKeepLine"].ToString()) == Convert.ToInt32(0) ? "" : "<br />";

                            }
                            else
                            {
                             //   result = result.Append("" + Convert.ToString(dtsalesPrintlayout.Rows[i]["PriField"].ToString().Substring(0, dtsalesPrintlayout.Rows[i]["PriField"].ToString().Length > Convert.ToInt32(dtsalesPrintlayout.Rows[i]["PriWidth"].ToString()) ? Convert.ToInt32(dtsalesPrintlayout.Rows[i]["PriWidth"].ToString()) : dtsalesPrintlayout.Rows[i]["PriField"].ToString().Length)) +""+ Convert.ToBoolean(dtsalesPrintlayout.Rows[i]["priKeepLine"].ToString()) == Convert.ToBoolean(0).ToString() ? "" : "<br />");


                                 // graphics.DrawString(Convert.ToString(dtsalesPrintlayout.Rows[i]["PriField"]), new Font(Convert.ToString(dtsalesPrintlayout.Rows[i]["PriFont"]), Convert.ToInt32(dtsalesPrintlayout.Rows[i]["PriFontSize"]), FontStyle.Bold), new SolidBrush(Color.Black), Convert.ToInt32(dtsalesPrintlayout.Rows[i]["PriCol"]), Convert.ToInt32(startY) * Convert.ToInt32(dtsalesPrintlayout.Rows[i]["PriRow"]) + Convert.ToInt32(Offset));
                                  
                            }
                        }
                        if (SalescolumnExist == 0)
                        {

                          //  graphics.DrawString(Convert.ToString(dtsalesPrintlayout.Rows[i]["PriField"].ToString().Substring(0, dtsalesPrintlayout.Rows[i]["PriField"].ToString().Length > Convert.ToInt32(dtsalesPrintlayout.Rows[i]["PriWidth"].ToString()) ? Convert.ToInt32(dtsalesPrintlayout.Rows[i]["PriWidth"].ToString()) : dtsalesPrintlayout.Rows[i]["PriField"].ToString().Length)), new Font(Convert.ToString(dtsalesPrintlayout.Rows[i]["PriFont"]), Convert.ToInt32(dtsalesPrintlayout.Rows[i]["PriFontSize"]), Convert.ToBoolean(dtsalesPrintlayout.Rows[i]["PriBold"]) == Convert.ToBoolean(true) ? FontStyle.Bold : FontStyle.Regular), new SolidBrush(Color.Black), Convert.ToInt32(dtsalesPrintlayout.Rows[i]["PriCol"]), Convert.ToInt32(startY) * Convert.ToInt64(dtsalesPrintlayout.Rows[i]["PriRow"]) + Convert.ToInt32(Offset));
                            result = result.Append("" + Convert.ToString(PriField.ToString().Substring(0, PriField.ToString().Length > Convert.ToInt32(dtsalesPrintlayout.Rows[i]["PriWidth"].ToString()) ? Convert.ToInt32(dtsalesPrintlayout.Rows[i]["PriWidth"].ToString()) : PriField.ToString().Length)));
                            result = result.Append("" + Convert.ToBoolean(dtsalesPrintlayout.Rows[i]["priKeepLine"].ToString()) == Convert.ToBoolean(0).ToString() ? "<br />" : "");
                        }

                    }
                    else
                    {
                        //  MessageBox.Show("No data in sales to print", "POS -Medlife", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }

                }
                else
                {
                    //if (Convert.ToString(dtsalesPrintlayout.Rows[i]["PriSubItem"].ToString()) == Convert.ToString("1"))
                    //{
                    //    //graphics.DrawString(Convert.ToString(dtsales.Rows[0][j].ToString()), new Font(Convert.ToString(dtsalesPrintlayout.Rows[i]["PriFont"]), Convert.ToInt32(dtsalesPrintlayout.Rows[i]["PriFontSize"]), FontStyle.Bold), new SolidBrush(Color.Black), Convert.ToInt32(dtsalesPrintlayout.Rows[i]["PriCol"]), Convert.ToInt32(startY) * Convert.ToInt32(dtsalesPrintlayout.Rows[i]["PriRow"]));
                    //    if (dtsalesdetails.Columns.Count > 0)
                    //    {
                    //        int SalesDetailscolumnExist = 0;
                    //        for (int k = 0; k < dtsalesdetails.Columns.Count; k++)
                    //        {
                    //            string PriFields = dtsalesPrintlayout.Rows[i]["PriField"].ToString();
                    //            if (Convert.ToString(dtsalesPrintlayout.Rows[i]["PriField"].ToString()) == Convert.ToString(dtsalesdetails.Columns[k].ColumnName))
                    //            {
                    //                Offset = 0;
                    //                for (int l = 0; l < dtsalesdetails.Rows.Count; l++)
                    //                {
                    //                    graphics.DrawString(Convert.ToString(dtsalesdetails.Rows[l][k].ToString().Substring(0, dtsalesdetails.Rows[l][k].ToString().Length > Convert.ToInt32(dtsalesPrintlayout.Rows[i]["PriWidth"].ToString()) ? Convert.ToInt32(dtsalesPrintlayout.Rows[i]["PriWidth"].ToString()) : dtsalesdetails.Rows[l][k].ToString().Length)), new Font(Convert.ToString(dtsalesPrintlayout.Rows[i]["PriFont"]), Convert.ToInt32(dtsalesPrintlayout.Rows[i]["PriFontSize"]), Convert.ToBoolean(dtsalesPrintlayout.Rows[i]["PriBold"]) == Convert.ToBoolean(true) ? FontStyle.Bold : FontStyle.Regular), new SolidBrush(Color.Black), Convert.ToInt32(dtsalesPrintlayout.Rows[i]["PriCol"]), Convert.ToInt32(startY) * Convert.ToInt64(dtsalesPrintlayout.Rows[i]["PriRow"]) + Convert.ToInt32(Offset));

                    //                    SalesDetailscolumnExist = 1;
                    //                    Offset = Offset + Convert.ToInt64(20);


                    //                }
                    //            }
                    //        }
                    //        if (SalesDetailscolumnExist == 0)
                    //        {
                    //            graphics.DrawString(Convert.ToString(dtsalesPrintlayout.Rows[i]["PriField"].ToString().Substring(0, dtsalesPrintlayout.Rows[i]["PriField"].ToString().Length > Convert.ToInt32(dtsalesPrintlayout.Rows[i]["PriWidth"].ToString()) ? Convert.ToInt32(dtsalesPrintlayout.Rows[i]["PriWidth"].ToString()) : dtsalesPrintlayout.Rows[i]["PriField"].ToString().Length)), new Font(Convert.ToString(dtsalesPrintlayout.Rows[i]["PriFont"]), Convert.ToInt32(dtsalesPrintlayout.Rows[i]["PriFontSize"]), Convert.ToBoolean(dtsalesPrintlayout.Rows[i]["PriBold"]) == Convert.ToBoolean(true) ? FontStyle.Bold : FontStyle.Regular), new SolidBrush(Color.Black), Convert.ToInt32(dtsalesPrintlayout.Rows[i]["PriCol"]), Convert.ToInt32(startY) * Convert.ToInt64(dtsalesPrintlayout.Rows[i]["PriRow"]));

                    //        }
                    //    }


                    //}

                }

                //Offset = Offset + Convert.ToInt64(dtsalesPrintlayout.Rows[i]["PriRow"]);
            }

            hdfPrintField.Value = result.ToString();


            BlSettings blsettings = new BlSettings();
            DataTable dtblsttng = new DataTable();

            blsettings.Module = "GEN";
            blsettings.SubModule = "BARPRNT";
            blsettings.FK_Client = Convert.ToInt64(hfClient.Value);

            dtblsttng = blsettings.SelectSettings();
            
            
            //dtblsttng.Rows[0]["Value"] = 2;//Check

            if (dtblsttng.Rows[0]["Value"].ToString() == "2")
             {
                 yourStream = File.CreateText("D:\\BarCodePrint.txt");
                 //yourStream = File.CreateText("C:\\BarCodePrint.txt");//Check
                result = result.Replace("<br />", "" + System.Environment.NewLine);
                yourStream.Write(result);
                yourStream.Close();



                // if (WebClientPrint.ProcessPrintJob(Request))
                // {

                ////////////bool useDefaultPrinter = (true);
                ////////////string printerName = "Bar Code Printer LP-100";

                //////////////full path of the txt file to be printed
                ////////////string txtFilePath = @"D:\BarCodePrint.txt";

                //////////////create a temp file name for our txt file...
                ////////////string fileName = Guid.NewGuid().ToString("N") + System.IO.Path.GetExtension(txtFilePath);

                //////////////Create a PrintFile object with the txt file
                ////////////PrintFile file = new PrintFile(txtFilePath, fileName);
                //////////////Create a ClientPrintJob and send it back to the client!
                ////////////ClientPrintJob cpj = new ClientPrintJob();
                //////////////set file to print...
                ////////////cpj.PrintFile = file;
                //////////////set client printer...
                ////////////if (useDefaultPrinter || printerName == "null")
                ////////////    cpj.ClientPrinter = new DefaultPrinter();
                ////////////else
                ////////////    cpj.ClientPrinter = new InstalledPrinter(printerName);
                //////////////send it...
                ////////////cpj.SendToClient(Response);

                //}


                Process printjob = new Process();

                printjob.StartInfo.FileName = @"D:\BarCodePrint.txt"; //path of your file;
                //printjob.StartInfo.FileName = @"C:\BarCodePrint.txt"; //path of your file;
                printjob.StartInfo.Verb = "Print";

                printjob.StartInfo.CreateNoWindow = true;

                printjob.StartInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;

                PrinterSettings setting = new PrinterSettings();
                setting.PrinterName = "Generic / Text Only";
                setting.Collate = true;

                setting.DefaultPageSettings.Landscape = true;
                // ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "alert('" + setting + "')", true);


                printjob.Start();




            
            }
            else
            {


                ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", " PrintButtonClick();", true);
            
            }


           

         // SReportFileName = "D:/BarCodePrint.txt"; // temp/mypdf.pdf 


               

        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "toastr.warning('" + ex.Message + "')", true);
        }
    }
}