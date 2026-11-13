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
    public interface IIndentIssue:IGeneral
    {

        string PONo { get; set; }
        DateTime Date { get; set; }

        long PostingId { get; set; }
        long FK_IndentRequest { get; set; }
    
     
        string Remarks { get; set; }

        Int16 PriceTag { get; set; }
      
        bool Closed { get; set; }
       string  ClosedUser { get; set; }
       string ClosedRemarks { get; set; }
       bool Cancelled { get; set; }
       string CancelledUser { get; set; }
       DateTime CancelledOn { get; set; }
       long FK_FromStore { get; set; }
       long FK_ToStore { get; set; }
     
       string XmlPurchaseItems { get; set; }
       DateTime FromDate { get; set; }
       DateTime ToDate { get; set; }
       string isClosed { get; set; }
       Boolean Posted { get; set; }
       Boolean Approved { get; set; }
       string ApprovedUser { get; set; }
       DateTime ApprovedOn { get; set; }


    }
}
