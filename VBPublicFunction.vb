Imports System.IO
Imports System.IO.Ports

'Source link: D:\Dung\08_dulieu\VBPublicFunction
'Version VBPublicFunction: 00.01
'Data New Change:2021/01/18
'Data New Change:2021/02/24
'From:OngenJIG
'D:\Dung\08_dulieu\VBPublicFunction
'Note: Version: 00.01 :Add [SpaceTime]
'Note: Version: 00.02 :Change GetData & SetData for integer

Public Class VBPublicFunction
    'TEST
    Public Shared Sub showtest(str As String)
        Debug.WriteLine(str)
    End Sub

#Region "/*** 'Other ***/"
    'Calculator space time[Start to End]
    '--------Add Project for uses--------
    'Dim tmr1 As DateTime ' = DateTime.Now
    'Dim tmr2 As DateTime = DateTime.Now
    'Dim tmr3 As DateTime = DateTime.Now
    'Dim tmr4 As DateTime = DateTime.Now
    'Dim tmr5 As DateTime = DateTime.Now
    'Dim tmr6 As DateTime = DateTime.Now
    Public Shared Sub SpaceTime(str As String, ByVal StartGet_Space As Boolean, ByRef time As DateTime)
        Dim tmr1 As DateTime = DateTime.Now
        If StartGet_Space = False Then ' Start get time
            Debug.WriteLine(str & ": " & tmr1)
            time = tmr1
        Else 'Calculater space time
            Debug.WriteLine(str & "TimeSpan: " & DateTime.Now.Subtract(time).ToString)
        End If
    End Sub

    'Get address from block no of NOR Flash(Block size 64k)
    Public Shared Function GetAddrBlock64K(ByVal BlockNo As Integer) As Integer
        '0000 0000h -> 0000 FFFFh: Block No 0
        '0001 0000h -> 0001 FFFFh: Block No 1
        '....
        GetAddrBlock64K = BlockNo << 16
    End Function

#End Region

#Region "/*** 'XỬ LÝ STRING ***/"
    'Chuyển đổi chuỗi thành các ký tự viết hoa.
    'VD:'= HOW ARE YOU?
    Shared Function StrNolmal2LowerCase(str As String) As String
        StrNolmal2LowerCase = StrConv(str, 1)
    End Function

    'Chuyển đổi chuỗi thành các ký tự viết thường.
    'VD:'= how are you?
    Shared Function StrNolmal2UppeCase(str As String) As String
        StrNolmal2UppeCase = StrConv(str, 2)
    End Function

    'Chuyển đổi chữ cái đầu tiên của mỗi từ trong chuỗi thành chữ hoa.
    'VD:'= How Are You?)
    Shared Function StrNolmal2Capitalize(str As String) As String
        StrNolmal2Capitalize = StrConv(str, 3)
    End Function

    Shared Function chkAscTypeHex(strChk As String) As Boolean
        Dim strTypeHex As String = "0123456789ABCDEF"
        chkAscTypeHex = True
        For i = 0 To strChk.Length - 1
            If strTypeHex.Contains(strChk(i)) = False Then
                chkAscTypeHex = False
                Exit Function
            End If
        Next
    End Function

    Shared Function chkStrTypeNumber(strChk As String) As Boolean
        Dim strTypeHex As String = "0123456789"
        chkStrTypeNumber = True
        For i = 0 To strChk.Length - 1
            If strTypeHex.Contains(strChk(i)) = False Then
                chkStrTypeNumber = False
                Exit Function
            End If
        Next
    End Function

    Shared Function chkStrTypeAsc(strChk As String) As Boolean
        Dim strTypeHex As String = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ"
        chkStrTypeAsc = True
        For i = 0 To strChk.Length - 1
            If strTypeHex.Contains(strChk(i)) = False Then
                chkStrTypeAsc = False
                Exit Function
            End If
        Next
    End Function
#End Region

