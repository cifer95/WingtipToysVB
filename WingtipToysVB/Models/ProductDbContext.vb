﻿Imports System.Data.Entity

Namespace Models
    Public Class ProductDbContext
        Inherits DbContext
        Public Property Products() As DbSet(Of Product)
        Public Property Categories() As DbSet(Of Category)
        Public Property ShoppingCartItems() As DbSet(Of CartItem)

        Public Sub New()
            MyBase.New("WingtipToys")
        End Sub
    End Class
End Namespace