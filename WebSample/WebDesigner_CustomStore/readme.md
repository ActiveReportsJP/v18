# Webデザイナ カスタムストアサンプル

このサンプルはASP.NET Coreを利用してActiveReports.NETのWebデザイナにてカスタムストアを使用する方法を示しています。

カスタムストアをデザイナに組み込むには次の手順を実行します:
1. `IResourcesService`インターフェースを実装します。 また、管理されていないリソースを使用する場合は、`IDisposable`インターフェースを実装する必要があります。
2. ストアに`ResourceLocator`を実装します。 レポートリソース（データセット、テーマ、画像など）を正しく取得するために必要です。
3. `GetReport`メソッドで、実装されたリソースロケーターを使用してレポートサイトを再定義します。 詳細については、`CustomStoreReports.cs`を参照してください。
4. `Startup`クラスで以下を行います:
   1. 実装した`IResourcesService`サービスを登録します。
   2. 引数として`GetReport`メソッドを使用して、ビューアの` UseCustomStore`メソッド（ `app.UseReporting`）を呼び出します。
   3. 引数として実装した`IResourcesService`を使用して、デザイナーの` UseCustomStore`メソッド（ `app.UseDesigner`）を呼び出します。

本ケースでは、例として[LiteDB](https://www.litedb.org/)と[CosmosDB](https://azure.microsoft.com/ja-jp/services/cosmos-db/)をベースにした2種類の実装を掲載しています。

デフォルトではLiteDBが使用されています。

## 必要システム

このサンプルをご利用いただくために以下のアプリケーションが必要です。
* [Visual Studio 2022](https://visualstudio.microsoft.com/vs/) 17.0 以降
* [.NET 6.0 SDK](https://www.microsoft.com/net/download)
* [.NET Core Hosting Bundle](https://dotnet.microsoft.com/download/dotnet/thank-you/runtime-aspnetcore-6.0.0-windows-hosting-bundle-installer) (IIS展開用)
* [Node.js](https://nodejs.org) 10.x 以降

## サンプルのビルド手順

1. Visual Studioを起動し、**ファイル → 開く → プロジェクト/ソリューション**を選択します。
2. サンプルフォルダを開き、Visual Studioソリューションファイル(.sln)を選択します。
3. ソリューションエクスプローラーでソリューションを右クリックし、**NuGetパッケージの復元**を選択します。
4. **ビルド → ソリューションのビルド**を選択し、ビルドを実行します。


## サンプルの実行

サンプルをデバッグ実行するには、**デバッグ → デバッグの開始**を選択します。
デバッグせずにサンプルを実行するには**デバッグ → デバッグなしで開始**を選択します。
