using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventory.Interface.Accounts
{

    public interface IFinalAccounts
    {
        DateTime TransDate { get; set; }
        string Mode { get; set; }

        long FK_Store { get; set; }
        string UserCode { get; set; }
        DataTable dtFinalAccount { get; set; }
        long ID_FinalAccount { get; set; }
        long Criteria { get; set; }
    }
}
