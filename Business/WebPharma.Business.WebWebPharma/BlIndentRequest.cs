/*----------------------------------- Business Layer Class -----------------------------------------------------------------------------------------------------------------------
Purpose    : 
Author     : Aiswarya
Copyright  : Perfect Software Solutions (Clt) Pvt Ltd
Created on : 01/10/2018
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
    public class BlIndentRequest:IIndentRequest
    {
        public BlIndentRequest()
        {
            Initialize();
        }
        public void Initialize()
        {
            _UserAction = 1;
            _FK_Store = 0;
            _ToStore = 0;
            _FK_Client = 1;
            _PageIndex = 1;
            _PageSize = 10;
            _InReqDate = DateTime.Now;
            _totalAmount = 0;
            _NetAmount = 0;
            _Remarks = string.Empty;
            _Cancelled = false;
            _CancelledUser = "";
            _CancelledOn = DateTime.Now;
            _FromDate = DateTime.Parse("01/01/1900");
            _ToDate = DateTime.Parse("1/1/9999");
            _UserCode = "";
            _XmlIndentRequestItems = string.Empty;
            _FK_Reason = 0;


        }
        #region Properties
        private byte _UserAction;
        private Int16 _PageIndex;
        private Int16 _PageSize;
        private long _FK_Client;
        private long _FK_Store;
        private long _ToStore;
        private DateTime _InReqDate;
        private string _XmlIndentRequestItems;
        private long _FK_Reason;
        public long FK_Reason
        {
            get { return _FK_Reason; }
            set { _FK_Reason = value; }
        }
        public DateTime InReqDate
        {
            get { return _InReqDate; }
            set { _InReqDate = value; }
        }
       
         private decimal _totalAmount;
        
         private decimal _RoundAmount;
         private decimal _NetAmount;
         private decimal _Advance;
         private string _Remarks;
         private bool _Cancelled;
         private string _CancelledUser;
         private DateTime _CancelledOn;
         private string _XmlPurchaseItems;
         private DateTime _FromDate;
         private DateTime _ToDate;
         private DateTime _EnteredOn;
         private string _UserCode;


         public string XmlIndentRequestItems
         {
             get { return _XmlIndentRequestItems; }
             set { _XmlIndentRequestItems = value; }
         }
         public string UserCode
         {
             get { return _UserCode; }
             set { _UserCode = value; }
         }
     
         private long _ID_IndentRequest;

         public DateTime EnteredOn
         {
             get { return _EnteredOn; }
             set { _EnteredOn = value; }
         }
         public long MasterId
         {
             get { return _ID_IndentRequest; }
             set { _ID_IndentRequest = value; }
         }



        //private DateTime 
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

        public decimal totalAmount
        {
            get { return _totalAmount; }
            set { _totalAmount = value; }
        }
      
        public decimal RoundAmount
        {
            get { return _RoundAmount; }
            set { _RoundAmount = value; }
        }
        public decimal NetAmount
        {
            get { return _NetAmount; }
            set { _NetAmount = value; }
        }
     
        public string Remarks
        {
            get { return _Remarks; }
            set { _Remarks = value; }
        }
      
   
        public bool Cancelled
        {
            get { return _Cancelled; }
            set { _Cancelled = value; }
        }
        public string CancelledUser
        {
            get { return _CancelledUser; }
            set { _CancelledUser = value; }
        }
        public DateTime CancelledOn
        {
            get { return _CancelledOn; }
            set { _CancelledOn = value; }
        }
        public string XmlPurchaseItems
        {
            get { return _XmlPurchaseItems; }
            set { _XmlPurchaseItems = value; }
        }
        public DateTime FromDate
        {
            get { return _FromDate; }
            set { _FromDate = value; }
        }

        public DateTime ToDate
        {
            get { return _ToDate; }
            set { _ToDate = value; }
        }
        #endregion
        #region Process
        public long InsertData()
        {
            long status;
            DalIndentRequest dalPurchase = new DalIndentRequest();
            status = 0;
            try
            {
                _UserAction = 1;
                status = dalPurchase.UpdateIndentRequest(this);
                dalPurchase = null;
            }
            catch (Exception ex)
            {

            }
            return status;
        }
        public long UpdateData()
        {
            
            DalIndentRequest dalPurchase = new DalIndentRequest();
            long status;
            status = 0;
            try
            {
                _UserAction = 2;
                status = dalPurchase.UpdateIndentRequest(this);
                dalPurchase = null;
            }
            catch (Exception ex)
            {

            }
            return status;
        }
        public long UpdateIndentRequestStatusOnly()
        {
            long status;
            DalIndentRequest dalPurchase = new DalIndentRequest();
            status = 0;
            try
            {
              
                status = dalPurchase.UpdateIndentRequestStatusOnly(this);
                dalPurchase = null;
            }
            catch (Exception ex)
            {

            }
            return status;
        }
        public long DeleteData()
        {
            _UserAction = 3;
            DalIndentRequest DalIndent = new DalIndentRequest();
            long status = 0;
            try
            {

                status = DalIndent.DeleteIntentRequest(this);
                DalIndent = null;
            }
            catch (Exception ex)
            {
            }
            return status;
        }
        public void SelectData()
        {
             DalIndentRequest dalIndent = new DalIndentRequest();
             IIndentRequest IdalIn;
             IdalIn = (IIndentRequest)dalIndent.SelectData(this);             
            // _PONo=IPurchase.PONo;
             _InReqDate = IdalIn.InReqDate;
             _Remarks = IdalIn.Remarks;
            //_Approved=IPurchase.Approved;
            //_ApprovedUser = IPurchase.ApprovedUser; 
             _UserCode = IdalIn.UserCode;
             _FK_Store = IdalIn.FK_Store;
             _ToStore = IdalIn.ToStore;
         
           dalIndent = null;
           
        }
        public DataTable SelectIntendRequestIDData()
        {
            DataTable dtbl = new DataTable();
            DalIndentRequest dalPurchase = new DalIndentRequest();
            dtbl = dalPurchase.SelectIntendRequestIDData(this);
            dalPurchase = null;
            return dtbl;
        }
        public DataTable SelectAllData()
        {
            DataTable dtbl = new DataTable();
            DalIndentRequest dalPurchase = new DalIndentRequest();
            dtbl = dalPurchase.SelectAllIndentRequest(this);
            dalPurchase = null;
            return dtbl;
        }
        public DataTable SelectIndentRequestItems()
        {   
             DataTable dtbl=new DataTable();
             DalIndentRequest dalPurchase = new DalIndentRequest();
             dtbl=dalPurchase.SelectIndentRequestItems(this);
             dalPurchase = null;
             return dtbl;
        }
        public DataTable SelectIndentRequestSmartFillItems()
        {
            DataTable dtbl = new DataTable();
            DalIndentRequest dalPurchase = new DalIndentRequest();
            dtbl = dalPurchase.SelectIndentRequestSmartFillItems(this);
            dalPurchase = null;
            return dtbl;
        }
        #endregion
    }

    public class IIndentRequestDetails
    {
 
        private Int64 _FK_Items;
        private Int64 _SlNo;
        private Int64 _FK_Stock;
        private decimal _Quantity;

        public Int64 SlNo
        {
            get { return _SlNo; }
            set { _SlNo = value; }
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
     

        public decimal Quantity
        {
            get { return _Quantity; }
            set { _Quantity = value; }
        }
    
      
    }
}
