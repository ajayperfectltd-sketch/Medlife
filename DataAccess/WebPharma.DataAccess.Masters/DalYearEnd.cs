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
    public class DalYearEnd
    {
        public DataTable UpdateYearEnd(IYearEnd iYearEnd, int process)
        {
            Database db = DatabaseFactory.CreateDatabase("WebPharma");
            string sqlcommand = "proYearEnd";
            DbCommand dbcmd = db.GetStoredProcCommand(sqlcommand);
            db.AddInParameter(dbcmd, "@UserAction", DbType.Byte, iYearEnd.UserAction);
            //db.AddInParameter(dbcmd, "@ID_YearEnd", DbType.Int64, iYearEnd.MasterId);
            db.AddInParameter(dbcmd, "@FK_Outlet", DbType.String, iYearEnd.FK_Outlet);      
            db.AddInParameter(dbcmd, "@FromDate", DbType.DateTime, iYearEnd.FromDate);
            db.AddInParameter(dbcmd, "@ToDate", DbType.DateTime, iYearEnd.ToDate);           
            db.AddInParameter(dbcmd, "@UserCode", DbType.String, iYearEnd.UserCode);
            db.AddInParameter(dbcmd, "@EnteredOn", DbType.DateTime, iYearEnd.EnteredOn);
            db.AddInParameter(dbcmd, "@FK_Client", DbType.Int64, iYearEnd.FK_Client);
            db.AddInParameter(dbcmd, "@process", DbType.Int64, process);
            db.AddInParameter(dbcmd, "@StockUpdateMode", DbType.Int64, iYearEnd.StockUpdateMode);

            try
     
            {
                DataTable dt = new DataTable();
                dt=db.ExecuteDataSet(dbcmd).Tables[0];
                return dt;
            }
            catch (SqlException e)
            {
               
               throw e;
            }
           
        }

        public DataTable cancelYearEnd(IYearEnd iYearEnd, int process)
        {
            Database db = DatabaseFactory.CreateDatabase("WebPharma");
            string sqlcommand = "proYearEnd";
            DbCommand dbcmd = db.GetStoredProcCommand(sqlcommand);
            db.AddInParameter(dbcmd, "@UserAction", DbType.Byte, iYearEnd.UserAction);
            //db.AddInParameter(dbcmd, "@ID_YearEnd", DbType.Int64, iYearEnd.MasterId);
            db.AddInParameter(dbcmd, "@FK_Outlet", DbType.String, iYearEnd.FK_Outlet);
            db.AddInParameter(dbcmd, "@FromDate", DbType.DateTime, iYearEnd.FromDate);
            db.AddInParameter(dbcmd, "@ToDate", DbType.DateTime, iYearEnd.ToDate);
            db.AddInParameter(dbcmd, "@UserCode", DbType.String, iYearEnd.UserCode);
            db.AddInParameter(dbcmd, "@EnteredOn", DbType.DateTime, iYearEnd.EnteredOn);
            db.AddInParameter(dbcmd, "@FK_Client", DbType.Int64, iYearEnd.FK_Client);
            db.AddInParameter(dbcmd, "@process", DbType.Int64, process);

            try
            {
                DataTable dt = new DataTable();
                dt = db.ExecuteDataSet(dbcmd).Tables[0];
                return dt;
            }
            catch (SqlException e)
            {

                throw e;
            }

        }


    }
}
