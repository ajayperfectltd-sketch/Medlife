/*----------------------------------- Business Layer Class -----------------------------------------------------------------------------------------------------------------------
Purpose    : 
Author     : Praveen MP
Copyright  : Perfect Software Solutions (Clt) Pvt Ltd
Created on : 21/05/2018
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
using WebPharma.Interface.Masters;
using WebPharma.DataAccess.Masters;
using System.Data;

namespace WebPharma.Business.Masters
{
   public class BlDistrict:IDistrict
    {
        private byte _UserAction;
        private string _UserCode;
        private DateTime _EnteredOn;
        private Int64 _FK_Client;      
        private Int16 _PageIndex;
        private Int16 _PageSize;
        private Int64 _ID_Country;
        private string _DistrictName;
        private Int64 _FK_Country;
        private long _FK_Reason;
        private Int64 _FK_States;

        public BlDistrict()
        {
            Initialize();
        }

        public void Initialize()
        {
            _UserAction = 1;
            _UserCode = string.Empty;
            _EnteredOn = DateTime.Now;
            _FK_Client = 1;
            _PageIndex = 1;
            _PageSize = 10;
            _ID_Country = 0;
            _DistrictName = string.Empty;
            _FK_States = 0;
            _FK_Country = 0;
            _FK_Reason = 0;
        }

        public byte UserAction
        {
            get { return _UserAction; }
            set { _UserAction = value; }
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
        public Int64 FK_Client
        {
            get { return _FK_Client; }
            set { _FK_Client = value; }
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
        public Int64 MasterId
        {
            get { return _ID_Country; }
            set { _ID_Country = value; }
        }
        public string DistrictName
        {
            get { return _DistrictName; }
            set { _DistrictName = value; }
        }
           
        public Int64 FK_Country
        {
            get { return _FK_Country; }
            set { _FK_Country = value; }
        }
        public long FK_Reason
        {
            get { return _FK_Reason; }
            set { _FK_Reason = value; }
        }
        public Int64 FK_States
        {
            get { return _FK_States; }
            set { _FK_States = value; }
        }
        public long InsertData()
        {
            long status;
            DalDistrict daldistrict = new DalDistrict();
            status = 0;
            try
            {
                _UserAction = 1;
                status = daldistrict.UpdateDistrict(this);
                daldistrict = null;
            }
            catch (Exception ex)
            {
            }
            return status;
        }
        public long UpdateData()
        {
            DalDistrict daldistrict = new DalDistrict();
            long status;
            status = 0;
            try
            {
                _UserAction = 2;
                status = daldistrict.UpdateDistrict(this);
                daldistrict = null;
            }
            catch (Exception ex)
            {
            }
            return status;
        }
        public long DeleteData()
        {
            DalDistrict daldistrict = new DalDistrict();
            long status;
            status = 0;
            try
            {
                status = daldistrict.DeleteDistrict(this);
                daldistrict = null;
            }
            catch (Exception ex)
            {
            }
            return status;
        }
        public void SelectData()
        {
            DalDistrict daldistrict = new DalDistrict();
            IDistrict idistrict;
            idistrict = (IDistrict)daldistrict.SelectDistrict(this);
         
            _DistrictName = idistrict.DistrictName;
            _FK_Country = idistrict.FK_Country;
            _FK_States = idistrict.FK_States;
            daldistrict = null;
        }
        public DataTable SelectAllData()
        {
            DataTable dtbl = new DataTable();
            DalDistrict daldistrict = new DalDistrict();
            dtbl = daldistrict.SelectAllDistrict(this);
            daldistrict = null;
            return dtbl;
        }
    }
}
