/*----------------------------------------------------------------------------------------------------------------------------------------------------------
Purpose    : Insert/Update/Delete Users
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
using WebPharma.Business.Masters;
using System.Data;
using System.IO;
using System.Text;
using System.Security.Cryptography;
using System.Configuration;
using System.Web.Security;
using System.Collections;
using System.Xml;
using System.Globalization;
using AjaxControlToolkit;
using System.Drawing;
using System.Drawing.Imaging;

public partial class Users : System.Web.UI.Page
{
    private const string Image = "Image";
    private const string DefaultImage = "~/UploadedItems/DefaultImage.jpg";
   
    private const string UploadedPath = "~\\UploadedItems\\Users\\";
    private static Byte[] PhotoData=null;
    private static Byte[] PhotoDataSmall;
    private static string UserImage = "CandImage";
    private static string UserImageSmall = "CandImageSmall";
    private static string TempPath = "";
    private const string ID_USER = "ID_User";

    public Int64 ID_User
    {
        get { return Convert.ToInt64(hfMasterId.Value); }
        set { hfMasterId.Value = value.ToString(); }
    }

    #region Functions
    protected void BindData()
    {
        Populate populate = new Populate();
        populate.FillDropdownList(ddlCustomer, "Customer", "CustomerName", "ID_Customer", "ID_Customer", "Active='True'  and Cancelled=0 AND FK_Client=" + hfClient.Value, "");
       // populate.FillDropdownList(ddlSearchCustomer, "Customer", "CustomerName", "ID_Customer", "ID_Customer", "Active='True' AND IsEmployee=1 and Cancelled=0 AND FK_Client=" + hfClient.Value, "");
        //populate.FillDropdownList(ddlEmployee, "Employee", "Name", "ID_Employee", "ID_Employee", "Active='True'  AND FK_Client=" + hfClient.Value, "");
       // populate.FillDropdownList(ddlSearchEmployee, "Employee", "Name", "ID_Employee", "ID_Employee", "FK_Client=" + hfClient.Value, "");
        populate.FillDropdownList(ddlUserGroup, "UserGroup", "GroupName", "ID_UserGroup", "ID_UserGroup", "Active='True' and Cancelled=0  AND FK_Client=" + hfClient.Value, "");
        populate.FillDropdownList(ddlSearchUserGroup, "UserGroup", "GroupName", "ID_UserGroup", "ID_UserGroup", "Active='True' and Cancelled=0 AND FK_Client=" + hfClient.Value, "");
        populate.FillDropdownList(ddlStore, "Store", "StoName", "ID_Store", "StoName", " Active='True' and Cancelled=0  and FK_Client = " + hfClient.Value, "");
        populate = null;
    }
    protected void GridFill(int currentPage)
    {
        BlUsers blusers = new BlUsers();
        DataTable dtbl = new DataTable();
        //blusers.FK_Employee = Convert.ToInt64(ddlSearchEmployee.SelectedValue.ToString());
        //blusers.FK_Customer = Convert.ToInt64(ddlSearchCustomer.SelectedValue.ToString());
        blusers.FK_UserGroup = Convert.ToInt64(ddlSearchUserGroup.SelectedValue.ToString());
        blusers.Username = txtSearchUsername.Text.Trim().ToLower();
        blusers.FK_Client = Convert.ToInt64(hfClient.Value);
        blusers.UserCode = hfUser.Value;
        blusers.PageIndex = Convert.ToInt16(currentPage);
        dtbl = blusers.SelectAllData();
        gvUsers.DataSource = dtbl;
        gvUsers.DataBind();
        //if (dtbl.Rows.Count > 0)
        //{
        //    int recordCount = Convert.ToInt32(dtbl.Rows[0]["RecordCount"].ToString());
        //    this.PopulatePager(recordCount, currentPage);
        //}
        blusers = null;
    }
    protected void CheckUserAccess()
    {
        BlUserAccess bluseraccess = new BlUserAccess();
        Populate populate = new Populate();

        bluseraccess.FK_Users = Convert.ToInt64(hfUser.Value);
        bluseraccess.FK_Pages = 13;
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
    protected String EncryptData(string strPassword)
    {
        string tempPass;
        tempPass = "";
        tempPass = FormsAuthentication.HashPasswordForStoringInConfigFile(strPassword, "SHA1");
        return tempPass;
    }
    private string Encrypt(string clearText)
    {
        string EncryptionKey = "P321PLATINUM123CLT";
        byte[] clearBytes = Encoding.Unicode.GetBytes(clearText);
        using (Aes encryptor = Aes.Create())
        {
            Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
            encryptor.Key = pdb.GetBytes(32);
            encryptor.IV = pdb.GetBytes(16);
            using (MemoryStream ms = new MemoryStream())
            {
                using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateEncryptor(), CryptoStreamMode.Write))
                {
                    cs.Write(clearBytes, 0, clearBytes.Length);
                    cs.Close();
                }
                clearText = Convert.ToBase64String(ms.ToArray());
            }
        }
        return clearText;
    }
    protected void SaveData()
    {
        string OldPassword = "";
        //IUserPhoto IuserPhoto = new IUserPhoto();
        long status = 0;
        BlUsers blusers = new BlUsers();
        blusers.MasterId = ID_User;
        blusers.FK_Customer = Convert.ToInt64(ddlCustomer.SelectedValue.ToString());
        // blusers.FK_Employee = Convert.ToInt64(ddlEmployee.SelectedValue.ToString());
        blusers.FK_UserGroup = Convert.ToInt64(ddlUserGroup.SelectedValue.ToString());
        blusers.FK_Store = Convert.ToInt64(ddlStore.SelectedValue.ToString());
        blusers.Name = txtName.Text;
        blusers.Username = txtUsername.Text.Trim().ToLower();
        if (ID_User > 0)
        {
            OldPassword = blusers.SelectPassword();
        }

        blusers.Password = txtPassword.Text.Trim() == "" ? OldPassword : Encrypt(txtPassword.Text.Trim());
        blusers.Active = Convert.ToBoolean(chkActive.Checked);
        blusers.FK_Client = Convert.ToInt64(hfClient.Value);
        blusers.UserCode = hfUsername.Value;
        blusers.EnteredOn = DateTime.Now;
        //UploadImages();

        blusers.Photo = UserImage.Split(',')[0];

        if (PhotoData == null)
        {
            blusers.PhotoData = Convert.FromBase64String("");
            blusers.Photo = "";
        }
        else
        {
            blusers.Photo = UserImage.Split(',')[0];
        }
        blusers.PhotoData = PhotoData;
        if (hfUserId.Value == "0")
        {

            if (hfIsAdd.Value.ToLower() == "true")
            {
                status = blusers.InsertData();
                if (status > 0)
                {
                    GridFill(1);
                    ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "ShowAddDiv();", true);
                    ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "toastr.success('Data inserted successfully');ClearAdd();", true);
                    PhotoData = null;

                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "ShowAddDiv();", true);
                    if (status == -3)
                        ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "toastr.error('Cannot insert data, Maximum users limit reached')", true);
                    else
                        ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "toastr.error('Cannot insert data, Username already exists')", true);
                    txtUsername.Focus();
                }
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "toastr.warning('User does not have permission to add')", true);


            }
        }
        else
        {
            blusers.MasterId = Convert.ToInt64(hfUserId.Value);
            if (hfIsUpdate.Value.ToLower() == "true")
            {

                status = blusers.UpdateData();
                if (status > 0)
                {
                    GridFill(1);
                    ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "ShowAddDiv();", true);
                    ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "toastr.success('Data updated successfully');ClearAdd();", true);
                    PhotoData = null;
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "ShowAddDiv();", true);

                    ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "toastr.error('Cannot update data, Username already exists')", true);
                    txtUsername.Focus();
                }
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "toastr.warning('User does not have permission to Update')", true);

            }
        }
         lbtnRemoveAttachment.Attributes.Add("style", "display:none");
        blusers = null;

        if (status > 0)
        {
            chkReset.Visible = false;
            txtPassword.Enabled = true;
            RequiredFieldValidator3.Enabled = true;
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

                if (Session["Username"] != null && Convert.ToString(Session["Username"]) != "")
                {

                    hfUsername.Value = Session["Username"].ToString();

                    Page.Header.DataBind();
                    //blMessage.Items.Clear();
                    ddlCustomer.Focus();
                    ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "ClearAdd();", true);
                    divAdd.Style.Add("display", "block");
                    // divImage.Style.Add("display", "block");
                    hfUser.Value = Request.QueryString["uid"];
                    hfClient.Value = Request.QueryString["clnt"];
                    CheckUserAccess();
                    BindData();
                    // rfvCustomer.Enabled = false;
                    ddlCustomer.Focus();
                    RequiredFieldValidator3.Enabled = true;
                    lbtnRemoveAttachment.Attributes.Add("style", "display:none");
                }
                else
                {
                    Response.Redirect("~/Default.aspx", false);
                }
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "toastr.warning('" + ex.Message + "')", true);
        }
    }
    protected void Page_Changed(object sender, EventArgs e)
    {
        try
        {
            //blMessage.Items.Clear();
            int pageIndex = int.Parse((sender as LinkButton).CommandArgument);
            GridFill(pageIndex);
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "toastr.warning('" + ex.Message + "')", true);
        }
    }
    protected void chkResetChange_Click(Object sender, EventArgs e)
    {
        if (chkReset.Checked == true)
        {
            txtPassword.Enabled = true;
            RequiredFieldValidator3.Enabled = true;
        }
        else
        {
            txtPassword.Enabled = false;
        }
    }
    private void UploadImages()
    {
        if (hfUserImage.Value != "")
        {
            FileInfo infoFile = new FileInfo(Server.MapPath(TempPath + hfUserImage.Value));
            if (infoFile.Exists)
            {
                infoFile.MoveTo(Server.MapPath(UploadedPath + hfUserImage.Value));

                DirectoryInfo info = new DirectoryInfo(Server.MapPath("~\\UploadedItems\\Temp"));
                foreach (FileInfo files in info.GetFiles())
                {
                    try
                    {
                        files.Delete();
                    }
                    catch (Exception ex)
                    {
                        ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "toastr.warning('" + ex.Message + "')", true);
                    }
                }
            }
        }
    }
    protected void DropTempImage()
    {
        string fileName = HttpContext.Current.Server.MapPath(UploadedPath + "Small_" + TempPath);
        if ((System.IO.File.Exists(fileName)))
        {
            System.IO.File.Delete(fileName);
        }
        fileName = HttpContext.Current.Server.MapPath(UploadedPath + TempPath);
        if ((System.IO.File.Exists(fileName)))
        {
            System.IO.File.Delete(fileName);
        }

    }
    protected void btnHiddenImage_Click(object sender, EventArgs e)
    {
        // imgCandidate.ImageUrl = Session["ImageUrl"].ToString();
        hfUserImage.Value = UserImage;
        if (hfUserImage.Value != "")
        {
            imgAttachment.ImageUrl = TempPath + hfUserImage.Value.Split(',')[0];
            imgAttachment.ImageUrl = UploadedPath + hfUserImage.Value.Split(',')[0];
        }
        else
        {
            imgAttachment.ImageUrl = DefaultImage;
        }
        //txtWrkExperience.Focus();
    }
    protected void btnClear_Click(object sender, EventArgs e)
    {
        Clear();
    }
    protected void lbtnRemoveAttachment_Click(object sender, EventArgs e)
    {
        try
        {
            BlUsers blusers = new BlUsers();
            imgAttachment.ImageUrl = DefaultImage;
            hfUserImage.Value = "";
            fupImage.Visible = true;
            lbtnRemoveAttachment.Attributes.Add("style", "display:none");
            blusers.Photo = null;
            PhotoData = null;
            //  ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "ShowAddDiv();", true);

        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "toastr.warning('" + ex.Message + "')", true);
            // Populate.WarningMsg(this, ex.ToString());
        }
    }
    protected void fupImage_UploadedComplete(object sender, AjaxControlToolkit.AsyncFileUploadEventArgs e)
    {
        try
        {
            AsyncFileUpload fupUserPhoto = (AsyncFileUpload)sender;
            AsyncFileUpload fupUserPhotoSmall = (AsyncFileUpload)sender;

            if (fupUserPhoto.HasFile)
            {
                string attachName = fupUserPhoto.PostedFile.FileName;
                string attachNameSmall = fupUserPhoto.PostedFile.FileName;
                if (fupUserPhoto.FileContent.Length <= 3000000)
                {

                    //bool status;
                    string filext = System.IO.Path.GetExtension(fupUserPhoto.FileName).ToLower();
                    if (filext == ".jpg" || filext == ".jpeg" || filext == ".gif" || filext == ".bmp" || filext == ".png" || filext == ".jpe" || filext == ".jfif" || filext == ".tif" || filext == ".tiff")
                    {

                        Session[UserImage] = "User_" + DateTime.Now.Ticks.ToString() + filext;
                        TempPath = hfUserImage.Value;
                        hfUserImage.Value = Session[UserImage].ToString();
                        UserImage = hfUserImage.Value;
                        UserImageSmall = "Small_" + hfUserImage.Value;
                        string saveAsfilePathName = Server.MapPath(UploadedPath + hfUserImage.Value);
                        string saveAsfilePathNamesmall = Server.MapPath(UploadedPath + "Small_" + hfUserImage.Value);

                        if (!File.Exists(saveAsfilePathName))
                        {
                            fupUserPhoto.PostedFile.SaveAs(saveAsfilePathName);
                            ResizeImage(30, saveAsfilePathName, saveAsfilePathNamesmall);

                            //record the file name 
                            Session["FileName"] = attachName;
                            string filetype = string.Empty;
                            Stream str = fupUserPhoto.PostedFile.InputStream;
                            BinaryReader br = new BinaryReader(str);
                            PhotoData = br.ReadBytes(Convert.ToInt32(str.Length));
                            imgAttachment.ImageUrl = "data:image/png;base64," + Convert.ToBase64String(PhotoData);
                            string path = HttpContext.Current.Server.MapPath(UploadedPath + "Small_" + hfUserImage.Value);
                            PhotoDataSmall = ImageToByteArray(path);

                        }
                    }

                    else
                    {
                        imgAttachment.ImageUrl = DefaultImage;
                        hfUserImage.Value = "";
                        //  Image = hfCanImage.Value;
                        ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "alert('Invalid File Format')", true);
                        return;
                    }
                    ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "AddFocus()", true);
                }
                else
                {
                    imgAttachment.ImageUrl = null;
                    imgAttachment.ImageUrl = DefaultImage;

                    hfUserImage.Value = "";
                    // Image = hfCanImage.Value;
                    ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "alert('Image size cannot be greater than 3 MB');", true);
                    return;
                }
            }

        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "toastr.warning('" + ex.Message + "')", true);
        }
    }
    protected void SelectUserPhoto(BlUsers blusers)
    {
        IUserPhoto iuserPhoto = new IUserPhoto();
        DataTable dtblEmployeePhoto = iuserPhoto.SelectUserPhoto(blusers.MasterId);
        if (dtblEmployeePhoto.Rows.Count > 0)
        {
            hfUserImage.Value = dtblEmployeePhoto.Rows[0]["Photo"].ToString();
            UserImage = dtblEmployeePhoto.Rows[0]["Photo"].ToString();
            UserImageSmall = "Small_" + dtblEmployeePhoto.Rows[0]["Photo"].ToString();
        }
        if (hfUserImage.Value != string.Empty)
        {
            PhotoData = (Byte[])(dtblEmployeePhoto.Rows[0]["PhotoData"]);

            string strPhoto = Server.MapPath("~\\UploadedItems\\DataBaseTemp\\" + hfUserImage.Value);
            FileInfo infoFile = new FileInfo(Server.MapPath("~\\UploadedItems\\DataBaseTemp\\" + hfUserImage.Value));
            if (infoFile.Exists)
            {
                DirectoryInfo info = new DirectoryInfo(Server.MapPath("~\\UploadedItems\\DataBaseTemp"));
                foreach (FileInfo files in info.GetFiles())
                {
                    try
                    {
                        files.Delete();
                    }
                    catch (Exception ex)
                    {
                        ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "toastr.warning('" + ex.Message + "')", true);
                    }
                }
            }
            FileStream fs = new FileStream(strPhoto, FileMode.CreateNew, FileAccess.Write);
            fs.Write(PhotoData, 0, PhotoData.Length);
            fs.Flush();
            fs.Close();
            imgAttachment.ImageUrl = "~\\UploadedItems\\DataBaseTemp\\" + hfUserImage.Value;
            lbtnRemoveAttachment.Attributes.Add("style", "display:block");
        }
        else
        {
            hfUserImage.Value = "";
            imgAttachment.ImageUrl = DefaultImage;
            lbtnRemoveAttachment.Attributes.Add("style", "display:none");
        }
        //if (PhotoData == null)
        //{

        //}
        //else
        //{

        //}

    }
    public static byte[] ImageToByteArray(string imagefilePath)
    {
        System.Drawing.Image image = System.Drawing.Image.FromFile(imagefilePath);
        byte[] imageByte = ImageToByteArraybyImageConverter(image);
        return imageByte;
    }
    private static byte[] ImageToByteArraybyImageConverter(System.Drawing.Image image)
    {
        ImageConverter imageConverter = new ImageConverter();
        byte[] imageByte = (byte[])imageConverter.ConvertTo(image, typeof(byte[]));
        return imageByte;
    }
    public void ResizeImage(int size, string filePath, string saveFilePath)// Created  for resizing image 
    {
        //variables for image dimension/scale
        double newHeight = 0;
        double newWidth = 0;
        double scale = 0;
        //create new image object
        Bitmap curImage = new Bitmap(filePath);

        //Determine image scaling
        if (curImage.Height > curImage.Width)
        {
            scale = Convert.ToSingle(size) / curImage.Height;
        }
        else
        {
            scale = Convert.ToSingle(size) / curImage.Width;
        }
        if (scale < 0 || scale > 1) { scale = 1; }

        //New image dimension
        newHeight = 20; //Math.Floor(Convert.ToSingle(curImage.Height) * scale);
        newWidth = 25;// Math.Floor(Convert.ToSingle(curImage.Width) * scale);

        //Create new object image
        Bitmap newImage = new Bitmap(curImage, Convert.ToInt32(newWidth), Convert.ToInt32(newHeight));
        Graphics imgDest = Graphics.FromImage(newImage);
        imgDest.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
        imgDest.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
        imgDest.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.HighQuality;
        imgDest.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
        ImageCodecInfo[] info = ImageCodecInfo.GetImageEncoders();
        EncoderParameters param = new EncoderParameters(1);
        param.Param[0] = new EncoderParameter(System.Drawing.Imaging.Encoder.Quality, 100L);

        //Draw the object image
        imgDest.DrawImage(curImage, 0, 0, newImage.Width, newImage.Height);

        //Save image file
        newImage.Save(saveFilePath, info[1], param);

        //Dispose the image objects
        curImage.Dispose();
        newImage.Dispose();
        imgDest.Dispose();
    }
    protected void imgbtnAdd_Click(object sender, ImageClickEventArgs e)
    {
        // divAdd.Style.Add("display", "block");
        // divView.Style.Add("display", "block");
        PhotoData = null;
        lbtnRemoveAttachment.Attributes.Add("style", "display:none");
        RequiredFieldValidator3.Enabled = true;
        chkReset.Visible = false;
        txtPassword.Enabled = true;

        ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "ClearAdd();ShowAddDiv();", true);
        ID_User = 0;
      
    }
    protected void imgbtnView_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            //blMessage.Items.Clear();
            PhotoData = null;
            divAdd.Style.Add("display", "none");
           // divImage.Style.Add("display", "none");
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
            ddlSearchUserGroup.Focus();
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "toastr.warning('" + ex.Message + "')", true);
        }
    }
    protected void gvUsers_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            //blMessage.Items.Clear();
            string confirmValue = Request.Form["confirm_value"];
            if (confirmValue.Length > 3)
                confirmValue = confirmValue.Substring(confirmValue.Length - 3, 3);
            if (confirmValue == "Yes")
            {

                hfMasterId.Value = gvUsers.DataKeys[e.RowIndex]["ID_Users"].ToString();

            }
            else { mpDelete.Hide(); }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "toastr.warning('" + ex.Message + "')", true);
        }
    }
    protected void gvUsers_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        try
        {
            RequiredFieldValidator3.Enabled = false;
            txtPassword.Enabled = false;
            chkReset.Visible = true;
            chkReset.Checked = false;
            
            //blMessage.Items.Clear();
            ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "ShowAddDiv();", true);
            BlUsers blusers = new BlUsers();
            blusers.MasterId = Convert.ToInt64(gvUsers.DataKeys[e.NewSelectedIndex]["ID_Users"].ToString());
            ID_User = Convert.ToInt64(gvUsers.DataKeys[e.NewSelectedIndex]["ID_Users"].ToString());
            hfUserId.Value = blusers.MasterId.ToString();
            blusers.FK_Client = Convert.ToInt64(hfClient.Value);
            blusers.UserCode = hfUser.Value;
            SelectUserPhoto(blusers);
            blusers.SelectData();
            try
            {
                ddlCustomer.SelectedValue = blusers.FK_Customer.ToString();
            }
            catch (Exception)
            {
                ddlCustomer.SelectedValue = "0";
            }
           
            try
            {
                ddlUserGroup.SelectedValue = blusers.FK_UserGroup.ToString();
            }
            catch (Exception)
            {
                ddlUserGroup.SelectedValue = "0";
            }



            try
            {
                ddlStore.SelectedValue = blusers.FK_Store.ToString();
            }
            catch (Exception)
            {
                ddlStore.SelectedValue = "0";
            }
            txtName.Text = blusers.Name;
            txtUsername.Text = blusers.Username;
            chkActive.Checked = blusers.Active;
            blusers = null;
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "toastr.warning('" + ex.Message + "')", true);
        }
        //divImage.Visible = true;
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
         
            //blMessage.Items.Clear();
            DataTable dtbl = new DataTable();

            Common common = new Common();
            dtbl = common.SelectPasswordLength();
            if (dtbl.Rows.Count > 0)
            {
                hfSEC01.Value = dtbl.Rows[0]["Value"].ToString();
                hfSEC02.Value = dtbl.Rows[1]["Value"].ToString();
            }
            if (((Convert.ToInt64(hfSEC01.Value)) <= txtPassword.Text.Length) && (txtPassword.Text.Length <= (Convert.ToInt64(hfSEC02.Value))) || txtPassword.Text == "")
            {
                //if (hfUserId.Value == "0") //Check License
                //{
                //    Licensing.CheckUserLicense();
                //}
                SaveData();
               
                //imgAttachment.ImageUrl = "";
                PhotoData = null;
            }
            else 
            {
                ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "toastr.error('Minimum Password length is " + hfSEC01.Value + " and Maximum Password length is " + hfSEC02.Value + "');ShowAddDiv();", true);
                //chkReset.Visible = true;
                //chkReset.Checked = true;
                txtPassword.Enabled = true;
            }
            //if (imgAttachment.ImageUrl == "")
            //{
            //    Clear();
            //}
            ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "ShowAddDiv();", true);
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "toastr.warning('" + ex.Message + "')", true);
        }
       
    }

    private void Clear()
    {
        lbtnRemoveAttachment.Attributes.Add("style", "display:none");
        imgAttachment.ImageUrl = DefaultImage;

        PhotoData = null;
        PhotoDataSmall = null;
        UserImage = "";
        //UserImageSmall = "";
        RequiredFieldValidator3.Enabled = true;
        chkReset.Visible = false;
        txtPassword.Enabled = true;
        txtUsername.Text = "";
        txtPassword.Text = "";
        ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "ShowAddDiv();", true);
    }
    #endregion
    protected void gvUsers_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvUsers.PageIndex = e.NewPageIndex;
        GridFill(e.NewPageIndex);
    }
    protected void ImageBtnDelete_Click(object sender, ImageClickEventArgs e)
    {
        Delete();
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
            BlUsers blusers = new BlUsers();
            blusers.MasterId = Convert.ToInt64(hfMasterId.Value);
            blusers.FK_Client = Convert.ToInt64(hfClient.Value);
            blusers.UserCode = hfUsername.Value;
            blusers.FK_Reason = Convert.ToInt64(Reason);
            if (hfIsDelete.Value.ToLower() == "true")
            {
                status = blusers.DeleteData();
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
            blusers = null;
        }



    }
}