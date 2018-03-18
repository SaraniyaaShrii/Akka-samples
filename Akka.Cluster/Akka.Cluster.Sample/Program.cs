using System;
using System.Configuration;
using Akka.Actor;
using Akka.Configuration;
using Akka.Configuration.Hocon;
using Akka.Cluster;

namespace AkkaCluster
{
    class Program
    {
        private static void Main(string[] args)
        {
            StartUp(args.Length == 0 ? new String[] { "0" } : args);
            Console.WriteLine("Press any key to exit");
            Console.ReadLine();
        }

        public static void StartUp(string[] ports)
        {
            var section = (AkkaConfigurationSection)ConfigurationManager.GetSection("akka");
            CreateActorSystem(section, 2551);
            //CreateActorSystem(section, 2552);
            
            //system.ActorOf(Props.Create(typeof(ClusterListener)), "clusterListener");
        }

        private static void CreateActorSystem(AkkaConfigurationSection section, int port)
        {
            var config = ConfigurationFactory.ParseString("akka.remote.dot-netty.tcp.port=" + port);

            var actorSystemName = string.Format("ClusterSystem{0}", port);

            var system = ActorSystem.Create("ClusterSystem", config);
            system.ActorOf(Props.Create(typeof(ClusterListener)), "clusterListener");
        }
    }
}