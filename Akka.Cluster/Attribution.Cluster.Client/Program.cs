using Akka.Actor;
using Akka.Cluster.Common;
using Akka.Configuration.Hocon;
using Akka.Routing;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Attribution.Cluster.Client
{
    class Program
    {
        static void Main(string[] args)
        {
            string actorSystemName = ConfigurationManager.AppSettings[Constants.ConfigKeys.ActorSystemName];
            //string sectionName = "akka";
            //var configuration = ((AkkaConfigurationSection)ConfigurationManager.GetSection(sectionName)).AkkaConfig;
            var actorSystem = ActorSystem.Create(actorSystemName);//, configuration);

            var requestHandlerActor = actorSystem.ActorOf(Props.Empty.WithRouter(FromConfig.Instance), "requestHandlerActor");
            var attribReq = new Akka.Cluster.Common.Messages.AttributionRequest()
            {
                FundId = 1,
            };

            requestHandlerActor.Tell(attribReq);


            actorSystem.WhenTerminated.Wait();
        }
    }
}
