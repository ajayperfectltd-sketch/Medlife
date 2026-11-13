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
	public class BlShift : IShift
	{
 
		private byte _UserAction;
		private Int16 _PageIndex;
		private Int16 _PageSize; 
		private long _ID_Shift;
		private string _Name;
		private string _ShortName;
		private DateTime _ShiftFrom;
		private DateTime _ShiftTo;
		private string _UserCode;
		private DateTime _EnteredOn;
		private long _FK_Client;
        private DateTime _Date;
        private Int16 _Type;
        private bool _Active;
        private long _FK_Reason;
 
 
		public BlShift()
		{
			Initialize();
		}
		public void Initialize()
		{
			_ID_Shift = 0;
			_Name = string.Empty;
			_ShortName = string.Empty;
			_ShiftFrom = DateTime.Now;
			_ShiftTo = DateTime.Now;
			_UserCode = string.Empty;
			_EnteredOn = DateTime.Now;
			_FK_Client = 1;
            _PageIndex = 1;
            _PageSize = 10;
            _UserAction = 1;
            _Date = DateTime.Now;
            _Type = 1;
            _Active = false;
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
			get { return _ID_Shift; }
			set { _ID_Shift = value; }
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
		
		public DateTime ShiftFrom
		{
			get { return _ShiftFrom; }
			set { _ShiftFrom = value; }
		}
		public DateTime ShiftTo
		{
			get { return _ShiftTo; }
			set { _ShiftTo = value; }
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
        public DateTime Date
        {
            get { return _Date; }
            set { _Date = value; }
        }
        public Int16 Type
        {
            get { return _Type; }
            set { _Type = value; }
        }
        public bool Active
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
			DalShift dalshift = new DalShift();
			status = 0;
			try
			{
				_UserAction = 1;
				status = dalshift.UpdateShift(this);
				dalshift=null;
			}
			catch (Exception ex)
			{
 
			}
			return status;
		}
 
		public long UpdateData()
		{
			_UserAction = 2;
			DalShift dalshift = new DalShift();
			long status;
			status = 0;
			try
			{
				status =  dalshift.UpdateShift(this);
				dalshift=null;
			}
			catch (Exception ex)
			{
 
			}
			return status;
		}
 
		public long DeleteData()
		{
			_UserAction =3;
			DalShift dalshift = new DalShift();
			long status;
			status = 0;
			try
			{
				status =  dalshift.DeleteShift(this);
				dalshift=null;
			}
			catch (Exception ex)
			{
 
			}
			return status;
		}
 
		public void SelectData()
		{
			DalShift dalshift = new DalShift();
			IShift ishift;
			ishift= (IShift)dalshift.SelectShift(this);
			_Name = ishift.Name;
			_ShortName = ishift.ShortName;
			_ShiftFrom = ishift.ShiftFrom;
			_ShiftTo = ishift.ShiftTo;
            _Active = ishift.Active;
		

		}
 
		public DataTable SelectAllData()
		{
			DataTable dtbl = new DataTable();
            DalShift dalshift = new DalShift();
            dtbl = dalshift.SelectAllShift(this);
            dalshift = null;
			return dtbl;
		}
        public DataSet SelectShiftCloseDetails()
        {
            DataSet dset = new DataSet();
            DalShift dalshift = new DalShift();
            dset = dalshift.SelectShiftCloseDetails(this);
            dalshift = null;
            return dset;
        }
	}
}
