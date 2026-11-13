using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebPharma.DataAccess.Masters;
using WebPharma.Interface.Masters;

namespace WebPharma.Business.Masters
{
   public  class BlScheduleMaster: IScheduleMaster
    {
        private byte _UserAction;
        private Int16 _PageIndex;
        private Int16 _PageSize;
        private long _ID_Manufacturer;
        private string _ShortName;
        private string _Name;
        private string _UserCode;
        private DateTime _EnteredOn;
        private long _FK_Client;
        private long _FK_Reason;
    
    public BlScheduleMaster()
		{
			Initialize();
		}
		public void Initialize()
		{
			_ID_Manufacturer = 0;
			_ShortName = string.Empty;
			_Name = string.Empty;
			_UserCode = string.Empty;
			_EnteredOn = DateTime.Now;
			_FK_Client = 1;
            
			_UserAction=1;
            _PageIndex = 1;
            _PageSize = 10;
            _FK_Reason = 1;
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
			get { return _ID_Manufacturer; }
			set { _ID_Manufacturer = value; }
		}	
		public string ShortName
		{
			get { return _ShortName; }
			set { _ShortName = value; }
		}
		public string Name
		{
			get { return _Name; }
			set { _Name = value; }
		}
		
        public long FK_Reason
        {
            get { return _FK_Reason; }
            set { _FK_Reason = value; }
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
            DalScheduleMaster dalSchedule = new DalScheduleMaster();
            status = 0;
            try
            {
                _UserAction = 1;
                status = dalSchedule.UpdateShedulueMaster(this);
                dalSchedule = null;
            }
            catch (Exception ex)
            {

            }
            return status;
        }

        public long UpdateData()
        {
            _UserAction = 2;
            DalScheduleMaster dalSchdule = new DalScheduleMaster();
            long status;
            status = 0;
            try
            {
                status = dalSchdule.UpdateShedulueMaster(this);
                dalSchdule = null;
            }
            catch (Exception ex)
            {

            }
            return status;
        }

        public long DeleteData()
        {
            _UserAction = 3;
            DalScheduleMaster dalSchdule = new DalScheduleMaster();
            long status;
            status = 0;
            try
            {
                status = dalSchdule.DeleteSchedule(this);
                dalSchdule = null;
            }
            catch (Exception ex)
            {

            }
            return status;
        }

        public void SelectData()
        {
            DalScheduleMaster dalSchdule = new DalScheduleMaster();
            IScheduleMaster iScheduleMaster;
            iScheduleMaster = (IScheduleMaster)dalSchdule.SelectSchedule(this);
            _ShortName = iScheduleMaster.ShortName;
            _Name = iScheduleMaster.Name;

        }

        public DataTable SelectAllData()
        {
            DataTable dtbl = new DataTable();
            DalScheduleMaster dalSchdule = new DalScheduleMaster();
            dtbl = dalSchdule.SelectAllScheduleMaster(this);
            dalSchdule = null;
            return dtbl;
        }
	}
}
