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
using WebPharma.DataAccess.WebPharma;
using WebPharma.Interface.WebPharma;
using System.Data;

namespace WebPharma.Business.WebPharma
{
    public class BlSaleReturn : ISaleReturn
    {
        #region Properties
        public BlSaleReturn()
        {
            Initialize();
        }
        public void Initialize()
        {

            _FK_Store = 0;
            _FK_ItemId = 0;
            _FK_Stock = 0;
            _FK_BillType = 0;
            _ID_SalesReturn = 0;
            _BillNo = 0;
            _BillDate = DateTime.Now;
            _CustomerType = 0;
            _FK_Customer = 0;
            _CustomerName = string.Empty;
            _CardNumber = string.Empty;
            _BillTotal = 0;
            _Discount = 0;
            _RoundOff = 0;
            _NetAmount = 0;
            _PaymentMode = 0;
            _FK_PaymentDetails = 0;
            _PayAmount = 0;
            _FK_Counter = 0;
            _Remarks = string.Empty;
            _Fk_Sale = 0;
            _UserCode = "";
            _EnteredOn = DateTime.Now;
            _Cancelled = false;
            _Cancelleduser = string.Empty;
            _CancelledOn = DateTime.Now;
            _FK_Client = 0;
            _UserAction = 1;
            _Credit = false;
            PageIndex = 1;
            PageSize = 10;
            _SaleNo = _Customer = "";
            _BatchNo = string.Empty;
            _ExpiryDate = DateTime.Now;
            _FK_Reason = 0;
            _TransactionID = 0;
        }
     
        private string _SaleNo;

        public string SaleNo
        {
            get { return _SaleNo; }
            set { _SaleNo = value; }
        }
        private long _TransactionID;

        public long TransactionID
        {
            get { return _TransactionID; }
            set { _TransactionID = value; }
        }


        private string _Customer;

        public string Customer
        {
            get { return _Customer; }
            set { _Customer = value; }
        }
        private long _FK_Store;
        private long _FK_ItemId;
        private long _FK_Stock;
        private long _FK_BillType;
        private long _BillNo;
        private DateTime _BillDate;
        private short _CustomerType;
        private long _FK_Customer;
        private string _CustomerName;
        private string _CardNumber;
        private decimal _BillTotal;
        private decimal _Discount;
        private decimal _RoundOff;
        private decimal _NetAmount;
        private short _PaymentMode;
        private long _FK_PaymentDetails;
        private decimal _PayAmount;
        private long _FK_Counter;
        private string _Remarks;
        private long _Fk_Sale;
        private string _UserCode;
        private DateTime _EnteredOn;
        private bool _Cancelled;
        private string _Cancelleduser;
        private DateTime _CancelledOn;
        private byte _UserAction;
        private long _FK_Client;
        private Int16 _PageIndex;
        private Int16 _PageSize;
        private string _XmlSR;
        private long _ID_SalesReturn;

        private DateTime _FromDate;
        private DateTime _ToDate;
        private Boolean _DirectReturn;
        private Boolean _Credit;

        private string _DatasetName;
        private string _ReportSchema;
        private string _ReportTable;
        private string _ReportSchemaPath;
        private string _ReportPath;
        private string _ReportFile;
        private string _BatchNo;
        private DateTime _ExpiryDate;
        private long _FK_Reason;

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
        public string DatasetName
        {
            get { return _DatasetName; }
            set { _DatasetName = value; }
        }

