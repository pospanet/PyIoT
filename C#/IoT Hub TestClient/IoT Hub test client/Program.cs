using System;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Azure.Devices.Client;
using Newtonsoft.Json;

namespace Pospa.NET.IoThubTest
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            string deviceId = "";
            DeviceClient client =
                DeviceClient.CreateFromConnectionString(
                    "<put device connection string here>");
            int i = 0;
            while (true)
            {
                var data = new { Name = deviceId, Value = i++, Date = DateTime.UtcNow };
                string jsonData = JsonConvert.SerializeObject(data);
                byte[] dataBuffer = Encoding.UTF8.GetBytes(jsonData);
                Message message = new Message(dataBuffer);
                Task task = client.SendEventAsync(message);
                Task.WhenAll(task);
                Thread.Sleep(TimeSpan.FromSeconds(1));
            }
        }
    }
}