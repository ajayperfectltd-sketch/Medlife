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
	public class BlCustomer:ICustomer
	{
       
		private byte _UserAction;
		private Int16 _PageIndex;
		private Int16 _PageSize; 
		private long _ID_Customer;
		private string _Name;
		private string _ShortName;
        private string _Area;
        private string _Place;
        private string _Post;
        private string _Pin;
        private string _LandMark;
		private string _Phone;
		private string _Mobile;
		private string _Email;
		private DateTime _DateOfBirth;
        private long _FK_Country;
        private long _FK_State;
        private long _FK_District;
		private Boolean _Active;
        private string _InactiveRemarks;
        private long _FK_CustomerGroup;
        private long _FK_AccountHeadSub;
        private string _UserCode;
        private DateTime _EnteredOn;
        private Boolean _IsEmployee;  
        private long _CreditDays;
        private decimal _CreditLimit;
        private long _FK_AccountHead;
        private string _CustomerCode;
        private Boolean _CreditSale;
        private Boolean _CardPrice;
        private string _Attachment;
        private decimal _DiscountPercentage;
        private string _GSTTIN;
        private string _CardNumber;
        private string _AccountHead;
        private byte[] _AttachmentData;
        private Boolean _DuplicateCard;
        private Int64 _Idtype;
        private long _FK_Store;
        private Int16 _Status;
        private long _FK_Reason;
        private long _FK_Client;
     

		public BlCustomer()
		{
			Initialize();
		}
		public void Initialize()
		{
			_ID_Customer = 0;
			_Name = string.Empty;
			_ShortName = string.Empty;
            _Area = string.Empty;
            _Place=string.Empty;
            _Post=string.Empty;
            _Pin = string.Empty;
            _LandMark = string.Empty;
			_Phone = string.Empty;
			_Mobile = string.Empty;
			_Email = string.Empty;
			_DateOfBirth = DateTime.Now;
            _FK_Country = 0;
            _FK_State = 0;
            _FK_District = 0;
			_Active = false;
            _IsEmployee = false;
			_InactiveRemarks = string.Empty;
			_FK_CustomerGroup = 0;
			_FK_AccountHeadSub = 0;
            _UserCode = string.Empty;
            _EnteredOn = DateTime.Now;
			_FK_Client = 1;
			_UserAction=1;
            _PageIndex = 1;
            _PageSize = 10;
            _CreditDays = 0;
            _CreditLimit = 0;
            _FK_AccountHead = 0; 
            _AccountHead = "";
            _AccountSubHead = "";
            _CreditSale = false;
            _CardPrice = false;
            _Attachment = string.Empty;
            _AttachmentData = null;
            _DuplicateCard = false;
            _DiscountPercentage = 0;
            _CustomerCode = string.Empty;
            _GSTTIN = string.Empty;
            _Idtype = 0;
            _CardNumber = string.Empty;
            _FK_Store = 0;
            _Status = 1;
            _FK_Reason = 1;
           

		}
        public Int16 Status
        {
            get { return _Status; }
            set { _Status = value; }
        }

        public long FK_Store
        {
            get { return _FK_Store; }
            set { _FK_Store = value; }
        }

        public decimal DiscountPercentage
        {
            get { return _DiscountPercentage; }
            set { _DiscountPercentage = value; }
        }
 
        public Boolean DuplicateCard
        {
            get { return _DuplicateCard; }
            set { _DuplicateCard = value; }
        }
      
     
        public byte[] AttachmentData
        {
            get { return _AttachmentData; }
            set { _AttachmentData = value; }
        }

        public string Attachment
        {
            get { return _Attachment; }
            set { _Attachment = value; }
        }
        public Boolean CardPrice
        {
            get { return _CardPrice; }
            set { _CardPrice = value; }
        }
        public Boolean CreditSale
        {
            get { return _CreditSale; }
            set { _CreditSale = value; }
        }
        public string AccountHead
        {
            get { return _AccountHead; }
            set { _AccountHead = value; }
        }
        private string _AccountSubHead;

        public string AccountSubHead
        {
            get { return _AccountSubHead; }
            set { _AccountSubHead = value; }
        }
		public byte UserAction
		{
			get { return _UserAction; }
			set { _UserAction = value; }
		}
        public long FK_AccountHead
        {
            get { return _FK_AccountHead; }
            set { _FK_AccountHead = value; }
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
			get { return _ID_Customer; }
			set { _ID_Customer = value; }
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
        public string Area
        {
            get { return _Area; }
            set { _Area = value; }
        }
        public string Place
        {
            get { return _Place; }
            set { _Place = value; }
        }
        public string Post
        {
            get { return _Post; }
            set { _Post = value; }
        }
	
	
		public string Pin
		{
			get { return _Pin; }
			set { _Pin = value; }
		}
		public string Phone
		{
			get { return _Phone; }
			set { _Phone = value; }
		}
		public string Mobile
		{
			get { return _Mobile; }
			set { _Mobile = value; }
		}
	
		public string Email
		{
			get { return _Email; }
			set { _Email = value; }
		}
		public DateTime DateOfBirth
		{
			get { return _DateOfBirth; }
			set { _DateOfBirth = value; }
		}
	
	
		public Boolean Active
		{
			get { return _Active; }
			set { _Active = value; }
		}
        public Boolean IsEmployee
        {
            get { return _IsEmployee; }
            set { _IsEmployee = value; }
        }
		public string InactiveRemarks
		{
			get { return _InactiveRemarks; }
			set { _InactiveRemarks = value; }
		}
	
		public long FK_Country
		{
			get { return _FK_Country; }
			set { _FK_Country = value; }
		}

        public long FK_State
        {
            get { return _FK_State; }
            set { _FK_State = value; }
        }

        public long FK_District
        {
            get { return _FK_District; }
            set { _FK_District = value; }
        }
		public long FK_CustomerGroup
		{
			get { return _FK_CustomerGroup; }
			set { _FK_CustomerGroup = value; }
		}
		public long FK_AccountHeadSub
		{
			get { return _FK_AccountHeadSub; }
			set { _FK_AccountHeadSub = value; }
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
      
        public long CreditDays
        {
            get { return _CreditDays; }
            set { _CreditDays = value; }
        }
        public decimal CreditLimit
        {
            get { return _CreditLimit; }
            set { _CreditLimit = value; }
        }

        public string CustomerCode
        {
            get { return _CustomerCode; }
            set { _CustomerCode = value; }
        }
        public string GSTTIN
        {
            get { return _GSTTIN; }
            set { _GSTTIN = value; }
        }
        public Int64  Idtype
        {
            get { return _Idtype; }
            set { _Idtype = value; }
        }
        public string CardNumber
        {
            get { return _CardNumber; }
            set { _CardNumber = value; }
        }
        public string LandMark
        {
            get { return _LandMark; }
            set { _LandMark = value; }
        }
        public long FK_Reason
        {
            get { return _FK_Reason; }
            set { _FK_Reason = value; }
        }
        
        
		public long InsertData()
		{
			long status;
			DalCustomer daldalcustomer = new DalCustomer();
			status = 0;
			try
			{
				_UserAction = 1;
				status = daldalcustomer.UpdateCustomer(this);
				daldalcustomer=null;
			}
			catch (Exception ex)
			{
 
			}
			return status;
		}
 
		public long UpdateData()
		{
			_UserAction = 2;
			DalCustomer daldalcustomer = new DalCustomer();
			long status;
			status = 0;
			try
			{
				status =  daldalcustomer.UpdateCustomer(this);
				daldalcustomer=null;
			}
			catch (Exception ex)
			{
 
			}
			return status;
		}
 
		public long DeleteData()
		{
			_UserAction =3;
			DalCustomer daldalcustomer = new DalCustomer();
			long status;
			status = 0;
			try
			{
				status =  daldalcustomer.DeleteCustomer(this);
				daldalcustomer=null;
			}
			catch (Exception ex)
			{
 
			}
			return status;
		}
 
		public void SelectData()
		{
			DalCustomer daldalcustomer = new DalCustomer();
			ICustomer icustomer;
			icustomer= (ICustomer)daldalcustomer.SelectCustomer(this);
            _CustomerCode = icustomer.CustomerCode;
			_Name = icustomer.Name;
			_ShortName = icustomer.ShortName;
            _Area = icustomer.Area;
            _Place = icustomer.Place;
            _Post = icustomer.Post;
			_Pin = icustomer.Pin;
			_Phone = icustomer.Phone;
			_Mobile = icustomer.Mobile;
			_Email = icustomer.Email;
			_DateOfBirth = icustomer.DateOfBirth;
            
			
			_Active = icustomer.Active;
			_InactiveRemarks = icustomer.InactiveRemarks;
			_FK_Country = icustomer.FK_Country;
            _FK_District = icustomer.FK_District;
            _FK_State = icustomer.FK_State;
			_FK_CustomerGroup = icustomer.FK_CustomerGroup;
			_FK_AccountHeadSub = icustomer.FK_AccountHeadSub;
            _CreditDays = icustomer.CreditDays;
            _CreditLimit = icustomer.CreditLimit;
            _CreditSale = icustomer.CreditSale;
            _CardPrice = icustomer.CardPrice;
            _DiscountPercentage = icustomer.DiscountPercentage;
            _GSTTIN = icustomer.GSTTIN;
            _Idtype = icustomer.Idtype;
            _CardNumber = icustomer.CardNumber;
		}
 
		public DataTable SelectAllData()
		{
			DataTable dtbl = new DataTable();
            DalCustomer dalcustomer = new DalCustomer();
            dtbl = dalcustomer.SelectAllCustomer(this);
            dalcustomer = null;
			return dtbl;
		}
        public DataTable SelectAllDataForReport()
        {
            DataTable dtbl = new DataTable();
            DalCustomer dalcustomer = new DalCustomer();
            dtbl = dalcustomer.SelectAllDataForReport(this);
            dalcustomer = null;
            return dtbl;
        }
        public DataTable SelectAllCustomerVeiw()
        {
            DataTable dtbl = new DataTable();
            DalCustomer dalcustomer = new DalCustomer();
            dtbl = dalcustomer.SelectAllCustomerVeiw(this);
            dalcustomer = null;
            return dtbl;
        }

        public DataTable SelectCustId()
        {
            DataTable dtbl = new DataTable();
            DalCustomer dalcustomer = new DalCustomer();
            dtbl = dalcustomer.SelectCustomerId(this);
            dalcustomer = null;
            return dtbl;
        }
        public DataTable SelectCustCreditLimit()
        {
            DataTable dtbl = new DataTable();
            DalCustomer dalcustomer = new DalCustomer();
            dtbl = dalcustomer.SelectCustCreditLimit(this);
            dalcustomer = null;
            return dtbl;
        }
    }
}
