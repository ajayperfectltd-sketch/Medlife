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
	public class BlCounter : ICounter
	{
 
		private byte _UserAction;
		private Int16 _PageIndex;
		private Int16 _PageSize; 
		private long _ID_Counter;
		private string _Name;
        private string _ShortName;
		private long _FK_Store;
        private Boolean _Active;
		private string _UserCode;
		private DateTime _EnteredOn;
		private long _FK_Client;
        private long _FK_AccountHead;
        private long _FK_AccountHeadSub;
        private long _FK_Reason;
        private string _XmlShiftDetails;
		public BlCounter()
		{
			Initialize();
		}
		public void Initialize()
		{
			_ID_Counter = 0;
			_Name = string.Empty;
			_FK_Store = 0;
            _Active = false;
			_UserCode = string.Empty;
			_EnteredOn = DateTime.Now;
			_FK_Client = 1;
            _PageIndex = 1;
            _PageSize = 10;
            _UserAction = 1;
            _FK_AccountHead = 0;
            _FK_AccountHeadSub = 0;
            _FK_Reason = 0;
            _XmlShiftDetails = string.Empty;
            _ShortName = string.Empty;
		}
        public long FK_AccountHead
        {
            get { return _FK_AccountHead; }
            set { _FK_AccountHead = value; }
        }
        public long FK_AccountHeadSub
        {
            get { return _FK_AccountHeadSub; }
            set { _FK_AccountHeadSub = value; }
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
			get { return _ID_Counter; }
			set { _ID_Counter = value; }
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
		public long FK_Store
		{
			get { return _FK_Store; }
			set { _FK_Store = value; }
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
        public long FK_Reason
        {
            get { return _FK_Reason; }
            set { _FK_Reason = value; }
        }
        public string XmlShiftDetails
        {
            get { return _XmlShiftDetails; }
            set { _XmlShiftDetails = value; }
        }
   
        
		public long InsertData()
		{
			long status;
			DalCounter dalcounter = new DalCounter();
			status = 0;
			try
			{
				_UserAction = 1;
				status = dalcounter.UpdateCounter(this);
				dalcounter=null;
			}
			catch (Exception ex)
			{
 
			}
			return status;
		}
 
		public long UpdateData()
		{
			_UserAction = 2;
			DalCounter dalcounter = new DalCounter();
			long status;
			status = 0;
			try
			{
				status =  dalcounter.UpdateCounter(this);
				dalcounter=null;
			}
			catch (Exception ex)
			{
 
			}
			return status;
		}
 
		public long DeleteData()
		{
			_UserAction =3;
			DalCounter dalcounter = new DalCounter();
			long status;
			status = 0;
			try
			{
				status =  dalcounter.DeleteCounter(this);
				dalcounter=null;
			}
			catch (Exception ex)
			{
 
			}
			return status;
		}
 
		public void SelectData()
		{
			DalCounter dalcounter = new DalCounter();
			ICounter icounter;
			icounter= (ICounter)dalcounter.SelectCounter(this);
			_Name = icounter.Name;
			_FK_Store = icounter.FK_Store;
            _FK_AccountHead = icounter.FK_AccountHead;
            _FK_AccountHeadSub = icounter.FK_AccountHeadSub;
		}
 
		public DataTable SelectAllData()
		{
			DataTable dtbl = new DataTable();
            DalCounter dalcounter = new DalCounter();
            dtbl = dalcounter.SelectAllCounter(this);
            dalcounter = null;
			return dtbl;
		}
        public DataTable selectCounterShiftDetails()
        {
            DataTable dtbl = new DataTable();
            DalCounter dalcounter = new DalCounter();
            dtbl = dalcounter.selectCounterShiftDetails(this);
            dalcounter = null;
            return dtbl;
        }
       
	}
    public class IShiftDetails
    {

        private Int64 _FK_Counters;
        private Int64 _FK_Shift;
        private string _ShiftName;

        private Int64 _ID_CounterWiseShift;
        private decimal _Percentage;

        public Int64 ID_CounterWiseShift
        {
            get { return _ID_CounterWiseShift; }
            set { _ID_CounterWiseShift = value; }
        }
        public Int64 FK_Shift
        {
            get { return _FK_Shift; }
            set { _FK_Shift = value; }
        }
        public string ShiftName
        {
            get { return _ShiftName; }
            set { _ShiftName = value; }
        }


    }
}
