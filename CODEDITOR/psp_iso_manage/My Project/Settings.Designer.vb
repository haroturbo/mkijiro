﻿'------------------------------------------------------------------------------
' <auto-generated>
'     このコードはツールによって生成されました。
'     ランタイム バージョン:4.0.30319.239
'
'     このファイルへの変更は、以下の状況下で不正な動作の原因になったり、
'     コードが再生成されるときに損失したりします。
' </auto-generated>
'------------------------------------------------------------------------------

Option Strict On
Option Explicit On


Namespace My
    
    <Global.System.Runtime.CompilerServices.CompilerGeneratedAttribute(),  _
     Global.System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.Editors.SettingsDesigner.SettingsSingleFileGenerator", "10.0.0.0"),  _
     Global.System.ComponentModel.EditorBrowsableAttribute(Global.System.ComponentModel.EditorBrowsableState.Advanced)>  _
    Partial Friend NotInheritable Class MySettings
        Inherits Global.System.Configuration.ApplicationSettingsBase
        
        Private Shared defaultInstance As MySettings = CType(Global.System.Configuration.ApplicationSettingsBase.Synchronized(New MySettings()),MySettings)
        
#Region "My.Settings 自動保存機能"
#If _MyType = "WindowsForms" Then
    Private Shared addedHandler As Boolean

    Private Shared addedHandlerLockObject As New Object

    <Global.System.Diagnostics.DebuggerNonUserCodeAttribute(), Global.System.ComponentModel.EditorBrowsableAttribute(Global.System.ComponentModel.EditorBrowsableState.Advanced)> _
    Private Shared Sub AutoSaveSettings(ByVal sender As Global.System.Object, ByVal e As Global.System.EventArgs)
        If My.Application.SaveMySettingsOnExit Then
            My.Settings.Save()
        End If
    End Sub
#End If
#End Region
        
        Public Shared ReadOnly Property [Default]() As MySettings
            Get
                
#If _MyType = "WindowsForms" Then
               If Not addedHandler Then
                    SyncLock addedHandlerLockObject
                        If Not addedHandler Then
                            AddHandler My.Application.Shutdown, AddressOf AutoSaveSettings
                            addedHandler = True
                        End If
                    End SyncLock
                End If
