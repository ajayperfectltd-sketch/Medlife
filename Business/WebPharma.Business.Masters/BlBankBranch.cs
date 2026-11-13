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
    public class BlBankBranch : IBankBranch
    {
          private byte _UserAction;
        private string _UserCode;
        private DateTime _EnteredOn;
        private Int64 _FK_Client;
        private Int16 _PageIndex;
        private Int16 _PageSize;
        private Int64 _ID_BankBranch;
        private string _Name;
        private string _ShortName;
        private string _Address1;
        private string _Address2;
        private string _Address3;
        private Boolean _Active;
        private Int64 _FK_Bank;
        private Int64 _FK_Country;
        private Int64 _FK_AccountHead;
        private Int64 _FK_AccountHeadSub;
        private string _AccountSubHead;

        public string AccountSubHead
        {
            get { return _AccountSubHead; }
            set { _AccountSubHead = value; }
        }
        private string _AccountHead;

        public string AccountHead
        {
            get { return _AccountHead; }
            set { _AccountHead = value; }
        }

        public Int64 FK_AccountHeadSub
        {
            get { return _FK_AccountHeadSub; }
            set { _FK_AccountHeadSub = value; }
        }
        public BlBankBranch()
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
            _FK_AccountHeadSub = 0;
            _ID_BankBranch = 0;
            _Name = string.Empty;
            _ShortName = string.Empty;
            _Address1 = string.Empty;
            _Address2 = string.Empty;
            _Address3 = string.Empty;
            _AccountHead = string.Empty;
            _AccountSubHead = string.Empty;
            _Active = true;
            _FK_Bank = 0;
            _FK_Country = 0;
            _FK_AccountHeadSub = 0;
            _FK_AccountHead = 0;
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
            get { return _ID_BankBranch; }
            set { _ID_BankBranch = value; }
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
        public string Address1
        {
            get { return _Address1; }
            set { _Address1 = value; }
        }
        public string Address2
        {
            get { return _Address2; }
            set { _Address2 = value; }
        }
        public string Address3
        {
            get { return _Address3; }
            set { _Address3 = value; }
        }
        public Boolean Active
        {
            get { return _Active; }
            set { _Active = value; }
        }
        public Int64 FK_Bank
        {
            get { return _FK_Bank; }
            set { _FK_Bank = value; }
        }
        public Int64 FK_Country
        {
            get { return _FK_Country; }
            set { _FK_Country = value; }
        }
        public Int64 FK_AccountHead
        {
            get { return _FK_AccountHead; }
            set { _FK_AccountHead = value; }
        }

        public long InsertData()
        {
            long status;
            DalBankBranch dalbankbranch = new DalBankBranch();
            status = 0;
            try
            {
                _UserAction = 1;
                status = dalbankbranch.UpdateBankBranch(this);
                dalbankbranch = null;
            }
            catch (Exception ex)
            {
            }
            return status;
        }
        public long UpdateData()
        {
            DalBankBranch dalbankbranch = new DalBankBranch();
            long status;
            status = 0;
            try
            {
                _UserAction = 2;
                status = dalbankbranch.UpdateBankBranch(this);
                dalbankbranch = null;
            }
            catch (Exception ex)
            {
            }
            return status;
        }
        public long DeleteData()
        {
            DalBankBranch dalbankbranch = new DalBankBranch();
            long status;
            status = 0;
            try
            {
                status = dalbankbranch.DeleteBankBranch(this);
                dalbankbranch = null;
            }
            catch (Exception ex)
            {
            }
            return status;
        }
        public void SelectData()
        {
            DalBankBranch dalbankbranch = new DalBankBranch();
            IBankBranch ibankbranch;
            ibankbranch = (IBankBranch)dalbankbranch.SelectBankBranch(this);
            _ShortName = ibankbranch.ShortName;
            _Name = ibankbranch.Name;
            _Address1 = ibankbranch.Address1;
            _Address2 = ibankbranch.Address2;
            _Address3 = ibankbranch.Address3;
            _Active = ibankbranch.Active;
            _FK_Bank = ibankbranch.FK_Bank;
            _FK_Country = ibankbranch.FK_Country;
            _FK_AccountHead = ibankbranch.FK_AccountHead;
            dalbankbranch = null;
        }
        public DataTable SelectAllData()
        {
            DataTable dtbl = new DataTable();
            DalBankBranch dalbankbranch = new DalBankBranch();
            dtbl = dalbankbranch.SelectAllBankBranch(this);
            dalbankbranch = null;
            return dtbl;
        }
    }
}
