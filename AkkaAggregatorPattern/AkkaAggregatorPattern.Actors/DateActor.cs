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
    public class DateActor : ReceiveActor
    {
        private const string RandomInstanceKey = "RandomInstance";

        public DateActor()
        {
            Receive<DataPerDateReqMsg>(GetAttributionData());
        }

        private Action<DataPerDateReqMsg> GetAttributionData()
        {
            return msg =>
            {
                Random randomInstance = (msg.Extras != null && msg.Extras.ContainsKey(RandomInstanceKey)) ? msg.Extras[RandomInstanceKey] as Random : new Random();
                var attribDataPerDate = AttributionDataProvider.GetAttributionDataForADate(randomInstance, msg.FundId, msg.SecurityId, msg.ContextDate);
                Sender.Tell(attribDataPerDate);
            };
        }
    }
}
