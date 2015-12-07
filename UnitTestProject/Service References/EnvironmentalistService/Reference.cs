﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace UnitTestProject.EnvironmentalistService {
    using System.Runtime.Serialization;
    using System;
    
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="Measurement", Namespace="http://schemas.datacontract.org/2004/07/WCFServiceWebRole1")]
    [System.SerializableAttribute()]
    public partial class Measurement : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private System.DateTime DateField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int IdField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private System.DateTime MovementField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int RoomField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private double TemperatureField;
        
        [global::System.ComponentModel.BrowsableAttribute(false)]
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData {
            get {
                return this.extensionDataField;
            }
            set {
                this.extensionDataField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.DateTime Date {
            get {
                return this.DateField;
            }
            set {
                if ((this.DateField.Equals(value) != true)) {
                    this.DateField = value;
                    this.RaisePropertyChanged("Date");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int Id {
            get {
                return this.IdField;
            }
            set {
                if ((this.IdField.Equals(value) != true)) {
                    this.IdField = value;
                    this.RaisePropertyChanged("Id");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.DateTime Movement {
            get {
                return this.MovementField;
            }
            set {
                if ((this.MovementField.Equals(value) != true)) {
                    this.MovementField = value;
                    this.RaisePropertyChanged("Movement");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int Room {
            get {
                return this.RoomField;
            }
            set {
                if ((this.RoomField.Equals(value) != true)) {
                    this.RoomField = value;
                    this.RaisePropertyChanged("Room");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public double Temperature {
            get {
                return this.TemperatureField;
            }
            set {
                if ((this.TemperatureField.Equals(value) != true)) {
                    this.TemperatureField = value;
                    this.RaisePropertyChanged("Temperature");
                }
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="Room", Namespace="http://schemas.datacontract.org/2004/07/WCFServiceWebRole1")]
    [System.SerializableAttribute()]
    public partial class Room : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int IdField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string NameField;
        
        [global::System.ComponentModel.BrowsableAttribute(false)]
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData {
            get {
                return this.extensionDataField;
            }
            set {
                this.extensionDataField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int Id {
            get {
                return this.IdField;
            }
            set {
                if ((this.IdField.Equals(value) != true)) {
                    this.IdField = value;
                    this.RaisePropertyChanged("Id");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Name {
            get {
                return this.NameField;
            }
            set {
                if ((object.ReferenceEquals(this.NameField, value) != true)) {
                    this.NameField = value;
                    this.RaisePropertyChanged("Name");
                }
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="EnvironmentalistService.IService1")]
    public interface IService1 {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService1/CheckDatabaseConnection", ReplyAction="http://tempuri.org/IService1/CheckDatabaseConnectionResponse")]
        bool CheckDatabaseConnection();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService1/CheckDatabaseConnection", ReplyAction="http://tempuri.org/IService1/CheckDatabaseConnectionResponse")]
        System.Threading.Tasks.Task<bool> CheckDatabaseConnectionAsync();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService1/GetFiftyMeasurementsFromRoom", ReplyAction="http://tempuri.org/IService1/GetFiftyMeasurementsFromRoomResponse")]
        UnitTestProject.EnvironmentalistService.Measurement[] GetFiftyMeasurementsFromRoom(int roomId);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService1/GetFiftyMeasurementsFromRoom", ReplyAction="http://tempuri.org/IService1/GetFiftyMeasurementsFromRoomResponse")]
        System.Threading.Tasks.Task<UnitTestProject.EnvironmentalistService.Measurement[]> GetFiftyMeasurementsFromRoomAsync(int roomId);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService1/GetLatestFiftyMeasurementsFromAll", ReplyAction="http://tempuri.org/IService1/GetLatestFiftyMeasurementsFromAllResponse")]
        UnitTestProject.EnvironmentalistService.Measurement[] GetLatestFiftyMeasurementsFromAll();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService1/GetLatestFiftyMeasurementsFromAll", ReplyAction="http://tempuri.org/IService1/GetLatestFiftyMeasurementsFromAllResponse")]
        System.Threading.Tasks.Task<UnitTestProject.EnvironmentalistService.Measurement[]> GetLatestFiftyMeasurementsFromAllAsync();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService1/GetMeasurementsFromDate", ReplyAction="http://tempuri.org/IService1/GetMeasurementsFromDateResponse")]
        UnitTestProject.EnvironmentalistService.Measurement[] GetMeasurementsFromDate(System.DateTime fromDate, System.DateTime toDate, int roomId);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService1/GetMeasurementsFromDate", ReplyAction="http://tempuri.org/IService1/GetMeasurementsFromDateResponse")]
        System.Threading.Tasks.Task<UnitTestProject.EnvironmentalistService.Measurement[]> GetMeasurementsFromDateAsync(System.DateTime fromDate, System.DateTime toDate, int roomId);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService1/GetLatestMeasurements", ReplyAction="http://tempuri.org/IService1/GetLatestMeasurementsResponse")]
        System.Collections.Generic.Dictionary<string, UnitTestProject.EnvironmentalistService.Measurement> GetLatestMeasurements();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService1/GetLatestMeasurements", ReplyAction="http://tempuri.org/IService1/GetLatestMeasurementsResponse")]
        System.Threading.Tasks.Task<System.Collections.Generic.Dictionary<string, UnitTestProject.EnvironmentalistService.Measurement>> GetLatestMeasurementsAsync();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService1/GetRooms", ReplyAction="http://tempuri.org/IService1/GetRoomsResponse")]
        UnitTestProject.EnvironmentalistService.Room[] GetRooms();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService1/GetRooms", ReplyAction="http://tempuri.org/IService1/GetRoomsResponse")]
        System.Threading.Tasks.Task<UnitTestProject.EnvironmentalistService.Room[]> GetRoomsAsync();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService1/GetRoomsByTemp", ReplyAction="http://tempuri.org/IService1/GetRoomsByTempResponse")]
        UnitTestProject.EnvironmentalistService.Room[] GetRoomsByTemp(double temperature, bool above);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService1/GetRoomsByTemp", ReplyAction="http://tempuri.org/IService1/GetRoomsByTempResponse")]
        System.Threading.Tasks.Task<UnitTestProject.EnvironmentalistService.Room[]> GetRoomsByTempAsync(double temperature, bool above);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService1/GetRoomsByDate", ReplyAction="http://tempuri.org/IService1/GetRoomsByDateResponse")]
        UnitTestProject.EnvironmentalistService.Room[] GetRoomsByDate(System.DateTime date, bool above);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService1/GetRoomsByDate", ReplyAction="http://tempuri.org/IService1/GetRoomsByDateResponse")]
        System.Threading.Tasks.Task<UnitTestProject.EnvironmentalistService.Room[]> GetRoomsByDateAsync(System.DateTime date, bool above);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService1/InsertMeasurement", ReplyAction="http://tempuri.org/IService1/InsertMeasurementResponse")]
        UnitTestProject.EnvironmentalistService.Measurement InsertMeasurement(UnitTestProject.EnvironmentalistService.Measurement measurement);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService1/InsertMeasurement", ReplyAction="http://tempuri.org/IService1/InsertMeasurementResponse")]
        System.Threading.Tasks.Task<UnitTestProject.EnvironmentalistService.Measurement> InsertMeasurementAsync(UnitTestProject.EnvironmentalistService.Measurement measurement);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService1/DeleteMeasurement", ReplyAction="http://tempuri.org/IService1/DeleteMeasurementResponse")]
        UnitTestProject.EnvironmentalistService.Measurement DeleteMeasurement(UnitTestProject.EnvironmentalistService.Measurement measurement);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService1/DeleteMeasurement", ReplyAction="http://tempuri.org/IService1/DeleteMeasurementResponse")]
        System.Threading.Tasks.Task<UnitTestProject.EnvironmentalistService.Measurement> DeleteMeasurementAsync(UnitTestProject.EnvironmentalistService.Measurement measurement);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService1/InsertRoom", ReplyAction="http://tempuri.org/IService1/InsertRoomResponse")]
        UnitTestProject.EnvironmentalistService.Room InsertRoom(UnitTestProject.EnvironmentalistService.Room room);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService1/InsertRoom", ReplyAction="http://tempuri.org/IService1/InsertRoomResponse")]
        System.Threading.Tasks.Task<UnitTestProject.EnvironmentalistService.Room> InsertRoomAsync(UnitTestProject.EnvironmentalistService.Room room);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService1/UpdateRoom", ReplyAction="http://tempuri.org/IService1/UpdateRoomResponse")]
        UnitTestProject.EnvironmentalistService.Room UpdateRoom(int roomId, UnitTestProject.EnvironmentalistService.Room newRoom);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService1/UpdateRoom", ReplyAction="http://tempuri.org/IService1/UpdateRoomResponse")]
        System.Threading.Tasks.Task<UnitTestProject.EnvironmentalistService.Room> UpdateRoomAsync(int roomId, UnitTestProject.EnvironmentalistService.Room newRoom);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService1/DeleteRoom", ReplyAction="http://tempuri.org/IService1/DeleteRoomResponse")]
        UnitTestProject.EnvironmentalistService.Room DeleteRoom(int roomId);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService1/DeleteRoom", ReplyAction="http://tempuri.org/IService1/DeleteRoomResponse")]
        System.Threading.Tasks.Task<UnitTestProject.EnvironmentalistService.Room> DeleteRoomAsync(int roomId);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IService1Channel : UnitTestProject.EnvironmentalistService.IService1, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class Service1Client : System.ServiceModel.ClientBase<UnitTestProject.EnvironmentalistService.IService1>, UnitTestProject.EnvironmentalistService.IService1 {
        
        public Service1Client() {
        }
        
        public Service1Client(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public Service1Client(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public Service1Client(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public Service1Client(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public bool CheckDatabaseConnection() {
            return base.Channel.CheckDatabaseConnection();
        }
        
        public System.Threading.Tasks.Task<bool> CheckDatabaseConnectionAsync() {
            return base.Channel.CheckDatabaseConnectionAsync();
        }
        
        public UnitTestProject.EnvironmentalistService.Measurement[] GetFiftyMeasurementsFromRoom(int roomId) {
            return base.Channel.GetFiftyMeasurementsFromRoom(roomId);
        }
        
        public System.Threading.Tasks.Task<UnitTestProject.EnvironmentalistService.Measurement[]> GetFiftyMeasurementsFromRoomAsync(int roomId) {
            return base.Channel.GetFiftyMeasurementsFromRoomAsync(roomId);
        }
        
        public UnitTestProject.EnvironmentalistService.Measurement[] GetLatestFiftyMeasurementsFromAll() {
            return base.Channel.GetLatestFiftyMeasurementsFromAll();
        }
        
        public System.Threading.Tasks.Task<UnitTestProject.EnvironmentalistService.Measurement[]> GetLatestFiftyMeasurementsFromAllAsync() {
            return base.Channel.GetLatestFiftyMeasurementsFromAllAsync();
        }
        
        public UnitTestProject.EnvironmentalistService.Measurement[] GetMeasurementsFromDate(System.DateTime fromDate, System.DateTime toDate, int roomId) {
            return base.Channel.GetMeasurementsFromDate(fromDate, toDate, roomId);
        }
        
        public System.Threading.Tasks.Task<UnitTestProject.EnvironmentalistService.Measurement[]> GetMeasurementsFromDateAsync(System.DateTime fromDate, System.DateTime toDate, int roomId) {
            return base.Channel.GetMeasurementsFromDateAsync(fromDate, toDate, roomId);
        }
        
        public System.Collections.Generic.Dictionary<string, UnitTestProject.EnvironmentalistService.Measurement> GetLatestMeasurements() {
            return base.Channel.GetLatestMeasurements();
        }
        
        public System.Threading.Tasks.Task<System.Collections.Generic.Dictionary<string, UnitTestProject.EnvironmentalistService.Measurement>> GetLatestMeasurementsAsync() {
            return base.Channel.GetLatestMeasurementsAsync();
        }
        
        public UnitTestProject.EnvironmentalistService.Room[] GetRooms() {
            return base.Channel.GetRooms();
        }
        
        public System.Threading.Tasks.Task<UnitTestProject.EnvironmentalistService.Room[]> GetRoomsAsync() {
            return base.Channel.GetRoomsAsync();
        }
        
        public UnitTestProject.EnvironmentalistService.Room[] GetRoomsByTemp(double temperature, bool above) {
            return base.Channel.GetRoomsByTemp(temperature, above);
        }
        
        public System.Threading.Tasks.Task<UnitTestProject.EnvironmentalistService.Room[]> GetRoomsByTempAsync(double temperature, bool above) {
            return base.Channel.GetRoomsByTempAsync(temperature, above);
        }
        
        public UnitTestProject.EnvironmentalistService.Room[] GetRoomsByDate(System.DateTime date, bool above) {
            return base.Channel.GetRoomsByDate(date, above);
        }
        
        public System.Threading.Tasks.Task<UnitTestProject.EnvironmentalistService.Room[]> GetRoomsByDateAsync(System.DateTime date, bool above) {
            return base.Channel.GetRoomsByDateAsync(date, above);
        }
        
        public UnitTestProject.EnvironmentalistService.Measurement InsertMeasurement(UnitTestProject.EnvironmentalistService.Measurement measurement) {
            return base.Channel.InsertMeasurement(measurement);
        }
        
        public System.Threading.Tasks.Task<UnitTestProject.EnvironmentalistService.Measurement> InsertMeasurementAsync(UnitTestProject.EnvironmentalistService.Measurement measurement) {
            return base.Channel.InsertMeasurementAsync(measurement);
        }
        
        public UnitTestProject.EnvironmentalistService.Measurement DeleteMeasurement(UnitTestProject.EnvironmentalistService.Measurement measurement) {
            return base.Channel.DeleteMeasurement(measurement);
        }
        
        public System.Threading.Tasks.Task<UnitTestProject.EnvironmentalistService.Measurement> DeleteMeasurementAsync(UnitTestProject.EnvironmentalistService.Measurement measurement) {
            return base.Channel.DeleteMeasurementAsync(measurement);
        }
        
        public UnitTestProject.EnvironmentalistService.Room InsertRoom(UnitTestProject.EnvironmentalistService.Room room) {
            return base.Channel.InsertRoom(room);
        }
        
        public System.Threading.Tasks.Task<UnitTestProject.EnvironmentalistService.Room> InsertRoomAsync(UnitTestProject.EnvironmentalistService.Room room) {
            return base.Channel.InsertRoomAsync(room);
        }
        
        public UnitTestProject.EnvironmentalistService.Room UpdateRoom(int roomId, UnitTestProject.EnvironmentalistService.Room newRoom) {
            return base.Channel.UpdateRoom(roomId, newRoom);
        }
        
        public System.Threading.Tasks.Task<UnitTestProject.EnvironmentalistService.Room> UpdateRoomAsync(int roomId, UnitTestProject.EnvironmentalistService.Room newRoom) {
            return base.Channel.UpdateRoomAsync(roomId, newRoom);
        }
        
        public UnitTestProject.EnvironmentalistService.Room DeleteRoom(int roomId) {
            return base.Channel.DeleteRoom(roomId);
        }
        
        public System.Threading.Tasks.Task<UnitTestProject.EnvironmentalistService.Room> DeleteRoomAsync(int roomId) {
            return base.Channel.DeleteRoomAsync(roomId);
        }
    }
}
