/*----------------------------------- Data Access Layer Class -----------------------------------------------------------------------------------------------------------------------
Purpose    : 
Author     :Bibin TC
Copyright  : Perfect Software Solutions (Clt) Pvt Ltd
Created on : 03/05/2018
----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
MODIFICATIONS 
On			By			OMID
----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
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
   public  class DalIndentIssue
    {
       public Int32 UpdateIndentIssue(IIndentIssue ipurchase)
       {

           Database db = DatabaseFactory.CreateDatabase("WebPharma");
           string sqlCommand = "ProStockTransferIssue";
           DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
           db.AddInParameter(dbCommand, "@UserAction", DbType.Byte, ipurchase.UserAction);
           db.AddInParameter(dbCommand, "@ID_IndentIssue", DbType.Int64, ipurchase.MasterId);
           db.AddInParameter(dbCommand, "@FK_IndentRequest", DbType.Int64, ipurchase.FK_IndentRequest);
           db.AddInParameter(dbCommand, "@TransDate", DbType.DateTime, ipurchase.Date);
           db.AddInParameter(dbCommand, "@FK_FromStore", DbType.Int64, ipurchase.FK_FromStore);
           db.AddInParameter(dbCommand, "@FK_ToStore", DbType.Int64, ipurchase.FK_ToStore);
           db.AddInParameter(dbCommand, "@PriceTag", DbType.Int16, ipurchase.PriceTag);        
           db.AddInParameter(dbCommand, "@UserCode", DbType.String, ipurchase.UserCode);
           db.AddInParameter(dbCommand, "@EnteredOn", DbType.DateTime, ipurchase.EnteredOn);
           db.AddInParameter(dbCommand, "@FK_Client", DbType.Int64, ipurchase.FK_Client);
           db.AddInParameter(dbCommand, "@XmlIndentIssueItems", DbType.Xml, ipurchase.XmlPurchaseItems);
          

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

       public DataTable SelectIndentRequestItemsForIssue(IIndentIssue Ipurchase)
       {
           DataTable dtblIndentRequestItems = new DataTable();
           Database db = DatabaseFactory.CreateDatabase("WebPharma");
           string sqlCommand = "ProIndentRequestItemsSelectForIssue";
           DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
           db.AddInParameter(dbCommand, "@FK_IndentRequest", DbType.Int64, Ipurchase.FK_IndentRequest);
           db.AddInParameter(dbCommand, "@FK_FromStore", DbType.Int64, Ipurchase.FK_FromStore);
           db.AddInParameter(dbCommand, "@FK_Client", DbType.Int64, Ipurchase.FK_Client);
           try
           {
               dtblIndentRequestItems = db.ExecuteDataSet(dbCommand).Tables[0];
               return dtblIndentRequestItems;
           }
           catch (SqlException e)
           {
               //UpdateErrorLog(istock, e);
               throw e;
           }
       }
       public DataTable SelectIndentIssueItems(IIndentIssue Ipurchase)
       {
           DataTable dtblIndentRequestItems = new DataTable();
           Database db = DatabaseFactory.CreateDatabase("WebPharma");
           string sqlCommand = "ProStocktransferIssueItemsSelect";
           DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
           db.AddInParameter(dbCommand, "@FK_Indent", DbType.Int64, Ipurchase.MasterId);
           db.AddInParameter(dbCommand, "@FK_IndentRequest", DbType.Int64, Ipurchase.FK_IndentRequest); 
           db.AddInParameter(dbCommand, "@FK_Client", DbType.Int64, Ipurchase.FK_Client);
           try
           {
               dtblIndentRequestItems = db.ExecuteDataSet(dbCommand).Tables[0];
               return dtblIndentRequestItems;
           }
           catch (SqlException e)
           {
               //UpdateErrorLog(istock, e);
               throw e;
           }
       }
       public DataSet StockTransferIssueReceipt(long StatusId)
       {
           string sqlCommand = "";
           DbCommand dbCommand;
           DataSet ds = new DataSet();
           Database db = DatabaseFactory.CreateDatabase("WebPharma");
           sqlCommand = "ProStockTransferIssueReceipt";
           dbCommand = db.GetStoredProcCommand(sqlCommand);
           db.AddInParameter(dbCommand, "@FK_Indent", DbType.Int64, StatusId);
           try
           {
               ds = db.ExecuteDataSet(dbCommand);
               return ds;
           }
           catch (SqlException e)
           {
               throw;
           }

       }
       
       public DataTable SelectAllIndentIssue(IIndentIssue Ipurchase)
       {
           DataTable dtblIndentRequest = new DataTable();
           Database db = DatabaseFactory.CreateDatabase("WebPharma");
           string sqlCommand = "ProStockTransferIssueSelectAll";
           DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
           db.AddInParameter(dbCommand, "@ID_IndentIssue", DbType.Int64, Ipurchase.MasterId);
           db.AddInParameter(dbCommand, "@FK_Client", DbType.Int64, Ipurchase.FK_Client);
           db.AddInParameter(dbCommand, "@fromDate", DbType.DateTime, Ipurchase.FromDate);
           db.AddInParameter(dbCommand, "@toDate", DbType.DateTime, Ipurchase.ToDate);
           db.AddInParameter(dbCommand, "@PageIndex", DbType.Int16, Ipurchase.PageIndex);
           db.AddInParameter(dbCommand, "@PageSize", DbType.Int16, Ipurchase.PageSize);         
           db.AddInParameter(dbCommand, "@FK_Store", DbType.Int64, Ipurchase.FK_FromStore);
           try
           {
               dtblIndentRequest = db.ExecuteDataSet(dbCommand).Tables[0];
               return dtblIndentRequest;
           }
           catch (SqlException e)
           {

               throw e;
           }
       }


       public IIndentIssue SelectData(IIndentIssue Ipurchase)
       {
           Database db = DatabaseFactory.CreateDatabase("WebPharma");
           string sqlCommand = "ProStockTransferIssueSelect";
           DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
           db.AddInParameter(dbCommand, "@ID_Indent", DbType.Int64, Ipurchase.MasterId);
           db.AddInParameter(dbCommand, "@FK_Client", DbType.Int64, Ipurchase.FK_Client);
           db.AddInParameter(dbCommand, "@fromDate", DbType.DateTime, Ipurchase.FromDate);
           db.AddInParameter(dbCommand, "@toDate", DbType.DateTime, Ipurchase.ToDate);
           db.AddInParameter(dbCommand, "@PageIndex", DbType.Int16, Ipurchase.PageIndex);
           db.AddInParameter(dbCommand, "@PageSize", DbType.Int16, Ipurchase.PageSize);
           db.AddInParameter(dbCommand, "@FK_Store", DbType.Int64, Ipurchase.FK_FromStore);
           try
           {
               using (IDataReader dataReader = db.ExecuteReader(dbCommand))
               {
                   while (dataReader.Read())
                   {
                     
                    Ipurchase.Date = DateTime.Parse((dataReader["Date"].ToString()));                  
                    Ipurchase.FK_FromStore = Convert.ToInt64(dataReader["FK_FromStore"]);
                    Ipurchase.FK_ToStore = Convert.ToInt64(dataReader["FK_ToStore"]);
                    Ipurchase.Remarks = Convert.ToString(dataReader["Remarks"]);
                    Ipurchase.PostingId = Convert.ToInt64(dataReader["PostingId"].ToString());
                    Ipurchase.FK_IndentRequest =Convert.ToInt64(dataReader["FK_IndentRequest"].ToString());

                   }
               }
           }
           catch (SqlException ex)
           {
               throw ex;
           }
           return Ipurchase;
       }
   


    }
}
