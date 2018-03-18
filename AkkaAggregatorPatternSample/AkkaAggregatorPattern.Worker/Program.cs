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

namespace AkkaAggregatorPatternSample.Server
{
    class Program
    {
        public static ActorSystem ActorSystem;

        static void Main(string[] args)
        {
            string actorSystemName = ConfigurationManager.AppSettings[Constants.ConfigKeys.ActorSystemName];
            ActorSystem = ActorSystem.Create(actorSystemName);

            ActorSystem.WhenTerminated.Wait();
        }

        //static void Main(string[] args)
        //{
        //    string actorSystemName = ConfigurationManager.AppSettings[Constants.ConfigKeys.ActorSystemName];
        //    ActorSystem = ActorSystem.Create(actorSystemName);

        //    var fundActorProps = Props.Create(() => new FundActor());
        //    var consoleWriterActorProps = Props.Create(() => new ConsoleWriterActor());

        //    IActorRef fundActor = ActorSystem.ActorOf(fundActorProps, "fundActor");
        //    IActorRef consoleWriterActor = ActorSystem.ActorOf(consoleWriterActorProps, "consoleWriterActor");

        //    RequestManager manager = new RequestManager();
        //    manager.ProcessRequest(fundActor, consoleWriterActor);

        //    ActorSystem.WhenTerminated.Wait();
        //}

    }
}
