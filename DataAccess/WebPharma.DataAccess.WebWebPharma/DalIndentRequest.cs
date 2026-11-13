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
   public  class DalIndentRequest
    {
       public Int32 UpdateIndentRequest(IIndentRequest IndentRequest)
       {

           Database db = DatabaseFactory.CreateDatabase("WebPharma");
           string sqlCommand = "ProIndentRequestUpdate";
           DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
           db.AddInParameter(dbCommand, "@UserAction", DbType.Byte, IndentRequest.UserAction);
           db.AddInParameter(dbCommand, "@ID_IndentRequest", DbType.Int64, IndentRequest.MasterId);
           db.AddInParameter(dbCommand, "@Date", DbType.DateTime, IndentRequest.InReqDate);
           db.AddInParameter(dbCommand, "@FK_Store", DbType.Int64, IndentRequest.FK_Store);
           db.AddInParameter(dbCommand, "@ToStore", DbType.Int64, IndentRequest.ToStore);
           db.AddInParameter(dbCommand, "@Remarks", DbType.String, IndentRequest.Remarks);
           db.AddInParameter(dbCommand, "@UserCode", DbType.String, IndentRequest.UserCode);
           db.AddInParameter(dbCommand, "@Cancelled", DbType.Boolean, IndentRequest.Cancelled);
           db.AddInParameter(dbCommand, "@Cancelleduser", DbType.String, IndentRequest.CancelledUser);
           db.AddInParameter(dbCommand, "@CancelledOn", DbType.DateTime, IndentRequest.CancelledOn);
           db.AddInParameter(dbCommand, "@FK_Client", DbType.Int64, IndentRequest.FK_Client);
           db.AddInParameter(dbCommand, "@XmlIndentRequestItems", DbType.Xml, IndentRequest.XmlIndentRequestItems);
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

       public Int32 UpdateIndentRequestStatusOnly(IIndentRequest ipurchase)
       {

           Database db = DatabaseFactory.CreateDatabase("WebPharma");
           string sqlCommand = "ProIndentRequestUpdateStatusOnly";
           DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
           db.AddInParameter(dbCommand, "@ID_IndentRequest", DbType.Int64, ipurchase.MasterId);
         
           db.AddInParameter(dbCommand, "@ClosedRemarks", DbType.String, ipurchase.Remarks);
           db.AddInParameter(dbCommand, "@UserCode", DbType.String, ipurchase.UserCode);
           db.AddInParameter(dbCommand, "@EnteredOn", DbType.DateTime, ipurchase.EnteredOn);
           db.AddInParameter(dbCommand, "@FK_Client", DbType.Int64, ipurchase.FK_Client);
        
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
       public DataTable SelectIntendRequestIDData(IIndentRequest Ipurchase)
       {
           DataTable dtblIndentRequest = new DataTable();
           Database db = DatabaseFactory.CreateDatabase("WebPharma");
           string sqlCommand = "proIndentRequestSelect";
           DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
           db.AddInParameter(dbCommand, "@ID_IndentRequest", DbType.Int64, Ipurchase.MasterId);
           db.AddInParameter(dbCommand, "@FK_Client", DbType.Int64, Ipurchase.FK_Client);
           db.AddInParameter(dbCommand, "@fromDate", DbType.DateTime, Ipurchase.FromDate);
           db.AddInParameter(dbCommand, "@toDate", DbType.DateTime, Ipurchase.ToDate);
           db.AddInParameter(dbCommand, "@PageIndex", DbType.Int16, Ipurchase.PageIndex);
           db.AddInParameter(dbCommand, "@PageSize", DbType.Int16, Ipurchase.PageSize);
          
           db.AddInParameter(dbCommand, "@FK_Store", DbType.Int64, Ipurchase.FK_Store);
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
           public IIndentRequest SelectData(IIndentRequest IIndent)
           {
               Database db = DatabaseFactory.CreateDatabase("WebPharma");
               string sqlCommand = "proIndentRequestSelect";
               DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
               db.AddInParameter(dbCommand, "@ID_IndentRequest", DbType.Int64, IIndent.MasterId);
               db.AddInParameter(dbCommand, "@FK_Client", DbType.Int64, IIndent.FK_Client);
               db.AddInParameter(dbCommand, "@fromDate", DbType.DateTime, IIndent.FromDate);
               db.AddInParameter(dbCommand, "@toDate", DbType.DateTime, IIndent.ToDate);
               db.AddInParameter(dbCommand, "@FK_Store", DbType.Int64, IIndent.FK_Store);
               try
               {
                   using (IDataReader dataReader = db.ExecuteReader(dbCommand))
                   {
                       while (dataReader.Read())
                       {
                           //Ipurchase.PONo = dataReader["PONo"].ToString();
                           IIndent.InReqDate = DateTime.Parse(dataReader["Date"].ToString());
                                     
                           IIndent.Cancelled = bool.Parse(dataReader["Cancelled"].ToString());
                           IIndent.CancelledUser = dataReader["CancelledBy"].ToString();
                           IIndent.CancelledOn = DateTime.Parse(dataReader["CancelledOn"].ToString());
                           IIndent.FK_Store = Convert.ToInt64(dataReader["FK_Store"]);
                           IIndent.Remarks = Convert.ToString(dataReader["Remarks"]);
                           IIndent.ToStore = Convert.ToInt64(dataReader["FK_ToStore"]);
                         //  Ipurchase.Posted = bool.Parse(dataReader["Posted"].ToString());
                       }
                   }
               }
               catch (SqlException ex)
               {
                   throw ex;
               }
               return IIndent;
           }
           public DataTable SelectAllIndentRequest(IIndentRequest Ipurchase)
           {
               DataTable dtblIndentRequest = new DataTable();
               Database db = DatabaseFactory.CreateDatabase("WebPharma");
               string sqlCommand = "proIndentRequestSelect";
               DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
               db.AddInParameter(dbCommand, "@ID_IndentRequest", DbType.Int64, Ipurchase.MasterId);
               db.AddInParameter(dbCommand, "@FK_Client", DbType.Int64, Ipurchase.FK_Client);
               db.AddInParameter(dbCommand, "@fromDate", DbType.DateTime, Ipurchase.FromDate);
               db.AddInParameter(dbCommand, "@toDate", DbType.DateTime, Ipurchase.ToDate);
               db.AddInParameter(dbCommand, "@FK_Store", DbType.Int64, Ipurchase.FK_Store);
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

           public int DeleteIntentRequest(IIndentRequest IIndent)
           {
               Database db = DatabaseFactory.CreateDatabase("WebPharma");
               string sqlCommand = "proIndentRequestDelete";
               DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
               db.AddInParameter(dbCommand, "@ID_IndentRequest", DbType.Int64, IIndent.MasterId);
               db.AddInParameter(dbCommand, "@FK_Client", DbType.Int32, IIndent.FK_Client);
               db.AddInParameter(dbCommand, "@Cancelleduser", DbType.String, IIndent.CancelledUser);
               db.AddInParameter(dbCommand, "@CancelledOn", DbType.DateTime, IIndent.CancelledOn);
               db.AddInParameter(dbCommand, "@FK_Reason", DbType.Int64, IIndent.FK_Reason);
               try
               {
                   return Convert.ToInt32(db.ExecuteScalar(dbCommand));
               }
               catch (SqlException e)
               {
                   //UpdateErrorLog(istock, e);
                   throw e;
               }
           }
           public DataTable SelectIndentRequestItems(IIndentRequest IIndent)
           {
               DataTable dtblIndentRequestItems = new DataTable();
               Database db = DatabaseFactory.CreateDatabase("WebPharma");
               string sqlCommand = "proIndentRequestItemSelect";
               DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
               db.AddInParameter(dbCommand, "@FK_IndentRequest", DbType.Int64, IIndent.MasterId);
               db.AddInParameter(dbCommand, "@FK_Client", DbType.Int64, IIndent.FK_Client);
               db.AddInParameter(dbCommand, "@FK_Store", DbType.Int64, IIndent.FK_Store);
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
           public DataTable SelectIndentRequestSmartFillItems(IIndentRequest Ipurchase)
           {
               DataTable dtblIndentRequestItems = new DataTable();
               Database db = DatabaseFactory.CreateDatabase("WebPharma");
               string sqlCommand = "IndentRequestSmartFillItemsSelect";
               DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
               db.AddInParameter(dbCommand, "@FK_Store", DbType.Int64, Ipurchase.FK_Store);
              
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


    }
}
