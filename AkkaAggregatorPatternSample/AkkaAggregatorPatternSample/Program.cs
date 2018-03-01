using Akka.Actor;
using AkkaAggregatorPattern.Actors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AkkaAggregatorPatternSample
{
    class Program
    {
        public static ActorSystem MyActorSystem;

        static void Main(string[] args)
        {
            MyActorSystem = ActorSystem.Create("AggregatorActorSystem");

            var fundActorProps = Props.Create(() => new FundActor());
            var consoleWriterActorProps = Props.Create(() => new ConsoleWriterActor());

            IActorRef fundActor = MyActorSystem.ActorOf(fundActorProps, "fundActor");
            IActorRef consoleWriterActor = MyActorSystem.ActorOf(consoleWriterActorProps, "consoleWriterActor");

            Console.WriteLine("Fund Id : ");
            int fundId = Convert.ToInt32(Console.ReadLine());

            fundActor.Tell(fundId);

            MyActorSystem.WhenTerminated.Wait();
        }
    }
}
