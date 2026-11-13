using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebPharma.Interface.General;

namespace WebPharma.Interface.Masters
{
  public  interface IDoctor:IGeneral
    {
      string DoctorName { get; set; }
      string Qualification { get; set; }
      string ClinicName { get; set; }
      DateTime EnteredOn { get; set; }
      long FK_Client { get; set; }
      long Reason { get; set; }
    }
}
