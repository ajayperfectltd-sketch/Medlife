using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebPharma.Interface
{
    public interface IPopulate
    {
        string TableName { get; set; }
        string ListFields { get; set; }
        string ValueFields { get; set; }
        string SortFields { get; set; }
        string Criteria { get; set; }
        string UpdateField { get; set; }
        string UpdateValue { get; set; }
        string GroupBy { get; set; }
        int PageIndex { get; set; }
        int PageSize { get; set; }
        string Criteria2 { get; set; }

        string DbName { get; set; }
        string SearchCriteria { get; set; }
        int SortOrder { get; set; }
        string Module { get; set; }
        string Usercode { get; set; }
        


    }
}
