using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using WebPharma.DataAccess.WebPharma;
using WebPharma.Interface.WebPharma;

namespace WebPharma.Business.WebPharma
{
    public class BlCloudLink : ICloudLink
    {
        private byte _UserAction;
        private Int16 _PageIndex;
        private Int16 _PageSize;
        private long _FK_Client;
        private long _FK_Store;
        private Int16 _Mode;
        private string _UserCode;
        private DateTime _EnteredOn;
        private long _MasterId;
        private string _XmlExportCloudDetails;
        private string _XmlMasterCount;
        //private DataTable _dtAccountHead;
        private DataSet _dsScoreData;
        private Int64 _ReturnId;
        private string _XmlmasterData;
        private string _XmldtAccountGroup;
        private string _XmldtAccountsubGroup;
        private string _XmldtAccountHead;
        private string _XmldtAccountHeadSub;
        private string _XmldtState;
        private string _XmldtDistrict;
        private string _XmldtPlace;
        private string _XmldtPost;
        private string _XmldtIdentityProof;
        private string _XmldtCustomerType;
        private string _XmldtCustomer;
        private string _ReturnMessage;
        private string _XmldtBehaviour;
        private Int64 _IDProcessExport;
        private DateTime _Tdate;
        private Boolean _SupplierWise;
        private Boolean _SalesMan;
        private string _XmldtSupplier;
        private string _XmldtAgent;
        private Boolean _Recovery;
        private string _XmldtRecovery;
        private string _XmlErrorCode;
     
        public BlCloudLink()
        {
            Initialize();
        }
        public void Initialize()
        {
            _UserAction = 0;
            _FK_Store = 0;
            _FK_Client = 1;
            _Mode = 0;
            _UserCode = string.Empty;
            _PageIndex = 1;
            _PageSize = 10;
            _EnteredOn = DateTime.Now;
            _MasterId = 0;
            _ReturnMessage = string.Empty;
            _IDProcessExport = 0;
            _Tdate = DateTime.Now;
            _SupplierWise = false;
            _SalesMan = false;
            _Recovery = false;

        }
        public string XmlErrorCode
        {
            get { return _XmlErrorCode; }
            set { _XmlErrorCode = value; }
        }
        public string XmldtRecovery
        {
            get { return _XmldtRecovery; }
            set { _XmldtRecovery = value; }
        }
        public Boolean Recovery
        {
            get { return _Recovery; }
            set { _Recovery = value; }
        }
        public string XmldtAgent
        {
            get { return _XmldtAgent; }
            set { _XmldtAgent = value; }
        }
        public string XmldtSupplier
        {
            get { return _XmldtSupplier; }
            set { _XmldtSupplier = value; }
        }
        public Boolean SupplierWise
        {
            get { return _SupplierWise; }
            set { _SupplierWise = value; }
        }

        public Boolean SalesMan
        {
            get { return _SalesMan; }
            set { _SalesMan = value; }
        }
        public DateTime Tdate
        {
            get { return _Tdate; }
            set { _Tdate = value; }
        }
        public Int64 IDProcessExport
        {
            get { return _IDProcessExport; }
            set { _IDProcessExport = value; }
        }
        public string ReturnMessage
        {
            get { return _ReturnMessage; }
            set { _ReturnMessage = value; }
        }
        public string XmldtCustomer
        {
            get { return _XmldtCustomer; }
            set { _XmldtCustomer = value; }
        }
        public string XmldtCustomerType
        {
            get { return _XmldtCustomerType; }
            set { _XmldtCustomerType = value; }
        }
        public string XmldtIdentityProof
        {
            get { return _XmldtIdentityProof; }
            set { _XmldtIdentityProof = value; }
        }
        public string XmldtPost
        {
            get { return _XmldtPost; }
            set { _XmldtPost = value; }
        }
        public string XmldtPlace
        {
            get { return _XmldtPlace; }
            set { _XmldtPlace = value; }
        }
        public string XmldtDistrict
        {
            get { return _XmldtDistrict; }
            set { _XmldtDistrict = value; }
        }
        public string XmldtState
        {
            get { return _XmldtState; }
            set { _XmldtState = value; }
        }
        public string XmldtAccountHeadSub
        {
            get { return _XmldtAccountHeadSub; }
            set { _XmldtAccountHeadSub = value; }
        }
        public string XmldtBehaviour
        {
            get { return _XmldtBehaviour; }
            set { _XmldtBehaviour = value; }
        }
        public string XmldtAccountHead
        {
            get { return _XmldtAccountHead; }
            set { _XmldtAccountHead = value; }
        }
        public string XmldtAccountsubGroup
        {
            get { return _XmldtAccountsubGroup; }
            set { _XmldtAccountsubGroup = value; }
        }
        public string XmldtAccountGroup
        {
            get { return _XmldtAccountGroup; }
            set { _XmldtAccountGroup = value; }
        }


