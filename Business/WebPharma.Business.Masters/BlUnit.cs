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
    public class BlUnit:IUnits
    {
        #region Initialize
        private byte _UserAction;
        private Int16 _PageIndex;
        private Int16 _PageSize;
        private long _ID_Units;
        private string _Name;
        private long _FK_CategoryFirst;
        private int _NoofUnits;
        private string _UserCode;
        bool _BaseUnit;
        private long _FK_Reason;

        private DateTime _EnteredOn;


        private long _FK_Client;

        
        public  BlUnit()
        {
            Initialize();
        }
        public void Initialize()
        {
            _UserAction = 1;
            _PageIndex = 1;
            _PageSize = 10;
            _ID_Units = 0;
            _Name = string.Empty;
            _FK_CategoryFirst = 0;
            _NoofUnits = 0;
            _UserCode = string.Empty;
            _BaseUnit = false;
            _EnteredOn = DateTime.Now;
            _FK_Client = 1;
            _FK_Reason = 1;

        }
        #endregion
        #region Propeties
        public long MasterId
        {
            get { return _ID_Units; }
            set { _ID_Units = value; }
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
    

        public long ID_Units
        {
            get { return _ID_Units; }
            set { _ID_Units = value; }
        }
    
        public string Name
        {
            get { return _Name; }
            set { _Name = value; }
        }
        public long FK_CategoryFirst
        {
            get { return _FK_CategoryFirst; }
            set { _FK_CategoryFirst = value; }
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
        public bool BaseUnit
        {
            get { return _BaseUnit; }
            set { _BaseUnit = value; }
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
        #region InsertDate 
        public long InsertData()
       {
           long status;
           DalUnit dlUnit = new DalUnit();
           status = 0;
           try
           {
               _UserAction = 1;
               status = dlUnit.UpdateUnit(this);
               dlUnit = null;
           }
           catch (Exception ex)
           {
           }
           return status;

       }
        public long UpdateDate()
        {

            _UserAction = 2;
            DalUnit dlUnits = new DalUnit();
            long status;
            status = 0;
            try
            {
                status = dlUnits.UpdateUnit(this);
                dlUnits = null;

            }
            catch (Exception e)
            {
                throw e;
            }
            return status;
        }
            
		public long DeleteData()
		{
			_UserAction =3;
            DalUnit dlunit=new DalUnit();
            long status;
			status = 0;
			try
			{
                status = dlunit.DeleteUnits(this);
                dlunit = null;
            }
            catch(Exception ex)
            {
            }
            return status;
        }
        public void SelectData()
        {
            DalUnit dalUnit = new DalUnit();
            IUnits IUnit = (IUnits)dalUnit.SelectUnit(this);
            _Name = IUnit.Name;
          //  _FK_CategoryFirst = IUnit.FK_CategoryFirst;
            _NoofUnits = IUnit.NoofUnits;
           // _BaseUnit = IUnit.BaseUnit;

        }
        public DataTable SelectAllData()
        {
            DalUnit dlunit = new DalUnit();
           DataTable dtbl=dlunit.SelectAllUnits(this);
            dlunit=null;
            return dtbl;
          

        }
        public string GetCategoryBaseUnit(long FK_CategoryFirst)
        {
            DalUnit dalUnit = new DalUnit();
            string staus = dalUnit.GetCategoryBaseUnit(FK_CategoryFirst);
            dalUnit = null;
            return staus;
        }
        #endregion
    }
}
