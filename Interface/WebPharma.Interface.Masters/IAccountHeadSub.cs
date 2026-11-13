
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
namespace WebPharma.Interface.Masters
{
    public interface IAccountHeadSub:IGeneral
    {
        long Fk_AccountHead { get; set; }
        string Ahcode { get; set; }
        string AhsCode { get; set; }
        string Name { get; set; }
        string ShortName { get; set; }
        int SortOrder { get; set; }
        string Description { get; set; }
        Boolean Active { get; set; }
        long Fk_Store { get; set; }
        Boolean PettyCash { get; set; }
        DateTime OpeningDate { get; set; }
        Decimal OpeningBalance { get; set; }
        int  BalanceType { get; set; }
        Int64 Fk_Cuscode { get; set; }
        Int64 FK_Reason { get; set; }

    }
}
