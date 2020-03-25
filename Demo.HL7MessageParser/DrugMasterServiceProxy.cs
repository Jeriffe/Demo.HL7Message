﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
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
using Demo.HL7MessageParser.Models;
// 
// This source code was auto-generated by wsdl, Version=4.6.81.0.
// 

namespace Demo.HL7MessageParser.WebProxy
{
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.6.81.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Web.Services.WebServiceBindingAttribute(Name = "DrugMasterServiceSoap", Namespace = "http://biz.dms.pms.model.ha.org.hk/")]
    public partial class DrugMasterServiceProxy : System.Web.Services.Protocols.SoapHttpClientProtocol
    {
        private WorkContextSoapHeader workContextField;

        private System.Threading.SendOrPostCallback getPreparationOperationCompleted;

        private System.Threading.SendOrPostCallback getDrugMdsPropertyHqOperationCompleted;

        /// <remarks/>
        public DrugMasterServiceProxy(string url)
        {
            this.Url = url;
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
        public event getPreparationCompletedEventHandler getPreparationCompleted;

        /// <remarks/>
        public event getDrugMdsPropertyHqCompletedEventHandler getDrugMdsPropertyHqCompleted;

        /// <remarks/>
        [System.Web.Services.Protocols.SoapHeaderAttribute("WorkContext", Direction = System.Web.Services.Protocols.SoapHeaderDirection.Out)]
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://biz.dms.pms.model.ha.org.hk/getPreparation", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Bare)]
        [return: System.Xml.Serialization.XmlElementAttribute("getPreparationResponse", Namespace = "http://biz.dms.pms.model.ha.org.hk/", IsNullable = true)]
        public Demo.HL7MessageParser.Models.GetPreparationResponse getPreparation([System.Xml.Serialization.XmlElementAttribute("getPreparation", Namespace = "http://biz.dms.pms.model.ha.org.hk/", IsNullable = true)] Demo.HL7MessageParser.Models.GetPreparationRequest getPreparation1)
        {
            object[] results = this.Invoke("getPreparation", new object[] {
                    getPreparation1});
            return ((Demo.HL7MessageParser.Models.GetPreparationResponse)(results[0]));
        }
        /// <remarks/>
        public System.IAsyncResult BegingetPreparation(Demo.HL7MessageParser.Models.GetPreparationRequest getPreparation1, System.AsyncCallback callback, object asyncState)
        {
            return this.BeginInvoke("getPreparation", new object[] {
                    getPreparation1}, callback, asyncState);
        }
        /// <remarks/>
        public Demo.HL7MessageParser.Models.GetPreparationResponse EndgetPreparation(System.IAsyncResult asyncResult)
        {
            object[] results = this.EndInvoke(asyncResult);
            return ((Demo.HL7MessageParser.Models.GetPreparationResponse)(results[0]));
        }
        /// <remarks/>
        public void getPreparationAsync(Demo.HL7MessageParser.Models.GetPreparationRequest getPreparation1)
        {
            this.getPreparationAsync(getPreparation1, null);
        }
        /// <remarks/>
        public void getPreparationAsync(Demo.HL7MessageParser.Models.GetPreparationRequest getPreparation1, object userState)
        {
            if ((this.getPreparationOperationCompleted == null))
            {
                this.getPreparationOperationCompleted = new System.Threading.SendOrPostCallback(this.OngetPreparationOperationCompleted);
            }
            this.InvokeAsync("getPreparation", new object[] {
                    getPreparation1}, this.getPreparationOperationCompleted, userState);
        }

        private void OngetPreparationOperationCompleted(object arg)
        {
            if ((this.getPreparationCompleted != null))
            {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.getPreparationCompleted(this, new getPreparationCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }

        /// <remarks/>
        [System.Web.Services.Protocols.SoapHeaderAttribute("WorkContext", Direction = System.Web.Services.Protocols.SoapHeaderDirection.Out)]
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://biz.dms.pms.model.ha.org.hk/getDrugMdsPropertyHq", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Bare)]
        [return: System.Xml.Serialization.XmlArrayAttribute("getDrugMdsPropertyHqResponse", Namespace = "http://biz.dms.pms.model.ha.org.hk/", IsNullable = true)]
        [return: System.Xml.Serialization.XmlArrayItemAttribute("return", IsNullable = false)]
        public ReturnObj[] getDrugMdsPropertyHq([System.Xml.Serialization.XmlElementAttribute("getDrugMdsPropertyHq", Namespace = "http://biz.dms.pms.model.ha.org.hk/", IsNullable = true)] Demo.HL7MessageParser.Models.GetDrugMdsPropertyHqRequest getDrugMdsPropertyHq1)
        {
            object[] results = this.Invoke("getDrugMdsPropertyHq", new object[] {
                    getDrugMdsPropertyHq1});
            return ((ReturnObj[])(results[0]));
        }
        /// <remarks/>
        public System.IAsyncResult BegingetDrugMdsPropertyHq(Demo.HL7MessageParser.Models.GetDrugMdsPropertyHqRequest getDrugMdsPropertyHq1, System.AsyncCallback callback, object asyncState)
        {
            return this.BeginInvoke("getDrugMdsPropertyHq", new object[] {
                    getDrugMdsPropertyHq1}, callback, asyncState);
        }
        /// <remarks/>
        public ReturnObj[] EndgetDrugMdsPropertyHq(System.IAsyncResult asyncResult)
        {
            object[] results = this.EndInvoke(asyncResult);
            return ((ReturnObj[])(results[0]));
        }
        /// <remarks/>
        public void getDrugMdsPropertyHqAsync(Demo.HL7MessageParser.Models.GetDrugMdsPropertyHqRequest getDrugMdsPropertyHq1)
        {
            this.getDrugMdsPropertyHqAsync(getDrugMdsPropertyHq1, null);
        }
        /// <remarks/>
        public void getDrugMdsPropertyHqAsync(Demo.HL7MessageParser.Models.GetDrugMdsPropertyHqRequest getDrugMdsPropertyHq1, object userState)
        {
            if ((this.getDrugMdsPropertyHqOperationCompleted == null))
            {
                this.getDrugMdsPropertyHqOperationCompleted = new System.Threading.SendOrPostCallback(this.OngetDrugMdsPropertyHqOperationCompleted);
            }
            this.InvokeAsync("getDrugMdsPropertyHq", new object[] {
                    getDrugMdsPropertyHq1}, this.getDrugMdsPropertyHqOperationCompleted, userState);
        }

        private void OngetDrugMdsPropertyHqOperationCompleted(object arg)
        {
            if ((this.getDrugMdsPropertyHqCompleted != null))
            {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.getDrugMdsPropertyHqCompleted(this, new getDrugMdsPropertyHqCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }

        /// <remarks/>
        public new void CancelAsync(object userState)
        {
            base.CancelAsync(userState);
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.6.81.0")]
    public delegate void getPreparationCompletedEventHandler(object sender, getPreparationCompletedEventArgs e);

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.6.81.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class getPreparationCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs
    {

        private object[] results;

        internal getPreparationCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) :
                base(exception, cancelled, userState)
        {
            this.results = results;
        }

        /// <remarks/>
        public Demo.HL7MessageParser.Models.GetPreparationResponse Result
        {
            get
            {
                this.RaiseExceptionIfNecessary();
                return ((Demo.HL7MessageParser.Models.GetPreparationResponse)(this.results[0]));
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.6.81.0")]
    public delegate void getDrugMdsPropertyHqCompletedEventHandler(object sender, getDrugMdsPropertyHqCompletedEventArgs e);

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.6.81.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class getDrugMdsPropertyHqCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs
    {

        private object[] results;

        internal getDrugMdsPropertyHqCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) :
                base(exception, cancelled, userState)
        {
            this.results = results;
        }

        /// <remarks/>
        public ReturnObj[] Result
        {
            get
            {
                this.RaiseExceptionIfNecessary();
                return ((ReturnObj[])(this.results[0]));
            }
        }
    }



    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.6.81.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://oracle.com/weblogic/soap/workarea/")]
    [System.Xml.Serialization.XmlRootAttribute("WorkContext", Namespace = "http://oracle.com/weblogic/soap/workarea/", IsNullable = false)]
    public partial class WorkContextSoapHeader : System.Web.Services.Protocols.SoapHeader
    {

        private System.Xml.XmlAttribute[] anyAttrField;

        private System.Xml.Serialization.XmlSerializerNamespaces xmlnsField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAnyAttributeAttribute()]
        public System.Xml.XmlAttribute[] AnyAttr
        {
            get
            {
                return this.anyAttrField;
            }
            set
            {
                this.anyAttrField = value;
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
}