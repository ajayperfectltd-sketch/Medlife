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

namespace WebPharma.Interface.Accounts
{
   public interface IWebPharmaTransaction
    {
       DateTime TransDate { get; set; }
       string TransType { get; set; }
     string TransferTo { get; set; }

     byte Type { get; set; }
     decimal Amount { get; set; }

     long FK_Store { get; set; }
     byte UserAction { get; set; }
     Int16 PageIndex { get; set; }
     Int16 PageSize { get; set; }
     long MasterId { get; set; }
     string Remarks { get; set; }
     string UserCode { get; set; }
     DateTime EnteredOn { get; set; }
     long FK_Client { get; set; }
     Int64 FK_Name { get; set; }
     DateTime FromDate { get; set; }
     DateTime ToDate { get; set; }
     string Name { get; set; }
     string XmlTransfertToAcc { get; set; }
    }
}
