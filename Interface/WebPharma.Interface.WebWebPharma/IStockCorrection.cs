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
  
    public interface IStockCorrection : IGeneral
    {
        long FK_Stock { get; set; }
        long FK_Store { get; set; }
        decimal ItmOldStock { get; set; }
        decimal ItmNewStock { get; set; }
        string xmlStockCorrectionItems { get; set; }
    }
}
