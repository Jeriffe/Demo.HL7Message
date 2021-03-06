﻿using Demo.HL7MessageParser.Common;
using NLog;
using System;
using System.Configuration;
using System.IO;
using System.Text;
using System.Web.Services.Protocols;

namespace Demo.HL7MessageParser
{
    public sealed class WebServiceSOAPExtension : SoapExtension
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();

        private Stream oldStream;
        private Stream newStream;

        // Save the Stream representing the SOAP request or SOAP response into a local memory buffer.
        public override Stream ChainStream(Stream stream)
        {
            oldStream = stream;
            newStream = new MemoryStream();
            return newStream;
        }

        // When the SOAP extension is accessed for the first time, the XML Web service method it is applied to is accessed to store the file name passed in,

        //using the corresponding SoapExtensionAttribute.    
        public override object GetInitializer(LogicalMethodInfo methodInfo, SoapExtensionAttribute attribute)
        {
            return null;
        }

        // The SOAP extension was configured to run using a configuration file instead of an attribute applied to a specific XML Web service method.
        public override object GetInitializer(Type WebServiceType)
        {
            return null;
        }

        // Receive the file name stored by GetInitializer and store it in a member variable for this specific instance.
        public override void Initialize(object initializer)
        {
        }

        //  If the SoapMessageStage is such that the SoapRequest or SoapResponse is still in the SOAP format to be sent or received, save it out to a file.
        public override void ProcessMessage(SoapMessage message)
        {
            switch (message.Stage)
            {
                case SoapMessageStage.BeforeSerialize:
                    break;
                case SoapMessageStage.AfterSerialize:
                    LogSoapRequestAfterSerialize((SoapClientMessage)message);
                    break;
                case SoapMessageStage.BeforeDeserialize:
                    LogSoapResponseBeforeDeserialize((SoapClientMessage)message);
                    break;
                case SoapMessageStage.AfterDeserialize:

                    break;
                default:
                    throw new Exception("invalidstage");
            }
        }

        public void LogSoapRequestAfterSerialize(SoapClientMessage message)
        {
            newStream.Position = 0;

            var requestStr = new StreamReader(newStream, Encoding.UTF8).ReadToEnd();
            try
            {
                requestStr = XmlHelper.FormatXML(requestStr);
            }
            catch { }
            
            logger.Info(string.Format("SoapRequestAfterSerialize of the {2}:{1}{0}", requestStr, Environment.NewLine, message.MethodInfo.Name));

            newStream.Position = 0;
            Copy(newStream, oldStream);
        }

        public void LogSoapResponseBeforeDeserialize(SoapMessage message)
        {
            Copy(oldStream, newStream);

            newStream.Position = 0;

            var responseStr = new StreamReader(newStream, Encoding.UTF8).ReadToEnd();
            try
            {
                responseStr = XmlHelper.FormatXML(responseStr);
            }
            catch { }

            logger.Info(string.Format("SoapResponseBeforeDeserialize of {2}:{1}{0}", responseStr, Environment.NewLine, message.MethodInfo.Name));

            newStream.Position = 0;
        }

        private void Copy(Stream from, Stream to)
        {
            TextReader reader = new StreamReader(from);
            TextWriter writer = new StreamWriter(to);
            writer.WriteLine(reader.ReadToEnd());
            writer.Flush();
        }
    }

    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    public class WebServiceSOAPExtensionAttribute : SoapExtensionAttribute
    {
        private int priority;

        public override Type ExtensionType
        {
            get { return typeof(WebServiceSOAPExtension); }
        }

        public override int Priority
        {
            get { return priority; }
            set { priority = value; }
        }
    }
}
