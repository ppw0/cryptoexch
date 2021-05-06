<%@ Page Title="Portfolio" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="Portfolio.aspx.cs" Inherits="Cryptoexch.Portfolio" Async="true" ViewStateMode="Enabled" EnableViewState="true" %>

<asp:Content ID="Content" ContentPlaceHolderID="MainContentLoggedIn" runat="Server">
    <div>
        <div class="container" id="main-content">
            <div class="row">
                <div class="col-md-12">
                    <div>
                        <ul class="nav nav-tabs" role="tablist">
                            <li class="nav-item"><a class="nav-link active" role="tab" data-toggle="tab" href="#tab-1">Overview</a></li>
                            <li class="nav-item"><a class="nav-link" role="tab" data-toggle="tab" href="#tab-2">New Order</a></li>
                            <li class="nav-item"><a class="nav-link" role="tab" data-toggle="tab" href="#tab-3">Orders</a></li>
                            <li class="nav-item"><a class="nav-link" role="tab" data-toggle="tab" href="#tab-4">Transactions</a></li>
                        </ul>
                        <div class="tab-content">
                            <div class="tab-pane active" role="tabpanel" id="tab-1">
                                <div class="row">
                                    <div class="col-md-6">
                                        <h4>Balances</h4>
                                        <div class="table-responsive table-striped">
                                            <table class="table" id="balance">
                                                <thead>
                                                    <tr>
                                                        <th>Currency</th>
                                                        <th>Balance</th>
                                                    </tr>
                                                </thead>
                                                <asp:ListView ID="lvBalances" runat="server" DataKeyNames="Name" ItemType="Cryptoexch.Models.ClientPortfolioView_Result" SelectMethod="lvBalances_GetData">
                                                    <ItemTemplate>
                                                        <tr>
                                                            <td><%# Item.Name  %></td>
                                                            <td><%# Item.Balance %></td>
                                                        </tr>
                                                    </ItemTemplate>
                                                    <LayoutTemplate>
                                                        <tbody>
                                                            <asp:PlaceHolder runat="server" ID="itemPlaceholder" />
                                                        </tbody>
                                                    </LayoutTemplate>
                                                </asp:ListView>
                                            </table>
                                        </div>
                                    </div>
                                    <div class="col-md-6">
                                        <h4>Exchange Rates</h4>
                                        <asp:ListView ID="lvCurrencyPairs" runat="server" DataKeyNames="ID" ItemType="Cryptoexch.Models.CurrencyPair" SelectMethod="lvCurrencyPairs_GetData">
                                            <ItemTemplate>
                                                <tr>
                                                    <td><%# Item.Heading %></td>
                                                    <td><%# Item.Last %></td>
                                                </tr>
                                            </ItemTemplate>
                                            <LayoutTemplate>
                                                <div class="table-responsive">
                                                    <table class="table table-striped" id="rates">
                                                        <thead>
                                                            <tr>
                                                                <th>Currency Pair</th>
                                                                <th>Exchange Rate</th>
                                                            </tr>
                                                        </thead>
                                                        <tbody>
                                                            <asp:PlaceHolder runat="server" ID="itemPlaceholder" />
                                                        </tbody>
                                                    </table>
                                                </div>
                                            </LayoutTemplate>
                                        </asp:ListView>
                                    </div>
                                </div>
                            </div>
                            <div class="tab-pane" role="tabpanel" id="tab-2">
                                <div class="row" style="padding-top: 35px;">
                                    <div class="col-md-6 order-form">
                                        <div class="form-group">
                                            <label class="order-label">Pair</label>
                                            <asp:DropDownList runat="server" ID="ddlPair" AutoPostBack="true" OnLoad="DdlPair_Load">
                                                <asp:ListItem Value="3" Text="BTC/EUR" Selected="True" />
                                                <asp:ListItem Value="5" Text="ETH/EUR" />
                                                <asp:ListItem Value="6" Text="XRP/EUR" />
                                                <asp:ListItem Value="10" Text="ETH/BTC" />
                                                <asp:ListItem Value="12" Text="XRP/BTC" />
                                                <asp:ListItem Value="14" Text="XRP/ETH" />
                                            </asp:DropDownList>
                                        </div>
                                        <div class="form-group">
                                            <label class="order-label">Type</label>
                                            <asp:UpdatePanel runat="server" ID="UpdatePanel1" class="btn-group btn-group-toggle">
                                                <ContentTemplate>
                                                    <asp:RadioButton ID="rbBuy" CssClass="btn btn-primary toggle-button active" runat="server" Checked="true" GroupName="btnBuySell" AutoPostBack="true" OnLoad="RbBuy_Load" Text="Buy"></asp:RadioButton>
                                                    <asp:RadioButton ID="rbSell" CssClass="btn btn-primary toggle-button" runat="server" GroupName="btnBuySell" AutoPostBack="true" OnLoad="RbSell_Load" Text="Sell"></asp:RadioButton>
                                                </ContentTemplate>
                                                <Triggers>
                                                    <asp:AsyncPostBackTrigger ControlID="ddlPair" />
                                                </Triggers>
                                            </asp:UpdatePanel>
                                        </div>
                                        <div class="form-group">
                                            <label class="order-label">Amount</label>
                                            <div class="d-inline-flex">
                                                <asp:UpdatePanel runat="server" ID="UpdatePanel6">
                                                    <ContentTemplate>
                                                        <asp:TextBox runat="server" ID="tbAmount" AutoPostBack="true" EnableViewState="true" />
                                                    </ContentTemplate>
                                                    <Triggers>
                                                        <asp:AsyncPostBackTrigger ControlID="tbLimit" />
                                                    </Triggers>
                                                </asp:UpdatePanel>
                                                <asp:UpdatePanel runat="server" ID="UpdatePanel3">
                                                    <ContentTemplate>
                                                        <asp:DropDownList ID="ddlCurrency" runat="server" AutoPostBack="true" Style="border-left: none;" OnLoad="DdlCurrency_Load" UpdateMode="Conditional" ChildrenAsTriggers="False">
                                                            <asp:ListItem Value="1" Text="EUR" />
                                                            <asp:ListItem Value="2" Text="USD" Enabled="false" />
                                                            <asp:ListItem Value="3" Text="BTC" Selected="True" />
                                                            <asp:ListItem Value="4" Text="ETH" Enabled="false" />
                                                            <asp:ListItem Value="5" Text="XRP" Enabled="false" />
                                                        </asp:DropDownList>
                                                    </ContentTemplate>
                                                    <Triggers>
                                                        <asp:AsyncPostBackTrigger ControlID="ddlPair" EventName="SelectedIndexChanged" />
                                                    </Triggers>
                                                </asp:UpdatePanel>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-6 order-form">
                                        <div class="form-group">
                                            <label class="order-label">Price</label>
                                            <asp:UpdatePanel runat="server" ID="UpdatePanel5" RenderMode="Inline">
                                                <ContentTemplate>
                                                    <asp:TextBox runat="server" ID="tbLimit" AutoPostBack="true" EnableViewState="true" />
                                                </ContentTemplate>
                                                <Triggers>
                                                    <asp:AsyncPostBackTrigger ControlID="tbAmount" />
                                                </Triggers>
                                            </asp:UpdatePanel>
                                            <asp:UpdatePanel runat="server" ID="UpdatePanel4" RenderMode="inline" class="order-label" Style="padding-left: 10px; padding-right: 10px; font-weight: bold;">
                                                <ContentTemplate>
                                                    <asp:Label runat="server" ID="lblSourceCurrency" OnLoad="LblSourceCurrency_Load" />
                                                </ContentTemplate>
                                                <Triggers>
                                                    <asp:AsyncPostBackTrigger ControlID="ddlPair" />
                                                    <asp:AsyncPostBackTrigger ControlID="ddlCurrency" />
                                                </Triggers>
                                            </asp:UpdatePanel>
                                            <asp:UpdatePanel runat="server" ID="UpdatePanel2" class="btn-group btn-group-toggle">
                                                <ContentTemplate>
                                                    <asp:RadioButton ID="rbMarket" CssClass="btn btn-primary toggle-button active" runat="server" GroupName="btnMarketLimit" AutoPostBack="true" OnLoad="RbMarket_Load" Text="market price" Checked="true"></asp:RadioButton>
                                                    <asp:RadioButton ID="rbLimit" CssClass="btn btn-primary toggle-button" runat="server" GroupName="btnMarketLimit" AutoPostBack="true" OnLoad="RbLimit_Load" Text="limit"></asp:RadioButton>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </div>
                                        <div class="form-group">
                                            <label class="order-label">Begin</label>
                                            <asp:DropDownList runat="server" ID="ddlOffset" OnSelectedIndexChanged="DdlOffsetSelected">
                                                <asp:ListItem Value="12" Text="now" />
                                                <asp:ListItem Value="13" Text="+24 hours" />
                                                <asp:ListItem Value="14" Text="+72 hours" />
                                            </asp:DropDownList>
                                        </div>
                                        <div class="form-group">
                                            <label class="order-label">Expiration</label>
                                            <asp:DropDownList runat="server" ID="ddlExpiration" OnSelectedIndexChanged="DdlExpirationSelected">
                                                <asp:ListItem Value="12" Text="none" />
                                                <asp:ListItem Value="13" Text="1 week" />
                                                <asp:ListItem Value="14" Text="1 month" />
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col">
                                        <div>
                                            <hr />
                                        </div>
                                        <div class="form-group" style="margin-top: 30px;">
                                            <asp:Label runat="server" CssClass="order-label" AssociatedControlID="lblTotal" Text="Total" />
                                            <asp:UpdatePanel runat="server" RenderMode="Inline">
                                                <ContentTemplate>
                                                    <asp:Label runat="server" ID="lblTotal" Style="font-weight: bold;" Text="" OnLoad="LblTotal_Load" />
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </div>
                                        <asp:UpdatePanel runat="server">
                                            <ContentTemplate>
                                                <asp:Button runat="server" ID="btnSubmit" CssClass="btn btn-primary btn-lg d-flex mx-auto action-button" Style="margin-top: 50px;" Text="Buy BTC with EUR" OnLoad="BtnSubmit_Load" OnClick="SubmitOrderForm" />
                                            </ContentTemplate>
                                            <Triggers>
                                                <asp:AsyncPostBackTrigger ControlID="ddlPair" />
                                                <asp:AsyncPostBackTrigger ControlID="rbBuy" />
                                                <asp:AsyncPostBackTrigger ControlID="rbSell" />
                                            </Triggers>
                                        </asp:UpdatePanel>
                                    </div>
                                </div>
                            </div>
                            <div class="tab-pane" role="tabpanel" id="tab-3">
                                <div class="col">
                                    <h4>Orders</h4>
                                    <asp:ListView ID="lvOrders" runat="server" DataKeyNames="ID" ItemType="Cryptoexch.Models.OrderView_Result" SelectMethod="LvOrders_GetData">
                                        <ItemTemplate>
                                            <tr>
                                                <td><%# Item.ID %></td>
                                                <td><%# Item.Beginning %></td>
                                                <td><%# Item.Expiration %></td>
                                                <td><%# Item.Type %></td>
                                                <td><%# Item.CurrencyPair %></td>
                                                <td><%# Item.Amount %></td>
                                                <td><%# Item.Total %></td>
                                            </tr>
                                        </ItemTemplate>
                                        <LayoutTemplate>
                                            <div class="table-responsive table-striped" style="margin-top: 10px; width: 100%;">
                                                <table class="table" id="orders">
                                                    <thead>
                                                        <tr>
                                                            <th>ID</th>
                                                            <th>Beginning</th>
                                                            <th>Expiration</th>
                                                            <th>Type</th>
                                                            <th>Currency Pair</th>
                                                            <th>Amount</th>
                                                            <th>Total</th>
                                                        </tr>
                                                    </thead>
                                                    <tbody>
                                                        <asp:PlaceHolder ID="ItemPlaceHolder" runat="server" />
                                                    </tbody>
                                                </table>
                                            </div>
                                        </LayoutTemplate>
                                    </asp:ListView>
                                </div>
                            </div>
                            <div class="tab-pane" role="tabpanel" id="tab-4">
                                <div class="col">
                                    <h4 class="d-flex justify-content-between">Transaction History
                                        <button class="btn btn-primary action-button" type="button" style="margin-left: 1%;">Export</button>
                                    </h4>
                                    <asp:ListView ID="lvTransactions" runat="server" DataKeyNames="ID" ItemType="Cryptoexch.Models.Transaction" SelectMethod="LvTransactions_GetData">
                                        <ItemTemplate>
                                            <tr>
                                                <td><%# Item.ID %></td>
                                                <td><%# Item.Timestamp %></td>
                                                <td><%# Item.Type %></td>
                                                <td><%# Item.CurrencyID %></td>
                                                <td><%# Item.Amount %></td>
                                                <td><%# Item.Fee %></td>
                                            </tr>
                                        </ItemTemplate>
                                        <LayoutTemplate>
                                            <div class="table-responsive table-striped" style="margin-top: 10px; width: 100%;">
                                                <table class="table" id="transactions">
                                                    <thead>
                                                        <tr>
                                                            <th>ID</th>
                                                            <th>Timestamp</th>
                                                            <th>Type</th>
                                                            <th>Currency</th>
                                                            <th>Amount</th>
                                                            <th>Fee</th>
                                                        </tr>
                                                    </thead>
                                                    <tbody>
                                                        <asp:PlaceHolder runat="server" ID="itemPlaceholder" />
                                                    </tbody>
                                                </table>
                                            </div>
                                        </LayoutTemplate>
                                    </asp:ListView>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>