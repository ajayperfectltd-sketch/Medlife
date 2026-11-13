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
    public class BlYearEnd : IYearEnd
    {
        public BlYearEnd()
        {
            Initialize();
        }

        public void Initialize()
        {


            _FromDate = DateTime.Now;
            _ToDate = DateTime.Now;
            _Type = 0;
            _FK_User = 1;
            _Active = true;
            _UserCode = "";
            _FK_Outlet = 1;
            _UserAction = 1;
            _UserCode = string.Empty;
            EnteredOn = DateTime.Now;
            FK_Client = 1;
            PageIndex = 1;
            PageSize = 10;
            _FK_UserGroup = 0;
        }
        private long _FK_UserGroup;

        public long FK_UserGroup
        {
            get { return _FK_UserGroup; }
            set { _FK_UserGroup = value; }
        }


        private string _XmlUser;

        public string XmlUser
        {
            get { return _XmlUser; }
            set { _XmlUser = value; }
        }
        private Int64 _FK_Outlet;
        public Int64 FK_Outlet
        {
            get { return _FK_Outlet; }
            set { _FK_Outlet = value; }
        }

        private byte _UserAction;
        public byte UserAction
        {
            get { return _UserAction; }
            set { _UserAction = value; }
        }
        private Int64 _FK_Client;

        public Int64 FK_Client
        {
            get { return _FK_Client; }
            set { _FK_Client = value; }
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

        private DateTime _EnteredOn;

        public DateTime EnteredOn
        {
            get { return _EnteredOn; }
            set { _EnteredOn = value; }
        }

        private string _Topic;
        private Int64 _ID_YearEnd;
        public Int64 MasterId
        {
            get { return _ID_YearEnd; }
            set { _ID_YearEnd = value; }
        }

        public string Topic
        {
            get { return _Topic; }
            set { _Topic = value; }
        }
        private string _Message;

        public string Message
        {
            get { return _Message; }
            set { _Message = value; }
        }
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
        private Byte _Type;

        public Byte Type
        {
            get { return _Type; }
            set { _Type = value; }
        }
        private long _FK_User;

        public long FK_User
        {
            get { return _FK_User; }
            set { _FK_User = value; }
        }
        private Boolean _Active;

        public Boolean Active
        {
            get { return _Active; }
            set { _Active = value; }
        }
        private string _UserCode;

        public string UserCode
        {
            get { return _UserCode; }
            set { _UserCode = value; }
        }
        private bool _StockUpdateMode;

        public bool StockUpdateMode
        {
            get { return _StockUpdateMode; }
            set { _StockUpdateMode = value; }
        }
        public DataTable InsertYearEnd(int a)
        {
          
            
           // long Status = 0;
            DalYearEnd dlYearEnd = new DalYearEnd();
           
            try
            {
                _UserAction = 1;
                DataTable dt = new DataTable();
                dt = dlYearEnd.UpdateYearEnd(this, a);
                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
           
          
        }



        public DataTable CancelYearEnd(int a)
        {


            // long Status = 0;
            DalYearEnd dlYearEnd = new DalYearEnd();

            try
            {
                _UserAction = 1;
                DataTable dt = new DataTable();
                dt = dlYearEnd.UpdateYearEnd(this, a);
                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }


        }
        //public long UpdateYearEnd()
        //{
        //    long Status = 0;
        //    DalYearEnd dlYearEnd = new DalYearEnd();
        //    try
        //    {
        //        _UserAction = 2;
        //        Status = dlYearEnd.UpdateYearEnd(this);
        //    }
        //    catch (Exception ex)
        //    {
        //    }
        //    return Status;

        //}
        //    public long DeleteData()
        //    {
        //        long Status = 0;
        //        DalYearEnd dlYearEnd = new DalYearEnd();
        //        try
        //        {
        //            Status = dlYearEnd.DeleteYearEnd(this);
        //            dlYearEnd = null;
        //        }
        //        catch (Exception ex)
        //        {
        //        }
        //        return Status;
        //    }
        //    public void SelectYearEnd()
        //    {
        //        DalYearEnd dlYearEnd = new DalYearEnd();
        //        IYearEnd IRem;
        //        IRem = (IYearEnd)dlYearEnd.SelectYearEnd(this);
        //        _Active = IRem.Active;
        //        _Topic = IRem.Topic;
        //        _Message = IRem.Message;
        //        _FromDate = IRem.FromDate;
        //        _ToDate = IRem.ToDate;
        //        _Type = IRem.Type;
        //        IRem = null;
        //    }
        //    public DataTable SelectUserForYearEnd()
        //    {
        //        DalYearEnd DlYearEnd = new DalYearEnd();
        //        DataTable dt = DlYearEnd.SelectUserForYearEnd(this);
        //        DlYearEnd = null;
        //        return dt;
        //    }
        //    public DataTable SelectAllYearEnd()
        //    {
        //        DalYearEnd dlYearEnd = new DalYearEnd();
        //        DataTable dtbl = new DataTable();
        //        dtbl = dlYearEnd.SelectAllYearEnd(this);
        //        dlYearEnd = null;
        //        return dtbl;
        //    }
        //    public DataTable GetYearEnd()
        //    {
        //        DataTable dtbl = new DataTable();
        //        DalYearEnd dlYearEnd = new DalYearEnd();
        //        dtbl = dlYearEnd.GetYearEnd(this);
        //        dlYearEnd = null;
        //        return dtbl;

        //    }
        //    public DataTable GetYearEndNew()
        //    {
        //        DataTable dtbl = new DataTable();
        //        DalYearEnd dlYearEnd = new DalYearEnd();
        //        dtbl = dlYearEnd.GetYearEndNew(this);
        //        dlYearEnd = null;
        //        return dtbl;

        //    }
        //    public DataTable GetYearEndDetails()
        //    {

        //        DataTable dtbl = new DataTable();
        //        DalYearEnd dlYearEnd = new DalYearEnd();
        //        dtbl = dlYearEnd.GetYearEndDetails(this);
        //        dlYearEnd = null;
        //        return dtbl;
        //    }
        //    public DataTable GetYearEndDetailsView()
        //    {

        //        DataTable dtbl = new DataTable();
        //        DalYearEnd dlYearEnd = new DalYearEnd();
        //        dtbl = dlYearEnd.GetYearEndDetailsView(this);
        //        dlYearEnd = null;
        //        return dtbl;
        //    }
        //}
        //public class IYearEndDetails
        //{
        //    private long _FK_User;

        //    public long FK_User
        //    {
        //        get { return _FK_User; }
        //        set { _FK_User = value; }
        //    }
        //}

    }
}
