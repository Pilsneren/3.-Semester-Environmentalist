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
            "Server=tcp:parbstit.database.windows.net,1433;Database=Environmentalist;User ID=Pilsneren@parbstit;Password=Admin123;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
        
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

        public List<Measurement> GetMeasurementsFromRooms(int roomName)
        {
            List<Measurement> measurements = new List<Measurement>();

            try
            {
                    using (SqlCommand selectCommand = new SqlCommand($"SELECT * FROM Measurements WHERE Rooms={roomName};", _sqlConnection))
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

        public List<Measurement> GetMeasurementsFromDate(DateTime fromDate, DateTime toDate, int roomName)
        {
            List<Measurement> measurements = new List<Measurement>();
            var from = new DateTime(2015, 11, 26, 07, 30, 11);
            var to = new DateTime(2015, 11, 26, 14, 00, 00);

            try
            {
                using ( SqlCommand selectCommand = new SqlCommand("SELECT * FROM Measurements WHERE Date BETWEEN @fromdate AND @todate;", _sqlConnection))
                {
                    selectCommand.Parameters.AddWithValue("@fromdate", fromDate);
                    selectCommand.Parameters.AddWithValue("@todate", toDate);
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
                        var r = new Room() {Id = reader.GetInt32(0), Name = reader.GetString(1)};
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
                            var r = new Room() {Id = reader.GetInt32(0), Name = reader.GetString(1)};
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

        public Room UpdateRoom(string roomToBeUpdated, Room newRoom)
        {
            try
            {
                using (
                    SqlCommand updateCommand =
                        new SqlCommand(
                            $"UPDATE Rooms SET Id={newRoom.Id}, Name='{newRoom.Name}' WHERE Name='{roomToBeUpdated}';", _sqlConnection))
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

        public Room DeleteRoom(string roomName)
        {
            try
            {
                using (SqlCommand deleteCommand = new SqlCommand($"DELETE FROM Rooms WHERE Name='{roomName}';", _sqlConnection))
                {
                    deleteCommand.ExecuteNonQuery();
                    return new Room() {Name = roomName};
                }
            }
            catch (SqlException)
            {
                return null; 
            }
        }
    }
}
