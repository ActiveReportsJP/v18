<%@ Page language="VB" Inherits="ActiveReports.Samples.Web.CustomPreview.DefaultPage" CodeBehind="Default.aspx.vb" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
	<head id="Head1" runat="server">
		<title>ActiveReports Standard Edition Web Sample</title>
		<meta name="GENERATOR" content="Microsoft Visual Studio 7.0">
		<meta name="CODE_LANGUAGE" content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<link rel="stylesheet" type="text/css" href="default.css">
	</head>
	<body>
		<div id="pagetop">
			<div id="pagetitlebanner">
				<h1>Standard Web サンプル</h1>
			</div>
		</div>
		<div id="pagebody">
			
			<p>
				&nbsp;<a href="CustomExportPdf.aspx">PDF カスタムエクスポート</a> &amp;  <a href="CustomExportHtml.aspx">HTML カスタム エクスポート</a><br /> ActiveReports 
                のPDFやHTMLエクスポート機能を使用して、わずかなコードを記述するだけでレポートをブラウザ上に表示する方法を紹介します。</p>
            <p>
				<br>
				注意:
                <br />
                「HTML カスタム 
                エクスポート」では、実行ユーザーがWebサンプルのディレクトリのReportOutputフォルダに対して、書き込み権を持っている必要があります。たとえば、IIS規定の場合、5.1では 
                「ASPNET」という名前のローカルユーザーに対して書き込み権限を許可する必要があります。6.0や7.0の場合は「NETWORK 
                SERVICE」というアカウントになります。書き込み権が不足した状態では、HTMLファイルを作成する事ができず、エラーが発生します。アクセス許可の詳細については、MSDNライブラリ等を参照してください。 </p>
			<p style="FONT-SIZE: smaller; VERTICAL-ALIGN: sub; FONT-STYLE: italic">
                &nbsp;</p>
		</div>
		<form id="Default" method="post" runat="server">
		</form>
	</body>
</html>
