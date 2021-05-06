<%@ Page Title="Signup" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeFile="Signup.aspx.cs" Inherits="Cryptoexch.Signup" %>

<asp:Content runat="server" ID="MainContent" ContentPlaceHolderID="MainContentAnonymous">
    <div class="container hero" id="main-content">
        <div class="row">
            <div class="col-auto col-lg-6 col-xl-5 offset-xl-1">
                <div class="login">
                    <h3 style="padding-top: 43px; padding-bottom: 49px;">Create New Account</h3>
                    <div class="form-group">
                        <asp:Label runat="server" AssociatedControlID="tbEmail">E-mail</asp:Label>
                        <asp:TextBox runat="server" ID="tbEmail" CssClass="form-control" />
                        <asp:RequiredFieldValidator runat="server" ControlToValidate="tbEmail" CssClass="text-danger" Display="Dynamic" ErrorMessage="E-mail address required" />
                        <asp:RegularExpressionValidator runat="server" ControlToValidate="tbEmail" SetFocusOnError="true" Display="Dynamic" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" CssClass="text-danger" ErrorMessage="Bad e-mail address" />
                    </div>
                    <div class="form-group">
                        <asp:Label runat="server" AssociatedControlID="tbUserName">Username</asp:Label>
                        <asp:TextBox runat="server" ID="tbUserName" CssClass="form-control" />
                        <asp:RequiredFieldValidator runat="server" ControlToValidate="tbUserName" CssClass="text-danger" Display="Dynamic" ErrorMessage="Username required" />
                    </div>
                    <div class="form-group">
                        <asp:Label runat="server" AssociatedControlID="tbPassword">Password</asp:Label>
                        <asp:TextBox runat="server" ID="tbPassword" TextMode="Password" CssClass="form-control" />
                        <small class="form-text text-muted">The password must contain at least 9 characters that are letters, numbers and special characters.</small>
                        <asp:RequiredFieldValidator runat="server" ControlToValidate="tbPassword" CssClass="text-danger" Display="Dynamic" ErrorMessage="Password required" />
                        <asp:RegularExpressionValidator runat="server" ControlToValidate="tbPassword" Display="Dynamic" ValidationExpression="^(?=(.*[a-zA-Z].*){2,})(?=.*\d.*)(?=.*\W.*)[a-zA-Z0-9\S]{9,}$" CssClass="text-danger" ErrorMessage="Password does not fit the requirements." />
                    </div>
                    <div class="form-group">
                        <asp:Label runat="server" AssociatedControlID="tbConfirmPassword">Confirm Password</asp:Label>
                        <asp:TextBox runat="server" ID="tbConfirmPassword" TextMode="Password" CssClass="form-control" />
                        <asp:RequiredFieldValidator runat="server" ControlToValidate="tbConfirmPassword" CssClass="text-danger" Display="Dynamic" ErrorMessage="Password required" />
                        <asp:CompareValidator runat="server" ControlToCompare="tbPassword" ControlToValidate="tbConfirmPassword" CssClass="text-danger" Display="Dynamic" ErrorMessage="Passwords do not match" />
                    </div>
                    <small class="form-text text-muted" style="margin-bottom: 16px; margin-top: 37px;">optional</small>
                    <div class="form-group">
                        <asp:Label runat="server" AssociatedControlID="ddlCountry">Country of Residence</asp:Label>
                        <asp:DropDownList runat="server" CssClass="form-control" ID="ddlCountry">
                            <asp:ListItem Text="Spain" Selected="True" Value="12"></asp:ListItem>
                            <asp:ListItem Text="Nigeria" Value="13"></asp:ListItem>
                            <asp:ListItem Text="Switzerland" Value="14"></asp:ListItem>
                        </asp:DropDownList>
                    </div>
                    <div class="form-group">
                        <asp:Label runat="server" AssociatedControlID="ddlTimeZone">Time Zone</asp:Label>
                        <asp:DropDownList runat="server" CssClass="form-control" ID="ddlTimeZone">
                            <asp:ListItem Text="[UTC +00:00] Dublin, Edinburgh, Lisbon, London" Selected="True" Value="12"></asp:ListItem>
                            <asp:ListItem Text="[UTC +01:00] Amsterdam, Berlin, Ljubljana, Prague" Value="13"></asp:ListItem>
                            <asp:ListItem Text="[UTC +02:00] Athens, Beirut, Cairo, Tripoli" Value="14"></asp:ListItem>
                        </asp:DropDownList>
                    </div>
                    <div class="form-check form-inline" style="margin-bottom: 40px; margin-top: 42px;">
                        <asp:CheckBox runat="server" CssClass="form-check-input" ID="cbTermsOfUse" />
                        <asp:Label runat="server" CssClass="form-check-label" AssociatedControlID="cbTermsOfUse">I agree with the&nbsp;<a href="#">Terms of Use</a>.
                            <asp:CustomValidator runat="server" ID="CheckBoxRequired" OnServerValidate="CheckBoxRequired_ServerValidate" CssClass="text-danger" Display="Dynamic" ErrorMessage="*"></asp:CustomValidator>
                        </asp:Label>
                    </div>
                    <asp:PlaceHolder runat="server" ID="ErrorMessage" Visible="false">
                        <p class="text-danger">
                            <asp:Literal runat="server" ID="FailureText" /></p>
                    </asp:PlaceHolder>
                    <asp:Button runat="server" CssClass="btn btn-primary d-flex m-auto action-button" Text="Create" OnClick="CreateUser_OnClick" />
                </div>
            </div>
        </div>
    </div>
</asp:Content>