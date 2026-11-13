/*----------------------------------- Business Layer Class -----------------------------------------------------------------------------------------------------------------------
Purpose    : 
Author     : Bibin Tc
Copyright  : Perfect Software Solutions (Clt) Pvt Ltd
Created on : 03/05/2018
----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
WebPharma
On			By			OMID
----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
23-01-2019  VINISHA Removed PosModule,DisableNegativeStock,PolePortName And Added Shift,BillType
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
	public class BlMachine : IMachine
	{
 
		private byte _UserAction;
		private Int16 _PageIndex;
		private Int16 _PageSize; 
		private long _ID_Machine;
		private string _MecName;
		private string _MecIP;
		private string _MecAddress;
		private string _UserCode;
		private DateTime _EnteredOn;
		private long _FK_Client;
        private long _FK_Store;
        private long _FK_Shift;
        private long _FK_BillType;
        private string _PrintModule;
        private long _FK_Counter;
        private Boolean _AutoPrint;
        private long _FK_Reason;

 
		public BlMachine()
		{
			Initialize();
		}
		public void Initialize()
		{
			_ID_Machine = 0;
            _FK_Counter = 0;
            _FK_Store = 0;
            _FK_Shift = 0;
            _FK_BillType = 0;
			_MecName = string.Empty;
			_MecIP = string.Empty;
			_MecAddress = string.Empty;
			_UserCode = string.Empty;
			_EnteredOn = DateTime.Now;
			_FK_Client = 1;
            _PageIndex = 1;
            _PageSize = 10;
            _UserAction = 1;
            _AutoPrint = false;
            _PrintModule = string.Empty;
            _FK_Reason = 0;
		}

        public Boolean AutoPrint
        {
            get { return _AutoPrint; }
            set { _AutoPrint = value; }
        }
         

        public string PrintModule
        {
            get { return _PrintModule; }
            set { _PrintModule = value; }
        } 
        public long FK_Counter
        {
            get { return _FK_Counter; }
            set { _FK_Counter = value; }
        }
        public long FK_Store
        {
            get { return _FK_Store; }
            set { _FK_Store = value; }
        }
        public long FK_Shift
        {
            get { return _FK_Shift; }
            set { _FK_Shift = value; }
        }
        public long FK_BillType
        {
            get { return _FK_BillType; }
            set { _FK_BillType = value; }
        }
        public long FK_Reason
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
		public long MasterId
		{
			get { return _ID_Machine; }
			set { _ID_Machine = value; }
		}
         
	
		public string MecName
		{
			get { return _MecName; }
			set { _MecName = value; }
		}
		public string MecIP
		{
			get { return _MecIP; }
			set { _MecIP = value; }
		}
		public string MecAddress
		{
			get { return _MecAddress; }
			set { _MecAddress = value; }
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

		public long InsertData()
		{
			long status;
			DalMachine dalmachine = new DalMachine();
			status = 0;
			try
			{
				_UserAction = 1;
				status = dalmachine.UpdateMachine(this);
				dalmachine=null;
			}
			catch (Exception ex)
			{
 
			}
			return status;
		}
 
		public long UpdateData()
		{
			_UserAction = 2;
			DalMachine dalmachine = new DalMachine();
			long status;
			status = 0;
			try
			{
				status =  dalmachine.UpdateMachine(this);
				dalmachine=null;
			}
			catch (Exception ex)
			{
 
			}
			return status;
		}
 
		public long DeleteData()
		{
			_UserAction =3;
			DalMachine dalmachine = new DalMachine();
			long status;
			status = 0;
			try
			{
				status =  dalmachine.DeleteMachine(this);
				dalmachine=null;
			}
			catch (Exception ex)
			{
 
			}
			return status;
		}
 
		public void SelectData()
		{
			DalMachine dalmachine = new DalMachine();
			IMachine imachine;
			imachine= (IMachine)dalmachine.SelectMachine(this);
			_MecName = imachine.MecName;
			_MecIP = imachine.MecIP;
			_MecAddress = imachine.MecAddress;
            _FK_Store = imachine.FK_Store;
            _FK_Counter = imachine.FK_Counter;
            _PrintModule = imachine.PrintModule;
            _AutoPrint = imachine.AutoPrint;
		}
 
		public DataTable SelectAllData()
		{
			DataTable dtbl = new DataTable();
			DalMachine dalMachine = new DalMachine();
			dtbl = dalMachine.SelectAllMachine(this);
			dalMachine = null;
			return dtbl;
		}
	}
}
