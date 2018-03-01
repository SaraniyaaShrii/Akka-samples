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
        public FundActor()
        {
            Receive<DataPerFundReqMsg>(msg => GetFundData(msg));
            Receive<SecurityData>(msg => HandleFundData(msg));
        }

        private void GetFundData(DataPerFundReqMsg msg)
        {
            HashSet<int> securityIds = GetSecurityIds(msg.FundId);

            foreach (var securityId in securityIds)
            {
                var securityActorProps = Props.Create(() => new ConsoleWriterActor());
                IActorRef securityActor = Context.ActorOf(securityActorProps, "securityActor");
                var dataPerSecurityReqMsg = GetRequestMsg(msg, securityId);
                securityActor.Tell(dataPerSecurityReqMsg);

            }
        }

        private void HandleFundData(SecurityData securityData)
        {
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
            HashSet<int> securityIds = AttributionDataProvider.GetRandomNumbers(new Random(), 1, securityCount, securityCount);
            return securityIds;
        }

    }
}
