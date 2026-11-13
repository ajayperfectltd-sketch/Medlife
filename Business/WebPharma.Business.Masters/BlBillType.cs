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
	public class BlBillType : IBillType
	{
 
		private byte _UserAction;
		private Int16 _PageIndex;
		private Int16 _PageSize; 
		private long _ID_BillType;
		private string _TypeName;
        private Boolean _GovtSuppliers;
        private Boolean _Credit;

     
        private Int32 _BillType;
        private long _FK_AccountHeadSub;
        private long _FK_AccountHead;


        private long _FK_AccountHeadCard;
        private long _FK_AccountHeadSubCard;
        private long _FK_AccountHeadCheque;
        private long _FK_AccountHeadSubCheque;

        private long _FK_AccountReturn;      
        private long _FK_AccountSubReturn;

		private string _UserCode;
		private DateTime _EnteredOn;
		private long _FK_Client;

       
        private long _FK_AccountHeadRounding;
        private long _FK_AccountHeadSubRounding;

      
        private long _FK_AccountHeadDiscount;
        private long _FK_AccountHeadSubDiscount;
        private long _FK_AccountCredit;

        private long _FK_Reason;
        

      
        public long FK_AccountHeadDiscount
        {
            get { return _FK_AccountHeadDiscount; }
            set { _FK_AccountHeadDiscount = value; }
        }
        public long FK_AccountHeadSubDiscount
        {
            get { return _FK_AccountHeadSubDiscount; }
            set { _FK_AccountHeadSubDiscount = value; }
        }
        
       
       
        public long FK_AccountHeadRounding
        {
            get { return _FK_AccountHeadRounding; }
            set { _FK_AccountHeadRounding = value; }
        }
        

        public long FK_AccountHeadSubRounding
        {
            get { return _FK_AccountHeadSubRounding; }
            set { _FK_AccountHeadSubRounding = value; }
        }
        public long FK_AccountCredit
        {
            get { return _FK_AccountCredit; }
            set { _FK_AccountCredit = value; }
        }

        public long FK_AccountHeadCard
        {
            get { return _FK_AccountHeadCard; }
            set { _FK_AccountHeadCard = value; }
        }
        public long FK_AccountHeadSubCard
        {
            get { return _FK_AccountHeadSubCard; }
            set { _FK_AccountHeadSubCard = value; }
        }
        public long FK_AccountHeadCheque
        {
            get { return _FK_AccountHeadCheque; }
            set { _FK_AccountHeadCheque = value; }
        }
        public long FK_AccountHeadSubCheque
        {
            get { return _FK_AccountHeadSubCheque; }
            set { _FK_AccountHeadSubCheque = value; }
        }

        public long FK_Reason
        {
            get { return _FK_Reason; }
            set { _FK_Reason = value; }
        }
		public BlBillType()
		{
			Initialize();
		}
		public void Initialize()
		{
			_ID_BillType = 0;
            _TypeName = string.Empty;
		
			_UserCode = string.Empty;
			_EnteredOn = DateTime.Now;
            _FK_AccountHead = 0;
            _FK_AccountHeadSub = 0;
           
            _FK_AccountSubReturn = 0;
            _FK_AccountReturn = 0;
            _BillType = 0;
            _GovtSuppliers = false;
			_FK_Client = 1;
            _PageIndex = 1;
            _PageSize = 10;
			_UserAction=1;
           
            _Credit = false;
           
            _FK_AccountHeadRounding = 0;
            _FK_AccountHeadSubRounding = 0;
         
            _FK_AccountHeadDiscount=0;
            _FK_AccountHeadSubDiscount=0;
            _FK_AccountCredit = 0;

            _FK_AccountHeadCard = 0;
            _FK_AccountHeadSubCard = 0;
            _FK_AccountHeadCheque = 0;
            _FK_AccountHeadSubCheque = 0;


            _FK_Reason = 0;
		}
        public Boolean Credit
        {
            get { return _Credit; }
            set { _Credit = value; }
        }
      
        public long FK_AccountSubReturn
        {
            get { return _FK_AccountSubReturn; }
            set { _FK_AccountSubReturn = value; }
        }
        public long FK_AccountReturn
        {
            get { return _FK_AccountReturn; }
            set { _FK_AccountReturn = value; }
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
        public Int32 BillType
		{
            get { return _BillType; }
            set { _BillType = value; }
		}
        public Int16 PageSize
        {
            get { return _PageSize; }
            set { _PageSize = value; }
        }
        public Boolean GovtSuppliers
        {
            get { return _GovtSuppliers; }
            set { _GovtSuppliers = value; }
        }
		public long MasterId
		{
			get { return _ID_BillType; }
			set { _ID_BillType = value; }
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
		public string TypeName
		{
            get { return _TypeName; }
            set { _TypeName = value; }
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
			DalBillType dalBillType = new DalBillType();
			status = 0;
			try
			{
				_UserAction = 1;
				status = dalBillType.UpdateBillType(this);
				dalBillType=null;
			}
			catch (Exception ex)
			{
 
			}
			return status;
		}
 
		public long UpdateData()
		{
			_UserAction = 2;
			DalBillType dalBillType = new DalBillType();
			long status;
			status = 0;
			try
			{
				status =  dalBillType.UpdateBillType(this);
				dalBillType=null;
			}
			catch (Exception ex)
			{
 
			}
			return status;
		}
 
		public long DeleteData()
		{
			_UserAction =3;
			DalBillType dalBillType = new DalBillType();
			long status;
			status = 0;
			try
			{
				status =  dalBillType.DeleteBillType(this);
				dalBillType=null;
			}
			catch (Exception ex)
			{
 
			}
			return status;
		}
 
		public void SelectData()
		{
			DalBillType dalBillType = new DalBillType();
			IBillType iBillType;
			iBillType= (IBillType)dalBillType.SelectBillType(this);
			_TypeName = iBillType.TypeName;
            _BillType = iBillType.BillType;
            _FK_AccountHead=iBillType.FK_AccountHead;
            _FK_AccountHeadSub = iBillType.FK_AccountHeadSub;
            _GovtSuppliers = iBillType.GovtSuppliers;
          
            _FK_AccountReturn = iBillType.FK_AccountReturn;
            _FK_AccountSubReturn = iBillType.FK_AccountSubReturn;
          
            _Credit = iBillType.Credit  ;

           
            _FK_AccountHeadRounding = iBillType.FK_AccountHeadRounding;
            _FK_AccountHeadSubRounding = iBillType.FK_AccountHeadSubRounding;

          
            _FK_AccountHeadDiscount = iBillType.FK_AccountHeadDiscount;
            _FK_AccountHeadSubDiscount = iBillType.FK_AccountHeadSubDiscount;
            _FK_AccountCredit = iBillType.FK_AccountCredit;

            _FK_AccountHeadCard = iBillType.FK_AccountHeadCard;
            _FK_AccountHeadSubCard = iBillType.FK_AccountHeadSubCard;
            _FK_AccountHeadCheque = iBillType.FK_AccountHeadCheque;
            _FK_AccountHeadSubCheque = iBillType.FK_AccountHeadSubCheque;
		}
 
		public DataTable SelectAllData()
		{
			DataTable dtbl = new DataTable();
            DalBillType dalBillType = new DalBillType();
            dtbl = dalBillType.SelectAllBillType(this);
            dalBillType = null;
			return dtbl;
		}
	}
}
