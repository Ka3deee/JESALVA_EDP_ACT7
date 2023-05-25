Imports System.Data.Common
Imports System.Diagnostics.Eventing
Imports Excel = Microsoft.Office.Interop.Excel
Imports MySql.Data.MySqlClient

Public Class Form5
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        With Me
            Call Connect_to_DB()
            Dim mycmd As New MySqlCommand
            Try
                strSQL = "Insert into products values('" _
                & .TextBox2.Text & "', '" _
                & .TextBox1.Text & "', '" _
                & .TextBox5.Text & "', '" _
                & .TextBox3.Text & "', '" _
                & .TextBox4.Text & "')"
                mycmd.CommandText = strSQL
                mycmd.Connection = myconn
                mycmd.ExecuteNonQuery()
                MsgBox("Record Successfully Added!")
                Call Clear_Boxes()
            Catch ex As MySqlException
                MsgBox(ex.Number & " " & ex.Message)
            End Try
            Disconnect_to_DB()
        End With
    End Sub

    Private Sub Clear_Boxes()
        With Me
            .TextBox2.Text = vbNullString
            .TextBox1.Text = ""
            .TextBox5.Text = ""
            .TextBox3.Text = ""
            .TextBox4.Text = ""
        End With
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Me.Hide()
        Form4.Show()
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Me.DataGridView1.Rows.Clear()
        Dim strsql As String
        Dim mycommand As New MySqlCommand
        strsql = "Select * from products"
        Connect_to_DB()
        With mycommand
            .Connection = myconn
            .CommandType = CommandType.Text
            .CommandText = strsql
        End With
        Dim myreader As MySqlDataReader
        myreader = mycommand.ExecuteReader

        DataGridView1.Columns.Add("product_id", "Product ID")
        DataGridView1.Columns.Add("product_name", "Product Name")
        DataGridView1.Columns.Add("description", "Description")
        DataGridView1.Columns.Add("size", "Size")
        DataGridView1.Columns.Add("price", "Price")
        While myreader.Read()
            Me.DataGridView1.Rows.Add(New Object() {myreader.Item("product_id"), myreader.Item("product_name"), myreader.Item("description"), myreader.Item("size"), myreader.Item("price")})
        End While
        Disconnect_to_DB()
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        Call Connect_to_DB()
        Dim productID As String = TextBox6.Text
        Dim productFound As Boolean = False

        ' Clear existing rows in DataGridView1
        DataGridView1.Rows.Clear()

        ' Search for the product in the database
        Dim strSQL As String = "SELECT product_id, product_name, description, size, price FROM products WHERE product_id = '" & productID & "'"
        Dim mycmd As New MySqlCommand(strSQL, myconn)
        Dim myreader As MySqlDataReader = mycmd.ExecuteReader()

        If myreader.HasRows Then
            While myreader.Read()
                TextBox2.Text = myreader.GetString(0)
                TextBox1.Text = myreader.GetString(1)
                TextBox5.Text = myreader.GetString(2)
                TextBox3.Text = myreader.GetString(3)
                TextBox4.Text = myreader.GetString(4)

                ' Add the searched product to DataGridView1
                DataGridView1.Rows.Add(myreader.GetString(0), myreader.GetString(1), myreader.GetString(2), myreader.GetString(3), myreader.GetString(4))

                productFound = True
            End While
        Else
            MsgBox("Product ID does not exist!")
        End If

        myreader.Close()
        Call Disconnect_to_DB()
    End Sub
End Class