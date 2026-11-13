using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebPharma.Interface.Masters;
using WebPharma.Interface.General;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data.SqlClient;
using System.Data;
using System.Data.Common;

namespace WebPharma.DataAccess.Masters
{
    public class DalReportAcess
    {
        public Int32 UpdateReportAcess(IReportAcess iReportAcess)
        {
            Database db = DatabaseFactory.CreateDatabase("WebPharma");
            string sqlCommand = "proReportAccessUpdate";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
            db.AddInParameter(dbCommand, "@XmlReportAccess", DbType.String, iReportAcess.XmlReportAccess);
            db.AddInParameter(dbCommand, "@FK_Client", DbType.Int64, iReportAcess.FK_Client);
            db.AddInParameter(dbCommand, "@FK_Usergroup", DbType.Int64, iReportAcess.FK_UserGroup);
            db.AddInParameter(dbCommand, "@Type", DbType.Int32, iReportAcess.Type);
            db.AddInParameter(dbCommand, "@ID_ReportAccess", DbType.Int64, iReportAcess.MasterId);
            db.AddInParameter(dbCommand, "@UserCode", DbType.String, iReportAcess.UserCode);
            try
            {
                return Convert.ToInt32(db.ExecuteScalar(dbCommand));
            }
            catch (SqlException e)
            {
                
                return 0;
            }
        }
        public DataTable SelectReportCategories(IReportAcess iReportAcess)
        {
            DataTable dtblusers = new DataTable();
            Database db = DatabaseFactory.CreateDatabase("WebPharma");
            string sqlCommand = "proReportCategorySelect";
          
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
            db.AddInParameter(dbCommand, "@Type", DbType.Int32, iReportAcess.Type);
            try
            {
                dtblusers = db.ExecuteDataSet(dbCommand).Tables[0];
                return dtblusers;
            }
            catch (SqlException e)
            {
                throw e;
            }


        
        }
        public DataTable SelectReportAccess(IReportAcess iReportAcess)
        {
            DataTable dtblusers = new DataTable();
            Database db = DatabaseFactory.CreateDatabase("WebPharma");
            string sqlCommand = "proReportAccessSelect";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
            db.AddInParameter(dbCommand, "@FK_ReportCategory", DbType.Int64, iReportAcess.FK_ReportCategory);
            db.AddInParameter(dbCommand, "@FK_Client", DbType.Int64, iReportAcess.FK_Client);
            db.AddInParameter(dbCommand, "@FK_UserGroup", DbType.Int64, iReportAcess.FK_UserGroup);
            db.AddInParameter(dbCommand, "@Type", DbType.Int32, iReportAcess.Type);
            try
            {
                dtblusers = db.ExecuteDataSet(dbCommand).Tables[0];
                return dtblusers;
            }
            catch (SqlException e)
            {
                throw e;
            }



        }
        public DataTable SelectReportMenu(IReportAcess iReportAcess)
        {
            DataTable dtblusers = new DataTable();
            Database db = DatabaseFactory.CreateDatabase("WebPharma");
            string sqlCommand = "proReportMenuSelect";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
         
            db.AddInParameter(dbCommand, "@FK_Client", DbType.Int64, iReportAcess.FK_Client);
            db.AddInParameter(dbCommand, "@FK_User", DbType.Int64, iReportAcess.FK_User);
            db.AddInParameter(dbCommand, "@Type", DbType.Int32, iReportAcess.Type);
            try
            {
                dtblusers = db.ExecuteDataSet(dbCommand).Tables[0];
                return dtblusers;
            }
            catch (SqlException e)
            {
                throw e;
            }



        }
    }
    
}
