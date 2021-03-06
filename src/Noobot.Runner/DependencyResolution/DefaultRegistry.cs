﻿using Noobot.Custom;
using Noobot.Domain.MessagingPipeline;
using Noobot.Runner.Logging;
using StructureMap.Configuration.DSL;
using StructureMap.Graph;

namespace Noobot.Runner.DependencyResolution
{
    public class DefaultRegistry : Registry
    {
        public DefaultRegistry()
        {
            Scan(scan =>
            {
                scan.TheCallingAssembly();
                scan.WithDefaultConventions();
                scan.Assembly("Noobot.Domain");
                scan.Assembly("Noobot.Custom");
            });

            For<INoobotHost>()
                .Use<NoobotHost>();
            
            For<IPipelineManager>()
                .Use<PipelineManager>();

            For<ILogger>()
                .Use<ConsoleLogger>();
        }
    }
}