using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Demo.HL7MessageParser.Models
{

    public class MedicationProfileResult
    {
        public MedicationProfileResult()
        {

        }
        public string MedProfileId { get; set; }

        public string CaseNum { get; set; }

        public List<MedProfileMoItem> MedProfileMoItems { get; set; }
    }

    public class MedProfileMoItem
    {
        public MedProfileMoItem()
        {

        }
        public long MedProfileMoItemId { get; set; }
        public string DrugOrder { get; set; }
        public string VerifiedFlag { get; set; }
        public string MdsAlertFlag { get; set; }
        public string ManualFlag { get; set; }
        public string SingleDsfFlag { get; set; }
        public string DoctorCode { get; set; }
        public string DoctorName { get; set; }
        public string DoctorRankCode { get; set; }

        public List<MedProfilePoItem> MedProfilePoItems { get; set; }
    }

    public class MedProfilePoItem
    {
        public MedProfilePoItem()
        {

        }
        public long? MedProfilePoItemId { get; set; }

        public string ItemCode { get; set; }

        public string SpecialHandleFlag { get; set; }
    }
}