#Region "/*** 'XỬ LÝ DATA ***/"
    'IEEE_754_64bit
    Public Shared Sub IEEE_754_64bit(data() As Byte, addr As Integer, ByRef outdata As Double) 'Format outdata: dddmm.sssss, ddd(+180:-180), mm(00:59), ss(00:59)
        'Data type Little endian
        'b = 1023 for mode 64 bit
        'seeeeeeeeeeemmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmm -> full 64 bit
        'X = (-1)^s x 1.m x 2^(e-b), type data binary
        '1.m = 2^0 + m^(-1) + m^(-2)... to bit 52
        Dim bias As Integer = 1023
        Dim Sign As Byte
        Dim Exponent As Integer
        Dim Mantissa As Double
        Dim i As Integer = 0
        Dim dmy As Byte = 0
        Sign = (data(addr + 7) And &H80) >> 7
        Exponent = data(addr + 7) And &H7F
        Exponent = (Exponent And &H7F) << 4
        Exponent = Exponent Or ((data(addr + 6) And &HF0) >> 4)
        Dim bit() As Byte
        ReDim bit(0 To 51)
        Dim pos As Byte = &H8
        Dim byte_ct As Integer = 6
        For i = 0 To bit.Count - 1
            If (data(addr + byte_ct) And pos) <> 0 Then
                bit(i) = 1
            Else
                bit(i) = 0
            End If
            pos = pos >> 1
            If pos = 0 Then
                pos = &H80
                byte_ct = byte_ct - 1
            End If
        Next
        Mantissa = 1
        For i = 0 To bit.Count - 1
            Mantissa = Mantissa + bit(i) * 2 ^ (-(i + 1))
        Next
        outdata = (-1) ^ Sign * Mantissa * 2 ^ (Exponent - bias)
    End Sub

    'IEEE_754_32bit
    Public Shared Sub IEEE_754_32bit(data As Integer, ByRef outdata As Double) 'Format outdata: dddmm.sssss, ddd(+180:-180), mm(00:59), ss(00:59)
        'Data type Little endian
        'b = 127 for mode 64 bit
        'seeeeeeeeeeemmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmm -> full 64 bit
        'e = 11 bit
        'outdata = (-1)^s x 1.m x 2^(e-b), type data binary
        '1.m = 2^0 + m^(-1) + m^(-2)... to bit 52
        Dim bias As Integer = 127
        Dim Sign As Byte
        Dim Exponent As Integer
        Dim Mantissa As Double
        Dim i As Integer = 0
        Sign = IIf(data And &H80000000, 1, 0)
        Exponent = (data And &H7F800000) >> 23
        Dim bit() As Byte
        ReDim bit(0 To 22)
        Dim pos As Integer = &H400000
        For i = 0 To bit.Count - 1
            If (data And pos) <> 0 Then
                bit(i) = 1
            Else
                bit(i) = 0
            End If
            pos = pos >> 1
        Next
        Mantissa = 1
        For i = 0 To bit.Count - 1
            Mantissa = Mantissa + bit(i) * 2 ^ (-(i + 1))
        Next
        outdata = (-1) ^ Sign * Mantissa * 2 ^ (Exponent - bias)
    End Sub

    Public Shared Function buffbyte2String(buff() As Byte, bytelen As Integer) As String
        Dim strDmy As String = ""
        For i = 0 To bytelen - 1
            strDmy = strDmy & CHex(buff(i), 2)
        Next
        buffbyte2String = strDmy
    End Function

    Public Shared Function String2buffbyte(strData As String, buff() As Byte, bytelen As Integer) As Boolean
        On Error GoTo ERROR_PROC
        Dim strDmy As String = ""
        For i = 0 To bytelen - 1
            buff(i) = CInt("&H" & strData.Substring(i * 2, 2))
        Next
        String2buffbyte = True
        Exit Function
