using System;

namespace NX.DependencyInjection
{
    /// <summary>
    /// <see cref="Microsoft.Extensions.DependencyInjection.IServiceCollection" />にコンポーネントを自動登録するための属性
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true, Inherited = false)]
    public class ComponentAttribute : Attribute
    {
        /// <summary>
        /// コンポーネントのライフサイクル。未設定の場合は<see cref="ComponentScope.Scoped"/>。
        /// </summary>
        public ComponentScope Scope { get; set; } = ComponentScope.Scoped;

        /// <summary>
        /// コンポーネントの登録対象の型。未設定の場合はコンポーネント自身の型。
        /// </summary>
        public Type? TargetType { get; set; }

        public ComponentAttribute()
        {
        }

        public ComponentAttribute(ComponentScope scope, Type? targetType = null)
        {
            Scope = scope;
            TargetType = targetType;
        }
    }
}
