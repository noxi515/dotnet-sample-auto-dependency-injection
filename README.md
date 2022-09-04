# .NET DI 自動登録（サンプル実装）

このプロジェクトは .NET の面倒臭い DI コンポーネント登録をアセンブリ単位で自動化する手法のサンプル実装です。


## 使い方

コンポーネントを自動登録するには、対象コンポーネントに自動登録用の属性を付与する、対象アセンブリからコンポーネントを走査する、の2段階があります。


### 属性の付与（サービス）

このサンプル実装では `ComponentAttribute` が自動登録するためのマーカーになっています。

```cs
using NX.DependencyInjection;

namespace Hoge;

[Component(Scope = ComponentScope.Singleton, Target = typeof(IHogeService))]
public class HogeService : IHogeService
{
}
```

自動登録に使用する属性は `ComponentAttribute` 以外にも `Singleton` `Transient` `Repository` `Service` が用意されています。なお `ComponentScope` は `ServiceLifetime` に対応する列挙値です。


### 自動登録（サービス）

自動登録用の属性を付与したら、 `ServiceCollection` に対してアセンブリ名を指定して自動登録用の拡張メソッドを呼び出します。引数のアセンブリ名は可変長引数となっていますので、必要な数だけ複数同時に指定できます。

```cs
var services = new ServiceCollection();
services.RegisterComponents("Target.Assembly.Name");
```

### 属性の付与（Options）


このサンプル実装では `ConfigurationAttribute` が自動登録するためのマーカーになっています。

```cs
using NX.DependencyInjection;

namespace Hoge;

[Configuration(SectionKey = "Hoge:Fuga")]
public class HogeConfiguration
{
}
```


### 自動登録（Options）

自動登録用の属性を付与したら、サービス時と同様に `ServiceCollection` に対してアセンブリ名を指定して自動登録用の拡張メソッドを呼び出します。引数のアセンブリ名は可変長引数となっていますので、必要な数だけ複数同時に指定できます。

```cs
var services = new ServiceCollection();
var configuration = ...;
services.RegisterConfigurations(configuration, "Target.Assembly.Name");
```
