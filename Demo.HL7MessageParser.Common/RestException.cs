﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;

namespace Demo.HL7MessageParser.Common
{
    public class AMException : Exception
    {
        public HttpStatusCode HttpStatusCode { get; private set; }

        public Uri RequestUri { get; private set; }

        public AMException(HttpStatusCode httpStatusCode, string message, Exception innerException) : base(message, innerException)
        {
            HttpStatusCode = httpStatusCode;
        }

        public AMException(HttpStatusCode httpStatusCode, Uri requestUri, string message, Exception innerException) : base(message, innerException)
        {
            HttpStatusCode = httpStatusCode;
            RequestUri = requestUri;
        }
    }
}
