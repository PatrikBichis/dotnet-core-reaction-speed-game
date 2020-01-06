using System;
using System.Threading;
using System.Threading.Tasks;
using MQTTnet;
using MQTTnet.Client.Options;
using MQTTnet.Extensions.ManagedClient;

namespace dotnet_core_reaction_speed_game{
    public class MqttSerivce : IInformationService
    {
        private MqttFactory factory;
        private IManagedMqttClient mqttClient;
        private IManagedMqttClientOptions options;
        public MqttSerivce()
        {
            // Create a new MQTT client.
            factory = new MqttFactory();

            // Setup and start a managed MQTT client.
            options = new ManagedMqttClientOptionsBuilder()
                .WithAutoReconnectDelay(TimeSpan.FromSeconds(5))
                .WithClientOptions(new MqttClientOptionsBuilder()
                    .WithClientId("ReactionSpeedGame")
                    .WithTcpServer("192.168.1.199")
                    .Build())
                .Build();

            mqttClient = factory.CreateManagedMqttClient();
            
        }
    
        public Task Init(){
            return mqttClient.StartAsync(options);
        }

        // public async Task<MQTTnet.Client.Connecting.MqttClientAuthenticateResult> Connect(){
            // try{
            //     var status = await mqttClient.StartAsync(options);
            //     //var status = await mqttClient.ConnectAsync(options, CancellationToken.None);
            //     return status;
            // }catch(Exception ex){
            //     Console.WriteLine(ex);
            //     return null;
            // }
        // }

        public void SendMsg(string topic, string message){
            Console.WriteLine("Sending topic {0}.", topic);
            Task.Run(() => mqttClient.PublishAsync(topic, message));
        }

        public bool GetConnectionStatus(){
            return mqttClient.IsConnected;
        }
    }

}