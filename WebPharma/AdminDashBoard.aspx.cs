using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebPharma.Business;
using WebPharma.Business.Masters;

public partial class AdminDashBoard : System.Web.UI.Page
{
    private void Search()
    {
        
        DataTable dtbl = AccessRight();

        if (dtbl.Rows.Count > 0)
        {
           
            divSearch.Visible = true;
            for (int i = 0; i < dtbl.Rows.Count; i++)
            {
                switch (Convert.ToInt64(dtbl.Rows[i][0]))
                {
                    case 1: column2.Visible = true;
                        hdfCustomer.Value = GetCustomer();
                        break;
                    case 2: column3.Visible = true;
                        hdfSale.Value = GetStore();
                        break;
                    case 3: column1.Visible = true;
                        hdfCounter.Value = GetItems();
                        break;
                    case 4: column4.Visible = true;
                        hdfMinStock.Value = GetMinimuStock();
                        break;

                }
            }

        }



    }
    private DataTable AccessRight()
    {
        BlDashBoard bldashboard = new BlDashBoard();
        bldashboard.FK_Users = Convert.ToInt64(hfUser.Value.Split(',')[0]);
        bldashboard.FK_Client = Convert.ToInt64(hfClient.Value.Split(',')[0]);

        BlReportData IReportData = new BlReportData();
        if (Convert.ToByte(ddlType.SelectedValue) == 6)
        {
            IReportData.FromDate = DateTime.Parse(txtFromDate.Text);
            IReportData.ToDate = DateTime.Parse(txtToDate.Text);

        }
        else
        {
            IReportData.FromDate = DateTime.Parse(txtDate.Text);
            IReportData.ToDate = DateTime.Parse("1/1/1900");

        }
        IReportData.SubID = "NOSL";
        IReportData.Period = Convert.ToByte(ddlType.SelectedValue);
        DataTable dts = IReportData.GetGraphDetail();
        hdfPeriod.Value = hdfPeriod.Value = ddlType.SelectedValue == "0" ? IReportData.FromDate.ToString("MMM-yyyy") : dts.Rows[0][0].ToString(); 
        IReportData = null;


        return bldashboard.DashBoardAccessRight();

    }
    private void ClearSearch()
    {
        ddlItemStore.SelectedIndex = 0;
        ddlType.SelectedIndex = 0;
        txtFromDate.Text = DateTime.Parse(txtDate.Text).ToString("dd/MM/yyyy");
        txtToDate.Text = DateTime.Parse(txtDate.Text).ToString("dd/MM/yyyy");
       
        Search();


    }
    private void BindData()
    {
        Populate populate = new Populate();
        //populate.FillDropdownList(ddlItemStore, "Store", "StoName", "ID_Store", "ID_Store", "Active='True' AND Cancelled=0 AND FK_Client= " + hfClient.Value.Split(',')[0], "");
        populate.FillDropdownList(ddlItemStore, "Store", "StoName", "ID_Store", "ID_Store", "FK_Client=" + hfClient.Value + "AND Cancelled=0", "");
        populate = null;
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            hfUser.Value = Request.QueryString["uid"];
            hfClient.Value = Request.QueryString["clnt"];
            txtFromDate.Attributes.Add("readonly", "readonly");
            txtToDate.Attributes.Add("readonly", "readonly");
          //  hfusername.Value = Encrypt(HttpUtility.UrlDecode(Request.QueryString["nme"]));
            //hfClient.Value = Encrypt(HttpUtility.UrlDecode(Request.QueryString["clnt"]));
           // Hflstin.Value = Encrypt(HttpUtility.UrlDecode(Request.QueryString["lstin"]));
           // lstpwd.Value = Encrypt(HttpUtility.UrlDecode(Request.QueryString["lstpwd"]));
            //hfUser.Value = Encrypt(HttpUtility.UrlDecode(Session["uid"].ToString()));
           // hfUserType.Value = Encrypt(HttpUtility.UrlDecode(Request.QueryString["utyp"]));
            //int chgPassword = Convert.ToInt32(Request.QueryString["chgpwd"].ToString());
            //if (chgPassword == 1)
            //{
            //    ScriptManager.RegisterStartupScript(Page, typeof(Page), "Script", "ShowPwdMsg();", true);
            //}
            hfDecriptUid.Value = Encrypt(HttpUtility.UrlDecode(Request.QueryString["uid"]));
            hfDeclnt.Value = Encrypt(HttpUtility.UrlDecode(Request.QueryString["clnt"]));
            BindData();
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "script", "ClearSearch();", true);

