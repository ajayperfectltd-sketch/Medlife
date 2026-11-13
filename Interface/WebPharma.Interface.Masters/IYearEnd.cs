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
    public interface IYearEnd : IGeneral
    {


        long FK_Outlet { get; set; }
        DateTime FromDate { get; set; }
        DateTime ToDate { get; set; }
        Byte Type { get; set; }
        long FK_User { get; set; }
        Boolean Active { get; set; }
        //string UserCode { get; set; }
        string XmlUser { get; set; }
        long FK_UserGroup { get; set; }
        bool StockUpdateMode { get; set; }

    }
}
