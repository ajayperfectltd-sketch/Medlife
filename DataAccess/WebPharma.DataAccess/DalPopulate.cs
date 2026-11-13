using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Data.Common;
using WebPharma.Interface;
using WebPharma.DataAccess;
using Microsoft.Practices.EnterpriseLibrary.Data;

namespace WebPharma.DataAccess
{
   public class DalPopulate
    {
       public DataTable PopulateData(IPopulate ipopulate)
       {
           string sqlCommand = "";
           DbCommand dbCommand;
           DataTable dt = new DataTable();
           Database db = DatabaseFactory.CreateDatabase("WebPharma");
           sqlCommand = "proCmnPopulateData";
           dbCommand = db.GetStoredProcCommand(sqlCommand);
           db.AddInParameter(dbCommand, "TableName", DbType.String, ipopulate.TableName);
           db.AddInParameter(dbCommand, "ListField", DbType.String, ipopulate.ListFields + ((ipopulate.ValueFields.Trim().Length <= 0) ? "" : "," + ipopulate.ValueFields));
           db.AddInParameter(dbCommand, "SortField", DbType.String, ipopulate.SortFields);
           db.AddInParameter(dbCommand, "Criteria", DbType.String, ipopulate.Criteria);
           try
           {
               DataSet ds = new DataSet();
               ds = db.ExecuteDataSet(dbCommand);
               if (ds.Tables.Count > 0)
               {
                   dt = ds.Tables[0];
               }
               return dt;
           }
           catch (Exception e)
           {
               throw;
           }
       }
       
       public DataTable PopulateGrid(IPopulate ipopulate)
       {
           string sqlCommand = ""; DbCommand dbCommand;
           DataTable dt = new DataTable();
           Database db = DatabaseFactory.CreateDatabase("WebPharma");
           sqlCommand = "ProCmnGetRecordsPopup";
           dbCommand = db.GetStoredProcCommand(sqlCommand);
           db.AddInParameter(dbCommand, "@TableName", DbType.String, ipopulate.TableName);
           db.AddInParameter(dbCommand, "@ListFields", DbType.String, ipopulate.ListFields);
           db.AddInParameter(dbCommand, "@SortField", DbType.String, ipopulate.SortFields);
           db.AddInParameter(dbCommand, "@Criteria", DbType.String, ipopulate.Criteria);
           db.AddInParameter(dbCommand, "@Criteria2", DbType.String, ipopulate.Criteria2);
           db.AddInParameter(dbCommand, "@Groupby", DbType.String, ipopulate.GroupBy);
           db.AddInParameter(dbCommand, "@SortOrder", DbType.Int16, ipopulate.SortOrder);
           try
           {
               DataSet ds = new DataSet();
               ds = db.ExecuteDataSet(dbCommand);
               if (ds.Tables.Count > 0)
               {
                   dt = ds.Tables[0];
               }
               return dt;
           }
           catch (Exception e)
           {
               throw;
           }
       }
       public DataTable PopulatePopUpGrid(IPopulate ipopulate)
       {
           string sqlCommand = ""; DbCommand dbCommand;
           DataTable dt = new DataTable();
           Database db = DatabaseFactory.CreateDatabase("WebPharma");
           sqlCommand = "ProCommnGetRecordsPopup";
           dbCommand = db.GetStoredProcCommand(sqlCommand);
           db.AddInParameter(dbCommand, "@TableName", DbType.String, ipopulate.TableName);
           db.AddInParameter(dbCommand, "@ListFields", DbType.String, ipopulate.ListFields);
           db.AddInParameter(dbCommand, "@SortField", DbType.String, ipopulate.SortFields);
           db.AddInParameter(dbCommand, "@Criteria", DbType.String, ipopulate.Criteria);
           db.AddInParameter(dbCommand, "@Criteria2", DbType.String, ipopulate.Criteria2);
           db.AddInParameter(dbCommand, "@Groupby", DbType.String, ipopulate.GroupBy);
       
           try
           {
               DataSet ds = new DataSet();
               ds = db.ExecuteDataSet(dbCommand);
               if (ds.Tables.Count > 0)
               {
                   dt = ds.Tables[0];
               }
               return dt;
           }
           catch (Exception e)
           {
               throw;
           }
       }


       public Int32 GetNextNumber(IPopulate ipopulate)
       {
           Database db = DatabaseFactory.CreateDatabase("WebPharma");
           string sqlCommand = "ProCmnNextAHNumber";
           DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
           db.AddInParameter(dbCommand, "@TableName", DbType.String, ipopulate.TableName);
           db.AddInParameter(dbCommand, "@FieldName", DbType.String, ipopulate.ValueFields);
           db.AddInParameter(dbCommand, "@Criteria", DbType.String, ipopulate.Criteria2);

           try
           {
               return Convert.ToInt32(db.ExecuteScalar(dbCommand));
           }
           catch (SqlException)
           {
               return 0;
           }
       }
       public DataTable PopulateSalesReturnGrid(IPopulate ipopulate)
       {
           string sqlCommand = ""; DbCommand dbCommand;
           DataTable dt = new DataTable();
           Database db = DatabaseFactory.CreateDatabase("WebPharma");
           sqlCommand = "ProCmnGetSalesReturnGridRecordsPopup";
           dbCommand = db.GetStoredProcCommand(sqlCommand);
           db.AddInParameter(dbCommand, "@TableName", DbType.String, ipopulate.TableName);
           db.AddInParameter(dbCommand, "@ListFields", DbType.String, ipopulate.ListFields);
           db.AddInParameter(dbCommand, "@SortField", DbType.String, ipopulate.SortFields);
           db.AddInParameter(dbCommand, "@Criteria", DbType.String, ipopulate.Criteria);
           db.AddInParameter(dbCommand, "@Criteria2", DbType.String, ipopulate.Criteria2);
           db.AddInParameter(dbCommand, "@Groupby", DbType.String, ipopulate.GroupBy);
           db.AddInParameter(dbCommand, "@SortOrder", DbType.Int16, ipopulate.SortOrder);
           try
           {
               DataSet ds = new DataSet();
               ds = db.ExecuteDataSet(dbCommand);
               if (ds.Tables.Count > 0)
               {
                   dt = ds.Tables[0];
               }
               return dt;
           }
           catch (Exception e)
           {
               throw;
           }
       }
    }
}
