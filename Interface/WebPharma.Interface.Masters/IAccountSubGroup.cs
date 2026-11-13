


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
   public interface IAccountSubGroup:IGeneral
    {
       long FK_Accountgroup { get; set; }
       string Name { get; set; }
       string ShortName { get; set; }
       int SortOrder { get; set; }
       string Description { get; set; }
       Boolean Active { get; set; }
       Int64 FK_Reason { get; set; }
    }
}
