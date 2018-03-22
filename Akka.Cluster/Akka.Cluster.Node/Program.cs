using Akka.Actor;
using Akka.Cluster.Common;
using Akka.Configuration;
using Akka.Configuration.Hocon;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Akka.Cluster.Node
{
    class Program
    {
        public static ActorSystem ActorSystem;

        static void Main(string[] args)
        {
            var configuration = GetAkkaConfig();

            CreateActorSystem(configuration);

            //ActorSystem.WhenTerminated.Wait();

            Console.WriteLine("Finished");
            Console.ReadLine();
            Console.ReadLine();
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
