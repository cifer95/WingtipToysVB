<%@ Page Title="" Language="vb" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="CheckoutReview.aspx.vb" Inherits="WingtipToysVB.CheckoutReview" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h1>Order Review</h1>
    <p></p>
    <h3 style="padding-left: 33px">Products:</h3>
    <asp:GridView runat="server" ID="OrderItemList" AutoGenerateColumns="False" GridLines="Both" CellPadding="10" Width="500" BorderColor="#efeeef" BorderWidth="33">
        <Columns>
            <asp:BoundField DataField="ProductId" HeaderText=" Product ID" />
            <asp:BoundField DataField="Product.ProductName" HeaderText=" Product Name" />
            <asp:BoundField DataField="Product.UnitPrice" HeaderText="Price (each)" DataFormatString="{0:c}" />
            <asp:BoundField DataField="Quantity" HeaderText="Quantity" />
        </Columns>
    </asp:GridView>
    <asp:DetailsView runat="server" ID="ShipInfo" AutoGenerateRows="False" GridLines="None" CellPadding="10" BorderStyle="None" CommandRowStyle-BorderStyle="None">
        <Fields>
            <asp:TemplateField>
                <ItemTemplate>
                    <h3>Shipping Address:</h3>
                    <br/>
                    <asp:Label runat="server" ID="FirstName" Text='<%#: Eval("FirstName") %>'></asp:Label>
                    <asp:Label runat="server" ID="LastName" Text='<%#: Eval("LastName") %>'></asp:Label>
                    <br/>
                    <asp:Label runat="server" ID="Address" Text='<%#: Eval("Address") %>'></asp:Label>
                    <br/>
                    <asp:Label runat="server" ID="City" Text='<%#: Eval("City") %>'></asp:Label>
                    <asp:Label runat="server" ID="State" Text='<%#: Eval("State") %>'></asp:Label>
                    <asp:Label runat="server" ID="PostalCode" Text='<%#: Eval("PostalCode") %>'></asp:Label>
                    <p></p>
                    <h3>Order Total:</h3>
                    <br/>
                    <asp:Label runat="server" ID="Total" Text='<%#: Eval("Total", "{0:C}") %>'></asp:Label>
                </ItemTemplate>
                <ItemStyle HorizontalAlign="Left" />
            </asp:TemplateField>
        </Fields>
    </asp:DetailsView>
    <p></p>
    <hr/>
    <asp:Button runat="server" ID="CheckoutConfirm" Text="Complete Order" OnClick="CheckoutConfirm_Click" />
</asp:Content>
