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
using WebPharma.Interface.Masters;
using WebPharma.DataAccess.Masters;
using System.Data;

namespace WebPharma.Business.Masters
{
   public class BlBank:IBank
    {
        private byte _UserAction;
        private string _UserCode;
        private DateTime _EnteredOn;
        private Int64 _FK_Client;
        private Int16 _PageIndex;
        private Int16 _PageSize;
        private Int64 _ID_Bank;
        private string _Name;
        private string _ShortName;
        private string _Address1;
        private string _Address2;
        private string _Address3;
        private Boolean _Active;

        public BlBank()
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
            _ID_Bank = 0;
            _Name = string.Empty;
            _ShortName = string.Empty;
            _Address1 = string.Empty;
            _Address2 = string.Empty;
            _Address3 = string.Empty;
            _Active = true;
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
            get { return _ID_Bank; }
            set { _ID_Bank = value; }
        }
        public string Name
        {
            get { return _Name; }
            set { _Name = value; }
        }
        public string ShortName
        {
            get { return _ShortName; }
            set { _ShortName = value; }
        }
        public string Address1
        {
            get { return _Address1; }
            set { _Address1 = value; }
        }
        public string Address2
        {
            get { return _Address2; }
            set { _Address2 = value; }
        }
        public string Address3
        {
            get { return _Address3; }
            set { _Address3 = value; }
        }
        public Boolean Active
        {
            get { return _Active; }
            set { _Active = value; }
        }

        public long InsertData()
        {
            long status;
            DalBank dalbank = new DalBank();
            status = 0;
            try
            {
                _UserAction = 1;
                status = dalbank.UpdateBank(this);
                dalbank = null;
            }
            catch (Exception ex)
            {
            }
            return status;
        }
        public long UpdateData()
        {
            DalBank dalbank = new DalBank();
            long status;
            status = 0;
            try
            {
                _UserAction = 2;
                status = dalbank.UpdateBank(this);
                dalbank = null;
            }
            catch (Exception ex)
            {
            }
            return status;
        }
        public long DeleteData()
        {
            DalBank dalbank = new DalBank();
            long status;
            status = 0;
            try
            {
                status = dalbank.DeleteBank(this);
                dalbank = null;
            }
            catch (Exception ex)
            {
            }
            return status;
        }
        public void SelectData()
        {
            DalBank dalbank = new DalBank();
            IBank ibank;
            ibank = (IBank)dalbank.SelectBank(this);
            _ShortName = ibank.ShortName;
            _Name = ibank.Name;
            _Address1 = ibank.Address1;
            _Address2 = ibank.Address2;
            _Address3 = ibank.Address3;
            _Active = ibank.Active;
            dalbank = null;
        }
        public DataTable SelectAllData()
        {
            DataTable dtbl = new DataTable();
            DalBank dalbank = new DalBank();
            dtbl = dalbank.SelectAllBank(this);
            dalbank = null;
            return dtbl;
        }
    }
}
