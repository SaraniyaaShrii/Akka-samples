using AkkaAggregatorPattern.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AkkaAggregatorPattern.Data
{
    public static class AttributionDataProvider
    {
        private static Dictionary<int, int> FundSecurities = new Dictionary<int, int>()
        {
            { 1, 4 },
            { 2, 5 },
            { 3, 2 },
            { 4, 3 },
        };

        private static List<DateTime> Dates = new List<DateTime>() { new DateTime(2018, 2, 28), new DateTime(2018, 2, 27), new DateTime(2018, 2, 26) };

        public static int GetFundSecurityCount(int fundId)
        {
            return FundSecurities[fundId];
        }

        private static FundData GetFundData(int fundId, object securitiesAttribData)
        {
            return new FundData()
            {
                Id = fundId,
                Name = string.Format("Fund_{0}", fundId),
                //Securities = securitiesAttribData
            };
        }
        
        private static SecurityData GetSecurityData(int securityId)
        {
            var security = new SecurityData();
            security.Id = securityId;
            security.Name = string.Format("Security_{0}", securityId);
            return security;
        }
        
        public static AttributionData GetAttributionDataForADate(Random randomInstance, int fundId, int securityId, DateTime date)
        {
            var data = new AttributionData();
            data.FundId = fundId;
            data.SecurityId = securityId;
            data.ContextDate = date;
            data.Stat1 = GetRandomNumber(randomInstance, 1000, 10000);
            data.Stat1 = GetRandomNumber(randomInstance, 0, 100);

            return data;
        }

        public static int GetRandomNumber(Random instance, int minValue, int maxValue)
        {
            int randomNumber = instance.Next(minValue, maxValue);
            return randomNumber;
        }

        public static HashSet<int> GetRandomNumbers(Random instance, int minValue, int maxValue, int count)
        {
            HashSet<int> randomNumbers = new HashSet<int>();

            int index = 0;

            while (index < count)
            {
                int randomNum = instance.Next(minValue, maxValue);

                if (randomNumbers.Contains(randomNum) == false)
                {
                    randomNumbers.Add(randomNum);
                    index++;
                }
            }
            return randomNumbers;
        }

    }
}
