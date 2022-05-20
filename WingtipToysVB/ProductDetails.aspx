<%@ Page Title="Product Details" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="ProductDetails.aspx.vb" Inherits="WingtipToysVB.ProductDetails" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <asp:FormView runat="server"
                  ID="productDetail"
                  ItemType="WingtipToysVB.Models.Product"
                  SelectMethod="GetProduct"
                  RenderOuterTable="False">
        <ItemTemplate>
            <div>
                <h1><%#:Item.ProductName %></h1>
            </div>
            <br/>
            <table>
                <tr>
                    <td>
                        <img src="/Catalog/Images/<%#:Item.ImagePath %>" style="border: solid; height: 300px;" alt="<%#:Item.ProductName %>" />
                    </td>
                    <td>&nbsp;</td>
                    <td style="vertical-align: top; text-align: left;">
                        <b>Description:</b><br/><%#:Item.Description %>
                        <br/>
                        <span><b>Price:</b>&nbsp;<%#:String.Format("{0:c}", Item.UnitPrice) %></span>
                        <br/>
                    </td>
                </tr>
            </table>
        </ItemTemplate>
    </asp:FormView>
</asp:Content>
