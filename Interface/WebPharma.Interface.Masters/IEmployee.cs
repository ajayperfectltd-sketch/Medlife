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

    public interface IEmployee : IGeneral
    {
        string EmployeeCode { get; set; }
        string Name { get; set; }
        string Address { get; set; }
        DateTime Dob { get; set; }
        string Phone { get; set; }
        Boolean Salesman { get; set; }
        string Email { get; set; }
        Boolean Active { get; set; }
        DateTime EnteredOn { get; set; }
        string XmlStoreAccessDetails { get; }
        long FK_Client { get; set; }

    }

}
