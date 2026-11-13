/*----------------------------------- Interface Class for Business Layer Class -----------------------------------------------------------------------------------------------------------------------
Purpose    : 
Author     : Aiswarya
Copyright  : Perfect Software Solutions (Clt) Pvt Ltd
Created on : 29/09/2018
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
    public interface IIssueOut:IGeneral
    {

            long  FK_Store{get; set;}
            long  FK_Category{get; set;}
            string IssueType { get; set; }
            DateTime TransDate{get; set;}
            decimal TotalAmt{get; set;}
            bool Cancelled{get; set;}
            string CancelledBy{get; set;}
            DateTime CancelledOn{get; set;}
            long FK_Client{get; set;}
            string XmlIssuedItems { get; set; }
            DateTime FromDate { get; set; }
            DateTime ToDate { get; set; }
            long Fk_Reasons { get; set; }
    }
}

