//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:2.0.50727.9035
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Xml.Serialization;

// 
// This source code was auto-generated by wsdl, Version=2.0.50727.42.
// 
namespace Demo.HL7MessageParser.WebProxy
{
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.42")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Web.Services.WebServiceBindingAttribute(Name = "PatientServiceSoap", Namespace = "http://webservice.pas.ha.org.hk/")]
    public partial class PatientServiceProxy : Microsoft.Web.Services3.WebServicesClientProtocol
    {

        private WorkContextSoapHeader workContextField;

        private System.Threading.SendOrPostCallback searchHKPMIPatientByCaseNoOperationCompleted;

        /// <remarks/>
        public PatientServiceProxy(string Url)
        {
            this.Url = Url;
        }

        public PatientServiceProxy() : this("http://localhost:8096/PatientService.asmx")
        {
        }

        public WorkContextSoapHeader WorkContext
        {
            get
            {
                return this.workContextField;
            }
            set
            {
                this.workContextField = value;
            }
        }

        /// <remarks/>
        public event searchHKPMIPatientByCaseNoCompletedEventHandler searchHKPMIPatientByCaseNoCompleted;

        [WebServiceSOAPExtension]
        [System.Web.Services.Protocols.SoapHeaderAttribute("WorkContext", Direction = System.Web.Services.Protocols.SoapHeaderDirection.InOut)]
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://webservice.pas.ha.org.hk/searchHKPMIPatientByCaseNo", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Bare)]
        [return: System.Xml.Serialization.XmlElementAttribute("searchHKPMIPatientByCaseNoResponse", Namespace = "http://webservice.pas.ha.org.hk/", IsNullable = true)]
        public SearchHKPMIPatientByCaseNoResponse searchHKPMIPatientByCaseNo([System.Xml.Serialization.XmlElementAttribute("searchHKPMIPatientByCaseNo", Namespace = "http://webservice.pas.ha.org.hk/", IsNullable = true)] searchHKPMIPatientByCaseNo searchHKPMIPatientByCaseNo1)
        {
            object[] results = this.Invoke("searchHKPMIPatientByCaseNo", new object[] {
                    searchHKPMIPatientByCaseNo1});
            return ((SearchHKPMIPatientByCaseNoResponse)(results[0]));
        }

        /// <remarks/>
        public System.IAsyncResult BeginsearchHKPMIPatientByCaseNo(searchHKPMIPatientByCaseNo searchHKPMIPatientByCaseNo1, System.AsyncCallback callback, object asyncState)
        {
            return this.BeginInvoke("searchHKPMIPatientByCaseNo", new object[] {
                    searchHKPMIPatientByCaseNo1}, callback, asyncState);
        }

        /// <remarks/>
        public SearchHKPMIPatientByCaseNoResponse EndsearchHKPMIPatientByCaseNo(System.IAsyncResult asyncResult)
        {
            object[] results = this.EndInvoke(asyncResult);
            return ((SearchHKPMIPatientByCaseNoResponse)(results[0]));
        }

        /// <remarks/>
        public void searchHKPMIPatientByCaseNoAsync(searchHKPMIPatientByCaseNo searchHKPMIPatientByCaseNo1)
        {
            this.searchHKPMIPatientByCaseNoAsync(searchHKPMIPatientByCaseNo1, null);
        }

        /// <remarks/>
        public void searchHKPMIPatientByCaseNoAsync(searchHKPMIPatientByCaseNo searchHKPMIPatientByCaseNo1, object userState)
        {
            if ((this.searchHKPMIPatientByCaseNoOperationCompleted == null))
            {
                this.searchHKPMIPatientByCaseNoOperationCompleted = new System.Threading.SendOrPostCallback(this.OnsearchHKPMIPatientByCaseNoOperationCompleted);
            }
            this.InvokeAsync("searchHKPMIPatientByCaseNo", new object[] {
                    searchHKPMIPatientByCaseNo1}, this.searchHKPMIPatientByCaseNoOperationCompleted, userState);
        }

