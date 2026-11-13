using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebPharma.Interface;
using System.Data;
using System.Collections;
using WebPharma.DataAccess;

namespace WebPharma.Business
{
   public class BlMenu:IMenu
    {
       private long _ID_PageModule;
       private string _ModuleName;
       private long _ID_Pages;
       private string _PageName;
       private string _Url;
       private Int64 _FK_User;
       private Int64 _FK_Client;

       public BlMenu()
        {
            Initialize();
        }
       public void Initialize()
       {
           _ID_PageModule = 0;
           _ModuleName = string.Empty;
           _ID_Pages = 0;
           _PageName = string.Empty;
           _Url = string.Empty;
           _FK_User = 0;
           _FK_Client = 0;
       }

       public long ID_PageModule
       {
           get { return _ID_PageModule; }
           set { _ID_PageModule = value; }
       }
       public string ModuleName
       {
           get { return _ModuleName; }
           set { _ModuleName = value; }
       }
       public long ID_Pages
       {
           get { return _ID_Pages; }
           set { _ID_Pages = value; }
       }
       public string PageName
       {
           get { return _PageName; }
           set { _PageName = value; }
       }
       public string Url
       {
           get { return _Url; }
           set { _Url = value; }
       }
       public long FK_User
       {
           get { return _FK_User; }
           set { _FK_User = value; }
       }
       public long FK_Client
       {
           get { return _FK_Client; }
           set { _FK_Client = value; }
       }

       public DataTable FillModules()
       {
           DataTable dtbl = new DataTable();
           DalMenu dalmenu = new DalMenu();
           dtbl = dalmenu.FillModules();
           dalmenu = null;
           return dtbl;
       }
       public DataTable FillPages()
       {
           DataTable dtbl = new DataTable();
           DalMenu dalmenu = new DalMenu();
           dtbl = dalmenu.FillPages(this);
           dalmenu = null;
           return dtbl;
       }
       public DataTable FillPageHelps()
       {
           DataTable dtbl = new DataTable();
           DalMenu dalmenu = new DalMenu();
           dtbl = dalmenu.FillHelpContent(this);
           dalmenu = null;
           return dtbl;
       }

    }
}
