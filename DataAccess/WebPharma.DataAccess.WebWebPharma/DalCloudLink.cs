using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.Common;
using System.Data.Sql;
using WebPharma.DataAccess;
using WebPharma.Interface.General;
using WebPharma.Interface.WebPharma;
using System.Data.SqlClient;
using System.IO;
using Microsoft.Practices.EnterpriseLibrary.Data;

namespace WebPharma.DataAccess.WebPharma
{
    public class DalCloudLink
    {
        public DataSet SelectImportandExportData(ICloudLink iCloudLink)
        {
            DataSet dtblCloudLink = new DataSet();
            Database db = DatabaseFactory.CreateDatabase("WebPharma");
            string sqlCommand = "proSelectImportAndExportData";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
            db.AddInParameter(dbCommand, "@FK_Client", DbType.Int64, iCloudLink.FK_Client);
            db.AddInParameter(dbCommand, "@UserCode", DbType.String, iCloudLink.UserCode);
            db.AddInParameter(dbCommand, "@Mode", DbType.Int16, iCloudLink.Mode);
            db.AddInParameter(dbCommand, "@MasterId", DbType.Int64, iCloudLink.MasterId);
            db.AddInParameter(dbCommand, "@IDProcessExport", DbType.Int64, iCloudLink.IDProcessExport);
            db.AddInParameter(dbCommand, "@XmlErrorCode", DbType.String, iCloudLink.XmlErrorCode);
            try
            {
                dtblCloudLink = db.ExecuteDataSet(dbCommand);
                return dtblCloudLink;
            }
            catch (SqlException e)
            {
                throw e;
            }
        }
        public DataTable SelectExportDetails(ICloudLink iCloudLink)
        {
            DataTable dtblCloudLink = new DataTable();
            Database db = DatabaseFactory.CreateDatabase("WebPharma");
            string sqlCommand = "proSelectExportData";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
            db.AddInParameter(dbCommand, "@FK_Client", DbType.Int64, iCloudLink.FK_Client);
            db.AddInParameter(dbCommand, "@UserCode", DbType.String, iCloudLink.UserCode);


            try
            {
                dtblCloudLink = db.ExecuteDataSet(dbCommand).Tables[0];
                return dtblCloudLink;
            }
            catch (SqlException e)
            {
                throw e;
            }
        }
        public DataTable SelectImportDetails(ICloudLink iCloudLink)
        {
            DataTable dtblCloudLink = new DataTable();
            Database db = DatabaseFactory.CreateDatabase("WebPharma");
            string sqlCommand = "proSelectImportData";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
            db.AddInParameter(dbCommand, "@FK_Client", DbType.Int64, iCloudLink.FK_Client);
            db.AddInParameter(dbCommand, "@UserCode", DbType.String, iCloudLink.UserCode);
            db.AddInParameter(dbCommand, "@Mode", DbType.Int16, iCloudLink.Mode);

            try
            {
                dtblCloudLink = db.ExecuteDataSet(dbCommand).Tables[0];
                return dtblCloudLink;
            }
            catch (SqlException e)
            {
                throw e;
            }
        }
        public DataTable InsertImportDetails(ICloudLink iCloudLink)
        {
            DataTable dtblCloudLink = new DataTable();
            Database db = DatabaseFactory.CreateDatabase("WebPharma");
            string sqlCommand = "proInsertImportDetails";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
            db.AddInParameter(dbCommand, "@FK_Client", DbType.Int64, iCloudLink.FK_Client);
            db.AddInParameter(dbCommand, "@XmlExportCloudDetails", DbType.String, iCloudLink.XmlExportCloudDetails);


            try
            {
                dtblCloudLink = db.ExecuteDataSet(dbCommand).Tables[0];
                return dtblCloudLink;
            }
            catch (SqlException e)
            {
                throw e;
            }
        }
        public DataTable SelectMasterCountDetails(ICloudLink iCloudLink)
        {
            DataTable dtblCloudLink = new DataTable();
            Database db = DatabaseFactory.CreateDatabase("WebPharma");
            string sqlCommand = "ProGetAllScoreLinkCount";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
            db.AddInParameter(dbCommand, "@FK_Client", DbType.Int64, iCloudLink.FK_Client);
            db.AddInParameter(dbCommand, "@XmlImportCloudDetails", DbType.String, iCloudLink.XmlExportCloudDetails);
            db.AddInParameter(dbCommand, "@Mode", DbType.Int16, iCloudLink.Mode);
            db.AddInParameter(dbCommand, "@UserCode", DbType.String, iCloudLink.UserCode);
            try
            {
                dtblCloudLink = db.ExecuteDataSet(dbCommand).Tables[0];
                return dtblCloudLink;
            }
            catch (SqlException e)
            {
                throw e;
            }
        }
        public DataSet SelectExportDataDetail(ICloudLink iCloudLink)
        {
            DataSet dtblCloudLink = new DataSet();
            Database db = DatabaseFactory.CreateDatabase("WebPharma");
            string sqlCommand = "proSelectExportDataDetail";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
            db.AddInParameter(dbCommand, "@FK_Client", DbType.Int64, iCloudLink.FK_Client);
            db.AddInParameter(dbCommand, "@UserCode", DbType.String, iCloudLink.UserCode);
            db.AddInParameter(dbCommand, "@Mode", DbType.Int16, iCloudLink.Mode);
            db.AddInParameter(dbCommand, "@Tdate", DbType.DateTime, iCloudLink.Tdate);
            db.AddInParameter(dbCommand, "@XmlExportCloudDetails", DbType.String, iCloudLink.XmlExportCloudDetails);
            db.AddInParameter(dbCommand, "@SupplierWise", DbType.Boolean, iCloudLink.SupplierWise);
            db.AddInParameter(dbCommand, "@Recovery", DbType.Boolean, iCloudLink.Recovery);
            db.AddInParameter(dbCommand, "@SaleMan", DbType.Boolean, iCloudLink.SalesMan);
            try
            {
                //dtblCloudLink = db.ExecuteDataSet(dbCommand).Tables[0];
                dtblCloudLink = db.ExecuteDataSet(dbCommand);
                return dtblCloudLink;
            }
            catch (SqlException e)
            {
                throw e;
            }
        }

