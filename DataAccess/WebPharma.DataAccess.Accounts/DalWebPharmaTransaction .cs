/*----------------------------------- DataAccess Layer Class -----------------------------------------------------------------------------------------------------------------------
Purpose    : 
Author     :bibin  tc
Copyright  : Perfect Software Solutions (Clt) Pvt Ltd
Created on : 03/05/2018
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
using WebPharma.Interface.Accounts;

using System.Collections;

namespace WebPharma.DataAccess.Accounts
{
  public  class DalWebPharmaTransaction
    {
      public Int32 UpdateWebPharmaTransaction(IWebPharmaTransaction iIWebPharmaTransactions)
      {

          Database db = DatabaseFactory.CreateDatabase("WebPharma");
          string sqlCommand = "proWebPharmaTransactionupdate";
          DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
          db.AddInParameter(dbCommand, "@UserAction", DbType.Byte, iIWebPharmaTransactions.UserAction);
          db.AddInParameter(dbCommand, "@TransDate", DbType.DateTime, iIWebPharmaTransactions.TransDate);
          db.AddInParameter(dbCommand, "@TransType", DbType.String, iIWebPharmaTransactions.TransType);
          db.AddInParameter(dbCommand, "@TransferTo", DbType.String, iIWebPharmaTransactions.TransferTo);
          db.AddInParameter(dbCommand, "@Type", DbType.Int16, iIWebPharmaTransactions.Type);
          db.AddInParameter(dbCommand, "@FK_Name", DbType.Int64, iIWebPharmaTransactions.FK_Name);
          db.AddInParameter(dbCommand, "@Amount", DbType.Decimal, iIWebPharmaTransactions.Amount);
          db.AddInParameter(dbCommand, "@Remarks", DbType.String, iIWebPharmaTransactions.Remarks);

          db.AddInParameter(dbCommand, "@FK_Client", DbType.Int64, iIWebPharmaTransactions.FK_Client);
          db.AddInParameter(dbCommand, "@UserCode", DbType.String, iIWebPharmaTransactions.UserCode);
          db.AddInParameter(dbCommand, "@EnteredOn", DbType.DateTime, iIWebPharmaTransactions.EnteredOn);
          db.AddInParameter(dbCommand, "@FK_Store", DbType.Int64, iIWebPharmaTransactions.FK_Store);



          db.AddInParameter(dbCommand, "@XmlTransfertToAcc", DbType.DateTime, iIWebPharmaTransactions.XmlTransfertToAcc);
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

      public DataTable SelectWebPharmaTransaction(IWebPharmaTransaction iIWebPharmaTransactions)
      {
          DataTable dbiIWebPharmaTransactions = new DataTable();
          Database db = DatabaseFactory.CreateDatabase("WebPharma");
          string sqlCommand = "proWebPharmaTransactionSelect";
          DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
          db.AddInParameter(dbCommand, "@ID_WebPharmaTransaction", DbType.Int64, iIWebPharmaTransactions.MasterId);
          db.AddInParameter(dbCommand, "@FK_Client", DbType.Int32, iIWebPharmaTransactions.FK_Client);
          db.AddInParameter(dbCommand, "@PageIndex", DbType.Int16, iIWebPharmaTransactions.PageIndex);
          db.AddInParameter(dbCommand, "@PageSize", DbType.Int16, iIWebPharmaTransactions.PageSize);
          //db.AddInParameter(dbCommand, "@EntryType", DbType.Int16, iIWebPharmaTransactions.EntryType);
          //  db.AddInParameter(dbCommand, "@VoucherNo", DbType.Int64, iIWebPharmaTransactions.VoucherNo);
         db.AddInParameter(dbCommand, "@FromDate", DbType.DateTime, iIWebPharmaTransactions.FromDate);
        db.AddInParameter(dbCommand, "@ToDate", DbType.DateTime, iIWebPharmaTransactions.ToDate);
          try
          {
              dbiIWebPharmaTransactions = db.ExecuteDataSet(dbCommand).Tables[0];
              return dbiIWebPharmaTransactions;
          }
          catch (SqlException e)
          {
              //UpdateErrorLog(iaccountheadsubtransaction, e);
              throw e;
          }
      }



      public IWebPharmaTransaction selectedWebPharmatransactions(IWebPharmaTransaction iIWebPharmaTransactions)
      {
          DataTable dbiIWebPharmaTransactions = new DataTable();
          Database db = DatabaseFactory.CreateDatabase("WebPharma");
          string sqlCommand = "proWebPharmaTransactionSelect";
          DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
          db.AddInParameter(dbCommand, "@ID_WebPharmaTransaction", DbType.Int64, iIWebPharmaTransactions.MasterId);
          db.AddInParameter(dbCommand, "@FK_Client", DbType.Int32, iIWebPharmaTransactions.FK_Client);
          db.AddInParameter(dbCommand, "@PageIndex", DbType.Int16, iIWebPharmaTransactions.PageIndex);
          db.AddInParameter(dbCommand, "@PageSize", DbType.Int16, iIWebPharmaTransactions.PageSize);
          //db.AddInParameter(dbCommand, "@EntryType", DbType.Int16, iIWebPharmaTransactions.EntryType);
          //  db.AddInParameter(dbCommand, "@VoucherNo", DbType.Int64, iIWebPharmaTransactions.VoucherNo);
          db.AddInParameter(dbCommand, "@FromDate", DbType.DateTime, iIWebPharmaTransactions.FromDate);
          db.AddInParameter(dbCommand, "@ToDate", DbType.DateTime, iIWebPharmaTransactions.ToDate);
          try
          {
              using (IDataReader dataReader = db.ExecuteReader(dbCommand))
              {
                  while (dataReader.Read())
                  {


                      iIWebPharmaTransactions.TransType = Convert.ToString(dataReader["TransType"].ToString());
                      iIWebPharmaTransactions.TransDate = Convert.ToDateTime(dataReader["TransDate"].ToString());
                      iIWebPharmaTransactions.Amount = Convert.ToDecimal(dataReader["Amount"].ToString());
                      iIWebPharmaTransactions.Remarks = Convert.ToString(dataReader["Remarks"].ToString());
                      iIWebPharmaTransactions.Type = Convert.ToByte(dataReader["Type"].ToString());
                      iIWebPharmaTransactions.Name = Convert.ToString(dataReader["Name"].ToString());
                      iIWebPharmaTransactions.TransferTo = Convert.ToString(dataReader["TransferTo"].ToString());
                      iIWebPharmaTransactions.FK_Name = Convert.ToInt64(dataReader["FK_Name"].ToString());

                  }
              }
          }
          catch (SqlException e)
          {
              throw e;
          }
          return iIWebPharmaTransactions;
      }

      public IWebPharmaTransaction SelectBalanceAmount(IWebPharmaTransaction iIWebPharmaTransactions)
      {
          Database db = DatabaseFactory.CreateDatabase("WebPharma");
          string sqlCommand = "proCheckBalanceAmounts";
          DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
          db.AddInParameter(dbCommand, "@TYPE", DbType.Int64, iIWebPharmaTransactions.Type);
          db.AddInParameter(dbCommand, "@FK_NAME", DbType.Int64, iIWebPharmaTransactions.FK_Name);
          db.AddInParameter(dbCommand, "@FK_Client", DbType.Int64, iIWebPharmaTransactions.FK_Client);
      
          try
          {
              using (IDataReader dataReader = db.ExecuteReader(dbCommand))
              {
                  while (dataReader.Read())
                  {
                      iIWebPharmaTransactions.Amount = Convert.ToDecimal(dataReader["BalanceAmount"].ToString());
                      iIWebPharmaTransactions.Type = Convert.ToByte(dataReader["type"].ToString());
                  }
              }
              return iIWebPharmaTransactions;
          }
          catch (SqlException e)
          {
              throw e;
          }
      }
    }
}
