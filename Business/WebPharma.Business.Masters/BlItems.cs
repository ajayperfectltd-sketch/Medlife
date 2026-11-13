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
using System.Threading.Tasks;
using WebPharma.Interface.General;
using WebPharma.Interface.Masters;
using WebPharma.DataAccess.Masters;
using System.Data;



namespace WebPharma.Business.Masters
{
	public class BlItems : IItems
	{
 
		private byte _UserAction;
		private Int16 _PageIndex;
		private Int16 _PageSize; 
		private long _ID_Items;
		private string _Name;
		private string _ShortName;
        private long _FK_CategoryFirst;
        private long _FK_CategorySecond;
        private long _FK_CategoryThird;
        private long _FK_Units;
        private long _FK_SubUnit;
        private long _FK_Manufacturer;
        private long _FK_Generic;
        private long _FK_Rack;
        private long _FK_Bin;
        private decimal _MinLevel;
        private decimal _MaxLevel;
        private decimal _ROLevel;
        private decimal _ROQuantity;
        private decimal _DangerLevel;
        private int _ExpairyValidation;
        private int _ExpiryWarningBefore;
        private Boolean _StopSale;
        private string _StopSaleRemarks;
        private string _HSNCode;
        private long _FK_Schedule;
        private string _StoragePlace;
        private Boolean _NotReturnable;
        private int _ReturnBeforDays;
        private Boolean _Packed;
        private Boolean _Combo;
		private string _UserCode;
		private DateTime _EnteredOn;
		private long _FK_Client;
        private long _FK_Reason;
        private string _XmlImport;
        private string _XmlItemsDetails;
        private string _XmlTaxDetails;
        private string _PartNo;
        private decimal _DiscPer;
        private string _BatchNo;
        private long _FK_Store;
        private long _FK_Supplier;
        private decimal _MRP;
        private DateTime _Date;
        private long _FK_User;
        private bool _Expiry;
        private DateTime _ExpiryDate;
        private string _Mode;

		public BlItems()
		{
			Initialize();
		}
		public void Initialize()
        {
            _Mode = string.Empty;
            _ExpiryDate = DateTime.Now;
            _Expiry = false;
			_ID_Items = 0;
			_Name = string.Empty;
			_ShortName = string.Empty;
			_FK_CategoryFirst = 0;
			_FK_CategorySecond = 0;
			_FK_CategoryThird = 0;
            _FK_Units = 0;
            _FK_SubUnit = 0;
            _FK_Manufacturer = 0;
            _FK_Generic = 0;
            _FK_Rack = 0;
            _FK_Bin = 0;
			_MinLevel = 0;
			_MaxLevel = 0;
			_ROLevel = 0;
			_ROQuantity = 0;
			_DangerLevel = 0;
            _ExpairyValidation = 0;
            _ExpiryWarningBefore = 0;
			_StopSale = false;
			_StopSaleRemarks = string.Empty;
            _HSNCode = string.Empty;
            _FK_Schedule = 0;
            _StoragePlace=string.Empty;
            _NotReturnable=false;
            _ReturnBeforDays=0;
			_UserCode = string.Empty;
			_EnteredOn = DateTime.Now;
			_FK_Client = 1;
            _PageIndex = 1;
            _PageSize = 100;
            _UserAction = 1;
            _XmlImport=string.Empty;
            _XmlItemsDetails = string.Empty;
            _XmlTaxDetails = string.Empty;
            _PartNo = string.Empty;
            _DiscPer = 0;
            _FK_Reason = 0;
            _BatchNo = string.Empty;
            _FK_Store = 0;
            _FK_Supplier = 0;
            _MRP = 0;
            _Date = DateTime.Now;
            _FK_User = 0;
		}
        public string Mode
        {
            get { return _Mode; }
            set { _Mode = value; }
        }
        public long FK_User
        {
            get { return _FK_User; }
            set { _FK_User = value; }
        }
        public DateTime ExpiryDate
        {
            get { return _ExpiryDate; }
            set { _ExpiryDate = value; }
        }
        public string BatchNo
        {
            get { return _BatchNo; }
            set { _BatchNo = value; }
        }
        public decimal MRP
        {
            get { return _MRP; }
            set { _MRP = value; }
        }
        public DateTime Date
        {
            get { return _Date; }
            set { _Date = value; }
        }