ERROR_PROC:
        String2buffbyte = False
        Exit Function
    End Function

    'CHUYỂN ĐỔI DEC -> HEX NHẬP SỐ LƯỢNG PHẦN TỪ BỔ SUNG len
    Public Shared Function CHex(data As Integer, len As Byte) As String
        CHex = Hex(data)
        While CHex.Length < len
            CHex = "0" & CHex
        End While
    End Function

    'CHUYỂN ĐỔI DEC -> HEX  giới hạn byte
    Public Shared Function CHex(data As Integer) As String
        CHex = Hex(data)
        While CHex.Length < 2
            CHex = "0" & CHex
        End While
    End Function

    'Format string
    Public Shared Function FormatStrAdd0(Data As Integer, len As Integer) As String
        FormatStrAdd0 = Data.ToString
        While FormatStrAdd0.Length < len
            FormatStrAdd0 = "0" & FormatStrAdd0
        End While
    End Function

    Public Shared Function FormatStrAdd0(Data As String, len As Integer) As String
        FormatStrAdd0 = Data
        While FormatStrAdd0.Length < len
            FormatStrAdd0 = "0" & FormatStrAdd0
        End While
    End Function

    ' Add Right
    Public Shared Function FormatStrAdd0Right(Data As String, len As Integer) As String
        FormatStrAdd0Right = Data
        While FormatStrAdd0Right.Length < len
            FormatStrAdd0Right = FormatStrAdd0Right & "0"
        End While
    End Function

    'Format to string
    Public Shared Function FormatStrAddSpace(Data As Integer, len As Integer) As String
        FormatStrAddSpace = Data.ToString
        While FormatStrAddSpace.Length < len
            FormatStrAddSpace = " " & FormatStrAddSpace
        End While
    End Function

    Public Shared Function FormatStrAddSpace(Data As String, len As Integer) As String
        FormatStrAddSpace = Data
        While FormatStrAddSpace.Length < len
            FormatStrAddSpace = " " & FormatStrAddSpace
        End While
    End Function

    Public Shared Function FormatStrAddSpace(Data As String, len As Integer, add_Right As Boolean) As String
        FormatStrAddSpace = Data
        While FormatStrAddSpace.Length < len
            If add_Right = True Then
                FormatStrAddSpace = FormatStrAddSpace & " "
            Else
                FormatStrAddSpace = " " & FormatStrAddSpace
            End If
        End While
    End Function

    ''' <summary>
    ''' /*** Decimal to BCD converting ***/
    ''' </summary>
    Public Shared Function Conv2Bcd(ByVal Data As Integer) As Integer
        Dim intDmy As Integer = 0

        intDmy = Data Mod 100
        intDmy = ((intDmy - (intDmy Mod 10)) / 10) * 16 + (intDmy Mod 10)

        Conv2Bcd = intDmy
    End Function

    ''' <summary>
    ''' /*** ASCII heximal to decimal converting ***/
    ''' </summary>
    Public Shared Function Clc_asc2bin(ByVal strData As String, ByRef Data As Integer) As Boolean

        Dim strDmy As String = ""
        Dim intDmy As Integer = 0

        strDmy = Mid(strData, 1, 1)
        intDmy = AscW(strDmy)
        If intDmy < &H30 Then
            GoTo ERROR_END
        ElseIf intDmy < &H3A Then
        ElseIf intDmy < &H41 Then
            GoTo ERROR_END
        ElseIf intDmy < &H47 Then
        Else
            GoTo ERROR_END
        End If

        strDmy = Mid(strData, 2, 1)
        intDmy = AscW(strDmy)
        If intDmy < &H30 Then
            GoTo ERROR_END
        ElseIf intDmy < &H3A Then
        ElseIf intDmy < &H41 Then
            GoTo ERROR_END
        ElseIf intDmy < &H47 Then
        Else
            GoTo ERROR_END
        End If

        Data = Val("&H" & strData)

        Return True
        Exit Function

