using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebPharma.Interface;
using WebPharma.DataAccess;
using System.Data;
using WebPharma.Business;


namespace WebPharma.Business
{
  public  class BlReportData:IReportData
    {
        private string _ReportFile;
        private string _SubModule;
        private string _ID;
        private string _SubID;
        private DateTime _FromDate;
        private DateTime _ToDate;
        private string _UserCode;
        private string _ReportCation;
        private DateTime _UserDate;
        private Boolean _Cancelled;
        private Int64 _FK_Customer;
        private Int64 _FK_Client;
        private Int64 _FK_Items;
        private Int64 _FK_Store;
        private Int64 _FK_Users;
        //private Int64 _FK_FromStore;
        //private Int64 _FK_ToStore;
        //private Int64 _FK_Category;
        private string _ReportSchema;
        private string _ReportTable;
        private string _ReportSchemaPath;
        private string _ReportPath;
        private string _DatasetName;
        private string _Critrea1;
        private string _Critrea2;
        private string _Critrea3;
        private string _Critrea4;
        private Int32 _TableCount;
        private string _PrinterName;
        private string _XmlData;
        private Int16 _FinYear;
        private Int16 _ReportPage;
        private string _Title;
        private string _PageTitle;
        private string _HomePageTitle;
        private Int64 _FromStore;
        private Int64 _ToStore;
        private Int64 _FK_Supplier;
        private Int64 _FK_Salesman;
        private byte _PaymentMode;
        private Int64 _FK_AccountHead;
        private Int64 _FK_AccountSubHead;
        private Int64 _FK_AccountGroup;
        private Int64 _FK_AccountSubGroup;
        private DateTime _AsonDate;
        private byte _IssueMode;
        private Int64 _FK_CategoryFirst;
        private Int64 _FK_Counter;
        private Int64 _FK_Shift;
        private Int64 _FK_CustomerGroup;
        private byte _Period;
        private DateTime _CompareDateFrom;
        private DateTime _CompareDateTo;
        private byte _UserAction;
        private string _reportname;
        private string _AuditData;
        private string _UserName;
        private Int64 _FK_Manufacturer;
        private Int64 _FK_Schedule;
        private Int64 _FK_Generic;
        public DateTime CompareDateTo
        {
            get { return _CompareDateTo; }
            set { _CompareDateTo = value; }
        }

        public DateTime CompareDateFrom
        {
            get { return _CompareDateFrom; }
            set { _CompareDateFrom = value; }
        }

        public byte Period
        {
            get { return _Period; }
            set { _Period = value; }
        }


