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
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebPharma.DataAccess.WebPharma;
using WebPharma.Interface.WebPharma;

namespace WebPharma.Business.WebPharma
{
    public class BlDayEnd : IDayEnd
    {
        private byte _UserAction;
        private ArrayList _ErrorList;
        private const string MODULENAME = "DayEnd";
        private const string TABLENAME = "DayEnd";
        private Int32 _FKReason;
        private Int32 _FKMachine;
        private string _DbName;
        private Int16 _SortOrder;
        private string _SqlUpdateQuery;
        private string _AuditData;
        private string _TransMode;
        private Int64 _ID_DayEnd;
        private DateTime _DbDate;
        private DateTime _DbPreviousDate;
        private string _OfficerCode;
        private DateTime _OfficerDate;
        private Boolean _Cancelled;
        private Boolean _Passed;
        private Int16 _BranchCode;
        private Int16 _BranchCodeUser;
        private string _UserCode;
        private DateTime _UserDate;
        private Boolean _OfflineEntry;
        private string _XmlAttendance;
        private string _DBCheckSum;
        private Int64 _FK_Client;
        private Int16 _MachineCode;
        private Int64 _FK_Store;
        private DateTime _FromDate;
        private decimal _openingbalance;
        private decimal _Closingbalance;
        private DateTime _ClosingDate;
        private decimal _Liability;
        private decimal _Stockvalue;
        private decimal _PurchaseLiability;
        private decimal _PurchaseAmount;
        private decimal _PReturnLiability;
        private decimal _PReturnAmount;
        private decimal _SalesLiability;
        private decimal _SalesAmount;
        private decimal _SReturnLiability;
        private decimal _SReturnAmount;
        private decimal _IndentFrom;
        private decimal _IndentTo;
        private decimal _DamageStock;
        private decimal _Receipt;
        private decimal _Payment;
        private decimal _ClosingLiability;
        //private decimal _openingbalance;
       // private decimal _openingliability;
        //private decimal _AmntDifference;

        public BlDayEnd()
        {
            Initialize();
        }
        public void Initialize()
        {

            //_openingbalance = 0;
          //  _openingliability = 0;
          //  _AmntDifference = 0;
            _MachineCode = 0;
            _ID_DayEnd= 0;
            _DbDate = DateTime.Now;
            _DbPreviousDate = DateTime.Now;
            _OfficerCode = string.Empty;
            _OfficerDate = DateTime.Now;
            _Cancelled = false;
            _Passed = false;
            _BranchCode = 0;
            _BranchCodeUser = 0;
            _UserCode = string.Empty;
            _UserDate = DateTime.Now;
            _OfflineEntry = false;
            _FK_Client = 1;
            //    _UserAction = (byte)BlPublic.UserAction.None; ;
            _ErrorList = new ArrayList();
            _DbName = string.Empty;
            _FKReason = 0;
            _FKMachine = 0;
            _SqlUpdateQuery = string.Empty;
            _AuditData = string.Empty;
            _TransMode = string.Empty;
            _XmlAttendance = string.Empty;
            _DBCheckSum = string.Empty;
            _UserAction = 0;
            _FK_Store = 0;
            _FromDate = DateTime.Now;
            _ClosingDate = DateTime.Now;
            _Liability = 0;
            _Stockvalue = 0;
            _PurchaseLiability = 0;
            _PurchaseAmount = 0;
            _PReturnLiability = 0;
            _PReturnAmount = 0;
            _SalesLiability = 0;
            _SalesAmount = 0;
            _SReturnLiability = 0;
            _SReturnAmount = 0;
            _IndentFrom = 0;
            _IndentTo = 0;
            _DamageStock = 0;
            _Receipt = 0;
            _Payment = 0;
            _ClosingLiability = 0;
            _Closingbalance = 0;

        }

        public decimal Closingbalance
        {
            get { return _Closingbalance; }
            set { _Closingbalance = value; }
        }
        public decimal ClosingLiability
        {
            get { return _ClosingLiability; }
            set { _ClosingLiability = value; }
        }

