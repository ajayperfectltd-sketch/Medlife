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
   public  class DalPurchaseOrder
    {
       public Int32 UpdatePurchaseOrder(IPurchaseOrder ipurchase)
       {

           Database db = DatabaseFactory.CreateDatabase("WebPharma");
           string sqlCommand = "ProPurchaseOrderUpdate";
           DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
           db.AddInParameter(dbCommand, "@UserAction", DbType.Byte, ipurchase.UserAction);
           db.AddInParameter(dbCommand, "@ID_PurchaseOrder", DbType.Int64, ipurchase.MasterId);
           db.AddInParameter(dbCommand, "@PONo", DbType.String, ipurchase.PONo);
           db.AddInParameter(dbCommand, "@Date", DbType.DateTime, ipurchase.Date);
           db.AddInParameter(dbCommand, "@FK_Supplier", DbType.Int64, ipurchase.FK_Supplier);
           db.AddInParameter(dbCommand, "@FK_OrderType", DbType.Int64, ipurchase.FK_ItemType);
           db.AddInParameter(dbCommand, "@totalAmount", DbType.Decimal, ipurchase.totalAmount);
           db.AddInParameter(dbCommand, "@discount", DbType.Decimal, ipurchase.Discount);
           db.AddInParameter(dbCommand, "@RoundAmount", DbType.Decimal, ipurchase.RoundAmount);
           db.AddInParameter(dbCommand, "@NetAmount", DbType.Decimal, ipurchase.NetAmount);
           db.AddInParameter(dbCommand, "@Advance", DbType.Decimal, ipurchase.Advance);
           db.AddInParameter(dbCommand, "@Remarks", DbType.String, ipurchase.Remarks);
           db.AddInParameter(dbCommand, "@CashAmount", DbType.Decimal, ipurchase.CashAmount);
           db.AddInParameter(dbCommand, "@DeliveryDate", DbType.DateTime, ipurchase.DeliveryDate);
           db.AddInParameter(dbCommand, "@DeliveryType", DbType.String, ipurchase.DeliveryType);
           db.AddInParameter(dbCommand, "@Closed", DbType.Boolean, ipurchase.Closed);
           db.AddInParameter(dbCommand, "@ClosedUser", DbType.String, ipurchase.ClosedUser);
           db.AddInParameter(dbCommand, "@ClosedRemarks", DbType.String, ipurchase.ClosedRemarks);
           db.AddInParameter(dbCommand, "@UserCode", DbType.String, ipurchase.UserCode);
           db.AddInParameter(dbCommand, "@FK_Store", DbType.Int64, ipurchase.FK_Store);
           db.AddInParameter(dbCommand, "@FK_Client", DbType.Int64, ipurchase.FK_Client);
           db.AddInParameter(dbCommand, "@XmlPurchaseOrderItems", DbType.Xml, ipurchase.XmlPurchaseItems);
          

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

       public Int32 UpdatePurchaseOrderStatusOnly(IPurchaseOrder ipurchase)
       {

           Database db = DatabaseFactory.CreateDatabase("WebPharma");
           string sqlCommand = "ProPurchaseOrderUpdateStatusOnly";
           DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
           db.AddInParameter(dbCommand, "@ID_PurchaseOrder", DbType.Int64, ipurchase.MasterId);
           db.AddInParameter(dbCommand, "@Closed", DbType.Boolean, ipurchase.Closed);
           db.AddInParameter(dbCommand, "@ClosedUser", DbType.String, ipurchase.ClosedUser);
           db.AddInParameter(dbCommand, "@ClosedRemarks", DbType.String, ipurchase.ClosedRemarks);
           db.AddInParameter(dbCommand, "@UserCode", DbType.String, ipurchase.UserCode);
           db.AddInParameter(dbCommand, "@EnteredOn", DbType.DateTime, ipurchase.EnteredOn);
           db.AddInParameter(dbCommand, "@FK_Client", DbType.Int64, ipurchase.FK_Client);
        
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
       
           public IPurchaseOrder SelectData(IPurchaseOrder Ipurchase)
           {
               Database db = DatabaseFactory.CreateDatabase("WebPharma");
               string sqlCommand = "proPurchaseOrderSelect";
               DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
               db.AddInParameter(dbCommand, "@ID_PurchaseOrder", DbType.Int64, Ipurchase.MasterId);
               db.AddInParameter(dbCommand, "@FK_Client", DbType.Int64, Ipurchase.FK_Client);
               db.AddInParameter(dbCommand, "@fromDate", DbType.DateTime, Ipurchase.FromDate);
               db.AddInParameter(dbCommand, "@toDate", DbType.DateTime, Ipurchase.ToDate);
               db.AddInParameter(dbCommand, "@FK_Supplier", DbType.Int64, Ipurchase.FK_Supplier);
               db.AddInParameter(dbCommand, "@DeliveryType", DbType.String, Ipurchase.DeliveryType);
               db.AddInParameter(dbCommand, "@isClosed", DbType.String, Ipurchase.isClosed);
               db.AddInParameter(dbCommand, "@FK_Store", DbType.Int64, Ipurchase.FK_Store);
               try
               {
                   using (IDataReader dataReader = db.ExecuteReader(dbCommand))
                   {
                       while (dataReader.Read())
                       {
                           Ipurchase.PONo = dataReader["PONo"].ToString();
                           Ipurchase.Date = DateTime.Parse(dataReader["Date"].ToString());
                           Ipurchase.FK_Supplier = Convert.ToInt64(dataReader["FK_Supplier"].ToString());
                           Ipurchase.totalAmount = Convert.ToDecimal(dataReader["totalAmount"]);
                           Ipurchase.Discount = Convert.ToDecimal(dataReader["Discount"]);
                           Ipurchase.RoundAmount = Convert.ToDecimal(dataReader["RoundAmount"]);
                           Ipurchase.NetAmount = Convert.ToDecimal(dataReader["NetAmount"]);
                           Ipurchase.Advance = Convert.ToDecimal(dataReader["Advance"]);
                           Ipurchase.Remarks = dataReader["Remarks"].ToString();
                           Ipurchase.CashAmount = Convert.ToDecimal(dataReader["CashAmount"]);
                           Ipurchase.DeliveryDate = DateTime.Parse(dataReader["DeliveryDate"].ToString());
                           Ipurchase.DeliveryType = dataReader["DeliveryType"].ToString();
                           Ipurchase.Closed = bool.Parse(dataReader["Closed"].ToString());
                           Ipurchase.ClosedUser = dataReader["ClosedUser"].ToString();
                           Ipurchase.ClosedRemarks = dataReader["ClosedRemarks"].ToString();
                           Ipurchase.Cancelled = bool.Parse(dataReader["Cancelled"].ToString());
                           Ipurchase.CancelledUser =(dataReader["Cancelledby"].ToString());
                           Ipurchase.FK_Store = Convert.ToInt64(dataReader["FK_Store"]);
                           Ipurchase.FK_ItemType = Convert.ToInt64(dataReader["FK_OrderType"]);
                       }
                   }
               }
               catch (SqlException ex)
               {
                   throw ex;
               }
               return Ipurchase;
           }
           public DataTable SelectAllPurchaseOrder(IPurchaseOrder Ipurchase)
           {
               DataTable dtblPurchaseOrder = new DataTable();
               Database db = DatabaseFactory.CreateDatabase("WebPharma");
               string sqlCommand = "proPurchaseOrderSelect";
               DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
               db.AddInParameter(dbCommand, "@ID_PurchaseOrder", DbType.Int64, Ipurchase.MasterId);
               db.AddInParameter(dbCommand, "@FK_Client", DbType.Int64, Ipurchase.FK_Client);
               db.AddInParameter(dbCommand, "@fromDate", DbType.DateTime, Ipurchase.FromDate);
               db.AddInParameter(dbCommand, "@toDate", DbType.DateTime, Ipurchase.ToDate);
               db.AddInParameter(dbCommand, "@FK_Supplier", DbType.Int64, Ipurchase.FK_Supplier);
               db.AddInParameter(dbCommand, "@DeliveryType", DbType.String, Ipurchase.DeliveryType);
               db.AddInParameter(dbCommand, "@isClosed", DbType.String, Ipurchase.isClosed);
               db.AddInParameter(dbCommand, "@FK_Store", DbType.Int64, Ipurchase.FK_Store);
               try
               {
                   dtblPurchaseOrder = db.ExecuteDataSet(dbCommand).Tables[0];
                   return dtblPurchaseOrder;
               }
               catch (SqlException e)
               {
                   
                   throw e;
               }
           }

           public int DeleteStock(IPurchaseOrder Ipurchase)
           {
               Database db = DatabaseFactory.CreateDatabase("WebPharma");
               string sqlCommand = "proPurchaseOrderDelete";
               DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
               db.AddInParameter(dbCommand, "@ID_PurchaseOrder", DbType.Int64, Ipurchase.MasterId);
               db.AddInParameter(dbCommand, "@FK_Client", DbType.Int32, Ipurchase.FK_Client);
               db.AddInParameter(dbCommand, "@UserCode", DbType.String, Ipurchase.UserCode);
               db.AddInParameter(dbCommand, "@FK_Reason", DbType.Int64, Ipurchase.FK_Reason);
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
           public DataTable SelectPurchaseOrderItems(IPurchaseOrder Ipurchase)
           {
               DataTable dtblPurchaseOrderItems = new DataTable();
               Database db = DatabaseFactory.CreateDatabase("WebPharma");
               string sqlCommand = "PurchaseOrderItemsSelect";
               DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
               db.AddInParameter(dbCommand, "@FK_PurchaseOrder", DbType.Int64, Ipurchase.MasterId);
               try
               {
                   dtblPurchaseOrderItems = db.ExecuteDataSet(dbCommand).Tables[0];
                   return dtblPurchaseOrderItems;
               }
               catch (SqlException e)
               {
                   //UpdateErrorLog(istock, e);
                   throw e;
               }
           }
           public DataTable SelectPurchaseOrderSmartFillItems(IPurchaseOrder Ipurchase)
           {
               DataTable dtblPurchaseOrderItems = new DataTable();
               Database db = DatabaseFactory.CreateDatabase("WebPharma");
               string sqlCommand = "ProPurchaseOrderSmartFill";
               DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
               db.AddInParameter(dbCommand, "@FK_Store", DbType.Int64, Ipurchase.FK_Store);
               db.AddInParameter(dbCommand, "@AsonDate", DbType.DateTime, Ipurchase.Date);
               db.AddInParameter(dbCommand, "@FK_Supplier", DbType.Int64, Ipurchase.FK_Supplier);
               db.AddInParameter(dbCommand, "@ItemType", DbType.Int64, Ipurchase.FK_ItemType);
               db.AddInParameter(dbCommand, "@Quantity", DbType.Int64, Ipurchase.Quantity);
               db.AddInParameter(dbCommand, "@FK_Client", DbType.Int64, Ipurchase.FK_Client);
               try
               {
                   dtblPurchaseOrderItems = db.ExecuteDataSet(dbCommand).Tables[0];
                   return dtblPurchaseOrderItems;
               }
               catch (SqlException e)
               {
                   //UpdateErrorLog(istock, e);
                   throw e;
               }
           }


    }
}
