/*----------------------------------- DataAccess -----------------------------------------------------------------------------------------------------------------------
Purpose    : 
Author     : Aiswarya
Copyright  : Perfect Software Solutions (Clt) Pvt Ltd
Created on : 15/11/2021
----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
MODIFICATIONS 
On			By			OMID
----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------*/


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using Microsoft.Practices.EnterpriseLibrary.Data;
using WebPharma.DataAccess;
using WebPharma.Interface.General;
using WebPharma.Interface.WebPharma;
namespace WebPharma.DataAccess.WebPharma
{
    public class DalSales
    {

        //public Int32 UpdateSales(ISales isales)
        //{
        //    Database db = DatabaseFactory.CreateDatabase("WebPharma");
        //    string sqlCommand = "proSalesUpdate";
        //    DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
        //    //db.AddInParameter(dbCommand, "@UserAction", DbType.Byte, isales.UserAction);
        //    //    db.AddInParameter(dbCommand, "@ID_Sales", DbType.Int64, isales.MasterId);
        //    db.AddInParameter(dbCommand, "@FK_Store", DbType.Int64, isales.FK_Store);
        //    db.AddInParameter(dbCommand, "@FK_BillType", DbType.Int64, isales.FK_BillType);
        //    db.AddInParameter(dbCommand, "@BillNo", DbType.Int64, isales.BillNo);
        //    db.AddInParameter(dbCommand, "@BillDate", DbType.DateTime, isales.BillDate);
        //    db.AddInParameter(dbCommand, "@CustomerType", DbType.Byte, isales.CustomerType);
        //    db.AddInParameter(dbCommand, "@FK_Customer", DbType.Int64, isales.FK_Customer);
        //    db.AddInParameter(dbCommand, "@CustomerName", DbType.String, isales.CustomerName);
        //    db.AddInParameter(dbCommand, "@CardNumber", DbType.String, isales.CardNumber);
        //    db.AddInParameter(dbCommand, "@BillTotal", DbType.Decimal, isales.BillTotal);
        //    db.AddInParameter(dbCommand, "@Discount", DbType.Decimal, isales.DiscountTest);
        //    db.AddInParameter(dbCommand, "@RountOff", DbType.Decimal, isales.RountOff);
        //    db.AddInParameter(dbCommand, "@NetAmount", DbType.Decimal, isales.NetAmount);
        //    db.AddInParameter(dbCommand, "@PayAmount", DbType.Decimal, isales.PayAmount);
        //    db.AddInParameter(dbCommand, "@FK_Counter", DbType.Int64, isales.FK_Counter);
        //    db.AddInParameter(dbCommand, "@Fk_Shift", DbType.Int64, isales.Fk_Shift);
        //    db.AddInParameter(dbCommand, "@FK_Client", DbType.Int64, isales.FK_Client);
        //    db.AddInParameter(dbCommand, "@CashAmount", DbType.Decimal, isales.CashAmount);
        //    db.AddInParameter(dbCommand, "@CrdAmount", DbType.Decimal, isales.CrdAmount);
        //    db.AddInParameter(dbCommand, "@ChqAmount", DbType.Decimal, isales.ChqAmount);
        //    db.AddInParameter(dbCommand, "@ChqBank", DbType.String, isales.ChqBank);
        //    db.AddInParameter(dbCommand, "@ChqRemarks", DbType.String, isales.ChqRemarks);
        //    db.AddInParameter(dbCommand, "@Remarks", DbType.String, isales.Remarks);
        //    db.AddInParameter(dbCommand, "@UserCode", DbType.String, isales.UserCode);
        //    db.AddInParameter(dbCommand, "@CrdNumber", DbType.String, isales.CrdNumber);
        //    db.AddInParameter(dbCommand, "@CrdSlipNo", DbType.String, isales.CrdSlipNo);
        //    db.AddInParameter(dbCommand, "@CrdBank", DbType.String, isales.CrdBank);
        //    db.AddInParameter(dbCommand, "@CrdRemarks", DbType.String, isales.CrdRemarks);
        //    db.AddInParameter(dbCommand, "@ChqNumber", DbType.String, isales.ChqNumber);
        //    db.AddInParameter(dbCommand, "@ChqAccount", DbType.String, isales.ChqAccount);
        //    db.AddInParameter(dbCommand, "@EnteredOn", DbType.DateTime, isales.EnteredOn);
        //    db.AddInParameter(dbCommand, "@ChqDate", DbType.DateTime, isales.ChqDate);
        //    db.AddInParameter(dbCommand, "@Credit", DbType.Boolean, isales.Credit);
        //    db.AddInParameter(dbCommand, "@Id_Hold", DbType.Int64, isales.FK_Hold);
        //    db.AddInParameter(dbCommand, "@XmlGiftDetails", DbType.String, isales.XmlGiftDetails);
        //    db.AddInParameter(dbCommand, "@XmlDiscDetails", DbType.String, isales.XmlDiscDetails);
        //    db.AddInParameter(dbCommand, "@XmlSales", DbType.String, isales.XmlSalesDetails);
        //    db.AddInParameter(dbCommand, "@BillDiscount", DbType.Decimal, isales.Discount);
        //    db.AddInParameter(dbCommand, "@NegativeStock", DbType.Int16, isales.NegativeStock);
        //    db.AddInParameter(dbCommand, "@FK_RecoveryType", DbType.Int64, isales.FK_RecoveryType);
        //    db.AddInParameter(dbCommand, "@InstallmentPeriod", DbType.Int32, isales.InstallmentPeriod);
        //    db.AddInParameter(dbCommand, "@InstallmentDuration", DbType.Int32, isales.InstallmentDuration);
        //    db.AddInParameter(dbCommand, "@InstallmentAmount", DbType.Decimal, isales.InstallmentAmount);
        //    db.AddInParameter(dbCommand, "@RecoveryDate", DbType.DateTime, isales.RecoveryDate);
        //    db.AddInParameter(dbCommand, "@SubsAmount", DbType.Decimal, isales.SubsAmount);
        //    db.AddInParameter(dbCommand, "@SubsNumber", DbType.String, isales.SubsNumber);
        //    db.AddInParameter(dbCommand, "@SubsPercentage", DbType.Decimal, isales.SubsPercentage);
        //    db.AddInParameter(dbCommand, "@SubsDate", DbType.DateTime, isales.SubsDate);
        //    db.AddInParameter(dbCommand, "@SubsAuthority", DbType.String, isales.SubsAuthority);
        //    db.AddInParameter(dbCommand, "@SubsRemarks", DbType.String, isales.SubsRemarks);
        //    db.AddInParameter(dbCommand, "@InterState", DbType.Boolean, isales.InterState);
        //    db.AddInParameter(dbCommand, "@RedeemPoint", DbType.Decimal, isales.RedeemPoint);
        //    db.AddInParameter(dbCommand, "@RedeemAmount", DbType.Decimal, isales.RedeemAmount);
        //    db.AddInParameter(dbCommand, "@RedeemDiscount", DbType.Decimal, isales.RedeemDiscount);
        //    db.AddInParameter(dbCommand, "@GSTIN", DbType.String, isales.GSTIN);
        //    db.AddInParameter(dbCommand, "@TransactionID", DbType.String, isales.TransID);

