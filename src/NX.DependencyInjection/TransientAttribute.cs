using System;

namespace NX.DependencyInjection
{
    /// <summary>
    /// <see cref="Microsoft.Extensions.DependencyInjection.IServiceCollection" />にコンポーネントをTransientとして自動登録するための属性。
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true, Inherited = false)]
    public class TransientAttribute : ComponentAttribute
    {
        public TransientAttribute()
            : base(ComponentScope.Transient)
        {
        }

        public TransientAttribute(Type? targetType = null)
            : base(ComponentScope.Transient, targetType)
        {
        }
    }
}
