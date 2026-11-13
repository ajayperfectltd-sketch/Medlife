/*----------------------------------- Data Access Layer Class -----------------------------------------------------------------------------------------------------------------------
Purpose    : 
Author     : Bibin TC
Copyright  : Perfect Software Solutions (Clt) Pvt Ltd
Created on : 14/02/2014
----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
WebPharma
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
   public class DalIdentityProof
    {

       public Int32 UpdateIdentityproof(IIdentityProof iAccGrp)
       {
           Database db = DatabaseFactory.CreateDatabase("WebPharma");
           string sqlCommand = "proIdentityProofUpdate";
           DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
           db.AddInParameter(dbCommand, "@ID_IdentityProof", DbType.Int64, iAccGrp.MasterId);
           db.AddInParameter(dbCommand, "@IdpName", DbType.String, iAccGrp.Name);
           db.AddInParameter(dbCommand, "@IdpType", DbType.Int64, iAccGrp.Type);
           db.AddInParameter(dbCommand, "@IdpDescription", DbType.String, iAccGrp.Descrption);
           db.AddInParameter(dbCommand, "@SortOrder", DbType.Int16, iAccGrp.SortOrder);
           db.AddInParameter(dbCommand, "@AddressProof", DbType.Boolean, iAccGrp.AdressProof);
           db.AddInParameter(dbCommand, "@IdProof", DbType.Boolean, iAccGrp.IdProof);
           db.AddInParameter(dbCommand, "@UserCode", DbType.String, iAccGrp.UserCode);
           db.AddInParameter(dbCommand, "@FK_Client", DbType.Int64, iAccGrp.FK_Client);
          db.AddInParameter(dbCommand, "@UserAction", DbType.Byte, iAccGrp.UserAction);
           try
           {
               return Convert.ToInt32(db.ExecuteScalar(dbCommand));
           }
           catch (SqlException e)
           {
               return 0;
           }




       }





       public DataTable SelectAllidentityproof(IIdentityProof iIdentity)
       {

           DataTable dtbl = new DataTable();
           Database db = DatabaseFactory.CreateDatabase("WebPharma");
           string sqlCommand = "proIdentityProofSelect";
           DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
           db.AddInParameter(dbCommand, "@ID_IdentityProof", DbType.Int64, iIdentity.MasterId);
           db.AddInParameter(dbCommand, "@IdpName", DbType.String, iIdentity.Name);
           db.AddInParameter(dbCommand, "@FK_Client", DbType.Int64, iIdentity.FK_Client);
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






       public long Deleteidproof(IIdentityProof iIdentity)
       {

           Database db = DatabaseFactory.CreateDatabase("WebPharma");
           string sqlCommand = "proIdentityProofDelete";
           DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
           db.AddInParameter(dbCommand, "@ID_IdentityProof", DbType.Int64, iIdentity.MasterId);
           db.AddInParameter(dbCommand, "@FK_Client", DbType.Int64, iIdentity.FK_Client);
           db.AddInParameter(dbCommand, "@UserCode", DbType.String, iIdentity.UserCode);
           db.AddInParameter(dbCommand, "@FK_Reason", DbType.Int64, iIdentity.FK_Reason);
           try
           {
               return Convert.ToInt32(db.ExecuteScalar(dbCommand));
           }
           catch (SqlException e)
           {
               throw e;
           }
       }

      

       public IIdentityProof Selectidproof(IIdentityProof iIdentity)
       {
           DataTable dtbl = new DataTable();
           Database db = DatabaseFactory.CreateDatabase("WebPharma");
           string sqlCommand = "proIdentityProofSelect";
           DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
           db.AddInParameter(dbCommand, "@ID_IdentityProof", DbType.Int64, iIdentity.MasterId);
           db.AddInParameter(dbCommand, "@IdpName", DbType.String, iIdentity.Name);
           db.AddInParameter(dbCommand, "@FK_Client", DbType.Int64, iIdentity.FK_Client);
           try
           {
               using (IDataReader dataReader = db.ExecuteReader(dbCommand))
               {
                   while (dataReader.Read())
                   {

                       iIdentity.Name = dataReader["Name"].ToString();

                       iIdentity.Type = Int64.Parse(dataReader["IdpType"].ToString());
                       iIdentity.Descrption = dataReader["Description"].ToString();
                       iIdentity.SortOrder = Int16.Parse(dataReader["SortOrder"].ToString());
                       iIdentity.AdressProof = Convert.ToBoolean(dataReader["AddressProof"].ToString());
                       iIdentity.IdProof = Convert.ToBoolean(dataReader["IdProof"].ToString());
                   }
               }
               return iIdentity;
           }
           catch (SqlException e)
           {
               throw e;
           }
       }
    }
}
