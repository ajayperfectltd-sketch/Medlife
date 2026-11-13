/*----------------------------------- Data Access Layer Class -----------------------------------------------------------------------------------------------------------------------
Purpose    : DAL class for State
Author     : Praveen MP
Copyright  : Perfect Software Solutions (Clt) Pvt Ltd
Created on : 21/05/2018
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
using WebPharma.DataAccess;
using Microsoft.Practices.EnterpriseLibrary.Data;
using WebPharma.Interface.Masters;
using WebPharma.Interface.General;

namespace WebPharma.DataAccess.Masters
{
    public class DalState
    {
        public Int32 UpdateState(IState iState)
        {
            Database db = DatabaseFactory.CreateDatabase("WebPharma");
            string sqlCommand = "proStateUpdate";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
            db.AddInParameter(dbCommand, "@UserAction", DbType.Byte, iState.UserAction);
            db.AddInParameter(dbCommand, "@ID_States", DbType.Int64, iState.MasterId);
            db.AddInParameter(dbCommand, "@StateName", DbType.String, iState.StateName);
            db.AddInParameter(dbCommand, "@FK_Country", DbType.Int64, iState.FK_Country);
            db.AddInParameter(dbCommand, "@UserCode", DbType.String, iState.UserCode);
            db.AddInParameter(dbCommand, "@FK_Client", DbType.Int64, iState.FK_Client);
            db.AddInParameter(dbCommand, "@StatesCode", DbType.String, iState.StateCode);
            try
            {
                return Convert.ToInt32(db.ExecuteScalar(dbCommand));
            }
            catch (SqlException e)
            {
                return 0;
            }
        }
        public int DeleteState(IState iState)
        {
            Database db = DatabaseFactory.CreateDatabase("WebPharma");
            string sqlCommand = "proStateDelete";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
            db.AddInParameter(dbCommand, "@ID_States", DbType.Int64, iState.MasterId);
            db.AddInParameter(dbCommand, "@FK_Client", DbType.Int64, iState.FK_Client);
            db.AddInParameter(dbCommand, "@UserCode", DbType.String, iState.UserCode);
            db.AddInParameter(dbCommand, "@FK_Reason", DbType.String, iState.FK_Reason);
            try
            {
                return Convert.ToInt32(db.ExecuteScalar(dbCommand));
            }
            catch (SqlException e)
            {
                throw e;
            }
        }
        public IState SelectState(IState iState)
        {
            Database db = DatabaseFactory.CreateDatabase("WebPharma");
            string sqlCommand = "proStateSelect";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
            db.AddInParameter(dbCommand, "@ID_States", DbType.Int64, iState.MasterId);
            db.AddInParameter(dbCommand, "@StateName", DbType.String, iState.StateName);
            db.AddInParameter(dbCommand, "@FK_Client", DbType.Int64, iState.FK_Client);
            db.AddInParameter(dbCommand, "@StatesCode", DbType.String, iState.StateCode);
            try
            {
                using (IDataReader dataReader = db.ExecuteReader(dbCommand))
                {
                    while (dataReader.Read())
                    {
                        iState.FK_Country = Convert.ToInt32(dataReader["FK_Country"].ToString());
                        iState.StateName = dataReader["StateName"].ToString();
                        iState.StateCode = dataReader["StatesCode"].ToString(); 
                      
                    }
                }
                return iState;
            }
            catch (SqlException e)
            {
                throw e;
            }
        }
        public DataTable SelectAllState(IState iState)
        {
            DataTable dtbl = new DataTable();
            Database db = DatabaseFactory.CreateDatabase("WebPharma");
            string sqlCommand = "proStateSelect";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
            db.AddInParameter(dbCommand, "@ID_States", DbType.Int64, iState.MasterId);
            db.AddInParameter(dbCommand, "@StateName", DbType.String, iState.StateName);
            db.AddInParameter(dbCommand, "@FK_Client", DbType.Int64, iState.FK_Client);
            db.AddInParameter(dbCommand, "@StatesCode", DbType.String, iState.StateCode);
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
