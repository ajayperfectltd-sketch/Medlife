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
using WebPharma.Interface.General;
using WebPharma.Interface.WebPharma;
using WebPharma.DataAccess.WebPharma;

namespace WebPharma.Business.WebPharma
{
	public class BlStock : IStock
	{
 
		private byte _UserAction;
		private long _ID_Stock;		
		private long _FK_Items;
        private long _FK_Store;
        private long _FK_RackArrangement;
        private string _BatchNo;
        private DateTime _ExpiryDate;
        private decimal _PurRate;
        private decimal _MRP;
        private decimal _OpeningStock;
        private decimal _CurrentStock;
        //private decimal _Off_FK_Stock;
        //private decimal _Off_Discount;
		private long _FK_Client;
        private DateTime _EnteredOn;
        private string _UserCode;
        private Int64 _FK_Units; 
        
        private string _xmlStockItems;
        private DateTime _TransDate;
        private long _FK_Category;
        private decimal _PurchaseTaxAmount;
        private decimal _PurchaseDiscPercentage;
        private Int16 _PageIndex;
        private Int16 _PageSize;
        private string _Unit;
        private Int16 _NoOfUnits;
        private decimal _SalePrice;
        private decimal _ActualSalPrice;
        private decimal _Tostore;
        private decimal _SalesTaxAmount;
        private string _Mode;
		public BlStock()
		{
			Initialize();
		}
		public void Initialize()
		{
            
            _UserAction=1;
            _FK_Client = 1;
            _PageIndex = 1;
            _PageSize = 10;
			_ID_Stock = 0;
			_FK_Items = 0;
            _FK_Store=0;
            _FK_RackArrangement=0;
            _BatchNo = string.Empty;
            _ExpiryDate=DateTime.Now;
            _PurRate=0;
            _MRP = 0;
            _PurchaseTaxAmount = 0;
			_OpeningStock = 0;
            _CurrentStock = 0;
            _EnteredOn = DateTime.Now;
		    _UserCode = string.Empty;			
		    _FK_Units=0;
            _TransDate = DateTime.Now;
            _FK_Category = 0;
            _PurchaseDiscPercentage = 0;
            _Unit = string.Empty;
            _NoOfUnits = 0;
            _SalePrice = 0;
            _Tostore = 0;
            _ActualSalPrice = 0;
            _SalesTaxAmount = 0;
            _Mode = string.Empty;
		}
        public string Mode
        {
            get { return _Mode; }
            set { _Mode = value; }
        }
        public decimal SalePrice
        {
            get { return _SalePrice; }
            set { _SalePrice = value; }
        }
        public decimal SalesTaxAmount
        {
            get { return _SalesTaxAmount; }
            set { _SalesTaxAmount = value; }
        }
        public decimal ActualSalPrice
        {
            get { return _ActualSalPrice; }
            set { _ActualSalPrice = value; }
        }
        public long FK_Category
        {
            get { return _FK_Category; }
            set { _FK_Category = value; }
        }
        public decimal Tostore
        {
            get { return _Tostore; }
            set { _Tostore = value; }
        }
        public string xmlStockItems
        {
            get { return _xmlStockItems; }
            set { _xmlStockItems = value; }
        }
        public string Unit
        {
            get { return _Unit; }
            set { _Unit = value; }
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
        public Int16 NoOfUnits
        {
            get { return _NoOfUnits; }
            set { _NoOfUnits = value; }
        }
		public byte UserAction
		{
			get { return _UserAction; }
			set { _UserAction = value; }
		}
        public long FK_Client
        {
            get { return _FK_Client; }
            set { _FK_Client = value; }
        }
   
        public long MasterId
        {
            get { return _ID_Stock; }
            set { _ID_Stock = value; }
        }
        public long FK_Items
        {
            get { return _FK_Items; }
            set { _FK_Items = value; }
        }
        public long FK_Store
        {
            get { return _FK_Store; }
            set { _FK_Store = value; }
        }
        public long FK_RackArrangement
        {
            get { return _FK_RackArrangement; }
            set { _FK_RackArrangement = value; }
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
        public decimal PurchaseTaxAmount
        {
            get { return _PurchaseTaxAmount; }
            set { _PurchaseTaxAmount = value; }
        }
      
      
        public decimal OpeningStock
        {
            get { return _OpeningStock; }
            set { _OpeningStock = value; }
        }
        public decimal CurrentStock
        {
            get { return _CurrentStock; }
            set { _CurrentStock = value; }
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
        public Int64 FK_Units
        {
            get { return _FK_Units; }
            set { _FK_Units = value; }

        }
        
        public DateTime TransDate
        {
            get { return _TransDate; }
            set { _TransDate = value; }
        }
        public decimal PurchaseDiscPercentage
        {
            get { return _PurchaseDiscPercentage; }
            set { _PurchaseDiscPercentage = value; }
        }

		public long InsertData()
		{
			long status;
			DalStock dalstock = new DalStock();
			status = 0;
			try
			{
				_UserAction = 1;
				status = dalstock.UpdateStock(this);
				dalstock=null;
			}
			catch (Exception ex)
			{
 
			}
			return status;
		}
		public long UpdateData()
		{
			_UserAction = 2;
			DalStock dalstock = new DalStock();
			long status;
			status = 0;
			try
			{
				status =  dalstock.UpdateStock(this);
				dalstock=null;
			}
			catch (Exception ex)
			{
 
			}
			return status;
		}
        //public string  ImportData()
        //{
        //    _UserAction = 3;
        //    DalStock dalStock = new DalStock();
        //    string Status = "";
        //    try
        //    {
        //        Status = dalStock.ImportData(this);
        //        dalStock = null;
        //    }
        //    catch (Exception ex)
        //    {

        //    }
        //    return Status;

        //}
		public long DeleteData()
		{
			_UserAction =3;
			DalStock dalstock = new DalStock();
			long status;
			status = 0;
			try
			{
				status =  dalstock.DeleteStock(this);
				dalstock=null;
			}
			catch (Exception ex)
			{
 
			}
			return status;
		}
		public void SelectData()
		{
			DalStock dalstock = new DalStock();
			IStock istock;
			istock= (IStock)dalstock.SelectStock(this);
			_FK_Items = istock.FK_Items;
            _FK_Store = istock.FK_Store;
            _FK_RackArrangement = istock.FK_RackArrangement;
            _BatchNo = istock.BatchNo;
            _ExpiryDate = istock.ExpiryDate;
            _PurRate = istock.PurRate;
            _MRP = istock.MRP;
            _OpeningStock = istock.OpeningStock;
            _CurrentStock = istock.CurrentStock;
            _PurchaseTaxAmount = istock.PurchaseTaxAmount;
            _FK_Units = istock.FK_Units;
            _Unit = istock.Unit;
            _NoOfUnits = istock.NoOfUnits;
            _SalePrice = istock.SalePrice;
            _Tostore = istock.Tostore;
            _ActualSalPrice = istock.ActualSalPrice; 

            //_Off_Free = istock.Off_Free;
            //_Off_FreePerUnit = istock.Off_FreePerUnit;
            //_Off_FK_Stock = istock.Off_FK_Stock;
            _FK_Units = istock.FK_Units;
             
            _PurchaseDiscPercentage = istock.PurchaseDiscPercentage;
           

		}
		public DataTable SelectAllData()
		{
			DataTable dtbl = new DataTable();
            DalStock dalstock = new DalStock();
            dtbl = dalstock.SelectAllStock(this);
            dalstock = null;
			return dtbl;
		}

        public DataTable SelectAllStockStockCorrection()
		{
			DataTable dtbl = new DataTable();
            DalStock dalstock = new DalStock();
            dtbl = dalstock.SelectAllStockStockCorrection(this);
            dalstock = null;
			return dtbl;
		}
        public DataTable SelectAllDataForPriceConversion()
        {
            DataTable dtbl = new DataTable();
            DalStock dalstock = new DalStock();
            dtbl = dalstock.SelectAllDataForPriceConversion(this);
            dalstock = null;
            return dtbl;
        }
        public DataTable SelectItemsPurchaseRate()
        {
            DataTable dtbl = new DataTable();
            DalStock dalstock = new DalStock();
            dtbl = dalstock.SelectItemsPurchaseRate(this);
            dalstock = null;
            return dtbl;
        }
        public DataTable SelectStockDatewithPurchase()
        {
            DataTable dtbl = new DataTable();
            DalStock dalstock = new DalStock();
            dtbl = dalstock.SelectStockDatewithPurchase(this);
            dalstock = null;
            return dtbl;
        }
     
	}
    public class ImportStock
    {


    
        private string _Items;

        public string Items
        {
            get { return _Items; }
            set { _Items = value; }
        }
        private decimal _ISalesPrice;

        public decimal ISalesPrice
        {
            get { return _ISalesPrice; }
            set { _ISalesPrice = value; }
        }
        private decimal _IOfferPrice;

        public decimal IOfferPrice
        {
            get { return _IOfferPrice; }
            set { _IOfferPrice = value; }
        }
        private decimal _IWholeSalePrice;

        public decimal IWholeSalePrice
        {
            get { return _IWholeSalePrice; }
            set { _IWholeSalePrice = value; }
        }
        private decimal _IPurchaserate;

        public decimal IPurchaserate
        {
            get { return _IPurchaserate; }
            set { _IPurchaserate = value; }
        }
        private decimal _IOpeningStock;

        public decimal IOpeningStock
        {
            get { return _IOpeningStock; }
            set { _IOpeningStock = value; }
        }
        private decimal _ICurrentStock;

        public decimal ICurrentStock
        {
            get { return _ICurrentStock; }
            set { _ICurrentStock = value; }
        }
        private string _IBatchNo;

        public string IBatchNo
        {
            get { return _IBatchNo; }
            set { _IBatchNo = value; }
        }
        private string _IBarCode;

        public string IBarCode
        {
            get { return _IBarCode; }
            set { _IBarCode = value; }
        }
        private string _IStore;

        public string IStore
        {
            get { return _IStore; }
            set { _IStore = value; }
        }
       

      
        private string _IUnits;

        public string IUnits
        {
            get { return _IUnits; }
            set { _IUnits = value; }
        }

    }


 
}
