using Microsoft.Practices.EnterpriseLibrary.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebPharma.Interface.WebPharma;

namespace WebPharma.DataAccess.WebPharma
{
  public  class DalReplace
    {
      public Int32 UpdateReplace(IReplace ireplacedetails)
      {
          Database db = DatabaseFactory.CreateDatabase("WebPharma");
          string sqlCommand = "proReplaceUpdate";
          DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
          db.AddInParameter(dbCommand, "@UserAction", DbType.Byte, ireplacedetails.UserAction);
          db.AddInParameter(dbCommand, "@ID_Replace", DbType.Int64, ireplacedetails.MasterId);
          db.AddInParameter(dbCommand, "@FK_Store", DbType.Int64, ireplacedetails.FK_Store);
          db.AddInParameter(dbCommand, "@FK_Supplier", DbType.Int64, ireplacedetails.FK_Supplier);
          db.AddInParameter(dbCommand, "@FK_BillType", DbType.Int64, ireplacedetails.Fk_BillType);
          db.AddInParameter(dbCommand, "@BillNo", DbType.Int64, ireplacedetails.BillNo);
          db.AddInParameter(dbCommand, "@BillDate", DbType.DateTime, ireplacedetails.RecivedDate);
          db.AddInParameter(dbCommand, "@InvoiceNo", DbType.String, ireplacedetails.InvoiceNo);
          db.AddInParameter(dbCommand, "@InvoiceDate", DbType.DateTime, ireplacedetails.InvoiceDate);
          db.AddInParameter(dbCommand, "@FK_Client", DbType.Int64, ireplacedetails.FK_Client);
          db.AddInParameter(dbCommand, "@UserCode", DbType.String, ireplacedetails.UserCode);
          db.AddInParameter(dbCommand, "@FTotalAmount", DbType.Decimal, ireplacedetails.FTotalAmount);
          db.AddInParameter(dbCommand, "@NetAmount", DbType.Decimal, ireplacedetails.NetAmount);
          db.AddInParameter(dbCommand, "@FnetAmnt", DbType.Decimal, ireplacedetails.FnetAmnt);
          db.AddInParameter(dbCommand, "@FDiscPer", DbType.Decimal, ireplacedetails.FDiscPer);
          db.AddInParameter(dbCommand, "@FDiscAmnt", DbType.Decimal, ireplacedetails.FDiscAmnt);
          db.AddInParameter(dbCommand, "@ToTotal", DbType.Decimal, ireplacedetails.TtotalAmount);
          db.AddInParameter(dbCommand, "@TonetAmnt", DbType.Decimal, ireplacedetails.TnetAmnt);
          db.AddInParameter(dbCommand, "@ToDiscountAmount", DbType.Decimal, ireplacedetails.TDiscAmnt);
          db.AddInParameter(dbCommand, "@ToDiscountPer", DbType.Decimal, ireplacedetails.TDiscPer);
          db.AddInParameter(dbCommand, "@XmlPurchaseValues", DbType.Xml, ireplacedetails.xmlPurchseValues);
          db.AddInParameter(dbCommand, "@Xmlvalues", DbType.Xml, ireplacedetails.xmlValues);
          db.AddInParameter(dbCommand, "@LnkPurReturn", DbType.Boolean, ireplacedetails.Linkbtn);
          db.AddInParameter(dbCommand, "@Fk_PurchaseReturn", DbType.Int64, ireplacedetails.Fk_PurchaseReturn);
          db.AddInParameter(dbCommand, "@TransactionID", DbType.Int64, ireplacedetails.TransactionID);
          try
          {
              return Convert.ToInt32(db.ExecuteScalar(dbCommand));
          }
          catch (SqlException e)
          {
              // UpdateErrorLog(imaterialreciept, e);
              return 0;
          }
      }
      public DataTable SelectReplace(IReplace IReplace)
      {
          DataTable dtbl = new DataTable();
          Database db = DatabaseFactory.CreateDatabase("WebPharma");
          string sqlCommand = "proReplaceSelect";
          DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
          db.AddInParameter(dbCommand, "@FK_Supplier", DbType.Int64, IReplace.FK_Supplier);
          db.AddInParameter(dbCommand, "@FK_Store", DbType.Int64, IReplace.FK_Store);
          db.AddInParameter(dbCommand, "@FK_Client", DbType.Int64, IReplace.FK_Client);
          db.AddInParameter(dbCommand, "@FromDate", DbType.DateTime, IReplace.FromDate);
          db.AddInParameter(dbCommand, "@ToDate", DbType.DateTime, IReplace.ToDate);
          db.AddInParameter(dbCommand, "@Approved", DbType.Int16, IReplace.ApproveorPending);
          db.AddInParameter(dbCommand, "@ID_Replace", DbType.Int64, IReplace.MasterId);
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
      public long ReplaceApprove(IReplace IReplace)
      {

          Database db = DatabaseFactory.CreateDatabase("WebPharma");
          string sqlCommand = "ProReplaceApprove";
          DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
          db.AddInParameter(dbCommand, "@ID_Replace", DbType.Int64, IReplace.MasterId);
          db.AddInParameter(dbCommand, "@UserCode", DbType.String, IReplace.UserCode);
          db.AddInParameter(dbCommand, "@FK_Client", DbType.String, IReplace.FK_Client);


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
      public int DeleteReplace(IReplace IReplace)
      {
          Database db = DatabaseFactory.CreateDatabase("WebPharma");
          string sqlCommand = "proReplaceDelete";
          DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
          db.AddInParameter(dbCommand, "@ID_Replace", DbType.Int64, IReplace.MasterId);
          db.AddInParameter(dbCommand, "@FK_Client", DbType.Int32, IReplace.FK_Client);
          db.AddInParameter(dbCommand, "@UserCode", DbType.String, IReplace.UserCode);
          db.AddInParameter(dbCommand, "@FK_Reason", DbType.String, IReplace.Fk_Reason);
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
      public DataTable SelectBillNo(IReplace IReplace)
      {
          DataTable dtbl = new DataTable();
          Database db = DatabaseFactory.CreateDatabase("WebPharma");
          string sqlCommand = "proReplaceBillNo";
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
      public IReplace SelectMasterData(IReplace IReplace)
      {
          Database db = DatabaseFactory.CreateDatabase("WebPharma");
          string sqlCommand = "proReplaceSelect";
          DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
          db.AddInParameter(dbCommand, "@FK_Supplier", DbType.Int64, IReplace.FK_Supplier);
          db.AddInParameter(dbCommand, "@FK_Store", DbType.Int64, IReplace.FK_Store);
          db.AddInParameter(dbCommand, "@FK_Client", DbType.Int64, IReplace.FK_Client);
          db.AddInParameter(dbCommand, "@FromDate", DbType.DateTime, IReplace.FromDate);
          db.AddInParameter(dbCommand, "@ToDate", DbType.DateTime, IReplace.ToDate);
          db.AddInParameter(dbCommand, "@Approved", DbType.Int16, IReplace.ApproveorPending);
          db.AddInParameter(dbCommand, "@ID_Replace", DbType.Int64, IReplace.MasterId);
          try
          {
              using (IDataReader dataReader = db.ExecuteReader(dbCommand))
              {
                  while (dataReader.Read())
                  {
                      IReplace.BillNo = dataReader["BillNo"].ToString();
                      IReplace.InvoiceNo = Convert.ToString(dataReader["InvoiceNo"].ToString());
                      IReplace.InvoiceDate = DateTime.Parse(dataReader["InvoiceDate"].ToString());
                      IReplace.FK_Supplier = Convert.ToInt64(dataReader["FK_Supplier"].ToString());
                      IReplace.FK_Store = Convert.ToInt64(dataReader["FK_Store"].ToString());
                      IReplace.RecivedDate = DateTime.Parse(dataReader["BillDate"].ToString());
                      IReplace.Fk_BillType = Convert.ToInt64(dataReader["Fk_BillType"].ToString());
                      IReplace.Linkbtn = Convert.ToBoolean(dataReader["LnkPurchaseReturn"].ToString());
                      IReplace.Approved = Convert.ToBoolean(dataReader["Approved"].ToString());
                      //IReplace.totalAmount = Convert.ToDecimal(dataReader["totalAmount"]);
                      //IReplace.Discount = Convert.ToDecimal(dataReader["Discount"]);
              
                  
                  }
              }
          }
          catch (SqlException ex)
          {
              throw ex;
          }
          return IReplace;
      }
      public DataSet SelectReplaceGrid(IReplace IReplace)
      {
          DataSet dtbl = new DataSet();
          Database db = DatabaseFactory.CreateDatabase("WebPharma");
          string sqlCommand = "proReplaceDetailsSelect";
          DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
          db.AddInParameter(dbCommand, "@FK_Supplier", DbType.Int64, IReplace.FK_Supplier);
          db.AddInParameter(dbCommand, "@FK_Store", DbType.Int64, IReplace.FK_Store);
          db.AddInParameter(dbCommand, "@FK_Client", DbType.Int64, IReplace.FK_Client);
        //  db.AddInParameter(dbCommand, "@fromDate", DbType.DateTime, IpurchaseReturn.FromDate);
        //  db.AddInParameter(dbCommand, "@toDate", DbType.DateTime, IpurchaseReturn.ToDate);
          db.AddInParameter(dbCommand, "@ID_Replace", DbType.Int64, IReplace.MasterId);
          try
          {
              dtbl = db.ExecuteDataSet(dbCommand);
              return dtbl;
          }
          catch (SqlException e)
          {

              throw e;
          }
      }

    }
}
