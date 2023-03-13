using Microsoft.Extensions.Configuration;

namespace Axxes.Workshop.AkkaDotNet.App.MessageReader;

public class IotHubSettings
{
    public IotHubSettings(IConfiguration configuration)
    {
        var section = configuration.GetSection("IoTHub");
        EventHubConnectionString = section.GetValue<string>("EventHubConnectionString") 
                                   ?? throw new KeyNotFoundException("EventHub connection string not found");
        BlobStorageConnectionString = section.GetValue<string>("BlobStorageConnectionString") 
                                      ?? throw new KeyNotFoundException("BlobStorage  connection string not found");
    }
    public string EventHubConnectionString { get; }
    public string BlobStorageConnectionString { get; }
}