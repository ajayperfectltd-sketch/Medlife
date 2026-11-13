/*----------------------------------- Dal Layer CS Class -----------------------------------------------------------------------------------------------------------------------
Purpose    : Getting Software vendor Daily Password
Author     :Bibin tc(class created by   Martin Joseph )
Copyright  : Perfect Software Solutions (Clt) Pvt Ltd
Created on : 03/05/2018
 * OMID:	84969 
----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------*/




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
using System.Text.RegularExpressions;
using System.Security.Cryptography;
using System.Collections.Generic;
/// <summary>
/// Summary description for DailyPassword
/// </summary>
public class DailyPassword
{
    string[] gAlphabets = new string[27];
    string[] gSuppAlphabets = new string[27];
    public string SoftwareVendorPassword()
    {
        string strDate = "", strPassword = "";
        Int32 intDateSum = 0, i = 0;
        string strDate1 = "";
        string password = "";

        FillAlphabets();
        strDate = DateTime.Now.Day.ToString().PadLeft(2, '0') + DateTime.Now.Month.ToString().PadLeft(2, '0') + DateTime.Now.Year.ToString();
        strDate1 = strDate;

        while (strDate.Length > 2)
        {
            intDateSum = 0;
            for (i = 1; i <= strDate.Length; i++)
            {
                intDateSum = intDateSum + Convert.ToInt32(strDate.Substring(i - 1, 1));
            }
            strDate = intDateSum.ToString();
        }

        //'if even day then add 1 else minus 1

        Int32 mode = (DateTime.Now.Day) % 2;
        if (mode == 0)
        {
            intDateSum = intDateSum + 2;
        }
        else
        {
            intDateSum = intDateSum - 1;
        }
        for (i = 1; i <= strDate1.Length; i++)
        {

            mode = i % 2;
            if (mode == 0)
            {
                strPassword = strPassword + (Convert.ToInt32(strDate1.Substring(i - 1, 1)) * intDateSum).ToString();
            }
            else
            {
                strPassword = strPassword + (Convert.ToInt32(strDate1.Substring(i - 1, 1)) + intDateSum).ToString();
            }
        }

        int tmplength = strPassword.Length - 1;

        for (i = 1; i <= strPassword.Length; i++)
        {
            int count = Convert.ToInt32(strPassword.Substring(i - 1, 1));
            int EndNum = 0; int EndNumTot = 0;

            if (tmplength > 0)
                EndNum = Convert.ToInt32(strPassword.Substring(tmplength - 1, 2));

            for (int j = 0; j < EndNum.ToString().Length; j++)
            {
                EndNumTot = EndNumTot + Convert.ToInt32(EndNum.ToString().Substring(j, 1));
            }
            if (count % 2 == 0 && EndNum != 0)
            {
                if (EndNum > 26)
                {
                    if (EndNum % 5 == 0)
                    {
                        password = password + "@";
                    }
                    else if (EndNum % 3 == 0)
                    {
                        password = password + "#";
                    }
                    else if (EndNum % 2 == 0)
                    {
                        password = password + "*";
                    }
                    else
                    {
                        password = password + EndNumTot.ToString();
                    }

                }
                else
                {

                    password = password + gAlphabets[EndNum];
                }
            }
            else
            {
                if ((EndNumTot - count) > 0)
                    password = password + gAlphabets[EndNumTot - count];
                else
                    password = password + gAlphabets[EndNumTot];

            }
            tmplength = tmplength - 1;
        }
        return password;
    }
    public string SoftwareVendorPasswordOld()
    {
        string strDate = "", strPassword = "";
        Int32 intDateSum = 0, i = 0;
        string strDate1 = "";
        string password = "";

        FillAlphabets();
        strDate = DateTime.Now.Day.ToString().PadLeft(2, '0') + DateTime.Now.Month.ToString().PadLeft(2, '0') + DateTime.Now.Year.ToString();
        strDate1 = strDate;

        while (strDate.Length > 2)
        {
            intDateSum = 0;
            for (i = 1; i <= strDate.Length; i++)
            {
                intDateSum = intDateSum + Convert.ToInt32(strDate.Substring(i - 1, 1));
            }
            strDate = intDateSum.ToString();
        }

        //'if even day then add 1 else minus 1

        Int32 mode = (DateTime.Now.Day) % 2;
        if (mode == 0)
        {
            intDateSum = intDateSum + 2;
        }
        else
        {
            intDateSum = intDateSum - 1;
        }
        for (i = 1; i <= strDate1.Length; i++)
        {

            mode = i % 2;
            if (mode == 0)
            {
                strPassword = strPassword + (Convert.ToInt32(strDate1.Substring(i - 1, 1)) * intDateSum).ToString();
            }
            else
            {
                strPassword = strPassword + (Convert.ToInt32(strDate1.Substring(i - 1, 1)) + intDateSum).ToString();
            }
        }
        for (i = 1; i <= strPassword.Length; i++)
        {
            int count = Convert.ToInt32(strPassword.Substring(i - 1, 1));
            password = password + gAlphabets[count];
        }
        return password;
    }
    private void FillAlphabets()
    {
        Int32 i = 0, StartAlphatet = 65;
        for (i = 1; i <= 26; i++)
        {
            char Chr = (char)StartAlphatet;
            gAlphabets[i] = Chr.ToString();
            StartAlphatet = StartAlphatet + 1;
        }

    }
    public string SupportPassword()
    {
        string strDate = "", strPassword = "";
        Int32 intDateSum = 0, i = 0;
        string strDate1 = "";
        string password = "";

        FillSuppAlphabets();
        strDate = DateTime.Now.Day.ToString().PadLeft(2, '0') + DateTime.Now.Month.ToString().PadLeft(2, '0') + DateTime.Now.Year.ToString();
        strDate1 = strDate;

        while (strDate.Length > 1)
        {
            intDateSum = 0;
            for (i = 1; i <= strDate.Length; i++)
            {
                intDateSum = intDateSum + Convert.ToInt32(strDate.Substring(i - 1, 1));
            }
            strDate = intDateSum.ToString();
        }

        //'if even day then add 1 else minus 1

        Int32 mode = (DateTime.Now.Day) % 2;
        if (mode == 0)
        {
            intDateSum = intDateSum + 1;
        }
        else
        {
            intDateSum = intDateSum - 1;
        }
        for (i = 1; i <= strDate1.Length; i++)
        {
            strPassword = strPassword + (Convert.ToInt32(strDate1.Substring(i - 1, 1)) + intDateSum).ToString();
        }
        for (i = 1; i <= strPassword.Length; i++)
        {
            int count = Convert.ToInt32(strPassword.Substring(i - 1, 1));
            password = password + gSuppAlphabets[count];
        }
        return password;
    }


