using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebPharma.DataAccess.WebPharma;
using WebPharma.Interface.WebPharma;

namespace WebPharma.Business.WebPharma
{
    public class BlPriceCorrection : IPriceCorrection
    {
        private byte _UserAction;
        private Int16 _PageIndex;
        private Int16 _PageSize;
        private long _FK_Store;
        private DateTime _PriceCorrectionDate;
        private long _ID_PriceCorrection;
        private string _UserCode;
        private DateTime _EnteredOn;
        private Boolean _Cancelled;
        private string _Cancelleduser;
        private DateTime _CancelledOn;
        private Boolean _Approved;
        private string _ApprovedBy;
        private DateTime _ApprovedOn;
        private long _FK_Reason;
        private string _xmlGriddata;
        private long _FK_Client;
        private string _Remarks;
        private DateTime _FromDate;
        private DateTime _ToDate;
        	public BlPriceCorrection()
		{
			Initialize();
		}
         public void Initialize()
        {
            _ID_PriceCorrection = 0;
            _FK_Store = 0;
            _UserCode = string.Empty;
            _FK_Reason = 0;
            _EnteredOn = DateTime.Now;
            _Cancelled = false;
            _Cancelleduser = string.Empty;
            _CancelledOn = DateTime.Now;
            _FK_Client = 1;
            _Approved = false;
            _ApprovedBy = string.Empty;
            _ApprovedOn = DateTime.Now;
            _xmlGriddata = string.Empty;
            _Remarks = string.Empty;
            _PriceCorrectionDate = DateTime.Now;
            _FromDate = DateTime.Now;
            _ToDate = DateTime.Now;
           
        }
         public string Remarks
         {
             get { return _Remarks; }
             set { _Remarks = value; }
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
             get { return _ID_PriceCorrection; }
             set { _ID_PriceCorrection = value; }
         }
         public long FK_Reason
         {
             get { return _FK_Reason; }
             set { _FK_Reason = value; }
         }
         public long FK_Store
         {
             get { return _FK_Store; }
             set { _FK_Store = value; }
         }
         public DateTime PriceCorrectionDate
         {
             get { return _PriceCorrectionDate; }
             set { _PriceCorrectionDate = value; }
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
         public string xmlGriddata
         {
             get { return _xmlGriddata; }
             set { _xmlGriddata = value; }
         }
         public string UserCode
         {
             get { return _UserCode; }
             set { _UserCode = value; }
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
             DalPriceCorrection dalPrice = new DalPriceCorrection();
             status = 0;
             try
             {
                 _UserAction = 1;
                 status = dalPrice.UpdatepriceCorrection(this);
                 dalPrice = null;
             }
             catch (Exception ex)
             {

             }
             return status;
         }
         public long UpdateData()
         {
             _UserAction = 2;
             DalPriceCorrection dalPrice = new DalPriceCorrection();
             long status;
             status = 0;
             try
             {
                 status = dalPrice.UpdatepriceCorrection(this);
                 dalPrice = null;
             }
             catch (Exception ex)
             {

             }
             return status;
         }
         public DataTable SelectAllData()
         {
             DataTable dtbl = new DataTable();
             DalPriceCorrection dalPrice = new DalPriceCorrection();
             dtbl = dalPrice.SelectPriceCorrectionData(this);
             dalPrice = null;
             return dtbl;
         }
         public void SelectData()
         {
             DalPriceCorrection DalPrice = new DalPriceCorrection();
             IPriceCorrection IPrice;
             IPrice = (IPriceCorrection)DalPrice.SelectMasterData(this);
             _FK_Store = IPrice.FK_Store;
             _UserCode = IPrice.UserCode;
             _PriceCorrectionDate = IPrice.PriceCorrectionDate;
             DalPrice = null;
         }
         public DataSet SelectdetailsGridData()
         {
             DataSet dtbl = new DataSet();
             DalPriceCorrection DalPrice = new DalPriceCorrection();
             dtbl = DalPrice.SelectPriceCorrectionDetailsGrid(this);
             DalPrice = null;
             return dtbl;
         }
         public long DeletePriceCorrection()
         {
             long status = 0;
             DalPriceCorrection dalPrice = new DalPriceCorrection();
             try
             {


                 status = dalPrice.DeletePriceCorrection(this);
                 dalPrice = null;
             }
             catch (Exception ex)
             {
             }
             return status;


         }


    

    }

    public class IPricCrtionDetails
    {
        private Int64 _FK_Category;
        private Int64 _FK_Item;
        private Int64 _SlNo;
        private Decimal _Quantity;
        private Decimal _MRP;
        private Decimal _NewMRP;
        private Decimal _SalesPrice;
        private Decimal _NewSalesPrice;
        private Int64 _FK_Stock;
        public IPricCrtionDetails()
       {
           Initialize();
       }
        public void Initialize()
        {
            _FK_Category = 0;
            _FK_Item = 0;
            _SlNo = 0;
            _FK_Stock = 0;
            _SalesPrice = 0;
            _NewSalesPrice = 0;
            _Quantity = 0;
            _NewMRP = 0;
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
         public decimal NewSalesPrice
         {
             get { return _NewSalesPrice; }
             set { _NewSalesPrice = value; }
         }
         public decimal NewMRP
         {
             get { return _NewMRP; }
             set { _NewMRP = value; }
         }
         public decimal MRP
         {
             get { return _MRP; }
             set { _MRP = value; }
         }

    }
}
