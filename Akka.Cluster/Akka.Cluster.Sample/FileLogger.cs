using Akka.Actor;
using Akka.Event;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AkkaCluster
{
    public class FileLogger : ReceiveActor
    {
        private StreamWriter writer;

        public FileLogger()
        {
            ReceiveAsync<Debug>(async e => await this.LogAsync(e.ToString()));
            ReceiveAsync<Info>(async e => await this.LogAsync(e.ToString()));
            ReceiveAsync<Warning>(async e => await this.LogAsync(e.ToString()));
            ReceiveAsync<Error>(async e => await this.LogAsync(e.ToString()));
            Receive<InitializeLogger>(_ => Sender.Tell(new LoggerInitialized()));
        }

        private async Task LogAsync(string message)
        {
            await this.writer.WriteLineAsync(message);
            await this.writer.FlushAsync();
        }

        protected override void PreStart()
        {
            base.PreStart();

            string filePath = "log.txt";
            filePath = Context.System.Settings.Config.GetString("akka.logfilepath", filePath);
            var fileStream = File.OpenWrite(filePath);
            this.writer = new StreamWriter(fileStream);
        }

        protected override void PostStop()
        {
            // dispose the StreamWriter, and implicitly the
            // underlying FileStream with it
            this.writer.Dispose();

            base.PostStop();
        }
    }
}