    private void FillSuppAlphabets()
    {
        Int32 i = 0, StartAlphatet = 65;
        for (i = 1; i <= 26; i++)
        {
            char Chr = (char)StartAlphatet;
            gSuppAlphabets[i] = Chr.ToString();
            StartAlphatet = StartAlphatet + 1;
        }

    }
    //public static Boolean isAlpha(string strToCheck)
    //{
    //    const string charSet = "a-zA-Z";
    //    return Regex.Match(strToCheck, @"^[" + charSet + @"]+$").Success;

    //}
    //public static Boolean isAlphaNumeric(string strToCheck)
    //{
    //    const string charSet = "a-zA-Z";
    //    const string numSet = @"0-9";
    //    return Regex.Match(strToCheck, @"^(?=[" + numSet + @"]*?[" + charSet + @"]+)(?=[" + charSet + @"]*?[" + numSet + @"]+)[" + charSet + numSet + @"]+$").Success;

    //}
    //public static Boolean isAlphaSpclChar(string strToCheck)
    //{
    //    const string charSet = "a-zA-Z";
    //    const string Spcl = @"!;~_=,@<>:.?$%#^[+&*()-";
    //    return Regex.Match(strToCheck, @"^(?=[" + Spcl + @"]*?[" + charSet + @"]+)(?=[" + charSet + @"]*?[" + Spcl + @"]+)[" + charSet + Spcl + @"]+$").Success;

    //}
    //public static Boolean isAlphaNumericSpclChar(string strToCheck)
    //{
    //    const string charSet = "a-zA-Z";
    //    const string Spcl = @"!;~_=,@<>:.?$%#^[+&*()-";
    //    const string numSet = @"0-9";
    //    return Regex.Match(strToCheck, @"^(?=[" + Spcl + @"]*?[" + charSet + @"]*?[" + numSet + "@]+)(?=[" + charSet + @"]*?[" + Spcl + @"]*?[" + numSet + @"]+)[" + charSet + Spcl + numSet + @"]+$").Success;

