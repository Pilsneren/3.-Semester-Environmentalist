using System;
using System.Runtime.InteropServices;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using UnitTestProject.EnvironmentalistService;

namespace UnitTestProject
{
    [TestClass]
    public class UnitTest1
    {
        private IService1 _service;

        [TestInitialize]
        public void BeforeTest()
        {
            _service = new Service1Client();

        }

        /// <summary>
        /// Testing for correct establishment to the webservice. 
        /// </summary>
        [TestMethod]
        public void TestMethod1()
        {
            Assert.IsTrue(_service.CheckDatabaseConnection());
        }

        /// <summary>
        /// General testing for returned data. 
        /// </summary>
        [TestMethod]
        public void TestMethod2()
        {
            DateTime fromDate = new DateTime(2015, 12, 3);
            DateTime toDate = new DateTime(2015, 12, 6);

            var measurementsByDate = _service.GetMeasurementsFromDate(fromDate, toDate, 115);

            Assert.IsNotNull(measurementsByDate);
        }

        /// <summary>
        /// Ensures that only the latest fifty measurements from the specified room is recieved. 
        /// </summary>
        [TestMethod]
        public void TestMethod3()
        {
            var latestFiftyMeasurementsFromAll = _service.GetFiftyMeasurementsFromRoom(205);

            Assert.AreEqual(50, latestFiftyMeasurementsFromAll.Length);
        }

        /// <summary>
        /// Ensures that only the latest measurement from each room is returned. 
        /// </summary>
        [TestMethod]
        public void TestMethod4()
        {
            var rooms = _service.GetRooms();
            var latestMeasurements = _service.GetLatestMeasurements();

            Assert.AreEqual(rooms.Length, latestMeasurements.Count);
        }
       
    }

}
