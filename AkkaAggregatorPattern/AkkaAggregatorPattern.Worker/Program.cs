using Akka.Actor;
using AkkaAggregatorPattern.Actors;
using AkkaAggregatorPattern.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Akka.Cluster;
using Akka.Cluster.Tools.Client;
using System.Configuration;
using Akka.Cluster.Common;
using Akka.Configuration;
using Akka.Configuration.Hocon;

namespace AkkaAggregatorPatternSample.Server
{
    class Program
    {
        public static ActorSystem ActorSystem;

        static void Main(string[] args)
        {
            var configuration = GetAkkaConfig();

            CreateActorSystem(configuration);

            //ActorSystem.WhenTerminated.Wait();

        }

        private static Config GetAkkaConfig()
        {
            string sectionName = "akka";
            return ((AkkaConfigurationSection)ConfigurationManager.GetSection(sectionName)).AkkaConfig;
        }

        private static void CreateActorSystem(Config configuration)
        {
            string actorSystemName = ConfigurationManager.AppSettings[Constants.ConfigKeys.ActorSystemName];
            ActorSystem = ActorSystem.Create(actorSystemName, configuration);
        }

    }
}