        public ICloudLink InsertImportMasterAccountGroupDataDetail(ICloudLink iCloudLink)
        {
            DataTable dtblCloudLink = new DataTable();
            Database db = DatabaseFactory.CreateDatabase("WebPharma");
            string sqlCommand = "proAPIInsertAccountGroup";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
            db.AddInParameter(dbCommand, "@FK_Client", DbType.Int64, iCloudLink.FK_Client);
            db.AddInParameter(dbCommand, "@XmldtAccountGroup", DbType.String, (iCloudLink.XmldtAccountGroup));


            try
            {
                using (IDataReader dataReader = db.ExecuteReader(dbCommand))
                {
                    while (dataReader.Read())
                    {


                        iCloudLink.ReturnId = dataReader["ErrCode"].ToString() == null ? 0 : Convert.ToInt64(dataReader["ErrCode"].ToString());
                        iCloudLink.ReturnMessage = Convert.ToString(dataReader["ErrMsg"].ToString());

                    }
                }
                return iCloudLink;
            }
            catch (SqlException e)
            {
                throw;
            }
        }
        public ICloudLink InsertImportMasterAccountsubGroupDataDetail(ICloudLink iCloudLink)
        {
            DataTable dtblCloudLink = new DataTable();
            Database db = DatabaseFactory.CreateDatabase("WebPharma");
            string sqlCommand = "proAPIInsertAccountsubGroup";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
            db.AddInParameter(dbCommand, "@FK_Client", DbType.Int64, iCloudLink.FK_Client);
            db.AddInParameter(dbCommand, "@XmldtAccountsubGroup", DbType.String, (iCloudLink.XmldtAccountsubGroup));


            try
            {
                using (IDataReader dataReader = db.ExecuteReader(dbCommand))
                {
                    while (dataReader.Read())
                    {


                        iCloudLink.ReturnId = dataReader["ErrCode"].ToString() == null ? 0 : Convert.ToInt64(dataReader["ErrCode"].ToString());
                        iCloudLink.ReturnMessage = Convert.ToString(dataReader["ErrMsg"].ToString());

                    }
                }
                return iCloudLink;
            }
            catch (SqlException e)
            {
                throw;
            }
        }

