using System;
using System.IO;
using System.Linq;
using System.Text;
using FluentAssertions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Xunit;

namespace NX.DependencyInjection
{
    public static class ServiceCollectionTests
    {
        public class ComponentTests
        {
            [Fact]
            public void コンポーネントの自動登録()
            {
                var services = new ServiceCollection();
                services.RegisterComponents(typeof(ServiceCollectionTests).Assembly.FullName!);

                var serviceDescriptors = services.ToList();
                serviceDescriptors.Should().BeEquivalentTo(new[]
                {
                    new ServiceDescriptor(typeof(ComponentLoaderTests.ComponentA), typeof(ComponentLoaderTests.ComponentA), ServiceLifetime.Scoped),
                    new ServiceDescriptor(typeof(ComponentLoaderTests.ComponentB), typeof(ComponentLoaderTests.ComponentB), ServiceLifetime.Singleton),
                    new ServiceDescriptor(typeof(IDisposable), typeof(ComponentLoaderTests.ComponentB), ServiceLifetime.Scoped),
                    new ServiceDescriptor(typeof(IDisposable), typeof(ComponentLoaderTests.ComponentC), ServiceLifetime.Transient),

                    new ServiceDescriptor(typeof(ComponentLoaderTests.RepositoryA), typeof(ComponentLoaderTests.RepositoryA), ServiceLifetime.Scoped),
                    new ServiceDescriptor(typeof(ComponentLoaderTests.RepositoryB), typeof(ComponentLoaderTests.RepositoryB), ServiceLifetime.Singleton),
                    new ServiceDescriptor(typeof(IDisposable), typeof(ComponentLoaderTests.RepositoryB), ServiceLifetime.Scoped),
                    new ServiceDescriptor(typeof(IDisposable), typeof(ComponentLoaderTests.RepositoryC), ServiceLifetime.Transient),

                    new ServiceDescriptor(typeof(ComponentLoaderTests.ServiceA), typeof(ComponentLoaderTests.ServiceA), ServiceLifetime.Scoped),
                    new ServiceDescriptor(typeof(ComponentLoaderTests.ServiceB), typeof(ComponentLoaderTests.ServiceB), ServiceLifetime.Singleton),
                    new ServiceDescriptor(typeof(IDisposable), typeof(ComponentLoaderTests.ServiceB), ServiceLifetime.Scoped),
                    new ServiceDescriptor(typeof(IDisposable), typeof(ComponentLoaderTests.ServiceC), ServiceLifetime.Transient),

                    new ServiceDescriptor(typeof(ComponentLoaderTests.SingletonA), typeof(ComponentLoaderTests.SingletonA), ServiceLifetime.Singleton),
                    new ServiceDescriptor(typeof(IDisposable), typeof(ComponentLoaderTests.SingletonB), ServiceLifetime.Singleton),

                    new ServiceDescriptor(typeof(ComponentLoaderTests.TransientA), typeof(ComponentLoaderTests.TransientA), ServiceLifetime.Transient),
                    new ServiceDescriptor(typeof(IDisposable), typeof(ComponentLoaderTests.TransientB), ServiceLifetime.Transient),
                });
            }
        }


        public class ConfigurationTests
        {
            [Fact]
            public void 設定の自動登録()
            {
                const string json = @"
{
  ""A"": { ""Prop1"": ""1"" },
  ""B"": { ""Prop2"": 2 },
  ""C"": { ""Prop2"": 3 }
}
";
                using var stream = new MemoryStream(Encoding.UTF8.GetBytes(json));
                var configuration = new ConfigurationBuilder().AddJsonStream(stream).Build();

                var services = new ServiceCollection();
                services.RegisterConfigurations(configuration, typeof(ServiceCollectionTests).Assembly.FullName!);

                using var provider = services.BuildServiceProvider();
                var root = provider.GetRequiredService<IOptions<ConfigurationLoaderTest.ConfigRoot>>().Value;
                root.A.Prop1.Should().Be("1");
                root.B.Prop2.Should().Be(2);
                root.C.Prop2.Should().Be(3);

                var a = provider.GetRequiredService<IOptions<ConfigurationLoaderTest.ConfigA>>().Value;
                a.Prop1.Should().Be("1");

                var b = provider.GetRequiredService<IOptionsSnapshot<ConfigurationLoaderTest.ConfigB_C>>().Get("ConfigB");
                b.Prop2.Should().Be(2);

                var c = provider.GetRequiredService<IOptionsSnapshot<ConfigurationLoaderTest.ConfigB_C>>().Get("ConfigC");
                c.Prop2.Should().Be(3);
            }
        }
    }
}
