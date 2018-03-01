using AkkaAggregatorPattern.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AkkaAggregatorPattern.Data
{
    public static class StaticDataProvider
    {
        public static Dictionary<int, int> FundSecurities = new Dictionary<int, int>()
        {
            { 1, 4 },
            { 2, 5 },
            { 3, 2 },
            { 4, 3 },
        };

        private static List<DateTime> Dates = new List<DateTime>() { new DateTime(2018, 2, 28), new DateTime(2018, 2, 27), new DateTime(2018, 2, 26) };

        public static FundData GetAttributionDataForFund(int fundId)
        {
            var securityCount = StaticDataProvider.FundSecurities[fundId];
            var securitiesAttribData = StaticDataProvider.GetAttributionDataForSecurities(securityCount);

            var fundData = new FundData()
            {
                Id = fundId,
                Name = string.Format("Fund_{0}", fundId),
                Securities = securitiesAttribData
            };

            return fundData;
        }

        private static List<SecurityData> GetAttributionDataForSecurities(int securityCount)
        {
            var securities = new List<SecurityData>();

            var randomIds = GetRandomNumbers(new Random(), 1, 10, securityCount);

            foreach (var randomId in randomIds)
            {
                var security = new SecurityData();
                security.Id = randomId;
                security.Name = string.Format("Security_{0}", randomId);
                security.AttributionDataForDates = GetAttributionDataForDates();

                securities.Add(security);
            }

            return securities;
        }

        private static Dictionary<DateTime, AttributionData> GetAttributionDataForDates()
        {
            var dataForDates = new Dictionary<DateTime, AttributionData>();

            var randomInstance = new Random();

            foreach (var date in Dates)
            {
                GetAttributionData(dataForDates, randomInstance, date);
            }
            return dataForDates;
        }

        private static void GetAttributionData(Dictionary<DateTime, AttributionData> dataForDates, Random randomInstance, DateTime date)
        {
            var data = new AttributionData();
            data.ContextDate = date;
            data.Stat1 = GetRandomNumber(randomInstance, 1000, 10000);
            data.Stat1 = GetRandomNumber(randomInstance, 0, 100);

            dataForDates.Add(date, data);
        }

        public static int GetRandomNumber(Random instance, int minValue, int maxValue)
        {
            int randomNumber = instance.Next(minValue, maxValue);
            return randomNumber;
        }


        private static HashSet<int> GetRandomNumbers(Random instance, int minValue, int maxValue, int count)
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
