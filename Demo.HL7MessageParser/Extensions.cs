﻿using Demo.HL7MessageParser.Common;
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


    public static class FullCacheHK
    {
        static FullCacheHK()
        {
            PataientCache = new CacheHK<Patient_AlertProfile>();
        }

        public static CacheHK<Patient_AlertProfile> PataientCache { get; set; }
    }

    public class CacheHK<T>
    {
        /// <summary>
        /// CASENUMBER IS THE KEY
        /// </summary>
        static Dictionary<string, T> cache = new Dictionary<string, T>();

        public T this[string cacheKey]
        {
            get
            {
                if (cache.ContainsKey(cacheKey))
                {
                    return cache[cacheKey];
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

        public void Register(string caseNumber, T result)
        {
            cache[caseNumber] = result;
        }

        public void Clear()
        {
            cache.Clear();
        }
    }

    public class Patient_AlertProfile
    {
        public Patient_AlertProfile()
        {
            MDSCache = new CacheHK<MDSCheckResultCache>();
        }

        public PatientDemoEnquiry PatientDemoEnquiry { get; set; }
        public AlertProfileResult AlertProfileRes { get; set; }

        public MedicationProfileResult MedicationProfileRes { get; set; }

        public CacheHK<MDSCheckResultCache> MDSCache { get; set; }
    }

    public class MDSCheckResultCache
    {
        public string Cautaion { get; set; }

        public List<string> AllergyMdsCheckResults { get; set; }

        public List<string> AdrMdsCheckResults { get; set; }

        public MDSCheckResult Res { get; internal set; }
        public MDSCheckInputParm Req { get; internal set; }

        public GetDrugMdsPropertyHqRequest DrugMdsPropertyHqReq { get; set; }
        public GetDrugMdsPropertyHqResponse DrugMdsPropertyHqRes { get; set; }

        public GetPreparationResponse PreparationRes { get; set; }
        public GetPreparationRequest PreparationReq { get; set; }
    }

    public static class ExtensionMethods
    {
        public static bool IsNullOrWhiteSpace(this string inputstring)
        {
            if (inputstring == null)
            {
                return true;
            }

            if (inputstring.Trim().Length == 0)
            {
                return true;
            }

            return false;
        }
    }
}
