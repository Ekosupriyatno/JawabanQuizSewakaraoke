Public Class cFood
    Dim strsql As String
    Dim info As String
    Private _ID As System.Int32
    Private _Jenis_Makanan As System.String
    Private _Paket_Makan As System.String
    Private _Harga_Makanan As System.String
    Public InsertState As Boolean = False
    Public UpdateState As Boolean = False
    Public DeleteState As Boolean = False
    Public Property Jenis_Makanan()
        Get
            Return _Jenis_Makanan
        End Get
        Set(ByVal value)
            _Jenis_Makanan = value
        End Set
    End Property
    Public Property Paket_Makan()
        Get
            Return _Paket_Makan
        End Get
        Set(ByVal value)
            _Paket_Makan = value
        End Set
    End Property
    Public Property Harga_Makanan()
        Get
            Return _Harga_Makanan
        End Get
        Set(ByVal value)
            _Harga_Makanan = value
        End Set
    End Property
    Public Sub Simpan()
        Dim info As String
        DBConnect()
        If (Food_baru = True) Then
            strsql = "Insert into Food(Jenis_Makanan,Paket_Makan,Harga_Makanan) values ('" & _Jenis_Makanan & "','" & _Paket_Makan & "','" & _Harga_Makanan & "')"
            info = "INSERT"
        Else
            strsql = "update Food set Jenis_Makanan='" & _Jenis_Makanan & "', Paket_Makan='" & _Paket_Makan & "', Harga_Makanan='" & _Harga_Makanan & "' "
            info = "UPDATE"
        End If
        mycommand.Connection = conn
        mycommand.CommandText = strsql
        Try
            mycommand.ExecuteNonQuery()
        Catch ex As Exception
            If (info = "INSERT") Then
                InsertState = False
            ElseIf (info = "UPDATE") Then
                UpdateState = False
            Else
            End If
        Finally
            If (info = "INSERT") Then
                InsertState = True
            ElseIf (info = "UPDATE") Then
                UpdateState = True
            Else
            End If
        End Try
        DBDisconnect()
    End Sub
    Public Sub CariFood(ByVal smilihlagu As String)
        DBConnect()
        strsql = "SELECT * FROM Food WHERE milihlagu='" & smilihlagu & "'"
        mycommand.Connection = conn
        mycommand.CommandText = strsql
        DR = mycommand.ExecuteReader
        If (DR.HasRows = True) Then
            Food_baru = False
            DR.Read()
            Jenis_Makanan = Convert.ToString((DR("Jenis_Makanan")))
            Paket_Makan = Convert.ToString((DR("Paket_Makan")))
            Harga_Makanan = Convert.ToString((DR("Harga_Makanan")))
        Else
            MessageBox.Show("Data Tidak Ditemukan.")
            Food_baru = True
        End If
        DBDisconnect()
    End Sub
    Public Sub Hapus(ByVal smilihlagu As String)
        Dim info As String
        DBConnect()
        strsql = "DELETE FROM Food WHERE milihlagu='" & smilihlagu & "'"
        info = "DELETE"
        mycommand.Connection = conn
        mycommand.CommandText = strsql
        Try
            mycommand.ExecuteNonQuery()
            DeleteState = True
        Catch ex As Exception
            MessageBox.Show(ex.ToString)
        End Try
        DBDisconnect()
    End Sub
    Public Sub getAllData(ByVal dg As DataGridView)
        Try
            DBConnect()
            strsql = "SELECT * FROM Food"
            mycommand.Connection = conn
            mycommand.CommandText = strsql
            mydata.Clear()
            myadapter.SelectCommand = mycommand
            myadapter.Fill(mydata)
            With dg
                .DataSource = mydata
                .AllowUserToAddRows = False
                .AllowUserToDeleteRows = False
                .ReadOnly = True
            End With
        Catch ex As Exception
            MessageBox.Show(ex.ToString)
        Finally
            DBDisconnect()
        End Try
    End Sub

End Class
