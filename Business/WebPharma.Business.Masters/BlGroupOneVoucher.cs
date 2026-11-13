using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebPharma.Interface.General;
using WebPharma.Interface.Masters;
using WebPharma.DataAccess.Masters;
using System.Data;


namespace WebPharma.Business.Masters
{
   public class BlGroupOneVoucher:IGroupOneVoucher
    {
         public  BlGroupOneVoucher()
        {
            Initialize();
        }
        private long _ID_GroupOneVoucher;
        private byte _UserAction;
        private string _GroupOneName;
        private string _GroupOneCode;     
        private int _SortOrder;
        private string _Description;
        private string _UserCode;
        private DateTime _EnteredOn;
        private Boolean _Active;
        private Int64 _FK_Client;
        private Int16 _PageIndex;
        private Int16 _PageSize;
        private Int64 _FK_GroupOneVoucher;
        private string _GroupOneVoucherNo;
        private decimal _GroupOneVoucherAmount;
        private Int64 _FK_AccountHeadSub;

       
       public string Description
        {
            get { return _Description; }
            set { _Description = value; }
        }

        public void Initialize()
        {

            _UserAction = 1;
            _UserCode = string.Empty;
            _EnteredOn = DateTime.Now;
            _FK_Client = 1;
            _PageIndex = 1;
            _PageSize = 10;
            _ID_GroupOneVoucher = 0;
            _GroupOneName = string.Empty;
            _GroupOneCode = string.Empty;           
            _SortOrder = 0;
            _Description = string.Empty;
            _UserCode = string.Empty;
            _Active = true;
            _FK_GroupOneVoucher = 0;
            _GroupOneVoucherNo = string.Empty;
            _GroupOneVoucherAmount = 0;
            _FK_AccountHeadSub = 0;

        }
        public decimal GroupOneVoucherAmount
        {
            get { return _GroupOneVoucherAmount; }
            set { _GroupOneVoucherAmount = value; }
        }
        public string GroupOneVoucherNo
        {
            get { return _GroupOneVoucherNo; }
            set { _GroupOneVoucherNo = value; }
        }
      
        public Int64 FK_GroupOneVoucher
        {
            get { return _FK_GroupOneVoucher; }
            set { _FK_GroupOneVoucher = value; }
        }
        public Int64 MasterId
        {
            get { return _ID_GroupOneVoucher; }
            set { _ID_GroupOneVoucher = value; }
        }

        public Int64 FK_AccountHeadSub
        {
            get { return _FK_AccountHeadSub; }
            set { _FK_AccountHeadSub = value; }
        }
        public string GroupOneName
        {
            get { return _GroupOneName; }
            set { _GroupOneName = value; }
        }
      
        public string GroupOneCode
        {
            get { return _GroupOneCode; }
            set { _GroupOneCode = value; }
        }
        
        public int SortOrder
        {
            get { return _SortOrder; }
            set { _SortOrder = value; }
        }
      

        public string Descrption
        {
            get { return _Description; }
            set { _Description = value; }
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


        public Boolean Active
        {
            get { return _Active; }
            set { _Active = value; }
        }
        

        public Int64 FK_Client
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
        public long InsertData()
        {
            DalGroupOneVoucher dalGroupOneVoucher=new DalGroupOneVoucher();
            long status = 0;
            try
            {
                _UserAction = 1;
               status=dalGroupOneVoucher.UpdateGroupOneVoucher(this);
               dalGroupOneVoucher = null;
            }
            catch (Exception ex)
            {
            }
            return status;
        }
        public long UpdateData()
        {
            DalGroupOneVoucher dalGroupOneVoucher=new DalGroupOneVoucher();
            long status = 0;
            try
            {
                _UserAction = 2;
                status = dalGroupOneVoucher.UpdateGroupOneVoucher(this);
                dalGroupOneVoucher = null;
            }
            catch (Exception ex)
            {
            }
            return status;
        }
        public long DeleteData()
        {
            DalGroupOneVoucher dalGroupOneVoucher=new DalGroupOneVoucher();

            long status;
            status = 0;
            try
            {
                status = dalGroupOneVoucher.DeleteGroupOneVoucher(this);
                dalGroupOneVoucher = null;
            }
            catch (Exception ex)
            {
            }
            return status;
        }
        public void SelectData()
        {
            DalGroupOneVoucher dalGroupOneVoucher=new DalGroupOneVoucher();
          IGroupOneVoucher iGroupOneVoucher=( IGroupOneVoucher)dalGroupOneVoucher.SelectGroupOneVoucher(this);
           _GroupOneCode =  iGroupOneVoucher.GroupOneCode;
           _GroupOneName =  iGroupOneVoucher.GroupOneName;                 
            _Description =  iGroupOneVoucher.Description;
            _Active =  iGroupOneVoucher.Active;
           dalGroupOneVoucher = null;
           

        }
        public DataTable SelectAllData()
        {
            DataTable dtbl = new DataTable();
            DalGroupOneVoucher dalGroupOneVoucher = new DalGroupOneVoucher();
            dtbl = dalGroupOneVoucher.SelectALLGroupOneVoucher(this);
            dalGroupOneVoucher = null;
            return dtbl;
        }
        public DataTable SelectGroupOneVoucherExist()
        {
            DataTable dtbl = new DataTable();
            DalGroupOneVoucher dalGroupOneVoucher = new DalGroupOneVoucher();
            dtbl = dalGroupOneVoucher.SelectGroupOneVoucherExist(this);
            dalGroupOneVoucher = null;
            return dtbl;
        }
       
    }
    
}
