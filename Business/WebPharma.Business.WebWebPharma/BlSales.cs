/*----------------------------------- Business Layer Class -----------------------------------------------------------------------------------------------------------------------
Purpose    : 
Author     : Aiswarya
Copyright  : Perfect Software Solutions (Clt) Pvt Ltd
Created on : 15/11/2021
----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
MODIFICATIONS 
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
using System.Data;

namespace WebPharma.Business.WebPharma
{
    public class BlSales : ISales
    {
        public BlSales()
        {
            Initialize();
        }
        public void Initialize()
        {
            _ID_Sales = 0;
            _UserAction = 1;
            _FK_Client = 1;
            _PageIndex = 1;
            _PageSize = 10;
            _EnteredOn = DateTime.Now;
            _UserCode = string.Empty;
            _Balance = 0;
            _FK_Store = 0;
            _FK_BillType = 0;
            _PriceType = 0;
            _BillNo = 0;
            _BillDate = DateTime.Now;
            _CustomerType = 0;
            _FK_Customer = 0;
            _CustomerName = string.Empty;
            _CardNumber = string.Empty;
            _BillTotal = 0;
            _Discount = 0;
            _DiscountTest = 0;
            _RountOff = 0;
            _FK_Hold = 0;
            _NetAmount = 0;
            _PayAmount = 0;
            _FK_Counter = 0;
            _Fk_Shift = 0;
            _Credit = false;
            _Remarks = string.Empty;
            _StockUpdation = 0;
            _Invoice = 0;
            _CashAmount = 0;
            _CrdAmount = 0;
            _ChqAmount = 0;
            _CrdNumber = string.Empty;
            _CrdSlipNo = string.Empty;
            _CrdBank = string.Empty;
            _CrdRemarks = string.Empty;
            _ChqNumber = string.Empty;
            _ChqAccount = string.Empty;
            _ChqDate = DateTime.Now;
            _ChqBank = string.Empty;
            _ChqRemarks = string.Empty;
            _OpeningCounterClose = 0;
            _SalesAmountCounterClose = 0;
            _SalesRetAmountCounterClose = 0;
            _PayInAmountCounterClose = 0;
            _PayOutAmountCounterClose = 0;
            _ClosingAmountCounterClose = 0;
            _CloseCounterRemittance = 0;
            _PriLayout = string.Empty;
            _BillNoSearch = string.Empty;
            _AmountSearch = string.Empty;
            _NegativeStock = 0;
            _FK_RecoveryType = 0;
            _InstallmentPeriod = 0;
            _InstallmentDuration = 0;
            _InstallmentAmount = 0;
            _RecoveryDate = DateTime.Now;
            _SubsAmount = 0;
            _SubsNumber = string.Empty;
            _SubsPercentage = 0;
            _SubsDate = DateTime.Now;
            _SubsAuthority = string.Empty;
            _SubsRemarks = string.Empty;
            _InterState = false;
            _RedeemAmount = 0;
            _RedeemDiscount = 0;
            _RedeemPoint = 0;
            _Status = string.Empty;
            _TableName = string.Empty;
            _ListFields = string.Empty;
            _ValueFields = string.Empty;
            _SortFields = string.Empty;
            _Criteria = string.Empty;
            _UpdateField = string.Empty;
            _UpdateValue = string.Empty;
            _BillAmount = 0;
            _FromDate = DateTime.Now;
            _ToDate = DateTime.Now;
            _GSTIN = string.Empty;
            _TransID = 0;
            _ReturnId = 0;
            _ReturnName = string.Empty;
            _IsExclude = 0;
            _SpecialDiscount = 0;
            _XmlOtherDetails = string.Empty;
            _ID_Items = 0;
            _FK_Vehicle = 0;
            _Address1 = string.Empty;
            _Address2 = string.Empty;
            _LicennseNo = string.Empty;
            _DriverName = string.Empty;
            _VehicleNo = string.Empty;
            _RBForm = string.Empty;
            _DrcDet = false;
            _FK_BuyState = 0;
            _BuyGSTIN = string.Empty;
            _BuyPAN = string.Empty;
            _ExcludeTax = false;
        }
        public Boolean _ExcludeTax;
        public Boolean ExcludeTax
        {
            get { return _ExcludeTax; }
            set { _ExcludeTax = value; }
        }
        public string _BuyPAN;
        public string BuyPAN
        {
            get { return _BuyPAN; }
            set { _BuyPAN = value; }
        }
        public string _BuyGSTIN;
        public string BuyGSTIN
        {
            get { return _BuyGSTIN; }
            set { _BuyGSTIN = value; }
        }
        public long _FK_BuyState;
        public long FK_BuyState
        {
            get { return _FK_BuyState; }
            set { _FK_BuyState = value; }
        }
        public long _FK_State;
        public long FK_State
        {
            get { return _FK_State; }
            set { _FK_State = value; }
        }
        public Boolean _DrcDet;
        public Boolean DrcDet
        {
            get { return _DrcDet; }
            set { _DrcDet = value; }
        }
        public string _RBForm;
        public string RBForm
        {
            get { return _RBForm; }
            set { _RBForm = value; }
        }
        public string _VehicleNo;
        public string VehicleNo
        {
            get { return _VehicleNo; }
            set { _VehicleNo = value; }
        }
        public string _DriverName;
        public string DriverName
        {
            get { return _DriverName; }
            set { _DriverName = value; }
        }
        private string _LicennseNo;
        public string LicennseNo
        {
            get { return _LicennseNo; }
            set { _LicennseNo = value; }
        }
        private string _Address2;
        public string Address2
        {
            get { return _Address2; }
            set { _Address2 = value; }
        }
        private string _Address1;
        public string Address1
        {
            get { return _Address1; }
            set { _Address1 = value; }
        }
        public long _FK_Vehicle;
        public long FK_Vehicle
        {
            get { return _FK_Vehicle; }
            set { _FK_Vehicle = value; }
        }
        private string _Mode;
        public string Mode
        {
            get { return _Mode; }
            set { _Mode = value; }
        }
        private byte _IsExclude;
        public byte IsExclude
        {
            get { return _IsExclude; }
            set { _IsExclude = value; }
        }
        private byte _SpecialDiscount;
        public byte SpecialDiscount
        {
            get { return _SpecialDiscount; }
            set { _SpecialDiscount = value; }
        }
   

        private Int64 _ReturnId;
        private string _ReturnName;
        private string _TableName;
        private string _ListFields;
        private string _ValueFields;
        private string _SortFields;
        private string _Criteria;
        private string _UpdateField;
        private string _UpdateValue;
        private decimal _BillAmount;
        private string _GSTIN;
        private Int64 _TransID;
        private string _XmlOtherDetails;
        private Int64 _ID_Items;
        public Int64 ID_Items
        {
            get { return _ID_Items; }
            set { _ID_Items = value; }
        }
        public string XmlOtherDetails
        {
            get { return _XmlOtherDetails; }
            set { _XmlOtherDetails = value; }
        }
        public Int64 ReturnId
        {
            get { return _ReturnId; }
            set { _ReturnId = value; }
        }
        public string ReturnName
        {
            get { return _ReturnName; }
            set { _ReturnName = value; }
        }
        public Int64 TransID
        {
            get { return _TransID; }
            set { _TransID = value; }
        }
        private DateTime _FromDate;
        private DateTime _ToDate;
        public string GSTIN
        {
            get { return _GSTIN; }
            set { _GSTIN = value; }
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


        public decimal BillAmount
        {
            get { return _BillAmount; }
            set { _BillAmount = value; }
        }

        public string TableName
        {
            get { return _TableName; }
            set { _TableName = value; }
        }


        public string ListFields
        {
            get { return _ListFields; }
            set { _ListFields = value; }
        }


        public string ValueFields
        {
            get { return _ValueFields; }
            set { _ValueFields = value; }
        }


        public string SortFields
        {
            get { return _SortFields; }
            set { _SortFields = value; }
        }


        public string Criteria
        {
            get { return _Criteria; }
            set { _Criteria = value; }
        }

        public string UpdateField
        {
            get { return _UpdateField; }
            set { _UpdateField = value; }
        }
        public string UpdateValue
        {
            get { return _UpdateValue; }
            set { _UpdateValue = value; }
        }



        private DateTime _SubsDate;
        public DateTime SubsDate
        {
            get { return _SubsDate; }
            set { _SubsDate = value; }
        }

        private string _SubsRemarks;
        public string SubsRemarks
        {
            get { return _SubsRemarks; }
            set { _SubsRemarks = value; }
        }
        private string _Status;
        public string Status
        {
            get { return _Status; }
            set { _Status = value; }
        }

        private string _SubsAuthority;
        public string SubsAuthority
        {
            get { return _SubsAuthority; }
            set { _SubsAuthority = value; }
        }

        private string _SubsNumber;
        public string SubsNumber
        {
            get { return _SubsNumber; }
            set { _SubsNumber = value; }
        }

        private decimal _SubsAmount;
        public decimal SubsAmount
        {
            get { return _SubsAmount; }
            set { _SubsAmount = value; }
        }

        private decimal _SubsPercentage;
        public decimal SubsPercentage
        {
            get { return _SubsPercentage; }
            set { _SubsPercentage = value; }
        }

        private Int64 _RedeemPoint;
        public Int64 RedeemPoint
        {
            get { return _RedeemPoint; }
            set { _RedeemPoint = value; }
        }

        private decimal _RedeemAmount;
        public decimal RedeemAmount
        {
            get { return _RedeemAmount; }
            set { _RedeemAmount = value; }
        }

        private decimal _RedeemDiscount;
        public decimal RedeemDiscount
        {
            get { return _RedeemDiscount; }
            set { _RedeemDiscount = value; }
        }

        private long _FK_RecoveryType;
        public long FK_RecoveryType
        {
            get { return _FK_RecoveryType; }
            set { _FK_RecoveryType = value; }
        }

        private Int32 _InstallmentPeriod;
        public Int32 InstallmentPeriod
        {
            get { return _InstallmentPeriod; }
            set { _InstallmentPeriod = value; }
        }

        private Int32 _InstallmentDuration;
        public Int32 InstallmentDuration
        {
            get { return _InstallmentDuration; }
            set { _InstallmentDuration = value; }
        }

        private decimal _InstallmentAmount;
        public decimal InstallmentAmount
        {
            get { return _InstallmentAmount; }
            set { _InstallmentAmount = value; }
        }

        private DateTime _RecoveryDate;
        public DateTime RecoveryDate
        {
            get { return _RecoveryDate; }
            set { _RecoveryDate = value; }
        }

        private string _AmountSearch;
        public string AmountSearch
        {
            get { return _AmountSearch; }
            set { _AmountSearch = value; }
        }

        private string _BillNoSearch;
        public string BillNoSearch
        {
            get { return _BillNoSearch; }
            set { _BillNoSearch = value; }
        }

        private string _PriLayout;
        public string PriLayout
        {
            get { return _PriLayout; }
            set { _PriLayout = value; }
        }

        private decimal _CloseCounterRemittance;
        public decimal CloseCounterRemittance
        {
            get { return _CloseCounterRemittance; }
            set { _CloseCounterRemittance = value; }
        }

        private decimal _PayOutAmountCounterClose;
        public decimal PayOutAmountCounterClose
        {
            get { return _PayOutAmountCounterClose; }
            set { _PayOutAmountCounterClose = value; }
        }

        private decimal _ClosingAmountCounterClose;
        public decimal ClosingAmountCounterClose
        {
            get { return _ClosingAmountCounterClose; }
            set { _ClosingAmountCounterClose = value; }
        }

        private decimal _PayInAmountCounterClose;
        public decimal PayInAmountCounterClose
        {
            get { return _PayInAmountCounterClose; }
            set { _PayInAmountCounterClose = value; }
        }

        private decimal _SalesAmountCounterClose;
        public decimal SalesAmountCounterClose
        {
            get { return _SalesAmountCounterClose; }
            set { _SalesAmountCounterClose = value; }
        }

        private decimal _SalesRetAmountCounterClose;
        public decimal SalesRetAmountCounterClose
        {
            get { return _SalesRetAmountCounterClose; }
            set { _SalesRetAmountCounterClose = value; }
        }

        private decimal _OpeningCounterClose;
        public decimal OpeningCounterClose
        {
            get { return _OpeningCounterClose; }
            set { _OpeningCounterClose = value; }
        }

        private Int16 _PriceType;
        public Int16 PriceType
        {
            get { return _PriceType; }
            set { _PriceType = value; }
        }

        private long _ID_Sales;
        public long MasterId
        {
            get { return _ID_Sales; }
            set { _ID_Sales = value; }
        }

        private long _FK_Sales;
        public long FK_Sales
        {
            get { return _FK_Sales; }
            set { _FK_Sales = value; }
        }

        private long _FK_Store;
        public long FK_Store
        {
            get { return _FK_Store; }
            set { _FK_Store = value; }
        }

        private long _FK_Hold;
        public long FK_Hold
        {
            get { return _FK_Hold; }
            set { _FK_Hold = value; }
        }

        private long _FK_BillType;
        public long FK_BillType
        {
            get { return _FK_BillType; }
            set { _FK_BillType = value; }
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
        }

        private byte _CustomerType;
        public byte CustomerType
        {
            get { return _CustomerType; }
            set { _CustomerType = value; }
        }

        private long _FK_Customer;
        public long FK_Customer
        {
            get { return _FK_Customer; }
            set { _FK_Customer = value; }
        }

        private string _CustomerName;
        public string CustomerName
        {
            get { return _CustomerName; }
            set { _CustomerName = value; }
        }

        private string _CardNumber;
        public string CardNumber
        {
            get { return _CardNumber; }
            set { _CardNumber = value; }
        }

        private decimal _BillTotal;
        public decimal BillTotal
        {
            get { return _BillTotal; }
            set { _BillTotal = value; }
        }

        private decimal _Discount;
        public decimal Discount
        {
            get { return _Discount; }
            set { _Discount = value; }
        }

        private decimal _DiscountTest;
        public decimal DiscountTest
        {
            get { return _DiscountTest; }
            set { _DiscountTest = value; }
        }

        private decimal _Balance;
        public decimal Balance
        {
            get { return _Balance; }
            set { _Balance = value; }
        }

        private decimal _RountOff;
        public decimal RountOff
        {
            get { return _RountOff; }
            set { _RountOff = value; }
        }

        private decimal _NetAmount;
        public decimal NetAmount
        {
            get { return _NetAmount; }
            set { _NetAmount = value; }
        }

        private decimal _PayAmount;
        public decimal PayAmount
        {
            get { return _PayAmount; }
            set { _PayAmount = value; }
        }

        private long _FK_Counter;
        public long FK_Counter
        {
            get { return _FK_Counter; }
            set { _FK_Counter = value; }
        }

        private long _Fk_Shift;
        public long Fk_Shift
        {
            get { return _Fk_Shift; }
            set { _Fk_Shift = value; }
        }

        private string _Remarks;
        public string Remarks
        {
            get { return _Remarks; }
            set { _Remarks = value; }
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

        private decimal _CashAmount;
        public decimal CashAmount
        {
            get { return _CashAmount; }
            set { _CashAmount = value; }
        }

        private decimal _CrdAmount;
        public decimal CrdAmount
        {
            get { return _CrdAmount; }
            set { _CrdAmount = value; }
        }

        private decimal _ChqAmount;
        public decimal ChqAmount
        {
            get { return _ChqAmount; }
            set { _ChqAmount = value; }
        }

        private string _CrdNumber;
        public string CrdNumber
        {
            get { return _CrdNumber; }
            set { _CrdNumber = value; }
        }

        private string _CrdSlipNo;
        public string CrdSlipNo
        {
            get { return _CrdSlipNo; }
            set { _CrdSlipNo = value; }
        }

        private string _CrdBank;
        public string CrdBank
        {
            get { return _CrdBank; }
            set { _CrdBank = value; }
        }

        private string _CrdRemarks;
        public string CrdRemarks
        {
            get { return _CrdRemarks; }
            set { _CrdRemarks = value; }
        }

        private string _ChqNumber;
        public string ChqNumber
        {
            get { return _ChqNumber; }
            set { _ChqNumber = value; }
        }

        private string _ChqAccount;
        public string ChqAccount
        {
            get { return _ChqAccount; }
            set { _ChqAccount = value; }
        }

        private DateTime _ChqDate;
        public DateTime ChqDate
        {
            get { return _ChqDate; }
            set { _ChqDate = value; }
        }

        private string _ChqBank;
        public string ChqBank
        {
            get { return _ChqBank; }
            set { _ChqBank = value; }
        }

        private string _ChqRemarks;
        public string ChqRemarks
        {
            get { return _ChqRemarks; }
            set { _ChqRemarks = value; }
        }

        private byte _UserAction;
        public byte UserAction
        {
            get { return _UserAction; }
            set { _UserAction = value; }
        }

        private long _FK_Client;
        public long FK_Client
        {
            get { return _FK_Client; }
            set { _FK_Client = value; }
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

        private string _XmlGiftDetails;
        public string XmlGiftDetails
        {
            get { return _XmlGiftDetails; }
            set { _XmlGiftDetails = value; }
        }

        private string _XmlCloseCounterDetails;
        public string XmlCloseCounterDetails
        {
            get { return _XmlCloseCounterDetails; }
            set { _XmlCloseCounterDetails = value; }
        }

        private string _XmlDiscDetails;
        public string XmlDiscDetails
        {
            get { return _XmlDiscDetails; }
            set { _XmlDiscDetails = value; }
        }

        private string _XmlSalesDetails;
        public string XmlSalesDetails
        {
            get { return _XmlSalesDetails; }
            set { _XmlSalesDetails = value; }
        }

        private Boolean _Credit;
        public Boolean Credit
        {
            get { return _Credit; }
            set { _Credit = value; }
        }

        private Boolean _InterState;
        public Boolean InterState
        {
            get { return _InterState; }
            set { _InterState = value; }
        }

        private Byte _StockUpdation;
        public Byte StockUpdation
        {
            get { return _StockUpdation; }
            set { _StockUpdation = value; }
        }

        private Byte _Invoice;
        public Byte Invoice
        {
            get { return _Invoice; }
            set { _Invoice = value; }
        }

        private Int16 _NegativeStock;
        public Int16 NegativeStock
        {
            get { return _NegativeStock; }
            set { _NegativeStock = value; }
        }

        //public long InsertData()
        //{
        //    long status;
        //    DalSales dalSales = new DalSales();
        //    status = 0;
        //    try
        //    {
        //        _UserAction = 1;
        //        status = dalSales.UpdateSales(this);
        //        dalSales = null;
        //    }
        //    catch (Exception ex)
        //    {

        //    }
        //    return status;
        //}
        public void InsertData()
        {

            DalSales dalSales = new DalSales();
            ISales iSales;
            iSales = (ISales)dalSales.UpdateSales(this);
            _ReturnId = iSales.ReturnId;
            _ReturnName = iSales.ReturnName;
        }
        public DataTable SelectAllData()
        {
            DataTable dtbl = new DataTable();
            DalSales dalSales = new DalSales();
            dtbl = dalSales.SelectSalesAll(this);
            dalSales = null;
            return dtbl;
        }
       

        public long HoldData()
        {
            long status;
            DalSales dalSales = new DalSales();
            status = 0;
            try
            {
                _UserAction = 1;
                status = dalSales.UpdateHoldSales(this);
                dalSales = null;
            }
            catch (Exception ex)
            {

            }
            return status;
        }
        
        public long CounterCloseUpdatePOS()
        {
            long status;
            DalSales dalSales = new DalSales();
            status = 0;
            try
            {
                _UserAction = 1;
                status = dalSales.CounterCloseUpdatePOS(this);
                dalSales = null;
            }
            catch (Exception ex)
            {

            }
            return status;
        }

        public long CounterOpenUpdatePOS()
        {
            long status;
            DalSales dalSales = new DalSales();
            status = 0;
            try
            {
                _UserAction = 1;
                status = dalSales.CounterOpenUpdatePOS(this);
                dalSales = null;
            }
            catch (Exception ex)
            {

            }
            return status;
        }
        public long CounterCloseDeletePOS()
        {
            long status;
            DalSales dalSales = new DalSales();
            status = 0;
            try
            {
                _UserAction = 3;
                status = dalSales.CounterCloseDeletePOS(this);
                dalSales = null;
            }
            catch (Exception ex)
            {

            }
            return status;
        }
        public DataTable SelectBillTypeWhenCredit()
        {
            DataTable dtbl = new DataTable();
            DalSales dalSales = new DalSales();
            dtbl = dalSales.SelectBillTypeWhenCredit(this);
            dalSales = null;
            return dtbl;
        }
       
        public DataTable SelectRecoveryType()
        {
            DataTable dtbl = new DataTable();
            DalSales dalSales = new DalSales();
            dtbl = dalSales.SelectRecoveryType(this);
            dalSales = null;
            return dtbl;
        }

        
        public long HoldDataDelete()
        {
            long status;
            DalSales dalSales = new DalSales();
            status = 0;
            try
            {
                _UserAction = 3;
                status = dalSales.UpdateHoldSales(this);
                dalSales = null;
            }
            catch (Exception ex)
            {

            }
            return status;
        }
        public long UpdateHoldData()
        {
            long status;
            DalSales dalSales = new DalSales();
            status = 0;
            try
            {
                _UserAction = 2;
                status = dalSales.UpdateHoldSales(this);
                dalSales = null;
            }
            catch (Exception ex)
            {

            }
            return status;
        }

       
       
       

        public DataTable SelectCounterCloseCheckingPOS()
        {
            DalSales dalSales = new DalSales();
            DataTable dtbl = new DataTable();
            dtbl = dalSales.SelectCounterCloseCheckingPOS(this);
            dalSales = null;
            return dtbl;
        }

      
       
        public long PayInOutDelete()
        {
            long status;
            DalSales dalSales = new DalSales();
            status = 0;
            try
            {
                _UserAction = 1;
                status = dalSales.PayInOutDelete(this);
                dalSales = null;
            }
            catch (Exception ex)
            {

            }
            return status;
        }
        public long CounterOpenClose()
        {
            long status;
            DalSales dalSales = new DalSales();
            status = 0;
            try
            {
                status = dalSales.CounterOpenClose(this);
                dalSales = null;
            }
            catch (Exception ex)
            {

            }
            return status;
        }
        public DataTable SelectOpeningPayInOutPOS()
        {
            DataTable dtbl = new DataTable();
            DalSales dalsale = new DalSales();
            dtbl = dalsale.SelectOpeningPayInOutPOS(this);
            dalsale = null;
            return dtbl;
        }

        public DataTable SelectOpeningCounter()
        {
            DataTable dtbl = new DataTable();
            DalSales dalsale = new DalSales();
            dtbl = dalsale.SelectOpeningCounter(this);
            dalsale = null;
            return dtbl;
        }
        public DataTable SelectAllDenominations()
        {
            DataTable dtbl = new DataTable();
            DalSales dalsale = new DalSales();
            dtbl = dalsale.SelectAllDenominations(this);
            dalsale = null;
            return dtbl;
        }
     

        public string GetScalarValues()
        {
            DalSales dalsale = new DalSales();
            try
            {
                return dalsale.GetScalarValue(this);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public int SelectSaleExistForCustomerDiscount()
        {
            int Return = 0;
            DalSales dalsale = new DalSales();
            Return = dalsale.SelectSaleExistForCustomerDiscount(this);
            dalsale = null;
            return Return;
        }


        public void SelectData()
        {
            DalSales dalS = new DalSales();
            ISales iSale;
            iSale = (ISales)dalS.SelectSales(this);

            _BillNo = iSale.BillNo;
            _BillDate = iSale.BillDate;
            _FK_Customer = iSale.FK_Customer;
            _FK_Store = iSale.FK_Store;
            _FK_Counter = iSale.FK_Counter;
            _Discount = iSale.Discount;
            _NetAmount = iSale.NetAmount;
            _Remarks = iSale.Remarks;
            _FK_Sales = iSale.FK_Sales;
            _BillTotal = iSale.BillTotal;
            _RountOff = iSale.RountOff;
            _Credit = iSale.Credit;
            _GSTIN = iSale.GSTIN;
            _InterState = iSale.InterState;
            _BuyGSTIN = iSale.BuyGSTIN;
            _BuyPAN = iSale.BuyPAN;
            _FK_BuyState = iSale.FK_BuyState;
            dalS = null;

        }




    }






    public class ISalesDetails
    {
        private Int64 _FK_Items;
        private Int64 _FK_Stock;
        private decimal _MRP;
        private decimal _SalesPrice;
        private decimal _Quantity;
        private decimal _Vat;
        private Int64 _GroupId;
        private decimal _Cess;
        private byte _EditPrice;
        private decimal _FCess;

        public ISalesDetails()
        {
            Initialize();
        }
        public void Initialize()
        {
            _FK_Items = 0;
            _FK_Stock = 0;
            _MRP = 0;
            _SalesPrice = 0;
            _Quantity = 0;
            _Vat = 0;
            _GroupId = 0;
            _Cess = 0;
            _EditPrice = 0;

        }

        public byte EditPrice
        {
            get { return _EditPrice; }
            set { _EditPrice = value; }
        }
        public decimal Cess
        {
            get { return _Cess; }
            set { _Cess = value; }
        }
        public decimal FCess
        {
            get { return _FCess; }
            set { _FCess = value; }
        }
        public Int64 FK_Items
        {
            get { return _FK_Items; }
            set { _FK_Items = value; }
        }
        public Int64 FK_Stock
        {
            get { return _FK_Stock; }
            set { _FK_Stock = value; }
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
        public decimal Quantity
        {
            get { return _Quantity; }
            set { _Quantity = value; }
        }
        public decimal Vat
        {
            get { return _Vat; }
            set { _Vat = value; }
        }
        public Int64 GroupId
        {
            get { return _GroupId; }
            set { _GroupId = value; }
        }

    }



    public class IGiftVoucherDetails
    {
        private Int64 _FK_VoucherGeneration;
        private decimal _Amount;
        private string _VoucherNo;

        public IGiftVoucherDetails()
        {
            Initialize();
        }
        public void Initialize()
        {
            _FK_VoucherGeneration = 0;
            _VoucherNo = "";
            _Amount = 0;
        }
        public Int64 FK_VoucherGeneration
        {
            get { return _FK_VoucherGeneration; }
            set { _FK_VoucherGeneration = value; }
        }

        public decimal Amount
        {
            get { return _Amount; }
            set { _Amount = value; }
        }
        public string VoucherNo
        {
            get { return _VoucherNo; }
            set { _VoucherNo = value; }
        }

    }
    public class IOtherChargeSalesDetails
    {
        private Int64 _SlNo;
        private Int64 _ID_OtherCharges;
        private Int64 _FK_OtherCharge;
        private Int64 _FK_Transaction;
        private decimal _Amount;


        public IOtherChargeSalesDetails()
        {
            Initialize();
        }
        public void Initialize()
        {
            _SlNo = 0;
            _ID_OtherCharges = 0;
            _FK_OtherCharge = 0;
            _FK_Transaction = 0;
            _Amount = 0;
        }
        public long SlNo
        {
            get { return _SlNo; }
            set { _SlNo = value; }
        }
        public long ID_OtherCharges
        {
            get { return _ID_OtherCharges; }
            set { _ID_OtherCharges = value; }
        }
        public long FK_OtherCharge
        {
            get { return _FK_OtherCharge; }
            set { _FK_OtherCharge = value; }
        }
        public long FK_Transaction
        {
            get { return _FK_Transaction; }
            set { _FK_Transaction = value; }
        }
        public decimal Amount
        {
            get { return _Amount; }
            set { _Amount = value; }
        }
    }
    public class ICloseCounterDetails
    {
        private Int64 _FK_Transaction;
        private Int64 _FK_Denominationmaster;
        private Int64 _DenominationCount;


        public ICloseCounterDetails()
        {
            Initialize();
        }
        public void Initialize()
        {
            _FK_Transaction = 0;
            _FK_Denominationmaster = 0;
            _DenominationCount = 0;


        }
        public Int64 FK_Transaction
        {
            get { return _FK_Transaction; }
            set { _FK_Transaction = value; }
        }
        public Int64 FK_Denominationmaster
        {
            get { return _FK_Denominationmaster; }
            set { _FK_Denominationmaster = value; }
        }
        public Int64 DenominationCount
        {
            get { return _DenominationCount; }
            set { _DenominationCount = value; }
        }


    }
}