ERROR_END:
        Return False
        Exit Function

    End Function

    Public Shared Function Clc_asc2binV2(ByVal strData As String, ByRef buff() As Byte, intLenByte As Integer) As Boolean

        Dim strDmy As String = ""
        Dim intDmy As Integer = 0

        For i = 0 To intLenByte - 1
            strDmy = Mid(strData, i * 2 + 1, 2)
            If Clc_asc2bin(strDmy, intDmy) = False Then
                GoTo ERROR_END
            End If
            buff(i) = intDmy And &HFF
        Next

        Return True
        Exit Function
ERROR_END:
        Return False
        Exit Function

    End Function

    'FORMAT SỐ LƯỢNG KÝ TỰ STRING SỐ LƯỢNG LEN, GIÁ TRỊ THÊM VÀO '0'
    Public Shared Function Fmat(data As String, len As Byte) As String
        Fmat = data
        While Fmat.Length < len
            Fmat = "0" & Fmat
        End While
    End Function

    'FORMAT SỐ LƯỢNG KÝ TỰ STRING SỐ LƯỢNG 2,GIÁ TRỊ THÊM VÀO '0'
    Public Shared Function Fmat(data As String) As String
        Fmat = data
        While Fmat.Length < 2
            Fmat = "0" & Fmat
        End While
    End Function

    'FORMAT SỐ LƯỢNG KÝ TỰ STRING SỐ LƯỢNG 2, GIÁ TRỊ THÊM VÀO LÀ KHOẢNG TRỐNG
    Public Shared Function Fmat2(data As String) As String
        Fmat2 = data
        While Fmat2.Length < 2
            Fmat2 = " " & Fmat2
        End While
    End Function

    'FORMAT SỐ LƯỢNG KÝ TỰ STRING SỐ LƯỢNG LEN, GIÁ TRỊ THÊM VÀO LÀ KHOẢNG TRỐNG
    Public Shared Function Fmat2(data As String, len As Integer) As String
        Fmat2 = data
        While Fmat2.Length < len
            Fmat2 = " " & Fmat2
        End While
        If Fmat2.Length > len Then
            Fmat2 = Mid(Fmat2, 1, len)
        End If
    End Function
	
	'Full string
	'ex: [data1						]
	'    [data2 					]
	Public Shared Function FormatTextRight(data As String, len As Integer) As String
        While data.Length < len
            data = data & " "
        End While
        FormatTextRight = data
    End Function

    'Set value for "ref" = Buff(intPos) && intPos++
    Public Shared Sub SetData(ByRef ref As Byte, ByRef Buff() As Byte, ByRef intPos As Integer)
        ref = Buff(intPos)
        intPos = intPos + 1
    End Sub

    'Set Data type integer
    Public Shared Sub SetData(ByRef ref As Integer, ByRef Buff() As Byte, ByRef intPos As Integer)
        'ref = 0
        'ref = ref Or (Buff(intPos) And &HFF) << 24
        'intPos = intPos + 1
        'ref = ref Or (Buff(intPos) And &HFF) << 16
        'intPos = intPos + 1
        'ref = ref Or (Buff(intPos) And &HFF) << 8
        'intPos = intPos + 1
        'ref = ref Or (Buff(intPos) And &HFF) << 0
        'intPos = intPos + 1
        'Dim pos As Integer = 0
        'pos = 3
        'ref = Buff(intPos + pos)
        'ref = ref << 8
        'intPos = intPos + 1
        'pos = pos - 1
        'ref = ref Or Buff(intPos + pos)
        'ref = ref << 8
        'intPos = intPos + 1
        'pos = pos - 1
        'ref = ref Or Buff(intPos + pos)
        'ref = ref << 8
        'intPos = intPos + 1
        'pos = pos - 1
        'ref = ref Or Buff(intPos + pos)
        'ref = ref << 8
        'intPos = intPos + 1
        'pos = pos - 1
        Dim Buff2(0 To 3) As Byte
        Dim i As Integer = 0
        For i = 0 To Buff2.Count - 1
            Buff2(i) = Buff(intPos)
            intPos = intPos + 1
        Next
        ref = 0
        For i = 0 To Buff2.Count - 1
            ref = ref << 8
            ref = ref Or Buff2(3 - i)
        Next

    End Sub

