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
	public class BlCategorySecond : ICategorySecond
	{
 
		private byte _UserAction;
		private Int16 _PageIndex;
		private Int16 _PageSize;
        private long _ID_SubCategory;
		private string _Name;
        private string _Shortname;
        private int _ReturnPeriod;
        private long _FK_Category;
		private string _UserCode;
		private DateTime _EnteredOn;
		private long _FK_Client;
        private long _FK_Reason;
        

       

 
		public BlCategorySecond()
		{
			Initialize();
		}
		public void Initialize()
		{
            _ID_SubCategory = 0;
			_Name = string.Empty;
            _Shortname = string.Empty;
			_UserCode = string.Empty;
            _ReturnPeriod = 0;
			_EnteredOn = DateTime.Now;
            _FK_Category = 0;
			_FK_Client = 1;
            _PageIndex = 1;
            _PageSize = 10;
			_UserAction=1;
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
            get { return _ID_SubCategory; }
            set { _ID_SubCategory = value; }
		}
        public long FK_Category
        {
            get { return _FK_Category; }
            set { _FK_Category = value; }
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
        public int ReturnPeriod
		{
            get { return _ReturnPeriod; }
            set { _ReturnPeriod = value; }
		}
        public long FK_Reason
        {
            get { return _FK_Reason; }
            set { _FK_Reason = value; }
        }

		public long InsertData()
		{
			long status;
			DalCategorySecond dalcategorysecond = new DalCategorySecond();
			status = 0;
			try
			{
				_UserAction = 1;
				status = dalcategorysecond.UpdateCategorySecond(this);
				dalcategorysecond=null;
			}
			catch (Exception ex)
			{
 
			}
			return status;
		}
 
		public long UpdateData()
		{
			_UserAction = 2;
			DalCategorySecond dalcategorysecond = new DalCategorySecond();
			long status;
			status = 0;
			try
			{
				status =  dalcategorysecond.UpdateCategorySecond(this);
				dalcategorysecond=null;
			}
			catch (Exception ex)
			{
 
			}
			return status;
		}
 
		public long DeleteData()
		{
			_UserAction =3;
			DalCategorySecond dalcategorysecond = new DalCategorySecond();
			long status;
			status = 0;
			try
			{
				status =  dalcategorysecond.DeleteCategorySecond(this);
				dalcategorysecond=null;
			}
			catch (Exception ex)
			{
 
			}
			return status;
		}
 
		public void SelectData()
		{
			DalCategorySecond dalcategorysecond = new DalCategorySecond();
			ICategorySecond icategorysecond;
			icategorysecond= (ICategorySecond)dalcategorysecond.SelectCategorySecond(this);
			_Name = icategorysecond.Name;
			_Shortname = icategorysecond.Shortname;
            _FK_Category = icategorysecond.FK_Category;
           

		}
 
		public DataTable SelectAllData()
		{
			DataTable dtbl = new DataTable();
            DalCategorySecond dalcategorysecond = new DalCategorySecond();
            dtbl = dalcategorysecond.SelectAllCategorySecond(this);
            dalcategorysecond = null;
			return dtbl;
		}
	}
}
