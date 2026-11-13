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
   public class DalDoctor
    {
       public Int32 UpdateDoctor(IDoctor idoctor)
       {
           Database db = DatabaseFactory.CreateDatabase("WebPharma");
           string sqlCommand = "proDoctorUpdate";
           DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
           db.AddInParameter(dbCommand, "@UserAction", DbType.Byte, idoctor.UserAction);
           db.AddInParameter(dbCommand, "@ID_Doctor", DbType.Int64, idoctor.MasterId);
           db.AddInParameter(dbCommand, "@DoctorName", DbType.String, idoctor.DoctorName);
           db.AddInParameter(dbCommand, "@Qualification", DbType.String, idoctor.Qualification);
           db.AddInParameter(dbCommand, "@ClinicName", DbType.String, idoctor.ClinicName);
           db.AddInParameter(dbCommand, "@UserCode", DbType.String, idoctor.UserCode);
           db.AddInParameter(dbCommand, "@FK_Client", DbType.Int64, idoctor.FK_Client);
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

       public IDoctor SelectDoctor(IDoctor idoctor)
       {
           Database db = DatabaseFactory.CreateDatabase("WebPharma");
           string sqlCommand = "proDoctorSelect";
           DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
           db.AddInParameter(dbCommand, "@ID_Doctor", DbType.Int64, idoctor.MasterId);
           db.AddInParameter(dbCommand, "@FK_Client", DbType.Int64, idoctor.FK_Client);
           db.AddInParameter(dbCommand, "@DoctorName", DbType.String, idoctor.DoctorName);
           try
           {
               using (IDataReader dataReader = db.ExecuteReader(dbCommand))
               {
                   while (dataReader.Read())
                   {
                       idoctor.DoctorName = Convert.ToString(dataReader["DoctorName"].ToString());
                       idoctor.ClinicName = Convert.ToString(dataReader["ClinicName"].ToString());
                       idoctor.Qualification = Convert.ToString(dataReader["Qualification"].ToString());
                       idoctor.UserCode = Convert.ToString(dataReader["UserCode"].ToString());
                       idoctor.FK_Client = Convert.ToInt64(dataReader["FK_Client"].ToString());
                   }
               }
               return idoctor;
           }
           catch (SqlException e)
           {
               //UpdateErrorLog(istore, e);
               throw e;
           }
       }
       public DataTable SelectAllDoctor(IDoctor idoctor)
       {
           DataTable dtblStore = new DataTable();
           Database db = DatabaseFactory.CreateDatabase("WebPharma");
           string sqlCommand = "proDoctorSelect";
           DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
           db.AddInParameter(dbCommand, "@ID_Doctor", DbType.Int64, idoctor.MasterId);
           db.AddInParameter(dbCommand, "@FK_Client", DbType.Int32, idoctor.FK_Client);
           db.AddInParameter(dbCommand, "@DoctorName", DbType.String, idoctor.DoctorName);
           //db.AddInParameter(dbCommand, "@PageIndex", DbType.Int16, istore.PageIndex);
           //db.AddInParameter(dbCommand, "@PageSize", DbType.Int16, istore.PageSize);
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
       public int DeleteDoctor(IDoctor idoctor)
       {
           Database db = DatabaseFactory.CreateDatabase("WebPharma");
           string sqlCommand = "proDoctorDelete";
           DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
           db.AddInParameter(dbCommand, "@ID_Doctor", DbType.Int64, idoctor.MasterId);
           db.AddInParameter(dbCommand, "@FK_Client", DbType.Int64, idoctor.FK_Client);
           db.AddInParameter(dbCommand, "@FK_Reason", DbType.Int64, idoctor.Reason);
           db.AddInParameter(dbCommand, "@UserCode", DbType.String, idoctor.UserCode);

           try
           {
               return Convert.ToInt32(db.ExecuteScalar(dbCommand));
           }
           catch (SqlException e)
           {
               //UpdateErrorLog(istore, e);
               throw e;
           }
       }
    }
}
