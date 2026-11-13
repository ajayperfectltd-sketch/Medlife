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
using WebPharma.Interface.WebPharma;
using WebPharma.DataAccess.WebPharma;
using System.Data;
namespace WebPharma.Business.WebPharma
{

    public class BlStockAdjustment : IStockAdjustment
    {
        private byte _UserAction;
        private Int16 _PageIndex;
        private Int16 _PageSize;
        private long _FK_Client;
        private long _ID_StockAdjustment;
        private long _FK_Stock;
        private string _Module;
        private long _FK_Module;
        private long _FK_StockTo;
        private long _FK_Item;
        private decimal _Quantity;
        private long _FK_Store;
        private DateTime _EnteredOn;
        private Boolean _Approved;
        private string _ApprovedBy;
        private DateTime _ApprovedOn;
        private DateTime _TransDate;
        private DateTime _FromDate;
        private DateTime _ToDate;
        private string _UserCode;
        private long _FK_Category;
        private string _XmlStockAdjustmentItems;
        private long _AdjustmentID;
        private string _Name;

      

        public BlStockAdjustment()
        {
            Initialize();
        }
        public void Initialize()
        {

            _Module = string.Empty;
            _FK_Module = 0;
            _FK_StockTo = 0;
            _FK_Stock = 0;
            _FK_Item = 0;
            _Quantity = 0;
            _FK_Store = 0;
            _EnteredOn = DateTime.Now;
            _Approved = false;
            _ApprovedBy = string.Empty;
            _ApprovedOn = DateTime.Now;
            _FK_Client = 1;
            _FromDate = DateTime.Now;
            _ToDate = DateTime.Now;
            _TransDate = DateTime.Now;
            _XmlStockAdjustmentItems = string.Empty;
            PageIndex = 1;
            PageSize = 10;
            _AdjustmentID = 0;
            _Name = string.Empty;
           
        }
        public string Name
        {
            get { return _Name; }
            set { _Name = value; }
        }

