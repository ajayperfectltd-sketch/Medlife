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


 
namespace WebPharma.Business.Masters
{
	public class BlEmployee : IEmployee
	{
 
		private byte _UserAction;
		private Int16 _PageIndex;
		private Int16 _PageSize; 
		private long _ID_Employee;
		private string _EmployeeCode;
		private string _Name;
		private string _Address;
        private DateTime _Dob;
		private string _Phone;
		private Boolean  _Salesman;
		private string _Email;
		private Boolean _Active;
		private string _UserCode;
		private DateTime _EnteredOn;
		private long _FK_Client;
        private string _XmlStoreAccessDetails;
        private string _CustomerCode;
 
		public BlEmployee()
		{
			Initialize();
		}
		public void Initialize()
		{
			_ID_Employee = 0;
			_EmployeeCode = string.Empty;
			_Name = string.Empty;
			_Address = string.Empty;
            _Dob = DateTime.Now;
			_Phone = string.Empty;
            _Salesman = false;
			_Email = string.Empty;
			_Active = false;
			_UserCode = string.Empty;
            _CustomerCode = string.Empty;
			_EnteredOn = DateTime.Now;
			_FK_Client = 1;
            _PageIndex = 1;
            _PageSize = 10;
            _UserAction = 1;
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
			get { return _ID_Employee; }
			set { _ID_Employee = value; }
		}
        public string CustomerCode
        {
            get { return _CustomerCode; }
            set { _CustomerCode = value; }
        }
		public string EmployeeCode
		{
			get { return _EmployeeCode; }
			set { _EmployeeCode = value; }
		}
		public string Name
		{
			get { return _Name; }
			set { _Name = value; }
		}
		public string Address
		{
			get { return _Address; }
			set { _Address = value; }
		}
		public DateTime Dob
		{
			get { return _Dob; }
			set { _Dob = value; }
		}
		public string Phone
		{
			get { return _Phone; }
			set { _Phone = value; }
		}
        public Boolean Salesman
		{
			get { return _Salesman; }
			set { _Salesman = value; }
		}
		public string Email
		{
			get { return _Email; }
			set { _Email = value; }
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
        public string XmlStoreAccessDetails
        {
            get { return _XmlStoreAccessDetails; }
            set { _XmlStoreAccessDetails = value; }
        }

		public long InsertData()
		{
			long status;
			DalEmployee dalemployee = new DalEmployee();
			status = 0;
			try
			{
				_UserAction = 1;
				status = dalemployee.UpdateEmployee(this);
				dalemployee=null;
			}
			catch (Exception ex)
			{
 
			}
			return status;
		}
 
		public long UpdateData()
		{
			_UserAction = 2;
			DalEmployee dalemployee = new DalEmployee();
			long status;
			status = 0;
			try
			{
				status =  dalemployee.UpdateEmployee(this);
				dalemployee=null;
			}
			catch (Exception ex)
			{
 
			}
			return status;
		}
 
		public long DeleteData()
		{
			_UserAction =3;
			DalEmployee dalemployee = new DalEmployee();
			long status;
			status = 0;
			try
			{
				status =  dalemployee.DeleteEmployee(this);
				dalemployee=null;
			}
			catch (Exception ex)
			{
 
			}
			return status;
		}


        public DataTable SelectStores()
        {
            DataTable dtbl = new DataTable();
            DalEmployee dalEmployee = new DalEmployee();
            dtbl = dalEmployee.SelectStores(this);
            dalEmployee = null;
            return dtbl;
        }
		public void SelectData()
		{
			DalEmployee dalemployee = new DalEmployee();
			IEmployee iemployee;
			iemployee= (IEmployee)dalemployee.SelectEmployee(this);
			_EmployeeCode = iemployee.EmployeeCode;
			_Name = iemployee.Name;
			_Address = iemployee.Address;
			_Dob = iemployee.Dob;
			_Phone = iemployee.Phone;
            _Salesman = iemployee.Salesman;
			_Email = iemployee.Email;
			_Active = iemployee.Active;

		}

        public DataTable SelectAllData()
        {
            DataTable dtbl = new DataTable();
            DalEmployee dalEmployee = new DalEmployee();
            dtbl = dalEmployee.SelectAllEmployee(this);
            dalEmployee = null;
            return dtbl;
        }

       
    }

    public class StoreAccessDetails
    {
        private long _FK_Store;
        private Boolean _IsDefault;

        public long FK_Store
        {
            get { return _FK_Store; }
            set { _FK_Store = value; }
        }
        public Boolean IsDefault
        {
            get { return _IsDefault; }
            set { _IsDefault = value; }
        }
    }
       
}
