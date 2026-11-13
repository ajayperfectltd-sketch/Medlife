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
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Threading.Tasks;
using WebPharma.DataAccess.WebPharma;
using WebPharma.Interface.WebPharma;

namespace WebPharma.Business.WebPharma
{
    public class BlDayBegin : IDayBegin
    {


        private byte _UserAction;
        private ArrayList _ErrorList;
        private const string MODULENAME = "DayBegin";
        private const string TABLENAME = "DayBegin";
        private Int32 _FKReason;
        private Int32 _FKMachine;
        private string _DbName;
        private Int16 _SortOrder;
        private string _SqlUpdateQuery;
        private string _AuditData;
        private string _TransMode;
        private Int64 _ID_DayBegin;
        private DateTime _DbDate;
        private DateTime _DbPreviousDate;
        private string _OfficerCode;
        private DateTime _OfficerDate;
        private Boolean _Cancelled;
        private Boolean _Passed;
        private Int16 _BranchCode;
        private Int16 _BranchCodeUser;
        private string _UserCode;
        private DateTime _UserDate;
        private Boolean _OfflineEntry;
        private string _XmlAttendance;
       private string _DBCheckSum;
        private long _FK_Client;

        private decimal _openingbalance;
        private decimal _openingliability;
        private decimal _AmntDifference;
      
      

        public BlDayBegin()
        {
            Initialize();
        }
        public void Initialize()
        {
            
             _openingbalance=0;
      _openingliability=0;
        _AmntDifference=0;

            _ID_DayBegin = 0;
            _DbDate = DateTime.Now;
            _DbPreviousDate = DateTime.Now;
            _OfficerCode = string.Empty;
            _OfficerDate = DateTime.Now;
            _Cancelled = false;
            _Passed = false;
            _BranchCode = 0;
            _BranchCodeUser = 0;
            _UserCode = string.Empty;
            _UserDate = DateTime.Now;
            _OfflineEntry = false;
            _FK_Client = 0;
        //    _UserAction = (byte)BlPublic.UserAction.None; ;
            _ErrorList = new ArrayList();
            _DbName = string.Empty;
            _FKReason = 0;
            _FKMachine = 0;
            _SqlUpdateQuery = string.Empty;
            _AuditData = string.Empty;
            _TransMode = string.Empty;
            _XmlAttendance = string.Empty;
            _DBCheckSum = string.Empty;
            _UserAction = 0;
        }
          public long FK_Client
        {
            get { return _FK_Client; }
            set { _FK_Client = value; }
        }
    
        public string DBCheckSum
        {
            get { return _DBCheckSum; }
            set { _DBCheckSum = value; }
        }
        public string XmlAttendance
        {
            get { return _XmlAttendance; }
            set { _XmlAttendance = value; }
        }
        public ArrayList ErrorList
        {
            get { return _ErrorList; }
        }
        public byte UserAction
        {
            get { return _UserAction; }
            set { _UserAction = value; }
        }
        public string ModuleName
        {
            get { return MODULENAME; }
        }
        public string TableName
        {
            get { return TABLENAME; }
        }
        public Int32 MachineCode
        {
            get { return _FKMachine; }
            set { _FKMachine = value; }
        }
        public Int32 ReasonCode
        {
            get { return _FKReason; }
            set { _FKReason = value; }
        }
        public string DbName
        {
            get { return _DbName; }
            set { _DbName = value; }
        }

        public Int64 MasterID
        {
            get { return _ID_DayBegin; }
            set { _ID_DayBegin = value; }
        }
        public Int16 SortOrder
        {
            get { return _SortOrder; }
            set { _SortOrder = value; }
        }
        public string SqlUpdateQuery
        {
            get { return _SqlUpdateQuery; }
            set { _SqlUpdateQuery = value; }
        }
        public string AuditData
        {
            get { return _AuditData; }
            set { _AuditData = value; }
        }
        public string TransMode
        {
            get { return _TransMode; }
            set { _TransMode = value; }
        }
        public DateTime DbDate
        {
            get { return _DbDate; }
            set { if (CheckData("DbDate", value)) _DbDate = value; }
        }
        public DateTime DbPreviousDate
        {
            get { return _DbPreviousDate; }
            set { if (CheckData("DbPreviousDate", value)) _DbPreviousDate = value; }
        }
        public string OfficerCode
        {
            get { return _OfficerCode; }
            set { if (CheckData("OfficerCode", value)) _OfficerCode = value; }
        }
        public DateTime OfficerDate
        {
            get { return _OfficerDate; }
            set { if (CheckData("OfficerDate", value)) _OfficerDate = value; }
        }
        public Boolean Cancelled
        {
            get { return _Cancelled; }
            set { if (CheckData("Cancelled", value)) _Cancelled = value; }
        }
        public Boolean Passed
        {
            get { return _Passed; }
            set { if (CheckData("Passed", value)) _Passed = value; }
        }
        public Int16 BranchCodeUser
        {
            get { return _BranchCodeUser; }
            set { if (CheckData("BranchCodeUser", value)) _BranchCodeUser = value; }
        }
        public string UserCode
        {
            get { return _UserCode; }
            set { if (CheckData("UserCode", value)) _UserCode = value; }
        }
        public DateTime UserDate
        {
            get { return _UserDate; }
            set { if (CheckData("UserDate", value)) _UserDate = value; }
        }
        public Boolean OfflineEntry
        {
            get { return _OfflineEntry; }
            set { if (CheckData("OfflineEntry", value)) _OfflineEntry = value; }
        }
        public Int16 BranchCode
        {
            get { return _BranchCode; }
            set { _BranchCode = value; }
        }

