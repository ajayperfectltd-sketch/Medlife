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
    public class BlQuotationComparison : IQuotationComparison
    {
        private byte _UserAction;
        private Int16 _PageIndex;
        private Int16 _PageSize;
        private long _FK_Items;
        private string _ItemName;
        private long _Fk_Customer;
        private string _CustomerName;
        private long _Fk_Supplier;
        private string _SupplierName;
        private Int64 _FreeQty;
        private decimal _PurchaseRate;
        private decimal _DiscountAmount;
        private long _FK_Client;
        private Int16 _Category;
        private string _CategoryName;
        private Int16 _Type;
        private string _UserCode;
        private DateTime _EnteredOn;
        private DateTime _FromDate;
        private DateTime _ToDate;

        //private long _MasterID;
        public BlQuotationComparison()
		{
			Initialize();
		}
      public void Initialize()
      {
          _FK_Items = 0;
          _ItemName = string.Empty;
          _Fk_Customer = 0;
          _CustomerName = string.Empty;
          _Fk_Supplier = 0;
          _SupplierName = string.Empty;
          _FreeQty = 0;
          _PurchaseRate = 0;
          _DiscountAmount = 0;
          _FK_Client = 1;
          _Category = 0;
          _CategoryName = string.Empty;
          _Type = 0;
          _PageIndex = 1;
          _PageSize = 10;
          _FromDate = DateTime.Now;
          _ToDate = DateTime.Now;
         // _MasterID = 0;

      }
      public long MasterId
      {
          get { return _FK_Items; }
          set { _FK_Items = value; }
      }
      public byte UserAction
      {
          get { return _UserAction; }
          set { _UserAction = value; }
      }
      public long FK_Items
      {
          get { return _FK_Items; }
          set { _FK_Items = value; }
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
     
      public string  ItemName
      {
          get { return _ItemName; }
          set { _ItemName = value; }
      }
      public long Fk_Customer
      {
          get { return _Fk_Customer; }
          set { _Fk_Customer = value; }
      }
      public string CustomerName
      {
          get { return _CustomerName; }
          set { _CustomerName = value; }
      }
      public Int16 Type
      {
          get { return _Type; }
          set { _Type = value; }
      }
      public long Fk_Supplier
      {
          get { return _Fk_Supplier; }
          set { _Fk_Supplier = value; }
      }
      public string SupplierName
      {
          get { return _SupplierName; }
          set { _SupplierName = value; }
      }
      public long FreeQty
      {
          get { return _FreeQty; }
          set { _FreeQty = value; }
      }
      public decimal PurchaseRate
      {
          get { return _PurchaseRate; }
          set { _PurchaseRate = value; }
      }
      public decimal DiscountAmount
      {
          get { return _DiscountAmount; }
          set { _DiscountAmount = value; }
      }
      public Int16 Category
      {
          get { return _Category; }
          set { _Category = value; }
      }
      public string CategoryName
      {
          get { return _CategoryName; }
          set { _CategoryName = value; }
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
      //public long MasterID
      //{
      //    get { return _MasterID; }
      //    set { _MasterID = value; }
      //}
      public DataTable SelectItemsData()
      {
          DataTable dtbl = new DataTable();
          DalQuotationComparison DalQ = new DalQuotationComparison();
          dtbl = DalQ.ItemsSelectQuotation(this);

          DalQ = null;
          return dtbl;
      }
      public DataTable SelectSupplierHistoryData()
      {
          DataTable dtbl = new DataTable();
          DalQuotationComparison DalQ = new DalQuotationComparison();
          dtbl = DalQ.SelectSupplierHistory(this);

          DalQ = null;
          return dtbl;
      }
      
    }
}
