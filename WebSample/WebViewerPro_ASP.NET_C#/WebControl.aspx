<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebControl.aspx.cs" Inherits="GrapeCity.ActiveReports.Samples.Web.WebControl" culture="auto"%>
<%@ Register TagPrefix="activereportsweb" Namespace="GrapeCity.ActiveReports.Web" assembly="MESCIUS.ActiveReports.Web" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>WebViewerコントロール</title>
    <link rel="stylesheet" type="text/css" href="CSS/default.css"/>
</head>
<body>
    <div id="pagetop">
        <div id="pagetitlebanner">
            <h1><a href="Default.aspx">Professional Webサンプル</a></h1>
            <h2>Webコントロールサンプル</h2>
        </div>
    </div>
    <div id="pagebody">
        <h2>ActiveReports for .NET Professional のみで使用できる機能</h2>
        <!-- WebControls -->
        <p>
            <a href="SectionWebControl.aspx">セクションレポート</a>
        </p>
        <p>
            <a href="PageWebControl.aspx">ページレポート</a>
        </p>
        <p>
            <a href="RdlxWebControl.aspx">RDLXレポート</a>
        </p>
        <br/>

        ActiveReportsは、Webフォーム上にドラッグ&ドロップするだけでブラウザ上にレポートを表示することができる ASP.NET 対応のサーバーサイドWebコントロール「WebViewerコントロール」を提供しています。
        このWebコントロールは、以下の3種類のプレビュー形式をサポートしています

        <dl>
            <dt>HTMLビューワ</dt>
            <dd>
                レポート内のページを１ページずつ表示する、スクロール可能なビューワを提供します。
                クライアントのブラウザにダウンロードされるのは、HTMLとJavaScriptだけです。
            </dd>
            <dt>PDF Reader</dt>
            <dd>
                レポート内のすべてのページをAdobe Readerで閲覧可能なPDFドキュメントとして出力します。
            </dd>
            <dt>Raw Html</dt>
            <dd>
                レポート内のすべてのページを、単一の連続したHTMLページとして表示します。
                基本的に、ブラウザの印刷機能を利用した印刷に適した状態でレポートをHTMLページとして出力しますが、レイアウトによっては、印刷時のページ付けが
                適切になされない場合があります。
            </dd>
        </dl>

    </div>
    <form id="form1" runat="server">
    </form>
</body>
</html>

