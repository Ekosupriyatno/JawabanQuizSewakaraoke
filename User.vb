Public Class User
    Dim strsql As String
    Dim info As String
    Private _ID As System.Int32
    Private _username As System.String
    Private _nama_lengkap As System.String
    Private _password As System.String
    Public InsertState As Boolean = False
    Public UpdateState As Boolean = False
    Public DeleteState As Boolean = False
    Public Property username()
        Get
            Return _username
        End Get
        Set(ByVal value)
            _username = value
        End Set
    End Property
    Public Property nama_lengkap()
        Get
            Return _nama_lengkap
        End Get
        Set(ByVal value)
            _nama_lengkap = value
        End Set
    End Property
    Public Property password()
        Get
            Return _password
        End Get
        Set(ByVal value)
            _password = value
        End Set
    End Property
    Public Sub Simpan()
        Dim info As String
        DBConnect()
        If (user_baru = True) Then
            strsql = "Insert into Table1(username,nama_lengkap,password) values ('" & _username & "','" & _nama_lengkap & "','" & _password & "')"
            info = "INSERT"
        Else
            strsql = "update Table1 set username='" & _username & "', nama_lengkap='" & _nama_lengkap & "', password='" & _password & "' where username='" & _username & "'"
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
    Public Sub CariTable1(ByVal susername As String)
        DBConnect()
        strsql = "SELECT * FROM Table1 WHERE username='" & susername & "'"
        myCommand.Connection = conn
        myCommand.CommandText = strsql
        DR = myCommand.ExecuteReader
        If (DR.HasRows = True) Then
            user_baru = False
            DR.Read()
            username = Convert.ToString((DR("username")))
            nama_lengkap = Convert.ToString((DR("nama_lengkap")))
            password = Convert.ToString((DR("password")))
        Else
            MessageBox.Show("Data Tidak Ditemukan.")
            user_baru = True
        End If
        DBDisconnect()
    End Sub
    Public Sub Hapus(ByVal susername As String)
        Dim info As String
        DBConnect()
        strsql = "DELETE FROM Table1 WHERE username='" & susername & "'"
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
            strsql = "SELECT * FROM Table1"
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
    Public Function Login(ByVal uname As String, ByVal pwd As String) As Boolean
        Dim pwd_en As String
        pwd_en = getMD5Hash(pwd)
        DBConnect()
        strsql = "SELECT * FROM users WHERE username='" & uname & "' and passwd ='" & pwd_en & "'"

        myCommand.Connection = conn
        myCommand.CommandText = strsql
        DR = myCommand.ExecuteReader
        If (DR.HasRows = True) Then
            login_valid = True
        Else
            login_valid = False
        End If
        DBDisconnect()
        Return login_valid
    End Function
End Class
