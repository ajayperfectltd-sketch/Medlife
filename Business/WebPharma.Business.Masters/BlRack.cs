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
    public class BlRack:IRack
    {
        private byte _UserAction;
		private Int16 _PageIndex;
		private Int16 _PageSize; 
		private long _ID_Rack;
        private string _ShortName;	
        private string _RackName;
		private string _UserCode;
		private DateTime _EnteredOn;
		private long _FK_Client;
        private long _FK_Supplier;
        private long _FK_Reason;
        public BlRack()
		{
			Initialize();
		}
		public void Initialize()
		{

            _ShortName = string.Empty;
            _RackName = string.Empty;
			_UserCode = string.Empty;
			_EnteredOn = DateTime.Now;
			_FK_Client = 1;
            _PageIndex = 1;
            _PageSize = 10;
			_UserAction=1;
            _FK_Supplier = 0;
            _FK_Reason = 1;
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
       
        public string ShortName
        {
            get { return _ShortName; }
            set { _ShortName = value; }
        }
        public string RackName
        {
            get { return _RackName; }
            set { _RackName = value; }
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
            get { return _ID_Rack; }
            set { _ID_Rack = value; }
        }
        public long FK_Supplier
        {
            get { return _FK_Supplier; }
            set { _FK_Supplier = value; }
        }
        public long  FK_Reason
        {
            get { return _FK_Reason; }
            set { _FK_Reason = value; }
        }

        public long InsertData()
        {
            long status;
            DalRack dalRack = new DalRack();
            status = 0;
            try
            {
                _UserAction = 1;
                status = dalRack.UpdateRack(this);
                dalRack = null;
            }
            catch (Exception ex)
            {

            }
            return status;
        }

        public long UpdateData()
        {
            _UserAction = 2;
            DalRack dalRack = new DalRack();
            long status;
            status = 0;
            try
            {
                status = dalRack.UpdateRack(this);
                dalRack = null;
            }
            catch (Exception ex)
            {

            }
            return status;
        }

        public long DeleteData()
        {
            _UserAction = 3;
            DalRack dalRack = new DalRack();
            long status;
            status = 0;
            try
            {
                status = dalRack.DeleteRack(this);
                dalRack = null;
            }
            catch (Exception ex)
            {
            }
            return status;
        }
        public void SelectData()
        {
            DalRack dalRack = new DalRack();
            IRack iRack;
            iRack = (IRack)dalRack.SelectRack(this);
            _RackName = iRack.RackName;
            _ShortName = iRack.ShortName;
            
        }
        public DataTable SelectAllData()
        {
            DataTable dtbl = new DataTable();
            DalRack dalRack = new DalRack();
            dtbl = dalRack.SelectAllRack(this);
            dalRack = null;
            return dtbl;
        }
        public DataTable SelectRackPopup()
        {
            DataTable dtbl = new DataTable();
            DalRack dalRack = new DalRack();
            dtbl = dalRack.SelectRackPopup(this);
            dalRack = null;
            return dtbl;
        }
    }
}
