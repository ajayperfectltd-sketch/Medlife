/*----------------------------------- Business Layer Class -----------------------------------------------------------------------------------------------------------------------
Purpose    : 
Author     : Aiswarya
Copyright  : Perfect Software Solutions (Clt) Pvt Ltd
Created on : 28/09/2018
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
    public class BlIssueOut:IIssueOut
    {
        #region Properties
        public BlIssueOut()
        {
            Initialize();
        }
        public void Initialize()
        {
            _FK_Store = 0;
            _FK_Category = 0;
            _IssueType ="";
            _TransDate = DateTime.Now;
            _TotalAmt = 0;
            _UserCode = "";
            _EnteredOn = DateTime.Now;
            _Cancelled = false;
            _CancelledBy = "";
            _CancelledOn = DateTime.Now;
            _FK_Client = 1;
            _MasterId = 0;
            _UserAction = 1;
            XmlIssuedItems = "";
            _PageIndex = 1;
            _PageSize = 10;
            _Fk_Reasons = 0;
            
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

        private string _XmlIssuedItems;

        public string XmlIssuedItems
        {
        get { return _XmlIssuedItems; }
        set { _XmlIssuedItems = value; }
        }

        private byte _UserAction;
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
        public byte UserAction
        {
        get { return _UserAction; }
        set { _UserAction = value; }
        }

        private long _Fk_Reasons;
        public long Fk_Reasons
        {
            get { return _Fk_Reasons; }
            set { _Fk_Reasons = value; }
        }

        private long _FK_Store;
        public long FK_Store
        {
            get { return _FK_Store; }
            set { _FK_Store = value; }
        }
        private long _FK_Category;

        public long FK_Category
        {
        get { return _FK_Category; }
        set { _FK_Category = value; }
        }
        private string _IssueType;
        public string IssueType
        {
        get { return _IssueType; }
        set { _IssueType = value; }
        }
        private  DateTime _TransDate;

        public DateTime TransDate
        {
        get { return _TransDate; }
        set { _TransDate = value; }
        }

        private decimal _TotalAmt;

        public decimal TotalAmt
        {
        get { return _TotalAmt; }
        set { _TotalAmt = value; }
        }

        private string _UserCode;

        public string UserCode
        {
        get { return _UserCode; }
        set { _UserCode = value; }
        }

        private DateTime _EnteredOn;

        public DateTime EnteredOn
        {
        get { return _EnteredOn; }
        set { _EnteredOn = value; }
        }
        private bool _Cancelled;

        public bool Cancelled
        {
        get { return _Cancelled; }
        set { _Cancelled = value; }
        }

        private string _CancelledBy;

        public string CancelledBy
        {
        get { return _CancelledBy; }
        set { _CancelledBy = value; }
        }

        private DateTime _CancelledOn;

        public DateTime CancelledOn
        {
        get { return _CancelledOn; }
        set { _CancelledOn = value; }
        }

        private long _FK_Client;

        public long FK_Client
        {
        get { return _FK_Client; }
        set { _FK_Client = value; }
        }
        private long _MasterId;

        public long MasterId
        {
        get { return _MasterId; }
        set { _MasterId = value; }
        }
                #endregion
        #region Process
            public long insertData()
            {
                long status = 0;
                DalIssueOut Issueout = new DalIssueOut();
                try
                {
                    UserAction = 1;
                    status = Issueout.UpdateIssueOut(this);
                    Issueout = null;
                }
                catch (Exception ex)
                {
                }
                return status;
            }
            public long UpdateData()
            {

                long status = 0;
                DalIssueOut Issueout = new DalIssueOut();
                try
                {
                    UserAction = 2;
                    status = Issueout.UpdateIssueOut(this);
                    Issueout = null;
                }
                catch (Exception ex)
                {
                }
                return status;
            }
            public long DeleteData()
            {
                long status = 0;
                DalIssueOut dalissueout = new DalIssueOut();
                try
                {
                    status = dalissueout.DeleteIssueOut(this);
                    dalissueout = null;
                }
                catch (Exception ex)
                {
                }
                return status;
            }
            public void SelectData()
            {
                DalIssueOut issueouts = new DalIssueOut();
                IIssueOut iissueout;
                iissueout = (IIssueOut)issueouts.SelectIssueOut(this);
                _FK_Store = iissueout.FK_Store;
                _FK_Category = iissueout.FK_Category;
                _IssueType = iissueout.IssueType;
                _TransDate = iissueout.TransDate;
                _TotalAmt = iissueout.TotalAmt;
                _Fk_Reasons = iissueout.Fk_Reasons;
                issueouts = null;
            }
            public DataTable SelectAllData()
            {
                DataTable dtbl = new DataTable();
                DalIssueOut issueouts = new DalIssueOut();
                dtbl = issueouts.SelectIssueOutAll(this);
                issueouts = null;
                return dtbl;
            }
            public DataTable SelectIssueOutItems()
            {
                DataTable dtbl = new DataTable();
                DalIssueOut issueout = new DalIssueOut();
                dtbl = issueout.SelectIssueOutItems(this);
                issueout = null;
                return dtbl;
            }

             
        #endregion
    }
    public class BlIssueoutItems
    {
                    public BlIssueoutItems()
                    {
                    Initialize();
                    }
                   public void Initialize()
                   {
                    _FK_Stock = 0;
                    _FK_Items = 0;
                    _CurrentStock = 0;
                    _FK_Reason = 0;
                    _FK_IssueType = string.Empty;
                    
                    
                   }
                   private string  _FK_IssueType;

                   public string FK_IssueType
                   {
                       get { return _FK_IssueType; }
                       set { _FK_IssueType = value; }
                   }
                private long _FK_Stock;

                public long FK_Stock
                {
                get { return _FK_Stock; }
                set { _FK_Stock = value; }
                }

                private long _FK_Items;

                public long FK_Items
                {
                get { return _FK_Items; }
                set { _FK_Items = value; }
                }
                private decimal _CurrentStock;

                public decimal CurrentStock
                {
                    get { return _CurrentStock; }
                    set { _CurrentStock = value; }
                }

                private long _FK_Reason;

                public long FK_Reason
                {
                    get { return _FK_Reason; }
                    set { _FK_Reason = value; }
                }
        //private string _IssueType;

        //public string IssueType
        //{
        //get { return _IssueType; }
        //set { _IssueType = value; }
        //}
//private long _FK_Store;

//public long FK_Store
//{
//get { return _FK_Store; }
//set { _FK_Store = value; }
//}

//private long _FK_RackArrangement;

//public long FK_RackArrangement
//{
//get { return _FK_RackArrangement; }
//set { _FK_RackArrangement = value; }
//}
//private string  _Batch;

//public string Batch
//{
//get { return _Batch; }
//set { _Batch = value; }
//}
//private DateTime _Expdate;

//public DateTime Expdate
//{
//get { return _Expdate; }
//set { _Expdate = value; }
//}

//private decimal _PurRate;

//public decimal PurRate
//{
//get { return _PurRate; }
//set { _PurRate = value; }
//}

//private decimal _MRP;

//public decimal MRP
//{
//get { return _MRP; }
//set { _MRP = value; }
//}

//private decimal _RetailPrice;

//public decimal RetailPrice
//{
//get { return _RetailPrice; }
//set { _RetailPrice = value; }
//}

//private decimal _WholesalePrice;

//public decimal WholesalePrice
//{
//get { return _WholesalePrice; }
//set { _WholesalePrice = value; }
//}
//private decimal _PrivilagePrice;

//public decimal PrivilagePrice
//{
//get { return _PrivilagePrice; }
//set { _PrivilagePrice = value; }
//}
  

    }
}
