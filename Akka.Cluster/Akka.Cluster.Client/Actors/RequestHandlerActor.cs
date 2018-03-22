using Akka.Actor;
using Akka.Cluster.API.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Akka.Cluster.API.Actors
{
    public class RequestHandlerActor : ReceiveActor
    {
        private readonly IActorRef _consoleWriterActor;

        public RequestHandlerActor(IActorRef consoleWriterActor)
        {
            _consoleWriterActor = consoleWriterActor;

            Receive<AttributionRequest>(requestMsg => 
            {
                //var consoleWriterProps = Props.Create(() => new ConsoleWriterActor());
                //IActorRef consoleWriterActor = Context.ActorOf(consoleWriterProps, "ConsoleWriterActor");

                string consoleMsg = string.Format("Requesting Fund: {0}", requestMsg.FundId);
                consoleWriterActor.Tell(consoleMsg);
            });
        }
    }
}
