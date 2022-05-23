Imports WingtipToysVB.Logic

Public Class AddToCart
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim rawId = Request.QueryString("productID")
        Dim productId As Integer
        If Not String.IsNullOrEmpty(rawId) And Integer.TryParse(rawId, productId) Then
            Using usersShoppingCart = New ShoppingCartActions()
                usersShoppingCart.AddToCart(Convert.ToInt16(rawId))
            End Using
        Else
            Debug.Fail("Error : We should never get to AddToCart.aspx without a ProductID.")
            Throw New Exception("Error : It is illegal to load AddToCart.aspx without setting a ProductId.")
        End If
        Response.Redirect("ShoppingCart.aspx")
    End Sub

End Class