        public decimal Payment
        {
            get { return _Payment; }
            set { _Payment = value; }
        }
        public decimal Receipt
        {
            get { return _Receipt; }
            set { _Receipt = value; }
        }
        public decimal PurchaseLiability
        {
            get { return _PurchaseLiability; }
            set { _PurchaseLiability = value; }
        }
        public decimal PurchaseAmount
        {
            get { return _PurchaseAmount; }
            set { _PurchaseAmount = value; }
        }
        public decimal PReturnLiability
        {
            get { return _PReturnLiability; }
            set { _PReturnLiability = value; }
        }
        public decimal PReturnAmount
        {
            get { return _PReturnAmount; }
            set { _PReturnAmount = value; }
        }
        public decimal SalesLiability
        {
            get { return _SalesLiability; }
            set { _SalesLiability = value; }
        }
        public decimal SalesAmount
        {
            get { return _SalesAmount; }
            set { _SalesAmount = value; }
        }
        public decimal SReturnLiability
        {
            get { return _SReturnLiability; }
            set { _SReturnLiability = value; }
        }
        public decimal SReturnAmount
        {
            get { return _SReturnAmount; }
            set { _SReturnAmount = value; }
        }
        public decimal IndentFrom
        {
            get { return _IndentFrom; }
            set { _IndentFrom = value; }
        }
        public decimal IndentTo
        {
            get { return _IndentTo; }
            set { _IndentTo = value; }
        }
        public decimal DamageStock
        {
            get { return _DamageStock; }
            set { _DamageStock = value; }
        }
        public DateTime ClosingDate
        {
            get { return _ClosingDate; }
            set { _ClosingDate = value; }
        }
        public Decimal Liability
        {
            get { return _Liability; }
            set { _Liability = value; }
        }
        public Decimal Stockvalue
        {
            get { return _Stockvalue; }
            set { _Stockvalue = value; }
        }
        public Int16 MachineCode
        {
            get { return _MachineCode; }
            set { _MachineCode = value; }
        }
        public Int64 FK_Client
        {
            get { return _FK_Client; }
            set { _FK_Client = value; }
        }

        public string DBCheckSum
        {
            get { return _DBCheckSum; }
            set { _DBCheckSum = value; }
        }
        public string XmlAttendance
        {
            get { return _XmlAttendance; }
            set { _XmlAttendance = value; }
        }
        public ArrayList ErrorList
        {
            get { return _ErrorList; }
        }
        public byte UserAction
        {
            get { return _UserAction; }
            set { _UserAction = value; }
        }
        public string ModuleName
        {
            get { return MODULENAME; }
        }
        public string TableName
        {
            get { return TABLENAME; }
        }
        //public Int32 MachineCode
        //{
        //    get { return _FKMachine; }
        //    set { _FKMachine = value; }
        //}
        public Int32 ReasonCode
        {
            get { return _FKReason; }
            set { _FKReason = value; }
        }
        public string DbName
        {
            get { return _DbName; }
            set { _DbName = value; }
        }

        public Int64 MasterID
        {
            get { return _ID_DayEnd; }
            set { _ID_DayEnd = value; }
        }
        public Int16 SortOrder
        {
            get { return _SortOrder; }
            set { _SortOrder = value; }
        }
        public string SqlUpdateQuery
        {
            get { return _SqlUpdateQuery; }
            set { _SqlUpdateQuery = value; }
        }
        public string AuditData
        {
            get { return _AuditData; }
            set { _AuditData = value; }
        }
        public string TransMode
        {
            get { return _TransMode; }
            set { _TransMode = value; }
        }
        public DateTime DbDate
        {
            get { return _DbDate; }
            set { if (CheckData("DbDate", value)) _DbDate = value; }
        }
        public DateTime DbPreviousDate
        {
            get { return _DbPreviousDate; }
            set { if (CheckData("DbPreviousDate", value)) _DbPreviousDate = value; }
        }
        public string OfficerCode
        {
            get { return _OfficerCode; }
            set { if (CheckData("OfficerCode", value)) _OfficerCode = value; }
        }
        public DateTime OfficerDate
        {
            get { return _OfficerDate; }
            set { if (CheckData("OfficerDate", value)) _OfficerDate = value; }
        }
        public Boolean Cancelled
        {
            get { return _Cancelled; }
            set { if (CheckData("Cancelled", value)) _Cancelled = value; }
        }
        public Boolean Passed
        {
            get { return _Passed; }
            set { if (CheckData("Passed", value)) _Passed = value; }
        }
        public Int16 BranchCodeUser
        {
            get { return _BranchCodeUser; }
            set { if (CheckData("BranchCodeUser", value)) _BranchCodeUser = value; }
        }
        public string UserCode
        {
            get { return _UserCode; }
            set { if (CheckData("UserCode", value)) _UserCode = value; }
        }
        public DateTime UserDate
        {
            get { return _UserDate; }
            set { if (CheckData("UserDate", value)) _UserDate = value; }
        }
        public Boolean OfflineEntry
        {
            get { return _OfflineEntry; }
            set { if (CheckData("OfflineEntry", value)) _OfflineEntry = value; }
        }
        public Int16 BranchCode
        {
            get { return _BranchCode; }
            set { _BranchCode = value; }
        }
        public Int64 FK_Store
        {
            get { return _FK_Store; }
            set { _FK_Store = value; }
        }
        public DateTime FromDate
        {
            get { return _FromDate; }
            set { _FromDate = value; }
        }
        public decimal openingbalance
        {
            get { return _openingbalance; }
            set { _openingbalance = value; }
        }

