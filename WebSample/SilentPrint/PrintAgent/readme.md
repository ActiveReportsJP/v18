# プリントエージェント ASP.NET Core サンプル

このプリントエージェントサンプルは、PDFファイルを印刷するためのASP.NET Core APIをホストするWindowsサービスを含んでいます。
プリントエージェントは、DioDocs.Pdf.jaライブラリを使用しています。
このライブラリの非ライセンス版では、読み込みが5ページまでに制限されています。
なお、DioDocs.Pdf.jaライブラリを使用するには、別途、開発ライセンスが必要です。
[価格表 - DioDocs（ディオドック） | Developer Solutions〈開発支援ツール〉 - メシウス株式会社](https://developer.mescius.jp/diodocs/pricelist#diodocs-pdf)


## 設定
プリンター名とリスニングポートは、appsettings.jsonファイルを修正することで変更できます。
プリンタ名はデフォルトで'Microsoft Print to PDF'が設定されています。この場合、ファイル名のプロンプトを避けるためにプリントエージェントは、すべてのユーザーに共通するドキュメントを含む特別なシステムディレクトリに出力pdfを保存します。
デフォルトでは、このサービスは http://localhost:5000 でリッスンします。PDFは、http://localhost:5000/print へのHTTP POSTで印刷することができます。

## 必要システム

このサンプルをご利用いただくために以下のアプリケーションが必要です。
* [Visual Studio 2022](https://visualstudio.microsoft.com/vs/) 17.0 以降
* [.NET 6.0 SDK](https://www.microsoft.com/net/download)
* [.NET Core Hosting Bundle](https://dotnet.microsoft.com/download/dotnet/thank-you/runtime-aspnetcore-6.0.0-windows-hosting-bundle-installer) (IIS展開用)

## サンプルのビルド手順

1. Visual Studioを起動し、**ファイル → 開く → プロジェクト/ソリューション**を選択します。
2. サンプルフォルダを開き、Visual Studioソリューションファイル(.sln)を選択します。
3. ソリューションエクスプローラーでソリューションを右クリックし、**NuGetパッケージの復元**を選択します。
4. **ビルド → ソリューションのビルド**を選択し、ビルドを実行します。

## サンプルの実行

サンプルをデバッグ実行するには、**デバッグ → デバッグの開始**を選択します。
デバッグせずにサンプルを実行するには**デバッグ → デバッグなしで開始**を選択します。
コマンドラインからサンプルを実行するには、以下のコマンドを使用します。: **dotnet run --console**

## Windowsサービスとしてインストールする

以下の手順でプリントエージェントをWindowsサービスとしてインストールします。:
1. サンプルのビルド
2. ビルドされた出力フォルダのコンテンツを特定のフォルダへコピーします。 (例. **C:\PrintAgent**)
3. 次のコマンドを実行し、サービスをインストールします。: **sc create PrintAgent binPath=C:\PrintAgent\PrintAgent.exe start=auto**
4. 次のコマンドを実行しサービスを開始します。: **sc start PrintAgent**
