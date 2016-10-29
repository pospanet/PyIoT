using System.Net;
using System;
using System.Threading;
using Microsoft.Azure.Devices.Client;
using Newtonsoft.Json;

public static async Task<HttpResponseMessage> Run(HttpRequestMessage req, TraceWriter log)
{
    log.Info($"C# HTTP trigger function processed a request. RequestUri={req.RequestUri}");

    // parse query parameter
    string valueString = req.GetQueryNameValuePairs()
        .FirstOrDefault(q => string.Compare(q.Key, "value", true) == 0)
        .Value;
    string name = req.GetQueryNameValuePairs()
        .FirstOrDefault(q => string.Compare(q.Key, "name", true) == 0)
        .Value;

    // Get request body
    dynamic requestData = await req.Content.ReadAsAsync<object>();

    // Set name to query string or body data
    valueString = valueString ?? requestData?.valueString;
    name = name ?? requestData?.name;

    int value = -1;
    int.TryParse(valueString, out value);

    DeviceClient client =
        DeviceClient.CreateFromConnectionString(
            "<device connection string>");

    var data = new { Name = name, Value = value, Date = DateTime.UtcNow };
    string jsonData = JsonConvert.SerializeObject(data);
    byte[] dataBuffer = System.Text.Encoding.UTF8.GetBytes(jsonData);
    Message message = new Message(dataBuffer);
    Task task = client.SendEventAsync(message);

    while (!task.IsCompleted)
    {
        Thread.Sleep(100);
    }

    return (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(valueString))
        ? req.CreateResponse(HttpStatusCode.BadRequest, "Please pass a name, value on the query string or in the request body")
        : req.CreateResponse(HttpStatusCode.OK, "Hi " + name + ". I have received value: " + value);
}