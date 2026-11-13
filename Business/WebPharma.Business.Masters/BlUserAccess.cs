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
    public class BlUserAccess : IUserAccess
    {
        private byte _UserAction;
        private string _UserCode;
        private DateTime _EnteredOn;
        private Int64 _FK_Client;
        private Int16 _PageIndex;
        private Int16 _PageSize;

        private Int64 _ID_UserAccess;
        private Int64 _FK_Users;
        private Int64 _FK_Pages;
        private bool _IsAdd;
        private bool _IsUpdate;
        private bool _IsDelete;
        private bool _IsView;
        private bool _IsPurchase;
        private bool _IsPurReturn;
        private bool _IsSales;
        private bool _IsSalesReturn;
        private bool _IsApprove;
        private string _XmlUserAccess;
        private Int64 _FK_Pages1;
        private Int64 _FK_Pages2;
        private Int64 _FK_Pages3;
        private Int64 _FK_Pages4;
        private Int64 _FK_Pages5;
        private Int64 _FK_UserGroup;

        public Int64 FK_Pages5
        {
            get { return _FK_Pages5; }
            set { _FK_Pages5 = value; }
        }

        private Int64 _FK_Pages6;

        public Int64 FK_Pages6
        {
            get { return _FK_Pages6; }
            set { _FK_Pages6 = value; }
        }




        public BlUserAccess()
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

            _ID_UserAccess = 0;
            _FK_Users = 0;
            _FK_Pages = 0;
            _IsAdd = true;
            _IsUpdate = true;
            _IsDelete = true;
            _IsView = true;
            _IsPurchase = true;
            _IsPurReturn = true;
            _IsSales = true;
            _IsSalesReturn = true;
            _XmlUserAccess = string.Empty;
            _FK_Pages1 = 0;
            _FK_Pages2 = 0;
            _FK_Pages3 = 0;
            _FK_Pages4 = 0;
            _FK_Pages5 = 0;
            _FK_Pages6 = 0;
            _FK_UserGroup = 0;
        }
        public Int64 FK_UserGroup
        {
            get { return _FK_UserGroup; }
            set { _FK_UserGroup = value; }
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
            get { return _ID_UserAccess; }
            set { _ID_UserAccess = value; }
        }
        public Int64 FK_Users
        {
            get { return _FK_Users; }
            set { _FK_Users = value; }
        }
        public Int64 FK_Pages
        {
            get { return _FK_Pages; }
            set { _FK_Pages = value; }
        }
        public bool IsAdd
        {
            get { return _IsAdd; }
            set { _IsAdd = value; }
        }
        public bool IsDelete
        {
            get { return _IsDelete; }
            set { _IsDelete = value; }
        }
        public bool IsUpdate
        {
            get { return _IsUpdate; }
            set { _IsUpdate = value; }
        }
        public bool IsView
        {
            get { return _IsView; }
            set { _IsView = value; }
        }

        public bool IsPurchase
        {
            get { return _IsPurchase; }
            set { _IsPurchase = value; }
        }
        public bool IsPurReturn
        {
            get { return _IsPurReturn; }
            set { _IsPurReturn = value; }
        }
        public bool IsSales
        {
            get { return _IsSales; }
            set { _IsSales = value; }
        }
        public bool IsSalesReturn
        {
            get { return _IsSalesReturn; }
            set { _IsSalesReturn = value; }
        }
        public bool IsApprove
        {
            get { return _IsApprove; }
            set { _IsApprove = value; }
        }
        public string XmlUserAccess
        {
            get { return _XmlUserAccess; }
            set { _XmlUserAccess = value; }
        }
        public Int64 FK_Pages1
        {
            get { return _FK_Pages1; }
            set { _FK_Pages1 = value; }
        }
        public Int64 FK_Pages2
        {
            get { return _FK_Pages2; }
            set { _FK_Pages2 = value; }
        }
        public Int64 FK_Pages3
        {
            get { return _FK_Pages3; }
            set { _FK_Pages3 = value; }
        }
        public Int64 FK_Pages4
        {
            get { return _FK_Pages4; }
            set { _FK_Pages4 = value; }
        }
        private Int64 _FK_PageModule;

        public Int64 FK_PageModule
        {
            get { return _FK_PageModule; }
            set { _FK_PageModule = value; }
        }


        public long UpdateData()
        {
            DalUserAccess daluseraccess = new DalUserAccess();
            long status;
            status = 0;
            try
            {
                status = daluseraccess.UpdateUserAccess(this);
                daluseraccess = null;
            }
            catch (Exception ex)
            {
            }
            return status;
        }
        public long UpdateQuickmenuData()
        {
            DalUserAccess daluseraccess = new DalUserAccess();
            long status;
            status = 0;
            try
            {
                status = daluseraccess.UpdateQuickmenu(this);
                daluseraccess = null;
            }
            catch (Exception ex)
            {
            }
            return status;
        }
        public DataSet SelectUserAccess()
        {
            //DataTable dtbl = new DataTable();
            DataSet dsetUserAccess = new DataSet();
            DalUserAccess daluseraccess = new DalUserAccess();
            dsetUserAccess = daluseraccess.SelectUserAccess(this);
            daluseraccess = null;
            return dsetUserAccess;
        }
        public DataSet SelectUserAccessMenu()
        {
            //DataTable dtbl = new DataTable();
            DataSet dsetUserAccess = new DataSet();
            DalUserAccess daluseraccess = new DalUserAccess();
            dsetUserAccess = daluseraccess.SelectUserAccessMenu(this);
            daluseraccess = null;
            return dsetUserAccess;
        }
        public DataTable SelectQuickMenu()
        {
            DataTable dtbl = new DataTable();
            DalUserAccess daluseraccess = new DalUserAccess();
            dtbl = daluseraccess.SelectQuickMenu(this);
            daluseraccess = null;
            return dtbl;
        }
        public DataTable SelectPages()
        {
            DataTable dtbl = new DataTable();
            DalUserAccess daluseraccess = new DalUserAccess();
            dtbl = daluseraccess.SelectPages(this);
            daluseraccess = null;
            return dtbl;
        }
        public void SelectPageSettings()
        {
            DalUserAccess daluseraccess = new DalUserAccess();
            IUserAccess iuseraccess;
            iuseraccess = (IUserAccess)daluseraccess.SelectPageSettings(this);
            _IsAdd = iuseraccess.IsAdd;
            _IsDelete = iuseraccess.IsDelete;
            _IsUpdate = iuseraccess.IsUpdate;
            _IsView = iuseraccess.IsView;

        }
        public void CheckUserAccess()
        {
            DalUserAccess daluseraccess = new DalUserAccess();
            IUserAccess iuseraccess;
            iuseraccess = (IUserAccess)daluseraccess.CheckUserAccess(this);
            _IsAdd = iuseraccess.IsAdd;
            _IsDelete = iuseraccess.IsDelete;
            _IsUpdate = iuseraccess.IsUpdate;
            _IsView = iuseraccess.IsView;
            _IsApprove = iuseraccess.IsApprove;
            _IsPurchase = iuseraccess.IsPurchase;
            _IsPurReturn = iuseraccess.IsPurReturn;
            _IsSales = iuseraccess.IsSales;
            _IsSalesReturn = iuseraccess.IsSalesReturn;
        }
    }
    public class UserAccessPages
    {
        private Int64 _FK_Pages;
        private bool _IsAdd;
        private bool _IsUpdate;
        private bool _IsDelete;
        private bool _IsView;

        public UserAccessPages()
        {
            Initialize();
        }
        public void Initialize()
        {
            _FK_Pages = 0;
            _IsAdd = true;
            _IsUpdate = true;
            _IsDelete = true;
            _IsView = true;
        }
        public Int64 FK_Pages
        {
            get { return _FK_Pages; }
            set { _FK_Pages = value; }
        }
        public bool IsAdd
        {
            get { return _IsAdd; }
            set { _IsAdd = value; }
        }
        public bool IsDelete
        {
            get { return _IsDelete; }
            set { _IsDelete = value; }
        }
        public bool IsUpdate
        {
            get { return _IsUpdate; }
            set { _IsUpdate = value; }
        }
        public bool IsView
        {
            get { return _IsView; }
            set { _IsView = value; }
        }
    }
}
