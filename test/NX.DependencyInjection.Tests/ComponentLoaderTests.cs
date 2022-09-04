using System;
using System.Reflection;
using FluentAssertions;
using Xunit;

namespace NX.DependencyInjection
{
    public class ComponentLoaderTests
    {
        [Fact]
        public void コンポーネントの自動読み込み()
        {
            var ci = ComponentLoader.Load(typeof(ComponentLoaderTests).Assembly);
            ci.Should().BeEquivalentTo(new[]
            {
                new ComponentLoader.ComponentInfo(ComponentScope.Scoped, typeof(ComponentA), typeof(ComponentA)),
                new ComponentLoader.ComponentInfo(ComponentScope.Singleton, typeof(ComponentB), typeof(ComponentB)),
                new ComponentLoader.ComponentInfo(ComponentScope.Scoped, typeof(IDisposable), typeof(ComponentB)),
                new ComponentLoader.ComponentInfo(ComponentScope.Transient, typeof(IDisposable), typeof(ComponentC)),

                new ComponentLoader.ComponentInfo(ComponentScope.Scoped, typeof(RepositoryA), typeof(RepositoryA)),
                new ComponentLoader.ComponentInfo(ComponentScope.Singleton, typeof(RepositoryB), typeof(RepositoryB)),
                new ComponentLoader.ComponentInfo(ComponentScope.Scoped, typeof(IDisposable), typeof(RepositoryB)),
                new ComponentLoader.ComponentInfo(ComponentScope.Transient, typeof(IDisposable), typeof(RepositoryC)),

                new ComponentLoader.ComponentInfo(ComponentScope.Scoped, typeof(ServiceA), typeof(ServiceA)),
                new ComponentLoader.ComponentInfo(ComponentScope.Singleton, typeof(ServiceB), typeof(ServiceB)),
                new ComponentLoader.ComponentInfo(ComponentScope.Scoped, typeof(IDisposable), typeof(ServiceB)),
                new ComponentLoader.ComponentInfo(ComponentScope.Transient, typeof(IDisposable), typeof(ServiceC)),

                new ComponentLoader.ComponentInfo(ComponentScope.Singleton, typeof(SingletonA), typeof(SingletonA)),
                new ComponentLoader.ComponentInfo(ComponentScope.Singleton, typeof(IDisposable), typeof(SingletonB)),

                new ComponentLoader.ComponentInfo(ComponentScope.Transient, typeof(TransientA), typeof(TransientA)),
                new ComponentLoader.ComponentInfo(ComponentScope.Transient, typeof(IDisposable), typeof(TransientB)),
            });
        }

        [Component]
        public class ComponentA
        {
        }

        [Component(Scope = ComponentScope.Singleton)]
        [Component(TargetType = typeof(IDisposable))]
        public class ComponentB : IDisposable
        {
            void IDisposable.Dispose()
            {
            }
        }

        [Component(Scope = ComponentScope.Transient, TargetType = typeof(IDisposable))]
        public class ComponentC : IDisposable
        {
            void IDisposable.Dispose()
            {
            }
        }


        [Component]
        public class RepositoryA
        {
        }

        [Component(Scope = ComponentScope.Singleton)]
        [Component(TargetType = typeof(IDisposable))]
        public class RepositoryB : IDisposable
        {
            void IDisposable.Dispose()
            {
            }
        }

        [Component(Scope = ComponentScope.Transient, TargetType = typeof(IDisposable))]
        public class RepositoryC : IDisposable
        {
            void IDisposable.Dispose()
            {
            }
        }


        [Component]
        public class ServiceA
        {
        }

        [Component(Scope = ComponentScope.Singleton)]
        [Component(TargetType = typeof(IDisposable))]
        public class ServiceB : IDisposable
        {
            void IDisposable.Dispose()
            {
            }
        }

        [Component(Scope = ComponentScope.Transient, TargetType = typeof(IDisposable))]
        public class ServiceC : IDisposable
        {
            void IDisposable.Dispose()
            {
            }
        }


        [Singleton]
        public class SingletonA
        {
        }

        [Singleton(TargetType = typeof(IDisposable))]
        public class SingletonB : IDisposable
        {
            void IDisposable.Dispose()
            {
            }
        }


        [Transient]
        public class TransientA
        {
        }

        [Transient(TargetType = typeof(IDisposable))]
        public class TransientB : IDisposable
        {
            void IDisposable.Dispose()
            {
            }
        }

    }
}