#If 1 Then
    'Set value for "ref(i)" = Buff(intPos) && intPos++
    Public Shared Sub SetData(ByRef ref() As Byte, ByRef Buff() As Byte, ByRef intPos As Integer, ByRef len As Integer)
        For i = 0 To len - 1
            ref(i) = Buff(intPos)
            intPos = intPos + 1
        Next
    End Sub

    'Set value for "ref(i)" = Buff(intPos) && intPos++
    Public Shared Sub SetData(ByRef ref() As Byte, ByRef Buff() As Byte, ByRef intPos As Integer)
        For i = 0 To ref.Count - 1
            ref(i) = Buff(intPos)
            intPos = intPos + 1
        Next
    End Sub
#End If
#If 1 Then
    'Set value for "ref(i,j)" = Buff(intPos) && intPos++
    Public Shared Sub SetData(ByRef ref(,) As Byte, ByRef Buff() As Byte, ByRef intPos As Integer, ByRef len1 As Integer, ByRef len2 As Integer)
        For i = 0 To len1 - 1
            For j = 0 To len2 - 1
                ref(i, j) = Buff(intPos)
                intPos = intPos + 1
            Next
        Next
    End Sub

    'Set value for "ref(i,j)" = Buff(intPos) && intPos++
    Public Shared Sub SetData(ByRef ref(,) As Byte, ByRef Buff() As Byte, ByRef intPos As Integer)
        For i = 0 To ref.GetLength(0) - 1
            For j = 0 To ref.GetLength(1) - 1
                ref(i, j) = Buff(intPos)
                intPos = intPos + 1
            Next
        Next
    End Sub
#End If


    'Set value for "ref(i,j)" = Buff(intPos) && intPos++ ' [Integer]
    Public Shared Sub SetData(ByRef ref(,) As Integer, ByRef Buff() As Byte, ByRef intPos As Integer, ByRef len1 As Integer, ByRef len2 As Integer)
        Dim pos As Integer = 0
        For i = 0 To len1 - 1
            For j = 0 To len2 - 1
                'ref(i, j) = 0
                'ref(i, j) = ref(i, j) Or ((Buff(intPos + 0) And &HFF) << 24)
                'intPos = intPos + 1
                'ref(i, j) = ref(i, j) Or ((Buff(intPos + 1) And &HFF) << 16)
                'intPos = intPos + 1
                'ref(i, j) = ref(i, j) Or ((Buff(intPos + 2) And &HFF) << 8)
                'intPos = intPos + 1
                'ref(i, j) = ref(i, j) Or ((Buff(intPos + 3) And &HFF) << 0)
                'intPos = intPos + 1
                'pos = 3
                'ref(i, j) = Buff(intPos + pos)
                'ref(i, j) = ref(i, j) << 8
                'intPos = intPos + 1
                'pos = pos - 1
                'ref(i, j) = ref(i, j) Or Buff(intPos + pos)
                'ref(i, j) = ref(i, j) << 8
                'intPos = intPos + 1
                'pos = pos - 1
                'ref(i, j) = ref(i, j) Or Buff(intPos + pos)
                'ref(i, j) = ref(i, j) << 8
                'intPos = intPos + 1
                'pos = pos - 1
                'ref(i, j) = ref(i, j) Or Buff(intPos + pos)
                'ref(i, j) = ref(i, j) << 8
                'intPos = intPos + 1
                'pos = pos - 1
                Dim Buff2(0 To 3) As Byte
                Dim k As Integer = 0
                For k = 0 To Buff2.Count - 1
                    Buff2(k) = Buff(intPos)
                    intPos = intPos + 1
                Next
                ref(i, j) = 0
                For k = 0 To Buff2.Count - 1
                    ref(i, j) = ref(i, j) << 8
                    ref(i, j) = ref(i, j) Or Buff2(3 - k)
                Next
            Next
        Next
    End Sub

    'Set value for Buff(intPos) = "ref" && intPos++
    Public Shared Sub GetData(ByRef ref As Byte, ByRef Buff() As Byte, ByRef intPos As Integer)
        Buff(intPos) = ref
        intPos = intPos + 1
    End Sub

    'Get data Type integer
    Public Shared Sub GetData(ByRef ref As Integer, ByRef Buff() As Byte, ByRef intPos As Integer)
        Buff(intPos) = (ref >> 24) And &HFF& '000000) >> 24
        intPos = intPos + 1
        Buff(intPos) = (ref >> 16) And &HFF& '(ref And &HFF0000) >> 16
        intPos = intPos + 1
        Buff(intPos) = (ref >> 8) And &HFF& '(ref And &HFF00) >> 8
        intPos = intPos + 1
        Buff(intPos) = ref And &HFF& '(ref And &HFF) >> 0
        intPos = intPos + 1
    End Sub

