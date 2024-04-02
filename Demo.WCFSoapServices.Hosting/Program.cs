using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Description;
using System.Text;
using System.Threading.Tasks;

namespace Demo.WCFSoapServices.Hosting
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //HostDrugMasterService();

            HostPreparationService();
        }
        private static void HostPreparationService()
        {
            using (ServiceHost host = new ServiceHost(typeof(PreparationService)))
            {
                host.AddServiceEndpoint(typeof(IPreparationService), new WSHttpBinding(), "http://localhost:10096/PreparationService");
                if (host.Description.Behaviors.Find<ServiceMetadataBehavior>() == null)
                {
                    var behavior = new ServiceMetadataBehavior();
                    behavior.HttpGetEnabled = true;
                    behavior.HttpGetUrl = new Uri("http://localhost:10096/PreparationService/metadata");
                    host.Description.Behaviors.Add(behavior);
                }
                host.Opened += delegate { Console.WriteLine("PreparationService has started, you can press any key to stop service."); };

                host.Open();
                Console.Read();
            }
        }
        private static void HostDrugMasterService()
        {
            using (ServiceHost host = new ServiceHost(typeof(DrugMasterService)))
            {
                host.AddServiceEndpoint(typeof(IDrugMasterService), new WSHttpBinding(), "http://localhost:10096/DrugMasterService");
                if (host.Description.Behaviors.Find<ServiceMetadataBehavior>() == null)
                {
                    var behavior = new ServiceMetadataBehavior();
                    behavior.HttpGetEnabled = true;
                    behavior.HttpGetUrl = new Uri("http://localhost:10096/DrugMasterService/metadata");
                    host.Description.Behaviors.Add(behavior);
                }
                host.Opened += delegate { Console.WriteLine("DrugMasterService has started, you can press any key to stop service."); };

                host.Open();
                Console.Read();
            }
        }
    }
}
