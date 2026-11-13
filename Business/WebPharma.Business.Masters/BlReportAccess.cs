using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebPharma.Interface.Masters;
using WebPharma.DataAccess.Masters;
using System.Data;


namespace WebPharma.Business.Masters
{
    public class BlReportAccess : IReportAcess
    {
        public BlReportAccess()
        {
            Initialize();
        }
        private long _ID_ReportAcess;
        private string _XmlReportAccess;
        private long _FK_User;
        private Int64 _FK_Client;
        private Int16 _PageSize;
        private Int16 _PageIndex;
        private string _UserCode;
        private byte _UserAction;
        private DateTime _EnteredOn;
        private Int64 _FK_ReportCategory;
        private Int32 _Type;
        private long _FK_UserGroup;

        public Int32 Type
        {
            get { return _Type; }
            set { _Type = value; }
        }


        public Int64 FK_ReportCategory
        {
            get { return _FK_ReportCategory; }
            set { _FK_ReportCategory = value; }
        }
        public Int64 FK_UserGroup
        {
            get { return _FK_UserGroup; }
            set { _FK_UserGroup = value; }
        }
        public string XmlReportAccess
        {
            get { return _XmlReportAccess; }
            set { _XmlReportAccess = value; }
        }
        public long MasterId
        {
            get { return _ID_ReportAcess; }
            set { _ID_ReportAcess = value; }
        }
        
   

        public long FK_User
        {
            get { return _FK_User; }
            set { _FK_User = value; }
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
    
        public byte UserAction
        {
            get { return _UserAction; }
            set { _UserAction = value; }
        }
        public void Initialize()
        {
            
            _FK_User = 0;
            _FK_Client = 1;
            _PageIndex = 1;
            _PageSize = 10;
            _Type = 1;
            _FK_UserGroup = 0;
        }

        public long InsertData()
        {
            DalReportAcess dalReportAcess = new DalReportAcess();
            long status = 0;
            try
            {

                status = dalReportAcess.UpdateReportAcess(this);
                dalReportAcess = null;

            }
            catch (Exception ex)
            {
            }
            return status;
        }
        public DataTable SelectReportCategory()
        {
            DataTable dtbl = new DataTable();
            DalReportAcess dalReportCategory = new DalReportAcess();
            dtbl = dalReportCategory.SelectReportCategories(this);
            dalReportCategory = null;
            return dtbl;
        }
        public DataTable SelectReportAccess()
        {
            DataTable dtbl = new DataTable();
            DalReportAcess dalReportaccess = new DalReportAcess();
            dtbl = dalReportaccess.SelectReportAccess(this);
            dalReportaccess = null;
            return dtbl;
        }
        public DataTable SelectReportMenu()
        {
            DataTable dtbl = new DataTable();
            DalReportAcess dalReportaccess = new DalReportAcess();
            dtbl = dalReportaccess.SelectReportMenu(this);
            dalReportaccess = null;
            return dtbl;
        }
       

    }
    public class ReportSubCategory
    {
        public Int64 _FK_ReportSubCategory;
        public Int32 _ReportSubType;

        public ReportSubCategory()
        {
            Initialize();
        }
        public void Initialize()
        {
            _FK_ReportSubCategory = 0;
            _ReportSubType = 1;
        }
        public Int64 FK_ReportSubCategory
        {
            get { return _FK_ReportSubCategory; }
            set { _FK_ReportSubCategory = value; }
        }

        public Int32 ReportSubType
        {
            get { return _ReportSubType; }
            set { _ReportSubType = value; }
        }

    }
}
