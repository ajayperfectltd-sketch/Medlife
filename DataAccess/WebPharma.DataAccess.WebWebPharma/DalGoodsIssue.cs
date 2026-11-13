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
 public   class DalGoodsIssue
    {
        public Int32 UpdateGoodsIssue(IGoodsIssue igoodsIssue)
        {
            Database db = DatabaseFactory.CreateDatabase("TEXTILES");
            string sqlCommand = "proGoodsIssueUpdate";
           DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
           db.AddInParameter(dbCommand, "@UserAction", DbType.Byte, igoodsIssue.UserAction);
           db.AddInParameter(dbCommand, "@ID_GoodsIssue", DbType.Int64, igoodsIssue.MasterId);
           db.AddInParameter(dbCommand, "@IssueDate", DbType.DateTime, igoodsIssue.IssueDate);
           db.AddInParameter(dbCommand, "@FK_FromStore", DbType.Int64, igoodsIssue.FK_FromStore);
           db.AddInParameter(dbCommand, "@FK_ToStore", DbType.Int64, igoodsIssue.FK_ToStore);
           db.AddInParameter(dbCommand, "@IssueMode", DbType.Byte, igoodsIssue.IssueMode);
           db.AddInParameter(dbCommand, "@Description", DbType.String, igoodsIssue.Description);
           db.AddInParameter(dbCommand, "@FK_Supplier", DbType.Int64, igoodsIssue.FK_Supplier);
           db.AddInParameter(dbCommand, "@UserCode", DbType.String, igoodsIssue.UserCode);
           db.AddInParameter(dbCommand, "@EnteredOn", DbType.DateTime, igoodsIssue.EnteredOn);
           db.AddInParameter(dbCommand, "@Cancelled", DbType.String, igoodsIssue.Cancelled);
           db.AddInParameter(dbCommand, "@Cancelleduser", DbType.String, igoodsIssue.Cancelleduser);
           db.AddInParameter(dbCommand, "@CancelledOn", DbType.DateTime, igoodsIssue.CancelledOn);
           db.AddInParameter(dbCommand, "@FK_Client", DbType.Int64, igoodsIssue.FK_Client);
           db.AddInParameter(dbCommand, "@XmlGoods", DbType.Xml, igoodsIssue.XmlGoods);
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

        public IGoodsIssue SelectGoodsIssue(IGoodsIssue IgoodsIssue)
        {
            Database db = DatabaseFactory.CreateDatabase("TEXTILES");
            string sqlCommand = "proGoodsIssueSelect";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

            db.AddInParameter(dbCommand, "@ID_GoodsIssue", DbType.Int64, IgoodsIssue.MasterId);
            db.AddInParameter(dbCommand, "@fromDate", DbType.DateTime, IgoodsIssue.FromDate);
            db.AddInParameter(dbCommand, "@todate", DbType.DateTime, IgoodsIssue.ToDate);
            db.AddInParameter(dbCommand, "@FK_FromStore", DbType.Int64, IgoodsIssue.FK_FromStore);
            db.AddInParameter(dbCommand, "@PageSize", DbType.Int16, IgoodsIssue.PageSize);
            db.AddInParameter(dbCommand, "@PageIndex", DbType.Int16, IgoodsIssue.PageIndex);
            db.AddInParameter(dbCommand, "@FK_Client", DbType.Int64, IgoodsIssue.FK_Client);
            try
            {
                using (IDataReader dataReader = db.ExecuteReader(dbCommand))
                {
                    while (dataReader.Read())
                    {
                        IgoodsIssue.IssueNo = dataReader["IssueNo"].ToString();
                        IgoodsIssue.IssueDate = Convert.ToDateTime(dataReader["IssueDate"]);
                        IgoodsIssue.FK_FromStore = Convert.ToInt64(dataReader["FK_FromStore"]);
                        IgoodsIssue.FK_ToStore = Convert.ToInt64(dataReader["FK_ToStore"]);
                        IgoodsIssue.IssueMode = Convert.ToByte(dataReader["IssueMode"]);
                        IgoodsIssue.Description = dataReader["Description"].ToString();
                        IgoodsIssue.FK_Supplier = Convert.ToInt64(dataReader["FK_Supplier"]);
                        IgoodsIssue.UserCode = dataReader["UserCode"].ToString();
                        IgoodsIssue.EnteredOn = Convert.ToDateTime(dataReader["EnteredOn"]);
                        IgoodsIssue.Cancelled = Convert.ToBoolean(dataReader["Cancelled"]);
                        IgoodsIssue.Cancelleduser = dataReader["Cancelleduser"].ToString();
                        IgoodsIssue.FK_Client = Convert.ToInt64(dataReader["FK_Client"]);

                    }
                }
            }
            catch (SqlException ex)
            {
                throw ex;
            }

            return IgoodsIssue;
        }
        public DataTable SelectAllGoodsIssue(IGoodsIssue IgoodsIssue)
        {
            DataTable dtblGoddsissue = new DataTable();
            Database db = DatabaseFactory.CreateDatabase("TEXTILES");
            string sqlCommand = "proGoodsIssueSelect";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

            db.AddInParameter(dbCommand, "@ID_GoodsIssue", DbType.Int64, IgoodsIssue.MasterId);
            db.AddInParameter(dbCommand, "@fromDate", DbType.DateTime, IgoodsIssue.FromDate);
            db.AddInParameter(dbCommand, "@todate", DbType.DateTime, IgoodsIssue.ToDate);
            db.AddInParameter(dbCommand, "@FK_FromStore", DbType.Int64, IgoodsIssue.FK_FromStore);
            db.AddInParameter(dbCommand, "@PageSize", DbType.Int16, IgoodsIssue.PageSize);
            db.AddInParameter(dbCommand, "@PageIndex", DbType.Int16, IgoodsIssue.PageIndex);
            db.AddInParameter(dbCommand, "@FK_Client", DbType.Int64, IgoodsIssue.FK_Client);
         
            try
            {
                dtblGoddsissue = db.ExecuteDataSet(dbCommand).Tables[0];
                return dtblGoddsissue;
            }
            catch (SqlException e)
            {

                throw e;
            }
        }


        public DataTable SelectGoodsIssueDetails(IGoodsIssue igoods)
        {
            
            DataTable dtblGoddsissue = new DataTable();
            Database db = DatabaseFactory.CreateDatabase("TEXTILES");
            string sqlCommand = "proGoodsIssueDetailsSelect";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
            db.AddInParameter(dbCommand, "@ID_GoodsIssue", DbType.Int64, igoods.MasterId);
            try
            {
                dtblGoddsissue = db.ExecuteDataSet(dbCommand).Tables[0];
                return dtblGoddsissue;
            }
            catch (SqlException e)
            {

                throw e;
            }

        }
        public int DeleteGoodsIssue(IGoodsIssue igoodsIssue)
        {
            Database db = DatabaseFactory.CreateDatabase("TEXTILES");
            string sqlCommand = "proGoodsIssueDelete";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
             db.AddInParameter(dbCommand, "@ID_GoodsIssue", DbType.Int64, igoodsIssue.MasterId);
            db.AddInParameter(dbCommand, "@FK_Client", DbType.Int32, igoodsIssue.FK_Client);
            db.AddInParameter(dbCommand, "@Cancelleduser", DbType.String, igoodsIssue.UserCode);
            db.AddInParameter(dbCommand, "@CancelledOn", DbType.DateTime, igoodsIssue.CancelledOn);
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

    }
}