#End If
                Return defaultInstance
            End Get
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("D:")>  _
        Public Property drivepath() As String
            Get
                Return CType(Me("drivepath"),String)
            End Get
            Set
                Me("drivepath") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("")>  _
        Public Property isobase() As String
            Get
                Return CType(Me("isobase"),String)
            End Get
            Set
                Me("isobase") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("False")>  _
        Public Property hbhash() As Boolean
            Get
                Return CType(Me("hbhash"),Boolean)
            End Get
            Set
                Me("hbhash") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("False")>  _
        Public Property topmost() As Boolean
            Get
                Return CType(Me("topmost"),Boolean)
            End Get
            Set
                Me("topmost") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("")>  _
        Public Property xml() As String
            Get
                Return CType(Me("xml"),String)
            End Get
            Set
                Me("xml") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("")>  _
        Public Property imgdir() As String
            Get
                Return CType(Me("imgdir"),String)
            End Get
            Set
                Me("imgdir") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("False")>  _
        Public Property lockdrive() As Boolean
            Get
                Return CType(Me("lockdrive"),Boolean)
            End Get
            Set
                Me("lockdrive") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("False")>  _
        Public Property pspgid() As Boolean
            Get
                Return CType(Me("pspgid"),Boolean)
            End Get
            Set
                Me("pspgid") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("X:\ISO\"&Global.Microsoft.VisualBasic.ChrW(13)&Global.Microsoft.VisualBasic.ChrW(10)&"X:\ISO\VIDEO\"&Global.Microsoft.VisualBasic.ChrW(13)&Global.Microsoft.VisualBasic.ChrW(10)&"X:\PSP\GAME\"&Global.Microsoft.VisualBasic.ChrW(13)&Global.Microsoft.VisualBasic.ChrW(10)&"X:\PSP\GAME\CAT_ISO\"&Global.Microsoft.VisualBasic.ChrW(13)&Global.Microsoft.VisualBasic.ChrW(10)&"X:\PSP\GAME\CAT_GAME\"& _ 
            ""&Global.Microsoft.VisualBasic.ChrW(13)&Global.Microsoft.VisualBasic.ChrW(10)&"X:\PSP\GAME\CAT_PSX\"&Global.Microsoft.VisualBasic.ChrW(13)&Global.Microsoft.VisualBasic.ChrW(10)&"X:\PSP\GAME\CAT_APP\"&Global.Microsoft.VisualBasic.ChrW(13)&Global.Microsoft.VisualBasic.ChrW(10)&"X:\PSP\GAME\CAT_TOOL\"&Global.Microsoft.VisualBasic.ChrW(13)&Global.Microsoft.VisualBasic.ChrW(10)&"X:\PSP\GAME"& _ 
            "\CAT_HOMEBREW\"&Global.Microsoft.VisualBasic.ChrW(13)&Global.Microsoft.VisualBasic.ChrW(10)&"X:\PSP\GAME\UPDATE\"&Global.Microsoft.VisualBasic.ChrW(13)&Global.Microsoft.VisualBasic.ChrW(10)&"X:\PSP\GAME150\"&Global.Microsoft.VisualBasic.ChrW(13)&Global.Microsoft.VisualBasic.ChrW(10)&"X:\PSP\GAME151\"&Global.Microsoft.VisualBasic.ChrW(13)&Global.Microsoft.VisualBasic.ChrW(10)&"X:\PSP\GA"& _ 
            "ME152\"&Global.Microsoft.VisualBasic.ChrW(13)&Global.Microsoft.VisualBasic.ChrW(10)&"X:\PSP\GAME200\"&Global.Microsoft.VisualBasic.ChrW(13)&Global.Microsoft.VisualBasic.ChrW(10)&"X:\PSP\GAME201\"&Global.Microsoft.VisualBasic.ChrW(13)&Global.Microsoft.VisualBasic.ChrW(10)&"X:\PSP\GAME250\"&Global.Microsoft.VisualBasic.ChrW(13)&Global.Microsoft.VisualBasic.ChrW(10)&"X:\PSP\GAME270\"&Global.Microsoft.VisualBasic.ChrW(13)&Global.Microsoft.VisualBasic.ChrW(10)&"X:\P"& _ 
            "SP\GAME271\"&Global.Microsoft.VisualBasic.ChrW(13)&Global.Microsoft.VisualBasic.ChrW(10)&"X:\PSP\GAME280\"&Global.Microsoft.VisualBasic.ChrW(13)&Global.Microsoft.VisualBasic.ChrW(10)&"X:\PSP\GAME281\"&Global.Microsoft.VisualBasic.ChrW(13)&Global.Microsoft.VisualBasic.ChrW(10)&"X:\PSP\GAME282\"&Global.Microsoft.VisualBasic.ChrW(13)&Global.Microsoft.VisualBasic.ChrW(10)&"X:\PSP\GAME300\"&Global.Microsoft.VisualBasic.ChrW(13)&Global.Microsoft.VisualBasic.ChrW(10)&"X:\PSP\GAME301\"&Global.Microsoft.VisualBasic.ChrW(13)&Global.Microsoft.VisualBasic.ChrW(10)&"X:\PSP\GAME302\"&Global.Microsoft.VisualBasic.ChrW(13)&Global.Microsoft.VisualBasic.ChrW(10)&"X:\PSP\GAME310\"&Global.Microsoft.VisualBasic.ChrW(13)&Global.Microsoft.VisualBasic.ChrW(10)&"X:\PSP\GAME311\"&Global.Microsoft.VisualBasic.ChrW(13)&Global.Microsoft.VisualBasic.ChrW(10)&"X:\PSP\GAME"& _ 
            "330\"&Global.Microsoft.VisualBasic.ChrW(13)&Global.Microsoft.VisualBasic.ChrW(10)&"X:\PSP\GAME340\"&Global.Microsoft.VisualBasic.ChrW(13)&Global.Microsoft.VisualBasic.ChrW(10)&"X:\PSP\GAME350\"&Global.Microsoft.VisualBasic.ChrW(13)&Global.Microsoft.VisualBasic.ChrW(10)&"X:\PSP\GAME351\"&Global.Microsoft.VisualBasic.ChrW(13)&Global.Microsoft.VisualBasic.ChrW(10)&"X:\PSP\GAME352\"&Global.Microsoft.VisualBasic.ChrW(13)&Global.Microsoft.VisualBasic.ChrW(10)&"X:\PSP"& _ 
            "\GAME370\"&Global.Microsoft.VisualBasic.ChrW(13)&Global.Microsoft.VisualBasic.ChrW(10)&"X:\PSP\GAME371\"&Global.Microsoft.VisualBasic.ChrW(13)&Global.Microsoft.VisualBasic.ChrW(10)&"X:\PSP\GAME372\"&Global.Microsoft.VisualBasic.ChrW(13)&Global.Microsoft.VisualBasic.ChrW(10)&"X:\PSP\GAME380\"&Global.Microsoft.VisualBasic.ChrW(13)&Global.Microsoft.VisualBasic.ChrW(10)&"X:\PSP\GAME390\"&Global.Microsoft.VisualBasic.ChrW(13)&Global.Microsoft.VisualBasic.ChrW(10)&"X"& _ 
            ":\PSP\GAME393\"&Global.Microsoft.VisualBasic.ChrW(13)&Global.Microsoft.VisualBasic.ChrW(10)&"X:\PSP\GAME395\"&Global.Microsoft.VisualBasic.ChrW(13)&Global.Microsoft.VisualBasic.ChrW(10)&"X:\PSP\GAME400\"&Global.Microsoft.VisualBasic.ChrW(13)&Global.Microsoft.VisualBasic.ChrW(10)&"X:\PSP\GAME403\"&Global.Microsoft.VisualBasic.ChrW(13)&Global.Microsoft.VisualBasic.ChrW(10)&"X:\PSP\GAME40"& _ 
            "5\"&Global.Microsoft.VisualBasic.ChrW(13)&Global.Microsoft.VisualBasic.ChrW(10)&"X:\PSP\GAME500\"&Global.Microsoft.VisualBasic.ChrW(13)&Global.Microsoft.VisualBasic.ChrW(10)&"X:\PSP\GAME501\"&Global.Microsoft.VisualBasic.ChrW(13)&Global.Microsoft.VisualBasic.ChrW(10)&"X:\PSP\GAME502\"&Global.Microsoft.VisualBasic.ChrW(13)&Global.Microsoft.VisualBasic.ChrW(10)&"X:\PSP\GAME503\"&Global.Microsoft.VisualBasic.ChrW(13)&Global.Microsoft.VisualBasic.ChrW(10)&"X:\PSP\G"& _ 
            "AME550\"&Global.Microsoft.VisualBasic.ChrW(13)&Global.Microsoft.VisualBasic.ChrW(10)&"X:\PSP\GAME551\"&Global.Microsoft.VisualBasic.ChrW(13)&Global.Microsoft.VisualBasic.ChrW(10)&"X:\PSP\GAME610\"&Global.Microsoft.VisualBasic.ChrW(13)&Global.Microsoft.VisualBasic.ChrW(10)&"X:\PSP\GAME620\"&Global.Microsoft.VisualBasic.ChrW(13)&Global.Microsoft.VisualBasic.ChrW(10)&"X:\PSP\GAME630\"&Global.Microsoft.VisualBasic.ChrW(13)&Global.Microsoft.VisualBasic.ChrW(10)&"X:\"& _ 
            "PSP\GAME631\"&Global.Microsoft.VisualBasic.ChrW(13)&Global.Microsoft.VisualBasic.ChrW(10)&"X:\PSP\GAME635\"&Global.Microsoft.VisualBasic.ChrW(13)&Global.Microsoft.VisualBasic.ChrW(10)&"X:\PSP\GAME637\"&Global.Microsoft.VisualBasic.ChrW(13)&Global.Microsoft.VisualBasic.ChrW(10)&"X:\PSP\GAME638\"&Global.Microsoft.VisualBasic.ChrW(13)&Global.Microsoft.VisualBasic.ChrW(10)&"X:\PSP\GAME639\"& _ 
            ""&Global.Microsoft.VisualBasic.ChrW(13)&Global.Microsoft.VisualBasic.ChrW(10)&"X:\PSP\GAME660\")>  _
        Public Property instdir() As String
            Get
                Return CType(Me("instdir"),String)
            End Get
            Set
                Me("instdir") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("")>  _
        Public Property imgbase() As String
            Get
                Return CType(Me("imgbase"),String)
            End Get
            Set
                Me("imgbase") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("")>  _
        Public Property pspinsdir() As String
            Get
                Return CType(Me("pspinsdir"),String)
            End Get
            Set
                Me("pspinsdir") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("")>  _
        Public Property datpath() As String
            Get
                Return CType(Me("datpath"),String)
            End Get
            Set
                Me("datpath") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("2")>  _
        Public Property rename() As Integer
            Get
                Return CType(Me("rename"),Integer)
            End Get
            Set
                Me("rename") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("False")>  _
        Public Property diskver() As Boolean
            Get
                Return CType(Me("diskver"),Boolean)
            End Get
            Set
                Me("diskver") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("False")>  _
        Public Property edit() As Boolean
            Get
                Return CType(Me("edit"),Boolean)
            End Get
            Set
                Me("edit") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("True")>  _
        Public Property alwayssave() As Boolean
            Get
                Return CType(Me("alwayssave"),Boolean)
            End Get
            Set
                Me("alwayssave") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("False")>  _
        Public Property romcode() As Boolean
            Get
                Return CType(Me("romcode"),Boolean)
            End Get
            Set
                Me("romcode") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("1")>  _
        Public Property xmlindex() As Integer
            Get
                Return CType(Me("xmlindex"),Integer)
            End Get
            Set
                Me("xmlindex") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("1800")>  _
        Public Property sabunindex() As Integer
            Get
                Return CType(Me("sabunindex"),Integer)
            End Get
            Set
                Me("sabunindex") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("</games>"&Global.Microsoft.VisualBasic.ChrW(13)&Global.Microsoft.VisualBasic.ChrW(10)&Global.Microsoft.VisualBasic.ChrW(9)&"<gui>"&Global.Microsoft.VisualBasic.ChrW(13)&Global.Microsoft.VisualBasic.ChrW(10)&Global.Microsoft.VisualBasic.ChrW(9)&Global.Microsoft.VisualBasic.ChrW(9)&"<images width=""431"" height=""183"">"&Global.Microsoft.VisualBasic.ChrW(13)&Global.Microsoft.VisualBasic.ChrW(10)&Global.Microsoft.VisualBasic.ChrW(9)&Global.Microsoft.VisualBasic.ChrW(9)&Global.Microsoft.VisualBasic.ChrW(9)&"<image x=""0"" y=""0"" widt"& _ 
            "h=""104"" height=""181""/>"&Global.Microsoft.VisualBasic.ChrW(13)&Global.Microsoft.VisualBasic.ChrW(10)&Global.Microsoft.VisualBasic.ChrW(9)&Global.Microsoft.VisualBasic.ChrW(9)&Global.Microsoft.VisualBasic.ChrW(9)&"<image x=""109"" y=""0"" width=""320"" height=""181""/>"&Global.Microsoft.VisualBasic.ChrW(13)&Global.Microsoft.VisualBasic.ChrW(10)&Global.Microsoft.VisualBasic.ChrW(9)&Global.Microsoft.VisualBasic.ChrW(9)&"</"& _ 
            "images>"&Global.Microsoft.VisualBasic.ChrW(13)&Global.Microsoft.VisualBasic.ChrW(10)&Global.Microsoft.VisualBasic.ChrW(9)&"</gui>"&Global.Microsoft.VisualBasic.ChrW(13)&Global.Microsoft.VisualBasic.ChrW(10)&"</dat>")>  _
        Public Property xmlfoot() As String
            Get
                Return CType(Me("xmlfoot"),String)
            End Get
            Set
                Me("xmlfoot") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("<?xml version=""1.0"" encoding=""UTF-8"" standalone=""no""?>"&Global.Microsoft.VisualBasic.ChrW(13)&Global.Microsoft.VisualBasic.ChrW(10)&"<dat xmlns:xsi=""http://ww"& _ 
            "w.w3.org/2001/XMLSchema-instance"" xsi:noNamespaceSchemaLocation=""datas.xsd"">"&Global.Microsoft.VisualBasic.ChrW(13)&Global.Microsoft.VisualBasic.ChrW(10)&Global.Microsoft.VisualBasic.ChrW(9)&"<"& _ 
            "configuration>"&Global.Microsoft.VisualBasic.ChrW(13)&Global.Microsoft.VisualBasic.ChrW(10)&Global.Microsoft.VisualBasic.ChrW(9)&Global.Microsoft.VisualBasic.ChrW(9)&"<datName>Sony PSP Collection USER</datName>"&Global.Microsoft.VisualBasic.ChrW(13)&Global.Microsoft.VisualBasic.ChrW(10)&Global.Microsoft.VisualBasic.ChrW(9)&Global.Microsoft.VisualBasic.ChrW(9)&"<imFolder>Sony "& _ 
            "PSP Collection USER</imFolder>"&Global.Microsoft.VisualBasic.ChrW(13)&Global.Microsoft.VisualBasic.ChrW(10)&Global.Microsoft.VisualBasic.ChrW(9)&Global.Microsoft.VisualBasic.ChrW(9)&"<datVersion>9999</datVersion>"&Global.Microsoft.VisualBasic.ChrW(13)&Global.Microsoft.VisualBasic.ChrW(10)&Global.Microsoft.VisualBasic.ChrW(9)&Global.Microsoft.VisualBasic.ChrW(9)&"<system>Sony "& _ 
            "PSP</system>"&Global.Microsoft.VisualBasic.ChrW(13)&Global.Microsoft.VisualBasic.ChrW(10)&Global.Microsoft.VisualBasic.ChrW(9)&Global.Microsoft.VisualBasic.ChrW(9)&"<screenshotsWidth>320</screenshotsWidth>"&Global.Microsoft.VisualBasic.ChrW(13)&Global.Microsoft.VisualBasic.ChrW(10)&Global.Microsoft.VisualBasic.ChrW(9)&Global.Microsoft.VisualBasic.ChrW(9)&"<screenshotsHeight>1"& _ 
            "81</screenshotsHeight>"&Global.Microsoft.VisualBasic.ChrW(13)&Global.Microsoft.VisualBasic.ChrW(10)&Global.Microsoft.VisualBasic.ChrW(9)&Global.Microsoft.VisualBasic.ChrW(9)&"<infos>"&Global.Microsoft.VisualBasic.ChrW(13)&Global.Microsoft.VisualBasic.ChrW(10)&Global.Microsoft.VisualBasic.ChrW(9)&Global.Microsoft.VisualBasic.ChrW(9)&Global.Microsoft.VisualBasic.ChrW(9)&"<releaseNumber visible=""true"" inNamingOpti"& _ 
            "on=""true"" default=""false""/>"&Global.Microsoft.VisualBasic.ChrW(13)&Global.Microsoft.VisualBasic.ChrW(10)&Global.Microsoft.VisualBasic.ChrW(9)&Global.Microsoft.VisualBasic.ChrW(9)&Global.Microsoft.VisualBasic.ChrW(9)&"<title visible=""false"" inNamingOption=""true"" def"& _ 
            "ault=""false""/>"&Global.Microsoft.VisualBasic.ChrW(13)&Global.Microsoft.VisualBasic.ChrW(10)&Global.Microsoft.VisualBasic.ChrW(9)&Global.Microsoft.VisualBasic.ChrW(9)&Global.Microsoft.VisualBasic.ChrW(9)&"<saveType visible=""true"" inNamingOption=""true"" default=""true"""& _ 
            "/>"&Global.Microsoft.VisualBasic.ChrW(13)&Global.Microsoft.VisualBasic.ChrW(10)&Global.Microsoft.VisualBasic.ChrW(9)&Global.Microsoft.VisualBasic.ChrW(9)&Global.Microsoft.VisualBasic.ChrW(9)&"<publisher visible=""true"" inNamingOption=""true"" default=""true""/>"&Global.Microsoft.VisualBasic.ChrW(13)&Global.Microsoft.VisualBasic.ChrW(10)&Global.Microsoft.VisualBasic.ChrW(9)&Global.Microsoft.VisualBasic.ChrW(9)&Global.Microsoft.VisualBasic.ChrW(9)&"<sou"& _ 
            "rceRom visible=""true"" inNamingOption=""true"" default=""true""/>"&Global.Microsoft.VisualBasic.ChrW(13)&Global.Microsoft.VisualBasic.ChrW(10)&Global.Microsoft.VisualBasic.ChrW(9)&Global.Microsoft.VisualBasic.ChrW(9)&Global.Microsoft.VisualBasic.ChrW(9)&"<romSize visibl"& _ 
            "e=""true"" inNamingOption=""true"" default=""true""/>"&Global.Microsoft.VisualBasic.ChrW(13)&Global.Microsoft.VisualBasic.ChrW(10)&Global.Microsoft.VisualBasic.ChrW(9)&Global.Microsoft.VisualBasic.ChrW(9)&Global.Microsoft.VisualBasic.ChrW(9)&"<romCRC visible=""true"" inNam"& _ 
            "ingOption=""true"" default=""true""/>"&Global.Microsoft.VisualBasic.ChrW(13)&Global.Microsoft.VisualBasic.ChrW(10)&Global.Microsoft.VisualBasic.ChrW(9)&Global.Microsoft.VisualBasic.ChrW(9)&Global.Microsoft.VisualBasic.ChrW(9)&"<im1CRC visible=""true"" inNamingOption=""fal"& _ 
            "se"" default=""true""/>"&Global.Microsoft.VisualBasic.ChrW(13)&Global.Microsoft.VisualBasic.ChrW(10)&Global.Microsoft.VisualBasic.ChrW(9)&Global.Microsoft.VisualBasic.ChrW(9)&Global.Microsoft.VisualBasic.ChrW(9)&"<im2CRC visible=""true"" inNamingOption=""false"" default="""& _ 
            "true""/>"&Global.Microsoft.VisualBasic.ChrW(13)&Global.Microsoft.VisualBasic.ChrW(10)&Global.Microsoft.VisualBasic.ChrW(9)&Global.Microsoft.VisualBasic.ChrW(9)&Global.Microsoft.VisualBasic.ChrW(9)&"<comment visible=""true"" inNamingOption=""true"" default=""true""/>"&Global.Microsoft.VisualBasic.ChrW(13)&Global.Microsoft.VisualBasic.ChrW(10)&Global.Microsoft.VisualBasic.ChrW(9)&Global.Microsoft.VisualBasic.ChrW(9)&Global.Microsoft.VisualBasic.ChrW(9)&"<"& _ 
            "languages visible=""true"" inNamingOption=""true"" default=""false""/>"&Global.Microsoft.VisualBasic.ChrW(13)&Global.Microsoft.VisualBasic.ChrW(10)&Global.Microsoft.VisualBasic.ChrW(9)&Global.Microsoft.VisualBasic.ChrW(9)&Global.Microsoft.VisualBasic.ChrW(9)&"<languageNu"& _ 
            "mber visible=""true"" inNamingOption=""true"" default=""false""/>"&Global.Microsoft.VisualBasic.ChrW(13)&Global.Microsoft.VisualBasic.ChrW(10)&Global.Microsoft.VisualBasic.ChrW(9)&Global.Microsoft.VisualBasic.ChrW(9)&"</infos>"&Global.Microsoft.VisualBasic.ChrW(13)&Global.Microsoft.VisualBasic.ChrW(10)&Global.Microsoft.VisualBasic.ChrW(9)&Global.Microsoft.VisualBasic.ChrW(9)&"<canO"& _ 
            "pen>"&Global.Microsoft.VisualBasic.ChrW(13)&Global.Microsoft.VisualBasic.ChrW(10)&Global.Microsoft.VisualBasic.ChrW(9)&Global.Microsoft.VisualBasic.ChrW(9)&Global.Microsoft.VisualBasic.ChrW(9)&"<extension>.iso</extension>"&Global.Microsoft.VisualBasic.ChrW(13)&Global.Microsoft.VisualBasic.ChrW(10)&Global.Microsoft.VisualBasic.ChrW(9)&Global.Microsoft.VisualBasic.ChrW(9)&"</canOpen>"&Global.Microsoft.VisualBasic.ChrW(13)&Global.Microsoft.VisualBasic.ChrW(10)&Global.Microsoft.VisualBasic.ChrW(9)&Global.Microsoft.VisualBasic.ChrW(9)&"<newDat>"&Global.Microsoft.VisualBasic.ChrW(13)&Global.Microsoft.VisualBasic.ChrW(10)&" "&Global.Microsoft.VisualBasic.ChrW(9)&Global.Microsoft.VisualBasic.ChrW(9)&Global.Microsoft.VisualBasic.ChrW(9)&"<datVersionU"& _ 
            "RL>http://</datVersionURL>"&Global.Microsoft.VisualBasic.ChrW(13)&Global.Microsoft.VisualBasic.ChrW(10)&Global.Microsoft.VisualBasic.ChrW(9)&Global.Microsoft.VisualBasic.ChrW(9)&Global.Microsoft.VisualBasic.ChrW(9)&"<datURL fileName=""PSP_List.zip"">http://</datURL>"&Global.Microsoft.VisualBasic.ChrW(13)&Global.Microsoft.VisualBasic.ChrW(10)&Global.Microsoft.VisualBasic.ChrW(9)&Global.Microsoft.VisualBasic.ChrW(9)&Global.Microsoft.VisualBasic.ChrW(9)&"<imURL>http://</imURL>"&Global.Microsoft.VisualBasic.ChrW(13)&Global.Microsoft.VisualBasic.ChrW(10)&Global.Microsoft.VisualBasic.ChrW(9)&Global.Microsoft.VisualBasic.ChrW(9)&"</newDat>"&Global.Microsoft.VisualBasic.ChrW(13)&Global.Microsoft.VisualBasic.ChrW(10)&Global.Microsoft.VisualBasic.ChrW(9)&Global.Microsoft.VisualBasic.ChrW(9)&"<search>"&Global.Microsoft.VisualBasic.ChrW(13)&Global.Microsoft.VisualBasic.ChrW(10)&Global.Microsoft.VisualBasic.ChrW(9)&Global.Microsoft.VisualBasic.ChrW(9)&Global.Microsoft.VisualBasic.ChrW(9)&"<to value=""saveType"" def"& _ 
            "ault=""true"" auto=""true""/>"&Global.Microsoft.VisualBasic.ChrW(13)&Global.Microsoft.VisualBasic.ChrW(10)&Global.Microsoft.VisualBasic.ChrW(9)&Global.Microsoft.VisualBasic.ChrW(9)&Global.Microsoft.VisualBasic.ChrW(9)&"<to value=""publisher"" default=""true"" auto=""true""/>"& _ 
            ""&Global.Microsoft.VisualBasic.ChrW(13)&Global.Microsoft.VisualBasic.ChrW(10)&Global.Microsoft.VisualBasic.ChrW(9)&Global.Microsoft.VisualBasic.ChrW(9)&Global.Microsoft.VisualBasic.ChrW(9)&"<to value=""sourceRom"" default=""true"" auto=""true""/>"&Global.Microsoft.VisualBasic.ChrW(13)&Global.Microsoft.VisualBasic.ChrW(10)&Global.Microsoft.VisualBasic.ChrW(9)&Global.Microsoft.VisualBasic.ChrW(9)&"</search>"&Global.Microsoft.VisualBasic.ChrW(13)&Global.Microsoft.VisualBasic.ChrW(10)&Global.Microsoft.VisualBasic.ChrW(9)&Global.Microsoft.VisualBasic.ChrW(9)&"<romTitl"& _ 
            "e>%u - %n</romTitle>"&Global.Microsoft.VisualBasic.ChrW(13)&Global.Microsoft.VisualBasic.ChrW(10)&Global.Microsoft.VisualBasic.ChrW(9)&"</configuration>"&Global.Microsoft.VisualBasic.ChrW(13)&Global.Microsoft.VisualBasic.ChrW(10)&Global.Microsoft.VisualBasic.ChrW(9)&"<games>"&Global.Microsoft.VisualBasic.ChrW(13)&Global.Microsoft.VisualBasic.ChrW(10))>  _
        Public Property xmlhead() As String
            Get
                Return CType(Me("xmlhead"),String)
            End Get
            Set
                Me("xmlhead") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("False")>  _
        Public Property filenamecrc() As Boolean
            Get
                Return CType(Me("filenamecrc"),Boolean)
            End Get
            Set
                Me("filenamecrc") = value
            End Set
        End Property
    End Class
End Namespace

Namespace My
    
    <Global.Microsoft.VisualBasic.HideModuleNameAttribute(),  _
     Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
     Global.System.Runtime.CompilerServices.CompilerGeneratedAttribute()>  _
    Friend Module MySettingsProperty
        
        <Global.System.ComponentModel.Design.HelpKeywordAttribute("My.Settings")>  _
        Friend ReadOnly Property Settings() As Global.umd_rawimage_manger.My.MySettings
            Get
                Return Global.umd_rawimage_manger.My.MySettings.Default
            End Get
        End Property
    End Module
End Namespace
