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
    public interface IHolidaySettings : IGeneral
    {


       
        string Reason { get; set; }
        DateTime Date { get; set; }
        byte Type { get; set; }
        DateTime FromTime { get; set; }
        DateTime ToTime { get; set; }
        bool Cancelled { get; set; }
        string Cancelleduser { get; set; }
        DateTime CancelledOn { get; set; }
        DateTime FromDate { get; set; }
        DateTime ToDate { get; set; }




    }
}
