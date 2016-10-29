import socket;
from time import sleep_ms;
import network;

#WiFi config
ssid = "";
pwd = "";

#Device ID
deviceId = "";

#Azure Function settings
functionName = "";
functionKey = "";
functionHost = ""

values = "&name={0}&value={1}"

#Global settings
httpVerb = "POST http://{0}/api/{1}?code={2}{3} HTTP/1.1";
baseHost = "{0}.azurewebsites.net";

nic=network.WLAN(network.STA_IF);
nic.active(True);
nic.connect(ssid, pwd);
print(nic.ifconfig());

host = baseHost.format(functionHost);
port = 80;
print("Address: {0} port: {1}".format(host, port));
address = socket.getaddrinfo(host, port);
ip = address[0][4];

print(ip);

s = socket.socket(socket.AF_INET, socket.SOCK_STREAM);
s.connect(ip);

i = 0;
    
while True:
    i = i + 1;
    url = httpVerb.format(host, functionName, functionKey, values.format(deviceId, str(i)));
    print(url);
    s.send(url);
    sleep_ms(1000);

print("Done.");