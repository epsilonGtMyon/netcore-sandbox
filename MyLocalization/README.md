# ローカリゼーションのカスタマイズ

`.resx` 以外の方法を用いて`IStringLocalizer` を使ってみたかったので書いてみた。

## MyLocalization
本体側

リソースの取得は `IMyLocalizedStringsProvider` に丸投げしてるので、実装側で好きな仕組みを使えるようにした。

私はwebapiしか使わないので `.cshtml` については対応していない

## MyLocalizationUsage
利用側

データベースに保存している情報をもとに国際化するようにした。
