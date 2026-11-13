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
using System.Data;

namespace WebPharma.Business.WebPharma
{
  
    public class BlltemConversion : IItemConversion
	{

        private byte _UserAction;
        private Int16 _PageIndex;
        private Int16 _PageSize;
        private long _FK_Client;

        
        private long _ID_ItemConversion;
        private Int64 _FK_StoreFrom;
        private Int64 _FK_StoreTo;
        private DateTime _TransDate;
        private decimal _FromAmount;
        private decimal _ToAmount;
        private string _Remarks;
        private string _UserCode;
        private DateTime _EnteredOn;
        private Boolean _Cancelled;
        private string _CancelledBy;
        private DateTime _CancelledOn;
        private Boolean _Approved;
        private string _ApprovedBy;
        private DateTime _ApprovedOn;
        private string _XmlIndendFrom;
        private string _XmlIndendTo;
        private DateTime _FromDate;

        
        private DateTime _ToDate;

        private string _IndentMode;

        private string _DatasetName;
        private string _ReportSchema;
        private string _ReportTable;
        private string _ReportSchemaPath;
        private string _ReportPath;
        private string _ReportFile;
        private Boolean _Production;
      
		public BlltemConversion()
		{
			Initialize();
		}
		public void Initialize()
		{
        _FK_StoreFrom=0;
        _FK_StoreTo=0;
        _TransDate=DateTime.Now;
        _FromAmount=0;
        _ToAmount=0;
        _Remarks=string.Empty;
        _UserCode=string.Empty;
        _EnteredOn=DateTime.Now;
        _Cancelled=false;
         _CancelledBy=string.Empty;
        _CancelledOn=DateTime.Now;
        _Approved=false;
        _ApprovedBy=string.Empty;
        _ApprovedOn=DateTime.Now;       
        _FK_Client = 1;

        _XmlIndendFrom = string.Empty;
        _XmlIndendTo = string.Empty;
        _FromDate = DateTime.Now;
        _ToDate = DateTime.Now;

        PageIndex = 1;
        PageSize = 10;

        _ReportPath = string.Empty;
        _ReportSchema = string.Empty;
        _ReportSchemaPath = string.Empty;
        _ReportTable = string.Empty;
        _ReportFile = string.Empty;
        _DatasetName = string.Empty;
        _Production = false;

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
        public string DatasetName
        {
            get { return _DatasetName; }
            set { _DatasetName = value; }
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
        public long MasterId
        {
            get { return _ID_ItemConversion; }
            set { _ID_ItemConversion = value; }
        }

        public Int64 FK_StoreFrom
        {
            get { return _FK_StoreFrom; }
            set { _FK_StoreFrom = value; }
        }
        public Int64 FK_StoreTo
        {
            get { return _FK_StoreTo; }
            set { _FK_StoreTo = value; }
        }

        public DateTime TransDate
        {
            get { return _TransDate; }
            set { _TransDate = value; }
        }


        public decimal FromAmount
        {
            get { return _FromAmount; }
            set { _FromAmount = value; }
        }


        public decimal ToAmount
        {
            get { return _ToAmount; }
            set { _ToAmount = value; }
        }

        public string Remarks
        {
            get { return _Remarks; }
            set { _Remarks = value; }
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


        public Boolean Cancelled
        {
            get { return _Cancelled; }
            set { _Cancelled = value; }
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


        public Boolean Approved
        {
            get { return _Approved; }
            set { _Approved = value; }
        }


        public string ApprovedBy
        {
            get { return _ApprovedBy; }
            set { _ApprovedBy = value; }
        }


        public DateTime ApprovedOn
        {
            get { return _ApprovedOn; }
            set { _ApprovedOn = value; }
        }

        public string XmlIndendFrom
        {
            get { return _XmlIndendFrom; }
            set { _XmlIndendFrom = value; }
        }

        public string XmlIndendTo
        {
            get { return _XmlIndendTo; }
            set { _XmlIndendTo = value; }
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
        public string IndentMode
        {
            get { return _IndentMode; }
            set { _IndentMode = value; }
        }
        public Boolean Production
        {
            get { return _Production; }
            set { _Production = value; }
        }


        public long InsertData()
        {
            long status = 0;
            DalItemConversion dalIC = new DalItemConversion();
            try
            {
                UserAction = 1;
                status = dalIC.UpdateItemConversion(this);
                dalIC = null;
            }
            catch (Exception ex)
            {
            }
            return status;
        }

        public long UpdateData()
        {
            long status = 0;
            DalItemConversion dalIC = new DalItemConversion();
            try
            {
                UserAction = 2;
                status = dalIC.UpdateItemConversion(this);
                dalIC = null;
            }
            catch (Exception ex)
            {
            }
            return status;

        }

        public long DeleteltemConversion()
        {
            long status = 0;
            DalItemConversion dalIC = new DalItemConversion();
            try
            {

                UserAction = 3;
                status = dalIC.UpdateItemConversion(this);
                dalIC = null;
            }
            catch (Exception ex)
            {
            }
            return status;


        }
        public long IndentApprove()
        {
            long status = 0;
            DalItemConversion dalIC = new DalItemConversion();
            try
            {

                UserAction = 3;
                status = dalIC.IndentApprove(this);
                dalIC = null;
            }
            catch (Exception ex)
            {
            }
            return status;


        }
        
        public void SelectData()
        {
            DalItemConversion dalIC = new DalItemConversion();
            IItemConversion iItemConversion;
            iItemConversion = (IItemConversion)dalIC.SelectItemConversion(this);
            _FK_StoreFrom = iItemConversion.FK_StoreFrom;
            _FK_StoreTo = iItemConversion.FK_StoreTo;
            _TransDate = iItemConversion.TransDate;
            _FromAmount = iItemConversion.FromAmount;
            _ToAmount = iItemConversion.ToAmount; ;
            _Remarks = iItemConversion.Remarks;
            _Approved = iItemConversion.Approved;
            dalIC = null;

        }


        public DataSet GetPurchaseDataSource(long StatusId)
        {
            DalItemConversion dalIC = new DalItemConversion();
            DataSet ds = new DataSet();
            try
            {
                ds = dalIC.GetPurchaseReportData(StatusId);
                ds.DataSetName = _DatasetName;
                ds.Tables[0].TableName = ReportTable;
                ds.WriteXmlSchema(_ReportSchemaPath + _ReportSchema + ".xsd");
            }
            catch (Exception ex)
            {
            }
            finally
            {
                dalIC = null;
            }
            return ds;
        }

        public DataSet GetStockTransferRequestDataSource(long StatusId)
        {
            DalItemConversion dalIC = new DalItemConversion();
            DataSet ds = new DataSet();
            try
            {
                ds = dalIC.GetStockTransferRequestReportData(StatusId);
                ds.DataSetName = _DatasetName;
                ds.Tables[0].TableName = ReportTable;
                ds.WriteXmlSchema(_ReportSchemaPath + _ReportSchema + ".xsd");
            }
            catch (Exception ex)
            {
            }
            finally
            {
                dalIC = null;
            }
            return ds;
        }

        public DataTable SelectAllData()
        {
            DataTable dtbl = new DataTable();
            DalItemConversion dalIC = new DalItemConversion();
            dtbl = dalIC.SelectItemConversionAll(this);
            dalIC = null;
            return dtbl;
        }
        public DataTable SelectItemConversionItems()
        {
            DataTable dtbl = new DataTable();
            DalItemConversion dalIC = new DalItemConversion();
            dtbl = dalIC.SelectItemConversionItems(this);

            dalIC = null;
            return dtbl;
        }
       

}
    public class BlItemConversionDetails
    {
    
        private Int32 _SlNo;
        private long _FK_Stock;
        private long _FK_Items;
        private long _FK_Store;
        private long _FK_RackArrangement;
        private string _BatchNo;
        private DateTime _ExpiryDate;
        private decimal _PurchaseRate;
        private decimal _MRP;
        private decimal _RetailPrice;
        private decimal _WholesalePrice;
        private decimal _PrivilagePrice;
        private decimal _VAT;
        private decimal _Quantity;
        private string _BarCode;
        private Int32 _SlNoFrom;
        private decimal _Productcost;
        public Int32 SlNoFrom
        {
            get { return _SlNoFrom; }
            set { _SlNoFrom = value; }
        }
      
        public Int32 SlNo
        {
            get { return _SlNo; }
            set { _SlNo = value; }
        }
       
        public long FK_Stock
        {
            get { return _FK_Stock; }
            set { _FK_Stock = value; }
        }
       
        public long FK_Items
        {
            get { return _FK_Items; }
            set { _FK_Items = value; }
        }
       
        public long FK_Store
        {
            get { return _FK_Store; }
            set { _FK_Store = value; }
        }

      
        public long FK_RackArrangement
        {
            get { return _FK_RackArrangement; }
            set { _FK_RackArrangement = value; }
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
       
        public decimal PurchaseRate
        {
            get { return _PurchaseRate; }
            set { _PurchaseRate = value; }
        }
      
        public decimal MRP
        {
            get { return _MRP; }
            set { _MRP = value; }
        }


       
        public decimal RetailPrice
        {
            get { return _RetailPrice; }
            set { _RetailPrice = value; }
        }
        
        public decimal WholesalePrice
        {
            get { return _WholesalePrice; }
            set { _WholesalePrice = value; }
        }

    
        public decimal PrivilagePrice
        {
            get { return _PrivilagePrice; }
            set { _PrivilagePrice = value; }
        }
        
        public decimal VAT
        {
            get { return _VAT; }
            set { _VAT = value; }
        }

     
        public decimal Quantity
        {
            get { return _Quantity; }
            set { _Quantity = value; }
        }
       
        public string BarCode
        {
            get { return _BarCode; }
            set { _BarCode = value; }
        }


        public decimal Productcost
        {
            get { return _Productcost; }
            set { _Productcost = value; }
        }
       

    }
}



