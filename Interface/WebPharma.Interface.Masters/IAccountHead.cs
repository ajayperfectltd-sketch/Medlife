
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
    public interface IAccountHead:IGeneral
    {
        long FK_AccountGroup { get; set; }
        long FK_AccountSubGroup { get; set; }
        string AhCode { get; set; }
        string Name { get; set; }
        string ShortName { get; set; }
        byte AccountType { get; set; }
        int SortOrder { get; set; }
        string Description { get; set; }
        Boolean Active { get; set; }
        Int64 FK_Reason { get; set; }
        Boolean CashinHand { get; set; }

    }
}
