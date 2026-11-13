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
	public class BlPromotionType : IPromotionType
	{
 
		private byte _UserAction;
		private Int16 _PageIndex;
		private Int16 _PageSize; 
		private long _ID_PromotionType;
		private string _Name;
		private string _ShortName;
		private string _UserCode;
		private DateTime _EnteredOn;
		private long _FK_Client;


 
		public BlPromotionType()
		{
			Initialize();
		}
		public void Initialize()
		{
			_ID_PromotionType = 0;
			_Name = string.Empty;
			_ShortName = string.Empty;
			_UserCode = string.Empty;
			_EnteredOn = DateTime.Now;
			_FK_Client = 1;
            _PageIndex = 1;
            _PageSize = 10;
			_UserAction=1;
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
			get { return _ID_PromotionType; }
			set { _ID_PromotionType = value; }
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
			DalPromotionType dalpromotiontype = new DalPromotionType();
			status = 0;
			try
			{
				_UserAction = 1;
				status = dalpromotiontype.UpdatePromotionType(this);
				dalpromotiontype=null;
			}
			catch (Exception ex)
			{
 
			}
			return status;
		}
 
		public long UpdateData()
		{
			_UserAction = 2;
			DalPromotionType dalpromotiontype = new DalPromotionType();
			long status;
			status = 0;
			try
			{
				status =  dalpromotiontype.UpdatePromotionType(this);
				dalpromotiontype=null;
			}
			catch (Exception ex)
			{
 
			}
			return status;
		}
 
		public long DeleteData()
		{
			_UserAction =3;
			DalPromotionType dalpromotiontype = new DalPromotionType();
			long status;
			status = 0;
			try
			{
				status =  dalpromotiontype.DeletePromotionType(this);
				dalpromotiontype=null;
			}
			catch (Exception ex)
			{
 
			}
			return status;
		}
 
		public void SelectData()
		{
			DalPromotionType dalpromotiontype = new DalPromotionType();
			IPromotionType ipromotiontype;
			ipromotiontype= (IPromotionType)dalpromotiontype.SelectPromotionType(this);
			_Name = ipromotiontype.Name;
			_ShortName = ipromotiontype.ShortName;

		}

        public DataTable SelectAllData()
        {
            DataTable dtbl = new DataTable();
            DalPromotionType dalPromotionType = new DalPromotionType();
            dtbl = dalPromotionType.SelectAllPromotionType(this);
            dalPromotionType = null;
            return dtbl;
        }
	}
}
