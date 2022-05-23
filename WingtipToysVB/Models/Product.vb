Imports System.ComponentModel.DataAnnotations
Imports System.ComponentModel.DataAnnotations.Schema
Imports System.Data.Entity

Namespace Models

    Public Class Product

        <ScaffoldColumn(False)>
        Public Property ProductID As Integer

        <Required>
        <StringLength(100)>
        <Display(Name:="Name")>
        Public Property ProductName As String

        <Required>
        <StringLength(1000)>
        <Display(Name:="Product Description")>
        Public Property Description As String
        Public Property ImagePath As String

        <Display(Name:="Price")>
        Public Property UnitPrice As Double
        Public Property CategoryID As Integer

        <ForeignKey("CategoryID")>
        Public Property Category As Category
    End Class
End Namespace