using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebPharma.DataAccess.Masters;
using WebPharma.Interface.Masters;

namespace WebPharma.Business.Masters
{
    public class BlItemRearrange : IItemRearrange
    {
        private byte _UserAction;
        private Int16 _PageIndex;
        private Int16 _PageSize;
        private long _ID_categoryItems;
        private string _Name;
        private long _FK_Newcategory;
        private long _FK_NewSubCategory;
        private long _FK_Category;
        private long _FK_SubCategory;
        private DateTime _EnteredOn;
        private long _FK_Client;
        private string _UserCode;
        private string _XmlItems;
        private DateTime _groupingdate;
        private bool _Checked;
        
        private long _FK_Manufacturer;
        private long _FK_Bin;
     
     
        private long _FK_Rack;
        private long _FK_Unit;
        private long _FK_Generic;
        private long _FK_PriceGroup;
        private long _FK_QuantityGroup;
        private string _Barcode;
        private bool _StopSale;
        private bool _Packed;
         
   

        private long _ChangeTypeID;
        private long _DataID;
        private long _SubDataID;
        private string _Reason;

        public BlItemRearrange()
        {
            Initialize();
        }
        public void Initialize()
        {


            _Name = string.Empty;
            _ID_categoryItems = 0;
            _UserAction = 1;
            _FK_Newcategory = 0;
            _FK_NewSubCategory = 0;
            _FK_Category = 0;
            _FK_SubCategory = 0;
            _Checked = true;
            _PageIndex = 1;
            _PageSize = 15;
            _FK_Client = 1;
            _EnteredOn = DateTime.Now;
            _UserCode = string.Empty;
            _XmlItems = string.Empty;
            _groupingdate = DateTime.Now;
            _FK_Unit = 0;
            _FK_Generic = 0;
          
            _FK_Manufacturer = 0;
            _FK_Bin = 0;
            
            _FK_Rack = 0;
            _FK_PriceGroup = 0;
            _FK_QuantityGroup = 0;
            _Barcode = string.Empty;
            _StopSale = false;
            _Packed = false;
            
           
            _ChangeTypeID = 0;
            _DataID = 0;
            _SubDataID = 0;
            _Reason = string.Empty;
        }
        public bool Checked
        {
            get { return _Checked; }
            set { _Checked = value; }
        }
        public long ChangeTypeID
        {
            get { return _ChangeTypeID; }
            set { _ChangeTypeID = value; }
        }
        public long FK_Unit
        {
            get { return _FK_Unit; }
            set { _FK_Unit = value; }
        }
        public long FK_Generic
        {
            get { return _FK_Generic; }
            set { _FK_Generic = value; }
        }
        public long DataID
        {
            get { return _DataID; }
            set { _DataID = value; }
        }
        public long SubDataID
        {
            get { return _SubDataID; }
            set { _SubDataID = value; }
        }
        public string Reason
        {
            get { return _Reason; }
            set { _Reason = value; }
        }



      
        public string Barcode
        {
            get { return _Barcode; }
            set { _Barcode = value; }
        }
        public bool StopSale
        {
            get { return _StopSale; }
            set { _StopSale = value; }
        }
        public bool Packed
        {
            get { return _Packed; }
            set { _Packed = value; }
        }
        
        public DateTime groupingdate
        {
            get { return _groupingdate; }
            set { _groupingdate = value; }
        }
        public string XmlItems
        {
            get { return _XmlItems; }
            set { _XmlItems = value; }
        }
        public long MasterId
        {
            get { return _ID_categoryItems; }
            set { _ID_categoryItems = value; }
        }
        public string UserCode
        {
            get { return _UserCode; }
            set { _UserCode = value; }
        }
        public Int16 PageIndex
        {
            get { return _PageIndex; }
            set { _PageIndex = value; }
        }
        public long FK_Client
        {
            get { return _FK_Client; }
            set { _FK_Client = value; }
        }
        public Int16 PageSize
        {
            get { return _PageSize; }
            set { _PageSize = value; }
        }
        public string Name
        {
            get { return _Name; }
            set { _Name = value; }
        }

        public long FK_Newcategory
        {
            get { return _FK_Newcategory; }
            set { _FK_Newcategory = value; }
        }
        public long FK_NewSubCategory
        {
            get { return _FK_NewSubCategory; }
            set { _FK_NewSubCategory = value; }
        }
        public byte UserAction
        {
            get { return _UserAction; }
            set { _UserAction = value; }
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
        public DateTime EnteredOn
        {
            get { return _EnteredOn; }
            set { _EnteredOn = value; }
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

        public long FK_PriceGroup
        {
            get { return _FK_PriceGroup; }
            set { _FK_PriceGroup = value; }
        }

        public long FK_QuantityGroup
        {
            get { return _FK_QuantityGroup; }
            set { _FK_QuantityGroup = value; }
        }

        public long InsertData()
        {
            long status;
            DalItemRearrange dalRearrange = new DalItemRearrange();
            status = 0;
            try
            {
                _UserAction = 1;
                status = dalRearrange.UpdateItemRearrange(this);
                dalRearrange = null;
            }
            catch (Exception ex)
            {

            }
            return status;
        }
        public DataTable SelectData()
        {
            DataTable dtbl = new DataTable();
            DalItemRearrange dalRearrange = new DalItemRearrange();
            dtbl = dalRearrange.SelectData(this);
            dalRearrange = null;
            return dtbl;
        }
        public class Items
        {

            private Int64 _FK_Items;
            public Items()
            {
                Initialize();
            }
            public void Initialize()
            {

                _FK_Items = 0;

            }


            public Int64 FK_Items
            {
                get { return _FK_Items; }
                set { _FK_Items = value; }
            }




        }

    }
}
