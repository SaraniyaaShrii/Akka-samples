using AkkaAggregatorPattern.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AkkaAggregatorPattern.Data
{
    public class AttributionDataProvider
    {
        public static FundData GetAttributionData(int fundId)
        {
            var attributionData = StaticDataProvider.GetAttributionDataForFund(fundId);
            return attributionData;
        }
    }
}
