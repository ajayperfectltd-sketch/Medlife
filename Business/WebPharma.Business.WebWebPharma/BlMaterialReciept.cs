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
	public class BlMaterialReciept : IMaterialReciept
	{
 
		private byte _UserAction;
		private Int16 _PageIndex;
		private Int16 _PageSize; 
		private string _TransMode;
        private long _FK_ItemsforPreviousPurchase;
        private long _FK_Supplier;
        private long _FK_Store;
        private long _FK_Items;
        private long _FK_Category;
        private long _FK_PurchaseOrder;
        private DateTime _RecDate;
        private string _InvoiceNo;
        private long _RefNo;
        private DateTime _InvoiceDate;
        private Decimal _TotalAmount ;
        private Decimal _RoundOff ;
        private Decimal _PurAmt;
        private Decimal _Fright ;
        private Decimal _NetAmount;
		private long _ID_MaterialReciept;
		private string _UserCode;
		private DateTime _EnteredOn;
		private Boolean _Cancelled;
		private string _Cancelleduser;
		private DateTime _CancelledOn;
        private string _XmlMaterialRecieptItems;
        private string _XmlSalesTax;
        private string _XmlPurchaseTax;
		private long _FK_Client;
        private string _GSTIN;
        public bool _InterStatePurchase;
        private long _FK_Reason;
        private string _xmlGriddata;
        private string _xmlExpenseData;
        private string _XmlExcelImport;
        private long _FK_SubCategory;
        private long _FK_Units;
        private long _FK_SubUnits;
        private long _FK_Manufacture;
        private string _ItemName;
        private string _BatchNo;
        private long _FK_Generic;
        private DateTime _FromDate;
        private DateTime _ToDate;
        private Int16 _Period;
        private long _TransactionID;
        private Int16 _State;
		public BlMaterialReciept()
		{
			Initialize();
		}
		public void Initialize()
		{
            _State = 0;
            _ID_MaterialReciept = 0;
            _FK_Supplier = 0;
            _FK_Store=0;
            _FK_Category=0;
            _FK_PurchaseOrder=0;
            _FK_ItemsforPreviousPurchase = 0;
            _RecDate = DateTime.Now;
            _FK_Reason = 0;
            _InvoiceNo =string.Empty;
            _InvoiceDate = DateTime.Now;
            _TotalAmount=0;
            _RoundOff=0;
            _PurAmt = 0;
            _Fright=0;
            _NetAmount=0;
            _FK_Items = 0;
	        _UserCode = string.Empty;
	        _EnteredOn = DateTime.Now;
	        _Cancelled = false;
	        _Cancelleduser = string.Empty;
	        _CancelledOn = DateTime.Now;
	        _FK_Client = 1;
       
            _UserAction = 1;
            _PageIndex = 1;
            _PageSize = 10;
	        _TransMode = string.Empty;
            _GSTIN = string.Empty;
            _InterStatePurchase = false;
            _XmlSalesTax = string.Empty;
            _XmlPurchaseTax = string.Empty;
            _xmlGriddata = string.Empty;
            _xmlExpenseData = string.Empty;
            _XmlExcelImport = string.Empty;
            _FK_SubCategory = 0;
            _FK_Units = 0;
            _FK_SubUnits = 0;
            _FK_Manufacture = 0;
            _ItemName = string.Empty;
            _BatchNo = string.Empty;
            _FK_Generic = 0;
            _FromDate = DateTime.Now;
            _ToDate = DateTime.Now;
            _Period = 0;
            _TransactionID = 0;
            _RefNo =0;
            _XmlMaterialRecieptItems = string.Empty;
		}
        public Int16 State
        {
            get { return _State; }
            set { _State = value; }
        }
        public long RefNo
        {
            get { return _RefNo; }
            set { _RefNo = value; }
        }
        public long TransactionID
        {
            get { return _TransactionID; }
            set { _TransactionID = value; }
        }
        public Int16 Period
        {
            get { return _Period; }
            set { _Period = value; }
        }
        public string XmlExcelImport
        {
            get { return _XmlExcelImport; }
            set { _XmlExcelImport = value; }
        }
        public string xmlExpenseData
        {
            get { return _xmlExpenseData; }
            set { _xmlExpenseData = value; }
        }
        public string GSTIN
        {
            get { return _GSTIN; }
            set { _GSTIN = value; }
        }
        public string xmlGriddata
        {
            get { return _xmlGriddata; }
            set { _xmlGriddata = value; }
        }
        public long FK_Items
        {
            get { return _FK_Items; }
            set { _FK_Items = value; }
        }
		public byte UserAction
		{
			get { return _UserAction; }
			set { _UserAction = value; }
		}
        public bool InterStatePurchase
        {
            get { return _InterStatePurchase; }
            set { _InterStatePurchase = value; }
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
        public string XmlMaterialRecieptItems
        {
            get { return _XmlMaterialRecieptItems; }
            set { _XmlMaterialRecieptItems = value; }
        }
      
        public string XmlPurchaseTax
        {
            get { return _XmlPurchaseTax; }
            set { _XmlPurchaseTax = value; }
        }
        public string XmlSalesTax
        {
            get { return _XmlSalesTax; }
            set { _XmlSalesTax = value; }
        }
		public long MasterId
		{
			get { return _ID_MaterialReciept; }
			set { _ID_MaterialReciept = value; }
		}

        public long FK_Supplier
		{
			get { return _FK_Supplier; }
			set { _FK_Supplier = value; }
		}
        public long FK_Reason
        {
            get { return _FK_Reason; }
            set { _FK_Reason = value; }
        }
        
        public long FK_PurchaseOrder
        {
            get { return _FK_PurchaseOrder; }
            set { _FK_PurchaseOrder = value; }
        }
        public long FK_Store
		{
			get { return _FK_Store; }
			set { _FK_Store = value; }
		}
        public long FK_Category
		{
			get { return _FK_Category; }
			set { _FK_Category = value; }
		}

        public long FK_ItemsforPreviousPurchase
		{
            get { return _FK_ItemsforPreviousPurchase; }
            set { _FK_ItemsforPreviousPurchase = value; }
		}
		public DateTime RecDate
		{
			get { return _RecDate; }
            set { _RecDate = value; }
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
        public decimal TotalAmount
        {
            get { return _TotalAmount; }
            set { _TotalAmount = value; }
        }
        public decimal RoundOff
        {
            get { return _RoundOff; }
            set { _RoundOff = value; }
        }
        public decimal PurAmt
        {
            get { return _PurAmt; }
            set { _PurAmt = value; }
        }
        public decimal Fright
        {
            get { return _Fright; }
            set { _Fright = value; }
        }
        public decimal NetAmount
        {
            get { return _NetAmount; }
            set { _NetAmount = value; }
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
		public Boolean Cancelled
		{
			get { return _Cancelled; }
			set { _Cancelled = value; }
		}
		public string Cancelleduser
		{
			get { return _Cancelleduser; }
			set { _Cancelleduser = value; }
		}
		public DateTime CancelledOn
		{
			get { return _CancelledOn; }
			set { _CancelledOn = value; }
		}
		public long FK_Client
		{
			get { return _FK_Client; }
			set { _FK_Client = value; }
		}
        public long FK_SubCategory
        {
            get { return _FK_SubCategory; }
            set { _FK_SubCategory = value; }
        }
        public long FK_Units
        {
            get { return _FK_Units; }
            set { _FK_Units = value; }
        }
        public long FK_SubUnits
        {
            get { return _FK_SubUnits; }
            set { _FK_SubUnits = value; }
        }
        public long FK_Manufacture
        {
            get { return _FK_Manufacture; }
            set { _FK_Manufacture = value; }
        }
        public string ItemName
        {
            get { return _ItemName; }
            set { _ItemName = value; }
        }
        public string BatchNo
        {
            get { return _BatchNo; }
            set { _BatchNo = value; }
        }
      
        public long FK_Generic
        {
            get { return _FK_Generic; }
            set { _FK_Generic = value; }
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

		public long InsertData()
		{
			long status;
			DalMaterialReciept dalmaterialreciept = new DalMaterialReciept();
			status = 0;
			try
			{
				_UserAction = 1;
				status = dalmaterialreciept.UpdateMaterialReciept(this);
				dalmaterialreciept=null;
			}
			catch (Exception ex)
			{
 
			}
			return status;
		}
 
		public long UpdateData()
		{
			_UserAction = 2;
			DalMaterialReciept dalmaterialreciept = new DalMaterialReciept();
			long status;
			status = 0;
			try
			{
				status =  dalmaterialreciept.UpdateMaterialReciept(this);
				dalmaterialreciept=null;
			}
			catch (Exception ex)
			{
 
			}
			return status;
		}
        public long DeleteData()
        {
            _UserAction = 3;
            DalMaterialReciept dalmaterialreciept = new DalMaterialReciept();
            long status = 0;
            try
            {

                status = dalmaterialreciept.DeleteMaterialReciept(this);
                dalmaterialreciept = null;
            }
            catch (Exception ex)
            {
            }
            return status;
        }
        public DataTable PuchaseQuatationSelect()
        {
            DalMaterialReciept purchase = new DalMaterialReciept();
            DataTable dtbl = purchase.PuchaseQuatationSelect(this);
            purchase = null;
            return dtbl;

        }
        public DataTable PreviousData()
        {
            DalMaterialReciept purchase = new DalMaterialReciept();
            DataTable dtbl = purchase.previousData(this);
            purchase = null;
            return dtbl;

        }
 
		public void SelectData()
		{
			DalMaterialReciept dalmaterialreciept = new DalMaterialReciept();
			IMaterialReciept imaterialreciept;
			imaterialreciept= (IMaterialReciept)dalmaterialreciept.SelectMaterialReciept(this);
			_FK_PurchaseOrder = imaterialreciept.FK_PurchaseOrder;
		
			_FK_Supplier = imaterialreciept.FK_Supplier;
			_FK_Store = imaterialreciept.FK_Store;
			_Cancelled = imaterialreciept.Cancelled;
			_Cancelleduser = imaterialreciept.Cancelleduser;
			_CancelledOn = imaterialreciept.CancelledOn;
        
		}
        //public long selectGridData()
        //{
        //    _UserAction = 3;
        //    DalMaterialReciept dalmaterialreciept = new DalMaterialReciept();
        //    long status = 0;
        //    try
        //    {

        //        status = dalmaterialreciept.selectGridData(this);
        //        dalmaterialreciept = null;
        //    }
        //    catch (Exception ex)
        //    {
        //    }
        //    return status;

        //}
        public DataTable selectGridData()
        {
            DataTable dtbl = new DataTable();
            DalMaterialReciept dalMaterialReciept = new DalMaterialReciept();
            dtbl = dalMaterialReciept.selectGridData(this);
            dalMaterialReciept = null;
            return dtbl;
        }
        public DataTable selectPurchaseOrderExist()
        {
            DataTable dtbl = new DataTable();
            DalMaterialReciept dalMaterialReciept = new DalMaterialReciept();
            dtbl = dalMaterialReciept.selectPurchaseOrderExist(this);
            dalMaterialReciept = null;
            return dtbl;
        }
        public void SelectpurchaseOrderStore()
        {
            DalMaterialReciept dalmaterialreciept = new DalMaterialReciept();
            IMaterialReciept imaterialreciept;
            imaterialreciept = (IMaterialReciept)dalmaterialreciept.SelectpurchaseOrderStore(this);
            _FK_Store = imaterialreciept.FK_Store;
            _FK_Supplier = imaterialreciept.FK_Supplier;
        
        }
        public void SelectMaterialStoreSupplier()
        {
            DalMaterialReciept dalmaterialreciept = new DalMaterialReciept();
            IMaterialReciept imaterialreciept;
            imaterialreciept = (IMaterialReciept)dalmaterialreciept.SelectMaterialStoreSupplier(this);
            _FK_Store = imaterialreciept.FK_Store;
            _FK_Supplier = imaterialreciept.FK_Supplier;
            dalmaterialreciept = null;
        
        }
 
		public DataTable SelectAllData()
		{
			DataTable dtbl = new DataTable();
			DalMaterialReciept dalMaterialReciept = new DalMaterialReciept();
			dtbl = dalMaterialReciept.SelectAllMaterialReciept(this);
			dalMaterialReciept = null;
			return dtbl;
		}
        public DataTable SelectPreviousPurchaseDetails()
        {
            DataTable dtbl = new DataTable();
            DalMaterialReciept dalMaterialReciept = new DalMaterialReciept();
            dtbl = dalMaterialReciept.SelectPreviousPurchaseDetails(this);
            dalMaterialReciept = null;
            return dtbl;
        }
        public DataTable SelectPreviousPurchaseDetailsGraph()
        {
            DataTable dtbl = new DataTable();
            DalMaterialReciept dalMaterialReciept = new DalMaterialReciept();
            dtbl = dalMaterialReciept.SelectPreviousPurchaseDetailsGraph(this);
            dalMaterialReciept = null;
            return dtbl;
        }
        public DataTable SelectMaterialRecieptDetails()
        {
            DataTable dtbl = new DataTable();
            DalMaterialReciept dalMaterialReciept = new DalMaterialReciept();
            dtbl = dalMaterialReciept.SelectMaterialRecieptDetails(this);
            dalMaterialReciept = null;
            return dtbl;
        }
        public string SelectPurchaseRefNo()
        {
             
            DalMaterialReciept dalMaterialReciept = new DalMaterialReciept();
           string refno= dalMaterialReciept.SelectPurchaseRefNo(this);
            dalMaterialReciept = null;
            return refno;
        }
        
        public DataTable SelectMaterialRecieptFright()
        {
            DataTable dtbl = new DataTable();
            DalMaterialReciept dalMaterialReciept = new DalMaterialReciept();
            dtbl = dalMaterialReciept.SelectMaterialRecieptFright(this);
            dalMaterialReciept = null;
            return dtbl;
        }
        public DataTable SelectMaterialRecieptItems()
        {
            DataTable dtbl = new DataTable();
            DalMaterialReciept dalMaterialReciept = new DalMaterialReciept();
            dtbl = dalMaterialReciept.SelectMaterialRecieptItems(this);
            dalMaterialReciept = null;
            return dtbl;
        }
        public DataTable GetPartialMaterialRecieptItems()
        {
            DataTable dtbl = new DataTable();
            DalMaterialReciept DalMaterialReciept = new DalMaterialReciept();
            dtbl = DalMaterialReciept.GetPartialMaterialRecieptItems(this);
            DalMaterialReciept = null;
            return dtbl;
        }
        public DataTable ValidateExcelData()
        {
            DataTable dtbl = new DataTable();
            DalMaterialReciept DalMaterialReciept = new DalMaterialReciept();

            dtbl = DalMaterialReciept.ValidateExcelData(this);
            DalMaterialReciept = null;
            return dtbl;
        }

            public DataTable SelectPrevPurchaseDetails()
            {
            DataTable dtbl = new DataTable();
            DalMaterialReciept dalMaterialReciept = new DalMaterialReciept();
            dtbl = dalMaterialReciept.SelectPrevPurchaseDetails(this);
            dalMaterialReciept = null;
            return dtbl;
        }
        
	}
  


       

    public class IMaterialRecieptDetails
    {
        private Int64 _FK_MaterialReciept;
        private Int64 _SlNo;
        private Int64 _FK_Items;
        private Int64 _Fk_Unit;
        private Int64 _FK_SubUnit;
        private Int64 _FK_Category;
        private string _BatchNo;
      
        private DateTime _ExpiryDate;
        private string _HSN;
        private decimal _Quantity;
        private decimal _Free;
        private decimal _ActQty;
        private decimal _PurRate;
        private bool _IncludePTax;
        private decimal _PurchaseTaxAmount;
        private decimal _TaxPer;
        private bool _DiscountOnTax;
        private decimal _PurchaseDiscPercentage;
        private decimal _PurchaseDiscAmount;
        private decimal _NetPurchase;
        private decimal _MRP;
        private decimal _ProfitPer;
        private decimal _ProfitAmount;
        private decimal _DiscountPer;
        private decimal _DiscountAmt;
        private bool _IncludeSTax;
        private decimal _SalesTaxAmount;
        private decimal _SalesPrice;
        private decimal _ActualPurchaseRate;
        private decimal _PurchaseTotal;
        private int _FlagBit;
        private Boolean _IsFreeItem;
        public IMaterialRecieptDetails()
        {
            Initialize();
        }
        public void Initialize()
        {
             _SlNo = 0;
             _FK_Items = 0;
             _FK_Category = 0;
             _Fk_Unit = 0;
             _FK_SubUnit = 0;
             _BatchNo=string.Empty;
             _ExpiryDate=DateTime.Now;
             _Quantity=0;
             _Free = 0;
             _ActQty=0;
             _PurRate = 0;
             _IncludePTax = false;
             _PurchaseTaxAmount = 0;
             _TaxPer = 0;
             _DiscountOnTax = false;
             _PurchaseDiscPercentage = 0;
             _PurchaseDiscAmount = 0;
             _NetPurchase = 0;
             _MRP = 0;
             _ProfitPer = 0;
             _ProfitAmount = 0;
             _DiscountPer = 0;
             _DiscountAmt = 0;
             _ActualPurchaseRate = 0;
             _PurchaseTotal = 0;
             _FlagBit = 0;
             _IsFreeItem = false;
          
        
        }


        public Boolean IsFreeItem
        {
            get { return _IsFreeItem; }
            set { _IsFreeItem = value; }
        }
        public Int64 FK_MaterialReciept
        {
            get { return _FK_MaterialReciept; }
            set { _FK_MaterialReciept = value; }
        }
       
        public Int64 SlNo
        {
            get { return _SlNo; }
            set { _SlNo = value; }
        }
        public int FlagBit
        {
            get { return _FlagBit; }
            set { _FlagBit = value; }
        }

        public Int64 FK_Items
        {
            get { return _FK_Items; }
            set { _FK_Items = value; }
        }
        public Int64 Fk_Unit
        {
            get { return _Fk_Unit; }
            set { _Fk_Unit = value; }
        }
        public Int64 FK_SubUnit   
        {
            get { return _FK_SubUnit; }
            set { _FK_SubUnit = value; }
        }
        public Int64 FK_Category   
        {
            get { return _FK_Category; }
            set { _FK_Category = value; }
        }
        public string BatchNo
        {
            get { return _BatchNo; }
            set { _BatchNo = value; }
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

           public decimal ActQty
        {
            get { return _ActQty; }
            set { _ActQty = value; }
        }
           public decimal PurRate
           {
               get { return _PurRate; }
               set { _PurRate = value; }
           }
           public bool IncludePTax
           {
               get { return _IncludePTax; }
               set { _IncludePTax = value; }
           }
           public decimal TaxPer
           {
               get { return _TaxPer; }
               set { _TaxPer = value; }
           }

           public decimal PurchaseTaxAmount 
           {
               get { return _PurchaseTaxAmount; }
               set { _PurchaseTaxAmount = value; }
           }
           public bool DiscountOnTax
           {
               get { return _DiscountOnTax; }
               set { _DiscountOnTax = value; }
           }
           public decimal PurchaseDiscPercentage
           {
               get { return _PurchaseDiscPercentage; }
               set { _PurchaseDiscPercentage = value; }
           }
        
        public decimal PurchaseDiscAmount
        {
            get { return _PurchaseDiscAmount; }
            set { _PurchaseDiscAmount = value; }
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
        public decimal ProfitAmount
        {
            get { return _ProfitAmount; }
            set { _ProfitAmount = value; }
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
        public bool IncludeSTax
        {
            get { return _IncludeSTax; }
            set { _IncludeSTax = value; }
        }

        public decimal SalesTaxAmount
        {
            get { return _SalesTaxAmount; }
            set { _SalesTaxAmount = value; }
        }
        public decimal SalesPrice
        {
            get { return _SalesPrice; }
            set { _SalesPrice = value; }
        }
        public decimal ActualPurchaseRate
        {
            get { return _ActualPurchaseRate; }
            set { _ActualPurchaseRate = value; }
        }
       
        public decimal PurchaseTotal
        {
            get { return _PurchaseTotal; }
            set { _PurchaseTotal = value; }
        }
  
    
    }
    public class IExpenseDetails
    {
        private Int64 _SlNo;
        private Int64 _ID_ExpeType;
        private Int64 _FK_ExpenseType;
        private Int64 _FK_Purchase;
        private decimal _Amount;
       
       
        public IExpenseDetails()
        {
            Initialize();
        }
        public void Initialize()
        {
            _SlNo = 0;
            _ID_ExpeType = 0;
            _FK_ExpenseType = 0;
            _FK_Purchase = 0;
            _Amount = 0;
        }
        public long SlNo
        {
            get { return _SlNo; }
            set { _SlNo = value; }
        }
        public long ID_ExpeType
        {
            get { return _ID_ExpeType; }
            set { _ID_ExpeType = value; }
        }
        public long FK_ExpenseType
        {
            get { return _FK_ExpenseType; }
            set { _FK_ExpenseType = value; }
        }
        public long FK_Purchase
        {
            get { return _FK_Purchase; }
            set { _FK_Purchase = value; }
        }
        public decimal Amount
        {
            get { return _Amount; }
            set { _Amount = value; }
        }
    }
    public class IPurchaseTaxDetails
    {
       
        private Int64 _SlNo;
        private Int64 _TaxTypeID;
        private string _TaxType;
        private string _Percentage;
        private Int64 _FK_Items;
        private bool _TaxInterstate;
        private bool _TaxIntrastate;
        public IPurchaseTaxDetails()
        {
            Initialize();
        }
        public void Initialize()
        {
            _SlNo = 0;
            _TaxTypeID = 0;
            _TaxType = string.Empty;
            _Percentage = string.Empty;
            _FK_Items = 0;
            _TaxInterstate = false;
            _TaxIntrastate = false;
            
        }
        public bool TaxInterstate
        {
            get { return _TaxInterstate; }
            set { _TaxInterstate = value; }
        }
        public bool TaxIntrastate
        {
            get { return _TaxIntrastate; }
            set { _TaxIntrastate = value; }
        }
        public long SlNo
        {
            get { return _SlNo; }
            set { _SlNo = value; }
        }
      
        public long TaxTypeID
        {
            get { return _TaxTypeID; }
            set { _TaxTypeID = value; }
        }
        public long FK_Items
        {
            get { return _FK_Items; }
            set { _FK_Items = value; }
        }
        public string TaxType
        {
            get { return _TaxType; }
            set { _TaxType = value; }
        }
        public string Percentage
        {
            get { return _Percentage; }
            set { _Percentage = value; }
        }
        


    }
    public class ISalesTaxDetails
    {

        private Int64 _SlNo;
        private Int64 _TaxTypeID;
        private string _TaxType;
        private string _Percentage;
        private Int64 _FK_Items;
        public ISalesTaxDetails()
        {
            Initialize();
        }
        public void Initialize()
        {
            _SlNo = 0;
            _TaxTypeID = 0;
            _TaxType = string.Empty;
            _Percentage = string.Empty;
            _FK_Items = 0;

        }
        public long SlNo
        {
            get { return _SlNo; }
            set { _SlNo = value; }
        }
        public long TaxTypeID
        {
            get { return _TaxTypeID; }
            set { _TaxTypeID = value; }
        }
        public long FK_Items
        {
            get { return _FK_Items; }
            set { _FK_Items = value; }
        }

        public string TaxType
        {
            get { return _TaxType; }
            set { _TaxType = value; }
        }
        public string Percentage
        {
            get { return _Percentage; }
            set { _Percentage = value; }
        }



    }
    public class IPurchaseGridDetails
    {
        private Int64 _FK_MaterialReciept;
        private Int64 _SlNo;
        private Int64 _FK_Items;
        private string _Item;
        private string _Units;
        private string _Category;
        private string _SubUnit;
        private Int64 _Fk_Unit;
        private Int64 _FK_SubUnit;
        private Int64 _FK_Category;
        private string _BatchNo;
        private DateTime _ExpiryDate;
        private string _HSN;
        private decimal _Quantity;
        private decimal _Free;
        private decimal _ActQty;
        private decimal _PurRate;
        private bool _IncludePTax;
        private decimal _TaxPer;
        private decimal _PurchaseTaxAmount;
        private bool _DiscountOnTax;
        private decimal _PurchaseDiscPercentage;
        private decimal _PurchaseDiscAmount;
        private decimal _NetPurchase;
        private decimal _MRP;
        private decimal _ProfitPer;
        private decimal _ProfitAmount;
        private decimal _DiscountPer;
        private decimal _DiscountAmt;
        private bool _IncludeSTax;
        private decimal _SalesTaxAmount;
        private decimal _SalesPrice;
        private decimal _ActualPurchaseRate;
        private decimal _PurchaseTotal;
        private Int64 _FlagBit;
        public IPurchaseGridDetails()
        {
            Initialize();
        }
        public void Initialize()
        {
            _SlNo = 0;
            _FK_Items = 0;
            _FK_Category = 0;
            _Fk_Unit = 0;
            _FK_SubUnit = 0;
            _BatchNo = string.Empty;
            _ExpiryDate = DateTime.Now;
            _Quantity = 0;
            _Free = 0;
            _ActQty = 0;
            _PurRate = 0;
            _IncludePTax = false;
            _PurchaseTaxAmount = 0;
            _TaxPer = 0;
            _DiscountOnTax = false;
            _PurchaseDiscPercentage = 0;
            _PurchaseDiscAmount = 0;
            _NetPurchase = 0;
            _MRP = 0;
            _ProfitPer = 0;
            _ProfitAmount = 0;
            _DiscountPer = 0;
            _DiscountAmt = 0;
            _ActualPurchaseRate = 0;
            _PurchaseTotal = 0;
            _Item = string.Empty;
            _Category = string.Empty;
            _Units = string.Empty;
            _SubUnit = string.Empty;
            _FlagBit = 0;


        }

        public Int64 FlagBit
        {
            get { return _FlagBit; }
            set { _FlagBit = value; }
        }


        public Int64 FK_MaterialReciept
        {
            get { return _FK_MaterialReciept; }
            set { _FK_MaterialReciept = value; }
        }

        public Int64 SlNo
        {
            get { return _SlNo; }
            set { _SlNo = value; }
        }

        public Int64 FK_Items
        {
            get { return _FK_Items; }
            set { _FK_Items = value; }
        }
        public Int64 Fk_Unit
        {
            get { return _Fk_Unit; }
            set { _Fk_Unit = value; }
        }
        public Int64 FK_SubUnit
        {
            get { return _FK_SubUnit; }
            set { _FK_SubUnit = value; }
        }
        public Int64 FK_Category
        {
            get { return _FK_Category; }
            set { _FK_Category = value; }
        }
        public string BatchNo
        {
            get { return _BatchNo; }
            set { _BatchNo = value; }
        }
        public string Item
        {
            get { return _Item; }
            set { _Item = value; }
        }
        public string Units
        {
            get { return _Units; }
            set { _Units = value; }
        }
        public string SubUnit
        {
            get { return _SubUnit; }
            set { _SubUnit = value; }
        }
        public string Category
        {
            get { return _Category; }
            set { _Category = value; }
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

        public decimal ActQty
        {
            get { return _ActQty; }
            set { _ActQty = value; }
        }
        public decimal PurRate
        {
            get { return _PurRate; }
            set { _PurRate = value; }
        }
        public bool IncludePTax
        {
            get { return _IncludePTax; }
            set { _IncludePTax = value; }
        }
        public decimal PurchaseTaxAmount
        {
            get { return _PurchaseTaxAmount; }
            set { _PurchaseTaxAmount = value; }
        }
        public decimal TaxPer
        {
            get { return _TaxPer; }
            set { _TaxPer = value; }
        }
        public bool DiscountOnTax
        {
            get { return _DiscountOnTax; }
            set { _DiscountOnTax = value; }
        }
        public decimal PurchaseDiscPercentage
        {
            get { return _PurchaseDiscPercentage; }
            set { _PurchaseDiscPercentage = value; }
        }

        public decimal PurchaseDiscAmount
        {
            get { return _PurchaseDiscAmount; }
            set { _PurchaseDiscAmount = value; }
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
        public decimal ProfitAmount
        {
            get { return _ProfitAmount; }
            set { _ProfitAmount = value; }
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
        public bool IncludeSTax
        {
            get { return _IncludeSTax; }
            set { _IncludeSTax = value; }
        }

        public decimal SalesTaxAmount
        {
            get { return _SalesTaxAmount; }
            set { _SalesTaxAmount = value; }
        }
        public decimal SalesPrice
        {
            get { return _SalesPrice; }
            set { _SalesPrice = value; }
        }
        public decimal ActualPurchaseRate
        {
            get { return _ActualPurchaseRate; }
            set { _ActualPurchaseRate = value; }
        }

        public decimal PurchaseTotal
        {
            get { return _PurchaseTotal; }
            set { _PurchaseTotal = value; }
        }


    }

}
