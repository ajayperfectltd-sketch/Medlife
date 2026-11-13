using System;
using System.Data;
using System.Text;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.IO;
using System.Management;
using Microsoft.Win32;
using System.Security.Cryptography;
using WebPharma.Business.Licensing;

/// <summary>
/// Summary description for Licensing
/// </summary>
public class Licensing
{
    public Licensing()
    {
        Initialize();
    }
    #region Enums
    public enum eReseller
    {
        Perfect = 1,
        Costech = 2,
        Mitco = 3,
        Accent = 4,
        NC = 5
    }
    public enum eClientStatus
    {
        Active = 1,
        InActive = 0
    }
    public enum eProduct
    {
        SCORE = 0001,
        VBCORE = 0002,
        SCB = 0003,
        HOUSEFED = 0004,
        KSIDC = 0005
    }
    public enum eEdition
    {
        Demo = 01,
        Evaluation = 02,
        Standard = 03,
        Premium = 04,
    }
    public enum eLicenseMethod
    {
        Fixed = 01,
        Floating = 02
    }
    public enum eLicenseMode
    {
        User = 01,
        Branch = 02,
        Machine = 03,
        Days = 04
    }
    public enum Modules
    {
        Customer = 1,
        SavingsBank = 2
    }
    public enum Features
    {
        PALMTECH = 1,
        SMS = 2,
        RTGS_NEFT = 3,
        NETBANKING = 4,
        MIS_DASH = 5
    }
    #endregion

    #region Variables
    private static Int16 _Reseller;
    private static string _ClientName;
    private static string _Product;
    private static string _Edition;
    private static string _LicenseMethod;
    private static string _LicenseMode;
    private static string _ErrorMessage;
    private static int _LicenseCount;
    private static DateTime _InstallationDate;
    private static DateTime _ExpiryDate;
    private static string _DbName;
    private static DataTable _dtLicense;
    private static DataTable _dtBranches;
    private static bool _Licensed;
    private static string _SerialKey;
    private static int _EvaluationPeriod;
    private static string error = " If you purchased the license Please update your License !";

    private static string _FooterText;
    private static string _FooterTextLogin;
    private static string _EmailReseller;
    private static string _WebReseller;

    public static string FooterTextLogin
    {
        get { return _FooterTextLogin; }
        set { _FooterTextLogin = value; }
    }
    public static string EmailReseller
    {
        get { return _EmailReseller; }
        set { _EmailReseller = value; }
    }
    public static string WebReseller
    {
        get { return _WebReseller; }
        set { _WebReseller = value; }
    }
    public static string FooterText
    {
        get { return _FooterText; }
        set { _FooterText = value; }
    }
    public static Int16 Reseller
    {
        get { return _Reseller; }
        set { _Reseller = value; }
    }
    public static string ClientName
    {
        get { return _ClientName; }
        set { _ClientName = value; }
    }
    public static string Product
    {
        get { return _Product; }
        set { _Product = value; }
    }
    public static string Edition
    {
        get { return _Edition; }
        set { _Edition = value; }
    }
    public static string LicenseMethod
    {
        get { return _LicenseMethod; }
        set { _LicenseMethod = value; }
    }
    public static string LicenseMode
    {
        get { return _LicenseMode; }
        set { _LicenseMode = value; }
    }
    public static int LicenseCount
    {
        get { return _LicenseCount; }
        set { _LicenseCount = value; }
    }
    public static DateTime InstallationDate
    {
        get { return _InstallationDate; }
        set { _InstallationDate = value; }
    }
    public static DateTime ExpiryDate
    {
        get { return _ExpiryDate; }
        set { _ExpiryDate = value; }
    }
    public static string DbName
    {
        get { return _DbName; }
        set { _DbName = value; }
    }
    public static DataTable DtLicense
    {
        get { return _dtLicense; }
        set { _dtLicense = value; }
    }
    public static DataTable DtBranches
    {
        get { return _dtBranches; }
        set { _dtBranches = value; }
    }
    public static bool Licensed
    {
        get { return _Licensed; }
        set { _Licensed = value; }
    }
    public static string ErrorMessage
    {
        get { return _ErrorMessage; }
        set { _ErrorMessage = value; }
    }
    public static string SerialKey
    {
        get { return _SerialKey; }
        set { _SerialKey = value; }
    }
    public static int EvaluationPeriod
    {
        get { return _EvaluationPeriod; }
        set { _EvaluationPeriod = value; }
    }
    #endregion

