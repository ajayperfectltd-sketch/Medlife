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
using System.Linq;
using System.Text;
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
    public class DalSchedule
    {


        public Int32 SaveProcessData(ISchedule iSchedule)
        {
            Database db = DatabaseFactory.CreateDatabase("WebPharma");
            string sqlCommand = "proScheduleUpdate";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
            dbCommand.CommandTimeout = 0;
            db.AddInParameter(dbCommand, "@UserAction", DbType.Byte, iSchedule.UserAction);
            db.AddInParameter(dbCommand, "@ID_Schedule", DbType.Int64, iSchedule.MasterId);
            db.AddInParameter(dbCommand, "@ScheduleType", DbType.Int16, iSchedule.ScheduleMode);
            db.AddInParameter(dbCommand, "@TransDate", DbType.DateTime , iSchedule.ScheduleFrom );
            db.AddInParameter(dbCommand, "@ScheduleUpto", DbType.DateTime, iSchedule.ScheduleUpto);
            db.AddInParameter(dbCommand, "@CollectionUpto", DbType.DateTime, iSchedule.ScheduleUpto);
            db.AddInParameter(dbCommand, "@ScAmount", DbType.Decimal, iSchedule.ScAmount);
            db.AddInParameter(dbCommand, "@UserCode", DbType.String, iSchedule.UserCode);
            db.AddInParameter(dbCommand, "@FK_Client", DbType.Int64, iSchedule.FK_Client);
           db.AddInParameter(dbCommand, "@XmlScheduleDetails", DbType.Xml, iSchedule.XmlScheduleProcessing);



            try
            {
                return Convert.ToInt32(db.ExecuteScalar(dbCommand));
            }
            catch (SqlException e)
            {
                // UpdateErrorLog(iSchedule, e);
                return 0;
            }
        }
        public Int32 SavePostingData(ISchedule iSchedule)
        {
            Database db = DatabaseFactory.CreateDatabase("WebPharma");
            string sqlCommand = "ProSchedulePostingUpdate";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
            db.AddInParameter(dbCommand, "@TransDate", DbType.Byte, iSchedule.ScheduleFrom);
            db.AddInParameter(dbCommand, "@ScheduleUpto", DbType.Byte, iSchedule.ScheduleUpto);
            db.AddInParameter(dbCommand, "@CollectionUpto", DbType.Byte, iSchedule.ScheduleUpto);
            db.AddInParameter(dbCommand, "@UserCode", DbType.Byte, iSchedule.UserCode);
            db.AddInParameter(dbCommand, "@FK_Client", DbType.Int64, iSchedule.FK_Client);
            db.AddInParameter(dbCommand, "@XmlSchedulePosting", DbType.Xml, iSchedule.XmlSchedulePosting);



            try
            {
                return Convert.ToInt32(db.ExecuteScalar(dbCommand));
            }
            catch (SqlException e)
            {
                // UpdateErrorLog(iSchedule, e);
                return 0;
            }
        }
        public Int32 SaveScheduleImport(ISchedule iSchedule)
        {
            Database db = DatabaseFactory.CreateDatabase("WebPharma");
            string sqlCommand = "ProScheduleImportUpdate";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
         
            db.AddInParameter(dbCommand, "@XmlSchedulePosting", DbType.Xml, iSchedule.XmlSchedulePosting);



            try
            {
                return Convert.ToInt32(db.ExecuteScalar(dbCommand));
            }
            catch (SqlException e)
            {
                // UpdateErrorLog(iSchedule, e);
                return 0;
            }
        }

        public DataSet GetScheduleDataSource(long StatusId)
        {
            string sqlCommand = "";
            DbCommand dbCommand;
            DataSet ds = new DataSet();
            Database db = DatabaseFactory.CreateDatabase("WebPharma");
            sqlCommand = "ProRptScheduleReceiptprint";
            dbCommand = db.GetStoredProcCommand(sqlCommand);
            db.AddInParameter(dbCommand, "@ID_Schedule", DbType.Int64, StatusId);
            try
            {
                ds = db.ExecuteDataSet(dbCommand);
                return ds;
            }
            catch (SqlException e)
            {
                throw;
            }

        }
        public DataSet GetSchedulePostingDataSource(long StatusId)
        {
            string sqlCommand = "";
            DbCommand dbCommand;
            DataSet ds = new DataSet();
            Database db = DatabaseFactory.CreateDatabase("WebPharma");
            sqlCommand = "ProRptSchedulePostingReceiptprint";
            dbCommand = db.GetStoredProcCommand(sqlCommand);
            db.AddInParameter(dbCommand, "@ID_Schedule", DbType.Int64, StatusId);
            try
            {
                ds = db.ExecuteDataSet(dbCommand);
                return ds;
            }
            catch (SqlException e)
            {
                throw;
            }

        }
        
        public DataTable ProcessingData(ISchedule iSchedule)
        {
            DataTable dtbl = new DataTable();
            Database db = DatabaseFactory.CreateDatabase("WebPharma");
            string sqlCommand = "ProScheduleProcess";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
            db.AddInParameter(dbCommand, "@TransDate", DbType.DateTime, iSchedule.ScheduleFrom);
            db.AddInParameter(dbCommand, "@ScheduleUpto", DbType.DateTime, iSchedule.ScheduleUpto);
            db.AddInParameter(dbCommand, "@CollectionUpto", DbType.DateTime, iSchedule.ScheduleUpto);
          
            try
            {
                dtbl = db.ExecuteDataSet(dbCommand).Tables[0];
                return dtbl;
            }
            catch (SqlException e)
            {
                // UpdateErrorLog(istock, e);
                throw e;
            }

        }

        public DataTable SelectSchedulePosting(ISchedule iSchedule)
        {
            DataTable dtbl = new DataTable();
            Database db = DatabaseFactory.CreateDatabase("WebPharma");
            string sqlCommand = "proSchedulePosting";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);                    
           
           
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

        public DataTable CheckAlreadyprocessed(ISchedule iSchedule)
        {
            DataTable dtbl = new DataTable();
            Database db = DatabaseFactory.CreateDatabase("WebPharma");
            string sqlCommand = "proScheduleSelect";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
            db.AddInParameter(dbCommand, "@ID_Schedule", DbType.Int64, iSchedule.MasterId);
            db.AddInParameter(dbCommand, "@ScheduleUpto", DbType.DateTime, iSchedule.ScheduleUpto);
            db.AddInParameter(dbCommand, "@FK_Client", DbType.Int64, iSchedule.FK_Client);
            db.AddInParameter(dbCommand, "@PageIndex", DbType.Int16, iSchedule.PageIndex);
            db.AddInParameter(dbCommand, "@PageSize", DbType.Int16, iSchedule.PageSize);

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
        public DataTable SelectAllSchedule(ISchedule iSchedule)
        {
            DataTable dtbl = new DataTable();
            Database db = DatabaseFactory.CreateDatabase("WebPharma");
            string sqlCommand = "proScheduleSelectAll";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
            db.AddInParameter(dbCommand, "@ID_Schedule", DbType.Int64, iSchedule.MasterId);
            db.AddInParameter(dbCommand, "@FromScheduleUpto", DbType.DateTime, iSchedule.SearchFromScheduleUpto);
            db.AddInParameter(dbCommand, "@ToScheduleUpto", DbType.DateTime, iSchedule.SearchToScheduleUpto);
            db.AddInParameter(dbCommand, "@FK_Client", DbType.Int64, iSchedule.FK_Client);
            db.AddInParameter(dbCommand, "@PageIndex", DbType.Int16, iSchedule.PageIndex);
            db.AddInParameter(dbCommand, "@PageSize", DbType.Int16, iSchedule.PageSize);

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
        public int DeleteSchedule(ISchedule iSchedule)
        {
            Database db = DatabaseFactory.CreateDatabase("WebPharma");
            string sqlCommand = "proScheduleDelete";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
            db.AddInParameter(dbCommand, "@ID_Schedule", DbType.Int64, iSchedule.MasterId);
            db.AddInParameter(dbCommand, "@FK_Client", DbType.Int32, iSchedule.FK_Client);

            try
            {
                return Convert.ToInt32(db.ExecuteScalar(dbCommand));
            }
            catch (SqlException e)
            {
                //UpdateErrorLog(iemployee, e);
                throw e;
            }
        }

    }
}
