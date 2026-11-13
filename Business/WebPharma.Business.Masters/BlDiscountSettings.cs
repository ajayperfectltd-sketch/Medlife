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
	public class BlDiscountSettings : IDiscountSettings
	{
 
		private byte _UserAction;
		private Int16 _PageIndex;
		private Int16 _PageSize; 
		private long _ID_DiscountSettings;
		private long _FK_Discount;
		private byte _DiscountType;
		private long _FK_Employee;
		private long _FK_Customer;
		private long _FK_Items;
		private long _StockCode;
		private DateTime _FromDate;
		private DateTime _ToDate;
		private Boolean _Closed;
		private decimal _Percentage;
		private decimal _Amount;
		private string _UserCode;
		private DateTime _EnteredOn;
		private long _FK_Client;
        private long _FK_SalesMan;
        private DateTime _Date;
        private string _VoucherNo;
        private string _DisName;
        private long _FK_CustomerGroup;
        private Int64 _FK_CategoryFirst;
        private Int64 _FK_Brand;

        public long FK_CustomerGroup
        {
            get { return _FK_CustomerGroup; }
            set { _FK_CustomerGroup = value; }
        }
       
        public string VoucherNo
        {
            get { return _VoucherNo; }
            set { _VoucherNo = value; }
        }
        public long FK_SalesMan
        {
            get { return _FK_SalesMan; }
            set { _FK_SalesMan = value; }
        }
        public Int64 FK_CategoryFirst
        {
            get { return _FK_CategoryFirst; }
            set { _FK_CategoryFirst = value; }
        }
        public Int64 FK_Brand
        {
            get { return _FK_Brand; }
            set { _FK_Brand = value; }
        }
 
		public BlDiscountSettings()
		{
			Initialize();
		}
		public void Initialize()
		{
			_ID_DiscountSettings = 0;
			_FK_Discount = 0;
			_DiscountType = 0;
			_FK_Employee = 0;
			_FK_Customer = 0;
			_FK_Items = 0;
			_StockCode = 0;
			_FromDate = DateTime.Now;
			_ToDate = DateTime.Now;
            _Date = DateTime.Now;
			_Closed = false;
			_Percentage = 0;
			_Amount = 0;
			_UserCode = string.Empty;
			_EnteredOn = DateTime.Now;
            _FK_SalesMan = 0;
            _FK_Client = 1;
            _PageIndex = 1;
            _PageSize = 10;
            _DisName = string.Empty;
            _UserAction = 1;

            _FK_CategoryFirst = 0;
            _FK_Brand = 0;
           
		}

        public DateTime Date
        {
            get { return _Date; }
            set { _Date = value; }
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
		public Int64 MasterId
		{
			get { return _ID_DiscountSettings; }
			set { _ID_DiscountSettings = value; }
		}
		
		public long FK_Discount
		{
			get { return _FK_Discount; }
			set { _FK_Discount = value; }
		}
		public byte DiscountType
		{
			get { return _DiscountType; }
			set { _DiscountType = value; }
		}
		public long FK_Employee
		{
			get { return _FK_Employee; }
			set { _FK_Employee = value; }
		}
		public long FK_Customer
		{
			get { return _FK_Customer; }
			set { _FK_Customer = value; }
		}
		public long FK_Items
		{
			get { return _FK_Items; }
			set { _FK_Items = value; }
		}
		public long StockCode
		{
			get { return _StockCode; }
			set { _StockCode = value; }
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
		public Boolean Closed
		{
			get { return _Closed; }
			set { _Closed = value; }
		}
		public decimal Percentage
		{
			get { return _Percentage; }
			set { _Percentage = value; }
		}
		public decimal Amount
		{
			get { return _Amount; }
			set { _Amount = value; }
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
        public string DisName
        {
            get { return _DisName; }
            set { _DisName = value; }
        }
		public long InsertData()
		{
			long status;
			DalDiscountSettings daldiscountsettings = new DalDiscountSettings();
			status = 0;
			try
			{
				_UserAction = 1;
				status = daldiscountsettings.UpdateDiscountSettings(this);
				daldiscountsettings=null;
			}
			catch (Exception ex)
			{
 
			}
			return status;
		}
 
		public long UpdateData()
		{
			_UserAction = 2;
			DalDiscountSettings daldiscountsettings = new DalDiscountSettings();
			long status;
			status = 0;
			try
			{
				status =  daldiscountsettings.UpdateDiscountSettings(this);
				daldiscountsettings=null;
			}
			catch (Exception ex)
			{
 
			}
			return status;
		}
 
		public long DeleteData()
		{
			_UserAction =3;
			DalDiscountSettings daldiscountsettings = new DalDiscountSettings();
			long status;
			status = 0;
			try
			{
				status =  daldiscountsettings.DeleteDiscountSettings(this);
				daldiscountsettings=null;
			}
			catch (Exception ex)
			{
 
			}
			return status;
		}
 
		public void SelectData()
		{
			DalDiscountSettings daldiscountsettings = new DalDiscountSettings();
			IDiscountSettings idiscountsettings;
			idiscountsettings= (IDiscountSettings)daldiscountsettings.SelectDiscountSettings(this);
			_FK_Discount = idiscountsettings.FK_Discount;
			_DiscountType = idiscountsettings.DiscountType;
			_FK_Employee = idiscountsettings.FK_Employee;
			_FK_Customer = idiscountsettings.FK_Customer;
			
			_StockCode = idiscountsettings.StockCode;
			_FromDate = idiscountsettings.FromDate;
			_ToDate = idiscountsettings.ToDate;
			_Closed = idiscountsettings.Closed;
			_Percentage = idiscountsettings.Percentage;
			_Amount = idiscountsettings.Amount;
            _DisName = idiscountsettings.DisName;
            _FK_Items = idiscountsettings.FK_Items;
            _FK_CategoryFirst = idiscountsettings.FK_CategoryFirst;
            _FK_Brand = idiscountsettings.FK_Brand;
		}
 
		public DataTable SelectAllData()
		{
			DataTable dtbl = new DataTable();
			DalDiscountSettings dalDiscountSettings = new DalDiscountSettings();
			dtbl = dalDiscountSettings.SelectAllDiscountSettings(this);
			dalDiscountSettings = null;
			return dtbl;
		}
        public DataTable SelectDiscountSettingsItems()
        {
            DataTable dtbl = new DataTable();
            DalDiscountSettings dalDiscountSettings = new DalDiscountSettings();
            dtbl = dalDiscountSettings.SelectDiscountSettingsItems(this);
            dalDiscountSettings = null;
            return dtbl;
        }
        public DataTable SelectDiscountSettingsCustomer()
        {
            DataTable dtbl = new DataTable();
            DalDiscountSettings dalDiscountSettings = new DalDiscountSettings();
            dtbl = dalDiscountSettings.SelectDiscountSettingsCustomer(this);
            dalDiscountSettings = null;
            return dtbl;
        }

        public DataTable SelectDiscountSettingsCustomerGroup()
        {
            DataTable dtbl = new DataTable();
            DalDiscountSettings dalDiscountSettings = new DalDiscountSettings();
            dtbl = dalDiscountSettings.SelectDiscountSettingsCustomerGroup(this);
            dalDiscountSettings = null;
            return dtbl;
        }
        public DataTable SelectDiscountSettingsSalesMan()
        {
            DataTable dtbl = new DataTable();
            DalDiscountSettings dalDiscountSettings = new DalDiscountSettings();
            dtbl = dalDiscountSettings.SelectDiscountSettingsSalesMan(this);
            dalDiscountSettings = null;
            return dtbl;
        }
        public DataTable SelectDiscountSettingsEmployee()
        {
            DataTable dtbl = new DataTable();
            DalDiscountSettings dalDiscountSettings = new DalDiscountSettings();
            dtbl = dalDiscountSettings.SelectDiscountSettingsEmployee(this);
            dalDiscountSettings = null;
            return dtbl;
        }
        public DataTable SelectVoucherDiscount()
        {
            DataTable dtbl = new DataTable();
            DalDiscountSettings dalDiscountSettings = new DalDiscountSettings();
            dtbl = dalDiscountSettings.SelectVoucherDiscount(this);
            dalDiscountSettings = null;
            return dtbl;
        }
        public DataTable SelectVoucherDiscountGift()
        {
            DataTable dtbl = new DataTable();
            DalDiscountSettings dalDiscountSettings = new DalDiscountSettings();
            dtbl = dalDiscountSettings.SelectVoucherDiscountGift(this);
            dalDiscountSettings = null;
            return dtbl;
        }
       


	}
}