        //    try
        //    {
        //        //string s = "";
        //        //s = Convert.ToString(db.ExecuteScalar(dbCommand));
        //        return Convert.ToInt32(db.ExecuteScalar(dbCommand));

        //    }
        //    catch (SqlException e)
        //    {
        //        // UpdateErrorLog(istock, e);
        //        return 0;
        //    }
        //}
        public ISales UpdateSales(ISales isales)
        {


            Database db = DatabaseFactory.CreateDatabase("WebPharma");
            string sqlCommand = "proSalesUpdate";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
            DataSet ds = new DataSet();
            db.AddInParameter(dbCommand, "@FK_Store", DbType.Int64, isales.FK_Store);
            db.AddInParameter(dbCommand, "@FK_BillType", DbType.Int64, isales.FK_BillType);
            db.AddInParameter(dbCommand, "@BillNo", DbType.Int64, isales.BillNo);
            db.AddInParameter(dbCommand, "@BillDate", DbType.DateTime, isales.BillDate);
            db.AddInParameter(dbCommand, "@CustomerType", DbType.Byte, isales.CustomerType);
            db.AddInParameter(dbCommand, "@FK_Customer", DbType.Int64, isales.FK_Customer);
            db.AddInParameter(dbCommand, "@CustomerName", DbType.String, isales.CustomerName);
            db.AddInParameter(dbCommand, "@CardNumber", DbType.String, isales.CardNumber);
            db.AddInParameter(dbCommand, "@BillTotal", DbType.Decimal, isales.BillTotal);
            db.AddInParameter(dbCommand, "@Discount", DbType.Decimal, isales.DiscountTest);
            db.AddInParameter(dbCommand, "@RountOff", DbType.Decimal, isales.RountOff);
            db.AddInParameter(dbCommand, "@NetAmount", DbType.Decimal, isales.NetAmount);
            db.AddInParameter(dbCommand, "@PayAmount", DbType.Decimal, isales.PayAmount);
            db.AddInParameter(dbCommand, "@FK_Counter", DbType.Int64, isales.FK_Counter);
            db.AddInParameter(dbCommand, "@Fk_Shift", DbType.Int64, isales.Fk_Shift);
            db.AddInParameter(dbCommand, "@FK_Client", DbType.Int64, isales.FK_Client);
            db.AddInParameter(dbCommand, "@CashAmount", DbType.Decimal, isales.CashAmount);
            db.AddInParameter(dbCommand, "@CrdAmount", DbType.Decimal, isales.CrdAmount);
            db.AddInParameter(dbCommand, "@ChqAmount", DbType.Decimal, isales.ChqAmount);
            db.AddInParameter(dbCommand, "@ChqBank", DbType.String, isales.ChqBank);
            db.AddInParameter(dbCommand, "@ChqRemarks", DbType.String, isales.ChqRemarks);
            db.AddInParameter(dbCommand, "@Remarks", DbType.String, isales.Remarks);
            db.AddInParameter(dbCommand, "@UserCode", DbType.String, isales.UserCode);
            db.AddInParameter(dbCommand, "@CrdNumber", DbType.String, isales.CrdNumber);
            db.AddInParameter(dbCommand, "@CrdSlipNo", DbType.String, isales.CrdSlipNo);
            db.AddInParameter(dbCommand, "@CrdBank", DbType.String, isales.CrdBank);
            db.AddInParameter(dbCommand, "@CrdRemarks", DbType.String, isales.CrdRemarks);
            db.AddInParameter(dbCommand, "@ChqNumber", DbType.String, isales.ChqNumber);
            db.AddInParameter(dbCommand, "@ChqAccount", DbType.String, isales.ChqAccount);
            db.AddInParameter(dbCommand, "@EnteredOn", DbType.DateTime, isales.EnteredOn);
            db.AddInParameter(dbCommand, "@ChqDate", DbType.DateTime, isales.ChqDate);
            db.AddInParameter(dbCommand, "@Credit", DbType.Boolean, isales.Credit);
            db.AddInParameter(dbCommand, "@Id_Hold", DbType.Int64, isales.FK_Hold);
            db.AddInParameter(dbCommand, "@XmlGiftDetails", DbType.String, isales.XmlGiftDetails);
            db.AddInParameter(dbCommand, "@XmlDiscDetails", DbType.String, isales.XmlDiscDetails);
            db.AddInParameter(dbCommand, "@XmlSales", DbType.String, isales.XmlSalesDetails);
            db.AddInParameter(dbCommand, "@BillDiscount", DbType.Decimal, isales.Discount);
            db.AddInParameter(dbCommand, "@NegativeStock", DbType.Int16, isales.NegativeStock);
            db.AddInParameter(dbCommand, "@FK_RecoveryType", DbType.Int64, isales.FK_RecoveryType);
            db.AddInParameter(dbCommand, "@InstallmentPeriod", DbType.Int32, isales.InstallmentPeriod);
            db.AddInParameter(dbCommand, "@InstallmentDuration", DbType.Int32, isales.InstallmentDuration);
            db.AddInParameter(dbCommand, "@InstallmentAmount", DbType.Decimal, isales.InstallmentAmount);
            db.AddInParameter(dbCommand, "@RecoveryDate", DbType.DateTime, isales.RecoveryDate);
            db.AddInParameter(dbCommand, "@SubsAmount", DbType.Decimal, isales.SubsAmount);
            db.AddInParameter(dbCommand, "@SubsNumber", DbType.String, isales.SubsNumber);
            db.AddInParameter(dbCommand, "@SubsPercentage", DbType.Decimal, isales.SubsPercentage);
            db.AddInParameter(dbCommand, "@SubsDate", DbType.DateTime, isales.SubsDate);
            db.AddInParameter(dbCommand, "@SubsAuthority", DbType.String, isales.SubsAuthority);
            db.AddInParameter(dbCommand, "@SubsRemarks", DbType.String, isales.SubsRemarks);
            db.AddInParameter(dbCommand, "@InterState", DbType.Boolean, isales.InterState);
            db.AddInParameter(dbCommand, "@RedeemPoint", DbType.Decimal, isales.RedeemPoint);
            db.AddInParameter(dbCommand, "@RedeemAmount", DbType.Decimal, isales.RedeemAmount);
            db.AddInParameter(dbCommand, "@RedeemDiscount", DbType.Decimal, isales.RedeemDiscount);
            db.AddInParameter(dbCommand, "@GSTIN", DbType.String, isales.GSTIN);
            db.AddInParameter(dbCommand, "@TransactionID", DbType.String, isales.TransID);
            db.AddInParameter(dbCommand, "@XmlOthercharges", DbType.Xml, isales.XmlOtherDetails);
            db.AddInParameter(dbCommand, "@FK_Vehicle", DbType.Int64, isales.FK_Vehicle);
          //  db.AddInParameter(dbCommand, "@Address1", DbType.String, isales.Address1);
            db.AddInParameter(dbCommand, "@DeliveryAddress", DbType.String, isales.Address2);
            db.AddInParameter(dbCommand, "@LicennseNo", DbType.String, isales.LicennseNo);
            db.AddInParameter(dbCommand, "@DriverName", DbType.String, isales.DriverName);
            db.AddInParameter(dbCommand, "@VehicleNo", DbType.String, isales.VehicleNo);
            db.AddInParameter(dbCommand, "@TermsOfDelivery", DbType.String, isales.RBForm);
            db.AddInParameter(dbCommand, "@DrcDet", DbType.Boolean, isales.DrcDet);
            db.AddInParameter(dbCommand, "@Fk_State", DbType.Int64, isales.FK_State);
            db.AddInParameter(dbCommand, "@Fk_BuyState", DbType.Int64, isales.FK_BuyState);
            db.AddInParameter(dbCommand, "@BuyGSTIN", DbType.String, isales.BuyGSTIN);
            db.AddInParameter(dbCommand, "@BuyPAN", DbType.String, isales.BuyPAN);
            db.AddInParameter(dbCommand, "@ExcludeTax", DbType.Boolean, isales.ExcludeTax);

            try
            {
                using (IDataReader dataReader = db.ExecuteReader(dbCommand))
                {
                    while (dataReader.Read())
                    {
                       

                        isales.ReturnId = dataReader["ID_Sales"].ToString() == null ? 0 : Convert.ToInt64(dataReader["ID_Sales"].ToString());
                        isales.ReturnName = Convert.ToString(dataReader["Name"].ToString());

                    }
                }
                return isales;
            }
            catch (SqlException e)
            {
                throw;
            }

        }
    
       
      
      
       

