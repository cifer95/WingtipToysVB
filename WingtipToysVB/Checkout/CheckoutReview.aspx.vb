Imports WingtipToysVB.Models

Public Class CheckoutReview
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub

    Protected Sub CheckoutConfirm_Click(sender As Object, e As System.EventArgs)
        Session("userCheckoutCompleted") = "true"
        Response.Redirect("~/Checkout/CheckoutComplete.aspx")
    End Sub

End Class