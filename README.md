# FormsMvvm2019
Xamarin Forms Mvvm ver 2019.

# ViewModel分離

## 目的

ViewModelをPageとは別クラスにしたい要望は多い。

## 従来の失敗例

### コードビハインドでBinding

コードビハインドでバインドすると実行時に期待動作となるが、
xamlエディターでのインテリセンスが効かない。

これを解決するためにPageでStaticResourceを作って、
d:BindingContextにセットする方法を考えた。
しかし、これだとインスタンスが2個できてしまう。

ViewModelコンストラクタに処理を書いてはいけないが、
もし書いてあったら二階実行されてしまう。
メモリ使用量はプロパティだけなので、
気になるほど大きくはならないはずだが二倍になっている。

### ViewModelにCommand

バインドに依存してイベントハンドラをVMに書いている。
画面依存しない処理もあるのでできるなら分離したい。

## 2019年の発見

### PageのBindingContextを使う方法

```
<ContentPage.BindingContext>
    <local:MainViewModel/>
</ContentPage.BindingContext>
```

これでインスタン生成とバインドが同時にできる。
当然インテリセンスが動く。
インスタンスはPageコンストラクタで作成された1個だけ。

### Commandをstaticクラスに書く方法

```
<Button
    Text="{Binding ButtonCommitText}"
    Command="{x:Static local:BizLogic.Command}"
    CommandParameter="{Binding}"/>
```

x:Staticでstaticクラスのプロパティをバインドできる。
引数はBindingだけ書いておけばBindingContext(=ViewModel)が
渡される。

```
static class BizLogic
{
    static public Command Command => new Command((args) =>
    {
        switch (args)
        {
            case MainViewModel mainViewModel:
                Application.Current.MainPage.DisplayAlert("BizLogic", $"PassCode={mainViewModel.PassCode}", "OK");
                break;

            default:
                Application.Current.MainPage.DisplayAlert("BizLogic", $"unknown args={args}", "OK");
                break;
        }
    });
}
```

Commandのプロパティ名を分けるか、
引数判定するかはケースバイケース。
型switchが使えるので短い処理ならこれが簡単に書ける。

# ローカルログ

## 目的

デバイス内にログファイルを残したいという要望は多い。
NLogしか選択肢が無いが、ILoggerで使いたい。
ログ実装を差し替えたいときに全ファイルにNLog型が存在するのは困る。

## 問題点

AddProviderでNLogを使えるNugetがあるが、
Releaseビルドでリンカーがメソッドを消してしまう不具合がある。

## 2019年の発見

### AppLoggerクラスを作る

ILoggerの実装を作ってGetLoggerを提供すればいい。
その中でNLogに変換して使っておくと、
NLog廃止となってもダメージがほとんどない。

NLogのConsoleでUWPだけデバッガに出ない件も対応できる。

# カスタムコントロール

## 目的

部品をカスタマイズする際に複数回使用するなら、
以下の作業を行う。

- styleを作る
- カスタムコントロールを作る

## 問題点

XamarinFormsにはカスタムコントロールの実装方法がいろいろあり、
どの方法を採用するべきなのか悩む。

## 2019年のカスタムコントロール整理

以下に簡単な順に整理する。

### 派生クラスを作る

Formsの吊るし部品に特定の設定を行う。
たぶんstyleで実現できることをソースで書いただけになる。
styleで書いた方が低コストなのかを検討すべき。

### ContentView

Formsの吊るしの部品を組み合わせて一つの部品にする。
依存関係プロパティを作るのが面倒だがメリットは多い。

- プラットフォームに依存しないコード
- デザイナープレビュー

### Behavior

Formsの吊るしの部品に添付ビヘイビアをくっつける。
イベントを新しいCommandに変換できる。
プロパティをバインドして、
VM側でchangedハンドラを作ることもできるが、
VMを定義だけにして処理を追い出したい場合に使える。
プロパティのバインドだけで済むのかどうかを検討する。

### Effect

Formsの吊るしの部品にプラットフォームに存在するプロパティが無いケースは多い。
Entryの枠消しなどが該当する。
FormsのRendererが何で実装されているのかを調べて、
プラットフォームのコードで調整できる場合に使う。

### Renderer

プラットフォームの部品を使うだけなのに、
Formsに部品が用意されていない場合に使用する。
Formsにある場合はEffect、無い場合はRendererを採用する。
これはほぼXamarinNativeという方法なので、
ソースは全く別物でプラットフォーム種類分だけ掛け算で増えることになる。

### SkiaSharp

プラットフォームにも無い。
それならForms側でSkiaSharpを使用するのがコスト的にいくらか。

### おまけ：ListViewは使えない

ListViewをカスタマイズして何とかしようと思うくらいなら、
作った方がましだったというケースは多い。
吊るしのCellを使えないならもうあきらめよう。
Cellのセレクターとかは面倒になるだけ。
