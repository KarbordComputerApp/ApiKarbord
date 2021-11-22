﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

// 
// This source code was auto-generated by Microsoft.VSDesigner, Version 4.0.30319.42000.
// 
#pragma warning disable 1591

namespace ApiKarbord.ConfirmService {
    using System;
    using System.Web.Services;
    using System.Diagnostics;
    using System.Web.Services.Protocols;
    using System.Xml.Serialization;
    using System.ComponentModel;
    
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.8.3761.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Web.Services.WebServiceBindingAttribute(Name="ConfirmServiceSoap", Namespace="https://pec.Shaparak.ir/NewIPGServices/Confirm/ConfirmService")]
    public partial class ConfirmService : System.Web.Services.Protocols.SoapHttpClientProtocol {
        
        private System.Threading.SendOrPostCallback ConfirmPaymentOperationCompleted;
        
        private System.Threading.SendOrPostCallback ConfirmPaymentWithAddDataOperationCompleted;
        
        private System.Threading.SendOrPostCallback ConfirmPaymentWithAmountOperationCompleted;
        
        private bool useDefaultCredentialsSetExplicitly;
        
        /// <remarks/>
        public ConfirmService() {
            this.Url = global::ApiKarbord.Properties.Settings.Default.ApiKarbord_ConfirmService_ConfirmService;
            if ((this.IsLocalFileSystemWebService(this.Url) == true)) {
                this.UseDefaultCredentials = true;
                this.useDefaultCredentialsSetExplicitly = false;
            }
            else {
                this.useDefaultCredentialsSetExplicitly = true;
            }
        }
        
        public new string Url {
            get {
                return base.Url;
            }
            set {
                if ((((this.IsLocalFileSystemWebService(base.Url) == true) 
                            && (this.useDefaultCredentialsSetExplicitly == false)) 
                            && (this.IsLocalFileSystemWebService(value) == false))) {
                    base.UseDefaultCredentials = false;
                }
                base.Url = value;
            }
        }
        
        public new bool UseDefaultCredentials {
            get {
                return base.UseDefaultCredentials;
            }
            set {
                base.UseDefaultCredentials = value;
                this.useDefaultCredentialsSetExplicitly = true;
            }
        }
        
        /// <remarks/>
        public event ConfirmPaymentCompletedEventHandler ConfirmPaymentCompleted;
        
        /// <remarks/>
        public event ConfirmPaymentWithAddDataCompletedEventHandler ConfirmPaymentWithAddDataCompleted;
        
        /// <remarks/>
        public event ConfirmPaymentWithAmountCompletedEventHandler ConfirmPaymentWithAmountCompleted;
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("https://pec.Shaparak.ir/NewIPGServices/Confirm/ConfirmService/ConfirmPayment", RequestNamespace="https://pec.Shaparak.ir/NewIPGServices/Confirm/ConfirmService", ResponseNamespace="https://pec.Shaparak.ir/NewIPGServices/Confirm/ConfirmService", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public ClientConfirmResponseData ConfirmPayment(ClientConfirmRequestData requestData) {
            object[] results = this.Invoke("ConfirmPayment", new object[] {
                        requestData});
            return ((ClientConfirmResponseData)(results[0]));
        }
        
        /// <remarks/>
        public void ConfirmPaymentAsync(ClientConfirmRequestData requestData) {
            this.ConfirmPaymentAsync(requestData, null);
        }
        
        /// <remarks/>
        public void ConfirmPaymentAsync(ClientConfirmRequestData requestData, object userState) {
            if ((this.ConfirmPaymentOperationCompleted == null)) {
                this.ConfirmPaymentOperationCompleted = new System.Threading.SendOrPostCallback(this.OnConfirmPaymentOperationCompleted);
            }
            this.InvokeAsync("ConfirmPayment", new object[] {
                        requestData}, this.ConfirmPaymentOperationCompleted, userState);
        }
        
        private void OnConfirmPaymentOperationCompleted(object arg) {
            if ((this.ConfirmPaymentCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.ConfirmPaymentCompleted(this, new ConfirmPaymentCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("https://pec.Shaparak.ir/NewIPGServices/Confirm/ConfirmService/ConfirmPaymentWithA" +
            "ddData", RequestNamespace="https://pec.Shaparak.ir/NewIPGServices/Confirm/ConfirmService", ResponseNamespace="https://pec.Shaparak.ir/NewIPGServices/Confirm/ConfirmService", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public ClientConfirmResponseDataAddData ConfirmPaymentWithAddData(ClientConfirmRequestData requestData) {
            object[] results = this.Invoke("ConfirmPaymentWithAddData", new object[] {
                        requestData});
            return ((ClientConfirmResponseDataAddData)(results[0]));
        }
        
        /// <remarks/>
        public void ConfirmPaymentWithAddDataAsync(ClientConfirmRequestData requestData) {
            this.ConfirmPaymentWithAddDataAsync(requestData, null);
        }
        
        /// <remarks/>
        public void ConfirmPaymentWithAddDataAsync(ClientConfirmRequestData requestData, object userState) {
            if ((this.ConfirmPaymentWithAddDataOperationCompleted == null)) {
                this.ConfirmPaymentWithAddDataOperationCompleted = new System.Threading.SendOrPostCallback(this.OnConfirmPaymentWithAddDataOperationCompleted);
            }
            this.InvokeAsync("ConfirmPaymentWithAddData", new object[] {
                        requestData}, this.ConfirmPaymentWithAddDataOperationCompleted, userState);
        }
        
        private void OnConfirmPaymentWithAddDataOperationCompleted(object arg) {
            if ((this.ConfirmPaymentWithAddDataCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.ConfirmPaymentWithAddDataCompleted(this, new ConfirmPaymentWithAddDataCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("https://pec.Shaparak.ir/NewIPGServices/Confirm/ConfirmService/ConfirmPaymentWithA" +
            "mount", RequestNamespace="https://pec.Shaparak.ir/NewIPGServices/Confirm/ConfirmService", ResponseNamespace="https://pec.Shaparak.ir/NewIPGServices/Confirm/ConfirmService", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public ClientConfirmResponseData ConfirmPaymentWithAmount(ClientConfirmWithAmountRequestData requestData) {
            object[] results = this.Invoke("ConfirmPaymentWithAmount", new object[] {
                        requestData});
            return ((ClientConfirmResponseData)(results[0]));
        }
        
        /// <remarks/>
        public void ConfirmPaymentWithAmountAsync(ClientConfirmWithAmountRequestData requestData) {
            this.ConfirmPaymentWithAmountAsync(requestData, null);
        }
        
        /// <remarks/>
        public void ConfirmPaymentWithAmountAsync(ClientConfirmWithAmountRequestData requestData, object userState) {
            if ((this.ConfirmPaymentWithAmountOperationCompleted == null)) {
                this.ConfirmPaymentWithAmountOperationCompleted = new System.Threading.SendOrPostCallback(this.OnConfirmPaymentWithAmountOperationCompleted);
            }
            this.InvokeAsync("ConfirmPaymentWithAmount", new object[] {
                        requestData}, this.ConfirmPaymentWithAmountOperationCompleted, userState);
        }
        
        private void OnConfirmPaymentWithAmountOperationCompleted(object arg) {
            if ((this.ConfirmPaymentWithAmountCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.ConfirmPaymentWithAmountCompleted(this, new ConfirmPaymentWithAmountCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        public new void CancelAsync(object userState) {
            base.CancelAsync(userState);
        }
        
        private bool IsLocalFileSystemWebService(string url) {
            if (((url == null) 
                        || (url == string.Empty))) {
                return false;
            }
            System.Uri wsUri = new System.Uri(url);
            if (((wsUri.Port >= 1024) 
                        && (string.Compare(wsUri.Host, "localHost", System.StringComparison.OrdinalIgnoreCase) == 0))) {
                return true;
            }
            return false;
        }
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlIncludeAttribute(typeof(ClientConfirmWithAmountRequestData))]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.8.3761.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="https://pec.Shaparak.ir/NewIPGServices/Confirm/ConfirmService")]
    public partial class ClientConfirmRequestData {
        
        private string loginAccountField;
        
        private long tokenField;
        
        /// <remarks/>
        public string LoginAccount {
            get {
                return this.loginAccountField;
            }
            set {
                this.loginAccountField = value;
            }
        }
        
        /// <remarks/>
        public long Token {
            get {
                return this.tokenField;
            }
            set {
                this.tokenField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlIncludeAttribute(typeof(ClientConfirmResponseDataAddData))]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.8.3761.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="https://pec.Shaparak.ir/NewIPGServices/Confirm/ConfirmService")]
    public partial class ClientConfirmResponseData {
        
        private short statusField;
        
        private string cardNumberMaskedField;
        
        private long rRNField;
        
        private long tokenField;
        
        /// <remarks/>
        public short Status {
            get {
                return this.statusField;
            }
            set {
                this.statusField = value;
            }
        }
        
        /// <remarks/>
        public string CardNumberMasked {
            get {
                return this.cardNumberMaskedField;
            }
            set {
                this.cardNumberMaskedField = value;
            }
        }
        
        /// <remarks/>
        public long RRN {
            get {
                return this.rRNField;
            }
            set {
                this.rRNField = value;
            }
        }
        
        /// <remarks/>
        public long Token {
            get {
                return this.tokenField;
            }
            set {
                this.tokenField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.8.3761.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="https://pec.Shaparak.ir/NewIPGServices/Confirm/ConfirmService")]
    public partial class ClientConfirmResponseDataAddData : ClientConfirmResponseData {
        
        private string addDataField;
        
        /// <remarks/>
        public string AddData {
            get {
                return this.addDataField;
            }
            set {
                this.addDataField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.8.3761.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="https://pec.Shaparak.ir/NewIPGServices/Confirm/ConfirmService")]
    public partial class ClientConfirmWithAmountRequestData : ClientConfirmRequestData {
        
        private long orderIdField;
        
        private long amountField;
        
        /// <remarks/>
        public long OrderId {
            get {
                return this.orderIdField;
            }
            set {
                this.orderIdField = value;
            }
        }
        
        /// <remarks/>
        public long Amount {
            get {
                return this.amountField;
            }
            set {
                this.amountField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.8.3761.0")]
    public delegate void ConfirmPaymentCompletedEventHandler(object sender, ConfirmPaymentCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.8.3761.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class ConfirmPaymentCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal ConfirmPaymentCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public ClientConfirmResponseData Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((ClientConfirmResponseData)(this.results[0]));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.8.3761.0")]
    public delegate void ConfirmPaymentWithAddDataCompletedEventHandler(object sender, ConfirmPaymentWithAddDataCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.8.3761.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class ConfirmPaymentWithAddDataCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal ConfirmPaymentWithAddDataCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public ClientConfirmResponseDataAddData Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((ClientConfirmResponseDataAddData)(this.results[0]));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.8.3761.0")]
    public delegate void ConfirmPaymentWithAmountCompletedEventHandler(object sender, ConfirmPaymentWithAmountCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.8.3761.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class ConfirmPaymentWithAmountCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal ConfirmPaymentWithAmountCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public ClientConfirmResponseData Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((ClientConfirmResponseData)(this.results[0]));
            }
        }
    }
}

#pragma warning restore 1591