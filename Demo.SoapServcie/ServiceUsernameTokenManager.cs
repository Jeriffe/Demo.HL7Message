using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Text;
using System.Xml;

using Microsoft.Web.Services3;
using Microsoft.Web.Services3.Design;
using Microsoft.Web.Services3.Security;
using Microsoft.Web.Services3.Security.Tokens;
using Demo.HL7MessageParser.Common;
using System.Net;
using System.Configuration;

namespace Demo.SoapServcie
{
    public class ServiceUsernameTokenManager : UsernameTokenManager
    {

        protected static readonly string PARAM_CLIENT_SECRET = null;
        protected static readonly string Token_Password = null;
        protected static readonly string Token_Username = null;

        /// <summary>
        /// Constructs an instance of this security token manager.
        /// </summary>
        static ServiceUsernameTokenManager()
        {
            Token_Username = ConfigurationManager.AppSettings["Token_Username"];

            Token_Password = ConfigurationManager.AppSettings["Token_Password"];
        }

        public ServiceUsernameTokenManager()
        {

        }
        /// <summary>
        /// Constructs an instance of this security token manager.
        /// </summary>
        /// <param name="nodes">An XmlNodeList containing XML elements from a configuration file.</param>
        public ServiceUsernameTokenManager(XmlNodeList nodes)
            : base(nodes)
        {
        }

        /// <summary>
        /// Returns the password or password equivalent for the username provided.
        /// </summary>
        /// <param name="token">The username token</param>
        /// <returns>The password (or password equivalent) for the username</returns>
        protected override string AuthenticateToken(UsernameToken token)
        {
            // it's up to you where you will get a password for some user
            // you may:
            // 1) get the password hash from web.config or system registry
            //    if you are implementing per-server security
            // 2) get the password from the database or XML file for the given user name

            if (ValidateToken(token))
            {
                return token.Password;
            }

            var errorMessage = "Unknown exception, internal system processing error.";
            var innerEx = new Exception("UserNmae or Password is invalid!");

            throw new AMException(HttpStatusCode.InternalServerError, errorMessage, innerEx);
        }

        private bool ValidateToken(UsernameToken token)
        {
            return ((Token_Username == token.Username) && (Token_Password == token.Password));
        }
    }
}
