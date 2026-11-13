/*----------------------------------- Interface Class for Business Layer Class -----------------------------------------------------------------------------------------------------------------------
Purpose    : ExpenseType Interface Class
Author     : Praveen
Copyright  : Perfect Software Solutions (Clt) Pvt Ltd
Created on : 19/05/2018
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

    public interface IExpenseType : IGeneral
    {
        string ExpenseType { get; set; }
        long FK_AccountHead { get; set; }
        long FK_AccountHeadSub { get; set; }
        long FK_Reason { get; set; }
        Boolean EtPurchase { get; set; }
        Boolean EtSales { get; set; }
        Boolean EtPurchaseReturn { get; set; }
        Boolean EtSalesReturn { get; set; }
        Boolean EtAddorless { get; set; }
  
    }

}
