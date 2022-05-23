Imports System.Web.ModelBinding
Imports WingtipToysVB.Models

Public Class ProductList
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub

    Public Function GetProducts(<QueryString("id")> categoryId As Integer?) As IQueryable(Of Product)
        Dim _db = New ProductDbContext
        Dim query As IQueryable(Of Product) = _db.Products
        If categoryId.HasValue And categoryId > 0 Then query = query.Where(Function(p) p.CategoryID = categoryId)
        Return query
    End Function
End Class