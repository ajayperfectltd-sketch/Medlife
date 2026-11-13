using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebPharma.DataAccess.Accounts;
using WebPharma.Interface.Accounts;

namespace WebPharma.Business.Accounts
{
    public class BlOpeningbalance : IOpeningBalance
    {
        private DateTime _TransDate;
        private string _UserCode;
        private DateTime _EnteredOn;
        private long _FK_Client;
        private long _FK_Store;
        private byte _UserAction;
        private Int16 _PageIndex;
        private Int16 _PageSize;
        private string _FinalGroup;
        private Int32 _FK_AccountGroup;
        private Int32 _FK_AccountSubGroup;
        private Int32 _FinYear;
        private string _XmlAccountBalance;
        private Int32 _BranchCode;

        public BlOpeningbalance()
        {
            Initialize();
        }
        public void Initialize()
        {
            _TransDate = DateTime.Now;
            _UserCode = string.Empty;
            _EnteredOn = DateTime.Now;
            _FK_Client = 1;
            _FK_Store = 0;
            _UserAction = 1;
            _PageIndex = 1;
            _PageSize = 10;
            _FinalGroup = string.Empty;
            _FK_AccountGroup = 0;
            _FK_AccountSubGroup = 0;
            _FinYear = 0;
            _XmlAccountBalance = string.Empty;
            _BranchCode = 0;
        }
        public Int32 BranchCode
        {
            get { return _BranchCode; }
            set { _BranchCode = value; }
        }
        public string XmlAccountBalance
        {
            get { return _XmlAccountBalance; }
            set { _XmlAccountBalance = value; }
        }
        public DateTime TransDate
        {
            get { return _TransDate; }
            set { _TransDate = value; }
        }
        public string FinalGroup
        {
            get { return _FinalGroup; }
            set { _FinalGroup = value; }
        }
        public Int32 FK_AccountGroup
        {
            get { return _FK_AccountGroup; }
            set { _FK_AccountGroup = value; }
        }
        public Int32 FK_AccountSubGroup
        {
            get { return _FK_AccountSubGroup; }
            set { _FK_AccountSubGroup = value; }
        }
        public Int32 FinYear
        {
            get { return _FinYear; }
            set { _FinYear = value; }
        }
        public string UserCode
        {
            get { return _UserCode; }
            set { _UserCode = value; }
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
        public long FK_Store
        {
            get { return _FK_Store; }
            set { _FK_Store = value; }
        }
        public DataTable FillFinYear()
        {
            DataTable dtbl = new DataTable();
            DalOpeningBalance dalopening = new DalOpeningBalance();
            dtbl = dalopening.FillFinyaer(this);
            dalopening = null;
            return dtbl;
        }
        public DataTable Accountbalancegrid()
        {
            DataTable dtbl = new DataTable();
            DalOpeningBalance dalopening = new DalOpeningBalance();
            dtbl = dalopening.Accountbalancegrid(this);
            dalopening = null;
            return dtbl;
        }
        public long InsertData()
        {
            long status;
            DalOpeningBalance dalopening = new DalOpeningBalance();
            status = 0;
            try
            {
                _UserAction = 1;
                status = dalopening.UpdateOpeningbalance(this);
                dalopening = null;
            }
            catch (Exception ex)
            {

            }
            return status;
        }
    }
    public class AccountHeadBalance
    {

        private Int64 _AhCode;
        private decimal _AhBalance;

        public Int64 AhCode
        {
            get { return _AhCode; }
            set { _AhCode = value; }
        }

        public decimal AhBalance
        {
            get { return _AhBalance; }
            set { _AhBalance = value; }
        }






    }
}
