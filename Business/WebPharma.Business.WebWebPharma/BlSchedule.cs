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
using WebPharma.Interface.WebPharma;
using WebPharma.DataAccess.WebPharma;
using System.Data;
namespace WebPharma.Business.WebPharma
{

    public class BlSchedule : ISchedule
    {
            private byte _UserAction;
            private Int16 _PageIndex;
            private Int16 _PageSize;
            private long _FK_Client;
            private long _ID_Schedule;
            private Int16 _ScheduleMode;
            private DateTime _ScheduleFrom;
            private DateTime _ScheduleUpto;
            private string _XmlSchedulePosting;
            private string _XmlScheduleProcessing;
            private byte _ScheduleType;
            private decimal _ScAmount;

            private DateTime _SearchFromScheduleUpto;

         
            private DateTime _SearchToScheduleUpto;

          
         
            private DateTime _EnteredOn;

         
            private string _UserCode;

            private string _DatasetName;
            private string _ReportSchema;
            private string _ReportTable;
            private string _ReportSchemaPath;
            private string _ReportPath;
            private string _ReportFile;
          

      

        public BlSchedule()
        {
            Initialize();
        }
        public void Initialize()
        {

        
            _ID_Schedule = 0;
            _ScheduleMode = 1;
            _FK_Client = 1;
            _ScheduleFrom = DateTime.Now;
            _ScheduleUpto = DateTime.Now;

            _XmlSchedulePosting = string.Empty;
            _XmlScheduleProcessing = string.Empty;
            PageIndex = 1;
            PageSize = 10;
            _UserCode = "";
            _EnteredOn = DateTime.Now;
            _ScheduleType = 1;
            _ScAmount = 0;
            _SearchFromScheduleUpto = DateTime.Now;
            _SearchToScheduleUpto = DateTime.Now;
        }

        public DateTime SearchFromScheduleUpto
        {
            get { return _SearchFromScheduleUpto; }
            set { _SearchFromScheduleUpto = value; }
        }
        public DateTime SearchToScheduleUpto
        {
            get { return _SearchToScheduleUpto; }
            set { _SearchToScheduleUpto = value; }
        }
        public string ReportFile
        {
            get { return _ReportFile; }
            set { _ReportFile = value; }
        }

        public string ReportSchema
        {
            get { return _ReportSchema; }
            set { _ReportSchema = value; }
        }

        public string ReportTable
        {
            get { return _ReportTable; }
            set { _ReportTable = value; }
        }

        public string ReportSchemaPath
        {
            get { return _ReportSchemaPath; }
            set { _ReportSchemaPath = value; }
        }

