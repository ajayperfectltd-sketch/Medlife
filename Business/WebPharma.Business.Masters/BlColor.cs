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
	public class BlColor : IColor
	{
 
		private byte _UserAction;
		private Int16 _PageIndex;
		private Int16 _PageSize; 
		private long _ID_Color;
		private string _Name;
		private string _ShortName;
		private string _UserCode;
		private DateTime _EnteredOn;
		private long _FK_Client;

 
		public BlColor()
		{
			Initialize();
		}
		public void Initialize()
		{
			_ID_Color = 0;
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
			get { return _ID_Color; }
			set { _ID_Color = value; }
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
			DalColor dalcolor = new DalColor();
			status = 0;
			try
			{
				_UserAction = 1;
				status = dalcolor.UpdateColor(this);
				dalcolor=null;
			}
			catch (Exception ex)
			{
 
			}
			return status;
		}
 
		public long UpdateData()
		{
			_UserAction = 2;
			DalColor dalcolor = new DalColor();
			long status;
			status = 0;
			try
			{
				status =  dalcolor.UpdateColor(this);
				dalcolor=null;
			}
			catch (Exception ex)
			{
 
			}
			return status;
		}
 
		public long DeleteData()
		{
			_UserAction =3;
			DalColor dalcolor = new DalColor();
			long status;
			status = 0;
			try
			{
				status =  dalcolor.DeleteColor(this);
				dalcolor=null;
			}
			catch (Exception ex)
			{
 
			}
			return status;
		}
 
		public void SelectData()
		{
			DalColor dalcolor = new DalColor();
			IColor icolor;
			icolor= (IColor)dalcolor.SelectColor(this);
			_Name = icolor.Name;
			_ShortName = icolor.ShortName;

		}
 
		 public DataTable SelectAllData()
        {
            DataTable dtbl = new DataTable();
            DalColor dalColor = new DalColor();
            dtbl = dalColor.SelectAllColor(this);
            dalColor = null;
            return dtbl;
        }
	}
}
