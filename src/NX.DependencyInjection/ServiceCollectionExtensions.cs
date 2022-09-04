using System;
using System.Linq;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace NX.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// 指定のアセンブリからコンポーネントを検索して自動登録します。
        /// </summary>
        /// <param name="services">コンポーネントを登録するServiceCollection</param>
        /// <param name="assemblyNames">コンポーネントを検索するアセンブリの名前一覧</param>
        public static IServiceCollection RegisterComponents(this IServiceCollection services, params string[] assemblyNames)
        {
            foreach (var ci in assemblyNames.SelectMany(ComponentLoader.Load))
            {
                var lifetime = ci.Scope switch
                {
                    ComponentScope.Singleton => ServiceLifetime.Singleton,
                    ComponentScope.Scoped => ServiceLifetime.Scoped,
                    ComponentScope.Transient => ServiceLifetime.Transient,
                    _ => throw new InvalidOperationException("Unknown ComponentScope value")
                };

                services.Add(new ServiceDescriptor(ci.TargetType, ci.ImplementType, lifetime));
            }

            return services;
        }

        /// <summary>
        /// 指定のアセンブリから設定クラスを検索して自動登録します。
        /// </summary>
        /// <param name="services">コンポーネントを登録するServiceCollection</param>
        /// <param name="configuration">設定値</param>
        /// <param name="assemblyNames">コンポーネントを検索するアセンブリの名前一覧</param>
        public static IServiceCollection RegisterConfigurations(this IServiceCollection services, IConfiguration configuration,
            params string[] assemblyNames)
        {
            services.AddOptions();

            var configureMethod = typeof(OptionsConfigurationServiceCollectionExtensions)
                                      .GetMethod(nameof(OptionsConfigurationServiceCollectionExtensions.Configure),
                                          new[] { typeof(IServiceCollection), typeof(IConfiguration) })
                                  ?? throw new InvalidOperationException(
                                      "Unable to reflect Configure(IServiceCollection, IConfiguration).");
            var namedConfigureMethod = typeof(OptionsConfigurationServiceCollectionExtensions)
                                           .GetMethod(nameof(OptionsConfigurationServiceCollectionExtensions.Configure),
                                               new[] { typeof(IServiceCollection), typeof(string), typeof(IConfiguration) })
                                       ?? throw new InvalidOperationException(
                                           "Unable to reflect Configure(IServiceCollection, string, IConfiguration).");

            foreach (var ci in assemblyNames.SelectMany(ConfigurationLoader.Load))
            {
                var configurationSection = string.IsNullOrEmpty(ci.Section) ? configuration : configuration.GetSection(ci.Section);
                if (string.IsNullOrEmpty(ci.Name))
                {
                    configureMethod.MakeGenericMethod(ci.Type)
                        .Invoke(null, new object[] { services, configurationSection });
                }
                else
                {
                    namedConfigureMethod.MakeGenericMethod(ci.Type)
                        .Invoke(null, new object[] { services, ci.Name, configurationSection });
                }
            }

            return services;
        }
    }
}
