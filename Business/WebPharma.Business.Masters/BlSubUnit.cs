using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebPharma.DataAccess.Masters;
using WebPharma.Interface.Masters;

namespace WebPharma.Business.Masters
{
    public class BlSubUnit : ISubUnit
    {
        #region Initialize
        private byte _UserAction;
        private Int16 _PageIndex;
        private Int16 _PageSize;
        private long _ID_SubUnits;
        private string _Name;
        private long _Fk_Units;
        private int _NoofUnits;
        private string _UserCode;
        private long _FK_Reason;
        private DateTime _EnteredOn;
        private long _FK_Client;


        public BlSubUnit()
        {
            Initialize();
        }
        public void Initialize()
        {
            _UserAction = 1;
            _PageIndex = 1;
            _PageSize = 10;
            _ID_SubUnits = 0;
            _Name = string.Empty;
            _Fk_Units = 0;
            _NoofUnits = 0;
            _UserCode = string.Empty;
            _EnteredOn = DateTime.Now;
            _FK_Client = 1;
            _FK_Reason = 1;

        }
        #endregion
        #region Propeties
        public long MasterId
        {
            get { return _ID_SubUnits; }
            set { _ID_SubUnits = value; }
        }
        public Int16 PageIndex
        {
            get { return _PageIndex; }
            set { _PageIndex = value; }
        }
        public byte UserAction
        {
            get { return _UserAction; }
            set { _UserAction = value; }
        }
        public Int16 PageSize
        {
            get { return _PageSize; }
            set { _PageSize = value; }
        }

        public long FK_Reason
        {
            get { return _FK_Reason; }
            set { _FK_Reason = value; }
        }


        public string Name
        {
            get { return _Name; }
            set { _Name = value; }
        }
        public long Fk_Units
        {
            get { return _Fk_Units; }
            set { _Fk_Units = value; }
        }
        public int NoofUnits
        {
            get { return _NoofUnits; }
            set { _NoofUnits = value; }
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
        #endregion
        #region InsertData
        public long InsertData()
        {
            long status;
            DalSubUnit dlsubUnit = new DalSubUnit();
            status = 0;
            try
            {
                _UserAction = 1;
                status = dlsubUnit.UpdatesubUnit(this);
                dlsubUnit = null;
            }
            catch (Exception ex)
            {
            }
            return status;

        }
        public long UpdateDate()
        {

            _UserAction = 2;
            DalSubUnit dlsubUnit = new DalSubUnit();
            long status;
            status = 0;
            try
            {
                status = dlsubUnit.UpdatesubUnit(this);
                dlsubUnit = null;

            }
            catch (Exception e)
            {
                throw e;
            }
            return status;
        }

        public long DeleteData()
        {
            _UserAction = 3;
            DalSubUnit dlsubUnit = new DalSubUnit();
            long status;
            status = 0;
            try
            {
                status = dlsubUnit.DeletesubUnits(this);
                dlsubUnit = null;
            }
            catch (Exception ex)
            {
            }
            return status;
        }
        public void SelectData()
        {
            DalSubUnit dlsubUnit = new DalSubUnit();
            ISubUnit IUnit = (ISubUnit)dlsubUnit.SelectsubUnit(this);
            _Name = IUnit.Name;
            //  _FK_CategoryFirst = IUnit.FK_CategoryFirst;
            _NoofUnits = IUnit.NoofUnits;
            // _BaseUnit = IUnit.BaseUnit;

        }
        public DataTable SelectAllData()
        {
            DalSubUnit dlsubUnit = new DalSubUnit();
            DataTable dtbl = dlsubUnit.SelectAllsubUnits(this);
            dlsubUnit = null;
            return dtbl;


        }
        //public string GetCategoryBaseUnit(long FK_CategoryFirst)
        //{
        //    DalSubUnit dlsubUnit = new DalSubUnit();
        //    string staus = dlsubUnit.GetCategoryBaseUnit(FK_CategoryFirst);
        //    dlsubUnit = null;
        //    return staus;
        //}
        #endregion
    }
}
