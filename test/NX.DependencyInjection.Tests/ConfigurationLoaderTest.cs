using FluentAssertions;
using Xunit;

namespace NX.DependencyInjection
{
    public class ConfigurationLoaderTest
    {
        [Fact]
        public void 設定の自動読み込み()
        {
            var ci = ConfigurationLoader.Load(typeof(ConfigurationLoaderTest).Assembly);
            ci.Should().BeEquivalentTo(new[]
            {
                new ConfigurationLoader.ConfigurationInfo(null, null, typeof(ConfigRoot)),
                new ConfigurationLoader.ConfigurationInfo("A", null, typeof(ConfigA)),
                new ConfigurationLoader.ConfigurationInfo("B", "ConfigB", typeof(ConfigB_C)),
                new ConfigurationLoader.ConfigurationInfo("C", "ConfigC", typeof(ConfigB_C)),
            });
        }


        [Configuration(null)]
        public class ConfigRoot
        {
            public ConfigA A { get; set; } = default!;
            public ConfigB_C B { get; set; } = default!;
            public ConfigB_C C { get; set; } = default!;
        }

        [Configuration("A")]
        public class ConfigA
        {
            public string Prop1 { get; set; } = default!;
        }

        [Configuration("B", Name = "ConfigB")]
        [Configuration("C", Name = "ConfigC")]
        public class ConfigB_C
        {
            public int Prop2 { get; set; }
        }
    }
}
