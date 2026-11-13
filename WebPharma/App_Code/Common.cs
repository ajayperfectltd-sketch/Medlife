
/*----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
WebPharma
On			By			OMID
----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

 Bibin tc  ADD AMOUNT IN WORDS
--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------*/


using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using WebPharma.Business;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Xml;
using System.Text;
using System.IO;
using System.Xml.Serialization;
using System.Security.Cryptography;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data.Common;
using System.Data.SqlClient;
/// <summary>
/// Summary description for Common
/// </summary>
public class Common
{
    public Common()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    public static string WriteToXml(ArrayList arlist)
    {
        StringBuilder strxml = new StringBuilder();
        try
        {
            strxml.Append("<root>");
            for (int i = 0; i < arlist.Count; i++)
            {
                strxml.Append(ToXml(arlist[i]));
                string a = arlist[i].ToString();
            }
            strxml.Append("</root>");
        }
        catch (Exception)
        {
            throw;
        }
        return strxml.ToString();

    }

    private static string ToXml(Object obj)
    {

        StringWriter Output = new StringWriter(new StringBuilder());
        string Ret = String.Empty;

        try
        {
            XmlSerializer s = new XmlSerializer(obj.GetType());
            s.Serialize(Output, obj);

            // To cut down on the size of the xml being sent to the database, we'll strip
            // out this extraneous xml.

            Ret = Output.ToString().Replace("xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\"", "");
            Ret = Ret.Replace("xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\"", "");
            Ret = Ret.Replace("<?xml version=\"1.0\" encoding=\"utf-16\"?>", "").Trim();
        }
        catch (Exception) { throw; }

        return Ret;
    }
    public DataTable SelectPasswordLength()
    {
        DataTable dtblusers = new DataTable();
        Database db = DatabaseFactory.CreateDatabase("WebPharma");
        string sqlCommand = "proCmnPasswordLength";
        DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

        try
        {
            dtblusers = db.ExecuteDataSet(dbCommand).Tables[0];
            return dtblusers;
        }
        catch (SqlException e)
        {
            throw e;
        }
    }
    public int BlockNewEntry(long Value)
    {
        if (Value >= 50)
        {
            return 1;
        }
        else
        {
            return 0;
        }
    }
    public static string ToWords(decimal amount)//convert amount into words --bibin tc
    {
        Int64 rupees;
        Int16 paise;
        int position;
        int count;
        bool divideByHundred;
        string inWords;
        string[] thouands ={
                    "",
                    "Thousand",
                    "Lakh",
                    "Crore",
                  };
        amount = Math.Abs(amount);
        rupees = Convert.ToInt64(Math.Floor(amount));
        paise = Convert.ToInt16((amount - rupees) * 100);
        inWords = "";
        divideByHundred = false;
        count = 0;
        position = 0;
        if (rupees > 0)
        {
            while (rupees > 0)
            {
                if (divideByHundred)
                {
                    position = (int)(rupees % 100);
                    rupees = Convert.ToInt64(rupees / 100);
                }
                else
                {
                    position = (int)(rupees % 1000);
                    rupees = Convert.ToInt64(rupees / 1000);
                    divideByHundred = true;
                }
                if (position > 0)
                {
                    inWords = SplitDenoms(position) + " " + thouands[count] + " " + inWords;
                }
                count++;
            }
            if (paise > 0)
            {
                inWords = inWords + ((inWords.Length > 0) ? " and " : "") + SplitDenoms(paise) + " Paise";
            }
            inWords = "Rupees " + inWords + " Only";
        }
        else
        {
            if (paise > 0)
            {
                inWords = inWords + ((inWords.Length > 0) ? " and " : "") + SplitDenoms(paise) + " Paise Only";
            }

        }
        return inWords;
    }
    public static string SplitDenoms(int position)
    {
        Int64 value;
        string retValue;
        string[] ones ={
                    "",
                    "One",
                    "Two",
                    "Three",
                    "Four",
                    "Five",
                    "Six",
                    "Seven",
                    "Eight",
                    "Nine",
                    "Ten",
                    "Eleven",
                    "Twelve",
                    "Thirteen",
                    "Fourteen",
                    "Fifteen",
                    "Sixteen",
                    "Seventeen",
                    "Eighteen",
                    "Nineteen",
                    "Twenty"
                  };
        string[] tens ={
                    "",
                    "",
                    "Twenty",
                    "Thirty",
                    "Forty",
                    "Fifty",
                    "Sixty",
                    "Seventy",
                    "Eighty",
                    "Ninety"
                };
        value = Convert.ToInt16(position / 100);
        position = (int)position % 100;
        retValue = ((value > 0) ? ones[value] + " Hundred" : "");
        retValue = retValue + (((position >= 1) && (position <= 20)) ? " " + ones[position] : "");
        if (position >= 21 && position <= 99)
        {
            value = Convert.ToInt16(position / 10);
            position = (int)position % 10;
            retValue = retValue + ((value > 0) ? " " + tens[value] : "");
            retValue = retValue + ((position > 0) ? " " + ones[position] : "");
        }
        return retValue;
    }

    public static string DateFormat(DateTime transdate)
    {
        string[] dt = Convert.ToDateTime(transdate).GetDateTimeFormats();
        return (dt[6]);
    }
    public static int TimeFormat(string hour)//...addded by Aswathi.U on 14.10.2014
    {
        int h = 0;
        h = Convert.ToDateTime(hour).Hour;
        return h;

    }

   
}