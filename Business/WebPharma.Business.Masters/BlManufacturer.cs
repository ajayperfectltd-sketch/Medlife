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
	public class BlManufacturer : IManufacturer
	{
 
		private byte _UserAction;
		private Int16 _PageIndex;
		private Int16 _PageSize; 
		private long _ID_Manufacturer;
		private string _ShortName;
		private string _Name;
		private string _Phone;
		private string _Address1;
		private string _Address2;
		private string _Address3;
		private string _UserCode;
		private DateTime _EnteredOn;
		private long _FK_Client;
        private long _FK_Reason;
        private string _MobileNo;
 
		public BlManufacturer()
		{
			Initialize();
		}
		public void Initialize()
		{
			_ID_Manufacturer = 0;
			_ShortName = string.Empty;
			_Name = string.Empty;
			_Phone = string.Empty;
			_Address1 = string.Empty;
			_Address2 = string.Empty;
			_Address3 = string.Empty;
			_UserCode = string.Empty;
			_EnteredOn = DateTime.Now;
			_FK_Client = 1;
            _MobileNo = string.Empty;
			_UserAction=1;
            _PageIndex = 1;
            _PageSize = 10;
            _FK_Reason = 1;
		}
        public string MobileNo
        {
            get { return _MobileNo; }
            set { _MobileNo = value; }
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
			get { return _ID_Manufacturer; }
			set { _ID_Manufacturer = value; }
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
		public string Phone
		{
			get { return _Phone; }
			set { _Phone = value; }
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
        public long FK_Reason
        {
            get { return _FK_Reason; }
            set { _FK_Reason = value; }
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
			DalManufacturer dalmanufacturer = new DalManufacturer();
			status = 0;
			try
			{
				_UserAction = 1;
				status = dalmanufacturer.UpdateManufacturer(this);
				dalmanufacturer=null;
			}
			catch (Exception ex)
			{
 
			}
			return status;
		}
 
		public long UpdateData()
		{
			_UserAction = 2;
			DalManufacturer dalmanufacturer = new DalManufacturer();
			long status;
			status = 0;
			try
			{
				status =  dalmanufacturer.UpdateManufacturer(this);
				dalmanufacturer=null;
			}
			catch (Exception ex)
			{
 
			}
			return status;
		}
 
		public long DeleteData()
		{
			_UserAction =3;
			DalManufacturer dalmanufacturer = new DalManufacturer();
			long status;
			status = 0;
			try
			{
				status =  dalmanufacturer.DeleteManufacturer(this);
				dalmanufacturer=null;
			}
			catch (Exception ex)
			{
 
			}
			return status;
		}
 
		public void SelectData()
		{
			DalManufacturer dalmanufacturer = new DalManufacturer();
			IManufacturer imanufacturer;
			imanufacturer= (IManufacturer)dalmanufacturer.SelectManufacturer(this);
			_ShortName = imanufacturer.ShortName;
			_Name = imanufacturer.Name;
			_Phone = imanufacturer.Phone;
			_Address1 = imanufacturer.Address1;
			_Address2 = imanufacturer.Address2;
			_Address3 = imanufacturer.Address3;
            _MobileNo = imanufacturer.MobileNo;


		}
 
		public DataTable SelectAllData()
		{
			DataTable dtbl = new DataTable();
            DalManufacturer dalmanufacturer = new DalManufacturer();
            dtbl = dalmanufacturer.SelectAllManufacturer(this);
            dalmanufacturer = null;
			return dtbl;
		}
	}
}
