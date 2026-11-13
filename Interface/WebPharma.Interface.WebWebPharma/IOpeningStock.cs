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
    public interface IOpeningStock:IGeneral
    {
        Int16  PageSize { get; set; }
        Int16 PageIndex { get; set; }
        long FK_Store { get; set; }
        string StoreName { get; set; }
        string xmlOpening { get; set; }
        long GroupID { get; set; }
        string BatchNo { get; set; }
        string ItemName { get; set; }
        long FK_Category { get; set; }
        long FK_SubCategory { get; set; }
        long FK_Rack { get; set; }
        long FK_Bin { get; set; }
        long FK_Manufacturer { get; set; }
        long FK_Generic { get; set; }

    }
}
