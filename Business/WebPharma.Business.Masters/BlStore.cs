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
	public class BlStore : IStore
	{
 
		private byte _UserAction;
		private Int16 _PageIndex;
		private Int16 _PageSize; 
		private long _ID_Store;
		private string _Name;
		private string _Shortname;
		private string _Description;
		private string _UserCode;
		private DateTime _EnteredOn;
		private long _FK_Client;
        private Int16 _Type;
        private Int16 _BranchCode;
        private decimal _OpeningBalance;
        private decimal _OpeningLiability;
        private string _Address1;
        private string _Address2;
        private string _Address3;
        private string _Email;
        private string _Phone;
        private string _Website;
        private DateTime _StartDate;
        private Int16 _BranchType;
        private byte _Cancelled;
        private string _CancelledBy;
        private DateTime _CancelledOn;
        private Boolean _Active;
        private long _FK_Reason;
        private long _FK_Agent;

		public BlStore()
		{
			Initialize();
		}
		public void Initialize()
		{
			_ID_Store = 0;
			_Name = string.Empty;
			_Shortname = string.Empty;
			_Description = string.Empty;
			_UserCode = string.Empty;
			_EnteredOn = DateTime.Now;
			_FK_Client = 1;
		    _PageIndex = 1;
            _PageSize = 10;
            _UserAction = 1;
            _Type = 0;
            _BranchCode = 0;
            _OpeningBalance = 0;
            _OpeningLiability = 0;
            _Address1 = string.Empty;
            _Address2 = string.Empty;
            _Address3 = string.Empty;
            _Email=string.Empty;
            _Phone = string.Empty;
            _Website=string.Empty;
            _StartDate = DateTime.Now;
            _BranchType = 1;
            _Cancelled = 0;
            _CancelledBy = string.Empty;
            _CancelledOn=DateTime.Now;
            _Active = false;
            _FK_Reason = 1;
            _FK_Agent = 0;
		}
        public long FK_Agent
        {
            get { return _FK_Agent; }
            set { _FK_Agent = value; }
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
			get { return _ID_Store; }
			set { _ID_Store = value; }
		}
	
		public string Name
		{
			get { return _Name; }
			set { _Name = value; }
		}
		public string Shortname
		{
			get { return _Shortname; }
			set { _Shortname = value; }
		}
		
		public string Description
		{
			get { return _Description; }
			set { _Description = value; }
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
        public Int16 Type
        {
            get { return _Type; }
            set { _Type = value; }
        }
        public Int16 BranchCode
        {
            get { return _BranchCode; }
            set { _BranchCode = value; }
        }
        public decimal OpeningBalance
        {
            get { return _OpeningBalance; }
            set { _OpeningBalance = value; }
        }
        public decimal OpeningLiability
        {
            get { return _OpeningLiability; }
            set { _OpeningLiability = value; }
        }
        public string Address1
        {
            get { return _Address1; }
            set { _Address1 = value; }
        }
        public string Address2
        {
            get { return _Address2; }
            set { _Address2 = value; }
        }
        public string Address3
        {
            get { return _Address3; }
            set { _Address3 = value; }
        }
        public string Email
        {
            get { return _Email; }
            set { _Email = value; }
        }
        public string Phone
        {
            get { return _Phone; }
            set { _Phone = value; }
        }
        public string Website
        {
            get { return _Website; }
            set { _Website = value; }
        }
        public DateTime  StartDate
        {
            get { return _StartDate; }
            set { _StartDate = value; }
        }
        public Int16 BranchType
        {
            get { return _BranchType; }
            set { _BranchType = value; }
        }
        public byte Cancelled
        {
            get { return _Cancelled; }
            set { _Cancelled = value; }
        }
            public string CancelledBy
        {
            get { return _CancelledBy; }
            set { _CancelledBy = value; }
        }

            public DateTime CancelledOn
        {
            get { return _CancelledOn; }
            set { _CancelledOn = value; }
        }

            public Boolean Active
        {
            get { return _Active; }
            set { _Active = value; }
        }
            public long FK_Reason
            {
                get { return _FK_Reason; }
                set { _FK_Reason = value; }
            }

		public long InsertData()
		{
			long status;
			DalStore dalstore = new DalStore();
			status = 0;
			try
			{
				_UserAction = 1;
				status = dalstore.UpdateStore(this);
				dalstore=null;
			}
			catch (Exception ex)
			{
 
			}
			return status;
		}
 
		public long UpdateData()
		{
			_UserAction = 2;
			DalStore dalstore = new DalStore();
			long status;
			status = 0;
			try
			{
				status =  dalstore.UpdateStore(this);
				dalstore=null;
			}
			catch (Exception ex)
			{
 
			}
			return status;
		}
 
		public long DeleteData()
		{
			_UserAction =3;
			DalStore dalstore = new DalStore();
			long status;
			status = 0;
			try
			{
				status =  dalstore.DeleteStore(this);
				dalstore=null;
			}
			catch (Exception ex)
			{
 
			}
			return status;
		}
 
		public void SelectData()
		{
			DalStore dalstore = new DalStore();
			IStore istore;
			istore= (IStore)dalstore.SelectStore(this);
			_Name = istore.Name;
			_Shortname = istore.Shortname;
			_Description = istore.Description;
            _Type = istore.Type;
            _BranchCode = istore.BranchCode;
            _OpeningBalance = istore.OpeningBalance;
            _OpeningLiability = istore.OpeningLiability;
            _Address1 = istore.Address1;
            _Address2 = istore.Address2;
            _Address3 = istore.Address3;
            _Phone = istore.Phone;
            _Email = istore.Email;
            _Website = istore.Website;
            _StartDate = istore.StartDate;
            _BranchType = istore.BranchType;
            Cancelled = istore.Cancelled;
            _CancelledBy = istore.CancelledBy;
            _CancelledOn = istore.CancelledOn;
            _FK_Agent = istore.FK_Agent;

        }
 
		public DataTable SelectAllData()
		{
			DataTable dtbl = new DataTable();
		    DalStore dalstore = new DalStore();
			dtbl = dalstore.SelectAllStore(this);
			dalstore = null;
			return dtbl;
		}

        public DataTable bindBranch()
        {
            DataTable dtbl = new DataTable();
            DalStore dalstore = new DalStore();
            dtbl = dalstore.selectBranch(this);
            dalstore = null;
            return dtbl;
           
        }

        public DataTable selectaccountlink()
        {
            DataTable dtbl = new DataTable();
            DalStore dalstore = new DalStore();
            dtbl = dalstore.selectaccountlink(this);
            dalstore = null;
            return dtbl;
            
        }
    }
}
