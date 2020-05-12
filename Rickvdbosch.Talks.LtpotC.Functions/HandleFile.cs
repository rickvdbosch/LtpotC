using System;
using System.IO;
using System.Threading.Tasks;

using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.ServiceBus;
using Microsoft.Extensions.Logging;

namespace Rickvdbosch.Talks.LtpotC.Functions
{
    public static class HandleFile
    {
        [FunctionName("HandleFile")]
        public static async Task Run(
            [BlobTrigger("to-process/{name}", Connection = "StorageConnectionString")] Stream addedBlob,
            [ServiceBus("process", Connection = "ServiceBusConnectionSend", EntityType = EntityType.Queue)] ICollector<string> queueCollector,
            string name, ILogger log)
        {
            using var reader = new StreamReader(addedBlob);
            var original = await reader.ReadToEndAsync();
            original = original.Replace(",", "").Replace(".", "");
            var words = original.Split(' ', StringSplitOptions.RemoveEmptyEntries);

            foreach (var word in words)
            {
                queueCollector.Add(word);
            }
        }
    }
}