        public long FK_Store
        {
            get { return _FK_Store; }
            set { _FK_Store = value; }
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
        public decimal DiscPer
        {
            get { return _DiscPer; }
            set { _DiscPer = value; }
        }
     
        public string PartNo
        {
            get { return _PartNo; }
            set { _PartNo = value; }
        }
     
        public string XmlImport
        {
            get { return _XmlImport; }
            set { _XmlImport = value; }
        }
        public string XmlItemsDetails
        {
            get { return _XmlItemsDetails; }
            set { _XmlItemsDetails = value; }
        }
        public string XmlTaxDetails
        {
            get { return _XmlTaxDetails; }
            set { _XmlTaxDetails = value; }
        }
      
   
   
		public byte UserAction
		{
			get { return _UserAction; }
			set { _UserAction = value; }
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
		public long MasterId
		{
			get { return _ID_Items; }
			set { _ID_Items = value; }
		}
		
		public string Name
		{
			get { return _Name; }
			set { _Name = value; }
		}
		public string ShortName
		{
			get { return _ShortName; }
			set { _ShortName = value; }
		}
	
		public long FK_CategoryFirst
		{
			get { return _FK_CategoryFirst; }
			set { _FK_CategoryFirst = value; }
		}
		public long FK_CategorySecond
		{
			get { return _FK_CategorySecond; }
			set { _FK_CategorySecond = value; }
		}
		public long FK_CategoryThird
		{
			get { return _FK_CategoryThird; }
			set { _FK_CategoryThird = value; }
		}
		public long FK_Units
		{
			get { return _FK_Units; }
			set { _FK_Units = value; }
		}
        public long FK_SubUnit
        {
            get { return _FK_SubUnit ; }
            set { _FK_SubUnit = value; }
        }
	
		public long FK_Manufacturer
		{
			get { return _FK_Manufacturer; }
			set { _FK_Manufacturer = value; }
		}
        public long FK_Generic
        {
            get { return _FK_Generic; }
            set { _FK_Generic = value; }
        }

        public Int64 FK_Rack
        {
            get { return _FK_Rack; }
            set { _FK_Rack = value; }
        }
        public Int64 FK_Bin
        {
            get { return _FK_Bin; }
            set { _FK_Bin = value; }
        }
    
		public decimal MinLevel
		{
			get { return _MinLevel; }
			set { _MinLevel = value; }
		}
		public decimal MaxLevel
		{
			get { return _MaxLevel; }
			set { _MaxLevel = value; }
		}

		public decimal ROLevel
		{
			get { return _ROLevel; }
			set { _ROLevel = value; }
		}
		public decimal ROQuantity
		{
			get { return _ROQuantity; }
			set { _ROQuantity = value; }
		}

		public decimal DangerLevel
		{
			get { return _DangerLevel; }
			set { _DangerLevel = value; }
		}

        public int ExpairyValidation
        {
            get { return _ExpairyValidation; }
			set { _ExpairyValidation = value; }
        }
        public Boolean StopSale
        {
            get { return _StopSale; }
            set { _StopSale = value; }
        }
        public string StopSaleRemarks
        {
            get { return _StopSaleRemarks; }
            set { _StopSaleRemarks = value; }
        }

        public string HSNCode
        {
            get { return _HSNCode; }
            set { _HSNCode = value; }
        }
        public long FK_Schedule
        {
            get { return _FK_Schedule; }
            set { _FK_Schedule = value; }
        }
         public string StoragePlace
        {
            get { return _StoragePlace ;}
            set { _StoragePlace = value; }
        }
         public Boolean NotReturnable
         {
             get { return _NotReturnable; }
             set { _NotReturnable = value; }
         }

         public int ReturnBeforDays
         {
            get { return _ReturnBeforDays; }
             set { _ReturnBeforDays = value; }
         }
         public int ExpiryWarningBefore
         {
             get { return _ExpiryWarningBefore; }
             set { _ExpiryWarningBefore = value; }
         }

        public Boolean Packed
        {
            get { return _Packed; }
            set { _Packed = value; }
        }
        public Boolean Combo
        {
            get { return _Combo; }
            set { _Combo = value; }
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
		public long FK_Client
		{
			get { return _FK_Client; }
			set { _FK_Client = value; }
		}
        public bool Expiry
        {
            get { return _Expiry; }
            set { _Expiry = value; }
        }
		public long InsertData()
		{
			long status;
			DalItems dalitems = new DalItems();
			status = 0;
			try
			{
				_UserAction = 1;
				status = dalitems.UpdateItems(this);
				dalitems=null;
			}
			catch (Exception ex)
			{
 
			}
			return status;
		}
        public DataTable ShowInCounterItems()
        {
            DalItems dalitems = new DalItems();
            DataTable dtbl = new DataTable();
            try
            {
                dtbl = dalitems.proShowInCounterItems(this);
            }
            catch (Exception ex)
            {
            }
            return dtbl;
        }
		public long UpdateData()
		{
			_UserAction = 2;
			DalItems dalitems = new DalItems();
			long status;
			status = 0;
			try
			{
				status =  dalitems.UpdateItems(this);
				dalitems=null;
			}
			catch (Exception ex)
			{
 
			}
			return status;
		}
 
		public long DeleteData()
		{
			_UserAction =3;
			DalItems dalitems = new DalItems();
			long status;
			status = 0;
			try
			{
				status =  dalitems.DeleteItems(this);
				dalitems=null;
			}
			catch (Exception ex)
			{
 
			}
			return status;
		}
 
		public void SelectData()
		{
			DalItems dalitems = new DalItems();
			IItems iitems;
			iitems= (IItems)dalitems.SelectItems(this);
			_Name = iitems.Name;
			_ShortName = iitems.ShortName;
            _FK_Rack = iitems.FK_Rack;
            _FK_Bin = iitems.FK_Bin;
			_FK_CategoryFirst = iitems.FK_CategoryFirst;
			_FK_CategorySecond = iitems.FK_CategorySecond;
			_FK_CategoryThird = iitems.FK_CategoryThird;
			_FK_Units = iitems.FK_Units;
            _FK_SubUnit = iitems.FK_SubUnit;
			_FK_Manufacturer = iitems.FK_Manufacturer;
			
            _FK_Rack = iitems.FK_Rack;
		
			_MinLevel = iitems.MinLevel;
			_MaxLevel = iitems.MaxLevel;
			_ROLevel = iitems.ROLevel;
			_ROQuantity = iitems.ROQuantity;
			_DangerLevel = iitems.DangerLevel;
            _NotReturnable = iitems.NotReturnable;
            _ReturnBeforDays = iitems.ReturnBeforDays;
            _Packed = iitems.Packed;
			
			_StopSale = iitems.StopSale;
			_StopSaleRemarks = iitems.StopSaleRemarks;


            _FK_Schedule = iitems.FK_Schedule;
            _HSNCode = iitems.HSNCode;
            _ExpiryWarningBefore = iitems.ExpiryWarningBefore;
		}
        public DataTable SelectExpiryItems()
        {
            DataTable dtbl = new DataTable();
            DalItems dalItems = new DalItems();
            dtbl = dalItems.SelectExpiryItems(this);
            dalItems = null;
            return dtbl;

        }
		public DataTable SelectAllData()
		{
			DataTable dtbl = new DataTable();
			DalItems dalItems = new DalItems();
			dtbl = dalItems.SelectAllItems(this);
			dalItems = null;
			return dtbl;

        }
        public DataTable ItemsSelectForReport()
        {
            DataTable dtbl = new DataTable();
            DalItems dalitem = new DalItems();
            dtbl = dalitem.ItemsSelectForReport(this);
            dalitem = null;
            return dtbl;
        }
        public DataTable selectUsercode()
        {
            DataTable dtbl = new DataTable();
            DalItems dalitem = new DalItems();
            dtbl = dalitem.selectUsercode(this);
            dalitem = null;
            return dtbl;
        }
        public DataTable ItemsSelectCurrentStock()
        {
            DataTable dtbl = new DataTable();
            DalItems dalitem = new DalItems();
            dtbl = dalitem.ItemsSelectCurrentStock(this);
            dalitem = null;
            return dtbl;
        }
        public DataTable StockItemsSelectPopup()
        {
            DataTable dtbl = new DataTable();
            DalItems dalItems = new DalItems();
            dtbl = dalItems.proItemsSelectPopUp(this);
           
            dalItems = null;
            return dtbl;
        }
        public DataTable StockItemsSelectPopupFromQuotation()
        {
            DataTable dtbl = new DataTable();
            DalItems dalItems = new DalItems();
            dtbl = dalItems.proItemsSelectPopUpFromQuotation(this);

            dalItems = null;
            return dtbl;
        }
        public DataTable BarcodeGeneration()
        {
            DataTable dtbl = new DataTable();
            DalItems dalitems = new DalItems();
            dtbl = dalitems.BarcodeGeneration(this);
            dalitems = null;
            return dtbl;
        }
        public DataTable StockItemsSelect()
        {
            DataTable dtbl = new DataTable();
            DalItems dalItems = new DalItems();
            dtbl = dalItems.StockItemsSelect(this);
            dalItems = null;
            return dtbl;
        }
        
        public DataTable SelectCurrentStock()
        {
            DataTable dtbl = new DataTable();
            DalItems dalItems = new DalItems();
            dtbl = dalItems.SelectCurrentStock(this);
            dalItems = null;
            return dtbl;
        }
        public DataTable SelectCurrentStockForStockInPriceConversion()
        {
            DataTable dtbl = new DataTable();
            DalItems dalItems = new DalItems();
            dtbl = dalItems.ItemsSelectForStockInPriceConversion(this);
            dalItems = null;
            return dtbl;
        }
        
        public DataTable SelectCurrentStockWithBatch()
        {
            DataTable dtbl = new DataTable();
            DalItems dalItems = new DalItems();
            dtbl = dalItems.SelectCurrentStockWithBatch(this);
            dalItems = null;
            return dtbl;
        }
        public int GetPartNoExist()
        {
            DalItems dalItems = new DalItems();
            int inCount = dalItems.GetPartNo(this);
             dalItems = null;
             return inCount;
           
        }
        
        public DataTable GetMinLevelItemStockName()
        {
          
            DataTable dtbl = new DataTable();
            DalItems dalItems = new DalItems();
            dtbl = dalItems.GetMinitemName(this);
            dalItems = null;
            return dtbl;
        }
     
        public string ImportItems()
        {
            DalItems dalitem = new DalItems();
            string status = "";
            try
            {
                status = dalitem.ImportItems(this);

                dalitem = null;
            }
            catch(Exception ex)
           
            {
                string st = ex.ToString();
            }
            return status;
        }
        public DataTable ItemsSearchForPriceCheck()
        {
            DataTable dtbl = new DataTable();
            DalItems dalitems = new DalItems();
            
            try
            {
                dtbl = dalitems.ItemsSearchForPriceCheck(this);
                dalitems = null;
            }
            catch
            {
            }
            return dtbl;
        }
        public DataTable ItemsSelectForStockCheck()
        {
            DataTable dtbl = new DataTable();
            DalItems dalitem = new DalItems();
            dtbl = dalitem.ItemsSelectForStockCheck(this);
            dalitem = null;
            return dtbl;
        }
        public DataTable ItemsSelectForStockDetails()
        {
            DataTable dtbl = new DataTable();
            DalItems dalitem = new DalItems();
            dtbl = dalitem.ItemsSelectForStockDetails(this);
            dalitem = null;
            return dtbl;
        }
        public DataTable ItemsSelectForDropdownFill()
        {
            DataTable dtbl = new DataTable();
            DalItems dalitem = new DalItems();
            dtbl = dalitem.ItemsSelectForDropdownFill(this);
            dalitem = null;
            return dtbl;
        }
        public DataTable ItemsSelectForDropdownFillFromStore()
        {
            DataTable dtbl = new DataTable();
            DalItems dalitem = new DalItems();
            dtbl = dalitem.ItemsSelectForDropdownFillFromStore(this);
            dalitem = null;
            return dtbl;
        }
        public DataTable SelectItemBatch()
        {
            DataTable dtbl = new DataTable();
            DalItems dalitem = new DalItems();
            dtbl = dalitem.SelectItemBatch(this);
            dalitem = null;
            return dtbl;
        }
        public DataTable SelectItemsDetails()
        {
            DataTable dtbl = new DataTable();
            DalItems dalitem = new DalItems();
            dtbl = dalitem.SelectItemsDetails(this);
            dalitem = null;
            return dtbl;
        }
        public DataTable SelectTaxDetails()
        {
            DataTable dtbl = new DataTable();
            DalItems dalitem = new DalItems();
            dtbl = dalitem.SelectTaxDetails(this);
            dalitem = null;
            return dtbl;
        }

        public DataTable SelectBarcode()
        {
            DataTable dtblBarcode = new DataTable();
            DalItems dalitem = new DalItems();
            dtblBarcode = dalitem.SelectBarcode(this);
            dalitem = null;
            return dtblBarcode;
        }
        public int ItemExistenceChecking()
        {
            int Return = 0;
            DalItems dalitem = new DalItems();
            Return = dalitem.ItemExistenceChecking(this);
            dalitem = null;
            return Return;
        }
        public DataTable SelectItemWiseTaxDetails()
        {
            DataTable dtbl = new DataTable();
            DalItems dalitem = new DalItems();
            dtbl = dalitem.SelectItemWiseTaxDetails(this);
            dalitem = null;
            return dtbl;
        }
    }
    public class ItemsDetails
    {
        private string _Name;
        private string _ShortName;
        private string _Model;
        private string _CategoryFirst;
        private  string _CategorySecond;
        private string _CategoryThird;
        private string  _Units;
        private string _Brand;
        private string  _Manufacturer;
        private string _supplier;
        private string _Size;
        private  string _Color;
        private string _Storage;
        private string   _Discount;
        private decimal _MinimumSalesPrice;
        private Boolean _EditSalesPrice;
        private decimal _MinLevel;
        private decimal _MaxLevel;
        private decimal _ROLevel;
        private decimal _ROQuantity;
        private decimal _DangerLevel;
        private string _Description;
      
        private Boolean _StopSale;
        private string _StopSaleRemarks;
        private string  _ItemType;
        private string _SpecialMessage;
        private string _Barcode;
       
        private decimal _SalesPrice;
        private decimal _LandedCost;
        private decimal _SellingPrice1;
        private decimal _SellingPrice2;
        private string _PartNo;
        private Boolean _ShowCounter;



        public decimal LandedCost
        {
            get { return _LandedCost; }
            set { _LandedCost = value; }
        }


        public decimal SellingPrice1
        {
            get { return _SellingPrice1; }
            set { _SellingPrice1 = value; }
        }


        public decimal SellingPrice2
        {
            get { return _SellingPrice2; }
            set { _SellingPrice2 = value; }
        }



        public string PartNo
        {
            get { return _PartNo; }
            set { _PartNo = value; }
        }


        public Boolean ShowCounter
        {
            get { return _ShowCounter; }
            set { _ShowCounter = value; }
        }


        public string Barcode
        {
            get { return _Barcode; }
            set { _Barcode = value; }
        }

       
        public decimal SalesPrice
        {
            get { return _SalesPrice; }
            set { _SalesPrice = value; }
        }



        public string Name
        {
            get { return _Name; }
            set { _Name = value; }
        }
        public string ShortName
        {
            get { return _ShortName; }
            set { _ShortName = value; }
        }
        public string Model
        {
            get { return _Model; }
            set { _Model = value; }
        }
        public string  CategoryFirst
        {
            get { return _CategoryFirst; }
            set {_CategoryFirst = value; }
        }
        public string CategorySecond
        {
            get { return _CategorySecond; }
            set { _CategorySecond = value; }
        }
        public string CategoryThird
        {
            get { return _CategoryThird; }
            set { _CategoryThird = value; }
        }
        public  string Units
        { 
            get { return _Units; }
            set { _Units = value; }
        }
        public string Brand
        {
            get { return _Brand; }
            set { _Brand = value; }
        }
        public string Manufacturer
        {
            get { return _Manufacturer; }
            set { _Manufacturer = value; }
        }
        public string supplier
        {
            get { return _supplier; }
            set { _supplier = value; }
        }
        public string  Size
        {
            get { return _Size; }
            set { _Size = value; }
        }
        public string Color
        {
            get { return _Color; }
            set { _Color = value; }
        }
        public string Storage
        {
            get { return _Storage; }
            set { _Storage = value; }
        }
        public string  Discount
        {
            get { return _Discount; }
            set { _Discount = value; }
        }
        public decimal MinimumSalesPrice
        {
            get { return _MinimumSalesPrice; }
            set { _MinimumSalesPrice = value; }
        }
        public Boolean EditSalesPrice
        {
            get { return _EditSalesPrice; }
            set { _EditSalesPrice = value; }
        }
        public decimal MinLevel
        {
            get { return _MinLevel; }
            set { _MinLevel = value; }
        }
        public decimal MaxLevel
        {
            get { return _MaxLevel; }
            set { _MaxLevel = value; }
        }
        public decimal ROLevel
        {
            get { return _ROLevel; }
            set { _ROLevel = value; }
        }
        public decimal ROQuantity
        {
            get { return _ROQuantity; }
            set { _ROQuantity = value; }
        }
        public decimal DangerLevel
        {
            get { return _DangerLevel; }
            set { _DangerLevel = value; }
        }
        public string Description
        {
            get { return _Description; }
            set { _Description = value; }
        }
       
     
        public Boolean StopSale
        {
            get { return _StopSale; }
            set { _StopSale = value; }
        }
        public string StopSaleRemarks
        {
            get { return _StopSaleRemarks; }
            set { _StopSaleRemarks = value; }
        }
        public string ItemType
        {
            get { return _ItemType; }
            set { _ItemType = value; }
        }
        public string SpecialMessage
        {
            get { return _SpecialMessage; }
            set { _SpecialMessage = value; }
        }

    }
    public class ITaxDetails
    {
        private Int64 _ID_TaxType;
        private decimal _Percentage;
        public Int64 ID_TaxType
        {
            get { return _ID_TaxType; }
            set { _ID_TaxType = value; }
        }
        public decimal Percentage
        {
            get { return _Percentage; }
            set { _Percentage = value; }
        }
    }

    public class IItemsDetails
    {
       
        private Int64 _FK_Items;
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
       
     


    }
}