#If 1 Then
    'Set value for Buff(intPos) = "ref(i)" && intPos++
    Public Shared Sub GetData(ByRef ref() As Byte, ByRef Buff() As Byte, ByRef intPos As Integer, ByRef len As Integer)
        For i = 0 To len - 1
            Buff(intPos) = ref(i)
            intPos = intPos + 1
        Next
    End Sub

    'Set value for Buff(intPos) = "ref(i)" && intPos++
    Public Shared Sub GetData(ByRef ref() As Byte, ByRef Buff() As Byte, ByRef intPos As Integer)
        For i = 0 To ref.Count - 1
            Buff(intPos) = ref(i)
            intPos = intPos + 1
        Next
    End Sub
#End If

#If 1 Then
    'Set value for Buff(intPos) = "ref(i,j)" && intPos++
    Public Shared Sub GetData(ByRef ref(,) As Byte, ByRef Buff() As Byte, ByRef intPos As Integer, ByRef len1 As Integer, ByRef len2 As Integer)
        For i = 0 To len1 - 1
            For j = 0 To len2 - 1
                Buff(intPos) = ref(i, j)
                intPos = intPos + 1
            Next
        Next
    End Sub

    'Set value for Buff(intPos) = "ref(i,j)" && intPos++
    Public Shared Sub GetData(ByRef ref(,) As Byte, ByRef Buff() As Byte, ByRef intPos As Integer)
        For i = 0 To ref.GetLength(0) - 1
            For j = 0 To ref.GetLength(1) - 1
                Buff(intPos) = ref(i, j)
                intPos = intPos + 1
            Next
        Next
    End Sub
