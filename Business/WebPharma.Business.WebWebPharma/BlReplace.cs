using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebPharma.DataAccess.WebPharma;
using WebPharma.Interface.WebPharma;

namespace WebPharma.Business.WebPharma
{
   public class BlReplace:IReplace
    {

       public void IReplace()

      {
          Initialize();
      }
       public void Initialize()
       {
           _UserAction = 1;
           _UserCode = "";
           _EnteredOn = DateTime.Now;
           _FK_Client = 1;
           _MasterId = 0;
           _PageIndex = 1;
           _PageSize = 10;
           _ReplaceID = 0;
           _RecivedDate = DateTime.Now;
           _Linkbtn = false;
           _Fk_PurchaseReturn = 0;
           _Fk_BillType = 0;
           _BillNo = string.Empty;
           _FK_Store = 0;
           _InvoiceNo = string.Empty;
           _InvoiceDate = DateTime.Now;
           _FK_Supplier = 0;
           _xmlPurchseValues = string.Empty;
           _xmlValues = string.Empty;
           _NetAmount = 0;
           _Fk_Reason = 0;
           _FTotalAmount = 0;
           _FDiscPer = 0;
           _FDiscAmnt = 0;
           _FnetAmnt = 0;
           _TtotalAmount = 0;
           _TDiscPer = 0;
           _TDiscAmnt = 0;
           _TnetAmnt = 0;
           _Cancelled = false;
           _CancelledUser = "";
           _CancelledOn = DateTime.Now;
           _Cancelled = false;
           _Approved = true;
           _TransactionID = 0;
           _ApproveorPending = 2;
           _FromDate = DateTime.Now;
           _ToDate = DateTime.Now;
       }
       private byte _UserAction;
       private Int64 _FK_Client;
       private Int64 _MasterId;
       private Int16 _PageIndex;
       private Int16 _PageSize;
       private Int64 _ReplaceID;
       private DateTime _RecivedDate;
       private Boolean _Linkbtn;
       private Int64 _Fk_PurchaseReturn;
       private Int64 _Fk_BillType;
       private string _BillNo;
       private Int64 _FK_Store;
       private string _InvoiceNo;
       private DateTime _InvoiceDate;
       private Int64 _FK_Supplier;
       private string _UserCode;
       private DateTime _EnteredOn;
       private string _xmlPurchseValues;
       private string _xmlValues;
       private decimal _NetAmount;
       private long _Fk_Reason;
       private decimal _FTotalAmount;
       private decimal _FDiscPer;
       private decimal _FDiscAmnt;
       private decimal _FnetAmnt;
       private decimal _TtotalAmount;
       private decimal _TDiscPer;
       private decimal _TDiscAmnt;
       private decimal _TnetAmnt;
       private bool _Cancelled;
       private bool _Approved;
       private string _CancelledUser;
       private DateTime _CancelledOn;
       private Int64 _TransactionID;
       private DateTime _FromDate;
       private DateTime _ToDate;
       private Int16 _ApproveorPending;
       public Int16 ApproveorPending
       {
           get { return _ApproveorPending; }
           set { _ApproveorPending = value; }
       }
       public DateTime ToDate
       {
           get { return _ToDate; }
           set { _ToDate = value; }
       }
       public DateTime FromDate
       {
           get { return _FromDate; }
           set { _FromDate = value; }
       }
       public Int64 TransactionID
       {
           get { return _TransactionID; }
           set { _TransactionID = value; }
       }
       public byte UserAction
       {
           get { return _UserAction; }
           set { _UserAction = value; }
       }

