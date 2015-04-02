using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.ServiceModel;

using SearchService;

namespace SearchClient
{
    class Program
    {
        static void Main(string[] args)
        {
            String BaseAddress = "http://localhost:8080";
            if (args.Length >= 2)
                BaseAddress = args[0] + ":" + args[1];
            var myBinding = new BasicHttpBinding();
            var myEndpoint = new EndpointAddress(BaseAddress + "/searchservice");
            var myChannelFactory = new ChannelFactory<ISearchServiceMain>(myBinding, myEndpoint);

            ISearchServiceMain client = null;

            try
            {
                client = myChannelFactory.CreateChannel();
                Console.WriteLine("Enter string: ");
                Console.WriteLine(client.GetData(Console.ReadLine()));
                ((ICommunicationObject)client).Close();
            }
            catch
            {
                Console.WriteLine("Connection error!");
                if (client != null)
                {
                    ((ICommunicationObject)client).Abort();
                }
            }
            Console.WriteLine("Press <Enter> to exit client app.");
            Console.ReadLine();
        }
    }
}