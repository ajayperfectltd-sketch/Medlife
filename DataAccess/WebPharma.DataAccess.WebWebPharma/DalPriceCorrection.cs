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
   public class DalPriceCorrection
    {
       public Int32 UpdatepriceCorrection(IPriceCorrection IPriceCorrection)
       {
           Database db = DatabaseFactory.CreateDatabase("WebPharma");
           string sqlCommand = "proPriceCorrectionUpdate";
           DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
           db.AddInParameter(dbCommand, "@UserAction", DbType.Byte, IPriceCorrection.UserAction);
           db.AddInParameter(dbCommand, "@ID_PriceCorction", DbType.Int64, IPriceCorrection.MasterId);
           db.AddInParameter(dbCommand, "@FK_Store", DbType.Int64, IPriceCorrection.FK_Store);
           db.AddInParameter(dbCommand, "@PriceConDate", DbType.DateTime, IPriceCorrection.PriceCorrectionDate);
           db.AddInParameter(dbCommand, "@FK_Client", DbType.Int64, IPriceCorrection.FK_Client);
           db.AddInParameter(dbCommand, "@UserCode", DbType.String, IPriceCorrection.UserCode);
           db.AddInParameter(dbCommand, "@XmlData", DbType.String, IPriceCorrection.xmlGriddata);


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
       public DataTable SelectPriceCorrectionData(IPriceCorrection IPriceCorrection)
       {
           DataTable dtbl = new DataTable();
           Database db = DatabaseFactory.CreateDatabase("WebPharma");
           string sqlCommand = "proPriceCorrectionSelect";
           DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

           db.AddInParameter(dbCommand, "@FK_Store", DbType.Int64, IPriceCorrection.FK_Store);
           db.AddInParameter(dbCommand, "@FK_Client", DbType.Int64, IPriceCorrection.FK_Client);
           db.AddInParameter(dbCommand, "@fromDate", DbType.DateTime, IPriceCorrection.FromDate);
           db.AddInParameter(dbCommand, "@toDate", DbType.DateTime, IPriceCorrection.ToDate);
           db.AddInParameter(dbCommand, "@ID_priceCorrection", DbType.Int64, IPriceCorrection.MasterId);
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
       public IPriceCorrection SelectMasterData(IPriceCorrection IPrice)
       {
           Database db = DatabaseFactory.CreateDatabase("WebPharma");
           string sqlCommand = "proPriceCorrectionSelect";
           DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

           db.AddInParameter(dbCommand, "@FK_Store", DbType.Int64, IPrice.FK_Store);
           db.AddInParameter(dbCommand, "@FK_Client", DbType.Int64, IPrice.FK_Client);
           db.AddInParameter(dbCommand, "@fromDate", DbType.DateTime, IPrice.FromDate);
           db.AddInParameter(dbCommand, "@toDate", DbType.DateTime, IPrice.ToDate);
           db.AddInParameter(dbCommand, "@ID_priceCorrection", DbType.Int64, IPrice.MasterId);
           try
           {
               using (IDataReader dataReader = db.ExecuteReader(dbCommand))
               {
                   while (dataReader.Read())
                   {
                       IPrice.PriceCorrectionDate = Convert.ToDateTime(dataReader["PricecorrectionDate"].ToString());
                       IPrice.FK_Store = Convert.ToInt64(dataReader["FK_Store"].ToString());

                   }
               }
           }
           catch (SqlException ex)
           {
               throw ex;
           }
           return IPrice;
       }
       public DataSet SelectPriceCorrectionDetailsGrid(IPriceCorrection IPrice)
       {
           DataSet dtbl = new DataSet();
           Database db = DatabaseFactory.CreateDatabase("WebPharma");
           string sqlCommand = "proPriceCorrectionDetailsSelect";
           DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

           db.AddInParameter(dbCommand, "@FK_Store", DbType.Int64, IPrice.FK_Store);
           db.AddInParameter(dbCommand, "@FK_Client", DbType.Int64, IPrice.FK_Client);
           //  db.AddInParameter(dbCommand, "@fromDate", DbType.DateTime, IpurchaseReturn.FromDate);
           //  db.AddInParameter(dbCommand, "@toDate", DbType.DateTime, IpurchaseReturn.ToDate);
           db.AddInParameter(dbCommand, "@ID_priceCorrection", DbType.Int64, IPrice.MasterId);
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

       public int DeletePriceCorrection(IPriceCorrection IPrice)
       {
           Database db = DatabaseFactory.CreateDatabase("WebPharma");
           string sqlCommand = "proPriceCorrectionDelete";
           DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
           db.AddInParameter(dbCommand, "@ID_priceCorrection", DbType.Int64, IPrice.MasterId);
           db.AddInParameter(dbCommand, "@FK_Client", DbType.Int32, IPrice.FK_Client);
           db.AddInParameter(dbCommand, "@UserCode", DbType.String, IPrice.UserCode);
           db.AddInParameter(dbCommand, "@FK_Reason", DbType.String, IPrice.FK_Reason);
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


    }
}
