using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using UnitTestProject.EnvironmentalistService;

namespace UnitTestProject
{
    [TestClass]
    public class UnitTest1
    {
        private IService1 _service = new Service1Client();

        [TestInitialize]
        public void BeforeTest()
        {
            

        }

        [TestMethod]
        public void TestMethod1()
        {
            Assert.IsTrue(_service.CheckDatabaseConnection());
        }
    }
}
