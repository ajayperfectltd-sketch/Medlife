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
    public class BlItemEnquiry : IItemEnquiry
    {
      private byte _UserAction;
      private Int16 _PageIndex;
      private Int16 _PageSize;
      private long _ID_Items;
      private string _Name;
      private long _FK_Category;
      private long _FK_SubCategory;
      private long _FK_Units;
      private long _FK_SubUnit;
      private long _FK_Manufacturer;
      private long _FK_Generic;
      private long _FK_Rack;
      private long _FK_Bin;
      private long _FK_Store;
      private long _FK_Supplier;
      private string _BatchNo;
      private long _FK_Schedule;
      private decimal _MRP;
      private decimal _Salesprice;
      private decimal _PurchaseRate;
      private string _UserCode;
      private DateTime _EnteredOn;
      private long _FK_Client;
      private string _Criteria;
      private long _Fk_Customer;
      private string _CustomerName;
      private long _Fk_Doctor;
      private string _DoctorName;
      private string _CriteriaMRP;
      private string _CriteriaPurchase;
      private string _CreteriaSales;
      private bool _Zerostock;
      private int _Level;


      public BlItemEnquiry()
		{
			Initialize();
		}
      public void Initialize()
      {
          _Name = string.Empty;
          _FK_Category = 0;
          _FK_SubCategory = 0;
          _FK_Units = 0;
          _FK_SubUnit = 0;
          _FK_Manufacturer = 0;
          _FK_Generic = 0;
          _FK_Rack = 0;
          _FK_Bin = 0;
          _FK_Store = 0;
          _FK_Supplier = 0;
          _FK_Schedule = 0;
          _MRP = 0;
          _PurchaseRate = 0;
          _Salesprice = 0;
          _BatchNo = string.Empty;
          _UserCode = string.Empty;
          _EnteredOn = DateTime.Now;
          _FK_Client = 1;
          _Criteria = string.Empty;
          _PageIndex = 1;
          _PageSize = 10;
          _ID_Items = 0;
          _Fk_Customer = 0;
          _CustomerName = string.Empty;
          _Fk_Doctor = 0;
          _DoctorName = string.Empty;
          _CreteriaSales = string.Empty;
          _CriteriaMRP = string.Empty;
          _CriteriaPurchase = string.Empty;
          _Zerostock = false;
          _Level = 0;
      }
      public bool Zerostock
      {
          get { return _Zerostock; }
          set { _Zerostock = value; }
      }
      public string CriteriaMRP
      {
          get { return _CriteriaMRP; }
          set { _CriteriaMRP = value; }
      }
      public string CreteriaSales
      {
          get { return _CreteriaSales; }
          set { _CreteriaSales = value; }
      }
      public string CriteriaPurchase
      {
          get { return _CriteriaPurchase; }
          set { _CriteriaPurchase = value; }
      }

         public string BatchNo
        {
            get { return _BatchNo; }
            set { _BatchNo = value; }
        }
         public string CustomerName
         {
             get { return _CustomerName; }
             set { _CustomerName = value; }
         }
         public string DoctorName
         {
             get { return _DoctorName; }
             set { _DoctorName = value; }
         }
         public long Fk_Doctor
         {
             get { return _Fk_Doctor; }
             set { _Fk_Doctor = value; }
         }
         public long Fk_Customer
         {
             get { return _Fk_Customer; }
             set { _Fk_Customer = value; }
         }
        
         public string Criteria
        {
            get { return _Criteria; }
            set { _Criteria = value; }
        }
        public decimal Salesprice
        {
            get { return _Salesprice; }
            set { _Salesprice = value; }
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
        public byte UserAction
        {
            get { return _UserAction; }
            set { _UserAction = value; }
        }
        public string Name
        {
            get { return _Name; }
            set { _Name = value; }
        }
        public long MasterId
        {
            get { return _ID_Items; }
            set { _ID_Items = value; }
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
        public long FK_Category
        {
            get { return _FK_Category; }
            set { _FK_Category = value; }
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
        public long FK_SubUnit
        {
            get { return _FK_SubUnit; }
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
        public long FK_Schedule
        {
            get { return _FK_Schedule; }
            set { _FK_Schedule = value; }
        }
      
        public decimal MRP
        {
            get { return _MRP; }
            set { _MRP = value; }
        }
        public decimal PurchaseRate
        {
            get { return _PurchaseRate; }
            set { _PurchaseRate = value; }
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
        public int Level
        {
            get { return _Level; }
            set { _Level = value; }
        }
        public DataTable SelectItemsData()
        {
            DataTable dtbl = new DataTable();
            DalItemEnquiry dalitem = new DalItemEnquiry();
            dtbl = dalitem.ItemsSelectEnquiry(this);

            dalitem = null;
            return dtbl;
        }
 
      
    }
}
