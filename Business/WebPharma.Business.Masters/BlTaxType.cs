/*----------------------------------- Business Layer Class -----------------------------------------------------------------------------------------------------------------------
Purpose    : TaxType Business class
Author     : Praveen MP
Copyright  : Perfect Software Solutions (Clt) Pvt Ltd
Created on : 18/05/2018
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
    public class BlTaxType : ITaxType
	{
 
		private byte _UserAction;
		private Int16 _PageIndex;
		private Int16 _PageSize; 
		private long _ID_BillType;
		private string _TypeName;
     
        private Int32 _BillType;
        private long _FK_AccountHeadSub;
        private long _FK_AccountHead;


		private string _UserCode;
		private DateTime _EnteredOn;
		private long _FK_Client;
        private long _FK_Reason;
        private Boolean _TaxInterState;
        private Boolean _TaxIntraState;
        private byte _GSTType;
       
        public BlTaxType()
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
          
            _BillType = 0;
           
			_FK_Client = 1;
            _PageIndex = 1;
            _PageSize = 10;
			_UserAction=1;
            _FK_Reason = 0;

            _TaxInterState = false;
            _TaxIntraState = false;
            _GSTType = 0;
           
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
        public long FK_Reason
		{
            get { return _FK_Reason; }
            set { _FK_Reason = value; }
		}

        public Boolean TaxInterState
        {
            get { return _TaxInterState; }
            set { _TaxInterState = value; }
        }

        public Boolean TaxIntraState
        {
            get { return _TaxIntraState; }
            set { _TaxIntraState = value; }
        }
        public byte GSTType
        {
            get { return _GSTType; }
            set { _GSTType = value; }
        }
		public long InsertData()
		{
			long status;
            DalTaxType dalBillType = new DalTaxType();
			status = 0;
			try
			{
				_UserAction = 1;
				status = dalBillType.UpdateTaxType(this);
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
            DalTaxType dalBillType = new DalTaxType();
			long status;
			status = 0;
			try
			{
				status =  dalBillType.UpdateTaxType(this);
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
            DalTaxType dalBillType = new DalTaxType();
			long status;
			status = 0;
			try
			{
				status =  dalBillType.DeleteTaxType(this);
				dalBillType=null;
			}
			catch (Exception ex)
			{
 
			}
			return status;
		}
 
		public void SelectData()
		{
            DalTaxType dalTaxType = new DalTaxType();
			ITaxType iTaxType;
            iTaxType = (ITaxType)dalTaxType.SelectTaxType(this);
			_TypeName = iTaxType.TypeName;
            _FK_AccountHead=iTaxType.FK_AccountHead;
            _FK_AccountHeadSub = iTaxType.FK_AccountHeadSub;
          

            
		}
 
		public DataTable SelectAllData()
		{
			DataTable dtbl = new DataTable();
            DalTaxType dalTaxType = new DalTaxType();
            dtbl = dalTaxType.SelectAllTaxType(this);
            dalTaxType = null;
			return dtbl;
		}
	}
}
