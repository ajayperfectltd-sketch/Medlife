/*----------------------------------- Business Layer Class -----------------------------------------------------------------------------------------------------------------------
Purpose    : 
Author     : Aiswarya
Copyright  : Perfect Software Solutions (Clt) Pvt Ltd
Created on : 02/05/2018
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
using WebPharma.Interface.General;
using WebPharma.Interface.WebPharma;
using WebPharma.DataAccess.WebPharma;

namespace WebPharma.Business.WebPharma
{
    public class BlOpeningStock:IOpeningStock
    {

        private byte _UserAction;
        private long _ID_OpeningStock;
        private Int16 _PageIndex;
        private Int16 _PageSize; 
        private long _FK_Store;
		private long _FK_Client;
        private DateTime _EnteredOn;
        private string _UserCode;
        private string _StoreName;
        private string _xmlOpening;
        private string _XmlExcelImport;
        private long _GroupID;
        private string _BatchNo;
        private string _ItemName;

        private long _FK_Category;
        private long _FK_SubCategory;
        private long _FK_Rack;
        private long _FK_Bin;
        private long _FK_Manufacturer;
        private long _FK_Generic;
      

        public BlOpeningStock()
		{
			Initialize();
		}
		public void Initialize()
        {
           
            _UserAction=1;
            _FK_Client = 1;
            _FK_Store=0;
            _EnteredOn = DateTime.Now;
		    _UserCode = string.Empty;
            _StoreName = string.Empty;
            _PageIndex = 1;
            _PageSize = 10;
            _xmlOpening = string.Empty;
            XmlExcelImport = string.Empty;
            _GroupID = 0;
            _BatchNo = string.Empty;
            _ItemName=string.Empty;
            _FK_Category = 0;
            _FK_SubCategory = 0;
            _FK_Rack = 0;
            _FK_Bin = 0;
            _FK_Manufacturer = 0;
            _FK_Generic = 0;
		}
        public long FK_Generic
        {
            get { return _FK_Generic; }
            set { _FK_Generic = value; }
        }
        public long FK_Manufacturer
        {
            get { return _FK_Manufacturer; }
            set { _FK_Manufacturer = value; }
        }

        public long FK_Bin
        {
            get { return _FK_Bin; }
            set { _FK_Bin = value; }
        }

        public long FK_Rack
        {
            get { return _FK_Rack; }
            set { _FK_Rack = value; }
        }
        public long FK_SubCategory
        {
            get { return _FK_SubCategory; }
            set { _FK_SubCategory = value; }
        }
        public long FK_Category
        {
            get { return _FK_Category; }
            set { _FK_Category = value; }
        }
        
		public byte UserAction
		{
			get { return _UserAction; }
			set { _UserAction = value; }
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
        public long GroupID
        {
            get { return _GroupID; }
            set { _GroupID = value; }
        }
        public long FK_Client
        {
            get { return _FK_Client; }
            set { _FK_Client = value; }
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
        public string xmlOpening
        {
            get { return _xmlOpening; }
            set { _xmlOpening = value; }
        }
        public long MasterId
        {
            get { return _ID_OpeningStock; }
            set { _ID_OpeningStock = value; }
        }
        public string XmlExcelImport
        {
            get { return _XmlExcelImport; }
            set { _XmlExcelImport = value; }
        }
     
        public long FK_Store
        {
            get { return _FK_Store; }
            set { _FK_Store = value; }
        }
      
     
        public DateTime EnteredOn
        {
            get { return _EnteredOn; }
            set { _EnteredOn = value; }
        }     
        public string UserCode
        {
            get { return _UserCode; }
            set { _UserCode = value; }
        }
     
        public string StoreName
        {
            get { return _StoreName; }
            set { _StoreName = value; }
        }
		public long InsertData()
		{
			long status;
            DalOpeningStock dalopeningstock = new DalOpeningStock();
			status = 0;
			try
			{
				_UserAction = 1;
                status = dalopeningstock.UpdateOpeningStock(this);
                dalopeningstock = null;
			}
			catch (Exception ex)
			{
 
			}
			return status;
		}
        public long InsertMultipleOpeningStockData()
        {
            long status;
            DalOpeningStock dalopeningstock = new DalOpeningStock();
            status = 0;
            try
            {
                _UserAction = 1;
                status = dalopeningstock.UpdateMultipleOpeningStock(this);
                dalopeningstock = null;
            }
            catch (Exception ex)
            {

            }
            return status;
        }
		public long UpdateData()
		{
			_UserAction = 2;
            DalOpeningStock dalopeningstock = new DalOpeningStock();
			long status;
			status = 0;
			try
			{
                status = dalopeningstock.UpdateOpeningStock(this);
                dalopeningstock = null;
			}
			catch (Exception ex)
			{
 
			}
			return status;
		}
        public void SelectData()
        {
            DalOpeningStock dalopeningstock = new DalOpeningStock();
            IOpeningStock Iopeningstock = (IOpeningStock)dalopeningstock.SelectOpeningStock(this);
            _ID_OpeningStock = Iopeningstock.MasterId;
            //_FK_Items = Iopeningstock.FK_Items;
            //_ItemName = Iopeningstock.ItemName;
            //_FK_Store = Iopeningstock.FK_Store;
            //_StoreName = Iopeningstock.StoreName;
            //_BatchNo = Iopeningstock.BatchNo;
            //_ExpiryDate = Iopeningstock.ExpiryDate;
            //_PurRate = Iopeningstock.PurRate;
            //_MRP = Iopeningstock.MRP;
            //_RetailPrice = Iopeningstock.RetailPrice;
            //_WholeSalePrice = Iopeningstock.WholeSalePrice;
            //_PrivilagePrice = Iopeningstock.PrivilagePrice;
            //_VAT = Iopeningstock.VAT;
            //_OpeningStock = Iopeningstock.OpeningStock;
            //_BarCode = Iopeningstock.BarCode;
        }
        public DataTable SelectAllData()
        {
            DalOpeningStock dalopeningstock = new DalOpeningStock();
            DataTable dtbl = dalopeningstock.SelectAllOpeningStock(this);
            dalopeningstock = null;
            return dtbl;


        }
        public DataTable SelectMultipleOpeningStock()
        {
            DalOpeningStock dalopeningstock = new DalOpeningStock();
            DataTable dtbl = dalopeningstock.SelectMultipleOpeningStock(this);
            dalopeningstock = null;
            return dtbl;


        }
        public DataTable SelectAllGridData()
        {
            DalOpeningStock dalopeningstock = new DalOpeningStock();
            DataTable dtbl = dalopeningstock.OpeningStockSelectGrid(this);
            dalopeningstock = null;
            return dtbl;


        }
        public DataTable ValidateExcelData()
        {
            DataTable dtbl = new DataTable();
            DalOpeningStock dalopeningstock = new DalOpeningStock();
            dtbl = dalopeningstock.ValidateExcelData(this);
            dalopeningstock = null;
            return dtbl;
        }
    }
    public class IOpngStock
    {
        private Int64 _ID_OpeningStock;
        private Int64 _SlNo;
        private Int64 _FK_Items;
        private string _Item;
        private string _BatchNo;
        private DateTime _ExpiryDate;
        private decimal _PurRate;
        private decimal _PurTaxAmount;
        private decimal _MRP;
        private decimal _SalePrice;
        private bool _IncludeSTax;
        private decimal _SalesTaxAmount;
        private decimal _Quantity;
        private decimal _CurrentStock;
        private decimal _OpeningLiability;
        private decimal _CurrentLiability;
        private decimal _ActualSalePrice;
        private decimal _ActualPurRate;
        private decimal _ProfitPer;
        private decimal _ProfitAmt;
        private decimal _DiscountPer;
        private decimal _DiscountAmt;
        private long _FK_TaxSettings;
        private decimal _PrevOpeningStock;
        private decimal _OpeningStock;
        private long _ID_Stock;
        public IOpngStock()
        {
            Initialize();
        }
        public void Initialize()
        {
            _PrevOpeningStock = 0;
            _OpeningStock = 0;
            _ID_OpeningStock = 0;
            _SlNo = 0;
            _FK_Items = 0;
            _Item = string.Empty;
            _BatchNo = string.Empty;
            _ExpiryDate = DateTime.Now;
            _PurRate = 0;
            _PurTaxAmount = 0;
            _MRP = 0;
            _SalePrice = 0;
            _IncludeSTax = false;
            _SalesTaxAmount = 0;
            _Quantity = 0;
            _CurrentStock = 0;
            _CurrentLiability = 0;
            _OpeningLiability = 0;
            _ActualSalePrice = 0;
            _ActualPurRate = 0;
            _ProfitPer = 0;
            _ProfitAmt = 0;
            _DiscountPer = 0;
            _DiscountAmt = 0;
            _FK_TaxSettings = 0;
            _ID_Stock = 0;
        }
     
        public long FK_Items
        {
            get { return _FK_Items; }
            set { _FK_Items = value; }
        }
        public long SlNo
        {
            get { return _SlNo; }
            set { _SlNo = value; }
        }
        public string Item
        {
            get { return _Item; }
            set { _Item = value; }
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
        public decimal PurRate
        {
            get { return _PurRate; }
            set { _PurRate = value; }
        }
        public decimal MRP
        {
            get { return _MRP; }
            set { _MRP = value; }
        }
        
        public decimal ActualSalePrice
        {
            get { return _ActualSalePrice; }
            set { _ActualSalePrice = value; }
        }
        public decimal ActualPurRate
        {
            get { return _ActualPurRate; }
            set { _ActualPurRate = value; }
        }
        public decimal PurTaxAmount
        {
            get { return _PurTaxAmount; }
            set { _PurTaxAmount = value; }
        }
        public decimal SalePrice
        {
            get { return _SalePrice; }
            set { _SalePrice = value; }
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
        public decimal Quantity
        {
            get { return _Quantity; }
            set { _Quantity = value; }
        }
        public decimal CurrentStock
        {
            get { return _CurrentStock; }
            set { _CurrentStock = value; }
        }
        public decimal OpeningStock
        {
            get { return _OpeningStock; }
            set { _OpeningStock = value; }
        }
        public decimal PrevOpeningStock
        {
            get { return _PrevOpeningStock; }
            set { _PrevOpeningStock = value; }
        }
        public decimal CurrentLiability
        {
            get { return _CurrentLiability; }
            set { _CurrentLiability = value; }
        }
        public decimal OpeningLiability
        {
            get { return _OpeningLiability; }
            set { _OpeningLiability = value; }
        }
        public decimal ProfitPer
        {
            get { return _ProfitPer; }
            set { _ProfitPer = value; }
        }
        public decimal ProfitAmt
        {
            get { return _ProfitAmt; }
            set { _ProfitAmt = value; }
        }
        //public decimal _DiscountPer
        //{
        //    get { return _DiscountPer; }
        //    set { _DiscountPer = value; }
        //}
        public decimal DiscountAmt
        {
            get { return _DiscountAmt; }
            set { _DiscountAmt = value; }
        }
        public decimal DiscountPer
        {
            get { return _DiscountPer; }
            set { _DiscountPer = value; }
        }
        public long FK_TaxSettings
        {
            get { return _FK_TaxSettings; }
            set { _FK_TaxSettings = value; }
        }

        public long ID_Stock
        {
            get { return _ID_Stock; }
            set { _ID_Stock = value; }
        }
         
    }
}
