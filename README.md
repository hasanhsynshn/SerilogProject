In this project, a logging mechanism was established using serilog and monitored in graylog.

First, you can download docker desktop to install graylog. Afterwards, you can check that it is installed with the docker --version command from Powershell. After installing, you can install the relevant elasticsearch, mongo and graylog by using the docker-compose up command in Powershell.
After the installation, go to the system section on the port 127.0.01:9000, select the inputs from there and make the settings.
Then apply the Graylog settings in the Program.cs file. To do this, you must first download the graylog package from the nuget manager. You must also download the relevant sinks. You can look at the packages section of the project. Then, you can see these values ​​in graylog by logging as in the controller. Thank you for reading.

