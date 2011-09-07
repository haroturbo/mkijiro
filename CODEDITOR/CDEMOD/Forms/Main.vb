﻿Imports System.IO       'Stream、StreamWriter、StreamReader、FileStream用
Imports System.Text     'Encoding用
Imports System.Diagnostics
Imports System.Collections
Imports System.Net
Imports System.Text.RegularExpressions

Public Class MERGE
    Friend database As String = Nothing
    Friend loaded As Boolean = False
    Friend PSX As Boolean = False
    Friend CODEFREAK As Boolean = False
    Dim enc1 As Integer = My.Settings.MSCODEPAGE
    Friend maintop As Boolean = My.Settings.TOP
    Friend showerror As Boolean = My.Settings.ERR
    Friend browser As String = My.Settings.browser

#Region "Menubar procedures"

#Region "Open Database/Save Database"

    Private Sub new_psp_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles new_psp.Click

        resets_level1()

        If loaded = False Then
            codetree.BeginUpdate()
            reset_PSP()
            codetree.Nodes.Clear()
            codetree.Nodes.Add("新規データベース").ImageIndex = 0 ' Add the root node and set its icon
            codetree.EndUpdate()
            loaded = True
        ElseIf MessageBox.Show("新規データベースを作成すると現在のデータベースが消えてしまいます。このまま新規データベースを作成してもよろしいですか？", "データベース保存の確認", MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation) = Windows.Forms.DialogResult.OK Then
            codetree.BeginUpdate()
            reset_PSP()
            codetree.Nodes.Clear()
            codetree.Nodes.Add("新規データベース").ImageIndex = 0 ' Add the root node and set its icon
            codetree.EndUpdate()
        End If
        file_saveas.Enabled = True
        UTF16BECP1201ToolStripMenuItem.Enabled = False
        saveas_codefreak.Enabled = False
        PSX = False
        saveas_cwcheat.Enabled = True
        saveas_psx.Enabled = False
        overwrite_db.Enabled = True
    End Sub

    Private Sub new_psx_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles new_psx.Click

        resets_level1()
        If loaded = False Then
            codetree.BeginUpdate()
            reset_PSX()
            codetree.Nodes.Clear()
            codetree.Nodes.Add("新規データベース").ImageIndex = 0 ' Add the root node and set its icon
            codetree.EndUpdate()
            loaded = True
        ElseIf MessageBox.Show("新規データベースを作成すると現在のデータベースが消えてしまいます。このまま新規データベースを作成してもよろしいですか？", "データベース保存の確認", MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation) = Windows.Forms.DialogResult.OK Then
            codetree.BeginUpdate()
            reset_PSX()
            codetree.Nodes.Clear()
            codetree.Nodes.Add("新規データベース").ImageIndex = 0 ' Add the root node and set its icon
            codetree.EndUpdate()
        End If
        file_saveas.Enabled = True
        UTF16BECP1201ToolStripMenuItem.Enabled = False
        saveas_codefreak.Enabled = False
        PSX = True
        saveas_cwcheat.Enabled = False
        saveas_psx.Enabled = True
        overwrite_db.Enabled = True
    End Sub

    Private Sub file_open_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles file_open.Click

        Dim open As New load_db

        If open_file.ShowDialog = Windows.Forms.DialogResult.OK And open_file.FileName <> Nothing Then
            database = open_file.FileName

            error_window.list_save_error.Items.Clear() 'Clear any save errors from a previous database
            PSX = open.check_db(database, 932) ' Check the file's format
            CODEFREAK = open.check2_db(database, 1201)
            codetree.Nodes.Clear()
            codetree.BeginUpdate()
            error_window.list_load_error.BeginUpdate()

            UTF16BECP1201ToolStripMenuItem.Enabled = False
            saveas_codefreak.Enabled = False

            If CODEFREAK = True Then
                reset_PSP()
                Application.DoEvents()
                enc1 = 1201
                open.read_cf(database, 1201)
            ElseIf PSX = True Then
                enc1 = open.check_enc(database)
                reset_PSX()
                Application.DoEvents()
                open.read_PSX(database, enc1)
                PSX = True
            Else
                enc1 = open.check_enc(database)
                reset_PSP()
                Application.DoEvents()
                open.read_PSP(database, enc1)
            End If
            If codetree.Nodes.Count >= 1 Then
                codetree.Nodes(0).Expand()
            End If
            If enc1 = 1201 Then
                UTF16BECP1201ToolStripMenuItem.Enabled = True
                saveas_codefreak.Enabled = True
            End If
            codetree.EndUpdate()
            error_window.list_load_error.EndUpdate()
            loaded = True
            file_saveas.Enabled = True
            overwrite_db.Enabled = True

            My.Settings.lastcodepath = database
            overwrite_db.ToolTipText = "対象;" & Path.GetFileNameWithoutExtension(database)
        End If


    End Sub

    Private Sub overwrite_db_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles overwrite_db.Click
        Dim s As New save_db
        If My.Settings.lastcodepath <> "" Then
            If CODEFREAK = True Then
                s.save_cf(database, 1201)
            ElseIf PSX = True Then
                s.save_psx(database, enc1)
            Else
                s.save_cwcheat(database, enc1)
            End If

            codetree.Nodes(0).Text = Path.GetFileNameWithoutExtension(database)
            If My.Settings.codepathwhensave = True Then
                My.Settings.lastcodepath = database
            End If

        End If
    End Sub

    Private Sub saveas_cwcheat_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles saveas_cwcheat.Click
        Dim open As New load_db
        Dim s As New save_db

        Me.save_file.Filter = "CWcheat (*.db)|*.db|ACTIOPREPLAY (*.ar)|*.ar|CMFUSION (*.cmf)|*.cmf|FreeCheat (*." & _
            "txt)|*.txt"

        If save_file.ShowDialog = Windows.Forms.DialogResult.OK And save_file.FileName <> Nothing Then

            database = save_file.FileName
            s.save_cwcheat(database, enc1)

            codetree.Nodes(0).Text = Path.GetFileNameWithoutExtension(database)
            overwrite_db.ToolTipText = "対象;" & database

            If My.Settings.codepathwhensave = True Then
                My.Settings.lastcodepath = database
            End If

            ' Reload the file
            'codetree.Nodes.Clear()
            'codetree.BeginUpdate()
            'error_window.list_load_error.BeginUpdate()

            'reset_PSP()
            'Application.DoEvents()
            'open.read_PSP(database, enc1)

            'If codetree.Nodes.Count >= 1 Then
            '    codetree.Nodes(0).Expand()
            'End If

            'codetree.EndUpdate()
            'error_window.list_load_error.EndUpdate()

        End If
    End Sub

    Private Sub saveas_psx_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles saveas_psx.Click
        Dim open As New load_db
        Dim s As New save_db

        Me.save_file.Filter = "CWcheat (*.db)|*.db"

        If save_file.ShowDialog = Windows.Forms.DialogResult.OK And save_file.FileName <> Nothing Then

            database = save_file.FileName
            s.save_psx(database, enc1)
            codetree.Nodes(0).Text = Path.GetFileNameWithoutExtension(database)
            overwrite_db.ToolTipText = "対象;" & database

            If My.Settings.codepathwhensave = True Then
                My.Settings.lastcodepath = database
            End If

            ' Reload the file
            'codetree.Nodes.Clear()
            'codetree.BeginUpdate()
            'error_window.list_load_error.BeginUpdate()

            'reset_PSX()
            'Application.DoEvents()
            'open.read_PSX(database, enc1)

            'If codetree.Nodes.Count >= 1 Then
            '    codetree.Nodes(0).Expand()
            'End If

            'codetree.EndUpdate()
            'error_window.list_load_error.EndUpdate()

        End If
    End Sub

    Private Sub saveas_codefreak_click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles saveas_codefreak.Click
        Dim open As New load_db
        Dim s As New save_db

        Me.save_file.Filter = "CODEFREAK (*.dat)|*.dat"

        If save_file.ShowDialog = Windows.Forms.DialogResult.OK And save_file.FileName <> Nothing Then

            database = save_file.FileName
            s.save_cf(database, 1201)
            overwrite_db.ToolTipText = "対象;" & database

            If My.Settings.codepathwhensave = True Then
                My.Settings.lastcodepath = database
                codetree.Nodes(0).Text = Path.GetFileNameWithoutExtension(database)
            End If

            '' Reload the file
            'codetree.Nodes.Clear()
            'codetree.BeginUpdate()
            'error_window.list_load_error.BeginUpdate()

            'reset_PSP()
            'Application.DoEvents()
            'open.read_cf(database, 1201)

            'If codetree.Nodes.Count >= 1 Then
            '    codetree.Nodes(0).Expand()
            'End If

            'codetree.EndUpdate()
            'error_window.list_load_error.EndUpdate()

        End If
    End Sub

    Private Sub file_exit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles file_exit.Click

        My.Settings.mainyoko = Me.Width
        My.Settings.maintate = Me.Height
        Close()
    End Sub


    '初期化
