using Martian_Robots;
using Martian_Robots.Data.Entities;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using System.Net.Http;
using System.Threading.Tasks;

namespace Martian_Robots_Test
{
    [TestClass]
    public class GameControllerTest
    {
        private readonly TestServer _server;
        private readonly HttpClient _client;

        
        public GameControllerTest()
        {
            // Arrange
            _server = new TestServer(new WebHostBuilder()
               .UseStartup<Startup>());
            _client = _server.CreateClient();
        }
        [TestMethod]
        public async Task StartRobot()
        {
            // Act
            var response = await _client.GetAsync("/Game/StartRobot?user=Antonio");
            response.EnsureSuccessStatusCode();
            //var responseString = await response.Content.ReadAsStringAsync();
            var statusCode = response.IsSuccessStatusCode;
            // Assert
            Assert.AreEqual(true, statusCode);
        }
        [TestMethod]
        public void MoveRight()
        {
            var robot = new Movement();

            robot.OrientationBefore = OrientationBoard.S;
            robot.PositionBeforeX = 8 - 1;
            robot.PositionBeforeY = 5 - 1;

            robot.moveRigth();
            //test position x
            Assert.AreEqual(robot.PositionBeforeX - 1, robot.PositionAfterX);
            // test position Y
            Assert.AreEqual(robot.PositionBeforeY, robot.PositionAfterY);
            // test Orientacion
            Assert.AreEqual('W', robot.OrientationAfter);

        }
        [TestMethod]
        public void MoveLeft()
        {
            var robot = new Movement();

            robot.OrientationBefore = OrientationBoard.S;
            robot.PositionBeforeX = 8 - 1;
            robot.PositionBeforeY = 5 - 1;

            robot.moveLeft();
            //test position x
            Assert.AreEqual(robot.PositionBeforeX + 1, robot.PositionAfterX);
            // test position Y
            Assert.AreEqual(robot.PositionBeforeY, robot.PositionAfterY);
            // test Orientacion
            Assert.AreEqual('E', robot.OrientationAfter);

        }
        [TestMethod]
        public void MoveForward()
        {
            var robot = new Movement();

            robot.OrientationBefore = OrientationBoard.S;
            robot.PositionBeforeX = 8 - 1;
            robot.PositionBeforeY = 5 - 1;

            robot.moveForward();
            //test position x
            Assert.AreEqual(robot.PositionBeforeX, robot.PositionAfterX);
            // test position Y
            Assert.AreEqual(robot.PositionBeforeY - 1, robot.PositionAfterY);
            // test Orientacion
            Assert.AreEqual('S', robot.OrientationAfter);

        }

    }
}
