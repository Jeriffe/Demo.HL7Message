using Demo.HL7MessageParser.Common;
using Demo.HL7MessageParser.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Demo.HL7MessageParser.WinForms
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            txtCaseNumber.Text = "HN202003005";
            txtHKID.Text = "H2003005";
            txtItemCode.Text = "DEMO01";


            patientSeed = Path.Combine(baseDir, @"Demo\PE\HN202003001.xml");

            medicationProfileSeed = Path.Combine(baseDir, @"Demo\MP\HN202003001.json");

            alertProfileSeed = Path.Combine(baseDir, @"Demo\AP\H2003001.json");

        }
        string baseDir = AppDomain.CurrentDomain.BaseDirectory;
        string patientSeed;
        string medicationProfileSeed;
        string alertProfileSeed;
        string caseNumberSeed = "HN202003001";
        string hkidSeed = "H2003001";
        string itemCodeSeed = "DEMO01";

        private void btnGenerate_Click(object sender, EventArgs e)
        {
            var caseNumberOriginal = txtCaseNumber.Text.Substring(4);
            //var hkidOrg = txtHKID.Text.Substring(1);
            //var itemcode = txtItemCode.Text.Substring(4);

            var caseNumberCount = int.Parse(caseNumberOriginal);

            var itemcodeCount = int.Parse(caseNumberOriginal.Substring(caseNumberOriginal.Length - 2, 2));
            for (int index = 0; index < 16; index++)
            {
                var casenumber = string.Format("HN20{0}", caseNumberCount + index);
                var hkid = string.Format("H{0}", caseNumberCount + index);
                var itemcode1 = string.Format("DEMO{0}", (itemcodeCount + index).ToString().PadLeft(2, '0'));

                GeneratePatient(casenumber, hkid);
                GenerateDrugMaster(itemcode1);
            }
            //Patient
        }

        private void GenerateDrugMaster(string itemcode)
        {

        }

        private void GeneratePatient(string casenumber, string hkId)
        {
            try
            {
                var patientOrg = SoapParserHelper.LoadSamplePatientDemoEnquiry("HN202003001", @"DEMO\PE"); ;

                patientOrg.Patient.HKID = hkId;
                patientOrg.CaseList[0].Number = casenumber;

                var patientFullXmlStr = @"<S:Envelope xmlns:S=""http://schemas.xmlsoap.org/soap/envelope/"">
 <S:Header>
 <work:WorkContext xmlns:work = ""http://oracle.com/weblogic/soap/workarea/"" >rO0ABXdqADF3ZWJsb2dpYy5hcHAuUEFTX1BBU19QQVRJRU5UX0VOUVVJUllfV1NTXzJfMF8xMl82AAAA1gAAACN3ZWJsb2dpYy53b3JrYXJlYS5TdHJpbmdXb3JrQ29udGV4dAAIMi4wLjEyLjYAAA==</work:WorkContext>
 </S:Header>
    <S:Body>
        <ns2:searchHKPMIPatientByCaseNoResponse xmlns:ns2=""http://webservice.pas.ha.org.hk/"">
            {0}
        </ns2:searchHKPMIPatientByCaseNoResponse>
    </S:Body>
</S:Envelope>";

                var patientXmlStr = XmlHelper.XmlSerializeToString(patientOrg);

                patientFullXmlStr = string.Format(patientFullXmlStr, patientXmlStr.Substring(@"<?xml version=""1.0"" encoding=""utf - 8""?>".Length));

                patientFullXmlStr = XmlHelper.FormatXML(patientFullXmlStr);

                var patientTargetFileName = Path.Combine(baseDir, string.Format(@"Demo\PE\{0}.xml", casenumber));
                File.WriteAllText(patientTargetFileName, patientFullXmlStr);

                var medicationOrg = JsonHelper.JsonToObjectFromFile<MedicationProfileResult>(medicationProfileSeed);
                var medicationTargetStr = JsonHelper.FormatJson(JsonHelper.ToJson(medicationOrg));

                var medicationTargetFileName = Path.Combine(baseDir, string.Format(@"Demo\MP\{0}.json", casenumber));
                File.WriteAllText(medicationTargetFileName, medicationTargetStr);


                var alertOrg = JsonHelper.JsonToObjectFromFile<AlertProfileResult>(alertProfileSeed);

                var alertTargetStr = JsonHelper.FormatJson(JsonHelper.ToJson(medicationOrg));

                var alertTargetFileName = Path.Combine(baseDir, string.Format(@"Demo\AP\{0}.json", hkId));
                File.WriteAllText(alertTargetFileName, alertTargetStr);
            }
            catch (Exception EX)
            {
                EX = EX;
            }


        }
    }
}
