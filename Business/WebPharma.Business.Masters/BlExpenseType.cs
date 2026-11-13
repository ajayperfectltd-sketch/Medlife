/*----------------------------------- Business Layer Class -----------------------------------------------------------------------------------------------------------------------
Purpose    : ExpenseType Business class
Author     : Praveen MP
Copyright  : Perfect Software Solutions (Clt) Pvt Ltd
Created on : 19/05/2018
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
    public class BlExpenseType : IExpenseType
	{
 
		private byte _UserAction;
		private Int16 _PageIndex;
		private Int16 _PageSize; 
		private long _ID_Type;
		private string _ExpenseType;
     
     
        private long _FK_AccountHeadSub;
        private long _FK_AccountHead;


		private string _UserCode;
		private DateTime _EnteredOn;
		private long _FK_Client;
        private long _FK_Reason;
        private Boolean _EtPurchase;
        private Boolean _EtSales;
        private Boolean _EtPurchaseReturn;
        private Boolean _EtSalesReturn;
        private Boolean _EtAddorless;

        public BlExpenseType()
		{
			Initialize();
		}
		public void Initialize()
		{
			_ID_Type = 0;
            _ExpenseType = string.Empty;
		
			_UserCode = string.Empty;
			_EnteredOn = DateTime.Now;
            _FK_AccountHead = 0;
            _FK_AccountHeadSub = 0;
            _EtPurchase = false;
            _EtPurchaseReturn = false;
            _EtSales = false;
            _EtSalesReturn = false;
            _EtAddorless = false;           
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
			get { return _ID_Type; }
			set { _ID_Type = value; }
		}
        public long FK_AccountHead
        {
            get { return _FK_AccountHead; }
            set { _FK_AccountHead = value; }
        }
        public long FK_AccountHeadSub
        {
            get { return _FK_AccountHeadSub; }
            set { _FK_AccountHeadSub = value; }
        }
		public string ExpenseType
		{
            get { return _ExpenseType; }
            set { _ExpenseType = value; }
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
        public long FK_Reason
		{
            get { return _FK_Reason; }
            set { _FK_Reason = value; }
		}
        public Boolean EtPurchase
        {
            get { return _EtPurchase; }
            set { _EtPurchase = value; }
        }
        public Boolean EtPurchaseReturn
        {
            get { return _EtPurchaseReturn; }
            set { _EtPurchaseReturn = value; }
        }
        public Boolean EtSales
        {
            get { return _EtSales; }
            set { _EtSales = value; }
        }
        public Boolean EtSalesReturn
        {
            get { return _EtSalesReturn; }
            set { _EtSalesReturn = value; }
        }
        public Boolean EtAddorless
        {
            get { return _EtAddorless; }
            set { _EtAddorless = value; }
        }


		public long InsertData()
		{
			long status;
            DalExpenseType dalExpenseType = new DalExpenseType();
			status = 0;
			try
			{
				_UserAction = 1;
				status = dalExpenseType.UpdateExpenseType(this);
				dalExpenseType=null;
			}
			catch (Exception ex)
			{
                throw ex;
			}
			return status;
		}
 
		public long UpdateData()
		{
			_UserAction = 2;
            DalExpenseType dalExpenseType = new DalExpenseType();
			long status;
			status = 0;
			try
			{
				status =  dalExpenseType.UpdateExpenseType(this);
				dalExpenseType=null;
			}
			catch (Exception ex)
			{
                throw ex;
			}
			return status;
		}
 
		public long DeleteData()
		{
			_UserAction =3;
            DalExpenseType dalExpenseType = new DalExpenseType();
			long status;
			status = 0;
			try
			{
				status =  dalExpenseType.DeleteExpenseType(this);
				dalExpenseType=null;
			}
			catch (Exception ex)
			{
                throw ex;
			}
			return status;
		}
 
		public void SelectData()
		{
            DalExpenseType dalExpenseType = new DalExpenseType();
			IExpenseType iExpenseType;
            iExpenseType = (IExpenseType)dalExpenseType.SelectExpenseType(this);
			_ExpenseType = iExpenseType.ExpenseType;
            _FK_AccountHead=iExpenseType.FK_AccountHead;
            _FK_AccountHeadSub = iExpenseType.FK_AccountHeadSub;
            _EtPurchase = iExpenseType.EtPurchase;
            _EtPurchaseReturn = iExpenseType.EtPurchaseReturn;
            _EtSales = iExpenseType.EtSales;
            _EtSalesReturn = iExpenseType.EtSalesReturn;
            _EtAddorless = iExpenseType.EtAddorless;
          
		}
 
		public DataTable SelectAllData()
		{
			DataTable dtbl = new DataTable();
            DalExpenseType dalExpenseType = new DalExpenseType();
            dtbl = dalExpenseType.SelectAllExpenseType(this);
            dalExpenseType = null;
			return dtbl;
		}
	}
}