        public Int32 PayInOutDelete(ISales isales)
        {
            Database db = DatabaseFactory.CreateDatabase("WebPharma");
            string sqlCommand = "ProPayInOutDelete";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

            db.AddInParameter(dbCommand, "@ID_AccountheadSubTransaction", DbType.Int64, isales.MasterId);
            db.AddInParameter(dbCommand, "@UserCode", DbType.String, isales.UserCode);
            db.AddInParameter(dbCommand, "@FK_Client", DbType.Int64, isales.FK_Client);

            try
            {
                return Convert.ToInt32(db.ExecuteScalar(dbCommand));
            }
            catch (SqlException e)
            {
                // UpdateErrorLog(istock, e);
                return 0;
            }
        }
        public Int32 UpdateHoldSales(ISales isales)
        {
            Database db = DatabaseFactory.CreateDatabase("WebPharma");
            string sqlCommand = "ProHoldUpdate";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
            db.AddInParameter(dbCommand, "@UserAction", DbType.Byte, isales.UserAction);
            db.AddInParameter(dbCommand, "@ID_Hold", DbType.Int64, isales.MasterId);
            db.AddInParameter(dbCommand, "@FK_Store", DbType.Int64, isales.FK_Store);
            db.AddInParameter(dbCommand, "@FK_BillType", DbType.Int64, isales.FK_BillType);

            db.AddInParameter(dbCommand, "@BillDate", DbType.DateTime, isales.BillDate);
            db.AddInParameter(dbCommand, "@CustomerType", DbType.Byte, isales.CustomerType);
            db.AddInParameter(dbCommand, "@FK_Customer", DbType.Int64, isales.FK_Customer);
            db.AddInParameter(dbCommand, "@CustomerName", DbType.String, isales.CustomerName);
            db.AddInParameter(dbCommand, "@GSTIN", DbType.String, isales.GSTIN);
            db.AddInParameter(dbCommand, "@BillTotal", DbType.Decimal, isales.BillTotal);
            db.AddInParameter(dbCommand, "@Discount", DbType.Decimal, isales.Discount);
            db.AddInParameter(dbCommand, "@RountOff", DbType.Decimal, isales.RountOff);
            db.AddInParameter(dbCommand, "@NetAmount", DbType.Decimal, isales.NetAmount);
            db.AddInParameter(dbCommand, "@FK_Counter", DbType.Int64, isales.FK_Counter);
            db.AddInParameter(dbCommand, "@Fk_Shift", DbType.Int64, isales.Fk_Shift);
            db.AddInParameter(dbCommand, "@Remarks", DbType.String, isales.Remarks);
            db.AddInParameter(dbCommand, "@UserCode", DbType.String, isales.UserCode);
            db.AddInParameter(dbCommand, "@EnteredOn", DbType.DateTime, isales.EnteredOn);
            db.AddInParameter(dbCommand, "@FK_Client", DbType.Int64, isales.FK_Client);
            db.AddInParameter(dbCommand, "@Invoice", DbType.Byte, isales.Invoice);
            db.AddInParameter(dbCommand, "@StockUpdation", DbType.Byte, isales.StockUpdation);
            db.AddInParameter(dbCommand, "@FK_Sales", DbType.Int64, isales.FK_Sales);
            db.AddInParameter(dbCommand, "@PriceType", DbType.Int16, isales.PriceType);


            db.AddInParameter(dbCommand, "@XmlHold", DbType.String, isales.XmlSalesDetails);




            try
            {
                return Convert.ToInt32(db.ExecuteScalar(dbCommand));
            }
            catch (SqlException e)
            {
                // UpdateErrorLog(istock, e);
                return 0;
            }
        }
        public Int32 CounterCloseUpdatePOS(ISales isales)
        {
            Database db = DatabaseFactory.CreateDatabase("WebPharma");
            string sqlCommand = "proCounterCloseUpdate";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
            db.AddInParameter(dbCommand, "@UserAction", DbType.Byte, isales.UserAction);
            db.AddInParameter(dbCommand, "@ID_CounterClose", DbType.Int64, isales.MasterId);
            db.AddInParameter(dbCommand, "@FK_Store", DbType.Int64, isales.FK_Store);
            db.AddInParameter(dbCommand, "@FK_Shift", DbType.Int64, isales.Fk_Shift);
            db.AddInParameter(dbCommand, "@FK_Counter", DbType.Int64, isales.FK_Counter);
            db.AddInParameter(dbCommand, "@Date", DbType.DateTime, isales.BillDate);
            db.AddInParameter(dbCommand, "@Opening", DbType.Decimal, isales.OpeningCounterClose);

            db.AddInParameter(dbCommand, "@SalesAmount", DbType.Decimal, isales.SalesAmountCounterClose);
            db.AddInParameter(dbCommand, "@Remittance", DbType.Decimal, isales.CloseCounterRemittance);
            db.AddInParameter(dbCommand, "@PayInAmount", DbType.Decimal, isales.PayInAmountCounterClose);
            db.AddInParameter(dbCommand, "@PayOutAmount", DbType.Decimal, isales.PayOutAmountCounterClose);
            db.AddInParameter(dbCommand, "@Closing", DbType.Decimal, isales.ClosingAmountCounterClose);

            db.AddInParameter(dbCommand, "@UserCode", DbType.String, isales.UserCode);
            db.AddInParameter(dbCommand, "@FK_Client", DbType.Int64, isales.FK_Client);
            db.AddInParameter(dbCommand, "@XmlCloseCounterDetails", DbType.String, isales.XmlCloseCounterDetails);
            db.AddInParameter(dbCommand, "@SalesReturn", DbType.Decimal, isales.SalesRetAmountCounterClose);

            try
            {
                return Convert.ToInt32(db.ExecuteScalar(dbCommand));
            }
            catch (SqlException e)
            {
                // UpdateErrorLog(istock, e);
                return 0;
            }
        }
        public Int32 CounterOpenUpdatePOS(ISales isales)
        {
            Database db = DatabaseFactory.CreateDatabase("WebPharma");
            string sqlCommand = "proCounterOpenUpdate";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
            db.AddInParameter(dbCommand, "@UserAction", DbType.Byte, isales.UserAction);
            db.AddInParameter(dbCommand, "@FK_Store", DbType.Int64, isales.FK_Store);
            db.AddInParameter(dbCommand, "@FK_Shift", DbType.Int64, isales.Fk_Shift);
            db.AddInParameter(dbCommand, "@FK_Counter", DbType.Int64, isales.FK_Counter);
            db.AddInParameter(dbCommand, "@Remittance", DbType.Decimal, isales.CloseCounterRemittance);
            db.AddInParameter(dbCommand, "@UserCode", DbType.String, isales.UserCode);
            db.AddInParameter(dbCommand, "@FK_Client", DbType.Int64, isales.FK_Client);

            try
            {
                return Convert.ToInt32(db.ExecuteScalar(dbCommand));
            }
            catch (SqlException e)
            {
                return 0;
            }
        }
     


     
        public DataTable SelectRecoveryType(ISales isales)
        {
            DataTable dtblSales = new DataTable();
            Database db = DatabaseFactory.CreateDatabase("WebPharma");
            string sqlCommand = "proRecoveryTypeSelect";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
            try
            {
                dtblSales = db.ExecuteDataSet(dbCommand).Tables[0];
                return dtblSales;
            }
            catch (SqlException e)
            {
                //UpdateErrorLog(istock, e);
                throw e;
            }
        }

