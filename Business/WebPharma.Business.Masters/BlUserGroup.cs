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
using System.Collections;
using System.Data;
using System.Threading.Tasks;
using WebPharma.DataAccess.Masters;
using WebPharma.Interface.Masters;
using WebPharma.Interface.General;
 
namespace WebPharma.Business.Masters
{
	public class BlUserGroup : IUserGroup
	{
 
		private byte _UserAction;
		private Int16 _PageIndex;
		private Int16 _PageSize; 
		private long _ID_UserGroup;
		private string _GroupName;
		private string _Shortname;
		private Boolean _Administrator;
		private Boolean _UgAdd;
		private Boolean _UgModify;
		private Boolean _UgDelete;
		private Boolean _UgView;
		private Boolean _UgDiscount;
		private Boolean _UgDayClose;
        private Boolean _UgMultiStoreLogin;
        private Boolean _Active;
		private string _UserCode;
		private DateTime _EnteredOn;
		private long _FK_Client;
        private Boolean _UgPurchase;
        private Boolean _UgSales;
        private Boolean _UgPurchaseReturn;
        private Boolean _UgSalesReturn;
        private Boolean _UgApprove;
        private Boolean _UgPriceCorrection;
        private long _FK_Reason;

 
		public BlUserGroup()
		{
			Initialize();
		}
		public void Initialize()
		{
			_ID_UserGroup = 0;
			_GroupName = string.Empty;
			_Shortname = string.Empty;
			_Administrator = false;
			_UgAdd = false;
			_UgModify = false;
			_UgDelete = false;
			_UgView = false;
			_UgDiscount = false;
			_UgDayClose = false;
            _UgMultiStoreLogin = false;
            _Active = true;
			_UserCode = string.Empty;
			_EnteredOn = DateTime.Now;
			_FK_Client = 1;
			_UserAction=1;
            _PageIndex = 1;
            _PageSize = 10;
            _UgPurchase = false;
            _UgPurchaseReturn = false;
            _UgSales = false;
            _UgSalesReturn = false;
            _UgApprove = false;
            _UgPriceCorrection = false;
            _FK_Reason = 0;
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
			get { return _ID_UserGroup; }
			set { _ID_UserGroup = value; }
		}	
		public string GroupName
		{
			get { return _GroupName; }
			set { _GroupName = value; }
		}
		public string Shortname
		{
			get { return _Shortname; }
			set { _Shortname = value; }
		}
		public Boolean Administrator
		{
			get { return _Administrator; }
			set { _Administrator = value; }
		}
		public Boolean UgAdd
		{
			get { return _UgAdd; }
			set { _UgAdd = value; }
		}
		public Boolean UgModify
		{
			get { return _UgModify; }
			set { _UgModify = value; }
		}
		public Boolean UgDelete
		{
			get { return _UgDelete; }
			set { _UgDelete = value; }
		}
		public Boolean UgView
		{
			get { return _UgView; }
			set { _UgView = value; }
		}
		public Boolean UgDiscount
		{
			get { return _UgDiscount; }
			set { _UgDiscount = value; }
		}
		public Boolean UgDayClose
		{
			get { return _UgDayClose; }
			set { _UgDayClose = value; }
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
        public Boolean UgMultiStoreLogin
        {
            get { return _UgMultiStoreLogin; }
            set { _UgMultiStoreLogin = value; }
        }
        public Boolean UgPurchase
        {
            get { return _UgPurchase; }
            set { _UgPurchase = value; }
        }
        public Boolean UgSales
        {
            get { return _UgSales; }
            set { _UgSales = value; }
        }
        public Boolean UgPurchaseReturn
        {
            get { return _UgPurchaseReturn; }
            set { _UgPurchaseReturn = value; }
        }
        public Boolean UgSalesReturn
        {
            get { return _UgSalesReturn; }
            set { _UgSalesReturn = value; }
        }
        public Boolean UgApprove
        {
            get { return _UgApprove; }
            set { _UgApprove = value; }
        }
        public Boolean UgPriceCorrection
        {
            get { return _UgPriceCorrection; }
            set { _UgPriceCorrection = value; }
        }
        public long FK_Reason
        {
            get { return _FK_Reason; }
            set { _FK_Reason = value; }
        }

		public long InsertData()
		{
			long status;
            DalUserGroup dalusergroup = new DalUserGroup();
			status = 0;
			try
			{
				_UserAction = 1;
				status = dalusergroup.UpdateUserGroup(this);
				dalusergroup=null;
			}
			catch (Exception ex)
			{
 
			}
			return status;
		}
 
		public long UpdateData()
		{
			_UserAction = 2;
			DalUserGroup dalusergroup = new DalUserGroup();
			long status;
			status = 0;
			try
			{
				status =  dalusergroup.UpdateUserGroup(this);
				dalusergroup=null;
			}
			catch (Exception ex)
			{
 
			}
			return status;
		}
 
		public long DeleteData()
		{
			_UserAction =3;
			DalUserGroup dalusergroup = new DalUserGroup();
			long status;
			status = 0;
			try
			{
				status =  dalusergroup.DeleteUserGroup(this);
				dalusergroup=null;
			}
			catch (Exception ex)
			{
 
			}
			return status;
		}
 
		public void SelectData()
		{
			DalUserGroup dalusergroup = new DalUserGroup();
			IUserGroup iusergroup;
			iusergroup= (IUserGroup)dalusergroup.SelectUserGroup(this);
			_GroupName = iusergroup.GroupName;
			_Shortname = iusergroup.Shortname;
			_Administrator = iusergroup.Administrator;
			_UgAdd = iusergroup.UgAdd;
			_UgModify = iusergroup.UgModify;
			_UgDelete = iusergroup.UgDelete;
			_UgView = iusergroup.UgView;
			_UgDiscount = iusergroup.UgDiscount;
			_UgDayClose = iusergroup.UgDayClose;
            _Active = iusergroup.Active;
            _UgMultiStoreLogin = iusergroup.UgMultiStoreLogin;
            _UgApprove = iusergroup.UgApprove;
            _UgPurchase = iusergroup.UgPurchase;
            _UgPurchaseReturn = iusergroup.UgPurchaseReturn;
            _UgSales = iusergroup.UgSales;
            _UgSalesReturn = iusergroup.UgSalesReturn;
            _UgPriceCorrection = iusergroup.UgPriceCorrection;

		}
 
		public DataTable SelectAllData()
		{
			DataTable dtbl = new DataTable();
			DalUserGroup dalusergroup = new DalUserGroup();
			dtbl = dalusergroup.SelectAllUserGroup(this);
			dalusergroup = null;
			return dtbl;
		}
	}
}
