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
  public   class BlPurchase:IPurchase
  {
      public BlPurchase()

      {
          Initialize();
      }

      public void Initialize()
      {
          _FK_Reason = 0;
          _FK_PurchaseOrder = 0;
          _UserAction = 1;
          _FK_Client = 1;
          _MasterId = 0;
          _PageIndex = 1;
          _PageSize = 10;
          _PurBillNo = "";
          _RefNo = "";
          _Date = DateTime.Now;
          _FK_Supplier = 0;
          _Approved = 0;
          _CashCredit = 0;
          _Cost = 0;
          _Expense = 0;
          _InvoiceAmount = 0;
          _Description = "";
          _Credit = false;
          _FK_Store = 0;
          _UserCode = "";
          _EnteredOn = DateTime.Now;
          _Cancelled = false;
          _Cancelleduser = "";
          _BlPurchaseDetails = "";
          _CancelledOn = DateTime.Now;
          _ItemPurchase = false;
          _XmlPurchaseItems = "";
          _InvoiceNo = "";
          _BillDate = DateTime.Now;
          _FromDate = DateTime.Now;
          _ToDate = DateTime.Now;
          _ReferenceNo = 0;
          _SupplierName = string.Empty;

          _ReportPath = string.Empty;
          _ReportSchema = string.Empty;
          _ReportSchemaPath = string.Empty;
          _ReportTable = string.Empty;
          _ReportFile = string.Empty;
          _DatasetName = string.Empty;
          _itemID = 0;
          _FK_Reason = 0;
          _Purchase = false;
          
      }
      #region Properties

      private string _XmlPurchaseItems;
      private Boolean _ItemPurchase; 
      private long _FK_PurchaseOrder;
      private DateTime _FromDate;
      private long _FK_MaterialReciept;
      private long _itemID;
     

      private string _InvoiceNo;
      private DateTime _BillDate;
      private decimal _DiscountPer;
      private decimal _DiscountAmt;
      private Int16 _Approved;

      private Int16 _CashCredit;

      private string _DatasetName;
      private string _ReportSchema;
      private string _ReportTable;
      private string _ReportSchemaPath;
      private string _ReportPath;
      private string _ReportFile;
      private string _SupplierName;
      private long _FK_Reason;
      private Boolean _Purchase;

      public Boolean Purchase
      {
          get { return _Purchase; }
          set { _Purchase = value; }
      }
      public long itemID
      {
          get { return _itemID; }
          set { _itemID = value; }
      }
      public long FK_Reason
      {
          get { return _FK_Reason; }
          set { _FK_Reason = value; }
      }
      public string SupplierName
      {
          get { return _SupplierName; }
          set { _SupplierName = value; }
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
      public Int16 Approved
      {
          get { return _Approved; }
          set { _Approved = value; }
      }

      public Int16 CashCredit
      {
          get { return _CashCredit; }
          set { _CashCredit = value; }
      }

      public decimal DiscountPer
      {
          get { return _DiscountPer; }
          set { _DiscountPer = value; }
      }
      public decimal DiscountAmt
      {
          get { return _DiscountAmt; }
          set { _DiscountAmt = value; }
      }
      public string InvoiceNo
      {
          get { return _InvoiceNo; }
          set { _InvoiceNo = value; }
      }
      public DateTime BillDate
      {
          get { return _BillDate; }
          set { _BillDate = value; }
      }


      public string XmlPurchaseItems
      {
          get { return _XmlPurchaseItems; }
          set { _XmlPurchaseItems = value; }
      }

      public Boolean ItemPurchase
      {
          get { return _ItemPurchase; }
          set { _ItemPurchase = value; }
      } 
      public long FK_MaterialReciept
      {
          get { return _FK_MaterialReciept; }
          set { _FK_MaterialReciept = value; }
      }
      private string _BlPurchaseDetails;

      public string BlPurchaseDetails
      {
          get { return _BlPurchaseDetails; }
          set { _BlPurchaseDetails = value; }
      }

      public DateTime FromDate
      {
          get { return _FromDate; }
          set { _FromDate = value; }
      }
      private DateTime _ToDate;

      public DateTime ToDate
      {
          get { return _ToDate; }
          set { _ToDate = value; }
      }

        private byte _UserAction;

        public byte UserAction
        {
            get { return _UserAction; }
            set { _UserAction = value; }
        }




        private Int64 _FK_Client;

        public Int64 FK_Client
        {
            get { return _FK_Client; }
            set { _FK_Client = value; }
        }
    private Int64 _MasterId;

    public Int64 MasterId
    {
        get { return _MasterId; }
        set { _MasterId = value; }
    }
    private Int16 _PageIndex;

    public Int16 PageIndex
    {
        get { return _PageIndex; }
        set { _PageIndex = value; }
    }
    private Int16 _PageSize;

    public Int16 PageSize
    {
        get { return _PageSize; }
        set { _PageSize = value; }
    }
        public long FK_PurchaseOrder
        {
            get { return _FK_PurchaseOrder; }
            set { _FK_PurchaseOrder = value; }
        }

        private string _PurBillNo;

        public string PurBillNo
        {
            get { return _PurBillNo; }
            set { _PurBillNo = value; }
        }


        private Int64 _ReferenceNo;

        public Int64 ReferenceNo
        {
            get { return _ReferenceNo; }
            set { _ReferenceNo = value; }
        }

        private string _RefNo;

        public string RefNo
        {
            get { return _RefNo; }
            set { _RefNo = value; }
        }
        private DateTime _Date;

        public DateTime Date
        {
            get { return _Date; }
            set { _Date = value; }
        }


        private long _FK_Supplier;

        public long FK_Supplier
        {
            get { return _FK_Supplier; }
            set { _FK_Supplier = value; }
        }

        private decimal _Cost;

        public decimal Cost
        {
            get { return _Cost; }
            set { _Cost = value; }
        }

        private decimal _Expense;

        public decimal Expense
        {
            get { return _Expense; }
            set { _Expense = value; }
        }
        private decimal _InvoiceAmount;

        public decimal InvoiceAmount
        {
            get { return _InvoiceAmount; }
            set { _InvoiceAmount = value; }
        }


        private string _Description;

        public string Description
        {
            get { return _Description; }
            set { _Description = value; }
        }

        private bool _Credit;

        public bool Credit
        {
            get { return _Credit; }
            set { _Credit = value; }
        }

        private long _FK_Store;

        public long FK_Store
        {
            get { return _FK_Store; }
            set { _FK_Store = value; }
        }

        private string _UserCode;

        public string UserCode
        {
            get { return _UserCode; }
            set { _UserCode = value; }
        }

     

        private DateTime _EnteredOn;

        public DateTime EnteredOn
        {
            get { return _EnteredOn; }
            set { _EnteredOn = value; }
        }

        private bool _Cancelled;

        public bool Cancelled
        {
            get { return _Cancelled; }
            set { _Cancelled = value; }
        }

        private string _Cancelleduser;

        public string Cancelleduser
        {
            get { return _Cancelleduser; }
            set { _Cancelleduser = value; }
        }
        private DateTime _CancelledOn;

        public DateTime CancelledOn
        {
            get { return _CancelledOn; }
            set { _CancelledOn = value; }
        }

      #endregion
      #region Process
        public long InsertData()
        {
            DalPurchase purchase = new DalPurchase();
            _UserAction = 1;
            long status = 0;
            try
            {
                status = purchase.UpdatePurchase(this);
            }
            catch (Exception ex)
            {

            }
            return status;
        }
        public long UpdateData()
        {
            DalPurchase purchase = new DalPurchase();
            _UserAction = 2;
            long status = 0;
            try
            {
                status = purchase.UpdatePurchase(this);
            }
            catch (Exception ex)
            {
            }
            return status;
        }
        public long DeletePurchase()
        {

            DalPurchase dalPurchase = new DalPurchase();
            long status = 0;
            try
            {

                status = dalPurchase.DeletePurchase(this);

                dalPurchase = null;
            }
            catch (Exception ex)
            {
            }
            return status;
        }
        public long DeletePurchaseDetails()
        {

            DalPurchase dalPurchase = new DalPurchase();
            long status = 0;
            try
            {

                status = dalPurchase.DeletePurchaseDetails(this);

                dalPurchase = null;
            }
            catch (Exception ex)
            {
            }
            return status;
        }

        public void SelectPurchase()
        {
            DalPurchase purchase = new DalPurchase();
            IPurchase ipurchase;
            ipurchase = (IPurchase)purchase.proPurchaseSelect(this);
            _FK_PurchaseOrder = ipurchase.FK_PurchaseOrder;
            _PurBillNo = ipurchase.PurBillNo;
            _RefNo = ipurchase.RefNo;
            _Date = ipurchase.Date;
            _FK_Supplier = ipurchase.FK_Supplier;
            _FK_Store = ipurchase.FK_Store;
            _Cost = ipurchase.Cost;
            _Expense = ipurchase.Expense;
            _InvoiceAmount = ipurchase.InvoiceAmount;
            _Description = ipurchase.Description;
            _Credit = ipurchase.Credit;
            _FK_Store = ipurchase.FK_Store;
            _Credit = ipurchase.Credit;
            _ItemPurchase = ipurchase.ItemPurchase;
            _DiscountPer = ipurchase.DiscountPer;
            _DiscountAmt = ipurchase.DiscountAmt;
            _InvoiceNo = ipurchase.InvoiceNo;
            _SupplierName = ipurchase.SupplierName;
            purchase = null;

        }
        public DataTable SelectAllData()
        {
            DalPurchase purchase = new DalPurchase();
            DataTable dtbl = purchase.proPurchaseSelectAll(this);
            purchase = null;
            return dtbl;
           
        }
        public DataTable SelectAllDataForPurchase()
        {
            DalPurchase purchase = new DalPurchase();
            DataTable dtbl = purchase.proSelectAllDateForPurchase(this);
            purchase = null;
            return dtbl;

        }
        public DataTable SelectInterStatePurchase()
        {
            DalPurchase purchase = new DalPurchase();
            DataTable dtbl = purchase.proSelectInterState(this);
            purchase = null;
            return dtbl;

        }
        public DataTable SelectAllDataForPurchaseApproveEditView()
        {
            DalPurchase purchase = new DalPurchase();
            DataTable dtbl = purchase.proSelectAllPurchaseApproveEditView(this);
            purchase = null;
            return dtbl;

        }
        public DataTable SelectGriddata()
        {
            DalPurchase purchase = new DalPurchase();
            DataTable dtbl = purchase.GridData(this);
            purchase = null;
            return dtbl;
        }
        public DataTable SelectGriddataPurchase()
        {
            DalPurchase purchase = new DalPurchase();
            DataTable dtbl = purchase.SelectGriddataPurchase(this);
            purchase = null;
            return dtbl;

        }
        public DataTable SelectGriddataSales()
        {
            DalPurchase purchase = new DalPurchase();
            DataTable dtbl = purchase.GridData(this);
            purchase = null;
            return dtbl;

        }
        public DataTable SelectExpenseInPurchase()
        {
            DalPurchase purchase = new DalPurchase();
            DataTable dtbl = purchase.ExpenseData(this);
            purchase = null;
            return dtbl;

        }
        public DataTable SelectPurchaseTax()
        {
            DalPurchase purchase = new DalPurchase();
            DataTable dtbl = purchase.PurchseTax(this);
            purchase = null;
            return dtbl;

        }
        public DataTable SelectGroupID()
        {
            DalPurchase purchase = new DalPurchase();
            DataTable dtbl = purchase.SelectGroupID(this);
            purchase = null;
            return dtbl;

        }
        public DataTable SelectPurchaseReturnTax()
        {
            DalPurchase purchase = new DalPurchase();
            DataTable dtbl = purchase.SelectPurchaseReturnSelect(this);
            purchase = null;
            return dtbl;

        }
      
        public DataTable SelectGridPurchaseTaxDetails()
        {
            DalPurchase purchase = new DalPurchase();
            DataTable dtbl = purchase.PurcahseGrid(this);
            purchase = null;
            return dtbl;

        }
        public DataTable SelectGridSalesTaxDetails()
        {
            DalPurchase purchase = new DalPurchase();
            DataTable dtbl = purchase.SalesGrid(this);
            purchase = null;
            return dtbl;

        }
        public DataTable SelectPurchaseStockAndStore()
        {
            DalPurchase dlPurchase = new DalPurchase();
            DataTable dtbl = dlPurchase.PurchaseStoreAndSupplier(this);
            dlPurchase = null;
            return dtbl;
        }
        public DataSet GetPurchaseDataSource(long StatusId)
        {
            DalPurchase DalPurchase = new DalPurchase();
            DataSet ds = new DataSet();
            try
            {
                ds = DalPurchase.GetPurchaseReportData(StatusId);
                ds.DataSetName = _DatasetName;
                ds.Tables[0].TableName = ReportTable;
                ds.WriteXmlSchema(_ReportSchemaPath + _ReportSchema + ".xsd");
            }
            catch (Exception ex)
            {
            }
            finally
            {
                DalPurchase = null;
            }
            return ds;
        }
        public DataTable proMaterialItemsUnderMaterials()
        {
            DalPurchase purchase = new DalPurchase();
            DataTable dtbl = purchase.proMaterialItemsUnderMaterials(this);
            purchase = null;
            return dtbl;
        }
        public DataTable proPurchaseMaterials()
        {
            DalPurchase purchase = new DalPurchase();
            DataTable dtbl = purchase.proPurchaseMaterials(this);
            purchase = null;
            return dtbl;
        }
        public DataTable proPurchaseMaterialsPurchaseItems()
        {
            DalPurchase purchase = new DalPurchase();
            DataTable dtbl = purchase.proPurchaseMaterialsPurchaseItems(this);
            purchase = null;
            return dtbl;
        }
        public DataTable proPurchaseMaterialsPurchaseItemsForApprove()
        {
            DalPurchase purchase = new DalPurchase();
            DataTable dtbl = purchase.proPurchaseMaterialsPurchaseItemsForApprove(this);
            purchase = null;
            return dtbl;
        }
        public DataTable PurchaseExport()
        {
            DalPurchase purchase = new DalPurchase();
            DataTable dtbl = purchase.PurchaseExport(this);
            purchase = null;
            return dtbl;
        }
        public string PurchaseImport()
        {
            DalPurchase purchase = new DalPurchase();
            string str = purchase.PurchaseImport(this);
            purchase = null;
            return str;
        }
       
      #endregion
  }

  public class BlPurchaseDetails
  {
      private long _FK_MaterialReciept;

      public long FK_MaterialReciept
      {
          get { return _FK_MaterialReciept; }
          set { _FK_MaterialReciept = value; }
      }
  }

  public class IPurchaseItemDetails
  {
      private Int64 _FK_Items;
      private string _BatchNo;
      private DateTime _ExpiryDate;
      private decimal _Quantity;
      private decimal _PurchaseRate;
      private decimal _ActualCost;
      private decimal _SalesPrice;
      private decimal _OfferPrice;
      private decimal _WholeSalePrice;
      private Int64 _FK_Units;



      public IPurchaseItemDetails()
      {
          Initialize();
      }
      public void Initialize()
      {

          _FK_Items = 0;
          _BatchNo = string.Empty;
          _ExpiryDate = DateTime.Now;
          _Quantity = 0;
          _PurchaseRate = 0;
          _ActualCost = 0;
          _SalesPrice = 0;
          _OfferPrice = 0;
          _WholeSalePrice = 0;
          _FK_MaterialReciept = 0;
          _FK_Units = 0;

      }
      private Int64 _FK_MaterialReciept;

      public Int64 FK_MaterialReciept
      {
          get { return _FK_MaterialReciept; }
          set { _FK_MaterialReciept = value; }
      }
      public Int64 FK_Items
      {
          get { return _FK_Items; }
          set { _FK_Items = value; }
      }
      public Int64 FK_Unit
      {
          get { return _FK_Units; }
          set { _FK_Units = value; }
      }
      public decimal Quantity
      {
          get { return _Quantity; }
          set { _Quantity = value; }
      }
      public DateTime ExpiryDate
      {
          get { return _ExpiryDate; }
          set { _ExpiryDate = value; }
      }
      public decimal WholeSalePrice
      {
          get { return _WholeSalePrice; }
          set { _WholeSalePrice = value; }
      }
      public decimal OfferPrice
      {
          get { return _OfferPrice; }
          set { _OfferPrice = value; }
      }
      public decimal ActualCost
      {
          get { return _ActualCost; }
          set { _ActualCost = value; }
      }
      public decimal PurchaseRate
      {
          get { return _PurchaseRate; }
          set { _PurchaseRate = value; }
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


  }
  public class IpurchaseExport
  {

    


      public string RefNo
      {
          get { return _RefNo; }
          set { _RefNo = value; }
      }
      public DateTime Date
      {
          get { return _Date; }
          set { _Date = value; }
      }
      public bool ItemPurchase
      {
          get { return _ItemPurchase; }
          set { _ItemPurchase = value; }
      }
      public string Supplier
      {
          get { return _Supplier; }
          set { _Supplier = value; }
      }

      public string Store
      {
          get { return _Store; }
          set { _Store = value; }
      }
      public string PONo
      {
          get { return _PONo; }
          set { _PONo = value; }
      }
      public long MrNo
      {
          get { return _MrNo; }
          set { _MrNo = value; }
      }
      public bool Credit
      {
          get { return _Credit; }
          set { _Credit = value; }
      }
      public string Item
      {
          get { return _Item; }
          set { _Item = value; }
      }
      public string Unit
      {
          get { return _Unit; }
          set { _Unit = value; }
      }
      public string Batch
      {
          get { return _Batch; }
          set { _Batch = value; }
      }
      public DateTime ExpiryDate
      {
          get { return _ExpiryDate; }
          set { _ExpiryDate = value; }
      }
      public decimal PurchaseRate
      {
          get { return _PurchaseRate; }
          set { _PurchaseRate = value; }
      }
      public decimal Quantity
      {
          get { return _Quantity; }
          set { _Quantity = value; }
      }
      public decimal SalesPrice
      {
          get { return _SalesPrice; }
          set { _SalesPrice = value; }
      }
      public decimal WholeSalePrice
      {
          get { return _WholeSalePrice; }
          set { _WholeSalePrice = value; }
      }
      public decimal InvoiceAmount
      {
          get { return _InvoiceAmount; }
          set { _InvoiceAmount = value; }
      }
      public decimal Cost
      {
          get { return _Cost; }
          set { _Cost = value; }
      }
      public decimal Expense
      {
          get { return _Expense; }
          set { _Expense = value; }
      }
      public string PurBillNo
      {
          get { return _PurBillNo; }
          set { _PurBillNo = value; }
      }


      private string _PurBillNo;
      private string _RefNo;
      private DateTime _Date;
      private bool _ItemPurchase;
      private string _Supplier;
      private string _Store;
      private string _PONo;
      private long _MrNo;    
      private bool _Credit;
      private string _Item;   
      private string _Unit;  
      private string _Batch;
      private DateTime _ExpiryDate;
      private decimal _PurchaseRate;
      private decimal _Quantity;
      private decimal _SalesPrice;
      private decimal _WholeSalePrice;
      private decimal _InvoiceAmount;
      private decimal _Cost;
      private decimal _Expense;
      public IpurchaseExport()
      {
          Initialize();
      }
      public void Initialize()
      {

          _PurBillNo = string.Empty;
          _RefNo = string.Empty;
          _Date = DateTime.Now;
          _ItemPurchase = true;
          _Supplier = string.Empty;
          _Store = string.Empty;
          _PONo = string.Empty;
          _MrNo = 0;
          _Credit = false;
          _Item = string.Empty;
          _Unit = string.Empty;
          _Batch = string.Empty;
          _ExpiryDate = DateTime.Now;
          _PurchaseRate = 0;
          _Quantity = 0;
          _SalesPrice = 0;
          _WholeSalePrice = 0;
          _InvoiceAmount = 0;
          _Cost = 0;
          _Expense = 0;



      }
   
  }
}