        private void OnsearchHKPMIPatientByCaseNoOperationCompleted(object arg)
        {
            if ((this.searchHKPMIPatientByCaseNoCompleted != null))
            {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.searchHKPMIPatientByCaseNoCompleted(this, new searchHKPMIPatientByCaseNoCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }

        /// <remarks/>
        public new void CancelAsync(object userState)
        {
            base.CancelAsync(userState);
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.42")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class address
    {

        private string buildingField;

        private string districtCodeField;

        private string fullEnglishAddressField;

        private string record_idField;

        private string roomField;

        /// <remarks/>
        public string building
        {
            get
            {
                return this.buildingField;
            }
            set
            {
                this.buildingField = value;
            }
        }

        /// <remarks/>
        public string districtCode
        {
            get
            {
                return this.districtCodeField;
            }
            set
            {
                this.districtCodeField = value;
            }
        }

        /// <remarks/>
        public string fullEnglishAddress
        {
            get
            {
                return this.fullEnglishAddressField;
            }
            set
            {
                this.fullEnglishAddressField = value;
            }
        }

        /// <remarks/>
        public string record_id
        {
            get
            {
                return this.record_idField;
            }
            set
            {
                this.record_idField = value;
            }
        }

        /// <remarks/>
        public string room
        {
            get
            {
                return this.roomField;
            }
            set
            {
                this.roomField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.42")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class patient
    {

        private address addressField;

        private string cCCode1Field;

        private string cCCode2Field;

        private string cCCode3Field;

        private string cCCode4Field;

        private string cCCode5Field;

        private string cCCode6Field;

        private string chineseNameField;

        private string confidentialFlagField;

        private string dOBField;

        private string exactDOBFlagField;

        private string hKIDField;

        private string hkicSymbolField;

        private string homePhoneField;

        private string keyField;

        private string lastPayCodeField;

        private string maritalStatusField;

        private string nameField;

        private string officePhoneField;

        private string raceField;

        private string sexField;

        /// <remarks/>
        public address address
        {
            get
            {
                return this.addressField;
            }
            set
            {
                this.addressField = value;
            }
        }

        /// <remarks/>
        public string CCCode1
        {
            get
            {
                return this.cCCode1Field;
            }
            set
            {
                this.cCCode1Field = value;
            }
        }

        /// <remarks/>
        public string CCCode2
        {
            get
            {
                return this.cCCode2Field;
            }
            set
            {
                this.cCCode2Field = value;
            }
        }

        /// <remarks/>
        public string CCCode3
        {
            get
            {
                return this.cCCode3Field;
            }
            set
            {
                this.cCCode3Field = value;
            }
        }

        public string CCCode4
        {
            get
            {
                return this.cCCode4Field;
            }
            set
            {
                this.cCCode4Field = value;
            }
        }

        public string CCCode5
        {
            get
            {
                return this.cCCode5Field;
            }
            set
            {
                this.cCCode5Field = value;
            }
        }

        public string CCCode6
        {
            get
            {
                return this.cCCode6Field;
            }
            set
            {
                this.cCCode6Field = value;
            }
        }
        /// <remarks/>
        public string chineseName
        {
            get
            {
                return this.chineseNameField;
            }
            set
            {
                this.chineseNameField = value;
            }
        }

        /// <remarks/>
        public string confidentialFlag
        {
            get
            {
                return this.confidentialFlagField;
            }
            set
            {
                this.confidentialFlagField = value;
            }
        }

        /// <remarks/>
        public string DOB
        {
            get
            {
                return this.dOBField;
            }
            set
            {
                this.dOBField = value;
            }
        }

        /// <remarks/>
        public string exactDOBFlag
        {
            get
            {
                return this.exactDOBFlagField;
            }
            set
            {
                this.exactDOBFlagField = value;
            }
        }

        /// <remarks/>
        public string HKID
        {
            get
            {
                return this.hKIDField;
            }
            set
            {
                this.hKIDField = value;
            }
        }

        /// <remarks/>
        public string hkicSymbol
        {
            get
            {
                return this.hkicSymbolField;
            }
            set
            {
                this.hkicSymbolField = value;
            }
        }

        /// <remarks/>
        public string homePhone
        {
            get
            {
                return this.homePhoneField;
            }
            set
            {
                this.homePhoneField = value;
            }
        }

        /// <remarks/>
        public string key
        {
            get
            {
                return this.keyField;
            }
            set
            {
                this.keyField = value;
            }
        }

        /// <remarks/>
        public string lastPayCode
        {
            get
            {
                return this.lastPayCodeField;
            }
            set
            {
                this.lastPayCodeField = value;
            }
        }

        /// <remarks/>
        public string maritalStatus
        {
            get
            {
                return this.maritalStatusField;
            }
            set
            {
                this.maritalStatusField = value;
            }
        }

        /// <remarks/>
        public string name
        {
            get
            {
                return this.nameField;
            }
            set
            {
                this.nameField = value;
            }
        }

        /// <remarks/>
        public string officePhone
        {
            get
            {
                return this.officePhoneField;
            }
            set
            {
                this.officePhoneField = value;
            }
        }

        /// <remarks/>
        public string race
        {
            get
            {
                return this.raceField;
            }
            set
            {
                this.raceField = value;
            }
        }

        /// <remarks/>
        public string sex
        {
            get
            {
                return this.sexField;
            }
            set
            {
                this.sexField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.42")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class @case
    {

        private string admissionDatetimeField;

        private string bedNoField;

        private string hospitalCodeField;

        private string numberField;

        private string patientTypeField;

        private string sourceCodeField;

        private string sourceIndicatorField;

        private string specialtyField;

        private string typeField;

        private string wardClassField;

        private string wardCodeField;

        /// <remarks/>
        public string admissionDatetime
        {
            get
            {
                return this.admissionDatetimeField;
            }
            set
            {
                this.admissionDatetimeField = value;
            }
        }

        /// <remarks/>
        public string bedNo
        {
            get
            {
                return this.bedNoField;
            }
            set
            {
                this.bedNoField = value;
            }
        }

        /// <remarks/>
        public string hospitalCode
        {
            get
            {
                return this.hospitalCodeField;
            }
            set
            {
                this.hospitalCodeField = value;
            }
        }

        /// <remarks/>
        public string number
        {
            get
            {
                return this.numberField;
            }
            set
            {
                this.numberField = value;
            }
        }

        /// <remarks/>
        public string patientType
        {
            get
            {
                return this.patientTypeField;
            }
            set
            {
                this.patientTypeField = value;
            }
        }

        /// <remarks/>
        public string sourceCode
        {
            get
            {
                return this.sourceCodeField;
            }
            set
            {
                this.sourceCodeField = value;
            }
        }

        /// <remarks/>
        public string sourceIndicator
        {
            get
            {
                return this.sourceIndicatorField;
            }
            set
            {
                this.sourceIndicatorField = value;
            }
        }

        /// <remarks/>
        public string specialty
        {
            get
            {
                return this.specialtyField;
            }
            set
            {
                this.specialtyField = value;
            }
        }

        /// <remarks/>
        public string type
        {
            get
            {
                return this.typeField;
            }
            set
            {
                this.typeField = value;
            }
        }

        /// <remarks/>
        public string wardClass
        {
            get
            {
                return this.wardClassField;
            }
            set
            {
                this.wardClassField = value;
            }
        }

        /// <remarks/>
        public string wardCode
        {
            get
            {
                return this.wardCodeField;
            }
            set
            {
                this.wardCodeField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.42")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [XmlRoot(ElementName = "PatientDemoEnquiryResult", Namespace = "")]
    public partial class PatientDemoEnquiry
    {

        private @case[] caseListField;

        private patient patientField;

        /// <remarks/>
        public @case[] caseList
        {
            get
            {
                return this.caseListField;
            }
            set
            {
                this.caseListField = value;
            }
        }

        /// <remarks/>
        public patient patient
        {
            get
            {
                return this.patientField;
            }
            set
            {
                this.patientField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.42")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://webservice.pas.ha.org.hk/")]
    public partial class SearchHKPMIPatientByCaseNoResponse
    {
        private PatientDemoEnquiry patientDemoEnquiryResultField;

        private System.Xml.Serialization.XmlSerializerNamespaces xmlnsField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(ElementName = "PatientDemoEnquiryResult", Namespace = "")]
        public PatientDemoEnquiry PatientDemoEnquiryResult
        {
            get
            {
                return this.patientDemoEnquiryResultField;
            }
            set
            {
                this.patientDemoEnquiryResultField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlNamespaceDeclarationsAttribute()]
        public System.Xml.Serialization.XmlSerializerNamespaces Xmlns
        {
            get
            {
                return this.xmlnsField;
            }
            set
            {
                this.xmlnsField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.42")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
   // [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://webservice.pas.ha.org.hk/")]
    public partial class searchHKPMIPatientByCaseNo
    {

        private string hospitalCodeField;

        private string caseNoField;

        /// <remarks/>
        [XmlElement(ElementName = "hospitalCode",Namespace ="")]
        public string hospitalCode
        {
            get
            {
                return this.hospitalCodeField;
            }
            set
            {
                this.hospitalCodeField = value;
            }
        }

        /// <remarks/>
        [XmlElement(ElementName = "caseNo", Namespace = "")]
        public string caseNo
        {
            get
            {
                return this.caseNoField;
            }
            set
            {
                this.caseNoField = value;
            }
        }

        private XmlSerializerNamespaces xmlns;

        [XmlNamespaceDeclarations]
        public XmlSerializerNamespaces Xmlns
        {
            get
            {
                if (xmlns == null)
                {
                    xmlns = new XmlSerializerNamespaces();
                    xmlns.Add("web", "http://webservice.pas.ha.org.hk/");
                }
                return xmlns;
            }
            set { xmlns = value; }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.42")]
    public delegate void searchHKPMIPatientByCaseNoCompletedEventHandler(object sender, searchHKPMIPatientByCaseNoCompletedEventArgs e);

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.42")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class searchHKPMIPatientByCaseNoCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs
    {

        private object[] results;

        internal searchHKPMIPatientByCaseNoCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) :
                base(exception, cancelled, userState)
        {
            this.results = results;
        }

        /// <remarks/>
        public SearchHKPMIPatientByCaseNoResponse Result
        {
            get
            {
                this.RaiseExceptionIfNecessary();
                return ((SearchHKPMIPatientByCaseNoResponse)(this.results[0]));
            }
        }
    }
}