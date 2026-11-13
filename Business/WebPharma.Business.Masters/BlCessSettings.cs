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
    public class BlCessSettings:ICessSettings
    {
        private byte _UserAction;
		private Int16 _PageIndex;
		private Int16 _PageSize;
        private long _ID_CessSettings;
        private DateTime _EffectFrom;
        private decimal _CGST;
        private decimal _SGST;
        private decimal _IGST;		private string _UserCode;		private DateTime _EnteredOn;		private long _FK_Client;
        private DateTime _FromDate;
        private DateTime _ToDate;

        public BlCessSettings()
		{
			Initialize();
		}
		public void Initialize()
		{
            _ID_CessSettings = 0;
            _EffectFrom = DateTime.Now;
            _CGST = 0;
            _SGST = 0;
            _IGST = 0;
			_UserCode = string.Empty;
			_EnteredOn = DateTime.Now;
			_FK_Client = 1;
            _PageIndex = 1;
            _PageSize = 10;
            _UserAction = 1;
            _FromDate = DateTime.Now;
            _ToDate = DateTime.Now;
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
			get { return _ID_CessSettings; }
            set { _ID_CessSettings = value; }
		}

        public DateTime EffectFrom		{
            get { return _EffectFrom; }
            set { _EffectFrom = value; }		}		public decimal CGST		{
            get { return _CGST; }
            set { _CGST = value; }		}
        public decimal SGST		{			get { return _SGST; }
            set { _SGST = value; }		}
        public decimal IGST		{
            get { return _IGST; }
            set { _IGST = value; }		}		public string UserCode		{			get { return _UserCode; }			set { _UserCode = value; }		}		public DateTime EnteredOn		{			get { return _EnteredOn; }			set { _EnteredOn = value; }		}		public long FK_Client		{			get { return _FK_Client; }			set { _FK_Client = value; }		}
        public DateTime FromDate
        {
            get { return _FromDate; }
            set { _FromDate = value; }
        }
        public DateTime ToDate
        {
            get { return _ToDate; }
            set { _ToDate = value; }
        }
		public long InsertData()
		{
			long status;
            DalCessSettings dalcesssettings = new DalCessSettings();
			status = 0;
			try
			{
				_UserAction = 1;
                status = dalcesssettings.UpdateCessSettings(this);
                dalcesssettings = null;
			}
			catch (Exception ex)
			{
 
			}
			return status;
		}
        public void SelectData()
        {
            DalCessSettings dalcesssettings = new DalCessSettings();
            ICessSettings iCessSettings;
            iCessSettings = (ICessSettings)dalcesssettings.SelectCessSettings(this);
            _EffectFrom = iCessSettings.EffectFrom;
            _CGST = iCessSettings.CGST;
            _SGST = iCessSettings.SGST;
            _IGST = iCessSettings.IGST;

        }

        public DataTable SelectAllData()
        {
            DataTable dtbl = new DataTable();
            DalCessSettings dalcesssettings = new DalCessSettings();
            dtbl = dalcesssettings.SelectAllCessSettings(this);
            dalcesssettings = null;
            return dtbl;
        }
 
    }
}
