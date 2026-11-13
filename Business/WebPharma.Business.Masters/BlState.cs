/*----------------------------------- Business Layer Class -----------------------------------------------------------------------------------------------------------------------
Purpose    : 
Author     : Praveen MP
Copyright  : Perfect Software Solutions (Clt) Pvt Ltd
Created on : 21/05/2018
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
   public class BlState:IState
    {
        private byte _UserAction;
        private string _UserCode;
        private DateTime _EnteredOn;
        private Int64 _FK_Client;      
        private Int16 _PageIndex;
        private Int16 _PageSize;
        private Int64 _ID_Country;
        private string _StateName;
        private Int64 _FK_Country;
        private long _FK_Reason;
        private string _StateCode;

        public BlState()
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
            _StateName = string.Empty;
                   
            _FK_Country = 0;
            _FK_Reason = 0;
            _StateCode = string.Empty;
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
        public string StateName
        {
            get { return _StateName; }
            set { _StateName = value; }
        }
           
        public Int64 FK_Country
        {
            get { return _FK_Country; }
            set { _FK_Country = value; }
        }
        public long FK_Reason
        {
            get { return _FK_Reason; }
            set { _FK_Reason = value; }
        }
        public string StateCode
        {
            get { return _StateCode; }
            set { _StateCode = value; }
        }

        public long InsertData()
        {
            long status;
            DalState dalstate = new DalState();
            status = 0;
            try
            {
                _UserAction = 1;
                status = dalstate.UpdateState(this);
                dalstate = null;
            }
            catch (Exception ex)
            {
            }
            return status;
        }
        public long UpdateData()
        {
            DalState dalstate = new DalState();
            long status;
            status = 0;
            try
            {
                _UserAction = 2;
                status = dalstate.UpdateState(this);
                dalstate = null;
            }
            catch (Exception ex)
            {
            }
            return status;
        }
        public long DeleteData()
        {
            DalState dalstate = new DalState();
            long status;
            status = 0;
            try
            {
                status = dalstate.DeleteState(this);
                dalstate = null;
            }
            catch (Exception ex)
            {
            }
            return status;
        }
        public void SelectData()
        {
            DalState dalstate = new DalState();
            IState istate;
            istate = (IState)dalstate.SelectState(this);
         
            _StateName = istate.StateName;
            _FK_Country = istate.FK_Country;
            _StateCode = istate.StateCode;

            dalstate = null;
        }
        public DataTable SelectAllData()
        {
            DataTable dtbl = new DataTable();
            DalState dalstate = new DalState();
            dtbl = dalstate.SelectAllState(this);
            dalstate = null;
            return dtbl;
        }
    }
}