        public string ReportPath
        {
            get { return _ReportPath; }
            set { _ReportPath = value; }
        }
        public string DatasetName
        {
            get { return _DatasetName; }
            set { _DatasetName = value; }
        }
        public DateTime EnteredOn
        {
            get { return _EnteredOn; }
            set { _EnteredOn = value; }
        }
        public string UserCode
        {
            get { return _UserCode; }
            set { _UserCode = value; }
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
        public long FK_Client
        {
            get { return _FK_Client; }
            set { _FK_Client = value; }
        }
        public Int16 PageSize
        {
            get { return _PageSize; }
            set { _PageSize = value; }
        }
        public long MasterId
        {
            get { return _ID_Schedule; }
            set { _ID_Schedule = value; }
        }
        public DateTime ScheduleUpto
        {
            get { return _ScheduleUpto; }
            set { _ScheduleUpto = value; }
        }
        public Int16 ScheduleMode
        {
            get { return _ScheduleMode; }
            set { _ScheduleMode = value; }
        }
        public DateTime ScheduleFrom
        {
            get { return _ScheduleFrom; }
            set { _ScheduleFrom = value; }
        }
        public string XmlSchedulePosting
        {
            get { return _XmlSchedulePosting; }
            set { _XmlSchedulePosting = value; }
        }
        public string XmlScheduleProcessing
        {
            get { return _XmlScheduleProcessing; }
            set { _XmlScheduleProcessing = value; }
        }

        public byte ScheduleType
        {
            get { return _ScheduleType; }
            set { _ScheduleType = value; }
        }
        public decimal ScAmount
        {
            get { return _ScAmount; }
            set { _ScAmount = value; }
        }

      
        public DataTable ProcessingData()
        {
            DataTable dt = new DataTable();
            DalSchedule dalschedule = new DalSchedule();
            try
            {
              
                dt = dalschedule.ProcessingData(this);
                dalschedule = null;
            }
            catch (Exception ex)
            {
            }
            return dt;
        }
        public long DeleteData()
        {
            _UserAction = 3;
            DalSchedule dalschedule = new DalSchedule();
            long status;
            status = 0;
            try
            {
                status = dalschedule.DeleteSchedule(this);
                dalschedule = null;
            }
            catch (Exception ex)
            {

            }
            return status;
        }
        public long SaveProcessData()
        {
            long status;
            DalSchedule dalschedule = new DalSchedule();
            status = 0;
            try
            {
                _UserAction = 1;
                status = dalschedule.SaveProcessData(this);
                dalschedule = null;
            }
            catch (Exception ex)
            {

            }
            return status;
        }
        public long SavePostingData()
        {
            long status;
            DalSchedule dalschedule = new DalSchedule();
            status = 0;
            try
            {
                _UserAction = 1;
                status = dalschedule.SavePostingData(this);
                dalschedule = null;
            }
            catch (Exception ex)
            {

            }
            return status;
        }

        public long SaveScheduleImport()
        {
            long status;
            DalSchedule dalschedule = new DalSchedule();
            status = 0;
            try
            {
                _UserAction = 1;
                status = dalschedule.SaveScheduleImport(this);
                dalschedule = null;
            }
            catch (Exception ex)
            {

            }
            return status;
        }
        //public void SelectData()
        //{
        //    DalItemConversion dalIC = new DalItemConversion();
        //    IItemConversion iItemConversion;
        //    iItemConversion = (IItemConversion)dalIC.SelectItemConversion(this);
        //    _FK_StoreFrom = iItemConversion.FK_StoreFrom;
        //    _FK_StoreTo = iItemConversion.FK_StoreTo;
        //    _TransDate = iItemConversion.TransDate;
        //    _FromAmount = iItemConversion.FromAmount;
        //    _ToAmount = iItemConversion.ToAmount; ;
        //    _Remarks = iItemConversion.Remarks;
        //    _Approved = iItemConversion.Approved;
        //    dalIC = null;

        //}


        public DataSet GetScheduleDataSource(long StatusId)
        {
            DalSchedule Schedule = new DalSchedule();
            DataSet ds = new DataSet();
            try
            {
                ds = Schedule.GetScheduleDataSource(StatusId);
                ds.DataSetName = _DatasetName;
                ds.Tables[0].TableName = ReportTable;
                ds.WriteXmlSchema(_ReportSchemaPath + _ReportSchema + ".xsd");
            }
            catch (Exception ex)
            {
            }
            finally
            {
                Schedule = null;
            }
            return ds;
        }
        public DataSet GetSchedulePostingDataSource(long StatusId)
        {
            DalSchedule Schedule = new DalSchedule();
            DataSet ds = new DataSet();
            try
            {
                ds = Schedule.GetSchedulePostingDataSource(StatusId);
                ds.DataSetName = _DatasetName;
                ds.Tables[0].TableName = ReportTable;
                ds.WriteXmlSchema(_ReportSchemaPath + _ReportSchema + ".xsd");
            }
            catch (Exception ex)
            {
            }
            finally
            {
                Schedule = null;
            }
            return ds;
        }
        
        public DataTable SelectAllData()
        {
            DataTable dtbl = new DataTable();
            DalSchedule Schedule = new DalSchedule();
            dtbl = Schedule.SelectAllSchedule(this);
            Schedule = null;
            return dtbl;
        }

        public DataTable SelectSchedulePosting()
        {
            DataTable dtbl = new DataTable();
            DalSchedule Schedule = new DalSchedule();
            dtbl = Schedule.SelectSchedulePosting(this);
            Schedule = null;
            return dtbl;
        }

        public DataTable checkAlreadyprocessed()
        {
            DataTable dtbl = new DataTable();
            DalSchedule Schedule = new DalSchedule();
            dtbl = Schedule.CheckAlreadyprocessed(this);
            Schedule = null;
            return dtbl;
        }




    }
    public class BlSchedulePosting
    {

        private decimal _Amount;

       
        private string _CusNumber;



        public BlSchedulePosting()
        {
            Initialize();
        }
        public void Initialize()
        {

            _Amount = 0;
            _CusNumber = string.Empty;
           

        }
        public string CusNumber
        {
            get { return _CusNumber; }
            set { _CusNumber = value; }
        }

        public decimal Amount
        {
            get { return _Amount; }
            set { _Amount = value; }
        }
    }

    public class BlScheduleProcessing
    {

        private Int64 _FK_Customer;
        private Int64 _FK_TermLoanApplication;
    
        private decimal _ProcessingNeethiAmount;
        private decimal _ProcessingConsumerAmount;
        private decimal _ProcessingRecoveryAmount;

      
       

     

       


        public BlScheduleProcessing()
        {
            Initialize();
        }
        public void Initialize()
        {

                _FK_Customer = 0;
                _FK_TermLoanApplication = 0;
              
                _ProcessingNeethiAmount = 0;
                _ProcessingConsumerAmount = 0;
                _ProcessingRecoveryAmount = 0;
            
           
        }
        public Int64 FK_Customer
        {
            get { return _FK_Customer; }
            set { _FK_Customer = value; }
        }
        public Int64 FK_TermLoanApplication
        {
            get { return _FK_TermLoanApplication; }
            set { _FK_TermLoanApplication = value; }
        }
     
        public decimal ProcessingNeethiAmount
        {
            get { return _ProcessingNeethiAmount; }
            set { _ProcessingNeethiAmount = value; }
        }
        public decimal ProcessingConsumerAmount
        {
            get { return _ProcessingConsumerAmount; }
            set { _ProcessingConsumerAmount = value; }
        }
     
        public decimal ProcessingRecoveryAmount
        {
            get { return _ProcessingRecoveryAmount; }
            set { _ProcessingRecoveryAmount = value; }
        }
    }
}