       public Int64 FK_Client
       {
           get { return _FK_Client; }
           set { _FK_Client = value; }
       }
       public Int64 MasterId
       {
           get { return _MasterId; }
           set { _MasterId = value; }
       }
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
       public Int64 ReplaceID
       {
           get { return _ReplaceID; }
           set { _ReplaceID = value; }
       }
       public bool Cancelled
       {
           get { return _Cancelled; }
           set { _Cancelled = value; }
       }
       public bool Approved
       {
           get { return _Approved; }
           set { _Approved = value; }
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
       public DateTime RecivedDate
       {
           get { return _RecivedDate; }
           set { _RecivedDate = value; }
       }
       public Boolean Linkbtn
       {
           get { return _Linkbtn; }
           set { _Linkbtn = value; }
       }
         public Int64 Fk_PurchaseReturn
       {
           get { return _Fk_PurchaseReturn; }
           set { _Fk_PurchaseReturn = value; }
       }
         public Int64 Fk_BillType
         {
             get { return _Fk_BillType; }
             set { _Fk_BillType = value; }
         }
        public string BillNo
         {
             get { return _BillNo; }
             set { _BillNo = value; }
         }
        public Int64 FK_Store 
        {
            get { return _FK_Store; }
            set { _FK_Store = value; }
        }
        public string InvoiceNo 
        {
            get { return _InvoiceNo; }
            set { _InvoiceNo = value; }
        }
        public DateTime InvoiceDate 
        {
            get { return _InvoiceDate; }
            set { _InvoiceDate = value; }
        }
        public Int64 FK_Supplier 
        {
            get { return _FK_Supplier; }
            set { _FK_Supplier = value; }
        }
        public string UserCode
        {
            get { return _UserCode; }
            set { _UserCode = value; }
        }
        public DateTime EnteredOn
        {
            get { return _EnteredOn; }
            set { _EnteredOn = value; }
        }
        public string xmlPurchseValues
        {
            get { return _xmlPurchseValues; }
            set { _xmlPurchseValues = value; }
        }
        public string xmlValues
        {
            get { return _xmlValues; }
            set { _xmlValues = value; }
        }
        public decimal NetAmount
        {
            get{return _NetAmount;}
            set { _NetAmount = value; }
        }
        public Int64 Fk_Reason
        {
            get { return _Fk_Reason; }
            set { _Fk_Reason = value; }
        }
        public decimal FTotalAmount
        {
            get { return _FTotalAmount; }
            set { _FTotalAmount = value; }
        }
        public decimal FDiscPer
        {
            get { return _FDiscPer; }
            set { _FDiscPer = value; }
        }
        public decimal FDiscAmnt
        {
            get { return _FDiscAmnt; }
            set { _FDiscAmnt = value; }
        }
        public decimal FnetAmnt
        {
            get { return _FnetAmnt; }
            set { _FnetAmnt = value; }
        }
        public decimal TtotalAmount
        {
            get { return _TtotalAmount; }
            set { _TtotalAmount = value; }
        }
        public decimal TDiscPer 
        {
            get { return _TDiscPer; }
            set { _TDiscPer = value; }
        }
        public decimal TDiscAmnt
        {
            get { return _TDiscAmnt; }
            set { _TDiscAmnt = value; }
        }
        public decimal TnetAmnt
        {
            get { return _TnetAmnt; }
            set { _TnetAmnt = value; }
        }

        public long InsertData()
        {
            long status;

            DalReplace dalReplace = new DalReplace();
            status = 0;
            try
            {
                _UserAction = 1;
                status = dalReplace.UpdateReplace(this);
                dalReplace = null;
            }
            catch (Exception ex)
            {

            }
            return status;
        }
        public long ReplaceApproval()
        {

            DalReplace dalReplace = new DalReplace();
            //_UserAction = 1;
            long status = 0;
            try
            {
                status = dalReplace.ReplaceApprove(this);
            }
            catch (Exception ex)
            {

            }
            return status;
        }
        public long UpdateData()
        {
            _UserAction = 2;
            DalReplace dalReplace = new DalReplace();
            long status;
            status = 0;
            try
            {
                status = dalReplace.UpdateReplace(this);
                dalReplace = null;
            }
            catch (Exception ex)
            {

            }
            return status;
        }
        public long DeleteReplace()
        {
            long status = 0;
               DalReplace dalReplace = new DalReplace();
            try
            {

                UserAction = 3;
                status = dalReplace.DeleteReplace(this);
                dalReplace = null;
            }
            catch (Exception ex)
            {
            }
            return status;


        }
        public DataTable SelectAllData()
        {
            DataTable dtbl = new DataTable();
            DalReplace dalReplace = new DalReplace();
            dtbl = dalReplace.SelectReplace(this);
            dalReplace = null;
            return dtbl;
        }
        public DataTable SelectBillNo()
        {
            DataTable dtbl = new DataTable();
            DalReplace DalRep = new DalReplace();
            dtbl = DalRep.SelectBillNo(this);
            DalRep = null;
            return dtbl;
        }
        public void SelectData()
        {
            DalReplace dalReplace = new DalReplace();
            IReplace IReplace;
            IReplace = (IReplace)dalReplace.SelectMasterData(this);
            _BillNo = IReplace.BillNo;
            Fk_BillType = IReplace.Fk_BillType;
            _FK_Supplier = IReplace.FK_Supplier;
            _FK_Store = IReplace.FK_Store;
            _UserCode = IReplace.UserCode;
            _RecivedDate = IReplace.RecivedDate;
            _InvoiceNo = IReplace.InvoiceNo;
            _InvoiceDate = IReplace.InvoiceDate;
            _Linkbtn = IReplace.Linkbtn;
            _Approved = IReplace.Approved;
        
            dalReplace = null;

        }
       
        public DataSet  SelectAllGridData()
        {
            DataSet dtbl = new DataSet();
            DalReplace dalReplace = new DalReplace();
            dtbl = dalReplace.SelectReplaceGrid(this);
            dalReplace = null;
            return dtbl;
        }
    }

