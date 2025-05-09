# Blazorデザイナ WebAssembly サンプル

このサンプルでは、リモートレポートサービスを使用して、ActiveReports BlazorデザイナでBlazor WebAssemblyアプリを作成する方法を説明します。

## 必要システム

このサンプルをご利用いただくために以下のアプリケーションが必要です。
* [Visual Studio 2022](https://visualstudio.microsoft.com/vs/) 17.0 以降
* [.NET 6.0 SDK](https://www.microsoft.com/net/download)
* [.NET Core Hosting Bundle](https://dotnet.microsoft.com/download/dotnet/thank-you/runtime-aspnetcore-6.0.0-windows-hosting-bundle-installer) (IIS展開用)

## サンプルのビルド手順

1. レポートサービス サンプルを実行して、レポートサービスを開始します。
2. Visual Studioを起動し、**ファイル → 開く → プロジェクト/ソリューション**を選択します。
3. サンプルフォルダを開き、Visual Studioソリューションファイル(.sln)を選択します。
4. ソリューションエクスプローラーでソリューションを右クリックし、**NuGetパッケージの復元**を選択します。
5. **ビルド → ソリューションのビルド**を選択し、ビルドを実行します。

## サンプルの実行

サンプルをデバッグ実行するには、**デバッグ → デバッグの開始**を選択します。
デバッグせずにサンプルを実行するには**デバッグ → デバッグなしで開始**を選択します。