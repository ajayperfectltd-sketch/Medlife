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

    public interface ICategoryFirst : IGeneral
    {
        string Shortname { get; set; }
        string Name { get; set; }
        decimal Discount { get; set; }
        decimal Profit { get; set; }
        decimal Commission { get; set; }
        int ExpiryDays { get; set; }
        DateTime EnteredOn { get; set; }
        long FK_Client { get; set; }
        long FK_Reason { get; set; }
        Boolean NonMedicine { get; set; }
    }

}
