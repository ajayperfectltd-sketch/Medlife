/*-------------------------------------------------------------------
Purpose    : BackUp database
Author     : BIBIN TC
Copyright  : Perfect Software Solutions (Clt) Pvt Ltd
Created on : 03/05/2018
----------------------------------------------------------------------*/
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
using System.Globalization;
using System.Threading;
using System.Net;

public partial class DatabaseBackup : System.Web.UI.Page
{
    #region Functions
    protected void CheckUserAccess()
    {
        BlUserAccess bluseraccess = new BlUserAccess();
        Populate populate = new Populate();

        bluseraccess.FK_Users = Convert.ToInt64(hfUser.Value);
        bluseraccess.FK_Pages = 62;
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
    protected void SelectDBBackup(int currentPage)
    {
        DataTable dtbl;
        BlSettings blsettings = new BlSettings();
        dtbl = blsettings.SelectDBBackup(currentPage, 10, Convert.ToInt64(hfClient.Value));
        gvLog.DataSource = dtbl;
        gvLog.DataBind();
        if (dtbl.Rows.Count > 0)
        {
            int recordCount = Convert.ToInt32(dtbl.Rows[0]["RecordCount"].ToString());
            this.PopulatePager(recordCount, currentPage);
        }
        blsettings = null;
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
    private void Download(string filename, string location)
    {
        //string filePath="E:/WebPharma";
        FtpWebRequest reqFTP;
        try
        {            
            //filePath = <<The full path where the file is to be created. the>>, 
            //fileName = <<Name of the file to be createdNeed not name on FTP server. name name()>>
            FileStream outputStream = new FileStream(location + "\\" + filename, FileMode.Create);

            reqFTP = (FtpWebRequest)FtpWebRequest.Create(new Uri("ftp://" + "arthome-stores.com" + "/httpdocs/db_backup/" + "Presentation1.ppt"));
            reqFTP.Method = WebRequestMethods.Ftp.DownloadFile;
            reqFTP.UseBinary = true;
            reqFTP.Credentials = new NetworkCredential("artPssHme", "ArtFtpHme@0101");
            FtpWebResponse response = (FtpWebResponse)reqFTP.GetResponse();
            Stream ftpStream = response.GetResponseStream();
            long cl = response.ContentLength;
            int bufferSize = 2048;
            int readCount;
            byte[] buffer = new byte[bufferSize];

            readCount = ftpStream.Read(buffer, 0, bufferSize);
            while (readCount > 0)
            {
                outputStream.Write(buffer, 0, readCount);
                readCount = ftpStream.Read(buffer, 0, bufferSize);
            }

            ftpStream.Close();
            outputStream.Close();
            response.Close();
        }
        catch (Exception ex)
        {
            //MessageBox.Show(ex.Message);
        }
    }
    protected void BindData()
    {
        BlSettings blsettings = new BlSettings();
        txtBackupLocation.Text = blsettings.GetLastBackupLocation();
        blsettings = null;
    }
    #endregion

    #region Events
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                hfUser.Value = Request.QueryString["uid"];
                hfClient.Value = Request.QueryString["clnt"];
                CheckUserAccess();
                SelectDBBackup(1);
                BindData();
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
            int result;
            BlSettings blsettings = new BlSettings();
            result = blsettings.CreateDBBackup(txtBackupLocation.Text.Trim()+"\\",hfUser.Value,Convert.ToInt64(hfClient.Value));
            if (result > 0)
            {
                SelectDBBackup(1);
                BindData();
                ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "toastr.success('Database backup successful');", true);               
            }
            else
            {
                ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "toastr.error('Database backup failed');", true);
            }
            blsettings = null;
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
    protected void gvLog_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            GridViewRow gvr = (GridViewRow)((Control)e.CommandSource).NamingContainer;
            int rowIndex = gvr.RowIndex;
            if (e.CommandName == "download")
            {
                hfFilename.Value = gvLog.Rows[rowIndex].Cells[2].Text;
                MpeDownloadLocation.Show();
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "toastr.warning('" + ex.Message + "')", true);
        }
    }
    protected void btnSubmitDownload_Click(object sender, EventArgs e)
    {
        try
        {
            Download(hfFilename.Value.Trim(), txtDownloadLocation.Text.Trim());
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "toastr.warning('" + ex.Message + "')", true);
        }
    }
    #endregion           
}