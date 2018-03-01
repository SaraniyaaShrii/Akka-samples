using Akka.Actor;
using AkkaAggregatorPattern.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AkkaAggregatorPattern.Actors
{
    public class SecurityActor : ReceiveActor
    {
        private const string RandomInstanceKey = "RandomInstance";

        public SecurityActor()
        {
            Receive<DataPerSecurityReqMsg>(GetDataForSecurity());

            Receive<AttributionData>(msg =>
            {
                // Sender.Tell();
            });
        }

        private Action<DataPerSecurityReqMsg> GetDataForSecurity()
        {
            return msg =>
            {
                Random randomInstance = (msg.Extras != null && msg.Extras.ContainsKey(RandomInstanceKey)) ? msg.Extras[RandomInstanceKey] as Random : new Random();
                var dates = GetDates();

                foreach (var date in dates)
                {
                    var dateActorProps = Props.Create(() => new ConsoleWriterActor());
                    IActorRef dateActor = Context.ActorOf(dateActorProps, "dateActor");
                    DataPerDateReqMsg dataPerDateReqmsg = GetRequestMsg(msg, date);
                }

            };
        }

        private static DataPerDateReqMsg GetRequestMsg(DataPerSecurityReqMsg msg, DateTime date)
        {
            return new DataPerDateReqMsg()
            {
                FundId = msg.FundId,
                SecurityId = msg.SecurityId,
                ContextDate = date
            };
        }

        private List<DateTime> GetDates()
        {
            List<DateTime> dates = new List<DateTime>() { new DateTime(2018, 2, 28), new DateTime(2018, 2, 27), new DateTime(2018, 2, 26) };
            return dates;
        }

    }
}
