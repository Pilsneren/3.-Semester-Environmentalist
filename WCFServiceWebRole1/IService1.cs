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
        List<Measurement> GetFiftyMeasurementsFromRoom(int roomId);

        [OperationContract]
        List<Measurement> GetLatestFiftyMeasurementsFromAll();

        [OperationContract]
        List<Measurement> GetMeasurementsFromDate(DateTime fromDate, DateTime toDate, int roomId);

        [OperationContract]
        Dictionary<string, Measurement> GetLatestMeasurements();

        [OperationContract]
        List<Room> GetRooms();

        [OperationContract]
        List<Room> GetRoomsByTemp(double temperature, bool above = true);

        [OperationContract]
        List<Room> GetRoomsByDate(DateTime date, bool above = true);

        [OperationContract]
        Measurement InsertMeasurement(Measurement measurement);

        [OperationContract]
        Measurement DeleteMeasurement(Measurement measurement);

        [OperationContract]
        Room InsertRoom(Room room);

        [OperationContract]
        Room UpdateRoom(int roomId, Room newRoom);

        [OperationContract]
        Room DeleteRoom(int roomId);

    }


    // Use a data contract as illustrated in the sample below to add composite types to service operations.

   [DataContract]
    public class Measurement
    {
        /// <summary>
        /// The id of the measurement. Can be left as null.
        /// </summary>
        [DataMember]
        public int Id { get; set; }

        /// <summary>
        /// The id of the room the measurement belongs to.
        /// </summary>
        [DataMember]
        public int Room { get; set; }

        /// <summary>
        /// The temperature of the room.
        /// </summary>
        [DataMember]
        public double Temperature { get; set; }

        /// <summary>
        /// The date and time when movement was last detected in the room.
        /// </summary>
        [DataMember]
        public DateTime Movement { get; set; }

        /// <summary>
        /// The date and time for the latest movement detection.
        /// </summary>
        [DataMember]
        public DateTime Date { get; set; }


    }

    [DataContract]
    public class Room
    {
        /// <summary>
        /// The id of the room.
        /// </summary>
        [DataMember]
        public int Id { get; set; }

        /// <summary>
        /// The name of the room. 
        /// </summary>
        [DataMember]
        public string Name { get; set; }


        
    }
}
