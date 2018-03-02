using Akka.Actor;
using AkkaAggregatorPattern.Actors;
using AkkaAggregatorPattern.Entities;
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

            //Console.WriteLine("Fund Id : ");
            int fundId = 1;// Convert.ToInt32(Console.ReadLine());

            DataPerFundReqMsg msg = GetMessage(consoleWriterActor, fundId);

            fundActor.Tell(msg);

            MyActorSystem.WhenTerminated.Wait();
        }

        private static DataPerFundReqMsg GetMessage(IActorRef consoleWriterActor, int fundId)
        {
            DataPerFundReqMsg msg = new DataPerFundReqMsg()
            {
                FundId = fundId,
                Extras = new Dictionary<string, object>()
                {
                    { "consoleWriterActor", consoleWriterActor }
                }
            };

            return msg;
        }

        public void Handler(IActorRef consoleWriterActor)
        {
            //Receive<FundData>(msg => 
            //{
            //    consoleWriterActor.Tell("Write");
            //});
        }
    }
}
