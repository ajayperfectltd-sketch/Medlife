/*----------------------------------- Business Layer Class -----------------------------------------------------------------------------------------------------------------------
Purpose    : 
Author     :bibin  tc
Copyright  : Perfect Software Solutions (Clt) Pvt Ltd
Created on : 03/05/2018
----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
MODIFICATIONS 
On			By			OMID
----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebPharma.Interface.Accounts;
using WebPharma.DataAccess.Accounts;
using System.Data;
using System.Collections;


namespace WebPharma.Business.Accounts
{
  public  class BlWebPharmaTransaction:IWebPharmaTransaction
    {

       
        private DateTime _TransDate;
        private string _TransType;
        private string _TransferTo;
        private byte _Type;
        private decimal _Amount;
        private byte _UserAction;
        private Int16 _PageIndex;
        private Int16 _PageSize;
        private long _ID_WebPharmaTransaction;
        private string _Remarks;
        private string _UserCode;
        private DateTime _EnteredOn;     
        private long _FK_Client;
        private Int64 _FK_Name;
        private string _Name;
        private long _FK_Store;
        private DateTime _FromDate;
        private DateTime _ToDate;
        private string _XmlTransfertToAcc;
        public BlWebPharmaTransaction()
		{
			Initialize();
		}

        public void Initialize()
        {
             _TransDate = DateTime.Now;
             _TransType = string.Empty; ;
              _TransferTo = string.Empty;
              _Type = 0;
             _Amount = 0;
             _UserAction = 1;
             _PageIndex = 1;
             _PageSize = 10;
             _ID_WebPharmaTransaction = 0;
             _Remarks = string.Empty;
             _UserCode = string.Empty;
             _EnteredOn = DateTime.Now;
             _FK_Client = 1;
              _FK_Name = 0;
              _Name = string.Empty;
              _FromDate = DateTime.Now;
              _ToDate = DateTime.Now;
              _XmlTransfertToAcc = string.Empty;
              _FK_Store = 0;

        }

        public DateTime TransDate
        {
            get { return _TransDate; }
            set { _TransDate = value; }
        }
        public string TransType
        {
            get { return _TransType; }
            set { _TransType = value; }
        }
        public string TransferTo
        {
            get { return _TransferTo; }
            set { _TransferTo = value; }
        }
        public byte Type
        {
            get { return _Type; }
            set { _Type = value; }
        }
        public decimal Amount
        {
            get { return _Amount; }
            set { _Amount = value; }
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
            get { return _ID_WebPharmaTransaction; }
            set { _ID_WebPharmaTransaction = value; }
        }
      
         public string Remarks
        {
            get { return _Remarks; }
            set { _Remarks = value; }
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
        public Int64 FK_Name
        {
            get { return _FK_Name; }
            set { _FK_Name = value; }
        }
        public string Name
        {
            get { return _Name; }
            set { _Name = value; }
        }
        public DateTime FromDate
        {
            get { return _FromDate; }
            set { _FromDate = value; }
        }
        public DateTime ToDate
        {
            get { return _ToDate; }
            set { _ToDate = value; }
        }
        public string XmlTransfertToAcc
        {
            set { _XmlTransfertToAcc = value; }
            get { return _XmlTransfertToAcc; }
        }
        public long FK_Store
        {
            get { return _FK_Store; }
            set { _FK_Store = value; }
        }
        public long insertdata()
        {
            long status;
            DalWebPharmaTransaction dalWebPharmaTransaction = new DalWebPharmaTransaction();

            status = 0;
            try
            {
                _UserAction = 1;
                status = dalWebPharmaTransaction.UpdateWebPharmaTransaction(this);
                dalWebPharmaTransaction = null;
            }
            catch (Exception ex)
            {

            }
            return status;
            
        }

        public DataTable SelectTransactionData()
        {
            DataTable dtbl = new DataTable();
            DalWebPharmaTransaction dalWebPharmaTransaction = new DalWebPharmaTransaction();
            dtbl = dalWebPharmaTransaction.SelectWebPharmaTransaction(this);
            dalWebPharmaTransaction = null;
            return dtbl;
        }

        public void SelectData()
        {
            DalWebPharmaTransaction dalWebPharmaTransaction = new DalWebPharmaTransaction();
            long status = 0;

            IWebPharmaTransaction iIWebPharmaTransactions = (IWebPharmaTransaction)dalWebPharmaTransaction.selectedWebPharmatransactions(this);

            _TransType = iIWebPharmaTransactions.TransType;
            _TransDate = iIWebPharmaTransactions.TransDate;
            _Amount = iIWebPharmaTransactions.Amount;
            _Remarks = iIWebPharmaTransactions.Remarks;
            _Type = iIWebPharmaTransactions.Type;
            Name = iIWebPharmaTransactions.Name;
            FK_Name = iIWebPharmaTransactions.FK_Name;
            _TransferTo = iIWebPharmaTransactions.TransferTo;
            dalWebPharmaTransaction = null;


        }

        public void SelectCustBalanceAmount()
        {
            DalWebPharmaTransaction dalWebPharmaTransaction = new DalWebPharmaTransaction();
            IWebPharmaTransaction iIWebPharmaTransactions = (IWebPharmaTransaction)dalWebPharmaTransaction.SelectBalanceAmount(this);
          _Amount = iIWebPharmaTransactions.Amount;
          _Type = iIWebPharmaTransactions.Type;
        }
     
    }
}
