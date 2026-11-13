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

    public interface IPriceConversion : IGeneral
    {
       
        DateTime TransDate { get; set; }
        long FK_Store { get; set; }
        long FK_Category { get; set; }
        Decimal MinimumQty { get; set; }
        long PriceTag { get; set; }
        string XmlItems { get; set; }
        long FK_Items { get; set; }

    }

}
