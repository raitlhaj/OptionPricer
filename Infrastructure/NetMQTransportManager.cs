using NetMQ;
using NetMQ.Sockets;
using OptionPricerDomain;


namespace Infrastructure
{
    public class NetMQTransportManager {
        private readonly string address;
        private readonly int port;
        private readonly ISerialization serialiser;

        public NetMQTransportManager(string address, int port, ISerialization serialiser)
        {
            this.address = address;
            this.port = port;
            this.serialiser = serialiser;
        }

        public Option PriceOptionUsingNetMQ(Option option)
        {
            //
            var fullAddress = $">tcp://{address}:{port}";

            using (var requestSocket = new RequestSocket(fullAddress))
            {
                    Console.WriteLine("Client is sending Hello: PriceOptionUsingNetMQ !");
                    string OptionJSON = this.serialiser.Serialize<Option>(option);

                    requestSocket.SendFrame(OptionJSON);
                    var message = requestSocket.ReceiveFrameString();
                    Console.WriteLine(message);
                    Thread.Sleep(2000);
                    var optionDeserialised = this.serialiser.Deserialize<Option>(message);

                return optionDeserialised;
               
            }
            //

        }
    
     }

}