#Region "Control resets"

    Private Sub resets_level1()

        ' Disable editing of games and codes if the root node is selected
        GID_tb.Enabled = False
        GID_tb.Text = Nothing
        GT_tb.Enabled = False
        GT_tb.Text = Nothing
        cmt_tb.Enabled = False
        cmt_tb.Text = Nothing
        cl_tb.Enabled = False
        cl_tb.Text = Nothing
        CT_tb.Enabled = False
        CT_tb.Text = Nothing
        off_rd.Enabled = False
        on_rd.Enabled = False
        Panel1.Enabled = False

        Button1.Enabled = False
        Button2.Enabled = False
        Button3.Enabled = False

        If PSX = False Then
            saveas_cwcheat.Enabled = True
            saveas_psx.Enabled = False
        ElseIf PSX = True Then
            saveas_cwcheat.Enabled = False
            saveas_psx.Enabled = True
        End If

        button_list.Enabled = False
        inverse_chk.Enabled = False
        inverse_chk.Checked = False

        For i = 0 To 19 ' Reset the checked list box state
            button_list.SetItemChecked(i, False)
        Next

    End Sub

    Private Sub resets_level2()

        ' Disable editing of a code if one is not selected
        GID_tb.Enabled = True
        GT_tb.Enabled = True
        cmt_tb.Enabled = False
        cmt_tb.Text = Nothing
        cl_tb.Enabled = False
        cl_tb.Text = Nothing
        CT_tb.Enabled = False
        CT_tb.Text = Nothing
        off_rd.Enabled = False
        on_rd.Enabled = False
        Panel1.Enabled = False

        Button1.Enabled = False
        Button2.Enabled = False
        Button3.Enabled = False


        If PSX = False Then
            saveas_cwcheat.Enabled = True
            saveas_psx.Enabled = False
        ElseIf PSX = True Then
            saveas_cwcheat.Enabled = False
            saveas_psx.Enabled = True
        End If


        button_list.Enabled = False
        inverse_chk.Enabled = False
        inverse_chk.Checked = False

        For i = 0 To 19 ' Reset the checked list box state
            button_list.SetItemChecked(i, False)
        Next

    End Sub

    Private Sub resets_level3()

        ' Enable editing of all controls
        cmt_tb.Enabled = True
        cmt_tb.Text = Nothing
        cl_tb.Enabled = True
        cl_tb.Text = Nothing
        CT_tb.Enabled = True
        off_rd.Enabled = True
        on_rd.Enabled = True
        Panel1.Enabled = True
        Button1.Enabled = True
        Button2.Enabled = True
        Button3.Enabled = True

        If PSX = False Then
            button_list.Enabled = True
            inverse_chk.Enabled = True
        End If

        For i = 0 To 19 ' Reset the checked list box state
            button_list.SetItemChecked(i, False)
        Next

    End Sub

    Private Sub reset_PSX()

        codetree.ImageList = PSX_iconset
        With tool_menu
            add_game.Image = My.Resources.Resources.add_PSX_game
            rem_game.Image = My.Resources.Resources.remove_PSX_game
            save_gc.Image = My.Resources.Resources.save_PSX_game
        End With

    End Sub

    Private Sub reset_PSP()

        codetree.ImageList = iconset
        With tool_menu
            add_game.Image = My.Resources.Resources.add_game
            rem_game.Image = My.Resources.remove_game
            save_gc.Image = My.Resources.Resources.save_game
        End With

    End Sub

#End Region

#End Region

