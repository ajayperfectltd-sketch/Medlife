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

    public interface IBillType : IGeneral
    {
        string TypeName { get; set; }
        Int32 BillType { get; set; }
        long FK_AccountHead { get; set; }
        long FK_AccountHeadSub { get; set; }
      
        DateTime EnteredOn { get; set; }
        long FK_Client { get; set; }
        Boolean GovtSuppliers { get; set; }
       

        long FK_AccountReturn { get; set; }
        long FK_AccountSubReturn { get; set; }
     

        long FK_AccountHeadRounding { get; set; }
        long FK_AccountHeadSubRounding { get; set; }

      
        long FK_AccountHeadDiscount { get; set; }
        long FK_AccountHeadSubDiscount { get; set; }

        long FK_AccountHeadCard { get; set; }
        long FK_AccountHeadSubCard { get; set; }
        long FK_AccountHeadCheque { get; set; }
        long FK_AccountHeadSubCheque { get; set; }

        long FK_AccountCredit { get; set; }
        
        Boolean Credit { get; set; }
        long FK_Reason { get; set; }
   
    }

}
