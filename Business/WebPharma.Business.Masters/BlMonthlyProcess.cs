using System;
using System.Data;
using WebPharma.Interface.General;
using WebPharma.Interface.Masters;
using WebPharma.DataAccess.Masters;

namespace WebPharma.Business.Masters
{
    public class BlMonthlyProcess : IMonthlyProcess
    {
        public BlMonthlyProcess()
        {
            Initialize();
        }

        public void Initialize()
        {
            _FK_Store = 0;
            _ID_MonthlyProcess = 0;

            _FromDate = DateTime.Now;
            _ToDate = DateTime.Now;
            _ProcessDate = DateTime.Now;

            _Opening = 0;
            _Receipt = 0;
            _Payment = 0;
            _Closing = 0;

            _PurchaseAmount = 0;
            _PurchaseLiability = 0;
            _PReturnAmount = 0;
            _PReturnLiability = 0;

            _SalesAmount = 0;
            _SalesLiability = 0;
            _SReturnAmount = 0;
            _SReturnLiability = 0;

            _DamageStock = 0;
            _IndentFrom = 0;
            _IndentTo = 0;

            _StockValue = 0;
            _ClosingLiability = 0;

            _Remarks = string.Empty;

            _UserAction = 1;
            _UserCode = string.Empty;

            EnteredOn = DateTime.Now;
            FK_Client = 1;
            PageIndex = 1;
            PageSize = 10;
        }
        private Int64 _FK_Store;
        private Int64 _ID_MonthlyProcess;

        private DateTime _FromDate;
        private DateTime _ToDate;
        private DateTime _ProcessDate;

        private Decimal _Opening;
        private Decimal _Receipt;
        private Decimal _Payment;
        private Decimal _Closing;

        private Decimal _PurchaseAmount;
        private Decimal _PurchaseLiability;
        private Decimal _PReturnAmount;
        private Decimal _PReturnLiability;

        private Decimal _SalesAmount;
        private Decimal _SalesLiability;
        private Decimal _SReturnAmount;
        private Decimal _SReturnLiability;

        private Decimal _DamageStock;
        private Decimal _IndentFrom;
        private Decimal _IndentTo;

        private Decimal _StockValue;
        private Decimal _ClosingLiability;

        private string _Remarks;

        private Byte _UserAction;

        private string _UserCode;

        private Int64 _FK_Client;

        private DateTime _EnteredOn;

        private Int16 _PageIndex;

        private Int16 _PageSize;

        private Int64 _MasterId;


        public Int64 FK_Store
        {
            get { return _FK_Store; }
            set { _FK_Store = value; }
        }

        public Int64 ID_MonthlyProcess
        {
            get { return _ID_MonthlyProcess; }
            set { _ID_MonthlyProcess = value; }
        }

