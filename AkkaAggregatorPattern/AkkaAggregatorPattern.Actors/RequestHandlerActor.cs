using Akka.Actor;
using Akka.Cluster.Common.Messages;
using Akka.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AkkaAggregatorPattern.Actors
{
    class RequestHandler : ReceiveActor
    {
        public RequestHandler()
        {
            Receive<AttributionRequest>(reqMsg => ProcessRequest(reqMsg));
        }

        private void ProcessRequest(AttributionRequest request)
        {
            var processOnsetActorProps = Props.Create(() => new ProcessOnsetActor());
            IActorRef processOnsetActor = Context.ActorOf(processOnsetActorProps.WithRouter(FromConfig.Instance), "ProcessOnsetActor");

            processOnsetActor.ActorOf(processOnsetActorProps.WithRouter(FromConfig.Instance), "remoteactor1");
        }
    }
}
