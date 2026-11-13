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
using System.Collections;
using System.Data;
using System.Threading.Tasks;
using WebPharma.DataAccess.Masters;
using WebPharma.Interface.Masters;
using WebPharma.Interface.General;
using WebPharma.Business.Masters;
 
namespace WebPharma.Business.Masters
{
	public class BlUsers : IUsers
	{
 
		private byte _UserAction;
		private Int16 _PageIndex;
		private Int16 _PageSize; 
		private long _ID_Users;
		private long _FK_Customer;
		private long _FK_Employee;
		private string _Name;
		private string _Username;
		private string _Password;
		private DateTime _LoginTime;
		private DateTime _LogoutTime;
		private long _FK_UserGroup;
		private DateTime _LastPasswordChange;
		private Boolean _Active;
		private string _UserCode;
		private DateTime _EnteredOn;
		private long _FK_Client;
        private string _NewPassword;


        private string _Photo;
        private Byte[] _PhotoData;
        private string _PhotoSmall;
        private long _FK_Store;
        private Byte[] _PhotoDataSmall;
        private long _FK_Reason;

        private string _DailyPassword;

        private string _UserName;

        private string _AuditData;
 
		public BlUsers()
		{
			Initialize();
		}
		public void Initialize()
		{
			_ID_Users = 0;
			_FK_Customer = 0;
			_FK_Employee = 0;
			_Name = string.Empty;
			_Username = string.Empty;
			_Password = string.Empty;
			_LoginTime = DateTime.Now;
			_LogoutTime = DateTime.Now;
			_FK_UserGroup = 0;
			_LastPasswordChange = DateTime.Now;
			_Active = false;
			_UserCode = string.Empty;
			_EnteredOn = DateTime.Now;
			_FK_Client = 1;
            _UserAction = 1;
            _PageIndex = 1;
            _PageSize = 10;
            _NewPassword = string.Empty;

            _Photo = string.Empty;
            _PhotoData = null;
            _PhotoSmall = string.Empty;
            _PhotoDataSmall = null;
            _FK_Store = 0;
            _DailyPassword = string.Empty;
            _FK_Reason = 0;
            _Username = string.Empty;
            _AuditData = string.Empty;

		}
        public string AuditData
        {
            get { return _AuditData; }
            set { _AuditData = value; }
        }
        public string Username
        {
            get { return _Username; }
            set { _Username = value; }
        }

        public string Photo
        {
            get { return _Photo; }
            set { if (CheckData("Photo", value)) _Photo = value; }
        }
        public Byte[] PhotoData
        {
            get { return _PhotoData; }
            set { _PhotoData = value; }
        }

