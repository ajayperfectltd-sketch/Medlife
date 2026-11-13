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
    public class BlHolidaySettings:IHolidaySettings
    {
        public void Initialize()
        {
                _MasterId = 0;
                _Reason = "";
                _Date = DateTime.Now;
                _Type = 0;
                _FromTime = DateTime.Now;
                _ToTime = DateTime.Now;
                _EnteredOn = DateTime.Now;
                _FK_Client = 1;
                _PageIndex = 1;
                _PageSize = 10;
                _UserAction = 1;
                _Cancelled = false;
                _Cancelleduser = "";
                _ToDate= _FromDate = _CancelledOn = DateTime.Now;
        }
        public BlHolidaySettings()
        {
            Initialize();
        }
        private Int64 _MasterId;
        private DateTime _FromDate;

        public DateTime FromDate
        {
            get { return _FromDate; }
            set { _FromDate = value; }
        }
        private DateTime _ToDate;

        public DateTime ToDate
        {
            get { return _ToDate; }
            set { _ToDate = value; }
        }
        public Int64 MasterId
        {
            get { return _MasterId; }
            set { _MasterId = value; }
        }
       
        private string _Reason;

        public string Reason
        {
            get { return _Reason; }
            set { _Reason = value; }
        }
        private DateTime _Date;
        public DateTime Date
        {
            get { return _Date; }
            set { _Date = value; }
        }
        private Byte _Type;

        public Byte Type
        {
            get { return _Type; }
            set { _Type = value; }
        }

        private DateTime _FromTime;
        public DateTime FromTime
        {
            get { return _FromTime; }
            set { _FromTime = value; }
        }
        private DateTime _ToTime;

        public DateTime ToTime
        {
            get { return _ToTime; }
            set { _ToTime = value; }
        }

        private DateTime _EnteredOn;

        public DateTime EnteredOn
        {
            get { return _EnteredOn; }
            set { _EnteredOn = value; }
        }
        private Int64 _FK_Client;
        public Int64 FK_Client
        {
            get { return _FK_Client; }
            set { _FK_Client = value; }
        }

        private string _UserCode;

        public string UserCode
        {
            get { return _UserCode; }
            set { _UserCode = value; }
        }
        private bool _Cancelled;

        public bool Cancelled
        {
            get { return _Cancelled; }
            set { _Cancelled = value; }
        }
        private string _Cancelleduser;

        public string Cancelleduser
        {
            get { return _Cancelleduser; }
            set { _Cancelleduser = value; }
        }
        private DateTime _CancelledOn;

        public DateTime CancelledOn
        {
            get { return _CancelledOn; }
            set { _CancelledOn = value; }
        }


        private byte _UserAction;

        public byte UserAction
        {
            get { return _UserAction; }
            set { _UserAction = value; }
        }


        private Int16 _PageIndex;

        public Int16 PageIndex
        {
            get { return _PageIndex; }
            set { _PageIndex = value; }
        }


        private Int16 _PageSize;

        public Int16 PageSize
        {
            get { return _PageSize; }
            set { _PageSize = value; }
        }

        public long InsertData()
        {
            DalHolidaySettings dalHoliday = new DalHolidaySettings();
            long status = 0;
            try
            {
                _UserAction = 1;
                status = dalHoliday.UpdateHolidaySettings(this);
                dalHoliday = null;
            }
            catch (Exception ex)
            {
            }
            return status;
        }

        public long UpdateData()
        {
            DalHolidaySettings dalHoliday = new DalHolidaySettings();
            long status = 0;
            try
            {
                _UserAction = 2;
                status = dalHoliday.UpdateHolidaySettings(this);
                dalHoliday = null;
            }
            catch (Exception ex)
            {
            }
            return status;
        }
        public long DeleteData()
        {
            DalHolidaySettings dalHoliday = new DalHolidaySettings();
            long status;
            status = 0;
            try
            {
                status = dalHoliday.DeleteHoliday(this);
                dalHoliday = null;
            }
            catch (Exception ex)
            {
            }
            return status;
        }
        public void SelectData()
        {
                    DalHolidaySettings dalHoliday = new DalHolidaySettings();
                    IHolidaySettings Iholiday = (IHolidaySettings)dalHoliday.SelectHolidaySettings(this);
                    _Reason= Iholiday.Reason; 
                    _Date= Iholiday.Date; 
                    _Type= Iholiday.Type; 
                    _FromTime= Iholiday.FromTime;
                    _ToTime= Iholiday.ToTime;



        }
        public DataTable SelectAllData()
        {
                            DataTable dtbl = new DataTable();
                            DalHolidaySettings dal = new DalHolidaySettings();
                            dtbl = dal.SelectAllHolidaySettings(this);
                            dal = null;
                            return dtbl;
        }




    }
}
