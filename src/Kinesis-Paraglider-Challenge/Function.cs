using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;

using Newtonsoft.Json;

using Amazon.Lambda.Core;
using Amazon.Lambda.KinesisEvents;
using Amazon.SimpleNotificationService;
using Amazon.SimpleNotificationService.Model;

// Assembly attribute to enable the Lambda function's JSON input to be converted into a .NET class.
[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.Json.JsonSerializer))]
namespace Kinesis_Paraglider_Challenge
{
    public class Function
    {
        private const string SNS_ARN = "";
        private AmazonSimpleNotificationServiceClient _snsClient;

        public Function()
        {
            _snsClient = new AmazonSimpleNotificationServiceClient();
        }

        public async Task FunctionHandler(KinesisEvent kinesisEvent, ILambdaContext context)
        {
            context.Logger.LogLine($"Beginning to process {kinesisEvent.Records.Count} records...");

            foreach (var record in kinesisEvent.Records)
            {
                string recordContents = GetRecordContents(record.Kinesis);
                var recordData = JsonConvert.DeserializeObject<Event>(recordContents);
                if (IsGoodParaglidingWeather(recordData))
                {
                    context.Logger.LogLine($"Found excellent paragliding weather!: {recordContents}");
                    await _snsClient.PublishAsync(SNS_ARN, $"Holy shit we are good to fly!! Travel to {recordData.location.areaDescription} at approximately {recordData.timestamp}");
                }
            }
            context.Logger.LogLine("Stream processing complete.");
        }

        private string GetRecordContents(KinesisEvent.Record streamRecord)
        {
            using (var reader = new StreamReader(streamRecord.Data, Encoding.ASCII))
            {
                return reader.ReadToEnd();
            }
        }

        private bool IsGoodParaglidingWeather(Event weatherEvent) {
            if (weatherEvent.location.areaDescription.Contains("Torrey Pines")
                && Convert.ToInt32(weatherEvent.relativeHumidity) <= 80
                && Convert.ToInt32(weatherEvent.windDirection.value) >= 230
                && Convert.ToInt32(weatherEvent.windDirection.value) <= 290
                && Convert.ToInt32(weatherEvent.windSpeed.value) >= 6
                && Convert.ToInt32(weatherEvent.windSpeed.value) <= 12
                && Convert.ToInt32(weatherEvent.gust.value) <= 20)
            {
                return true;
            }
            return false;
        }
    }
}
