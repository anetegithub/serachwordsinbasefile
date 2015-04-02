using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.ServiceModel;
using System.ServiceModel.Description;

using System.IO;

using SearchServer.SearchServiceReference;
using SearchService;

namespace SearchServer
{
    class Program
    {
        static void Main(string[] args)
        {
            String Port="8080";
            String Path = Directory.GetCurrentDirectory() + @"\test.in";
            Console.ReadLine();
            if (args.Length >= 2)
            {
                Path = args[0];
                Port = args[1];
            }
            Uri baseAddress = new Uri("http://localhost:" + Port + "/searchservice");

            using (ServiceHost host = new ServiceHost(typeof(SearchServiceMain), baseAddress))
            {
                // Enable metadata publishing.
                ServiceMetadataBehavior smb = new ServiceMetadataBehavior();
                smb.HttpGetEnabled = true;
                smb.MetadataExporter.PolicyVersion = PolicyVersion.Policy15;
                host.Description.Behaviors.Add(smb);

                // Open the ServiceHost to start listening for messages. Since
                // no endpoints are explicitly configured, the runtime will create
                // one endpoint per base address for each service contract implemented
                // by the service.
                host.Open();
                SetPathToFile(Port, Path);
                

                Console.WriteLine("The service is ready at {0}", baseAddress);
                Console.WriteLine("Press <Enter> to stop the service.");                
                Console.ReadLine();

                // Close the ServiceHost.
                host.Close();
            }
        }

        static void SetPathToFile(string Port,string Path)
        {
            var myBinding = new BasicHttpBinding();
            var myEndpoint = new EndpointAddress("http://localhost:" + Port + "/searchservice");
            var myChannelFactory = new ChannelFactory<SearchService.ISearchServiceMain>(myBinding, myEndpoint);

            SearchService.ISearchServiceMain client = null;

            try
            {
                client = myChannelFactory.CreateChannel();
                Console.WriteLine(client.SetPath(Path));
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
        }
    }
}
