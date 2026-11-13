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
    public class BlDashBoard : IDashBoard
    {
        private byte _UserAction;
        private Int16 _PageIndex;
        private Int16 _PageSize;
        private DateTime _EnteredOn;
        private long _FK_Users;
        private long _FK_UserGroup;

        public long FK_Users
        {
            get { return _FK_Users; }
            set { _FK_Users = value; }
        }
        public long FK_UserGroup
        {
            get { return _FK_UserGroup; }
            set { _FK_UserGroup = value; }
        }
        private long _MasterId;
       
        public long MasterId
        {
            get { return _MasterId; }
            set { _MasterId = value; }
        }

       
        private string _XmlUserAccess;

        public string XmlUserAccess
        {
            get { return _XmlUserAccess; }
            set { _XmlUserAccess = value; }
        }
        private long _FK_Client;
        private string _UserCode;
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
        public BlDashBoard()
        {
            Initialize();
        }
        public void Initialize()
        {
             _EnteredOn = DateTime.Now;
             _FK_Client = 1;
            _PageIndex = 1;
            _PageSize = 10;
            _UserCode = string.Empty;
            XmlUserAccess = string.Empty;
            _FK_Users = 0;
        }
        public long UpdateData()
        {
            DalDashBoard DalDashboard = new DalDashBoard();
            long status;
            status = 0;
            try
            {
                status = DalDashboard.UpdateDashBoardAccess(this); 
                DalDashboard = null;

            }
            catch (Exception ex)
            {
            }
            DalDashboard = null;
            return status;
        }
        public DataTable SelectDashBoardAccesss()
        {
            DalDashBoard DalDashboards = new DalDashBoard();
            DataTable dtbl = new DataTable();
            
            try
            {
                dtbl = DalDashboards.SelectDashBoardAccess(this);
            }
            catch (Exception ex)
            {
            }
            DalDashboards = null;
            return dtbl;
            
        }
        public DataTable DashBoardAccessRight()
        {
            DalDashBoard DalDashboards = new DalDashBoard();
            DataTable dtbl = new DataTable();
            
            try
            {
                dtbl = DalDashboards.DashBoardAccessRight(this);
            }
            catch (Exception ex)
            {
            }
            DalDashboards = null;
            return dtbl;
            
        }

    }
    public class BlDashBoardAccess
    {
        private Int64 _FK_Dashboard;
        public Int64 FK_Dashboard
        {
            get { return _FK_Dashboard; }
            set { _FK_Dashboard = value; }
        }
        public BlDashBoardAccess()
        {
            Initialize();
         }
        public void Initialize()
        {
            _FK_Dashboard = 0;
        }
    

    }
}
