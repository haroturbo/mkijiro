﻿'------------------------------------------------------------------------------
' <auto-generated>
'     このコードはツールによって生成されました。
'     ランタイム バージョン:4.0.30319.235
'
'     このファイルへの変更は、以下の状況下で不正な動作の原因になったり、
'     コードが再生成されるときに損失したりします。
' </auto-generated>
'------------------------------------------------------------------------------

Option Strict On
Option Explicit On

Imports System

Namespace My.Resources
    
    'このクラスは StronglyTypedResourceBuilder クラスが ResGen
    'または Visual Studio のようなツールを使用して自動生成されました。
    'メンバーを追加または削除するには、.ResX ファイルを編集して、/str オプションと共に
    'ResGen を実行し直すか、または VS プロジェクトをビルドし直します。
    '''<summary>
    '''  ローカライズされた文字列などを検索するための、厳密に型指定されたリソース クラスです。
    '''</summary>
    <Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0"),  _
     Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
     Global.System.Runtime.CompilerServices.CompilerGeneratedAttribute(),  _
     Global.Microsoft.VisualBasic.HideModuleNameAttribute()>  _
    Friend Module Resources
        
        Private resourceMan As Global.System.Resources.ResourceManager
        
        Private resourceCulture As Global.System.Globalization.CultureInfo
        
        '''<summary>
        '''  このクラスで使用されているキャッシュされた ResourceManager インスタンスを返します。
        '''</summary>
        <Global.System.ComponentModel.EditorBrowsableAttribute(Global.System.ComponentModel.EditorBrowsableState.Advanced)>  _
        Friend ReadOnly Property ResourceManager() As Global.System.Resources.ResourceManager
            Get
                If Object.ReferenceEquals(resourceMan, Nothing) Then
                    Dim temp As Global.System.Resources.ResourceManager = New Global.System.Resources.ResourceManager("temparchecker.Resources", GetType(Resources).Assembly)
                    resourceMan = temp
                End If
                Return resourceMan
            End Get
        End Property
        
        '''<summary>
        '''  厳密に型指定されたこのリソース クラスを使用して、すべての検索リソースに対し、
        '''  現在のスレッドの CurrentUICulture プロパティをオーバーライドします。
        '''</summary>
        <Global.System.ComponentModel.EditorBrowsableAttribute(Global.System.ComponentModel.EditorBrowsableState.Advanced)>  _
        Friend Property Culture() As Global.System.Globalization.CultureInfo
            Get
                Return resourceCulture
            End Get
            Set
                resourceCulture = value
            End Set
        End Property
        
        '''<summary>
        '''  前回インストールしたｐｒｘ に類似しているローカライズされた文字列を検索します。
        '''</summary>
        Friend ReadOnly Property s1() As String
            Get
                Return ResourceManager.GetString("s1", resourceCulture)
            End Get
        End Property
        
        '''<summary>
        '''  last installed prx に類似しているローカライズされた文字列を検索します。
        '''</summary>
        Friend ReadOnly Property s1_e() As String
            Get
                Return ResourceManager.GetString("s1_e", resourceCulture)
            End Get
        End Property
        
        '''<summary>
        '''  PSPが見つかりました,temparのダウンロードを開始します に類似しているローカライズされた文字列を検索します。
        '''</summary>
        Friend ReadOnly Property s2() As String
            Get
                Return ResourceManager.GetString("s2", resourceCulture)
            End Get
        End Property
        
        '''<summary>
        '''  found psp,downloading latest tempar に類似しているローカライズされた文字列を検索します。
        '''</summary>
        Friend ReadOnly Property s2_e() As String
            Get
                Return ResourceManager.GetString("s2_e", resourceCulture)
            End Get
        End Property
        
        '''<summary>
        '''  をPSPにコピーしています... に類似しているローカライズされた文字列を検索します。
        '''</summary>
        Friend ReadOnly Property s3() As String
            Get
                Return ResourceManager.GetString("s3", resourceCulture)
            End Get
        End Property
        
        '''<summary>
        '''  copy to psp... に類似しているローカライズされた文字列を検索します。
        '''</summary>
        Friend ReadOnly Property s3_e() As String
            Get
                Return ResourceManager.GetString("s3_e", resourceCulture)
            End Get
        End Property
        
        '''<summary>
        '''  ランゲージファイルをコピーしています に類似しているローカライズされた文字列を検索します。
        '''</summary>
        Friend ReadOnly Property s4() As String
            Get
                Return ResourceManager.GetString("s4", resourceCulture)
            End Get
        End Property
        
        '''<summary>
        '''  copy languagefiles to psp... に類似しているローカライズされた文字列を検索します。
        '''</summary>
        Friend ReadOnly Property s4_e() As String
            Get
                Return ResourceManager.GetString("s4_e", resourceCulture)
            End Get
        End Property
        
        '''<summary>
        '''  インストールが完了しました に類似しているローカライズされた文字列を検索します。
        '''</summary>
        Friend ReadOnly Property s5() As String
            Get
                Return ResourceManager.GetString("s5", resourceCulture)
            End Get
        End Property
        
        '''<summary>
        '''  install completed!! に類似しているローカライズされた文字列を検索します。
        '''</summary>
        Friend ReadOnly Property s5_e() As String
            Get
                Return ResourceManager.GetString("s5_e", resourceCulture)
            End Get
        End Property
        
        '''<summary>
        '''  メモリースティックフォーマット時自動生成されるPSPフォルダとMEMSTICK.INDが見つかりませんでした に類似しているローカライズされた文字列を検索します。
        '''</summary>
        Friend ReadOnly Property s6() As String
            Get
                Return ResourceManager.GetString("s6", resourceCulture)
            End Get
        End Property
        
        '''<summary>
        '''  cannot find PSP directory and hidden file &quot;MEMSTICK.IND&quot; created  with psp-xmb memorystick format menu に類似しているローカライズされた文字列を検索します。
        '''</summary>
        Friend ReadOnly Property s6_e() As String
            Get
                Return ResourceManager.GetString("s6_e", resourceCulture)
            End Get
        End Property
        
        '''<summary>
        '''  隠しファイルMEMSTICK.INDがない場合はメモリースティックのルートに作成してください に類似しているローカライズされた文字列を検索します。
        '''</summary>
        Friend ReadOnly Property s7() As String
            Get
                Return ResourceManager.GetString("s7", resourceCulture)
            End Get
        End Property
        
        '''<summary>
        '''  if there are no MEMSTICK.IND,please make it on memorysitck root directory  に類似しているローカライズされた文字列を検索します。
        '''</summary>
        Friend ReadOnly Property s7_e() As String
            Get
                Return ResourceManager.GetString("s7_e", resourceCulture)
            End Get
        End Property
        
        '''<summary>
        '''  インターネットに接続されてません に類似しているローカライズされた文字列を検索します。
        '''</summary>
        Friend ReadOnly Property s8() As String
            Get
                Return ResourceManager.GetString("s8", resourceCulture)
            End Get
        End Property
        
        '''<summary>
        '''  internet connection not working に類似しているローカライズされた文字列を検索します。
        '''</summary>
        Friend ReadOnly Property s8_e() As String
            Get
                Return ResourceManager.GetString("s8_e", resourceCulture)
            End Get
        End Property
        
        '''<summary>
        '''  がリリースされてます に類似しているローカライズされた文字列を検索します。
        '''</summary>
        Friend ReadOnly Property s9() As String
            Get
                Return ResourceManager.GetString("s9", resourceCulture)
            End Get
        End Property
        
        '''<summary>
        '''   has been released に類似しているローカライズされた文字列を検索します。
        '''</summary>
        Friend ReadOnly Property s9_e() As String
            Get
                Return ResourceManager.GetString("s9_e", resourceCulture)
            End Get
        End Property
    End Module
End Namespace
