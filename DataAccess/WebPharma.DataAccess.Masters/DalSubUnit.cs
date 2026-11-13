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
   public class DalSubUnit
    {
       public Int32 UpdatesubUnit(ISubUnit isubUnit)
       {
           Database db = DatabaseFactory.CreateDatabase("WebPharma");
           string sqlCommand = "proSubUnitsUpdate";
           DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
           db.AddInParameter(dbCommand, "@UserAction", DbType.Byte, isubUnit.UserAction);
           db.AddInParameter(dbCommand, "@ID_SubUnits", DbType.Int64, isubUnit.MasterId);
           db.AddInParameter(dbCommand, "@Name", DbType.String, isubUnit.Name);
           db.AddInParameter(dbCommand, "@NoofUnits", DbType.Int32, isubUnit.NoofUnits);
           db.AddInParameter(dbCommand, "@UserCode", DbType.String, isubUnit.UserCode);
           db.AddInParameter(dbCommand, "@FK_Client", DbType.Int64, isubUnit.FK_Client);
           try
           {
               return Convert.ToInt32(db.ExecuteScalar(dbCommand));
           }
           catch (SqlException e)
           {
               return 0;
           }



       }
       public ISubUnit SelectsubUnit(ISubUnit isubUnit)
       {
           Database db = DatabaseFactory.CreateDatabase("WebPharma");
           string sqlCommand = "proSubunitSelect";
           DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
           db.AddInParameter(dbCommand, "@ID_SubUnits", DbType.Int64, isubUnit.MasterId);
           db.AddInParameter(dbCommand, "@Name", DbType.String, isubUnit.Name);
           db.AddInParameter(dbCommand, "@FK_Client", DbType.Int32, isubUnit.FK_Client);
           try
           {
               using (IDataReader dataReader = db.ExecuteReader(dbCommand))
               {
                   while (dataReader.Read())
                   {
                       isubUnit.Name = Convert.ToString(dataReader["SubUnitName"].ToString());
                       isubUnit.NoofUnits = Convert.ToInt32(dataReader["NoofUnits"].ToString());
                       isubUnit.UserCode = Convert.ToString(dataReader["UserCode"].ToString());
                       isubUnit.EnteredOn = Convert.ToDateTime(dataReader["EnteredOn"].ToString());
                       isubUnit.FK_Client = Convert.ToInt64(dataReader["FK_Client"].ToString());
                     
                   }


               }
           }
           catch (Exception ex)
           {
               throw ex;
           }
           return isubUnit;
       }
       public DataTable SelectAllsubUnits(ISubUnit isubUnit)
       {
           DataTable dtblUnit = new DataTable();
           Database db = DatabaseFactory.CreateDatabase("WebPharma");
           string sqlCommand = "proSubunitSelect";
           DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
           db.AddInParameter(dbCommand, "@ID_SubUnits", DbType.Int64, isubUnit.MasterId);
           db.AddInParameter(dbCommand, "@Name", DbType.String, isubUnit.Name);
           db.AddInParameter(dbCommand, "@FK_Client", DbType.Int32, isubUnit.FK_Client);

           try
           {
               dtblUnit = db.ExecuteDataSet(dbCommand).Tables[0];
               return dtblUnit;
           }
           catch (SqlException e)
           {
               //UpdateErrorLog(ibrand, e);
               throw e;
           }
       }
       public int DeletesubUnits(ISubUnit isubUnit)
       {
           Int32 status = 0;
           Database db = DatabaseFactory.CreateDatabase("WebPharma");
           string sqlCommand = "proSubUnitDelete";
           DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
           db.AddInParameter(dbCommand, "@ID_SubUnits", DbType.Int64, isubUnit.MasterId);
           db.AddInParameter(dbCommand, "@FK_Client", DbType.Int32, isubUnit.FK_Client);
           db.AddInParameter(dbCommand, "@UserCode", DbType.String, isubUnit.UserCode);
           db.AddInParameter(dbCommand, "@FK_Reason", DbType.String, isubUnit.FK_Reason);
           try
           {
               status = Convert.ToInt32(db.ExecuteScalar(dbCommand));
           }
           catch (SqlException e)
           {



           }
           return status;
       }
    }
}
