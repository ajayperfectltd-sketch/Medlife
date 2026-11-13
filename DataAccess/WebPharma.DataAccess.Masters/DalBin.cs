using Microsoft.Practices.EnterpriseLibrary.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebPharma.Interface.Masters;

namespace WebPharma.DataAccess.Masters
{
   public  class DalBin
    {

       public Int32 UpdateBin(IBin ibin)
       {
           Database db = DatabaseFactory.CreateDatabase("WebPharma");
           string sqlCommand = "proBinUpdate";
           DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
           db.AddInParameter(dbCommand, "@UserAction", DbType.Byte, ibin.UserAction);
           db.AddInParameter(dbCommand, "@ID_Bin", DbType.Int64, ibin.MasterId);
           db.AddInParameter(dbCommand, "@ShortName", DbType.String, ibin.ShortName);
           db.AddInParameter(dbCommand, "@BinName", DbType.String, ibin.BinName);
           db.AddInParameter(dbCommand, "@FK_Rack", DbType.Int64, ibin.FK_Rack);
           db.AddInParameter(dbCommand, "@UserCode", DbType.String, ibin.UserCode);
           db.AddInParameter(dbCommand, "@FK_Client", DbType.Int64, ibin.FK_Client);
           try
           {
               return Convert.ToInt32(db.ExecuteScalar(dbCommand));
           }
           catch (SqlException e)
           {
               // UpdateErrorLog(istore, e);
               return 0;
           }

       }
       public IBin SelectBin(IBin ibin)
       {
           Database db = DatabaseFactory.CreateDatabase("WebPharma");
           string sqlCommand = "proBinSelect";
           DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
           db.AddInParameter(dbCommand, "@ID_Bin", DbType.Int64, ibin.MasterId);
           db.AddInParameter(dbCommand, "@FK_Client", DbType.Int64, ibin.FK_Client);
           db.AddInParameter(dbCommand, "@BinName", DbType.String, ibin.BinName);
           db.AddInParameter(dbCommand, "@ShortName", DbType.String, ibin.ShortName);
           try
           {
               using (IDataReader dataReader = db.ExecuteReader(dbCommand))
               {
                   while (dataReader.Read())
                   {
                       ibin.BinName = Convert.ToString(dataReader["BinName"].ToString());
                       ibin.ShortName = Convert.ToString(dataReader["ShortName"].ToString());
                       ibin.UserCode = Convert.ToString(dataReader["UserCode"].ToString());
                       ibin.FK_Client = Convert.ToInt64(dataReader["FK_Client"].ToString());
                       ibin.FK_Rack = Convert.ToInt64(dataReader["FK_Rack"].ToString());
                   }
               }
               return ibin;
           }
           catch (SqlException e)
           {
               //UpdateErrorLog(istore, e);
               throw e;
           }                
       }
       public DataTable SelectAllBin(IBin ibin)
       {
           DataTable dtblStore = new DataTable();
           Database db = DatabaseFactory.CreateDatabase("WebPharma");
           string sqlCommand = "proBinSelect";
           DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
           db.AddInParameter(dbCommand, "@ID_Bin", DbType.Int64, ibin.MasterId);
           db.AddInParameter(dbCommand, "@FK_Client", DbType.Int32, ibin.FK_Client);
           db.AddInParameter(dbCommand, "@ShortName", DbType.String, ibin.ShortName);
           db.AddInParameter(dbCommand, "@BinName", DbType.String, ibin.BinName);
          
           try
           {
               dtblStore = db.ExecuteDataSet(dbCommand).Tables[0];
               return dtblStore;
           }
           catch (SqlException e)
           {
               //UpdateErrorLog(istore, e);
               throw e;
           }
       }
       public int DeleteBin(IBin ibin)
       {
           Database db = DatabaseFactory.CreateDatabase("WebPharma");
           string sqlCommand = "proBinDelete";
           DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
           db.AddInParameter(dbCommand, "@ID_Bin", DbType.Int64, ibin.MasterId);
           db.AddInParameter(dbCommand, "@FK_Client", DbType.Int32, ibin.FK_Client);
           db.AddInParameter(dbCommand, "@FK_Reason", DbType.Int64, ibin.FK_Reason);
           db.AddInParameter(dbCommand, "@UserCode", DbType.String, ibin.UserCode);
           try
           {
               return Convert.ToInt32(db.ExecuteScalar(dbCommand));
           }
           catch (SqlException e)
           {
               //UpdateErrorLog(icategoryfirst, e);
               throw e;
           }
       }
    }
}