#Region "Sort procedures"
    'さんぷるどおりだとうごくがなぜかうまくいかないので代替関数
    Function sort_game(ByVal mode As Integer) As Boolean

        error_window.Visible = False
        codetree.BeginUpdate() ' This will stop the tree view from constantly drawing the changes while we sort the nodes

        Dim z As Integer = codetree.Nodes(0).Nodes.Count
        Dim i As Integer = 0
        Dim b1 As String = Nothing
        Dim b2 As String = Nothing
        Dim s(z) As String
        For Each n As TreeNode In codetree.Nodes(0).Nodes
            If (mode And 2) = 2 Then
                b1 = n.Name
            Else
                b1 = n.Tag.ToString
            End If
            Dim sb As New System.Text.StringBuilder()
            b2 = n.Index.ToString
            sb.Append(b1)
            sb.Append(" ,")
            sb.Append(b2)
            s(i) = sb.ToString
            i += 1
        Next
        Array.Sort(s)
        Dim j As Integer = 1
        Dim k As Integer = 0
        Dim y As Integer = 0
        Dim commaindex As Integer = 0
        Dim ss As String
        Dim dbtrim As String = Path.GetFileNameWithoutExtension(database)
        codetree.Nodes.Add(dbtrim & "_sort")
        If (mode And 1) = 0 Then
            While k < z
                commaindex = s(j).LastIndexOf(",") + 1
                ss = s(j).Substring(commaindex, s(j).Length - commaindex)
                y = CInt(ss)
                Dim cln As TreeNode = CType(codetree.Nodes(0).Nodes(y).Clone(), TreeNode)
                codetree.Nodes(1).Nodes.Add(cln)
                k += 1
                j += 1
            End While
        ElseIf (mode And 1) = 1 Then
            j = z
            While k < z
                commaindex = s(j).LastIndexOf(",") + 1
                ss = s(j).Substring(commaindex, s(j).Length - commaindex)
                y = CInt(ss)
                Dim cln As TreeNode = CType(codetree.Nodes(0).Nodes(y).Clone(), TreeNode)
                codetree.Nodes(1).Nodes.Add(cln)
                k += 1
                j -= 1
            End While
        End If
        codetree.Nodes(0).Remove()
        If codetree.Nodes.Count >= 1 Then
            codetree.Nodes(0).Expand()
        End If

        codetree.EndUpdate() ' Update the changes made to the tree view.

        If options_error.Checked = True Then
            error_window.Visible = True
        End If

        Return True
    End Function

    Private Sub GID昇順(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Sort_GID１.Click

        'http://msdn.microsoft.com/ja-jp/library/system.windows.forms.treeview.treeviewnodesorter.aspx
        'codetree.TreeViewNodeSorter = New GID_sort
        'codetree.TreeViewNodeSorter = New GID_sort
        sort_game(0)

    End Sub

    Private Sub GID降順(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Sort_GID2.Click
        'codetree.TreeViewNodeSorter = New GID_sortz
        'codetree.TreeViewNodeSorter = New GID_sortz
        sort_game(1)

    End Sub

    Private Sub Sort_GTitle1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Sort_GTitle1.Click

        'codetree.TreeViewNodeSorter = New G_Title_sort
        'codetree.TreeViewNodeSorter = New G_Title_sort
        sort_game(2)

    End Sub

    Private Sub Sort_CTitle2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Sort_GTitle2.Click

        'codetree.TreeViewNodeSorter = New G_Title_sortz
        'codetree.TreeViewNodeSorter = New G_Title_sortz
        sort_game(3)

    End Sub

#End Region

#Region "Options"

#Region "FONT"
    Private Sub ツリービューToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles font_treeview.Click

        Dim fd As New FontDialog()

        fd.Font = codetree.Font
        fd.Color = codetree.ForeColor
        fd.MaxSize = 24
        fd.MinSize = 9
        fd.FontMustExist = True
        fd.ShowHelp = True
        fd.ShowApply = True
        If fd.ShowDialog() <> DialogResult.Cancel Then
            codetree.Font = fd.Font
            My.Settings.codetree = fd.Font
        End If
    End Sub

    Private Sub ゲームタイトルToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles font_gtitle.Click
        Dim fd As New FontDialog()
        fd.Font = GT_tb.Font
        fd.Color = GT_tb.ForeColor
        fd.MaxSize = 12
        fd.MinSize = 9
        fd.FontMustExist = True
        fd.ShowHelp = True
        fd.ShowApply = True
        If fd.ShowDialog() <> DialogResult.Cancel Then
            GT_tb.Font = fd.Font
            My.Settings.GT_tb = fd.Font
        End If
    End Sub

    Private Sub ゲームIDToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles font_gid.Click
        Dim fd As New FontDialog()
        fd.Font = GID_tb.Font
        fd.Color = GID_tb.ForeColor
        fd.MaxSize = 12
        fd.MinSize = 9
        fd.FontMustExist = True
        fd.ShowHelp = True
        fd.ShowApply = True
        fd.FixedPitchOnly = True
        If fd.ShowDialog() <> DialogResult.Cancel Then
            GID_tb.Font = fd.Font
            My.Settings.GID_tb = fd.Font
        End If
    End Sub

    Private Sub コード名ToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles font_codetxt.Click
        Dim fd As New FontDialog()
        fd.Font = CT_tb.Font
        fd.Color = CT_tb.ForeColor
        fd.MaxSize = 12
        fd.MinSize = 9
        fd.FontMustExist = True
        fd.ShowHelp = True
        fd.ShowApply = True
        If fd.ShowDialog() <> DialogResult.Cancel Then
            CT_tb.Font = fd.Font
            My.Settings.CT_tb = fd.Font
        End If
    End Sub

    Private Sub コード内容ToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles font_cmt.Click

        Dim fd As New FontDialog()

        fd.Font = cl_tb.Font
        fd.Color = cl_tb.ForeColor
        fd.MaxSize = 12
        fd.MinSize = 9
        fd.FontMustExist = True
        fd.ShowHelp = True
        fd.ShowApply = True
        fd.FixedPitchOnly = True
        If fd.ShowDialog() <> DialogResult.Cancel Then
            cl_tb.Font = fd.Font
            My.Settings.cl_tb = fd.Font
        End If
    End Sub

    Private Sub コメントToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim fd As New FontDialog()

        fd.Font = cmt_tb.Font
        fd.Color = cmt_tb.ForeColor
        fd.MaxSize = 24
        fd.MinSize = 9
        fd.FontMustExist = True
        fd.ShowHelp = True
        fd.ShowApply = True
        If fd.ShowDialog() <> DialogResult.Cancel Then
            cmt_tb.Font = fd.Font
            My.Settings.cmt_tb = fd.Font
        End If
    End Sub
#End Region

    Private Sub options_error_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

        If options_error.Checked = False Then
            error_window.Show()
            options_error.Checked = True
            options_error.Text = "エラー画面を隠す"
            My.Settings.ERR = True
            Me.Focus()

            If options_ontop.Checked = True Then
                Me.TopMost = True
                error_window.TopMost = True
            End If

        Else
            error_window.Hide()
            options_error.Checked = False
            options_error.Text = "エラー画面を表示"
            My.Settings.ERR = False
        End If

    End Sub

    Private Sub options_ontop_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

        If options_ontop.Checked = False Then
            Me.TopMost = True
            error_window.TopMost = True
            options_ontop.Checked = True
            My.Settings.TOP = True
        Else
            Me.TopMost = False
            error_window.TopMost = False
            options_ontop.Checked = False
            My.Settings.TOP = False
        End If

    End Sub

    Private Sub codesite(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles codesite_browser.Click
        Dim ofd As New OpenFileDialog()
        ofd.InitialDirectory = "C:\Program Files"
        ofd.Filter = _
    "EXEファイル(*.exe)|*.exe"
        ofd.Title = "ブラウザのEXEを選んでください"
        If ofd.ShowDialog() = DialogResult.OK Then
            'OKボタンがクリックされたとき
            '選択されたファイル名を表示する
            My.Settings.browser = ofd.FileName
            browser = ofd.FileName
        End If
    End Sub

    Private Sub nichsite(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles nichannel_browser.Click
        Dim ofd As New OpenFileDialog()
        ofd.InitialDirectory = "C:\Program Files"
        ofd.Filter = _
    "EXEファイル(*.exe)|*.exe"
        ofd.Title = "ブラウザのEXEを選んでください"
        If ofd.ShowDialog() = DialogResult.OK Then
            'OKボタンがクリックされたとき
            '選択されたファイル名を表示する
            My.Settings.nichbrowser = ofd.FileName
        End If
    End Sub


    Private Sub URL8ToolStripMenuItem1_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles URL8custom.Click
        Dim f As New Form3
        f.TextBox1.Text = My.Settings.url8
        f.ShowDialog(Me)
        My.Settings.url8 = f.TextBox1.Text
        URL8.Text = urltrim(f.TextBox1.Text)
        URL8custom.Text = urltrim(f.TextBox1.Text)
        f.Dispose()
    End Sub

    Private Sub URL9ToolStripMenuItem1_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles URL9custom.Click
        Dim f As New Form3
        f.TextBox1.Text = My.Settings.url9
        f.ShowDialog(Me)
        My.Settings.url9 = f.TextBox1.Text
        URL9.Text = urltrim(f.TextBox1.Text)
        URL9custom.Text = urltrim(f.TextBox1.Text)
        f.Dispose()
    End Sub

    Private Sub URL10ToolStripMenuItem1_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles URL10custom.Click
        Dim f As New Form3
        f.TextBox1.Text = My.Settings.url10
        f.ShowDialog(Me)
        My.Settings.url10 = f.TextBox1.Text
        URL10.Text = urltrim(f.TextBox1.Text)
        URL10custom.Text = urltrim(f.TextBox1.Text)
        f.Dispose()
    End Sub

    Function urltrim(ByVal url As String) As String
        Dim r As New Regex(
    "^s?https?://[-_.!~*'()a-zA-Z0-9;/?:@&=+$,%#]+$")
        Dim m As Match = r.Match(url)
        If m.Success Then
            Dim i As Integer = m.Value.IndexOf(":") + 3
            url = url.Substring(i, url.Length - i)
            i = 0
            i = url.IndexOf("/")
            If i > 0 Then
                url = url.Substring(0, i)
            End If
        End If
        Return url
    End Function

    Private Sub APP8ToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles APP8custom.Click
        Dim ofd As New OpenFileDialog()
        ofd.InitialDirectory = "C:\Program Files"
        ofd.Filter = _
    "EXEファイル(*.exe)|*.exe"
        ofd.Title = "ブラウザのEXEを選んでください"
        If ofd.ShowDialog() = DialogResult.OK Then
            'OKボタンがクリックされたとき
            '選択されたファイル名を表示する
            My.Settings.app8 = ofd.FileName
            APP8.Text = exename(ofd.FileName)
            APP8custom.Text = exename(ofd.FileName)
        End If
    End Sub
    Private Sub APP9ToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles APP9custom.Click
        Dim ofd As New OpenFileDialog()
        ofd.InitialDirectory = "C:\Program Files"
        ofd.Filter = _
    "EXEファイル(*.exe)|*.exe"
        ofd.Title = "ブラウザのEXEを選んでください"
        If ofd.ShowDialog() = DialogResult.OK Then
            'OKボタンがクリックされたとき
            '選択されたファイル名を表示する
            My.Settings.app9 = ofd.FileName
            APP9.Text = exename(ofd.FileName)
            APP9custom.Text = exename(ofd.FileName)
        End If
    End Sub
    Private Sub APP10ToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles APP10custom.Click
        Dim ofd As New OpenFileDialog()
        ofd.InitialDirectory = "C:\Program Files"
        ofd.Filter = _
    "EXEファイル(*.exe)|*.exe"
        ofd.Title = "ブラウザのEXEを選んでください"
        If ofd.ShowDialog() = DialogResult.OK Then
            'OKボタンがクリックされたとき
            '選択されたファイル名を表示する
            My.Settings.app10 = ofd.FileName
            APP10.Text = exename(ofd.FileName)
            APP10custom.Text = exename(ofd.FileName)
        End If
    End Sub

    Function exename(ByVal path As String) As String
        Dim root As Integer = path.LastIndexOf("\") + 1

        Dim str As String = path.Substring(root, path.Length - root)

        Return str.Replace(".exe", "")

    End Function

#End Region

#Region "MSCODEPAGE"

    Private Sub CP932ToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CP932ToolStripMenuItem.Click

        'エンコードを指定する場合
        My.Settings.MSCODEPAGE = 932
        enc1 = 932
        GBKToolStripMenuItem.Checked = False
        CP932ToolStripMenuItem.Checked = True
        UTF16BECP1201ToolStripMenuItem.Enabled = False

    End Sub

    Private Sub GBKToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles GBKToolStripMenuItem.Click

        'エンコードを指定する場合
        My.Settings.MSCODEPAGE = 936
        enc1 = 936
        GBKToolStripMenuItem.Checked = True
        CP932ToolStripMenuItem.Checked = False
        UTF16BECP1201ToolStripMenuItem.Enabled = False
    End Sub


    Private Sub UTF16BECP1201ToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles UTF16BECP1201ToolStripMenuItem.Click
        'エンコードを指定する場合
        enc1 = 1201
        GBKToolStripMenuItem.Checked = False
        CP932ToolStripMenuItem.Checked = False
        UTF16BECP1201ToolStripMenuItem.Checked = True
    End Sub

    Private Sub EncodeToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles codepage_select.Click

        If enc1 = 932 Then
            GBKToolStripMenuItem.Checked = False
            CP932ToolStripMenuItem.Checked = True
            UTF16BECP1201ToolStripMenuItem.Checked = False
        ElseIf enc1 = 1201 Then
            GBKToolStripMenuItem.Checked = False
            CP932ToolStripMenuItem.Checked = False
            UTF16BECP1201ToolStripMenuItem.Checked = True
        ElseIf enc1 = 936 Then
            GBKToolStripMenuItem.Checked = True
            CP932ToolStripMenuItem.Checked = False
            UTF16BECP1201ToolStripMenuItem.Checked = False
        End If

    End Sub

#End Region

#Region "codetree"

    'コードパーサー
    Private Sub paserToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cntparser.Click, paserToolStripMenuItem.Click
        Dim backup As String = cmt_tb.Text
        Dim f As New parser
        cmt_tb.Text = Nothing
        f.ShowDialog(Me)
        Dim b1 As String = cmt_tb.Text
        Dim b2 As String() = b1.Split(CChar(vbLf))
        Dim gid As String = Nothing
        Dim gname As String = Nothing
        Dim cname As String = Nothing
        Dim code As String = Nothing
        Dim cname2 As String = Nothing
        Dim code2 As String = Nothing
        Dim coment As String = Nothing
        Dim add As Boolean = False
        Dim havegame As Boolean = False
        Dim nullcode As Boolean = False
        Dim i As Integer = 0
        Dim k As Integer = 0
        Dim level2insert As Integer = 1
        Dim pos As Integer
        Dim parent As Integer
        If codetree.Nodes.Count >= 1 And b1 <> Nothing Then

            codetree.BeginUpdate()

            Dim selnode1stlv As Integer = codetree.SelectedNode.Level
            If selnode1stlv = 2 Then
                pos = codetree.SelectedNode.Index
                parent = codetree.SelectedNode.Parent.Index
            End If

            For Each s As String In b2

                If s.Length >= 2 Then
                    If selnode1stlv = 0 Then
                        If s.Substring(0, 2) = "_S" Then
                            If havegame = True AndAlso nullcode = False Then
                                add = True
                                i = 0
                            End If
                            s = s.PadRight(4)
                            gid = s.Substring(3, s.Length - 3).Trim
                        ElseIf s.Substring(0, 2) = "_G" Then
                            s = s.PadRight(4)
                            gname = s.Substring(3, s.Length - 3).Trim
                            Dim gnode = New TreeNode(gname)
                            With gnode
                                .Name = gname
                                .Tag = gid
                                .ImageIndex = 1
                            End With
                            codetree.Nodes(0).Nodes.Insert(k, gnode)
                            k += 1
                            codetree.SelectedNode = gnode
                            havegame = True
                            nullcode = True
                        End If

                    End If

                    If s.Substring(0, 2) = "_C" Then
                        nullcode = True
                        s = s.PadRight(3, "0"c)
                        If i = 0 Then
                            If s.Substring(2, 1) = "0" Then
                                code = "0" & vbCrLf
                            Else
                                code = "1" & vbCrLf
                            End If
                            cname = s.Substring(3, s.Length - 3).Trim
                        Else
                            add = True
                            If nullcode = True Then
                                code2 &= "0" & vbCrLf
                            End If
                            code = code & coment
                            If s.Substring(2, 1) = "0" Then
                                code2 = "0" & vbCrLf
                            Else
                                code2 = "1" & vbCrLf
                            End If
                            cname2 = s.Substring(3, s.Length - 3).Trim
                        End If
                        i += 1
                    End If

                    If s.Substring(0, 2) = "_L" Or s.Substring(0, 2) = "_M" Or s.Substring(0, 2) = "_N" Then
                        nullcode = False
                        s = s.Replace(vbCr, "")
                        If PSX = True Then
                            s = s.PadRight(17, "0"c)
                            '_L 12345678 1234
                            If s.Substring(2, 1) = " " And s.Substring(11, 1) = " " Then
                                code &= s.Substring(3, 13).Trim & vbCrLf
                            End If
                        Else
                            s = s.PadRight(24, "0"c)
                            '_L 0x12345678 0x12345678
                            If s.Substring(3, 2) = "0x" And s.Substring(14, 2) = "0x" Then
                                If s.Substring(0, 2) = "_M" Then
                                    Dim z As Integer = Integer.Parse(code.Substring(0, 1))
                                    code = code.Remove(0, 1)
                                    z = 2 Or z
                                    code = code.Insert(0, z.ToString())
                                ElseIf s.Substring(0, 2) = "_N" Then
                                    Dim z As Integer = Integer.Parse(code.Substring(0, 1))
                                    code = code.Remove(0, 1)
                                    z = 4 Or z
                                    code = code.Insert(0, z.ToString())
                                End If
                                code &= s.Substring(3, 21).Trim & vbCrLf
                            End If

                        End If
                    End If

                    If s.Substring(0, 1) = "#" Then
                        s = s.Replace("#", "")
                        coment &= "#" & s.Trim & vbCrLf
                    End If
                End If

                If add = True Then
                    Try
                        Dim newcode As New TreeNode

                        With newcode
                            .ImageIndex = 2
                            .SelectedImageIndex = 3
                            .Name = cname
                            .Text = cname
                            .Tag = code
                        End With

                        Select Case codetree.SelectedNode.Level

                            Case Is = 1
                                codetree.SelectedNode.Nodes.Add(newcode)
                            Case Is = 2
                                codetree.Nodes(0).Nodes(parent).Nodes.Insert(pos + level2insert, newcode)
                                level2insert += 1
                        End Select

                    Catch ex As Exception

                    End Try

                    code = code2
                    cname = cname2
                    coment = Nothing
                    add = False
                End If
            Next
            codetree.EndUpdate()
        End If
        f.Dispose()
        cmt_tb.Text = backup
    End Sub

    Private Sub すべて閉じるToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tree_collapse.Click, cntclose.Click
        codetree.CollapseAll()
    End Sub

    Private Sub 全て展開するToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tree_expand.Click, cntexpand.Click
        codetree.ExpandAll()
    End Sub


    Private Sub 半角カナ全角ToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles str_wide.Click, hankaku.Click

        codetree.BeginUpdate() ' This will stop the tree view from constantly drawing the changes while we sort the nodes

        Dim z As Integer = 0
        Dim i As Integer = 0
        Dim b1 As String = Nothing
        Dim b2 As String = Nothing
        For Each n As TreeNode In codetree.Nodes(0).Nodes
            b1 = n.Text
            b1 = ConvANK(b1)
            codetree.Nodes(0).Nodes(i).Text = b1
            For Each m As TreeNode In n.Nodes
                b2 = m.Text
                b2 = ConvANK(b2)
                codetree.Nodes(0).Nodes(i).Nodes(z).Text = b2
                z += 1
            Next
            i += 1
            z = 0
        Next
        codetree.EndUpdate()
    End Sub

    Public Function ConvANK(ByVal moto As String) As String
        '-- 半角カタカナ(Unicodeで\uFF61-\uFF9Fが範囲)を全角に --
        Dim re2 As Regex = New Regex("[\uFF61-\uFF9F]+")
        Dim output2 As String = re2.Replace(moto, AddressOf myReplacer2)
        Return output2
    End Function

    Shared Function myReplacer2(ByVal m As Match) As String
        Return Strings.StrConv(m.Value, VbStrConv.Wide, 0)
    End Function


    Private Sub 中国語文字化け対策ToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles str_gbk.Click, CNchar.Click

        codetree.BeginUpdate() ' This will stop the tree view from constantly drawing the changes while we sort the nodes

        Dim z As Integer = 0
        Dim i As Integer = 0
        Dim b1 As String = Nothing
        Dim b2 As String = Nothing
        For Each n As TreeNode In codetree.Nodes(0).Nodes
            b1 = n.Text
            b1 = ConvCH(b1)
            codetree.Nodes(0).Nodes(i).Text = b1
            For Each m As TreeNode In n.Nodes
                b2 = m.Text
                b2 = ConvCH(b2)
                codetree.Nodes(0).Nodes(i).Nodes(z).Text = b2
                z += 1
            Next
            i += 1
            z = 0
        Next
        codetree.EndUpdate()
    End Sub


    Public Function ConvCH(ByVal moto As String) As String
        Dim st As String() = {"ー", "∋", "⊆", "⊇", "⊂", "⊃", "￢", "⇒", "⇔", "∃", "∂", "∇", "≪", "≫", "∬", "Å", "♯", "♭", "♪", "†", "‡", "¶", "⑪", "⑫", "⑬", "⑭", "⑮", "⑯", "⑰", "⑱", "⑲", "⑳", "㍉", "㌔", "㌢", "㍍", "㌘", "㌧", "㌃", "㌶", "㍑", "㍗", "㌍", "㌦", "㌣", "㌫", "㍊", "㌻", "㍻", "〝", "〟", "㏍", "㊤", "㊥", "㊦", "㊧", "㊨", "㍾", "㍽", "㍼"}
        Dim sr As String() = {"-", " ", " ", " ", " ", " ", " ", "→", "↔", "ヨ", "", "", "<<", ">>", "ダブルインテグラル", "オングストローム", "シャ-プ", "フラット", "8分音符", "ダガー", "ダブルダガー", "パラグラフ", "11", "12", "13", "14", "15", "16", "17", "18", "19", "20", "ミリ", "キロ", "センチ", "メ-トル", "グラム", "トン", "ア-ル", "ヘクタ-ル", "リットル", "ワｯト", "カロリ-", "ドル", "セント", "パ-セント", "ミリバ-ル", "ペ-ジ", "平成", " ", " ", "KK", "上", "中", "下", "左", "右", "明治", "大正", "昭和"}
        Dim i As Integer = 0
        For i = 0 To 59
            If moto.Contains(st(i)) Then
                moto = moto.Replace(st(i), sr(i))
            End If
        Next
        Return moto
    End Function

#End Region

#Region "BROWSER"
    Private Sub wikiToolStripMenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles wikiToolStripMenuItem1.Click
        Process.Start(browser, "http://www21.atwiki.jp/cwcwiki/")
    End Sub

    Private Sub OHGToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OHGToolStripMenuItem.Click
        Process.Start(browser, "http://www.onehitgamer.com/forum/")
    End Sub

    Private Sub HAXToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles HAXToolStripMenuItem.Click
        Process.Start(browser, "http://haxcommunity.org/")
    End Sub

    Private Sub CNGBAToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CNGBAToolStripMenuItem.Click
        Process.Start(browser, "http://www.cngba.com/forum-988-1.html")
    End Sub

    Private Sub GOOGLEToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles GOOGLEToolStripMenuItem.Click
        Process.Start(browser, "http://www.google.co.jp/")
    End Sub

    Private Sub CMF暗号復元ToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmf_decript.Click
        Process.Start(browser, "http://raing3.gshi.org/psp-utilities/#index.php?action=cmf-decrypter")
    End Sub

    Private Sub cwcToolStripMenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cwcToolStripMenuItem1.Click
        Process.Start(browser, "http://www.myconsole.it/143-cwcheat-official-support-forum/98-english-support-board/")
    End Sub

    Private Sub url8ToolStripMenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles URL8.Click
        Process.Start(browser, My.Settings.url8)
    End Sub
    Private Sub url9ToolStripMenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles URL9.Click
        Process.Start(browser, My.Settings.url9)
    End Sub
    Private Sub url10ToolStripMenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles URL10.Click
        Process.Start(browser, My.Settings.url10)
    End Sub

    Private Sub CDEMODToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Process.Start(browser, "http://unzu127xp.pa.land.to/data/IJIRO/CDEMOD/bin/Release/index.html")
    End Sub

#End Region

#Region "EXECUTE"
    Private Sub KAKASI_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles KAKASI.Click, cntkakasi.Click
        boot("APP\kakasi.bat")
    End Sub

    Private Sub MECAB_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MECAB.Click
        boot("APP\kanahenkan.bat")
    End Sub

    Private Sub PMETAN変換ToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles pme_cnv.Click
        boot("APP\pme.bat")
    End Sub

    Private Sub TEMPAR鶴ToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles temparutility.Click
        boot("APP\temp.bat")
    End Sub

    Private Sub WgetToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Wget.Click
        boot("APP\wget.bat")
    End Sub

    Private Sub JaneStyleToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles nichanbrowser.Click
        boot(My.Settings.nichbrowser)
    End Sub

    Private Sub PSPへコードコピーToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles copy_to_psp.Click, cntdbcopy.Click
        boot("APP\cp.bat")
    End Sub

    Private Sub 登録なし8ToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles APP8.Click
        boot(My.Settings.app8)
    End Sub
    Private Sub 登録なし9ToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles APP9.Click
        boot(My.Settings.app9)
    End Sub
    Private Sub 登録なし10ToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles APP10.Click
        boot(My.Settings.app10)
    End Sub

    Function boot(ByVal exe As String) As Boolean

        If exe = "" Then
            MessageBox.Show("アプリケーションが登録されてません。", "アプリ未登録")
            Return False
        ElseIf Not exe.Contains(":") AndAlso Not exe.Contains(Application.StartupPath) _
            AndAlso exe.Contains("APP\") AndAlso exe.Contains(".bat") Then
            exe = Application.StartupPath & "\" & exe
        End If

        If System.IO.File.Exists(exe) Then
            Process.Start(exe)
        Else
            MessageBox.Show("'" + exe + "'が見つかりませんでした。")
        End If

        Return True
    End Function

#End Region

#Region "HELP"

    Private Sub オンラインヘルプToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles オンラインヘルプToolStripMenuItem.Click
        System.Diagnostics.Process.Start(browser, "http://unzu127xp.pa.land.to/data/CDE.html")
    End Sub

    Private Sub バージョン情報ToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles バージョン情報ToolStripMenuItem.Click
        Dim f As New Form2
        f.ShowDialog(Me)
        f.Dispose()
    End Sub




#End Region

    Private Sub menu_option(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles menu_options.Click
        ToolStripButton1.Enabled = False
        ToolStripButton2.Enabled = False
        ToolStripButton3.Enabled = False
    End Sub

    Private Sub help(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ヘルプHToolStripMenuItem.Click
        ToolStripButton1.Enabled = False
        ToolStripButton2.Enabled = False
        ToolStripButton3.Enabled = False
    End Sub

#End Region

#Region "Toolbar buttons procedures"
    Private Sub add_game_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles add_game.Click

        Try

            Dim newgame As New TreeNode
            With newgame
                .Name = "新規ゲーム"
                .Text = "新規ゲーム"
                .ImageIndex = 1
                .Tag = "0000-00000"
            End With
            codetree.Nodes(0).Nodes.Insert(0, newgame)
            codetree.SelectedNode = newgame
            GT_tb.Enabled = True
            GT_tb.Text = "新規ゲーム"
        Catch ex As Exception

        End Try


    End Sub

    Private Sub rem_game_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rem_game.Click

        Try
            Select Case codetree.SelectedNode.Level

                Case Is <> 0
                    If MessageBox.Show("選択しているゲームとコードをすべて削除しますか？", "削除の確認", _
                                      MessageBoxButtons.OKCancel, MessageBoxIcon.Question) = Windows.Forms.DialogResult.OK Then
                        Select Case codetree.SelectedNode.Level
                            Case Is = 1
                                codetree.SelectedNode.Remove()
                            Case Is = 2
                                codetree.SelectedNode.Parent.Remove()
                        End Select

                    End If

            End Select

        Catch ex As Exception

        End Try

    End Sub

    Private Sub Add_cd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Add_cd.Click

        Try
            Dim newcode As New TreeNode

            With newcode
                .ImageIndex = 2
                .SelectedImageIndex = 3
                .Name = "新規コード"
                .Text = "新規コード"
                .Tag = "0"
            End With

            Select Case codetree.SelectedNode.Level

                Case Is = 1

                    off_rd.Checked = True
                    CT_tb.Enabled = True
                    CT_tb.Text = "新規コード"
                    cmt_tb.Enabled = True
                    cl_tb.Enabled = True
                    codetree.SelectedNode.Nodes.Insert(0, newcode)
                    codetree.SelectedNode = newcode
                Case Is = 2

                    off_rd.Checked = True
                    CT_tb.Enabled = True
                    CT_tb.Text = "新規コード"
                    cmt_tb.Enabled = True
                    cl_tb.Enabled = True
                    codetree.SelectedNode.Parent.Nodes.Insert(codetree.SelectedNode.Index + 1, newcode)
                    codetree.SelectedNode = newcode

            End Select

        Catch ex As Exception

        End Try

    End Sub

    Private Sub rem_cd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rem_cd.Click

        Try
            If codetree.SelectedNode.Level = 2 Then

                If MessageBox.Show("選択しているコードを削除しますか?", "削除の確認", MessageBoxButtons.OKCancel, _
                   MessageBoxIcon.Question) = Windows.Forms.DialogResult.OK Then

                    codetree.SelectedNode.Remove()

                End If

            End If

        Catch ex As Exception

        End Try

    End Sub

    Private Sub save_gc_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles save_gc.Click

        changed.Text = ""
        Try
            GID_tb.Text = System.Text.RegularExpressions.Regex.Replace(GID_tb.Text, "[^\-0-9A-Za-z]", "0").ToUpper
            Select Case codetree.SelectedNode.Level

                Case Is = 1
                    With codetree.SelectedNode
                        .Name = GT_tb.Text
                        .Text = GT_tb.Text
                        .Tag = GID_tb.Text
                    End With

                Case Is = 2
                    With codetree.SelectedNode.Parent
                        .Name = GT_tb.Text
                        .Text = GT_tb.Text
                        .Tag = GID_tb.Text
                    End With
            End Select

        Catch ex As Exception

        End Try

    End Sub

    Private Sub save_cc_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles save_cc.Click

        changed.Text = ""
        Try

            Dim b1 As String = cl_tb.Text
            Dim buffer As String = Nothing
            Dim i As Integer = 0
            Dim b5 As String = cmt_tb.Text
            cl_tb.Text = Nothing
            cmt_tb.Text = Nothing
            If off_rd.Checked = True Then
                If RadioButton5.Checked = True Then
                    buffer = "2" & vbCrLf
                ElseIf RadioButton6.Checked = True Then
                    buffer = "4" & vbCrLf
                Else
                    buffer = "0" & vbCrLf
                End If
            Else
                If RadioButton5.Checked = True Then
                    buffer = "3" & vbCrLf
                ElseIf RadioButton6.Checked = True Then
                    buffer = "5" & vbCrLf
                Else
                    buffer = "1" & vbCrLf
                End If
            End If

            If PSX = True Then
                Dim r As New System.Text.RegularExpressions.Regex( _
        "[0-9a-fA-F]{8} [0-9a-zA-Z?]{4}", _
        System.Text.RegularExpressions.RegexOptions.IgnoreCase)

                Dim m As System.Text.RegularExpressions.Match = r.Match(b1)

                While m.Success
                    buffer &= (m.Value) & vbCrLf
                    cl_tb.Text &= (m.Value) & vbCrLf
                    m = m.NextMatch()
                End While
            Else
                b1 = b1.Replace("_L ", "")
                Dim r As New System.Text.RegularExpressions.Regex( _
        "0x........ 0x........", _
        System.Text.RegularExpressions.RegexOptions.IgnoreCase)

                Dim m As System.Text.RegularExpressions.Match = r.Match(b1)

                While m.Success
                    buffer &= (m.Value) & vbCrLf
                    cl_tb.Text &= (m.Value) & vbCrLf
                    m = m.NextMatch()
                End While

                '        b1 = cl_tb.Text.Replace("_L ", "")
                '        b1 = System.Text.RegularExpressions.Regex.Replace( _
                '            b1, "_C.+\n", vbCrLf)
                '        b1 = System.Text.RegularExpressions.Regex.Replace( _
                '        b1, "[!-/;-@\u005B-`\u007B-\uFFFF].+\n", vbCrLf)
                buffer = System.Text.RegularExpressions.Regex.Replace( _
        buffer, "[g-zG-Z]", "A")
                buffer = buffer.ToUpper
                buffer = System.Text.RegularExpressions.Regex.Replace( _
        buffer, "^0A", "0x")
                buffer = System.Text.RegularExpressions.Regex.Replace( _
        buffer, "(\r|\n)0A", vbCrLf & "0x")
                buffer = buffer.Replace(" 0A", " 0x")
                '        b1 = System.Text.RegularExpressions.Regex.Replace( _
                'b1, "[!-/;-@\u005B-`\u007B-\uFFFF].+[^0-9A-F]$", "")
                '        Dim b2 As String() = b1.Split(CChar(vbCrLf))
            End If

            If codetree.SelectedNode.Level = 2 Then
                codetree.BeginUpdate()
                codetree.SelectedNode.Name = CT_tb.Text.Replace("_C0 ", "")
                codetree.SelectedNode.Text = CT_tb.Text.Replace("_C0 ", "")
                codetree.SelectedNode.Name = codetree.SelectedNode.Name.Replace("_C1 ", "")
                codetree.SelectedNode.Text = codetree.SelectedNode.Text.Replace("_C1 ", "")
                CT_tb.Text = codetree.SelectedNode.Name
                'For Each s As String In b2

                '    If s <> vbCrLf Then
                '        If i = 0 Then
                '            If off_rd.Checked = True Then
                '                buffer = "0" & vbCrLf
                '            Else
                '                buffer = "1" & vbCrLf
                '            End If
                '            i += 1
                '        End If

                '        If i > 0 And s.Length > 2 Then
                '            buffer &= s.Trim & vbCrLf
                '        End If
                '    End If

                'Next
                If b5 <> Nothing Then
                    Dim b3 As String() = b5.Split(CChar(vbLf))
                    For Each s As String In b3
                        s = s.Replace("#", "")
                        If i = 0 Then
                            If s.Substring(0, 1) >= "!" Then
                                buffer &= "#" & s.Trim & vbCrLf
                                cmt_tb.Text &= s.Trim & vbCrLf

                            End If
                        End If

                        If i > 0 And s.Length > 1 Then
                            buffer &= "#" & s.Trim & vbCrLf
                            cmt_tb.Text &= s.Trim & vbCrLf
                        End If
                        i += 1
                    Next
                End If


                codetree.SelectedNode.Tag = buffer
                codetree.EndUpdate()
            End If


        Catch ex As Exception

        End Try

    End Sub

    Private Sub ToolStripButton1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripButton1.Click

        Try

            Dim newcode As New TreeNode

            If codetree.SelectedNode.Level = 1 Then
                codetree.BeginUpdate()
                Dim cln As TreeNode = CType(codetree.SelectedNode.Clone(), TreeNode)
                codetree.SelectedNode.Parent.Nodes.Insert(codetree.SelectedNode.Index - 1, cln)
                codetree.SelectedNode.Remove()
                codetree.SelectedNode = cln
                codetree.EndUpdate()
            End If

            If codetree.SelectedNode.Level = 2 Then

                With newcode
                    .ImageIndex = 2
                    .SelectedImageIndex = 3
                    .Name = codetree.SelectedNode.Name
                    .Text = codetree.SelectedNode.Text
                    .Tag = codetree.SelectedNode.Tag
                End With

                codetree.BeginUpdate()
                codetree.SelectedNode.Parent.Nodes.Insert(codetree.SelectedNode.Index - 1, newcode)
                codetree.SelectedNode.Remove()
                codetree.SelectedNode = newcode
                codetree.EndUpdate()
            End If

        Catch ex As Exception

        End Try
    End Sub

    Private Sub ToolStripButton2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripButton2.Click

        Try
            Dim newcode As New TreeNode


            If codetree.SelectedNode.Level = 1 Then
                codetree.BeginUpdate()
                Dim cln As TreeNode = CType(codetree.SelectedNode.Clone(), TreeNode)
                codetree.SelectedNode.Parent.Nodes.Insert(codetree.SelectedNode.Index + 2, cln)
                codetree.SelectedNode.Remove()
                codetree.SelectedNode = cln
                codetree.EndUpdate()
            End If

            If codetree.SelectedNode.Level = 2 Then

                With newcode
                    .ImageIndex = 2
                    .SelectedImageIndex = 3
                    .Name = codetree.SelectedNode.Name
                    .Text = codetree.SelectedNode.Text
                    .Tag = codetree.SelectedNode.Tag
                End With

                codetree.BeginUpdate()
                codetree.SelectedNode.Parent.Nodes.Insert(codetree.SelectedNode.Index + 2, newcode)
                codetree.SelectedNode.Remove()
                codetree.SelectedNode = newcode
                codetree.EndUpdate()
            End If

        Catch ex As Exception

        End Try
    End Sub

    Private Sub ToolStripButton3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripButton3.Click

        Try
            Dim newcode As New TreeNode
            Dim newcode2 As New TreeNode

            If codetree.SelectedNode.Level = 1 Then
                codetree.BeginUpdate()

                Dim i As Integer = 0
                Dim z As Integer = codetree.SelectedNode.Index
                Dim x As Integer = codetree.SelectedNode.Parent.Index
                Dim rr As TreeNode = codetree.SelectedNode
                Dim kk As Integer = rr.Nodes.Count

                If z > 0 Then
                    While kk > 0
                        merge_prevtreeview(z)
                        kk -= 1
                    End While
                End If

                codetree.EndUpdate()
            End If

            If codetree.SelectedNode.Level = 2 Then

                With newcode
                    .ImageIndex = 2
                    .SelectedImageIndex = 3
                    .Name = codetree.SelectedNode.Name
                    .Text = codetree.SelectedNode.Text
                    .Tag = codetree.SelectedNode.Tag
                End With
                Dim z As Integer = codetree.SelectedNode.Index
                Dim x As Integer = codetree.SelectedNode.Parent.Index
                With newcode2
                    .ImageIndex = 2
                    .SelectedImageIndex = 3
                    .Name = codetree.Nodes(0).Nodes(x).Nodes(z - 1).Name
                    .Text = codetree.Nodes(0).Nodes(x).Nodes(z - 1).Text
                    .Tag = codetree.Nodes(0).Nodes(x).Nodes(z - 1).Tag
                End With

                codetree.BeginUpdate()
                Dim b1 As String = newcode.Tag.ToString
                Dim b2 As String = newcode2.Tag.ToString
                b2 &= b1.Remove(0, 1) & vbCrLf
                newcode.Name &= "'"
                newcode.Text &= "'"
                newcode.Tag = b2
                Dim a As Integer = CInt(b1.Substring(0, 1))
                Dim b As Integer = CInt(b2.Substring(0, 1))

                If z = 0 Then

                ElseIf (b And &HE) = (a And &HE) Then
                    codetree.Nodes(0).Nodes(x).Nodes(z - 1).Remove()
                    codetree.SelectedNode.Parent.Nodes.Insert(codetree.SelectedNode.Index, newcode)
                    codetree.SelectedNode.Remove()
                    codetree.SelectedNode = newcode

                ElseIf MessageBox.Show("コード形式が一致してません。このまま合成しますか？", "コード合成の確認", MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation) = Windows.Forms.DialogResult.OK Then
                    codetree.Nodes(0).Nodes(x).Nodes(z - 1).Remove()
                    codetree.SelectedNode.Parent.Nodes.Insert(codetree.SelectedNode.Index, newcode)
                    codetree.SelectedNode.Remove()
                    codetree.SelectedNode = newcode
                End If
                codetree.EndUpdate()
            End If

        Catch ex As Exception

        End Try
    End Sub

    Function merge_prevtreeview(ByVal z As Integer) As Boolean

        Dim newcode As New TreeNode

        With newcode
            .ImageIndex = 2
            .SelectedImageIndex = 3
            .Name = codetree.Nodes(0).Nodes(z).Nodes(0).Name
            .Text = codetree.Nodes(0).Nodes(z).Nodes(0).Text
            .Tag = codetree.Nodes(0).Nodes(z).Nodes(0).Tag
        End With
        codetree.Nodes(0).Nodes(z - 1).Nodes.Add(newcode)
        codetree.Nodes(0).Nodes(z).Nodes(0).Remove()
        Return True
    End Function
#End Region
    'ツリー操作
#Region "Code tree procedures"

    Private Sub main_Load(ByVal sender As Object, _
        ByVal e As EventArgs) Handles MyBase.Load
        'http://dobon.net/vb/dotnet/control/tvdraganddrop.html
        'TreeView1へのドラッグを受け入れる
        codetree.AllowDrop = True

        Me.Width = My.Settings.mainyoko
        Me.Height = My.Settings.maintate
        If My.Settings.fixedform = True Then
            Me.FormBorderStyle = FormBorderStyle.FixedToolWindow
            fixedform.Checked = True
        End If

        If System.IO.File.Exists(browser) Then
        Else
            browser = "IExplore.exe"
        End If

        If My.Settings.app8 <> "" Then
            APP8.Text = exename(My.Settings.app8)
            APP8custom.Text = APP8.Text
        End If
        If My.Settings.app9 <> "" Then
            APP9.Text = exename(My.Settings.app9)
            APP9custom.Text = APP9.Text
        End If
        If My.Settings.app10 <> "" Then
            APP10.Text = exename(My.Settings.app10)
            APP10custom.Text = APP10.Text
        End If

        If My.Settings.url8 <> "" Then
            URL8.Text = urltrim(My.Settings.url8)
            URL8custom.Text = URL8.Text
        End If
        If My.Settings.url9 <> "" Then
            URL9.Text = urltrim(My.Settings.url9)
            URL9custom.Text = URL9.Text
        End If
        If My.Settings.url10 <> "" Then
            URL10.Text = urltrim(My.Settings.url10)
            URL10custom.Text = URL8.Text
        End If



        If System.IO.File.Exists(My.Settings.lastcodepath) Then
            Dim open As New load_db
            database = My.Settings.lastcodepath
            PSX = open.check_db(database, 932) ' Check the file's format
            CODEFREAK = open.check2_db(database, 1201)
            codetree.BeginUpdate()
            error_window.list_load_error.BeginUpdate()

            If CODEFREAK = True Then
                reset_PSP()
                Application.DoEvents()
                enc1 = 1201
                open.read_cf(database, 1201)
                saveas_cwcheat.Enabled = True
                saveas_psx.Enabled = False
                UTF16BECP1201ToolStripMenuItem.Enabled = True
                saveas_codefreak.Enabled = True
            ElseIf PSX = True Then
                enc1 = open.check_enc(database)
                reset_PSX()
                Application.DoEvents()
                open.read_PSX(database, enc1)
                saveas_psx.Enabled = True
                saveas_cwcheat.Enabled = False
                UTF16BECP1201ToolStripMenuItem.Enabled = False
                saveas_codefreak.Enabled = False
            Else
                enc1 = open.check_enc(database)
                reset_PSP()
                Application.DoEvents()
                open.read_PSP(database, enc1)
                saveas_cwcheat.Enabled = True
                saveas_psx.Enabled = False
                UTF16BECP1201ToolStripMenuItem.Enabled = False
                saveas_codefreak.Enabled = False
            End If

            If My.Settings.codepathwhensave = True Then
                update_save_filepass.Checked = True
            Else
                update_save_filepass.Checked = False
            End If

            If codetree.Nodes.Count >= 1 Then
                codetree.Nodes(0).Expand()
            End If
            codetree.EndUpdate()
            error_window.list_load_error.EndUpdate()
            loaded = True
            file_saveas.Enabled = True
            overwrite_db.Enabled = True
            overwrite_db.ToolTipText = "対象;" & database
        End If

        'イベントハンドラを追加する
        AddHandler codetree.ItemDrag, AddressOf codetree_ItemDrag
        AddHandler codetree.DragOver, AddressOf codetree_DragOver
        AddHandler codetree.DragDrop, AddressOf codetree_DragDrop

        If showerror = True Then
            error_window.Show()
            options_error.Checked = True
            options_error.Text = "エラー画面を隠す"

            If maintop = True Then
                error_window.TopMost = True
            End If

        Else
            error_window.Hide()
            options_error.Checked = False
            options_error.Text = "エラー画面を表示"
        End If

        If maintop = True Then
            Me.TopMost = True
            error_window.TopMost = True
            options_ontop.Checked = True
        Else
            error_window.TopMost = False
            options_ontop.Checked = False
        End If

        If My.Settings.gridvalueedit = True Then
            grided_use.Checked = True
            Button4.Visible = True
        End If


        CT_tb.Font = My.Settings.CT_tb
        GID_tb.Font = My.Settings.GID_tb
        GT_tb.Font = My.Settings.CT_tb
        cmt_tb.Font = My.Settings.cmt_tb
        cl_tb.Font = My.Settings.cl_tb
        codetree.Font = My.Settings.codetree

    End Sub

    Private Sub codetree_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles codetree.KeyUp
        If e.KeyCode = Keys.Delete Then
            Try
                If codetree.SelectedNode.Level = 1 Then
                    If MessageBox.Show("選択しているゲームとコードをすべて削除しますか？", "削除の確認", _
                       MessageBoxButtons.OKCancel, MessageBoxIcon.Question) = Windows.Forms.DialogResult.OK Then
                        codetree.SelectedNode.Remove()
                    End If
                ElseIf codetree.SelectedNode.Level = 2 Then

                    If MessageBox.Show("選択されたコードを削除しますか?", "削除の確認", MessageBoxButtons.OKCancel, _
                       MessageBoxIcon.Question) = Windows.Forms.DialogResult.OK Then
                        codetree.SelectedNode.Remove()
                    End If
                End If

            Catch ex As Exception

            End Try
        End If
    End Sub

    'ツリー選択時
    Private Sub codetree_AfterSelect(ByVal sender As System.Object, ByVal e As System.Windows.Forms.TreeViewEventArgs) Handles codetree.AfterSelect
        Dim j As New joker

        changed.Text = ""
        ToolStripButton1.Enabled = True
        ToolStripButton2.Enabled = True
        ToolStripButton3.Enabled = True


        Select Case codetree.SelectedNode.Level

            Case Is = 0
                codetree.SelectedNode.SelectedImageIndex = 0
                resets_level1() ' Sets appropriate access to code editing
            Case Is = 1
                codetree.SelectedNode.SelectedImageIndex = 1
                GID_tb.Text = codetree.SelectedNode.Tag.ToString.Trim
                GT_tb.Text = codetree.SelectedNode.Text.Trim
                resets_level2() ' Sets appropriate access to code editing
            Case Is = 2
                Dim b1 As String = codetree.SelectedNode.Tag.ToString
                Dim b2 As String() = b1.Split(CChar(vbCrLf))
                Dim i As Integer = 0
                Dim skip As Boolean = False

                codetree.SelectedNode.SelectedImageIndex = 3
                CT_tb.Text = codetree.SelectedNode.Text.Trim
                GID_tb.Text = codetree.SelectedNode.Parent.Tag.ToString.Trim
                GT_tb.Text = codetree.SelectedNode.Parent.Text.Trim
                resets_level3() ' Sets appropriate access to code editing

                For Each s As String In b2

                    skip = False

                    s = s.Trim ' Remove the new line character so it doesn't interfere with checks

                    If i = 0 Then ' If on the first line, check if the code is enabled by default

                        If s = "1" Or s = "3" Or s = "5" Then
                            on_rd.Checked = True
                        Else
                            off_rd.Checked = True
                        End If

                        If s = "4" Or s = "5" Then
                            RadioButton6.Checked = True
                        ElseIf s = "2" Or s = "3" Then
                            RadioButton5.Checked = True
                        ElseIf s = "0" Or s = "1" Then
                            RadioButton4.Checked = True
                        End If

                        skip = True

                    End If

                    i += 1

                    Try

                        If s <> Nothing And skip = False Then

                            ' Check for a joker
                            If s.Trim.Length = 21 Then

                                If s.Substring(2, 1).ToUpper = "D" And s.Substring(13, 1) = "1" Then
                                    j.button_value(s)
                                ElseIf s.Substring(2, 1).ToUpper = "D" And s.Substring(13, 1) = "3" Then
                                    inverse_chk.Checked = True
                                    j.button_value(s)
                                End If

                            End If

                            If s.Length >= 2 Then

                                If s.Contains("#") Then
                                    cmt_tb.Text &= s.Substring(1, s.Length - 1) & vbCrLf
                                Else
                                    cl_tb.Text &= s & vbCrLf
                                End If

                            End If

                        End If

                    Catch ex As Exception
                        MsgBox(ex.Message)
                    End Try

                Next

        End Select

    End Sub

    'ドラッグ
    Private Sub codetree_ItemDrag(ByVal sender As Object, _
        ByVal e As ItemDragEventArgs)
        Dim tv As TreeView = CType(sender, TreeView)
        tv.SelectedNode = CType(e.Item, TreeNode)
        tv.Focus()

        'ノードのドラッグを開始する
        Dim dde As DragDropEffects = _
            tv.DoDragDrop(e.Item, DragDropEffects.All)

    End Sub

    'ドラッグしている時
    Private Sub codetree_DragOver(ByVal sender As Object, _
            ByVal e As DragEventArgs)
        'ドラッグされているデータがTreeNodeか調べる
        If e.Data.GetDataPresent(GetType(TreeNode)) Then
            If (e.KeyState And 8) = 8 And _
                (e.AllowedEffect And DragDropEffects.Copy) = _
                    DragDropEffects.Copy Then
                'Ctrlキーが押されていればCopy
                '"8"はCtrlキーを表す
                e.Effect = DragDropEffects.Copy
            ElseIf (e.AllowedEffect And DragDropEffects.Move) = _
                DragDropEffects.Move Then
                '何も押されていなければMove
                e.Effect = DragDropEffects.Move
            Else
                e.Effect = DragDropEffects.None
            End If
        Else
            'TreeNodeでなければ受け入れない
            e.Effect = DragDropEffects.None
        End If

        'マウス下のNodeを選択する
        If e.Effect <> DragDropEffects.None Then
            Dim tv As TreeView = CType(sender, TreeView)
            'マウスのあるNodeを取得する
            Dim target As TreeNode = _
                tv.GetNodeAt(tv.PointToClient(New Point(e.X, e.Y)))
            'ドラッグされているNodeを取得する
            Dim [source] As TreeNode = _
                CType(e.Data.GetData(GetType(TreeNode)), TreeNode)
            'マウス下のNodeがドロップ先として適切か調べる
            If Not target Is Nothing AndAlso _
                target.Level = [source].Level AndAlso _
                Not target Is [source] AndAlso _
                Not IsChildNode([source], target) AndAlso _
        ToolStripButton1.Enabled = True Then
                'Nodeを選択する
                If target.IsSelected = False Then
                    tv.SelectedNode = target
                End If
            Else
                e.Effect = DragDropEffects.None
            End If
        End If
    End Sub

    'ドロップされたとき
    Private Sub codetree_DragDrop(ByVal sender As Object, _
            ByVal e As DragEventArgs)
        'ドロップされたデータがTreeNodeか調べる
        If e.Data.GetDataPresent(GetType(TreeNode)) Then
            Dim tv As TreeView = CType(sender, TreeView)
            'ドロップされたデータ(TreeNode)を取得
            Dim [source] As TreeNode = _
                CType(e.Data.GetData(GetType(TreeNode)), TreeNode)
            'ドロップ先のTreeNodeを取得する
            Dim target As TreeNode = _
                tv.GetNodeAt(tv.PointToClient(New Point(e.X, e.Y)))
            'マウス下のNodeがドロップ先として適切か調べる
            If Not target Is Nothing AndAlso _
                target.Level = [source].Level AndAlso _
                Not target Is [source] AndAlso _
                Not IsChildNode([source], target) And
        ToolStripButton1.Enabled = True Then
                'ドロップされたNodeのコピーを作成
                Dim cln As TreeNode = CType([source].Clone(), TreeNode)
                'Nodeを追加
                If target.Index < [source].Index Then
                    target.Parent.Nodes.Insert(target.Index, cln)
                Else
                    target.Parent.Nodes.Insert(target.Index + 1, cln)
                End If
                If e.Effect = DragDropEffects.Move Then
                    [source].Remove()
                End If
                '追加されたNodeを選択
                tv.SelectedNode = cln
            Else
                e.Effect = DragDropEffects.None
            End If
        Else
            e.Effect = DragDropEffects.None
        End If
    End Sub

    '禁止状態
    Private Shared Function IsChildNode( _
            ByVal parentNode As TreeNode, _
            ByVal childNode As TreeNode) As Boolean
        If Not childNode.Parent Is parentNode.Parent Then
            Return True
        ElseIf childNode.Parent Is parentNode Then
            Return True 'IsChildNode(parentNode, childNode.Parent)
        Else
            Return False
        End If
    End Function



#End Region

    'パッドボタン
#Region "Joker code procedures"

    Private Sub button_list_DoubleClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles button_list.DoubleClick

        ' For some reason, when clicking quickly on the checkbox list it will not fire 
        ' off the SelectedIndexChanged event  so this is used to capture any changes 
        ' when the user clicks on the control quickly.

        Dim x As Integer = 0
        Dim proceed As Boolean = False
        Dim j As New joker

        If cl_tb.Text.Trim.Length >= 21 Then ' If the code text box contains at least one code or more

            For x = 0 To 19  ' Check if any joker buttons were selected
                If button_list.GetItemChecked(x) = True Then
                    proceed = True
                    Exit For ' No need to continue since we know something is checked
                End If
            Next

        End If

        If proceed = True Then ' If a joker was selected, calculate the code
            j.add_joker()
        Else ' If not, remove any jokers if they exist
            'j.remove_joker()
        End If

    End Sub

    Private Sub inverse_chk_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles inverse_chk.CheckedChanged

        Dim x As Integer = 0
        Dim proceed As Boolean = False
        Dim j As New joker

        If cl_tb.Text.Trim.Length >= 21 Then ' If the code text box contains at least one code or more

            For x = 0 To 19  ' Check if any joker buttons were selected
                If button_list.GetItemChecked(x) = True Then
                    proceed = True
                End If
            Next

        End If

        If proceed = True Then ' If a joker was selected, calculate the code
            j.add_joker()
        Else ' If not, remove any jokers if they exist
            'j.remove_joker()
        End If

    End Sub

    Private Sub button_list_ItemCheck(ByVal sender As System.Object, ByVal e As System.Windows.Forms.ItemCheckEventArgs) Handles button_list.ItemCheck


        ' Restricts the amount of checked items to 3 since CWcheat 
        ' only supports a 3 button press combination for joker codes

        If button_list.CheckedItems.Count >= 3 Then

            e.NewValue = CheckState.Unchecked

        End If

    End Sub

#End Region

#Region "Window control"

    ' This makes sure the error list window always ends up below the main window
    Private Sub Main_locationchanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.LocationChanged

        If error_window.Visible = True Then

            Dim point As New Point
            point.X = Me.Location.X
            point.Y = Me.Location.Y + Me.Height
            error_window.Location = point

        End If

    End Sub

    Private Sub Main_resized(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Resize

        If error_window.Visible = True Then

            Dim point As New Point
            error_window.Width = Me.Width
            point.X = Me.Location.X
            point.Y = Me.Location.Y + Me.Height
            error_window.Location = point

        End If

    End Sub

#End Region

#Region "Hotkeys"

    Private Sub main_key_down(ByVal sender As Object, ByVal e As KeyEventArgs) Handles MyBase.KeyDown

        ' CTRL + V
        If e.Control = True AndAlso e.KeyCode = Keys.V Then
            'to do
        End If

        ' CTRL + C
        If e.Control = True AndAlso e.KeyCode = Keys.C Then
            'to do
        End If

    End Sub

#End Region

    'リスト連携
#Region "LISTVIEW"
    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Dim f As New list
        Dim backup As String = cmt_tb.Text
        Dim line As Integer = 1
        Dim type As String = Nothing
        Dim bit As Integer = 1
        Dim lslen As Integer = 23
        Dim rmlen As Integer = 0
        Dim i As Integer = 0
        Dim z As Integer = 0
        Dim truelist As Boolean = True
        Dim b3 As String = cl_tb.Text
        Dim r As New System.Text.RegularExpressions.Regex( _
"LIST/.+\.txt\((A|V),([1-9]|[1-9][0-9]),[1-8],[1-8]\)", _
System.Text.RegularExpressions.RegexOptions.IgnoreCase)
        Dim m As System.Text.RegularExpressions.Match = r.Match(backup)
        Dim len As Integer = 20
        If PSX = True Then
            lslen = 15
            len = 13
        End If

        While m.Success
            Dim b1 As String = m.Value
            b1 = b1.Substring(b1.Length - 9, 9)
            If b1.Substring(0, 1) = "," Then
                b1 = b1.Remove(0, 1)
            End If
            i = 0
            Dim b2 As String() = b1.Split(CChar(","c))
            For Each s In b2
                Select Case i
                    Case 0
                        type = s.Substring(s.Length - 1, 1)
                    Case 1
                        s = s.Replace(",", "")
                        line = CType(s, Integer)
                    Case 2
                        s = s.Substring(0, 1)
                        bit = CType(s, Integer)
                    Case 3
                        s = s.Substring(0, 1)
                        rmlen = CType(s, Integer)
                End Select
                i += 1
            Next
            If type = "V" Then
                i = 11
                If PSX = True Then
                    i = 7
                End If
            Else
                i = 0
            End If

            m = m.NextMatch()
            z += 1
            i += (line - 1) * lslen + bit + 1
            If PSX = False Then
                If truelist = True AndAlso i + rmlen < b3.Length AndAlso rmlen + bit <= 9 Then
                    truelist = True
                Else
                    truelist = False
                End If
            ElseIf PSX = True Then
                If type = "A" AndAlso i + rmlen < b3.Length AndAlso rmlen + bit <= 9 Then
                    truelist = True
                ElseIf type = "V" AndAlso i + rmlen < b3.Length AndAlso rmlen + bit <= 5 Then
                    truelist = True
                Else
                    truelist = False
                End If
            End If
        End While

        If truelist = True And changed.Text <> "コード内容が変更されました。" And backup <> Nothing And z > 0 Then
            f.ShowDialog(Me)
            f.Dispose()
        ElseIf cl_tb.Text.Length < len Then
            changed.Text = "コード内容が空か文字数が足りません。"
        Else
            changed.Text = "リスト定義がないか,範囲が異常です。"
        End If
        cmt_tb.Text = backup
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        Dim len As Integer = 20
        If PSX = True Then
            Exit Sub
        End If
        If cl_tb.Text.Length > len Then
            changed.Text = "簡易シフトが追加されました,行を合わせてください。"
            Dim z As String = "0"
            If cmt_tb.Text.Contains("840") Then
                z = "8"
            End If

            cmt_tb.Text = "LIST/shift" & z & ".txt" & "(V,1,6,3)シフト倍" & vbCrLf & cmt_tb.Text
        Else
            changed.Text = "コード内容が空か文字数が足りません。"
        End If
    End Sub

    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click

        Dim len As Integer = 20
        If PSX = True Then
            len = 12
        End If
        Dim ofd As New OpenFileDialog()
        Dim lspath As String = Nothing
        ofd.InitialDirectory = My.Application.Info.DirectoryPath.ToString() & "\LIST\"
        ofd.Filter = _
    "txtファイル(*.txt)|*.txt"
        ofd.Title = "追加するリストのTXTを選んでください"
        If cl_tb.Text.Length > len Then
            If ofd.ShowDialog() = DialogResult.OK Then
                changed.Text = "リストが追加されました,行を合わせてください。"
                lspath = ofd.FileName
                lspath = lspath.Replace(My.Application.Info.DirectoryPath.ToString(), "")
                lspath = lspath.Replace("\", "/")
                lspath = lspath.Remove(0, 1)
                cmt_tb.Text = lspath & "(V,1,1,8)" & vbCrLf & cmt_tb.Text
            End If
        Else
            changed.Text = "コード内容が空か文字数が足りません。"
        End If
    End Sub
#End Region

    '保存警告
#Region "ALERTTXT"
    Private Sub RadioButton4_clicked(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadioButton4.Click
        changed.Text = "コード形式が変更されました。"
    End Sub
    Private Sub RadioButton5_clicked(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadioButton5.Click
        changed.Text = "コード形式が変更されました。"
    End Sub

    Private Sub RadioButton6_clicked(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadioButton6.Click
        changed.Text = "コード形式が変更されました。"
    End Sub

    Private Sub GT_tb_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles GT_tb.KeyPress
        changed.Text = "タイトル/IDが変更されました。"
    End Sub

    Private Sub GID_tb_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles GID_tb.KeyPress
        changed.Text = "タイトル/IDが変更されました。"
    End Sub

    Private Sub CT_tb_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CT_tb.KeyPress
        changed.Text = "コード名が変更されました。"
    End Sub

    Private Sub cl_tb_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cl_tb.KeyPress
        changed.Text = "コード内容が変更されました。"
    End Sub

    Private Sub cmt_tb_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmt_tb.KeyPress
        changed.Text = "コードコメントが変更されました。"
    End Sub

    Private Sub on_rd_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles on_rd.Click
        changed.Text = "コード実行状態が変更されました。"
    End Sub

    Private Sub off_rd_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles off_rd.Click
        changed.Text = "コード実行状態が変更されました。"
    End Sub
#End Region
    'ぐっりど値えｄぃた
    Private Sub Button4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button4.Click
        Dim f As New Form1
        f.ShowDialog(Me)
        f.Dispose()
    End Sub
    '隠し
    Private Sub G有効ToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

        If Button4.Visible = False Then
            Button4.Visible = True
            grided_use.Checked = True
        Else
            Button4.Visible = False
            grided_use.Checked = False
        End If
        My.Settings.gridvalueedit = Button4.Visible

    End Sub

    Private Sub フォーム固定ToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles fixedform.Click

        If fixedform.Checked = True Then
            My.Settings.fixedform = False
            fixedform.Checked = False
        Else
            My.Settings.fixedform = True
            fixedform.Checked = True
        End If
    End Sub

    Private Sub 保存時最終コードパスを更新ToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles update_save_filepass.Click

        If update_save_filepass.Checked = True Then
            My.Settings.codepathwhensave = False
            update_save_filepass.Checked = False
        Else
            My.Settings.codepathwhensave = True
            update_save_filepass.Checked = True
        End If
    End Sub
End Class