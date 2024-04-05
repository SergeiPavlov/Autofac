﻿namespace Autofac.Benchmarks;

public class RegistrationBenchmark
{
    [Benchmark]
    public void Register()
    {
        var builder = new ContainerBuilder();
        builder.RegisterType<A>();
        builder.RegisterType<B1>();
        builder.RegisterType<B2>().InstancePerMatchingLifetimeScope("request");
        builder.RegisterType<C2>().InstancePerLifetimeScope();
        builder.RegisterType<D1>().SingleInstance();
        builder.RegisterType<D2>().SingleInstance();
        var _container = builder.Build();
    }

    internal class A
    {
        public A(B1 b1, B2 b2) { }
    }

    internal class B1
    {
        public B1(B2 b2, C1 c1, C2 c2) { }
    }

    internal class B2
    {
        public B2(C1 c1, C2 c2) { }
    }

    internal class C1
    {
        public C1(C2 c2, D1 d1, D2 d2) { }
    }

    internal class C2
    {
        public C2(D1 d1, D2 d2) { }
    }

    internal class D1 { }

    internal class D2 { }
}
