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

namespace WebPharma.Interface.WebPharma
{
    public interface ISchedule : IGeneral
    {
        Int16 ScheduleMode { get; set; }
        DateTime ScheduleFrom { get; set; }
        DateTime ScheduleUpto { get; set; }

        decimal ScAmount { get; set; }

        DateTime  SearchFromScheduleUpto { get; set; }
        DateTime  SearchToScheduleUpto  { get; set; }

        string XmlSchedulePosting { get; set; }

        string XmlScheduleProcessing { get; set; }
    }
}