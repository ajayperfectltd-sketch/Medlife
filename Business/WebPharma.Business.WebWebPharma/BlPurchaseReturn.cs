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
    public class BlPurchaseReturn : IPurchaseReturn
    {
        #region Properties
        public BlPurchaseReturn()
        {
            Initialize();
        }
        public void Initialize()
        {
            _ID_PurchaseReturn = 0;
            _BillNo = 0;
            _BillDate = DateTime.Now;
            _ExpiryDate = string.Empty;
            _Invoice = "";
            _InvDate = DateTime.Now;
            _FK_Supplier = 0;
            _SupplierName = string.Empty;
            _FK_Store = 0;
            _FK_Category = 0;
            _TotalAmt = 0;
            _DiscountAmt = 0;
            _NetAmount = 0;
            _Remarks = string.Empty;
            _Credit = false;
            _FK_Purchase = 0;
            _UserCode = "";
            _EnteredOn = DateTime.Now;
            _Cancelled = false;
            _Discount = 0;
            _CancelledBy = "";
            _CancelledOn = DateTime.Now;
            _FK_Client = 0;
            _UserAction = 1;
            _AccountPosting = false;
            _FK_BillType = 0;
            PageIndex = 1;
            PageSize = 10;
            _PurchaseDiscPercentage = 0;
            _FK_Items = 0;
            _TransactionID = 0;
            _Expiry = false; _ReturnMethod = "";
        }
        private bool _Expiry;

        private long _TransactionID;
        private decimal _PurchaseDiscPercentage;
        private string _DatasetName;
        private string _ReportSchema;
        private string _ReportTable;
        private string _ReportSchemaPath;
        private string _ReportPath; private string _ReturnMethod;
        
        private string _ReportFile;
        public long TransactionID
        {
            get { return _TransactionID; }
            set { _TransactionID = value; }
        }
        public decimal PurchaseDiscPercentage
        {
            get { return _PurchaseDiscPercentage; }
            set { _PurchaseDiscPercentage = value; }
        }
        public string ReportFile
        {
            get { return _ReportFile; }
            set { _ReportFile = value; }
        }

        public string ReportSchema
        {
            get { return _ReportSchema; }
            set { _ReportSchema = value; }
        }

        public string ReportTable
        {
            get { return _ReportTable; }
            set { _ReportTable = value; }
        }

        public string ReportSchemaPath
        {
            get { return _ReportSchemaPath; }
            set { _ReportSchemaPath = value; }
        }

        public string ReportPath
        {
            get { return _ReportPath; }
            set { _ReportPath = value; }
        }
        public string ReturnMethod
        {
            get { return _ReturnMethod; }
            set { _ReturnMethod = value; }
        }
        public string DatasetName
        {
            get { return _DatasetName; }
            set { _DatasetName = value; }
        }
        private Int64 _FK_Items;

        public Int64 FK_Items
        {
            get { return _FK_Items; }
            set { _FK_Items = value; }
        }
  
        private Int64 _FK_BillType;

        public Int64 FK_BillType
        {
            get { return _FK_BillType; }
            set { _FK_BillType = value; }
        }

        private Int64 _FK_Purchase;

        public Int64 FK_Purchase
        {
            get { return _FK_Purchase; }
            set { _FK_Purchase = value; }
        }
        public bool Expiry
        {
            get { return _Expiry; }
            set { _Expiry = value; }
        }
        private decimal _Discount;

        public decimal Discount
        {
            get { return _Discount; }
            set { _Discount = value; }
        }

        private bool _Credit;

        public bool Credit
        {
            get { return _Credit; }
            set { _Credit = value; }
        }
        private string _Remarks;

        public string Remarks
        {
            get { return _Remarks; }
            set { _Remarks = value; }
        }
        private decimal _NetAmount;

        public decimal NetAmount
        {
            get { return _NetAmount; }
            set { _NetAmount = value; }
        }private decimal _OtherCharge;

        public decimal OtherCharge
        {
            get { return _OtherCharge; }
            set { _OtherCharge = value; }
        }

        private decimal _DiscountAmt;

        public decimal DiscountAmt
        {
            get { return _DiscountAmt; }
            set { _DiscountAmt = value; }
        }

        private decimal _TotalAmt;

        public decimal TotalAmt
        {
            get { return _TotalAmt; }
            set { _TotalAmt = value; }
        }

        private long _FK_Category;
        public long FK_Category
        {
            get { return _FK_Category; }
            set { _FK_Category = value; }
        }

        private string _GSTIN;

        public string GSTIN
        {
            get { return _GSTIN; }
            set { _GSTIN = value; }
        }
        private string _Invoice;

        public string Invoice
        {
            get { return _Invoice; }
            set { _Invoice = value; }
        }

        private Boolean _DirectReturn;
        private Boolean _AccountPosting;

        public Boolean AccountPosting
        {
            get { return _AccountPosting; }
            set { _AccountPosting = value; }
        }

        private long _BillNo;

        public long BillNo
        {
            get { return _BillNo; }
            set { _BillNo = value; }
        }
        private DateTime _BillDate;

        public DateTime BillDate
        {
            get { return _BillDate; }
            set { _BillDate = value; }
        } private string _ExpiryDate;

        public string ExpiryDate
        {
            get { return _ExpiryDate; }
            set { _ExpiryDate = value; }
        }
        

        private DateTime _InvDate;

        public DateTime InvDate
        {
            get { return _InvDate; }
            set { _InvDate = value; }
        }
        private long _FK_Supplier;

        public long FK_Supplier
        {
            get { return _FK_Supplier; }
            set { _FK_Supplier = value; }
        }
        private string _SupplierName;
         public string SupplierName
        {
            get { return _SupplierName; }
            set { _SupplierName = value; }
        }



        private decimal _Cost;

        public decimal Cost
        {
            get { return _Cost; }
            set { _Cost = value; }
        }



        private long _FK_Store;

        public long FK_Store
        {
            get { return _FK_Store; }
            set { _FK_Store = value; }
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
        public Boolean DirectReturn
        {
            get { return _DirectReturn; }
            set { _DirectReturn = value; }
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
        private string _XmlPR;

        public string XmlPR
        {
            get { return _XmlPR; }
            set { _XmlPR = value; }
        }
        public Int16 PageSize
        {
            get { return _PageSize; }
            set { _PageSize = value; }
        }
        private long _ID_PurchaseReturn;

        public long MasterId
        {
            get { return _ID_PurchaseReturn; }
            set { _ID_PurchaseReturn = value; }
        }
        private bool _Approved;

        public bool Approved
        {
            get { return _Approved; }
            set { _Approved = value; }
        }
        #endregion
        #region Process
        public long InsertData()
        {
            long status = 0;
            DalPurchaseReturn dalPR = new DalPurchaseReturn();
            try
            {
                UserAction = 1;
                status = dalPR.UpdatePurchaseReturn(this);
                dalPR = null;
            }
            catch (Exception ex)
            {
            }
            return status;
        }
        public long ApproveData()
        {
            long status = 0;
            DalPurchaseReturn dalPR = new DalPurchaseReturn();
            try
            {
                UserAction = 1;
                status = dalPR.ApprovePurchaseReturn(this);
                dalPR = null;
            }
            catch (Exception ex)
            {
            }
            return status;
        }
        public long UpdateData()
        {
            long status = 0;
            DalPurchaseReturn dalPR = new DalPurchaseReturn();
            try
            {
                UserAction = 2;
                status = dalPR.UpdatePurchaseReturn(this);
                dalPR = null;
            }
            catch (Exception ex)
            {
            }
            return status;

        }

        public long DeletePurchaseReturn()
        {
            long status = 0;
            DalPurchaseReturn dalPR = new DalPurchaseReturn();
            try
            {

                UserAction = 3;
                status = dalPR.UpdatePurchaseReturn(this);
                dalPR = null;
            }
            catch (Exception ex)
            {
            }
            return status;


        }
        public void SelectData()
        {
            DalPurchaseReturn dalPR = new DalPurchaseReturn();
            IPurchaseReturn ipurchaseReturn;
            ipurchaseReturn = (IPurchaseReturn)dalPR.SelectPurchaseReturn(this);

           
            _BillNo = ipurchaseReturn.BillNo;

            _BillDate = ipurchaseReturn.BillDate;

            _Invoice = ipurchaseReturn.Invoice;

            _InvDate = ipurchaseReturn.InvDate;
            _FK_Supplier = ipurchaseReturn.FK_Supplier;

            _GSTIN = ipurchaseReturn.GSTIN;
            _FK_Store = ipurchaseReturn.FK_Store;
            _FK_Category = ipurchaseReturn.FK_Category;
            _TotalAmt = ipurchaseReturn.TotalAmt;
            _SupplierName = ipurchaseReturn.SupplierName;
            _DiscountAmt = ipurchaseReturn.DiscountAmt;
            _NetAmount = ipurchaseReturn.NetAmount;

            _Remarks = ipurchaseReturn.Remarks;
            _Credit = ipurchaseReturn.Credit;
            _Cancelled = ipurchaseReturn.Cancelled;
            _CancelledBy = ipurchaseReturn.CancelledBy;
            _CancelledOn = ipurchaseReturn.CancelledOn;
            _XmlPR = ipurchaseReturn.XmlPR;
            _FromDate = ipurchaseReturn.FromDate;
            _ToDate = ipurchaseReturn.ToDate;
            _DirectReturn = ipurchaseReturn.DirectReturn;
            _FK_Purchase = ipurchaseReturn.FK_Purchase;
            _AccountPosting = ipurchaseReturn.AccountPosting;
            _FK_BillType=ipurchaseReturn.FK_BillType;
            _Expiry = ipurchaseReturn.Expiry;
            _Discount = ipurchaseReturn.Discount;
            _ReturnMethod = ipurchaseReturn.ReturnMethod;
            _OtherCharge = ipurchaseReturn.OtherCharge;
            _TotalAmt = ipurchaseReturn.TotalAmt;
            _ExpiryDate = ipurchaseReturn.ExpiryDate;
            dalPR = null;
            _Approved = ipurchaseReturn.Approved;

        }
        public DataTable SelectAllData()
        {
            DataTable dtbl = new DataTable();
            DalPurchaseReturn dalPR = new DalPurchaseReturn();
            dtbl = dalPR.SelectPurchaseReturnAll(this);
            dalPR = null;
            return dtbl;
        }
        public DataTable SelectPurchaseItems()
        {
            DataTable dtbl = new DataTable();
            DalPurchaseReturn dalPR = new DalPurchaseReturn();
            dtbl = dalPR.SelectPurchaseItems(this);

            dalPR = null;
            return dtbl;
        }
        public DataTable SelectPurchaseWiseItemsSelect()
        {
            DataTable dtbl = new DataTable();
            DalPurchaseReturn dalPR = new DalPurchaseReturn();
            dtbl = dalPR.SelectPurchaseWiseItemsSelect(this);

            dalPR = null;
            return dtbl;
        }
        
        public DataTable SelectNonreturnableItems()
        {
            DataTable dtbl = new DataTable();
            DalPurchaseReturn dalPR = new DalPurchaseReturn();
            dtbl = dalPR.SelectNonreturnableItems(this);

            dalPR = null;
            return dtbl;
        }
        public DataTable DirectPurchaseReturnItems()
        {
            DataTable dtbl = new DataTable();
            DalPurchaseReturn dalPR = new DalPurchaseReturn();
            dtbl = dalPR.DirectPurchaseReturnItems(this);

            dalPR = null;
            return dtbl;
        }
        public DataSet GetPurchaseReturnDataSource(long StatusId)
        {
            DalPurchaseReturn dalPR = new DalPurchaseReturn();
            DataSet ds = new DataSet();
            try
            {
                ds = dalPR.GetPurchaseReturnDataSource(StatusId);
                ds.DataSetName = _DatasetName;
                ds.Tables[0].TableName = ReportTable;
                ds.WriteXmlSchema(_ReportSchemaPath + _ReportSchema + ".xsd");
            }
            catch (Exception ex)
            {
            }
            finally
            {
                dalPR = null;
            }
            return ds;
        }
        public DataTable ExpiryReturnItemWiseRounfOff()
        {
            DataTable dtbl = new DataTable();
            DalPurchaseReturn dalPR = new DalPurchaseReturn();
            dtbl = dalPR.ExpiryReturnItemWiseRounfOff(this);

            dalPR = null;
            return dtbl;
        }
        #endregion
    }
    public class BlPurchaseReturnDetails
    {

        
        private long _FK_Items;
        private long _FK_Stock;
        private long _FK_Units;
        private string _BatchNo;
        private DateTime _ExpiryDate;
        private decimal _Quantity;
        private decimal _ActQty;
        private decimal _Free;
        private decimal _PurRate;
        private decimal _MRP;
        private decimal _SalesPrice;       
        private long _FK_Reason;
        private Int32  _SlNo;
        private decimal _NoofUnits;
        private decimal _PurchaseDiscAmount;
        private decimal _PurchaseDiscPercentage;
        private long _FK_TaxGroupID;
        private bool _DiscountOnTax;
        private String _PurInvoiceNo;
        private DateTime? _PurInvoiceDate;
        public BlPurchaseReturnDetails()
        {
            Initialize();
        }
        public void Initialize()
        {
            _PurInvoiceNo=string.Empty;
            _PurInvoiceDate = DateTime.Now;
        }
        public bool DiscountOnTax
        {
            get { return _DiscountOnTax; }
            set { _DiscountOnTax = value; }
        }
        private bool _IncludePtax;

        public bool IncludePtax
        {
            get { return _IncludePtax; }
            set { _IncludePtax = value; }
        }
        public long FK_TaxGroupID
        {
            get { return _FK_TaxGroupID; }
            set { _FK_TaxGroupID = value; }
        }
        public decimal PurchaseDiscPercentage
        {
            get { return _PurchaseDiscPercentage; }
            set { _PurchaseDiscPercentage = value; }
        }

        public decimal NoofUnits
        {
            get { return _NoofUnits; }
            set { _NoofUnits = value; }
        }
        public Int32 SlNo
        {
            get { return _SlNo; }
            set { _SlNo = value; }
        }

        public long FK_Items
        {
            get { return _FK_Items; }
            set { _FK_Items = value; }
        }

        public long FK_Stock
        {
            get { return _FK_Stock; }
            set { _FK_Stock = value; }
        }

        public long FK_Units
        {
            get { return _FK_Units; }
            set { _FK_Units = value; }
        }

        public string BatchNo
        {
            get { return _BatchNo; }
            set { _BatchNo = value; }
        }
        public DateTime ExpiryDate
        {
            get { return _ExpiryDate; }
            set { _ExpiryDate = value; }
        }
        public decimal Quantity
        {
            get { return _Quantity; }
            set { _Quantity = value; }
        }
        public decimal ActQty
        {
            get { return _ActQty; }
            set { _ActQty = value; }
        }
        public decimal Free
        {
            get { return _Free; }
            set { _Free = value; }
        }
        public decimal PurRate
        {
            get { return _PurRate; }
            set { _PurRate = value; }
        }
       
        public decimal MRP
        {
            get { return _MRP; }
            set { _MRP = value; }
        }

        public decimal SalesPrice
        {
            get { return _SalesPrice; }
            set { _SalesPrice = value; }
        }  

        public long FK_Reason
        {
            get { return _FK_Reason; }
            set { _FK_Reason = value; }
        }
        public decimal PurchaseDiscAmount
        {
            get { return _PurchaseDiscAmount; }
            set { _PurchaseDiscAmount = value; }
        }
        public string PurInvoiceNo
        {
            get { return _PurInvoiceNo; }
            set { _PurInvoiceNo = value; }
        }
        public DateTime? PurInvoiceDate
        {
            get { return _PurInvoiceDate; }
            set { _PurInvoiceDate = value; }
        }
    }

}
