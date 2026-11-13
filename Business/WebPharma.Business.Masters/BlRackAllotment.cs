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
        public class BlRackAllotment : IRackAllotment
        {
          
            private long _ID_RackAllotment;
            private long _FK_Rack;

          
            private DateTime _AllotmentDate;
            private long _FK_Supplier;
            private Int32 _PeriodType;
            private Int32 _Period;
            private DateTime _DueDate;
            private decimal _Rent;
            private string _Remarks;

            private string _UserCode;          
            private long _FK_Client;
            private DateTime _EnteredOn;
            private byte _UserAction;
            private Int16 _PageIndex;
            private Int16 _PageSize;
            private Boolean _Vacate;

           

            public BlRackAllotment()
            {
                Initialize();
            }
            public void Initialize()
            {

            _AllotmentDate = DateTime.Now;
            _FK_Supplier=0;
            _PeriodType=1;
             _Period=0;
            _DueDate=DateTime.Now;
            _Rent=0;
            _Remarks = string.Empty;
            _FK_Rack = 0;
                _UserCode = string.Empty;
                _EnteredOn = DateTime.Now;
                _FK_Client = 1;
                _PageIndex = 1;
                _PageSize = 10;
                _UserAction = 1;
                _Vacate = false;
            }


            public Int64 MasterId
            {
                get { return _ID_RackAllotment; }
                set { _ID_RackAllotment = value; }
            }
            public Int64 FK_Rack
            {
                get { return _FK_Rack; }
                set { _FK_Rack = value; }
            }
            public DateTime AllotmentDate
            {
                get { return _AllotmentDate; }
                set { _AllotmentDate = value; }
            }
            public Int64 FK_Supplier
            {
                get { return _FK_Supplier; }
                set { _FK_Supplier = value; }
            }
            public Int32 PeriodType
            {
                get { return _PeriodType; }
                set { _PeriodType = value; }
            }
            public Int32 Period
            {
                get { return _Period; }
                set { _Period = value; }
            }

            public DateTime DueDate
            {
                get { return _DueDate; }
                set { _DueDate = value; }
            }
            public decimal Rent
            {
                get { return _Rent; }
                set { _Rent = value; }
            }
            public string Remarks
            {
                get { return _Remarks; }
                set { _Remarks = value; }
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

            public long InsertData()
            {
                long status;
                DalRackAllotment dalRackAllotment = new DalRackAllotment();
                status = 0;
                try
                {
                    _UserAction = 1;
                    status = dalRackAllotment.UpdateRackAllotment(this);
                    dalRackAllotment = null;
                }
                catch (Exception ex)
                {

                }
                return status;
            }

            public long UpdateData()
            {
                _UserAction = 2;
                DalRackAllotment dalRackAllotment = new DalRackAllotment();
                long status;
                status = 0;
                try
                {
                    status = dalRackAllotment.UpdateRackAllotment(this);
                    dalRackAllotment = null;
                }
                catch (Exception ex)
                {

                }
                return status;
            }

            public long DeleteData()
            {
                _UserAction = 3;
                DalRackAllotment dalRackAllotment = new DalRackAllotment();
                long status;
                status = 0;
                try
                {
                    status = dalRackAllotment.DeleteRackAllotment(this);
                    dalRackAllotment = null;
                }
                catch (Exception ex)
                {
                }
                return status;
            }
     
            public void SelectData()
            {
                DalRackAllotment dalRackAllotment = new DalRackAllotment();
                IRackAllotment iRackAllotment;
                iRackAllotment = (IRackAllotment)dalRackAllotment.SelectRackAllotment(this);
                _ID_RackAllotment = iRackAllotment.MasterId;
                _FK_Rack = iRackAllotment.FK_Rack;

                _AllotmentDate = iRackAllotment.AllotmentDate;
                _FK_Supplier = iRackAllotment.FK_Supplier;
                _PeriodType = iRackAllotment.PeriodType;
                _Period = iRackAllotment.Period;
                _DueDate = iRackAllotment.DueDate;
                _Rent = iRackAllotment.Rent;
                _Remarks = iRackAllotment.Remarks;

                _UserCode = iRackAllotment.UserCode;
                _EnteredOn = iRackAllotment.EnteredOn;
                _FK_Client = iRackAllotment.FK_Client;



            }
            public DataTable SelectAllData()
            {
                DataTable dtbl = new DataTable();
                DalRackAllotment dalRackAllotment = new DalRackAllotment();
                dtbl = dalRackAllotment.SelectAllRackAllotment(this);
                dalRackAllotment = null;
                return dtbl;
            }
       

        }
    }