         public BlReportData()
        {
            Initialize();
        }
        public void Initialize()
        {
            _ReportFile = string.Empty;
            _SubModule = string.Empty;
            _ID = string.Empty;
            _SubID = string.Empty;
            _FromDate = DateTime.Now;
            _ToDate = DateTime.Now;
            _UserCode = string.Empty;
            _UserDate = DateTime.Now;
            _Cancelled = false;
            _ReportSchema = string.Empty;
            _ReportTable = string.Empty;
            _ReportSchemaPath = string.Empty;
            _FK_Customer = 0;
            _FK_Client = 1;
            _FK_Items = 0;
            _FK_Store = 0;
            _FK_Users = 0;
            _Period = 0;
            //_FK_FromStore = 0;
            //_FK_ToStore=0;
            //_FK_Category = 0;
            _ReportPath = string.Empty;
            _DatasetName = string.Empty;
            _Critrea1 = string.Empty;
            _Critrea2 = string.Empty;
            _Critrea3 = string.Empty;
            _Critrea4 = string.Empty;
            _TableCount = 0;
            _PrinterName = string.Empty;
            _XmlData = string.Empty;
            _ReportCation = string.Empty;
            _FinYear = 0;
            _ReportPage = 0;
            
           // _HomePageTitle = "Kozhikode Thaluk Sahakarana Karshikolpadana Samskarana Vipanana Sangam Ltd- (D No.3043)";
            DataTable dtTitle = new DataTable();
            dtTitle = SelectTitle();
            if (dtTitle.Rows.Count > 0)
            {
                _Title = dtTitle.Rows[0]["Value"].ToString() == "" ? "0" : dtTitle.Rows[0]["Value"].ToString().Trim();
            }
            else { _Title = ""; }
            _PageTitle = string.Empty;
            _FromStore = 0;
            _ToStore = 0;
            _FK_Supplier = 0;
            _FK_Salesman = 0;
            _PaymentMode = 0;
            _FK_AccountHead = 0;
            _FK_AccountSubHead = 0;
            _FK_AccountGroup = 0;
            _FK_AccountSubGroup = 0;
            _AsonDate = DateTime.Now;
            _IssueMode = 0;
            _FK_CategoryFirst = 0;
            _FK_Counter = 0;
            _FK_Shift = 0;
            _FK_CustomerGroup = 0;
            _UserAction = 1;
            _reportname = string.Empty;
            _AuditData = string.Empty;
            _UserName = string.Empty;
            _FK_Manufacturer = 0;
            _FK_Schedule = 0;
            _FK_Generic = 0;
        }
        public string HomePageTitle
        {
            get { return _HomePageTitle; }
            set { _HomePageTitle = value;}
        }
        public Int64 FK_CustomerGroup
        {
            get { return _FK_CustomerGroup; }
            set { _FK_CustomerGroup = value; }
        }

        public Int64 FK_CategoryFirst
        {
            get { return _FK_CategoryFirst; }
            set { _FK_CategoryFirst = value; }
        }
        public byte IssueMode
        {
            get { return _IssueMode; }
            set { _IssueMode = value; }

        }
        public DateTime AsonDate
        {
            get { return _AsonDate; }
            set { _AsonDate = value; }
        }

        public Int64 FK_AccountHead
        {
            get { return _FK_AccountHead; }
            set { _FK_AccountHead = value; }
        }
        public Int64 FK_AccountSubHead
        {
            get { return _FK_AccountSubHead; }
            set { _FK_AccountSubHead = value; }
        }
        public Int64 FK_AccountGroup
        {
            get { return _FK_AccountGroup; }
            set { _FK_AccountGroup = value; }
        }
        public Int64 FK_AccountSubGroup
        {
            get { return _FK_AccountSubGroup; }
            set { _FK_AccountSubGroup = value; }
        }

        public byte PaymentMode
        {
            get { return _PaymentMode; }
            set { _PaymentMode = value; }
        }

