<%@ Page Title="Charts" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="Charts.aspx.cs" Inherits="Cryptoexch.Charts" %>

<%@ Register Src="~/ChartsControl.ascx" TagName="Charts" TagPrefix="uc" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContentAnonymous" runat="Server">
    <asp:Literal runat="server" Text="</div>" />
    <uc:charts runat="server" />
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContentLoggedIn" runat="server">
    <uc:charts runat="server" />
</asp:Content>