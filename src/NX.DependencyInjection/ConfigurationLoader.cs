using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace NX.DependencyInjection
{
    public static class ConfigurationLoader
    {
        public static IEnumerable<ConfigurationInfo> Load(string assemblyName)
        {
            try
            {
                return Load(Assembly.Load(assemblyName));
            }
            catch (Exception e)
            {
                throw new AssemblyLoadException("アセンブリの読み込みに失敗しました。", e);
            }
        }

        public static IEnumerable<ConfigurationInfo> Load(Assembly assembly)
        {
            return assembly.GetTypes()
                .SelectMany(type => type.GetCustomAttributes<ConfigurationAttribute>()
                    .Select(attr => new ConfigurationInfo(attr.SectionKey, attr.Name, type)));
        }

        public class ConfigurationInfo
        {
            public string? Section { get; }
            public string? Name { get; }
            public Type Type { get; }

            public ConfigurationInfo(string? section, string? name, Type type)
            {
                Section = section;
                Type = type;
                Name = name;
            }
        }
    }
}
