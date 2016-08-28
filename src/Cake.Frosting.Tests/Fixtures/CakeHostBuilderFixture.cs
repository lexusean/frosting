﻿using Cake.Core;
using Cake.Core.Composition;
using Cake.Core.Diagnostics;
using Cake.Core.IO;
using Cake.Frosting.Tests.Fakes;
using Cake.Testing;
using NSubstitute;

namespace Cake.Frosting.Tests.Fixtures
{
    public class CakeHostBuilderFixture
    {
        public CakeHostBuilder Builder { get; set; }

        public IFileSystem FileSystem { get; set; }
        public ICakeEnvironment Environment { get; set; }
        public ICakeEngine Engine { get; set; }
        public ICakeLog Log { get; set; }
        public IExecutionStrategy Strategy { get; set; }
        public CakeHostOptions Options { get; set; }

        public CakeHostBuilderFixture()
        {
            Builder = new CakeHostBuilder();
            Environment = FakeEnvironment.CreateUnixEnvironment();
            FileSystem = new FakeFileSystem(Environment);
            Log = Substitute.For<ICakeLog>();
            Engine = new CakeEngine(Log);
            Options = new CakeHostOptions();
        }

        public ICakeHost Build()
        {
            Builder.ConfigureServices(s => s.RegisterType<NullConsole>().As<IConsole>());
            Builder.ConfigureServices(s => s.RegisterInstance(Environment).As<ICakeEnvironment>());
            Builder.ConfigureServices(s => s.RegisterInstance(FileSystem).As<IFileSystem>());
            Builder.ConfigureServices(s => s.RegisterInstance(Engine).As<ICakeEngine>());
            Builder.ConfigureServices(s => s.RegisterInstance(Log).As<ICakeLog>());
            Builder.ConfigureServices(s => s.RegisterInstance(Options).As<CakeHostOptions>());

            if (Strategy != null)
            {
                Builder.ConfigureServices(services => services.RegisterInstance(Strategy).As<IExecutionStrategy>());
            }

            Builder.ConfigureServices(s => s.RegisterInstance(Options).AsSelf().Singleton());

            return Builder.Build();
        }

        public int Run()
        {
            return Build().Run();
        }
    }
}