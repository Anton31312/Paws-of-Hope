﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан программой.
//     Исполняемая версия:4.0.30319.42000
//
//     Изменения в этом файле могут привести к неправильной работе и будут потеряны в случае
//     повторной генерации кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Paws_of_Hope.ServiceAuth {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="ServiceAuth.IServiceAuth")]
    public interface IServiceAuth {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IServiceAuth/Connect", ReplyAction="http://tempuri.org/IServiceAuth/ConnectResponse")]
        int Connect(string login, string password);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IServiceAuth/Connect", ReplyAction="http://tempuri.org/IServiceAuth/ConnectResponse")]
        System.Threading.Tasks.Task<int> ConnectAsync(string login, string password);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IServiceAuth/Disconnect", ReplyAction="http://tempuri.org/IServiceAuth/DisconnectResponse")]
        void Disconnect(int id);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IServiceAuth/Disconnect", ReplyAction="http://tempuri.org/IServiceAuth/DisconnectResponse")]
        System.Threading.Tasks.Task DisconnectAsync(int id);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IServiceAuthChannel : Paws_of_Hope.ServiceAuth.IServiceAuth, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class ServiceAuthClient : System.ServiceModel.ClientBase<Paws_of_Hope.ServiceAuth.IServiceAuth>, Paws_of_Hope.ServiceAuth.IServiceAuth {
        
        public ServiceAuthClient() {
        }
        
        public ServiceAuthClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public ServiceAuthClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public ServiceAuthClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public ServiceAuthClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public int Connect(string login, string password) {
            return base.Channel.Connect(login, password);
        }
        
        public System.Threading.Tasks.Task<int> ConnectAsync(string login, string password) {
            return base.Channel.ConnectAsync(login, password);
        }
        
        public void Disconnect(int id) {
            base.Channel.Disconnect(id);
        }
        
        public System.Threading.Tasks.Task DisconnectAsync(int id) {
            return base.Channel.DisconnectAsync(id);
        }
    }
}
