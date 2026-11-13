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
    public interface IPromotionSetting:IGeneral
    {
       
        long FK_PromotionType	{get;set;}
        long FK_Items	{get;set;}
        DateTime EffectDate	{get;set;}
        decimal DiscountPercent	{get;set;}
         long  Buy	{get;set;}
        long Get{get;set;}	
        string UserCode	{get;set;}
        DateTime EnteredOn{get;set;}
        long FK_Client { get; set; }
        DateTime CloseDate { get; set; }
        bool Closed { get; set; }
        DateTime Date { get; set; }
        long FK_Brand { get; set; }
        long FK_CategoryFirst { get; set; }
        short Type { get; set; }
    }
}
