using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Mvc;
using Martian_Robots.Data;
using Martian_Robots.Data.Entities;
using Martian_Robots.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Martian_Robots.Controllers
{
    public class GameController:Controller
    {
        private readonly ILogger<GameController> _logger;
        private readonly IRepository repository;

        public GameController(ILogger<GameController> logger,IRepository repository)
        {
            _logger = logger;
            this.repository = repository;
        }
        [HttpGet]
        public Object GetMovements(DataSourceLoadOptions loadOptions)
        {
            try
            {

                var movements = repository.GetMovements();

                return DataSourceLoader.Load(movements, loadOptions);

            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to get movements: {ex}");
                ModelState.AddModelError("", "EMPTY");
                return Ok(ModelState);
            }
        }
        [HttpGet]
        public IActionResult StartRobot(string user)
        {
            try
            {
                var lastMove = repository.getLastMoveByUser(user);
                if(lastMove != null)
                {
                    return BadRequest("This robot is lost, change the name");
                }
                return Ok();

            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to get movements: {ex}");
                ModelState.AddModelError("", "EMPTY");
                return Ok(ModelState);
            }
        }
        [HttpGet]
        public IActionResult ResetGame(string user)
        {
            try
            {

                return Ok(repository.RemoveAllMovements());

            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to RemoveAllMovements: {ex}");
                ModelState.AddModelError("", "RemoveAllMovements");
                return Ok(ModelState);
            }
        }
        [HttpGet]
        public IActionResult GetRobot(string user)
        {
            try
            {

                return Ok(new robotsBoardViewModel(){
                    robot = repository.getLastMoveByUser(user),
                    robotsLost = repository.getLastAllRobotsLost()
                });

            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to GetRobot: {ex}");
                ModelState.AddModelError("", "GetRobot");
                return Ok(ModelState);
            }
        }
        [HttpPost]
        public IActionResult Move(string user,char intruction)
        {
            try
            {
                var lastMoveUser = repository.getLastMoveByUser(user);
                var settings = repository.GetSettings();

                var robotMove = new Movement();
                robotMove.Id = repository.MovementLength() + 1;
                robotMove.User = user;
                robotMove.Time = DateTime.Now;               

                positionBefore(lastMoveUser, settings, robotMove);

                switch (intruction)
                {
                    case Instruction.R:
                        robotMove.moveRigth();
                        break;

                    case Instruction.L:
                        robotMove.moveLeft();
                        break;

                    case Instruction.F:
                        robotMove.moveForward();
                        break;

                    default:

                        break;
                }



                //is other robot lost ????

                //

                //lost robot
                var isScent = robotIsLost(settings, robotMove);
                if (isScent)
                    return BadRequest("Scent in this grid");


                repository.SaveMovement(robotMove);

                return Ok(robotMove);

            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to get movements: {ex}");
                ModelState.AddModelError("", "EMPTY");
                return Ok(ModelState);
            }
        }

        private bool robotIsLost(Settings settings, Movement move)
        {


            if (move.PositionAfterX >= settings.lenX || move.PositionAfterY >= settings.lenY 
                || move.PositionAfterX < 0 || move.PositionAfterY < 0)
            {
                var robotsOut = repository.getLastAllRobotsLost();
                if (robotsOut != null)
                {
                    foreach (var r in robotsOut)
                    {
                        if (r.PositionBeforeX == move.PositionBeforeX && r.PositionBeforeY == move.PositionBeforeY) return true;
                    }
                }

                move.isOut = true;
            }
            return false;
        }

        private void positionBefore(Movement lastMoveUser, Settings settings, Movement move)
        {
            if (lastMoveUser == null)
            {
                move.OrientationBefore = OrientationBoard.S;
                move.PositionBeforeX = settings.lenX - 1;
                move.PositionBeforeY = settings.lenY - 1;
            }
            else
            {
                move.OrientationBefore = lastMoveUser.OrientationAfter;
                move.PositionBeforeX = lastMoveUser.PositionAfterX;
                move.PositionBeforeY = lastMoveUser.PositionAfterY;
            }
        }
    }
}
