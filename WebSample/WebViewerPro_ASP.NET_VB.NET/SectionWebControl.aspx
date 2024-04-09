<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="SectionWebControl.aspx.vb" Inherits="GrapeCity.ActiveReports.Samples.Web.SectionWebControl" culture="auto"%>
<%@ Register TagPrefix="activereportsweb" Namespace="GrapeCity.ActiveReports.Web" assembly="MESCIUS.ActiveReports.Web" %>

<!DOCTYPE html>

<style>
    html, body {
      width: 100%; 
      height: 100%; 
      margin: 0; 
      padding: 0;
    }
</style>

<html xmlns="http://www.w3.org/1999/xhtml">
    <head runat="server">
        <title>ActiveReports WebViewer Control</title>
        <link rel="stylesheet" type="text/css" href="CSS/default.css"/>
        <link rel="stylesheet" type="text/css" href="CSS/tab.css" />
        <script src="Scripts/tab.js" language="javascript" type="text/javascript"></script>
        <script>
            // default tab:
            window.onload = function() {
                document.getElementById("htmlBtn").click();
            };
		</script>
    </head>

    <body>
        <div id="pagetop" style="z-index: 101">
            <div id="pagetitlebanner">
                <h1><a href="Default.aspx">Professional Webサンプル</a></h1>
                <h2><a href="WebControl.aspx">ASP.NET対応のWebコントロール</a></h2>
                <h2>セクションレポート</h2>
            </div>
        </div>
         <div class="content" style="overflow: auto">
            ActiveReportsのWebコントロールは、レポートをブラウザに表示するために、簡単に配信できる機能を提供します。クライアントには、ActiveReportsをインストールする必要はありません。以下は、ActiveReportsのWebコントロールを使用した簡単な例です。
            Webコントロールにレポートを表示するには、デザイン時にWebコントロールのReportNameプロパティに、レポートオブジェクトを設定するだけです。
            また、別な方法として、コード上で、WebコントロールのReportNameプロパティに、レポートクラスのインスタンスを設定することも可能です。
        </div>
        <!-- Tab links -->
        <div class="tab">
            <button id="htmlBtn" class="tablinks" onclick="clickOnTab(event, 'HtmlViewer')">Html Viewer</button>
            <button id="rawBtn"  class="tablinks" onclick="clickOnTab(event, 'RawHtml')">Raw Html</button>
            <button id="pdfBtn"  class="tablinks" onclick="clickOnTab(event, 'AcrobatReader')">Acrobat Reader</button>
        </div>
        <!-- Tab content -->
        <div id="HtmlViewer" class="tabcontent">
            <ActiveReportsWeb:WebViewer class="viewer" ViewerType="HtmlViewer" ReportName="RpxReports\\Invoice.rpx" runat="server" LocalizationJson="ar-webviewer-locale.json"/>
        </div>
        <div id="RawHtml" class="tabcontent">
            <ActiveReportsWeb:WebViewer class="viewer" ViewerType="RawHtml" ReportName="RpxReports\\Invoice.rpx" runat="server"/>
        </div>
        <div id="AcrobatReader" class="tabcontent">
            <ActiveReportsWeb:WebViewer class="viewer" ViewerType="AcrobatReader" ReportName="RpxReports\\Invoice.rpx" runat="server"/>
        </div>
    </body>
</html>