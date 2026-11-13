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
    public class BlQuotation : IQuotation
    {
        private byte _UserAction;
        private Int16 _PageIndex;
        private Int16 _PageSize;
        private long _ID_Quatation;
        private Int16 _Type;
        private long _FK_Master;
        private DateTime _EffectFrom;
        private DateTime _EffectTo;
        private string  _UserCode;
        private DateTime _EnteredOn;
        private long _FK_Client;
        private string _XmlDetails;
        private string _Description;
        private long _FK_Reason;
        private string _Name;
        public BlQuotation()
		{
			Initialize();
		}
        public void Initialize()
        {

            _ID_Quatation = 0;
            _Type = 0;
            _FK_Master = 0;
            _EffectFrom=DateTime.Now;
            _EffectTo = DateTime.Now;
            _UserCode = string.Empty;
            _EnteredOn = DateTime.Now;
            _FK_Client = 1;
            _PageIndex = 1;
            _PageSize = 100;
            _UserAction = 1;
            _XmlDetails = string.Empty;
            _Description = string.Empty;
            _Name = string.Empty;
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
            get { return _ID_Quatation; }
            set { _ID_Quatation = value; }
        }
		
        public long FK_Reason
        {
            get { return _FK_Reason; }
            set { _FK_Reason = value; }
        }
        public long FK_Client
        {
            get { return _FK_Client; }
            set { _FK_Client = value; }
        }
        public DateTime EnteredOn
        {
            get { return _EnteredOn; }
            set { _EnteredOn = value; }
        }
        public Int16 Type
        {
            get { return _Type; }
            set { _Type = value; }
        }
   
        public string XmlDetails
        {
            get { return _XmlDetails; }
            set { _XmlDetails = value; }
        }
        public string Description
        {
            get { return _Description; }
            set { _Description = value; }
        }
        public DateTime EffectFrom
        {
            get { return _EffectFrom; }
            set { _EffectFrom = value; }
        }
        public DateTime EffectTo
        {
            get { return _EffectTo; }
            set { _EffectTo = value; }
        }
        public long FK_Master
        {
            get { return _FK_Master; }
            set { _FK_Master = value; }
        }
        public string  UserCode
        {
            get { return _UserCode; }
            set { _UserCode = value; }
        }
        public string Name
        {
            get { return _Name; }
            set { _Name = value; }
        }
        public long UpdateData()
        {
            _UserAction = 2;
            DalQuotation dalQuotation = new DalQuotation();
            long status;
            status = 0;
            try
            {
                status = dalQuotation.UpdateQuotation(this);
                dalQuotation = null;
            }
            catch (Exception ex)
            {

            }
            return status;
        }

        public long DeleteData()
        {
            _UserAction = 3;
            DalQuotation dalQuotation = new DalQuotation();
            long status;
            status = 0;
            try
            {
                status = dalQuotation.DeleteQuataion(this);
                dalQuotation = null;
            }
            catch (Exception ex)
            {

            }
            return status;
        }
        public long InsertData()
        {
            long status;
            DalQuotation dalQuotation = new DalQuotation();
            status = 0;
            try
            {
                _UserAction = 1;
                status = dalQuotation.UpdateQuotation(this);
                dalQuotation = null;
            }
            catch (Exception ex)
            {

            }
            return status;
        }
      
        
  
        public DataTable SelectAllData()
        {
            DataTable dtbl = new DataTable();
            DalQuotation dalQuotation = new DalQuotation();
            dtbl = dalQuotation.SelectAllIQuotation(this);
            dalQuotation = null;
            return dtbl;

        }
        public void SelectData()
        {
            DalQuotation dalQuotation = new DalQuotation();
            IQuotation IQuotation;
            IQuotation = (IQuotation)dalQuotation.Selectdata(this);
            _Name = IQuotation.Name;
            _EffectFrom = IQuotation.EffectFrom;
            _EffectTo = IQuotation.EffectTo;

        }
        public DataTable SelectQuatationDetails()
        {
            DataTable dtbl = new DataTable();
            DalQuotation dalquotation = new DalQuotation();
            dtbl = dalquotation.SelectQuotationDetails(this);
            dalquotation = null;
            return dtbl;
        }
    }
       public class IItemsDetail
    {
       
        private Int64 _FK_Items;
        private Int64 _FK_Units;
        private Int64 _FK_SubUnits;
        private decimal _Rate;
        private decimal _Percentage;
        private decimal _FreeQuantity;
        private decimal _FreeQuantityPer;
        private Boolean _Frequent;
        private Boolean _PercentageWise;
        private decimal _Discount;
        private decimal _DiscountPer;
        private Boolean _FrequentPer;
        private Boolean _SupplierOnly;


        public Int64 FK_Items
        {
            get { return _FK_Items; }
            set { _FK_Items = value; }
        }
        public Int64 FK_Units
        {
            get { return _FK_Units; }
            set { _FK_Units = value; }
        }
        public Int64 FK_SubUnits
        {
            get { return _FK_SubUnits; }
            set { _FK_SubUnits = value; }
        }
        public decimal Rate
        {
            get { return _Rate; }
            set { _Rate = value; }
        }
        public decimal Percentage
        {
            get { return _Percentage; }
            set { _Percentage = value; }
        }
        public decimal FreeQuantity
        {
            get { return _FreeQuantity; }
            set { _FreeQuantity = value; }
        }
        public decimal FreeQuantityPer
        {
            get { return _FreeQuantityPer; }
            set { _FreeQuantityPer = value; }
        }
        public Boolean Frequent
        {
            get { return _Frequent; }
            set { _Frequent = value; }
        }
        public Boolean SupplierOnly
        {
            get { return _SupplierOnly; }
            set { _SupplierOnly = value; }
        }
       
        public Boolean PercentageWise
        {
            get { return _PercentageWise; }
            set { _PercentageWise = value; }
        }
        public decimal Discount
        {
            get { return _Discount; }
            set { _Discount = value; }
        }
        public decimal DiscountPer
        {
            get { return _DiscountPer; }
            set { _DiscountPer = value; }
        }
        public Boolean FrequentPer
        {
            get { return _FrequentPer; }
            set { _FrequentPer = value; }
        }
     }
}
