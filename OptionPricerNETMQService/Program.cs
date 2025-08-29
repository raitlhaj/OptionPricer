// See https://aka.ms/new-console-template for more information

using Infrastructure;
using NetMQ;
using NetMQ.Sockets;
using OptionPricerNetMQService;
using OptionPricerService;

using (var starter = new Starter())
{
    starter.RegisterInstancies(); //register all we need in the starter
    var optionService = starter.Resolve<IOptionService>();
    var log = starter.Resolve<ILogger>();


    using (var responseSocket = new ResponseSocket("@tcp://*:5555"))
    {  
        while (true)
        {
            var message = responseSocket.ReceiveFrameString();

            log.Info($"Server is receiving {message}");
            
            var optionDeserialized = optionService.DeserializeOption(message);
            optionService.EnrichOptionWithPrice(optionDeserialized);
            optionService.PersistOption(optionDeserialized);
            //optionService.UpdateOption(optionDeserialized, optionDeserialized); to do
            //Option price serialization
            string answer = optionService.SerializeOption(optionDeserialized);
            //serialisation & de-serialisation : JSON (String) <=> Objets
            //responseSocket.SendFrame("Hello from Server!");
            log.Info($"\n\n the server is answering : {answer}");

            responseSocket.SendFrame(answer);
        }

    }

}


