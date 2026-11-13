using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using WebPharma.Interface.WebPharma;
using System.Data.SqlClient;
using System.Data.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;

namespace WebPharma.DataAccess.WebPharma
{
   public class DalDayEnd
    {
     public Int32 UpdateDayEnd(IDayEnd iDayEnd)
     {
         Database db = DatabaseFactory.CreateDatabase("WebPharma");
         string sqlCommand = "proDayEndProcessWebPharma";
         DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

         //db.AddInParameter(dbCommand, "@UserAction", DbType.Byte, iDayEnd.UserAction);
         db.AddInParameter(dbCommand, "@DayDate", DbType.DateTime, iDayEnd.DbDate);
         db.AddInParameter(dbCommand, "@BranchCode", DbType.Int16, iDayEnd.BranchCode);
         db.AddInParameter(dbCommand, "@UserCode", DbType.String, iDayEnd.UserCode);
         db.AddInParameter(dbCommand, "@FK_Machine", DbType.String, iDayEnd.MachineCode);

         db.AddInParameter(dbCommand, "@OnLineDaybook", DbType.Boolean, 0);
         try
         {
             return Convert.ToInt32(db.ExecuteScalar(dbCommand));
         }
         catch (SqlException e)
         {
             // UpdateErrorLog(iaccountheadsubtransaction, e);
             return 0;
         }

     }

     public DataSet SelectGriddata(IDayEnd iDayEnd)
     {
         DataSet dtbl = new DataSet();
         Database db = DatabaseFactory.CreateDatabase("WebPharma");
         string sqlCommand = "ProTransactiondetails";
         DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
         db.AddInParameter(dbCommand, "@FK_Store", DbType.Int64, iDayEnd.FK_Store);
         db.AddInParameter(dbCommand, "@FromDate", DbType.DateTime, iDayEnd.FromDate);
         db.AddInParameter(dbCommand, "@FK_Client", DbType.Int64, iDayEnd.FK_Client);

         try
         {
             dtbl = db.ExecuteDataSet(dbCommand);
             return dtbl;
         }
         catch (SqlException e)
         {

             throw e;
         }
     }
     public DataSet LiabilityDetails(IDayEnd iDayEnd)
     {
         DataSet dtbl = new DataSet();
         Database db = DatabaseFactory.CreateDatabase("WebPharma");
         string sqlCommand = "ProDayendProcessGriddetails";
         DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
         db.AddInParameter(dbCommand, "@FK_Store", DbType.Int64, iDayEnd.FK_Store);
         db.AddInParameter(dbCommand, "@FromDate", DbType.DateTime, iDayEnd.FromDate);
         db.AddInParameter(dbCommand, "@ToDate", DbType.DateTime, iDayEnd.FromDate);
         db.AddInParameter(dbCommand, "@FK_Client", DbType.Int64, iDayEnd.FK_Client);

         try
         {
             dtbl = db.ExecuteDataSet(dbCommand);
             return dtbl;
         }
         catch (SqlException e)
         {

             throw e;
         }
     }
     public DataSet Payementreceiptdetails(IDayEnd iDayEnd)
     {
         DataSet dtbl = new DataSet();
         Database db = DatabaseFactory.CreateDatabase("WebPharma");
         string sqlCommand = "ProPaymentReceiptdetails";
         DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
         db.AddInParameter(dbCommand, "@FromDate", DbType.DateTime, iDayEnd.FromDate);
         db.AddInParameter(dbCommand, "@FK_Store", DbType.Int64, iDayEnd.FK_Store);
         try
         {
             dtbl = db.ExecuteDataSet(dbCommand);
             return dtbl;
         }
         catch (SqlException e)
         {

             throw e;
         }
     }
     public Int32 UpdateDayEndData(IDayEnd iDayEnd)
     {
         Database db = DatabaseFactory.CreateDatabase("WebPharma");
         string sqlCommand = "ProDayendupdate";
         DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
         db.AddInParameter(dbCommand, "@UserAction", DbType.Byte, iDayEnd.UserAction);
         db.AddInParameter(dbCommand, "@ID_DayClose", DbType.Int64, iDayEnd.MasterID);
         db.AddInParameter(dbCommand, "@FK_Store", DbType.Int64, iDayEnd.FK_Store);
         db.AddInParameter(dbCommand, "@DayDate", DbType.DateTime, iDayEnd.FromDate);
         db.AddInParameter(dbCommand, "@Opening", DbType.Decimal, iDayEnd.openingbalance);
         db.AddInParameter(dbCommand, "@Receipt", DbType.Decimal, iDayEnd.Receipt);
         db.AddInParameter(dbCommand, "@Payment", DbType.Decimal, iDayEnd.Payment);
         db.AddInParameter(dbCommand, "@Closing", DbType.Decimal, iDayEnd.Closingbalance);
         db.AddInParameter(dbCommand, "@PurchaseAmount", DbType.Decimal, iDayEnd.PurchaseAmount);
         db.AddInParameter(dbCommand, "@PurchaseLiability", DbType.Decimal, iDayEnd.PurchaseLiability);
         db.AddInParameter(dbCommand, "@PReturnAmount", DbType.Decimal, iDayEnd.PReturnAmount);
         db.AddInParameter(dbCommand, "@PReturnLiability", DbType.Decimal, iDayEnd.PReturnAmount);
         db.AddInParameter(dbCommand, "@SalesAmount", DbType.Decimal, iDayEnd.SalesAmount);
         db.AddInParameter(dbCommand, "@SalesLiability", DbType.Decimal, iDayEnd.SalesLiability);
         db.AddInParameter(dbCommand, "@SReturnAmount", DbType.Decimal, iDayEnd.SReturnAmount);
         db.AddInParameter(dbCommand, "@SReturnLiability", DbType.Decimal, iDayEnd.SReturnLiability);
         db.AddInParameter(dbCommand, "@DamageStock", DbType.Decimal, iDayEnd.DamageStock);
         db.AddInParameter(dbCommand, "@IndentFrom", DbType.Decimal, iDayEnd.IndentFrom);
         db.AddInParameter(dbCommand, "@IndentTo", DbType.Decimal, iDayEnd.IndentTo);
         db.AddInParameter(dbCommand, "@StockValue", DbType.Decimal, iDayEnd.Stockvalue);
         db.AddInParameter(dbCommand, "@UserCode", DbType.String, iDayEnd.UserCode);
         db.AddInParameter(dbCommand, "@FK_Client", DbType.Int64, iDayEnd.FK_Client);
         db.AddInParameter(dbCommand, "@MasterId", DbType.Int16, iDayEnd.MasterID);

         try
         {
             return Convert.ToInt32(db.ExecuteScalar(dbCommand));
         }
         catch (SqlException e)
         {
             // UpdateErrorLog(iaccountheadsubtransaction, e);
             return 0;
         }
     }
    }
}
