using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebPharma.DataAccess.WebPharma;
using WebPharma.Interface.WebPharma;

namespace WebPharma.Business.WebPharma
{
    public class BlIndent : IIndent
    {
        private byte _UserAction;
        private Int16 _PageIndex;
        private Int16 _PageSize;
        private long _FK_Client;
        private long _MasterId = 0;
        private string _UserCode;
        private DateTime _IndentDate;
        private DateTime _EnteredOn;
        private Boolean _Cancelled;
        private string _CancelledBy;
        private DateTime _CancelledOn;
        private long _ReqID = 0;
        private long _FK_Store = 0;
        private long _ToStore = 0;
        private string _xmlGriddata;
        private string _Remarks;
        private DateTime _FromDate;
        private DateTime _Todate;
        private long _FK_Reason;
        private Decimal _FK_Stock;
        private long _ToStoreE = 0;
        private long _FK_Items = 0;
      //  private Int16 _
        public BlIndent()
		{
			Initialize();
		}
        public void Initialize()
        {
            _UserCode = string.Empty;
            _EnteredOn = DateTime.Now;
            _Cancelled = false;
            _CancelledBy = string.Empty;
            _CancelledOn = DateTime.Now;
            _FK_Client = 0;
            _ReqID = 0;
            _FK_Store = 0;
            _ToStore = 0;
            _IndentDate = DateTime.Now;
            _xmlGriddata = string.Empty;
            _Remarks = string.Empty;
            _FromDate = DateTime.Now;
            _Todate = DateTime.Now;
            _FK_Reason = 0;
            _FK_Stock = 0;
            _ToStoreE = 0;
            _FK_Items = 0;
            
            
        }
        public long FK_Reason
        {
            get { return _FK_Reason; }
            set { _FK_Reason = value; }
        }
        public long FK_Items
        {
            get { return _FK_Items; }
            set { _FK_Items = value; }
        }
        public long ToStoreE
        {
            get { return _ToStoreE; }
            set { _ToStoreE = value; }
        }
        public byte UserAction
        {
            get { return _UserAction; }
            set { _UserAction = value; }
        }
        public string  xmlGriddata
        {
            get { return _xmlGriddata; }
            set { _xmlGriddata = value; }
        }
        public string Remarks
        {
            get { return _Remarks; }
           set { _Remarks = value; }
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
            get { return _MasterId; }
            set { _MasterId = value; }
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
        public DateTime IndentDate
        {
            get { return _IndentDate; }
            set { _IndentDate = value; }
        }
        public DateTime Todate
        {
            get { return _Todate; }
            set { _Todate = value; }
        }
        public DateTime FromDate
        {
            get { return _FromDate; }
            set { _FromDate = value; }
        }
        public Boolean Cancelled
        {
            get { return _Cancelled; }
            set { _Cancelled = value; }
        }
        public long FK_Client
        {
            get { return _FK_Client; }
            set { _FK_Client = value; }
        }


        public string CancelledBy
        {
            get { return _CancelledBy; }
            set { _CancelledBy = value; }
        }

        public DateTime CancelledOn
        {
            get { return _CancelledOn; }
            set { _CancelledOn = value; }
        }
        public long ReqID
        {
            get { return _ReqID; }
            set { _ReqID = value; }
        }
       
        public long FK_Store
        {
            get { return _FK_Store; }
            set { _FK_Store = value; }
        }
        public long ToStore
        {
            get { return _ToStore; }
            set { _ToStore = value; }
        }
        public decimal FK_Stock
        {
            get { return _FK_Stock; }
            set { _FK_Stock = value; }
        }


        public DataTable SelectIntentReq()
        {
            DataTable dtbl = new DataTable();
            DalIndent Indent = new DalIndent();
            dtbl = Indent.SelectIntentREq(this);
            Indent = null;
            return dtbl;
        }
        public DataTable SelectRequestPopup()
        {
            DataTable dtbl = new DataTable();
            DalIndent Indent = new DalIndent();
            dtbl = Indent.SelectReqPopup(this);
            Indent = null;
            return dtbl;
        }
        public DataTable SelectIndentData()
        {
            DalIndent Indent = new DalIndent();
            DataTable dtbl = Indent.SelectIndentData(this);
            Indent = null;
            return dtbl;

        }
        public DataSet SelectIndentStockEdit()
        {
            DalIndent Indent = new DalIndent();
            DataSet dtbl = Indent.SelectIndentStockEdit(this);
            Indent = null;
            return dtbl;

        }
        public DataSet SelectdetailsGridData()
        {
            DataSet dtbl = new DataSet();
            DalIndent DalIndent = new DalIndent();
            dtbl = DalIndent.SelectItemDetailsGrid(this);
            DalIndent = null;
            return dtbl;
        }
     
        public void SelectData()
        {
            DalIndent DalIndent = new DalIndent();
            IIndent Indent;
            Indent = (IIndent)DalIndent.SelectMasterData(this);
            _ReqID = Indent.ReqID;
            _FK_Store = Indent.FK_Store;
            _UserCode = Indent.UserCode;
            _IndentDate = Indent.IndentDate;
            DalIndent = null;
        }
        public long InsertData()
        {
            long status;
            DalIndent Indent = new DalIndent();
            status = 0;
            try
            {
                _UserAction = 1;
                status = Indent.UpdateIndent(this);
                Indent = null;
            }
            catch (Exception ex)
            {

            }
            return status;
        }
        public long UpdateData()
        {
            _UserAction = 2;
            DalIndent Indent = new DalIndent();
            long status;
            status = 0;
            try
            {
                status = Indent.UpdateIndent(this);
                Indent = null;
            }
            catch (Exception ex)
            {

            }
            return status;
        }
        public long DeleteIndent()
        {
            long status = 0;
            DalIndent dalindent = new DalIndent();
            try
            {


                status = dalindent.DeleteIndent(this);
                dalindent = null;
            }
            catch (Exception ex)
            {
            }
            return status;


        }
        public long IndentApproval()
        {

            DalIndent dalindent = new DalIndent();
            //_UserAction = 1;
            long status = 0;
            try
            {
                status = dalindent.IndentApprove(this);
            }
            catch (Exception ex)
            {

            }
            return status;
        }


       
    }
    public class IIndentItemDetails
    {
        private Int64 _SlNo;
        private Int64 _FK_Indent;
        private Int64 _FK_Stock;
        private Int64 _FK_Items;
        private Int64 _FK_Store;
        private string _Batch;
        private DateTime _Expdate;
        private Decimal _SalesPrice;
        private Decimal _MRP;
        private Decimal _Quantity;
        private Decimal _CurrentStock;
        private Decimal _RequestedQuantity;
        public IIndentItemDetails()
        {
            Initialize();
        }
        public void Initialize()
        {

            _SlNo = 0;
            _FK_Indent = 0;
            _FK_Stock = 0;
            _FK_Items = 0;
            _FK_Store = 0;
            _Batch = string.Empty;
            _Expdate = DateTime.Now;
            _SalesPrice = 0;
            _MRP = 0;
            _Quantity = 0;
            _CurrentStock = 0;
            _RequestedQuantity = 0;
            //_FK_Category = 0;
            //_FK_Item = 0;
            //_SlNo = 0;
            //_FK_Stock = 0;
            //_SalesPrice = 0;
            //_NewSalesPrice = 0;
            //_Quantity = 0;
        }
        public Int64 SlNo
        {
            get { return _SlNo; }
            set { _SlNo = value; }
        }
        public Int64 FK_Indent
        {
            get { return _FK_Indent; }
            set { _FK_Indent = value; }
        }
        public Int64 FK_Stock
        {
            get { return _FK_Stock; }
            set { _FK_Stock = value; }
        }
        public Int64 FK_Items
        {
            get { return _FK_Items; }
            set { _FK_Items = value; }
        }
        public Int64 FK_Store
        {
            get { return _FK_Store; }
            set { _FK_Store = value; }
        }
        public string Batch
        {
            get { return _Batch; }
            set { _Batch = value; }
        }
        public DateTime Expdate
        {
            get { return _Expdate; }
            set { _Expdate = value; }
        }
        public decimal SalesPrice
        {
            get { return _SalesPrice; }
            set { _SalesPrice = value; }
        }
        public decimal MRP
        {
            get { return _MRP; }
            set { _MRP = value; }
        }
        public decimal Quantity
        {
            get { return _Quantity; }
            set { _Quantity = value; }
        }
        public decimal CurrentStock
        {
            get { return _CurrentStock; }
            set { _CurrentStock = value; }
        }
        public decimal RequestedQuantity
        {
            get { return _RequestedQuantity; }
            set { _RequestedQuantity = value; }
        }


    }
}