        public Byte[] PhotoDataSmall
        {
            get { return _PhotoDataSmall; }
            set { _PhotoDataSmall = value; }
        }
        public string PhotoSmall
        {
            get { return _PhotoSmall; }
            set { _PhotoSmall = value; }
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
		public long MasterId
		{
			get { return _ID_Users; }
			set { _ID_Users = value; }
		}	
		public long FK_Customer
		{
			get { return _FK_Customer; }
			set { _FK_Customer = value; }
		}
		public long FK_Employee
		{
			get { return _FK_Employee; }
			set { _FK_Employee = value; }
		}
		public string Name
		{
			get { return _Name; }
			set { _Name = value; }
		}
		
		public string Password
		{
			get { return _Password; }
			set { _Password = value; }
		}
        public string NewPassword
        {
            get { return _NewPassword; }
            set { _NewPassword = value; }
        }
		public DateTime LoginTime
		{
			get { return _LoginTime; }
			set { _LoginTime = value; }
		}
		public DateTime LogoutTime
		{
			get { return _LogoutTime; }
			set { _LogoutTime = value; }
		}
		public long FK_UserGroup
		{
			get { return _FK_UserGroup; }
			set { _FK_UserGroup = value; }
		}
		public DateTime LastPasswordChange
		{
			get { return _LastPasswordChange; }
			set { _LastPasswordChange = value; }
		}
		public Boolean Active
		{
			get { return _Active; }
			set { _Active = value; }
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
		public long FK_Client
		{
			get { return _FK_Client; }
			set { _FK_Client = value; }
		}
        public string DailyPassword
        {
            get { return _DailyPassword; }
            set { _DailyPassword = value; }
        }
        public long FK_Store
        {
            get { return _FK_Store; }
            set { _FK_Store = value; }
        }

        public long FK_Reason
        {
            get { return _FK_Reason; }
            set { _FK_Reason = value; }
        }

        public DataTable SelectCandidatePhoto(Int64 FK_User)
        {
            DataTable dtblPhoto = new DataTable();
            DalUsers dalUser = new DalUsers();
            dtblPhoto = dalUser.SelectUserPhoto(FK_User);
            return dtblPhoto;
        }

        public bool CheckData(string fieldName, object value)
        {
            string errorMessage;
            errorMessage = "";
            switch (fieldName)
            {

                case "Photo":
                    //errorMessage = "Photo Already Exists";
                    break;
                default:
                    break;
            }
            if (errorMessage != "")
            {
                //_ErrorList.Add(errorMessage.ToString());
                return false;
            }
            else
                return true;
        }
		public long InsertData()
		{
			long status;
			DalUsers dalusers = new DalUsers();
			status = 0;
			try
			{
				_UserAction = 1;
				status = dalusers.UpdateUsers(this);
				dalusers=null;
			}
			catch (Exception ex)
			{
 
			}
			return status;
		}
 
		public long UpdateData()
		{
			_UserAction = 2;
			DalUsers dalusers = new DalUsers();
			long status;
			status = 0;
			try
			{
				status =  dalusers.UpdateUsers(this);
				dalusers=null;
			}
			catch (Exception ex)
			{
 
			}
			return status;
		}
 
		public long DeleteData()
		{
			_UserAction =3;
			DalUsers dalusers = new DalUsers();
			long status;
			status = 0;
			try
			{
				status =  dalusers.DeleteUsers(this);
				dalusers=null;
			}
			catch (Exception ex)
			{
 
			}
			return status;
		}
 
		public void SelectData()
		{
			DalUsers dalusers = new DalUsers();
			IUsers iusers;
			iusers= (IUsers)dalusers.SelectUsers(this);
			_FK_Customer = iusers.FK_Customer;
			_FK_Employee = iusers.FK_Employee;
			_Name = iusers.Name;
			_Username = iusers.Username;
			_Password = iusers.Password;
			_LoginTime = iusers.LoginTime;
			_LogoutTime = iusers.LogoutTime;
			_FK_UserGroup = iusers.FK_UserGroup;
			_LastPasswordChange = iusers.LastPasswordChange;
			_Active = iusers.Active;
            _FK_Store = iusers.FK_Store;

		}
 
		public DataTable SelectAllData()
		{
			DataTable dtbl = new DataTable();
			DalUsers dalusers = new DalUsers();
			dtbl = dalusers.SelectAllUsers(this);
			dalusers = null;
			return dtbl;
		}
        public DataTable selectFaildLogindetails()
        {
            DataTable dtF = new DataTable();
            DalUsers dalUser = new DalUsers();
            dtF = dalUser.selectFaildLogindetails(this);
            return dtF;
        }

        public long AuditTrailUpdation()
        {
            _UserAction = 1;
            DalUsers dalusr = new DalUsers();
            long status;
            status = 0;
            try
            {
                status = dalusr.UpdateAuditTrail(this);
                dalusr = null;
            }
            catch (Exception ex)
            {

            }
            return status;
        }

        public void LoginCheck()
        {
            DalUsers dalusers = new DalUsers();
            IUsers iusers;
            iusers = (IUsers)dalusers.LoginCheck(this);
            _ID_Users = iusers.MasterId;
            _Name = iusers.Name;            
            _LoginTime = iusers.LoginTime;
            _LogoutTime = iusers.LogoutTime;
            _FK_UserGroup = iusers.FK_UserGroup;
            _LastPasswordChange = iusers.LastPasswordChange;
            _FK_Client = iusers.FK_Client;
        }

        public int UpdateLoginDetails()
        {       
            DalUsers dalusers = new DalUsers();
            int changePassword;
            try
            {               
           changePassword= dalusers.UpdateLoginDetails(this);
                dalusers = null;
                
            }
            catch (Exception ex)
            {
                return 0;
            }
            return changePassword;
        }
        public long ChangePassword()
        {
            long statusId;
            DalUsers dalusers = new DalUsers();
            try
            {
                statusId = dalusers.ChangePassword(this);
                dalusers = null;
                return statusId;
            }
            catch (Exception ex)
            {
                return -1;
            }
        }
        public string SelectPassword()
        {
            DalUsers dalusers = new DalUsers();
            
            return dalusers.SelectPassword(this);
            dalusers = null;
        }
	}
    public class IUserPhoto
    {
        private string _Photo;
     
        private Byte[] _PhotoData;
        private string _PhotoSmall;
        private Byte[] _PhotoDataSmall;



        public IUserPhoto()
        {
            Initialize();
        }
        public void Initialize()
        {
            _Photo = string.Empty;        
            _PhotoData = null;
            _PhotoSmall = string.Empty;
            _PhotoDataSmall = null; 
        }

        public string Photo
        {
            get { return _Photo; }
            set { if (CheckData("Photo", value)) _Photo = value; }
        }
        public Byte[] PhotoData
        {
            get { return _PhotoData; }
            set { _PhotoData = value; }
        }
     
        public Byte[] PhotoDataSmall
        {
            get { return _PhotoDataSmall; }
            set { _PhotoDataSmall = value; }
        }
        public string PhotoSmall
        {
            get { return _PhotoSmall; }
            set { _PhotoSmall = value; }
        }

      
      
        public DataTable SelectUserPhoto(Int64 ID_User)
        {
            DataTable dtblPhoto = new DataTable();
            DalUsers dalUser = new DalUsers();
            dtblPhoto = dalUser.SelectUserPhoto(ID_User);
            return dtblPhoto;
        }

        public bool CheckData(string fieldName, object value)
        {
            string errorMessage;
            errorMessage = "";
            switch (fieldName)
            {

                case "Photo":
                    //errorMessage = "Photo Already Exists";
                    break;
                case "Sign":
                    // errorMessage = "Sign Already Exists";
                    break;


                default:
                    break;
            }
            if (errorMessage != "")
            {
                //_ErrorList.Add(errorMessage.ToString());
                return false;
            }
            else
                return true;
        }
    }
}
