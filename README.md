# Python IoT demo

Demo project showing how to use microcontrollers running MicroPython together with Microsoft Azure.

## Environment setup

Reqierements:
- Python 2.7 (includng esptool)
- IDE
- Microsoft Azure SDK

### Python
Dowload and install Python 2.7.x from official site [Python Download site](https://www.python.org/downloads/)
Install [esptool](https://github.com/themadinventor/esptool) using PIP. You may need to run it as sudo under Linux or run console as Administrator under Windows.

### IDE
You will need any suitable IDE based on your preference. I am using [Visual Studio code](https://code.visualstudio.com/) for desktop projects and [ESPlorer](http://esp8266.ru/esplorer/) for codeing and debuging microcontoler. In my case I am using [ESP8266](https://en.wikipedia.org/wiki/ESP8266).

## Flashing ESP8266 with MicroPython
One you have any ESP8266 related board, you have to have MicroPython ROM image to be flashed to the chip. You can find precompiled images [here](http://micropython.org/download), or you can compile your own from [here](https://github.com/micropython/micropython).

1. Erase flash using `esptool`
```
esptool.py --port <specify serial device as /dev/ttyX or COMX> erase_flash
```
2. Install MicroPython
```
esptool.py --port COM3 --baud 460800 write_flash --flash_size 4m 0 esp8266-20161017-v1.8.5.bin
```
..* Replace COM3 with path to your serial device where ESP8266 board is connected
..* baud rate should be lowered if flash is unsuccessful. 115200 should be safe speed.
..* flash_size has to be set to physical device flash memory size.
..* following number is offset where flashing will start. Just use 0.
..* As a last parameter you have to specify location of binary flash file.

### Microsoft Azure SDK
For more advanced scenarios I can recommend [Microsoft Azure SDK](https://azure.microsoft.com/en-us/downloads/) to do rapid development. You can also find some examples and utilities [here](https://github.com/Azure/azure-iot-sdks).

## Azure functions
For implementation of [Azure Function](https://azure.microsoft.com/en-us/services/functions/) see [here](./C#/Azure Function) 