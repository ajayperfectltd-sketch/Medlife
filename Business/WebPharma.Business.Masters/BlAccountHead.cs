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
   public class BlAccountHead:IAccountHead
    {
        #region Properties

       public  BlAccountHead()
        {
            Initialize();
        }
        private long _ID_AccountHead;

    
        private long _FK_AccountGroup;

        private long _FK_AccountSubGroup;
    
      
        private string _AhCode;

    
        private string _Name;

      
        private string _ShortName;

     
        private byte _AccountType;

     
        private int _SortOrder;

      
        private string _Description;


        private Boolean _Active;

   

       
        private string _UserCode;


        private byte _UserAction;

      
        private DateTime _EnteredOn;

       

        private Int64 _FK_Client;

      
        private Int16 _PageIndex;

      
        private Int16 _PageSize;
        private long _FK_Reason;
        private Boolean _CashinHand;

        public Boolean CashinHand
        {
            get { return _CashinHand; }
            set { _CashinHand = value; }
        }
        public Boolean Active
        {
            get { return _Active; }
            set { _Active = value; }
        }


        public long MasterId
        {
            get { return _ID_AccountHead; }
            set { _ID_AccountHead = value; }
        }


        public long FK_AccountGroup
        {
            get { return _FK_AccountGroup; }
            set { _FK_AccountGroup = value; }
        }
        public long FK_AccountSubGroup
        {
            get { return _FK_AccountSubGroup; }
            set { _FK_AccountSubGroup = value; }
        }
        public string AhCode
        {
            get { return _AhCode; }
            set { _AhCode = value; }
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
        public byte AccountType
        {
            get { return _AccountType; }
            set { _AccountType = value; }
        }
        public int SortOrder
        {
            get { return _SortOrder; }
            set { _SortOrder = value; }
        }
        public string Description
        {
            get { return _Description; }
            set { _Description = value; }
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
        public Int64 FK_Reason
        {
            get { return _FK_Reason; }
            set { _FK_Reason = value; }
        }
        public void Initialize()
        {
            _UserAction = 1;
            _UserCode = string.Empty;
            _EnteredOn = DateTime.Now;
            _FK_Client = 1;
            _PageIndex = 1;
            _PageSize = 10;
            _ID_AccountHead = 0;
            _FK_AccountGroup = 0;
            _FK_AccountSubGroup = 0;
            _AhCode = string.Empty;
            _Name = string.Empty;
            _ShortName = string.Empty;
            _AccountType = 0;
            _SortOrder = 0;
            _Description = string.Empty;
            _Active = true;
            _FK_Reason = 0;
            _CashinHand = false;
        }
        #endregion
        #region Functions
        public long InsertData()
        {
            DalAccountHead dalAccHead = new DalAccountHead();
            long status = 0;
            try
            {
                _UserAction = 1;
                status = dalAccHead.UpdateAccountHead(this);
                dalAccHead = null;

            }
            catch (Exception ex)
            {
            }
            return status;
        }
        public long UpdateData()
        {
            DalAccountHead dalAccHead = new DalAccountHead();
            long status = 0;
            try
            {
                _UserAction = 2;
                status = dalAccHead.UpdateAccountHead(this);
                dalAccHead = null;  
            }
            catch (Exception ex)
            {
            }
            return status;
        }
        public long DeleteData()
        {
            DalAccountHead dalAccHead = new DalAccountHead();
            long status = 0;
            try
            {
                status = dalAccHead.DeleteAccountGroup(this);
                dalAccHead = null;
            }
            catch (Exception ex)
            {
                 
            }
            return status;
        }

        public void SelectData()
        {

            DalAccountHead dalAccHead = new DalAccountHead();
            long status = 0;
            IAccountHead IAccHead = (IAccountHead)dalAccHead.SelectAccountHead(this);
            _FK_AccountGroup = IAccHead.FK_AccountGroup;
            _FK_AccountSubGroup = IAccHead.FK_AccountSubGroup;
            _AhCode = IAccHead.AhCode;
            _Name = IAccHead.Name;
            _ShortName = IAccHead.ShortName;
            _Description = IAccHead.Description;
            _Active = IAccHead.Active;
            _CashinHand = IAccHead.CashinHand;
            dalAccHead = null;
        }
        public DataTable SelectAllData()
        {
            DataTable dtbl = new DataTable();
            DalAccountHead dalAccHead = new DalAccountHead();
            dtbl = dalAccHead.SelectAllData(this);
            dalAccHead = null;
            return dtbl;
        }
        
        #endregion
    }
}
