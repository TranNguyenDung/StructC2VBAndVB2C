
Public Class Form1
    Private Sub VB2C_Click(sender As Object, e As EventArgs) Handles VB2C.Click
        'Dim ledPwm As Byte
        'Dim ledPwm2 As Byte
        'Dim aircloneId As Byte
        'Dim volGuide As Byte
        'Dim setteiId() As Byte
        'Dim tokuExtend As Byte
        'Dim txLimitOff As Byte

        'unsigned Char lo_pwr;
        'unsigned Char group;
        'unsigned Char code;
        'unsigned Char comp;
        'unsigned Char vox;
        'unsigned Char mic_gain;
        'unsigned Char guide_vol;
        'unsigned Char rec;
        Dim strDmy As String
        strDmy = CODE_VB.Text
        strDmy = strDmy.Replace("Dim", "unsigned char")
        strDmy = strDmy.Replace(" As Byte", ";")

        CODE_C.Text = strDmy
        'Debug.WriteLine(CODE_C.Text)
    End Sub

    Private Sub C2VB_Click(sender As Object, e As EventArgs) Handles C2VB.Click
        Dim strDmy As String
        strDmy = CODE_C.Text
        Dim strData() As String = strDmy.Split(vbCrLf)
        strDmy = ""
        For i = 0 To strData.Length - 1
            strData(i) = strData(i).Replace(vbLf, "").Replace(vbTab, "")
            If (strData(i) <> "") And (strData(i).StartsWith("//") = False) Then
                ' Erase char(//) to enter
                strData(i) = strData(i).Substring(0, strData(i).IndexOf(";"))
                Debug.WriteLine(strData(i))
                ' Process
                If strData(i).Contains("struct") = False Then
                    If (strData(i).Contains("[") = True) And (strData(i).Contains("]") = True) And (strData(i).Split("[").Length = 2) Then
                        'Get Number Init
                        Dim StrLen As String = strData(i).Split("[")(1).Trim
                        StrLen = StrLen.Split("]")(0).Trim

                        strData(i) = strData(i).Split("[")(0).Replace("unsigned char ", "").Trim
                        strData(i) = "Dim " & strData(i).Trim & "() as byte"

                    ElseIf (strData(i).Contains("[") = False) And (strData(i).Contains("]") = False) Then
                        strData(i) = strData(i).Replace("unsigned char ", "").Trim
                        strData(i) = "Dim " & strData(i).Trim & " as byte"
                    Else
                        strData(i) = strData(i) & "<<< Not fix"
                    End If
                Else
                    If strData(i).Contains("[") = False Then
                        Dim tmp() As String = strData(i).Split(" ")
                        strData(i) = "Dim " & tmp(2).Trim & " as " & tmp(1).ToUpper
                    Else
                        If strData(i).Split("[").Length = 2 Then
                            Dim tmp() As String = strData(i).Split(" ")
                            tmp(2) = tmp(2).Substring(0, tmp(2).IndexOf("["))
                            strData(i) = "Dim " & tmp(2).Trim & "() as " & tmp(1).ToUpper
                        Else
                            strData(i) = strData(i) & "<<< Not fix"
                        End If
                    End If
                End If
                    ' Erase space
                    strDmy = strDmy & strData(i).Trim & vbCrLf
            End If

        Next
        CODE_VB.Text = strDmy
    End Sub

    Private Sub btnRedim_Click(sender As Object, e As EventArgs) Handles btnRedim.Click
        Dim strDmy As String
        strDmy = CODE_C.Text
        Dim strData() As String = strDmy.Split(vbCrLf)
        strDmy = ""
        For i = 0 To strData.Length - 1
            strData(i) = strData(i).Replace(vbLf, "").Replace(vbTab, "")
            If (strData(i) <> "") And (strData(i).StartsWith("//") = False) Then
                ' Erase char(//) to enter
                strData(i) = strData(i).Substring(0, strData(i).IndexOf(";"))
                Debug.WriteLine(strData(i))
                ' Process
                If strData(i).Contains("struct") = False Then
                    If (strData(i).Contains("[") = True) And (strData(i).Contains("]") = True) And (strData(i).Split("[").Length = 2) Then
                        'Get Number Init
                        Dim StrLen As String = strData(i).Split("[")(1).Trim
                        StrLen = StrLen.Split("]")(0).Trim

                        strData(i) = strData(i).Split("[")(0).Replace("unsigned char ", "").Trim
                        strData(i) = "Redim " & strData(i) & "(0 to " & (CInt(StrLen) - 1).ToString & ")"
                        ' Erase space
                        strDmy = strDmy & strData(i).Trim & vbCrLf
                    End If
                Else
                    If strData(i).Contains("[") = False Then
                        Dim tmp() As String = strData(i).Split(" ")
                        strData(i) = tmp(2).Trim & ".InitST()"
                    Else
                        If strData(i).Split("[").Length = 2 Then
                            'Get name
                            Dim name As String = strData(i).Split(" ")(2)
                            name = name.Substring(0, name.IndexOf("["))
                            'Get len
                            Dim strlen As String = strData(i).Split("[")(1)
                            strlen = strlen.Split("]")(0)
                            strlen = (CInt(strlen) - 1).ToString

                            strData(i) = "ReDim " & name & "(0 to " & strlen & ")" & vbCrLf
                            strData(i) = strData(i) & "For i = 0 To " & name & ".Count - 1" & vbCrLf
                            strData(i) = strData(i) & "     " & name & "(i).InitST()" & vbCrLf
                            strData(i) = strData(i) & "Next" & vbCrLf
                        Else
                            strData(i) = strData(i) & "<<< Not fix"
                        End If
                    End If
                    ' Erase space
                    strDmy = strDmy & strData(i).Trim & vbCrLf
                End If
            End If
        Next
        CODE_VB.Text = strDmy
    End Sub

    Private Sub SetData_Click(sender As Object, e As EventArgs) Handles btnSetData.Click
        Dim strDmy As String
        strDmy = CODE_C.Text
        Dim strData() As String = strDmy.Split(vbCrLf)
        strDmy = ""
        For i = 0 To strData.Length - 1
            strData(i) = strData(i).Replace(vbLf, "").Replace(vbTab, "")
            If (strData(i) <> "") And (strData(i).StartsWith("//") = False) Then
                ' Erase char(//) to enter
                strData(i) = strData(i).Substring(0, strData(i).IndexOf(";"))
                Debug.WriteLine(strData(i))

                ' Process
                If strData(i).Contains("struct") = False Then
                    If (strData(i).Contains("[") = True) And (strData(i).Contains("]") = True) And (strData(i).Split("[").Length = 2) Then
                        strData(i) = strData(i).Split("[")(0).Replace("unsigned char ", "").Trim
                        'strData(i) = "SetData(" & strData(i) & ", Buff, intPos, " & strData(i) & ".length)"
                        strData(i) = "SetData(" & strData(i) & ", Buff, intPos)"
                    ElseIf (strData(i).Contains("[") = False) And (strData(i).Contains("]") = False) Then
                        strData(i) = strData(i).Replace("unsigned char ", "").Trim
                        strData(i) = "SetData(" & strData(i) & ", Buff, intPos)"
                    Else
                        strData(i) = strData(i) & "<<< Not fix"
                    End If
                Else
                    If strData(i).Contains("[") = False Then
                        Dim tmp() As String = strData(i).Split(" ")
                        strData(i) = tmp(2).Trim & ".SetST(Buff, intPos)"
                    Else
                        If strData(i).Split("[").Length = 2 Then
                            'Get name
                            Dim name As String = strData(i).Split(" ")(2)
                            name = name.Substring(0, name.IndexOf("["))
                            strData(i) = ""
                            strData(i) = strData(i) & "For i = 0 To " & name & ".Count - 1" & vbCrLf
                            strData(i) = strData(i) & "     " & name.Trim & "(i).SetST(Buff, intPos)" & vbCrLf
                            strData(i) = strData(i) & "Next" & vbCrLf
                        Else
                            strData(i) = strData(i) & "<<< Not fix"
                        End If
                    End If
                End If

                ' Erase space
                strDmy = strDmy & strData(i).Trim & vbCrLf
            End If
        Next
        CODE_VB.Text = strDmy
    End Sub

    Private Sub GetData_Click(sender As Object, e As EventArgs) Handles btnGetData.Click
        Dim strDmy As String
        strDmy = CODE_C.Text
        Dim strData() As String = strDmy.Split(vbCrLf)
        strDmy = ""
        For i = 0 To strData.Length - 1
            strData(i) = strData(i).Replace(vbLf, "").Replace(vbTab, "")
            If (strData(i) <> "") And (strData(i).StartsWith("//") = False) Then
                ' Erase char(//) to enter
                strData(i) = strData(i).Substring(0, strData(i).IndexOf(";"))
                Debug.WriteLine(strData(i))

                ' Process
                If strData(i).Contains("struct") = False Then
                    If (strData(i).Contains("[") = True) And (strData(i).Contains("]") = True) And (strData(i).Split("[").Length = 2) Then
                        strData(i) = strData(i).Split("[")(0).Replace("unsigned char ", "").Trim
                        'strData(i) = "GetData(" & strData(i) & ", Buff, intPos, " & strData(i) & ".length)"
                        strData(i) = "GetData(" & strData(i) & ", Buff, intPos)"
                    ElseIf (strData(i).Contains("[") = False) And (strData(i).Contains("]") = False) Then
                        strData(i) = strData(i).Replace("unsigned char ", "").Trim
                        strData(i) = "GetData(" & strData(i) & ", Buff, intPos)"
                    Else
                        strData(i) = strData(i) & "<<< Not fix"
                    End If
                Else
                    If strData(i).Contains("[") = False Then
                        Dim tmp() As String = strData(i).Split(" ")
                        strData(i) = tmp(2).Trim & ".GetST(Buff, intPos)"
                    Else
                        If strData(i).Split("[").Length = 2 Then
                            'Get name
                            Dim name As String = strData(i).Split(" ")(2)
                            name = name.Substring(0, name.IndexOf("["))
                            strData(i) = ""
                            strData(i) = strData(i) & "For i = 0 To " & name & ".Count - 1" & vbCrLf
                            strData(i) = strData(i) & "     " & name.Trim & "(i).GetST(Buff, intPos)" & vbCrLf
                            strData(i) = strData(i) & "Next" & vbCrLf
                        Else
                            strData(i) = strData(i) & "<<< Not fix"
                        End If
                    End If
                End If

                ' Erase space
                strDmy = strDmy & strData(i).Trim & vbCrLf
            End If
        Next
        CODE_VB.Text = strDmy
    End Sub
End Class

#If 0 Then

		unsigned char led_pwm;			
		unsigned char led_pwm2;	
		unsigned char airclone_id;	
		unsigned char vol_guide;	
		unsigned char settei_id[2];			
		unsigned char toku_extend;			
		unsigned char tx_limit_off;			
					
		unsigned char alarm_on;			
		unsigned char alarm_time[3];
		unsigned char temp_alrm_on;
		unsigned char temp_th;
		unsigned char am_th;
		unsigned char fm_th;


redim 
setteiid(0 to 1)       
redim 
alarmtime(0 to 2)       
    

#End If
