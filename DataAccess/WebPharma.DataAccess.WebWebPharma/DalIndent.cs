using Microsoft.Practices.EnterpriseLibrary.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebPharma.Interface.WebPharma;

namespace WebPharma.DataAccess.WebPharma
{
   public class DalIndent
    {
       public DataTable SelectIntentREq(IIndent Indent)
       {
           DataTable dtbl = new DataTable();
           Database db = DatabaseFactory.CreateDatabase("WebPharma");
           string sqlCommand = "proIndentItemSelect";
           DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
           db.AddInParameter(dbCommand, "@FK_IndentRequest", DbType.Int64, Indent.ReqID);
          
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
       public DataTable SelectReqPopup(IIndent Indent)
       {
           DataTable dtbl = new DataTable();
           Database db = DatabaseFactory.CreateDatabase("WebPharma");
           string sqlCommand = "proIndentReqStore";
           DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
           db.AddInParameter(dbCommand, "@FK_Store", DbType.Int64, Indent.FK_Store);
           db.AddInParameter(dbCommand, "@ToStore", DbType.Int64, Indent.ToStore);
           db.AddInParameter(dbCommand, "@FK_Client", DbType.Int64, Indent.FK_Client);

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
       public int DeleteIndent(IIndent IIndent)
       {
           Database db = DatabaseFactory.CreateDatabase("WebPharma");
           string sqlCommand = "proIndentDelete";
           DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
           db.AddInParameter(dbCommand, "@ID_Indent", DbType.Int64, IIndent.MasterId);
           db.AddInParameter(dbCommand, "@FK_Client", DbType.Int32, IIndent.FK_Client);
           db.AddInParameter(dbCommand, "@UserCode", DbType.String, IIndent.UserCode);
           db.AddInParameter(dbCommand, "@FK_Reason", DbType.String, IIndent.FK_Reason);
           try
           {
               return Convert.ToInt32(db.ExecuteScalar(dbCommand));
           }
           catch (SqlException e)
           {
               //UpdateErrorLog(iitems, e);
               throw e;
           }
       }
       public DataTable SelectIndentData(IIndent IIndent)
       {
           DataTable dtbl = new DataTable();
           Database db = DatabaseFactory.CreateDatabase("WebPharma");
           string sqlCommand = "proIndentSelect";
           DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

           db.AddInParameter(dbCommand, "@FK_Store", DbType.Int64, IIndent.FK_Store);
           db.AddInParameter(dbCommand, "@FK_Client", DbType.Int64, IIndent.FK_Client);
           db.AddInParameter(dbCommand, "@fromDate", DbType.DateTime, IIndent.FromDate);
           db.AddInParameter(dbCommand, "@toDate", DbType.DateTime, IIndent.Todate);
           db.AddInParameter(dbCommand, "@ID_Indent", DbType.Int64, IIndent.MasterId);
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
       public DataSet SelectIndentStockEdit(IIndent IIndent)
       {
           DataSet dtbl = new DataSet();
           Database db = DatabaseFactory.CreateDatabase("WebPharma");
           string sqlCommand = "proIndentStockEdit";
           DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

           db.AddInParameter(dbCommand, "@Fk_Indent", DbType.Int64, IIndent.MasterId);
           db.AddInParameter(dbCommand, "@Fk_Stock", DbType.Int64, IIndent.FK_Stock);
           db.AddInParameter(dbCommand, "@FK_Store", DbType.Int64, IIndent.ToStoreE);
           db.AddInParameter(dbCommand, "@Fk_Items", DbType.Int64, IIndent.FK_Items);
         
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
       public IIndent SelectMasterData(IIndent IIndent)
       {
           Database db = DatabaseFactory.CreateDatabase("WebPharma");
           string sqlCommand = "proIndentSelect";
           DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

           db.AddInParameter(dbCommand, "@FK_Store", DbType.Int64, IIndent.FK_Store);
           db.AddInParameter(dbCommand, "@FK_Client", DbType.Int64, IIndent.FK_Client);
           db.AddInParameter(dbCommand, "@fromDate", DbType.DateTime, IIndent.FromDate);
           db.AddInParameter(dbCommand, "@toDate", DbType.DateTime, IIndent.Todate);
           db.AddInParameter(dbCommand, "@ID_Indent", DbType.Int64, IIndent.MasterId);
           try
           {
               using (IDataReader dataReader = db.ExecuteReader(dbCommand))
               {
                   while (dataReader.Read())
                   {
                       IIndent.IndentDate = Convert.ToDateTime(dataReader["IndentDate"].ToString());
                       IIndent.FK_Store = Convert.ToInt64(dataReader["FK_StoreFrom"].ToString());
                       IIndent.ToStore = Convert.ToInt64(dataReader["FK_StoreTo"].ToString());
                       IIndent.ReqID = Convert.ToInt64(dataReader["FK_IndentRequest"].ToString());
                       IIndent.UserCode = Convert.ToString(dataReader["UserCode"].ToString());


                   }
               }
           }
           catch (SqlException ex)
           {
               throw ex;
           }
           return IIndent;
       }
       public DataSet SelectItemDetailsGrid(IIndent IIndent)
       {
           DataSet dtbl = new DataSet();
           Database db = DatabaseFactory.CreateDatabase("WebPharma");
           string sqlCommand = "proIndentDetailsSelect";
           DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

           db.AddInParameter(dbCommand, "@FK_Store", DbType.Int64, IIndent.FK_Store);
           db.AddInParameter(dbCommand, "@FK_Client", DbType.Int64, IIndent.FK_Client);
           db.AddInParameter(dbCommand, "@ID_Indent", DbType.Int64, IIndent.MasterId);
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

       public Int32 UpdateIndent(IIndent IIndent)
       {
           Database db = DatabaseFactory.CreateDatabase("WebPharma");
           string sqlCommand = "ProIndentUpdate";
           DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
           db.AddInParameter(dbCommand, "@UserAction", DbType.Byte, IIndent.UserAction);
           db.AddInParameter(dbCommand, "@ID_Indent", DbType.Int64, IIndent.MasterId);
           db.AddInParameter(dbCommand, "@FK_StoreFrom", DbType.Int64, IIndent.FK_Store);
           db.AddInParameter(dbCommand, "@FK_StoreTo", DbType.Int64, IIndent.ToStore);
           db.AddInParameter(dbCommand, "@TransDate", DbType.DateTime, IIndent.IndentDate);
           db.AddInParameter(dbCommand, "@FK_IndentRequest", DbType.Int64, IIndent.ReqID);
           db.AddInParameter(dbCommand, "@Remarks", DbType.String, IIndent.Remarks);
           db.AddInParameter(dbCommand, "@UserCode", DbType.String, IIndent.UserCode);
           db.AddInParameter(dbCommand, "@FK_Client", DbType.Int64, IIndent.FK_Client);
           db.AddInParameter(dbCommand, "@XmlIndendDetails", DbType.Xml, IIndent.xmlGriddata);

           try
           {
               return Convert.ToInt32(db.ExecuteScalar(dbCommand));
           }
           catch (SqlException e)
           {
              
               return 0;
           }
       }
       public long IndentApprove(IIndent IIndent)
       {

           Database db = DatabaseFactory.CreateDatabase("WebPharma");
           string sqlCommand = "ProIndentApprove";
           DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
           db.AddInParameter(dbCommand, "@ID_Indent", DbType.Int64, IIndent.MasterId);
           db.AddInParameter(dbCommand, "@UserCode", DbType.String, IIndent.UserCode);
           db.AddInParameter(dbCommand, "@FK_Client", DbType.String, IIndent.FK_Client);


           try
           {
               return Convert.ToInt64(db.ExecuteScalar(dbCommand));
           }
           catch (SqlException e)
           {
               // UpdateErrorLog(istock, e);
               return 0;
           }


       }
    }
}
