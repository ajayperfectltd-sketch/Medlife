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
  public  class DalGeneric
    {
      public Int32 UpdateGeneric(IGeneric iGeneric)
      {
          Database db = DatabaseFactory.CreateDatabase("WebPharma");
          string sqlCommand = "proGenericUpdate";
          DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
          db.AddInParameter(dbCommand, "@UserAction", DbType.Byte, iGeneric.UserAction);
          db.AddInParameter(dbCommand, "@ID_Generic", DbType.Int64, iGeneric.MasterId);
          db.AddInParameter(dbCommand, "@ShortName", DbType.String, iGeneric.ShortName);
          db.AddInParameter(dbCommand, "@GenericName", DbType.String, iGeneric.GenericName);
          db.AddInParameter(dbCommand, "@UserCode", DbType.String, iGeneric.UserCode);
          db.AddInParameter(dbCommand, "@FK_Client", DbType.Int64, iGeneric.FK_Client);
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
      public IGeneric SelectGeneric(IGeneric iGeneric)
      {
          Database db = DatabaseFactory.CreateDatabase("WebPharma");
          string sqlCommand = "proGenericSelect";
          DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
          db.AddInParameter(dbCommand, "@ID_Generic", DbType.Int64, iGeneric.MasterId);
          db.AddInParameter(dbCommand, "@FK_Client", DbType.Int64, iGeneric.FK_Client);
          db.AddInParameter(dbCommand, "@GenericName", DbType.String, iGeneric.GenericName);
          db.AddInParameter(dbCommand, "@ShortName", DbType.String, iGeneric.ShortName);
          try
          {
              using (IDataReader dataReader = db.ExecuteReader(dbCommand))
              {
                  while (dataReader.Read())
                  {
                      iGeneric.GenericName = Convert.ToString(dataReader["GenericName"].ToString());
                      iGeneric.ShortName = Convert.ToString(dataReader["ShortName"].ToString());
                      iGeneric.UserCode = Convert.ToString(dataReader["UserCode"].ToString());
                      iGeneric.FK_Client = Convert.ToInt64(dataReader["FK_Client"].ToString());
                  }
              }
              return iGeneric;
          }
          catch (SqlException e)
          {
              //UpdateErrorLog(istore, e);
              throw e;
          }
      }
      public DataTable SelectAllGeneric(IGeneric iGeneric)
      {
          DataTable dtblStore = new DataTable();
          Database db = DatabaseFactory.CreateDatabase("WebPharma");
          string sqlCommand = "proGenericSelect";
          DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
          db.AddInParameter(dbCommand, "@ID_Generic", DbType.Int64, iGeneric.MasterId);
          db.AddInParameter(dbCommand, "@FK_Client", DbType.Int32, iGeneric.FK_Client);
          db.AddInParameter(dbCommand, "@ShortName", DbType.String, iGeneric.ShortName);
          db.AddInParameter(dbCommand, "@GenericName", DbType.String, iGeneric.GenericName);

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
      public int DeleteGeneric(IGeneric iGeneric)
      {
          Database db = DatabaseFactory.CreateDatabase("WebPharma");
          string sqlCommand = "proGenericDelete";
          DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
          db.AddInParameter(dbCommand, "@ID_Generic", DbType.Int64, iGeneric.MasterId);
          db.AddInParameter(dbCommand, "@FK_Client", DbType.Int32, iGeneric.FK_Client);
          db.AddInParameter(dbCommand, "@FK_Reason", DbType.Int64, iGeneric.FK_Reason);
          db.AddInParameter(dbCommand, "@UserCode", DbType.String, iGeneric.UserCode);
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
