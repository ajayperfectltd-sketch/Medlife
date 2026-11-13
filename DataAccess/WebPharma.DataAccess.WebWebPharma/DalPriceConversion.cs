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
    public class DalPriceConversion
    {

        public Int32 UpdatePriceConversion(IPriceConversion iPriceConversion)
        {
            Database db = DatabaseFactory.CreateDatabase("WebPharma");
            string sqlCommand = "ProAutoStockConvertion";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

            db.AddInParameter(dbCommand, "@TransDate", DbType.DateTime, iPriceConversion.TransDate);
            db.AddInParameter(dbCommand, "@FK_Store", DbType.Int64, iPriceConversion.FK_Store);
            db.AddInParameter(dbCommand, "@FK_Category", DbType.Int64, iPriceConversion.FK_Category);
            db.AddInParameter(dbCommand, "@MinimumQty", DbType.Decimal, iPriceConversion.MinimumQty);
            db.AddInParameter(dbCommand, "@PriceTag", DbType.Int64, iPriceConversion.PriceTag);
            db.AddInParameter(dbCommand, "@FK_Client", DbType.Int64, iPriceConversion.FK_Client);
            db.AddInParameter(dbCommand, "@UserCode", DbType.String, iPriceConversion.UserCode);
            db.AddInParameter(dbCommand, "@XmlItems", DbType.Xml, iPriceConversion.XmlItems);
            db.AddInParameter(dbCommand, "@FK_Items", DbType.Int64, iPriceConversion.FK_Items);
          
          

          

            try
            {
                return Convert.ToInt32(db.ExecuteScalar(dbCommand));
            }
            catch (SqlException e)
            {
                // UpdateErrorLog(iPriceConversion, e);
                return 0;
            }
        }
     

    }
}
