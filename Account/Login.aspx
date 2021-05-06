<%@ Page Title="Login" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeFile="Login.aspx.cs" Inherits="Cryptoexch.Login" Async="true" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContentAnonymous" runat="server">
    <div class="container hero" id="main-content">
        <div class="row">
            <div class="col-12 col-lg-6 col-xl-5 offset-xl-1">
                <h1>Welcome.</h1>
                <p>This one is the best. Really.</p>
                <button class="btn btn-light btn-lg action-button" type="button">Learn More</button>
            </div>
            <div class="col-9 mx-auto col-md-7 col-sm-9 col-lg-5">
                <section id="loginForm">
                    <div class="login">
                        <h3 style="padding-top: 43px; padding-bottom: 49px;">Log In</h3>
                        <div class="form-group">
                            <asp:Label runat="server" AssociatedControlID="UserName">Username</asp:Label>
                            <asp:TextBox runat="server" ID="UserName" CssClass="form-control" />
                            <asp:RequiredFieldValidator runat="server" ControlToValidate="UserName" CssClass="text-danger" Display="Dynamic" ErrorMessage="Username Required" />
                        </div>
                        <div class="form-group">
                            <asp:Label runat="server" AssociatedControlID="Password">Password</asp:Label>
                            <asp:TextBox runat="server" ID="Password" TextMode="Password" CssClass="form-control" />
                            <asp:RequiredFieldValidator runat="server" ControlToValidate="Password" CssClass="text-danger" Display="Dynamic" ErrorMessage="Password Required" />
                        </div>
                        <div class="form-group">
                            <asp:Label runat="server" AssociatedControlID="TwoFactor">2FA</asp:Label>
                            <asp:TextBox runat="server" ID="TwoFactor" CssClass="form-control" placeholder="(6-digit code)" />
                            <asp:RequiredFieldValidator runat="server" ControlToValidate="TwoFactor" CssClass="text-danger" Display="Dynamic" ErrorMessage="6-digit Code Required" />
                        </div>
                        <div class="form-group d-flex justify-content-around" style="border-radius: 5px; border: 1px solid lightgray; padding-top: 5%; padding-bottom: 5%; margin-top: 10%; margin-bottom: 10%;">
                            <div class="form-check form-inline">
                                <asp:CheckBox runat="server" CssClass="form-check-input form-check-input-large" ID="NoRobot" />
                                <asp:Label runat="server" CssClass="form-check-label" AssociatedControlID="NoRobot" Style="font-size: 17px;">I am not a robot</asp:Label>
                                <asp:CustomValidator runat="server" ID="CheckBoxRequired" Display="Dynamic" OnServerValidate="CheckBoxRequired_ServerValidate"></asp:CustomValidator>
                            </div>
                            <img src="~/Images/recaptcha.png" alt="reCAPTCHA" runat="server">
                        </div>
                        <asp:PlaceHolder runat="server" ID="ErrorMessage" Visible="false">
                            <p class="text-danger">
                                <asp:Literal runat="server" ID="FailureText" /></p>
                        </asp:PlaceHolder>
                        <asp:Button runat="server" OnClick="LogIn" CssClass="btn btn-primary d-flex m-auto action-button" Text="Log In" />
                        <ul class="list-group" style="margin-top: 40px;">
                            <li class="list-group-item"><a href="#">Forgot password?</a></li>
                            <li class="list-group-item">
                                <asp:HyperLink ID="SignupLink" ViewStateMode="Disabled" runat="server" Text="Create New Account" NavigateUrl="~/Signup"></asp:HyperLink>
                            </li>
                        </ul>
                    </div>
                </section>
            </div>
        </div>
    </div>
</asp:Content>