        public ICloudLink InsertMasterAccountBehaviourData(ICloudLink iCloudLink)
        {
            DataTable dtblCloudLink = new DataTable();
            Database db = DatabaseFactory.CreateDatabase("WebPharma");
            string sqlCommand = "proAPIInsertAccountBehaviour";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
            db.AddInParameter(dbCommand, "@FK_Client", DbType.Int64, iCloudLink.FK_Client);
            db.AddInParameter(dbCommand, "@XmldtBehaviour", DbType.String, (iCloudLink.XmldtBehaviour));


            try
            {
                using (IDataReader dataReader = db.ExecuteReader(dbCommand))
                {
                    while (dataReader.Read())
                    {


                        iCloudLink.ReturnId = dataReader["ErrCode"].ToString() == null ? 0 : Convert.ToInt64(dataReader["ErrCode"].ToString());
                        iCloudLink.ReturnMessage = Convert.ToString(dataReader["ErrMsg"].ToString());

                    }
                }
                return iCloudLink;
            }
            catch (SqlException e)
            {
                throw;
            }
        }
        public ICloudLink InsertImportMasterAccountHeadData(ICloudLink iCloudLink)
        {
            DataTable dtblCloudLink = new DataTable();
            Database db = DatabaseFactory.CreateDatabase("WebPharma");
            string sqlCommand = "proAPIInsertAccountHead";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
            db.AddInParameter(dbCommand, "@FK_Client", DbType.Int64, iCloudLink.FK_Client);
            db.AddInParameter(dbCommand, "@XmldtAccountHead", DbType.String, (iCloudLink.XmldtAccountHead));


            try
            {
                using (IDataReader dataReader = db.ExecuteReader(dbCommand))
                {
                    while (dataReader.Read())
                    {


                        iCloudLink.ReturnId = dataReader["ErrCode"].ToString() == null ? 0 : Convert.ToInt64(dataReader["ErrCode"].ToString());
                        iCloudLink.ReturnMessage = Convert.ToString(dataReader["ErrMsg"].ToString());

                    }
                }
                return iCloudLink;
            }
            catch (SqlException e)
            {
                throw;
            }
        }

        public ICloudLink InsertImportMasterAccountHeadSubData(ICloudLink iCloudLink)
        {
            DataTable dtblCloudLink = new DataTable();
            Database db = DatabaseFactory.CreateDatabase("WebPharma");
            string sqlCommand = "proAPIInsertAccountHeadSubData";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
            db.AddInParameter(dbCommand, "@FK_Client", DbType.Int64, iCloudLink.FK_Client);
            db.AddInParameter(dbCommand, "@XmldtAccountHeadSub", DbType.String, (iCloudLink.XmldtAccountHeadSub));


            try
            {
                using (IDataReader dataReader = db.ExecuteReader(dbCommand))
                {
                    while (dataReader.Read())
                    {


                        iCloudLink.ReturnId = dataReader["ErrCode"].ToString() == null ? 0 : Convert.ToInt64(dataReader["ErrCode"].ToString());
                        iCloudLink.ReturnMessage = Convert.ToString(dataReader["ErrMsg"].ToString());

                    }
                }
                return iCloudLink;
            }
            catch (SqlException e)
            {
                throw;
            }
        }

