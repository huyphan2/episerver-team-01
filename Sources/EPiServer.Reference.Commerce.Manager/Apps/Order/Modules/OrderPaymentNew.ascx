<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="OrderPaymentNew.ascx.cs" Inherits="Mediachase.Commerce.Manager.Apps.Order.Modules.OrderPaymentProcess" %>
<%@ Register TagPrefix="mc2" Assembly="Mediachase.BusinessFoundation" Namespace="Mediachase.BusinessFoundation" %>
<%@ Register Src="~/Apps/Order/Modules/PaymentTransactionTypeControl.ascx" TagName="TransactionTypeControl" TagPrefix="ecf" %>
<%@ Register Src="~/Apps/Order/Modules/PaymentMethodSelectControl.ascx" TagName="PaymentMethodSelectControl" TagPrefix="ecf" %>

<div style="padding: 5px; height: 474px; overflow:auto;">

	<asp:Label runat="server" ID="lblErrorInfo" Style="color: Red"></asp:Label>
	<ecf:TransactionTypeControl runat="server" ID="TransactionTypeControl1" />
	<ecf:PaymentMethodSelectControl runat="server" ID="PaymentMethodSelectControl1" />			
	<table width="100%">
		<tr>
			<td align="right" colspan="2" style="padding-top: 10px; padding-right: 10px;">
				<mc2:IMButton ID="btnSave" runat="server" style="width: 105px;" OnServerClick="btnSave_ServerClick">
				</mc2:IMButton>
				&nbsp;
				<mc2:IMButton ID="btnCancel" runat="server" style="width: 105px;" CausesValidation="false">
				</mc2:IMButton>
			</td>
		</tr>
	</table>	
</div>