        public DataTable SelectBillTypeWhenCredit(ISales isales)
        {
            DataTable dtblSales = new DataTable();
            Database db = DatabaseFactory.CreateDatabase("WebPharma");
            string sqlCommand = "proBillTypeWhenCreditPOS";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
            //db.AddInParameter(dbCommand, "@ID_Items", DbType.Int64, Convert.ToInt64(isales.MasterId));
            db.AddInParameter(dbCommand, "@FK_Client", DbType.Int64, isales.FK_Client);
            db.AddInParameter(dbCommand, "@Credit", DbType.Boolean, isales.Credit);

            try
            {
                dtblSales = db.ExecuteDataSet(dbCommand).Tables[0];
                return dtblSales;
            }
            catch (SqlException e)
            {
                //UpdateErrorLog(istock, e);
                throw e;
            }
        }
        public Int32 CounterCloseDeletePOS(ISales isales)
        {
            Database db = DatabaseFactory.CreateDatabase("WebPharma");
            string sqlCommand = "proCounterCloseDeletePOS";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);


            db.AddInParameter(dbCommand, "@FK_Store", DbType.Int64, isales.FK_Store);
            db.AddInParameter(dbCommand, "@FK_Shift", DbType.Int64, isales.Fk_Shift);
            db.AddInParameter(dbCommand, "@FK_Counter", DbType.Int64, isales.FK_Counter);
            db.AddInParameter(dbCommand, "@Date", DbType.DateTime, isales.BillDate);
            db.AddInParameter(dbCommand, "@UserCode", DbType.Int64, Convert.ToInt64(isales.UserCode));

