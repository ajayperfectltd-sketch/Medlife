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
	public class BlBrand : IBrand
	{
 
		private byte _UserAction;
		private Int16 _PageIndex;
		private Int16 _PageSize; 
		private long _ID_Brand;
		private string _Shortname;
		private string _Name;
		private string _Remarks;
		private long _FK_Manufacturer;
		private string _UserCode;
		private DateTime _EnteredOn;
		private long _FK_Client;

 
		public BlBrand()
		{
			Initialize();
		}
		public void Initialize()
		{
			_ID_Brand = 0;
			_Shortname = string.Empty;
			_Name = string.Empty;
			_Remarks = string.Empty;
			_FK_Manufacturer = 0;
			_UserCode = string.Empty;
			_EnteredOn = DateTime.Now;
			_FK_Client = 1;
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
			get { return _ID_Brand; }
			set { _ID_Brand = value; }
		}
	
		public string Shortname
		{
			get { return _Shortname; }
			set { _Shortname = value; }
		}
		public string Name
		{
			get { return _Name; }
			set { _Name = value; }
		}
		public string Remarks
		{
			get { return _Remarks; }
			set { _Remarks = value; }
		}
		public long FK_Manufacturer
		{
			get { return _FK_Manufacturer; }
			set { _FK_Manufacturer = value; }
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
			DalBrand dalbrand = new DalBrand();
			status = 0;
			try
			{
				_UserAction = 1;
				status = dalbrand.UpdateBrand(this);
				dalbrand=null;
			}
			catch (Exception ex)
			{
 
			}
			return status;
		}
 
		public long UpdateData()
		{
			_UserAction = 2;
			DalBrand dalbrand = new DalBrand();
			long status;
			status = 0;
			try
			{
				status =  dalbrand.UpdateBrand(this);
				dalbrand=null;
			}
			catch (Exception ex)
			{
 
			}
			return status;
		}
 
		public long DeleteData()
		{
			_UserAction =3;
			DalBrand dalbrand = new DalBrand();
			long status;
			status = 0;
			try
			{
				status =  dalbrand.DeleteBrand(this);
				dalbrand=null;
			}
			catch (Exception ex)
			{
 
			}
			return status;
		}
 
		public void SelectData()
		{
			DalBrand dalbrand = new DalBrand();
			IBrand ibrand;
			ibrand= (IBrand)dalbrand.SelectBrand(this);
			_Shortname = ibrand.Shortname;
			_Name = ibrand.Name;
			_Remarks = ibrand.Remarks;
			_FK_Manufacturer = ibrand.FK_Manufacturer;

		}
 
		public DataTable SelectAllData()
		{
			DataTable dtbl = new DataTable();
			DalBrand dalBrand = new DalBrand();
			dtbl = dalBrand.SelectAllBrand(this);
			dalBrand = null;
			return dtbl;
		}
	}
}
