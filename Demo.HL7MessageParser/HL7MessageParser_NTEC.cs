﻿using Demo.HL7MessageParser.Model;
using Demo.HL7MessageParser.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Demo.HL7MessageParser
{
    public class HL7MessageParser_NTEC : IHL7MessageParser
    {
        IPatientVisitParser patientVisitParser;
        IMedicationProfileParser medicationProfileParser;
        IAlertProfileParser allergiesParser;

        public HL7MessageParser_NTEC()
        {
            this.patientVisitParser = new SoapPatientVisitParser();
            this.medicationProfileParser = new JSONMedicationProfileParser();
            this.allergiesParser = new JSONIAlertProfileParser();
        }
        public HL7MessageParser_NTEC(IPatientVisitParser patientVisitParser,
            IMedicationProfileParser medicationProfileParser,
            IAlertProfileParser allergiesParser)
        {
            this.patientVisitParser = patientVisitParser;
            this.medicationProfileParser = medicationProfileParser;
            this.allergiesParser = allergiesParser;
        }

        public IEnumerable<Order> GetOrders(string caseno)
        {
            var medicationProfile = medicationProfileParser.GetMedicationProfile(caseno);

            //TODO:storage the response

            var orders = medicationProfile.MedProfileMoItems.ToConvert();

            return orders;
        }


        public PatientVisit GetPatient(string caseno)
        {
            var pr = patientVisitParser.GetPatientResult(caseno);
            
            //TODO: storage the response

            var patientVisit = pr.ToConvert();

            return patientVisit;
        }


        public IEnumerable<Allergies> GetAllergies(AlertInputParm alertinput)
        {
            var apr = allergiesParser.GetAlertProfile(alertinput);

            //TODO:storage the response

            var result = apr.ToConvert();

            return result;
        }
    }
}
