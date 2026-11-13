<%@ WebService Language="C#" Class="Textile" %>
/*-------------------------------------------------------------------
Purpose    : Load menu for home page
Author     : Remin M
Copyright  : Perfect Software Solutions (Clt) Pvt Ltd
Created on : 18/02/2014
----------------------------------------------------------------------
WebPharma
OMID	On			By		
----------------------------------------------------------------------
 17/04/2014       Nimmi P  
----------------------------------------------------------------------*/
using System;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Data;
using WebPharma.Business;
using WebPharma.Business.Masters;
using WebPharma.Business.WebPharma;
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
[System.Web.Script.Services.ScriptService]
public class Textile : System.Web.Services.WebService
{

    [WebMethod]
    public string HelloWorld()
    {
        return "Hello World";

    }
    [WebMethod]
    public string FillMenu(string userid, string clientid)
    {
        DataTable dtblModules = new DataTable();
        DataTable dtblPages = new DataTable();
        BlMenu blmenu = new BlMenu();
        Populate populate = new Populate();
        string UserId = populate.Decrypt(HttpUtility.UrlDecode(userid));
        string ClientId = populate.Decrypt(HttpUtility.UrlDecode(clientid));
        dtblModules = blmenu.FillModules();
        string strHtml = "";
        if (dtblModules.Rows.Count > 0)
        {
            //for (int i = 0; i < dtblModules.Rows.Count; i++)
            //{
            //    blmenu.FK_User = Convert.ToInt64(UserId);
            //    blmenu.FK_Client = Convert.ToInt64(ClientId);
            //    blmenu.ID_PageModule = Convert.ToInt32(dtblModules.Rows[i][0].ToString());
            //   dtblPages = blmenu.FillPages();
            //  //  BlUserAccess blUser = new BlUserAccess();
            //  //  DataTable dt = blUser.SelectQuickMenu();
            //    if (dtblPages.Rows.Count > 0)
            //    {


            //        strHtml = strHtml + "<li><a href=\"#\">" + dtblModules.Rows[i][1].ToString() + "</a>";
            //        strHtml = strHtml + "<div>";
            //        for (int j = 0; j < dtblPages.Rows.Count; j++)
            //        {
            //            strHtml = strHtml + "<p>";
            //            strHtml = strHtml + "<a href=\"\" onclick=\"return false;\" class=\"aaf\" id=" + dtblPages.Rows[j][3].ToString() + ">" + dtblPages.Rows[j][2].ToString() + "</a>";
            //            strHtml = strHtml + "</p>";
            //        }
            //        strHtml = strHtml + "</div>";
            //        strHtml = strHtml + "</li>";
            //    }

            //}

            BlUserAccess blUser = new BlUserAccess();
            blUser.FK_Users = Convert.ToInt64(UserId);
            DataTable dt = blUser.SelectQuickMenu();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                //strHtml = strHtml + "<li><a href=\"#\">" + dt.Rows[i][6].ToString() + "</a>" + "<li><a href=\"#\">" + dt.Rows[i][7].ToString() + "</a>";
                // strHtml = strHtml + "<p>";
                strHtml = strHtml + "<li><a href=\"\" onclick=\"return false;\" class=\"aaf\" id=" + dt.Rows[i][12].ToString() + ">" + dt.Rows[i][6].ToString() + "</a>" +
                    "<li><a href=\"\" onclick=\"return false;\" class=\"aaf\" id=" + dt.Rows[i][13].ToString() + ">" + dt.Rows[i][7].ToString() + "</a>" +
                    "<li><a href=\"\" onclick=\"return false;\" class=\"aaf\" id=" + dt.Rows[i][14].ToString() + ">" + dt.Rows[i][8].ToString() + "</a>" +
                "<li><a href=\"\" onclick=\"return false;\" class=\"aaf\" id=" + dt.Rows[i][15].ToString() + ">" + dt.Rows[i][9].ToString() + "</a>" +
                "<li><a href=\"\" onclick=\"return false;\" class=\"aaf\" id=" + dt.Rows[i][16].ToString() + ">" + dt.Rows[i][10].ToString() + "</a>" +
                  "<li><a href=\"\" onclick=\"return false;\" class=\"aaf\" id=" + dt.Rows[i][17].ToString() + ">" + dt.Rows[i][11].ToString() + "</a>";
            }

        }
        blmenu = null;
        return strHtml;

    }
    [WebMethod]
    public string FillMenuLeft(string userid, string clientid)
    {
        DataTable dtblModules = new DataTable();
        DataTable dtblPages = new DataTable();
        BlMenu blmenu = new BlMenu();
        Populate populate = new Populate();
        string UserId = populate.Decrypt(HttpUtility.UrlDecode(userid));
        string ClientId = populate.Decrypt(HttpUtility.UrlDecode(clientid));
        dtblModules = blmenu.FillModules();
        string strHtml = "";
        if (dtblModules.Rows.Count > 0)
        {
            for (int i = 0; i < dtblModules.Rows.Count; i++)
            {
                blmenu.FK_User = Convert.ToInt64(UserId);
                blmenu.FK_Client = Convert.ToInt64(ClientId);
                blmenu.ID_PageModule = Convert.ToInt32(dtblModules.Rows[i][0].ToString());
                dtblPages = blmenu.FillPages();
                if (dtblPages.Rows.Count > 0)
                {

                    strHtml = strHtml + "<li>	<a class=\"head\" href=\"\" onclick=\"return false;\">" + dtblModules.Rows[i][1].ToString() + "</a>";
                    strHtml = strHtml + "<ul style=\"display: none;  width:100%;    \"   >";
                    //strHtml = strHtml + "<li><a href=\"#\">" + dtblModules.Rows[i][1].ToString() + "</a>";
                    //strHtml = strHtml + "<div>";
                    for (int j = 0; j < dtblPages.Rows.Count; j++)
                    {
                        strHtml = strHtml + "<li><a  href=\"\"   onclick=\" return false ;\"  class=\"aaf\"  id=" + dtblPages.Rows[j][3].ToString() + " >" + dtblPages.Rows[j][2].ToString() + "  </a></li>";
                        //strHtml = strHtml + "<p>";
                        //strHtml = strHtml + "<a href=\"\" onclick=\"return false;\" class=\"aaf\" id=" + dtblPages.Rows[j][3].ToString() + ">" + dtblPages.Rows[j][2].ToString() + "</a>";
                        //strHtml = strHtml + "</p>";
                    }

                    strHtml = strHtml + "</ul>";
                    strHtml = strHtml + "</li>";
                    //strHtml = strHtml + "</div>";
                    //strHtml = strHtml + "</li>";

                }

            }
        }
        blmenu = null;
        return strHtml;

    }

    [WebMethod]
    public string FillMenuGreen(string userid, string clientid)
    {
        DataTable dtblModules = new DataTable();
        DataTable dtblPages = new DataTable();
        BlMenu blmenu = new BlMenu();
        Populate populate = new Populate();
        string UserId = populate.Decrypt(HttpUtility.UrlDecode(userid));
        string ClientId = populate.Decrypt(HttpUtility.UrlDecode(clientid));
        dtblModules = blmenu.FillModules();
        string strHtml = "";
        if (dtblModules.Rows.Count > 0)
        {
            for (int i = 0; i < dtblModules.Rows.Count; i++)
            {
                blmenu.FK_User = Convert.ToInt64(UserId);
                blmenu.FK_Client = Convert.ToInt64(ClientId);
                blmenu.ID_PageModule = Convert.ToInt32(dtblModules.Rows[i][0].ToString());
                dtblPages = blmenu.FillPages();
                if (dtblPages.Rows.Count > 0)
                {
                    strHtml = strHtml + "<li>	<a class=\"head\" href=\"\" onclick=\"return false;\">" + dtblModules.Rows[i][1].ToString() + "</a>";
                    strHtml = strHtml + "<ul style=\"display: none\">";
                    for (int j = 0; j < dtblPages.Rows.Count; j++)
                    {
                        strHtml = strHtml + "<li><a href=\"\" onclick=\"return false;\" class=\"aaf\" id=" + dtblPages.Rows[j][3].ToString() + ">" + dtblPages.Rows[j][2].ToString() + "</a></li>";
                    }
                    strHtml = strHtml + "</ul>";
                    strHtml = strHtml + "</li>";
                }

            }
        }
        blmenu = null;
        return strHtml;

    }


    [WebMethod]
    public string GetReminder(string userid, string clientid, string DateTimestr)
    {


        BlReminder blr = new BlReminder();
        blr.FK_Client = Convert.ToInt64(clientid);
        blr.FK_User = Convert.ToInt64(userid);
        blr.ToDate = Convert.ToDateTime(DateTimestr);
        // DateTime dt = DateTime.ParseExact(DateTimestr, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
        //blr.ToDate = dt;
        DataTable dtbl = blr.GetReminderDetails();
        int i = 1;
        string str = "{ 'bindings': [";
        foreach (DataRow dtRow in dtbl.Rows)
        {
            if (i < dtbl.Rows.Count)
                str = str + "{'message' : '" + dtRow["Message"].ToString() + "','title':'" + dtRow["Topic"].ToString() + "'},";
            else
                str = str + "{'message' : '" + dtRow["Message"].ToString() + "','title':'" + dtRow["Topic"].ToString() + "'}";
            i++;
        }
        str = str + " ]}";

        return str;
    }

    [WebMethod]
    public string GetCategoryBaseUnit(long FK_CategoryFirst)
    {

        BlUnit blunit = new BlUnit();
        string str = blunit.GetCategoryBaseUnit(FK_CategoryFirst);
        blunit = null;
        return str;

    }
    [WebMethod]
    public static System.Collections.Generic.List<SupplierDetails> GetChartData()
    {
        BlMaterialReciept blmaterialreciept = new BlMaterialReciept();
        blmaterialreciept.FK_Client = 1;
        blmaterialreciept.ItemName = "Paracetamol";
        DataTable dt = new DataTable();
        dt = blmaterialreciept.SelectPreviousPurchaseDetails();

        System.Collections.Generic.List<SupplierDetails> dataList = new System.Collections.Generic.List<SupplierDetails>();
        foreach (DataRow dtrow in dt.Rows)
        {
            SupplierDetails details = new SupplierDetails();
            details.Suppliername = dtrow[0].ToString();
            details.PurchaseRatr = Convert.ToInt32(dtrow[1]);
            dataList.Add(details);
        }
        return dataList;
    }
    public class SupplierDetails
    {
        public string Suppliername { get; set; }
        public int PurchaseRatr { get; set; }
    }


    [WebMethod]
    public string GetPartNo(string strPartno)
    {
        BlItems blitem = new BlItems();
        blitem.PartNo = strPartno;
        int Count = blitem.GetPartNoExist();
        blitem = null;
        if (Count > 0)
        {
            return "Exists";
        }
        else
        {
            return "";
        }


    }
    [WebMethod]
    public int GetMinItemStockCount(string userid, string clientid)
    {
        BlItems blItem = new BlItems();
        // blItem.FK_Client = Convert.ToInt64(clientid);
        Populate populate = new Populate();
        // blItem.UserCode = userid;
        long UserId = Convert.ToInt64(populate.Decrypt(HttpUtility.UrlDecode(userid)));
        string ClientId = populate.Decrypt(HttpUtility.UrlDecode(clientid));
        //  = UserId;
        blItem.FK_User = UserId;
        DataTable dtp = new DataTable();
        dtp = blItem.selectUsercode();
        blItem.UserCode = dtp.Rows[0]["UseCode"].ToString();
        blItem.FK_Client = Convert.ToInt64(ClientId);
        DataTable dtblNames = new DataTable();
        dtblNames = blItem.GetMinLevelItemStockName();
        int count = dtblNames.Rows.Count;
        return count;
    }
    [WebMethod]
    public int GetReminderCount(string userid, string clientid, string DateTimestr)
    {

        BlReminder blRemind = new BlReminder();
        DataTable dtblNames = new DataTable();
        //blRemind.FK_Client = Convert.ToInt64(clientid);
        blRemind.FK_User = Convert.ToInt64(userid);
        blRemind.ToDate = Convert.ToDateTime(DateTimestr);
        dtblNames = blRemind.GetReminderNew();
        int count = dtblNames.Rows.Count;
        return count;
    }
    [WebMethod]
    public string GetMinItemStockName(string userid, string clientid)
    {
        BlItems blItem = new BlItems();
        // blItem.FK_Client = Convert.ToInt64(clientid);
        Populate populate = new Populate();
        // blItem.UserCode = userid;
        long UserId = Convert.ToInt64(populate.Decrypt(HttpUtility.UrlDecode(userid)));
        blItem.FK_User = UserId;
        string ClientId = populate.Decrypt(HttpUtility.UrlDecode(clientid));
        DataTable dtp = new DataTable();
        dtp = blItem.selectUsercode();
        blItem.UserCode = dtp.Rows[0]["UseCode"].ToString();
        blItem.FK_Client = Convert.ToInt64(ClientId);
        DataTable dtblNames = new DataTable();
        dtblNames = blItem.GetMinLevelItemStockName();

        string strHtml = "";
        if (dtblNames.Rows.Count > 0)
        {
            for (int i = 0; i < dtblNames.Rows.Count; i++)
            {

                if (i % 2 == 0)
                {
                    strHtml = strHtml + "<li class=\"clear\" ></li><li style=\"width:96%%;background-color:white;\"><a href=\"\" onclick=\"return false;\" class=\"notif\" style=\"padding:15px 20px 0px 5px;font-size: 12px;color:#008080;font-weight:bold;width:100%;text-decoration:none;\" id=" + dtblNames.Rows[i][1].ToString() + ">" + dtblNames.Rows[i][0].ToString() + " in " + dtblNames.Rows[i][2].ToString() + " " + dtblNames.Rows[i][3].ToString() + "</a></li>";// "<li>	" + dtblNames.Rows[i][0].ToString() + "";

                    strHtml = strHtml + " </li> ";
                }
                else
                {
                    strHtml = strHtml + "<li class=\"clear\" ></li><li style=\"width:96%%;background-color:#008080;\"><a href=\"\" onclick=\"return false;\" class=\"notif\" style=\"padding:15px 20px 0px 5px;font-size: 12px;color:white;font-weight:bold;width:100%;text-decoration:none;\" id=" + dtblNames.Rows[i][1].ToString() + ">" + dtblNames.Rows[i][0].ToString() + " in " + dtblNames.Rows[i][2].ToString() + " " + dtblNames.Rows[i][3].ToString() + "</a></li>";// "<li>	" + dtblNames.Rows[i][0].ToString() + "";

                    strHtml = strHtml + " </li> ";
                }

            }
        }
        else
        {
            strHtml = strHtml + "<li class=\"clear\" ></li><li><a href=\"\" onclick=\"return false;\" class=\"notif\" style=\"padding:15px 20px 0px 5px;font-size: 12px;\" >" + "No new notification" + "</a></li>";
            strHtml = strHtml + " </li> ";
        }
        blItem = null;
        return strHtml;
    }
    [WebMethod]
    public string GetReminderDetails(string userid, string clientid, string DateTimestr)
    {

        BlReminder blRemind = new BlReminder();
        DataTable dtblNames = new DataTable();
        Populate populate = new Populate();
        // if (userid != null)
        // {

        string UserId = populate.Decrypt(HttpUtility.UrlDecode(userid));
        string ClientId = populate.Decrypt(HttpUtility.UrlDecode(clientid));
        // }

        blRemind.FK_User = Convert.ToInt64(UserId);
        blRemind.ToDate = Convert.ToDateTime(DateTimestr);
        dtblNames = blRemind.GetReminderDetails();
        string strHtml = "";
        if (dtblNames.Rows.Count > 0)
        {
            for (int i = 0; i < dtblNames.Rows.Count; i++)
            {

                //if (i % 2 == 0)
                //{
                //    strHtml = strHtml + "<li class=\"clear\" ></li><li style=\"width:100%;\"><a href=\"\" onclick=\"return false;\" class=\"notif\" style=\"padding:15px 20px 0px 5px;font-size: 12px;color:#0C3A5E;font-weight:bold;background-color:white;width:100%;\" id=" + dtblNames.Rows[i][0].ToString() + ">" + dtblNames.Rows[i][1].ToString() + "</a></li>";// "<li>	" + dtblNames.Rows[i][0].ToString() + "";
                //    strHtml = strHtml + " </li> ";
                //}
                //else
                //{
                //    strHtml = strHtml + "<li class=\"clear\" ></li><li style=\"width:100%;\"><a href=\"\" onclick=\"return false;\" class=\"notif\" style=\"padding:15px 20px 0px 5px;font-size: 12px;color:white;font-weight:bold;background-color:#0C3A5E;width:100%;\" id=" + dtblNames.Rows[i][0].ToString() + ">" + dtblNames.Rows[i][1].ToString() + "</a></li>";// "<li>	" + dtblNames.Rows[i][0].ToString() + "";
                //    strHtml = strHtml + " </li> ";
                //}
                if (i % 2 == 0)
                {
                    strHtml = strHtml + "<li class=\"clear\"  ></li><li style=\"width:99%;background-color:white;\"><a href=\"\" onclick=\"return false;\" class=\"notif\" style=\"padding:15px 20px 0px 5px;font-size: 12px;color:#008080;font-weight:bold;width:100%;text-decoration:none;\" id=" + dtblNames.Rows[i][0].ToString() + ">" + dtblNames.Rows[i][1].ToString() + "</a></li>";// "<li>	" + dtblNames.Rows[i][0].ToString() + "";
                    strHtml = strHtml + " </li> ";
                }
                else
                {
                    strHtml = strHtml + "<li class=\"clear\" ></li><li style=\"width:99%;background-color:#008080;\"><a href=\"\" onclick=\"return false;\" class=\"notif\" style=\"padding:15px 20px 0px 5px;font-size: 12px;color:white;font-weight:bold;width:100%;text-decoration:none;\" id=" + dtblNames.Rows[i][0].ToString() + ">" + dtblNames.Rows[i][1].ToString() + "</a></li>";// "<li>	" + dtblNames.Rows[i][0].ToString() + "";
                    strHtml = strHtml + " </li> ";
                }
            }


        }
        else
        {
            strHtml = strHtml + "<li class=\"clear\" ></li><li><a href=\"\" onclick=\"return false;\" class=\"notif\" style=\"padding:15px 20px 0px 5px;font-size: 12px;\" >" + "No new reminder" + "</a></li>";
            strHtml = strHtml + " </li> ";
        }
        blRemind = null;
        return strHtml;
    }

    [WebMethod]
    public string GetGraphDetail(string ChartType, string Period, string stDate, string endDate, string ClientId, string storeid)
    {

        BlReportData IReportData = new BlReportData();
        IReportData.SubID = ChartType;
        if (Convert.ToByte(Period) == 6)
        {
            IReportData.FromDate = DateTime.Parse(stDate);
            IReportData.ToDate = DateTime.Parse(endDate);

        }
        else if (Convert.ToByte(Period) == 0)
        {
            DateTime date = DateTime.Now;

            DateTime firstDayOfMonth = new DateTime(date.Year, date.Month, 1);
            firstDayOfMonth = firstDayOfMonth.AddMonths(-1);
            string First = firstDayOfMonth.ToString("dd/MM/yyyy");
            var lastDayOfMonth = firstDayOfMonth.AddMonths(1).AddDays(-1);
            string Last = lastDayOfMonth.ToString("dd/MM/yyyy");
            IReportData.FromDate = DateTime.Parse(First);
            IReportData.ToDate = DateTime.Parse(Last);
        }
        else if (Convert.ToByte(Period) == 1)
        {
            DateTime date = DateTime.Now;
            //  var firstDayOfMonth = new DateTime(date.Year, date.Month, date.Day - 7);
            var firstDayOfMonth = date.AddDays(-7);
            string First = firstDayOfMonth.ToString("dd/MM/yyyy");
            var lastDayOfMonth = firstDayOfMonth.AddDays(7);
            string Last = lastDayOfMonth.ToString("dd/MM/yyyy");
            IReportData.FromDate = DateTime.Parse(First);
            IReportData.ToDate = date;
        }
        else if (Convert.ToByte(Period) == 2)
        {
            DateTime date = DateTime.Now;
            var firstDayOfMonth = date.AddDays(-14);
            //        var firstDayOfMonth = new DateTime(date.Year, date.Month, date.Day - 14);
            string First = firstDayOfMonth.ToString("dd/MM/yyyy");
            var lastDayOfMonth = firstDayOfMonth.AddDays(14);
            string Last = lastDayOfMonth.ToString("dd/MM/yyyy");
            IReportData.FromDate = DateTime.Parse(First);
            IReportData.ToDate = date;

        }
        else if (Convert.ToByte(Period) == 3)
        {
            DateTime date = DateTime.Now;
            //var firstDayOfMonth = new DateTime(date.Year, date.Month-3, date.Day);
            var firstDayOfMonth = date.AddMonths(-3);
            string First = firstDayOfMonth.ToString("dd/MM/yyyy");
            var lastDayOfMonth = firstDayOfMonth.AddMonths(3);
            string Last = lastDayOfMonth.ToString("dd/MM/yyyy");
            IReportData.FromDate = DateTime.Parse(First);
            IReportData.ToDate = date;

        }
        else if (Convert.ToByte(Period) == 4)
        {
            DateTime date = DateTime.Now;
            var firstDayOfMonth = date.AddMonths(-6);
            //  var firstDayOfMonth = new DateTime(date.Year, date.Month - 6, date.Day);
            string First = firstDayOfMonth.ToString("dd/MM/yyyy");
            var lastDayOfMonth = firstDayOfMonth.AddMonths(6);
            string Last = lastDayOfMonth.ToString("dd/MM/yyyy");
            IReportData.FromDate = DateTime.Parse(First);
            IReportData.ToDate = date;
        }
        else if (Convert.ToByte(Period) == 5)
        {
            DateTime date = DateTime.Now;
            var firstDayOfMonth = date.AddYears(-1);
            // var firstDayOfMonth = new DateTime(date.Year-1, date.Month, date.Day);
            string First = firstDayOfMonth.ToString("dd/MM/yyyy");
            var lastDayOfMonth = firstDayOfMonth.AddYears(1);
            string Last = lastDayOfMonth.ToString("dd/MM/yyyy");
            IReportData.FromDate = DateTime.Parse(First);
            IReportData.ToDate = date;

        }
        else
        {
            IReportData.FromDate = DateTime.Parse(stDate);
            IReportData.ToDate = DateTime.Parse("1/1/1900");

        }
        IReportData.FK_Client = Convert.ToInt64(ClientId.Split(',')[0]);
        IReportData.FK_Store = Convert.ToInt64(storeid.Split(',')[0]);
        if (ChartType == "STR")
        {
            IReportData.FK_Store = 0;
        }
        IReportData.Period = Convert.ToByte(Period);
        DataTable dtbl = IReportData.GetAdminDashBoard();


        int i = 1;
        string str = "[";
        string per = "";
        if (dtbl.Rows.Count > 0)
        {
            if (ChartType == "NOSL")
            {
                if (dtbl.Rows.Count > 0)
                {
                    per = dtbl.Rows[0][0].ToString();
                }
            }
            else if (ChartType == "MSW")
            {
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


            }
            else if (ChartType == "SLMW")
            {
                foreach (DataRow dtRow in dtbl.Rows)
                {
                    if (i < dtbl.Rows.Count)
                    {
                        str += "\n {ename:'" + dtRow[3].ToString() + "',amount:'" + dtRow[1].ToString() + "',y:" + dtRow[2].ToString() + ",name:'" + dtRow[0].ToString() + "'},\n";
                    }
                    else
                    {
                        str += "\n {ename:'" + dtRow[3].ToString() + "',amount:'" + dtRow[1].ToString() + "',y:" + dtRow[2].ToString() + ",name:'" + dtRow[0].ToString() + "'}";
                    }
                    i++;
                }
            }
            else if (ChartType == "CRPW" || ChartType == "CUST" || ChartType == "CRCW")
            {
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
            }
            else if (ChartType == "STR")
            {
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
            }
            else
            {

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
            }
            str += "]";
            if (ChartType == "NOSL")
                return per;
            else
                return str;
        }
        else
        {
            str += "]";
            return str;
        }


    }
    [WebMethod]
    public string GetComparableData(string SubID, string dtFromDate, string dtToDate, long FK_Client, string CompareFrom, string dtCompareTo)
    {
        BlReportData IReportData = new BlReportData();
        IReportData.SubID = SubID;
        IReportData.FromDate = DateTime.Parse(dtFromDate);
        IReportData.ToDate = DateTime.Parse(dtToDate);
        IReportData.FK_Client = FK_Client;
        IReportData.FK_Store = 0;
        IReportData.CompareDateFrom = DateTime.Parse(CompareFrom);
        IReportData.CompareDateTo = DateTime.Parse(dtCompareTo);
        DataSet ds = IReportData.GetComparableDashBoard();

        int i = 1, c = 1;
        string strPerio1 = IReportData.FromDate.ToString("dd/MM/yyyy") + "-" + IReportData.ToDate.ToString("dd/MM/yyyy");
        string strPeriod2 = IReportData.CompareDateFrom.ToString("dd/MM/yyyy") + "-" + IReportData.CompareDateTo.ToString("dd/MM/yyyy");

        string strlabel = "dataLabels: {  enabled: true, rotation: -90, color: 'rgb(252, 253, 214)',align: 'right',x: 4,  y: 10,   style: {  fontFamily: 'Verdana, sans-serif', textShadow: '0 0 3px black'}}";
        string str = "[{name:'" + strPerio1 + "',data:[";
        if (ds.Tables.Count > 0)
        {
            if (ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dtRow in ds.Tables[0].Rows)
                {
                    if (i < ds.Tables[0].Rows.Count)
                    {
                        str += "\n {period:'" + strPerio1 + "',Amount:'" + dtRow[3].ToString() + "',y:" + dtRow[2].ToString() + ",name:'" + dtRow[1].ToString() + "'},\n";
                    }
                    else
                    {
                        str += "\n {period:'" + strPerio1 + "',Amount:'" + dtRow[3].ToString() + "',y:" + dtRow[2].ToString() + ",name:'" + dtRow[1].ToString() + "'}]," + strlabel;
                    }
                    i++;
                }
                str += "},";
            }
            else
            {
                str += "]},";
            }

            str += "{name:'" + strPeriod2 + "',data:[";
            if (ds.Tables.Count > 1)
            {
                if (ds.Tables[1].Rows.Count > 0)
                {
                    foreach (DataRow dtRow in ds.Tables[1].Rows)
                    {
                        if (c < ds.Tables[1].Rows.Count)
                        {
                            str += "\n {period:'" + strPeriod2 + "',Amount:'" + dtRow[3].ToString() + "',y:" + dtRow[2].ToString() + ",name:'" + dtRow[1].ToString() + "'},\n";
                        }
                        else
                        {
                            str += "\n {period:'" + strPeriod2 + "',Amount:'" + dtRow[3].ToString() + "',y:" + dtRow[2].ToString() + ",name:'" + dtRow[1].ToString() + "'}]," + strlabel;
                        }
                        c++;
                    }
                    str += "}]";
                }
                else
                {
                    str += "]}]";
                }

            }
            else
            {
                str += "]}]";
            }
        }
        else
        {
            str += "}]";
        }
        return str;
    }
    [WebMethod]
    public string GetCustomer(string stDate, string clientid, string FK_Store)
    {

        BlReportData IReportData = new BlReportData();
        IReportData.SubID = "CUST";
        //if (Convert.ToByte(ddlType.SelectedValue) == 6)
        //{
        //    IReportData.FromDate = DateTime.Parse(txtFromDate.Text);
        //    IReportData.ToDate = DateTime.Parse(txtToDate.Text);

        //}
        //else
        //{
        //}


        //IReportData.ToDate = new DateTime(IReportData.FromDate.Year, IReportData.FromDate.Month, DateTime.DaysInMonth(IReportData.FromDate.Year, IReportData.FromDate.Month));
        IReportData.FromDate = DateTime.Parse(stDate);
        IReportData.ToDate = DateTime.Parse("1/1/1900");


        IReportData.FK_Client = Convert.ToInt64(clientid);
        IReportData.FK_Store = Convert.ToInt64(FK_Store);
        IReportData.Period = 0;
        DataTable dtbl = IReportData.GetAdminDashBoard();


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
            } i++;
        }
        str += "]";
        return str;


    }

    [WebMethod]
    public string GetHelpContents(string strpath)
    {
        DataTable dtblModules = new DataTable();
        DataTable dtblPages = new DataTable();
        BlMenu blmenu = new BlMenu();
        Populate populate = new Populate();
        blmenu.Url = strpath;
        dtblModules = blmenu.FillPageHelps();

        string strHtml = "";
        if (dtblModules.Rows.Count > 0)
        {

            strHtml = strHtml + "<div class=\"container\" style='color: #2a3b3b;'><div class=\"row\"><h3>" + dtblModules.Rows[0][0].ToString() + "</h3></div></div>";
            strHtml = strHtml + "<div class=\"row\"><P>" + dtblModules.Rows[0][1].ToString() + "</P></div>";
            strHtml = strHtml + "<div class=\"row\"><P>" + dtblModules.Rows[0][2].ToString() + "</P></div>";
            strHtml = strHtml + "<div class=\"row\"><P>" + dtblModules.Rows[0][3].ToString() + "</P></div>";

        }




        return strHtml;

    }
    [WebMethod]
    public string GetUrlEncrypt(string ItemName, string hfUser, string hfClient, string ddlCategory, string ddlSupplier, string txtInvoiceDate, string hfItemvalue, string ItemType)
    {
        Security security = new Security();
        string strHtml = "";

        string ItemNameEncr = HttpUtility.UrlEncode(security.UrlEncrypt(ItemName));
        string hfUserEncr = HttpUtility.UrlEncode(security.UrlEncrypt(hfUser));
        string hfClientEncr = HttpUtility.UrlEncode(security.UrlEncrypt(hfClient));
        string ddlCategoryEncr = HttpUtility.UrlEncode(security.UrlEncrypt(ddlCategory));
        string ddlSupplierEncr = HttpUtility.UrlEncode(security.UrlEncrypt(ddlSupplier));
        string txtInvoiceDateEncr = HttpUtility.UrlEncode(security.UrlEncrypt(txtInvoiceDate));
        string hfItemvalueEncr = HttpUtility.UrlEncode(security.UrlEncrypt(hfItemvalue));
        string ItemTypeEncr = HttpUtility.UrlEncode(security.UrlEncrypt(ItemType));

        strHtml = "ItemType=" + ItemTypeEncr + "&uidPop=" + hfUserEncr + "&clntPop=" + hfClientEncr + "&itn=" + ItemNameEncr + "&ctgry=" + ddlCategoryEncr + "&Supplier=" + ddlSupplierEncr + "&Date=" + txtInvoiceDateEncr + "&ID=" + hfItemvalueEncr;

        return strHtml;

    }
    [WebMethod]
    public string GetUrlEncryptStock(string ItemName, string hfUser, string hfClient, string ddlCategory, string txtSupRefno, string hfStore)
    {
        Security security = new Security();
        string strHtml = "";

        string ItemNameEncr = HttpUtility.UrlEncode(security.UrlEncrypt(ItemName));
        string hfUserEncr = HttpUtility.UrlEncode(security.UrlEncrypt(hfUser));
        string hfClientEncr = HttpUtility.UrlEncode(security.UrlEncrypt(hfClient));
        string ddlCategoryEncr = HttpUtility.UrlEncode(security.UrlEncrypt(ddlCategory));
        string txtSupRefnoEncr = HttpUtility.UrlEncode(security.UrlEncrypt(txtSupRefno));
        string hfStoreEncr = HttpUtility.UrlEncode(security.UrlEncrypt(hfStore));
        


        strHtml = "uid=" + hfUserEncr + "&clnt=" + hfClientEncr + "&itn=" + ItemNameEncr + "&ctgry=" + ddlCategoryEncr + "&suplr=" + txtSupRefnoEncr + "&str=" + hfStoreEncr ;

        return strHtml;

    }
    [WebMethod]
    public string GetUrlEncryptStockReturn(string ItemName, string hfUser, string hfClient, string ddlCategory, string txtSupRefno, string hfStore, string expdt,string mode)
    {
        Security security = new Security();
        string strHtml = "";

        string ItemNameEncr = HttpUtility.UrlEncode(security.UrlEncrypt(ItemName));
        string hfUserEncr = HttpUtility.UrlEncode(security.UrlEncrypt(hfUser));
        string hfClientEncr = HttpUtility.UrlEncode(security.UrlEncrypt(hfClient));
        string ddlCategoryEncr = HttpUtility.UrlEncode(security.UrlEncrypt(ddlCategory));
        string txtSupRefnoEncr = HttpUtility.UrlEncode(security.UrlEncrypt(txtSupRefno));
        string hfStoreEncr = HttpUtility.UrlEncode(security.UrlEncrypt(hfStore));
        string hfexpDte = HttpUtility.UrlEncode(security.UrlEncrypt(expdt));
        string Expmode = HttpUtility.UrlEncode(security.UrlEncrypt(mode));

        strHtml = "uid=" + hfUserEncr + "&clnt=" + hfClientEncr + "&itn=" + ItemNameEncr + "&ctgry=" + ddlCategoryEncr + "&suplr=" + txtSupRefnoEncr + "&str=" + hfStoreEncr + "&expdt=" + hfexpDte + "&Expmode=" + Expmode;

        return strHtml;

    }
    [WebMethod]
    public string GetUrlEncryptStockSR(string ItemName, string hfUser, string hfClient, string txtCustRefno, string hfStore, string hfNtRtrn, string mode)
    {
        Security security = new Security();
        string strHtml = "";

        string ItemNameEncr = HttpUtility.UrlEncode(security.UrlEncrypt(ItemName));
        string hfUserEncr = HttpUtility.UrlEncode(security.UrlEncrypt(hfUser));
        string hfClientEncr = HttpUtility.UrlEncode(security.UrlEncrypt(hfClient));
        string txtCustRefnoEncr = HttpUtility.UrlEncode(security.UrlEncrypt(txtCustRefno));
        string hfStoreEncr = HttpUtility.UrlEncode(security.UrlEncrypt(hfStore));
        string hfNtRtrnEncr = HttpUtility.UrlEncode(security.UrlEncrypt(hfNtRtrn));
        string Expmode = HttpUtility.UrlEncode(security.UrlEncrypt(mode));
        strHtml = "uid=" + hfUserEncr + "&clnt=" + hfClientEncr + "&itn=" + ItemNameEncr + "&suplr=" + txtCustRefnoEncr + "&str=" + hfStoreEncr + "&NtRtrn=" + hfNtRtrnEncr+"&Expmode=" + Expmode;

        return strHtml;

    }
}
   