        public long FK_Category
        {
            get { return _FK_Category; }
            set { _FK_Category = value; }
        }
        public long FK_Stock
        {
            get { return _FK_Stock; }
            set { _FK_Stock = value; }
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
            get { return _ID_StockAdjustment; }
            set { _ID_StockAdjustment = value; }
        }
        public DateTime TransDate
        {
            get { return _TransDate; }
            set { _TransDate = value; }
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
        public Boolean Approved
        {
            get { return _Approved; }
            set { _Approved = value; }
        }
        public string ApprovedBy
        {
            get { return _ApprovedBy; }
            set { _ApprovedBy = value; }
        }
        public DateTime ApprovedOn
        {
            get { return _ApprovedOn; }
            set { _ApprovedOn = value; }
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
        public string Module
        {
            get { return _Module; }
            set { _Module = value; }
        }
        public long FK_Module
        {
            get { return _FK_Module; }
            set { _FK_Module = value; }
        }
        public long FK_StockTo
        {
            get { return _FK_StockTo; }
            set { _FK_StockTo = value; }
        }
        public long FK_Item
        {
            get { return _FK_Item; }
            set { _FK_Item = value; }
        }
        public decimal Quantity
        {
            get { return _Quantity; }
            set { _Quantity = value; }
        }
        public long FK_Store
        {
            get { return _FK_Store; }
            set { _FK_Store = value; }
        }
        public string XmlStockAdjustmentItems
        {
            get { return _XmlStockAdjustmentItems; }
            set { _XmlStockAdjustmentItems = value; }
        }
        public long AdjustmentID
        {
            get { return _AdjustmentID; }
            set { _AdjustmentID = value; }
        }

        public long InsertData()
        {
            long status = 0;
            DalStockAdjustment dalSA = new DalStockAdjustment();
            try
            {
                UserAction = 1;
                status = dalSA.UpdateStockAdjustment(this);
                dalSA = null;
            }
            catch (Exception ex)
            {
            }
            return status;
        }

        public long UpdateData()
        {
            long status = 0;
            DalStockAdjustment dalSA = new DalStockAdjustment();
            try
            {
                UserAction = 2;
                status = dalSA.UpdateStockAdjustment(this);
                dalSA = null;
            }
            catch (Exception ex)
            {
            }
            return status;

        }




        //public void SelectData()
        //{
        //    DalItemConversion dalIC = new DalItemConversion();
        //    IItemConversion iItemConversion;
        //    iItemConversion = (IItemConversion)dalIC.SelectItemConversion(this);
        //    _FK_StoreFrom = iItemConversion.FK_StoreFrom;
        //    _FK_StoreTo = iItemConversion.FK_StoreTo;
        //    _TransDate = iItemConversion.TransDate;
        //    _FromAmount = iItemConversion.FromAmount;
        //    _ToAmount = iItemConversion.ToAmount; ;
        //    _Remarks = iItemConversion.Remarks;
        //    _Approved = iItemConversion.Approved;
        //    dalIC = null;

        //}
        public DataTable SelectAllData()
        {
            DataTable dtbl = new DataTable();
            DalStockAdjustment dalSA = new DalStockAdjustment();
            dtbl = dalSA.SelectAllStockAdjustment(this);
            dalSA = null;
            return dtbl;
        }

        public DataTable SelectStockAdjustmentItem()
        {
            DataTable dtbl = new DataTable();
            DalStockAdjustment dalSA = new DalStockAdjustment();
            dtbl = dalSA.SelectStockAdjustmentItem(this);
            dalSA = null;
            return dtbl;
        }




    }
    public class StockAdjustmentItems
    {
        private Int64 _ID_StockAdjustment;
        private Int64 _FK_StockFrom;
        private Int64 _FK_StockTo;

 
        private Int64 _FK_Module;
        private Int64 _FK_StoreTo;      
        private Int64 _FK_StoreFrom;     
        private decimal _Quantity;
        private decimal _SalesPriceFrom;
        

     
        private decimal _AdjustmentQuantity;
        private string _Module;
        private Int64 _FK_Details;
        
        public Int64 FK_Details
        {
            get { return _FK_Details; }
            set { _FK_Details = value; }
        }
        public string Module
        {
            get { return _Module; }
            set { _Module = value; }
        }
        public Int64 FK_StockFrom
        {
            get { return _FK_StockFrom; }
            set { _FK_StockFrom = value; }
        }
        public Int64 FK_StockTo
        {
            get { return _FK_StockTo; }
            set { _FK_StockTo = value; }
        }
        public decimal Quantity
        {
            get { return _Quantity; }
            set { _Quantity = value; }
        }
        public Int64 FK_StoreFrom
        {
            get { return _FK_StoreFrom; }
            set { _FK_StoreFrom = value; }
        }
        public Int64 FK_StoreTo
        {
            get { return _FK_StoreTo; }
            set { _FK_StoreTo = value; }
        }
        public StockAdjustmentItems()
        {
            Initialize();
        }
        public void Initialize()
        {
           // _FK_Stock = 0;
            _Quantity = 0;
            _ID_StockAdjustment = 0;
            _SalesPriceFrom = 0;
          
            _AdjustmentQuantity = 0;
            _Module = string.Empty;
            _FK_StoreFrom = 0;
            _FK_StoreTo = 0;

            _FK_StockFrom = 0;
            _FK_StockTo = 0;

        }
        public Int64 ID_StockAdjustment
        {
            get { return _ID_StockAdjustment; }
            set { _ID_StockAdjustment = value; }
        }
        public Int64 FK_Module
        {
            get { return _FK_Module; }
            set { _FK_Module = value; }
        }

        public decimal SalesPriceFrom
        {
            get { return _SalesPriceFrom; }
            set { _SalesPriceFrom = value; }
        }
      
        public decimal AdjustmentQuantity
        {
            get { return _AdjustmentQuantity; }
            set { _AdjustmentQuantity = value; }
        }
    }

}



