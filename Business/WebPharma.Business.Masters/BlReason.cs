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
    public class BlReason:IReason
    {
        private byte _UserAction;
		private Int16 _PageIndex;
		private Int16 _PageSize; 
		private long _ID_Reason;		
		private string _Reason;
        private string _ReasonMode;
		private string _UserCode;
		private DateTime _EnteredOn;
		private long _FK_Client;
       


        public BlReason()
		{
			Initialize();
		}
		public void Initialize()
		{

            _Reason = string.Empty;
            _ReasonMode = string.Empty;
			_UserCode = string.Empty;
			_EnteredOn = DateTime.Now;
			_FK_Client = 1;
            _PageIndex = 1;
            _PageSize = 10;
			_UserAction=1;
            
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
       
        public string Reason
        {
            get { return _Reason; }
            set { _Reason = value; }
        }
        public string ReasonMode
        {
            get { return _ReasonMode; }
            set { _ReasonMode = value; }
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
        public Int64 MasterId
        {
            get { return _ID_Reason; }
            set { _ID_Reason = value; }
        }

        public long InsertData()
        {
            long status;
            DalReason dalReason = new DalReason();
            status = 0;
            try
            {
                _UserAction = 1;
                status = dalReason.UpdateReason(this);
                dalReason = null;
            }
            catch (Exception ex)
            {

            }
            return status;
        }

        public long UpdateData()
        {
            _UserAction = 2;
            DalReason dalReason = new DalReason();
            long status;
            status = 0;
            try
            {
                status = dalReason.UpdateReason(this);
                dalReason = null;
            }
            catch (Exception ex)
            {

            }
            return status;
        }

        public long DeleteData()
        {
            _UserAction = 3;
            DalReason dalReason = new DalReason();
            long status;
            status = 0;
            try
            {
                status = dalReason.DeleteReason(this);
                dalReason = null;
            }
            catch (Exception ex)
            {

            }
            return status;
        }

        public void SelectData()
        {
            DalReason dalReason = new DalReason();
            IReason iReason;
            iReason = (IReason)dalReason.SelectReason(this);
            _Reason = iReason.Reason;
            _ReasonMode = iReason.ReasonMode;

        }

        public DataTable SelectAllData()
        {
            DataTable dtbl = new DataTable();
            DalReason dalReason = new DalReason();
            dtbl = dalReason.SelectAllReason(this);
            dalReason = null;
            return dtbl;
        }
    }
}