#End If

    'Set value for Buff(intPos) = "ref(i,j)" && intPos++ ' [Integer]
    Public Shared Sub GetData(ByRef ref(,) As Integer, ByRef Buff() As Byte, ByRef intPos As Integer, ByRef len1 As Integer, ByRef len2 As Integer)
        For i = 0 To len1 - 1
            For j = 0 To len2 - 1
                Buff(intPos) = (ref(i, j) >> 24) And &HFF& '(ref(i, j) And &HFF000000) >> 24
                intPos = intPos + 1
                Buff(intPos) = (ref(i, j) >> 16) And &HFF& '(ref(i, j) And &HFF0000) >> 16
                intPos = intPos + 1
                Buff(intPos) = (ref(i, j) >> 8) And &HFF& '(ref(i, j) And &HFF00) >> 8
                intPos = intPos + 1
                Buff(intPos) = ref(i, j) And &HFF& '(ref(i, j) And &HFF) >> 0
                intPos = intPos + 1
            Next
        Next
    End Sub

    'set buff value
    Public Shared Sub MemSet(ByRef buff() As Byte, ByVal value As Byte, len As Integer)
        For i = 0 To len - 1
            buff(i) = value
        Next
    End Sub

    Public Shared Sub MemCopy(ByVal s_buff() As Byte, ByRef d_buff2() As Byte, len As Integer)
        For i = 0 To len - 1
            d_buff2(i) = s_buff(i)
        Next
    End Sub

    Public Shared Sub MemCopy(ByVal s_buff() As Byte, ByVal start_pos As Integer, ByRef d_buff2() As Byte, len As Integer)
        For i = 0 To len - 1
            d_buff2(i) = s_buff(i + start_pos)
        Next
    End Sub

    Public Shared Sub MemCopy(ByVal s_buff() As Byte, ByRef d_buff2() As Byte, ByVal start_pos As Integer, len As Integer)
        For i = 0 To len - 1
            d_buff2(i + start_pos) = s_buff(i)
        Next
    End Sub

    'Init lable
    Public Shared Sub Init_lbl(ByRef lbl() As Label, len1 As Integer)
        ReDim lbl(0 To len1 - 1)
        For i = 0 To len1 - 1
            lbl(i) = New Label()
        Next
    End Sub

    'Init checkbox aray 
    Public Sub Init_chk(ByRef chk() As CheckBox, len1 As Integer)
        ReDim chk(0 To len1 - 1)
        For i = 0 To len1 - 1
            chk(i) = New CheckBox()
        Next
    End Sub

    'Init checkbox aray 
    Public Sub Init_chk(ByRef chk(,,) As CheckBox, len1 As Integer, len2 As Integer, len3 As Integer)
        ReDim chk(0 To len1 - 1, 0 To len2 - 1, 0 To len3 - 1)
        For i = 0 To len1 - 1
            For j = 0 To len2 - 1
                For k = 0 To len3 - 1
                    chk(i, j, k) = New CheckBox()
                Next
            Next
        Next
    End Sub

    'Init checkbox aray 
    Public Sub Init_chk(ByRef chk(,) As CheckBox, len1 As Integer, len2 As Integer)
        ReDim chk(0 To len1 - 1, 0 To len2 - 1)
        For i = 0 To len1 - 1
            For j = 0 To len2 - 1
                chk(i, j) = New CheckBox()
            Next
        Next
    End Sub

    'Init ComboBox aray 
    Public Sub Init_cmb(ByRef cmb() As ComboBox, len1 As Integer)
        ReDim cmb(0 To len1 - 1)
        For i = 0 To len1 - 1
            cmb(i) = New ComboBox()
        Next
    End Sub

    'Init button
    Public Sub Init_btn(ByRef btn() As Button, len1 As Integer)
        ReDim btn(0 To len1 - 1)
        For i = 0 To len1 - 1
            btn(i) = New Button()
        Next
    End Sub

    'Init group
    Public Sub Init_grp(ByRef ref() As GroupBox, len1 As Integer)
        ReDim ref(0 To len1 - 1)
        For i = 0 To len1 - 1
            ref(i) = New GroupBox()
        Next
    End Sub

    'BIN TO BDC
    Private Function BIN2BCD(ByVal Data As Byte) As Byte
        Dim intDmy As Integer = 0
        intDmy = Data Mod 100 '00 -> 99
        intDmy = (intDmy \ 10) * 16 + (intDmy Mod 10)
        BIN2BCD = intDmy
    End Function

    'BCD to BIN
    Private Function BCD2BIN(ByVal Data As Byte) As Byte
        Dim intDmy As Integer = 0
        intDmy = Data And &HF0 '0x00 -> 0x99
        intDmy = intDmy >> 4
        intDmy = intDmy * 10
        intDmy = intDmy + (Data And &HF)
        BCD2BIN = intDmy
    End Function
#End Region

