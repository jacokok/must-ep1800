# Must EP1800

This is a project to get JSON information from Must EP1800

This is different enough to not fork but most code comes from [github](https://github.com/dylangmiles/docker-must-homeassistant)

## Docker

Make sure the user has permissions to access /dev/ttyUSB0. Could also run in privileged but not recommended.

```bash
sudo usermod -a -G dialout $USER # might have to logout or reboot after this
# Run every 30 seconds
docker run -d --device=/dev/ttyUSB0 -e MUST_Config__Cron='0/30 * * * * ?' -e MUST_CONFIG__MqttServer='serverNameOrIP' -e MUST_CONFIG__MqttUserName='username' -e MUST_CONFIG__MqttPassword='pass' doink/must-ep1800
```

## Docker Compose

```yaml
version: "3"
services:
  must-ep1800:
    image: doink/must-ep1800
    hostname: must-ep1800
    container_name: must-ep1800
    restart: always
    environment:
      MUST_Config__Cron: "0/2 * * * * ?"
      MUST_Config__IsTest: false
      MUST_Config__PortName: "/dev/ttyUSB0"
      MUST_CONFIG__MqttServer: 191.323.23.23
      MUST_Serilog__MinimumLevel: "INFO"
    devices:
      - /dev/ttyUSB0:/dev/ttyUSB0:rwm
    # privileged: true
```

## Env

[quartz]: https://www.quartz-scheduler.net/documentation/quartz-3.x/tutorial/crontriggers.html#example-cron-expressions

| Env                         | Description                                                  |
| --------------------------- | ------------------------------------------------------------ |
| MUST_Config__Cron           | Quartz cron expression. [Docs][quartz]                       |
| MUST_Config__IsTest         | Run in test mode with serial logging and scheduling disabled |
| MUST_Config__PortName       | Location where usb serial device is mounted. /dev/ttyUSB0    |
| MUST_CONFIG__MqttServer     | Mqtt Server                                                  |
| MUST_CONFIG__MqttPort       | Mqtt Port. Default: 1883                                     |
| MUST_CONFIG__MqttTopic      | Default: homeassistant                                       |
| MUST_CONFIG__MqttDeviceName | Default: must-inverter                                       |
| MUST_CONFIG__MqttUserName   | Mqtt UserName                                                |
| MUST_CONFIG__MqttPassword   | Mqtt Password                                                |
| MUST_Serilog__MinimumLevel  | Default: Error                                               |

## How to find the port

```bash
# Run after inserting usb device to see if it was found
lsusb
# Find ttyUSB devices
ls /dev | grep ttyUSB
```
