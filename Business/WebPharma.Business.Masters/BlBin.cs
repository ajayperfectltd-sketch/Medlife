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
 public   class BlBin:IBin
    {
        private byte _UserAction;
        private Int16 _PageIndex;
        private Int16 _PageSize;
        private long _ID_Bin;
        private string _ShortName;
        private string _BinName;
        private string _UserCode;
        private DateTime _EnteredOn;
        private long _FK_Client;
        private long _FK_Rack;
        private long _FK_Reason;

        public BlBin()
        {
            Initialize();
        }
        public void Initialize()
        {
            _ID_Bin = 0;
            _ShortName = string.Empty;
            _UserAction = 1;
            _PageIndex = 1;
            _PageSize = 10;
            _ShortName = string.Empty;
            _BinName = string.Empty;
            _EnteredOn = DateTime.Now;
            _FK_Client = 1;
            _FK_Rack = 1;
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
        public string ShortName
        {
            get { return _ShortName; }
            set { _ShortName = value; }
        }
        public string BinName
        {
            get { return _BinName; }
            set { _BinName = value; }
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
        public string UserCode
        {
            get { return _UserCode; }
            set { _UserCode = value; }
        }
        public long MasterId
        {
            get { return _ID_Bin; }
            set { _ID_Bin = value; }
        }
        public long FK_Rack
        {
            get { return _FK_Rack; }
            set { _FK_Rack = value; }
        }
        public long FK_Reason
        {
            get { return _FK_Reason; }
            set { _FK_Reason = value; }
        }
        public long InsertData()
        {
            long status;
            DalBin dalbin = new DalBin();
            status = 0;
            try
            {
                _UserAction = 1;
                status = dalbin.UpdateBin(this); 
                   
                dalbin = null;
            }
            catch (Exception ex)
            {

            }
            return status;
        }

        public long UpdateData()
        {
            _UserAction = 2;
            DalBin dalbin = new DalBin();
            long status;
            status = 0;
            try
            {
                status = dalbin.UpdateBin(this);
                dalbin = null;
            }
            catch (Exception ex)
            {

            }
            return status;
        }

        public long DeleteData()
        {
            _UserAction = 3;
            DalBin dalbin = new DalBin();
            long status;
            status = 0;
            try
            {
                status = dalbin.DeleteBin(this);
                dalbin = null;
            }
            catch (Exception ex)
            {

            }
            return status;
        }

        public void SelectData()
        {
            DalBin dalbin = new DalBin();
            IBin ibin;
            ibin = (IBin)dalbin.SelectBin(this);
            //_DoctorName = idoctor.DoctorName;
            //_ClinicName = idoctor.ClinicName;
            //_Qualification = idoctor.Qualification;


        }

        public DataTable SelectAllData()
        {
            DataTable dtbl = new DataTable();
            DalBin dalbin = new DalBin();
            dtbl = dalbin.SelectAllBin(this);
            dalbin = null;
            return dtbl;
        }
    }
}
