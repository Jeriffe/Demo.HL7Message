using Demo.Infrastructure.Processors;
using Microsoft.Extensions.Logging;
using System;
using Microsoft.Web.Services3;
using Microsoft.Web.Services3.Addressing;
using Microsoft.Web.Services3.Messaging;
using Demo.SoapServcie;

namespace Demo.Processors.SoapServiceProcessor
{
    public class HK_WSESoapServiceProcessor : IProcessor
    {
        readonly ILogger logger = null;

        public int LoopInterval { get; set; } = 3;

        public string Name => GetType().Name;

        public HK_WSESoapServiceProcessor(ILogger<HK_WSESoapServiceProcessor> logger)
        {
            this.logger = logger;
        }

        public void DoProcess(object obj)
        {
            try
            {
                bool action = (bool)obj;
                logger.LogInformation($"DoProcess of {this.Name} has been called");

                if (action)
                {
                    var url = new Uri("soap.tcp://localhost:8096/PatientService.asmx");
                    SoapReceivers.Add(new EndpointReference(url), typeof(PatientService));
                }
                else
                {
                    SoapReceivers.Clear();
                }

            }
            catch (Exception ex)
            {

            }

        }

        public void Dispose()
        {
        }
    }
}
