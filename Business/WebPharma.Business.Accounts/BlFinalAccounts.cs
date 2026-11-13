using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using Inventory.DataAccess.Accounts;
using Inventory.Interface.Accounts;

namespace Inventory.Business.Accounts
{
    public class BlFinalAccounts : IFinalAccounts
    {
        private DateTime _TransDate;
        private string _Mode;
        private long _FK_Store;
        private string _UserCode;
        private DataTable _dtFinalAccount;
        private long _ID_FinalAccount;
        private long _Criteria;

        public BlFinalAccounts()
        {
            Initialize();
        }

        public void Initialize()
        {
            _TransDate = DateTime.Now;
            _Mode = string.Empty;
            _FK_Store = 0;
            _Criteria = 0;
            _ID_FinalAccount = 0;

        }
        public long ID_FinalAccount
        {
            get { return _ID_FinalAccount; }
            set { _ID_FinalAccount = value; }
        }
        public long Criteria
        {
            get { return _Criteria; }
            set { _Criteria = value; }
        }
        public DataTable dtFinalAccount
        {
            get { return _dtFinalAccount; }
            set { _dtFinalAccount = value; }
        }
        public string UserCode
        {
            get { return _UserCode; }
            set { _UserCode = value; }
        }


        public DateTime TransDate
        {
            get { return _TransDate; }
            set { _TransDate = value; }
        }
        public string Mode
        {
            get { return _Mode; }
            set { _Mode = value; }
        }

        public long FK_Store
        {
            get { return _FK_Store; }
            set { _FK_Store = value; }
        }
        public DataTable SelectFinalAccounts()
        {
            DataTable dtbl = new DataTable();
            DalFinalAccounts dalInventoryTransaction = new DalFinalAccounts();
            dtbl = dalInventoryTransaction.SelectFinalAccounts(this);
            dalInventoryTransaction = null;
            return dtbl;
        }
        public DataTable SelectFinalAccountProcess()
        {
            _Criteria = 1;
            DataTable dtbl = new DataTable();
            try
            {
                DalFinalAccounts dalInventoryTransaction = new DalFinalAccounts();
                dtbl = dalInventoryTransaction.SelectFinalAccounts(this);
                dalInventoryTransaction = null;
            }
            catch (Exception ex)
            {

            }
            return dtbl;
        }
        public long FinalAccountUpdate()
        {
            long status;
            DalFinalAccounts dalFinalAccount = new DalFinalAccounts();

            status = 0;
            try
            {


                status = dalFinalAccount.FinalAccountUpdate(this);
                dalFinalAccount = null;
            }
            catch (Exception ex)
            {

            }
            return status;

        }


    }
}