   public class IReplaceDetails
   {
       private Int64 _FK_Category;
       private Int64 _FK_Item;
       private Int64 _FK_Unit;
       private Int64 _FK_SubUnit;
       private Int64 _FK_Stock;
       private string _BatchNo;
       private Int64 _SlNo;
       private DateTime _ExpiryDate;
       private string _HSN;
       private Decimal _Quantity;
       private Decimal _Free;
       private Decimal _ActQuantity;
       private Decimal _PurchaseRate;
       private Boolean _IncludePtax;
       private Decimal _PurchaseTaxAmount;
       private Boolean _DiscountOntax;
       private Decimal _PurchaseDiscper;
       private Decimal _PurchaseDiscAmnt;
       private Decimal _NetPurchase;
       private Decimal _MRP;
       private Decimal _ProfitPer;
       private Decimal _ProfitAmnt;
       private Decimal _Discper;
       private Decimal _DiscAmnt;
       private Boolean _IncludeStax;
       private Decimal _SalesTaxAmnt;
       private Decimal _SalesPrice;
       private string _Type;
       private Int64 _FK_Reason;
       private decimal _ActQty;
       private decimal _ActualPurchaseRate;
       private decimal _TaxPer;

       public IReplaceDetails()
       {
           Initialize();
       }
       public void Initialize()
       {
           _FK_Category = 0;
           _FK_Item = 0;
           _FK_Unit = 0;
           _FK_SubUnit = 0;
           _BatchNo =string.Empty;
           _ExpiryDate = DateTime.Now;
           _HSN = string.Empty;
           _Quantity = 0;
           _Free = 0;
           _ActQuantity = 0;
           _PurchaseRate = 0;
           _IncludePtax = false;
           _PurchaseTaxAmount = 0;
           _DiscountOntax = false;
           _PurchaseDiscper = 0;
           _PurchaseDiscAmnt = 0;
           _NetPurchase = 0;
           _MRP = 0;
           _ProfitPer = 0;
           _ProfitAmnt = 0;
           _Discper = 0;
           _DiscAmnt = 0;
           _IncludeStax = false;
           _SalesTaxAmnt = 0;
           _SalesPrice = 0;
           _SlNo = 0;
           _Type = string.Empty;
           _FK_Reason = 0;
           _ActQty = 0;
           _FK_Stock = 0;
           _ActualPurchaseRate = 0;
           _TaxPer = 0;
       }
       public Int64 FK_Stock
       {
           get { return _FK_Stock; }
           set { _FK_Stock = value; }
       }
       public Int64 FK_Category
       {
           get { return _FK_Category; }
           set { _FK_Category = value; }
       }
       public Int64 FK_Item
       {
           get { return _FK_Item; }
           set { _FK_Item = value; }
       }
       public Int64 SlNo
       {
           get { return _SlNo; }
           set { _SlNo = value; }
       }
       public Int64 FK_Unit
       {
           get { return _FK_Unit; }
           set { _FK_Unit = value; }
       }
       public Int64 FK_SubUnit
       {
           get { return _FK_SubUnit; }
           set { _FK_SubUnit = value; }
       }
       public Int64 FK_Reason
       {
           get { return _FK_Reason; }
           set { _FK_Reason = value; }
       }
       public decimal ActQty
       {
           get { return _ActQty; }
           set { _ActQty = value; }
       }
      
