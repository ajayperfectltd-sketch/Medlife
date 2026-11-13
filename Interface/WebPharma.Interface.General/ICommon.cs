/*----------------------------------- Interface Layer Class -----------------------------------------------------------------------------------------------------------------------
Purpose    : 
Author     :bibin  tc
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

namespace WebPharma.Interface.General
{
    public interface IGeneral
    {
        byte UserAction { get; set; }
        string UserCode { get; set; }
        DateTime EnteredOn { get; set; }
        Int64 FK_Client { get; set; }
        Int64 MasterId { get; set; }
        Int16 PageIndex { get; set; }
        Int16 PageSize { get; set; }
      
        
    }
    public interface IAccounts : IGeneral
    {
        Boolean Active { get; set; }
        Int32 SortOrder { get; set; }
        string Description { get; set; }
    }
    public interface IReports
    {
        string ReportSchema { get; set; }
        Int16 ReportOption { get; set; }
        Int16 ReportIndex { get; set; }

        string SubModule { get; set; }
        DateTime FromDate { get; set; }
        DateTime ToDate { get; set; }
        Int64 FK_Client { get; set; }
        string UserCode { get; set; }
    }
}
