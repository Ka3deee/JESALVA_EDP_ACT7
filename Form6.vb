Imports MySql.Data.MySqlClient

Public Class Form6
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        With Me
            Call Connect_to_DB()
            Dim mycmd As New MySqlCommand
            Try
                strSQL = "Update products set product_id = '" _
                & .TextBox2.Text & "' where product_name = '" _
                & .TextBox1.Text & "' AND description = '" _
                & .TextBox5.Text & "' AND size = '" _
                & .TextBox3.Text & "' AND price = '" _
                & .TextBox4.Text & "'"

                mycmd.CommandText = strSQL
                mycmd.Connection = myconn
                mycmd.ExecuteNonQuery()
                MsgBox("Record Successfully Updated")
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
End Class