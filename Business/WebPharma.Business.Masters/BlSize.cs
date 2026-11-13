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
	public class BlSize : ISize
	{
 
		private byte _UserAction;
		private Int16 _PageIndex;
		private Int16 _PageSize; 
		private long _ID_Size;
		private string _Name;
		private string _ShortName;
		private string _UserCode;
		private DateTime _EnteredOn;
		private long _FK_Client;

 
		public BlSize()
		{
			Initialize();
		}
		public void Initialize()
		{
			_ID_Size = 0;
			_Name = string.Empty;
			_ShortName = string.Empty;
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
			get { return _ID_Size; }
			set { _ID_Size = value; }
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
			DalSize dalsize = new DalSize();
			status = 0;
			try
			{
				_UserAction = 1;
				status = dalsize.UpdateSize(this);
				dalsize=null;
			}
			catch (Exception ex)
			{
 
			}
			return status;
		}
 
		public long UpdateData()
		{
			_UserAction = 2;
			DalSize dalsize = new DalSize();
			long status;
			status = 0;
			try
			{
				status =  dalsize.UpdateSize(this);
				dalsize=null;
			}
			catch (Exception ex)
			{
 
			}
			return status;
		}
 
		public long DeleteData()
		{
			_UserAction =3;
			DalSize dalsize = new DalSize();
			long status;
			status = 0;
			try
			{
				status =  dalsize.DeleteSize(this);
				dalsize=null;
			}
			catch (Exception ex)
			{
 
			}
			return status;
		}
 
		public void SelectData()
		{
			DalSize dalsize = new DalSize();
			ISize isize;
			isize= (ISize)dalsize.SelectSize(this);
			_Name = isize.Name;
			_ShortName = isize.ShortName;

		}
 
		public DataTable SelectAllData()
		{
			DataTable dtbl = new DataTable();
			DalSize dalSize = new DalSize();
			dtbl = dalSize.SelectAllSize(this);
			dalSize = null;
			return dtbl;
		}
	}
}
