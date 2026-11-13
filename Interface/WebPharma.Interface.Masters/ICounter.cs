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

    public interface ICounter : IGeneral
    {
        string Name { get; set; }
        string ShortName { get; set; }
        long FK_Store { get; set; }
        long FK_Reason { get; set; }
        DateTime EnteredOn { get; set; }
        long FK_Client { get; set; }
        long FK_AccountHead { get; set; }
        long FK_AccountHeadSub { get; set; }
        string XmlShiftDetails { get; set; }
        bool Active { get; set; }
    }

}