#Region "/*** 'Show lable/text safe ***/"
    ''' <summary>
    ''' /*** ラベルに文字列を設定します。***/
    ''' </summary>
    Private Delegate Sub D_UpdateLabelText(ByVal label As Label, ByVal text As String)
    Public Sub SetLabelText(label As Label, text As String)

        System.Threading.Thread.Sleep(1)
        label.Invoke(
            New D_UpdateLabelText(AddressOf UpdateLabelText), label, text)

    End Sub

    Public Sub UpdateLabelText(ByVal label As Label, ByVal text As String)
        label.Text = text
    End Sub

    ''' <summary>
    ''' /*** TextBoxに文字列を設定します。***/
    ''' </summary>
    Public Delegate Sub D_UpdateTextBoxText(ByVal Textbox As TextBox, ByVal text As String)
    Public Sub SetTextBoxText(Textbox As TextBox, text As String)

        System.Threading.Thread.Sleep(1)
        Textbox.Invoke(
            New D_UpdateTextBoxText(AddressOf UpdateTextBoxText), Textbox, text)

    End Sub

    Public Sub UpdateTextBoxText(ByVal Textbox As TextBox, ByVal text As String)
        Textbox.Text = ""
        Textbox.SelectionStart = Textbox.Text.Length
        Textbox.SelectedText = text
    End Sub

#End Region

#Region "/*** 'Media ***/"
    'Command with Ffmpeg
    Public Shared Sub cmdFfmpeg(arg As String)
        Dim startinfo As New System.Diagnostics.ProcessStartInfo
        Dim proc As New Process
        Dim _ffmpeg As String = My.Application.Info.DirectoryPath & "\Resources\ffmpeg.exe"
        ' all parameters required to run the process
        startinfo.FileName = _ffmpeg
        'startinfo.FileName = strFfmpegPath
        startinfo.Arguments = arg
        startinfo.UseShellExecute = False
        startinfo.WindowStyle = ProcessWindowStyle.Hidden
        startinfo.RedirectStandardError = True
        startinfo.RedirectStandardOutput = True
        startinfo.CreateNoWindow = True
        proc.StartInfo = startinfo
        proc.Start() ' start the process
        proc.WaitForExit()

    End Sub

    Public Sub cmdFfplay(arg As String)
        'Debug.WriteLine(strFfplayPath)
        Dim startinfo As New System.Diagnostics.ProcessStartInfo
        Dim proc As New Process
        Dim _ffplay As String = My.Application.Info.DirectoryPath & "\Resources\ffplay.exe"
        ' all parameters required to run the process
        'startinfo.FileName = strFfplayPath
        startinfo.FileName = _ffplay
        startinfo.Arguments = arg
        startinfo.UseShellExecute = False
        startinfo.WindowStyle = ProcessWindowStyle.Hidden
        startinfo.RedirectStandardError = True
        startinfo.RedirectStandardOutput = True
        startinfo.CreateNoWindow = True
        proc.StartInfo = startinfo
        proc.Start() ' start the process
        'proc.Start("cmd.exe", "/k " & strFfplayPath & " C:\Number.wav")

        Dim sr As StreamReader
        Dim ffmpegOutput As String
        sr = proc.StandardError 'standard error is used by ffmpeg

        Do
            ffmpegOutput = sr.ReadLine
            Debug.WriteLine(ffmpegOutput)
        Loop Until proc.HasExited And ffmpegOutput = Nothing Or ffmpegOutput = ""
    End Sub

    '
    Public Sub cmdFfprobe(arg As String)
        Dim startinfo As New System.Diagnostics.ProcessStartInfo
        Dim proc As New Process
        ' all parameters required to run the process
        Dim _ffprobe As String = My.Application.Info.DirectoryPath & "\Resources\ffprobe.exe"
        'startinfo.FileName = strFfprobePath
        startinfo.FileName = _ffprobe
        startinfo.Arguments = arg
        startinfo.UseShellExecute = False
        startinfo.WindowStyle = ProcessWindowStyle.Hidden
        startinfo.RedirectStandardError = True
        startinfo.RedirectStandardOutput = True
        startinfo.CreateNoWindow = True
        proc.StartInfo = startinfo
        proc.Start() ' start the process
    End Sub


#End Region

End Class