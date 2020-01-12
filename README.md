# Reaction Speed Game (.Net Core and Docer and more!)

This repository is for a simple reaction speed game build to learn and demonstrate how docker and .Net Core can be used for fast development of IOT and embedded projects.  The project is also using Node-Red for game interface and also to collect data and storing it in InfluxDB, it also using Grafana to show analytics from game play. And lastly there is two more containers a Mosquitto container to handle communication (MQTT) between the containers, there is also a Portainer container to monitor and handle the docker containers in a easy way. 

The hardware used in this project is the [Raspberry Pi Reaction Speed Game Kit](https://thepihut.com/products/raspberry-pi-reaction-speed-game-kit), it's easy to assamble and they also provide a Phyton script for playing the game. But was is the fun to use that and also I don't like Phyton so we have made ours own .Net Core code for it instead. I also wanted to add some IOT and analytics to the project.

I also wanted to use Docker containers for the hole project and for handling the data collection I used Node-Red because it is relay easy to use and you could do a lot of cool things in a short time.

To also make it easy to deploy to the Raspberry Pi I used Docker compose to create all the needed containers and for the Node-Red container I also pre-installed all the extra function I wanted by build a custom image based on the official image.

    FROM nodered/node-red:latest
    RUN for addonnodes in \
    node-red-dashboard \
    node-red-contrib-influxdb \
    node-red-contrib-ui-led \
    node-red-node-rbe \
    node-red-node-tail \
    ; do \
    npm install ${addonnodes} ;\
    done;
    
I made script for all the different tasks to simplify the handling of the docker compose and other docker tasks. (Are under /scripts)

**buildAndRun** - *Build and run the .Net Core Speed-game, with simulated buttons and MQTT.* 
**buildAndRunGpio -** *The same as above but is not simulating the hardware, should only be used on the rpi.*
**start -** *Starting and creating the containers (Also building out .Net Core code).*
**stop -** *Stops the containers made by the docker compose file*

*There are a lot more scripts but these are the most impotent.*

> To get this running on the Raspberry Pi  you just clone this repository and run the script ./scripts/start.sh and that's it. *(All the needed containers will be downloaded and built and started automatically)* 

I developed the hole program and the IOT parts before assembling the hardware, I used Docker Desktop to run the containers on my Windows computer. To simulating the hardware I simulating the input I used keyboard numbers 1-5 and outputting the button LED states to the console output. When running the game on my developing computer I comment the speed-game part from the docker compose file. So I easier could build the .Net Core code and starting it with the simulation (-s) parameter added. The -m parameter is if the game should publish MQTT information, that information is used for the IOT part. (Used the /scripts/windows/buildAndRun.bat script)

*docker build -t patrikbichis/speed-game .
docker run -it patrikbichis/speed-game **-s** **-m***

Then when we build and run the code on the Raspberry Pi we connect the GPIO pins on the host to the container and removing the -s parameter.

*docker build -t patrikbichis/speed-game .
docker run **--device /dev/gpiomem** -it patrikbichis/speed-game -m*

# The game play
The game will startup automatically on the Raspbarry Pi and flashing all the LED's when it's starting up. The white LED will light when the game is ready to start playing. The user enter some user data as bellow and then starting the game by pressing the white LED button. *(If the user don't enter any data it will run on the last   
input data.)*

 - Name 
 - Gender
 - Age
	 - 0 - 5 years
	 - 5 - 10 years
	 - 10 - 20 years
	 - 20 - 30 years
	 - 30 - 40 years
	 - etc.

Then game will light up all the buttons and count down by turning off a button each second. *(There will also be a voice telling you to get ready and also go go go! when the countdown is done)*The countdown is for 5 seconds after that a random button is lighted up and you should press it as fast as possible. You get score based on how fast you are, if you press the wrong button you will get a penalty and get a score reduction.

You need to repeat that 10 times and then all the buttons start to light up and turning off randomly and a voice will tell you how you did.

You will see how well you did for each button press and also you total time an score, you can also press the high score button to see how you did against all others how played this game.

![enter image description here](https://github.com/PatrikBichis/dotnet-core-reaction-speed-game/raw/master/docs/gameui.JPG)

You access the game user interface by a web browser at the following address [http://\[hostname/ip\]:1880/ui](http://hostname_ip:1880/ui).

# Containers

We are using the following containers beside the one we built by the .Net Core code. To access the different containers from each container we use the container name. (You see it in the docker compose file) For example if you should connect to the MQTT broker you use **mosquitto**. 

 - Node-red
 - Portainer
 - InfluxDB
 - Graphana
 - Mosquitto 

## Portainer 

Portainer is used to give you a easy to use graphical interface for interacting with the containers and also see the health of all the containers.
![enter image description here](https://github.com/PatrikBichis/dotnet-core-reaction-speed-game/raw/master/docs/portainer1.JPG)
![enter image description here](https://github.com/PatrikBichis/dotnet-core-reaction-speed-game/raw/master/docs/portainer2.JPG)
### Accessing
http://hostname/ip:9000
First time you access you get to create a local account to access the portainer in the future.

## Node-Red
We use Node-red to handle all the MQTT data that are sent from the Speed-game, we also use Node-Red to create the game user interface. Node-red will store the data in InfluxDB to be used for analytics.
![enter image description here](https://github.com/PatrikBichis/dotnet-core-reaction-speed-game/raw/master/docs/nodered1.JPG)
### Accessing
[http://hostname_ip:1880](http://hostname_ip:1880) (For programing ui)
[htto://hostname_ip:1880/ui](htto://hostname_ip:1880/ui) (For game ui, dashboard)
