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

namespace WebPharma.Interface.General
{

    public interface IDiscountSettings : IGeneral
    {
        long FK_Discount { get; set; }
        byte DiscountType { get; set; }
        long FK_Employee { get; set; }
        long FK_Customer { get; set; }
        long FK_Items { get; set; }
        long FK_CustomerGroup { get; set; }
        long StockCode { get; set; }
        DateTime FromDate { get; set; }
        DateTime ToDate { get; set; }
        DateTime Date { get; set; }
        Boolean Closed { get; set; }
        decimal Percentage { get; set; }
        decimal Amount { get; set; }
        //DateTime EnteredOn { get; set; }
        //long FK_Client { get; set; }
        long FK_SalesMan { get; set; }
        string VoucherNo { get; set; }
        string DisName { get; set; }
        long FK_Brand { get; set; }
        long FK_CategoryFirst { get; set; }
    }

}
