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
using WebPharma.DataAccess.Masters;
using WebPharma.Interface.Masters;
using System.Data;



namespace WebPharma.Business.Masters
{
    public class BlSettings : ISettings
    {
        private byte _UserAction;
        private string _UserCode;
        private DateTime _EnteredOn;
        private Int64 _FK_Client;
        private Int16 _PageIndex;
        private Int16 _PageSize;
        private string _MultiId;
        private Int64 _ID_Settings;
        private string _Module;
        private string _SubModule;
        private string _Name;
        private string _Value;
        private byte _DeleteFlag;
     
        public BlSettings()
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
            _MultiId = string.Empty;
            _ID_Settings = 0;
            _Module = string.Empty;
            _SubModule = string.Empty;
            _Name = string.Empty;
            _Value = string.Empty;
            _DeleteFlag = 1;
            
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
        public string MultiId
        {
            get { return _MultiId; }
            set { _MultiId = value; }
        }
        public Int64 MasterId
        {
            get { return _ID_Settings; }
            set { _ID_Settings = value; }
        }
        public byte DeleteFlag
        {
            get { return _DeleteFlag; }
            set { _DeleteFlag = value; }
        }
        public string Module
        {
            get { return _Module; }
            set { _Module = value; }
        }
        public string SubModule
        {
            get { return _SubModule; }
            set { _SubModule = value; }
        }
        public string Name
        {
            get { return _Name; }
            set { _Name = value; }
        }
        public string Value
        {
            get { return _Value; }
            set { _Value = value; }
        }

       


        public long InsertData()
        {
            long status;
            DalSettings dalsettings = new DalSettings();
           
            status = 0;
            try
            {
                _UserAction = 1;
                status = dalsettings.UpdateSettings(this);
                dalsettings = null;
            }
            catch (Exception ex)
            {
            }
            return status;
        }
        public long UpdateData()
        {
            _UserAction = 2;
            DalSettings dalsettings = new DalSettings();
            long status;
            status = 0;
            try
            {
                status = dalsettings.UpdateSettings(this);
               
                dalsettings = null;
            }
            catch (Exception ex)
            {
            }
            return status;
        }
        public long DeleteData()
        {
            _UserAction = 3;
            DalSettings dalsettings = new DalSettings();
            long status;
            status = 0;
            try
            {
                status = dalsettings.DeleteSettings(this);
                dalsettings = null;
            }
            catch (Exception ex)
            {
            }
            return status;
        }
        public DataTable SelectSettings()
        {
            DataTable dtbl = new DataTable();
            DalSettings dalsettings = new DalSettings();
            dtbl = dalsettings.SelectSettings(this);
            dalsettings = null;
            return dtbl;
        }
        public string GetModuleSettings()
        {          
            DalSettings dalsettings = new DalSettings();
            string settings;
            settings = "";
            try
            {
                settings = dalsettings.GetModuleSettings(this);
                dalsettings = null;
            }
            catch (Exception ex)
            {
            }
            return settings;
        }

        public int CreateDBBackup(string DBLocation, string UserCode, Int64 FK_Client)
        {
            int result;
            DalSettings dalsettings = new DalSettings();
            result = dalsettings.CreateDBBackup(DBLocation, UserCode, FK_Client);
            dalsettings = null;
            return result;
        }
        public DataTable SelectDBBackup(int PageIndex, int PageSize, Int64 FK_Client)
        {
            DataTable dtbl = new DataTable();
            DalSettings dalsettings = new DalSettings();
            dtbl = dalsettings.SelectDBBackup(PageIndex, PageSize, FK_Client);
            dalsettings = null;
            return dtbl;
        }
        public string GetLastBackupLocation()
        {
            DalSettings dalsettings = new DalSettings();
            string location;
            location = "";
            location = dalsettings.GetLastBackupLocation();
            dalsettings = null;
            return location;
        }

        public DataTable SelectBarcodeLocationPath()
        {
            DataTable dtbl = new DataTable();
            DalSettings dalsettings = new DalSettings();
            dtbl = dalsettings.SelectBarcodeLocationPath(this);
            dalsettings = null;
            return dtbl;
        }
    }
}
