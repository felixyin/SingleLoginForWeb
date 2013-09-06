Imports System.Net
Imports System.IO
Imports System.Text

''' <summary>
''' 单点登录
''' </summary>
''' <remarks>
''' author：王强/尹彬
''' email:ybkk1027@gmail.com
''' version：1.0
''' 禁止抄袭
''' </remarks>
Public Class SingleLogin

    ''' <summary>
    ''' 单点登陆方法
    ''' </summary>
    ''' <param name="res">页面Response</param>
    ''' <param name="sRegUrl">注册url</param>
    ''' <param name="sLoginUrl">登陆url</param>
    ''' <param name="InfoList">注册参数集合</param>
    ''' <remarks>如有疑问，请联系:ybkk1027@gmail.com(尹工)</remarks>
    Public Shared Sub login(ByVal res As Web.HttpResponse, ByVal sRegUrl As String, ByVal sLoginUrl As String, ByVal InfoList As ArrayList, ByVal target As String)
        '验证用户&注册单点登录 
        Dim sKey As String = registerSingleLogin(sRegUrl, InfoList)
        '单点登录
        signleLogin(res, sLoginUrl, sKey, target)
    End Sub


    ''' <summary>
    ''' 单点登陆方法
    ''' </summary>
    ''' <param name="res">页面Response</param>
    ''' <param name="sRegUrl">注册url</param>
    ''' <param name="sLoginUrl">登陆url</param>
    ''' <param name="InfoList">注册参数集合</param>
    ''' <remarks>如有疑问，请联系:ybkk1027@gmail.com(尹工)</remarks>
    Public Shared Sub loginAsIframe(ByVal res As Web.HttpResponse, ByVal sRegUrl As String, ByVal sLoginUrl As String, ByVal InfoList As ArrayList)
        '验证用户&注册单点登录 
        Dim sKey As String = registerSingleLogin(sRegUrl, InfoList)
        '单点登录
        signleLoginAsIframe(res, sLoginUrl, sKey)
    End Sub


    ''' <summary>
    ''' 向Url传输信息，并Key
    ''' </summary>
    ''' <param name="sUrl">Url</param>
    ''' <param name="InfoList">传输数据</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Shared Function registerSingleLogin(ByVal sUrl As String, ByVal InfoList As ArrayList) As String
        Dim sAction As String = sUrl & "?params="
        'sAction += "fy.com,"
        For Each sItem As String In InfoList
            sAction += sItem & ","
        Next
        'sUrl.Remove(0, sUrl.Length - 1)
        sAction = sAction.Substring(0, sAction.Length - 1)
        Return sendRequest(sAction)
    End Function

    ''' <summary>
    ''' 发送Request请求
    ''' </summary>
    ''' <param name="url">Url</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Shared Function sendRequest(ByVal url As String) As String
        Dim request As HttpWebRequest = CType(WebRequest.Create(url), HttpWebRequest)
        request.Credentials = CredentialCache.DefaultCredentials

        Dim response As HttpWebResponse = CType(request.GetResponse(), HttpWebResponse)
        Dim receiveStream As Stream = response.GetResponseStream()
        Dim readStream As New StreamReader(receiveStream, Encoding.UTF8)

        Dim resultKey As String = readStream.ReadToEnd()
        response.Close()
        readStream.Close()
        Console.WriteLine(resultKey)
        Return resultKey
    End Function

    ''' <summary>
    ''' 单点登陆
    ''' </summary>
    ''' <param name="sUrl">Url</param>
    ''' <param name="sKey">Key</param>
    ''' <param name="target">target</param>
    ''' <remarks></remarks>
    Private Shared Sub signleLogin(ByVal res As Web.HttpResponse, ByVal sUrl As String, ByVal sKey As String, ByVal target As String)
        sKey = sKey.Replace("""", "")
        Dim sHtml As String = ""
        If sKey.Length > 0 Then
            sHtml = "<script type='text/javascript'>" & _
                "window.onload=function(){document.forms[0].submit();}" & _
                "</script>" & _
                "<form action='" & sUrl & "' style='display:none' method='post' ""target='" & target & "'>" & _
                "<input type='hidden' name='key' value='" & sKey & "'/>" & _
                "</form>"
        Else
            sHtml = "<script type='text/javascript'>" & _
                "window.onload=function(){alert('" & "单点登录目标系统返回的key有问题" & "')}" & _
                "</script>"
        End If
        res.Write(sHtml)
        res.Flush()
    End Sub

    ''' <summary>
    ''' 单点登陆 什么也看不到
    ''' </summary>
    ''' <param name="sUrl">Url</param>
    ''' <param name="sKey">Key</param>
    ''' <remarks></remarks>
    Private Shared Sub signleLoginAsIframe(ByVal res As Web.HttpResponse, ByVal sUrl As String, ByVal sKey As String)
        sKey = sKey.Replace("""", "")

        Dim sHtml As String = ""
        If sKey.Length > 0 Then
            sHtml = "<script type='text/javascript'>document.domain='localhost';</script>" & _
                "<iframe src='" & sUrl & "?key=" & sKey & "' frameborder='0' width='100%' height='100%' scrolling='no' marginheight='0' marginwidth='0' style='margin:0px;padding:0px;'></iframe></body></html>"
        Else
            sHtml = "<script type='text/javascript'>" & _
                "window.onload=function(){alert('" & "单点登录目标系统返回的key有问题" & "')}" & _
                "</script>"
        End If
        res.Write(sHtml)
        res.Flush()
    End Sub
End Class
