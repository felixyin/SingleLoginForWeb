Imports System.Net
Imports System.IO
Imports System.Configuration

Public Class _Default
    Inherits System.Web.UI.Page

    Protected Sub LinkButton1_Click(sender As Object, e As EventArgs) Handles LinkButton1.Click

        '1.访问 Signature 传输本系统Session中用户信息, Signature确认用户信息后，生成Key并返回。
        '2.跳转 Signature 并以Key进行登陆Signature。

        '传输本系统用户信息 AccountId,PassWord
        Dim InfoList As ArrayList = New ArrayList()

        'List Add 顺序不能错
        InfoList.Add("0") 'cDocumentType
        InfoList.Add("admin")
        InfoList.Add("a12345")
        InfoList.Add("cCode")

        Dim webRoot As String = System.Configuration.ConfigurationManager.AppSettings("Signature")
        FY.SingleLogin.login(Response, webRoot & "/registerSingleSignon.action", webRoot & "/singleSignon.action", InfoList, "_self")

        'Response.Write("")
    End Sub

    Protected Sub LinkButton2_Click(ByVal sender As Object, ByVal e As EventArgs) Handles LinkButton2.Click
        '1.访问 Signature 传输本系统Session中用户信息, Signature确认用户信息后，生成Key并返回。
        '2.跳转 Signature 并以Key进行登陆Signature。

        '传输本系统用户信息 AccountId,PassWord
        Dim InfoList As ArrayList = New ArrayList()

        'List Add 顺序不能错
        InfoList.Add("0") 'cDocumentType
        InfoList.Add("admin")
        InfoList.Add("a12345")
        InfoList.Add("cCode")

        Dim webRoot As String = System.Configuration.ConfigurationManager.AppSettings("Signature")
        FY.SingleLogin.login(Response, webRoot & "/registerSingleSignon.action", webRoot & "/singleSignon.action", InfoList, "_blank")

        'Response.Write("")
    End Sub
End Class