    public static void Initialize()
    {
        _Reseller = 1;
        _ClientName = string.Empty;
        _Product = string.Empty;
        _Edition = string.Empty;
        _LicenseMethod = string.Empty;
        _LicenseMode = string.Empty;
        _ErrorMessage = string.Empty;
        _LicenseCount = 0;
        _InstallationDate = DateTime.Now;
        _ExpiryDate = DateTime.Now;
        _DbName = string.Empty;
        _Licensed = false;
        _SerialKey = string.Empty;
        _EvaluationPeriod = 0;
    }
    public static bool InsertLicense(string License)
    {
        BlLicensing bllicensing = new BlLicensing();
        return bllicensing.InsertLicense(License);
    }
    public static void CheckLicensedOrNot()
    {

        int count = 0;
        BlLicensing bllicensing = new BlLicensing();
        count = bllicensing.CheckLicensedOrNot();
        if (count > 0)
        {
            Licensed = true;
        }
        else
        {
            Licensed = false;
        }

    }
    public static bool InsertSerialKey(string serialKey)
    {
        BlLicensing bllicensing = new BlLicensing();
        string Field = "";
        string Value = "";
        string Field1 = "";
        string Value1 = "";

        Field = Security.Encrypt("SerialKey");
        Value = Security.Encrypt(serialKey);

        Field1 = Security.Encrypt("EvaluationPeriod");
        Value1 = Security.Encrypt("0");

        return bllicensing.InsertSerialKey(Field, Value, Field1, Value1);
    }
    public static bool InsertEvaluationPeriod(int evaluationprd)
    {
        BlLicensing bllicensing = new BlLicensing();
        string Field1 = "", Value1 = "", Field2 = "", Value2 = "", Field3 = "", Value3 = "", Field4 = "", Value4 = "";
        Field1 = Security.Encrypt("SerialKey");
        //Value1 = Security.Encrypt("04");

        Field2 = Security.Encrypt("InstallationDate");
        Value2 = Security.Encrypt(Convert.ToDateTime(DateTime.Now.ToShortDateString()).ToString());

        Field3 = Security.Encrypt("ExpiryDate");
        Value3 = Security.Encrypt(Convert.ToDateTime(DateTime.Now.AddDays(7).ToShortDateString()).ToString());

        Field4 = Security.Encrypt("EvaluationPeriod");
        Value4 = Security.Encrypt(Convert.ToString(evaluationprd + 1));

        return bllicensing.InsertEvaluationPeriod(Field1, Value1, Field2, Value2, Field3, Value3, Field4, Value4);
    }
    public static void GetLicense()
    {
        DtLicense = new DataTable();
        BlLicensing bllicensing = new BlLicensing();
        DtLicense = bllicensing.GetLicense();
        SetLicenseValues(DtLicense);
    }
    public static void SetLicenseValues(DataTable dt)
    {
        DtBranches = dt.Clone();
        int brIndex = 0;
        foreach (DataRow dr in dt.Rows)
        {
            if (Security.DecryptLicense(dr["Field"].ToString()) == "Remarks")
            {
                brIndex = dt.Rows.IndexOf(dr) + 1;
            }
        }
        foreach (DataRow dr in dt.Rows)
        {
            switch (Security.DecryptLicense(dr["Field"].ToString()))
            {
                case "ResellerID":
                    Reseller = Convert.ToInt16(Security.DecryptLicense(dr["Value"].ToString()));
                    break;
                case "ClientName":
                    ClientName = Security.DecryptLicense(dr["Value"].ToString());
                    break;
                case "Product":
                    Product = ((eProduct)Convert.ToInt32(Security.DecryptLicense(dr["Value"].ToString()))).ToString();
                    break;
                case "Edition":
                    Edition = ((eEdition)Convert.ToInt32(Security.DecryptLicense(dr["Value"].ToString()))).ToString();
                    break;
                case "LicenseMethod":
                    LicenseMethod = ((eLicenseMethod)Convert.ToInt32(Security.DecryptLicense(dr["Value"].ToString()))).ToString();
                    break;
                case "LicenseMode":
                    LicenseMode = ((eLicenseMode)Convert.ToInt32(Security.DecryptLicense(dr["Value"].ToString()))).ToString();
                    break;
                case "LicenseCount":
                    LicenseCount = Convert.ToInt32(Security.DecryptLicense(dr["Value"].ToString()));
                    break;
                case "InstallationDate":
                    InstallationDate = Convert.ToDateTime(Security.DecryptLicense(dr["Value"].ToString()));
                    break;
                case "ExpiryDate":
                    ExpiryDate = Convert.ToDateTime(Security.DecryptLicense(dr["Value"].ToString()));
                    break;
                case "SerialKey":
                    SerialKey = Security.DecryptLicense(dr["Value"].ToString());
                    break;
                case "EvaluationPeriod":
                    EvaluationPeriod = Convert.ToInt32(Security.DecryptLicense(dr["Value"].ToString()));
                    break;

            }
            if (dt.Rows.IndexOf(dr) >= brIndex && Security.DecryptLicense(dr["Field"].ToString()) != "SerialKey" && Security.DecryptLicense(dr["Field"].ToString()) != "EvaluationPeriod")
            {
                DtBranches.Rows.Add(dr.ItemArray);
            }
        }
    }
    public static BlLicensing UpdateData()
    {
        BlLicensing bllicensing = new BlLicensing();
        bllicensing.Reseller = Reseller;
        bllicensing.ClientName = ClientName;
        bllicensing.Product = Product;
        bllicensing.Edition = Edition;
        bllicensing.LicenseMethod = LicenseMethod;
        bllicensing.LicenseMode = LicenseMode;
        bllicensing.LicenseCount = LicenseCount;
        bllicensing.InstallationDate = InstallationDate;
        bllicensing.ExpiryDate = ExpiryDate;
        return bllicensing;
    }
    public static void CheckUserLicense()
    {
        if (DtLicense.Rows.Count > 0)
        {
            BlLicensing bllicensing = new BlLicensing();
            bllicensing = UpdateData();
            if (bllicensing.LicenseMode == eLicenseMode.User.ToString())
            {

                DataTable dtUsers = new DataTable();
                dtUsers = bllicensing.CheckLicense();

                if (dtUsers.Rows.Count > 0)
                {
                    // if (dtUsers.Rows[0]["BankHO"].ToString().ToUpper() == Licensing.ClientName.ToUpper())
                    // {
                    int TotalUsers = 0;
                    int TotalLoginUsers = 0;

                    TotalUsers = Convert.ToInt32(dtUsers.Rows[0]["TotalUsers"].ToString());
                    TotalLoginUsers = Convert.ToInt32(dtUsers.Rows[0]["TotalLoginUsers"].ToString());

                    if (bllicensing.LicenseMethod == eLicenseMethod.Fixed.ToString())//If Fixed
                    {
                        if (HttpContext.Current.Handler.ToString() == "ASP.default_aspx")//Check usercount When Login
                        {
                            if (TotalUsers > bllicensing.LicenseCount)
                            {
                                Licensing.ErrorMessage = "Medlife Login failed. User count exceed License count. " + error;
                                HttpContext.Current.Response.Redirect("Licensing.aspx");
                            }
                        }
                        else//Check UserCount When Add new user
                        {
                            if (TotalUsers >= bllicensing.LicenseCount)
                            {
                                Licensing.ErrorMessage = "Medlife not allowed to create new user. User count reached License count. " + error;
                                HttpContext.Current.Response.Redirect("Licensing.aspx");
                            }
                        }
                    }
                    else if (bllicensing.LicenseMethod == eLicenseMethod.Floating.ToString())//If Floating
                    {
                        if (HttpContext.Current.Handler.ToString() == "ASP.default_aspx")//Check usercount When Login
                        {
                            if (TotalLoginUsers >= bllicensing.LicenseCount)
                            {
                                Licensing.ErrorMessage = "Medlife Login failed. Logined User count reached License count. " + error;
                                HttpContext.Current.Response.Redirect("Licensing.aspx");
                            }
                        }
                    }

                    //}
                    //else
                    //{
                    //    //Client error
                    //    Licensing.ErrorMessage = "Invalid License. " + error;
                    //    HttpContext.Current.Response.Redirect("~/Presentation/Licensing/Licensing.aspx");
                    //}
                }

            }
        }
    }
    public static void CheckMachineLicense()
    {
        if (DtLicense.Rows.Count > 0)
        {
            BlLicensing bllicensing = new BlLicensing();
            bllicensing = UpdateData();
            if (bllicensing.LicenseMode == eLicenseMode.Machine.ToString())
            {
                DataTable dtMachine = new DataTable();
                dtMachine = bllicensing.CheckLicense();

                if (dtMachine.Rows.Count > 0)
                {
                    // if (dtMachine.Rows[0]["BankHO"].ToString().ToUpper() == Licensing.ClientName.ToUpper())
                    // {
                    int TotalMachines = 0;
                    int TotalLoginMachines = 0;

                    TotalMachines = Convert.ToInt32(dtMachine.Rows[0]["TotalMachines"].ToString());
                    TotalLoginMachines = Convert.ToInt32(dtMachine.Rows[0]["TotalLoginMachines"].ToString());

                    if (bllicensing.LicenseMethod == eLicenseMethod.Fixed.ToString())//If Fixed
                    {
                        if (HttpContext.Current.Handler.ToString() == "ASP.default_aspx")//Check Machinecount When Login
                        {
                            if (TotalMachines > bllicensing.LicenseCount)
                            {
                                Licensing.ErrorMessage = "Medlife Login failed.Machine count exceed License count. " + error;
                                HttpContext.Current.Response.Redirect("Licensing.aspx");
                            }
                        }
                        else//Check Machinecount When Add New Machines
                        {
                            if (TotalMachines >= bllicensing.LicenseCount)
                            {
                                Licensing.ErrorMessage = "Medlife not allowed to add new Machine. Machine count reached License count. " + error;
                                HttpContext.Current.Response.Redirect("Licensing.aspx" + (HttpContext.Current.Request.QueryString == null ? "" : "?" + HttpContext.Current.Request.QueryString));
                            }
                        }
                    }
                    //}
                    //else
                    //{
                    //    //client error
                    //    Licensing.ErrorMessage = "Invalid License. " + error;
                    //    HttpContext.Current.Response.Redirect("~/Presentation/Licensing/Licensing.aspx");
                    //}
                }
            }
        }
    }
    public static void CheckBranchLicense(string newStore)
    {
        if (DtLicense.Rows.Count > 0)
        {
            BlLicensing bllicensing = new BlLicensing();
            bllicensing = UpdateData();
            if (bllicensing.LicenseMode == eLicenseMode.Branch.ToString())
            {

                DataTable dt = new DataTable();
                dt = bllicensing.CheckLicense();

                if (dt.Rows.Count > 0)
                {
                    // if (dt.Rows[0]["BankHO"].ToString().ToUpper() == Licensing.ClientName.ToUpper())
                    // {
                    int TotalBranches = 0;
                    int TotalLoginBranches = 0;

                    TotalBranches = Convert.ToInt32(dt.Rows[0]["TotalBranches"].ToString());
                    TotalLoginBranches = Convert.ToInt32(dt.Rows[0]["TotalLoginBranches"].ToString());

                    if (bllicensing.LicenseMethod == eLicenseMethod.Fixed.ToString())//If Fixed
                    {
                        if (HttpContext.Current.Handler.ToString() == "ASP.default_aspx")//Check BranchCount When Login
                        {
                            if (TotalBranches > bllicensing.LicenseCount)
                            {
                                Licensing.ErrorMessage = "Pharma Login failed! store count exceed License count. " + error;
                                HttpContext.Current.Response.Redirect("Licensing.aspx");
                            }
                            else
                            {

                                if (newStore != "All")
                                {
                                    if (DtBranches.Rows.Count > 0)
                                    {
                                        bool status = false;
                                        foreach (DataRow drBranchs in DtBranches.Rows)
                                        {
                                            char[] sep = { '!' };

                                            string[] vals = newStore.ToString().Split(sep);
                                            string newStoreName = vals[0].ToString().ToUpper().Trim();
                                            //string newStoreTitle = vals[1].ToString().ToUpper().Trim();

                                            string[] vals1 = Security.DecryptLicense(drBranchs["Field"].ToString()).Split(sep);
                                            string licenseBranchName = vals1[0].ToString().ToUpper().Trim();
                                            string licenseBranchTitle = vals1[1].ToString().ToUpper().Trim();

                                            if (licenseBranchName == newStoreName && newStoreName == licenseBranchTitle)//Check Whether Licensed branch or not  
                                            {
                                                status = true;
                                                break;
                                            }
                                        }
                                        if (status == false)
                                        {
                                            Licensing.ErrorMessage = "No License for this Company! Please Verify your Company Name/Title ! ";
                                            HttpContext.Current.Response.Redirect("Licensing.aspx");
                                        }
                                    }
                                }
                            }
                        }
                        else//Check BranchCount When Create new branch
                        {
                            if (TotalBranches >= bllicensing.LicenseCount)
                            {
                                Licensing.ErrorMessage = "Store count reached License count! " + error;
                                HttpContext.Current.Response.Redirect("Licensing.aspx");
                            }
                            else
                            {

                                if (newStore != "All")
                                {
                                    if (DtBranches.Rows.Count > 0)
                                    {
                                        bool status = false;
                                        foreach (DataRow drBranchs in DtBranches.Rows)
                                        {
                                            char[] sep = { '!' };

                                            string[] vals = newStore.ToString().Split(sep);
                                            string newBranchName = vals[0].ToString().ToUpper().Trim();
                                            //string newBranchTitle = vals[1].ToString().ToUpper().Trim();

                                            string[] vals1 = Security.DecryptLicense(drBranchs["Field"].ToString()).Split(sep);
                                            string licenseBranchName = vals1[0].ToString().ToUpper().Trim();
                                            string licenseBranchTitle = vals1[1].ToString().ToUpper().Trim();

                                            if (licenseBranchName == newBranchName && newBranchName == licenseBranchTitle)//Check Whether Licensed branch or not  
                                            {
                                                status = true;
                                                break;
                                            }
                                        }
                                        if (status == false)
                                        {
                                            Licensing.ErrorMessage = "No License for this Company! Please Verify your Company Name/Title ! ";
                                            HttpContext.Current.Response.Redirect("Licensing.aspx");
                                        }
                                    }
                                }
                            }
                        }
                    }
                    //}
                    //else
                    //{
                    //    //BankHo Mismatch error
                    //    Licensing.ErrorMessage = "Bank Name Mismatch Error! Please Check Bank Head Office Name.";
                    //    HttpContext.Current.Response.Redirect("~/Presentation/Licensing/Licensing.aspx");
                    //}
                }
                //else
                //{
                //    //BankHo null
                //    Licensing.ErrorMessage = "Bank Name Error! Please Update Bank Head Office Name. ";
                //    HttpContext.Current.Response.Redirect("~/Presentation/Licensing/Licensing.aspx");
                //}
            }
            //else
            //{
            //    //Invalid Licence
            //    Licensing.ErrorMessage = "Invalid License. " + error;
            //    HttpContext.Current.Response.Redirect("~/Presentation/Licensing/Licensing.aspx");
            //}
        }
    }

