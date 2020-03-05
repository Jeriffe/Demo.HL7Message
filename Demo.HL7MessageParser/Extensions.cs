using Demo.HL7MessageParser.Common;
using Demo.HL7MessageParser.Models;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;

namespace Demo.HL7MessageParser
{

   
    /// <summary>
    /// RestSharpExtensions.cs
    /// https://gist.github.com/lkaczanowski/febb25cc49f339c5f516
    /// </summary>
    public static class RestSharpExtensions
    {
        public static bool IsScuccessStatusCode(this HttpStatusCode responseCode)
        {
            var numericResponse = (int)responseCode;

            const int statusCodeOk = (int)HttpStatusCode.OK;

            const int statusCodeBadRequest = (int)HttpStatusCode.BadRequest;

            return numericResponse >= statusCodeOk &&
                   numericResponse < statusCodeBadRequest;
        }

        public static bool IsSuccessful(this IRestResponse response)
        {
            return response.StatusCode.IsScuccessStatusCode() &&
                   response.ResponseStatus == ResponseStatus.Completed;
        }
        public static void ThrowException(this IRestResponse response)
        {
            if (response.StatusCode == HttpStatusCode.OK)
            {
                return;
            }

            if (response.StatusCode == 0)
            {
                throw new AMException(HttpStatusCode.ServiceUnavailable, response.ErrorMessage, response.ErrorException);
            }
            else
            {
                throw new AMException(response.StatusCode, response.Content, response.ErrorException);
            }
        }
    }

    public class CallFasterWebProxy : IWebProxy
    {
        public ICredentials Credentials { get; set; }

        public Uri GetProxy(Uri destination)
        {
            return destination;
        }

        public bool IsBypassed(Uri host)
        {
            // if return true, service will be very slow.
            return false;
        }

        private static CallFasterWebProxy defaultProxy = new CallFasterWebProxy();
        public static CallFasterWebProxy Default
        {
            get
            {
                return defaultProxy;
            }
        }
    }


    public static class Cache_HK
    {
        static Cache_HK()
        {
            PataientCache = new Cache<Patient_AlertProfile>();
            MDS_CheckCache = new Cache<MDSCheckCacheResult>();
        }

        public static Cache<MDSCheckCacheResult> MDS_CheckCache { get; set; }
        public static Cache<Patient_AlertProfile> PataientCache { get; set; }
    }

    public class Cache<T>
    {
        /// <summary>
        /// CASENUMBER IS THE KEY
        /// </summary>
        static Dictionary<string, T> cache = new Dictionary<string, T>();
      
        public T this[string hkId]
        {
            get
            {
                if (cache.ContainsKey(hkId))
                {
                    return cache[hkId];
                }

                return default(T);
            }
        }
        public T GetByCache(string hkId)
        {
            if (cache.ContainsKey(hkId))
            {
                return cache[hkId];
            }

            return default(T);
        }

        public void Register(string hkId, T result)
        {
            cache[hkId] = result;
        }

        public void Clear()
        {
            cache.Clear();
        }
    }

    public class Patient_AlertProfile
    {
        public PatientDemoEnquiry PatientDemoEnquiry { get; set; }
        public MedicationProfileResult MedicationProfileRes { get; set; }
        public AlertProfileResult AlertProfileRes { get; set; }
    }

    public class MDSCheckCacheResult
    {
        public string Cautaion { get; set; }

        public List<string> AllergyMdsCheckResults { get; set; }

        public List<string> AdrMdsCheckResults { get; set; }

        public ComplexMDSResult Res { get; internal set; }
        public MDSCheckInputParm Req { get; internal set; }
    }
}
