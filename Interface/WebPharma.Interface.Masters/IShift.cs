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

    public interface IShift : IGeneral
    {
        string Name { get; set; }
        string ShortName { get; set; }
        DateTime ShiftFrom { get; set; }
        DateTime ShiftTo { get; set; }
        DateTime EnteredOn { get; set; }
        long FK_Client { get; set; }
        DateTime Date { get; set; }
        Int16 Type { get; set; }
        bool Active { get; set; }
        long FK_Reason { get; set; }
    }

}
