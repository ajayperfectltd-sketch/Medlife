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
   public class BlCustomerGroup:ICustomerGroup
    {
        private byte _UserAction;
        private string _UserCode;
        private DateTime _EnteredOn;
        private Int64 _FK_Client;
        private Int16 _PageIndex;
        private Int16 _PageSize;
        private Int64 _ID_CustomerGroup;
        private string _Name;
        private string _ShortName;
        private decimal _CreditLimit;
        private Int32 _CreditDays;
        private Boolean _Active;
        private long _FK_Reason;

        public BlCustomerGroup()
        {
            Initialize();
        }

        public void Initialize()
        {
            _UserAction = 1;
            _UserCode = string.Empty;
            _EnteredOn = DateTime.Now;
            _FK_Client = 1;
            _PageIndex = 1;
            _PageSize = 10;
            _ID_CustomerGroup = 0;
            _Name = string.Empty;
            _ShortName = string.Empty;
            _CreditLimit = 0;
            _CreditDays = 0;            
            _Active = true;
            _FK_Reason = 0;
        }

        public byte UserAction
        {
            get { return _UserAction; }
            set { _UserAction = value; }
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
        public Int64 FK_Client
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
        public Int64 MasterId
        {
            get { return _ID_CustomerGroup; }
            set { _ID_CustomerGroup = value; }
        }
        public string Name
        {
            get { return _Name; }
            set { _Name = value; }
        }
        public string ShortName
        {
            get { return _ShortName; }
            set { _ShortName = value; }
        }
        public decimal CreditLimit
        {
            get { return _CreditLimit; }
            set { _CreditLimit = value; }
        }
        public Int32 CreditDays
        {
            get { return _CreditDays; }
            set { _CreditDays = value; }
        }       
        public Boolean Active
        {
            get { return _Active; }
            set { _Active = value; }
        }
        public long FK_Reason
        {
            get { return _FK_Reason; }
            set { _FK_Reason = value; }
        }

        public long InsertData()
        {
            long status;
            DalCustomerGroup dalcustomergroup = new DalCustomerGroup();
            status = 0;
            try
            {
                _UserAction = 1;
                status = dalcustomergroup.UpdateCustomerGroup(this);
                dalcustomergroup = null;
            }
            catch (Exception ex)
            {
            }
            return status;
        }
        public long UpdateData()
        {
            DalCustomerGroup dalcustomergroup = new DalCustomerGroup();
            long status;
            status = 0;
            try
            {
                _UserAction = 2;
                status = dalcustomergroup.UpdateCustomerGroup(this);
                dalcustomergroup = null;
            }
            catch (Exception ex)
            {
            }
            return status;
        }
        public long DeleteData()
        {
            DalCustomerGroup dalcustomergroup = new DalCustomerGroup();
            long status;
            status = 0;
            try
            {
                status = dalcustomergroup.DeleteCustomerGroup(this);
                dalcustomergroup = null;
            }
            catch (Exception ex)
            {
            }
            return status;
        }
        public void SelectData()
        {
            DalCustomerGroup dalcustomergroup = new DalCustomerGroup();
            ICustomerGroup icustomergroup;
            icustomergroup = (ICustomerGroup)dalcustomergroup.SelectCustomerGroup(this);
            _ShortName = icustomergroup.ShortName;
            _Name = icustomergroup.Name;
            _CreditLimit = icustomergroup.CreditLimit;
            _CreditDays = icustomergroup.CreditDays;
            _Active = icustomergroup.Active;
            _FK_Reason = icustomergroup.FK_Reason;
            dalcustomergroup = null;
        }
        public DataTable SelectAllData()
        {
            DataTable dtbl = new DataTable();
            DalCustomerGroup dalcustomergroup = new DalCustomerGroup();
            dtbl = dalcustomergroup.SelectAllCustomerGroup(this);
            dalcustomergroup = null;
            return dtbl;
        }
    }
}
