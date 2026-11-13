using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebPharma.Interface.General;

namespace WebPharma.Interface.WebPharma
{
   public interface ICloudLink : IGeneral
    {
        long FK_Client { get; set; }
        long FK_Store { get; set; }
        Int16 Mode { get; set; }
        string UserCode { get; set; }
        string XmlExportCloudDetails { get; set; }
        string XmlMasterCount { get; set; }
        DataSet dsScoreData { get; set; }
        Int64 ReturnId { get; set; }
        string XmlmasterData { get; set; }
        string XmldtAccountGroup { get; set; }
        string XmldtAccountsubGroup { get; set; }
        string XmldtAccountHead { get; set; }
        string XmldtAccountHeadSub { get; set; }
        string XmldtState { get; set; }
        string XmldtDistrict { get; set; }
        string XmldtPlace { get; set; }
        string XmldtPost { get; set; }
        string XmldtIdentityProof { get; set; }
        string XmldtCustomerType { get; set; }
        string XmldtCustomer { get; set; }
        string XmldtBehaviour { get; set; }
        string ReturnMessage { get; set; }
        Int64 IDProcessExport { get; set; }
        DateTime Tdate { get; set; }
        Boolean SupplierWise { get; set; }
        Boolean SalesMan { get; set; }
        string XmldtAgent { get; set; }
        string XmldtSupplier { get; set; }
        string XmldtRecovery { get; set; }
        Boolean Recovery { get; set; }
        string XmlErrorCode { get; set; }
    } 
}
