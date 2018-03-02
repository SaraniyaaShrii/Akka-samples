using Akka.Actor;
using AkkaAggregatorPattern.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AkkaAggregatorPattern.Actors
{
    public class ConsoleWriterActor : ReceiveActor
    {
        public ConsoleWriterActor()
        {
            Receive<FundData>(msg => WriteToConsole(msg));
        }

        private void WriteToConsole<T>(T msg)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Attribution data processed for fund {0}", (msg as FundData).Id);
            Console.ResetColor();
        }
    }

    public class TextFileWriterActor : ReceiveActor
    {
        public TextFileWriterActor()
        {
            ReceiveAny(msg => WriteToFile(msg));
        }

        private void WriteToFile<T>(T msg)
        {
            string filePath = "B:\\Prototypes\\Akka.net\\Akka-samples\\SampleFiles";
            if (msg is FundData)
            {
                var data = msg as FundData;
                filePath = string.Format("{0}\\Fund_{1}.txt", filePath, data.Id);
            }
            else
            {
                filePath = string.Format("{0}\\Sample.txt", filePath);
            }

            File.WriteAllText(filePath, msg.ToString());
        }
    }

}
