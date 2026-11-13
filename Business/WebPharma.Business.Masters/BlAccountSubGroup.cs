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
    public class BlAccountSubGroup:IAccountSubGroup
    {
        public BlAccountSubGroup()
        {
            Initialize();
        }
        #region Properties
        private byte _UserAction;
        private string _UserCode;
        private DateTime _EnteredOn;
        private Int64 _FK_Client;
        private Int16 _PageIndex;
        private Int16 _PageSize;
        private Int64 _ID_AccountSubGroup;
        private string _Name;
        private string _ShortName;
        private int _SortOrder;
        private string _Descrption;
        private Boolean _Active;
        private long _FK_Accountgroup;
        private long _FK_Reason;
       

        private string _Description;

  
     
        public void Initialize()
        {

            _UserAction = 1;
            _UserCode = string.Empty;
            _EnteredOn = DateTime.Now;
            _FK_Client = 1;
            _PageIndex = 1;
            _PageSize = 10;
            _ID_AccountSubGroup = 0;
            _FK_Accountgroup = 0;
            _Name = string.Empty;
            _ShortName = string.Empty;
            _Descrption = string.Empty;
            _Active = true;
            _Description = string.Empty;
            _FK_Reason = 0;
        }

        public string Description
        {
            get { return _Description; }
            set { _Description = value; }
        }
        public Int64 MasterId
        {
            get { return _ID_AccountSubGroup; }
            set { _ID_AccountSubGroup = value; }
        }
        public long FK_Accountgroup
        {
            get { return _FK_Accountgroup; }
            set { _FK_Accountgroup = value; }
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
      

        public int SortOrder
        {
            get { return _SortOrder; }
            set { _SortOrder = value; }
        }


      
     

        public Boolean Active
        {
            get { return _Active; }
            set { _Active = value; }
        }

        public Int64 FK_Reason
        {
            get { return _FK_Reason; }
            set { _FK_Reason = value; }
        }



        #endregion
        #region Process
        public long InsertData()
        {
            DalAccountSubGroup dalAccGrp = new DalAccountSubGroup();
            long status = 0;
            try
            {
                _UserAction = 1;
                status = dalAccGrp.UpdateAccountSubGroup(this);
                dalAccGrp = null;

            }
            catch (Exception ex)
            {
            }
             return status;
        }
        public long UpdateData()
        {
            DalAccountSubGroup dalAccGrp = new DalAccountSubGroup();
             long status = 0;
             try
             {
                 _UserAction = 2;
                 status = dalAccGrp.UpdateAccountSubGroup(this);
                 dalAccGrp = null;
             }
             catch (Exception ex)
             {
             }
             return status;
        }
        public long DeleteData()
        {
            DalAccountSubGroup DalAcc = new DalAccountSubGroup();

            long status;
            status = 0;
            try
            {
                status = DalAcc.DeleteAccountSubGroup(this);
                DalAcc = null;
            }
            catch (Exception ex)
            {
            }
            return status;
        }
        public void SelectData()
        {
            DalAccountSubGroup dalAccGrp = new DalAccountSubGroup();
            IAccountSubGroup IAcc = (IAccountSubGroup)dalAccGrp.SelectAccountSubGroup(this);
            _FK_Accountgroup = IAcc.FK_Accountgroup;
            _Name = IAcc.Name;
            _ShortName = IAcc.ShortName;
            _SortOrder = IAcc.SortOrder;
            _Descrption = IAcc.Description;
            _Active = IAcc.Active;

        }

        public DataTable SelectAllData()
        {
            DataTable dtbl = new DataTable();
            DalAccountSubGroup dal = new DalAccountSubGroup();
            dtbl = dal.SelectAllAccountSubGroup(this);
            dal = null;
            return dtbl;
        }
        #endregion
    }
}
