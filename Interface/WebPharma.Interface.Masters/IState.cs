/*----------------------------------- Interface Class for Business Layer Class -----------------------------------------------------------------------------------------------------------------------
Purpose    : 
Author     : Praveen MP
Copyright  : Perfect Software Solutions (Clt) Pvt Ltd
Created on : 21/05/2018
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
    public interface IState : IGeneral
    {
        string StateName { get; set; }
        string StateCode { get; set; }
        Int64 FK_Country { get; set; }
        long FK_Reason { get; set; }
    }
}
