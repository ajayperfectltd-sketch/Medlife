<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MonthlyProcess.aspx.cs" Inherits="MonthlyProcess"  ResponseEncoding="utf-8" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc" %>

<head id="Head1" runat="server">
    <link href="./css/main.css"         rel="stylesheet" />
    <link href="./css/bootstrap.min.css" rel="stylesheet" />
    <link href="./css/general.css"      rel="stylesheet" />
    <link href="./css/toastr.css"       rel="stylesheet" />
    <script src="js/jquery-1.10.2.js"></script>
    <script src="js/Custom.js"></script>
    <script src="js/toastr.js"></script>

    <style>
     #calProcessDate_container {
    top: 390px !important;
}

    #toast-container {
        position: fixed !important;
        top: 20px !important;
        right: 20px !important;
        bottom: auto !important;
        left: auto !important;
        z-index: 999999 !important;
    }

    #toast-container > div {
        margin-top: 0 !important;
    }
         

        .mp-container {
            max-width: 960px;
            margin: 30px auto;
            border: 1px solid #ccc;
            padding: 25px;
            border-radius: 8px;
            background: #fff;
        }
        /* ── Progress bar ─────────────────────────────────── */
        .mp-progress {
            display: flex;
            justify-content: space-between;
            margin-bottom: 28px;
            position: relative;
        }
        .mp-progress::before {
            content: '';
            position: absolute;
            top: 18px;
            left: 10%;
            right: 10%;
            height: 2px;
            background: #dee2e6;
            z-index: 0;
        }
        .mp-step {
            flex: 1;
            text-align: center;
            position: relative;
            z-index: 1;
        }
        .mp-step-circle {
            width: 36px;
            height: 36px;
            border-radius: 50%;
            background: #dee2e6;
            color: #6c757d;
            font-weight: bold;
            font-size: 15px;
            line-height: 36px;
            margin: 0 auto 6px;
            border: 2px solid #dee2e6;
        }
        .mp-step.active   .mp-step-circle { background:#17a2b8; color:#fff; border-color:#17a2b8; }
        .mp-step.done     .mp-step-circle { background:#28a745; color:#fff; border-color:#28a745; }
        .mp-step-label { font-size: 11px; color: #6c757d; }
        .mp-step.active .mp-step-label { color: #17a2b8; font-weight:600; }
        .mp-step.done   .mp-step-label { color: #28a745; }

        /* ── Step panels ──────────────────────────────────── */
        .mp-panel { display:none; }
        .mp-panel.active { display:block; }
        .mp-panel-title {
            font-size: 16px;
            font-weight: 600;
            color: #343a40;
            border-bottom: 2px solid #17a2b8;
            padding-bottom: 8px;
            margin-bottom: 18px;
        }

        /* ── Summary table ────────────────────────────────── */
        .mp-table { width:100%; border-collapse:collapse; font-size:13px; margin-bottom:16px; }
        .mp-table th { background:#f8f9fa; padding:8px 10px; border:1px solid #dee2e6;
                        text-align:left; font-weight:600; color:#495057; }
        .mp-table td { padding:8px 10px; border:1px solid #dee2e6; }
        .mp-table tfoot td { font-weight:600; background:#e9ecef; }

        /* ── Cash detail rows ─────────────────────────────── */
        .cash-row { display:flex; align-items:center; margin-bottom:10px; }
        .cash-lbl { width:220px; font-weight:500; color:#495057; font-size:13px; }
        .cash-val { font-size:14px; font-weight:600; color:#212529; }

        /* ── Check grid colours ───────────────────────────── */
       .check-ok     { color:#155724; background:#d4edda; padding:2px 8px;
                border-radius:4px; font-size:12px; }
        .check-warn   { color:#856404; background:#fff3cd; padding:2px 8px;
                        border-radius:4px; font-size:12px; }
        .check-error  { color:#721c24; background:#f8d7da; padding:2px 8px;
                        border-radius:4px; font-size:12px; }

        /* Full-row status colouring for Step 4 grid */
        .check-row-error td { background:#f8d7da !important; }
        .check-row-warn  td { background:#fff3cd !important; }
        .check-row-ok    td { background:#d4edda !important; }

        /* ── Recap panel (Step 5) ─────────────────────────── */
        .recap-row { display:flex; padding:6px 0; border-bottom:1px solid #f1f1f1; font-size:13px; }
        .recap-lbl { width:200px; flex-shrink:0; color:#6c757d; }
       /*.recap-val { flex:1; min-width:0; font-weight:600; color:#212529; }*/
        .recap-val { flex:1; min-width:0; font-weight:600; color:#212529;                 
             overflow-wrap:break-word;}
        .mp-warning-box {
            background:#fff3cd; border:1px solid #ffc107; border-radius:5px;
            padding:12px 16px; margin:16px 0; font-size:13px; color:#856404;
        }

        /* ── Result panels ────────────────────────────────── */
        .panelSuccess { margin-top:16px; padding:12px; background:#d4edda;
                        color:#155724; border-radius:5px; font-weight:600; display:none; }
        .panelFailed  { margin-top:16px; padding:12px; background:#f8d7da;
                        color:#721c24; border-radius:5px; font-weight:600; display:none; }

        /* ── Button bar ───────────────────────────────────── */
        .mp-btn-bar { margin-top:24px; padding-top:16px; border-top:1px solid #dee2e6; }
        .mp-btn-bar .btn { margin-right:6px; }

        /* ── Form rows ────────────────────────────────────── */
        .mp-form-row { display:flex; align-items:center; margin-bottom:12px; }
        .mp-form-lbl { width:160px; font-size:13px; color:#495057; font-weight:500; }
        .mp-form-val { flex:1; }

        
    </style>

</head>

<body onkeydown="javascript:EnterKeyFilter();">
<form id="form1" runat="server">

    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
    <ContentTemplate>

        <div class="container-fluid">
        <div class="mp-container">

            <!-- ═══ Page heading ═══════════════════════════════════════ -->
            <div class="col-lg-12 top-header" style="margin-bottom:10px;">
                <div class="heading">
                    <asp:Label ID="lblPageName" runat="server"
                               Text="Period Close" Style="font-size:18px;"></asp:Label>
                </div>
            </div>
            <div class="clear"></div>

            <!-- ═══ Progress bar ════════════════════════════════════════ -->
            <div class="mp-progress">
                <div class="mp-step" id="prgStep1" runat="server">
                    <div class="mp-step-circle">1</div>
                    <div class="mp-step-label">Date &amp; Store</div>
                </div>
                <div class="mp-step" id="prgStep2" runat="server">
                    <div class="mp-step-circle">2</div>
                    <div class="mp-step-label">Transactions</div>
                </div>
                <div class="mp-step" id="prgStep3" runat="server">
                    <div class="mp-step-circle">3</div>
                    <div class="mp-step-label">Cash Details</div>
                </div>
                <div class="mp-step" id="prgStep4" runat="server">
                    <div class="mp-step-circle">4</div>
                    <div class="mp-step-label">Pending Items</div>
                </div>
                <div class="mp-step" id="prgStep5" runat="server">
                    <div class="mp-step-circle">5</div>
                    <div class="mp-step-label">Confirm</div>
                </div>
            </div>

            <!-- ═══════════════════════════════════════════════════════════
                 STEP 1 – Date & Store Selection
            ═══════════════════════════════════════════════════════════════ -->
            <div id="divStep1" runat="server" class="mp-panel">
                <div class="mp-panel-title">Step 1 - Date &amp; Store Selection</div>

                <div class="mp-form-row">
                    <label class="mp-form-lbl">Store</label>
                    <div class="mp-form-val">
                        <asp:DropDownList ID="ddlStore" runat="server"
                            CssClass="form-control" Style="max-width:300px;"
                            AutoPostBack="true" OnSelectedIndexChanged="ddlStore_SelectedIndexChanged"
                            ToolTip="Select the store to close the period for">
                        </asp:DropDownList>
                        <asp:RequiredFieldValidator ID="rfvStore" runat="server"
                            ControlToValidate="ddlStore" InitialValue="0"
                            ErrorMessage="Please select a store."
                            Display="None" ValidationGroup="vgStep1" SetFocusOnError="true">
                        </asp:RequiredFieldValidator>
                        <cc:ValidatorCalloutExtender ID="vcStore" runat="server"
                            TargetControlID="rfvStore" Enabled="true">
                        </cc:ValidatorCalloutExtender>
                    </div>
                </div>

                <div class="mp-form-row">
                    <label class="mp-form-lbl">From Date</label>
                    <div class="mp-form-val">
                    <asp:Label ID="lblFromDate" runat="server" Text="-"
                        CssClass="form-control" Style="background:#f8f9fa;max-width:180px;display:inline-block;">
                    </asp:Label>
                    </div>
                </div>

                <div class="mp-form-row">
                    <label class="mp-form-lbl">To Date</label>
                    <div class="mp-form-val">
                       <asp:Label ID="lblToDate" runat="server" Text="-"
                            CssClass="form-control" Style="background:#f8f9fa;max-width:180px;display:inline-block;">
                        </asp:Label>
                    </div>
                </div>

                <div class="mp-form-row">
                    <label class="mp-form-lbl">Process Date</label>
                    <div class="mp-form-val">
                        <asp:TextBox ID="txtProcessDate" runat="server"
                            CssClass="form-control" Style="max-width:180px;" TabIndex="2" ReadOnly="true"
                            ToolTip="Date on which this period-close is being processed. Cannot be a future date or earlier than the period start date.">
                        </asp:TextBox>
                        <%--<cc:CalendarExtender ID="calProcessDate" runat="server"
                            TargetControlID="txtProcessDate" Format="dd/MM/yyyy" Enabled="true" >
                        </cc:CalendarExtender>--%>
                        <asp:RequiredFieldValidator ID="rfvProcessDate" runat="server"
                            ControlToValidate="txtProcessDate"
                            ErrorMessage="Please enter the process date."
                            Display="None" ValidationGroup="vgStep1" SetFocusOnError="true">
                        </asp:RequiredFieldValidator>
                        <cc:ValidatorCalloutExtender ID="vcProcessDate" runat="server"
                            TargetControlID="rfvProcessDate" Enabled="true">
                        </cc:ValidatorCalloutExtender>
                    </div>
                </div>

                <div class="mp-form-row">
                    <label class="mp-form-lbl">Financial Year</label>
                    <div class="mp-form-val">
                        <asp:Label ID="lblFinYear" runat="server" Style="font-weight:600;"></asp:Label>
                    </div>
                </div>

                <div class="mp-form-row">
                    <label class="mp-form-lbl">Period Status</label>
                    <div class="mp-form-val">
                        <asp:Label ID="lblMonthStatus" runat="server" ToolTip="Current status of the selected period"></asp:Label>
                    </div>
                </div>

                <div class="mp-form-row">
                    <label class="mp-form-lbl">Remarks</label>
                    <div class="mp-form-val">
                        <asp:TextBox ID="txtRemarks" runat="server"
                            TextMode="MultiLine" Rows="3"
                            MaxLength="500" CssClass="form-control"
                            Style="max-width:400px;" TabIndex="3"
                              ToolTip="Optional notes for this period close (max 500 characters)">
                        </asp:TextBox>
                      <asp:CustomValidator
                            ID="cvRemarks"
                            runat="server"
                            ControlToValidate="txtRemarks"
                            ValidateEmptyText="false"
                            ClientValidationFunction="validateRemarksLength"
                            OnServerValidate="cvRemarks_ServerValidate"
                            ErrorMessage="Remarks cannot exceed 500 characters."
                            Display="Dynamic"
                            CssClass="text-danger"
                            ValidationGroup="vgStep1">
                        </asp:CustomValidator>
                    </div>
                </div>
            </div>

            <!-- ═══════════════════════════════════════════════════════════
                 STEP 2 – Transaction Summary
            ═══════════════════════════════════════════════════════════════ -->
            <div id="divStep2" runat="server" class="mp-panel">
                <div class="mp-panel-title">Step 2 - Transaction Summary</div>

                <p style="font-size:12px;color:#6c757d;margin-bottom:14px;">
                    Period:
                    <asp:Label ID="lblStep2Period" runat="server" Style="font-weight:600;"></asp:Label>
                </p>

                <!-- Sub-section A: Transaction Amounts -->
                <div style="font-weight:600;margin-bottom:8px;font-size:13px;">Transaction Amounts</div>
                <asp:GridView ID="gvTransactions" runat="server"
                    AutoGenerateColumns="false"
                    CssClass="mp-table" GridLines="None"
                    ShowFooter="true">
                    <Columns>
                        <asp:BoundField  DataField="Name"      HeaderText="Transaction"   />
                        <asp:BoundField  DataField="NetAmount" HeaderText="Net Amount"
                            DataFormatString="{0:N2}" HtmlEncode="false" ItemStyle-HorizontalAlign="Right" />
                        <asp:BoundField  DataField="Liability" HeaderText="Liability"
                            DataFormatString="{0:N2}" HtmlEncode="false" ItemStyle-HorizontalAlign="Right" />
                    </Columns>
                    <FooterStyle CssClass="mp-table tfoot" />
                    <AlternatingRowStyle BackColor="#f8f9fa" />
                </asp:GridView>

                <!-- Sub-section B: Indent / Transfer -->
                <div style="font-weight:600;margin:16px 0 8px;font-size:13px;">Indent / Transfer Details</div>
                <table class="mp-table">
                    <thead>
                        <tr>
                            <th>Description</th>
                            <th style="text-align:right;">Goods Received</th>
                            <th style="text-align:right;">Goods Issued</th>
                        </tr>
                    </thead>
                    <tbody>
                        <tr>
                            <td>Inter-Store Transfer (Indent)</td>
                            <td style="text-align:right;">
                                <asp:Label ID="lblIndentTo" runat="server" Text="0.00"></asp:Label>
                            </td>
                            <td style="text-align:right;">
                                <asp:Label ID="lblIndentFrom" runat="server" Text="0.00"></asp:Label>
                            </td>
                        </tr>
                    </tbody>
                </table>

                <!-- Sub-section C: Damage Stock -->
                <div style="font-weight:600;margin:16px 0 8px;font-size:13px;">Damage / Issue Out</div>
                <table class="mp-table">
                    <thead>
                        <tr><th>Description</th><th style="text-align:right;">Value</th></tr>
                    </thead>
                    <tbody>
                        <tr>
                            <td>Damage Stock</td>
                            <td style="text-align:right;">
                                <asp:Label ID="lblDamageStock" runat="server" Text="0.00"></asp:Label>
                            </td>
                        </tr>
                    </tbody>
                </table>
            </div>

            <!-- ═══════════════════════════════════════════════════════════
                 STEP 3 – Cash Details
            ═══════════════════════════════════════════════════════════════ -->
            <div id="divStep3" runat="server" class="mp-panel">
                <div class="mp-panel-title">Step 3 - Liability &amp; Payment Summary</div>

                <!-- Liability Movement -->
                <div style="font-weight:600;margin-bottom:8px;font-size:13px;">Liability Movement</div>
                <div class="cash-row">
                    <span class="cash-lbl">Opening Liability (Last Period)</span>
                    <span class="cash-val">&nbsp;
                        <asp:Label ID="lblPrevLiability" runat="server" Text="0.00"></asp:Label>
                    </span>
                </div>
                <div class="cash-row">
                    <span class="cash-lbl">Closing Liability (This Period)</span>
                    <span class="cash-val">&nbsp;
                        <asp:Label ID="lblCurrLiability" runat="server" Text="0.00"></asp:Label>
                    </span>
                </div>
                <hr style="border-top:2px solid #dee2e6;margin:12px 0;" />
                <div class="cash-row">
                    <span class="cash-lbl" style="font-size:14px;">Net Change</span>
                    <span class="cash-val" style="font-size:16px;">&nbsp;
                        <asp:Label ID="lblLiabilityChange" runat="server" Text="0.00"></asp:Label>
                        (<asp:Label ID="lblLiabilityChangePct" runat="server" Text="0.00%"></asp:Label>)
                    </span>
                </div>

                <!-- Payment Mode Breakdown -->
                <div style="font-weight:600;margin:20px 0 8px;font-size:13px;">Payment Mode Breakdown</div>
                <asp:GridView ID="gvPaymentModes" runat="server"
                    AutoGenerateColumns="false"
                    CssClass="mp-table" GridLines="None"
                    EmptyDataText="No payments recorded for this period.">
                    <Columns>
                        <asp:BoundField DataField="ModeName" HeaderText="Payment Mode" />
                        <asp:BoundField DataField="TotalAmount" HeaderText="Total Amount"
                            DataFormatString="{0:N2}" HtmlEncode="false" ItemStyle-HorizontalAlign="Right" />
                        <asp:BoundField DataField="TransactionCount" HeaderText="Transactions"
                            ItemStyle-HorizontalAlign="Right" ItemStyle-Width="100px" />
                    </Columns>
                    <AlternatingRowStyle BackColor="#f8f9fa" />
                </asp:GridView>
            </div>

            <!-- ═══════════════════════════════════════════════════════════
                 STEP 4 – Pending Items Check
            ═══════════════════════════════════════════════════════════════ -->
         <div id="divStep4" runat="server" class="mp-panel">
            <div class="mp-panel-title">Step 4 - Pending Items Check</div>

            <asp:Label ID="lblStep4Info" runat="server"
                Style="font-size:13px;color:#6c757d;display:block;margin-bottom:14px;">
            </asp:Label>

            <asp:Label ID="lblRectifyMsg" runat="server"
                Style="font-size:13px;display:block;margin-bottom:14px;font-weight:600;">
            </asp:Label>

                <!-- All-clear message when no issues -->
                <div id="divStep4Clear" runat="server" style="display:none;">
                    <div class="panelSuccess" style="display:block;">
                        All checks passed - no pending items found. You may proceed.
                    </div>
                </div>

                <!-- Issue grid (only failed rows shown) -->
            <asp:GridView ID="gvChecks" runat="server"
                AutoGenerateColumns="false"
                CssClass="mp-table" GridLines="None"
                EmptyDataText="No pending issues found."
                Visible="false"
                ToolTip="Issues that must be resolved before the period can be closed"
                OnRowDataBound="gvChecks_RowDataBound">
                <Columns>
                    <asp:BoundField  DataField="Module"  HeaderText="#" ItemStyle-Width="40px" />
                    <asp:BoundField  DataField="Caption" HeaderText="Check Description" />
                    <asp:TemplateField HeaderText="Status" ItemStyle-Width="100px">
                        <ItemTemplate>
                            <asp:Label ID="lblStatus" runat="server"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                  
                </Columns>
                
            </asp:GridView>
            </div>

            <!-- ═══════════════════════════════════════════════════════════
                 STEP 5 – Confirm & Execute
            ═══════════════════════════════════════════════════════════════ -->
            <div id="divStep5" runat="server" class="mp-panel">
                <div class="mp-panel-title">Step 5 - Confirm &amp; Execute</div>

                <div style="max-width:560px;">
                    <div class="recap-row">
                        <span class="recap-lbl">Store</span>
                        <span class="recap-val"><asp:Label ID="lblRecapStore"    runat="server"></asp:Label></span>
                    </div>
                    <div class="recap-row">
                        <span class="recap-lbl">Financial Year</span>
                        <span class="recap-val"><asp:Label ID="lblRecapFinYear"  runat="server"></asp:Label></span>
                    </div>
                    <div class="recap-row">
                        <span class="recap-lbl">Period From</span>
                        <span class="recap-val"><asp:Label ID="lblRecapFrom"     runat="server"></asp:Label></span>
                    </div>
                    <div class="recap-row">
                        <span class="recap-lbl">Period To</span>
                        <span class="recap-val"><asp:Label ID="lblRecapTo"       runat="server"></asp:Label></span>
                    </div>
                    <div class="recap-row">
                        <span class="recap-lbl">Process Date</span>
                        <span class="recap-val"><asp:Label ID="lblRecapProcess"  runat="server"></asp:Label></span>
                    </div>
                    <div class="recap-row">
                        <span class="recap-lbl">Closing Liability</span>
                        <span class="recap-val"><asp:Label ID="lblRecapLiability" runat="server"></asp:Label></span>
                    </div>
                    <div class="recap-row">
                        <span class="recap-lbl">Stock Value</span>
                        <span class="recap-val"><asp:Label ID="lblRecapStock"  runat="server"></asp:Label></span>
                    </div>
                    <div class="recap-row">
                        <span class="recap-lbl">Opening Cash</span>
                        <span class="recap-val"><asp:Label ID="lblRecapOpening" runat="server"></asp:Label></span>
                    </div>
                    <div class="recap-row">
                        <span class="recap-lbl">Closing Cash</span>
                        <span class="recap-val"><asp:Label ID="lblRecapClosing" runat="server"></asp:Label></span>
                    </div>
                    <div class="recap-row">
                        <span class="recap-lbl">Remarks</span>
                        <span class="recap-val"><asp:Label ID="lblRecapRemarks"  runat="server"></asp:Label></span>
                    </div>
                </div>

                <div class="mp-warning-box">
                    Once processed, this period will be <strong>permanently closed</strong>.
                    Transactions cannot be backdated into a closed period.
                    Please confirm all figures above are correct before clicking <strong>Process</strong>.
                </div>

                <!-- Result panels shown after Process click -->
                <div id="panSuccess" runat="server" class="panelSuccess">
                PERIOD CLOSE COMPLETED SUCCESSFULLY
                </div>
                <div id="panFailed" runat="server" class="panelFailed">
                    PERIOD CLOSE FAILED - Please contact support.
                </div>
                <asp:BulletedList ID="blErrors" runat="server"
                    CssClass="panelFailed" Style="display:none;">
                </asp:BulletedList>
            </div>

            <!-- ═══ Hidden fields ══════════════════════════════════════════ -->
            <asp:HiddenField ID="hfCurrentStep"      runat="server" Value="1" />
            <asp:HiddenField ID="hfClient"            runat="server" />
            <asp:HiddenField ID="hfUser"              runat="server" />
            <asp:HiddenField ID="hfFromDate"          runat="server" />
            <asp:HiddenField ID="hfToDate"            runat="server" />
            <asp:HiddenField ID="hfProcessDate"       runat="server" />
            <asp:HiddenField ID="hfFinYear"           runat="server" />
            <asp:HiddenField ID="hfIsFuturePeriod"    runat="server" Value="0" />
            <asp:HiddenField ID="hfClosingLiability"  runat="server" />
            <asp:HiddenField ID="hfStockValue"        runat="server" />
            <asp:HiddenField ID="hfOpening"           runat="server" />
            <asp:HiddenField ID="hfReceipt"           runat="server" />
            <asp:HiddenField ID="hfPayment"           runat="server" />
            <asp:HiddenField ID="hfClosing"           runat="server" />
            <asp:HiddenField ID="hfPurchaseAmount"    runat="server" />
            <asp:HiddenField ID="hfPurchaseLiability" runat="server" />
            <asp:HiddenField ID="hfPReturnAmount"     runat="server" />
            <asp:HiddenField ID="hfPReturnLiability"  runat="server" />
            <asp:HiddenField ID="hfSalesAmount"       runat="server" />
            <asp:HiddenField ID="hfSalesLiability"    runat="server" />
            <asp:HiddenField ID="hfSReturnAmount"     runat="server" />
            <asp:HiddenField ID="hfSReturnLiability"  runat="server" />
            <asp:HiddenField ID="hfDamageStock"       runat="server" />
            <asp:HiddenField ID="hfIndentFrom"        runat="server" />
            <asp:HiddenField ID="hfIndentTo"          runat="server" />
            <asp:HiddenField ID="hfMonthlyProcessID"  runat="server" Value="0" />
            <asp:HiddenField ID="hfHasPendingErrors"  runat="server" Value="0" />
            <asp:HiddenField ID="hfIsAdd"             runat="server" />

            <!-- ═══ Button bar ════════════════════════════════════════════ -->
            <div class="mp-btn-bar">
                <asp:Button ID="btnBack" runat="server" Text="Back"
                    CssClass="btn btn-default" OnClick="btnBack_Click"
                    CausesValidation="false" Style="display:none;" 
                     ToolTip="Go back to the previous step" />

                <asp:Button ID="btnNext" runat="server" Text="Next"
                    CssClass="btn btn-info" OnClick="btnNext_Click"
                    ValidationGroup="vgStep1"
                    OnClientClick="return validateAndDisableNext();" 
                     ToolTip="Proceed to the next step"/>

                <asp:Button ID="btnRectify" runat="server" Text="Rectify"
                    CssClass="btn btn-warning" OnClick="btnRectify_Click"
                    CausesValidation="false" Style="display:none;"
                    ToolTip="Attempt to automatically fix the pending issues listed above" />

                <asp:Button ID="btnProcess" runat="server" Text="Process"
                    CssClass="btn btn-success" OnClick="btnProcess_Click"
                    CausesValidation="false" Style="display:none;"
                    OnClientClick="return ConfirmProcess();"
                    ToolTip="Permanently close this period. This cannot be undone." />

                <asp:Button ID="btnClose" runat="server" Text="Close"
                    CssClass="btn btn-secondary" OnClick="btnClose_Click"
                    CausesValidation="false"
                    ToolTip="Exit without saving and log out" />
            </div>

        </div><!-- /mp-container -->
        </div><!-- /container-fluid -->

    </ContentTemplate>
    </asp:UpdatePanel>
</form>

    
    <script type="text/javascript">

        var btnProcessId = '<%= btnProcess.ClientID %>';

        function EnableProcessBtn() {
            $('#' + btnProcessId).prop('disabled', false);
        }

        /* Warn before executing month close */
        function ConfirmProcess() {
            return confirm('This will permanently close the period and protect it from backdated changes.\n\nProceed?');
        }

        function validateRemarksLength(source, args) {
            var remarks = args.Value || "";
            args.IsValid = remarks.length <= 500;
        }








        function resizeParentIframe() {
            try {
                var frame = window.frameElement; // this page's own <iframe> in Home.aspx
                if (frame) {
                    // Let layout settle, then measure actual content height
                    setTimeout(function () {
                        var height = document.body.scrollHeight;
                        frame.style.height = height + "px";
                        frame.setAttribute("height", height);
                    }, 50);
                }
            } catch (e) {
                // cross-origin or no frameElement access — ignore
            }
        }

        if (typeof Sys !== "undefined" && Sys.WebForms && Sys.WebForms.PageRequestManager) {
            Sys.WebForms.PageRequestManager.getInstance().add_endRequest(function () {
                window.scrollTo(0, 0);
                resizeParentIframe();
            });
        }

        // Also run once on initial load
        window.addEventListener("load", resizeParentIframe);









        

    </script>
</body>
