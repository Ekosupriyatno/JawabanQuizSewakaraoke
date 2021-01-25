Public Class Booking
    Dim strsql As String
    Dim info As String
    Private _ID As System.Int32
    Private _Namapelanggan As System.String
    Private _Nomorbukti As System.Int32
    Private _Tanggal As System.Int32
    Private _Namaroom As System.String
    Public InsertState As Boolean = False
    Public UpdateState As Boolean = False
    Public DeleteState As Boolean = False
    Public Property Namapelanggan()
        Get
            Return _Namapelanggan
        End Get
        Set(ByVal value)
            _Namapelanggan = value
        End Set
    End Property
    Public Property Nomorbukti()
        Get
            Return _Nomorbukti
        End Get
        Set(ByVal value)
            _Nomorbukti = value
        End Set
    End Property
    Public Property Tanggal()
        Get
            Return _Tanggal
        End Get
        Set(ByVal value)
            _Tanggal = value
        End Set
    End Property
    Public Property Namaroom()
        Get
            Return _Namaroom
        End Get
        Set(ByVal value)
            _Namaroom = value
        End Set
    End Property
    Public Sub Simpan()
        Dim info As String
        DBConnect()
        If (Booking_baru = True) Then
            strsql = "Insert into Booking(Namapelanggan,Nomorbukti,Tanggal,Namaroom) values ('" & _Namapelanggan & "','" & _Nomorbukti & "','" & _Tanggal & "','" & _Namaroom & "')"
            info = "INSERT"
        Else
            strsql = "update Booking set Namapelanggan='" & _Namapelanggan & "', Nomorbukti='" & _Nomorbukti & "', Tanggal='" & _Tanggal & "', Namaroom='" & _Namaroom & "' where Namapelanggan='" & _Namapelanggan & "'"
            info = "UPDATE"
        End If
        myCommand.Connection = conn
        myCommand.CommandText = strsql
        Try
            myCommand.ExecuteNonQuery()
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
    Public Sub CariBooking(ByVal sNamapelanggan As String)
        DBConnect()
        strsql = "SELECT * FROM Booking WHERE Namapelanggan='" & sNamapelanggan & "'"
        myCommand.Connection = conn
        myCommand.CommandText = strsql
        DR = myCommand.ExecuteReader
        If (DR.HasRows = True) Then
            Booking_baru = False
            DR.Read()
            Namapelanggan = Convert.ToString((DR("Namapelanggan")))
            Nomorbukti = Convert.ToString((DR("Nomorbukti")))
            Tanggal = Convert.ToString((DR("Tanggal")))
            Namaroom = Convert.ToString((DR("Namaroom")))
        Else
            MessageBox.Show("Data Tidak Ditemukan.")
            Booking_baru = True
        End If
        DBDisconnect()
    End Sub
    Public Sub Hapus(ByVal sNamapelanggan As String)
        Dim info As String
        DBConnect()
        strsql = "DELETE FROM Booking WHERE Namapelanggan='" & sNamapelanggan & "'"
        info = "DELETE"
        myCommand.Connection = conn
        myCommand.CommandText = strsql
        Try
            myCommand.ExecuteNonQuery()
            DeleteState = True
        Catch ex As Exception
            MessageBox.Show(ex.ToString)
        End Try
        DBDisconnect()
    End Sub
    Public Sub getAllData(ByVal dg As DataGridView)
        Try
            DBConnect()
            strsql = "SELECT * FROM Booking"
            myCommand.Connection = conn
            myCommand.CommandText = strsql
            myData.Clear()
            myAdapter.SelectCommand = myCommand
            myAdapter.Fill(myData)
            With dg
                .DataSource = myData
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