        public ICloudLink InsertImportMasterStateData(ICloudLink iCloudLink)
        {
            DataTable dtblCloudLink = new DataTable();
            Database db = DatabaseFactory.CreateDatabase("WebPharma");
            string sqlCommand = "proAPIInsertState";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
            db.AddInParameter(dbCommand, "@FK_Client", DbType.Int64, iCloudLink.FK_Client);
            db.AddInParameter(dbCommand, "@XmldtState", DbType.String, (iCloudLink.XmldtState));


            try
            {
                using (IDataReader dataReader = db.ExecuteReader(dbCommand))
                {
                    while (dataReader.Read())
                    {


                        iCloudLink.ReturnId = dataReader["ErrCode"].ToString() == null ? 0 : Convert.ToInt64(dataReader["ErrCode"].ToString());
                        iCloudLink.ReturnMessage = Convert.ToString(dataReader["ErrMsg"].ToString());

                    }
                }
                return iCloudLink;
            }
            catch (SqlException e)
            {
                throw;
            }
        }
        public ICloudLink InsertImportMasterDistrictData(ICloudLink iCloudLink)
        {
            DataTable dtblCloudLink = new DataTable();
            Database db = DatabaseFactory.CreateDatabase("WebPharma");
            string sqlCommand = "proAPIInsertDistrict";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
            db.AddInParameter(dbCommand, "@FK_Client", DbType.Int64, iCloudLink.FK_Client);
            db.AddInParameter(dbCommand, "@XmldtDistrict", DbType.String, (iCloudLink.XmldtDistrict));


            try
            {
                using (IDataReader dataReader = db.ExecuteReader(dbCommand))
                {
                    while (dataReader.Read())
                    {


                        iCloudLink.ReturnId = dataReader["ErrCode"].ToString() == null ? 0 : Convert.ToInt64(dataReader["ErrCode"].ToString());
                        iCloudLink.ReturnMessage = Convert.ToString(dataReader["ErrMsg"].ToString());

                    }
                }
                return iCloudLink;
            }
            catch (SqlException e)
            {
                throw;
            }
        }
        public ICloudLink InsertImportMasterPlaceData(ICloudLink iCloudLink)
        {
            DataTable dtblCloudLink = new DataTable();
            Database db = DatabaseFactory.CreateDatabase("WebPharma");
            string sqlCommand = "proAPIInsertPlace";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
            db.AddInParameter(dbCommand, "@FK_Client", DbType.Int64, iCloudLink.FK_Client);
            db.AddInParameter(dbCommand, "@XmldtPlace", DbType.String, (iCloudLink.XmldtPlace));


            try
            {
                using (IDataReader dataReader = db.ExecuteReader(dbCommand))
                {
                    while (dataReader.Read())
                    {


                        iCloudLink.ReturnId = dataReader["ErrCode"].ToString() == null ? 0 : Convert.ToInt64(dataReader["ErrCode"].ToString());
                        iCloudLink.ReturnMessage = Convert.ToString(dataReader["ErrMsg"].ToString());

                    }
                }
                return iCloudLink;
            }
            catch (SqlException e)
            {
                throw;
            }
        }
        public ICloudLink InsertImportMasterPostData(ICloudLink iCloudLink)
        {
            DataTable dtblCloudLink = new DataTable();
            Database db = DatabaseFactory.CreateDatabase("WebPharma");
            string sqlCommand = "proAPIInsertPost";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
            db.AddInParameter(dbCommand, "@FK_Client", DbType.Int64, iCloudLink.FK_Client);
            db.AddInParameter(dbCommand, "@XmldtPost", DbType.String, (iCloudLink.XmldtPost));


            try
            {
                using (IDataReader dataReader = db.ExecuteReader(dbCommand))
                {
                    while (dataReader.Read())
                    {


                        iCloudLink.ReturnId = dataReader["ErrCode"].ToString() == null ? 0 : Convert.ToInt64(dataReader["ErrCode"].ToString());
                        iCloudLink.ReturnMessage = Convert.ToString(dataReader["ErrMsg"].ToString());

                    }
                }
                return iCloudLink;
            }
            catch (SqlException e)
            {
                throw;
            }
        }
        public ICloudLink InsertImportMasterIdentityProofData(ICloudLink iCloudLink)
        {
            DataTable dtblCloudLink = new DataTable();
            Database db = DatabaseFactory.CreateDatabase("WebPharma");
            string sqlCommand = "proAPIInsertIdentityProof";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
            db.AddInParameter(dbCommand, "@FK_Client", DbType.Int64, iCloudLink.FK_Client);
            db.AddInParameter(dbCommand, "@XmldtIdentityProof", DbType.String, (iCloudLink.XmldtIdentityProof));


            try
            {
                using (IDataReader dataReader = db.ExecuteReader(dbCommand))
                {
                    while (dataReader.Read())
                    {


                        iCloudLink.ReturnId = dataReader["ErrCode"].ToString() == null ? 0 : Convert.ToInt64(dataReader["ErrCode"].ToString());
                        iCloudLink.ReturnMessage = Convert.ToString(dataReader["ErrMsg"].ToString());

                    }
                }
                return iCloudLink;
            }
            catch (SqlException e)
            {
                throw;
            }
        }
        public ICloudLink InsertImportMasterCustomerTypeData(ICloudLink iCloudLink)
        {
            DataTable dtblCloudLink = new DataTable();
            Database db = DatabaseFactory.CreateDatabase("WebPharma");
            string sqlCommand = "proAPIInsertCustomerType";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
            db.AddInParameter(dbCommand, "@FK_Client", DbType.Int64, iCloudLink.FK_Client);
            db.AddInParameter(dbCommand, "@XmldtCustomerType", DbType.String, (iCloudLink.XmldtCustomerType));


            try
            {
                using (IDataReader dataReader = db.ExecuteReader(dbCommand))
                {
                    while (dataReader.Read())
                    {


                        iCloudLink.ReturnId = dataReader["ErrCode"].ToString() == null ? 0 : Convert.ToInt64(dataReader["ErrCode"].ToString());
                        iCloudLink.ReturnMessage = Convert.ToString(dataReader["ErrMsg"].ToString());

                    }
                }
                return iCloudLink;
            }
            catch (SqlException e)
            {
                throw;
            }
        }
        public ICloudLink InsertImportMasterCustomerData(ICloudLink iCloudLink)
        {
            DataTable dtblCloudLink = new DataTable();
            Database db = DatabaseFactory.CreateDatabase("WebPharma");
            string sqlCommand = "proAPIInsertCustomer";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
            db.AddInParameter(dbCommand, "@FK_Client", DbType.Int64, iCloudLink.FK_Client);
            db.AddInParameter(dbCommand, "@XmldtCustomer", DbType.String, (iCloudLink.XmldtCustomer));


            try
            {
                using (IDataReader dataReader = db.ExecuteReader(dbCommand))
                {
                    while (dataReader.Read())
                    {


                        iCloudLink.ReturnId = dataReader["ErrCode"].ToString() == null ? 0 : Convert.ToInt64(dataReader["ErrCode"].ToString());
                        iCloudLink.ReturnMessage = Convert.ToString(dataReader["ErrMsg"].ToString());

                    }
                }
                return iCloudLink;
            }
            catch (SqlException e)
            {
                throw;
            }
        }
        public ICloudLink InsertImportMasterSupplierData(ICloudLink iCloudLink)
        {
            DataTable dtblCloudLink = new DataTable();
            Database db = DatabaseFactory.CreateDatabase("WebPharma");
            string sqlCommand = "proAPIInsertSupplierData";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
            db.AddInParameter(dbCommand, "@FK_Client", DbType.Int64, iCloudLink.FK_Client);
            db.AddInParameter(dbCommand, "@XmldtSupplier", DbType.String, (iCloudLink.XmldtSupplier));


            try
            {
                using (IDataReader dataReader = db.ExecuteReader(dbCommand))
                {
                    while (dataReader.Read())
                    {


                        iCloudLink.ReturnId = dataReader["ErrCode"].ToString() == null ? 0 : Convert.ToInt64(dataReader["ErrCode"].ToString());
                        iCloudLink.ReturnMessage = Convert.ToString(dataReader["ErrMsg"].ToString());

                    }
                }
                return iCloudLink;
            }
            catch (SqlException e)
            {
                throw;
            }
        }
        public ICloudLink InsertImportMasterAgentData(ICloudLink iCloudLink)
        {
            DataTable dtblCloudLink = new DataTable();
            Database db = DatabaseFactory.CreateDatabase("WebPharma");
            string sqlCommand = "proAPIInsertAgentData";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
            db.AddInParameter(dbCommand, "@FK_Client", DbType.Int64, iCloudLink.FK_Client);
            db.AddInParameter(dbCommand, "@XmldtAgent", DbType.String, (iCloudLink.XmldtAgent));


            try
            {
                using (IDataReader dataReader = db.ExecuteReader(dbCommand))
                {
                    while (dataReader.Read())
                    {


                        iCloudLink.ReturnId = dataReader["ErrCode"].ToString() == null ? 0 : Convert.ToInt64(dataReader["ErrCode"].ToString());
                        iCloudLink.ReturnMessage = Convert.ToString(dataReader["ErrMsg"].ToString());

                    }
                }
                return iCloudLink;
            }
            catch (SqlException e)
            {
                throw;
            }
        }
        public ICloudLink InsertImportRecoveryData(ICloudLink iCloudLink)
        {
            DataTable dtblCloudLink = new DataTable();
            Database db = DatabaseFactory.CreateDatabase("WebPharma");
            string sqlCommand = "proAPIInsertRecoveryData";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
            db.AddInParameter(dbCommand, "@FK_Client", DbType.Int64, iCloudLink.FK_Client);
            db.AddInParameter(dbCommand, "@XmldtRecovery", DbType.String, (iCloudLink.XmldtRecovery));


            try
            {
                using (IDataReader dataReader = db.ExecuteReader(dbCommand))
                {
                    while (dataReader.Read())
                    {


                        iCloudLink.ReturnId = dataReader["ErrCode"].ToString() == null ? 0 : Convert.ToInt64(dataReader["ErrCode"].ToString());
                        iCloudLink.ReturnMessage = Convert.ToString(dataReader["ErrMsg"].ToString());

                    }
                }
                return iCloudLink;
            }
            catch (SqlException e)
            {
                throw;
            }
        }
        public static string DsToXml(DataSet ds)
        {
            StringBuilder Xml = new StringBuilder();
            int i = 0;
            Xml.Append("<root>");
            foreach (DataTable dt in ds.Tables)
            {
                if (dt.Rows.Count > 0)
                    Xml.Append(WriteToXml(dt, dt.TableName));
                else
                    Xml.Append("<Table" + i + ">" + "</Table" + i + ">");
                i++;
            }
            Xml.Append("</root>");
            return Xml.ToString();
        }
        public static string WriteToXml(DataTable dt, string tablename)
        {
            StringWriter strxml = new StringWriter();
            {
                try
                {
                    dt.TableName = tablename;
                    dt.WriteXml(strxml, XmlWriteMode.IgnoreSchema, true);
                }
                catch (Exception)
                {
                    throw;
                }
                return strxml.ToString().Replace(tablename, "row").Replace("NewDataSet", tablename);
            }
        }
    }
}
