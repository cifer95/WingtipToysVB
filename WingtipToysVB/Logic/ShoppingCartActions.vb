Imports WingtipToysVB.Models

Namespace Logic

    Public Class ShoppingCartActions
        Implements IDisposable

        Public Property ShoppingCartId() As String
        Private Property _db As ProductDbContext = New ProductDbContext()
        Public Const CartSessionKey = "CartId"

        Public Sub Dispose() Implements IDisposable.Dispose
            If _db IsNot Nothing Then
                _db.Dispose()
                _db = Nothing
            End If
        End Sub

        Public Sub AddToCart(id As Integer)
            ShoppingCartId = GetCartId()

            Dim cartItem = _db.ShoppingCartItems.SingleOrDefault(Function(c) c.CartId = ShoppingCartId And c.ProductId = id)

            If cartItem Is Nothing Then
                ' Create a new cart item if no cart item exists
                cartItem = New CartItem With {.ItemId = Guid.NewGuid().ToString(), .ProductId = id, .CartId = ShoppingCartId, .Product = _db.Products.SingleOrDefault(Function(p) p.ProductID = id), .Quantity = 1, .DateCreated = DateTime.Now}

                _db.ShoppingCartItems.Add(cartItem)
            Else
                ' If the item does exist in the cart
                ' Then add one to the quantity.
                cartItem.Quantity += 1
            End If
            _db.SaveChanges()
        End Sub

        Public Function GetCartId() As String
            If HttpContext.Current.Session(CartSessionKey) Is Nothing Then
                If (Not String.IsNullOrWhiteSpace(HttpContext.Current.User.Identity.Name)) Then
                    HttpContext.Current.Session(CartSessionKey) = HttpContext.Current.User.Identity.Name
                Else
                    ' Generate a new random GUID using System.Guid class.
                    Dim tempCartId = Guid.NewGuid()
                    HttpContext.Current.Session(CartSessionKey) = tempCartId.ToString()
                End If
            End If
            Return HttpContext.Current.Session(CartSessionKey).ToString()
        End Function

        Public Function GetCartItems() As List(Of CartItem)
            ShoppingCartId = GetCartId()

            Return _db.ShoppingCartItems.Where(Function(c) c.CartId = ShoppingCartId).ToList()
        End Function

        Public Function GetTotal() As Decimal
            ShoppingCartId = GetCartId()
            ' Multiply product price by quantity of that product to get
            ' the current price for each of those products in the cart.
            ' Sum all product price totals to get the cart total.
            Dim total As Decimal? = Decimal.Zero
            total = CType((From cartItems In _db.ShoppingCartItems Where cartItems.CartId = ShoppingCartId Select CType((cartItems.Quantity), Integer?) * cartItems.Product.UnitPrice).Sum(), Decimal?)

            Return If(total, Decimal.Zero)
        End Function

        Public Function GetCart(context As HttpContext) As ShoppingCartActions
            Using cart = New ShoppingCartActions()
                cart.ShoppingCartId = cart.GetCartId()
                Return cart
            End Using
        End Function

        Public Sub UpdateShoppingCartDatabase(cartId As String, CartItemUpdates As ShoppingCartUpdates())
            Using db = New ProductDbContext()
                Try
                    Dim CartItemCount = CartItemUpdates.Count()
                    Dim myCart As List(Of CartItem) = GetCartItems()
                    For Each cartItem As CartItem In myCart
                        ' iterate through all rows within shopping cart list
                        For i As Integer = 0 To CartItemCount - 1
                            If cartItem.Product.ProductID = CartItemUpdates(i).ProductId Then
                                If CartItemUpdates(i).PurchaseQuantity < 1 Or CartItemUpdates(i).RemoveItem = True Then
                                    RemoveItem(cartId, cartItem.ProductId)
                                Else
                                    UpdateItem(cartId, cartItem.ProductId, CartItemUpdates(i).PurchaseQuantity)
                                End If
                            End If
                        Next
                    Next
                Catch ex As Exception
                    Throw New Exception("ERROR: Unable to Update Cart Database - " + ex.Message.ToString(), ex)
                End Try
            End Using
        End Sub

        Public Sub RemoveItem(removeCartID As String, removeProductID As Integer)
            Using _db = New ProductDbContext()
                Try
                    Dim myItem = (From c In _db.ShoppingCartItems Where c.CartId = removeCartID AndAlso c.Product.ProductID = removeProductID Select c).FirstOrDefault()
                    If myItem IsNot Nothing Then
                        ' Remove Item
                        _db.ShoppingCartItems.Remove(myItem)
                        _db.SaveChanges()
                    End If
                Catch ex As Exception
                    Throw New Exception("ERROR: Unable to Remove Cart Item - " + ex.Message.ToString(), ex)
                End Try
            End Using
        End Sub

        Public Sub UpdateItem(updateCartID As String, updateProductID As Integer, quantity As Integer)
            Using _db = New ProductDbContext()
                Try
                    Dim myItem = (From c In _db.ShoppingCartItems Where c.CartId = updateCartID AndAlso c.Product.ProductID = updateProductID Select c).FirstOrDefault()
                    If myItem IsNot Nothing Then
                        myItem.Quantity = quantity
                        _db.SaveChanges()
                    End If
                Catch ex As Exception
                    Throw New Exception("ERROR: Unable to Update Cart Item - " + ex.Message.ToString(), ex)
                End Try
            End Using
        End Sub

        Public Sub EmptyCart()
            ShoppingCartId = GetCartId()
            Dim cartItems = _db.ShoppingCartItems.Where(Function(c) c.CartId = ShoppingCartId)
            For Each cartItem As CartItem In cartItems
                _db.ShoppingCartItems.Remove(cartItem)
            Next
            ' Save changes
            _db.SaveChanges()
        End Sub

        Public Function GetCount() As Integer
            ShoppingCartId = GetCartId()

            ' Get the count of each item in the cart and sum them up
            Dim count As Integer? = (From cartItems In _db.ShoppingCartItems Where cartItems.CartId = ShoppingCartId Select CType(cartItems.Quantity, Integer?)).Sum()
            ' Return 0 if all entries are null
            Return If(count, 0)
        End Function

        Public Structure ShoppingCartUpdates
            Public ProductId As Integer
            Public PurchaseQuantity As Integer
            Public RemoveItem As Boolean
        End Structure

        Public Sub MigrateCart(cartId As String, userName As String)
            Dim shoppingCart = _db.ShoppingCartItems.Where(Function(c) c.CartId = cartId)
            For Each item As CartItem In shoppingCart
                item.CartId = userName
            Next

            HttpContext.Current.Session(CartSessionKey) = userName
            _db.SaveChanges()
        End Sub
    End Class
End Namespace