        //public decimal openingbalance
        //{
        //    get { return _openingbalance; }
        //    set { _openingbalance = value; }
        //}
        //public decimal openingliability
        //{
        //    get { return _openingliability; }
        //    set { _openingliability = value; }
        //}
        //public decimal AmntDifference
        //{
        //    get { return _AmntDifference; }
        //    set { _AmntDifference = value; }
        //}


        public bool CheckData(string fieldName, object value)
        {
            string errorMessage;
            errorMessage = "";
            switch (fieldName)
            {
                case "DbDate":
                    // errorMessage = "DbDate Already Exists";
                    break;
                case "DbPreviousDate":
                    // errorMessage = "DbPreviousDate Already Exists";
                    break;
                case "OfficerCode":
                    // errorMessage = "OfficerCode Already Exists";
                    break;
                case "OfficerDate":
                    // errorMessage = "OfficerDate Already Exists";
                    break;
                case "Cancelled":
                    // errorMessage = "Cancelled Already Exists";
                    break;
                case "Passed":
                    // errorMessage = "Passed Already Exists";
                    break;
                case "BranchCode":
                    // errorMessage = "BranchCode Already Exists";
                    break;
                case "BranchCodeUser":
                    // errorMessage = "BranchCodeUser Already Exists";
                    break;
                case "UserCode":
                    // errorMessage = "UserCode Already Exists";
                    break;
                case "UserDate":
                    // errorMessage = "UserDate Already Exists";
                    break;
                case "OfflineEntry":
                    // errorMessage = "OfflineEntry Already Exists";
                    break;

                default:
                    break;
            }
            if (errorMessage != "")
            {
                _ErrorList.Add(errorMessage.ToString());
                return false;
            }
            else
                return true;
        }

        public long InsertData()
        {


            long status;
            DalDayEnd dalDayEnd = new DalDayEnd();

            status = 0;
            try
            {
                _UserAction = 1;
                status = dalDayEnd.UpdateDayEnd(this);
                dalDayEnd = null;
            }
            catch (Exception ex)
            {

            }
            return status;
        }




        public DataSet SelectGridData()
        {
            DataSet dtbl = new DataSet();
            DalDayEnd dalend = new DalDayEnd();
            dtbl = dalend.SelectGriddata(this);
            dalend = null;
            return dtbl;
        }
        public DataSet LiabilityDetails()
        {
            DataSet dtbl = new DataSet();
            DalDayEnd dalend = new DalDayEnd();
            dtbl = dalend.LiabilityDetails(this);
            dalend = null;
            return dtbl;
        }
        public DataSet Payementreceiptdetails()
        {
            DataSet dtbl = new DataSet();
            DalDayEnd dalend = new DalDayEnd();
            dtbl = dalend.Payementreceiptdetails(this);
            dalend = null;
            return dtbl;
        }


        public long InsertDayendData()
        {


            long status;
            DalDayEnd dalDayEnd = new DalDayEnd();

            status = 0;
            try
            {
                _UserAction = 1;
                status = dalDayEnd.UpdateDayEndData(this);
                dalDayEnd = null;
            }
            catch (Exception ex)
            {

            }
            return status;
        }

    }
}
