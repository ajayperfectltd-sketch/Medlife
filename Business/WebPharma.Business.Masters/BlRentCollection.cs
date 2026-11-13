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
   public class BlRentCollection:IRentCollection
       {
          
            private long _ID_RentCollection;
            private long _FK_Rack;          
            private DateTime _TransDate;
            private decimal _Amount;
            private Int32 _PaymentMode;

            private string _UserCode;          
            private long _FK_Client;
            private DateTime _EnteredOn;
            private byte _UserAction;
            private Int16 _PageIndex;
            private Int16 _PageSize;
            private Boolean _Vacate;
            private long _FK_Supplier;

            private long _FK_RackAllotment;

            private string _RackName;

          
            



            public BlRentCollection()
            {
                Initialize();
            }
            public void Initialize()
            {
            _PaymentMode = 0;
            _Amount = 0;
            _TransDate = DateTime.Now;
            _FK_Rack = 0;
                _UserCode = string.Empty;
                _EnteredOn = DateTime.Now;
                _FK_Client = 1;
                _PageIndex = 1;
                _PageSize = 10;
                _UserAction = 1;
                _Vacate = false;
                _FK_RackAllotment = 0;
                _FK_Supplier = 0;
                _RackName = string.Empty;
            }


            public Int64 FK_RackAllotment
            {
                get { return _FK_RackAllotment; }
                set { _FK_RackAllotment = value; }
            }
            public Int64 MasterId
            {
                get { return _ID_RentCollection; }
                set { _ID_RentCollection = value; }
            }
            public Int64 FK_Rack
            {
                get { return _FK_Rack; }
                set { _FK_Rack = value; }
            }
            public DateTime TransDate
            {
                get { return _TransDate; }
                set { _TransDate = value; }
            }

            public Int32 PaymentMode
            {
                get { return _PaymentMode; }
                set { _PaymentMode = value; }
            }

            public decimal Amount
            {
                get { return _Amount; }
                set { _Amount = value; }
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
            public Boolean Vacate
            {
                get { return _Vacate; }
                set { _Vacate = value; }
            }
            public Int64 FK_Supplier
            {
                get { return _FK_Supplier; }
                set { _FK_Supplier = value; }
            }
            public string RackName
            {
                get { return _RackName; }
                set { _RackName = value; }
            }
            public long InsertData()
            {
                long status;
                DalRentCollection dalRentCollection = new DalRentCollection();
                status = 0;
                try
                {
                    _UserAction = 1;
                    status = dalRentCollection.UpdateRentCollection(this);
                    dalRentCollection = null;
                }
                catch (Exception ex)
                {

                }
                return status;
            }

            public long UpdateData()
            {
                _UserAction = 2;
                DalRentCollection dalRentCollection = new DalRentCollection();
                long status;
                status = 0;
                try
                {
                    status = dalRentCollection.UpdateRentCollection(this);
                    dalRentCollection = null;
                }
                catch (Exception ex)
                {

                }
                return status;
            }

            public long DeleteData()
            {
                _UserAction = 3;
                DalRentCollection dalRentCollection = new DalRentCollection();
                long status;
                status = 0;
                try
                {
                    status = dalRentCollection.UpdateRentCollection(this);
                    dalRentCollection = null;
                }
                catch (Exception ex)
                {
                }
                return status;
            }
     
            public void SelectData()
            {
                DalRentCollection dalRentCollection = new DalRentCollection();
                IRentCollection iRentCollection;
                iRentCollection = (IRentCollection)dalRentCollection.SelectRentCollection(this);
                _ID_RentCollection = iRentCollection.MasterId;
                _FK_Rack = iRentCollection.FK_Rack;
                _TransDate = iRentCollection.TransDate;
                _Amount = iRentCollection.Amount;
                _PaymentMode = iRentCollection.PaymentMode;
                _UserCode = iRentCollection.UserCode;
                _EnteredOn = iRentCollection.EnteredOn;
                _FK_Client = iRentCollection.FK_Client;
                _Vacate = iRentCollection.Vacate;
                _FK_Supplier = iRentCollection.FK_Supplier;
            }
            public DataTable SelectAllData()
            {
                DataTable dtbl = new DataTable();
                DalRentCollection dalRentCollection = new DalRentCollection();
                dtbl = dalRentCollection.SelectAllRentCollection(this);
                dalRentCollection = null;
                return dtbl;
            }

        }
    }

