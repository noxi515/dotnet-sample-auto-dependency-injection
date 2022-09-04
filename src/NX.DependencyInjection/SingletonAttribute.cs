using System;

namespace NX.DependencyInjection
{
    /// <summary>
    /// <see cref="Microsoft.Extensions.DependencyInjection.IServiceCollection" />にコンポーネントをSingletonとして自動登録するための属性。
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true, Inherited = false)]
    public class SingletonAttribute : ComponentAttribute
    {
        public SingletonAttribute()
            : base(ComponentScope.Singleton)
        {
        }

        public SingletonAttribute(Type? targetType = null)
            : base(ComponentScope.Singleton, targetType)
        {
        }
    }
}
