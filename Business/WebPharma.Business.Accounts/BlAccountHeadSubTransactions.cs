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
    
    public class BlAccountHeadSubTransactions : IAccountHeadSubTransactions , IAccountTransaction
    {
        private Int64 _groupid;
        private DateTime? _EffectDate;
        private long _FK_AccountHead;
		private string _AhCode;
		private string _AhsCode;
        private DateTime? _TransDate;    
        private string _TransType;
        private string _Party;
        private string _Narration;
		private decimal _Amount;
        private string _ChequeNo;
        private DateTime? _ChequeDate;
        private long _FK_AccountHeadSub;     
        private byte _UserAction;
        private Int16 _PageIndex;
        private Int16 _PageSize;
        private long _ID_AccountHeadSubTransaction;   
		private string _TransMode;
		private Boolean _Transfer;		
		private Int64 _VoucherNo;
		private string _Remarks;
		private long _FK_Transaction;
		private string _UserCode;
		private DateTime _EnteredOn;
		private Boolean _Cancelled;
		private string _CancelledUser;
		private DateTime _CancelledOn;
		private long _FK_Store;
		private long _FK_FinYear;
		private long _FK_Client;
        private string _MultiId;
        private string _Description;
        private Int32 _SortOrder;
        private Boolean _Active;
        private string _AccountHolderName;
        private string _AccountNo;
        private Boolean _InterBranch;
        private Int16 _BranchCode;
        private Boolean _OfflineEntry;
        private Int16 _MachineCode;
        private Int16 _BankTransType;
        private long _Bank;
        private long _BankBranch;
        private string _NetBankAccount;
        private Int16 _CreditAccount;
        private long _CustomerNo;
        private long _AccountHead;
        private long _AccountHeadSub;
        private long _InvoiceNo;
        private Int16 _EntryType;
        private DateTime _FromDate;
        private DateTime _ToDate;
        private decimal _CustBalanceAmount;
        private string _SubId;      
        private string _XmlJournalDetails;
        private string _BillNo;
        private string _XmlInvoiceDetails;
        private string _CardNo;      
        private Byte  _PaymentMode;
        private decimal _Commission;
        private long _FK_Party;
        private decimal _BalanceAmount;
        private string _DbName;
        private string _XmlAccountTransMultiple;
        private string _XmlExcelImport;
        private long _Fk_Reason;
        private string _headName;
        private string _Subheadname;
       

		public BlAccountHeadSubTransactions()
		{
			Initialize();
		}
		public void Initialize()

        {
            _groupid = 0;
            _EffectDate = null;
            _FK_AccountHead = 0;
			_ID_AccountHeadSubTransaction = 0;
			_FK_AccountHeadSub = 0;        
			_AhCode = string.Empty;
			_AhsCode = string.Empty;
            _TransDate = null;
			_Amount = 0;
			_TransType = string.Empty;
			_TransMode = string.Empty;
			_Transfer =false;
			_ChequeNo = string.Empty;
            _ChequeDate = null;
			_VoucherNo = 0;
			_Party = string.Empty;
			_Narration = string.Empty;
			_Remarks = string.Empty;
			_FK_Transaction = 0;
			_UserCode = string.Empty;
			_EnteredOn = DateTime.Now;
			_Cancelled = false;
			_CancelledUser = string.Empty;
			_CancelledOn = DateTime.Now;
			_FK_Store = 0;
			_FK_FinYear = 0;
            _MultiId = string.Empty;
            _Description = string.Empty;
            _SortOrder = 0;
            _Active = false;
            _AccountHolderName = string.Empty;
            _AccountNo = string.Empty;
            _InterBranch = false;
            _BranchCode = 0;
            _OfflineEntry = false;
            _MachineCode = 0;
            _FK_Client = 1;
            _PageIndex = 1;
            _PageSize = 10;
            _UserAction = 1;
            _EntryType = 0;
            _FromDate = DateTime.Now;
            _ToDate = DateTime.Now;
            _CustBalanceAmount = 0;
            _SubId = string.Empty;
            _BillNo = string.Empty;
            _XmlInvoiceDetails = string.Empty;
            _CardNo = string.Empty;
            _PaymentMode = 0;
            _Commission = 0;
            _FK_Party = 0;
            _BalanceAmount = 0;
            _DbName = string.Empty;
            _XmlAccountTransMultiple = string.Empty;
            _XmlExcelImport = string.Empty;
            _Fk_Reason = 0;
            _headName = string.Empty;
            _Subheadname = string.Empty;
            
		}
        public string headName
        {
            set { _headName = value; }
            get { return _headName; }
        }
        public string Subheadname
        {
            set { _Subheadname = value; }
            get { return _Subheadname; }
        }
        public Int64 groupid
        {
            get { return _groupid; }
            set { _groupid = value; }
        }
        public string XmlAccountTransMultiple
        {
            set { _XmlAccountTransMultiple = value; }
            get { return _XmlAccountTransMultiple; }
        }

        public long FK_AccountHead
        {
            get { return _FK_AccountHead; }
            set { _FK_AccountHead = value; }
        }
        public DateTime? EffectDate
        {
            get { return _EffectDate; }
            set { _EffectDate = value; }
        }
        public decimal Commission
        {
            get { return _Commission; }
            set { _Commission = value; }
        }
        public Byte PaymentMode
        {
            get { return _PaymentMode; }
            set { _PaymentMode = value; }
        }

        public string CardNo
        {
            get { return _CardNo; }
            set { _CardNo = value; }
        }

        public string XmlInvoiceDetails
        {
            get { return _XmlInvoiceDetails; }
            set { _XmlInvoiceDetails = value; }
        }

        public string DbName
        {
            get { return _DbName; }
            set { _DbName = value; }
        }

        public string BillNo
        {
            get { return _BillNo; }
            set { _BillNo = value; }
        }
      

        public string SubId
        {
            get { return _SubId; }
            set { _SubId = value; }
        
        }
        public decimal CustBalanceAmount
        {
            get { return _CustBalanceAmount; }
            set { _CustBalanceAmount = value; }
        
        }
        public DateTime ToDate
        {
            get { return _ToDate; }
            set { _ToDate = value; }
        }

        public DateTime FromDate
        {
            get { return _FromDate; }
            set { _FromDate = value; }
        }

        public Int16 EntryType
        {
            get { return _EntryType; }
            set { _EntryType = value; }
        }
        public long InvoiceNo
        {
            get { return _InvoiceNo; }
            set { _InvoiceNo = value; }
        }
        public long AccountHeadSub
        {
            get { return _AccountHeadSub; }
            set { _AccountHeadSub = value; }
        }

        public long AccountHead
        {
            get { return _AccountHead; }
            set { _AccountHead = value; }
        }

        public long CustomerNo
        {
            get { return _CustomerNo; }
            set { _CustomerNo = value; }
        }
        public Int16 CreditAccount
        {
            get { return _CreditAccount; }
            set { _CreditAccount = value; }
        }

        public string NetBankAccount
        {
            get { return _NetBankAccount; }
            set { _NetBankAccount = value; }
        }

        public long BankBranch
        {
            get { return _BankBranch; }
            set { _BankBranch = value; }
        }
        public long Bank
        {
            get { return _Bank; }
            set { _Bank = value; }
        }
        public Int16 BankTransType
        {
            get { return _BankTransType; }
            set { _BankTransType = value; }
        }
        public Int16 MachineCode
        {
            get { return _MachineCode; }
            set { _MachineCode = value; }
        }
        public Boolean OfflineEntry
        {
            get { return _OfflineEntry; }
            set { _OfflineEntry = value; }
        }
        public Int16 BranchCode
        {
            get { return _BranchCode; }
            set { _BranchCode = value; }
        }
        public Boolean InterBranch
        {
            get { return _InterBranch; }
            set { _InterBranch = value; }
        }
        public string AccountNo
        {
            get { return _AccountNo; }
            set { _AccountNo = value; }
        }
        public string AccountHolderName
        {
            get { return _AccountHolderName; }
            set { _AccountHolderName = value; }
        }
        public DateTime? TransDate
        {
            get { return _TransDate; }
            set { _TransDate = value; }
        }
       


        public Boolean Active
        {
            get { return _Active; }
            set { _Active = value; }
        }
        public Int32 SortOrder
        {
            get { return _SortOrder; }
            set { _SortOrder = value; }
        }
        public string Description
        {
            get { return _Description; }
            set { _Description = value; }
        }
        public string MultiId
        {
            get { return _MultiId; }
            set { _MultiId = value; }
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
			get { return _ID_AccountHeadSubTransaction; }
			set { _ID_AccountHeadSubTransaction = value; }
		}
		public string TransMode
		{
			get { return _TransMode; }
			set { _TransMode = value; }
		}
		public long FK_AccountHeadSub
		{
			get { return _FK_AccountHeadSub; }
			set { _FK_AccountHeadSub = value; }
		}
       
		public string AhCode
		{
			get { return _AhCode; }
			set { _AhCode = value; }
		}
		public string AhsCode
		{
			get { return _AhsCode; }
			set { _AhsCode = value; }
		}
		public decimal Amount
		{
			get { return _Amount; }
			set { _Amount = value; }
		}
		public string TransType
		{
			get { return _TransType; }
			set { _TransType = value; }
		}
		public Boolean Transfer
		{
			get { return _Transfer; }
			set { _Transfer = value; }
		}
		public string ChequeNo
		{
			get { return _ChequeNo; }
			set { _ChequeNo = value; }
		}
		public DateTime? ChequeDate
		{
			get { return _ChequeDate; }
			set { _ChequeDate = value; }
		}
		public Int64 VoucherNo
		{
			get { return _VoucherNo; }
			set { _VoucherNo = value; }
		}
		public string Party
		{
			get { return _Party; }
			set { _Party = value; }
		}
		public string Narration
		{
			get { return _Narration; }
			set { _Narration = value; }
		}
		public string Remarks
		{
			get { return _Remarks; }
			set { _Remarks = value; }
		}
		public long FK_Transaction
		{
			get { return _FK_Transaction; }
			set { _FK_Transaction = value; }
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
		public Boolean Cancelled
		{
			get { return _Cancelled; }
			set { _Cancelled = value; }
		}
		public string CancelledUser
		{
			get { return _CancelledUser; }
			set { _CancelledUser = value; }
		}
		public DateTime CancelledOn
		{
			get { return _CancelledOn; }
			set { _CancelledOn = value; }
		}
		public long FK_Store
		{
			get { return _FK_Store; }
			set { _FK_Store = value; }
		}
		public long FK_FinYear
		{
			get { return _FK_FinYear; }
			set { _FK_FinYear = value; }
		}
		public long FK_Client
		{
			get { return _FK_Client; }
			set { _FK_Client = value; }
		}
       
        public string XmlJournalDetails
        {
            get { return _XmlJournalDetails; }
            set { _XmlJournalDetails = value; }
        }
        public long FK_Party
        {
            get { return _FK_Party; }
            set { _FK_Party = value; }
        }
        public decimal BalanceAmount
        {
            get { return _BalanceAmount; }
            set { _BalanceAmount = value; }
        }
        public string XmlExcelImport
        {
            set { _XmlExcelImport = value; }
            get { return _XmlExcelImport; }
        }
        public long Fk_Reason
        {
            get { return _Fk_Reason; }
            set { _Fk_Reason = value; }
        }

        public DataTable ValidateExcelData()
        {
            DataTable dtbl = new DataTable();
            DalAccountHeadSubTransaction dalaccountheadsubtransaction = new DalAccountHeadSubTransaction();

            dtbl = dalaccountheadsubtransaction.ValidateExcelData(this);
            dalaccountheadsubtransaction = null;
            return dtbl;
        }
        public long InsertData()
        {
            long status;
            DalAccountHeadSubTransaction  dalaccountheadsubtransaction =new DalAccountHeadSubTransaction();
           
            status = 0;
            try
            {
                _UserAction = 1;
                status = dalaccountheadsubtransaction.UpdateAccountHeadSubTransaction(this);
                dalaccountheadsubtransaction=null;
            }
            catch (Exception ex)
            {
 
            }
            return status;
        }
        public long UpdateData()
        {
            _UserAction = 2;
            DalAccountHeadSubTransaction dalaccountheadsubtransaction = new DalAccountHeadSubTransaction();
            long status;
            status = 0;
            try
            {
                status = dalaccountheadsubtransaction.UpdateAccountHeadSubTransaction(this);
                dalaccountheadsubtransaction = null;
            }
            catch (Exception ex)
            {

            }
            return status;
        }
        public long UpdateAccountHeadSubTransaction()
        {
            _UserAction = 1;
            DalAccountTransaction dalaccounttransaction = new DalAccountTransaction();
            long status;
            status = 0;
            try
            {
                status = dalaccounttransaction.UpdateAccountHeadSubTransaction(this);
                dalaccounttransaction = null;
            }
            catch (Exception ex)
            {

            }
            return status;
        }

        public long DeleteData()
        {
            _UserAction = 3;
            DalAccountHeadSubTransaction dalaccountheadsubtransaction = new DalAccountHeadSubTransaction();
            long status;
            status = 0;
            try
            {
                status = dalaccountheadsubtransaction.DeleteAccountHeadSubTransaction(this);
                dalaccountheadsubtransaction = null;
            }
            catch (Exception ex)
            {

            }
            return status;
        }
        public decimal OustandingBalance()
        {
            DalAccountHeadSubTransaction dalaccountheadsubtransaction = new DalAccountHeadSubTransaction();
            decimal status;
            status = 0;
            try
            {
                status = dalaccountheadsubtransaction.OustandingBalance(this);
                dalaccountheadsubtransaction = null;
            }
            catch (Exception ex)
            {

            }
            return status;
        }
        public decimal GetAccountBalance()
        {
            DalAccountHeadSubTransaction dalaccountheadsubtransaction = new DalAccountHeadSubTransaction();
            decimal status;
            status = 0;
            try
            {
                status = dalaccountheadsubtransaction.GetBalance(this);
                dalaccountheadsubtransaction = null;
            }
            catch (Exception ex)
            {

            }
            return status;
        }
        public void SelectData()
        {
            _UserAction = 1;
            DalAccountHeadSubTransaction dalaccountheadsubtransaction = new DalAccountHeadSubTransaction();
            IAccountHeadSubTransactions iaccountheadsubtransaction;
            iaccountheadsubtransaction = (IAccountHeadSubTransactions)dalaccountheadsubtransaction.SelectAccountHeadSubTransaction(this);
            //iaccountheadsubtransaction = (IAccountHeadTransactions)dalaccountheadsubtransaction.SelectAccountHeadSubTransaction(this);
            _FK_AccountHeadSub = iaccountheadsubtransaction.FK_AccountHeadSub;
            _AhCode = iaccountheadsubtransaction.AhCode;
            _AhsCode = iaccountheadsubtransaction.AhsCode;
            _TransDate = iaccountheadsubtransaction.TransDate;
            _Amount = iaccountheadsubtransaction.Amount;
            _TransType = iaccountheadsubtransaction.TransType;
            _TransMode = iaccountheadsubtransaction.TransMode;
            _Transfer = iaccountheadsubtransaction.Transfer;
            _ChequeNo = iaccountheadsubtransaction.ChequeNo;
            _ChequeDate = iaccountheadsubtransaction.ChequeDate;
            _VoucherNo = iaccountheadsubtransaction.VoucherNo;
            _Party = iaccountheadsubtransaction.Party;
            _Narration = iaccountheadsubtransaction.Narration;
            _Remarks = iaccountheadsubtransaction.Remarks;
            _FK_Transaction = iaccountheadsubtransaction.FK_Transaction;
            _Cancelled = iaccountheadsubtransaction.Cancelled;
            _CancelledUser = iaccountheadsubtransaction.CancelledUser;
            _CancelledOn = iaccountheadsubtransaction.CancelledOn;
            _FK_Store = iaccountheadsubtransaction.FK_Store;
            _FK_FinYear = iaccountheadsubtransaction.FK_FinYear;
            _headName = iaccountheadsubtransaction.headName;
            _Subheadname = iaccountheadsubtransaction.Subheadname;

        }

        public void SelectMultipleData()
        {
            _UserAction = 2;
            DalAccountHeadSubTransaction dalaccountheadsubtransaction = new DalAccountHeadSubTransaction();
            IAccountHeadSubTransactions iaccountheadsubtransaction;
            iaccountheadsubtransaction = (IAccountHeadSubTransactions)dalaccountheadsubtransaction.SelectAccountHeadSubTransaction(this);
            //iaccountheadsubtransaction = (IAccountHeadTransactions)dalaccountheadsubtransaction.SelectAccountHeadSubTransaction(this);
            _FK_AccountHeadSub = iaccountheadsubtransaction.FK_AccountHeadSub;
            _AhCode = iaccountheadsubtransaction.AhCode;
            _AhsCode = iaccountheadsubtransaction.AhsCode;
            _TransDate = iaccountheadsubtransaction.TransDate;
            _Amount = iaccountheadsubtransaction.Amount;
            _TransType = iaccountheadsubtransaction.TransType;
            _TransMode = iaccountheadsubtransaction.TransMode;
            _Transfer = iaccountheadsubtransaction.Transfer;
            _ChequeNo = iaccountheadsubtransaction.ChequeNo;
            _ChequeDate = iaccountheadsubtransaction.ChequeDate;
            _VoucherNo = iaccountheadsubtransaction.VoucherNo;
            _Party = iaccountheadsubtransaction.Party;
            _Narration = iaccountheadsubtransaction.Narration;
            _Remarks = iaccountheadsubtransaction.Remarks;
            _FK_Transaction = iaccountheadsubtransaction.FK_Transaction;
            _Cancelled = iaccountheadsubtransaction.Cancelled;
            _CancelledUser = iaccountheadsubtransaction.CancelledUser;
            _CancelledOn = iaccountheadsubtransaction.CancelledOn;
            _FK_Store = iaccountheadsubtransaction.FK_Store;
            _FK_FinYear = iaccountheadsubtransaction.FK_FinYear;
            _groupid = iaccountheadsubtransaction.groupid;
        }
        public DataTable SelectAllData()
        {
            _UserAction = 1;
            DataTable dtbl = new DataTable();
            DalAccountHeadSubTransaction dalAccountHeadSubTransaction = new DalAccountHeadSubTransaction();
            dtbl = dalAccountHeadSubTransaction.SelectAllAccountHeadSubTransaction(this);
            dalAccountHeadSubTransaction = null;
            return dtbl;
        }
        public DataTable SelectallmultipleData()
        {
            _UserAction = 2;
            DataTable dtbl = new DataTable();
            DalAccountHeadSubTransaction dalAccountHeadSubTransaction = new DalAccountHeadSubTransaction();
            dtbl = dalAccountHeadSubTransaction.SelectAllAccountHeadSubTransaction(this);
            dalAccountHeadSubTransaction = null;
            return dtbl;
        }
        public long InsertAccountTransaction()
        {
            long status;
            DalAccountTransaction dalaccounttransaction = new DalAccountTransaction();
            status = 0;
            try
            {
                _UserAction = 1;
                status = dalaccounttransaction.UpdateAccountTransaction(this);
                dalaccounttransaction = null;
            }
            catch (Exception ex)
            {

            }
            return status;
        }
        public long InsertDataMultiple()
        {
            long status;
            DalAccountHeadSubTransaction dalaccountheadsubtransaction = new DalAccountHeadSubTransaction();
            status = 0;
            try
            {
                _UserAction = 1;
                status = dalaccountheadsubtransaction.UpdateAccountHeadSubTransMultiple(this);
            }
            catch (Exception ex)
            {

            }
            finally
            {
                dalaccountheadsubtransaction = null;
            }
            return status;
        }
        
        public long UpdateAccountTransaction()
        {
            _UserAction = 2;
            DalAccountTransaction dalaccounttransaction = new DalAccountTransaction();
            long status;
            status = 0;
            try
            {
                status = dalaccounttransaction.UpdateAccountTransaction(this);
                dalaccounttransaction = null;
            }
            catch (Exception ex)
            {

            }
            return status;
        }
        public void SelectAccountData()
        {
            DalAccountTransaction dalaccounttransaction = new DalAccountTransaction();
            IAccountTransaction iaccounttransaction;
            iaccounttransaction = (IAccountTransaction)dalaccounttransaction.SelectAccountTransaction(this);
            _TransType = iaccounttransaction.TransType;
            _TransDate = iaccounttransaction.TransDate;
            _VoucherNo = iaccounttransaction.VoucherNo;
            _BankTransType = iaccounttransaction.BankTransType;
           _Bank = iaccounttransaction.Bank;
           _BankBranch = iaccounttransaction.BankBranch;
            _ChequeNo = iaccounttransaction.ChequeNo;
            _ChequeDate = iaccounttransaction.ChequeDate;
          _NetBankAccount = iaccounttransaction.NetBankAccount;
           // _CreditAccount = iaccounttransaction.CreditAccount;
            //_InvoiceNo = iaccounttransaction.InvoiceNo;
            _CustomerNo = iaccounttransaction.CustomerNo;
            _AccountHead = iaccounttransaction.AccountHead;
            _AccountHeadSub = iaccounttransaction.AccountHeadSub;
            _Amount = iaccounttransaction.Amount;
            _Remarks = iaccounttransaction.Remarks;
            //_Commission = iaccounttransaction.Commission;
        }
        public void SelectCustBalanceAmount()
        {
            DalAccountHeadSubTransaction dalaccountheadsubtransaction = new DalAccountHeadSubTransaction();
            IAccountHeadSubTransactions iaccountheadsubtransactions;
            iaccountheadsubtransactions = (IAccountHeadSubTransactions)dalaccountheadsubtransaction.SelectBalanceAmount(this);
            _BalanceAmount = iaccountheadsubtransactions.BalanceAmount;
        }


        public DataTable SelectMutipleTransaction()
        {
            DalAccountHeadSubTransaction dalaccountheadsubtransaction = new DalAccountHeadSubTransaction();
            DataTable dt = new DataTable();
            try
            {
                dt = dalaccountheadsubtransaction.SelectAccountHeadSubMutipleTransaction(this);
            }
            catch
            {
                dt = new DataTable();
            }
            finally
            {

                dalaccountheadsubtransaction = null;
            }
            return dt;
        }

        
        //public DataTable selectAccountsubhead()
        //{
        //    DataTable dtbl = new DataTable();
        //    DalAccountHeadSubTransaction DalAccount = new DalAccountHeadSubTransaction();
        //    dtbl = DalAccount.SelectAccountHeadSub(this);
        //    DalAccount = null;
        //    return dtbl;
        //}

        public ArrayList GetBalance()
        {
            DalAccountHeadSubTransaction dalaccountheadsubtransaction = new DalAccountHeadSubTransaction();
            return dalaccountheadsubtransaction.GetAccountHeadSubBalance(this);
        }




        public long DeleteMultipleData()
        {
            _UserAction = 3;
            DalAccountHeadSubTransaction dalaccountheadsubtransaction = new DalAccountHeadSubTransaction();
            long status;
            status = 0;
            try
            {
                status = dalaccountheadsubtransaction.DeleteMultipleAccountHeadSubTransaction(this);
                dalaccountheadsubtransaction = null;
            }
            catch (Exception ex)
            {

            }
            return status;
        }
    }
    
    public class BlAccountHeadSubTransactionDetails
    {
        private decimal _Amount;
        private string _Remarks;
        private long _FK_Sales;
        private string _InvoiveNo;
        private Decimal _Commission;
        private string _CardNo;
        private string _ChequeNo;
        private DateTime _ChequeDate;
      
       
       
        
        public BlAccountHeadSubTransactionDetails()
        {
            Initialize();
        }
        public void Initialize()
        {
            _Amount = 0;
            _Remarks = string.Empty;
            _FK_Sales = 0;
            _InvoiveNo = string.Empty;
            _Commission = 0;
            _CardNo = string.Empty;
            _ChequeDate = DateTime.Now;
            _ChequeNo = string.Empty;

          
        }

        public string ChequeNo
        {
            get { return _ChequeNo; }
            set { _ChequeNo = value; }
        }


        public DateTime ChequeDate
        {
            get { return _ChequeDate; }
            set { _ChequeDate = value; }
        }

        public string CardNo
        {
            get { return _CardNo; }
            set { _CardNo = value; }
        }   

        public Decimal Commission
        {
            get { return _Commission; }
            set { _Commission = value; }
        }
     

        public decimal Amount
        {
            get { return _Amount; }
            set { _Amount = value; }
        }

        public string Remarks
        {
            get { return _Remarks; }
            set { _Remarks = value; }
        }

        public long FK_Sales
        {
            get { return _FK_Sales; }
            set { _FK_Sales = value; }
        }

        public string InvoiveNo
        {
            get { return _InvoiveNo; }
            set { _InvoiveNo = value; }
        }
    }
    public class AccountHeadSubTransMultiples
    {
        private Int32 _AhCode;
        private long _FK_AccountHeadSub;
        private string _AhName;
        private string _FK_AccountHeadSubName;
        private decimal _AhstAmount;
        private string _TransType;
        private string _AhstNarration;
        private string _AhstParty;
        private string _CheckDate;
        private string _CheckNo;
        private string _TaxDetails;
        private string _AhstTaxAmount;

        private string _Billdate;
        private string _BillNo;
        private string _TransDate;
        private string _EffectDate;
        private Boolean _Transfer;	

        public Int32 AhCode
        {
            get { return _AhCode; }
            set { _AhCode = value; }
        }
        public long FK_AccountHeadSub
        {
            get { return _FK_AccountHeadSub; }
            set { _FK_AccountHeadSub = value; }
        }
        public decimal AhstAmount
        {
            get { return _AhstAmount; }
            set { _AhstAmount = value; }
        }
        public string TransType
        {
            get { return _TransType; }
            set { _TransType = value; }
        }
        public string AhstNarration
        {
            get { return _AhstNarration; }
            set { _AhstNarration = value; }
        }
        public string AhstParty
        {
            get { return _AhstParty; }
            set { _AhstParty = value; }
        }
        public string CheckDate
        {
            get { return _CheckDate; }
            set { _CheckDate = value; }
        }
        public string CheckNo
        {
            get { return _CheckNo; }
            set { _CheckNo = value; }
        }
        public string TaxDetails
        {
            get { return _TaxDetails; }
            set { _TaxDetails = value; }
        }
        public string AhstTaxAmount
        {
            get { return _AhstTaxAmount; }
            set { _AhstTaxAmount = value; }
        }
        public string AhName
        {
            get { return _AhName; }
            set { _AhName = value; }
        }
        public string FK_AccountHeadSubName
        {
            get { return _FK_AccountHeadSubName; }
            set { _FK_AccountHeadSubName = value; }
        }

        public string Billdate
        {
            get { return _Billdate; }
            set { _Billdate = value; }
        }
        public string BillNo
        {
            get { return _BillNo; }
            set { _BillNo = value; }
        }
        public string TransDate
        {
            get { return _TransDate; }
            set { _TransDate = value; }
        }
        public string EffectDate
        {
            get { return _EffectDate; }
            set { _EffectDate = value; }
        }
        public Boolean Transfer
        {
            get { return _Transfer; }
            set { _Transfer = value; }
        }

    }
    public class BlbankToAcc
    {
        private long _Bankno;
        private string _ChequeNumber;
        private DateTime? _ChequeDates;
        private string _XmlBankToAcc;

        public DateTime? ChequeDates
        {
            get { return _ChequeDates; }
            set { _ChequeDates = value; }
        }
        public string ChequeNumber
        {
            get { return _ChequeNumber; }
            set { _ChequeNumber = value; }
        }
        public long Bankno
        {
            get { return _Bankno; }
            set { _Bankno = value; }
        }
        public string XmlBankToAcc
        {
            set { _XmlBankToAcc = value; }
            get { return _XmlBankToAcc; }
        }
    }
}


