/*----------------------------------- Interface Class for Business Layer Class -----------------------------------------------------------------------------------------------------------------------
Purpose    : 
Author     : Bibin TC
Copyright  : Perfect Software Solutions (Clt) Pvt Ltd
Created on : 03/05/2018
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
using WebPharma.Interface.General;

namespace WebPharma.Interface.General
{

    public interface IUserGroup : IGeneral
    {
        string GroupName { get; set; }
        string Shortname { get; set; }
        Boolean Administrator { get; set; }
        Boolean UgAdd { get; set; }
        Boolean UgModify { get; set; }
        Boolean UgDelete { get; set; }
        Boolean UgView { get; set; }
        Boolean UgDiscount { get; set; }
        Boolean UgDayClose { get; set; }
        Boolean Active { get; set; }
        Boolean UgMultiStoreLogin { get; set; }
        Boolean UgPurchase { get; set; }
        Boolean UgSales { get; set; }
        Boolean UgPurchaseReturn { get; set; }
        Boolean UgSalesReturn { get; set; }
        Boolean UgApprove { get; set; }
        Boolean UgPriceCorrection { get; set; }
        long FK_Reason { get; set; }
        
    }

}
