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
    public class BlGeneric : IGeneric
    {
        private byte _UserAction;
        private Int16 _PageIndex;
        private Int16 _PageSize;
        private long _ID_Generic;
        private string _GenericName;
        private string _ShortName;
        private string _UserCode;
        private DateTime _EnteredOn;
        private long _FK_Client;
        private long _FK_Reason;
        public BlGeneric()
		{
			Initialize();
		}
        public void Initialize()
        {
            _ID_Generic = 0;
            _GenericName = string.Empty;
            _ShortName = string.Empty;
            _UserCode = string.Empty;
            _EnteredOn = DateTime.Now;
            _FK_Client = 1;
            _PageIndex = 1;
            _PageSize = 10;
            _UserAction = 1;
            _FK_Reason = 0;

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
            get { return _ID_Generic; }
            set { _ID_Generic = value; }
        }

        public string GenericName
        {
            get { return _GenericName; }
            set { _GenericName = value; }
        }
        public string ShortName
        {
            get { return _ShortName; }
            set { _ShortName = value; }
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
        public long InsertData()
        {
            long status;
            DalGeneric dalGeneric = new DalGeneric();
            status = 0;
            try
            {
                _UserAction = 1;
                status = dalGeneric.UpdateGeneric(this);
                dalGeneric = null;
            }
            catch (Exception ex)
            {

            }
            return status;
        }

        public long UpdateData()
        {
            _UserAction = 2;
            DalGeneric dalGeneric = new DalGeneric();
            long status;
            status = 0;
            try
            {
                status = dalGeneric.UpdateGeneric(this);
                dalGeneric = null;
            }
            catch (Exception ex)
            {

            }
            return status;
        }

        public long DeleteData()
        {
            _UserAction = 3;
            DalGeneric dalGeneric = new DalGeneric();
            long status;
            status = 0;
            try
            {
                status = dalGeneric.DeleteGeneric(this);
                dalGeneric = null;
            }
            catch (Exception ex)
            {

            }
            return status;
        }

        public void SelectData()
        {
            DalGeneric dalGeneric = new DalGeneric();
            IGeneric iGeneric;
            iGeneric = (IGeneric)dalGeneric.SelectGeneric(this);
            _ShortName = iGeneric.ShortName;
            _GenericName = iGeneric.GenericName;
        }

        public DataTable SelectAllData()
        {
            DataTable dtbl = new DataTable();
            DalGeneric dalGeneric = new DalGeneric();
            dtbl = dalGeneric.SelectAllGeneric(this);
            dalGeneric = null;
            return dtbl;
        }

    }
}