        public Boolean Credit
        {
            get { return _Credit; }
            set { _Credit = value; }
        }
        public Boolean DirectReturn
        {
            get { return _DirectReturn; }
            set { _DirectReturn = value; }
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
        public long FK_Store
        {
            get { return _FK_Store; }
            set { _FK_Store = value; }
        }
        public long FK_Reason
        {
            get { return _FK_Reason; }
            set { _FK_Reason = value; }
        }
        public long FK_ItemId
        {
            get { return _FK_ItemId; }
            set { _FK_ItemId = value; }
        }
        public long FK_BillType
        {
            get { return _FK_BillType; }
            set { _FK_BillType = value; }
        }
        public long FK_Stock
        {
            get { return _FK_Stock; }
            set { _FK_Stock = value; }
        }

        public long BillNo
        {
            get { return _BillNo; }
            set { _BillNo = value; }
        }


        public DateTime BillDate
        {
            get { return _BillDate; }
            set { _BillDate = value; }
        }


        public short CustomerType
        {
            get { return _CustomerType; }
            set { _CustomerType = value; }
        }

        public long FK_Customer
        {
            get { return _FK_Customer; }
            set { _FK_Customer = value; }
        }


        public string CustomerName
        {
            get { return _CustomerName; }
            set { _CustomerName = value; }
        }

        public string CardNumber
        {
            get { return _CardNumber; }
            set { _CardNumber = value; }
        }


        public decimal BillTotal
        {
            get { return _BillTotal; }
            set { _BillTotal = value; }
        }

        public decimal Discount
        {
            get { return _Discount; }
            set { _Discount = value; }
        }


        public decimal RoundOff
        {
            get { return _RoundOff; }
            set { _RoundOff = value; }
        }


        public decimal NetAmount
        {
            get { return _NetAmount; }
            set { _NetAmount = value; }
        }


        public short PaymentMode
        {
            get { return _PaymentMode; }
            set { _PaymentMode = value; }
        }


        public long FK_PaymentDetails
        {
            get { return _FK_PaymentDetails; }
            set { _FK_PaymentDetails = value; }
        }


        public decimal PayAmount
        {
            get { return _PayAmount; }
            set { _PayAmount = value; }
        }


        public string Remarks
        {
            get { return _Remarks; }
            set { _Remarks = value; }
        }
        public long FK_Counter
        {
            get { return _FK_Counter; }
            set { _FK_Counter = value; }
        }
        public long Fk_Sale
        {
            get { return _Fk_Sale; }
            set { _Fk_Sale = value; }
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

        public string XmlSR
        {
            get { return _XmlSR; }
            set { _XmlSR = value; }
        }
        public Int16 PageSize
        {
            get { return _PageSize; }
            set { _PageSize = value; }
        }

        public long MasterId
        {
            get { return _ID_SalesReturn; }
            set { _ID_SalesReturn = value; }
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
        #endregion
        #region Process
        public long InsertData()
        {
            long status = 0;
            DalSaleReturn dalSR = new DalSaleReturn();
            try
            {
                UserAction = 1;
                status = dalSR.UpdateSaleReturn(this);
                dalSR = null;
            }
            catch (Exception ex)
            {
            }
            return status;
        }

        public long UpdateData()
        {
            long status = 0;
            DalSaleReturn dalSR = new DalSaleReturn();
            try
            {
                UserAction = 2;
                status = dalSR.UpdateSaleReturn(this);
                dalSR = null;
            }
            catch (Exception ex)
            {
            }
            return status;

        }
        public DataTable SelectGriddata()
        {
            DalSaleReturn SaleReturn = new DalSaleReturn();
            DataTable dtbl = SaleReturn.GridData(this);
            SaleReturn = null;
            return dtbl;

        }
        public DataTable SelectTaxGriddata()
        {
            DalSaleReturn SaleReturn = new DalSaleReturn();
            DataTable dtbl = SaleReturn.TaxGridData(this);
            SaleReturn = null;
            return dtbl;

        }
        public DataTable SelectSaleReturnItems()
        {
            DataTable dtbl = new DataTable();
            DalSaleReturn dalSR = new DalSaleReturn();
            dtbl = dalSR.SelectSaleReturnItems(this);
            dalSR = null;
            return dtbl;
        }

        public DataTable SelectSaleReturnItemsPrint()
        {
            DataTable dtbl = new DataTable();
            DalSaleReturn dalSR = new DalSaleReturn();
            dtbl = dalSR.SelectSaleReturnItemsforPrint(this);
            dalSR = null;
            return dtbl;
        }

        public long DeleteSalesReturn()
        {
            long status = 0;
            DalSaleReturn dalSR = new DalSaleReturn();
            try
            {

                UserAction = 3;
                status = dalSR.UpdateSaleReturn(this);
                dalSR = null;
            }
            catch (Exception ex)
            {
            }
            return status;


        }
        public void SelectData()
        {
            DalSaleReturn dalSR = new DalSaleReturn();
            ISaleReturn iSaleReturn;
            iSaleReturn = (ISaleReturn)dalSR.SelectSaleReturn(this);

            _BillNo = iSaleReturn.BillNo;
            _BillDate = iSaleReturn.BillDate;
            _FK_Customer = iSaleReturn.FK_Customer;
            _FK_Store = iSaleReturn.FK_Store;
            _Discount = iSaleReturn.Discount;
            _NetAmount = iSaleReturn.NetAmount;
            _Remarks = iSaleReturn.Remarks;
            _Cancelled = iSaleReturn.Cancelled;
            _Fk_Sale = iSaleReturn.Fk_Sale;
            _BillTotal = iSaleReturn.BillTotal;
            _RoundOff = iSaleReturn.RoundOff;
            _Credit = iSaleReturn.Credit;
            _FK_BillType = iSaleReturn.FK_BillType;
            _FK_Counter = iSaleReturn.FK_Counter;
            dalSR = null;

        }
        public DataTable SelectAllData()
        {
            DataTable dtbl = new DataTable();
            DalSaleReturn dalSR = new DalSaleReturn();
            dtbl = dalSR.SelectSaleReturnAll(this);
            dalSR = null;
            return dtbl;
        }
        public DataTable SelectAllDataPrint()
        {
            DataTable dtbl = new DataTable();
            DalSaleReturn dalSR = new DalSaleReturn();
            dtbl = dalSR.SelectSaleReturnAllForPrint(this);
            dalSR = null;
            return dtbl;
        }

        public DataTable DirectSaleReturnItems()
        {
            DataTable dtbl = new DataTable();
            DalSaleReturn dalSR = new DalSaleReturn();
            dtbl = dalSR.DirectSaleReturnItems(this);

            dalSR = null;
            return dtbl;
        }
        public String SRHtmlLayout()
        {
            DalSaleReturn dalSR = new DalSaleReturn();
            String str = dalSR.SalesReturnPrint(this);
            dalSR = null;
            return str;
        }
        public DataSet GetSalesReturnDataSource(long StatusId)
        {
            DalSaleReturn dalSR = new DalSaleReturn();
            DataSet ds = new DataSet();
            try
            {
                ds = dalSR.GetSalesReturnDataSource(StatusId);
                ds.DataSetName = _DatasetName;
                ds.Tables[0].TableName = ReportTable;
                ds.WriteXmlSchema(_ReportSchemaPath + _ReportSchema + ".xsd");
            }
            catch (Exception ex)
            {
            }
            finally
            {
                dalSR = null;
            }
            return ds;
        }

        #endregion
    }


    public class BlSaleReturnDetails
    {


        private long _FK_Items;
        private long _FK_Stock;
        private decimal _MRP;
        private decimal _SalesPrice;
        private decimal _ActualSalesPrice;
        private decimal _Quantity;
        private decimal _VAT;
        private Int32 _GroupId;
        private long _FK_Reason;
        private Int32 _SlNo;
        private decimal _CESS;
        private string _BatchNo;
        private DateTime _ExpiryDate;

        public DateTime ExpiryDate
        {
            get { return _ExpiryDate; }
            set { _ExpiryDate = value; }
        }
        public string BatchNo
        {
            get { return _BatchNo; }
            set { _BatchNo = value; }
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


        public decimal Quantity
        {
            get { return _Quantity; }
            set { _Quantity = value; }
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
        public decimal ActualSalesPrice
        {
            get { return _ActualSalesPrice; }
            set { _ActualSalesPrice = value; }
        }


        public long FK_Reason
        {
            get { return _FK_Reason; }
            set { _FK_Reason = value; }
        }
        public decimal VAT
        {
            get { return _VAT; }
            set { _VAT = value; }
        }
        public decimal CESS
        {
            get { return _CESS; }
            set { _CESS = value; }
        }
        public Int32 GroupId
        {
            get { return _GroupId; }
            set { _GroupId = value; }
        }
    }

}