       public DateTime ExpiryDate
       {
           get { return _ExpiryDate; }
           set { _ExpiryDate = value; }
       }
       public string HSN
       {
           get { return _HSN; }
           set { _HSN = value; }
       }

       public decimal Quantity
       {
           get { return _Quantity; }
           set { _Quantity = value; }
       }
       public decimal Free
       {
           get { return _Free; }
           set { _Free = value; }
       }

       public decimal ActQuantity
       {
           get { return _ActQuantity; }
           set { _ActQuantity = value; }
       }
       public decimal PurchaseRate
       {
           get { return _PurchaseRate ; }
           set { _PurchaseRate= value; }
       }
       public decimal TaxPer
       {
           get { return _TaxPer; }
           set { _TaxPer = value; }
       }
       public decimal ActualPurchaseRate
       {
           get { return _ActualPurchaseRate; }
           set { _ActualPurchaseRate = value; }
       }
       public bool IncludePtax
       {
           get { return _IncludePtax; }
           set { _IncludePtax= value; }
       }
       public decimal PurchaseTaxAmount
       {
           get { return _PurchaseTaxAmount; }
           set { _PurchaseTaxAmount = value; }
       }
       public bool DiscountOntax
       {
           get { return _DiscountOntax ; }
           set { _DiscountOntax = value; }
       }
       public decimal PurchaseDiscper
       {
           get { return _PurchaseDiscper; }
           set { _PurchaseDiscper = value; }
       }

       public decimal PurchaseDiscAmnt
       {
           get { return _PurchaseDiscAmnt; }
           set { _PurchaseDiscAmnt = value; }
       }

       public decimal NetPurchase
       {
           get { return _NetPurchase; }
           set { _NetPurchase = value; }
       }
       public decimal MRP
       {
           get { return _MRP; }
           set { _MRP = value; }
       }
       public decimal ProfitPer
       {
           get { return _ProfitPer; }
           set { _ProfitPer = value; }
       }
       public decimal ProfitAmnt
       {
           get { return _ProfitAmnt ; }
           set { _ProfitAmnt = value; }
       }
       public decimal Discper
       {
           get { return _Discper; }
           set { _Discper = value; }
       }
       public decimal DiscAmnt
       {
           get { return _DiscAmnt; }
           set { _DiscAmnt = value; }
       }
       public bool IncludeStax
       {
           get { return _IncludeStax; }
           set { _IncludeStax = value; }
       }

       public decimal SalesTaxAmnt
       {
           get { return _SalesTaxAmnt; }
           set { _SalesTaxAmnt = value; }
       }
       public decimal SalesPrice
       {
           get { return _SalesPrice; }
           set { _SalesPrice = value; }
       }
       public string BatchNo
       {
           get { return _BatchNo; }
           set { _BatchNo = value; }
       }
         public string Type
       {
           get { return _Type; }
           set { _Type = value; }
       }
      
       
       
   }
}
