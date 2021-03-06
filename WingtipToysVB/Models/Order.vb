Imports System.ComponentModel
Imports System.ComponentModel.DataAnnotations

Namespace Models

    Public Class Order
        Public Property OrderId As Integer

        Public Property OrderDate As DateTime

        Public Property Username As String

        <Required(ErrorMessage:="First Name is required")>
        <DisplayName("First Name")>
        <StringLength(160)>
        Public Property FirstName As String

        <Required(ErrorMessage:="Last Name is required")>
        <DisplayName("Last Name")>
        <StringLength(160)>
        Public Property LastName As String

        <Required(ErrorMessage:="Address is required")>
        <StringLength(70)>
        Public Property Address As String

        <Required(ErrorMessage:="City is required")>
        <StringLength(40)>
        Public Property City As String

        <Required(ErrorMessage:="State is required")>
        <StringLength(40)>
        Public Property State As String

        <Required(ErrorMessage:="Postal code is required")>
        <DisplayName("Postal Code")>
        <StringLength(10)>
        Public Property PostalCode As String

        <Required(ErrorMessage:="Country is required")>
        <StringLength(40)>
        Public Property Country As String

        <StringLength(24)>
        Public Property Phone As String

        <Required(ErrorMessage:="Email Address is required")>
        <DisplayName("Email Address")>
        <RegularExpression("[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,4}", ErrorMessage:="Email is not valid")>
        <DataType(DataType.EmailAddress)>
        Public Property Email As String

        <ScaffoldColumn(False)>
        Public Property Total As Decimal

        <ScaffoldColumn(False)>
        Public Property PaymentTransactionId As String

        Public Property OrderDetails As List(Of OrderDetail)
    End Class
End Namespace