    //}


    public static Boolean isAlpha(string strToCheck)
    {
        bool result = false;
        for (int i = 0; i < strToCheck.Length; i++)
        {
            if (char.IsLetter(strToCheck[i]))
            {
                result = true;
            }
        }
        return result;

    }
    //public static Boolean IsAlphaNumeric(string strToCheck)
    //{
    //    const string charSet = "a-zA-Z!*~@#%^&(),-.?';:><[$_";
    //    const string numSet = @"0-9";
    //    return Regex.Match(strToCheck, @"^(?=[" + numSet + @"]*?[" + charSet + @"]+)(?=[" + charSet + @"]*?[" + numSet + @"]+)[" + charSet + numSet + @"]+$").Success;

    //}

    public static Boolean IsAlphaNumeric(string strToCheck)
    {
        bool result1 = false, result2 = false, result = false;
        for (int i = 0; i < strToCheck.Length; i++)
        {
            if (char.IsLetter(strToCheck[i]))
            {
                result1 = true;
            }
            if (char.IsNumber(strToCheck[i]))
            {
                result2 = true;
            }

        }
        if (result1 == true && result2 == true)
        {
            result = true;
        }

        return result;

    }

    //public static Boolean IsAlphaNumericSpclChar(string strToCheck)
    //{
    //    bool result = false;
    //    if (Regex.IsMatch(strToCheck, @"^[a-zA-Z0-9]*$") == false)
    //    {
    //        if (Regex.IsMatch(strToCheck, @"^[0-9]*$") == false)
    //        {
    //            if (Regex.IsMatch(strToCheck, @"^[a-zA-Z]*$") == false)
    //            {
    //                result = true;
    //            }
    //            else
    //            {
    //                result = false;
    //            }
    //        }
    //        else
    //        {
    //            result = false;
    //        }

    //    }
    //    else
    //    {
    //        result = false;
    //    }

    //    return result;
    //}
    public static Boolean isAlphaNumericSpclChar(string strToCheck)
    {
        bool result1 = false, result2 = false, result3 = false, result = false;
        for (int i = 0; i < strToCheck.Length; i++)
        {
            if (char.IsLetter(strToCheck[i]))
            {
                result1 = true;
            }
            if (char.IsSymbol(strToCheck[i]) || char.IsPunctuation(strToCheck[i]) || char.IsSeparator(strToCheck[i]))
            {
                result2 = true;
            }

            if (char.IsNumber(strToCheck[i]))
            {
                result3 = true;
            }


        }
        if (result1 == true && result2 == true && result3 == true)
        {
            result = true;
        }

        return result;
    }

    public static string GetRandomAlphaString(int length)
    {
        const string alphanumericCharacters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ" +
            "abcdefghijklmnopqrstuvwxyz";
        return GetRandomString(length, alphanumericCharacters);
    }
    public static string GetRandomAlphaNumericString(int length)
    {
        const string alphanumericCharacters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ" +
            "abcdefghijklmnopqrstuvwxyz" +
            "0123456789";
        return GetRandomString(length, alphanumericCharacters);
    }
    public static string GetRandomAlphanumericSpclCharString(int length)
    {
        const string alphanumericCharacters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ" +
            "abcdefghijklmnopqrstuvwxyz" +
            "0123456789" +
            "*$-+?_&=!%{}/";
        return GetRandomString(length, alphanumericCharacters);
    }

    public static string GetRandomString(int length, IEnumerable<char> characterSet)
    {
        if (length < 0)
            throw new ArgumentException("length must not be negative", "length");
        if (length > int.MaxValue / 8) // 250 million chars ought to be enough for anybody
            throw new ArgumentException("length is too big", "length");
        if (characterSet == null)
            throw new ArgumentNullException("characterSet");
        var characterArray = characterSet.Distinct().ToArray();
        if (characterArray.Length == 0)
            throw new ArgumentException("characterSet must not be empty", "characterSet");

        var bytes = new byte[length * 8];
        new RNGCryptoServiceProvider().GetBytes(bytes);
        var result = new char[length];
        for (int i = 0; i < length; i++)
        {
            ulong value = BitConverter.ToUInt64(bytes, i * 8);
            result[i] = characterArray[value % (uint)characterArray.Length];
        }
        return new string(result);
    }
   
   
}