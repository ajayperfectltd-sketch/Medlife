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
    public interface  IItemConversion:IGeneral
    {
       
        long FK_StoreFrom { get; set; }
        long FK_StoreTo { get; set; }
        DateTime TransDate { get; set; }
        decimal FromAmount { get; set; }
        decimal ToAmount { get; set; }
        string Remarks{ get; set; }
        string UserCode{ get; set; }
        DateTime EnteredOn { get; set; }
        Boolean Cancelled { get; set; }
        string CancelledBy{ get; set; }
        DateTime CancelledOn{ get; set; }
        Boolean Approved{ get; set; }
        string ApprovedBy{ get; set; }
        DateTime ApprovedOn{ get; set; }
        string XmlIndendFrom { get; set; }
        string XmlIndendTo { get; set; }
        DateTime FromDate { get; set; }
        DateTime ToDate { get; set; }
        string IndentMode { get; set; }
        Boolean Production { get; set; }
        
    }
}
