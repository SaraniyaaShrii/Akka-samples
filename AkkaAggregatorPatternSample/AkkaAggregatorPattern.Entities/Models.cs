using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AkkaAggregatorPattern.Entities
{
    public class FundData
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public List<SecurityData> Securities { get; set; }
    }

    public class SecurityData
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public Dictionary<DateTime, AttributionData> AttributionDataForDates { get; set; }
    }

    public class AttributionData
    {
        public DateTime ContextDate { get; set; }

        public double Stat1 { get; set; }

        public double Stat2 { get; set; }
    } 
}
