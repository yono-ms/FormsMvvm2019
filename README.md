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
