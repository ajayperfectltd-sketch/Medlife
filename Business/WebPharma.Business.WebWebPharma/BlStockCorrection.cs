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
namespace WebPharma.Business.WebPharma
{
    public class BlStockCorrection : IStockCorrection
    {
        private long _FK_Client;
        private byte _UserAction;
        private Int16 _PageIndex;
        private Int16 _PageSize;
        private long _FK_Stock;
        private decimal _ItmOldStock;
        private decimal _ItmNewStock;
        private string _UserCode;
        private DateTime _EnteredOn;
        private string _xmlStockCorrectionItems;
        private long _ID_StockCorrection;
        private long _FK_Store;

  
        public BlStockCorrection()
		{
			Initialize();
		}

        public void Initialize()
        {
            _UserAction = 1;
            _FK_Client = 1;
            _PageIndex = 1;
            _PageSize = 10;
            _FK_Stock = 0;
            _ItmOldStock = 0;
            _ItmNewStock = 0;
            _UserCode = string.Empty;
            _EnteredOn = DateTime.Now;
            _xmlStockCorrectionItems = string.Empty;
            _ID_StockCorrection = 0;
            _FK_Store = 0;
        }
        public long FK_Store
        {
            get { return _FK_Store; }
            set { _FK_Store = value; }
        }
        public long MasterId
        {
            get { return _ID_StockCorrection; }
            set { _ID_StockCorrection = value; }
        }
        public long FK_Client
        {
            get { return _FK_Client; }
            set { _FK_Client = value; }
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
        public long FK_Stock
        {
            get { return _FK_Stock; }
            set { _FK_Stock = value; }
        }
        public decimal ItmOldStock
        {
            get { return _ItmOldStock; }
            set { _ItmOldStock = value; }
        }
        public decimal ItmNewStock
        {
            get { return _ItmNewStock; }
            set { _ItmNewStock = value; }
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

        public string xmlStockCorrectionItems
        {
            get { return _xmlStockCorrectionItems; }
            set { _xmlStockCorrectionItems = value; }
        }

        public long InsertData()
        {
            long status;
            DalStockCorrection dalstock = new DalStockCorrection();
            status = 0;
            try
            {
                _UserAction = 1;
                status = dalstock.UpdateStockCorrection(this);
                dalstock = null;
            }
            catch (Exception ex)
            {

            }
            return status;
        }
    }
    public class StockCorrectionItems
    {
        private Int64 _FK_Stock;
        private decimal _ItmOldStock;
        private decimal _ItmNewStock;

        public Int64 FK_Stock
        {
            get { return _FK_Stock; }
            set { _FK_Stock = value; }
        }
        public decimal ItmOldStock
        {
            get { return _ItmOldStock; }
            set { _ItmOldStock = value; }
        }
        public decimal ItmNewStock
        {
            get { return _ItmNewStock; }
            set { _ItmNewStock = value; }
        }
        public StockCorrectionItems()
        {
            Initialize();
        }
        public void Initialize()
        {
            _FK_Stock = 0;
            _ItmOldStock = 0;
            _ItmNewStock = 0;

        }

    }
}
