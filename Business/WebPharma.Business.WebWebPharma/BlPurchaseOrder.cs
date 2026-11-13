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
    public class BlPurchaseOrder:IPurchaseOrder
    {
        public BlPurchaseOrder()
        {
            Initialize();
        }
        public void Initialize()
        {
            _UserAction = 1;
            _FK_Store = 0;
            _FK_Client = 1;
            _PageIndex = 1;
            _PageSize = 10;
            _PONo = "";
            _Date = DateTime.Now;
            _FK_Supplier = 0;
            _totalAmount = 0;
            _Discount = 0;
            _RoundAmount = 0;
            _NetAmount = 0;
            _Advance = 0;
            _Remarks = string.Empty;
            _CashAmount = 0;
            _DeliveryDate = DateTime.Now;
            _DeliveryType = "Normal";
            _Closed = false;
            _ClosedUser = "";
            _ClosedRemarks = "";
            _Cancelled = false;
            _CancelledUser = "";
            _CancelledOn = DateTime.Now;
            _FromDate = DateTime.Parse("01/01/1900");
            _ToDate = DateTime.Parse("1/1/9999");
            _isClosed = "";
            _UserCode = "";
            _FK_ItemType = 0;
            _Quantity = 0;
            _FK_Reason = 0;
            
            

        }
        #region Properties
        private byte _UserAction;
        private Int16 _PageIndex;
        private Int16 _PageSize;
        private long _FK_Client;
        private long _FK_Store;
        private long _FK_ItemType;
        private long _FK_Reason;
        public long FK_Reason
        {
            get { return _FK_Reason; }
            set { _FK_Reason = value; }
        }
        public long FK_ItemType
        {
            get { return _FK_ItemType; }
            set { _FK_ItemType = value; }
        }
        private string _isClosed;

        public string isClosed
        {
            get { return _isClosed; }
            set { _isClosed = value; }
        }
        private string _PONo;
      

      
        public string PONo
        {
            get { return _PONo; }
            set { _PONo = value; }
        }
        private DateTime _Date;

        public DateTime Date
        {
            get { return _Date; }
            set { _Date = value; }
        }
        private long _FK_Supplier;
        private decimal _totalAmount;
        private decimal _Discount;
        private decimal _RoundAmount;
        private decimal _NetAmount;
        private decimal _Advance;
        private string _Remarks;
        private decimal _CashAmount;
        private DateTime _DeliveryDate;
        private string _DeliveryType;
         private bool _Closed;
         private string _ClosedUser;
         private string _ClosedRemarks;
         private bool _Cancelled;
         private string _CancelledUser;
         private DateTime _CancelledOn;
         private string _XmlPurchaseItems;
         private DateTime _FromDate;
         private DateTime _ToDate;
         private DateTime _EnteredOn;
         private string _UserCode;
         private decimal _Quantity;


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
     
         private long _ID_PurchaseOrder;

         public DateTime EnteredOn
         {
             get { return _EnteredOn; }
             set { _EnteredOn = value; }
         }
         public long MasterId
         {
             get { return _ID_PurchaseOrder; }
             set { _ID_PurchaseOrder = value; }
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
        public decimal Discount
        {
            get { return _Discount; }
            set { _Discount = value; }
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
        public decimal Advance
        {
            get { return _Advance; }
            set { _Advance = value; }
        }
        public string Remarks
        {
            get { return _Remarks; }
            set { _Remarks = value; }
        }
        public decimal CashAmount
        {
            get { return _CashAmount; }
            set { _CashAmount = value; }
        }
        public DateTime DeliveryDate
        {
            get { return _DeliveryDate; }
            set { _DeliveryDate = value; }

        }
       
        public string DeliveryType
        {
            get { return _DeliveryType; }
            set { _DeliveryType = value; }
        }
        public bool Closed
        {
            get { return _Closed; }
            set { _Closed = value; }
        }
        public string ClosedUser
        {
            get { return _ClosedUser; }
            set { _ClosedUser = value; }
        }
        public string ClosedRemarks
        {
            get { return _ClosedRemarks; }
            set { _ClosedRemarks = value; }
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
        public decimal Quantity
        {
            get { return _Quantity; }
            set { _Quantity = value; }
        }
        #endregion
        #region Process
        public long InsertData()
        {
            long status;
            DalPurchaseOrder dalPurchase = new DalPurchaseOrder();
            status = 0;
            try
            {
                _UserAction = 1;
                status = dalPurchase.UpdatePurchaseOrder(this);
                dalPurchase = null;
            }
            catch (Exception ex)
            {

            }
            return status;
        }
        public long UpdateData()
        {
            
            DalPurchaseOrder dalPurchase = new DalPurchaseOrder();
            long status;
            status = 0;
            try
            {
                _UserAction = 2;
                status = dalPurchase.UpdatePurchaseOrder(this);
                dalPurchase = null;
            }
            catch (Exception ex)
            {

            }
            return status;
        }
        public long UpdatePurchaseOrderStatusOnly()
        {
            long status;
            DalPurchaseOrder dalPurchase = new DalPurchaseOrder();
            status = 0;
            try
            {
              
                status = dalPurchase.UpdatePurchaseOrderStatusOnly(this);
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
            DalPurchaseOrder dalPurchase = new DalPurchaseOrder();
            long status = 0;
            try
            {

                status = dalPurchase.DeleteStock(this);
                dalPurchase = null;
            }
            catch (Exception ex)
            {
            }
            return status;
        }
        public void SelectData()
        {
             DalPurchaseOrder dalPurchase = new DalPurchaseOrder();
             IPurchaseOrder IPurchase;
             IPurchase = (IPurchaseOrder)dalPurchase.SelectData(this);
             
             _PONo=IPurchase.PONo;
             _Date=IPurchase.Date;
            _FK_Supplier=IPurchase.FK_Supplier;
            _totalAmount=IPurchase.totalAmount;
            _Discount=IPurchase.Discount;
            _RoundAmount=IPurchase.RoundAmount;
            _NetAmount=IPurchase.NetAmount;
            _Advance=IPurchase.Advance;
            _Remarks=IPurchase.Remarks;
            _CashAmount=IPurchase.CashAmount;
            _DeliveryDate=IPurchase.DeliveryDate;
            _DeliveryType=IPurchase.DeliveryType;
            _Closed=IPurchase.Closed;
            _ClosedRemarks=IPurchase.ClosedRemarks;
           _UserCode=IPurchase.UserCode;
           _FK_Store = IPurchase.FK_Store;
           _FK_ItemType = IPurchase.FK_ItemType;
           dalPurchase = null;
           
        }
        public DataTable SelectAllData()
        {
            DataTable dtbl = new DataTable();
            DalPurchaseOrder dalPurchase = new DalPurchaseOrder();
            dtbl = dalPurchase.SelectAllPurchaseOrder(this);
            dalPurchase = null;
            return dtbl;
        }
    
        public DataTable SelectPurchaseOrderItems()
        {   
             DataTable dtbl=new DataTable();
             DalPurchaseOrder dalPurchase = new DalPurchaseOrder();
             dtbl=dalPurchase.SelectPurchaseOrderItems(this);
             dalPurchase = null;
             return dtbl;
        }
        public DataTable SelectPurchaseOrderSmartFillItems()
        {
            DataTable dtbl = new DataTable();
            DalPurchaseOrder dalPurchase = new DalPurchaseOrder();
            dtbl = dalPurchase.SelectPurchaseOrderSmartFillItems(this);
            dalPurchase = null;
            return dtbl;
        }
        #endregion
    }

    public class IPurchaseOrderDetails
    {
        /*private Int64 _FK_PurchaseOrder;

        public Int64 FK_PurchaseOrder
        {
            get { return _FK_PurchaseOrder; }
            set { _FK_PurchaseOrder = value; }
        }*/
        private Int64 _FK_Items;
        private Int64 _FK_Units;
        private Int64 _FK_SubUnit;

        public Int64 FK_SubUnit
        {
            get { return _FK_SubUnit; }
            set { _FK_SubUnit = value; }
        }

        public Int64 FK_Units
        {
            get { return _FK_Units; }
            set { _FK_Units = value; }
        }
        public Int64 FK_Items
        {
            get { return _FK_Items; }
            set { _FK_Items = value; }
        }
        private decimal _Quantity;

        public decimal Quantity
        {
            get { return _Quantity; }
            set { _Quantity = value; }
        }
        private decimal _SalesPrice;

        public decimal SalesPrice
        {
            get { return _SalesPrice; }
            set { _SalesPrice = value; }
        }
        private decimal _PurchaseRate;

        public decimal PurchaseRate
        {
            get { return _PurchaseRate; }
            set { _PurchaseRate = value; }
        }
        private decimal _DiscountItem;

        public decimal DiscountItem
        {
            get { return _DiscountItem; }
            set { _DiscountItem = value; }
        }
        private string _BatchNo;

        public string BatchNo
        {
            get { return _BatchNo; }
            set { _BatchNo = value; }
        }
        private string _Notes;

        public string Notes
        {
            get { return _Notes; }
            set { _Notes = value; }
        }
    
    
        
    }
}