            //hdfCounter.Value = GetGraphDetail();

        }

    }
    private string Decrypt(string cipherText)
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
    public string GetCustomer()
    {
        string str77 = txtDate.Text;
        BlReportData IReportData = new BlReportData();
        IReportData.SubID = "CUST";
        
        if (Convert.ToByte(ddlType.SelectedValue) == 6)
        {
            IReportData.FromDate = DateTime.Parse(txtFromDate.Text);
            IReportData.ToDate = DateTime.Parse(txtToDate.Text);

        }
        else
        {
            IReportData.FromDate = DateTime.Parse(txtDate.Text);
            IReportData.ToDate = DateTime.Parse("1/1/1900");

        }


        //IReportData.ToDate = new DateTime(IReportData.FromDate.Year, IReportData.FromDate.Month, DateTime.DaysInMonth(IReportData.FromDate.Year, IReportData.FromDate.Month));
        IReportData.FK_Client = Convert.ToInt64(hfClient.Value.Split(',')[0]);
        IReportData.FK_Store = Convert.ToInt64(ddlItemStore.SelectedValue);
        IReportData.Period = Convert.ToByte(ddlType.SelectedValue);
        DataTable dtbl = IReportData.GetGraphDetail();


        int i = 1;
        string str = "[";
        foreach (DataRow dtRow in dtbl.Rows)
        {
            if (i < dtbl.Rows.Count)
            {
                str += "\n {amount:'" + dtRow[2].ToString() + "',y:" + dtRow[1].ToString() + ",name:'" + dtRow[0].ToString() + "'},\n";
            }
            else
            {
                str += "\n {amount:'" + dtRow[2].ToString() + "',y:" + dtRow[1].ToString() + ",name:'" + dtRow[0].ToString() + "'}";
            }
            i++;
        }
        str += "]";
        return str;

        /*['Store1', 45.0],
                        ['Store2', 26.8],
                        {
                            name: 'WholeSale Shop',
                            y: 12.8,
                            sliced: true,
                            selected: true
                        },
                        ['Store3', 8.5],
                        ['Store4', 6.2],
                        ['Store5', 0.7]*/
    }

    public string GetStore()
    {
        string str77 = txtDate.Text;
        BlReportData IReportData = new BlReportData();
        IReportData.SubID = "STR";
        if (Convert.ToByte(ddlType.SelectedValue) == 6)
        {
            IReportData.FromDate = DateTime.Parse(txtFromDate.Text);
            IReportData.ToDate = DateTime.Parse(txtToDate.Text);

        }
        else
        {
            IReportData.FromDate = DateTime.Parse(txtDate.Text);
            IReportData.ToDate = DateTime.Parse("1/1/1900");

        }

        IReportData.FK_Store = Convert.ToInt32(ddlItemStore.SelectedValue);
        //// IReportData.ToDate = new DateTime(IReportData.FromDate.Year, IReportData.FromDate.Month, DateTime.DaysInMonth(IReportData.FromDate.Year, IReportData.FromDate.Month));
        IReportData.FK_Client = Convert.ToInt64(hfClient.Value.Split(',')[0]);
        IReportData.Period = Convert.ToByte(ddlType.SelectedValue);
        DataTable dtbl = IReportData.GetGraphDetail();
        

        int i = 1;
        string str = "[";
        foreach (DataRow dtRow in dtbl.Rows)
        {
            if (i < dtbl.Rows.Count)
            {
                str += "\n {amount:'" + dtRow[3].ToString() + "',y:" + dtRow[1].ToString() + ",name:'" + dtRow[0].ToString() + "'},\n";
            }
            else
            {
                str += "\n {amount:'" + dtRow[3].ToString() + "',y:" + dtRow[1].ToString() + ",name:'" + dtRow[0].ToString() + "'}";
            }
            i++;
        }
        str += "]";
        return str;



    }

    public string GetItems()
    {
        string str77 = txtDate.Text;
        BlReportData IReportData = new BlReportData();
        IReportData.SubID = "ITM";
        if (Convert.ToByte(ddlType.SelectedValue) == 6)
        {
            IReportData.FromDate = DateTime.Parse(txtFromDate.Text);
            IReportData.ToDate = DateTime.Parse(txtToDate.Text);

        }
        else
        {
            IReportData.FromDate = DateTime.Parse(txtDate.Text);
            IReportData.ToDate = DateTime.Parse("1/1/1900");

        }
        //  IReportData.FromDate = new DateTime(DateTime.Parse(txtDate.Text).Year, DateTime.Parse(txtDate.Text).Month, 1);
        //  IReportData.ToDate = new DateTime(IReportData.FromDate.Year, IReportData.FromDate.Month, DateTime.DaysInMonth(IReportData.FromDate.Year, IReportData.FromDate.Month));
        IReportData.FK_Client = Convert.ToInt64(hfClient.Value.Split(',')[0]);
        IReportData.FK_Store = Convert.ToInt64(ddlItemStore.SelectedValue);
        IReportData.Period = Convert.ToByte(ddlType.SelectedValue);
        DataTable dtbl = IReportData.GetGraphDetail();


        int i = 1;
        string str = "[";
        foreach (DataRow dtRow in dtbl.Rows)
        {
            if (i < dtbl.Rows.Count)
            {
                str += "\n ['" + dtRow[0].ToString() + "'," + dtRow[1].ToString() + "],\n";
            }
            else
            {
                str += "\n ['" + dtRow[0].ToString() + "'," + dtRow[1].ToString() + "]";
            }
            i++;
        }
        str += "]";
        return str;
    }
    public string GetMinimuStock()
    {
        BlReportData IReportData = new BlReportData();
        IReportData.SubID = "MSW";
        if (Convert.ToByte(ddlType.SelectedValue) == 6)
        {
            IReportData.FromDate = DateTime.Parse(txtFromDate.Text);
            IReportData.ToDate = DateTime.Parse(txtToDate.Text);

        }
        else
        {
            IReportData.FromDate = DateTime.Parse(txtDate.Text);
            IReportData.ToDate = DateTime.Parse("1/1/1900");

        }
        //  IReportData.FromDate = new DateTime(DateTime.Parse(txtDate.Text).Year, DateTime.Parse(txtDate.Text).Month, 1);
        //  IReportData.ToDate = new DateTime(IReportData.FromDate.Year, IReportData.FromDate.Month, DateTime.DaysInMonth(IReportData.FromDate.Year, IReportData.FromDate.Month));
        IReportData.FK_Client = Convert.ToInt64(hfClient.Value.Split(',')[0]);
        IReportData.FK_Store = Convert.ToInt64(ddlItemStore.SelectedValue);
        IReportData.Period = Convert.ToByte(ddlType.SelectedValue);
        DataTable dtbl = IReportData.GetGraphDetail();
        rptMinstock.DataSource = dtbl;
        rptMinstock.DataBind();
        int i = 1;
        string mst = "";
        string str = "[";
        foreach (DataRow dtRow in dtbl.Rows)
        {
            if (i < dtbl.Rows.Count)
            {
                str += "\n {batch:'" + dtRow[2].ToString() + "',categories:'" + dtRow[3].ToString() + "',y:" + dtRow[1].ToString() + ",name:'" + dtRow[0].ToString() + "',Min:'" + dtRow["MinLevel"].ToString() + "',CS:'" + dtRow["CurrentStock"].ToString() + "'},\n";
            }
            else
            {
                str += "\n {batch:'" + dtRow[2].ToString() + "',categories:'" + dtRow[3].ToString() + "',y:" + dtRow[1].ToString() + ",name:'" + dtRow[0].ToString() + "',Min:'" + dtRow["MinLevel"].ToString() + "',CS:'" + dtRow["CurrentStock"].ToString() + "'}";
            }
            i++;
        }
        str += "]";

        //string str2 = "dataLabels: { enabled: true,    rotation: -90,color: '#000000', align: 'right',   x: 4, y: 10, style: {  fontSize: '13px',  fontFamily: 'Verdana, sans-serif', textShadow: '0 0 3px black'  } }";
        //string str = "[{name:'CS',data:[";
        //foreach (DataRow dtRow in dtbl.Rows)
        //{
        //    if (i < dtbl.Rows.Count)
        //    {
        //        str += "\n {batch:'" + dtRow[2].ToString() + "',categories:'" + dtRow[3].ToString() + "',y:" + dtRow[1].ToString() + ",name:'" + dtRow[0].ToString() + "',color:'#FF0000'},\n";
        //    }
        //    else
        //    {
        //        str += "\n {batch:'" + dtRow[2].ToString() + "',categories:'" + dtRow[3].ToString() + "',y:" + dtRow[1].ToString() + ",name:'" + dtRow[0].ToString() + "',color:'#FF0000'}]," + str2;
        //    }
        //    i++;
        //}
        //str += "},";
        //int c = 1;
        //str += "{name:'MinLevel',data:[";
        //foreach (DataRow dtRow in dtbl.Rows)
        //{
        //    if (c < dtbl.Rows.Count)
        //    {
        //        str += "\n {batch:'" + dtRow[2].ToString() + "',categories:'" + dtRow[3].ToString() + "',y:" + dtRow["MinLevel"].ToString() + ",name:'" + dtRow[0].ToString() + "',color:'green'},\n";
        //    }
        //    else
        //    {
        //        str += "\n {batch:'" + dtRow[2].ToString() + "',categories:'" + dtRow[3].ToString() + "',y:" + dtRow["MinLevel"].ToString() + ",name:'" + dtRow[0].ToString() + "',color:'green'}]";
        //    }
        //    c++;
        //}
        //str += "}]";
















        return str;
    }


    protected void btnHidden_Click(object sender, EventArgs e)
    {
        txtFromDate.Text = DateTime.Parse(txtDate.Text).ToString("dd/MM/yyyy");
        txtToDate.Text = DateTime.Parse(txtDate.Text).ToString("dd/MM/yyyy");
        /*hdfSale.Value = GetStore();
        hdfCustomer.Value = GetCustomer();
        hdfMinStock.Value = GetMinimuStock();
        hdfCounter.Value = GetItems();*/
        Search();


    }

    protected void ddlItemStore_SelectedIndexChanged1(object sender, EventArgs e)
    {
        try
        {

           /* hdfSale.Value = GetStore();
            hdfCustomer.Value = GetCustomer();
            hdfMinStock.Value = GetMinimuStock();
            hdfCounter.Value = GetItems();*/

            Search();
        }
        catch (Exception ex)
        {
        }
    }
    protected void ddlType_SelectedIndexChanged(object sender, EventArgs e)
    {
       /* hdfSale.Value = GetStore();
        hdfCustomer.Value = GetCustomer();
        hdfMinStock.Value = GetMinimuStock();
        hdfCounter.Value = GetItems();*/
        Search();
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        Search();
    }
    protected void btnClearSearch_Click(object sender, EventArgs e)
    {
        ClearSearch();
    }
    protected void lnk1_Click(object sender, EventArgs e)
    {

    }
}