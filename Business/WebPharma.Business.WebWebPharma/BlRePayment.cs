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
using WebPharma.Interface.WebPharma;
using WebPharma.DataAccess.WebPharma;
using System.Data;

namespace WebPharma.Business.WebPharma
{
    public class BlRePayment:IRePayment
    {
        private byte _UserAction;
        private long _ID_CustomerRePayment;
        private long _FK_Customer;
        private string _TransType;
        private decimal _Amount;
        private string _UserCode;
        private long _FK_Client;
        private Int16 _PageIndex;
        private Int16 _PageSize;
        private DateTime _EnteredOn;
        private string _CustName;
        private long _FK_Store;
        private long _FK_Counter;

        private string _DatasetName;
        private string _ReportSchema;
        private string _ReportTable;
        private string _ReportSchemaPath;
        private string _ReportPath;
        private string _ReportFile;

        public BlRePayment()
		{
			Initialize();
		}
		public void Initialize()
		{
            _ID_CustomerRePayment = 0;
            _FK_Customer = 0;
            _TransType = string.Empty;
            _Amount = 0;
			_UserCode = string.Empty;
			_FK_Client = 1;
            _CustName = string.Empty;
            _UserAction = 1;
            _EnteredOn = DateTime.Now;
            _PageIndex = 1;
            _PageSize = 10;
            _FK_Store = 0;
            _FK_Counter = 0;

            _ReportPath = string.Empty;
            _ReportSchema = string.Empty;
            _ReportSchemaPath = string.Empty;
            _ReportTable = string.Empty;
            _ReportFile = string.Empty;
            _DatasetName = string.Empty;
		}
		public byte UserAction
		{
			get { return _UserAction; }
			set { _UserAction = value; }
		}
        public long MasterId
        {
            get { return _ID_CustomerRePayment; }
            set { _ID_CustomerRePayment = value; }
        }
        public long FK_Customer
        {
            get { return _FK_Customer; }
            set { _FK_Customer = value; }
        }
        public string TransType
        {
            get { return _TransType; }
            set { _TransType = value; }
        }
        public string CustName
        {
            get { return _CustName; }
            set { _CustName = value; }
        }
        public decimal Amount
        {
            get { return _Amount; }
            set { _Amount = value; }
        }
        public string UserCode
        {
            get { return _UserCode; }
            set { _UserCode = value; }
        }
        public long FK_Client
        {
            get { return _FK_Client; }
            set { _FK_Client = value; }
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
        public long FK_Store
        {
            get { return _FK_Store; }
            set { _FK_Store = value; }
        }
        public long FK_Counter
        {
            get { return _FK_Counter; }
            set { _FK_Counter = value; }
        }
        public DateTime EnteredOn
        {
            get { return _EnteredOn; }
            set { _EnteredOn = value; }
        }

        public string ReportFile
        {
            get { return _ReportFile; }
            set { _ReportFile = value; }
        }

        public string ReportSchema
        {
            get { return _ReportSchema; }
            set { _ReportSchema = value; }
        }

        public string ReportTable
        {
            get { return _ReportTable; }
            set { _ReportTable = value; }
        }

        public string ReportSchemaPath
        {
            get { return _ReportSchemaPath; }
            set { _ReportSchemaPath = value; }
        }

        public string ReportPath
        {
            get { return _ReportPath; }
            set { _ReportPath = value; }
        }
        public string DatasetName
        {
            get { return _DatasetName; }
            set { _DatasetName = value; }
        }
        public long InsertData()
        {
            long status;
            DalRePayment dalrepayment = new DalRePayment();
            status = 0;
            try
            {
                _UserAction = 1;
                status = dalrepayment.UpdateRepayment(this);
                dalrepayment = null;
            }
            catch (Exception ex)
            {

            }
            return status;
        }

        public long UpdateData()
        {
            _UserAction = 2;
            DalRePayment dalrepayment = new DalRePayment();
            long status;
            status = 0;
            try
            {
                status = dalrepayment.UpdateRepayment(this);
                dalrepayment = null;
            }
            catch (Exception ex)
            {

            }
            return status;
        }
        public void SelectData()
        {
            DalRePayment dalrepayment = new DalRePayment();
            IRePayment irepayment;
            irepayment = (IRePayment)dalrepayment.SelectRePayments(this);
            _CustName = irepayment.CustName;
            _FK_Customer = irepayment.FK_Customer;
            _TransType = irepayment.TransType;
            _Amount = irepayment.Amount;
        }

        public DataTable SelectAllData()
        {
            DataTable dtbl = new DataTable();
            DalRePayment dalrepayment = new DalRePayment();
            dtbl = dalrepayment.SelectAllRePayments(this);
            dalrepayment = null;
            return dtbl;
        }
        public DataSet RepaymentReportData(long StatusId)
        {
            DalRePayment dalrepayment = new DalRePayment();
            DataSet ds = new DataSet();
            try
            {
                ds = dalrepayment.RepaymentReportData(StatusId);
                ds.DataSetName = _DatasetName;
                ds.Tables[0].TableName = ReportTable;
                ds.WriteXmlSchema(_ReportSchemaPath + _ReportSchema + ".xsd");
            }
            catch (Exception ex)
            {
            }
            finally
            {
                dalrepayment = null;
            }
            return ds;
        }
    }
}