        public Int64 FK_Salesman
        {
            get { return _FK_Salesman; }
            set { _FK_Salesman = value; }
        }
        public Int64 FK_Supplier
        {
            get { return _FK_Supplier; }
            set { _FK_Supplier = value; }
        }
        public Int64 FK_Users
        {
            get { return _FK_Users; }
            set { _FK_Users = value; }
        }
        public Int64 FromStore
        {
            get { return _FromStore; }
            set { _FromStore = value; }
        }
        public Int64 ToStore
        {
            get { return _ToStore; }
            set { _ToStore = value; }
        }
        public string Title
        {
            get { return _Title; }
            set { _Title = value; }
        }
        public string PageTitle
        {
            get { return _PageTitle; }
            set { _PageTitle = value; }
        }
        public Int64 FK_Client
        {
            get { return _FK_Client; }
            set { _FK_Client = value; }
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
        //public Int64 FK_FromStore
        //{
        //    get { return _FK_FromStore; }
        //    set { _FK_FromStore = value; }
        //}
        //public Int64 FK_ToStore
        //{
        //    get { return _FK_ToStore; }
        //    set { _FK_ToStore = value; }
        //}
        //public Int64 FK_Category
        //{
        //    get { return _FK_Category; }
        //    set { _FK_Category = value; }
        //}
        public string ReportPath
        {
            get { return _ReportPath; }
            set { _ReportPath = value; }
        }


        public string DatasetName
        {
            get { return _DatasetName; }
            set { _DatasetName = value; }
        }


        public string Critrea1
        {
            get { return _Critrea1; }
            set { _Critrea1 = value; }
        }


        public string Critrea2
        {
            get { return _Critrea2; }
            set { _Critrea2 = value; }
        }


        public string Critrea3
        {
            get { return _Critrea3; }
            set { _Critrea3 = value; }
        }


        public string Critrea4
        {
            get { return _Critrea4; }
            set { _Critrea4 = value; }
        }


        public Int32 TableCount
        {
            get { return _TableCount; }
            set { _TableCount = value; }
        }


        public string PrinterName
        {
            get { return _PrinterName; }
            set { _PrinterName = value; }
        }


        public string XmlData
        {
            get { return _XmlData; }
            set { _XmlData = value; }
        }


        public Int16 FinYear
        {
            get { return _FinYear; }
            set { _FinYear = value; }
        }


        public Int16 ReportPage
        {
            get { return _ReportPage; }
            set { _ReportPage = value; }
        }
        public Boolean Cancelled
        {
            get { return _Cancelled; }
            set { _Cancelled = value; }
        }

        public string ReportSchema
        {
            get { return _ReportSchema; }
            set { _ReportSchema = value; }
        }

        public string ReportTable
        {
            get { return _ReportTable; }
            set { _ReportTable = value; }
        }

        public string ReportSchemaPath
        {
            get { return _ReportSchemaPath; }
            set { _ReportSchemaPath = value; }
        }

        public Int64 FK_Customer
        {
            get { return _FK_Customer; }
            set { _FK_Customer = value; }
        }
        public DateTime ToDate
        {
            get { return _ToDate; }
            set { _ToDate = value; }
        }


        public string UserCode
        {
            get { return _UserCode; }
            set { _UserCode = value; }
        }


        public string ReportCation
        {
            get { return _ReportCation; }
            set { _ReportCation = value; }
        }


        public DateTime UserDate
        {
            get { return _UserDate; }
            set { _UserDate = value; }
        }
        public string ID
        {
            get { return _ID; }
            set { _ID = value; }
        }
        public string SubID
        {
            get { return _SubID; }
            set { _SubID = value; }
        }
        public DateTime FromDate
        {
            get { return _FromDate; }
            set { _FromDate = value; }
        }
        public string ReportFile
        {
            get { return _ReportFile; }
            set { _ReportFile = value; }
        }
        public string SubModule
        {
            get { return _SubModule; }
            set { _SubModule = value; }
        }
        public Int64 FK_Counter
        {
            get { return _FK_Counter; }
            set { _FK_Counter = value; }
        }
        public Int64 FK_Shift
        {
            get { return _FK_Shift; }
            set { _FK_Shift = value; }
        }
        public byte UserAction
        {
            get { return _UserAction; }
            set { _UserAction = value; }
        }
        public string reportname
        {
            get { return _reportname; }
            set { _reportname = value; }
        }

        public string AuditData
        {
            get { return _AuditData; }
            set { _AuditData = value; }
        }
        public string UserName
        {
            get { return _UserName; }
            set { _UserName = value; }
        }
       
       public Int64 FK_Manufacturer
        {
            get { return _FK_Manufacturer; }
            set { _FK_Manufacturer = value; }
        }
        public Int64 FK_Schedule
        {
            get { return _FK_Schedule; }
            set { _FK_Schedule = value; }
        }
        public Int64 FK_Generic
        {
            get { return _FK_Generic; }
            set { _FK_Generic = value; }
        }
        public DataTable SelectStoreName()
        {
            DataTable dtbl = new DataTable();
            DalReportData dalReport = new DalReportData();
            dtbl = dalReport.SelectStoreName(this);
            dalReport = null;
            return dtbl;
        }
        public DataSet GetDataSource()
        {
            DalReportData dalReportData = new DalReportData();
            DataSet ds = new DataSet();
            try
            {
                ds = dalReportData.GetReportDataSource(this);
                ds.DataSetName = _DatasetName;
                ds.Tables[0].TableName = ReportTable;
                ds.WriteXmlSchema(_ReportSchemaPath + _ReportSchema + ".xsd");
            }
            catch (Exception ex)
            {
            }
            finally
            {
                dalReportData = null;
            }
            return ds;
        }
        public DataTable GetGraphDetail()
        {
            DalReportData dalReportData = new DalReportData();
            DataTable ds = new DataTable();
            try
            {
                ds = dalReportData.GetDashBoardGraphDataSource(this);
            }
            catch (Exception ex)
            {
            }
            finally
            {
                dalReportData = null;
            }
            return ds;

        }
        public DataTable GetAdminDashBoard()
        {
            DalReportData dalReport = new DalReportData();
            DataTable dt = new DataTable();
            try
            {
                dt = dalReport.GetAdminDashBoard(this);
            }
            catch (Exception ex)
            {
            }
            finally
            {
                dalReport = null;
            }
            return dt;
            
        }
        public DataSet GetComparableDashBoard()
        {
            DalReportData dalReport = new DalReportData();
            DataSet ds = new DataSet();
            try
            {
                ds = dalReport.GetComparableGraph(this);
            }
            catch (Exception ex)
            {
            }
            finally
            {
                dalReport = null;
            }
            return ds;
        }
        public DataSet GetStockTransferRequestDataSource(long StatusId)
        {
            DalReportData dalReport = new DalReportData();
            DataSet ds = new DataSet();
            try
            {
                ds = dalReport.GetStockTransferRequestReportData(StatusId);
                ds.DataSetName = _DatasetName;
                ds.Tables[0].TableName = ReportTable;
                ds.WriteXmlSchema(_ReportSchemaPath + _ReportSchema + ".xsd");
            }
            catch (Exception ex)
            {
            }
            finally
            {
                dalReport = null;
            }
            return ds;
        }
        public DataSet RepaymentReportData(long StatusId)
        {
            DalReportData dalReport = new DalReportData();
            DataSet ds = new DataSet();
            try
            {
                ds = dalReport.RepaymentReportData(StatusId);
                ds.DataSetName = _DatasetName;
                ds.Tables[0].TableName = ReportTable;
                ds.WriteXmlSchema(_ReportSchemaPath + _ReportSchema + ".xsd");
            }
            catch (Exception ex)
            {
            }
            finally
            {
                dalReport = null;
            }
            return ds;
        }

        public DataSet StockTransferIssueReceipt(long StatusId)
        {
            DalReportData dalReport = new DalReportData();
            DataSet ds = new DataSet();
            try
            {
                ds = dalReport.StockTransferIssueReceipt(StatusId);
                ds.DataSetName = _DatasetName;
                ds.Tables[0].TableName = ReportTable;
                ds.WriteXmlSchema(_ReportSchemaPath + _ReportSchema + ".xsd");
            }
            catch (Exception ex)
            {
            }
            finally
            {
                dalReport = null;
            }
            return ds;
        }

        public DataSet GetPurchaseDataSource(long StatusId)
        {
            DalReportData dalReport = new DalReportData();
            DataSet ds = new DataSet();
            try
            {
                ds = dalReport.GetPurchaseReportData(StatusId);
                ds.DataSetName = _DatasetName;
                ds.Tables[0].TableName = ReportTable;
                ds.WriteXmlSchema(_ReportSchemaPath + _ReportSchema + ".xsd");
            }
            catch (Exception ex)
            {
            }
            finally
            {
                dalReport = null;
            }
            return ds;
        }
        public DataSet GetPurchaseRecieptDataSource(long StatusId)
        {
            DalReportData dalReport = new DalReportData();
            DataSet ds = new DataSet();
            try
            {
                ds = dalReport.GetPurchaseRecieptDataSource(StatusId);
                ds.DataSetName = _DatasetName;
                ds.Tables[0].TableName = ReportTable;
                ds.WriteXmlSchema(_ReportSchemaPath + _ReportSchema + ".xsd");
            }
            catch (Exception ex)
            {
            }
            finally
            {
                dalReport = null;
            }
            return ds;
        }
        public DataSet GetPurchaseOrderDataSource(long StatusId)
        {
            DalReportData dalReport = new DalReportData();
            DataSet ds = new DataSet();
            try
            {
                ds = dalReport.GetPurchaseOrderDataSource(StatusId);
                ds.DataSetName = _DatasetName;
                ds.Tables[0].TableName = ReportTable;
                ds.WriteXmlSchema(_ReportSchemaPath + _ReportSchema + ".xsd");
            }
            catch (Exception ex)
            {
            }
            finally
            {
                dalReport = null;
            }
            return ds;
        }

        public DataSet GetSalesReturnDataSource(long StatusId)
        {
            DalReportData dalReport = new DalReportData();
            DataSet ds = new DataSet();
            try
            {
                ds = dalReport.GetSalesReturnDataSource(StatusId);
                ds.DataSetName = _DatasetName;
                ds.Tables[0].TableName = ReportTable;
                ds.WriteXmlSchema(_ReportSchemaPath + _ReportSchema + ".xsd");
            }
            catch (Exception ex)
            {
            }
            finally
            {
                dalReport = null;
            }
            return ds;
        }

        public DataSet GetPurchaseReturnDataSource(long StatusId,string Mode)
        {
            DalReportData dalReport = new DalReportData();
            DataSet ds = new DataSet();
            try
            {
                ds = dalReport.GetPurchaseReturnDataSource(StatusId,Mode);
                ds.DataSetName = _DatasetName;
                ds.Tables[0].TableName = ReportTable;
                ds.WriteXmlSchema(_ReportSchemaPath + _ReportSchema + ".xsd");
            }
            catch (Exception ex)
            {
            }
            finally
            {
                dalReport = null;
            }
            return ds;
        }
        public DataSet GetIndentDataSource(long StatusId)
        {
            DalReportData dalReport = new DalReportData();
            DataSet ds = new DataSet();
            try
            {
                ds = dalReport.GetIndentDataSource(StatusId);
                ds.DataSetName = _DatasetName;
                ds.Tables[0].TableName = ReportTable;
                ds.WriteXmlSchema(_ReportSchemaPath + _ReportSchema + ".xsd");
            }
            catch (Exception ex)
            {
            }
            finally
            {
                dalReport = null;
            }
            return ds;
        }
        public DataTable SelectTitle()
        {
            DataTable dtbl = new DataTable();
            DalReportData dalReport = new DalReportData();
            dtbl = dalReport.SelectTitle();
            dalReport = null;
            return dtbl;
        }

        public DataSet Getprintdatasource(long StatusId, string type)
        {
            DalReportData dalReport = new DalReportData();
            DataSet ds = new DataSet();
            try
            {
                ds = dalReport.Getprintdatasource(StatusId, type);
                ds.DataSetName = _DatasetName;
                ds.Tables[0].TableName = ReportTable;
                ds.WriteXmlSchema(_ReportSchemaPath + _ReportSchema + ".xsd");
            }
            catch (Exception ex)
            {
            }
            finally
            {
                dalReport = null;
            }
            return ds;
        }

        public long AuditTrailUpdation()
        {
            _UserAction = 1;
            DalReportData dalReport = new DalReportData();
            long status;
            status = 0;
            try
            {
                status = dalReport.UpdateAuditTrail(this);
                dalReport = null;
            }
            catch (Exception ex)
            {

            }
            return status;
        }
 
    }
}
