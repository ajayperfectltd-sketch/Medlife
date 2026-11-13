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
   public  class DalQuotation
    {
         public Int32 UpdateQuotation(IQuotation iquotation)
        
         {
            Database db = DatabaseFactory.CreateDatabase("WebPharma");
            string sqlCommand = "proQuatationUpdate";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
            db.AddInParameter(dbCommand, "@UserAction", DbType.Byte, iquotation.UserAction);
            db.AddInParameter(dbCommand, "@ID_Quatation", DbType.Int64, iquotation.MasterId);
            db.AddInParameter(dbCommand, "@Type", DbType.Int16, iquotation.Type);
            db.AddInParameter(dbCommand, "@FK_Master", DbType.Int64, iquotation.FK_Master);
            db.AddInParameter(dbCommand, "@EffectFrom", DbType.DateTime, iquotation.EffectFrom);
            db.AddInParameter(dbCommand, "@EffectTo", DbType.DateTime, iquotation.EffectTo);
            db.AddInParameter(dbCommand, "@XmlDetails", DbType.String, iquotation.XmlDetails);
            db.AddInParameter(dbCommand, "@Description", DbType.String, iquotation.Description);
            db.AddInParameter(dbCommand, "@UserCode", DbType.String, iquotation.UserCode);
            db.AddInParameter(dbCommand, "@FK_Client", DbType.Int64, iquotation.FK_Client);
              try
              {
                  return Convert.ToInt32(db.ExecuteScalar(dbCommand));

              }
              catch (SqlException e)
              {
                  // UpdateErrorLog(iitems, e);
                  return 0;
              }
         }
     

         public DataTable SelectAllIQuotation(IQuotation iQuatation)
         {
             DataTable dtblItems = new DataTable();
             Database db = DatabaseFactory.CreateDatabase("WebPharma");
             string sqlCommand = "proQuatationSelect";
             DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
             db.AddInParameter(dbCommand, "@Type", DbType.Int16, iQuatation.MasterId);
             db.AddInParameter(dbCommand, "@SearchName", DbType.String, iQuatation.Name);
             db.AddInParameter(dbCommand, "@FK_Client", DbType.Int32, iQuatation.FK_Client);
             try
             {
                 dtblItems = db.ExecuteDataSet(dbCommand).Tables[0];
                 return dtblItems;
             }
             catch (SqlException e)
             {
                 
                 throw e;
             }
         }
       
         public DataTable SelectQuotationDetails(IQuotation iQuatation)
         {
             DataTable dtblItems = new DataTable();
             Database db = DatabaseFactory.CreateDatabase("WebPharma");
             string sqlCommand = "proQuatationGridSelect";
             DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
             db.AddInParameter(dbCommand, "@ID_GroupID", DbType.Int32, iQuatation.MasterId);
             try
             {
                 dtblItems = db.ExecuteDataSet(dbCommand).Tables[0];
                 return dtblItems;
             }
             catch (SqlException e)
             {
                 
                 throw e;
             }
         }
         public int DeleteQuataion(IQuotation iQuatation)
         {
             Database db = DatabaseFactory.CreateDatabase("WebPharma");
             string sqlCommand = "proQuotationDelete";
             DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
             db.AddInParameter(dbCommand, "@ID_Quatation", DbType.Int64, iQuatation.MasterId);
             db.AddInParameter(dbCommand, "@FK_Client", DbType.Int32, iQuatation.FK_Client);
             db.AddInParameter(dbCommand, "@UserCode", DbType.String, iQuatation.UserCode);
             db.AddInParameter(dbCommand, "@FK_Reason", DbType.String, iQuatation.FK_Reason);
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
         public IQuotation Selectdata(IQuotation iQuatation)
         {
             Database db = DatabaseFactory.CreateDatabase("WebPharma");
             string sqlCommand = "proQuatationSelect";
             DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
             db.AddInParameter(dbCommand, "@Type", DbType.Int16, iQuatation.MasterId);
             db.AddInParameter(dbCommand, "@SearchName", DbType.String, iQuatation.Name);
             db.AddInParameter(dbCommand, "@FK_Client", DbType.Int32, iQuatation.FK_Client);
             try
             {
                 using (IDataReader dataReader = db.ExecuteReader(dbCommand))
                 {
                     while (dataReader.Read())
                     {
                         iQuatation.Name = Convert.ToString(dataReader["MasterName"].ToString());
                         iQuatation.EffectFrom = Convert.ToDateTime(dataReader["EffectFrom"].ToString());
                         iQuatation.EffectTo = Convert.ToDateTime(dataReader["EffectTo"].ToString());
                         iQuatation.FK_Master = Convert.ToInt64(dataReader["FK_Master"].ToString());
                         iQuatation.Description = Convert.ToString(dataReader["Description"].ToString());
                      
                         
                     }
                 }
                 return iQuatation;
             }
             catch (SqlException e)
             {
                 //UpdateErrorLog(iitems, e);
                 throw e;
             }
         }
    }
}
