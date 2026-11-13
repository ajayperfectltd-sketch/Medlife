/*----------------------------------- Business Layer Class -----------------------------------------------------------------------------------------------------------------------
Purpose    : 
Author     : Bibin Tc
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
using System.Text;
using System.Collections;
using System.Data;
using System.Threading.Tasks;
using WebPharma.DataAccess.WebPharma;
using WebPharma.Interface.WebPharma;
 

namespace WebPharma.Business.WebPharma
{
    public class BlIndentIssue:IIndentIssue
    {
        public BlIndentIssue()
        {
            Initialize();
        }
       
        #region Properties
        private byte _UserAction;
        private Int16 _PageIndex;
        private Int16 _PageSize;
        private long _FK_Client;
        private long _FK_FromStore;
        private long _FK_ToStore;

        private Int16 _PriceTag;
        private long _PostingId;
        private long _FK_IndentRequest;
        private Boolean _Posted;
        private string _Remarks;
        private bool _Closed;
        private string _ClosedUser;
        private string _ClosedRemarks;
        private bool _Cancelled;
        private string _CancelledUser;
        private DateTime _CancelledOn;
        private string _XmlPurchaseItems;
        private DateTime _FromDate;
        private DateTime _ToDate;
        private DateTime _EnteredOn;
        private string _UserCode;

        private bool _Approved;
        private string _ApprovedUser;
        private DateTime _ApprovedOn;


        private string _DatasetName;
        private string _ReportSchema;
        private string _ReportTable;
        private string _ReportSchemaPath;
        private string _ReportPath;
        private string _ReportFile;

        public void Initialize()
        {
            _UserAction = 1;
            _FK_FromStore = 0;
            _FK_Client = 1;
            _PageIndex = 1;
            _PageSize = 10;
            _PONo = "";
            _Date = DateTime.Now;      
      
           
            _Remarks = string.Empty;
            _Closed = false;
            _ClosedUser = "";
            _ClosedRemarks = "";
            _Cancelled = false;
            _CancelledUser = "";
            _CancelledOn = DateTime.Now;
            _FromDate = DateTime.Parse("01/01/1900");
            _ToDate = DateTime.Parse("1/1/9999");
            _isClosed = "";
            _UserCode = "";
            _PriceTag = 0;

            _Posted = false;
            _Approved = false;
            _ApprovedUser = "";
            _ApprovedOn = DateTime.Now;
            _FK_ToStore = 0;
            _FK_IndentRequest = 0;
            _PostingId = 0;

            _ReportPath = string.Empty;
            _ReportSchema = string.Empty;
            _ReportSchemaPath = string.Empty;
            _ReportTable = string.Empty;
            _ReportFile = string.Empty;
            _DatasetName = string.Empty;
        }
        public long PostingId
        {
            get { return _PostingId; }
            set { _PostingId = value; }
        } 

        public long FK_IndentRequest
        {
            get { return _FK_IndentRequest; }
            set { _FK_IndentRequest = value; }
        }
        public Int16 PriceTag
        {
            get { return _PriceTag; }
            set { _PriceTag = value; }
        }


        public long FK_ToStore
        {
            get { return _FK_ToStore; }
            set { _FK_ToStore = value; }
        }




        public Boolean Posted
        {
            get { return _Posted; }
            set { _Posted = value; }
        }

   
        private string _isClosed;

        public string isClosed
        {
            get { return _isClosed; }
            set { _isClosed = value; }
        }
        private string _PONo;
      

      
        public string PONo
        {
            get { return _PONo; }
            set { _PONo = value; }
        }
        private DateTime _Date;

        public DateTime Date
        {
            get { return _Date; }
            set { _Date = value; }
        }
     
      

         public DateTime ApprovedOn
         {
             get { return _ApprovedOn; }
             set { _ApprovedOn = value; }
         }

         public string ApprovedUser
         {
             get { return _ApprovedUser; }
             set { _ApprovedUser = value; }
         }
       
         public bool Approved
         {
             get { return _Approved; }
             set { _Approved = value; }
         }
      
         public string UserCode
         {
             get { return _UserCode; }
             set { _UserCode = value; }
         }
     
         private long _ID_IndentIssue;

         public DateTime EnteredOn
         {
             get { return _EnteredOn; }
             set { _EnteredOn = value; }
         }
         public long MasterId
         {
             get { return _ID_IndentIssue; }
             set { _ID_IndentIssue = value; }
         }



        //private DateTime 
        public Int16 PageIndex
        {
            get { return _PageIndex; }
            set { _PageIndex = value; }
        }
        public Int16 PageSize
        {
            get { return _PageSize; }
            set { _PageSize = value; }
        }
        public long FK_FromStore
        {
            get { return _FK_FromStore; }
            set { _FK_FromStore = value; }
        }
        public long FK_Client
        {
            get { return _FK_Client; }
            set { _FK_Client = value; }
        }
       

        public byte UserAction
        {
            get { return _UserAction; }
            set { _UserAction = value; }
        }

     
    
       
        public string Remarks
        {
            get { return _Remarks; }
            set { _Remarks = value; }
        }
 
       
  
        public bool Closed
        {
            get { return _Closed; }
            set { _Closed = value; }
        }
        public string ClosedUser
        {
            get { return _ClosedUser; }
            set { _ClosedUser = value; }
        }
        public string ClosedRemarks
        {
            get { return _ClosedRemarks; }
            set { _ClosedRemarks = value; }
        }
        public bool Cancelled
        {
            get { return _Cancelled; }
            set { _Cancelled = value; }
        }
        public string CancelledUser
        {
            get { return _CancelledUser; }
            set { _CancelledUser = value; }
        }
        public DateTime CancelledOn
        {
            get { return _CancelledOn; }
            set { _CancelledOn = value; }
        }
        public string XmlPurchaseItems
        {
            get { return _XmlPurchaseItems; }
            set { _XmlPurchaseItems = value; }
        }
        public DateTime FromDate
        {
            get { return _FromDate; }
            set { _FromDate = value; }
        }

        public DateTime ToDate
        {
            get { return _ToDate; }
            set { _ToDate = value; }
        }

        public string ReportFile
        {
            get { return _ReportFile; }
            set { _ReportFile = value; }
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
        #endregion
        #region Process
        public long InsertData()
        {
            long status;
            DalIndentIssue dalPurchase = new DalIndentIssue();
            status = 0;
            try
            {
                _UserAction = 1;
                status = dalPurchase.UpdateIndentIssue(this);
                dalPurchase = null;
            }
            catch (Exception ex)
            {

            }
            return status;
        }
        public long UpdateData()
        {
            
            DalIndentIssue dalPurchase = new DalIndentIssue();
            long status;
            status = 0;
            try
            {
                _UserAction = 2;
                status = dalPurchase.UpdateIndentIssue(this);
                dalPurchase = null;
            }
            catch (Exception ex)
            {

            }
            return status;
        }
        public DataTable SelectIndentRequestItemsForIssue()
        {
            DataTable dtbl = new DataTable();
            DalIndentIssue dalPurchase = new DalIndentIssue();
            dtbl = dalPurchase.SelectIndentRequestItemsForIssue(this);
            dalPurchase = null;
            return dtbl;
        }
        public DataTable SelectIndentIssueItems()
        {
            DataTable dtbl = new DataTable();
            DalIndentIssue dalPurchase = new DalIndentIssue();
            dtbl = dalPurchase.SelectIndentIssueItems(this);
            dalPurchase = null;
            return dtbl;
        }
        public DataSet StockTransferIssueReceipt(long StatusId)
        {
            DalIndentIssue dalIndentIssue = new DalIndentIssue();
            DataSet ds = new DataSet();
            try
            {
                ds = dalIndentIssue.StockTransferIssueReceipt(StatusId);
                ds.DataSetName = _DatasetName;
                ds.Tables[0].TableName = ReportTable;
                ds.WriteXmlSchema(_ReportSchemaPath + _ReportSchema + ".xsd");
            }
            catch (Exception ex)
            {
            }
            finally
            {
                dalIndentIssue = null;
            }
            return ds;
        }
        public DataTable SelectAllData()
        {
            DataTable dtbl = new DataTable();
            DalIndentIssue dalPurchase = new DalIndentIssue();
            dtbl = dalPurchase.SelectAllIndentIssue(this);
            dalPurchase = null;
            return dtbl;
        }
        public void SelectData()
        {
            DalIndentIssue dalPurchase = new DalIndentIssue();
            IIndentIssue IPurchase;
            IPurchase = (IIndentIssue)dalPurchase.SelectData(this);         
            _Date = IPurchase.Date;               
            _FK_FromStore = IPurchase.FK_FromStore;
            _FK_ToStore = IPurchase.FK_ToStore;
            _FK_IndentRequest = IPurchase.FK_IndentRequest;
            _PostingId = IPurchase.PostingId;
            _Remarks = IPurchase.Remarks;  
            dalPurchase = null;

        }
        #endregion
    }

    public class IIndentIssueDetails
    {
        /*private Int64 _FK_IndentIssue;

        public Int64 FK_IndentIssue
        {
            get { return _FK_IndentIssue; }
            set { _FK_IndentIssue = value; }
        }*/
        private Int64 _FK_Items;
        private Int64 _FK_Units;
        private Int64 _FK_Stock;

        public Int64 FK_Stock
        {
            get { return _FK_Stock; }
            set { _FK_Stock = value; }
        }

        public Int64 FK_Units
        {
            get { return _FK_Units; }
            set { _FK_Units = value; }
        }
        public Int64 FK_Items
        {
            get { return _FK_Items; }
            set { _FK_Items = value; }
        }
        private decimal _Quantity;

        public decimal Quantity
        {
            get { return _Quantity; }
            set { _Quantity = value; }
        }
        private decimal _SalesPrice;

        public decimal SalesPrice
        {
            get { return _SalesPrice; }
            set { _SalesPrice = value; }
        }
        private decimal _PurchaseRate;

        public decimal PurchaseRate
        {
            get { return _PurchaseRate; }
            set { _PurchaseRate = value; }
        }
        private decimal _DiscountItem;

        public decimal DiscountItem
        {
            get { return _DiscountItem; }
            set { _DiscountItem = value; }
        }
        private string _BatchNo;

        public string BatchNo
        {
            get { return _BatchNo; }
            set { _BatchNo = value; }
        }
        private string _Notes;

        public string Notes
        {
            get { return _Notes; }
            set { _Notes = value; }
        }
    
    
        
    }
}
