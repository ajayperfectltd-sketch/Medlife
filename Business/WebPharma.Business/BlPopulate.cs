using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebPharma.Interface;
using System.Data;
using System.Collections;
using WebPharma.DataAccess;

namespace WebPharma.Business
{
   public class BlPopulate:IPopulate
    {
        private string _TableName;
        private string _ListFields;
        private string _ValueFields;
        private string _SortFields;
        private string _Criteria;
        private string _UpdateField;
        private string _UpdateValue;
        private string _GroupBy;
        private string _Criteria2;
        private string _SearchCriteria;
        private string _DbName;
        private int _SortOrder;
        private string _Usercode;
        private string _Module;
        public string Module
        {
            get { return _Module; }
            set { _Module = value; }
        }
        public string Usercode
        {
            get { return _Usercode; }
            set { _Usercode = value; }
        }
        public string SearchCriteria
        {
            get { return _SearchCriteria; }
            set { _SearchCriteria = value; }
        }

        public string Criteria2
        {
            get { return _Criteria2; }
            set { _Criteria2 = value; }
        }

        public string GroupBy
        {
            get { return _GroupBy; }
            set { _GroupBy = value; }
        }
        private int _PageSize;

        public int PageSize
        {
            get { return _PageSize; }
            set { _PageSize = value; }
        }
        private int _PageIndex;

        public int PageIndex
        {
            get { return _PageIndex; }
            set { _PageIndex = value; }
        }
        public string DbName
        {
            get { return _DbName; }
            set { _DbName = value; }
        }
        public int SortOrder
        {
            get { return _SortOrder; }
            set { _SortOrder = value; }
        }
       
        public BlPopulate()
        {
            Initialize();
        }
        public void Initialize()
        {
            _TableName = string.Empty;
            _ListFields = string.Empty;
            _ValueFields = string.Empty;
            _SortFields = string.Empty;
            _Criteria = string.Empty;
            _UpdateField = string.Empty;
            _UpdateValue = string.Empty;
            _GroupBy = "";
            _PageIndex = 1;
            _PageSize = 10;
            Criteria2 = string.Empty;
            _SearchCriteria = String.Empty;
            _DbName = String.Empty;
            _SortOrder = 0;
            _Usercode = string.Empty;
            _Module = string.Empty;
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

        public DataTable PopulateData()
        {
            DalPopulate dalpopulate = new DalPopulate();
            try
            {
                return dalpopulate.PopulateData(this);
            }
            catch (Exception ex)
            {
               throw;
            }

        }
        public DataTable PopulateGrid()
        {
            DalPopulate dalpopulate = new DalPopulate();
            try
            {
                return dalpopulate.PopulateGrid(this);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public Int32 GetNextNumber(string tblname, string fieldname, string criteria, string dbname)
        {
            DalPopulate dalpopulate = new DalPopulate();
            TableName = tblname;
            ValueFields = fieldname;
            DbName = dbname;
            Criteria2 = criteria;
            return dalpopulate.GetNextNumber(this);

        }
        public DataTable PopulatePopupGrid()
        {
            DalPopulate dalpopulate = new DalPopulate();
            try
            {
                return dalpopulate.PopulatePopUpGrid(this);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public DataTable PopulateSalesReturnGrid()
        {
            DalPopulate dalpopulate = new DalPopulate();
            try
            {
                return dalpopulate.PopulateSalesReturnGrid(this);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
