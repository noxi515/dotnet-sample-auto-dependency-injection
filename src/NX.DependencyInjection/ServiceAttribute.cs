using System;

namespace NX.DependencyInjection
{
    /// <summary>
    /// <see cref="Microsoft.Extensions.DependencyInjection.IServiceCollection" />にコンポーネントを自動登録するための属性。
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true, Inherited = false)]
    public class ServiceAttribute : ComponentAttribute
    {
    }
}
