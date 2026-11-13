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
using   WebPharma.Interface.General;

namespace WebPharma.Interface.WebPharma
{
    public interface IIndentRequest:IGeneral
    {

       
       
        decimal totalAmount { get; set; }
        decimal RoundAmount { get; set; }
        decimal NetAmount { get; set; }
        string Remarks { get; set; }
        bool Cancelled { get; set; }
        string CancelledUser { get; set; }
        DateTime CancelledOn { get; set; }
        long FK_Store { get; set; }
        DateTime FromDate { get; set; }
        DateTime ToDate { get; set; }
        long ToStore { get; set; }
        string XmlIndentRequestItems { get; set; }
        DateTime InReqDate { get; set; }
        long FK_Reason { get; set; }


    }
}
