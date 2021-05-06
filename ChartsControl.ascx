<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ChartsControl.ascx.cs" Inherits="Cryptoexch.ChartsControl" ClassName="ChartsControl" %>

<div class="container" id="main-content">
    <div class="row">
        <div class="col-md-6">
            <div class="row">
                <div class="col d-inline-flex">
                    <asp:DropDownList runat="server" ID="ddlCurrencyPairs" AutoPostBack="true">
                        <asp:ListItem Value="3" Text="BTC/EUR" />
                        <asp:ListItem Value="5" Text="ETH/EUR" />
                        <asp:ListItem Value="6" Text="XRP/EUR" />
                        <asp:ListItem Value="10" Text="ETH/BTC" />
                        <asp:ListItem Value="12" Text="XRP/BTC" />
                        <asp:ListItem Value="14" Text="XRP/ETH" />
                    </asp:DropDownList>
                    <select>
                        <option value="11" selected="">1D</option>
                        <option value="12">5D</option>
                        <option value="13">1M</option>
                        <option value="14">3M</option>
                        <option value="15">6M</option>
                        <option value="16">1Y</option>
                    </select>
                    <select>
                        <option value="13" selected="">line graph</option>
                        <option value="14">candlestick</option>
                    </select>
                    <button class="btn btn-primary action-button" type="button" style="margin-left: 1%;">Export</button>
                </div>
            </div>
            <div class="row" style="padding-top: 20px">
                <canvas id="monthlyHighs" height="250"></canvas>
            </div>
        </div>
        <div class="col-md-6">
            <asp:UpdatePanel ID="upCurrencyPairs" runat="server" UpdateMode="Always">
                <ContentTemplate>
                    <asp:ListView ID="lvCurrencyPairs" runat="server" DataKeyNames="ID" ItemType="Cryptoexch.Models.CurrencyPair" SelectMethod="LvCurrencyPairInfo_GetData">
                        <ItemTemplate>
                            <h4>
                                <asp:Label runat="server" ID="lblCurrencyPair" Text="<%# Item.Heading %>" /></h4>
                            <div class="table-responsive">
                                <table class="table table-striped">
                                    <tbody>
                                        <tr>
                                            <th style="width: 50%">last</th>
                                            <td><%# Item.Last %></td>
                                        </tr>
                                        <tr>
                                            <th style="width: 50%">open</th>
                                            <td><%# Item.Open %></td>
                                        </tr>
                                        <tr>
                                            <th style="width: 50%">high</th>
                                            <td><%# Item.High %></td>
                                        </tr>
                                        <tr>
                                            <th style="width: 50%">low</th>
                                            <td><%# Item.Low %></td>
                                        </tr>
                                        <tr>
                                            <th style="width: 50%">24 hours</th>
                                            <td><%# Item.C24hours %></td>
                                        </tr>
                                        <tr>
                                            <th style="width: 50%">% change</th>
                                            <td><%# Item.Change %></td>
                                        </tr>
                                    </tbody>
                                </table>
                            </div>
                        </ItemTemplate>
                        <LayoutTemplate>
                            <asp:PlaceHolder ID="ItemPlaceHolder" runat="server" />
                        </LayoutTemplate>
                    </asp:ListView>
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="ddlCurrencyPairs" />
                </Triggers>
            </asp:UpdatePanel>
        </div>
    </div>
</div>