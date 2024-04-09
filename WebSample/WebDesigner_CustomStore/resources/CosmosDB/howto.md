# CosmosDB実装の確認方法

このプロジェクトには、CosmosDBで使用するために必要なファイルが含まれています。
動作を確認するためには、**Azure Cosmos DBアカウント**、または、**CosmosDBエミュレータ**が必要です。（[ダウンロード](https://aka.ms/cosmosdb-emulator)）

## 接続設定
以下の手順で、データベースへの接続を設定します。

1. **App.config**に、アカウントのエンドポイントとキーを設定します。
2. **Startup.cs** で、LiteDB実装を登録する行をコメントアウトします（行番号 37）。
3. **Startup.cs**で、CosmosDBの実装を登録する行のコメントを外します（行番号38）。
4. **Startup.cs**で、CosmosDBイニシャライザーに関連する行のコメントを外します（行番号48～50）。
5. **CosmoDB.cs**で、**DATABASE_NAME**の値を設定します。

初回実行時に、データベースが作成されます。
