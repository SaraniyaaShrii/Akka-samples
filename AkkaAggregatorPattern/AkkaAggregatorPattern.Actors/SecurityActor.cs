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
        private IActorRef FundActor;
        private SecurityData SecAttribData;
        private double ContextTimeout = 10;

        public SecurityActor()
        {
            Context.SetReceiveTimeout(TimeSpan.FromSeconds(ContextTimeout));
            Receive<DataPerSecurityReqMsg>(ProcessSecurityAttribData());
            Become(AggregateDates);
        }

        private void AggregateDates()
        {
            Receive<DataPerSecurityReqMsg>(ProcessSecurityAttribData());

            Receive<ReceiveTimeout>(msg =>
            {
                FundActor.Tell(SecAttribData);
                Context.Stop(Self);
            });

            Receive<AttributionData>(msg =>
            {
                SecAttribData.AttributionDataForDates.Add(msg.ContextDate, msg);
            });
        }

        private Action<DataPerSecurityReqMsg> ProcessSecurityAttribData()
        {
            return msg =>
            {
                FundActor = Sender;
                CreateSecurityData(msg);

                Random randomInstance = (msg.Extras != null && msg.Extras.ContainsKey(RandomInstanceKey)) ? msg.Extras[RandomInstanceKey] as Random : new Random();
                var dates = GetDates();

                foreach (var date in dates)
                {
                    var dateActorProps = Props.Create(() => new DateActor());
                    IActorRef dateActor = Context.ActorOf(dateActorProps);
                    DataPerDateReqMsg dataPerDateReqmsg = GetRequestMsg(msg, date);
                    dateActor.Tell(dataPerDateReqmsg);
                }
            };
        }

        private void CreateSecurityData(DataPerSecurityReqMsg msg)
        {
            SecAttribData = new SecurityData()
            {
                Id = msg.SecurityId,
                Name = string.Format("Security_{0}", msg.SecurityId),
                AttributionDataForDates = new Dictionary<DateTime, AttributionData>()
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