            db.AddInParameter(dbCommand, "@FK_Client", DbType.Int64, isales.FK_Client);


            try
            {
                return Convert.ToInt32(db.ExecuteScalar(dbCommand));
            }
            catch (SqlException e)
            {
                // UpdateErrorLog(istock, e);
                return 0;
            }
        }

       

      
        public DataTable SelectCounterCloseCheckingPOS(ISales isales)
        {
            DataTable dtblSales = new DataTable();
            Database db = DatabaseFactory.CreateDatabase("WebPharma");
            string sqlCommand = "proSelectCounterCloseCheckingPOS";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

            db.AddInParameter(dbCommand, "@FK_Store", DbType.Int64, isales.FK_Store);
            db.AddInParameter(dbCommand, "@FK_Shift", DbType.Int64, isales.Fk_Shift);
            db.AddInParameter(dbCommand, "@FK_Counter", DbType.Int64, isales.FK_Counter);
            db.AddInParameter(dbCommand, "@FK_Client", DbType.Int32, isales.FK_Client);

            try
            {
                dtblSales = db.ExecuteDataSet(dbCommand).Tables[0];
                return dtblSales;
            }
            catch (SqlException e)
            {
                //UpdateErrorLog(isales, e);
                throw e;
            }
        }

   
       
      
      
       
      
