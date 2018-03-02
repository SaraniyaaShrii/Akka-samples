using Akka.Actor;
using AkkaAggregatorPattern.Data;
using AkkaAggregatorPattern.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AkkaAggregatorPattern.Actors
{
    public class FundActor : ReceiveActor
    {
        private IActorRef Client;
        private FundData FundAttribData;
        private double ContextTimeout = 30;
        private string consoleWriterActorKey = "consoleWriterActor";

        public FundActor()
        {
            Context.SetReceiveTimeout(TimeSpan.FromSeconds(ContextTimeout));
            //Receive<DataPerFundReqMsg>(ProcessFundAttributionData());
            //Receive<DataPerFundReqMsg>(msg => ProcessFundAttributionData(msg));

            //Become(AggregateSecurities);

            Receive<DataPerFundReqMsg>(msg => ProcessFundAttributionData(msg));

            Receive<SecurityData>(msg =>
            {
                FundAttribData.Securities.Add(msg);
            });

            Receive<ReceiveTimeout>(msg =>
            {
                if (Client != null)
                {
                    Client.Tell(FundAttribData);
                }

                Context.Stop(Self);
            });
        }

        //private Action<DataPerFundReqMsg> ProcessFundAttributionData()
        //{
        //    return msg =>
        //    {
        //        //Client = Sender;
        //        if (msg.Extras.ContainsKey(consoleWriterActorKey))
        //        {
        //            Client = msg.Extras[consoleWriterActorKey] as IActorRef;
        //        }
        //        CreateGlobalFundData(msg);
        //        ProcessSecuritiesData(msg);
        //    };
        //}

        private void ProcessFundAttributionData<T>(T message)
        {
            //Client = Sender;
            var msg = message as DataPerFundReqMsg;
            if (msg.Extras.ContainsKey(consoleWriterActorKey))
            {
                Client = msg.Extras[consoleWriterActorKey] as IActorRef;
            }
            CreateGlobalFundData(msg);
            ProcessSecuritiesData(msg);
        }

        private void AggregateSecurities()
        {
            Receive<DataPerFundReqMsg>(msg => ProcessFundAttributionData(msg));

            Receive<SecurityData>(msg =>
            {
                FundAttribData.Securities.Add(msg);
            });

            Receive<ReceiveTimeout>(msg =>
            {
                if (Client != null)
                {
                    Client.Tell(FundAttribData);
                }

                Context.Stop(Self);
            });

        }

        private void ProcessSecuritiesData(DataPerFundReqMsg msg)
        {
            HashSet<int> securityIds = GetSecurityIds(msg.FundId);

            foreach (var securityId in securityIds)
            {
                var securityActorProps = Props.Create(() => new SecurityActor());
                IActorRef securityActor = Context.ActorOf(securityActorProps);
                var dataPerSecurityReqMsg = GetRequestMsg(msg, securityId);
                securityActor.Tell(dataPerSecurityReqMsg);
            }
        }

        private void CreateGlobalFundData(DataPerFundReqMsg msg)
        {
            FundAttribData = new FundData()
            {
                Id = msg.FundId,
                Name = string.Format("Fund_{0}", msg.FundId),
                Securities = new List<SecurityData>()
            };
        }

        private static DataPerSecurityReqMsg GetRequestMsg(DataPerFundReqMsg msg, int securityId)
        {
            return new DataPerSecurityReqMsg()
            {
                FundId = msg.FundId,
                SecurityId = securityId
            };
        }

        public HashSet<int> GetSecurityIds(int fundId)
        {
            int securityCount = AttributionDataProvider.GetFundSecurityCount(fundId);
            HashSet<int> securityIds = AttributionDataProvider.GetRandomNumbers(new Random(), 1, 100, securityCount);
            return securityIds;
        }

    }
}
