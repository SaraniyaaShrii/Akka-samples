using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Akka.Cluster.Common.Messages
{
    public class AttributionRequestMsg
    {
        public int FundId { get; set; }

        public DateTime ContextDate { get; set; }

        public List<string> Items { get; set; }

        public List<string> Groups { get; set; }
    }
}
