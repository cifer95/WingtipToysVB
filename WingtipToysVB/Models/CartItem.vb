Imports System.ComponentModel.DataAnnotations
Imports System.ComponentModel.DataAnnotations.Schema

Namespace Models

    Public Class CartItem

        <Key>
        Public Property ItemId As String
        Public Property CartId As String
        Public Property Quantity As Integer
        Public Property DateCreated As DateTime
        Public Property ProductId As Integer

        <ForeignKey("ProductId")>
        Public Overridable Property Product As Product
    End Class
End Namespace