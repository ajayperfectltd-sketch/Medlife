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
    public class DalPurchaseApprove
    {


        public DataTable PurchaseApprove(IPurchaseApprove ipurchaseapprove)
        {
            var dt = new DataTable();
            Database db = DatabaseFactory.CreateDatabase("WebPharma");
            string sqlCommand = "proPurchaseApprove";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
            db.AddInParameter(dbCommand, "@ID_Purchase", DbType.Int64, ipurchaseapprove.MasterId);
            db.AddInParameter(dbCommand, "@FK_BillType", DbType.Int64, ipurchaseapprove.FK_BillType);
            //   db.AddInParameter(dbCommand, "@FK_Supplier", DbType.Int64, ipurchaseapprove.FK_Supplier);
            db.AddInParameter(dbCommand, "@FK_Client", DbType.Int64, ipurchaseapprove.FK_Client);
            db.AddInParameter(dbCommand, "@BillNo", DbType.Int64, ipurchaseapprove.BillNo);
            db.AddInParameter(dbCommand, "@BillDate", DbType.DateTime, ipurchaseapprove.BillDate);
            db.AddInParameter(dbCommand, "@PurchaseAmt", DbType.Decimal, ipurchaseapprove.PurchaseAmt);
            db.AddInParameter(dbCommand, "@RoundOff", DbType.Decimal, ipurchaseapprove.RoundOff);
            db.AddInParameter(dbCommand, "@DiscPer", DbType.Decimal, ipurchaseapprove.DiscPer);
            db.AddInParameter(dbCommand, "@DiscAmt", DbType.Decimal, ipurchaseapprove.DiscAmt);
            db.AddInParameter(dbCommand, "@NetAmount", DbType.Decimal, ipurchaseapprove.NetAmount);
            db.AddInParameter(dbCommand, "@Advance", DbType.Decimal, ipurchaseapprove.Advance);
            db.AddInParameter(dbCommand, "@Credit", DbType.Boolean, ipurchaseapprove.Credit);
            db.AddInParameter(dbCommand, "@PaymentMode", DbType.Int16, ipurchaseapprove.PaymentMode);
            db.AddInParameter(dbCommand, "@AhCode", DbType.Int64, ipurchaseapprove.FK_Accounthead);
            db.AddInParameter(dbCommand, "@AhsCode", DbType.Int64, ipurchaseapprove.FK_AccountheadSub);
            db.AddInParameter(dbCommand, "@ChequeNo", DbType.String, ipurchaseapprove.Chequeno);
            db.AddInParameter(dbCommand, "@ChequeDate", DbType.DateTime, ipurchaseapprove.ChequeDate);
            db.AddInParameter(dbCommand, "@Remarks", DbType.String, ipurchaseapprove.Remarks);
            db.AddInParameter(dbCommand, "@UserCode", DbType.String, ipurchaseapprove.UserCode);
            db.AddInParameter(dbCommand, "@State", DbType.Int16, ipurchaseapprove.State);

            try
            {
                var ds = db.ExecuteDataSet(dbCommand); 
                if (ds.Tables.Count > 0)
                {
                    dt = ds.Tables[0];
                } return dt;
               
            }
            catch (SqlException e)
            {
                // UpdateErrorLog(istock, e);
                return dt;
            }


        }

        public long proGetPurchaseApproveBillNo(IPurchaseApprove ipurchaseapprove)
        {

            Database db = DatabaseFactory.CreateDatabase("WebPharma");
            string sqlCommand = "ProGetNextPurchaseBillNo";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
            db.AddInParameter(dbCommand, "@FK_BillType", DbType.Int64, ipurchaseapprove.FK_BillType);
            db.AddInParameter(dbCommand, "@FK_Store", DbType.Int64, ipurchaseapprove.FK_Store);
            db.AddInParameter(dbCommand, "@FK_Client", DbType.Int32, ipurchaseapprove.FK_Client);



            try
            {
                return Convert.ToInt64(db.ExecuteScalar(dbCommand));

            }
            catch (SqlException e)
            {

                return 0;
            }
        }


    }
}
