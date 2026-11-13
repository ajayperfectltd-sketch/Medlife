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
    public class BlVoucher : WebPharma.Interface.Masters.IVoucher
	{
 
		private byte _UserAction;
		private Int16 _PageIndex;
		private Int16 _PageSize; 
		private long _ID_Voucher;
		private string _ShortName;
		private string _Name;
		private byte _Type;
		private decimal _Amount;
		private decimal _Percentage;
		private Int32 _validity;
		private long _FK_AccountHeadSub;
		private string _Remarks;
		private Boolean _Active;
		private string _UserCode;
		private DateTime _EnteredOn;
		private long _FK_Client;
        private bool _IsRefrence;

        public bool IsRefrence
        {
            get { return _IsRefrence; }
            set { _IsRefrence = value; }
        }
        

 
		public BlVoucher()
		{
			Initialize();
		}
		public void Initialize()
		{
			_ID_Voucher = 0;
			_ShortName = string.Empty;
			_Name = string.Empty;
			_Type = 0;
			_Amount = 0;
			_Percentage = 0;
			_validity = 0;
			_FK_AccountHeadSub = 0;
			_Remarks = string.Empty;
			_Active = false;
			_UserCode = string.Empty;
			_EnteredOn = DateTime.Now;
			_FK_Client = 0;
            _PageIndex = 1;
            _PageSize = 10;
            _UserAction = 1;
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
			get { return _ID_Voucher; }
			set { _ID_Voucher = value; }
		}
	
		public string ShortName
		{
			get { return _ShortName; }
			set { _ShortName = value; }
		}
		public string Name
		{
			get { return _Name; }
			set { _Name = value; }
		}
		public byte Type
		{
			get { return _Type; }
			set { _Type = value; }
		}
		public decimal Amount
		{
			get { return _Amount; }
			set { _Amount = value; }
		}
		public decimal Percentage
		{
			get { return _Percentage; }
			set { _Percentage = value; }
		}
		public Int32 validity
		{
			get { return _validity; }
			set { _validity = value; }
		}
		public long FK_AccountHeadSub
		{
			get { return _FK_AccountHeadSub; }
			set { _FK_AccountHeadSub = value; }
		}
		public string Remarks
		{
			get { return _Remarks; }
			set { _Remarks = value; }
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

		public long InsertData()
		{
			long status;
			DalVoucher dalvoucher = new DalVoucher();
			status = 0;
			try
			{
				_UserAction = 1;
				status = dalvoucher.UpdateVoucher(this);
				dalvoucher=null;
			}
			catch (Exception ex)
			{
 
			}
			return status;
		}
 
		public long UpdateData()
		{
			_UserAction = 2;
			DalVoucher dalvoucher = new DalVoucher();
			long status;
			status = 0;
			try
			{
				status =  dalvoucher.UpdateVoucher(this);
				dalvoucher=null;
			}
			catch (Exception ex)
			{
 
			}
			return status;
		}
 
		public long DeleteData()
		{
			_UserAction =3;
			DalVoucher dalvoucher = new DalVoucher();
			long status;
			status = 0;
			try
			{
				status =  dalvoucher.DeleteVoucher(this);
				dalvoucher=null;
			}
			catch (Exception ex)
			{
 
			}
			return status;
		}
 
		public void SelectData()
		{
			DalVoucher dalvoucher = new DalVoucher();
			IVoucher ivoucher;
			ivoucher= (IVoucher)dalvoucher.SelectVoucher(this);
			_ShortName = ivoucher.ShortName;
			_Name = ivoucher.Name;
			_Type = ivoucher.Type;
			_Amount = ivoucher.Amount;
			_Percentage = ivoucher.Percentage;
			_validity = ivoucher.validity;
			_FK_AccountHeadSub = ivoucher.FK_AccountHeadSub;
			_Remarks = ivoucher.Remarks;
			_Active = ivoucher.Active;

		}
 
		public DataTable SelectAllData()
		{
			DataTable dtbl = new DataTable();
			DalVoucher dalvoucher = new DalVoucher();
            dtbl = dalvoucher.SelectAllVoucher(this);
			dalvoucher = null;
			return dtbl;
		}
	}
}