        public Int32 CounterOpenClose(ISales isales)
        {
            Database db = DatabaseFactory.CreateDatabase("WebPharma");
            string sqlCommand = "ProSalesCounterOpen";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
            db.AddInParameter(dbCommand, "@FK_Client", DbType.Int64, isales.FK_Client);
            db.AddInParameter(dbCommand, "@FK_Counter", DbType.Int64, isales.FK_Counter);
            db.AddInParameter(dbCommand, "@OpeningCounterClose", DbType.Decimal, isales.OpeningCounterClose);
            db.AddInParameter(dbCommand, "@CashAmount", DbType.Decimal, isales.CashAmount);
            db.AddInParameter(dbCommand, "@UserCode", DbType.String, isales.UserCode);
            db.AddInParameter(dbCommand, "@Status", DbType.String, isales.Status);
            db.AddInParameter(dbCommand, "@FK_Store", DbType.Int64, isales.FK_Store);
            try
            {
                return Convert.ToInt32(db.ExecuteScalar(dbCommand));
            }
            catch (SqlException e)
            {
                // UpdateErrorLog(iPriceConversion, e);
                return 0;
            }
        }
        public DataTable SelectOpeningPayInOutPOS(ISales isales)
        {
            DataTable dtblStock = new DataTable();
            Database db = DatabaseFactory.CreateDatabase("WebPharma");
            string sqlCommand = "proSelectOpeningPayInOutPOS";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

            db.AddInParameter(dbCommand, "@FK_Client", DbType.Int32, isales.FK_Client);
            db.AddInParameter(dbCommand, "@FK_Shift", DbType.Int32, isales.Fk_Shift);
            db.AddInParameter(dbCommand, "@FK_Store", DbType.Int32, isales.FK_Store);
            db.AddInParameter(dbCommand, "@FK_Counter", DbType.Int32, isales.FK_Counter);

            try
            {
                dtblStock = db.ExecuteDataSet(dbCommand).Tables[0];
                return dtblStock;
            }
            catch (SqlException e)
            {
                //UpdateErrorLog(istock, e);
                throw e;
            }
        }
        public DataTable SelectOpeningCounter(ISales isales)
        {
            DataTable dtblStock = new DataTable();
            Database db = DatabaseFactory.CreateDatabase("WebPharma");
            string sqlCommand = "proSelectOpeningCounter";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

            db.AddInParameter(dbCommand, "@FK_Client", DbType.Int32, isales.FK_Client);
            db.AddInParameter(dbCommand, "@FK_Shift", DbType.Int32, isales.Fk_Shift);
            db.AddInParameter(dbCommand, "@FK_Store", DbType.Int32, isales.FK_Store);
            db.AddInParameter(dbCommand, "@FK_Counter", DbType.Int32, isales.FK_Counter);

            try
            {
                dtblStock = db.ExecuteDataSet(dbCommand).Tables[0];
                return dtblStock;
            }
            catch (SqlException e)
            {
                //UpdateErrorLog(istock, e);
                throw e;
            }
        }
        public DataTable SelectAllDenominations(ISales isales)
        {
            DataTable dtblStock = new DataTable();
            Database db = DatabaseFactory.CreateDatabase("WebPharma");
            string sqlCommand = "proSelectAllDenominationsPOS";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
            db.AddInParameter(dbCommand, "@FK_Client", DbType.Int32, isales.FK_Client);
            try
            {
                dtblStock = db.ExecuteDataSet(dbCommand).Tables[0];
                return dtblStock;
            }
            catch (SqlException e)
            {
                //UpdateErrorLog(istock, e);
                throw e;
            }
        }
        public String GetScalarValue(ISales isales)
        {
            string sqlCommand = "";
            DbCommand dbCommand;
            DataTable dt = new DataTable();
            Database db = DatabaseFactory.CreateDatabase("WebPharma");
            sqlCommand = "proCmnPopulateData";
            dbCommand = db.GetStoredProcCommand(sqlCommand);
            db.AddInParameter(dbCommand, "TableName", DbType.String, isales.TableName);
            db.AddInParameter(dbCommand, "ListField", DbType.String, isales.ListFields);
            db.AddInParameter(dbCommand, "SortField", DbType.String, isales.SortFields);
            db.AddInParameter(dbCommand, "Criteria", DbType.String, isales.Criteria);
            try
            {
                return Convert.ToString(db.ExecuteScalar(dbCommand));
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public int SelectSaleExistForCustomerDiscount(ISales isales)
        {

            Database db = DatabaseFactory.CreateDatabase("WebPharma");
            string sqlCommand = "proSaleExistForCustomerDiscount";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
            db.AddInParameter(dbCommand, "@FK_Customer", DbType.Int64, isales.MasterId);
            db.AddInParameter(dbCommand, "@Date", DbType.DateTime, isales.BillDate);
            db.AddInParameter(dbCommand, "@Amount", DbType.Decimal, isales.BillAmount);

            try
            {
                return Convert.ToInt32(db.ExecuteScalar(dbCommand));
            }
            catch (SqlException e)
            {
                throw e;
            }
        }

        public DataTable SelectSalesAll(ISales  iSales )
        {
            DataTable dtbl = new DataTable();
            Database db = DatabaseFactory.CreateDatabase("WebPharma");
            string sqlCommand = "proSalesSelect";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
            db.AddInParameter(dbCommand, "@FK_Customer", DbType.Int64, iSales.FK_Customer);
            db.AddInParameter(dbCommand, "@FK_Store", DbType.Int64, iSales.FK_Store);
            db.AddInParameter(dbCommand, "@FK_Counter", DbType.Int64, iSales.FK_Counter);
            db.AddInParameter(dbCommand, "@PageIndex", DbType.Int32, iSales.PageIndex);
            db.AddInParameter(dbCommand, "@PageSize", DbType.Int32, iSales.PageSize);
            db.AddInParameter(dbCommand, "@FK_Client", DbType.Int64, iSales.FK_Client);
            db.AddInParameter(dbCommand, "@fromDate", DbType.DateTime, iSales.FromDate);
            db.AddInParameter(dbCommand, "@toDate", DbType.DateTime, iSales.ToDate);
            db.AddInParameter(dbCommand, "@ID_Sales", DbType.Int64, iSales.MasterId);
            db.AddInParameter(dbCommand, "@Fk_User", DbType.String, iSales.UserCode);

            try
            {
                dtbl = db.ExecuteDataSet(dbCommand).Tables[0];
                return dtbl;
            }
            catch (SqlException e)
            {

                throw e;
            }
        }


        public ISales SelectSales(ISales iSales)
        {
            Database db = DatabaseFactory.CreateDatabase("WebPharma");
            string sqlCommand = "proSalesSelect";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
            db.AddInParameter(dbCommand, "@FK_Customer", DbType.Int64, iSales.FK_Customer);
            db.AddInParameter(dbCommand, "@FK_Store", DbType.Int64, iSales.FK_Store);
            db.AddInParameter(dbCommand, "@FK_Counter", DbType.Int64, iSales.FK_Counter);
            db.AddInParameter(dbCommand, "@PageIndex", DbType.Int32, iSales.PageIndex);
            db.AddInParameter(dbCommand, "@PageSize", DbType.Int32, iSales.PageSize);
            db.AddInParameter(dbCommand, "@FK_Client", DbType.Int64, iSales.FK_Client);
            db.AddInParameter(dbCommand, "@fromDate", DbType.DateTime, iSales.FromDate);
            db.AddInParameter(dbCommand, "@toDate", DbType.DateTime, iSales.ToDate);
            db.AddInParameter(dbCommand, "ID_Sales", DbType.Int64, iSales.MasterId);
            try
            {
                using (IDataReader dataReader = db.ExecuteReader(dbCommand))
                {
                    while (dataReader.Read())
                    {
                        iSales.BillNo = Convert.ToInt64(dataReader["BillNo"].ToString());
                        iSales.BillDate = Convert.ToDateTime(dataReader["BillDate"].ToString());
                        iSales.FK_Customer = Convert.ToInt64(dataReader["FK_Customer"].ToString());
                        iSales.FK_Store = Convert.ToInt64(dataReader["FK_Store"].ToString());
                        iSales.BillTotal = Convert.ToDecimal(dataReader["BillTotal"].ToString());
                        iSales.Discount = Convert.ToDecimal(dataReader["Discount"].ToString());
                        iSales.RountOff = Convert.ToDecimal(dataReader["RoundOff"].ToString());
                        iSales.NetAmount = Convert.ToDecimal(dataReader["NetAmount"].ToString());
                        iSales.Remarks = dataReader["Remarks"].ToString();
                        iSales.FK_Sales = Convert.ToInt64(dataReader["ID_Sales"].ToString());
                        iSales.Credit = Convert.ToBoolean(dataReader["Credit"].ToString());
                        iSales.CustomerName = dataReader["CustomerName"].ToString();
                        iSales.FK_Counter = Convert.ToInt64(dataReader["FK_Counter"].ToString());
                        iSales.GSTIN = Convert.ToString(dataReader["GSTTIN"].ToString());
                        iSales.InterState = Convert.ToBoolean(dataReader["InterState"].ToString());
                      

                    }
                }
                return iSales;
            }
            catch (SqlException e)
            {

                throw e;
            }

        }


    }
}
