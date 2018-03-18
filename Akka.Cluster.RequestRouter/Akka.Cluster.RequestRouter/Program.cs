using Akka.Actor;
using Akka.Configuration;
using Akka.Configuration.Hocon;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Akka.Cluster.Common;

namespace Akka.RequestRouter
{
    class Program
    {
        public static ActorSystem ActorSystem;

        static void Main(string[] args)
        {
            var configuration = GetAkkaConfig("akka");
            //configuration = configuration.WithFallback(GetAkkaConfig("akka/akka.logging"))
            //  .WithFallback(GetAkkaConfig("akka/akka.remote"))
            //  .WithFallback(GetAkkaConfig("akka/akka.cluster"));

            CreateActorSystem(configuration);
        }

        private static Config GetAkkaConfig(string sectionName)
        {
            return ((AkkaConfigurationSection)ConfigurationManager.GetSection(sectionName)).AkkaConfig;
        }

        private static void CreateActorSystem(Config configuration)
        {
            string actorSystemName = ConfigurationManager.AppSettings[Constants.ConfigKeys.ActorSystemName];
            ActorSystem = ActorSystem.Create(actorSystemName, configuration);
        }
    }
}
