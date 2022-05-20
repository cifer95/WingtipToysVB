Imports System.Web.ModelBinding
Imports WingtipToysVB.Models

Public Class ProductDetails
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub

    Public Function GetProduct(<QueryString("productID")> productId As Nullable(Of Integer)) As IQueryable(Of Product)
        Dim _db = New ProductContext
        Dim query As IQueryable(Of Product) = _db.Products
        If (productId.HasValue And productId > 0) Then
            query = query.Where(Function(p) p.ProductID = productId)
        Else
            query = Nothing
        End If

        Return query
    End Function

End Class