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
    public class BlRackArrangement:IRackArrangement
    {
        private byte _UserAction;
		private Int16 _PageIndex;
		private Int16 _PageSize; 
		private long _ID_RackArrangement;
        private long _FK_Rack;
        private long _FK_Item;
        private decimal _Quantity;
		private string _UserCode;
		private DateTime _EnteredOn;
		private long _FK_Client;
        private string _xmlRackItems;
        private long _GroupID;
  
        public BlRackArrangement()
		{
			Initialize();
		}
		public void Initialize()
		{
            _FK_Rack = 0;
            _FK_Item = 0;
            _Quantity = 0;
			_UserCode = string.Empty;
            _xmlRackItems = string.Empty;
			_EnteredOn = DateTime.Now;
			_FK_Client = 1;
            _PageIndex = 1;
            _PageSize = 10;
			_UserAction=1;
            _GroupID = 0;
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
       

        public long FK_Rack
        {
            get { return _FK_Rack; }
            set { _FK_Rack = value; }
        }
        public long FK_Item
        {
            get { return _FK_Item; }
            set { _FK_Item = value; }
        }
        public decimal Quantity
        {
            get { return _Quantity; }
            set { _Quantity = value; }
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
            get { return _ID_RackArrangement; }
            set { _ID_RackArrangement = value; }
        }
        public string xmlRackItems
        {
            get { return _xmlRackItems; }
            set { _xmlRackItems = value; }
        }
        public long GroupID
        {
            get { return _GroupID; }
            set { _GroupID = value; }
        }

        public long InsertData()
        {
            long status;
            DalRackArrangement dalRackArrangement = new DalRackArrangement();
            status = 0;
            try
            {
                _UserAction = 1;
                status = dalRackArrangement.UpdateRackArrangement(this);
                dalRackArrangement = null;
            }
            catch (Exception ex)
            {

            }
            return status;
        }

        public long UpdateData()
        {
            _UserAction = 2;
            DalRackArrangement dalRackArrangement = new DalRackArrangement();
            long status;
            status = 0;
            try
            {
                status = dalRackArrangement.UpdateRackArrangement(this);
                dalRackArrangement = null;
            }
            catch (Exception ex)
            {

            }
            return status;
        }

        public long DeleteData()
        {
            _UserAction = 3;
            DalRackArrangement dalRackArrangement = new DalRackArrangement();
            long status;
            status = 0;
            try
            {
                status = dalRackArrangement.DeleteRackArrangement(this);
                dalRackArrangement = null;
            }
            catch (Exception ex)
            {
            }
            return status;
        }
        public long DeleteGroupData()
        {
            _UserAction = 3;
            DalRackArrangement dalRackArrangement = new DalRackArrangement();
            long status;
            status = 0;
            try
            {
                status = dalRackArrangement.DeleteRackArrangementGroup(this);
                dalRackArrangement = null;
            }
            catch (Exception ex)
            {
            }
            return status;
        }
        public void SelectData()
        {
            DalRackArrangement dalRackArrangement = new DalRackArrangement();
            IRackArrangement iRackArrangement;
            iRackArrangement = (IRackArrangement)dalRackArrangement.SelectRackArrangement(this);
            _ID_RackArrangement = iRackArrangement.MasterId;
            _FK_Rack = iRackArrangement.FK_Rack;
           
        }
        public DataTable SelectAllData()
        {
            DataTable dtbl = new DataTable();
            DalRackArrangement dalRackArrangement = new DalRackArrangement();
            dtbl = dalRackArrangement.SelectAllRackArrangement(this);
            dalRackArrangement = null;
            return dtbl;
        }
        public DataTable SelectAllGroupData()
        {
            DataTable dtbl = new DataTable();
            DalRackArrangement dalRackArrangement = new DalRackArrangement();
            dtbl = dalRackArrangement.SelectAllRackArrangementGroupData(this);
            dalRackArrangement = null;
            return dtbl;
        }

      
    }
    public class RackItems
    {
        private Int64 _FK_Items;
        private decimal _Quantity;
        
        private Int64 _ID_RackArrangement;

        



        public RackItems()
        {
            Initialize();
        }
        public void Initialize()
        {

            _FK_Items = 0;
            _Quantity = 0;
           
            _ID_RackArrangement = 0;

        }

        public Int64 FK_Items
        {
            get { return _FK_Items; }
            set { _FK_Items = value; }
        }

        public Int64 ID_RackArrangement
        {
            get { return _ID_RackArrangement; }
            set { _ID_RackArrangement = value; }
        }

        public decimal Quantity
        {
            get { return _Quantity; }
            set { _Quantity = value; }
        }
       

    }
}
