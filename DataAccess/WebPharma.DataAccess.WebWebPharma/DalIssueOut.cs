/*----------------------------------- Data Access Layer Class -----------------------------------------------------------------------------------------------------------------------
Purpose    : 
Author     :Aiswarya
Copyright  : Perfect Software Solutions (Clt) Pvt Ltd
Created on : 28/09/2018
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
    public class DalIssueOut
    {
        public Int32 UpdateIssueOut(IIssueOut issueout)
        {
            Database db = DatabaseFactory.CreateDatabase("WebPharma");
            string sqlCommand = "proIssueOutUpdate";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
            db.AddInParameter(dbCommand, "@FK_Store", DbType.Int64, issueout.FK_Store);
            db.AddInParameter(dbCommand, "@TransDate", DbType.DateTime, issueout.TransDate);
            db.AddInParameter(dbCommand, "@TotalAmt", DbType.Decimal, issueout.TotalAmt);
            db.AddInParameter(dbCommand, "@UserCode", DbType.String, issueout.UserCode);
            db.AddInParameter(dbCommand, "@FK_Client", DbType.Int64, issueout.FK_Client);
            db.AddInParameter(dbCommand, "@ID_IssueOut", DbType.Int64, issueout.MasterId);
            db.AddInParameter(dbCommand, "@UserAction", DbType.Byte, issueout.UserAction);
            db.AddInParameter(dbCommand, "@XmlIssuedItems", DbType.Xml, issueout.XmlIssuedItems);
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
        public long DeleteIssueOut(IIssueOut issueout)
        {
            Database db = DatabaseFactory.CreateDatabase("WebPharma");
            string sqlCommand = "proIssueOutDelete";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
            db.AddInParameter(dbCommand, "@ID_IssueOut", DbType.Int64, issueout.MasterId);
            db.AddInParameter(dbCommand, "@UserCode", DbType.String, issueout.UserCode);
            db.AddInParameter(dbCommand, "@FK_Client", DbType.Int64, issueout.FK_Client);
            db.AddInParameter(dbCommand, "@Fk_Reason", DbType.Int64, issueout.Fk_Reasons);
            try
            {
                return Convert.ToInt64(db.ExecuteScalar(dbCommand));
            }
            catch (SqlException e)
            {
                // UpdateErrorLog(istock, e);
                return 0;
            }
        }
        public IIssueOut SelectIssueOut(IIssueOut issueout)
        {
            Database db = DatabaseFactory.CreateDatabase("WebPharma");
            string sqlCommand = "proIssueOutSelect";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
            db.AddInParameter(dbCommand, "@ID_IssueOut", DbType.Int64, issueout.MasterId);
            db.AddInParameter(dbCommand, "@FK_Client", DbType.Int64, issueout.FK_Client);
            db.AddInParameter(dbCommand, "@FromDate", DbType.DateTime, issueout.FromDate);
            db.AddInParameter(dbCommand, "@ToDate", DbType.DateTime, issueout.ToDate);
            db.AddInParameter(dbCommand, "@FK_Store", DbType.Int64, issueout.FK_Store);
            //db.AddInParameter(dbCommand, "@IssueType", DbType.String, issueout.IssueType);
            try
            {
                using (IDataReader dataReader = db.ExecuteReader(dbCommand))
                {
                    while (dataReader.Read())
                    {
                        issueout.FK_Store = Convert.ToInt64(dataReader["FK_Store"].ToString());
                       // issueout.FK_Category = Convert.ToInt64(dataReader["FK_Category"].ToString());
                      //  issueout.IssueType = dataReader["IssueType"].ToString();
                        issueout.TransDate = DateTime.Parse(dataReader["TransDate"].ToString());
                        issueout.TotalAmt = Convert.ToDecimal(dataReader["TotalAmt"].ToString());
                        issueout.Fk_Reasons = Convert.ToInt32(dataReader["FK_Reason"].ToString());
                        
                    }
                }
                return issueout;
            }
            catch (SqlException e)
            {

                throw e;
            }

        }
        public DataTable SelectIssueOutAll(IIssueOut issueout)
        {
            DataTable dtbl = new DataTable();
            Database db = DatabaseFactory.CreateDatabase("WebPharma");
            string sqlCommand = "proIssueOutSelect";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
            db.AddInParameter(dbCommand, "@ID_IssueOut", DbType.Int64, issueout.MasterId);
            db.AddInParameter(dbCommand, "@FK_Client", DbType.Int64, issueout.FK_Client);
            db.AddInParameter(dbCommand, "@FromDate", DbType.DateTime, issueout.FromDate);
            db.AddInParameter(dbCommand, "@ToDate", DbType.DateTime, issueout.ToDate);
            db.AddInParameter(dbCommand, "@FK_Store", DbType.Int64, issueout.FK_Store);
           
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


        public DataTable SelectIssueOutItems(IIssueOut issueout)
        {
             DataTable dtbl = new DataTable();
             Database db = DatabaseFactory.CreateDatabase("WebPharma");
             string sqlCommand = "proIssueOutItemsSelect";
             DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
             db.AddInParameter(dbCommand, "@FK_Client", DbType.Int64, issueout.FK_Client);
             db.AddInParameter(dbCommand, "@FK_IssueOut", DbType.Int64, issueout.MasterId);
             db.AddInParameter(dbCommand, "@FK_Store", DbType.Int64, issueout.FK_Store);
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