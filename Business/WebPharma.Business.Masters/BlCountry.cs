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
   public class BlCountry:ICountry
    {
        private byte _UserAction;
        private string _UserCode;
        private DateTime _EnteredOn;
        private Int64 _FK_Client;      
        private Int16 _PageIndex;
        private Int16 _PageSize;
        private Int64 _ID_Country;
        private string _CountryName;
        private string _CountryCode;
        private Int64 _FK_Currency;
        private long _FK_Reason;

        public BlCountry()
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
            _ID_Country = 0;
            _CountryName = string.Empty;
            _CountryCode = string.Empty;          
            _FK_Currency = 0;
            _FK_Reason = 0;
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
            get { return _ID_Country; }
            set { _ID_Country = value; }
        }
        public string CountryName
        {
            get { return _CountryName; }
            set { _CountryName = value; }
        }
        public string CountryCode
        {
            get { return _CountryCode; }
            set { _CountryCode = value; }
        }      
        public Int64 FK_Currency
        {
            get { return _FK_Currency; }
            set { _FK_Currency = value; }
        }
        public long FK_Reason
        {
            get { return _FK_Reason; }
            set { _FK_Reason = value; }
        }

        public long InsertData()
        {
            long status;
            DalCountry dalcountry = new DalCountry();
            status = 0;
            try
            {
                _UserAction = 1;
                status = dalcountry.UpdateCountry(this);
                dalcountry = null;
            }
            catch (Exception ex)
            {
            }
            return status;
        }
        public long UpdateData()
        {
            DalCountry dalcountry = new DalCountry();
            long status;
            status = 0;
            try
            {
                _UserAction = 2;
                status = dalcountry.UpdateCountry(this);
                dalcountry = null;
            }
            catch (Exception ex)
            {
            }
            return status;
        }
        public long DeleteData()
        {
            DalCountry dalcountry = new DalCountry();
            long status;
            status = 0;
            try
            {
                status = dalcountry.DeleteCountry(this);
                dalcountry = null;
            }
            catch (Exception ex)
            {
            }
            return status;
        }
        public void SelectData()
        {
            DalCountry dalcountry = new DalCountry();
            ICountry icountry;
            icountry = (ICountry)dalcountry.SelectCountry(this);
            _CountryCode = icountry.CountryCode;
            _CountryName = icountry.CountryName;
            _FK_Currency = icountry.FK_Currency;          
            dalcountry = null;
        }
        public DataTable SelectAllData()
        {
            DataTable dtbl = new DataTable();
            DalCountry dalcountry = new DalCountry();
            dtbl = dalcountry.SelectAllCountry(this);
            dalcountry = null;
            return dtbl;
        }
    }
}