        public string XmlmasterData
        {
            get { return _XmlmasterData; }
            set { _XmlmasterData = value; }
        }
        public Int64 ReturnId
        {
            get { return _ReturnId; }
            set { _ReturnId = value; }
        }
        public DataSet dsScoreData
        {
            get { return _dsScoreData; }
            set { _dsScoreData = value; }
        }

        public byte UserAction
        {
            set { _UserAction = value; }
            get { return _UserAction; }
        }
        public long MasterId
        {
            set { _MasterId = value; }
            get { return _MasterId; }
        }
        public Int16 Mode
        {
            set { _Mode = value; }
            get { return _Mode; }
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
        public string UserCode
        {
            get { return _UserCode; }
            set { _UserCode = value; }
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
        public DateTime EnteredOn
        {
            get { return _EnteredOn; }
            set { _EnteredOn = value; }
        }
        public string XmlExportCloudDetails
        {
            get { return _XmlExportCloudDetails; }
            set { _XmlExportCloudDetails = value; }
        }
        public string XmlMasterCount
        {
            get { return _XmlMasterCount; }
            set { _XmlMasterCount = value; }
        }
        public DataSet SelectImportandExportData()
        {
            DataSet dtbl = new DataSet();
            DalCloudLink dalCloudLink = new DalCloudLink();
            dtbl = dalCloudLink.SelectImportandExportData(this);
            dalCloudLink = null;
            return dtbl;
        }
        public DataTable SelectImportDetails()
        {
            DataTable dtbl = new DataTable();
            DalCloudLink dalCloudLink = new DalCloudLink();
            dtbl = dalCloudLink.SelectImportDetails(this);
            dalCloudLink = null;
            return dtbl;
        }
        public DataTable SelectExportDetails()
        {
            DataTable dtbl = new DataTable();
            DalCloudLink dalCloudLink = new DalCloudLink();
            dtbl = dalCloudLink.SelectExportDetails(this);
            dalCloudLink = null;
            return dtbl;
        }
        public DataTable InsertImportDetails()
        {
            DataTable dtbl = new DataTable();
            DalCloudLink dalCloudLink = new DalCloudLink();
            dtbl = dalCloudLink.InsertImportDetails(this);
            dalCloudLink = null;
            return dtbl;
        }
        public DataTable SelectMasterCountDetails()
        {
            DataTable dtbl = new DataTable();
            DalCloudLink dalCloudLink = new DalCloudLink();
            dtbl = dalCloudLink.SelectMasterCountDetails(this);
            dalCloudLink = null;
            return dtbl;
        }
        public DataSet SelectExportDataDetails()
        {
            DataSet dts = new DataSet();
            DalCloudLink dalCloudLink = new DalCloudLink();
            dts = dalCloudLink.SelectExportDataDetail(this);
            dalCloudLink = null;
            return dts;
        }
        //public DataTable InsertMasterDatail()
        //{
        //    DataTable dtbl = new DataTable();
        //    DalCloudLink dalCloudLink = new DalCloudLink();
        //    dtbl = dalCloudLink.InsertImportMasterDataDetail(this);
        //    dalCloudLink = null;
        //    return dtbl;
        //}
        //public void InsertMasterDetail()
        //{

        //    DalCloudLink dalCloudLink = new DalCloudLink();
        //    ICloudLink iCloudLink;
        //    iCloudLink = (ICloudLink)dalCloudLink.InsertImportMasterDataDetail(this);
        //    _ReturnId = iCloudLink.ReturnId;

        //}
        public void InsertMasterAccountGroupDetail()
        {

            DalCloudLink dalCloudLink = new DalCloudLink();
            ICloudLink iCloudLink;
            iCloudLink = (ICloudLink)dalCloudLink.InsertImportMasterAccountGroupDataDetail(this);
            _ReturnId = iCloudLink.ReturnId;
            _ReturnMessage = iCloudLink.ReturnMessage;
        }
        public void InsertMasterAccountsubGroupData()
        {

            DalCloudLink dalCloudLink = new DalCloudLink();
            ICloudLink iCloudLink;
            iCloudLink = (ICloudLink)dalCloudLink.InsertImportMasterAccountsubGroupDataDetail(this);
            _ReturnId = iCloudLink.ReturnId;
            _ReturnMessage = iCloudLink.ReturnMessage;
        }
        public void InsertMasterAccountBehaviourData()
        {

            DalCloudLink dalCloudLink = new DalCloudLink();
            ICloudLink iCloudLink;
            iCloudLink = (ICloudLink)dalCloudLink.InsertMasterAccountBehaviourData(this);
            _ReturnId = iCloudLink.ReturnId;
            _ReturnMessage = iCloudLink.ReturnMessage;
        }
        public void InsertMasterAccountHeadData()
        {

            DalCloudLink dalCloudLink = new DalCloudLink();
            ICloudLink iCloudLink;
            iCloudLink = (ICloudLink)dalCloudLink.InsertImportMasterAccountHeadData(this);
            _ReturnId = iCloudLink.ReturnId;
            _ReturnMessage = iCloudLink.ReturnMessage;
        }
        public void InsertMasterAccountHeadSubData()
        {

            DalCloudLink dalCloudLink = new DalCloudLink();
            ICloudLink iCloudLink;
            iCloudLink = (ICloudLink)dalCloudLink.InsertImportMasterAccountHeadSubData(this);
            _ReturnId = iCloudLink.ReturnId;
            _ReturnMessage = iCloudLink.ReturnMessage;
        }
        public void InsertImportMasterStateData()
        {

            DalCloudLink dalCloudLink = new DalCloudLink();
            ICloudLink iCloudLink;
            iCloudLink = (ICloudLink)dalCloudLink.InsertImportMasterStateData(this);
            _ReturnId = iCloudLink.ReturnId;
            _ReturnMessage = iCloudLink.ReturnMessage;
        }
        public void InsertImportMasterDistrictData()
        {

            DalCloudLink dalCloudLink = new DalCloudLink();
            ICloudLink iCloudLink;
            iCloudLink = (ICloudLink)dalCloudLink.InsertImportMasterDistrictData(this);
            _ReturnId = iCloudLink.ReturnId;
            _ReturnMessage = iCloudLink.ReturnMessage;
        }
        public void InsertImportMasterPlaceData()
        {

            DalCloudLink dalCloudLink = new DalCloudLink();
            ICloudLink iCloudLink;
            iCloudLink = (ICloudLink)dalCloudLink.InsertImportMasterPlaceData(this);
            _ReturnId = iCloudLink.ReturnId;
            _ReturnMessage = iCloudLink.ReturnMessage;
        }
        public void InsertImportMasterPostData()
        {

            DalCloudLink dalCloudLink = new DalCloudLink();
            ICloudLink iCloudLink;
            iCloudLink = (ICloudLink)dalCloudLink.InsertImportMasterPostData(this);
            _ReturnId = iCloudLink.ReturnId;
            _ReturnMessage = iCloudLink.ReturnMessage;
        }
        public void InsertImportMasterIdentityProofData()
        {

            DalCloudLink dalCloudLink = new DalCloudLink();
            ICloudLink iCloudLink;
            iCloudLink = (ICloudLink)dalCloudLink.InsertImportMasterIdentityProofData(this);
            _ReturnId = iCloudLink.ReturnId;
            _ReturnMessage = iCloudLink.ReturnMessage;
        }
        public void InsertImportMasterCustomerTypeData()
        {

            DalCloudLink dalCloudLink = new DalCloudLink();
            ICloudLink iCloudLink;
            iCloudLink = (ICloudLink)dalCloudLink.InsertImportMasterCustomerTypeData(this);
            _ReturnId = iCloudLink.ReturnId;
            _ReturnMessage = iCloudLink.ReturnMessage;
        }
        public void InsertImportMasterCustomerData()
        {

            DalCloudLink dalCloudLink = new DalCloudLink();
            ICloudLink iCloudLink;
            iCloudLink = (ICloudLink)dalCloudLink.InsertImportMasterCustomerData(this);
            _ReturnId = iCloudLink.ReturnId;
            _ReturnMessage = iCloudLink.ReturnMessage;
        }
        public void InsertMasterSupplierData()
        {

            DalCloudLink dalCloudLink = new DalCloudLink();
            ICloudLink iCloudLink;
            iCloudLink = (ICloudLink)dalCloudLink.InsertImportMasterSupplierData(this);
            _ReturnId = iCloudLink.ReturnId;
            _ReturnMessage = iCloudLink.ReturnMessage;
        }
        public void InsertImportMasterAgentData()
        {

            DalCloudLink dalCloudLink = new DalCloudLink();
            ICloudLink iCloudLink;
            iCloudLink = (ICloudLink)dalCloudLink.InsertImportMasterAgentData(this);
            _ReturnId = iCloudLink.ReturnId;
            _ReturnMessage = iCloudLink.ReturnMessage;
        }
        public void InsertImportRecoveryData()
        {

            DalCloudLink dalCloudLink = new DalCloudLink();
            ICloudLink iCloudLink;
            iCloudLink = (ICloudLink)dalCloudLink.InsertImportRecoveryData(this);
            _ReturnId = iCloudLink.ReturnId;
            _ReturnMessage = iCloudLink.ReturnMessage;
        }
    }

    public class ICloudImportDetails
    {
        private string _ID;
        private string _TableName;
        private Int64 _Processid;
        private string _UserCode;
        private Boolean _checkedTable;
        public ICloudImportDetails()
        {
            Initialize();
        }
        public void Initialize()
        {
            _ID = string.Empty;
            _TableName = string.Empty;
            _checkedTable = false;
        }
        public string ID
        {
            set { _ID = value; }
            get { return _ID; }
        }
        public string TableName
        {
            set { _TableName = value; }
            get { return _TableName; }
        }
        public Int64 Processid
        {
            set { _Processid = value; }
            get { return _Processid; }
        }
        public string UserCode
        {
            set { _UserCode = value; }
            get { return _UserCode; }
        }
        public Boolean checkedTable
        {
            set { _checkedTable = value; }
            get { return _checkedTable; }
        }
    }
    public class BlMasterCount
    {
        private string _TableName;
        private long _LastIdUpdate;
        private DateTime? _LastEntedOn;
        private Boolean _Checked;
        public BlMasterCount()
        {
            Initialisation();
        }
        public void Initialisation()
        {
            _LastEntedOn = DateTime.Now;
            _LastIdUpdate = 0;
            _TableName = string.Empty;
            _Checked = false;
        }
        public string TableName
        {
            get { return _TableName; }
            set { _TableName = value; }
        }
        public long LastIdUpdate
        {
            get { return _LastIdUpdate; }
            set { _LastIdUpdate = value; }
        }
        public DateTime? LastEntedOn
        {
            get { return _LastEntedOn; }
            set { _LastEntedOn = value; }
        }
        public Boolean Checked
        {
            get { return _Checked; }
            set { _Checked = value; }
        }

    }
    public class IcloudExportDetails
    {
        private string _ID;
        private string _Module;
        private string _Transmode;
        private DateTime _Transdate;
        private string _Party;
        private string _BillNo;
        private decimal _Amount;
        private Int64 _FK_Transaction;
        private Boolean _Checked;
        public IcloudExportDetails()
        {
            _ID = string.Empty;
            _Module = string.Empty;
            _Transmode = string.Empty;
            _Amount = 0;
            _Transdate = DateTime.Now;
            _Party = string.Empty;
            _BillNo = string.Empty;
            _FK_Transaction = 0;
            _Checked = false;
        }
        public void Initialize()
        {
            _ID = string.Empty;
            _Module = string.Empty;
        }
        public string Module
        {
            set { _Module = value; }
            get { return _Module; }
        }
        public string Transmode
        {
            set { _Transmode = value; }
            get { return _Transmode; }
        }
        public string Party
        {
            set { _Party = value; }
            get { return _Party; }
        }
        public string BillNo
        {
            set { _BillNo = value; }
            get { return _BillNo; }
        }
        public decimal Amount
        {
            set { _Amount = value; }
            get { return _Amount; }
        }
        public DateTime Transdate
        {
            set { _Transdate = value; }
            get { return _Transdate; }
        }
        public Int64 FK_Transaction
        {
            set { _FK_Transaction = value; }
            get { return _FK_Transaction; }
        }
        public Boolean Checked
        {
            set { _Checked = value; }
            get { return _Checked; }
        }
    }
    public class CommonAPIResponse
    {
        public int StatusCode { get; set; }
        public string EXMessage { get; set; }
    }
    public class RootScoreLinkingRQ : CommonAPIResponse
    {
        public DataSet dsdata { get; set; }

    }
    public class RootExportScoreLinkingRQ : CommonAPIResponse
    {
        public DataTable dtExportData { get; set; }

    }
    public class parameter
    {
        public string XMLdata { get; set; }
        public Int32 Mode { get; set; }
        public string url { get; set; }
        public string Key { get; set; }
        public string Product { get; set; }
    }
    public class Exportparameter
    {
        public string dtExpData { get; set; }
        public Int32 Mode { get; set; }
        public string url { get; set; }
        public string Key { get; set; }
        public string dtSupplierData { get; set; }
        public string dtSalesmandata { get; set; }
        public string Product { get; set; }
        public Boolean Recovery { get; set; }
    }
    public class BlErrExportMsg
    {
        private Int64 _EResID;
        private string _EResMsg;

        public BlErrExportMsg()
        {
            Initialisation();
        }
        public void Initialisation()
        {
            _EResID = 0;
            _EResMsg = string.Empty;
        }

        public Int64 EResID
        {
            set { _EResID = value; }
            get { return _EResID; }
        }

        public string EResMsg
        {
            set { _EResMsg = value; }
            get { return _EResMsg; }
        }
    }
}
