/*----------------------------------- Business Layer Class -----------------------------------------------------------------------------------------------------------------------
Purpose    : 
Author     : Nithin Das H
Copyright  : Perfect Software Solutions (Clt) Pvt Ltd
Created on : 07/02/2014
----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
WebPharma
On			By			OMID
----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

07/02/2014	Nithin Das H		33235
--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Data;
using System.Threading.Tasks;
using WebPharma.DataAccess.Masters;
using WebPharma.Interface.Masters;
using WebPharma.Interface.General;
 
namespace WebPharma.Business.Masters
{
	public class BlSupplier : ISupplier
	{
 
		private byte _UserAction;
		private Int16 _PageIndex;
		private Int16 _PageSize; 
		private long _ID_Supplier;
		private string _SupplierName;
		private string _ContactPerson;
		private string _Area;
        private string _Place;
        private string _Post;
        private string _Pin;
        private string _LandMark;
        private long _FK_District;
        private long _FK_State;
		private long _FK_Country;
        private string _Fax;
        private string _Phone;
        private string _Mobile;
        private string _Email;
        private string _Website;
        private string _TinNo;
        private string _CstNo;
        private string _KgstNo;
        private Boolean _GovernmentSupplier;
        private Boolean _Active;
        private string _UserCode;
        private DateTime _EnteredOn;
        private long _FK_Reason;
		private long _FK_Client;
     
 
		public BlSupplier()
		{
			Initialize();
		}
		public void Initialize()
		{
			_ID_Supplier = 0;
            _SupplierName = string.Empty;
			_ContactPerson = string.Empty;
            _Area = string.Empty;
            _Place = string.Empty;
            _Post = string.Empty;
            _Pin = string.Empty;
            _LandMark = string.Empty;
            _FK_District = 0;
            _FK_State = 0;
            _FK_Country = 0;
            _Fax = string.Empty;
			_Phone = string.Empty;
		    _Mobile = string.Empty;
            _Email = string.Empty;
		    _Website = string.Empty;
            _TinNo=  string.Empty;
            _CstNo= string.Empty;
            _KgstNo=  string.Empty;
			_GovernmentSupplier = false;
			_Active = false;
			_UserCode = string.Empty;
			_EnteredOn = DateTime.Now;
			_FK_Client = 1;
            _UserAction = 1;
            _FK_Reason=1;
            
            
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
            get { return _ID_Supplier; }
            set { _ID_Supplier = value; }
		}

        public string SupplierName
		{
            get { return _SupplierName; }
            set { _SupplierName = value; }
		}
		public string ContactPerson
		{
			get { return _ContactPerson; }
			set { _ContactPerson = value; }
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
        public string LandMark
        {
            get { return _LandMark; }
            set { _LandMark = value; }
        }
        public long FK_District
        {
            get { return _FK_District; }
            set { _FK_District = value; }
        }
        public long FK_State
        {
            get { return _FK_State; }
            set { _FK_State = value; }
        }

		public long FK_Country
		{
			get { return _FK_Country; }
			set { _FK_Country = value; }
		}

        public string Fax
		{
            get { return _Fax; }
            set { _Fax = value; }
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
        public string Website
        {
            get { return _Website; }
            set { _Website = value; }
        }


        public string TinNo
        {
            get { return _TinNo; }
            set { _TinNo = value; }
        }
        public string KgstNo
        {
            get { return _KgstNo; }
            set { _KgstNo = value; }
        }
        public string CstNo
        {
            get { return _CstNo; }
            set { _CstNo = value; }
        }
	
		public Boolean Active
		{
			get { return _Active; }
			set { _Active = value; }
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

        public Boolean GovernmentSupplier
        {
            get { return _GovernmentSupplier; }
            set { _GovernmentSupplier = value; }
        }
        public long FK_Reason
        {
            get { return _FK_Reason; }
            set { _FK_Reason = value; }
        }
		public long InsertData()
		{
			long status;
			DalSupplier dalsupplier = new DalSupplier();
			status = 0;
			try
			{
				_UserAction = 1;
				status = dalsupplier.UpdateSupplier(this);
				dalsupplier=null;
			}
			catch (Exception ex)
			{
 
			}
			return status;
		}
 
		public long UpdateData()
		{
			_UserAction = 2;
			DalSupplier dalsupplier = new DalSupplier();
			long status;
			status = 0;
			try
			{
				status =  dalsupplier.UpdateSupplier(this);
				dalsupplier=null;
			}
			catch (Exception ex)
			{
 
			}
			return status;
		}
 
		public long DeleteData()
		{
			_UserAction =3;
			DalSupplier dalsupplier = new DalSupplier();
			long status;
			status = 0;
			try
			{
				status =  dalsupplier.DeleteSupplier(this);
				dalsupplier=null;
			}
			catch (Exception ex)
			{
 
			}
			return status;
		}
 
		public void SelectData()
		{
			DalSupplier dalsupplier = new DalSupplier();
			ISupplier isupplier;
			isupplier= (ISupplier)dalsupplier.SelectSupplier(this);
			_SupplierName = isupplier.SupplierName;
			_ContactPerson = isupplier.ContactPerson;
			_Area = isupplier.Area;
            _Place = isupplier.Place;
            _Post = isupplier.Post;
            _Pin = isupplier.Pin;
            _LandMark = isupplier.LandMark;
            _FK_District = isupplier.FK_District;
            _FK_State = isupplier.FK_State;
			_FK_Country = isupplier.FK_Country;
            _Fax = isupplier.Fax;
            _Phone = isupplier.Phone;
            _Mobile = isupplier.Mobile;
            _Email = isupplier.Email;
			_Website = isupplier.Website;
            _TinNo = isupplier.TinNo;
            _KgstNo = isupplier.KgstNo;
            _CstNo = isupplier.CstNo ;
            _GovernmentSupplier = isupplier.GovernmentSupplier;
            _Active = isupplier.Active;
            _UserCode = isupplier.UserCode;
            _EnteredOn = isupplier.EnteredOn;
            _FK_Reason = isupplier.FK_Reason;
            _FK_Client = isupplier.FK_Client;
		}

        public DataTable SelectTinNumber()
        {
            DataTable dtbl = new DataTable();
            DalSupplier dalSupplier = new DalSupplier();
            dtbl = dalSupplier.SelectTinNumber(this);
            dalSupplier = null;
            return dtbl;
        }

		public DataTable SelectAllData()
		{
			DataTable dtbl = new DataTable();
			DalSupplier dalSupplier = new DalSupplier();
			dtbl = dalSupplier.SelectAllSupplier(this);
			dalSupplier = null;
			return dtbl;
		}
	}
}
