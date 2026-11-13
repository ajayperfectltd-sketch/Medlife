using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using WebPharma.Interface.General;

using WebPharma.DataAccess.Masters;


namespace WebPharma.Business.Masters
{
    public class BlCategoryFirst : ICategoryFirst
    {

        private byte _UserAction;
        private Int16 _PageIndex;
        private Int16 _PageSize;
        private long _ID_Category;
        private string _Shortname;
        private string _Name;
        private decimal _Discount;
        private decimal _Profit;
        private int _ExpiryDays;
        private decimal _Commission;
        private string _UserCode;
        private DateTime _EnteredOn;
        private long _FK_Client;
        private long _FK_Reason;
        private Boolean _NonMedicine;
        public BlCategoryFirst()
        {
            Initialize();
        }
        public void Initialize()
        {
            _ID_Category = 0;
            _Shortname = string.Empty;
            _Name = string.Empty;
            _Discount = 0;
            _Profit = 0;
            _ExpiryDays = 1;
            _Commission = 0;
            _PageIndex = 1;
            _PageSize = 10;
            _EnteredOn = DateTime.Now;
            _FK_Client = 1;
            _UserAction = 1;
            _FK_Reason = 1;
            _UserCode = string.Empty;
            _NonMedicine = false;
        
        }
        public Boolean NonMedicine
        {
            get { return _NonMedicine; }
            set { _NonMedicine = value; }
        }
        public byte UserAction
        {
            get { return _UserAction; }
            set { _UserAction = value; }
        }
        public decimal Discount
        {
            get { return _Discount; }
            set { _Discount = value; }
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
            get { return _ID_Category; }
            set { _ID_Category = value; }
        }

        public string Shortname
        {
            get { return _Shortname; }
            set { _Shortname = value; }
        }
        public string Name
        {
            get { return _Name; }
            set { _Name = value; }
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
        public long FK_Client
        {
            get { return _FK_Client; }
            set { _FK_Client = value; }
        }

        public int ExpiryDays
        {
            get { return _ExpiryDays; }
            set { _ExpiryDays = value; }
        }
        public decimal Commission
        {
            get { return _Commission; }
            set { _Commission = value; }
        }
        public decimal Profit
        {
            get { return _Profit; }
            set { _Profit = value; }
        }
        public long FK_Reason
        {
            get { return _FK_Reason; }
            set { _FK_Reason = value; }
        }
        public long InsertData()
        {
            long status;
            DalCategoryFirst dalcategoryfirst = new DalCategoryFirst();
            status = 0;
            try
            {
                _UserAction = 1;
                status = dalcategoryfirst.UpdateCategoryFirst(this);
                dalcategoryfirst = null;
            }
            catch (Exception ex)
            {

            }
            return status;
        }

        public long UpdateData()
        {
            _UserAction = 2;
            DalCategoryFirst dalcategoryfirst = new DalCategoryFirst();
            long status;
            status = 0;
            try
            {
                status = dalcategoryfirst.UpdateCategoryFirst(this);
                dalcategoryfirst = null;
            }
            catch (Exception ex)
            {

            }
            return status;
        }

        public long DeleteData()
        {
            _UserAction = 3;
            DalCategoryFirst dalcategoryfirst = new DalCategoryFirst();
            long status;
            status = 0;
            try
            {
                status = dalcategoryfirst.DeleteCategoryFirst(this);
                dalcategoryfirst = null;
            }
            catch (Exception ex)
            {

            }
            return status;
        }

        public void SelectData()
        {
            DalCategoryFirst dalcategoryfirst = new DalCategoryFirst();
            ICategoryFirst icategoryfirst;
            icategoryfirst = (ICategoryFirst)dalcategoryfirst.SelectCategoryFirst(this);
            _Shortname = icategoryfirst.Shortname;
            _Name = icategoryfirst.Name;
            _Profit = icategoryfirst.Profit;
            _Discount = icategoryfirst.Discount;
            _Commission = icategoryfirst.Commission;
           
        }

        public DataTable SelectAllData()
        {
            DataTable dtbl = new DataTable();
            DalCategoryFirst dalcategoryfirst = new DalCategoryFirst();
            dtbl = dalcategoryfirst.SelectAllCategoryFirst(this);
            dalcategoryfirst = null;
            return dtbl;
        }
    }
}
