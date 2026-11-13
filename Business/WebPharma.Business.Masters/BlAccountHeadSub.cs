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
    public class BlAccountHeadSub:IAccountHeadSub
    {
        public BlAccountHeadSub()
        {

            Initialize();
        }
        
        public void Initialize()
        {
            _ID_AccountHeadSub = 0;
            _Fk_AccountHead = 0;
            _Ahcode= string.Empty;
            _AhsCode = string.Empty;
            _Name = string.Empty;
            _ShortName = string.Empty;
            _SortOrder = 0;
            _Description = "";
            _UserCode = string.Empty;
            _EnteredOn = DateTime.Now;
            _Active = true;
            _Fk_Store = 0;
            _UserAction = 1;
            _UserCode = string.Empty;
            _EnteredOn = DateTime.Now;
            _FK_Client = 1;
            _PageIndex = 1;
            _PageSize = 10;
            _PettyCash = false;
            _OpeningDate = DateTime.Now;
            _OpeningBalance = 0;
            _BalanceType = 0;
            _Fk_Cuscode = 0;
            _Fk_Reason = 0;

        }
        #region Properties
        

        private Boolean _PettyCash;
        private Int64 _Fk_Reason;
        public Boolean PettyCash
        {
            get { return _PettyCash; }
            set { _PettyCash = value; }
        }

        private Int64 _FK_Client;

        public Int64 FK_Client
        {
            get { return _FK_Client; }
            set { _FK_Client = value; }
        }
        private Int16 _PageIndex;

        public Int16 PageIndex
        {
            get { return _PageIndex; }
            set { _PageIndex = value; }
        }
        private Int16 _PageSize;

        public Int16 PageSize
        {
            get { return _PageSize; }
            set { _PageSize = value; }
        }
        private byte _UserAction;

        public byte UserAction
        {
            get { return _UserAction; }
            set { _UserAction = value; }
        } 
        long _ID_AccountHeadSub;

        public long MasterId
        {
            get { return _ID_AccountHeadSub; }
            set { _ID_AccountHeadSub = value; }
        }
        long _Fk_AccountHead;

        public long Fk_AccountHead
        {
            get { return _Fk_AccountHead; }
            set { _Fk_AccountHead = value; }
        }
        string _Ahcode;

        public string Ahcode
        {
            get { return _Ahcode; }
            set { _Ahcode = value; }
        }

      
        string _AhsCode;

        public string AhsCode
        {
            get { return _AhsCode; }
            set { _AhsCode = value; }
        }
        string _Name;

        public string Name
        {
            get { return _Name; }
            set { _Name = value; }
        }
        string _ShortName;

        public string ShortName
        {
            get { return _ShortName; }
            set { _ShortName = value; }
        }
        int _SortOrder;

        public int SortOrder
        {
            get { return _SortOrder; }
            set { _SortOrder = value; }
        }
        string _Description;

        public string Description
        {
            get { return _Description; }
            set { _Description = value; }
        }
        string _UserCode;

        public string UserCode
        {
            get { return _UserCode; }
            set { _UserCode = value; }
        }
        DateTime _EnteredOn;

        public DateTime EnteredOn
        {
            get { return _EnteredOn; }
            set { _EnteredOn = value; }
        }
        Boolean _Active;

        public Boolean Active
        {
            get { return _Active; }
            set { _Active = value; }
        }
        long _Fk_Store;

        public long Fk_Store
        {
            get { return _Fk_Store; }
            set { _Fk_Store = value; }
        }
        long _Fk_Client;

        public long Fk_Client
        {
            get { return _Fk_Client; }
            set { _Fk_Client = value; }
        }
        public Int64 FK_Reason
        {
            get { return _Fk_Reason; }
            set { _Fk_Reason = value; }
        }




        private DateTime _OpeningDate;

        public DateTime OpeningDate
        {
            get { return _OpeningDate; }
            set { _OpeningDate = value; }
        }
        private Decimal _OpeningBalance;

        public Decimal OpeningBalance
        {
            get { return _OpeningBalance; }
            set { _OpeningBalance = value; }
        }
        private int _BalanceType;
            public int BalanceType
        {
            get { return _BalanceType; }
            set { _BalanceType = value; }
        }
           private Int64 _Fk_Cuscode;
         public Int64 Fk_Cuscode
        {
            get { return _Fk_Cuscode; }
            set { _Fk_Cuscode = value; }
        }

        




        #endregion
        #region Process
        public long InsertData()
        {
            DalAccountHeadSub DalAccount = new DalAccountHeadSub();
            long status = 0;
            try
            {
                _UserAction = 1;
                status = DalAccount.UpdateAccountHeadSub(this);
                DalAccount = null;

            }
            catch (Exception ex)
            {
            }
            return status;
        }
        public long UpdateData()
        {
            DalAccountHeadSub DalAccount = new DalAccountHeadSub();
            long status = 0;
            try
            {
                _UserAction = 2;
                status = DalAccount.UpdateAccountHeadSub(this);
                DalAccount = null;
            }
            catch (Exception ex)
            {
            }
            return status;
        }
        public long DeleteData()
        {
            DalAccountHeadSub DalAccount = new DalAccountHeadSub();
            long status = 0;
            try
            {

                status = DalAccount.AccountHeadSubDelete(this);
                DalAccount = null;
            }
            catch (Exception ex)
            {
            }
            return status;
        }
        public void SelectData()
        {
            DalAccountHeadSub DalAccount = new DalAccountHeadSub();
            long status = 0;
            IAccountHeadSub IAcHeadSub = (IAccountHeadSub)DalAccount.SelectAccountSub(this);
            _Fk_AccountHead = IAcHeadSub.Fk_AccountHead;
           _Ahcode= IAcHeadSub.Ahcode;
           _Name = IAcHeadSub.Name;
           _ShortName = IAcHeadSub.ShortName;
           _SortOrder = IAcHeadSub.SortOrder;
           _Description = IAcHeadSub.Description;
           _Active = IAcHeadSub.Active;
           _Fk_Store = IAcHeadSub.Fk_Store;
           _OpeningDate = IAcHeadSub.OpeningDate;
           _OpeningBalance = IAcHeadSub.OpeningBalance;
           _Fk_Cuscode = IAcHeadSub.Fk_Cuscode;
               
           DalAccount = null;
        }
        public DataTable SelectAllData()
        {
            DataTable dtbl = new DataTable();
            DalAccountHeadSub DalAccount = new DalAccountHeadSub();
            dtbl = DalAccount.SelectAllData(this);
            DalAccount = null;
            return dtbl;
        }
        #endregion

        public DataTable selectid()
        {
            DataTable dtbl = new DataTable();
            DalAccountHeadSub DalAccount = new DalAccountHeadSub();
            dtbl = DalAccount.SelectsubheadId(this);
            DalAccount = null;
            return dtbl;
        }

        public DataTable selectAccountsubhead()
        {
            DataTable dtbl = new DataTable();
            DalAccountHeadSub DalAccount = new DalAccountHeadSub();
            dtbl = DalAccount.SelectAccountHeadSub(this);
            DalAccount = null;
            return dtbl;
        }

        public DataTable selectSubHeadCustomerDetails()
        {
            DataTable dtbl = new DataTable();
            DalAccountHeadSub DalAccount = new DalAccountHeadSub();
            dtbl = DalAccount.selectSubHeadCustomerDetails(this);
            DalAccount = null;
            return dtbl;
        }

      

        public DataTable selectfkAccounthead()
        {
            DataTable dtbl = new DataTable();
            DalAccountHeadSub DalAccount = new DalAccountHeadSub();
            dtbl = DalAccount.selectfkAccounthead(this);
            DalAccount = null;
            return dtbl;
        }

        public DataTable selectfkAccountheadsub()
        {
            DataTable dtbl = new DataTable();
            DalAccountHeadSub DalAccount = new DalAccountHeadSub();
            dtbl = DalAccount.selectfkAccountheadsub(this);
            DalAccount = null;
            return dtbl;
        }
    }
}