          public decimal openingbalance
        {
            get { return _openingbalance; }
            set { _openingbalance = value; }
        }
          public decimal openingliability
        {
            get { return _openingliability; }
            set { _openingliability = value; }
        }
         public decimal AmntDifference
        {
            get { return _AmntDifference; }
            set { _AmntDifference = value; }
        }
       

        public bool CheckData(string fieldName, object value)
        {
            string errorMessage;
            errorMessage = "";
            switch (fieldName)
            {
                case "DbDate":
                    // errorMessage = "DbDate Already Exists";
                    break;
                case "DbPreviousDate":
                    // errorMessage = "DbPreviousDate Already Exists";
                    break;
                case "OfficerCode":
                    // errorMessage = "OfficerCode Already Exists";
                    break;
                case "OfficerDate":
                    // errorMessage = "OfficerDate Already Exists";
                    break;
                case "Cancelled":
                    // errorMessage = "Cancelled Already Exists";
                    break;
                case "Passed":
                    // errorMessage = "Passed Already Exists";
                    break;
                case "BranchCode":
                    // errorMessage = "BranchCode Already Exists";
                    break;
                case "BranchCodeUser":
                    // errorMessage = "BranchCodeUser Already Exists";
                    break;
                case "UserCode":
                    // errorMessage = "UserCode Already Exists";
                    break;
                case "UserDate":
                    // errorMessage = "UserDate Already Exists";
                    break;
                case "OfflineEntry":
                    // errorMessage = "OfflineEntry Already Exists";
                    break;

                default:
                    break;
            }
            if (errorMessage != "")
            {
                _ErrorList.Add(errorMessage.ToString());
                return false;
            }
            else
                return true;
        }



        public long InsertData()
        {


            long status;
            DalDayBegin dalDayBegin = new DalDayBegin();

            status = 0;
            try
            {
                _UserAction = 1;
                status = dalDayBegin.UpdateDayBegin(this);
                dalDayBegin = null;
            }
            catch (Exception ex)
            {

            }
            return status;
        }


        //public DataTable PreviouusDayEndCheck()
        //{
        //    DalDayBegin DalDayBegins = new DalDayBegin();
        //    try
        //    {
        //        return DalDayBegins.PreviouusDayEndCheck(this);

        //    }
        //    catch (Exception e)
        //    {
        //        throw e;
        //    }
        //    finally
        //    {
        //        DalDayBegins = null;
        //    }
        //}

        //public DataTable DayProcessValidate(string Transmode, Int16 BranchCode, DateTime DayDate, byte ProcessNo)
        //{
        //    DalDayBegin DalDayBegin = new DalDayBegin();
        //    return DalDayBegin.DayProcessValidate(Transmode, BranchCode, DayDate, ProcessNo);
        //}


    }
    


    public class BlAttendancedetails
    {

        private Int64 _ID_Attendance;
        private DateTime _AttDate;
        private DateTime _AttTime;
        private string _AttUser;
        private string _UserCode;
        private DateTime _UserDate;
        private Int64 _BranchCode;





        public BlAttendancedetails()
        {
            Initialize();
        }
        public void Initialize()
        {
            _ID_Attendance = 0;
            _AttDate = DateTime.Now;
            _AttTime = DateTime.Now;
            _AttUser = string.Empty;
            _UserCode = string.Empty;
            _UserDate = DateTime.Now;
            _BranchCode = 0;
          

        }


        public Int64 ID_Attendance
        {
            get { return _ID_Attendance; }
            set { _ID_Attendance = value; }
        }
        public DateTime AttDate
        {
            get { return _AttDate; }
            set { _AttDate = value; }
        }
        public DateTime AttTime
        {
            get { return _AttTime; }
            set { _AttTime = value; }
        }
        public DateTime UserDate
        {
            get { return _UserDate; }
            set { _UserDate = value; }
        }
        public string UserCode
        {
            get { return _UserCode; }
            set { _UserCode = value; }
        }
        public string AttUser
        {
            get { return _AttUser; }
            set { _AttUser = value; }
        }
        public Int64 BranchCode
        {
            get { return _BranchCode; }
            set { _BranchCode = value; }
        }

    }

}
