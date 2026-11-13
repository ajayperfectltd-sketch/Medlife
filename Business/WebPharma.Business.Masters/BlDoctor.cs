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
    public class BlDoctor : IDoctor
    {
        private byte _UserAction;
        private Int16 _PageIndex;
        private Int16 _PageSize;
        private string _ShortName;
        private string _DoctorName;
        private string _Qualification;
        private string _ClinicName;
        private DateTime _EnteredOn;
        private long _FK_Client;
        private string _UserCode;
        private long _ID_Doctor;
        private long _Reason;
        public BlDoctor()
        {
            Initialize();
        }
        public void Initialize()
        {
            _UserAction = 1;
            _PageIndex =1;
            _PageSize = 10;
            _ShortName = string.Empty;
            _DoctorName = string.Empty;
            _ClinicName = string.Empty;
            _Qualification = string.Empty;
            _EnteredOn = DateTime.Now;
            _FK_Client = 1;
            _Reason = 1;
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
        public string DoctorName
        {
            get { return _DoctorName; }
            set { _DoctorName = value; }
        }
        public string ClinicName
        {
            get { return _ClinicName; }
            set { _ClinicName = value; }
        }
        public string Qualification
        {
            get { return _Qualification; }
            set { _Qualification = value; }
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
            get { return _ID_Doctor; }
            set { _ID_Doctor = value; }
        }
        public long Reason
        {
            get { return _Reason; }
            set { _Reason = value; }
        }
        public long InsertData()
        {
            long status;
            DalDoctor daldoctor = new DalDoctor();
            status = 0;
            try
            {
                _UserAction = 1;
                status = daldoctor.UpdateDoctor(this);
                daldoctor = null;
            }
            catch (Exception ex)
            {

            }
            return status;
        }

        public long UpdateData()
        {
            _UserAction = 2;
            DalDoctor daldoctor = new DalDoctor();
            long status;
            status = 0;
            try
            {
                status = daldoctor.UpdateDoctor(this);
                daldoctor = null;
            }
            catch (Exception ex)
            {

            }
            return status;
        }

        public long DeleteData()
        {
            _UserAction = 3;
            DalDoctor daldoctor = new DalDoctor();
            long status;
            status = 0;
            try
            {
                status = daldoctor.DeleteDoctor(this);
                daldoctor = null;
            }
            catch (Exception ex)
            {

            }
            return status;
        }

        public void SelectData()
        {
            DalDoctor daldoctor = new DalDoctor();
            IDoctor idoctor;
            idoctor = (IDoctor)daldoctor.SelectDoctor(this);
            _DoctorName = idoctor.DoctorName;
            _ClinicName = idoctor.ClinicName;
            _Qualification = idoctor.Qualification;
            

        }

        public DataTable SelectAllData()
        {
            DataTable dtbl = new DataTable();
            DalDoctor daldoctor = new DalDoctor();
            dtbl = daldoctor.SelectAllDoctor(this);
            daldoctor = null;
            return dtbl;
        }
    
    }
}
