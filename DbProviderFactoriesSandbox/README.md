# .NET CoreのDbProviderFactoriesの確認

.NET Coreで`DbProviderFactories`への登録の仕方がわからなかったので試した

コードを見た感じだと  
`DbProviderFactories.RegisterFactory`にキーと値を渡せば登録できる  
登録時点では存在チェックはしておらず、`GetFactory`のタイミングでチェックはされているようだ。


ひとまず、登録は`DbProviderFactoryAutoRegister`というクラスを作ってそっちに`dbproviderfactories.json`の内容に応じて
登録するようにした。

`.netstandard2.0`だと`DbProviderFactories`, `DbProviderFactory`は存在しないので注意
