using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebPharma.Business.Accounts;

public partial class Popup_CommonPopupTransferToAccount : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                txtAhstChequeDate.Attributes.Add("readonly", "readonly");

                hfClient.Value = Request.QueryString["clnt"];
                hfUser.Value = Request.QueryString["uid"];



            }


        }
        catch (Exception ex)
        {

        }


    }



    protected void btntrsfrSearch_Click(object sender, ImageClickEventArgs e)
    {



    }



    protected void btnApply_Click(object sender, EventArgs e)
    {
        BlAccountHeadSubTransactions BlAccountHeadSubTransaction = new BlAccountHeadSubTransactions();
        ArrayList list = new ArrayList();
        BlbankToAcc blbanktoaccounts = new BlbankToAcc();
        blbanktoaccounts.Bankno = Convert.ToInt64(ddlBank.SelectedValue);
        blbanktoaccounts.ChequeNumber = txtCheqNo.Text;
        blbanktoaccounts.ChequeDates = Convert.ToDateTime(txtAhstChequeDate.Text);
        list.Add(blbanktoaccounts);

        blbanktoaccounts.XmlBankToAcc = Common.WriteToXml(list);
        Session["SessionName"] = blbanktoaccounts.XmlBankToAcc;
        blbanktoaccounts = null;
        clearfields();
    }

    private void clearfields()
    {
        ddlBank.SelectedValue = "0";
        txtCheqNo.Text = string.Empty;

        txtpoptrsfrAccntDetails.Text = string.Empty;
        txtAhstChequeDate.Text = DateTime.Now.ToShortDateString();

    }


}