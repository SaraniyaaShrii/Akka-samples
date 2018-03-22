using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Topshelf;

namespace Attribution.Lighthouse
{
    class Program
    {
        public static void Main(string[] args)
        {
            HostFactory.Run(x =>
            {
                x.SetServiceName("Lighthouse");
                x.SetDisplayName("Lighthouse");
                x.SetDescription("Seed node for the Akka Cluster");

                x.UseAssemblyInfoForServiceInfo();
                x.RunAsLocalSystem();
                x.StartAutomatically();

                x.Service<LighthouseService>(sc =>
                {
                    sc.ConstructUsing(() => new LighthouseService());

                    // the start and stop methods for the service
                    sc.WhenStarted(s => s.Start());
                    sc.WhenStopped(s => s.StopAsync().Wait());
                });

                x.EnableServiceRecovery(r => r.RestartService(1));
            });
        }
    }
}
