using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using WebPharma.DataAccess;
using Microsoft.Practices.EnterpriseLibrary.Data;
using WebPharma.Interface.Masters;
using WebPharma.Interface.General;

namespace WebPharma.DataAccess.Masters
{
   public  class DalAccountSubGroup
    {
       public Int32 UpdateAccountSubGroup(IAccountSubGroup iAccGrp)
       {
           Database db = DatabaseFactory.CreateDatabase("WebPharma");
           string sqlCommand = "proAccountSubGroupUpdate";
           DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
           db.AddInParameter(dbCommand, "@ID_AccountSubGroup", DbType.Int64, iAccGrp.MasterId);
           db.AddInParameter(dbCommand, "@FK_AccountGroup", DbType.Int64, iAccGrp.FK_Accountgroup);
           db.AddInParameter(dbCommand, "@Name", DbType.String, iAccGrp.Name);
           db.AddInParameter(dbCommand, "@ShortName", DbType.String, iAccGrp.ShortName);
           db.AddInParameter(dbCommand, "@SortOrder", DbType.Int32, iAccGrp.SortOrder);
           db.AddInParameter(dbCommand, "@Description", DbType.String, iAccGrp.Description);
           db.AddInParameter(dbCommand, "@UserCode", DbType.String, iAccGrp.UserCode);
           db.AddInParameter(dbCommand, "@EnteredOn", DbType.DateTime, iAccGrp.EnteredOn);
           db.AddInParameter(dbCommand, "@FK_Client", DbType.Int64, iAccGrp.FK_Client);
           db.AddInParameter(dbCommand, "@Active", DbType.Boolean, iAccGrp.Active);
           db.AddInParameter(dbCommand, "@UserAction", DbType.Byte, iAccGrp.UserAction);
           try
           {
               return Convert.ToInt32(db.ExecuteScalar(dbCommand));
           }
           catch (SqlException ex)
           {
               return 0;
           }
       }
       public int DeleteAccountSubGroup(IAccountSubGroup iAccGrp)
       {
             Database db = DatabaseFactory.CreateDatabase("WebPharma");
             string sqlCommand = "proAccountSubGroupDelete";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
            db.AddInParameter(dbCommand, "@ID_AccountSubGroup", DbType.Int64, iAccGrp.MasterId);
            db.AddInParameter(dbCommand, "@FK_Client", DbType.Int64, iAccGrp.FK_Client);
            db.AddInParameter(dbCommand, "@UserCode", DbType.String, iAccGrp.UserCode);
            try
            {
                return Convert.ToInt32(db.ExecuteScalar(dbCommand));
            }
            catch (SqlException e)
            {
                throw e;
            }
       
       }
       public IAccountSubGroup SelectAccountSubGroup(IAccountSubGroup iAccGrp)
       {
           Database db = DatabaseFactory.CreateDatabase("WebPharma");
           string sqlCommand = "proAccountSubGroupSelect";
           DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
           db.AddInParameter(dbCommand, "@ID_AccountSubGroup", DbType.Int64, iAccGrp.MasterId);
           db.AddInParameter(dbCommand, "@FK_AccountGroup", DbType.Int64, iAccGrp.FK_Accountgroup);
           db.AddInParameter(dbCommand, "@Name", DbType.String, iAccGrp.Name);
           db.AddInParameter(dbCommand, "@ShortName", DbType.String, iAccGrp.ShortName);
           db.AddInParameter(dbCommand, "@FK_Client", DbType.Int64, iAccGrp.FK_Client);
           try
           {
               using (IDataReader dataReader = db.ExecuteReader(dbCommand))
               {
                   while (dataReader.Read())
                   {
                       iAccGrp.ShortName = dataReader["ShortName"].ToString();
                       iAccGrp.Name = dataReader["SubGroupName"].ToString();
                       iAccGrp.FK_Accountgroup = Convert.ToInt64(dataReader["FK_Accountgroup"].ToString());
                       iAccGrp.SortOrder = int.Parse(dataReader["SortOrder"].ToString());
                      
                     
                   }
               }
               return iAccGrp;
           }
              
           catch (SqlException e)
           {
               throw e;
           }

       }
       public DataTable SelectAllAccountSubGroup(IAccountSubGroup iAccGrp)
       {
           Database db = DatabaseFactory.CreateDatabase("WebPharma");
           DataTable dtbl = new DataTable();
           string sqlCommand = "proAccountSubGroupSelect";
           DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
           db.AddInParameter(dbCommand, "@ID_AccountSubGroup", DbType.Int64, iAccGrp.MasterId);
           db.AddInParameter(dbCommand, "@FK_AccountGroup", DbType.Int64, iAccGrp.FK_Accountgroup);
           db.AddInParameter(dbCommand, "@ShortName", DbType.String, iAccGrp.ShortName);
           db.AddInParameter(dbCommand, "@Name", DbType.String, iAccGrp.Name);
           db.AddInParameter(dbCommand, "@FK_Client", DbType.Int64, iAccGrp.FK_Client);

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

        
       
    }
   
}
