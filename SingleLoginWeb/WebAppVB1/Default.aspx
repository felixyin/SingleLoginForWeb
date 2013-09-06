<%@ Page Title="主页" Language="vb" MasterPageFile="~/Site.Master" AutoEventWireup="false"
    CodeBehind="Default.aspx.vb" Inherits="WebAppVB1._Default" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <h2>
        欢迎使用 ASP.NET!
    </h2>
    <p>
        若要了解关于 ASP.NET 的详细信息，请访问 <a href="http://www.asp.net/cn" title="ASP.NET 网站">www.asp.net/cn</a>。
    </p>
    <p>
        您还可以找到 <a href="http://go.microsoft.com/fwlink/?LinkID=152368" title="MSDN ASP.NET 文档">
            MSDN 上有关 ASP.NET 的文档</a>。
    </p>
    <asp:Label ID="Label1" runat="server" Text="Url"></asp:Label>
    <br />
    <asp:Label ID="lblKey" runat="server" Text="Key"></asp:Label>
    <br />
    <br />
    <br />
    <br />
    一：asp 的 linkButton 控件方式
    <br />
    1:<asp:LinkButton ID="LinkButton1" runat="server">在当前页面单点登录signature，并打开签章列表</asp:LinkButton>
    <br />
    2:<asp:LinkButton ID="LinkButton2" runat="server">在新页面单点登录signature，并打开签章列表，但是当前页面刷新了</asp:LinkButton>
    <br />
    <br />
    <br />
    <br />
    二：普通a链接 方式
    <br />
    1:<a href="WebForm1.aspx" target="_self">在当前页面单点登录signature，并打开签章列表</a>
    <br />
    2:<a href="WebForm1.aspx" target="_blank">在新页面单点登录signature，并打开签章列表，不刷新当前页面</a>
    <br />
    <br />
    <br />
    <br />
    三：iframe a链接 方式
    <br />
    1:<a href="signature.aspx" target="_blank">在当前页面单点登录signature，并打开签章列表</a>
    <br />
    loginAsIframe

</asp:Content>
