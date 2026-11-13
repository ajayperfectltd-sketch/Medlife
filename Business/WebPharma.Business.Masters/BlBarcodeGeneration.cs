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
   public class BlBarcodeGeneration:IBarcodeGeneration
    {
        private byte _UserAction;
        private string _UserCode;
        private DateTime _EnteredOn;
        private Int64 _FK_Client;
        private Int16 _PageIndex;
        private Int16 _PageSize;
        private Int64 _ID_BarcodeGeneration;
        private string _Name;
        private string _ShortName;
        private string _BarCode;

      
        private string _Address1;
        private string _Address2;
        private string _Address3;
        private Boolean _Active;

        private Int64 _FK_StockCode;
        private Int64 _FK_Items;
        private string _PriLayout;

     

        public BlBarcodeGeneration()
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
            _BarCode = string.Empty;
            _ID_BarcodeGeneration = 0;
            _Name = string.Empty;
            _ShortName = string.Empty;
            _Address1 = string.Empty;
            _Address2 = string.Empty;
            _Address3 = string.Empty;
            _Active = true;
            _FK_StockCode = 0;
            _FK_Items = 0;
            _PriLayout = string.Empty;
        }


        public string BarCode
        {
            get { return _BarCode; }
            set { _BarCode = value; }
        }
        public string PriLayout
        {
            get { return _PriLayout; }
            set { _PriLayout = value; }
        }

        public Int64 FK_Items
        {
            get { return _FK_Items; }
            set { _FK_Items = value; }
        }
        public Int64 FK_StockCode
        {
            get { return _FK_StockCode; }
            set { _FK_StockCode = value; }
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
            get { return _ID_BarcodeGeneration; }
            set { _ID_BarcodeGeneration = value; }
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
            DalBarcodeGeneration dalBarcodeGeneration = new DalBarcodeGeneration();
            status = 0;
            try
            {
                _UserAction = 1;
                status = dalBarcodeGeneration.UpdateBarcodeGeneration(this);
                dalBarcodeGeneration = null;
            }
            catch (Exception ex)
            {
            }
            return status;
        }
        public long UpdateData()
        {
            DalBarcodeGeneration dalBarcodeGeneration = new DalBarcodeGeneration();
            long status;
            status = 0;
            try
            {
                _UserAction = 2;
                status = dalBarcodeGeneration.UpdateBarcodeGeneration(this);
                dalBarcodeGeneration = null;
            }
            catch (Exception ex)
            {
            }
            return status;
        }
        public long DeleteData()
        {
            DalBarcodeGeneration dalBarcodeGeneration = new DalBarcodeGeneration();
            long status;
            status = 0;
            try
            {
                status = dalBarcodeGeneration.DeleteBarcodeGeneration(this);
                dalBarcodeGeneration = null;
            }
            catch (Exception ex)
            {
            }
            return status;
        }
        public void SelectData()
        {
            DalBarcodeGeneration dalBarcodeGeneration = new DalBarcodeGeneration();
            IBarcodeGeneration iBarcodeGeneration;
            iBarcodeGeneration = (IBarcodeGeneration)dalBarcodeGeneration.SelectBarcodeGeneration(this);
            _ShortName = iBarcodeGeneration.ShortName;
            _Name = iBarcodeGeneration.Name;
            _Address1 = iBarcodeGeneration.Address1;
            _Address2 = iBarcodeGeneration.Address2;
            _Address3 = iBarcodeGeneration.Address3;
            _Active = iBarcodeGeneration.Active;
            dalBarcodeGeneration = null;
        }
        public DataTable SelectAllData()
        {
            DataTable dtbl = new DataTable();
            DalBarcodeGeneration dalBarcodeGeneration = new DalBarcodeGeneration();
            dtbl = dalBarcodeGeneration.SelectAllBarcodeGeneration(this);
            dalBarcodeGeneration = null;
            return dtbl;
        }
        public DataTable SelectPrintLayout()
        {
            DataTable dtbl = new DataTable();
            DalBarcodeGeneration DalBarcodeGeneration = new DalBarcodeGeneration();
            dtbl = DalBarcodeGeneration.SelectPrintLayout(this);
            DalBarcodeGeneration = null;
            return dtbl;
        }
        public DataTable SelectPrintLayoutSalesReturn()
        {
            DataTable dtbl = new DataTable();
            DalBarcodeGeneration DalBarcodeGeneration = new DalBarcodeGeneration();
            dtbl = DalBarcodeGeneration.SelectPrintLayoutSalesReturn(this);
            DalBarcodeGeneration = null;
            return dtbl;
        }
        public DataTable SelectWeighingMachineItemsSelect()
        {
            DataTable dtbl = new DataTable();
            DalBarcodeGeneration DalBarcodeGeneration = new DalBarcodeGeneration();
            dtbl = DalBarcodeGeneration.SelectWeighingMachineItemsSelect(this);
            DalBarcodeGeneration = null;
            return dtbl;
        }
        public DataTable SelectWeighingMachineHistory(int PageIndex, int PageSize, Int64 FK_Client)
        {
            DataTable dtbl = new DataTable();
            DalBarcodeGeneration DalBarcodeGeneration = new DalBarcodeGeneration();
            dtbl = DalBarcodeGeneration.SelectWeighingMachineHistory(PageIndex, PageSize, FK_Client);
            DalBarcodeGeneration = null;
            return dtbl;
        }
    }
}
