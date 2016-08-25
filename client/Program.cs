using System;
using System.ServiceModel;
using System.Threading.Tasks;
using System.Threading;

namespace Client
{
    [ServiceContract]
    public interface IMyObject
    {
        [OperationContract]
        void send(string nick, string str);
        [OperationContract]
        string checkMes(string nick);
    }

    class Program
    {
        static void Main(string[] args)
        {
            Uri tcpUri = new Uri("http://localhost:8080/signals");
            EndpointAddress address = new EndpointAddress(tcpUri);
            BasicHttpBinding binding = new BasicHttpBinding();
            ChannelFactory<IMyObject> factory = new ChannelFactory<IMyObject>(binding, address);
            IMyObject service = factory.CreateChannel();

            string myNick = "s";
            string Nick = "k";


            Task autoRefresh = new Task(() => 
            {
                while(true)
                {
                    Console.Write(service.checkMes(myNick));
                    Thread.Sleep(500);
                }
            });
            autoRefresh.Start();
            while (true)
            {
                
                string mes = Console.ReadLine();
                service.send(Nick, mes);
            }
        }
    }
}