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
using System.Collections;
using System.Data;
using WebPharma.Interface.General;
using WebPharma.Interface.WebPharma;
using WebPharma.DataAccess.WebPharma;

namespace WebPharma.Business.WebPharma
{
    public class BlMultipleStockCorrection : IMultipleStockCorrection
    {
        private byte _UserAction;
        private Int16 _PageIndex;
        private Int16 _PageSize;
        private long _FK_Items;
        private long _ID_MultipleStockEntry;
        private string _BarCode;
        private long _FK_Client;
        private DateTime _EnteredOn;
        private string _UserCode;
        private string _xmlMultipleStocks;

        public BlMultipleStockCorrection()
		{
			Initialize();
		}
		public void Initialize()
		{
            _UserAction=1;
            _FK_Client = 1;
            _PageIndex = 1;
            _PageSize = 10;
            _ID_MultipleStockEntry = 0;
			_FK_Items = 0;
            _BarCode = string.Empty;
            _EnteredOn = DateTime.Now;
		    _UserCode = string.Empty;
            _xmlMultipleStocks = string.Empty;
		}
        public byte UserAction
        {
            get { return _UserAction; }
            set { _UserAction = value; }
        }
        public long FK_Client
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
        public long MasterId
        {
            get { return _ID_MultipleStockEntry; }
            set { _ID_MultipleStockEntry = value; }
        }
        public long FK_Items
        {
            get { return _FK_Items; }
            set { _FK_Items = value; }
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
        public string XmlMultipleStocks
        {
            get { return _xmlMultipleStocks; }
            set { _xmlMultipleStocks = value; }
        }
        public DataTable SelectStockDetails()
        {
            DataTable dtbl = new DataTable();
            DalMultipleStockCorrection dalmultiplestockcorrection = new DalMultipleStockCorrection();
            dtbl = dalmultiplestockcorrection.SelectStockDetails(this);
            dalmultiplestockcorrection = null;
            return dtbl;
        }
        public long InsertData()
        {
            long status;
            DalMultipleStockCorrection dalmultiplestockcorrection = new DalMultipleStockCorrection();
            status = 0;
            try
            {
                _UserAction = 1;
                status = dalmultiplestockcorrection.UpdateMultipleStockEntry(this);
                dalmultiplestockcorrection = null;
            }
            catch (Exception ex)
            {

            }
            return status;
        }
    }
    public class IStockDetails
    {
        private Int64 _FK_Stock;
        private Int64 _FK_Store;
        private decimal _OpeningStock;
        private decimal _ActualStock;
        private decimal _CurrentStock;
        public Int64 FK_Stock
        {
            get { return _FK_Stock; }
            set { _FK_Stock = value; }
        }
        public Int64 FK_Store
        {
            get { return _FK_Store; }
            set { _FK_Store = value; }
        }
        public decimal OpeningStock
        {
            get { return _OpeningStock; }
            set { _OpeningStock = value; }
        }
        public decimal ActualStock
        {
            get { return _ActualStock; }
            set { _ActualStock = value; }
        }
        public decimal CurrentStock
        {
            get { return _CurrentStock; }
            set { _CurrentStock = value; }
        }
    }
}
