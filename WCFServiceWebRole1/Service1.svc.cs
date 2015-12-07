using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.Serialization;
using System.Security.AccessControl;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Web;
using System.Text;
using System.Web;

namespace WCFServiceWebRole1
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service1.svc or Service1.svc.cs at the Solution Explorer and start debugging.
    public class Service1 : IService1
    {
        /// <summary>
        /// Connection string to the Azure hosted database in use. 
        /// </summary>
        private const string ConnectionString =
            "Server=tcp:parbstit.database.windows.net,1433;Database=Environmentalist;User ID=Pilsneren@parbstit;Password=Admin123;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;MultipleActiveResultSets=true";

        private SqlConnection _sqlConnection;
        private string _localAddress;
        


        /// <summary>
        /// Constructor used to initialize and establish SQL-connection to the database, while also setting up tracing. 
        /// </summary>
        public Service1()
        {
            _sqlConnection = new SqlConnection(ConnectionString);
            _sqlConnection.Open();
            
            //TracingSetup();
        }

        /// <summary>
        /// Testmethod to ensure correct connection to the database. 
        /// </summary>
        /// <returns></returns>
        public bool CheckDatabaseConnection()
        {
            try
            {
                using (_sqlConnection = new SqlConnection(ConnectionString))
                {
                    _sqlConnection.Open();

                    OperationContext context = OperationContext.Current;
                    MessageProperties myProp = context.IncomingMessageProperties;
                    RemoteEndpointMessageProperty endpoint = myProp[RemoteEndpointMessageProperty.Name] as RemoteEndpointMessageProperty;
                    _localAddress = endpoint.Address;
                   
                    Trace.WriteLine(_localAddress);


                    return true;

                }
            }
            catch (SqlException)
            {
                return false;
            }

        }

        /// <summary>
        /// Returns the latest fifty measurements from the specified room.
        /// </summary>
        /// <param name="roomId">Id of the room to get latest fifty measurements from.</param>
        /// <returns></returns>
        public List<Measurement> GetFiftyMeasurementsFromRoom(int roomId)
        {
            List<Measurement> measurements = new List<Measurement>();

            try
            {

                using (SqlCommand selectCommand = new SqlCommand($"SELECT TOP(50) * FROM Measurements WHERE Rooms={roomId} ORDER BY Date DESC;", _sqlConnection))
                {
                    var reader = selectCommand.ExecuteReader();

                    while (reader.Read())
                    {
                        var m = new Measurement()
                        {
                            Id = reader.GetInt32(0),
                            Room = reader.GetInt32(1),
                            Temperature = reader.GetDouble(2),
                            Movement = reader.GetDateTime(3),
                            Date = reader.GetDateTime(4)
                        };
                        measurements.Add(m);
                    }

                }
            }
            catch (SqlException)
            {

            }

            //Trace.WriteLine($"\n{DateTime.Now}: Method GetFiftyMeasurementsFromRoom() was called from client IP: {_localAddress}.");

            return measurements;
        }

        /// <summary>
        /// Returns all measurements between the specified dates from the room with the specified id.
        /// </summary>
        /// <param name="fromDate">From-date.</param>
        /// <param name="toDate">To-date.</param>
        /// <param name="roomId">Id of the room to get measurements from.</param>
        /// <returns></returns>
        public List<Measurement> GetMeasurementsFromDate(DateTime fromDate, DateTime toDate, int roomId)
        {
            List<Measurement> measurements = new List<Measurement>();

            try
            {
                using (SqlCommand selectCommand = new SqlCommand($"SELECT * FROM Measurements WHERE Date BETWEEN @fromdate AND @todate AND Rooms=@roomNumber;", _sqlConnection))
                {
                    selectCommand.Parameters.AddWithValue("@fromdate", fromDate);
                    selectCommand.Parameters.AddWithValue("@todate", toDate);
                    selectCommand.Parameters.AddWithValue("@roomNumber", roomId);

                    var reader = selectCommand.ExecuteReader();

                    while (reader.Read())
                    {
                        var m = new Measurement()
                        {
                            Id = reader.GetInt32(0),
                            Room = reader.GetInt32(1),
                            Temperature = reader.GetDouble(2),
                            Movement = reader.GetDateTime(3),
                            Date = reader.GetDateTime(4)
                        };
                        measurements.Add(m);
                    }
                }


            }
            catch (SqlException)
            {

            }
            //Trace.WriteLine($"\n{DateTime.Now}: Method GetMeasurementsFromDate() was called from client IP: {_localAddress}.");
            return measurements;
        }

        /// <summary>
        /// Returns the single latest measurement from all rooms. 
        /// </summary>
        /// <returns></returns>
        public Dictionary<string, Measurement> GetLatestMeasurements()
        {
            Dictionary<string, Measurement> measurements = new Dictionary<string, Measurement>();


            try
            {
                using (
                    SqlCommand insertCommand =
                        new SqlCommand(
                            $"SELECT Rooms.Id AS RoomId, Rooms.Name AS RoomName, MAX(Measurements.Date) AS Date, MAX(Measurements.Id) AS Id, MAX(Measurements.Temperature) AS Temperature, MAX(Measurements.Movement) AS Movement FROM Measurements INNER JOIN Rooms ON Rooms.Id = Measurements.Rooms GROUP BY Rooms.Name, Rooms.Id",
                            _sqlConnection))
                {
                    var reader = insertCommand.ExecuteReader();

                    while (reader.Read())
                    {
                        var m = new Measurement()
                        {
                            Room = reader.GetInt32(0),
                            Date = reader.GetDateTime(2),
                            Id = reader.GetInt32(3),
                            Temperature = reader.GetDouble(4),
                            Movement = reader.GetDateTime(5)

                        };
                        measurements.Add(reader.GetString(1), m);
                    }
                }
            }
            catch (SqlException)
            {
                return null;
            }
           // Trace.WriteLine($"\n{DateTime.Now}: Method GetLatestMeasurements() was called from client IP: {_localAddress}.");
            return measurements;
        }

        /// <summary>
        /// Returns the fifty latest measurements from all rooms. 
        /// </summary>
        /// <returns></returns>
        public List<Measurement> GetLatestFiftyMeasurementsFromAll()
        {
            List<Measurement> measurements = new List<Measurement>();


            try
            {
                using (
                    SqlCommand insertCommand =
                        new SqlCommand(
                            @"SELECT Measurements.Id, Measurements.Rooms, Measurements.Temperature, 
                            Measurements.Movement, Measurements.Date, Rooms.Name
                            FROM (
                                SELECT Measurements.*, Rank() 
                                over(Partition BY Measurements.Rooms 
                                ORDER BY Measurements.Date DESC) AS Rank
                                FROM Measurements
                            ) Measurements 
                            INNER JOIN Rooms ON Rooms.Id = Measurements.Rooms 
                            WHERE Rank <= 50 ",
                            _sqlConnection))
                {
                    var reader = insertCommand.ExecuteReader();

                    while (reader.Read())
                    {
                        var m = new Measurement()
                        {
                            Id = reader.GetInt32(0),
                            Room = reader.GetInt32(1),
                            Temperature = reader.GetDouble(2),
                            Movement = reader.GetDateTime(3),
                            Date = reader.GetDateTime(4)
                        };
                        measurements.Add(m);
                    }
                }
            }
            catch (SqlException)
            {
                return null;
            }
            //Trace.WriteLine($"\n{DateTime.Now}: Method GetFiftyMeasurementsFromAll() was called from client IP: {_localAddress}.");
            return measurements;
        }

        /// <summary>
        /// Returns all rooms.
        /// </summary>
        /// <returns></returns>
        public List<Room> GetRooms()
        {
            List<Room> rooms = new List<Room>();

            try
            {
                using (SqlCommand selectCommand = new SqlCommand("SELECT * FROM Rooms;", _sqlConnection))
                {
                    var reader = selectCommand.ExecuteReader();

                    while (reader.Read())
                    {
                        var r = new Room() { Id = reader.GetInt32(0), Name = reader.GetString(1) };
                        rooms.Add(r);
                    }
                }
            }
            catch (SqlException)
            {

            }
            Trace.WriteLine($"\n{DateTime.Now}: Method GetRooms() was called from client IP: {_localAddress}.");
            return rooms;
        }

        /// <summary>
        /// Returns all rooms above/below the specified temperature. 
        /// </summary>
        /// <param name="temperature">Temperature.</param>
        /// <param name="above">If left true, the method returns the rooms above the specifed temperature. If set to false, the method returns the rooms below the specified temperature.</param>
        /// <returns></returns>
        public List<Room> GetRoomsByTemp(double temperature, bool above = true)
        {
            List<Room> rooms = new List<Room>();

            try
            {
                if (above)
                {
                    using (
                        SqlCommand selectAboveCommand =
                            new SqlCommand(
                                $"SELECT r.Id, r.Name FROM Rooms r, Measurements m WHERE r.Id = m.Rooms AND m.Temperature>{temperature};",
                                _sqlConnection)
                        )
                    {
                        var reader = selectAboveCommand.ExecuteReader();

                        while (reader.Read())
                        {
                            var r = new Room() { Id = reader.GetInt32(0), Name = reader.GetString(1) };
                            rooms.Add(r);
                        }
                    }
                }
                else
                {
                    using (
                        SqlCommand selectBelowCommand =
                            new SqlCommand(
                                $"SELECT r.Id, r.Name FROM Rooms r, Measurements m WHERE r.Id = m.Rooms AND m.Temperature<{temperature};",
                                _sqlConnection))
                    {
                        var reader = selectBelowCommand.ExecuteReader();

                        while (reader.Read())
                        {
                            var r = new Room() { Id = reader.GetInt32(0), Name = reader.GetString(1) };
                            rooms.Add(r);
                        }
                    }
                }
            }
            catch (SqlException)
            {

            }
            //Trace.WriteLine($"\n{DateTime.Now}: Method GetRoomsByTemp() was called from client IP: {_localAddress}.");
            return rooms;
        }

        /// <summary>
        /// Returns all rooms with detected movement after/before the specified date.
        /// </summary>
        /// <param name="date">Date.</param>
        /// <param name="after">If left true, the method returns the rooms that detected movement after the specified date. If set to false, the method returns the rooms that detected movement before the specified date.</param>
        /// <returns></returns>
        public List<Room> GetRoomsByDate(DateTime date, bool after = true)
        {
            List<Room> rooms = new List<Room>();

            try
            {
                if (after)
                {
                    using (
                        SqlCommand selectAboveCommand =
                            new SqlCommand(
                                $"SELECT r.Id, r.Name FROM Rooms r, Measurements m WHERE r.Id = m.Rooms AND m.Date >= '{date}';",
                                _sqlConnection)
                        )
                    {
                        var reader = selectAboveCommand.ExecuteReader();

                        while (reader.Read())
                        {
                            var r = new Room() { Id = reader.GetInt32(0), Name = reader.GetString(1) };
                            rooms.Add(r);
                        }
                    }
                }
                else
                {
                    using (
                        SqlCommand selectBelowCommand =
                            new SqlCommand(
                                $"SELECT r.Id, r.Name FROM Rooms r, Measurements m WHERE r.Id = m.Rooms AND m.Date <= '{date}';",
                                _sqlConnection))
                    {
                        var reader = selectBelowCommand.ExecuteReader();

                        while (reader.Read())
                        {
                            var r = new Room() { Id = reader.GetInt32(0), Name = reader.GetString(1) };
                            rooms.Add(r);
                        }
                    }
                }
            }
            catch (SqlException)
            {

            }
            //Trace.WriteLine($"\n{DateTime.Now}: Method GetRoomsByDate() was called from client IP: {_localAddress}.");
            return rooms;
        }

        /// <summary>
        /// Inserts the specified Measurement object into the database. 
        /// </summary>
        /// <param name="measurement">The Measurement to upload to the database.</param>
        /// <returns></returns>
        public Measurement InsertMeasurement(Measurement measurement)
        {
            try
            {
                using (
                    SqlCommand insertCommand =
                        new SqlCommand(
                            $"INSERT INTO Measurements (Rooms, Temperature, Movement, Date) VALUES(@Rooms, @Temperature, @Movement, @Date);",
                            _sqlConnection))
                {
                    insertCommand.Parameters.AddWithValue("@Rooms", measurement.Room);
                    insertCommand.Parameters.AddWithValue("@Temperature", measurement.Temperature);
                    insertCommand.Parameters.AddWithValue("@Movement", measurement.Movement);
                    insertCommand.Parameters.AddWithValue("@Date", measurement.Date);
                    insertCommand.ExecuteNonQuery();

                    //Trace.WriteLine($"\n{DateTime.Now}: Method InsertMeasurement() was called from client IP: {_localAddress}.");
                    return measurement;
                }
            }
            catch (SqlException)
            {
                return null;
            }
        }

        /// <summary>
        /// Deletes the specified Measurement object from the database.
        /// </summary>
        /// <param name="measurement">The Measurement to remove from the database.</param>
        /// <returns></returns>
        public Measurement DeleteMeasurement(Measurement measurement)
        {
            try
            {
                using (
                    SqlCommand deleteCommand = new SqlCommand($"DELETE FROM Measurements WHERE Id={measurement.Id}",
                        _sqlConnection))
                {
                    deleteCommand.ExecuteNonQuery();

                    //Trace.WriteLine($"\n{DateTime.Now}: Method DeleteMeasurement() was called from client IP: {_localAddress}.");
                    return measurement;
                }
            }
            catch (SqlException)
            {
                return null;
            }
        }

        /// <summary>
        /// Inserts the specified Room object into the database. 
        /// </summary>
        /// <param name="room">The Room to upload to the database.</param>
        /// <returns></returns>
        public Room InsertRoom(Room room)
        {
            try
            {
                using (SqlCommand insertCommand = new SqlCommand($"INSERT INTO Rooms VALUES({room.Id}, '{room.Name}');", _sqlConnection))
                {
                    insertCommand.ExecuteNonQuery();

                    //Trace.WriteLine($"\n{DateTime.Now}: Method InsertRoom() was called from client IP: {_localAddress}.");
                    return room;
                }
            }
            catch (SqlException ex)
            {
                return null;
            }
        }

        /// <summary>
        /// Updates the specified room with the new specfied Room object.
        /// </summary>
        /// <param name="roomId">The id of the room to update.</param>
        /// <param name="newRoom">The Room object to update to.</param>
        /// <returns></returns>
        public Room UpdateRoom(int roomId, Room newRoom)
        {
            try
            {
                using (
                    SqlCommand updateCommand =
                        new SqlCommand(
                            $"UPDATE Rooms SET Id={newRoom.Id}, Name='{newRoom.Name}' WHERE Id='{roomId}';", _sqlConnection))
                {
                    updateCommand.ExecuteNonQuery();

                    //Trace.WriteLine($"\n{DateTime.Now}: Method UpdateRoom() was called from client IP: {_localAddress}.");
                    return newRoom;
                }
            }
            catch (SqlException)
            {
                return null;
            }
        }

        /// <summary>
        /// Deletes the specified room from the database.
        /// </summary>
        /// <param name="roomId">The id of the room to remove from the database.</param>
        /// <returns></returns>
        public Room DeleteRoom(int roomId)
        {
            try
            {
                using (SqlCommand deleteCommand = new SqlCommand($"DELETE FROM Rooms WHERE Id='{roomId}';", _sqlConnection))
                {
                    deleteCommand.ExecuteNonQuery();

                    //Trace.WriteLine($"\n{DateTime.Now}: Method DeleteRoom() was called from client IP: {_localAddress}.");
                    return new Room() { Id = roomId };
                }
            }
            catch (SqlException)
            {
                return null;
            }
        }

        /// <summary>
        /// Private method used to setup correct tracing settings. 
        /// </summary>
        private void TracingSetup()
        {
            Trace.AutoFlush = true;
            var filename = @"C:/Environmentalist/Log.txt";
            Directory.CreateDirectory(Path.GetDirectoryName(filename));
            FileStream fs = new FileStream(filename, FileMode.Append, FileAccess.Write, FileShare.Write);
            Trace.Listeners.Add(new TextWriterTraceListener(fs));

            
        }
    }
}
