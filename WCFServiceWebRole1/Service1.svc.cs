using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace WCFServiceWebRole1
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service1.svc or Service1.svc.cs at the Solution Explorer and start debugging.
    public class Service1 : IService1
    {
        private const string ConnectionString =
            "Server=tcp:parbstit.database.windows.net,1433;Database=Environmentalist;User ID=Pilsneren@parbstit;Password=Admin123;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;MultipleActiveResultSets=true";

        private SqlConnection _sqlConnection;

        public Service1()
        {
            _sqlConnection = new SqlConnection(ConnectionString);
            _sqlConnection.Open();
        }

        public bool CheckDatabaseConnection()
        {
            try
            {
                using (_sqlConnection = new SqlConnection(ConnectionString))
                {
                    _sqlConnection.Open();
                    return true;
                }
            }
            catch (SqlException)
            {
                return false;
            }

        }

        public List<Measurement> GetFiftyMeasurementsFromRoom(int roomId)
        {
            List<Measurement> measurements = new List<Measurement>();
            try
            {
                using (SqlCommand selectCommand = new SqlCommand($"SELECT TOP(50) * FROM Measurements WHERE Rooms={roomId};", _sqlConnection))
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

            return measurements;
        }

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

            return measurements;
        }

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
            return measurements;
        }

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

            return rooms;
        }

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

            return rooms;
        }

        public List<Room> GetRoomsByDate(DateTime date, bool above = true)
        {
            List<Room> rooms = new List<Room>();

            try
            {
                if (above)
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

            return rooms;
        }

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

                    return measurement;
                }
            }
            catch (SqlException)
            {
                return null;
            }
        }

        public Measurement DeleteMeasurement(Measurement measurement)
        {
            try
            {
                using (
                    SqlCommand deleteCommand = new SqlCommand($"DELETE FROM Measurements WHERE Id={measurement.Id}",
                        _sqlConnection))
                {
                    deleteCommand.ExecuteNonQuery();
                    return measurement;
                }
            }
            catch (SqlException)
            {
                return null;
            }
        }

        public Room InsertRoom(Room room)
        {
            try
            {
                using (SqlCommand insertCommand = new SqlCommand($"INSERT INTO Rooms VALUES({room.Id}, '{room.Name}');", _sqlConnection))
                {
                    insertCommand.ExecuteNonQuery();
                    return room;
                }
            }
            catch (SqlException ex)
            {
                return null;
            }
        }

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
                    return newRoom;
                }
            }
            catch (SqlException)
            {
                return null;
            }
        }

        public Room DeleteRoom(int roomId)
        {
            try
            {
                using (SqlCommand deleteCommand = new SqlCommand($"DELETE FROM Rooms WHERE Id='{roomId}';", _sqlConnection))
                {
                    deleteCommand.ExecuteNonQuery();
                    return new Room() { Id = roomId };
                }
            }
            catch (SqlException)
            {
                return null;
            }
        }
    }
}
