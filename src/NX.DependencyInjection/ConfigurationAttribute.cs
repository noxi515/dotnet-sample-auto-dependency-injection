using System;

namespace NX.DependencyInjection
{
    /// <summary>
    /// <see cref="Microsoft.Extensions.DependencyInjection.IServiceCollection" />に<see cref="Microsoft.Extensions.Options.IOptions{TOptions}" />を自動登録するための属性。
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true, Inherited = false)]
    public class ConfigurationAttribute : Attribute
    {
        /// <summary>
        /// 設定ファイルにバインドするためのキー。<br />
        /// ネストする場合は &quot;:&quot; で区切り、　<c>hoge:fuga:piyo</c> と記述すること。
        /// </summary>
        public string? SectionKey { get; }

        /// <summary>
        /// 設定値を登録する名前。
        /// </summary>
        public string? Name { get; set; }

        /// <summary>
        /// 初期化します。
        /// </summary>
        /// <param name="sectionKey">設定ファイルにバインドするためのキー</param>
        /// <param name="name">設定値を登録する名前</param>
        public ConfigurationAttribute(string? sectionKey, string? name = null)
        {
            SectionKey = sectionKey;
            Name = name;
        }
    }
}
