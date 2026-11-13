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
    public interface IRackAllotment : IGeneral
    {
        long FK_Rack { get; set; }
        DateTime AllotmentDate { get; set; }
        long FK_Supplier { get; set; }
        Int32 PeriodType { get; set; }
        Int32 Period { get; set; }
        DateTime DueDate { get; set; }
        decimal Rent { get; set; }
        string Remarks { get; set; }
        Boolean Vacate { get; set; }



    }
}
