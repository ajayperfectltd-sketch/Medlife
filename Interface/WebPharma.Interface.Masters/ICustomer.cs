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

    public interface ICustomer : IGeneral
    {
        string Name { get; set; }
        string ShortName { get; set; }
        string Area { get; set; }
        string Place { get; set; }
        string Post { get; set; }
        string Pin { get; set; }
        string LandMark { get; set; }
        string Phone { get; set; }
        string Mobile { get; set; }
        string Email { get; set; }
        DateTime DateOfBirth { get; set; }
        long FK_Country { get; set; }
        long FK_State { get; set; }
        long FK_District { get; set; }
        Boolean Active { get; set; }
        Boolean IsEmployee { get; set; }
        string InactiveRemarks { get; set; }
        long FK_CustomerGroup { get; set; }
        long FK_AccountHeadSub { get; set; }
        long CreditDays { get; set; }
        long FK_AccountHead { get; set; }
        decimal CreditLimit { get; set; }
        string AccountHead { get; set; }
        string AccountSubHead { get; set; }
        Boolean CreditSale { get; set; }
        Boolean CardPrice { get; set; }
        string Attachment { get; set; }
        byte[] AttachmentData { get; set; }
        decimal DiscountPercentage { get; set; }
        string CustomerCode { get; set; }
        string GSTTIN { get; set; }
         string CardNumber { get; set; }
         Int64 Idtype { get; set; }
         long FK_Store { get; set; }
           Int16 Status { get; set; }
          
           long FK_Reason { get; set; }
     
    }

}