    public static void CheckDayLicense()
    {
        if (DtLicense.Rows.Count > 0)
        {
            BlLicensing bllicensing = new BlLicensing();
            bllicensing = UpdateData();
            if (bllicensing.LicenseMode == eLicenseMode.Days.ToString())
            {

                if (Convert.ToDateTime(DateTime.Now.ToShortDateString()) < Convert.ToDateTime(Licensing.InstallationDate.ToString()) ||
                    Convert.ToDateTime(DateTime.Now.ToShortDateString()) > Convert.ToDateTime(Licensing.ExpiryDate.ToString()))
                {
                    Licensing.ErrorMessage = "Pharma Login failed. Product License Expired. " + error;
                    HttpContext.Current.Response.Redirect("Licensing.aspx");
                }

            }
        }
    }


    public static string GenerateProductID()
    {

        //==========Get Hard Disk Information -Serial Number==========

        ManagementObjectSearcher searcher = new
        ManagementObjectSearcher("SELECT * FROM Win32_Processor");
        string ProductIDString;
        string ProductID;
        string ProcessorId = "";
        foreach (ManagementObject Processor in searcher.Get())
        {
            if (Processor["ProcessorId"] != null)
            {
                ProcessorId = Processor["ProcessorId"].ToString();
                break;
            }
        }

        //==========Get Server and Domain Name=========================
        string DomainName;
        string serverName;
        DomainName = System.Net.Dns.GetHostName();
        serverName = Environment.MachineName.ToString();
        ProductIDString = ClientName + "_" + (ProcessorId.Length > 0 ? ProcessorId.Trim() : ProcessorId) + "_" + serverName + "_" + DomainName;
        ProductID = GetMD5ProductID(ProductIDString);
        if (ProductID != "")
        {
            ProductID = ProductID.Replace("-", "").Substring(0, 25);
            for (int i = 5; i < ProductID.Length; i++)
            {
                ProductID = ProductID.Insert(i, "-");
                i += 5;
            }
            // ProductID = ProductID;
        }
        return ProductID;
    }

    public static string GetMD5ProductID(string toEncrypt)
    {
        string KeyString = "";
        byte[] toEncryptArray = UTF8Encoding.UTF8.GetBytes(toEncrypt);

        MD5CryptoServiceProvider hashmd5 = new MD5CryptoServiceProvider();
        KeyString = BitConverter.ToString(hashmd5.ComputeHash(toEncryptArray));
        return KeyString;
    }

}
