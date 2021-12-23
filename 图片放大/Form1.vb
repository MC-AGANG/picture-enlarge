Public Class Form1
    Dim n As Integer = 0
    Dim ii As Integer = 0
    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Button4.Visible = False
        ListBox1.Visible = False
        Button5.Visible = False
        Button6.Visible = False
        Button7.Visible = False
        ProgressBar1.Visible = False
        Me.Text = "图片放大"
        Timer1.Enabled = False
        Button9.Visible = False
    End Sub
    Public Sub Enlarge(strArg As String)
        Dim p As New Process
        p.StartInfo.FileName = Environment.CurrentDirectory + "\realesrgan-ncnn-vulkan.exe"
        p.StartInfo.Arguments = strArg
        p.StartInfo.UseShellExecute = False
        p.StartInfo.RedirectStandardError = True
        p.StartInfo.RedirectStandardOutput = True
        p.StartInfo.CreateNoWindow = True
        p.Start()
        p.BeginErrorReadLine()
        p.WaitForExit()
        p.Close()
        p.Dispose()
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim command As String
        If ComboBox1.Text = "二次元" Then
            command = "-i """ + TextBox1.Text + """ -o """ + Mid(TextBox1.Text, 1, Len(TextBox1.Text) - 4) + "_output" + Mid(TextBox1.Text, Len(TextBox1.Text) - 3, 4) + """ -n realesrgan-x4plus-anime"
            Enlarge(command)
        Else
            command = "-i """ + TextBox1.Text + """ -o """ + Mid(TextBox1.Text, 1, Len(TextBox1.Text) - 4) + "_output" + Mid(TextBox1.Text, Len(TextBox1.Text) - 3, 4) + """ -n realesrgan-x4plus"
            Enlarge(command)
        End If
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        If OpenFileDialog1.ShowDialog() = DialogResult.OK Then
            TextBox1.Text = OpenFileDialog1.FileName
            PictureBox1.Image = Image.FromFile(TextBox1.Text)
        End If
    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        If OpenFileDialog2.ShowDialog() = DialogResult.OK Then
            For Each f In OpenFileDialog2.FileNames
                ListBox1.Items.Add(f)
                n += 1
            Next
            ProgressBar1.Maximum = n
        End If
    End Sub
    Public Sub Threadsub()
        ii = 0
        Dim command As String
        Dim model As String
        If ComboBox1.Text = "二次元" Then
            model = "realesrgan-x4plus-anime"
        Else
            model = "realesrgan-x4plus"
        End If
        For Each f In ListBox1.Items
            command = "-i """ + f + """ -o """ + Mid(f, 1, Len(f) - 4) + "_output" + Mid(f, Len(f) - 3, 4) + """ -n " + model
            Enlarge(command)
            ii += 1
        Next
    End Sub

    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click
        ListBox1.Items.Clear()
        n = 0
        ProgressBar1.Maximum = n
    End Sub

    Private Sub Button7_Click(sender As Object, e As EventArgs) Handles Button7.Click
        Dim thread1 As Task
        thread1 = System.Threading.Tasks.Task.Run(Sub() Threadsub())
        Button7.Enabled = False
        Timer1.Enabled = True
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        Button4.Visible = False
        ListBox1.Visible = False
        Button5.Visible = False
        Button6.Visible = False
        Button7.Visible = False
        ProgressBar1.Visible = False
        Button9.Visible = False
        Label1.Visible = True
        TextBox1.Visible = True
        Button3.Visible = True
        Button1.Visible = True
        GroupBox1.Visible = True
        Button2.Visible = True
        Me.Text = "图片放大"
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Button4.Visible = True
        ListBox1.Visible = True
        Button5.Visible = True
        Button6.Visible = True
        Button7.Visible = True
        ProgressBar1.Visible = True
        Button9.Visible = True
        Label1.Visible = False
        TextBox1.Visible = False
        Button3.Visible = False
        Button1.Visible = False
        GroupBox1.Visible = False
        Button2.Visible = False
        Me.Text = "图片放大*批处理"
    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        ProgressBar1.Value = ii
        If ii = ProgressBar1.Maximum Then
            Timer1.Enabled = False
            MsgBox("处理完成")
            Button7.Enabled = True

        End If

    End Sub


    Private Sub Button8_Click(sender As Object, e As EventArgs) Handles Button8.Click
        MsgBox("支持的格式：
jpg/png
版本：1.2
by AGANG")
    End Sub

    Private Sub Button9_Click(sender As Object, e As EventArgs) Handles Button9.Click
        ListBox1.Items.Remove(ListBox1.SelectedItem)
    End Sub
End Class
