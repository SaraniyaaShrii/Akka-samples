using Akka.Actor;
using AkkaAggregatorPattern.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Akka.Cluster.Common.Messages;

namespace AkkaAggregatorPattern.Actors
{
    public class ProcessOnsetActor : ReceiveActor
    {
        public ProcessOnsetActor()
        {
            Receive<AttributionRequestMsg>(msg => ProcessRequest(msg));

            Receive<FundData>(msg => );
        }

        public void ProcessRequest(AttributionRequestMsg requestMsg)
        {
            var fundActorProps = Props.Create(() => new FundActor());
            IActorRef fundActor = Context.ActorOf(fundActorProps, "FundActor");
            var consoleWriterActorProps = Props.Create(() => new ConsoleWriterActor());
            IActorRef consoleWriterActor = Context.ActorOf(consoleWriterActorProps, "ConsoleWriterActor");

            int fundId = requestMsg.FundId;

            DataPerFundReqMsg msg = new DataPerFundReqMsg()
            {
                FundId = fundId,
                Extras = new Dictionary<string, object>()
                {
                    { "consoleWriterActor", consoleWriterActor }
                }
            };

            fundActor.Tell(msg);
        }
        
    }
}
