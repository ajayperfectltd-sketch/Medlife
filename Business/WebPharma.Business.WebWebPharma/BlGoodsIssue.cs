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
using System.Collections;
using System.Data;
using System.Threading.Tasks;
using WebPharma.DataAccess.WebPharma;
using WebPharma.Interface.WebPharma;

namespace WebPharma.Business.WebPharma
{
    public class BlGoodsIssue : IGoodsIssue
    {
        public BlGoodsIssue()
        {
            Initialise();
        }
        #region Properties
        private DateTime _CancelledOn;
        private string _Cancelleduser;
        private bool _Cancelled;
        private DateTime _EnteredOn;
        private string _UserCode;
        private long _FK_Supplier;
        private string _Description;
        private byte _IssueMode;
        private long _FK_ToStore;
        private long _FK_FromStore;
        private DateTime _IssueDate;
        private string _IssueNo;
        private long _ID_GoodsIssue;
        private DateTime _FromDate;
        private DateTime _ToDate;
      private  string _XmlGoods;
        public void Initialise()
        {
            _CancelledOn = DateTime.Now;
            _Cancelleduser = "";
            _Cancelled = false;
            _EnteredOn = DateTime.Now;
            _UserCode = "";
            _FK_Supplier = 0;
            _Description = "";
            _IssueMode = 0;
            _FK_ToStore = 0;
            _FK_FromStore = 0;
            _IssueDate = DateTime.Now;
            _IssueNo = "";
            _ID_GoodsIssue = 0;
            _FromDate = DateTime.Now;
            _ToDate = DateTime.Now;
            _XmlGoods = "";
            _UserAction = 1;
           
            _FK_Client = 1;
            _PageIndex = 1;
            _PageSize = 10;


        }


        
        public string XmlGoods
        {
            get { return _XmlGoods; }
            set { _XmlGoods = value; }
        }
        public DateTime ToDate
        {
            get { return _ToDate; }
            set { _ToDate = value; }
        }
        public DateTime FromDate
        {
            get { return _FromDate; }
            set { _FromDate = value; }
        }

        private byte _UserAction;

        public byte UserAction
        {
            get { return _UserAction; }
            set { _UserAction = value; }
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
        private long _FK_Client;

        public long FK_Client
        {
            get { return _FK_Client; }
            set { _FK_Client = value; }
        }


        public long MasterId
        {
            get { return _ID_GoodsIssue; }
            set { _ID_GoodsIssue = value; }
        }


        public string IssueNo
        {
            get { return _IssueNo; }
            set { _IssueNo = value; }
        }


        public DateTime IssueDate
        {
            get { return _IssueDate; }
            set { _IssueDate = value; }
        }


        public long FK_FromStore
        {
            get { return _FK_FromStore; }
            set { _FK_FromStore = value; }
        }


        public long FK_ToStore
        {
            get { return _FK_ToStore; }
            set { _FK_ToStore = value; }
        }


        public byte IssueMode
        {
            get { return _IssueMode; }
            set { _IssueMode = value; }
        }


        public string Description
        {
            get { return _Description; }
            set { _Description = value; }
        }


        public long FK_Supplier
        {
            get { return _FK_Supplier; }
            set { _FK_Supplier = value; }
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


        public bool Cancelled
        {
            get { return _Cancelled; }
            set { _Cancelled = value; }
        }


        public string Cancelleduser
        {
            get { return _Cancelleduser; }
            set { _Cancelleduser = value; }
        }


        public DateTime CancelledOn
        {
            get { return _CancelledOn; }
            set { _CancelledOn = value; }
        }
        #endregion
        #region Process
        public long InsertData()
        {
            long status;
            DalGoodsIssue dalGoodsIssue = new DalGoodsIssue();
            status = 0;
            try
            {
                _UserAction = 1;
                status = dalGoodsIssue.UpdateGoodsIssue(this);
                dalGoodsIssue = null;
            }
            catch (Exception ex)
            {
            }
            return status;
        }
        public long UpdateData()
        {
            long status;
            DalGoodsIssue dalGoodsIssue = new DalGoodsIssue();
            status = 0;
            try
            {
                _UserAction = 2;
                status = dalGoodsIssue.UpdateGoodsIssue(this);
                dalGoodsIssue = null;
            }
            catch (Exception ex)
            {
            }
            return status;
        }

        public void SelectData()
        {
            DalGoodsIssue dalgoods = new DalGoodsIssue();
            IGoodsIssue Igoods;
            Igoods = (IGoodsIssue)dalgoods.SelectGoodsIssue(this);
            _IssueDate = Igoods.IssueDate;
            _FK_FromStore = Igoods.FK_FromStore;
            _FK_ToStore = Igoods.FK_ToStore;
            _IssueMode = Igoods.IssueMode;
            _Description = Igoods.Description;
            _FK_Supplier = Igoods.FK_Supplier;


        }

        public DataTable SelectAllData()
        {
            DataTable dtbl = new DataTable();
            DalGoodsIssue dalgoods = new DalGoodsIssue();
            dtbl = dalgoods.SelectAllGoodsIssue(this);
            return dtbl;
        }


         public DataTable GoodsIssueDetails()
        {
            DataTable dtbl = new DataTable();
            DalGoodsIssue dalgoods = new DalGoodsIssue();
            dtbl = dalgoods.SelectGoodsIssueDetails(this);
            return dtbl;
        }
        public long DeleteData()
        {

            DalGoodsIssue dalGoodsIssue = new DalGoodsIssue();
            long status = 0;
            try
            {

                status = dalGoodsIssue.DeleteGoodsIssue(this);
                dalGoodsIssue = null;
            }
            catch (Exception ex)
            {
            }
            return status;
        }
        #endregion
    }

    public class GoodsIssueDetails
    {
       

        private long _FK_GoodsIssue;

        public long FK_GoodsIssue
        {
            get { return _FK_GoodsIssue; }
            set { _FK_GoodsIssue = value; }
        }

        private long _FK_Items;

        public long FK_Items
        {
            get { return _FK_Items; }
            set { _FK_Items = value; }
        }
        private long _StockCode;

        public long StockCode
        {
            get { return _StockCode; }
            set { _StockCode = value; }
        }
        private decimal _Quantity;

        public decimal Quantity
        {
            get { return _Quantity; }
            set { _Quantity = value; }
        }
        private decimal _PurchaseRate;

        public decimal PurchaseRate
        {
            get { return _PurchaseRate; }
            set { _PurchaseRate = value; }
        }
        private decimal _SalesRate;

        public decimal SalesRate
        {
            get { return _SalesRate; }
            set { _SalesRate = value; }
        }
        private string _BatchNo;

        public string BatchNo
        {
            get { return _BatchNo; }
            set { _BatchNo = value; }
        }
        private long _FK_Units;
        public long FK_Units
        {
            get { return _FK_Units; }
            set { _FK_Units = value; }
        }
    


    }
}
