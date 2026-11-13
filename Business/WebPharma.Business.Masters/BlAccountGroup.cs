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
    public class BlAccountGroup:IAccountGroup
    {

        public BlAccountGroup()
        {
            Initialize();
        }
        private long _ID_AccountGroup;
        private byte _UserAction;
        private string _Name;
        private string _ShortName;
        private string _AccountType;
        private string _GroupType;
        private int _SortOrder;
        private string _Descrption;
        private string _UserCode;
        private DateTime _EnteredOn;
        private Boolean _Active;
        private Int64 _FK_Client;
        private Int16 _PageIndex;
        private Int16 _PageSize;
        private long _FK_Reason;

        private string _Description;

        public string Description
        {
            get { return _Description; }
            set { _Description = value; }
        }

        public void Initialize()
        {

            _UserAction = 1;
            _UserCode = string.Empty;
            _EnteredOn = DateTime.Now;
            _FK_Client = 1;
            _PageIndex = 1;
            _PageSize = 10;
            _ID_AccountGroup = 0;
            _Name = string.Empty;
            _ShortName = string.Empty;
            _AccountType = string.Empty;
            _GroupType = string.Empty;

            _SortOrder = 0;
            _Descrption = string.Empty;
            _UserCode = string.Empty;
            _Active = true;
            _FK_Reason = 0;


        }
        public Int64 MasterId
        {
            get { return _ID_AccountGroup; }
            set { _ID_AccountGroup = value; }
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

        public string AccountType
        {
            get { return _AccountType; }
            set { _AccountType = value; }
        }

        public string GroupType
        {
            get { return _GroupType; }
            set { _GroupType = value; }
        }
       

        public int SortOrder
        {
            get { return _SortOrder; }
            set { _SortOrder = value; }
        }
      

        public string Descrption
        {
            get { return _Descrption; }
            set { _Descrption = value; }
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


        public Boolean Active
        {
            get { return _Active; }
            set { _Active = value; }
        }
        

        public Int64 FK_Client
        {
            get { return _FK_Client; }
            set { _FK_Client = value; }
        }
        public Int64 FK_Reason
        {
            get { return _FK_Reason; }
            set { _FK_Reason = value; }
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
        public long InsertData()
        {
            DalAccountGroup dalaccGrp = new DalAccountGroup();
            long status = 0;
            try
            {
                _UserAction = 1;
               status= dalaccGrp.UpdateAccountGroup(this);
               dalaccGrp = null;
            }
            catch (Exception ex)
            {
            }
            return status;
        }
        public long UpdateData()
        {
            DalAccountGroup dalaccGrp = new DalAccountGroup();
            long status = 0;
            try
            {
                _UserAction = 2;
                status = dalaccGrp.UpdateAccountGroup(this);
                dalaccGrp = null;
            }
            catch (Exception ex)
            {
            }
            return status;
        }
        public long DeleteData()
        { DalAccountGroup DalAcc = new DalAccountGroup();

            long status;
            status = 0;
            try
            {
                status = DalAcc.DeleteAccountGroup(this);
                DalAcc = null;
            }
            catch (Exception ex)
            {
            }
            return status;
        }
        public void SelectData()
        {
            DalAccountGroup dalaccGrp = new DalAccountGroup();
            IAccountGroup IAcc=(IAccountGroup)dalaccGrp.SelectAccountGroup(this);
            _ShortName = IAcc.ShortName;
            _Name = IAcc.Name;
            AccountType = IAcc.AccountType;
            _GroupType = IAcc.GroupType;
            _SortOrder = IAcc.SortOrder;
            _Description = IAcc.Description;
            _Active = IAcc.Active;
            dalaccGrp = null;
           

        }
        public DataTable SelectAllData()
        {
            DataTable dtbl = new DataTable();
            DalAccountGroup dalaccGrp = new DalAccountGroup();
            dtbl = dalaccGrp.SelectAllAccountGroup(this);
            dalaccGrp = null;
            return dtbl;
        }
    }
}
