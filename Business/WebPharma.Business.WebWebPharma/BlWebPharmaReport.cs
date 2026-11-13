using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebPharma.Interface.WebWebPharma;

namespace WebPharma.Business.WebWebPharma
{
   

 public  class BlWebPharmaReport:IWebPharmaReport
    {
        private string _ReportSchema;      
        private Int16 _ReportIndex;
        private Int16 _ReportOption;
        private string _ReportFile;
        private string _ReportTitle;
        private string _ReportSubTitle;        
        private string _SchemaPath;  
  

        private string _SubModule;  
     
        private DateTime _FromDate;      
        private DateTime _ToDate;       
        private Int64 _FK_Client;
        private string _UserCode;
        private Int64 _FK_CategoryFirst;
        private Int64 _FK_Store;       
        private Int64 _FK_Items;      
        private Int64 _FK_Supplier;


     public BlWebPharmaReport()
     {
         Initialize();
     }
     public void Initialize()
     {
         _SubModule = string.Empty;
         _FromDate = DateTime.Now;
         _ToDate = DateTime.Now;
         _FK_Client = 1;
         _UserCode = string.Empty;
         _FK_CategoryFirst = 0;
         _FK_Items = 0;
         _FK_Store = 0;
         _FK_Supplier = 0;         
     }



     public Int16 ReportOption
     {
         get { return _ReportOption; }
         set { _ReportOption = value; }
     }
     public string ReportSchema
     {
         get { return _ReportSchema; }
         set { _ReportSchema = value; }
     }
     public Int16 ReportIndex
     {
         get { return _ReportIndex; }
         set { _ReportIndex = value; }
     }
     public string ReportFile
     {
         get { return _ReportFile; }
         set { _ReportFile = value; }
     }

     public string ReportTitle
     {
         get { return _ReportTitle; }
         set { _ReportTitle = value; }
     }
     public string ReportSubTitle
     {
         get { return _ReportSubTitle; }
         set { _ReportSubTitle = value; }
     }

     public string SchemaPath
     {
         get { return _SchemaPath; }
         set { _SchemaPath = value; }
     }
     public DateTime FromDate
     {
         get { return _FromDate; }
         set { _FromDate = value; }
     }
     public string SubModule
     {
         get { return _SubModule; }
         set { _SubModule = value; }
     }
     public Int64 FK_Supplier
     {
         get { return _FK_Supplier; }
         set { _FK_Supplier = value; }
     }
     public Int64 FK_Items
     {
         get { return _FK_Items; }
         set { _FK_Items = value; }
     }
     public Int64 FK_Store
     {
         get { return _FK_Store; }
         set { _FK_Store = value; }
     }

     public Int64 FK_Category
     {
         get { return _FK_CategoryFirst; }
         set { _FK_CategoryFirst = value; }
     }

     public string UserCode
     {
         get { return _UserCode; }
         set { _UserCode = value; }
     }

     public Int64 FK_Client
     {
         get { return _FK_Client; }
         set { _FK_Client = value; }
     }
     public DateTime ToDate
     {
         get { return _ToDate; }
         set { _ToDate = value; }
     }


     public void InitializeReport()
     {
         _ReportFile = "";

     }
    }
}
