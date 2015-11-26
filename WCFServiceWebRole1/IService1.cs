using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.Web.ModelBinding;

namespace WCFServiceWebRole1
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
    [ServiceContract]
    public interface IService1
    {
        [OperationContract]
        bool CheckDatabaseConnection();

        [OperationContract]
        List<Measurement> GetMeasurementsFromRooms(int roomName);

        [OperationContract]
        List<Measurement> GetMeasurementsFromDate(DateTime fromDate, DateTime toDate, int roomName);

        [OperationContract]
        List<Room> GetRooms();

        [OperationContract]
        List<Room> GetRoomsByTemp(double temperature, bool above = true);

        [OperationContract]
        List<Room> GetRoomsByDate(DateTime date, bool above = true);


    }


    // Use a data contract as illustrated in the sample below to add composite types to service operations.
   [DataContract]
    public class Measurement
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public int Room { get; set; }
        [DataMember]
        public double Temperature { get; set; }
        [DataMember]
        public DateTime Movement { get; set; }
        [DataMember]
        public DateTime Date { get; set; }


    }

    [DataContract]
    public class Room
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public string Name { get; set; }


        
    }
}
