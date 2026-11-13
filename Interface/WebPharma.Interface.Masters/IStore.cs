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

    public interface IStore : IGeneral
    {
        string Name { get; set; }
        string Shortname { get; set; }
        string Description { get; set; }
        DateTime EnteredOn { get; set; }
        long FK_Client { get; set; }
        Int16 Type { get; set; }
        Int16 BranchCode { get; set; }
        decimal OpeningBalance { get; set; }
        decimal OpeningLiability { get; set; }
        string Address1 { get; set; }
        string Address2 { get; set; }
        string Address3 { get; set; }
        string Email { get; set; }
        string Phone { get; set; }
        string Website { get; set; }
        DateTime StartDate { get; set; }
        Int16 BranchType { get; set; }
        byte Cancelled { get; set; }
        string  CancelledBy { get; set; }
        DateTime CancelledOn { get; set; }
        Boolean Active { get; set; }
        long FK_Reason { get; set; }
        long FK_Agent { get; set; }

    }

}