        public Int64 MasterId
        {
            get { return _MasterId; }
            set { _MasterId = value; }
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

        public DateTime ProcessDate
        {
            get { return _ProcessDate; }
            set { _ProcessDate = value; }
        }

        public Decimal Opening
        {
            get { return _Opening; }
            set { _Opening = value; }
        }

        public Decimal Receipt
        {
            get { return _Receipt; }
            set { _Receipt = value; }
        }

        public Decimal Payment
        {
            get { return _Payment; }
            set { _Payment = value; }
        }

        public Decimal Closing
        {
            get { return _Closing; }
            set { _Closing = value; }
        }

        public Decimal PurchaseAmount
        {
            get { return _PurchaseAmount; }
            set { _PurchaseAmount = value; }
        }

        public Decimal PurchaseLiability
        {
            get { return _PurchaseLiability; }
            set { _PurchaseLiability = value; }
        }

        public Decimal PReturnAmount
        {
            get { return _PReturnAmount; }
            set { _PReturnAmount = value; }
        }

        public Decimal PReturnLiability
        {
            get { return _PReturnLiability; }
            set { _PReturnLiability = value; }
        }

        public Decimal SalesAmount
        {
            get { return _SalesAmount; }
            set { _SalesAmount = value; }
        }

        public Decimal SalesLiability
        {
            get { return _SalesLiability; }
            set { _SalesLiability = value; }
        }

        public Decimal SReturnAmount
        {
            get { return _SReturnAmount; }
            set { _SReturnAmount = value; }
        }

        public Decimal SReturnLiability
        {
            get { return _SReturnLiability; }
            set { _SReturnLiability = value; }
        }

        public Decimal DamageStock
        {
            get { return _DamageStock; }
            set { _DamageStock = value; }
        }

        public Decimal IndentFrom
        {
            get { return _IndentFrom; }
            set { _IndentFrom = value; }
        }

        public Decimal IndentTo
        {
            get { return _IndentTo; }
            set { _IndentTo = value; }
        }

        public Decimal StockValue
        {
            get { return _StockValue; }
            set { _StockValue = value; }
        }

        public Decimal ClosingLiability
        {
            get { return _ClosingLiability; }
            set { _ClosingLiability = value; }
        }

        public string Remarks
        {
            get { return _Remarks; }
            set { _Remarks = value; }
        }

        public Byte UserAction
        {
            get { return _UserAction; }
            set { _UserAction = value; }
        }

        public string UserCode
        {
            get { return _UserCode; }
            set { _UserCode = value; }
        }

        public Int64 FK_Client
        {
            get { return _FK_Client; }
            set { _FK_Client = value; }
        }

        public DateTime EnteredOn
        {
            get { return _EnteredOn; }
            set { _EnteredOn = value; }
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







        public DataTable GetActiveBusinessDates()
        {
            DalMonthlyProcess dlMonthlyProcess = new DalMonthlyProcess();

            try
            {
                DataTable dt = new DataTable();
                dt = dlMonthlyProcess.GetActiveBusinessDates(this);
                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataSet GetTransactionDetails()
        {
            DalMonthlyProcess dlMonthlyProcess = new DalMonthlyProcess();

            try
            {
                DataSet ds = new DataSet();
                ds = dlMonthlyProcess.GetTransactionDetails(this);
                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable GetGridDetails()
        {
            DalMonthlyProcess dlMonthlyProcess = new DalMonthlyProcess();

            try
            {
                DataTable dt = new DataTable();
                dt = dlMonthlyProcess.GetGridDetails(this);
                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataSet GetPaymentReceiptDetails()
        {
            DalMonthlyProcess dlMonthlyProcess = new DalMonthlyProcess();

            try
            {
                DataSet ds = new DataSet();
                ds = dlMonthlyProcess.GetPaymentReceiptDetails(this);
                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataSet GetCashLiabilitySummary()
        {
            DalMonthlyProcess dlMonthlyProcess = new DalMonthlyProcess();
            try
            {
                return dlMonthlyProcess.GetCashLiabilitySummary(this);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable GetPendingChecks()
        {
            DalMonthlyProcess dlMonthlyProcess = new DalMonthlyProcess();

            try
            {
                DataTable dt = new DataTable();
                dt = dlMonthlyProcess.GetPendingChecks(this);
                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int LogMonthlyProcessErrors()
        {
            DalMonthlyProcess dlMonthlyProcess = new DalMonthlyProcess();

            try
            {
                return dlMonthlyProcess.LogMonthlyProcessErrors(this);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataSet RunDataIntegrityCheck(int mode)
        {
            DalMonthlyProcess dlMonthlyProcess = new DalMonthlyProcess();

            try
            {
                DataSet ds = new DataSet();
                ds = dlMonthlyProcess.RunDataIntegrityCheck(this.FK_Store, mode);
                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



        public DataTable SaveMonthlyProcess()
        {
            DalMonthlyProcess dlMonthlyProcess = new DalMonthlyProcess();

            try
            {
                _UserAction = 1;

                DataTable dt = new DataTable();
                dt = dlMonthlyProcess.SaveMonthlyProcess(this);
                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DateTime? GetLastMonthlyProcessDate()
        {
            DalMonthlyProcess dlMonthlyProcess = new DalMonthlyProcess();
            try
            {
                return dlMonthlyProcess.GetLastMonthlyProcessDate(this);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        /// Evaluates Monthly Process grace-period status for login warnings.
        /// NextProcessDate = LastMonthlyProcessDate + GracePeriodDays.
        /// Warning is shown during the grace window; urgent when due date is reached/exceeded.
        /// No warning only when Monthly Process is already done for the current cycle
        /// (last process date is on/after the active period FromDate).

        public MonthlyProcessGraceStatus GetGracePeriodWarningStatus(int gracePeriodDays)
        {
            MonthlyProcessGraceStatus status = new MonthlyProcessGraceStatus();

            if (gracePeriodDays < 0)
                return status;

            DataTable dtActive = GetActiveBusinessDates();
            DateTime? periodFromDate = null;
            DateTime? periodToDate = null;

            if (dtActive != null && dtActive.Rows.Count > 0)
            {
                DataRow row = dtActive.Rows[0];
                if (row.Table.Columns.Contains("FromDate") && row["FromDate"] != DBNull.Value)
                    periodFromDate = Convert.ToDateTime(row["FromDate"]).Date;
                if (row["ToDate"] != DBNull.Value)
                    periodToDate = Convert.ToDateTime(row["ToDate"]).Date;
            }

            DateTime today = DateTime.Today;
            DateTime? lastProcessDate = null;
            try
            {
                lastProcessDate = GetLastMonthlyProcessDate();
            }
            catch
            {
                lastProcessDate = null;
            }

            // No prior Monthly Process: use active period end when available (existing open-period flow).
            DateTime? referenceDate = lastProcessDate;
            if (!referenceDate.HasValue)
            {
                if (!periodToDate.HasValue)
                    return status;
                referenceDate = periodToDate;
            }

            DateTime dueDate = referenceDate.Value.Date.AddDays(gracePeriodDays);
            status.DueDate = dueDate;
            status.LastProcessDate = lastProcessDate;

            // Already completed for current open cycle and still before due  no warning.
            // (Last close covers/starts the current active period.)
            if (lastProcessDate.HasValue && periodFromDate.HasValue
                && lastProcessDate.Value.Date >= periodFromDate.Value.Date
                && today < dueDate)
            {
                return status;
            }

            status.ShowWarning = true;
            string dueDateText = dueDate.ToString("dd-MMM-yyyy", System.Globalization.CultureInfo.InvariantCulture);

            if (today >= dueDate)
            {
                status.IsUrgent = true;
                status.Message = "Urgent: Monthly Process is pending. It must be completed before "
                    + dueDateText + ". Due date has been reached/exceeded.";
            }
            else
            {
                status.IsUrgent = false;
                status.Message = "Warning: Monthly Process needs to be completed before "
                    + dueDateText + " to avoid interruption of transactions.";
            }

            return status;
        }
    }

    public class MonthlyProcessGraceStatus
    {
        public bool ShowWarning { get; set; }
        public bool IsUrgent { get; set; }
        public DateTime? DueDate { get; set; }
        public DateTime? LastProcessDate { get; set; }
        public string Message { get; set; }

        public MonthlyProcessGraceStatus()
        {
            ShowWarning = false;
            IsUrgent = false;
            DueDate = null;
            LastProcessDate = null;
            Message = string.Empty;
        }
    }
}