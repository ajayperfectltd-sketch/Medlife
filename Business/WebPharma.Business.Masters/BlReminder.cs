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
    public class BlReminder:IReminder
    {
        public BlReminder()
        {
            Initialize();
        }

        public void Initialize()
        {
            _Topic = "";
            _Message = "";
            _FromDate = DateTime.Now;
            _ToDate = DateTime.Now;
            _Type = 0;
            _FK_User = 1;
            _Active = true;
            _UserCode = "";
            _UserAction = 1;
            _UserCode = string.Empty;
            EnteredOn = DateTime.Now;
            FK_Client = 1;
            PageIndex = 1;
            PageSize = 10;
            _FK_UserGroup = 0;
        }
        private long _FK_UserGroup;

        public long FK_UserGroup
        {
            get { return _FK_UserGroup; }
            set { _FK_UserGroup = value; }
        }


        private string _XmlUser;

        public string XmlUser
        {
            get { return _XmlUser; }
            set { _XmlUser = value; }
        }
        private byte _UserAction;
        public byte UserAction
        {
            get { return _UserAction; }
            set { _UserAction = value; }
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
      
        private DateTime _EnteredOn;

        public DateTime EnteredOn
        {
            get { return _EnteredOn; }
            set { _EnteredOn = value; }
        }
       
        private string _Topic;
        private Int64 _ID_Reminder;
        public Int64 MasterId
        {
            get { return _ID_Reminder; }
            set { _ID_Reminder = value; }
        }
       
        public string Topic
        {
            get { return _Topic; }
            set { _Topic = value; }
        }
        private string _Message;

        public string Message
        {
            get { return _Message; }
            set { _Message = value; }
        }
        private DateTime _FromDate;

        public DateTime FromDate
        {
            get { return _FromDate; }
            set { _FromDate = value; }
        }
        private DateTime _ToDate;

        public DateTime ToDate
        {
            get { return _ToDate; }
            set { _ToDate = value; }
        }
        private Byte _Type;

        public Byte Type
        {
            get { return _Type; }
            set { _Type = value; }
        }
        private long _FK_User;

        public long FK_User
        {
            get { return _FK_User; }
            set { _FK_User = value; }
        }
        private Boolean _Active;

        public Boolean Active
        {
            get { return _Active; }
            set { _Active = value; }
        }
        private string _UserCode;

        public string UserCode
        {
            get { return _UserCode; }
            set { _UserCode = value; }
        }
        public long InsertReminder()
        {
            long Status = 0;
            DalReminder dlReminder = new DalReminder();
            try
            {
                _UserAction = 1;
                Status = dlReminder.UpdateReminder(this);
            }
            catch (Exception ex)
            {
            }
            return Status;
        }
        public long UpdateReminder()
        {
            long Status = 0;
            DalReminder dlReminder = new DalReminder();
            try
            {
                _UserAction = 2;
                Status = dlReminder.UpdateReminder(this);
            }
            catch (Exception ex)
            {
            }
            return Status;

        }
        public long DeleteData()
        {
            long Status = 0;
            DalReminder dlReminder = new DalReminder();
            try
            {
                Status = dlReminder.DeleteReminder(this);
                dlReminder = null;
            }
            catch (Exception ex)
            {
            }
            return Status;
        }
        public void SelectReminder()
        {
            DalReminder dlReminder = new DalReminder();
            IReminder IRem;
            IRem = (IReminder)dlReminder.SelectReminder(this);
            _Active = IRem.Active;
            _Topic = IRem.Topic;
            _Message = IRem.Message;
            _FromDate = IRem.FromDate;
            _ToDate = IRem.ToDate;
            _Type = IRem.Type;
            IRem = null;
        }
        public DataTable SelectUserForReminder()
        {
            DalReminder DlReminder = new DalReminder();
            DataTable dt = DlReminder.SelectUserForReminder(this);
            DlReminder = null;
            return dt;
        }
        public DataTable SelectAllReminder()
        {
            DalReminder dlReminder = new DalReminder();
            DataTable dtbl = new DataTable();
            dtbl = dlReminder.SelectAllReminder(this);
            dlReminder = null;
            return dtbl;
        }
        public DataTable GetReminder()
        {
            DataTable dtbl = new DataTable();
            DalReminder dlReminder = new DalReminder();
            dtbl = dlReminder.GetReminder(this);
            dlReminder = null;
            return dtbl;

        }
        public DataTable GetReminderNew()
        {
            DataTable dtbl = new DataTable();
            DalReminder dlReminder = new DalReminder();
            dtbl = dlReminder.GetReminderNew(this);
            dlReminder = null;
            return dtbl;

        }
        public DataTable GetReminderDetails()
        {

            DataTable dtbl = new DataTable();
            DalReminder dlReminder = new DalReminder();
            dtbl = dlReminder.GetReminderDetails(this);
            dlReminder = null;
            return dtbl;
        }
        public DataTable GetReminderDetailsView()
        {

            DataTable dtbl = new DataTable();
            DalReminder dlReminder = new DalReminder();
            dtbl = dlReminder.GetReminderDetailsView(this);
            dlReminder = null;
            return dtbl;
        }
    }
    public class IReminderDetails
    {
        private long _FK_User;

        public long FK_User
        {
            get { return _FK_User; }
            set { _FK_User = value; }
        }
    }
   
}
