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
            catch (SqlException ex)
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

            string convertedFromDate = fromDate.ToString("yyyy-MM-dd");
            string convertedToDate = toDate.ToString("yyyy-MM-dd");



            try
            {
                using (
                    SqlCommand selectCommand =
                        new SqlCommand(
                            $"SELECT * FROM Measurements WHERE Date BETWEEN '{DateTime.Parse(convertedFromDate)}' AND '{convertedToDate}';", _sqlConnection)
                    )
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
    }
}
