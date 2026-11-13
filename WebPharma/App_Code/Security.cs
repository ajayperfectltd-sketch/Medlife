using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Net.NetworkInformation;
using System.Runtime.InteropServices;
using System.ComponentModel;
using System.Globalization;
using System.IO;
using System.Text;
using System.Management;
using Microsoft.Win32;
using System.Security.Cryptography;
using System.Reflection;

[assembly: AssemblyVersion("2.6.9")] 
/// <summary>
/// Summary description for Security
/// </summary>
/// 

/*
       =====================================================================================================================
           Name:BIJOY CP
       =====================================================================================================================
       */
public class Security
{
    #region GetIpNetTable interop
    // The max number of physical addresses.
    const int MAXLEN_PHYSADDR = 8;
    // Define the MIB_IPNETROW structure.
    [StructLayout(LayoutKind.Sequential)]
    struct MIB_IPNETROW
    {
        [MarshalAs(UnmanagedType.U4)]
        public int dwIndex;
        [MarshalAs(UnmanagedType.U4)]
        public int dwPhysAddrLen;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = MAXLEN_PHYSADDR)]
        public byte[] bPhysAddr;
        [MarshalAs(UnmanagedType.U4)]
        public int dwAddr;
        [MarshalAs(UnmanagedType.U4)]
        public int dwType;
    }

    // Declare the GetIpNetTable function.
    [DllImport("IpHlpApi.dll")]
    [return: MarshalAs(UnmanagedType.U4)]
    static extern int GetIpNetTable(
    IntPtr pIpNetTable,
    [MarshalAs(UnmanagedType.U4)]
ref int pdwSize,
    bool bOrder);
    // The insufficient buffer error.
    const int ERROR_INSUFFICIENT_BUFFER = 122;

    #endregion

    /// <summary>
    /// Pulls the MAC address for a given IP address out of the ARP table

    /// </summary>
    /// <param name="ip">IP address to lookup</param>
    /// <returns>The MAC address for the given IP address or null if the
    public static string GetClientIPAddress1(System.Web.HttpRequest httpRequest)
    {
        string OriginalIP = string.Empty; string RemoteIP = string.Empty; OriginalIP = httpRequest.ServerVariables["HTTP_X_FORWARDED_FOR"]; //original IP will be updated by Proxy/Load Balancer.
        RemoteIP = httpRequest.ServerVariables["REMOTE_ADDR"]; //Proxy/Load Balancer IP or original IP if no proxy was used
        if (OriginalIP != null && OriginalIP.Trim().Length > 0) return OriginalIP + "(" + RemoteIP + ")"; //Lets return both the IPs.



        return RemoteIP;
    }
    public static string NetClassIP(System.Web.HttpRequest httpRequest)
    {
        string OriginalIP = string.Empty; string RemoteIP = string.Empty; OriginalIP = httpRequest.ServerVariables["HTTP_X_FORWARDED_FOR"]; //original IP will be updated by Proxy/Load Balancer.
        RemoteIP = httpRequest.ServerVariables["MAC_ADDR"]; //Proxy/Load Balancer IP or original IP if no proxy was used
        //  RemoteIP = httpRequest.ServerVariables["REMOTE_ADDR"];
        if (OriginalIP != null && OriginalIP.Trim().Length > 0)
        {
            return OriginalIP + "(" + RemoteIP + ")"; //Lets return both the IPs.
        } return RemoteIP;
    }
    private string NetClassMAC()
    {
        string macAddresses = ""; foreach (NetworkInterface nic in NetworkInterface.GetAllNetworkInterfaces())
        {
            if (nic.OperationalStatus == OperationalStatus.Up)
            {
                macAddresses += nic.GetPhysicalAddress().ToString();
                break;
            }
        }
        return macAddresses;
    }
    public string ResetMAC(string ip)
    { // The number of bytes needed.
        int bytesNeeded = 0;
        // The result from the API call.
        int result = GetIpNetTable(IntPtr.Zero, ref bytesNeeded, false);
        // Call the function, expecting an insufficient buffer.
        if (result != ERROR_INSUFFICIENT_BUFFER)
        {  // Throw an exception.
            throw new Win32Exception(result);
        }
        // Allocate the memory, do it in a try/finally block, to ensure
        // that it is released.
        IntPtr buffer = IntPtr.Zero; try
        {      // Allocate the memory.
            buffer = Marshal.AllocCoTaskMem(bytesNeeded);
            // Make the call again. If it did not succeed, then
            // raise an error.
            result = GetIpNetTable(buffer, ref bytesNeeded, false);
            // If the result is not 0 (no error), then throw an exception.
            if (result != 0)
            {
                // Throw an exception.
                throw new Win32Exception(result);
            }
            // Now we have the buffer, we have to marshal it. We can read
            // the first 4 bytes to get the length of the buffer.
            int entries = Marshal.ReadInt32(buffer);
            // Increment the memory pointer by the size of the int.
            IntPtr currentBuffer = new IntPtr(buffer.ToInt64() +
            Marshal.SizeOf(new int()));
            // Allocate an array of entries.
            MIB_IPNETROW[] table = new MIB_IPNETROW[entries];
            // Cycle through the entries.

           



            for (int index = 0; index < entries; index++)
            {
                // Call PtrToStructure, getting the structure information.
                table[index] = (MIB_IPNETROW)Marshal.PtrToStructure(new
                IntPtr(currentBuffer.ToInt64() + (index *
                Marshal.SizeOf(typeof(MIB_IPNETROW)))), typeof(MIB_IPNETROW));
            }
            byte[] macBytes = null;
            for (int i = 0; i < entries; i++)
            {
                string tableIP = String.Format("{0}.{1}.{2}.{3}",
                table[i].dwAddr & 0x000000ff,
                (table[i].dwAddr & 0x0000ff00) >> 8,
                (table[i].dwAddr & 0x00ff0000) >> 16,
                (table[i].dwAddr & 0xff000000) >> 24);
                if (ip == "127.0.0.1" || ip == ConfigurationManager.AppSettings.Get("SecurityIP"))
                {
                    string macdirect = GetMACcNorm();
                    string retunMac = macdirect.Substring(0, 2);
                    retunMac = retunMac + ":";
                    retunMac = retunMac + macdirect.Substring(2, 2);
                    retunMac = retunMac + ":";
                    retunMac = retunMac + macdirect.Substring(4, 2);
                    retunMac = retunMac + ":";
                    retunMac = retunMac + macdirect.Substring(6, 2);
                    retunMac = retunMac + ":";
                    retunMac = retunMac + macdirect.Substring(8, 2);
                    retunMac = retunMac + ":";
                    retunMac = retunMac + macdirect.Substring(10, 2);
                    return retunMac;
                }


                if (tableIP == ip)
                {
                    macBytes = table[i].bPhysAddr;
                    break;
                }
            }
            string mac = ""; for (int k = 0; k < 6; k++)
            {
                string hexTet = macBytes[k].ToString("X", new NumberFormatInfo()); if (hexTet.Length == 1)
                {
                    hexTet = "0" + hexTet;
                }

                mac += hexTet;

                if (k < 5)
                {
                    mac += ":";
                }

            }

            return mac;




        }
        catch
        {
            return null;
        }
        finally
        {
            // Release the memory.
            Marshal.FreeCoTaskMem(buffer);
        }
    }
    private string GetMACcNorm()
    {
        string macAddresses = "";

        foreach (NetworkInterface nic in NetworkInterface.GetAllNetworkInterfaces())
        {
            if (nic.OperationalStatus == OperationalStatus.Up)
            {
                macAddresses += nic.GetPhysicalAddress().ToString();
                break;
            }
        }
        return macAddresses;
    }

    public static string GetMac(HttpRequest e,string host,string local)
    {
        
        Security.GetIP(e, System.Net.Dns.GetHostName().ToString(), ConfigurationManager.AppSettings.Get("SecurityIP"));
        string ClientIP = Security.GetIP(e, System.Net.Dns.GetHostName().ToString(), ConfigurationManager.AppSettings.Get("SecurityIP"));
        if (host == local)
        {
            ClientIP = Security.GetIP(e,host,local );
            Security security = new Security();
            return security.ResetMAC(ClientIP);
        }
        else
        {
           
            Security security = new Security();
            return security.ResetMAC(ClientIP);

        }
       


    }


    public static string GetIPOrginIP(System.Web.HttpRequest httpRequest, string host, string local)
    {
        string OriginalIP = string.Empty; string RemoteIP = string.Empty; OriginalIP = httpRequest.ServerVariables["HTTP_X_FORWARDED_FOR"]; //original IP will be updated by Proxy/Load Balancer.
        RemoteIP = httpRequest.ServerVariables["REMOTE_ADDR"]; //Proxy/Load Balancer IP or original IP if no proxy was used
        if (OriginalIP != null && OriginalIP.Trim().Length > 0) return OriginalIP + "(" + RemoteIP + ")"; //Lets return both the IPs.

       
        if (local == host)
        {
            RemoteIP= "127.0.0.1";
        }
        if (RemoteIP == "127.0.0.1")
        {
            RemoteIP = ConfigurationManager.AppSettings.Get("127.0.0.1");
        }

        return RemoteIP;
    }








    public static string GetIP(System.Web.HttpRequest httpRequest,string host,string local)
    {
        string OriginalIP = string.Empty; string RemoteIP = string.Empty; OriginalIP = httpRequest.ServerVariables["HTTP_X_FORWARDED_FOR"]; //original IP will be updated by Proxy/Load Balancer.
        RemoteIP = httpRequest.ServerVariables["REMOTE_ADDR"]; //Proxy/Load Balancer IP or original IP if no proxy was used
        if (OriginalIP != null && OriginalIP.Trim().Length > 0) return OriginalIP + "(" + RemoteIP + ")"; //Lets return both the IPs.

        if (RemoteIP == "127.0.0.1")
        {
            string m_HostName = System.Net.Dns.GetHostName();
            RemoteIP = System.Net.Dns.GetHostAddresses(m_HostName).GetValue(0).ToString();
        }
        RemoteIP = httpRequest.ServerVariables["REMOTE_ADDR"];
        if(local == host)
        {
            return "127.0.0.1";
        }
        return RemoteIP;
    }
    public static string Encrypt(string inputText)
    {
        const string _KEY = "oi0e06108823e25";//No need to change : this key is used in Licensing Appliction also
        byte[] _SALT = Encoding.ASCII.GetBytes(_KEY.Length.ToString());
        RijndaelManaged rijndaelCipher = new RijndaelManaged();
        byte[] plainText = Encoding.Unicode.GetBytes(inputText);
        PasswordDeriveBytes SecretKey = new PasswordDeriveBytes(_KEY, _SALT);
        using (ICryptoTransform encryptor = rijndaelCipher.CreateEncryptor(SecretKey.GetBytes(32), SecretKey.GetBytes(16)))
        {
            using (MemoryStream memoryStream = new MemoryStream())
            {
                using (CryptoStream cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write))
                {
                    cryptoStream.Write(plainText, 0, plainText.Length);
                    cryptoStream.FlushFinalBlock();
                    return Convert.ToBase64String(memoryStream.ToArray());
                }
            }
        }
    }
    public static string DecryptLicense(string inputText)
    {
        const string _KEY = "oi0e06108823e25";//No need to change : this key is used in Licensing Appliction also
        byte[] _SALT = Encoding.ASCII.GetBytes(_KEY.Length.ToString());
        RijndaelManaged rijndaelCipher = new RijndaelManaged();
        byte[] encryptedData = Convert.FromBase64String(inputText);
        PasswordDeriveBytes secretKey = new PasswordDeriveBytes(_KEY, _SALT);
        using (ICryptoTransform decryptor = rijndaelCipher.CreateDecryptor(secretKey.GetBytes(32), secretKey.GetBytes(16)))
        {
            using (MemoryStream memoryStream = new MemoryStream(encryptedData))
            {
                using (CryptoStream cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read))
                {
                    byte[] plainText = new byte[encryptedData.Length];
                    int decryptedCount = cryptoStream.Read(plainText, 0, plainText.Length);
                    return Encoding.Unicode.GetString(plainText, 0, decryptedCount);
                }
            }
        }
    }


    public string UrlDecrypt(string cipherText)
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

    public string UrlEncrypt(string clearText)
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


    //public void test()
    //{
    //    string M = "";
    //    NetworkInterface[] nics = NetworkInterface.GetAllNetworkInterfaces();

    //    for (int j = 0; j <= 1; j++)
    //    {

    //        PhysicalAddress address = nics[j].GetPhysicalAddress();

    //        byte[] bytes = address.GetAddressBytes();

    //        for (int i = 0; i < bytes.Length; i++)
    //        {

    //            // M = M + bytes.ToString("X2");

    //            if (i != bytes.Length - 1)
    //            {

    //                M = M + ("-");

    //            }

    //        }

    //    }
    //}




    //protected void Page_Load(object sender, EventArgs e)
    //{
    //    // string ClientIP = GetClientIPAddress(HttpContext.Current.Request); 
    //  //  TextBox1.Text = ResolveMAC("192.168.0.119");



    //}









}
