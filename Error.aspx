<%@ Page Title="Error" Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeFile="Error.aspx.cs" Inherits="Cryptoexch.Error" %>

<%@ Register Src="~/ErrorControl.ascx" TagName="Error" TagPrefix="uc" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContentAnonymous" runat="Server">
    <asp:Literal runat="server" Text="</div>" />
    <uc:Error runat="server" />
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContentLoggedIn" runat="server">
    <uc:Error runat="server" />
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="MainContentAdmin" runat="server">
    <uc:Error runat="server" />
</asp:Content>